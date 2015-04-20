using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using Slyce.Common.Controls.Diagramming.SlyceGrid;

namespace ArchAngel.Providers.EntityModel.UI.Editors
{
	public partial class TableColumnsEditor : UserControl
	{
		private Table _Table;
		private Type uniDbType = typeof(EntityModel.Helper.SQLServer.UniDbTypes);

		public TableColumnsEditor()
		{
			InitializeComponent();

			slyceGrid1.DeleteClicked += new SlyceGrid.DeleteClickedDelegate(slyceGrid1_DeleteClicked);
			slyceGrid1.CellValueChanged += new SlyceGrid.CellValueChangedDelegate(slyceGrid1_CellValueChanged);
			slyceGrid1.NewRowAdded += new SlyceGrid.NewRowAddedDelegate(slyceGrid1_NewRowAdded);

			slyceGrid1.InvalidColor = Color.FromArgb(100, 100, 100);
			slyceGrid1.DisabledColor = Color.FromArgb(25, 25, 25);
			slyceGrid1.BackColor = Color.Black;//.FromArgb(13, 13, 13);
		}

		public Table Table
		{
			get { return _Table; }
			set
			{
				_Table = value;

				if (_Table != null)
				{
					Populate();
				}
			}
		}

		void slyceGrid1_NewRowAdded(out object newObject)
		{
			Column column = new Column("NewColumn")
			{

			};
			Table.AddColumn(column);
			column.OriginalDataType = ArchAngel.Interfaces.ProjectOptions.TypeMappings.Utility.GetDefaultDatabaseType(Table.Database.DatabaseType.ToString(), "System.String");
			newObject = column;

			AddColumnToColumnsGrid(column);
		}

		private void Populate()
		{
			Slyce.Common.Utility.SuspendPainting(slyceGrid1);
			slyceGrid1.Clear();
			// Populate Columns from Entity
			slyceGrid1.Columns.Add(new ColumnItem("Name", ColumnItem.ColumnTypes.Textbox, "NewProp", "General"));
			slyceGrid1.Columns.Add(new ColumnItem("Description", ColumnItem.ColumnTypes.Textbox, "System.String", "General"));
			slyceGrid1.Columns.Add(new ColumnItem("Is Nullable", ColumnItem.ColumnTypes.Checkbox, false, "General"));
			//slyceGrid1.Columns.Add(new ColumnItem("Data Type", ColumnItem.ColumnTypes.ComboBox, "varchar(20)", "General"));

			string defaultDbType = ArchAngel.Interfaces.ProjectOptions.TypeMappings.Utility.GetDefaultDatabaseType(Table.Database.DatabaseType.ToString(), "System.String");
			ColumnItem col = new ColumnItem("Data Type", ColumnItem.ColumnTypes.ComboBox, defaultDbType, "General");
			col.ColorScheme = ColumnItem.ColorSchemes.Blue;

			foreach (var dbTypeName in ArchAngel.Interfaces.ProjectOptions.TypeMappings.Utility.GetDatabaseTypes(Table.Database.DatabaseType.ToString()).Select(d => d.TypeName).Distinct())
				col.ComboItems.Add(dbTypeName, null);

			slyceGrid1.Columns.Add(col);

			slyceGrid1.Columns.Add(new ColumnItem("Default Value", ColumnItem.ColumnTypes.Textbox, "", "General"));
			slyceGrid1.Columns.Add(new ColumnItem("Ordinal Position", ColumnItem.ColumnTypes.IntegerInput, "0", "General"));
			slyceGrid1.Columns.Add(new ColumnItem("Size", ColumnItem.ColumnTypes.IntegerInput, "8", "General"));
			slyceGrid1.Columns.Add(new ColumnItem("Size Is Max", ColumnItem.ColumnTypes.Checkbox, false, "General"));
			slyceGrid1.Columns.Add(new ColumnItem("Precision", ColumnItem.ColumnTypes.IntegerInput, "1", "General"));
			slyceGrid1.Columns.Add(new ColumnItem("Scale", ColumnItem.ColumnTypes.IntegerInput, "1", "General"));

			foreach (Key key in Table.Keys)
				slyceGrid1.Columns.Add(new ColumnItem(GetColumnHeaderForKey(key), ColumnItem.ColumnTypes.Checkbox, false, "Keys"));

			foreach (Column column in Table.Columns)
				AddColumnToColumnsGrid(column);

			slyceGrid1.Populate();
			Slyce.Common.Utility.ResumePainting(slyceGrid1);
		}

		private string GetColumnHeaderForKey(Key key)
		{
			if (key.Keytype == ArchAngel.Providers.EntityModel.Helper.DatabaseKeyType.Primary)
				return "Primary key";
			else
				return string.Format("{0} key [{1}]", key.Keytype, key.Name);
		}

