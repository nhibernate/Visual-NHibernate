namespace ArchAngel.Designer
{
    partial class ucUserOptionsHelper
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucUserOptionsHelper));
            this.ucLabel1 = new Slyce.Common.Controls.ucHeading();
            this.treeListAPIHelper = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn5 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.mnuHelperTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuHelperItemViewFunction = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelperItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.treeListAPIHelper)).BeginInit();
            this.mnuHelperTree.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucLabel1
            // 
            this.ucLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucLabel1.Location = new System.Drawing.Point(0, 0);
            this.ucLabel1.Margin = new System.Windows.Forms.Padding(2);
            this.ucLabel1.Name = "ucLabel1";
            this.ucLabel1.Size = new System.Drawing.Size(876, 25);
            this.ucLabel1.TabIndex = 8;
            // 
            // treeListAPIHelper
            // 
            this.treeListAPIHelper.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeListAPIHelper.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn4,
            this.treeListColumn5});
            this.treeListAPIHelper.ContextMenuStrip = this.mnuHelperTree;
            this.treeListAPIHelper.Location = new System.Drawing.Point(3, 30);
            this.treeListAPIHelper.Name = "treeListAPIHelper";
            this.treeListAPIHelper.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.treeListAPIHelper.SelectImageList = this.imageList1;
            this.treeListAPIHelper.Size = new System.Drawing.Size(751, 377);
            this.treeListAPIHelper.TabIndex = 16;
            this.treeListAPIHelper.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.treeListAPIHelper_NodeCellStyle);
            this.treeListAPIHelper.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeListAPIHelper_MouseDown);
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
            this.treeListColumn4.VisibleIndex = 0;
            this.treeListColumn4.Width = 263;
            // 
            // treeListColumn5
            // 
            this.treeListColumn5.Caption = "Override Function";
            this.treeListColumn5.FieldName = "Value";
            this.treeListColumn5.Name = "treeListColumn5";
            this.treeListColumn5.OptionsColumn.AllowEdit = false;
            this.treeListColumn5.OptionsColumn.AllowSort = false;
            this.treeListColumn5.VisibleIndex = 1;
            this.treeListColumn5.Width = 500;
            // 
            // mnuHelperTree
            // 
            this.mnuHelperTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelperItemViewFunction,
            this.mnuHelperItemDelete});
            this.mnuHelperTree.Name = "mnuTabPage";
            this.mnuHelperTree.Size = new System.Drawing.Size(153, 70);
            this.mnuHelperTree.Opening += new System.ComponentModel.CancelEventHandler(this.mnuHelperTree_Opening);
            // 
            // mnuHelperItemViewFunction
            // 
            this.mnuHelperItemViewFunction.Image = ((System.Drawing.Image)(resources.GetObject("mnuHelperItemViewFunction.Image")));
            this.mnuHelperItemViewFunction.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuHelperItemViewFunction.Name = "mnuHelperItemViewFunction";
            this.mnuHelperItemViewFunction.Size = new System.Drawing.Size(152, 22);
            this.mnuHelperItemViewFunction.Text = "&View function";
            this.mnuHelperItemViewFunction.ToolTipText = "View/edit the function that populates this file";
            this.mnuHelperItemViewFunction.Click += new System.EventHandler(this.mnuHelperItemViewFunction_Click);
            // 
            // mnuHelperItemDelete
            // 
            this.mnuHelperItemDelete.Name = "mnuHelperItemDelete";
            this.mnuHelperItemDelete.Size = new System.Drawing.Size(152, 22);
            this.mnuHelperItemDelete.Text = "&Delete";
            this.mnuHelperItemDelete.Click += new System.EventHandler(this.mnuHelperItemDelete_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList1.Images.SetKeyName(0, "VSObject_Class.bmp");
            this.imageList1.Images.SetKeyName(1, "VSObject_Properties.bmp");
            this.imageList1.Images.SetKeyName(2, "VSObject_Method.bmp");
            // 
            // ucUserOptionsHelper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeListAPIHelper);
            this.Controls.Add(this.ucLabel1);
            this.Name = "ucUserOptionsHelper";
            this.Size = new System.Drawing.Size(876, 410);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ucUserOptionsHelper_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.treeListAPIHelper)).EndInit();
            this.mnuHelperTree.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Slyce.Common.Controls.ucHeading ucLabel1;
        private DevExpress.XtraTreeList.TreeList treeListAPIHelper;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn5;
        private System.Windows.Forms.ContextMenuStrip mnuHelperTree;
        private System.Windows.Forms.ToolStripMenuItem mnuHelperItemViewFunction;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripMenuItem mnuHelperItemDelete;
    }
}
