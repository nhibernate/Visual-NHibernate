using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ActiproSoftware.SyntaxEditor;
using ArchAngel.Interfaces.ProjectOptions.TypeMappings;

namespace ArchAngel.Workbench.UserControls
{
	public partial class ProjectSettings : UserControl
	{
		private bool BusyPopulating = false;
		private DateTime TimeOfLastUniTypesUpdate = DateTime.Now;
		private DateTime TimeOfLastDatabaseGridsUpdate = DateTime.Now;
		private ArchAngel.Interfaces.ProjectOptions.DatabaseScripts.MaintenanceScript CurrentDatabaseScript = null;

		public ProjectSettings()
		{
			InitializeComponent();

			SetStyle(
					ControlStyles.UserPaint |
					ControlStyles.AllPaintingInWmPaint |
					ControlStyles.OptimizedDoubleBuffer, true);

			panelContent.Controls.Clear();
			SetSyntax(Slyce.Common.TemplateContentLanguage.CSharp, syntaxEditorMySQL, syntaxEditorOracle, syntaxEditorPostgreSQL, syntaxEditorSqlServer, syntaxEditorFirebird, syntaxEditorSQLite);

			Populate();
		}

		public void SwitchFormatting(ActiproSoftware.SyntaxEditor.SyntaxEditor syntaxEditor)
		{
			//UseSplitLanguage = !UseSplitLanguage;
			if (syntaxEditor.Document.Language.LexicalStates.Count > 1)
			{
				syntaxEditor.Document.Language.LexicalStates["ASPDirectiveState"].LexicalStateTransitionLexicalState.
					Language.BackColor = Slyce.Common.SyntaxEditorHelper.EDITOR_BACK_COLOR_FADED;
				syntaxEditor.Document.Language.BackColor = Slyce.Common.SyntaxEditorHelper.EDITOR_BACK_COLOR_NORMAL;
				syntaxEditor.Refresh();
			}
		}

		private void Populate()
		{
			BusyPopulating = true;
			Utility.LoadSettings();
			ArchAngel.Interfaces.ProjectOptions.DatabaseScripts.Utility.LoadSettings();
			ArchAngel.Interfaces.ProjectOptions.ModelScripts.Utility.LoadSettings();

			syntaxEditorSqlServer.Text = Utility.PostProcessSciptSqlServer;
			syntaxEditorOracle.Text = Utility.PostProcessSciptOracle;
			syntaxEditorMySQL.Text = Utility.PostProcessSciptMySql;
			syntaxEditorPostgreSQL.Text = Utility.PostProcessSciptPostgreSql;
			syntaxEditorFirebird.Text = Utility.PostProcessSciptFirebird;
			syntaxEditorSQLite.Text = Utility.PostProcessSciptSQLite;

			BusyPopulating = false;
			TimeOfLastUniTypesUpdate = DateTime.Now;

			baseTypeMapEditor1.Populate();
		}

		private void SetSyntax(Slyce.Common.TemplateContentLanguage textLanguage, params SyntaxEditor[] editors)
		{
			foreach (var editor in editors)
			{
				//if (CurrentLanguages[editor] != textLanguage)
				//{
				Slyce.Common.SyntaxEditorHelper.SetupEditorTemplateAndScriptLanguages(editor, textLanguage, Slyce.Common.SyntaxEditorHelper.ScriptLanguageTypes.CSharp, @"<%", @"%>");
				//UseSplitLanguage = true;// !UseSplitLanguage;
				//SwitchFormatting();
				//CurrentLanguages[editor] = textLanguage;
				//}
			}
		}

		private void buttonSaveTypeMappings_Click(object sender, System.EventArgs e)
		{
			Save();
		}

		private void Save()
		{
			dbTypeMapEditor1.Save();
			SaveDatabaseScriptEditor();
			SaveNamingScriptEditor();
			Utility.SaveSettings();
			ArchAngel.Interfaces.ProjectOptions.DatabaseScripts.Utility.SaveSettings();
			ArchAngel.Interfaces.ProjectOptions.ModelScripts.Utility.SaveSettings();
		}

		private void dataGridViewUniTypes_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			if (BusyPopulating)
				return;

			DataGridView grid = (DataGridView)sender;

			for (int i = 0; i < e.RowCount; i++)
			{
				DataGridViewRow row = grid.Rows[e.RowIndex + i - 1];
				DotNetType dotnetType = new DotNetType();
				Utility.DotNetTypes.Add(dotnetType);
				row.Tag = dotnetType;
			}
		}

		private void dataGridViewUniTypes_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (BusyPopulating || e.RowIndex < 0)
				return;

			DataGridViewRow row = dataGridViewCSharp.Rows[e.RowIndex];
			DataGridViewCell cell = row.Cells[e.ColumnIndex];
			DotNetType dotnetType = (DotNetType)row.Tag;

