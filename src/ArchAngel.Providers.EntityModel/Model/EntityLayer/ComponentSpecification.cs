using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Slyce.Common;
using Slyce.Common.EventExtensions;

namespace ArchAngel.Providers.EntityModel.Model.EntityLayer
{
	[ArchAngel.Interfaces.Attributes.ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = false)]
	[DebuggerDisplay("Name = {Name}")]
	public class ComponentSpecificationImpl : ModelObject, ComponentSpecification
	{
		private readonly List<ComponentProperty> properties = new List<ComponentProperty>();
		private readonly List<Component> implementedComponents = new List<Component>();

		public EntitySet EntitySet { get; set; }
		public ReadOnlyCollection<ComponentProperty> Properties { get { return properties.AsReadOnly(); } }
		public ReadOnlyCollection<Component> ImplementedComponents { get { return implementedComponents.AsReadOnly(); } }

		public ComponentSpecificationImpl()
		{
		}

		public ComponentSpecificationImpl(string name)
		{
			Name = name;
		}

		private string _name;
		public string Name
		{
			get { return _name; }
			set { _name = value; RaisePropertyChanged("Name"); }
		}

		public override string DisplayName
		{
			get { return "Component:" + Name; }
		}

		public void AddProperty(ComponentProperty prop)
		{
			properties.Add(prop);
			prop.Specification = this;

			foreach (var impl in implementedComponents)
			{
				impl.AddProperty(new ComponentPropertyMarker(prop));
			}

			PropertiesChanged.RaiseEvent(this, new CollectionChangeEvent<ComponentProperty>(CollectionChangeAction.Addition, prop));
		}

		public Component CreateImplementedComponentFor(Entity entity, string nameOfComponent)
		{
			Component component = new ComponentImpl(this, entity, nameOfComponent);

			foreach (var property in properties)
				component.AddProperty(new ComponentPropertyMarker(property));

			implementedComponents.Add(component);
			entity.AddComponent(component);

			ImplementedComponentsChanged.RaiseEvent(this, new CollectionChangeEvent<Component>(CollectionChangeAction.Addition, component));

			return component;
		}

		public Component GetImplementationFor(Entity entity)
		{
			if (entity == null) return null;

			return implementedComponents.Find(c => c.ParentEntity.InternalIdentifier == entity.InternalIdentifier);
		}

		public void RemoveImplementation(Component component)
		{
			implementedComponents.Remove(component);
		}

		public void DeleteSelf()
		{
			for (int i = implementedComponents.Count - 1; i >= 0; i--)
			{
				var component = implementedComponents[i];
				EntitySet.MappingSet.RemoveMapping(EntitySet.MappingSet.GetMappingFor(component));
				component.ParentEntity.RemoveComponent(component);
			}
			implementedComponents.Clear();
			EntitySet.RemoveComponentSpecification(this);
		}

		public void RemovePropertyAndMarkers(ComponentProperty property)
		{
			properties.Remove(property);

			var ms = this.GetMappingSet();

			foreach (var impl in implementedComponents)
			{
				var marker = impl.GetProperty(property.Name);
				if (ms != null)
					ms.ChangeMappingFor(marker).To(null);

				impl.RemoveProperty(property);
			}
			PropertiesChanged.RaiseEventEx(this, new CollectionChangeEvent<ComponentProperty>(CollectionChangeAction.Deletion, property));
		}

		public override string ToString() { return _name; }

		public event EventHandler<CollectionChangeEvent<Component>> ImplementedComponentsChanged;
		public event EventHandler<CollectionChangeEvent<ComponentProperty>> PropertiesChanged;
	}

	[ArchAngel.Interfaces.Attributes.ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = false)]
	public class ComponentPropertyImpl : ModelObject, ComponentProperty
	{
		private string _name;
		private string _type;
		private ValidationOptions _validationOptions = new ValidationOptions();

		public ComponentPropertyImpl()
		{
		}

		public ComponentPropertyImpl(string name)
		{
			Name = name;
		}

		public static ComponentProperty CreateFromProperty(Property property)
		{
			var newProperty = new ComponentPropertyImpl(property.Name)
			{
				ValidationOptions = new ValidationOptions(property.ValidationOptions),
				Type = property.Type
			};
			return newProperty;
		}

		public override string DisplayName
		{
			get { return "Property:" + Name; }
		}

		public ComponentSpecification Specification { get; set; }

		public string Name
		{
			get { return _name; }
			set { _name = value; RaisePropertyChanged("Name"); }
		}

		public string Type
		{
			get { return _type; }
			set { _type = value; RaisePropertyChanged("Type"); }
		}

		public ValidationOptions ValidationOptions
		{
			get { return _validationOptions; }
			set { _validationOptions = value; RaisePropertyChanged("ValidationOptions"); }
		}

		public void DeleteSelf()
		{
			Specification.RemovePropertyAndMarkers(this);

		}
	}

	public class ComponentPropertyMarker : ModelObject
	{
		public ComponentProperty RepresentedProperty { get; set; }
		public Component Component { get; set; }

		public string PropertyName { get { return RepresentedProperty.Name; } }

		/// <summary>
		/// Don't use this constructor, it is really only for testing.
		/// </summary>
		public ComponentPropertyMarker()
		{
		}

		public ComponentPropertyMarker(ComponentProperty representedProperty)
		{
			if (representedProperty == null) throw new ArgumentNullException("representedProperty");
			RepresentedProperty = representedProperty;
		}

		public override string DisplayName
		{
			get { return "Component Property:" + RepresentedProperty.Name; }
		}
	}

	[ArchAngel.Interfaces.Attributes.ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = false)]
	public class ComponentImpl : ModelObject, Component
	{
		private readonly List<ComponentPropertyMarker> properties = new List<ComponentPropertyMarker>();
		private string _name;
		private IList<string> _OldNames = new List<string>();

		public string Name
		{
			get { return _name; }
			set
			{
				if (_name != value)
				{
					if (OldNames.Count == 0 && !string.IsNullOrEmpty(value))
						OldNames.Add(value);

					_name = value;
					RaisePropertyChanged("Name");
				}
			}
		}


		public IList<string> OldNames
		{
			get { return _OldNames; }
		}

		public Entity ParentEntity { get; set; }
		public ComponentSpecification Specification { get; set; }
		public ReadOnlyCollection<ComponentPropertyMarker> Properties { get { return properties.AsReadOnly(); } }

		public ComponentImpl()
		{
		}

		public ComponentImpl(ComponentSpecification specification, Entity parentEntity, string name)
		{
			Specification = specification;
			ParentEntity = parentEntity;
			Name = name;
		}

		public override string DisplayName
		{
			get { return "Component Usage:" + Name; }
		}

		public void AddProperty(ComponentPropertyMarker property)
		{
			properties.Add(property);
			property.Component = this;
		}

		public void RemoveProperty(ComponentProperty property)
		{
			properties.RemoveAll(p => ReferenceEquals(p.RepresentedProperty, property));
		}

		public ComponentPropertyMarker GetProperty(string name)
		{
			return properties.FirstOrDefault(p => p.RepresentedProperty.Name == name);
		}

		public void DeleteSelf()
		{
			var ms = this.GetMappingSet();
			ms.RemoveMapping(ms.GetMappingFor(this));

			ParentEntity.RemoveComponent(this);
			Specification.RemoveImplementation(this);

		}
	}
}
