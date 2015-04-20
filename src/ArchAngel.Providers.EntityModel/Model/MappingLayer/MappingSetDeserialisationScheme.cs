using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using ArchAngel.Interfaces;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using Slyce.Common;
using Slyce.Common.Exceptions;

namespace ArchAngel.Providers.EntityModel.Model.MappingLayer
{
	public class MappingSetDeserialisationScheme
	{
		public MappingSet DeserialiseMappingSet(string xml, IDatabase database, EntitySet set)
		{
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);
			return DeserialiseMappingSet(doc.DocumentElement, database, set);
		}

		public Mapping DeserialiseMapping(XmlNode node, IDatabase database, EntitySet set)
		{
			NodeProcessor proc = new NodeProcessor(node);
			Mapping mapping = new MappingImpl();

			mapping.FromTable = database.GetTable(proc.GetString("FromTable"), proc.GetString("FromSchema"));

			if (mapping.FromTable == null)
				mapping.FromTable = database.GetView(proc.GetString("FromTable"), proc.GetString("FromSchema"));

			mapping.ToEntity = set.GetEntity(proc.GetString("ToEntity"));

			if (mapping.FromTable == null || mapping.ToEntity == null)
				return null;

			var columnNodes = node.SelectNodes("FromColumns/Column");
			var propNodes = node.SelectNodes("ToProperties/Property");
			if (columnNodes == null) throw new DeserialisationException("There were no columns in this Mapping xml");
			if (propNodes == null) throw new DeserialisationException("There were no properties in this Mapping xml");

			List<IColumn> columns = new List<IColumn>();
			foreach (XmlNode columnNode in columnNodes)
				columns.Add(mapping.FromTable.GetColumn(columnNode.InnerText));

			List<Property> properties = new List<Property>();
			foreach (XmlNode propNode in propNodes)
				properties.Add(mapping.ToEntity.GetProperty(propNode.InnerText));

			if (columns.Count != properties.Count) throw new DeserialisationException("Mapping contains different numbers of columns and properties");

			for (int i = 0; i < columns.Count; i++)
			{
				if (properties[i] != null && columns[i] != null)
					mapping.AddPropertyAndColumn(properties[i], columns[i]);
			}

			ProcessScriptBase(mapping, node);

			return mapping;
		}

		public string DeserialisePrefix(XmlNode node)
		{
			return node.Attributes["value"].Value;
		}

		public ComponentMapping DeserialiseComponentMapping(XmlNode mappingNode, IDatabase database, EntitySet set)
		{
			NodeProcessor proc = new NodeProcessor(mappingNode);
			ComponentMapping mapping = new ComponentMappingImpl();

			mapping.FromTable = database.GetTable(proc.GetString("FromTable"), proc.GetString("FromSchema"));

			NodeProcessor specProcessor = new NodeProcessor(mappingNode.SelectSingleNode("ToComponent"));

			var specification = set.GetComponentSpecification(specProcessor.Attributes.GetString("specification"));
			var parentEntity = set.GetEntity(specProcessor.Attributes.GetString("parent-entity"));
			string name = specProcessor.Attributes.GetString("name");

			if (parentEntity == null)
				throw new DeserialisationException(string.Format("Could not find the Entity named {0}", name));
			if (specification == null)
				throw new DeserialisationException(string.Format("Could not find the Component Specification named {0}", name));

			var component = specification.ImplementedComponents.FirstOrDefault(c => ReferenceEquals(c.ParentEntity, parentEntity) && c.Name == name);
			if (component == null)
				throw new DeserialisationException(string.Format("Could not find the component named {0}", name));

			mapping.ToComponent = component;
			var columnNodes = mappingNode.SelectNodes("FromColumns/Column");
			var propNodes = mappingNode.SelectNodes("ToProperties/Property");
			if (columnNodes == null) throw new DeserialisationException("There were no columns in this Mapping xml");
			if (propNodes == null) throw new DeserialisationException("There were no properties in this Mapping xml");

			List<IColumn> columns = new List<IColumn>();
			foreach (XmlNode columnNode in columnNodes)
			{
				columns.Add(mapping.FromTable.GetColumn(columnNode.InnerText));
			}

			List<ComponentPropertyMarker> properties = new List<ComponentPropertyMarker>();
			foreach (XmlNode propNode in propNodes)
			{
				properties.Add(mapping.ToComponent.GetProperty(propNode.InnerText));
			}

			if (columns.Count != properties.Count) throw new DeserialisationException("Mapping contains different numbers of columns and properties");

			for (int i = 0; i < columns.Count; i++)
			{
				mapping.AddPropertyAndColumn(properties[i], columns[i]);
			}

			ProcessScriptBase(mapping, mappingNode);
			return mapping;
		}

