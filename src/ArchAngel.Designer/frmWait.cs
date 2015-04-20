using System.Windows.Forms;

namespace ArchAngel.Designer
{
	public partial class frmWait : Form
	{
		public frmWait(Form parentForm)
		{
			InitializeComponent();

			//this.Left = parentForm.Left + (parentForm.Right - parentForm.Left) - this.Width / 2;
			//this.Top = parentForm.Top + (parentForm.Bottom - parentForm.Top) - this.Height / 2;
		}

	}
}