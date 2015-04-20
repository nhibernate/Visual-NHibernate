using System;
using System.Collections.Generic;
using ArchAngel.Designer.DesignerProject;

namespace ArchAngel.Designer
{
	internal struct ColumnInfo
	{
		public readonly int LineNumber, TemplateColumnOffset, CompiledColumnOffset, SnippetLength;

		public ColumnInfo(int lineNumber, int templateColumnOffset, int compiledColumnOffset, int snippetLength)
		{
			LineNumber = lineNumber;
			TemplateColumnOffset = templateColumnOffset;
			CompiledColumnOffset = compiledColumnOffset;
			SnippetLength = snippetLength;
		}
	}


	public struct CompiledToTemplateLineLookup
	{
		public readonly FunctionInfo Function;
		public readonly int TemplateLineNumber;
		public readonly int TemplateColumn;
		public readonly int CompiledColumn;
		public readonly int SnippetLength;


		public CompiledToTemplateLineLookup(FunctionInfo function, int templateLineNumber, int templateColumn, int compiledColumn, int snippetLength)
		{
			Function = function;
			TemplateLineNumber = templateLineNumber;
			TemplateColumn = templateColumn;
			CompiledColumn = compiledColumn;
			SnippetLength = snippetLength;
		}
	}

	public struct TemplateToCompiledLineLookup
	{
		public readonly int CompiledLineNumber;
		public readonly int TemplateColumn;
		public readonly int CompiledColumn;

		public TemplateToCompiledLineLookup(int compiledLineNumber, int templateColumn, int compiledColumn)
		{
			CompiledLineNumber = compiledLineNumber;
			TemplateColumn = templateColumn;
			CompiledColumn = compiledColumn;
		}
	}

	public class CompiledLineNumberMap
	{
		private readonly Dictionary<int, Dictionary<FunctionInfo, List<TemplateToCompiledLineLookup>>> _Dictionary;
		private readonly int _StartingCapacity;

		public CompiledLineNumberMap(int startingCapacity)
		{
			_Dictionary = new Dictionary<int, Dictionary<FunctionInfo, List<TemplateToCompiledLineLookup>>>();
			_StartingCapacity = startingCapacity;
		}

		internal bool ContainsKey(FunctionInfo function, int origLineNum)
		{
			if (_Dictionary.ContainsKey(origLineNum))
			{
				if (_Dictionary[origLineNum].ContainsKey(function))
				{
					return true;
				}
			}
			return false;
		}

		public List<TemplateToCompiledLineLookup> this[FunctionInfo function, int origLineNum]
		{
			get
			{
				if (ContainsKey(function, origLineNum))
				{
					return _Dictionary[origLineNum][function];
				}
				throw new Exception("Could not find key " + function.Name + ", " + origLineNum + " in Dictionary");
			}
		}

		public TemplateToCompiledLineLookup this[FunctionInfo function, int origLineNum, int originalColumnIndex]
		{
			get
			{
				if (ContainsKey(function, origLineNum))
				{
					List<TemplateToCompiledLineLookup> lines = _Dictionary[origLineNum][function];
					if (lines.Count == 0)
					{
						throw new Exception(string.Format(
								"The line {0} in function '{1}' does not have any generated code associated with it.",
								origLineNum, function));
					}
					TemplateToCompiledLineLookup returnValue = lines[0];

					foreach (TemplateToCompiledLineLookup lookup in lines)
					{
						if (originalColumnIndex >= lookup.TemplateColumn)
						{
							return lookup;
						}
					}

					return returnValue;
				}
				throw new Exception("Could not find key " + function.Name + ", " + origLineNum + " in Dictionary");
			}
		}

		public void AddLookup(FunctionInfo function, int origLineNum, TemplateToCompiledLineLookup value)
		{
			if (_Dictionary.ContainsKey(origLineNum) == false)
			{
				_Dictionary[origLineNum] = new Dictionary<FunctionInfo, List<TemplateToCompiledLineLookup>>(_StartingCapacity);
				_Dictionary[origLineNum][function] = new List<TemplateToCompiledLineLookup>();
			}
			else if (_Dictionary[origLineNum].ContainsKey(function) == false)
			{
				_Dictionary[origLineNum][function] = new List<TemplateToCompiledLineLookup>();
			}

			_Dictionary[origLineNum][function].Add(value);
		}
	}
}
