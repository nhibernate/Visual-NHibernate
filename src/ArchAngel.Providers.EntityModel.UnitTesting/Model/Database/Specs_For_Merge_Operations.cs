using System;
using System.Collections.Generic;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Specs_For_Databases;
using System.Linq;
using Specs_For_Merging_Databases.Relationships;

namespace Specs_For_Merge_Operations
{
	[TestFixture]
	public class When_Getting_Two_Step_Operations
	{
		[Test]
		public void The_Operations_Come_Back_In_A_Good_Order()
		{
			DatabaseMergeResult result = new DatabaseMergeResult();
			result.AddIndexOperation(new IndexAdditionOperation(new Table(), new Index()));
			result.AddKeyOperation(new KeyAdditionOperation(new Table(), new Key()));
			// Not a ITwoStepMergeOperation
			result.AddTableOperation(new TableAdditionOperation(new Database(""), new Table()));

			IEnumerable<ITwoStepMergeOperation> list = result.TwoStepOperations;
 
			Assert.That(list, Has.Count(2));
			Assert.That(list.ElementAt(0) is KeyAdditionOperation);
			Assert.That(list.ElementAt(1) is IndexAdditionOperation);
		}
	}
	
	[TestFixture]
	public class When_Running_An_Uninitialised_Operation
	{
		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void An_Invalid_Operation_Exception_Is_Thrown()
		{
			var op = new TableAdditionOperation(null, null);
			op.RunOperation();
		}
	}


	namespace Tables
	{

		[TestFixture]
		public class After_A_Table_Addition_Is_Applied
		{
			[Test]
			public void The_Original_Database_Should_Contain_A_Copy_Of_The_New_Table()
			{
				var db1 = TestDatabaseLoader.TestDatabase();

				Table newTable = new Table("Table2");
				Column newColumn = new Column("Column1");
				Key newKey = new Key("PrimaryKey");
				Index newIndex = new Index("Index1");
				newTable.AddColumn(newColumn);
				newTable.AddKey(newKey);
				newTable.AddIndex(newIndex);

				TableAdditionOperation op = new TableAdditionOperation(db1, newTable);
				op.RunOperation();

				Assert.That(db1.Tables, Has.Count(2));
				Assert.That(db1.Tables[1].Name, Is.EqualTo(newTable.Name));
				Assert.That(db1.Tables[1], Is.Not.SameAs(newTable), "The added table should be a copy, not the original object.");
				Assert.That(db1.Tables[1].Columns[0], Is.EqualTo(newColumn));
				Assert.That(db1.Tables[1].Keys[0], Is.EqualTo(newKey));
				Assert.That(db1.Tables[1].Indexes[0], Is.EqualTo(newIndex));
			}
		}

		[TestFixture]
		public class After_A_Table_Removal_Is_Applied
		{
			private IDatabase db;

			[SetUp]
			public void Setup()
			{
				db = TestDatabaseLoader.TestDatabase();

				var t2 = new Table("Table2");
				db.AddTable(t2);
				t2.AddColumn(new Column("Column1") { InPrimaryKey = true, Datatype = "int", OrdinalPosition = 0, Size = 4 });
				t2.AddColumn(new Column("Column2") { Datatype = "nvarchar", OrdinalPosition = 1, Size = 100 });
				t2.AddColumn(new Column("Column3") { Datatype = "datetime", OrdinalPosition = 2, Size = 8 });

				t2.AddIndex(new Index("PK_Table2") { IsUnique = true, Datatype = DatabaseIndexType.PrimaryKey }).AddColumn("Column1");
				t2.AddKey(new Key("PK_Table2") { Keytype = DatabaseKeyType.Primary }).AddColumn("Column1").AddColumn("Column2");

				t2.CreateRelationshipTo(db.Tables[0]);
			}

			[Test]
			public void The_Original_Database_Should_Not_Contain_The_Removed_Table()
			{
				TableRemovalOperation op = new TableRemovalOperation(db.Tables[0]);
				op.RunOperation();

				Assert.That(db.Tables, Has.Count(1));
			}

			[Test]
			public void The_Tables_Relationships_Should_Be_Removed()
			{
				ITable table = db.Tables[0];

				Assert.That(table.Relationships, Is.Not.Empty);

				TableRemovalOperation op = new TableRemovalOperation(table);
				op.RunOperation();

				Assert.That(table.Relationships, Is.Empty);
			}
		}

