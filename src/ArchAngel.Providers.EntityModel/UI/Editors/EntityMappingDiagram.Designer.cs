namespace ArchAngel.Providers.EntityModel.UI.Editors
{
	partial class EntityMappingDiagram
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EntityMappingDiagram));
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mnuEditDiscriminator = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuRemoveDiscriminator = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuRemoveTable = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuRemoveReference = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditComponent = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuDeleteEntity = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuRemoveComponent = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditNearSide = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditFarSide = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuAddComponent = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuRefactorToExistingComponent = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuConvertToChild = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuMergeEntity = new System.Windows.Forms.ToolStripMenuItem();
			this.shapeCanvas1 = new Slyce.Common.Controls.Diagramming.Shapes.ShapeCanvas();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditDiscriminator,
            this.mnuRemoveDiscriminator,
            this.mnuRemoveTable,
            this.mnuRemoveReference,
            this.mnuEditComponent,
            this.mnuDeleteEntity,
            this.mnuRemoveComponent,
            this.mnuEditNearSide,
            this.mnuEditFarSide,
            this.mnuAddComponent,
            this.mnuRefactorToExistingComponent,
            this.mnuConvertToChild,
            this.mnuMergeEntity});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(254, 312);
			// 
			// mnuEditDiscriminator
			// 
			this.mnuEditDiscriminator.Image = ((System.Drawing.Image)(resources.GetObject("mnuEditDiscriminator.Image")));
			this.mnuEditDiscriminator.Name = "mnuEditDiscriminator";
			this.mnuEditDiscriminator.Size = new System.Drawing.Size(253, 22);
			this.mnuEditDiscriminator.Text = "Edit this discriminator";
			this.mnuEditDiscriminator.Click += new System.EventHandler(this.mnuEditDiscriminator_Click);
			// 
			// mnuRemoveDiscriminator
			// 
			this.mnuRemoveDiscriminator.Image = ((System.Drawing.Image)(resources.GetObject("mnuRemoveDiscriminator.Image")));
			this.mnuRemoveDiscriminator.Name = "mnuRemoveDiscriminator";
			this.mnuRemoveDiscriminator.Size = new System.Drawing.Size(253, 22);
			this.mnuRemoveDiscriminator.Text = "Remove this discriminator";
			this.mnuRemoveDiscriminator.Click += new System.EventHandler(this.mnuRemoveDiscriminator_Click);
			// 
			// mnuRemoveTable
			// 
			this.mnuRemoveTable.Image = ((System.Drawing.Image)(resources.GetObject("mnuRemoveTable.Image")));
			this.mnuRemoveTable.Name = "mnuRemoveTable";
			this.mnuRemoveTable.Size = new System.Drawing.Size(253, 22);
			this.mnuRemoveTable.Text = "Unmap this table";
			this.mnuRemoveTable.Click += new System.EventHandler(this.mnuRemoveTable_Click);
			// 
			// mnuRemoveReference
			// 
			this.mnuRemoveReference.Image = ((System.Drawing.Image)(resources.GetObject("mnuRemoveReference.Image")));
			this.mnuRemoveReference.Name = "mnuRemoveReference";
			this.mnuRemoveReference.Size = new System.Drawing.Size(253, 22);
			this.mnuRemoveReference.Text = "Remove this reference";
			this.mnuRemoveReference.Click += new System.EventHandler(this.mnuRemoveReference_Click);
			// 
			// mnuEditComponent
			// 
			this.mnuEditComponent.Image = ((System.Drawing.Image)(resources.GetObject("mnuEditComponent.Image")));
			this.mnuEditComponent.Name = "mnuEditComponent";
			this.mnuEditComponent.Size = new System.Drawing.Size(253, 22);
			this.mnuEditComponent.Text = "Edit component mapping";
			this.mnuEditComponent.Click += new System.EventHandler(this.mnuEditComponent_Click);
			// 
			// mnuDeleteEntity
			// 
			this.mnuDeleteEntity.Image = ((System.Drawing.Image)(resources.GetObject("mnuDeleteEntity.Image")));
			this.mnuDeleteEntity.Name = "mnuDeleteEntity";
			this.mnuDeleteEntity.Size = new System.Drawing.Size(253, 22);
			this.mnuDeleteEntity.Text = "Delete this entity";
			this.mnuDeleteEntity.Click += new System.EventHandler(this.mnuDeleteEntity_Click);
			// 
			// mnuRemoveComponent
			// 
			this.mnuRemoveComponent.Image = ((System.Drawing.Image)(resources.GetObject("mnuRemoveComponent.Image")));
			this.mnuRemoveComponent.Name = "mnuRemoveComponent";
			this.mnuRemoveComponent.Size = new System.Drawing.Size(253, 22);
			this.mnuRemoveComponent.Text = "Remove component";
			this.mnuRemoveComponent.Click += new System.EventHandler(this.mnuRemoveComponent_Click);
			// 
			// mnuEditNearSide
			// 
			this.mnuEditNearSide.Image = ((System.Drawing.Image)(resources.GetObject("mnuEditNearSide.Image")));
			this.mnuEditNearSide.Name = "mnuEditNearSide";
			this.mnuEditNearSide.Size = new System.Drawing.Size(253, 22);
			this.mnuEditNearSide.Text = "Edit this side";
			// 
			// mnuEditFarSide
			// 
			this.mnuEditFarSide.Image = ((System.Drawing.Image)(resources.GetObject("mnuEditFarSide.Image")));
			this.mnuEditFarSide.Name = "mnuEditFarSide";
			this.mnuEditFarSide.Size = new System.Drawing.Size(253, 22);
			this.mnuEditFarSide.Text = "Edit far side";
			// 
			// mnuAddComponent
			// 
			this.mnuAddComponent.Image = ((System.Drawing.Image)(resources.GetObject("mnuAddComponent.Image")));
			this.mnuAddComponent.Name = "mnuAddComponent";
			this.mnuAddComponent.Size = new System.Drawing.Size(253, 22);
			this.mnuAddComponent.Text = "Create new component";
			this.mnuAddComponent.Click += new System.EventHandler(this.mnuAddComponent_Click);
			// 
			// mnuRefactorToExistingComponent
			// 
			this.mnuRefactorToExistingComponent.Image = ((System.Drawing.Image)(resources.GetObject("mnuRefactorToExistingComponent.Image")));
			this.mnuRefactorToExistingComponent.Name = "mnuRefactorToExistingComponent";
			this.mnuRefactorToExistingComponent.Size = new System.Drawing.Size(253, 22);
			this.mnuRefactorToExistingComponent.Text = "Refactor to existing component";
			this.mnuRefactorToExistingComponent.Click += new System.EventHandler(this.mnuRefactorToExistingComponent_Click);
			// 
			// mnuConvertToChild
			// 
			this.mnuConvertToChild.Image = ((System.Drawing.Image)(resources.GetObject("mnuConvertToChild.Image")));
			this.mnuConvertToChild.Name = "mnuConvertToChild";
			this.mnuConvertToChild.Size = new System.Drawing.Size(253, 22);
			this.mnuConvertToChild.Text = "Convert to child of X (inheritance)";
			this.mnuConvertToChild.Click += new System.EventHandler(this.mnuConvertToChild_Click);
			// 
			// mnuMergeEntity
			// 
			this.mnuMergeEntity.Image = ((System.Drawing.Image)(resources.GetObject("mnuMergeEntity.Image")));
			this.mnuMergeEntity.Name = "mnuMergeEntity";
			this.mnuMergeEntity.Size = new System.Drawing.Size(253, 22);
			this.mnuMergeEntity.Text = "Merge into X";
			this.mnuMergeEntity.Click += new System.EventHandler(this.mnuMergeEntity_Click);
			// 
			// shapeCanvas1
			// 
			this.shapeCanvas1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.shapeCanvas1.AutoScroll = true;
			this.shapeCanvas1.KeepMainShapeFull = false;
			this.shapeCanvas1.Location = new System.Drawing.Point(0, 0);
			this.shapeCanvas1.Name = "shapeCanvas1";
			this.shapeCanvas1.Size = new System.Drawing.Size(587, 424);
			this.shapeCanvas1.TabIndex = 0;
			// 
			// EntityMappingDiagram
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.BackColor = System.Drawing.Color.Red;
			this.Controls.Add(this.shapeCanvas1);
			this.Name = "EntityMappingDiagram";
			this.Size = new System.Drawing.Size(587, 424);
			this.Scroll += new System.Windows.Forms.ScrollEventHandler(this.EntityMappingDiagram_Scroll);
			this.SizeChanged += new System.EventHandler(this.MappingEditor_SizeChanged);
			this.Resize += new System.EventHandler(this.MappingEditor_Resize);
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Slyce.Common.Controls.Diagramming.Shapes.ShapeCanvas shapeCanvas1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem mnuEditDiscriminator;
		private System.Windows.Forms.ToolStripMenuItem mnuRemoveDiscriminator;
		private System.Windows.Forms.ToolStripMenuItem mnuRemoveTable;
		private System.Windows.Forms.ToolStripMenuItem mnuRemoveReference;
		private System.Windows.Forms.ToolStripMenuItem mnuDeleteEntity;
		private System.Windows.Forms.ToolStripMenuItem mnuRemoveComponent;
		private System.Windows.Forms.ToolStripMenuItem mnuEditNearSide;
		private System.Windows.Forms.ToolStripMenuItem mnuEditFarSide;
		private System.Windows.Forms.ToolStripMenuItem mnuAddComponent;
		private System.Windows.Forms.ToolStripMenuItem mnuEditComponent;
		private System.Windows.Forms.ToolStripMenuItem mnuRefactorToExistingComponent;
		private System.Windows.Forms.ToolStripMenuItem mnuConvertToChild;
		private System.Windows.Forms.ToolStripMenuItem mnuMergeEntity;
	}
}
