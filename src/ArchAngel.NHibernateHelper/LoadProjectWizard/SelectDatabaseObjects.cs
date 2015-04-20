using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ArchAngel.Interfaces.Wizards.NewProject;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;

namespace ArchAngel.NHibernateHelper.LoadProjectWizard
{
	public partial class SelectDatabaseObjects : UserControl, INewProjectScreen
	{
		private static readonly string ScreenDataKey = typeof(LoadExistingDatabase).FullName;

		private class ScreenData
		{
			public IDatabaseLoader Loader;

			public ScreenData(IDatabaseLoader loader)
			{
				Loader = loader;
			}
		}

		public SelectDatabaseObjects()
		{
			InitializeComponent();
		}

		public IFormNewProject NewProjectForm { get; set; }
		public IDatabaseLoader Loader;

		private void buttonBack_Click(object sender, EventArgs e)
		{
			NewProjectForm.LoadScreen(typeof(LoadExistingDatabase));
		}

		private void buttonFinish_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;

			try
			{
				LoadExistingDatabaseInfo info = (LoadExistingDatabaseInfo)NewProjectForm.NewProjectInformation;
				List<SchemaData> tableSchemas = new List<SchemaData>();
				List<SchemaData> viewSchemas = new List<SchemaData>();

				foreach (DevComponents.AdvTree.Node schemaNode in advTree1.Nodes)
				{
					List<string> selectedTableNames = new List<string>();
					List<string> selectedViewNames = new List<string>();

					foreach (DevComponents.AdvTree.Node collectionNode in schemaNode.Nodes)
					{
						switch (collectionNode.Text)
						{
							case "Tables":
								foreach (DevComponents.AdvTree.Node tableNode in collectionNode.Nodes)
									if (tableNode.Checked)
										selectedTableNames.Add(tableNode.Text);

								break;
							case "Views":
								foreach (DevComponents.AdvTree.Node viewNode in collectionNode.Nodes)
									if (viewNode.Checked)
										selectedViewNames.Add(viewNode.Text);

								break;
							default:
								throw new NotImplementedException("Collection node not handled yet: " + collectionNode.Text);
						}
					}
					// TODO: Get selected View names...
					if (selectedTableNames.Count > 0 || selectedViewNames.Count > 0)
					{
						SchemaData schema = new SchemaData(schemaNode.Text, selectedTableNames, selectedViewNames);
						tableSchemas.Add(schema);
					}
				}
				info.DatabaseLoader.DatabaseObjectsToFetch = tableSchemas;

				NewProjectForm.UserChosenAction = NewProjectFormActions.NewProject;
				// NewProjectForm.NewProjectInformation = new LoadExistingDatabaseInfo { DatabaseLoader = DatabasePresenter.CreateDatabaseLoader(ucDatabaseInformation1) };

				NewProjectForm.LoadScreen(typeof(Prefixes));
			}
			finally
			{
				Cursor = Cursors.Default;
			}
		}

