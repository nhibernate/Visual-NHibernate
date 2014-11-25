using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using Slyce.Common;

namespace ArchAngel.Providers.EntityModel.UI.PropertyGrids
{
    public partial class ValidationPropertyGrid : UserControl
    {
        private ValidationOptions _options;

        public ValidationPropertyGrid()
        {
            InitializeComponent();
        }

        private Dictionary<string, bool> OptionsVisibility = new Dictionary<string, bool>();

        public void SetAvailableOptions(ApplicableOptions options)
        {
            OptionsVisibility.Clear();

            OptionsVisibility.Add("nhv_nullable", options.IsSet(ApplicableOptions.Nullable));
            OptionsVisibility.Add("nhv_validate", options.IsSet(ApplicableOptions.Validate));
            OptionsVisibility.Add("nhv_min_value", options.IsSet(ApplicableOptions.Value));
            OptionsVisibility.Add("nhv_max_value", options.IsSet(ApplicableOptions.Value));
            OptionsVisibility.Add("nhv_int_digits", options.IsSet(ApplicableOptions.Digits));
            OptionsVisibility.Add("nhv_frac_digits", options.IsSet(ApplicableOptions.Digits));
            OptionsVisibility.Add("nhv_not_empty", options.IsSet(ApplicableOptions.NotEmpty));
            OptionsVisibility.Add("nhv_min_length", options.IsSet(ApplicableOptions.Length));
            OptionsVisibility.Add("nhv_max_length", options.IsSet(ApplicableOptions.Length));
            OptionsVisibility.Add("nhv_reg_exp", options.IsSet(ApplicableOptions.RegexPattern));
            OptionsVisibility.Add("nhv_past_date", options.IsSet(ApplicableOptions.Date));
            OptionsVisibility.Add("nhv_future_date", options.IsSet(ApplicableOptions.Date));

            SetNodeVisibility();
        }

        /// <summary>
        /// Sets the visibility of all nodes
        /// </summary>
        private void SetNodeVisibility()
        {
            foreach (var nodeName in OptionsVisibility.Keys)
            {
                SetNodeVisibility(nodeName, OptionsVisibility[nodeName]);
            }
        }
        
        private delegate void SetOptionValue(object value);

        private void ValueChanged(DevComponents.DotNetBar.Controls.CheckBoxX useCheckBox, Control valueControl, SetOptionValue del)
        {
            if (_options == null) return;

            if (!useCheckBox.Checked)
                del(null);
            else
            {
                Type t = valueControl.GetType();

                switch (t.Name)
                {
                    case "CheckBoxX":
                        del(((DevComponents.DotNetBar.Controls.CheckBoxX)valueControl).Checked);
                        break;
                    case "TextBox":
                        del(((TextBox)valueControl).Text);
                        break;
                    case "IntegerInput":
                        del(((DevComponents.Editors.IntegerInput)valueControl).Value);
                        break;
                    case "DoubleInput":
                        del(((DevComponents.Editors.DoubleInput)valueControl).Value);
                        break;
                    default:
                        throw new NotImplementedException("Type of control not handled yet: " + t.Name);
                }
            }
        }

