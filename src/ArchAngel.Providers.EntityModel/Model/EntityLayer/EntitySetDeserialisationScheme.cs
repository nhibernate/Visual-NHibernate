using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using ArchAngel.Interfaces;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using Slyce.Common;
using Slyce.Common.Exceptions;
using Slyce.Common.StringExtensions;

namespace ArchAngel.Providers.EntityModel.Model.EntityLayer
{
	public class EntitySetDeserialisationScheme
	{
		private int Version = -1;

		public EntitySet DeserialiseEntitySet(string xml, IDatabase database)
		{
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);
			return DeserialiseEntitySet(doc.DocumentElement, database);
		}

		public Entity DeserialiseEntity(XmlNode entityNode)
		{
			var entity = new EntityImpl();
			entity.EventRaisingDisabled = true;

			entity.Name = entityNode.SelectSingleNode("Name").InnerText;
			entity.Schema = entityNode.SelectSingleNode("Schema").InnerText;

			XmlNodeList nodes = entityNode.SelectNodes("Properties/Property");
			if (nodes != null)
			{
				foreach (XmlNode propertyNode in nodes)
					entity.AddProperty(DeserialiseProperty(propertyNode, entity));
			}
			ProcessScriptBase(entity, entityNode);

			if (Version >= 3)
			{
				XmlNode generatorNode = entityNode.SelectSingleNode("Generator");

				if (generatorNode == null)
					entity.Generator = new EntityGenerator("assigned");
				else
					entity.Generator = DeserialiseGenerator(generatorNode);
			}
			if (Version > 4)
				entity.Cache = DeserialiseCache(entityNode.SelectSingleNode("Cache"));

			if (Version > 5)
				entity.IsAbstract = bool.Parse(entityNode.SelectSingleNode("IsAbstract").InnerText);

			if (Version > 6)
			{
				entity.Discriminator = DeserialiseDiscriminator(entityNode.SelectSingleNode("Discriminator"));
				entity.DiscriminatorValue = entityNode.SelectSingleNode("DiscriminatorValue").InnerText;
			}
			else
				entity.Discriminator = new Discriminator();

			entity.EventRaisingDisabled = false;
			return entity;
		}

		public EntityGenerator DeserialiseGenerator(XmlNode generatorNode)
		{
			var generator = new EntityGenerator(generatorNode.SelectSingleNode("ClassName").InnerText);
			//generator.EventRaisingDisabled = true;

			XmlNodeList nodes = generatorNode.SelectNodes("Parameters/Param");
			if (nodes != null)
			{
				foreach (XmlNode paramNode in nodes)
				{
					generator.Parameters.Add(new EntityGenerator.Parameter(paramNode.Attributes["Name"].Value, paramNode.Attributes["Value"].Value));
				}
			}
			//generator.EventRaisingDisabled = false;
			return generator;
		}

