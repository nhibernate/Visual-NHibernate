namespace ArchAngel.Designer
{
	partial class ucGenerationChoices
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucGenerationChoices));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.mnuTreeNode = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuItemAddFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemAddFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemAddStaticFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTreeNodeSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuItemEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListTreeNodes = new System.Windows.Forms.ImageList(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.treeFiles = new DevComponents.AdvTree.AdvTree();
            this.colFiles = new DevComponents.AdvTree.ColumnHeader();
            this.colFunction = new DevComponents.AdvTree.ColumnHeader();
            this.colIterate = new DevComponents.AdvTree.ColumnHeader();
            this.node1 = new DevComponents.AdvTree.Node();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.functionLinkStyle = new DevComponents.DotNetBar.ElementStyle();
            this.functionLinkHoverStyle = new DevComponents.DotNetBar.ElementStyle();
            this.colPreGenFunction = new DevComponents.AdvTree.ColumnHeader();
            this.mnuTreeNode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeFiles)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList1.Images.SetKeyName(0, "VSFolder_closed.bmp");
            this.imageList1.Images.SetKeyName(1, "VSFolder_open.bmp");
            this.imageList1.Images.SetKeyName(2, "VSProject_asp.bmp");
            this.imageList1.Images.SetKeyName(3, "OrgChartHS.png");
            this.imageList1.Images.SetKeyName(4, "VSProject_script.bmp");
            this.imageList1.Images.SetKeyName(5, "document.bmp");
            // 
            // mnuTreeNode
            // 
            this.mnuTreeNode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemAddFolder,
            this.mnuItemAddFile,
            this.mnuItemAddStaticFiles,
            this.mnuTreeNodeSeparator2,
            this.mnuItemEdit,
            this.mnuItemDelete});
            this.mnuTreeNode.Name = "mnuTabPage";
            this.mnuTreeNode.Size = new System.Drawing.Size(174, 120);
            this.mnuTreeNode.Opening += new System.ComponentModel.CancelEventHandler(this.mnuTreeNode_Opening);
            // 
            // mnuItemAddFolder
            // 
            this.mnuItemAddFolder.Image = ((System.Drawing.Image)(resources.GetObject("mnuItemAddFolder.Image")));
            this.mnuItemAddFolder.Name = "mnuItemAddFolder";
            this.mnuItemAddFolder.Size = new System.Drawing.Size(173, 22);
            this.mnuItemAddFolder.Text = "New Sub-F&older...";
            this.mnuItemAddFolder.ToolTipText = "Add a new child folder to this folder";
            this.mnuItemAddFolder.Click += new System.EventHandler(this.mnuItemAddFolder_Click);
            // 
            // mnuItemAddFile
            // 
            this.mnuItemAddFile.Image = ((System.Drawing.Image)(resources.GetObject("mnuItemAddFile.Image")));
            this.mnuItemAddFile.Name = "mnuItemAddFile";
            this.mnuItemAddFile.Size = new System.Drawing.Size(173, 22);
            this.mnuItemAddFile.Text = "New &File...";
            this.mnuItemAddFile.Click += new System.EventHandler(this.newAddFileToolStripMenuItem_Click);
            // 
            // mnuItemAddStaticFiles
            // 
            this.mnuItemAddStaticFiles.Image = global::ArchAngel.Designer.Properties.Resources.treenode_add_32;
            this.mnuItemAddStaticFiles.Name = "mnuItemAddStaticFiles";
            this.mnuItemAddStaticFiles.Size = new System.Drawing.Size(173, 22);
            this.mnuItemAddStaticFiles.Text = "New Static Files";
            this.mnuItemAddStaticFiles.Click += new System.EventHandler(this.newStaticFilesToolStripMenuItem_Click);
            // 
            // mnuTreeNodeSeparator2
            // 
            this.mnuTreeNodeSeparator2.Name = "mnuTreeNodeSeparator2";
            this.mnuTreeNodeSeparator2.Size = new System.Drawing.Size(170, 6);
            // 
            // mnuItemEdit
            // 
            this.mnuItemEdit.Image = ((System.Drawing.Image)(resources.GetObject("mnuItemEdit.Image")));
            this.mnuItemEdit.Name = "mnuItemEdit";
            this.mnuItemEdit.Size = new System.Drawing.Size(173, 22);
            this.mnuItemEdit.Text = "&Edit";
            this.mnuItemEdit.ToolTipText = "Edit this item";
            this.mnuItemEdit.Click += new System.EventHandler(this.mnuItemEdit_Click);
            // 
            // mnuItemDelete
            // 
            this.mnuItemDelete.Image = ((System.Drawing.Image)(resources.GetObject("mnuItemDelete.Image")));
            this.mnuItemDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuItemDelete.Name = "mnuItemDelete";
            this.mnuItemDelete.Size = new System.Drawing.Size(173, 22);
            this.mnuItemDelete.Text = "&Delete";
            this.mnuItemDelete.ToolTipText = "Delete this item";
            this.mnuItemDelete.Click += new System.EventHandler(this.deleteNodeToolStripMenuItem1_Click);
            // 
            // imageListTreeNodes
            // 
            this.imageListTreeNodes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTreeNodes.ImageStream")));
            this.imageListTreeNodes.TransparentColor = System.Drawing.Color.Magenta;
            this.imageListTreeNodes.Images.SetKeyName(0, "");
            this.imageListTreeNodes.Images.SetKeyName(1, "");
            this.imageListTreeNodes.Images.SetKeyName(2, "");
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // treeFiles
            // 
            this.treeFiles.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.treeFiles.AllowDrop = true;
            this.treeFiles.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.treeFiles.BackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.treeFiles.BackgroundStyle.BackColorGradientAngle = 90;
            this.treeFiles.BackgroundStyle.BackColorGradientType = DevComponents.DotNetBar.eGradientType.Radial;
            this.treeFiles.BackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.treeFiles.BackgroundStyle.Class = "TreeBorderKey";
            this.treeFiles.Columns.Add(this.colFiles);
            this.treeFiles.Columns.Add(this.colFunction);
            this.treeFiles.Columns.Add(this.colIterate);
            this.treeFiles.Columns.Add(this.colPreGenFunction);
            this.treeFiles.ContextMenuStrip = this.mnuTreeNode;
            this.treeFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeFiles.GridLinesColor = System.Drawing.Color.WhiteSmoke;
            this.treeFiles.GridRowLines = true;
            this.treeFiles.HotTracking = true;
            this.treeFiles.ImageList = this.imageList1;
            this.treeFiles.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.treeFiles.Location = new System.Drawing.Point(0, 0);
            this.treeFiles.Name = "treeFiles";
            this.treeFiles.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.node1});
            this.treeFiles.NodesConnector = this.nodeConnector1;
            this.treeFiles.NodeStyle = this.elementStyle1;
            this.treeFiles.PathSeparator = ";";
            this.treeFiles.Size = new System.Drawing.Size(907, 417);
            this.treeFiles.Styles.Add(this.elementStyle1);
            this.treeFiles.Styles.Add(this.functionLinkStyle);
            this.treeFiles.Styles.Add(this.functionLinkHoverStyle);
            this.treeFiles.TabIndex = 5;
            this.treeFiles.Text = "advTree1";
            this.treeFiles.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeFiles_MouseClick);
            this.treeFiles.AfterNodeDrop += new DevComponents.AdvTree.TreeDragDropEventHandler(this.treeFiles_AfterNodeDrop);
            this.treeFiles.DragLeave += new System.EventHandler(this.treeFiles_DragLeave);
            this.treeFiles.DragOver += new System.Windows.Forms.DragEventHandler(this.treeFiles_DragOver);
            this.treeFiles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeFiles_KeyDown);
            this.treeFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeFiles_DragDrop);
            this.treeFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeFiles_DragEnter);
            // 
            // colFiles
            // 
            this.colFiles.Name = "colFiles";
            this.colFiles.Text = "Files";
            this.colFiles.Width.Absolute = 300;
            // 
            // colFunction
            // 
            this.colFunction.Name = "colFunction";
            this.colFunction.Text = "Function";
            this.colFunction.Width.Absolute = 150;
            // 
            // colIterate
            // 
            this.colIterate.Name = "colIterate";
            this.colIterate.Text = "Iterate";
            this.colIterate.Width.Absolute = 150;
            // 
            // node1
            // 
            this.node1.Expanded = true;
            this.node1.Name = "node1";
            this.node1.Text = "node1";
            // 
            // nodeConnector1
            // 
            this.nodeConnector1.LineColor = System.Drawing.SystemColors.ControlText;
            // 
            // elementStyle1
            // 
            this.elementStyle1.Class = "";
            this.elementStyle1.Name = "elementStyle1";
            this.elementStyle1.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // functionLinkStyle
            // 
            this.functionLinkStyle.Class = "";
            this.functionLinkStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.functionLinkStyle.Name = "functionLinkStyle";
            this.functionLinkStyle.TextColor = System.Drawing.Color.Blue;
            // 
            // functionLinkHoverStyle
            // 
            this.functionLinkHoverStyle.Class = "";
            this.functionLinkHoverStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.functionLinkHoverStyle.Name = "functionLinkHoverStyle";
            this.functionLinkHoverStyle.TextColor = System.Drawing.Color.Blue;
            // 
            // colPreGenFunction
            // 
            this.colPreGenFunction.Name = "colPreGenFunction";
            this.colPreGenFunction.Text = "Pre-Generation function (static files)";
            this.colPreGenFunction.Width.Absolute = 150;
            // 
            // ucGenerationChoices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(223)))), ((int)(((byte)(251)))));
            this.Controls.Add(this.treeFiles);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ucGenerationChoices";
            this.Size = new System.Drawing.Size(907, 417);
            this.mnuTreeNode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeFiles)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.ContextMenuStrip mnuTreeNode;
		private System.Windows.Forms.ToolStripMenuItem mnuItemDelete;
        private System.Windows.Forms.ToolStripMenuItem mnuItemEdit;
        private System.Windows.Forms.ToolStripMenuItem mnuItemAddFolder;
        private System.Windows.Forms.ImageList imageListTreeNodes;
        private System.Windows.Forms.ToolStripSeparator mnuTreeNodeSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuItemAddFile;
        private DevComponents.AdvTree.AdvTree treeFiles;
        private DevComponents.AdvTree.ColumnHeader colFiles;
        private DevComponents.AdvTree.ColumnHeader colFunction;
        private DevComponents.AdvTree.ColumnHeader colIterate;
        private DevComponents.AdvTree.Node node1;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private DevComponents.DotNetBar.ElementStyle functionLinkStyle;
        private DevComponents.DotNetBar.ElementStyle functionLinkHoverStyle;
		private System.Windows.Forms.ToolStripMenuItem mnuItemAddStaticFiles;
        private DevComponents.AdvTree.ColumnHeader colPreGenFunction;
	}
}
