using System;
using System.Xml;

namespace Slyce.Common.ExtensionMethods
{
	public static class XmlExtensionMethods
	{
		public static XmlElement GetXmlDocRoot(this string xml)
		{
			var doc = new XmlDocument();
			doc.LoadXml(xml);
			return doc.DocumentElement;
		}

		public static string GetAttributeValueIfExists(this XmlNode node, string name)
		{
			if(node == null) throw new ArgumentNullException("node");

			XmlAttribute attribute = node.Attributes[name];
			
			return attribute != null ? attribute.Value : null;
		}
	}
}