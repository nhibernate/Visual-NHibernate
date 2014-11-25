using System;
using System.Collections.Generic;
using System.Threading;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;
using Slyce.Common.EventExtensions;

namespace ArchAngel.Providers.EntityModel.UI.Presenters
{
	public class DatabasePresenter : PresenterBase
	{
		private readonly IDatabaseForm form;
		private readonly AutoResetEvent databaseLock = new AutoResetEvent(true);
		private bool RefreshingSchema;
		public IDatabaseConnector DatabaseConnector { get; set; }
		public IDatabase Database { get; set; }
		public event EventHandler NewDatabaseCreated;
		public event EventHandler SchemaRefreshed;

		public DatabasePresenter(IMainPanel mainPanel, IDatabaseForm form)
			: base(mainPanel)
		{
			this.form = form;

			SetupForm();
			//form.TestConnection += (sender, e) => TestConnection(true);
			//form.RefreshSchema += (sender, e) => RefreshSchema();
			form.DatabaseHelper = new ServerAndDatabaseHelper();
			form.SelectedDatabaseTypeChanged += form_SelectedDatabaseTypeChanged;
		}

		void form_SelectedDatabaseTypeChanged(object sender, EventArgs e)
		{
			DatabaseTypes dbType = form.SelectedDatabaseType;
			switch (dbType)
			{
				//case DatabaseTypes.SQLServer2000:
				case DatabaseTypes.SQLServer2005:
				case DatabaseTypes.MySQL:
				case DatabaseTypes.Oracle:
				case DatabaseTypes.PostgreSQL:
					form.SelectDatabase();
					break;
				case DatabaseTypes.SQLCE:
				case DatabaseTypes.SQLServerExpress:
				case DatabaseTypes.SQLite:
					form.SelectDatabaseFile();
					break;
				default:
					throw new NotImplementedException("Database type not handled yet: " + dbType.ToString());
			}
		}

		private void SetupForm()
		{
			if (Detached) return;

			form.Clear();

			if (DatabaseConnector == null) return;

			new DatabaseFormFillerFactory()
				.GetFormFillerFor(DatabaseConnector)
				.FillForm(form);
		}

		private void RefreshSchema()
		{
			// Note: this is only ok because this method only runs on the UI thread.
			// Two instances of it will not run at once, so this is not a race condition.
			// The moment it can run from another thread, this assumption is false and the
			// code is incorrect.
			if (RefreshingSchema)
				return;

			databaseLock.WaitOne();

			RefreshingSchema = true;
			IDatabaseLoader loader = CreateDatabaseLoader(form);

			// Test connection first
			if (TestConnection(false) == false)
			{
				databaseLock.Set();
				return;
			}

			Thread thread = new Thread(
				() =>
				{
					try
					{
						IDatabase newDb = loader.LoadDatabase(loader.DatabaseObjectsToFetch, null);
						new DatabaseProcessor().CreateRelationships(newDb);

						if (Database == null || Database.IsEmpty)
						{
							Database = newDb;
							DatabaseConnector = loader.DatabaseConnector;
							NewDatabaseCreated.RaiseEvent(this);
							return;
						}

						var result = new DatabaseProcessor().MergeDatabases(Database, newDb);
						if (result.AnyChanges)
						{
							mainPanel.ShowDatabaseRefreshResults(result, Database, newDb);
							SchemaRefreshed.RaiseEvent(this);
						}
						else
						{
							form.SetDatabaseOperationResults(new DatabaseOperationResults("Schema Refresh", true, "No schema changes detected."));
						}
					}
					finally
					{
						databaseLock.Set();
						RefreshingSchema = false;
					}
				});
			thread.Start();

		}

		private bool TestConnection(bool showSuccess)
		{
			Monitor.Enter(databaseLock);

			IDatabaseLoader loader = CreateDatabaseLoader(form);

			try
			{
				loader.TestConnection();
				if (showSuccess)
					form.SetDatabaseOperationResults(new DatabaseOperationResults("Connection Test", true));

				return true;
			}
			catch (DatabaseLoaderException e)
			{
				DatabaseOperationResults results = new DatabaseOperationResults("Connection Test", false, e.ActualMessage);

				form.SetDatabaseOperationResults(results);

				return false;
			}
			finally
			{
				Monitor.Exit(databaseLock);
			}
		}

