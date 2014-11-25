namespace ArchAngel.Providers.EntityModel.UI.PropertyGrids
{
	partial class ucDatabaseInformation
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDatabaseInformation));
			this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
			this.label1 = new System.Windows.Forms.Label();
			this.labelServer = new System.Windows.Forms.Label();
			this.comboBoxDatabaseTypes = new System.Windows.Forms.ComboBox();
			this.comboBoxServers = new System.Windows.Forms.ComboBox();
			this.checkBoxUseIntegratedSecurity = new System.Windows.Forms.CheckBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxUserName = new System.Windows.Forms.TextBox();
			this.textBoxPassword = new System.Windows.Forms.TextBox();
			this.comboBoxDatabases = new System.Windows.Forms.ComboBox();
			this.optDB = new System.Windows.Forms.RadioButton();
			this.optDBFile = new System.Windows.Forms.RadioButton();
			this.textBoxFilename = new System.Windows.Forms.TextBox();
			this.labelServiceName = new System.Windows.Forms.Label();
			this.labelPort = new System.Windows.Forms.Label();
			this.textBoxPort = new System.Windows.Forms.TextBox();
			this.textBoxServiceName = new System.Windows.Forms.TextBox();
			this.groupBoxLogin = new System.Windows.Forms.GroupBox();
			this.labelTestStatus = new System.Windows.Forms.Label();
			this.checkBoxDirect = new System.Windows.Forms.CheckBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.buttonBrowse = new DevComponents.DotNetBar.ButtonX();
			this.buttonRefreshDatabases = new DevComponents.DotNetBar.ButtonX();
			this.buttonRefreshServers = new DevComponents.DotNetBar.ButtonX();
			this.groupBoxLogin.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// highlighter1
			// 
			this.highlighter1.ContainerControl = this;
			this.highlighter1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(7, 64);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(103, 16);
			this.label1.TabIndex = 51;
			this.label1.Text = "Database type";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelServer
			// 
			this.labelServer.Location = new System.Drawing.Point(58, 100);
			this.labelServer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelServer.Name = "labelServer";
			this.labelServer.Size = new System.Drawing.Size(52, 16);
			this.labelServer.TabIndex = 52;
			this.labelServer.Text = "Server";
			this.labelServer.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// comboBoxDatabaseTypes
			// 
			this.comboBoxDatabaseTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxDatabaseTypes.FormattingEnabled = true;
			this.comboBoxDatabaseTypes.Location = new System.Drawing.Point(118, 64);
			this.comboBoxDatabaseTypes.Margin = new System.Windows.Forms.Padding(4);
			this.comboBoxDatabaseTypes.Name = "comboBoxDatabaseTypes";
			this.comboBoxDatabaseTypes.Size = new System.Drawing.Size(263, 21);
			this.comboBoxDatabaseTypes.TabIndex = 0;
			this.comboBoxDatabaseTypes.SelectedIndexChanged += new System.EventHandler(this.comboBoxDatabaseTypes_SelectedIndexChanged);
			this.comboBoxDatabaseTypes.SelectedValueChanged += new System.EventHandler(this.comboBoxDatabaseTypes_SelectedValueChanged);
			// 
			// comboBoxServers
			// 
			this.comboBoxServers.FormattingEnabled = true;
			this.comboBoxServers.Location = new System.Drawing.Point(118, 97);
			this.comboBoxServers.Margin = new System.Windows.Forms.Padding(4);
			this.comboBoxServers.Name = "comboBoxServers";
			this.comboBoxServers.Size = new System.Drawing.Size(263, 21);
			this.comboBoxServers.TabIndex = 1;
			this.comboBoxServers.SelectedIndexChanged += new System.EventHandler(this.comboBoxServers_SelectedIndexChanged);
			// 
			// checkBoxUseIntegratedSecurity
			// 
			this.checkBoxUseIntegratedSecurity.AutoSize = true;
			this.checkBoxUseIntegratedSecurity.Location = new System.Drawing.Point(21, 23);
			this.checkBoxUseIntegratedSecurity.Margin = new System.Windows.Forms.Padding(4);
			this.checkBoxUseIntegratedSecurity.Name = "checkBoxUseIntegratedSecurity";
			this.checkBoxUseIntegratedSecurity.Size = new System.Drawing.Size(131, 17);
			this.checkBoxUseIntegratedSecurity.TabIndex = 3;
			this.checkBoxUseIntegratedSecurity.Text = "Use Windows security";
			this.checkBoxUseIntegratedSecurity.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(16, 55);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(55, 13);
			this.label4.TabIndex = 57;
			this.label4.Text = "Username";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(16, 85);
			this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(53, 13);
			this.label5.TabIndex = 58;
			this.label5.Text = "Password";
			// 
			// textBoxUserName
			// 
			this.textBoxUserName.Location = new System.Drawing.Point(97, 52);
			this.textBoxUserName.Margin = new System.Windows.Forms.Padding(4);
			this.textBoxUserName.Name = "textBoxUserName";
			this.textBoxUserName.Size = new System.Drawing.Size(132, 20);
			this.textBoxUserName.TabIndex = 4;
			this.textBoxUserName.TextChanged += new System.EventHandler(this.textBoxUserName_TextChanged);
			// 
			// textBoxPassword
			// 
			this.textBoxPassword.Location = new System.Drawing.Point(97, 81);
			this.textBoxPassword.Margin = new System.Windows.Forms.Padding(4);
			this.textBoxPassword.Name = "textBoxPassword";
			this.textBoxPassword.PasswordChar = '*';
			this.textBoxPassword.Size = new System.Drawing.Size(132, 20);
			this.textBoxPassword.TabIndex = 5;
			this.textBoxPassword.TextChanged += new System.EventHandler(this.textBoxPassword_TextChanged);
			// 
			// comboBoxDatabases
			// 
			this.comboBoxDatabases.FormattingEnabled = true;
			this.comboBoxDatabases.Location = new System.Drawing.Point(118, 272);
			this.comboBoxDatabases.Margin = new System.Windows.Forms.Padding(4);
			this.comboBoxDatabases.Name = "comboBoxDatabases";
			this.comboBoxDatabases.Size = new System.Drawing.Size(263, 21);
			this.comboBoxDatabases.TabIndex = 6;
			this.comboBoxDatabases.SelectedIndexChanged += new System.EventHandler(this.comboBoxDatabases_SelectedIndexChanged);
			this.comboBoxDatabases.MouseClick += new System.Windows.Forms.MouseEventHandler(this.comboBoxDatabases_MouseClick);
			// 
			// optDB
			// 
			this.optDB.Location = new System.Drawing.Point(4, 276);
			this.optDB.Margin = new System.Windows.Forms.Padding(4);
			this.optDB.Name = "optDB";
			this.optDB.Size = new System.Drawing.Size(106, 21);
			this.optDB.TabIndex = 63;
			this.optDB.TabStop = true;
			this.optDB.Text = "Database";
			this.optDB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.optDB.UseVisualStyleBackColor = true;
			// 
			// optDBFile
			// 
			this.optDBFile.Location = new System.Drawing.Point(4, 306);
			this.optDBFile.Margin = new System.Windows.Forms.Padding(4);
			this.optDBFile.Name = "optDBFile";
			this.optDBFile.Size = new System.Drawing.Size(106, 21);
			this.optDBFile.TabIndex = 64;
			this.optDBFile.TabStop = true;
			this.optDBFile.Text = "Database file";
			this.optDBFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.optDBFile.UseVisualStyleBackColor = true;
			// 
			// textBoxFilename
			// 
			this.textBoxFilename.Location = new System.Drawing.Point(118, 305);
			this.textBoxFilename.Margin = new System.Windows.Forms.Padding(4);
			this.textBoxFilename.Name = "textBoxFilename";
			this.textBoxFilename.Size = new System.Drawing.Size(263, 20);
			this.textBoxFilename.TabIndex = 7;
			// 
			// labelServiceName
			// 
			this.labelServiceName.Location = new System.Drawing.Point(4, 369);
			this.labelServiceName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelServiceName.Name = "labelServiceName";
			this.labelServiceName.Size = new System.Drawing.Size(106, 16);
			this.labelServiceName.TabIndex = 67;
			this.labelServiceName.Text = "SID";
			this.labelServiceName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelPort
			// 
			this.labelPort.Location = new System.Drawing.Point(4, 337);
			this.labelPort.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelPort.Name = "labelPort";
			this.labelPort.Size = new System.Drawing.Size(106, 16);
			this.labelPort.TabIndex = 68;
			this.labelPort.Text = "Port";
			this.labelPort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.labelPort.Click += new System.EventHandler(this.labelPort_Click);
			// 
			// textBoxPort
			// 
			this.textBoxPort.Location = new System.Drawing.Point(118, 337);
			this.textBoxPort.Margin = new System.Windows.Forms.Padding(4);
			this.textBoxPort.Name = "textBoxPort";
			this.textBoxPort.Size = new System.Drawing.Size(132, 20);
			this.textBoxPort.TabIndex = 8;
			// 
			// textBoxServiceName
			// 
			this.textBoxServiceName.Location = new System.Drawing.Point(118, 369);
			this.textBoxServiceName.Margin = new System.Windows.Forms.Padding(4);
			this.textBoxServiceName.Name = "textBoxServiceName";
			this.textBoxServiceName.Size = new System.Drawing.Size(132, 20);
			this.textBoxServiceName.TabIndex = 9;
			// 
			// groupBoxLogin
			// 
			this.groupBoxLogin.Controls.Add(this.checkBoxUseIntegratedSecurity);
			this.groupBoxLogin.Controls.Add(this.label4);
			this.groupBoxLogin.Controls.Add(this.label5);
			this.groupBoxLogin.Controls.Add(this.textBoxUserName);
			this.groupBoxLogin.Controls.Add(this.textBoxPassword);
			this.groupBoxLogin.ForeColor = System.Drawing.Color.White;
			this.groupBoxLogin.Location = new System.Drawing.Point(118, 130);
			this.groupBoxLogin.Margin = new System.Windows.Forms.Padding(4);
			this.groupBoxLogin.Name = "groupBoxLogin";
			this.groupBoxLogin.Padding = new System.Windows.Forms.Padding(4);
			this.groupBoxLogin.Size = new System.Drawing.Size(265, 123);
			this.groupBoxLogin.TabIndex = 71;
			this.groupBoxLogin.TabStop = false;
			this.groupBoxLogin.Text = "Login";
			// 
			// labelTestStatus
			// 
			this.labelTestStatus.Dock = System.Windows.Forms.DockStyle.Top;
			this.labelTestStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTestStatus.Location = new System.Drawing.Point(0, 0);
			this.labelTestStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelTestStatus.Name = "labelTestStatus";
			this.labelTestStatus.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
			this.labelTestStatus.Size = new System.Drawing.Size(419, 25);
			this.labelTestStatus.TabIndex = 72;
			this.labelTestStatus.Text = "labelDatabaseOperationMessage1";
			this.labelTestStatus.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// checkBoxDirect
			// 
			this.checkBoxDirect.AutoSize = true;
			this.checkBoxDirect.Location = new System.Drawing.Point(4, 100);
			this.checkBoxDirect.Margin = new System.Windows.Forms.Padding(4);
			this.checkBoxDirect.Name = "checkBoxDirect";
			this.checkBoxDirect.Size = new System.Drawing.Size(54, 17);
			this.checkBoxDirect.TabIndex = 2;
			this.checkBoxDirect.Text = "Direct";
			this.checkBoxDirect.UseVisualStyleBackColor = true;
			this.checkBoxDirect.CheckedChanged += new System.EventHandler(this.checkBoxDirect_CheckedChanged);
			// 
			// panel1
			// 
			this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.panel1.BackColor = System.Drawing.Color.Transparent;
			this.panel1.Controls.Add(this.buttonBrowse);
			this.panel1.Controls.Add(this.buttonRefreshDatabases);
			this.panel1.Controls.Add(this.buttonRefreshServers);
			this.panel1.Controls.Add(this.checkBoxDirect);
			this.panel1.Controls.Add(this.comboBoxDatabaseTypes);
			this.panel1.Controls.Add(this.labelTestStatus);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.groupBoxLogin);
			this.panel1.Controls.Add(this.labelServer);
			this.panel1.Controls.Add(this.comboBoxServers);
			this.panel1.Controls.Add(this.comboBoxDatabases);
			this.panel1.Controls.Add(this.textBoxServiceName);
			this.panel1.Controls.Add(this.textBoxPort);
			this.panel1.Controls.Add(this.optDB);
			this.panel1.Controls.Add(this.labelPort);
			this.panel1.Controls.Add(this.optDBFile);
			this.panel1.Controls.Add(this.labelServiceName);
			this.panel1.Controls.Add(this.textBoxFilename);
			this.panel1.Location = new System.Drawing.Point(14, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(419, 436);
			this.panel1.TabIndex = 73;
			// 
			// buttonBrowse
			// 
			this.buttonBrowse.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonBrowse.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonBrowse.HoverImage = ((System.Drawing.Image)(resources.GetObject("buttonBrowse.HoverImage")));
			this.buttonBrowse.Image = ((System.Drawing.Image)(resources.GetObject("buttonBrowse.Image")));
			this.buttonBrowse.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
			this.buttonBrowse.Location = new System.Drawing.Point(389, 305);
			this.buttonBrowse.Name = "buttonBrowse";
			this.buttonBrowse.Size = new System.Drawing.Size(24, 24);
			this.buttonBrowse.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonBrowse.TabIndex = 12;
			this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
			// 
			// buttonRefreshDatabases
			// 
			this.buttonRefreshDatabases.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonRefreshDatabases.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonRefreshDatabases.HoverImage = ((System.Drawing.Image)(resources.GetObject("buttonRefreshDatabases.HoverImage")));
			this.buttonRefreshDatabases.Image = ((System.Drawing.Image)(resources.GetObject("buttonRefreshDatabases.Image")));
			this.buttonRefreshDatabases.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
			this.buttonRefreshDatabases.Location = new System.Drawing.Point(389, 272);
			this.buttonRefreshDatabases.Name = "buttonRefreshDatabases";
			this.buttonRefreshDatabases.Size = new System.Drawing.Size(24, 24);
			this.buttonRefreshDatabases.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonRefreshDatabases.TabIndex = 11;
			this.buttonRefreshDatabases.Click += new System.EventHandler(this.buttonRefreshDatabases_Click);
			// 
			// buttonRefreshServers
			// 
			this.buttonRefreshServers.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonRefreshServers.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonRefreshServers.HoverImage = ((System.Drawing.Image)(resources.GetObject("buttonRefreshServers.HoverImage")));
			this.buttonRefreshServers.Image = ((System.Drawing.Image)(resources.GetObject("buttonRefreshServers.Image")));
			this.buttonRefreshServers.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
			this.buttonRefreshServers.Location = new System.Drawing.Point(389, 97);
			this.buttonRefreshServers.Name = "buttonRefreshServers";
			this.buttonRefreshServers.Size = new System.Drawing.Size(24, 24);
			this.buttonRefreshServers.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonRefreshServers.TabIndex = 10;
			this.buttonRefreshServers.Click += new System.EventHandler(this.buttonRefreshServers_Click);
			// 
			// ucDatabaseInformation
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.Black;
			this.Controls.Add(this.panel1);
			this.ForeColor = System.Drawing.Color.White;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "ucDatabaseInformation";
			this.Size = new System.Drawing.Size(446, 467);
			this.Load += new System.EventHandler(this.ucDatabaseInformation_Load);
			this.groupBoxLogin.ResumeLayout(false);
			this.groupBoxLogin.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.Validator.Highlighter highlighter1;
		private System.Windows.Forms.ComboBox comboBoxDatabaseTypes;
		private System.Windows.Forms.Label labelServer;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboBoxServers;
		private System.Windows.Forms.CheckBox checkBoxUseIntegratedSecurity;
		private System.Windows.Forms.Label labelPort;
		private System.Windows.Forms.Label labelServiceName;
		private System.Windows.Forms.TextBox textBoxFilename;
		private System.Windows.Forms.RadioButton optDBFile;
		private System.Windows.Forms.RadioButton optDB;
		private System.Windows.Forms.ComboBox comboBoxDatabases;
		private System.Windows.Forms.TextBox textBoxPassword;
		private System.Windows.Forms.TextBox textBoxUserName;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox groupBoxLogin;
		private System.Windows.Forms.TextBox textBoxServiceName;
		private System.Windows.Forms.TextBox textBoxPort;
		private System.Windows.Forms.Label labelTestStatus;
		private System.Windows.Forms.CheckBox checkBoxDirect;
		private System.Windows.Forms.Panel panel1;
		private DevComponents.DotNetBar.ButtonX buttonRefreshServers;
		private DevComponents.DotNetBar.ButtonX buttonRefreshDatabases;
		private DevComponents.DotNetBar.ButtonX buttonBrowse;
	}
}
