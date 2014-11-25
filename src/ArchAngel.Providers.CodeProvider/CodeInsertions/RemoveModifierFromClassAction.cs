using System.Text;
using System.Text.RegularExpressions;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CodeInsertions
{
	public class RemoveModifierFromClassAction : CodeInsertionAction
	{
		public Class ClassToChange { get; set; }
		public string ModifierToRemove { get; set; }

		public RemoveModifierFromClassAction(Class classToChange, string modifierToRemove)
		{
			ClassToChange = classToChange;
			ModifierToRemove = modifierToRemove;
		}

		public override ActionResult ApplyActionTo(StringBuilder sb)
		{
			int searchStart = ClassToChange.TextRange.StartOffset;
			int searchEnd = ClassToChange.TextRange.EndOffset;
			string text = sb.ToString().Substring(searchStart, searchEnd-searchStart);

			// Trim to just before the class keyword.
			var classIndex = text.IndexOf("class");
			text = text.Substring(0, classIndex);


			//Remove each instance of the modifier before the class keyword
			// as well as the whitespace that comes just after it.
			Regex removalRegex = new Regex(ModifierToRemove + @"\s+");
			var newText = removalRegex.Replace(text, "");

			sb.Remove(searchStart, classIndex);
			sb.Insert(searchStart, newText);

			// Remove the modifier from the class object.
			ClassToChange.Modifiers.RemoveAll(s => s == ModifierToRemove);

			return new ActionResult(searchStart, newText.Length - classIndex, null);
		}
	}
}