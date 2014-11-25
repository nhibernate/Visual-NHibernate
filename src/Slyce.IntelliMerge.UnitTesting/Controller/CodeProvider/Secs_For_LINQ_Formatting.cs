using System;
using NUnit.Framework;

namespace Specs_For_CSharp_Formatter_Body_Text
{
	[TestFixture]
	public class LINQ_Formatting : MethodBodyTestBase
	{
		[Test]
		public void from_select()
		{
			const string methodBody = "var i = from  item in  list  select item;";
			string expectedText = "{\n\tvar i = from item in list select item;\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void from_let_select()
		{
			const string methodBody = "var i = from  item in  list let item = 6 select item;";
			string expectedText = "{\n\tvar i = from item in list let item = 6 select item;\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void from_where_select()
		{
			const string methodBody = "var i = from  item in  list where item == 6 select item;";
			string expectedText = "{\n\tvar i = from item in list where item == 6 select item;\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void from_join_select()
		{
			const string methodBody = "var i = from  item in  list join ident in list2 on list1 equals 5 select item;";
			string expectedText = "{\n\tvar i = from item in list join ident in list2 on list1 equals 5 select item;\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void from_join_into_select()
		{
			const string methodBody = "var i = from  item in  list join ident in list2 on list1 equals 5 into list3 select item;";
			string expectedText = "{\n\tvar i = from item in list join ident in list2 on list1 equals 5 into list3 select item;\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void from_orderby_select()
		{
			string methodBody = "var i = from  item in  list orderby i.Value ascending select i;";
			string expectedText = "{\n\tvar i = from item in list orderby i.Value ascending select i;\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);

			methodBody = "var i = from  item in  list orderby i.Value descending select i;";
			expectedText = "{\n\tvar i = from item in list orderby i.Value descending select i;\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);

			methodBody = "var i = from  item in  list orderby i.Value select i;";
			expectedText = "{\n\tvar i = from item in list orderby i.Value select i;\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);

			methodBody = "var i = from  item in  list orderby i.Value,  i.Value2 ascending select i;";
			expectedText = "{\n\tvar i = from item in list orderby i.Value, i.Value2 ascending select i;\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void from_groupby()
		{
			const string methodBody = "var i = from  item in  list group item by item.Value;";
			string expectedText = "{\n\tvar i = from item in list group item by item.Value;\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void from_groupby_into()
		{
			const string methodBody = "var i = from  item in  list group item by item.Value into list2 where item.Value == 5 select item.Value;";
			string expectedText = "{\n\tvar i = from item in list group item by item.Value into list2 where item.Value == 5 select item.Value;\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);
		}

		[Test]
		public void from_select_into()
		{
			const string methodBody = "var i = from  item in  list select item.Value into list2 where item.Value == 5 select item.Value;";
			string expectedText = "{\n\tvar i = from item in list select item.Value into list2 where item.Value == 5 select item.Value;\n}\n".Replace("\n", Environment.NewLine);
			TestBodyText(methodBody, expectedText);
		}
	}
}