using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Slyce.Common;
using Slyce.Common.ExtensionMethods;

namespace Specs_For_Deserialisation_Of_Database_Connection_Information
{
    [TestFixture]
    public class When_Given_A_SQL_CE_Connector
    {
        [Test]
        public void Outputs_This()
        {
            IDatabaseLoader loader = new DatabaseDeserialisationScheme().DeserialiseConnectionInformation(
                Specs_For_Serialisation_Of_Database_Connection_Information.When_Given_A_SQL_CE_Connector.ExpectedXml.GetXmlDocRoot());

            Assert.That(loader, Is.Not.Null);
            Assert.That(loader, Is.TypeOf(typeof(SQLCEDatabaseLoader)));
            Assert.That(loader.DatabaseConnector, Is.Not.Null);
            
            var connector = (SQLCEDatabaseConnector)loader.DatabaseConnector;
            Assert.That(connector.Filename, Is.EqualTo("database.sdf"));
        }
    }

	[TestFixture]
	public class When_Given_A_SQL_Server_2005_Connector
	{
		[Test]
		public void Outputs_This()
		{
            IDatabaseLoader loader = new DatabaseDeserialisationScheme().DeserialiseConnectionInformation(
                Specs_For_Serialisation_Of_Database_Connection_Information.When_Given_A_SQL_Server_2005_Connector.ExpectedXml.GetXmlDocRoot());

            Assert.That(loader, Is.Not.Null);
            Assert.That(loader, Is.TypeOf(typeof(SQLServer2005DatabaseLoader)));
            Assert.That(loader.DatabaseConnector, Is.Not.Null);

            var connector = (SQLServer2005DatabaseConnector)loader.DatabaseConnector;
		    var helper = connector.ConnectionInformation;
            Assert.That(helper.ServerName, Is.EqualTo("server"));
            Assert.That(helper.DatabaseName, Is.EqualTo("database"));
            Assert.That(helper.UserName, Is.EqualTo("username"));
            Assert.That(helper.UseIntegratedSecurity, Is.False);
		}
	}
}
