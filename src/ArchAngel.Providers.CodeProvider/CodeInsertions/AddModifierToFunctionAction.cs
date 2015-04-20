using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;
using log4net;

namespace ArchAngel.Providers.CodeProvider.CodeInsertions
{
	public class AddModifierToFunctionAction : CodeInsertionAction
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(AddModifierToFunctionAction));

		public Function FunctionToChange { get; set; }
		public string NewModifier { get; set; }
		public bool InsertAtStart { get; set; }

		public AddModifierToFunctionAction(Function functionToChange, string newModifier, bool insertAtStart)
		{
			FunctionToChange = functionToChange;
			NewModifier = newModifier;
			InsertAtStart = insertAtStart;
		}

		public override ActionResult ApplyActionTo(StringBuilder sb)
		{
			int searchStart = FunctionToChange.TextRange.StartOffset;
			int searchEnd = FunctionToChange.TextRange.EndOffset;
			string text = sb.ToString();

			if (InsertAtStart)
			{
				sb.Insert(searchStart, NewModifier + " ");
				FunctionToChange.Modifiers.Insert(0, NewModifier);
				return new ActionResult(searchStart, NewModifier.Length + 1, null);
			}

			int nameIndex = InsertionHelpers.GetFunctionNameIndex(text, FunctionToChange, searchStart, searchEnd);

			// Search Function TextRange for class keyword
			// The last "word" between the start of the function and the name is the return-type.
			var substring = text.Substring(0, nameIndex);
			string typeName = InsertionHelpers.GetLastWord(substring);
			if (typeName == null)
			{
				log.ErrorFormat("Could not find return-type of function {0} to change, so can't insert modifier before it.", FunctionToChange.Name);
				return new ActionResult();
			}

			// Find the index of the existing type
			int typeIndex = substring.LastIndexOf(typeName);

			//Insert the new modifier just before the class keyword
			sb.Insert(typeIndex, NewModifier + " ");
			FunctionToChange.Modifiers.Add(NewModifier);

			return new ActionResult(typeIndex, NewModifier.Length + 1, null);
		}
	}
}