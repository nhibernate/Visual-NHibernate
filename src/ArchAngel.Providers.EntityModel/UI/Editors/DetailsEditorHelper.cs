using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ArchAngel.Interfaces;
using ArchAngel.Interfaces.ITemplate;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using Slyce.Common;
using Slyce.Common.StringExtensions;

namespace ArchAngel.Providers.EntityModel.UI.Editors
{
    internal class DetailsEditorHelper
    {
        private Control Control;
        private readonly List<IUserOption> virtualProperties = new List<IUserOption>();
        private Dictionary<IUserOption, Control> UserOptionLookups = new Dictionary<IUserOption, Control>();
        private Dictionary<Control, IUserOption> ControlLookups = new Dictionary<Control, IUserOption>();
        private Color BackColor;
        private Color ForeColor;
        private SuperTooltip superTooltip1;

        internal DetailsEditorHelper(Control control, Color backColor, Color foreColor, SuperTooltip supertooltip)
        {
            Control = control;
            BackColor = backColor;
            ForeColor = foreColor;
            superTooltip1 = supertooltip;
        }

        internal void Clear()
        {
            virtualProperties.Clear();
            UserOptionLookups.Clear();
            ControlLookups.Clear();
        }

        internal int AddLabel(string text, int top, int width)
        {
            Label label = new Label();
            label.TextAlign = ContentAlignment.MiddleRight;
            label.Width = width;
            label.Top = top;
            label.Text = text;
            label.Left = 5;
            label.Height = 20;
            label.Margin = new System.Windows.Forms.Padding(0);
            Control.Controls.Add(label);
            return label.Bottom + 1;
        }

        private int AddUserOption(ArchAngel.Interfaces.ITemplate.IUserOption userOption, int top, int width)
        {
            int newTop = AddLabel(userOption.Text, top, width);

            return newTop;
        }

        private int GetVirtualPropertyControl(ArchAngel.Interfaces.ITemplate.IUserOption option, int CurrentRowHeight)
        {
            Label labelPropertyName = new Label();
            labelPropertyName.Text = option.Text;

            Control inputControl;

            if (VirtualPropertyHelper.IsEnumType(option))
                inputControl = AddEnumInputControl(option);
            else if (VirtualPropertyHelper.IsStringType(option))
                inputControl = AddStringInputControl(option);
            else if (VirtualPropertyHelper.IsIntegerNumericType(option))
                inputControl = AddIntegerInputControl(option);
            else if (VirtualPropertyHelper.IsCharType(option))
                inputControl = AddCharInputControl(option);
            else if (VirtualPropertyHelper.IsDecimalNumericType(option))
                inputControl = AddDecimalInputControl(option);
            else if (VirtualPropertyHelper.IsBoolType(option))
                inputControl = AddBoolInputControl(option);
            else
                throw new NotImplementedException("Not handled yet");
            //return;

            labelPropertyName.Left = 5;
            labelPropertyName.AutoSize = true;
            inputControl.Left = labelPropertyName.PreferredWidth + 15;
            inputControl.Top = labelPropertyName.Top = CurrentRowHeight;
            inputControl.Width = Control.Width - inputControl.Left - 5;

            bool propertyEnabled = option.DisplayToUser;
            inputControl.Enabled = propertyEnabled;

            labelPropertyName.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            inputControl.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            Control.Controls.Add(labelPropertyName);
            Control.Controls.Add(inputControl);

            if (propertyEnabled)
                SetTooltip(labelPropertyName, "", option.Description);
            else
                SetTooltip(labelPropertyName, "Disabled", option.Description);

            //CurrentRowHeight += 40;

            //Height = CurrentRowHeight + 50;
            return Math.Max(labelPropertyName.Bottom, inputControl.Bottom);
        }

        private Control AddBoolInputControl(IUserOption option)
        {
            CheckBox cbOptionInput = new CheckBox();
            cbOptionInput.Checked = (bool)option.Value;
            cbOptionInput.CheckedChanged += (sender, e) => OnVirtualPropertyValueChanged(sender as Control);
            cbOptionInput.AutoEllipsis = true;
            //cbOptionInput.Text = option.Text;

            return cbOptionInput;
        }

        private Control AddDecimalInputControl(IUserOption option)
        {
            TextBox tbOptionInput = new TextBox();
            tbOptionInput.Text = option.Value != null ? option.Value.ToString() : "";
            tbOptionInput.TextChanged += (sender, e) => OnVirtualPropertyValueChanged(sender as Control);

            return tbOptionInput;
        }

        private Control AddEnumInputControl(IUserOption option)
        {
            ComboBox box = new ComboBox();
            box.DropDownStyle = ComboBoxStyle.DropDownList;

            foreach (object val in Enum.GetValues(option.DataType))
            {
                if (ExtensionAttributeHelper.HasNullValueAttribute(option.DataType, val))
                {
                    box.Items.Add(new ComboBoxItemEx<object>(val, o => ""));
                    if (option.Value == null || option.Value == val)
                        box.SelectedIndex = box.Items.Count - 1;
                }
                else
                {
                    box.Items.Add(new ComboBoxItemEx<object>(val, o => Enum.GetName(option.DataType, o)));
                    if (option.Value != null && option.Value.Equals(val))
                        box.SelectedIndex = box.Items.Count - 1;
                }
            }

            box.SelectedIndexChanged += (sender, e) => OnVirtualPropertyValueChanged(sender as Control);

            return box;
        }

