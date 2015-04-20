namespace ArchAngel.Providers.Database.Controls
{
    partial class FormFilter2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFilter2));
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.ucHeading1 = new Slyce.Common.Controls.ucHeading();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelTop = new System.Windows.Forms.Panel();
            this.labelPageDescription = new System.Windows.Forms.Label();
            this.labelPageHeader = new System.Windows.Forms.Label();
            this.pictureHeading = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHeading)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonNext
            // 
            this.buttonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNext.CausesValidation = false;
            this.buttonNext.Location = new System.Drawing.Point(657, 432);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(75, 23);
            this.buttonNext.TabIndex = 16;
            this.buttonNext.Text = "&Next >";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBack.Location = new System.Drawing.Point(576, 432);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(75, 23);
            this.buttonBack.TabIndex = 15;
            this.buttonBack.Text = "< &Back";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // ucHeading1
            // 
            this.ucHeading1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucHeading1.Location = new System.Drawing.Point(0, 425);
            this.ucHeading1.Margin = new System.Windows.Forms.Padding(2);
            this.ucHeading1.Name = "ucHeading1";
            this.ucHeading1.Size = new System.Drawing.Size(738, 39);
            this.ucHeading1.TabIndex = 17;
            // 
            // panelContent
            // 
            this.panelContent.Location = new System.Drawing.Point(246, 128);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(200, 100);
            this.panelContent.TabIndex = 18;
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
            this.panelTop.Size = new System.Drawing.Size(738, 46);
            this.panelTop.TabIndex = 19;
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
            this.labelPageDescription.Size = new System.Drawing.Size(568, 29);
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
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(12, 432);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 20;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FormFilter2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(738, 464);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.ucHeading1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormFilter2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Filter";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormFilter2_FormClosed);
            this.Resize += new System.EventHandler(this.FormFilter2_Resize);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormFilter2_FormClosing);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHeading)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonBack;
        private Slyce.Common.Controls.ucHeading ucHeading1;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label labelPageDescription;
        private System.Windows.Forms.Label labelPageHeader;
        private System.Windows.Forms.PictureBox pictureHeading;
        private System.Windows.Forms.Button btnClose;
    }
}