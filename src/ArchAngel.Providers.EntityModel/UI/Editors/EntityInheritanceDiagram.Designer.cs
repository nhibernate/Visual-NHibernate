namespace ArchAngel.Providers.EntityModel.UI.Editors
{
	partial class EntityInheritanceDiagram
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EntityInheritanceDiagram));
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mnuRemove = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuCreateInheritanceHierarchy = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSetDiscriminatorForSubClass = new System.Windows.Forms.ToolStripMenuItem();
			this.shapeCanvasInheritance = new Slyce.Common.Controls.Diagramming.Shapes.ShapeCanvas();
			this.superTooltip1 = new DevComponents.DotNetBar.SuperTooltip();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRemove,
            this.mnuCreateInheritanceHierarchy,
            this.mnuSetDiscriminatorForSubClass});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(232, 92);
			this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
			// 
			// mnuRemove
			// 
			this.mnuRemove.Image = ((System.Drawing.Image)(resources.GetObject("mnuRemove.Image")));
			this.mnuRemove.Name = "mnuRemove";
			this.mnuRemove.Size = new System.Drawing.Size(231, 22);
			this.mnuRemove.Text = "Remove";
			this.mnuRemove.Click += new System.EventHandler(this.mnuRemove_Click);
			// 
			// mnuCreateInheritanceHierarchy
			// 
			this.mnuCreateInheritanceHierarchy.Image = ((System.Drawing.Image)(resources.GetObject("mnuCreateInheritanceHierarchy.Image")));
			this.mnuCreateInheritanceHierarchy.Name = "mnuCreateInheritanceHierarchy";
			this.mnuCreateInheritanceHierarchy.Size = new System.Drawing.Size(231, 22);
			this.mnuCreateInheritanceHierarchy.Text = "Manage inheritance hierarchy";
			this.mnuCreateInheritanceHierarchy.ToolTipText = "Create/manage an inheritance hierachy from the single mapped table (Table Per Cla" +
				"ss Hierarchy)";
			this.mnuCreateInheritanceHierarchy.Click += new System.EventHandler(this.mnuCreateInheritanceHierarchy_Click);
			// 
			// mnuSetDiscriminatorForSubClass
			// 
			this.mnuSetDiscriminatorForSubClass.Name = "mnuSetDiscriminatorForSubClass";
			this.mnuSetDiscriminatorForSubClass.Size = new System.Drawing.Size(231, 22);
			this.mnuSetDiscriminatorForSubClass.Text = "Set discriminator";
			this.mnuSetDiscriminatorForSubClass.Click += new System.EventHandler(this.mnuSetDiscriminatorForSubClass_Click);
			// 
			// shapeCanvasInheritance
			// 
			this.shapeCanvasInheritance.AutoScroll = true;
			this.shapeCanvasInheritance.KeepMainShapeFull = true;
			this.shapeCanvasInheritance.Location = new System.Drawing.Point(0, 0);
			this.shapeCanvasInheritance.Name = "shapeCanvasInheritance";
			this.shapeCanvasInheritance.Size = new System.Drawing.Size(680, 336);
			this.shapeCanvasInheritance.TabIndex = 0;
			// 
			// superTooltip1
			// 
			this.superTooltip1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "tick_32.png");
			this.imageList1.Images.SetKeyName(1, "stop_24.png");
			// 
			// EntityInheritanceDiagram
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.Controls.Add(this.shapeCanvasInheritance);
			this.Name = "EntityInheritanceDiagram";
			this.Size = new System.Drawing.Size(680, 336);
			this.Scroll += new System.Windows.Forms.ScrollEventHandler(this.EntityInheritanceDiagram_Scroll);
			this.Resize += new System.EventHandler(this.EntityInheritanceDiagram_Resize);
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Slyce.Common.Controls.Diagramming.Shapes.ShapeCanvas shapeCanvasInheritance;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem mnuRemove;
		private System.Windows.Forms.ToolStripMenuItem mnuCreateInheritanceHierarchy;
		private DevComponents.DotNetBar.SuperTooltip superTooltip1;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ToolStripMenuItem mnuSetDiscriminatorForSubClass;
	}
}
