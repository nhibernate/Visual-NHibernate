using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CodeInsertions
{
	public class AddModifierToClassAction : CodeInsertionAction
	{
		public Class ClassToChange { get; set; }
		public string NewModifier { get; set; }
		public bool InsertAtStart { get; set; }

		public AddModifierToClassAction(Class classToChange, string newModifier, bool insertAtStart)
		{
			ClassToChange = classToChange;
			NewModifier = newModifier;
			InsertAtStart = insertAtStart;
		}

		public override ActionResult ApplyActionTo(StringBuilder sb)
		{
			int searchStart = ClassToChange.TextRange.StartOffset;
			int searchEnd = ClassToChange.TextRange.EndOffset;
			string text = sb.ToString();

			if (InsertAtStart)
			{
				sb.Insert(searchStart, NewModifier + " ");
				ClassToChange.Modifiers.Insert(0, NewModifier);
				return new ActionResult(searchStart, NewModifier.Length+1, null);
			}

			// Search Class TextRange for class keyword
			int classIndex = InsertionHelpers.GetClassKeywordIndex(text, searchStart, searchEnd);

			//Insert the new modifier just before the class keyword
			sb.Insert(classIndex, NewModifier + " ");
			ClassToChange.Modifiers.Add(NewModifier);

			return new ActionResult(classIndex, NewModifier.Length + 1, null);
		}
	}
}