using System.Collections.Generic;
using ArchAngel.Providers.EntityModel.Helper;

namespace ArchAngel.Providers.EntityModel.UI.Presenters
{
	public static class SQLiteHelper
	{
		/// <summary>
		/// Gets all local and network instances of SQLite.
		/// </summary>
		/// <returns></returns>
		public static List<string> GetSQLiteInstances()
		{
			List<string> serverNames = new List<string>();
			return serverNames;
		}

		public static string[] GetSQLiteDatabases(ConnectionStringHelper helper)
		{
			List<string> databaseNames = new List<string>();
			return databaseNames.ToArray();
		}
	}
}