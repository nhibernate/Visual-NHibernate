using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;

namespace ArchAngel.Providers.EntityModel.Model.MappingLayer
{
	public class MappingChanger
	{
		private readonly MappingSetImpl ms;
		private readonly Property property;

		public MappingChanger(MappingSetImpl ms, Property property)
		{
			this.ms = ms;
			this.property = property;
		}

		public void To(IColumn column)
		{
			// Remove old mapped column
			IColumn oldColumn = property.MappedColumn();
			if (oldColumn != null)
				ms.RemoveMappingBetween(property, oldColumn);

			if (column == null)
			{
				ms.InvalidateCache();
				return;
			}

			Mapping mapping = ms.GetMappingBetween(property.Entity, column.Parent);

			mapping.SetMapping(property, column);
			ms.InvalidateCache();
		}
	}

	public class ReferenceMappingChanger
	{
		private readonly MappingSet ms;
		private readonly Reference reference;

		public ReferenceMappingChanger(MappingSet ms, Reference reference)
		{
			this.ms = ms;
			this.reference = reference;
		}

		public void To(ITable table)
		{
			RemoveOldMapping();

			if (table == null) return;

			TableReferenceMapping mapping = new TableReferenceMappingImpl() { FromTable = table, ToReference = reference };
			ms.AddMapping(mapping);
			ms.InvalidateCache();
		}

		public void To(Relationship relationship)
		{
			RemoveOldMapping();

			if (relationship == null) return;

			RelationshipReferenceMapping mapping = new RelationshipReferenceMappingImpl() { FromRelationship = relationship, ToReference = reference };
			ms.AddMapping(mapping);
			ms.InvalidateCache();
		}

		private void RemoveOldMapping()
		{
			ITable oldTable = reference.MappedTable();

			if (oldTable != null)
				ms.RemoveMappingBetween(reference, oldTable);

			Relationship oldRel = reference.MappedRelationship();

			if (oldRel != null)
				ms.RemoveMappingBetween(reference, oldRel);

			ms.InvalidateCache();
		}
	}

	public class ComponentPropertyMappingChanger
	{
		private readonly MappingSet ms;
		private readonly ComponentPropertyMarker property;

		public ComponentPropertyMappingChanger(MappingSet ms, ComponentPropertyMarker property)
		{
			this.ms = ms;
			this.property = property;
		}

		public void To(IColumn column)
		{
			RemoveOldMapping();

			if (column == null)
			{
				ms.InvalidateCache();
				return;
			}

			var mapping = ms.GetMappingFor(property.Component);

			if (mapping == null)
			{
				mapping = new ComponentMappingImpl();
				mapping.ToComponent = property.Component;
				mapping.FromTable = column.Parent;
				mapping.AddPropertyAndColumn(property, column);

				ms.AddMapping(mapping);
			}
			else
			{
				mapping.SetMapping(property, column);
				ms.InvalidateCache();
			}
		}

		private void RemoveOldMapping()
		{
			IColumn oldColumn = property.MappedColumn();
			if (oldColumn != null)
				ms.RemoveMappingBetween(property, oldColumn);
		}
	}
}