using System.Linq;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CodeInsertions
{
	public class AddFunctionToClassAction : AddChildToClassAction
	{
		private readonly Function FunctionToAdd;

		public AddFunctionToClassAction(Function functionToAdd, AdditionPoint additionPoint, Class classToAddTo)
		{
			ConstructToAdd = FunctionToAdd = functionToAdd;
			AdditionPoint = additionPoint;
			ClassToAddTo = classToAddTo;
		}

		public override ActionResult ApplyActionTo(StringBuilder sb)
		{
			if (AdditionPoint == AdditionPoint.EndOfParent)
			{
				IBaseConstruct lastSibling = ClassToAddTo.WalkChildren().OfType<Function>()
					.OrderBy(bc => bc.TextRange.EndOffset).LastOrDefault();

				ClassToAddTo.AddChild(FunctionToAdd);

				return InsertAtEndOfParent(sb, lastSibling);
			}

			return new ActionResult();
		}

		protected override void BeforeInsert(int insertionIndex, string textToInsert)
		{
		}
	}
}
