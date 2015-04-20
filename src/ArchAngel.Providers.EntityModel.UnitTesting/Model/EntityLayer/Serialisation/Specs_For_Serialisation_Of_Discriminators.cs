using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer.Discrimination;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.Common;

namespace Specs_For_Serialisation_Of_Discriminators
{
	[TestFixture]
	public class When_Serialising_An_Empty_Discriminator
	{
		public const string BasicDiscriminatorXml = "<Discriminator />";

		[Test]
		public void It_Should_Serialise_To_This()
		{
			Discriminator dis = new DiscriminatorImpl();

			string outputXML = new EntitySetSerialisationScheme().SerialiseDiscriminator(dis);
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(BasicDiscriminatorXml));
		}
	}

	[TestFixture]
	public class When_Serialising_A_Discriminator_With_One_Condition
	{
		public const string FullDiscriminatorXml = "<Discriminator><AndGrouping><Condition>"
			+ "<Column table=\"Table1\" name=\"Column1\" />"
			+ "<Operator>Equal</Operator>"
			+ "<ExpressionValue>5</ExpressionValue>"
			+ "</Condition></AndGrouping></Discriminator>";

		[Test]
		public void It_Should_Serialise_To_This()
		{
			Table table = new Table("Table1");
			Column column = new Column("Column1");
			table.AddColumn(column);

			var dis = new DiscriminatorBuilder()
				.SingleConditionDiscriminator(column, Operator.Equal, new ExpressionValueImpl("5"));

			string outputXML = new EntitySetSerialisationScheme().SerialiseDiscriminator(dis);
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(FullDiscriminatorXml));
		}
	}
}
