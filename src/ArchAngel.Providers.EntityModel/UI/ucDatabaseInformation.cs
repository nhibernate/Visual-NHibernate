using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.UI.Presenters;
using Slyce.Common;
using Slyce.Common.EventExtensions;

namespace ArchAngel.Providers.EntityModel.UI.PropertyGrids
{
	public partial class ucDatabaseInformation : UserControl, IEventSender, IDatabaseForm
	{
		private enum WorkItem
		{
			RefreshServers, RefreshDatabases
		}

		public event EventHandler TestConnection;
		public event EventHandler RefreshSchema;
		public event EventHandler UsernameChanged;
		public event EventHandler PasswordChanged;
		public event EventHandler SelectedDatabaseChanged;
		public event EventHandler SelectedDatabaseTypeChanged;
		public event EventHandler ServerNameChanged;

		private static bool BusyClearingDatabaseCombo = false;
		public bool EventRaisingDisabled { get; set; }

		private DatabaseTypes selectedDatabaseType = DatabaseTypes.SQLCE;
		private string databaseFilename;

		private readonly BackgroundWorker worker = new BackgroundWorker();

		private bool InvalidateServerCache { get; set; }
		private bool InvalidateDatabaseCache { get; set; }

		public ucDatabaseInformation()
		{
			InitializeComponent();

			ArchAngel.Interfaces.SharedData.AboutToSave += new EventHandler(SharedData_AboutToSave);

			//panel1.Width = buttonBrowse.Right + checkBoxDirect.Left;

			if (DesignMode)
				return;

			Populate();

			this.ForeColor = Color.White;
			labelPort.ForeColor = Color.White;

			labelTestStatus.Text = "";

			// See below
			//comboBoxServers.SelectedIndexChanged += comboBoxServers_DropDownChange;
			//comboBoxDatabases.SelectedIndexChanged += comboBoxDatabases_DropDownChange;
			//comboBoxSchemas.SelectedIndexChanged += comboBoxSchemas_DropDownChange;

			EventHandler invalidateDatabase = (s, e) => InvalidateDatabaseCache = true;

			comboBoxServers.TextChanged += invalidateDatabase;
			comboBoxServers.SelectedIndexChanged += invalidateDatabase;
			checkBoxUseIntegratedSecurity.CheckedChanged += invalidateDatabase;
			textBoxFilename.TextChanged += invalidateDatabase;
			textBoxPassword.TextChanged += invalidateDatabase;
			textBoxUserName.TextChanged += invalidateDatabase;
			textBoxPort.TextChanged += invalidateDatabase;
			textBoxServiceName.TextChanged += invalidateDatabase;

			checkBoxUseIntegratedSecurity.CheckedChanged += checkBoxUseIntegratedSecurity_CheckedChanged;
			comboBoxDatabaseTypes.SelectedIndexChanged += comboBoxDatabaseTypes_SelectedIndexChanged;
			textBoxFilename.TextChanged += textBoxFilename_TextChanged;
			comboBoxDatabases.TextChanged += comboBoxDatabases_TextChanged;
			comboBoxServers.SelectedIndexChanged += (s, e) => ServerNameChanged.RaiseEventEx(this);
			textBoxPassword.TextChanged += (s, e) => PasswordChanged.RaiseEventEx(this);
			textBoxUserName.TextChanged += (s, e) => UsernameChanged.RaiseEventEx(this);

			worker.DoWork += worker_DoWork;

			SetSecurityInputEnabledStatus();
		}

		void SharedData_AboutToSave(object sender, EventArgs e)
		{
			//throw new NotImplementedException();
		}

		public int BottomOfControls
		{
			get { return textBoxServiceName.Bottom; }
		}

		void comboBoxDatabases_TextChanged(object sender, EventArgs e)
		{
			SelectDatabase();
			SelectedDatabaseChanged.RaiseEventEx(this);
		}

		void textBoxFilename_TextChanged(object sender, EventArgs e)
		{
			SelectDatabaseFile();
			SelectedDatabaseChanged.RaiseEventEx(this);
		}

		void comboBoxDatabaseTypes_SelectedIndexChanged(object sender, EventArgs e)
		{
			comboBoxDatabases.Text = "";
			SelectedDatabaseTypeChanged.RaiseEventEx(this);
		}