        private Control AddCharInputControl(IUserOption option)
        {
            TextBox tbOptionInput = new TextBox();
            tbOptionInput.MaxLength = 1;
            tbOptionInput.Text = option.Value != null ? option.Value.ToString() : "";
            tbOptionInput.TextChanged += (sender, e) => OnVirtualPropertyValueChanged(sender as Control);

            return tbOptionInput;
        }

        private Control AddIntegerInputControl(IUserOption option)
        {
            TextBox tbOptionInput = new TextBox();
            tbOptionInput.Text = option.Value != null ? option.Value.ToString() : "";
            tbOptionInput.TextChanged += (sender, e) => OnVirtualPropertyValueChanged(sender as Control);

            return tbOptionInput;
        }

        private Control AddStringInputControl(IUserOption option)
        {
            TextBox tbOptionInput = new TextBox();
            tbOptionInput.Text = option.Value != null ? option.Value.ToString() : "";
            tbOptionInput.TextChanged += (sender, e) => OnVirtualPropertyValueChanged(sender as Control);

            return tbOptionInput;
        }

        private void OnVirtualPropertyValueChanged(Control control)
        {
            ControlLookups[control].Value = GetValueFrom(control, ControlLookups[control]);
            RefreshVisibilities();
        }

        private void SetTooltip(IComponent labelPropertyName, string header, string description)
        {
            superTooltip1.SetSuperTooltip(labelPropertyName, new SuperTooltipInfo(header, "", description, null, null, eTooltipColor.System));
        }

        public object GetVirtualPropertyValue(IUserOption userOption)
        {
            return GetValueFrom(UserOptionLookups[userOption], userOption);
        }

        private object GetValueFrom(Control control, IUserOption option)
        {
            if (VirtualPropertyHelper.IsEnumType(option))
            {
                ComboBoxItemEx<object> item = ((ComboBox)control).SelectedItem as ComboBoxItemEx<object>;
                if (item == null) return null;

                return item.Object;
            }
            if (VirtualPropertyHelper.IsStringType(option))
                return control.Text;

            if (VirtualPropertyHelper.IsIntegerNumericType(option))
                return control.Text.As<int>();

            if (VirtualPropertyHelper.IsCharType(option))
                return control.Text[0];

            if (VirtualPropertyHelper.IsDecimalNumericType(option))
                return control.Text.As<decimal>();

            if (VirtualPropertyHelper.IsBoolType(option))
                return ((CheckBox)control).Checked;

            return null;
        }

        private int AddVirtualProperty(IUserOption option, int top, int labelWidth)
        {
            virtualProperties.Add(option);

            Label labelPropertyName = new Label();
            labelPropertyName.Text = option.Text;
            labelPropertyName.BackColor = BackColor;
            labelPropertyName.ForeColor = ForeColor;

            Control inputControl;

            if (VirtualPropertyHelper.IsEnumType(option))
                inputControl = AddEnumInputControl(option);
            else if (VirtualPropertyHelper.IsStringType(option))
                inputControl = AddStringInputControl(option);
            else if (VirtualPropertyHelper.IsIntegerNumericType(option))
                inputControl = AddIntegerInputControl(option);
            else if (VirtualPropertyHelper.IsCharType(option))
                inputControl = AddCharInputControl(option);
            else if (VirtualPropertyHelper.IsDecimalNumericType(option))
                inputControl = AddDecimalInputControl(option);
            else if (VirtualPropertyHelper.IsBoolType(option))
                inputControl = AddBoolInputControl(option);
            else
                throw new NotImplementedException("Not handled yet");
            //return;

            labelPropertyName.Left = 5;
            labelPropertyName.AutoSize = false;
            labelPropertyName.Width = labelWidth;
            labelPropertyName.TextAlign = ContentAlignment.MiddleRight;
            inputControl.Left = labelPropertyName.Right + 5;
            inputControl.Top = labelPropertyName.Top = top;
            inputControl.Width = Control.Width - inputControl.Left - 5;

            bool propertyEnabled = option.DisplayToUser;
            inputControl.Enabled = propertyEnabled;

            labelPropertyName.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            inputControl.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            Control.Controls.Add(labelPropertyName);
            Control.Controls.Add(inputControl);

            if (propertyEnabled)
            {
                SetTooltip(labelPropertyName, "", option.Description);
                SetTooltip(inputControl, "", option.Description);
            }
            else
            {
                SetTooltip(labelPropertyName, "Disabled", option.Description);
                SetTooltip(inputControl, "Disabled", option.Description);
            }
            //CurrentRowHeight += 40;

            //Height = CurrentRowHeight + 50;
            UserOptionLookups.Add(option, inputControl);
            ControlLookups.Add(inputControl, option);
            return Math.Max(labelPropertyName.Bottom, inputControl.Bottom);
        }

        public void RefreshVisibilities()
        {
            for (int i = 0; i < virtualProperties.Count; i++)
            {
                var virtualProperty = virtualProperties[i];
                bool display = virtualProperty.DisplayToUser;

                Control control = Control.Controls[(i * 2) + 1];
                control.Enabled = display;
                
                if (display == false)
                {
                    Label label = (Label)Control.Controls[i * 2];
                    SetTooltip(label, "Disabled", virtualProperty.Description);
                }
            }
        }

    }
}
