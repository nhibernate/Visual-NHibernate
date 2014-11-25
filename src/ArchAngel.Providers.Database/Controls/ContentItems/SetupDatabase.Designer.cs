namespace ArchAngel.Providers.Database.Controls.ContentItems
{
    partial class SetupDatabase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupDatabase));
            this.buttonStoredProcedurePrefixAdd = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.buttonViewPrefixAdd = new System.Windows.Forms.Button();
            this.buttonTablePrefixAdd = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonAddDatabase = new System.Windows.Forms.Button();
            this.listBoxDatabase = new System.Windows.Forms.ListBox();
            this.textBoxTablePrefix = new System.Windows.Forms.TextBox();
            this.listBoxTablePrefix = new System.Windows.Forms.ListBox();
            this.listBoxStoredProcedurePrefix = new System.Windows.Forms.ListBox();
            this.textBoxViewPrefix = new System.Windows.Forms.TextBox();
            this.textBoxStoredProcedurePrefix = new System.Windows.Forms.TextBox();
            this.listBoxViewPrefix = new System.Windows.Forms.ListBox();
            this.contextMenuStripPrefix = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemDeletePrefix = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripDatabase = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemDatabaseDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.buttonTablePrefixDelete = new System.Windows.Forms.Button();
            this.buttonViewPrefixDelete = new System.Windows.Forms.Button();
            this.buttonStoredProcedurePrefixDelete = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.groupPanel4 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.groupPanel3 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.contextMenuStripPrefix.SuspendLayout();
            this.contextMenuStripDatabase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.groupPanel4.SuspendLayout();
            this.groupPanel3.SuspendLayout();
            this.groupPanel2.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStoredProcedurePrefixAdd
            // 
            this.buttonStoredProcedurePrefixAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonStoredProcedurePrefixAdd.ImageIndex = 0;
            this.buttonStoredProcedurePrefixAdd.ImageList = this.imageList1;
            this.buttonStoredProcedurePrefixAdd.Location = new System.Drawing.Point(141, 4);
            this.buttonStoredProcedurePrefixAdd.Name = "buttonStoredProcedurePrefixAdd";
            this.buttonStoredProcedurePrefixAdd.Size = new System.Drawing.Size(24, 24);
            this.buttonStoredProcedurePrefixAdd.TabIndex = 15;
            this.buttonStoredProcedurePrefixAdd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.buttonStoredProcedurePrefixAdd, "Add prefix to the list");
            this.buttonStoredProcedurePrefixAdd.UseVisualStyleBackColor = true;
            this.buttonStoredProcedurePrefixAdd.Click += new System.EventHandler(this.buttonStoredProcedurePrefixAdd_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList1.Images.SetKeyName(0, "AddTableHS.png");
            this.imageList1.Images.SetKeyName(1, "EditInformationHS.png");
            this.imageList1.Images.SetKeyName(2, "delete.bmp");
            // 
            // buttonViewPrefixAdd
            // 
            this.buttonViewPrefixAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonViewPrefixAdd.ImageIndex = 0;
            this.buttonViewPrefixAdd.ImageList = this.imageList1;
            this.buttonViewPrefixAdd.Location = new System.Drawing.Point(143, 11);
            this.buttonViewPrefixAdd.Name = "buttonViewPrefixAdd";
            this.buttonViewPrefixAdd.Size = new System.Drawing.Size(24, 24);
            this.buttonViewPrefixAdd.TabIndex = 11;
            this.buttonViewPrefixAdd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.buttonViewPrefixAdd, "Add prefix to the list");
            this.buttonViewPrefixAdd.UseVisualStyleBackColor = true;
            this.buttonViewPrefixAdd.Click += new System.EventHandler(this.buttonViewPrefixAdd_Click);
            // 
            // buttonTablePrefixAdd
            // 
            this.buttonTablePrefixAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonTablePrefixAdd.ImageIndex = 0;
            this.buttonTablePrefixAdd.ImageList = this.imageList1;
            this.buttonTablePrefixAdd.Location = new System.Drawing.Point(141, 9);
            this.buttonTablePrefixAdd.Name = "buttonTablePrefixAdd";
            this.buttonTablePrefixAdd.Size = new System.Drawing.Size(24, 24);
            this.buttonTablePrefixAdd.TabIndex = 7;
            this.buttonTablePrefixAdd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.buttonTablePrefixAdd, "Add prefix to the list");
            this.buttonTablePrefixAdd.UseVisualStyleBackColor = true;
            this.buttonTablePrefixAdd.Click += new System.EventHandler(this.buttonTablePrefixAdd_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonEdit.ImageIndex = 1;
            this.buttonEdit.ImageList = this.imageList1;
            this.buttonEdit.Location = new System.Drawing.Point(141, 38);
            this.buttonEdit.Margin = new System.Windows.Forms.Padding(2);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(24, 24);
            this.buttonEdit.TabIndex = 6;
            this.buttonEdit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.buttonEdit, "Edit");
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonRemove.ImageIndex = 2;
            this.buttonRemove.ImageList = this.imageList1;
            this.buttonRemove.Location = new System.Drawing.Point(141, 66);
            this.buttonRemove.Margin = new System.Windows.Forms.Padding(2);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(24, 24);
            this.buttonRemove.TabIndex = 5;
            this.buttonRemove.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.buttonRemove, "Remove");
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonAddDatabase
            // 
            this.buttonAddDatabase.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddDatabase.ImageIndex = 0;
            this.buttonAddDatabase.ImageList = this.imageList1;
            this.buttonAddDatabase.Location = new System.Drawing.Point(141, 10);
            this.buttonAddDatabase.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAddDatabase.Name = "buttonAddDatabase";
            this.buttonAddDatabase.Size = new System.Drawing.Size(24, 24);
            this.buttonAddDatabase.TabIndex = 4;
            this.buttonAddDatabase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.buttonAddDatabase, "Add another database to the list");
            this.buttonAddDatabase.UseVisualStyleBackColor = true;
            this.buttonAddDatabase.Click += new System.EventHandler(this.buttonAddDatabase_Click);
            // 
            // listBoxDatabase
            // 
            this.listBoxDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxDatabase.FormattingEnabled = true;
            this.listBoxDatabase.Location = new System.Drawing.Point(16, 10);
            this.listBoxDatabase.Name = "listBoxDatabase";
            this.listBoxDatabase.Size = new System.Drawing.Size(120, 121);
            this.listBoxDatabase.TabIndex = 2;
            this.listBoxDatabase.Validating += new System.ComponentModel.CancelEventHandler(this.listBoxDatabase_Validating);
            this.listBoxDatabase.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listBoxDatabase_MouseUp);
            // 
            // textBoxTablePrefix
            // 
            this.textBoxTablePrefix.Location = new System.Drawing.Point(16, 10);
            this.textBoxTablePrefix.Name = "textBoxTablePrefix";
            this.textBoxTablePrefix.Size = new System.Drawing.Size(120, 20);
            this.textBoxTablePrefix.TabIndex = 6;
            this.textBoxTablePrefix.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxTablePrefix_KeyDown);
            // 
            // listBoxTablePrefix
            // 
            this.listBoxTablePrefix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxTablePrefix.FormattingEnabled = true;
            this.listBoxTablePrefix.Location = new System.Drawing.Point(16, 36);
            this.listBoxTablePrefix.Name = "listBoxTablePrefix";
            this.listBoxTablePrefix.Size = new System.Drawing.Size(120, 108);
            this.listBoxTablePrefix.TabIndex = 8;
            this.listBoxTablePrefix.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listBoxTablePrefix_MouseUp);
            this.listBoxTablePrefix.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBoxTablePrefix_KeyDown);
            // 
            // listBoxStoredProcedurePrefix
            // 
            this.listBoxStoredProcedurePrefix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxStoredProcedurePrefix.FormattingEnabled = true;
            this.listBoxStoredProcedurePrefix.Location = new System.Drawing.Point(15, 31);
            this.listBoxStoredProcedurePrefix.Name = "listBoxStoredProcedurePrefix";
            this.listBoxStoredProcedurePrefix.Size = new System.Drawing.Size(120, 108);
            this.listBoxStoredProcedurePrefix.TabIndex = 16;
            this.listBoxStoredProcedurePrefix.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listBoxStoredProcedurePrefix_MouseUp);
            this.listBoxStoredProcedurePrefix.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBoxStoredProcedurePrefix_KeyDown);
            // 
            // textBoxViewPrefix
            // 
            this.textBoxViewPrefix.Location = new System.Drawing.Point(17, 12);
            this.textBoxViewPrefix.Name = "textBoxViewPrefix";
            this.textBoxViewPrefix.Size = new System.Drawing.Size(120, 20);
            this.textBoxViewPrefix.TabIndex = 10;
            this.textBoxViewPrefix.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxViewPrefix_KeyDown);
            // 
            // textBoxStoredProcedurePrefix
            // 
            this.textBoxStoredProcedurePrefix.Location = new System.Drawing.Point(15, 5);
            this.textBoxStoredProcedurePrefix.Name = "textBoxStoredProcedurePrefix";
            this.textBoxStoredProcedurePrefix.Size = new System.Drawing.Size(120, 20);
            this.textBoxStoredProcedurePrefix.TabIndex = 14;
            this.textBoxStoredProcedurePrefix.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxStoredProcedurePrefix_KeyDown);
            // 
            // listBoxViewPrefix
            // 
            this.listBoxViewPrefix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxViewPrefix.FormattingEnabled = true;
            this.listBoxViewPrefix.Location = new System.Drawing.Point(17, 38);
            this.listBoxViewPrefix.Name = "listBoxViewPrefix";
            this.listBoxViewPrefix.Size = new System.Drawing.Size(120, 108);
            this.listBoxViewPrefix.TabIndex = 12;
            this.listBoxViewPrefix.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listBoxViewPrefix_MouseUp);
            this.listBoxViewPrefix.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBoxViewPrefix_KeyDown);
            // 
            // contextMenuStripPrefix
            // 
            this.contextMenuStripPrefix.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDeletePrefix});
            this.contextMenuStripPrefix.Name = "contextMenuStripPrefix";
            this.contextMenuStripPrefix.Size = new System.Drawing.Size(117, 26);
            // 
            // toolStripMenuItemDeletePrefix
            // 
            this.toolStripMenuItemDeletePrefix.Name = "toolStripMenuItemDeletePrefix";
            this.toolStripMenuItemDeletePrefix.Size = new System.Drawing.Size(116, 22);
            this.toolStripMenuItemDeletePrefix.Text = "&Delete";
            this.toolStripMenuItemDeletePrefix.Click += new System.EventHandler(this.toolStripMenuItemDeletePrefix_Click);
            // 
            // contextMenuStripDatabase
            // 
            this.contextMenuStripDatabase.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDatabaseDelete});
            this.contextMenuStripDatabase.Name = "contextMenuStripDatabase";
            this.contextMenuStripDatabase.Size = new System.Drawing.Size(117, 26);
            // 
            // toolStripMenuItemDatabaseDelete
            // 
            this.toolStripMenuItemDatabaseDelete.Name = "toolStripMenuItemDatabaseDelete";
            this.toolStripMenuItemDatabaseDelete.Size = new System.Drawing.Size(116, 22);
            this.toolStripMenuItemDatabaseDelete.Text = "&Delete";
            this.toolStripMenuItemDatabaseDelete.Click += new System.EventHandler(this.toolStripMenuItemDatabaseDelete_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // buttonTablePrefixDelete
            // 
            this.buttonTablePrefixDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonTablePrefixDelete.ImageIndex = 2;
            this.buttonTablePrefixDelete.ImageList = this.imageList1;
            this.buttonTablePrefixDelete.Location = new System.Drawing.Point(141, 42);
            this.buttonTablePrefixDelete.Name = "buttonTablePrefixDelete";
            this.buttonTablePrefixDelete.Size = new System.Drawing.Size(24, 24);
            this.buttonTablePrefixDelete.TabIndex = 18;
            this.buttonTablePrefixDelete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.buttonTablePrefixDelete, "Remove");
            this.buttonTablePrefixDelete.UseVisualStyleBackColor = true;
            this.buttonTablePrefixDelete.Click += new System.EventHandler(this.buttonTablePrefixDelete_Click);
            // 
            // buttonViewPrefixDelete
            // 
            this.buttonViewPrefixDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonViewPrefixDelete.ImageIndex = 2;
            this.buttonViewPrefixDelete.ImageList = this.imageList1;
            this.buttonViewPrefixDelete.Location = new System.Drawing.Point(143, 44);
            this.buttonViewPrefixDelete.Name = "buttonViewPrefixDelete";
            this.buttonViewPrefixDelete.Size = new System.Drawing.Size(24, 24);
            this.buttonViewPrefixDelete.TabIndex = 19;
            this.buttonViewPrefixDelete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.buttonViewPrefixDelete, "Remove");
            this.buttonViewPrefixDelete.UseVisualStyleBackColor = true;
            this.buttonViewPrefixDelete.Click += new System.EventHandler(this.buttonViewPrefixDelete_Click);
            // 
            // buttonStoredProcedurePrefixDelete
            // 
            this.buttonStoredProcedurePrefixDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonStoredProcedurePrefixDelete.ImageIndex = 2;
            this.buttonStoredProcedurePrefixDelete.ImageList = this.imageList1;
            this.buttonStoredProcedurePrefixDelete.Location = new System.Drawing.Point(141, 37);
            this.buttonStoredProcedurePrefixDelete.Name = "buttonStoredProcedurePrefixDelete";
            this.buttonStoredProcedurePrefixDelete.Size = new System.Drawing.Size(24, 24);
            this.buttonStoredProcedurePrefixDelete.TabIndex = 20;
            this.buttonStoredProcedurePrefixDelete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.buttonStoredProcedurePrefixDelete, "Remove");
            this.buttonStoredProcedurePrefixDelete.UseVisualStyleBackColor = true;
            this.buttonStoredProcedurePrefixDelete.Click += new System.EventHandler(this.buttonStoredProcedurePrefixDelete_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panelEx1.Controls.Add(this.groupPanel4);
            this.panelEx1.Controls.Add(this.groupPanel3);
            this.panelEx1.Controls.Add(this.groupPanel2);
            this.panelEx1.Controls.Add(this.groupPanel1);
            this.panelEx1.Controls.Add(this.label4);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(623, 372);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 25;
            // 
            // groupPanel4
            // 
            this.groupPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupPanel4.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel4.Controls.Add(this.textBoxStoredProcedurePrefix);
            this.groupPanel4.Controls.Add(this.listBoxStoredProcedurePrefix);
            this.groupPanel4.Controls.Add(this.buttonStoredProcedurePrefixAdd);
            this.groupPanel4.Controls.Add(this.buttonStoredProcedurePrefixDelete);
            this.groupPanel4.Location = new System.Drawing.Point(426, 170);
            this.groupPanel4.Name = "groupPanel4";
            this.groupPanel4.Size = new System.Drawing.Size(183, 180);
            // 
            // 
            // 
            this.groupPanel4.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel4.Style.BackColorGradientAngle = 90;
            this.groupPanel4.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel4.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel4.Style.BorderBottomWidth = 1;
            this.groupPanel4.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel4.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel4.Style.BorderLeftWidth = 1;
            this.groupPanel4.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel4.Style.BorderRightWidth = 1;
            this.groupPanel4.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel4.Style.BorderTopWidth = 1;
            this.groupPanel4.Style.CornerDiameter = 4;
            this.groupPanel4.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel4.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel4.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel4.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            this.groupPanel4.TabIndex = 19;
            this.groupPanel4.Text = "Stored Procedure Prefixes";
            // 
            // groupPanel3
            // 
            this.groupPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupPanel3.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel3.Controls.Add(this.textBoxViewPrefix);
            this.groupPanel3.Controls.Add(this.listBoxViewPrefix);
            this.groupPanel3.Controls.Add(this.buttonViewPrefixDelete);
            this.groupPanel3.Controls.Add(this.buttonViewPrefixAdd);
            this.groupPanel3.Location = new System.Drawing.Point(215, 170);
            this.groupPanel3.Name = "groupPanel3";
            this.groupPanel3.Size = new System.Drawing.Size(181, 180);
            // 
            // 
            // 
            this.groupPanel3.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel3.Style.BackColorGradientAngle = 90;
            this.groupPanel3.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel3.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderBottomWidth = 1;
            this.groupPanel3.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel3.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderLeftWidth = 1;
            this.groupPanel3.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderRightWidth = 1;
            this.groupPanel3.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderTopWidth = 1;
            this.groupPanel3.Style.CornerDiameter = 4;
            this.groupPanel3.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel3.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel3.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel3.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            this.groupPanel3.TabIndex = 18;
            this.groupPanel3.Text = "View Prefixes";
            // 
            // groupPanel2
            // 
            this.groupPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.buttonTablePrefixAdd);
            this.groupPanel2.Controls.Add(this.textBoxTablePrefix);
            this.groupPanel2.Controls.Add(this.listBoxTablePrefix);
            this.groupPanel2.Controls.Add(this.buttonTablePrefixDelete);
            this.groupPanel2.Location = new System.Drawing.Point(6, 171);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(186, 179);
            // 
            // 
            // 
            this.groupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel2.Style.BackColorGradientAngle = 90;
            this.groupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderBottomWidth = 1;
            this.groupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderLeftWidth = 1;
            this.groupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderRightWidth = 1;
            this.groupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderTopWidth = 1;
            this.groupPanel2.Style.CornerDiameter = 4;
            this.groupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            this.groupPanel2.TabIndex = 1;
            this.groupPanel2.Text = "Table Prefixes";
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.buttonAddDatabase);
            this.groupPanel1.Controls.Add(this.listBoxDatabase);
            this.groupPanel1.Controls.Add(this.buttonEdit);
            this.groupPanel1.Controls.Add(this.buttonRemove);
            this.groupPanel1.Location = new System.Drawing.Point(6, 3);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(184, 162);
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
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            this.groupPanel1.TabIndex = 0;
            this.groupPanel1.Text = "Databases";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(215, 16);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(408, 117);
            this.label4.TabIndex = 17;
            this.label4.Text = resources.GetString("label4.Text");
            // 
            // SetupDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelEx1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SetupDatabase";
            this.NavBarIcon = ((System.Drawing.Image)(resources.GetObject("$this.NavBarIcon")));
            this.NavBarIconTransparentColor = System.Drawing.Color.Magenta;
            this.Size = new System.Drawing.Size(623, 372);
            this.contextMenuStripPrefix.ResumeLayout(false);
            this.contextMenuStripDatabase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.groupPanel4.ResumeLayout(false);
            this.groupPanel4.PerformLayout();
            this.groupPanel3.ResumeLayout(false);
            this.groupPanel3.PerformLayout();
            this.groupPanel2.ResumeLayout(false);
            this.groupPanel2.PerformLayout();
            this.groupPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonStoredProcedurePrefixAdd;
        private System.Windows.Forms.Button buttonViewPrefixAdd;
        private System.Windows.Forms.Button buttonTablePrefixAdd;
        private System.Windows.Forms.ListBox listBoxDatabase;
        private System.Windows.Forms.TextBox textBoxTablePrefix;
        private System.Windows.Forms.ListBox listBoxTablePrefix;
        private System.Windows.Forms.ListBox listBoxStoredProcedurePrefix;
        private System.Windows.Forms.TextBox textBoxViewPrefix;
        private System.Windows.Forms.TextBox textBoxStoredProcedurePrefix;
        private System.Windows.Forms.ListBox listBoxViewPrefix;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripPrefix;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeletePrefix;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripDatabase;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDatabaseDelete;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Button buttonAddDatabase;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonTablePrefixDelete;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button buttonStoredProcedurePrefixDelete;
        private System.Windows.Forms.Button buttonViewPrefixDelete;
        private System.Windows.Forms.ToolTip toolTip1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel4;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel3;
        private System.Windows.Forms.Label label4;
    }
}
