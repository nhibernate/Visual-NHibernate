using System;
using System.Collections.Generic;
using System.Text;
using ArchAngel.Providers.CodeProvider;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Specs_For_Whatever
{
	[TestFixture]
	public class Tests
	{
		[Test]
		public void TestBasicSyntaxError()
		{
			CSharpParser parser = new CSharpParser();
			parser.ParseCode("using System;");

			Assert.That(parser.ErrorOccurred, Is.False);

			parser = new CSharpParser();
			parser.ParseCode("lkjakjlawel;rfjaolkjnawlekmfpijsdpv;je;kf23-54-9845emadvlz");

			Assert.That(parser.ErrorOccurred, Is.True);
			Assert.That(parser.SyntaxErrors, Has.Count(1));
            
		}
		[Test]
		public void TestPosition()
		{
			CSharpParser parser = new CSharpParser();
			parser.ParseCode(@"using System;
			using NUnit.Framework;
			class Hahaha
			{
			");

			Assert.That(parser.ErrorOccurred, Is.True);
			Assert.That(parser.SyntaxErrors, Has.Count(1));
			Assert.That(parser.SyntaxErrors[0].LineNumber, Is.EqualTo(3));

			parser = new CSharpParser();
			parser.ParseCode(@"using System;
			using NUnit.Framework;
			class Hahaha
			{
				public TestMethod(){ }");

			Assert.That(parser.ErrorOccurred, Is.True);
			Assert.That(parser.SyntaxErrors, Has.Count(1));
			Assert.That(parser.SyntaxErrors[0].LineNumber, Is.EqualTo(4));

			parser = new CSharpParser();
			parser.ParseCode(@"using System");

			Assert.That(parser.ErrorOccurred, Is.True);
			Assert.That(parser.SyntaxErrors, Has.Count(1));
			Assert.That(parser.SyntaxErrors[0].LineNumber, Is.EqualTo(0));
		}

		[Test]
		[Ignore("Not finished")]
		public void testMultipleErrors()
		{
			CSharpParser parser = new CSharpParser();
			parser.ParseCode(@"using System
			using NUnit.Framework;
			class Hahaha
			{
				publi TestMethod()
				{ 
				}
			}");
			Assert.That(parser.ErrorOccurred, Is.True);
			Assert.That(parser.SyntaxErrors, Has.Count(2));
			Assert.That(parser.SyntaxErrors[0].LineNumber, Is.EqualTo(0));
			Assert.That(parser.SyntaxErrors[1].LineNumber, Is.EqualTo(4));


			parser = new CSharpParser();
			parser.ParseCode(@"using System
			using NUnit.Framework;
			class Hahaha
			{
				publi TestMethod()
				{ 
				}

				public testOtherMethod(
				{
				}
			}");
			Assert.That(parser.ErrorOccurred, Is.True);
			Assert.That(parser.SyntaxErrors, Has.Count(3));
			Assert.That(parser.SyntaxErrors[0].LineNumber, Is.EqualTo(0));
			Assert.That(parser.SyntaxErrors[1].LineNumber, Is.EqualTo(4));
			Assert.That(parser.SyntaxErrors[2].LineNumber, Is.EqualTo(8));
		}
		



	}
}
