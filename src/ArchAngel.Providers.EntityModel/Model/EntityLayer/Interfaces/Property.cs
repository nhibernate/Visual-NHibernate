using System.Collections.Generic;

namespace ArchAngel.Providers.EntityModel.Model.EntityLayer
{
	[ArchAngel.Interfaces.Attributes.ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = true, PreviewDisplayProperty = "Name")]
	public interface Property : IModelObject
	{
		/// <summary>
		/// True if this Property is attached to an Entity
		/// </summary>
		bool HasParentEntity { get; }
		/// <summary>
		/// The parent Entity of this class. Check if HasParentEntity is true
		/// before using this property, as it can be null.
		/// </summary>
		Entity Entity { get; set; }

		/// <summary>
		/// True if the Property has been overridden by a descendant Entity.
		/// </summary>
		bool IsOverridden { get; }

		/// <summary>
		/// True if the Property overrides a Property in an ancestor Entity.
		/// </summary>
		bool IsInherited { get; }

		/// <summary>
		/// True if this Property is part of an abstract parent (Table Per Concrete Class inheritance),
		/// but we still need to access the mapped column.
		/// </summary>
		bool IsHiddenByAbstractParent { get; set; }

		/// <summary>
		/// True if this Property is part of a the key of a SubClass child entity (Table Per SubClass inheritance),
		/// but we still need to access the mapped column.
		/// </summary>
		bool IsPartOfHiddenKey { get; set; }

		[NotNullable]
		string Name { get; set; }

		[NotNullable]
		IList<string> OldNames { get; }

		[NotNullable]
		string Type { get; set; }

		[NotNullable]
		string NHibernateType { get; set; }

		[NotNullable]
		ValidationOptions ValidationOptions { get; set; }

		[NotNullable]
		bool ReadOnly { get; set; }

		[NotNullable]
		bool IsKeyProperty { get; set; }

		void SetIsKeyPropertyDirectly(bool newValue);

		/// <summary>
		/// True if the property should be marked virtual. If a property is marked as
		/// IsVirtual = false but a descendant entity overrides it, the property should be
		/// generated as "new" in C# (hiding rather than overriding).
		/// </summary>
		//[NotNullable]
		//bool IsVirtual { get; set; }

		void DeleteSelf();
	}
}