using System;
using System.Data;
using System.IO;
using System.Linq;
using ArchAngel.Interfaces;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using ArchAngel.Providers.EntityModel.UnitTesting;

namespace Specs_For_SQL_Server_Variants
{
    public class Opening_Simple_DatabaseBase
    {
		protected static void Generic_Index_With_None_Index_Type(IDatabase db)
        {
            ITable table = db.Tables[0];
            IIndex index = table.Indexes[2];

            Assert.That(index.Datatype, Is.EqualTo(DatabaseIndexType.None));
            Assert.That(index.Columns, Has.Count(3));
            Assert.That(index.Columns[0], Is.SameAs(table.Columns[0]));
            Assert.That(index.Columns[1], Is.SameAs(table.Columns[1]));
            Assert.That(index.Columns[2], Is.SameAs(table.Columns[2]));
        }

		protected static void Generic_Relationships_Hold_Mapped_Columns(IDatabase db)
        {
            ITable sourceTable = db.Tables[1];
            ITable targetTable = db.Tables[0];

            var rel = sourceTable.Relationships[0];
            Assert.That(rel.Name, Is.EqualTo("FK"), "Someone has changed the database that this test relies on.");

            Assert.That(rel.MappedColumns.Count(), Is.EqualTo(2));
            Assert.That(rel.MappedColumns.ElementAt(0).Source, Is.SameAs(sourceTable.Columns[0]));
            Assert.That(rel.MappedColumns.ElementAt(0).Target, Is.SameAs(targetTable.Columns[1]));

            Assert.That(rel.MappedColumns.ElementAt(1).Source, Is.SameAs(sourceTable.Columns[1]));
            Assert.That(rel.MappedColumns.ElementAt(1).Target, Is.SameAs(targetTable.Columns[0]));
        }

		protected static void Generic_Loads_FK_Correctly_When_It_Refers_To_UniqueKey_In_DB(IDatabase db)
		{
			ITable primaryTable = db.Tables.First(t => t.Name == "PrimaryTable");
			ITable foreignTable = db.Tables.First(t => t.Name == "ForeignTable");

			Assert.That(foreignTable.Keys, Has.Count(1));
			Assert.That(primaryTable.Keys, Has.Count(1));
			var foreignKey = foreignTable.Keys[0];
			var uniqueKey = primaryTable.Keys[0];
			
			Assert.That(foreignKey.ReferencedKey, Is.SameAs(uniqueKey));
		}

		protected static void Generic_Relationship_Created_From_FK_To_UniqueKey(IDatabase db)
		{
			ITable primaryTable = db.Tables.First(t => t.Name == "PrimaryTable");
			ITable foreignTable = db.Tables.First(t => t.Name == "ForeignTable");

			Assert.That(foreignTable.Relationships, Has.Count(1));
			Assert.That(primaryTable.Relationships, Has.Count(1));

			var rel = foreignTable.DirectedRelationships.First();

			Assert.That(rel.FromKey, Is.SameAs(foreignTable.Keys[0]));
			Assert.That(rel.ToKey, Is.SameAs(primaryTable.Keys[0]));
		}

		protected static void Generic_Multiple_Column_Relationship(IDatabase db)
        {
            ITable table = db.Tables[0];
            IKey key = table.Keys[2];

            Assert.That(key.IsUnique, Is.True);
            Assert.That(key.Columns, Has.Count(2));
            Assert.That(key.Columns[0], Is.SameAs(table.Columns[1]));
            Assert.That(key.Columns[1], Is.SameAs(table.Columns[0]));
        }

		protected static void Generic_Multiple_Column_Indexes(IDatabase db)
        {
            ITable table = db.Tables[0];
            IIndex index = table.Indexes[2];

            Assert.That(index.IsUnique, Is.True);
            Assert.That(index.Columns, Has.Count(3));
            Assert.That(index.Columns[0], Is.SameAs(table.Columns[1]));
            Assert.That(index.Columns[1], Is.SameAs(table.Columns[2]));
            Assert.That(index.Columns[2], Is.SameAs(table.Columns[0]));
        }

