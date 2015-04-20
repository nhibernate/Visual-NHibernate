namespace ArchAngel.Designer.Wizards.TemplateSync
{
    partial class ScreenFunctions
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScreenFunctions));
			ActiproSoftware.SyntaxEditor.Document document2 = new ActiproSoftware.SyntaxEditor.Document();
			ActiproSoftware.SyntaxEditor.Document document1 = new ActiproSoftware.SyntaxEditor.Document();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.imageListTreeNodeStates = new System.Windows.Forms.ImageList(this.components);
			this.treeListReferencedFiles = new DevExpress.XtraTreeList.TreeList();
			this.treeListColumn6 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.treeListColumn7 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.treeListColumn8 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.repositoryItemPictureEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
			this.treeListColumn9 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.treeListColumn10 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.repositoryItemButtonEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.repositoryItemButtonEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
			this.imageList2 = new System.Windows.Forms.ImageList(this.components);
			this.imageList3 = new System.Windows.Forms.ImageList(this.components);
			this.dockManager1 = new ActiproSoftware.UIStudio.Dock.DockManager(this.components);
			this.syntaxEditorExternal = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
			this.splitContainerMain = new System.Windows.Forms.SplitContainer();
			this.btnTest = new System.Windows.Forms.Button();
			this.syntaxEditorCurrent = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
			this.buttonResolve = new System.Windows.Forms.Button();
			this.ucHeading1 = new Slyce.Common.Controls.ucHeading();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mnuAccept = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.treeListReferencedFiles)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
			this.splitContainerMain.Panel1.SuspendLayout();
			this.splitContainerMain.Panel2.SuspendLayout();
			this.splitContainerMain.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
			this.imageList1.Images.SetKeyName(0, "");
			this.imageList1.Images.SetKeyName(1, "");
			this.imageList1.Images.SetKeyName(2, "");
			// 
			// imageListTreeNodeStates
			// 
			this.imageListTreeNodeStates.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTreeNodeStates.ImageStream")));
			this.imageListTreeNodeStates.TransparentColor = System.Drawing.Color.Magenta;
			this.imageListTreeNodeStates.Images.SetKeyName(0, "");
			this.imageListTreeNodeStates.Images.SetKeyName(1, "");
			this.imageListTreeNodeStates.Images.SetKeyName(2, "");
			// 
			// treeListReferencedFiles
			// 
			this.treeListReferencedFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.treeListReferencedFiles.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn6,
            this.treeListColumn7,
            this.treeListColumn8,
            this.treeListColumn9,
            this.treeListColumn10});
			this.treeListReferencedFiles.Location = new System.Drawing.Point(8, 10);
			this.treeListReferencedFiles.MinWidth = 10;
			this.treeListReferencedFiles.Name = "treeListReferencedFiles";
			this.treeListReferencedFiles.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.treeListReferencedFiles.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPictureEdit4,
            this.repositoryItemButtonEdit4,
            this.repositoryItemButtonEdit5,
            this.repositoryItemPictureEdit1});
			this.treeListReferencedFiles.Size = new System.Drawing.Size(823, 293);
			this.treeListReferencedFiles.TabIndex = 13;
			this.treeListReferencedFiles.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.treeListReferencedFiles_NodeCellStyle);
			this.treeListReferencedFiles.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeListReferencedFiles_MouseDown);
			this.treeListReferencedFiles.CustomNodeCellEdit += new DevExpress.XtraTreeList.GetCustomNodeCellEditEventHandler(this.treeListReferencedFiles_CustomNodeCellEdit);
			this.treeListReferencedFiles.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeListReferencedFiles_FocusedNodeChanged);
			// 
			// treeListColumn6
			// 
			this.treeListColumn6.Caption = "Setting";
			this.treeListColumn6.FieldName = "Name";
			this.treeListColumn6.MinWidth = 10;
			this.treeListColumn6.Name = "treeListColumn6";
			this.treeListColumn6.OptionsColumn.AllowEdit = false;
			this.treeListColumn6.OptionsColumn.AllowSort = false;
			this.treeListColumn6.Visible = true;
			this.treeListColumn6.VisibleIndex = 0;
			this.treeListColumn6.Width = 184;
			// 
			// treeListColumn7
			// 
			this.treeListColumn7.AppearanceCell.Options.UseTextOptions = true;
			this.treeListColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.treeListColumn7.AppearanceHeader.Options.UseTextOptions = true;
			this.treeListColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.treeListColumn7.Caption = "Their Value";
			this.treeListColumn7.FieldName = "Their Value";
			this.treeListColumn7.MinWidth = 10;
			this.treeListColumn7.Name = "treeListColumn7";
			this.treeListColumn7.OptionsColumn.AllowEdit = false;
			this.treeListColumn7.OptionsColumn.AllowSort = false;
			this.treeListColumn7.Visible = true;
			this.treeListColumn7.VisibleIndex = 1;
			this.treeListColumn7.Width = 183;
			// 
			// treeListColumn8
			// 
			this.treeListColumn8.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
			this.treeListColumn8.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
			this.treeListColumn8.AppearanceCell.Options.UseFont = true;
			this.treeListColumn8.AppearanceCell.Options.UseForeColor = true;
			this.treeListColumn8.AppearanceCell.Options.UseTextOptions = true;
			this.treeListColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.treeListColumn8.ColumnEdit = this.repositoryItemPictureEdit4;
			this.treeListColumn8.FieldName = "Action";
			this.treeListColumn8.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.treeListColumn8.MinWidth = 10;
			this.treeListColumn8.Name = "treeListColumn8";
			this.treeListColumn8.OptionsColumn.AllowEdit = false;
			this.treeListColumn8.OptionsColumn.AllowSize = false;
			this.treeListColumn8.OptionsColumn.AllowSort = false;
			this.treeListColumn8.OptionsColumn.FixedWidth = true;
			this.treeListColumn8.Visible = true;
			this.treeListColumn8.VisibleIndex = 2;
			this.treeListColumn8.Width = 30;
			// 
			// repositoryItemPictureEdit4
			// 
			this.repositoryItemPictureEdit4.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("repositoryItemPictureEdit4.Appearance.Image")));
			this.repositoryItemPictureEdit4.Appearance.Options.UseImage = true;
			this.repositoryItemPictureEdit4.Name = "repositoryItemPictureEdit4";
			// 
			// treeListColumn9
			// 
			this.treeListColumn9.AppearanceCell.Options.UseTextOptions = true;
			this.treeListColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.treeListColumn9.AppearanceHeader.Options.UseTextOptions = true;
			this.treeListColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.treeListColumn9.Caption = "My Value";
			this.treeListColumn9.FieldName = "My Value";
			this.treeListColumn9.MinWidth = 10;
			this.treeListColumn9.Name = "treeListColumn9";
			this.treeListColumn9.OptionsColumn.AllowEdit = false;
			this.treeListColumn9.OptionsColumn.AllowSort = false;
			this.treeListColumn9.Visible = true;
			this.treeListColumn9.VisibleIndex = 3;
			this.treeListColumn9.Width = 207;
			// 
			// treeListColumn10
			// 
			this.treeListColumn10.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
			this.treeListColumn10.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
			this.treeListColumn10.AppearanceCell.Options.UseFont = true;
			this.treeListColumn10.AppearanceCell.Options.UseForeColor = true;
			this.treeListColumn10.AppearanceCell.Options.UseTextOptions = true;
			this.treeListColumn10.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.treeListColumn10.ColumnEdit = this.repositoryItemPictureEdit4;
			this.treeListColumn10.MinWidth = 10;
			this.treeListColumn10.Name = "treeListColumn10";
			this.treeListColumn10.OptionsColumn.AllowEdit = false;
			this.treeListColumn10.OptionsColumn.AllowSize = false;
			this.treeListColumn10.OptionsColumn.AllowSort = false;
			this.treeListColumn10.OptionsColumn.FixedWidth = true;
			this.treeListColumn10.Visible = true;
			this.treeListColumn10.VisibleIndex = 4;
			this.treeListColumn10.Width = 30;
			// 
			// repositoryItemButtonEdit4
			// 
			this.repositoryItemButtonEdit4.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("repositoryItemButtonEdit4.Appearance.Image")));
			this.repositoryItemButtonEdit4.Appearance.Options.UseImage = true;
			this.repositoryItemButtonEdit4.AutoHeight = false;
			this.repositoryItemButtonEdit4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.repositoryItemButtonEdit4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", 30, true, true, false, DevExpress.Utils.HorzAlignment.Center, ((System.Drawing.Image)(resources.GetObject("repositoryItemButtonEdit4.Buttons"))))});
			this.repositoryItemButtonEdit4.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.repositoryItemButtonEdit4.HideSelection = false;
			this.repositoryItemButtonEdit4.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
			this.repositoryItemButtonEdit4.LookAndFeel.UseDefaultLookAndFeel = false;
			this.repositoryItemButtonEdit4.LookAndFeel.UseWindowsXPTheme = true;
			this.repositoryItemButtonEdit4.Name = "repositoryItemButtonEdit4";
			this.repositoryItemButtonEdit4.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			// 
			// repositoryItemButtonEdit5
			// 
			this.repositoryItemButtonEdit5.AutoHeight = false;
			this.repositoryItemButtonEdit5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.repositoryItemButtonEdit5.Name = "repositoryItemButtonEdit5";
			// 
			// repositoryItemPictureEdit1
			// 
			this.repositoryItemPictureEdit1.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("repositoryItemPictureEdit1.Appearance.Image")));
			this.repositoryItemPictureEdit1.Appearance.Options.UseImage = true;
			this.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
			// 
			// imageList2
			// 
			this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
			this.imageList2.TransparentColor = System.Drawing.Color.Magenta;
			this.imageList2.Images.SetKeyName(0, "");
			this.imageList2.Images.SetKeyName(1, "");
			this.imageList2.Images.SetKeyName(2, "");
			// 
			// imageList3
			// 
			this.imageList3.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList3.ImageStream")));
			this.imageList3.TransparentColor = System.Drawing.Color.Magenta;
			this.imageList3.Images.SetKeyName(0, "Input.bmp");
			this.imageList3.Images.SetKeyName(1, "delete_16x.ico");
			// 
			// dockManager1
			// 
			this.dockManager1.HostContainerControl = this;
			// 
			// syntaxEditorExternal
			// 
			this.syntaxEditorExternal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
			this.syntaxEditorExternal.Document = document2;
			this.syntaxEditorExternal.LineNumberMarginVisible = true;
			this.syntaxEditorExternal.Location = new System.Drawing.Point(8, 49);
			this.syntaxEditorExternal.Margin = new System.Windows.Forms.Padding(2);
			this.syntaxEditorExternal.Name = "syntaxEditorExternal";
			this.syntaxEditorExternal.Size = new System.Drawing.Size(403, 226);
			this.syntaxEditorExternal.TabIndex = 1;
			this.syntaxEditorExternal.DocumentTextChanged += new ActiproSoftware.SyntaxEditor.DocumentModificationEventHandler(this.syntaxEditor1_DocumentTextChanged);
			this.syntaxEditorExternal.ContextMenuRequested += new ActiproSoftware.SyntaxEditor.ContextMenuRequestEventHandler(this.syntaxEditor1_ContextMenuRequested);
			this.syntaxEditorExternal.ViewVerticalScroll += new ActiproSoftware.SyntaxEditor.EditorViewEventHandler(this.syntaxEditorExternal_ViewVerticalScroll);
			this.syntaxEditorExternal.Resize += new System.EventHandler(this.syntaxEditorExternal_Resize);
			this.syntaxEditorExternal.UserMarginPaint += new ActiproSoftware.SyntaxEditor.UserMarginPaintEventHandler(this.syntaxEditor1_UserMarginPaint);
			this.syntaxEditorExternal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.syntaxEditor1_MouseDown);
			// 
			// splitContainerMain
			// 
			this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
			this.splitContainerMain.Name = "splitContainerMain";
			this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainerMain.Panel1
			// 
			this.splitContainerMain.Panel1.Controls.Add(this.treeListReferencedFiles);
			// 
			// splitContainerMain.Panel2
			// 
			this.splitContainerMain.Panel2.Controls.Add(this.btnTest);
			this.splitContainerMain.Panel2.Controls.Add(this.syntaxEditorCurrent);
			this.splitContainerMain.Panel2.Controls.Add(this.buttonResolve);
			this.splitContainerMain.Panel2.Controls.Add(this.syntaxEditorExternal);
			this.splitContainerMain.Panel2.Controls.Add(this.ucHeading1);
			this.splitContainerMain.Panel2.Resize += new System.EventHandler(this.splitContainerMain_Panel2_Resize);
			this.splitContainerMain.Size = new System.Drawing.Size(845, 605);
			this.splitContainerMain.SplitterDistance = 306;
			this.splitContainerMain.TabIndex = 5;
			// 
			// btnTest
			// 
			this.btnTest.Location = new System.Drawing.Point(152, 4);
			this.btnTest.Name = "btnTest";
			this.btnTest.Size = new System.Drawing.Size(75, 23);
			this.btnTest.TabIndex = 36;
			this.btnTest.Text = "Test";
			this.btnTest.UseVisualStyleBackColor = true;
			this.btnTest.Visible = false;
			this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
			// 
			// syntaxEditorCurrent
			// 
			this.syntaxEditorCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
			this.syntaxEditorCurrent.Document = document1;
			this.syntaxEditorCurrent.LineNumberMarginVisible = true;
			this.syntaxEditorCurrent.Location = new System.Drawing.Point(454, 49);
			this.syntaxEditorCurrent.Margin = new System.Windows.Forms.Padding(2);
			this.syntaxEditorCurrent.Name = "syntaxEditorCurrent";
			this.syntaxEditorCurrent.Size = new System.Drawing.Size(349, 224);
			this.syntaxEditorCurrent.TabIndex = 35;
			this.syntaxEditorCurrent.ViewVerticalScroll += new ActiproSoftware.SyntaxEditor.EditorViewEventHandler(this.syntaxEditorCurrent_ViewVerticalScroll);
			this.syntaxEditorCurrent.Resize += new System.EventHandler(this.syntaxEditorCurrent_Resize);
			// 
			// buttonResolve
			// 
			this.buttonResolve.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonResolve.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonResolve.ImageIndex = 2;
			this.buttonResolve.ImageList = this.imageList1;
			this.buttonResolve.Location = new System.Drawing.Point(712, 4);
			this.buttonResolve.Name = "buttonResolve";
			this.buttonResolve.Size = new System.Drawing.Size(130, 25);
			this.buttonResolve.TabIndex = 28;
			this.buttonResolve.Text = "      Mark as resolved";
			this.buttonResolve.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonResolve.UseVisualStyleBackColor = true;
			this.buttonResolve.Click += new System.EventHandler(this.buttonResolve_Click);
			// 
			// ucHeading1
			// 
			this.ucHeading1.Dock = System.Windows.Forms.DockStyle.Top;
			this.ucHeading1.Location = new System.Drawing.Point(0, 0);
			this.ucHeading1.Name = "ucHeading1";
			this.ucHeading1.Size = new System.Drawing.Size(845, 33);
			this.ucHeading1.TabIndex = 34;
			this.ucHeading1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAccept,
            this.mnuDelete});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(176, 48);
			// 
			// mnuAccept
			// 
			this.mnuAccept.Image = ((System.Drawing.Image)(resources.GetObject("mnuAccept.Image")));
			this.mnuAccept.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.mnuAccept.Name = "mnuAccept";
			this.mnuAccept.Size = new System.Drawing.Size(175, 22);
			this.mnuAccept.Text = "&Apply this change";
			this.mnuAccept.Click += new System.EventHandler(this.mnuAccept_Click);
			// 
			// mnuDelete
			// 
			this.mnuDelete.Image = ((System.Drawing.Image)(resources.GetObject("mnuDelete.Image")));
			this.mnuDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.mnuDelete.Name = "mnuDelete";
			this.mnuDelete.Size = new System.Drawing.Size(175, 22);
			this.mnuDelete.Text = "&Ignore this change";
			this.mnuDelete.Click += new System.EventHandler(this.mnuDelete_Click);
			// 
			// ScreenFunctions
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainerMain);
			this.Name = "ScreenFunctions";
			this.Size = new System.Drawing.Size(845, 605);
			this.Resize += new System.EventHandler(this.ScreenFunctions_Resize);
			((System.ComponentModel.ISupportInitialize)(this.treeListReferencedFiles)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
			this.splitContainerMain.Panel1.ResumeLayout(false);
			this.splitContainerMain.Panel2.ResumeLayout(false);
			this.splitContainerMain.ResumeLayout(false);
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ImageList imageListTreeNodeStates;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.ImageList imageList3;
        private ActiproSoftware.UIStudio.Dock.DockManager dockManager1;
		private ActiproSoftware.SyntaxEditor.SyntaxEditor syntaxEditorExternal;
		private System.Windows.Forms.SplitContainer splitContainerMain;
		private System.Windows.Forms.Button buttonResolve;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuAccept;
		private System.Windows.Forms.ToolStripMenuItem mnuDelete;
        private Slyce.Common.Controls.ucHeading ucHeading1;
		private DevExpress.XtraTreeList.TreeList treeListReferencedFiles;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn6;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn7;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn8;
		private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit4;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn9;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn10;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit4;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit5;
		private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit1;
		private ActiproSoftware.SyntaxEditor.SyntaxEditor syntaxEditorCurrent;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Button btnTest;
    }
}
