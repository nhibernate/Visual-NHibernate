using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.UnitTesting.Integration_Tests;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Specs_For_Keys
{
	[TestFixture]
	[Category("IntegrationTests")]
	public class When_Deleting_An_Index
	{
		private IDatabase database;
		private ITable table;
		private IIndex index;

		[SetUp]
		public void Setup()
		{
			var mappingSet = ModelSetup.SetupModel();
			database = mappingSet.Database;
			table = database.Tables[0];
			index = new Index("index1");
			table.AddIndex(index);
		}

		[Test]
		public void The_Index_Is_Removed_From_The_Table()
		{
			Assert.That(table.Indexes, Has.Count(1));

			index.DeleteSelf();

			Assert.That(table.Indexes, Has.Count(0));
		}
	}
}
