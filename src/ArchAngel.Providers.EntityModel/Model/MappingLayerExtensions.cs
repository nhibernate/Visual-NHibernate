using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;

namespace ArchAngel.Providers.EntityModel.Model
{
	public static class MappingSetExtensions
	{
		/// <summary>
		/// Gets the MappingSet object that the Column belongs to, or null if it doesn't belong to one.
		/// </summary>
		/// <param name="column"></param>
		/// <returns></returns>
		public static MappingSet GetMappingSet(this IColumn column)
		{
			if (column == null || column.Parent == null || column.Parent.Database == null) return null;

			return column.Parent.Database.MappingSet;
		}

		/// <summary>
		/// Gets the MappingSet object that the Reference belongs to, or null if it doesn't belong to one.
		/// </summary>
		/// <param name="reference"></param>
		/// <returns></returns>
		public static MappingSet GetMappingSet(this Reference reference)
		{
			if (reference == null || reference.EntitySet == null) return null;

			return reference.EntitySet.MappingSet;
		}

		/// <summary>
		/// Gets the MappingSet object that the Property belongs to, or null if it doesn't belong to one.
		/// </summary>
		/// <param name="property"></param>
		/// <returns></returns>
		public static MappingSet GetMappingSet(this Property property)
		{
			if (property == null || property.Entity == null || property.Entity.EntitySet == null) return null;

			return property.Entity.EntitySet.MappingSet;
		}

		/// <summary>
		/// Gets the MappingSet object that the EntityKey belongs to, or null if it doesn't belong to one.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static MappingSet GetMappingSet(this EntityKey key)
		{
			if (key == null || key.Parent == null || key.Parent.EntitySet == null) return null;

			return key.Parent.EntitySet.MappingSet;
		}

		/// <summary>
		/// Gets the MappingSet object that the Entity belongs to, or null if it doesn't belong to one.
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public static MappingSet GetMappingSet(this Entity entity)
		{
			if (entity == null || entity.EntitySet == null) return null;

			return entity.EntitySet.MappingSet;
		}

		/// <summary>
		/// Gets the MappingSet object that the ComponentSpecification belongs to, or null if it doesn't belong to one.
		/// </summary>
		/// <param name="componentSpec"></param>
		/// <returns></returns>
		public static MappingSet GetMappingSet(this ComponentSpecification componentSpec)
		{
			if (componentSpec == null || componentSpec.EntitySet == null) return null;

			return componentSpec.EntitySet.MappingSet;
		}

		/// <summary>
		/// Gets the MappingSet object that the Component belongs to, or null if it doesn't belong to one.
		/// </summary>
		/// <param name="component"></param>
		/// <returns></returns>
		public static MappingSet GetMappingSet(this Component component)
		{
			if (component == null || component.Specification == null || component.Specification.EntitySet == null) return null;

			return component.Specification.EntitySet.MappingSet;
		}

		/// <summary>
		/// Gets the MappingSet object that the Component belongs to, or null if it doesn't belong to one.
		/// </summary>
		/// <param name="marker"></param>
		/// <returns></returns>
		public static MappingSet GetMappingSet(this ComponentPropertyMarker marker)
		{
			if (marker == null || marker.Component == null || marker.Component.Specification == null || marker.Component.Specification.EntitySet == null) return null;

			return marker.Component.Specification.EntitySet.MappingSet;
		}

		/// <summary>
		/// Gets the MappingSet object that the ITable belongs to, or null if it doesn't belong to one.
		/// </summary>
		/// <param name="entityObject"></param>
		/// <returns></returns>
		public static MappingSet GetMappingSet(this ITable entityObject)
		{
			if (entityObject == null || entityObject.Database == null) return null;

			return entityObject.Database.MappingSet;
		}

		/// <summary>
		/// Gets the MappingSet object that the Relationship belongs to, or null if it doesn't belong to one.
		/// </summary>
		/// <param name="relationship"></param>
		/// <returns></returns>
		public static MappingSet GetMappingSet(this Relationship relationship)
		{
			if (relationship == null || relationship.Database == null) return null;

			return relationship.Database.MappingSet;
		}
	}
}
