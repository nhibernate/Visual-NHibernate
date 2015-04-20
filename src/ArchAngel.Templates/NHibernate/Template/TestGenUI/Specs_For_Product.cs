using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Project.Model;
using Order=Project.Model.Order;

namespace TestGenUI
{
	[TestFixture]
	public class Specs_For_Product
	{
		Product product;
		private ISession session;
		private ITransaction transaction;

		[SetUp]
		public void RunTests()
		{
			session = NHSessionHelper.OpenSession();
			transaction = session.BeginTransaction();

            product = session.CreateCriteria(typeof(Product))
                    .Add(Restrictions.Eq("ProductID", 51))
                    .UniqueResult<Product>();
		}

		[TearDown]
		public void TearDown()
		{
			transaction.Rollback();
			session.Dispose();
		}

		[Test]
        public void Check_Discontinued()
		{
			Assert.That(product.Discontinued, Is.False);
		}

        [Test]
        public void Check_ProductName()
        {
            Assert.That(product.ProductName, Is.EqualTo("Manjimup Dried Apples"));
        }

        [Test]
        public void Check_QuantityPerUnit()
        {
            Assert.That(product.QuantityPerUnit, Is.EqualTo("50 - 300 g pkgs."));
        }

        [Test]
        public void Check_ReorderLevel()
        {
            Assert.That(product.ReorderLevel, Is.EqualTo(10));
        }

        [Test]
        public void Check_UnitPrice()
        {
            Assert.That(product.UnitPrice, Is.EqualTo(53d));
        }

        [Test]
        public void Check_UnitsInStock()
        {
            Assert.That(product.UnitsInStock, Is.EqualTo(20));
        }

        [Test]
        public void Check_UnitsOnOrder()
        {
            Assert.That(product.UnitsOnOrder, Is.EqualTo(0));
        }

        [Test]
        public void Check_Category()
        {
            var category = session.CreateCriteria(typeof(Category))
                .Add(Restrictions.Eq("CategoryID", 7))
                .UniqueResult<Category>();

            Assert.That(product.Category, Is.SameAs(category));
        }

        [Test]
        public void Check_Supplier()
        {
            var supplier = session.CreateCriteria(typeof(Supplier))
                .Add(Restrictions.Eq("SupplierID", 24))
                .UniqueResult<Supplier>();

            Assert.That(product.Supplier, Is.SameAs(supplier));
        }
	}
}
