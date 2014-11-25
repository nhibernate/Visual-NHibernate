namespace ArchAngel.Designer.Wizards.OutputFileWizardScreens
{
    partial class Screen2
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
			this.lblHeadingFiles = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.panelStaticFile = new System.Windows.Forms.Panel();
			this.listStaticFiles = new System.Windows.Forms.ListBox();
			this.buttonAddStaticFile = new System.Windows.Forms.Button();
			this.panelScriptFile = new System.Windows.Forms.Panel();
			this.listTypes = new System.Windows.Forms.ListBox();
			this.panelStaticFile.SuspendLayout();
			this.panelScriptFile.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblHeadingFiles
			// 
			this.lblHeadingFiles.AutoSize = true;
			this.lblHeadingFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblHeadingFiles.Location = new System.Drawing.Point(12, 5);
			this.lblHeadingFiles.Name = "lblHeadingFiles";
			this.lblHeadingFiles.Size = new System.Drawing.Size(271, 13);
			this.lblHeadingFiles.TabIndex = 0;
			this.lblHeadingFiles.Text = "Create one of these files for every instance of:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(9, 5);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(133, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Include this static file:";
			// 
			// panelStaticFile
			// 
			this.panelStaticFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.panelStaticFile.Controls.Add(this.listStaticFiles);
			this.panelStaticFile.Controls.Add(this.buttonAddStaticFile);
			this.panelStaticFile.Controls.Add(this.label2);
			this.panelStaticFile.Location = new System.Drawing.Point(3, 303);
			this.panelStaticFile.Name = "panelStaticFile";
			this.panelStaticFile.Size = new System.Drawing.Size(891, 195);
			this.panelStaticFile.TabIndex = 8;
			// 
			// listStaticFiles
			// 
			this.listStaticFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.listStaticFiles.FormattingEnabled = true;
			this.listStaticFiles.Location = new System.Drawing.Point(12, 22);
			this.listStaticFiles.Name = "listStaticFiles";
			this.listStaticFiles.Size = new System.Drawing.Size(867, 160);
			this.listStaticFiles.TabIndex = 10;
			// 
			// buttonAddStaticFile
			// 
			this.buttonAddStaticFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonAddStaticFile.AutoSize = true;
			this.buttonAddStaticFile.Location = new System.Drawing.Point(720, 0);
			this.buttonAddStaticFile.Name = "buttonAddStaticFile";
			this.buttonAddStaticFile.Size = new System.Drawing.Size(159, 23);
			this.buttonAddStaticFile.TabIndex = 9;
			this.buttonAddStaticFile.Text = "Add another file to this list...";
			this.buttonAddStaticFile.UseVisualStyleBackColor = true;
			this.buttonAddStaticFile.Click += new System.EventHandler(this.buttonAddStaticFile_Click);
			// 
			// panelScriptFile
			// 
			this.panelScriptFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.panelScriptFile.Controls.Add(this.listTypes);
			this.panelScriptFile.Controls.Add(this.lblHeadingFiles);
			this.panelScriptFile.Location = new System.Drawing.Point(0, 143);
			this.panelScriptFile.Name = "panelScriptFile";
			this.panelScriptFile.Size = new System.Drawing.Size(894, 138);
			this.panelScriptFile.TabIndex = 9;
			// 
			// listTypes
			// 
			this.listTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.listTypes.FormattingEnabled = true;
			this.listTypes.Location = new System.Drawing.Point(15, 22);
			this.listTypes.Name = "listTypes";
			this.listTypes.Size = new System.Drawing.Size(867, 108);
			this.listTypes.TabIndex = 1;
			// 
			// Screen2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panelScriptFile);
			this.Controls.Add(this.panelStaticFile);
			this.Name = "Screen2";
			this.Size = new System.Drawing.Size(894, 577);
			this.Resize += new System.EventHandler(this.Screen2_Resize);
			this.panelStaticFile.ResumeLayout(false);
			this.panelStaticFile.PerformLayout();
			this.panelScriptFile.ResumeLayout(false);
			this.panelScriptFile.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblHeadingFiles;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelStaticFile;
        private System.Windows.Forms.Panel panelScriptFile;
		private System.Windows.Forms.Button buttonAddStaticFile;
		private System.Windows.Forms.ListBox listTypes;
		private System.Windows.Forms.ListBox listStaticFiles;
    }
}
