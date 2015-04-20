using System;
using System.Collections.ObjectModel;
using Slyce.Common;

namespace ArchAngel.Providers.EntityModel.Model.EntityLayer
{
	[ArchAngel.Interfaces.Attributes.ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = true, PreviewDisplayProperty = "Name")]
	public interface ComponentSpecification : IModelObject
	{
		string Name { get; set; }
		EntitySet EntitySet { get; set; }
		ReadOnlyCollection<ComponentProperty> Properties { get; }
		ReadOnlyCollection<Component> ImplementedComponents { get; }

		void AddProperty(ComponentProperty prop);
		Component CreateImplementedComponentFor(Entity entity, string nameOfComponent);
		Component GetImplementationFor(Entity entity);
		void RemoveImplementation(Component component);

		event EventHandler<CollectionChangeEvent<Component>> ImplementedComponentsChanged;
		event EventHandler<CollectionChangeEvent<ComponentProperty>> PropertiesChanged;
		void DeleteSelf();
		
		void RemovePropertyAndMarkers(ComponentProperty property);
	}
}