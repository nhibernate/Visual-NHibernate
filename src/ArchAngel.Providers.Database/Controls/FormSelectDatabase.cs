using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ArchAngel.Providers.Database.Controls
{
    [DotfuscatorDoNotRename]
    public partial class FormSelectDatabase : Form
    {
        #region Enums
        public enum Action
        {
            AddDatabase,
            EditDatabase
        }
        #endregion

        #region Fields
        private bool SaveClicked = false;
        private BLL.ConnectionStringHelper _connectionString;
        private readonly Slyce.Common.CrossThreadHelper CrossThreadHelper;
        #endregion

        public FormSelectDatabase()
        {
            CrossThreadHelper = new Slyce.Common.CrossThreadHelper(this);
            InitializeComponent();
            //BackColor = Slyce.Common.Colors.BackgroundColor;
            Populate();
            ucHeading1.Text = "";
            Interfaces.Events.ShadeMainForm();
        }

        public DatabaseTypes DatabaseType
        {
            get
            {
                string dbTypeString = "";

                if (comboBoxDatabaseTypes.InvokeRequired)
                {
                    dbTypeString = (string)CrossThreadHelper.GetCrossThreadProperty(comboBoxDatabaseTypes, "Text");
                }
                else
                {
                    dbTypeString = comboBoxDatabaseTypes.Text;
                }
                return (DatabaseTypes)Enum.Parse(typeof(DatabaseTypes), dbTypeString);
            }
        }

        public ArchAngel.Providers.Database.BLL.ConnectionStringHelper ConnectionString
        {
            get
            {
                if (_connectionString == null)
                {
                    _connectionString = new ArchAngel.Providers.Database.BLL.ConnectionStringHelper(comboBoxServers.Text, DatabaseName, textBoxUserName.Text, textBoxPassword.Text, checkBoxUseIntegratedSecurity.Checked, optDBFile.Checked, textBoxFilename.Text, DatabaseType);
                }
                return _connectionString;
            }
        }

        private void Populate()
        {
            TestStatusText = "";
            string[] names = Enum.GetNames(typeof(DatabaseTypes));
            comboBoxDatabaseTypes.Items.Clear();

            foreach (string name in names)
            {
                comboBoxDatabaseTypes.Items.Add(name);
            }
			if (comboBoxDatabaseTypes.Items.Count > 0)
				comboBoxDatabaseTypes.SelectedIndex = 0;
            SetLogonEnabledStates();
            HideControls();
        }

        public bool ShowAddDatabase(IWin32Window ownerForm)
        {
            this.Text = "Select Database";
            //this.ShowDialog(this.ParentForm);
            this.ShowDialog(ownerForm);
            return SaveClicked;
        }

        public bool ShowEditDatabase(ArchAngel.Providers.Database.BLL.ConnectionStringHelper connectionString)
        {
            //CurrentAction = Action.EditDatabase;
            this.Text = "Edit Database";
            comboBoxDatabaseTypes.Text = connectionString.CurrentDbType.ToString();
            comboBoxDatabases.Text = connectionString.DatabaseName;
            comboBoxServers.Text = connectionString.ServerName;
            textBoxPassword.Text = connectionString.Password;
            textBoxUserName.Text = connectionString.UserName;
            checkBoxUseIntegratedSecurity.Checked = connectionString.UseIntegratedSecurity;
            buttonSave.Text = "OK";
            Cursor = Cursors.Default;
            //ShowPanel(pnlOutput);
            this.ShowDialog(this.ParentForm);
            comboBoxDatabases.Focus();
            return SaveClicked;
        }

        private void buttonRefreshServers_Click(object sender, EventArgs e)
        {
            RefreshServers();
        }

        private void RefreshServers()
        {
            if (backgroundWorker1.IsBusy)
            {
                MessageBox.Show("Busy, please wait...", "Busy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Cursor = Cursors.WaitCursor;
        	Font boldFont = new Font(comboBoxServers.Font, FontStyle.Bold);

            comboBoxServers.Items.Clear();
            comboBoxServers.Font = boldFont;
            //comboBoxServers.ForeColor = Color.Red;
            comboBoxServers.Text = "Finding servers...";
            comboBoxServers.Refresh();
            Application.DoEvents();
            backgroundWorker1.RunWorkerAsync("RefreshServers");
        }

        private void RefreshServersAsync()
        {
            Font originalFont = comboBoxServers.Font;

        	try
            {
                string[] registeredServerNames = new string[0];

                switch (DatabaseType)
                {
                    case DatabaseTypes.SQLServer2000:
                        registeredServerNames = ArchAngel.Providers.Database.SQLServerDAL_2005.SQLServer.GetSqlServers();
                        break;
                    case DatabaseTypes.SQLServer2005:
                        registeredServerNames = ArchAngel.Providers.Database.SQLServerDAL_2005.SQLServer.GetSqlServers();
                        break;
                    case DatabaseTypes.SQLServerExpress:
                        registeredServerNames = ArchAngel.Providers.Database.SQLServerDAL_Express.SQLServer.GetSqlServers();
                        break;
                    default:
                        MessageBox.Show("This type of database is not yet handled: " + DatabaseType.ToString());
                        break;
                }
                CrossThreadHelper.CallCrossThreadMethod(comboBoxServers.Items, "Clear", null);
                CrossThreadHelper.SetCrossThreadProperty(comboBoxServers, "Text", "");
                CrossThreadHelper.SetCrossThreadProperty(comboBoxServers, "Font", originalFont);
                CrossThreadHelper.SetCrossThreadProperty(comboBoxServers, "ForeColor", Color.Black);

                foreach (string server in registeredServerNames)
                {
                    CrossThreadHelper.CallCrossThreadMethod(comboBoxServers.Items, "Add", new object[] { server });
                }
                if (comboBoxServers.Items.Count > 0)
                {
                    CrossThreadHelper.SetCrossThreadProperty(comboBoxServers, "SelectedIndex", 0);
                }
            }
            catch (Exception)
            {
                CrossThreadHelper.SetCrossThreadProperty(comboBoxServers, "Text", "");
                CrossThreadHelper.SetCrossThreadProperty(comboBoxServers, "Font", originalFont);
                CrossThreadHelper.SetCrossThreadProperty(comboBoxServers, "ForeColor", Color.Black);
                //Common.ReportError(ex);
            }
            finally
            {
                CrossThreadHelper.SetCrossThreadProperty(this, "Cursor", Cursors.Default);
            }

        }

        private void buttonRefreshSqlSmoDatabase_Click(object sender, EventArgs e)
        {
            RefreshDatabases();
        }

        private void RefreshDatabases()
        {
            if (backgroundWorker1.IsBusy)
            {
                MessageBox.Show("Busy, please wait...", "Busy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (textBoxUserName.Text.Trim().Length == 0)
            {
                labelUsernameError.Visible = true;
                return;
            }
            Cursor = Cursors.WaitCursor;
        	Font boldFont = new Font(comboBoxDatabases.Font, FontStyle.Bold);

            comboBoxDatabases.Items.Clear();
            comboBoxDatabases.Font = boldFont;
            //comboBoxDatabases.ForeColor = Color.Red;
            comboBoxDatabases.Text = "Finding databases...";
            comboBoxDatabases.Refresh();
            Application.DoEvents();
            backgroundWorker1.RunWorkerAsync("RefreshDatabases");
        }

        private void RefreshDatabasesAsync()
        {
            Font originalFont = comboBoxServers.Font;

        	try
            {
                string server = (string)CrossThreadHelper.GetCrossThreadProperty(comboBoxServers, "Text");
                string userName = (string)CrossThreadHelper.GetCrossThreadProperty(textBoxUserName, "Text");
                string password = (string)CrossThreadHelper.GetCrossThreadProperty(textBoxPassword, "Text");
                bool useItegratedSecurity = (bool)CrossThreadHelper.GetCrossThreadProperty(checkBoxUseIntegratedSecurity, "Checked");
                string[] databaseNames = new string[0];

                switch (DatabaseType)
                {
                    case DatabaseTypes.SQLServer2000:
                        databaseNames = ArchAngel.Providers.Database.SQLServerDAL_2005.Database.GetDatabases(server, userName, password, DatabaseTypes.SQLServer2000, useItegratedSecurity);
                        break;
                    case DatabaseTypes.SQLServer2005:
                        databaseNames = ArchAngel.Providers.Database.SQLServerDAL_2005.Database.GetDatabases(server, userName, password, DatabaseTypes.SQLServer2005, useItegratedSecurity);
                        break;
                    case DatabaseTypes.SQLServerExpress:
                        databaseNames = ArchAngel.Providers.Database.SQLServerDAL_Express.Database.GetDatabases(server, userName, password, DatabaseTypes.SQLServer2005, useItegratedSecurity);
                        break;
                    default:
                        MessageBox.Show("This type of database is not yet handled: " + DatabaseType.ToString());
                        break;
                }
                CrossThreadHelper.CallCrossThreadMethod(comboBoxServers.Items, "Clear", null);
                CrossThreadHelper.SetCrossThreadProperty(comboBoxDatabases, "Text", "");
                CrossThreadHelper.SetCrossThreadProperty(comboBoxDatabases, "Font", originalFont);
                CrossThreadHelper.SetCrossThreadProperty(comboBoxDatabases, "ForeColor", Color.Black);

                foreach (string databaseName in databaseNames)
                {
                    CrossThreadHelper.CallCrossThreadMethod(comboBoxDatabases.Items, "Add", new object[] { databaseName });
                }
                if (comboBoxDatabases.Items.Count > 0)
                {
                    CrossThreadHelper.SetCrossThreadProperty(comboBoxDatabases, "SelectedIndex", 0);
                }
            }
            catch (Exception ex)
            {
                CrossThreadHelper.SetCrossThreadProperty(comboBoxDatabases, "Text", "");
                CrossThreadHelper.SetCrossThreadProperty(comboBoxDatabases, "Font", originalFont);
                CrossThreadHelper.SetCrossThreadProperty(comboBoxDatabases, "ForeColor", Color.Black);

                if (ex.InnerException != null && ex.InnerException.Message.IndexOf("Login failed for user") >= 0)
                {
                    MessageBox.Show("The username or password is incorrect.", "Invalid login", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (ex.InnerException != null && ex.InnerException.Message.ToLower().IndexOf("an error has occurred while establishing a connection to the server") >= 0)
                {
                    MessageBox.Show(string.Format("'{0}' cannot be found, or you don't have access to it.", comboBoxServers.Text), "Database not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    //Common.ReportError(ex);
                }
            }
            finally
            {
                CrossThreadHelper.SetCrossThreadProperty(this, "Cursor", Cursors.Default);
            }
        }

        public string DatabaseName
        {
            get
            {
                if (optDBFile.Checked)
                {
                    string filename = System.IO.Path.GetFileNameWithoutExtension(textBoxFilename.Text);

                    if (comboBoxDatabaseTypes.Text == DatabaseTypes.SQLServerExpress.ToString())
                    {
                        // Remove the _Data portion of the name
                        filename = filename.Replace("_Data", "");
                    }
                    return filename;
                }
                else
                {
                    return comboBoxDatabases.Text;
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveClicked = true;
            this.Close();
        }

        private void comboBoxDatabaseTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            TestStatusText = "";
            HideControls();
            comboBoxServers.Text = "";
            SetDbSelectionVisibility();
        }

        private void HideControls()
        {
            textBoxFilename.Visible = false;
            buttonBrowse.Visible = false;
            optDB.Visible = false;
            labelDatabase.Visible = true;
            tableLayoutPanel1.Controls.Add(labelDatabase, 0, 7);
            optDBFile.Visible = false;
        }

        private void textBoxUserName_TextChanged(object sender, EventArgs e)
        {
            TestStatusText = "";
            labelUsernameError.Visible = textBoxUserName.Text.Length == 0;
        }

        private void textBoxUserName_Validated(object sender, EventArgs e)
        {

        }

        private void checkBoxUseIntegratedSecurity_CheckedChanged(object sender, EventArgs e)
        {
            TestStatusText = "";
            SetLogonEnabledStates();
        }

        private void SetLogonEnabledStates()
        {
            textBoxUserName.Enabled = !checkBoxUseIntegratedSecurity.Checked;
            textBoxPassword.Enabled = !checkBoxUseIntegratedSecurity.Checked;
            labelUserName.Enabled = !checkBoxUseIntegratedSecurity.Checked;
            labelPassword.Enabled = !checkBoxUseIntegratedSecurity.Checked;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            switch ((string)e.Argument)
            {
                case "RefreshServers":
                    RefreshServersAsync();
                    break;
                case "RefreshDatabases":
                    RefreshDatabasesAsync();
                    break;
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                string message = e.Error.Message;

                if (e.Error.InnerException != null)
                {
                    message += e.Error.InnerException.Message;
                }
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBoxDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {
            TestStatusText = "";
        }

        private void comboBoxServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            TestStatusText = "";
            comboBoxDatabases.Items.Clear();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "SQL Express file (*.mdf)|*.mdf|All files (*.*)|*.*";

            if (System.IO.File.Exists(textBoxFilename.Text))
            {
                openFileDialog1.FileName = textBoxFilename.Text;
            }
            else
            {
                openFileDialog1.InitialDirectory = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            }
            if (openFileDialog1.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }
            textBoxFilename.Text = openFileDialog1.FileName;
        }

        private void optDB_CheckedChanged(object sender, EventArgs e)
        {
            SetDbSelectionVisibility();
        }

        private string TestStatusText
        {
            set
            {
                labelTestStatus.Text = value;
                toolTip1.SetToolTip(labelTestStatus, value);
            }
        }

        private void SetDbSelectionVisibility()
        {
			try
			{
				TestStatusText = "";
				Slyce.Common.Utility.SuspendPainting(groupPanel1);
				switch (DatabaseType)
				{
					case DatabaseTypes.SQLServerExpress:
						optDB.Visible = true;
						labelDatabase.Visible = false;
						tableLayoutPanel1.Controls.Add(optDB, 0, 7);
						optDBFile.Visible = true;
						textBoxFilename.Visible = true;
						buttonBrowse.Visible = true;
						break;
					default:
						optDB.Checked = true;
						optDB.Visible = false;
						labelDatabase.Visible = true;
						tableLayoutPanel1.Controls.Add(labelDatabase, 0, 7);
						optDBFile.Visible = false;
						textBoxFilename.Visible = false;
						buttonBrowse.Visible = false;
						break;
				}
				comboBoxDatabases.Enabled = optDB.Checked;
				textBoxFilename.Enabled = buttonBrowse.Enabled = optDBFile.Checked;
				buttonRefreshDatabases.Enabled = optDB.Checked;
			}
			finally
			{
				Slyce.Common.Utility.ResumePainting(groupPanel1);
			}
        }

        private void optDBFile_CheckedChanged(object sender, EventArgs e)
        {
            SetDbSelectionVisibility();
        }

        private void buttonTestConnection_Click(object sender, EventArgs e)
        {
            TestConnection();
        }

        private bool TestConnection()
        {
            TestStatusText = "";

            if (optDBFile.Checked && textBoxFilename.Text.Length == 0)
            {
                TestStatusText = "No file has been selected.";
                return false;
            }
            if (optDB.Checked && comboBoxDatabases.Text.Length == 0)
            {
                TestStatusText = "No database has been selected.";
                return false;
            }
            Cursor = Cursors.WaitCursor;

            try
            {
                switch (DatabaseType)
                {
                    case DatabaseTypes.SQLServerExpress:
                        System.Data.SqlClient.SqlConnection connSqlExpress = new System.Data.SqlClient.SqlConnection(ConnectionString.GetConnectionStringSqlClient(DatabaseTypes.SQLServerExpress));

                        try
                        {
                            connSqlExpress.Open();
                            TestStatusText = "Success";
                            return true;
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message.IndexOf("already exists.") > 0 &&
                                ex.Message.IndexOf("Could not attach file") > 0)
                            {
                                ConnectionString.SqlExpressDbIsAlreadyAttached = true;
                                connSqlExpress = new System.Data.SqlClient.SqlConnection(ConnectionString.GetConnectionStringSqlClient(DatabaseTypes.SQLServerExpress));
                            }
                            try
                            {
                                connSqlExpress.Open();
                                TestStatusText = "Success";
                                return true;
                            }
                            catch (Exception ex2)
                            {
                                TestStatusText = "Failed: " + ex2.Message;
                                return false;
                            }
                        }
                        finally
                        {
                            if (connSqlExpress != null) { connSqlExpress.Close(); }
                        }
                    case DatabaseTypes.SQLServer2000:
                        System.Data.SqlClient.SqlConnection connSql2000 = new System.Data.SqlClient.SqlConnection(ConnectionString.GetConnectionStringSqlClient(DatabaseTypes.SQLServer2000));

                        try
                        {
                            connSql2000.Open();
                            TestStatusText = "Success";
                            return true;
                        }
                        catch (Exception ex)
                        {
                            TestStatusText = "Failed: " + ex.Message;
                            return false;
                        }
                        finally
                        {
                            if (connSql2000 != null) { connSql2000.Close(); }
                        }
                    case DatabaseTypes.SQLServer2005:
                        System.Data.SqlClient.SqlConnection connSql2005 = new System.Data.SqlClient.SqlConnection(ConnectionString.GetConnectionStringSqlClient(DatabaseTypes.SQLServer2005));

                        try
                        {
                            connSql2005.Open();
                            TestStatusText = "Success";
                            return true;
                        }
                        catch (Exception ex)
                        {
                            TestStatusText = "Failed: " + ex.Message;
                            return false;
                        }
                        finally
                        {
                            if (connSql2005 != null) { connSql2005.Close(); }
                        }
                    default:
                        TestStatusText = "This type of database is not yet handled: " + DatabaseType.ToString();
                        return false;
                }
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void labelFilename_Click(object sender, EventArgs e)
        {
            optDBFile.Checked = true;
        }

        private void labelDatabases_Click(object sender, EventArgs e)
        {
            optDB.Checked = true;
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
            TestStatusText = "";
        }

        private void textBoxFilename_TextChanged(object sender, EventArgs e)
        {
            TestStatusText = "";
        }

        private void FormSelectDatabase_FormClosed(object sender, FormClosedEventArgs e)
        {
            ArchAngel.Interfaces.Events.UnShadeMainForm();
        }
    }
}