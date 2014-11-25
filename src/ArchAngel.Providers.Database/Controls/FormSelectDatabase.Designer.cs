namespace ArchAngel.Providers.Database.Controls
{
    partial class FormSelectDatabase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSelectDatabase));
            this.buttonRefreshDatabases = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.buttonRefreshServers = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.buttonTestConnection = new System.Windows.Forms.Button();
            this.ucHeading1 = new Slyce.Common.Controls.ucHeading();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.comboBoxDatabaseTypes = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.optDB = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.optDBFile = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.labelTestStatus = new DevComponents.DotNetBar.LabelX();
            this.comboBoxDatabases = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.textBoxFilename = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.tableLayoutPanel1 = new Slyce.Common.Controls.TableLayoutPanelEx(this.components);
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.labelUsernameError = new DevComponents.DotNetBar.LabelX();
            this.textBoxPassword = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.textBoxUserName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelPassword = new DevComponents.DotNetBar.LabelX();
            this.labelUserName = new DevComponents.DotNetBar.LabelX();
            this.checkBoxUseIntegratedSecurity = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.comboBoxServers = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelDatabase = new DevComponents.DotNetBar.LabelX();
            this.panelEx1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonRefreshDatabases
            // 
            this.buttonRefreshDatabases.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonRefreshDatabases.Image = ((System.Drawing.Image)(resources.GetObject("buttonRefreshDatabases.Image")));
            this.buttonRefreshDatabases.Location = new System.Drawing.Point(287, 243);
            this.buttonRefreshDatabases.Name = "buttonRefreshDatabases";
            this.buttonRefreshDatabases.Size = new System.Drawing.Size(24, 24);
            this.buttonRefreshDatabases.TabIndex = 20;
            this.toolTip1.SetToolTip(this.buttonRefreshDatabases, "Refresh");
            this.buttonRefreshDatabases.UseVisualStyleBackColor = true;
            this.buttonRefreshDatabases.Click += new System.EventHandler(this.buttonRefreshSqlSmoDatabase_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(256, 408);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(63, 23);
            this.btnCancel.TabIndex = 23;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(187, 408);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(63, 23);
            this.buttonSave.TabIndex = 24;
            this.buttonSave.Text = "OK";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonBrowse.Image = ((System.Drawing.Image)(resources.GetObject("buttonBrowse.Image")));
            this.buttonBrowse.Location = new System.Drawing.Point(287, 273);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(24, 19);
            this.buttonBrowse.TabIndex = 31;
            this.toolTip1.SetToolTip(this.buttonBrowse, "Refresh");
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // buttonRefreshServers
            // 
            this.buttonRefreshServers.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonRefreshServers.Image = ((System.Drawing.Image)(resources.GetObject("buttonRefreshServers.Image")));
            this.buttonRefreshServers.Location = new System.Drawing.Point(287, 80);
            this.buttonRefreshServers.Name = "buttonRefreshServers";
            this.buttonRefreshServers.Size = new System.Drawing.Size(24, 24);
            this.buttonRefreshServers.TabIndex = 17;
            this.toolTip1.SetToolTip(this.buttonRefreshServers, "Refresh");
            this.buttonRefreshServers.UseVisualStyleBackColor = true;
            this.buttonRefreshServers.Click += new System.EventHandler(this.buttonRefreshServers_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // buttonTestConnection
            // 
            this.buttonTestConnection.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonTestConnection.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.buttonTestConnection, 3);
            this.buttonTestConnection.Location = new System.Drawing.Point(93, 358);
            this.buttonTestConnection.Name = "buttonTestConnection";
            this.buttonTestConnection.Size = new System.Drawing.Size(141, 23);
            this.buttonTestConnection.TabIndex = 34;
            this.buttonTestConnection.Text = "&Test Connection";
            this.buttonTestConnection.UseVisualStyleBackColor = true;
            this.buttonTestConnection.Click += new System.EventHandler(this.buttonTestConnection_Click);
            // 
            // ucHeading1
            // 
            this.ucHeading1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucHeading1.Location = new System.Drawing.Point(0, 402);
            this.ucHeading1.Margin = new System.Windows.Forms.Padding(2);
            this.ucHeading1.Name = "ucHeading1";
            this.ucHeading1.Size = new System.Drawing.Size(328, 34);
            this.ucHeading1.TabIndex = 36;
            this.ucHeading1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelX1
            // 
            this.labelX1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX1.AutoSize = true;
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            this.labelX1.Location = new System.Drawing.Point(20, 54);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(77, 15);
            this.labelX1.TabIndex = 38;
            this.labelX1.Text = "Database Type";
            // 
            // labelX3
            // 
            this.labelX3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelX3.AutoSize = true;
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.SetColumnSpan(this.labelX3, 3);
            this.labelX3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX3.Location = new System.Drawing.Point(42, 13);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(243, 15);
            this.labelX3.TabIndex = 40;
            this.labelX3.Text = "Select the database you want to generate from.";
            // 
            // comboBoxDatabaseTypes
            // 
            this.comboBoxDatabaseTypes.DisplayMember = "Text";
            this.comboBoxDatabaseTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxDatabaseTypes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxDatabaseTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDatabaseTypes.FormattingEnabled = true;
            this.comboBoxDatabaseTypes.ItemHeight = 14;
            this.comboBoxDatabaseTypes.Location = new System.Drawing.Point(103, 54);
            this.comboBoxDatabaseTypes.Name = "comboBoxDatabaseTypes";
            this.comboBoxDatabaseTypes.Size = new System.Drawing.Size(178, 20);
            this.comboBoxDatabaseTypes.TabIndex = 41;
            this.comboBoxDatabaseTypes.SelectedIndexChanged += new System.EventHandler(this.comboBoxDatabaseTypes_SelectedIndexChanged);
            // 
            // optDB
            // 
            this.optDB.AutoSize = true;
            this.optDB.BackColor = System.Drawing.Color.Transparent;
            this.optDB.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.optDB.Location = new System.Drawing.Point(3, 243);
            this.optDB.Name = "optDB";
            this.optDB.Size = new System.Drawing.Size(69, 15);
            this.optDB.TabIndex = 43;
            this.optDB.Text = "Database";
            this.optDB.CheckedChanged += new System.EventHandler(this.optDB_CheckedChanged);
            // 
            // optDBFile
            // 
            this.optDBFile.AutoSize = true;
            this.optDBFile.BackColor = System.Drawing.Color.Transparent;
            this.optDBFile.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.optDBFile.Location = new System.Drawing.Point(3, 273);
            this.optDBFile.Name = "optDBFile";
            this.optDBFile.Size = new System.Drawing.Size(89, 15);
            this.optDBFile.TabIndex = 44;
            this.optDBFile.Text = "Database File";
            this.optDBFile.CheckedChanged += new System.EventHandler(this.optDBFile_CheckedChanged);
            // 
            // labelTestStatus
            // 
            this.labelTestStatus.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.SetColumnSpan(this.labelTestStatus, 3);
            this.labelTestStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTestStatus.ForeColor = System.Drawing.Color.Red;
            this.labelTestStatus.Location = new System.Drawing.Point(3, 318);
            this.labelTestStatus.Name = "labelTestStatus";
            this.labelTestStatus.Size = new System.Drawing.Size(314, 14);
            this.labelTestStatus.TabIndex = 45;
            this.labelTestStatus.Text = "ddd\r\nggg";
            this.labelTestStatus.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // comboBoxDatabases
            // 
            this.comboBoxDatabases.DisplayMember = "Text";
            this.comboBoxDatabases.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxDatabases.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxDatabases.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDatabases.FormattingEnabled = true;
            this.comboBoxDatabases.ItemHeight = 14;
            this.comboBoxDatabases.Location = new System.Drawing.Point(103, 243);
            this.comboBoxDatabases.Name = "comboBoxDatabases";
            this.comboBoxDatabases.Size = new System.Drawing.Size(178, 20);
            this.comboBoxDatabases.TabIndex = 46;
            this.comboBoxDatabases.SelectedIndexChanged += new System.EventHandler(this.comboBoxDatabases_SelectedIndexChanged);
            // 
            // textBoxFilename
            // 
            // 
            // 
            // 
            this.textBoxFilename.Border.Class = "TextBoxBorder";
            this.textBoxFilename.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxFilename.Location = new System.Drawing.Point(103, 273);
            this.textBoxFilename.Name = "textBoxFilename";
            this.textBoxFilename.Size = new System.Drawing.Size(178, 20);
            this.textBoxFilename.TabIndex = 47;
            this.textBoxFilename.TextChanged += new System.EventHandler(this.textBoxFilename_TextChanged);
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panelEx1.Controls.Add(this.tableLayoutPanel1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(328, 436);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 48;
            this.panelEx1.Text = "panelEx1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.labelX3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupPanel1, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.labelTestStatus, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.buttonRefreshDatabases, 2, 7);
            this.tableLayoutPanel1.Controls.Add(this.buttonBrowse, 2, 8);
            this.tableLayoutPanel1.Controls.Add(this.textBoxFilename, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.labelX1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelX2, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxDatabaseTypes, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxServers, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.optDBFile, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxDatabases, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.buttonRefreshServers, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.buttonTestConnection, 1, 11);
            this.tableLayoutPanel1.Controls.Add(this.optDB, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.labelDatabase, 0, 9);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 12;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(328, 436);
            this.tableLayoutPanel1.TabIndex = 48;
            // 
            // groupPanel1
            // 
            this.groupPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.tableLayoutPanel1.SetColumnSpan(this.groupPanel1, 3);
            this.groupPanel1.Controls.Add(this.labelUsernameError);
            this.groupPanel1.Controls.Add(this.textBoxPassword);
            this.groupPanel1.Controls.Add(this.textBoxUserName);
            this.groupPanel1.Controls.Add(this.labelPassword);
            this.groupPanel1.Controls.Add(this.labelUserName);
            this.groupPanel1.Controls.Add(this.checkBoxUseIntegratedSecurity);
            this.groupPanel1.Location = new System.Drawing.Point(9, 130);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(309, 107);
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
            this.groupPanel1.TabIndex = 37;
            this.groupPanel1.Text = "Logon Credentials";
            // 
            // labelUsernameError
            // 
            this.labelUsernameError.AutoSize = true;
            this.labelUsernameError.BackColor = System.Drawing.Color.Transparent;
            this.labelUsernameError.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUsernameError.ForeColor = System.Drawing.Color.Red;
            this.labelUsernameError.Location = new System.Drawing.Point(267, 30);
            this.labelUsernameError.Name = "labelUsernameError";
            this.labelUsernameError.Size = new System.Drawing.Size(27, 15);
            this.labelUsernameError.TabIndex = 33;
            this.labelUsernameError.Text = "* req";
            this.labelUsernameError.Visible = false;
            // 
            // textBoxPassword
            // 
            // 
            // 
            // 
            this.textBoxPassword.Border.Class = "TextBoxBorder";
            this.textBoxPassword.Enabled = false;
            this.textBoxPassword.Location = new System.Drawing.Point(88, 58);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(173, 20);
            this.textBoxPassword.TabIndex = 32;
            this.textBoxPassword.TextChanged += new System.EventHandler(this.textBoxPassword_TextChanged);
            // 
            // textBoxUserName
            // 
            // 
            // 
            // 
            this.textBoxUserName.Border.Class = "TextBoxBorder";
            this.textBoxUserName.Enabled = false;
            this.textBoxUserName.Location = new System.Drawing.Point(88, 29);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(173, 20);
            this.textBoxUserName.TabIndex = 31;
            this.textBoxUserName.Text = "sa";
            this.textBoxUserName.Validated += new System.EventHandler(this.textBoxUserName_Validated);
            this.textBoxUserName.TextChanged += new System.EventHandler(this.textBoxUserName_TextChanged);
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.BackColor = System.Drawing.Color.Transparent;
            this.labelPassword.Location = new System.Drawing.Point(7, 59);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(51, 15);
            this.labelPassword.TabIndex = 30;
            this.labelPassword.Text = "Password";
            // 
            // labelUserName
            // 
            this.labelUserName.AutoSize = true;
            this.labelUserName.BackColor = System.Drawing.Color.Transparent;
            this.labelUserName.Location = new System.Drawing.Point(7, 30);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new System.Drawing.Size(53, 15);
            this.labelUserName.TabIndex = 29;
            this.labelUserName.Text = "Username";
            // 
            // checkBoxUseIntegratedSecurity
            // 
            this.checkBoxUseIntegratedSecurity.AutoSize = true;
            this.checkBoxUseIntegratedSecurity.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxUseIntegratedSecurity.Location = new System.Drawing.Point(7, 3);
            this.checkBoxUseIntegratedSecurity.Name = "checkBoxUseIntegratedSecurity";
            this.checkBoxUseIntegratedSecurity.Size = new System.Drawing.Size(130, 15);
            this.checkBoxUseIntegratedSecurity.TabIndex = 28;
            this.checkBoxUseIntegratedSecurity.Text = "Use Windows security";
            this.checkBoxUseIntegratedSecurity.CheckedChanged += new System.EventHandler(this.checkBoxUseIntegratedSecurity_CheckedChanged);
            // 
            // labelX2
            // 
            this.labelX2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX2.AutoSize = true;
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            this.labelX2.Location = new System.Drawing.Point(62, 80);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(35, 15);
            this.labelX2.TabIndex = 39;
            this.labelX2.Text = "Server";
            // 
            // comboBoxServers
            // 
            this.comboBoxServers.DisplayMember = "Text";
            this.comboBoxServers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxServers.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxServers.FormattingEnabled = true;
            this.comboBoxServers.ItemHeight = 14;
            this.comboBoxServers.Location = new System.Drawing.Point(103, 80);
            this.comboBoxServers.Name = "comboBoxServers";
            this.comboBoxServers.Size = new System.Drawing.Size(178, 20);
            this.comboBoxServers.TabIndex = 42;
            this.comboBoxServers.SelectedIndexChanged += new System.EventHandler(this.comboBoxServers_SelectedIndexChanged);
            // 
            // labelDatabase
            // 
            this.labelDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDatabase.AutoSize = true;
            this.labelDatabase.BackColor = System.Drawing.Color.Transparent;
            this.labelDatabase.Location = new System.Drawing.Point(47, 298);
            this.labelDatabase.Name = "labelDatabase";
            this.labelDatabase.Size = new System.Drawing.Size(50, 15);
            this.labelDatabase.TabIndex = 48;
            this.labelDatabase.Text = "Database";
            this.labelDatabase.Visible = false;
            // 
            // FormSelectDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(219)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(328, 436);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.ucHeading1);
            this.Controls.Add(this.panelEx1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(336, 470);
            this.Name = "FormSelectDatabase";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Database";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormSelectDatabase_FormClosed);
            this.panelEx1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonRefreshDatabases;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button buttonTestConnection;
        private Slyce.Common.Controls.ucHeading ucHeading1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxDatabaseTypes;
        private DevComponents.DotNetBar.Controls.CheckBoxX optDB;
        private DevComponents.DotNetBar.Controls.CheckBoxX optDBFile;
        private DevComponents.DotNetBar.LabelX labelTestStatus;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxDatabases;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxFilename;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        //private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Slyce.Common.Controls.TableLayoutPanelEx tableLayoutPanel1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxServers;
        private System.Windows.Forms.Button buttonRefreshServers;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.LabelX labelUsernameError;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxPassword;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxUserName;
        private DevComponents.DotNetBar.LabelX labelPassword;
        private DevComponents.DotNetBar.LabelX labelUserName;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxUseIntegratedSecurity;
        private DevComponents.DotNetBar.LabelX labelDatabase;
    }
}