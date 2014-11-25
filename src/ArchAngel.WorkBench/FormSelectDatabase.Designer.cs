namespace ArchAngel.Workbench
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
            this.buttonRefreshServers = new System.Windows.Forms.Button();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.comboBoxDatabases = new System.Windows.Forms.ComboBox();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.labelUserName = new System.Windows.Forms.Label();
            this.comboBoxServers = new System.Windows.Forms.ComboBox();
            this.labelServers = new System.Windows.Forms.Label();
            this.labelDatabases = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxDatabaseTypes = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.labelUsernameError = new System.Windows.Forms.Label();
            this.checkBoxUseIntegratedSecurity = new System.Windows.Forms.CheckBox();
            this.groupBoxLogonDetails = new System.Windows.Forms.GroupBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.labelFilename = new System.Windows.Forms.Label();
            this.textBoxFilename = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.optDB = new System.Windows.Forms.RadioButton();
            this.optDBFile = new System.Windows.Forms.RadioButton();
            this.buttonTestConnection = new System.Windows.Forms.Button();
            this.labelTestStatus = new System.Windows.Forms.Label();
            this.ucHeading1 = new Slyce.Common.Controls.ucHeading();
            this.groupBoxLogonDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonRefreshDatabases
            // 
            this.buttonRefreshDatabases.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRefreshDatabases.Image = ((System.Drawing.Image)(resources.GetObject("buttonRefreshDatabases.Image")));
            this.buttonRefreshDatabases.Location = new System.Drawing.Point(285, 223);
            this.buttonRefreshDatabases.Name = "buttonRefreshDatabases";
            this.buttonRefreshDatabases.Size = new System.Drawing.Size(24, 24);
            this.buttonRefreshDatabases.TabIndex = 20;
            this.toolTip1.SetToolTip(this.buttonRefreshDatabases, "Refresh");
            this.buttonRefreshDatabases.UseVisualStyleBackColor = true;
            this.buttonRefreshDatabases.Click += new System.EventHandler(this.buttonRefreshSqlSmoDatabase_Click);
            // 
            // buttonRefreshServers
            // 
            this.buttonRefreshServers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRefreshServers.Image = ((System.Drawing.Image)(resources.GetObject("buttonRefreshServers.Image")));
            this.buttonRefreshServers.Location = new System.Drawing.Point(285, 74);
            this.buttonRefreshServers.Name = "buttonRefreshServers";
            this.buttonRefreshServers.Size = new System.Drawing.Size(24, 24);
            this.buttonRefreshServers.TabIndex = 17;
            this.toolTip1.SetToolTip(this.buttonRefreshServers, "Refresh");
            this.buttonRefreshServers.UseVisualStyleBackColor = true;
            this.buttonRefreshServers.Click += new System.EventHandler(this.buttonRefreshServers_Click);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPassword.Location = new System.Drawing.Point(89, 71);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(178, 20);
            this.textBoxPassword.TabIndex = 3;
            this.textBoxPassword.TextChanged += new System.EventHandler(this.textBoxPassword_TextChanged);
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.BackColor = System.Drawing.Color.Transparent;
            this.labelPassword.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPassword.Location = new System.Drawing.Point(8, 74);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(53, 13);
            this.labelPassword.TabIndex = 13;
            this.labelPassword.Text = "Password";
            // 
            // comboBoxDatabases
            // 
            this.comboBoxDatabases.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDatabases.FormattingEnabled = true;
            this.comboBoxDatabases.Location = new System.Drawing.Point(101, 223);
            this.comboBoxDatabases.Name = "comboBoxDatabases";
            this.comboBoxDatabases.Size = new System.Drawing.Size(178, 21);
            this.comboBoxDatabases.TabIndex = 4;
            this.comboBoxDatabases.SelectedIndexChanged += new System.EventHandler(this.comboBoxDatabases_SelectedIndexChanged);
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxUserName.Location = new System.Drawing.Point(89, 44);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(178, 20);
            this.textBoxUserName.TabIndex = 2;
            this.textBoxUserName.Text = "sa";
            this.textBoxUserName.Validated += new System.EventHandler(this.textBoxUserName_Validated);
            this.textBoxUserName.TextChanged += new System.EventHandler(this.textBoxUserName_TextChanged);
            // 
            // labelUserName
            // 
            this.labelUserName.AutoSize = true;
            this.labelUserName.BackColor = System.Drawing.Color.Transparent;
            this.labelUserName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUserName.Location = new System.Drawing.Point(8, 47);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new System.Drawing.Size(59, 13);
            this.labelUserName.TabIndex = 11;
            this.labelUserName.Text = "User Name";
            // 
            // comboBoxServers
            // 
            this.comboBoxServers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxServers.FormattingEnabled = true;
            this.comboBoxServers.Location = new System.Drawing.Point(101, 74);
            this.comboBoxServers.Name = "comboBoxServers";
            this.comboBoxServers.Size = new System.Drawing.Size(178, 21);
            this.comboBoxServers.TabIndex = 1;
            this.comboBoxServers.SelectedIndexChanged += new System.EventHandler(this.comboBoxServers_SelectedIndexChanged);
            // 
            // labelServers
            // 
            this.labelServers.AutoSize = true;
            this.labelServers.BackColor = System.Drawing.Color.Transparent;
            this.labelServers.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelServers.Location = new System.Drawing.Point(20, 77);
            this.labelServers.Name = "labelServers";
            this.labelServers.Size = new System.Drawing.Size(39, 13);
            this.labelServers.TabIndex = 15;
            this.labelServers.Text = "Server";
            // 
            // labelDatabases
            // 
            this.labelDatabases.AutoSize = true;
            this.labelDatabases.BackColor = System.Drawing.Color.Transparent;
            this.labelDatabases.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDatabases.Location = new System.Drawing.Point(26, 226);
            this.labelDatabases.Name = "labelDatabases";
            this.labelDatabases.Size = new System.Drawing.Size(53, 13);
            this.labelDatabases.TabIndex = 18;
            this.labelDatabases.Text = "Database";
            this.labelDatabases.Click += new System.EventHandler(this.labelDatabases_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Database Type";
            // 
            // comboBoxDatabaseTypes
            // 
            this.comboBoxDatabaseTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDatabaseTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDatabaseTypes.FormattingEnabled = true;
            this.comboBoxDatabaseTypes.Items.AddRange(new object[] {
            "SQL Server"});
            this.comboBoxDatabaseTypes.Location = new System.Drawing.Point(100, 47);
            this.comboBoxDatabaseTypes.Name = "comboBoxDatabaseTypes";
            this.comboBoxDatabaseTypes.Size = new System.Drawing.Size(178, 21);
            this.comboBoxDatabaseTypes.TabIndex = 0;
            this.comboBoxDatabaseTypes.SelectedIndexChanged += new System.EventHandler(this.comboBoxDatabaseTypes_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(260, 346);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(63, 23);
            this.btnCancel.TabIndex = 23;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(191, 346);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(63, 23);
            this.buttonSave.TabIndex = 24;
            this.buttonSave.Text = "OK";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(309, 30);
            this.label2.TabIndex = 25;
            this.label2.Text = "Select the database you want to generate from.";
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowse.Image = ((System.Drawing.Image)(resources.GetObject("buttonBrowse.Image")));
            this.buttonBrowse.Location = new System.Drawing.Point(284, 250);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(24, 24);
            this.buttonBrowse.TabIndex = 31;
            this.toolTip1.SetToolTip(this.buttonBrowse, "Refresh");
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // labelUsernameError
            // 
            this.labelUsernameError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelUsernameError.AutoSize = true;
            this.labelUsernameError.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUsernameError.ForeColor = System.Drawing.Color.Red;
            this.labelUsernameError.Location = new System.Drawing.Point(271, 46);
            this.labelUsernameError.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelUsernameError.Name = "labelUsernameError";
            this.labelUsernameError.Size = new System.Drawing.Size(34, 13);
            this.labelUsernameError.TabIndex = 26;
            this.labelUsernameError.Text = "* req";
            this.labelUsernameError.Visible = false;
            // 
            // checkBoxUseIntegratedSecurity
            // 
            this.checkBoxUseIntegratedSecurity.AutoSize = true;
            this.checkBoxUseIntegratedSecurity.Checked = true;
            this.checkBoxUseIntegratedSecurity.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUseIntegratedSecurity.Location = new System.Drawing.Point(10, 21);
            this.checkBoxUseIntegratedSecurity.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxUseIntegratedSecurity.Name = "checkBoxUseIntegratedSecurity";
            this.checkBoxUseIntegratedSecurity.Size = new System.Drawing.Size(131, 17);
            this.checkBoxUseIntegratedSecurity.TabIndex = 27;
            this.checkBoxUseIntegratedSecurity.Text = "Use Windows security";
            this.checkBoxUseIntegratedSecurity.UseVisualStyleBackColor = true;
            this.checkBoxUseIntegratedSecurity.CheckedChanged += new System.EventHandler(this.checkBoxUseIntegratedSecurity_CheckedChanged);
            // 
            // groupBoxLogonDetails
            // 
            this.groupBoxLogonDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxLogonDetails.Controls.Add(this.textBoxUserName);
            this.groupBoxLogonDetails.Controls.Add(this.checkBoxUseIntegratedSecurity);
            this.groupBoxLogonDetails.Controls.Add(this.labelUserName);
            this.groupBoxLogonDetails.Controls.Add(this.labelUsernameError);
            this.groupBoxLogonDetails.Controls.Add(this.labelPassword);
            this.groupBoxLogonDetails.Controls.Add(this.textBoxPassword);
            this.groupBoxLogonDetails.Location = new System.Drawing.Point(12, 108);
            this.groupBoxLogonDetails.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxLogonDetails.Name = "groupBoxLogonDetails";
            this.groupBoxLogonDetails.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxLogonDetails.Size = new System.Drawing.Size(310, 98);
            this.groupBoxLogonDetails.TabIndex = 28;
            this.groupBoxLogonDetails.TabStop = false;
            this.groupBoxLogonDetails.Text = "Logon Credentials";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // labelFilename
            // 
            this.labelFilename.AutoSize = true;
            this.labelFilename.BackColor = System.Drawing.Color.Transparent;
            this.labelFilename.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFilename.Location = new System.Drawing.Point(25, 253);
            this.labelFilename.Name = "labelFilename";
            this.labelFilename.Size = new System.Drawing.Size(72, 13);
            this.labelFilename.TabIndex = 30;
            this.labelFilename.Text = "Database File";
            this.labelFilename.Click += new System.EventHandler(this.labelFilename_Click);
            // 
            // textBoxFilename
            // 
            this.textBoxFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFilename.Location = new System.Drawing.Point(100, 250);
            this.textBoxFilename.Name = "textBoxFilename";
            this.textBoxFilename.Size = new System.Drawing.Size(178, 20);
            this.textBoxFilename.TabIndex = 29;
            this.textBoxFilename.TextChanged += new System.EventHandler(this.textBoxFilename_TextChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // optDB
            // 
            this.optDB.AutoSize = true;
            this.optDB.Checked = true;
            this.optDB.Location = new System.Drawing.Point(9, 226);
            this.optDB.Margin = new System.Windows.Forms.Padding(2);
            this.optDB.Name = "optDB";
            this.optDB.Size = new System.Drawing.Size(14, 13);
            this.optDB.TabIndex = 32;
            this.optDB.TabStop = true;
            this.optDB.UseVisualStyleBackColor = true;
            this.optDB.CheckedChanged += new System.EventHandler(this.optDB_CheckedChanged);
            // 
            // optDBFile
            // 
            this.optDBFile.AutoSize = true;
            this.optDBFile.Location = new System.Drawing.Point(8, 255);
            this.optDBFile.Margin = new System.Windows.Forms.Padding(2);
            this.optDBFile.Name = "optDBFile";
            this.optDBFile.Size = new System.Drawing.Size(14, 13);
            this.optDBFile.TabIndex = 33;
            this.optDBFile.UseVisualStyleBackColor = true;
            this.optDBFile.CheckedChanged += new System.EventHandler(this.optDBFile_CheckedChanged);
            // 
            // buttonTestConnection
            // 
            this.buttonTestConnection.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonTestConnection.Location = new System.Drawing.Point(112, 313);
            this.buttonTestConnection.Name = "buttonTestConnection";
            this.buttonTestConnection.Size = new System.Drawing.Size(100, 23);
            this.buttonTestConnection.TabIndex = 34;
            this.buttonTestConnection.Text = "&Test Connection";
            this.buttonTestConnection.UseVisualStyleBackColor = true;
            this.buttonTestConnection.Click += new System.EventHandler(this.buttonTestConnection_Click);
            // 
            // labelTestStatus
            // 
            this.labelTestStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTestStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTestStatus.ForeColor = System.Drawing.Color.Red;
            this.labelTestStatus.Location = new System.Drawing.Point(6, 273);
            this.labelTestStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTestStatus.Name = "labelTestStatus";
            this.labelTestStatus.Size = new System.Drawing.Size(317, 37);
            this.labelTestStatus.TabIndex = 35;
            this.labelTestStatus.Text = "hhh\r\nggg";
            this.labelTestStatus.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // ucHeading1
            // 
            this.ucHeading1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucHeading1.Location = new System.Drawing.Point(0, 340);
            this.ucHeading1.Margin = new System.Windows.Forms.Padding(2);
            this.ucHeading1.Name = "ucHeading1";
            this.ucHeading1.Size = new System.Drawing.Size(328, 34);
            this.ucHeading1.TabIndex = 36;
            // 
            // FormSelectDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(219)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(328, 374);
            this.Controls.Add(this.labelTestStatus);
            this.Controls.Add(this.buttonTestConnection);
            this.Controls.Add(this.optDBFile);
            this.Controls.Add(this.optDB);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.labelFilename);
            this.Controls.Add(this.textBoxFilename);
            this.Controls.Add(this.groupBoxLogonDetails);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.comboBoxDatabaseTypes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonRefreshDatabases);
            this.Controls.Add(this.buttonRefreshServers);
            this.Controls.Add(this.comboBoxDatabases);
            this.Controls.Add(this.comboBoxServers);
            this.Controls.Add(this.labelServers);
            this.Controls.Add(this.labelDatabases);
            this.Controls.Add(this.ucHeading1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(336, 408);
            this.Name = "FormSelectDatabase";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Database";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormSelectDatabase_FormClosed);
            this.Load += new System.EventHandler(this.FormSelectDatabase_Load);
            this.groupBoxLogonDetails.ResumeLayout(false);
            this.groupBoxLogonDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonRefreshDatabases;
        private System.Windows.Forms.Button buttonRefreshServers;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.ComboBox comboBoxDatabases;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.Label labelUserName;
        private System.Windows.Forms.ComboBox comboBoxServers;
        private System.Windows.Forms.Label labelServers;
        private System.Windows.Forms.Label labelDatabases;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxDatabaseTypes;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label labelUsernameError;
        private System.Windows.Forms.CheckBox checkBoxUseIntegratedSecurity;
        private System.Windows.Forms.GroupBox groupBoxLogonDetails;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label labelFilename;
        private System.Windows.Forms.TextBox textBoxFilename;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.RadioButton optDB;
        private System.Windows.Forms.RadioButton optDBFile;
        private System.Windows.Forms.Button buttonTestConnection;
        private System.Windows.Forms.Label labelTestStatus;
        private Slyce.Common.Controls.ucHeading ucHeading1;
    }
}