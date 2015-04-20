using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using ArchAngel.Interfaces;
using ArchAngel.Interfaces.Controls;
using ArchAngel.Interfaces.Controls.ContentItems;
using ArchAngel.Interfaces.ITemplate;
using ArchAngel.Interfaces.TemplateInfo;
using ArchAngel.Providers.Database.Controls.AssociationWizard;
using ArchAngel.Providers.Database.Model;
using DevComponents.AdvTree;
using Slyce.Common;
using File=System.IO.File;
using View=ArchAngel.Providers.Database.Model.View;

namespace ArchAngel.Providers.Database.Controls.ContentItems
{
	public partial class EditModel : ContentItem
	{
		#region Enums

		[DotfuscatorDoNotRename]
		private enum Images
		{
			Unchecked = 0,
			Checked = 1,
			PartiallyChecked = 2,
			Database = 3,
			Table = 4,
			Column = 5,
			Key = 6,
			Index = 7,
			Filter = 8,
			Option = 9,
			Relationship = 10,
			StoredProcedure = 11,
			GreenBullet = 12,
			OrangeBullet = 13,
			RedBullet = 14,
			Red_Database = 15,
			Red_Table = 16,
			Red_Column = 17,
			Red_Key = 18,
			Red_Index = 19,
			Red_Filter = 20,
			Red_Option = 21,
			Red_Relationship = 22,
			Red_StoredProcedure = 23,
			Association = 24,
            Lookup = 25
		}

		[DotfuscatorDoNotRename]
		private enum BackGroundWorkerSteps
		{
			Step1,
			Step2,
			Step3
		}

		#endregion

		private List<IOption> _optionsForTable;
		private List<IOption> _optionsForViews;
		private List<IOption> _optionsForStoredProcedures;
		private List<IOption> _optionsForScriptObjects;
		private List<IOption> _optionsForColumns;
		private List<IOption> _optionsForOneToOneRelationships;
		private List<IOption> _optionsForOneToManyRelationship;
		private List<IOption> _optionsForManyToOneRelationship;
		private List<IOption> _optionsForManyToManyRelationship;
		private List<IOption> _optionsForFilters;
		private List<IOption> _optionsForIndexes;
		private List<IOption> _optionsForKeys;
		private bool MustSetDefaultValues;
		private BackGroundWorkerSteps CurrentBackgroundWorkerStep;
		private readonly Type ScriptBaseType = typeof (ScriptBase);
		private readonly Type ScriptObjectType = typeof (ScriptObject);
		private readonly Type RelationshipType = typeof (Relationship);
		private const int RedBulletValue = (int) Images.RedBullet;
		private const int GreenBulletValue = (int) Images.GreenBullet;

		private const int RedColumnValue = (int) Images.Red_Column;
		private const int RedDatabaseValue = (int) Images.Red_Database;
		private const int RedFilterValue = (int) Images.Red_Filter;
		private const int RedIndexValue = (int) Images.Red_Index;
		private const int RedKeyValue = (int) Images.Red_Key;
		private const int RedOptionValue = (int) Images.Red_Option;
		private const int RedRelationshipValue = (int) Images.Red_Relationship;
		private const int RedStoredProcedureValue = (int) Images.Red_StoredProcedure;
		private const int RedTableValue = (int) Images.Red_Table;

		private readonly Type DatabaseType = typeof (Model.Database);
		protected Dictionary<Type, PropertyInfo[]> ObjectProperties = new Dictionary<Type, PropertyInfo[]>();
		protected Dictionary<string, MethodInfo> DefaultValueMethods = new Dictionary<string, MethodInfo>();
		private bool RefreshTables = true;
		private bool RefreshViews = true;
		private bool RefreshStoredProcedures = true;
        private Dictionary<int, bool> ColumnSortOrders = new Dictionary<int, bool>();

		public EditModel()
		{
			InitializeComponent();
			treeList.AllowDrop = false;
			BackColor = Colors.BackgroundColor;

			if (Utility.InDesignMode)
			{
				return;
			}
			HasPrev = true;
			HasNext = true;
			HelpPage = "Workbench_Screen_Object_Model.htm";
			Title = "Object Model";
			PageDescription = "Fine-tune what your object model should look like.";

			Interfaces.Events.ObjectBeingProcessed += BllDatabase_ObjectBeingProcessed;
			ObjectProperties = new Dictionary<Type, PropertyInfo[]>();
			DefaultValueMethods = new Dictionary<string, MethodInfo>();
			labelDatabaseFeedback.Text = "";

            for (int i = 0; i < treeList.Columns.Count; i++)
            {
                DevComponents.AdvTree.ColumnHeader column = treeList.Columns[i];
                column.MouseUp += column_MouseUp;
                ColumnSortOrders.Add(i, true);
            }
		}

        void column_MouseUp(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            DevComponents.AdvTree.ColumnHeader column = (DevComponents.AdvTree.ColumnHeader)sender;
            int columnIndex = treeList.Columns.IndexOf(column);
            bool ascending = ColumnSortOrders[columnIndex];
            ColumnSortOrders[columnIndex] = !ColumnSortOrders[columnIndex];
            TreelistUtility.TreelistNodeComparer.SortDirection sortDirection = ascending ? TreelistUtility.TreelistNodeComparer.SortDirection.Ascending : Slyce.Common.TreelistUtility.TreelistNodeComparer.SortDirection.Descending;
            TreelistUtility.TreelistNodeComparer comparer = new TreelistUtility.TreelistNodeComparer(sortDirection, columnIndex);
            treeList.BeginUpdate();
            TreelistUtility.SortNodes(treeList, comparer);
            treeList.EndUpdate();
            Cursor = Cursors.Default;
        }

		public override void Clear()
		{
			ClearNodes(treeList.Nodes);

			Interfaces.Events.ObjectBeingProcessed -= BllDatabase_ObjectBeingProcessed;
		}

		private void ClearNodes(NodeCollection nodes)
		{
			foreach(Node node in nodes)
			{
				node.Tag = null;
				foreach(Cell cell in node.Cells)
				{
					cell.Tag = null;
				}
				ClearNodes(node.Nodes);
			}
			nodes.Clear();
		}

		internal void Populate()
		{
			if (File.Exists(SharedData.TemplateFileName))
			{
				// Settings
				LoadTreeView();
			}
			else
			{
				// No project file has been specified. There is probably no project file.
				treeList.Nodes.Clear();
			}
		}

		private void BllDatabase_ObjectBeingProcessed(string objectName, string description)
		{
			if (labelDatabaseFeedback.InvokeRequired)
			{
				MethodInvoker mi = () => BllDatabase_ObjectBeingProcessed(objectName, description);
				Invoke(mi);
				return;
			}

			labelDatabaseFeedback.Text = string.Format("Processing: {1} - {0}", objectName, description);
			labelDatabaseFeedback.Refresh();
			Utility.UpdateMessagePanelStatus(this, string.Format("Processing: {1} - {0}", objectName, description));
		}

		public override bool Back()
		{
			return true;
		}

		public override bool Next()
		{
			return true;
		}

		#region Edit Model

		///<summary>
		/// Calls Invoke with a timeout, which will keep waiting until either the 
		/// timer runs out and the Application is closing, or the method finishes.
		///</summary>
		///<param name="control">The control to Invoke on.</param>
		///<param name="theMethod">The delegate to invoke.</param>
		public static void SafeInvoke(Control control, Delegate theMethod)
		{
			// TODO: This method is repeated in Workbench.Controller, Designer.Controller and here.
			// It should be defined in one place. Also, ApplicationClosing isn't implemented
			// for Providers, and we need a solution for this fact.
			IAsyncResult result = control.BeginInvoke(theMethod);
			while (result.IsCompleted == false) // && Instance.ApplicationClosing == false)
			{
				result.AsyncWaitHandle.WaitOne(10000, false);
			}
		}

		[DotfuscatorDoNotRename]
		public void LoadTreeView()
		{
			bool isThreadSafe = false;
			
			if (InvokeRequired)
			{
				MethodInvoker mi = LoadTreeView;
				SafeInvoke(this, mi);
				return;
			}
			try
			{
				isThreadSafe = true;
				FillOptions();
				Utility.SuspendPainting(this);
				treeList.BeginUpdate();
				treeList.Nodes.Clear();

				if (ProviderInfo.TheBllDatabase == null)
				{
					return;
				}
				foreach (Model.Database database in ProviderInfo.TheBllDatabase.Databases)
				{
					database.SnapshotMode = true;
				}
				foreach (Model.Database database in ProviderInfo.TheBllDatabase.Databases)
				{
					Node rootNode = AddNode(null, database.Name, database, Images.Database, false, true);

					#region Tables

					Node tablesNode = AddNode(rootNode, "Tables", database.Tables, Images.Table, false, true);
					
					foreach (Table table in database.Tables)
					{
						AddScriptObjectNode(tablesNode, table, Images.GreenBullet);
					}
			
					tablesNode.Nodes.Sort();

					#endregion

					#region Views

					Node viewNodes = AddNode(rootNode, "Views", database.Views, Images.Table, false, true);

					foreach (View view in database.Views)
					{
						AddScriptObjectNode(viewNodes, view, Images.GreenBullet);
					}

					viewNodes.Nodes.Sort();

					#endregion

					#region StoredProcedures

					Node storedProcedureNodes = AddNode(rootNode, "Stored Procedures", database.StoredProcedures,
					                                    Images.StoredProcedure, false, true);

					foreach (StoredProcedure storedProcedure in database.StoredProcedures)
					{
						AddScriptObjectNode(storedProcedureNodes, storedProcedure, Images.GreenBullet);
					}
					storedProcedureNodes.Nodes.Sort();

					#endregion

                    #region Lookups

                    Node lookupNodes = AddNode(rootNode, "Lookups", database.Lookups, Images.Lookup, false, true);

                    foreach (Lookup lookup in database.Lookups)
                    {
                        AddLookupNode(lookupNodes, lookup, Images.GreenBullet);
                    }
                    lookupNodes.Nodes.Sort();

                    #endregion

					rootNode.Expanded = true;
				}
				ProcessTreeValidity(treeList.Nodes);

				foreach (Model.Database database in ProviderInfo.TheBllDatabase.Databases)
				{
					database.SnapshotMode = false;
				}
				if (chkHideNonSelectedNodes.Checked)
				{
					HideUnselectedNodes();
				}
				treeList.Nodes.Sort();
			}
			finally
			{
				try
				{
					if (isThreadSafe)
					{
						treeList.EndUpdate();
						SetPerObjectUserOptions();
					}
				}
				finally
				{
					Utility.ResumePainting(this);
				}
			}
		}

		private void LoadTreeListNode(ScriptObject scriptObject)
		{
			foreach (Node treeListNode in treeList.Nodes)
			{
				LoadTreeListNode(treeListNode, scriptObject);
			}
		}

