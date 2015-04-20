using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActiproSoftware.SyntaxEditor;

namespace ArchAngel.Workbench
{
	[Serializable]
	public class FoundLocation
	{
		public ArchAngel.Interfaces.Template.File ScriptFile;
		public int StartPos = -1;
		public int Length;

		public FoundLocation(ArchAngel.Interfaces.Template.File scriptFile, int startPos, int length)
		{
			ScriptFile = scriptFile;
			StartPos = startPos;
			Length = length;
		}

		public string Body
		{
			get
			{
				// Check whether this function is open in an editor first
				if (ContentItems.Templates.CurrentFile == ScriptFile)
					// We don't need to replace \r\n with \n, because the SyntaxEditor control automatically stores it in this format internally already
					return ContentItems.Templates.Instance.SyntaxEditor.Text;

				// This function is not open in an editor, so just fetch the saved version
				return ScriptFile.Script.Body.Replace("\r\n", "\n");
			}
		}
	}

	class SearchHelper
	{
		#region Enums
		public enum Scope
		{
			ScriptOnly,
			OutputOnly,
			Both
		}
		public enum SearchFunctions
		{
			CurrentFunction,
			OpenFunctions,
			AllFunctions
		}
		#endregion

		private static readonly List<FoundLocation> m_foundLocations = new List<FoundLocation>();
		private static int CurrentIndex = -1;
		public static FindReplaceOptions Options = new FindReplaceOptions();
		public static Scope _scope = Scope.Both;
		public static SearchFunctions searchFunctions;
		private const string ValidWordChars = "abcdefghijklmnopqrstuvwxyz0123456789_";
		//internal static string TextToFind = "";

		public static Scope scope
		{
			get
			{
				return _scope;
			}
			set
			{
				_scope = value;
			}
		}

		public static List<FoundLocation> FoundLocations
		{
			get { return m_foundLocations; }
		}

		public static void FindNext()//string textToFind, Scope scope, SearchFunctions searchFunctions, FindReplaceOptions options)
		{
			switch (searchFunctions)
			{
				case SearchFunctions.CurrentFunction:
					if (ContentItems.Templates.CurrentFile != null)
					{
						ArchAngel.Interfaces.Template.File function = ContentItems.Templates.CurrentFile;
						SyntaxEditor editor = ContentItems.Templates.Instance.SyntaxEditor;
						FindInText(editor.Text, Options.FindText, scope, function, true, Options, editor.SelectedView.Selection.StartOffset, true);
					}
					break;
				case SearchFunctions.OpenFunctions:
					break;
				case SearchFunctions.AllFunctions:
					break;
				default:
					throw new NotImplementedException("Not coded yet.");
			}
		}

