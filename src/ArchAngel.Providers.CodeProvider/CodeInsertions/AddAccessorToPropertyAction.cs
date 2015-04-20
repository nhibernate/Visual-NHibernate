using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;
using log4net;

namespace ArchAngel.Providers.CodeProvider.CodeInsertions
{
	public class AddAccessorToPropertyAction : CodeInsertionAction
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(AddModifierToPropertyAction));

		public Property PropertyToChange { get; set; }
		public PropertyAccessor NewAccessor { get; set; }

		public AddAccessorToPropertyAction(Property propertyToChange, PropertyAccessor newAccessor)
		{
			PropertyToChange = propertyToChange;
			NewAccessor = newAccessor;
		}

		public override ActionResult ApplyActionTo(StringBuilder sb)
		{
			int searchStart = PropertyToChange.TextRange.StartOffset;
			int searchEnd = PropertyToChange.TextRange.EndOffset;
			string text = sb.ToString();

			// Find the opening brace.
			var endOfOpeningBraceIndex = text.IndexOf("{", searchStart, searchEnd - searchStart) + 1;

			int numTabs = InsertionHelpers.GetIndentationInFrontOf(text, searchStart) + 1;
			NewAccessor.Controller.IndentLevel = numTabs;

			string newAccessorText = Helper.RemoveTrailingLineBreaks(NewAccessor.ToString());

			// The "+ numTabs + 2" is account for the /r/n and the tabs that are at the start
			// of the new text. If we don't trim those off the text range, then it will
			// include the whitespace before the actual property, which the objects which have actually
			// been parsed will not have. If this number is wrong, the next object to be placed after it
			// will not be able to get the tabs correctly.
			NewAccessor.TextRange.StartOffset = endOfOpeningBraceIndex + numTabs + 2;
			NewAccessor.TextRange.EndOffset = endOfOpeningBraceIndex + newAccessorText.Length - (numTabs + 2);
			NewAccessor.Index = endOfOpeningBraceIndex + numTabs + 2;

			sb.Insert(endOfOpeningBraceIndex, newAccessorText);

			// Add accessor to the property
			PropertyToChange.AddChild(NewAccessor);
			NewAccessor.Parent = PropertyToChange;
			NewAccessor.Controller = PropertyToChange.Controller;

			//return new ActionResult(endOfOpeningBraceIndex, newAccessorText.Length, new[] { NewAccessor });
			return new ActionResult(NewAccessor.TextRange.StartOffset, newAccessorText.Length, new[] { NewAccessor });
		}
	}
}