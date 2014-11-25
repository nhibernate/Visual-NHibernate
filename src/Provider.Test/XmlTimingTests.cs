using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;

namespace Provider.Test
{
	public class XmlTimingTests
	{
		public static void Main()
		{
			var schemaReader = XmlReader.Create("database.xsd");
			var schema = XmlSchema.Read(schemaReader, ValidationEvent);

			Tester tester = new Tester();

			tester.RunTest("Took {0} ms to validate an XmlDocument file with {1} Entities", numEntities => DoXmlSizeTestXmlDoc(numEntities, schema), "xmlDocSize.csv", 20, 10);
			tester.RunTest("Took {0} ms to validate an XPathNavigator file with {1} Entities", numEntities => DoXmlSizeTestXPathNavigator(numEntities, schema), "XPathSize.csv", 20, 10);
			tester.RunTest("Took {0} ms to search an XmlDocument with {1} Entities", DoXmlSearchTestXmlDoc, "xmlDocSearch.csv", 20, 10);
			tester.RunTest("Took {0} ms to search an XmlDocument with {1} Entities", DoXmlSearchTestXPathNavigator, "XPathSearch.csv", 20, 10);
		}

		private static TimeSpan DoXmlSearchTestXmlDoc(int numEntities)
		{
			string xml = CreateXml(numEntities);

			XmlDocument doc = CreateXmlDocument(xml);

			DateTime start = DateTime.Now;

			for (int i = 0; i < 100000; i++)
				doc.SelectNodes("LogicalSchema/Table");

			return DateTime.Now - start;
		}

		private static TimeSpan DoXmlSearchTestXPathNavigator(int numEntities)
		{
			string xml = CreateXml(numEntities);

			var doc = CreateXPathNavigator(xml);

			DateTime start = DateTime.Now;

			for (int i = 0; i < 100000; i++ )
				doc.Select("LogicalSchema/Table");

			return DateTime.Now - start;
		}

		private static TimeSpan DoXmlSizeTestXmlDoc(int numEntities, XmlSchema schema)
		{
			string xml = CreateXml(numEntities);

			XmlDocument doc = CreateXmlDocument(xml, schema);

			DateTime start = DateTime.Now;

			doc.Validate(ValidationEvent);

			return DateTime.Now - start;
		}

		private static TimeSpan DoXmlSizeTestXPathNavigator(int numEntities, XmlSchema schema)
		{
			string xml = CreateXml(numEntities);

			XPathNavigator doc = CreateXPathNavigator(xml);
			var schemaSet = new XmlSchemaSet();
			schemaSet.Add(schema);

			DateTime start = DateTime.Now;

			doc.CheckValidity(schemaSet, ValidationEvent);

			return DateTime.Now - start;
		}

		private static XmlDocument CreateXmlDocument(string xml)
		{
			return CreateXmlDocument(xml, null);
		}

		private static XmlDocument CreateXmlDocument(string xml, XmlSchema schema)
		{
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);
			if(schema != null)
				doc.Schemas.Add(schema);
			return doc;
		}

		private static XPathNavigator CreateXPathNavigator(string xml)
		{
			return new XPathDocument(new StringReader(xml)).CreateNavigator();
		}

		private static string CreateXml(int numEntities)
		{
			string xml = @"<?xml version=""1.0"" encoding=""utf-16""?><LogicalSchema Version=""1""><DatabaseName>2Tables1Relationship.sdf</DatabaseName>";

			for (int i = 0; i < numEntities; i++)
				xml += ENTITY_XML + Environment.NewLine;

			xml += @"</LogicalSchema>";
			return xml;
		}

		private static void ValidationEvent(object sender, ValidationEventArgs e)
		{
			
		}

