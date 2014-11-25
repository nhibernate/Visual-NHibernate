using ArchAngel.Providers.EntityModel.Helper;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Specs_For_SQL_To_CLR_Type_Conversion
{
	[TestFixture]
	public class When_Given_A_Correct_Sql_Type_It_Returns_The_Right_CLR_Type_Name
	{
		private void Test(string sqlType, string clrType)
		{
			Assert.That(SQLServer.ConvertSQLTypeNameToCLRTypeName(sqlType), Is.EqualTo(clrType));
		}

		[Test] public void Bit()			{ Test("bit", "System.Boolean"); }
		[Test] public void TinyInt()		{ Test("tinyint", "System.Byte"); }
		[Test] public void SmallInt()		{ Test("smallint", "System.Int16"); }
		[Test] public void Int()			{ Test("int", "System.Int32"); }
		[Test] public void BigInt()			{ Test("bigint", "System.Int64"); }
		[Test] public void SmallMoney()		{ Test("smallmoney", "System.Decimal"); }
		[Test] public void Money()			{ Test("money", "System.Decimal"); }
		[Test] public void Decimal20()		{ Test("decimal(20)", "System.Decimal"); }
		[Test] public void Decimal30()		{ Test("decimal(30)", "System.Double"); }
		[Test] public void Numeric20()		{ Test("numeric(20)", "System.Single"); }
		[Test] public void Numeric30()		{ Test("numeric(30)", "System.Double"); }
		[Test] public void Real()			{ Test("real", "System.Single"); }
		[Test] public void Float18()		{ Test("float(18)", "System.Single"); }
		[Test] public void Float()			{ Test("float", "System.Double"); }
		[Test] public void Float53()		{ Test("float(53)", "System.Double"); }
		
		[Test] public void Char()			{ Test("char", "System.Char"); }
		[Test] public void Char1()			{ Test("char(1)", "System.Char"); }
		[Test] public void NChar()			{ Test("nchar", "System.Char"); }
		[Test] public void NChar1()			{ Test("nchar(1)", "System.Char"); }
		[Test] public void Char50()			{ Test("char(50)", "System.String"); }
		[Test] public void NChar50()		{ Test("nchar(50)", "System.String"); }
		[Test] public void VarChar50()		{ Test("varchar(50)", "System.String"); }
		[Test] public void NVarChar50()		{ Test("nvarchar(50)", "System.String"); }
		[Test] public void VarCharMax()		{ Test("varchar(MAX)", "System.String"); }
		[Test] public void NVarCharMax()	{ Test("nvarchar(MAX)", "System.String"); }
		[Test] public void Text()			{ Test("text", "System.String"); }
		[Test] public void NText()			{ Test("ntext", "System.String"); }
		[Test] public void Xml()			{ Test("xml", "System.String"); }

		[Test] public void SmallDateTime()	{ Test("smalldatetime", "System.DateTime"); }
		[Test] public void DateTime()		{ Test("datetime", "System.DateTime"); }
		[Test] public void DateTime2()		{ Test("datetime2", "System.DateTime"); }
		[Test] public void DateTimeOffset()	{ Test("datetimeoffset", "System.DateTimeOffset"); }
		[Test] public void Date()			{ Test("date", "System.DateTime"); }
		[Test] public void Time()			{ Test("time", "System.DateTime"); }

		[Test] public void Binary50()		{ Test("binary(50)", "System.Byte[]"); }
		[Test] public void BinaryMax()		{ Test("varbinary(50)", "System.Byte[]"); }
		[Test] public void VarBinaryMax()	{ Test("varbinary(MAX)", "System.Byte[]"); }
		[Test] public void Image()			{ Test("image", "System.Byte[]"); }
		[Test] public void Timestamp()		{ Test("timestamp", "System.Byte[]"); }
		[Test] public void UniqueIdentifier(){ Test("uniqueidentifier", "System.Guid"); }
		[Test] public void SqlVariant()		{ Test("sql_variant", "System.Object"); }
	}
}