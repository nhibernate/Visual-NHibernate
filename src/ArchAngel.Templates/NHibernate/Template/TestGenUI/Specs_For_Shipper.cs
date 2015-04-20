using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Project.Model;

namespace TestGenUI
{
	[TestFixture]
	public class Specs_For_Shipper
	{
        Shipper shipper;
		private ISession session;
		private ITransaction transaction;

		[SetUp]
		public void RunTests()
		{
			session = NHSessionHelper.OpenSession();
			transaction = session.BeginTransaction();

            shipper = session.CreateCriteria(typeof(Shipper))
                    .Add(Restrictions.Eq("ShipperID", 2)).UniqueResult<Shipper>();
		}

		[TearDown]
		public void TearDown()
		{
			transaction.Rollback();
			session.Dispose();
		}

		[Test]
		public void Check_CompanyName()
		{
            Assert.That(shipper.CompanyName, Is.EqualTo("United Package"));
		}

        [Test]
        public void Check_Phone()
        {
            Assert.That(shipper.Phone, Is.EqualTo("(503) 555-3199"));
        }

		[Test]
		public void Check_Territories()
		{
			Assert.That(shipper.Orders, Is.Not.Null);
            Assert.That(shipper.Orders, Has.Count(326));
            Assert.That(shipper.Orders.Any(o => o.OrderID == 10250));
            Assert.That(shipper.Orders.Any(o => o.OrderID == 10252));
            Assert.That(shipper.Orders.Any(o => o.OrderID == 10253));
            Assert.That(shipper.Orders.Any(o => o.OrderID == 10254));
		}
	}
}
