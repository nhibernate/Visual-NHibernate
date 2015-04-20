using System.Linq;
using ArchAngel.Interfaces;
using ArchAngel.Providers.EntityModel.Controller.MappingLayer;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Specs_For_Creating_Tables_From_Entities
{
	[TestFixture]
	public class When_Creating_A_Table_From_A_Single_Entity
	{
		private Entity entity;
		[SetUp]
		public void SetUp()
		{
			entity = new EntityImpl("Entity1");
			entity.AddProperty(new PropertyImpl("Property1") { Type = "System.Int32", IsKeyProperty = true });
			entity.AddProperty(new PropertyImpl("Property2") { Type = "bool" });
			entity.AddProperty(new PropertyImpl("Property3") { Type = "string", ValidationOptions = new ValidationOptions {MaximumLength = 50} });
		}

		[Test]
		public void It_Creates_A_Simple_Table()
		{
			var proc = new OneToOneEntityProcessor();
			var table = proc.CreateTable(entity);

			Assert.That(table.Name, Is.EqualTo("Entity1s"), "Name should be the Entity Name pluralized");
			Assert.That(table.Columns, Has.Count(3));
		}

		[Test]
		public void It_Creates_A_Single_Primary_Key_With_One_Column()
		{
			var proc = new OneToOneEntityProcessor();
			var table = proc.CreateTable(entity);

			Assert.That(table.Keys, Has.Count(1));
			var firstKey = table.Keys.First();
			Assert.That(firstKey.Keytype, Is.EqualTo(DatabaseKeyType.Primary));
			Assert.That(firstKey.Columns, Has.Count(1));
			Assert.That(firstKey.Columns[0], Is.SameAs(table.Columns[0]));
		}
	}

	[TestFixture]
	public class When_Creating_A_Column_From_A_Property
	{
		[Test]
		public void String_Type_Defaults_To_NText()
		{
			var proc = new OneToOneEntityProcessor();
			var column = proc.CreateColumn(new PropertyImpl("Property1") { Type = "System.String" });

			Assert.That(column.Name, Is.EqualTo("Property1"));
			Assert.That(column.Datatype, Is.EqualTo("ntext"));
		}

		[Test]
		public void The_Type_Takes_The_Maximum_Length_Into_Account()
		{
			var proc = new OneToOneEntityProcessor();
			var column = proc.CreateColumn(new PropertyImpl("Property1") { Type = "System.String", ValidationOptions = new ValidationOptions {MaximumLength = 50} });

			Assert.That(column.Name, Is.EqualTo("Property1"));
			Assert.That(column.Size, Is.EqualTo(50));
			Assert.That(column.Datatype, Is.EqualTo("nvarchar"));
		}
	}

	[TestFixture]
	public class When_Creating_A_Tables_From_Multiple_Entities_ManyToOne
	{
		private EntitySet entities;
		private Entity entity1;
		private Entity entity2;
		private Reference reference;

		[SetUp]
		public void SetUp()
		{
			entities = new EntitySetImpl();

			entity1 = new EntityImpl("Entity1");
			entity1.AddProperty(new PropertyImpl("PrimaryKey") { Type = "System.Int32", IsKeyProperty = true });
			entity1.AddProperty(new PropertyImpl("Property") { Type = "bool" });

			entity2 = new EntityImpl("Entity2");
			entity2.AddProperty(new PropertyImpl("PrimaryKey") { Type = "System.Int32", IsKeyProperty = true });

			entities.AddEntity(entity1);
			entities.AddEntity(entity2);

			reference = entity1.CreateReferenceTo(entity2);
			reference.Cardinality1 = Cardinality.One;
			reference.Cardinality2 = Cardinality.Many;
		}

		[Test]
		public void It_Creates_A_Column_On_The_Many_Side()
		{
			var proc = new MappingProcessor(new OneToOneEntityProcessor());
			var mappingSet = proc.CreateOneToOneMapping(entities);

			var table1 = mappingSet.Database.Tables[1];
			var table2 = mappingSet.Database.Tables[1];

			Assert.That(table2.Columns, Has.Count(2)); // Extra column added
			var newColumn = table2.Columns[1];
			Assert.That(newColumn.Datatype, Is.SameAs(table1.Columns[0].Datatype));
		}

		[Test]
		public void It_Creates_A_Foreign_Key_On_The_Many_Side()
		{
			var proc = new MappingProcessor(new OneToOneEntityProcessor());
			var mappingSet = proc.CreateOneToOneMapping(entities);

			var table1 = mappingSet.Database.Tables[0];
			var table2 = mappingSet.Database.Tables[1];
			
			Assert.That(table2.Keys, Has.Count(2), "Many table did not get the foreign key");
			var key = table2.Keys.FirstOrDefault(k => k.Keytype == DatabaseKeyType.Foreign);
			var primaryKey = table1.Keys.FirstOrDefault(k => k.Keytype == DatabaseKeyType.Primary);

			Assert.That(key, Is.Not.Null);
			Assert.That(key.ReferencedKey, Is.Not.Null);
			Assert.That(key.ReferencedKey, Is.SameAs(primaryKey));
		}

		[Test]
		public void It_Creates_A_Single_Relationship_With_The_Right_Keys()
		{
			var proc = new MappingProcessor(new OneToOneEntityProcessor());
			var mappingSet = proc.CreateOneToOneMapping(entities);

			var table1 = mappingSet.Database.Tables[0];
			var table2 = mappingSet.Database.Tables[1];
			
			var foreignKey = table2.Keys.FirstOrDefault(k => k.Keytype == DatabaseKeyType.Foreign);
			var primaryKey = table1.Keys.FirstOrDefault(k => k.Keytype == DatabaseKeyType.Primary);
			
			Assert.That(table1.Relationships, Has.Count(1));
			var relationship = table1.Relationships[0];
			Assert.That(relationship.PrimaryKey, Is.SameAs(primaryKey));
			Assert.That(relationship.ForeignKey, Is.SameAs(foreignKey));
		}
	}

	[TestFixture]
	public class When_Creating_Tables_From_Multiple_Entities_OneToOne
	{
		private EntitySet entities;
		private Entity entity1;
		private Entity entity2;
		private Reference reference;

		[SetUp]
		public void SetUp()
		{
			entities = new EntitySetImpl();

			entity1 = new EntityImpl("Entity1");
			entity1.AddProperty(new PropertyImpl("PrimaryKey") { Type = "System.Int32", IsKeyProperty = true });
			entity1.AddProperty(new PropertyImpl("Property") { Type = "bool" });

			entity2 = new EntityImpl("Entity2");
			entity2.AddProperty(new PropertyImpl("PrimaryKey") { Type = "System.Int32", IsKeyProperty = true });

			entities.AddEntity(entity1);
			entities.AddEntity(entity2);

			reference = entity1.CreateReferenceTo(entity2);
			reference.Cardinality1 = Cardinality.One;
			reference.Cardinality2 = Cardinality.One;
		}

		[Test]
		public void It_Creates_A_Foreign_Key_On_The_Second_Entity()
		{
			var proc = new MappingProcessor(new OneToOneEntityProcessor());
			var mappingSet = proc.CreateOneToOneMapping(entities);

			var table1 = mappingSet.Database.Tables[0];
			var table2 = mappingSet.Database.Tables[1];

			Assert.That(table2.Keys, Has.Count(3), "The second table did not get the new keys");
			var key = table2.Keys.FirstOrDefault(k => k.Keytype == DatabaseKeyType.Foreign);
			var primaryKey = table1.Keys.FirstOrDefault(k => k.Keytype == DatabaseKeyType.Primary);

			Assert.That(key, Is.Not.Null);
			Assert.That(key.ReferencedKey, Is.Not.Null);
			Assert.That(key.ReferencedKey, Is.SameAs(primaryKey));
		}

		[Test]
		public void It_Creates_A_Unique_Key_On_The_Second_Entity()
		{
			var proc = new MappingProcessor(new OneToOneEntityProcessor());
			var mappingSet = proc.CreateOneToOneMapping(entities);

			var table2 = mappingSet.Database.Tables[1];

			Assert.That(table2.Keys, Has.Count(3), "The second table did not get the new keys");
			var uniqueKey = table2.Keys.FirstOrDefault(k => k.Keytype == DatabaseKeyType.Unique);
			var foreignKey = table2.Keys.FirstOrDefault(k => k.Keytype == DatabaseKeyType.Foreign);

			Assert.That(uniqueKey, Is.Not.Null);
			Assert.That(uniqueKey.Columns, Has.Count(1));
			Assert.That(uniqueKey.Columns[0], Is.SameAs(foreignKey.Columns[0]));
		}

		[Test]
		public void It_Creates_A_Single_Relationship_With_The_Right_Keys()
		{
			var proc = new MappingProcessor(new OneToOneEntityProcessor());
			var mappingSet = proc.CreateOneToOneMapping(entities);

			var table1 = mappingSet.Database.Tables[0];
			var table2 = mappingSet.Database.Tables[1];

			var foreignKey = table2.Keys.FirstOrDefault(k => k.Keytype == DatabaseKeyType.Foreign);
			var primaryKey = table1.Keys.FirstOrDefault(k => k.Keytype == DatabaseKeyType.Primary);

			Assert.That(table1.Relationships, Has.Count(1), "The relationship was not created.");
			Assert.That(table2.Relationships, Has.Count(1), "The relationship was not created.");
			var relationship = table1.Relationships[0];
			Assert.That(relationship.PrimaryKey, Is.SameAs(primaryKey));
			Assert.That(relationship.ForeignKey, Is.SameAs(foreignKey));
		}
	}

	[TestFixture]
	public class When_Creating_Tables_From_Multiple_Entities_ManyToMany
	{
		private EntitySet entities;
		private Entity entity1;
		private Entity entity2;
		private Reference reference;
		private MappingSet mappingSet;

		[SetUp]
		public void SetUp()
		{
			entities = new EntitySetImpl();

			entity1 = new EntityImpl("Entity1");
			entity1.AddProperty(new PropertyImpl("PrimaryKey") { Type = "System.Int32", IsKeyProperty = true });

			entity2 = new EntityImpl("Entity2");
			entity2.AddProperty(new PropertyImpl("PrimaryKey") { Type = "System.Int32", IsKeyProperty = true });

			entities.AddEntity(entity1);
			entities.AddEntity(entity2);

			reference = entity1.CreateReferenceTo(entity2);
			reference.Cardinality1 = Cardinality.Many;
			reference.Cardinality2 = Cardinality.Many;

			var proc = new MappingProcessor(new OneToOneEntityProcessor());
			mappingSet = proc.CreateOneToOneMapping(entities);
		}

		[Test]
		public void It_Should_Create_An_Association_Table_Mapped_To_The_Reference()
		{		
			var mappedTable = reference.MappedTable();
			
			Assert.That(mappedTable, Is.Not.Null);
			Assert.That(mappedTable.Columns, Has.Count(2));
			Assert.That(mappedTable.Keys, Has.Count(2));

			var entity1Table = entity1.MappedTables().First();
			var entity2Table = entity2.MappedTables().First();

			var table1ForeignKey = mappedTable.Keys.FirstOrDefault(k => k.Keytype == DatabaseKeyType.Foreign && k.ReferencedKey == entity1Table.FirstPrimaryKey);
			var table2ForeignKey = mappedTable.Keys.FirstOrDefault(k => k.Keytype == DatabaseKeyType.Foreign && k.ReferencedKey == entity2Table.FirstPrimaryKey);

			Assert.That(table1ForeignKey, Is.Not.Null);
			Assert.That(table1ForeignKey.Columns, Has.Count(1));

			Assert.That(table2ForeignKey, Is.Not.Null);
			Assert.That(table2ForeignKey.Columns, Has.Count(1));
		}

		[Test]
		public void It_Should_Create_A_Relationship_From_The_First_Table_To_The_Association_Table()
		{
			var mappedTable = reference.MappedTable();

			Assert.That(mappedTable, Is.Not.Null);

			var entity1Table = entity1.MappedTables().First();
			Assert.That(entity1Table.Relationships, Has.Count(1));
			
			var relationship = entity1Table.DirectedRelationships.First();
			Assert.That(relationship.ToEntity, Is.SameAs(mappedTable));
		}

		[Test]
		public void It_Should_Create_A_Relationship_From_The_Second_Table_To_The_Association_Table()
		{
			var mappedTable = reference.MappedTable();

			Assert.That(mappedTable, Is.Not.Null);

			var entity2Table = entity2.MappedTables().First();
			Assert.That(entity2Table.Relationships, Has.Count(1));

			var relationship = entity2Table.DirectedRelationships.First();
			Assert.That(relationship.ToEntity, Is.SameAs(mappedTable));
		}
	}

	[TestFixture]
	public class When_Creating_Tables_From_Entities_With_Inheritance
	{
		private EntitySet entities;
		private Entity entityParent;
		private Entity entityChild;
		private MappingSet mappingSet;

		[SetUp]
		public void SetUp()
		{
			entities = new EntitySetImpl();

			entityParent = new EntityImpl("EntityParent");
			entityParent.AddProperty(new PropertyImpl("PrimaryKey") { Type = "int", IsKeyProperty = true });

			entityChild = new EntityImpl("EntityChild");
			entityParent.AddChild(entityChild);
			entityChild.CopyPropertyFromParent(entityParent.ConcreteProperties[0]);
			entityChild.AddProperty(new PropertyImpl("ActualProperty"){ Type = "string" });

			entities.AddEntity(entityParent);
			entities.AddEntity(entityChild);

			var proc = new MappingProcessor(new OneToOneEntityProcessor());
			mappingSet = proc.CreateOneToOneMapping(entities);
		}

		[Test]
		public void The_Parent_Entity_Should_Generate_A_Single_Regular_Table()
		{
			// We don't bother checking the table closely, it should just be using the table creation code
			// which has already been tested.
			Assert.That(entityParent.MappedTables().ToList(), Has.Count(1));
		}

		[Test]
		public void The_Child_Entity_Should_Generate_A_Table_With_A_Foreign_Key_To_The_Parent_Table()
		{
			var childTable = entityChild.MappedTables().First();
			var parentTable = entityParent.MappedTables().First();

			Assert.That(childTable.Columns, Has.Count(2));

			var childPrimaryKey = childTable.FirstPrimaryKey;
			var childForeignKey = childTable.ForeignKeys.First();

			Assert.That(childForeignKey.Columns, Has.Count(childPrimaryKey.Columns.Count), "Foreign Key should contain all of the columns in the Primary Key.");
			Assert.That(childForeignKey.Columns[0], Is.EqualTo(childPrimaryKey.Columns[0]));
			Assert.That(childForeignKey.ReferencedKey, Is.SameAs(parentTable.FirstPrimaryKey));

			Assert.That(entityChild.ConcreteProperties[0].IsKeyProperty);
			Assert.That(entityChild.ConcreteProperties[0].MappedColumn(), Is.SameAs(childTable.Columns[0]));
		}
	}

	[TestFixture]
	public class When_Creating_Tables_From_An_Inherited_Entity
	{
		private EntitySet entities;
		private Entity personEntity;
		private Entity managerEntity;
		private Reference reference;

		[SetUp]
		public void SetUp()
		{
			entities = new EntitySetImpl();

			personEntity = new EntityImpl("Person");
			personEntity.AddProperty(new PropertyImpl("ID") { Type = "int", IsKeyProperty = true });
			personEntity.AddProperty(new PropertyImpl("Name") { Type = "string", ValidationOptions = new ValidationOptions {MaximumLength = 50} });
			personEntity.AddProperty(new PropertyImpl("Salary") { Type = "int" });

			managerEntity = new EntityImpl("Manager");
			managerEntity.Parent = personEntity;

			entities.AddEntity(personEntity);
			entities.AddEntity(managerEntity);

			reference = managerEntity.CreateReferenceTo(personEntity);
			reference.End1Enabled = true; reference.End2Enabled = true;
			reference.End1Name = "Subordinates"; reference.End2Name = "Manager";
			reference.Cardinality1 = Cardinality.Many; reference.Cardinality2 = Cardinality.One;
		}
	}
}
