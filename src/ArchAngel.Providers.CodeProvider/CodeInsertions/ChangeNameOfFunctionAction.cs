using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ArchAngel.Providers.CodeProvider.DotNet;
using log4net;

namespace ArchAngel.Providers.CodeProvider.CodeInsertions
{
	public class ChangeNameOfFunctionAction : CodeInsertionAction
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(ChangeNameOfFunctionAction));
		public Function FunctionToChange { get; set; }
		public string NewName { get; set; }

		public ChangeNameOfFunctionAction(Function functionToChange, string newName)
		{
			FunctionToChange = functionToChange;
			NewName = newName;
		}

		public override ActionResult ApplyActionTo(StringBuilder sb)
		{
			if (FunctionToChange.Name == NewName)
				return new ActionResult();

			// Search FunctionToChange TextRange for name
			int searchStart = FunctionToChange.TextRange.StartOffset;
			int searchEnd = FunctionToChange.TextRange.EndOffset;
			string text = sb.ToString();

			int nameIndex = InsertionHelpers.GetFunctionNameIndex(text, FunctionToChange, searchStart, searchEnd);

			// The last "word" between the start of the property and the name is the type.
			var substring = text.Substring(nameIndex, searchEnd - nameIndex);
			var name = Regex.Split(substring, @"\s+").Where(s => !string.IsNullOrEmpty(s)).FirstOrDefault();

			if (string.IsNullOrEmpty(name))
			{
				log.ErrorFormat("Could not find name of function {0} to change.", FunctionToChange.Name);
				return new ActionResult();
			}

			// Find the index of the existing type
			int nameLength = name.Length;

			// Replace the old type with the new one.
			sb.Replace(name, NewName, nameIndex, nameLength);
			FunctionToChange.Name = NewName;

			return new ActionResult(searchStart + nameIndex, NewName.Length - nameLength, null);
		}
	}
}
