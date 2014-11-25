using System.Linq;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CodeInsertions
{
	public class AddMethodToClassAction : AddChildToClassAction
	{
		private readonly Function MethodToAdd;

		public AddMethodToClassAction(Function functionToAdd, AdditionPoint additionPoint, Class classToAddTo)
		{
			ConstructToAdd = MethodToAdd = functionToAdd;
			AdditionPoint = additionPoint;
			ClassToAddTo = classToAddTo;
		}

		public override ActionResult ApplyActionTo(StringBuilder sb)
		{
			if (AdditionPoint == AdditionPoint.EndOfParent)
			{
				//IBaseConstruct lastSibling = ClassToAddTo.WalkChildren().OrderBy(bc => bc.TextRange.EndOffset).Last();
				IBaseConstruct lastSibling = ClassToAddTo.WalkChildren().OfType<Function>()
					.OrderBy(bc => bc.TextRange.EndOffset).LastOrDefault();

				ClassToAddTo.AddChild(MethodToAdd);

				return InsertAtEndOfParent(sb, lastSibling);
			}

			return new ActionResult();
		}

		protected override void BeforeInsert(int insertionIndex, string textToInsert)
		{
		}
	}
}