		private void LoadTreeListNode(Node parentTreeListNode, ScriptObject scriptObject)
		{
			foreach (Node childTreeListNode in parentTreeListNode.Nodes)
			{
				if (childTreeListNode.Tag == scriptObject)
				{
					childTreeListNode.Nodes.Clear();
					Images image = Images.Table;

					if (scriptObject.IsTable)
						image = Images.Table;
					else if (scriptObject.IsStoredProcedure)
						image = Images.StoredProcedure;
					else if (scriptObject.IsView)
						image = Images.Table;

					UpdateScriptObjectNode(childTreeListNode, scriptObject, image);
					return;
				}
				LoadTreeListNode(childTreeListNode, scriptObject);
			}
		}

		private static void GetTreeListNodeText(ScriptBase scriptBase, Node node)
		{
			if (scriptBase.GetType() == typeof (Table) ||
			    scriptBase.GetType() == typeof (View) ||
			    scriptBase.GetType() == typeof (StoredProcedure) ||
			    scriptBase.GetType() == typeof (Column) ||
			    scriptBase.GetType() == typeof (Filter) ||
			    scriptBase.GetType() == typeof (Index) ||
			    scriptBase.GetType() == typeof (Key) ||
			    scriptBase.GetType() == typeof (Association))
			{
				node.Text = scriptBase.Alias;

				if (scriptBase.Name != scriptBase.Alias)
				{
					node.Cells[1].Text = scriptBase.Name;
				}
				else
				{
					node.Cells[1].Text = "";
				}
				return;
			}

			if (scriptBase.GetType() == typeof (MapColumn))
			{
				MapColumn mapColumn = (MapColumn) scriptBase;
				node.Text = scriptBase.Alias;
				node.Cells[1].Text = scriptBase.Name != scriptBase.Alias ? scriptBase.Name : "";
				node.Cells[2].Text = mapColumn.ForeignColumn == null
				                     	? ""
				                     	: mapColumn.ForeignColumn.Parent.Alias + "." + mapColumn.ForeignColumn.Alias;
				return;
			}

			if (scriptBase.GetType() == typeof (OneToOneRelationship) ||
			    scriptBase.GetType() == typeof (OneToManyRelationship) ||
			    scriptBase.GetType() == typeof (ManyToOneRelationship) ||
			    scriptBase.GetType() == typeof (ManyToManyRelationship))
			{
				Relationship relationship = (Relationship) scriptBase;
				node.Text = scriptBase.Alias;
				node.Cells[1].Text = scriptBase.Name != scriptBase.Alias ? scriptBase.Name : "";
				node.Cells[2].Text = relationship.Path;
				return;
			}
			throw new Exception("ScriptBase Type " + scriptBase.GetType().Name + " not found");
		}

		private void UpdateTreeListNodeText(Node treeListNode, ScriptBase scriptBase)
		{
			GetTreeListNodeText(scriptBase, treeListNode);
			treeListNode.Tag = scriptBase;
		}

		private Node AddScriptObjectNode(Node scriptObjectNodes,
		                                                       ScriptObject scriptObject, Images image)
		{
			Node scriptObjectNode = AddNode(scriptObjectNodes, "", null, Images.Table, true,
			                                                      scriptObject.Enabled);
			UpdateScriptObjectNode(scriptObjectNode, scriptObject, image);
			return scriptObjectNode;
		}

        private Node AddLookupNode(Node lookupNodes, Lookup lookup, Images image)
        {
            Node lookupNode = AddNode(lookupNodes, lookup.Alias, lookup, image, true, lookup.Enabled);
            //UpdateScriptObjectNode(scriptObjectNode, scriptObject, Images.Lookup);
            return lookupNode;
        }

		private Node AddNode(Node parentNode, string text, object tag,
		                                           Images image, bool showCheckbox, bool isChecked)
		{
			return AddNode(parentNode, new[] {text, "", ""}, tag, image, showCheckbox, isChecked);
		}

		private Node AddNode(Node parentNode, string[] texts, object tag,
		                                           Images image, bool showCheckbox, bool isChecked)
		{
			// We need to ensure that the are always three text values
			List<string> newTexts = new List<string>(3);

			foreach (string text in texts)
			{
				newTexts.Add(text);
			}
			while (newTexts.Count < 3)
			{
				newTexts.Add("");
			}
			Node newNode = new Node();
			newNode.Text = newTexts[0];
			newNode.Cells.Add(new Cell(newTexts[1]));
			newNode.Cells.Add(new Cell(newTexts[2]));
			newNode.Tag = tag;
			SetNodeImage(newNode, image);
			newNode.Checked = isChecked;
			newNode.CheckBoxVisible = showCheckbox;
			newNode.StyleSelected = treeList.Styles["elementStyleSelected"];

			if (parentNode != null)
			{
				parentNode.Nodes.Add(newNode);
			}
			else
			{
				treeList.Nodes.Add(newNode);
			}
			return newNode;
		}

		private void UpdateScriptObjectNode(Node scriptObjectNode, ScriptObject scriptObject, Images image)
		{
			scriptObjectNode.Cells[0].Text = scriptObject.Alias;
			scriptObjectNode.Cells[1].Text = scriptObject.Name != scriptObject.Alias ? scriptObject.Name : "";
			scriptObjectNode.Tag = scriptObject;
			SetNodeImage(scriptObjectNode, image);
			scriptObjectNode.CheckBoxVisible = true;
			scriptObjectNode.Checked = scriptObject.Enabled;
			AddOptionNodes(scriptObjectNode, scriptObject);

			#region Columns

			Node columnNodes = AddNode(scriptObjectNode, "Columns", scriptObject.Columns, Images.Column,
			                                                 false, true);

			if (scriptObject.Columns.Length > 0)
			{
				AddNode(columnNodes, "", "LAZY", Images.Unchecked, true, false);
			}

			#endregion

			#region Relationships

			Node relationshipNodes = AddNode(scriptObjectNode, "Relationships", scriptObject.Relationships, Images.Relationship,
			                                 false, true);

			#region One-to-one relationships

			Node oneToOneRelationshipNodes = AddNode(relationshipNodes, "One to One", scriptObject.OneToOneRelationships,
			                                         Images.Relationship, false, true);

			if (scriptObject.OneToOneRelationships.Length > 0)
			{
				AddNode(oneToOneRelationshipNodes, "", "LAZY", Images.Unchecked, true, false);
			}

			#endregion

			#region One-to-many relationships

			Node oneToManyRelationshipNodes = AddNode(relationshipNodes, "One to Many",
			                                                                scriptObject.OneToManyRelationships,
			                                                                Images.Relationship, false, true);

			if (scriptObject.OneToManyRelationships.Length > 0)
			{
				AddNode(oneToManyRelationshipNodes, "", "LAZY", Images.Unchecked, true, false);
			}

			#endregion

			#region Many-to-one relationships

			Node manyToOneRelationshipNodes = AddNode(relationshipNodes, "Many to One",
			                                                                scriptObject.ManyToOneRelationships,
			                                                                Images.Relationship, false, true);

			if (scriptObject.ManyToOneRelationships.Length > 0)
			{
				AddNode(manyToOneRelationshipNodes, "", "LAZY", Images.Unchecked, true, false);
			}

			#endregion

			#region Many-to-many relationships

			Node manyToManyRelationshipNodes = AddNode(relationshipNodes, "Many to Many", scriptObject.ManyToManyRelationships,
			                                           Images.Relationship, false, true);

			if (scriptObject.ManyToManyRelationships.Length > 0)
			{
				AddNode(manyToManyRelationshipNodes, "", "LAZY", Images.Unchecked, true, false);
			}

			#endregion

			#endregion

			#region Filters

			Node filterNodes = AddNode(scriptObjectNode, "Filters", scriptObject.Filters, Images.Filter,
			                                                 false, true);

			if (scriptObject.Filters.Length > 0)
			{
				AddNode(filterNodes, "", "LAZY", Images.Unchecked, true, false);
			}

			#endregion

			#region Associations

			Node associationNodes = AddNode(scriptObjectNode, "Associations", scriptObject.Associations,
			                                                      Images.Association, false, true);

			if (scriptObject.Associations.Count > 0)
			{
				AddNode(associationNodes, "", "LAZY", Images.Unchecked, true, false);
			}

			#endregion

			#region Table-only nodes

			if (scriptObject.GetType() == typeof (Table))
			{
				Table table = (Table) scriptObject;
				Node indexNodes = AddNode(scriptObjectNode, "Indexes", table.Indexes, Images.Index, false,
				                                                true);

				if (table.Indexes.Length > 0)
				{
					AddNode(indexNodes, "", "LAZY", Images.Unchecked, true, false);
				}
				Node keyNodes = AddNode(scriptObjectNode, "Keys", table.Keys, Images.Key, false, true);

				if (table.Keys.Length > 0)
				{
					AddNode(keyNodes, "", "LAZY", Images.Unchecked, true, false);
				}
			}

			#endregion

			#region Stored-procedure-only nodes

			if (scriptObject.GetType() == typeof (StoredProcedure))
			{
				StoredProcedure storedProcedure = (StoredProcedure) scriptObject;
				Node parameterNodes = AddNode(scriptObjectNode, "Parameters", storedProcedure.Parameters,
				                                                    Images.StoredProcedure, false, true);

				if (storedProcedure.Parameters.Length > 0)
				{
					AddNode(parameterNodes, "", "LAZY", Images.Unchecked, true, false);
				}
			}

			#endregion
		}

		/// <summary>
		/// Adds per-object options to ScriptObject node.
		/// </summary>
		/// <param name="scriptObjectNode"></param>
		/// <param name="scriptObject"></param>
		[DotfuscatorDoNotRename]
		private void AddOptionNodes(Node scriptObjectNode, ScriptBase scriptObject)
		{
			string scriptObjectTypeName = scriptObject.GetType().FullName;

			if (scriptObjectTypeName == ModelTypes.MapColumn.FullName ||
			    scriptObjectTypeName == ModelTypes.Column.FullName)
			{
				AddIndividualOptions(scriptObjectNode, _optionsForColumns, scriptObject);
			}
			else if (scriptObjectTypeName == ModelTypes.Filter.FullName)
			{
				AddIndividualOptions(scriptObjectNode, _optionsForFilters, scriptObject);
			}
			else if (scriptObjectTypeName == ModelTypes.Index.FullName)
			{
				AddIndividualOptions(scriptObjectNode, _optionsForIndexes, scriptObject);
			}
			else if (scriptObjectTypeName == ModelTypes.Key.FullName)
			{
				AddIndividualOptions(scriptObjectNode, _optionsForKeys, scriptObject);
			}
			else if (scriptObjectTypeName == ModelTypes.ManyToManyRelationship.FullName)
			{
				AddIndividualOptions(scriptObjectNode, _optionsForManyToManyRelationship, scriptObject);
			}
			else if (scriptObjectTypeName == ModelTypes.ManyToOneRelationship.FullName)
			{
				AddIndividualOptions(scriptObjectNode, _optionsForManyToOneRelationship, scriptObject);
			}
			else if (scriptObjectTypeName == ModelTypes.OneToManyRelationship.FullName)
			{
				AddIndividualOptions(scriptObjectNode, _optionsForOneToManyRelationship, scriptObject);
			}
			else if (scriptObjectTypeName == ModelTypes.OneToOneRelationship.FullName)
			{
				AddIndividualOptions(scriptObjectNode, _optionsForOneToOneRelationships, scriptObject);
			}
			else if (scriptObjectTypeName == ModelTypes.ScriptObject.FullName)
			{
				AddIndividualOptions(scriptObjectNode, _optionsForScriptObjects, scriptObject);
			}
			else if (scriptObjectTypeName == ModelTypes.StoredProcedure.FullName)
			{
				AddIndividualOptions(scriptObjectNode, _optionsForStoredProcedures, scriptObject);
			}
			else if (scriptObjectTypeName == ModelTypes.Table.FullName)
			{
				AddIndividualOptions(scriptObjectNode, _optionsForTable, scriptObject);
			}
			else if (scriptObjectTypeName == ModelTypes.View.FullName)
			{
				AddIndividualOptions(scriptObjectNode, _optionsForViews, scriptObject);
			}
			else
			{
				throw new NotImplementedException("Not coded yet:" + scriptObject.GetType().FullName + ". Apparently '" +
				                                  scriptObjectTypeName + "' != '" + ModelTypes.Table.FullName + "'.");
			}
		}
        
