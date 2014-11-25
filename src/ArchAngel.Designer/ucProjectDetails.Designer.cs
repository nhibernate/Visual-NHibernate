namespace ArchAngel.Designer
{
	partial class ucProjectDetails
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucProjectDetails));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.btnCancelNamespace = new System.Windows.Forms.Button();
			this.txtNamespace = new System.Windows.Forms.TextBox();
			this.lstNamespaces = new System.Windows.Forms.ListBox();
			this.btnAddNamespace = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.btnOutputFile = new System.Windows.Forms.Button();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.label4 = new System.Windows.Forms.Label();
			this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
			this.buttonDeleteReference = new DevComponents.DotNetBar.ButtonX();
			this.buttonEditReference = new DevComponents.DotNetBar.ButtonX();
			this.btnAddReferencedFile = new DevComponents.DotNetBar.ButtonX();
			this.gridReferencedFiles = new DevComponents.DotNetBar.Controls.DataGridViewX();
			this.colRefFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colRefDisplay = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.colRefPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
			this.superTooltip1 = new DevComponents.DotNetBar.SuperTooltip();
			this.expandablePanel2 = new DevComponents.DotNetBar.ExpandablePanel();
			this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
			this.txtVersion = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.txtDescription = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.txtOutputFile = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.txtName = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.labelX5 = new DevComponents.DotNetBar.LabelX();
			this.labelX4 = new DevComponents.DotNetBar.LabelX();
			this.labelX3 = new DevComponents.DotNetBar.LabelX();
			this.labelX2 = new DevComponents.DotNetBar.LabelX();
			this.elementStyle3 = new DevComponents.DotNetBar.ElementStyle();
			this.elementStyle2 = new DevComponents.DotNetBar.ElementStyle();
			this.expandablePanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridReferencedFiles)).BeginInit();
			this.expandablePanel2.SuspendLayout();
			this.panelEx1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnCancelNamespace
			// 
			this.btnCancelNamespace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancelNamespace.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCancelNamespace.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelNamespace.Image")));
			this.btnCancelNamespace.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnCancelNamespace.Location = new System.Drawing.Point(633, 55);
			this.btnCancelNamespace.Margin = new System.Windows.Forms.Padding(2);
			this.btnCancelNamespace.Name = "btnCancelNamespace";
			this.btnCancelNamespace.Size = new System.Drawing.Size(24, 24);
			this.btnCancelNamespace.TabIndex = 13;
			this.btnCancelNamespace.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.toolTip1.SetToolTip(this.btnCancelNamespace, "Remove selected namespace");
			this.btnCancelNamespace.UseVisualStyleBackColor = true;
			this.btnCancelNamespace.Click += new System.EventHandler(this.btnCancelNamespace_Click);
			// 
			// txtNamespace
			// 
			this.txtNamespace.Location = new System.Drawing.Point(126, 33);
			this.txtNamespace.Margin = new System.Windows.Forms.Padding(2);
			this.txtNamespace.Name = "txtNamespace";
			this.txtNamespace.Size = new System.Drawing.Size(222, 20);
			this.txtNamespace.TabIndex = 8;
			this.txtNamespace.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNamespace_KeyDown);
			// 
			// lstNamespaces
			// 
			this.lstNamespaces.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lstNamespaces.FormattingEnabled = true;
			this.lstNamespaces.Location = new System.Drawing.Point(6, 55);
			this.lstNamespaces.Margin = new System.Windows.Forms.Padding(2);
			this.lstNamespaces.Name = "lstNamespaces";
			this.lstNamespaces.Size = new System.Drawing.Size(623, 95);
			this.lstNamespaces.Sorted = true;
			this.lstNamespaces.TabIndex = 7;
			this.lstNamespaces.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstNamespaces_KeyDown);
			// 
			// btnAddNamespace
			// 
			this.btnAddNamespace.Image = ((System.Drawing.Image)(resources.GetObject("btnAddNamespace.Image")));
			this.btnAddNamespace.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnAddNamespace.Location = new System.Drawing.Point(352, 30);
			this.btnAddNamespace.Margin = new System.Windows.Forms.Padding(2);
			this.btnAddNamespace.Name = "btnAddNamespace";
			this.btnAddNamespace.Size = new System.Drawing.Size(24, 24);
			this.btnAddNamespace.TabIndex = 6;
			this.btnAddNamespace.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.toolTip1.SetToolTip(this.btnAddNamespace, "Add new namespace");
			this.btnAddNamespace.UseVisualStyleBackColor = true;
			this.btnAddNamespace.Click += new System.EventHandler(this.btnAddNamespace_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// btnOutputFile
			// 
			this.btnOutputFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOutputFile.Image = ((System.Drawing.Image)(resources.GetObject("btnOutputFile.Image")));
			this.btnOutputFile.Location = new System.Drawing.Point(612, 70);
			this.btnOutputFile.Margin = new System.Windows.Forms.Padding(2);
			this.btnOutputFile.Name = "btnOutputFile";
			this.btnOutputFile.Size = new System.Drawing.Size(25, 25);
			this.btnOutputFile.TabIndex = 16;
			this.btnOutputFile.UseVisualStyleBackColor = true;
			this.btnOutputFile.Click += new System.EventHandler(this.btnOutputFile_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(20, 36);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(87, 13);
			this.label4.TabIndex = 14;
			this.label4.Text = "Add namespace:";
			// 
			// expandablePanel1
			// 
			this.expandablePanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.expandablePanel1.CanvasColor = System.Drawing.SystemColors.Control;
			this.expandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
			this.expandablePanel1.Controls.Add(this.buttonDeleteReference);
			this.expandablePanel1.Controls.Add(this.buttonEditReference);
			this.expandablePanel1.Controls.Add(this.btnAddReferencedFile);
			this.expandablePanel1.Controls.Add(this.gridReferencedFiles);
			this.expandablePanel1.ExpandButtonVisible = false;
			this.expandablePanel1.Location = new System.Drawing.Point(32, 232);
			this.expandablePanel1.Name = "expandablePanel1";
			this.expandablePanel1.Size = new System.Drawing.Size(674, 135);
			this.expandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.expandablePanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.expandablePanel1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.expandablePanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.expandablePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
			this.expandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.expandablePanel1.Style.GradientAngle = 90;
			this.expandablePanel1.TabIndex = 21;
			this.expandablePanel1.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.expandablePanel1.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.expandablePanel1.TitleStyle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("expandablePanel1.TitleStyle.BackgroundImage")));
			this.expandablePanel1.TitleStyle.BackgroundImagePosition = DevComponents.DotNetBar.eBackgroundImagePosition.CenterLeft;
			this.expandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
			this.expandablePanel1.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandablePanel1.TitleStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.expandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.expandablePanel1.TitleStyle.GradientAngle = 90;
			this.expandablePanel1.TitleStyle.MarginLeft = 30;
			this.expandablePanel1.TitleText = "Referenced Files";
			// 
			// buttonDeleteReference
			// 
			this.buttonDeleteReference.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonDeleteReference.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonDeleteReference.AutoSize = true;
			this.buttonDeleteReference.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonDeleteReference.Image = global::ArchAngel.Designer.Properties.Resources.edit_16_h;
			this.buttonDeleteReference.Location = new System.Drawing.Point(579, 0);
			this.buttonDeleteReference.Name = "buttonDeleteReference";
			this.buttonDeleteReference.Size = new System.Drawing.Size(92, 23);
			this.buttonDeleteReference.TabIndex = 16;
			this.buttonDeleteReference.Text = "Delete";
			this.buttonDeleteReference.Click += new System.EventHandler(this.buttonDeleteReference_Click);
			// 
			// buttonEditReference
			// 
			this.buttonEditReference.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonEditReference.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonEditReference.AutoSize = true;
			this.buttonEditReference.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonEditReference.Image = global::ArchAngel.Designer.Properties.Resources.edit_16_h;
			this.buttonEditReference.Location = new System.Drawing.Point(481, 0);
			this.buttonEditReference.Name = "buttonEditReference";
			this.buttonEditReference.Size = new System.Drawing.Size(92, 23);
			this.buttonEditReference.TabIndex = 15;
			this.buttonEditReference.Text = "Edit";
			this.buttonEditReference.Click += new System.EventHandler(this.buttonEditReference_Click);
			// 
			// btnAddReferencedFile
			// 
			this.btnAddReferencedFile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnAddReferencedFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddReferencedFile.AutoSize = true;
			this.btnAddReferencedFile.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnAddReferencedFile.Image = ((System.Drawing.Image)(resources.GetObject("btnAddReferencedFile.Image")));
			this.btnAddReferencedFile.Location = new System.Drawing.Point(383, 0);
			this.btnAddReferencedFile.Name = "btnAddReferencedFile";
			this.btnAddReferencedFile.Size = new System.Drawing.Size(92, 23);
			this.btnAddReferencedFile.TabIndex = 14;
			this.btnAddReferencedFile.Text = "Add file";
			this.btnAddReferencedFile.Click += new System.EventHandler(this.btnAddReferencedFile_Click);
			// 
			// gridReferencedFiles
			// 
			this.gridReferencedFiles.AllowUserToAddRows = false;
			this.gridReferencedFiles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.gridReferencedFiles.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			this.gridReferencedFiles.ColumnHeadersHeight = 20;
			this.gridReferencedFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRefFile,
            this.colRefDisplay,
            this.colRefPath});
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.gridReferencedFiles.DefaultCellStyle = dataGridViewCellStyle2;
			this.gridReferencedFiles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridReferencedFiles.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
			this.gridReferencedFiles.Location = new System.Drawing.Point(0, 26);
			this.gridReferencedFiles.MultiSelect = false;
			this.gridReferencedFiles.Name = "gridReferencedFiles";
			this.gridReferencedFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.gridReferencedFiles.Size = new System.Drawing.Size(674, 109);
			this.gridReferencedFiles.TabIndex = 13;
			this.gridReferencedFiles.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.gridReferencedFiles_UserDeletedRow);
			this.gridReferencedFiles.SelectionChanged += new System.EventHandler(this.gridReferencedFiles_SelectionChanged);
			// 
			// colRefFile
			// 
			this.colRefFile.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.colRefFile.HeaderText = "File";
			this.colRefFile.Name = "colRefFile";
			this.colRefFile.ReadOnly = true;
			this.colRefFile.Width = 48;
			// 
			// colRefDisplay
			// 
			this.colRefDisplay.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.colRefDisplay.HeaderText = "Display in Workbench";
			this.colRefDisplay.Name = "colRefDisplay";
			this.colRefDisplay.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.colRefDisplay.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.colRefDisplay.Width = 136;
			// 
			// colRefPath
			// 
			this.colRefPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.colRefPath.HeaderText = "Path";
			this.colRefPath.Name = "colRefPath";
			this.colRefPath.ReadOnly = true;
			this.colRefPath.Width = 54;
			// 
			// elementStyle1
			// 
			this.elementStyle1.Name = "elementStyle1";
			this.elementStyle1.TextColor = System.Drawing.SystemColors.ControlText;
			// 
			// superTooltip1
			// 
			this.superTooltip1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// expandablePanel2
			// 
			this.expandablePanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.expandablePanel2.CanvasColor = System.Drawing.SystemColors.Control;
			this.expandablePanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
			this.expandablePanel2.Controls.Add(this.label4);
			this.expandablePanel2.Controls.Add(this.txtNamespace);
			this.expandablePanel2.Controls.Add(this.btnCancelNamespace);
			this.expandablePanel2.Controls.Add(this.btnAddNamespace);
			this.expandablePanel2.Controls.Add(this.lstNamespaces);
			this.expandablePanel2.ExpandButtonVisible = false;
			this.expandablePanel2.Location = new System.Drawing.Point(32, 386);
			this.expandablePanel2.Name = "expandablePanel2";
			this.expandablePanel2.Size = new System.Drawing.Size(674, 172);
			this.expandablePanel2.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.expandablePanel2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.expandablePanel2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.expandablePanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.expandablePanel2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
			this.expandablePanel2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.expandablePanel2.Style.GradientAngle = 90;
			this.expandablePanel2.TabIndex = 22;
			this.expandablePanel2.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.expandablePanel2.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.expandablePanel2.TitleStyle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("expandablePanel2.TitleStyle.BackgroundImage")));
			this.expandablePanel2.TitleStyle.BackgroundImagePosition = DevComponents.DotNetBar.eBackgroundImagePosition.CenterLeft;
			this.expandablePanel2.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
			this.expandablePanel2.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandablePanel2.TitleStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.expandablePanel2.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.expandablePanel2.TitleStyle.GradientAngle = 90;
			this.expandablePanel2.TitleStyle.MarginLeft = 30;
			this.expandablePanel2.TitleText = "Namespaces";
			// 
			// panelEx1
			// 
			this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
			this.panelEx1.Controls.Add(this.txtVersion);
			this.panelEx1.Controls.Add(this.txtDescription);
			this.panelEx1.Controls.Add(this.txtOutputFile);
			this.panelEx1.Controls.Add(this.txtName);
			this.panelEx1.Controls.Add(this.labelX5);
			this.panelEx1.Controls.Add(this.labelX4);
			this.panelEx1.Controls.Add(this.btnOutputFile);
			this.panelEx1.Controls.Add(this.labelX3);
			this.panelEx1.Controls.Add(this.labelX2);
			this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx1.Location = new System.Drawing.Point(0, 0);
			this.panelEx1.Name = "panelEx1";
			this.panelEx1.Size = new System.Drawing.Size(742, 586);
			this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx1.Style.GradientAngle = 90;
			this.panelEx1.TabIndex = 23;
			this.panelEx1.Text = "panelEx1";
			// 
			// txtVersion
			// 
			// 
			// 
			// 
			this.txtVersion.Border.Class = "TextBoxBorder";
			this.txtVersion.Location = new System.Drawing.Point(525, 42);
			this.txtVersion.Name = "txtVersion";
			this.txtVersion.Size = new System.Drawing.Size(82, 20);
			this.txtVersion.TabIndex = 20;
			this.txtVersion.Validated += new System.EventHandler(this.txtVersion_Validated);
			this.txtVersion.TextChanged += new System.EventHandler(this.txtVersion_TextChanged);
			// 
			// txtDescription
			// 
			this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			// 
			// 
			// 
			this.txtDescription.Border.Class = "TextBoxBorder";
			this.txtDescription.Location = new System.Drawing.Point(144, 105);
			this.txtDescription.Multiline = true;
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtDescription.Size = new System.Drawing.Size(463, 107);
			this.txtDescription.TabIndex = 19;
			this.txtDescription.Validated += new System.EventHandler(this.txtDescription_Validated);
			this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged_1);
			// 
			// txtOutputFile
			// 
			this.txtOutputFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			// 
			// 
			// 
			this.txtOutputFile.Border.Class = "TextBoxBorder";
			this.txtOutputFile.Location = new System.Drawing.Point(145, 74);
			this.txtOutputFile.Name = "txtOutputFile";
			this.txtOutputFile.Size = new System.Drawing.Size(463, 20);
			this.txtOutputFile.TabIndex = 18;
			this.txtOutputFile.Validating += new System.ComponentModel.CancelEventHandler(this.txtOutputFile_Validating);
			this.txtOutputFile.Validated += new System.EventHandler(this.txtOutputFile_Validated);
			this.txtOutputFile.TextChanged += new System.EventHandler(this.txtOutputFile_TextChanged);
			// 
			// txtName
			// 
			// 
			// 
			// 
			this.txtName.Border.Class = "TextBoxBorder";
			this.txtName.Location = new System.Drawing.Point(146, 42);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(144, 20);
			this.txtName.TabIndex = 17;
			this.txtName.Validating += new System.ComponentModel.CancelEventHandler(this.txtName_Validating);
			this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
			// 
			// labelX5
			// 
			this.labelX5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelX5.ForeColor = System.Drawing.Color.Black;
			this.labelX5.Location = new System.Drawing.Point(468, 38);
			this.labelX5.Name = "labelX5";
			this.labelX5.Size = new System.Drawing.Size(51, 24);
			this.labelX5.TabIndex = 4;
			this.labelX5.Text = "Version";
			this.labelX5.TextAlignment = System.Drawing.StringAlignment.Far;
			// 
			// labelX4
			// 
			this.labelX4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelX4.ForeColor = System.Drawing.Color.Black;
			this.labelX4.Location = new System.Drawing.Point(74, 38);
			this.labelX4.Name = "labelX4";
			this.labelX4.Size = new System.Drawing.Size(65, 24);
			this.labelX4.TabIndex = 3;
			this.labelX4.Text = "Name";
			this.labelX4.TextAlignment = System.Drawing.StringAlignment.Far;
			// 
			// labelX3
			// 
			this.labelX3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelX3.ForeColor = System.Drawing.Color.Black;
			this.labelX3.Location = new System.Drawing.Point(97, 70);
			this.labelX3.Name = "labelX3";
			this.labelX3.Size = new System.Drawing.Size(42, 24);
			this.labelX3.TabIndex = 2;
			this.labelX3.Text = "Output";
			this.labelX3.TextAlignment = System.Drawing.StringAlignment.Far;
			// 
			// labelX2
			// 
			this.labelX2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelX2.ForeColor = System.Drawing.Color.Black;
			this.labelX2.Location = new System.Drawing.Point(73, 101);
			this.labelX2.Name = "labelX2";
			this.labelX2.Size = new System.Drawing.Size(65, 24);
			this.labelX2.TabIndex = 1;
			this.labelX2.Text = "Description";
			this.labelX2.TextAlignment = System.Drawing.StringAlignment.Far;
			// 
			// elementStyle3
			// 
			this.elementStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
			this.elementStyle3.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(168)))), ((int)(((byte)(228)))));
			this.elementStyle3.BackColorGradientAngle = 90;
			this.elementStyle3.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle3.BorderBottomWidth = 1;
			this.elementStyle3.BorderColor = System.Drawing.Color.DarkGray;
			this.elementStyle3.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle3.BorderLeftWidth = 1;
			this.elementStyle3.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle3.BorderRightWidth = 1;
			this.elementStyle3.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle3.BorderTopWidth = 1;
			this.elementStyle3.CornerDiameter = 4;
			this.elementStyle3.Description = "Blue";
			this.elementStyle3.Name = "elementStyle3";
			this.elementStyle3.PaddingBottom = 1;
			this.elementStyle3.PaddingLeft = 1;
			this.elementStyle3.PaddingRight = 1;
			this.elementStyle3.PaddingTop = 1;
			this.elementStyle3.TextColor = System.Drawing.Color.Black;
			// 
			// elementStyle2
			// 
			this.elementStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
			this.elementStyle2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(168)))), ((int)(((byte)(228)))));
			this.elementStyle2.BackColorGradientAngle = 90;
			this.elementStyle2.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle2.BorderBottomWidth = 1;
			this.elementStyle2.BorderColor = System.Drawing.Color.DarkGray;
			this.elementStyle2.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle2.BorderLeftWidth = 1;
			this.elementStyle2.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle2.BorderRightWidth = 1;
			this.elementStyle2.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.elementStyle2.BorderTopWidth = 1;
			this.elementStyle2.CornerDiameter = 4;
			this.elementStyle2.Description = "Blue";
			this.elementStyle2.Name = "elementStyle2";
			this.elementStyle2.PaddingBottom = 1;
			this.elementStyle2.PaddingLeft = 1;
			this.elementStyle2.PaddingRight = 1;
			this.elementStyle2.PaddingTop = 1;
			this.elementStyle2.TextColor = System.Drawing.Color.Black;
			// 
			// ucProjectDetails
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(219)))), ((int)(((byte)(250)))));
			this.Controls.Add(this.expandablePanel2);
			this.Controls.Add(this.expandablePanel1);
			this.Controls.Add(this.panelEx1);
			this.DoubleBuffered = true;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "ucProjectDetails";
			this.Size = new System.Drawing.Size(742, 586);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.ucProjectDetails_Paint);
			this.expandablePanel1.ResumeLayout(false);
			this.expandablePanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridReferencedFiles)).EndInit();
			this.expandablePanel2.ResumeLayout(false);
			this.expandablePanel2.PerformLayout();
			this.panelEx1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.TextBox txtNamespace;
        private System.Windows.Forms.ListBox lstNamespaces;
		private System.Windows.Forms.Button btnCancelNamespace;
		private System.Windows.Forms.Button btnAddNamespace;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Button btnOutputFile;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Label label4;
		private DevComponents.DotNetBar.ExpandablePanel expandablePanel1;
		private DevComponents.DotNetBar.SuperTooltip superTooltip1;
		private DevComponents.DotNetBar.ExpandablePanel expandablePanel2;
		private DevComponents.DotNetBar.PanelEx panelEx1;
		private DevComponents.DotNetBar.LabelX labelX2;
		private DevComponents.DotNetBar.LabelX labelX5;
		private DevComponents.DotNetBar.LabelX labelX4;
		private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtName;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
		private DevComponents.DotNetBar.ElementStyle elementStyle3;
		private DevComponents.DotNetBar.ElementStyle elementStyle2;
		private DevComponents.DotNetBar.Controls.TextBoxX txtOutputFile;
		private DevComponents.DotNetBar.Controls.TextBoxX txtDescription;
		private DevComponents.DotNetBar.Controls.TextBoxX txtVersion;
        private DevComponents.DotNetBar.Controls.DataGridViewX gridReferencedFiles;
        private DevComponents.DotNetBar.ButtonX btnAddReferencedFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRefFile;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colRefDisplay;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRefPath;
		private DevComponents.DotNetBar.ButtonX buttonEditReference;
		private DevComponents.DotNetBar.ButtonX buttonDeleteReference;
	}
}
