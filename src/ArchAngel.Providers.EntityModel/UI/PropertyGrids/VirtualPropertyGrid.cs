using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ArchAngel.Interfaces;
using ArchAngel.Interfaces.ITemplate;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using DevComponents.DotNetBar;
using DevComponents.Editors;
using Slyce.Common;
using Slyce.Common.StringExtensions;

namespace ArchAngel.Providers.EntityModel.UI.PropertyGrids
{
	public partial class VirtualPropertyGrid : UserControl
	{
		//private const int RowSize = 25;
		//private int CurrentRowHeight = 0;
		private Dictionary<IUserOption, Control> UserOptionLookups = new Dictionary<IUserOption, Control>();
		private Dictionary<Control, IUserOption> ControlLookups = new Dictionary<Control, IUserOption>();
		private readonly List<IUserOption> virtualProperties = new List<IUserOption>();

		public event EventHandler<GenericEventArgs<IUserOption>> VirtualPropertyValueChanged;

		public VirtualPropertyGrid()
		{
			InitializeComponent();
		}

		public ReferenceImpl Reference { get; set; }
		public bool IsEnd1 { get; set; }

		public void SetVirtualProperties(IEnumerable<IUserOption> vps)
		{
			Slyce.Common.Utility.SuspendPainting(this);

			Clear();

			int top = 5;
			int maxLabelWidth = 0;

			Label label = new Label();
			Graphics g = Graphics.FromHwnd(label.Handle);
			maxLabelWidth = Math.Max(maxLabelWidth, Convert.ToInt32(g.MeasureString("Name", label.Font).Width));

			for (int i = 0; i < vps.Count(); i++)
				maxLabelWidth = Math.Max(maxLabelWidth, Convert.ToInt32(g.MeasureString(vps.ElementAt(i).Name, label.Font).Width));

			maxLabelWidth += 10;

			foreach (var vp in vps.OrderBy(v => v.Name))
				top = AddVirtualProperty(vp, top, maxLabelWidth);

			LayoutOptions();
			Slyce.Common.Utility.ResumePainting(this);
		}

		internal void LayoutOptions()
		{
			int top = 0;

			foreach (IUserOption option in virtualProperties)
			{
				Control inputControl = UserOptionLookups[option];
				Control label = Controls[string.Format("label{0}", option.Name)];
				bool displayToUser = option.DisplayToUser;
				label.Visible = displayToUser;
				inputControl.Visible = displayToUser;

				if (displayToUser)
				{
					inputControl.Top = top;
					label.Top = top;
					top = Math.Max(inputControl.Bottom, label.Bottom) + 1;
				}
			}
			this.Height = top;

			foreach (Control control in this.Controls)
				control.Refresh();

			this.Refresh();
		}

		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;

