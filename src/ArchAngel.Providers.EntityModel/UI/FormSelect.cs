using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Slyce.Common;

namespace ArchAngel.Providers.EntityModel.UI
{
	public partial class FormSelect<T> : Form
	{
		public FormSelect(string objectName, IEnumerable<T> objects, Func<T, string> nameFunction)
		{
			InitializeComponent();

			foreach (var spec in objects)
			{
				comboBoxEntities.Items.Add(new ComboBoxItemEx<T>(spec, nameFunction));
			}

			Text = "Select " + objectName;

			if(objects.Any())
				comboBoxEntities.SelectedIndex = 0;
		}

		public T SelectedColumn
		{
			get
			{
				return comboBoxEntities.GetSelectedItem<T>();
			}
		}

		private void buttonAccept_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
