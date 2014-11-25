using System.Linq;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Specs_For_Entities
{
	[TestFixture]
	[Category("IntegrationTests")]
	public class When_An_Entity_Does_Not_Belong_To_An_EntitySet
	{
		[Test]
		public void Delete_Does_Nothing()
		{
			// I realise this test has no asserts. I am making sure there are no
			// null pointer exceptions thrown.
			Entity entity = new EntityImpl();
			entity.DeleteSelf();
		}
	}

	[TestFixture]
	[Category("IntegrationTests")]
	public class When_An_Entity_Does_Belong_To_An_EntitySet
	{
		private Entity entity1;
		private Entity entity2;
		private EntitySet entitySet;

		[SetUp]
		public void SetUp()
		{
			entity1 = new EntityImpl("Entity1");
			entity2 = new EntityImpl("Entity2");

			entitySet = new EntitySetImpl();
			entitySet.AddEntity(entity1);
			entitySet.AddEntity(entity2);

			entity1.CreateReferenceTo(entity2);
		}

		[Test]
		public void Deleting_The_Entity_Removes_It_From_The_EntitySet()
		{
			Assert.That(entitySet.Entities.Contains(entity1), Is.True);

			entity1.DeleteSelf();

			Assert.That(entitySet.Entities.Contains(entity1), Is.False);
		}

		[Test]
		public void Deleting_The_Entity_Removes_Any_References_Pointing_To_It()
		{
			Assert.That(entitySet.References.Count(), Is.EqualTo(1));

			entity1.DeleteSelf();

			Assert.That(entitySet.References.Count(), Is.EqualTo(0));
			Assert.That(entity2.References.Count(), Is.EqualTo(0));
		}

	}
}
