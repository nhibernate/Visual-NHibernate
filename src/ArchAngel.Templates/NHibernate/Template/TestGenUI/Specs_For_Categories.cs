using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Project.Model;

namespace TestGenUI
{
	[TestFixture]
	public class Specs_For_Categories
	{
		Category category;
		private ISession session;
		private ITransaction transaction;

		[SetUp]
		public void RunTests()
		{
			session = NHSessionHelper.OpenSession();
			transaction = session.BeginTransaction();

			category = session.CreateCriteria(typeof(Category))
					.Add(Restrictions.Eq("CategoryID", 7)).UniqueResult<Category>();
		}

		[TearDown]
		public void TearDown()
		{
			transaction.Rollback();
			session.Dispose();
		}

		[Test]
		public void Check_Name()
		{
			Assert.That(category.CategoryName, Is.EqualTo("Produce"));
		}

		[Test]
		public void Check_Description()
		{
			Assert.That(category.Description, Is.EqualTo("Dried fruit and bean curd"));
		}

		[Test]
		public void Check_Picture()
		{
			Assert.That(category.Picture, Is.Not.Null);
			Assert.That(category.Picture, Has.Length(10746));
		}

		[Test]
		public void Check_Products()
		{
			Assert.That(category.Products, Is.Not.Null);
			Assert.That(category.Products, Has.Count(5));
			Assert.That(category.Products.Any(p => p.ProductID == 7));
			Assert.That(category.Products.Any(p => p.ProductID == 14));
			Assert.That(category.Products.Any(p => p.ProductID == 28));
			Assert.That(category.Products.Any(p => p.ProductID == 51));
			Assert.That(category.Products.Any(p => p.ProductID == 74));
		}
	}
}
