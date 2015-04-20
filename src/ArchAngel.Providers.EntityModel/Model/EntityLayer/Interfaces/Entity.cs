using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArchAngel.Interfaces.SchemaDiagrammer;
using ArchAngel.Providers.CodeProvider.DotNet;
using Slyce.Common;

namespace ArchAngel.Providers.EntityModel.Model.EntityLayer
{
	[ArchAngel.Interfaces.Attributes.ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = true, PreviewDisplayProperty = "Name")]
	public interface Entity : IEntity, IPropertyContainer
	{
		string Name { get; set; }

		string Schema { get; set; }

		EntityGenerator Generator { get; set; }

		Cache Cache { get; set; }

		bool IsAbstract { get; set; }

		/// <summary>
		/// All Properties on this object. Includes all inherited and concrete properties.
		/// Check whether property.Entity is equal to this Entity on each property to determine if it is concrete or
		/// inherited from the parent Entity. It could also be an overridden property, in which case it will show up
		/// twice in this list -> once as a Property on the parent Entity, and once as a property on this Entity but with
		/// the IsOverridden property set to true.
		/// </summary>
		IEnumerable<Property> Properties { get; }

		/// <summary>
		/// Gets all hidden properties - those Properties that are part of an abstract parent (Table Per Concrete Class inheritance),
		/// but we still need to access the mapped column.
		/// </summary>
		IEnumerable<Property> PropertiesHiddenByAbstractParent { get; }

		/// <summary>
		/// Gets all hidden properties - those Properties that are hidden by a SubClass' parent (Table Per SubClass inheritance),
		/// but we still need to access the mapped column.
		/// </summary>
		IEnumerable<Property> PropertiesInHiddenKey { get; }

		/// <summary>
		/// Properties that exist just on Entity. Does not included Inherited Properties but does include Overridden properties.
		/// </summary>
		ReadOnlyCollection<Property> ConcreteProperties { get; }
		/// <summary>
		/// Properties that should be excluded because they are part of a foreign key whose relationship has been marked as FK columns to be excluded.
		/// </summary>
		ReadOnlyCollection<Property> ForeignKeyPropertiesToExclude { get; }
		/// <summary>
		/// The properties that have been inherited from this Entity's parent.
		/// </summary>
		IEnumerable<Property> InheritedProperties { get; }
		/// <summary>
		/// Returns all properties which are overridden in a child entity somewhere down the
		/// inheritance tree.
		/// </summary>
		IEnumerable<Property> OverriddenProperties { get; }

		/// <summary>
		/// This is the Class that has been generated for this Entity. If it is null, either
		/// the property hasn't been filled or there is no Class on disk for this.
		/// </summary>
		Class MappedClass { get; set; }

		EntityKey Key { get; set; }

		ReadOnlyCollection<Reference> References { get; }

		/// <summary>
		/// This is a collection of all the references this Entity is part of wrapped in a DirectedReference.
		/// </summary>
		/// <remarks>
		/// Something to watch out for when using this collection is it may have DirectedReferences which are self References
		/// that go from this Entity to itself (parent/child references are a good example of this). These will show up twice,
		/// and it is up to the template author to detect and deal with them. They can be detected by checking the SelfReference
		/// property on the DirectedReference. The first time Entity1 will be set to the From end, the second time Entity2 will be.
		/// </remarks>
		IEnumerable<DirectedReference> DirectedReferences { get; }
		EntitySet EntitySet { get; set; }
		Interfaces.IDiscriminator Discriminator { get; set; }

		string DiscriminatorValue { get; set; }

		/// <summary>
		/// True if this Entity is inherited from another Entity.
		/// </summary>
		bool HasParent { get; }

		/// <summary>
		/// True if this Entity has any child Entities.
		/// </summary>
		bool HasChildren { get; }
		/// <summary>
		/// The parent Entity of this class. Check if HasParentEntity is true
		/// before using this property.
		/// </summary>
		Entity Parent { get; set; }
		ReadOnlyCollection<Entity> Children { get; }
		ReadOnlyCollection<Component> Components { get; }

		void AddReference(Reference reference);
		void RemoveReference(Reference reference);
		void ClearReferences();

		void AddComponent(Component component);
		void RemoveComponent(Component component);

		/// <summary>
		/// Adds entity to this Entity's list of child Entities, and
		/// sets its Entity property to this.
		/// </summary>
		/// <param name="entity">The Entity to add as a child.</param>
		void AddChild(Entity entity);
		/// <summary>
		/// Removes entity from this Entity's list of child Entities, and
		/// sets its Entity property to null.
		/// </summary>
		/// <param name="entity">The Entity to remove from the child Entity list.</param>
		void RemoveChild(Entity entity);
		/// <summary>
		/// Removes itself from the parent (if it has one), and sets the parent to null.
		/// </summary>
		void RemoveParent();

		Reference CreateReferenceTo(Entity entity2);
		void DeleteSelf();

		// Events
		event CollectionChangeHandler<Property> PropertiesChanged;
		event CollectionChangeHandler<Component> ComponentsChanged;
		event CollectionChangeHandler<Entity> ChildrenChanged;

		/// <summary>
		/// Creates an overriding Property by copying the given property.
		/// </summary>
		/// <param name="o"></param>
		void CopyPropertyFromParent(Property o);

		/// <summary>
		/// Searches down the inheritance tree for a property, returns true if it finds it.
		/// </summary>
		/// <param name="name">The name of the property to search for.</param>
		/// <returns>True if the property is found on any descendant entities.</returns>
		bool AnyChildHasPropertyNamed(string name);

		/// <summary>
		/// Searches up the inheritance tree for a property, returns true if it finds it.
		/// </summary>
		/// <param name="name">The name of the property to search for.</param>
		/// <returns>True if the property is found on any ancestor entities.</returns>
		bool HasPropertyNamed(string name);

		/// <summary>
		/// Searches this Entity for a property, returns true if it finds it.
		/// </summary>
		/// <param name="propertyName">The name of the property to search for.</param>
		/// <returns>True if the property is found on this Entity.</returns>
		bool HasConcretePropertyNamed(string propertyName);
	}
}