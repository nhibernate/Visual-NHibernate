using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ArchAngel.Interfaces.Scripting.NHibernate.Model;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.UI.Presenters;
using Slyce.Common;
using Slyce.Common.EventExtensions;

namespace ArchAngel.Providers.EntityModel.UI.PropertyGrids
{
	public partial class FormDatabase : UserControl, IEventSender //IDatabaseForm, IEventSender
	{
		//public event EventHandler TestConnection;
		//public event EventHandler RefreshSchema;
		public delegate void DatabaseSchemaChangedDelegate(DatabaseMergeResult mergeResult, IDatabase db1, IDatabase db2);
		public delegate void DatabaseCreatedDelegate(ArchAngel.Providers.EntityModel.Model.MappingLayer.MappingSet mappingSet);
		public event DatabaseSchemaChangedDelegate DatabaseSchemaChanged;
		public event DatabaseCreatedDelegate NewDatabaseCreated;

		public event EventHandler UsernameChanged;
		public event EventHandler PasswordChanged;
		public event EventHandler SelectedDatabaseChanged;
		public event EventHandler SelectedDatabaseTypeChanged;
		public event EventHandler ServerNameChanged;
		public int MaxWidth = 0;
		private readonly AutoResetEvent databaseLock = new AutoResetEvent(true);
		internal static bool RefreshingSchema = false;
		private IDatabase _Database = null;
		private ArchAngel.Providers.EntityModel.Model.MappingLayer.MappingSet MappingSet;
		public static FormDatabase Instance;

		public bool EventRaisingDisabled { get; set; }

		public FormDatabase(IDatabase database, ArchAngel.Providers.EntityModel.Model.MappingLayer.MappingSet mappingSet)
		{
			InitializeComponent();

			Instance = this;

			ArchAngel.Interfaces.SharedData.AboutToSave += new EventHandler(SharedData_AboutToSave);
			modelChanges1.RefreshCalled += new EventHandler(RefreshWasCalled);

			labelTablePrefixes.Top = ucDatabaseInformation1.BottomOfControls + 20;
			textBoxTablePrefixes.Top = labelTablePrefixes.Top;
			labelColumnPrefixes.Top = textBoxTablePrefixes.Bottom + 5;
			textBoxColumnPrefixes.Top = labelColumnPrefixes.Top;

			labelTableSuffixes.Top = textBoxColumnPrefixes.Bottom + 5;
			textBoxTableSuffixes.Top = labelTableSuffixes.Top;
			labelColumnSuffixes.Top = textBoxTableSuffixes.Bottom + 5;
			textBoxColumnSuffixes.Top = labelColumnSuffixes.Top;

			buttonResync.Top = textBoxColumnSuffixes.Bottom + 20;
			buttonTestConnection.Top = buttonResync.Top;

			if (DesignMode)
				return;

			superTabConnectionSettings.SelectedTab = superTabItemConnection;
			MappingSet = mappingSet;
			ucDatabaseInformation1.UsernameChanged += (sender, e) => UsernameChanged.RaiseEvent(sender, e);
			ucDatabaseInformation1.PasswordChanged += (sender, e) => PasswordChanged.RaiseEvent(sender, e);
			ucDatabaseInformation1.SelectedDatabaseChanged += (sender, e) => SelectedDatabaseChanged.RaiseEvent(sender, e);
			ucDatabaseInformation1.SelectedDatabaseTypeChanged += (sender, e) => SelectedDatabaseTypeChanged.RaiseEvent(sender, e);
			ucDatabaseInformation1.ServerNameChanged += (sender, e) => ServerNameChanged.RaiseEvent(sender, e);

			Database = database;

			if (Database != null)
				labelHeader.Text = Database.Name;
			else
				labelHeader.Text = "No Database Selected";

			PopulatePrefixes();

			buttonImport.Enabled = false;
		}

		private void RefreshWasCalled(object sender, EventArgs e)
		{
			Refresh();
		}

		void SharedData_AboutToSave(object sender, EventArgs e)
		{
			Save();
		}

