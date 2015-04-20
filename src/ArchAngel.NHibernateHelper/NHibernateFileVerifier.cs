using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.XPath;

namespace ArchAngel.NHibernateHelper
{
	public class NHibernateFileVerifier
	{
		private XmlSchema ConfigSchema;
		private XmlSchema MappingSchema;
		private XmlSchema ValidationConfigSchema;
		private XmlSchema ValidationMappingSchema;

		public NHibernateFileVerifier()
		{
			var assembly = typeof(NHibernateFileVerifier).Assembly;

			LoadConfigSchema(assembly);
			LoadMappingSchema(assembly);
			LoadValidationConfigSchema(assembly);
			LoadValidationMappingSchema(assembly);
		}

		private static bool ValidateFile(XmlSchema schema, XDocument doc)
		{
			var schemaName = schema.TargetNamespace;

			if (HasSchema(doc, schemaName) == false)
				return false;

			XmlSchemaSet set = new XmlSchemaSet();
			set.Add(schema);

			bool hasErrors = false;

			doc.Validate(set, (o, e) =>
			{
				hasErrors = true;
			});

			return !hasErrors;
		}

		private static bool HasSchema(XDocument doc, string schemaName)
		{
			IDictionary<string, string> schemasInFile = GetSchemasInFile(doc);

			bool foundOurSchema = false;

			foreach (KeyValuePair<string, string> namespaces in schemasInFile)
			{
				if (namespaces.Value.CompareTo(schemaName) == 0)
					foundOurSchema = true;
			}

			return foundOurSchema;
		}

		public static IDictionary<string, string> GetSchemasInFile(XDocument doc)
		{
			XPathNavigator nav = doc.CreateNavigator();
			nav.MoveToFollowing(XPathNodeType.Element);
			return nav.GetNamespacesInScope(XmlNamespaceScope.All);
		}

		private void LoadValidationMappingSchema(Assembly assembly)
		{
			var validationMappingSchemaStream = assembly.GetManifestResourceStream("ArchAngel.NHibernateHelper.nhv-mapping.xsd");

			if (validationMappingSchemaStream == null)
				throw new Exception("Cannot find the NHibernate Validator Mapping Schema as an embedded resource in the current assembly.");

			ValidationMappingSchema = XmlSchema.Read(validationMappingSchemaStream, SchemaReadError);

		}

		private void LoadValidationConfigSchema(Assembly assembly)
		{
			var validationConfigSchemaStream = assembly.GetManifestResourceStream("ArchAngel.NHibernateHelper.nhv-configuration.xsd");
			if (validationConfigSchemaStream == null)
				throw new Exception("Cannot find the NHibernate Validator Configuration Schema as an embedded resource in the current assembly.");

			ValidationConfigSchema = XmlSchema.Read(validationConfigSchemaStream, SchemaReadError);
		}

		private void LoadMappingSchema(Assembly assembly)
		{
			var mappingSchemaStream = assembly.GetManifestResourceStream("ArchAngel.NHibernateHelper.nhibernate-mapping.xsd");

			if (mappingSchemaStream == null)
				throw new Exception("Cannot find the NHibernate Mapping Schema as an embedded resource in the current assembly.");

			MappingSchema = XmlSchema.Read(mappingSchemaStream, SchemaReadError);
		}

		private void LoadConfigSchema(Assembly assembly)
		{
			var configSchemaStream = assembly.GetManifestResourceStream("ArchAngel.NHibernateHelper.nhibernate-configuration.xsd");

			if (configSchemaStream == null)
				throw new Exception("Cannot find the NHibernate Configuration Schema as an embedded resource in the current assembly.");

			ConfigSchema = XmlSchema.Read(configSchemaStream, SchemaReadError);
		}


		public bool IsValidConfigFile(XDocument document)
		{
			return ValidateFile(ConfigSchema, document);
		}

		public bool IsValidMappingFile(TextReader file)
		{
			return ValidateFile(MappingSchema, XDocument.Load(file));
		}

		public bool IsValidValidationConfigFile(TextReader file)
		{
			return ValidateFile(ValidationConfigSchema, XDocument.Load(file));
		}

		public bool IsValidValidationMappingFile(TextReader file)
		{
			return ValidateFile(ValidationMappingSchema, XDocument.Load(file));
		}

		private void SchemaReadError(object sender, ValidationEventArgs e)
		{

		}
	}
}