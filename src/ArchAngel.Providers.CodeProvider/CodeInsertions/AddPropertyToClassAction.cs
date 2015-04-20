using System.Linq;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CodeInsertions
{
	public class AddPropertyToClassAction : AddChildToClassAction
	{
		private readonly Property PropertyToAdd;

		public AddPropertyToClassAction(Property propertyToAdd, AdditionPoint additionPoint, Class classToAddTo)
		{
			ConstructToAdd = PropertyToAdd = propertyToAdd;
			AdditionPoint = additionPoint;
			ClassToAddTo = classToAddTo;
		}

		public override ActionResult ApplyActionTo(StringBuilder sb)
		{
			if (AdditionPoint == AdditionPoint.EndOfParent)
			{
				IBaseConstruct lastSibling = ClassToAddTo.WalkChildren().OfType<Property>()
					.OrderBy(bc => bc.TextRange.EndOffset).LastOrDefault();

				ClassToAddTo.Properties.Add(PropertyToAdd);

				var result = InsertAtEndOfParent(sb, lastSibling);

				if (PropertyToAdd.GetAccessor != null)
					result.ConstructsChanged.Add(PropertyToAdd.GetAccessor);
				if (PropertyToAdd.SetAccessor != null)
					result.ConstructsChanged.Add(PropertyToAdd.SetAccessor);
				return result;
			}

			return new ActionResult();
		}

		protected override void BeforeInsert(int insertionIndex, string textToInsert)
		{
			FixAccessor(PropertyToAdd.GetAccessor, textToInsert, insertionIndex);
			FixAccessor(PropertyToAdd.SetAccessor, textToInsert, insertionIndex);
		}

		private static void FixAccessor(PropertyAccessor accessor, string textToInsert, int insertionIndex)
		{
			if (accessor != null)
			{
				// Need to set the text range of the get {}
				var accessorText = accessor.ToString().Trim();
				int startIndex = textToInsert.IndexOf(accessorText) + insertionIndex;
				int endIndex = startIndex + accessorText.Length;

				accessor.TextRange.StartOffset = startIndex;
				accessor.TextRange.EndOffset = endIndex;
			}
		}
	}

	public enum AdditionPoint
	{
		EndOfParent
	}
}
