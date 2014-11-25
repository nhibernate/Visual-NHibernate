using System.Collections.Generic;
using System.Linq;
using ArchAngel.Providers.CodeProvider.CodeInsertions;
using ArchAngel.Providers.CodeProvider.DotNet;
using Slyce.Common.IEnumerableExtensions;

namespace ArchAngel.NHibernateHelper.CodeInsertions
{
	public class PropertyConstraintBuilder
	{
		private readonly Class _class;
		private readonly string _name;
		private readonly IList<string> _oldNames = new List<string>();
		private string _typeName;
		private readonly List<string> _Modifiers = new List<string>();
		private string _getAccessor;
		private string _getAccessorAccessibility = "";
		private string _setAccessor;
		private string _setAccessorAccessibility = "";
		private bool _mustRemove = false;

		private readonly List<AttributeConstraintBuilder<PropertyConstraintBuilder>> attributes = new List<AttributeConstraintBuilder<PropertyConstraintBuilder>>();

		public PropertyConstraintBuilder(Class @class, string name, IList<string> oldNames)
		{
			_class = @class;
			_name = name;

			foreach (string oldName in oldNames)
				_oldNames.Add(oldName);
		}

		public PropertyConstraintBuilder RemoveProperty()
		{
			_mustRemove = true; return this;
		}

		public PropertyConstraintBuilder WithType(string typeName)
		{
			_typeName = typeName; return this;
		}

		public PropertyConstraintBuilder WithModifiers(params string[] modifiers)
		{
			_Modifiers.AddRange(modifiers); return this;
		}

		public PropertyConstraintBuilder WithGetAccessorBody(string getAccessor)
		{
			_getAccessor = getAccessor; return this;
		}

		public PropertyConstraintBuilder WithGetAccessorAccessibility(string accessibility)
		{
			_getAccessorAccessibility = accessibility; return this;
		}

		public PropertyConstraintBuilder WithSetAccessorBody(string setAccessor)
		{
			_setAccessor = setAccessor; return this;
		}

		public PropertyConstraintBuilder WithSetAccessorAccessibility(string accessibility)
		{
			_setAccessorAccessibility = accessibility; return this;
		}

		public AttributeConstraintBuilder<PropertyConstraintBuilder> WithAttribute(string attributeName)
		{
			var attribute = new AttributeConstraintBuilder<PropertyConstraintBuilder>(this, attributeName);
			attributes.Add(attribute);
			return attribute;
		}

		public void ApplyTo(Actions actions)
		{
			var property = Construct(_class);

			// Try find the property in the existing class using the old name
			var existingProperty = _class.Properties.FirstOrDefault(p => p.Name == _name);

			if (existingProperty == null)
			{
				for (int i = _oldNames.Count - 1; i >= 0; i--)
				{
					// No property found with the new name, so try the old name
					existingProperty = _class.Properties.FirstOrDefault(p => p.Name == _oldNames[i]);

					if (existingProperty != null)
						break;
				}
			}
			if (existingProperty == null)
			{
				// As a last ditch effort to find the property, ignore case when searching
				existingProperty = _class.Properties.FirstOrDefault(p => p.Name.ToLowerInvariant() == _name.ToLowerInvariant());
			}
			if (existingProperty != null)
			{
				if (_mustRemove)
					ApplyPropertyDeletion(actions, existingProperty);
				else
				{
					// Make sure the property matches the given property
					ApplyPropertyChanges(actions, existingProperty, property);
				}
			}
			else if (_mustRemove)
			{
				// No existing property found, so we don't need to do anything, it's already removed.
				// Don't add any actions
				return;
			}
			else
			{
				// Add the property to the class.
				actions.AddAction(new AddPropertyToClassAction(property, AdditionPoint.EndOfParent, _class));
			}
			if (!_mustRemove)
			{
				var propertyToAddTo = existingProperty ?? property;

				foreach (var attrBuilder in attributes)
				{
					var attribute = attrBuilder.Build(propertyToAddTo.Controller);

					if (propertyToAddTo.Attributes.Any(a => a.Name == attribute.Name) ||
						propertyToAddTo.AttributeSections.SelectMany(section => section.Attributes).Any(a => a.Name == attribute.Name))
					{
						// if there are any attributes on the property currently with this name, then we should skip it.
						continue;
					}

					actions.AddAction(new AddAttributeToPropertyAction(propertyToAddTo, attribute));
				}
			}
		}

