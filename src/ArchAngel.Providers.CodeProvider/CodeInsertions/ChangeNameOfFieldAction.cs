using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ArchAngel.Providers.CodeProvider.DotNet;
using log4net;

namespace ArchAngel.Providers.CodeProvider.CodeInsertions
{
	public class ChangeNameOfFieldAction : CodeInsertionAction
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(ChangeNameOfFieldAction));
		public Field FieldToChange { get; set; }
		public string NewName { get; set; }

		public ChangeNameOfFieldAction(Field fieldToChange, string newName)
		{
			FieldToChange = fieldToChange;
			NewName = newName;
		}

		public override ActionResult ApplyActionTo(StringBuilder sb)
		{
			if (FieldToChange.Name == NewName)
				return new ActionResult();

			// Search FieldToChange TextRange for name
			int searchStart = FieldToChange.TextRange.StartOffset;
			int searchEnd = FieldToChange.TextRange.EndOffset;
			string text = sb.ToString();

			int nameIndex = InsertionHelpers.GetFieldNameIndex(text, FieldToChange, searchStart, searchEnd);

			// The last "word" between the start of the property and the name is the type.
			var substring = text.Substring(nameIndex, searchEnd - nameIndex);
			var name = Regex.Split(substring, @"\s+").Where(s => !string.IsNullOrEmpty(s)).FirstOrDefault();
			//int dataTypeStart = substring.IndexOf(PropertyToChange.DataType.Name);
			//var name = substring.Substring(dataTypeStart).TrimEnd();

			if (string.IsNullOrEmpty(name))
			{
				log.ErrorFormat("Could not find name of property {0} to change.", FieldToChange.Name);
				return new ActionResult();
			}

			// Find the index of the existing type
			int nameLength = name.Length;

			// Replace the old type with the new one.
			sb.Replace(name, NewName, nameIndex, nameLength);
			FieldToChange.Name = NewName;

			return new ActionResult(searchStart + nameIndex, NewName.Length - nameLength, null);
		}
	}
}