		public RelationshipReferenceMapping DeserialiseRelationshipMapping(XmlNode relationshipMappingNode, IDatabase database, EntitySet set)
		{
			NodeProcessor proc = new NodeProcessor(relationshipMappingNode);
			RelationshipReferenceMapping mapping = new RelationshipReferenceMappingImpl();

			Guid fromRelationshipId = proc.GetGuid("FromRelationship");
			Guid toReferenceId = proc.GetGuid("ToReference");

			mapping.FromRelationship = database.Relationships.FirstOrDefault(r => r.Identifier == fromRelationshipId);
			mapping.ToReference = set.References.FirstOrDefault(r => r.Identifier == toReferenceId);

			if (mapping.ToReference == null)
				return null;

			ProcessScriptBase(mapping, relationshipMappingNode);

			return mapping;
		}

		public TableReferenceMapping DeserialiseReferenceMapping(XmlNode referenceMappingNode, IDatabase database, EntitySet set)
		{
			NodeProcessor proc = new NodeProcessor(referenceMappingNode);
			TableReferenceMapping mapping = new TableReferenceMappingImpl();
			string fromTableName = proc.GetString("FromTable");
			string fromSchema = proc.GetString("FromSchema");
			Guid toReferenceIdentifier = proc.GetGuid("ToReference");
			var reference = set.References.FirstOrDefault(r => r.Identifier == toReferenceIdentifier);
			mapping.FromTable = database.GetTable(fromTableName, fromSchema);
			mapping.ToReference = reference;
			ProcessScriptBase(mapping, referenceMappingNode);
			return mapping;
		}

		public MappingSet DeserialiseMappingSet(XmlNode mappingSetNode, IDatabase database, EntitySet entitySet)
		{
			var mappingSet = new MappingSetImpl(database, entitySet);
			var nodes = mappingSetNode.SelectNodes("Mappings/Mapping");

			if (nodes != null)
				foreach (XmlNode node in nodes)
				{
					var mapping = DeserialiseMapping(node, database, entitySet);

					if (mapping != null)
						mappingSet.AddMapping(mapping);
				}
			var refNodes = mappingSetNode.SelectNodes("ReferenceMappings/TableReferenceMapping");

			if (refNodes != null)
				foreach (XmlNode node in refNodes)
					mappingSet.AddMapping(DeserialiseReferenceMapping(node, database, entitySet));

			var relationshipNodes = mappingSetNode.SelectNodes("ReferenceMappings/RelationshipReferenceMapping");

			if (relationshipNodes != null)
				foreach (XmlNode node in relationshipNodes)
				{
					RelationshipReferenceMapping relRefMapping = DeserialiseRelationshipMapping(node, database, entitySet);

					if (relRefMapping != null)
						mappingSet.AddMapping(relRefMapping);
				}
			var componentNodes = mappingSetNode.SelectNodes("ComponentMappings/ComponentMapping");

			if (componentNodes != null)
				foreach (XmlNode node in componentNodes)
					mappingSet.AddMapping(DeserialiseComponentMapping(node, database, entitySet));

			#region Table Prefixes
			var prefixNodes = mappingSetNode.SelectNodes("TablePrefixes/Prefix");

			if (prefixNodes != null)
				foreach (XmlNode node in prefixNodes)
					mappingSet.TablePrefixes.Add(DeserialisePrefix(node));

			#endregion

			#region Column Prefixes
			prefixNodes = mappingSetNode.SelectNodes("ColumnPrefixes/Prefix");

			if (prefixNodes != null)
				foreach (XmlNode node in prefixNodes)
					mappingSet.ColumnPrefixes.Add(DeserialisePrefix(node));

			#endregion

			#region Table Suffixes
			prefixNodes = mappingSetNode.SelectNodes("TableSuffixes/Suffix");

			if (prefixNodes != null)
				foreach (XmlNode node in prefixNodes)
					mappingSet.TableSuffixes.Add(DeserialisePrefix(node));

			#endregion

			#region Column Prefixes
			prefixNodes = mappingSetNode.SelectNodes("ColumnSuffixes/Suffix");

			if (prefixNodes != null)
				foreach (XmlNode node in prefixNodes)
					mappingSet.ColumnSuffixes.Add(DeserialisePrefix(node));

			#endregion

			ProcessScriptBase(mappingSet, mappingSetNode);
			return mappingSet;
		}

		public void ProcessScriptBase(IScriptBaseObject scriptBase, XmlNode node)
		{
			var virtualPropertiesNode = node.SelectSingleNode(VirtualPropertyDeserialiser.VirtualPropertiesNodeName);

			if (virtualPropertiesNode != null)
			{
				var deserialiser = new VirtualPropertyDeserialiser();
				scriptBase.Ex = deserialiser.DeserialiseVirtualProperties(virtualPropertiesNode);
			}
		}
	}
}