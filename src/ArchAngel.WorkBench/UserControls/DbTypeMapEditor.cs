using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ArchAngel.Interfaces.ProjectOptions.TypeMappings;
using DevComponents.DotNetBar.Controls;

namespace ArchAngel.Workbench.UserControls
{
	public partial class DbTypeMapEditor : UserControl
	{
		BaseTypeMapEditor.TypeDisplayStyles TypeDisplayStyle = BaseTypeMapEditor.TypeDisplayStyles.CSharp;
		private bool BusyPopulating = false;
		private List<DatabaseTypeMap> Maps;
		private string MapType;

		public DbTypeMapEditor()
		{
			InitializeComponent();

			Slyce.Common.SyntaxEditorHelper.SetupEditorTemplateAndScriptLanguages(syntaxEditorSqlServer, Slyce.Common.TemplateContentLanguage.CSharp, Slyce.Common.SyntaxEditorHelper.ScriptLanguageTypes.CSharp, @"<%", @"%>");
			//Slyce.Common.SyntaxEditorHelper.SetupEditorTemplateAndScriptLanguages(syntaxEditorOffscreen, Slyce.Common.TemplateContentLanguage.CSharp, Slyce.Common.SyntaxEditorHelper.ScriptLanguageTypes.CSharp);
		}

		public void Populate(string mapType)
		{
			BusyPopulating = true;

			MapType = mapType;

			switch (MapType)
			{
				case "Sql Server":
					Maps = ArchAngel.Interfaces.ProjectOptions.TypeMappings.Utility.SqlServerTypes;
					syntaxEditorSqlServer.Document.Text = Utility.PostProcessSciptSqlServer;
					dataGridViewSqlServer.Columns[0].HeaderText = "SQL Server types";
					labelScriptHeader.Text = "SQL Sever types : post-processing script";
					break;
				case "Oracle":
					Maps = ArchAngel.Interfaces.ProjectOptions.TypeMappings.Utility.OracleTypes;
					syntaxEditorSqlServer.Document.Text = Utility.PostProcessSciptOracle;
					dataGridViewSqlServer.Columns[0].HeaderText = "Oracle types";
					labelScriptHeader.Text = "Oracle types : post-processing script";
					break;
				case "MySQL":
					Maps = ArchAngel.Interfaces.ProjectOptions.TypeMappings.Utility.MySqlTypes;
					syntaxEditorSqlServer.Document.Text = Utility.PostProcessSciptMySql;
					dataGridViewSqlServer.Columns[0].HeaderText = "MySQL types";
					labelScriptHeader.Text = "MySQL types : post-processing script";
					break;
				case "PostgreSQL":
					Maps = ArchAngel.Interfaces.ProjectOptions.TypeMappings.Utility.PostgreSqlTypes;
					syntaxEditorSqlServer.Document.Text = Utility.PostProcessSciptPostgreSql;
					dataGridViewSqlServer.Columns[0].HeaderText = "PostgreSQL types";
					labelScriptHeader.Text = "PostgreSQL types : post-processing script";
					break;
				case "Firebird":
					Maps = ArchAngel.Interfaces.ProjectOptions.TypeMappings.Utility.FirebirdTypes;
					syntaxEditorSqlServer.Document.Text = Utility.PostProcessSciptFirebird;
					dataGridViewSqlServer.Columns[0].HeaderText = "Firebird types";
					labelScriptHeader.Text = "Firebird types : post-processing script";
					break;
				case "SQLite":
					Maps = ArchAngel.Interfaces.ProjectOptions.TypeMappings.Utility.SQLiteTypes;
					syntaxEditorSqlServer.Document.Text = Utility.PostProcessSciptSQLite;
					dataGridViewSqlServer.Columns[0].HeaderText = "SQLite types";
					labelScriptHeader.Text = "SQLite types : post-processing script";
					break;
				default:
					throw new NotImplementedException("MapType not handled yet: " + MapType);
			}
			dataGridViewSqlServer.Rows.Clear();
			SetupDatabaseGrid(dataGridViewSqlServer);

			foreach (var mapRow in Maps)
				AddMapGridRow(dataGridViewSqlServer, mapRow);

			BusyPopulating = false;
		}

