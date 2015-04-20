using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;
using Slyce.Common.StringExtensions;

namespace Slyce.Common
{
	public interface IProcessor
	{
		bool GetBool(string name);
		string GetString(string name, string @default);
		string GetString(string name);
		int GetInt(string name);
		long GetLong(string name);
		T GetEnum<T>(string s) where T : struct, IConvertible;
		T? GetNullable<T>(string s) where T : struct, IConvertible;
		Guid GetGuid(string s);
		int? GetNullableInt(string name);
		bool Exists(string name);
		
	}

	/// <summary>
	/// Class for getting information from the inner text of an xml node
	/// and converting it to various different formats
	/// </summary>
	public class NodeProcessor : IProcessor
	{
		private readonly XPathNavigator navigator;
		private readonly XmlNode originalNode;
		private readonly AttributeProcessor attributeProcessor;
		private readonly XmlNamespaceManager ns;

		public NodeProcessor(XmlNode node)
		{
			originalNode = node;
			navigator = node.CreateNavigator();
			attributeProcessor = new AttributeProcessor(node);
			ns = new XmlNamespaceManager(node.OwnerDocument.NameTable);
		}

		public NodeProcessor(XmlNode node, XmlNamespaceManager ns) : this(node)
		{
			this.ns = ns;
		}

		public IProcessor Attributes { get { return attributeProcessor; } }

		public bool GetBool(string name)
		{
			return GetInnerText(name).As<bool>().Value;
		}

		public string GetString(string name, string @default)
		{
			return GetInnerText(name, @default);
		}

		public string GetString(string name)
		{
			return GetInnerText(name);
		}

		private string GetInnerText(string name)
		{
			XPathNavigator node = navigator.SelectSingleNode(name, ns);
			if(node == null)
				throw new ArgumentException(string.Format("Cannot find child node called {0} under node {1}", name, navigator.Name));
			return node.ToString();
		}

		private string GetInnerText(string name, string @default)
		{
			var node = navigator.SelectSingleNode(name, ns);
			return node == null ? @default : node.ToString();
		}

		private IEnumerable<string> GetInnerTextMultiple(string name)
		{
			var nodes = navigator.Select(name, ns);
			foreach(XPathNavigator node in nodes)
			{
				yield return node.ToString();
			}
		}

		public IEnumerable<string> GetStrings(string name)
		{
			return GetInnerTextMultiple(name);
		}

		public int GetInt(string name)
		{
			return int.Parse(GetInnerText(name));
		}

		public long GetLong(string name)
		{
			return long.Parse(GetInnerText(name));
		}

		public T GetEnum<T>(string s) where T : struct, IConvertible
		{
			return GetInnerText(s).As<T>().Value;
		}

		public T? GetNullable<T>(string s) where T : struct, IConvertible
		{
			return GetInnerText(s, "").As<T>();
		}

		public Guid GetGuid(string s)
		{
			return new Guid(GetInnerText(s));
		}

		public int? GetNullableInt(string name)
		{
			string n = GetInnerText(name, "");
			return string.IsNullOrEmpty(n) ? null : n.As<int>();
		}

		public bool Exists(string name)
		{
			return navigator.SelectSingleNode(name, ns) != null;
		}

		public NodeProcessor SubNode(string name)
		{
			if(Exists(name) == false)
				throw new ArgumentException(string.Format("Cannot find node named {0} under node {1}", navigator.Name, name));
			return new NodeProcessor(originalNode.SelectSingleNode(name, ns), ns);
		}
	}

	public class AttributeProcessor : IProcessor
	{
		private readonly XmlNode node;

		public AttributeProcessor(XmlNode navigable)
		{
			node = navigable;
		}

		public bool GetBool(string name)
		{
			return GetInnerText(name).As<bool>().Value;
		}

		public string GetString(string name, string @default)
		{
			return GetInnerText(name, @default);
		}

		public string GetString(string name)
		{
			return GetInnerText(name);
		}

		private string GetInnerText(string name)
		{
			var attr = node.Attributes[name];
			if (attr == null)
				throw new ArgumentException(string.Format("Cannot find attribute called {0} on node {1}", name, node.Name));
			return attr.Value;
		}

		private string GetInnerText(string name, string @default)
		{
			var attr = node.Attributes[name];
			return attr == null ? @default : attr.Value;
		}

		public int GetInt(string name)
		{
			return int.Parse(GetInnerText(name));
		}

		public long GetLong(string name)
		{
			return long.Parse(GetInnerText(name));
		}

		public T GetEnum<T>(string s) where T : struct, IConvertible
		{
			return GetInnerText(s).As<T>().Value;
		}

		public T? GetNullable<T>(string s) where T : struct, IConvertible
		{
			return GetInnerText(s, "").As<T>();
		}

		public Guid GetGuid(string s)
		{
			return new Guid(GetInnerText(s));
		}

		public int? GetNullableInt(string name)
		{
			string n = GetInnerText(name, "");
			return string.IsNullOrEmpty(n) ? null : n.As<int>();
		}

		public bool Exists(string name)
		{
			return node.Attributes[name] != null;
		}
	}
}
