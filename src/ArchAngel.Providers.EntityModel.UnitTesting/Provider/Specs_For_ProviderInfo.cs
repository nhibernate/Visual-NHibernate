using System.Collections.Generic;
using System.IO;
using System.Linq;
using ArchAngel.Interfaces;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Key=ArchAngel.Providers.EntityModel.Model.DatabaseLayer.Key;
using ProviderInfo=ArchAngel.Providers.EntityModel.ProviderInfo;

namespace Specs_For_ProviderInfo
{
	[TestFixture]
	public class When_Saving_The_Current_State
	{
		private string tempFolder;

		[SetUp]
		public void Setup()
		{
			tempFolder = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(Path.GetTempFileName()));
			Directory.CreateDirectory(tempFolder);
		}

		[TearDown]
		public void TearDown()
		{
			Directory.Delete(tempFolder, true);
		}

		[Test]
		public void The_Correct_Files_Should_Be_Written_To()
		{
			ProviderInfo info = new ProviderInfo();
			info.Save(tempFolder);
			
			Assert.That(File.Exists(Path.Combine(tempFolder, "mappings.xml")));
			Assert.That(File.Exists(Path.Combine(tempFolder, "entities.xml")));
			Assert.That(File.Exists(Path.Combine(tempFolder, "database.xml")));
		}
	}

	[TestFixture]
	public class When_Requesting_Database_Objects
	{
		[Test]
		public void It_Returns_The_One_Database_Object()
		{
			ProviderInfo info = new ProviderInfo();

			IEnumerable<IScriptBaseObject> objects = info.GetAllObjectsOfType(typeof(IDatabase));

			Assert.That(objects, Has.Length(1));
			Assert.That(objects.ElementAt(0), Is.SameAs(info.MappingSet.Database));
		}
	}

	[TestFixture]
	public class When_Requesting_MappingSet_Objects
	{
		[Test]
		public void It_Returns_The_One_MappingSet_Object()
		{
			ProviderInfo info = new ProviderInfo();

			IEnumerable<IScriptBaseObject> objects = info.GetAllObjectsOfType(typeof(MappingSet));

			Assert.That(objects, Has.Length(1));
			Assert.That(objects.ElementAt(0), Is.SameAs(info.MappingSet));
		}
	}

	[TestFixture]
	public class When_Requesting_Mapping_Objects
	{
		[Test]
		public void It_Returns_The_Mapping_Objects()
		{
			ProviderInfo info = new ProviderInfo();

			var mapping1 = new MappingImpl();
			var mapping2 = new MappingImpl();
			info.MappingSet.AddMapping(mapping1);
			info.MappingSet.AddMapping(mapping2);

			IEnumerable<IScriptBaseObject> objects = info.GetAllObjectsOfType(typeof(Mapping));

			Assert.That(objects, Has.Length(2));
			Assert.That(objects.Contains(mapping1), "Missing mapping1");
			Assert.That(objects.Contains(mapping2), "Missing mapping2");
		}
	}

	[TestFixture]
	public class When_Requesting_EntitySet_Objects
	{
		[Test]
		public void It_Returns_The_One_MappingSet_Object()
		{
			ProviderInfo info = new ProviderInfo();
			IEnumerable<IScriptBaseObject> objects = info.GetAllObjectsOfType(typeof(EntitySet));

			Assert.That(objects, Has.Length(1));
			Assert.That(objects.ElementAt(0), Is.SameAs(info.MappingSet.EntitySet));
		}
	}

	[TestFixture]
	public class When_Requesting_Entity_Objects
	{
		[Test]
		public void It_Returns_The_Entity_Objects()
		{
			ProviderInfo info = new ProviderInfo();

			var entity = new EntityImpl();
			info.MappingSet.EntitySet.AddEntity(entity);

			IEnumerable<IScriptBaseObject> objects = info.GetAllObjectsOfType(typeof(Entity));

			Assert.That(objects, Has.Length(1));
			Assert.That(objects.ElementAt(0), Is.SameAs(entity));
		}
	}

	[TestFixture]
	public class When_Requesting_IEntityObject_Objects
	{
		[Test]
		public void It_Returns_The_ITable_Objects()
		{
			ProviderInfo info = new ProviderInfo();

			var table = new Table("T1");
			var table2 = new Table("T2");
			info.MappingSet.Database.AddTable(table);
			info.MappingSet.Database.AddTable(table2);

			IEnumerable<IScriptBaseObject> objects = info.GetAllObjectsOfType(typeof(ITable));

			Assert.That(objects, Has.Length(2));
			Assert.That(objects.Contains(table));
			Assert.That(objects.Contains(table2));
		}
	}

	[TestFixture]
	public class When_Requesting_ITable_Objects
	{
		[Test]
		public void It_Returns_The_ITable_Objects()
		{
			ProviderInfo info = new ProviderInfo();

			var table = new Table();
			info.MappingSet.Database.AddTable(table);

			IEnumerable<IScriptBaseObject> objects = info.GetAllObjectsOfType(typeof(ITable));

			Assert.That(objects, Has.Length(1));
			Assert.That(objects.ElementAt(0), Is.SameAs(table));
		}
	}

	[TestFixture]
	public class When_Requesting_IColumn_Objects
	{
		[Test]
		public void It_Returns_The_IColumn_Objects()
		{
			ProviderInfo info = new ProviderInfo();

			var table1 = new Table();
			var column1 = new Column("Col1");
			table1.AddColumn(column1);
			var table2 = new Table();
			var column2 = new Column("Col2");
			table2.AddColumn(column2);
			info.MappingSet.Database.AddTable(table1);
			info.MappingSet.Database.AddTable(table2);

			IEnumerable<IScriptBaseObject> objects = info.GetAllObjectsOfType(typeof(IColumn));

			Assert.That(objects, Has.Length(2));
			Assert.That(objects.Contains(column1));
			Assert.That(objects.Contains(column2));
		}
	}

	[TestFixture]
	public class When_Requesting_IIndex_Objects
	{
		[Test]
		public void It_Returns_The_IIndex_Objects()
		{
			ProviderInfo info = new ProviderInfo();

			var table1 = new Table();
			var index1 = new Index("1");
			table1.AddIndex(index1);
			var table2 = new Table();
			var index2 = new Index("2");
			table2.AddIndex(index2);
			info.MappingSet.Database.AddTable(table1);
			info.MappingSet.Database.AddTable(table2);

			IEnumerable<IScriptBaseObject> objects = info.GetAllObjectsOfType(typeof(IIndex));

			Assert.That(objects, Has.Length(2));
			Assert.That(objects.Contains(index1));
			Assert.That(objects.Contains(index2));
		}
	}

	[TestFixture]
	public class When_Requesting_IKey_Objects
	{
		[Test]
		public void It_Returns_The_IKey_Objects()
		{
			ProviderInfo info = new ProviderInfo();

			var table1 = new Table();
			var index1 = new Key("1");
			table1.AddKey(index1);
			var table2 = new Table();
			var index2 = new Key("2");
			table2.AddKey(index2);
			info.MappingSet.Database.AddTable(table1);
			info.MappingSet.Database.AddTable(table2);

			IEnumerable<IScriptBaseObject> objects = info.GetAllObjectsOfType(typeof(IKey));

			Assert.That(objects, Has.Length(2));
			Assert.That(objects.Contains(index1));
			Assert.That(objects.Contains(index2));
		}
	}

	[TestFixture]
	public class When_Requesting_Relationship_Objects
	{
		[Test]
		public void It_Returns_The_Relationship_Objects()
		{
			ProviderInfo info = new ProviderInfo();

			var entity1 = new Table("T1");
			var entity2 = new Table("T2");
			var entity3 = new Table("T3");
			var rel1 = new RelationshipImpl();
			var rel2 = new RelationshipImpl();

			info.MappingSet.Database.AddEntity(entity1);
			info.MappingSet.Database.AddEntity(entity2);
			info.MappingSet.Database.AddEntity(entity3);

			rel1.AddThisTo(entity1, entity2);
			rel2.AddThisTo(entity3, entity2);

			IEnumerable<IScriptBaseObject> objects = info.GetAllObjectsOfType(typeof(Relationship));

			Assert.That(objects, Has.Length(2));
			Assert.That(objects.Contains(rel1));
			Assert.That(objects.Contains(rel2));
		}
	}

	[TestFixture]
	public class When_Requesting_Reference_Objects
	{
		[Test]
		public void It_Returns_The_Reference_Objects()
		{
			ProviderInfo info = new ProviderInfo();

			var entity1 = new EntityImpl("T1");
			var entity2 = new EntityImpl("T2");
			var entity3 = new EntityImpl("T3");
			var rel1 = new ReferenceImpl { EntitySet = info.MappingSet.EntitySet };
			var rel2 = new ReferenceImpl { EntitySet = info.MappingSet.EntitySet };

			info.MappingSet.EntitySet.AddEntity(entity1);
			info.MappingSet.EntitySet.AddEntity(entity2);
			info.MappingSet.EntitySet.AddEntity(entity3);
			
			rel1.AddThisTo(entity1, entity2);
			rel2.AddThisTo(entity3, entity2);

			IEnumerable<IScriptBaseObject> objects = info.GetAllObjectsOfType(typeof(Reference));

			Assert.That(objects, Has.Length(2));
			Assert.That(objects.Contains(rel1));
			Assert.That(objects.Contains(rel2));
		}
	}

	[TestFixture]
	public class When_Requesting_Property_Objects
	{
		[Test]
		public void It_Returns_The_Property_Objects()
		{
			ProviderInfo info = new ProviderInfo();

			var entity1 = new EntityImpl();
			var property1 = new PropertyImpl("1");
			entity1.AddProperty(property1);
			var entity2 = new EntityImpl();
			var property2 = new PropertyImpl("2");
			entity2.AddProperty(property2);
			info.MappingSet.EntitySet.AddEntity(entity1);
			info.MappingSet.EntitySet.AddEntity(entity2);

			IEnumerable<IScriptBaseObject> objects = info.GetAllObjectsOfType(typeof(Property));

			Assert.That(objects, Has.Length(2));
			Assert.That(objects.Contains(property1));
			Assert.That(objects.Contains(property2));
		}
	}

	[TestFixture]
	public class When_Requesting_EntityKey_Objects
	{
		[Test]
		public void It_Returns_The_EntityKey_Objects()
		{
			ProviderInfo info = new ProviderInfo();

			var entity1 = new EntityImpl();
			var entity2 = new EntityImpl();
			info.MappingSet.EntitySet.AddEntity(entity1);
			info.MappingSet.EntitySet.AddEntity(entity2);

			IEnumerable<IScriptBaseObject> objects = info.GetAllObjectsOfType(typeof(EntityKey));

			Assert.That(objects, Has.Length(2));
			Assert.That(objects.Contains(entity1.Key));
			Assert.That(objects.Contains(entity2.Key));
		}
	}
}
