using System.IO;
using System.Xml.Linq;
using ArchAngel.NHibernateHelper;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace NHibernate.Specs_For_Creating_A_Database_Schema_From_NH_Config
{
    [TestFixture]
    public class When_Given_A_SQLCE_Config
    {
    	private static readonly string ConfigPath = Path.Combine("Resources", "basicSQLCE.cfg.xml");

    	[Test]
		public void It_Validates_Correctly()
		{
			NHibernateFileVerifier verifier = new NHibernateFileVerifier();

			bool valid = verifier.IsValidConfigFile(XDocument.Load(ConfigPath));

			Assert.That(valid, Is.True);
		}
    }

    [TestFixture]
    public class When_Given_A_SQL2005_Config
    {
		private static readonly string ConfigPath = Path.Combine("Resources", "basicSQL2005.cfg.xml");

		[Test]
		public void It_Validates_Correctly()
		{
			NHibernateFileVerifier verifier = new NHibernateFileVerifier();

			bool valid = verifier.IsValidConfigFile(XDocument.Load(ConfigPath));

			Assert.That(valid, Is.True);
		}
    }
}
