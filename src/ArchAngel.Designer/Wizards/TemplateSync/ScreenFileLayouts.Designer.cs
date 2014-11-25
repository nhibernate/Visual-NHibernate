namespace ArchAngel.Designer.Wizards.TemplateSync
{
    partial class ScreenFileLayouts
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScreenFileLayouts));
			this.imageListTreeNodeStates = new System.Windows.Forms.ImageList(this.components);
			this.tgv = new DevExpress.XtraTreeList.TreeList();
			this.colFile = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.colFunction = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.colIterate = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
			this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.repositoryItemPictureEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
			this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.repositoryItemProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
			this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
			this.ddlTreeListFunctions = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			((System.ComponentModel.ISupportInitialize)(this.tgv)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ddlTreeListFunctions)).BeginInit();
			this.SuspendLayout();
			// 
			// imageListTreeNodeStates
			// 
			this.imageListTreeNodeStates.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTreeNodeStates.ImageStream")));
			this.imageListTreeNodeStates.TransparentColor = System.Drawing.Color.Magenta;
			this.imageListTreeNodeStates.Images.SetKeyName(0, "");
			this.imageListTreeNodeStates.Images.SetKeyName(1, "");
			this.imageListTreeNodeStates.Images.SetKeyName(2, "");
			// 
			// tgv
			// 
			this.tgv.Appearance.FocusedCell.BackColor = System.Drawing.Color.Navy;
			this.tgv.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.Navy;
			this.tgv.Appearance.FocusedCell.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			this.tgv.Appearance.FocusedCell.Options.UseBackColor = true;
			this.tgv.Appearance.FocusedRow.BackColor = System.Drawing.Color.SteelBlue;
			this.tgv.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.Navy;
			this.tgv.Appearance.FocusedRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			this.tgv.Appearance.FocusedRow.Options.UseBackColor = true;
			this.tgv.Appearance.Row.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			this.tgv.Appearance.Row.Options.UseImage = true;
			this.tgv.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.tgv.Appearance.SelectedRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.tgv.Appearance.SelectedRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.tgv.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Silver;
			this.tgv.Appearance.SelectedRow.Options.UseBackColor = true;
			this.tgv.Appearance.SelectedRow.Options.UseBorderColor = true;
			this.tgv.Appearance.SelectedRow.Options.UseForeColor = true;
			this.tgv.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colFile,
            this.colFunction,
            this.colIterate,
            this.treeListColumn1,
            this.treeListColumn2});
			this.tgv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tgv.Location = new System.Drawing.Point(0, 0);
			this.tgv.Margin = new System.Windows.Forms.Padding(2);
			this.tgv.MinWidth = 10;
			this.tgv.Name = "tgv";
			this.tgv.BeginUnboundLoad();
			this.tgv.AppendNode(new object[] {
            null,
            null,
            null,
            null,
            null}, -1);
			this.tgv.AppendNode(new object[] {
            null,
            null,
            null,
            null,
            null}, 0);
			this.tgv.AppendNode(new object[] {
            null,
            null,
            null,
            null,
            null}, 0);
			this.tgv.EndUnboundLoad();
			this.tgv.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.tgv.OptionsView.AutoWidth = false;
			this.tgv.OptionsView.ShowIndicator = false;
			this.tgv.OptionsView.ShowRoot = false;
			this.tgv.OptionsView.ShowVertLines = false;
			this.tgv.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemTextEdit1,
            this.repositoryItemProgressBar1,
            this.repositoryItemComboBox1,
            this.ddlTreeListFunctions,
            this.repositoryItemPictureEdit1,
            this.repositoryItemPictureEdit2});
			this.tgv.SelectImageList = this.imageList1;
			this.tgv.ShowButtonMode = DevExpress.XtraTreeList.ShowButtonModeEnum.ShowAlways;
			this.tgv.Size = new System.Drawing.Size(839, 542);
			this.tgv.TabIndex = 8;
			this.tgv.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tgv_MouseDown);
			this.tgv.CustomNodeCellEdit += new DevExpress.XtraTreeList.GetCustomNodeCellEditEventHandler(this.tgv_CustomNodeCellEdit);
			this.tgv.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tgv_MouseMove);
			// 
			// colFile
			// 
			this.colFile.AppearanceCell.Options.UseImage = true;
			this.colFile.ImageAlignment = System.Drawing.StringAlignment.Center;
			this.colFile.MinWidth = 37;
			this.colFile.Name = "colFile";
			this.colFile.OptionsColumn.AllowEdit = false;
			this.colFile.OptionsColumn.AllowFocus = false;
			this.colFile.OptionsColumn.AllowSort = false;
			this.colFile.Visible = true;
			this.colFile.VisibleIndex = 0;
			this.colFile.Width = 225;
			// 
			// colFunction
			// 
			this.colFunction.AppearanceCell.Options.UseTextOptions = true;
			this.colFunction.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.colFunction.AppearanceHeader.Options.UseTextOptions = true;
			this.colFunction.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colFunction.Caption = "Their value";
			this.colFunction.FieldName = "Function";
			this.colFunction.MinWidth = 10;
			this.colFunction.Name = "colFunction";
			this.colFunction.OptionsColumn.AllowEdit = false;
			this.colFunction.OptionsColumn.AllowFocus = false;
			this.colFunction.OptionsColumn.AllowSort = false;
			this.colFunction.Visible = true;
			this.colFunction.VisibleIndex = 1;
			this.colFunction.Width = 280;
			// 
			// colIterate
			// 
			this.colIterate.ColumnEdit = this.repositoryItemPictureEdit1;
			this.colIterate.FieldName = "Iterate";
			this.colIterate.MinWidth = 10;
			this.colIterate.Name = "colIterate";
			this.colIterate.OptionsColumn.AllowEdit = false;
			this.colIterate.OptionsColumn.AllowFocus = false;
			this.colIterate.OptionsColumn.AllowSize = false;
			this.colIterate.OptionsColumn.AllowSort = false;
			this.colIterate.Visible = true;
			this.colIterate.VisibleIndex = 2;
			this.colIterate.Width = 30;
			// 
			// repositoryItemPictureEdit1
			// 
			this.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
			// 
			// treeListColumn1
			// 
			this.treeListColumn1.AppearanceCell.Options.UseTextOptions = true;
			this.treeListColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.treeListColumn1.AppearanceHeader.Options.UseTextOptions = true;
			this.treeListColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.treeListColumn1.Caption = "My value";
			this.treeListColumn1.FieldName = "My value";
			this.treeListColumn1.MinWidth = 10;
			this.treeListColumn1.Name = "treeListColumn1";
			this.treeListColumn1.OptionsColumn.AllowEdit = false;
			this.treeListColumn1.OptionsColumn.AllowSort = false;
			this.treeListColumn1.Visible = true;
			this.treeListColumn1.VisibleIndex = 3;
			this.treeListColumn1.Width = 147;
			// 
			// treeListColumn2
			// 
			this.treeListColumn2.ColumnEdit = this.repositoryItemPictureEdit2;
			this.treeListColumn2.MinWidth = 10;
			this.treeListColumn2.Name = "treeListColumn2";
			this.treeListColumn2.OptionsColumn.AllowEdit = false;
			this.treeListColumn2.OptionsColumn.AllowSize = false;
			this.treeListColumn2.Visible = true;
			this.treeListColumn2.VisibleIndex = 4;
			this.treeListColumn2.Width = 30;
			// 
			// repositoryItemPictureEdit2
			// 
			this.repositoryItemPictureEdit2.Name = "repositoryItemPictureEdit2";
			// 
			// repositoryItemCheckEdit1
			// 
			this.repositoryItemCheckEdit1.AutoHeight = false;
			this.repositoryItemCheckEdit1.Caption = "GFH";
			this.repositoryItemCheckEdit1.FullFocusRect = true;
			this.repositoryItemCheckEdit1.GlyphAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
			// 
			// repositoryItemTextEdit1
			// 
			this.repositoryItemTextEdit1.AutoHeight = false;
			this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
			// 
			// repositoryItemProgressBar1
			// 
			this.repositoryItemProgressBar1.Name = "repositoryItemProgressBar1";
			// 
			// repositoryItemComboBox1
			// 
			this.repositoryItemComboBox1.AutoHeight = false;
			this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
			// 
			// ddlTreeListFunctions
			// 
			this.ddlTreeListFunctions.AutoHeight = false;
			this.ddlTreeListFunctions.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.ddlTreeListFunctions.Name = "ddlTreeListFunctions";
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
			this.imageList1.Images.SetKeyName(6, "StopHS.png");
			// 
			// ScreenFileLayouts
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tgv);
			this.Name = "ScreenFileLayouts";
			this.Size = new System.Drawing.Size(839, 542);
			((System.ComponentModel.ISupportInitialize)(this.tgv)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ddlTreeListFunctions)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.ImageList imageListTreeNodeStates;
		private DevExpress.XtraTreeList.TreeList tgv;
		private DevExpress.XtraTreeList.Columns.TreeListColumn colFile;
		private DevExpress.XtraTreeList.Columns.TreeListColumn colFunction;
		private DevExpress.XtraTreeList.Columns.TreeListColumn colIterate;
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
		private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repositoryItemProgressBar1;
		private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
		private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox ddlTreeListFunctions;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
		private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit1;
		private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit2;
		private System.Windows.Forms.ImageList imageList1;
    }
}
