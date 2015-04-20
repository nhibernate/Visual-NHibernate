using System;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;

namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
	public partial class FormInheritanceInfo : Form
	{
		private Entity ParentEntity;
		private Entity ChildEntity;

		public FormInheritanceInfo(Entity parentEntity, Entity childEntity)
		{
			InitializeComponent();

			ParentEntity = parentEntity;
			ChildEntity = childEntity;

			Populate();
		}

		private void Populate()
		{
			labelTypeOfInheritance.Text = EntityImpl.GetDisplayText(EntityImpl.DetermineInheritanceTypeWithChildren(ParentEntity));
			labelEntity1.Text = ParentEntity.Name + " (Parent)";
			labelEntity2.Text = ChildEntity.Name + " (Child)";


		}

		private void buttonOk_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
