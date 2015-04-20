using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ArchAngel.Providers.EntityModel.Model.DatabaseLayer.Discrimination
{
	public class AndGrouping : Grouping
	{
		private readonly List<Condition> conditions = new List<Condition>();
		private readonly List<Grouping> groupings = new List<Grouping>();

		public IEnumerable<ILeaf> Children
		{
			get
			{
				foreach(var condition in conditions)
					yield return condition;
				foreach (var grouping in groupings)
					yield return grouping;
			}
		}

		public void AcceptVisitor(IVisitor visitor)
		{
			visitor.ProcessGrouping(this);
		}

		public bool ContainsConditions { get { return conditions.Count > 0; } }
		public bool ContainsGroupings { get { return groupings.Count > 0; } }

		public ReadOnlyCollection<Condition> Conditions
		{
			get { return conditions.AsReadOnly(); }
		}

		public ReadOnlyCollection<Grouping> Groupings
		{
			get { return groupings.AsReadOnly(); }
		}

		public void AddCondition(Condition condition)
		{
			groupings.Clear();
			conditions.Add(condition);
		}

		public void AddGrouping(Grouping grouping)
		{
			conditions.Clear();
			groupings.Add(grouping);
		}
	}
}