		private void AddIndividualOptions(Node node, List<IOption> options,
		                                  IScriptBaseObject iteratorObject)
		{
            // TODO: Due to the high amount of processing, the Properties node should only be
            // added when the parent node is expanded, not all at once.
            //bool optionsExist = false;

            //foreach (IOption option in options)
            //{
            //    if (ProjectHelper.MyCurrentProject.DisplayOptionToUser(option, iteratorObject))
            //    {
            //        optionsExist = true;
            //        break;
            //    }
            //}
            //if (optionsExist)
            if (options.Count > 0)
			{
				Node optionsNode = AddNode(node, "Properties", options, Images.Option, false, true);
				AddNode(optionsNode, "", "LAZY", Images.Unchecked, true, false);
			}
		}

		/// <summary>
		/// Sets the per-object user options of the actual objects that are going to be used for generation.
		/// This ensures all values are set on the corredsponding objects, ready for generation. This function
		/// should get called just before the generation process begins, to update all tables, views, columns etc with
		/// the user-option values selected by the user.
		/// </summary>
		public void SetPerObjectUserOptions()
		{
			// Get the code to compile the UserOptions
			foreach (Node childNode in treeList.Nodes)
			{
				SetPerObjectUserOptionsForNodeAndChildren(childNode);
			}
		}

		private static void SetPerObjectUserOptionsForNodeAndChildren(Node node)
		{
			if (node.Tag.GetType() == typeof (Model.Database))
			{
				foreach (Node childNode in node.Nodes)
				{
					SetPerObjectUserOptionsForNodeAndChildren(childNode);
				}
			}
			ScriptBase scriptBase;
			Type type = node.Tag.GetType();

			// We need to handle any options that are not visible to the user ie: not in the treelist
			if (type.BaseType == typeof (ScriptBase))
			{
				scriptBase = (ScriptBase) node.Tag;

				foreach (IUserOption userOption in scriptBase.Ex)
				{
					// Find the matching Option for the UserOption
					foreach (IOption opt in Interfaces.SharedData.CurrentProject.Options)
					{
						if (opt.VariableName == userOption.Name)
						{
							if (!SharedData.CurrentProject.DisplayOptionToUser(opt, scriptBase))
							{
								object defaultValue;

								if (opt.DefaultValueIsFunction)
								{
									defaultValue = SharedData.CurrentProject.GetVirtualPropertyDefaultValue(opt.DefaultValue, scriptBase);
								}
								else
								{
									defaultValue = opt.DefaultValue;
								}
								userOption.Value = defaultValue;
							}
							break;
						}
					}
				}
			}
		}

		#region Check and Colour nodes

		private static CheckState GetNodeCheckState(Node treeListNode)
		{
			string typeName = BLL.Helper.GetBaseType(treeListNode.Tag).Name;

			if (typeName == "String" && (string) treeListNode.Tag == "LAZY")
			{
				return CheckState.Unchecked;
			}
			switch (typeName)
			{
				case "Database":
					return CheckState.Checked;

				case "ScriptBase":
					ScriptBase scriptBase = (ScriptBase) treeListNode.Tag;

					if (scriptBase.Enabled)
					{
						return CheckState.Checked;
					}
					return CheckState.Unchecked;

				case "ScriptBaseObject":
					return CheckState.Checked;

				case "Array":
					int enabledCount = 0;
					int disabledCount = 0;
					ScriptBase[] scriptBases = (ScriptBase[]) treeListNode.Tag;

					foreach (ScriptBase scriptBase1 in scriptBases)
					{
						if (scriptBase1.Enabled)
						{
							enabledCount++;
						}
						else
						{
							disabledCount++;
						}
					}

					if (disabledCount == scriptBases.Length)
					{
						return CheckState.Unchecked;
					}
					if (enabledCount == scriptBases.Length)
					{
						return CheckState.Checked;
					}
					else
					{
						return CheckState.Indeterminate;
					}

				case "List`1": // Generic List
					string fullTypeName = BLL.Helper.GetBaseType(treeListNode.Tag).FullName;

					if (fullTypeName.IndexOf("IOption") >= 0)
					{
						return CheckState.Checked;
					}
					if (fullTypeName.IndexOf("Association") >= 0)
					{
						List<Association> associations = (List<Association>) treeListNode.Tag;

						if (associations.Count > 0)
						{
							foreach (Association association in associations)
							{
								if (association.Enabled)
								{
									return CheckState.Checked;
								}
							}
						}
						return CheckState.Unchecked;
					}
					else
					{
						throw new Exception("Unexpected generic list type. TypeName = " + fullTypeName);
					}

				case "Option":
					Option option = (Option) treeListNode.Tag;

					if (option.Enabled)
					{
						return CheckState.Checked;
					}
					return CheckState.Unchecked;
				case "Association":
					Association association1 = (Association) treeListNode.Tag;

					if (association1.Enabled)
					{
						return CheckState.Checked;
					}
					return CheckState.Unchecked;

				default:
					throw new Exception("Cannot determine check state. TypeName = " + typeName);
			}
		}

		private void ToggleNodeCheckState(Node treeListNode)
		{
			treeList.BeginUpdate();
			CheckState checkState = GetNodeCheckState(treeListNode);

			if (checkState == CheckState.Indeterminate || checkState == CheckState.Unchecked)
			{
				checkState = CheckState.Checked;

				if (treeListNode.ImageIndex == (int) Images.OrangeBullet)
				{
					SetNodeImage(treeListNode, Images.GreenBullet);
				}
			}
			else
			{
				checkState = CheckState.Unchecked;

				if (treeListNode.ImageIndex == (int) Images.GreenBullet)
				{
					SetNodeImage(treeListNode, Images.OrangeBullet);
				}
			}

			SetNodeCheckState(treeListNode, checkState);

			foreach (Model.Database database in ProviderInfo.TheBllDatabase.Databases)
			{
				database.SnapshotMode = true;
			}
			ProcessTreeValidity(treeList.Nodes);

			foreach (Model.Database database in ProviderInfo.TheBllDatabase.Databases)
			{
				database.SnapshotMode = false;
			}
			treeList.EndUpdate();
			Interfaces.Events.RaiseIsDirtyEvent();
		}

		private static void SetNodeCheckState(Node treeListNode, CheckState checkState)
		{
			switch (checkState)
			{
				case CheckState.Checked:
					treeListNode.Checked = true;
					break;
				case CheckState.Unchecked:
					treeListNode.Checked = false;
					break;
			}
			Type type = BLL.Helper.GetBaseType(treeListNode.Tag);

			if (type == typeof (ScriptBase))
			{
				ScriptBase scriptBase = (ScriptBase) treeListNode.Tag;

				if (checkState == CheckState.Checked)
				{
					scriptBase.Enabled = true;
				}
				else
				{
					scriptBase.Enabled = false;
				}
				if (scriptBase.GetType() == typeof (ManyToOneRelationship))
				{
					// TODO: Multiple columns can be mapped on a single relationship. All should be disabled, not just one.
					ManyToOneRelationship relationship = (ManyToOneRelationship) scriptBase;
					MapColumn mapColumn = (MapColumn) relationship.Parent.GetColumn(relationship.Name);

					// If a MapColumn exists for this relationship, disable it.
					if (mapColumn != null)
					{
						mapColumn.Enabled = scriptBase.Enabled;
					}
				}
			}
			else if (type == typeof (Option))
			{
				Option option = (Option) treeListNode.Tag;

				if (checkState == CheckState.Checked)
				{
					option.Enabled = true;
				}
				else
				{
					option.Enabled = false;
				}
			}
			else if (type == typeof (Association))
			{
				Association association = (Association) treeListNode.Tag;

				if (checkState == CheckState.Checked)
				{
					association.Enabled = true;
				}
				else
				{
					association.Enabled = false;
				}
			}
			foreach (Node childTreeListNode in treeListNode.Nodes)
			{
				SetNodeCheckState(childTreeListNode, checkState);
			}
		}

		private static void HideAllMenuItems(ContextMenuStrip menu)
		{
			foreach (ToolStripItem item in menu.Items)
			{
				item.Visible = false;
			}
		}

		private void SetNodeStyle(Node node)
		{
			int nodeImageIndex = node.ImageIndex;

			if (nodeImageIndex == RedBulletValue ||
			    nodeImageIndex == RedColumnValue ||
			    nodeImageIndex == RedDatabaseValue ||
			    nodeImageIndex == RedFilterValue ||
			    nodeImageIndex == RedIndexValue ||
			    nodeImageIndex == RedKeyValue ||
			    nodeImageIndex == RedOptionValue ||
			    nodeImageIndex == RedRelationshipValue ||
			    nodeImageIndex == RedStoredProcedureValue ||
			    nodeImageIndex == RedTableValue)
			{
				node.Style = treeList.Styles["elementStyleInvalid"];
				return;
			}
			if (BLL.Helper.GetBaseType(node.Tag) == typeof (ScriptBase))
			{
				ScriptBase scriptBase = (ScriptBase) node.Tag;

				if (!scriptBase.Enabled)
				{
					node.Style = treeList.Styles["elementStyleUnselected"];
					return;
				}
				if (ModelTypes.MapColumn.IsInstanceOfType(scriptBase))
				{
					node.Style = treeList.Styles["elementStyleMapColumn"];
					return;
				}
				if (scriptBase.IsUserDefined)
				{
					node.Style = treeList.Styles["elementStyleUserDefined"];
					return;
				}
			}
			node.Style = treeList.Styles["elementStyle1"];
		}

		#endregion

		//private void treeList_Resize(object sender, EventArgs e)
		//{
		//    double treeViewWidth = Convert.ToDouble(treeList.Width);
		//    colAlias.Width.Absolute = (int)(treeViewWidth * 0.5);
		//    colName.Width.Absolute = (int)(treeList.Width * 0.25);
		//    colNotes.Width.Absolute = (int)(treeList.Width * 0.25);
		//}

		#endregion

		#region TreeView

		//private List<IOption> GetSpecificOptions(Type type)
		//{
		//    string typeName = type.Name;
		//    List<IOption> options = new List<IOption>();

