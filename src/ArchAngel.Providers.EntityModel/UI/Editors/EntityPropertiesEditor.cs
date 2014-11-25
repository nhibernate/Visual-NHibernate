using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using Slyce.Common.Controls.Diagramming.SlyceGrid;

namespace ArchAngel.Providers.EntityModel.UI.Editors
{
	public partial class EntityPropertiesEditor : UserControl
	{
		private EntityImpl _Entity;
		public List<ITable> MappedTables = new List<ITable>();

		public EntityPropertiesEditor()
		{
			InitializeComponent();

			ArchAngel.Interfaces.SharedData.AboutToSave += new EventHandler(SharedData_AboutToSave);

			slyceGrid1.DeleteClicked += new SlyceGrid.DeleteClickedDelegate(slyceGrid1_DeleteClicked);
			slyceGrid1.CellValueChanged += new SlyceGrid.CellValueChangedDelegate(slyceGrid1_CellValueChanged);
			slyceGrid1.NewRowAdded += new SlyceGrid.NewRowAddedDelegate(slyceGrid1_NewRowAdded);

			slyceGrid1.InvalidColor = Color.FromArgb(100, 100, 100);
			slyceGrid1.DisabledColor = Color.FromArgb(25, 25, 25);
			slyceGrid1.BackColor = Color.Black;//.FromArgb(13, 13, 13);
		}

		internal void FinaliseEdits()
		{
			slyceGrid1.FinaliseEdits();
		}

		void SharedData_AboutToSave(object sender, EventArgs e)
		{
			slyceGrid1.FinaliseEdits();
		}

		private void slyceGrid1_NewRowAdded(out object newObject)
		{
			Property property = new PropertyImpl("NewProperty")
				{
					Type = "System.String",
					NHibernateType = "String",
					Entity = this.Entity,
					IsKeyProperty = false,
					ReadOnly = false
				};
			property.ValidationOptions.FractionalDigits = 0;
			property.ValidationOptions.FutureDate = false;
			property.ValidationOptions.IntegerDigits = 0;
			property.ValidationOptions.MaximumLength = 0;
			property.ValidationOptions.MaximumValue = 0;
			property.ValidationOptions.MinimumLength = 0;
			property.ValidationOptions.MinimumValue = 0;
			property.ValidationOptions.NotEmpty = false;
			property.ValidationOptions.Nullable = false;
			property.ValidationOptions.PastDate = false;
			property.ValidationOptions.RegexPattern = "";
			property.ValidationOptions.Validate = false;

			Entity.AddProperty(property);
			newObject = property;

			bool hasMultiSchemas = MappedTables.Select(t => t.Schema).Distinct().Count() > 1;
			AddPropertyToPropertiesGrid(property, hasMultiSchemas);
		}

