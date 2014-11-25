using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CodeInsertions
{
	public class AddUsingStatementToClassAction : CodeInsertionAction
	{
		public Class ClassToAddTo { get; set; }
		public UsingStatement UsingStatementToAdd { get; set; }

		public AddUsingStatementToClassAction(Class classToAddTo, UsingStatement usingStatementToAdd)
		{
			ClassToAddTo = classToAddTo;
			UsingStatementToAdd = usingStatementToAdd;
		}

		public override ActionResult ApplyActionTo(StringBuilder sb)
		{
			int startSearch;

			if (ClassToAddTo.Controller.Root.UsingStatements.Count == 0)
				startSearch = 0;
			else
				startSearch = ClassToAddTo.Controller.Root.UsingStatements[ClassToAddTo.Controller.Root.UsingStatements.Count - 1].TextRange.StartOffset;

			string text = sb.ToString();

			// Put the new Attribute just in front of the class declaration.
			int insertionPoint = text.LastIndexOf("\n", startSearch) + 1;
			string newText = Helper.StandardizeLineBreaks(string.Format("using {0};", UsingStatementToAdd), "\n") + "\n";

			// Calculate the Attribute's Text Range
			// The start index is the insertion point + the number of tabs in front of the attribute, +1 for the \n
			UsingStatementToAdd.TextRange.StartOffset = insertionPoint + 1;// +indentLevel + 1;
			UsingStatementToAdd.TextRange.EndOffset = UsingStatementToAdd.TextRange.StartOffset + (newText.Trim()).Length;

			sb.Insert(insertionPoint, newText);

			ClassToAddTo.Controller.Root.UsingStatements.Add(UsingStatementToAdd);

			//return new ActionResult(insertionPoint, newText.Length, new[] { UsingStatementToAdd });
			return new ActionResult(UsingStatementToAdd.TextRange.StartOffset, newText.Length, new[] { UsingStatementToAdd });
		}
	}
}