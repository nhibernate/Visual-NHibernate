using System;
using System.Linq;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Specs_For_Databases;

namespace Specs_For_Merging_Databases
{
	[TestFixture]
	public class When_Merging_Two_Identical_Databases
	{
		[Test]
		public void The_ResultSet_Has_Nothing_in_It()
		{
			Database db1 = TestDatabaseLoader.TestDatabase();
			Database db2 = TestDatabaseLoader.TestDatabase();

			DatabaseMergeResult result = new DatabaseProcessor().MergeDatabases(db1, db2);

			Assert.That(result.TableOperations, Is.Empty);
			Assert.That(result.ColumnOperations, Is.Empty);
			Assert.That(result.IndexOperations, Is.Empty);
			Assert.That(result.KeyOperations, Is.Empty);
		}
	}

	namespace TableOperations
	{
		[TestFixture]
		public class When_Merging_A_Database_With_One_Additional_Table
		{
			[Test]
			public void The_New_Table_Is_In_The_ResultSet()
			{
				Database db1 = TestDatabaseLoader.TestDatabase();
				Database db2 = TestDatabaseLoader.TestDatabase();
				db2.AddTable(new Table("Table2"));

				DatabaseMergeResult result = new DatabaseProcessor().MergeDatabases(db1, db2);

				Assert.That(result.ColumnOperations, Is.Empty);
				Assert.That(result.IndexOperations, Is.Empty);
				Assert.That(result.KeyOperations, Is.Empty);

				Assert.That(result.TableOperations, Has.Count(1));
				var op = (TableAdditionOperation)result.TableOperations.ElementAt(0);

				Assert.That(op, Is.Not.Null);
				Assert.That(op.Object, Is.SameAs(db2.Tables[1]));
				Assert.That(op.Database, Is.SameAs(db1));
			}
		}

		[TestFixture]
		public class When_Merging_A_Database_With_Two_Additional_Tables
		{
			[Test]
			public void The_New_Tables_Are_In_The_ResultSet()
			{
				Database db1 = TestDatabaseLoader.TestDatabase();
				Database db2 = TestDatabaseLoader.TestDatabase();
				db2.AddTable(new Table("Table2"));
				db2.AddTable(new Table("aaaaaaa"));

				DatabaseMergeResult result = new DatabaseProcessor().MergeDatabases(db1, db2);

				Assert.That(result.ColumnOperations, Is.Empty);
				Assert.That(result.IndexOperations, Is.Empty);
				Assert.That(result.KeyOperations, Is.Empty);

				Assert.That(result.TableOperations, Has.Count(2));

				var op = (TableAdditionOperation)result.TableOperations.ElementAt(0);
				Assert.That(op, Is.Not.Null);
				Assert.That(op.Object, Is.SameAs(db2.Tables[1]));
				Assert.That(op.Database, Is.SameAs(db1));

				op = (TableAdditionOperation)result.TableOperations.ElementAt(1);
				Assert.That(op, Is.Not.Null);
				Assert.That(op.Object, Is.SameAs(db2.Tables[2]));
				Assert.That(op.Database, Is.SameAs(db1));
			}
		}

		[TestFixture]
		public class When_Merging_A_Database_With_One_Table_Removed
		{
			[Test]
			public void The_Removed_Table_Is_In_The_ResultSet()
			{
				Database db1 = TestDatabaseLoader.TestDatabase();
				Database db2 = TestDatabaseLoader.TestDatabase();
				db2.RemoveTable(db2.Tables[0]);

				DatabaseMergeResult result = new DatabaseProcessor().MergeDatabases(db1, db2);

				Assert.That(result.ColumnOperations, Is.Empty);
				Assert.That(result.IndexOperations, Is.Empty);
				Assert.That(result.KeyOperations, Is.Empty);

				Assert.That(result.TableOperations, Has.Count(1));
				var op = (TableRemovalOperation)result.TableOperations.ElementAt(0);

				Assert.That(op, Is.Not.Null);
				Assert.That(op.Object, Is.SameAs(db1.Tables[0]));
			}
		}

