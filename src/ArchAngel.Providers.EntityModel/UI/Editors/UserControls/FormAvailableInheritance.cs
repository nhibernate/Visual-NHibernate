using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;

namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
	public partial class FormAvailableInheritance : UserControl
	{
		internal event EventHandler AddAbstractParent;
		internal static Image ImageTick = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.tick_16.png"));
		internal static Image ImageStop = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.stop_16.png"));

		private Entity _Entity;
		private bool CanHaveTablePerSubClass = false;
		private bool CanHaveTableHierarchy = false;
		private string TextForHierarchy = "Create hierarchy...";
		private string TextForConcreteClass = "Create abstract parent...";

		public FormAvailableInheritance()
		{
			InitializeComponent();

			buttonTablePerConcreteClass.Image = ImageTick;
			buttonTablePerHierarchy.Image = ImageTick;
			buttonTablePerSubClass.Image = ImageTick;

			linkCreateFromHierarchy.Visible = false;
		}

		public Entity Entity
		{

			get { return _Entity; }
			set
			{
				_Entity = value;
				Populate();
			}
		}

		private void Populate()
		{
			#region Check for Table Per Sub-Class
			bool hasOneToOne = false;

			foreach (var directedRef in Entity.DirectedReferences)
			{
				if (ArchAngel.Interfaces.Cardinality.IsOneToOne(directedRef.FromEndCardinality, directedRef.ToEndCardinality))
				{
					hasOneToOne = true;
					break;
				}
			}
			if (hasOneToOne)
			{
				CanHaveTablePerSubClass = true;
				buttonTablePerSubClass.Image = ImageTick;
			}
			else
			{
				CanHaveTablePerSubClass = false;
				buttonTablePerSubClass.Image = ImageStop;
			}
			#endregion

			CanHaveTableHierarchy = Entity.MappedTables().Count() == 1;
		}

		private void buttonTablePerSubClass_MouseEnter(object sender, EventArgs e)
		{
			if (CanHaveTablePerSubClass)
				labelDescription.Text = "Available because 1:1 relationships exist. Click 'Add Parent' or 'Add Child' to select entities.";
			else
				labelDescription.Text = "Unavailable because no 1:1 relationships exist.";

			labelDescription.Refresh();
			linkCreateFromHierarchy.Visible = false;
		}

		private void buttonTablePerSubClass_MouseLeave(object sender, EventArgs e)
		{
			//labelDescription.Text = "";
		}

		private void buttonTablePerConcreteClass_MouseEnter(object sender, EventArgs e)
		{
			labelDescription.Text = "Available. Requires abstract parent.";
			linkCreateFromHierarchy.Text = TextForConcreteClass;
			linkCreateFromHierarchy.Visible = true;
		}

		private void buttonTablePerConcreteClass_MouseLeave(object sender, EventArgs e)
		{
			//labelDescription.Text = "";
		}

		private void buttonTablePerHierarchy_MouseEnter(object sender, EventArgs e)
		{
			if (CanHaveTableHierarchy)
			{
				labelDescription.Text = "Available. Multiple entities from single table using discriminator.";
				linkCreateFromHierarchy.Visible = true;
			}
			else
			{
				labelDescription.Text = "Unavailable becuase entity must be mapped to exactly one table.";
				linkCreateFromHierarchy.Visible = false;
			}
			linkCreateFromHierarchy.Text = TextForHierarchy;
		}

		private void buttonTablePerHierarchy_MouseLeave(object sender, EventArgs e)
		{
			//labelDescription.Text = "";
			//linkCreateFromHierarchy.Visible = false;
		}

		private void buttonTablePerHierarchy_Click(object sender, EventArgs e)
		{

		}

		private void linkCreateFromHierarchy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (linkCreateFromHierarchy.Text == TextForHierarchy)
			{
				UserControls.FormInheritanceHierarchy form = new UserControls.FormInheritanceHierarchy(Entity);
				form.ShowDialog(this);
			}
			else if (linkCreateFromHierarchy.Text == TextForConcreteClass)
			{
				if (AddAbstractParent != null)
					AddAbstractParent(null, null);
			}
		}

		private void buttonTablePerConcreteClass_Click(object sender, EventArgs e)
		{

		}
	}
}