		[TestFixture]
		public class After_A_Table_Removal_Is_NotApplied
		{
			[Test]
			public void The_Table_Should_Be_Marked_UserDefined()
			{
				var db1 = TestDatabaseLoader.TestDatabase();

				TableRemovalOperation op = new TableRemovalOperation(db1.Tables[0]);
				op.NotApplied();

				Assert.That(db1.Tables, Has.Count(1));
				Assert.That(db1.Tables[0].IsUserDefined, Is.True);
			}
		}
	}

	namespace Columns
	{
		[TestFixture]
		public class After_A_Column_Addition_Is_Applied
		{
			[Test]
			public void The_Original_Database_Should_Contain_A_Copy_Of_The_New_Column()
			{
				var db1 = TestDatabaseLoader.TestDatabase();
				
				Column newCol = new Column("Column4");
				ColumnAdditionOperation op = new ColumnAdditionOperation(db1.Tables[0], newCol);
				op.RunOperation();

				Assert.That(db1.Tables[0].Columns, Has.Count(4));
				Assert.That(db1.Tables[0].Columns[3].Name, Is.EqualTo(newCol.Name));
				Assert.That(db1.Tables[0].Columns[3], Is.Not.SameAs(newCol), "The added column should be a copy, not the original object.");
			}
		}

		[TestFixture]
		public class After_A_Column_Removal_Is_Applied
		{
			[Test]
			public void The_Original_Database_Should_Not_Contain_The_Removed_Column()
			{
				var db1 = TestDatabaseLoader.TestDatabase();

				IColumn oldColumn = db1.Tables[0].Columns[0];
				ColumnRemovalOperation op = new ColumnRemovalOperation(db1.Tables[0].Columns[0]);
				op.RunOperation();

				Assert.That(db1.Tables[0].Columns, Has.Count(2));
				Assert.That(db1.Tables[0].Columns.Contains(oldColumn), Is.False);
				Assert.That(db1.Tables[0].Keys[0].Columns, Has.Count(1), "The column should have been removed from the primary key");
				Assert.That(db1.Tables[0].Indexes[0].Columns, Is.Empty, "The column should have been removed from the unique index");
			}
		}

		[TestFixture]
		public class After_A_Column_Change_Is_Applied
		{
			[Test]
			public void The_Original_Column_Should_Be_Modified()
			{
				var db1 = TestDatabaseLoader.TestDatabase();

				IColumn originalColumn = db1.Tables[0].Columns[2];
				IColumn newColumn = originalColumn.Clone();
				newColumn.Datatype = "some datatype";
				ColumnChangeOperation op = new ColumnChangeOperation(db1.Tables[0].Columns[2], newColumn);
				op.RunOperation();

				Assert.That(db1.Tables[0].Columns, Has.Count(3));
				Assert.That(db1.Tables[0].Columns[2], Is.SameAs(originalColumn), "Column object should still be the same");
				Assert.That(db1.Tables[0].Columns[2].Datatype, Is.EqualTo("some datatype"));
			}
		}
	}

	namespace Keys
	{

		[TestFixture]
		public class After_A_Key_Addition_Is_Applied
		{
			[Test]
			public void The_Original_Database_Should_Contain_A_Copy_Of_The_New_Key()
			{
				var db1 = TestDatabaseLoader.TestDatabase();

				// Setup new key information
				Table parentTable = new Table("Table1");
				parentTable.AddColumn(new Column("Column3"));
				Key newKey = new Key("UQ_Column3");
				newKey.Parent = parentTable;
				newKey.AddColumn("Column3");

				KeyAdditionOperation op = new KeyAdditionOperation(db1.Tables[0], newKey);
				op.RunOperation();
				op.RunSecondStep();

				Assert.That(db1.Tables[0].Keys, Has.Count(2));
				Assert.That(db1.Tables[0].Keys[1].Name, Is.EqualTo(newKey.Name));
				Assert.That(db1.Tables[0].Keys[1], Is.Not.SameAs(newKey), "The added Key should be a copy, not the original object.");
				Assert.That(db1.Tables[0].Keys[1].Columns[0], Is.SameAs(db1.Tables[0].Columns[2]), "The new key should reference existing columns, not the new ones");
				Assert.That(db1.Tables[0].Keys[1].Parent, Is.SameAs(db1.Tables[0]), "The new key's parent should be Table1 in the existing database");
			}
		}

