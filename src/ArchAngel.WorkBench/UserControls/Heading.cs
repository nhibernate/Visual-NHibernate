using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ArchAngel.Workbench.UserControls
{
	public partial class Heading : UserControl
	{
		public string TextVal;
		private Font TextFont = new Font("Verdana", 12.0f, FontStyle.Bold);
		Brush TextBrush = new SolidBrush(Color.White);
		Graphics graphics;

		public Heading()
		{
			InitializeComponent();
            //if (Slyce.Common.Utility.InDesignMode) { return; }
			TextVal = this.Name;
		}

		private void ucLabel_Paint(object sender, PaintEventArgs e)
		{
			Repaint();
		}

		private void ucLabel_Resize(object sender, EventArgs e)
		{
			Repaint();
		}

		private void Repaint()
		{
            if (this.ClientRectangle.Width > 0 && this.ClientRectangle.Height > 0)
            {
                graphics = this.CreateGraphics();
                LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, Slyce.Common.Colors.FadingTitleLightColor, Slyce.Common.Colors.FadingTitleDarkColor, LinearGradientMode.Vertical);
                graphics.FillRectangle(brush, this.ClientRectangle);
                graphics.DrawString(Text, TextFont, TextBrush, (this.Width / 2) - (System.Convert.ToInt32(graphics.MeasureString(Text, TextFont).Width) / 2), 0);
            }

		}
	}
}
