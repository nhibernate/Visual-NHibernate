namespace ArchAngel.Workbench.FilterWizard
{
    partial class ucFilterWhere
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
            ActiproSoftware.SyntaxEditor.Document document2 = new ActiproSoftware.SyntaxEditor.Document();
            this.syntaxEditorCustomWhere = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
            this.checkBoxOverride = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // syntaxEditorCustomWhere
            // 
            this.syntaxEditorCustomWhere.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            document2.FooterText = null;
            document2.HeaderText = null;
            this.syntaxEditorCustomWhere.Document = document2;
            this.syntaxEditorCustomWhere.Enabled = false;
            this.syntaxEditorCustomWhere.Location = new System.Drawing.Point(6, 26);
            this.syntaxEditorCustomWhere.Name = "syntaxEditorCustomWhere";
            this.syntaxEditorCustomWhere.Size = new System.Drawing.Size(678, 294);
            this.syntaxEditorCustomWhere.TabIndex = 2;
            // 
            // checkBoxOverride
            // 
            this.checkBoxOverride.AutoSize = true;
            this.checkBoxOverride.Location = new System.Drawing.Point(3, 3);
            this.checkBoxOverride.Name = "checkBoxOverride";
            this.checkBoxOverride.Size = new System.Drawing.Size(378, 17);
            this.checkBoxOverride.TabIndex = 3;
            this.checkBoxOverride.Text = "Override the default SQL \'where\' clause with a custom SQL \'where\' clause:";
            this.checkBoxOverride.UseVisualStyleBackColor = true;
            this.checkBoxOverride.CheckedChanged += new System.EventHandler(this.checkBoxOverride_CheckedChanged);
            // 
            // ucFilterWhere
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBoxOverride);
            this.Controls.Add(this.syntaxEditorCustomWhere);
            this.Name = "ucFilterWhere";
            this.Size = new System.Drawing.Size(687, 323);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ActiproSoftware.SyntaxEditor.SyntaxEditor syntaxEditorCustomWhere;
        private System.Windows.Forms.CheckBox checkBoxOverride;
    }
}