        public void SetValidationOptions(ValidationOptions options)
        {
            Clear();
            _options = options;

            if (options == null) { return; }

            Utility.SuspendPainting(this);

            AddNode("nhv_validate", "Validate", "", InputTypes.Boolean, options.Validate, true, 
                delegate(object value) { _options.Validate = (bool)value; } );

            AddNode("nhv_nullable", "Nullable", "Check if the property is not null", InputTypes.Boolean, options.Nullable, options.Nullable.HasValue,
                delegate(object value) { _options.Nullable = (bool?)value; } );

            AddNode("nhv_min_value", "Minimum Value", "Checks if the value >= value", InputTypes.Integer, options.MinimumValue, options.MinimumValue.HasValue,
                delegate(object value) { _options.MinimumValue = (int?)value; } );

            AddNode("nhv_max_value", "Maximum Value", "Checks if the value <= value", InputTypes.Integer, options.MaximumValue, options.MaximumValue.HasValue,
                delegate(object value) { _options.MaximumValue = (int?)value; } );

            AddNode("nhv_int_digits", "Integer Digits", "Checks if the property has no more than this number of integer digits", InputTypes.Integer, options.IntegerDigits, options.IntegerDigits.HasValue,
                delegate(object value) { _options.IntegerDigits = (int?)value; } );

            AddNode("nhv_frac_digits", "Fractional Digits", "Checks if the property has no more than this number of fractional digits", InputTypes.Integer, options.FractionalDigits, options.FractionalDigits.HasValue,
                delegate(object value) { _options.FractionalDigits = (int?)value; } );

            AddNode("nhv_not_empty", "Not Empty", "Check if the string property is not null nor empty", InputTypes.Boolean, options.NotEmpty, options.NotEmpty.HasValue,
                delegate(object value) { _options.NotEmpty = (bool?)value; } );

            AddNode("nhv_min_length", "Minimum Length", "Checks if the string length >= value", InputTypes.Integer, options.MinimumLength, options.MinimumLength.HasValue,
                delegate(object value) { _options.MinimumLength = (int?)value; } );

            AddNode("nhv_max_length", "Maximum Length", "Checks if the string length <= value", InputTypes.Integer, options.MaximumLength, options.MaximumLength.HasValue,
                delegate(object value) { _options.MaximumLength = (int?)value; } );

            AddNode("nhv_reg_exp", "Regular Expression", "Check if the property matches the regular expression given a match flag.", InputTypes.String, options.RegexPattern, !string.IsNullOrEmpty(options.RegexPattern),
                delegate(object value) { _options.RegexPattern = value == null ? null : (string) value; } );

            AddNode("nhv_past_date", "Past Date", "Check if the date is in the past", InputTypes.Boolean, options.PastDate, options.PastDate.HasValue,
                delegate(object value) { _options.PastDate = (bool?) value; } );

            AddNode("nhv_future_date", "Future Date", "Check if the date is in the future", InputTypes.Boolean, options.FutureDate, options.FutureDate.HasValue,
                delegate(object value) { _options.FutureDate = (bool?) value; } );

            SetNodeVisibility();
            advTree1.ExpandAll();
            Utility.ResumePainting(this);
        }

        private void Clear()
        {
            Utility.SuspendPainting(this);
            advTree1.Nodes.Clear();
            _options = null;
            Utility.ResumePainting(this);
        }

        #region Treeview

        private enum InputTypes
        {
            Integer,
            Double,
            String,
            Boolean
        }

        /// <summary>
        /// Sets the visibility of the specified node.
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="visible"></param>
        private void SetNodeVisibility(string tag, bool visible)
        {
            DevComponents.AdvTree.Node node = advTree1.FindNodeByName(tag);

            if (node != null)
                node.Visible = visible;
        }

        private void ClearNode(string tag)
        {
            DevComponents.AdvTree.Node node = advTree1.FindNodeByName(tag);

            if (node != null)
            {
                ((DevComponents.DotNetBar.Controls.CheckBoxX)node.HostedControl).Checked = false;
                node.Cells[0].Enabled = false;
                node.Cells[1].Enabled = false;

                Control valueControl = node.Cells[2].HostedControl;
                Type t = node.Cells[2].HostedControl.GetType();

                switch (t.Name)
                {
                    case "CheckBoxX":
                        ((DevComponents.DotNetBar.Controls.CheckBoxX)valueControl).Checked = false;
                        break;
                    case "TextBox":
                        ((TextBox)valueControl).Text = "";
                        break;
                    case "IntegerInput":
                        ((DevComponents.Editors.IntegerInput)valueControl).Value = 0;
                        break;
                    case "DoubleInput":
                        ((DevComponents.Editors.DoubleInput)valueControl).Value = 0;
                        break;
                    default:
                        throw new NotImplementedException("Type not handled yet: " + t.Name);
                }
            }
        }

        private DevComponents.AdvTree.Node AddCategoryNode(string name, string tag)
        {
            DevComponents.AdvTree.Node categoryNode = new DevComponents.AdvTree.Node()
            {
                Name = tag,
                Text = name,
                FullRowBackground = true,
                Style = new DevComponents.DotNetBar.ElementStyle()
                {
                    BackColor = Color.DarkGray,
                    BackColor2 = Color.LightGray,
                    TextColor = Color.White
                }
            };
            advTree1.Nodes.Add(categoryNode);
            return categoryNode;
        }

        private DevComponents.AdvTree.Node AddNode(string tag, string name, string description, InputTypes inputType, object value, bool selected, SetOptionValue setOptionValue)
        {
            return AddNode(null, tag, name, description, inputType, value, selected, setOptionValue);
        }

