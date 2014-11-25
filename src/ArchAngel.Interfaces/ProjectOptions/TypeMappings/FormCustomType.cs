using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ArchAngel.Interfaces.ProjectOptions.TypeMappings
{
	public partial class FormCustomType : Form
	{
		private Utility.DatabaseTypes Database;

		public FormCustomType(string databaseTypeName, Utility.DatabaseTypes database)
		{
			InitializeComponent();

			Database = database;
			labelDatabaseType.Text = databaseTypeName;
			labelQuestion.Text = string.Format(@"What .Net type do you want to map <b>{0}</b> to?", databaseTypeName);
			//comboBoxCSharpTypes.DisplayMember = "Name";
			//comboBoxCSharpTypes.DataSource = Utility.DotNetTypes.OrderBy(t => t.Name).ToList();

			comboBoxCSharpTypes.Items.AddRange(Utility.DotNetTypes.OrderBy(t => t.Name).Select(t => t.Name).ToArray());

			//foreach (DotNetType dotnetType in Utility.DotNetTypes.OrderBy(t => t.Name))
			//    comboBoxCSharpTypes.Items.Add(dotnetType);

		}

		private void buttonOk_Click(object sender, EventArgs e)
		{
			if (checkBoxExisting.Checked && comboBoxCSharpTypes.SelectedItem == null)
			{
				MessageBox.Show(this, "Please select a .Net type.", "Missing value", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (checkBoxNew.Checked && (string.IsNullOrWhiteSpace(textBoxDotNetType.Text) || string.IsNullOrWhiteSpace(textBoxCSharpType.Text)))
			{
				MessageBox.Show(this, "You need to enter .Net and C# types.", "Missing values", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			DotNetType dotnetType = null;

			if (checkBoxExisting.Checked)
				dotnetType = Utility.DotNetTypes.SingleOrDefault(testc => testc.Name == comboBoxCSharpTypes.Text);
			else
			{
				Utility.DotNetTypes.SingleOrDefault(t => t.Name.ToLowerInvariant() == textBoxDotNetType.Text.Trim().ToLowerInvariant());

				if (dotnetType == null)
				{
					dotnetType = new DotNetType() { Name = textBoxDotNetType.Text.Trim(), CSharpName = textBoxCSharpType.Text.Trim() };
					ArchAngel.Interfaces.ProjectOptions.TypeMappings.Utility.DotNetTypes.Add(dotnetType);
				}
			}
			List<DatabaseTypeMap> maps;

			switch (Database)
			{
				case Utility.DatabaseTypes.Firebird:
					maps = Utility.FirebirdTypes;
					break;
				case Utility.DatabaseTypes.MySql:
					maps = Utility.MySqlTypes;
					break;
				case Utility.DatabaseTypes.Oracle:
					maps = Utility.OracleTypes;
					break;
				case Utility.DatabaseTypes.PostgreSql:
					maps = Utility.PostgreSqlTypes;
					break;
				case Utility.DatabaseTypes.SqlServer:
					maps = Utility.SqlServerTypes;
					break;
				case Utility.DatabaseTypes.SQLite:
					maps = Utility.SQLiteTypes;
					break;
				default:
					throw new NotImplementedException("Database type not handled yet: " + Database.ToString());
			}
			DatabaseTypeMap newDBType = new DatabaseTypeMap("", null);
			newDBType.TypeName = labelDatabaseType.Text;
			newDBType.DotNetType = dotnetType;
			maps.Add(newDBType);
			DotNetType = dotnetType;
			Utility.SaveSettings();
			Close();
		}

		internal DotNetType DotNetType
		{
			get;
			set;
		}

		private void checkBoxNew_CheckedChanged(object sender, EventArgs e)
		{
			groupPanel1.Enabled = checkBoxNew.Checked;
			comboBoxCSharpTypes.Enabled = checkBoxExisting.Checked;
		}
	}
}
