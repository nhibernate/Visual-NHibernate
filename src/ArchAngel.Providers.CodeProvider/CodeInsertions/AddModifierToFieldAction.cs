using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;
using log4net;

namespace ArchAngel.Providers.CodeProvider.CodeInsertions
{
	public class AddModifierToFieldAction : CodeInsertionAction
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(AddModifierToFieldAction));

		public Field FieldToChange { get; set; }
		public string NewModifier { get; set; }
		public bool InsertAtStart { get; set; }

		public AddModifierToFieldAction(Field fieldToChange, string newModifier, bool insertAtStart)
		{
			FieldToChange = fieldToChange;
			NewModifier = newModifier;
			InsertAtStart = insertAtStart;
		}

		public override ActionResult ApplyActionTo(StringBuilder sb)
		{
			int searchStart = FieldToChange.TextRange.StartOffset;
			int searchEnd = FieldToChange.TextRange.EndOffset;
			string text = sb.ToString();

			if (InsertAtStart)
			{
				sb.Insert(searchStart, NewModifier + " ");
				FieldToChange.Modifiers.Insert(0, NewModifier);
				return new ActionResult(searchStart, NewModifier.Length + 1, null);
			}

			int nameIndex = InsertionHelpers.GetFieldNameIndex(text, FieldToChange, searchStart, searchEnd);

			// Search Field TextRange for class keyword
			// The last "word" between the start of the field and the name is the type.
			var substring = text.Substring(0, nameIndex);
			string typeName = InsertionHelpers.GetLastWord(substring);
			if (typeName == null)
			{
				log.ErrorFormat("Could not find type of property {0} to change, so can't insert modifier before it.", FieldToChange.Name);
				return new ActionResult();
			}

			// Find the index of the existing type
			int typeIndex = substring.LastIndexOf(typeName);

			//Insert the new modifier just before the class keyword
			sb.Insert(typeIndex, NewModifier + " ");
			FieldToChange.Modifiers.Add(NewModifier);

			return new ActionResult(typeIndex, NewModifier.Length + 1, null);
		}
	}
}