namespace ArchAngel.Designer
{
	partial class ucOptions
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucOptions));
			this.treeOptions = new DevComponents.AdvTree.AdvTree();
			this.columnHeader1 = new DevComponents.AdvTree.ColumnHeader();
			this.node1 = new DevComponents.AdvTree.Node();
			this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
			this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
			this.dotNetBarManager1 = new DevComponents.DotNetBar.DotNetBarManager(this.components);
			this.dockSite4 = new DevComponents.DotNetBar.DockSite();
			this.dockSite1 = new DevComponents.DotNetBar.DockSite();
			this.barOptions = new DevComponents.DotNetBar.Bar();
			this.panelDockContainer2 = new DevComponents.DotNetBar.PanelDockContainer();
			this.dockContainerItem2 = new DevComponents.DotNetBar.DockContainerItem();
			this.dockSite2 = new DevComponents.DotNetBar.DockSite();
			this.barDetails = new DevComponents.DotNetBar.Bar();
			this.panelPropertyGrid = new DevComponents.DotNetBar.PanelDockContainer();
			this.dockContainerItem1 = new DevComponents.DotNetBar.DockContainerItem();
			this.dockSite8 = new DevComponents.DotNetBar.DockSite();
			this.dockSite5 = new DevComponents.DotNetBar.DockSite();
			this.dockSite6 = new DevComponents.DotNetBar.DockSite();
			this.dockSite7 = new DevComponents.DotNetBar.DockSite();
			this.dockSite3 = new DevComponents.DotNetBar.DockSite();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.panelContent = new DevComponents.DotNetBar.PanelEx();
			this.propertyGridUserOption = new ArchAngel.Designer.UI.PropertyGrids.FormUserOption();
			((System.ComponentModel.ISupportInitialize)(this.treeOptions)).BeginInit();
			this.dockSite1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.barOptions)).BeginInit();
			this.barOptions.SuspendLayout();
			this.panelDockContainer2.SuspendLayout();
			this.dockSite2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.barDetails)).BeginInit();
			this.barDetails.SuspendLayout();
			this.panelPropertyGrid.SuspendLayout();
			this.SuspendLayout();
			// 
			// treeOptions
			// 
			this.treeOptions.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
			this.treeOptions.AllowDrop = true;
			this.treeOptions.BackColor = System.Drawing.SystemColors.Window;
			// 
			// 
			// 
			this.treeOptions.BackgroundStyle.Class = "TreeBorderKey";
			this.treeOptions.Columns.Add(this.columnHeader1);
			this.treeOptions.ColumnsVisible = false;
			this.treeOptions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeOptions.GridColumnLines = false;
			this.treeOptions.HotTracking = true;
			this.treeOptions.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.treeOptions.Location = new System.Drawing.Point(0, 0);
			this.treeOptions.Name = "treeOptions";
			this.treeOptions.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.node1});
			this.treeOptions.NodesConnector = this.nodeConnector1;
			this.treeOptions.NodeStyle = this.elementStyle1;
			this.treeOptions.PathSeparator = ";";
			this.treeOptions.Size = new System.Drawing.Size(240, 538);
			this.treeOptions.Styles.Add(this.elementStyle1);
			this.treeOptions.TabIndex = 0;
			this.treeOptions.Text = "advTree1";
			this.treeOptions.SelectionChanged += new System.EventHandler(this.treeOptions_SelectionChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Name = "columnHeader1";
			this.columnHeader1.Text = "Options";
			this.columnHeader1.Width.Absolute = 150;
			this.columnHeader1.Width.AutoSize = true;
			// 
			// node1
			// 
			this.node1.Expanded = true;
			this.node1.Name = "node1";
			this.node1.Text = "node1";
			// 
			// nodeConnector1
			// 
			this.nodeConnector1.LineColor = System.Drawing.SystemColors.ControlText;
			// 
			// elementStyle1
			// 
			this.elementStyle1.Name = "elementStyle1";
			this.elementStyle1.TextColor = System.Drawing.SystemColors.ControlText;
			// 
			// dotNetBarManager1
			// 
			this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.F1);
			this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlC);
			this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlA);
			this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlV);
			this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlX);
			this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlZ);
			this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlY);
			this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.Del);
			this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.Ins);
			this.dotNetBarManager1.BottomDockSite = this.dockSite4;
			this.dotNetBarManager1.DefinitionName = "";
			this.dotNetBarManager1.EnableFullSizeDock = false;
			this.dotNetBarManager1.LeftDockSite = this.dockSite1;
			this.dotNetBarManager1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.dotNetBarManager1.ParentForm = null;
			this.dotNetBarManager1.ParentUserControl = this;
			this.dotNetBarManager1.RightDockSite = this.dockSite2;
			this.dotNetBarManager1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
			this.dotNetBarManager1.ToolbarBottomDockSite = this.dockSite8;
			this.dotNetBarManager1.ToolbarLeftDockSite = this.dockSite5;
			this.dotNetBarManager1.ToolbarRightDockSite = this.dockSite6;
			this.dotNetBarManager1.ToolbarTopDockSite = this.dockSite7;
			this.dotNetBarManager1.TopDockSite = this.dockSite3;
			// 
			// dockSite4
			// 
			this.dockSite4.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
			this.dockSite4.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.dockSite4.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer();
			this.dockSite4.Location = new System.Drawing.Point(0, 564);
			this.dockSite4.Name = "dockSite4";
			this.dockSite4.Size = new System.Drawing.Size(988, 0);
			this.dockSite4.TabIndex = 6;
			this.dockSite4.TabStop = false;
			// 
			// dockSite1
			// 
			this.dockSite1.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
			this.dockSite1.Controls.Add(this.barOptions);
			this.dockSite1.Dock = System.Windows.Forms.DockStyle.Left;
			this.dockSite1.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer(new DevComponents.DotNetBar.DocumentBaseContainer[] {
            ((DevComponents.DotNetBar.DocumentBaseContainer)(new DevComponents.DotNetBar.DocumentBarContainer(this.barOptions, 246, 564)))}, DevComponents.DotNetBar.eOrientation.Horizontal);
			this.dockSite1.Location = new System.Drawing.Point(0, 0);
			this.dockSite1.Name = "dockSite1";
			this.dockSite1.Size = new System.Drawing.Size(249, 564);
			this.dockSite1.TabIndex = 3;
			this.dockSite1.TabStop = false;
			// 
			// barOptions
			// 
			this.barOptions.AccessibleDescription = "DotNetBar Bar (barOptions)";
			this.barOptions.AccessibleName = "DotNetBar Bar";
			this.barOptions.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
			this.barOptions.AutoSyncBarCaption = true;
			this.barOptions.CloseSingleTab = true;
			this.barOptions.Controls.Add(this.panelDockContainer2);
			this.barOptions.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Caption;
			this.barOptions.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.dockContainerItem2});
			this.barOptions.LayoutType = DevComponents.DotNetBar.eLayoutType.DockContainer;
			this.barOptions.Location = new System.Drawing.Point(0, 0);
			this.barOptions.Name = "barOptions";
			this.barOptions.Size = new System.Drawing.Size(246, 564);
			this.barOptions.Stretch = true;
			this.barOptions.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
			this.barOptions.TabIndex = 0;
			this.barOptions.TabStop = false;
			this.barOptions.Text = "Options";
			// 
			// panelDockContainer2
			// 
			this.panelDockContainer2.Controls.Add(this.treeOptions);
			this.panelDockContainer2.Location = new System.Drawing.Point(3, 23);
			this.panelDockContainer2.Name = "panelDockContainer2";
			this.panelDockContainer2.Size = new System.Drawing.Size(240, 538);
			this.panelDockContainer2.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelDockContainer2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
			this.panelDockContainer2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
			this.panelDockContainer2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
			this.panelDockContainer2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.panelDockContainer2.Style.GradientAngle = 90;
			this.panelDockContainer2.TabIndex = 0;
			// 
			// dockContainerItem2
			// 
			this.dockContainerItem2.Control = this.panelDockContainer2;
			this.dockContainerItem2.Name = "dockContainerItem2";
			this.dockContainerItem2.Text = "Options";
			// 
			// dockSite2
			// 
			this.dockSite2.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
			this.dockSite2.Controls.Add(this.barDetails);
			this.dockSite2.Dock = System.Windows.Forms.DockStyle.Right;
			this.dockSite2.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer(new DevComponents.DotNetBar.DocumentBaseContainer[] {
            ((DevComponents.DotNetBar.DocumentBaseContainer)(new DevComponents.DotNetBar.DocumentBarContainer(this.barDetails, 313, 564)))}, DevComponents.DotNetBar.eOrientation.Horizontal);
			this.dockSite2.Location = new System.Drawing.Point(672, 0);
			this.dockSite2.Name = "dockSite2";
			this.dockSite2.Size = new System.Drawing.Size(316, 564);
			this.dockSite2.TabIndex = 4;
			this.dockSite2.TabStop = false;
			// 
			// barDetails
			// 
			this.barDetails.AccessibleDescription = "DotNetBar Bar (barDetails)";
			this.barDetails.AccessibleName = "DotNetBar Bar";
			this.barDetails.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
			this.barDetails.AutoSyncBarCaption = true;
			this.barDetails.CloseSingleTab = true;
			this.barDetails.Controls.Add(this.panelPropertyGrid);
			this.barDetails.Dock = System.Windows.Forms.DockStyle.Right;
			this.barDetails.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Caption;
			this.barDetails.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.dockContainerItem1});
			this.barDetails.LayoutType = DevComponents.DotNetBar.eLayoutType.DockContainer;
			this.barDetails.Location = new System.Drawing.Point(3, 0);
			this.barDetails.Name = "barDetails";
			this.barDetails.Size = new System.Drawing.Size(313, 564);
			this.barDetails.Stretch = true;
			this.barDetails.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
			this.barDetails.TabIndex = 0;
			this.barDetails.TabStop = false;
			this.barDetails.Text = "Option Properties";
			// 
			// panelPropertyGrid
			// 
			this.panelPropertyGrid.Controls.Add(this.propertyGridUserOption);
			this.panelPropertyGrid.Location = new System.Drawing.Point(3, 23);
			this.panelPropertyGrid.Name = "panelPropertyGrid";
			this.panelPropertyGrid.Size = new System.Drawing.Size(307, 538);
			this.panelPropertyGrid.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelPropertyGrid.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
			this.panelPropertyGrid.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
			this.panelPropertyGrid.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
			this.panelPropertyGrid.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.panelPropertyGrid.Style.GradientAngle = 90;
			this.panelPropertyGrid.TabIndex = 0;
			// 
			// dockContainerItem1
			// 
			this.dockContainerItem1.Control = this.panelPropertyGrid;
			this.dockContainerItem1.Name = "dockContainerItem1";
			this.dockContainerItem1.Text = "Option Properties";
			// 
			// dockSite8
			// 
			this.dockSite8.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
			this.dockSite8.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.dockSite8.Location = new System.Drawing.Point(0, 564);
			this.dockSite8.Name = "dockSite8";
			this.dockSite8.Size = new System.Drawing.Size(988, 0);
			this.dockSite8.TabIndex = 10;
			this.dockSite8.TabStop = false;
			// 
			// dockSite5
			// 
			this.dockSite5.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
			this.dockSite5.Dock = System.Windows.Forms.DockStyle.Left;
			this.dockSite5.Location = new System.Drawing.Point(0, 0);
			this.dockSite5.Name = "dockSite5";
			this.dockSite5.Size = new System.Drawing.Size(0, 564);
			this.dockSite5.TabIndex = 7;
			this.dockSite5.TabStop = false;
			// 
			// dockSite6
			// 
			this.dockSite6.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
			this.dockSite6.Dock = System.Windows.Forms.DockStyle.Right;
			this.dockSite6.Location = new System.Drawing.Point(988, 0);
			this.dockSite6.Name = "dockSite6";
			this.dockSite6.Size = new System.Drawing.Size(0, 564);
			this.dockSite6.TabIndex = 8;
			this.dockSite6.TabStop = false;
			// 
			// dockSite7
			// 
			this.dockSite7.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
			this.dockSite7.Dock = System.Windows.Forms.DockStyle.Top;
			this.dockSite7.Location = new System.Drawing.Point(0, 0);
			this.dockSite7.Name = "dockSite7";
			this.dockSite7.Size = new System.Drawing.Size(988, 0);
			this.dockSite7.TabIndex = 9;
			this.dockSite7.TabStop = false;
			// 
			// dockSite3
			// 
			this.dockSite3.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
			this.dockSite3.Dock = System.Windows.Forms.DockStyle.Top;
			this.dockSite3.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer();
			this.dockSite3.Location = new System.Drawing.Point(0, 0);
			this.dockSite3.Name = "dockSite3";
			this.dockSite3.Size = new System.Drawing.Size(988, 0);
			this.dockSite3.TabIndex = 5;
			this.dockSite3.TabStop = false;
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
			this.imageList1.Images.SetKeyName(0, "VSObject_Class.bmp");
			this.imageList1.Images.SetKeyName(1, "VSObject_Properties.bmp");
			this.imageList1.Images.SetKeyName(2, "VSObject_Namespace.bmp");
			this.imageList1.Images.SetKeyName(3, "VSObject_Method.bmp");
			// 
			// panelContent
			// 
			this.panelContent.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelContent.Location = new System.Drawing.Point(249, 0);
			this.panelContent.Name = "panelContent";
			this.panelContent.Size = new System.Drawing.Size(423, 564);
			this.panelContent.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelContent.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelContent.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelContent.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelContent.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelContent.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelContent.Style.GradientAngle = 90;
			this.panelContent.TabIndex = 11;
			// 
			// propertyGridUserOption
			// 
			this.propertyGridUserOption.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGridUserOption.EventRaisingDisabled = false;
			this.propertyGridUserOption.Location = new System.Drawing.Point(0, 0);
			this.propertyGridUserOption.Name = "propertyGridUserOption";
			this.propertyGridUserOption.Size = new System.Drawing.Size(307, 538);
			this.propertyGridUserOption.TabIndex = 0;
			// 
			// ucOptions
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panelContent);
			this.Controls.Add(this.dockSite2);
			this.Controls.Add(this.dockSite1);
			this.Controls.Add(this.dockSite3);
			this.Controls.Add(this.dockSite4);
			this.Controls.Add(this.dockSite5);
			this.Controls.Add(this.dockSite6);
			this.Controls.Add(this.dockSite7);
			this.Controls.Add(this.dockSite8);
			this.Name = "ucOptions";
			this.Size = new System.Drawing.Size(988, 564);
			this.Load += new System.EventHandler(this.ucOptions_Load);
			((System.ComponentModel.ISupportInitialize)(this.treeOptions)).EndInit();
			this.dockSite1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.barOptions)).EndInit();
			this.barOptions.ResumeLayout(false);
			this.panelDockContainer2.ResumeLayout(false);
			this.dockSite2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.barDetails)).EndInit();
			this.barDetails.ResumeLayout(false);
			this.panelPropertyGrid.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.AdvTree.AdvTree treeOptions;
		private DevComponents.AdvTree.Node node1;
		private DevComponents.AdvTree.NodeConnector nodeConnector1;
		private DevComponents.DotNetBar.ElementStyle elementStyle1;
		private DevComponents.DotNetBar.DotNetBarManager dotNetBarManager1;
		private DevComponents.DotNetBar.DockSite dockSite4;
		private DevComponents.DotNetBar.DockSite dockSite1;
		private DevComponents.DotNetBar.DockSite dockSite2;
		private DevComponents.DotNetBar.Bar barDetails;
		private DevComponents.DotNetBar.PanelDockContainer panelPropertyGrid;
		private DevComponents.DotNetBar.DockContainerItem dockContainerItem1;
		private DevComponents.DotNetBar.DockSite dockSite3;
		private DevComponents.DotNetBar.DockSite dockSite5;
		private DevComponents.DotNetBar.DockSite dockSite6;
		private DevComponents.DotNetBar.DockSite dockSite7;
		private DevComponents.DotNetBar.DockSite dockSite8;
		private DevComponents.DotNetBar.Bar barOptions;
		private DevComponents.DotNetBar.PanelDockContainer panelDockContainer2;
		private DevComponents.DotNetBar.DockContainerItem dockContainerItem2;
		private DevComponents.AdvTree.ColumnHeader columnHeader1;
		private System.Windows.Forms.ImageList imageList1;
		private ArchAngel.Designer.UI.PropertyGrids.FormUserOption propertyGridUserOption;
		private DevComponents.DotNetBar.PanelEx panelContent;
	}
}