		private void slyceGrid1_CellValueChanged(int row, int cell, int columnIndex, string columnHeader, ref object tag, object newValue)
		{
			if (cell == 0) // delete image column
				return;

			Property property = (Property)tag;

			if (property == null)
				return;

			bool nullableColumn = string.IsNullOrEmpty(columnHeader);

			if (nullableColumn)
				columnHeader = slyceGrid1.Columns[columnIndex].Text;

			switch (columnHeader)
			{
				case "Name":
					property.Name = (string)newValue;
					break;
				case "Type":
					property.Type = (string)newValue;
					property.NHibernateType = "";
					int nhTypeColumn = 3;
					slyceGrid1.SetValue(row, nhTypeColumn, property.NHibernateType);
					slyceGrid1.Refresh();
					break;
				case "NHibernate Type":
					property.NHibernateType = (string)newValue;
					break;
				case "In Key":
					property.IsKeyProperty = (bool)newValue;
					break;
				case "Read-only":
					property.ReadOnly = (bool)newValue;
					break;
				case "Mapped To":
					Column mappedColumn = null;
					string name = (string)newValue;

					if (MappedTables.Count == 1)
					{
						foreach (Column column in MappedTables[0].Columns)
						{
							if (column.Name == name)
							{
								mappedColumn = column;
								break;
							}
						}
					}
					else
					{
						bool hasMultiSchemas = MappedTables.Select(t => t.Schema).Distinct().Count() > 1;

						foreach (Table table in MappedTables)
						{
							bool colFound = false;

							foreach (Column column in table.Columns)
							{
								if (hasMultiSchemas)
								{
									if (string.Format("{0}.{1}.{2}", table.Schema, table.Name, column.Name) == name)
									{
										mappedColumn = column;
										colFound = true;
										break;
									}
								}
								else if (string.Format("{0}.{1}", table.Name, column.Name) == name)
								{
									mappedColumn = column;
									colFound = true;
									break;
								}
							}
							if (colFound)
								break;
						}
					}
					property.SetMappedColumn(mappedColumn);
					break;
				case "Fractional Digits":
					property.ValidationOptions.FractionalDigits = GetNullableIntValue(row, cell, newValue, nullableColumn);
					break;
				case "Future Date":
					property.ValidationOptions.FutureDate = GetNullableBooleanValue(row, cell, newValue, nullableColumn);
					break;
				case "Integer Digits":
					property.ValidationOptions.IntegerDigits = GetNullableIntValue(row, cell, newValue, nullableColumn);
					break;
				case "Max Length":
					property.ValidationOptions.MaximumLength = GetNullableIntValue(row, cell, newValue, nullableColumn);
					break;
				case "Max Value":
					property.ValidationOptions.MaximumValue = GetNullableIntValue(row, cell, newValue, nullableColumn);
					break;
				case "Min Length":
					property.ValidationOptions.MinimumLength = GetNullableIntValue(row, cell, newValue, nullableColumn);
					break;
				case "Min Value":
					property.ValidationOptions.MinimumValue = GetNullableIntValue(row, cell, newValue, nullableColumn);
					break;
				case "Not Empty":
					property.ValidationOptions.NotEmpty = GetNullableBooleanValue(row, cell, newValue, nullableColumn);
					break;
				case "Nullable":
					property.ValidationOptions.Nullable = GetNullableBooleanValue(row, cell, newValue, nullableColumn);
					break;
				case "Past Date":
					property.ValidationOptions.PastDate = GetNullableBooleanValue(row, cell, newValue, nullableColumn);
					break;
				case "Regex":
					if (nullableColumn)
					{
						if ((bool)slyceGrid1.GetValue(row, cell))
							property.ValidationOptions.RegexPattern = (string)slyceGrid1.GetValue(row, cell + 1);
						else
							property.ValidationOptions.RegexPattern = null;
					}
					else
						property.ValidationOptions.RegexPattern = (string)newValue;
					break;
				case "Validate":
					property.ValidationOptions.Validate = newValue == null ? false : bool.Parse(newValue.ToString());
					break;
				default:
					bool found = false;

					foreach (ArchAngel.Interfaces.ITemplate.IUserOption uo in property.Ex)
					{
						if (uo.Text == columnHeader)
						{
							found = true;
							uo.Value = newValue;
							break;
						}
					}
					if (!found)
						throw new NotImplementedException("Column header not handled yet: " + columnHeader);
					break;
			}
		}

		private int? GetNullableIntValue(int row, int cell, object newValue, bool isNullableColumn)
		{
			int? returnVal;
			string stringValue;

			if (isNullableColumn)
			{
				if ((bool)slyceGrid1.GetValue(row, cell))
				{
					object val = slyceGrid1.GetValue(row, cell + 1);

					if (val == null)
						val = 0;

					stringValue = val.ToString();
					returnVal = string.IsNullOrEmpty(stringValue) ? 0 : Convert.ToInt32(stringValue);
				}
				else
					returnVal = null;
			}
			else
			{
				if (newValue == null)
					returnVal = null;
				else
				{
					int result;

					if (!int.TryParse(newValue.ToString(), out result))
						returnVal = null;
					else
						returnVal = result;
				}
			}
			return returnVal;
		}

		private bool? GetNullableBooleanValue(int row, int cell, object newValue, bool isNullableColumn)
		{
			bool? returnVal;

			if (isNullableColumn)
			{
				if ((bool)slyceGrid1.GetValue(row, cell))
				{
					returnVal = (bool)slyceGrid1.GetValue(row, cell + 1);
				}
				else
					returnVal = null;
			}
			else
			{
				bool result;

				if (newValue == null)
					returnVal = null;
				else if (bool.TryParse(newValue.ToString(), out result))
					returnVal = result;
				else
					returnVal = null;
			}
			return returnVal;
		}

