using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Slyce.Licensing
{
    public partial class LicenseStatus : UserControl
    {
        private int DaysPassed = 0;
        private int TotalDays = 30;
        private int Gap = 15;
        private int HeightDiff = 10;

        public LicenseStatus()
        {
            HeightDiff = this.Height - this.ClientSize.Height;
            InitializeComponent();
            //if (Controller.InDesignMode) { return; }

            //SetGlassButtonColors(btnBuyNow);
            //SetGlassButtonColors(btnTrial);
            //SetGlassButtonColors(btnUnlock);
            
            EnableDoubleBuffering();
            Populate();
        }

        //private void SetGlassButtonColors(Slyce.Common.Controls.GelButton button)
        //{
        //    button.GradientTop = Slyce.Common.Colors.FadingTitleDarkColor;
        //    button.GradientBottom = Slyce.Common.Colors.FadingTitleLightColor;
        //    button.ForeColor = Slyce.Common.Colors.IdealTextColor(button.GradientTop);
        //}

        private void EnableDoubleBuffering()
        {
            // Set the value of the double-buffering style bits to true.
            this.SetStyle(ControlStyles.DoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint,
                true);
            this.UpdateStyles();
        }

        private void Populate()
        {
            try
            {
                DaysPassed = License.Status.Evaluation_Time_Current;
                TotalDays = License.Status.Evaluation_Time;

                if (TotalDays == 0)
                {
                    TotalDays = 30;
                }
                if (DaysPassed > 0)
                {
                    DaysPassed--;
                }
                //MessageBox.Show(string.Format("TotalDays: {0}, DaysPassed: {1}.", TotalDays, DaysPassed));
                //int difference = TotalDays - 30;
                //DaysPassed = DaysPassed - difference;
                //DaysPassed = 22;
                TotalDays = 30;
            }
            catch (Exception ex)
            {
                DaysPassed = 0;
                TotalDays = 30;
            }
            this.Height = HeightDiff + panel1.Top + panel1.Height + Gap;
            int daysLeft = TotalDays - DaysPassed;

            if (daysLeft >= 0)
            {
                lblRemainingDays.Text = string.Format("{0} days left", daysLeft);
                lblTotalDaysMessage.Text = string.Format("of your {0} day trial", TotalDays);
            }
            else
            {
                int daysOver = -1 * daysLeft;
                lblRemainingDays.Text = "Trial Expired";
                //btnTrial.Enabled = false;
                this.Controls.Remove(btnTrial);
                //btnTrial.Visible = false;
                //Font italicFont = new Font(btnTrial.Font, FontStyle.Italic);
                //btnTrial.Font = italicFont;
                if (daysOver == 1)
                {
                    lblTotalDaysMessage.Text = string.Format("{0} day trial expired 1 day ago", TotalDays);
                }
                else
                {
                    lblTotalDaysMessage.Text = string.Format("{0} day trial expired {1} days ago", TotalDays, daysOver);
                }
            }
            
        }

        private void pnlRemainingDays_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Red);

            if (TotalDays == 0)
            {
                TotalDays = 30;
            }
            int daysWidth = (int)((double)pnlRemainingDays.Width * (double)DaysPassed / (double)TotalDays);

            if (daysWidth == 0)
            {
                daysWidth = 1;
            }
            Rectangle redRectTop = new Rectangle(0, 0, daysWidth, pnlRemainingDays.Height / 2 + 1);
            Rectangle greenRectTop = new Rectangle(daysWidth, 0, pnlRemainingDays.Width - daysWidth, pnlRemainingDays.Height / 2 + 1);
            Rectangle redRectBottom = new Rectangle(0, pnlRemainingDays.Height / 2, daysWidth, pnlRemainingDays.Height / 2);
            Rectangle greenRectBottom = new Rectangle(daysWidth, pnlRemainingDays.Height / 2, pnlRemainingDays.Width - daysWidth, pnlRemainingDays.Height / 2);
            LinearGradientBrush redBrushTop = redRectTop.Width > 0 ? new LinearGradientBrush(redRectTop, Color.Wheat, Color.Red, LinearGradientMode.Vertical) : null;
            LinearGradientBrush greenBrushTop = greenRectTop.Width > 0 ? new LinearGradientBrush(greenRectTop, Color.GreenYellow, Color.Green, LinearGradientMode.Vertical) : null;
            LinearGradientBrush redBrushBottom = redRectBottom.Width > 0 ? new LinearGradientBrush(redRectBottom, Color.Red, Color.Wheat, LinearGradientMode.Vertical) : null;
            LinearGradientBrush greenBrushBottom = greenRectBottom.Width > 0 ? new LinearGradientBrush(greenRectBottom, Color.Green, Color.GreenYellow, LinearGradientMode.Vertical) : null;

            if (DaysPassed == 0)
            {
                redBrushBottom = greenBrushBottom;
                redBrushTop = greenBrushTop;
            }
            if (redRectBottom.Width > 0 && redBrushBottom != null)
            {
                g.FillRectangle(redBrushBottom, redRectBottom);
            }
            if (redRectTop.Width > 0 && redBrushTop != null)
            {
                g.FillRectangle(redBrushTop, redRectTop);
            }
            if (greenRectBottom.Width > 0 && greenBrushBottom != null)
            {
                g.FillRectangle(greenBrushBottom, greenRectBottom);
            }
            if (greenRectTop.Width > 0 && greenBrushTop != null)
            {
                g.FillRectangle(greenBrushTop, greenRectTop);
            }
            g.Flush();

            if (redBrushBottom != null){redBrushBottom.Dispose();}
            if (redBrushTop != null) {redBrushTop.Dispose();}
            if (greenBrushBottom != null) {greenBrushBottom.Dispose();}
            if (greenBrushTop != null) { greenBrushTop.Dispose(); }
        }

        private void lnkEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string toEmail = "sales@slyce.com";
            string subject = "Purchase ArchAngel";
            string body = "";// "I would like to purchase ArchAngel. \r\nPlease can you phone me at your earliest as soon as possible. \n\nThe best time to phone me is: I live in the United States. Yours sincerely, XXX";
            string message = string.Format("mailto:{0}?subject={1}&body={2}", toEmail, subject, body);
            System.Diagnostics.Process.Start(message);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.slyce.com/purchase");
        }

        private void btnBuyNow_Click(object sender, EventArgs e)
        {
            this.Height = HeightDiff + lnkEmail.Top + lnkEmail.Height + Gap;
        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            frmUnlock form = new frmUnlock();
            this.Hide();
            form.ShowDialog();
            Cursor = Cursors.Default;

            if (this.ParentForm != null)
            {
                this.ParentForm.Close();
            }
        }

        private void btnTrial_Click(object sender, EventArgs e)
        {
            Slyce.Licensing.Licenser.ContinueWithTrial = true;
            this.ParentForm.Close();
        }

        private void LicenseStatus_Resize(object sender, EventArgs e)
        {
            int buttonLeftValue = panel1.Left + panel1.Width + Gap;
            btnBuyNow.Left = buttonLeftValue;
            btnTrial.Left = buttonLeftValue;
            btnUnlock.Left = buttonLeftValue;

            this.Width = buttonLeftValue + btnBuyNow.Width + Gap;

            btnBuyNow.Visible = true;
            btnTrial.Visible = true;
            btnUnlock.Visible = true;

            btnBuyNow.BringToFront();
            btnTrial.BringToFront();
            btnUnlock.BringToFront();
        }

        private void btnTrial1_Click(object sender, EventArgs e)
        {
            Slyce.Licensing.Licenser.ContinueWithTrial = true;
            this.ParentForm.Close();

        }

        private void btnUnlock1_Click(object sender, EventArgs e)
        {
            this.ParentForm.Hide();
            frmUnlock form = new frmUnlock();
            //this.Hide();
            form.ShowDialog();
            this.ParentForm.Close();

        }

        private void btnBuyNow1_Click(object sender, EventArgs e)
        {
            this.Height = HeightDiff + lnkEmail.Top + lnkEmail.Height + Gap;
        }

        private void btnBuyNow_Click_1(object sender, EventArgs e)
        {

        }

        private void btnUnlock_Click_1(object sender, EventArgs e)
        {

        }


    }
}
