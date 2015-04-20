using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using DevComponents.DotNetBar;

namespace ArchAngel.Providers.EntityModel.UI.PropertyGrids
{
	public partial class EntityForm2 : UserControl
	{
		private enum EntityDiagramTypes
		{
			//EntityReferences,
			EntityInheritance,
			EntityMapping,
			EntityHybrid
		}
		private enum TableDiagramTypes
		{
			TableRelationships,
			TableMapping
		}
		private enum ComponentDiagramTypes
		{
			Mappings
		}
		public delegate void TableActionDelegate(ITable table);
		public delegate void EntityActionDelegate(EntityImpl entity);
		public event TableActionDelegate TableSelected;
		public event EntityActionDelegate EntitySelected;

		private static Image ReferencesImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.tree_24.png"));
		private static Image ReferencesImageDisabled = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.tree_24_d.png"));
		private static Image MappingImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.view24.png"));
		private static Image MappingImageDisabled = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.view24_d.png"));

		private EntityDiagramTypes EntityDiagramType = EntityDiagramTypes.EntityMapping;
		private TableDiagramTypes TableDiagramType = TableDiagramTypes.TableRelationships;
		private ComponentDiagramTypes ComponentDiagramType = ComponentDiagramTypes.Mappings;
		private Editors.EntityMappingDiagram MappingEditor = new ArchAngel.Providers.EntityModel.UI.Editors.EntityMappingDiagram();
		private Editors.EntityInheritanceDiagram InheritanceEditor = new ArchAngel.Providers.EntityModel.UI.Editors.EntityInheritanceDiagram();
		private Editors.TableMappingDiagram TableMappingEditor = new ArchAngel.Providers.EntityModel.UI.Editors.TableMappingDiagram();
		private Editors.ComponentMappingDiagram ComponentMappingEditor = new ArchAngel.Providers.EntityModel.UI.Editors.ComponentMappingDiagram();
		private List<ITable> MappedTables = new List<ITable>();
		private List<Entity> MappedEntities = new List<Entity>();
		private EntityImpl _Entity;
		private ComponentSpecification _ComponentSpecification;
		private Table _Table;
		private bool ShowTablesMappedEntities = true;
		private bool ShowTablesRelatedTables = true;
		private bool ShowEntityReferences = true;
		private bool ShowEntityMappedTables = true;
		internal static bool BusyPopulating = false;
		private bool BusyFinalizingEdits = false;

