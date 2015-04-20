namespace ArchAngel.Designer
{
	partial class ucUserOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucUserOptions));
            this.lstOptions = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.ucLabel1 = new Slyce.Common.Controls.ucHeading();
            this.mnuTreeNode = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuItemViewFunction = new System.Windows.Forms.ToolStripMenuItem();
            this.viewValidationFunctionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTreeNodeSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuTreeNodeSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuStrip1.SuspendLayout();
            this.mnuTreeNode.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstOptions
            // 
            this.lstOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstOptions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9});
            this.lstOptions.ContextMenuStrip = this.contextMenuStrip1;
            this.lstOptions.FullRowSelect = true;
            this.lstOptions.GridLines = true;
            this.lstOptions.HideSelection = false;
            this.lstOptions.Location = new System.Drawing.Point(20, 30);
            this.lstOptions.Margin = new System.Windows.Forms.Padding(2);
            this.lstOptions.Name = "lstOptions";
            this.lstOptions.Size = new System.Drawing.Size(784, 418);
            this.lstOptions.TabIndex = 1;
            this.lstOptions.UseCompatibleStateImageBehavior = false;
            this.lstOptions.View = System.Windows.Forms.View.Details;
            this.lstOptions.DoubleClick += new System.EventHandler(this.lstOptions_DoubleClick);
            this.lstOptions.SelectedIndexChanged += new System.EventHandler(this.lstOptions_SelectedIndexChanged);
            this.lstOptions.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstOptions_KeyDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Variable Name";
            this.columnHeader1.Width = 137;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Type";
            this.columnHeader2.Width = 92;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Text";
            this.columnHeader3.Width = 209;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Description";
            this.columnHeader4.Width = 143;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Enums";
            this.columnHeader5.Width = 100;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Category";
            this.columnHeader6.Width = 103;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Default Value";
            this.columnHeader7.Width = 113;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Iterate Over...";
            this.columnHeader8.Width = 123;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Validator Function";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.removeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 48);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("editToolStripMenuItem.Image")));
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.editToolStripMenuItem.Text = "&Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("removeToolStripMenuItem.Image")));
            this.removeToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.removeToolStripMenuItem.Text = "&Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.Image = ((System.Drawing.Image)(resources.GetObject("btnRemove.Image")));
            this.btnRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemove.Location = new System.Drawing.Point(808, 86);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(2);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(76, 24);
            this.btnRemove.TabIndex = 1;
            this.btnRemove.Text = "      &Remove";
            this.btnRemove.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNew.Location = new System.Drawing.Point(808, 30);
            this.btnNew.Margin = new System.Windows.Forms.Padding(2);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(76, 24);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "      &New";
            this.btnNew.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(808, 58);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(2);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(78, 24);
            this.btnEdit.TabIndex = 13;
            this.btnEdit.Text = "      &Edit";
            this.btnEdit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // ucLabel1
            // 
            this.ucLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucLabel1.Location = new System.Drawing.Point(0, 0);
            this.ucLabel1.Margin = new System.Windows.Forms.Padding(2);
            this.ucLabel1.Name = "ucLabel1";
            this.ucLabel1.Size = new System.Drawing.Size(908, 25);
            this.ucLabel1.TabIndex = 7;
            // 
            // mnuTreeNode
            // 
            this.mnuTreeNode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemViewFunction,
            this.viewValidationFunctionToolStripMenuItem,
            this.mnuTreeNodeSeparator1,
            this.mnuTreeNodeSeparator2});
            this.mnuTreeNode.Name = "mnuTabPage";
            this.mnuTreeNode.Size = new System.Drawing.Size(219, 60);
            // 
            // mnuItemViewFunction
            // 
            this.mnuItemViewFunction.Image = ((System.Drawing.Image)(resources.GetObject("mnuItemViewFunction.Image")));
            this.mnuItemViewFunction.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuItemViewFunction.Name = "mnuItemViewFunction";
            this.mnuItemViewFunction.Size = new System.Drawing.Size(218, 22);
            this.mnuItemViewFunction.Text = "View &Default Value Function";
            this.mnuItemViewFunction.ToolTipText = "View/edit the function that populates this file";
            // 
            // viewValidationFunctionToolStripMenuItem
            // 
            this.viewValidationFunctionToolStripMenuItem.Name = "viewValidationFunctionToolStripMenuItem";
            this.viewValidationFunctionToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.viewValidationFunctionToolStripMenuItem.Text = "View &Validation Function";
            // 
            // mnuTreeNodeSeparator1
            // 
            this.mnuTreeNodeSeparator1.Name = "mnuTreeNodeSeparator1";
            this.mnuTreeNodeSeparator1.Size = new System.Drawing.Size(215, 6);
            // 
            // mnuTreeNodeSeparator2
            // 
            this.mnuTreeNodeSeparator2.Name = "mnuTreeNodeSeparator2";
            this.mnuTreeNodeSeparator2.Size = new System.Drawing.Size(215, 6);
            // 
            // ucUserOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(219)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.ucLabel1);
            this.Controls.Add(this.lstOptions);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ucUserOptions";
            this.Size = new System.Drawing.Size(908, 461);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ucUserOptions_Paint);
            this.contextMenuStrip1.ResumeLayout(false);
            this.mnuTreeNode.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView lstOptions;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
		private Slyce.Common.Controls.ucHeading ucLabel1;
		private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ContextMenuStrip mnuTreeNode;
        private System.Windows.Forms.ToolStripMenuItem mnuItemViewFunction;
        private System.Windows.Forms.ToolStripMenuItem viewValidationFunctionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator mnuTreeNodeSeparator1;
        private System.Windows.Forms.ToolStripSeparator mnuTreeNodeSeparator2;
	}
}
