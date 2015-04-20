namespace ArchAngel.Designer.Wizards.TemplateSync
{
    partial class ScreenProjectDetails
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScreenProjectDetails));
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
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.imageListTreeNodeStates = new System.Windows.Forms.ImageList(this.components);
			((System.ComponentModel.ISupportInitialize)(this.treeListReferencedFiles)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).BeginInit();
			this.SuspendLayout();
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
			this.treeListReferencedFiles.Location = new System.Drawing.Point(14, 34);
			this.treeListReferencedFiles.MinWidth = 10;
			this.treeListReferencedFiles.Name = "treeListReferencedFiles";
			this.treeListReferencedFiles.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.treeListReferencedFiles.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPictureEdit4,
            this.repositoryItemButtonEdit4,
            this.repositoryItemButtonEdit5,
            this.repositoryItemPictureEdit1});
			this.treeListReferencedFiles.Size = new System.Drawing.Size(863, 513);
			this.treeListReferencedFiles.TabIndex = 11;
			this.treeListReferencedFiles.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.treeListReferencedFiles_NodeCellStyle);
			this.treeListReferencedFiles.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeListReferencedFiles_MouseDown);
			this.treeListReferencedFiles.CustomNodeCellEdit += new DevExpress.XtraTreeList.GetCustomNodeCellEditEventHandler(this.treeListReferencedFiles_CustomNodeCellEdit);
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
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
			this.imageList1.Images.SetKeyName(0, "Input.bmp");
			this.imageList1.Images.SetKeyName(1, "delete_16x.ico");
			// 
			// imageListTreeNodeStates
			// 
			this.imageListTreeNodeStates.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTreeNodeStates.ImageStream")));
			this.imageListTreeNodeStates.TransparentColor = System.Drawing.Color.Magenta;
			this.imageListTreeNodeStates.Images.SetKeyName(0, "");
			this.imageListTreeNodeStates.Images.SetKeyName(1, "");
			this.imageListTreeNodeStates.Images.SetKeyName(2, "");
			// 
			// ScreenProjectDetails
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.treeListReferencedFiles);
			this.Name = "ScreenProjectDetails";
			this.Size = new System.Drawing.Size(892, 561);
			((System.ComponentModel.ISupportInitialize)(this.treeListReferencedFiles)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.ImageList imageListTreeNodeStates;
		private System.Windows.Forms.ImageList imageList1;
		private DevExpress.XtraTreeList.TreeList treeListReferencedFiles;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn6;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn7;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn8;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit4;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn9;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn10;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit5;
		private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit4;
		private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit1;

    }
}
