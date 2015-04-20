namespace ArchAngel.Workbench.ContentItems
{
	partial class Output
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
			System.Windows.Forms.Panel panelGeneratedFilesPanelContainer;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Output));
			this.buttonDeselectAll = new DevComponents.DotNetBar.ButtonX();
			this.buttonSelectAll = new DevComponents.DotNetBar.ButtonX();
			this.buttonWriteFileToDisk = new DevComponents.DotNetBar.ButtonX();
			this.panelGeneratedFiles = new System.Windows.Forms.Panel();
			this.labelErrors = new DevComponents.DotNetBar.LabelX();
			this.labelChangedFiles = new DevComponents.DotNetBar.LabelX();
			this.labelUnchangedFiles = new DevComponents.DotNetBar.LabelX();
			this.labelNewFiles = new DevComponents.DotNetBar.LabelX();
			this.panelGenerationErrors = new DevComponents.DotNetBar.PanelEx();
			this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
			this.treeListDuplicatedFiles = new DevComponents.AdvTree.AdvTree();
			this.node1 = new DevComponents.AdvTree.Node();
			this.nodeConnector2 = new DevComponents.AdvTree.NodeConnector();
			this.elementStyle2 = new DevComponents.DotNetBar.ElementStyle();
			this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
			this.labelX1 = new DevComponents.DotNetBar.LabelX();
			this.treeList1 = new DevComponents.AdvTree.AdvTree();
			this.columnHeader1 = new DevComponents.AdvTree.ColumnHeader();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemViewFileDefault = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemViewfileInExplorer = new System.Windows.Forms.ToolStripMenuItem();
			this.imageListNodes = new System.Windows.Forms.ImageList(this.components);
			this.node2 = new DevComponents.AdvTree.Node();
			this.elementStyle3 = new DevComponents.DotNetBar.ElementStyle();
			this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
			this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
			this.labelNumGeneratedFiles = new System.Windows.Forms.Label();
			this.imageListStatuses = new System.Windows.Forms.ImageList(this.components);
			this.dockManager1 = new ActiproSoftware.UIStudio.Dock.DockManager(this.components);
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.panel2 = new System.Windows.Forms.Panel();
			this.superTooltip1 = new DevComponents.DotNetBar.SuperTooltip();
			this.buttonRefresh = new DevComponents.DotNetBar.ButtonX();
			this.imageListFileActions = new System.Windows.Forms.ImageList(this.components);
			this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
			this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
			this.expandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
			this.panelEditor = new System.Windows.Forms.Panel();
			this.labelX2 = new DevComponents.DotNetBar.LabelX();
			this.checkBoxPerformAnalysis = new System.Windows.Forms.CheckBox();
			panelGeneratedFilesPanelContainer = new System.Windows.Forms.Panel();
			panelGeneratedFilesPanelContainer.SuspendLayout();
			this.panelGeneratedFiles.SuspendLayout();
			this.panelGenerationErrors.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.treeListDuplicatedFiles)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
			this.contextMenuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelGeneratedFilesPanelContainer
			// 
			panelGeneratedFilesPanelContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			panelGeneratedFilesPanelContainer.Controls.Add(this.buttonDeselectAll);
			panelGeneratedFilesPanelContainer.Controls.Add(this.buttonSelectAll);
			panelGeneratedFilesPanelContainer.Controls.Add(this.buttonWriteFileToDisk);
			panelGeneratedFilesPanelContainer.Controls.Add(this.panelGeneratedFiles);
			panelGeneratedFilesPanelContainer.Controls.Add(this.labelNumGeneratedFiles);
			panelGeneratedFilesPanelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			panelGeneratedFilesPanelContainer.Location = new System.Drawing.Point(0, 0);
			panelGeneratedFilesPanelContainer.Name = "panelGeneratedFilesPanelContainer";
			panelGeneratedFilesPanelContainer.Size = new System.Drawing.Size(510, 475);
			panelGeneratedFilesPanelContainer.TabIndex = 62;
			// 
			// buttonDeselectAll
			// 
			this.buttonDeselectAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonDeselectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonDeselectAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonDeselectAll.Location = new System.Drawing.Point(89, 431);
			this.buttonDeselectAll.Name = "buttonDeselectAll";
			this.buttonDeselectAll.Size = new System.Drawing.Size(75, 23);
			this.buttonDeselectAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonDeselectAll.TabIndex = 69;
			this.buttonDeselectAll.Text = "Deselect all";
			this.buttonDeselectAll.Click += new System.EventHandler(this.buttonDeselectAll_Click);
			// 
			// buttonSelectAll
			// 
			this.buttonSelectAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonSelectAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonSelectAll.Location = new System.Drawing.Point(8, 431);
			this.buttonSelectAll.Name = "buttonSelectAll";
			this.buttonSelectAll.Size = new System.Drawing.Size(75, 23);
			this.buttonSelectAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonSelectAll.TabIndex = 68;
			this.buttonSelectAll.Text = "Select all";
			this.buttonSelectAll.Click += new System.EventHandler(this.buttonSelectAll_Click);
			// 
			// buttonWriteFileToDisk
			// 
			this.buttonWriteFileToDisk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonWriteFileToDisk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonWriteFileToDisk.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonWriteFileToDisk.DisabledImage = ((System.Drawing.Image)(resources.GetObject("buttonWriteFileToDisk.DisabledImage")));
			this.buttonWriteFileToDisk.HoverImage = ((System.Drawing.Image)(resources.GetObject("buttonWriteFileToDisk.HoverImage")));
			this.buttonWriteFileToDisk.Image = ((System.Drawing.Image)(resources.GetObject("buttonWriteFileToDisk.Image")));
			this.buttonWriteFileToDisk.Location = new System.Drawing.Point(368, 431);
			this.buttonWriteFileToDisk.Name = "buttonWriteFileToDisk";
			this.buttonWriteFileToDisk.Size = new System.Drawing.Size(133, 33);
			this.buttonWriteFileToDisk.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.superTooltip1.SetSuperTooltip(this.buttonWriteFileToDisk, new DevComponents.DotNetBar.SuperTooltipInfo("Write files to disk", "<b>Note:</b> No files are written to disk until you click this button.", "Write the selected files to disk, overwriting any existing ones.", ((System.Drawing.Image)(resources.GetObject("buttonWriteFileToDisk.SuperTooltip"))), ((System.Drawing.Image)(resources.GetObject("buttonWriteFileToDisk.SuperTooltip1"))), DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonWriteFileToDisk.TabIndex = 67;
			this.buttonWriteFileToDisk.Text = "Write files to disk";
			this.buttonWriteFileToDisk.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonWriteFileToDisk.Click += new System.EventHandler(this.buttonWriteFileToDisk_Click);
			// 
			// panelGeneratedFiles
			// 
			this.panelGeneratedFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.panelGeneratedFiles.Controls.Add(this.labelErrors);
			this.panelGeneratedFiles.Controls.Add(this.labelChangedFiles);
			this.panelGeneratedFiles.Controls.Add(this.labelUnchangedFiles);
			this.panelGeneratedFiles.Controls.Add(this.labelNewFiles);
			this.panelGeneratedFiles.Controls.Add(this.panelGenerationErrors);
			this.panelGeneratedFiles.Controls.Add(this.treeList1);
			this.panelGeneratedFiles.Location = new System.Drawing.Point(0, 0);
			this.panelGeneratedFiles.Name = "panelGeneratedFiles";
			this.panelGeneratedFiles.Size = new System.Drawing.Size(508, 425);
			this.panelGeneratedFiles.TabIndex = 7;
			this.panelGeneratedFiles.Resize += new System.EventHandler(this.panelGeneratedFiles_Resize);
			// 
			// labelErrors
			// 
			this.labelErrors.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			// 
			// 
			// 
			this.labelErrors.BackgroundStyle.BackColorGradientAngle = 90;
			this.labelErrors.BackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.labelErrors.BackgroundStyle.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBorder;
			this.labelErrors.BackgroundStyle.Class = "";
			this.labelErrors.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelErrors.BackgroundStyle.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
			this.labelErrors.ForeColor = System.Drawing.Color.White;
			this.labelErrors.Image = ((System.Drawing.Image)(resources.GetObject("labelErrors.Image")));
			this.labelErrors.Location = new System.Drawing.Point(293, 18);
			this.labelErrors.Name = "labelErrors";
			this.labelErrors.Size = new System.Drawing.Size(96, 23);
			this.superTooltip1.SetSuperTooltip(this.labelErrors, new DevComponents.DotNetBar.SuperTooltipInfo("x files with errors", "", "Click to show/hide files that were not successfully generated.", ((System.Drawing.Image)(resources.GetObject("labelErrors.SuperTooltip"))), null, DevComponents.DotNetBar.eTooltipColor.Gray, true, false, new System.Drawing.Size(0, 0)));
			this.labelErrors.TabIndex = 72;
			this.labelErrors.Text = " 0 Errors";
			this.labelErrors.Click += new System.EventHandler(this.labelErrors_Click_1);
			// 
			// labelChangedFiles
			// 
			this.labelChangedFiles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			// 
			// 
			// 
			this.labelChangedFiles.BackgroundStyle.BackColorGradientAngle = 90;
			this.labelChangedFiles.BackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.labelChangedFiles.BackgroundStyle.Class = "";
			this.labelChangedFiles.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelChangedFiles.BackgroundStyle.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
			this.labelChangedFiles.ForeColor = System.Drawing.Color.White;
			this.labelChangedFiles.Image = ((System.Drawing.Image)(resources.GetObject("labelChangedFiles.Image")));
			this.labelChangedFiles.Location = new System.Drawing.Point(191, 18);
			this.labelChangedFiles.Name = "labelChangedFiles";
			this.labelChangedFiles.Size = new System.Drawing.Size(96, 23);
			this.superTooltip1.SetSuperTooltip(this.labelChangedFiles, new DevComponents.DotNetBar.SuperTooltipInfo("x changed files", "", "Click to show/hide files that have changes compared to files on disk.", ((System.Drawing.Image)(resources.GetObject("labelChangedFiles.SuperTooltip"))), null, DevComponents.DotNetBar.eTooltipColor.Gray, true, false, new System.Drawing.Size(0, 0)));
			this.labelChangedFiles.TabIndex = 71;
			this.labelChangedFiles.Text = " 0 Changed";
			this.labelChangedFiles.Click += new System.EventHandler(this.labelChangedFiles_Click);
			// 
			// labelUnchangedFiles
			// 
			this.labelUnchangedFiles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			// 
			// 
			// 
			this.labelUnchangedFiles.BackgroundStyle.BackColorGradientAngle = 90;
			this.labelUnchangedFiles.BackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.labelUnchangedFiles.BackgroundStyle.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBorder;
			this.labelUnchangedFiles.BackgroundStyle.Class = "";
			this.labelUnchangedFiles.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelUnchangedFiles.BackgroundStyle.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
			this.labelUnchangedFiles.ForeColor = System.Drawing.Color.White;
			this.labelUnchangedFiles.Image = ((System.Drawing.Image)(resources.GetObject("labelUnchangedFiles.Image")));
			this.labelUnchangedFiles.Location = new System.Drawing.Point(89, 18);
			this.labelUnchangedFiles.Name = "labelUnchangedFiles";
			this.labelUnchangedFiles.Size = new System.Drawing.Size(96, 23);
			this.superTooltip1.SetSuperTooltip(this.labelUnchangedFiles, new DevComponents.DotNetBar.SuperTooltipInfo("x unchanged files", "", "Click to show/hide files that are exact copies of files already on your disk.", ((System.Drawing.Image)(resources.GetObject("labelUnchangedFiles.SuperTooltip"))), null, DevComponents.DotNetBar.eTooltipColor.Gray, true, false, new System.Drawing.Size(0, 0)));
			this.labelUnchangedFiles.TabIndex = 70;
			this.labelUnchangedFiles.Text = " 0 Unchanged";
			this.labelUnchangedFiles.Click += new System.EventHandler(this.labelUnchangedFiles_Click);
			// 
			// labelNewFiles
			// 
			this.labelNewFiles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			// 
			// 
			// 
			this.labelNewFiles.BackgroundStyle.BackColorGradientAngle = 90;
			this.labelNewFiles.BackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.labelNewFiles.BackgroundStyle.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBorder;
			this.labelNewFiles.BackgroundStyle.Class = "";
			this.labelNewFiles.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelNewFiles.BackgroundStyle.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
			this.labelNewFiles.ForeColor = System.Drawing.Color.White;
			this.labelNewFiles.Image = ((System.Drawing.Image)(resources.GetObject("labelNewFiles.Image")));
			this.labelNewFiles.Location = new System.Drawing.Point(8, 18);
			this.labelNewFiles.Name = "labelNewFiles";
			this.labelNewFiles.Size = new System.Drawing.Size(75, 23);
			this.superTooltip1.SetSuperTooltip(this.labelNewFiles, new DevComponents.DotNetBar.SuperTooltipInfo("x new files", "", "Click to show/hide new files that don\'t yet exist on disk.", ((System.Drawing.Image)(resources.GetObject("labelNewFiles.SuperTooltip"))), null, DevComponents.DotNetBar.eTooltipColor.Gray, true, false, new System.Drawing.Size(0, 0)));
			this.labelNewFiles.TabIndex = 69;
			this.labelNewFiles.Text = " 0 New";
			this.labelNewFiles.Click += new System.EventHandler(this.labelNewFiles_Click);
			// 
			// panelGenerationErrors
			// 
			this.panelGenerationErrors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.panelGenerationErrors.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelGenerationErrors.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
			this.panelGenerationErrors.Controls.Add(this.buttonX2);
			this.panelGenerationErrors.Controls.Add(this.treeListDuplicatedFiles);
			this.panelGenerationErrors.Controls.Add(this.buttonX1);
			this.panelGenerationErrors.Controls.Add(this.labelX1);
			this.panelGenerationErrors.Location = new System.Drawing.Point(8, 172);
			this.panelGenerationErrors.Name = "panelGenerationErrors";
			this.panelGenerationErrors.Size = new System.Drawing.Size(491, 110);
			this.panelGenerationErrors.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelGenerationErrors.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelGenerationErrors.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelGenerationErrors.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelGenerationErrors.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelGenerationErrors.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelGenerationErrors.Style.GradientAngle = 90;
			this.panelGenerationErrors.TabIndex = 1;
			this.panelGenerationErrors.Text = "panelEx1";
			// 
			// buttonX2
			// 
			this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonX2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonX2.Location = new System.Drawing.Point(4, 74);
			this.buttonX2.Name = "buttonX2";
			this.buttonX2.Size = new System.Drawing.Size(111, 23);
			this.buttonX2.TabIndex = 5;
			this.buttonX2.Text = "Copy To Clipboard";
			this.buttonX2.Click += new System.EventHandler(this.buttonCopyToClipboard_Click);
			// 
			// treeListDuplicatedFiles
			// 
			this.treeListDuplicatedFiles.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
			this.treeListDuplicatedFiles.AllowDrop = true;
			this.treeListDuplicatedFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.treeListDuplicatedFiles.BackColor = System.Drawing.SystemColors.Window;
			// 
			// 
			// 
			this.treeListDuplicatedFiles.BackgroundStyle.Class = "TreeBorderKey";
			this.treeListDuplicatedFiles.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.treeListDuplicatedFiles.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.treeListDuplicatedFiles.Location = new System.Drawing.Point(3, 54);
			this.treeListDuplicatedFiles.Name = "treeListDuplicatedFiles";
			this.treeListDuplicatedFiles.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.node1});
			this.treeListDuplicatedFiles.NodesConnector = this.nodeConnector2;
			this.treeListDuplicatedFiles.NodeStyle = this.elementStyle2;
			this.treeListDuplicatedFiles.PathSeparator = ";";
			this.treeListDuplicatedFiles.Size = new System.Drawing.Size(482, 14);
			this.treeListDuplicatedFiles.Styles.Add(this.elementStyle2);
			this.treeListDuplicatedFiles.TabIndex = 4;
			this.treeListDuplicatedFiles.Text = "advTree1";
			// 
			// node1
			// 
			this.node1.Expanded = true;
			this.node1.Name = "node1";
			this.node1.Text = "node1";
			// 
			// nodeConnector2
			// 
			this.nodeConnector2.LineColor = System.Drawing.SystemColors.ControlText;
			// 
			// elementStyle2
			// 
			this.elementStyle2.Class = "";
			this.elementStyle2.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.elementStyle2.Name = "elementStyle2";
			this.elementStyle2.TextColor = System.Drawing.SystemColors.ControlText;
			// 
			// buttonX1
			// 
			this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonX1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonX1.Location = new System.Drawing.Point(404, 74);
			this.buttonX1.Name = "buttonX1";
			this.buttonX1.Size = new System.Drawing.Size(81, 23);
			this.buttonX1.TabIndex = 3;
			this.buttonX1.Text = "Ok";
			this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
			// 
			// labelX1
			// 
			this.labelX1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			// 
			// 
			// 
			this.labelX1.BackgroundStyle.Class = "";
			this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX1.Location = new System.Drawing.Point(3, 3);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(483, 45);
			this.labelX1.TabIndex = 1;
			this.labelX1.Text = "The following duplicated files and folders were generated. These errors must be f" +
				"ixed in the template before you can continue.";
			this.labelX1.WordWrap = true;
			// 
			// treeList1
			// 
			this.treeList1.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
			this.treeList1.AllowDrop = true;
			this.treeList1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.treeList1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			// 
			// 
			// 
			this.treeList1.BackgroundStyle.Class = "TreeBorderKey";
			this.treeList1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.treeList1.CheckBoxImageChecked = ((System.Drawing.Image)(resources.GetObject("treeList1.CheckBoxImageChecked")));
			this.treeList1.CheckBoxImageIndeterminate = ((System.Drawing.Image)(resources.GetObject("treeList1.CheckBoxImageIndeterminate")));
			this.treeList1.CheckBoxImageUnChecked = ((System.Drawing.Image)(resources.GetObject("treeList1.CheckBoxImageUnChecked")));
			this.treeList1.Columns.Add(this.columnHeader1);
			this.treeList1.ColumnsVisible = false;
			this.treeList1.ContextMenuStrip = this.contextMenuStrip;
			this.treeList1.DragDropEnabled = false;
			this.treeList1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
			this.treeList1.FullRowSelect = false;
			this.treeList1.GridColumnLines = false;
			this.treeList1.GridLinesColor = System.Drawing.Color.LightGray;
			this.treeList1.HotTracking = true;
			this.treeList1.ImageList = this.imageListNodes;
			this.treeList1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.treeList1.Location = new System.Drawing.Point(0, 47);
			this.treeList1.MultiSelect = true;
			this.treeList1.MultiSelectRule = DevComponents.AdvTree.eMultiSelectRule.AnyNode;
			this.treeList1.Name = "treeList1";
			this.treeList1.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.node2});
			this.treeList1.NodesColumnsBackgroundStyle = this.elementStyle3;
			this.treeList1.NodesConnector = this.nodeConnector1;
			this.treeList1.NodeSpacing = 5;
			this.treeList1.NodeStyle = this.elementStyle1;
			this.treeList1.NodeStyleMouseOver = this.elementStyle3;
			this.treeList1.NodeStyleSelected = this.elementStyle3;
			this.treeList1.PathSeparator = ";";
			this.treeList1.Size = new System.Drawing.Size(510, 375);
			this.treeList1.Styles.Add(this.elementStyle1);
			this.treeList1.Styles.Add(this.elementStyle3);
			this.treeList1.TabIndex = 64;
			this.treeList1.Text = "advTree1";
			this.treeList1.AfterCheck += new DevComponents.AdvTree.AdvTreeCellEventHandler(this.treeList1_AfterCheck);
			this.treeList1.BeforeCheck += new DevComponents.AdvTree.AdvTreeCellBeforeCheckEventHandler(this.treeList1_BeforeCheck);
			this.treeList1.BeforeCollapse += new DevComponents.AdvTree.AdvTreeNodeCancelEventHandler(this.treeList1_BeforeCollapse);
			this.treeList1.BeforeExpand += new DevComponents.AdvTree.AdvTreeNodeCancelEventHandler(this.treeList1_BeforeExpand);
			this.treeList1.BeforeNodeSelect += new DevComponents.AdvTree.AdvTreeNodeCancelEventHandler(this.treeList1_BeforeNodeSelect);
			this.treeList1.AfterNodeSelect += new DevComponents.AdvTree.AdvTreeNodeEventHandler(this.treeList1_AfterNodeSelect);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Name = "columnHeader1";
			this.columnHeader1.Width.Relative = 100;
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemViewFileDefault,
            this.toolStripMenuItemViewfileInExplorer});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(251, 52);
			this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
			// 
			// toolStripMenuItemViewFileDefault
			// 
			this.toolStripMenuItemViewFileDefault.Name = "toolStripMenuItemViewFileDefault";
			this.toolStripMenuItemViewFileDefault.Size = new System.Drawing.Size(250, 24);
			this.toolStripMenuItemViewFileDefault.Text = "View File in Default Editor";
			this.toolStripMenuItemViewFileDefault.Click += new System.EventHandler(this.toolStripMenuItemViewFileDefault_Click);
			// 
			// toolStripMenuItemViewfileInExplorer
			// 
			this.toolStripMenuItemViewfileInExplorer.Name = "toolStripMenuItemViewfileInExplorer";
			this.toolStripMenuItemViewfileInExplorer.Size = new System.Drawing.Size(250, 24);
			this.toolStripMenuItemViewfileInExplorer.Text = "View File In File Explorer";
			this.toolStripMenuItemViewfileInExplorer.Click += new System.EventHandler(this.toolStripMenuItemViewfileInExplorer_Click);
			// 
			// imageListNodes
			// 
			this.imageListNodes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListNodes.ImageStream")));
			this.imageListNodes.TransparentColor = System.Drawing.Color.Magenta;
			this.imageListNodes.Images.SetKeyName(0, "");
			this.imageListNodes.Images.SetKeyName(1, "");
			this.imageListNodes.Images.SetKeyName(2, "");
			this.imageListNodes.Images.SetKeyName(3, "");
			this.imageListNodes.Images.SetKeyName(4, "");
			this.imageListNodes.Images.SetKeyName(5, "");
			this.imageListNodes.Images.SetKeyName(6, "");
			this.imageListNodes.Images.SetKeyName(7, "");
			this.imageListNodes.Images.SetKeyName(8, "");
			this.imageListNodes.Images.SetKeyName(9, "");
			this.imageListNodes.Images.SetKeyName(10, "");
			this.imageListNodes.Images.SetKeyName(11, "");
			this.imageListNodes.Images.SetKeyName(12, "");
			this.imageListNodes.Images.SetKeyName(13, "");
			this.imageListNodes.Images.SetKeyName(14, "");
			this.imageListNodes.Images.SetKeyName(15, "VSObject_Event.bmp");
			this.imageListNodes.Images.SetKeyName(16, "VSObject_Constant.bmp");
			this.imageListNodes.Images.SetKeyName(17, "VSObject_Operator.bmp");
			this.imageListNodes.Images.SetKeyName(18, "VSObject_Structure.bmp");
			this.imageListNodes.Images.SetKeyName(19, "VSObject_Interface.bmp");
			this.imageListNodes.Images.SetKeyName(20, "VSObject_Enum.bmp");
			this.imageListNodes.Images.SetKeyName(21, "VSObject_Delegate.bmp");
			this.imageListNodes.Images.SetKeyName(22, "doc_cross_16_h.png");
			this.imageListNodes.Images.SetKeyName(23, "folder_closed_16_h.png");
			this.imageListNodes.Images.SetKeyName(24, "folder_open_16_h.png");
			this.imageListNodes.Images.SetKeyName(25, "question_mark_32.ico");
			this.imageListNodes.Images.SetKeyName(26, "folder_closed_error.png");
			this.imageListNodes.Images.SetKeyName(27, "folder_open_error.png");
			this.imageListNodes.Images.SetKeyName(28, "font_char61_purple_16_h.bmp");
			this.imageListNodes.Images.SetKeyName(29, "NotSet");
			this.imageListNodes.Images.SetKeyName(30, "IntelliMerge");
			this.imageListNodes.Images.SetKeyName(31, "CreateOnly");
			this.imageListNodes.Images.SetKeyName(32, "Overwrite");
			this.imageListNodes.Images.SetKeyName(33, "Plain Text Merge");
			this.imageListNodes.Images.SetKeyName(34, "doc_add_16_h.png");
			this.imageListNodes.Images.SetKeyName(35, "doc_tick_16.png");
			this.imageListNodes.Images.SetKeyName(36, "doc_orange_16_h.png");
			// 
			// node2
			// 
			this.node2.Expanded = true;
			this.node2.Image = global::ArchAngel.Workbench.Properties.Resources.folder_16;
			this.node2.Name = "node2";
			this.node2.Text = "<a><font color=\"White\">Click here to set output folder</font></a>";
			// 
			// elementStyle3
			// 
			this.elementStyle3.BackColor = System.Drawing.Color.White;
			this.elementStyle3.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(228)))), ((int)(((byte)(240)))));
			this.elementStyle3.BackColorGradientAngle = 90;
			this.elementStyle3.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle3.BorderBottomWidth = 1;
			this.elementStyle3.BorderColor = System.Drawing.Color.DarkGray;
			this.elementStyle3.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle3.BorderLeftWidth = 1;
			this.elementStyle3.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle3.BorderRightWidth = 1;
			this.elementStyle3.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle3.BorderTopWidth = 1;
			this.elementStyle3.Class = "";
			this.elementStyle3.CornerDiameter = 4;
			this.elementStyle3.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.elementStyle3.Description = "Gray";
			this.elementStyle3.Name = "elementStyle3";
			this.elementStyle3.PaddingBottom = 1;
			this.elementStyle3.PaddingLeft = 1;
			this.elementStyle3.PaddingRight = 1;
			this.elementStyle3.PaddingTop = 1;
			this.elementStyle3.TextColor = System.Drawing.Color.Black;
			// 
			// nodeConnector1
			// 
			this.nodeConnector1.LineColor = System.Drawing.Color.LightGray;
			// 
			// elementStyle1
			// 
			this.elementStyle1.Class = "";
			this.elementStyle1.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
			this.elementStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.elementStyle1.Name = "elementStyle1";
			this.elementStyle1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
			this.elementStyle1.TextTrimming = DevComponents.DotNetBar.eStyleTextTrimming.EllipsisPath;
			// 
			// labelNumGeneratedFiles
			// 
			this.labelNumGeneratedFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.labelNumGeneratedFiles.Location = new System.Drawing.Point(8, 482);
			this.labelNumGeneratedFiles.Name = "labelNumGeneratedFiles";
			this.labelNumGeneratedFiles.Size = new System.Drawing.Size(493, 15);
			this.labelNumGeneratedFiles.TabIndex = 63;
			this.labelNumGeneratedFiles.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// imageListStatuses
			// 
			this.imageListStatuses.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListStatuses.ImageStream")));
			this.imageListStatuses.TransparentColor = System.Drawing.Color.Magenta;
			this.imageListStatuses.Images.SetKeyName(0, "");
			this.imageListStatuses.Images.SetKeyName(1, "");
			this.imageListStatuses.Images.SetKeyName(2, "");
			this.imageListStatuses.Images.SetKeyName(3, "gear_1.bmp");
			this.imageListStatuses.Images.SetKeyName(4, "CheckMixed.bmp");
			// 
			// dockManager1
			// 
			this.dockManager1.HostContainerControl = this;
			// 
			// backgroundWorker1
			// 
			this.backgroundWorker1.WorkerReportsProgress = true;
			this.backgroundWorker1.WorkerSupportsCancellation = true;
			this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
			this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(panelGeneratedFilesPanelContainer);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel2.Location = new System.Drawing.Point(0, 54);
			this.panel2.MinimumSize = new System.Drawing.Size(327, 350);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(510, 475);
			this.panel2.TabIndex = 62;
			// 
			// superTooltip1
			// 
			this.superTooltip1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.superTooltip1.BeforeTooltipDisplay += new DevComponents.DotNetBar.SuperTooltipEventHandler(this.superTooltip1_BeforeTooltipDisplay);
			// 
			// buttonRefresh
			// 
			this.buttonRefresh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.buttonRefresh.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonRefresh.DisabledImage = ((System.Drawing.Image)(resources.GetObject("buttonRefresh.DisabledImage")));
			this.buttonRefresh.HoverImage = ((System.Drawing.Image)(resources.GetObject("buttonRefresh.HoverImage")));
			this.buttonRefresh.Image = ((System.Drawing.Image)(resources.GetObject("buttonRefresh.Image")));
			this.buttonRefresh.Location = new System.Drawing.Point(12, 10);
			this.buttonRefresh.Name = "buttonRefresh";
			this.buttonRefresh.Size = new System.Drawing.Size(133, 33);
			this.buttonRefresh.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.superTooltip1.SetSuperTooltip(this.buttonRefresh, new DevComponents.DotNetBar.SuperTooltipInfo("Re-generate the files", "<b>Note:</b> No files will be written to your disk until you click the \'Write fil" +
						"es to disk\' button.", "Re-generate the files based on your model.", ((System.Drawing.Image)(resources.GetObject("buttonRefresh.SuperTooltip"))), ((System.Drawing.Image)(resources.GetObject("buttonRefresh.SuperTooltip1"))), DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonRefresh.TabIndex = 68;
			this.buttonRefresh.Text = " Re-generate";
			this.buttonRefresh.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
			// 
			// imageListFileActions
			// 
			this.imageListFileActions.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListFileActions.ImageStream")));
			this.imageListFileActions.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListFileActions.Images.SetKeyName(0, "CreateOnly");
			this.imageListFileActions.Images.SetKeyName(1, "Overwrite");
			this.imageListFileActions.Images.SetKeyName(2, "Not Set");
			this.imageListFileActions.Images.SetKeyName(3, "IntelliMerge");
			// 
			// highlighter1
			// 
			this.highlighter1.ContainerControl = this;
			this.highlighter1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// customValidator1
			// 
			this.customValidator1.ErrorMessage = "Your error message here.";
			this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
			// 
			// expandableSplitter1
			// 
			this.expandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
			this.expandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.expandableSplitter1.ExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
			this.expandableSplitter1.ExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandableSplitter1.ExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.expandableSplitter1.ExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.expandableSplitter1.GripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.expandableSplitter1.GripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.expandableSplitter1.GripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
			this.expandableSplitter1.GripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
			this.expandableSplitter1.HotBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(166)))), ((int)(((byte)(72)))));
			this.expandableSplitter1.HotBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(108)))));
			this.expandableSplitter1.HotBackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2;
			this.expandableSplitter1.HotBackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground;
			this.expandableSplitter1.HotExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
			this.expandableSplitter1.HotExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandableSplitter1.HotExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.expandableSplitter1.HotExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.expandableSplitter1.HotGripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
			this.expandableSplitter1.HotGripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandableSplitter1.HotGripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
			this.expandableSplitter1.HotGripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
			this.expandableSplitter1.Location = new System.Drawing.Point(510, 54);
			this.expandableSplitter1.Name = "expandableSplitter1";
			this.expandableSplitter1.Size = new System.Drawing.Size(6, 475);
			this.expandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
			this.expandableSplitter1.TabIndex = 63;
			this.expandableSplitter1.TabStop = false;
			// 
			// panelEditor
			// 
			this.panelEditor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.panelEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEditor.Location = new System.Drawing.Point(516, 54);
			this.panelEditor.Name = "panelEditor";
			this.panelEditor.Size = new System.Drawing.Size(497, 475);
			this.panelEditor.TabIndex = 64;
			// 
			// labelX2
			// 
			this.labelX2.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.labelX2.BackgroundStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
			this.labelX2.BackgroundStyle.BackColor2 = System.Drawing.Color.Black;
			this.labelX2.BackgroundStyle.BackColorGradientAngle = 90;
			this.labelX2.BackgroundStyle.Class = "";
			this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX2.Dock = System.Windows.Forms.DockStyle.Top;
			this.labelX2.Location = new System.Drawing.Point(0, 0);
			this.labelX2.Name = "labelX2";
			this.labelX2.Size = new System.Drawing.Size(1013, 54);
			this.labelX2.TabIndex = 65;
			// 
			// checkBoxPerformAnalysis
			// 
			this.checkBoxPerformAnalysis.AutoSize = true;
			this.checkBoxPerformAnalysis.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.checkBoxPerformAnalysis.Checked = true;
			this.checkBoxPerformAnalysis.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxPerformAnalysis.ForeColor = System.Drawing.Color.White;
			this.checkBoxPerformAnalysis.Location = new System.Drawing.Point(160, 19);
			this.checkBoxPerformAnalysis.Name = "checkBoxPerformAnalysis";
			this.checkBoxPerformAnalysis.Size = new System.Drawing.Size(135, 17);
			this.superTooltip1.SetSuperTooltip(this.checkBoxPerformAnalysis, new DevComponents.DotNetBar.SuperTooltipInfo("Merge or overwrite files", "<font color=\'Red\'><b>Note:</b></font><b>All</b> existing files will be <b>overwri" +
						"tten</b> if unchecked. No merging will take place.", "Check this to merge the generated files into any existing files.", ((System.Drawing.Image)(resources.GetObject("checkBoxPerformAnalysis.SuperTooltip"))), ((System.Drawing.Image)(resources.GetObject("checkBoxPerformAnalysis.SuperTooltip1"))), DevComponents.DotNetBar.eTooltipColor.Gray));
			this.checkBoxPerformAnalysis.TabIndex = 69;
			this.checkBoxPerformAnalysis.Text = "Merge into existing files";
			this.checkBoxPerformAnalysis.UseVisualStyleBackColor = false;
			// 
			// Output
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.checkBoxPerformAnalysis);
			this.Controls.Add(this.panelEditor);
			this.Controls.Add(this.buttonRefresh);
			this.Controls.Add(this.expandableSplitter1);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.labelX2);
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "Output";
			this.NavBarIcon = ((System.Drawing.Image)(resources.GetObject("$this.NavBarIcon")));
			this.Size = new System.Drawing.Size(1013, 529);
			this.Resize += new System.EventHandler(this.Output_Resize);
			panelGeneratedFilesPanelContainer.ResumeLayout(false);
			this.panelGeneratedFiles.ResumeLayout(false);
			this.panelGenerationErrors.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.treeListDuplicatedFiles)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
			this.contextMenuStrip.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

        private ActiproSoftware.UIStudio.Dock.DockManager dockManager1;
		private System.Windows.Forms.Panel panelGeneratedFiles;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private System.Windows.Forms.ImageList imageListNodes;
        private System.Windows.Forms.ImageList imageListStatuses;
        private System.Windows.Forms.Panel panel2;
		  private System.Windows.Forms.Label labelNumGeneratedFiles;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemViewFileDefault;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemViewfileInExplorer;
		private DevComponents.AdvTree.AdvTree treeList1;
		private DevComponents.AdvTree.ColumnHeader columnHeader1;
		private DevComponents.AdvTree.NodeConnector nodeConnector1;
		private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private DevComponents.DotNetBar.SuperTooltip superTooltip1;
		private System.Windows.Forms.ImageList imageListFileActions;
		private DevComponents.DotNetBar.PanelEx panelGenerationErrors;
		private DevComponents.DotNetBar.LabelX labelX1;
		private DevComponents.DotNetBar.ButtonX buttonX1;
		private DevComponents.AdvTree.AdvTree treeListDuplicatedFiles;
		private DevComponents.AdvTree.Node node1;
		private DevComponents.AdvTree.NodeConnector nodeConnector2;
		private DevComponents.DotNetBar.ElementStyle elementStyle2;
        private DevComponents.DotNetBar.ButtonX buttonX2;
		private DevComponents.DotNetBar.Validator.Highlighter highlighter1;
		private DevComponents.DotNetBar.Validator.CustomValidator customValidator1;
		private DevComponents.DotNetBar.LabelX labelNewFiles;
		private DevComponents.DotNetBar.LabelX labelErrors;
		private DevComponents.DotNetBar.LabelX labelChangedFiles;
		private DevComponents.DotNetBar.LabelX labelUnchangedFiles;
		private DevComponents.DotNetBar.ButtonX buttonWriteFileToDisk;
		private DevComponents.DotNetBar.ButtonX buttonDeselectAll;
		private DevComponents.DotNetBar.ButtonX buttonSelectAll;
        private DevComponents.AdvTree.Node node2;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
        private System.Windows.Forms.Panel panelEditor;
        private DevComponents.DotNetBar.ElementStyle elementStyle3;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.ButtonX buttonRefresh;
		private System.Windows.Forms.CheckBox checkBoxPerformAnalysis;
	}
}
