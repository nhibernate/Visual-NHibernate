using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Slyce.Common;
using Slyce.Common.EventExtensions;
using Slyce.Common.IEnumerableExtensions;

namespace ArchAngel.Providers.EntityModel.Model.EntityLayer
{
	[ArchAngel.Interfaces.Attributes.ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = false)]
	[DebuggerDisplay("Parent = {Parent}, KeyType = {KeyType}.ToString()")]
	public class EntityKeyImpl : ModelObject, EntityKey
	{
		private readonly HashSet<Property> properties = new HashSet<Property>();
		private Component _component;
		private EntityKeyType _keyType = EntityKeyType.Empty;

		public EntityKeyImpl()
		{
		}

		public Entity Parent { get; set; }

		public Component Component
		{
			get { return _component; }
			set
			{
				_component = value;
				if (value != null)
					KeyType = EntityKeyType.Component;
				else if (properties.Any())
					KeyType = EntityKeyType.Properties;
				else
					KeyType = EntityKeyType.Empty;

				RaisePropertyChanged("Component");
			}
		}

		public IEnumerable<Property> Properties { get { return properties; } }

		public override string DisplayName
		{
			get { return "Entity Key:" + Parent.Name; }
		}

		/// <summary>
		/// Adds a property, and sets the KeyType to Properties
		/// </summary>
		/// <param name="propertyName">The name of the Property to add</param>
		public void AddProperty(string propertyName)
		{
			var newProperty = Parent.GetProperty(propertyName);

			if (newProperty == null)
				return;

			properties.Add(newProperty);
			newProperty.SetIsKeyPropertyDirectly(true);

			Component = null;
			RaisePropertyChanged("Properties");
			KeyType = EntityKeyType.Properties;
		}

		/// <summary>
		/// Adds a property, and sets the KeyType to Properties
		/// </summary>
		public void AddProperty(Property prop)
		{
			properties.Add(prop);
			prop.SetIsKeyPropertyDirectly(true);
			Component = null;
			RaisePropertyChanged("Properties");
			PropertiesChanged.RaiseAdditionEvent(this, prop);
			KeyType = EntityKeyType.Properties;
		}

		public void RemoveProperty(Property property)
		{
			bool removed = properties.Remove(property);
			if (removed)
			{
				property.SetIsKeyPropertyDirectly(false);
				RaisePropertyChanged("Properties");
				PropertiesChanged.RaiseDeletionEvent(this, property);
			}
			if (properties.Count == 0)
			{
				KeyType = EntityKeyType.Empty;
			}
		}

		/// <summary>
		/// Adds the list of properties, and sets the KeyType to Properties
		/// </summary>
		public void AddProperties(IEnumerable<Property> newProperties)
		{
			foreach (var property in newProperties)
			{
				properties.Add(property);
				property.SetIsKeyPropertyDirectly(true);
				PropertiesChanged.RaiseAdditionEvent(this, property);
			}
			Component = null;
			RaisePropertyChanged("Properties");
			KeyType = EntityKeyType.Properties;
		}

		public EntityKeyType KeyType
		{
			get { return _keyType; }
			set
			{
				if (_keyType == value) return;

				if (_keyType == EntityKeyType.Properties)
				{
					// KeyType has changed and was previously Properties, so we need
					// to set all of the properties to IsKeyProperty = false;
					properties.ForEach(p => p.SetIsKeyPropertyDirectly(false));
				}
				else if (value == EntityKeyType.Properties)
				{
					// KeyType has changed to Properties, so we need
					// to set all of the properties to IsKeyProperty = true;
					properties.ForEach(p => p.SetIsKeyPropertyDirectly(true));
				}

				_keyType = value;
				RaisePropertyChanged("KeyType");
			}
		}

		public event CollectionChangeHandler<Property> PropertiesChanged;
	}
}