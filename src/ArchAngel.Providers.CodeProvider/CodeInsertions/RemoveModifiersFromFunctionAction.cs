using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CodeInsertions
{
	public class RemoveModifiersFromFunctionAction : CodeInsertionAction
	{
		public Function FunctionToChange { get; set; }

		public RemoveModifiersFromFunctionAction(Function functionToChange)
		{
			FunctionToChange = functionToChange;
		}

		public override ActionResult ApplyActionTo(StringBuilder sb)
		{
			int searchStart = FunctionToChange.TextRange.StartOffset;
			int searchEnd = FunctionToChange.TextRange.EndOffset;
			string text = sb.ToString();

			// Search the Function's TextRange for the type of the function
			int typeIndex = text.IndexOf(FunctionToChange.ReturnType.ToString(), searchStart, searchEnd - searchStart);

			// Remove everything before the type keyword
			var numCharactersRemoved = typeIndex - searchStart;
			sb.Remove(searchStart, numCharactersRemoved);
			FunctionToChange.Modifiers.Clear();

			return new ActionResult(searchStart, -numCharactersRemoved, null);
		}
	}
}