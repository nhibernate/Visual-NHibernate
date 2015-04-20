using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArchAngel.Interfaces.SchemaDiagrammer;
using ArchAngel.Providers.CodeProvider;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using Entity = ArchAngel.Providers.EntityModel.Model.EntityLayer.Entity;
using EntitySet = ArchAngel.Providers.EntityModel.Model.EntityLayer.EntitySet;

namespace ArchAngel.Providers.EntityModel.Model.MappingLayer
{
	[ArchAngel.Interfaces.Attributes.ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = true, PreviewDisplayName = "MappingSet")]
	public interface MappingSet : IModelObject
	{
		EventAggregator EventAggregator { get; }

		ReadOnlyCollection<Mapping> Mappings { get; }
		ReadOnlyCollection<TableReferenceMapping> ReferenceMappings { get; }
		ReadOnlyCollection<RelationshipReferenceMapping> RelationshipMappings { get; }
		ReadOnlyCollection<ComponentMapping> ComponentMappings { get; }

		IDatabase Database { get; set; }
		EntitySet EntitySet { get; set; }
		ParseResults CodeParseResults { get; set; }

		void AddMapping(Mapping mapping);
		void AddMapping(TableReferenceMapping mapping);
		void AddMapping(RelationshipReferenceMapping mapping);
		void AddMapping(ComponentMapping mapping);

		void RemoveMapping(Mapping mapping);
		void RemoveMapping(TableReferenceMapping mapping);
		void RemoveMapping(RelationshipReferenceMapping mapping);
		void RemoveMapping(ComponentMapping mapping);

		IEnumerable<ITable> GetMappedTablesFor(Entity e);
		IEnumerable<Mapping> GetMappingsContaining(Entity e);
		IEnumerable<Mapping> GetMappingsContaining(ITable e);

		IColumn GetMappedColumnFor(Property property);
		IColumn GetMappedColumnFor(ComponentPropertyMarker property);
		IEnumerable<Entity> GetMappedEntitiesFor(ITable table);
		IEnumerable<Property> GetMappedPropertiesFor(IColumn column);
		ITable GetMappedTableFor(Reference reference);
		ITable GetMappedTableFor(Component component);
		Relationship GetMappedRelationshipFor(Reference reference);
		IEnumerable<Reference> GetMappedReferencesFor(ITable table);
		IEnumerable<Reference> GetMappedReferencesFor(Relationship relationship);
		IEnumerable<Component> GetMappedComponentsFor(ITable table);
		IEnumerable<ITable> GetAssociationTablesFor(IDatabase database);

		IEnumerable<Entity> GetEntitiesFromEntitySet();
		IEnumerable<ITable> GetEntitiesFromDatabase();

		ITable GetNullEntityObject();

		MappingChanger ChangeMappedColumnFor(Property property);
		ReferenceMappingChanger ChangeMappingFor(Reference reference);
		ComponentPropertyMappingChanger ChangeMappingFor(ComponentPropertyMarker component);

		Mapping GetMappingBetween(Entity entity, ITable table);
		ComponentMapping GetMappingFor(Component component);

		List<string> TablePrefixes { get; set; }
		List<string> ColumnPrefixes { get; set; }
		List<string> TableSuffixes { get; set; }
		List<string> ColumnSuffixes { get; set; }

		void RemoveMappingBetween(ComponentPropertyMarker property, IColumn column);
		void RemoveMappingBetween(Property property, IColumn column);
		void RemoveMappingBetween(Reference reference, ITable table);
		void RemoveMappingBetween(Reference reference, Relationship relationship);
		void InvalidateCache();
		void DeleteTable(ITable table);
		void DeleteView(ITable table);
		void DeleteEntity(Entity entity);
		void DeleteReference(Reference reference);
		void DeleteRelationship(Relationship relationship1);
		void RemoveMappingsContaining(Property property);
		void RemoveMappingsContaining(Relationship relationship);
		void RemoveMappingsContaining(IColumn column);
	}

	[ArchAngel.Interfaces.Attributes.ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = true, PreviewDisplayName = "ComponentMapping")]
	public interface ComponentMapping : IModelObject
	{
		Component ToComponent { get; set; }
		ITable FromTable { get; set; }
		MappingSet MappingSet { get; set; }
		ReadOnlyCollection<ComponentPropertyMarker> ToProperties { get; }
		ReadOnlyCollection<IColumn> FromColumns { get; }
		IEnumerable<ColumnComponentPropertyMapping> Mappings { get; }
		void AddPropertyAndColumn(ComponentPropertyMarker prop, IColumn col);
		void SetMappings(IEnumerable<ColumnComponentPropertyMapping> mappings);
		void Delete();
		void SetMapping(ComponentPropertyMarker property, IColumn column);
		void RemovePropertyAndMappedColumn(ComponentPropertyMarker property);
	}

	[ArchAngel.Interfaces.Attributes.ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = true, PreviewDisplayName = "ReferenceMapping")]
	public interface TableReferenceMapping : IModelObject
	{
		ITable FromTable { get; set; }
		Reference ToReference { get; set; }
		MappingSet MappingSet { get; set; }
		void Delete();
	}

	[ArchAngel.Interfaces.Attributes.ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = true, PreviewDisplayName = "RelationshipReferenceSet")]
	public interface RelationshipReferenceMapping : IModelObject
	{
		Relationship FromRelationship { get; set; }
		Reference ToReference { get; set; }
		MappingSet MappingSet { get; set; }
		void Delete();
	}

	[ArchAngel.Interfaces.Attributes.ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = true, PreviewDisplayName = "Name")]
	public interface Mapping : IModelObject, IRelationship
	{
		Entity ToEntity { get; set; }
		ReadOnlyCollection<Property> ToProperties { get; }
		ITable FromTable { get; set; }
		ReadOnlyCollection<IColumn> FromColumns { get; }
		IEnumerable<ColumnPropertyMapping> Mappings { get; }
		MappingSet MappingSet { get; set; }

		void AddPropertyAndColumn(Property prop, IColumn col);
		void SetMappings(IEnumerable<ColumnPropertyMapping> mappings);
		void Delete();
		void SetMapping(Property property, IColumn column);
		void RemovePropertyAndMappedColumn(Property property);
		void RemoveColumnAndMappedProperties(IColumn column);
	}

	public class ColumnPropertyMapping
	{
		public readonly Property Property;
		public readonly IColumn Column;

		public ColumnPropertyMapping(Property property, IColumn column)
		{
			Property = property;
			Column = column;
		}
	}

	public interface ColumnReference
	{
		ITable FromTable { get; set; }
		IColumn FromColumn { get; set; }
	}
}