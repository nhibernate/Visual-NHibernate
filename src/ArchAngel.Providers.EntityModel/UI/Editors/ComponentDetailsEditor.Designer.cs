namespace ArchAngel.Providers.EntityModel.UI.Editors
{
    partial class ComponentDetailsEditor
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
            this.superTooltip1 = new DevComponents.DotNetBar.SuperTooltip();
            this.virtualPropertyGrid1 = new ArchAngel.Providers.EntityModel.UI.PropertyGrids.VirtualPropertyGrid();
            this.SuspendLayout();
            // 
            // superTooltip1
            // 
            this.superTooltip1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            // 
            // virtualPropertyGrid1
            // 
            this.virtualPropertyGrid1.Location = new System.Drawing.Point(30, 24);
            this.virtualPropertyGrid1.Name = "virtualPropertyGrid1";
            this.virtualPropertyGrid1.Size = new System.Drawing.Size(187, 109);
            this.virtualPropertyGrid1.TabIndex = 0;
            // 
            // ComponentDetailsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.virtualPropertyGrid1);
            this.Name = "ComponentDetailsEditor";
            this.Size = new System.Drawing.Size(613, 246);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.SuperTooltip superTooltip1;
        private ArchAngel.Providers.EntityModel.UI.PropertyGrids.VirtualPropertyGrid virtualPropertyGrid1;
    }
}
