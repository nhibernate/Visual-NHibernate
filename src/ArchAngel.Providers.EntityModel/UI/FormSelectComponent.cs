using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;

namespace ArchAngel.Providers.EntityModel.UI
{
	public partial class FormSelectComponent : Form
	{
		public FormSelectComponent(IEnumerable<ComponentSpecification> specs)
		{
			InitializeComponent();

			foreach(var spec in specs)
			{
				comboBoxComponents.Items.Add(spec);
			}
			comboBoxComponents.SelectedItem = specs.FirstOrDefault();
		}

		public ComponentSpecification SelectedComponentSpec
		{
			get
			{
				return comboBoxComponents.SelectedItem as ComponentSpecification;
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
