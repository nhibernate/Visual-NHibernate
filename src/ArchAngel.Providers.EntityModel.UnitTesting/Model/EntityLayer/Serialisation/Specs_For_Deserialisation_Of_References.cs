using System;
using System.Linq;
using ArchAngel.Interfaces;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.Common.ExtensionMethods;
using Specs_For_Serialisation_Of_References;

namespace Specs_For_Deserialisation_Of_References
{
	[TestFixture]
	public class When_Deserialising_An_Reference_With_All_Fields_Set
	{
		[Test]
		public void It_Should_Serialise_To_This()
		{
			EntitySet entitySet = new EntitySetImpl();
			entitySet.AddEntity(new EntityImpl("Entity1"));
			entitySet.AddEntity(new EntityImpl("Entity2"));

			Reference reference = new EntitySetDeserialisationScheme().DeserialiseReference(When_Serialising_An_Reference_With_All_Fields_Set.FullReferenceXml.GetXmlDocRoot(), entitySet);

			Assert.That(reference.Identifier, Is.EqualTo(new Guid("11111111-1111-1111-1111-111111111111")));
			Assert.That(reference.Cardinality1, Is.EqualTo(Cardinality.One));
			Assert.That(reference.Cardinality2, Is.EqualTo(new Cardinality(0, 5)));
			Assert.That(reference.End1Name, Is.EqualTo("ParentEntity1"));
			Assert.That(reference.End2Name, Is.EqualTo("Entity2s"));
			Assert.That(reference.End1Enabled, Is.True);
			Assert.That(reference.End2Enabled, Is.True);
			Assert.That(reference.Entity1, Is.SameAs(entitySet.Entities[1]));
			Assert.That(reference.Entity2, Is.SameAs(entitySet.Entities[0]));
		}
	}
}
