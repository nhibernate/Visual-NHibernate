using System.Linq;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;
using log4net;
using ArchAngel.Providers.CodeProvider.Extensions.IEnumerableExtensions;

namespace ArchAngel.Providers.CodeProvider.CodeInsertions
{
	public class AddImplementsToClassAction : CodeInsertionAction
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(AddModifierToPropertyAction));

		public Class ClassToChange { get; set; }
		public DataType NewImplementor { get; set; }
		public bool InsertAtBeginning { get; set; }

		public AddImplementsToClassAction(Class classToChange, DataType newImplements, bool insertAtBeginning)
		{
			ClassToChange = classToChange;
			NewImplementor = newImplements;
			InsertAtBeginning = insertAtBeginning;
		}

		public override ActionResult ApplyActionTo(StringBuilder sb)
		{
			int searchStart = ClassToChange.TextRange.StartOffset;
			int searchEnd = ClassToChange.TextRange.EndOffset;
			string text = sb.ToString();

			// Find the class name
			int classNameIndex = text.IndexOf(ClassToChange.Name, searchStart, searchEnd - searchStart);

			// Find the first brace after that
			int firstBraceIndex = text.IndexOf("{", classNameIndex, searchEnd - classNameIndex);

			string textToSearch = text.Substring(classNameIndex, firstBraceIndex - classNameIndex);

			var implementorText = NewImplementor.ToString();

			string implementsText = "";
			int insertionIndex = 0;

			// Get the new text and insertion point. 
			// If the class doesn't already implement something, we need to add the :
			if (ClassToChange.BaseNames.Count == 0)
			{
				implementsText = " : " + implementorText;
				insertionIndex = classNameIndex + ClassToChange.Name.Length;
			}
			else if (InsertAtBeginning)
			{
				implementsText = implementorText + ", ";

				// Search for the first type name that is implemented.
				insertionIndex = ClassToChange.BaseNames
					.Select(s => new IndexedItem<string>(
									textToSearch.IndexOf(s), s))
					.OrderBy(i => i.Index)
					.First()
					.Index;

				insertionIndex += classNameIndex;
			}
			else
			{
				implementsText = ", " + implementorText;

				// Search for the last type name that is implemented.
				insertionIndex = ClassToChange.BaseNames
					.Select(s => new IndexedItem<string>(
									textToSearch.LastIndexOf(s, textToSearch.Length, textToSearch.Length) + s.Length, s))
					.OrderByDescending(i => i.Index)
					.First()
					.Index;

				insertionIndex += classNameIndex;
			}

			// Insert the new text.
			sb.Insert(insertionIndex, implementsText);

			// Add the type to the implemented classes/interfaces collection.
			ClassToChange.BaseNames.Add(implementorText);

			return new ActionResult(insertionIndex, implementsText.Length, null);
		}
	}
}