		public void Save()
		{
			if (string.IsNullOrEmpty(MapType))
				return;

			switch (MapType)
			{
				case "Sql Server":
					Utility.PostProcessSciptSqlServer = syntaxEditorSqlServer.Document.GetText(ActiproSoftware.SyntaxEditor.LineTerminator.Newline);
					break;
				case "Oracle":
					Utility.PostProcessSciptOracle = syntaxEditorSqlServer.Document.GetText(ActiproSoftware.SyntaxEditor.LineTerminator.Newline);
					break;
				case "MySQL":
					Utility.PostProcessSciptMySql = syntaxEditorSqlServer.Document.GetText(ActiproSoftware.SyntaxEditor.LineTerminator.Newline);
					break;
				case "PostgreSQL":
					Utility.PostProcessSciptPostgreSql = syntaxEditorSqlServer.Document.GetText(ActiproSoftware.SyntaxEditor.LineTerminator.Newline);
					break;
				case "Firebird":
					Utility.PostProcessSciptFirebird = syntaxEditorSqlServer.Document.GetText(ActiproSoftware.SyntaxEditor.LineTerminator.Newline);
					break;
				case "SQLite":
					Utility.PostProcessSciptSQLite = syntaxEditorSqlServer.Document.GetText(ActiproSoftware.SyntaxEditor.LineTerminator.Newline);
					break;
				default:
					throw new NotImplementedException("MapType not handled yet: " + MapType);
			}
		}

		private void SetupDatabaseGrid(DataGridViewX grid)
		{
			((DataGridViewComboBoxExColumn)grid.Columns[1]).DropDownStyle = ComboBoxStyle.DropDownList;
			((DataGridViewComboBoxExColumn)grid.Columns[1]).DropDownWidth = 200;

			UpdateUniTypeComboBoxes();
		}

		private void AddMapGridRow(DataGridView grid, DatabaseTypeMap map)
		{
			DataGridViewRow row = new DataGridViewRow();
			row.Cells.Add(new DataGridViewTextBoxCell());
			row.Cells.Add(new DataGridViewComboBoxExCell());
			row.Cells[0].Value = map.TypeName;
			row.Cells[1].Value = GetDisplayText(map.DotNetType);
			row.Tag = map;
			grid.Rows.Add(row);
		}

		private void UpdateUniTypeComboBoxes()
		{
			string[] datatypes;
			datatypes = Utility.DotNetTypes.Select(x => x.Name).OrderBy(x => x).ToArray();

			((DataGridViewComboBoxExColumn)dataGridViewSqlServer.Columns[1]).Items.Clear();
			((DataGridViewComboBoxExColumn)dataGridViewSqlServer.Columns[1]).Items.Add("");
			((DataGridViewComboBoxExColumn)dataGridViewSqlServer.Columns[1]).Items.AddRange(datatypes);
		}

		private string GetDisplayText(DotNetType dotNetType)
		{
			if (dotNetType == null)
				return "";

			return dotNetType.Name;
		}

		private void dataGridViewSqlServer_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			if (BusyPopulating)
				return;

			DataGridView grid = (DataGridView)sender;

			for (int i = 0; i < e.RowCount; i++)
			{
				DataGridViewRow row = grid.Rows[e.RowIndex + i - 1];
				DatabaseTypeMap dbMapType = new DatabaseTypeMap("", null);
				Maps.Add(dbMapType);
				row.Tag = dbMapType;
			}
		}

		private void dataGridViewSqlServer_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (BusyPopulating || e.RowIndex < 0)
				return;

			DataGridView grid = (DataGridView)sender;

			DataGridViewRow row = grid.Rows[e.RowIndex];
			DataGridViewCell cell = row.Cells[e.ColumnIndex];
			DatabaseTypeMap dbMapType = (DatabaseTypeMap)row.Tag;

			switch (e.ColumnIndex)
			{
				case 0:
					dbMapType.TypeName = (string)cell.Value;
					break;
				case 1:
					string name = (string)cell.Value;

					if (!string.IsNullOrWhiteSpace(name))
					{
						//if (TypeDisplayStyle == TypeDisplayStyles.CSharp)
						//    name = Utility.DotNetTypes.SingleOrDefault(u => u.CSharpName == name).Name;
						//else if (TypeDisplayStyle == TypeDisplayStyles.VB)
						//    name = Utility.DotNetTypes.SingleOrDefault(u => u.VbName == name).Name;

						dbMapType.DotNetType = Utility.DotNetTypes.SingleOrDefault(u => u.Name == name);
					}
					break;
				default:
					throw new NotImplementedException("Column index is unexpected: " + e.ColumnIndex.ToString());
			}
		}
	}
}
