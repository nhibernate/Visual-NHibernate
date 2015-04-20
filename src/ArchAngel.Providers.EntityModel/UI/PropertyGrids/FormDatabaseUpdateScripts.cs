using System;
using System.Text;
using System.Windows.Forms;

namespace ArchAngel.Providers.EntityModel.UI.PropertyGrids
{
	public partial class FormDatabaseUpdateScripts : Form
	{
		private bool BusyPopulating = false;
		private ArchAngel.Interfaces.Scripting.DatabaseChanges.IDatabase _Database;

		public FormDatabaseUpdateScripts()
		{
			InitializeComponent();
		}

		public ArchAngel.Interfaces.Scripting.DatabaseChanges.IDatabase Database
		{
			get { return _Database; }
			set
			{
				if (_Database != value)
				{
					_Database = value;

					if (_Database != null)
					{
						Populate();
					}
				}
			}
		}

		private void Populate()
		{
			BusyPopulating = true;
			comboBoxTables.Items.Clear();
			comboBoxTables.Sorted = false;
			comboBoxTables.DisplayMember = "Name";

			foreach (var table in Database.ChangedTables)
				comboBoxTables.Items.Add(table);

			foreach (var table in Database.NewTables)
				comboBoxTables.Items.Add(table);

			foreach (var table in Database.RemovedTables)
				comboBoxTables.Items.Add(table);

			comboBoxTables.Sorted = true;
			comboBoxTables.Sorted = false;
			comboBoxTables.Items.Insert(0, "All");

			switch (Database.DatabaseType)
			{
				case Interfaces.ProjectOptions.TypeMappings.Utility.DatabaseTypes.SqlServer:
					comboBoxDatabases.Text = "SQL Server 2005/2008/Azure";
					break;
				case Interfaces.ProjectOptions.TypeMappings.Utility.DatabaseTypes.Oracle:
					comboBoxDatabases.Text = "Oracle";
					break;
				case Interfaces.ProjectOptions.TypeMappings.Utility.DatabaseTypes.MySql:
					comboBoxDatabases.Text = "MySQL";
					break;
				case Interfaces.ProjectOptions.TypeMappings.Utility.DatabaseTypes.PostgreSql:
					comboBoxDatabases.Text = "PostgreSQL";
					break;
				case Interfaces.ProjectOptions.TypeMappings.Utility.DatabaseTypes.Firebird:
					comboBoxDatabases.Text = "Firebird";
					break;
				default:
					throw new NotImplementedException("DatabaseType not supported yet: " + Database.DatabaseType.ToString());
			}
			BusyPopulating = false;

			comboBoxTables.SelectedIndex = 0;
			comboBoxDatabases.SelectedIndex = 0;
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void comboBoxTables_SelectedIndexChanged(object sender, EventArgs e)
		{
			syntaxEditorScript.Text = GetScriptForAll();
		}

		private string GetScriptForAll()
		{
			if (BusyPopulating)
				return "";

			ArchAngel.Interfaces.ProjectOptions.DatabaseScripts.Scripts.ScriptRunnerContainer scriptContainer = null;
			string db = comboBoxDatabases.SelectedItem.ToString();

			if (db == "SQL Server 2005/2008/Azure")
			{
				scriptContainer = ArchAngel.Interfaces.ProjectOptions.DatabaseScripts.Scripts.ContainerForSqlServer;
				Slyce.Common.SyntaxEditorHelper.SetupEditorTemplateAndScriptLanguages(syntaxEditorScript, Slyce.Common.TemplateContentLanguage.Sql, Slyce.Common.SyntaxEditorHelper.ScriptLanguageTypes.CSharp, @"<%", @"%>");
			}
			else if (db == "Oracle")
			{
				scriptContainer = ArchAngel.Interfaces.ProjectOptions.DatabaseScripts.Scripts.ContainerForOracle;
				Slyce.Common.SyntaxEditorHelper.SetupEditorTemplateAndScriptLanguages(syntaxEditorScript, Slyce.Common.TemplateContentLanguage.Sql, Slyce.Common.SyntaxEditorHelper.ScriptLanguageTypes.CSharp, @"<%", @"%>");
			}
			else if (db == "MySQL")
			{
				scriptContainer = ArchAngel.Interfaces.ProjectOptions.DatabaseScripts.Scripts.ContainerForMySql;
				Slyce.Common.SyntaxEditorHelper.SetupEditorTemplateAndScriptLanguages(syntaxEditorScript, Slyce.Common.TemplateContentLanguage.Sql, Slyce.Common.SyntaxEditorHelper.ScriptLanguageTypes.CSharp, @"<%", @"%>");
			}
			else if (db == "PostgreSQL")
			{
				scriptContainer = ArchAngel.Interfaces.ProjectOptions.DatabaseScripts.Scripts.ContainerForPostgreSql;
				Slyce.Common.SyntaxEditorHelper.SetupEditorTemplateAndScriptLanguages(syntaxEditorScript, Slyce.Common.TemplateContentLanguage.Sql, Slyce.Common.SyntaxEditorHelper.ScriptLanguageTypes.CSharp, @"<%", @"%>");
			}
			else if (db == "Firebird")
			{
				scriptContainer = ArchAngel.Interfaces.ProjectOptions.DatabaseScripts.Scripts.ContainerForFirebird;
				Slyce.Common.SyntaxEditorHelper.SetupEditorTemplateAndScriptLanguages(syntaxEditorScript, Slyce.Common.TemplateContentLanguage.Sql, Slyce.Common.SyntaxEditorHelper.ScriptLanguageTypes.CSharp, @"<%", @"%>");
			}
			else
				throw new NotImplementedException("Database type not handled yet: " + db);

			try
			{
				StringBuilder sb = new StringBuilder(10000);
				sb.AppendLine(scriptContainer.GetHeader(Database));

				//if (comboBoxTables.SelectedItem is ArchAngel.Interfaces.Scripting.DatabaseChanges.IChangedTable)
				//{
				//    ArchAngel.Interfaces.Scripting.DatabaseChanges.IChangedTable t = (ArchAngel.Interfaces.Scripting.DatabaseChanges.IChangedTable)comboBoxTables.SelectedItem;
				//    sb.AppendLine(scriptContainer.GetUpdate(t));
				//}
				//else if (comboBoxTables.SelectedItem is ArchAngel.Interfaces.Scripting.NHibernate.Model.ITable)
				//{
				//    ArchAngel.Interfaces.Scripting.NHibernate.Model.ITable t = (ArchAngel.Interfaces.Scripting.NHibernate.Model.ITable)comboBoxTables.SelectedItem;

				//    if (Database.NewTables.Contains(t))
				//        sb.AppendLine(scriptContainer.GetCreate(t));
				//    else
				//        sb.AppendLine(scriptContainer.GetDelete(t));
				//}
				//else
				//{
				//    foreach (var tbl in Database.NewTables)
				//        sb.AppendLine(scriptContainer.GetCreate(tbl));

				//    foreach (var tbl in Database.ChangedTables)
				//        sb.AppendLine(scriptContainer.GetUpdate(tbl));

				//    foreach (var tbl in Database.RemovedTables)
				//        sb.AppendLine(scriptContainer.GetDelete(tbl));
				//}
				return sb.ToString();
			}
			catch (Exception e)
			{
				//MessageBox.Show(this.Parent, e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return e.Message;
			}
		}

		private void comboBoxDatabases_SelectedIndexChanged(object sender, EventArgs e)
		{
			syntaxEditorScript.Text = GetScriptForAll();
		}

		private void buttonCopy_Click(object sender, EventArgs e)
		{

		}

		private void buttonX1_Click(object sender, EventArgs e)
		{

		}

		private void buttonRunScript_Click(object sender, EventArgs e)
		{

		}
	}
}
