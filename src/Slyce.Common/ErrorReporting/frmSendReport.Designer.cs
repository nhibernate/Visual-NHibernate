namespace Slyce.Common.ErrorReporting
{
	partial class frmSendReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSendReport));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCompanyMessage = new System.Windows.Forms.Label();
            this.chkIgnore = new System.Windows.Forms.CheckBox();
            this.lblBoldPleed = new System.Windows.Forms.Label();
            this.lblGeneralDescription = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnDontSend = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnDebug = new System.Windows.Forms.Button();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblCompanyMessage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(529, 58);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(369, -7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(155, 61);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(232, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "We are sorry for the inconvenience.";
            // 
            // lblCompanyMessage
            // 
            this.lblCompanyMessage.AutoSize = true;
            this.lblCompanyMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompanyMessage.Location = new System.Drawing.Point(12, 9);
            this.lblCompanyMessage.Name = "lblCompanyMessage";
            this.lblCompanyMessage.Size = new System.Drawing.Size(218, 15);
            this.lblCompanyMessage.TabIndex = 1;
            this.lblCompanyMessage.Text = "XXX has encountered a problem.";
            // 
            // chkIgnore
            // 
            this.chkIgnore.AutoSize = true;
            this.chkIgnore.Location = new System.Drawing.Point(15, 129);
            this.chkIgnore.Name = "chkIgnore";
            this.chkIgnore.Size = new System.Drawing.Size(217, 17);
            this.chkIgnore.TabIndex = 2;
            this.chkIgnore.Text = "Ignore this error and attempt to &continue.";
            this.chkIgnore.UseVisualStyleBackColor = true;
            // 
            // lblBoldPleed
            // 
            this.lblBoldPleed.AutoSize = true;
            this.lblBoldPleed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBoldPleed.Location = new System.Drawing.Point(12, 158);
            this.lblBoldPleed.Name = "lblBoldPleed";
            this.lblBoldPleed.Size = new System.Drawing.Size(213, 13);
            this.lblBoldPleed.TabIndex = 3;
            this.lblBoldPleed.Text = "Please tell Slyce about this problem.";
            // 
            // lblGeneralDescription
            // 
            this.lblGeneralDescription.Location = new System.Drawing.Point(12, 171);
            this.lblGeneralDescription.Name = "lblGeneralDescription";
            this.lblGeneralDescription.Size = new System.Drawing.Size(366, 50);
            this.lblGeneralDescription.TabIndex = 4;
            this.lblGeneralDescription.Text = "To help improve the software you use, Slyce Software is interested in learning mo" +
                "re about this error. We have created a report about this error for you to send u" +
                "s. Email it to support@slyce.com.";
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSend.Location = new System.Drawing.Point(307, 208);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(134, 34);
            this.btnSend.TabIndex = 5;
            this.btnSend.Text = "&Copy Error To Clipboard";
            this.toolTip1.SetToolTip(this.btnSend, "You need an internet connection to send an error report");
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnDontSend
            // 
            this.btnDontSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDontSend.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDontSend.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDontSend.Location = new System.Drawing.Point(429, 208);
            this.btnDontSend.Name = "btnDontSend";
            this.btnDontSend.Size = new System.Drawing.Size(88, 34);
            this.btnDontSend.TabIndex = 6;
            this.btnDontSend.Text = "&Don\'t Send";
            this.btnDontSend.UseVisualStyleBackColor = true;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // btnDebug
            // 
            this.btnDebug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDebug.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDebug.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDebug.Location = new System.Drawing.Point(273, 208);
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(60, 34);
            this.btnDebug.TabIndex = 7;
            this.btnDebug.Text = "Debug";
            this.btnDebug.UseVisualStyleBackColor = true;
            this.btnDebug.Visible = false;
            this.btnDebug.Click += new System.EventHandler(this.btnDebug_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.AcceptsReturn = true;
            this.txtDescription.BackColor = System.Drawing.SystemColors.Control;
            this.txtDescription.Location = new System.Drawing.Point(15, 61);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(473, 62);
            this.txtDescription.TabIndex = 8;
            // 
            // frmSendReport
            // 
            this.AcceptButton = this.btnSend;
            this.CancelButton = this.btnDontSend;
            this.ClientSize = new System.Drawing.Size(529, 245);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.btnDebug);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnDontSend);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chkIgnore);
            this.Controls.Add(this.lblBoldPleed);
            this.Controls.Add(this.lblGeneralDescription);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSendReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Send Error Report";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmSendReport_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSendReport_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label lblCompanyMessage;
        private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox chkIgnore;
		private System.Windows.Forms.Label lblBoldPleed;
		private System.Windows.Forms.Label lblGeneralDescription;
		private System.Windows.Forms.Button btnSend;
		private System.Windows.Forms.Button btnDontSend;
		private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnDebug;
        private System.Windows.Forms.TextBox txtDescription;
	}
}