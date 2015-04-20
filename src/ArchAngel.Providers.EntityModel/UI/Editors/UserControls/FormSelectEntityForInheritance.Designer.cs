namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
	partial class FormSelectEntityForInheritance
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Test");
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("test2");
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSelectEntityForInheritance));
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Test");
			System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("test2");
			this.superTooltip1 = new DevComponents.DotNetBar.SuperTooltip();
			this.buttonOk = new DevComponents.DotNetBar.ButtonX();
			this.buttonCancel = new DevComponents.DotNetBar.ButtonX();
			this.listViewEntities = new DevComponents.DotNetBar.Controls.ListViewEx();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
			this.labelWarningSelectEntity = new DevComponents.DotNetBar.LabelX();
			this.labelSelectEntitiesHeading = new DevComponents.DotNetBar.LabelX();
			this.labelSelectText = new DevComponents.DotNetBar.LabelX();
			this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
			this.buttonClearAllProperties = new DevComponents.DotNetBar.ButtonX();
			this.buttonSelectAllProperties = new DevComponents.DotNetBar.ButtonX();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.labelX1 = new DevComponents.DotNetBar.LabelX();
			this.labelX4 = new DevComponents.DotNetBar.LabelX();
			this.labelX6 = new DevComponents.DotNetBar.LabelX();
			this.listViewProperties = new DevComponents.DotNetBar.Controls.ListViewEx();
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.panel2 = new System.Windows.Forms.Panel();
			this.superTabControl1 = new DevComponents.DotNetBar.SuperTabControl();
			this.superTabControlPanel1 = new DevComponents.DotNetBar.SuperTabControlPanel();
			this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
			this.inheritanceHierarchy1 = new ArchAngel.Providers.EntityModel.UI.Editors.UserControls.InheritanceHierarchy();
			this.labelHierarchyWarning = new DevComponents.DotNetBar.LabelX();
			this.labelHierarchyHeader = new DevComponents.DotNetBar.LabelX();
			this.tabCreateHierarchy = new DevComponents.DotNetBar.SuperTabItem();
			this.superTabControlPanel2 = new DevComponents.DotNetBar.SuperTabControlPanel();
			this.tabCreateAbstractParent = new DevComponents.DotNetBar.SuperTabItem();
			this.superTabControlPanel3 = new DevComponents.DotNetBar.SuperTabControlPanel();
			this.tabSelectEntity = new DevComponents.DotNetBar.SuperTabItem();
			this.panelEx2.SuspendLayout();
			this.panelEx3.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.superTabControl1)).BeginInit();
			this.superTabControl1.SuspendLayout();
			this.superTabControlPanel1.SuspendLayout();
			this.panelEx4.SuspendLayout();
			this.superTabControlPanel2.SuspendLayout();
			this.superTabControlPanel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// superTooltip1
			// 
			this.superTooltip1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// buttonOk
			// 
			this.buttonOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOk.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonOk.Location = new System.Drawing.Point(577, 14);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(75, 23);
			this.buttonOk.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonOk.TabIndex = 15;
			this.buttonOk.Text = "Ok";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(658, 14);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonCancel.TabIndex = 16;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// listViewEntities
			// 
			this.listViewEntities.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			// 
			// 
			// 
			this.listViewEntities.Border.Class = "ListViewBorder";
			this.listViewEntities.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.listViewEntities.CheckBoxes = true;
			this.listViewEntities.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.listViewEntities.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			listViewItem1.StateImageIndex = 0;
			listViewItem2.StateImageIndex = 0;
			this.listViewEntities.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
			this.listViewEntities.Location = new System.Drawing.Point(16, 76);
			this.listViewEntities.Name = "listViewEntities";
			this.listViewEntities.Size = new System.Drawing.Size(575, 317);
			this.listViewEntities.TabIndex = 17;
			this.listViewEntities.UseCompatibleStateImageBehavior = false;
			this.listViewEntities.View = System.Windows.Forms.View.Details;
			this.listViewEntities.SizeChanged += new System.EventHandler(this.listViewEx1_SizeChanged);
			// 
			// textBoxName
			// 
			this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxName.Location = new System.Drawing.Point(41, 3);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(115, 20);
			this.textBoxName.TabIndex = 18;
			this.textBoxName.Text = "NewName";
			// 
			// panelEx2
			// 
			this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx2.Controls.Add(this.labelWarningSelectEntity);
			this.panelEx2.Controls.Add(this.labelSelectEntitiesHeading);
			this.panelEx2.Controls.Add(this.labelSelectText);
			this.panelEx2.Controls.Add(this.listViewEntities);
			this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx2.Location = new System.Drawing.Point(0, 0);
			this.panelEx2.Name = "panelEx2";
			this.panelEx2.Size = new System.Drawing.Size(613, 399);
			this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx2.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
			this.panelEx2.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
			this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx2.Style.BorderColor.Color = System.Drawing.Color.Black;
			this.panelEx2.Style.CornerDiameter = 3;
			this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx2.Style.GradientAngle = 90;
			this.panelEx2.TabIndex = 20;
			// 
			// labelWarningSelectEntity
			// 
			this.labelWarningSelectEntity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			// 
			// 
			// 
			this.labelWarningSelectEntity.BackgroundStyle.Class = "";
			this.labelWarningSelectEntity.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelWarningSelectEntity.ForeColor = System.Drawing.Color.White;
			this.labelWarningSelectEntity.Image = ((System.Drawing.Image)(resources.GetObject("labelWarningSelectEntity.Image")));
			this.labelWarningSelectEntity.Location = new System.Drawing.Point(16, 38);
			this.labelWarningSelectEntity.Name = "labelWarningSelectEntity";
			this.labelWarningSelectEntity.Size = new System.Drawing.Size(583, 20);
			this.labelWarningSelectEntity.TabIndex = 29;
			this.labelWarningSelectEntity.Text = "  This entity has no 1:1 relationships";
			// 
			// labelSelectEntitiesHeading
			// 
			this.labelSelectEntitiesHeading.AutoSize = true;
			// 
			// 
			// 
			this.labelSelectEntitiesHeading.BackgroundStyle.Class = "";
			this.labelSelectEntitiesHeading.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelSelectEntitiesHeading.ForeColor = System.Drawing.Color.White;
			this.labelSelectEntitiesHeading.Image = ((System.Drawing.Image)(resources.GetObject("labelSelectEntitiesHeading.Image")));
			this.labelSelectEntitiesHeading.Location = new System.Drawing.Point(16, 12);
			this.labelSelectEntitiesHeading.Name = "labelSelectEntitiesHeading";
			this.labelSelectEntitiesHeading.Size = new System.Drawing.Size(275, 20);
			this.labelSelectEntitiesHeading.TabIndex = 25;
			this.labelSelectEntitiesHeading.Text = "  Table Per SubClass inheritance  - 1:1 relationships";
			// 
			// labelSelectText
			// 
			this.labelSelectText.AutoSize = true;
			// 
			// 
			// 
			this.labelSelectText.BackgroundStyle.Class = "";
			this.labelSelectText.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelSelectText.ForeColor = System.Drawing.Color.White;
			this.labelSelectText.Location = new System.Drawing.Point(16, 55);
			this.labelSelectText.Name = "labelSelectText";
			this.labelSelectText.Size = new System.Drawing.Size(139, 15);
			this.labelSelectText.TabIndex = 24;
			this.labelSelectText.Text = "Select an existing 1:1 entity:";
			// 
			// panelEx3
			// 
			this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx3.Controls.Add(this.buttonClearAllProperties);
			this.panelEx3.Controls.Add(this.buttonSelectAllProperties);
			this.panelEx3.Controls.Add(this.flowLayoutPanel1);
			this.panelEx3.Controls.Add(this.labelX4);
			this.panelEx3.Controls.Add(this.labelX6);
			this.panelEx3.Controls.Add(this.listViewProperties);
			this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx3.Location = new System.Drawing.Point(0, 0);
			this.panelEx3.Name = "panelEx3";
			this.panelEx3.Size = new System.Drawing.Size(613, 399);
			this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx3.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
			this.panelEx3.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
			this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx3.Style.BorderColor.Color = System.Drawing.Color.Black;
			this.panelEx3.Style.CornerDiameter = 3;
			this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx3.Style.GradientAngle = 90;
			this.panelEx3.TabIndex = 21;
			// 
			// buttonClearAllProperties
			// 
			this.buttonClearAllProperties.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonClearAllProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClearAllProperties.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonClearAllProperties.Location = new System.Drawing.Point(528, 129);
			this.buttonClearAllProperties.Name = "buttonClearAllProperties";
			this.buttonClearAllProperties.Size = new System.Drawing.Size(65, 23);
			this.buttonClearAllProperties.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonClearAllProperties.TabIndex = 30;
			this.buttonClearAllProperties.Text = "Clear All";
			this.buttonClearAllProperties.Click += new System.EventHandler(this.buttonClearAllProperties_Click);
			// 
			// buttonSelectAllProperties
			// 
			this.buttonSelectAllProperties.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonSelectAllProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSelectAllProperties.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonSelectAllProperties.Location = new System.Drawing.Point(528, 100);
			this.buttonSelectAllProperties.Name = "buttonSelectAllProperties";
			this.buttonSelectAllProperties.Size = new System.Drawing.Size(65, 23);
			this.buttonSelectAllProperties.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonSelectAllProperties.TabIndex = 29;
			this.buttonSelectAllProperties.Text = "Select All";
			this.buttonSelectAllProperties.Click += new System.EventHandler(this.buttonSelectAllProperties_Click);
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.labelX1);
			this.flowLayoutPanel1.Controls.Add(this.textBoxName);
			this.flowLayoutPanel1.Location = new System.Drawing.Point(10, 48);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(466, 25);
			this.flowLayoutPanel1.TabIndex = 28;
			// 
			// labelX1
			// 
			this.labelX1.AutoSize = true;
			// 
			// 
			// 
			this.labelX1.BackgroundStyle.Class = "";
			this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX1.ForeColor = System.Drawing.Color.White;
			this.labelX1.Location = new System.Drawing.Point(3, 3);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(32, 15);
			this.labelX1.TabIndex = 27;
			this.labelX1.Text = "Name";
			// 
			// labelX4
			// 
			this.labelX4.AutoSize = true;
			// 
			// 
			// 
			this.labelX4.BackgroundStyle.Class = "";
			this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX4.ForeColor = System.Drawing.Color.White;
			this.labelX4.Image = ((System.Drawing.Image)(resources.GetObject("labelX4.Image")));
			this.labelX4.Location = new System.Drawing.Point(23, 12);
			this.labelX4.Name = "labelX4";
			this.labelX4.Size = new System.Drawing.Size(342, 20);
			this.labelX4.TabIndex = 26;
			this.labelX4.Text = "  Table Per Concrete Class inheritance - create an abstract parent";
			// 
			// labelX6
			// 
			this.labelX6.AutoSize = true;
			// 
			// 
			// 
			this.labelX6.BackgroundStyle.Class = "";
			this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX6.ForeColor = System.Drawing.Color.White;
			this.labelX6.Location = new System.Drawing.Point(10, 79);
			this.labelX6.Name = "labelX6";
			this.labelX6.Size = new System.Drawing.Size(232, 15);
			this.labelX6.TabIndex = 23;
			this.labelX6.Text = "Select properties to promote to abstract parent:";
			// 
			// listViewProperties
			// 
			this.listViewProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			// 
			// 
			// 
			this.listViewProperties.Border.Class = "ListViewBorder";
			this.listViewProperties.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.listViewProperties.CheckBoxes = true;
			this.listViewProperties.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
			this.listViewProperties.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			listViewItem3.StateImageIndex = 0;
			listViewItem4.StateImageIndex = 0;
			this.listViewProperties.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem3,
            listViewItem4});
			this.listViewProperties.Location = new System.Drawing.Point(10, 100);
			this.listViewProperties.Name = "listViewProperties";
			this.listViewProperties.Size = new System.Drawing.Size(512, 293);
			this.listViewProperties.TabIndex = 22;
			this.listViewProperties.UseCompatibleStateImageBehavior = false;
			this.listViewProperties.View = System.Windows.Forms.View.Details;
			this.listViewProperties.SizeChanged += new System.EventHandler(this.listViewProperties_SizeChanged);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.buttonCancel);
			this.panel2.Controls.Add(this.buttonOk);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 399);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(745, 49);
			this.panel2.TabIndex = 24;
			// 
			// superTabControl1
			// 
			// 
			// 
			// 
			// 
			// 
			// 
			this.superTabControl1.ControlBox.CloseBox.Name = "";
			// 
			// 
			// 
			this.superTabControl1.ControlBox.MenuBox.Name = "";
			this.superTabControl1.ControlBox.Name = "";
			this.superTabControl1.ControlBox.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabControl1.ControlBox.MenuBox,
            this.superTabControl1.ControlBox.CloseBox});
			this.superTabControl1.ControlBox.Visible = false;
			this.superTabControl1.Controls.Add(this.superTabControlPanel3);
			this.superTabControl1.Controls.Add(this.superTabControlPanel1);
			this.superTabControl1.Controls.Add(this.superTabControlPanel2);
			this.superTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.superTabControl1.Location = new System.Drawing.Point(0, 0);
			this.superTabControl1.Name = "superTabControl1";
			this.superTabControl1.ReorderTabsEnabled = true;
			this.superTabControl1.SelectedTabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.superTabControl1.SelectedTabIndex = 2;
			this.superTabControl1.Size = new System.Drawing.Size(745, 399);
			this.superTabControl1.TabAlignment = DevComponents.DotNetBar.eTabStripAlignment.Left;
			this.superTabControl1.TabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.superTabControl1.TabIndex = 25;
			this.superTabControl1.Tabs.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.tabSelectEntity,
            this.tabCreateHierarchy,
            this.tabCreateAbstractParent});
			this.superTabControl1.TabStyle = DevComponents.DotNetBar.eSuperTabStyle.Office2010BackstageBlue;
			this.superTabControl1.Text = "Select entity";
			this.superTabControl1.SelectedTabChanged += new System.EventHandler<DevComponents.DotNetBar.SuperTabStripSelectedTabChangedEventArgs>(this.superTabControl1_SelectedTabChanged);
			// 
			// superTabControlPanel1
			// 
			this.superTabControlPanel1.Controls.Add(this.panelEx4);
			this.superTabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.superTabControlPanel1.Location = new System.Drawing.Point(132, 0);
			this.superTabControlPanel1.Name = "superTabControlPanel1";
			this.superTabControlPanel1.Size = new System.Drawing.Size(613, 399);
			this.superTabControlPanel1.TabIndex = 1;
			this.superTabControlPanel1.TabItem = this.tabCreateHierarchy;
			// 
			// panelEx4
			// 
			this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx4.Controls.Add(this.inheritanceHierarchy1);
			this.panelEx4.Controls.Add(this.labelHierarchyWarning);
			this.panelEx4.Controls.Add(this.labelHierarchyHeader);
			this.panelEx4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx4.Location = new System.Drawing.Point(0, 0);
			this.panelEx4.Name = "panelEx4";
			this.panelEx4.Size = new System.Drawing.Size(613, 399);
			this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx4.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
			this.panelEx4.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
			this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx4.Style.BorderColor.Color = System.Drawing.Color.Black;
			this.panelEx4.Style.CornerDiameter = 3;
			this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx4.Style.GradientAngle = 90;
			this.panelEx4.TabIndex = 21;
			// 
			// inheritanceHierarchy1
			// 
			this.inheritanceHierarchy1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.inheritanceHierarchy1.Location = new System.Drawing.Point(0, 48);
			this.inheritanceHierarchy1.Name = "inheritanceHierarchy1";
			this.inheritanceHierarchy1.Size = new System.Drawing.Size(613, 351);
			this.inheritanceHierarchy1.TabIndex = 22;
			this.inheritanceHierarchy1.Table = null;
			// 
			// labelHierarchyWarning
			// 
			this.labelHierarchyWarning.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			// 
			// 
			// 
			this.labelHierarchyWarning.BackgroundStyle.Class = "";
			this.labelHierarchyWarning.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelHierarchyWarning.ForeColor = System.Drawing.Color.White;
			this.labelHierarchyWarning.Image = ((System.Drawing.Image)(resources.GetObject("labelHierarchyWarning.Image")));
			this.labelHierarchyWarning.Location = new System.Drawing.Point(18, 49);
			this.labelHierarchyWarning.Name = "labelHierarchyWarning";
			this.labelHierarchyWarning.Size = new System.Drawing.Size(583, 20);
			this.labelHierarchyWarning.TabIndex = 28;
			this.labelHierarchyWarning.Text = "  Can\'t create hierarchy - entity must be mapped to exactly 1 table.";
			// 
			// labelHierarchyHeader
			// 
			this.labelHierarchyHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			// 
			// 
			// 
			this.labelHierarchyHeader.BackgroundStyle.Class = "";
			this.labelHierarchyHeader.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelHierarchyHeader.ForeColor = System.Drawing.Color.White;
			this.labelHierarchyHeader.Image = ((System.Drawing.Image)(resources.GetObject("labelHierarchyHeader.Image")));
			this.labelHierarchyHeader.Location = new System.Drawing.Point(18, 3);
			this.labelHierarchyHeader.Name = "labelHierarchyHeader";
			this.labelHierarchyHeader.Size = new System.Drawing.Size(583, 40);
			this.labelHierarchyHeader.TabIndex = 27;
			this.labelHierarchyHeader.Text = "  Create an inheritance hierarchy from a single table, based on a discriminator.";
			this.labelHierarchyHeader.WordWrap = true;
			// 
			// tabCreateHierarchy
			// 
			this.tabCreateHierarchy.AttachedControl = this.superTabControlPanel1;
			this.tabCreateHierarchy.GlobalItem = false;
			this.tabCreateHierarchy.Name = "tabCreateHierarchy";
			this.tabCreateHierarchy.Text = "Create hierarchy";
			// 
			// superTabControlPanel2
			// 
			this.superTabControlPanel2.Controls.Add(this.panelEx3);
			this.superTabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.superTabControlPanel2.Location = new System.Drawing.Point(132, 0);
			this.superTabControlPanel2.Name = "superTabControlPanel2";
			this.superTabControlPanel2.Size = new System.Drawing.Size(613, 399);
			this.superTabControlPanel2.TabIndex = 0;
			this.superTabControlPanel2.TabItem = this.tabCreateAbstractParent;
			// 
			// tabCreateAbstractParent
			// 
			this.tabCreateAbstractParent.AttachedControl = this.superTabControlPanel2;
			this.tabCreateAbstractParent.GlobalItem = false;
			this.tabCreateAbstractParent.Name = "tabCreateAbstractParent";
			this.tabCreateAbstractParent.Text = "Create abstract parent";
			this.tabCreateAbstractParent.GotFocus += new System.EventHandler(this.tabCreateAbstractParent_GotFocus);
			// 
			// superTabControlPanel3
			// 
			this.superTabControlPanel3.Controls.Add(this.panelEx2);
			this.superTabControlPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.superTabControlPanel3.Location = new System.Drawing.Point(132, 0);
			this.superTabControlPanel3.Name = "superTabControlPanel3";
			this.superTabControlPanel3.Size = new System.Drawing.Size(613, 399);
			this.superTabControlPanel3.TabIndex = 0;
			this.superTabControlPanel3.TabItem = this.tabSelectEntity;
			// 
			// tabSelectEntity
			// 
			this.tabSelectEntity.AttachedControl = this.superTabControlPanel3;
			this.tabSelectEntity.GlobalItem = false;
			this.tabSelectEntity.Name = "tabSelectEntity";
			this.tabSelectEntity.Text = "Select entity";
			// 
			// FormSelectEntityForInheritance
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(745, 448);
			this.Controls.Add(this.superTabControl1);
			this.Controls.Add(this.panel2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MinimumSize = new System.Drawing.Size(507, 424);
			this.Name = "FormSelectEntityForInheritance";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Inheritance Editor";
			this.Load += new System.EventHandler(this.FormSelectEntity_Load);
			this.panelEx2.ResumeLayout(false);
			this.panelEx2.PerformLayout();
			this.panelEx3.ResumeLayout(false);
			this.panelEx3.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.superTabControl1)).EndInit();
			this.superTabControl1.ResumeLayout(false);
			this.superTabControlPanel1.ResumeLayout(false);
			this.panelEx4.ResumeLayout(false);
			this.superTabControlPanel2.ResumeLayout(false);
			this.superTabControlPanel3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.SuperTooltip superTooltip1;
		private DevComponents.DotNetBar.ButtonX buttonOk;
		private DevComponents.DotNetBar.ButtonX buttonCancel;
		private DevComponents.DotNetBar.Controls.ListViewEx listViewEntities;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.TextBox textBoxName;
		private DevComponents.DotNetBar.PanelEx panelEx2;
		private DevComponents.DotNetBar.PanelEx panelEx3;
		private System.Windows.Forms.Panel panel2;
		private DevComponents.DotNetBar.SuperTabControl superTabControl1;
		private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel2;
		private DevComponents.DotNetBar.SuperTabItem tabCreateAbstractParent;
		private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel1;
		private DevComponents.DotNetBar.SuperTabItem tabCreateHierarchy;
		private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel3;
		private DevComponents.DotNetBar.SuperTabItem tabSelectEntity;
		private DevComponents.DotNetBar.LabelX labelX6;
		private DevComponents.DotNetBar.Controls.ListViewEx listViewProperties;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private DevComponents.DotNetBar.LabelX labelSelectText;
		private DevComponents.DotNetBar.PanelEx panelEx4;
		private DevComponents.DotNetBar.LabelX labelSelectEntitiesHeading;
		private DevComponents.DotNetBar.LabelX labelX4;
		private DevComponents.DotNetBar.LabelX labelX1;
		private InheritanceHierarchy inheritanceHierarchy1;
		private DevComponents.DotNetBar.LabelX labelHierarchyHeader;
		private DevComponents.DotNetBar.LabelX labelHierarchyWarning;
		private DevComponents.DotNetBar.LabelX labelWarningSelectEntity;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private DevComponents.DotNetBar.ButtonX buttonClearAllProperties;
		private DevComponents.DotNetBar.ButtonX buttonSelectAllProperties;
	}
}