		public Cache DeserialiseCache(XmlNode cacheNode)
		{
			var cache = new Cache()
				{
					Usage = (Cache.UsageTypes)Enum.Parse(typeof(Cache.UsageTypes), cacheNode.Attributes["usage"].Value, true),
					Include = (Cache.IncludeTypes)Enum.Parse(typeof(Cache.IncludeTypes), cacheNode.Attributes["include"].Value, true),
					Region = cacheNode.Attributes["region"].Value
				};
			return cache;
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

		public void PostProcessEntity(EntitySet set, IDatabase database, Entity entity, XmlNode entityNode)
		{
			string parentName = entityNode.SelectSingleNode("Parent").InnerText;
			if (string.IsNullOrEmpty(parentName)) return;

			Entity parent = set.GetEntity(parentName);
			if (parent == null) return;

			parent.AddChild(entity);

			// Discriminator must be deserialised here, as this method needs the properties 
			// and parent to have been deserialised first.
			//entity.Discriminator = DeserialiseDiscriminator(entityNode.SelectSingleNode("Discriminator"), database);
		}

		public EntityKey DeserialiseKey(XmlNode keyNode, Entity parentEntity)
		{
			var key = new EntityKeyImpl();
			key.Parent = parentEntity;

			if (keyNode.InnerXml == "")
				return key;

			NodeProcessor keyProc = new NodeProcessor(keyNode);
			bool propertiesExist = false;
			bool componentExists = false;

			var nodes = keyNode.SelectNodes("Properties/Property");

			if (nodes != null)
			{
				propertiesExist = true;
				foreach (XmlNode node in nodes)
				{
					key.AddProperty(node.InnerText);
				}
			}

			if (keyProc.Exists("Component"))
			{
				componentExists = true;
				string componentName = keyProc.SubNode("Component").Attributes.GetString("name");
				Component component = parentEntity.Components.FirstOrDefault(c => c.Name == componentName);

				if (component == null)
					throw new DeserialisationException(string.Format("Could not find component named {0} on Entity {1}", componentName, parentEntity.Name));

				key.Component = component;
			}

			if (keyProc.Attributes.Exists("keytype"))
			{
				key.KeyType = keyProc.Attributes.GetEnum<EntityKeyType>("keytype");
			}
			else if (propertiesExist && componentExists)
			{
				throw new DeserialisationException(string.Format("Both a Component and a set of Properties were listed as the Key for entity {0}, but no keytype attribute was found on the Key node in the Entity Model XML.", parentEntity.Name));
			}

			ProcessScriptBase(key, keyNode);

			return key;
		}

		public Discriminator DeserialiseDiscriminator(XmlNode discriminatorNode)
		{
			NodeProcessor processor = new NodeProcessor(discriminatorNode);

			Discriminator discriminator = new Discriminator();

			discriminator.AllowNull = processor.GetBool("AllowNull");
			discriminator.ColumnName = processor.GetString("ColumnName");
			discriminator.DiscriminatorType = (Enums.DiscriminatorTypes)Enum.Parse(typeof(Enums.DiscriminatorTypes), processor.GetString("DiscriminatorType"), true);
			discriminator.Force = processor.GetBool("Force");
			discriminator.Formula = processor.GetString("Formula");
			discriminator.Insert = processor.GetBool("Insert");
			return discriminator;
		}

		public Property DeserialiseProperty(XmlNode propertyNode, EntityImpl parentEntity)
		{
			Property property = new PropertyImpl();
			property.Entity = parentEntity;

			NodeProcessor processor = new NodeProcessor(propertyNode);

			if (processor.Exists("IsKey"))
				property.IsKeyProperty = processor.GetBool("IsKey");

			property.Name = processor.GetString("Name");
			property.ReadOnly = processor.GetBool("ReadOnly");
			property.Type = processor.GetString("Type");

			if (processor.Exists("NHibernateType"))
				property.NHibernateType = processor.GetString("NHibernateType");

			if (processor.Exists("Validation"))
				property.ValidationOptions = DeserialiseValidationOptions(propertyNode.SelectSingleNode("Validation"));
			else property.ValidationOptions = new ValidationOptions();

			if (processor.Exists("IsHiddenByAbstractParent"))
				property.IsHiddenByAbstractParent = processor.GetBool("IsHiddenByAbstractParent");

			if (processor.Exists("IsPartOfHiddenKey"))
				property.IsPartOfHiddenKey = processor.GetBool("IsPartOfHiddenKey");

			ProcessScriptBase(property, propertyNode);

			return property;
		}

		public ValidationOptions DeserialiseValidationOptions(XmlNode validationNode)
		{
			var options = new ValidationOptions();

			var proc = new NodeProcessor(validationNode);

			options.NotEmpty = proc.GetNullable<bool>("NotEmpty");
			options.Nullable = proc.GetNullable<bool>("Nullable");
			options.MaximumValue = proc.GetNullable<int>("MaximumValue");
			options.MinimumValue = proc.GetNullable<int>("MinimumValue");
			options.FutureDate = proc.GetNullable<bool>("FutureDate");
			options.PastDate = proc.GetNullable<bool>("PastDate");
			options.FractionalDigits = proc.GetNullable<int>("FractionalDigits");
			options.IntegerDigits = proc.GetNullable<int>("IntegerDigits");
			options.MaximumLength = proc.GetNullable<long>("MaximumLength");
			options.MinimumLength = proc.GetNullable<int>("MinimumLength");
			options.RegexPattern = proc.GetString("RegexPattern", null);
			options.Validate = proc.Exists("Validate");

			return options;
		}

		private static Type TypePropertiesForThisEntity = typeof(ArchAngel.Interfaces.NHibernateEnums.PropertiesForThisEntity);

		public Reference DeserialiseReference(XmlNode refNode, EntitySet set)
		{
			Reference reference = new ReferenceImpl();

			NodeProcessor processor = new NodeProcessor(refNode);

			reference.Identifier = processor.Attributes.GetGuid("identifier");
			reference.End1Name = processor.GetString("End1Name");
			reference.End2Name = processor.GetString("End2Name");
			reference.End1Enabled = processor.GetBool("End1Enabled");
			reference.End2Enabled = processor.GetBool("End2Enabled");

			reference.Entity1 = set.GetEntity(processor.GetString("Entity1"));
			reference.Entity2 = set.GetEntity(processor.GetString("Entity2"));

			reference.Cardinality1 = DeserialiseCardinality(refNode.SelectSingleNode("Cardinality1"));
			reference.Cardinality2 = DeserialiseCardinality(refNode.SelectSingleNode("Cardinality2"));

			if (Version >= 2)
				reference.IncludeForeignKey = processor.GetBool("IncludeForeignKey");

			ProcessScriptBase(reference, refNode);

			// Fixup virtual properties of type PropertiesForThisEntity
			foreach (var uo in reference.Ex.Where(v => v.DataType == TypePropertiesForThisEntity))
			{
				object obj = null;

				if (reference.Entity1 != null)
					obj = reference.Entity1.Properties.SingleOrDefault(p => p.Name.Equals(uo.Value.ToString(), StringComparison.InvariantCultureIgnoreCase));

				if (obj == null && reference.Entity2 != null)
					obj = reference.Entity2.Properties.SingleOrDefault(p => p.Name.Equals(uo.Value.ToString(), StringComparison.InvariantCultureIgnoreCase));

				uo.Value = obj;
			}
			return reference;
		}

		public Cardinality DeserialiseCardinality(XmlNode node)
		{
			int min = node.Attributes["min"].Value.As<int>().Value;
			int max = node.Attributes["max"].Value.As<int>().Value;

			return new Cardinality(min, max);
		}

		//public Discriminator DeserialiseDiscriminator(XmlNode disNode, IDatabase database)
		//{
		//    var discriminator = new DiscriminatorImpl();

		//    discriminator.RootGrouping = new AndGrouping();

		//    var andNode = disNode.SelectSingleNode("AndGrouping");
		//    if (andNode != null)
		//    {
		//        discriminator.RootGrouping = DeserialiseGrouping(andNode, database);
		//    }

		//    return discriminator;
		//}

		//private Grouping DeserialiseGrouping(XmlNode node, IDatabase database)
		//{
		//    AndGrouping group = new AndGrouping();

		//    var conditionNodes = node.SelectNodes("Condition");
		//    if (conditionNodes != null)
		//        foreach (XmlNode condNode in conditionNodes)
		//        {
		//            group.AddCondition(DeserialiseCondition(condNode, database));
		//        }

		//    return group;
		//}

		//private Condition DeserialiseCondition(XmlNode node, IDatabase database)
		//{
		//    NodeProcessor proc = new NodeProcessor(node);

		//    var condition = new ConditionImpl();

		//    // Find column in database
		//    condition.Column = GetColumn(node, database);

		//    // Get the operator
		//    string opName = proc.GetString("Operator");
		//    condition.Operator = Operator.FromString(opName);

		//    condition.ExpressionValue = new ExpressionValueImpl(proc.GetString("ExpressionValue"));

		//    return condition;
		//}

		private IColumn GetColumn(XmlNode node, IDatabase database)
		{
			XmlNode columnNode = node.SelectSingleNode("Column");
			if (columnNode == null) throw new DeserialisationException("Could not find Column definition in Discriminator condition.");

			NodeProcessor proc = new NodeProcessor(columnNode);
			string columnName = proc.Attributes.GetString("name");
			string tableName = proc.Attributes.GetString("table");
			string schemaName = proc.Attributes.GetString("schema");

			return database.GetTable(tableName, schemaName).GetColumn(columnName);
		}

		public EntitySet DeserialiseEntitySet(XmlNode node, IDatabase database)
		{
			if (string.IsNullOrEmpty(node.InnerXml)) return new EntitySetImpl();

			var set = new EntitySetImpl();
			Version = int.Parse(node.Attributes["Version"].Value);

			Dictionary<XmlNode, Entity> entityNodes = new Dictionary<XmlNode, Entity>();

			var entityNodeList = node.SelectNodes("Entities/Entity");
			if (entityNodeList != null)
			{
				foreach (XmlNode entityNode in entityNodeList)
				{
					Entity entity = DeserialiseEntity(entityNode);
					entityNodes[entityNode] = entity;
					set.AddEntity(entity);
				}
			}

			ProcessScriptBase(set, node);

			// Second processing pass
			if (entityNodeList != null)
				foreach (XmlNode entityNode in entityNodeList)
					PostProcessEntity(set, database, entityNodes[entityNode], entityNode);

			var refNodes = node.SelectNodes("References/Reference");
			if (refNodes != null)
			{
				foreach (XmlNode referenceNode in refNodes)
				{
					Reference reference = DeserialiseReference(referenceNode, set);

					if (reference.Entity1 != null &&
						reference.Entity2 != null)
					{
						reference.Entity1.AddReference(reference);
						if (reference.Entity1 != reference.Entity2)
							reference.Entity2.AddReference(reference);

						set.AddReference(reference);
					}
				}
			}

			var componentSpecNodes = node.SelectNodes("ComponentSpecifications/ComponentSpecification");

			if (componentSpecNodes != null)
				foreach (XmlNode specNode in componentSpecNodes)
					DeserialiseComponentSpecification(specNode, set);

			if (entityNodeList != null)
			{
				foreach (XmlNode entityNode in entityNodeList)
				{
					var entity = entityNodes[entityNode];
					// Key requires Components to have been deserialised first.
					entity.Key = DeserialiseKey(entityNode.SelectSingleNode("Key"), entity);
				}
			}
			return set;
		}

		public Component DeserialiseComponent(XmlNode componentNode, ComponentSpecification spec)
		{
			var proc = new NodeProcessor(componentNode);

			string parentName = proc.Attributes.GetString("parent-type");
			var parentEntity = spec.EntitySet.GetEntity(parentName);
			if (parentEntity == null) throw new DeserialisationException(string.Format("Could not find parent type {0} of component", parentName));

			string componentName = proc.Attributes.GetString("name");

			Component component = spec.CreateImplementedComponentFor(parentEntity, componentName);
			ProcessScriptBase(component, componentNode);

			return component;
		}

		public ComponentProperty DeserialiseComponentProperty(XmlNode propertyNode, ComponentSpecification spec)
		{
			NodeProcessor proc = new NodeProcessor(propertyNode);

			ComponentProperty property = new ComponentPropertyImpl();
			property.Specification = spec;
			property.Name = proc.GetString("Name");
			property.Type = proc.GetString("Type");

			if (proc.Exists("Validation"))
				property.ValidationOptions = DeserialiseValidationOptions(propertyNode.SelectSingleNode("Validation"));
			else property.ValidationOptions = new ValidationOptions();

			ProcessScriptBase(property, propertyNode);

			return property;
		}

		public ComponentSpecification DeserialiseComponentSpecification(XmlNode specNode, EntitySet entitySet)
		{
			NodeProcessor proc = new NodeProcessor(specNode);

			string name = proc.Attributes.GetString("name");

			ComponentSpecification spec = new ComponentSpecificationImpl(name);
			entitySet.AddComponentSpecification(spec);

			var propertyNodes = specNode.SelectNodes("Property");
			if (propertyNodes != null)
			{
				foreach (XmlNode node in propertyNodes)
				{
					var property = DeserialiseComponentProperty(node, spec);
					spec.AddProperty(property);
				}
			}

			var componentNodes = specNode.SelectNodes("Component");
			if (componentNodes != null)
			{
				foreach (XmlNode node in componentNodes)
				{
					// This adds the component as well as creating it.
					DeserialiseComponent(node, spec);
				}
			}

			return spec;
		}
	}
}
