using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CodeInsertions
{
	public class ChangeMethodBodyAction : CodeInsertionAction
	{
		public Function MethodToChange { get; set; }
		public Function NewMethodInfo { get; set; }

		public ChangeMethodBodyAction(Function methodToChange, Function newMethodInfo)
		{
			MethodToChange = methodToChange;
			NewMethodInfo = newMethodInfo;
		}

		public override ActionResult ApplyActionTo(StringBuilder sb)
		{
			int searchStart = MethodToChange.TextRange.StartOffset;
			int searchEnd = MethodToChange.TextRange.EndOffset;
			string text = Helper.StandardizeLineBreaks(sb.ToString(), Helper.LineBreaks.Unix);

			// Get the text range of the whole method header
			// It goes from searchStart to just after the )
			int firstParenIndex = searchStart;// text.IndexOf("{", searchStart, searchEnd - searchStart);

			// Get the new method header text from the new method information
			NewMethodInfo.Controller.IndentLevel = 0;

			for (int i = 0; i < NewMethodInfo.BodyText.Length; i++)
			{
				if (NewMethodInfo.BodyText[i] == '\t' || NewMethodInfo.BodyText[i] == ' ')
					NewMethodInfo.Controller.IndentLevel++;
				else
					break;
			}
			string newMethodHeaderText = Helper.StandardizeLineBreaks(NewMethodInfo.ToString().Trim(), Helper.LineBreaks.Unix);

			// Remove the old header and place the new one in.
			var oldLength = searchEnd - searchStart;
			sb.Remove(firstParenIndex, oldLength);
			sb.Insert(firstParenIndex, newMethodHeaderText);

			int numCharactersInserted = newMethodHeaderText.Length - oldLength;
			MethodToChange.TextRange.EndOffset += numCharactersInserted;

			return new ActionResult(firstParenIndex, numCharactersInserted, null);
		}
	}
}