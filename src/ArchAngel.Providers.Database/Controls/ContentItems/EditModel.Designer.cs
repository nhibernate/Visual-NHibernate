namespace ArchAngel.Providers.Database.Controls.ContentItems
{
    partial class EditModel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditModel));
            this.contextMenuStripTreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemTreeViewAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemTreeViewAddMapColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemTreeViewEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemTreeViewExecuteStoredProcedure = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemTreeViewDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListButton = new System.Windows.Forms.ImageList(this.components);
            this.buttonRefreshDatabase = new System.Windows.Forms.Button();
            this.imageListTreeList = new System.Windows.Forms.ImageList(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btnResetDefaultValues = new System.Windows.Forms.Button();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.treeList = new DevComponents.AdvTree.AdvTree();
            this.colAlias = new DevComponents.AdvTree.ColumnHeader();
            this.colName = new DevComponents.AdvTree.ColumnHeader();
            this.colNotes = new DevComponents.AdvTree.ColumnHeader();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.elementStyleInvalid = new DevComponents.DotNetBar.ElementStyle();
            this.elementStyleUnselected = new DevComponents.DotNetBar.ElementStyle();
            this.elementStyleMapColumn = new DevComponents.DotNetBar.ElementStyle();
            this.elementStyleUserDefined = new DevComponents.DotNetBar.ElementStyle();
            this.elementStyleSelected = new DevComponents.DotNetBar.ElementStyle();
            this.chkHideNonSelectedNodes = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.checkBoxKeepChanges = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.labelDatabaseFeedback = new DevComponents.DotNetBar.LabelX();
            this.contextMenuStripTreeView.SuspendLayout();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStripTreeView
            // 
            this.contextMenuStripTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemTreeViewAdd,
            this.toolStripMenuItemTreeViewAddMapColumn,
            this.toolStripMenuItemTreeViewEdit,
            this.toolStripMenuItemTreeViewExecuteStoredProcedure,
            this.toolStripMenuItem1,
            this.toolStripMenuItemTreeViewDelete,
            this.refreshToolStripMenuItem});
            this.contextMenuStripTreeView.Name = "contextMenuStripTreeView";
            this.contextMenuStripTreeView.Size = new System.Drawing.Size(166, 142);
            // 
            // toolStripMenuItemTreeViewAdd
            // 
            this.toolStripMenuItemTreeViewAdd.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemTreeViewAdd.Image")));
            this.toolStripMenuItemTreeViewAdd.Name = "toolStripMenuItemTreeViewAdd";
            this.toolStripMenuItemTreeViewAdd.Size = new System.Drawing.Size(165, 22);
            this.toolStripMenuItemTreeViewAdd.Text = "&Add";
            this.toolStripMenuItemTreeViewAdd.Click += new System.EventHandler(this.toolStripMenuItemTreeViewAdd_Click);
            // 
            // toolStripMenuItemTreeViewAddMapColumn
            // 
            this.toolStripMenuItemTreeViewAddMapColumn.Name = "toolStripMenuItemTreeViewAddMapColumn";
            this.toolStripMenuItemTreeViewAddMapColumn.Size = new System.Drawing.Size(165, 22);
            this.toolStripMenuItemTreeViewAddMapColumn.Text = "Add &Map Column";
            this.toolStripMenuItemTreeViewAddMapColumn.Click += new System.EventHandler(this.toolStripMenuItemTreeViewAddMapColumn_Click);
            // 
            // toolStripMenuItemTreeViewEdit
            // 
            this.toolStripMenuItemTreeViewEdit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemTreeViewEdit.Image")));
            this.toolStripMenuItemTreeViewEdit.Name = "toolStripMenuItemTreeViewEdit";
            this.toolStripMenuItemTreeViewEdit.Size = new System.Drawing.Size(165, 22);
            this.toolStripMenuItemTreeViewEdit.Text = "&Edit";
            this.toolStripMenuItemTreeViewEdit.Click += new System.EventHandler(this.toolStripMenuItemTreeViewEdit_Click);
            // 
            // toolStripMenuItemTreeViewExecuteStoredProcedure
            // 
            this.toolStripMenuItemTreeViewExecuteStoredProcedure.Name = "toolStripMenuItemTreeViewExecuteStoredProcedure";
            this.toolStripMenuItemTreeViewExecuteStoredProcedure.Size = new System.Drawing.Size(165, 22);
            this.toolStripMenuItemTreeViewExecuteStoredProcedure.Text = "&Execute";
            this.toolStripMenuItemTreeViewExecuteStoredProcedure.Click += new System.EventHandler(this.toolStripMenuItemTreeViewExecuteStoredProcedure_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(162, 6);
            // 
            // toolStripMenuItemTreeViewDelete
            // 
            this.toolStripMenuItemTreeViewDelete.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemTreeViewDelete.Image")));
            this.toolStripMenuItemTreeViewDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMenuItemTreeViewDelete.Name = "toolStripMenuItemTreeViewDelete";
            this.toolStripMenuItemTreeViewDelete.Size = new System.Drawing.Size(165, 22);
            this.toolStripMenuItemTreeViewDelete.Text = "&Delete";
            this.toolStripMenuItemTreeViewDelete.Click += new System.EventHandler(this.toolStripMenuItemTreeViewDelete_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("refreshToolStripMenuItem.Image")));
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // imageListButton
            // 
            this.imageListButton.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListButton.ImageStream")));
            this.imageListButton.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListButton.Images.SetKeyName(0, "RefreshDocViewHS.png");
            this.imageListButton.Images.SetKeyName(1, "NewDocumentHS.png");
            this.imageListButton.Images.SetKeyName(2, "services.ico");
            // 
            // buttonRefreshDatabase
            // 
            this.buttonRefreshDatabase.Image = ((System.Drawing.Image)(resources.GetObject("buttonRefreshDatabase.Image")));
            this.buttonRefreshDatabase.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonRefreshDatabase.Location = new System.Drawing.Point(4, 7);
            this.buttonRefreshDatabase.Name = "buttonRefreshDatabase";
            this.buttonRefreshDatabase.Size = new System.Drawing.Size(119, 23);
            this.buttonRefreshDatabase.TabIndex = 31;
            this.buttonRefreshDatabase.Text = "    Refresh Database";
            this.buttonRefreshDatabase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonRefreshDatabase.UseVisualStyleBackColor = true;
            this.buttonRefreshDatabase.Click += new System.EventHandler(this.buttonRefreshDatabase_Click);
            // 
            // imageListTreeList
            // 
            this.imageListTreeList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTreeList.ImageStream")));
            this.imageListTreeList.TransparentColor = System.Drawing.Color.Magenta;
            this.imageListTreeList.Images.SetKeyName(0, "");
            this.imageListTreeList.Images.SetKeyName(1, "");
            this.imageListTreeList.Images.SetKeyName(2, "");
            this.imageListTreeList.Images.SetKeyName(3, "VSProject_database.bmp");
            this.imageListTreeList.Images.SetKeyName(4, "Webcontrol_Gridview.bmp");
            this.imageListTreeList.Images.SetKeyName(5, "HighlightHS.png");
            this.imageListTreeList.Images.SetKeyName(6, "PrimaryKeyHS.png");
            this.imageListTreeList.Images.SetKeyName(7, "List_NumberedHS.png");
            this.imageListTreeList.Images.SetKeyName(8, "Filter2HS.png");
            this.imageListTreeList.Images.SetKeyName(9, "List_BulletsHS.png");
            this.imageListTreeList.Images.SetKeyName(10, "RelationshipsHS.png");
            this.imageListTreeList.Images.SetKeyName(11, "FunctionHS.png");
            this.imageListTreeList.Images.SetKeyName(12, "bullet_green.png");
            this.imageListTreeList.Images.SetKeyName(13, "bullet_orange.png");
            this.imageListTreeList.Images.SetKeyName(14, "bullet_red.png");
            this.imageListTreeList.Images.SetKeyName(15, "VSProject_database_red_dot.ico");
            this.imageListTreeList.Images.SetKeyName(16, "Webcontrol_Gridview_red_dot.ico");
            this.imageListTreeList.Images.SetKeyName(17, "HighlightHS_red_dot.ico");
            this.imageListTreeList.Images.SetKeyName(18, "PrimaryKeyHS_red_dot.ico");
            this.imageListTreeList.Images.SetKeyName(19, "List_NumberedHS_red_dot.ico");
            this.imageListTreeList.Images.SetKeyName(20, "Filter2HS_red_dot.ico");
            this.imageListTreeList.Images.SetKeyName(21, "List_BulletsHS_red_dot.ico");
            this.imageListTreeList.Images.SetKeyName(22, "RelationshipsHS_red_dot.ico");
            this.imageListTreeList.Images.SetKeyName(23, "FunctionHS_red_dot.ico");
            this.imageListTreeList.Images.SetKeyName(24, "Webcontrol_ConnectionsZone.bmp");
            this.imageListTreeList.Images.SetKeyName(25, "select_16_h.png");
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // btnResetDefaultValues
            // 
            this.btnResetDefaultValues.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetDefaultValues.Image = ((System.Drawing.Image)(resources.GetObject("btnResetDefaultValues.Image")));
            this.btnResetDefaultValues.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnResetDefaultValues.Location = new System.Drawing.Point(773, 4);
            this.btnResetDefaultValues.Name = "btnResetDefaultValues";
            this.btnResetDefaultValues.Size = new System.Drawing.Size(135, 24);
            this.btnResetDefaultValues.TabIndex = 37;
            this.btnResetDefaultValues.Text = "     Reset Default Values";
            this.btnResetDefaultValues.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnResetDefaultValues.UseVisualStyleBackColor = true;
            this.btnResetDefaultValues.Click += new System.EventHandler(this.btnResetDefaultValues2_Click);
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panelEx1.Controls.Add(this.treeList);
            this.panelEx1.Controls.Add(this.chkHideNonSelectedNodes);
            this.panelEx1.Controls.Add(this.checkBoxKeepChanges);
            this.panelEx1.Controls.Add(this.labelDatabaseFeedback);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(911, 549);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 42;
            // 
            // treeList
            // 
            this.treeList.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.treeList.AllowDrop = true;
            this.treeList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeList.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.treeList.BackgroundStyle.Class = "TreeBorderKey";
            this.treeList.Columns.Add(this.colAlias);
            this.treeList.Columns.Add(this.colName);
            this.treeList.Columns.Add(this.colNotes);
            this.treeList.DragDropEnabled = false;
            this.treeList.GridLinesColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.treeList.GridRowLines = true;
            this.treeList.ImageList = this.imageListTreeList;
            this.treeList.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.treeList.Location = new System.Drawing.Point(4, 55);
            this.treeList.Name = "treeList";
            this.treeList.NodesConnector = this.nodeConnector1;
            this.treeList.NodeStyle = this.elementStyle1;
            this.treeList.PathSeparator = ";";
            this.treeList.Size = new System.Drawing.Size(903, 491);
            this.treeList.Styles.Add(this.elementStyle1);
            this.treeList.Styles.Add(this.elementStyleInvalid);
            this.treeList.Styles.Add(this.elementStyleUnselected);
            this.treeList.Styles.Add(this.elementStyleMapColumn);
            this.treeList.Styles.Add(this.elementStyleUserDefined);
            this.treeList.Styles.Add(this.elementStyleSelected);
            this.treeList.TabIndex = 6;
            this.treeList.Text = "advTree1";
            this.treeList.NodeDoubleClick += new DevComponents.AdvTree.TreeNodeMouseEventHandler(this.treeList_NodeDoubleClick);
            this.treeList.AfterCheck += new DevComponents.AdvTree.AdvTreeCellEventHandler(this.treeList_AfterCheck);
            this.treeList.NodeClick += new DevComponents.AdvTree.TreeNodeMouseEventHandler(this.treeList_NodeClick);
            this.treeList.BeforeExpand += new DevComponents.AdvTree.AdvTreeNodeCancelEventHandler(this.treeList_BeforeExpand);
            // 
            // colAlias
            // 
            this.colAlias.Name = "colAlias";
            this.colAlias.Text = "Alias";
            this.colAlias.Width.Relative = 50;
            // 
            // colName
            // 
            this.colName.Name = "colName";
            this.colName.Text = "Name";
            this.colName.Width.Relative = 20;
            // 
            // colNotes
            // 
            this.colNotes.Name = "colNotes";
            this.colNotes.Text = "Notes";
            this.colNotes.Width.Relative = 30;
            // 
            // nodeConnector1
            // 
            this.nodeConnector1.LineColor = System.Drawing.SystemColors.ControlText;
            // 
            // elementStyle1
            // 
            this.elementStyle1.Name = "elementStyle1";
            this.elementStyle1.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // elementStyleInvalid
            // 
            this.elementStyleInvalid.BackColor = System.Drawing.Color.Red;
            this.elementStyleInvalid.BackColor2 = System.Drawing.Color.DarkSalmon;
            this.elementStyleInvalid.BackColorGradientAngle = 90;
            this.elementStyleInvalid.Name = "elementStyleInvalid";
            this.elementStyleInvalid.TextColor = System.Drawing.Color.White;
            // 
            // elementStyleUnselected
            // 
            this.elementStyleUnselected.Name = "elementStyleUnselected";
            this.elementStyleUnselected.TextColor = System.Drawing.Color.Gray;
            // 
            // elementStyleMapColumn
            // 
            this.elementStyleMapColumn.Name = "elementStyleMapColumn";
            this.elementStyleMapColumn.TextColor = System.Drawing.Color.Green;
            // 
            // elementStyleUserDefined
            // 
            this.elementStyleUserDefined.Name = "elementStyleUserDefined";
            this.elementStyleUserDefined.TextColor = System.Drawing.Color.Blue;
            // 
            // elementStyleSelected
            // 
            this.elementStyleSelected.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.elementStyleSelected.BackColorGradientAngle = 90;
            this.elementStyleSelected.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarCaptionBackground2;
            this.elementStyleSelected.Name = "elementStyleSelected";
            this.elementStyleSelected.TextColor = System.Drawing.Color.White;
            // 
            // chkHideNonSelectedNodes
            // 
            this.chkHideNonSelectedNodes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkHideNonSelectedNodes.AutoSize = true;
            this.chkHideNonSelectedNodes.Location = new System.Drawing.Point(759, 33);
            this.chkHideNonSelectedNodes.Name = "chkHideNonSelectedNodes";
            this.chkHideNonSelectedNodes.Size = new System.Drawing.Size(149, 15);
            this.chkHideNonSelectedNodes.TabIndex = 5;
            this.chkHideNonSelectedNodes.Text = "Hide non-selected objects";
            this.chkHideNonSelectedNodes.CheckedChanged += new System.EventHandler(this.chkHideNonSelectedNodes_CheckedChanged);
            // 
            // checkBoxKeepChanges
            // 
            this.checkBoxKeepChanges.AutoSize = true;
            this.checkBoxKeepChanges.Checked = true;
            this.checkBoxKeepChanges.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxKeepChanges.CheckValue = "Y";
            this.checkBoxKeepChanges.Location = new System.Drawing.Point(129, 12);
            this.checkBoxKeepChanges.Name = "checkBoxKeepChanges";
            this.checkBoxKeepChanges.Size = new System.Drawing.Size(174, 15);
            this.checkBoxKeepChanges.TabIndex = 1;
            this.checkBoxKeepChanges.Text = "Keep changes when refreshing";
            // 
            // labelDatabaseFeedback
            // 
            this.labelDatabaseFeedback.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDatabaseFeedback.Location = new System.Drawing.Point(4, 33);
            this.labelDatabaseFeedback.Name = "labelDatabaseFeedback";
            this.labelDatabaseFeedback.Size = new System.Drawing.Size(749, 19);
            this.labelDatabaseFeedback.TabIndex = 0;
            this.labelDatabaseFeedback.Text = "labelDatabaseFeedback";
            // 
            // EditModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnResetDefaultValues);
            this.Controls.Add(this.buttonRefreshDatabase);
            this.Controls.Add(this.panelEx1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "EditModel";
            this.NavBarIcon = ((System.Drawing.Image)(resources.GetObject("$this.NavBarIcon")));
            this.NavBarIconTransparentColor = System.Drawing.Color.Magenta;
            this.Size = new System.Drawing.Size(911, 549);
            this.contextMenuStripTreeView.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStripTreeView;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTreeViewAdd;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTreeViewAddMapColumn;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTreeViewEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTreeViewDelete;
        private System.Windows.Forms.Button buttonRefreshDatabase;
        private System.Windows.Forms.ImageList imageListButton;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ImageList imageListTreeList;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTreeViewExecuteStoredProcedure;
        private System.Windows.Forms.Button btnResetDefaultValues;
		private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxKeepChanges;
        private DevComponents.DotNetBar.LabelX labelDatabaseFeedback;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkHideNonSelectedNodes;
        private DevComponents.AdvTree.AdvTree treeList;
        private DevComponents.AdvTree.ColumnHeader colAlias;
        private DevComponents.AdvTree.ColumnHeader colName;
        private DevComponents.AdvTree.ColumnHeader colNotes;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private DevComponents.DotNetBar.ElementStyle elementStyleInvalid;
        private DevComponents.DotNetBar.ElementStyle elementStyleUnselected;
        private DevComponents.DotNetBar.ElementStyle elementStyleMapColumn;
        private DevComponents.DotNetBar.ElementStyle elementStyleUserDefined;
        private DevComponents.DotNetBar.ElementStyle elementStyleSelected;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        //private Skybound.VisualTips.VisualTipProvider visualTipProvider1;
    }
}
