namespace ArchAngel.Workbench.OptionsItems
{
    partial class Formatting
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
            this.tabStrip1 = new ActiproSoftware.UIStudio.TabStrip.TabStrip();
            this.tabStripPage1 = new ActiproSoftware.UIStudio.TabStrip.TabStripPage();
            ((System.ComponentModel.ISupportInitialize)(this.tabStrip1)).BeginInit();
            this.tabStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabStrip1
            // 
            this.tabStrip1.AutoSetFocusOnClick = true;
            this.tabStrip1.Controls.Add(this.tabStripPage1);
            this.tabStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabStrip1.Location = new System.Drawing.Point(0, 0);
            this.tabStrip1.Name = "tabStrip1";
            this.tabStrip1.Size = new System.Drawing.Size(791, 427);
            this.tabStrip1.TabIndex = 0;
            this.tabStrip1.Text = "tabStrip1";
            // 
            // tabStripPage1
            // 
            this.tabStripPage1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tabStripPage1.Key = "TabStripPage";
            this.tabStripPage1.Location = new System.Drawing.Point(0, 21);
            this.tabStripPage1.Name = "tabStripPage1";
            this.tabStripPage1.Size = new System.Drawing.Size(791, 406);
            this.tabStripPage1.TabIndex = 0;
            this.tabStripPage1.Text = "C#";
            // 
            // Formatting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabStrip1);
            this.Name = "Formatting";
            this.Size = new System.Drawing.Size(791, 427);
            ((System.ComponentModel.ISupportInitialize)(this.tabStrip1)).EndInit();
            this.tabStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ActiproSoftware.UIStudio.TabStrip.TabStrip tabStrip1;
        private ActiproSoftware.UIStudio.TabStrip.TabStripPage tabStripPage1;
    }
}
