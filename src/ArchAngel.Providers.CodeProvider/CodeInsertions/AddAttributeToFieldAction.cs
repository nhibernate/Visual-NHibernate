using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;
using Attribute = ArchAngel.Providers.CodeProvider.DotNet.Attribute;

namespace ArchAngel.Providers.CodeProvider.CodeInsertions
{
	public class AddAttributeToFieldAction : CodeInsertionAction
	{
		public Field FieldToAddTo { get; set; }
		public Attribute AttributeToAdd { get; set; }

		public AddAttributeToFieldAction(Field fieldToAddTo, Attribute attributeToAdd)
		{
			FieldToAddTo = fieldToAddTo;
			AttributeToAdd = attributeToAdd;
		}

		public override ActionResult ApplyActionTo(StringBuilder sb)
		{
			int startSearch = FieldToAddTo.TextRange.StartOffset;
			string text = sb.ToString();

			// Put the new Attribute just in front of the property declaration.
			int insertionPoint = text.LastIndexOf("\n", startSearch) + 1;

			AttributeSection section = new AttributeSection(AttributeToAdd.Controller);
			section.AddAttribute(AttributeToAdd);
			section.PreceedingBlankLines = -1;
			var indentLevel = InsertionHelpers.GetIndentationInFrontOf(text, startSearch);
			section.Controller.IndentLevel = indentLevel;

			string newText = Helper.StandardizeLineBreaks(section.ToString(), "\n") + "\n";

			// Calculate the Attribute's Text Range
			// The start index is the insertion point + the number of tabs in front of the attribute, +1 for the \n
			AttributeToAdd.TextRange.StartOffset = insertionPoint + indentLevel + 1;
			AttributeToAdd.TextRange.EndOffset = AttributeToAdd.TextRange.StartOffset + (newText.Trim()).Length;

			sb.Insert(insertionPoint, newText);

			FieldToAddTo.AttributeSections.Add(section);

			//return new ActionResult(insertionPoint, newText.Length, new[] { AttributeToAdd });
			return new ActionResult(AttributeToAdd.TextRange.StartOffset, newText.Length, new[] { AttributeToAdd });
		}
	}
}