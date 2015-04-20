using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Project.Model;

namespace TestGenUI
{
	[TestFixture]
	public class Specs_For_Supplier
	{
        Supplier supplier;
		private ISession session;
		private ITransaction transaction;

		[SetUp]
		public void RunTests()
		{
			session = NHSessionHelper.OpenSession();
			transaction = session.BeginTransaction();

            supplier = session.CreateCriteria(typeof(Supplier))
                    .Add(Restrictions.Eq("SupplierID", 2)).UniqueResult<Supplier>();
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
            Assert.That(supplier.Address, Is.EqualTo("P.O. Box 78934"));
		}

        [Test]
        public void Check_City()
        {
            Assert.That(supplier.City, Is.EqualTo("New Orleans"));
        }

        [Test]
        public void Check_CompanyName()
        {
            Assert.That(supplier.CompanyName, Is.EqualTo("New Orleans Cajun Delights"));
        }

        [Test]
        public void Check_ContactName()
        {
            Assert.That(supplier.ContactName, Is.EqualTo("Shelley Burke"));
        }

        [Test]
        public void Check_ContactTitle()
        {
            Assert.That(supplier.ContactTitle, Is.EqualTo("Order Administrator"));
        }

        [Test]
        public void Check_Country()
        {
            Assert.That(supplier.Country, Is.EqualTo("USA"));
        }

        [Test]
        public void Check_Fax()
        {
            Assert.That(supplier.Fax, Is.Null);
        }

        [Test]
        public void Check_HomePage()
        {
            Assert.That(supplier.HomePage, Is.EqualTo("#CAJUN.HTM#"));
        }

        [Test]
        public void Check_Phone()
        {
            Assert.That(supplier.Phone, Is.EqualTo("(100) 555-4822"));
        }

        [Test]
        public void Check_PostalCode()
        {
            Assert.That(supplier.PostalCode, Is.EqualTo("70117"));
        }

        [Test]
        public void Check_Region()
        {
            Assert.That(supplier.Region, Is.EqualTo("LA"));
        }

        [Test]
        public void Check_Products()
        {
            Assert.That(supplier.Products, Is.Not.Null);
            Assert.That(supplier.Products, Has.Count(4));
            Assert.That(supplier.Products.Any(p => p.ProductID == 4));
            Assert.That(supplier.Products.Any(p => p.ProductID == 5));
            Assert.That(supplier.Products.Any(p => p.ProductID == 65));
            Assert.That(supplier.Products.Any(p => p.ProductID == 66));
        }
	}
}
