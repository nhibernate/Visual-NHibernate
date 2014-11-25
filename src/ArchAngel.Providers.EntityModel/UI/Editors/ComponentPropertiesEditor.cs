using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using Slyce.Common.Controls.Diagramming.SlyceGrid;

namespace ArchAngel.Providers.EntityModel.UI.Editors
{
    public partial class ComponentPropertiesEditor : UserControl
    {
        public event EventHandler ComponentChanged;
        private ComponentSpecification _ComponentSpecification;

        public ComponentPropertiesEditor()
        {
            InitializeComponent();

            slyceGrid1.DeleteClicked += new SlyceGrid.DeleteClickedDelegate(slyceGrid1_DeleteClicked);
            slyceGrid1.CellValueChanged += new SlyceGrid.CellValueChangedDelegate(slyceGrid1_CellValueChanged);
            slyceGrid1.NewRowAdded += new SlyceGrid.NewRowAddedDelegate(slyceGrid1_NewRowAdded);

            slyceGrid1.InvalidColor = Color.FromArgb(100, 100, 100);
            slyceGrid1.DisabledColor = Color.FromArgb(25, 25, 25);
            slyceGrid1.BackColor = Color.Black;//.FromArgb(13, 13, 13);
        }

        private void slyceGrid1_NewRowAdded(out object newObject)
        {
            ComponentPropertyImpl property = new ComponentPropertyImpl("NewProperty")
            {
                Type = "System.String"
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

            ComponentSpecification.AddProperty(property);
            newObject = property;

            AddPropertyToPropertiesGrid(property);

            if (ComponentChanged != null)
                ComponentChanged(ComponentSpecification, null);
        }

        private bool slyceGrid1_DeleteClicked(int row, object tag)
        {
            if (MessageBox.Show(this, string.Format("Delete {0}?", ((ComponentPropertyImpl)tag).Name), "Delete Property", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ComponentSpecification.RemovePropertyAndMarkers((ComponentPropertyImpl)tag);
                
                if (ComponentChanged != null)
                    ComponentChanged(ComponentSpecification, null);

                return true;
            }
            return false;
        }

        private void slyceGrid1_CellValueChanged(int row, int cell, int columnIndex, string columnHeader, ref object tag, object newValue)
        {
            ComponentPropertyImpl property = (ComponentPropertyImpl)tag;

            if (property == null)
                return;

            bool nullableColumn = string.IsNullOrEmpty(columnHeader);

            if (nullableColumn)
            {
                columnHeader = slyceGrid1.Columns[columnIndex].Text;
            }

            switch (columnHeader)
            {
                case "Name":
                    property.Name = (string)newValue;
                    break;
                case "Type":
                    property.Type = (string)newValue;
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
            if (ComponentChanged != null)
                ComponentChanged(ComponentSpecification, null);
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
                //returnVal = string.IsNullOrEmpty(stringValue) ? 0 : Convert.ToInt32(newValue);
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


        public ComponentSpecification ComponentSpecification
        {
            get { return _ComponentSpecification; }
            set
            {
                _ComponentSpecification = value;

                if (_ComponentSpecification != null)
                {
                    Populate();
                }
            }
        }

        private void Populate()
        {
            Slyce.Common.Utility.SuspendPainting(slyceGrid1);
            slyceGrid1.Clear();
            // Populate Columns from Component
            slyceGrid1.Columns.Add(new ColumnItem("Name", ColumnItem.ColumnTypes.Textbox, "NewProp", "General"));
            slyceGrid1.Columns.Add(new ColumnItem("Type", ColumnItem.ColumnTypes.Textbox, "System.String", "General"));

            ComponentPropertyImpl prop = new ComponentPropertyImpl();

            foreach (ArchAngel.Interfaces.ITemplate.IUserOption uo in prop.Ex)
            {
                if (uo.DataType == typeof(bool?))
                    slyceGrid1.Columns.Add(new ColumnItem(uo.Text, ColumnItem.ColumnTypes.NullableCheckBox, null, "General"));
                else if (uo.DataType == typeof(string))
                    slyceGrid1.Columns.Add(new ColumnItem(uo.Text, ColumnItem.ColumnTypes.Textbox, null, "General"));
                else if (uo.DataType == typeof(int))
                    slyceGrid1.Columns.Add(new ColumnItem(uo.Text, ColumnItem.ColumnTypes.Textbox, null, "General"));
                else if (uo.DataType == typeof(bool))
                    slyceGrid1.Columns.Add(new ColumnItem(uo.Text, ColumnItem.ColumnTypes.Checkbox, null, "General"));
                else
                    throw new NotImplementedException("Type not handled yet: " + uo.DataType.Name);
            }
            #region Validation options
            slyceGrid1.Columns.Add(new ColumnItem("Fractional Digits", ColumnItem.ColumnTypes.NullableIntegerInput, 0, "Validation (optional)"));
            slyceGrid1.Columns.Add(new ColumnItem("Future Date", ColumnItem.ColumnTypes.NullableCheckBox, false, "Validation (optional)"));
            slyceGrid1.Columns.Add(new ColumnItem("Integer Digits", ColumnItem.ColumnTypes.NullableIntegerInput, 0, "Validation (optional)"));
            slyceGrid1.Columns.Add(new ColumnItem("Max Length", ColumnItem.ColumnTypes.NullableIntegerInput, 0, "Validation (optional)"));
            slyceGrid1.Columns.Add(new ColumnItem("Min Length", ColumnItem.ColumnTypes.NullableIntegerInput, 0, "Validation (optional)"));
            slyceGrid1.Columns.Add(new ColumnItem("Max Value", ColumnItem.ColumnTypes.NullableIntegerInput, 0, "Validation (optional)"));
            slyceGrid1.Columns.Add(new ColumnItem("Min Value", ColumnItem.ColumnTypes.NullableIntegerInput, 0, "Validation (optional)"));
            slyceGrid1.Columns.Add(new ColumnItem("Not Empty", ColumnItem.ColumnTypes.NullableCheckBox, false, "Validation (optional)"));
            slyceGrid1.Columns.Add(new ColumnItem("Nullable", ColumnItem.ColumnTypes.NullableCheckBox, false, "Validation (optional)"));
            slyceGrid1.Columns.Add(new ColumnItem("Past Date", ColumnItem.ColumnTypes.NullableCheckBox, false, "Validation (optional)"));
            slyceGrid1.Columns.Add(new ColumnItem("Regex", ColumnItem.ColumnTypes.NullableTextBox, "", "Validation (optional)"));
            slyceGrid1.Columns.Add(new ColumnItem("Validate", ColumnItem.ColumnTypes.NullableCheckBox, false, "Validation (optional)"));
            #endregion

            foreach (ComponentPropertyImpl property in ComponentSpecification.Properties)
            {
                AddPropertyToPropertiesGrid(property);
            }
            slyceGrid1.Populate();
            Slyce.Common.Utility.ResumePainting(slyceGrid1);
        }

        private void AddPropertyToPropertiesGrid(ComponentPropertyImpl property)
        {
            SlyceTreeGridItem gridItem = new SlyceTreeGridItem();
            gridItem.Tag = property;
            gridItem.SubItems.Add(new SlyceTreeGridCellItem(property.Name));
            gridItem.SubItems.Add(new SlyceTreeGridCellItem(property.Type));

            foreach (ArchAngel.Interfaces.ITemplate.IUserOption uo in property.Ex)
            {
                if (uo.DataType == typeof(bool?))
                {
                    bool? nullableBoolValue = (bool?)uo.Value;
                    gridItem.SubItems.Add(new SlyceTreeGridCellItem(false, true, nullableBoolValue.HasValue ? nullableBoolValue.Value : false));
                }
                else if (uo.DataType == typeof(string))
                {
                    gridItem.SubItems.Add(new SlyceTreeGridCellItem((string)uo.Value));
                }
                else if (uo.DataType == typeof(int))
                {
                    gridItem.SubItems.Add(new SlyceTreeGridCellItem((int)uo.Value));
                }
                else if (uo.DataType == typeof(bool))
                {
                    gridItem.SubItems.Add(new SlyceTreeGridCellItem((bool)uo.Value));
                }
                else
                {
                    throw new NotImplementedException("Type not handled yet: " + uo.DataType.Name);
                }
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

        private SlyceTreeGridCellItem CreateNewNullableCell(ComponentPropertyImpl property, object value, ApplicableOptions options)
        {
            ApplicableOptions applicableOptions = ValidationOptions.GetApplicableValidationOptionsForType(property.Type);
            SlyceTreeGridCellItem cell = new SlyceTreeGridCellItem(value, (applicableOptions & options) != options);
            cell.IsNullable = true;
            return cell;
        }

    }
}
