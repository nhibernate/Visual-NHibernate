using System;
using ArchAngel.Interfaces.ITemplate;
using ArchAngel.Providers.EntityModel.Model;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Slyce.Common;
using Slyce.Common.ExtensionMethods;
using Specs_For_Serialisation_Of_VirtualProperties;

namespace Specs_For_Deserialisation_Of_VirtualProperties
{
	internal static class TestHelper
	{
		public static void TestDeserialisation(string xml, string name, Type type, object expectedValue)
		{
			VirtualPropertyDeserialiser serialiser = new VirtualPropertyDeserialiser();
			IUserOption virtualProperty = serialiser.DeserialiseVirtualProperty(xml.GetXmlDocRoot());

			Assert.That(virtualProperty.Value, Is.EqualTo(expectedValue));
			Assert.That(virtualProperty.Name, Is.EqualTo(name));
			Assert.That(virtualProperty.DataType, Is.EqualTo(type));
		}
	}

	[TestFixture]
	public class When_Deserialising_Enum_Types
	{
		[Test]
		public void It_Should_Fill_The_Value_Properly()
		{
			TestHelper.TestDeserialisation(When_Serialising_Enum_Types.Xml, "VP1", 
				typeof (Test_Classes.VirtualPropEnum), Test_Classes.VirtualPropEnum.FileNotFound);
		}
	}

	[TestFixture]
	public class When_Deserialising_Int_Types
	{
		[Test]
		public void It_Should_Serialise_To_This()
		{
			TestHelper.TestDeserialisation(When_Serialising_Int_Types.Xml, "VP1",
							typeof(Int32), 60);	
		}
	}

	[TestFixture]
	public class When_Deserialising_Decimal_Types
	{
		[Test]
		public void It_Should_Serialise_To_This()
		{
			TestHelper.TestDeserialisation(When_Serialising_Decimal_Types.Xml, "VP1",
							typeof(Double), 6.0d);	
		}
	}

	[TestFixture]
	public class When_Deserialising_String_Types
	{
		[Test]
		public void It_Should_Serialise_To_This()
		{
			TestHelper.TestDeserialisation(When_Serialising_String_Types.Xml, "VP1",
				typeof(String), "some string");	
		}
	}

	[TestFixture]
	public class When_Deserialising_Char_Types
	{
		[Test]
		public void It_Should_Serialise_To_This()
		{
			TestHelper.TestDeserialisation(When_Serialising_Char_Types.Xml, "VP1",
				typeof(char), 'T');	
		}
	}

	[TestFixture]
	public class When_Deserialising_Boolean_Types
	{
		[Test]
		public void It_Should_Serialise_To_This()
		{
			TestHelper.TestDeserialisation(When_Serialising_Boolean_Types.Xml, "VP1",
				typeof(bool), true);	
		}
	}
}
