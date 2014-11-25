using System.Collections.Generic;
using ArchAngel.Providers.CodeProvider.CodeInsertions;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.NHibernateHelper.CodeInsertions
{
	public class ClassConstraintBuilder
	{
		private Class _class;
		private List<AttributeConstraintBuilder<ClassConstraintBuilder>> attributes = new List<AttributeConstraintBuilder<ClassConstraintBuilder>>();
		private List<string> attributesToDelete = new List<string>();

		public ClassConstraintBuilder(Class @class)
		{
			_class = @class;
		}

		public AttributeConstraintBuilder<ClassConstraintBuilder> AttributeNamed(string attributeName)
		{
			var builder = new AttributeConstraintBuilder<ClassConstraintBuilder>(this, attributeName);
			attributes.Add(builder);
			return builder;
		}

		public AttributeConstraintBuilder<ClassConstraintBuilder> HasNoAttributeNamed(string attributeName)
		{
			var builder = new AttributeConstraintBuilder<ClassConstraintBuilder>(this, attributeName);
			//attributes.Add(builder);
			attributesToDelete.Add(attributeName);
			return builder;
		}

		public void ApplyTo(Actions actions)
		{
			foreach (var attr in attributes)
			{
				var attribute = attr.Build(_class.Controller);

				if (attribute == null)
					continue;

				if (_class.HasAttributeNamed(attribute.Name) || attributesToDelete.Contains(attribute.Name)) continue;

				actions.AddAction(new AddAttributeToClassAction(_class, attribute));
			}
			foreach (var attr in attributesToDelete)
			{
				if (_class.HasAttributeNamed(attr))
				{
					ArchAngel.Providers.CodeProvider.DotNet.Attribute att = _class.Attributes.Find(a => a.Name == attr);
					actions.AddAction(new RemoveAttributeFromClassAction(att));
				}
			}
		}
	}
}