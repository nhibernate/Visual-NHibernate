using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using Slyce.Common;

namespace ArchAngel.Providers.EntityModel.UI
{
	public partial class FormSelectEntity : Form
	{
		public FormSelectEntity(IEnumerable<Entity> entities)
		{
			InitializeComponent();

			foreach (var spec in entities)
			{
				comboBoxEntities.Items.Add(new ComboBoxItemEx<Entity>(spec, e => e.Name));
			}

			if(entities.Any())
				comboBoxEntities.SelectedIndex = 0;
		}

		public Entity SelectedEntity
		{
			get
			{
				return comboBoxEntities.GetSelectedItem<Entity>();
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
