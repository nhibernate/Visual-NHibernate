using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Project.Model;

namespace TestGenUI
{
	[TestFixture]
	public class Specs_For_Region
	{
		Region region;
		private ISession session;
		private ITransaction transaction;

		[SetUp]
		public void RunTests()
		{
			session = NHSessionHelper.OpenSession();
			transaction = session.BeginTransaction();

            region = session.CreateCriteria(typeof(Region))
                    .Add(Restrictions.Eq("RegionID", 2)).UniqueResult<Region>();
		}

		[TearDown]
		public void TearDown()
		{
			transaction.Rollback();
			session.Dispose();
		}

		[Test]
		public void Check_Description()
		{
			Assert.That(region.RegionDescription.Trim(), Is.EqualTo("Western"));
		}

		[Test]
		public void Check_Territories()
		{
			Assert.That(region.Territories, Is.Not.Null);
            Assert.That(region.Territories, Has.Count(15));
            Assert.That(region.Territories.Any(t => t.TerritoryID == "60179"));
            Assert.That(region.Territories.Any(t => t.TerritoryID == "60601"));
            Assert.That(region.Territories.Any(t => t.TerritoryID == "80202"));
		}
	}
}
