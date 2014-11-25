using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActiproSoftware.SyntaxEditor;

namespace ArchAngel.Providers.CodeProvider.CodeInsertions
{
	public class Actions
	{
		private readonly List<CodeInsertionAction> ActionsToPerform = new List<CodeInsertionAction>();

		public void AddAction(CodeInsertionAction action)
		{
			ActionsToPerform.Add(action);
		}

		public string RunActions(CSharpParser parser)
		{
			return RunActions(parser.ActiproDocument.GetText(LineTerminator.Newline), parser.CreatedCodeRoot, false);
		}

		public string RunActions(string code, ICodeRoot codeRoot, bool standardiseLineBreaks)
		{
			if (standardiseLineBreaks)
				return RunActions(new StringBuilder(Helper.StandardizeLineBreaks(code, "\n")), codeRoot);

			return RunActions(new StringBuilder(code), codeRoot);
		}

		private string RunActions(StringBuilder builder, ICodeRoot codeRoot)
		{
			foreach (var action in ActionsToPerform)
			{
				var result = action.ApplyActionTo(builder);
				FixTextRanges(codeRoot, result);
			}

			return builder.ToString();
		}

		private void FixTextRanges(ICodeRoot root, ActionResult result)
		{
			if (!result.ChangeApplied ||
				result.NumCharactersInserted == 0)
			{
				return;
			}
			var constructs = root.WalkTree().ToList();

			foreach (var bc in constructs)
			{
				if (bc.TextRange.StartOffset > result.ChangeStartIndex)
				{
					bc.TextRange.StartOffset += result.NumCharactersInserted;
					bc.TextRange.EndOffset += result.NumCharactersInserted;
				}
				else if (bc.TextRange.StartOffset < result.ChangeStartIndex &&
					bc.TextRange.EndOffset > result.ChangeStartIndex)
					bc.TextRange.EndOffset += result.NumCharactersInserted;
				//else if (bc.TextRange.StartOffset == result.ChangeStartIndex)
				//    bc.TextRange.EndOffset += result.NumCharactersInserted; // GFH
			}
		}
	}

	public abstract class CodeInsertionAction
	{
		public abstract ActionResult ApplyActionTo(StringBuilder sb);
	}

	public class ActionResult
	{
		public readonly bool ChangeApplied;
		public readonly int ChangeStartIndex;
		public readonly int NumCharactersInserted;
		public readonly List<IBaseConstruct> ConstructsChanged = new List<IBaseConstruct>();

		public ActionResult()
		{
			ChangeApplied = false;
		}

		public ActionResult(int changeStartIndex, int numCharactersInserted, IEnumerable<IBaseConstruct> constructsToSkipTextRangeUpdating)
		{
			ChangeStartIndex = changeStartIndex;
			NumCharactersInserted = numCharactersInserted;

			if (constructsToSkipTextRangeUpdating != null)
				ConstructsChanged.AddRange(constructsToSkipTextRangeUpdating);

			ChangeApplied = true;
		}
	}
}
