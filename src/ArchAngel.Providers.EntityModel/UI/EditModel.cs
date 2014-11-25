using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
//using System.Windows.Markup;
using ArchAngel.Interfaces;
using ArchAngel.Interfaces.Controls.ContentItems;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Controller.MappingLayer;
using ArchAngel.Providers.EntityModel.Controller.Validation;
using ArchAngel.Providers.EntityModel.Controller.Validation.Modules;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using ArchAngel.Providers.EntityModel.UI.Presenters;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;
using DevComponents.AdvTree;
using log4net;
using Slyce.Common;
using CollectionChangeAction = Slyce.Common.CollectionChangeAction;
//using Colors = System.Windows.Media.Colors;
using Component = ArchAngel.Providers.EntityModel.Model.EntityLayer.Component;
//using MessageBox = System.Windows.MessageBox;
using RibbonBar = ArchAngel.Interfaces.RibbonBar;

namespace ArchAngel.Providers.EntityModel.UI
{
	public partial class EditModel : ContentItem, IMainPanel, RibbonBarContentItem, IUserInteractor
	{
		private const string EntitySetKey = "Entities";
		private const string TablesKey = "Tables";
		private const string ViewsKey = "Views";

		/// <summary>
		/// Don't remove any of these values because they correspond to images in the TreeImages imagelist.
		/// </summary>
		// ReSharper disable UnusedMember.Local
		private enum EntityTreeImages
		{
			Database,
			AddDatabase,
			Table,
			StoredProcedure,
			View,
			Filter,
			Index,
			Key,
			Entity,
			Column,
			FolderClosed,
			FolderOpen
		}
		private static Image UndoImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.undo_square_16.png"));
		private static Image UndoImageDisabled = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.undo_square_16_d.png"));

		// ReSharper restore UnusedMember.Local
		private MappingSet MappingSet { get; set; }
		//private readonly PresenterController presenterController;
		//private readonly SchemaDiagrammerController schemaDiagrammerController;
		//private readonly DatabasePresenter databasePresenter;
		private bool IgnoreChangedNode = false;

		private LinkedListNode<IModelObject> currentlyDisplayedObject;
		private readonly LinkedList<IModelObject> displayedObjects = new LinkedList<IModelObject>();

		private readonly Dictionary<Guid, List<Node>> treeNodes = new Dictionary<Guid, List<Node>>();
		private readonly Dictionary<Guid, IModelObject> modelObjects = new Dictionary<Guid, IModelObject>();
		private readonly Dictionary<Guid, ReferenceNodeSet> referenceNodes = new Dictionary<Guid, ReferenceNodeSet>();
		private readonly List<Node> TableNodes = new List<Node>();
		private readonly List<Node> ViewNodes = new List<Node>();
		private readonly List<Node> EntityNodes = new List<Node>();
		private readonly Timer searchBarTimer;
		private readonly AutoCompleteStringCollection tableNameList = new AutoCompleteStringCollection();
		private readonly AutoCompleteStringCollection viewNameList = new AutoCompleteStringCollection();
		private Form modelDialogForm;
		private RibbonBarController RibbonBarController;
		private RibbonButton oneToOneEntityMappingButton;
		internal static bool BusyPopulating = false;
		private Control HostedControl = null;
		private bool ComponentAddedFromContextMenu = false;
		private Node ComponentsNode;
		private Node EntitySetNode;
		private List<Node> TableCollectionNodes = new List<Node>();
		internal static EditModel Instance = null;

		public ProviderInfo Provider { get; set; }

		private readonly HashSet<IModelObject> objectsThatAreBeingMonitored = new HashSet<IModelObject>(new InternalIdentifierComparer());

		private class InternalIdentifierComparer : IEqualityComparer<IModelObject>
		{
			public bool Equals(IModelObject x, IModelObject y)
			{
				return x.InternalIdentifier == y.InternalIdentifier;
			}

			public int GetHashCode(IModelObject obj)
			{
				return obj.InternalIdentifier.GetHashCode();
			}
		}

		internal static void SortNodes()
		{
			Instance.advTree1.BeginUpdate();
			Instance.EntitySetNode.Nodes.Sort();

			foreach (Node node in Instance.TableCollectionNodes)
				node.Nodes.Sort();

			Instance.advTree1.EndUpdate();
		}

		private static readonly ILog log = LogManager.GetLogger(typeof(EditModel));

		public EditModel()
		{
			InitializeComponent();
			//this.mainWindow1 = new SchemaDiagrammer.MainWindow();
			//this.elementHost1.Child = this.mainWindow1;

			Instance = this;
			panelEx3.Controls.Clear();
			Name = "Model";
			Title = "Model";
			PageDescription = "This screen is where you pull your database tables in, and define the entities that they map to.";
			NavBarIcon = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.Resources.flow_chart_16.png"));

			diagrammer.EntitySelected += new EntityForm2.EntityActionDelegate(diagrammer_EntitySelected);
			diagrammer.TableSelected += new EntityForm2.TableActionDelegate(diagrammer_TableSelected);

			//mainWindow1.DiagramBackgroundColour = Colors.White;

			//presenterController = new PresenterController(this);
			//schemaDiagrammerController = new SchemaDiagrammerController(mainWindow1, this);

			//databasePresenter = presenterController.GetPresenter<DatabasePresenter>();
			//databasePresenter.NewDatabaseCreated += databasePresenter_NewDatabaseCreated;

			ClearDisplayedObjectsList();

			// This sets the themes for the Schema Diagrammer.
			//using (var stream = GetType().Assembly.GetManifestResourceStream("ArchAngel.Providers.EntityModel.ShapeStyles.xaml"))
			//{
			//    if (stream == null) throw new Exception("Could not locate Schema Diagrammer theme resource. (ArchAngel.Providers.EntityModel.ShapeStyles.xaml)");

			//    mainWindow1.Resources.MergedDictionaries.Add((ResourceDictionary)XamlReader.Load(stream));
			//}

			//validationFailureView1.NavigateToObject += (s, e) =>
			//                                            {
			//                                                ShowObjectPropertyGrid(e.Object);
			//                                                schemaDiagrammerController.ShowTag(e.Object);
			//                                            };
			validationFailureView1.CloseView += (s, e) => HideValidationResults();
			validationFailureView1.ReValidateRequested += new EventHandler(validationFailureView1_ReValidateRequested);
			validationFailureView1.NavigateToObject += new EventHandler<GenericEventArgs<IModelObject>>(validationFailureView1_NavigateToObject);

			// Enable and disable the clear search box button when the search bar text changes.
			//tbddSearchBar.TextChanged += (sender, e) => btnClearSearchBox.Enabled = !string.IsNullOrEmpty(tbddSearchBar.Text);
			//textBoxSearch.TextChanged += (sender, e) => btnClearSearchBox.Enabled = !string.IsNullOrEmpty(textBoxSearch.Text);

			searchBarTimer = new Timer();
			searchBarTimer.Interval = 3000; // 3 seconds
			searchBarTimer.Tick += SearchBarTimerTick;

			tbddSearchBar.AutoCompleteMode = AutoCompleteMode.Suggest;
			tbddSearchBar.AutoCompleteSource = AutoCompleteSource.CustomSource;
			tbddSearchBar.AutoCompleteCustomSource = tableNameList;
			Populate();
		}

		private void diagrammer_TableSelected(ITable table)
		{
			SyncCurrentlySelectedObject(table);
		}

		private void diagrammer_EntitySelected(EntityImpl entity)
		{
			SyncCurrentlySelectedObject(entity);
		}

		void validationFailureView1_NavigateToObject(object sender, GenericEventArgs<IModelObject> e)
		{
			if (e.Object is Entity)
				foreach (Node node in EntitySetNode.Nodes)
				{
					if (node.Tag == e.Object)
					{
						advTree1.SelectedNode = node;
						break;
					}
				}
			else if (e.Object is ReferenceImpl)
			{
				Entity entity = ((ReferenceImpl)e.Object).Entity2;

				foreach (Node node in EntitySetNode.Nodes)
				{
					if (node.Tag == entity)
					{
						advTree1.SelectedNode = node;
						break;
					}
				}
			}
			else if (e.Object is Table)
				foreach (Node tablesNode in TableCollectionNodes)
					foreach (Node node in tablesNode.Nodes)
					{
						if (node.Tag == e.Object)
						{
							advTree1.SelectedNode = node;
							break;
						}
					}
		}

		void validationFailureView1_ReValidateRequested(object sender, EventArgs e)
		{
			CheckModel();
		}

		private void ClearDisplayedObjectsList()
		{
			displayedObjects.Clear();
			displayedObjects.AddFirst(new LinkedListNode<IModelObject>(null));
			currentlyDisplayedObject = displayedObjects.First;
		}

		public override bool Save()
		{
			//var presenter = presenterController.GetCurrentPresenter() as DatabasePresenter;
			//if (presenter != null)
			//{
			//    MappingSet.Database.Loader = presenter.CreateDatabaseLoader();
			//}
			return true;
		}

		public override void OnDisplaying()
		{
			base.OnDisplaying();

			if (advTree1.SelectedNode == null)
			{
				SetFocusToFirstEntityNode();
			}
		}

		//void databasePresenter_NewDatabaseCreated(object sender, EventArgs e)
		//{
		//    if (InvokeRequired)
		//    {
		//        Invoke(new MethodInvoker(() => databasePresenter_NewDatabaseCreated(sender, e)));
		//        return;
		//    }

		//    MappingSet.Database = databasePresenter.Database;
		//    Populate();
		//}

		public override void Clear()
		{
			MappingSet = null;
			ClearObjectsThatAreBeingMonitored();
			referenceNodes.Clear();
			ClearDisplayedObjectsList();
			Populate();
		}

		private void ClearObjectsThatAreBeingMonitored()
		{
			foreach (var model in objectsThatAreBeingMonitored)
			{
				model.PropertyChanged -= obj_PropertyChanged;
				if (model is EntitySet)
				{
					((EntitySet)model).ReferencesChanged -= entities_ReferencesChanged;
					((EntitySet)model).EntitiesChanged -= entities_EntitiesChanged;
					((EntitySet)model).ComponentSpecsChanged -= entities_ComponentSpecsChanged;
				}
				else if (model is Entity)
				{
					//((Entity)model).PropertiesChanged -= entity_PropertiesChanged;
				}
				else if (model is IDatabase)
				{
					((IDatabase)model).TablesChanged -= db_TablesChanged;
					((IDatabase)model).ViewsChanged -= db_ViewsChanged;
				}
				else if (model is ITable)
				{
					((ITable)model).ColumnsChanged -= table_ColumnsChanged;
				}
				else if (model is ComponentSpecification)
				{
					((ComponentSpecification)model).ImplementedComponentsChanged -= spec_ImplementedComponentsChanged;
					((ComponentSpecification)model).PropertiesChanged -= spec_PropertiesChanged;
				}
			}

			objectsThatAreBeingMonitored.Clear();
		}

		private void ClearTree()
		{
			advTree1.Nodes.Clear();
			TableNodes.Clear();
			EntityNodes.Clear();
			tableNameList.Clear();
			treeNodes.Clear();
			ClearObjectsThatAreBeingMonitored();
		}

		private void RefreshTree()
		{
			foreach (IModelObject obj in modelObjects.Values)
			{
				obj.PropertyChanged -= obj_PropertyChanged;
			}

			Populate();
		}

