using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CodeInsertions
{
	public class ChangeMethodHeaderAction : CodeInsertionAction
	{
		public Function MethodToChange { get; set; }
		public Function NewMethodInfo { get; set; }

		public ChangeMethodHeaderAction(Function methodToChange, Function newMethodInfo)
		{
			MethodToChange = methodToChange;
			NewMethodInfo = newMethodInfo;
		}

		public override ActionResult ApplyActionTo(StringBuilder sb)
		{
			int searchStart = MethodToChange.TextRange.StartOffset;
			int searchEnd = MethodToChange.TextRange.EndOffset;
			string text = sb.ToString();

			// Get the text range of the whole method header
			// It goes from searchStart to just after the )
			int firstParenIndex = text.IndexOf(")", searchStart, searchEnd - searchStart) + 1;

			// Get the new method header text from the new method information
			string newMethodHeaderText = NewMethodInfo.GetOuterText().Trim(' ', '\t', '\r', '\n');

			// Remove the old header and place the new one in.
			var oldLength = firstParenIndex - searchStart;
			sb.Remove(searchStart, oldLength);
			sb.Insert(searchStart, newMethodHeaderText);

			return  new ActionResult(searchStart, newMethodHeaderText.Length - oldLength, null);
		}
	}
}