namespace ArchAngel.Designer
{
    partial class frmNewProvider
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewProvider));
			this.btnBrowse = new System.Windows.Forms.Button();
			this.btnCreate = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.txtFolder = new System.Windows.Forms.TextBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.txtNamespace = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.panelTop = new System.Windows.Forms.Panel();
			this.labelPageDescription = new System.Windows.Forms.Label();
			this.labelPageHeader = new System.Windows.Forms.Label();
			this.pictureHeading = new System.Windows.Forms.PictureBox();
			this.label3 = new System.Windows.Forms.Label();
			this.linkHelp = new System.Windows.Forms.LinkLabel();
			this.ucHeading1 = new Slyce.Common.Controls.ucHeading();
			this.panelTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureHeading)).BeginInit();
			this.SuspendLayout();
			// 
			// btnBrowse
			// 
			this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowse.Location = new System.Drawing.Point(521, 156);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(35, 23);
			this.btnBrowse.TabIndex = 0;
			this.btnBrowse.Text = "...";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// btnCreate
			// 
			this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCreate.Location = new System.Drawing.Point(399, 203);
			this.btnCreate.Name = "btnCreate";
			this.btnCreate.Size = new System.Drawing.Size(75, 23);
			this.btnCreate.TabIndex = 1;
			this.btnCreate.Text = "Create";
			this.btnCreate.UseVisualStyleBackColor = true;
			this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 161);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(49, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Create in";
			// 
			// txtFolder
			// 
			this.txtFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtFolder.Location = new System.Drawing.Point(91, 158);
			this.txtFolder.Name = "txtFolder";
			this.txtFolder.Size = new System.Drawing.Size(424, 20);
			this.txtFolder.TabIndex = 3;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(480, 203);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// txtNamespace
			// 
			this.txtNamespace.Location = new System.Drawing.Point(91, 132);
			this.txtNamespace.Name = "txtNamespace";
			this.txtNamespace.Size = new System.Drawing.Size(222, 20);
			this.txtNamespace.TabIndex = 6;
			this.txtNamespace.Text = "MyCompany.Providers.MyProvider";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 135);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Namespace";
			// 
			// panelTop
			// 
			this.panelTop.BackColor = System.Drawing.Color.White;
			this.panelTop.Controls.Add(this.labelPageDescription);
			this.panelTop.Controls.Add(this.labelPageHeader);
			this.panelTop.Controls.Add(this.pictureHeading);
			this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelTop.Location = new System.Drawing.Point(0, 0);
			this.panelTop.Margin = new System.Windows.Forms.Padding(2);
			this.panelTop.Name = "panelTop";
			this.panelTop.Size = new System.Drawing.Size(567, 46);
			this.panelTop.TabIndex = 20;
			// 
			// labelPageDescription
			// 
			this.labelPageDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.labelPageDescription.ForeColor = System.Drawing.Color.MidnightBlue;
			this.labelPageDescription.Location = new System.Drawing.Point(168, 17);
			this.labelPageDescription.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.labelPageDescription.Name = "labelPageDescription";
			this.labelPageDescription.Size = new System.Drawing.Size(397, 29);
			this.labelPageDescription.TabIndex = 1;
			this.labelPageDescription.Text = "Create skeleton code for a new ArchAngel provider (C#, Visual Studio 2005)";
			// 
			// labelPageHeader
			// 
			this.labelPageHeader.AutoSize = true;
			this.labelPageHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelPageHeader.ForeColor = System.Drawing.Color.MidnightBlue;
			this.labelPageHeader.Location = new System.Drawing.Point(154, 2);
			this.labelPageHeader.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.labelPageHeader.Name = "labelPageHeader";
			this.labelPageHeader.Size = new System.Drawing.Size(138, 15);
			this.labelPageHeader.TabIndex = 0;
			this.labelPageHeader.Text = "Create New Provider";
			// 
			// pictureHeading
			// 
			this.pictureHeading.Image = ((System.Drawing.Image)(resources.GetObject("pictureHeading.Image")));
			this.pictureHeading.Location = new System.Drawing.Point(0, 0);
			this.pictureHeading.Margin = new System.Windows.Forms.Padding(2);
			this.pictureHeading.Name = "pictureHeading";
			this.pictureHeading.Size = new System.Drawing.Size(150, 57);
			this.pictureHeading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureHeading.TabIndex = 2;
			this.pictureHeading.TabStop = false;
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.label3.Location = new System.Drawing.Point(16, 48);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(540, 58);
			this.label3.TabIndex = 21;
			this.label3.Text = resources.GetString("label3.Text");
			// 
			// linkHelp
			// 
			this.linkHelp.AutoSize = true;
			this.linkHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.linkHelp.Location = new System.Drawing.Point(16, 106);
			this.linkHelp.Name = "linkHelp";
			this.linkHelp.Size = new System.Drawing.Size(192, 13);
			this.linkHelp.TabIndex = 22;
			this.linkHelp.TabStop = true;
			this.linkHelp.Text = "Click here for more information...";
			this.linkHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkHelp_LinkClicked);
			// 
			// ucHeading1
			// 
			this.ucHeading1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ucHeading1.Location = new System.Drawing.Point(0, 195);
			this.ucHeading1.Margin = new System.Windows.Forms.Padding(2);
			this.ucHeading1.Name = "ucHeading1";
			this.ucHeading1.Size = new System.Drawing.Size(567, 39);
			this.ucHeading1.TabIndex = 18;
			this.ucHeading1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// frmNewProvider
			// 
			this.AcceptButton = this.btnCreate;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(567, 234);
			this.Controls.Add(this.linkHelp);
			this.Controls.Add(this.panelTop);
			this.Controls.Add(this.txtNamespace);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.txtFolder);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnCreate);
			this.Controls.Add(this.btnBrowse);
			this.Controls.Add(this.ucHeading1);
			this.Controls.Add(this.label3);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(575, 268);
			this.Name = "frmNewProvider";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "New Provider";
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmNewProvider_Paint);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmNewProvider_FormClosing);
			this.panelTop.ResumeLayout(false);
			this.panelTop.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureHeading)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtNamespace;
        private System.Windows.Forms.Label label2;
        private Slyce.Common.Controls.ucHeading ucHeading1;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label labelPageDescription;
        private System.Windows.Forms.Label labelPageHeader;
        private System.Windows.Forms.PictureBox pictureHeading;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel linkHelp;
    }
}