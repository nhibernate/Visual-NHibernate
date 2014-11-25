namespace Slyce.Licensing
{
	partial class frmUnlock
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUnlock));
            this.lblRequestStatus = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSerialNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEmailBody = new System.Windows.Forms.TextBox();
            this.picSerialHelp = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.lnkManualMethod = new System.Windows.Forms.LinkLabel();
            this.txtHardwareId = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlEmail = new System.Windows.Forms.Panel();
            this.button1 = new Slyce.Common.Controls.GlassButton();
            this.label9 = new System.Windows.Forms.Label();
            this.lblWaitMessage = new System.Windows.Forms.Label();
            this.button2 = new Slyce.Common.Controls.GlassButton();
            this.btnGetLicence = new Slyce.Common.Controls.GlassButton();
            ((System.ComponentModel.ISupportInitialize)(this.picSerialHelp)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnlEmail.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblRequestStatus
            // 
            this.lblRequestStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblRequestStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRequestStatus.ForeColor = System.Drawing.Color.Red;
            this.lblRequestStatus.Location = new System.Drawing.Point(136, 48);
            this.lblRequestStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRequestStatus.Name = "lblRequestStatus";
            this.lblRequestStatus.Size = new System.Drawing.Size(170, 24);
            this.lblRequestStatus.TabIndex = 1;
            this.lblRequestStatus.Text = "* requires internet connection";
            this.lblRequestStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(29, 7);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Enter your serial number:";
            // 
            // txtSerialNumber
            // 
            this.txtSerialNumber.Location = new System.Drawing.Point(32, 24);
            this.txtSerialNumber.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtSerialNumber.Name = "txtSerialNumber";
            this.txtSerialNumber.Size = new System.Drawing.Size(227, 20);
            this.txtSerialNumber.TabIndex = 6;
            this.txtSerialNumber.Validated += new System.EventHandler(this.txtSerialNumber_Validated);
            this.txtSerialNumber.TextChanged += new System.EventHandler(this.txtSerialNumber_TextChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(21, 70);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(248, 16);
            this.label3.TabIndex = 14;
            this.label3.Text = "Copy the following text into an email and send to:";
            // 
            // txtEmailBody
            // 
            this.txtEmailBody.Location = new System.Drawing.Point(28, 389);
            this.txtEmailBody.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtEmailBody.Multiline = true;
            this.txtEmailBody.Name = "txtEmailBody";
            this.txtEmailBody.Size = new System.Drawing.Size(249, 42);
            this.txtEmailBody.TabIndex = 15;
            // 
            // picSerialHelp
            // 
            this.picSerialHelp.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.picSerialHelp.Image = ((System.Drawing.Image)(resources.GetObject("picSerialHelp.Image")));
            this.picSerialHelp.Location = new System.Drawing.Point(262, 24);
            this.picSerialHelp.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.picSerialHelp.Name = "picSerialHelp";
            this.picSerialHelp.Size = new System.Drawing.Size(15, 16);
            this.picSerialHelp.TabIndex = 16;
            this.picSerialHelp.TabStop = false;
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 20000;
            this.toolTip1.InitialDelay = 200;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 100;
            this.toolTip1.ShowAlways = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Where is my serial number?";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label7.ForeColor = System.Drawing.Color.Navy;
            this.label7.Location = new System.Drawing.Point(16, 203);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(284, 31);
            this.label7.TabIndex = 18;
            this.label7.Text = "Click the link below to visit the unlock page. You will be able to download the l" +
                "icense file into the ArchAngel folder.";
            // 
            // lnkManualMethod
            // 
            this.lnkManualMethod.AutoSize = true;
            this.lnkManualMethod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lnkManualMethod.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lnkManualMethod.DisabledLinkColor = System.Drawing.Color.Yellow;
            this.lnkManualMethod.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkManualMethod.LinkColor = System.Drawing.Color.White;
            this.lnkManualMethod.Location = new System.Drawing.Point(80, 234);
            this.lnkManualMethod.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lnkManualMethod.Name = "lnkManualMethod";
            this.lnkManualMethod.Size = new System.Drawing.Size(138, 13);
            this.lnkManualMethod.TabIndex = 13;
            this.lnkManualMethod.TabStop = true;
            this.lnkManualMethod.Text = "www.slyce.com/unlock";
            this.lnkManualMethod.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkManualMethod_LinkClicked);
            // 
            // txtHardwareId
            // 
            this.txtHardwareId.Location = new System.Drawing.Point(92, 133);
            this.txtHardwareId.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtHardwareId.Name = "txtHardwareId";
            this.txtHardwareId.Size = new System.Drawing.Size(164, 20);
            this.txtHardwareId.TabIndex = 26;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(116, 54);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 17);
            this.label4.TabIndex = 30;
            this.label4.Text = "- or -";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(9, 131);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 23);
            this.label5.TabIndex = 30;
            this.label5.Text = "Hardware ID:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(89, 96);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 19);
            this.label1.TabIndex = 31;
            this.label1.Text = "Alternative activation methods:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Navy;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(213, 19);
            this.label6.TabIndex = 32;
            this.label6.Text = "Unlock via website - quick";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Navy;
            this.label8.Location = new System.Drawing.Point(8, 284);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(213, 19);
            this.label8.TabIndex = 33;
            this.label8.Text = "Email request - slower";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.label6);
            this.panel1.Location = new System.Drawing.Point(8, 180);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(292, 83);
            this.panel1.TabIndex = 34;
            // 
            // pnlEmail
            // 
            this.pnlEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.pnlEmail.Controls.Add(this.button1);
            this.pnlEmail.Controls.Add(this.label9);
            this.pnlEmail.Controls.Add(this.label3);
            this.pnlEmail.Controls.Add(this.label4);
            this.pnlEmail.Location = new System.Drawing.Point(8, 284);
            this.pnlEmail.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlEmail.Name = "pnlEmail";
            this.pnlEmail.Size = new System.Drawing.Size(292, 158);
            this.pnlEmail.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(38, 21);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.OuterBorderColor = System.Drawing.Color.Transparent;
            this.button1.Size = new System.Drawing.Size(209, 23);
            this.button1.TabIndex = 38;
            this.button1.Text = "Create pre-formatted email...";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Navy;
            this.label9.Location = new System.Drawing.Point(92, 86);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(105, 17);
            this.label9.TabIndex = 35;
            this.label9.Text = "sales@slyce.com";
            // 
            // lblWaitMessage
            // 
            this.lblWaitMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblWaitMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWaitMessage.ForeColor = System.Drawing.Color.Red;
            this.lblWaitMessage.Location = new System.Drawing.Point(74, 72);
            this.lblWaitMessage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWaitMessage.Name = "lblWaitMessage";
            this.lblWaitMessage.Size = new System.Drawing.Size(131, 24);
            this.lblWaitMessage.TabIndex = 35;
            this.lblWaitMessage.Text = "Please wait...";
            this.lblWaitMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(244, 94);
            this.button2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button2.Name = "button2";
            this.button2.OuterBorderColor = System.Drawing.Color.Transparent;
            this.button2.Size = new System.Drawing.Size(32, 22);
            this.button2.TabIndex = 37;
            this.button2.Text = ">>";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnGetLicence
            // 
            this.btnGetLicence.Location = new System.Drawing.Point(32, 46);
            this.btnGetLicence.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnGetLicence.Name = "btnGetLicence";
            this.btnGetLicence.OuterBorderColor = System.Drawing.Color.Transparent;
            this.btnGetLicence.Size = new System.Drawing.Size(102, 24);
            this.btnGetLicence.TabIndex = 36;
            this.btnGetLicence.Text = "&Activate Now";
            this.btnGetLicence.Click += new System.EventHandler(this.btnGetLicense_Click);
            // 
            // frmUnlock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(219)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(309, 524);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnGetLicence);
            this.Controls.Add(this.lblWaitMessage);
            this.Controls.Add(this.txtEmailBody);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lnkManualMethod);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtHardwareId);
            this.Controls.Add(this.lblRequestStatus);
            this.Controls.Add(this.picSerialHelp);
            this.Controls.Add(this.txtSerialNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlEmail);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmUnlock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Unlock";
            this.Shown += new System.EventHandler(this.frmUnlock_Shown);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picSerialHelp)).EndInit();
            this.panel1.ResumeLayout(false);
            this.pnlEmail.ResumeLayout(false);
            this.pnlEmail.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.Label lblRequestStatus;
		private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSerialNumber;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.PictureBox picSerialHelp;
        private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.TextBox txtEmailBody;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.LinkLabel lnkManualMethod;
        private System.Windows.Forms.TextBox txtHardwareId;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel pnlEmail;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label lblWaitMessage;
        private Slyce.Common.Controls.GlassButton btnGetLicence;
        private Slyce.Common.Controls.GlassButton button2;
        private Slyce.Common.Controls.GlassButton button1;
	}
}

