namespace ArchAngel.Designer
{
	partial class ucFunction
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
            ActiproSoftware.SyntaxEditor.Document document1 = new ActiproSoftware.SyntaxEditor.Document();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucFunction));
            ActiproSoftware.SyntaxEditor.Document document2 = new ActiproSoftware.SyntaxEditor.Document();
            this.syntaxEditor1 = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
            this.lblName = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnEdit = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.chkOverrideDefaultValueFunction = new System.Windows.Forms.CheckBox();
            this.btnResetDefaultCode = new System.Windows.Forms.Button();
            this.dockManager1 = new ActiproSoftware.UIStudio.Dock.DockManager(this.components);
            this.toolWindow1 = new ActiproSoftware.UIStudio.Dock.ToolWindow();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.treeDebugParams = new DevComponents.AdvTree.AdvTree();
            this.columnHeader1 = new DevComponents.AdvTree.ColumnHeader();
            this.columnHeader2 = new DevComponents.AdvTree.ColumnHeader();
            this.node1 = new DevComponents.AdvTree.Node();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.elementStyleMissing = new DevComponents.DotNetBar.ElementStyle();
            this.cbHighlightTemplateWrites = new System.Windows.Forms.CheckBox();
            this.syntaxEditorPreviewText = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemBOO = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.functionStatusStrip = new System.Windows.Forms.StatusStrip();
            this.functionToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.functionToolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolWindowContainer1 = new ActiproSoftware.UIStudio.Dock.ToolWindowContainer();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.toolWindow1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeDebugParams)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.functionStatusStrip.SuspendLayout();
            this.toolWindowContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // syntaxEditor1
            // 
            this.syntaxEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.syntaxEditor1.Document = document1;
            this.syntaxEditor1.LineNumberMarginVisible = true;
            this.syntaxEditor1.Location = new System.Drawing.Point(0, 62);
            this.syntaxEditor1.Margin = new System.Windows.Forms.Padding(2);
            this.syntaxEditor1.Name = "syntaxEditor1";
            this.syntaxEditor1.Size = new System.Drawing.Size(656, 616);
            this.syntaxEditor1.TabIndex = 11;
            this.syntaxEditor1.ViewMouseHover += new ActiproSoftware.SyntaxEditor.EditorViewMouseEventHandler(this.syntaxEditor1_ViewMouseHover);
            this.syntaxEditor1.IncrementalSearch += new ActiproSoftware.SyntaxEditor.IncrementalSearchEventHandler(this.syntaxEditor1_IncrementalSearch);
            this.syntaxEditor1.DocumentTextChanged += new ActiproSoftware.SyntaxEditor.DocumentModificationEventHandler(this.syntaxEditor1_DocumentTextChanged);
            this.syntaxEditor1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.syntaxEditor1_KeyDown);
            this.syntaxEditor1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.syntaxEditor1_KeyPress);
            this.syntaxEditor1.TriggerActivated += new ActiproSoftware.SyntaxEditor.TriggerEventHandler(this.syntaxEditor1_TriggerActivated);
            this.syntaxEditor1.ViewMouseDown += new ActiproSoftware.SyntaxEditor.EditorViewMouseEventHandler(this.syntaxEditor1_ViewMouseDown);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.BackColor = System.Drawing.Color.Transparent;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(13, 9);
            this.lblName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(39, 13);
            this.lblName.TabIndex = 14;
            this.lblName.Text = "Name";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList1.Images.SetKeyName(0, "searchFiles.bmp");
            this.imageList1.Images.SetKeyName(1, "Error.bmp");
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(597, 32);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(57, 24);
            this.btnEdit.TabIndex = 28;
            this.btnEdit.Text = "      &Edit";
            this.btnEdit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.btnEdit, "Edit the function definition: name, paramters etc.");
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // chkOverrideDefaultValueFunction
            // 
            this.chkOverrideDefaultValueFunction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkOverrideDefaultValueFunction.AutoSize = true;
            this.chkOverrideDefaultValueFunction.Location = new System.Drawing.Point(16, 32);
            this.chkOverrideDefaultValueFunction.Name = "chkOverrideDefaultValueFunction";
            this.chkOverrideDefaultValueFunction.Size = new System.Drawing.Size(243, 17);
            this.chkOverrideDefaultValueFunction.TabIndex = 29;
            this.chkOverrideDefaultValueFunction.Text = "Override the default method with custom code";
            this.chkOverrideDefaultValueFunction.UseVisualStyleBackColor = true;
            this.chkOverrideDefaultValueFunction.CheckedChanged += new System.EventHandler(this.chkOverrideDefaultValueFunction_CheckedChanged);
            // 
            // btnResetDefaultCode
            // 
            this.btnResetDefaultCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetDefaultCode.Image = ((System.Drawing.Image)(resources.GetObject("btnResetDefaultCode.Image")));
            this.btnResetDefaultCode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnResetDefaultCode.Location = new System.Drawing.Point(513, 3);
            this.btnResetDefaultCode.Name = "btnResetDefaultCode";
            this.btnResetDefaultCode.Size = new System.Drawing.Size(140, 24);
            this.btnResetDefaultCode.TabIndex = 31;
            this.btnResetDefaultCode.Text = "     Reset Default Code";
            this.btnResetDefaultCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnResetDefaultCode.UseVisualStyleBackColor = true;
            this.btnResetDefaultCode.Click += new System.EventHandler(this.btnResetDefaultCode_Click);
            // 
            // dockManager1
            // 
            this.dockManager1.HostContainerControl = this;
            // 
            // toolWindow1
            // 
            this.toolWindow1.AutoHideSize = new System.Drawing.Size(342, 200);
            this.toolWindow1.Controls.Add(this.panel2);
            this.toolWindow1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolWindow1.DockedSize = new System.Drawing.Size(253, 200);
            this.toolWindow1.DockManager = this.dockManager1;
            this.toolWindow1.Location = new System.Drawing.Point(4, 20);
            this.toolWindow1.Name = "toolWindow1";
            this.toolWindow1.Size = new System.Drawing.Size(253, 680);
            this.toolWindow1.TabIndex = 0;
            this.toolWindow1.Text = "Preview Output";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupPanel1);
            this.panel2.Controls.Add(this.cbHighlightTemplateWrites);
            this.panel2.Controls.Add(this.syntaxEditorPreviewText);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(253, 680);
            this.panel2.TabIndex = 34;
            // 
            // groupPanel1
            // 
            this.groupPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.treeDebugParams);
            this.groupPanel1.Location = new System.Drawing.Point(3, 3);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(247, 113);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.Class = "";
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.Class = "";
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.Class = "";
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 39;
            this.groupPanel1.Text = "Debug Parameters";
            // 
            // treeDebugParams
            // 
            this.treeDebugParams.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.treeDebugParams.AllowDrop = true;
            this.treeDebugParams.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.treeDebugParams.BackgroundStyle.Class = "TreeBorderKey";
            this.treeDebugParams.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.treeDebugParams.CellEdit = true;
            this.treeDebugParams.Columns.Add(this.columnHeader1);
            this.treeDebugParams.Columns.Add(this.columnHeader2);
            this.treeDebugParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeDebugParams.ExpandWidth = 0;
            this.treeDebugParams.FullRowSelect = false;
            this.treeDebugParams.GridLinesColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.treeDebugParams.GridRowLines = true;
            this.treeDebugParams.HotTracking = true;
            this.treeDebugParams.ImageList = this.imageList1;
            this.treeDebugParams.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.treeDebugParams.Location = new System.Drawing.Point(0, 0);
            this.treeDebugParams.Name = "treeDebugParams";
            this.treeDebugParams.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.node1});
            this.treeDebugParams.NodesConnector = this.nodeConnector1;
            this.treeDebugParams.NodeStyle = this.elementStyle1;
            this.treeDebugParams.PathSeparator = ";";
            this.treeDebugParams.Size = new System.Drawing.Size(241, 92);
            this.treeDebugParams.Styles.Add(this.elementStyle1);
            this.treeDebugParams.Styles.Add(this.elementStyleMissing);
            this.treeDebugParams.TabIndex = 38;
            this.treeDebugParams.Text = "advTree1";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Name = "columnHeader1";
            this.columnHeader1.Text = "Parameter";
            this.columnHeader1.Width.Relative = 50;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Name = "columnHeader2";
            this.columnHeader2.Text = "Value";
            this.columnHeader2.Width.Relative = 50;
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
            this.elementStyle1.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle1.Name = "elementStyle1";
            this.elementStyle1.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // elementStyleMissing
            // 
            this.elementStyleMissing.BackColor = System.Drawing.Color.Red;
            this.elementStyleMissing.BackColor2 = System.Drawing.Color.White;
            this.elementStyleMissing.Class = "";
            this.elementStyleMissing.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyleMissing.Name = "elementStyleMissing";
            // 
            // cbHighlightTemplateWrites
            // 
            this.cbHighlightTemplateWrites.AutoSize = true;
            this.cbHighlightTemplateWrites.Checked = true;
            this.cbHighlightTemplateWrites.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbHighlightTemplateWrites.Location = new System.Drawing.Point(18, 122);
            this.cbHighlightTemplateWrites.Name = "cbHighlightTemplateWrites";
            this.cbHighlightTemplateWrites.Size = new System.Drawing.Size(122, 17);
            this.cbHighlightTemplateWrites.TabIndex = 37;
            this.cbHighlightTemplateWrites.Text = "Track Live Changes";
            this.cbHighlightTemplateWrites.UseVisualStyleBackColor = true;
            this.cbHighlightTemplateWrites.CheckedChanged += new System.EventHandler(this.cbHighlightTemplateWrites_CheckedChanged);
            // 
            // syntaxEditorPreviewText
            // 
            this.syntaxEditorPreviewText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.syntaxEditorPreviewText.ContextMenuStrip = this.contextMenuStrip1;
            this.syntaxEditorPreviewText.Document = document2;
            this.syntaxEditorPreviewText.LineNumberMarginVisible = true;
            this.syntaxEditorPreviewText.Location = new System.Drawing.Point(0, 145);
            this.syntaxEditorPreviewText.Name = "syntaxEditorPreviewText";
            this.syntaxEditorPreviewText.Size = new System.Drawing.Size(253, 535);
            this.syntaxEditorPreviewText.TabIndex = 33;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemBOO});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(164, 26);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // toolStripMenuItemBOO
            // 
            this.toolStripMenuItemBOO.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItemBOO.Name = "toolStripMenuItemBOO";
            this.toolStripMenuItemBOO.Size = new System.Drawing.Size(163, 22);
            this.toolStripMenuItemBOO.Text = "Break On Output";
            this.toolStripMenuItemBOO.Click += new System.EventHandler(this.toolStripMenuItemBOO_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.syntaxEditor1);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.functionStatusStrip);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(656, 700);
            this.panel1.TabIndex = 35;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnEdit);
            this.panel3.Controls.Add(this.chkOverrideDefaultValueFunction);
            this.panel3.Controls.Add(this.btnResetDefaultCode);
            this.panel3.Controls.Add(this.lblName);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(656, 62);
            this.panel3.TabIndex = 33;
            // 
            // functionStatusStrip
            // 
            this.functionStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.functionToolStripStatusLabel,
            this.functionToolStripProgressBar});
            this.functionStatusStrip.Location = new System.Drawing.Point(0, 678);
            this.functionStatusStrip.Name = "functionStatusStrip";
            this.functionStatusStrip.Size = new System.Drawing.Size(656, 22);
            this.functionStatusStrip.TabIndex = 32;
            this.functionStatusStrip.Text = "statusStrip1";
            // 
            // functionToolStripStatusLabel
            // 
            this.functionToolStripStatusLabel.Name = "functionToolStripStatusLabel";
            this.functionToolStripStatusLabel.Size = new System.Drawing.Size(641, 17);
            this.functionToolStripStatusLabel.Spring = true;
            // 
            // functionToolStripProgressBar
            // 
            this.functionToolStripProgressBar.Name = "functionToolStripProgressBar";
            this.functionToolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            this.functionToolStripProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.functionToolStripProgressBar.Visible = false;
            // 
            // toolWindowContainer1
            // 
            this.toolWindowContainer1.Controls.Add(this.toolWindow1);
            this.toolWindowContainer1.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolWindowContainer1.DockManager = this.dockManager1;
            this.toolWindowContainer1.Location = new System.Drawing.Point(656, 0);
            this.toolWindowContainer1.Name = "toolWindowContainer1";
            this.toolWindowContainer1.Size = new System.Drawing.Size(257, 700);
            this.toolWindowContainer1.TabIndex = 36;
            // 
            // ucFunction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(219)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolWindowContainer1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ucFunction";
            this.Size = new System.Drawing.Size(913, 700);
            this.Tag = "ucFunction";
            this.Load += new System.EventHandler(this.ucFunction_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ucFunction_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.toolWindow1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeDebugParams)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.functionStatusStrip.ResumeLayout(false);
            this.functionStatusStrip.PerformLayout();
            this.toolWindowContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

	    #endregion

        internal ActiproSoftware.SyntaxEditor.SyntaxEditor syntaxEditor1;
		private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.CheckBox chkOverrideDefaultValueFunction;
        private System.Windows.Forms.Button btnResetDefaultCode;
        private ActiproSoftware.UIStudio.Dock.DockManager dockManager1;
        private ActiproSoftware.UIStudio.Dock.ToolWindow toolWindow1;
        private ActiproSoftware.SyntaxEditor.SyntaxEditor syntaxEditorPreviewText;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private ActiproSoftware.UIStudio.Dock.ToolWindowContainer toolWindowContainer1;
        private System.Windows.Forms.CheckBox cbHighlightTemplateWrites;
        private System.Windows.Forms.StatusStrip functionStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel functionToolStripStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar functionToolStripProgressBar;
        private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemBOO;
        private DevComponents.AdvTree.AdvTree treeDebugParams;
        private DevComponents.AdvTree.Node node1;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private DevComponents.AdvTree.ColumnHeader columnHeader1;
        private DevComponents.AdvTree.ColumnHeader columnHeader2;
        private DevComponents.DotNetBar.ElementStyle elementStyleMissing;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
	}
}