		public EntityForm2()
		{
			InitializeComponent();

			BackColor = Color.Black;// Color.FromArgb(40, 40, 40);

			entityDetailsEditor1.BackColor = this.BackColor;
			tableDetailsEditor1.BackColor = this.BackColor;
			componentDetailsEditor1.BackColor = this.BackColor;

			MappingEditor.TableAdded += new ArchAngel.Providers.EntityModel.UI.Editors.EntityMappingDiagram.TableActionDelegate(MappingEditor_TableAdded);
			MappingEditor.TableRemoved += new ArchAngel.Providers.EntityModel.UI.Editors.EntityMappingDiagram.TableActionDelegate(MappingEditor_TableRemoved);
			MappingEditor.EntityDeleted += new ArchAngel.Providers.EntityModel.UI.Editors.EntityMappingDiagram.EntityActionDelegate(MappingEditor_EntityDeleted);
			MappingEditor.TableSelected += new ArchAngel.Providers.EntityModel.UI.Editors.EntityMappingDiagram.TableActionDelegate(MappingEditor_TableSelected);
			MappingEditor.EntitySelected += new ArchAngel.Providers.EntityModel.UI.Editors.EntityMappingDiagram.EntityActionDelegate(MappingEditor_EntitySelected);
			MappingEditor.EntityUpdated += new ArchAngel.Providers.EntityModel.UI.Editors.EntityMappingDiagram.EntityActionDelegate(MappingEditor_EntityUpdated);

			InheritanceEditor.EntitySelected += new ArchAngel.Providers.EntityModel.UI.Editors.EntityMappingDiagram.EntityActionDelegate(MappingEditor_EntitySelected);
			InheritanceEditor.EntityDeleted += new Editors.EntityMappingDiagram.EntityActionDelegate(MappingEditor_EntityDeleted);

			tableKeysEditor1.KeysChanged += new ArchAngel.Providers.EntityModel.UI.Editors.TableKeysEditor.KeysChangedDelegate(tableKeysEditor1_KeysChanged);

			TableMappingEditor.EntityAdded += new ArchAngel.Providers.EntityModel.UI.Editors.TableMappingDiagram.EntityActionDelegate(TableMappingEditor_EntityAdded);
			TableMappingEditor.EntityRemoved += new ArchAngel.Providers.EntityModel.UI.Editors.TableMappingDiagram.EntityActionDelegate(TableMappingEditor_EntityRemoved);
			TableMappingEditor.EntityCreated += new ArchAngel.Providers.EntityModel.UI.Editors.TableMappingDiagram.EntityCreatedDelegate(TableMappingEditor_EntityCreated);
			TableMappingEditor.TableDeleted += new ArchAngel.Providers.EntityModel.UI.Editors.TableMappingDiagram.TableActionDelegate(TableMappingEditor_TableDeleted);
			TableMappingEditor.EntitySelected += new ArchAngel.Providers.EntityModel.UI.Editors.TableMappingDiagram.EntityActionDelegate(TableMappingEditor_EntitySelected);
			TableMappingEditor.TableSelected += new ArchAngel.Providers.EntityModel.UI.Editors.TableMappingDiagram.TableActionDelegate(TableMappingEditor_TableSelected);
			TableMappingEditor.RelationshipDeleted += new ArchAngel.Providers.EntityModel.UI.Editors.TableMappingDiagram.RelationshipActionDelegate(TableMappingEditor_RelationshipDeleted);

			ComponentMappingEditor.EntitySelected += new ArchAngel.Providers.EntityModel.UI.Editors.ComponentMappingDiagram.EntityActionDelegate(ComponentMappingEditor_EntitySelected);

			componentPropertiesEditor1.ComponentChanged += new EventHandler(componentPropertiesEditor1_ComponentChanged);

			for (int i = 0; i < bar1.Tabs.Count; i++)
			{
				DevComponents.DotNetBar.SuperTabItem tab = (DevComponents.DotNetBar.SuperTabItem)bar1.Tabs[i];

				tab.TabColor.Default.Normal.Background.Colors = new Color[] { Color.FromArgb(10, 10, 10) };
				tab.TabColor.Default.Normal.Text = Color.White;
				tab.TabColor.Default.Normal.InnerBorder = Color.FromArgb(10, 10, 10);
				tab.TabColor.Default.Normal.OuterBorder = Color.FromArgb(10, 10, 10);

				tab.TabColor.Default.Selected.Background.Colors = new Color[] { Color.FromArgb(100, 100, 100), Color.FromArgb(20, 20, 20) };
				tab.TabColor.Default.Selected.Background.GradientAngle = 90;
				tab.TabColor.Default.Selected.InnerBorder = Color.FromArgb(140, 140, 140);
				tab.TabColor.Default.Selected.OuterBorder = Color.Black;
				tab.TabColor.Default.Selected.Text = Color.White;

				tab.TabColor.Default.SelectedMouseOver.Background.Colors = new Color[] { Color.FromArgb(120, 120, 120), Color.FromArgb(50, 50, 50) };
				tab.TabColor.Default.SelectedMouseOver.Background.GradientAngle = 90;
				tab.TabColor.Default.SelectedMouseOver.InnerBorder = Color.FromArgb(140, 140, 140);
				tab.TabColor.Default.SelectedMouseOver.OuterBorder = Color.Black;
				tab.TabColor.Default.SelectedMouseOver.Text = Color.White;

				tab.TabColor.Default.MouseOver.Background.Colors = new Color[] { Color.FromArgb(70, 70, 70) };
				tab.TabColor.Default.MouseOver.InnerBorder = Color.FromArgb(140, 140, 140);
				tab.TabColor.Default.MouseOver.OuterBorder = Color.Black;
				tab.TabColor.Default.MouseOver.Text = Color.White;
			}
		}

