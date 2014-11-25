using System.Linq;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Specs_For_Managing_The_Database_Model
{
    [TestFixture]
    public class When_Deleting_An_Entity
    {
        [Test]
        public void All_Its_References_Are_Deleted()
        {
            EntitySet entitySet = new EntitySetImpl();

            Entity entity1 = new EntityImpl("Table1");
            Entity entity2 = new EntityImpl("Table2");

            entitySet.AddEntity(entity1);
            entitySet.AddEntity(entity2);

            entity1.CreateReferenceTo(entity2);

            Assert.That(entity2.References.Count(), Is.EqualTo(1));
            Assert.That(entitySet.Entities.Count(), Is.EqualTo(2));

            entity1.DeleteSelf();

			Assert.That(entitySet.Entities.Count(), Is.EqualTo(1));
			Assert.That(entitySet.Entities.ElementAt(0), Is.SameAs(entity2));
            Assert.That(entity2.References.Count, Is.EqualTo(0));
        }
    }

	[TestFixture]
	public class When_Deleting_A_Property
	{
		[Test]
		public void It_Is_Removed_From_The_EntityKey()
		{
			Entity entity = new EntityImpl();
			Property property = new PropertyImpl();
			entity.AddProperty(property);
			entity.Key.AddProperty(property);

			CollectionAssert.Contains(entity.Key.Properties, property);
			
			property.DeleteSelf();

			Assert.That(entity.Key.Properties.ToList(), Is.Empty);
		}
	}
}