		private void PopulatePrefixes()
		{
			string prefixes = "";

			#region Table Prefixes
			foreach (var prefix in Database.MappingSet.TablePrefixes.Where(p => p.Trim().Length > 0))
				prefixes += prefix + ", ";

			textBoxTablePrefixes.Text = prefixes.TrimEnd(' ', ',');
			#endregion

			#region Column Prefixes
			prefixes = "";

			foreach (var prefix in Database.MappingSet.ColumnPrefixes.Where(p => p.Trim().Length > 0))
				prefixes += prefix + ", ";

			textBoxColumnPrefixes.Text = prefixes.TrimEnd(' ', ',');
			#endregion

			#region Table Suffixes
			prefixes = "";

			foreach (var prefix in Database.MappingSet.TableSuffixes.Where(p => p.Trim().Length > 0))
				prefixes += prefix + ", ";

			textBoxTableSuffixes.Text = prefixes.TrimEnd(' ', ',');
			#endregion

			#region Column Suffixes
			prefixes = "";

			foreach (var prefix in Database.MappingSet.ColumnSuffixes.Where(p => p.Trim().Length > 0))
				prefixes += prefix + ", ";

			textBoxColumnSuffixes.Text = prefixes.TrimEnd(' ', ',');
			#endregion
		}

		public IDatabase Database
		{
			get { return _Database; }
			set
			{
				if (_Database != value)
				{
					_Database = value;

					if (_Database.Loader != null)
					{
						ConnectionStringHelper connStringHelper = SetConnectionString(_Database);

						ucDatabaseInformation1.SetDatabase(_Database.Name);
						ucDatabaseInformation1.SetDatabaseFilename(connStringHelper.FileName);
						ucDatabaseInformation1.UseIntegratedSecurity = connStringHelper.UseIntegratedSecurity;
						ucDatabaseInformation1.Username = connStringHelper.UserName;
						ucDatabaseInformation1.Password = connStringHelper.Password;
						ucDatabaseInformation1.SetPort(connStringHelper.Port);
						ucDatabaseInformation1.Direct = connStringHelper.Direct;
						ucDatabaseInformation1.ServiceName = connStringHelper.ServiceName;
						ucDatabaseInformation1.SetServersNames(new string[] { connStringHelper.ServerName });
					}
					DatabaseHelper = new ServerAndDatabaseHelper();
				}
			}
		}

		private ConnectionStringHelper SetConnectionString(IDatabase database)
		{
			ConnectionStringHelper connStringHelper = null;

			if (database.Loader.DatabaseConnector is SQLServer2005DatabaseConnector)
			{
				SQLServer2005DatabaseConnector sqlConn = (SQLServer2005DatabaseConnector)database.Loader.DatabaseConnector;
				connStringHelper = sqlConn.ConnectionInformation;
				ucDatabaseInformation1.SelectedDatabaseType = DatabaseTypes.SQLServer2005;
			}
			else if (database.Loader.DatabaseConnector is SQLServerExpressDatabaseConnector)
			{
				SQLServerExpressDatabaseConnector sqlConn = (SQLServerExpressDatabaseConnector)database.Loader.DatabaseConnector;
				connStringHelper = sqlConn.ConnectionInformation;
				ucDatabaseInformation1.SelectedDatabaseType = DatabaseTypes.SQLServerExpress;
			}
			else if (database.Loader.DatabaseConnector is SQLCEDatabaseConnector)
			{
				SQLCEDatabaseConnector sqlConn = (SQLCEDatabaseConnector)database.Loader.DatabaseConnector;
				connStringHelper = sqlConn.ConnectionInformation;
				ucDatabaseInformation1.SelectedDatabaseType = DatabaseTypes.SQLCE;
			}
			else if (database.Loader.DatabaseConnector is MySQLDatabaseConnector)
			{
				MySQLDatabaseConnector sqlConn = (MySQLDatabaseConnector)database.Loader.DatabaseConnector;
				connStringHelper = sqlConn.ConnectionInformation;
				ucDatabaseInformation1.SelectedDatabaseType = DatabaseTypes.MySQL;
			}
			else if (database.Loader.DatabaseConnector is OracleDatabaseConnector)
			{
				OracleDatabaseConnector sqlConn = (OracleDatabaseConnector)database.Loader.DatabaseConnector;
				connStringHelper = sqlConn.ConnectionInformation;
				ucDatabaseInformation1.SelectedDatabaseType = DatabaseTypes.Oracle;
			}
			else if (database.Loader.DatabaseConnector is PostgreSQLDatabaseConnector)
			{
				PostgreSQLDatabaseConnector sqlConn = (PostgreSQLDatabaseConnector)database.Loader.DatabaseConnector;
				connStringHelper = sqlConn.ConnectionInformation;
				ucDatabaseInformation1.SelectedDatabaseType = DatabaseTypes.PostgreSQL;
			}
			else if (database.Loader.DatabaseConnector is FirebirdDatabaseConnector)
			{
				FirebirdDatabaseConnector sqlConn = (FirebirdDatabaseConnector)database.Loader.DatabaseConnector;
				connStringHelper = sqlConn.ConnectionInformation;
				ucDatabaseInformation1.SelectedDatabaseType = DatabaseTypes.Firebird;
			}
			else if (database.Loader.DatabaseConnector is SQLiteDatabaseConnector)
			{
				SQLiteDatabaseConnector sqlConn = (SQLiteDatabaseConnector)database.Loader.DatabaseConnector;
				connStringHelper = sqlConn.ConnectionInformation;
				ucDatabaseInformation1.SelectedDatabaseType = DatabaseTypes.SQLite;
			}
			else
				throw new NotImplementedException("Database type not handled yet: " + database.Loader.DatabaseConnector.GetType().Name);

			return connStringHelper;
		}

