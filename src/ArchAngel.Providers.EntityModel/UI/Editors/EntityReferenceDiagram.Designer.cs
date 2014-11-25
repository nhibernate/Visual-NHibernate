namespace ArchAngel.Providers.EntityModel.UI.Editors
{
    partial class EntityReferenceDiagram
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EntityReferenceDiagram));
            this.shapeCanvas1 = new Slyce.Common.Controls.Diagramming.Shapes.ShapeCanvas();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExpand = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // shapeCanvas1
            // 
            this.shapeCanvas1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.shapeCanvas1.AutoScroll = true;
            this.shapeCanvas1.Location = new System.Drawing.Point(0, 0);
            this.shapeCanvas1.Name = "shapeCanvas1";
            this.shapeCanvas1.Size = new System.Drawing.Size(281, 217);
            this.shapeCanvas1.TabIndex = 0;
            this.shapeCanvas1.Load += new System.EventHandler(this.shapeCanvas1_Load);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRemove,
            this.mnuExpand});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(161, 86);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // mnuRemove
            // 
            this.mnuRemove.Image = ((System.Drawing.Image)(resources.GetObject("mnuRemove.Image")));
            this.mnuRemove.Name = "mnuRemove";
            this.mnuRemove.Size = new System.Drawing.Size(160, 30);
            this.mnuRemove.Text = "Remove";
            this.mnuRemove.Click += new System.EventHandler(this.mnuRemove_Click);
            // 
            // mnuExpand
            // 
            this.mnuExpand.Image = ((System.Drawing.Image)(resources.GetObject("mnuExpand.Image")));
            this.mnuExpand.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuExpand.Name = "mnuExpand";
            this.mnuExpand.Size = new System.Drawing.Size(160, 30);
            this.mnuExpand.Text = "Stretch";
            this.mnuExpand.Click += new System.EventHandler(this.mnuExpand_Click);
            // 
            // EntityReferenceDiagram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.shapeCanvas1);
            this.Name = "EntityReferenceDiagram";
            this.Size = new System.Drawing.Size(281, 217);
            this.Resize += new System.EventHandler(this.ReferenceEditor_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Slyce.Common.Controls.Diagramming.Shapes.ShapeCanvas shapeCanvas1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuRemove;
        private System.Windows.Forms.ToolStripMenuItem mnuExpand;
    }
}
