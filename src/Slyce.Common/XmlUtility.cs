using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Slyce.Common
{
	public static class XmlUtility
	{
		public static XmlNode AddNode(this XmlNode node, string name)
		{
			XmlNode childNode = node.OwnerDocument.CreateElement(name);
			node.AppendChild(childNode);
			return childNode;
		}

		public static XmlNode AddNode(this XmlDocument doc, string name)
		{
			XmlNode childNode = doc.CreateElement(name);
			doc.AppendChild(childNode);
			return childNode;
		}

		public static string GetAttributeValue(this XmlNode node, string attributeName)
		{
			return node.Attributes[attributeName].Value;
		}

		public static string GetAttributeValue(this XmlNode node, string attributeName, string defaultValueIfMissing)
		{
			if (node.Attributes[attributeName] == null)
				return defaultValueIfMissing;

			return node.Attributes[attributeName].Value;
		}

		public static void AddCdataSection(this XmlNode node, string value)
		{
			XmlCDataSection cdata = node.OwnerDocument.CreateCDataSection(value);
			node.AppendChild(cdata);
		}

		public static XmlNode AddAttribute(this XmlNode parentNode, string name, string value)
		{
			XmlAttribute att = parentNode.OwnerDocument.CreateAttribute(name);
			att.Value = value;
			parentNode.Attributes.Append(att);
			return parentNode;
		}

		public static string GetIndentedUTF8Xml(this string xml)
		{
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);
			return GetIndentedUTF8Xml(doc);
		}

		public static string GetIndentedUTF8Xml(this XmlDocument doc)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				var settings = new XmlWriterSettings { Indent = true, IndentChars = "\t", Encoding = Encoding.Unicode };
				using (XmlWriter writer = XmlWriter.Create(ms, settings))
				{
					doc.Save(writer);
				}

				return Encoding.UTF8.GetString(ms.ToArray());
			}
		}
	}

	public class WriterHelper
	{
		private readonly XmlWriter writer;

		public WriterHelper(XmlWriter writer)
		{
			this.writer = writer;
		}

		public ElementWriter Element(string elementName)
		{
			return new ElementWriter(writer, elementName);
		}
	}

	public class ElementWriter : IDisposable
	{
		private readonly XmlWriter writer;

		public ElementWriter(XmlWriter writer, string elementName)
		{
			this.writer = writer;
			writer.WriteStartElement(elementName);
		}

		public void Dispose()
		{
			writer.WriteEndElement();
		}
	}
}
