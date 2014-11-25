using System.IO;
using System.Linq;
using ArchAngel.NHibernateHelper;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Slyce.Common;

namespace NHibernate.Specs_For_Loading_From_HBMs__Entities
{
	[TestFixture]
	public class When_Given_A_Basic_HBM_For_One_Entity
	{
		private EntityLoader loader;
		private MappingSet mappingSet;
		private Database database;
		private Table table;
		private readonly string path = Path.Combine("Resources", "basic.hbm.xml");
		[SetUp]
		public void Setup()
		{
			loader = new EntityLoader(MockRepository.GenerateStub<IFileController>());

			database = new Database("DB1");
			table = new Table("Table1");
			table.AddColumn(new Column("ID") { Datatype = "int" });
			table.AddColumn(new Column("Column1"));
			table.AddColumn(new Column("Column2"));
			database.AddEntity(table);

			// Call we are testing
			mappingSet = loader.GetEntities(new[] { path }, database);
		}

		[Test]
		public void It_Creates_One_Entity_Object()
		{
			var entities = mappingSet.EntitySet;

			Assert.That(entities.Entities, Has.Count(1));

			var entity = entities.GetEntity("BasicClass1");
			Assert.That(entity, Is.Not.Null);

			Assert.That(entity.Name, Is.EqualTo("BasicClass1"));
		}

		[Test]
		public void It_Fills_The_Properties_In()
		{
			var entity = mappingSet.EntitySet.GetEntity("BasicClass1");
			Assert.That(entity.Properties, Has.Count(3));

			var property = entity.Properties.ElementAt(0);

			Assert.That(property.Name, Is.EqualTo("ID"));
			Assert.That(property.Type, Is.EqualTo("int"));

			property = entity.Properties.ElementAt(1);
			Assert.That(property.Name, Is.EqualTo("Property1"));
			Assert.That(property.Type, Is.EqualTo("System.String"));

			property = entity.Properties.ElementAt(2);
			Assert.That(property.Name, Is.EqualTo("Property2"));
			Assert.That(property.Type, Is.EqualTo("Test.Class1"));
		}

		[Test]
		public void It_Hooks_Up_The_Mappings()
		{
			var entity = mappingSet.EntitySet.GetEntity("BasicClass1");

			IColumn columnID = entity.GetProperty("ID").MappedColumn();
			Assert.That(columnID, Is.Not.Null);
			Assert.That(columnID.Name, Is.EqualTo("ID"));

			IColumn column1 = entity.GetProperty("Property1").MappedColumn();
			Assert.That(column1, Is.Not.Null);
			Assert.That(column1.Name, Is.EqualTo("Column1"));

			IColumn column2 = entity.GetProperty("Property2").MappedColumn();
			Assert.That(column2, Is.Not.Null);
			Assert.That(column2.Name, Is.EqualTo("Column2"));
		}
	}
}
