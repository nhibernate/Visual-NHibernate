namespace ArchAngel.NHibernateHelper.LoadProjectWizard
{
	partial class SetNhConfig
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
			ArchAngel.Providers.EntityModel.UI.Presenters.ServerAndDatabaseHelper serverAndDatabaseHelper1 = new ArchAngel.Providers.EntityModel.UI.Presenters.ServerAndDatabaseHelper();
			this.buttonNext = new DevComponents.DotNetBar.ButtonX();
			this.buttonBack = new DevComponents.DotNetBar.ButtonX();
			this.superTabControl1 = new DevComponents.DotNetBar.SuperTabControl();
			this.superTabControlPanel1 = new DevComponents.DotNetBar.SuperTabControlPanel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.comboBoxUseProxyValidator = new DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.comboBoxUseOuterJoin = new DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.textBoxTransactionFactoryClass = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.comboBoxShowSql = new DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.textBoxQuerySubstitutions = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.comboBoxGenerateStatistics = new DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.labelX1 = new DevComponents.DotNetBar.LabelX();
			this.comboBoxByteCodeGenerator = new DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.labelX12 = new DevComponents.DotNetBar.LabelX();
			this.labelX13 = new DevComponents.DotNetBar.LabelX();
			this.labelX7 = new DevComponents.DotNetBar.LabelX();
			this.labelX8 = new DevComponents.DotNetBar.LabelX();
			this.labelX11 = new DevComponents.DotNetBar.LabelX();
			this.labelX9 = new DevComponents.DotNetBar.LabelX();
			this.labelX10 = new DevComponents.DotNetBar.LabelX();
			this.textBoxMaxFetchDepth = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.superTabItemGeneral = new DevComponents.DotNetBar.SuperTabItem();
			this.superTabControlPanel2 = new DevComponents.DotNetBar.SuperTabControlPanel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.textBoxCacheRegionPrefix = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.textBoxCacheQueryCacheFactory = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.labelX2 = new DevComponents.DotNetBar.LabelX();
			this.labelX6 = new DevComponents.DotNetBar.LabelX();
			this.labelX4 = new DevComponents.DotNetBar.LabelX();
			this.labelX5 = new DevComponents.DotNetBar.LabelX();
			this.labelX3 = new DevComponents.DotNetBar.LabelX();
			this.textBoxCacheProviderClass = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.comboBoxCacheUseMinimalPuts = new DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.comboBoxCacheUseQueryCache = new DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.superTabItemCache = new DevComponents.DotNetBar.SuperTabItem();
			this.superTabItemDatabase = new DevComponents.DotNetBar.SuperTabControlPanel();
			this.ucDatabaseInformation1 = new ArchAngel.Providers.EntityModel.UI.PropertyGrids.ucDatabaseInformation();
			this.superTabItem1 = new DevComponents.DotNetBar.SuperTabItem();
			this.labelConfigFile = new DevComponents.DotNetBar.LabelX();
			this.textBoxConfigFileLocation = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.buttonBrowse = new DevComponents.DotNetBar.ButtonX();
			this.buttonBrowseProjectFile = new DevComponents.DotNetBar.ButtonX();
			this.tbProjectLocation = new System.Windows.Forms.TextBox();
			this.labelX16 = new DevComponents.DotNetBar.LabelX();
			this.panelConfigSettings = new System.Windows.Forms.Panel();
			this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
			this.radioFile = new DevComponents.DotNetBar.Controls.CheckBoxX();
			this.radioManual = new DevComponents.DotNetBar.Controls.CheckBoxX();
			((System.ComponentModel.ISupportInitialize)(this.superTabControl1)).BeginInit();
			this.superTabControl1.SuspendLayout();
			this.superTabControlPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.superTabControlPanel2.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.superTabItemDatabase.SuspendLayout();
			this.panelConfigSettings.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonNext
			// 
			this.buttonNext.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonNext.CallBasePaintBackground = true;
			this.buttonNext.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonNext.Location = new System.Drawing.Point(416, 481);
			this.buttonNext.Name = "buttonNext";
			this.buttonNext.Size = new System.Drawing.Size(76, 23);
			this.buttonNext.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonNext.TabIndex = 52;
			this.buttonNext.Text = "Finish >";
			this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
			// 
			// buttonBack
			// 
			this.buttonBack.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonBack.CallBasePaintBackground = true;
			this.buttonBack.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonBack.Location = new System.Drawing.Point(15, 481);
			this.buttonBack.Name = "buttonBack";
			this.buttonBack.Size = new System.Drawing.Size(71, 23);
			this.buttonBack.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonBack.TabIndex = 51;
			this.buttonBack.Text = "< Back";
			this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
			// 
			// superTabControl1
			// 
			this.superTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.superTabControl1.CloseButtonOnTabsAlwaysDisplayed = false;
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
			this.superTabControl1.Controls.Add(this.superTabControlPanel1);
			this.superTabControl1.Controls.Add(this.superTabControlPanel2);
			this.superTabControl1.Controls.Add(this.superTabItemDatabase);
			this.superTabControl1.Location = new System.Drawing.Point(0, 42);
			this.superTabControl1.Name = "superTabControl1";
			this.superTabControl1.ReorderTabsEnabled = true;
			this.superTabControl1.SelectedTabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.superTabControl1.SelectedTabIndex = 1;
			this.superTabControl1.Size = new System.Drawing.Size(505, 334);
			this.superTabControl1.TabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.superTabControl1.TabIndex = 72;
			this.superTabControl1.Tabs.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabItemGeneral,
            this.superTabItemCache,
            this.superTabItem1});
			this.superTabControl1.TabStyle = DevComponents.DotNetBar.eSuperTabStyle.Office2010BackstageBlue;
			this.superTabControl1.Text = "superTabControl1";
			// 
			// superTabControlPanel1
			// 
			this.superTabControlPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
			this.superTabControlPanel1.Controls.Add(this.tableLayoutPanel2);
			this.superTabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.superTabControlPanel1.Location = new System.Drawing.Point(0, 23);
			this.superTabControlPanel1.Name = "superTabControlPanel1";
			this.superTabControlPanel1.Size = new System.Drawing.Size(505, 311);
			this.superTabControlPanel1.TabIndex = 1;
			this.superTabControlPanel1.TabItem = this.superTabItemGeneral;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 4;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 263F));
			this.tableLayoutPanel2.Controls.Add(this.comboBoxUseProxyValidator, 2, 8);
			this.tableLayoutPanel2.Controls.Add(this.comboBoxUseOuterJoin, 2, 7);
			this.tableLayoutPanel2.Controls.Add(this.textBoxTransactionFactoryClass, 2, 6);
			this.tableLayoutPanel2.Controls.Add(this.comboBoxShowSql, 2, 5);
			this.tableLayoutPanel2.Controls.Add(this.textBoxQuerySubstitutions, 2, 4);
			this.tableLayoutPanel2.Controls.Add(this.comboBoxGenerateStatistics, 2, 2);
			this.tableLayoutPanel2.Controls.Add(this.labelX1, 1, 1);
			this.tableLayoutPanel2.Controls.Add(this.comboBoxByteCodeGenerator, 2, 1);
			this.tableLayoutPanel2.Controls.Add(this.labelX12, 1, 8);
			this.tableLayoutPanel2.Controls.Add(this.labelX13, 1, 7);
			this.tableLayoutPanel2.Controls.Add(this.labelX7, 1, 2);
			this.tableLayoutPanel2.Controls.Add(this.labelX8, 1, 3);
			this.tableLayoutPanel2.Controls.Add(this.labelX11, 1, 6);
			this.tableLayoutPanel2.Controls.Add(this.labelX9, 1, 4);
			this.tableLayoutPanel2.Controls.Add(this.labelX10, 1, 5);
			this.tableLayoutPanel2.Controls.Add(this.textBoxMaxFetchDepth, 2, 3);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 9;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.Size = new System.Drawing.Size(505, 311);
			this.tableLayoutPanel2.TabIndex = 67;
			// 
			// comboBoxUseProxyValidator
			// 
			this.comboBoxUseProxyValidator.DisplayMember = "Text";
			this.comboBoxUseProxyValidator.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBoxUseProxyValidator.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.comboBoxUseProxyValidator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxUseProxyValidator.FormattingEnabled = true;
			this.comboBoxUseProxyValidator.ItemHeight = 14;
			this.comboBoxUseProxyValidator.Location = new System.Drawing.Point(153, 205);
			this.comboBoxUseProxyValidator.Name = "comboBoxUseProxyValidator";
			this.comboBoxUseProxyValidator.Size = new System.Drawing.Size(210, 20);
			this.comboBoxUseProxyValidator.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.comboBoxUseProxyValidator.TabIndex = 76;
			// 
			// comboBoxUseOuterJoin
			// 
			this.comboBoxUseOuterJoin.DisplayMember = "Text";
			this.comboBoxUseOuterJoin.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBoxUseOuterJoin.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.comboBoxUseOuterJoin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxUseOuterJoin.FormattingEnabled = true;
			this.comboBoxUseOuterJoin.ItemHeight = 14;
			this.comboBoxUseOuterJoin.Location = new System.Drawing.Point(153, 179);
			this.comboBoxUseOuterJoin.Name = "comboBoxUseOuterJoin";
			this.comboBoxUseOuterJoin.Size = new System.Drawing.Size(210, 20);
			this.comboBoxUseOuterJoin.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.comboBoxUseOuterJoin.TabIndex = 75;
			// 
			// textBoxTransactionFactoryClass
			// 
			// 
			// 
			// 
			this.textBoxTransactionFactoryClass.Border.Class = "TextBoxBorder";
			this.textBoxTransactionFactoryClass.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.textBoxTransactionFactoryClass.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxTransactionFactoryClass.Location = new System.Drawing.Point(153, 153);
			this.textBoxTransactionFactoryClass.Name = "textBoxTransactionFactoryClass";
			this.textBoxTransactionFactoryClass.Size = new System.Drawing.Size(210, 20);
			this.textBoxTransactionFactoryClass.TabIndex = 74;
			// 
			// comboBoxShowSql
			// 
			this.comboBoxShowSql.DisplayMember = "Text";
			this.comboBoxShowSql.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBoxShowSql.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.comboBoxShowSql.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxShowSql.FormattingEnabled = true;
			this.comboBoxShowSql.ItemHeight = 14;
			this.comboBoxShowSql.Location = new System.Drawing.Point(153, 127);
			this.comboBoxShowSql.Name = "comboBoxShowSql";
			this.comboBoxShowSql.Size = new System.Drawing.Size(210, 20);
			this.comboBoxShowSql.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.comboBoxShowSql.TabIndex = 73;
			// 
			// textBoxQuerySubstitutions
			// 
			// 
			// 
			// 
			this.textBoxQuerySubstitutions.Border.Class = "TextBoxBorder";
			this.textBoxQuerySubstitutions.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.textBoxQuerySubstitutions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxQuerySubstitutions.Location = new System.Drawing.Point(153, 101);
			this.textBoxQuerySubstitutions.Name = "textBoxQuerySubstitutions";
			this.textBoxQuerySubstitutions.Size = new System.Drawing.Size(210, 20);
			this.textBoxQuerySubstitutions.TabIndex = 72;
			// 
			// comboBoxGenerateStatistics
			// 
			this.comboBoxGenerateStatistics.DisplayMember = "Text";
			this.comboBoxGenerateStatistics.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBoxGenerateStatistics.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.comboBoxGenerateStatistics.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxGenerateStatistics.FormattingEnabled = true;
			this.comboBoxGenerateStatistics.ItemHeight = 14;
			this.comboBoxGenerateStatistics.Location = new System.Drawing.Point(153, 49);
			this.comboBoxGenerateStatistics.Name = "comboBoxGenerateStatistics";
			this.comboBoxGenerateStatistics.Size = new System.Drawing.Size(210, 20);
			this.comboBoxGenerateStatistics.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.comboBoxGenerateStatistics.TabIndex = 71;
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
			this.labelX1.Location = new System.Drawing.Point(23, 23);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(101, 15);
			this.labelX1.TabIndex = 55;
			this.labelX1.Text = "Bytecode Generator";
			// 
			// comboBoxByteCodeGenerator
			// 
			this.comboBoxByteCodeGenerator.DisplayMember = "Text";
			this.comboBoxByteCodeGenerator.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBoxByteCodeGenerator.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.comboBoxByteCodeGenerator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxByteCodeGenerator.FormattingEnabled = true;
			this.comboBoxByteCodeGenerator.ItemHeight = 14;
			this.comboBoxByteCodeGenerator.Location = new System.Drawing.Point(153, 23);
			this.comboBoxByteCodeGenerator.Name = "comboBoxByteCodeGenerator";
			this.comboBoxByteCodeGenerator.Size = new System.Drawing.Size(210, 20);
			this.comboBoxByteCodeGenerator.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.comboBoxByteCodeGenerator.TabIndex = 54;
			// 
			// labelX12
			// 
			this.labelX12.AutoSize = true;
			// 
			// 
			// 
			this.labelX12.BackgroundStyle.Class = "";
			this.labelX12.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX12.ForeColor = System.Drawing.Color.White;
			this.labelX12.Location = new System.Drawing.Point(23, 205);
			this.labelX12.Name = "labelX12";
			this.labelX12.Size = new System.Drawing.Size(97, 15);
			this.labelX12.TabIndex = 69;
			this.labelX12.Text = "Use proxy validator";
			// 
			// labelX13
			// 
			this.labelX13.AutoSize = true;
			// 
			// 
			// 
			this.labelX13.BackgroundStyle.Class = "";
			this.labelX13.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX13.ForeColor = System.Drawing.Color.White;
			this.labelX13.Location = new System.Drawing.Point(23, 179);
			this.labelX13.Name = "labelX13";
			this.labelX13.Size = new System.Drawing.Size(71, 15);
			this.labelX13.TabIndex = 70;
			this.labelX13.Text = "Use outer-join";
			// 
			// labelX7
			// 
			this.labelX7.AutoSize = true;
			// 
			// 
			// 
			this.labelX7.BackgroundStyle.Class = "";
			this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX7.ForeColor = System.Drawing.Color.White;
			this.labelX7.Location = new System.Drawing.Point(23, 49);
			this.labelX7.Name = "labelX7";
			this.labelX7.Size = new System.Drawing.Size(94, 15);
			this.labelX7.TabIndex = 64;
			this.labelX7.Text = "Generate statistics";
			// 
			// labelX8
			// 
			this.labelX8.AutoSize = true;
			// 
			// 
			// 
			this.labelX8.BackgroundStyle.Class = "";
			this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX8.ForeColor = System.Drawing.Color.White;
			this.labelX8.Location = new System.Drawing.Point(23, 75);
			this.labelX8.Name = "labelX8";
			this.labelX8.Size = new System.Drawing.Size(80, 15);
			this.labelX8.TabIndex = 65;
			this.labelX8.Text = "Max fetch depth";
			// 
			// labelX11
			// 
			this.labelX11.AutoSize = true;
			// 
			// 
			// 
			this.labelX11.BackgroundStyle.Class = "";
			this.labelX11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX11.ForeColor = System.Drawing.Color.White;
			this.labelX11.Location = new System.Drawing.Point(23, 153);
			this.labelX11.Name = "labelX11";
			this.labelX11.Size = new System.Drawing.Size(124, 15);
			this.labelX11.TabIndex = 68;
			this.labelX11.Text = "Transaction factory class";
			// 
			// labelX9
			// 
			this.labelX9.AutoSize = true;
			// 
			// 
			// 
			this.labelX9.BackgroundStyle.Class = "";
			this.labelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX9.ForeColor = System.Drawing.Color.White;
			this.labelX9.Location = new System.Drawing.Point(23, 101);
			this.labelX9.Name = "labelX9";
			this.labelX9.Size = new System.Drawing.Size(97, 15);
			this.labelX9.TabIndex = 66;
			this.labelX9.Text = "Query substitutions";
			// 
			// labelX10
			// 
			this.labelX10.AutoSize = true;
			// 
			// 
			// 
			this.labelX10.BackgroundStyle.Class = "";
			this.labelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX10.ForeColor = System.Drawing.Color.White;
			this.labelX10.Location = new System.Drawing.Point(23, 127);
			this.labelX10.Name = "labelX10";
			this.labelX10.Size = new System.Drawing.Size(55, 15);
			this.labelX10.TabIndex = 67;
			this.labelX10.Text = "Show SQL";
			// 
			// textBoxMaxFetchDepth
			// 
			// 
			// 
			// 
			this.textBoxMaxFetchDepth.Border.Class = "TextBoxBorder";
			this.textBoxMaxFetchDepth.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.textBoxMaxFetchDepth.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxMaxFetchDepth.Location = new System.Drawing.Point(153, 75);
			this.textBoxMaxFetchDepth.Name = "textBoxMaxFetchDepth";
			this.textBoxMaxFetchDepth.Size = new System.Drawing.Size(210, 20);
			this.textBoxMaxFetchDepth.TabIndex = 56;
			// 
			// superTabItemGeneral
			// 
			this.superTabItemGeneral.AttachedControl = this.superTabControlPanel1;
			this.superTabItemGeneral.GlobalItem = false;
			this.superTabItemGeneral.Name = "superTabItemGeneral";
			this.superTabItemGeneral.Text = "General Settings";
			// 
			// superTabControlPanel2
			// 
			this.superTabControlPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
			this.superTabControlPanel2.Controls.Add(this.tableLayoutPanel1);
			this.superTabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.superTabControlPanel2.Location = new System.Drawing.Point(0, 0);
			this.superTabControlPanel2.Name = "superTabControlPanel2";
			this.superTabControlPanel2.Size = new System.Drawing.Size(505, 334);
			this.superTabControlPanel2.TabIndex = 0;
			this.superTabControlPanel2.TabItem = this.superTabItemCache;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 4;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 312F));
			this.tableLayoutPanel1.Controls.Add(this.textBoxCacheRegionPrefix, 2, 3);
			this.tableLayoutPanel1.Controls.Add(this.textBoxCacheQueryCacheFactory, 2, 2);
			this.tableLayoutPanel1.Controls.Add(this.labelX2, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.labelX6, 1, 5);
			this.tableLayoutPanel1.Controls.Add(this.labelX4, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.labelX5, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.labelX3, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.textBoxCacheProviderClass, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.comboBoxCacheUseMinimalPuts, 2, 4);
			this.tableLayoutPanel1.Controls.Add(this.comboBoxCacheUseQueryCache, 2, 5);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 6;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(505, 334);
			this.tableLayoutPanel1.TabIndex = 66;
			// 
			// textBoxCacheRegionPrefix
			// 
			// 
			// 
			// 
			this.textBoxCacheRegionPrefix.Border.Class = "TextBoxBorder";
			this.textBoxCacheRegionPrefix.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.textBoxCacheRegionPrefix.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxCacheRegionPrefix.Location = new System.Drawing.Point(130, 75);
			this.textBoxCacheRegionPrefix.Name = "textBoxCacheRegionPrefix";
			this.textBoxCacheRegionPrefix.Size = new System.Drawing.Size(161, 20);
			this.textBoxCacheRegionPrefix.TabIndex = 68;
			// 
			// textBoxCacheQueryCacheFactory
			// 
			// 
			// 
			// 
			this.textBoxCacheQueryCacheFactory.Border.Class = "TextBoxBorder";
			this.textBoxCacheQueryCacheFactory.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.textBoxCacheQueryCacheFactory.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxCacheQueryCacheFactory.Location = new System.Drawing.Point(130, 49);
			this.textBoxCacheQueryCacheFactory.Name = "textBoxCacheQueryCacheFactory";
			this.textBoxCacheQueryCacheFactory.Size = new System.Drawing.Size(161, 20);
			this.textBoxCacheQueryCacheFactory.TabIndex = 67;
			// 
			// labelX2
			// 
			this.labelX2.AutoSize = true;
			// 
			// 
			// 
			this.labelX2.BackgroundStyle.Class = "";
			this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX2.ForeColor = System.Drawing.Color.White;
			this.labelX2.Location = new System.Drawing.Point(23, 23);
			this.labelX2.Name = "labelX2";
			this.labelX2.Size = new System.Drawing.Size(72, 15);
			this.labelX2.TabIndex = 59;
			this.labelX2.Text = "Provider class";
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
			this.labelX6.Location = new System.Drawing.Point(23, 127);
			this.labelX6.Name = "labelX6";
			this.labelX6.Size = new System.Drawing.Size(85, 15);
			this.labelX6.TabIndex = 63;
			this.labelX6.Text = "Use query cache";
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
			this.labelX4.Location = new System.Drawing.Point(23, 101);
			this.labelX4.Name = "labelX4";
			this.labelX4.Size = new System.Drawing.Size(87, 15);
			this.labelX4.TabIndex = 61;
			this.labelX4.Text = "Use minimal puts";
			// 
			// labelX5
			// 
			this.labelX5.AutoSize = true;
			// 
			// 
			// 
			this.labelX5.BackgroundStyle.Class = "";
			this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX5.ForeColor = System.Drawing.Color.White;
			this.labelX5.Location = new System.Drawing.Point(23, 75);
			this.labelX5.Name = "labelX5";
			this.labelX5.Size = new System.Drawing.Size(67, 15);
			this.labelX5.TabIndex = 62;
			this.labelX5.Text = "Region prefix";
			// 
			// labelX3
			// 
			this.labelX3.AutoSize = true;
			// 
			// 
			// 
			this.labelX3.BackgroundStyle.Class = "";
			this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX3.ForeColor = System.Drawing.Color.White;
			this.labelX3.Location = new System.Drawing.Point(23, 49);
			this.labelX3.Name = "labelX3";
			this.labelX3.Size = new System.Drawing.Size(101, 15);
			this.labelX3.TabIndex = 60;
			this.labelX3.Text = "Query cache factory";
			// 
			// textBoxCacheProviderClass
			// 
			// 
			// 
			// 
			this.textBoxCacheProviderClass.Border.Class = "TextBoxBorder";
			this.textBoxCacheProviderClass.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.textBoxCacheProviderClass.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxCacheProviderClass.Location = new System.Drawing.Point(130, 23);
			this.textBoxCacheProviderClass.Name = "textBoxCacheProviderClass";
			this.textBoxCacheProviderClass.Size = new System.Drawing.Size(161, 20);
			this.textBoxCacheProviderClass.TabIndex = 64;
			// 
			// comboBoxCacheUseMinimalPuts
			// 
			this.comboBoxCacheUseMinimalPuts.DisplayMember = "Text";
			this.comboBoxCacheUseMinimalPuts.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBoxCacheUseMinimalPuts.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.comboBoxCacheUseMinimalPuts.FormattingEnabled = true;
			this.comboBoxCacheUseMinimalPuts.ItemHeight = 14;
			this.comboBoxCacheUseMinimalPuts.Location = new System.Drawing.Point(130, 101);
			this.comboBoxCacheUseMinimalPuts.Name = "comboBoxCacheUseMinimalPuts";
			this.comboBoxCacheUseMinimalPuts.Size = new System.Drawing.Size(161, 20);
			this.comboBoxCacheUseMinimalPuts.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.comboBoxCacheUseMinimalPuts.TabIndex = 65;
			// 
			// comboBoxCacheUseQueryCache
			// 
			this.comboBoxCacheUseQueryCache.DisplayMember = "Text";
			this.comboBoxCacheUseQueryCache.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBoxCacheUseQueryCache.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.comboBoxCacheUseQueryCache.FormattingEnabled = true;
			this.comboBoxCacheUseQueryCache.ItemHeight = 14;
			this.comboBoxCacheUseQueryCache.Location = new System.Drawing.Point(130, 127);
			this.comboBoxCacheUseQueryCache.Name = "comboBoxCacheUseQueryCache";
			this.comboBoxCacheUseQueryCache.Size = new System.Drawing.Size(161, 20);
			this.comboBoxCacheUseQueryCache.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.comboBoxCacheUseQueryCache.TabIndex = 66;
			// 
			// superTabItemCache
			// 
			this.superTabItemCache.AttachedControl = this.superTabControlPanel2;
			this.superTabItemCache.GlobalItem = false;
			this.superTabItemCache.Name = "superTabItemCache";
			this.superTabItemCache.Text = "Cache Settings";
			// 
			// superTabItemDatabase
			// 
			this.superTabItemDatabase.Controls.Add(this.ucDatabaseInformation1);
			this.superTabItemDatabase.Dock = System.Windows.Forms.DockStyle.Fill;
			this.superTabItemDatabase.Location = new System.Drawing.Point(0, 0);
			this.superTabItemDatabase.Name = "superTabItemDatabase";
			this.superTabItemDatabase.Size = new System.Drawing.Size(505, 334);
			this.superTabItemDatabase.TabIndex = 0;
			this.superTabItemDatabase.TabItem = this.superTabItem1;
			// 
			// ucDatabaseInformation1
			// 
			this.ucDatabaseInformation1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.ucDatabaseInformation1.DatabaseHelper = serverAndDatabaseHelper1;
			this.ucDatabaseInformation1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ucDatabaseInformation1.EventRaisingDisabled = false;
			this.ucDatabaseInformation1.ForeColor = System.Drawing.Color.White;
			this.ucDatabaseInformation1.Location = new System.Drawing.Point(0, 0);
			this.ucDatabaseInformation1.Margin = new System.Windows.Forms.Padding(4);
			this.ucDatabaseInformation1.Name = "ucDatabaseInformation1";
			this.ucDatabaseInformation1.Password = "";
			this.ucDatabaseInformation1.Port = 1433;
			this.ucDatabaseInformation1.SelectedDatabaseType = ArchAngel.Providers.EntityModel.Controller.DatabaseLayer.DatabaseTypes.SQLServer2005;
			this.ucDatabaseInformation1.SelectedServerName = "WIN7-PC";
			this.ucDatabaseInformation1.ServiceName = "";
			this.ucDatabaseInformation1.Size = new System.Drawing.Size(505, 334);
			this.ucDatabaseInformation1.TabIndex = 0;
			this.ucDatabaseInformation1.UseIntegratedSecurity = true;
			this.ucDatabaseInformation1.Username = "sa";
			this.ucDatabaseInformation1.UsingDatabaseFile = false;
			// 
			// superTabItem1
			// 
			this.superTabItem1.AttachedControl = this.superTabItemDatabase;
			this.superTabItem1.GlobalItem = false;
			this.superTabItem1.Name = "superTabItem1";
			this.superTabItem1.Text = "Database";
			// 
			// labelConfigFile
			// 
			// 
			// 
			// 
			this.labelConfigFile.BackgroundStyle.Class = "";
			this.labelConfigFile.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelConfigFile.ForeColor = System.Drawing.Color.White;
			this.labelConfigFile.Location = new System.Drawing.Point(8, 3);
			this.labelConfigFile.Name = "labelConfigFile";
			this.labelConfigFile.Size = new System.Drawing.Size(109, 23);
			this.labelConfigFile.TabIndex = 73;
			this.labelConfigFile.Text = "Locate config file";
			// 
			// textBoxConfigFileLocation
			// 
			this.textBoxConfigFileLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			// 
			// 
			// 
			this.textBoxConfigFileLocation.Border.Class = "TextBoxBorder";
			this.textBoxConfigFileLocation.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.textBoxConfigFileLocation.Location = new System.Drawing.Point(102, 6);
			this.textBoxConfigFileLocation.Name = "textBoxConfigFileLocation";
			this.textBoxConfigFileLocation.Size = new System.Drawing.Size(249, 20);
			this.textBoxConfigFileLocation.TabIndex = 74;
			this.textBoxConfigFileLocation.TextChanged += new System.EventHandler(this.textBoxConfigFileLocation_TextChanged);
			// 
			// buttonBrowse
			// 
			this.buttonBrowse.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonBrowse.CallBasePaintBackground = true;
			this.buttonBrowse.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonBrowse.Location = new System.Drawing.Point(357, 3);
			this.buttonBrowse.Name = "buttonBrowse";
			this.buttonBrowse.Size = new System.Drawing.Size(56, 23);
			this.buttonBrowse.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonBrowse.TabIndex = 76;
			this.buttonBrowse.Text = "Browse";
			this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
			// 
			// buttonBrowseProjectFile
			// 
			this.buttonBrowseProjectFile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonBrowseProjectFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonBrowseProjectFile.CallBasePaintBackground = true;
			this.buttonBrowseProjectFile.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonBrowseProjectFile.ForeColor = System.Drawing.SystemColors.ControlText;
			this.buttonBrowseProjectFile.Location = new System.Drawing.Point(360, 15);
			this.buttonBrowseProjectFile.Name = "buttonBrowseProjectFile";
			this.buttonBrowseProjectFile.Size = new System.Drawing.Size(56, 20);
			this.buttonBrowseProjectFile.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonBrowseProjectFile.TabIndex = 78;
			this.buttonBrowseProjectFile.Text = "Browse";
			this.buttonBrowseProjectFile.Click += new System.EventHandler(this.buttonBrowseProjectFile_Click);
			// 
			// tbProjectLocation
			// 
			this.tbProjectLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbProjectLocation.Location = new System.Drawing.Point(105, 14);
			this.tbProjectLocation.Name = "tbProjectLocation";
			this.tbProjectLocation.Size = new System.Drawing.Size(249, 20);
			this.tbProjectLocation.TabIndex = 77;
			this.tbProjectLocation.TextChanged += new System.EventHandler(this.tbProjectLocation_TextChanged);
			// 
			// labelX16
			// 
			// 
			// 
			// 
			this.labelX16.BackgroundStyle.Class = "";
			this.labelX16.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX16.ForeColor = System.Drawing.Color.White;
			this.labelX16.Location = new System.Drawing.Point(15, 13);
			this.labelX16.Name = "labelX16";
			this.labelX16.Size = new System.Drawing.Size(109, 22);
			this.labelX16.TabIndex = 79;
			this.labelX16.Text = "Existing project";
			// 
			// panelConfigSettings
			// 
			this.panelConfigSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.panelConfigSettings.Controls.Add(this.textBoxConfigFileLocation);
			this.panelConfigSettings.Controls.Add(this.labelConfigFile);
			this.panelConfigSettings.Controls.Add(this.buttonBrowse);
			this.panelConfigSettings.Controls.Add(this.superTabControl1);
			this.panelConfigSettings.Location = new System.Drawing.Point(3, 90);
			this.panelConfigSettings.Name = "panelConfigSettings";
			this.panelConfigSettings.Size = new System.Drawing.Size(505, 385);
			this.panelConfigSettings.TabIndex = 80;
			this.panelConfigSettings.Visible = false;
			// 
			// highlighter1
			// 
			this.highlighter1.ContainerControl = this;
			this.highlighter1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// radioFile
			// 
			this.radioFile.AutoSize = true;
			// 
			// 
			// 
			this.radioFile.BackgroundStyle.Class = "";
			this.radioFile.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.radioFile.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
			this.radioFile.Location = new System.Drawing.Point(108, 41);
			this.radioFile.Name = "radioFile";
			this.radioFile.Size = new System.Drawing.Size(105, 15);
			this.radioFile.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.radioFile.TabIndex = 77;
			this.radioFile.Text = "Locate config file";
			this.radioFile.TextColor = System.Drawing.Color.White;
			this.radioFile.Visible = false;
			this.radioFile.CheckedChanged += new System.EventHandler(this.radioFile_CheckedChanged);
			// 
			// radioManual
			// 
			this.radioManual.AutoSize = true;
			// 
			// 
			// 
			this.radioManual.BackgroundStyle.Class = "";
			this.radioManual.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.radioManual.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
			this.radioManual.Location = new System.Drawing.Point(108, 61);
			this.radioManual.Name = "radioManual";
			this.radioManual.Size = new System.Drawing.Size(147, 15);
			this.radioManual.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.radioManual.TabIndex = 78;
			this.radioManual.Text = "Specify settings manually";
			this.radioManual.TextColor = System.Drawing.Color.White;
			this.radioManual.Visible = false;
			this.radioManual.CheckedChanged += new System.EventHandler(this.radioManual_CheckedChanged);
			// 
			// SetNhConfig
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.Controls.Add(this.radioManual);
			this.Controls.Add(this.panelConfigSettings);
			this.Controls.Add(this.radioFile);
			this.Controls.Add(this.tbProjectLocation);
			this.Controls.Add(this.labelX16);
			this.Controls.Add(this.buttonBrowseProjectFile);
			this.Controls.Add(this.buttonNext);
			this.Controls.Add(this.buttonBack);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "SetNhConfig";
			this.Size = new System.Drawing.Size(511, 519);
			((System.ComponentModel.ISupportInitialize)(this.superTabControl1)).EndInit();
			this.superTabControl1.ResumeLayout(false);
			this.superTabControlPanel1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.superTabControlPanel2.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.superTabItemDatabase.ResumeLayout(false);
			this.panelConfigSettings.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevComponents.DotNetBar.ButtonX buttonNext;
		private DevComponents.DotNetBar.ButtonX buttonBack;
		private DevComponents.DotNetBar.SuperTabControl superTabControl1;
		private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxUseProxyValidator;
		private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxUseOuterJoin;
		private DevComponents.DotNetBar.Controls.TextBoxX textBoxTransactionFactoryClass;
		private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxShowSql;
		private DevComponents.DotNetBar.Controls.TextBoxX textBoxQuerySubstitutions;
		private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxGenerateStatistics;
		private DevComponents.DotNetBar.LabelX labelX1;
		private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxByteCodeGenerator;
		private DevComponents.DotNetBar.LabelX labelX12;
		private DevComponents.DotNetBar.LabelX labelX13;
		private DevComponents.DotNetBar.LabelX labelX7;
		private DevComponents.DotNetBar.LabelX labelX8;
		private DevComponents.DotNetBar.LabelX labelX11;
		private DevComponents.DotNetBar.LabelX labelX9;
		private DevComponents.DotNetBar.LabelX labelX10;
		private DevComponents.DotNetBar.Controls.TextBoxX textBoxMaxFetchDepth;
		private DevComponents.DotNetBar.SuperTabItem superTabItemGeneral;
		private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private DevComponents.DotNetBar.Controls.TextBoxX textBoxCacheRegionPrefix;
		private DevComponents.DotNetBar.Controls.TextBoxX textBoxCacheQueryCacheFactory;
		private DevComponents.DotNetBar.LabelX labelX2;
		private DevComponents.DotNetBar.LabelX labelX6;
		private DevComponents.DotNetBar.LabelX labelX4;
		private DevComponents.DotNetBar.LabelX labelX5;
		private DevComponents.DotNetBar.LabelX labelX3;
		private DevComponents.DotNetBar.Controls.TextBoxX textBoxCacheProviderClass;
		private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxCacheUseMinimalPuts;
		private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxCacheUseQueryCache;
		private DevComponents.DotNetBar.SuperTabItem superTabItemCache;
		private DevComponents.DotNetBar.Controls.TextBoxX textBoxConfigFileLocation;
		private DevComponents.DotNetBar.LabelX labelConfigFile;
		private DevComponents.DotNetBar.ButtonX buttonBrowse;
		private DevComponents.DotNetBar.ButtonX buttonBrowseProjectFile;
		private System.Windows.Forms.TextBox tbProjectLocation;
		private DevComponents.DotNetBar.LabelX labelX16;
		private System.Windows.Forms.Panel panelConfigSettings;
		private DevComponents.DotNetBar.Validator.Highlighter highlighter1;
		private DevComponents.DotNetBar.Controls.CheckBoxX radioManual;
		private DevComponents.DotNetBar.Controls.CheckBoxX radioFile;
		private DevComponents.DotNetBar.SuperTabControlPanel superTabItemDatabase;
		private DevComponents.DotNetBar.SuperTabItem superTabItem1;
		private Providers.EntityModel.UI.PropertyGrids.ucDatabaseInformation ucDatabaseInformation1;
	}
}
