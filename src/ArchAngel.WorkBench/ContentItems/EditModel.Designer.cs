namespace ArchAngel.Workbench.ContentItems
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
            this.labelDatabaseFeedback = new System.Windows.Forms.Label();
            this.imageListButton = new System.Windows.Forms.ImageList(this.components);
            this.buttonSetupModel = new System.Windows.Forms.Button();
            this.buttonRefreshDatabase = new System.Windows.Forms.Button();
            this.buttonReloadTreeListView = new System.Windows.Forms.Button();
            this.treeList = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumnAlias = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumnName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumnNote = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imageListTreeList = new System.Windows.Forms.ImageList(this.components);
            this.checkBoxKeepChanges = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btnResetDefaultValues = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.contextMenuStripTreeView.SuspendLayout();
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
            this.toolStripMenuItemTreeViewDelete});
            this.contextMenuStripTreeView.Name = "contextMenuStripTreeView";
            this.contextMenuStripTreeView.Size = new System.Drawing.Size(166, 120);
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
            // labelDatabaseFeedback
            // 
            this.labelDatabaseFeedback.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDatabaseFeedback.AutoEllipsis = true;
            this.labelDatabaseFeedback.BackColor = System.Drawing.Color.Transparent;
            this.labelDatabaseFeedback.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDatabaseFeedback.Location = new System.Drawing.Point(1, 504);
            this.labelDatabaseFeedback.Name = "labelDatabaseFeedback";
            this.labelDatabaseFeedback.Size = new System.Drawing.Size(907, 45);
            this.labelDatabaseFeedback.TabIndex = 32;
            // 
            // imageListButton
            // 
            this.imageListButton.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListButton.ImageStream")));
            this.imageListButton.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListButton.Images.SetKeyName(0, "RefreshDocViewHS.png");
            this.imageListButton.Images.SetKeyName(1, "NewDocumentHS.png");
            this.imageListButton.Images.SetKeyName(2, "services.ico");
            // 
            // buttonSetupModel
            // 
            this.buttonSetupModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSetupModel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSetupModel.ImageIndex = 2;
            this.buttonSetupModel.ImageList = this.imageListButton;
            this.buttonSetupModel.Location = new System.Drawing.Point(535, 6);
            this.buttonSetupModel.Name = "buttonSetupModel";
            this.buttonSetupModel.Size = new System.Drawing.Size(93, 23);
            this.buttonSetupModel.TabIndex = 30;
            this.buttonSetupModel.Text = "    Setup Model";
            this.toolTip1.SetToolTip(this.buttonSetupModel, "Apply the rules from the Update Model AAL file to the database schema");
            this.buttonSetupModel.UseVisualStyleBackColor = true;
            this.buttonSetupModel.Visible = false;
            // 
            // buttonRefreshDatabase
            // 
            this.buttonRefreshDatabase.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonRefreshDatabase.ImageIndex = 0;
            this.buttonRefreshDatabase.ImageList = this.imageListButton;
            this.buttonRefreshDatabase.Location = new System.Drawing.Point(4, 7);
            this.buttonRefreshDatabase.Name = "buttonRefreshDatabase";
            this.buttonRefreshDatabase.Size = new System.Drawing.Size(93, 23);
            this.buttonRefreshDatabase.TabIndex = 31;
            this.buttonRefreshDatabase.Text = "    Refresh DB";
            this.buttonRefreshDatabase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.buttonRefreshDatabase, "Refresh the database schema, keeping any changes you have made");
            this.buttonRefreshDatabase.UseVisualStyleBackColor = true;
            this.buttonRefreshDatabase.Click += new System.EventHandler(this.buttonRefreshDatabase_Click);
            // 
            // buttonReloadTreeListView
            // 
            this.buttonReloadTreeListView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReloadTreeListView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonReloadTreeListView.ImageIndex = 2;
            this.buttonReloadTreeListView.ImageList = this.imageListButton;
            this.buttonReloadTreeListView.Location = new System.Drawing.Point(506, 6);
            this.buttonReloadTreeListView.Name = "buttonReloadTreeListView";
            this.buttonReloadTreeListView.Size = new System.Drawing.Size(23, 23);
            this.buttonReloadTreeListView.TabIndex = 35;
            this.buttonReloadTreeListView.Text = "    Setup Model";
            this.toolTip1.SetToolTip(this.buttonReloadTreeListView, "Apply the rules from the Update Model AAL file to the database schema");
            this.buttonReloadTreeListView.UseVisualStyleBackColor = true;
            this.buttonReloadTreeListView.Visible = false;
            this.buttonReloadTreeListView.Click += new System.EventHandler(this.buttonReloadTreeListView_Click);
            // 
            // treeList
            // 
            this.treeList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumnAlias,
            this.treeListColumnName,
            this.treeListColumnNote});
            this.treeList.ColumnsImageList = this.imageListTreeList;
            this.treeList.Location = new System.Drawing.Point(4, 33);
            this.treeList.Name = "treeList";
            this.treeList.OptionsView.AutoWidth = false;
            this.treeList.SelectImageList = this.imageListTreeList;
            this.treeList.Size = new System.Drawing.Size(904, 468);
            this.treeList.StateImageList = this.imageListTreeList;
            this.treeList.TabIndex = 34;
            this.treeList.GetStateImage += new DevExpress.XtraTreeList.GetStateImageEventHandler(this.treeList_GetStateImage);
            this.treeList.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.treeList_NodeCellStyle);
            this.treeList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeList_MouseUp);
            this.treeList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeList_MouseDoubleClick);
            this.treeList.CustomNodeCellEdit += new DevExpress.XtraTreeList.GetCustomNodeCellEditEventHandler(this.treeList_CustomNodeCellEdit);
            this.treeList.Resize += new System.EventHandler(this.treeList_Resize);
            // 
            // treeListColumnAlias
            // 
            this.treeListColumnAlias.Caption = "Alias";
            this.treeListColumnAlias.FieldName = "Alias";
            this.treeListColumnAlias.MinWidth = 43;
            this.treeListColumnAlias.Name = "treeListColumnAlias";
            this.treeListColumnAlias.OptionsColumn.AllowEdit = false;
            this.treeListColumnAlias.OptionsColumn.ReadOnly = true;
            this.treeListColumnAlias.VisibleIndex = 0;
            this.treeListColumnAlias.Width = 192;
            // 
            // treeListColumnName
            // 
            this.treeListColumnName.Caption = "Name";
            this.treeListColumnName.FieldName = "Name";
            this.treeListColumnName.Name = "treeListColumnName";
            this.treeListColumnName.OptionsColumn.ReadOnly = true;
            this.treeListColumnName.VisibleIndex = 1;
            this.treeListColumnName.Width = 105;
            // 
            // treeListColumnNote
            // 
            this.treeListColumnNote.Caption = "Notes";
            this.treeListColumnNote.FieldName = "Note";
            this.treeListColumnNote.Name = "treeListColumnNote";
            this.treeListColumnNote.OptionsColumn.AllowEdit = false;
            this.treeListColumnNote.OptionsColumn.ReadOnly = true;
            this.treeListColumnNote.VisibleIndex = 2;
            this.treeListColumnNote.Width = 156;
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
            // 
            // checkBoxKeepChanges
            // 
            this.checkBoxKeepChanges.AutoSize = true;
            this.checkBoxKeepChanges.Checked = true;
            this.checkBoxKeepChanges.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxKeepChanges.Location = new System.Drawing.Point(102, 11);
            this.checkBoxKeepChanges.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxKeepChanges.Name = "checkBoxKeepChanges";
            this.checkBoxKeepChanges.Size = new System.Drawing.Size(173, 17);
            this.checkBoxKeepChanges.TabIndex = 33;
            this.checkBoxKeepChanges.Text = "Keep changes when refreshing";
            this.toolTip1.SetToolTip(this.checkBoxKeepChanges, "Check to keep any naming changes etc you have made when refreshing the database s" +
                    "chema again. Uncheck to lose name changes and start with a blank slate again.");
            this.checkBoxKeepChanges.UseVisualStyleBackColor = true;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
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
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(347, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 24);
            this.button1.TabIndex = 38;
            this.button1.Text = "     Check Validity";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // EditModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelDatabaseFeedback);
            this.Controls.Add(this.treeList);
            this.Controls.Add(this.btnResetDefaultValues);
            this.Controls.Add(this.buttonReloadTreeListView);
            this.Controls.Add(this.checkBoxKeepChanges);
            this.Controls.Add(this.buttonRefreshDatabase);
            this.Controls.Add(this.buttonSetupModel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "EditModel";
            this.Size = new System.Drawing.Size(911, 549);
            this.contextMenuStripTreeView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStripTreeView;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTreeViewAdd;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTreeViewAddMapColumn;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTreeViewEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTreeViewDelete;
        private System.Windows.Forms.Label labelDatabaseFeedback;
        private System.Windows.Forms.Button buttonSetupModel;
        private System.Windows.Forms.Button buttonRefreshDatabase;
        private System.Windows.Forms.ImageList imageListButton;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox checkBoxKeepChanges;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private DevExpress.XtraTreeList.TreeList treeList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnAlias;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnNote;
        private System.Windows.Forms.ImageList imageListTreeList;
        private System.Windows.Forms.Button buttonReloadTreeListView;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTreeViewExecuteStoredProcedure;
        private System.Windows.Forms.Button btnResetDefaultValues;
        private System.Windows.Forms.Button button1;
    }
}