		protected static void Generic_Constructs_The_Corrent_Many2One_Relationshps(IDatabase db)
        {
            var table1 = db.Tables[0];
            var table2 = db.Tables[1];

            Assert.That(table1.Relationships, Is.Not.Null);
            Assert.That(table2.Relationships, Is.Not.Null);
            Assert.That(table1.Relationships, Has.Count(1));
            Assert.That(table2.Relationships, Has.Count(1));
            Assert.That(table1.Relationships[0], Is.Not.Null);
            Assert.That(table2.Relationships[0], Is.Not.Null);
            Assert.That(table1.Relationships[0], Is.SameAs(table2.Relationships[0]));

            Relationship relationship = table1.Relationships[0];
            Assert.That(relationship, Is.Not.Null);
            Assert.That(relationship.PrimaryTable, Is.SameAs(table2));
            Assert.That(relationship.ForeignTable, Is.SameAs(table1));
            Assert.That(relationship.PrimaryCardinality, Is.EqualTo(Cardinality.Many));
            Assert.That(relationship.ForeignCardinality, Is.EqualTo(Cardinality.One));

            Assert.That(relationship.PrimaryKey, Is.Not.Null);
            Assert.That(relationship.PrimaryKey.Columns, Has.Count(1));
            Assert.That(relationship.ForeignKey.Columns, Has.Count(1));
            Assert.That(relationship.PrimaryKey.Keytype, Is.EqualTo(DatabaseKeyType.Primary));
            Assert.That(relationship.ForeignKey.Keytype, Is.EqualTo(DatabaseKeyType.Foreign));
        }

        protected static void Generic_Constructs_The_Correct_One2One_Relationships(Database db)
        {
            var table1 = db.Tables[4];
            var table2 = db.Tables[3];

            Assert.That(table1.Relationships, Is.Not.Null);
            Assert.That(table2.Relationships, Is.Not.Null);
            Assert.That(table1.Relationships, Has.Count(1));
            Assert.That(table2.Relationships, Has.Count(1));
            Assert.That(table1.Relationships[0], Is.Not.Null);
            Assert.That(table2.Relationships[0], Is.Not.Null);
            Assert.That(table1.Relationships[0], Is.SameAs(table2.Relationships[0]));

            var relationship = table1.Relationships[0];
            Assert.That(relationship, Is.Not.Null);
            Assert.That(relationship.PrimaryTable, Is.SameAs(table2));
            Assert.That(relationship.ForeignTable, Is.SameAs(table1));
            Assert.That(relationship.SourceCardinality, Is.EqualTo(Cardinality.One));
            Assert.That(relationship.TargetCardinality, Is.EqualTo(Cardinality.One));

            Assert.That(relationship.PrimaryKey, Is.Not.Null);
            Assert.That(relationship.PrimaryKey.Columns, Has.Count(3));
            Assert.That(relationship.ForeignKey.Columns, Has.Count(3));
            Assert.That(relationship.PrimaryKey.Keytype, Is.EqualTo(DatabaseKeyType.Primary));
            Assert.That(relationship.ForeignKey.Keytype, Is.EqualTo(DatabaseKeyType.Foreign));
        }

