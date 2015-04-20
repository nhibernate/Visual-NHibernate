using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.UnitTesting.Integration_Tests;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Specs_For_Keys
{
	[TestFixture]
	[Category("IntegrationTests")]
	public class When_Deleting_A_Key
	{
		private IDatabase database;
		private ITable table;
		private IKey key;

		[SetUp]
		public void Setup()
		{
			var mappingSet = ModelSetup.SetupModel();
			database = mappingSet.Database;
			table = database.Tables[0];
			key = table.Keys[0];
		}

		[Test]
		public void The_Key_Is_Removed_From_The_Table()
		{
			Assert.That(table.Keys, Has.Count(2));

			key.DeleteSelf();

			Assert.That(table.Keys, Has.Count(1));
		}

		[Test]
		public void The_ReferencedKey_No_Longer_References_It()
		{
			IKey referencedKey = new Key();
			referencedKey.ReferencedKey = key;
			key.ReferencedKey = referencedKey;

			key.DeleteSelf();

			Assert.That(referencedKey.ReferencedKey, Is.Null);
		}

		[Test]
		public void The_Relationship_Is_Removed_From_The_Table()
		{
			Assert.That(table.Relationships, Has.Count(1));

			key.DeleteSelf();

			Assert.That(table.Relationships, Has.Count(0));
		}
	}
}