		private void AddColumnToColumnsGrid(Column column)
		{
			SlyceTreeGridItem gridItem = new SlyceTreeGridItem();
			gridItem.Tag = column;
			gridItem.SubItems.Add(new SlyceTreeGridCellItem(column.Name));
			gridItem.SubItems.Add(new SlyceTreeGridCellItem(column.Description));
			gridItem.SubItems.Add(new SlyceTreeGridCellItem(column.IsNullable));
			gridItem.SubItems.Add(new SlyceTreeGridCellItem(column.OriginalDataType == null ? null : column.OriginalDataType.ToLower()));
			gridItem.SubItems.Add(new SlyceTreeGridCellItem(column.Default));
			gridItem.SubItems.Add(new SlyceTreeGridCellItem(column.OrdinalPosition));
			gridItem.SubItems.Add(new SlyceTreeGridCellItem(column.Size));
			gridItem.SubItems.Add(new SlyceTreeGridCellItem(column.SizeIsMax));
			gridItem.SubItems.Add(new SlyceTreeGridCellItem(column.Precision));
			gridItem.SubItems.Add(new SlyceTreeGridCellItem(column.Scale));

			foreach (IKey key in Table.Keys)
			{
				SlyceTreeGridCellItem item = new SlyceTreeGridCellItem(key.Columns.Contains(column));
				item.Enabled = true;// column.InPrimaryKey;
				gridItem.SubItems.Add(item);
			}

			slyceGrid1.Items.Add(gridItem);
		}

		private bool slyceGrid1_DeleteClicked(int row, object tag)
		{
			if (MessageBox.Show(this, string.Format("Delete {0}?", ((Column)tag).Name), "Delete Property", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				Table.RemoveColumn((Column)tag);
				return true;
			}
			return false;
		}

		internal void FinaliseEdits()
		{
			slyceGrid1.FinaliseEdits();
		}

		void slyceGrid1_CellValueChanged(int row, int cell, int columnIndex, string columnHeader, ref object tag, object newValue)
		{
			if (cell == 0) // delete image column
				return;

			Column column = (Column)tag;

			if (column == null)
				return;

			bool nullableColumn = string.IsNullOrEmpty(columnHeader);

			if (nullableColumn)
				columnHeader = slyceGrid1.Columns[columnIndex].Text;

			switch (columnHeader)
			{
				case "Name":
					var originalName = column.Name;
					column.Name = (string)newValue;

					if (originalName != column.Name)
						ArchAngel.Interfaces.Events.RaiseIsDirtyEvent();
					break;
				case "Description":
					var originalDescription = column.Description;
					column.Description = (string)newValue;

					if (originalDescription != column.Description)
						ArchAngel.Interfaces.Events.RaiseIsDirtyEvent();
					break;
				case "Is Nullable":
					var originalIsNullable = column.IsNullable;
					column.IsNullable = (bool)newValue;

					if (originalIsNullable != column.IsNullable)
						ArchAngel.Interfaces.Events.RaiseIsDirtyEvent();
					break;
				case "Data Type":
					var originalOriginalDataType = column.OriginalDataType;
					column.OriginalDataType = (string)newValue;

					if (originalOriginalDataType != column.OriginalDataType)
						ArchAngel.Interfaces.Events.RaiseIsDirtyEvent();
					break;
				case "Default Value":
					var originalDefault = column.Default;
					column.Default = (string)newValue;

					if (originalDefault != column.Default)
						ArchAngel.Interfaces.Events.RaiseIsDirtyEvent();
					break;
				case "Ordinal Position":
					var originalOrdinalPosition = column.OrdinalPosition;
					column.OrdinalPosition = newValue is int ? (int)newValue : int.Parse((string)newValue);

					if (originalOrdinalPosition != column.OrdinalPosition)
						ArchAngel.Interfaces.Events.RaiseIsDirtyEvent();
					break;
				case "Size":
					var originalSize = column.Size;
					column.Size = newValue is long ? (long)newValue : long.Parse((string)newValue);

					if (originalSize != column.Size)
						ArchAngel.Interfaces.Events.RaiseIsDirtyEvent();
					break;
				case "Size Is Max":
					var originalSizeIsMax = column.SizeIsMax;
					column.SizeIsMax = (bool)newValue;

					if (originalSizeIsMax != column.SizeIsMax)
						ArchAngel.Interfaces.Events.RaiseIsDirtyEvent();
					break;
				case "Precision":
					var originalPrecision = column.Precision;
					column.Precision = newValue is int ? (int)newValue : int.Parse((string)newValue);

					if (originalPrecision != column.Precision)
						ArchAngel.Interfaces.Events.RaiseIsDirtyEvent();
					break;
				case "Scale":
					var originalScale = column.Scale;
					column.Scale = newValue is int ? (int)newValue : int.Parse((string)newValue);

					if (originalScale != column.Scale)
						ArchAngel.Interfaces.Events.RaiseIsDirtyEvent();
					break;
				default:
					bool inKey = (bool)newValue;
					bool found = false;

					foreach (Key key in Table.Keys)
					{
						if (columnHeader == GetColumnHeaderForKey(key))
						{
							found = true;

							if (inKey)
							{
								if (!key.Columns.Contains(column))
								{
									key.AddColumn(column.Name);
									ArchAngel.Interfaces.Events.RaiseIsDirtyEvent();
								}
							}
							else
							{
								if (key.Columns.Contains(column))
								{
									key.RemoveColumn(column);
									ArchAngel.Interfaces.Events.RaiseIsDirtyEvent();
								}
							}
						}
					}
					if (!found)
						throw new NotImplementedException("Column header not handled yet: " + columnHeader);
					break;
			}
		}


	}
}
