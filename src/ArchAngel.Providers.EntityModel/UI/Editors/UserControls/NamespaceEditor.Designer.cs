namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
    partial class NamespaceEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NamespaceEditor));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuEditNamespace = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDeleteNamespace = new System.Windows.Forms.ToolStripMenuItem();
            this.slyceGridEntities = new Slyce.Common.Controls.Diagramming.SlyceGrid.SlyceGrid();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditNamespace,
            this.mnuDeleteNamespace});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(108, 48);
            // 
            // mnuEditNamespace
            // 
            this.mnuEditNamespace.Image = ((System.Drawing.Image)(resources.GetObject("mnuEditNamespace.Image")));
            this.mnuEditNamespace.Name = "mnuEditNamespace";
            this.mnuEditNamespace.Size = new System.Drawing.Size(107, 22);
            this.mnuEditNamespace.Text = "Edit";
            this.mnuEditNamespace.Click += new System.EventHandler(this.mnuEditNamespace_Click);
            // 
            // mnuDeleteNamespace
            // 
            this.mnuDeleteNamespace.Image = ((System.Drawing.Image)(resources.GetObject("mnuDeleteNamespace.Image")));
            this.mnuDeleteNamespace.Name = "mnuDeleteNamespace";
            this.mnuDeleteNamespace.Size = new System.Drawing.Size(107, 22);
            this.mnuDeleteNamespace.Text = "Delete";
            this.mnuDeleteNamespace.Click += new System.EventHandler(this.mnuDeleteNamespace_Click);
            // 
            // slyceGridEntities
            // 
            this.slyceGridEntities.AllowUserToAddRows = true;
            this.slyceGridEntities.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.slyceGridEntities.DisabledColor = System.Drawing.Color.Gray;
            this.slyceGridEntities.FrozenColumnIndex = null;
            this.slyceGridEntities.InvalidColor = System.Drawing.Color.Orange;
            this.slyceGridEntities.Location = new System.Drawing.Point(18, 0);
            this.slyceGridEntities.Name = "slyceGridEntities";
            this.slyceGridEntities.Size = new System.Drawing.Size(575, 491);
            this.slyceGridEntities.TabIndex = 2;
            // 
            // NamespaceEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.slyceGridEntities);
            this.Name = "NamespaceEditor";
            this.Size = new System.Drawing.Size(593, 491);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Slyce.Common.Controls.Diagramming.SlyceGrid.SlyceGrid slyceGridEntities;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuDeleteNamespace;
        private System.Windows.Forms.ToolStripMenuItem mnuEditNamespace;

    }
}
