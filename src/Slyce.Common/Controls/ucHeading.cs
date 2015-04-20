using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Slyce.Common.Controls
{
	public partial class ucHeading : UserControl
	{
		private string m_textVal;
		private Font TextFont = new Font("Verdana", 12.0f, FontStyle.Bold);
		Brush TextBrush = new SolidBrush(Color.White);
		Graphics graphics;
		private HorizontalAlignment _TextAlign = HorizontalAlignment.Center;

		public HorizontalAlignment TextAlign
		{
			get { return _TextAlign; }
			set { _TextAlign = value; }
		}

		public ucHeading()
		{
			InitializeComponent();
			Text = this.Name;
			this.Resize += new EventHandler(ucHeading_Resize);
			this.Paint += new PaintEventHandler(ucHeading_Paint);
		}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
			// Do nothing, just to prevent flickering
		}

		[Browsable(true)]
		public override string Text
		{
			get
			{
				return m_textVal;
			}
			set
			{
				m_textVal = value;
				this.Refresh();
			}
		}

		private void ucHeading_Paint(object sender, PaintEventArgs e)
		{
			Repaint();
		}

		private void ucHeading_Resize(object sender, EventArgs e)
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

				switch (TextAlign)
				{
					case HorizontalAlignment.Center:
						graphics.DrawString(Text, TextFont, TextBrush, (this.Width / 2) - (System.Convert.ToInt32(graphics.MeasureString(Text, TextFont).Width) / 2), 0);
						break;
					case HorizontalAlignment.Left:
						graphics.DrawString(Text, TextFont, TextBrush, 0, 0);
						break;
					case HorizontalAlignment.Right:
						graphics.DrawString(Text, TextFont, TextBrush, this.Width - System.Convert.ToInt32(graphics.MeasureString(Text, TextFont).Width), 0);
						break;
				}

			}
		}
	}
}
