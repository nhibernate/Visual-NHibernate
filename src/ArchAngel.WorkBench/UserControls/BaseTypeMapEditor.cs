using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArchAngel.Interfaces.ProjectOptions.TypeMappings;
using DevComponents.DotNetBar.Controls;

namespace ArchAngel.Workbench.UserControls
{
	public partial class BaseTypeMapEditor : UserControl
	{
		public enum TypeDisplayStyles
		{
			CSharp,
			VB,
			Framework
		}
		private const string SPACE = "&nbsp;&nbsp;&nbsp;&nbsp;";
		private TypeDisplayStyles TypeDisplayStyle = TypeDisplayStyles.CSharp;
		private bool BusyPopulating = false;

		public BaseTypeMapEditor()
		{
			InitializeComponent();
		}

		public void Populate()
		{
			BusyPopulating = true;
			SetupDotNetGrid();
			BusyPopulating = false;
		}

		private void SetupDotNetGrid()
		{
			int startColIndex = 3;

			for (int i = 0; i < 6; i++)
			{
				DataGridViewComboBoxExColumn col = (DataGridViewComboBoxExColumn)dataGridViewCSharp.Columns[i + startColIndex];
				col.DropDownStyle = ComboBoxStyle.DropDownList;
				col.DropDownWidth = 200;

				string[] dbTypes;

				switch (i)
				{
					case 0:
						dbTypes = ArchAngel.Interfaces.ProjectOptions.TypeMappings.Utility.SqlServerTypes.Select(t => t.TypeName).OrderBy(t => t).ToArray();
						break;
					case 1:
						dbTypes = ArchAngel.Interfaces.ProjectOptions.TypeMappings.Utility.OracleTypes.Select(t => t.TypeName).OrderBy(t => t).ToArray();
						break;
					case 2:
						dbTypes = ArchAngel.Interfaces.ProjectOptions.TypeMappings.Utility.MySqlTypes.Select(t => t.TypeName).OrderBy(t => t).ToArray();
						break;
					case 3:
						dbTypes = ArchAngel.Interfaces.ProjectOptions.TypeMappings.Utility.PostgreSqlTypes.Select(t => t.TypeName).OrderBy(t => t).ToArray();
						break;
					case 4:
						dbTypes = ArchAngel.Interfaces.ProjectOptions.TypeMappings.Utility.FirebirdTypes.Select(t => t.TypeName).OrderBy(t => t).ToArray();
						break;
					case 5:
						dbTypes = ArchAngel.Interfaces.ProjectOptions.TypeMappings.Utility.SQLiteTypes.Select(t => t.TypeName).OrderBy(t => t).ToArray();
						break;
					default:
						throw new NotImplementedException(string.Format("Column {0} not handled yet", i));
				}
				col.Items.Add("");
				col.Items.AddRange(dbTypes);
			}
			foreach (var x in Utility.DotNetTypes.OrderBy(o => o.Name))
			{
				DataGridViewRow row = new DataGridViewRow();
				row.Cells.Add(new DataGridViewTextBoxCell());
				row.Cells.Add(new DataGridViewTextBoxCell());
				row.Cells.Add(new DataGridViewTextBoxCell());
				row.Cells.Add(new DataGridViewComboBoxExCell());
				row.Cells.Add(new DataGridViewComboBoxExCell());
				row.Cells.Add(new DataGridViewComboBoxExCell());
				row.Cells.Add(new DataGridViewComboBoxExCell());
				row.Cells.Add(new DataGridViewComboBoxExCell());
				row.Cells.Add(new DataGridViewComboBoxExCell());
				row.Cells.Add(new DataGridViewButtonXCell());

				row.Cells[0].Value = x.Name;
				row.Cells[1].Value = x.CSharpName;
				row.Cells[2].Value = x.VbName;
				row.Cells[3].Value = x.SqlServerName;
				row.Cells[4].Value = x.OracleName;
				row.Cells[5].Value = x.MySqlName;
				row.Cells[6].Value = x.PostgreSqlName;
				row.Cells[7].Value = x.FirebirdName;
				row.Cells[8].Value = x.SQLiteName;
				row.Tag = x;
				dataGridViewCSharp.Rows.Add(row);
			}
		}

		private void dataGridViewCSharp_CellEnter(object sender, DataGridViewCellEventArgs e)
		{
			DotNetType dotnetType = (DotNetType)((DataGridView)sender).Rows[e.RowIndex].Tag;

			if (dotnetType == null)
			{
				labelUsedBy.Text = "";
				return;
			}
			labelUsedBy.BeginUpdate();

			StringBuilder sb = new StringBuilder();
			sb.AppendLine("<br/><br/>");
			//sb.AppendFormat("<br/>{0}UniType: <b>{1}</b>", SPACE, dotnetType.Name);
			//sb.AppendFormat("<br/>{0}C# type: <b>{1}</b>", SPACE, dotnetType.CSharpType);
			//sb.AppendLine("<br/><br/>");
			sb.AppendFormat("<br/>{0}<b>DB types that map to this:</b><br/><br/>", SPACE);

			sb.Append(GetDisplayTextForTypeUsages("SQL Server Types", Utility.SqlServerTypes, dotnetType));
			sb.Append(GetDisplayTextForTypeUsages("Oracle Types", Utility.OracleTypes, dotnetType));
			sb.Append(GetDisplayTextForTypeUsages("MySQL Types", Utility.MySqlTypes, dotnetType));
			sb.Append(GetDisplayTextForTypeUsages("PostgreSQL Types", Utility.PostgreSqlTypes, dotnetType));
			sb.Append(GetDisplayTextForTypeUsages("Firebird Types", Utility.FirebirdTypes, dotnetType));
			sb.Append(GetDisplayTextForTypeUsages("SQLite Types", Utility.SQLiteTypes, dotnetType));
			labelUsedBy.Text = sb.ToString();
			labelUsedBy.EndUpdate();
		}

		private string GetDisplayTextForTypeUsages(string heading, IEnumerable<DatabaseTypeMap> maps, DotNetType dotNetType)
		{
			var filteredMaps = maps.Where(s => s.DotNetType == dotNetType);

			if (filteredMaps.Count() > 0)
			{
				StringBuilder sb = new StringBuilder();
				sb.AppendLine(string.Format("{0}{0}{0}<b>{1}</b>", SPACE, heading));

				foreach (DatabaseTypeMap map in filteredMaps)
					sb.AppendLine(string.Format("<br/>{0}{0}{0}{0}{1}", SPACE, map.TypeName));

				sb.AppendLine(string.Format("<br/><br/>", heading));
				return sb.ToString();
			}
			return "";
		}

		private void dataGridViewCSharp_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
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

		private void dataGridViewCSharp_CellValueChanged(object sender, DataGridViewCellEventArgs e)
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
				case 8:
					dotnetType.SQLiteName = (string)cell.Value;
					break;
				default:
					throw new NotImplementedException("Column index is unexpected: " + e.ColumnIndex.ToString());
			}
		}

		private void dataGridViewCSharp_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (dataGridViewCSharp.Columns[e.ColumnIndex] == ColumnDelete)
			{
				DotNetType dotnetType = (DotNetType)dataGridViewCSharp.Rows[e.RowIndex].Tag;

				if (MessageBox.Show(this, string.Format("Delete [{0}]?", dotnetType.Name), "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					Utility.DotNetTypes.Remove(dotnetType);
					dataGridViewCSharp.Rows.RemoveAt(e.RowIndex);
				}
			}
		}

	}
}
