using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Specs_For_Databases;

namespace Specs_For_Merging_Databases.Relationships
{
	[TestFixture]
	public class When_Adding_A_New_Column_To_A_Primary_Key
	{
		[Test]
		public void The_Corresponding_Relationship_Should_Also_Have_The_Column()
		{
			// Create database and relationships
			Database db = RelationshipDatabaseLoader.GetDb();

			Relationship relationship = db.Tables[1].Relationships[0];
			Assert.That(relationship.PrimaryKey, Is.SameAs(db.Tables[0].Keys[0]));
			Assert.That(relationship.ForeignKey, Is.SameAs(db.Tables[1].Keys[0]));
			Assert.That(relationship.ForeignKey.Columns[0].Name, Is.EqualTo("Column1"));

			IKey originalKey = db.Tables[0].Keys[0];

			IKey newKey = new Key(originalKey.Name);
			newKey.Parent = new Table("Table1");
			newKey.Parent.AddColumn(new Column("Column2"));
			newKey.Description = "new description";
			newKey.AddColumn("Column2");

			KeyChangeOperation op = new KeyChangeOperation(db.Tables[0].Keys[0], newKey);
			op.RunOperation();
			op.RunSecondStep();

			// Make sure the relationship is still the same, and has the same references.
			Relationship relationship2 = db.Tables[1].Relationships[0];
			Assert.That(relationship2, Is.SameAs(relationship));
			Assert.That(relationship2.PrimaryKey, Is.SameAs(db.Tables[0].Keys[0]));
			Assert.That(relationship2.ForeignKey, Is.SameAs(db.Tables[1].Keys[0]));
			Assert.That(relationship2.ForeignKey.Columns[0].Name, Is.EqualTo("Column1"));
		}
	}

	[TestFixture]
	public class When_Removing_A_Column_From_A_Foreign_Key
	{
		[Test]
		public void The_Corresponding_Relationship_Should_Also_Have_The_Column_Removed()
		{
			// Create database and relationships
			Database db = RelationshipDatabaseLoader.GetDb();

			Relationship relationship = db.Tables[1].Relationships[0];
			Assert.That(relationship.PrimaryKey, Is.SameAs(db.Tables[0].Keys[0]));
			Assert.That(relationship.ForeignKey, Is.SameAs(db.Tables[1].Keys[0]));
			Assert.That(relationship.PrimaryKey.Columns[0].Name, Is.EqualTo("Column1"));
			Assert.That(relationship.PrimaryKey.Columns[1].Name, Is.EqualTo("Column2"));

			IKey originalKey = db.Tables[1].Keys[0];

			IKey newKey = new Key(originalKey.Name, originalKey.Keytype);
			newKey.Parent = new Table("Table2");
			newKey.Parent.AddColumn(new Column("Column2"));
			newKey.Description = "new description";
			newKey.AddColumn("Column2");

			KeyChangeOperation op = new KeyChangeOperation(db.Tables[1].Keys[0], newKey);
			op.RunOperation();
			op.RunSecondStep();

			// Make sure the relationship is still the same, and has the same references.
			Relationship relationship2 = db.Tables[1].Relationships[0];
			Assert.That(relationship2, Is.SameAs(relationship));
			Assert.That(relationship2.PrimaryKey, Is.SameAs(db.Tables[0].Keys[0]));
			Assert.That(relationship2.ForeignKey, Is.SameAs(db.Tables[1].Keys[0]));
			Assert.That(relationship2.PrimaryKey.Columns, Has.Count(2));
			Assert.That(relationship2.ForeignKey.Columns, Has.Count(1));
			Assert.That(relationship2.ForeignKey.Columns[0].Name, Is.EqualTo("Column2"));
		}
	}

	[TestFixture]
	public class When_Removing_A_Foreign_Key
	{
		[Test]
		public void The_Corresponding_Relationship_Should_Also_Be_Removed()
		{
			// Create database and relationships
			Database db = RelationshipDatabaseLoader.GetDb();

			KeyRemovalOperation op = new KeyRemovalOperation(db.Tables[1].Keys[0]);
			op.RunOperation();

			Assert.That(db.Tables[1].Relationships, Is.Empty);
		}
	}

	internal static class RelationshipDatabaseLoader
	{
		internal static Database GetDb()
		{
			var db = TestDatabaseLoader.TestDatabase();
			Table table2 = new Table("Table2");
			table2.AddColumn(new Column("Column1"));
			table2.AddColumn(new Column("Column2"));
			table2.AddColumn(new Column("Column3"));
			Key foreignKey = new Key("FK_Table2", DatabaseKeyType.Foreign);
			foreignKey.Parent = table2;
			foreignKey.ReferencedKey = db.Tables[0].Keys[0];
			foreignKey.AddColumn("Column1");
			foreignKey.AddColumn("Column2");
			table2.AddKey(foreignKey);
			db.AddTable(table2);

			new DatabaseProcessor().CreateRelationships(db);
			return db;
		}
	}
}
