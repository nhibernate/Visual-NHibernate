using System;
using System.Data.SqlServerCe;
using System.Diagnostics;
using System.IO;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;

namespace Provider.Test
{
	public class DatabaseTimingTests
	{
		public static void Main()
		{
			const string connectionString = "Data Source = b.sdf";
			const string file = "b.sdf";

			// Warm up
			DoTest(100, file, connectionString);

			using (var stream = File.CreateText("output.csv"))
			{
				for (int i = 1; i < 21; i++)
				{
					int numTables = i * 500;
					TimeSpan timeTaken = DoTest(numTables, file, connectionString);
					
					Console.WriteLine("Took {0} ms to process {1} Tables", timeTaken.TotalMilliseconds, numTables);

					stream.WriteLine(string.Format("{0}, {1}", numTables, timeTaken.TotalMilliseconds));
					stream.Flush();
				}
			}
			Process.Start("output.csv");
		}

		private static TimeSpan DoTest(int numTables, string file, string connectionString)
		{
			DeleteDatabseFile(file);
			CreateDatabaseFile(connectionString);

			SetUpDatabase(connectionString, numTables);

			DateTime startTime = DateTime.Now;

			LoadDatabase(file);

			return DateTime.Now - startTime;
		}

		private static void LoadDatabase(string file)
		{
			Database db = new SQLCEDatabaseLoader(new SQLCEDatabaseConnector(file)).LoadDatabase();
			new DatabaseProcessor().CreateRelationships(db);
		}

		private static void SetUpDatabase(string connectionString, int numTables)
		{
			SqlCeConnection sqlConnection = GetSqlConnection(connectionString);
			for (int i = 0; i < numTables; i++)
			{
				var command = sqlConnection.CreateCommand();
				command.CommandText = string.Format("Create table table{0}(id int NOT NULL, field1 nvarchar(1), field2 int, field3 blob)", i);
				command.ExecuteNonQuery();
			}
			sqlConnection.Close();
		}

		private static void DeleteDatabseFile(string file)
		{
			if(File.Exists(file))
				File.Delete(file);
		}

		private static SqlCeConnection GetSqlConnection(string connectionString)
		{
			var sqlConnection = new SqlCeConnection(connectionString);
			sqlConnection.Open();
			return sqlConnection;
		}

		private static void CreateDatabaseFile(string connectionString)
		{
			var engine = new SqlCeEngine(connectionString);
			engine.CreateDatabase();
		}
	}
}