		void worker_DoWork(object sender, DoWorkEventArgs e)
		{
			switch ((WorkItem)e.Argument)
			{
				case WorkItem.RefreshServers:
					RefreshServersAsync();
					break;
				case WorkItem.RefreshDatabases:
					RefreshDatabasesAsync();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		void checkBoxUseIntegratedSecurity_CheckedChanged(object sender, EventArgs e)
		{
			SetSecurityInputEnabledStatus();
		}

		private void SetSecurityInputEnabledStatus()
		{
			bool enabled = !checkBoxUseIntegratedSecurity.Checked;
			textBoxUserName.Enabled = enabled;
			textBoxPassword.Enabled = enabled;
		}

		private void Populate()
		{
			comboBoxDatabaseTypes.Items.Clear();
			Array enumValues = Enum.GetValues(typeof(DatabaseTypes));
			DatabaseTypes lastDbUsed = SettingsEngine.LastDatabaseTypeUsed;

			if (lastDbUsed == DatabaseTypes.Unknown)
				lastDbUsed = DatabaseTypes.SQLServer2005;

			foreach (DatabaseTypes item in enumValues)
			{
				if (item == DatabaseTypes.Unknown)
					continue;
				//if (item == DatabaseTypes.SQLite)
				//    // TODO: Remove this when SQLite dev is finished
				//    continue;

				var description = Utility.GetDescription(item);
				comboBoxDatabaseTypes.Items.Add(new ComboBoxItemEx<DatabaseTypes>(item, f => description));

				if (item == lastDbUsed)
					comboBoxDatabaseTypes.SelectedIndex = comboBoxDatabaseTypes.Items.Count - 1;
			}
			if (comboBoxDatabaseTypes.SelectedIndex < 0)
				comboBoxDatabaseTypes.SelectedIndex = 0;

			comboBoxDatabaseTypes.Sorted = true;
		}

		public void ClearDatabaseOperationResults()
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(ClearDatabaseOperationResults));
				return;
			}
			try
			{
				Utility.SuspendPainting(this);
				labelTestStatus.Text = "";
				labelTestStatus.ForeColor = Color.White;
			}
			finally
			{
				Utility.ResumePainting(this);
			}
		}

