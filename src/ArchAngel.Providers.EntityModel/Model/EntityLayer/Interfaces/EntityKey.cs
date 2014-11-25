using System.Collections.Generic;
using Slyce.Common;

namespace ArchAngel.Providers.EntityModel.Model.EntityLayer
{
	[ArchAngel.Interfaces.Attributes.ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = true, PreviewDisplayProperty = "")]
	public interface EntityKey : IModelObject
	{
		Component Component { get; set; }
		IEnumerable<Property> Properties { get; }
		Entity Parent { get; set; }
		void AddProperty(string propertyName);
		void AddProperty(Property prop);
		void RemoveProperty(Property property);
		void AddProperties(IEnumerable<Property> properties);

		EntityKeyType KeyType { get; set; }

		event CollectionChangeHandler<Property> PropertiesChanged;
	}

	public enum EntityKeyType
	{
		Component, Properties, Empty
	}
}