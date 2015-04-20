using System.Drawing;
using System.Windows.Forms;

namespace Slyce.Common.Controls
{
	public class TransparentPanelEx : Panel
	{
		public int Opacity { get; set; }

		public void ControlsToOverlay(Control[] controls)
		{
			foreach (Control control in controls)
			{
				control.Paint += new PaintEventHandler(control_Paint);
			}
		}

		void control_Paint(object sender, PaintEventArgs e)
		{
			this.Invalidate();
		}

		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.ExStyle |= 0x20; // Turn on WS_EX_TRANSPARENT
				return cp;
			}
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			pe.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(Opacity, this.BackColor)), this.ClientRectangle);
		}
	}
}