		//    foreach (IOption option in ArchAngel.Interfaces.ProjectHelper.MyCurrentProject.Options)
		//    {
		//        if (option.IteratorName == typeName)
		//        {
		//            options.Add(option);
		//        }
		//    }
		//    return options;
		//}

		private void FillOptions()
		{
			var project = SharedData.CurrentProject;

			_optionsForTable = project.GetVirtualProperties(typeof(Table));
			_optionsForViews = project.GetVirtualProperties(typeof(View));
			_optionsForStoredProcedures = project.GetVirtualProperties(typeof(StoredProcedure));
			_optionsForScriptObjects = project.GetVirtualProperties(typeof(ScriptObject));
			_optionsForColumns = project.GetVirtualProperties(typeof(Column));
			_optionsForOneToOneRelationships = project.GetVirtualProperties(typeof(OneToOneRelationship));
			_optionsForOneToManyRelationship = project.GetVirtualProperties(typeof(OneToManyRelationship));
			_optionsForManyToOneRelationship = project.GetVirtualProperties(typeof(ManyToOneRelationship));
			_optionsForManyToManyRelationship =
				project.GetVirtualProperties(typeof(ManyToManyRelationship));
			_optionsForFilters = project.GetVirtualProperties(typeof(Filter));
			_optionsForIndexes = project.GetVirtualProperties(typeof(Index));
			_optionsForKeys = project.GetVirtualProperties(typeof(Key));
		}

		private void toolStripMenuItemTreeViewAdd_Click(object sender, EventArgs e)
		{
			Refresh();
			treeList.BeginUpdate();

			Node treeListNode = treeList.SelectedNode;

			Type type = treeListNode.Tag.GetType();

			if (type == typeof (Table[]) ||
			    type == typeof (View[]) ||
			    type == typeof (StoredProcedure[]))
			{
				Model.Database parent = (Model.Database) treeListNode.Parent.Tag;
				FormScriptObject form = new FormScriptObject((ScriptObject[]) treeListNode.Tag, parent);

				if (form.ShowDialog(ParentForm) == DialogResult.OK)
				{
					Interfaces.Events.SetCursor(Cursors.WaitCursor);
					parent.AddScriptObject(form.ScriptObject);

					Node newTreeListNode = AddNode(treeListNode, "", form.ScriptObject, Images.GreenBullet, true,
					                                                     form.ScriptObject.Enabled);
					GetTreeListNodeText(form.ScriptObject, newTreeListNode);
					SetupNewTreeListNode(newTreeListNode, form.ScriptObject);

					if (type == typeof (Table[]))
					{
						treeListNode.Tag = parent.Tables;
					}
					if (type == typeof (View[]))
					{
						treeListNode.Tag = parent.Views;
					}
					if (type == typeof (StoredProcedure[]))
					{
						treeListNode.Tag = parent.StoredProcedures;
					}
					ProcessTreeValidity(treeList.Nodes);
					Interfaces.Events.SetCursor(Cursors.Default);
				}
			}
			else if (type == typeof (Column[]))
			{
				ScriptObject parent = (ScriptObject) treeListNode.Parent.Tag;
				FormColumn form = new FormColumn(parent);

				if (form.ShowDialog(ParentForm) == DialogResult.OK)
				{
					Interfaces.Events.SetCursor(Cursors.WaitCursor);
					parent.AddColumn(form.Column);

					Node newTreeListNode = AddNode(treeListNode, "", form.Column, Images.GreenBullet, true,
					                                                     form.Column.Enabled);
					GetTreeListNodeText(form.Column, newTreeListNode);

					treeListNode.Tag = parent.Columns;
					ProcessTreeValidity(treeList.Nodes);
					Interfaces.Events.SetCursor(Cursors.Default);
				}
			}
            else if (type == typeof(Lookup[]))
            {
                Model.Database database = (Model.Database)treeListNode.Parent.Tag;
                FormLookup form = new FormLookup(database);

                if (form.ShowDialog(ParentForm) == DialogResult.OK)
                {
                    Interfaces.Events.SetCursor(Cursors.WaitCursor);
                    Node newTreeListNode = AddLookupNode(treeListNode, form.Lookup, Images.GreenBullet);
                    treeListNode.Tag = database.Lookups;
                    Interfaces.Events.SetCursor(Cursors.Default);
                    treeList.SelectNode(newTreeListNode, eTreeAction.Code);
                }
            }
			else if (type == typeof (OneToOneRelationship[]) ||
			         type == typeof (OneToManyRelationship[]) ||
			         type == typeof (ManyToOneRelationship[]) ||
			         type == typeof (ManyToManyRelationship[]))
			{
				ScriptObject parent = (ScriptObject) treeListNode.Parent.Parent.Tag;

				Type relationshipType = null;
				if (type == typeof (OneToOneRelationship[]))
				{
					relationshipType = typeof (OneToOneRelationship);
				}

				if (type == typeof (ManyToOneRelationship[]))
				{
					relationshipType = typeof (ManyToOneRelationship);
				}

				if (type == typeof (OneToManyRelationship[]))
				{
					relationshipType = typeof (OneToManyRelationship);
				}

				if (type == typeof (ManyToManyRelationship[]))
				{
					relationshipType = typeof (ManyToManyRelationship);
				}
				FormRelationship form = new FormRelationship(relationshipType, (ScriptObject) treeListNode.Parent.Parent.Tag,
				                                             ProviderInfo.TheBllDatabase.EnabledScriptObjects);

				if (form.ShowDialog(ParentForm) == DialogResult.OK)
				{
					Interfaces.Events.SetCursor(Cursors.WaitCursor);
					ScriptObject primaryScriptObject = form.PrimaryRelationship.Parent;
					primaryScriptObject.AddRelationship(form.PrimaryRelationship);

					ScriptObject foreignScriptObject = form.ForeignRelationship.Parent;
					foreignScriptObject.AddRelationship(form.ForeignRelationship);

					Node newTreeListNode = AddNode(treeListNode, "", form.PrimaryRelationship, Images.GreenBullet,
					                                                     true, form.PrimaryRelationship.Enabled);
					GetTreeListNodeText(form.PrimaryRelationship, newTreeListNode);

					if (type == typeof (OneToOneRelationship[]))
					{
						treeListNode.Tag = parent.OneToOneRelationships;
					}

					if (type == typeof (ManyToOneRelationship[]))
					{
						treeListNode.Tag = parent.ManyToOneRelationships;
					}

					if (type == typeof (OneToManyRelationship[]))
					{
						treeListNode.Tag = parent.OneToManyRelationships;
					}

					if (type == typeof (ManyToManyRelationship[]))
					{
						treeListNode.Tag = parent.ManyToManyRelationships;
					}

					LoadTreeListNode(foreignScriptObject);
					ProcessTreeValidity(treeList.Nodes);
					Interfaces.Events.SetCursor(Cursors.Default);
				}
				if (form.FilterWasCreated)
				{
					LoadTreeView();
				}
			}
			else if (type == typeof (Filter[]))
			{
				Model.Database database = (Model.Database) treeListNode.Parent.Parent.Parent.Tag;
				ScriptObject parent = (ScriptObject) treeListNode.Parent.Tag;

				FormFilter2 form;

				// Stored Procedure Filters can only be created from other Stored Procedures (Enabled or Disabled)
				if (parent.GetType() == typeof (StoredProcedure))
				{
					form = new FormFilter2(ParentForm, parent);
				}
				else
				{
					form = new FormFilter2(ParentForm, parent);
				}
				if (form.ShowDialog(ParentForm) == DialogResult.OK)
				{
					Interfaces.Events.SetCursor(Cursors.WaitCursor);
					parent.AddFilter(form.TheFilter);

					Node newTreeListNode = AddNode(treeListNode, "", form.TheFilter, Images.GreenBullet, true,
					                                                     form.TheFilter.Enabled);
					GetTreeListNodeText(form.TheFilter, newTreeListNode);

					treeListNode.Tag = parent.Filters;

					ProcessTreeValidity(treeList.Nodes);
					Interfaces.Events.SetCursor(Cursors.Default);
				}
			}
			else if (type == typeof (Index[]))
			{
				Table parent = (Table) treeListNode.Parent.Tag;
				FormIndex form = new FormIndex((Table) treeListNode.Parent.Tag);

				if (form.ShowDialog(ParentForm) == DialogResult.OK)
				{
					Interfaces.Events.SetCursor(Cursors.WaitCursor);
					parent.AddIndex(form.Index);

					Node newTreeListNode = AddNode(treeListNode, "", form.Index, Images.GreenBullet, true,
					                                                     form.Index.Enabled);
					GetTreeListNodeText(form.Index, newTreeListNode);

					treeListNode.Tag = parent.Indexes;

					ProcessTreeValidity(treeList.Nodes);
					Interfaces.Events.SetCursor(Cursors.Default);
				}
			}
			else if (type == typeof (Key[]))
			{
				Table parent = (Table) treeListNode.Parent.Tag;
				FormKey form = new FormKey((Table) treeListNode.Parent.Tag);

				if (form.ShowDialog(ParentForm) == DialogResult.OK)
				{
					Interfaces.Events.SetCursor(Cursors.WaitCursor);
					parent.AddKey(form.Key);

					Node newTreeListNode = AddNode(treeListNode, "", form.Key, Images.GreenBullet, true,
					                                                     form.Key.Enabled);
					GetTreeListNodeText(form.Key, newTreeListNode);

					treeListNode.Tag = parent.Keys;

					ProcessTreeValidity(treeList.Nodes);
					Interfaces.Events.SetCursor(Cursors.Default);
				}
			}
			else if (type == typeof (List<Association>))
			{
				ScriptObject parent = (ScriptObject) treeListNode.Parent.Tag;

				frmAssociationWizard.Association = new Association((ScriptObject) treeListNode.Parent.Tag);
				frmAssociationWizard.Association.Enabled = true;
				frmAssociationWizard form = new frmAssociationWizard();

				if (form.ShowDialog(ParentForm) == DialogResult.OK)
				{
					Interfaces.Events.SetCursor(Cursors.WaitCursor);
					parent.AddAssociation(frmAssociationWizard.Association);

					Node newTreeListNode = AddNode(treeListNode,
					                                                     frmAssociationWizard.Association.Name,
					                                                     frmAssociationWizard.Association,
					                                                     Images.GreenBullet, true,
					                                                     frmAssociationWizard.Association.Enabled);
					treeListNode.Tag = parent.Associations;

					ProcessTreeValidity(treeList.Nodes);
					Interfaces.Events.SetCursor(Cursors.Default);
					treeListNode.Expanded = true;
					treeList.SelectedNode = newTreeListNode;
				}
			}
			treeList.EndUpdate();
		}