		[TestFixture]
		public class When_Merging_A_Database_With_Two_Tables_Removed
		{
			[Test]
			public void The_Removed_Table_Is_In_The_ResultSet()
			{
				Database db1 = TestDatabaseLoader.TestDatabase();
				db1.AddTable(new Table("aaaaaa"));
				Database db2 = TestDatabaseLoader.TestDatabase();
				db2.RemoveTable(db2.Tables[0]);

				DatabaseMergeResult result = new DatabaseProcessor().MergeDatabases(db1, db2);

				Assert.That(result.ColumnOperations, Is.Empty);
				Assert.That(result.IndexOperations, Is.Empty);
				Assert.That(result.KeyOperations, Is.Empty);

				Assert.That(result.TableOperations, Has.Count(2));
				
				var op = (TableRemovalOperation)result.TableOperations.ElementAt(0);
				Assert.That(op, Is.Not.Null);
				Assert.That(op.Object, Is.SameAs(db1.Tables[0]));

				op = (TableRemovalOperation)result.TableOperations.ElementAt(1);
				Assert.That(op, Is.Not.Null);
				Assert.That(op.Object, Is.SameAs(db1.Tables[1]));
			}
		}

		[TestFixture]
		public class When_Merging_A_Database_With_One_Table_Renamed
		{
			[Test]
			[Ignore("Ignore until we come up with a better renaming mechanism.")]
			public void The_Changed_Table_Is_In_The_ResultSet()
			{
				Database db1 = TestDatabaseLoader.TestDatabase();
				db1.Tables[0].UID = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);

				Database db2 = TestDatabaseLoader.TestDatabase();
				db2.Tables[0].UID = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
				db2.Tables[0].Name = "SomethingElse";

				DatabaseMergeResult result = new DatabaseProcessor().MergeDatabases(db1, db2);

				Assert.That(result.ColumnOperations, Is.Empty);
				Assert.That(result.IndexOperations, Is.Empty);
				Assert.That(result.KeyOperations, Is.Empty);

				Assert.That(result.TableOperations, Has.Count(1));

