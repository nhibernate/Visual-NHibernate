namespace ArchAngel.Providers.EntityModel.UI.Editors
{
	partial class TableMappingDiagram
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TableMappingDiagram));
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mnuRemoveTable = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuUnmapEntity = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuDeleteRelationship = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuCreateTPHInheritance = new System.Windows.Forms.ToolStripMenuItem();
			this.shapeCanvas1 = new Slyce.Common.Controls.Diagramming.Shapes.ShapeCanvas();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRemoveTable,
            this.mnuUnmapEntity,
            this.mnuDeleteRelationship,
            this.mnuCreateTPHInheritance});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(206, 114);
			// 
			// mnuRemoveTable
			// 
			this.mnuRemoveTable.Image = ((System.Drawing.Image)(resources.GetObject("mnuRemoveTable.Image")));
			this.mnuRemoveTable.Name = "mnuRemoveTable";
			this.mnuRemoveTable.Size = new System.Drawing.Size(205, 22);
			this.mnuRemoveTable.Text = "Remove table";
			this.mnuRemoveTable.Click += new System.EventHandler(this.mnuRemoveTable_Click);
			// 
			// mnuUnmapEntity
			// 
			this.mnuUnmapEntity.Image = ((System.Drawing.Image)(resources.GetObject("mnuUnmapEntity.Image")));
			this.mnuUnmapEntity.Name = "mnuUnmapEntity";
			this.mnuUnmapEntity.Size = new System.Drawing.Size(205, 22);
			this.mnuUnmapEntity.Text = "Unmap entity";
			this.mnuUnmapEntity.Click += new System.EventHandler(this.mnuUnmapEntity_Click);
			// 
			// mnuDeleteRelationship
			// 
			this.mnuDeleteRelationship.Image = ((System.Drawing.Image)(resources.GetObject("mnuDeleteRelationship.Image")));
			this.mnuDeleteRelationship.Name = "mnuDeleteRelationship";
			this.mnuDeleteRelationship.Size = new System.Drawing.Size(205, 22);
			this.mnuDeleteRelationship.Text = "Delete relationship";
			this.mnuDeleteRelationship.Click += new System.EventHandler(this.mnuDeleteRelationship_Click);
			// 
			// mnuCreateTPHInheritance
			// 
			this.mnuCreateTPHInheritance.Image = ((System.Drawing.Image)(resources.GetObject("mnuCreateTPHInheritance.Image")));
			this.mnuCreateTPHInheritance.Name = "mnuCreateTPHInheritance";
			this.mnuCreateTPHInheritance.Size = new System.Drawing.Size(205, 22);
			this.mnuCreateTPHInheritance.Text = "Create TPH inheritance...";
			this.mnuCreateTPHInheritance.Click += new System.EventHandler(this.mnuCreateTPHInheritance_Click);
			// 
			// shapeCanvas1
			// 
			this.shapeCanvas1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.shapeCanvas1.AutoScroll = true;
			this.shapeCanvas1.KeepMainShapeFull = true;
			this.shapeCanvas1.Location = new System.Drawing.Point(0, 0);
			this.shapeCanvas1.Name = "shapeCanvas1";
			this.shapeCanvas1.Size = new System.Drawing.Size(236, 141);
			this.shapeCanvas1.TabIndex = 1;
			// 
			// TableMappingDiagram
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.Controls.Add(this.shapeCanvas1);
			this.Name = "TableMappingDiagram";
			this.Size = new System.Drawing.Size(236, 141);
			this.Scroll += new System.Windows.Forms.ScrollEventHandler(this.TableMappingDiagram_Scroll);
			this.Resize += new System.EventHandler(this.TableMappingDiagram_Resize);
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Slyce.Common.Controls.Diagramming.Shapes.ShapeCanvas shapeCanvas1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem mnuRemoveTable;
		private System.Windows.Forms.ToolStripMenuItem mnuUnmapEntity;
		private System.Windows.Forms.ToolStripMenuItem mnuDeleteRelationship;
		private System.Windows.Forms.ToolStripMenuItem mnuCreateTPHInheritance;
	}
}
