using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;
using DevComponents.AdvTree;

namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
	public partial class FormSelectSchemas : Form
	{
		private List<string> ExistingSchemas = new List<string>();

		public FormSelectSchemas(List<string> existingSchemas)
		{
			InitializeComponent();

			ExistingSchemas = existingSchemas;
			Populate();
		}

		private void Populate()
		{
			nodeNewTables.Nodes.Clear();
			advTreeTables.BeginUpdate();

			try
			{
				// Fetch all schemas for
				IDatabaseLoader loader = FormDatabase.Instance.CreateDatabaseLoader();
				IDatabase newDb = loader.LoadDatabase(loader.DatabaseObjectsToFetch, null);

				foreach (var schema in newDb.Tables.Select(s => s.Schema).Distinct().Where(s => !ExistingSchemas.Contains(s)).OrderBy(s => s))
				{
					Node schemaNode = new Node(schema)
						{
							CheckBoxVisible = true,
							Checked = false
						};
					nodeNewTables.Nodes.Add(schemaNode);

					foreach (var table in newDb.Tables.Where(t => t.Schema == schema).Select(t => t.Name).OrderBy(t => t))
					{
						Node tableNode = new Node(table)
						{
							CheckBoxVisible = true,
							Checked = false
						};
						schemaNode.Nodes.Add(tableNode);
					}
				}
			}
			finally
			{
				advTreeTables.EndUpdate();
			}
		}
	}
}
