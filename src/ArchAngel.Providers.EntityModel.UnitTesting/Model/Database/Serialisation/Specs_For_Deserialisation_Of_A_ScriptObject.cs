using System;
using System.Xml;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Specs_For_Serialisation_Of_A_Table;

namespace Specs_For_Deserialisation.Of_A_ScriptObject
{
	[TestFixture]
	public class When_Given_The_Specs_For_A_Basic_ScriptObject
	{
		[Test]
		public void It_Should_Be_Reconstructed_Correctly()
		{
			const string xml = When_Serialising_An_Empty_Table.BasicTableXml;
			XmlDocument document = new XmlDocument();
			document.LoadXml(xml);

			ITable table = new Table();
			new DatabaseDeserialisationScheme().ProcessScriptBase(table, document.DocumentElement);

			Assert.That(table.Description, Is.EqualTo(""));
			Assert.That(table.Enabled, Is.True);
			Assert.That(table.IsUserDefined, Is.False);
			Assert.That(table.Name, Is.EqualTo("Entity1"));
			Assert.That(table.UID, Is.EqualTo(new Guid("00000000-0000-0000-0000-000000000000")));
		}
	}

	[TestFixture]
	public class When_Serialising_A_ScriptObject_With_All_Information_Set
	{
		[Test]
		public void Should_Return_This()
		{
			const string xml = 
			"<Table>" + 
				@"<NamePlural>Tables1</NamePlural><Description>description</Description>" + 
				@"<Enabled>True</Enabled><IsUserDefined>True</IsUserDefined><Name>Entity1</Name>" +
				@"<UID>88888888-4444-4444-4444-121212121212</UID>" + 
			"</Table>";
			XmlDocument document = new XmlDocument();
			document.LoadXml(xml);
			
			ITable table = new Table();
			new DatabaseDeserialisationScheme().ProcessScriptBase(table, document.DocumentElement);

			Assert.That(table.Description, Is.EqualTo("description"));
			Assert.That(table.Enabled, Is.True);
			Assert.That(table.IsUserDefined, Is.True);
			Assert.That(table.Name, Is.EqualTo("Entity1"));
			Assert.That(table.UID, Is.EqualTo(new Guid("88888888-4444-4444-4444-121212121212")));
		}
	}
}
