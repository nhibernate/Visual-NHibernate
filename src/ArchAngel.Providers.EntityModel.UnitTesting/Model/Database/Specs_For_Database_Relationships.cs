using System.Collections.Generic;
using System.Linq;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Specs_For_Database_Relationships
{
	[TestFixture]
	public class When_Given_A_Table_And_Degree_One
	{
		[Test]
		public void Returns_The_Table_And_Its_Immediately_Related_Tables__Central()
		{
			var setup = new SetupDatabase();

			IEnumerable<ITable> tables = setup.Db.GetRelatedEntities(setup.CentralTable, 1);
			Assert.That(tables, Has.Count(2));
			Assert.That(tables.Contains(setup.CentralTable));
			Assert.That(tables.Contains(setup.DegreeOneTable));
			Assert.That(tables.Contains(setup.DegreeTwoTable), Is.False);
		}

		[Test]
		public void Returns_The_Table_And_Its_Immediately_Related_Tables__DegreeOne()
		{
			var setup = new SetupDatabase();

			IEnumerable<ITable> tables = setup.Db.GetRelatedEntities(setup.DegreeOneTable, 1);
			Assert.That(tables, Has.Count(3));
			Assert.That(tables.Contains(setup.CentralTable));
			Assert.That(tables.Contains(setup.DegreeOneTable));
			Assert.That(tables.Contains(setup.DegreeTwoTable));
		}
	}

	[TestFixture]
	public class When_Given_A_Table_And_Degree_Two
	{
		[Test]
		public void Returns_The_Table_Its_Immediately_Related_Tables_And_Their_Related_Tables_Central()
		{
			var setup = new SetupDatabase();

			IEnumerable<ITable> tables = setup.Db.GetRelatedEntities(setup.CentralTable, 2);
			Assert.That(tables, Has.Count(3));
			Assert.That(tables.Contains(setup.CentralTable));
			Assert.That(tables.Contains(setup.DegreeOneTable));
			Assert.That(tables.Contains(setup.DegreeTwoTable));
		}

		[Test]
		public void Returns_The_Table_Its_Immediately_Related_Tables_And_Their_Related_Tables_DegreeOne()
		{
			var setup = new SetupDatabase();

			IEnumerable<ITable> tables = setup.Db.GetRelatedEntities(setup.DegreeOneTable, 2);
			Assert.That(tables, Has.Count(4));
			Assert.That(tables.Contains(setup.CentralTable));
			Assert.That(tables.Contains(setup.DegreeOneTable));
			Assert.That(tables.Contains(setup.DegreeTwoTable));
			Assert.That(tables.Contains(setup.DegreeThreeTable));
		}
	}

	[TestFixture]
	public class When_A_Relationships_Keys_Change
	{
		[Test]
		public void The_Change_Is_Reflected_In_Its_MappedColumns()
		{
			var table1 = new Table("Table1");
			table1.AddColumn(new Column("Column1"));
			table1.AddColumn(new Column("Column2"));
			var table2 = new Table("Table2");
			table2.AddColumn(new Column("Column1"));
			table2.AddColumn(new Column("Column2"));
			
			var key1 = new Key("PrimaryKey") { Keytype = DatabaseKeyType.Primary};
			table1.AddKey(key1);
			key1.AddColumn("Column1");
			var key2 = new Key("ForeignKey") { Keytype = DatabaseKeyType.Foreign };
			table2.AddKey(key2);
			key2.AddColumn("Column1");

			var relationship = new RelationshipImpl();
			relationship.PrimaryTable = table1;
			relationship.ForeignTable = table2;
			relationship.PrimaryKey = key1;
			relationship.ForeignKey = key2;

			Assert.That(relationship.MappedColumns.Count(), Is.EqualTo(1));

			MappedColumn mappedColumn = relationship.MappedColumns.ElementAt(0);

			Assert.That(mappedColumn.Source, Is.SameAs(table1.Columns[0]));
			Assert.That(mappedColumn.Target, Is.SameAs(table2.Columns[0]));

			key1.AddColumn("Column2");
			key2.AddColumn("Column2");

			Assert.That(relationship.MappedColumns.Count(), Is.EqualTo(2), "The new column was not added.");

			mappedColumn = relationship.MappedColumns.ElementAt(0);
			Assert.That(mappedColumn.Source, Is.SameAs(table1.Columns[0]), "The source column is wrong on item 0");
			Assert.That(mappedColumn.Target, Is.SameAs(table2.Columns[0]), "The target column is wrong on item 0");

			mappedColumn = relationship.MappedColumns.ElementAt(1);

			Assert.That(mappedColumn.Source, Is.SameAs(table1.Columns[1]), "The source column is wrong on item 1");
			Assert.That(mappedColumn.Target, Is.SameAs(table2.Columns[1]), "The target column is wrong on item 1");
		}
	}

	internal class SetupDatabase
	{
		internal ITable CentralTable;
		internal ITable DegreeOneTable;
		internal ITable DegreeTwoTable;
		internal ITable DegreeThreeTable;
		internal IDatabase Db;

		public SetupDatabase()
		{
			CreateDatabase();
		}

		internal void CreateDatabase()
		{
			Db = new Database("Database1");

			CentralTable = new Table("Table1");
			DegreeOneTable = new Table("Table2");
			DegreeTwoTable = new Table("Table3");
			DegreeThreeTable = new Table("Table4");

			Db.AddTable(CentralTable);
			Db.AddTable(DegreeOneTable);
			Db.AddTable(DegreeTwoTable);
			Db.AddTable(DegreeThreeTable);

			CreateRelationship(CentralTable, DegreeOneTable);
			CreateRelationship(DegreeOneTable, DegreeTwoTable);
			CreateRelationship(DegreeTwoTable, DegreeThreeTable);
		}

		internal void CreateRelationship(ITable source, ITable target)
		{
			var relationship = new RelationshipImpl();
			relationship.Name = source.Name + " : " + target.Name;
			relationship.AddThisTo(source, target);
		}
	}
}
