using System.Collections.Generic;

namespace ArchAngel.Providers.EntityModel.Model.DatabaseLayer.Discrimination
{
	public class ConditionImpl : Condition
	{
		public ConditionImpl()
		{
		}

		public ConditionImpl(IColumn column, Operator @operator, ExpressionValue expressionValue)
		{
			Column = column;
			Operator = @operator;
			ExpressionValue = expressionValue;
		}

		public IColumn Column { get; set; }
		public Operator Operator { get; set; }
		public ExpressionValue ExpressionValue { get; set; }

		public IEnumerable<ILeaf> Children
		{
			get
			{
				yield return new ColumnWrapper(Column);
				yield return Operator;
				yield return ExpressionValue;
			}
		}

		public void AcceptVisitor(IVisitor visitor)
		{
			visitor.ProcessCondition(this);
		}
	}
}