		public void SetDatabase(IDatabase db)
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(() => SetDatabase(db)));
				return;
			}

			if (db == null) throw new ArgumentNullException("db");

			if (MappingSet == null)
			{
				MappingSet = Provider.MappingSet;
			}

			MappingSet.Database = db;

			Populate();
		}

		public void SetMappingSet(MappingSet set)
		{
			if (set == null) throw new ArgumentNullException("set");

			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(() => SetMappingSet(set)));
				return;
			}

			MappingSet = set;

			Populate();
		}

		private void Populate()
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(() => Populate()));
				return;
			}
			BusyPopulating = true;
			advTree1.BeginUpdate();

			ClearTree();

			if (MappingSet != null)
			{
				AddDatabaseToTree(MappingSet.Database, advTree1);
				AddEntitySetToTree(MappingSet.EntitySet, advTree1);
			}

			advTree1.EndUpdate(true);
			BusyPopulating = false;
		}

		private void AddEntitySetToTree(EntitySet entities, AdvTree parentNode)
		{
			EntitySetNode = new Node
									{
										Text = "Entities",
										Name = EntitySetKey,
										ImageIndex = (int)EntityTreeImages.FolderClosed,
										ImageExpandedIndex = (int)EntityTreeImages.FolderOpen,
									};

			AddToTree(parentNode, EntitySetNode, entities);

			foreach (Entity entity in entities.Entities)
			{
				AddEntityToTree(entity, EntitySetNode);
			}

			AddComponentSpecificationsToTree(entities, EntitySetNode);

			// Only add the event handler once.
			if (!objectsThatAreBeingMonitored.Contains(entities))
			{
				entities.EntitiesChanged += entities_EntitiesChanged;
				entities.ReferencesChanged += entities_ReferencesChanged;
				entities.ComponentSpecsChanged += entities_ComponentSpecsChanged;
				objectsThatAreBeingMonitored.Add(entities);
			}
		}

		private void AddComponentSpecificationsToTree(EntitySet entities, Node entitySetNode)
		{
			ComponentsNode = new Node
			{
				Text = "Components",
				Name = "Components",
				ImageIndex = (int)EntityTreeImages.FolderClosed,
				ImageExpandedIndex = (int)EntityTreeImages.FolderOpen
			};
			advTree1.Nodes.Add(ComponentsNode);
			//entitySetNode.Nodes.Add(componentsNode);

			foreach (ComponentSpecification spec in entities.ComponentSpecifications)
				AddComponentSpecToTree(spec, ComponentsNode);
		}

		private void AddComponentSpecToTree(ComponentSpecification spec, Node entitySetNode)
		{
			Node specNode = new Node
								{
									Text = spec.Name,
									Name = spec.Name,
									ImageIndex = (int)EntityTreeImages.Entity
								};
			AddToTree(entitySetNode, specNode, spec);

			AddComponentPropertiesToTree(spec, specNode);
			AddComponentsToTreeUnderSpecification(spec, specNode);

			// Only add the event handler once.
			if (!objectsThatAreBeingMonitored.Contains(spec))
			{
				spec.ImplementedComponentsChanged += spec_ImplementedComponentsChanged;
				spec.PropertiesChanged += spec_PropertiesChanged;
				objectsThatAreBeingMonitored.Add(spec);
			}
			if (!BusyPopulating && ComponentAddedFromContextMenu)
				advTree1.SelectedNode = specNode;// GFH
		}

		private void AddComponentPropertiesToTree(ComponentSpecification spec, Node specNode)
		{
			Node propertiesNode = new Node
								{
									Text = "Properties",
									Name = "Properties",
									ImageIndex = (int)EntityTreeImages.FolderClosed,
									ImageExpandedIndex = (int)EntityTreeImages.FolderOpen
								};
			specNode.Nodes.Add(propertiesNode);

			foreach (var property in spec.Properties)
			{
				AddComponentPropertyToTree(property, propertiesNode);
			}
		}

		private void AddComponentPropertyToTree(ComponentProperty property, Node propertiesNode)
		{
			Node propertyNode = new Node()
									{
										Text = property.Name,
										Name = property.Name,
										ImageIndex = (int)EntityTreeImages.Column
									};
			AddToTree(propertiesNode, propertyNode, property);
		}

		private void AddComponentsToTreeUnderSpecification(ComponentSpecification spec, Node specNode)
		{
			foreach (var component in spec.ImplementedComponents)
			{
				AddComponentToTreeUnderSpecification(component, specNode);
			}
		}

		private void AddComponentToTreeUnderSpecification(Component component, Node specNode)
		{
			Node componentNode = new Node
									{
										Text = component.ParentEntity.Name,
										Name = component.ParentEntity.Name,
									};
			AddToTree(specNode, componentNode, component);
		}

		//private void AddComponentToTreeUnderEntity(Component component, Node componentsNode)
		//{
		//    Node componentNode = new Node
		//    {
		//        Text = component.Name,
		//        Name = component.Name,
		//    };
		//    AddToTree(componentsNode, componentNode, component);
		//}

		//private void AddComponentsToTreeUnderEntity(Entity entity, Node entityNode)
		//{
		//    Node componentsNode = new Node
		//                            {
		//                                Text = "Components",
		//                                Name = "Components"
		//                            };
		//    entityNode.Nodes.Add(componentsNode);

		//    foreach (var component in entity.Components)
		//    {
		//        AddComponentToTreeUnderEntity(component, componentsNode);
		//    }
		//}

		private void AddEntityToTree(Entity entity, Node parentNode)
		{
			Node entityNode = new Node
								{
									Text = entity.Name + "  ",
									Name = entity.Name,
									ImageIndex = (int)EntityTreeImages.Entity
								};

			AddToTree(parentNode, entityNode, entity);
			EntityNodes.Add(entityNode);

			//AddEntityKeyToTree(entity, entityNode);

			//AddPropertiesToTree(entity, entityNode);
			//AddReferencesToTree(entity, entityNode);
			//AddComponentsToTreeUnderEntity(entity, entityNode);

			// Only add the event handler once.
			if (!objectsThatAreBeingMonitored.Contains(entity))
			{
				//entity.PropertiesChanged += entity_PropertiesChanged;
				//entity.ComponentsChanged += entity_ComponentsChanged;
				objectsThatAreBeingMonitored.Add(entity);
			}
			if (!BusyPopulating)
				advTree1.SelectedNode = entityNode;
		}

		//private void AddEntityKeyToTree(Entity entity, Node node)
		//{
		//    Node keyNode = new Node
		//    {
		//        Text = "EntityKey",
		//        Name = "EntityKey",
		//        ImageIndex = ((int)EntityTreeImages.Key),
		//    };

		//    AddToTree(node, keyNode, entity.Key);
		//    // Needs no monitoring, there is nothing in the tree that has any information about the key.
		//}

		//private void AddReferencesToTree(Entity entity, Node node)
		//{
		//    Node referencesNode = new Node
		//                            {
		//                                Text = "References",
		//                                Name = "References",
		//                                ImageIndex = ((int)EntityTreeImages.Filter),
		//                                Tag = new CollectionPlaceholder(entity, typeof(Reference))
		//                            };
		//    node.Nodes.Add(referencesNode);

		//    // This is a collection of all references from this entity that have
		//    // already been processed. This stops us adding self references twice.
		//    HashSet<Guid> handledReferences = new HashSet<Guid>();

		//    foreach (var directedReference in entity.DirectedReferences)
		//    {
		//        if (handledReferences.Contains(directedReference.Reference.InternalIdentifier))
		//            continue;

		//        AddReferenceToTree(directedReference, referencesNode);

		//        handledReferences.Add(directedReference.Reference.InternalIdentifier);
		//    }
		//}

		//private void AddReferenceToTree(DirectedReference reference, Node node)
		//{
		//    ReferenceNodeSet nodeSet;

		//    Guid identifier = reference.Reference.InternalIdentifier;
		//    if (referenceNodes.ContainsKey(identifier) == false)
		//    {
		//        nodeSet = new ReferenceNodeSet();
		//        referenceNodes.Add(identifier, nodeSet);
		//    }
		//    else
		//    {
		//        nodeSet = referenceNodes[identifier];
		//    }

		//    bool direction = reference.Reference.Entity1 == reference.FromEntity;

		//    string nodeText = reference.FromName;
		//    if (reference.FromEntity == reference.ToEntity)
		//    {
		//        nodeText = GetSelfReferenceNodeName(reference.Reference);
		//        // Also set a different icon for the refNode in here.
		//    }

		//    Node refNode = new Node
		//                    {
		//                        Text = nodeText,
		//                        Name = reference.Reference.InternalIdentifier.ToString()
		//                    };
		//    AddToTree(node, refNode, reference.Reference);
		//    if (direction)
		//        nodeSet.Entity1Node = refNode;
		//    else
		//        nodeSet.Entity2Node = refNode;
		//}

		//private void AddPropertiesToTree(Entity entity, Node node)
		//{
		//    Node propertiesNode = new Node
		//                            {
		//                                Text = "Properties",
		//                                Name = "Properties",
		//                                ImageIndex = ((int)EntityTreeImages.FolderClosed),
		//                                ImageExpandedIndex = ((int)EntityTreeImages.FolderOpen),
		//                                Tag = new CollectionPlaceholder(entity, typeof(Property))
		//                            };
		//    node.Nodes.Add(propertiesNode);

		//    foreach (Property property in entity.ConcreteProperties)
		//    {
		//        AddPropertyToTree(property, propertiesNode);
		//    }
		//}

		//private void AddPropertyToTree(Property property, Node propertiesNode)
		//{
		//    Node propertyNode = new Node
		//    {
		//        Text = property.Name,
		//        Name = property.Name,
		//        ImageIndex = ((int)EntityTreeImages.Column)
		//    };
		//    AddToTree(propertiesNode, propertyNode, property);
		//}

		private void AddDatabaseToTree(IDatabase db, AdvTree parent)
		{
			Node databaseNode = new Node
									{
										Text = db.Name,
										Name = db.Name,
										ImageIndex = ((int)EntityTreeImages.Database)
									};

			switch (db.DatabaseType)
			{
				case DatabaseTypes.Firebird:
					if (!string.IsNullOrEmpty(Path.GetFileName(db.Name)))
						databaseNode.Text = Path.GetFileName(db.Name);
					break;
			}
			AddToTree(parent, databaseNode, db);

			#region Schemas
			List<string> schemas = db.Tables.Select(t => t.Schema).Distinct().OrderBy(s => s).ToList();

			if (schemas.Count == 0)
				schemas.Add("");

			TableCollectionNodes.Clear();

			foreach (string schema in schemas)
			{
				Node tablesNode = new Node
				{
					Text = "Tables",
					Name = TablesKey,
					ImageIndex = ((int)EntityTreeImages.FolderClosed),
					ImageExpandedIndex = ((int)EntityTreeImages.FolderOpen)
				};
				TableCollectionNodes.Add(tablesNode);

				Node viewsNode = new Node
				{
					Text = "Views",
					Name = ViewsKey,
					ImageIndex = ((int)EntityTreeImages.FolderClosed),
					ImageExpandedIndex = ((int)EntityTreeImages.FolderOpen)
				};
				TableCollectionNodes.Add(viewsNode);

				// Only add a 'schemas' node if there are multiple schemas
				if (schemas.Count > 1)
				{
					Node schemaNode = new Node
					{
						Text = schema,
						Name = "Schema_" + schema,
						ImageIndex = ((int)EntityTreeImages.FolderClosed),
						ImageExpandedIndex = ((int)EntityTreeImages.FolderOpen)
					};
					databaseNode.Nodes.Add(schemaNode);
					schemaNode.Nodes.Add(tablesNode);
					schemaNode.Nodes.Add(viewsNode);
				}
				else
				{
					databaseNode.Nodes.Add(tablesNode);
					databaseNode.Nodes.Add(viewsNode);
				}
				foreach (ITable table in db.Tables.Where(t => t.Schema == schema).OrderBy(t => t.Name))
					AddTableToTree(table, tablesNode);

				foreach (ITable view in db.Views.Where(v => v.Schema == schema).OrderBy(v => v.Name))
					AddViewToTree(view, viewsNode);
			}
			#endregion

			if (!objectsThatAreBeingMonitored.Contains(db))
			{
				objectsThatAreBeingMonitored.Add(db);
				db.TablesChanged += db_TablesChanged;
				db.ViewsChanged += new EventHandler<CollectionChangeEvent<ITable>>(db_ViewsChanged);
			}
		}

		void db_ViewsChanged(object sender, CollectionChangeEvent<ITable> e)
		{
			IViewContainer db = sender as IViewContainer;
			if (db == null) return;

			CollectionChangedHandler(db, e, "Views", AddViewToTree, e.ChangedObject.Name);
		}

		private void AddTableToTree(ITable table, Node tablesNode)
		{
			var tableName = table.Name;
			Node tableNode = new Node
								{
									Text = tableName + "  ",
									Name = tableName,
									ImageIndex = ((int)EntityTreeImages.Table)
								};
			AddToTree(tablesNode, tableNode, table);

			TableNodes.Add(tableNode);
			tableNameList.Add(tableName);

			//AddColumnsToTree(table, tableNode);
			//AddIndexesToTree(table, tableNode);
			//AddKeysToTree(table, tableNode);

			if (!objectsThatAreBeingMonitored.Contains(table))
			{
				table.ColumnsChanged += table_ColumnsChanged;
				objectsThatAreBeingMonitored.Add(table);
			}
			if (!BusyPopulating)
				advTree1.SelectedNode = tableNode;
		}

		private void AddViewToTree(ITable view, Node viewsNode)
		{
			var viewName = view.Name;
			Node viewNode = new Node
			{
				Text = viewName + "  ",
				Name = viewName,
				ImageIndex = ((int)EntityTreeImages.View)
			};
			AddToTree(viewsNode, viewNode, view);

			ViewNodes.Add(viewNode);
			viewNameList.Add(viewName);

			if (!objectsThatAreBeingMonitored.Contains(view))
			{
				view.ColumnsChanged += new EventHandler<CollectionChangeEvent<IColumn>>(view_ColumnsChanged);
				objectsThatAreBeingMonitored.Add(view);
			}
			if (!BusyPopulating)
				advTree1.SelectedNode = viewNode;
		}

		void view_ColumnsChanged(object sender, CollectionChangeEvent<IColumn> e)
		{
			IColumnContainer view = sender as IColumnContainer;
			if (view == null) return;

			CollectionChangedHandler(view, e, "Columns", AddColumnToTree, e.ChangedObject.Name);
		}

		/// <summary>
		/// Adds the new child node to the parent node, adds the child to the node
		/// lookup table with the key as the obj paramenter, and sets the child's tag 
		/// to obj.
		/// </summary>
		/// <param name="obj">The object that the new child node is representing</param>
		/// <param name="parentNode">The parent of the new child</param>
		/// <param name="newChildNode">The newly created child node that represents obj.</param>
		private void AddToTree(AdvTree parentNode, Node newChildNode, IModelObject obj)
		{
			NodeCollection nodes = parentNode.Nodes;
			AddToNodeCollection(nodes, newChildNode, obj);
		}

		/// <summary>
		/// Adds the new child node to the parent node, adds the child to the node
		/// lookup table with the key as the obj paramenter, and sets the child's tag 
		/// to obj.
		/// </summary>
		/// <param name="obj">The object that the new child node is representing</param>
		/// <param name="parentNode">The parent of the new child</param>
		/// <param name="newChildNode">The newly created child node that represents obj.</param>
		private void AddToTree(Node parentNode, Node newChildNode, IModelObject obj)
		{
			NodeCollection nodes = parentNode.Nodes;
			AddToNodeCollection(nodes, newChildNode, obj);
		}

		private void AddToNodeCollection(NodeCollection nodes, Node newChildNode, IModelObject obj)
		{
			nodes.Add(newChildNode);

			if (treeNodes.ContainsKey(obj.InternalIdentifier) == false)
			{
				treeNodes[obj.InternalIdentifier] = new List<Node>();
				modelObjects[obj.InternalIdentifier] = obj;
			}

			treeNodes[obj.InternalIdentifier].Add(newChildNode);

			newChildNode.Tag = obj;

			obj.PropertyChanged += obj_PropertyChanged;
		}

		private void SetNodeName(Node node, object sender)
		{
			if (sender is IDatabase)
			{
				node.Name = node.Text = ((IDatabase)sender).Name;
			}
			else if (sender is IScriptBase)
			{
				node.Name = node.Text = ((IScriptBase)sender).Name;
			}
			else if (sender is Mapping)
			{
				Mapping mapping = sender as Mapping;
				node.Name = node.Text = mapping.FromTable.Name + " to " + mapping.ToEntity.Name;
			}
			else if (sender is Entity)
			{
				node.Name = node.Text = ((Entity)sender).Name;
			}
			else if (sender is ITable)
			{
				node.Name = node.Text = ((ITable)sender).Name;
			}
			else if (sender is Property)
			{
				node.Name = node.Text = ((Property)sender).Name;
			}
			else if (sender is Reference)
			{
				Reference reference = ((Reference)sender);
				var nodeSet = referenceNodes[reference.InternalIdentifier];
				bool isEntity1Node = node == nodeSet.Entity1Node;

				string newName = isEntity1Node ? reference.End1Name : reference.End2Name;
				if (nodeSet.Entity2Node == null)
					newName = GetSelfReferenceNodeName(reference);

				node.Text = newName;
			}
			else if (sender is Component && node.Parent.Text == "Components")
			{
				node.Name = node.Text = ((Component)sender).Name;
			}
			else if (sender is Component)
			{
				node.Name = node.Text = ((Component)sender).ParentEntity.Name;
			}
			else if (sender is ComponentSpecification)
			{
				node.Name = node.Text = ((ComponentSpecification)sender).Name;
			}
			else if (sender is ComponentProperty)
			{
				node.Name = node.Text = ((ComponentProperty)sender).Name;
			}
			else
			{
				throw new ArgumentException("Not handling the case where the sender object is a " + sender.GetType() + " in EditModel2.SetNodeName()");
			}
		}

		private string GetSelfReferenceNodeName(Reference reference)
		{
			if (reference.End1Enabled == false && reference.End2Enabled)
				return reference.End2Name;
			if (reference.End1Enabled && reference.End2Enabled == false)
				return reference.End1Name;

			return reference.End1Name + " && " + reference.End2Name;
		}

		private void CollectionChangedHandler<P>(IModelObject entity, CollectionChangeEvent<P> e, string collectionNodeName, Action<P, Node> addFunction, string objectName)
			where P : class
		{
			List<Node> nodeList = treeNodes[entity.InternalIdentifier];

			advTree1.BeginUpdate();

			foreach (Node node in nodeList)
			{
				// find the collection node. If it doesn't exist, skip this node.
				Node collectionNode = FindFirstNodeNamed(collectionNodeName, node.Nodes);

				if (collectionNode != null)
				{
					switch (e.ChangeType)
					{
						case CollectionChangeAction.Addition:
							addFunction(e.ChangedObject, collectionNode);
							break;
						case CollectionChangeAction.Deletion:
							Node removedNode = FindFirstNodeNamed(objectName, collectionNode.Nodes);
							if (removedNode != null) removedNode.Remove();
							break;
						default:
							throw new Exception("Collection Change Type not handled");
					}
				}
			}

			advTree1.EndUpdate();
		}

		private void CollectionChangedHandler_No_Child<T>(IModelObject entity, CollectionChangeEvent<T> e, Action<T, Node> addFunction, string objectName)
			where T : class
		{
			List<Node> nodeList = treeNodes[entity.InternalIdentifier];

			advTree1.BeginUpdate();

			foreach (Node node in nodeList)
			{
				switch (e.ChangeType)
				{
					case CollectionChangeAction.Addition:
						addFunction(e.ChangedObject, node);
						node.Nodes.Sort();
						break;
					case CollectionChangeAction.Deletion:
						Node removedNode = FindFirstNodeNamed(objectName, node.Nodes);
						if (removedNode != null)
							removedNode.Remove();
						break;
					default:
						throw new Exception("Collection Change Type not handled");
				}
			}

			advTree1.EndUpdate();
		}

		private Node FindFirstNodeNamed(string name, NodeCollection collection)
		{
			Node[] nodes = collection.Find(name, false);
			return nodes.Length > 0 ? nodes[0] : null;
		}

		private static bool IsContainerName(string name)
		{
			switch (name)
			{
				case "Tables":
				case "Columns":
				//case "Indexes":
				case "Keys":
					return true;
				default:
					return false;
			}
		}

		//private void AddKeysToTree(IKeyContainer table, Node tableNode)
		//{
		//    Node keysNode = new Node
		//                        {
		//                            Text = "Keys",
		//                            ImageIndex = ((int)EntityTreeImages.Key)
		//                        };
		//    keysNode.Tag = new CollectionPlaceholder(table, typeof(IKey));
		//    tableNode.Nodes.Add(keysNode);

		//    foreach (Key key in table.Keys)
		//    {
		//        Node keyNode = new Node { Text = key.Name };
		//        AddToTree(keysNode, keyNode, key);
		//    }
		//}

		//private void AddIndexesToTree(IIndexContainer table, Node tableNode)
		//{
		//    Node indexesNode = new Node
		//                        {
		//                            Text = "Indexes",
		//                            ImageIndex = ((int)EntityTreeImages.Index)
		//                        };
		//    indexesNode.Tag = new CollectionPlaceholder(table, typeof(IIndex));
		//    tableNode.Nodes.Add(indexesNode);

		//    foreach (Index index in table.Indexes)
		//    {
		//        Node indexNode = new Node { Text = index.Name };
		//        AddToTree(indexesNode, indexNode, index);
		//    }
		//}

		//private void AddColumnsToTree(IColumnContainer table, Node tableNode)
		//{
		//    Node columnsNode = new Node
		//                        {
		//                            Text = "Columns",
		//                            Name = "Columns",
		//                            ImageIndex = ((int)EntityTreeImages.FolderClosed),
		//                            ImageExpandedIndex = ((int)EntityTreeImages.FolderOpen)
		//                        };
		//    columnsNode.Tag = new CollectionPlaceholder(table, typeof(IColumn));
		//    tableNode.Nodes.Add(columnsNode);

		//    foreach (IColumn col in table.Columns)
		//    {
		//        AddColumnToTree(col, columnsNode);
		//    }
		//}

		private void AddColumnToTree(IColumn col, Node columnsNode)
		{
			Node colNode = new Node { Text = col.Name, Name = col.Name };
			colNode.ImageIndex = ((int)EntityTreeImages.Column);
			AddToTree(columnsNode, colNode, col);
		}

		///// <summary>
		///// 
		///// </summary>
		///// <param name="item">The property grid to show. If null, the grid panel will be cleared.</param>
		//public void ShowPropertyGrid(IEditorForm item)
		//{
		//    if (InvokeRequired)
		//    {
		//        Invoke(new MethodInvoker(() => ShowPropertyGrid(item)));
		//        return;
		//    }

		//    panelPropertyGrid.Controls.Clear();

		//    if (item == null) return;

		//    Control value = item as Control;
		//    if (value == null)
		//        throw new ArgumentException("The IEditorForm implementation " + item.GetType() +
		//                                    " needs to extend UserControl in order to be used in EditModel2.");

		//    var oldCursor = Cursor;
		//    Cursor = Cursors.WaitCursor;
		//    try
		//    {
		//        Utility.SuspendPainting(panelPropertyGrid);
		//        panelPropertyGrid.Controls.Add(value);
		//        value.Dock = DockStyle.Fill;
		//        Utility.ResumePainting(panelPropertyGrid);
		//    }
		//    finally { Cursor = oldCursor; }
		//}

		public void SyncCurrentlySelectedObject(IModelObject obj)
		{
			if (obj == null)
				return;

			SetNewlyDisplayedObject(obj);
			if (treeNodes.ContainsKey(obj.InternalIdentifier))
			{
				var node = treeNodes[obj.InternalIdentifier].FirstOrDefault();

				if (node != null)
				{
					IgnoreChangedNode = true;
					advTree1.SelectedNode = node;
					IgnoreChangedNode = false;
				}
			}
		}

		DialogResult IMainPanel.ShowDialog(Form form)
		{
			return form.ShowDialog(this);
		}

		//public void DisableDiagramRefresh()
		//{
		//    schemaDiagrammerController.DisableDiagramRefresh();
		//}

		//public void EnableDiagramRefresh()
		//{
		//    schemaDiagrammerController.EnableDiagramRefresh();
		//}

		//public void ShowOnDiagram(IModelObject modelObject)
		//{
		//    schemaDiagrammerController.ClearDiagram();
		//    schemaDiagrammerController.ShowTag(modelObject);
		//}

		private IModelObject CurrentlyDisplayedObject
		{
			get { return currentlyDisplayedObject.Value; }
		}

		public void ShowObjectPropertyGrid(IModelObject obj)
		{
			if (obj == CurrentlyDisplayedObject) return;

			SetNewlyDisplayedObject(obj);
			//presenterController.ShowPresenter(CurrentlyDisplayedObject);
		}

		private void SetNewlyDisplayedObject(IModelObject obj)
		{
			if (obj != null && ModelObjectsEqual(obj, CurrentlyDisplayedObject) == false)
			{
				// Clear any nodes after this one.
				while (displayedObjects.Last != currentlyDisplayedObject)
				{
					displayedObjects.RemoveLast();
				}

				// Add the new node
				var newNode = displayedObjects.AddLast(obj);
				currentlyDisplayedObject = newNode;
				//UpdateNavigationButtons();
			}
			else if (obj == null)
			{
				currentlyDisplayedObject = displayedObjects.First;
				//UpdateNavigationButtons();
			}
			RibbonBarController.RefreshButtonStatus(this);
		}

		private bool ModelObjectsEqual(IModelObject obj1, IModelObject obj2)
		{
			if (obj1 == null && obj2 == null) return true;
			if (obj1 == null || obj2 == null) return false;

			return obj1.InternalIdentifier == obj2.InternalIdentifier;
		}

		private void DisplayPreviouslyDisplayedObject()
		{
			if (CanShowPreviouslyDisplayedObject())
			{
				currentlyDisplayedObject = currentlyDisplayedObject.Previous;

				//presenterController.ShowPresenter(CurrentlyDisplayedObject);
				//schemaDiagrammerController.ShowTag(CurrentlyDisplayedObject);
				//UpdateNavigationButtons();
			}
		}

		private void DisplayNextDisplayedObject()
		{
			if (CanShowNextDisplayedObject())
			{
				currentlyDisplayedObject = currentlyDisplayedObject.Next;

				//presenterController.ShowPresenter(CurrentlyDisplayedObject);
				//schemaDiagrammerController.ShowTag(CurrentlyDisplayedObject);
				//UpdateNavigationButtons();
			}
		}

		//private void UpdateNavigationButtons()
		//{
		//    buttonBack.Enabled = CanShowPreviouslyDisplayedObject();
		//    buttonNext.Enabled = CanShowNextDisplayedObject();
		//}

		private bool CanShowNextDisplayedObject()
		{
			return currentlyDisplayedObject.Next != null;
		}

		private bool CanShowPreviouslyDisplayedObject()
		{
			if (currentlyDisplayedObject.Previous != null)
				return currentlyDisplayedObject.Previous.Previous != null;

			return currentlyDisplayedObject.Previous != null;
		}

		//private void AddItemToCrumbBar(IModelObject obj)
		//{
		//    if(obj == null) return;

		//    CrumbBarItem item = new CrumbBarItem
		//                            {
		//                                Tag = obj,
		//                                Text = obj.DisplayName,

		//                            };
		//    item.Click += item_Click;

		//    crumbBar1.Items.Add(item);
		//    crumbBar1.SelectedItem = item;
		//}

		//void item_Click(object sender, EventArgs e)
		//{
		//    var item = sender as CrumbBarItem;
		//    if(item == null) return;

		//    ShowObjectPropertyGridWithoutCrumbBarItem(item.Tag as IModelObject);
		//}

		public void ShowDatabaseRefreshResults(DatabaseMergeResult results, IDatabase db1, IDatabase db2)
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(() => ShowDatabaseRefreshResults(results, db1, db2)));
				return;
			}