		public void SetDatabaseOperationMessage(string message)
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(() => SetDatabaseOperationMessage(message)));
				return;
			}

			try
			{
				//MessageBox.Show(this, message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

				Utility.SuspendPainting(this);

				if (string.IsNullOrEmpty(message))
				{
					labelTestStatus.Text = "";
					return;
				}

				labelTestStatus.Text = message;// "Information";
				labelTestStatus.ForeColor = Color.White;

				//if (string.IsNullOrEmpty(message))
				//{
				//    labelDatabaseOperationMessage.Visible = false;
				//    return;
				//}

				//labelDatabaseOperationMessage.Text = message;
				//labelDatabaseOperationMessage.Visible = true;
			}
			finally
			{
				Utility.ResumePainting(this);
			}
		}

		public bool ReadyToProceed()
		{
			bool readyToProceed = true;

			int val;

			if (!int.TryParse(textBoxPort.Text, out val))
			{
				readyToProceed = false;
				highlighter1.SetHighlightColor(textBoxPort, DevComponents.DotNetBar.Validator.eHighlightColor.Orange);
			}
			else
				highlighter1.SetHighlightColor(textBoxPort, DevComponents.DotNetBar.Validator.eHighlightColor.None);

			if (optDB.Checked && string.IsNullOrEmpty(comboBoxDatabases.Text) && SelectedDatabaseType != DatabaseTypes.Oracle)
			{
				readyToProceed = false;
				highlighter1.SetHighlightColor(comboBoxDatabases, DevComponents.DotNetBar.Validator.eHighlightColor.Orange);
			}
			else if (optDBFile.Checked && string.IsNullOrEmpty(textBoxFilename.Text))
			{
				readyToProceed = false;
				highlighter1.SetHighlightColor(textBoxFilename, DevComponents.DotNetBar.Validator.eHighlightColor.Orange);
			}
			else
				highlighter1.SetHighlightColor(comboBoxDatabases, DevComponents.DotNetBar.Validator.eHighlightColor.None);

			if (!readyToProceed)
				MessageBox.Show(this, "Some database information is missing", "Missing data", MessageBoxButtons.OK, MessageBoxIcon.Error);

			return readyToProceed;
		}

		public void SetDatabaseOperationResults(DatabaseOperationResults results)
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(() => SetDatabaseOperationResults(results)));
				return;
			}

			try
			{
				Utility.SuspendPainting(this);
				labelTestStatus.Text = "";

				if (!results.Succeeded)
					MessageBox.Show(this, results.Message, results.OperationName + (results.Succeeded ? " Succeeded" : " Failed"), MessageBoxButtons.OK, results.Succeeded ? MessageBoxIcon.Information : MessageBoxIcon.Error);
				else
				{
					//labelTestStatus.Text = results.OperationName + (results.Succeeded ? " Succeeded" : " Failed");
					//labelTestStatus.ForeColor = results.Succeeded ? Color.Green : Color.Red;

					if (string.IsNullOrEmpty(results.Message))
					{
						labelTestStatus.Text = results.OperationName;// "";
						return;
					}
					labelTestStatus.Text = results.OperationName + " Succeeded";
					labelTestStatus.ForeColor = Color.White;

					//labelDatabaseOperationMessage.Text = results.Message;
					//labelDatabaseOperationMessage.Visible = true;
				}
			}
			finally
			{
				Utility.ResumePainting(this);
			}
		}

		public void SetServersNames(IEnumerable<string> value)
		{
			EventRaisingDisabled = true;
			PopulateComboBox(comboBoxServers, value);
			InvalidateDatabaseCache = true;
			EventRaisingDisabled = false;
		}

		public DatabaseTypes SelectedDatabaseType
		{
			get
			{
				if (selectedDatabaseType != DatabaseTypes.Unknown)
					SettingsEngine.LastDatabaseTypeUsed = selectedDatabaseType;

				return selectedDatabaseType;
			}
			set
			{
				EventRaisingDisabled = true;
				comboBoxDatabaseTypes.SelectedItem = value;

				foreach (var item in comboBoxDatabaseTypes.GetInnerObjects<DatabaseTypes>())
				{
					if (item.Value == value)
					{
						comboBoxDatabaseTypes.SelectedIndex = item.Key;
						break;
					}
				}
				InvalidateServerCache = true;
				InvalidateDatabaseCache = true;
				selectedDatabaseType = value;
				EventRaisingDisabled = false;
				SetControlVisibilities();
			}
		}

		public IEnumerable<string> DatabaseNames
		{
			get
			{
				return comboBoxDatabases.Items.Cast<string>();
			}
			set
			{
				EventRaisingDisabled = true;
				PopulateComboBox(comboBoxDatabases, value);
				EventRaisingDisabled = false;
			}
		}

		/// <summary>
		/// Is either a database or a database file, depending on which
		/// option is selected in the UI.
		/// </summary>
		public string SelectedDatabase
		{
			get
			{
				SetSelectedDatabaseField();
				return databaseFilename;
			}
		}

		public ConnectionStringHelper ConnectionStringHelper
		{
			get
			{
				return GetHelper();
			}
		}


		private ConnectionStringHelper __helper;
		public ConnectionStringHelper GetHelper()
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(() => GetHelper()));
				return __helper;
			}

			__helper = new ConnectionStringHelper
			{
				DatabaseName = comboBoxDatabases.Text,
				CurrentDbType = SelectedDatabaseType,
				FileName = textBoxFilename.Text,
				Password = textBoxPassword.Text,
				ServerName = comboBoxServers.Text,
				UseFileName = optDBFile.Checked,
				UseIntegratedSecurity = checkBoxUseIntegratedSecurity.Checked,
				UserName = textBoxUserName.Text,
				Port = string.IsNullOrEmpty(textBoxPort.Text) ? int.MinValue : int.Parse(textBoxPort.Text),
				ServiceName = textBoxServiceName.Text,
				Direct = checkBoxDirect.Checked
			};
			return __helper;
		}

		private void SetSelectedDatabaseField()
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(SetSelectedDatabaseField));
				return;
			}

			if (optDBFile.Checked)
				databaseFilename = textBoxFilename.Text ?? "";
			else
				databaseFilename = comboBoxDatabases.Text ?? "";
		}

		private string __selectedServerName;
		private string GetSelectedServerName()
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(() => GetSelectedServerName()));
				return __selectedServerName;
			}
			__selectedServerName = comboBoxServers.Text ?? "";
			return __selectedServerName;
		}

		public bool UseIntegratedSecurity
		{
			get { return checkBoxUseIntegratedSecurity.Checked; }
			set { checkBoxUseIntegratedSecurity.Checked = value; }
		}

		private IServerAndDatabaseHelper _databaseHelper = new ServerAndDatabaseHelper();

		public IServerAndDatabaseHelper DatabaseHelper
		{
			get { return _databaseHelper; }
			set { _databaseHelper = value; }
		}

		public void SetDatabaseFilename(string newDatabaseFilename)
		{
			EventRaisingDisabled = true;
			//optDBFile.Checked = true;

			if (!string.IsNullOrEmpty(textBoxFilename.Text))
				optDBFile.Checked = true;
			else if (!string.IsNullOrEmpty(comboBoxDatabases.SelectedText))
				optDB.Checked = true;

			textBoxFilename.Text = newDatabaseFilename;
			EventRaisingDisabled = false;
		}

		public void SelectDatabase()
		{
			optDB.Checked = true;
			optDBFile.Checked = false;
		}

		public void SelectDatabaseFile()
		{
			optDB.Checked = false;
			optDBFile.Checked = true;
		}

		public void SetPort(int port)
		{
			EventRaisingDisabled = true;
			textBoxPort.Text = port.ToString();
			EventRaisingDisabled = false;
		}

		public void SetDatabase(string database)
		{
			EventRaisingDisabled = true;

			if (!string.IsNullOrEmpty(database))
				optDB.Checked = true;
			else if (!string.IsNullOrEmpty(textBoxFilename.Text))
				optDBFile.Checked = true;

			if (string.IsNullOrEmpty(database) == false && comboBoxDatabases.Items.Contains(database) == false)
				comboBoxDatabases.Items.Add(database);

			if (database != null)
				comboBoxDatabases.SelectedItem = database;
			else
			{
				comboBoxDatabases.SelectedItem = "";
				comboBoxDatabases.SelectedText = "";
			}
			EventRaisingDisabled = false;
		}

		public string SelectedServerName
		{
			get
			{
				return GetSelectedServerName();
			}
			set
			{
				EventRaisingDisabled = true;

				if (comboBoxServers.Items.Contains(value) == false)
				{
					comboBoxServers.Items.Add(value);
				}

				comboBoxServers.SelectedIndex = comboBoxServers.Items.IndexOf(value);

				EventRaisingDisabled = false;
			}
		}

		public bool UsingDatabaseFile
		{
			get
			{
				return optDBFile.Checked;
			}
			set
			{
				EventRaisingDisabled = true;
				optDBFile.Checked = value;
				EventRaisingDisabled = false;
			}
		}

		public string Username
		{
			get
			{
				return textBoxUserName.Text ?? "";
			}
			set
			{
				EventRaisingDisabled = true;
				textBoxUserName.Text = value ?? "";
				EventRaisingDisabled = false;
			}
		}

		public bool Direct
		{
			get
			{
				return checkBoxDirect.Checked;
			}
			set
			{
				EventRaisingDisabled = true;
				checkBoxDirect.Checked = value;
				EventRaisingDisabled = false;
			}
		}

		public string Password
		{
			get
			{
				return textBoxPassword.Text ?? "";
			}
			set
			{
				EventRaisingDisabled = true;
				textBoxPassword.Text = value ?? "";
				EventRaisingDisabled = false;
			}
		}

		public int Port
		{
			get
			{
				if (string.IsNullOrEmpty(textBoxPort.Text))
					return ConnectionStringHelper.GetDefaultPort(SelectedDatabaseType);

				return int.Parse(textBoxPort.Text);
			}
			set
			{
				EventRaisingDisabled = true;
				textBoxPort.Text = value.ToString();
				EventRaisingDisabled = false;
			}
		}

		public string ServiceName
		{
			get
			{
				return textBoxServiceName.Text;
			}
			set
			{
				EventRaisingDisabled = true;
				textBoxServiceName.Text = value;
				EventRaisingDisabled = false;
			}
		}

		private void comboBoxDatabaseTypes_SelectedValueChanged(object sender, EventArgs e)
		{
			InvalidateServerCache = true;
			if (comboBoxDatabaseTypes.SelectedItem == null)
				return;

			SelectedDatabaseType = comboBoxDatabaseTypes.GetSelectedItem<DatabaseTypes>();
			SetControlVisibilities();
		}

		private void SetControlVisibilities()
		{
			labelServer.Text = "Server";

			switch (SelectedDatabaseType)
			{
				case DatabaseTypes.SQLCE:
					SetEnabledForSQLServerCE();
					break;
				//case DatabaseTypes.SQLServer2000:
				case DatabaseTypes.SQLServer2005:
					SetEnabledForSQLServer();
					break;
				case DatabaseTypes.SQLServerExpress:
					SetEnabledForSqlServerExpress();
					break;
				case DatabaseTypes.MySQL:
					SetEnabledForMySQL();
					break;
				case DatabaseTypes.Oracle:
					SetEnabledForOracle();
					break;
				case DatabaseTypes.PostgreSQL:
					SetEnabledForPostgreSQL();
					break;
				case DatabaseTypes.Firebird:
					SetEnabledForFirebird();
					break;
				case DatabaseTypes.SQLite:
					SetEnabledForSQLite();
					break;
				case DatabaseTypes.Unknown:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void SetEnabledForOracle()
		{
			ResetAllEnabledTo(true);

			optDB.Visible = false;
			optDB.Checked = true;
			optDBFile.Visible = false;
			optDBFile.Checked = false;
			textBoxFilename.Visible = false;
			buttonBrowse.Visible = false;
			comboBoxDatabases.Visible = false;
			buttonRefreshDatabases.Visible = false;

			checkBoxUseIntegratedSecurity.Checked = false;
			textBoxUserName.Text = "SYSTEM";
			//comboBoxServers.Text = "localhost";
			checkBoxUseIntegratedSecurity.Visible = false;
			checkBoxDirect.Visible = true;
			labelServer.Text = "TNS name";
			labelServer.Visible = true;
			comboBoxServers.Visible = true;
			buttonRefreshServers.Visible = true;

			labelPort.Visible = false;
			textBoxPort.Visible = false;
			textBoxPort.Text = ConnectionStringHelper.GetDefaultPort(DatabaseTypes.Oracle).ToString();

			labelServiceName.Visible = false;
			textBoxServiceName.Visible = false;
			textBoxServiceName.Text = "";

			comboBoxServers.Items.Clear();

			HashSet<string> dbs = OracleHelper.GetOracleInstances();

			foreach (string db in dbs)
				comboBoxServers.Items.Add(db);

			if (dbs.Count > 0)
				comboBoxServers.SelectedIndex = 0;
			else
				comboBoxServers.Text = "";
		}

		private void SetEnabledForPostgreSQL()
		{
			ResetAllEnabledTo(true);

			optDB.Visible = true;
			optDB.Checked = true;
			optDBFile.Visible = false;
			optDBFile.Checked = false;
			textBoxFilename.Visible = false;
			buttonBrowse.Visible = false;
			comboBoxDatabases.Visible = true;
			buttonRefreshDatabases.Visible = true;
			checkBoxDirect.Visible = false;

			checkBoxUseIntegratedSecurity.Checked = false;
			textBoxUserName.Text = "postgres";
			comboBoxServers.Items.Clear();
			comboBoxServers.Text = "localhost";

			optDB.Checked = true;
			optDBFile.Visible = false;
			textBoxFilename.Visible = false;
			buttonBrowse.Visible = false;
			checkBoxUseIntegratedSecurity.Visible = false;
			labelServer.Visible = true;
			comboBoxServers.Visible = true;
			buttonRefreshServers.Visible = true;

			labelPort.Visible = true;
			textBoxPort.Visible = true;
			textBoxPort.Text = ConnectionStringHelper.GetDefaultPort(DatabaseTypes.PostgreSQL).ToString();

			labelServiceName.Visible = false;
			textBoxServiceName.Visible = false;
			textBoxServiceName.Text = "";
		}

		private void SetEnabledForMySQL()
		{
			ResetAllEnabledTo(true);

			optDBFile.Checked = false;
			optDBFile.Visible = false;
			textBoxFilename.Visible = false;
			optDB.Visible = true;
			comboBoxDatabases.Visible = true;
			buttonBrowse.Visible = false;
			buttonRefreshDatabases.Visible = true;

			checkBoxUseIntegratedSecurity.Checked = false;
			textBoxUserName.Text = "root";
			comboBoxServers.Items.Clear();
			comboBoxServers.Text = "localhost";

			optDB.Checked = true;
			optDBFile.Visible = false;
			textBoxFilename.Visible = false;
			buttonBrowse.Visible = false;
			checkBoxUseIntegratedSecurity.Visible = false;
			checkBoxDirect.Visible = false;
			labelServer.Visible = true;
			comboBoxServers.Visible = true;
			buttonRefreshServers.Visible = true;

			labelPort.Visible = true;
			textBoxPort.Visible = true;
			textBoxPort.Text = ConnectionStringHelper.GetDefaultPort(DatabaseTypes.MySQL).ToString();

			labelServiceName.Visible = false;
			textBoxServiceName.Visible = false;
		}

		private void SetEnabledForFirebird()
		{
			ResetAllEnabledTo(true);

			checkBoxUseIntegratedSecurity.Checked = true;
			textBoxUserName.Text = "SYSDBA";
			comboBoxServers.Items.Clear();
			comboBoxServers.Text = "localhost";

			textBoxUserName.Enabled = true;
			textBoxPassword.Enabled = true;

			optDB.Visible = false;
			comboBoxDatabases.Visible = false;
			optDBFile.Checked = true;
			optDBFile.Visible = true;
			textBoxFilename.Visible = true;
			buttonBrowse.Visible = true;
			checkBoxUseIntegratedSecurity.Checked = false;
			checkBoxUseIntegratedSecurity.Visible = false;
			buttonRefreshDatabases.Visible = false;
			checkBoxDirect.Visible = false;
			labelServer.Visible = false;
			comboBoxServers.Visible = false;
			buttonRefreshServers.Visible = false;

			labelPort.Visible = true;
			textBoxPort.Visible = true;
			textBoxPort.Text = ConnectionStringHelper.GetDefaultPort(DatabaseTypes.Firebird).ToString();

			labelServiceName.Visible = false;
			textBoxServiceName.Visible = false;
		}

		private void SetEnabledForSQLite()
		{
			ResetAllEnabledTo(true);

			groupBoxLogin.Visible = false;
			checkBoxUseIntegratedSecurity.Checked = false;
			//textBoxUserName.Text = "SYSDBA";
			comboBoxServers.Items.Clear();
			comboBoxServers.Text = "localhost";

			textBoxUserName.Enabled = true;
			textBoxPassword.Enabled = true;

			optDB.Visible = false;
			comboBoxDatabases.Visible = false;
			optDBFile.Checked = true;
			optDBFile.Visible = true;
			textBoxFilename.Visible = true;
			buttonBrowse.Visible = true;
			checkBoxUseIntegratedSecurity.Checked = false;
			checkBoxUseIntegratedSecurity.Visible = false;
			buttonRefreshDatabases.Visible = false;
			checkBoxDirect.Visible = false;
			labelServer.Visible = false;
			comboBoxServers.Visible = false;
			buttonRefreshServers.Visible = false;

			labelPort.Visible = false;
			textBoxPort.Visible = false;
			textBoxPort.Text = ConnectionStringHelper.GetDefaultPort(DatabaseTypes.Firebird).ToString();

			labelServiceName.Visible = false;
			textBoxServiceName.Visible = false;
		}

		private void SetEnabledForSQLServer()
		{
			ResetAllEnabledTo(true);

			checkBoxUseIntegratedSecurity.Checked = true;
			textBoxUserName.Text = "sa";
			comboBoxServers.Items.Clear();
			comboBoxServers.Text = Environment.MachineName;// ".";

			comboBoxDatabases.Visible = true;
			optDB.Visible = true;
			optDB.Checked = true;
			optDBFile.Visible = false;
			textBoxFilename.Visible = false;
			buttonBrowse.Visible = false;
			checkBoxUseIntegratedSecurity.Visible = true;
			buttonRefreshDatabases.Visible = true;
			checkBoxDirect.Visible = false;
			labelServer.Visible = true;
			comboBoxServers.Visible = true;
			buttonRefreshServers.Visible = true;

			labelPort.Visible = true;
			textBoxPort.Visible = true;
			textBoxPort.Text = ConnectionStringHelper.GetDefaultPort(DatabaseTypes.SQLServer2005).ToString();

			labelServiceName.Visible = false;
			textBoxServiceName.Visible = false;
		}

		private void SetEnabledForSQLServerCE()
		{
			ResetAllEnabledTo(false);

			comboBoxServers.Items.Clear();
			optDB.Visible = false;
			optDB.Checked = false;
			optDBFile.Visible = true;
			optDBFile.Checked = true;
			textBoxFilename.Visible = true;
			buttonBrowse.Visible = true;
			comboBoxDatabases.Visible = false;
			buttonRefreshDatabases.Visible = false;
			checkBoxDirect.Visible = false;
			labelServer.Visible = false;
			comboBoxServers.Visible = false;
			buttonRefreshServers.Visible = false;

			checkBoxUseIntegratedSecurity.Visible = true;

			labelPort.Visible = false;
			textBoxPort.Visible = false;

			labelServiceName.Visible = false;
			textBoxServiceName.Visible = false;
		}

		private void SetEnabledForSqlServerExpress()
		{
			ResetAllEnabledTo(true);

			checkBoxUseIntegratedSecurity.Checked = true;
			textBoxUserName.Text = "sa";
			comboBoxServers.Items.Clear();
			comboBoxServers.Text = string.Format("{0}\\SQLEXPRESS", Environment.MachineName); ;// ".";

			optDB.Visible = true;
			optDB.Checked = false;
			optDBFile.Visible = true;
			optDBFile.Checked = true;
			textBoxFilename.Visible = true;
			buttonBrowse.Visible = true;
			comboBoxDatabases.Visible = true;
			buttonRefreshDatabases.Visible = true;
			checkBoxDirect.Visible = false;
			labelServer.Visible = true;
			comboBoxServers.Visible = true;
			buttonRefreshServers.Visible = true;

			checkBoxUseIntegratedSecurity.Visible = true;

			labelPort.Visible = true;
			textBoxPort.Visible = true;
			textBoxPort.Text = ConnectionStringHelper.GetDefaultPort(DatabaseTypes.SQLServer2005).ToString();

			labelServiceName.Visible = false;
			textBoxServiceName.Visible = false;
		}

		private void ResetAllEnabledTo(bool enabled)
		{
			//buttonBrowse.Enabled = enabled;
			//buttonRefreshDatabases.Enabled = enabled;
			//buttonRefreshServers.Enabled = enabled;

			//checkBoxUseIntegratedSecurity.Enabled = enabled;

			//comboBoxServers.Enabled = enabled;
			//comboBoxDatabases.Enabled = enabled;

			//optDB.Enabled = enabled;
			//optDBFile.Enabled = enabled;

			//textBoxFilename.Enabled = enabled;
			//textBoxUserName.Enabled = !enabled;
			//textBoxPassword.Enabled = !enabled;
			labelPort.Visible = true;
			textBoxPort.Visible = true;
			groupBoxLogin.Visible = true;
		}

		private bool _isAnyDatabaseTypeSelected;
		private bool IsAnyDatabaseTypeSelected()
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(() => IsAnyDatabaseTypeSelected()));
				return _isAnyDatabaseTypeSelected;
			}

			_isAnyDatabaseTypeSelected = comboBoxDatabaseTypes.SelectedIndex != -1;
			return _isAnyDatabaseTypeSelected;
		}

		private void RefreshServersAsync()
		{
			if (InvalidateServerCache == false) return;

			List<string> servers;

			if (IsAnyDatabaseTypeSelected() == false)
				servers = new List<string>();
			else
				servers = DatabaseHelper.GetServerNames(SelectedDatabaseType).OrderBy(f => f).ToList();

			// If the combo box items collection is empty when it is re-filled while open,
			// it will throw an exception. Add . to the list as it represents the local
			// server, so should always be a valid choice.
			if (servers.Contains(".") == false)
			{
				servers.Add(".");
			}
			FillServerComboBox(servers);

			InvalidateServerCache = false;
		}

		private void RefreshDatabasesAsync()
		{
			//if (SelectedDatabaseType != DatabaseTypes.PostgreSQL)
			//    if (InvalidateDatabaseCache == false) return;

			ClearDatabaseOperationResults();

			List<string> databases;

			SetDatabaseOperationMessage("Fetching databases...");

			try
			{
				if (string.IsNullOrEmpty(SelectedServerName))
					databases = new List<string>();
				else
					databases = DatabaseHelper.GetDatabaseNamesForServer(GetHelper()).OrderBy(f => f).ToList();
			}
			catch (SqlException e)
			{
				SetDatabaseOperationResults(new DatabaseOperationResults("Database Search", false, e.Message));
				return;
			}
			catch (Exception e)
			{
				SetDatabaseOperationResults(new DatabaseOperationResults("Database Search", false, e.Message));
				return;
			}

			// If the combo box items collection is empty when it is re-filled while open,
			// it will throw an exception.
			if (databases.Count == 0)
			{
				databases.Add("");
			}
			FillDatabasesComboBox(databases);

			SetDatabaseOperationResults(new DatabaseOperationResults("Database Search", true));

			if (SelectedDatabaseType != DatabaseTypes.PostgreSQL)
				InvalidateDatabaseCache = false;
		}

		private void FillServerComboBox(IEnumerable<string> servers)
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(() => FillServerComboBox(servers)));
				return;
			}

			ClearComboBox(comboBoxServers, true);
			foreach (var server in servers)
			{
				comboBoxServers.Items.Add(server);
			}
		}

		private static void PopulateComboBox(ComboBox comboBox, IEnumerable<string> items)
		{
			comboBox.SelectedIndex = -1;
			comboBox.Items.Clear();

			foreach (var item in items)
			{
				if (!string.IsNullOrEmpty(item))
					comboBox.Items.Add(item);
			}
			if (comboBox.Items.Count > 0)
				comboBox.SelectedIndex = 0;
		}

		private void FillDatabasesComboBox(IEnumerable<string> databases)
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(() => FillDatabasesComboBox(databases)));
				return;
			}

			ClearComboBox(comboBoxDatabases, true);

			foreach (var database in databases)
			{
				comboBoxDatabases.Items.Add(database);
			}
		}

		private static void ClearComboBox(ComboBox combo, bool clearText)
		{
			BusyClearingDatabaseCombo = true;

			if (clearText)
				combo.Text = "";

			combo.SelectedIndex = -1;
			combo.Items.Clear();

			BusyClearingDatabaseCombo = false;
		}

		private void buttonBrowse_Click(object sender, EventArgs e)
		{
			var openFileDialog = new OpenFileDialog();
			openFileDialog.Multiselect = false;
			openFileDialog.ShowReadOnly = false;
			openFileDialog.CheckFileExists = true;
			openFileDialog.AutoUpgradeEnabled = true;
			openFileDialog.InitialDirectory = Application.ExecutablePath;

			DialogResult result = openFileDialog.ShowDialog(this);

			if (result != DialogResult.OK) return;

			textBoxFilename.Text = openFileDialog.FileName;
		}

		public void Clear()
		{
			//comboBoxServers.Items.Clear();
			comboBoxDatabases.Items.Clear();
		}

		public void StartBulkUpdate()
		{
			Utility.SuspendPainting(this);
		}

		public void EndBulkUpdate()
		{
			Utility.ResumePainting(this);
		}

		private void buttonRefreshServers_Click(object sender, EventArgs e)
		{
			if (worker.IsBusy) return;
			worker.RunWorkerAsync(WorkItem.RefreshServers);
		}

		private void buttonRefreshDatabases_Click(object sender, EventArgs e)
		{
			if (worker.IsBusy) return;

			worker.RunWorkerAsync(WorkItem.RefreshDatabases);
		}

		private void comboBoxDatabases_MouseClick(object sender, MouseEventArgs e)
		{
			if (worker.IsBusy) return;
			if (InvalidateDatabaseCache == false && comboBoxDatabases.Items.Count > 1) return;

			ClearComboBox(comboBoxDatabases, false);
			comboBoxDatabases.Items.Add("Fetching databases...");

			// Wait for the BackgroundWorker to finish the download.
			while (worker.IsBusy)
			{
				// Keep UI messages moving, so the form remains 
				// responsive during the asynchronous operation.
				Application.DoEvents();
			}
			worker.RunWorkerAsync(WorkItem.RefreshDatabases);
		}

		private void textBoxPort_TextChanged(object sender, EventArgs e)
		{
			int val;

			if (!int.TryParse(textBoxPort.Text, out val))
				highlighter1.SetHighlightColor(textBoxPort, DevComponents.DotNetBar.Validator.eHighlightColor.Orange);
			else
				highlighter1.SetHighlightColor(textBoxPort, DevComponents.DotNetBar.Validator.eHighlightColor.None);
		}

		private void comboBoxDatabases_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!comboBoxDatabases.DroppedDown) return;

			if (comboBoxDatabases.SelectedIndex < 0)
			{
				if (!BusyClearingDatabaseCombo)
					MessageBox.Show(this, "Please select a database type", "No database type", MessageBoxButtons.OK, MessageBoxIcon.Warning);

				return;
			}
			if (worker.IsBusy) return;
			if (InvalidateDatabaseCache == false && comboBoxDatabases.Items.Count > 0) return;

			ClearComboBox(comboBoxDatabases, false);
			comboBoxDatabases.Items.Add("Fetching databases...");

			comboBoxDatabases.DroppedDown = false;

			worker.RunWorkerAsync(WorkItem.RefreshDatabases);

			if (string.IsNullOrEmpty(comboBoxDatabases.Text))
				highlighter1.SetHighlightColor(comboBoxDatabases, DevComponents.DotNetBar.Validator.eHighlightColor.Orange);
			else
				highlighter1.SetHighlightColor(comboBoxDatabases, DevComponents.DotNetBar.Validator.eHighlightColor.None);
		}

		private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
		{

		}

		private void ucDatabaseInformation_Load(object sender, EventArgs e)
		{

		}

		private void labelPort_Click(object sender, EventArgs e)
		{

		}

		private void checkBoxDirect_CheckedChanged(object sender, EventArgs e)
		{
			if (SelectedDatabaseType != DatabaseTypes.Oracle)
				return;

			if (checkBoxDirect.Checked)
			{
				labelServer.Text = "Server";
				optDB.Visible = false;
				comboBoxDatabases.Visible = false;
				labelPort.Visible = true;
				textBoxPort.Visible = true;
				labelServiceName.Visible = true;
				textBoxServiceName.Visible = true;
				comboBoxServers.Items.Clear();
				comboBoxServers.Text = "localhost";
			}
			else
			{
				labelServer.Text = "TNS name";
				optDB.Visible = false;
				comboBoxDatabases.Visible = false;
				labelPort.Visible = false;
				textBoxPort.Visible = false;
				labelServiceName.Visible = false;
				textBoxServiceName.Visible = false;

				comboBoxServers.Items.Clear();
				HashSet<string> dbs = OracleHelper.GetOracleInstances();

				foreach (string db in dbs)
					comboBoxServers.Items.Add(db);

				if (dbs.Count > 0)
					comboBoxServers.SelectedIndex = 0;
				else
					comboBoxServers.Text = "";
			}
		}

		/// <summary>
		/// Theses two event handlers are for a feature we got halfway through - clicking
		/// on the drop down button and having the lists auto-refresh if needed. I was having
		/// an issue with the combobox, where modifying the drop down list while it was open
		/// caused it to blow up.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comboBoxServers_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!comboBoxServers.DroppedDown) return;
			if (worker.IsBusy) return;
			if (InvalidateServerCache == false) return;

			ClearComboBox(comboBoxServers, false);
			comboBoxServers.Items.Add("Fetching servers...");

			comboBoxServers.DroppedDown = false;

			while (worker.IsBusy)
				System.Threading.Thread.Sleep(50);

			worker.RunWorkerAsync(WorkItem.RefreshServers);
		}

		private void textBoxUserName_TextChanged(object sender, EventArgs e)
		{
			SelectedDatabaseChanged.RaiseEventEx(this);
		}

		private void textBoxPassword_TextChanged(object sender, EventArgs e)
		{
			SelectedDatabaseChanged.RaiseEventEx(this);
		}
	}
}
