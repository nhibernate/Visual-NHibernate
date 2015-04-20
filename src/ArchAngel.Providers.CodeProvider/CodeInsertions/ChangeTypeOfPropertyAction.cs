using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;
using log4net;

namespace ArchAngel.Providers.CodeProvider.CodeInsertions
{
	public class ChangeTypeOfPropertyAction : CodeInsertionAction
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(ChangeTypeOfPropertyAction));
		public Property PropertyToChange { get; set; }
		public DataType NewType { get; set; }

		public ChangeTypeOfPropertyAction(Property propertyToChange, DataType newName)
		{
			PropertyToChange = propertyToChange;
			NewType = newName;
		}

		public override ActionResult ApplyActionTo(StringBuilder sb)
		{
			if (PropertyToChange.DataType.ToString() == NewType.ToString())
				return new ActionResult();

			// Search PropertyToChange TextRange for name
			int searchStart = PropertyToChange.TextRange.StartOffset;
			int searchEnd = PropertyToChange.TextRange.EndOffset;
			string text = sb.ToString();

			int nameIndex = InsertionHelpers.GetPropertyNameIndex(text, PropertyToChange, searchStart, searchEnd);

			// The last "word" between the start of the property and the name is the type.
			var substring = text.Substring(searchStart, nameIndex - searchStart);
			int dataTypeStart = substring.IndexOf(PropertyToChange.DataType.ToString());
			var typeName = substring.Substring(dataTypeStart).TrimEnd();// PropertyToChange.DataType.ToString();
			if (typeName == null)
			{
				log.ErrorFormat("Could not find type of property {0} to change.", PropertyToChange.Name);
				return new ActionResult();
			}

			// Find the index of the existing type
			int typeIndex = substring.LastIndexOf(typeName);
			int typeLength = typeName.Length;

			// Replace the old type with the new one.
			var newTypeName = NewType.ToString();
			sb.Replace(typeName, newTypeName, searchStart + typeIndex, typeLength);
			PropertyToChange.DataType = NewType;

			return new ActionResult(searchStart + typeIndex, newTypeName.Length - typeLength, null);
		}
	}
}