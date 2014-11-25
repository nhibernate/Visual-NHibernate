using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using ArchAngel.Interfaces.SchemaDiagrammer;
using ArchAngel.Providers.CodeProvider.DotNet;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using Slyce.Common;
using Slyce.Common.EventExtensions;

namespace ArchAngel.Providers.EntityModel.Model.EntityLayer
{
	[ArchAngel.Interfaces.Attributes.ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = false)]
	[DebuggerDisplay("Schema.Name = {Schema}.{Name}")]
	public class EntityImpl : ModelObject, Entity
	{
		public enum InheritanceType
		{
			None, TablePerClassHierarchy, TablePerSubClass, TablePerSubClassWithDiscriminator, TablePerConcreteClass, Unsupported
		}

		private string name;
		private string schema;
		private string _DiscriminatorValue;
		private EntityGenerator _Generator;
		private readonly List<Property> properties = new List<Property>();
		private readonly List<Reference> references = new List<Reference>();
		private readonly List<Entity> children = new List<Entity>();
		private readonly List<Component> components = new List<Component>();

		public event CollectionChangeHandler<Property> PropertiesChanged;
		public event CollectionChangeHandler<Component> ComponentsChanged;
		public event CollectionChangeHandler<Entity> ChildrenChanged;

		public EntityImpl()
		{
			Key = new EntityKeyImpl();
			Generator = new EntityGenerator("assigned");
			Cache = new Cache()
			{
				Include = Cache.IncludeTypes.All,
				Region = "",
				Usage = Cache.UsageTypes.None
			};
			Discriminator = new Discriminator();
		}

		public EntityImpl(string name)
			: this()
		{
			Name = name;
			Generator = new EntityGenerator("assigned");
		}

		public override string DisplayName
		{
			get { return "Entity:" + Name; }
		}

		public EntitySet EntitySet { get; set; }
		public Interfaces.IDiscriminator Discriminator { get; set; }
		public Cache Cache { get; set; }
		public bool IsAbstract { get; set; }

		public string DiscriminatorValue
		{
			get { return _DiscriminatorValue == null ? "" : _DiscriminatorValue; }
			set { _DiscriminatorValue = value; }
		}

		public Entity Parent
		{
			get;
			set;
		}

		public ReadOnlyCollection<Entity> Children
		{
			get
			{
				return children.AsReadOnly();
			}
		}

		private EntityKey _key;

		public EntityKey Key
		{
			get { return _key; }
			set
			{
				_key = value;
				if (value != null)
					value.Parent = this;
			}
		}

		public string Name
		{
			get { return name; }
			set
			{
				if (name != value)
				{
					name = value;
					RaisePropertyChanged("Name");
				}
			}
		}

		public EntityGenerator Generator
		{
			get
			{
				if (_Generator == null)
				{
					_Generator = new EntityGenerator("assigned");
				}
				return _Generator;
			}
			set
			{
				if (_Generator != value)
				{
					_Generator = value;
					RaisePropertyChanged("Generator");
				}
			}
		}

		public string Schema
		{
			get { return schema; }
			set
			{
				if (schema != value)
				{
					schema = value;
					RaisePropertyChanged("Schema");
				}
			}
		}

		public IEnumerable<Property> Properties
		{
			get
			{
				if (Parent == null)
					return properties.Where(p => !p.IsHiddenByAbstractParent && !p.IsPartOfHiddenKey);

				return properties.Where(p => !p.IsHiddenByAbstractParent && !p.IsPartOfHiddenKey).Concat(InheritedProperties);
			}
		}

		public IEnumerable<Property> PropertiesHiddenByAbstractParent
		{
			get
			{
				if (Parent == null)
					return properties.Where(p => p.IsHiddenByAbstractParent);

				return properties.Where(p => p.IsHiddenByAbstractParent).Concat(Parent.PropertiesHiddenByAbstractParent.Where(pr => properties.Count(p => p.Name == pr.Name) == 0));
			}
		}

		public IEnumerable<Property> PropertiesInHiddenKey
		{
			get
			{
				if (Parent == null)
					return properties.Where(p => p.IsPartOfHiddenKey);

				return properties.Where(p => p.IsPartOfHiddenKey);
			}
		}

		public static string GetDisplayText(InheritanceType inheritanceType)
		{
			switch (inheritanceType)
			{
				case InheritanceType.None: return "No inheritance";
				case InheritanceType.TablePerClassHierarchy: return "Table Per Class Hierarchy";
				case InheritanceType.TablePerConcreteClass: return "Table Per Concrete Class";
				case InheritanceType.TablePerSubClass: return "Table Per Sub-Class";
				case InheritanceType.Unsupported: return "Unsupported";
				default:
					throw new NotImplementedException("InheritanceType not handled yet: " + inheritanceType.ToString());
			}
		}

		public static InheritanceType DetermineInheritanceTypeWithChildren(Entity entity)
		{
			int numChildren = entity.Children.Count();
			IList<ITable> mappedTables = entity.MappedTables().ToList();

			if (numChildren > 0)
			{
				if (mappedTables.Count == 1)
				{
					if (entity.Children.All(e => e.MappedTables().Count() == 1 && e.MappedTables().First() == mappedTables[0]))
					{
						// All children mapped to the same table.
						return InheritanceType.TablePerClassHierarchy;
					}
					return InheritanceType.TablePerSubClass;
				}
				if (mappedTables.Count == 0)
				{
					// No mapping for parent class.
					return InheritanceType.TablePerConcreteClass;
				}
				return InheritanceType.Unsupported;
			}
			return InheritanceType.None;
		}

		public static InheritanceType DetermineInheritanceTypeWithParent(Entity entity)
		{
			if (entity.Parent == null)
				return InheritanceType.None;
			else
			{
				List<ITable> parentMappedTables = entity.Parent.MappedTables().ToList();
				List<ITable> childMappedTables = entity.MappedTables().ToList();

				if (childMappedTables.Count == 1 && entity.Parent.IsAbstract && parentMappedTables.Count == 0)
					return InheritanceType.TablePerConcreteClass;
				else if (childMappedTables.Count == 1 && parentMappedTables.Count == 1 &&
					childMappedTables[0] == parentMappedTables[0])
				{
					return InheritanceType.TablePerClassHierarchy;
				}
				else
					return InheritanceType.TablePerSubClass;
				//return DetermineInheritanceTypeWithChildren(entity.Parent);
			}
		}

		public static InheritanceType DetermineInheritanceType(Entity entity)
		{
			if (entity.Parent != null)
				return DetermineInheritanceTypeWithParent(entity);
			else
				return DetermineInheritanceTypeWithChildren(entity);
		}

		public ReadOnlyCollection<Property> ConcreteProperties
		{
			get
			{
				return properties.Where(p => !p.IsHiddenByAbstractParent && !p.IsPartOfHiddenKey).ToList().AsReadOnly();//.Where(p => p.Entity == this);
			}
		}

		public ReadOnlyCollection<Property> ForeignKeyPropertiesToExclude
		{
			get
			{
				HashSet<DatabaseLayer.IColumn> columnsInKeys = new HashSet<DatabaseLayer.IColumn>();
				IList<IColumn> columnsInPrimaryKey = this.PrimaryKeyColumns();

				foreach (var directedRef in DirectedReferences.Where(d => d.Reference.IncludeForeignKey == false))
				{
					DatabaseLayer.Relationship rel = directedRef.Reference.MappedRelationship();

					// TODO: Association tables?
					if (rel == null)
						continue;

					if (this.MappedTables().Contains(rel.ForeignTable))
					{
						foreach (var col in rel.ForeignKey.Columns)
						{
							if (!columnsInPrimaryKey.Contains(col))
								columnsInKeys.Add(col);
						}
					}
				}
				List<Property> props = new List<Property>();

				foreach (var prop in properties)
				{
					if (columnsInKeys.Contains(prop.MappedColumn()))
						props.Add(prop);
				}
				return props.AsReadOnly();
			}
		}

		/// <summary>
		/// Returns all properties which are overridden in a child entity somewhere down the
		/// inheritance tree.
		/// </summary>
		public IEnumerable<Property> OverriddenProperties
		{
			get { return properties.Where(p => p.IsOverridden); }
		}

		private Class _MappedClass;
		/// <summary>
		/// This is the Class that has been generated for this Entity. If it is null, either
		/// the property hasn't been filled or there is no Class on disk for this.
		/// </summary>
		public Class MappedClass
		{
			get { return _MappedClass; }
			set { _MappedClass = value; }
		}

		public IEnumerable<Property> InheritedProperties
		{
			get
			{
				if (Parent == null)
					return new List<Property>();

				// Get the paren't properties, but rather replace each property with the child's hidden property if it exists,
				// so that we get the correct MappedColumn
				List<Property> inheritedPproperties = new List<Property>();

				foreach (var parentProperty in Parent.Properties)
				{
					Property matchingChildProperty = PropertiesHiddenByAbstractParent.SingleOrDefault(p => p.Name == parentProperty.Name);

					if (matchingChildProperty != null)
						inheritedPproperties.Add(matchingChildProperty);
					else
						inheritedPproperties.Add(parentProperty);
				}
				return inheritedPproperties;
			}
		}

		public ReadOnlyCollection<Reference> References
		{
			get { return references.AsReadOnly(); }
		}

		public ReadOnlyCollection<Component> Components
		{
			get { return components.AsReadOnly(); }
		}

		/// <summary>
		/// This is a collection of all the references this Entity is part of wrapped in a DirectedReference.
		/// </summary>
		/// <remarks>
		/// Something to watch out for when using this collection is it may have DirectedReferences which are self References
		/// that go from this Entity to itself (parent/child references are a good example of this). These will only show up once,
		/// and it is up to the template author to detect and deal with them. They can be detected by checking the SelfReference
		/// property on the DirectedReference.
		/// </remarks>
		public IEnumerable<DirectedReference> DirectedReferences
		{
			get
			{
				for (int i = references.Count - 1; i >= 0; i--)
				{
					Reference reference = references[i];

					yield return new DirectedReference(this, reference);

					if (reference.SelfReference)
						yield return new DirectedReference(this, reference, true);
				}
			}
		}

		/// <summary>
		/// True if this Entity is inherited from another Entity.
		/// </summary>
		public bool HasParent { get { return Parent != null; } }

		public bool HasChildren { get { return children.Count > 0; } }

		public void AddProperty(Property property)
		{
			if (properties.Contains(property))//.Count(p => p.Equals(property)) > 0)
				return;

			properties.Add(property);
			property.Entity = this;

			if (property.IsKeyProperty)
				Key.AddProperty(property);

			property.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(property_PropertyChanged);
			RaisePropertyChanged("Properties");
			PropertiesChanged.RaiseAdditionEventEx(this, property);
		}

		void property_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			RaisePropertyChanged("Property");
		}

		public void AddReference(Reference reference)
		{
			if (references.Contains(reference))
				return;

			references.Add(reference);
			reference.EntitySet = EntitySet;
			RaisePropertyChanged("References");
		}

		public Property GetProperty(string propertyName)
		{
			Property prop = ConcreteProperties.FirstOrDefault(p => p.Name == propertyName);

			if (prop != null)
				return prop;
			else
			{
				prop = PropertiesHiddenByAbstractParent.Union(PropertiesInHiddenKey).FirstOrDefault(p => p.Name == propertyName);

				if (prop != null)
					return prop;
			}
			return InheritedProperties.FirstOrDefault(p => p.Name == propertyName);
		}

		/// <summary>
		/// Just removes the reference from this entity, not
		/// completely.
		/// </summary>
		/// <param name="reference"></param>
		public void RemoveReference(Reference reference)
		{
			references.Remove(reference);
			RaisePropertyChanged("References");
		}

		/// <summary>
		/// Removes all references from this entity. This doesn't
		/// actually change the reference objects, just removes them
		/// from this objects internal storage of them. This will
		/// leave them on the entity on the other side of the reference.
		/// </summary>
		public void ClearReferences()
		{
			references.Clear();
		}

		/// <summary>
		/// Just removes the component from this entity, not
		/// completely.
		/// </summary>
		/// <param name="component"></param>
		public void RemoveComponent(Component component)
		{
			components.Remove(component);
			component.Specification.RemoveImplementation(component);
			RaisePropertyChanged("Components");
			ComponentsChanged.RaiseDeletionEventEx(this, component);
		}

		public void AddComponent(Component component)
		{
			if (components.Contains(component))
				return;

			components.Add(component);
			RaisePropertyChanged("Components");
			ComponentsChanged.RaiseAdditionEventEx(this, component);
		}

		public bool HasConcretePropertyNamed(string propertyName)
		{
			// Any will return true as soon as any element
			// satisfies the condition.
			return properties.Any(p => p.Name == propertyName);
		}

		public bool HasPropertyNamed(string propertyName)
		{
			// Any will return true as soon as any element
			// satisfies the condition.
			return Properties.Any(p => p.Name == propertyName);
		}

		public void CopyPropertyFromParent(Property o)
		{
			Property prop = new PropertyImpl(o);
			prop.Entity = this;
			AddProperty(prop);
		}

		public bool AnyChildHasPropertyNamed(string propertyName)
		{
			foreach (var child in Children)
			{
				if (child.HasConcretePropertyNamed(propertyName))
					return true;
				if (child.AnyChildHasPropertyNamed(propertyName))
					return true;
			}
			return false;
		}

		public void AddChild(Entity entity)
		{
			if (children.Contains(entity))
				return;

			children.Add(entity);
			entity.Parent = this;
			ChildrenChanged.RaiseAdditionEventEx(this, entity);
		}

		public void RemoveChild(Entity entity)
		{
			if (!children.Contains(entity))
				return;

			children.Remove(entity);
			entity.Parent = null;

			// Add the ID property back to the child entity
			for (int i = entity.Key.Properties.ToList().Count - 1; i >= 0; i--)
			{
				if (entity.Properties.Count(p => p == entity.Key.Properties.ElementAt(i)) == 0)
					entity.AddProperty(entity.Key.Properties.ElementAt(i));
			}
			// Add any hidden properties back to the child
			foreach (var hiddenProperty in entity.PropertiesHiddenByAbstractParent)
				hiddenProperty.IsHiddenByAbstractParent = false;

			foreach (var hiddenProperty in entity.PropertiesInHiddenKey)
				hiddenProperty.IsPartOfHiddenKey = false;

			// Add references back between removed child and its parent, which was removed when creating the inheritance structure.
			if (entity.MappedTables().Count() > 0 && this.MappedTables().Count() > 0 &&
				entity.DirectedReferences.Count(d => d.ToEntity == this) == 0)
			{
				var thisTable = this.MappedTables().ElementAt(0);
				var childTable = entity.MappedTables().ElementAt(0);

				foreach (Relationship rel in thisTable.Relationships.Where(r => (r.ForeignTable == thisTable && r.PrimaryTable == childTable) || (r.ForeignTable == childTable && r.PrimaryTable == thisTable)))
					ArchAngel.Providers.EntityModel.Controller.MappingLayer.MappingProcessor.ProcessRelationshipInternal(entity.EntitySet.MappingSet, rel, new ArchAngel.Providers.EntityModel.Controller.MappingLayer.OneToOneEntityProcessor());
			}
			ChildrenChanged.RaiseDeletionEventEx(this, entity);
		}

		public void RemoveParent()
		{
			if (HasParent)
				Parent.RemoveChild(this);

			Parent = null;

			//TODO: why are we doing this again..its just been done by the parent!

			// Add the ID property back to the child entity
			for (int i = this.Key.Properties.ToList().Count - 1; i >= 0; i--)
			{
				if (this.Properties.Count(p => p == this.Key.Properties.ElementAt(i)) == 0)
					this.AddProperty(this.Key.Properties.ElementAt(i));
			}
			// Add any hidden properties back to the child
			foreach (var hiddenProperty in this.PropertiesHiddenByAbstractParent)
				hiddenProperty.IsHiddenByAbstractParent = false;

			foreach (var hiddenProperty in this.PropertiesInHiddenKey)
				hiddenProperty.IsPartOfHiddenKey = false;
		}

		public void RemoveProperty(Property property)
		{
			if (properties.Remove(property))
			{
				foreach (var mapping in this.Mappings().Where(m => m.ToProperties.Contains(property)).ToList())
					mapping.RemovePropertyAndMappedColumn(property);

				//property.Entity = null;
				RaisePropertyChanged("Properties");
				PropertiesChanged.RaiseDeletionEventEx(this, property);
			}
			else
			{
				string gfh = "";
			}
			// If the property isn't in the key this does nothing.
			Key.RemoveProperty(property);

			if (EntitySet != null)
				EntitySet.DeleteProperty(property);
		}

		string IEntity.EntityName
		{
			get { return name; }
			set { name = value; }
		}

		IEnumerable<IRelationship> IEntity.Relationships
		{
			get { return references.Cast<IRelationship>(); }
		}

		public override string ToString()
		{
			return string.Format("EntityImpl(Name: {0})", name);
		}

		public Reference CreateReferenceTo(Entity entity2)
		{
			var newRel = new ReferenceImpl();
			newRel.Entity1 = this;
			newRel.Entity2 = entity2;
			newRel.End1Name = entity2.Name;
			newRel.End2Name = Name;

			AddReference(newRel);

			if (EntitySet != null) EntitySet.AddReference(newRel);

			if (entity2 != this)
				entity2.AddReference(newRel);

			return newRel;
		}

		public void DeleteSelf()
		{
			var name = Name;

			foreach (var directedReference in DirectedReferences.ToList())
			{
				if (EntitySet != null)
					EntitySet.DeleteReference(directedReference.Reference);

				directedReference.ToEntity.RemoveReference(directedReference.Reference);
			}
			if (EntitySet != null)
				EntitySet.DeleteEntity(this);

			EntitySet = null;

			ArchAngel.Interfaces.ProjectOptions.ModelScripts.Scripts.ExistingEntityNames.Remove(name);
		}
	}
}
