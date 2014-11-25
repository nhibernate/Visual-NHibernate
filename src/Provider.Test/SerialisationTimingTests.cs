using System;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;

namespace Provider.Test
{
	public class SerialisationTimingTests
	{
		public static void Main()
		{
			Tester tester = new Tester();
			const string baseDir = @"C:\TestFiles\";
			tester.RunTest("Took {0} ms to serialise small database", RunSmallSerialisationTest, baseDir + "smallDatabaseSer.csv");
			tester.RunTest("Took {0} ms to serialise medium database", RunMediumSerialisationTest, baseDir + "mediumDatabaseSer.csv");
			tester.RunTest("Took {0} ms to serialise large database", RunLargeSerialisationTest, baseDir + "largeDatabaseSer.csv");
			tester.RunTest("Took {0} ms to deserialise small database", RunSmallDeserialisationTest, baseDir + "smallDatabaseDes.csv");
			tester.RunTest("Took {0} ms to deserialise medium database", RunMediumDeserialisationTest, baseDir + "mediumDatabaseDes.csv");
			tester.RunTest("Took {0} ms to deserialise large database", RunLargeDeserialisationTest, baseDir + "largeDatabaseDes.csv");

			tester.RunTest("Took {0} ms to serialise a database with {1} tables", RunSerialisationTest, baseDir +"databaseSerialisation.csv", 50, 20);
			tester.RunTest("Took {0} ms to deserialise a database with {1} tables", RunDeserialisationTest, baseDir + "databaseDeserialisation.csv", 50, 20);
		}

		private static TimeSpan RunSmallSerialisationTest()
		{
			return RunSerialisationTest(20, 5);
		}

		private static TimeSpan RunMediumSerialisationTest()
		{
			return RunSerialisationTest(100, 10);
		}

		private static TimeSpan RunLargeSerialisationTest()
		{
			return RunSerialisationTest(500, 20);
		}

		private static TimeSpan RunSmallDeserialisationTest()
		{
			return RunDeserialisationTest(20, 5);
		}

		private static TimeSpan RunMediumDeserialisationTest()
		{
			return RunDeserialisationTest(100, 10);
		}

		private static TimeSpan RunLargeDeserialisationTest()
		{
			return RunDeserialisationTest(500, 20);
		}

		private static TimeSpan RunSerialisationTest(int numTables)
		{
			return RunSerialisationTest(numTables, 10);
		}

		private static TimeSpan RunDeserialisationTest(int numTables)
		{
			return RunDeserialisationTest(numTables, 10);
		}

		private static TimeSpan RunSerialisationTest(int numTables, int numColumns)
		{
			IDatabase db = new Database("Test Database", ArchAngel.Providers.EntityModel.Controller.DatabaseLayer.DatabaseTypes.SQLServer2005);
			SetupDatabase(db, numTables, numColumns);

			DateTime start = DateTime.Now;

			db.Serialise(new DatabaseSerialisationScheme());

			return DateTime.Now - start;
		}

		private static TimeSpan RunDeserialisationTest(int numTables, int numColumns)
		{
			IDatabase db = new Database("Test Database", ArchAngel.Providers.EntityModel.Controller.DatabaseLayer.DatabaseTypes.SQLServer2005);
			SetupDatabase(db, numTables, numColumns);

			string xml = db.Serialise(new DatabaseSerialisationScheme());

			var scheme = new DatabaseDeserialisationScheme();

			DateTime start = DateTime.Now;

			scheme.Deserialise(xml);

			return DateTime.Now - start;
		}


		private static void SetupDatabase(IDatabase db, int numTables, int numColumns)
		{
			for (int i = 0; i < numTables; i++)
				db.AddTable(new Table("Table" + i, ""));

			for (int i = 0; i < db.Tables.Count; i++)
			{
				ITable table = db.Tables[i];
				for (int j = 0; j < numColumns; j++ )
					table.AddColumn(new Column("Column" + j));
			}
		}
	}
}
