namespace Slyce.IntelliMerge
{
	partial class ucMerger
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucMerger));
            ActiproSoftware.SyntaxEditor.Document document3 = new ActiproSoftware.SyntaxEditor.Document();
            ActiproSoftware.Drawing.MultiColorLinearGradient multiColorLinearGradient3 = new ActiproSoftware.Drawing.MultiColorLinearGradient();
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Node0");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Node1");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Node3");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Node4");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Node2", new System.Windows.Forms.TreeNode[] {
            treeNode8,
            treeNode9});
            ActiproSoftware.SyntaxEditor.Document document4 = new ActiproSoftware.SyntaxEditor.Document();
            ActiproSoftware.Drawing.MultiColorLinearGradient multiColorLinearGradient4 = new ActiproSoftware.Drawing.MultiColorLinearGradient();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnPopulateTree = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tabStrip1 = new ActiproSoftware.UIStudio.TabStrip.TabStrip();
            this.tabStripPage1 = new ActiproSoftware.UIStudio.TabStrip.TabStripPage();
            this.syntaxEditor1 = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
            this.tabStripPage5 = new ActiproSoftware.UIStudio.TabStrip.TabStripPage();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuAccept = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPersist = new System.Windows.Forms.Button();
            this.ddlLanguages = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvOne = new System.Windows.Forms.TreeView();
            this.lstFunctions = new System.Windows.Forms.ListBox();
            this.treeGridView1 = new AdvancedDataGridView.TreeGridView();
            this.Column1 = new AdvancedDataGridView.TreeGridColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewImageColumn();
            this.imageListNodes = new System.Windows.Forms.ImageList(this.components);
            this.btnDisplayNodeFile = new System.Windows.Forms.Button();
            this.ucStatus1 = new Slyce.IntelliMerge.ucStatus();
            this.ucBinaryFileViewer1 = new Slyce.IntelliMerge.ucBinaryFileViewer();
            this.btnSwitchEditorView = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.syntaxEditor2 = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
            ((System.ComponentModel.ISupportInitialize)(this.tabStrip1)).BeginInit();
            this.tabStrip1.SuspendLayout();
            this.tabStripPage1.SuspendLayout();
            this.tabStripPage5.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeGridView1)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "");
            this.imageList1.Images.SetKeyName(6, "VSObject_Class.bmp");
            this.imageList1.Images.SetKeyName(7, "VSObject_Field.bmp");
            this.imageList1.Images.SetKeyName(8, "VSObject_Method.bmp");
            this.imageList1.Images.SetKeyName(9, "VSObject_Properties.bmp");
            // 
            // btnPopulateTree
            // 
            this.btnPopulateTree.Image = ((System.Drawing.Image)(resources.GetObject("btnPopulateTree.Image")));
            this.btnPopulateTree.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPopulateTree.Location = new System.Drawing.Point(378, 3);
            this.btnPopulateTree.Name = "btnPopulateTree";
            this.btnPopulateTree.Size = new System.Drawing.Size(94, 32);
            this.btnPopulateTree.TabIndex = 10;
            this.btnPopulateTree.Text = "     Undo All";
            this.btnPopulateTree.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPopulateTree.UseVisualStyleBackColor = true;
            this.btnPopulateTree.Click += new System.EventHandler(this.btnPopulateTree_Click);
            // 
            // tabStrip1
            // 
            this.tabStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabStrip1.AutoSetFocusOnClick = true;
            this.tabStrip1.Controls.Add(this.tabStripPage1);
            this.tabStrip1.Controls.Add(this.tabStripPage5);
            this.tabStrip1.ImageList = this.imageList1;
            this.tabStrip1.Location = new System.Drawing.Point(3, 3);
            this.tabStrip1.Name = "tabStrip1";
            this.tabStrip1.Size = new System.Drawing.Size(695, 431);
            this.tabStrip1.TabIndex = 14;
            this.tabStrip1.Text = "tabMerge";
            // 
            // tabStripPage1
            // 
            this.tabStripPage1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tabStripPage1.Controls.Add(this.splitContainer2);
            this.tabStripPage1.Key = "TabStripPage";
            this.tabStripPage1.Location = new System.Drawing.Point(0, 22);
            this.tabStripPage1.Name = "tabStripPage1";
            this.tabStripPage1.Size = new System.Drawing.Size(695, 409);
            this.tabStripPage1.TabIndex = 0;
            this.tabStripPage1.Text = "Merge View";
            // 
            // syntaxEditor1
            // 
            this.syntaxEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.syntaxEditor1.Document = document3;
            this.syntaxEditor1.LineNumberMarginVisible = true;
            this.syntaxEditor1.Location = new System.Drawing.Point(0, 0);
            this.syntaxEditor1.Name = "syntaxEditor1";
            this.syntaxEditor1.Size = new System.Drawing.Size(349, 409);
            this.syntaxEditor1.TabIndex = 35;
            multiColorLinearGradient3.EndColor = System.Drawing.Color.Silver;
            multiColorLinearGradient3.IntermediateColors = new ActiproSoftware.Drawing.LinearGradientColorPosition[] {
        new ActiproSoftware.Drawing.LinearGradientColorPosition(System.Drawing.Color.White, 0.5F)};
            multiColorLinearGradient3.StartColor = System.Drawing.Color.Silver;
            this.syntaxEditor1.UserMarginBackgroundFill = multiColorLinearGradient3;
            this.syntaxEditor1.UserMarginVisible = true;
            this.syntaxEditor1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.syntaxEditor1_MouseClick);
            this.syntaxEditor1.DocumentTextChanged += new ActiproSoftware.SyntaxEditor.DocumentModificationEventHandler(this.syntaxEditor1_DocumentTextChanged);
            this.syntaxEditor1.UserMarginPaint += new ActiproSoftware.SyntaxEditor.UserMarginPaintEventHandler(this.syntaxEditor1_UserMarginPaint);
            this.syntaxEditor1.ContextMenuRequested += new ActiproSoftware.SyntaxEditor.ContextMenuRequestEventHandler(this.syntaxEditor1_ContextMenuRequested);
            // 
            // tabStripPage5
            // 
            this.tabStripPage5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tabStripPage5.BackgroundFill = new ActiproSoftware.Drawing.TwoColorLinearGradient(System.Drawing.SystemColors.ControlLight, System.Drawing.SystemColors.ControlDark, 90F, ActiproSoftware.Drawing.TwoColorLinearGradientStyle.Normal, ActiproSoftware.Drawing.BackgroundFillRotationType.None);
            this.tabStripPage5.Controls.Add(this.label3);
            this.tabStripPage5.Controls.Add(this.label2);
            this.tabStripPage5.Controls.Add(this.button2);
            this.tabStripPage5.Controls.Add(this.listBox2);
            this.tabStripPage5.Controls.Add(this.textBox2);
            this.tabStripPage5.Controls.Add(this.button1);
            this.tabStripPage5.Controls.Add(this.listBox1);
            this.tabStripPage5.Controls.Add(this.textBox1);
            this.tabStripPage5.Key = "TabStripPage";
            this.tabStripPage5.Location = new System.Drawing.Point(0, 22);
            this.tabStripPage5.Name = "tabStripPage5";
            this.tabStripPage5.Size = new System.Drawing.Size(695, 409);
            this.tabStripPage5.TabIndex = 4;
            this.tabStripPage5.Text = "File Matching";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(47, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(171, 17);
            this.label3.TabIndex = 17;
            this.label3.Text = "Previously Generated File";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(340, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "User File";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(516, 33);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(36, 24);
            this.button2.TabIndex = 15;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 16;
            this.listBox2.Location = new System.Drawing.Point(343, 62);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(209, 100);
            this.listBox2.TabIndex = 14;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(343, 34);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(166, 22);
            this.textBox2.TabIndex = 13;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(244, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(36, 24);
            this.button1.TabIndex = 12;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(50, 62);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(230, 100);
            this.listBox1.TabIndex = 11;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(50, 34);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(188, 22);
            this.textBox1.TabIndex = 10;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAccept,
            this.mnuDelete});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(136, 48);
            // 
            // mnuAccept
            // 
            this.mnuAccept.Image = ((System.Drawing.Image)(resources.GetObject("mnuAccept.Image")));
            this.mnuAccept.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuAccept.Name = "mnuAccept";
            this.mnuAccept.Size = new System.Drawing.Size(135, 22);
            this.mnuAccept.Text = "&Accept";
            this.mnuAccept.Click += new System.EventHandler(this.mnuAccept_Click);
            // 
            // mnuDelete
            // 
            this.mnuDelete.Image = ((System.Drawing.Image)(resources.GetObject("mnuDelete.Image")));
            this.mnuDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuDelete.Name = "mnuDelete";
            this.mnuDelete.Size = new System.Drawing.Size(135, 22);
            this.mnuDelete.Text = "&Delete";
            this.mnuDelete.Click += new System.EventHandler(this.mnuDelete_Click);
            // 
            // btnPersist
            // 
            this.btnPersist.Image = ((System.Drawing.Image)(resources.GetObject("btnPersist.Image")));
            this.btnPersist.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPersist.Location = new System.Drawing.Point(478, 3);
            this.btnPersist.Name = "btnPersist";
            this.btnPersist.Size = new System.Drawing.Size(76, 32);
            this.btnPersist.TabIndex = 19;
            this.btnPersist.Text = "     Finish";
            this.btnPersist.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPersist.UseVisualStyleBackColor = true;
            this.btnPersist.Click += new System.EventHandler(this.btnPersist_Click);
            // 
            // ddlLanguages
            // 
            this.ddlLanguages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ddlLanguages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlLanguages.FormattingEnabled = true;
            this.ddlLanguages.Items.AddRange(new object[] {
            "Batch file",
            "C#",
            "CSS",
            "HTML",
            "INI file",
            "Java",
            "JScript",
            "Perl",
            "PHP",
            "Python",
            "T-SQL",
            "VB.net",
            "VBScript",
            "XML"});
            this.ddlLanguages.Location = new System.Drawing.Point(946, 11);
            this.ddlLanguages.Name = "ddlLanguages";
            this.ddlLanguages.Size = new System.Drawing.Size(121, 24);
            this.ddlLanguages.TabIndex = 22;
            this.ddlLanguages.SelectedIndexChanged += new System.EventHandler(this.ddlLanguages_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(879, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 17);
            this.label4.TabIndex = 23;
            this.label4.Text = "File type";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(3, 52);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvOne);
            this.splitContainer1.Panel1.Controls.Add(this.lstFunctions);
            this.splitContainer1.Panel1.Controls.Add(this.treeGridView1);
            this.splitContainer1.Panel1.Controls.Add(this.btnDisplayNodeFile);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(1064, 492);
            this.splitContainer1.SplitterDistance = 354;
            this.splitContainer1.TabIndex = 25;
            // 
            // tvOne
            // 
            this.tvOne.Location = new System.Drawing.Point(208, 3);
            this.tvOne.Name = "tvOne";
            treeNode6.Name = "Node0";
            treeNode6.Text = "Node0";
            treeNode7.Name = "Node1";
            treeNode7.Text = "Node1";
            treeNode8.Name = "Node3";
            treeNode8.Text = "Node3";
            treeNode9.Name = "Node4";
            treeNode9.Text = "Node4";
            treeNode10.Name = "Node2";
            treeNode10.Text = "Node2";
            this.tvOne.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode7,
            treeNode10});
            this.tvOne.Size = new System.Drawing.Size(143, 27);
            this.tvOne.TabIndex = 5;
            this.tvOne.Visible = false;
            this.tvOne.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvOne_AfterSelect);
            // 
            // lstFunctions
            // 
            this.lstFunctions.FormattingEnabled = true;
            this.lstFunctions.ItemHeight = 16;
            this.lstFunctions.Items.AddRange(new object[] {
            "aaa",
            "bbb",
            "ccc"});
            this.lstFunctions.Location = new System.Drawing.Point(3, 3);
            this.lstFunctions.Name = "lstFunctions";
            this.lstFunctions.Size = new System.Drawing.Size(101, 20);
            this.lstFunctions.TabIndex = 4;
            this.lstFunctions.Visible = false;
            this.lstFunctions.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstFunctions_MouseDoubleClick);
            this.lstFunctions.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstFunctions_KeyDown);
            // 
            // treeGridView1
            // 
            this.treeGridView1.AllowUserToAddRows = false;
            this.treeGridView1.AllowUserToDeleteRows = false;
            this.treeGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.treeGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.treeGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.treeGridView1.ImageList = this.imageListNodes;
            this.treeGridView1.Location = new System.Drawing.Point(3, 47);
            this.treeGridView1.MultiSelect = false;
            this.treeGridView1.Name = "treeGridView1";
            this.treeGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.treeGridView1.ShowLines = false;
            this.treeGridView1.Size = new System.Drawing.Size(347, 442);
            this.treeGridView1.TabIndex = 0;
            this.treeGridView1.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.treeGridView1_CellMouseLeave);
            this.treeGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.treeGridView1_CellClick_1);
            this.treeGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.treeGridView1_RowEnter);
            this.treeGridView1.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.treeGridView1_RowLeave);
            this.treeGridView1.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.treeGridView1_CellMouseEnter);
            // 
            // Column1
            // 
            this.Column1.DefaultNodeImage = null;
            this.Column1.HeaderText = "Files";
            this.Column1.Name = "Column1";
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 400;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Prev Gen File";
            this.Column2.Name = "Column2";
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "";
            this.Column3.Image = ((System.Drawing.Image)(resources.GetObject("Column3.Image")));
            this.Column3.Name = "Column3";
            this.Column3.Width = 40;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "User File";
            this.Column4.Name = "Column4";
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "";
            this.Column5.Image = ((System.Drawing.Image)(resources.GetObject("Column5.Image")));
            this.Column5.Name = "Column5";
            this.Column5.Width = 40;
            // 
            // imageListNodes
            // 
            this.imageListNodes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListNodes.ImageStream")));
            this.imageListNodes.TransparentColor = System.Drawing.Color.Magenta;
            this.imageListNodes.Images.SetKeyName(0, "VSObject_Class.bmp");
            this.imageListNodes.Images.SetKeyName(1, "VSObject_Field.bmp");
            this.imageListNodes.Images.SetKeyName(2, "VSObject_Method.bmp");
            this.imageListNodes.Images.SetKeyName(3, "VSObject_Properties.bmp");
            this.imageListNodes.Images.SetKeyName(4, "VSProject_CSCodefile.bmp");
            this.imageListNodes.Images.SetKeyName(5, "document.bmp");
            this.imageListNodes.Images.SetKeyName(6, "otheroptions.ico");
            this.imageListNodes.Images.SetKeyName(7, "blank.ico");
            // 
            // btnDisplayNodeFile
            // 
            this.btnDisplayNodeFile.Location = new System.Drawing.Point(18, 18);
            this.btnDisplayNodeFile.Name = "btnDisplayNodeFile";
            this.btnDisplayNodeFile.Size = new System.Drawing.Size(200, 23);
            this.btnDisplayNodeFile.TabIndex = 1;
            this.btnDisplayNodeFile.Text = "Display Selected Node";
            this.btnDisplayNodeFile.UseVisualStyleBackColor = true;
            this.btnDisplayNodeFile.Click += new System.EventHandler(this.btnDisplayNodeFile_Click);
            // 
            // ucStatus1
            // 
            this.ucStatus1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucStatus1.Location = new System.Drawing.Point(559, 3);
            this.ucStatus1.Name = "ucStatus1";
            this.ucStatus1.Size = new System.Drawing.Size(314, 32);
            this.ucStatus1.TabIndex = 24;
            // 
            // ucBinaryFileViewer1
            // 
            this.ucBinaryFileViewer1.Location = new System.Drawing.Point(0, 0);
            this.ucBinaryFileViewer1.Name = "ucBinaryFileViewer1";
            this.ucBinaryFileViewer1.Size = new System.Drawing.Size(221, 35);
            this.ucBinaryFileViewer1.TabIndex = 21;
            this.ucBinaryFileViewer1.Visible = false;
            // 
            // btnSwitchEditorView
            // 
            this.btnSwitchEditorView.Image = ((System.Drawing.Image)(resources.GetObject("btnSwitchEditorView.Image")));
            this.btnSwitchEditorView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSwitchEditorView.Location = new System.Drawing.Point(257, 3);
            this.btnSwitchEditorView.Name = "btnSwitchEditorView";
            this.btnSwitchEditorView.Size = new System.Drawing.Size(115, 32);
            this.btnSwitchEditorView.TabIndex = 26;
            this.btnSwitchEditorView.Text = "     Switch View";
            this.btnSwitchEditorView.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSwitchEditorView.UseVisualStyleBackColor = true;
            this.btnSwitchEditorView.Click += new System.EventHandler(this.btnSwitchEditorView_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.syntaxEditor1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.syntaxEditor2);
            this.splitContainer2.Size = new System.Drawing.Size(695, 409);
            this.splitContainer2.SplitterDistance = 349;
            this.splitContainer2.TabIndex = 36;
            // 
            // syntaxEditor2
            // 
            this.syntaxEditor2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.syntaxEditor2.Document = document4;
            this.syntaxEditor2.LineNumberMarginVisible = true;
            this.syntaxEditor2.Location = new System.Drawing.Point(0, 0);
            this.syntaxEditor2.Name = "syntaxEditor2";
            this.syntaxEditor2.Size = new System.Drawing.Size(342, 409);
            this.syntaxEditor2.TabIndex = 36;
            multiColorLinearGradient4.EndColor = System.Drawing.Color.Silver;
            multiColorLinearGradient4.IntermediateColors = new ActiproSoftware.Drawing.LinearGradientColorPosition[] {
        new ActiproSoftware.Drawing.LinearGradientColorPosition(System.Drawing.Color.White, 0.5F)};
            multiColorLinearGradient4.StartColor = System.Drawing.Color.Silver;
            this.syntaxEditor2.UserMarginBackgroundFill = multiColorLinearGradient4;
            this.syntaxEditor2.UserMarginVisible = true;
            // 
            // ucMerger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(219)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.btnSwitchEditorView);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.ucStatus1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ddlLanguages);
            this.Controls.Add(this.btnPersist);
            this.Controls.Add(this.btnPopulateTree);
            this.Controls.Add(this.ucBinaryFileViewer1);
            this.Name = "ucMerger";
            this.Size = new System.Drawing.Size(1070, 547);
            this.Load += new System.EventHandler(this.ucMerger_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tabStrip1)).EndInit();
            this.tabStrip1.ResumeLayout(false);
            this.tabStripPage1.ResumeLayout(false);
            this.tabStripPage5.ResumeLayout(false);
            this.tabStripPage5.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeGridView1)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnPopulateTree;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.ImageList imageList1;
		private ActiproSoftware.UIStudio.TabStrip.TabStrip tabStrip1;
        private ActiproSoftware.UIStudio.TabStrip.TabStripPage tabStripPage1;
        private ActiproSoftware.SyntaxEditor.SyntaxEditor syntaxEditor1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem mnuAccept;
        private System.Windows.Forms.ToolStripMenuItem mnuDelete;
        private System.Windows.Forms.Button btnPersist;
		private ucBinaryFileViewer ucBinaryFileViewer1;
		private System.Windows.Forms.ComboBox ddlLanguages;
		private System.Windows.Forms.Label label4;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Slyce.IntelliMerge.ucStatus ucStatus1;
		private ActiproSoftware.UIStudio.TabStrip.TabStripPage tabStripPage5;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.ListBox listBox2;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Button btnDisplayNodeFile;
		private AdvancedDataGridView.TreeGridView treeGridView1;
		private System.Windows.Forms.ListBox lstFunctions;
		private System.Windows.Forms.TreeView tvOne;
		private System.Windows.Forms.ImageList imageListNodes;
		private AdvancedDataGridView.TreeGridColumn Column1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
		private System.Windows.Forms.DataGridViewImageColumn Column3;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
		private System.Windows.Forms.DataGridViewImageColumn Column5;
        private System.Windows.Forms.Button btnSwitchEditorView;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private ActiproSoftware.SyntaxEditor.SyntaxEditor syntaxEditor2;

	}
}
