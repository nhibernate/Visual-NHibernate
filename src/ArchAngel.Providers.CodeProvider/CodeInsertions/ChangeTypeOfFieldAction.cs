using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;
using log4net;

namespace ArchAngel.Providers.CodeProvider.CodeInsertions
{
	public class ChangeTypeOfFieldAction : CodeInsertionAction
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(ChangeTypeOfFieldAction));
		public Field FieldToChange { get; set; }
		public DataType NewType { get; set; }

		public ChangeTypeOfFieldAction(Field fieldToChange, DataType newName)
		{
			FieldToChange = fieldToChange;
			NewType = newName;
		}

		public override ActionResult ApplyActionTo(StringBuilder sb)
		{
			if (FieldToChange.DataType.ToString() == NewType.ToString())
				return new ActionResult();

			// Search FieldToChange TextRange for name
			int searchStart = FieldToChange.TextRange.StartOffset;
			int searchEnd = FieldToChange.TextRange.EndOffset;
			string text = sb.ToString();

			int nameIndex = InsertionHelpers.GetFieldNameIndex(text, FieldToChange, searchStart, searchEnd);

			// The last "word" between the start of the field and the name is the type.
			var substring = text.Substring(searchStart, nameIndex - searchStart);
			int dataTypeStart = substring.IndexOf(FieldToChange.DataType.ToString());
			var typeName = substring.Substring(dataTypeStart).TrimEnd();// FieldToChange.DataType.ToString();
			if (typeName == null)
			{
				log.ErrorFormat("Could not find type of field {0} to change.", FieldToChange.Name);
				return new ActionResult();
			}

			// Find the index of the existing type
			int typeIndex = substring.LastIndexOf(typeName);
			int typeLength = typeName.Length;

			// Replace the old type with the new one.
			var newTypeName = NewType.ToString();
			sb.Replace(typeName, newTypeName, searchStart + typeIndex, typeLength);
			FieldToChange.DataType = NewType;

			return new ActionResult(searchStart + typeIndex, newTypeName.Length - typeLength, null);
		}
	}
}