        protected static void Generic_Returns_Correct_Data(Database db, string databaseName, IDatabaseLoader loader)
        {
            Assert.That(db, Is.Not.Null);
            Assert.That(db.Tables, Is.Not.Null);
            Assert.That(db.Tables, Has.Count(1));
            Assert.That(db.Name, Is.EqualTo(databaseName));
            Assert.That(db.Loader, Is.SameAs(loader));

            var table = db.Tables[0];

            Assert.That(table, Is.Not.Null);
            Assert.That(table.Database, Is.SameAs(db));
            Assert.That(table.Name, Is.EqualTo("Table1"));
            Assert.That(table.Columns, Is.Not.Null);
            Assert.That(table.Columns, Has.Count(3));

            var column = table.Columns[0];

            Assert.That(column, Is.Not.Null);
            Assert.That(column.Parent, Is.SameAs(table));
            Assert.That(column.Name, Is.EqualTo("Column1"));
            Assert.That(column.Datatype, Is.EqualTo("int"));
			Assert.That(column.IsIdentity, Is.True, "Column1.IsIdentity should be true");
            Assert.That(column.InPrimaryKey, Is.True);
            Assert.That(column.IsNullable, Is.False);
            Assert.That(column.IsReadOnly, Is.False);
            Assert.That(column.IsUnique, Is.True);
            Assert.That(column.IsUserDefined, Is.False);
            Assert.That(column.OrdinalPosition, Is.EqualTo(1));
            Assert.That(column.Default, Is.EqualTo(""));

            column = table.Columns[1];

            Assert.That(column, Is.Not.Null);
            Assert.That(column.Parent, Is.SameAs(table));
            Assert.That(column.Name, Is.EqualTo("Column2"));
            Assert.That(column.Datatype, Is.EqualTo("nvarchar"));
            Assert.That(column.InPrimaryKey, Is.False);
			Assert.That(column.IsIdentity, Is.False);
			Assert.That(column.IsNullable, Is.True);
            Assert.That(column.IsReadOnly, Is.False);
            Assert.That(column.IsUnique, Is.False);
            Assert.That(column.IsUserDefined, Is.False);
            Assert.That(column.OrdinalPosition, Is.EqualTo(2));
            Assert.That(column.Default, Is.EqualTo(""));

            column = table.Columns[2];

            Assert.That(column, Is.Not.Null);
            Assert.That(column.Parent, Is.SameAs(table));
            Assert.That(column.Name, Is.EqualTo("Column3"));
            Assert.That(column.Datatype, Is.EqualTo("datetime"));
            Assert.That(column.InPrimaryKey, Is.False);
			Assert.That(column.IsIdentity, Is.False);
            Assert.That(column.IsNullable, Is.False);
            Assert.That(column.IsReadOnly, Is.False);
            Assert.That(column.IsUnique, Is.False);
            Assert.That(column.IsUserDefined, Is.False);
            Assert.That(column.OrdinalPosition, Is.EqualTo(3));
            Assert.That(column.Default, Is.EqualTo(""));

            IIndex index = table.Indexes[0];

            Assert.That(index, Is.Not.Null);
            Assert.That(index.Parent, Is.SameAs(table));
            Assert.That(index.Name, Is.EqualTo("PK_Table1"));
            Assert.That(index.Datatype, Is.EqualTo(DatabaseIndexType.PrimaryKey));
            Assert.That(index.IsClustered, Is.False);
            Assert.That(index.IsUnique, Is.True);
            Assert.That(index.IsUserDefined, Is.False);
            Assert.That(index.Columns, Is.Not.Null);
            Assert.That(index.Columns[0].Name, Is.EqualTo("Column1"));
            Assert.That(index.Columns[0].Parent, Is.EqualTo(table));

            index = table.Indexes[1];

            Assert.That(index, Is.Not.Null);
            Assert.That(index.Parent, Is.SameAs(table));
            Assert.That(index.Name, Is.EqualTo("UQ__Table1__000000000000000C"));
            Assert.That(index.Datatype, Is.EqualTo(DatabaseIndexType.Unique));
            Assert.That(index.IsClustered, Is.False);
            Assert.That(index.IsUnique, Is.True);
            Assert.That(index.IsUserDefined, Is.False);
            Assert.That(index.Columns, Is.Not.Null);
            Assert.That(index.Columns[0].Name, Is.EqualTo("Column1"));
            Assert.That(index.Columns[0].Parent, Is.EqualTo(table));
        }

        protected static void SetupLog4Net()
        {
#if TestUseLog4Net
			ConsoleAppender appender = new ConsoleAppender
			{
				Layout = new log4net.Layout.PatternLayout()
			};
			log4net.Config.BasicConfigurator.Configure(appender);
#endif
        }
    }

    [TestFixture]
    public class Opening_Simple_SQLServer2005_Database : Opening_Simple_DatabaseBase
    {
        const string clearFilename = "DropAllObjects - SQL Server.sql";

        [TestFixtureSetUp]
        public void SetupOnce()
        {
            SetupLog4Net();
            SqlServerHelper.CreateDatabaseFiles();
        }

