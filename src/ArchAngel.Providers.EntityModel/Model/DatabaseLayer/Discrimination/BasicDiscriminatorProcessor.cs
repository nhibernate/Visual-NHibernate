using System;

namespace ArchAngel.Providers.EntityModel.Model.DatabaseLayer.Discrimination
{
	public class BasicDiscriminatorProcessor : IVisitor
	{
		public Action<Discriminator> ProcessDiscriminatorObject;
		public Action<Grouping> ProcessGroupingObject;
		public Action<Condition> ProcessConditionObject;
		public Action<ColumnWrapper> ProcessColumnObject;
		public Action<Operator> ProcessOperatorObject;
		public Action<ExpressionValue> ProcessExpressionValueObject;

		public void ProcessDiscriminator(Discriminator dis)
		{
            if(ProcessOperatorObject != null)
			    ProcessDiscriminatorObject(dis);
            foreach(var obj in dis.Children)
                obj.AcceptVisitor(this);
		}

		public void ProcessGrouping(Grouping group)
		{
            if (ProcessGroupingObject != null)
    			ProcessGroupingObject(group);
            foreach (var obj in group.Children)
                obj.AcceptVisitor(this);
		}

		public void ProcessCondition(Condition condition)
		{
            if (ProcessConditionObject != null)
			    ProcessConditionObject(condition);
            foreach (var obj in condition.Children)
                obj.AcceptVisitor(this);
		}

		public void ProcessColumn(ColumnWrapper col)
		{
            if (ProcessColumnObject != null)
			    ProcessColumnObject(col);
            foreach (var obj in col.Children)
                obj.AcceptVisitor(this);
		}

		public void ProcessOperator(Operator op)
		{
            if (ProcessOperatorObject != null)
			    ProcessOperatorObject(op);
            foreach (var obj in op.Children)
                obj.AcceptVisitor(this);
		}

		public void ProcessExpressionValue(ExpressionValue exprValue)
		{
            if (ProcessExpressionValueObject != null)
			    ProcessExpressionValueObject(exprValue);
            foreach (var obj in exprValue.Children)
                obj.AcceptVisitor(this);
		}
	}
}