		public IDatabaseLoader CreateDatabaseLoader()
		{
			return CreateDatabaseLoader(form);
		}

		public static IDatabaseLoader CreateDatabaseLoader(IDatabaseForm databaseForm)
		{
			IDatabaseLoader loader;

			switch (databaseForm.SelectedDatabaseType)
			{
				case DatabaseTypes.SQLCE:
					loader = DatabaseLoaderFacade.GetSQLCELoader(databaseForm.SelectedDatabase);
					break;
				case DatabaseTypes.SQLServer2005:
					loader = DatabaseLoaderFacade.GetSQLServer2005Loader(databaseForm.ConnectionStringHelper);
					break;
				case DatabaseTypes.SQLServerExpress:
					loader = DatabaseLoaderFacade.GetSqlServerExpressLoader(databaseForm.ConnectionStringHelper, databaseForm.SelectedDatabase);
					break;
				case DatabaseTypes.MySQL:
					loader = DatabaseLoaderFacade.GetMySQLLoader(databaseForm.ConnectionStringHelper);
					break;
				case DatabaseTypes.Oracle:
					loader = DatabaseLoaderFacade.GetOracleLoader(databaseForm.ConnectionStringHelper, databaseForm.SelectedDatabase);
					break;
				case DatabaseTypes.PostgreSQL:
					loader = DatabaseLoaderFacade.GetPostgreSQLLoader(databaseForm.ConnectionStringHelper, databaseForm.SelectedDatabase);
					break;
				case DatabaseTypes.Firebird:
					loader = DatabaseLoaderFacade.GetFirebirdLoader(databaseForm.ConnectionStringHelper);
					break;
				case DatabaseTypes.SQLite:
					loader = DatabaseLoaderFacade.GetSQLiteLoader(databaseForm.ConnectionStringHelper);
					break;
				default:
					throw new NotImplementedException(
						string.Format("DatabasePresenter.CreateDatabaseLoader() does not support databases of type {0} yet",
						databaseForm.SelectedDatabaseType));
			}

			return loader;
		}

		public void AttachToModel(IDatabase db)
		{
			if (!Detached) DetachFromModel();

			if (db == null) return;
			Database = db;
			if (db.Loader != null) DatabaseConnector = db.Loader.DatabaseConnector;
			Detached = false;
			SetupForm();
		}

		public override void DetachFromModel()
		{
			Database = null;
			DatabaseConnector = null;
			Detached = true;
			SetupForm();
		}

		internal override void AttachToModel(object obj)
		{
			if (obj is IDatabase == false)
				throw new ArgumentException("Model must be an IDatabase");
			AttachToModel((IDatabase)obj);
		}

		public override void Show()
		{
			//ShowPropertyGrid(form);
		}
	}

	public class DatabaseOperationResults
	{
		public bool Succeeded { get; set; }
		public string OperationName { get; set; }
		public string Message { get; set; }

		public DatabaseOperationResults(string operationName)
		{
			OperationName = operationName;
		}

		public DatabaseOperationResults(string operationName, bool succeeded, string message)
			: this(operationName)
		{
			Succeeded = succeeded;
			Message = message;
		}

		public DatabaseOperationResults(string operationName, bool succeeded)
			: this(operationName)
		{
			Succeeded = succeeded;
		}
	}

