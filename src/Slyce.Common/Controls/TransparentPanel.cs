using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Slyce.Common.Controls
{
    public partial class TransparentPanel : UserControl
    {
        Bitmap cachedBitmap;

        public TransparentPanel()
        {
            InitializeComponent();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Do nothing
            //e.Graphics.DrawString("Hello world", this.Font, Brushes.Red, 100, 100);
        }

        private Bitmap CachedBitmap
        {
            get
            {
                if (cachedBitmap == null)
                {
                    cachedBitmap = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
                    Graphics g = Graphics.FromImage(cachedBitmap);
                    g.FillRectangle(new SolidBrush(Color.FromArgb(128, Color.Black)), this.ClientRectangle);
                }
                return cachedBitmap;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);

            e.Graphics.DrawImage(CachedBitmap, 0, 0);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20;  // Turn on WS_EX_TRANSPARENT
                return cp; 
            }
        }
    }
}
