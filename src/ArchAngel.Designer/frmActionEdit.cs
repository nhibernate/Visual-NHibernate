using System;
using System.Windows.Forms;

namespace ArchAngel.Designer
{
	public partial class frmActionEdit : Form
	{
		private ArchAngel.Interfaces.BaseAction CurrentAction;

		public frmActionEdit(ArchAngel.Interfaces.BaseAction action)
		{
			InitializeComponent();

			Controller.ShadeMainForm();
			ucHeading1.Text = "";
			this.Text = "Edit Action: " + action.DisplayName;
			CurrentAction = action;
			Populate();
		}

		private void Populate()
		{
			Type currentActionType = CurrentAction.GetType();
			object[] customAttributes = currentActionType.GetCustomAttributes(typeof(ArchAngel.Interfaces.ActionAttribute), false);
			ArchAngel.Interfaces.ActionAttribute att = (ArchAngel.Interfaces.ActionAttribute)customAttributes[0];
			System.Reflection.Assembly actionAssembly = System.Reflection.Assembly.GetAssembly(currentActionType);

			Control control = (Control)actionAssembly.CreateInstance(att.EditControlName);
			panel1.Controls.Add(control);

		}

		private void frmActionEdit_Paint(object sender, PaintEventArgs e)
		{
			this.BackColor = Slyce.Common.Colors.BackgroundColor;
		}

		private void frmActionEdit_FormClosed(object sender, FormClosedEventArgs e)
		{
			Controller.UnshadeMainForm();
		}
	}
}