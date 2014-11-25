using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CodeInsertions
{
	public class RenameBaseConstructAction : CodeInsertionAction
	{
		public BaseConstruct PropertyToChange { get; set; }
		public string NewName { get; set; }

		public RenameBaseConstructAction(BaseConstruct propertyToChange, string newName)
		{
			PropertyToChange = propertyToChange;
			NewName = newName;
		}

		public override ActionResult ApplyActionTo(StringBuilder sb)
		{
			// Search PropertyToChange TextRange for name
			int searchStart = PropertyToChange.TextRange.StartOffset;
			int searchEnd = PropertyToChange.TextRange.EndOffset;
			string text = sb.ToString();

			int nameIndex = text.IndexOf(PropertyToChange.Name, searchStart, searchEnd - searchStart);
			int nameLength = PropertyToChange.Name.Length;

			sb.Replace(PropertyToChange.Name, NewName, nameIndex, nameLength);
			PropertyToChange.Name = NewName;

			return new ActionResult(nameIndex, NewName.Length - nameLength, null);
		}
	}
}