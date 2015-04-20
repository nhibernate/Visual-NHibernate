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
	public class Specs_For_OrderLine
	{
		Order_Line orderLine;
		private ISession session;
		private ITransaction transaction;

		[SetUp]
		public void RunTests()
		{
			session = NHSessionHelper.OpenSession();
			transaction = session.BeginTransaction();

			orderLine = session.CreateCriteria(typeof(Order_Line))
                    .Add(Restrictions.Eq("OrderID", 10250))
                    .Add(Restrictions.Eq("ProductID", 51))
                    .UniqueResult<Order_Line>();
		}

		[TearDown]
		public void TearDown()
		{
			transaction.Rollback();
			session.Dispose();
		}

		[Test]
		public void Check_Discount()
		{
			Assert.That(orderLine.Discount, Is.EqualTo(0.15f));
		}

        [Test]
        public void Check_Quantity()
        {
            Assert.That(orderLine.Quantity, Is.EqualTo(35));
        }

        [Test]
        public void Check_UnitPrice()
        {
            Assert.That(orderLine.UnitPrice, Is.EqualTo(42.40d));
        }

        [Test]
        public void Check_Order()
        {
            var order = session.CreateCriteria(typeof(Order))
                .Add(Restrictions.Eq("OrderID", 10250))
                .UniqueResult<Order>();
            Assert.That(orderLine.Order, Is.SameAs(order));
        }

        [Test]
        public void Check_Product()
        {
            var product = session.CreateCriteria(typeof(Product))
                .Add(Restrictions.Eq("ProductID", 51))
                .UniqueResult<Product>();
            Assert.That(orderLine.Product, Is.SameAs(product));
        }
	}
}
