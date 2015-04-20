using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using Slyce.Common;

namespace ArchAngel.Providers.EntityModel.UI
{
	public partial class FormSelectColumn : Form
	{
		public FormSelectColumn(IEnumerable<IColumn> columns)
		{
			InitializeComponent();

			foreach (var spec in columns)
			{
				comboBoxEntities.Items.Add(new ComboBoxItemEx<IColumn>(spec, e => e.Name));
			}

			if(columns.Any())
				comboBoxEntities.SelectedIndex = 0;
		}

		public IColumn SelectedColumn
		{
			get
			{
				return comboBoxEntities.GetSelectedItem<IColumn>();
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
