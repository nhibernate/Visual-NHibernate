using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CodeInsertions
{
	public class RemoveModifiersFromPropertyAction : CodeInsertionAction
	{
		public Property PropertyToChange { get; set; }

		public RemoveModifiersFromPropertyAction(Property propertyToChange)
		{
			PropertyToChange = propertyToChange;
		}

		public override ActionResult ApplyActionTo(StringBuilder sb)
		{
			int searchStart = PropertyToChange.TextRange.StartOffset;
			int searchEnd = PropertyToChange.TextRange.EndOffset;
			string text = sb.ToString();

			// Search the Property's TextRange for the type of the property
			int typeIndex = text.IndexOf(PropertyToChange.DataType.ToString(), searchStart, searchEnd-searchStart);

			// Remove everything before the type keyword
			var numCharactersRemoved = typeIndex - searchStart;
			sb.Remove(searchStart, numCharactersRemoved);
			PropertyToChange.Modifiers.Clear();

			return new ActionResult(searchStart, -numCharactersRemoved, null);
		}
	}
}