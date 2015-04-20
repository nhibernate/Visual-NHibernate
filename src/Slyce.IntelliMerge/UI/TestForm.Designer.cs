using ucTextMergeEditor=Slyce.IntelliMerge.UI.Editors.ucTextMergeEditor;

namespace Slyce.IntelliMerge.UI
{
    partial class TestForm
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
            this.ucTextMergeEditor1 = new ucTextMergeEditor();
            this.SuspendLayout();
            // 
            // ucTextMergeEditor1
            // 
            this.ucTextMergeEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTextMergeEditor1.FileInformation = null;
            this.ucTextMergeEditor1.Location = new System.Drawing.Point(0, 0);
            this.ucTextMergeEditor1.Name = "ucTextMergeEditor1";
            this.ucTextMergeEditor1.Size = new System.Drawing.Size(740, 532);
            this.ucTextMergeEditor1.TabIndex = 0;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 532);
            this.Controls.Add(this.ucTextMergeEditor1);
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.ResumeLayout(false);

        }

        #endregion

        private ucTextMergeEditor ucTextMergeEditor1;

    }
}