using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using NUnit.Framework;

namespace Specs_For_Database_Connections
{
	[TestFixture]
	public class When_An_Invalid_Connection_String_Is_Passed
	{
		[Test]
		public void The_Constructor_Should_Complete_But_The_Test_Should_Fail()
		{
			IDatabaseConnector connector = new SQLCEDatabaseConnector("asdf.sdf");

			try
			{
				connector.TestConnection();
				Assert.Fail("The connector did not fail the test.");
			}
			catch
			{
			}

			try
			{
				connector.Open();
				Assert.Fail("The connector should not be able to be opened.");
			}
			catch
			{
			}
		}
	}
}
