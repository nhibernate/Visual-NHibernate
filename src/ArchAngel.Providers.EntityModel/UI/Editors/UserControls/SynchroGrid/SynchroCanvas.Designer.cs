namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls.SynchroGrid
{
    partial class SynchroCanvas
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
            this.SuspendLayout();
            // 
            // SynchroCanvas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Name = "SynchroCanvas";
            this.Size = new System.Drawing.Size(548, 339);
            this.MouseLeave += new System.EventHandler(this.SynchroCanvas_MouseLeave);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SynchroCanvas_Paint);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SynchroCanvas_MouseMove);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SynchroCanvas_MouseDown);
            this.SizeChanged += new System.EventHandler(this.SynchroCanvas_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
