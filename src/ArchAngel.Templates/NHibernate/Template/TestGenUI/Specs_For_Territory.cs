using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Project.Model;

namespace TestGenUI
{
	[TestFixture]
	public class Specs_For_Territory
	{
		Territory territory;
		private ISession session;
		private ITransaction transaction;

		[SetUp]
		public void RunTests()
		{
			session = NHSessionHelper.OpenSession();
			transaction = session.BeginTransaction();

			territory = session.CreateCriteria(typeof(Territory))
					.Add(Restrictions.Eq("TerritoryID", "10019")).UniqueResult<Territory>();
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
			Assert.That(territory.TerritoryDescription.Trim(), Is.EqualTo("New York"));
		}

		[Test]
		public void Check_Region()
		{
			Assert.That(territory.Region.RegionID, Is.EqualTo(1));
		}

		[Test]
		public void Check_Employees()
		{
			Assert.That(territory.Employees, Is.Not.Null);
			Assert.That(territory.Employees, Has.Count(1));
			Assert.That(territory.Employees.ElementAt(0).EmployeeID, Is.EqualTo(5));
		}
	}
}