		private bool slyceGrid1_DeleteClicked(int row, object tag)
		{
			if (MessageBox.Show(this, string.Format("Delete {0}?", ((Property)tag).Name), "Delete Property", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				Entity.RemoveProperty((Property)tag);
				return true;
			}
			return false;
		}

		public EntityImpl Entity
		{
			get { return _Entity; }
			set
			{
				_Entity = value;

				if (_Entity != null)
					Populate();
			}
		}

		public void RefreshData()
		{
			Populate();
		}

		private void Populate()
		{
			//ArchAngel.Interfaces.
			Slyce.Common.Utility.SuspendPainting(slyceGrid1);
			slyceGrid1.Clear();
			// Populate Columns from Entity

			#region Name column
			ColumnItem col = new ColumnItem("Name", ColumnItem.ColumnTypes.Textbox, "NewProp", "General");
			col.ColorScheme = ColumnItem.ColorSchemes.Blue;
			slyceGrid1.Columns.Add(col);
			#endregion

			#region Type column
			col = new ColumnItem("Type", ColumnItem.ColumnTypes.ComboBox, "System.String", "General");
			col.ColorScheme = ColumnItem.ColorSchemes.Blue;

			foreach (var x in ArchAngel.Interfaces.ProjectOptions.TypeMappings.Utility.DotNetTypes.Select(t => t.CSharpName).Distinct())
				col.ComboItems.Add(x, null);

			slyceGrid1.Columns.Add(col);
			#endregion

			#region NHibernate Type column
			col = new ColumnItem("NHibernate Type", ColumnItem.ColumnTypes.ComboBox, "System.String", "General");
			col.ColorScheme = ColumnItem.ColorSchemes.Blue;

			foreach (var x in Enum.GetNames(typeof(PropertyImpl.NHibernateTypes)).OrderBy(t => t))
				col.ComboItems.Add(x, null);

			slyceGrid1.Columns.Add(col);
			#endregion

			#region MappedColumns column
			ColumnItem mappedColumnsColumn = new ColumnItem("Mapped To", ColumnItem.ColumnTypes.ComboBox, "", "General");
			mappedColumnsColumn.ComboItems.Add("", null);
			mappedColumnsColumn.ColorScheme = ColumnItem.ColorSchemes.Blue;

			int numTables = MappedTables.Count;

			bool hasMultiSchemas = MappedTables.Select(t => t.Schema).Distinct().Count() > 1;

			foreach (Table table in MappedTables)
			{
				foreach (Column column in table.Columns)
				{
					string name;

					if (numTables == 1) name = column.Name;
					else if (hasMultiSchemas) name = string.Format("{0}.{1}.{2}", table.Schema, table.Name, column.Name);
					else name = string.Format("{0}.{1}", table.Name, column.Name);

					if (!mappedColumnsColumn.ComboItems.ContainsKey(name))
						mappedColumnsColumn.ComboItems.Add(name, column);
				}
			}
			slyceGrid1.Columns.Add(mappedColumnsColumn);
			#endregion

			#region InKey column
			col = new ColumnItem("In Key", ColumnItem.ColumnTypes.Checkbox, false, "General");
			col.ColorScheme = ColumnItem.ColorSchemes.Blue;
			slyceGrid1.Columns.Add(col);
			#endregion

			#region ReadOnly column
			col = new ColumnItem("Read-only", ColumnItem.ColumnTypes.Checkbox, false, "General");
			col.ColorScheme = ColumnItem.ColorSchemes.Blue;
			slyceGrid1.Columns.Add(col);
			#endregion

			ArchAngel.Providers.EntityModel.Model.EntityLayer.PropertyImpl prop = new ArchAngel.Providers.EntityModel.Model.EntityLayer.PropertyImpl();

			foreach (ArchAngel.Interfaces.ITemplate.IUserOption uo in prop.Ex.OrderBy(u => u.Name))
			{
				if (uo.DataType == typeof(bool?))
					col = new ColumnItem(uo.Text, ColumnItem.ColumnTypes.NullableCheckBox, null, "General");
				else if (uo.DataType == typeof(string))
					col = new ColumnItem(uo.Text, ColumnItem.ColumnTypes.Textbox, null, "General");
				else if (uo.DataType == typeof(int))
					col = new ColumnItem(uo.Text, ColumnItem.ColumnTypes.IntegerInput, null, "General");
				else if (uo.DataType == typeof(bool))
					col = new ColumnItem(uo.Text, ColumnItem.ColumnTypes.Checkbox, null, "General");
				else if (uo.DataType.ToString() == "ArchAngel.Interfaces.NHibernateEnums.PropertyAccessTypes")
				{
					col = new ColumnItem(uo.Text, ColumnItem.ColumnTypes.ComboBox, null, "General");

					foreach (var x in Enum.GetNames(typeof(ArchAngel.Interfaces.NHibernateEnums.PropertyAccessTypes)).OrderBy(t => t))
						col.ComboItems.Add(x, null);
				}
				else if (uo.DataType.ToString() == "ArchAngel.Interfaces.NHibernateEnums.PropertyGeneratedTypes")
				{
					col = new ColumnItem(uo.Text, ColumnItem.ColumnTypes.ComboBox, null, "General");

					foreach (var x in Enum.GetNames(typeof(ArchAngel.Interfaces.NHibernateEnums.PropertyGeneratedTypes)).OrderBy(t => t))
						col.ComboItems.Add(x, null);
				}
				else
					throw new NotImplementedException("Type not handled yet: " + uo.DataType.Name);

				col.ColorScheme = ColumnItem.ColorSchemes.Blue;
				slyceGrid1.Columns.Add(col);
			}
			#region Validation options
			slyceGrid1.Columns.Add(new ColumnItem("Fractional Digits", ColumnItem.ColumnTypes.NullableIntegerInput, 0, "Validation (optional)") { ColorScheme = ColumnItem.ColorSchemes.Green });
			slyceGrid1.Columns.Add(new ColumnItem("Future Date", ColumnItem.ColumnTypes.NullableCheckBox, false, "Validation (optional)") { ColorScheme = ColumnItem.ColorSchemes.Green });
			slyceGrid1.Columns.Add(new ColumnItem("Integer Digits", ColumnItem.ColumnTypes.NullableIntegerInput, 0, "Validation (optional)") { ColorScheme = ColumnItem.ColorSchemes.Green });
			slyceGrid1.Columns.Add(new ColumnItem("Max Length", ColumnItem.ColumnTypes.NullableIntegerInput, 0, "Validation (optional)") { ColorScheme = ColumnItem.ColorSchemes.Green });
			slyceGrid1.Columns.Add(new ColumnItem("Min Length", ColumnItem.ColumnTypes.NullableIntegerInput, 0, "Validation (optional)") { ColorScheme = ColumnItem.ColorSchemes.Green });
			slyceGrid1.Columns.Add(new ColumnItem("Max Value", ColumnItem.ColumnTypes.NullableIntegerInput, 0, "Validation (optional)") { ColorScheme = ColumnItem.ColorSchemes.Green });
			slyceGrid1.Columns.Add(new ColumnItem("Min Value", ColumnItem.ColumnTypes.NullableIntegerInput, 0, "Validation (optional)") { ColorScheme = ColumnItem.ColorSchemes.Green });
			slyceGrid1.Columns.Add(new ColumnItem("Not Empty", ColumnItem.ColumnTypes.NullableCheckBox, false, "Validation (optional)") { ColorScheme = ColumnItem.ColorSchemes.Green });
			slyceGrid1.Columns.Add(new ColumnItem("Nullable", ColumnItem.ColumnTypes.NullableCheckBox, false, "Validation (optional)") { ColorScheme = ColumnItem.ColorSchemes.Green });
			slyceGrid1.Columns.Add(new ColumnItem("Past Date", ColumnItem.ColumnTypes.NullableCheckBox, false, "Validation (optional)") { ColorScheme = ColumnItem.ColorSchemes.Green });
			slyceGrid1.Columns.Add(new ColumnItem("Regex", ColumnItem.ColumnTypes.NullableTextBox, "", "Validation (optional)") { ColorScheme = ColumnItem.ColorSchemes.Green });
			slyceGrid1.Columns.Add(new ColumnItem("Validate", ColumnItem.ColumnTypes.NullableCheckBox, false, "Validation (optional)") { ColorScheme = ColumnItem.ColorSchemes.Green });
			#endregion

			foreach (ArchAngel.Providers.EntityModel.Model.EntityLayer.Property property in Entity.Properties.Except(Entity.ForeignKeyPropertiesToExclude))
			{
				AddPropertyToPropertiesGrid(property, hasMultiSchemas);
			}
			slyceGrid1.Populate();
			slyceGrid1.FrozenColumnIndex = 1;
			Slyce.Common.Utility.ResumePainting(slyceGrid1);
		}

		private void AddPropertyToPropertiesGrid(ArchAngel.Providers.EntityModel.Model.EntityLayer.Property property, bool hasMultiSchemas)
		{
			SlyceTreeGridItem gridItem = new SlyceTreeGridItem();
			gridItem.Tag = property;
			gridItem.SubItems.Add(new SlyceTreeGridCellItem(property.Name));
			gridItem.SubItems.Add(new SlyceTreeGridCellItem(property.Type));
			gridItem.SubItems.Add(new SlyceTreeGridCellItem(property.NHibernateType));

			int numTables = MappedTables.Count;
			IColumn mappedColumn = property.MappedColumn();
			string mappedColumnName;

			if (mappedColumn == null)
				mappedColumnName = "";
			else if (numTables == 1)
				mappedColumnName = mappedColumn.Name;
			else if (hasMultiSchemas)
				mappedColumnName = string.Format("{0}.{1}.{2}", mappedColumn.Parent.Schema, mappedColumn.Parent.Name, mappedColumn.Name);
			else
				mappedColumnName = string.Format("{0}.{1}", mappedColumn.Parent.Name, mappedColumn.Name);

			gridItem.SubItems.Add(new SlyceTreeGridCellItem(mappedColumnName));
			gridItem.SubItems.Add(new SlyceTreeGridCellItem(property.IsKeyProperty));
			gridItem.SubItems.Add(new SlyceTreeGridCellItem(property.ReadOnly));

			foreach (ArchAngel.Interfaces.ITemplate.IUserOption uo in property.Ex.OrderBy(u => u.Name))
			{
				if (uo.DataType == typeof(bool?))
				{
					bool? nullableBoolValue = (bool?)uo.Value;
					gridItem.SubItems.Add(new SlyceTreeGridCellItem(false, true, nullableBoolValue.HasValue ? nullableBoolValue.Value : false));
				}
				else if (uo.DataType == typeof(string))
					gridItem.SubItems.Add(new SlyceTreeGridCellItem((string)uo.Value));
				else if (uo.DataType == typeof(int))
					gridItem.SubItems.Add(new SlyceTreeGridCellItem((int)uo.Value));
				else if (uo.DataType == typeof(bool))
					gridItem.SubItems.Add(new SlyceTreeGridCellItem((bool)uo.Value));
				else if (uo.DataType.ToString() == "ArchAngel.Interfaces.NHibernateEnums.PropertyAccessTypes")
					gridItem.SubItems.Add(new SlyceTreeGridCellItem(uo.Value.ToString()));
				else if (uo.DataType.ToString() == "ArchAngel.Interfaces.NHibernateEnums.PropertyGeneratedTypes")
					gridItem.SubItems.Add(new SlyceTreeGridCellItem(uo.Value.ToString()));
				else
					throw new NotImplementedException("Type not handled yet: " + uo.DataType.Name);
			}
			gridItem.SubItems.Add(CreateNewNullableCell(property, property.ValidationOptions.FractionalDigits, ApplicableOptions.Digits));
			gridItem.SubItems.Add(CreateNewNullableCell(property, property.ValidationOptions.FutureDate, ApplicableOptions.Date));
			gridItem.SubItems.Add(CreateNewNullableCell(property, property.ValidationOptions.IntegerDigits, ApplicableOptions.Digits));
			gridItem.SubItems.Add(CreateNewNullableCell(property, property.ValidationOptions.MaximumLength, ApplicableOptions.Length));
			gridItem.SubItems.Add(CreateNewNullableCell(property, property.ValidationOptions.MinimumLength, ApplicableOptions.Length));
			gridItem.SubItems.Add(CreateNewNullableCell(property, property.ValidationOptions.MaximumValue, ApplicableOptions.Value));
			gridItem.SubItems.Add(CreateNewNullableCell(property, property.ValidationOptions.MinimumValue, ApplicableOptions.Value));
			gridItem.SubItems.Add(CreateNewNullableCell(property, property.ValidationOptions.NotEmpty, ApplicableOptions.NotEmpty));
			gridItem.SubItems.Add(CreateNewNullableCell(property, property.ValidationOptions.Nullable, ApplicableOptions.Nullable));
			gridItem.SubItems.Add(CreateNewNullableCell(property, property.ValidationOptions.PastDate, ApplicableOptions.Date));
			gridItem.SubItems.Add(CreateNewNullableCell(property, property.ValidationOptions.RegexPattern, ApplicableOptions.RegexPattern));
			gridItem.SubItems.Add(new SlyceTreeGridCellItem(property.ValidationOptions.Validate, (ValidationOptions.GetApplicableValidationOptionsForType(property.Type) & ApplicableOptions.Validate) != ApplicableOptions.Validate));

			slyceGrid1.Items.Add(gridItem);
		}

		private SlyceTreeGridCellItem CreateNewNullableCell(ArchAngel.Providers.EntityModel.Model.EntityLayer.Property property, object value, ApplicableOptions options)
		{
			ApplicableOptions applicableOptions = ValidationOptions.GetApplicableValidationOptionsForType(property.Type);
			SlyceTreeGridCellItem cell = new SlyceTreeGridCellItem(value, (applicableOptions & options) != options);
			cell.IsNullable = true;
			return cell;
		}

	}
}
