using System.Collections.Generic;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.NHibernateHelper.CodeInsertions
{
	public class AttributeConstraintBuilder<T>
	{
		private readonly string _attributeName;

		private readonly List<string> _parameters = new List<string>();
		private readonly List<KeyValuePair<string, string>> _namedParameters = new List<KeyValuePair<string, string>>();
		private T _parent;
		private bool _mustRemove = false;

		public AttributeConstraintBuilder(T parent, string name)
		{
			_parent = parent;
			_attributeName = name;
		}

		public AttributeConstraintBuilder<T> RemoveAttribute()
		{
			_mustRemove = true; return this;
		}

		public AttributeConstraintBuilder<T> WithParameter(string value)
		{
			_parameters.Add(value); return this;
		}

		public AttributeConstraintBuilder<T> WithParameters(string[] values)
		{
			foreach (string value in values)
				_parameters.Add(value);

			return this;
		}

		public AttributeConstraintBuilder<T> WithNamedParameter(string name, string value)
		{
			_namedParameters.Add(new KeyValuePair<string, string>(name, value)); return this;
		}

		public T Finish() { return _parent; }

		public Attribute Build(Controller controller)
		{
			if (_mustRemove)
				return null;

			var attribute = new Attribute(controller);
			attribute.Name = _attributeName;

			attribute.PositionalArguments.AddRange(_parameters);

			foreach (var named in _namedParameters)
			{
				attribute.NamedArguments.Add(new Attribute.NamedArgument(named.Key, named.Value));
			}
			return attribute;
		}
	}
}