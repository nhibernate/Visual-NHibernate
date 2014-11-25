using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Specs_For_Merge_Operation_Text
{
	[TestFixture]
	public class TableOperations 
	{
		[Test]
		public void Addition()
		{
			IDatabase db = new Database("DB1");
			Table table = new Table("Table2");
			IMergeOperation<ITable> op = new TableAdditionOperation(db, table);

			Assert.That(op.ToString(), Is.EqualTo("DB1.Table2 Added"));
		}

		[Test]
		public void Removed()
		{
			IDatabase db = new Database("DB1");
			Table table = new Table("Table2");
			table.Database = db;

			IMergeOperation<ITable> op = new TableRemovalOperation(table);

			StringAssert.Contains("DB1.Table2 Removed", op.ToString());
		}
	}

	[TestFixture]
	public class IndexOperations
	{
		[Test]
		public void Addition()
		{
			IIndex index = GetIndex();
			IMergeOperation<IIndex> op = new IndexAdditionOperation(index.Parent, index);

			Assert.That(op.ToString(), Is.EqualTo("DB1.Table2.Index1 Added"));
		}

		[Test]
		public void Removed()
		{
			IIndex index = GetIndex();
			IMergeOperation<IIndex> op = new IndexRemovalOperation(index);

			Assert.That(op.ToString(), Is.EqualTo("DB1.Table2.Index1 Removed"));
		}

		[Test]
		public void Modified()
		{
			IIndex index = GetIndex();
			IMergeOperation<IIndex> op = new IndexChangeOperation(index, new Index("Index1"));

			Assert.That(op.ToString(), Is.EqualTo("DB1.Table2.Index1 Changed"));
		}

		private static IIndex GetIndex()
		{
			IDatabase db = new Database("DB1");
			Table table = new Table("Table2");
			table.Database = db;
			Index index = new Index("Index1");
			index.Parent = table;
			return index;
		}
	}

	[TestFixture]
	public class KeyOperations
	{
		[Test]
		public void Addition()
		{
			IKey key = GetKey();
			IMergeOperation<IKey> op = new KeyAdditionOperation(key.Parent, key);

			Assert.That(op.ToString(), Is.EqualTo("DB1.Table2.KeyV Added"));
		}

		[Test]
		public void Removed()
		{
			IKey key = GetKey();
			IMergeOperation<IKey> op = new KeyRemovalOperation(key);

			Assert.That(op.ToString(), Is.EqualTo("DB1.Table2.KeyV Removed"));
		}

		[Test]
		public void Modified()
		{
			IKey key = GetKey();
			IMergeOperation<IKey> op = new KeyChangeOperation(key, new Key("KeyV"));

			Assert.That(op.ToString(), Is.EqualTo("DB1.Table2.KeyV Changed"));
		}

		private static IKey GetKey()
		{
			IDatabase db = new Database("DB1");
			Table table = new Table("Table2");
			table.Database = db;
			IKey key = new Key("KeyV");
			key.Parent = table;
			return key;
		}
	}

	[TestFixture]
	public class ColumnOperations
	{
		[Test]
		public void Addition()
		{
			IColumn column = GetColumn();
			IMergeOperation<IColumn> op = new ColumnAdditionOperation(column.Parent as Table, column);

		    string actual = op.ToString();
		    StringAssert.Contains("Column0011", actual);
            StringAssert.Contains("Added", actual);
		}

		[Test]
		public void Removed()
		{
			IColumn column = GetColumn();
			IMergeOperation<IColumn> op = new ColumnRemovalOperation(column);

		    string actual = op.ToString();
		    StringAssert.Contains("Column0011", actual);
            StringAssert.Contains("Removed", actual);
		}

		[Test]
		public void Modified()
		{
			IColumn column = GetColumn();
			IMergeOperation<IColumn> op = new ColumnChangeOperation(column, new Column("Column0011"));

            string actual = op.ToString();
            StringAssert.Contains("Column0011", actual);
            StringAssert.Contains("Changed", actual);
		}

		private static IColumn GetColumn()
		{
			IDatabase db = new Database("DB1");
			Table table = new Table("Table2");
			table.Database = db;
			Column column = new Column("Column0011");
			column.Parent = table;
			return column;
		}
	}
}
