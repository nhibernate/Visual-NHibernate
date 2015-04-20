using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using Slyce.Common;

namespace ArchAngel.Providers.EntityModel.Model.EntityLayer
{
	[ArchAngel.Interfaces.Attributes.ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = true, PreviewDisplayProperty = "")]
	public interface EntitySet : IModelObject
	{
		ReadOnlyCollection<Entity> Entities { get; }
		ReadOnlyCollection<Reference> References { get; }
		Entity this[int i] { get; }
		int Count { get; }
		MappingSet MappingSet { get; set; }
		bool IsEmpty { get; }
		ReadOnlyCollection<ComponentSpecification> ComponentSpecifications { get; }
		ReadOnlyCollection<string> Schemas { get; }

		void AddEntity(Entity entity);
		void AddReference(Reference entity);
		void AddComponentSpecification(ComponentSpecification componentSpec);

		ComponentSpecification GetComponentSpecification(string name);
		Entity GetEntity(string name);

		IEnumerable<Entity> GetRelatedEntities(Entity entity, int show);

		void RemoveEntity(Entity entity);
		void RemoveReference(Reference reference);
		void RemoveComponentSpecification(ComponentSpecification spec);

		void DeleteEntity(Entity impl);
		void DeleteProperty(Property property);
		void DeleteReference(Reference impl);

		event CollectionChangeHandler<Entity> EntitiesChanged;
		event CollectionChangeHandler<Reference> ReferencesChanged;
		event CollectionChangeHandler<ComponentSpecification> ComponentSpecsChanged;


	}
}