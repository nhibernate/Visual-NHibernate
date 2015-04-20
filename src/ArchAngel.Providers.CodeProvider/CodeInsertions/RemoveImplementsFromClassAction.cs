using System;
using System.Text;
using System.Text.RegularExpressions;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CodeInsertions
{
	public class RemoveImplementsFromClassAction : CodeInsertionAction
	{
		public Class ClassToChange { get; set; }
		public string ImplementToRemove { get; set; }

		public RemoveImplementsFromClassAction(Class classToChange, string implementToRemove)
		{
			ClassToChange = classToChange;
			ImplementToRemove = implementToRemove;
		}

		public override ActionResult ApplyActionTo(StringBuilder sb)
		{
			int searchStart = ClassToChange.TextRange.StartOffset;
			int searchEnd = ClassToChange.TextRange.EndOffset;
			string text = sb.ToString();

			// Find the :
			int colonIndex = text.IndexOf(":", searchStart, searchEnd - searchStart);

			// Find the first brace after that
			int firstBraceIndex = text.IndexOf("{", colonIndex, searchEnd - colonIndex);

			// Trim to just the text between the : and {
			text = text.Substring(colonIndex, firstBraceIndex - colonIndex);

			// Remove each instance of the implement text between the : and {
			// as well as the whitespace and comma that comes just after it.
			Regex removalRegex = new Regex(@"[\s,]+" + ImplementToRemove);
			var newText = removalRegex.Replace(text, "");

			// Remove the implement from the class object.
			ClassToChange.BaseNames.RemoveAll(s => s == ImplementToRemove);

			if(ClassToChange.BaseNames.Count == 0)
			{
				// Remove the colon as well by moving the start of the removal
				// to just before it.
				colonIndex--;
				int numTabs = InsertionHelpers.GetIndentationInFrontOf(sb, searchStart);
				newText = Environment.NewLine + new string('\t', numTabs);
			}

			sb.Remove(colonIndex, firstBraceIndex-colonIndex);
			sb.Insert(colonIndex, newText);

			return new ActionResult(colonIndex, newText.Length - (firstBraceIndex - colonIndex), null);
		}
	}
}