		private static void ApplyPropertyChanges(Actions actions, Property existingProperty, Property exampleProperty)
		{
			ApplyNameChanges(existingProperty, exampleProperty, actions);
			ApplyModifierChanges(existingProperty, exampleProperty, actions);
			ApplyAccessorChanges(existingProperty, exampleProperty, actions);
			ApplyTypeChanges(existingProperty, exampleProperty, actions);
		}

		private static void ApplyPropertyDeletion(Actions actions, Property existingProperty)
		{
			actions.AddAction(new RemovePropertyFromClassAction(existingProperty));
		}

		private static void ApplyNameChanges(Property existingProperty, Property exampleProperty, Actions actions)
		{
			if (existingProperty.Name != exampleProperty.Name)
			{
				// Change datatype
				actions.AddAction(new ChangeNameOfPropertyAction(existingProperty, exampleProperty.Name));
			}
		}

		private static void ApplyTypeChanges(Property existingProperty, Property exampleProperty, Actions actions)
		{
			if (exampleProperty.DataType != null && existingProperty.DataType.Equals(exampleProperty.DataType) == false)
			{
				// Change datatype
				actions.AddAction(new ChangeTypeOfPropertyAction(existingProperty, exampleProperty.DataType));
			}
		}

		private static void ApplyModifierChanges(Property existingProperty, Property exampleProperty, Actions actions)
		{
			if (existingProperty.Modifiers.UnorderedEqual(exampleProperty.Modifiers) == false)
			{
				actions.AddAction(new RemoveModifiersFromPropertyAction(existingProperty));

				foreach (var modifier in exampleProperty.Modifiers)
				{
					actions.AddAction(new AddModifierToPropertyAction(existingProperty, modifier, false));
				}
			}
		}

		private static void ApplyAccessorChanges(Property existingProperty, Property exampleProperty, Actions actions)
		{
			if (existingProperty.GetAccessor == null)
			{
				actions.AddAction(new AddAccessorToPropertyAction(existingProperty, exampleProperty.GetAccessor));
			}
			else if (exampleProperty.GetAccessor != null && existingProperty.GetAccessor.Modifier != exampleProperty.GetAccessor.Modifier)
			{
				actions.AddAction(new ChangePropertyAccessorModifierAction(existingProperty.GetAccessor, exampleProperty.GetAccessor.Modifier));
			}

			if (existingProperty.SetAccessor == null)
			{
				actions.AddAction(new AddAccessorToPropertyAction(existingProperty, exampleProperty.SetAccessor));
			}
			else if (exampleProperty.SetAccessor != null && existingProperty.SetAccessor.Modifier != exampleProperty.SetAccessor.Modifier)
			{
				actions.AddAction(new ChangePropertyAccessorModifierAction(existingProperty.SetAccessor, exampleProperty.SetAccessor.Modifier));
			}
		}

		private Property Construct(Class @class)
		{
			Property prop = new Property(@class.Controller);
			prop.Name = _name;
			prop.Modifiers.AddRange(_Modifiers);
			prop.DataType = new DataType(@class.Controller, _typeName);

			if (string.IsNullOrEmpty(_getAccessor) == false)
			{
				prop.GetAccessor = new PropertyAccessor(@class.Controller, PropertyAccessor.AccessorTypes.Get)
									{
										BodyText = _getAccessor,
										Modifier = _getAccessorAccessibility,
										PreceedingBlankLines = -1
									};
			}
			if (string.IsNullOrEmpty(_setAccessor) == false)
			{
				prop.SetAccessor = new PropertyAccessor(@class.Controller, PropertyAccessor.AccessorTypes.Set)
									{
										BodyText = _setAccessor,
										Modifier = _setAccessorAccessibility,
										PreceedingBlankLines = -1
									};
			}

			return prop;
		}
	}
}