		private void toolStripMenuItemTreeViewEdit_Click(object sender, EventArgs e)
		{
			Refresh();
			Node treeListNode = treeList.SelectedNode;
			Type type = treeListNode.Tag.GetType();

            if (type == typeof(Lookup))
            {
                try
                {
                    Interfaces.Events.SetCursor(Cursors.WaitCursor);
                    this.Refresh();
                    FormLookup form = new FormLookup((Lookup)treeListNode.Tag);

                    if (form.ShowDialog(ParentForm) == DialogResult.OK)
                    {
                        treeList.BeginUpdate();
                        treeListNode.Text = form.Lookup.Alias;
                        treeListNode.Tag = form.Lookup;
                        treeList.EndUpdate();
                        return;
                    }
                }
                finally
                {
                    Interfaces.Events.UnShadeMainForm();
                    Interfaces.Events.RaiseRefreshApplicationEvent();
                    Interfaces.Events.SetCursor(Cursors.Default);
                    this.Refresh();
                }
            }
			else if (type == typeof (Option))
			{
				IOption option = (IOption) treeListNode.Tag;
				IUserOption userOption;
				object parentObject = treeListNode.Parent.Parent.Tag;
				Type parentType = parentObject.GetType();

				Type scriptBaseInterface = typeof (IScriptBaseObject);

				if (scriptBaseInterface.IsInstanceOfType(parentObject))
				{
					IScriptBaseObject parentScriptObject = (IScriptBaseObject) parentObject;

					for (int i = 0; i < parentScriptObject.Ex.Count; i++)
					{
						if (parentScriptObject.Ex[i].Name == option.VariableName)
						{
							userOption = parentScriptObject.Ex[i];
							FormVirtualPropertyEdit form = new FormVirtualPropertyEdit(userOption,
							                                                                                                   parentScriptObject);

							if (form.ShowDialog(ParentForm) == DialogResult.OK)
							{
								Interfaces.Events.UnShadeMainForm();
								Interfaces.Events.RaiseRefreshApplicationEvent();
								Interfaces.Events.SetCursor(Cursors.WaitCursor);
								treeList.BeginUpdate();
								ProcessTreeValidity(treeList.Nodes);
								treeList.EndUpdate();
								Interfaces.Events.SetCursor(Cursors.Default);
							}
							break;
						}
					}
					return;
				}
				if (parentType == typeof (Column))
				{
					Column parentColumn = (Column) parentObject;

					for (int i = 0; i < parentColumn.Ex.Count; i++)
					{
						if (parentColumn.Ex[i].Name == option.VariableName)
						{
							userOption = parentColumn.Ex[i];
							FormVirtualPropertyEdit form = new FormVirtualPropertyEdit(userOption,
							                                                                                                   parentColumn);

							if (form.ShowDialog(ParentForm) == DialogResult.OK)
							{
								Interfaces.Events.UnShadeMainForm();
								Interfaces.Events.RaiseRefreshApplicationEvent();
								Interfaces.Events.SetCursor(Cursors.WaitCursor);
								treeList.BeginUpdate();
								parentColumn.Ex[i].Value = form.VirtualProperty.Value;
								treeList.SelectedNode.Cells[1].Text = form.VirtualProperty.Value.ToString();
								ProcessTreeValidity(treeList.Nodes);
								treeList.EndUpdate();
								Interfaces.Events.SetCursor(Cursors.Default);
							}
							break;
						}
					}
					return;
				}
			}
			if (type == typeof (Association))
			{
				frmAssociationWizard.Association = (Association) treeListNode.Tag;
				frmAssociationWizard form = new frmAssociationWizard();

				if (form.ShowDialog(ParentForm) == DialogResult.OK)
				{
					Interfaces.Events.UnShadeMainForm();
					Interfaces.Events.RaiseRefreshApplicationEvent();
					Interfaces.Events.SetCursor(Cursors.WaitCursor);
					treeList.BeginUpdate();
					treeListNode.Cells[1].Text = frmAssociationWizard.Association.Name;
					treeListNode.Tag = frmAssociationWizard.Association;
					SetNodeImage(treeListNode, Images.GreenBullet);

					foreach (Model.Database database in ProviderInfo.TheBllDatabase.Databases)
					{
						database.SnapshotMode = true;
					}
					ProcessTreeValidity(treeList.Nodes);
					treeList.EndUpdate();

					foreach (Model.Database database in ProviderInfo.TheBllDatabase.Databases)
					{
						database.SnapshotMode = false;
					}
					Interfaces.Events.RaiseIsDirtyEvent();
					Interfaces.Events.SetCursor(Cursors.Default);
					return;
				}
				return;
			}
			ScriptBase scriptBase = (ScriptBase) treeListNode.Tag;

			if (type == typeof (Table) ||
			    type == typeof (View) ||
			    type == typeof (StoredProcedure))
			{
				FormScriptObject form = new FormScriptObject((ScriptObject) scriptBase, (ScriptObject[]) treeListNode.Parent.Tag);

				if (form.ShowDialog(ParentForm) == DialogResult.OK)
				{
					Interfaces.Events.UnShadeMainForm();
					Interfaces.Events.RaiseRefreshApplicationEvent();
					Interfaces.Events.SetCursor(Cursors.WaitCursor);
					treeList.BeginUpdate();
					UpdateTreeListNodeText(treeListNode, form.ScriptObject);

					foreach (Model.Database database in ProviderInfo.TheBllDatabase.Databases)
					{
						database.SnapshotMode = true;
					}
					ProcessTreeValidity(treeList.Nodes);
					treeList.EndUpdate();

					foreach (Model.Database database in ProviderInfo.TheBllDatabase.Databases)
					{
						database.SnapshotMode = false;
					}
					Interfaces.Events.SetCursor(Cursors.Default);
					return;
				}
			}
			else if (type == typeof (Column))
			{
				FormColumn form = new FormColumn((Column) scriptBase);

				if (form.ShowDialog(ParentForm) == DialogResult.OK)
				{
					Interfaces.Events.UnShadeMainForm();
					Interfaces.Events.RaiseRefreshApplicationEvent();
					Interfaces.Events.SetCursor(Cursors.WaitCursor);
					treeList.BeginUpdate();
					UpdateTreeListNodeText(treeListNode, form.Column);
					ProcessTreeValidity(treeList.Nodes);
					treeList.EndUpdate();
					Interfaces.Events.SetCursor(Cursors.Default);
					return;
				}
			}
			else if (ModelTypes.MapColumn == type)
			{
				FormMapColumn form = new FormMapColumn((ScriptObject) treeListNode.Parent.Parent.Tag, (MapColumn) scriptBase);

				if (form.ShowDialog(ParentForm) == DialogResult.OK)
				{
					Interfaces.Events.UnShadeMainForm();
					Interfaces.Events.RaiseRefreshApplicationEvent();
					Interfaces.Events.SetCursor(Cursors.WaitCursor);
					treeList.BeginUpdate();

					UpdateTreeListNodeText(treeListNode, form.MapColumn);
					ProcessTreeValidity(treeList.Nodes);
					treeList.EndUpdate();
					Interfaces.Events.SetCursor(Cursors.Default);
					return;
				}
			}
			else if (type.BaseType == typeof (Relationship))
			{
				FormRelationship form = new FormRelationship((Relationship) scriptBase, ProviderInfo.TheBllDatabase.AllScriptObjects);

				if (form.ShowDialog(ParentForm) == DialogResult.OK)
				{
					Interfaces.Events.UnShadeMainForm();
					Interfaces.Events.RaiseRefreshApplicationEvent();
					Interfaces.Events.SetCursor(Cursors.WaitCursor);
					treeList.BeginUpdate();
					UpdateTreeListNodeText(treeListNode, form.PrimaryRelationship);
					ProcessTreeValidity(treeList.Nodes);
					treeList.EndUpdate();
					Interfaces.Events.SetCursor(Cursors.Default);
					return;
				}
			}
			else if (type == typeof (Filter))
			{
				Model.Database database = (Model.Database) treeListNode.Parent.Parent.Parent.Parent.Tag;
				Filter filter = (Filter) scriptBase;

				FormFilter2 form;
				// Stored Procedure Filters can only be created from other Stored Procedures (Enabled or Disabled)
				if (filter.Parent.GetType() == typeof (StoredProcedure))
				{
					form = new FormFilter2(ParentForm, filter);
				}
				else
				{
					form = new FormFilter2(ParentForm, filter);
				}
				if (form.ShowDialog(ParentForm) == DialogResult.OK)
				{
					Interfaces.Events.UnShadeMainForm();
					Interfaces.Events.RaiseRefreshApplicationEvent();
					Interfaces.Events.SetCursor(Cursors.WaitCursor);
					treeList.BeginUpdate();
					UpdateTreeListNodeText(treeListNode, form.TheFilter);
					ProcessTreeValidity(treeList.Nodes);
					treeList.EndUpdate();
					Interfaces.Events.SetCursor(Cursors.Default);
					return;
				}
			}
			else if (type == typeof (Index))
			{
				FormIndex form = new FormIndex((Index) scriptBase);

				if (form.ShowDialog(ParentForm) == DialogResult.OK)
				{
					Interfaces.Events.UnShadeMainForm();
					Interfaces.Events.RaiseRefreshApplicationEvent();
					Interfaces.Events.SetCursor(Cursors.WaitCursor);
					treeList.BeginUpdate();
					UpdateTreeListNodeText(treeListNode, form.Index);
					ProcessTreeValidity(treeList.Nodes);
					treeList.EndUpdate();
					Interfaces.Events.SetCursor(Cursors.Default);
					return;
				}
			}
			else if (type == typeof (Key))
			{
				FormKey form = new FormKey((Key) scriptBase);

				if (form.ShowDialog(ParentForm) == DialogResult.OK)
				{
					Interfaces.Events.UnShadeMainForm();
					Interfaces.Events.RaiseRefreshApplicationEvent();
					Interfaces.Events.SetCursor(Cursors.WaitCursor);
					treeList.BeginUpdate();
					UpdateTreeListNodeText(treeListNode, form.Key);
					ProcessTreeValidity(treeList.Nodes);
					treeList.EndUpdate();
					Interfaces.Events.SetCursor(Cursors.Default);
					return;
				}
			}
		}

