using System;
using System.Windows.Forms;

namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
	public partial class FormInheritanceHierarchy : Form
	{
		public FormInheritanceHierarchy(ArchAngel.Providers.EntityModel.Model.DatabaseLayer.ITable table)
		{
			InitializeComponent();

			InheritanceHierarchy1.Table = table;
		}

		private void buttonOk_Click(object sender, EventArgs e)
		{
			if (!InheritanceHierarchy1.Save())
				return;

			DialogResult = System.Windows.Forms.DialogResult.OK;
			Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
