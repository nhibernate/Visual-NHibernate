namespace ArchAngel.Providers.Database.Controls.AssociationWizard
{
    partial class frmAssociationWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAssociationWizard));
            this.panelContent = new System.Windows.Forms.Panel();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.labelPageDescription = new System.Windows.Forms.Label();
            this.labelPageHeader = new System.Windows.Forms.Label();
            this.pictureHeading = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.ucHeading1 = new Slyce.Common.Controls.ucHeading();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHeading)).BeginInit();
            this.SuspendLayout();
            // 
            // panelContent
            // 
            this.panelContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContent.Location = new System.Drawing.Point(0, 49);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(718, 298);
            this.panelContent.TabIndex = 31;
            // 
            // buttonBack
            // 
            this.buttonBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBack.Location = new System.Drawing.Point(486, 358);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(97, 23);
            this.buttonBack.TabIndex = 30;
            this.buttonBack.Text = "< &Back";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonNext
            // 
            this.buttonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNext.Location = new System.Drawing.Point(601, 358);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(102, 23);
            this.buttonNext.TabIndex = 29;
            this.buttonNext.Text = "&Next >";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
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
            this.panelTop.Size = new System.Drawing.Size(718, 46);
            this.panelTop.TabIndex = 32;
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
            this.labelPageDescription.Size = new System.Drawing.Size(548, 29);
            this.labelPageDescription.TabIndex = 1;
            this.labelPageDescription.Text = "labelPageDescription";
            // 
            // labelPageHeader
            // 
            this.labelPageHeader.AutoSize = true;
            this.labelPageHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPageHeader.ForeColor = System.Drawing.Color.MidnightBlue;
            this.labelPageHeader.Location = new System.Drawing.Point(154, 2);
            this.labelPageHeader.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelPageHeader.Name = "labelPageHeader";
            this.labelPageHeader.Size = new System.Drawing.Size(119, 15);
            this.labelPageHeader.TabIndex = 0;
            this.labelPageHeader.Text = "labelPageHeader";
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
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(12, 359);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(97, 23);
            this.btnCancel.TabIndex = 33;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // ucHeading1
            // 
            this.ucHeading1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucHeading1.Location = new System.Drawing.Point(0, 351);
            this.ucHeading1.Name = "ucHeading1";
            this.ucHeading1.Size = new System.Drawing.Size(718, 35);
            this.ucHeading1.TabIndex = 28;
            // 
            // frmAssociationWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 386);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.ucHeading1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAssociationWizard";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Association Wizard";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmAssociationWizard_Paint);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAssociationWizard_FormClosing);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHeading)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label labelPageDescription;
        private System.Windows.Forms.Label labelPageHeader;
        private System.Windows.Forms.PictureBox pictureHeading;
        private System.Windows.Forms.Button btnCancel;
        private Slyce.Common.Controls.ucHeading ucHeading1;

    }
}