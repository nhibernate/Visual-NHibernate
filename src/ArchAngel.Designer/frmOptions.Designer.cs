namespace ArchAngel.Designer
{
    partial class frmOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOptions));
            this.tabStrip1 = new ActiproSoftware.UIStudio.TabStrip.TabStrip();
            this.tabStripPageFiles = new ActiproSoftware.UIStudio.TabStrip.TabStripPage();
            this.chkStartNewTemplate = new System.Windows.Forms.CheckBox();
            this.tabStripPageDebugSettings = new ActiproSoftware.UIStudio.TabStrip.TabStripPage();
            this.buttonGenerationOutput = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDebugGenerationOutputPath = new System.Windows.Forms.TextBox();
            this.buttonTemplateFileName = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDebugCSPDatabasePath = new System.Windows.Forms.TextBox();
            this.tabStripPagePrograms = new ActiproSoftware.UIStudio.TabStrip.TabStripPage();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.txtILMergeLocation = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnILMergeLocation = new System.Windows.Forms.Button();
            this.tabStripPageGeneral = new ActiproSoftware.UIStudio.TabStrip.TabStripPage();
            this.btnColorBrowse = new System.Windows.Forms.Button();
            this.chkUseThemeColor = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.ucHeading3 = new Slyce.Common.Controls.ucHeading();
            ((System.ComponentModel.ISupportInitialize)(this.tabStrip1)).BeginInit();
            this.tabStrip1.SuspendLayout();
            this.tabStripPageFiles.SuspendLayout();
            this.tabStripPageDebugSettings.SuspendLayout();
            this.tabStripPagePrograms.SuspendLayout();
            this.tabStripPageGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabStrip1
            // 
            this.tabStrip1.AutoSetFocusOnClick = true;
            this.tabStrip1.Controls.Add(this.tabStripPageFiles);
            this.tabStrip1.Controls.Add(this.tabStripPageDebugSettings);
            this.tabStrip1.Controls.Add(this.tabStripPagePrograms);
            this.tabStrip1.Controls.Add(this.tabStripPageGeneral);
            this.tabStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabStrip1.Location = new System.Drawing.Point(0, 0);
            this.tabStrip1.Margin = new System.Windows.Forms.Padding(2);
            this.tabStrip1.Name = "tabStrip1";
            this.tabStrip1.Size = new System.Drawing.Size(521, 313);
            this.tabStrip1.TabIndex = 3;
            this.tabStrip1.Text = "tabStrip1";
            // 
            // tabStripPageFiles
            // 
            this.tabStripPageFiles.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tabStripPageFiles.Controls.Add(this.chkStartNewTemplate);
            this.tabStripPageFiles.Key = "TabStripPage";
            this.tabStripPageFiles.Location = new System.Drawing.Point(0, 21);
            this.tabStripPageFiles.Margin = new System.Windows.Forms.Padding(2);
            this.tabStripPageFiles.Name = "tabStripPageFiles";
            this.tabStripPageFiles.Size = new System.Drawing.Size(521, 292);
            this.tabStripPageFiles.TabIndex = 0;
            this.tabStripPageFiles.Text = "Files";
            // 
            // chkStartNewTemplate
            // 
            this.chkStartNewTemplate.AutoSize = true;
            this.chkStartNewTemplate.BackColor = System.Drawing.Color.Transparent;
            this.chkStartNewTemplate.Location = new System.Drawing.Point(2, 14);
            this.chkStartNewTemplate.Margin = new System.Windows.Forms.Padding(2);
            this.chkStartNewTemplate.Name = "chkStartNewTemplate";
            this.chkStartNewTemplate.Size = new System.Drawing.Size(136, 17);
            this.chkStartNewTemplate.TabIndex = 0;
            this.chkStartNewTemplate.Text = "Start with new template";
            this.chkStartNewTemplate.UseVisualStyleBackColor = false;
            // 
            // tabStripPageDebugSettings
            // 
            this.tabStripPageDebugSettings.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tabStripPageDebugSettings.Controls.Add(this.buttonGenerationOutput);
            this.tabStripPageDebugSettings.Controls.Add(this.label4);
            this.tabStripPageDebugSettings.Controls.Add(this.txtDebugGenerationOutputPath);
            this.tabStripPageDebugSettings.Controls.Add(this.buttonTemplateFileName);
            this.tabStripPageDebugSettings.Controls.Add(this.label2);
            this.tabStripPageDebugSettings.Controls.Add(this.txtDebugCSPDatabasePath);
            this.tabStripPageDebugSettings.Key = "TabStripPage";
            this.tabStripPageDebugSettings.Location = new System.Drawing.Point(0, 21);
            this.tabStripPageDebugSettings.Margin = new System.Windows.Forms.Padding(2);
            this.tabStripPageDebugSettings.Name = "tabStripPageDebugSettings";
            this.tabStripPageDebugSettings.Size = new System.Drawing.Size(521, 292);
            this.tabStripPageDebugSettings.TabIndex = 1;
            this.tabStripPageDebugSettings.Text = "Debug Settings";
            // 
            // buttonGenerationOutput
            // 
            this.buttonGenerationOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGenerationOutput.Image = ((System.Drawing.Image)(resources.GetObject("buttonGenerationOutput.Image")));
            this.buttonGenerationOutput.Location = new System.Drawing.Point(405, 59);
            this.buttonGenerationOutput.Name = "buttonGenerationOutput";
            this.buttonGenerationOutput.Size = new System.Drawing.Size(27, 23);
            this.buttonGenerationOutput.TabIndex = 12;
            this.buttonGenerationOutput.UseVisualStyleBackColor = true;
            this.buttonGenerationOutput.Click += new System.EventHandler(this.buttonGenerationOutput_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(2, 47);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(166, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Test Generation Output Directory:";
            // 
            // txtDebugGenerationOutputPath
            // 
            this.txtDebugGenerationOutputPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDebugGenerationOutputPath.Location = new System.Drawing.Point(4, 62);
            this.txtDebugGenerationOutputPath.Margin = new System.Windows.Forms.Padding(2);
            this.txtDebugGenerationOutputPath.Name = "txtDebugGenerationOutputPath";
            this.txtDebugGenerationOutputPath.Size = new System.Drawing.Size(396, 20);
            this.txtDebugGenerationOutputPath.TabIndex = 10;
            this.toolTip1.SetToolTip(this.txtDebugGenerationOutputPath, "This is the file that will be used to get data from when previewing a function");
            // 
            // buttonTemplateFileName
            // 
            this.buttonTemplateFileName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTemplateFileName.Image = ((System.Drawing.Image)(resources.GetObject("buttonTemplateFileName.Image")));
            this.buttonTemplateFileName.Location = new System.Drawing.Point(405, 22);
            this.buttonTemplateFileName.Name = "buttonTemplateFileName";
            this.buttonTemplateFileName.Size = new System.Drawing.Size(27, 23);
            this.buttonTemplateFileName.TabIndex = 9;
            this.buttonTemplateFileName.UseVisualStyleBackColor = true;
            this.buttonTemplateFileName.Click += new System.EventHandler(this.buttonTemplateFileName_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(2, 10);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "ArchAngel Project File:";
            // 
            // txtDebugCSPDatabasePath
            // 
            this.txtDebugCSPDatabasePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDebugCSPDatabasePath.Location = new System.Drawing.Point(4, 25);
            this.txtDebugCSPDatabasePath.Margin = new System.Windows.Forms.Padding(2);
            this.txtDebugCSPDatabasePath.Name = "txtDebugCSPDatabasePath";
            this.txtDebugCSPDatabasePath.Size = new System.Drawing.Size(396, 20);
            this.txtDebugCSPDatabasePath.TabIndex = 7;
            this.toolTip1.SetToolTip(this.txtDebugCSPDatabasePath, "This is the file that will be used to get data from when previewing a function");
            this.txtDebugCSPDatabasePath.TextChanged += new System.EventHandler(this.txtDebugCSPDatabasePath_TextChanged);
            // 
            // tabStripPagePrograms
            // 
            this.tabStripPagePrograms.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tabStripPagePrograms.Controls.Add(this.linkLabel1);
            this.tabStripPagePrograms.Controls.Add(this.label3);
            this.tabStripPagePrograms.Controls.Add(this.txtILMergeLocation);
            this.tabStripPagePrograms.Controls.Add(this.label1);
            this.tabStripPagePrograms.Controls.Add(this.btnILMergeLocation);
            this.tabStripPagePrograms.Key = "TabStripPage";
            this.tabStripPagePrograms.Location = new System.Drawing.Point(0, 21);
            this.tabStripPagePrograms.Name = "tabStripPagePrograms";
            this.tabStripPagePrograms.Size = new System.Drawing.Size(521, 292);
            this.tabStripPagePrograms.TabIndex = 2;
            this.tabStripPagePrograms.Text = "Programs";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(379, 50);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(85, 13);
            this.linkLabel1.TabIndex = 6;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "ILMerge website";
            this.toolTip1.SetToolTip(this.linkLabel1, "www.microsoft.com/downloads/details.aspx?FamilyID=22914587-b4ad-4eae-87cf-b14ae6a" +
                    "939b0&displaylang=en");
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(121, 37);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(313, 26);
            this.label3.TabIndex = 1;
            this.label3.Text = "ILMerge is not supplied with ArchAngel due to Microsoft licensing\r\nrestrictions. " +
                "To download it from Microsoft, click here:";
            // 
            // txtILMergeLocation
            // 
            this.txtILMergeLocation.Location = new System.Drawing.Point(124, 15);
            this.txtILMergeLocation.Margin = new System.Windows.Forms.Padding(2);
            this.txtILMergeLocation.Name = "txtILMergeLocation";
            this.txtILMergeLocation.Size = new System.Drawing.Size(276, 20);
            this.txtILMergeLocation.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Location of ILMerge";
            // 
            // btnILMergeLocation
            // 
            this.btnILMergeLocation.Location = new System.Drawing.Point(403, 15);
            this.btnILMergeLocation.Margin = new System.Windows.Forms.Padding(2);
            this.btnILMergeLocation.Name = "btnILMergeLocation";
            this.btnILMergeLocation.Size = new System.Drawing.Size(23, 19);
            this.btnILMergeLocation.TabIndex = 2;
            this.btnILMergeLocation.Text = "...";
            this.btnILMergeLocation.UseVisualStyleBackColor = true;
            this.btnILMergeLocation.Click += new System.EventHandler(this.btnILMergeLocation_Click);
            // 
            // tabStripPageGeneral
            // 
            this.tabStripPageGeneral.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tabStripPageGeneral.Controls.Add(this.btnColorBrowse);
            this.tabStripPageGeneral.Controls.Add(this.chkUseThemeColor);
            this.tabStripPageGeneral.Key = "TabStripPage";
            this.tabStripPageGeneral.Location = new System.Drawing.Point(0, 21);
            this.tabStripPageGeneral.Name = "tabStripPageGeneral";
            this.tabStripPageGeneral.Size = new System.Drawing.Size(521, 292);
            this.tabStripPageGeneral.TabIndex = 3;
            this.tabStripPageGeneral.Text = "General";
            // 
            // btnColorBrowse
            // 
            this.btnColorBrowse.Location = new System.Drawing.Point(132, 33);
            this.btnColorBrowse.Name = "btnColorBrowse";
            this.btnColorBrowse.Size = new System.Drawing.Size(111, 23);
            this.btnColorBrowse.TabIndex = 1;
            this.btnColorBrowse.Text = "Select Color...";
            this.btnColorBrowse.UseVisualStyleBackColor = true;
            this.btnColorBrowse.BackColorChanged += new System.EventHandler(this.btnColorBrowse_BackColorChanged);
            this.btnColorBrowse.Click += new System.EventHandler(this.btnColorBrowse_Click);
            // 
            // chkUseThemeColor
            // 
            this.chkUseThemeColor.AutoSize = true;
            this.chkUseThemeColor.Location = new System.Drawing.Point(12, 37);
            this.chkUseThemeColor.Name = "chkUseThemeColor";
            this.chkUseThemeColor.Size = new System.Drawing.Size(103, 17);
            this.chkUseThemeColor.TabIndex = 0;
            this.chkUseThemeColor.Text = "Use theme color";
            this.chkUseThemeColor.UseVisualStyleBackColor = true;
            this.chkUseThemeColor.CheckedChanged += new System.EventHandler(this.chkUseThemeColor_CheckedChanged);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(382, 325);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(66, 21);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "OK";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(452, 325);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(66, 21);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // ucHeading3
            // 
            this.ucHeading3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucHeading3.Location = new System.Drawing.Point(0, 313);
            this.ucHeading3.Margin = new System.Windows.Forms.Padding(2);
            this.ucHeading3.Name = "ucHeading3";
            this.ucHeading3.Size = new System.Drawing.Size(521, 34);
            this.ucHeading3.TabIndex = 18;
            this.ucHeading3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // frmOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(219)))), ((int)(((byte)(250)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(521, 347);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tabStrip1);
            this.Controls.Add(this.ucHeading3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(537, 386);
            this.MinimizeBox = false;
            this.Name = "frmOptions";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmOptions_Paint);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmOptions_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmOptions_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.tabStrip1)).EndInit();
            this.tabStrip1.ResumeLayout(false);
            this.tabStripPageFiles.ResumeLayout(false);
            this.tabStripPageFiles.PerformLayout();
            this.tabStripPageDebugSettings.ResumeLayout(false);
            this.tabStripPageDebugSettings.PerformLayout();
            this.tabStripPagePrograms.ResumeLayout(false);
            this.tabStripPagePrograms.PerformLayout();
            this.tabStripPageGeneral.ResumeLayout(false);
            this.tabStripPageGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ActiproSoftware.UIStudio.TabStrip.TabStrip tabStrip1;
		 private ActiproSoftware.UIStudio.TabStrip.TabStripPage tabStripPageFiles;
		 private System.Windows.Forms.CheckBox chkStartNewTemplate;
		 private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private ActiproSoftware.UIStudio.TabStrip.TabStripPage tabStripPageDebugSettings;
        private System.Windows.Forms.Button buttonTemplateFileName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDebugCSPDatabasePath;
        private Slyce.Common.Controls.ucHeading ucHeading3;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private ActiproSoftware.UIStudio.TabStrip.TabStripPage tabStripPagePrograms;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.TextBox txtILMergeLocation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnILMergeLocation;
        private ActiproSoftware.UIStudio.TabStrip.TabStripPage tabStripPageGeneral;
        private System.Windows.Forms.Button btnColorBrowse;
        private System.Windows.Forms.CheckBox chkUseThemeColor;
        private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button buttonGenerationOutput;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtDebugGenerationOutputPath;
    }
}