		[TestFixture]
		public class After_A_Key_Removal_Is_Applied
		{
			[Test]
			public void The_Original_Database_Should_Not_Contain_The_Removed_Key()
			{
				var db1 = TestDatabaseLoader.TestDatabase();

				KeyRemovalOperation op = new KeyRemovalOperation(db1.Tables[0].Keys[0]);
				op.RunOperation();

				Assert.That(db1.Tables[0].Keys, Is.Empty);
			}
		}

		[TestFixture]
		public class After_A_Key_Removal_Is_NotApplied
		{
			[Test]
			public void The_Key_Should_Be_Marked_As_User_Defined()
			{
				var db1 = TestDatabaseLoader.TestDatabase();

				KeyRemovalOperation op = new KeyRemovalOperation(db1.Tables[0].Keys[0]);
				op.NotApplied();

				Assert.That(db1.Tables[0].Keys, Has.Count(1));
				Assert.That(db1.Tables[0].Keys[0].IsUserDefined, Is.True);
			}
		}

		[TestFixture]
		public class After_A_Key_Change_Is_Applied
		{
			[Test(Description = "Test for modifying a Primary Key")]
			public void The_Original_Primary_Key_Should_Be_Modified()
			{
				var db1 = TestDatabaseLoader.TestDatabase();

				IKey originalKey = db1.Tables[0].Keys[0];
				originalKey.Description = "old description";

				IKey newKey = new Key(originalKey.Name);
				newKey.Parent = new Table("Table1");
				newKey.Parent.AddColumn(new Column("Column2"));
				newKey.Description = "new description";
				newKey.AddColumn("Column2");

				KeyChangeOperation op = new KeyChangeOperation(db1.Tables[0].Keys[0], newKey);
				op.RunOperation();
				op.RunSecondStep();

				IKey key = db1.Tables[0].Keys[0];

				Assert.That(db1.Tables[0].Keys, Has.Count(1));
				Assert.That(key, Is.SameAs(originalKey), "Key object should still be the same");
				Assert.That(key.Description, Is.EqualTo("old description"), "Should not change user set properties.");
				Assert.That(key.ReferencedKey, Is.Null);
				Assert.That(key.Columns, Has.Count(1));
				Assert.That(key.Columns[0].Name, Is.EqualTo("Column2"));
				Assert.That(key.Columns[0], Is.Not.SameAs(newKey.Columns[0]));
				Assert.That(key.Columns[0], Is.SameAs(db1.Tables[0].Columns[1]), "The new key should reference existing columns");
				Assert.That(key.Parent, Is.SameAs(db1.Tables[0]), "The new key's parent should be Table1 in the existing database");
			}

			[Test(Description = "Test for modifying a Foreign Key")]
			public void The_Original_Foreign_Key_Should_Be_Modified()
			{
				var db1 = RelationshipDatabaseLoader.GetDb();

				IKey originalKey = db1.Tables[1].Keys[0];
				originalKey.Description = "old description";

				IKey newPrimaryKey = new Key(db1.Tables[0].Keys[0].Name);
				newPrimaryKey.Parent = new Table("Table1");
				newPrimaryKey.Parent.AddColumn(new Column("Column1"));
				newPrimaryKey.Description = "new description";
				newPrimaryKey.AddColumn("Column1");

				IKey newForeignKey = new Key(originalKey.Name);
				newForeignKey.Parent = new Table("Table2");
				newForeignKey.Parent.AddColumn(new Column("Column2"));
				newForeignKey.Parent.AddColumn(new Column("Column3"));
				newForeignKey.Description = "new description";
				newForeignKey.AddColumn("Column3"); // This is the change.
				newForeignKey.ReferencedKey = newPrimaryKey;

				KeyChangeOperation op = new KeyChangeOperation(db1.Tables[1].Keys[0], newForeignKey);
				op.RunOperation();
				op.RunSecondStep();

				IKey key = db1.Tables[1].Keys[0];

				Assert.That(db1.Tables[1].Keys, Has.Count(1));
				Assert.That(key, Is.SameAs(originalKey), "Key object should still be the same");
				Assert.That(key.Description, Is.EqualTo("old description"), "Should not change user set properties.");
				Assert.That(key.ReferencedKey, Is.SameAs(db1.Tables[0].Keys[0]));
				Assert.That(key.Columns, Has.Count(1));
				Assert.That(key.Columns[0].Name, Is.EqualTo("Column3"));
				Assert.That(key.Columns[0], Is.Not.SameAs(newForeignKey.Columns[0]));
				Assert.That(key.Columns[0], Is.SameAs(db1.Tables[1].Columns[2]), "The new key should reference existing columns");
				Assert.That(key.Parent, Is.SameAs(db1.Tables[1]), "The new key's parent should be Table1 in the existing database");
			}
		}
	}

