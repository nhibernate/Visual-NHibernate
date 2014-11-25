using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;
using log4net;

namespace ArchAngel.Providers.CodeProvider.CodeInsertions
{
	public class ChangeReturnTypeOfFunctionAction : CodeInsertionAction
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(ChangeReturnTypeOfFunctionAction));
		public Function FunctionToChange { get; set; }
		public DataType NewType { get; set; }

		public ChangeReturnTypeOfFunctionAction(Function functionToChange, DataType newName)
		{
			FunctionToChange = functionToChange;
			NewType = newName;
		}

		public override ActionResult ApplyActionTo(StringBuilder sb)
		{
			if (FunctionToChange.ReturnType.ToString() == NewType.ToString())
				return new ActionResult();

			// Search FunctionToChange TextRange for name
			int searchStart = FunctionToChange.TextRange.StartOffset;
			int searchEnd = FunctionToChange.TextRange.EndOffset;
			string text = sb.ToString();

			int nameIndex = InsertionHelpers.GetFunctionNameIndex(text, FunctionToChange, searchStart, searchEnd);

			// The last "word" between the start of the function and the name is the return-type.
			var substring = text.Substring(searchStart, nameIndex - searchStart);
			int dataTypeStart = substring.IndexOf(FunctionToChange.ReturnType.ToString());
			var typeName = substring.Substring(dataTypeStart).TrimEnd();
			if (typeName == null)
			{
				log.ErrorFormat("Could not find type of function {0} to change.", FunctionToChange.Name);
				return new ActionResult();
			}

			// Find the index of the existing return-type
			int typeIndex = substring.LastIndexOf(typeName);
			int typeLength = typeName.Length;

			// Replace the old return-type with the new one.
			var newTypeName = NewType.ToString();
			sb.Replace(typeName, newTypeName, searchStart + typeIndex, typeLength);
			FunctionToChange.ReturnType = NewType;

			return new ActionResult(searchStart + typeIndex, newTypeName.Length - typeLength, null);
		}
	}
}