				var op = (TableChangeOperation)result.TableOperations.ElementAt(0);
				Assert.That(op, Is.Not.Null);
				Assert.That(op.Object, Is.SameAs(db1.Tables[0]));
			}
		}
	}

	namespace ColumnOperations
	{

		[TestFixture]
		public class When_Merging_A_Database_With_One_Added_Column
		{
			[Test]
			public void The_New_Column_Is_In_The_ResultSet()
			{
				Database db1 = TestDatabaseLoader.TestDatabase();
				Database db2 = TestDatabaseLoader.TestDatabase();
				db2.Tables[0].AddColumn(new Column("Column4"));

				DatabaseMergeResult result = new DatabaseProcessor().MergeDatabases(db1, db2);

				Assert.That(result.TableOperations, Is.Empty);
				Assert.That(result.IndexOperations, Is.Empty);
				Assert.That(result.KeyOperations, Is.Empty);

				Assert.That(result.ColumnOperations, Has.Count(1));
				IMergeOperation<IColumn> op = result.ColumnOperations.ElementAt(0);

				Assert.That(op.Object, Is.SameAs(db2.Tables[0].Columns[3]));
				Assert.That(op, Is.TypeOf(typeof(ColumnAdditionOperation)));
			}
		}

		[TestFixture]
		public class When_Merging_A_Database_With_Two_Added_Columns
		{
			[Test]
			public void The_New_Columns_Are_In_The_ResultSet()
			{
				Database db1 = TestDatabaseLoader.TestDatabase();
				Database db2 = TestDatabaseLoader.TestDatabase();
				db2.Tables[0].AddColumn(new Column("aaaaaaaaa"));
				db2.Tables[0].AddColumn(new Column("Column4"));

				DatabaseMergeResult result = new DatabaseProcessor().MergeDatabases(db1, db2);

				Assert.That(result.TableOperations, Is.Empty);
				Assert.That(result.IndexOperations, Is.Empty);
				Assert.That(result.KeyOperations, Is.Empty);
				Assert.That(result.ColumnOperations, Has.Count(2));

				IMergeOperation<IColumn> op = result.ColumnOperations.ElementAt(0);
				Assert.That(op.Object, Is.SameAs(db2.Tables[0].Columns[3]));
				Assert.That(op, Is.TypeOf(typeof(ColumnAdditionOperation)));

				op = result.ColumnOperations.ElementAt(1);
				Assert.That(op.Object, Is.SameAs(db2.Tables[0].Columns[4]));
				Assert.That(op, Is.TypeOf(typeof(ColumnAdditionOperation)));
			}
		}

		[TestFixture]
		public class When_Merging_A_Database_With_One_Removed_Column
		{
			[Test]
			public void The_Removed_Column_Is_In_The_ResultSet()
			{
				Database db1 = TestDatabaseLoader.TestDatabase();
				Database db2 = TestDatabaseLoader.TestDatabase();
				db2.Tables[0].RemoveColumn(db1.Tables[0].Columns[2]);

				DatabaseMergeResult result = new DatabaseProcessor().MergeDatabases(db1, db2);

				Assert.That(result.TableOperations, Is.Empty);
				Assert.That(result.IndexOperations, Is.Empty);
				Assert.That(result.KeyOperations, Is.Empty);

				Assert.That(result.ColumnOperations, Is.Not.Empty);
				Assert.That(result.ColumnOperations, Has.Count(1));
				IMergeOperation<IColumn> op = result.ColumnOperations.ElementAt(0);

				Assert.That(op.Object, Is.SameAs(db1.Tables[0].Columns[2]));
				Assert.That(op, Is.TypeOf(typeof(ColumnRemovalOperation)));
			}
		}

		[TestFixture]
		public class When_Merging_A_Database_With_Two_Removed_Columns
		{
			[Test]
			public void The_Removed_Columns_Are_In_The_ResultSet()
			{
				Database db1 = TestDatabaseLoader.TestDatabase();
				db1.Tables[0].AddColumn(new Column("aaaaaa"));
				Database db2 = TestDatabaseLoader.TestDatabase();
				db2.Tables[0].RemoveColumn(db1.Tables[0].Columns[2]);

				DatabaseMergeResult result = new DatabaseProcessor().MergeDatabases(db1, db2);

				Assert.That(result.TableOperations, Is.Empty);
				Assert.That(result.IndexOperations, Is.Empty);
				Assert.That(result.KeyOperations, Is.Empty);

				Assert.That(result.ColumnOperations, Is.Not.Empty);
				Assert.That(result.ColumnOperations, Has.Count(2));

				IMergeOperation<IColumn> op = result.ColumnOperations.ElementAt(0);
				Assert.That(op.Object, Is.SameAs(db1.Tables[0].Columns[2]));
				Assert.That(op, Is.TypeOf(typeof(ColumnRemovalOperation)));
				
				op = result.ColumnOperations.ElementAt(1);
				Assert.That(op.Object, Is.SameAs(db1.Tables[0].Columns[3]));
				Assert.That(op, Is.TypeOf(typeof(ColumnRemovalOperation)));
			}
		}

		[TestFixture]
		public class When_Merging_A_Database_With_One_Changed_Column
		{
			[Test]
			public void The_Changed_Column_Is_In_The_ResultSet()
			{
				Database db1 = TestDatabaseLoader.TestDatabase();
				db1.Tables[0].Columns[0].UID = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
				Database db2 = TestDatabaseLoader.TestDatabase();
				db2.Tables[0].Columns[0].Size = 235;
				db2.Tables[0].Columns[0].UID = new Guid(1,1,1,1,1,1,1,1,1,1,1);

				DatabaseMergeResult result = new DatabaseProcessor().MergeDatabases(db1, db2);

				Assert.That(result.TableOperations, Is.Empty);
				Assert.That(result.IndexOperations, Is.Empty);
				Assert.That(result.KeyOperations, Is.Empty);

				Assert.That(result.ColumnOperations, Has.Count(1));
				IMergeOperation<IColumn> op = result.ColumnOperations.ElementAt(0);

				Assert.That(op.Object, Is.SameAs(db1.Tables[0].Columns[0]));
				Assert.That(op, Is.TypeOf(typeof(ColumnChangeOperation)));
			}
		}
	}

	namespace MultiOperations
	{

		[TestFixture]
		public class When_Merging_A_Database_With_One_Added_Column_That_Has_Been_Added_To_The_Primary_Key
		{
			[Test]
			public void The_New_Column_And_The_Changed_Key_Are_In_The_ResultSet()
			{
				Database db1 = TestDatabaseLoader.TestDatabase();
				Database db2 = TestDatabaseLoader.TestDatabase();
				db2.Tables[0].AddColumn(new Column("Column4") { InPrimaryKey = true});
				db2.Tables[0].Keys[0].AddColumn("Column4");
				db2.Tables[0].Indexes[0].AddColumn("Column4");

				DatabaseMergeResult result = new DatabaseProcessor().MergeDatabases(db1, db2);
				
				Assert.That(result.TableOperations, Is.Empty);

				Assert.That(result.IndexOperations, Has.Count(1));
				var indexOp = result.IndexOperations.ElementAt(0);
				Assert.That(indexOp, Is.TypeOf(typeof(IndexChangeOperation)));
				Assert.That(indexOp.Object, Is.SameAs(db1.Tables[0].Indexes[0]), "Changed index should be equal to PK_Table1 from db1");

				Assert.That(result.KeyOperations, Has.Count(1));
				var keyOp = result.KeyOperations.ElementAt(0);
				Assert.That(keyOp, Is.TypeOf(typeof(KeyChangeOperation)));
				Assert.That(keyOp.Object, Is.SameAs(db1.Tables[0].Keys[0]), "Changed Key should be equal to PK_Table1 from db1");
				
				Assert.That(result.ColumnOperations, Has.Count(1));
				IMergeOperation<IColumn> columnOp = result.ColumnOperations.ElementAt(0);
				Assert.That(columnOp, Is.TypeOf(typeof(ColumnAdditionOperation)));
				Assert.That(columnOp.Object, Is.SameAs(db2.Tables[0].Columns[3]), "Added column should be equal to Table1.Column4 from db2");
			}
		}

		[TestFixture]
		public class When_Merging_A_Database_With_One_Column_Removed_That_Existed_In_A_Primary_Key
		{
			[Test]
			public void The_Removed_Column_And_The_Changed_Key_Are_In_The_ResultSet()
			{
				Database db1 = TestDatabaseLoader.TestDatabase();
				db1.Tables[0].AddColumn(new Column("Column4") { InPrimaryKey = true });
				db1.Tables[0].Keys[0].AddColumn("Column4");
				db1.Tables[0].Indexes[0].AddColumn("Column4");
				Database db2 = TestDatabaseLoader.TestDatabase();

				DatabaseMergeResult result = new DatabaseProcessor().MergeDatabases(db1, db2);

				Assert.That(result.TableOperations, Is.Empty);

				Assert.That(result.IndexOperations, Has.Count(1));
				var indexOp = result.IndexOperations.ElementAt(0);
				Assert.That(indexOp, Is.TypeOf(typeof(IndexChangeOperation)));
				Assert.That(indexOp.Object, Is.SameAs(db1.Tables[0].Indexes[0]), "Changed index should be equal to PK_Table1 from db1");

				Assert.That(result.KeyOperations, Has.Count(1));
				var keyOp = result.KeyOperations.ElementAt(0);
				Assert.That(keyOp, Is.TypeOf(typeof(KeyChangeOperation)));
				Assert.That(keyOp.Object, Is.SameAs(db1.Tables[0].Keys[0]), "Changed Key should be equal to PK_Table1 from db1");

				Assert.That(result.ColumnOperations, Has.Count(1));
				IMergeOperation<IColumn> columnOp = result.ColumnOperations.ElementAt(0);
				Assert.That(columnOp, Is.TypeOf(typeof(ColumnRemovalOperation)));
				Assert.That(columnOp.Object, Is.SameAs(db1.Tables[0].Columns[3]), "Removed column should be equal to Table1.Column4 from db1");
			}
		}

		[TestFixture]
		public class When_Merging_A_Database_With_One_Column_Removed_That_Was_The_Only_One_In_A_Primary_Key
		{
			[Test]
			public void The_Removed_Column_And_The_Changed_Key_Are_In_The_ResultSet()
			{
				Database db1 = TestDatabaseLoader.TestDatabase();
				Database db2 = TestDatabaseLoader.TestDatabase();
				db2.Tables[0].RemoveKey(db2.Tables[0].Keys[0]);
				db2.Tables[0].RemoveIndex(db2.Tables[0].Indexes[0]);
				db2.Tables[0].RemoveColumn(db2.Tables[0].Columns[0]);

				DatabaseMergeResult result = new DatabaseProcessor().MergeDatabases(db1, db2);

				Assert.That(result.TableOperations, Is.Empty);

				Assert.That(result.IndexOperations, Has.Count(1), "There should be one Index operation");
				var indexOp = result.IndexOperations.ElementAt(0);
				Assert.That(indexOp, Is.TypeOf(typeof(IndexRemovalOperation)));
				Assert.That(indexOp.Object, Is.SameAs(db1.Tables[0].Indexes[0]), "Removed index should be equal to PK_Table1 from db1");

				Assert.That(result.KeyOperations, Has.Count(1), "There should be one Key operation");
				var keyOp = result.KeyOperations.ElementAt(0);
				Assert.That(keyOp, Is.TypeOf(typeof(KeyRemovalOperation)));
				Assert.That(keyOp.Object, Is.SameAs(db1.Tables[0].Keys[0]), "Removed Key should be equal to PK_Table1 from db1");

				Assert.That(result.ColumnOperations, Has.Count(1), "There should be one Column operation");
				IMergeOperation<IColumn> columnOp = result.ColumnOperations.ElementAt(0);
				Assert.That(columnOp, Is.TypeOf(typeof(ColumnRemovalOperation)));
				Assert.That(columnOp.Object, Is.SameAs(db1.Tables[0].Columns[0]), "Removed column should be equal to Table1.Column1 from db1");
			}
		}
	}

	namespace KeyOperations
	{

		[TestFixture]
		public class When_Merging_A_Database_With_One_Added_Key
		{
			[Test]
			public void The_New_Key_Is_In_The_ResultSet()
			{
				Database db1 = TestDatabaseLoader.TestDatabase();
				Database db2 = TestDatabaseLoader.TestDatabase();
				db2.Tables[0].AddKey(new Key("PK_Table1_1"));

				DatabaseMergeResult result = new DatabaseProcessor().MergeDatabases(db1, db2);

				Assert.That(result.TableOperations, Is.Empty);
				Assert.That(result.IndexOperations, Is.Empty); 
				Assert.That(result.ColumnOperations, Is.Empty);

				Assert.That(result.KeyOperations, Has.Count(1));
				IMergeOperation<IKey> op = result.KeyOperations.ElementAt(0);

				Assert.That(op.Object, Is.SameAs(db2.Tables[0].Keys[1]));
				Assert.That(op, Is.TypeOf(typeof(KeyAdditionOperation)));
			}
		}

		[TestFixture]
		public class When_Merging_A_Database_With_Two_Added_Keys
		{
			[Test]
			public void The_New_Key_Is_In_The_ResultSet()
			{
				Database db1 = TestDatabaseLoader.TestDatabase();
				Database db2 = TestDatabaseLoader.TestDatabase();
				db2.Tables[0].AddKey(new Key("PK_Table1_1"));
				db2.Tables[0].AddKey(new Key("OutOfOrderKey"));

				DatabaseMergeResult result = new DatabaseProcessor().MergeDatabases(db1, db2);

				Assert.That(result.TableOperations, Is.Empty);
				Assert.That(result.IndexOperations, Is.Empty);
				Assert.That(result.ColumnOperations, Is.Empty);

				Assert.That(result.KeyOperations, Has.Count(2));
				IMergeOperation<IKey> op = result.KeyOperations.ElementAt(0);

				Assert.That(op.Object, Is.SameAs(db2.Tables[0].Keys[1]));
				Assert.That(op, Is.TypeOf(typeof(KeyAdditionOperation)));
				
				op = result.KeyOperations.ElementAt(1);
				Assert.That(op.Object, Is.SameAs(db2.Tables[0].Keys[2]));
				Assert.That(op, Is.TypeOf(typeof(KeyAdditionOperation)));
			}
		}

		[TestFixture]
		public class When_Merging_A_Database_With_One_Removed_Key
		{
			[Test]
			public void The_Removed_Key_Is_In_The_ResultSet()
			{
				Database db1 = TestDatabaseLoader.TestDatabase();
				Database db2 = TestDatabaseLoader.TestDatabase();
				db2.Tables[0].RemoveKey(db2.Tables[0].Keys[0]);

				DatabaseMergeResult result = new DatabaseProcessor().MergeDatabases(db1, db2);

				Assert.That(result.TableOperations, Is.Empty);
				Assert.That(result.IndexOperations, Is.Empty);
				Assert.That(result.ColumnOperations, Is.Empty);

				Assert.That(result.KeyOperations, Has.Count(1));
				IMergeOperation<IKey> op = result.KeyOperations.ElementAt(0);

				Assert.That(op.Object, Is.SameAs(db1.Tables[0].Keys[0]));
				Assert.That(op, Is.TypeOf(typeof(KeyRemovalOperation)));
			}
		}

		[TestFixture]
		public class When_Merging_A_Database_With_Two_Removed_Keys
		{
			[Test]
			public void The_Removed_Keys_Are_In_The_ResultSet()
			{
				Database db1 = TestDatabaseLoader.TestDatabase();
				// This key doesn't exist in the second db, so is effectively "removed"
				db1.Tables[0].AddKey(new Key("aaaaaa")); 
				Database db2 = TestDatabaseLoader.TestDatabase();
				db2.Tables[0].RemoveKey(db2.Tables[0].Keys[0]);

				DatabaseMergeResult result = new DatabaseProcessor().MergeDatabases(db1, db2);

				Assert.That(result.TableOperations, Is.Empty);
				Assert.That(result.IndexOperations, Is.Empty);
				Assert.That(result.ColumnOperations, Is.Empty);

				Assert.That(result.KeyOperations, Has.Count(2));

				IMergeOperation<IKey> op = result.KeyOperations.ElementAt(0);
				Assert.That(op.Object, Is.SameAs(db1.Tables[0].Keys[0]));
				Assert.That(op, Is.TypeOf(typeof(KeyRemovalOperation)));

				op = result.KeyOperations.ElementAt(1);
				Assert.That(op.Object, Is.SameAs(db1.Tables[0].Keys[1]));
				Assert.That(op, Is.TypeOf(typeof(KeyRemovalOperation)));
			}
		}

		[TestFixture]
		public class When_Merging_A_Database_With_One_Changed_Key
		{
			[Test]
			public void The_Changed_Key_Is_In_The_ResultSet()
			{
				Database db1 = TestDatabaseLoader.TestDatabase();
				db1.Tables[0].Keys[0].UID = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
				Database db2 = TestDatabaseLoader.TestDatabase();
				db2.Tables[0].Keys[0].Keytype = DatabaseKeyType.None;
				db2.Tables[0].Keys[0].UID = new Guid(1,1,1,1,1,1,1,1,1,1,1);

				DatabaseMergeResult result = new DatabaseProcessor().MergeDatabases(db1, db2);

				Assert.That(result.TableOperations, Is.Empty);
				Assert.That(result.IndexOperations, Is.Empty);
				Assert.That(result.ColumnOperations, Is.Empty);

				Assert.That(result.KeyOperations, Has.Count(1));
				IMergeOperation<IKey> op = result.KeyOperations.ElementAt(0);

				Assert.That(op.Object, Is.SameAs(db1.Tables[0].Keys[0]));
				Assert.That(op, Is.TypeOf(typeof(KeyChangeOperation)));
			}
		}
	}

	namespace IndexOperations
	{

		[TestFixture]
		public class When_Merging_A_Database_With_One_Added_Index
		{
			[Test]
			public void The_New_Index_Is_In_The_ResultSet()
			{
				Database db1 = TestDatabaseLoader.TestDatabase();
				Database db2 = TestDatabaseLoader.TestDatabase();
				db2.Tables[0].AddIndex(new Index("UQ_Table1_1"));

				DatabaseMergeResult result = new DatabaseProcessor().MergeDatabases(db1, db2);

				Assert.That(result.TableOperations, Is.Empty);
				Assert.That(result.KeyOperations, Is.Empty);
				Assert.That(result.ColumnOperations, Is.Empty);

				Assert.That(result.IndexOperations, Has.Count(1));
				IMergeOperation<IIndex> op = result.IndexOperations.ElementAt(0);

				Assert.That(op.Object, Is.SameAs(db2.Tables[0].Indexes[1]));
				Assert.That(op, Is.TypeOf(typeof(IndexAdditionOperation)));
			}
		}

		[TestFixture]
		public class When_Merging_A_Database_With_One_Removed_Index
		{
			[Test]
			public void The_Removed_Index_Is_In_The_ResultSet()
			{
				Database db1 = TestDatabaseLoader.TestDatabase();
				Database db2 = TestDatabaseLoader.TestDatabase();
				db2.Tables[0].RemoveIndex(db2.Tables[0].Indexes[0]);

				DatabaseMergeResult result = new DatabaseProcessor().MergeDatabases(db1, db2);

				Assert.That(result.TableOperations, Is.Empty);
				Assert.That(result.KeyOperations, Is.Empty);
				Assert.That(result.ColumnOperations, Is.Empty);

				Assert.That(result.IndexOperations, Has.Count(1));
				IMergeOperation<IIndex> op = result.IndexOperations.ElementAt(0);

				Assert.That(op.Object, Is.SameAs(db1.Tables[0].Indexes[0]));
				Assert.That(op, Is.TypeOf(typeof(IndexRemovalOperation)));
			}
		}

		[TestFixture]
		public class When_Merging_A_Database_With_One_Changed_Index
		{
			[Test]
			public void The_Changed_Index_Is_In_The_ResultSet()
			{
				Database db1 = TestDatabaseLoader.TestDatabase();
				db1.Tables[0].Indexes[0].UID = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
				Database db2 = TestDatabaseLoader.TestDatabase();
				db2.Tables[0].Indexes[0].IsUnique = false;
				db2.Tables[0].Indexes[0].UID = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);

				DatabaseMergeResult result = new DatabaseProcessor().MergeDatabases(db1, db2);

				Assert.That(result.TableOperations, Is.Empty);
				Assert.That(result.KeyOperations, Is.Empty);
				Assert.That(result.ColumnOperations, Is.Empty);

				Assert.That(result.IndexOperations, Has.Count(1));
				IMergeOperation<IIndex> op = result.IndexOperations.ElementAt(0);

				Assert.That(op.Object, Is.SameAs(db1.Tables[0].Indexes[0]));
				Assert.That(op, Is.TypeOf(typeof(IndexChangeOperation)));
			}
		}
	}
}