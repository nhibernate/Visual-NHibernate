using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ArchAngel.Providers.EntityModel.Model.DatabaseLayer.Discrimination
{
	public interface ILeaf
	{
		IEnumerable<ILeaf> Children { get; }
		void AcceptVisitor(IVisitor visitor);
	}

	public interface IVisitor
	{
		void ProcessDiscriminator(Discriminator dis);
		void ProcessGrouping(Grouping group);
		void ProcessCondition(Condition condition);
		void ProcessColumn(ColumnWrapper col);
		void ProcessOperator(Operator op);
		void ProcessExpressionValue(ExpressionValue exprValue);
	}

	public class ColumnWrapper : ILeaf
	{
		public IEnumerable<ILeaf> Children { get { return new List<ILeaf>(); } }
		public IColumn Column { get; private set; }

		public ColumnWrapper(IColumn column)
		{
			Column = column;
		}

		public void AcceptVisitor(IVisitor visitor)
		{
			visitor.ProcessColumn(this);
		}
	}

	public interface Condition : ILeaf
	{
		IColumn Column { get; set; }
		Operator Operator { get; set; }
		ExpressionValue ExpressionValue { get; set; }
	}

	public interface Grouping : ILeaf
	{
		bool ContainsConditions { get; }
		bool ContainsGroupings { get; }
		ReadOnlyCollection<Condition> Conditions { get; }
		ReadOnlyCollection<Grouping> Groupings { get; }
		void AddCondition(Condition condition);
		void AddGrouping(Grouping grouping);
	}

	public interface Discriminator : ILeaf
	{
		Grouping RootGrouping { get; set; }
	}

	public interface ExpressionValue : ILeaf
	{
		string Value { get; set; }
	}

	public class ExpressionValueImpl : ExpressionValue
	{
		public string Value { get; set; }

		public ExpressionValueImpl(string value)
		{
			Value = value;
		}

		public IEnumerable<ILeaf> Children
		{
			get { return new List<ILeaf>(); }
		}

		public void AcceptVisitor(IVisitor visitor)
		{
			visitor.ProcessExpressionValue(this);
		}
	}

	public class Operator : ILeaf
	{
		public string Name { get; private set; }
		public string DisplayText { get; set; }

		protected Operator(string name, string displayText)
		{
			Name = name;
			DisplayText = displayText;
		}

		public static readonly Operator Equal = new Operator("Equal", "=");
		public static readonly Operator NotEqual = new Operator("NotEqual", "!=");

		public IEnumerable<ILeaf> Children { get { return new List<ILeaf>(); } }

		public static IEnumerable<Operator> BuiltInOperations
		{
			get
			{
				yield return Equal;
				yield return NotEqual;
			}
		}

		public void AcceptVisitor(IVisitor visitor)
		{
			visitor.ProcessOperator(this);
		}

		public static Operator FromString(string name)
		{
			switch (name)
			{
				case "=":
				case "Equal":
					return Equal;
				case "!=":
				case "NotEqual":
					return NotEqual;
				default:
					return new Operator(name, name);
			}
		}
	}
}
