using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.Common;

namespace Specs_For_Serialisation_Of_An_Index
{
	[TestFixture]
	public class When_Serialising_An_Empty_Index
	{
		public const string BasicIndexXml = @"<Index><Description /><Enabled>True</Enabled>" + 
											@"<IsUserDefined>False</IsUserDefined><Name>Index1</Name>" + 
											@"<UID>00000000-0000-0000-0000-000000000000</UID><Datatype>Unique</Datatype>" + 
											@"<IsUnique>False</IsUnique><IsClustered>False</IsClustered></Index>";

		[Test]
		public void Should_Return_This()
		{
			const string expectedXML =	BasicIndexXml;

			Index index = new Index("Index1");

			string outputXML = index.Serialise(new DatabaseSerialisationScheme());
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(expectedXML));
		}
	}

	[TestFixture]
	public class When_Serialising_Index_With_All_Information_Set
	{
		public const string FullIndexXml = @"<Index><Description /><Enabled>True</Enabled>" +
		                                   @"<IsUserDefined>False</IsUserDefined><Name>Index1</Name>" +
		                                   @"<UID>00000000-0000-0000-0000-000000000000</UID><Datatype>PrimaryKey</Datatype>" +
		                                   @"<IsUnique>True</IsUnique><IsClustered>True</IsClustered></Index>";

		[Test]
		public void Should_Return_This()
		{
			const string expectedXML =	FullIndexXml;

			Index index = new Index("Index1");
			index.Datatype = DatabaseIndexType.PrimaryKey;
			index.IsClustered = true;
			index.IsUnique = true;

			string outputXML = index.Serialise(new DatabaseSerialisationScheme());
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(expectedXML));
		}
	}

	[TestFixture]
	public class When_Serialising_Index_With_Columns
	{
		public const string IndexWithColumnsXml = @"<Index><Description /><Enabled>True</Enabled>" +
		                                          @"<IsUserDefined>False</IsUserDefined><Name>Index1</Name>" +
		                                          @"<UID>00000000-0000-0000-0000-000000000000</UID><Datatype>Unique</Datatype>" +
		                                          @"<IsUnique>False</IsUnique><IsClustered>False</IsClustered>" +
		                                          @"<Columns><ColumnName>Column1</ColumnName></Columns></Index>";

		[Test]
		public void Should_Return_This()
		{
			const string expectedXML =	IndexWithColumnsXml;

			Table table1 = new Table("Table1");
			table1.AddColumn(new Column("Column1"));
			Index index = new Index("Index1");
			index.Parent = table1;
			index.AddColumn("Column1");

			string outputXML = index.Serialise(new DatabaseSerialisationScheme());
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(expectedXML));
		}
	}
}
