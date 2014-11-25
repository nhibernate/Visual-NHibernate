using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;
using ArchAngel.Interfaces;

namespace ArchAngel.Providers.EntityModel.Model.EntityLayer
{
	/* Version History
	 * 1    Original version.
	 * 2    Added 'IncludeForeignKey' property to Reference class.
	 * 3    Added 'Generator' to Entity.
	 * 4    Added 'CustomNamespaces' to EntitySet.
	 * 5    Added 'Cache' to Entity.
	 * 6	Added 'IsAbstract' to Entity, 'IsHiddenByAbstractParent' to Property
	 * 7	Added new Discriminator
	 */
	public class EntitySetSerialisationScheme
	{
		private int Version = 7;

		public string SerialiseEntitySet(EntitySet set)
		{
			return Serialise(writer => SerialiseEntitySetInternal(set, writer));
		}

		public string SerialiseEntity(Entity entity)
		{
			return Serialise(writer => SerialiseEntityInternal(entity, writer));
		}

		public string SerialiseReference(Reference reference)
		{
			return Serialise(writer => SerialiseReferenceInternal(reference, writer));
		}

		public string SerialiseProperty(Property property)
		{
			return Serialise(writer => SerialisePropertyInternal(property, writer));
		}

		public string SerialiseComponent(Component component)
		{
			return Serialise(writer => SerialiseComponentInternal(component, writer));
		}

		public string SerialiseKey(EntityKey key)
		{
			return Serialise(writer => SerialiseKeyInternal(key, writer));
		}

		//public string SerialiseDiscriminator(Discriminator dis)
		//{
		//    return Serialise(writer => SerialiseDiscriminatorInternal(dis, writer));
		//}

		public string SerialiseComponentSpecification(ComponentSpecification spec)
		{
			return Serialise(writer => SerialiseComponentSpecificationInternal(spec, writer));
		}

