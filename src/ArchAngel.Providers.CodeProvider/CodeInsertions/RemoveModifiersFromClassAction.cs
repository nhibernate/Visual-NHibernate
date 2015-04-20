using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CodeInsertions
{
	public class RemoveModifiersFromClassAction : CodeInsertionAction
	{
		public Class ClassToChange { get; set; }

		public RemoveModifiersFromClassAction(Class classToChange)
		{
			ClassToChange = classToChange;
		}

		public override ActionResult ApplyActionTo(StringBuilder sb)
		{
			int searchStart = ClassToChange.TextRange.StartOffset;
			int searchEnd = ClassToChange.TextRange.EndOffset;
			string text = sb.ToString();

			// Search Class TextRange for class
			int classIndex = text.IndexOf("class", searchStart, searchEnd-searchStart);

			//Remove everything before the class keyword
			var numCharactersRemoved = classIndex - searchStart;
			sb.Remove(searchStart, numCharactersRemoved);
			ClassToChange.Modifiers.Clear();

			return new ActionResult(searchStart, -numCharactersRemoved, null);
		}
	}
}