        private void ClearDatabase(SQLServer2005DatabaseConnector database)
        {
            database.RunNonQuerySQL(File.ReadAllText(clearFilename));

            try
            {
                database.RunNonQuerySQL("exec DropSPViewsTables");
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                // As long as 'iterations:' is in the message, it executed ok
                if (!ex.Message.Contains("iterations:"))
                {
                    throw;
                }
            }
        }

        private void CreateDatabase(string scriptFile)
        {
            SQLServer2005DatabaseConnector database = new SQLServer2005DatabaseConnector(SqlServerHelper.ConnStringHelper);
            database.Open();
            ClearDatabase(database);
            database.RunNonQuerySQL(File.ReadAllText(scriptFile));
            database.Close();
        }

        [Test]
        public void Returns_Correct_Data()
        {
            var loader = new SQLServer2005DatabaseLoader(new SQLServer2005DatabaseConnector(SqlServerHelper.ConnStringHelper));
            CreateDatabase("1Table3Columns - SQL Server.sql");
            var db = loader.LoadDatabase();
            Generic_Returns_Correct_Data(db, "TestDatabase", loader);
        }

        [Test]
        public void Constructs_The_Correct_One2One_Relationships()
        {
            var loader = new SQLServer2005DatabaseLoader(new SQLServer2005DatabaseConnector(SqlServerHelper.ConnStringHelper));
            CreateDatabase("2Tables1Relationship - SQL Server.sql");
            var db = loader.LoadDatabase();
            new DatabaseProcessor().CreateRelationships(db);
            Generic_Constructs_The_Correct_One2One_Relationships(db);
        }

        [Test]
        public void Constructs_The_Corrent_Many2One_Relationshps()
        {
            var loader = new SQLServer2005DatabaseLoader(new SQLServer2005DatabaseConnector(SqlServerHelper.ConnStringHelper));
            CreateDatabase("2Tables1Relationship - SQL Server.sql");
            var db = loader.LoadDatabase();
            new DatabaseProcessor().CreateRelationships(db);
            Generic_Constructs_The_Corrent_Many2One_Relationshps(db);
        }

		[Test]
		public void Constructs_The_Corrent_ForeignKey_From_A_FK_To_Unique_Key()
		{
			var loader = new SQLServer2005DatabaseLoader(new SQLServer2005DatabaseConnector(SqlServerHelper.ConnStringHelper));
			CreateDatabase("ForeignKeyToUniqueKey - SQL Server.sql");
			var db = loader.LoadDatabase();
			Generic_Loads_FK_Correctly_When_It_Refers_To_UniqueKey_In_DB(db);
		}

		[Test]
		public void Constructs_The_Corrent_Relationshp_From_A_FK_To_Unique_Key()
		{
			var loader = new SQLServer2005DatabaseLoader(new SQLServer2005DatabaseConnector(SqlServerHelper.ConnStringHelper));
			CreateDatabase("ForeignKeyToUniqueKey - SQL Server.sql");
			var db = loader.LoadDatabase();
			new DatabaseProcessor().CreateRelationships(db);
			Generic_Relationship_Created_From_FK_To_UniqueKey(db);
		}
    }

    [TestFixture]
    public class When_Opening_Simple_SQLCE_Database : Opening_Simple_DatabaseBase
    {
        [TestFixtureSetUp]
        public void SetupOnce()
        {
            SetupLog4Net();
        }

        [Test]
        [ExpectedException(typeof(DatabaseLoaderException))]
        public void Fails_To_Load_From_Invalid_Connector()
        {
            var loader = new SQLCEDatabaseLoader(new SQLCEDatabaseConnector("doesnotexist.sdf"));
            loader.LoadDatabase();
        }

        [Test]
        public void Returns_Correct_Data()
        {
            var loader = DatabaseLoaderFacade.GetSQLCELoader("1Table3Columns.sdf");
            Database db = loader.LoadDatabase();
            Generic_Returns_Correct_Data(db, "1Table3Columns", loader);
        }

        [Test]
        public void Constructs_The_Correct_One2One_Relationships()
        {
            var loader = DatabaseLoaderFacade.GetSQLCELoader("2Tables1Relationship.sdf");
            Database db = loader.LoadDatabase();
            new DatabaseProcessor().CreateRelationships(db);
            Generic_Constructs_The_Correct_One2One_Relationships(db);
        }