		void InheritanceEditor_EntityDeleted(EntityImpl entity)
		{
			PopulateEntity(false);
		}

		void MappingEditor_EntityUpdated(EntityImpl entity)
		{
			propertiesEditorEntityProperties.RefreshData();
		}

		void TableMappingEditor_RelationshipDeleted(Relationship relationship)
		{
			//Table.GetMappingSet().DeleteRelationship(relationship);
			relationship.DeleteSelf();//.PrimaryTable.RemoveRelationship(re
			//Table.RemoveRelationship(relationship);
			PopulateTable(true);
		}

		internal bool HasData
		{
			get
			{
				return Entity != null || Table != null || ComponentSpecification != null;
			}
		}

		void componentPropertiesEditor1_ComponentChanged(object sender, EventArgs e)
		{
			PopulateComponent(true);
		}

		void ComponentMappingEditor_EntitySelected(Entity entity)
		{
			Entity = (EntityImpl)entity;
			PopulateEntity(false);
		}

		void TableMappingEditor_TableSelected(Table nextTable)
		{
			Table = nextTable;

			if (TableSelected != null)
				TableSelected(Table);
		}

		void TableMappingEditor_EntitySelected(Entity entity, Table table)
		{
			Entity = (EntityImpl)entity;

			if (EntitySelected != null)
				EntitySelected(Entity);
		}

		void MappingEditor_EntitySelected(EntityImpl entity)
		{
			Entity = entity;

			if (EntitySelected != null)
				EntitySelected(Entity);
		}

		void MappingEditor_TableSelected(ITable table)
		{
			Table = (Table)table;

			if (TableSelected != null)
				TableSelected(Table);
		}

		void MappingEditor_EntityDeleted(EntityImpl nextEntity)
		{
			if (nextEntity != null)
			{
				Entity = nextEntity;
				PopulateEntity(false);
			}
		}

		void TableMappingEditor_TableDeleted(Table nextTable)
		{
			//if (nextTable != null)
			//{
			Table = nextTable;
			PopulateTable(false);
			//}
		}

		void TableMappingEditor_EntityCreated(Table fromTable)
		{
			foreach (Entity entity in Table.MappedEntities())
			{
				if (!MappedEntities.Contains(entity))
				{
					MappedEntities.Add(entity);
					break;
				}
			}
		}

		void TableMappingEditor_EntityRemoved(Entity entity, Table table)
		{
			//bool changed = false;
			MappedEntities.Remove(entity);

			foreach (Property property in entity.Properties)
			{
				IColumn mappedColumn = property.MappedColumn();

				if (mappedColumn != null &&
					mappedColumn.Parent == Table)
				{
					property.SetMappedColumn(null);
					//changed = true;
				}
			}
			DrawTableMappings();

			//if (changed || MappedTables.Count == 1)
			//{
			//    PopulateEntityPropertyEditor();
			//}
		}

		void TableMappingEditor_EntityAdded(Entity entity, Table table)
		{
			BusyPopulating = true;

			try
			{
				//foreach (IColumn column in table.Columns)
				//{
				//    Property newProperty = Controller.MappingLayer.OneToOneEntityProcessor.CreatePropertyFromColumn(column);
				//    entity.AddProperty(newProperty);
				//    newProperty.SetMappedColumn(column);
				//}
				Entity = null;
				Entity = (EntityImpl)entity;
				//MappedTables.Add(Table);

				Table = null;
				//MappedEntities.Add(entity);
				//DrawTableMappings();
				//DrawEntityMappings();
				//PopulateTableColumnEditor();
			}
			finally
			{
				BusyPopulating = false;
			}
		}

		void tableKeysEditor1_KeysChanged()
		{
			if (!BusyFinalizingEdits)
				PopulateTableColumnEditor();
		}

