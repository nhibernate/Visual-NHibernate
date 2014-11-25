using System.Linq;
using System.Xml;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer.Discrimination;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.Common.ExtensionMethods;
using Specs_For_Serialisation_Of_Discriminators;

namespace Specs_For_Deserialisation_Of_Discriminators
{
	[TestFixture]
	public class When_Deserialising_An_Empty_Discriminator
	{
		public const string BasicDiscriminatorXml = "<Discriminator />";

		[Test]
		public void It_Should_Serialise_To_This()
		{
			var dis = new EntitySetDeserialisationScheme().DeserialiseDiscriminator(BasicDiscriminatorXml.GetXmlDocRoot(), null);
			
			Assert.That(dis, Is.Not.Null);
			Assert.That(dis.RootGrouping, Is.Not.Null);
			Assert.That(dis.RootGrouping.ContainsConditions, Is.False);
			Assert.That(dis.RootGrouping.ContainsGroupings, Is.False);
		}
	}

	[TestFixture]
	public class When_Deserialising_A_Discriminator_With_All_Fields_Set
	{
		[Test]
		public void It_Should_Serialise_To_This()
		{
			XmlNode root = When_Serialising_A_Discriminator_With_One_Condition.FullDiscriminatorXml.GetXmlDocRoot();
			Entity entity = new EntityImpl("Entity1");
			
			var column = new Column("Column1");
			Table table = new Table("Table1");
			table.AddColumn(column);
			var database = new Database("db1");
			database.AddTable(table);

			var dis = new EntitySetDeserialisationScheme().DeserialiseDiscriminator(root, database);
			
			Assert.That(dis, Is.Not.Null);
			Assert.That(dis.RootGrouping, Is.Not.Null);
			Assert.That(dis.RootGrouping.ContainsConditions, Is.True);
			Assert.That(dis.RootGrouping.ContainsGroupings, Is.False);

			var condition = dis.RootGrouping.Conditions.ElementAt(0);

			Assert.That(condition.Column, Is.SameAs(column));
			Assert.That(condition.Operator, Is.SameAs(Operator.Equal));
			Assert.That(condition.ExpressionValue.Value, Is.EqualTo("5"));
		}
	}
}
