using System;
using System.Text;
using System.Xml;
using ArchAngel.Interfaces;
using Slyce.Common;

namespace ArchAngel.Providers.EntityModel.Model.MappingLayer
{
	public class MappingSetSerialisationScheme
	{
		public string SerialiseMapping(Mapping mapping)
		{
			return Serialise(writer => SerialiseMappingInternal(mapping, writer));
		}

		public string SerialiseComponentMapping(ComponentMapping mapping)
		{
			return Serialise(writer => SerialiseComponentMappingInternal(mapping, writer));
		}

		public string SerialiseReferenceMapping(TableReferenceMapping mapping)
		{
			return Serialise(writer => SerialiseReferenceMappingInternal(mapping, writer));
		}

		public string SerialiseRelationshipMapping(RelationshipReferenceMapping mapping)
		{
			return Serialise(writer => SerialiseRelationshipMappingInternal(mapping, writer));
		}

		public string SerialiseMappingSet(MappingSet set)
		{
			return Serialise(writer => SerialiseMappingSetInternal(set, writer));
		}

		public void SerialiseRelationshipMappingInternal(RelationshipReferenceMapping mapping, XmlWriter writer)
		{
			if (mapping.FromRelationship == null)
				//throw new ArgumentNullException("mapping", string.Format("mapping.FromRelationship cannot be null [{0}]", mapping.DisplayName));
				return;
			if (mapping.ToReference == null)
				//throw new ArgumentNullException("mapping", string.Format("mapping.ToReference cannot be null [{0}]", mapping.DisplayName));
				return;

			WriterHelper document = new WriterHelper(writer);

			using (document.Element("RelationshipReferenceMapping"))
			{
				writer.WriteElementString("FromRelationship", mapping.FromRelationship.Identifier.ToString());
				writer.WriteElementString("ToReference", mapping.ToReference.Identifier.ToString());

				ProcessScriptBase(mapping, writer);
			}
		}

		private void SerialiseReferenceMappingInternal(TableReferenceMapping mapping, XmlWriter writer)
		{
			if (mapping.FromTable == null)
				throw new ArgumentNullException("mapping", string.Format("mapping.FromTable cannot be null [{0}]", mapping.DisplayName));
			if (mapping.ToReference == null)
				throw new ArgumentNullException("mapping", string.Format("mapping.ToReference cannot be null [{0}]", mapping.DisplayName));

			WriterHelper document = new WriterHelper(writer);

			using (document.Element("TableReferenceMapping"))
			{
				writer.WriteElementString("FromTable", mapping.FromTable.Name);
				writer.WriteElementString("FromSchema", mapping.FromTable.Schema);
				writer.WriteElementString("ToReference", mapping.ToReference.Identifier.ToString());

				ProcessScriptBase(mapping, writer);
			}
		}

		private void SerialisePrefixInternal(string prefix, XmlWriter writer)
		{
			writer.WriteStartElement("Prefix");
			writer.WriteAttributeString("value", prefix);
			writer.WriteEndElement();
		}

		private void SerialiseSuffixInternal(string prefix, XmlWriter writer)
		{
			writer.WriteStartElement("Suffix");
			writer.WriteAttributeString("value", prefix);
			writer.WriteEndElement();
		}

		private void SerialiseMappingSetInternal(MappingSet set, XmlWriter writer)
		{
			writer.WriteStartElement("MappingSet");
			writer.WriteAttributeString("Version", 1.ToString());

			if (set.Mappings.Count > 0)
			{
				writer.WriteStartElement("Mappings");

				foreach (var mapping in set.Mappings)
					SerialiseMappingInternal(mapping, writer);

				writer.WriteEndElement();
			}

			WriteReferenceMappings(writer, set);

			if (set.ComponentMappings.Count > 0)
			{
				writer.WriteStartElement("ComponentMappings");

				foreach (var mapping in set.ComponentMappings)
					SerialiseComponentMappingInternal(mapping, writer);

				writer.WriteEndElement();
			}
			#region Table Prefixes
			if (set.TablePrefixes.Count > 0)
			{
				writer.WriteStartElement("TablePrefixes");

				foreach (var prefix in set.TablePrefixes)
					SerialisePrefixInternal(prefix, writer);

				writer.WriteEndElement();
			}
			#endregion

			#region Column Prefixes
			if (set.ColumnPrefixes.Count > 0)
			{
				writer.WriteStartElement("ColumnPrefixes");

				foreach (var prefix in set.ColumnPrefixes)
					SerialisePrefixInternal(prefix, writer);

				writer.WriteEndElement();
			}
			#endregion

			#region Table Suffixes
			if (set.TableSuffixes.Count > 0)
			{
				writer.WriteStartElement("TableSuffixes");

				foreach (var prefix in set.TableSuffixes)
					SerialiseSuffixInternal(prefix, writer);

				writer.WriteEndElement();
			}
			#endregion

			#region Column Suffixes
			if (set.ColumnSuffixes.Count > 0)
			{
				writer.WriteStartElement("ColumnSuffixes");

				foreach (var prefix in set.ColumnSuffixes)
					SerialiseSuffixInternal(prefix, writer);

				writer.WriteEndElement();
			}
			#endregion
			ProcessScriptBase(set, writer);
			writer.WriteEndElement();
		}

