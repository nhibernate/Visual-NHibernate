using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;
using log4net;

namespace ArchAngel.Providers.CodeProvider.CodeInsertions
{
	public class ChangeInitialValueOfFieldAction : CodeInsertionAction
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(ChangeInitialValueOfFieldAction));

		public Field FieldToChange { get; set; }
		public string NewInitialValue { get; set; }

		public ChangeInitialValueOfFieldAction(Field fieldToChange, string newInitialValue)
		{
			FieldToChange = fieldToChange;
			NewInitialValue = newInitialValue;
		}

		public override ActionResult ApplyActionTo(StringBuilder sb)
		{
			int searchStart = FieldToChange.TextRange.StartOffset;
			int searchEnd = FieldToChange.TextRange.EndOffset;
			string text = sb.ToString();

			int nameIndex = InsertionHelpers.GetFieldNameIndex(text, FieldToChange, searchStart, searchEnd);
			int endOfNameIndex = nameIndex + FieldToChange.Name.Length + 1;

			// Find the old initial value and remove it, if it exists
			int lengthRemoved = searchEnd - endOfNameIndex;

			sb.Remove(endOfNameIndex, lengthRemoved);
			string textToInsert = "";

			if (!string.IsNullOrWhiteSpace(NewInitialValue))
			{
				textToInsert = string.Format(" = {0};", NewInitialValue);
				sb.Insert(endOfNameIndex + 1, textToInsert);
			}
			// Add the new modifier only if it is not null
			if (string.IsNullOrEmpty(NewInitialValue))
			{
				FieldToChange.InitialValue = "";
				return new ActionResult(searchStart, -lengthRemoved, null);
			}

			sb.Insert(searchStart, NewInitialValue + " ");
			FieldToChange.InitialValue = NewInitialValue;
			return new ActionResult(searchStart, textToInsert.Length + 1 - lengthRemoved, null);
		}
	}
}