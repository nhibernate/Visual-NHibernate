using System.Collections.Generic;
using System.Linq;
using ArchAngel.Providers.CodeProvider.CodeInsertions;
using ArchAngel.Providers.CodeProvider.DotNet;
using Slyce.Common.IEnumerableExtensions;

namespace ArchAngel.NHibernateHelper.CodeInsertions
{
	public class FieldConstraintBuilder
	{
		private readonly Class _class;
		private readonly string _name;
		private readonly IList<string> _oldNames = new List<string>();
		private string _typeName;
		private string _initialValue;
		private readonly List<string> _Modifiers = new List<string>();
		private bool _mustRemove = false;

		private readonly List<AttributeConstraintBuilder<FieldConstraintBuilder>> attributes = new List<AttributeConstraintBuilder<FieldConstraintBuilder>>();

		public FieldConstraintBuilder(Class @class, string name, IList<string> oldNames)
		{
			_class = @class;
			_name = name;

			foreach (string oldName in oldNames)
				_oldNames.Add(oldName);
		}

		public FieldConstraintBuilder RemoveField()
		{
			_mustRemove = true; return this;
		}

		public FieldConstraintBuilder WithType(string typeName)
		{
			_typeName = typeName; return this;
		}

		public FieldConstraintBuilder WithModifiers(params string[] modifiers)
		{
			_Modifiers.AddRange(modifiers); return this;
		}

		public FieldConstraintBuilder WithInitialValue(string initialValue)
		{
			_initialValue = initialValue; return this;
		}

		public AttributeConstraintBuilder<FieldConstraintBuilder> WithAttribute(string attributeName)
		{
			var attribute = new AttributeConstraintBuilder<FieldConstraintBuilder>(this, attributeName);
			attributes.Add(attribute);
			return attribute;
		}

		public void ApplyTo(Actions actions)
		{
			var field = Construct(_class);

			// Try find the field in the existing class using the old name
			var existingField = _class.Fields.FirstOrDefault(p => p.Name == _name);

			if (existingField == null)
			{
				for (int i = _oldNames.Count - 1; i >= 0; i--)
				{
					// No field found with the new name, so try the old name
					existingField = _class.Fields.FirstOrDefault(p => p.Name == _oldNames[i]);

					if (existingField != null)
						break;
				}
			}
			if (existingField == null)
			{
				// As a last ditch effort to find the field, ignore case when searching
				existingField = _class.Fields.FirstOrDefault(p => p.Name.ToLowerInvariant() == _name.ToLowerInvariant());
			}
			if (existingField != null)
			{
				if (_mustRemove)
					ApplyFieldDeletion(actions, existingField);
				else
				{
					// Make sure the field matches the given property
					ApplyFieldChanges(actions, existingField, field);
				}
			}
			else if (_mustRemove)
			{
				// No existing field found, so we don't need to do anything, it's already removed.
				// Don't add any actions
				return;
			}
			else
			{
				// Add the property to the class.
				actions.AddAction(new AddFieldToClassAction(field, AdditionPoint.EndOfParent, _class));
			}
			if (!_mustRemove)
			{
				var fieldToAddTo = existingField ?? field;

				foreach (var attrBuilder in attributes)
				{
					var attribute = attrBuilder.Build(fieldToAddTo.Controller);

					if (fieldToAddTo.Attributes.Any(a => a.Name == attribute.Name) ||
						fieldToAddTo.AttributeSections.SelectMany(section => section.Attributes).Any(a => a.Name == attribute.Name))
					{
						// if there are any attributes on the property currently with this name, then we should skip it.
						continue;
					}

					actions.AddAction(new AddAttributeToFieldAction(fieldToAddTo, attribute));
				}
			}
		}

		private static void ApplyFieldChanges(Actions actions, Field existingField, Field exampleField)
		{
			ApplyNameChanges(existingField, exampleField, actions);
			ApplyModifierChanges(existingField, exampleField, actions);
			ApplyInitialValueChanges(existingField, exampleField, actions);
			ApplyTypeChanges(existingField, exampleField, actions);
		}

		private static void ApplyFieldDeletion(Actions actions, Field existingField)
		{
			actions.AddAction(new RemoveFieldFromClassAction(existingField));
		}

		private static void ApplyNameChanges(Field existingField, Field exampleField, Actions actions)
		{
			if (existingField.Name != exampleField.Name)
			{
				// Change name
				actions.AddAction(new ChangeNameOfFieldAction(existingField, exampleField.Name));
			}
		}

		private static void ApplyTypeChanges(Field existingField, Field exampleField, Actions actions)
		{
			if (exampleField.DataType != null && existingField.DataType.Equals(exampleField.DataType) == false)
			{
				// Change datatype
				actions.AddAction(new ChangeTypeOfFieldAction(existingField, exampleField.DataType));
			}
		}

		private static void ApplyModifierChanges(Field existingField, Field exampleField, Actions actions)
		{
			if (existingField.Modifiers.UnorderedEqual(exampleField.Modifiers) == false)
			{
				string modifierString = "";

				foreach (var mod in existingField.Modifiers)
					modifierString += mod + " ";

				modifierString = modifierString.Trim();
				actions.AddAction(new RemoveModifierFromFieldAction(existingField, modifierString));

				foreach (var modifier in exampleField.Modifiers)
					actions.AddAction(new AddModifierToFieldAction(existingField, modifier, false));
			}
		}

		private static void ApplyInitialValueChanges(Field existingField, Field exampleField, Actions actions)
		{
			if (exampleField.InitialValue != null && existingField.InitialValue.Equals(exampleField.InitialValue) == false)
			{
				// Change initial value
				actions.AddAction(new ChangeInitialValueOfFieldAction(existingField, exampleField.InitialValue));
			}
		}

		private Field Construct(Class @class)
		{
			Field field = new Field(@class.Controller);
			field.Name = _name;
			field.Modifiers.AddRange(_Modifiers);
			field.DataType = new DataType(@class.Controller, _typeName);
			field.InitialValue = _initialValue;

			return field;
		}
	}
}
