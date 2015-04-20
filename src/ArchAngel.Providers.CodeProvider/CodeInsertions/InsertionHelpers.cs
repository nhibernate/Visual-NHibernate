using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CodeInsertions
{
	public static class InsertionHelpers
	{
		public static int GetPropertyNameIndex(string textToSearch, Property baseConstruct, int searchStart, int searchEnd)
		{
			searchEnd = textToSearch.IndexOf('{', searchStart);
			return textToSearch.LastIndexOf(baseConstruct.Name, searchEnd, searchEnd - searchStart);
		}

		public static int GetFunctionNameIndex(string textToSearch, Function baseConstruct, int searchStart, int searchEnd)
		{
			searchEnd = textToSearch.IndexOf('{', searchStart);
			return textToSearch.LastIndexOf(baseConstruct.Name, searchEnd, searchEnd - searchStart);
		}

		public static int GetFieldNameIndex(string textToSearch, Field baseConstruct, int searchStart, int searchEnd)
		{
			//searchEnd = textToSearch.IndexOf('{', searchStart);
			return textToSearch.IndexOf(baseConstruct.Name, searchStart, searchEnd - searchStart);
		}

		public static string GetLastWord(string substring)
		{
			return Regex.Split(substring, @"\s+").Where(s => !String.IsNullOrEmpty(s)).LastOrDefault();
		}

		public static int GetClassKeywordIndex(string text, int searchStart, int searchEnd)
		{
			return text.IndexOf("class", searchStart, searchEnd - searchStart);
		}

		public static int GetIndentationInFrontOf(StringBuilder document, int startIndex)
		{
			return GetIndentationInFrontOf(document.ToString(0, startIndex), startIndex);
		}

		public static int GetIndentationInFrontOf(string document, int startIndex)
		{
			string text = document.Substring(0, startIndex);

			int lastNewLine = text.LastIndexOf('\n');
			string indentationText = text.Substring(lastNewLine + 1, startIndex - lastNewLine - 1);

			int spaces = 0;
			int tabs = 0;
			foreach (char ch in indentationText)
			{
				if (ch == ' ') spaces++;
				else if (ch == '\t') tabs++;
				else break;
			}
			return tabs + (spaces / 4);
		}
	}
}