#if DEBUG
			UI.Editors.UserControls.DatabaseSynchroEditor form = new ArchAngel.Providers.EntityModel.UI.Editors.UserControls.DatabaseSynchroEditor();
			form.Fill(results, db1, db2);
			form.ShowDialog(this);
#else

			var form = new FormDatabaseRefreshResults();

			var simplePresenter = new SimpleDBMergeResultPresenter(form, this, results);
			var detailedPresenter = new DetailedDBMergeResultPresenter(form, new DBMergeResultProcessor(results));

			detailedPresenter.Show();
			simplePresenter.Show();
#endif
		}

		public void ShowDatabaseRefreshResultsForm(ISimpleDBMergeResultForm form)
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(() => ShowDatabaseRefreshResultsForm(form)));
				return;
			}

			Control control = form as Control;
			if (control == null)
				throw new ArgumentException("The ISimpleDBMergeResultForm implementation " + form.GetType() +
											" needs to extend Control in order to be used in EditModel2.");

			ShowModalForm(control);
		}

		public void CloseDatabaseRefreshResultsForm(Changes status)
		{
			CloseModalForm();

			if (status == Changes.WereAccepted)
			{
				//schemaDiagrammerController.ClearDiagram();
				//schemaDiagrammerController.ShowTag(MappingSet.Database);
				Populate();
				SyncCurrentlySelectedObject(MappingSet.Database);
			}
		}

		public void CloseCreateOneToOneMappingForm(Changes status)
		{
			CloseModalForm();

			if (status == Changes.WereAccepted)
			{
				//schemaDiagrammerController.ClearDiagram();
				//schemaDiagrammerController.ShowTag(MappingSet.EntitySet);
				Populate();
				SyncCurrentlySelectedObject(MappingSet.EntitySet);
			}
		}

		private void CloseModalForm()
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(CloseModalForm));
				return;
			}

			if (modelDialogForm == null) return;

			modelDialogForm.Close();
			modelDialogForm = null;
		}

		public void ShowCreateOneToOneMappingForm()
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(ShowCreateOneToOneMappingForm));
				return;
			}

			var form = new FormCreateOneToOneMapping(MappingSet);
			form.ShowDialog();
			//var presenter = new CreateOneToOneMappingPresenter(form, this, MappingSet);

			//presenter.Show();
		}

		public void ShowCreateOneToOneMappingForm(ICreateOneToOneMappingsForm form)
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(() => ShowCreateOneToOneMappingForm(form)));
				return;
			}

			Control control = form as Control;
			if (control == null)
				throw new ArgumentException("The ICreateOneToOneMappingsForm implementation " + form.GetType() +
											" needs to extend Control in order to be used in EditModel2.");

			ShowModalForm(control);
		}

		private void ShowModalForm(Control control)
		{
			if (modelDialogForm != null)
			{
				modelDialogForm.Close();
				modelDialogForm = null;
			}

			modelDialogForm = new Form();
			modelDialogForm.ControlBox = false;
			modelDialogForm.Size = control.Size;
			modelDialogForm.Controls.Add(control);
			control.Dock = DockStyle.Fill;

			modelDialogForm.StartPosition = FormStartPosition.CenterParent;
			modelDialogForm.ShowDialog();
		}

		private void StartSearchTimer()
		{
			if (searchBarTimer.Enabled) return;

			searchBarTimer.Start();
		}

		private void FilterTablesInTreeList(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				MakeAllTableNodesVisible();
				MakeAllEntityNodesVisible();
				return;
			}
			advTree1.BeginUpdate();
			MakeAllTableNodesInvisible();
			MakeAllEntityNodesInvisible();
			List<Node> visibleNodes = new List<Node>();

			// Get each table node that fits the filter and make it visible
			foreach (var node in TableNodes)
			{
				if (node.Text.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0)
				{
					node.Visible = true;
					visibleNodes.Add(node);
				}
			}
			foreach (var node in EntityNodes)
			{
				if (node.Text.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0)
				{
					node.Visible = true;
					visibleNodes.Add(node);
				}
			}
			if (advTree1.SelectedNode == null || advTree1.SelectedNode.Visible == false)
			{
				// If there are multiple visible nodes or none, show nothing.
				if (visibleNodes.Count == 1)
				{
					visibleNodes[0].Parent.Expanded = true;
					advTree1.SelectedNode = visibleNodes[0];
				}
				//else
				//{
				//    schemaDiagrammerController.ShowNothing();
				//}
			}
			advTree1.EndUpdate();
		}

		private void MakeAllTableNodesVisible()
		{
			MakeAllTableNodesVisible(true);
		}

		private void MakeAllTableNodesInvisible()
		{
			MakeAllTableNodesVisible(false);
		}

		private void MakeAllEntityNodesVisible()
		{
			MakeAllEntityNodesVisible(true);
		}

		private void MakeAllEntityNodesInvisible()
		{
			MakeAllEntityNodesVisible(false);
		}

		private void SetChangesToNote(IEnumerable<ChangeToNote> changes)
		{
			foreach (var change in changes)
			{
				// TODO This is where I'm up to.
			}
		}

		private void CompareCurrentAndInitialDatabases()
		{
			var initialDatabase = string.IsNullOrEmpty(Provider.LoadedDatabaseXml) ?
				new Database(MappingSet.Database.Name, MappingSet.Database.DatabaseType) :
				new DatabaseDeserialisationScheme().Deserialise(Provider.LoadedDatabaseXml);

			var result = new DatabaseProcessor().MergeDatabases(initialDatabase, MappingSet.Database);
			if (result.AnyChanges)
			{
				var form = new FormDetailedDatabaseChanges();
				form.SetDatabaseChanges(result);
				form.ShowDialog(this);
			}
			else
			{
				MessageBox.Show("No changes have been made to your Database Schema", "No Changes Detected",
								MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}

		}

		private void MakeAllTableNodesVisible(bool visible)
		{
			foreach (var node in TableNodes)
				node.Visible = visible;
		}

		private void MakeAllEntityNodesVisible(bool visible)
		{
			foreach (var node in EntityNodes)
				node.Visible = visible;
		}

		public bool RunAllValidationRules()
		{
			var validationResults = Provider.Engine.RunAllRules();

			if (validationResults.HasAnyIssues)
			{
				ShowValidationResults(validationResults);
				return false;
			}
			else
			{
				HideValidationResults();
				return true;
			}
		}

		private void CreateEmptyEntitySet()
		{
			if (!CanCreateEmptyEntitySet())
				return;

			MappingSet.EntitySet = new EntitySetImpl();
			RefreshRibbonButtons();
			Populate();
		}

		private void CreateOneToOneDatabaseMapping()
		{
			if (!CanCreateOneToOneDatabaseMapping())
				return;

			if (MappingSet.Database.IsEmpty == false)
			{
				var result = MessageBox.Show("This will clear your existing Database Schema. Continue?",
											 "Wipe Database Schema and Regenerate?", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

				if (result == DialogResult.No) return;
			}

			// Process Validation Rules before running
			ValidationRulesEngine engine = new ValidationRulesEngine(MappingSet);
			engine.AddModule(new OneToOneDatabaseMappingModule());
			var validationResults = engine.RunAllRules();

			if (validationResults.HasAnyIssues)
			{
				ShowValidationResults(validationResults);

				// Continue if we only have warnings.
				if (validationResults.HasAnyErrorsOrWorse) return;
			}

			HideValidationResults();

			var entitySet = MappingSet.EntitySet;

			MappingProcessor proc = new MappingProcessor(new OneToOneEntityProcessor());

			var mapping = proc.CreateOneToOneMapping(entitySet);

			MappingSet = mapping;

			Provider.MappingSet = mapping;

			Populate();

			advTree1.SelectedNode = treeNodes[Provider.MappingSet.Database.InternalIdentifier].First();
		}

		private void HideValidationResults()
		{
			Utility.SuspendPainting(this);

			validationFailureView1.Visible = false;

			Utility.ResumePainting(this);
		}

		public void ShowValidationResults(ValidationResults results)
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(() => ShowValidationResults(results)));
				return;
			}
			validationFailureView1.ClearResultsView();
			validationFailureView1.SetValidationResults(results);
			validationFailureView1.Visible = true;
		}

		private void CreateOneToOneEntityMapping()
		{
			if (!CanCreateOneToOneEntityMapping())
				return;

			var db = MappingSet.Database;

			MappingProcessor proc = new MappingProcessor(new OneToOneEntityProcessor());

			var mapping = proc.CreateOneToOneMapping(db, MappingSet.TablePrefixes, MappingSet.ColumnPrefixes, MappingSet.TableSuffixes, MappingSet.ColumnSuffixes);

			MappingSet = mapping;

			Provider.MappingSet = mapping;

			Populate();

			advTree1.SelectedNode = treeNodes[Provider.MappingSet.EntitySet.InternalIdentifier].First();
		}

		private void AddNewEntity()
		{
			EntityImpl entity = new EntityImpl("New_Entity");
			MappingSet.EntitySet.AddEntity(entity);

			//schemaDiagrammerController.RefreshSchemaDiagrammer(MappingSet.EntitySet);
			//schemaDiagrammerController.ShowTag(entity);

			//presenterController.ShowPresenter(entity);

			diagrammer.Entity = entity;

			if (diagrammer.HasData && panelEx3.Controls.Count == 0)
				panelEx3.Controls.Add(diagrammer);
		}

		private void AddNewTable()
		{
			string name = "New_Table".GetNextName(MappingSet.Database.Tables.Select(t => t.Name));
			string schema = MappingSet.Database.Tables.Count > 0 ? MappingSet.Database.Tables[0].Schema : "";
			Table table = new Table(name, schema);
			MappingSet.Database.AddTable(table);

			//schemaDiagrammerController.RefreshSchemaDiagrammer(MappingSet.Database);
			//schemaDiagrammerController.ShowTag(table);

			//presenterController.ShowPresenter(table);

			diagrammer.Table = table;

			if (diagrammer.HasData && panelEx3.Controls.Count == 0)
				panelEx3.Controls.Add(diagrammer);
		}

		private void RemoveSelectedTable()
		{
			if (advTree1.SelectedNode == null || advTree1.SelectedNode.Tag is ITable == false)
				throw new InvalidOperationException("Cannot delete table - the currently selected node is null or does not represent a table");

			Node nextNode = GetNodeToFocusAfterCurrentNodeIsRemoved();

			((ITable)advTree1.SelectedNode.Tag).DeleteSelf();
			advTree1.SelectedNode = nextNode;
		}

		private Node GetNodeToFocusAfterCurrentNodeIsRemoved()
		{
			if (advTree1.SelectedNode.PrevNode != null)
				return advTree1.SelectedNode.PrevNode;
			else if (advTree1.SelectedNode.NextNode != null)
				return advTree1.SelectedNode.NextNode;
			else
				return advTree1.Nodes[0];
		}

		private void RemoveSelectedEntity()
		{
			if (advTree1.SelectedNode == null || advTree1.SelectedNode.Tag is Entity == false)
				throw new InvalidOperationException("Cannot delete entity - the currently selected node is null or does not represent an Entity");

			Node nextNode = GetNodeToFocusAfterCurrentNodeIsRemoved();
			((Entity)advTree1.SelectedNode.Tag).DeleteSelf();
			advTree1.SelectedNode = nextNode;
		}

		private void RemoveSelectedComponent()
		{
			if (advTree1.SelectedNode == null || advTree1.SelectedNode.Tag is ComponentSpecification == false)
				throw new InvalidOperationException("Cannot delete component - the currently selected node is null or does not represent a Component");

			Node nextNode = GetNodeToFocusAfterCurrentNodeIsRemoved();
			((ComponentSpecification)advTree1.SelectedNode.Tag).DeleteSelf();
			advTree1.SelectedNode = nextNode;
		}

		private bool CanCreateDatabase()
		{
			return MappingSet == null || MappingSet.Database == null;
		}

		private bool CanCreateOneToOneEntityMapping()
		{
			return MappingSet == null || MappingSet.EntitySet == null || MappingSet.EntitySet.IsEmpty;
		}

		private bool CanCreateOneToOneDatabaseMapping()
		{
			return MappingSet.EntitySet.IsEmpty == false;
		}

		private bool CanRemoveRelationship()
		{
			return currentlyDisplayedObject.Value is Relationship;
		}

		private bool CanAddRelationship()
		{
			return MappingSet.Database != null && MappingSet.Database.Tables.Count > 0;
		}

		private bool CanCreateEmptyEntitySet()
		{
			return MappingSet != null && MappingSet.Database != null && MappingSet.EntitySet == null;
		}

		private bool CanCreateTable()
		{
			return MappingSet != null && MappingSet.Database != null;
		}

		private bool CanRemoveTable()
		{
			return MappingSet != null && MappingSet.Database != null
				&& advTree1.SelectedNode != null &&
				advTree1.SelectedNode.Tag is ITable;
		}

		private bool CanCreateEntity()
		{
			return MappingSet != null && MappingSet.EntitySet != null;
		}

		private bool CanRemoveEntity()
		{
			return MappingSet != null && MappingSet.EntitySet != null
				&& advTree1.SelectedNode != null &&
				advTree1.SelectedNode.Tag is Entity;
		}

		private bool CanCreateComponentSpec()
		{
			return MappingSet != null && MappingSet.EntitySet != null;
		}

		private bool CanRemoveComponentSpec()
		{
			return MappingSet != null && MappingSet.EntitySet != null
				&& advTree1.SelectedNode != null &&
				advTree1.SelectedNode.Tag is ComponentSpecification;
		}

		public void AddRibbonBarButtons(RibbonBarBuilder builder)
		{
			RibbonBarController = builder.Controller;

			//SetupGeneralBar(builder);
			//SetupDatabaseBar(builder);
			//SetupEntityBar(builder);
			//SetupDiagramBar(builder);
		}

		private bool CanAddReference()
		{
			return MappingSet.EntitySet != null && MappingSet.EntitySet.Count > 1 &&
				advTree1.SelectedNode != null && advTree1.SelectedNode.Tag is Entity;
		}

		private bool CanRemoveReference()
		{
			return advTree1.SelectedNode != null && advTree1.SelectedNode.Tag is Reference;
		}

		//private bool CanLayoutDiagram()
		//{
		//    return mainWindow1.Controller.Shapes.Count() > 0;
		//}

		private void addReference_Click(object sender, EventArgs e)
		{
			AddNewReference();
		}

		private void AddNewReference()
		{
			if (CanAddReference() == false) return;

			ArchAngel.Providers.EntityModel.UI.Editors.EntityMappingDiagram.CreateNewReference((Entity)advTree1.SelectedNode.Tag);
			//var reference = new ReferenceImpl();
			//reference.Entity1 = reference.Entity2 = (Entity)advTree1.SelectedNode.Tag;
			//reference.End1Name = "PrimaryTable";
			//reference.End2Name = "ForeignTable";
			//MappingSet.EntitySet.AddReference(reference);
		}

		private void removeReference_Click(object sender, EventArgs e)
		{
			if (CanRemoveReference() == false) return;

			var reference = (Reference)advTree1.SelectedNode.Tag;

			// Call this first to avoid updating UI while deleting.
			//panelPropertyGrid.Controls.Clear();
			//presenterController.ClearCurrentPresenter();

			reference.DeleteSelf();
		}

		private void removeRelationship_Click(object sender, EventArgs e)
		{
			var relationship = currentlyDisplayedObject.Value as Relationship;
			if (relationship == null) return;

			//ShowPropertyGrid(null);

			relationship.DeleteSelf();
		}

		private void addNewRelationship_Click(object sender, EventArgs e)
		{
			AddNewRelationship();
		}

		private void AddNewRelationship()
		{
			FormCreateNewRelationship form = new FormCreateNewRelationship();
			var keys = MappingSet.Database.GetAllKeys().ToList();

			form.SetPossiblePrimaryKeys(keys.Where(k => k.Keytype == DatabaseKeyType.Primary));
			form.SetPossibleForeignKeys(keys.Where(k => k.Keytype == DatabaseKeyType.Foreign));
			var result = form.ShowDialog();

			if (result == DialogResult.OK)
			{
				var primaryKey = form.PrimaryKey;
				var foreignKey = form.ForeignKey;

				var relationship = primaryKey.Parent.CreateRelationshipUsing(primaryKey, foreignKey);
				relationship.Name = form.RelationshipName;
			}
		}

		//private void layoutDiagram_Click(object sender, EventArgs e)
		//{
		//    if (mainWindow1 != null)
		//        mainWindow1.Layout();
		//}

		private void showMappedTable_CheckedChanged(object sender, bool newCheckState)
		{
			//schemaDiagrammerController.ShowMappedTables = newCheckState;
		}

		private void addComponentSpec_Click(object sender, EventArgs e)
		{
			CreateNewComponentSpec();
		}

		private void CreateNewComponentSpec()
		{
			if (CanCreateComponentSpec() == false) return;

			var componentSpec = new ComponentSpecificationImpl("New_Component");
			MappingSet.EntitySet.AddComponentSpecification(componentSpec);

			//presenterController.ShowPresenter(componentSpec);
		}

		private void removeComponentSpec_Click(object sender, EventArgs e)
		{
			if (CanRemoveComponentSpec() == false) return;

			var selectedSpec = (ComponentSpecification)advTree1.SelectedNode.Tag;

			//panelPropertyGrid.Controls.Clear();
			//presenterController.ClearCurrentPresenter();

			selectedSpec.DeleteSelf();
		}

		private void addComponentToEntity_Click(object sender, EventArgs e)
		{
			AddComponentToEntity();
		}

		private void AddComponentToEntity()
		{
			if (CanAddComponentToEntity() == false) return;

			var selectedEntity = (Entity)advTree1.SelectedNode.Tag;

			var selectSpecForm = new FormSelectComponent(MappingSet.EntitySet.ComponentSpecifications);
			if (selectSpecForm.ShowDialog(this) == DialogResult.OK)
			{
				var selectedSpec = selectSpecForm.SelectedComponentSpec;
				if (selectedSpec == null) return;

				selectedSpec.CreateImplementedComponentFor(selectedEntity, selectedSpec.Name + "_New");
			}
		}

		private bool CanAddComponentToEntity()
		{
			return MappingSet != null && MappingSet.EntitySet != null
				&& advTree1.SelectedNode != null &&
				advTree1.SelectedNode.Tag is Entity;
		}

		private void SetupEntityBar(RibbonBarBuilder builder)
		{
			RibbonBar bar = builder.CreateBar().SetName("Entity Layer").SetOrientation(Orientation.Horizontal);

			var newEntityButton = builder.CreateButton()
				.SetText("Add New Blank Entity").AddClickEventHandler(addNewEntity_Click)
				.SetIsEnabledHandler(CanCreateEntity)
				.SetToolTip("Adds a new unmapped Entity")
				.SetImage(GetImage(@"ArchAngel.Providers.EntityModel.Resources.add_entity_32.png"))
				.SetDisplayType(ButtonDisplayType.TextAndImage)
				.AddDropDownItem(builder.CreateDropDownItem()
					.SetText("Add New Entities From Table")
					.SetToolTip("Creates new Entities from your choice of Tables")
					.AddEventHandler(createNewEntitiesFromTables_Click)
				);

			var removeEntityButton = builder.CreateButton()
				.SetText("Remove Entity").AddClickEventHandler(removeEntity_Click)
				.SetToolTip("Remove selected Entity from the Entity Model")
				.SetImage(GetImage(@"ArchAngel.Providers.EntityModel.Resources.RemoveEntity_32.png"))
				.SetDisplayType(ButtonDisplayType.TextAndImage)
				.SetIsEnabledHandler(CanRemoveEntity);

			var addReferenceButton = builder.CreateButton()
				.SetText("Add Reference").AddClickEventHandler(addReference_Click)
				.SetToolTip("Add a Reference to the selected Entity")
				.SetImage(GetImage(@"ArchAngel.Providers.EntityModel.Resources.AddReference_32.png"))
				.SetDisplayType(ButtonDisplayType.TextAndImage)
				.SetIsEnabledHandler(CanAddReference);

			var removeReferenceButton = builder.CreateButton()
				.SetText("Remove Reference").AddClickEventHandler(removeReference_Click)
				.SetToolTip("Remove the selected Entity from the Entity Model")
				.SetImage(GetImage(@"ArchAngel.Providers.EntityModel.Resources.Remove_Reference_32.png"))
				.SetDisplayType(ButtonDisplayType.TextAndImage)
				.SetIsEnabledHandler(CanRemoveReference);

			var newComponentSpecButton = builder.CreateButton()
				.SetText("Add New Component").AddClickEventHandler(addComponentSpec_Click)
				.SetToolTip("Adds a new Component Definition type to the Entity Model")
				.SetImage(GetImage(@"ArchAngel.Providers.EntityModel.Resources.Add_Component_32.png"))
				.SetDisplayType(ButtonDisplayType.TextAndImage)
				.SetIsEnabledHandler(CanCreateComponentSpec);

			var removeComponentSpecButton = builder.CreateButton()
				.SetText("Remove Component").AddClickEventHandler(removeComponentSpec_Click)
				.SetToolTip("Removes the selected Component Definition from the Entity Model")
				.SetImage(GetImage(@"ArchAngel.Providers.EntityModel.Resources.Remove_Component_32.png"))
				.SetDisplayType(ButtonDisplayType.TextAndImage)
				.SetIsEnabledHandler(CanRemoveComponentSpec);

			var addComponentToEntityButton = builder.CreateButton()
				.SetText("Add Component To Entity").AddClickEventHandler(addComponentToEntity_Click)
				.SetToolTip("Adds an instance of a Component Definition to the selected Entity")
				.SetImage(GetImage(@"ArchAngel.Providers.EntityModel.Resources.insert_32_h.png"))
				.SetDisplayType(ButtonDisplayType.TextAndImage)
				.SetIsEnabledHandler(CanAddComponentToEntity);

			bar.AddButton(newEntityButton)
				.AddButton(removeEntityButton)
				.AddButton(addReferenceButton)
				.AddButton(removeReferenceButton)
				.AddButton(newComponentSpecButton)
				.AddButton(removeComponentSpecButton)
				.AddButton(addComponentToEntityButton);

			builder.AddRibbonBar(bar);
		}

		//private void SetupDiagramBar(RibbonBarBuilder builder)
		//{
		//    RibbonBar bar = builder.CreateBar().SetName("Diagram").SetOrientation(Orientation.Horizontal);
		//    var layoutButton = builder.CreateButton()
		//        .SetText("Layout Diagram").AddClickEventHandler(layoutDiagram_Click)
		//        .SetIsEnabledHandler(CanLayoutDiagram)
		//        .SetToolTip("Resets the diagram layout")
		//        .SetImage(GetImage(@"ArchAngel.Providers.EntityModel.Resources.flow_chart_32_h.png"))
		//        .SetDisplayType(ButtonDisplayType.TextAndImage);
		//    var showMappedTableButton = builder.CreateButton()
		//        .SetText("Show Mapped Tables")
		//        .SetToolTip("Shows the tables an Entity is mapped to when a single Entity is displayed in the diagram")
		//        .AddCheckEventHandler(showMappedTable_CheckedChanged)
		//        .SetIsCheckable(true)
		//        .SetIsChecked(true)
		//        //.SetToolTip("If checked, the diagram will show the mapped tables for the selected entity.")
		//        .SetImage(GetImage(@"ArchAngel.Providers.EntityModel.Resources.import_32.png"))
		//        .SetDisplayType(ButtonDisplayType.TextAndImage);

		//    //schemaDiagrammerController.ShowMappedTables = showMappedTableButton.CheckedState;

		//    bar.AddButton(layoutButton);
		//    bar.AddButton(showMappedTableButton);
		//    builder.AddRibbonBar(bar);
		//}

		private void SetupDatabaseBar(RibbonBarBuilder builder)
		{
			RibbonBar bar = builder.CreateBar().SetName("Database Layer").SetOrientation(Orientation.Horizontal);
			var newTableButton = builder.CreateButton()
				.SetText("Add New Table")
				.SetToolTip("Adds a new Table to the Database Model")
				.AddClickEventHandler(addNewTable_Click)
				.SetImage(GetImage(@"ArchAngel.Providers.EntityModel.Resources.Add_Table_32.png"))
				.SetDisplayType(ButtonDisplayType.TextAndImage)
				.SetIsEnabledHandler(CanCreateTable);
			var removeTableButton = builder.CreateButton()
				.SetText("Remove Table")
				.SetToolTip("Removes the selected Table from the Database Model")
				.AddClickEventHandler(removeTable_Click)
				.SetImage(GetImage(@"ArchAngel.Providers.EntityModel.Resources.Remove_Table_32.png"))
				.SetDisplayType(ButtonDisplayType.TextAndImage)
				.SetIsEnabledHandler(CanRemoveTable);
			var addRelationshipButton = builder.CreateButton()
				.SetText("Add New Relationship")
				.SetToolTip("Create a new Relationship between two Tables")
				.AddClickEventHandler(addNewRelationship_Click)
				.SetImage(GetImage(@"ArchAngel.Providers.EntityModel.Resources.Add_Relationship_32.png"))
				.SetDisplayType(ButtonDisplayType.TextAndImage)
				.SetIsEnabledHandler(CanAddRelationship);
			var removeRelationshipButton = builder.CreateButton()
				.SetText("Remove Relationship")
				.SetToolTip("Removes the selected Relationship from the Database Model")
				.AddClickEventHandler(removeRelationship_Click)
				.SetImage(GetImage(@"ArchAngel.Providers.EntityModel.Resources.Remove_Relationship_32.png"))
				.SetDisplayType(ButtonDisplayType.TextAndImage)
				.SetIsEnabledHandler(CanRemoveRelationship);

			bar.AddButton(newTableButton);
			bar.AddButton(removeTableButton);
			bar.AddButton(addRelationshipButton);
			bar.AddButton(removeRelationshipButton);
			builder.AddRibbonBar(bar);
		}

		private void SetupGeneralBar(RibbonBarBuilder builder)
		{
			RibbonBar bar = builder.CreateBar().SetName("General").SetOrientation(Orientation.Horizontal);

			//var clearModelButton = builder.CreateButton()
			//    .SetText("Clear Model")
			//    .SetToolTip("Clears both the Entity Model and the Database Model")
			//    .AddClickEventHandler(buttonClearModel_Click)
			//    .SetImage(GetImage(@"ArchAngel.Providers.EntityModel.Resources.reset.png"));

			//var runAllValidationRulesButton = builder.CreateButton()
			//    .SetText("Check Model")
			//    .SetToolTip("Run all model validation rules and display the results")
			//    .SetImage(GetImage(@"ArchAngel.Providers.EntityModel.Resources.tick_32.png"))
			//    .SetDisplayType(ButtonDisplayType.TextAndImage)
			//    .AddClickEventHandler(runAllValidationRules_Click);

			oneToOneEntityMappingButton = builder.CreateButton()
				.SetText("Generate Entity Model")
				.SetToolTip("Create One Entity for each Table")
				.SetImage(GetImage(@"ArchAngel.Providers.EntityModel.Resources.OneToOne_32.png"))
				.SetDisplayType(ButtonDisplayType.TextAndImage)
				.SetIsEnabledHandler(CanCreateOneToOneEntityMapping)
				.AddClickEventHandler(createOneToOneEntityMappingToolStripMenuItem_Click);

			//var oneToOneDatabaseMappingButton = builder.CreateButton()
			//    .SetText("Generate Database")
			//    .SetToolTip("Create Database Schema for your Entity Model.")
			//    .SetImage(GetImage(@"ArchAngel.Providers.EntityModel.Resources.transfer_data_32.png"))
			//    .SetDisplayType(ButtonDisplayType.TextAndImage)
			//    .SetIsEnabledHandler(CanCreateOneToOneDatabaseMapping)
			//    .AddClickEventHandler(createOneToOneDatabaseMappingToolStripMenuItem_Click);

			var emptyEntitySetButton = builder.CreateButton()
				.SetText("Create Empty EntitySet")
				.SetToolTip("Create Empty EntitySet")
				//.SetImage(GetImage(@"ArchAngel.Providers.EntityModel.Resources.AddDB_32.png"))
				.SetDisplayType(ButtonDisplayType.TextAndImage)
				.SetIsEnabledHandler(CanCreateEmptyEntitySet)
				.AddClickEventHandler(createEmptyEntitySetToolStripMenuItem_Click);

			var addDatabaseButton = builder.CreateButton()
				.SetText("Set Database")
				.SetToolTip("Opens the Database Details Form")
				.SetImage(GetImage(@"ArchAngel.Providers.EntityModel.Resources.AddDB_32.png"))
				.SetIsEnabledHandler(CanCreateDatabase)
				.AddClickEventHandler(AddDatabaseButton_Click);

			var showDatabaseChangesButton = builder.CreateButton()
				.SetText("Show DB Changes")
				.SetToolTip("Shows the changes made to the Database Schema since the project was loaded.")
				.SetDisplayType(ButtonDisplayType.Text)
				.AddClickEventHandler(ShowDatabaseChanges_Click);

			bar//.AddButton(clearModelButton)
				//.AddButton(runAllValidationRulesButton)
				.AddButton(oneToOneEntityMappingButton)
				//.AddButton(oneToOneDatabaseMappingButton)
				.AddButton(emptyEntitySetButton)
				.AddButton(addDatabaseButton)
				.AddButton(showDatabaseChangesButton);

			builder.AddRibbonBar(bar);
		}

		private Image GetImage(string resourceName)
		{
			return Image.FromStream(GetResourceStream(resourceName));
		}

		private Stream GetResourceStream(string resourceName)
		{
			return System.Reflection.Assembly.GetAssembly(typeof(EditModel)).GetManifestResourceStream(resourceName);
		}

		#region Event Handlers

		private void ShowDatabaseChanges_Click(object sender, EventArgs e)
		{
			CompareCurrentAndInitialDatabases();
		}

		private void AddDatabaseButton_Click(object sender, EventArgs e)
		{
			//presenterController.ShowPresenter(MappingSet.Database);
		}

		private void addNewEntity_Click(object sender, EventArgs e)
		{
			AddNewEntity();
		}

		private void createNewEntitiesFromTables_Click(object sender, EventArgs e)
		{
			ShowCreateOneToOneMappingForm();
		}

		private void removeEntity_Click(object sender, EventArgs e)
		{
			RemoveSelectedEntity();
		}

		private void addNewTable_Click(object sender, EventArgs e)
		{
			AddNewTable();
		}

		private void removeTable_Click(object sender, EventArgs e)
		{
			RemoveSelectedTable();
		}

		//private void buttonClearModel_Click(object sender, EventArgs e)
		//{
		//    var result = MessageBox.Show("This will clear your database and entities. Do you really want to do this?",
		//                                 "Clear Entire Model?", MessageBoxButton.YesNo, MessageBoxImage.Warning,
		//                                 MessageBoxResult.No);

		//    if (result == MessageBoxResult.No) return;

		//    //presenterController.ClearCurrentPresenter();


		//    if (Provider != null)
		//    {
		//        Provider.Clear();
		//        Populate();
		//    }
		//    else
		//    {
		//        MappingSet = null;
		//        Populate();
		//    }
		//}

		private void btnClearSearchBox_Click(object sender, EventArgs e)
		{
			tbddSearchBar.Text = "";
			textBoxSearch.Clear();
		}

		private void SearchBarTimerTick(object sender, EventArgs e)
		{
			searchBarTimer.Stop();

			string text = tbddSearchBar.Text;

			ComboBox.ObjectCollection items = tbddSearchBar.Items;

			if (items.Contains(text))
			{
				items.Remove(text);
			}

			items.Insert(0, text);

			while (items.Count > 10)
				items.RemoveAt(10);
		}

		private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
		{
			if (advTree1.SelectedNode == null || advTree1.SelectedNode.Tag is IDatabase == false)
			{
				e.Cancel = true;
				return;
			}
		}

		private void createOneToOneEntityMappingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CreateOneToOneEntityMapping();
		}

		private void createOneToOneDatabaseMappingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CreateOneToOneDatabaseMapping();
		}

		private void createEmptyEntitySetToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CreateEmptyEntitySet();
		}

		private void runAllValidationRules_Click(object sender, EventArgs e)
		{
			RunAllValidationRules();
		}

		private void advTree1_AfterNodeSelect(object sender, AdvTreeNodeEventArgs e)
		{
			if (advTree1.SelectedNode == null || IgnoreChangedNode)
				return;

			object tag = advTree1.SelectedNode.Tag;

			Cursor = Cursors.WaitCursor;

			if (tag is PresenterBase)
			{
				//presenterController.ShowPresenter((PresenterBase)tag);
			}
			else
			{
				//if (tag is EntitySet && ((EntitySet)tag).Entities.Count > 20)
				//{
				//    EntitySet entitySet = (EntitySet)tag;

				//    if (MessageBox.Show(string.Format("This diagram is very big and will be slow ({0} entities). Do you want to display it?", entitySet.Entities.Count), "Large Diagram", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
				//    {
				//        SetNewlyDisplayedObject(entitySet.Entities[0] as IModelObject);
				//        presenterController.ShowPresenter(entitySet.Entities[0] as IModelObject);
				//        schemaDiagrammerController.ShowTag(entitySet.Entities[0] as IModelObject);
				//        return;
				//    }
				//}
				SetNewlyDisplayedObject(tag as IModelObject);
				//presenterController.ShowPresenter(tag as IModelObject);
				//schemaDiagrammerController.ShowTag(tag as IModelObject);

				if (tag is EntityImpl)
				{
					diagrammer.Entity = (EntityImpl)tag;

					if (!panelEx3.Controls.Contains(diagrammer))
					{
						panelEx3.Controls.Clear();
						panelEx3.Controls.Add(diagrammer);
					}
				}
				else if (tag is Table)
				{
					diagrammer.Table = (Table)tag;

					if (!panelEx3.Controls.Contains(diagrammer))
					{
						panelEx3.Controls.Clear();
						panelEx3.Controls.Add(diagrammer);
					}
				}
				else if (tag is ComponentSpecification)
				{
					diagrammer.ComponentSpecification = (ComponentSpecification)tag;

					if (!panelEx3.Controls.Contains(diagrammer))
					{
						panelEx3.Controls.Clear();
						panelEx3.Controls.Add(diagrammer);
					}
				}
				else if (tag is Database)
				{
					ShowDatabaseForm((Database)tag);
				}
				//else if (tag is EntitySet)
				//{
				//    EntitiesForm.FillData(MappingSet.EntitySet);

				//    if (!panelEx3.Controls.Contains(EntitiesForm))
				//    {
				//        panelEx3.Controls.Clear();
				//        panelEx3.Controls.Add(EntitiesForm);
				//    }
				//}
			}
			RefreshRibbonButtons();
			Cursor = Cursors.Default;
		}

		private void ShowDatabaseForm(Database db)
		{
			FormDatabase form = new FormDatabase(db, MappingSet);
			form.Database = db;
			form.MaxWidth = 500;
			form.DatabaseSchemaChanged += new FormDatabase.DatabaseSchemaChangedDelegate(form_DatabaseSchemaChanged);
			form.NewDatabaseCreated += new FormDatabase.DatabaseCreatedDelegate(form_NewDatabaseCreated);
			form.Dock = DockStyle.Fill;

			Slyce.Common.Utility.SuspendPainting(panelEx3);
			panelEx3.Controls.Clear();
			panelEx3.Controls.Add(form);
			Slyce.Common.Utility.ResumePainting(panelEx3);
		}

		private void form_NewDatabaseCreated(MappingSet mappingSet)
		{
			SetMappingSet(mappingSet);
		}

		void form_DatabaseSchemaChanged(DatabaseMergeResult mergeResult, IDatabase db1, IDatabase db2)
		{
			ShowDatabaseRefreshResults(mergeResult, db1, db2);
		}

		private void RefreshRibbonButtons()
		{
			if (RibbonBarController != null)
				RibbonBarController.RefreshButtonStatus(this);
		}

		private void tbddSearchBar_TextChanged(object sender, EventArgs e)
		{
			StartSearchTimer();

			FilterTablesInTreeList(tbddSearchBar.Text);
		}

		private void obj_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			IModelObject obj = sender as IModelObject;
			if (obj == null) return;
			if (treeNodes.ContainsKey(obj.InternalIdentifier) == false) return;

			List<Node> nodeList = treeNodes[obj.InternalIdentifier];

			if (e.PropertyName == "Name")
				foreach (Node node in nodeList)
					SetNodeName(node, sender);
			else if (e.PropertyName == "Properties")
				SyncCurrentlySelectedObject(diagrammer.Entity);
			else if (e.PropertyName == "Property")
				SyncCurrentlySelectedObject(diagrammer.Entity);
			else if (e.PropertyName == "Column")
				SyncCurrentlySelectedObject(diagrammer.Table);
			else if (sender is Mapping && (e.PropertyName == "FromTable" || e.PropertyName == "Entity"))
			{
				foreach (Node node in nodeList)
					SetNodeName(node, sender);
			}
			else if (sender is MappingSet)
			{
				// Refresh the entire tree - something has structurally changed.
				RefreshTree();
			}
			else if (sender is IItemContainer && sender is IColumnContainer == false
				&& sender is ITableContainer == false
				&& sender is IViewContainer == false
				&& IsContainerName(e.PropertyName))
			{
				// Refresh the entire tree - something has structurally changed.
				RefreshTree();
			}
			else if (sender is Reference && (e.PropertyName == "End1Enabled" || e.PropertyName == "End2Enabled"))
			{
				foreach (Node node in nodeList)
					SetNodeName(node, sender);
			}
			else if (sender is Reference && (e.PropertyName == "Entity1" || e.PropertyName == "Entity2"))
			{
				// Structural change. Need to remove existing nodes and add new ones.
				ProcessReferenceChanges(sender as Reference);
			}
		}

		private void ProcessReferenceChanges(Reference reference)
		{
			IgnoreChangedNode = true;
			try
			{
				// First, find any existing nodes and remove them.
				ReferenceNodeSet nodeSet;
				bool entity1Selected = false;
				bool entity2Selected = false;
				if (referenceNodes.TryGetValue(reference.InternalIdentifier, out nodeSet))
				{
					if (nodeSet.Entity1Node != null)
					{
						nodeSet.Entity1Node.Parent.Nodes.Remove(nodeSet.Entity1Node);
					}
					if (nodeSet.Entity2Node != null)
					{
						nodeSet.Entity2Node.Parent.Nodes.Remove(nodeSet.Entity2Node);
					}

					if (advTree1.SelectedNode == nodeSet.Entity1Node) entity1Selected = true;
					if (advTree1.SelectedNode == nodeSet.Entity2Node) entity2Selected = true;

					treeNodes.Remove(reference.InternalIdentifier);
					referenceNodes.Remove(reference.InternalIdentifier);
				}

				if (reference.Entity1 == null && reference.Entity2 == null) return;

				// Add the nodes back in.
				if (reference.Entity1 != null)
				{
					var entity1Nodes = treeNodes[reference.Entity1.InternalIdentifier];
					foreach (var entity1Node in entity1Nodes)
					{
						var referencesNode = FindFirstNodeNamed("References", entity1Node.Nodes);
						//AddReferenceToTree(new DirectedReference(reference.Entity1, reference), referencesNode);
					}
				}

				if (reference.Entity1 != reference.Entity2 && reference.Entity2 != null)
				{
					// Only add a second node if this is not a self reference.
					var entity2Nodes = treeNodes[reference.Entity2.InternalIdentifier];
					foreach (var entity2Node in entity2Nodes)
					{
						var referencesNode = FindFirstNodeNamed("References", entity2Node.Nodes);
						//AddReferenceToTree(new DirectedReference(reference.Entity2, reference), referencesNode);
					}
				}

				var newNodeSet = referenceNodes[reference.InternalIdentifier];

				if (entity1Selected && newNodeSet.Entity1Node != null)
				{
					advTree1.SelectedNode = newNodeSet.Entity1Node;
				}
				else if (entity2Selected && newNodeSet.Entity2Node != null)
				{
					advTree1.SelectedNode = newNodeSet.Entity2Node;
				}
				else
				{
					advTree1.SelectedNode = newNodeSet.Entity1Node ?? newNodeSet.Entity2Node ?? advTree1.SelectedNode;
				}
			}
			finally
			{
				IgnoreChangedNode = false;
			}

		}

		void db_TablesChanged(object sender, CollectionChangeEvent<ITable> e)
		{
			ITableContainer db = sender as ITableContainer;
			if (db == null) return;

			CollectionChangedHandler(db, e, "Tables", AddTableToTree, e.ChangedObject.Name);
		}

		void table_ColumnsChanged(object sender, CollectionChangeEvent<IColumn> e)
		{
			IColumnContainer table = sender as IColumnContainer;
			if (table == null) return;

			CollectionChangedHandler(table, e, "Columns", AddColumnToTree, e.ChangedObject.Name);
		}

		void entities_ComponentSpecsChanged(object sender, CollectionChangeEvent<ComponentSpecification> e)
		{
			EntitySet set = sender as EntitySet;
			if (set == null) return;

			//componentsNode
			//CreateCollectioNodeIfMissing(set, "Components");
			//CollectionChangedHandler(set, e, "Components", AddComponentSpecToTree, e.ChangedObject.Name);

			switch (e.ChangeType)
			{
				case CollectionChangeAction.Addition:
					AddComponentSpecToTree(e.ChangedObject, ComponentsNode);
					break;
				case CollectionChangeAction.Deletion:
					Node removedNode = FindFirstNodeNamed(e.ChangedObject.Name, ComponentsNode.Nodes);
					if (removedNode != null) removedNode.Remove();
					break;
				default:
					throw new Exception("Collection Change Type not handled");
			}
		}

		private void CreateCollectioNodeIfMissing(IModelObject modelObject, string collectionNodeName)
		{
			var nodeList = treeNodes[modelObject.InternalIdentifier];
			foreach (var node in nodeList)
			{
				var propertiesNode = FindFirstNodeNamed(collectionNodeName, node.Nodes);
				if (propertiesNode != null) continue;

				propertiesNode = new Node
									{
										Name = collectionNodeName,
										Text = collectionNodeName,
									};
				node.Nodes.Add(propertiesNode);
			}
		}

		void entities_EntitiesChanged(object sender, CollectionChangeEvent<Entity> e)
		{
			EntitySet entitySet = sender as EntitySet;
			if (entitySet == null) return;

			CollectionChangedHandler_No_Child(entitySet, e, AddEntityToTree, e.ChangedObject.Name);
		}

		void spec_ImplementedComponentsChanged(object sender, CollectionChangeEvent<Component> e)
		{
			ComponentSpecification spec = sender as ComponentSpecification;
			if (spec == null) return;

			CollectionChangedHandler_No_Child(spec, e, AddComponentToTreeUnderSpecification, e.ChangedObject.ParentEntity.Name);
		}

		void spec_PropertiesChanged(object sender, CollectionChangeEvent<ComponentProperty> e)
		{
			ComponentSpecification spec = sender as ComponentSpecification;
			if (spec == null) return;

			CollectionChangedHandler(spec, e, "Properties", AddComponentPropertyToTree, e.ChangedObject.Name);
		}

		void entities_ReferencesChanged(object sender, CollectionChangeEvent<Reference> e)
		{
			EntitySet entitySet = sender as EntitySet;
			if (entitySet == null) return;

			Reference reference = e.ChangedObject;

			advTree1.BeginUpdate();

			ReferenceNodeSet nodeSet;
			if (referenceNodes.TryGetValue(reference.InternalIdentifier, out nodeSet) == false)
			{
				nodeSet = new ReferenceNodeSet();
				referenceNodes.Add(reference.InternalIdentifier, nodeSet);
			}

			//if (reference.Entity1 != null)
			//    ReferenceCollectionChanged(e, reference, reference.Entity1);
			//if (reference.Entity2 != null && reference.Entity1 != reference.Entity2)
			//    ReferenceCollectionChanged(e, reference, reference.Entity2);

			advTree1.EndUpdate();
		}

		//private void ReferenceCollectionChanged(CollectionChangeEvent<Reference> e, Reference reference, Entity entity)
		//{
		//    var entityNodes = treeNodes[entity.InternalIdentifier];
		//    foreach (var entityNode in entityNodes)
		//    {
		//        var node = FindFirstNodeNamed("References", entityNode.Nodes);
		//        switch (e.ChangeType)
		//        {
		//            case CollectionChangeAction.Addition:
		//                AddReferenceToTree(new DirectedReference(entity, reference), node);
		//                break;
		//            case CollectionChangeAction.Deletion:
		//                Node propertyNode = FindFirstNodeNamed(reference.InternalIdentifier.ToString(), node.Nodes);
		//                if (propertyNode != null) propertyNode.Remove();
		//                break;
		//            default:
		//                throw new Exception("Collection Change Type not handled");
		//        }
		//    }
		//}

		//void entity_PropertiesChanged(object sender, CollectionChangeEvent<Property> e)
		//{
		//    Entity entity = sender as Entity;
		//    if (entity == null) return;

		//    CollectionChangedHandler(entity, e, "Properties", AddPropertyToTree, e.ChangedObject.Name);
		//}

		//private void entity_ComponentsChanged(object sender, CollectionChangeEvent<Component> e)
		//{
		//    Entity entity = sender as Entity;
		//    if (entity == null) return;

		//    CollectionChangedHandler(entity, e, "Components", AddComponentToTreeUnderEntity, e.ChangedObject.Name);
		//}

		#endregion

		private class ReferenceNodeSet
		{
			public Node Entity1Node;
			public Node Entity2Node;
		}

		public void SetFocusToFirstEntityNode()
		{
			//if (MappingSet.EntitySet.Entities.Count > 20)
			//{
			//    if (MessageBox.Show(string.Format("This diagram is very big and will be slow ({0} entities). Do you want to display it?", MappingSet.EntitySet.Entities.Count), "Large Diagram", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
			//    {
			//        advTree1.SelectedNode = advTree1.Nodes.Find(EntitySetKey, false).FirstOrDefault().Nodes[0];
			//        return;
			//    }
			//}
			Node entitySetNode = advTree1.Nodes.Find(EntitySetKey, false).FirstOrDefault();

			if (entitySetNode == null)
				return;

			if (entitySetNode.Nodes.Count > 0)
				advTree1.SelectedNode = entitySetNode.Nodes[0];
			else
			{
				Node tablesNode = advTree1.Nodes.Find(TablesKey, false).FirstOrDefault();

				if (tablesNode != null && tablesNode.Nodes.Count > 0)
					advTree1.SelectedNode = tablesNode.Nodes[0];
				else
					advTree1.SelectedNode = entitySetNode;
			}
		}

		private void buttonBack_Click(object sender, EventArgs e)
		{
			DisplayPreviouslyDisplayedObject();
		}

		private void buttonNext_Click(object sender, EventArgs e)
		{
			DisplayNextDisplayedObject();
		}

		public void ShowObjectWithPrimaryFocus(IModelObject entity)
		{
			ShowObjectPropertyGrid(entity);
			SyncCurrentlySelectedObject(entity);
			//schemaDiagrammerController.ShowTag(entity);
		}

		public void ShowError(string shortDescription, string longDescription)
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(() => ShowError(shortDescription, longDescription)));
				return;
			}

			MessageBox.Show(longDescription, shortDescription, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private bool waitScreenShown;
		public void ShowWaitScreen(string caption)
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(() => ShowWaitScreen(caption)));
				return;
			}

			Utility.DisplayMessagePanel(this, caption, Slyce.Common.Controls.MessagePanel.ImageType.Hourglass);
			//Utility.DisplayMessagePanel(panelEx3, "", Slyce.Common.Controls.MessagePanel.ImageType.None);
			diagrammer.Visible = false;
			waitScreenShown = true;
			this.Refresh();
			panelEx3.Refresh();
		}

		public void UpdateWaitScreen(string message)
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(() => UpdateWaitScreen(message)));
				return;
			}

			if (waitScreenShown)
			{
				Utility.UpdateMessagePanelStatus(this, message);
				Utility.UpdateMessagePanelStatus(panelEx3, "");
			}
			else
			{
				ShowWaitScreen(message);
			}
			this.Refresh();
			panelEx3.Refresh();
		}

		public IDatabaseLoader GetDatabaseLoader(IDatabaseConnector existingInformation)
		{
			if (InvokeRequired)
			{
				IDatabaseLoader loader = null;
				Invoke(new MethodInvoker(() => { loader = GetDatabaseLoader(existingInformation); }));
				return loader;
			}

			LoadDatabaseForm form = new LoadDatabaseForm();
			form.MessageText = "Cannot connect to your database. Please update the database configuration below.";

			if (existingInformation != null)
				form.FillFrom(existingInformation);
			ShowDialog(form);

			if (form.DialogResult == DialogResult.Cancel)
				return null;

			return form.DatabaseLoader;
		}

		public void RemoveWaitScreen()
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(RemoveWaitScreen));
				return;
			}

			Utility.HideMessagePanel(this);
			Utility.HideMessagePanel(panelEx3);
			diagrammer.Visible = true;
			waitScreenShown = false;
			this.Refresh();
			panelEx3.Refresh();
		}

		public void ShowDialog(Form form)
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(() => ShowDialog(form)));
			}

			form.ShowDialog(this);
		}

		private void create11EntityMappingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CreateOneToOneEntityMapping();
		}

		private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			foreach (ToolStripItem item in contextMenuStrip.Items)
				item.Visible = false;

			if (contextMenuStrip.SourceControl == advTree1)
			{
				if (advTree1.SelectedNode == null)
				{
					e.Cancel = true;
					return;
				}
				if (advTree1.SelectedNode.Tag == null)
				{
					if (advTree1.SelectedNode.Name == "Components")
					{
						contextMenuNewComponent.Visible = true;
					}
					else if (advTree1.SelectedNode.Text == "Tables")
					{
						contextMenuNewTable.Visible = true;
						//contextMenuCreate1to1EntityMappings.Visible = true;
						contextMenuNewEntitiesFromManyTables.Visible = true;

						//contextMenuCreate1to1EntityMappings.Enabled = CanCreateOneToOneEntityMapping();
					}
					else
						e.Cancel = true;
				}
				else if (advTree1.SelectedNode.Tag is EntityImpl)
				{
					contextMenuDelete.Visible = true;
					contextMenuAddReference.Visible = true;
					contextMenuAddComponentToEntity.Visible = true;
				}
				else if (advTree1.SelectedNode.Tag is Table)
				{
					contextMenuDelete.Visible = true;
					contextMenuNewRelationship.Visible = true;
					contextMenuNewEntityFromTable.Visible = true;
				}
				else if (advTree1.SelectedNode.Tag is EntitySetImpl)
				{
					contextMenuNewBlankEntity.Visible = true;
					contextMenuNewEntitiesFromManyTables.Visible = true;
					contextMenuNewDatabaseFromEntities.Visible = true;
					contextMenuCheckModel.Visible = true;
				}
				else if (advTree1.SelectedNode.Tag is Database)
				{
					//contextMenuCreate1to1EntityMappings.Visible = true;
					contextMenuNewEntitiesFromManyTables.Visible = true;
					contextMenuNewTable.Visible = true;
					//contextMenuShowDBChanges.Visible = true;
				}
				else if (advTree1.SelectedNode.Tag is ComponentSpecification)
				{
					contextMenuDelete.Visible = true;
				}
				else
					e.Cancel = true;
			}
		}

		private void contextMenuNewBlankEntity_Click(object sender, EventArgs e)
		{
			AddNewEntity();
		}

		private void contextMenuDelete_Click(object sender, EventArgs e)
		{
			try
			{
				Cursor = Cursors.WaitCursor;

				if (advTree1.SelectedNode.Tag is EntityImpl)
				{
					if (MessageBox.Show(string.Format("Delete this entity [{0}]?", ((EntityImpl)advTree1.SelectedNode.Tag).Name), "Delete entity", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						UpdateWaitScreen("Deleting entity...");
						RemoveSelectedEntity();
					}
				}
				else if (advTree1.SelectedNode.Tag is Table)
				{
					if (MessageBox.Show(string.Format("Delete this table [{0}]?", ((Table)advTree1.SelectedNode.Tag).Name), "Delete table", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						UpdateWaitScreen("Deleting table...");
						RemoveSelectedTable();
					}
				}
				else if (advTree1.SelectedNode.Tag is ComponentSpecificationImpl)
				{
					if (MessageBox.Show(string.Format("Delete this component [{0}]?", ((ComponentSpecificationImpl)advTree1.SelectedNode.Tag).Name), "Delete component", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						UpdateWaitScreen("Deleting component...");
						RemoveSelectedComponent();
					}
				}
				else
					throw new NotImplementedException("Not handled yet: " + advTree1.SelectedNode.Tag.GetType().Name);
			}
			finally
			{
				RemoveWaitScreen();
				Utility.HideMessagePanel(panelEx3);
				diagrammer.Visible = true;
				Cursor = Cursors.Default;
			}
		}

		private void contextMenuNewTable_Click(object sender, EventArgs e)
		{
			try
			{
				UpdateWaitScreen("Adding new table...");
				AddNewTable();
			}
			finally
			{
				RemoveWaitScreen();
			}
		}

		private void contextMenuNewEntitiesFromManyTables_Click(object sender, EventArgs e)
		{
			ShowCreateOneToOneMappingForm();
		}

		private void contextMenuNewRelationship_Click(object sender, EventArgs e)
		{
			AddNewRelationship();
		}

		private void contextMenuNewEntityFromTable_Click(object sender, EventArgs e)
		{
			Table selectedTable = (Table)advTree1.SelectedNode.Tag;
			CreateNewEntityFromTable(selectedTable);
		}

		private Entity CreateNewEntityFromTable(Table table)
		{
			List<ITable> tablesToProcess = new List<ITable>();
			tablesToProcess.Add(table);
			List<string> existingEntityNames = table.Database.MappingSet.EntitySet.Entities.Select(ent => ent.Name).ToList();
			ArchAngel.Interfaces.ProjectOptions.ModelScripts.Scripts.ExistingEntityNames = existingEntityNames;
			EntityModel.Controller.MappingLayer.MappingProcessor proc = new EntityModel.Controller.MappingLayer.MappingProcessor(new EntityModel.Controller.MappingLayer.OneToOneEntityProcessor(existingEntityNames));
			List<Entity> newEntities = proc.CreateOneToOneMappingsFor(tablesToProcess, table.Database.MappingSet);

			Populate();

			if (treeNodes.ContainsKey(newEntities[0].InternalIdentifier))
				advTree1.SelectedNode = treeNodes[newEntities[0].InternalIdentifier].First();

			return newEntities[0];
		}

		private void contextMenuAddReference_Click(object sender, EventArgs e)
		{
			AddNewReference();
		}

		private void contextMenuNewComponent_Click(object sender, EventArgs e)
		{
			ComponentAddedFromContextMenu = true;
			CreateNewComponentSpec();
			ComponentAddedFromContextMenu = false;
		}

		private void contextMenuAddComponentToEntity_Click(object sender, EventArgs e)
		{
			AddComponentToEntity();
		}

		private void contextMenuNewDatabaseFromEntities_Click(object sender, EventArgs e)
		{
			CreateOneToOneDatabaseMapping();
		}

		private void contextMenuCheckModel_Click(object sender, EventArgs e)
		{
			CheckModel();
		}

		private void CheckModel()
		{
			diagrammer.FinaliseEdits();

			if (RunAllValidationRules())
			{
				MessageBox.Show(this, "Model is ok - no problems found.", "Valid", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			//StartSearchTimer();
			btnClearSearchBox.Image = string.IsNullOrEmpty(textBoxSearch.Text) ? UndoImageDisabled : UndoImage;
			FilterTablesInTreeList(textBoxSearch.Text);
		}

		private void contextMenuShowDBChanges_Click(object sender, EventArgs e)
		{
			//CompareCurrentAndInitialDatabases();
		}

		private void mnuRefreshDatabaseSchema_Click(object sender, EventArgs e)
		{
			//DatabaseTypes databaseType, ConnectionStringHelper connStringHelper, string selectedDatabase
			//FormDatabase.RefreshSchema(MappingSet.Database.DatabaseType, null, MappingSet.Database.Name);
		}
	}
}
