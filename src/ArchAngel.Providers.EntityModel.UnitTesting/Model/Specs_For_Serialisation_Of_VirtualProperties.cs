using System;
using ArchAngel.Interfaces.ITemplate;
using ArchAngel.Providers.EntityModel.Model;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Slyce.Common;

namespace Specs_For_Serialisation_Of_VirtualProperties
{
	internal static class TestHelper
	{
		public static void TestXml(IUserOption virtualProperty, string xml)
		{
			VirtualPropertySerialiser serialiser = new VirtualPropertySerialiser();
			string result = serialiser.SerialiseVirtualProperty(virtualProperty).RemoveWhitespaceBetweenXmlElements();

			Assert.That(result, Is.EqualTo(xml));
		}

		public static string CreateXml(string name, string type, string value)
		{
			return string.Format("<VirtualProperty name=\"{0}\" type=\"{1}\"><Value>{2}</Value></VirtualProperty>",
				name, type, value);
		}
	}

	[TestFixture]
	public class When_Serialising_Enum_Types
	{
		public static readonly string Xml = TestHelper.CreateXml("VP1", "Test_Classes.VirtualPropEnum", "FileNotFound");
	
		[Test]
		public void It_Should_Serialise_To_This()
		{
			IUserOption virtualProperty = MockRepository.GenerateStub<IUserOption>();
			virtualProperty.DataType = typeof (Test_Classes.VirtualPropEnum);
			virtualProperty.Name = "VP1";
			virtualProperty.Value = Test_Classes.VirtualPropEnum.FileNotFound;

			TestHelper.TestXml(virtualProperty, Xml);
		}
	}

	[TestFixture]
	public class When_Serialising_Int_Types
	{
		public static readonly string Xml = TestHelper.CreateXml("VP1", typeof(Int32).FullName, "60");

		[Test]
		public void It_Should_Serialise_To_This()
		{
			IUserOption virtualProperty = MockRepository.GenerateStub<IUserOption>();
			virtualProperty.DataType = typeof(Int32);
			virtualProperty.Name = "VP1";
			virtualProperty.Value = 60;

			TestHelper.TestXml(virtualProperty, Xml);
		}
	}

	[TestFixture]
	public class When_Serialising_Decimal_Types
	{
		public static readonly string Xml = TestHelper.CreateXml("VP1", typeof(Double).FullName, "6");

		[Test]
		public void It_Should_Serialise_To_This()
		{
			IUserOption virtualProperty = MockRepository.GenerateStub<IUserOption>();
			virtualProperty.DataType = typeof(Double);
			virtualProperty.Name = "VP1";
			virtualProperty.Value = 6.0d;

			TestHelper.TestXml(virtualProperty, Xml);
		}
	}

	[TestFixture]
	public class When_Serialising_String_Types
	{
		public static readonly string Xml = TestHelper.CreateXml("VP1", typeof(String).FullName, "some string");

		[Test]
		public void It_Should_Serialise_To_This()
		{
			IUserOption virtualProperty = MockRepository.GenerateStub<IUserOption>();
			virtualProperty.DataType = typeof(String);
			virtualProperty.Name = "VP1";
			virtualProperty.Value = "some string";

			TestHelper.TestXml(virtualProperty, Xml);
		}
	}

	[TestFixture]
	public class When_Serialising_Char_Types
	{
		public static readonly string Xml = TestHelper.CreateXml("VP1", typeof(char).FullName, "T");

		[Test]
		public void It_Should_Serialise_To_This()
		{
			IUserOption virtualProperty = MockRepository.GenerateStub<IUserOption>();
			virtualProperty.DataType = typeof(char);
			virtualProperty.Name = "VP1";
			virtualProperty.Value = 'T';

			TestHelper.TestXml(virtualProperty, Xml);
		}
	}

	[TestFixture]
	public class When_Serialising_Boolean_Types
	{
		public static readonly string Xml = TestHelper.CreateXml("VP1", typeof(bool).FullName, "True");

		[Test]
		public void It_Should_Serialise_To_This()
		{
			IUserOption virtualProperty = MockRepository.GenerateStub<IUserOption>();
			virtualProperty.DataType = typeof(bool);
			virtualProperty.Name = "VP1";
			virtualProperty.Value = true;

			TestHelper.TestXml(virtualProperty, Xml);
		}
	}
}

namespace Test_Classes
{
	public enum VirtualPropEnum { None, One, FileNotFound }
}