	public class DatabaseFormFillerFactory
	{
		public IDatabaseFormFiller GetFormFillerFor(IDatabaseConnector connector)
		{
			if (connector is ISQLServer2005DatabaseConnector)
				return new SQLServer2005DatabaseFormFiller(connector as ISQLServer2005DatabaseConnector);

			if (connector is ISQLCEDatabaseConnector)
				return new SQLServerCEDatabaseFormFiller(connector as ISQLCEDatabaseConnector);

			if (connector is ISQLServerExpressDatabaseConnector)
				return new SQLServerExpressDatabaseFormFiller(connector as ISQLServerExpressDatabaseConnector);

			if (connector is IOracleDatabaseConnector)
				return new OracleDatabaseFormFiller(connector as IOracleDatabaseConnector);

			if (connector is IPostgreSQLDatabaseConnector)
				return new PostgreSQLDatabaseFormFiller(connector as IPostgreSQLDatabaseConnector);

			if (connector is IMySQLDatabaseConnector)
				return new MySQLDatabaseFormFiller(connector as IMySQLDatabaseConnector);

			if (connector is IFirebirdDatabaseConnector)
				return new FirebirdDatabaseFormFiller(connector as IFirebirdDatabaseConnector);

			if (connector is ISQLiteDatabaseConnector)
				return new SQLiteDatabaseFormFiller(connector as ISQLiteDatabaseConnector);

			throw new ArgumentException("DatabaseFormFillerFactory does not support " + connector.GetType() + " connectors yet.");
		}
	}

	public interface IServerAndDatabaseHelper
	{
		IEnumerable<string> GetServerNames(DatabaseTypes databaseType);
		IEnumerable<string> GetDatabaseNamesForServer(ConnectionStringHelper info);
		IEnumerable<string> GetSchemaNamesForServer(ConnectionStringHelper info);
	}

	public class ServerAndDatabaseHelper : IServerAndDatabaseHelper
	{
		public IEnumerable<string> GetServerNames(DatabaseTypes databaseType)
		{
			if (databaseType == DatabaseTypes.SQLServer2005)
				return SqlServer2005Helper.GetSqlServer2005Instances();

			if (databaseType == DatabaseTypes.MySQL)
				return MySQLHelper.GetMySQLInstances();

			if (databaseType == DatabaseTypes.Oracle)
				return OracleHelper.GetOracleInstances();

			if (databaseType == DatabaseTypes.PostgreSQL)
				return PostgreSQLHelper.GetPostgreSQLInstances();

			if (databaseType == DatabaseTypes.SQLServerExpress)
				return SqlServerExpressHelper.GetSqlServerExpressInstances();

			if (databaseType == DatabaseTypes.SQLite)
				return SQLiteHelper.GetSQLiteInstances();

			throw new NotImplementedException("This database type not handled yet in GetServerNames(): " + databaseType.ToString());

			//return new List<string>();
		}

		public IEnumerable<string> GetDatabaseNamesForServer(ConnectionStringHelper serverInfo)
		{
			//if (serverInfo.CurrentDbType == DatabaseTypes.SQLServer2000)
			//    return SqlServer2005Helper.GetSQlServer2005Databases(serverInfo);

			if (serverInfo.CurrentDbType == DatabaseTypes.SQLServer2005)
				return SqlServer2005Helper.GetSQlServer2005Databases(serverInfo);

			if (serverInfo.CurrentDbType == DatabaseTypes.MySQL)
				return MySQLHelper.GetMySQLDatabases(serverInfo);

			if (serverInfo.CurrentDbType == DatabaseTypes.Oracle)
				return OracleHelper.GetOracleDatabases(serverInfo);

			if (serverInfo.CurrentDbType == DatabaseTypes.PostgreSQL)
				return PostgreSQLHelper.GetPostgreSQLDatabases(serverInfo);

			if (serverInfo.CurrentDbType == DatabaseTypes.SQLServerExpress)
				return SqlServerExpressHelper.GetSqlServerExpressDatabases(serverInfo);

			if (serverInfo.CurrentDbType == DatabaseTypes.SQLite)
				return SQLiteHelper.GetSQLiteDatabases(serverInfo);

			throw new NotImplementedException("This database type not handled yet in GetDatabaseNamesForServer(): " + serverInfo.CurrentDbType.ToString());
			//return new List<string>();
		}

		public IEnumerable<string> GetSchemaNamesForServer(ConnectionStringHelper serverInfo)
		{
			if (serverInfo.CurrentDbType == DatabaseTypes.PostgreSQL)
				return PostgreSQLHelper.GetPostgreSQLSchemas(serverInfo);

			throw new NotImplementedException("This database type not handled yet in GetSchemaNamesForServer(): " + serverInfo.CurrentDbType.ToString());
			//return new List<string>();
		}
	}
}
