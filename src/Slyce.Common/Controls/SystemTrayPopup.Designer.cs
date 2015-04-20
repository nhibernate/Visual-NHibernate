namespace Slyce.Common.Controls
{
        partial class SystemTrayPopup
        {
            /// <summary>
            /// Required designer variable.
            /// </summary>
            private System.ComponentModel.IContainer components = null;

            /// <summary>
            /// Clean up any resources being used.
            /// </summary>
            /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
            protected override void Dispose(bool disposing)
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }

            #region Windows Form Designer generated code

            /// <summary>
            /// Required method for Designer support - do not modify
            /// the contents of this method with the code editor.
            /// </summary>
            private void InitializeComponent()
            {
                this.components = new System.ComponentModel.Container();
                this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
                this.backgroundWorkerShow = new System.ComponentModel.BackgroundWorker();
                this.SuspendLayout();
                // 
                // backgroundWorker1
                // 
                this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
                this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
                // 
                // backgroundWorkerShow
                // 
                this.backgroundWorkerShow.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerShow_DoWork);
                // 
                // SystemTrayPopup
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.BackColor = System.Drawing.SystemColors.Control;
                this.ClientSize = new System.Drawing.Size(219, 211);
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.Location = new System.Drawing.Point(10000, 10000);
                this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
                this.Name = "SystemTrayPopup";
                this.ShowInTaskbar = false;
                this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
                this.Text = "SystemTrayPopup";
                this.TopMost = true;
                this.Paint += new System.Windows.Forms.PaintEventHandler(this.SystemTrayPopup_Paint);
                this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SystemTrayPopup_MouseClick);
                this.MouseEnter += new System.EventHandler(this.SystemTrayPopup_MouseEnter);
                this.Activated += new System.EventHandler(this.SystemTrayPopup_Activated);
                this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SystemTrayPopup_FormClosing);
                this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SystemTrayPopup_MouseMove);
                this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SystemTrayPopup_MouseDown);
                this.ResumeLayout(false);

            }

            #endregion

            private System.ComponentModel.BackgroundWorker backgroundWorkerShow;
            private System.ComponentModel.BackgroundWorker backgroundWorker1;
        }
    }