				foreach (Control control in this.Controls)
				{
					control.Font = this.Font;
				}
			}
		}

		public void Clear()
		{
			for (int i = 0; i < this.Controls.Count; i++)
				superTooltip1.SetSuperTooltip(this.Controls[i], null);

			this.Controls.Clear();
			virtualProperties.Clear();
			UserOptionLookups.Clear();
			ControlLookups.Clear();
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
			labelPropertyName.Name = string.Format("label{0}", option.Name);
			labelPropertyName.Font = this.Font;
			labelPropertyName.Text = option.Text.Replace("End1:", "").Replace("End2:", "").Trim();
			labelPropertyName.BackColor = BackColor;
			labelPropertyName.ForeColor = ForeColor;

			Control inputControl;

			if (VirtualPropertyHelper.IsEntityPropertyType(option))
				inputControl = AddPropertySelectorInputControl(option);
			else if (VirtualPropertyHelper.IsEnumType(option))
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
			inputControl.Font = this.Font;

			labelPropertyName.Left = 5;
			labelPropertyName.AutoSize = false;
			labelPropertyName.Width = labelWidth;
			labelPropertyName.TextAlign = ContentAlignment.MiddleRight;
			inputControl.Left = labelPropertyName.Right + 5;
			inputControl.Top = labelPropertyName.Top = top;
			inputControl.Width = Width - inputControl.Left - 5;

			bool propertyEnabled = option.DisplayToUser;

			inputControl.Visible = propertyEnabled;
			labelPropertyName.Visible = propertyEnabled;
			labelPropertyName.Anchor = AnchorStyles.Left | AnchorStyles.Top;
			inputControl.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
			inputControl.Name = option.Name;

			if (inputControl is ComboBox)
			{
				inputControl.Width = Math.Min(inputControl.Width, 200);
				inputControl.Anchor = AnchorStyles.Left | AnchorStyles.Top;
			}
			else if (inputControl is IntegerInput || inputControl is DoubleInput)
			{
				inputControl.Width = 50;
				inputControl.Anchor = AnchorStyles.Left | AnchorStyles.Top;
			}
			this.Controls.Add(labelPropertyName);
			this.Controls.Add(inputControl);

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
			this.Height = labelPropertyName.Bottom + 10;
			labelPropertyName.Refresh();
			inputControl.Refresh();

			if (propertyEnabled)
				return Math.Max(labelPropertyName.Bottom, inputControl.Bottom);
			else
				return top;
		}

		private void SetTooltip(IComponent labelPropertyName, string header, string description)
		{
			superTooltip1.SetSuperTooltip(labelPropertyName, new SuperTooltipInfo(header, "", description, null, null, eTooltipColor.System));
		}

		private void OnVirtualPropertyValueChanged(Control control)
		{
			var originalValue = ControlLookups[control].Value;
			ControlLookups[control].Value = GetValueFrom(control, ControlLookups[control]);

			if (originalValue != ControlLookups[control].Value)
				ArchAngel.Interfaces.Events.RaiseIsDirtyEvent();

			RefreshVisibilities();
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
			DoubleInput tbOptionInput = new DoubleInput();
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
				//if (ExtensionAttributeHelper.HasNullValueAttribute(option.DataType, val))
				//{
				//    box.Items.Add(new ComboBoxItemEx<object>(val, o => ""));
				//    if (option.Value == null || option.Value == val)
				//        box.SelectedIndex = box.Items.Count - 1;
				//}
				//else
				//{
				box.Items.Add(new ComboBoxItemEx<object>(val, o => Enum.GetName(option.DataType, o)));
				if (option.Value != null && option.Value.Equals(val))
					box.SelectedIndex = box.Items.Count - 1;
				//}
			}
			box.SelectedIndexChanged += (sender, e) => OnVirtualPropertyValueChanged(sender as Control);

			return box;
		}

		private Control AddPropertySelectorInputControl(IUserOption option)
		{
			ComboBox box = new ComboBox();
			box.DropDownStyle = ComboBoxStyle.DropDownList;
			Entity entity = IsEnd1 ? Reference.Entity2 : Reference.Entity1;
			box.Items.Add(new ComboBoxItemEx<object>(null, p => GetPropertyName(null, "")));

			foreach (Property prop in entity.Properties)
			{
				box.Items.Add(new ComboBoxItemEx<object>(prop, p => GetPropertyName(p, "")));

				if (!(option.Value is string))
				{
					if (!(option.Value is ArchAngel.Interfaces.NHibernateEnums.PropertiesForThisEntity))
					{
						ArchAngel.Providers.EntityModel.Model.EntityLayer.Property optionProp = (ArchAngel.Providers.EntityModel.Model.EntityLayer.Property)option.Value;

						if (option.Value != null && optionProp.Name.Equals(prop.Name, StringComparison.InvariantCultureIgnoreCase))
							box.SelectedIndex = box.Items.Count - 1;
					}
				}
			}
			box.SelectedIndexChanged += (sender, e) => OnVirtualPropertyValueChanged(sender as Control);

			return box;
		}

		private string GetPropertyName(object property, string dummy)
		{
			if (property == null)
				return "";

			return ((Property)property).Name;
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
			IntegerInput tbOptionInput = new IntegerInput();
			tbOptionInput.Text = option.Value != null ? option.Value.ToString() : "";
			tbOptionInput.ValueChanged += (sender, e) => OnVirtualPropertyValueChanged(sender as Control);
			tbOptionInput.ForeColor = Color.Black;

			return tbOptionInput;
		}

		private Control AddStringInputControl(IUserOption option)
		{
			TextBox tbOptionInput = new TextBox();
			tbOptionInput.Text = option.Value != null ? option.Value.ToString() : "";
			tbOptionInput.TextChanged += (sender, e) => OnVirtualPropertyValueChanged(sender as Control);

			return tbOptionInput;
		}

		public void RefreshVisibilities()
		{
			for (int i = 0; i < virtualProperties.Count; i++)
			{
				IUserOption virtualProperty = virtualProperties[i];
				bool display = virtualProperty.DisplayToUser;

				//Control control = this.Controls[(i * 2) + 1];
				Control control = UserOptionLookups[virtualProperty];// this.Controls[virtualProperty.Name];
				control.Visible = display;

				//if (display == false)
				//{
				//    Label label = (Label)this.Controls[i * 2];
				//    SetTooltip(label, "Disabled", virtualProperty.Description);
				//}
			}
			LayoutOptions();
		}
	}
}
