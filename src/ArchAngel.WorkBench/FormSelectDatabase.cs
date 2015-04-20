using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ArchAngel.Workbench
{
    public partial class FormSelectDatabase : Form
    {
        #region Delegate Definitions
        // delegates used to call MainForm functions from worker thread
        public delegate void FormSelectDatabase_DelegateSetProperty(object obj, string propertyName, object val);
        public delegate object FormSelectDatabase_DelegateGetProperty(object obj, string propertyName);
        public delegate object FormSelectDatabase_DelegateCallMethod(object obj, string methodName, object[] parameters);
        #endregion

        #region Delegate Instances
        // Delegate instances used to call user interface functions 
        // from worker thread:
        private FormSelectDatabase_DelegateSetProperty _delegateSetProperty;
        private FormSelectDatabase_DelegateGetProperty _delegateGetProperty;
        private FormSelectDatabase_DelegateCallMethod _delegateCallMethod;
        #endregion

        #region Enums
        public enum Action
        {
            AddDatabase,
            EditDatabase
        }
        #endregion
        
        #region Fields
        private Action CurrentAction;
        private bool SaveClicked = false;
        private ArchAngel.Providers.Database.BLL.ConnectionStringHelper _connectionString;
        #endregion

        public FormSelectDatabase()
        {
            InitializeComponent();
            this.BackColor = Slyce.Common.Colors.BackgroundColor;
            _delegateSetProperty = new FormSelectDatabase_DelegateSetProperty(this.SetObjectProperty);
            _delegateGetProperty = new FormSelectDatabase_DelegateGetProperty(this.GetObjectProperty);
            _delegateCallMethod = new FormSelectDatabase_DelegateCallMethod(this.CallObjectMethod);
            Populate();
            ucHeading1.Text = "";
            Controller.ShadeMainForm();
        }

        public ArchAngel.Providers.Database.BLL.ConnectionStringHelper.DatabaseTypes DatabaseType
        {
            get 
            {
                string dbTypeString = "";

                if (comboBoxDatabaseTypes.InvokeRequired)
                {
                    dbTypeString = (string)GetCrossThreadProperty(comboBoxDatabaseTypes, "Text");
                }
                else
                {
                    dbTypeString = comboBoxDatabaseTypes.Text;
                }
                return (ArchAngel.Providers.Database.BLL.ConnectionStringHelper.DatabaseTypes)Enum.Parse(typeof(ArchAngel.Providers.Database.BLL.ConnectionStringHelper.DatabaseTypes), dbTypeString);
            }
        }
        internal ArchAngel.Providers.Database.BLL.ConnectionStringHelper ConnectionString
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
            string[] names = Enum.GetNames(typeof(ArchAngel.Providers.Database.BLL.ConnectionStringHelper.DatabaseTypes));
            comboBoxDatabaseTypes.Items.Clear();

            foreach (string name in names)
            {
                comboBoxDatabaseTypes.Items.Add(name);
            }
            SetLogonEnabledStates();
            HideControls();
        }

        public bool ShowAddDatabase()
        {
            CurrentAction = Action.AddDatabase;
            this.Text = "New Database";
            //ShowPanel(pnlOutput);
            //comboBoxDatabases.Focus();
            this.ShowDialog(this.ParentForm);
            return SaveClicked;
        }

        public bool ShowEditDatabase(ArchAngel.Providers.Database.BLL.ConnectionStringHelper connectionString)
        {
            CurrentAction = Action.EditDatabase;
            this.Text = "Edit Database";
            comboBoxDatabaseTypes.Text = connectionString.CurrentDbType.ToString();
            comboBoxDatabases.Text = connectionString.DatabaseName;
            comboBoxServers.Text = connectionString.ServerName;
            textBoxPassword.Text = connectionString.Password;
            textBoxUserName.Text = connectionString.UserName;
            checkBoxUseIntegratedSecurity.Checked = connectionString.UseIntegratedSecurity;
            buttonSave.Text = "Save";
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
            Font originalFont = comboBoxServers.Font;
            Font boldFont = new Font(comboBoxServers.Font, FontStyle.Bold);

            comboBoxServers.Items.Clear();
            comboBoxServers.Font = boldFont;
            comboBoxServers.ForeColor = Color.Red;
            comboBoxServers.Text = "Finding servers...";
            comboBoxServers.Refresh();
            Application.DoEvents();
            backgroundWorker1.RunWorkerAsync("RefreshServers");
        }

        private void RefreshServersAsync()
        {
            Font originalFont = comboBoxServers.Font;
            Font boldFont = new Font(comboBoxServers.Font, FontStyle.Bold);

            try
            {
                string[] registeredServerNames = new string[0];

                switch (DatabaseType)
                {
#if USE_SMO
                    case ArchAngel.Providers.Database.BLL.ConnectionStringHelper.DatabaseTypes.SQLServerDmo:
                        registeredServerNames = ArchAngel.SQLServerDAL_DMO.SQLServer.GetSqlServers();
                        break;
                    case ArchAngel.Providers.Database.BLL.ConnectionStringHelper.DatabaseTypes.SQLServerSmo:
                        registeredServerNames = ArchAngel.SQLServerDAL_SMO.SQLServer.GetSqlServers();
                        break;
#endif
                    case ArchAngel.Providers.Database.BLL.ConnectionStringHelper.DatabaseTypes.SQLServer2000:
                        registeredServerNames = ArchAngel.Providers.Database.SQLServerDAL_2000.SQLServer.GetSqlServers();
                        break;
                    case ArchAngel.Providers.Database.BLL.ConnectionStringHelper.DatabaseTypes.SQLServer2005:
                        registeredServerNames = ArchAngel.Providers.Database.SQLServerDAL_2005.SQLServer.GetSqlServers();
                        break;
                    case ArchAngel.Providers.Database.BLL.ConnectionStringHelper.DatabaseTypes.SQLServerExpress:
                        registeredServerNames = ArchAngel.Providers.Database.SQLServerDAL_Express.SQLServer.GetSqlServers();
                        break;
                    default:
                        MessageBox.Show("This type of database is not yet handled: " + DatabaseType.ToString());
                        break;
                }
                CallCrossThreadMethod(comboBoxServers.Items, "Clear", null);
                SetCrossThreadProperty(comboBoxServers, "Text", "");
                SetCrossThreadProperty(comboBoxServers, "Font", originalFont);
                SetCrossThreadProperty(comboBoxServers, "ForeColor", Color.Black);

                foreach (string server in registeredServerNames)
                {
                    CallCrossThreadMethod(comboBoxServers.Items, "Add", new object[] { server });
                }
                if (comboBoxServers.Items.Count > 0)
                {
                    SetCrossThreadProperty(comboBoxServers, "SelectedIndex", 0);
                }
            }
            catch (Exception ex)
            {
                SetCrossThreadProperty(comboBoxServers, "Text", "");
                SetCrossThreadProperty(comboBoxServers, "Font", originalFont);
                SetCrossThreadProperty(comboBoxServers, "ForeColor", Color.Black);
                Controller.ReportError(ex);
            }
            finally
            {
                SetCrossThreadProperty(this, "Cursor", Cursors.Default);
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
            Font originalFont = comboBoxServers.Font;
            Font boldFont = new Font(comboBoxDatabases.Font, FontStyle.Bold);

            comboBoxDatabases.Items.Clear();
            comboBoxDatabases.Font = boldFont;
            comboBoxDatabases.ForeColor = Color.Red;
            comboBoxDatabases.Text = "Finding databases...";
            comboBoxDatabases.Refresh();
            Application.DoEvents();
            backgroundWorker1.RunWorkerAsync("RefreshDatabases");
        }

        private void RefreshDatabasesAsync()
        {
            Font originalFont = comboBoxServers.Font;
            Font boldFont = new Font(comboBoxServers.Font, FontStyle.Bold);

            try
            {
                string server = (string)GetCrossThreadProperty(comboBoxServers, "Text");
                string userName = (string)GetCrossThreadProperty(textBoxUserName, "Text");
                string password = (string)GetCrossThreadProperty(textBoxPassword, "Text");
                bool useItegratedSecurity = (bool)GetCrossThreadProperty(checkBoxUseIntegratedSecurity, "Checked");
                string[] databaseNames = new string[0];

                switch (DatabaseType)
                {
#if USE_SMO
                    case ArchAngel.Providers.Database.BLL.ConnectionStringHelper.DatabaseTypes.SQLServerDmo:
                        databaseNames = ArchAngel.SQLServerDAL_DMO.Database.GetDatabases(server, userName, password, ArchAngel.Providers.Database.BLL.ConnectionStringHelper.DatabaseTypes.SQLServerDmo, useItegratedSecurity);
                        break;
                    case ArchAngel.Providers.Database.BLL.ConnectionStringHelper.DatabaseTypes.SQLServerSmo:
                        databaseNames = ArchAngel.SQLServerDAL_SMO.Database.GetDatabases(server, userName, password, ArchAngel.Providers.Database.BLL.ConnectionStringHelper.DatabaseTypes.SQLServerSmo, useItegratedSecurity);
                        break;
#endif
                    case ArchAngel.Providers.Database.BLL.ConnectionStringHelper.DatabaseTypes.SQLServer2000:
                        databaseNames = ArchAngel.Providers.Database.SQLServerDAL_2000.Database.GetDatabases(server, userName, password, ArchAngel.Providers.Database.BLL.ConnectionStringHelper.DatabaseTypes.SQLServer2000, useItegratedSecurity);
                        break;
                    case ArchAngel.Providers.Database.BLL.ConnectionStringHelper.DatabaseTypes.SQLServer2005:
                        databaseNames = ArchAngel.Providers.Database.SQLServerDAL_2005.Database.GetDatabases(server, userName, password, ArchAngel.Providers.Database.BLL.ConnectionStringHelper.DatabaseTypes.SQLServer2005, useItegratedSecurity);
                        break;
                    case ArchAngel.Providers.Database.BLL.ConnectionStringHelper.DatabaseTypes.SQLServerExpress:
                        databaseNames = ArchAngel.Providers.Database.SQLServerDAL_Express.Database.GetDatabases(server, userName, password, ArchAngel.Providers.Database.BLL.ConnectionStringHelper.DatabaseTypes.SQLServer2005, useItegratedSecurity);
                        break;
                    default:
                        MessageBox.Show("This type of database is not yet handled: " + DatabaseType.ToString());
                        break;
                }
                CallCrossThreadMethod(comboBoxServers.Items, "Clear", null);
                SetCrossThreadProperty(comboBoxDatabases, "Text", "");
                SetCrossThreadProperty(comboBoxDatabases, "Font", originalFont);
                SetCrossThreadProperty(comboBoxDatabases, "ForeColor", Color.Black);

                foreach (string databaseName in databaseNames)
                {
                    CallCrossThreadMethod(comboBoxDatabases.Items, "Add", new object[] { databaseName });
                }
                if (comboBoxDatabases.Items.Count > 0)
                {
                    SetCrossThreadProperty(comboBoxDatabases, "SelectedIndex", 0);
                }
            }
            catch (Exception ex)
            {
                SetCrossThreadProperty(comboBoxDatabases, "Text", "");
                SetCrossThreadProperty(comboBoxDatabases, "Font", originalFont);
                SetCrossThreadProperty(comboBoxDatabases, "ForeColor", Color.Black);

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
                    Controller.ReportError(ex);
                }
            }
            finally
            {
                SetCrossThreadProperty(this, "Cursor", Cursors.Default);
            }
        }

        internal string DatabaseName
        {
            get
            {
                if (optDBFile.Checked)
                {
                    string filename = System.IO.Path.GetFileNameWithoutExtension(textBoxFilename.Text);

                    if (comboBoxDatabaseTypes.Text == ArchAngel.Providers.Database.BLL.ConnectionStringHelper.DatabaseTypes.SQLServerExpress.ToString())
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
            //Font originalFont = comboBoxServers.Font;
            //Font boldFont = new Font(comboBoxServers.Font, FontStyle.Bold);
            //comboBoxDatabases.Items.Clear();
            //comboBoxDatabases.Text = "";
            //comboBoxServers.Items.Clear();
            //comboBoxServers.Font = boldFont;
            //comboBoxServers.ForeColor = Color.Red;
            //comboBoxServers.Text = "Finding servers...";
            //comboBoxServers.Refresh();
            comboBoxServers.Text = "";
            SetDbSelectionVisibility();
            //RefreshServers();
        }

        private void HideControls()
        {
            //groupBoxLogonDetails.Visible = false;
            //labelDatabases.Visible = false;
            //comboBoxDatabases.Visible = false;
            //buttonRefreshDatabases.Visible = false;
            //labelServers.Visible = false;
            //comboBoxServers.Visible = false;
            //buttonRefreshServers.Visible = false;
            textBoxFilename.Visible = false;
            labelFilename.Visible = false;
            buttonBrowse.Visible = false;
            optDB.Visible = false;
            optDBFile.Visible = false;
        }

        private void textBoxUserName_TextChanged(object sender, EventArgs e)
        {
            TestStatusText = "";
            labelUsernameError.Visible = textBoxUserName.Text.Length == 0;
        }

        private void FormSelectDatabase_Load(object sender, EventArgs e)
        {
            
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

        #region Cross Threading Methods

        private void SetObjectProperty(object obj, string propertyName, object val)
        {
            System.Reflection.PropertyInfo pi = obj.GetType().GetProperty(propertyName);
            pi.SetValue(obj, val, null);
        }

        private object GetObjectProperty(object obj, string propertyName)
        {
            System.Reflection.PropertyInfo pi = obj.GetType().GetProperty(propertyName);
            return pi.GetValue(obj, null); // TODO: if we need to access an array, we need to change this from null
        }

        private object CallObjectMethod(object obj, string methodName, object[] parameters)
        {
            Type type = obj.GetType();

            if (methodName.IndexOf(".") > 0)
            {
                string[] parts = methodName.Split('.');
                System.Reflection.PropertyInfo pi = type.GetProperty(parts[0]);

                if (pi != null)
                {
                    obj = pi.GetValue(obj, null);
                    type = obj.GetType();
                    methodName = parts[1];
                }
            }
            Type[] paramTypes = new Type[0];

            if (parameters != null)
            {
                paramTypes = new Type[parameters.Length];

                for (int i = 0; i < parameters.Length; i++)
                {
                    paramTypes[i] = parameters[i].GetType();
                }
            }
            System.Reflection.MethodInfo mi = type.GetMethod(methodName, paramTypes);
            return mi.Invoke(obj, parameters);
        }

        private object CallCrossThreadMethod(object obj, string methodName, object[] parameters)
        {
            try
            {
                return this.Invoke(_delegateCallMethod, obj, methodName, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error calling CallCrossThreadMethod (Object: {0}, methodName: {1})", (obj != null ? obj.ToString() : "??"), methodName), ex);
            }
        }

        private void SetCrossThreadProperty(object obj, string propertyName, object val)
        {
            this.Invoke(_delegateSetProperty, obj, propertyName, val);
        }

        private object GetCrossThreadProperty(object obj, string propertyName)
        {
            return this.Invoke(_delegateGetProperty, obj, propertyName);
        }

        #endregion

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
            else if (!string.IsNullOrEmpty(Controller.Instance.AppConfig.ProjectPath) && System.IO.Directory.Exists(Controller.Instance.AppConfig.ProjectPath))
            {
                openFileDialog1.InitialDirectory = Controller.Instance.AppConfig.ProjectPath;
            }
            else
            {
                openFileDialog1.InitialDirectory = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            }
            Controller.ShadeMainForm();

            if (openFileDialog1.ShowDialog(this) != DialogResult.OK)
            {
                Controller.UnshadeMainForm();
                return;
            }
            Controller.UnshadeMainForm();
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
            TestStatusText = "";

            switch (DatabaseType)
            {
#if USE_SMO
                case ArchAngel.Providers.Database.BLL.ConnectionStringHelper.DatabaseTypes.Access:
#endif
                case ArchAngel.Providers.Database.BLL.ConnectionStringHelper.DatabaseTypes.SQLServerExpress:
                    optDB.Visible = true;
                    optDBFile.Visible = true;
                    textBoxFilename.Visible = true;
                    buttonBrowse.Visible = true;
                    labelFilename.Visible = true;
                    break;
                default:
                    optDB.Checked = true;
                    optDB.Visible = false;
                    optDBFile.Visible = false;
                    textBoxFilename.Visible = false;
                    buttonBrowse.Visible = false;
                    labelFilename.Visible = false;
                    break;
            }
            comboBoxDatabases.Enabled = optDB.Checked;
            textBoxFilename.Enabled = buttonBrowse.Enabled = optDBFile.Checked;
            buttonRefreshDatabases.Enabled = optDB.Checked;
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
#if USE_SMO
                    case ArchAngel.Providers.Database.BLL.ConnectionStringHelper.DatabaseTypes.Access:
                        System.Data.OleDb.OleDbConnection connAccess = new System.Data.OleDb.OleDbConnection(ConnectionString.GetConnectionStringSqlClient(ArchAngel.Providers.Database.BLL.ConnectionStringHelper.DatabaseTypes.Access));

                        try
                        {
                            connAccess.Open();
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
                            if (connAccess != null) { connAccess.Close(); }
                        }
#endif
                    case ArchAngel.Providers.Database.BLL.ConnectionStringHelper.DatabaseTypes.SQLServerExpress:
                        System.Data.SqlClient.SqlConnection connSqlExpress = new System.Data.SqlClient.SqlConnection(ConnectionString.GetConnectionStringSqlClient(ArchAngel.Providers.Database.BLL.ConnectionStringHelper.DatabaseTypes.SQLServerExpress));

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
                                connSqlExpress = new System.Data.SqlClient.SqlConnection(ConnectionString.GetConnectionStringSqlClient(ArchAngel.Providers.Database.BLL.ConnectionStringHelper.DatabaseTypes.SQLServerExpress));
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
#if USE_SMO
                    case ArchAngel.Providers.Database.BLL.ConnectionStringHelper.DatabaseTypes.SQLServerDmo:
                        System.Data.SqlClient.SqlConnection connSqlDmo = new System.Data.SqlClient.SqlConnection(ConnectionString.GetConnectionStringSqlClient(ArchAngel.Providers.Database.BLL.ConnectionStringHelper.DatabaseTypes.SQLServerDmo));

                        try
                        {
                            connSqlDmo.Open();
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
                            if (connSqlDmo != null) { connSqlDmo.Close(); }
                        }
                    case ArchAngel.Providers.Database.BLL.ConnectionStringHelper.DatabaseTypes.SQLServerSmo:
                        System.Data.SqlClient.SqlConnection connSqlSmo = new System.Data.SqlClient.SqlConnection(ConnectionString.GetConnectionStringSqlClient(ArchAngel.Providers.Database.BLL.ConnectionStringHelper.DatabaseTypes.SQLServerSmo));

                        try
                        {
                            connSqlSmo.Open();
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
                            if (connSqlSmo != null) { connSqlSmo.Close(); }
                        }
#endif
                    case ArchAngel.Providers.Database.BLL.ConnectionStringHelper.DatabaseTypes.SQLServer2000:
                        System.Data.SqlClient.SqlConnection connSql2000 = new System.Data.SqlClient.SqlConnection(ConnectionString.GetConnectionStringSqlClient(ArchAngel.Providers.Database.BLL.ConnectionStringHelper.DatabaseTypes.SQLServer2000));

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
                    case ArchAngel.Providers.Database.BLL.ConnectionStringHelper.DatabaseTypes.SQLServer2005:
                        System.Data.SqlClient.SqlConnection connSql2005 = new System.Data.SqlClient.SqlConnection(ConnectionString.GetConnectionStringSqlClient(ArchAngel.Providers.Database.BLL.ConnectionStringHelper.DatabaseTypes.SQLServer2005));

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
            Controller.UnshadeMainForm();
        }


    }
}