using System.Linq;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CodeInsertions
{
	public class RemoveAttributeFromClassAction : CodeInsertionAction
	{
		static readonly char[] WhitespaceChars = new char[] { ' ', '\t', '\r', '\n' };
		public Attribute AttributeToRemove { get; set; }

		public RemoveAttributeFromClassAction(Attribute attributeToRemove)
		{
			AttributeToRemove = attributeToRemove;
		}

		public override ActionResult ApplyActionTo(StringBuilder sb)
		{
			int searchStart = AttributeToRemove.TextRange.StartOffset;
			int searchEnd = AttributeToRemove.TextRange.EndOffset;

			// Also remove all characters up to next line break
			for (int i = searchEnd; i < sb.Length - 1; i++)
			{
				char character = sb[i];

				if (!WhitespaceChars.Contains(character))
					break;

				if (character == '\r')
				{
					if (i + 1 < sb.Length &&
						sb[i + 1] == '\n')
					{
						searchEnd = i + 1;
						break;
					}
					else
					{
						searchEnd = i;
						break;
					}
				}
				else if (character == '\n')
				{
					searchEnd = i;
					break;
				}
			}
			// Also remove all characters up to previous line break
			if (searchStart > 0)
			{
				for (int i = searchStart - 1; i >= 0; i--)
				{
					char character = sb[i];

					if (!WhitespaceChars.Contains(character))
						break;

					if (character == '\n')
					{
						if (i - 1 > 0 &&
							sb[i - 1] == '\r')
						{
							searchStart = i - 1;
							break;
						}
						else
						{
							if (i > 0)
								searchStart = i;

							break;
						}
					}
					else if (character == '\r')
					{
						if (i > 0)
							searchStart = i;

						break;
					}
				}
			}
			sb.Remove(searchStart, searchEnd - searchStart);
			return new ActionResult(searchStart, -1 * (searchEnd - searchStart), null);
		}
	}
}