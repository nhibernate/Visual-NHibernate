using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;
using log4net;

namespace ArchAngel.Providers.CodeProvider.CodeInsertions
{
	public class AddModifierToPropertyAction : CodeInsertionAction
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(AddModifierToPropertyAction));

		public Property PropertyToChange { get; set; }
		public string NewModifier { get; set; }
		public bool InsertAtStart { get; set; }

		public AddModifierToPropertyAction(Property propertyToChange, string newModifier, bool insertAtStart)
		{
			PropertyToChange = propertyToChange;
			NewModifier = newModifier;
			InsertAtStart = insertAtStart;
		}

		public override ActionResult ApplyActionTo(StringBuilder sb)
		{
			int searchStart = PropertyToChange.TextRange.StartOffset;
			int searchEnd = PropertyToChange.TextRange.EndOffset;
			string text = sb.ToString();

			if (InsertAtStart)
			{
				sb.Insert(searchStart, NewModifier + " ");
				PropertyToChange.Modifiers.Insert(0, NewModifier);
				return new ActionResult(searchStart, NewModifier.Length + 1, null);
			}

			int nameIndex = InsertionHelpers.GetPropertyNameIndex(text, PropertyToChange, searchStart, searchEnd);

			// Search Property TextRange for class keyword
			// The last "word" between the start of the property and the name is the type.
			var substring = text.Substring(0, nameIndex);
			string typeName = InsertionHelpers.GetLastWord(substring);
			if (typeName == null)
			{
				log.ErrorFormat("Could not find type of property {0} to change, so can't insert modifier before it.", PropertyToChange.Name);
				return new ActionResult();
			}

			// Find the index of the existing type
			int typeIndex = substring.LastIndexOf(typeName);

			//Insert the new modifier just before the class keyword
			sb.Insert(typeIndex, NewModifier + " ");
			PropertyToChange.Modifiers.Add(NewModifier);

			return new ActionResult(typeIndex, NewModifier.Length + 1, null);
		}
	}
}