        private DevComponents.AdvTree.Node AddNode(DevComponents.AdvTree.Node parentNode, string tag, string name, string description, InputTypes inputType, object value, bool selected, SetOptionValue setOptionValue)
        {
            DevComponents.DotNetBar.SuperTooltipInfo tooltip = new DevComponents.DotNetBar.SuperTooltipInfo(name, null, description, null, null, DevComponents.DotNetBar.eTooltipColor.Gray);

            DevComponents.DotNetBar.Controls.CheckBoxX checkBoxSelect = new DevComponents.DotNetBar.Controls.CheckBoxX()
            {
                Checked = selected,
                Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled,
                BackColor = Color.Transparent,
                CheckBoxPosition = DevComponents.DotNetBar.eCheckBoxPosition.Right
            };
            DevComponents.AdvTree.Node newNode = new DevComponents.AdvTree.Node()
            {
                HostedControl = checkBoxSelect,
                Name = tag
            };
            DevComponents.AdvTree.Cell nameCell = new DevComponents.AdvTree.Cell()
            {
                Text = name
            };
            DevComponents.AdvTree.Cell valueCell = new DevComponents.AdvTree.Cell()
            {
                EditorType = DevComponents.AdvTree.eCellEditorType.Custom,
                Editable = true
            };
            switch (inputType)
            {
                case InputTypes.Double:
                    double? doubleValue = (double?)value;
                    DevComponents.Editors.DoubleInput doubleInput = new DevComponents.Editors.DoubleInput()
                    {
                        Value = doubleValue.HasValue ? doubleValue.Value : 0
                    };
                    valueCell.HostedControl = doubleInput;
                    doubleInput.ValueChanged += delegate(object sender, EventArgs e) { ValueChanged(checkBoxSelect, doubleInput, setOptionValue); };
                    break;
                case InputTypes.Integer:
                    int? intValue = (int?)value;
                    DevComponents.Editors.IntegerInput integerInput = new DevComponents.Editors.IntegerInput()
                    {
                        Value = intValue.HasValue ? intValue.Value : 0
                    };
                    valueCell.HostedControl = integerInput;
                    integerInput.ValueChanged += delegate(object sender, EventArgs e) { ValueChanged(checkBoxSelect, integerInput, setOptionValue); };
                    break;
                case InputTypes.String:
                    TextBox textBox = new TextBox()
                    {
                        Text = (string)value
                    };
                    valueCell.HostedControl = textBox;
                    textBox.TextChanged += delegate(object sender, EventArgs e) { ValueChanged(checkBoxSelect, textBox, setOptionValue); };
                    break;
                case InputTypes.Boolean:
                    bool? boolValue = (bool?)value;

                    DevComponents.DotNetBar.Controls.CheckBoxX checkBox = new DevComponents.DotNetBar.Controls.CheckBoxX()
                    {
                        Checked = boolValue.HasValue ? boolValue.Value : false,
                        Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled,
                        BackColor = Color.Transparent,
                    };
                    checkBox.CheckedChanged += delegate (object sender, EventArgs e) { ValueChanged(checkBoxSelect, checkBox, setOptionValue); };
                    valueCell.HostedControl = checkBox;
                    break;
                default:
                    throw new NotImplementedException("InputType not handled yet: " + inputType.ToString());
            }
            newNode.Cells.Add(nameCell);
            newNode.Cells.Add(valueCell);

            nameCell.Enabled = checkBoxSelect.Checked;
            valueCell.Enabled = checkBoxSelect.Checked;
            valueCell.HostedControl.Enabled = checkBoxSelect.Checked;

            superTooltip1.SetSuperTooltip(newNode, tooltip);
            superTooltip1.SetSuperTooltip(checkBoxSelect, tooltip);
            superTooltip1.SetSuperTooltip(valueCell.HostedControl, tooltip);

            checkBoxSelect.CheckedChanged += delegate
            {
                nameCell.Enabled = checkBoxSelect.Checked;
                valueCell.Enabled = checkBoxSelect.Checked;
                valueCell.HostedControl.Enabled = checkBoxSelect.Checked;

                if (checkBoxSelect.Checked)
                    valueCell.HostedControl.Focus();
            };
            if (parentNode == null)
                advTree1.Nodes.Add(newNode);
            else
                parentNode.Nodes.Add(newNode);

            return newNode;
        }

        #endregion
    }
}