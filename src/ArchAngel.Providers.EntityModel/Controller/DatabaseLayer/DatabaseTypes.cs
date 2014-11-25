using System.ComponentModel;

namespace ArchAngel.Providers.EntityModel.Controller.DatabaseLayer
{
	public enum DatabaseTypes
	{
		//[Description("SQL Server 2000")]
		//SQLServer2000,
		[Description("Firebird")]
		Firebird,
		[Description("MySQL 5")]
		MySQL,
		[Description("Oracle")]
		Oracle,
		[Description("PostgreSQL")]
		PostgreSQL,
		[Description("SQL Compact Edition")]
		SQLCE,
		[Description("SQLite")]
		SQLite,
		[Description("SQL Server 2005/2008/Azure")]
		SQLServer2005,
		[Description("SQL Server Express")]
		SQLServerExpress,
		[Description("Unknown")]
		Unknown
	}
}