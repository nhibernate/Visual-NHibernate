using System;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.Common;

namespace Specs_For_Serialisation_Of_A_ScriptBase
{
	[TestFixture]
	public class When_Serialising_A_Script_Base
	{
		[Test]
		public void Should_Return_This()
		{
			const string expectedXML =	@"<Table><Description>Table1 Entity</Description>" +
										@"<Enabled>True</Enabled><IsUserDefined>True</IsUserDefined><Name>Entity1</Name><UID>88888888-4444-4444-4444-121212121212</UID></Table>";
			// These are all ScriptBase properties.
			Table table = new Table("Entity1");
			table.Description = "Table1 Entity";
			table.Enabled = true;
			table.IsUserDefined = true;
			table.UID = new Guid("88888888-4444-4444-4444-121212121212");

			string outputXML = table.Serialise(new DatabaseSerialisationScheme());
			outputXML = XmlSqueezer.RemoveWhitespaceBetweenElements(outputXML);
			Assert.That(outputXML, Is.EqualTo(expectedXML));
		}
	}
}