		void MappingEditor_TableRemoved(ArchAngel.Providers.EntityModel.Model.DatabaseLayer.ITable table)
		{
			BusyPopulating = true;
			bool changed = false;
			MappedTables.Remove(table);
			var mapping = Entity.Mappings().SingleOrDefault(m => m.FromTable == table);

			if (mapping != null)
				mapping.Delete();

			if (MessageBox.Show(this, "Remove all entity properties that are mapped to this table's columns?", "Remove properties", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				for (int i = Entity.Properties.Count() - 1; i >= 0; i--)
				{
					Property property = Entity.Properties.ElementAt(i);
					IColumn mappedColumn = property.MappedColumn();

					if (mappedColumn != null &&
						mappedColumn.Parent == table)
					{
						Entity.RemoveProperty(property);
						changed = true;
					}
				}
			}
			else
			{
				foreach (Property property in Entity.Properties)
				{
					IColumn mappedColumn = property.MappedColumn();

					if (mappedColumn != null &&
						mappedColumn.Parent == table)
					{
						property.SetMappedColumn(null);
						changed = true;
					}
				}
			}
			BusyPopulating = false;
			DrawEntityMappings();

			if (changed || MappedTables.Count == 1)
			{
				PopulateEntityPropertyEditor();
			}
		}

		void MappingEditor_TableAdded(ArchAngel.Providers.EntityModel.Model.DatabaseLayer.ITable table)
		{
			MappedTables.Add(table);
			DrawEntityMappings();
			PopulateEntityPropertyEditor();
		}

		public EntityImpl Entity
		{
			get { return _Entity; }
			set
			{
				//if (_Entity != value)
				//{
				if (_Entity != null)
				{
					_Entity.PropertyChanged -= new PropertyChangedEventHandler(Entity_PropertyChanged);
					_Entity.PropertiesChanged -= new Slyce.Common.CollectionChangeHandler<Property>(Entity_PropertiesChanged);
				}
				_Entity = value;

				if (_Entity != null)
				{
					MappedTables.Clear();

					foreach (ITable table in value.MappedTables())
					{
						MappedTables.Add(table);
					}
					//EntityDiagramType = EntityDiagramTypes.EntityMapping;
					PopulateEntity(false);
					value.PropertyChanged += new PropertyChangedEventHandler(Entity_PropertyChanged);
					value.PropertiesChanged += new Slyce.Common.CollectionChangeHandler<Property>(Entity_PropertiesChanged);
					ComponentSpecification = null;
					//Table = null;
				}
				//pictureEntityReferences.Left = 15;
				//pictureEntityMapping.Left = pictureEntityReferences.Right + 10;
				//pictureEntityReferences.Visible = true;
				//pictureEntityMapping.Visible = true;
				//}
			}
		}

		void Entity_PropertiesChanged(object sender, Slyce.Common.CollectionChangeEvent<Property> args)
		{
			if (!BusyPopulating)
			{
				if (panelDiagram.Controls.Contains(MappingEditor))
					PopulateEntity(true);
			}
		}

		public Table Table
		{
			get { return _Table; }
			set
			{
				if (_Table != value)
				{
					if (_Table != null)
					{
						_Table.PropertyChanged -= new PropertyChangedEventHandler(Table_PropertyChanged);
						_Table.ColumnsChanged -= new EventHandler<Slyce.Common.CollectionChangeEvent<IColumn>>(Table_ColumnsChanged);
					}
					_Table = value;

					if (_Table != null)
					{
						MappedEntities.Clear();

						foreach (Entity entity in value.MappedEntities())
						{
							MappedEntities.Add(entity);
						}
						TableDiagramType = TableDiagramTypes.TableMapping;
						PopulateTable(false);
						value.PropertyChanged += new PropertyChangedEventHandler(Table_PropertyChanged);
						value.ColumnsChanged += new EventHandler<Slyce.Common.CollectionChangeEvent<IColumn>>(Table_ColumnsChanged);
						ComponentSpecification = null;
						Entity = null;
						buttonShowMapping.Visible = false;
						buttonShowInheritance.Visible = false;
					}
				}
			}
		}

		void Table_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Columns" || e.PropertyName == "Column" || e.PropertyName == "Name")
				if (!BusyPopulating)
				{
					if (panelDiagram.Controls.Contains(TableMappingEditor))
						PopulateTable(true);
				}
		}

