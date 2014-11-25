using System.Linq;
using ArchAngel.Providers.CodeProvider.CodeInsertions;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.NHibernateHelper.CodeInsertions
{
	public class UsingStatementConstraintBuilder
	{
		private readonly Class _class;
		private readonly string _value;
		private bool _mustRemove = false;

		public UsingStatementConstraintBuilder(Class @class, string name)
		{
			_class = @class;
			_value = name;
		}

		public UsingStatementConstraintBuilder RemoveUsingStatement()
		{
			_mustRemove = true; return this;
		}

		public void ApplyTo(Actions actions)
		{
			var usingStatement = Construct(_class);

			// Try find the UsingStatement in the existing class using the old name
			var existingUsingStatement = _class.Controller.Root.UsingStatements.FirstOrDefault(u => u.ToString() == _value);

			if (existingUsingStatement != null)
			{
				if (_mustRemove)
					ApplyUsingStatementDeletion(actions, existingUsingStatement);
			}
			else if (!_mustRemove)
			{
				// Add the UsingStatement to the class.
				actions.AddAction(new AddUsingStatementToClassAction(_class, usingStatement));
			}
		}

		private static void ApplyUsingStatementDeletion(Actions actions, UsingStatement existingUsingStatement)
		{
			actions.AddAction(new RemoveUsingStatementFromClassAction(existingUsingStatement));
		}

		private UsingStatement Construct(Class @class)
		{
			UsingStatement us = new UsingStatement(@class.Controller, "", _value);
			return us;
		}
	}
}
