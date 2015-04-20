using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace Slyce.Licensing
{
	public partial class frmMain : Form
	{
        private int Gap = 10;

		public frmMain()
		{
			InitializeComponent();
            //this.BackColor = Slyce.Common.Colors.BackgroundColor;
            //if (Controller.InDesignMode) { return; }

            EnableDoubleBuffering();
            ResizeForm();
		}

        private void EnableDoubleBuffering()
        {
            // Set the value of the double-buffering style bits to true.
            this.SetStyle(ControlStyles.DoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint,
                true);
            this.UpdateStyles();
        }

        private void ResizeForm()
        {
            this.Height = (this.Height - this.ClientSize.Height) + licenseStatus1.Top + licenseStatus1.Height;
            licenseStatus1.Left = Gap;
            this.Width = licenseStatus1.Width + Gap * 2;
        }

        private void licenseStatus1_Resize(object sender, EventArgs e)
        {
            this.Height = (this.Height - this.ClientSize.Height) + licenseStatus1.Top + licenseStatus1.Height;
            this.Width = licenseStatus1.Width + Gap * 2;
        }

	}
}