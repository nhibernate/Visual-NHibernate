using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ArchAngel.Providers.CodeProvider.DotNet;
using log4net;

namespace ArchAngel.Providers.CodeProvider.CodeInsertions
{
	public class ChangeNameOfPropertyAction : CodeInsertionAction
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(ChangeNameOfPropertyAction));
		public Property PropertyToChange { get; set; }
		public string NewName { get; set; }

		public ChangeNameOfPropertyAction(Property propertyToChange, string newName)
		{
			PropertyToChange = propertyToChange;
			NewName = newName;
		}

		public override ActionResult ApplyActionTo(StringBuilder sb)
		{
			if (PropertyToChange.Name == NewName)
				return new ActionResult();

			// Search PropertyToChange TextRange for name
			int searchStart = PropertyToChange.TextRange.StartOffset;
			int searchEnd = PropertyToChange.TextRange.EndOffset;
			string text = sb.ToString();

			int nameIndex = InsertionHelpers.GetPropertyNameIndex(text, PropertyToChange, searchStart, searchEnd);

			// The last "word" between the start of the property and the name is the type.
			var substring = text.Substring(nameIndex, searchEnd - nameIndex);
			var name = Regex.Split(substring, @"\s+").Where(s => !string.IsNullOrEmpty(s)).FirstOrDefault();
			//int dataTypeStart = substring.IndexOf(PropertyToChange.DataType.Name);
			//var name = substring.Substring(dataTypeStart).TrimEnd();

			if (string.IsNullOrEmpty(name))
			{
				log.ErrorFormat("Could not find name of property {0} to change.", PropertyToChange.Name);
				return new ActionResult();
			}

			// Find the index of the existing type
			int nameLength = name.Length;

			// Replace the old type with the new one.
			sb.Replace(name, NewName, nameIndex, nameLength);
			PropertyToChange.Name = NewName;

			return new ActionResult(searchStart + nameIndex, NewName.Length - nameLength, null);
		}
	}
}