		public string SerialiseComponentProperty(ComponentProperty property)
		{
			return Serialise(writer => SerialiseComponentPropertyInternal(property, writer));
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

		private void SerialiseEntitySetInternal(EntitySet set, XmlWriter writer)
		{
			if (set == null)
				set = new EntitySetImpl(); // Serialise an empty entity set.

			writer.WriteStartElement("EntitySet");
			writer.WriteAttributeString("Version", Version.ToString());

			if (set.Entities.Count > 0)
			{
				writer.WriteStartElement("Entities");

				foreach (var entity in set.Entities.OrderBy(e => e.Name))
					SerialiseEntityInternal(entity, writer);

				writer.WriteEndElement();
			}

			if (set.References.Count > 0)
			{
				writer.WriteStartElement("References");

				// Skip references that have no entity
				foreach (var reference in set.References.Where(r => r.Entity1 != null && r.Entity2 != null).OrderBy(r => r.Entity1.Name).ThenBy(r => r.Entity2.Name).ThenBy(r => r.End1Name).ThenBy(r => r.End2Name))
					SerialiseReferenceInternal(reference, writer);

				writer.WriteEndElement();
			}

			if (set.ComponentSpecifications.Count > 0)
			{
				writer.WriteStartElement("ComponentSpecifications");

				foreach (var spec in set.ComponentSpecifications.OrderBy(c => c.Name))
					SerialiseComponentSpecificationInternal(spec, writer);

				writer.WriteEndElement();
			}
			ProcessScriptBase(set, writer);
			writer.WriteEndElement();
		}

		private void SerialiseEntityInternal(Entity entity, XmlWriter writer)
		{
			writer.WriteStartElement("Entity");
			SerialiseDiscriminatorInternal(entity.Discriminator, writer);
			SerialiseKeyInternal(entity.Key, writer);
			writer.WriteElementString("Name", entity.Name);
			writer.WriteElementString("Schema", entity.Schema);
			writer.WriteElementString("DiscriminatorValue", entity.DiscriminatorValue);
			writer.WriteElementString("IsAbstract", entity.IsAbstract.ToString());
			string entityParent = entity.HasParent ? entity.Parent.Name : "";
			writer.WriteElementString("Parent", entityParent);
			SerialiseGeneratorInternal((EntityGenerator)entity.Generator, writer);
			SerialiseCacheInternal((Cache)entity.Cache, writer);
			writer.WriteStartElement("Properties");

			foreach (var prop in entity.ConcreteProperties.Concat(entity.PropertiesHiddenByAbstractParent).Concat(entity.PropertiesInHiddenKey).OrderBy(p => p.Name))
				SerialisePropertyInternal(prop, writer);

			writer.WriteEndElement();
			ProcessScriptBase(entity, writer);
			writer.WriteEndElement();
		}

		private void SerialiseGeneratorInternal(EntityGenerator generator, XmlWriter writer)
		{
			writer.WriteStartElement("Generator");
			writer.WriteElementString("ClassName", generator.ClassName);
			writer.WriteStartElement("Parameters");

			foreach (var p in generator.Parameters)
			{
				writer.WriteStartElement("Param");
				writer.WriteAttributeString("Name", p.Name);
				writer.WriteAttributeString("Value", p.Value);
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		private void SerialiseCacheInternal(Cache cache, XmlWriter writer)
		{
			writer.WriteStartElement("Cache");
			writer.WriteAttributeString("usage", cache.Usage.ToString());
			writer.WriteAttributeString("include", cache.Include.ToString());
			writer.WriteAttributeString("region", cache.Region);
			writer.WriteEndElement();
		}

		private void SerialiseReferenceInternal(Reference reference, XmlWriter writer)
		{
			if (reference.Entity1 == null)
				throw new ArgumentException("Reference object missing the Entity1 property");
			if (reference.Entity2 == null)
				throw new ArgumentException("Reference object missing the Entity2 property");

			writer.WriteStartElement("Reference");
			writer.WriteAttributeString("identifier", reference.Identifier.ToString());
			SerialiseCardinalityInternal(writer, "Cardinality1", reference.Cardinality1);
			writer.WriteElementString("End1Enabled", reference.End1Enabled.ToString());
			writer.WriteElementString("End1Name", reference.End1Name);
			writer.WriteElementString("Entity1", reference.Entity1.Name);
			writer.WriteElementString("IncludeForeignKey", reference.IncludeForeignKey.ToString()); // Added in Version 2
			SerialiseCardinalityInternal(writer, "Cardinality2", reference.Cardinality2);
			writer.WriteElementString("End2Enabled", reference.End2Enabled.ToString());
			writer.WriteElementString("End2Name", reference.End2Name);
			writer.WriteElementString("Entity2", reference.Entity2.Name);
			ProcessScriptBase(reference, writer);
			writer.WriteEndElement();
		}

		private void SerialiseCardinalityInternal(XmlWriter writer, string elementName, Cardinality cardinality)
		{
			writer.WriteStartElement(elementName);

			if (cardinality == null)
				cardinality = Cardinality.One;

			writer.WriteAttributeString("min", cardinality.Start.ToString());
			writer.WriteAttributeString("max", cardinality.End.ToString());
			writer.WriteEndElement();
		}

		private void SerialiseComponentInternal(Component component, XmlWriter writer)
		{
			writer.WriteStartElement("Component");
			writer.WriteAttributeString("parent-type", component.ParentEntity.Name);
			writer.WriteAttributeString("name", component.Name);
			ProcessScriptBase(component, writer);
			writer.WriteEndElement();
		}

		private void SerialiseComponentSpecificationInternal(ComponentSpecification spec, XmlWriter writer)
		{
			writer.WriteStartElement("ComponentSpecification");
			writer.WriteAttributeString("name", spec.Name);

			foreach (var property in spec.Properties.OrderBy(p => p.Name))
				SerialiseComponentPropertyInternal(property, writer);

			foreach (var component in spec.ImplementedComponents.OrderBy(i => i.Name))
				SerialiseComponentInternal(component, writer);

			ProcessScriptBase(spec, writer);
			writer.WriteEndElement();
		}

		private void SerialiseKeyInternal(EntityKey key, XmlWriter writer)
		{
			writer.WriteStartElement("Key");

			if (key.Properties.Any() || key.Component != null)
			{
				// Don't bother writing out the keytype if the node is empty
				writer.WriteAttributeString("keytype", key.KeyType.ToString());
			}

			if (key.Component != null)
			{
				writer.WriteStartElement("Component");
				writer.WriteAttributeString("name", key.Component.Name);
				writer.WriteEndElement();
			}

			if (key.Properties.Any())
			{
				writer.WriteStartElement("Properties");

				foreach (var prop in key.Properties.OrderBy(p => p.Name))
					writer.WriteElementString("Property", prop.Name);

				writer.WriteEndElement();
			}
			ProcessScriptBase(key, writer);
			writer.WriteEndElement();
		}

		private void SerialisePropertyInternal(Property property, XmlWriter writer)
		{
			writer.WriteStartElement("Property");
			writer.WriteElementString("IsKey", property.IsKeyProperty.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("Name", property.Name ?? "");
			writer.WriteElementString("IsHiddenByAbstractParent", property.IsHiddenByAbstractParent.ToString());
			writer.WriteElementString("IsPartOfHiddenKey", property.IsPartOfHiddenKey.ToString());
			writer.WriteElementString("ReadOnly", property.ReadOnly.ToString(CultureInfo.InvariantCulture));
			writer.WriteElementString("Type", property.Type ?? "object");
			writer.WriteElementString("NHibernateType", property.NHibernateType ?? "");
			SerialiseValidationOptions(property.ValidationOptions, writer);
			ProcessScriptBase(property, writer);
			writer.WriteEndElement();
		}

		private void SerialiseComponentPropertyInternal(ComponentProperty property, XmlWriter writer)
		{
			writer.WriteStartElement("Property");
			writer.WriteElementString("Name", property.Name ?? "");
			writer.WriteElementString("Type", property.Type ?? "object");
			SerialiseValidationOptions(property.ValidationOptions, writer);
			ProcessScriptBase(property, writer);
			writer.WriteEndElement();
		}

		private void SerialiseValidationOptions(ValidationOptions options, XmlWriter writer)
		{
			writer.WriteStartElement("Validation");
			WriteIfHasValue("NotEmpty", options.NotEmpty, writer);
			WriteIfHasValue("Nullable", options.Nullable, writer);

			if (options.Validate)
				writer.WriteElementString("Validate", "");

			WriteIfHasValue("MaximumValue", options.MaximumValue, writer);
			WriteIfHasValue("MinimumValue", options.MinimumValue, writer);
			WriteIfHasValue("MaximumLength", options.MaximumLength, writer);
			WriteIfHasValue("MinimumLength", options.MinimumLength, writer);
			WriteIfHasValue("FutureDate", options.FutureDate, writer);
			WriteIfHasValue("PastDate", options.PastDate, writer);
			WriteIfHasValue("FractionalDigits", options.FractionalDigits, writer);
			WriteIfHasValue("IntegerDigits", options.IntegerDigits, writer);
			WriteIfHasValue("RegexPattern", options.RegexPattern, writer);
			writer.WriteEndElement();
		}

		private void WriteIfHasValue(string name, string obj, XmlWriter writer)
		{
			if (string.IsNullOrEmpty(obj)) return;

			writer.WriteElementString(name, obj);
		}

		private void WriteIfHasValue<T>(string name, T? obj, XmlWriter writer) where T : struct, IConvertible
		{
			if (obj.HasValue)
				writer.WriteElementString(name, obj.Value.ToString(CultureInfo.InvariantCulture));
		}

		private void SerialiseDiscriminatorInternal(Interfaces.IDiscriminator dis, XmlWriter writer)
		{
			if (dis != null)
			{
				writer.WriteStartElement("Discriminator");
				writer.WriteElementString("AllowNull", dis.AllowNull.ToString());
				writer.WriteElementString("ColumnName", dis.ColumnName);
				writer.WriteElementString("DiscriminatorType", dis.DiscriminatorType.ToString());
				writer.WriteElementString("Force", dis.Force.ToString());
				writer.WriteElementString("Formula", dis.Formula);
				writer.WriteElementString("Insert", dis.Insert.ToString());
				writer.WriteEndElement();
			}
		}

		//private void SerialiseGrouping(XmlWriter writer, Grouping grouping)
		//{
		//    if (grouping.ContainsConditions == false && grouping.ContainsGroupings == false)
		//        return;

		//    writer.WriteStartElement("AndGrouping");

		//    foreach (var condition in grouping.Conditions)
		//    {
		//        writer.WriteStartElement("Condition");
		//        writer.WriteStartElement("Column");
		//        writer.WriteAttributeString("table", condition.Column.Parent.Name);
		//        writer.WriteAttributeString("name", condition.Column.Name);
		//        writer.WriteEndElement();
		//        writer.WriteElementString("Operator", condition.Operator.Name);
		//        writer.WriteElementString("ExpressionValue", condition.ExpressionValue.Value);
		//        writer.WriteEndElement();
		//    }
		//    foreach (var gr in grouping.Groupings)
		//        SerialiseGrouping(writer, gr);

		//    writer.WriteEndElement();
		//}

		public void ProcessScriptBase(IScriptBaseObject scriptBase, XmlWriter writer)
		{
			if (scriptBase.Ex != null && scriptBase.Ex.Count > 0)
			{
				var serialiser = new VirtualPropertySerialiser();
				serialiser.SerialiseVirtualProperties(scriptBase.Ex, writer);
			}
		}

		private static string GetNullableString<T>(T? property) where T : struct
		{
			return property.HasValue ? property.Value.ToString() : "";
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
	}
}