        [Test]
        public void Constructs_The_Corrent_Many2One_Relationshps()
        {
            var loader = DatabaseLoaderFacade.GetSQLCELoader("2Tables1Relationship.sdf");
            Database db = loader.LoadDatabase();
            new DatabaseProcessor().CreateRelationships(db);
            Generic_Constructs_The_Corrent_Many2One_Relationshps(db);
        }

		[Test]
		public void Constructs_The_Corrent_ForeignKey_From_A_FK_To_Unique_Key()
		{
			var loader = DatabaseLoaderFacade.GetSQLCELoader("ForeignKeyToUniqueKey.sdf");
			var db = loader.LoadDatabase();
			Generic_Loads_FK_Correctly_When_It_Refers_To_UniqueKey_In_DB(db);
		}

		[Test]
		public void Constructs_The_Corrent_Relationshp_From_A_FK_To_Unique_Key()
		{
			var loader = DatabaseLoaderFacade.GetSQLCELoader("ForeignKeyToUniqueKey.sdf");
			var db = loader.LoadDatabase();
			new DatabaseProcessor().CreateRelationships(db);
			Generic_Relationship_Created_From_FK_To_UniqueKey(db);
		}

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Does_Not_Support_Check_Indexes()
        {
            // Create mock index data
            DataTable dt = new DataTable();
            /*IND.TABLE_NAME, IND.INDEX_NAME, IND.COLUMN_NAME, IND.PRIMARY_KEY AS IsPrimaryKey, IND.[UNIQUE] AS IsUnique, 
                         IND.[CLUSTERED] AS IsClustered, TC.CONSTRAINT_TYPE, TC.CONSTRAINT_NAME*/
            dt.Columns.Add("TABLE_NAME");
            dt.Columns.Add("INDEX_NAME");
            dt.Columns.Add("COLUMN_NAME");
            dt.Columns.Add("IsPrimaryKey");
            dt.Columns.Add("IsUnique");
            dt.Columns.Add("IsClustered");
            dt.Columns.Add("CONSTRAINT_TYPE");
            dt.Columns.Add("CONSTRAINT_NAME");

            dt.Rows.Add("Table1", "PK_Table1", "Column1", true, true, false, "CHECK", "PK_Table1");


            ISQLCEDatabaseConnector connector = MockRepository.GenerateStub<ISQLCEDatabaseConnector>();
            connector.Stub(t => t.Indexes).Return(dt);

            SQLCEDatabaseLoader loader = new SQLCEDatabaseLoader(connector);
            loader.GetTableConstraintIndexes(new Table("Table1"));
        }

        [Test(Description = "This test will fail if multicolumn indexes are being loaded in the wrong order")]
        public void Multiple_Column_Indexes()
        {
            var loader = DatabaseLoaderFacade.GetSQLCELoader("MultiColumnIndexes.sdf");
            Database db = loader.LoadDatabase();
            new DatabaseProcessor().CreateRelationships(db);
            Generic_Multiple_Column_Indexes(db);
        }

        [Test(Description = "This test will fail if multicolumn keys are being loaded in the wrong order")]
        public void Multiple_Column_Relationship()
        {
            var loader = DatabaseLoaderFacade.GetSQLCELoader("MultiColumnKeys.sdf");
            Database db = loader.LoadDatabase();
            new DatabaseProcessor().CreateRelationships(db);
            Generic_Multiple_Column_Relationship(db);
        }

        [Test]
        public void Relationships_Hold_Mapped_Columns()
        {
            var loader = DatabaseLoaderFacade.GetSQLCELoader("MultiColumnKeys.sdf");
            Database db = loader.LoadDatabase();
            new DatabaseProcessor().CreateRelationships(db);
            Generic_Relationships_Hold_Mapped_Columns(db);
        }

        [Test]
        public void Index_With_None_Index_Type()
        {
            Database db = DatabaseLoaderFacade.LoadSQLCeDatabase("NoneIndexType.sdf");
            Generic_Index_With_None_Index_Type(db);
        }
    }
}
