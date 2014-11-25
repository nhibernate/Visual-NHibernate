using System;
using System.Collections.Generic;
using System.Linq;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using System.Collections.ObjectModel;

namespace ArchAngel.Providers.EntityModel.Model
{
	public static class EntityExtensions
	{
		public static IColumn MappedColumn(this Property property)
		{
			MappingSet ms = property.GetMappingSet();
			if (ms == null) return null;

			return property.GetMappingSet().GetMappedColumnFor(property);
		}

		public static void SetMappedColumn(this Property property, IColumn column)
		{
			if (property == null) throw new ArgumentNullException("property");
			var set = property.GetMappingSet();
			if (set == null) throw new Exception(string.Format("Could not set Mapped Column for Property {0}: Could not get its MappingSet", property.Name));
			set.ChangeMappedColumnFor(property).To(column);
		}

		public static void SetMappedTable(this Reference reference, ITable table)
		{
			if (reference == null) throw new ArgumentNullException("reference");
			var set = reference.GetMappingSet();
			if (set == null) throw new Exception(string.Format("Could not set Mapped Table for Reference {0}: Could not get its MappingSet", reference.Name));

			reference.GetMappingSet().ChangeMappingFor(reference).To(table);
		}

		public static void SetMappedRelationship(this Reference reference, Relationship relationship)
		{
			if (reference == null) throw new ArgumentNullException("reference");
			var set = reference.GetMappingSet();
			if (set == null) throw new Exception(string.Format("Could not set Mapped Relationship for Reference {0}: Could not get its MappingSet", reference.Name));

			set.ChangeMappingFor(reference).To(relationship);
		}

		public static IEnumerable<ITable> MappedTables(this Entity entity)
		{
			MappingSet ms = entity.GetMappingSet();
			if (ms == null) return new List<ITable>();

			return ms.GetMappedTablesFor(entity);
		}

		public static IEnumerable<Mapping> Mappings(this Entity entity)
		{
			MappingSet ms = entity.GetMappingSet();
			if (ms == null) return new List<Mapping>();

			return ms.GetMappingsContaining(entity);
		}

		public static IList<IColumn> PrimaryKeyColumns(this Entity entity)
		{
			List<IColumn> columns = new List<IColumn>();

			foreach (var table in entity.MappedTables())
				foreach (var key in table.Keys.Where(k => k.Keytype == ArchAngel.Providers.EntityModel.Helper.DatabaseKeyType.Primary))
					columns.AddRange(key.Columns);

			return columns;
		}
	}

	public static class ComponentExtensions
	{
		public static ITable MappedTable(this Component component)
		{
			MappingSet ms = component.GetMappingSet();
			if (ms == null) return null;

			return ms.GetMappedTableFor(component);
		}

		public static IColumn MappedColumn(this ComponentPropertyMarker property)
		{
			MappingSet ms = property.GetMappingSet();
			if (ms == null) return null;

			return ms.GetMappedColumnFor(property);
		}

		public static void SetMappedColumn(this ComponentPropertyMarker property, IColumn column)
		{
			if (property == null) throw new ArgumentNullException("property");
			var set = property.GetMappingSet();
			if (set == null) throw new Exception(string.Format("Could not set Mapped Column for Property {0}: Could not get its MappingSet", property.PropertyName));
			set.ChangeMappingFor(property).To(column);
		}

		public static ComponentMapping Mapping(this Component component)
		{
			MappingSet ms = component.GetMappingSet();
			if (ms == null) return null;

			return ms.GetMappingFor(component);
		}
	}

	public static class ReferenceExtensions
	{
		public static ITable MappedTable(this Reference reference)
		{
			MappingSet ms = reference.GetMappingSet();
			if (ms == null) return null;

			return ms.GetMappedTableFor(reference);
		}

		public static Relationship MappedRelationship(this Reference reference)
		{
			MappingSet ms = reference.GetMappingSet();
			if (ms == null) return null;

			return ms.GetMappedRelationshipFor(reference);
		}

