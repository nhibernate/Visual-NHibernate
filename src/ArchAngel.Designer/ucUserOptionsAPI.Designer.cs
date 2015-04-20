namespace ArchAngel.Designer
{
    partial class ucUserOptionsAPI
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucUserOptionsAPI));
			this.ucLabel1 = new Slyce.Common.Controls.ucHeading();
			this.treeListAPI = new DevExpress.XtraTreeList.TreeList();
			this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.treeListColumn6 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.treeListColumn7 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.mnuTreeNode = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mnuItemViewFunction = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuItemNewUserOption = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuItemNewProperty = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuItemEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuTreeNodeSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.ucHeading1 = new Slyce.Common.Controls.ucHeading();
			this.treeListAPIHelper = new DevExpress.XtraTreeList.TreeList();
			this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.treeListColumn5 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.treeListColumn8 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.mnuHelperTreeEditVirtualProperty = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mnuHelperItemViewFunction = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuHelperItemDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuHelperItemNewVirtualProperty = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.expandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
			this.panel1 = new System.Windows.Forms.Panel();
			this.mnuItemNewExtensionMethod = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.treeListAPI)).BeginInit();
			this.mnuTreeNode.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.treeListAPIHelper)).BeginInit();
			this.mnuHelperTreeEditVirtualProperty.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// ucLabel1
			// 
			this.ucLabel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.ucLabel1.Location = new System.Drawing.Point(0, 0);
			this.ucLabel1.Margin = new System.Windows.Forms.Padding(2);
			this.ucLabel1.Name = "ucLabel1";
			this.ucLabel1.Size = new System.Drawing.Size(756, 25);
			this.ucLabel1.TabIndex = 8;
			this.ucLabel1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// treeListAPI
			// 
			this.treeListAPI.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn2,
            this.treeListColumn3,
            this.treeListColumn6,
            this.treeListColumn7});
			this.treeListAPI.ContextMenuStrip = this.mnuTreeNode;
			this.treeListAPI.Dock = System.Windows.Forms.DockStyle.Top;
			this.treeListAPI.Location = new System.Drawing.Point(0, 25);
			this.treeListAPI.Name = "treeListAPI";
			this.treeListAPI.OptionsSelection.EnableAppearanceFocusedRow = false;
			this.treeListAPI.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
			this.treeListAPI.SelectImageList = this.imageList1;
			this.treeListAPI.Size = new System.Drawing.Size(756, 227);
			this.treeListAPI.TabIndex = 15;
			this.treeListAPI.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.treeListAPI_NodeCellStyle);
			this.treeListAPI.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeListAPI_MouseDown);
			this.treeListAPI.CustomNodeCellEdit += new DevExpress.XtraTreeList.GetCustomNodeCellEditEventHandler(this.treeListAPI_CustomNodeCellEdit);
			this.treeListAPI.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeListAPI_FocusedNodeChanged);
			// 
			// treeListColumn1
			// 
			this.treeListColumn1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.treeListColumn1.AppearanceCell.Options.UseFont = true;
			this.treeListColumn1.Caption = "Object / Property";
			this.treeListColumn1.FieldName = "Object";
			this.treeListColumn1.MinWidth = 27;
			this.treeListColumn1.Name = "treeListColumn1";
			this.treeListColumn1.OptionsColumn.AllowEdit = false;
			this.treeListColumn1.OptionsColumn.AllowSort = false;
			this.treeListColumn1.SortOrder = System.Windows.Forms.SortOrder.Ascending;
			this.treeListColumn1.Visible = true;
			this.treeListColumn1.VisibleIndex = 0;
			// 
			// treeListColumn2
			// 
			this.treeListColumn2.Caption = "Default Value Function";
			this.treeListColumn2.FieldName = "Value";
			this.treeListColumn2.Name = "treeListColumn2";
			this.treeListColumn2.OptionsColumn.AllowEdit = false;
			this.treeListColumn2.OptionsColumn.AllowSort = false;
			this.treeListColumn2.Visible = true;
			this.treeListColumn2.VisibleIndex = 1;
			// 
			// treeListColumn3
			// 
			this.treeListColumn3.Caption = "Validation Function";
			this.treeListColumn3.FieldName = "Validation Function";
			this.treeListColumn3.Name = "treeListColumn3";
			this.treeListColumn3.OptionsColumn.AllowEdit = false;
			this.treeListColumn3.OptionsColumn.AllowSort = false;
			this.treeListColumn3.Visible = true;
			this.treeListColumn3.VisibleIndex = 2;
			// 
			// treeListColumn6
			// 
			this.treeListColumn6.Caption = "DisplayToUser Function";
			this.treeListColumn6.FieldName = "DisplayToUser Function";
			this.treeListColumn6.Name = "treeListColumn6";
			this.treeListColumn6.Visible = true;
			this.treeListColumn6.VisibleIndex = 3;
			// 
			// treeListColumn7
			// 
			this.treeListColumn7.Caption = "Refresh per session";
			this.treeListColumn7.FieldName = "Refresh per session";
			this.treeListColumn7.Name = "treeListColumn7";
			this.treeListColumn7.OptionsColumn.AllowEdit = false;
			this.treeListColumn7.Visible = true;
			this.treeListColumn7.VisibleIndex = 4;
			// 
			// mnuTreeNode
			// 
			this.mnuTreeNode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemViewFunction,
            this.mnuItemNewUserOption,
            this.mnuItemNewProperty,
            this.mnuItemEdit,
            this.mnuTreeNodeSeparator1,
            this.mnuItemDelete});
			this.mnuTreeNode.Name = "mnuTabPage";
			this.mnuTreeNode.Size = new System.Drawing.Size(162, 120);
			this.mnuTreeNode.Opening += new System.ComponentModel.CancelEventHandler(this.mnuTreeNode_Opening);
			// 
			// mnuItemViewFunction
			// 
			this.mnuItemViewFunction.Image = ((System.Drawing.Image)(resources.GetObject("mnuItemViewFunction.Image")));
			this.mnuItemViewFunction.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.mnuItemViewFunction.Name = "mnuItemViewFunction";
			this.mnuItemViewFunction.Size = new System.Drawing.Size(161, 22);
			this.mnuItemViewFunction.Text = "&View";
			this.mnuItemViewFunction.ToolTipText = "View/edit the function that populates this file";
			this.mnuItemViewFunction.Click += new System.EventHandler(this.mnuItemViewFunction_Click);
			// 
			// mnuItemNewUserOption
			// 
			this.mnuItemNewUserOption.Image = ((System.Drawing.Image)(resources.GetObject("mnuItemNewUserOption.Image")));
			this.mnuItemNewUserOption.Name = "mnuItemNewUserOption";
			this.mnuItemNewUserOption.Size = new System.Drawing.Size(161, 22);
			this.mnuItemNewUserOption.Text = "New &user option";
			this.mnuItemNewUserOption.Click += new System.EventHandler(this.mnuItemNewUserOption_Click);
			// 
			// mnuItemNewProperty
			// 
			this.mnuItemNewProperty.Image = ((System.Drawing.Image)(resources.GetObject("mnuItemNewProperty.Image")));
			this.mnuItemNewProperty.Name = "mnuItemNewProperty";
			this.mnuItemNewProperty.Size = new System.Drawing.Size(161, 22);
			this.mnuItemNewProperty.Text = "New &property";
			this.mnuItemNewProperty.Click += new System.EventHandler(this.mnuItemNewProperty_Click);
			// 
			// mnuItemEdit
			// 
			this.mnuItemEdit.Image = ((System.Drawing.Image)(resources.GetObject("mnuItemEdit.Image")));
			this.mnuItemEdit.Name = "mnuItemEdit";
			this.mnuItemEdit.Size = new System.Drawing.Size(161, 22);
			this.mnuItemEdit.Text = "&Edit";
			this.mnuItemEdit.Click += new System.EventHandler(this.mnuItemEdit_Click);
			// 
			// mnuTreeNodeSeparator1
			// 
			this.mnuTreeNodeSeparator1.Name = "mnuTreeNodeSeparator1";
			this.mnuTreeNodeSeparator1.Size = new System.Drawing.Size(158, 6);
			// 
			// mnuItemDelete
			// 
			this.mnuItemDelete.Image = ((System.Drawing.Image)(resources.GetObject("mnuItemDelete.Image")));
			this.mnuItemDelete.Name = "mnuItemDelete";
			this.mnuItemDelete.Size = new System.Drawing.Size(161, 22);
			this.mnuItemDelete.Text = "&Delete";
			this.mnuItemDelete.Click += new System.EventHandler(this.mnuItemDelete_Click);
			// 
			// repositoryItemCheckEdit1
			// 
			this.repositoryItemCheckEdit1.AutoHeight = false;
			this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
			this.imageList1.Images.SetKeyName(0, "VSObject_Class.bmp");
			this.imageList1.Images.SetKeyName(1, "VSObject_Properties.bmp");
			this.imageList1.Images.SetKeyName(2, "VSObject_Namespace.bmp");
			this.imageList1.Images.SetKeyName(3, "VSObject_Method.bmp");
			// 
			// ucHeading1
			// 
			this.ucHeading1.Dock = System.Windows.Forms.DockStyle.Top;
			this.ucHeading1.Location = new System.Drawing.Point(0, 0);
			this.ucHeading1.Margin = new System.Windows.Forms.Padding(2);
			this.ucHeading1.Name = "ucHeading1";
			this.ucHeading1.Size = new System.Drawing.Size(756, 25);
			this.ucHeading1.TabIndex = 19;
			this.ucHeading1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// treeListAPIHelper
			// 
			this.treeListAPIHelper.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.treeListAPIHelper.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn4,
            this.treeListColumn5,
            this.treeListColumn8});
			this.treeListAPIHelper.ContextMenuStrip = this.mnuHelperTreeEditVirtualProperty;
			this.treeListAPIHelper.Location = new System.Drawing.Point(0, 25);
			this.treeListAPIHelper.Name = "treeListAPIHelper";
			this.treeListAPIHelper.OptionsSelection.EnableAppearanceFocusedRow = false;
			this.treeListAPIHelper.SelectImageList = this.imageList1;
			this.treeListAPIHelper.Size = new System.Drawing.Size(756, 252);
			this.treeListAPIHelper.TabIndex = 18;
			this.treeListAPIHelper.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.treeListAPIHelper_NodeCellStyle);
			this.treeListAPIHelper.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeListAPIHelper_MouseDown);
			this.treeListAPIHelper.CustomNodeCellEdit += new DevExpress.XtraTreeList.GetCustomNodeCellEditEventHandler(this.treeListAPIHelper_CustomNodeCellEdit);
			// 
			// treeListColumn4
			// 
			this.treeListColumn4.Caption = "Namespace / Method";
			this.treeListColumn4.FieldName = "Object";
			this.treeListColumn4.MinWidth = 27;
			this.treeListColumn4.Name = "treeListColumn4";
			this.treeListColumn4.OptionsColumn.AllowEdit = false;
			this.treeListColumn4.OptionsColumn.AllowSort = false;
			this.treeListColumn4.SortOrder = System.Windows.Forms.SortOrder.Ascending;
			this.treeListColumn4.Visible = true;
			this.treeListColumn4.VisibleIndex = 0;
			this.treeListColumn4.Width = 224;
			// 
			// treeListColumn5
			// 
			this.treeListColumn5.Caption = "Override Function";
			this.treeListColumn5.FieldName = "Value";
			this.treeListColumn5.Name = "treeListColumn5";
			this.treeListColumn5.OptionsColumn.AllowEdit = false;
			this.treeListColumn5.OptionsColumn.AllowSort = false;
			this.treeListColumn5.Visible = true;
			this.treeListColumn5.VisibleIndex = 1;
			this.treeListColumn5.Width = 385;
			// 
			// treeListColumn8
			// 
			this.treeListColumn8.Caption = "Refresh per session";
			this.treeListColumn8.FieldName = "Refresh per session";
			this.treeListColumn8.Name = "treeListColumn8";
			this.treeListColumn8.OptionsColumn.AllowEdit = false;
			this.treeListColumn8.Visible = true;
			this.treeListColumn8.VisibleIndex = 2;
			this.treeListColumn8.Width = 112;
			// 
			// mnuHelperTreeEditVirtualProperty
			// 
			this.mnuHelperTreeEditVirtualProperty.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelperItemViewFunction,
            this.mnuHelperItemDelete,
            this.mnuItemNewExtensionMethod,
            this.mnuHelperItemNewVirtualProperty,
            this.editToolStripMenuItem});
			this.mnuHelperTreeEditVirtualProperty.Name = "mnuTabPage";
			this.mnuHelperTreeEditVirtualProperty.Size = new System.Drawing.Size(197, 136);
			this.mnuHelperTreeEditVirtualProperty.Opening += new System.ComponentModel.CancelEventHandler(this.mnuHelperTree_Opening);
			// 
			// mnuHelperItemViewFunction
			// 
			this.mnuHelperItemViewFunction.Image = ((System.Drawing.Image)(resources.GetObject("mnuHelperItemViewFunction.Image")));
			this.mnuHelperItemViewFunction.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.mnuHelperItemViewFunction.Name = "mnuHelperItemViewFunction";
			this.mnuHelperItemViewFunction.Size = new System.Drawing.Size(196, 22);
			this.mnuHelperItemViewFunction.Text = "&View function";
			this.mnuHelperItemViewFunction.ToolTipText = "View/edit the function that populates this file";
			this.mnuHelperItemViewFunction.Click += new System.EventHandler(this.mnuHelperItemViewFunction_Click);
			// 
			// mnuHelperItemDelete
			// 
			this.mnuHelperItemDelete.Image = ((System.Drawing.Image)(resources.GetObject("mnuHelperItemDelete.Image")));
			this.mnuHelperItemDelete.Name = "mnuHelperItemDelete";
			this.mnuHelperItemDelete.Size = new System.Drawing.Size(196, 22);
			this.mnuHelperItemDelete.Text = "&Delete";
			this.mnuHelperItemDelete.Click += new System.EventHandler(this.mnuHelperItemDelete_Click);
			// 
			// mnuHelperItemNewVirtualProperty
			// 
			this.mnuHelperItemNewVirtualProperty.Image = ((System.Drawing.Image)(resources.GetObject("mnuHelperItemNewVirtualProperty.Image")));
			this.mnuHelperItemNewVirtualProperty.Name = "mnuHelperItemNewVirtualProperty";
			this.mnuHelperItemNewVirtualProperty.Size = new System.Drawing.Size(196, 22);
			this.mnuHelperItemNewVirtualProperty.Text = "&New virtual property";
			this.mnuHelperItemNewVirtualProperty.Click += new System.EventHandler(this.mnuHelperItemNewVirtualProperty_Click);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("editToolStripMenuItem.Image")));
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.editToolStripMenuItem.Text = "&Edit";
			this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
			// 
			// expandableSplitter1
			// 
			this.expandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
			this.expandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.expandableSplitter1.Dock = System.Windows.Forms.DockStyle.Top;
			this.expandableSplitter1.ExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
			this.expandableSplitter1.ExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandableSplitter1.ExpandLineColor = System.Drawing.SystemColors.ControlText;
			this.expandableSplitter1.ExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.expandableSplitter1.GripDarkColor = System.Drawing.SystemColors.ControlText;
			this.expandableSplitter1.GripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.expandableSplitter1.GripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
			this.expandableSplitter1.GripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
			this.expandableSplitter1.HotBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(142)))), ((int)(((byte)(75)))));
			this.expandableSplitter1.HotBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(207)))), ((int)(((byte)(139)))));
			this.expandableSplitter1.HotBackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2;
			this.expandableSplitter1.HotBackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground;
			this.expandableSplitter1.HotExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
			this.expandableSplitter1.HotExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandableSplitter1.HotExpandLineColor = System.Drawing.SystemColors.ControlText;
			this.expandableSplitter1.HotExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.expandableSplitter1.HotGripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
			this.expandableSplitter1.HotGripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandableSplitter1.HotGripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
			this.expandableSplitter1.HotGripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
			this.expandableSplitter1.Location = new System.Drawing.Point(0, 252);
			this.expandableSplitter1.Name = "expandableSplitter1";
			this.expandableSplitter1.Size = new System.Drawing.Size(756, 7);
			this.expandableSplitter1.TabIndex = 20;
			this.expandableSplitter1.TabStop = false;
			this.expandableSplitter1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.expandableSplitter1_SplitterMoved);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.ucHeading1);
			this.panel1.Controls.Add(this.treeListAPIHelper);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 259);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(756, 277);
			this.panel1.TabIndex = 21;
			// 
			// mnuItemNewExtensionMethod
			// 
			this.mnuItemNewExtensionMethod.Image = ((System.Drawing.Image)(resources.GetObject("mnuItemNewExtensionMethod.Image")));
			this.mnuItemNewExtensionMethod.Name = "mnuItemNewExtensionMethod";
			this.mnuItemNewExtensionMethod.Size = new System.Drawing.Size(196, 22);
			this.mnuItemNewExtensionMethod.Text = "New Extension Method";
			this.mnuItemNewExtensionMethod.Click += new System.EventHandler(this.mnuItem_newExtensionMethod_Click);
			// 
			// ucUserOptionsAPI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.expandableSplitter1);
			this.Controls.Add(this.treeListAPI);
			this.Controls.Add(this.ucLabel1);
			this.Name = "ucUserOptionsAPI";
			this.Size = new System.Drawing.Size(756, 536);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.ucUserOptionsAPI_Paint);
			this.Resize += new System.EventHandler(this.ucUserOptionsAPI_Resize);
			((System.ComponentModel.ISupportInitialize)(this.treeListAPI)).EndInit();
			this.mnuTreeNode.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.treeListAPIHelper)).EndInit();
			this.mnuHelperTreeEditVirtualProperty.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private Slyce.Common.Controls.ucHeading ucLabel1;
        private DevExpress.XtraTreeList.TreeList treeListAPI;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private System.Windows.Forms.ContextMenuStrip mnuTreeNode;
        private System.Windows.Forms.ToolStripMenuItem mnuItemDelete;
        private System.Windows.Forms.ToolStripSeparator mnuTreeNodeSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuItemViewFunction;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripMenuItem mnuItemNewProperty;
        private System.Windows.Forms.ToolStripMenuItem mnuItemNewUserOption;
		private System.Windows.Forms.ToolStripMenuItem mnuItemEdit;
        private DevExpress.XtraTreeList.TreeList treeListAPIHelper;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn5;
        private System.Windows.Forms.ContextMenuStrip mnuHelperTreeEditVirtualProperty;
        private System.Windows.Forms.ToolStripMenuItem mnuHelperItemViewFunction;
        private System.Windows.Forms.ToolStripMenuItem mnuHelperItemDelete;
        private Slyce.Common.Controls.ucHeading ucHeading1;
        private System.Windows.Forms.ToolStripMenuItem mnuHelperItemNewVirtualProperty;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn6;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn7;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn8;
		private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ToolStripMenuItem mnuItemNewExtensionMethod;
    }
}