		public void Setup()
		{
			LoadExistingDatabaseInfo info = (LoadExistingDatabaseInfo)NewProjectForm.NewProjectInformation;
			//ArchAngel.Providers.EntityModel.UI.Presenters.ServerAndDatabaseHelper DatabaseHelper = new ServerAndDatabaseHelper();
			//List<string> databases = DatabaseHelper.GetDatabaseNamesForServer(info.ConnStringHelper).OrderBy(f => f).ToList();

			//comboBoxDatabases.Items.Clear();

			//foreach (var db in 

			List<SchemaData> schemas = info.DatabaseLoader.GetSchemaObjects();

			advTree1.BeginUpdate();
			advTree1.Nodes.Clear();

			foreach (var schema in schemas)
			{
				DevComponents.AdvTree.Node schemaNode = new DevComponents.AdvTree.Node(schema.Name);
				schemaNode.CheckBoxVisible = true;
				schemaNode.Image = Images.Schema16;
				schemaNode.Checked = schemas.Count == 1;

				#region Tables
				DevComponents.AdvTree.Node tableCollectionNode = new DevComponents.AdvTree.Node("Tables")
				{
					CheckBoxVisible = true,
					Image = Images.Table16,
					Checked = schemas.Count == 1
				};
				schemaNode.Nodes.Add(tableCollectionNode);

				foreach (var tableName in schema.TableNames)
				{
					bool check = schemas.Count == 1 && tableName != "HIBERNATE_UNIQUE_KEY";

					DevComponents.AdvTree.Node tableNode = new DevComponents.AdvTree.Node(tableName)
					{
						CheckBoxVisible = true,
						Checked = check
					};
					tableCollectionNode.Nodes.Add(tableNode);
				}

				#endregion

				#region Views
				DevComponents.AdvTree.Node viewCollectionNode = new DevComponents.AdvTree.Node("Views")
					{
						CheckBoxVisible = true,
						Image = Images.Table16,
						Checked = false
					};
				schemaNode.Nodes.Add(viewCollectionNode);

				foreach (var viewName in schema.ViewNames)
				{
					DevComponents.AdvTree.Node viewNode = new DevComponents.AdvTree.Node(viewName)
					{
						CheckBoxVisible = true,
						Checked = false
					};
					viewCollectionNode.Nodes.Add(viewNode);
				}
				#endregion

				advTree1.Nodes.Add(schemaNode);
			}
			if (schemas.Count > 1)
				advTree1.CollapseAll();
			else
				advTree1.ExpandAll();

			advTree1.EndUpdate();
		}

		private bool? BusyInAfterCheck = null;

		private void advTree1_AfterCheck(object sender, DevComponents.AdvTree.AdvTreeCellEventArgs e)
		{
			bool firstEntry = BusyInAfterCheck == null;

			if (firstEntry)
			{
				BusyInAfterCheck = true;

				if (e.Action != DevComponents.AdvTree.eTreeAction.Code && BusyInAfterNodeSelect)
					e.Cell.Parent.Checked = !e.Cell.Parent.Checked;
			}
			if (e.Cell.Parent.Nodes.Count > 0)
			{
				foreach (DevComponents.AdvTree.Node node in e.Cell.Parent.Nodes)
					node.Checked = e.Cell.Checked;
			}
			else
			{
				foreach (DevComponents.AdvTree.Node node in advTree1.SelectedNodes)
					node.Checked = e.Cell.Checked;
			}
			if (firstEntry)
				BusyInAfterCheck = null;
		}

		bool BusyInAfterNodeSelect = false;

		private void advTree1_AfterNodeSelect(object sender, DevComponents.AdvTree.AdvTreeNodeEventArgs e)
		{
			BusyInAfterNodeSelect = true;
			//if (e.Node.Checked == SelectedNodeWasCheckedBeforeSelection &&
			//    advTree1.SelectedNodes.Count > 1)
			//{
			//    advTree1.SelectedNodes.Clear();
			//    advTree1.SelectedNode = e.Node;
			//    return;
			//}
			if (e.Node.Checked != SelectedNodeWasCheckedBeforeSelection &&
				advTree1.SelectedNodes.Count > 1)
			{
				if (e.Node.IsSelected)
				{
					foreach (DevComponents.AdvTree.Node node in advTree1.SelectedNodes)
						node.Checked = true;
				}
				else
					foreach (DevComponents.AdvTree.Node node in advTree1.SelectedNodes)
						node.Checked = false;
			}
			else if (e.Node.Checked == SelectedNodeWasCheckedBeforeSelection)
				e.Node.Checked = !e.Node.Checked;

			BusyInAfterNodeSelect = false;
		}

		private bool SelectedNodeWasCheckedBeforeSelection = false;

		private void advTree1_BeforeNodeSelect(object sender, DevComponents.AdvTree.AdvTreeNodeCancelEventArgs e)
		{
			if (e.Node != null)
				SelectedNodeWasCheckedBeforeSelection = e.Node.Checked;
		}
	}
}