		/// <summary>
		/// Gets a list of all reference names 'from' this entity.
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public static IList<string> GetReferenceNames(this Entity entity, Entity entity2)
		{
			List<string> names = new List<string>();

			foreach (var reference in entity.DirectedReferences)
			{
				if (reference.FromEntity == entity &&
					reference.ToEntity == entity2)
					names.Add(reference.ToName);
				else if (reference.FromEntity == entity2 &&
					reference.ToEntity == entity)
					names.Add(reference.FromName);
			}
			return names;
		}

		public static IEnumerable<Relationship> PossibleMappedRelationships(this Reference reference)
		{
			var list = new List<Relationship>();
			MappingSet ms = reference.GetMappingSet();
			if (ms == null) return list;

			IList<ITable> entity1MappedTables = reference.Entity1.MappedTables().ToList();
			IList<ITable> entity2MappedTables = reference.Entity2.MappedTables().ToList();

			var tables = reference.Entity1.MappedTables()
				.Concat(reference.Entity2.MappedTables());

			if (reference.Entity1 != null)
			{
				Entity parent = reference.Entity1.Parent;
				while (parent != null)
				{
					tables = tables.Concat(parent.MappedTables());
					parent = parent.Parent;
				}
			}

			if (reference.Entity2 != null)
			{
				Entity parent = reference.Entity2.Parent;
				while (parent != null)
				{
					tables = tables.Concat(parent.MappedTables());
					parent = parent.Parent;
				}
			}
			foreach (var table in tables)
			{
				foreach (var relationship in table.Relationships)
				{
					if ((entity1MappedTables.Contains(relationship.PrimaryTable) && entity2MappedTables.Contains(relationship.ForeignTable)) ||
						(entity1MappedTables.Contains(relationship.ForeignTable) && entity2MappedTables.Contains(relationship.PrimaryTable)))
					{
						if (!list.Contains(relationship))
							list.Add(relationship);
					}
				}
			}

			return list;
		}

		/// <summary>
		/// Gets a list of possible 'association tables' for the reference ie: to 
		/// implement many-to-many relationships between two tables.
		/// </summary>
		/// <param name="reference"></param>
		/// <returns></returns>
		public static ReadOnlyCollection<ITable> PossibleMappedTables(this Reference reference)
		{
			MappingSet ms = reference.GetMappingSet();
			if (ms == null) return new List<ITable>().AsReadOnly();

			List<ITable> possibles = new List<ITable>();
			List<ITable> definites = new List<ITable>();

			foreach (ITable mappedTable in reference.Entity1.MappedTables())
			{
				foreach (Relationship relationship in mappedTable.Relationships)
				{
					if (relationship.PrimaryTable == mappedTable &&
						ArchAngel.Interfaces.Cardinality.One.Equals(relationship.ForeignCardinality))// &&
					//!ArchAngel.Interfaces.Cardinality.One.Equals(relationship.PrimaryCardinality))
					{
						possibles.Add(relationship.ForeignTable);
					}
					else if (relationship.ForeignTable == mappedTable &&
						ArchAngel.Interfaces.Cardinality.One.Equals(relationship.PrimaryCardinality))// &&
					//!ArchAngel.Interfaces.Cardinality.One.Equals(relationship.ForeignCardinality))
					{
						possibles.Add(relationship.PrimaryTable);
					}
				}
			}
			foreach (ITable mappedTable in reference.Entity2.MappedTables())
			{
				foreach (Relationship relationship in mappedTable.Relationships)
				{
					if (relationship.PrimaryTable == mappedTable &&
						possibles.Contains(relationship.ForeignTable) &&
						ArchAngel.Interfaces.Cardinality.One.Equals(relationship.ForeignCardinality))
					{
						if (possibles.Contains(relationship.ForeignTable))
							definites.Add(relationship.ForeignTable);
					}
					else if (relationship.ForeignTable == mappedTable &&
						possibles.Contains(relationship.PrimaryTable) &&
						ArchAngel.Interfaces.Cardinality.One.Equals(relationship.PrimaryCardinality))
					{
						if (possibles.Contains(relationship.PrimaryTable))
							definites.Add(relationship.PrimaryTable);
					}
				}
			}
			return definites.AsReadOnly();
		}
	}
}