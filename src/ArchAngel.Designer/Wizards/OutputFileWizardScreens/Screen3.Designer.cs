namespace ArchAngel.Designer.Wizards.OutputFileWizardScreens
{
    partial class Screen3
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
            ActiproSoftware.SyntaxEditor.Document document1 = new ActiproSoftware.SyntaxEditor.Document();
            this.lblExample = new System.Windows.Forms.Label();
            this.lblHeading = new System.Windows.Forms.Label();
            this.syntaxEditorFilename = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
            this.SuspendLayout();
            // 
            // lblExample
            // 
            this.lblExample.AutoSize = true;
            this.lblExample.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExample.Location = new System.Drawing.Point(37, 49);
            this.lblExample.Name = "lblExample";
            this.lblExample.Size = new System.Drawing.Size(182, 13);
            this.lblExample.TabIndex = 3;
            this.lblExample.Text = "eg: My#iterator.Name#Data.txt";
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.Location = new System.Drawing.Point(16, 10);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(113, 13);
            this.lblHeading.TabIndex = 5;
            this.lblHeading.Text = "Filename template:";
            // 
            // syntaxEditorFilename
            // 
            this.syntaxEditorFilename.AcceptsTab = false;
            document1.Multiline = false;
            this.syntaxEditorFilename.Document = document1;
            this.syntaxEditorFilename.HideSelection = true;
            this.syntaxEditorFilename.IndicatorMarginVisible = false;
            this.syntaxEditorFilename.Location = new System.Drawing.Point(19, 24);
            this.syntaxEditorFilename.Name = "syntaxEditorFilename";
            this.syntaxEditorFilename.SelectionMarginWidth = 0;
            this.syntaxEditorFilename.Size = new System.Drawing.Size(458, 22);
            this.syntaxEditorFilename.TabIndex = 6;
            this.syntaxEditorFilename.KeyUp += new System.Windows.Forms.KeyEventHandler(this.syntaxEditorFilename_KeyUp);
            this.syntaxEditorFilename.TriggerActivated += new ActiproSoftware.SyntaxEditor.TriggerEventHandler(this.syntaxEditorFilename_TriggerActivated);
            this.syntaxEditorFilename.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.syntaxEditorFilename_KeyPress);
            // 
            // Screen3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.syntaxEditorFilename);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.lblExample);
            this.Name = "Screen3";
            this.Size = new System.Drawing.Size(737, 451);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblExample;
        private System.Windows.Forms.Label lblHeading;
        private ActiproSoftware.SyntaxEditor.SyntaxEditor syntaxEditorFilename;
    }
}
