using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.Common;
using Specs_For_Serialisation_Of_A_Table;

namespace Specs_For_Serialisation_Of_A_Column
{
	[TestFixture]
	public class When_Serialising_An_Empty_Column
	{
		public const string BasicColumnXml = "<Column>" + When_Serialising_An_Empty_Table.ScriptBaseXml +
											"<Datatype /><Default /><InPrimaryKey>False</InPrimaryKey>" +
											"<IsCalculated>False</IsCalculated><IsComputed>False</IsComputed>" +
											"<IsIdentity>False</IsIdentity><IsNullable>False</IsNullable>" +
											"<IsReadOnly>False</IsReadOnly><IsUnique>False</IsUnique>" +
											"<OrdinalPosition>0</OrdinalPosition><Precision>0</Precision>" +
											"<Scale>0</Scale><Size>0</Size>" +
											"</Column>";

		[Test]
		public void Should_Return_This()
		{
			const string expectedXML =	BasicColumnXml;

			Column column = new Column("Entity1");

			string outputXML = column.Serialise(new DatabaseSerialisationScheme());
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(expectedXML));
		}
	}

	[TestFixture]
	public class When_Serialising_A_Column_With_All_Information_Set
	{
		public const string FullColumnXml = @"<Column>"+ When_Serialising_An_Empty_Table.ScriptBaseXml + 
											@"<Datatype>nvarchar(50)</Datatype>" +
											@"<Default>default</Default><InPrimaryKey>True</InPrimaryKey>" +
											@"<IsCalculated>True</IsCalculated><IsComputed>True</IsComputed>" +
											@"<IsIdentity>True</IsIdentity><IsNullable>True</IsNullable>" +
											@"<IsReadOnly>True</IsReadOnly><IsUnique>True</IsUnique>" +
											@"<OrdinalPosition>0</OrdinalPosition><Precision>10</Precision>" +
											@"<Scale>11</Scale><Size>12</Size>" +
											@"</Column>";

		[Test]
		public void Should_Return_This()
		{
			const string expectedXML = FullColumnXml;

			Column column = new Column("Entity1");
			column.Datatype = "nvarchar(50)";
			column.Default = "default";
			column.InPrimaryKey = true;
			column.IsCalculated = true;
			column.IsComputed = true;
			column.IsIdentity = true;
			column.IsNullable = true;
			column.IsReadOnly = true;
			column.IsUnique = true;
			column.OrdinalPosition = 0;
			column.Precision = 10;
			column.Scale = 11;
			column.Size = 12;

			string outputXML = column.Serialise(new DatabaseSerialisationScheme());
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(expectedXML));
		}
	}
}
