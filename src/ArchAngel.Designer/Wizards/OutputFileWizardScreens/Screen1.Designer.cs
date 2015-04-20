namespace ArchAngel.Designer.Wizards.OutputFileWizardScreens
{
    partial class Screen1
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
            this.label1 = new System.Windows.Forms.Label();
            this.optScriptFile = new System.Windows.Forms.RadioButton();
            this.optBinaryFile = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(737, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "Do you want to add a static file, such as a JPG, GIF, Word Document etc, or do yo" +
                "u want to add a file that is going to get generated at runtime, getting it\'s tex" +
                "t populated by a function?";
            // 
            // optScriptFile
            // 
            this.optScriptFile.AutoSize = true;
            this.optScriptFile.Checked = true;
            this.optScriptFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optScriptFile.Location = new System.Drawing.Point(54, 74);
            this.optScriptFile.Name = "optScriptFile";
            this.optScriptFile.Size = new System.Drawing.Size(105, 17);
            this.optScriptFile.TabIndex = 1;
            this.optScriptFile.TabStop = true;
            this.optScriptFile.Text = "Generated file";
            this.optScriptFile.UseVisualStyleBackColor = true;
            // 
            // optBinaryFile
            // 
            this.optBinaryFile.AutoSize = true;
            this.optBinaryFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optBinaryFile.Location = new System.Drawing.Point(54, 97);
            this.optBinaryFile.Name = "optBinaryFile";
            this.optBinaryFile.Size = new System.Drawing.Size(79, 17);
            this.optBinaryFile.TabIndex = 2;
            this.optBinaryFile.Text = "Static file";
            this.optBinaryFile.UseVisualStyleBackColor = true;
            // 
            // Screen1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.optBinaryFile);
            this.Controls.Add(this.optScriptFile);
            this.Controls.Add(this.label1);
            this.Name = "Screen1";
            this.Size = new System.Drawing.Size(775, 541);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton optScriptFile;
        private System.Windows.Forms.RadioButton optBinaryFile;
    }
}
