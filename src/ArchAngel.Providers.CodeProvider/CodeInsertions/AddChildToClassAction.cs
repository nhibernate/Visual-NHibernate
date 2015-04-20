using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CodeInsertions
{
	public abstract class AddChildToClassAction : CodeInsertionAction
	{
		protected BaseConstruct ConstructToAdd { get; set; }
		protected AdditionPoint AdditionPoint { get; set; }
		protected Class ClassToAddTo { get; set; }

		protected abstract void BeforeInsert(int insertionIndex, string textToInsert);

		protected ActionResult InsertAtStartOfParent(StringBuilder sb)
		{
			int searchStart = ClassToAddTo.TextRange.StartOffset;
			int searchEnd = ClassToAddTo.TextRange.EndOffset;
			var text = sb.ToString();

			// Figure out where the first NewLine is after the class's opening brace
			int braceIndex = text.IndexOf("{", searchStart, searchEnd - searchStart);

			// Find the first NewLine after the brace
			int newLineIndex = text.IndexOf("\n", braceIndex, searchEnd - braceIndex);

			// Indent one level further than the Class we are adding to.
			int numTabs = InsertionHelpers.GetIndentationInFrontOf(text, braceIndex) + 1;
			ConstructToAdd.Controller.IndentLevel = numTabs;
			ConstructToAdd.PreceedingBlankLines = -1;
			string newPropertyText = ConstructToAdd.ToString();

			int insertionIndex = newLineIndex + 1;

			ConstructToAdd.TextRange.StartOffset = insertionIndex + numTabs;
			ConstructToAdd.TextRange.EndOffset = insertionIndex + (newPropertyText).Length - numTabs;
			ConstructToAdd.Index = insertionIndex + numTabs;

			BeforeInsert(insertionIndex, newPropertyText);
			sb.Insert(insertionIndex, newPropertyText);

			//return new ActionResult(insertionIndex, newPropertyText.Length, new[] { ConstructToAdd });
			return new ActionResult(ConstructToAdd.TextRange.StartOffset, newPropertyText.Length, new[] { ConstructToAdd });
		}

		protected ActionResult InsertAtEndOfParent(StringBuilder sb, IBaseConstruct insertAfter)
		{
			if (insertAfter == null)
				return InsertAtStartOfParent(sb);

			int lastSiblingStartOffset = insertAfter.TextRange.StartOffset;
			int lastSiblingEndOffset = insertAfter.TextRange.EndOffset;
			int endIndex = ClassToAddTo.TextRange.EndOffset;
			string text = sb.ToString();

			if (lastSiblingEndOffset < 0 || string.IsNullOrWhiteSpace(text))
				return InsertAtStartOfParent(sb);

			int firstNewLine;

			if (endIndex > lastSiblingEndOffset)
				firstNewLine = text.IndexOf('\n', lastSiblingEndOffset, (endIndex - lastSiblingEndOffset));
			else
				firstNewLine = text.IndexOf('\n', lastSiblingEndOffset);

			int newStartIndex = firstNewLine + 1;
			int numTabs = InsertionHelpers.GetIndentationInFrontOf(sb, lastSiblingStartOffset);
			ConstructToAdd.Controller.IndentLevel = numTabs;
			ConstructToAdd.PreceedingBlankLines = -1;
			string newPropertyText = ConstructToAdd.ToString();

			// The "+ numTabs" is account for the tabs that are at the start
			// of the new text. If we don't trim those off the text range, then it will
			// include the whitespace before the actual property, which the objects which have actually
			// been parsed will not have. If this number is wrong, the next object to be placed after it
			// will not be able to get the tabs correctly.
			ConstructToAdd.TextRange.StartOffset = newStartIndex + numTabs;
			ConstructToAdd.TextRange.EndOffset = newStartIndex + numTabs + newPropertyText.Trim().Length;
			ConstructToAdd.Index = newStartIndex + numTabs;

			BeforeInsert(newStartIndex, newPropertyText);
			sb.Insert(newStartIndex, newPropertyText);

			return new ActionResult(ConstructToAdd.TextRange.StartOffset, newPropertyText.Length, new[] { ConstructToAdd });
		}
	}
}
