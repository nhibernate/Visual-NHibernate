
namespace ArchAngel.Providers.EntityModel.Model.DatabaseLayer.Discrimination
{
	public class DiscriminatorBuilder
	{
		public static Discriminator SingleConditionDiscriminator(IColumn column, Operator op, ExpressionValue val)
		{
			Discriminator dis = new DiscriminatorImpl();

			dis.RootGrouping = new AndGrouping();
			dis.RootGrouping.AddCondition(new ConditionImpl()
											{
												Column = column,
												Operator = op,
												ExpressionValue = val
											});
			return dis;
		}
	}
}
