namespace Slyce.Common.Updates
{
    partial class frmUpdate
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUpdate));
			this.panel1 = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lblFileSize = new System.Windows.Forms.Label();
			this.lnkPatchFile = new System.Windows.Forms.LinkLabel();
			this.btnViewDetails = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.webBrowser1 = new System.Windows.Forms.WebBrowser();
			this.prgDownload = new System.Windows.Forms.ProgressBar();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.progressBarDownload = new DevComponents.DotNetBar.Controls.ProgressBarX();
			this.ucHeading1 = new Slyce.Common.Controls.ucHeading();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(526, 57);
			this.panel1.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Location = new System.Drawing.Point(185, 23);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(289, 31);
			this.label2.TabIndex = 4;
			this.label2.Text = "We are going to check for updates for your product. Please make sure you are conn" +
				"ected to the internet.";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(166, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(142, 15);
			this.label1.TabIndex = 3;
			this.label1.Text = "Slyce Update Service";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(150, 57);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			// 
			// lblFileSize
			// 
			this.lblFileSize.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.lblFileSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblFileSize.Location = new System.Drawing.Point(16, 70);
			this.lblFileSize.Name = "lblFileSize";
			this.lblFileSize.Size = new System.Drawing.Size(142, 13);
			this.lblFileSize.TabIndex = 18;
			this.lblFileSize.Text = "(123kb)";
			this.lblFileSize.Visible = false;
			// 
			// lnkPatchFile
			// 
			this.lnkPatchFile.AutoSize = true;
			this.lnkPatchFile.Location = new System.Drawing.Point(312, 67);
			this.lnkPatchFile.Name = "lnkPatchFile";
			this.lnkPatchFile.Size = new System.Drawing.Size(55, 13);
			this.lnkPatchFile.TabIndex = 17;
			this.lnkPatchFile.TabStop = true;
			this.lnkPatchFile.Text = "linkLabel1";
			this.lnkPatchFile.Visible = false;
			this.lnkPatchFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkPatchFile_LinkClicked);
			this.lnkPatchFile.SizeChanged += new System.EventHandler(this.lnkPatchFile_SizeChanged);
			// 
			// btnViewDetails
			// 
			this.btnViewDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnViewDetails.AutoSize = true;
			this.btnViewDetails.Enabled = false;
			this.btnViewDetails.Location = new System.Drawing.Point(258, 418);
			this.btnViewDetails.Name = "btnViewDetails";
			this.btnViewDetails.Size = new System.Drawing.Size(165, 23);
			this.btnViewDetails.TabIndex = 16;
			this.btnViewDetails.Text = "Download";
			this.btnViewDetails.UseVisualStyleBackColor = true;
			this.btnViewDetails.Click += new System.EventHandler(this.btnViewDetails_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(439, 418);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 15;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// webBrowser1
			// 
			this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.webBrowser1.Location = new System.Drawing.Point(12, 92);
			this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
			this.webBrowser1.Name = "webBrowser1";
			this.webBrowser1.Size = new System.Drawing.Size(502, 303);
			this.webBrowser1.TabIndex = 14;
			this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
			// 
			// prgDownload
			// 
			this.prgDownload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.prgDownload.Location = new System.Drawing.Point(373, 63);
			this.prgDownload.Name = "prgDownload";
			this.prgDownload.Size = new System.Drawing.Size(141, 23);
			this.prgDownload.TabIndex = 13;
			this.prgDownload.Visible = false;
			// 
			// backgroundWorker1
			// 
			this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			// 
			// progressBarDownload
			// 
			this.progressBarDownload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			// 
			// 
			// 
			this.progressBarDownload.BackgroundStyle.Class = "";
			this.progressBarDownload.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.progressBarDownload.Location = new System.Drawing.Point(12, 418);
			this.progressBarDownload.Name = "progressBarDownload";
			this.progressBarDownload.Size = new System.Drawing.Size(240, 23);
			this.progressBarDownload.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
			this.progressBarDownload.TabIndex = 22;
			this.progressBarDownload.Text = "progressBarX1";
			this.progressBarDownload.TextVisible = true;
			this.progressBarDownload.Visible = false;
			// 
			// ucHeading1
			// 
			this.ucHeading1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ucHeading1.Location = new System.Drawing.Point(0, 411);
			this.ucHeading1.Name = "ucHeading1";
			this.ucHeading1.Size = new System.Drawing.Size(526, 36);
			this.ucHeading1.TabIndex = 21;
			this.ucHeading1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// frmUpdate
			// 
			this.AcceptButton = this.btnViewDetails;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(526, 447);
			this.Controls.Add(this.progressBarDownload);
			this.Controls.Add(this.lblFileSize);
			this.Controls.Add(this.lnkPatchFile);
			this.Controls.Add(this.btnViewDetails);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.webBrowser1);
			this.Controls.Add(this.prgDownload);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.ucHeading1);
			this.Name = "frmUpdate";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Check For Updates";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.frmUpdate_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmUpdate_Paint);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblFileSize;
        private System.Windows.Forms.LinkLabel lnkPatchFile;
        private System.Windows.Forms.Button btnViewDetails;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.ProgressBar prgDownload;
        private Slyce.Common.Controls.ucHeading ucHeading1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private DevComponents.DotNetBar.Controls.ProgressBarX progressBarDownload;
    }
}