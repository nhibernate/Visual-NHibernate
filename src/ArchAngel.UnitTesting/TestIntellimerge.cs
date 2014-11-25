using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.IntelliMerge;
using Slyce.IntelliMerge.Controller;

namespace ArchAngel.Workbench.Tests
{
    public class DiffTestBaseClass
    {
        protected TextFileInformation tfi;

        public void SetupAndPerformDiff(string user, string prevGen, string newGen)
        {
            tfi = new TextFileInformation();
            tfi.UserFile = string.IsNullOrEmpty(user) ? TextFile.Blank : new TextFile(user);
            tfi.NewGenFile = string.IsNullOrEmpty(newGen) ? TextFile.Blank : new TextFile(newGen);
            tfi.PrevGenFile = string.IsNullOrEmpty(prevGen) ? TextFile.Blank : new TextFile(prevGen);
            tfi.RelativeFilePath = "Class.cs";
            tfi.PerformDiff();
        }
    }

    [TestFixture]
	[Ignore]
    public class TestIntellimerge : DiffTestBaseClass
    {
        [Test]
        public void TestNamespaceDiff()
        {
            SetupAndPerformDiff(
            @"namespace N1 
            {
                namespace N2
                {
                    public class S1 
                    { 
                        public int i;
                        public int j;
                    }
                }
            }",
            @"namespace N1 
            {
                namespace N2
                {
                    public class S1 { public int j; }
                }
            }",
            @"namespace N1 
            {
                namespace N2
                {
                    public class S1 { public int k; }
                }
            }");

            Assert.That(tfi.CurrentDiffResult.DiffType, Is.EqualTo(TypeOfDiff.UserAndTemplateChange));
            Assert.That(tfi.CodeRootMap.GetMergedCodeRoot().ToString().TrimStart()
                , Is.EqualTo(
@"namespace N1
{
	namespace N2
	{
		public class S1
		{
			public int k;
			public int i;
			public int j;
		}
	}
}"));
            
        }
    }
}