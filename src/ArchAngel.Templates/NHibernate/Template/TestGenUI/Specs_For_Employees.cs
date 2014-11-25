using System;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Project.Model;

namespace TestGenUI
{
	[TestFixture]
	public class Specs_For_Employees
	{
		Employee employee;
		private ISession session;
		private ITransaction transaction;

		[SetUp]
		public void RunTests()
		{
			session = NHSessionHelper.OpenSession();
			transaction = session.BeginTransaction();

			employee = session.CreateCriteria(typeof(Employee))
					.Add(Restrictions.Eq("EmployeeID", 2)).UniqueResult<Employee>();
		}

		[TearDown]
		public void TearDown()
		{
			transaction.Rollback();
			session.Dispose();
		}

		[Test]
		public void Check_LastName()
		{
			Assert.That(employee.LastName, Is.EqualTo("Fuller"));
		}

		[Test]
		public void Check_FirstName()
		{
			Assert.That(employee.FirstName, Is.EqualTo("Andrew"));
		}

		[Test]
		public void Check_Title()
		{
			Assert.That(employee.Title, Is.EqualTo("Vice President, Sales"));
		}

		[Test]
		public void Check_TitleOfCourtesy()
		{
			Assert.That(employee.TitleOfCourtesy, Is.EqualTo("Dr."));
		}

		[Test]
		public void Check_BirthDate()
		{
			Assert.That(employee.BirthDate, Is.EqualTo(new DateTime(1952, 02, 19)));
		}

		[Test]
		public void Check_HireDate()
		{
			Assert.That(employee.HireDate, Is.EqualTo(new DateTime(1992, 8, 14)));
		}

		[Test]
		public void Check_Address()
		{
			Assert.That(employee.Address, Is.EqualTo("908 W. Capital Way"));
		}

		[Test]
		public void Check_City()
		{
			Assert.That(employee.City, Is.EqualTo("Tacoma"));
		}

		[Test]
		public void Check_Region()
		{
			Assert.That(employee.Region, Is.EqualTo("WA"));
		}

		[Test]
		public void Check_PostalCode()
		{
			Assert.That(employee.PostalCode, Is.EqualTo("98401"));
		}

		[Test]
		public void Check_Country()
		{
			Assert.That(employee.Country, Is.EqualTo("USA"));
		}

		[Test]
		public void Check_HomePhone()
		{
			Assert.That(employee.HomePhone, Is.EqualTo("(206) 555-9482"));
		}

		[Test]
		public void Check_Extension()
		{
			Assert.That(employee.Extension, Is.EqualTo("3457"));
		}

		[Test]
		public void Check_Photo()
		{
			Assert.That(employee.Photo, Is.Not.Null);
			Assert.That(employee.Photo, Has.Length(21626)); // Can't check much more than this.
		}

		[Test]
		public void Check_PhotoPath()
		{
			Assert.That(employee.PhotoPath, Is.EqualTo("http://accweb/emmployees/fuller.bmp"));
		}

		[Test]
		public void Check_Notes()
		{
			Assert.That(employee.Notes, Is.EqualTo("Andrew received his BTS commercial in 1974 and a Ph.D. in international marketing from the University of Dallas in 1981.  He is fluent in French and Italian and reads German.  He joined the company as a sales representative, was promoted to sales manager in January 1992 and to vice president of sales in March 1993.  Andrew is a member of the Sales Management Roundtable, the Seattle Chamber of Commerce, and the Pacific Rim Importers Association."));
		}

		[Test]
		public void Check_Orders()
		{
			Assert.That(employee.Orders, Is.Not.Null);
			Assert.That(employee.Orders, Has.Count(96));
		}

		[Test]
		public void Check_Territories()
		{
			Assert.That(employee.Territories, Is.Not.Null);
			Assert.That(employee.Territories, Has.Count(7));
			Assert.That(employee.Territories.Any(t => t.TerritoryID == "01581"));
			Assert.That(employee.Territories.Any(t => t.TerritoryID == "01730"));
			Assert.That(employee.Territories.Any(t => t.TerritoryID == "01833"));
			Assert.That(employee.Territories.Any(t => t.TerritoryID == "02116"));
			Assert.That(employee.Territories.Any(t => t.TerritoryID == "02139"));
			Assert.That(employee.Territories.Any(t => t.TerritoryID == "02184"));
			Assert.That(employee.Territories.Any(t => t.TerritoryID == "40222"));
		}

		[Test]
		public void Check_ReportsTo()
		{
			Assert.That(employee.ReportsTo, Is.Null);
		}

		[Test]
		public void Check_ReportsTo_HasManager()
		{
			var employee2 = session.CreateCriteria(typeof (Employee))
				.Add(Restrictions.Eq("EmployeeID", 1))
				.UniqueResult<Employee>();

			Assert.That(employee2.ReportsTo, Is.Not.Null);
			Assert.That(employee2.ReportsTo.EmployeeID, Is.EqualTo(2));
			Assert.That(employee2.ReportsTo, Is.SameAs(employee), "Make sure it reuses existing entity objects");
		}
	}
}
