using System.Text;
using System.Text.RegularExpressions;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CodeInsertions
{
	public class RemoveModifierFromFieldAction : CodeInsertionAction
	{
		public Field FieldToChange { get; set; }
		public string ModifierToRemove { get; set; }

		public RemoveModifierFromFieldAction(Field fieldToChange, string modifierToRemove)
		{
			FieldToChange = fieldToChange;
			ModifierToRemove = modifierToRemove;
		}

		public override ActionResult ApplyActionTo(StringBuilder sb)
		{
			int searchStart = FieldToChange.TextRange.StartOffset;
			int searchEnd = FieldToChange.TextRange.EndOffset;
			string text = sb.ToString().Substring(searchStart, searchEnd - searchStart);

			// Trim to just before the name of the field.
			var nameIndex = text.IndexOf(FieldToChange.Name);
			text = text.Substring(0, nameIndex);

			// Remove each instance of the modifier before the name
			// as well as the whitespace that comes just after it.
			Regex removalRegex = new Regex(ModifierToRemove + @"\s+");
			var newText = removalRegex.Replace(text, "");

			sb.Remove(searchStart, nameIndex);
			sb.Insert(searchStart, newText);

			// Remove the modifier from the field object.
			FieldToChange.Modifiers.RemoveAll(s => s == ModifierToRemove);

			return new ActionResult(searchStart, newText.Length - nameIndex, null);
		}
	}
}