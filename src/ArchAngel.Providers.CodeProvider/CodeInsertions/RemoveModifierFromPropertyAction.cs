using System.Text;
using System.Text.RegularExpressions;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CodeInsertions
{
	public class RemoveModifierFromPropertyAction : CodeInsertionAction
	{
		public Property PropertyToChange { get; set; }
		public string ModifierToRemove { get; set; }

		public RemoveModifierFromPropertyAction(Property propertyToChange, string modifierToRemove)
		{
			PropertyToChange = propertyToChange;
			ModifierToRemove = modifierToRemove;
		}

		public override ActionResult ApplyActionTo(StringBuilder sb)
		{
			int searchStart = PropertyToChange.TextRange.StartOffset;
			int searchEnd = PropertyToChange.TextRange.EndOffset;
			string text = sb.ToString().Substring(searchStart, searchEnd-searchStart);

			// Trim to just before the name of the property.
			var nameIndex = text.IndexOf(PropertyToChange.Name);
			text = text.Substring(0, nameIndex);

			//Remove each instance of the modifier before the name
			// as well as the whitespace that comes just after it.
			Regex removalRegex = new Regex(ModifierToRemove + @"\s+");
			var newText = removalRegex.Replace(text, "");

			sb.Remove(searchStart, nameIndex);
			sb.Insert(searchStart, newText);

			// Remove the modifier from the property object.
			PropertyToChange.Modifiers.RemoveAll(s => s == ModifierToRemove);

			return new ActionResult(searchStart, newText.Length - nameIndex, null);
		}
	}
}