		private void WriteReferenceMappings(XmlWriter writer, MappingSet set)
		{
			if (set.ReferenceMappings.Count > 0 || set.RelationshipMappings.Count > 0)
				writer.WriteStartElement("ReferenceMappings");

			foreach (var mapping in set.ReferenceMappings)
				SerialiseReferenceMappingInternal(mapping, writer);

			foreach (var mapping in set.RelationshipMappings)
				SerialiseRelationshipMappingInternal(mapping, writer);

			if (set.ReferenceMappings.Count > 0 || set.RelationshipMappings.Count > 0)
				writer.WriteEndElement();
		}

		private void SerialiseMappingInternal(Mapping mapping, XmlWriter writer)
		{
			if (mapping.FromTable == null)
				return; // throw new ArgumentException("FromTable in Mapping cannot be null");
			if (mapping.ToEntity == null)
				return; // throw new ArgumentException("ToEntity in Mapping cannot be null");
			if (mapping.ToProperties == null)
				return; // throw new ArgumentException("ToProperties in Mapping cannot be null");
			if (mapping.FromColumns == null)
				return; // throw new ArgumentException("FromColumns in Mapping cannot be null");
			if (mapping.FromColumns.Count <= 0)
				return; // throw new ArgumentException("FromColumns in Mapping cannot be empty");
			if (mapping.ToProperties.Count <= 0)
				return; // throw new ArgumentException("ToProperties in Mapping cannot be empty");

			writer.WriteStartElement("Mapping");
			{
				writer.WriteStartElement("FromColumns");

				foreach (var column in mapping.FromColumns)
					writer.WriteElementString("Column", column.Name);

				writer.WriteEndElement();

				writer.WriteElementString("FromTable", mapping.FromTable.Name);
				writer.WriteElementString("FromSchema", mapping.FromTable.Schema);
				writer.WriteElementString("ToEntity", mapping.ToEntity.Name);

				writer.WriteStartElement("ToProperties");

				foreach (var property in mapping.ToProperties)
					writer.WriteElementString("Property", property.Name);

				writer.WriteEndElement();
				ProcessScriptBase(mapping, writer);
			}
			writer.WriteEndElement();
		}

		private void SerialiseComponentMappingInternal(ComponentMapping mapping, XmlWriter writer)
		{
			if (mapping.FromTable == null)
				throw new ArgumentNullException("mapping", "mapping.FromTable cannot be null");
			if (mapping.ToComponent == null)
				throw new ArgumentNullException("mapping", "mapping.ToComponent cannot be null");
			if (mapping.ToComponent.Specification == null)
				throw new ArgumentNullException("mapping", "mapping.ToComponent.Specification cannot be null");
			if (mapping.ToComponent.ParentEntity == null)
				throw new ArgumentNullException("mapping", "mapping.ToComponent.ParentEntity cannot be null");
			if (mapping.ToProperties == null)
				throw new ArgumentException("ToProperties in Mapping cannot be null");
			if (mapping.FromColumns == null)
				throw new ArgumentException("FromColumns in Mapping cannot be null");
			if (mapping.FromColumns.Count <= 0)
				throw new ArgumentException("FromColumns in Mapping cannot be empty");
			if (mapping.ToProperties.Count <= 0)
				throw new ArgumentException("ToProperties in Mapping cannot be empty");

			WriterHelper document = new WriterHelper(writer);

			using (document.Element("ComponentMapping"))
			{
				writer.WriteElementString("FromTable", mapping.FromTable.Name);
				writer.WriteElementString("FromSchema", mapping.FromTable.Schema);

				using (document.Element("ToComponent"))
				{
					writer.WriteAttributeString("specification", mapping.ToComponent.Specification.Name);
					writer.WriteAttributeString("parent-entity", mapping.ToComponent.ParentEntity.Name);
					writer.WriteAttributeString("name", mapping.ToComponent.Name);
				}

				using (document.Element("FromColumns"))
					foreach (var column in mapping.FromColumns)
						writer.WriteElementString("Column", column.Name);

				using (document.Element("ToProperties"))
					foreach (var property in mapping.ToProperties)
						writer.WriteElementString("Property", property.RepresentedProperty.Name);

				ProcessScriptBase(mapping, writer);
			}
		}

		private string Serialise(Action<XmlWriter> writeAction)
		{
			var sb = new StringBuilder();
			// If OmitXmlDecl is not set to true, an <?xml> node will be placed at the start
			// of the snippet, which is not what we want.
			XmlWriter writer = GetWriter(sb, new XmlWriterSettings { OmitXmlDeclaration = true });
			writeAction(writer);
			writer.Close();
			return sb.ToString();
		}

		private static XmlWriter GetWriter(StringBuilder sb, XmlWriterSettings settings)
		{
			settings.Indent = true;
			settings.IndentChars = "\t";
			XmlWriter writer = XmlWriter.Create(sb, settings);

			if (writer == null)
				throw new InvalidOperationException("Couldn't create an XML Writer. ");

			return writer;
		}

		public void ProcessScriptBase(IScriptBaseObject scriptBase, XmlWriter writer)
		{
			if (scriptBase.Ex != null && scriptBase.Ex.Count > 0)
			{
				var serialiser = new VirtualPropertySerialiser();
				serialiser.SerialiseVirtualProperties(scriptBase.Ex, writer);
			}
		}
	}
}