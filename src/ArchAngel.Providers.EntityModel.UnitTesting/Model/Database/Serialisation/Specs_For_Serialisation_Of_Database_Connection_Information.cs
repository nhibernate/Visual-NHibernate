using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Slyce.Common;

namespace Specs_For_Serialisation_Of_Database_Connection_Information
{
	[TestFixture]
	public class When_Given_Empty_Connection_Information
	{
		[Test]
		public void Outputs_Nothing()
		{
			IDatabaseConnector connector = MockRepository.GenerateStub<IDatabaseConnector>();
			string output = new DatabaseSerialisationScheme().SerialiseConnectionInformation(connector);

			Assert.That(output, Is.Empty);
		}
	}

    [TestFixture]
    public class When_Given_A_SQL_CE_Connector
    {
        public const string ExpectedXml = "<ConnectionInformation DatabaseConnector=\"SqlCE\">" +
                                            "<FileName>database.sdf</FileName>" +
                                            "</ConnectionInformation>";

        [Test]
        public void Outputs_This()
        {
            ISQLCEDatabaseConnector connector = MockRepository.GenerateStub<ISQLCEDatabaseConnector>();
            connector.Filename = "database.sdf";

            string output = new DatabaseSerialisationScheme().SerialiseConnectionInformation(connector);

            Assert.That(output.RemoveWhitespaceBetweenXmlElements(), Is.EqualTo(ExpectedXml));
        }
    }

	[TestFixture]
	public class When_Given_A_SQL_Server_2005_Connector
	{
        public const string ExpectedXml = "<ConnectionInformation DatabaseConnector=\"SqlServer2005\">" +
											"<ServerName>server</ServerName>" +
											"<DatabaseName>database</DatabaseName>" +
											"<UserName>username</UserName>" +
											"</ConnectionInformation>";

		[Test]
		public void Outputs_This()
		{
			ISQLServer2005DatabaseConnector connector = MockRepository.GenerateStub<ISQLServer2005DatabaseConnector>();

			connector.ConnectionInformation = new ConnectionStringHelper
			{
				UserName = "username",
				Password = "password", // Including to make sure it does not save the password
				ServerName = "server",
				DatabaseName = "database"
			};
			
			string output = new DatabaseSerialisationScheme().SerialiseConnectionInformation(connector);

			Assert.That(output.RemoveWhitespaceBetweenXmlElements(), Is.EqualTo(ExpectedXml));
		}
	}
}
