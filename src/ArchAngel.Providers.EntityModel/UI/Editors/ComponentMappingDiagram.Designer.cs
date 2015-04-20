namespace ArchAngel.Providers.EntityModel.UI.Editors
{
    partial class ComponentMappingDiagram
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
            this.shapeCanvas1 = new Slyce.Common.Controls.Diagramming.Shapes.ShapeCanvas();
            this.SuspendLayout();
            // 
            // shapeCanvas1
            // 
            this.shapeCanvas1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.shapeCanvas1.AutoScroll = true;
            this.shapeCanvas1.KeepMainShapeFull = false;
            this.shapeCanvas1.Location = new System.Drawing.Point(0, 0);
            this.shapeCanvas1.Name = "shapeCanvas1";
            this.shapeCanvas1.Size = new System.Drawing.Size(258, 472);
            this.shapeCanvas1.TabIndex = 1;
            // 
            // ComponentMappingDiagram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.shapeCanvas1);
            this.Name = "ComponentMappingDiagram";
            this.Size = new System.Drawing.Size(258, 193);
            this.ResumeLayout(false);

        }

        #endregion

        private Slyce.Common.Controls.Diagramming.Shapes.ShapeCanvas shapeCanvas1;
    }
}
