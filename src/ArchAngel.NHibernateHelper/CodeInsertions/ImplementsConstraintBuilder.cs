using System.Linq;
using ArchAngel.Providers.CodeProvider.CodeInsertions;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.NHibernateHelper.CodeInsertions
{
	public class ImplementsConstraintBuilder
	{
		private readonly Class _class;
		private readonly string _value;
		private bool _mustRemove = false;

		public ImplementsConstraintBuilder(Class @class, string name)
		{
			_class = @class;
			_value = name;
		}

		public ImplementsConstraintBuilder RemoveCustomImplements()
		{
			_mustRemove = true; return this;
		}

		public void ApplyTo(Actions actions)
		{
			var interface1 = Construct(_class);

			// Try find the Interface in the existing class using the old name
			var existingInterface = _class.BaseNames.FirstOrDefault(i => i == _value);

			if (existingInterface != null)
			{
				if (_mustRemove)
					ApplyImplementsDeletion(actions, _class, existingInterface);
			}
			else if (!_mustRemove)
			{
				// Add the Implements to the class.
				DataType newImplements = new DataType(_class.Controller, interface1.Name);
				actions.AddAction(new AddImplementsToClassAction(_class, newImplements, false));
			}
		}

		private static void ApplyImplementsDeletion(Actions actions, Class _class, string existingInterface)
		{
			DataType newImplements = new DataType(_class.Controller, existingInterface);
			actions.AddAction(new RemoveImplementsFromClassAction(_class, existingInterface));
		}

		private Interface Construct(Class @class)
		{
			Interface i = new Interface(@class.Controller, _value);
			return i;
		}
	}
}