		private const string ENTITY_XML = @"
<Table>
    <Name>Table1</Name>
    <Alias>Table1</Alias>
    <AliasPlural />
    <Description />
    <Enabled>False</Enabled>
    <IsUserDefined>False</IsUserDefined>
    <UID>00000000-0000-0000-0000-000000000000</UID>
    <Keys>
      <Key>
        <Name>PK__Table1__000000000000006C</Name>
        <Alias>PK__Table1__000000000000006C</Alias>
        <AliasPlural />
        <Description />
        <Enabled>False</Enabled>
        <IsUserDefined>False</IsUserDefined>
        <UID>00000000-0000-0000-0000-000000000000</UID>
        <IsUnique>True</IsUnique>
        <Keytype>Primary</Keytype>
        <Columns>
          <ColumnName>ColumnT11</ColumnName>
        </Columns>
      </Key>
      <Key>
        <Name>UQ__Table1__000000000000005B</Name>
        <Alias>UQ__Table1__000000000000005B</Alias>
        <AliasPlural />
        <Description />
        <Enabled>False</Enabled>
        <IsUserDefined>False</IsUserDefined>
        <UID>00000000-0000-0000-0000-000000000000</UID>
        <IsUnique>True</IsUnique>
        <Keytype>Unique</Keytype>
        <Columns>
          <ColumnName>ColumnT11</ColumnName>
        </Columns>
      </Key>
      <Key>
        <Name>FK_Table1</Name>
        <Alias>FK_Table1</Alias>
        <AliasPlural />
        <Description />
        <Enabled>False</Enabled>
        <IsUserDefined>False</IsUserDefined>
        <UID>00000000-0000-0000-0000-000000000000</UID>
        <IsUnique>False</IsUnique>
        <Keytype>Foreign</Keytype>
        <ReferencedKey>
          <TableName>Table2</TableName>
          <KeyName>PK_Table2</KeyName>
        </ReferencedKey>
        <Columns>
          <ColumnName>ColumnT12</ColumnName>
        </Columns>
      </Key>
    </Keys>
    <Indexes>
      <Index>
        <Name>PK__Table1__000000000000006C</Name>
        <Alias>PK__Table1__000000000000006C</Alias>
        <AliasPlural />
        <Description />
        <Enabled>False</Enabled>
        <IsUserDefined>False</IsUserDefined>
        <UID>00000000-0000-0000-0000-000000000000</UID>
        <IsUnique>True</IsUnique>
        <IsClustered>False</IsClustered>
        <Datatype>PrimaryKey</Datatype>
        <Columns>
          <ColumnName>ColumnT11</ColumnName>
        </Columns>
      </Index>
      <Index>
        <Name>UQ__Table1__000000000000005B</Name>
        <Alias>UQ__Table1__000000000000005B</Alias>
        <AliasPlural />
        <Description />
        <Enabled>False</Enabled>
        <IsUserDefined>False</IsUserDefined>
        <UID>00000000-0000-0000-0000-000000000000</UID>
        <IsUnique>True</IsUnique>
        <IsClustered>False</IsClustered>
        <Datatype>Unique</Datatype>
        <Columns>
          <ColumnName>ColumnT11</ColumnName>
        </Columns>
      </Index>
      <Index>
        <Name>UQ_ColumnT13</Name>
        <Alias>UQ_ColumnT13</Alias>
        <AliasPlural />
        <Description />
        <Enabled>False</Enabled>
        <IsUserDefined>False</IsUserDefined>
        <UID>00000000-0000-0000-0000-000000000000</UID>
        <IsUnique>True</IsUnique>
        <IsClustered>False</IsClustered>
        <Datatype>None</Datatype>
        <Columns>
          <ColumnName>ColumnT13</ColumnName>
        </Columns>
      </Index>
      <Index>
        <Name>UQ_ColumnT13</Name>
        <Alias>UQ_ColumnT13</Alias>
        <AliasPlural />
        <Description />
        <Enabled>False</Enabled>
        <IsUserDefined>False</IsUserDefined>
        <UID>00000000-0000-0000-0000-000000000000</UID>
        <IsUnique>True</IsUnique>
        <IsClustered>False</IsClustered>
        <Datatype>Unique</Datatype>
        <Columns>
          <ColumnName>ColumnT13</ColumnName>
        </Columns>
      </Index>
    </Indexes>
    <Columns>
      <Column>
        <Name>ColumnT11</Name>
        <Alias>ColumnT11</Alias>
        <AliasPlural />
        <Description />
        <Enabled>False</Enabled>
        <IsUserDefined>False</IsUserDefined>
        <UID>00000000-0000-0000-0000-000000000000</UID>
        <AliasDisplay />
        <Datatype>int</Datatype>
        <Default />
        <InPrimaryKey>True</InPrimaryKey>
        <IsCalculated>False</IsCalculated>
        <IsComputed>False</IsComputed>
        <IsIdentity>False</IsIdentity>
        <IsNullable>False</IsNullable>
        <IsReadOnly>False</IsReadOnly>
        <IsUnique>True</IsUnique>
        <OrdinalPosition>1</OrdinalPosition>
        <Precision>10</Precision>
        <Scale>0</Scale>
        <Size>0</Size>
      </Column>
      <Column>
        <Name>ColumnT12</Name>
        <Alias>ColumnT12</Alias>
        <AliasPlural />
        <Description />
        <Enabled>False</Enabled>
        <IsUserDefined>False</IsUserDefined>
        <UID>00000000-0000-0000-0000-000000000000</UID>
        <AliasDisplay />
        <Datatype>int</Datatype>
        <Default />
        <InPrimaryKey>False</InPrimaryKey>
        <IsCalculated>False</IsCalculated>
        <IsComputed>False</IsComputed>
        <IsIdentity>False</IsIdentity>
        <IsNullable>False</IsNullable>
        <IsReadOnly>False</IsReadOnly>
        <IsUnique>False</IsUnique>
        <OrdinalPosition>2</OrdinalPosition>
        <Precision>10</Precision>
        <Scale>0</Scale>
        <Size>0</Size>
      </Column>
      <Column>
        <Name>ColumnT13</Name>
        <Alias>ColumnT13</Alias>
        <AliasPlural />
        <Description />
        <Enabled>False</Enabled>
        <IsUserDefined>False</IsUserDefined>
        <UID>00000000-0000-0000-0000-000000000000</UID>
        <AliasDisplay />
        <Datatype>int</Datatype>
        <Default />
        <InPrimaryKey>False</InPrimaryKey>
        <IsCalculated>False</IsCalculated>
        <IsComputed>False</IsComputed>
        <IsIdentity>False</IsIdentity>
        <IsNullable>True</IsNullable>
        <IsReadOnly>False</IsReadOnly>
        <IsUnique>True</IsUnique>
        <OrdinalPosition>3</OrdinalPosition>
        <Precision>10</Precision>
        <Scale>0</Scale>
        <Size>0</Size>
      </Column>
    </Columns>
  </Table>";
	}
}
