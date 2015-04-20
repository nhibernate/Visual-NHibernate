using System.Xml;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Specs_For_Serialisation_Of_A_Column;

namespace Specs_For_Deserialisation.Of_A_Column
{
	[TestFixture]
	public class When_Given_The_Specs_For_A_Column_With_Default_Settings
	{
		[Test]
		public void It_Should_Be_Reconstructed_Correctly()
		{
			const string xml = When_Serialising_An_Empty_Column.BasicColumnXml;
			XmlDocument document = new XmlDocument();
			document.LoadXml(xml);

			IColumn index = new DatabaseDeserialisationScheme().ProcessColumnNode(document.DocumentElement);

			// All of the other column stuff is tested in the tests for IScriptObject
			Assert.That(index.Datatype, Is.EqualTo(""));
			Assert.That(index.Default, Is.EqualTo(""));
			Assert.That(index.InPrimaryKey, Is.False);
			Assert.That(index.IsCalculated, Is.False);
			Assert.That(index.IsComputed, Is.False);
			Assert.That(index.IsIdentity, Is.False);
			Assert.That(index.IsNullable, Is.False);
			Assert.That(index.IsReadOnly, Is.False);
			Assert.That(index.IsUnique, Is.False);
			Assert.That(index.OrdinalPosition, Is.EqualTo(0));
			Assert.That(index.Precision, Is.EqualTo(0));
			Assert.That(index.Scale, Is.EqualTo(0));
			Assert.That(index.Size, Is.EqualTo(0));
		}
	}

	[TestFixture]
	public class When_Given_The_Specs_For_A_Index_With_Everything_Set
	{
		[Test]
		public void It_Should_Be_Reconstructed_Correctly()
		{
			const string xml = When_Serialising_A_Column_With_All_Information_Set.FullColumnXml;
			XmlDocument document = new XmlDocument();
			document.LoadXml(xml);

			IColumn index = new DatabaseDeserialisationScheme().ProcessColumnNode(document.DocumentElement);

			// All of the other column stuff is tested in the tests for IScriptObject
			Assert.That(index.Datatype, Is.EqualTo("nvarchar(50)"));
			Assert.That(index.Default, Is.EqualTo("default"));
			Assert.That(index.InPrimaryKey, Is.True);
			Assert.That(index.IsCalculated, Is.True);
			Assert.That(index.IsComputed, Is.True);
			Assert.That(index.IsIdentity, Is.True);
			Assert.That(index.IsNullable, Is.True);
			Assert.That(index.IsReadOnly, Is.True);
			Assert.That(index.IsUnique, Is.True);
			Assert.That(index.OrdinalPosition, Is.EqualTo(0));
			Assert.That(index.Precision, Is.EqualTo(10));
			Assert.That(index.Scale, Is.EqualTo(11));
			Assert.That(index.Size, Is.EqualTo(12));
		}
	}
}
