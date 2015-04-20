using System.Linq;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;
using ArchAngel.Providers.CodeProvider.Extensions.IEnumerableExtensions;
using log4net;

namespace ArchAngel.Providers.CodeProvider.CodeInsertions
{
	public class RemoveAccessorFromPropertyAction : CodeInsertionAction
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(AddModifierToPropertyAction));

		public Property PropertyToChange { get; set; }
		public PropertyAccessor AccessorToRemove { get; set; }

		public RemoveAccessorFromPropertyAction(Property propertyToChange, PropertyAccessor accessorToRemove)
		{
			PropertyToChange = propertyToChange;
			AccessorToRemove = accessorToRemove;
		}

		public override ActionResult ApplyActionTo(StringBuilder sb)
		{
			int searchStart = AccessorToRemove.TextRange.StartOffset;
			int searchEnd = AccessorToRemove.TextRange.EndOffset;
			var text = sb.ToString();

			int startOfWhitespace = text.LastIndexOf("\n", searchStart, searchStart - 1);
			if (startOfWhitespace != -1)
			{
				foreach (var c in text.Substring(startOfWhitespace, searchStart - startOfWhitespace).WithIndices().Reverse())
				{
					if (char.IsWhiteSpace(c.Value) == false)
					{
						// The +1 is so we jump one past the current character, which is the first non whitespace char
						// in the substring
						startOfWhitespace = c.Index + startOfWhitespace + 1;
						break;
					}
				}

				searchStart = startOfWhitespace;
			}

			sb.Remove(searchStart, searchEnd - searchStart);

			PropertyToChange.RemoveChildObject(AccessorToRemove);

			return new ActionResult(searchStart, searchStart - searchEnd, null);
		}
	}
}