		public ComponentSpecification ComponentSpecification
		{
			get { return _ComponentSpecification; }
			set
			{
				if (_ComponentSpecification != value)
				{
					_ComponentSpecification = value;
					buttonShowMapping.Visible = false;

					if (_ComponentSpecification != null)
					{
						ComponentDiagramType = ComponentDiagramTypes.Mappings;
						PopulateComponent(false);
					}
					else
					{
						Entity = null;
						Table = null;
					}
				}
			}
		}

		void Component_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			PopulateComponent(true);
		}

		void Table_ColumnsChanged(object sender, Slyce.Common.CollectionChangeEvent<IColumn> e)
		{
			if (!BusyPopulating)
			{
				if (panelDiagram.Controls.Contains(TableMappingEditor))
					PopulateTable(true);
			}
		}

		void Entity_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (!BusyPopulating)
			{
				if (panelDiagram.Controls.Contains(MappingEditor))
					PopulateEntity(true);
			}
		}

		private void PopulateTable(bool diagramsOnly)
		{
			if (BusyFinalizingEdits)
				return;

			FinaliseEdits();
			TableDiagramType = TableDiagramTypes.TableRelationships;

			HideAllDockContainers(new List<DevComponents.DotNetBar.SuperTabItem>()
				{
					superTabItemTableDetails,
					superTabItemTableColumns,
					superTabItemTableKeys
				});
			ShowToolbarIcons(new List<ButtonX>());
			//dockContainerItemTableDetails.Visible = true;
			//dockContainerItemTableProperties.Visible = true;
			//dockContainerItemTableKeys.Visible = true;

			//switch (TableDiagramType)
			//{
			//    case TableDiagramTypes.TableRelationships:
			//        DrawTableRelationships();
			//        break;
			//    case TableDiagramTypes.TableMapping:
			DrawTableMappings();
			//        break;
			//    default:
			//        throw new NotImplementedException("Not handled yet");
			//}


			if (!diagramsOnly)
			{
				PopulateTableColumnEditor();
				PopulateTableDetailsEditor();
				PopulateTableKeyEditor();
			}
		}

		private void ShowToolbarIcons(List<ButtonX> picturesToShow)
		{
			foreach (Control control in panelTop.Controls)
				control.Visible = false;

			foreach (Control control in picturesToShow)
				control.Visible = true;
		}

		private void PopulateEntity(bool diagramsOnly)
		{
			//bool originalBusyPopulating = BusyPopulating;

			try
			{
				ArchAngel.Interfaces.Events.SetBusyPopulating(true);
				//BusyPopulating = true;

				if (!diagramsOnly)
					FinaliseEdits();

				Table = null;
				HideAllDockContainers(new List<DevComponents.DotNetBar.SuperTabItem>()
				{
					superTabItemEntityDetails,
					superTabItemEntityProperties
				});
				ShowToolbarIcons(new List<ButtonX>() { buttonShowInheritance, buttonShowMapping });

				buttonShowMapping.Left = 5;
				buttonShowInheritance.Left = buttonShowMapping.Right + 5;
				buttonShowMapping.BringToFront();
				buttonShowMapping.Refresh();
				panelTop.Refresh();
				//HideAllDockContainers();
				//dockContainerItemEntityDetails.Visible = true;
				//dockContainerItemEntityProperties.Visible = true;

				switch (EntityDiagramType)
				{
					case EntityDiagramTypes.EntityInheritance:
						DrawEntityInheritance();
						break;
					case EntityDiagramTypes.EntityMapping:
						DrawEntityMappings();
						break;
					//case EntityDiagramTypes.EntityReferences:
					//    DrawEntityReferences();
					//    break;
					case EntityDiagramTypes.EntityHybrid:
						DrawEntityHybrid();
						break;
					default:
						throw new NotImplementedException("Not handled yet");
				}
				if (!diagramsOnly)
				{
					PopulateEntityPropertyEditor();
					//PopulateEntityDetailsGrid();
					PopulateEntityDetailsEditor();
				}
			}
			finally
			{
				ArchAngel.Interfaces.Events.SetBusyPopulating(false);
				//BusyPopulating = originalBusyPopulating;
			}
		}

		internal void FinaliseEdits()
		{
			BusyFinalizingEdits = true;
			tablePropertiesEditor1.FinaliseEdits();
			tableKeysEditor1.FinaliseEdits();
			propertiesEditorEntityProperties.FinaliseEdits();
			BusyFinalizingEdits = false;
		}

		private void PopulateComponent(bool diagramsOnly)
		{
			ComponentDiagramType = ComponentDiagramTypes.Mappings;

			HideAllDockContainers(new List<DevComponents.DotNetBar.SuperTabItem>()
				{
					superTabItemComponentDetails,
					superTabItemComponentProperties
				});
			ShowToolbarIcons(new List<ButtonX>());

			DrawComponentMappings();

			if (!diagramsOnly)
			{
				PopulateComponentDetailsEditor();
				PopulateComponentPropertyEditor();
			}
		}

		void prop_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			PopulateComponent(true);
		}

		private void HideAllDockContainers(IList<DevComponents.DotNetBar.SuperTabItem> itemsToShow)
		{
			foreach (DevComponents.DotNetBar.SuperTabItem item in itemsToShow)
			{
				if (!item.Visible)
					item.Visible = true;
			}
			foreach (DevComponents.DotNetBar.SuperTabItem item in bar1.Tabs)
			{
				if (!itemsToShow.Contains(item) && item.Visible)
					item.Visible = false;
			}
			//dockContainerItemComponentDetails.Visible = false;
			//dockContainerItemComponentProperties.Visible = false;
			//dockContainerItemTableDetails.Visible = false;
			//dockContainerItemTableProperties.Visible = false;
			//dockContainerItemTableKeys.Visible = false;
			//dockContainerItemEntityDetails.Visible = false;
			//dockContainerItemEntityProperties.Visible = false;
		}

		private void PopulateEntityDetailsEditor()
		{
			entityDetailsEditor1.VerticalScroll.Value = 0;
			entityDetailsEditor1.Entity = Entity;
		}

		private void PopulateTableDetailsEditor()
		{
			tableDetailsEditor1.Table = Table;
			//entityDetailsEditor1.Entity = Entity;
		}

		private void PopulateComponentDetailsEditor()
		{
			componentDetailsEditor1.ComponentSpecification = ComponentSpecification;
			//entityDetailsEditor1.Entity = Entity;
		}

		private void DrawEntityInheritance()
		{
			highlighter1.SetHighlightColor(buttonShowInheritance, DevComponents.DotNetBar.Validator.eHighlightColor.Orange);
			highlighter1.SetHighlightColor(buttonShowMapping, DevComponents.DotNetBar.Validator.eHighlightColor.None);
			InheritanceEditor.Entity = Entity;

			if (!panelDiagram.Controls.Contains(InheritanceEditor))
			{
				Slyce.Common.Utility.SuspendPainting(panelDiagram);
				panelDiagram.Controls.Clear();
				panelDiagram.Controls.Add(InheritanceEditor);
				InheritanceEditor.Dock = DockStyle.Fill;
				Slyce.Common.Utility.ResumePainting(panelDiagram);
			}
			else
				InheritanceEditor.Refresh();
		}

		private void DrawEntityMappings()
		{
			highlighter1.SetHighlightColor(buttonShowInheritance, DevComponents.DotNetBar.Validator.eHighlightColor.None);
			highlighter1.SetHighlightColor(buttonShowMapping, DevComponents.DotNetBar.Validator.eHighlightColor.Orange);
			MappingEditor.MappedTables = MappedTables;
			MappingEditor.ShowMappedTables = ShowEntityMappedTables;
			MappingEditor.ShowReferencedEntities = ShowEntityReferences;
			MappingEditor.TheEntity = Entity;

			if (!panelDiagram.Controls.Contains(MappingEditor))
			{
				Slyce.Common.Utility.SuspendPainting(panelDiagram);
				panelDiagram.Controls.Clear();
				panelDiagram.Controls.Add(MappingEditor);
				MappingEditor.Dock = DockStyle.Fill;
				Slyce.Common.Utility.ResumePainting(panelDiagram);
			}
			else
				MappingEditor.Refresh();
		}

		//private void DrawEntityReferences()
		//{
		//    Editors.EntityReferenceDiagram editor = new Editors.EntityReferenceDiagram();
		//    panelDiagram.Controls.Clear();
		//    panelDiagram.Controls.Add(editor);
		//    editor.Dock = DockStyle.Fill;
		//    editor.Entity = Entity;
		//}

		private void DrawEntityHybrid()
		{
			Editors.EntityHybridDiagram editor = new Editors.EntityHybridDiagram();
			panelDiagram.Controls.Clear();
			panelDiagram.Controls.Add(editor);
			editor.Dock = DockStyle.Fill;
			editor.Entity = Entity;
		}

		private void DrawTableMappings()
		{
			TableMappingEditor.ShowMappedEntities = ShowTablesMappedEntities;
			TableMappingEditor.ShowRelatedtables = ShowTablesRelatedTables;
			TableMappingEditor.MappedEntities = MappedEntities;
			TableMappingEditor.Table = Table;

			if (!panelDiagram.Controls.Contains(TableMappingEditor))
			{
				Slyce.Common.Utility.SuspendPainting(panelDiagram);
				panelDiagram.Controls.Clear();
				panelDiagram.Controls.Add(TableMappingEditor);
				TableMappingEditor.Dock = DockStyle.Fill;
				Slyce.Common.Utility.ResumePainting(panelDiagram);
			}
			else
				TableMappingEditor.Refresh();
		}

		private void DrawComponentMappings()
		{
			//ComponentMappingEditor.ShowMappedEntities = ShowTablesMappedEntities;
			//ComponentMappingEditor.ShowRelatedtables = ShowTablesRelatedTables;
			//ComponentMappingEditor.MappedEntities = MappedEntities;
			ComponentMappingEditor.ComponentSpecification = ComponentSpecification;

			if (!panelDiagram.Controls.Contains(ComponentMappingEditor))
			{
				panelDiagram.Controls.Clear();
				panelDiagram.Controls.Add(ComponentMappingEditor);
				ComponentMappingEditor.Dock = DockStyle.Fill;
			}
			ComponentMappingEditor.Refresh();
		}

		private void PopulateEntityPropertyEditor()
		{
			propertiesEditorEntityProperties.MappedTables = MappedTables;
			propertiesEditorEntityProperties.Entity = this.Entity;
		}

		private void PopulateComponentPropertyEditor()
		{
			componentPropertiesEditor1.ComponentSpecification = this.ComponentSpecification;
		}

		private void PopulateTableColumnEditor()
		{
			tablePropertiesEditor1.Table = this.Table;
		}

		private void PopulateTableKeyEditor()
		{
			tableKeysEditor1.Table = this.Table;
		}

		private void buttonHybrid_Click(object sender, EventArgs e)
		{
			EntityDiagramType = EntityDiagramTypes.EntityHybrid;
			DrawEntityHybrid();
		}

		private void buttonTableRelationships_Click(object sender, EventArgs e)
		{
			ShowTablesRelatedTables = !ShowTablesRelatedTables;
			//TableDiagramType = TableDiagramTypes.TableRelationships;
			//DrawTableRelationships();

			TableDiagramType = TableDiagramTypes.TableMapping;
			DrawTableMappings();
		}

		private void buttonTableMappings_Click(object sender, EventArgs e)
		{
			ShowTablesMappedEntities = !ShowTablesMappedEntities;
			TableDiagramType = TableDiagramTypes.TableMapping;
			DrawTableMappings();
		}

		private void buttonShowInheritance_Click(object sender, EventArgs e)
		{
			EntityDiagramType = EntityDiagramTypes.EntityInheritance;
			DrawEntityInheritance();
		}

		private void buttonShowMapping_Click(object sender, EventArgs e)
		{
			//ShowEntityMappedTables = !ShowEntityMappedTables;
			EntityDiagramType = EntityDiagramTypes.EntityMapping;
			DrawEntityMappings();
		}

	}
}
