namespace ArchAngel.Designer
{
    partial class frmExtractTemplate
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExtractTemplate));
			this.buttonBrowseCompiledFile = new System.Windows.Forms.Button();
			this.txtCompiledFile = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.ucHeading1 = new Slyce.Common.Controls.ucHeading();
			this.buttonExtract = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// buttonBrowseCompiledFile
			// 
			this.buttonBrowseCompiledFile.Image = ((System.Drawing.Image)(resources.GetObject("buttonBrowseCompiledFile.Image")));
			this.buttonBrowseCompiledFile.Location = new System.Drawing.Point(529, 47);
			this.buttonBrowseCompiledFile.Name = "buttonBrowseCompiledFile";
			this.buttonBrowseCompiledFile.Size = new System.Drawing.Size(25, 25);
			this.buttonBrowseCompiledFile.TabIndex = 0;
			this.buttonBrowseCompiledFile.UseVisualStyleBackColor = true;
			this.buttonBrowseCompiledFile.Click += new System.EventHandler(this.buttonBrowseCompiledFile_Click);
			// 
			// txtCompiledFile
			// 
			this.txtCompiledFile.Location = new System.Drawing.Point(163, 50);
			this.txtCompiledFile.Name = "txtCompiledFile";
			this.txtCompiledFile.Size = new System.Drawing.Size(360, 20);
			this.txtCompiledFile.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 53);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(145, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Compiled template file (*.AAT.DLL)";
			// 
			// ucHeading1
			// 
			this.ucHeading1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ucHeading1.Location = new System.Drawing.Point(0, 124);
			this.ucHeading1.Name = "ucHeading1";
			this.ucHeading1.Size = new System.Drawing.Size(578, 31);
			this.ucHeading1.TabIndex = 3;
			this.ucHeading1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// buttonExtract
			// 
			this.buttonExtract.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonExtract.Location = new System.Drawing.Point(410, 128);
			this.buttonExtract.Name = "buttonExtract";
			this.buttonExtract.Size = new System.Drawing.Size(75, 23);
			this.buttonExtract.TabIndex = 4;
			this.buttonExtract.Text = "Extract";
			this.buttonExtract.UseVisualStyleBackColor = true;
			this.buttonExtract.Click += new System.EventHandler(this.buttonExtract_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(491, 128);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 5;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(375, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Extract a editable ArchAngel template file from a compiled template file (*.AAT.DLL)." +
				"";
			// 
			// frmExtractTemplate
			// 
			this.AcceptButton = this.buttonExtract;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(578, 155);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonExtract);
			this.Controls.Add(this.ucHeading1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtCompiledFile);
			this.Controls.Add(this.buttonBrowseCompiledFile);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmExtractTemplate";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Extract Template";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmExtractTemplate_FormClosed);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonBrowseCompiledFile;
        private System.Windows.Forms.TextBox txtCompiledFile;
        private System.Windows.Forms.Label label1;
        private Slyce.Common.Controls.ucHeading ucHeading1;
        private System.Windows.Forms.Button buttonExtract;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label2;
    }
}