		public static void RunFind()//string textToFind, Scope scope, SearchFunctions searchFunctions, FindReplaceOptions options)
		{
			m_foundLocations.Clear();

			switch (searchFunctions)
			{
				case SearchFunctions.AllFunctions:
					// Process open functions first
					ArchAngel.Interfaces.Template.File currentScriptFile = ContentItems.Templates.CurrentFile;
					SyntaxEditor editor = ContentItems.Templates.Instance.SyntaxEditor;

					if (editor != null)
						FindInText(editor.Text, Options.FindText, scope, currentScriptFile, true, Options);

					// Process remaining functions
					foreach (var scriptFile in ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.AllScriptFiles)
					{
						// Don't add functions that have already been added as open functions
						if (scriptFile != currentScriptFile)
							FindInText(scriptFile.Script.Body, Options.FindText, scope, scriptFile, true, Options);
					}
					break;
				case SearchFunctions.CurrentFunction:
					if (ContentItems.Templates.CurrentFile != null)
						FindInText(ContentItems.Templates.Instance.SyntaxEditor.Text, Options.FindText, scope, ContentItems.Templates.CurrentFile, true, Options, ContentItems.Templates.Instance.SyntaxEditor.SelectedView.Selection.StartOffset, false);

					break;
				default:
					throw new NotImplementedException("Not coded yet.");
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="textToFind"></param>
		/// <param name="replacementText"></param>
		/// <returns>Number of replacements made.</returns>
		public static int ReplaceAll(string textToFind, string replacementText)
		{
			SearchHelper.Search(textToFind);
			int padIncrement = replacementText.Length - textToFind.Length;
			int pad = 0;

			switch (SearchHelper.searchFunctions)
			{
				case SearchHelper.SearchFunctions.CurrentFunction:
					ReplaceInCurrentEditor(replacementText, padIncrement, ref pad);
					break;
				case SearchHelper.SearchFunctions.AllFunctions:
					ReplaceInCurrentEditor(replacementText, padIncrement, ref pad);

					foreach (var scriptFile in SearchHelper.FoundLocations.Where(f => f.ScriptFile != ContentItems.Templates.CurrentFile).Select(l => l.ScriptFile).Distinct())
					{
						pad = 0;

						foreach (var location in SearchHelper.FoundLocations.Where(f => f.ScriptFile == scriptFile))
						{
							var function = location.ScriptFile;

							function.Script.Body = function.Script.Body.Replace("\r\n", "\n");
							function.Script.Body = function.Script.Body.Remove(location.StartPos + pad, location.Length);
							function.Script.Body = function.Script.Body.Insert(location.StartPos + pad, replacementText);
							pad += padIncrement;
						}
					}
					break;
				default:
					throw new NotImplementedException("Not coded yet.");
			}

			return SearchHelper.FoundLocations.Count;
		}

		private static void ReplaceInCurrentEditor(string replacementText, int padIncrement, ref int pad)
		{
			StringBuilder sb = new StringBuilder(ContentItems.Templates.Instance.SyntaxEditor.Document.Text, ContentItems.Templates.Instance.SyntaxEditor.Document.Text.Length + 100);
			sb.Replace("\r\n", "\n");
			try
			{
				ContentItems.Templates.Instance.SyntaxEditor.SuspendPainting();

				foreach (var location in SearchHelper.FoundLocations.Where(f => f.ScriptFile == ContentItems.Templates.CurrentFile))
				{
					sb.Remove(location.StartPos + pad, location.Length);
					sb.Insert(location.StartPos + pad, replacementText);
					pad += padIncrement;
				}
				ContentItems.Templates.Instance.SyntaxEditor.Document.Text = sb.ToString();
			}
			finally
			{
				ContentItems.Templates.Instance.SyntaxEditor.ResumePainting();
			}
		}

		private static void FindInText(string text, string textToFind, Scope scope, ArchAngel.Interfaces.Template.File scriptFile, bool isTemplateFunction, FindReplaceOptions options)
		{
			FindInText(text, textToFind, scope, scriptFile, isTemplateFunction, options, -1, false);
		}

		/// <summary>
		/// Fills FoundLocations with positions in the text.
		/// </summary>
		/// <param name="text"></param>
		/// <param name="textToFind"></param>
		/// <param name="scope"></param>
		/// <param name="scriptFile"></param>
		/// <param name="isTemplateFunction"></param>
		/// <param name="options"></param>
		/// <param name="userOffset"></param>
		/// <param name="findOneOnly"></param>
		/// <returns></returns>
		private static bool FindInText(string text, string textToFind, Scope scope, ArchAngel.Interfaces.Template.File scriptFile, bool isTemplateFunction, FindReplaceOptions options, int userOffset, bool findOneOnly)
		{
			char delimiter = '%';

			if (ArchAngel.Interfaces.SharedData.CurrentProject != null &&
				ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject != null &&
				ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.Delimiter == Interfaces.Template.TemplateProject.DelimiterTypes.T4)
			{
				delimiter = '#';
			}
			text = text.Replace("\r\n", "\n");
			// TODO: this function is in dire need of refactoring. The searching code is repeated in multiple places.
			bool found = false;

			if (string.IsNullOrEmpty(textToFind))
			{
				return false;
			}
			if (!isTemplateFunction)
			{
				// Only template functions should have 'script' and 'output'
				scope = Scope.Both;
			}
			if (!options.MatchCase)
			{
				text = text.ToLower();
				textToFind = textToFind.ToLower();
			}
			int scriptStartPos = 0;
			int scriptEndPos = 0;
			int nextPos;

			switch (scope)
			{
				case Scope.ScriptOnly:
					for (int i = 0; i < text.Length; i++)
					{
						if (text[i] == '<' &&
							text.Length > (i + 1) &&
							text[i + 1] == delimiter)
						{
							scriptStartPos = i + 2;
						}
						else if (text[i] == delimiter &&
							text.Length > (i + 1) &&
							text[i + 1] == '>')
						{
							scriptEndPos = i;
							// Replace text
							string script = text.Substring(scriptStartPos, scriptEndPos - scriptStartPos);
							nextPos = script.IndexOf(textToFind);

							while (nextPos >= 0)
							{
								if (!options.MatchWholeWord ||
									(options.MatchWholeWord && IsWholeWord(ref script, ref textToFind, ref nextPos)))
								{
									m_foundLocations.Add(new FoundLocation(scriptFile, scriptStartPos + nextPos, textToFind.Length));

									if (scriptStartPos + nextPos > userOffset)
									{
										found = true;

										if (findOneOnly)
										{
											m_foundLocations.Clear();
											m_foundLocations.Add(new FoundLocation(scriptFile, scriptStartPos + nextPos, textToFind.Length));
											return found;
										}
									}
								}
								nextPos = script.IndexOf(textToFind, nextPos + 1);
							}
							i = scriptEndPos + 2;
						}
					}
					break;
				case Scope.OutputOnly:
					scriptStartPos = 0; // This works for template functions only

					for (int i = 0; i < text.Length; i++)
					{
						if (text[i] == delimiter &&
							text.Length > (i + 1) &&
							text[i + 1] == '>')
						{
							scriptStartPos = i + 2;
						}
						else if (text[i] == '<' &&
							text.Length > (i + 1) &&
							text[i + 1] == delimiter)
						{
							scriptEndPos = i;
							// Replace text
							string script = text.Substring(scriptStartPos, scriptEndPos - scriptStartPos);
							nextPos = script.IndexOf(textToFind);

							while (nextPos >= 0)
							{
								if (!options.MatchWholeWord ||
									(options.MatchWholeWord && IsWholeWord(ref text, ref textToFind, ref nextPos)))
								{
									m_foundLocations.Add(new FoundLocation(scriptFile, scriptStartPos + nextPos, textToFind.Length));

									if (scriptStartPos + nextPos > userOffset)
									{
										found = true;

										if (findOneOnly)
										{
											m_foundLocations.Clear();
											m_foundLocations.Add(new FoundLocation(scriptFile, scriptStartPos + nextPos, textToFind.Length));
											return found;
										}
									}
								}
								nextPos = script.IndexOf(textToFind, nextPos + 1);
							}
							i = scriptEndPos + 2;
						}
					}
					// Search the remaining text
					if (scriptEndPos < text.Length)
					{
						string script = text.Substring(scriptStartPos);
						nextPos = script.IndexOf(textToFind);

						while (nextPos >= 0)
						{
							if (!options.MatchWholeWord ||
									(options.MatchWholeWord && IsWholeWord(ref text, ref textToFind, ref nextPos)))
							{
								m_foundLocations.Add(new FoundLocation(scriptFile, scriptStartPos + nextPos, textToFind.Length));

								if (scriptStartPos + nextPos > userOffset)
								{
									found = true;

									if (findOneOnly)
									{
										m_foundLocations.Clear();
										m_foundLocations.Add(new FoundLocation(scriptFile, scriptStartPos + nextPos, textToFind.Length));
										return found;
									}
								}
							}
							nextPos = script.IndexOf(textToFind, nextPos + 1);
						}
					}
					break;
				case Scope.Both:
					nextPos = text.IndexOf(textToFind);

					while (nextPos >= 0)
					{
						if (!options.MatchWholeWord ||
									(options.MatchWholeWord && IsWholeWord(ref text, ref textToFind, ref nextPos)))
						{
							m_foundLocations.Add(new FoundLocation(scriptFile, nextPos, textToFind.Length));

							if (scriptStartPos + nextPos > userOffset)
							{
								found = true;

								if (findOneOnly)
								{
									m_foundLocations.Clear();
									m_foundLocations.Add(new FoundLocation(scriptFile, nextPos, textToFind.Length));
									return found;
								}
							}
						}
						nextPos = text.IndexOf(textToFind, nextPos + 1);
					}
					break;
				default:
					throw new NotImplementedException("Not coded yet");
			}
			return found;
		}

		/// <summary>
		/// Gets whether the found word is a whole word.
		/// </summary>
		/// <param name="text">The text being searched.</param>
		/// <param name="findText">The text being searched for.</param>
		/// <param name="pos">The position where the word has been found.</param>
		/// <returns>True if the found word is a whole word, false if it is part of a larger word.</returns>
		private static bool IsWholeWord(ref string text, ref string findText, ref int pos)
		{
			// Check MatchWholeWord
			if (pos > 0 &&
				ValidWordChars.IndexOf(text[pos - 1]) >= 0)
			{
				// MatchWholeWord has failed - there is a valid word character in front of it
				return false;
			}
			if (text.Length > pos + findText.Length &&
				ValidWordChars.IndexOf(text[pos + findText.Length]) >= 0)
			{
				// MatchWholeWord has failed - there is a valid word character in after it
				return false;
			}
			return true;
		}

		public static void Search()
		{
			Search("NONE_SPECIFIED");
		}

		public static void Search(string textToFind)
		{
			if (Options == null)
				return;

			if (textToFind == "NONE_SPECIFIED")
				textToFind = Options.FindText;

			Options.FindText = textToFind;
			SearchHelper.RunFind();

			if (SearchHelper.FoundLocations.Count == 0)
				return;

			if (CurrentIndex < 0)
				CurrentIndex = 0;

			if (CurrentIndex < SearchHelper.FoundLocations.Count - 1)
				CurrentIndex++;
			else if (SearchHelper.FoundLocations.Count >= 0)
				CurrentIndex = 0;

			bool found;

			// We now have a collection of indexes, so let's look for the actual words
			switch (searchFunctions)
			{
				case SearchFunctions.CurrentFunction:
					if (ContentItems.Templates.CurrentFile == null)
						return;

					if (SearchHelper.FoundLocations.Count > 0 && SearchHelper.FoundLocations[0].ScriptFile != ContentItems.Templates.CurrentFile)
					{
						scope = SearchHelper.Scope.Both;
						searchFunctions = SearchHelper.SearchFunctions.AllFunctions;
						SearchHelper.RunFind();
					}
					SyntaxEditor editor = ContentItems.Templates.Instance.SyntaxEditor;
					int currentStartPos = editor.SelectedView.Selection.StartOffset + 1;
					CurrentIndex = -1;
					found = false;

					for (int foundLocIndex = 0; foundLocIndex < SearchHelper.FoundLocations.Where(l => l.ScriptFile == ContentItems.Templates.CurrentFile).Count(); foundLocIndex++)
					{
						if (CurrentIndex < 0) { CurrentIndex = foundLocIndex; }

						CurrentIndex = foundLocIndex;

						if (SearchHelper.FoundLocations[foundLocIndex].StartPos > currentStartPos)
						{
							CurrentIndex = foundLocIndex;
							found = true;
							break;
						}
					}
					if (!found)
						CurrentIndex = 0;

					if (CurrentIndex < 0)
						return;

					editor.SelectedView.Selection.StartOffset = SearchHelper.FoundLocations[CurrentIndex].StartPos;
					editor.SelectedView.Selection.EndOffset = SearchHelper.FoundLocations[CurrentIndex].StartPos + SearchHelper.FoundLocations[CurrentIndex].Length;
					break;
				case SearchFunctions.AllFunctions:// This is just highlighting the text in all open functions
					// Make sure highlighting starts from the currently selected position
					CurrentIndex = -1;
					found = false;

					if (ContentItems.Templates.CurrentFile != null)
					{
						int selectedPos = ContentItems.Templates.Instance.SyntaxEditor.SelectedView.Selection.StartOffset + ContentItems.Templates.Instance.SyntaxEditor.SelectedView.Selection.Length;

						if (SearchHelper.FoundLocations.Count > 0)
						{
							for (int i = 0; i < SearchHelper.FoundLocations.Count; i++)
							{
								if (SearchHelper.FoundLocations[i].ScriptFile == ContentItems.Templates.CurrentFile)
								{
									if (CurrentIndex < 0) { CurrentIndex = i; }

									CurrentIndex = i;

									if (SearchHelper.FoundLocations[i].StartPos > selectedPos)
									{
										found = true;
										break;
									}
								}
							}
						}
					}
					if (!found && SearchHelper.FoundLocations.Where(l => l.ScriptFile != ContentItems.Templates.CurrentFile).Count() > 0)
					{

						if (CurrentIndex < SearchHelper.FoundLocations.Count - 1)
							CurrentIndex++;
						else
							CurrentIndex = 0;

						ContentItems.Templates.Instance.SelectFile(SearchHelper.FoundLocations[CurrentIndex].ScriptFile);
					}
					ContentItems.Templates.Instance.SyntaxEditor.SelectedView.Selection.StartOffset = SearchHelper.FoundLocations[CurrentIndex].StartPos;
					ContentItems.Templates.Instance.SyntaxEditor.SelectedView.Selection.EndOffset = SearchHelper.FoundLocations[CurrentIndex].StartPos + SearchHelper.FoundLocations[CurrentIndex].Length;
					break;
				default:
					throw new NotImplementedException("Not coded yet.");
			}

		}

		/// <summary>
		/// Searches the regex matches, typically only the script or output portions only.
		/// </summary>
		/// <param name="text"></param>
		/// <param name="textToFind"></param>
		/// <param name="replacementText"></param>
		/// <param name="replaceAll"></param>
		/// <param name="startPos"></param>
		/// <returns></returns>
		public static string ReplaceTextInScript(string text, string textToFind, string replacementText, bool replaceAll, ref int startPos)
		{
			char delimiter = '%';

			if (ArchAngel.Interfaces.SharedData.CurrentProject != null &&
				ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject != null &&
				ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.Delimiter == Interfaces.Template.TemplateProject.DelimiterTypes.T4)
			{
				delimiter = '#';
			}
			bool inScript = false;
			int scriptStartPos = 0;
			StringBuilder sb = new StringBuilder(text);

			for (int i = startPos; i < sb.Length; i++)
			{
				if (sb[i] == '<' &&
					sb.Length > (i + 1) &&
					sb[i + 1] == delimiter)
				{
					if (inScript)
					{
						//throw new Exception("Script start tag with no matching end tag.");
					}
					inScript = true;
					scriptStartPos = i + 2;
				}
				else if (sb[i] == delimiter &&
					sb.Length > (i + 1) &&
					sb[i + 1] == '>')
				{
					if (!inScript)
					{
						//throw new Exception("Script end tag with no matching start tag.");
					}
					inScript = false;
					int scriptEndPos = i;
					// Replace text
					string script = sb.ToString(scriptStartPos, scriptEndPos - scriptStartPos);
					int originalScriptLength = script.Length;
					script = script.Replace(textToFind, replacementText);
					int newScriptLength = script.Length;
					sb.Remove(scriptStartPos, scriptEndPos - scriptStartPos);
					sb.Insert(scriptStartPos, script);
					i += newScriptLength - originalScriptLength;

					if (replaceAll)
					{
						return sb.ToString();
					}
				}
			}
			return sb.ToString();
		}

	}
}