		private void ShowMessageBox(string caption, string message, MessageBoxButtons button, MessageBoxIcon icon)
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(() => ShowMessageBox(caption, message, button, icon)));
				return;
			}
			//labelNoChanges.Visible = true;
			MessageBox.Show(this, message, caption, button, icon);
		}

		private void ShowNoChangesLabel()
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(() => ShowNoChangesLabel()));
				return;
			}
			labelNoChanges.Visible = true;
			labelNoChangesExport.Visible = true;
		}

		private void SetControlEnabled(Control control, bool enabled)
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(() => SetControlEnabled(control, enabled)));
				return;
			}
			control.Enabled = enabled;
		}

		private void SetCursor(Cursor cursor)
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(() => SetCursor(cursor)));
				return;
			}
			Cursor = cursor;
		}

		public void SetDatabaseOperationResults(DatabaseOperationResults results)
		{
			ucDatabaseInformation1.SetDatabaseOperationResults(results);
		}

		//public void SetServersNames(IEnumerable<string> value)
		//{
		//    ucDatabaseInformation1.SetServersNames(value);
		//}

		//public DatabaseTypes SelectedDatabaseType
		//{
		//    get { return ucDatabaseInformation1.SelectedDatabaseType; }
		//    set { ucDatabaseInformation1.SelectedDatabaseType = value; }
		//}

		public IEnumerable<string> DatabaseNames
		{
			get { return ucDatabaseInformation1.DatabaseNames; }
			set { ucDatabaseInformation1.DatabaseNames = value; }
		}

		/// <summary>
		/// Is either a database or a database file, depending on which
		/// option is selected in the UI.
		/// </summary>
		public string SelectedDatabase
		{
			get { return ucDatabaseInformation1.SelectedDatabase; }
		}

		public ConnectionStringHelper ConnectionStringHelper
		{
			get { return ucDatabaseInformation1.ConnectionStringHelper; }
		}

		//public bool UseIntegratedSecurity
		//{
		//    get { return ucDatabaseInformation1.UseIntegratedSecurity; }
		//    set { ucDatabaseInformation1.UseIntegratedSecurity = value; }
		//}

		public IServerAndDatabaseHelper DatabaseHelper
		{
			get { return ucDatabaseInformation1.DatabaseHelper; }
			set { ucDatabaseInformation1.DatabaseHelper = value; }
		}

		//public void SetDatabaseFilename(string newDatabaseFilename)
		//{
		//    ucDatabaseInformation1.SetDatabaseFilename(newDatabaseFilename);
		//}

		public void SelectDatabase()
		{
			ucDatabaseInformation1.SelectDatabase();
		}

		public void SelectDatabaseFile()
		{
			ucDatabaseInformation1.SelectDatabaseFile();
		}

		//public void SetDatabase(string database)
		//{
		//    ucDatabaseInformation1.SetDatabase(database);
		//}

		public string SelectedServerName
		{
			get { return ucDatabaseInformation1.SelectedServerName; }
			set { ucDatabaseInformation1.SelectedServerName = value; }
		}

		public bool UsingDatabaseFile
		{
			get { return ucDatabaseInformation1.UsingDatabaseFile; }
			set { ucDatabaseInformation1.UsingDatabaseFile = value; }
		}

		//public string Username
		//{
		//    get { return ucDatabaseInformation1.Username; }
		//    set { ucDatabaseInformation1.Username = value; }
		//}

		public string Password
		{
			get { return ucDatabaseInformation1.Password; }
			set { ucDatabaseInformation1.Password = value; }
		}

		public void Clear()
		{
			ucDatabaseInformation1.Clear();
		}

		public void StartBulkUpdate()
		{
			ucDatabaseInformation1.StartBulkUpdate();
		}

		public void EndBulkUpdate()
		{
			ucDatabaseInformation1.EndBulkUpdate();
		}

		private bool TestConnection(bool showSuccess)
		{
			Monitor.Enter(databaseLock);


			try
			{
				IDatabaseLoader loader = CreateDatabaseLoader();
				loader.TestConnection();

				if (showSuccess)
					ShowMessageBox("Success", "Connection succeeded.", MessageBoxButtons.OK, MessageBoxIcon.Information);
				//MessageBox.Show(this, "Connection succeeded.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

				return true;
			}
			catch (DatabaseLoaderException e)
			{
				//if (showSuccess)
				ShowMessageBox("Failure", e.ActualMessage, MessageBoxButtons.OK, MessageBoxIcon.Error);
				//MessageBox.Show(this, e.ActualMessage, "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);

				return false;
			}
			finally
			{
				Monitor.Exit(databaseLock);
			}
		}

		private void RefreshSchema()
		{
			// Note: this is only ok because this method only runs on the UI thread.
			// Two instances of it will not run at once, so this is not a race condition.
			// The moment it can run from another thread, this assumption is false and the
			// code is incorrect.
			if (RefreshingSchema)
				return;

			Cursor = Cursors.WaitCursor;
			databaseLock.WaitOne();
			RefreshingSchema = true;
			IDatabaseLoader loader = CreateDatabaseLoader();

			// Test connection first
			if (TestConnection(false) == false)
			{
				databaseLock.Set();
				RefreshingSchema = false;
				Cursor = Cursors.Default;
				return;
			}
			labelNoChanges.Visible = false;
			labelNoChangesExport.Visible = false;

			Thread thread = new Thread(
				() =>
				{
					try
					{
						List<string> schemasToLimit = Database.Tables.Union(Database.Views).Select(t => t.Schema).Distinct().ToList();
						IDatabase newDb = loader.LoadDatabase(loader.DatabaseObjectsToFetch, schemasToLimit);
						new DatabaseProcessor().CreateRelationships(newDb);

						if (Database.Name == "New Database" && Database.Tables.Count == 0)
						{
							var mappingSet = new EntityModel.Controller.MappingLayer.MappingProcessor(new EntityModel.Controller.MappingLayer.OneToOneEntityProcessor())
								.CreateOneToOneMapping(newDb, Database.MappingSet.TablePrefixes, Database.MappingSet.ColumnPrefixes, Database.MappingSet.TableSuffixes, Database.MappingSet.ColumnSuffixes);

							MappingSet = mappingSet;
							_Database = newDb;

							if (NewDatabaseCreated != null)
								NewDatabaseCreated(mappingSet);
						}
						else
						{
							var result = new DatabaseProcessor().MergeDatabases(Database, newDb);

							if (result.AnyChanges)
							{
								databaseChanges1.Fill(result, Database, newDb);
								modelChanges1.Fill(result, Database, newDb);
								SetControlEnabled(buttonImport, true);
							}
							else
								ShowNoChangesLabel();
						}
					}
					finally
					{
						databaseLock.Set();
						RefreshingSchema = false;
						SetCursor(Cursors.Default);
					}
				});
			thread.Start();
		}

		public IDatabaseLoader CreateDatabaseLoader()
		{
			IDatabaseLoader loader;

			switch (ucDatabaseInformation1.SelectedDatabaseType)
			{
				case DatabaseTypes.SQLCE:
					loader = DatabaseLoaderFacade.GetSQLCELoader(SelectedDatabase);
					break;
				case DatabaseTypes.SQLServer2005:
					loader = DatabaseLoaderFacade.GetSQLServer2005Loader(ConnectionStringHelper);
					break;
				case DatabaseTypes.SQLServerExpress:
					loader = DatabaseLoaderFacade.GetSqlServerExpressLoader(ConnectionStringHelper, SelectedDatabase);
					break;
				case DatabaseTypes.MySQL:
					loader = DatabaseLoaderFacade.GetMySQLLoader(ConnectionStringHelper);
					break;
				case DatabaseTypes.Oracle:
					loader = DatabaseLoaderFacade.GetOracleLoader(ConnectionStringHelper, SelectedDatabase);
					break;
				case DatabaseTypes.PostgreSQL:
					loader = DatabaseLoaderFacade.GetPostgreSQLLoader(ConnectionStringHelper, SelectedDatabase);
					break;
				case DatabaseTypes.Firebird:
					loader = DatabaseLoaderFacade.GetFirebirdLoader(ConnectionStringHelper);
					break;
				case DatabaseTypes.SQLite:
					loader = DatabaseLoaderFacade.GetSQLiteLoader(ConnectionStringHelper);
					break;
				default:
					throw new NotImplementedException(
						string.Format("DatabasePresenter.CreateDatabaseLoader() does not support databases of type {0} yet",
						ucDatabaseInformation1.SelectedDatabaseType));
			}
			return loader;
		}

		private void ucDatabaseInformation1_Resize(object sender, EventArgs e)
		{

		}

		private void FormDatabase_Resize(object sender, EventArgs e)
		{
			ResizeControls();
		}

		private void ResizeControls()
		{
			//Slyce.Common.Utility.SuspendPainting(this);

			//if (MaxWidth == 0 || this.Width < MaxWidth)
			//{
			//    if (ucDatabaseInformation1.Dock != DockStyle.Fill)
			//        ucDatabaseInformation1.Dock = DockStyle.Fill;
			//}
			//else
			//{
			//    if (ucDatabaseInformation1.Dock != DockStyle.None)
			//        ucDatabaseInformation1.Dock = DockStyle.None;

			//    if (ucDatabaseInformation1.Width != MaxWidth)
			//        ucDatabaseInformation1.Width = MaxWidth;

			//    int left = (this.ClientSize.Width - ucDatabaseInformation1.Width) / 2;

			//    if (ucDatabaseInformation1.Left != left)
			//        ucDatabaseInformation1.Left = left;
			//}
			//Slyce.Common.Utility.ResumePainting(this);
		}

		private void FormDatabase_ParentChanged(object sender, EventArgs e)
		{
			if (this.Parent == null)
				Save();
			else
				SetHeaderText();
		}

		private void Save()
		{
			if (_Database.ConnectionInformation == null)
			{
				IDatabaseLoader dbLoader = DatabasePresenter.CreateDatabaseLoader(ucDatabaseInformation1);

				try
				{
					_Database = dbLoader.LoadDatabase(dbLoader.DatabaseObjectsToFetch, null);
				}
				catch (DatabaseLoaderException e)
				{
					#region Save what we can
					_Database.DatabaseType = ucDatabaseInformation1.SelectedDatabaseType;

					_Database.ConnectionInformation = new Helper.ConnectionStringHelper();
					_Database.ConnectionInformation.FileName = ucDatabaseInformation1.ConnectionStringHelper.FileName;
					_Database.ConnectionInformation.UseIntegratedSecurity = ucDatabaseInformation1.ConnectionStringHelper.UseIntegratedSecurity;
					_Database.ConnectionInformation.UserName = ucDatabaseInformation1.ConnectionStringHelper.UserName;
					_Database.ConnectionInformation.Password = ucDatabaseInformation1.ConnectionStringHelper.Password;
					_Database.ConnectionInformation.Port = ucDatabaseInformation1.ConnectionStringHelper.Port;
					_Database.ConnectionInformation.ServerName = ucDatabaseInformation1.ConnectionStringHelper.ServerName;
					_Database.ConnectionInformation.ServiceName = ucDatabaseInformation1.ConnectionStringHelper.ServiceName;
					#endregion

					//if (e.InnerException != null && e.InnerException.Message.StartsWith("Database cannot be null, the empty string, or string of only whitespace."))
					//    return;
					//else
					//    throw;
				}
			}
			else
			{
				//_Database.Name = ucDatabaseInformation1.ConnectionStringHelper.DatabaseName;
				_Database.DatabaseType = ucDatabaseInformation1.SelectedDatabaseType;
				var connStringHelper = ucDatabaseInformation1.ConnectionStringHelper;

				_Database.ConnectionInformation.FileName = connStringHelper.FileName;
				_Database.ConnectionInformation.UseIntegratedSecurity = connStringHelper.UseIntegratedSecurity;
				_Database.ConnectionInformation.UserName = connStringHelper.UserName;
				_Database.ConnectionInformation.Password = connStringHelper.Password;
				_Database.ConnectionInformation.Port = connStringHelper.Port;
				_Database.ConnectionInformation.ServerName = connStringHelper.ServerName;
				_Database.ConnectionInformation.ServiceName = connStringHelper.ServiceName;
			}
		}

		private void buttonTestConnection_Click(object sender, EventArgs e)
		{
			TestConnection(true);
		}

		private void buttonRefresh_Click(object sender, EventArgs e)
		{
			Refresh();
		}

		private void Refresh()
		{
			buttonImport.Enabled = false;

			if (TestConnection(false))
				RefreshSchema();
		}

		private void buttonImport_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;
			ArchAngel.Providers.EntityModel.UI.EditModel.BusyPopulating = true;
			databaseChanges1.AcceptChanges();
			EditModel.SortNodes();
			ArchAngel.Providers.EntityModel.UI.EditModel.BusyPopulating = false;
			buttonImport.Enabled = false;
			Cursor = Cursors.Default;
			MessageBox.Show(this, "The changes have been imported and applied.", "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void buttonResync_Click(object sender, EventArgs e)
		{
			superTabConnectionSettings.SelectedTab = superTabItemImport;
			superTabConnectionSettings.Refresh();
			Refresh();
		}

		private void superTabControl1_SelectedTabChanged(object sender, DevComponents.DotNetBar.SuperTabStripSelectedTabChangedEventArgs e)
		{
			SetHeaderText();
		}

		private void SetHeaderText()
		{
			if (Database == null)
			{
				labelHeader.Text = "No Database Selected";
				return;
			}
			if (superTabConnectionSettings.SelectedTab == superTabItemConnection)
				labelHeader.Text = string.Format("{0} - Connection Settings", Database.Name);
			else if (superTabConnectionSettings.SelectedTab == superTabItemImport)
				labelHeader.Text = string.Format("{0} - Import Changes", Database.Name);
			else if (superTabConnectionSettings.SelectedTab == superTabItemExport)
				labelHeader.Text = string.Format("{0} - Export Changes", Database.Name);
			else
				throw new NotImplementedException("Tab not handled yet: " + superTabConnectionSettings.SelectedTab.Name);
		}

		private void buttonRefreshExport_Click(object sender, EventArgs e)
		{
			Refresh();
		}

		private void textBoxTablePrefixes_TextChanged(object sender, EventArgs e)
		{
			Database.MappingSet.TablePrefixes.Clear();
			Database.MappingSet.TablePrefixes.AddRange(textBoxTablePrefixes.Text.Split(','));
		}

		private void textBoxColumnPrefixes_TextChanged(object sender, EventArgs e)
		{
			Database.MappingSet.ColumnPrefixes.Clear();
			Database.MappingSet.ColumnPrefixes.AddRange(textBoxColumnPrefixes.Text.Split(','));
		}

		private void textBoxTableSuffixes_TextChanged(object sender, EventArgs e)
		{
			Database.MappingSet.TableSuffixes.Clear();
			Database.MappingSet.TableSuffixes.AddRange(textBoxTableSuffixes.Text.Split(','));
		}

		private void textBoxColumnSuffixes_TextChanged(object sender, EventArgs e)
		{
			Database.MappingSet.ColumnSuffixes.Clear();
			Database.MappingSet.ColumnSuffixes.AddRange(textBoxColumnSuffixes.Text.Split(','));
		}

		private void buttonGenerateCreateScript_Click(object sender, EventArgs e)
		{
			//CreateEntityFiles();
			//var config = new Configuration();
			//IDictionary props = new Hashtable();

			//props["hibernate.connection.provider"] = "NHibernate.Connection.DriverConnectionProvider";
			//props["hibernate.dialect"] = "NHibernate.Dialect.MsSql2000Dialect";
			//props["hibernate.connection.driver_class"] = "NHibernate.Driver.SqlClientDriver";
			//props["hibernate.connection.connection_string"] = "Server=localhost;initial catalog=Northwind;Integrated Security=SSPI";

			//foreach (DictionaryEntry de in props)
			//    config.SetProperty(de.Key.ToString(), de.Value.ToString());

			//config.AddAssembly("nhibernator");

			//config.Configure(GetConfigXml());
			List<string> hbmFiles;
			string ass = CompileAssembly(out hbmFiles);
			//config.SetDefaultAssembly(ass);
			System.Reflection.Assembly a = System.Reflection.Assembly.LoadFrom(ass);
			//config.AddAssembly(a);

			//string assemblyName = ass.FullName.Substring(0, ass.FullName.IndexOf(","));

			//foreach (var hbmFile in CreateHbmFiles(assemblyName))
			//    config.AddXmlFile(hbmFile);

			//var export = new NHibernate.Tool.hbm2ddl.SchemaExport(config);
			//export.SetOutputFile(@"C:\test.txt");
			//export.Execute(true, false, false);

			var configPath = Path.Combine(ArchAngel.Interfaces.SharedData.CurrentProject.TempFolder, "nhibernate.config.xml");
			File.WriteAllText(configPath, GetConfigXml());
			var outputFile = Path.Combine(ArchAngel.Interfaces.SharedData.CurrentProject.TempFolder, "test.txt");

			Type runner = a.GetType(a.FullName.Substring(0, a.FullName.IndexOf(",")) + ".Runner");
			var method = runner.GetMethod("ExportSchema");
			method.Invoke(null, new object[] { configPath, outputFile, hbmFiles });
		}

		private string GetConfigXml()
		{
			string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
						<hibernate-configuration xmlns=""urn:nhibernate-configuration-2.2"">
							<session-factory>
								<property name=""connection.provider"">NHibernate.Connection.DriverConnectionProvider</property>
								<property name=""dialect"">NHibernate.Dialect.MsSql2005Dialect</property>
								<property name=""connection.driver_class"">NHibernate.Driver.SqlClientDriver</property>
								<property name=""connection.connection_string"">Server=WIN7-PC;Database=Northwind;Trusted_Connection=True</property>
								<property name=""proxyfactory.factory_class"">NHibernate.ByteCode.Castle.ProxyFactoryFactory, NHibernate.ByteCode.Castle</property>
								<property name=""cache.use_minimal_puts"">false</property>
								<property name=""use_outer_join"">false</property>
							</session-factory>
						</hibernate-configuration>";
			return xml;
		}

		private List<string> CreateHbmFiles(string assemblyName)
		{
			var x = AppDomain.CurrentDomain.GetAssemblies().Single(a => a.FullName.StartsWith("ArchAngel.NHibernateHelper"));
			var utilityType = x.GetType("ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Utility");
			var method = utilityType.GetMethod("CreateMappingXMLFrom");
			List<string> files = new List<string>();

			var provider = (ProviderInfo)ArchAngel.Interfaces.SharedData.CurrentProject.Providers.Single(p => p.Name == "Entity Provider");

			foreach (var entity in provider.MappingSet.EntitySet.Entities)
			{
				// Parameters
				string entityNamespace = assemblyName.Split('.')[0];
				bool autoImport = false;
				ArchAngel.Interfaces.NHibernateEnums.TopLevelAccessTypes defaultAccess = Interfaces.NHibernateEnums.TopLevelAccessTypes.property;
				ArchAngel.Interfaces.NHibernateEnums.TopLevelCascadeTypes defaultCascade = Interfaces.NHibernateEnums.TopLevelCascadeTypes.all;
				bool defaultLazy = true;

				string xml = (string)method.Invoke(null, new object[] { entity, assemblyName, entityNamespace, autoImport, defaultAccess, defaultCascade, defaultLazy });
				//files.Add(xml);
				//xml = xml.Replace(string.Format(@"assembly=""{0}""", assemblyName), "");
				string filename = Path.Combine(ArchAngel.Interfaces.SharedData.CurrentProject.TempFolder, Path.GetFileNameWithoutExtension(Path.GetTempFileName()) + ".hbm.xml");

				File.WriteAllText(filename, xml, new System.Text.UTF8Encoding(false));
				files.Add(filename);
			}
			return files;
		}

		private string CompileAssembly(out List<string> hbmFiles)
		{
			string assemblyName = Path.GetFileNameWithoutExtension(Path.GetTempFileName());
			List<Slyce.Common.Scripter.FileToCompile> entityClassFiles = CreateEntityFiles(assemblyName);
			string assemblyPath = Path.Combine(ArchAngel.Interfaces.SharedData.CurrentProject.TempFolder, assemblyName + ".dll");
			hbmFiles = CreateHbmFiles(assemblyName);

			string configFile = Path.Combine(ArchAngel.Interfaces.SharedData.CurrentProject.TempFolder, "hibernate.cfg.xml");
			File.WriteAllText(configFile, GetConfigXml());
			//hbmFiles.Add(configFile);
			List<System.CodeDom.Compiler.CompilerError> errors;
			List<string> referencedAssemblies = new List<string>();
#if DEBUG
			referencedAssemblies.Add("NHibernate.dll");
#else
			referencedAssemblies.Add("NHibernate.dll");
#endif
			entityClassFiles.Add(GetRunnerClass(assemblyName));

			if (!Directory.Exists(@"D:\deleteme\")) Directory.Delete(@"D:\deleteme\");
			if (!Directory.Exists(@"D:\deleteme\")) Directory.CreateDirectory(@"D:\deleteme\");
			if (!Directory.Exists(@"D:\deleteme\Model\")) Directory.CreateDirectory(@"D:\deleteme\Model\");
			if (!Directory.Exists(@"D:\deleteme\Model\Mappings")) Directory.CreateDirectory(@"D:\deleteme\Model\Mappings");

			foreach (var file in entityClassFiles)
				File.WriteAllText(@"D:\deleteme\Model\" + file.Name + ".cs", file.Code);

			int i = 0;

			foreach (var file in hbmFiles)
				File.Copy(file, @"D:\deleteme\Model\Mappings\" + Path.GetFileName(file));

			System.Reflection.Assembly ass = Slyce.Common.Scripter.CompileCode(entityClassFiles, referencedAssemblies, hbmFiles, out errors, assemblyPath);

			if (errors.Count > 0)
				throw new Exception("Error compiling temp DLL.");

			return assemblyPath;
		}

		private Scripter.FileToCompile GetRunnerClass(string assemblyName)
		{
			string code = string.Format(@"
using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace {0}
{{
	public class Runner
	{{
		public static void ExportSchema(string configFilePath, string outputFile, List<string> hbmFiles)
		{{
			System.AppDomain.CurrentDomain.AssemblyResolve += new System.ResolveEventHandler(CurrentDomain_AssemblyResolve);

			var ass = System.Reflection.Assembly.GetExecutingAssembly();

			var config = new Configuration();
			config.Configure(configFilePath);
			//config.Configure();
			config.AddAssembly(ass);

			//foreach (var f in hbmFiles)
			//	config.AddXmlFile(f);

			var export = new SchemaExport(config);
			export.SetOutputFile(outputFile);
			export.Execute(true, false, false);
		}}

		private static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, System.ResolveEventArgs args)
		{{
			string ggg = args.Name;
			throw new System.NotImplementedException(""GFH hahaha"");
		}}
	}}
}}", assemblyName);

			return new Scripter.FileToCompile("Runner", code, "Runner");
		}

		private List<Slyce.Common.Scripter.FileToCompile> CreateEntityFiles(string assemblyName)
		{
			List<Slyce.Common.Scripter.FileToCompile> files = new List<Slyce.Common.Scripter.FileToCompile>();
			ArchAngel.Interfaces.SharedData.CurrentProject.InitialiseScriptObjects();
			var Project = ArchAngel.Interfaces.SharedData.CurrentProject.ScriptProject;

			foreach (var entity in Project.Entities)
			{
				StringBuilder code = new StringBuilder(@"
using System;
using System.Collections.Generic;

namespace " + assemblyName + @"
{
	[Serializable]
	public ");

				if (entity.IsAbstract) code.Append("abstract ");

				code.Append("class " + entity.Name);

				if (entity.IsInherited) code.Append(" : " + entity.ParentName);

				code.Append(@"
	{
		public " + entity.Name + @"()
		{");

				foreach (var reference in entity.References)
				{
					if (reference.ReferenceType == ReferenceTypes.ManyToMany ||
						reference.ReferenceType == ReferenceTypes.ManyToOne)
					{
						code.Append(System.Environment.NewLine + reference.Name + @" = new List<" + reference.ToEntity.Name + @">();");
					}
				}
				code.Append(System.Environment.NewLine + "}");

				/* START Property Definitions */
				foreach (var property in entity.Properties.Where(p => !p.IsInherited))
					code.Append(System.Environment.NewLine + @"public virtual " + property.Type + " " + property.Name + " { get; set; }");
				/* END Property Definitions */

				/* Start Reference Definitions */
				foreach (var reference in entity.References)
					code.Append(System.Environment.NewLine + "public virtual " + reference.Type.Replace("ISet", "List").Replace("IList", "List").Replace("IDictionary", "List") + " " + reference.Name + "{ get; set; }");
				/* END Reference Definitions */

				/* START Component Definitions */
				foreach (var component in entity.Components)
					code.Append(System.Environment.NewLine + "public virtual " + component.Type + " " + component.Name + "{ get; set; }");
				/* END Component Definitions */

				code.Append(@"
							public override bool Equals(object obj)
							{{
								return false;
							}}

							[[System.Security.SecurityCritical]]
							public override int GetHashCode()
							{{
								return 0;
							}}");
				code.Append(System.Environment.NewLine + "}" + System.Environment.NewLine + "}");
				files.Add(new Scripter.FileToCompile(entity.Name, code.ToString(), entity.Name));
			}
			return files;
		}
	}
}
