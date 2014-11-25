using System.Xml;
using ArchAngel.Providers;
using ArchAngel.Providers.CodeProvider;
using ArchAngel.Providers.CodeProvider.CSharp;
using ArchAngel.Providers.CodeProvider.DotNet;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Slyce.IntelliMerge.Controller;
using Slyce.IntelliMerge.Controller.ManifestWorkers;

namespace Specs_For_CodeRootMap.MatchProcessor
{
    [TestFixture]
    public class LoadCustomMatches
    {
        private MockRepository mocks;
        private const string xml = @"
<Manifest>
  <Mappings>
    <CodeRootMappings>
      <CodeRootMap filename=""Test.cs"">
        <UserObject>Namespace|Class|Function1(string, int)</UserObject>
        <NewGenObject>Namespace|Class|Function2(string, int)</NewGenObject>
        <PrevGenObject>Namespace|Class|Function3(string, int)</PrevGenObject>
      </CodeRootMap>
      <CodeRootMap filename=""Test.cs"">
        <UserObject>Namespace|Class|Field1</UserObject>
        <NewGenObject>Namespace|Class|Field2</NewGenObject>
        <PrevGenObject>Namespace|Class|Field3</PrevGenObject>
      </CodeRootMap>
      <CodeRootMap filename=""NotTest.cs"">
        <UserObject>Namespace|Class1</UserObject>
        <NewGenObject>Namespace|Class2</NewGenObject>
        <PrevGenObject>Namespace|Class3</PrevGenObject>
      </CodeRootMap>
    </CodeRootMappings>
  </Mappings>
</Manifest>";
        [SetUp]
        public void Setup()
        {
            mocks = new MockRepository();
        }

        [Test]
        public void MatchesLoadedSuccessfully()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

			CodeRootMap map = mocks.StrictMock<CodeRootMap>();

            using(mocks.Record())
            {
                Expect.Call(map.MatchConstructs("Namespace|Class|Function1(string, int)", "Namespace|Class|Function2(string, int)", "Namespace|Class|Function3(string, int)")).Repeat.Once().Return(true);
                Expect.Call(map.MatchConstructs("Namespace|Class|Field1", "Namespace|Class|Field2", "Namespace|Class|Field3")).Repeat.Once().Return(true);
            }

            using (mocks.Playback())
            {
                CodeRootMapMatchProcessor processor = new CodeRootMapMatchProcessor();
                processor.LoadCustomMappings(doc, map, "Test.cs");
            }
        }
    }

    [TestFixture]
    public class SaveCustomMatches
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void MatchesSavedSuccessfully()
        {
			XmlDocument doc = new XmlDocument();
			Controller controller = new CSharpController();
			controller.Reorder = true;

			CodeRootMap map = new CodeRootMap();
			CodeRoot userCR = new CodeRoot(controller);
			CodeRoot newgenCR = new CodeRoot(controller);
			CodeRoot prevgenCR = new CodeRoot(controller);

        	Class clazz = new Class(controller, "Class1");
        	clazz.AddChild(new Class(controller, "InnerClass"));
        	userCR.AddChild(clazz);

			clazz = new Class(controller, "Class2");
			clazz.AddChild(new Class(controller, "InnerClass"));
			newgenCR.AddChild(clazz);

			clazz = new Class(controller, "Class3");
			clazz.AddChild(new Class(controller, "InnerClass"));
			prevgenCR.AddChild(clazz);

        	map.AddCodeRoot(userCR,		Version.User);
			map.AddCodeRoot(newgenCR,	Version.NewGen);
			map.AddCodeRoot(prevgenCR,	Version.PrevGen);

        	Assert.That(map.MatchConstructs("Class1", "Class2", "Class3"), "Matching constructs", Is.True);

			CodeRootMapMatchProcessor processor = new CodeRootMapMatchProcessor();
        	processor.SaveCustomMappings(doc, map, "Test.cs");
        	XmlNodeList nodes = doc.SelectNodes("Manifest/Mappings/CodeRootMappings/CodeRootMap");
			Assert.IsNotNull(nodes, "Couldn't find the CodeRootMap nodes");
        	Assert.That(nodes.Count, Is.EqualTo(1));

        	Assert.That(nodes.Item(0).Attributes["filename"].Value, Is.EqualTo("Test.cs"));

        	XmlNode mappingElement = nodes.Item(0);

        	XmlNode userNode = mappingElement.SelectSingleNode(ManifestConstants.UserObjectElement);
			XmlNode newgNode = mappingElement.SelectSingleNode(ManifestConstants.NewGenObjectElement);
			XmlNode prevNode = mappingElement.SelectSingleNode(ManifestConstants.PrevGenObjectElement);

        	Assert.That(userNode, Is.Not.Null);
        	Assert.That(newgNode, Is.Not.Null);
        	Assert.That(prevNode, Is.Not.Null);

        	Assert.That(userNode.InnerText, Is.EqualTo("Class1"));
			Assert.That(newgNode.InnerText, Is.EqualTo("Class2"));
			Assert.That(prevNode.InnerText, Is.EqualTo("Class3"));
        }
    }
}
