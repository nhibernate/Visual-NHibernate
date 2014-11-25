using System;
using System.Drawing;
using System.Windows.Forms;

namespace ArchAngel.Workbench.UserControls
{
    public partial class ErrorProvider : UserControl
    {
        private string Title = "";
        private string FooterText = "";

        public ErrorProvider()
        {
            InitializeComponent();
            //((Bitmap)pictureBox1.Image).MakeTransparent(Color.Magenta);
            this.Size = pictureBox1.Size;
        }

        //private VisualTipProvider vtp
        //{
        //    get
        //    {
        //        if (_vtp == null)
        //        {
        //            _vtp = new VisualTipProvider();
        //            //vtp.AccessKey = Shortcut.F1;
        //            _vtp.Shadow = VisualTipShadow.Enabled;
        //            _vtp.Animation = VisualTipAnimation.Enabled;
        //            _vtp.Opacity = 1.0;
        //            _vtp.Renderer = this.Renderer;
        //        }
        //        return _vtp;
        //    }
        //}

        public void SetError(Control control, string text, string title, string footerText)
        {
            this.Text = text;
            this.Title = title;
            this.FooterText = footerText;
            this.Location = new Point(control.Left - this.Width - 3, control.Top);
            this.Visible = true;
        }

        public void Clear()
        {
            //this.Visible = false;

            //if (vtp != null)
            //{
            //    vtp.HideTip();
            //}
        }

        //private Skybound.VisualTips.Rendering.VisualTipOfficeRenderer Renderer
        //{
        //    get
        //    {
        //        if (_Renderer == null)
        //        {
        //            _Renderer = new Skybound.VisualTips.Rendering.VisualTipOfficeRenderer();
        //            _Renderer.BackgroundEffect = Skybound.VisualTips.Rendering.VisualTipOfficeBackgroundEffect.Gradient;
        //            _Renderer.Preset = Skybound.VisualTips.Rendering.VisualTipOfficePreset.Midnight;
        //            _Renderer.RoundCorners = true;
        //        }
        //        return _Renderer;
        //    }
        //}

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            //vtp.FooterText = this.FooterText;

            //visualTip = new VisualTip();
            //visualTip.Text = this.Text;
            //visualTip.Title = this.Title;
            ////visualTip.DisabledMessage = "Nothing is selected.";
            //visualTip.Image = pictureBox1.Image;
            //vtp.SetVisualTip((Control)sender, visualTip);
            ////visualTip.Shortcut = Shortcut.CtrlC;
            
            //Rectangle rect = new Rectangle(this.PointToScreen(new Point(15, 5)), this.Size);
            //vtp.ShowTip((Control)sender, VisualTipDisplayOptions.HideOnKeyPress | VisualTipDisplayOptions.HideOnLostFocus | VisualTipDisplayOptions.HideOnMouseLeave, rect);
        }

    }
}