		private void toolStripMenuItemTreeViewDelete_Click(object sender, EventArgs e)
		{
			Interfaces.Events.SetCursor(Cursors.WaitCursor);
			// TODO: ensure these objects are not used else where
			Node treeListNode = treeList.SelectedNode;

			if (typeof (Association).IsInstanceOfType(treeListNode.Tag))
			{
				Association association = (Association) treeListNode.Tag;
				ScriptObject scriptObject = (ScriptObject) treeListNode.Parent.Parent.Tag;
				scriptObject.RemoveAssociation(association);
			}
            else if (typeof(Lookup).IsInstanceOfType(treeListNode.Tag))
            {
                Lookup lookup = (Lookup)treeListNode.Tag;
                Model.Database database = (Model.Database)treeListNode.Parent.Parent.Tag;
                database.RemoveLookup(lookup);
            }
			else
			{
				Type type = treeListNode.Tag.GetType();

				if (type == typeof (Table) ||
				    type == typeof (View) ||
				    type == typeof (StoredProcedure))
				{
					ScriptObject scriptObject = (ScriptObject) treeListNode.Tag;
					Model.Database database = scriptObject.Database;
					database.RemoveScriptObject(scriptObject);
				}
				else if (type == typeof (Column))
				{
					Column column = (Column) treeListNode.Tag;
					ScriptObject scriptObject = column.Parent;
					scriptObject.RemoveColumn(column);
				}
					//else if (ModelTypes.MapColumn.IsInstanceOfType(type))
				else if (type == typeof (MapColumn))
				{
					MapColumn mapColumn = (MapColumn) treeListNode.Tag;
					ScriptObject scriptObject = mapColumn.Parent;
					scriptObject.RemoveColumn(mapColumn);
				}
				else if (type.BaseType == typeof (Relationship))
				{
					Relationship relationship = (Relationship) treeListNode.Tag;
					ScriptObject primaryScriptObject = relationship.Parent;
					primaryScriptObject.RemoveRelationship(relationship);

					ScriptObject foreignScriptObject = relationship.ForeignRelationship.Parent;
					foreignScriptObject.RemoveRelationship(relationship.ForeignRelationship);

					LoadTreeListNode(foreignScriptObject);
				}
				else if (type == typeof (Filter))
				{
					Filter filter = (Filter) treeListNode.Tag;
					ScriptObject scriptObject = filter.Parent;

					// Make sure that no Relationships are using this filter
					foreach (Relationship relationship in scriptObject.Relationships)
					{
						if (relationship.Filter == filter)
						{
							MessageBox.Show(
								string.Format("{0} [{1}] is using this filter, so it can't be deleted.", relationship.GetType().Name,
								              relationship.Alias), "Failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							return;
						}
					}
					scriptObject.RemoveFilter(filter);
				}
				else if (type == typeof (Index))
				{
					Index index = (Index) treeListNode.Tag;
					Table table = (Table) index.Parent;
					table.RemoveIndex(index);
				}
				else if (type == typeof (Key))
				{
					Key key = (Key) treeListNode.Tag;
					Table table = (Table) key.Parent;
					table.RemoveKey(key);
				}
				else
				{
					throw new NotImplementedException("Not handled yet: " + type.Name);
				}
			}
			treeListNode.Parent.Nodes.Remove(treeListNode);
			ProcessTreeValidity(treeList.Nodes);
			Interfaces.Events.SetCursor(Cursors.Default);
		}

		private void toolStripMenuItemTreeViewExecuteStoredProcedure_Click(object sender, EventArgs e)
		{
			treeList.BeginUpdate();
			Node treeListNode = treeList.SelectedNode;
			StoredProcedure storedProcedure = (StoredProcedure) treeListNode.Tag;
			ProviderInfo.TheBllDatabase.FillStoredProcedureColumns(storedProcedure);
			treeList.EndUpdate();
			LoadTreeView();
		}

		private void toolStripMenuItemTreeViewAddMapColumn_Click(object sender, EventArgs e)
		{
			treeList.BeginUpdate();

			Node treeListNode = treeList.SelectedNode;

			ScriptObject parent = (ScriptObject) treeListNode.Parent.Tag;
			FormMapColumn form = new FormMapColumn(parent);

			if (form.ShowDialog(ParentForm) == DialogResult.OK)
			{
				parent.AddColumn(form.MapColumn);
				Node newTreeListNode = AddNode(treeListNode, "", form.MapColumn, Images.GreenBullet, true,
				                                                     form.MapColumn.Enabled);
				treeListNode.Tag = parent.Columns;

				GetTreeListNodeText(form.MapColumn, newTreeListNode);
				ValidateNode(treeListNode.Parent);
			}
			treeList.EndUpdate();
		}

		private void SetupNewTreeListNode(Node treeListNode, ScriptObject scriptObject)
		{
			Node associationNodes = AddNode(treeListNode, "Associations", scriptObject.Associations, Images.Association, false,
			                                true);
			Node columnNodes = AddNode(treeListNode, "Columns", scriptObject.Columns, Images.Column, false, true);
			Node relationshipNodes = AddNode(treeListNode, "Relationships", scriptObject.Relationships, Images.Relationship,
			                                 false, true);
			Node oneToOneRelationshipNodes = AddNode(relationshipNodes, "One to One", scriptObject.OneToOneRelationships,
			                                         Images.Relationship, false, true);
			Node oneToManyRelationshipNodes = AddNode(relationshipNodes, "One to Many", scriptObject.OneToManyRelationships,
			                                          Images.Relationship, false, true);
			Node manyToOneRelationshipNodes = AddNode(relationshipNodes, "Many to One", scriptObject.ManyToOneRelationships,
			                                          Images.Relationship, false, true);
			Node manyToManyRelationshipNodes = AddNode(relationshipNodes, "Many to Many", scriptObject.ManyToManyRelationships,
			                                           Images.Relationship, false, true);
			Node filterNodes = AddNode(treeListNode, "Filters", scriptObject.Filters, Images.Filter, false, true);

			if (scriptObject.GetType() == typeof (Table))
			{
				Table table = (Table) scriptObject;
				Node indexNodes = AddNode(treeListNode, "Indexes", table.Indexes, Images.Index, false, true);
				Node keyNodes = AddNode(treeListNode, "Keys", table.Keys, Images.Key, false, true);
			}
		}

		private void MakeCrossThreadCall(MethodInvoker mi)
		{
			Invoke(mi);
		}

		/// <summary>
		/// Refreshes the database structure and keeps user-changes if specified.
		/// </summary>
		/// <returns>Returns a description of any errors that occurred or alerts that the user needs to be aware of.</returns>
		private void ReloadDatabase(bool fetchTables, bool fetchViews, bool fetchStoredProcedures)
		{
			try
			{
				StringBuilder sb = new StringBuilder(1000);
				MustSetDefaultValues = false;
				MakeCrossThreadCall(() => treeList.Nodes.Clear());
				Application.DoEvents();

				if (checkBoxKeepChanges.Checked)
				{
					for (int i = 0; i < ProviderInfo.TheBllDatabase.Databases.Length; i++)
					{
						Model.Database database = ProviderInfo.TheBllDatabase.Databases[i];
						bool isNewDatabase = (database.Tables.Length + database.Views.Length + database.StoredProcedures.Length) == 0;
						string errors;

						try
						{
							errors = ProviderInfo.TheBllDatabase.RefreshDatabase(database, i,
							                                                     SetupDatabase.GetListItems(
							                                                     	SetupDatabase.CurrentSetupDatabase.ListBoxTablePrefix),
							                                                     SetupDatabase.GetListItems(
							                                                     	SetupDatabase.CurrentSetupDatabase.ListBoxViewPrefix),
							                                                     SetupDatabase.GetListItems(
							                                                     	SetupDatabase.CurrentSetupDatabase.
							                                                     		ListBoxStoredProcedurePrefix), fetchTables, fetchViews,
							                                                     fetchStoredProcedures);
						}
						catch (OleDbException ex)
						{
							MessageBox.Show("There was a problem connecting to: " + database.Name + "\nError: " + ex.Message,
							                "Database Access Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
							return;
						}
						catch (Exception ex)
						{
							if (ex.Message.IndexOf("access denied") >= 0)
							{
								MessageBox.Show("You do not have access to this database: " + database.Name, "Database Access Error",
								                MessageBoxButtons.OK, MessageBoxIcon.Error);
								return;
							}
							MessageBox.Show("An error occurred with: " + database.Name + "\nError: " + ex.Message, "Database Access Error",
							                MessageBoxButtons.OK, MessageBoxIcon.Error);
							return;
						}
						if (errors.Length > 0)
						{
							sb.Append(errors);
						}

						if (isNewDatabase)
						{
							MustSetDefaultValues = true;
						}
					}
				}
				else // Overwrite all changes by doing a fresh reload of the database
				{
					for (int i = 0; i < ProviderInfo.TheBllDatabase.Databases.Length; i++)
					{
						Model.Database database = ProviderInfo.TheBllDatabase.Databases[i];

						try
						{
							ProviderInfo.TheBllDatabase.LoadNewDatabase(i, database.Name, database.DatabaseType, database.ConnectionString,
							                                            SetupDatabase.GetListItems(
							                                            	SetupDatabase.CurrentSetupDatabase.ListBoxTablePrefix),
							                                            SetupDatabase.GetListItems(
							                                            	SetupDatabase.CurrentSetupDatabase.ListBoxViewPrefix),
							                                            SetupDatabase.GetListItems(
							                                            	SetupDatabase.CurrentSetupDatabase.ListBoxStoredProcedurePrefix),
							                                            fetchTables, fetchViews, fetchStoredProcedures);
						}
						catch (OleDbException ex)
						{
							MessageBox.Show("There was a problem connecting to: " + database.Name + "\nError: " + ex.Message,
							                "Database Access Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
							return;
						}
						catch (Exception ex)
						{
							if (ex.Message.IndexOf("does not exist") >= 0)
							{
								MessageBox.Show("The following database could not be found: " + database.Name, "Database Access Error",
								                MessageBoxButtons.OK, MessageBoxIcon.Error);
								return;
							}
							if (ex.Message.IndexOf("access denied") >= 0)
							{
								MessageBox.Show("You do not have access to this database: " + database.Name, "Database Access Error",
								                MessageBoxButtons.OK, MessageBoxIcon.Error);
								return;
							}
							throw;
						}
						MustSetDefaultValues = true;
					}
					//CrossThreadHelper.CallCrossThreadMethod(this, "LoadTreeView", null);
					LoadTreeView();
					MakeCrossThreadCall(() => labelDatabaseFeedback.Text = "Load Database Complete");
				}
				if (sb.Length > 0)
				{
					// TODO: display errors to user, probably as a printable report or directly in the treeeview as error-nodes...
					//MessageBox.Show(sb.ToString(), "Issues while refreshing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			catch (Exception ex)
			{
				string message = ex.Message;

				if (ex.InnerException != null)
				{
					message += Environment.NewLine + ex.InnerException.Message;
				}
				Interfaces.Events.ReportError(ex);
				MakeCrossThreadCall(() => labelDatabaseFeedback.Text = message);
			}
		}

		private void buttonRefreshDatabase_Click(object sender, EventArgs e)
		{
			if (ProviderInfo.TheBllDatabase == null)
			{
				return;
			}
			RefreshDatabaseObjects();
		}

		#endregion

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			switch (CurrentBackgroundWorkerStep)
			{
				case BackGroundWorkerSteps.Step1:
					ReloadDatabase(RefreshTables, RefreshViews, RefreshStoredProcedures);
					break;
				case BackGroundWorkerSteps.Step2:
					break;
				case BackGroundWorkerSteps.Step3:
					LoadTreeView();

					if (InvokeRequired)
					{
						MakeCrossThreadCall(() => labelDatabaseFeedback.Text = "Refresh Database Complete");
					}
					else
					{
						labelDatabaseFeedback.Text = "Refresh Database Complete";
						Utility.UpdateMessagePanelStatus(this, "Refresh Database Complete");
					}
					break;
				default:
					throw new NotImplementedException("Not handled yet.");
			}
		}

		public void SetDefaultValues(object obj)
		{
			// TODO: why? Need to call DoEvents() because there is a problem calling this routine on a separate thread...
			Interfaces.Events.RaiseRefreshApplicationEvent();
			Type objType = obj.GetType();
			PropertyInfo[] props;

			if (ObjectProperties.ContainsKey(objType))
			{
				props = ObjectProperties[objType];
			}
			else
			{
				props = objType.GetProperties();
				ObjectProperties.Add(objType, props);
			}
			foreach (PropertyInfo property in props)
			{
				if (property.PropertyType.IsArray)
				{
					object[] objArray = (object[]) property.GetValue(obj, null);

					foreach (object subObj in objArray)
					{
						SetDefaultValues(subObj);
					}
				}
				else if (property.PropertyType.IsGenericType &&
				         property.PropertyType.GetGenericTypeDefinition().FullName == "System.Collections.Generic.List`1")
				{
					IList ilist = (IList) property.GetValue(obj, null);
					Type genericType = null;

					foreach (object o in ilist)
					{
						if (genericType == null)
						{
							genericType = o.GetType();
						}

						if (genericType == typeof (UserOption))
						{
							UserOption uo = (UserOption) o;
							uo.Value = SharedData.CurrentProject.GetVirtualPropertyDefaultValue(uo.Name, obj);
						}
						else
						{
							if (property.Name != "Errors" && property.Name != "OutParameters")
							{
								throw new NotImplementedException("Not coded yet.");
							}
						}
					}
				}
				else
				{
					MethodInfo method;
					string methodKeyName = objType.FullName + property.Name + "Default";

					if (DefaultValueMethods.ContainsKey(methodKeyName))
					{
						method = DefaultValueMethods[methodKeyName];
					}
					else
					{
						method = objType.GetMethod(property.Name + "Default", new[] {objType});
						DefaultValueMethods.Add(methodKeyName, method);
					}
					if (method != null)
					{
						object newValue = method.Invoke(obj, new[] {obj});
						property.SetValue(obj, newValue, null);
					}
				}
			}
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Error != null)
			{
				Interfaces.Events.ReportError(e.Error);
			}
			switch (CurrentBackgroundWorkerStep)
			{
				case BackGroundWorkerSteps.Step1:
					if (MustSetDefaultValues)
					{
						labelDatabaseFeedback.Text = "Setting default values...";
						Utility.UpdateMessagePanelStatus(this, "Setting default values...");

						foreach (Model.Database database in ProviderInfo.TheBllDatabase.Databases)
						{
							//ArchAngel.Interfaces.ProjectHelper.MyCurrentProject.FillVirtualProperties();
							SetDefaultValues(database);
						}
					}
					CurrentBackgroundWorkerStep = BackGroundWorkerSteps.Step3;
					backgroundWorker1.RunWorkerAsync();
					break;
				case BackGroundWorkerSteps.Step3:
					// Do nothing
					SharedData.CurrentProject.FillVirtualProperties();
					break;
				default:
					throw new NotImplementedException("Not handled yet.");
			}
			labelDatabaseFeedback.Text = "Finished";
			Utility.UpdateMessagePanelStatus(this, "Finished");
			Interfaces.Events.SetBusyPopulating(false);
			Interfaces.Events.RaiseIsDirtyEvent();
			Cursor = Cursors.Default;
            Utility.HideMessagePanel(this);
			Utility.UnShadeForm(this);
		}

		private void btnResetDefaultValues2_Click(object sender, EventArgs e)
		{
			if (ProviderInfo.TheBllDatabase == null)
			{
				return;
			}
			if (
				MessageBox.Show("Reset ALL default values and discard any changes you have made?", "Reset Default Values",
				                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
			{
				btnResetDefaultValues.Enabled = false;
				Interfaces.Events.SetCursor(Cursors.WaitCursor);
				Application.DoEvents();

				if (ProviderInfo.TheBllDatabase != null)
				{
					foreach (Model.Database database in ProviderInfo.TheBllDatabase.Databases)
					{
						MakeCrossThreadCall(() => labelDatabaseFeedback.Text = "Setting default values...");
						SetDefaultValues(database);
					}
				}
				LoadTreeView();
				MakeCrossThreadCall(() => labelDatabaseFeedback.Text = "");
				btnResetDefaultValues.Enabled = true;
				Interfaces.Events.SetCursor(Cursors.Default);
				MessageBox.Show("Finished");
			}
		}

		private void ProcessTreeValidity(NodeCollection nodesToProcess)
		{
			for (int i = nodesToProcess.Count - 1; i >= 0; i--)
			{
				Node node = nodesToProcess[i];

				if (GetNodeCheckState(node) == CheckState.Unchecked && node.ImageIndex == GreenBulletValue)
				{
					continue;
				}
				ValidateNode(node);
				ProcessTreeValidity(node.Nodes);
			}
		}

		private void SetNodeImage(Node node, Images image)
		{
			SetNodeImage(node, (int) image);
		}

		private void SetNodeImage(Node node, int imageIndex)
		{
			if (node.ImageIndex != imageIndex)
			{
				node.ImageIndex = imageIndex;
			}
		}

		private void ValidateNode(Node node)
		{
            if (node == null)
            {
                return;
            }
			string failReason = null;
			int imageIndex = node.ImageIndex;

			if (imageIndex == GreenBulletValue ||
			    imageIndex == RedBulletValue)
			{
				SetNodeImage(node, GreenBulletValue);
			}

			if (node.Tag.GetType().BaseType == ScriptBaseType)
			{
				if (!((ScriptBase) node.Tag).IsValid(false, out failReason))
				{
					if (node.ImageIndex != RedBulletValue)
					{
						SetNodeImage(node, RedBulletValue);
					}
					EnsureParentNodesAreRed(node);
				}
				else
				{
					if (node.ImageIndex != GreenBulletValue)
					{
						SetNodeImage(node, GreenBulletValue);
					}
				}
			}
			else if (node.Tag.GetType().BaseType == ScriptObjectType)
			{
				if (!((ScriptObject) node.Tag).IsValid(false, out failReason))
				{
					if (node.ImageIndex != RedBulletValue)
					{
						SetNodeImage(node, RedBulletValue);
					}
					EnsureParentNodesAreRed(node);
				}
				else
				{
					if (node.ImageIndex != GreenBulletValue)
					{
						SetNodeImage(node, GreenBulletValue);
					}
				}
			}
			else if (node.Tag.GetType().BaseType == RelationshipType)
			{
				if (!((Relationship) node.Tag).IsValid(false, out failReason))
				{
					if (node.ImageIndex != RedBulletValue)
					{
						SetNodeImage(node, RedBulletValue);
					}
					EnsureParentNodesAreRed(node);
				}
				else
				{
					if (node.ImageIndex != GreenBulletValue)
					{
						SetNodeImage(node, GreenBulletValue);
					}
				}
			}
			else if (node.Tag.GetType() == DatabaseType)
			{
				if (!((Model.Database) node.Tag).IsValid(false, out failReason))
				{
					if (node.ImageIndex != RedDatabaseValue)
					{
						SetNodeImage(node, RedDatabaseValue);
					}
					EnsureParentNodesAreRed(node);
				}
				else
				{
					if (node.ImageIndex != (int) Images.Database)
					{
						SetNodeImage(node, Images.Database);
					}
				}
			}
			else if (node.Tag.GetType().Name == "Column[]")
			{
				SetNodeImage(node, Images.Column);
			}
			else if (node.Tag.GetType().Name == "Filter[]")
			{
				SetNodeImage(node, Images.Filter);
			}
			else if (node.Tag.GetType().Name == "Index[]")
			{
				SetNodeImage(node, Images.Index);
			}
			else if (node.Tag.GetType().Name == "Key[]")
			{
				SetNodeImage(node, Images.Key);
			}
			else if (node.Tag.GetType().Name == "View[]")
			{
				SetNodeImage(node, Images.Table);
			}
			else if (node.Tag.GetType().Name == "StoredProcedure[]")
			{
				SetNodeImage(node, Images.StoredProcedure);
			}
			else if (node.Tag.GetType().Name == "Relationship[]" ||
			         node.Tag.GetType().Name == "OneToOneRelationship[]" ||
			         node.Tag.GetType().Name == "OneToManyRelationship[]" ||
			         node.Tag.GetType().Name == "ManyToManyRelationship[]" ||
			         node.Tag.GetType().Name == "ManyToOneRelationship[]")
			{
				SetNodeImage(node, Images.Relationship);
			}
			else if (node.Tag.GetType().Name == "Table[]")
			{
				SetNodeImage(node, Images.Table);
			}
			if (!string.IsNullOrEmpty(failReason))
			{
				node.Cells[2].Text = failReason;
			}
			SetNodeStyle(node);
		}

		private void EnsureParentNodesAreRed(Node node)
		{
			Node parentNode = node.Parent;

			while (parentNode != null)
			{
				switch (parentNode.ImageIndex)
				{
					case GreenBulletValue:
						SetNodeImage(parentNode, RedBulletValue);
						break;
					case (int) Images.Column:
						SetNodeImage(parentNode, Images.Red_Column);
						break;
					case (int) Images.Database:
						SetNodeImage(parentNode, Images.Red_Database);
						break;
					case (int) Images.Filter:
						SetNodeImage(parentNode, Images.Red_Filter);
						break;
					case (int) Images.Index:
						SetNodeImage(parentNode, Images.Red_Index);
						break;
					case (int) Images.Key:
						SetNodeImage(parentNode, Images.Red_Key);
						break;
					case (int) Images.Option:
						SetNodeImage(parentNode, Images.Red_Option);
						break;
					case (int) Images.Relationship:
						SetNodeImage(parentNode, Images.Red_Relationship);
						break;
					case (int) Images.StoredProcedure:
						SetNodeImage(parentNode, Images.Red_StoredProcedure);
						break;
					case (int) Images.Table:
						SetNodeImage(parentNode, Images.Red_Table);
						break;
				}
				SetNodeStyle(parentNode);
				parentNode = parentNode.Parent;
			}
		}

		private void chkHideNonSelectedNodes_CheckedChanged(object sender, EventArgs e)
		{
			HideUnselectedNodes();
		}

		private void HideUnselectedNodes()
		{
			Cursor = Cursors.WaitCursor;
			treeList.BeginUpdate();

			try
			{
				foreach (Node node in treeList.Nodes)
				{
					ProcessNodeVisibility(node);
				}
			}
			finally
			{
				treeList.EndUpdate();
				treeList.Invalidate();
				treeList.Refresh();
				Cursor = Cursors.Default;
			}
		}

		private void ProcessNodeVisibility(Node node)
		{
			if (chkHideNonSelectedNodes.Checked)
			{
				if (node.CheckBoxVisible &&
				    GetNodeCheckState(node) == CheckState.Unchecked)
				{
					node.Visible = false;
					return;
				}
			}
			if (!node.Visible)
			{
				node.Visible = true;
			}
			foreach (Node childNode in node.Nodes)
			{
				ProcessNodeVisibility(childNode);
			}
		}

		private void treeList_NodeClick(object sender, TreeNodeMouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				HideAllMenuItems(contextMenuStripTreeView);
				Node treeListNode = e.Node;
				treeList.SelectedNode = treeListNode;
				Type tagBaseType = BLL.Helper.GetBaseType(treeListNode.Tag);
				Type tagType = treeListNode.Tag.GetType();

				if (treeListNode == null ||
				    treeListNode.Tag == null ||
				    tagType == typeof (Model.Database) ||
				    treeListNode.Text == "Tables" ||
				    treeListNode.Text == "Views" ||
				    treeListNode.Text == "Stored Procedures")
				{
					refreshToolStripMenuItem.Visible = true;
				}
				if (treeListNode == null ||
				    treeListNode.Tag == null ||
				    tagType == typeof (Model.Database) ||
				    treeListNode.Text == "Relationships" ||
				    treeListNode.Text == "Parameters" ||
				    treeListNode.Parent.Text == "Parameters")
				{
					return;
				}
				else if (tagBaseType == typeof (Array))
				{
					toolStripMenuItemTreeViewAdd.Visible = true;
					toolStripMenuItemTreeViewEdit.Visible = true;
					toolStripMenuItemTreeViewDelete.Visible = true;

					toolStripMenuItemTreeViewAdd.Enabled = true;
					toolStripMenuItemTreeViewEdit.Enabled = false;
					toolStripMenuItemTreeViewDelete.Enabled = false;
				}
				else if (tagBaseType == typeof (ScriptBase))
				{
					toolStripMenuItemTreeViewAdd.Visible = true;
					toolStripMenuItemTreeViewEdit.Visible = true;
					toolStripMenuItemTreeViewDelete.Visible = true;

					toolStripMenuItemTreeViewAdd.Enabled = false;
					toolStripMenuItemTreeViewEdit.Enabled = true;

					ScriptBase scriptBase = (ScriptBase) treeListNode.Tag;

					if (ModelTypes.MapColumn.IsInstanceOfType(scriptBase))
					{
						toolStripMenuItemTreeViewDelete.Enabled = true;
					}
					else
					{
						toolStripMenuItemTreeViewDelete.Enabled = scriptBase.IsUserDefined;
					}
				}
				else if (tagType == typeof (List<Association>))
				{
					toolStripMenuItemTreeViewAdd.Visible = true;
					toolStripMenuItemTreeViewEdit.Visible = true;
					toolStripMenuItemTreeViewDelete.Visible = true;

					toolStripMenuItemTreeViewAdd.Enabled = true;
					toolStripMenuItemTreeViewEdit.Enabled = false;
					toolStripMenuItemTreeViewDelete.Enabled = false;
				}
				// Map Columns
				if (tagType == typeof (Column[]))
				{
					toolStripMenuItemTreeViewAddMapColumn.Enabled = true;
					toolStripMenuItemTreeViewAddMapColumn.Visible = true;
				}
					// Stored Procedure Execute
				else if (tagType == typeof (StoredProcedure))
				{
					toolStripMenuItemTreeViewExecuteStoredProcedure.Enabled = true;
					toolStripMenuItemTreeViewExecuteStoredProcedure.Visible = true;
				}
				else if (tagType == typeof (Option))
				{
					toolStripMenuItemTreeViewEdit.Enabled = true;
					toolStripMenuItemTreeViewEdit.Visible = true;
				}
				else if (tagType == typeof (Association))
				{
					toolStripMenuItemTreeViewEdit.Enabled = true;
					toolStripMenuItemTreeViewEdit.Visible = true;
					toolStripMenuItemTreeViewDelete.Enabled = true;
					toolStripMenuItemTreeViewDelete.Visible = true;
				}
				contextMenuStripTreeView.Show(treeList, new Point(e.X, e.Y));
			}
		}

		private void treeList_NodeDoubleClick(object sender, TreeNodeMouseEventArgs e)
		{
			Node treeListNode = e.Node;

			if (treeListNode == null ||
			    treeListNode.Tag == null ||
			    treeListNode.Text == "Relationships")
			{
				return;
			}
			if (BLL.Helper.GetBaseType(treeListNode.Tag) == typeof (Array))
			{
				toolStripMenuItemTreeViewAdd_Click(null, null);
			}
			if (BLL.Helper.GetBaseType(treeListNode.Tag) == typeof (ScriptBase))
			{
				toolStripMenuItemTreeViewEdit_Click(null, null);
			}
		}

		private void treeList_AfterCheck(object sender, AdvTreeCellEventArgs e)
		{
            if (e.Cell.Parent.Tag is ScriptBase)
            {
                ScriptBase scriptBase = (ScriptBase)e.Cell.Parent.Tag;
                scriptBase.Enabled = e.Cell.Parent.Checked;
            }
            else if (e.Cell.Parent.Tag.GetType().GetProperty("Enabled") != null)
            {
                PropertyInfo prop = e.Cell.Parent.Tag.GetType().GetProperty("Enabled");
                prop.GetSetMethod().Invoke(e.Cell.Parent.Tag, new object[] { false });
            }
            ValidateNode(e.Cell.Parent);
		}

		private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ProviderInfo.TheBllDatabase == null)
			{
				return;
			}
			RefreshTables = treeList.SelectedNode.Text == "Tables";
			RefreshStoredProcedures = treeList.SelectedNode.Text == "Stored Procedures";
			RefreshViews = treeList.SelectedNode.Text == "Views";

			RefreshDatabaseObjects();
		}

		private void RefreshDatabaseObjects()
		{
			if (!backgroundWorker1.IsBusy)
			{
				Utility.DisplayMessagePanel(this, "Querying database...");
				Interfaces.Events.RaiseCancelGenerationEvent();
				CurrentBackgroundWorkerStep = BackGroundWorkerSteps.Step1;
				Interfaces.Events.SetBusyPopulating(true);
				Cursor = Cursors.WaitCursor;
				labelDatabaseFeedback.Text = "Connecting to database...";
				Utility.UpdateMessagePanelStatus(this, "Connecting to database...");
				CurrentBackgroundWorkerStep = BackGroundWorkerSteps.Step1;
				backgroundWorker1.RunWorkerAsync();
			}
		}

		private void treeList_BeforeExpand(object sender, AdvTreeNodeCancelEventArgs e)
		{
			if (e.Node.Nodes.Count == 1 && e.Node.Nodes[0].Tag.GetType() == typeof (string) &&
			    (string) e.Node.Nodes[0].Tag == "LAZY")
			{
				LazyLoadNodes(e.Node);
			}
		}

		private void LazyLoadNodes(Node parentNode)
		{
			treeList.BeginUpdate();

			try
			{
				parentNode.Nodes.Clear();
				string typeName = parentNode.Tag.GetType().ToString().ToLower();

				switch (typeName)
				{
					case "archangel.providers.database.model.column[]":
						foreach (Column column in (Column[]) parentNode.Tag)
						{
							Node columnNode = AddNode(parentNode, "", column, Images.GreenBullet, true, column.Enabled);
							GetTreeListNodeText(column, columnNode);
							AddOptionNodes(columnNode, column);
						}
						break;
					case "archangel.providers.database.model.onetoonerelationship[]":
						foreach (OneToOneRelationship oneToOneRelationship in (OneToOneRelationship[]) parentNode.Tag)
						{
							Node oneToOneRelationshipNode = AddNode(parentNode, "", oneToOneRelationship, Images.GreenBullet, true,
							                                        oneToOneRelationship.Enabled);
							GetTreeListNodeText(oneToOneRelationship, oneToOneRelationshipNode);
							AddOptionNodes(oneToOneRelationshipNode, oneToOneRelationship);
						}
						break;
					case "archangel.providers.database.model.onetomanyrelationship[]":
						foreach (OneToManyRelationship oneToManyRelationship in (OneToManyRelationship[]) parentNode.Tag)
						{
							Node oneToManyRelationshipNode = AddNode(parentNode, "", oneToManyRelationship,
							                                                               Images.GreenBullet, true,
							                                                               oneToManyRelationship.Enabled);
							GetTreeListNodeText(oneToManyRelationship, oneToManyRelationshipNode);
							AddOptionNodes(oneToManyRelationshipNode, oneToManyRelationship);
						}
						break;
					case "archangel.providers.database.model.manytoonerelationship[]":
						foreach (ManyToOneRelationship manyToOneRelationship in (ManyToOneRelationship[]) parentNode.Tag)
						{
							Node manyToOneRelationshipNode = AddNode(parentNode, "", manyToOneRelationship,
							                                                               Images.GreenBullet, true,
							                                                               manyToOneRelationship.Enabled);
							GetTreeListNodeText(manyToOneRelationship, manyToOneRelationshipNode);
							AddOptionNodes(manyToOneRelationshipNode, manyToOneRelationship);
						}
						break;
					case "archangel.providers.database.model.manytomanyrelationship[]":
						foreach (ManyToManyRelationship manyToManyRelationship in (ManyToManyRelationship[]) parentNode.Tag)
						{
							Node manyToManyRelationshipNode = AddNode(parentNode, "", manyToManyRelationship, Images.GreenBullet, true,
							                                          manyToManyRelationship.Enabled);
							GetTreeListNodeText(manyToManyRelationship, manyToManyRelationshipNode);
							AddOptionNodes(manyToManyRelationshipNode, manyToManyRelationship);
						}
						break;
					case "archangel.providers.database.model.filter[]":
						foreach (Filter filter in (Filter[]) parentNode.Tag)
						{
							Node filterNode = AddNode(parentNode, "", filter, Images.GreenBullet, true, filter.Enabled);
							GetTreeListNodeText(filter, filterNode);
							AddOptionNodes(filterNode, filter);
						}
						break;
					case "system.collections.generic.list`1[archangel.providers.database.model.association]":
						foreach (Association association in (List<Association>) parentNode.Tag)
						{
							Node associationNode = AddNode(parentNode,
							                                                     new[]
							                                                     	{association.Name, association.AssociatedObject.Alias},
							                                                     association, Images.GreenBullet, true, association.Enabled);
						}
						break;
					case "archangel.providers.database.model.index[]":
						foreach (Index index in (Index[]) parentNode.Tag)
						{
							Node indexNode = AddNode(parentNode, "", index, Images.GreenBullet, true, index.Enabled);
							GetTreeListNodeText(index, indexNode);
						}
						break;
					case "archangel.providers.database.model.key[]":
						foreach (Key key in (Key[]) parentNode.Tag)
						{
							Node keyNode = AddNode(parentNode, "", key, Images.GreenBullet, true, key.Enabled);
							GetTreeListNodeText(key, keyNode);
						}
						break;
					case "archangel.providers.database.model.storedprocedure+parameter[]":
						foreach (StoredProcedure.Parameter parameter in (StoredProcedure.Parameter[]) parentNode.Tag)
						{
							AddNode(parentNode, parameter.Name, parameter, Images.GreenBullet, true, parameter.Enabled);
						}
						break;
					case "system.collections.generic.list`1[archangel.interfaces.itemplate.ioption]":
						foreach (IOption option in (List<IOption>) parentNode.Tag)
						{
							IScriptBaseObject iteratorObject = (IScriptBaseObject) parentNode.Parent.Tag;

							if (SharedData.CurrentProject.DisplayOptionToUser(option, iteratorObject))
							{
								AddNode(parentNode, new[] {option.Text, option.Description}, option, Images.GreenBullet, false,
								        option.Enabled);
							}
						}
						break;
					default:
						throw new NotImplementedException("Not coded yet: " + typeName);
				}
                parentNode.Nodes.Sort();
			}
			finally
			{
				treeList.EndUpdate();
			}
		}
	}
}
