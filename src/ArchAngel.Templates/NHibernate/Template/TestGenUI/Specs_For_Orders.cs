using System;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Order=Project.Model.Order;

namespace TestGenUI
{
	[TestFixture]
	public class Specs_For_Orders
	{
		Order order;
		private ISession session;
		private ITransaction transaction;

		[SetUp]
		public void RunTests()
		{
			session = NHSessionHelper.OpenSession();
			transaction = session.BeginTransaction();
				
			order = session.CreateCriteria(typeof(Order))
					.Add(Restrictions.Eq("OrderID", 10248)).UniqueResult<Order>();	
		}

		[TearDown]
		public void TearDown()
		{
			transaction.Rollback();
			session.Dispose();
		}

		[Test]
		public void Check_Customer()
		{
			Assert.That(order.Customer, Is.Not.Null);
			Assert.That(order.Customer.CustomerID, Is.EqualTo("VINET"));
		}

		[Test]
		public void Check_Employee()
		{
			Assert.That(order.Employee, Is.Not.Null);
			Assert.That(order.Employee.EmployeeID, Is.EqualTo(5));
		}

		[Test]
		public void Check_Freight()
		{
			Assert.That(order.Freight, Is.EqualTo(32.38d));
		}

		[Test]
		public void Check_OrderDate()
		{
			Assert.That(order.OrderDate, Is.EqualTo(new DateTime(1996, 7, 4)));
		}

		[Test]
		public void Check_OrderLines()
		{
			Assert.That(order.OrderLines, Has.Count(3));
			Assert.That(order.OrderLines.Any(item => item.ProductID == 11), Is.True);
			Assert.That(order.OrderLines.Any(item => item.ProductID == 42), Is.True);
			Assert.That(order.OrderLines.Any(item => item.ProductID == 72), Is.True);
		}

		[Test]
		public void Check_RequiredDate()
		{
			Assert.That(order.RequiredDate, Is.EqualTo(new DateTime(1996, 8, 1)));
		}

		[Test]
		public void Check_ShipAddress()
		{
			Assert.That(order.ShipAddress, Is.EqualTo("59 rue de l'Abbaye"));
			Assert.That(order.ShipCity, Is.EqualTo("Reims"));
			Assert.That(order.ShipCountry, Is.EqualTo("France"));
			Assert.That(order.ShipName, Is.EqualTo("Vins et alcools Chevalier"));
			Assert.That(order.ShipPostalCode, Is.EqualTo("51100"));
			Assert.That(order.ShipRegion, Is.EqualTo(null));
		}

		[Test]
		public void Check_ShippedDate()
		{
			Assert.That(order.ShippedDate, Is.EqualTo(new DateTime(1996, 7, 16)));
		}

		[Test]
		public void Check_ShipVia()
		{
			Assert.That(order.ShipVia, Is.Not.Null);
			Assert.That(order.ShipVia.ShipperID, Is.EqualTo(3));
		}
	}
}
