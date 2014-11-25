using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Project.Model;

namespace TestGenUI
{
	[TestFixture]
	public class Specs_For_Customer
	{
		Customer customer;
		private ISession session;
		private ITransaction transaction;

        // CustomerID   CompanyName     ContactName     ContactTitle	Address	        City	Region	PostalCode	Country	Phone	    Fax
        // ERNSH        Ernst Handel	Roland Mendel	Sales Manager	Kirchgasse 6	Graz	NULL	8010	    Austria	7675-3425	7675-3426

		[SetUp]
		public void RunTests()
		{
			session = NHSessionHelper.OpenSession();
			transaction = session.BeginTransaction();

            customer = session.CreateCriteria(typeof(Customer))
                    .Add(Restrictions.Eq("CustomerID", "ERNSH")).UniqueResult<Customer>();
		}

		[TearDown]
		public void TearDown()
		{
			transaction.Rollback();
			session.Dispose();
		}

        [Test]
        public void Check_Address()
        {
            Assert.That(customer.Address, Is.EqualTo("Kirchgasse 6"));
        }

        [Test]
        public void Check_City()
        {
            Assert.That(customer.City, Is.EqualTo("Graz"));
        }

        [Test]
        public void Check_CompanyName()
        {
            Assert.That(customer.CompanyName, Is.EqualTo("Ernst Handel"));
        }

		[Test]
		public void Check_ContactName()
		{
            Assert.That(customer.ContactName, Is.EqualTo("Roland Mendel"));
		}

        [Test]
        public void Check_ContactTitle()
        {
            Assert.That(customer.ContactTitle, Is.EqualTo("Sales Manager"));
        }

        [Test]
        public void Check_Country()
        {
            Assert.That(customer.Country, Is.EqualTo("Austria"));
        }

        [Test]
        public void Check_Fax()
        {
            Assert.That(customer.Fax, Is.EqualTo("7675-3426"));
        }

		[Test]
		public void Check_Phone()
		{
            Assert.That(customer.Phone, Is.EqualTo("7675-3425"));
		}

		[Test]
		public void Check_PostalCode()
		{
			Assert.That(customer.PostalCode, Is.EqualTo("8010"));
		}

        [Test]
        public void Check_Region()
        {
            Assert.That(customer.Region, Is.Null);
        }

        [Test]
        public void Check_Demographics()
        {
            Assert.That(customer.Demographics, Is.Not.Null);
            Assert.Fail("Need to add demographic information to northwind script.");
        }

		[Test]
		public void Check_Orders()
		{
			Assert.That(customer.Orders, Is.Not.Null);
			Assert.That(customer.Orders, Has.Count(30));
			Assert.That(customer.Orders.Any(p => p.OrderID == 10258));
            Assert.That(customer.Orders.Any(p => p.OrderID == 10263));
            Assert.That(customer.Orders.Any(p => p.OrderID == 10351));
            Assert.That(customer.Orders.Any(p => p.OrderID == 10368));
		}
	}
}
