using DevComponents.DotNetBar;

namespace ArchAngel.Workbench
{
	partial class FormMain
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			this.panelContent = new System.Windows.Forms.Panel();
			this.tracePanel = new System.Windows.Forms.Panel();
			this.textBoxTrace = new System.Windows.Forms.RichTextBox();
			this.panelDebugToolbar = new System.Windows.Forms.Panel();
			this.cbEnableDebugLogging = new System.Windows.Forms.CheckBox();
			this.cbAutoScroll = new System.Windows.Forms.CheckBox();
			this.cbLimitSize = new System.Windows.Forms.CheckBox();
			this.btnSaveLogToFile = new System.Windows.Forms.Button();
			this.cbWordWrap = new System.Windows.Forms.CheckBox();
			this.imageListNavBar = new System.Windows.Forms.ImageList(this.components);
			this.imageListHeading = new System.Windows.Forms.ImageList(this.components);
			this.imageListMisc = new System.Windows.Forms.ImageList(this.components);
			this.backgroundWorkerUpdateChecker = new System.ComponentModel.BackgroundWorker();
			this.ribbonControl = new DevComponents.DotNetBar.RibbonControl();
			this.office2007StartButton1 = new DevComponents.DotNetBar.Office2007StartButton();
			this.superTabControlFileMenu = new DevComponents.DotNetBar.SuperTabControl();
			this.superTabControlPanelHelp = new DevComponents.DotNetBar.SuperTabControlPanel();
			this.labelRegisteredTo = new DevComponents.DotNetBar.LabelX();
			this.labelLicenseRegistrationDetails = new System.Windows.Forms.Label();
			this.buttonCopySerial = new DevComponents.DotNetBar.ButtonX();
			this.labelSerialNumber = new System.Windows.Forms.Label();
			this.labelErrorMessage = new System.Windows.Forms.Label();
			this.labelX5 = new DevComponents.DotNetBar.LabelX();
			this.labelLicenseDetails = new DevComponents.DotNetBar.LabelX();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.labelX2 = new DevComponents.DotNetBar.LabelX();
			this.buttonSuggestion = new DevComponents.DotNetBar.ButtonX();
			this.buttonReportBug = new DevComponents.DotNetBar.ButtonX();
			this.buttonForums = new DevComponents.DotNetBar.ButtonX();
			this.panelAbout = new System.Windows.Forms.Panel();
			this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
			this.linkLabel2 = new System.Windows.Forms.LinkLabel();
			this.labelX3 = new DevComponents.DotNetBar.LabelX();
			this.labelVersion = new System.Windows.Forms.Label();
			this.labelAboutCopyright = new System.Windows.Forms.Label();
			this.linkLabelLicense = new System.Windows.Forms.LinkLabel();
			this.superTabItemHelp = new DevComponents.DotNetBar.SuperTabItem();
			this.superTabControlPanel5 = new DevComponents.DotNetBar.SuperTabControlPanel();
			this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
			this.recentDocsItemPane = new DevComponents.DotNetBar.ItemPanel();
			this.labelX1 = new DevComponents.DotNetBar.LabelX();
			this.superTabItemRecentProjects = new DevComponents.DotNetBar.SuperTabItem();
			this.superTabControlPanel6 = new DevComponents.DotNetBar.SuperTabControlPanel();
			this.projectSettings1 = new ArchAngel.Workbench.UserControls.ProjectSettings();
			this.superTabItemOptions = new DevComponents.DotNetBar.SuperTabItem();
			this.buttonNewProject = new DevComponents.DotNetBar.ButtonItem();
			this.buttonOpenProject = new DevComponents.DotNetBar.ButtonItem();
			this.buttonSaveProject = new DevComponents.DotNetBar.ButtonItem();
			this.buttonSaveAsProject = new DevComponents.DotNetBar.ButtonItem();
			this.buttonExit = new DevComponents.DotNetBar.ButtonItem();
			this.itemContainer13 = new DevComponents.DotNetBar.ItemContainer();
			this.itemContainer14 = new DevComponents.DotNetBar.ItemContainer();
			this.itemContainer15 = new DevComponents.DotNetBar.ItemContainer();
			this.mnuBoxNew = new DevComponents.DotNetBar.ButtonItem();
			this.mnuBoxOpen = new DevComponents.DotNetBar.ButtonItem();
			this.mnuBoxSave = new DevComponents.DotNetBar.ButtonItem();
			this.mnuBoxSaveAs = new DevComponents.DotNetBar.ButtonItem();
			this.mnuBoxLicense = new DevComponents.DotNetBar.ButtonItem();
			this.mnuBoxCheckUpdates = new DevComponents.DotNetBar.ButtonItem();
			this.mnuBoxAbout = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItem17 = new DevComponents.DotNetBar.ButtonItem();
			this.galleryContainer3 = new DevComponents.DotNetBar.GalleryContainer();
			this.labelItem8 = new DevComponents.DotNetBar.LabelItem();
			this.buttonItem36 = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItem37 = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItem38 = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItem39 = new DevComponents.DotNetBar.ButtonItem();
			this.itemContainer16 = new DevComponents.DotNetBar.ItemContainer();
			this.mnuBoxExit = new DevComponents.DotNetBar.ButtonItem();
			this.ribbonTabItemOptions = new DevComponents.DotNetBar.RibbonTabItem();
			this.ribbonTabItemTemplate = new DevComponents.DotNetBar.RibbonTabItem();
			this.ribbonTabItemFiles = new DevComponents.DotNetBar.RibbonTabItem();
			this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
			this.labelItem1 = new DevComponents.DotNetBar.LabelItem();
			this.buttonItemStyleOfficeBlue = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemStyleOfficeBlack = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemStyleOfficeSilver = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemStyleVistaGlass = new DevComponents.DotNetBar.ButtonItem();
			this.labelItem2 = new DevComponents.DotNetBar.LabelItem();
			this.buttonItemStyleOffice2010Blue = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemStyleOffice2010Silver = new DevComponents.DotNetBar.ButtonItem();
			this.labelItem3 = new DevComponents.DotNetBar.LabelItem();
			this.buttonItemStyleWindows7 = new DevComponents.DotNetBar.ButtonItem();
			this.labelItem4 = new DevComponents.DotNetBar.LabelItem();
			this.buttonStyleCustom = new DevComponents.DotNetBar.ColorPickerDropDown();
			this.mnuHelp = new DevComponents.DotNetBar.ButtonItem();
			this.mnuForums = new DevComponents.DotNetBar.ButtonItem();
			this.mnuReportBug = new DevComponents.DotNetBar.ButtonItem();
			this.mnuSuggestion = new DevComponents.DotNetBar.ButtonItem();
			this.mnuTopNew = new DevComponents.DotNetBar.ButtonItem();
			this.mnuTopOpen2 = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItem6 = new DevComponents.DotNetBar.ButtonItem();
			this.mnuSaveTop = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemResetDefaultOptions = new DevComponents.DotNetBar.ButtonItem();
			this.mnuChangeOutputPath = new DevComponents.DotNetBar.ButtonItem();
			this.mnuRefresh = new DevComponents.DotNetBar.ButtonItem();
			this.mnuWriteFilesToDisk = new DevComponents.DotNetBar.ButtonItem();
			this.itemContainer1 = new DevComponents.DotNetBar.ItemContainer();
			this.itemContainer2 = new DevComponents.DotNetBar.ItemContainer();
			this.mnuFontIncrease = new DevComponents.DotNetBar.ButtonItem();
			this.mnuFontDecrease = new DevComponents.DotNetBar.ButtonItem();
			this.mnuSwitchHighlighting = new DevComponents.DotNetBar.ButtonItem();
			this.itemContainer18 = new DevComponents.DotNetBar.ItemContainer();
			this.mnuToggleBreakpoint = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItem4 = new DevComponents.DotNetBar.ButtonItem();
			this.galleryContainer1 = new DevComponents.DotNetBar.GalleryContainer();
			this.buttonItem5 = new DevComponents.DotNetBar.ButtonItem();
			this.itemContainer3 = new DevComponents.DotNetBar.ItemContainer();
			this.mnuDebugRestart = new DevComponents.DotNetBar.ButtonItem();
			this.mnuDebugContinue = new DevComponents.DotNetBar.ButtonItem();
			this.itemContainer4 = new DevComponents.DotNetBar.ItemContainer();
			this.mnuDebugStepInto = new DevComponents.DotNetBar.ButtonItem();
			this.mnuDebugStepOver = new DevComponents.DotNetBar.ButtonItem();
			this.itemContainer11 = new DevComponents.DotNetBar.ItemContainer();
			this.mnuFindNext = new DevComponents.DotNetBar.ButtonItem();
			this.mnuReplace = new DevComponents.DotNetBar.ButtonItem();
			this.itemContainer12 = new DevComponents.DotNetBar.ItemContainer();
			this.mnuCut = new DevComponents.DotNetBar.ButtonItem();
			this.mnuCopy = new DevComponents.DotNetBar.ButtonItem();
			this.mnuDebugStart = new DevComponents.DotNetBar.ButtonItem();
			this.mnuDebugStop = new DevComponents.DotNetBar.ButtonItem();
			this.mnuFind = new DevComponents.DotNetBar.ButtonItem();
			this.mnuNewFunction = new DevComponents.DotNetBar.ButtonItem();
			this.mnuDeleteFunction = new DevComponents.DotNetBar.ButtonItem();
			this.mnuPaste = new DevComponents.DotNetBar.ButtonItem();
			this.superTooltip1 = new DevComponents.DotNetBar.SuperTooltip();
			this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
			this.tracePanel.SuspendLayout();
			this.panelDebugToolbar.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.superTabControlFileMenu)).BeginInit();
			this.superTabControlFileMenu.SuspendLayout();
			this.superTabControlPanelHelp.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			this.panelAbout.SuspendLayout();
			this.superTabControlPanel5.SuspendLayout();
			this.panelEx1.SuspendLayout();
			this.superTabControlPanel6.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelContent
			// 
			this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelContent.Location = new System.Drawing.Point(5, 59);
			this.panelContent.Margin = new System.Windows.Forms.Padding(2);
			this.panelContent.Name = "panelContent";
			this.panelContent.Size = new System.Drawing.Size(930, 292);
			this.panelContent.TabIndex = 6;
			// 
			// tracePanel
			// 
			this.tracePanel.Controls.Add(this.textBoxTrace);
			this.tracePanel.Controls.Add(this.panelDebugToolbar);
			this.tracePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tracePanel.Location = new System.Drawing.Point(5, 351);
			this.tracePanel.Name = "tracePanel";
			this.tracePanel.Size = new System.Drawing.Size(930, 129);
			this.tracePanel.TabIndex = 0;
			this.tracePanel.Visible = false;
			// 
			// textBoxTrace
			// 
			this.textBoxTrace.Cursor = System.Windows.Forms.Cursors.Default;
			this.textBoxTrace.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxTrace.Location = new System.Drawing.Point(0, 29);
			this.textBoxTrace.Name = "textBoxTrace";
			this.textBoxTrace.ReadOnly = true;
			this.textBoxTrace.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textBoxTrace.Size = new System.Drawing.Size(930, 100);
			this.textBoxTrace.TabIndex = 0;
			this.textBoxTrace.Text = "";
			// 
			// panelDebugToolbar
			// 
			this.panelDebugToolbar.Controls.Add(this.cbEnableDebugLogging);
			this.panelDebugToolbar.Controls.Add(this.cbAutoScroll);
			this.panelDebugToolbar.Controls.Add(this.cbLimitSize);
			this.panelDebugToolbar.Controls.Add(this.btnSaveLogToFile);
			this.panelDebugToolbar.Controls.Add(this.cbWordWrap);
			this.panelDebugToolbar.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelDebugToolbar.Location = new System.Drawing.Point(0, 0);
			this.panelDebugToolbar.Name = "panelDebugToolbar";
			this.panelDebugToolbar.Size = new System.Drawing.Size(930, 29);
			this.panelDebugToolbar.TabIndex = 1;
			// 
			// cbEnableDebugLogging
			// 
			this.cbEnableDebugLogging.AutoSize = true;
			this.cbEnableDebugLogging.Location = new System.Drawing.Point(5, 5);
			this.cbEnableDebugLogging.Name = "cbEnableDebugLogging";
			this.cbEnableDebugLogging.Size = new System.Drawing.Size(135, 17);
			this.cbEnableDebugLogging.TabIndex = 4;
			this.cbEnableDebugLogging.Text = "Enable Debug Logging";
			this.cbEnableDebugLogging.UseVisualStyleBackColor = true;
			this.cbEnableDebugLogging.CheckedChanged += new System.EventHandler(this.cbEnableDebugLogging_CheckedChanged);
			// 
			// cbAutoScroll
			// 
			this.cbAutoScroll.AutoSize = true;
			this.cbAutoScroll.Checked = true;
			this.cbAutoScroll.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbAutoScroll.Location = new System.Drawing.Point(309, 5);
			this.cbAutoScroll.Name = "cbAutoScroll";
			this.cbAutoScroll.Size = new System.Drawing.Size(74, 17);
			this.cbAutoScroll.TabIndex = 3;
			this.cbAutoScroll.Text = "AutoScroll";
			this.cbAutoScroll.UseVisualStyleBackColor = true;
			// 
			// cbLimitSize
			// 
			this.cbLimitSize.AutoSize = true;
			this.cbLimitSize.Checked = true;
			this.cbLimitSize.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbLimitSize.Location = new System.Drawing.Point(233, 5);
			this.cbLimitSize.Name = "cbLimitSize";
			this.cbLimitSize.Size = new System.Drawing.Size(70, 17);
			this.cbLimitSize.TabIndex = 2;
			this.cbLimitSize.Text = "Limit Size";
			this.cbLimitSize.UseVisualStyleBackColor = true;
			this.cbLimitSize.CheckedChanged += new System.EventHandler(this.cbLimitSize_CheckedChanged);
			// 
			// btnSaveLogToFile
			// 
			this.btnSaveLogToFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSaveLogToFile.Location = new System.Drawing.Point(852, 3);
			this.btnSaveLogToFile.Name = "btnSaveLogToFile";
			this.btnSaveLogToFile.Size = new System.Drawing.Size(75, 23);
			this.btnSaveLogToFile.TabIndex = 1;
			this.btnSaveLogToFile.Text = "Save To File";
			this.btnSaveLogToFile.UseVisualStyleBackColor = true;
			this.btnSaveLogToFile.Click += new System.EventHandler(this.btnSaveLogToFile_Click);
			// 
			// cbWordWrap
			// 
			this.cbWordWrap.AutoSize = true;
			this.cbWordWrap.Location = new System.Drawing.Point(146, 5);
			this.cbWordWrap.Name = "cbWordWrap";
			this.cbWordWrap.Size = new System.Drawing.Size(81, 17);
			this.cbWordWrap.TabIndex = 0;
			this.cbWordWrap.Text = "Word Wrap";
			this.cbWordWrap.UseVisualStyleBackColor = true;
			this.cbWordWrap.CheckedChanged += new System.EventHandler(this.cbWordWrap_CheckedChanged);
			// 
			// imageListNavBar
			// 
			this.imageListNavBar.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageListNavBar.ImageSize = new System.Drawing.Size(24, 24);
			this.imageListNavBar.TransparentColor = System.Drawing.Color.Magenta;
			// 
			// imageListHeading
			// 
			this.imageListHeading.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListHeading.ImageStream")));
			this.imageListHeading.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListHeading.Images.SetKeyName(0, "Header_ProjectDetails.bmp");
			this.imageListHeading.Images.SetKeyName(1, "Header_Database.bmp");
			this.imageListHeading.Images.SetKeyName(2, "Header_Model.bmp");
			this.imageListHeading.Images.SetKeyName(3, "Header_Options.bmp");
			this.imageListHeading.Images.SetKeyName(4, "Header_Generation.bmp");
			// 
			// imageListMisc
			// 
			this.imageListMisc.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMisc.ImageStream")));
			this.imageListMisc.TransparentColor = System.Drawing.Color.Magenta;
			this.imageListMisc.Images.SetKeyName(0, "Help.bmp");
			// 
			// backgroundWorkerUpdateChecker
			// 
			this.backgroundWorkerUpdateChecker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerUpdateChecker_DoWork);
			this.backgroundWorkerUpdateChecker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerUpdateChecker_RunWorkerCompleted);
			// 
			// ribbonControl
			// 
			this.ribbonControl.BackColor = System.Drawing.Color.Gainsboro;
			// 
			// 
			// 
			this.ribbonControl.BackgroundStyle.Class = "";
			this.ribbonControl.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ribbonControl.CaptionVisible = true;
			this.ribbonControl.Dock = System.Windows.Forms.DockStyle.Top;
			this.ribbonControl.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.office2007StartButton1,
            this.ribbonTabItemOptions,
            this.ribbonTabItemTemplate,
            this.ribbonTabItemFiles,
            this.buttonItem1,
            this.mnuHelp});
			this.ribbonControl.KeyTipsFont = new System.Drawing.Font("Tahoma", 7F);
			this.ribbonControl.Location = new System.Drawing.Point(5, 1);
			this.ribbonControl.Name = "ribbonControl";
			this.ribbonControl.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
			this.ribbonControl.QuickToolbarItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.mnuTopNew,
            this.mnuTopOpen2,
            this.mnuSaveTop});
			this.ribbonControl.Size = new System.Drawing.Size(930, 58);
			this.ribbonControl.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ribbonControl.SystemText.MaximizeRibbonText = "&Maximize the Ribbon";
			this.ribbonControl.SystemText.MinimizeRibbonText = "Mi&nimize the Ribbon";
			this.ribbonControl.SystemText.QatAddItemText = "&Add to Quick Access Toolbar";
			this.ribbonControl.SystemText.QatCustomizeMenuLabel = "<b>Customize Quick Access Toolbar</b>";
			this.ribbonControl.SystemText.QatCustomizeText = "&Customize Quick Access Toolbar...";
			this.ribbonControl.SystemText.QatDialogAddButton = "&Add >>";
			this.ribbonControl.SystemText.QatDialogCancelButton = "Cancel";
			this.ribbonControl.SystemText.QatDialogCaption = "Customize Quick Access Toolbar";
			this.ribbonControl.SystemText.QatDialogCategoriesLabel = "&Choose commands from:";
			this.ribbonControl.SystemText.QatDialogOkButton = "OK";
			this.ribbonControl.SystemText.QatDialogPlacementCheckbox = "&Place Quick Access Toolbar below the Ribbon";
			this.ribbonControl.SystemText.QatDialogRemoveButton = "&Remove";
			this.ribbonControl.SystemText.QatPlaceAboveRibbonText = "&Place Quick Access Toolbar above the Ribbon";
			this.ribbonControl.SystemText.QatPlaceBelowRibbonText = "&Place Quick Access Toolbar below the Ribbon";
			this.ribbonControl.SystemText.QatRemoveItemText = "&Remove from Quick Access Toolbar";
			this.ribbonControl.TabGroupHeight = 14;
			this.ribbonControl.TabIndex = 33;
			this.ribbonControl.Text = "TTTTT";
			// 
			// office2007StartButton1
			// 
			this.office2007StartButton1.AutoExpandOnClick = true;
			this.office2007StartButton1.BackstageTab = this.superTabControlFileMenu;
			this.office2007StartButton1.CanCustomize = false;
			this.office2007StartButton1.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.Image;
			this.office2007StartButton1.Image = ((System.Drawing.Image)(resources.GetObject("office2007StartButton1.Image")));
			this.office2007StartButton1.ImageFixedSize = new System.Drawing.Size(16, 16);
			this.office2007StartButton1.ImagePaddingHorizontal = 0;
			this.office2007StartButton1.ImagePaddingVertical = 0;
			this.office2007StartButton1.Name = "office2007StartButton1";
			this.office2007StartButton1.ShowSubItems = false;
			this.office2007StartButton1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainer13});
			this.office2007StartButton1.Text = "&File";
			// 
			// superTabControlFileMenu
			// 
			this.superTabControlFileMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			// 
			// 
			// 
			// 
			// 
			// 
			this.superTabControlFileMenu.ControlBox.CloseBox.Name = "";
			// 
			// 
			// 
			this.superTabControlFileMenu.ControlBox.MenuBox.Name = "";
			this.superTabControlFileMenu.ControlBox.Name = "";
			this.superTabControlFileMenu.ControlBox.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabControlFileMenu.ControlBox.MenuBox,
            this.superTabControlFileMenu.ControlBox.CloseBox});
			this.superTabControlFileMenu.ControlBox.Visible = false;
			this.superTabControlFileMenu.Controls.Add(this.superTabControlPanel5);
			this.superTabControlFileMenu.Controls.Add(this.superTabControlPanelHelp);
			this.superTabControlFileMenu.Controls.Add(this.superTabControlPanel6);
			this.superTabControlFileMenu.ItemPadding.Left = 6;
			this.superTabControlFileMenu.ItemPadding.Right = 4;
			this.superTabControlFileMenu.ItemPadding.Top = 4;
			this.superTabControlFileMenu.Location = new System.Drawing.Point(5, 53);
			this.superTabControlFileMenu.Name = "superTabControlFileMenu";
			this.superTabControlFileMenu.ReorderTabsEnabled = false;
			this.superTabControlFileMenu.SelectedTabFont = new System.Drawing.Font("Segoe UI", 9.75F);
			this.superTabControlFileMenu.SelectedTabIndex = 6;
			this.superTabControlFileMenu.Size = new System.Drawing.Size(930, 424);
			this.superTabControlFileMenu.TabAlignment = DevComponents.DotNetBar.eTabStripAlignment.Left;
			this.superTabControlFileMenu.TabFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.superTabControlFileMenu.TabHorizontalSpacing = 16;
			this.superTabControlFileMenu.TabIndex = 35;
			this.superTabControlFileMenu.Tabs.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabItemRecentProjects,
            this.buttonNewProject,
            this.buttonOpenProject,
            this.buttonSaveProject,
            this.buttonSaveAsProject,
            this.superTabItemOptions,
            this.superTabItemHelp,
            this.buttonExit});
			this.superTabControlFileMenu.TabStyle = DevComponents.DotNetBar.eSuperTabStyle.Office2010BackstageBlue;
			this.superTabControlFileMenu.TabVerticalSpacing = 8;
			this.superTabControlFileMenu.VisibleChanged += new System.EventHandler(this.superTabControlFileMenu_VisibleChanged);
			// 
			// superTabControlPanelHelp
			// 
			this.superTabControlPanelHelp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("superTabControlPanelHelp.BackgroundImage")));
			this.superTabControlPanelHelp.BackgroundImagePosition = DevComponents.DotNetBar.eStyleBackgroundImage.BottomRight;
			this.superTabControlPanelHelp.Controls.Add(this.labelRegisteredTo);
			this.superTabControlPanelHelp.Controls.Add(this.labelLicenseRegistrationDetails);
			this.superTabControlPanelHelp.Controls.Add(this.buttonCopySerial);
			this.superTabControlPanelHelp.Controls.Add(this.labelSerialNumber);
			this.superTabControlPanelHelp.Controls.Add(this.labelErrorMessage);
			this.superTabControlPanelHelp.Controls.Add(this.labelX5);
			this.superTabControlPanelHelp.Controls.Add(this.labelLicenseDetails);
			this.superTabControlPanelHelp.Controls.Add(this.pictureBox2);
			this.superTabControlPanelHelp.Controls.Add(this.labelX2);
			this.superTabControlPanelHelp.Controls.Add(this.buttonSuggestion);
			this.superTabControlPanelHelp.Controls.Add(this.buttonReportBug);
			this.superTabControlPanelHelp.Controls.Add(this.buttonForums);
			this.superTabControlPanelHelp.Controls.Add(this.panelAbout);
			this.superTabControlPanelHelp.Dock = System.Windows.Forms.DockStyle.Fill;
			this.superTabControlPanelHelp.Location = new System.Drawing.Point(143, 0);
			this.superTabControlPanelHelp.Name = "superTabControlPanelHelp";
			this.superTabControlPanelHelp.Size = new System.Drawing.Size(787, 424);
			this.superTabControlPanelHelp.TabIndex = 4;
			this.superTabControlPanelHelp.TabItem = this.superTabItemHelp;
			this.superTabControlPanelHelp.Click += new System.EventHandler(this.superTabControlPanel8_Click);
			// 
			// labelRegisteredTo
			// 
			this.labelRegisteredTo.AutoSize = true;
			this.labelRegisteredTo.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.labelRegisteredTo.BackgroundStyle.Class = "";
			this.labelRegisteredTo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelRegisteredTo.Location = new System.Drawing.Point(243, 126);
			this.labelRegisteredTo.Name = "labelRegisteredTo";
			this.labelRegisteredTo.Size = new System.Drawing.Size(71, 15);
			this.labelRegisteredTo.TabIndex = 44;
			this.labelRegisteredTo.Text = "Registered to:";
			// 
			// labelLicenseRegistrationDetails
			// 
			this.labelLicenseRegistrationDetails.AutoSize = true;
			this.labelLicenseRegistrationDetails.BackColor = System.Drawing.Color.Transparent;
			this.labelLicenseRegistrationDetails.Location = new System.Drawing.Point(320, 127);
			this.labelLicenseRegistrationDetails.Name = "labelLicenseRegistrationDetails";
			this.labelLicenseRegistrationDetails.Size = new System.Drawing.Size(84, 13);
			this.labelLicenseRegistrationDetails.TabIndex = 33;
			this.labelLicenseRegistrationDetails.Text = "Free trial version";
			// 
			// buttonCopySerial
			// 
			this.buttonCopySerial.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonCopySerial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCopySerial.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonCopySerial.Image = ((System.Drawing.Image)(resources.GetObject("buttonCopySerial.Image")));
			this.buttonCopySerial.Location = new System.Drawing.Point(640, 122);
			this.buttonCopySerial.Name = "buttonCopySerial";
			this.buttonCopySerial.Size = new System.Drawing.Size(88, 23);
			this.buttonCopySerial.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonCopySerial.TabIndex = 43;
			this.buttonCopySerial.Text = "Copy serial";
			this.buttonCopySerial.Click += new System.EventHandler(this.buttonCopySerial_Click);
			// 
			// labelSerialNumber
			// 
			this.labelSerialNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelSerialNumber.BackColor = System.Drawing.Color.Transparent;
			this.labelSerialNumber.Location = new System.Drawing.Point(443, 99);
			this.labelSerialNumber.Name = "labelSerialNumber";
			this.labelSerialNumber.Size = new System.Drawing.Size(285, 13);
			this.labelSerialNumber.TabIndex = 42;
			this.labelSerialNumber.Text = "xxxx-xxxx-xxxx-xxxx-xxxx-xxxx";
			this.labelSerialNumber.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelErrorMessage
			// 
			this.labelErrorMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.labelErrorMessage.BackColor = System.Drawing.Color.Transparent;
			this.labelErrorMessage.ForeColor = System.Drawing.Color.Red;
			this.labelErrorMessage.Location = new System.Drawing.Point(240, 119);
			this.labelErrorMessage.Name = "labelErrorMessage";
			this.labelErrorMessage.Size = new System.Drawing.Size(463, 13);
			this.labelErrorMessage.TabIndex = 38;
			this.labelErrorMessage.Text = "Error message";
			this.labelErrorMessage.Visible = false;
			// 
			// labelX5
			// 
			this.labelX5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.labelX5.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.labelX5.BackgroundStyle.BorderBottomColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
			this.labelX5.BackgroundStyle.BorderBottomWidth = 1;
			this.labelX5.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.labelX5.BackgroundStyle.BorderRightColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
			this.labelX5.BackgroundStyle.BorderRightWidth = 1;
			this.labelX5.BackgroundStyle.Class = "";
			this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelX5.ForeColor = System.Drawing.Color.DimGray;
			this.labelX5.Location = new System.Drawing.Point(200, 12);
			this.labelX5.Name = "labelX5";
			this.labelX5.Size = new System.Drawing.Size(20, 403);
			this.labelX5.TabIndex = 37;
			// 
			// labelLicenseDetails
			// 
			this.labelLicenseDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.labelLicenseDetails.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.labelLicenseDetails.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Dash;
			this.labelLicenseDetails.BackgroundStyle.BorderBottomColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
			this.labelLicenseDetails.BackgroundStyle.BorderBottomWidth = 1;
			this.labelLicenseDetails.BackgroundStyle.Class = "";
			this.labelLicenseDetails.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelLicenseDetails.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelLicenseDetails.ForeColor = System.Drawing.Color.DimGray;
			this.labelLicenseDetails.Location = new System.Drawing.Point(243, 93);
			this.labelLicenseDetails.Name = "labelLicenseDetails";
			this.labelLicenseDetails.Size = new System.Drawing.Size(485, 23);
			this.labelLicenseDetails.TabIndex = 32;
			this.labelLicenseDetails.Text = "License Details";
			// 
			// pictureBox2
			// 
			this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
			this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
			this.pictureBox2.Location = new System.Drawing.Point(243, 12);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(202, 56);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox2.TabIndex = 30;
			this.pictureBox2.TabStop = false;
			// 
			// labelX2
			// 
			this.labelX2.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.labelX2.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Dash;
			this.labelX2.BackgroundStyle.BorderBottomColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
			this.labelX2.BackgroundStyle.BorderBottomWidth = 1;
			this.labelX2.BackgroundStyle.Class = "";
			this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelX2.ForeColor = System.Drawing.Color.DimGray;
			this.labelX2.Location = new System.Drawing.Point(17, 12);
			this.labelX2.Name = "labelX2";
			this.labelX2.Size = new System.Drawing.Size(192, 23);
			this.labelX2.TabIndex = 29;
			this.labelX2.Text = "Support";
			// 
			// buttonSuggestion
			// 
			this.buttonSuggestion.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonSuggestion.BackColor = System.Drawing.Color.White;
			this.buttonSuggestion.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonSuggestion.HoverImage = ((System.Drawing.Image)(resources.GetObject("buttonSuggestion.HoverImage")));
			this.buttonSuggestion.Image = ((System.Drawing.Image)(resources.GetObject("buttonSuggestion.Image")));
			this.buttonSuggestion.Location = new System.Drawing.Point(35, 163);
			this.buttonSuggestion.Name = "buttonSuggestion";
			this.buttonSuggestion.Size = new System.Drawing.Size(143, 34);
			this.buttonSuggestion.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonSuggestion.TabIndex = 2;
			this.buttonSuggestion.Text = "Make a suggestion...";
			this.buttonSuggestion.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonSuggestion.Click += new System.EventHandler(this.buttonSuggestion_Click);
			// 
			// buttonReportBug
			// 
			this.buttonReportBug.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonReportBug.BackColor = System.Drawing.Color.White;
			this.buttonReportBug.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonReportBug.HoverImage = ((System.Drawing.Image)(resources.GetObject("buttonReportBug.HoverImage")));
			this.buttonReportBug.Image = ((System.Drawing.Image)(resources.GetObject("buttonReportBug.Image")));
			this.buttonReportBug.Location = new System.Drawing.Point(35, 111);
			this.buttonReportBug.Name = "buttonReportBug";
			this.buttonReportBug.Size = new System.Drawing.Size(143, 34);
			this.buttonReportBug.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonReportBug.TabIndex = 1;
			this.buttonReportBug.Text = "Report a bug...";
			this.buttonReportBug.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonReportBug.Click += new System.EventHandler(this.buttonReportBug_Click);
			// 
			// buttonForums
			// 
			this.buttonForums.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonForums.BackColor = System.Drawing.Color.White;
			this.buttonForums.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonForums.HoverImage = ((System.Drawing.Image)(resources.GetObject("buttonForums.HoverImage")));
			this.buttonForums.Image = ((System.Drawing.Image)(resources.GetObject("buttonForums.Image")));
			this.buttonForums.Location = new System.Drawing.Point(35, 59);
			this.buttonForums.Name = "buttonForums";
			this.buttonForums.Size = new System.Drawing.Size(143, 34);
			this.buttonForums.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonForums.TabIndex = 0;
			this.buttonForums.Text = " Visit the Forums...";
			this.buttonForums.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
			this.buttonForums.Click += new System.EventHandler(this.buttonForums_Click);
			// 
			// panelAbout
			// 
			this.panelAbout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.panelAbout.BackColor = System.Drawing.Color.Transparent;
			this.panelAbout.Controls.Add(this.buttonX1);
			this.panelAbout.Controls.Add(this.linkLabel2);
			this.panelAbout.Controls.Add(this.labelX3);
			this.panelAbout.Controls.Add(this.labelVersion);
			this.panelAbout.Controls.Add(this.labelAboutCopyright);
			this.panelAbout.Controls.Add(this.linkLabelLicense);
			this.panelAbout.Location = new System.Drawing.Point(243, 215);
			this.panelAbout.Name = "panelAbout";
			this.panelAbout.Size = new System.Drawing.Size(515, 157);
			this.panelAbout.TabIndex = 41;
			// 
			// buttonX1
			// 
			this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonX1.Image = ((System.Drawing.Image)(resources.GetObject("buttonX1.Image")));
			this.buttonX1.Location = new System.Drawing.Point(166, 40);
			this.buttonX1.Name = "buttonX1";
			this.buttonX1.Size = new System.Drawing.Size(123, 23);
			this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonX1.TabIndex = 44;
			this.buttonX1.Text = "Check for updates";
			this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
			// 
			// linkLabel2
			// 
			this.linkLabel2.AutoSize = true;
			this.linkLabel2.BackColor = System.Drawing.Color.Transparent;
			this.linkLabel2.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.linkLabel2.Location = new System.Drawing.Point(33, 112);
			this.linkLabel2.Name = "linkLabel2";
			this.linkLabel2.Size = new System.Drawing.Size(81, 13);
			this.linkLabel2.TabIndex = 42;
			this.linkLabel2.TabStop = true;
			this.linkLabel2.Text = "www.slyce.com";
			this.linkLabel2.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
			// 
			// labelX3
			// 
			this.labelX3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.labelX3.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.labelX3.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Dash;
			this.labelX3.BackgroundStyle.BorderBottomColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
			this.labelX3.BackgroundStyle.BorderBottomWidth = 1;
			this.labelX3.BackgroundStyle.Class = "";
			this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelX3.ForeColor = System.Drawing.Color.DimGray;
			this.labelX3.Location = new System.Drawing.Point(3, 11);
			this.labelX3.Name = "labelX3";
			this.labelX3.Size = new System.Drawing.Size(482, 23);
			this.labelX3.TabIndex = 31;
			this.labelX3.Text = "About Visual NHibernate";
			// 
			// labelVersion
			// 
			this.labelVersion.AutoSize = true;
			this.labelVersion.BackColor = System.Drawing.Color.Transparent;
			this.labelVersion.Location = new System.Drawing.Point(33, 46);
			this.labelVersion.Name = "labelVersion";
			this.labelVersion.Size = new System.Drawing.Size(45, 13);
			this.labelVersion.TabIndex = 34;
			this.labelVersion.Text = "Version:";
			// 
			// labelAboutCopyright
			// 
			this.labelAboutCopyright.AutoSize = true;
			this.labelAboutCopyright.BackColor = System.Drawing.Color.Transparent;
			this.labelAboutCopyright.Location = new System.Drawing.Point(33, 71);
			this.labelAboutCopyright.Name = "labelAboutCopyright";
			this.labelAboutCopyright.Size = new System.Drawing.Size(188, 13);
			this.labelAboutCopyright.TabIndex = 35;
			this.labelAboutCopyright.Text = "Copyright 2010 Slyce Software Limited";
			// 
			// linkLabelLicense
			// 
			this.linkLabelLicense.AutoSize = true;
			this.linkLabelLicense.BackColor = System.Drawing.Color.Transparent;
			this.linkLabelLicense.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.linkLabelLicense.Location = new System.Drawing.Point(33, 89);
			this.linkLabelLicense.Name = "linkLabelLicense";
			this.linkLabelLicense.Size = new System.Drawing.Size(150, 13);
			this.linkLabelLicense.TabIndex = 36;
			this.linkLabelLicense.TabStop = true;
			this.linkLabelLicense.Text = "Slyce Software License Terms";
			this.linkLabelLicense.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.linkLabelLicense.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelLicense_LinkClicked);
			// 
			// superTabItemHelp
			// 
			this.superTabItemHelp.AttachedControl = this.superTabControlPanelHelp;
			this.superTabItemHelp.GlobalItem = false;
			this.superTabItemHelp.KeyTips = "H";
			this.superTabItemHelp.Name = "superTabItemHelp";
			this.superTabItemHelp.Text = "Help";
			// 
			// superTabControlPanel5
			// 
			this.superTabControlPanel5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("superTabControlPanel5.BackgroundImage")));
			this.superTabControlPanel5.BackgroundImagePosition = DevComponents.DotNetBar.eStyleBackgroundImage.BottomRight;
			this.superTabControlPanel5.Controls.Add(this.panelEx1);
			this.superTabControlPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.superTabControlPanel5.Location = new System.Drawing.Point(143, 0);
			this.superTabControlPanel5.Name = "superTabControlPanel5";
			this.superTabControlPanel5.Size = new System.Drawing.Size(787, 424);
			this.superTabControlPanel5.TabIndex = 1;
			this.superTabControlPanel5.TabItem = this.superTabItemRecentProjects;
			// 
			// panelEx1
			// 
			this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx1.Controls.Add(this.recentDocsItemPane);
			this.panelEx1.Controls.Add(this.labelX1);
			this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx1.Location = new System.Drawing.Point(0, 0);
			this.panelEx1.Name = "panelEx1";
			this.panelEx1.Padding = new System.Windows.Forms.Padding(12);
			this.panelEx1.Size = new System.Drawing.Size(787, 424);
			this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx1.Style.BackColor1.Color = System.Drawing.Color.Transparent;
			this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx1.Style.BorderSide = DevComponents.DotNetBar.eBorderSide.Right;
			this.panelEx1.Style.GradientAngle = 90;
			this.panelEx1.TabIndex = 1;
			this.panelEx1.Text = "panelEx1";
			// 
			// recentDocsItemPane
			// 
			this.recentDocsItemPane.AutoScroll = true;
			// 
			// 
			// 
			this.recentDocsItemPane.BackgroundStyle.BackColor = System.Drawing.Color.Transparent;
			this.recentDocsItemPane.BackgroundStyle.Class = "";
			this.recentDocsItemPane.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.recentDocsItemPane.ContainerControlProcessDialogKey = true;
			this.recentDocsItemPane.Dock = System.Windows.Forms.DockStyle.Fill;
			this.recentDocsItemPane.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.recentDocsItemPane.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.recentDocsItemPane.Location = new System.Drawing.Point(12, 35);
			this.recentDocsItemPane.Name = "recentDocsItemPane";
			this.recentDocsItemPane.Size = new System.Drawing.Size(763, 377);
			this.recentDocsItemPane.TabIndex = 1;
			// 
			// labelX1
			// 
			// 
			// 
			// 
			this.labelX1.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Dash;
			this.labelX1.BackgroundStyle.BorderBottomColor = System.Drawing.Color.Gray;
			this.labelX1.BackgroundStyle.BorderBottomWidth = 1;
			this.labelX1.BackgroundStyle.Class = "";
			this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX1.Dock = System.Windows.Forms.DockStyle.Top;
			this.labelX1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelX1.ForeColor = System.Drawing.Color.DimGray;
			this.labelX1.Location = new System.Drawing.Point(12, 12);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(763, 23);
			this.labelX1.TabIndex = 0;
			this.labelX1.Text = "Recent Documents";
			// 
			// superTabItemRecentProjects
			// 
			this.superTabItemRecentProjects.AttachedControl = this.superTabControlPanel5;
			this.superTabItemRecentProjects.GlobalItem = false;
			this.superTabItemRecentProjects.KeyTips = "R";
			this.superTabItemRecentProjects.Name = "superTabItemRecentProjects";
			this.superTabItemRecentProjects.Text = "Recent Projects";
			// 
			// superTabControlPanel6
			// 
			this.superTabControlPanel6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("superTabControlPanel6.BackgroundImage")));
			this.superTabControlPanel6.BackgroundImagePosition = DevComponents.DotNetBar.eStyleBackgroundImage.BottomRight;
			this.superTabControlPanel6.Controls.Add(this.projectSettings1);
			this.superTabControlPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.superTabControlPanel6.Location = new System.Drawing.Point(143, 0);
			this.superTabControlPanel6.Name = "superTabControlPanel6";
			this.superTabControlPanel6.Size = new System.Drawing.Size(787, 424);
			this.superTabControlPanel6.TabIndex = 2;
			this.superTabControlPanel6.TabItem = this.superTabItemOptions;
			// 
			// projectSettings1
			// 
			this.projectSettings1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.projectSettings1.Location = new System.Drawing.Point(0, 0);
			this.projectSettings1.Margin = new System.Windows.Forms.Padding(4);
			this.projectSettings1.Name = "projectSettings1";
			this.projectSettings1.Size = new System.Drawing.Size(787, 424);
			this.projectSettings1.TabIndex = 0;
			// 
			// superTabItemOptions
			// 
			this.superTabItemOptions.AttachedControl = this.superTabControlPanel6;
			this.superTabItemOptions.GlobalItem = false;
			this.superTabItemOptions.KeyTips = "N";
			this.superTabItemOptions.Name = "superTabItemOptions";
			this.superTabItemOptions.Text = "Options";
			// 
			// buttonNewProject
			// 
			this.buttonNewProject.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.buttonNewProject.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
			this.buttonNewProject.HoverImage = ((System.Drawing.Image)(resources.GetObject("buttonNewProject.HoverImage")));
			this.buttonNewProject.Image = ((System.Drawing.Image)(resources.GetObject("buttonNewProject.Image")));
			this.buttonNewProject.ImagePaddingHorizontal = 18;
			this.buttonNewProject.ImagePaddingVertical = 10;
			this.buttonNewProject.Name = "buttonNewProject";
			this.buttonNewProject.Stretch = true;
			this.buttonNewProject.Text = "New";
			this.buttonNewProject.Click += new System.EventHandler(this.buttonNewProject_Click);
			// 
			// buttonOpenProject
			// 
			this.buttonOpenProject.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.buttonOpenProject.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
			this.buttonOpenProject.HoverImage = ((System.Drawing.Image)(resources.GetObject("buttonOpenProject.HoverImage")));
			this.buttonOpenProject.Image = ((System.Drawing.Image)(resources.GetObject("buttonOpenProject.Image")));
			this.buttonOpenProject.ImagePaddingHorizontal = 18;
			this.buttonOpenProject.ImagePaddingVertical = 10;
			this.buttonOpenProject.KeyTips = "O";
			this.buttonOpenProject.Name = "buttonOpenProject";
			this.buttonOpenProject.Stretch = true;
			this.buttonOpenProject.Text = "Open";
			this.buttonOpenProject.Click += new System.EventHandler(this.buttonOpenProject_Click);
			// 
			// buttonSaveProject
			// 
			this.buttonSaveProject.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.buttonSaveProject.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
			this.buttonSaveProject.HoverImage = ((System.Drawing.Image)(resources.GetObject("buttonSaveProject.HoverImage")));
			this.buttonSaveProject.Image = ((System.Drawing.Image)(resources.GetObject("buttonSaveProject.Image")));
			this.buttonSaveProject.ImagePaddingHorizontal = 18;
			this.buttonSaveProject.ImagePaddingVertical = 10;
			this.buttonSaveProject.KeyTips = "S";
			this.buttonSaveProject.Name = "buttonSaveProject";
			this.buttonSaveProject.Stretch = true;
			this.buttonSaveProject.Text = "Save";
			this.buttonSaveProject.Click += new System.EventHandler(this.buttonSaveProject_Click);
			// 
			// buttonSaveAsProject
			// 
			this.buttonSaveAsProject.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.buttonSaveAsProject.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
			this.buttonSaveAsProject.HoverImage = ((System.Drawing.Image)(resources.GetObject("buttonSaveAsProject.HoverImage")));
			this.buttonSaveAsProject.Image = ((System.Drawing.Image)(resources.GetObject("buttonSaveAsProject.Image")));
			this.buttonSaveAsProject.ImagePaddingHorizontal = 18;
			this.buttonSaveAsProject.ImagePaddingVertical = 10;
			this.buttonSaveAsProject.Name = "buttonSaveAsProject";
			this.buttonSaveAsProject.Stretch = true;
			this.buttonSaveAsProject.Text = "Save As";
			this.buttonSaveAsProject.Click += new System.EventHandler(this.buttonSaveAsProject_Click);
			// 
			// buttonExit
			// 
			this.buttonExit.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.buttonExit.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
			this.buttonExit.Image = ((System.Drawing.Image)(resources.GetObject("buttonExit.Image")));
			this.buttonExit.ImagePaddingHorizontal = 18;
			this.buttonExit.ImagePaddingVertical = 10;
			this.buttonExit.KeyTips = "X";
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Stretch = true;
			this.buttonExit.Text = "Exit";
			this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
			// 
			// itemContainer13
			// 
			// 
			// 
			// 
			this.itemContainer13.BackgroundStyle.Class = "RibbonFileMenuContainer";
			this.itemContainer13.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainer13.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainer13.Name = "itemContainer13";
			this.itemContainer13.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainer14,
            this.itemContainer16});
			// 
			// itemContainer14
			// 
			// 
			// 
			// 
			this.itemContainer14.BackgroundStyle.Class = "RibbonFileMenuTwoColumnContainer";
			this.itemContainer14.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainer14.ItemSpacing = 0;
			this.itemContainer14.Name = "itemContainer14";
			this.itemContainer14.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainer15,
            this.galleryContainer3});
			// 
			// itemContainer15
			// 
			// 
			// 
			// 
			this.itemContainer15.BackgroundStyle.Class = "RibbonFileMenuColumnOneContainer";
			this.itemContainer15.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainer15.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainer15.MinimumSize = new System.Drawing.Size(120, 0);
			this.itemContainer15.Name = "itemContainer15";
			this.itemContainer15.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.mnuBoxNew,
            this.mnuBoxOpen,
            this.mnuBoxSave,
            this.mnuBoxSaveAs,
            this.mnuBoxLicense,
            this.mnuBoxCheckUpdates,
            this.mnuBoxAbout,
            this.buttonItem17});
			// 
			// mnuBoxNew
			// 
			this.mnuBoxNew.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.mnuBoxNew.HoverImage = ((System.Drawing.Image)(resources.GetObject("mnuBoxNew.HoverImage")));
			this.mnuBoxNew.Image = ((System.Drawing.Image)(resources.GetObject("mnuBoxNew.Image")));
			this.mnuBoxNew.Name = "mnuBoxNew";
			this.mnuBoxNew.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlN);
			this.mnuBoxNew.SubItemsExpandWidth = 24;
			this.mnuBoxNew.Text = "&New";
			this.mnuBoxNew.Click += new System.EventHandler(this.mnuBoxNew_Click);
			// 
			// mnuBoxOpen
			// 
			this.mnuBoxOpen.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.mnuBoxOpen.HoverImage = ((System.Drawing.Image)(resources.GetObject("mnuBoxOpen.HoverImage")));
			this.mnuBoxOpen.Image = ((System.Drawing.Image)(resources.GetObject("mnuBoxOpen.Image")));
			this.mnuBoxOpen.Name = "mnuBoxOpen";
			this.mnuBoxOpen.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlO);
			this.mnuBoxOpen.SubItemsExpandWidth = 24;
			this.mnuBoxOpen.Text = "&Open...";
			this.mnuBoxOpen.Click += new System.EventHandler(this.mnuBoxOpen_Click);
			// 
			// mnuBoxSave
			// 
			this.mnuBoxSave.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.mnuBoxSave.HoverImage = ((System.Drawing.Image)(resources.GetObject("mnuBoxSave.HoverImage")));
			this.mnuBoxSave.Image = ((System.Drawing.Image)(resources.GetObject("mnuBoxSave.Image")));
			this.mnuBoxSave.Name = "mnuBoxSave";
			this.mnuBoxSave.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlS);
			this.mnuBoxSave.SubItemsExpandWidth = 24;
			this.mnuBoxSave.Text = "&Save...";
			this.mnuBoxSave.Click += new System.EventHandler(this.mnuBoxSave_Click);
			// 
			// mnuBoxSaveAs
			// 
			this.mnuBoxSaveAs.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.mnuBoxSaveAs.HoverImage = ((System.Drawing.Image)(resources.GetObject("mnuBoxSaveAs.HoverImage")));
			this.mnuBoxSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("mnuBoxSaveAs.Image")));
			this.mnuBoxSaveAs.Name = "mnuBoxSaveAs";
			this.mnuBoxSaveAs.Text = "Save As...";
			this.mnuBoxSaveAs.Click += new System.EventHandler(this.mnuBoxSaveAs_Click);
			// 
			// mnuBoxLicense
			// 
			this.mnuBoxLicense.BeginGroup = true;
			this.mnuBoxLicense.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.mnuBoxLicense.HoverImage = ((System.Drawing.Image)(resources.GetObject("mnuBoxLicense.HoverImage")));
			this.mnuBoxLicense.Image = ((System.Drawing.Image)(resources.GetObject("mnuBoxLicense.Image")));
			this.mnuBoxLicense.Name = "mnuBoxLicense";
			this.mnuBoxLicense.Text = "License details...";
			this.mnuBoxLicense.Click += new System.EventHandler(this.mnuBoxLicense_Click);
			// 
			// mnuBoxCheckUpdates
			// 
			this.mnuBoxCheckUpdates.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.mnuBoxCheckUpdates.HoverImage = ((System.Drawing.Image)(resources.GetObject("mnuBoxCheckUpdates.HoverImage")));
			this.mnuBoxCheckUpdates.Image = ((System.Drawing.Image)(resources.GetObject("mnuBoxCheckUpdates.Image")));
			this.mnuBoxCheckUpdates.Name = "mnuBoxCheckUpdates";
			this.mnuBoxCheckUpdates.Text = "Check for updates";
			this.mnuBoxCheckUpdates.Click += new System.EventHandler(this.mnuBoxCheckUpdates_Click);
			// 
			// mnuBoxAbout
			// 
			this.mnuBoxAbout.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.mnuBoxAbout.HoverImage = ((System.Drawing.Image)(resources.GetObject("mnuBoxAbout.HoverImage")));
			this.mnuBoxAbout.Image = ((System.Drawing.Image)(resources.GetObject("mnuBoxAbout.Image")));
			this.mnuBoxAbout.Name = "mnuBoxAbout";
			this.mnuBoxAbout.Text = "About...";
			this.mnuBoxAbout.Click += new System.EventHandler(this.mnuBoxAbout_Click);
			// 
			// buttonItem17
			// 
			this.buttonItem17.BeginGroup = true;
			this.buttonItem17.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.buttonItem17.HoverImage = ((System.Drawing.Image)(resources.GetObject("buttonItem17.HoverImage")));
			this.buttonItem17.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem17.Image")));
			this.buttonItem17.Name = "buttonItem17";
			this.buttonItem17.SubItemsExpandWidth = 24;
			this.buttonItem17.Text = "E&xit";
			this.buttonItem17.Click += new System.EventHandler(this.buttonItem17_Click);
			// 
			// galleryContainer3
			// 
			// 
			// 
			// 
			this.galleryContainer3.BackgroundStyle.Class = "RibbonFileMenuColumnTwoContainer";
			this.galleryContainer3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.galleryContainer3.EnableGalleryPopup = false;
			this.galleryContainer3.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.galleryContainer3.MinimumSize = new System.Drawing.Size(180, 240);
			this.galleryContainer3.MultiLine = false;
			this.galleryContainer3.Name = "galleryContainer3";
			this.galleryContainer3.PopupUsesStandardScrollbars = false;
			this.galleryContainer3.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItem8,
            this.buttonItem36,
            this.buttonItem37,
            this.buttonItem38,
            this.buttonItem39});
			// 
			// labelItem8
			// 
			this.labelItem8.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom;
			this.labelItem8.BorderType = DevComponents.DotNetBar.eBorderType.Etched;
			this.labelItem8.CanCustomize = false;
			this.labelItem8.Image = ((System.Drawing.Image)(resources.GetObject("labelItem8.Image")));
			this.labelItem8.Name = "labelItem8";
			this.labelItem8.PaddingBottom = 7;
			this.labelItem8.PaddingTop = 5;
			this.labelItem8.Stretch = true;
			this.labelItem8.Text = "Recent Documents";
			// 
			// buttonItem36
			// 
			this.buttonItem36.Name = "buttonItem36";
			this.buttonItem36.Text = "&1. Short News 5-7.rtf";
			// 
			// buttonItem37
			// 
			this.buttonItem37.Name = "buttonItem37";
			this.buttonItem37.Text = "&2. Prospect Email.rtf";
			// 
			// buttonItem38
			// 
			this.buttonItem38.Name = "buttonItem38";
			this.buttonItem38.Text = "&3. Customer Email.rtf";
			// 
			// buttonItem39
			// 
			this.buttonItem39.Name = "buttonItem39";
			this.buttonItem39.Text = "&4. example.rtf";
			// 
			// itemContainer16
			// 
			// 
			// 
			// 
			this.itemContainer16.BackgroundStyle.Class = "RibbonFileMenuBottomContainer";
			this.itemContainer16.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainer16.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Right;
			this.itemContainer16.Name = "itemContainer16";
			this.itemContainer16.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.mnuBoxExit});
			// 
			// mnuBoxExit
			// 
			this.mnuBoxExit.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.mnuBoxExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.mnuBoxExit.HoverImage = ((System.Drawing.Image)(resources.GetObject("mnuBoxExit.HoverImage")));
			this.mnuBoxExit.Image = ((System.Drawing.Image)(resources.GetObject("mnuBoxExit.Image")));
			this.mnuBoxExit.Name = "mnuBoxExit";
			this.mnuBoxExit.SubItemsExpandWidth = 24;
			this.mnuBoxExit.Text = "E&xit";
			this.mnuBoxExit.Click += new System.EventHandler(this.mnuBoxExit_Click);
			// 
			// ribbonTabItemOptions
			// 
			this.ribbonTabItemOptions.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.ribbonTabItemOptions.HoverImage = ((System.Drawing.Image)(resources.GetObject("ribbonTabItemOptions.HoverImage")));
			this.ribbonTabItemOptions.Image = ((System.Drawing.Image)(resources.GetObject("ribbonTabItemOptions.Image")));
			this.ribbonTabItemOptions.Name = "ribbonTabItemOptions";
			this.ribbonTabItemOptions.Text = " &Project Settings";
			this.ribbonTabItemOptions.Click += new System.EventHandler(this.ribbonTab_Click);
			// 
			// ribbonTabItemTemplate
			// 
			this.ribbonTabItemTemplate.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.ribbonTabItemTemplate.Checked = true;
			this.ribbonTabItemTemplate.HoverImage = ((System.Drawing.Image)(resources.GetObject("ribbonTabItemTemplate.HoverImage")));
			this.ribbonTabItemTemplate.Image = ((System.Drawing.Image)(resources.GetObject("ribbonTabItemTemplate.Image")));
			this.ribbonTabItemTemplate.Name = "ribbonTabItemTemplate";
			this.ribbonTabItemTemplate.Text = " &Templates";
			this.ribbonTabItemTemplate.Click += new System.EventHandler(this.ribbonTab_Click);
			// 
			// ribbonTabItemFiles
			// 
			this.ribbonTabItemFiles.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.ribbonTabItemFiles.HoverImage = ((System.Drawing.Image)(resources.GetObject("ribbonTabItemFiles.HoverImage")));
			this.ribbonTabItemFiles.Image = ((System.Drawing.Image)(resources.GetObject("ribbonTabItemFiles.Image")));
			this.ribbonTabItemFiles.Name = "ribbonTabItemFiles";
			this.ribbonTabItemFiles.Text = " F&ile Generation";
			this.ribbonTabItemFiles.Click += new System.EventHandler(this.ribbonTab_Click);
			// 
			// buttonItem1
			// 
			this.buttonItem1.AutoExpandOnClick = true;
			this.buttonItem1.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
			this.buttonItem1.Name = "buttonItem1";
			this.buttonItem1.SplitButton = true;
			this.buttonItem1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItem1,
            this.buttonItemStyleOfficeBlue,
            this.buttonItemStyleOfficeBlack,
            this.buttonItemStyleOfficeSilver,
            this.buttonItemStyleVistaGlass,
            this.labelItem2,
            this.buttonItemStyleOffice2010Blue,
            this.buttonItemStyleOffice2010Silver,
            this.labelItem3,
            this.buttonItemStyleWindows7,
            this.labelItem4,
            this.buttonStyleCustom});
			this.buttonItem1.Text = "Style";
			this.buttonItem1.Visible = false;
			// 
			// labelItem1
			// 
			this.labelItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
			this.labelItem1.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom;
			this.labelItem1.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.labelItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(21)))), ((int)(((byte)(110)))));
			this.labelItem1.Name = "labelItem1";
			this.labelItem1.PaddingBottom = 1;
			this.labelItem1.PaddingLeft = 10;
			this.labelItem1.PaddingTop = 1;
			this.labelItem1.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
			this.labelItem1.Text = "Office 2007";
			// 
			// buttonItemStyleOfficeBlue
			// 
			this.buttonItemStyleOfficeBlue.Name = "buttonItemStyleOfficeBlue";
			this.buttonItemStyleOfficeBlue.Text = "Office 2007 <font color=\"Blue\"><b>Blue</b></font>";
			this.buttonItemStyleOfficeBlue.Click += new System.EventHandler(this.buttonItemStyleOfficeBlue_Click);
			// 
			// buttonItemStyleOfficeBlack
			// 
			this.buttonItemStyleOfficeBlack.Name = "buttonItemStyleOfficeBlack";
			this.buttonItemStyleOfficeBlack.Text = "Office 2007 <font color=\"Black\"><b>Black</b></font>";
			this.buttonItemStyleOfficeBlack.Click += new System.EventHandler(this.buttonItemStyleOfficeBlack_Click);
			// 
			// buttonItemStyleOfficeSilver
			// 
			this.buttonItemStyleOfficeSilver.Name = "buttonItemStyleOfficeSilver";
			this.buttonItemStyleOfficeSilver.Text = "Office 2007 <font color=\"Silver\"><b>Silver</b></font>";
			this.buttonItemStyleOfficeSilver.Click += new System.EventHandler(this.buttonItemStyleOfficeSilver_Click);
			// 
			// buttonItemStyleVistaGlass
			// 
			this.buttonItemStyleVistaGlass.Name = "buttonItemStyleVistaGlass";
			this.buttonItemStyleVistaGlass.Text = "Office 2007 <font color=\"darkblue\"><b>Vista Glass</b></font>";
			this.buttonItemStyleVistaGlass.Click += new System.EventHandler(this.buttonItemStyleVistaGlass_Click);
			// 
			// labelItem2
			// 
			this.labelItem2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
			this.labelItem2.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom;
			this.labelItem2.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.labelItem2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(21)))), ((int)(((byte)(110)))));
			this.labelItem2.Name = "labelItem2";
			this.labelItem2.PaddingBottom = 1;
			this.labelItem2.PaddingLeft = 10;
			this.labelItem2.PaddingTop = 1;
			this.labelItem2.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
			this.labelItem2.Text = "Office 2010";
			// 
			// buttonItemStyleOffice2010Blue
			// 
			this.buttonItemStyleOffice2010Blue.Name = "buttonItemStyleOffice2010Blue";
			this.buttonItemStyleOffice2010Blue.Text = "Office 2010 <font color=\"blue\"><b>Blue</b></font>";
			this.buttonItemStyleOffice2010Blue.Click += new System.EventHandler(this.buttonItemStyleOffice2010Blue_Click);
			// 
			// buttonItemStyleOffice2010Silver
			// 
			this.buttonItemStyleOffice2010Silver.Name = "buttonItemStyleOffice2010Silver";
			this.buttonItemStyleOffice2010Silver.Text = "Office 2010 <font color=\"silver\"><b>Silver</b></font>";
			this.buttonItemStyleOffice2010Silver.Click += new System.EventHandler(this.buttonItemStyleOffice2010Silver_Click);
			// 
			// labelItem3
			// 
			this.labelItem3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
			this.labelItem3.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom;
			this.labelItem3.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.labelItem3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(21)))), ((int)(((byte)(110)))));
			this.labelItem3.Name = "labelItem3";
			this.labelItem3.PaddingBottom = 1;
			this.labelItem3.PaddingLeft = 10;
			this.labelItem3.PaddingTop = 1;
			this.labelItem3.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
			this.labelItem3.Text = "Windows 7";
			// 
			// buttonItemStyleWindows7
			// 
			this.buttonItemStyleWindows7.Name = "buttonItemStyleWindows7";
			this.buttonItemStyleWindows7.Text = "Windows 7 <font color=\"blue\"><b>Blue</b></font>";
			this.buttonItemStyleWindows7.Click += new System.EventHandler(this.buttonItemStyleWindows7_Click);
			// 
			// labelItem4
			// 
			this.labelItem4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(231)))), ((int)(((byte)(238)))));
			this.labelItem4.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom;
			this.labelItem4.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.labelItem4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(21)))), ((int)(((byte)(110)))));
			this.labelItem4.Name = "labelItem4";
			this.labelItem4.PaddingBottom = 1;
			this.labelItem4.PaddingLeft = 10;
			this.labelItem4.PaddingTop = 1;
			this.labelItem4.SingleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
			this.labelItem4.Text = "Custom";
			// 
			// buttonStyleCustom
			// 
			this.buttonStyleCustom.Image = ((System.Drawing.Image)(resources.GetObject("buttonStyleCustom.Image")));
			this.buttonStyleCustom.Name = "buttonStyleCustom";
			this.buttonStyleCustom.Text = "Custom colour";
			this.buttonStyleCustom.SelectedColorChanged += new System.EventHandler(this.buttonStyleCustom_SelectedColorChanged);
			this.buttonStyleCustom.ColorPreview += new DevComponents.DotNetBar.ColorPreviewEventHandler(this.buttonStyleCustom_ColorPreview);
			this.buttonStyleCustom.ExpandChange += new System.EventHandler(this.buttonStyleCustom_ExpandChange);
			// 
			// mnuHelp
			// 
			this.mnuHelp.Image = ((System.Drawing.Image)(resources.GetObject("mnuHelp.Image")));
			this.mnuHelp.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
			this.mnuHelp.Name = "mnuHelp";
			this.mnuHelp.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.mnuForums,
            this.mnuReportBug,
            this.mnuSuggestion});
			this.mnuHelp.Text = "Help";
			this.mnuHelp.Click += new System.EventHandler(this.mnuHelp_Click);
			// 
			// mnuForums
			// 
			this.mnuForums.Image = ((System.Drawing.Image)(resources.GetObject("mnuForums.Image")));
			this.mnuForums.Name = "mnuForums";
			this.mnuForums.Text = "Forums";
			this.mnuForums.Click += new System.EventHandler(this.mnuForums_Click);
			// 
			// mnuReportBug
			// 
			this.mnuReportBug.Image = ((System.Drawing.Image)(resources.GetObject("mnuReportBug.Image")));
			this.mnuReportBug.Name = "mnuReportBug";
			this.mnuReportBug.Text = "Report a bug";
			this.mnuReportBug.Click += new System.EventHandler(this.mnuReportBug_Click);
			// 
			// mnuSuggestion
			// 
			this.mnuSuggestion.Image = ((System.Drawing.Image)(resources.GetObject("mnuSuggestion.Image")));
			this.mnuSuggestion.Name = "mnuSuggestion";
			this.mnuSuggestion.Text = "Make a suggestion";
			this.mnuSuggestion.Click += new System.EventHandler(this.mnuSuggestion_Click);
			// 
			// mnuTopNew
			// 
			this.mnuTopNew.HoverImage = ((System.Drawing.Image)(resources.GetObject("mnuTopNew.HoverImage")));
			this.mnuTopNew.Image = ((System.Drawing.Image)(resources.GetObject("mnuTopNew.Image")));
			this.mnuTopNew.Name = "mnuTopNew";
			this.mnuTopNew.Text = "buttonItem3";
			this.mnuTopNew.Click += new System.EventHandler(this.mnuTopNew_Click);
			// 
			// mnuTopOpen2
			// 
			this.mnuTopOpen2.HoverImage = ((System.Drawing.Image)(resources.GetObject("mnuTopOpen2.HoverImage")));
			this.mnuTopOpen2.Image = ((System.Drawing.Image)(resources.GetObject("mnuTopOpen2.Image")));
			this.mnuTopOpen2.Name = "mnuTopOpen2";
			this.mnuTopOpen2.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem6});
			this.mnuTopOpen2.Text = "buttonItem3";
			this.mnuTopOpen2.Click += new System.EventHandler(this.buttonItem3_Click);
			// 
			// buttonItem6
			// 
			this.buttonItem6.Name = "buttonItem6";
			this.buttonItem6.Text = "New Item";
			// 
			// mnuSaveTop
			// 
			this.mnuSaveTop.HoverImage = ((System.Drawing.Image)(resources.GetObject("mnuSaveTop.HoverImage")));
			this.mnuSaveTop.Image = ((System.Drawing.Image)(resources.GetObject("mnuSaveTop.Image")));
			this.mnuSaveTop.Name = "mnuSaveTop";
			this.mnuSaveTop.Text = "Save";
			this.mnuSaveTop.Click += new System.EventHandler(this.mnuSaveTop_Click);
			// 
			// buttonItemResetDefaultOptions
			// 
			this.buttonItemResetDefaultOptions.Image = global::ArchAngel.Workbench.Properties.Resources.cog_refresh_32;
			this.buttonItemResetDefaultOptions.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
			this.buttonItemResetDefaultOptions.Name = "buttonItemResetDefaultOptions";
			this.buttonItemResetDefaultOptions.SubItemsExpandWidth = 14;
			this.buttonItemResetDefaultOptions.Text = "Reset Default Values";
			this.buttonItemResetDefaultOptions.Click += new System.EventHandler(this.buttonItemResetDefaultOptions_Click);
			// 
			// mnuChangeOutputPath
			// 
			this.mnuChangeOutputPath.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.mnuChangeOutputPath.Image = global::ArchAngel.Workbench.Properties.Resources.folder_add_32;
			this.mnuChangeOutputPath.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
			this.mnuChangeOutputPath.Name = "mnuChangeOutputPath";
			this.mnuChangeOutputPath.SubItemsExpandWidth = 14;
			this.mnuChangeOutputPath.Text = "Change Output Path";
			this.mnuChangeOutputPath.Tooltip = "Changes the path the generated files are written to.";
			this.mnuChangeOutputPath.Click += new System.EventHandler(this.mnuChangeOutputPath_Click);
			// 
			// mnuRefresh
			// 
			this.mnuRefresh.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.mnuRefresh.Image = ((System.Drawing.Image)(resources.GetObject("mnuRefresh.Image")));
			this.mnuRefresh.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
			this.mnuRefresh.Name = "mnuRefresh";
			this.mnuRefresh.SubItemsExpandWidth = 14;
			this.mnuRefresh.Text = "&Refresh";
			this.mnuRefresh.Click += new System.EventHandler(this.mnuRefresh_Click);
			// 
			// mnuWriteFilesToDisk
			// 
			this.mnuWriteFilesToDisk.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.mnuWriteFilesToDisk.HoverImage = ((System.Drawing.Image)(resources.GetObject("mnuWriteFilesToDisk.HoverImage")));
			this.mnuWriteFilesToDisk.Image = ((System.Drawing.Image)(resources.GetObject("mnuWriteFilesToDisk.Image")));
			this.mnuWriteFilesToDisk.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
			this.mnuWriteFilesToDisk.Name = "mnuWriteFilesToDisk";
			this.mnuWriteFilesToDisk.SubItemsExpandWidth = 14;
			this.superTooltip1.SetSuperTooltip(this.mnuWriteFilesToDisk, new DevComponents.DotNetBar.SuperTooltipInfo("Write files to disk", "<b>Note:</b> No files are written to disk until you click this button.", "Write the selected files to disk, overwriting any existing ones.", ((System.Drawing.Image)(resources.GetObject("mnuWriteFilesToDisk.SuperTooltip"))), ((System.Drawing.Image)(resources.GetObject("mnuWriteFilesToDisk.SuperTooltip1"))), DevComponents.DotNetBar.eTooltipColor.Gray));
			this.mnuWriteFilesToDisk.Text = "Write files";
			this.mnuWriteFilesToDisk.Click += new System.EventHandler(this.mnuWriteFilesToDisk_Click);
			// 
			// itemContainer1
			// 
			// 
			// 
			// 
			this.itemContainer1.BackgroundStyle.Class = "";
			this.itemContainer1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainer1.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainer1.Name = "itemContainer1";
			this.itemContainer1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainer2,
            this.mnuFontDecrease,
            this.mnuSwitchHighlighting});
			// 
			// itemContainer2
			// 
			// 
			// 
			// 
			this.itemContainer2.BackgroundStyle.Class = "";
			this.itemContainer2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainer2.Name = "itemContainer2";
			this.itemContainer2.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.mnuFontIncrease});
			// 
			// mnuFontIncrease
			// 
			this.mnuFontIncrease.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.mnuFontIncrease.Image = ((System.Drawing.Image)(resources.GetObject("mnuFontIncrease.Image")));
			this.mnuFontIncrease.Name = "mnuFontIncrease";
			this.mnuFontIncrease.SubItemsExpandWidth = 14;
			this.mnuFontIncrease.Text = "Increase Font";
			// 
			// mnuFontDecrease
			// 
			this.mnuFontDecrease.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.mnuFontDecrease.Image = ((System.Drawing.Image)(resources.GetObject("mnuFontDecrease.Image")));
			this.mnuFontDecrease.Name = "mnuFontDecrease";
			this.mnuFontDecrease.SubItemsExpandWidth = 14;
			this.mnuFontDecrease.Text = "Decrease Font";
			// 
			// mnuSwitchHighlighting
			// 
			this.mnuSwitchHighlighting.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.mnuSwitchHighlighting.Image = ((System.Drawing.Image)(resources.GetObject("mnuSwitchHighlighting.Image")));
			this.mnuSwitchHighlighting.Name = "mnuSwitchHighlighting";
			this.mnuSwitchHighlighting.Text = "Switch Highlighting";
			// 
			// itemContainer18
			// 
			// 
			// 
			// 
			this.itemContainer18.BackgroundStyle.Class = "";
			this.itemContainer18.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainer18.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainer18.Name = "itemContainer18";
			this.itemContainer18.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.mnuToggleBreakpoint,
            this.buttonItem4});
			// 
			// mnuToggleBreakpoint
			// 
			this.mnuToggleBreakpoint.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.mnuToggleBreakpoint.Image = ((System.Drawing.Image)(resources.GetObject("mnuToggleBreakpoint.Image")));
			this.mnuToggleBreakpoint.Name = "mnuToggleBreakpoint";
			this.mnuToggleBreakpoint.SubItemsExpandWidth = 14;
			this.mnuToggleBreakpoint.Text = "Toggle";
			// 
			// buttonItem4
			// 
			this.buttonItem4.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.buttonItem4.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem4.Image")));
			this.buttonItem4.Name = "buttonItem4";
			this.buttonItem4.SubItemsExpandWidth = 14;
			this.buttonItem4.Text = "Delete All";
			// 
			// galleryContainer1
			// 
			// 
			// 
			// 
			this.galleryContainer1.BackgroundStyle.Class = "";
			this.galleryContainer1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.galleryContainer1.MinimumSize = new System.Drawing.Size(58, 58);
			this.galleryContainer1.Name = "galleryContainer1";
			this.galleryContainer1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem5});
			this.galleryContainer1.Text = "zdf";
			// 
			// buttonItem5
			// 
			this.buttonItem5.Name = "buttonItem5";
			this.buttonItem5.Text = "Param1";
			// 
			// itemContainer3
			// 
			// 
			// 
			// 
			this.itemContainer3.BackgroundStyle.Class = "";
			this.itemContainer3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainer3.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainer3.Name = "itemContainer3";
			this.itemContainer3.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.mnuDebugRestart,
            this.mnuDebugContinue});
			// 
			// mnuDebugRestart
			// 
			this.mnuDebugRestart.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.mnuDebugRestart.Image = ((System.Drawing.Image)(resources.GetObject("mnuDebugRestart.Image")));
			this.mnuDebugRestart.Name = "mnuDebugRestart";
			this.mnuDebugRestart.SubItemsExpandWidth = 14;
			this.mnuDebugRestart.Text = "Restart";
			this.mnuDebugRestart.Tooltip = "Restart the debug process";
			// 
			// mnuDebugContinue
			// 
			this.mnuDebugContinue.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.mnuDebugContinue.Image = ((System.Drawing.Image)(resources.GetObject("mnuDebugContinue.Image")));
			this.mnuDebugContinue.Name = "mnuDebugContinue";
			this.mnuDebugContinue.SubItemsExpandWidth = 14;
			this.mnuDebugContinue.Text = "Continue";
			// 
			// itemContainer4
			// 
			// 
			// 
			// 
			this.itemContainer4.BackgroundStyle.Class = "";
			this.itemContainer4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainer4.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainer4.Name = "itemContainer4";
			this.itemContainer4.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.mnuDebugStepInto,
            this.mnuDebugStepOver});
			// 
			// mnuDebugStepInto
			// 
			this.mnuDebugStepInto.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.mnuDebugStepInto.Image = ((System.Drawing.Image)(resources.GetObject("mnuDebugStepInto.Image")));
			this.mnuDebugStepInto.Name = "mnuDebugStepInto";
			this.mnuDebugStepInto.SubItemsExpandWidth = 14;
			this.mnuDebugStepInto.Text = "Step Into";
			// 
			// mnuDebugStepOver
			// 
			this.mnuDebugStepOver.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.mnuDebugStepOver.Image = ((System.Drawing.Image)(resources.GetObject("mnuDebugStepOver.Image")));
			this.mnuDebugStepOver.Name = "mnuDebugStepOver";
			this.mnuDebugStepOver.SubItemsExpandWidth = 14;
			this.mnuDebugStepOver.Text = "Step Over";
			// 
			// itemContainer11
			// 
			// 
			// 
			// 
			this.itemContainer11.BackgroundStyle.Class = "";
			this.itemContainer11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainer11.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainer11.Name = "itemContainer11";
			this.itemContainer11.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.mnuFindNext,
            this.mnuReplace});
			// 
			// mnuFindNext
			// 
			this.mnuFindNext.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.mnuFindNext.Image = ((System.Drawing.Image)(resources.GetObject("mnuFindNext.Image")));
			this.mnuFindNext.Name = "mnuFindNext";
			this.mnuFindNext.Text = "Find Next";
			// 
			// mnuReplace
			// 
			this.mnuReplace.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.mnuReplace.Image = ((System.Drawing.Image)(resources.GetObject("mnuReplace.Image")));
			this.mnuReplace.Name = "mnuReplace";
			this.mnuReplace.Text = "Replace";
			// 
			// itemContainer12
			// 
			// 
			// 
			// 
			this.itemContainer12.BackgroundStyle.Class = "";
			this.itemContainer12.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.itemContainer12.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
			this.itemContainer12.Name = "itemContainer12";
			this.itemContainer12.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.mnuCut,
            this.mnuCopy});
			this.itemContainer12.Text = "Cut";
			// 
			// mnuCut
			// 
			this.mnuCut.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.mnuCut.Image = ((System.Drawing.Image)(resources.GetObject("mnuCut.Image")));
			this.mnuCut.Name = "mnuCut";
			this.mnuCut.Text = "Cut";
			// 
			// mnuCopy
			// 
			this.mnuCopy.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.mnuCopy.Image = ((System.Drawing.Image)(resources.GetObject("mnuCopy.Image")));
			this.mnuCopy.Name = "mnuCopy";
			this.mnuCopy.Text = "Copy";
			// 
			// mnuDebugStart
			// 
			this.mnuDebugStart.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.mnuDebugStart.Image = ((System.Drawing.Image)(resources.GetObject("mnuDebugStart.Image")));
			this.mnuDebugStart.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
			this.mnuDebugStart.Name = "mnuDebugStart";
			this.mnuDebugStart.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.F5);
			this.mnuDebugStart.SubItemsExpandWidth = 14;
			this.mnuDebugStart.Text = "Start";
			// 
			// mnuDebugStop
			// 
			this.mnuDebugStop.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.mnuDebugStop.Image = ((System.Drawing.Image)(resources.GetObject("mnuDebugStop.Image")));
			this.mnuDebugStop.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
			this.mnuDebugStop.Name = "mnuDebugStop";
			this.mnuDebugStop.SubItemsExpandWidth = 14;
			this.mnuDebugStop.Text = "Stop";
			// 
			// mnuFind
			// 
			this.mnuFind.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.mnuFind.Image = ((System.Drawing.Image)(resources.GetObject("mnuFind.Image")));
			this.mnuFind.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
			this.mnuFind.Name = "mnuFind";
			this.mnuFind.SubItemsExpandWidth = 14;
			this.mnuFind.Text = "Find";
			// 
			// mnuNewFunction
			// 
			this.mnuNewFunction.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.mnuNewFunction.Image = ((System.Drawing.Image)(resources.GetObject("mnuNewFunction.Image")));
			this.mnuNewFunction.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
			this.mnuNewFunction.Name = "mnuNewFunction";
			this.mnuNewFunction.SubItemsExpandWidth = 14;
			this.mnuNewFunction.Text = "New";
			// 
			// mnuDeleteFunction
			// 
			this.mnuDeleteFunction.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.mnuDeleteFunction.Image = ((System.Drawing.Image)(resources.GetObject("mnuDeleteFunction.Image")));
			this.mnuDeleteFunction.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
			this.mnuDeleteFunction.Name = "mnuDeleteFunction";
			this.mnuDeleteFunction.SubItemsExpandWidth = 14;
			this.mnuDeleteFunction.Text = "Delete";
			// 
			// mnuPaste
			// 
			this.mnuPaste.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.mnuPaste.Image = ((System.Drawing.Image)(resources.GetObject("mnuPaste.Image")));
			this.mnuPaste.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
			this.mnuPaste.Name = "mnuPaste";
			this.mnuPaste.SubItemsExpandWidth = 14;
			this.mnuPaste.Text = "Paste";
			// 
			// superTooltip1
			// 
			this.superTooltip1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// styleManager1
			// 
			this.styleManager1.ManagerColorTint = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
			this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2010Black;
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
			this.BackColor = System.Drawing.Color.Gainsboro;
			this.ClientSize = new System.Drawing.Size(940, 482);
			this.Controls.Add(this.superTabControlFileMenu);
			this.Controls.Add(this.panelContent);
			this.Controls.Add(this.tracePanel);
			this.Controls.Add(this.ribbonControl);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "FormMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "`";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
			this.Load += new System.EventHandler(this.FormMain_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
			this.Resize += new System.EventHandler(this.FormMain_Resize);
			this.tracePanel.ResumeLayout(false);
			this.panelDebugToolbar.ResumeLayout(false);
			this.panelDebugToolbar.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.superTabControlFileMenu)).EndInit();
			this.superTabControlFileMenu.ResumeLayout(false);
			this.superTabControlPanelHelp.ResumeLayout(false);
			this.superTabControlPanelHelp.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			this.panelAbout.ResumeLayout(false);
			this.panelAbout.PerformLayout();
			this.superTabControlPanel5.ResumeLayout(false);
			this.panelEx1.ResumeLayout(false);
			this.superTabControlPanel6.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ImageList imageListNavBar;
		private System.Windows.Forms.Panel panelContent;
		private System.Windows.Forms.ImageList imageListHeading;
		private System.Windows.Forms.ImageList imageListMisc;
		private System.Windows.Forms.Panel tracePanel;
		private System.Windows.Forms.RichTextBox textBoxTrace;
		private System.Windows.Forms.Panel panelDebugToolbar;
		private System.Windows.Forms.CheckBox cbLimitSize;
		private System.Windows.Forms.Button btnSaveLogToFile;
		private System.Windows.Forms.CheckBox cbWordWrap;
		private System.Windows.Forms.CheckBox cbAutoScroll;
		private System.Windows.Forms.CheckBox cbEnableDebugLogging;
		private System.ComponentModel.BackgroundWorker backgroundWorkerUpdateChecker;
		private DevComponents.DotNetBar.RibbonControl ribbonControl;
		private DevComponents.DotNetBar.ItemContainer itemContainer1;
		private DevComponents.DotNetBar.ItemContainer itemContainer2;
		private DevComponents.DotNetBar.ButtonItem mnuFontIncrease;
		private DevComponents.DotNetBar.ButtonItem mnuFontDecrease;
		private DevComponents.DotNetBar.ButtonItem mnuSwitchHighlighting;
		private DevComponents.DotNetBar.ItemContainer itemContainer18;
		private DevComponents.DotNetBar.ButtonItem mnuToggleBreakpoint;
		private DevComponents.DotNetBar.ButtonItem buttonItem4;
		private DevComponents.DotNetBar.GalleryContainer galleryContainer1;
		private DevComponents.DotNetBar.ButtonItem buttonItem5;
		private DevComponents.DotNetBar.ButtonItem mnuDebugStart;
		private DevComponents.DotNetBar.ButtonItem mnuDebugStop;
		private DevComponents.DotNetBar.ItemContainer itemContainer3;
		private DevComponents.DotNetBar.ButtonItem mnuDebugRestart;
		private DevComponents.DotNetBar.ButtonItem mnuDebugContinue;
		private DevComponents.DotNetBar.ItemContainer itemContainer4;
		private DevComponents.DotNetBar.ButtonItem mnuDebugStepInto;
		private DevComponents.DotNetBar.ButtonItem mnuDebugStepOver;
		private DevComponents.DotNetBar.ButtonItem mnuFind;
		private DevComponents.DotNetBar.ItemContainer itemContainer11;
		private DevComponents.DotNetBar.ButtonItem mnuFindNext;
		private DevComponents.DotNetBar.ButtonItem mnuReplace;
		private DevComponents.DotNetBar.ButtonItem mnuNewFunction;
		private DevComponents.DotNetBar.ButtonItem mnuDeleteFunction;
		private DevComponents.DotNetBar.ButtonItem mnuPaste;
		private DevComponents.DotNetBar.ItemContainer itemContainer12;
		private DevComponents.DotNetBar.ButtonItem mnuCut;
		private DevComponents.DotNetBar.ButtonItem mnuCopy;
		private DevComponents.DotNetBar.ButtonItem mnuRefresh;
		private DevComponents.DotNetBar.RibbonTabItem ribbonTabItemOptions;
		private DevComponents.DotNetBar.RibbonTabItem ribbonTabItemTemplate;
		private DevComponents.DotNetBar.RibbonTabItem ribbonTabItemFiles;
		private DevComponents.DotNetBar.ButtonItem mnuHelp;
		private DevComponents.DotNetBar.Office2007StartButton office2007StartButton1;
		private DevComponents.DotNetBar.ItemContainer itemContainer13;
		private DevComponents.DotNetBar.ItemContainer itemContainer14;
		private DevComponents.DotNetBar.ItemContainer itemContainer15;
		private DevComponents.DotNetBar.ButtonItem mnuBoxNew;
		private DevComponents.DotNetBar.ButtonItem mnuBoxOpen;
		private DevComponents.DotNetBar.ButtonItem mnuBoxSave;
		private DevComponents.DotNetBar.ButtonItem mnuBoxSaveAs;
		private DevComponents.DotNetBar.ButtonItem buttonItem17;
		private DevComponents.DotNetBar.GalleryContainer galleryContainer3;
		private DevComponents.DotNetBar.LabelItem labelItem8;
		private DevComponents.DotNetBar.ButtonItem buttonItem36;
		private DevComponents.DotNetBar.ButtonItem buttonItem37;
		private DevComponents.DotNetBar.ButtonItem buttonItem38;
		private DevComponents.DotNetBar.ButtonItem buttonItem39;
		private DevComponents.DotNetBar.ItemContainer itemContainer16;
		private DevComponents.DotNetBar.ButtonItem mnuBoxExit;
		private DevComponents.DotNetBar.ButtonItem mnuSaveTop;
		private DevComponents.DotNetBar.ButtonItem buttonItem1;
		private DevComponents.DotNetBar.ButtonItem buttonItemStyleOfficeBlue;
		private DevComponents.DotNetBar.ButtonItem buttonItemStyleOfficeBlack;
		private DevComponents.DotNetBar.ButtonItem buttonItemStyleOfficeSilver;
		private DevComponents.DotNetBar.ButtonItem buttonItemStyleVistaGlass;
		private ButtonItem mnuBoxLicense;
		private ButtonItem mnuBoxCheckUpdates;
		private ButtonItem mnuBoxAbout;
		private ButtonItem mnuTopNew;
		private ButtonItem mnuTopOpen2;
		private ButtonItem buttonItem6;
		private ButtonItem mnuChangeOutputPath;
		private ButtonItem buttonItemResetDefaultOptions;
		private ButtonItem buttonItemStyleOffice2010Blue;
		private ButtonItem buttonItemStyleOffice2010Silver;
		private ButtonItem buttonItemStyleWindows7;
		private ColorPickerDropDown buttonStyleCustom;
		private LabelItem labelItem1;
		private LabelItem labelItem2;
		private LabelItem labelItem3;
		private LabelItem labelItem4;
		private ButtonItem mnuWriteFilesToDisk;
		private SuperTooltip superTooltip1;
		private ButtonItem mnuForums;
		private ButtonItem mnuReportBug;
		private ButtonItem mnuSuggestion;
		private StyleManager styleManager1;
		private SuperTabControl superTabControlFileMenu;
		private SuperTabControlPanel superTabControlPanelHelp;
		private SuperTabItem superTabItemHelp;
		private SuperTabControlPanel superTabControlPanel6;
		private SuperTabItem superTabItemOptions;
		private SuperTabControlPanel superTabControlPanel5;
		private SuperTabItem superTabItemRecentProjects;
		private ButtonItem buttonSaveProject;
		private ButtonItem buttonOpenProject;
		private ButtonItem buttonExit;
		private ButtonItem buttonNewProject;
		private ButtonItem buttonSaveAsProject;
		private ButtonX buttonSuggestion;
		private ButtonX buttonReportBug;
		private ButtonX buttonForums;
		private PanelEx panelEx1;
		private ItemPanel recentDocsItemPane;
		private LabelX labelX1;
		private UserControls.ProjectSettings projectSettings1;
		private LabelX labelX5;
		private System.Windows.Forms.LinkLabel linkLabelLicense;
		private System.Windows.Forms.Label labelAboutCopyright;
		private System.Windows.Forms.Label labelVersion;
		private System.Windows.Forms.Label labelLicenseRegistrationDetails;
		private LabelX labelLicenseDetails;
		private LabelX labelX3;
		private System.Windows.Forms.PictureBox pictureBox2;
		private LabelX labelX2;
		private System.Windows.Forms.Label labelErrorMessage;
		private System.Windows.Forms.Panel panelAbout;
		private System.Windows.Forms.Label labelSerialNumber;
		private ButtonX buttonCopySerial;
		private System.Windows.Forms.LinkLabel linkLabel2;
		private ButtonX buttonX1;
		private LabelX labelRegisteredTo;
	}
}