	namespace Indexes
	{

		[TestFixture]
		public class After_A_Index_Addition_Is_Applied
		{
			[Test]
			public void The_Original_Database_Should_Contain_A_Copy_Of_The_New_Index()
			{
				var db1 = TestDatabaseLoader.TestDatabase();

				// Setup new key information
				Table parentTable = new Table("Table1");
				parentTable.AddColumn(new Column("Column3"));
				Index newIndex = new Index("UQ_Column3");
				newIndex.Parent = parentTable;
				newIndex.AddColumn("Column3");

				IndexAdditionOperation op = new IndexAdditionOperation(db1.Tables[0], newIndex);
				op.RunOperation();
				op.RunSecondStep();

				Assert.That(db1.Tables[0].Indexes, Has.Count(2));
				Assert.That(db1.Tables[0].Indexes[1].Name, Is.EqualTo(newIndex.Name));
				Assert.That(db1.Tables[0].Indexes[1], Is.Not.SameAs(newIndex), "The added Index should be a copy, not the original object.");
				Assert.That(db1.Tables[0].Indexes[1].Columns[0], Is.SameAs(db1.Tables[0].Columns[2]), "The new Index should reference existing columns, not the new ones");
				Assert.That(db1.Tables[0].Indexes[1].Parent, Is.SameAs(db1.Tables[0]), "The new Index's parent should be Table1 in the existing database");
			}
		}

		[TestFixture]
		public class After_An_Index_Removal_Is_Applied
		{
			[Test]
			public void The_Original_Database_Should_Not_Contain_The_Removed_Index()
			{
				var db1 = TestDatabaseLoader.TestDatabase();

				IndexRemovalOperation op = new IndexRemovalOperation(db1.Tables[0].Indexes[0]);
				op.RunOperation();

				Assert.That(db1.Tables[0].Indexes, Is.Empty);
			}
		}

		[TestFixture]
		public class After_An_Index_Removal_Is_NotApplied
		{
			[Test]
			public void The_Index_Is_Marked_As_User_Defined()
			{
				var db1 = TestDatabaseLoader.TestDatabase();

				IndexRemovalOperation op = new IndexRemovalOperation(db1.Tables[0].Indexes[0]);
				op.NotApplied();

				Assert.That(db1.Tables[0].Indexes, Has.Count(1));
				Assert.That(db1.Tables[0].Indexes[0].IsUserDefined, Is.True);
			}
		}

		[TestFixture]
		public class After_An_Index_Change_Is_Applied
		{
			[Test]
			public void The_Original_Index_Should_Be_Modified()
			{
				var db1 = TestDatabaseLoader.TestDatabase();

				IIndex originalIndex = db1.Tables[0].Indexes[0];
				originalIndex.Description = "old description";

				IIndex newIndex = new Index(originalIndex.Name);
				newIndex.Parent = new Table("Table1");
				newIndex.Parent.AddColumn(new Column("Column2"));
				newIndex.Description = "new description";
				newIndex.AddColumn("Column2");

				IndexChangeOperation op = new IndexChangeOperation(db1.Tables[0].Indexes[0], newIndex);
				op.RunOperation();
				op.RunSecondStep();

				IIndex index = db1.Tables[0].Indexes[0];

				Assert.That(db1.Tables[0].Indexes, Has.Count(1), "No new indexes should have been added");
				Assert.That(index, Is.SameAs(originalIndex), "Index object should still be the same");
				Assert.That(index.Description, Is.EqualTo("old description"));
				Assert.That(index.Columns, Has.Count(1));
				Assert.That(index.Columns[0].Name, Is.EqualTo("Column2"));
				Assert.That(index.Columns[0], Is.Not.SameAs(newIndex.Columns[0]));
				Assert.That(index.Columns[0], Is.SameAs(db1.Tables[0].Columns[1]), "The new Index should reference existing columns");
				Assert.That(index.Parent, Is.SameAs(db1.Tables[0]), "The new Index's parent should be Table1 in the existing database");
			}
		}
	}
}
