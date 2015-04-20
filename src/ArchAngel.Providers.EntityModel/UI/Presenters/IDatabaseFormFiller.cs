using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using ArchAngel.Providers.EntityModel.UI.PropertyGrids;

namespace ArchAngel.Providers.EntityModel.UI.Presenters
{
	public interface IDatabaseFormFiller
	{
		void FillForm(IDatabaseForm form);
	}

	public class SQLServerCEDatabaseFormFiller : IDatabaseFormFiller
	{
		private ISQLCEDatabaseConnector connector;

		public SQLServerCEDatabaseFormFiller(ISQLCEDatabaseConnector connector)
		{
			this.connector = connector;
		}

		public void FillForm(IDatabaseForm form)
		{
			form.SelectedDatabaseType = DatabaseTypes.SQLCE;
			form.SetDatabaseFilename(connector.DatabaseName);
		}
	}

	public class SQLServer2005DatabaseFormFiller : IDatabaseFormFiller
	{
		private ISQLServer2005DatabaseConnector connector;

		public SQLServer2005DatabaseFormFiller(ISQLServer2005DatabaseConnector connector)
		{
			this.connector = connector;
		}

		public void FillForm(IDatabaseForm form)
		{
			form.SelectedDatabaseType = DatabaseTypes.SQLServer2005;

			if (connector.ConnectionInformation.UseFileName)
				form.SetDatabaseFilename(connector.DatabaseName);
			else
				form.SetDatabase(connector.DatabaseName);

			form.UseIntegratedSecurity = connector.ConnectionInformation.UseIntegratedSecurity;
			form.Username = connector.ConnectionInformation.UserName;
			form.Password = connector.ConnectionInformation.Password;
			form.SelectedServerName = connector.ConnectionInformation.ServerName;
			form.Port = connector.ConnectionInformation.Port;
			form.ServiceName = connector.ConnectionInformation.ServiceName;
		}
	}

	public class SQLServerExpressDatabaseFormFiller : IDatabaseFormFiller
	{
		private ISQLServerExpressDatabaseConnector connector;

		public SQLServerExpressDatabaseFormFiller(ISQLServerExpressDatabaseConnector connector)
		{
			this.connector = connector;
		}

		public void FillForm(IDatabaseForm form)
		{
			form.SelectedDatabaseType = DatabaseTypes.SQLServerExpress;

			if (connector.ConnectionInformation.UseFileName)
				form.SetDatabaseFilename(connector.DatabaseName);
			else
				form.SetDatabase(connector.DatabaseName);

			form.UseIntegratedSecurity = connector.ConnectionInformation.UseIntegratedSecurity;
			form.Username = connector.ConnectionInformation.UserName;
			form.Password = connector.ConnectionInformation.Password;
			form.SelectedServerName = connector.ConnectionInformation.ServerName;
			form.Port = connector.ConnectionInformation.Port;
			form.ServiceName = connector.ConnectionInformation.ServiceName;
		}
	}

	public class OracleDatabaseFormFiller : IDatabaseFormFiller
	{
		private IOracleDatabaseConnector connector;

		public OracleDatabaseFormFiller(IOracleDatabaseConnector connector)
		{
			this.connector = connector;
		}

		public void FillForm(IDatabaseForm form)
		{
			form.SelectedDatabaseType = DatabaseTypes.Oracle;

			form.SetDatabase(connector.DatabaseName);
			form.UseIntegratedSecurity = connector.ConnectionInformation.UseIntegratedSecurity;
			form.Username = connector.ConnectionInformation.UserName;
			form.Password = connector.ConnectionInformation.Password;
			form.SelectedServerName = connector.ConnectionInformation.ServerName;
			form.Port = connector.ConnectionInformation.Port;
			form.ServiceName = connector.ConnectionInformation.ServiceName;
		}
	}

	public class MySQLDatabaseFormFiller : IDatabaseFormFiller
	{
		private IMySQLDatabaseConnector connector;

		public MySQLDatabaseFormFiller(IMySQLDatabaseConnector connector)
		{
			this.connector = connector;
		}

		public void FillForm(IDatabaseForm form)
		{
			form.SelectedDatabaseType = DatabaseTypes.MySQL;

			form.SetDatabase(connector.DatabaseName);
			form.UseIntegratedSecurity = connector.ConnectionInformation.UseIntegratedSecurity;
			form.Username = connector.ConnectionInformation.UserName;
			form.Password = connector.ConnectionInformation.Password;
			form.SelectedServerName = connector.ConnectionInformation.ServerName;
			form.Port = connector.ConnectionInformation.Port;
			form.ServiceName = connector.ConnectionInformation.ServiceName;
		}
	}

	public class PostgreSQLDatabaseFormFiller : IDatabaseFormFiller
	{
		private IPostgreSQLDatabaseConnector connector;

		public PostgreSQLDatabaseFormFiller(IPostgreSQLDatabaseConnector connector)
		{
			this.connector = connector;
		}

		public void FillForm(IDatabaseForm form)
		{
			form.SelectedDatabaseType = DatabaseTypes.PostgreSQL;

			form.SetDatabase(connector.DatabaseName);
			form.UseIntegratedSecurity = connector.ConnectionInformation.UseIntegratedSecurity;
			form.Username = connector.ConnectionInformation.UserName;
			form.Password = connector.ConnectionInformation.Password;
			form.SelectedServerName = connector.ConnectionInformation.ServerName;
			form.Port = connector.ConnectionInformation.Port;
			form.ServiceName = connector.ConnectionInformation.ServiceName;
		}
	}

	public class FirebirdDatabaseFormFiller : IDatabaseFormFiller
	{
		private IFirebirdDatabaseConnector connector;

		public FirebirdDatabaseFormFiller(IFirebirdDatabaseConnector connector)
		{
			this.connector = connector;
		}

		public void FillForm(IDatabaseForm form)
		{
			form.SelectedDatabaseType = DatabaseTypes.Firebird;

			form.SetDatabase(connector.DatabaseName);
			form.UseIntegratedSecurity = connector.ConnectionInformation.UseIntegratedSecurity;
			form.Username = connector.ConnectionInformation.UserName;
			form.Password = connector.ConnectionInformation.Password;
			form.SelectedServerName = connector.ConnectionInformation.ServerName;
			form.Port = connector.ConnectionInformation.Port;
			form.ServiceName = connector.ConnectionInformation.ServiceName;
		}
	}

	public class SQLiteDatabaseFormFiller : IDatabaseFormFiller
	{
		private ISQLiteDatabaseConnector connector;

		public SQLiteDatabaseFormFiller(ISQLiteDatabaseConnector connector)
		{
			this.connector = connector;
		}

		public void FillForm(IDatabaseForm form)
		{
			form.SelectedDatabaseType = DatabaseTypes.SQLite;

			form.SetDatabase(connector.DatabaseName);
			form.UsingDatabaseFile = true;
			form.UseIntegratedSecurity = connector.ConnectionInformation.UseIntegratedSecurity;
			form.Username = connector.ConnectionInformation.UserName;
			form.Password = connector.ConnectionInformation.Password;
			form.SelectedServerName = connector.ConnectionInformation.ServerName;
			form.Port = connector.ConnectionInformation.Port;
			form.ServiceName = connector.ConnectionInformation.ServiceName;
		}
	}
}