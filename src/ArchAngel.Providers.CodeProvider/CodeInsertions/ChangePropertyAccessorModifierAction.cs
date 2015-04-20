using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;
using log4net;

namespace ArchAngel.Providers.CodeProvider.CodeInsertions
{
	public class ChangePropertyAccessorModifierAction : CodeInsertionAction
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(ChangePropertyAccessorModifierAction));

		public PropertyAccessor AccessorToChange { get; set; }
		public string NewModifier { get; set; }

		public ChangePropertyAccessorModifierAction(PropertyAccessor propertyToChange, string newModifier)
		{
			AccessorToChange = propertyToChange;
			NewModifier = newModifier;
		}

		public override ActionResult ApplyActionTo(StringBuilder sb)
		{
			int searchStart = AccessorToChange.TextRange.StartOffset;
			int searchEnd = AccessorToChange.TextRange.EndOffset;
			string text = sb.ToString();

			// Find the old text and remove it, if it exists
			string keyword = AccessorToChange.AccessorType == PropertyAccessor.AccessorTypes.Get
			                 	? "get" : "set";

			int indexOfKeyword = text.IndexOf(keyword, searchStart, searchEnd-searchStart);

			int lengthOfOldModifier = indexOfKeyword - searchStart;
			sb.Remove(searchStart, lengthOfOldModifier);

			// Add the new modifier only if it is not null
			if (string.IsNullOrEmpty(NewModifier))
			{
				AccessorToChange.Modifier = "";
				return new ActionResult(searchStart, -lengthOfOldModifier, null);
			}
			
			sb.Insert(searchStart, NewModifier + " ");
			AccessorToChange.Modifier = NewModifier;
			return new ActionResult(searchStart, NewModifier.Length + 1 - lengthOfOldModifier, null);
		}
	}
}