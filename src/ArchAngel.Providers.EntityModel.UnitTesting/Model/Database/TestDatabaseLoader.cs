using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;

namespace Specs_For_Databases
{
	public class TestDatabaseLoader
	{
		public static Database TestDatabase()
		{
			Database db = new Database("Test Database");

			var t1 = new Table("Table1");
			db.AddTable(t1);
			t1.AddColumn(new Column("Column1") { InPrimaryKey = true, Datatype = "int", OrdinalPosition = 0, Size = 4});
			t1.AddColumn(new Column("Column2") { Datatype = "nvarchar", OrdinalPosition = 1, Size = 100 });
			t1.AddColumn(new Column("Column3") { Datatype = "datetime", OrdinalPosition = 2, Size = 8 });

			t1.AddIndex(new Index("PK_Table1") { IsUnique = true, Datatype = DatabaseIndexType.PrimaryKey}).AddColumn("Column1");
			t1.AddKey(new Key("PK_Table1", DatabaseKeyType.Primary)).AddColumn("Column1").AddColumn("Column2");

			return db;
		}
	}
}