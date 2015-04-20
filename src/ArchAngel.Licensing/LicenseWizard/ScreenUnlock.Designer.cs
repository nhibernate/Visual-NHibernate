namespace ArchAngel.Licensing.LicenseWizard
{
    partial class ScreenUnlock
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScreenUnlock));
            this.labelDescription = new System.Windows.Forms.Label();
            this.panelWebsiteActivation = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtLicenseFile2 = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.linkWebsite = new System.Windows.Forms.LinkLabel();
            this.label13 = new System.Windows.Forms.Label();
            this.buttonCopyLicenseNumber = new System.Windows.Forms.Button();
            this.buttonCopyInstallationID = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lblWebsiteLicenseNumber = new System.Windows.Forms.Label();
            this.txtWebsiteLicenseNumber = new System.Windows.Forms.TextBox();
            this.txtWebsiteInstallationID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panelEmailActivation = new System.Windows.Forms.Panel();
            this.btnCreateEmail = new System.Windows.Forms.Button();
            this.txtEmailBody = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtEmailSubject = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtEmailTo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.txtLicenseFile = new System.Windows.Forms.TextBox();
            this.labelEmailStep2 = new System.Windows.Forms.Label();
            this.panelExtendTrial = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.txtExtendTrial = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.buttonExtendTrial = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panelWebsiteActivation.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panelEmailActivation.SuspendLayout();
            this.panelExtendTrial.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelDescription
            // 
            this.labelDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDescription.Location = new System.Drawing.Point(25, 23);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(421, 22);
            this.labelDescription.TabIndex = 2;
            this.labelDescription.Text = "Your browser is opening the unlock webpage. Enter your serial number and Hardware" +
                " ID.";
            this.labelDescription.Visible = false;
            // 
            // panelWebsiteActivation
            // 
            this.panelWebsiteActivation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelWebsiteActivation.Controls.Add(this.label6);
            this.panelWebsiteActivation.Controls.Add(this.button1);
            this.panelWebsiteActivation.Controls.Add(this.txtLicenseFile2);
            this.panelWebsiteActivation.Controls.Add(this.flowLayoutPanel1);
            this.panelWebsiteActivation.Controls.Add(this.buttonCopyLicenseNumber);
            this.panelWebsiteActivation.Controls.Add(this.buttonCopyInstallationID);
            this.panelWebsiteActivation.Controls.Add(this.label5);
            this.panelWebsiteActivation.Controls.Add(this.lblWebsiteLicenseNumber);
            this.panelWebsiteActivation.Controls.Add(this.txtWebsiteLicenseNumber);
            this.panelWebsiteActivation.Controls.Add(this.txtWebsiteInstallationID);
            this.panelWebsiteActivation.Controls.Add(this.label3);
            this.panelWebsiteActivation.Location = new System.Drawing.Point(3, 78);
            this.panelWebsiteActivation.Name = "panelWebsiteActivation";
            this.panelWebsiteActivation.Size = new System.Drawing.Size(457, 174);
            this.panelWebsiteActivation.TabIndex = 3;
            this.panelWebsiteActivation.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(36, 154);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(122, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Downloaded license file:";
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(370, 147);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(24, 24);
            this.button1.TabIndex = 21;
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.button1, "Copy to clipboard");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // txtLicenseFile2
            // 
            this.txtLicenseFile2.Location = new System.Drawing.Point(164, 151);
            this.txtLicenseFile2.Name = "txtLicenseFile2";
            this.txtLicenseFile2.Size = new System.Drawing.Size(200, 20);
            this.txtLicenseFile2.TabIndex = 20;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.linkWebsite);
            this.flowLayoutPanel1.Controls.Add(this.label13);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(34, 19);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(383, 25);
            this.flowLayoutPanel1.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "1) Browse to";
            // 
            // linkWebsite
            // 
            this.linkWebsite.AutoSize = true;
            this.linkWebsite.Location = new System.Drawing.Point(66, 0);
            this.linkWebsite.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.linkWebsite.Name = "linkWebsite";
            this.linkWebsite.Size = new System.Drawing.Size(126, 13);
            this.linkWebsite.TabIndex = 1;
            this.linkWebsite.TabStop = true;
            this.linkWebsite.Text = "http://activate.slyce.com";
            this.linkWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(195, 0);
            this.label13.Margin = new System.Windows.Forms.Padding(0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(114, 13);
            this.label13.TabIndex = 2;
            this.label13.Text = "(click to open browser)";
            // 
            // buttonCopyLicenseNumber
            // 
            this.buttonCopyLicenseNumber.Image = ((System.Drawing.Image)(resources.GetObject("buttonCopyLicenseNumber.Image")));
            this.buttonCopyLicenseNumber.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCopyLicenseNumber.Location = new System.Drawing.Point(340, 97);
            this.buttonCopyLicenseNumber.Name = "buttonCopyLicenseNumber";
            this.buttonCopyLicenseNumber.Size = new System.Drawing.Size(24, 24);
            this.buttonCopyLicenseNumber.TabIndex = 12;
            this.buttonCopyLicenseNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.buttonCopyLicenseNumber, "Copy to clipboard");
            this.buttonCopyLicenseNumber.UseVisualStyleBackColor = true;
            this.buttonCopyLicenseNumber.Click += new System.EventHandler(this.buttonCopyLicenseNumber_Click);
            // 
            // buttonCopyInstallationID
            // 
            this.buttonCopyInstallationID.Image = ((System.Drawing.Image)(resources.GetObject("buttonCopyInstallationID.Image")));
            this.buttonCopyInstallationID.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCopyInstallationID.Location = new System.Drawing.Point(340, 71);
            this.buttonCopyInstallationID.Name = "buttonCopyInstallationID";
            this.buttonCopyInstallationID.Size = new System.Drawing.Size(24, 24);
            this.buttonCopyInstallationID.TabIndex = 11;
            this.buttonCopyInstallationID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.buttonCopyInstallationID, "Copy to clipboard");
            this.buttonCopyInstallationID.UseVisualStyleBackColor = true;
            this.buttonCopyInstallationID.Click += new System.EventHandler(this.buttonCopyInstallationID_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(290, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "2) Enter these details on the webpage then click \'Download\'";
            // 
            // lblWebsiteLicenseNumber
            // 
            this.lblWebsiteLicenseNumber.AutoSize = true;
            this.lblWebsiteLicenseNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWebsiteLicenseNumber.Location = new System.Drawing.Point(57, 103);
            this.lblWebsiteLicenseNumber.Name = "lblWebsiteLicenseNumber";
            this.lblWebsiteLicenseNumber.Size = new System.Drawing.Size(76, 13);
            this.lblWebsiteLicenseNumber.TabIndex = 6;
            this.lblWebsiteLicenseNumber.Text = "Serial Number:";
            // 
            // txtWebsiteLicenseNumber
            // 
            this.txtWebsiteLicenseNumber.Location = new System.Drawing.Point(133, 100);
            this.txtWebsiteLicenseNumber.Name = "txtWebsiteLicenseNumber";
            this.txtWebsiteLicenseNumber.ReadOnly = true;
            this.txtWebsiteLicenseNumber.Size = new System.Drawing.Size(201, 20);
            this.txtWebsiteLicenseNumber.TabIndex = 5;
            // 
            // txtWebsiteInstallationID
            // 
            this.txtWebsiteInstallationID.Location = new System.Drawing.Point(133, 74);
            this.txtWebsiteInstallationID.Name = "txtWebsiteInstallationID";
            this.txtWebsiteInstallationID.ReadOnly = true;
            this.txtWebsiteInstallationID.Size = new System.Drawing.Size(201, 20);
            this.txtWebsiteInstallationID.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(57, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Hardware ID:";
            // 
            // panelEmailActivation
            // 
            this.panelEmailActivation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEmailActivation.Controls.Add(this.btnCreateEmail);
            this.panelEmailActivation.Controls.Add(this.txtEmailBody);
            this.panelEmailActivation.Controls.Add(this.label12);
            this.panelEmailActivation.Controls.Add(this.txtEmailSubject);
            this.panelEmailActivation.Controls.Add(this.label9);
            this.panelEmailActivation.Controls.Add(this.txtEmailTo);
            this.panelEmailActivation.Controls.Add(this.label8);
            this.panelEmailActivation.Controls.Add(this.label1);
            this.panelEmailActivation.Controls.Add(this.buttonBrowse);
            this.panelEmailActivation.Controls.Add(this.txtLicenseFile);
            this.panelEmailActivation.Controls.Add(this.labelEmailStep2);
            this.panelEmailActivation.Location = new System.Drawing.Point(3, 258);
            this.panelEmailActivation.Name = "panelEmailActivation";
            this.panelEmailActivation.Size = new System.Drawing.Size(457, 264);
            this.panelEmailActivation.TabIndex = 11;
            this.panelEmailActivation.Visible = false;
            // 
            // btnCreateEmail
            // 
            this.btnCreateEmail.Location = new System.Drawing.Point(295, 166);
            this.btnCreateEmail.Name = "btnCreateEmail";
            this.btnCreateEmail.Size = new System.Drawing.Size(88, 24);
            this.btnCreateEmail.TabIndex = 26;
            this.btnCreateEmail.Text = "Create Email";
            this.btnCreateEmail.UseVisualStyleBackColor = true;
            this.btnCreateEmail.Click += new System.EventHandler(this.btnCreateEmail_Click);
            // 
            // txtEmailBody
            // 
            this.txtEmailBody.Location = new System.Drawing.Point(83, 62);
            this.txtEmailBody.Multiline = true;
            this.txtEmailBody.Name = "txtEmailBody";
            this.txtEmailBody.ReadOnly = true;
            this.txtEmailBody.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEmailBody.Size = new System.Drawing.Size(300, 98);
            this.txtEmailBody.TabIndex = 25;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(31, 65);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 13);
            this.label12.TabIndex = 24;
            this.label12.Text = "Body:";
            // 
            // txtEmailSubject
            // 
            this.txtEmailSubject.Location = new System.Drawing.Point(83, 36);
            this.txtEmailSubject.Name = "txtEmailSubject";
            this.txtEmailSubject.ReadOnly = true;
            this.txtEmailSubject.Size = new System.Drawing.Size(300, 20);
            this.txtEmailSubject.TabIndex = 23;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(31, 39);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Subject:";
            // 
            // txtEmailTo
            // 
            this.txtEmailTo.Location = new System.Drawing.Point(83, 10);
            this.txtEmailTo.Name = "txtEmailTo";
            this.txtEmailTo.ReadOnly = true;
            this.txtEmailTo.Size = new System.Drawing.Size(200, 20);
            this.txtEmailTo.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(31, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "To:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(36, 239);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Received license file:";
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Image = ((System.Drawing.Image)(resources.GetObject("buttonBrowse.Image")));
            this.buttonBrowse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonBrowse.Location = new System.Drawing.Point(370, 232);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(24, 24);
            this.buttonBrowse.TabIndex = 17;
            this.buttonBrowse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.buttonBrowse, "Copy to clipboard");
            this.buttonBrowse.UseVisualStyleBackColor = true;
            // 
            // txtLicenseFile
            // 
            this.txtLicenseFile.Location = new System.Drawing.Point(164, 236);
            this.txtLicenseFile.Name = "txtLicenseFile";
            this.txtLicenseFile.Size = new System.Drawing.Size(200, 20);
            this.txtLicenseFile.TabIndex = 16;
            // 
            // labelEmailStep2
            // 
            this.labelEmailStep2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelEmailStep2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEmailStep2.Location = new System.Drawing.Point(34, 209);
            this.labelEmailStep2.Name = "labelEmailStep2";
            this.labelEmailStep2.Size = new System.Drawing.Size(407, 20);
            this.labelEmailStep2.TabIndex = 13;
            this.labelEmailStep2.Text = "Your license file will be emailed to you:";
            // 
            // panelExtendTrial
            // 
            this.panelExtendTrial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelExtendTrial.Controls.Add(this.label11);
            this.panelExtendTrial.Controls.Add(this.txtExtendTrial);
            this.panelExtendTrial.Controls.Add(this.label10);
            this.panelExtendTrial.Controls.Add(this.buttonExtendTrial);
            this.panelExtendTrial.Location = new System.Drawing.Point(6, 528);
            this.panelExtendTrial.Name = "panelExtendTrial";
            this.panelExtendTrial.Size = new System.Drawing.Size(457, 202);
            this.panelExtendTrial.TabIndex = 14;
            this.panelExtendTrial.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(36, 106);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(16, 13);
            this.label11.TabIndex = 16;
            this.label11.Text = "2)";
            // 
            // txtExtendTrial
            // 
            this.txtExtendTrial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExtendTrial.Location = new System.Drawing.Point(60, 37);
            this.txtExtendTrial.Multiline = true;
            this.txtExtendTrial.Name = "txtExtendTrial";
            this.txtExtendTrial.Size = new System.Drawing.Size(383, 52);
            this.txtExtendTrial.TabIndex = 15;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(36, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(232, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "1) Why do you want to extend your 30-day trial?";
            // 
            // buttonExtendTrial
            // 
            this.buttonExtendTrial.Image = ((System.Drawing.Image)(resources.GetObject("buttonExtendTrial.Image")));
            this.buttonExtendTrial.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonExtendTrial.Location = new System.Drawing.Point(58, 101);
            this.buttonExtendTrial.Name = "buttonExtendTrial";
            this.buttonExtendTrial.Size = new System.Drawing.Size(138, 23);
            this.buttonExtendTrial.TabIndex = 1;
            this.buttonExtendTrial.Text = "      Create email request";
            this.buttonExtendTrial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonExtendTrial.UseVisualStyleBackColor = true;
            this.buttonExtendTrial.Click += new System.EventHandler(this.buttonExtendTrial_Click);
            // 
            // ScreenUnlock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelExtendTrial);
            this.Controls.Add(this.panelEmailActivation);
            this.Controls.Add(this.panelWebsiteActivation);
            this.Controls.Add(this.labelDescription);
            this.Name = "ScreenUnlock";
            this.Size = new System.Drawing.Size(463, 763);
            this.panelWebsiteActivation.ResumeLayout(false);
            this.panelWebsiteActivation.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.panelEmailActivation.ResumeLayout(false);
            this.panelEmailActivation.PerformLayout();
            this.panelExtendTrial.ResumeLayout(false);
            this.panelExtendTrial.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Panel panelWebsiteActivation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblWebsiteLicenseNumber;
        private System.Windows.Forms.TextBox txtWebsiteLicenseNumber;
        private System.Windows.Forms.TextBox txtWebsiteInstallationID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panelEmailActivation;
        private System.Windows.Forms.Label labelEmailStep2;
        private System.Windows.Forms.Button buttonCopyInstallationID;
        private System.Windows.Forms.Button buttonCopyLicenseNumber;
        private System.Windows.Forms.Panel panelExtendTrial;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtExtendTrial;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button buttonExtendTrial;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.LinkLabel linkWebsite;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.TextBox txtLicenseFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtLicenseFile2;
        private System.Windows.Forms.TextBox txtEmailSubject;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtEmailTo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnCreateEmail;
        private System.Windows.Forms.TextBox txtEmailBody;
        private System.Windows.Forms.Label label12;
    }
}
