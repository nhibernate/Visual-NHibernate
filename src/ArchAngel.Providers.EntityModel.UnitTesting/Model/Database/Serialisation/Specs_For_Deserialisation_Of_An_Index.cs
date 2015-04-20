using System.Xml;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Specs_For_Serialisation_Of_An_Index;

namespace Specs_For_Deserialisation.Of_An_Index
{
	[TestFixture]
	public class When_Given_The_Specs_For_An_Index_With_Default_Settings
	{
		[Test]
		public void It_Should_Be_Reconstructed_Correctly()
		{
			const string xml = When_Serialising_An_Empty_Index.BasicIndexXml;
			XmlDocument document = new XmlDocument();
			document.LoadXml(xml);

			IIndex index = new DatabaseDeserialisationScheme().ProcessIndexNode(document.DocumentElement, new Table(), new Database(""));

			// All of the other index stuff is tested in the tests for IScriptObject
			Assert.That(index.Datatype, Is.EqualTo(DatabaseIndexType.Unique));
			Assert.That(index.IsClustered, Is.False);
			Assert.That(index.IsUnique, Is.False);
		}
	}

	[TestFixture]
	public class When_Given_The_Specs_For_A_Index_With_Everything_Set
	{
		[Test]
		public void It_Should_Be_Reconstructed_Correctly()
		{
			const string xml = When_Serialising_Index_With_All_Information_Set.FullIndexXml;
			XmlDocument document = new XmlDocument();
			document.LoadXml(xml);

			IIndex index = new DatabaseDeserialisationScheme().ProcessIndexNode(document.DocumentElement, new Table(), new Database(""));

			// All of the other key stuff is tested in the tests for IScriptObject
			Assert.That(index.Datatype, Is.EqualTo(DatabaseIndexType.PrimaryKey));
			Assert.That(index.IsClustered, Is.True);
			Assert.That(index.IsUnique, Is.True);
		}
	}

	[TestFixture]
	public class When_Given_The_Specs_For_A_Index_With_Columns
	{
		[Test]
		public void It_Should_Be_Reconstructed_Correctly()
		{
			const string xml = When_Serialising_Index_With_Columns.IndexWithColumnsXml;
			XmlDocument document = new XmlDocument();
			document.LoadXml(xml);

			Table table1 = new Table("Table1");
			table1.AddColumn(new Column("Column1"));

			IIndex index = new DatabaseDeserialisationScheme().ProcessIndexNode(document.DocumentElement, table1, new Database(""));

			Assert.That(index.Columns, Has.Count(1));
			Assert.That(index.Columns[0], Is.SameAs(table1.Columns[0]));
		}
	}
}
