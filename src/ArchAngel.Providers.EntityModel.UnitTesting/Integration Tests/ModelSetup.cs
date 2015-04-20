using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;

namespace ArchAngel.Providers.EntityModel.UnitTesting.Integration_Tests
{
	public static class ModelSetup
	{
		public static MappingSet SetupModel()
		{
			MappingSet ms = new MappingSetImpl();

			SetupDatabase(ms.Database);
			SetupEntities(ms.EntitySet);

			SetupMappings(ms);

			return ms;
		}

		private static void SetupMappings(MappingSet set)
		{
			MapAllColumns(set, set.Database.Tables[0], set.EntitySet.Entities[0]);
			MapAllColumns(set, set.Database.Tables[1], set.EntitySet.Entities[1]);

			set.ChangeMappingFor(set.EntitySet.Entities[0].References[0])
				.To(set.Database.Tables[0].Relationships[0]);
		}

		private static void MapAllColumns(MappingSet set, ITable table, Entity entity)
		{
			for(int i = 0; i < table.Columns.Count; i++)
			{
				set.ChangeMappedColumnFor(entity.ConcreteProperties[i]).To(table.Columns[i]);
			}
		}

		private static void SetupEntities(EntitySet set)
		{
			var entity1 = CreateEntity(set, "Entity1");
			var entity2 = CreateEntity(set, "Entity2");

			entity1.CreateReferenceTo(entity2);
		}

		private static Entity CreateEntity(EntitySet set, string entityName)
		{
			var entity = new EntityImpl(entityName);
			entity.AddProperty(new PropertyImpl("Property1"));
			entity.AddProperty(new PropertyImpl("Property2"));
			entity.AddProperty(new PropertyImpl("Property3"));

			set.AddEntity(entity);

			return entity;
		}

		private static void SetupDatabase(IDatabase db)
		{
			var table1 = CreateTable(db, "Table1");
			var table2 = CreateTable(db, "Table2");

			table1.CreateRelationshipUsing(table1.Keys[0], table2.Keys[1]);
		}

		private static ITable CreateTable(IDatabase db, string tableName)
		{
			var table = new Table(tableName);
			table.AddColumn(new Column("Column1"));
			table.AddColumn(new Column("Column2"));
			table.AddColumn(new Column("Column3"));

			var primaryKey = new Key("PrimaryKey", DatabaseKeyType.Primary);
			table.AddKey(primaryKey);
			var foreignKey = new Key("ForeignKey", DatabaseKeyType.Foreign);
			table.AddKey(foreignKey);

			primaryKey.AddColumn("Column1");
			foreignKey.AddColumn("Column1");

			db.AddTable(table);

			return table;
		}
	}
}
