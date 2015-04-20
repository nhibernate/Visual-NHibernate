using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml;
using ArchAngel.Interfaces;
using ArchAngel.Interfaces.ITemplate;
using log4net;
using Slyce.Common;
using Slyce.Common.Exceptions;
using Slyce.Common.StringExtensions;

namespace ArchAngel.Providers.EntityModel.Model
{
	public class VirtualPropertySerialiser
	{
		public static readonly string VirtualPropertiesNodeName = "VirtualProperties";
		private static Type TypePropertiesForThisEntity = typeof(ArchAngel.Interfaces.NHibernateEnums.PropertiesForThisEntity);

		public void SerialiseVirtualProperties(IEnumerable<IUserOption> virtualProperties, XmlWriter writer)
		{
			writer.WriteStartElement(VirtualPropertiesNodeName);

			foreach (var vp in virtualProperties)
			{
				var isDefault = vp.DefaultValue != null && (vp.DefaultValue.Equals(vp.Value));

				if (isDefault || vp.Value == null)
					continue;

				SerialiseVirtualPropertyInternal(writer, vp);
			}
			writer.WriteEndElement();
		}

		public string SerialiseVirtualProperty(IUserOption virtualProperty)
		{
			StringBuilder sb = new StringBuilder(80);

			using (XmlWriter writer = XmlWriter.Create(sb, new XmlWriterSettings() { OmitXmlDeclaration = true, IndentChars = "\t" }))
			{
				if (writer == null)
					throw new SerialisationException("Could not aquire an XmlWriter");

				writer.WriteStartDocument();

				SerialiseVirtualPropertyInternal(writer, virtualProperty);

				writer.WriteEndDocument();
			}

			return sb.ToString();
		}

		private void SerialiseVirtualPropertyInternal(XmlWriter writer, IUserOption virtualProperty)
		{
			writer.WriteStartElement("VirtualProperty");
			writer.WriteAttributeString("name", virtualProperty.Name);
			writer.WriteAttributeString("type", virtualProperty.DataType.FullName);

			if (virtualProperty.DataType == TypePropertiesForThisEntity)
			{
				if (virtualProperty.Value is string)
					writer.WriteElementString("Value", virtualProperty.Value.ToString());
				else
					writer.WriteElementString("Value", ((ArchAngel.Providers.EntityModel.Model.EntityLayer.Property)virtualProperty.Value).Name);
			}
			else
				writer.WriteElementString("Value", virtualProperty.Value.ToString());

			writer.WriteEndElement();
		}
	}

	public class VirtualPropertyDeserialiser
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(VirtualPropertyDeserialiser));

		public static readonly string VirtualPropertiesNodeName = VirtualPropertySerialiser.VirtualPropertiesNodeName;

		public string GetVirtualPropertyName(XmlNode virtualPropertyNode)
		{
			NodeProcessor proc = new NodeProcessor(virtualPropertyNode);
			return proc.Attributes.GetString("name");
		}

		public List<IUserOption> DeserialiseVirtualProperties(XmlNode virtualPropertiesNode)
		{
			var virtualProperties = new List<IUserOption>();
			var nodes = virtualPropertiesNode.SelectNodes("VirtualProperty");

			if (nodes != null)
				foreach (XmlNode node in nodes)
					virtualProperties.Add(DeserialiseVirtualProperty(node));

			return virtualProperties;
		}

		private static Type TypePropertiesForThisEntity = typeof(ArchAngel.Interfaces.NHibernateEnums.PropertiesForThisEntity);

		public IUserOption DeserialiseVirtualProperty(XmlNode virtualPropertyNode)
		{
			NodeProcessor proc = new NodeProcessor(virtualPropertyNode);
			string name = proc.Attributes.GetString("name");

			string typeName = proc.Attributes.GetString("type");

			Type type = GetTypeNamed(typeName);

			if (type == null)
				throw new Exception(string.Format("Could not find type named \"{0}\" for virtual property {1}", typeName, name));

			string valueString = proc.GetString("Value");

			IUserOption option = new UserOption();
			option.Name = name;
			option.DataType = type;

			if (type == TypePropertiesForThisEntity)
				option.Value = valueString;
			else
				option.Value = valueString.As(option.DataType);

			return option;
		}

		internal Type GetTypeNamed(string typeName)
		{
			if (string.IsNullOrEmpty(typeName)) return null;

			foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
			{
				Type type = asm.GetType(typeName);
				if (type != null) return type;
			}

			throw new DeserialisationException("Could not find type " + typeName + ". A referenced assembly may be missing.");
		}
	}
}
