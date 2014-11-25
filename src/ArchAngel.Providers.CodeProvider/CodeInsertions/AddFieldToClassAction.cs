using System.Linq;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CodeInsertions
{
	public class AddFieldToClassAction : AddChildToClassAction
	{
		private readonly Field FieldToAdd;

		public AddFieldToClassAction(Field fieldToAdd, AdditionPoint additionPoint, Class classToAddTo)
		{
			ConstructToAdd = FieldToAdd = fieldToAdd;
			AdditionPoint = additionPoint;
			ClassToAddTo = classToAddTo;
		}

		public override ActionResult ApplyActionTo(StringBuilder sb)
		{
			if (AdditionPoint == AdditionPoint.EndOfParent)
			{
				IBaseConstruct lastSibling = ClassToAddTo.WalkChildren().OfType<Field>()
					.OrderBy(bc => bc.TextRange.EndOffset).LastOrDefault();

				ClassToAddTo.AddField(FieldToAdd);

				var result = InsertAtEndOfParent(sb, lastSibling);
				return result;
			}
			return new ActionResult();
		}

		protected override void BeforeInsert(int insertionIndex, string textToInsert)
		{
		}
	}
}
