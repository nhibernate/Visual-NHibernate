namespace ArchAngel.Licensing
{
    partial class frmStatus
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStatus));
			this.buttonRemove = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.txtInstallationID = new System.Windows.Forms.TextBox();
			this.buttonClose = new System.Windows.Forms.Button();
			this.labelErrorMessage = new System.Windows.Forms.Label();
			this.labelStatus = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.listDetails = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ucHeading1 = new Slyce.Common.Controls.ucHeading();
			this.txtSerial = new System.Windows.Forms.TextBox();
			this.labelSerial = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// buttonRemove
			// 
			this.buttonRemove.Image = ((System.Drawing.Image)(resources.GetObject("buttonRemove.Image")));
			this.buttonRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonRemove.Location = new System.Drawing.Point(215, 2);
			this.buttonRemove.Name = "buttonRemove";
			this.buttonRemove.Size = new System.Drawing.Size(130, 27);
			this.buttonRemove.TabIndex = 0;
			this.buttonRemove.Text = "      Remove License...";
			this.buttonRemove.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonRemove.UseVisualStyleBackColor = true;
			this.buttonRemove.Visible = false;
			this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 72);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(67, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Hardware ID";
			// 
			// txtInstallationID
			// 
			this.txtInstallationID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtInstallationID.Location = new System.Drawing.Point(109, 69);
			this.txtInstallationID.Name = "txtInstallationID";
			this.txtInstallationID.Size = new System.Drawing.Size(360, 20);
			this.txtInstallationID.TabIndex = 2;
			// 
			// buttonClose
			// 
			this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonClose.Location = new System.Drawing.Point(394, 334);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(85, 23);
			this.buttonClose.TabIndex = 5;
			this.buttonClose.Text = "Close";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// labelErrorMessage
			// 
			this.labelErrorMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.labelErrorMessage.ForeColor = System.Drawing.Color.Red;
			this.labelErrorMessage.Location = new System.Drawing.Point(16, 9);
			this.labelErrorMessage.Name = "labelErrorMessage";
			this.labelErrorMessage.Size = new System.Drawing.Size(463, 13);
			this.labelErrorMessage.TabIndex = 7;
			this.labelErrorMessage.Text = "Error message";
			this.labelErrorMessage.Visible = false;
			// 
			// labelStatus
			// 
			this.labelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.labelStatus.Location = new System.Drawing.Point(106, 22);
			this.labelStatus.Name = "labelStatus";
			this.labelStatus.Size = new System.Drawing.Size(373, 27);
			this.labelStatus.TabIndex = 8;
			this.labelStatus.Text = "License Number";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(16, 22);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(37, 13);
			this.label4.TabIndex = 9;
			this.label4.Text = "Status";
			// 
			// listDetails
			// 
			this.listDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.listDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
			this.listDetails.Location = new System.Drawing.Point(19, 110);
			this.listDetails.Name = "listDetails";
			this.listDetails.Size = new System.Drawing.Size(450, 200);
			this.listDetails.TabIndex = 10;
			this.listDetails.UseCompatibleStateImageBehavior = false;
			this.listDetails.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Key";
			this.columnHeader1.Width = 82;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Value";
			this.columnHeader2.Width = 332;
			// 
			// ucHeading1
			// 
			this.ucHeading1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ucHeading1.Location = new System.Drawing.Point(0, 332);
			this.ucHeading1.Name = "ucHeading1";
			this.ucHeading1.Size = new System.Drawing.Size(491, 35);
			this.ucHeading1.TabIndex = 6;
			this.ucHeading1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// txtSerial
			// 
			this.txtSerial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtSerial.Location = new System.Drawing.Point(109, 43);
			this.txtSerial.Name = "txtSerial";
			this.txtSerial.Size = new System.Drawing.Size(360, 20);
			this.txtSerial.TabIndex = 13;
			// 
			// labelSerial
			// 
			this.labelSerial.AutoSize = true;
			this.labelSerial.Location = new System.Drawing.Point(16, 46);
			this.labelSerial.Name = "labelSerial";
			this.labelSerial.Size = new System.Drawing.Size(71, 13);
			this.labelSerial.TabIndex = 12;
			this.labelSerial.Text = "Serial number";
			// 
			// frmStatus
			// 
			this.AcceptButton = this.buttonClose;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonClose;
			this.ClientSize = new System.Drawing.Size(491, 367);
			this.Controls.Add(this.txtSerial);
			this.Controls.Add(this.labelSerial);
			this.Controls.Add(this.listDetails);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.labelStatus);
			this.Controls.Add(this.labelErrorMessage);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.txtInstallationID);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonRemove);
			this.Controls.Add(this.ucHeading1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(497, 395);
			this.Name = "frmStatus";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "License Details";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInstallationID;
        private System.Windows.Forms.Button buttonClose;
        private Slyce.Common.Controls.ucHeading ucHeading1;
        private System.Windows.Forms.Label labelErrorMessage;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView listDetails;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TextBox txtSerial;
        private System.Windows.Forms.Label labelSerial;
    }
}