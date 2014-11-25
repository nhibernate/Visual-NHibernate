using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ArchAngel.Common.DesignerProject;
using Slyce.Common;
using Slyce.Common.EventExtensions;

namespace ArchAngel.Designer.UI.PropertyGrids
{
	public partial class FormUserOption : UserControl, IEventSender
	{
		private UserOption _UserOption;
		private bool BusyPopulating = false;
		private readonly List<Type> userOptionTypes = new List<Type>();

		public event EventHandler<TextChangedEventArgs> UserOptionCategoryChanged;
		public event EventHandler<TextChangedEventArgs> UserOptionNameChanged;

		public FormUserOption()
		{
			InitializeComponent();
		}

		internal UserOption UserOption
		{
			get { return _UserOption; }
			set
			{
				if (_UserOption != value)
				{
					_UserOption = value;
					Populate();
				}
			}
		}

		private void Populate()
		{
			BusyPopulating = true;

			EventRaisingDisabled = true;

			PopulateCategories();
			PopulateIterators();
			PopulateVariableTypes();
			dataGridEnumValues.Rows.Clear();

			if (UserOption != null)
			{
				if (UserOption.VarType == null) UserOption.VarType = typeof(string);

				comboBoxType.SelectedIndex = userOptionTypes.IndexOf(UserOption.VarType);
				textBoxVariableName.Text = UserOption.VariableName;
				textBoxText.Text = UserOption.Text;
				textBoxDescription.Text = UserOption.Description;
				checkBoxResetPerSession.Checked = UserOption.ResetPerSession;
				comboBoxCategory.Text = UserOption.Category;
				PopulateEnumValues();
			}
			BusyPopulating = false;
			EventRaisingDisabled = false;
		}

		private void PopulateEnumValues()
		{
			dataGridEnumValues.Rows.Clear();

			foreach (string val in UserOption.Values)
			{
				dataGridEnumValues.Rows.Add(val);
			}
		}

		private void PopulateIterators()
		{
			return;
		}

		private void PopulateVariableTypes()
		{
			comboBoxType.Items.Clear();
			userOptionTypes.Clear();

			var optionTypes = Project.Instance.GetUserOptionTypes();

			foreach (var userOptionType in optionTypes)
			{
				var ci = new ComboBoxItemEx<Type>(userOptionType, GetUserFriendlyName);
				comboBoxType.Items.Add(ci);
				userOptionTypes.Add(userOptionType);
			}
		}

		private string GetUserFriendlyName(Type arg)
		{
			if (arg == typeof(bool?))
				return "bool?";
			else if (arg == typeof(int?))
				return "int?";

			return arg.FullName;
		}

		private void PopulateCategories()
		{
			comboBoxCategory.Sorted = false;
			comboBoxCategory.Items.Clear();
			comboBoxCategory.Items.AddRange(Project.Instance.UserOptionCategories);
			comboBoxCategory.Sorted = true;
		}

		private void comboBoxType_SelectedValueChanged(object sender, EventArgs e)
		{
			if (BusyPopulating) return;
			bool visible = (comboBoxType.Text == "Enumeration");
			dataGridEnumValues.Visible = visible;
			labelEnumValues.Visible = visible;

			if (comboBoxType.SelectedIndex == -1)
				return;

			UserOption.VarType = userOptionTypes[comboBoxType.SelectedIndex];
		}

		private void dataGridEnumValues_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (BusyPopulating || UserOption == null)
				return;

			List<string> vals = new List<string>();

			foreach (DataGridViewRow row in dataGridEnumValues.Rows)
			{
				if (!row.IsNewRow)
					vals.Add(row.Cells[0].Value.ToString());
			}
			UserOption.Values.Clear();
			UserOption.Values.AddRange(vals);
		}

		private void textBoxVariableName_TextChanged(object sender, EventArgs e)
		{
			if (BusyPopulating) return;
			string oldText = UserOption.VariableName;
			UserOption.VariableName = textBoxVariableName.Text;
			UserOptionNameChanged.RaiseEventEx(this, new TextChangedEventArgs(oldText, UserOption.VariableName));
		}

		private void textBoxText_TextChanged(object sender, EventArgs e)
		{
			if (BusyPopulating) return;
			UserOption.Text = textBoxText.Text;
		}

		private void textBoxDescription_TextChanged(object sender, EventArgs e)
		{
			if (BusyPopulating) return;
			UserOption.Description = textBoxDescription.Text;
		}


		private void comboBoxCategory_TextChanged(object sender, EventArgs e)
		{
			if (BusyPopulating) return;

			string oldCategory = UserOption.Category;
			UserOption.Category = comboBoxCategory.Text;
			UserOptionCategoryChanged.RaiseEventEx(this, new TextChangedEventArgs(oldCategory, UserOption.Category));
		}

		public bool EventRaisingDisabled
		{
			get;
			set;
		}

		private void checkBoxResetPerSession_CheckedChanged(object sender, EventArgs e)
		{
			if (BusyPopulating) return;

			UserOption.ResetPerSession = checkBoxResetPerSession.Checked;
		}
	}

	public class TextChangedEventArgs : EventArgs
	{
		public readonly string OldText;
		public readonly string NewText;

		public TextChangedEventArgs(string oldText, string newText)
		{
			OldText = oldText;
			NewText = newText;
		}
	}
}
