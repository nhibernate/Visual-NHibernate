namespace ArchAngel.Workbench
{
    partial class FormOptions
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOptions));
			this.label13 = new System.Windows.Forms.Label();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.buttonClose = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.eventLog1 = new System.Diagnostics.EventLog();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.panelContent = new System.Windows.Forms.Panel();
			this.buttonNext = new System.Windows.Forms.Button();
			this.buttonBack = new System.Windows.Forms.Button();
			this.headingContentBottom = new Slyce.Common.Controls.ucHeading();
			this.panelTop = new System.Windows.Forms.Panel();
			this.labelPageDescription = new System.Windows.Forms.Label();
			this.labelPageHeader = new System.Windows.Forms.Label();
			this.pictureHeading = new System.Windows.Forms.PictureBox();
			this.imageListHeading = new System.Windows.Forms.ImageList(this.components);
			this.ucHeading1 = new Slyce.Common.Controls.ucHeading();
			this.sequentialNavBar1 = new ArchAngel.Workbench.UserControls.SequentialNavBar();
			this.headingContentTitle = new ArchAngel.Workbench.UserControls.Heading();
			((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.panelTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureHeading)).BeginInit();
			this.SuspendLayout();
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.BackColor = System.Drawing.Color.Transparent;
			this.label13.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label13.Location = new System.Drawing.Point(10, 29);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(178, 13);
			this.label13.TabIndex = 9;
			this.label13.Text = "Database Schema Update Template";
			// 
			// openFileDialog
			// 
			this.openFileDialog.FileName = "openFileDialog1";
			// 
			// buttonClose
			// 
			this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonClose.Location = new System.Drawing.Point(529, 303);
			this.buttonClose.Margin = new System.Windows.Forms.Padding(2);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(66, 26);
			this.buttonClose.TabIndex = 12;
			this.buttonClose.Text = "Close";
			this.buttonClose.UseVisualStyleBackColor = true;
			// 
			// eventLog1
			// 
			this.eventLog1.SynchronizingObject = this;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.sequentialNavBar1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.headingContentTitle);
			this.splitContainer1.Panel2.Controls.Add(this.panelContent);
			this.splitContainer1.Panel2.Controls.Add(this.buttonNext);
			this.splitContainer1.Panel2.Controls.Add(this.buttonBack);
			this.splitContainer1.Panel2.Controls.Add(this.headingContentBottom);
			this.splitContainer1.Panel2.Controls.Add(this.panelTop);
			this.splitContainer1.Size = new System.Drawing.Size(557, 440);
			this.splitContainer1.SplitterDistance = 185;
			this.splitContainer1.TabIndex = 0;
			this.splitContainer1.Resize += new System.EventHandler(this.splitContainer1_Resize);
			// 
			// panelContent
			// 
			this.panelContent.Location = new System.Drawing.Point(57, 89);
			this.panelContent.Margin = new System.Windows.Forms.Padding(2);
			this.panelContent.Name = "panelContent";
			this.panelContent.Size = new System.Drawing.Size(287, 257);
			this.panelContent.TabIndex = 12;
			// 
			// buttonNext
			// 
			this.buttonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonNext.Location = new System.Drawing.Point(212, 411);
			this.buttonNext.Margin = new System.Windows.Forms.Padding(2);
			this.buttonNext.Name = "buttonNext";
			this.buttonNext.Size = new System.Drawing.Size(64, 27);
			this.buttonNext.TabIndex = 11;
			this.buttonNext.Text = "&Save";
			this.buttonNext.UseVisualStyleBackColor = true;
			this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
			// 
			// buttonBack
			// 
			this.buttonBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonBack.Location = new System.Drawing.Point(280, 411);
			this.buttonBack.Margin = new System.Windows.Forms.Padding(2);
			this.buttonBack.Name = "buttonBack";
			this.buttonBack.Size = new System.Drawing.Size(64, 27);
			this.buttonBack.TabIndex = 10;
			this.buttonBack.Text = "&Cancel";
			this.buttonBack.UseVisualStyleBackColor = true;
			this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
			// 
			// headingContentBottom
			// 
			this.headingContentBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.headingContentBottom.Location = new System.Drawing.Point(0, 401);
			this.headingContentBottom.Margin = new System.Windows.Forms.Padding(2);
			this.headingContentBottom.Name = "headingContentBottom";
			this.headingContentBottom.Size = new System.Drawing.Size(368, 39);
			this.headingContentBottom.TabIndex = 9;
			this.headingContentBottom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// panelTop
			// 
			this.panelTop.BackColor = System.Drawing.Color.White;
			this.panelTop.Controls.Add(this.labelPageDescription);
			this.panelTop.Controls.Add(this.labelPageHeader);
			this.panelTop.Controls.Add(this.pictureHeading);
			this.panelTop.Location = new System.Drawing.Point(9, 49);
			this.panelTop.Margin = new System.Windows.Forms.Padding(2);
			this.panelTop.Name = "panelTop";
			this.panelTop.Size = new System.Drawing.Size(357, 46);
			this.panelTop.TabIndex = 8;
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
			this.labelPageDescription.Size = new System.Drawing.Size(187, 29);
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
			// imageListHeading
			// 
			this.imageListHeading.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListHeading.ImageStream")));
			this.imageListHeading.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListHeading.Images.SetKeyName(0, "Header_ProjectDetails.bmp");
			this.imageListHeading.Images.SetKeyName(1, "Header_Database.bmp");
			this.imageListHeading.Images.SetKeyName(2, "Header_Model.bmp");
			this.imageListHeading.Images.SetKeyName(3, "Header_Options.bmp");
			this.imageListHeading.Images.SetKeyName(4, "Header_Generation.bmp");
			// 
			// ucHeading1
			// 
			this.ucHeading1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ucHeading1.Location = new System.Drawing.Point(0, 298);
			this.ucHeading1.Margin = new System.Windows.Forms.Padding(2);
			this.ucHeading1.Name = "ucHeading1";
			this.ucHeading1.Size = new System.Drawing.Size(601, 35);
			this.ucHeading1.TabIndex = 13;
			this.ucHeading1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// sequentialNavBar1
			// 
			this.sequentialNavBar1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.sequentialNavBar1.Location = new System.Drawing.Point(0, 0);
			this.sequentialNavBar1.Name = "sequentialNavBar1";
			this.sequentialNavBar1.Size = new System.Drawing.Size(185, 440);
			this.sequentialNavBar1.TabIndex = 0;
			// 
			// headingContentTitle
			// 
			this.headingContentTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.headingContentTitle.Location = new System.Drawing.Point(0, 0);
			this.headingContentTitle.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.headingContentTitle.Name = "headingContentTitle";
			this.headingContentTitle.Size = new System.Drawing.Size(368, 20);
			this.headingContentTitle.TabIndex = 13;
			// 
			// FormOptions
			// 
			this.ClientSize = new System.Drawing.Size(557, 440);
			this.Controls.Add(this.splitContainer1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormOptions";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Options";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormOptions_FormClosed);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormOptions_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.panelTop.ResumeLayout(false);
			this.panelTop.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureHeading)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.ToolTip toolTip1;
        private Slyce.Common.Controls.ucHeading ucHeading1;
        private System.Diagnostics.EventLog eventLog1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private ArchAngel.Workbench.UserControls.SequentialNavBar sequentialNavBar1;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label labelPageDescription;
        private System.Windows.Forms.Label labelPageHeader;
        private System.Windows.Forms.PictureBox pictureHeading;
        private Slyce.Common.Controls.ucHeading headingContentBottom;
        public System.Windows.Forms.Button buttonNext;
        public System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Panel panelContent;
        private ArchAngel.Workbench.UserControls.Heading headingContentTitle;
        private System.Windows.Forms.ImageList imageListHeading;
    }
}