			switch (e.ColumnIndex)
			{
				case 0:
					dotnetType.Name = (string)cell.Value;
					break;
				case 1:
					dotnetType.CSharpName = (string)cell.Value;
					break;
				case 2:
					dotnetType.VbName = (string)cell.Value;
					break;
				case 3:
					dotnetType.SqlServerName = (string)cell.Value;
					break;
				case 4:
					dotnetType.OracleName = (string)cell.Value;
					break;
				case 5:
					dotnetType.MySqlName = (string)cell.Value;
					break;
				case 6:
					dotnetType.PostgreSqlName = (string)cell.Value;
					break;
				case 7:
					dotnetType.FirebirdName = (string)cell.Value;
					break;
				default:
					throw new NotImplementedException("Column index is unexpected: " + e.ColumnIndex.ToString());
			}
			TimeOfLastUniTypesUpdate = DateTime.Now;
		}

		private void mapGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			if (BusyPopulating)
				return;

			DataGridView grid = (DataGridView)sender;
			List<DatabaseTypeMap> collection;

			if (grid == dataGridViewSqlServer)
				collection = Utility.SqlServerTypes;
			else if (grid == dataGridViewOracle)
				collection = Utility.OracleTypes;
			else if (grid == dataGridViewMySQL)
				collection = Utility.MySqlTypes;
			else if (grid == dataGridViewPostgreSQL)
				collection = Utility.PostgreSqlTypes;
			else if (grid == dataGridViewFirebird)
				collection = Utility.FirebirdTypes;
			else if (grid == dataGridViewSQLite)
				collection = Utility.SQLiteTypes;
			else
				throw new NotImplementedException("Grid not expected: " + grid.Name);

			for (int i = 0; i < e.RowCount; i++)
			{
				DataGridViewRow row = grid.Rows[e.RowIndex + i - 1];
				DatabaseTypeMap dbMapType = new DatabaseTypeMap("", null);
				collection.Add(dbMapType);
				row.Tag = dbMapType;
			}
		}

		private void buttonX1_Click(object sender, EventArgs e)
		{
			Save();
		}

		private void SaveDatabaseScriptEditor()
		{
			if (BusyPopulating)// || CurrentDatabaseScript == null)
				return;

			databaseScriptEditor1.Save();
		}

		private void SaveNamingScriptEditor()
		{
			if (BusyPopulating)// || CurrentDatabaseScript == null)
				return;

			namingEditor1.Save();
		}

		private void treeSettings_AfterNodeSelect(object sender, DevComponents.AdvTree.AdvTreeNodeEventArgs e)
		{
			//baseTypeMapEditor1.Save();
			dbTypeMapEditor1.Save();
			databaseScriptEditor1.Save();
			namingEditor1.Save();

			if (e.Node == nodeTypeMapsBase)
			{
				ShowControl(baseTypeMapEditor1);
				//baseTypeMapEditor1.Populate(ArchAngel.Interfaces.ProjectOptions.DatabaseScripts.Utility.SqlServerScript, "Delete Table");
			}
			else if (e.Node == nodeTypeMapsSqlServer)
			{
				ShowControl(dbTypeMapEditor1);
				dbTypeMapEditor1.Populate("Sql Server");
			}
			else if (e.Node == nodeTypeMapsOracle)
			{
				ShowControl(dbTypeMapEditor1);
				dbTypeMapEditor1.Populate("Oracle");
			}
			else if (e.Node == nodeTypeMapsMySQL)
			{
				ShowControl(dbTypeMapEditor1);
				dbTypeMapEditor1.Populate("MySQL");
			}
			else if (e.Node == nodeTypeMapsPostgreSQL)
			{
				ShowControl(dbTypeMapEditor1);
				dbTypeMapEditor1.Populate("PostgreSQL");
			}
			else if (e.Node == nodeTypeMapsFirebird)
			{
				ShowControl(dbTypeMapEditor1);
				dbTypeMapEditor1.Populate("Firebird");
			}
			else if (e.Node == nodeTypeMapsSQLite)
			{
				ShowControl(dbTypeMapEditor1);
				dbTypeMapEditor1.Populate("SQLite");
			}
			else if (e.Node == nodeNamingScriptEntity)
			{
				ShowControl(namingEditor1);
				namingEditor1.Populate(NamingEditor.NamingTypes.Entity, ArchAngel.Interfaces.ProjectOptions.ModelScripts.Utility.EntityNamingScript);
			}
			else if (e.Node == nodeNamingScriptProperty)
			{
				ShowControl(namingEditor1);
				namingEditor1.Populate(NamingEditor.NamingTypes.Property, ArchAngel.Interfaces.ProjectOptions.ModelScripts.Utility.PropertyNamingScript);
			}
			else if (e.Node.Parent != null)
			{
				ArchAngel.Interfaces.ProjectOptions.DatabaseScripts.MaintenanceScript script = null;

				if (e.Node.Parent.Text == "SQL Server") script = ArchAngel.Interfaces.ProjectOptions.DatabaseScripts.Utility.SqlServerScript;
				else if (e.Node.Parent.Text == "Oracle") script = ArchAngel.Interfaces.ProjectOptions.DatabaseScripts.Utility.OracleScript;
				else if (e.Node.Parent.Text == "MySQL") script = ArchAngel.Interfaces.ProjectOptions.DatabaseScripts.Utility.MySqlScript;
				else if (e.Node.Parent.Text == "PostgreSQL") script = ArchAngel.Interfaces.ProjectOptions.DatabaseScripts.Utility.PostgreSqlScript;
				else if (e.Node.Parent.Text == "Firebird") script = ArchAngel.Interfaces.ProjectOptions.DatabaseScripts.Utility.FirebirdScript;
				else if (e.Node.Parent.Text == "SQLite") script = ArchAngel.Interfaces.ProjectOptions.DatabaseScripts.Utility.SQLiteScript;

				if (script != null)
				{
					ShowControl(databaseScriptEditor1);
					databaseScriptEditor1.Populate(script, e.Node.Text);
				}
			}
		}

		private void ShowControl(Control control)
		{
			if (panelContent.Controls.Count == 0 ||
				panelContent.Controls[0] != control)
			{
				Slyce.Common.Utility.SuspendPainting(panelContent);
				panelContent.Controls.Clear();
				panelContent.Controls.Add(control);
				control.Dock = DockStyle.Fill;
				Slyce.Common.Utility.ResumePainting(panelContent);
			}

		}
	}
}
