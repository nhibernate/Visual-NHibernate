using System;
using System.Windows.Forms;

namespace Slyce.Common.Controls
{
	public partial class MessagePanel : UserControl
	{
		private string _Message = "Testing";
		private ImageType imageType;
		//private System.Drawing.StringAlignment _StatusTextAlignment = System.Drawing.StringAlignment.Center;

		public enum ImageType
		{
			Alarm = 0,
			Hourglass = 1,
			None = 2
		}

		public MessagePanel(string message)
		{
			InitializeComponent();

			SetStyle(
			ControlStyles.UserPaint |
			ControlStyles.AllPaintingInWmPaint |
			ControlStyles.OptimizedDoubleBuffer, true);

			Message = message;
			Image = ImageType.Hourglass;
			StatusText = "";
		}

		//public System.Drawing.StringAlignment StatusTextAlignment
		//{
		//    get { return _StatusTextAlignment; }
		//    set
		//    {
		//        if (_StatusTextAlignment == value)
		//        {
		//            return;
		//        }
		//        _StatusTextAlignment = value;
		//        labelDetail.TextAlignment = _StatusTextAlignment;
		//        PerformLayout();
		//    }
		//}

		public string Message
		{
			get { return _Message; }
			set
			{
				_Message = value;
				//reflectionLabel1.Text = string.Format("<b><font color=\"#ffffff\" size=\"+6\">{0}</font></b>", _Message);
				reflectionLabel1.Text = _Message;
				PerformLayout();
			}
		}

		public ImageType Image
		{
			get { return imageType; }
			set
			{
				imageType = value;

				if (imageType == ImageType.None)
					reflectionImage1.Visible = false;
				else
				{
					reflectionImage1.Visible = true;
					reflectionImage1.Image = imageList1.Images[(int)value];
				}
			}
		}

		public string StatusText
		{
			get { return labelDetail.Text; }
			set
			{
				//labelDetail.Text = value;
				//this.Refresh();
				SetLabelDetailText(value);
			}
		}

		private void SetLabelDetailText(string text)
		{
			if (labelDetail.InvokeRequired)
			{
				MethodInvoker mi = () => SetLabelDetailText(text);
				labelDetail.Invoke(mi);
				return;
			}
			labelDetail.Text = text;
			this.Refresh();
		}

		private void panelEx1_Resize(object sender, EventArgs e)
		{
			PerformLayout();
		}

		private new void PerformLayout()
		{
			System.Drawing.Graphics g = System.Drawing.Graphics.FromHwnd(reflectionLabel1.Handle);
			int requiredLabelWidth = (int)g.MeasureString(Message, reflectionLabel1.Font).Width + 5;
			int requiredPanelWidth = requiredLabelWidth + reflectionImage1.Width;

			if (panel1.Width < requiredPanelWidth)
			{
				if (requiredPanelWidth < this.ClientSize.Width)
				{
					panel1.Width = requiredPanelWidth;
					panel1.Left = (this.ClientSize.Width - panel1.Width) / 2;
				}
				else
				{
					panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left;
					panel1.Left = 0;
					panel1.Width = Math.Min(requiredPanelWidth, this.ClientSize.Width);
				}
			}
		}
	}
}
