using System.Linq;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer.Discrimination;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Specs_For_DiscriminatorBuilder
{
	[TestFixture]
	public class When_Constructing_A_Single_Condition_Discriminator
	{
		[Test]
		public void It_Is_Built_Correctly()
		{
			var build = new DiscriminatorBuilder();
			ExpressionValue val = MockRepository.GenerateMock<ExpressionValue>();
			Operator op = Operator.Equal;
			IColumn column = MockRepository.GenerateMock<IColumn>(); ;
			Discriminator dis = build.SingleConditionDiscriminator(column, op, val);

			Assert.That(dis.RootGrouping, Is.Not.Null);
			Assert.That(dis.RootGrouping.Groupings, Is.Empty);
			Assert.That(dis.RootGrouping.Conditions, Has.Count(1));

			Condition condition = dis.RootGrouping.Conditions[0];
			Assert.That(condition.Column, Is.SameAs(column));
			Assert.That(condition.Operator, Is.SameAs(op));
			Assert.That(condition.ExpressionValue, Is.SameAs(val));
		}
	}
}