using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using ActiproSoftware.SyntaxEditor;
using ActiproSoftware.SyntaxEditor.Addons.Dynamic;

namespace Slyce.Common
{
	public class SyntaxEditorHelper
	{
		/// <summary>
		/// The normal background colour of text
		/// </summary>
		public static Color EDITOR_BACK_COLOR_NORMAL = Color.White;

		/// <summary>
		/// The background colour of text when it is set to faded. By default this is
		/// the script language text, but the user can switch this.
		/// </summary>
		public static Color EDITOR_BACK_COLOR_FADED = Color.LightBlue;

		/// <summary>
		/// The colour of the &lt;% and %&gt; tags.
		/// </summary>
		public static Color ASP_DIRECTIVE_COLOUR = Color.CornflowerBlue;

		/// <summary>
		/// Dictionary containing mapping from a file extension (.txt) to a syntax language.
		/// </summary>
		private static Dictionary<string, TemplateContentLanguage> languagesMap;

		private static Dictionary<string, TemplateContentLanguage> LanguagesMap
		{
			get
			{
				if (languagesMap == null)
				{
					SetupLanguagesMap();
				}
				return languagesMap;
			}
		}

		///<summary>
		/// Enum representing valid scripting languages.
		///</summary>
		[DotfuscatorDoNotRename]
		[Serializable]
		public enum ScriptLanguageTypes
		{
			/// <summary>
			/// C# script language type
			/// </summary>
			[DescriptionAttribute("C#")]
			CSharp,
			/// <summary>
			/// VB.Net script language type.
			/// </summary>
			[DescriptionAttribute("VB.Net")]
			VbNet
		}

		/// <summary>
		/// Gets the display name for the given language.
		/// </summary>
		/// <param name="language"></param>
		/// <returns></returns>
		public static string LanguageNameFromEnum(TemplateContentLanguage language)
		{
			switch (language)
			{
				case TemplateContentLanguage.Assembly: return "Assembly";
				case TemplateContentLanguage.BatchFile: return "Batch File";
				case TemplateContentLanguage.C: return "C";
				case TemplateContentLanguage.Cpp: return "C++";
				case TemplateContentLanguage.CSharp: return "C#";
				case TemplateContentLanguage.Css: return "CSS";
				case TemplateContentLanguage.Html: return "HTML";
				case TemplateContentLanguage.IniFile: return "Ini File";
				case TemplateContentLanguage.Java: return "Java";
				case TemplateContentLanguage.JScript: return "JScript";
				case TemplateContentLanguage.Lua: return "Lua";
				case TemplateContentLanguage.Msil: return "MSIL";
				case TemplateContentLanguage.Pascal: return "Pascal";
				case TemplateContentLanguage.Perl: return "Perl";
				case TemplateContentLanguage.PHP: return "PHP";
				case TemplateContentLanguage.PlainText: return "Plain Text";
				case TemplateContentLanguage.PowerShell: return "PowerShell";
				case TemplateContentLanguage.Python: return "Python";
				case TemplateContentLanguage.Rtf: return "RTF";
				case TemplateContentLanguage.Sql: return "SQL";
				case TemplateContentLanguage.VbDotNet: return "VB.Net";
				case TemplateContentLanguage.VbScript: return "VBScript";
				case TemplateContentLanguage.Xaml: return "XAML";
				case TemplateContentLanguage.Xml: return "XML";
				default:
					throw new NotImplementedException("Syntax language not handled yet: " + language);
			}
		}

		public static TemplateContentLanguage LanguageEnumFromName(string languageName)
		{
			switch (languageName.ToLower())
			{
				case "assembly": return TemplateContentLanguage.Assembly;
				case "batch file": return TemplateContentLanguage.BatchFile;
				case "c": return TemplateContentLanguage.C;
				case "c++": return TemplateContentLanguage.Cpp;
				case "c#": return TemplateContentLanguage.CSharp;
				case "css": return TemplateContentLanguage.Css;
				case "html": return TemplateContentLanguage.Html;
				case "ini file": return TemplateContentLanguage.IniFile;
				case "java": return TemplateContentLanguage.Java;
				case "jscript": return TemplateContentLanguage.JScript;
				case "lua": return TemplateContentLanguage.Lua;
				case "msil": return TemplateContentLanguage.Msil;
				case "pascal": return TemplateContentLanguage.Pascal;
				case "perl": return TemplateContentLanguage.Perl;
				case "php": return TemplateContentLanguage.PHP;
				case "plain text": return TemplateContentLanguage.PlainText;
				case "powershell": return TemplateContentLanguage.PowerShell;
				case "python": return TemplateContentLanguage.Python;
				case "rtf": return TemplateContentLanguage.Rtf;
				case "sql": return TemplateContentLanguage.Sql;
				case "vb.net": return TemplateContentLanguage.VbDotNet;
				case "vbscript": return TemplateContentLanguage.VbScript;
				case "xaml": return TemplateContentLanguage.Xaml;
				case "xml": return TemplateContentLanguage.Xml;
				default:
					throw new NotImplementedException("Syntax language name not handled yet: " + languageName);
			}
		}

		/// <summary>
		/// Tries to determine the language type of the file from its file extension. If it cannot figure it out,
		/// returns PlainText;
		/// </summary>
		/// <param name="filename">The filename of the file to get a language for.</param>
		/// <returns>The language of the file if one is found, or PlainText if the file extension is not in the list of known file types.</returns>
		public static TemplateContentLanguage GetLanguageFromFileName(string filename)
		{
			if (string.IsNullOrEmpty(filename))
				return TemplateContentLanguage.PlainText;

			string extension = Path.GetExtension(filename);
			if (LanguagesMap.ContainsKey(extension))
			{
				return LanguagesMap[extension];
			}

			return TemplateContentLanguage.PlainText;
		}

		/// <summary>
		/// Gets the Actipro DynamicSyntaxLanguage for the given file, if one is available.
		/// Otherwise gets the syntax language for PlainText.
		/// </summary>
		/// <param name="filename">The filename to get the language for.</param>
		/// <returns>The Actipro DynamicSyntaxLanguage for the given file. If no suitable language can be found, the PlainText language is returned.</returns>
		public static DynamicSyntaxLanguage GetSyntaxLanguageFromFileName(string filename)
		{
			return GetDynamicLanguage(GetLanguageFromFileName(filename));
		}

		private static void SetupLanguagesMap()
		{
			languagesMap = new Dictionary<string, TemplateContentLanguage>();

			languagesMap[".asm"] = TemplateContentLanguage.Assembly;
			languagesMap[".bat"] = TemplateContentLanguage.BatchFile;
			languagesMap[".cs"] = TemplateContentLanguage.CSharp;
			languagesMap[".csharp"] = TemplateContentLanguage.CSharp;
			languagesMap[".css"] = TemplateContentLanguage.Css;
			languagesMap[".htm"] = TemplateContentLanguage.Html;
			languagesMap[".aspx"] = TemplateContentLanguage.Html;
			languagesMap[".ascx"] = TemplateContentLanguage.Html;
			languagesMap[".html"] = TemplateContentLanguage.Html;
			languagesMap[".ini"] = TemplateContentLanguage.IniFile;
			languagesMap[".java"] = TemplateContentLanguage.Java;
			languagesMap[".js"] = TemplateContentLanguage.JScript;
			languagesMap[".lua"] = TemplateContentLanguage.Lua;
			languagesMap[".exe"] = TemplateContentLanguage.Msil;
			languagesMap[".p"] = TemplateContentLanguage.Pascal;
			languagesMap[".ph"] = TemplateContentLanguage.Perl;
			languagesMap[".pl"] = TemplateContentLanguage.Perl;
			languagesMap[".php"] = TemplateContentLanguage.PHP;
			languagesMap[".php3"] = TemplateContentLanguage.PHP;
			languagesMap[".py"] = TemplateContentLanguage.Python;
			languagesMap[".sql"] = TemplateContentLanguage.Sql;
			languagesMap[".vb"] = TemplateContentLanguage.VbDotNet;
			languagesMap[".vbs"] = TemplateContentLanguage.VbScript;
			languagesMap[".xml"] = TemplateContentLanguage.Xml;
			languagesMap[".config"] = TemplateContentLanguage.Xml;
			languagesMap[".xsd"] = TemplateContentLanguage.Xml;
			languagesMap[".csproj"] = TemplateContentLanguage.Xml;
			languagesMap[".txt"] = TemplateContentLanguage.PlainText;
		}

		public static string GetLanguageFileName(TemplateContentLanguage language)
		{
			string tempDir = Path.Combine(Path.GetTempPath(), "SlyceSyntaxFiles");
			string filepath;

			switch (language)
			{
				case TemplateContentLanguage.Assembly:
					filepath = Path.Combine(tempDir, "ActiproSoftware.Assembly.xml");
					break;
				case TemplateContentLanguage.BatchFile:
					filepath = Path.Combine(tempDir, "ActiproSoftware.BatchFile.xml");
					break;
				case TemplateContentLanguage.C:
					filepath = Path.Combine(tempDir, "ActiproSoftware.C.xml");
					break;
				case TemplateContentLanguage.Cpp:
					filepath = Path.Combine(tempDir, "ActiproSoftware.Cpp.xml");
					break;
				case TemplateContentLanguage.CSharp:
					filepath = Path.Combine(tempDir, "ActiproSoftware.CSharp.xml");
					break;
				case TemplateContentLanguage.Css:
					filepath = Path.Combine(tempDir, "ActiproSoftware.CSS.xml");
					break;
				case TemplateContentLanguage.Html:
					filepath = Path.Combine(tempDir, "ActiproSoftware.HTML.xml");
					break;
				case TemplateContentLanguage.IniFile:
					filepath = Path.Combine(tempDir, "ActiproSoftware.INIFile.xml");
					break;
				case TemplateContentLanguage.Java:
					filepath = Path.Combine(tempDir, "ActiproSoftware.Java.xml");
					break;
				case TemplateContentLanguage.JScript:
					filepath = Path.Combine(tempDir, "ActiproSoftware.JScript.xml");
					break;
				case TemplateContentLanguage.Lua:
					filepath = Path.Combine(tempDir, "ActiproSoftware.Lua.xml");
					break;
				case TemplateContentLanguage.Msil:
					filepath = Path.Combine(tempDir, "ActiproSoftware.MSIL.xml");
					break;
				case TemplateContentLanguage.Pascal:
					filepath = Path.Combine(tempDir, "ActiproSoftware.Pascal.xml");
					break;
				case TemplateContentLanguage.Perl:
					filepath = Path.Combine(tempDir, "ActiproSoftware.Perl.xml");
					break;
				case TemplateContentLanguage.PHP:
					filepath = Path.Combine(tempDir, "ActiproSoftware.PHP.xml");
					break;
				case TemplateContentLanguage.PlainText:
					filepath = Path.Combine(tempDir, "ActiproSoftware.PlainText.xml");
					break;
				case TemplateContentLanguage.PowerShell:
					filepath = Path.Combine(tempDir, "ActiproSoftware.PowerShell.xml");
					break;
				case TemplateContentLanguage.Python:
					filepath = Path.Combine(tempDir, "ActiproSoftware.Python.xml");
					break;
				case TemplateContentLanguage.Rtf:
					filepath = Path.Combine(tempDir, "ActiproSoftware.RTF.xml");
					break;
				case TemplateContentLanguage.Sql:
					filepath = Path.Combine(tempDir, "ActiproSoftware.SQL.xml");
					break;
				case TemplateContentLanguage.VbDotNet:
					filepath = Path.Combine(tempDir, "ActiproSoftware.VBDotNet.xml");
					break;
				case TemplateContentLanguage.VbScript:
					filepath = Path.Combine(tempDir, "ActiproSoftware.VBScript.xml");
					break;
				case TemplateContentLanguage.Xaml:
					filepath = Path.Combine(tempDir, "ActiproSoftware.XAML.xml");
					break;
				case TemplateContentLanguage.Xml:
					filepath = Path.Combine(tempDir, "ActiproSoftware.XML.xml");
					break;
				default:
					throw new Exception("Language filename not defined yet: " + language);
			}
			if (!File.Exists(filepath))
			{
				WriteSyntaxFilesToDisk();
			}
			if (!File.Exists(filepath))
			{
				throw new FileNotFoundException("Syntax file not found: " + filepath);
			}
			return filepath;
		}

		private static void WriteSyntaxFilesToDisk()
		{
			// Load the two languages
			Assembly loadedAssembly = Assembly.GetExecutingAssembly();
			string tempPath = Path.Combine(Path.GetTempPath(), "SlyceSyntaxFiles");

			if (!Directory.Exists(tempPath))
			{
				Directory.CreateDirectory(tempPath);
			}
			string[] embeddedResourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();

			foreach (string file in embeddedResourceNames)
			{
				if (file.IndexOf("ActiproSoftware") < 0)
				{
					// Only write out syntax files
					continue;
				}
				string filePath = Path.Combine(tempPath, file).Replace("Slyce.Common.SyntaxFiles.", "");

				if (File.Exists(filePath))
					try
					{
						File.Delete(filePath);
					}
					catch
					{
						// Do nothing
					}

				try
				{
					using (Stream outs = loadedAssembly.GetManifestResourceStream(file))
					{
						outs.Seek(0, SeekOrigin.Begin);

						using (StreamReader srException = new StreamReader(outs))
						{
							using (StreamWriter oWrite = new StreamWriter(filePath, false, Encoding.Unicode))
							{
								oWrite.Write(srException.ReadToEnd());
								oWrite.Close();
							}
						}
					}
				}
				catch
				{
					// Do nothing
				}
			}
		}

		/// <summary>
		/// Deletes all resource files written to disk
		/// </summary>
		public static void DeleteResources()
		{
			string tempDir = Path.Combine(Path.GetTempPath(), "SlyceSyntaxFiles");

			if (Directory.Exists(tempDir))
			{
				Utility.DeleteDirectoryBrute(tempDir);
			}
		}

		/// <summary>
		/// Gets a DynamicSyntaxLanguage instance given a Languages enum.
		/// </summary>
		/// <param name="language"></param>
		/// <returns></returns>
		public static DynamicSyntaxLanguage GetDynamicLanguage(TemplateContentLanguage language)
		{
			string outputSyntaxFilePath = GetLanguageFileName(language);
			//if (!File.Exists(outputSyntaxFilePath))
			//{
			//    throw new Exception("Language syntax file could not be loaded: " + outputSyntaxFilePath);
			//}
			string filename = Path.GetFileName(outputSyntaxFilePath);
			//Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(string.Format("Slyce.Common.SyntaxFiles.{0}", filename));
			return DynamicSyntaxLanguage.LoadFromXml(outputSyntaxFilePath, 0);
			//return DynamicSyntaxLanguage.LoadFromXml(stream, 0);
		}

		/// <summary>
		/// Converts the given ScriptLanguageTypes enum value to a Languages enum value.
		/// </summary>
		/// <param name="scriptLanguage"></param>
		/// <returns></returns>
		public static TemplateContentLanguage GetScriptingLanguage(ScriptLanguageTypes scriptLanguage)
		{
			// Set the scripting language
			TemplateContentLanguage scriptingLanguage;
			switch (scriptLanguage)
			{
				case ScriptLanguageTypes.CSharp:
					scriptingLanguage = TemplateContentLanguage.CSharp;
					break;
				case ScriptLanguageTypes.VbNet:
					scriptingLanguage = TemplateContentLanguage.VbDotNet;
					break;
				default:
					throw new Exception("ScriptLanguage not catered for yet in CreateDirectiveXmlToCSharpLanguage: " + scriptLanguage);
			}
			return scriptingLanguage;
		}

		/// <summary>
		/// Loads two languages and creates a state transition from XML to C# within ASP-style directives.
		/// </summary>
		public static void SetupEditorTemplateAndScriptLanguages(SyntaxEditor editor, TemplateContentLanguage textLanguage, ScriptLanguageTypes scriptLanguage, string delimiterStart, string delimiterEnd)
		{
			DynamicSyntaxLanguage language = GetDynamicLanguage(textLanguage);
			DynamicSyntaxLanguage cSharpLanguage = GetDynamicLanguage(GetScriptingLanguage(scriptLanguage));

			language.AutomaticOutliningBehavior = AutomaticOutliningBehavior.PostSemanticParse;
			cSharpLanguage.AutomaticOutliningBehavior = AutomaticOutliningBehavior.PostSemanticParse;

			language.Tag = "TemplateLanguage";
			cSharpLanguage.Tag = "ScriptLanguage";

			// Mark that updating is starting
			language.IsUpdating = true;
			cSharpLanguage.IsUpdating = true;

			// Add StateTransitions to current language as well nested languages eg: HTML -> JavaScript, VBScript etc.
			for (int i = 0; i <= language.ChildLanguages.Count; i++)
			{
				DynamicSyntaxLanguage lan;

				if (i == language.ChildLanguages.Count)
				{
					lan = language;
				}
				else
				{
					lan = (DynamicSyntaxLanguage)language.ChildLanguages[i];
				}
				// Add a highlighting style
				lan.HighlightingStyles.Add(new HighlightingStyle("ASPDirectiveDelimiterStyle", null, Color.Black, ASP_DIRECTIVE_COLOUR));
				lan.AutomaticOutliningBehavior = AutomaticOutliningBehavior.SemanticParseDataChange;

				// Create a new lexical state
				DynamicLexicalState lexicalState = new DynamicLexicalState(0, "ASPDirectiveState");
				lexicalState.DefaultTokenKey = "ASPDirectiveDefaultToken";
				lexicalState.DefaultHighlightingStyle = lan.HighlightingStyles["DefaultStyle"];
				lexicalState.LexicalStateTransitionLexicalState = cSharpLanguage.LexicalStates["DefaultState"];

				lan.LexicalStates.Add(lexicalState);

				// Add the new lexical state at the beginning of the child states...
				// Remember that with an NFA regular expression, the first match is taken...
				// So since a < scope pattern is already in place, we must insert the new one before it
				lan.LexicalStates["DefaultState"].ChildLexicalStates.Insert(0, lexicalState);

				#region Extra Transition points - Eg: comments
				if (lan.LexicalStates.IndexOf("XMLCommentState") >= 0) // C#
				{
					lan.LexicalStates["XMLCommentState"].ChildLexicalStates.Insert(0, lexicalState); // Added this to ensure that transitions can occur in XML Comments
				}
				if (lan.LexicalStates.IndexOf("CommentState") >= 0) // C#
				{
					lan.LexicalStates["CommentState"].ChildLexicalStates.Insert(0, lexicalState); // Added this to ensure that transitions can occur in XML Comments
				}
				if (lan.LexicalStates.IndexOf("StringState") >= 0) // SQL
				{
					// Note: Had to modify the RegexPatternGroup for StringState in ActiproSoftware.SQL.xml to: <RegexPatternGroup TokenKey="StringDefaultToken" PatternValue="[^&lt;^']+" />
					lan.LexicalStates["StringState"].ChildLexicalStates.Insert(0, lexicalState); // Added this to ensure that transitions can occur in XML Comments
				}
				if (lan.LexicalStates.IndexOf("SquareStringState") >= 0) // SQL
				{
					lan.LexicalStates["SquareStringState"].ChildLexicalStates.Insert(0, lexicalState); // Added this to ensure that transitions can occur in XML Comments
				}
				if (lan.LexicalStates.IndexOf("MultiLineCommentState") >= 0) // SQL
				{
					lan.LexicalStates["MultiLineCommentState"].ChildLexicalStates.Insert(0, lexicalState); // Added this to ensure that transitions can occur in XML Comments
				}
				if (lan.LexicalStates.IndexOf("StartTagState") >= 0) // HTML
				{
					lan.LexicalStates["StartTagState"].ChildLexicalStates.Insert(0, lexicalState); // Added this to ensure that transitions can occur in XML Comments
				}
				if (lan.LexicalStates.IndexOf("StartTagAttributeStringValueState") >= 0) // HTML
				{
					lan.LexicalStates["StartTagAttributeStringValueState"].ChildLexicalStates.Insert(0, lexicalState); // Added this to ensure that transitions can occur in XML Comments
				}
				if (lan.LexicalStates.IndexOf("StartTagAttributeState") >= 0) // HTML
				{
					lan.LexicalStates["StartTagAttributeState"].ChildLexicalStates.Insert(0, lexicalState); // Added this to ensure that transitions can occur in XML Comments
				}
				if (lan.LexicalStates.IndexOf("StartTagAttributeValueState") >= 0) // HTML
				{
					lan.LexicalStates["StartTagAttributeValueState"].ChildLexicalStates.Insert(0, lexicalState); // Added this to ensure that transitions can occur in XML Comments
				}
				// Create a lexical scope with a lexical state transition
				DynamicLexicalScope lexicalScope = new DynamicLexicalScope();
				lexicalState.LexicalScopes.Add(lexicalScope);
				lexicalScope.StartLexicalPatternGroup = new LexicalPatternGroup(LexicalPatternType.Explicit, "ASPDirectiveStartToken", lan.HighlightingStyles["ASPDirectiveDelimiterStyle"], delimiterStart);
				lexicalScope.StartLexicalPatternGroup.LookBehindPattern = @"\\{2}|([^\\]|^)";// @"\\{2}|[^\\]";
				lexicalScope.EndLexicalPatternGroup = new LexicalPatternGroup(LexicalPatternType.Explicit, "ASPDirectiveEndToken", lan.HighlightingStyles["ASPDirectiveDelimiterStyle"], delimiterEnd);
				lexicalScope.AncestorEndScopeCheckEnabled = false;
			}
				#endregion

			// Mark that updating is complete (since linking is complete, the flag setting 
			// will filter from the XML language into the C# language)
			language.IsUpdating = false;
			editor.Document.Language = language;
		}

		/// <summary>
		/// Gets the start and end offsets of the current template language block.
		/// </summary>
		/// <param name="stream">The stream to search.</param>
		/// <param name="newStart">The variable to put the start offset in.</param>
		/// <param name="newEnd">The variable to put the end offset in.</param>
		public static void GetTemplateLanguageBlock(TextStream stream, out int newStart, out int newEnd)
		{
			newStart = GetStartOfTemplateLanguageBlock(stream);
			newEnd = GetEndOfTemplateLanguageBlock(stream);
		}

		/// <summary>
		/// Helper method to find the end of a template language block.
		/// </summary>
		/// <param name="stream">The stream to search.</param>
		/// <returns>The end offset of the current template lanugage block, excluding the &lt;% token. </returns>
		public static int GetEndOfTemplateLanguageBlock(TextStream stream)
		{
			return GetEndOfLanguageBlock(stream, "TemplateLanguage", "ASPDirectiveStartToken");
		}

		/// <summary>
		/// Helper method to find the start of a template language block.
		/// </summary>
		/// <param name="stream">The stream to search.</param>
		/// <returns>The start offset of the current template lanugage block, excluding the %> token. </returns>
		public static int GetStartOfTemplateLanguageBlock(TextStream stream)
		{
			return GetStartOfLanguageBlock(stream, "TemplateLanguage", "ASPDirectiveEndToken");
		}

		/// <summary>
		/// Helper method to find the end of a script language block.
		/// </summary>
		/// <param name="stream">The stream to search.</param>
		/// <returns>The end offset of the current script lanugage block, excluding the %> token. </returns>
		public static int GetEndOfScriptLanguageBlock(TextStream stream)
		{
			return GetEndOfLanguageBlock(stream, "ScriptLanguage", "ASPDirectiveEndToken");
		}

		/// <summary>
		/// Helper method to find the start of a script language block.
		/// </summary>
		/// <param name="stream">The stream to search.</param>
		/// <returns>The start offset of the current script lanugage block, excluding the &lt;% token. </returns>
		public static int GetStartOfScriptLanguageBlock(TextStream stream)
		{
			return GetStartOfLanguageBlock(stream, "ScriptLanguage", "ASPDirectiveStartToken");
		}

		/// <summary>
		/// Gets the offset of the start of the specified language. If the stream is not currently
		/// in that language, the result is meaningless.
		/// </summary>
		/// <param name="stream">The stream to search.</param>
		/// <param name="language">The language we are currently in and should find the start of.</param>
		/// <param name="startTokenKey">The token which delimits this language block. If used, stops
		/// the delimiters being counted as part of the language block.</param>
		/// <returns>The offset at which the language starts.</returns>
		public static int GetStartOfLanguageBlock(TextStream stream, string language, string startTokenKey)
		{
			do
			{
				if (stream.Token.Language.Tag.ToString() != language
					|| stream.Token.Key == startTokenKey)
				{
					stream.SeekToken(1);
					break;
				}
				stream.SeekToken(-1);
			}
			while (stream.Token.StartOffset > 0);

			return stream.Token.StartOffset;
		}

		/// <summary>
		/// Gets the offset of the end of the specified language. If the stream is not currently
		/// in that language, the result is meaningless.
		/// </summary>
		/// <param name="stream">The stream to search.</param>
		/// <param name="language">The language we are currently in and should find the end of.</param>
		/// <param name="endTokenKey">The token which delimits this language block. If used, stops
		/// the delimiters being counted as part of the language block.</param>
		/// <returns>The offset at which the language ends.</returns>
		public static int GetEndOfLanguageBlock(TextStream stream, string language, string endTokenKey)
		{
			do
			{
				if (stream.Token.Language.Tag.ToString() != language
					|| stream.IsAtDocumentEnd
					|| stream.Token.Key == endTokenKey)
				{
					stream.SeekToken(-1);
					break;
				}
				stream.SeekToken(1);
			}
			while (true);

			return stream.Token.EndOffset;
		}

		/// <summary>
		/// Checks the given line and determines if it is one single language.
		/// </summary>
		/// <param name="line">The line to check.</param>
		/// <param name="stream">The TextStream from the Document the line belongs to. We
		/// need this because the line does not have a reference to its parent Document.</param>
		/// <param name="language">The text representation of the language. We compare this against
		/// stream.Token.Language.Tag.ToString().</param>
		/// <returns>True if the line contains one language, false if it contains two or more.</returns>
		public static bool IsEntireLineOneLanguage(DocumentLine line, TextStream stream, string language)
		{
			bool isLineTemplateLanguage = true;
			stream.Offset = line.StartOffset;
			do
			{
				if (stream.Token.Language.Tag.ToString() != language)
				{
					isLineTemplateLanguage = false;
					break;
				}
				stream.SeekToken(1);
			}
			while (stream.Token.EndOffset <= line.EndOffset && stream.IsAtDocumentEnd == false);

			return isLineTemplateLanguage;
		}

		/// <summary>
		/// Gets the first index of ASPDirectiveStartToken from the given stream.
		/// </summary>
		/// <param name="stream">The stream to search.</param>
		/// <returns>The index of the first ASP start token, or -1 if there isn't one
		/// after the stream's current offset.</returns>
		public static int GetFirstStartScriptTag(TextStream stream)
		{
			do
			{
				if (stream.Token.Key == "ASPDirectiveStartToken")
				{
					return stream.Token.StartOffset;
				}
				stream.SeekToken(1);
			}
			while (stream.IsAtDocumentEnd == false);

			return -1;
		}

		/// <summary>
		/// Gets the first index of ASPDirectiveEndToken from the given stream.
		/// </summary>
		/// <param name="stream">The stream to search.</param>
		/// <returns>The index of the first ASP end token, or -1 if there isn't one
		/// after the stream's current offset.</returns>
		public static int GetFirstEndScriptTag(TextStream stream)
		{
			do
			{
				if (stream.Token.Key == "ASPDirectiveEndToken")
				{
					return stream.Token.StartOffset;
				}
				stream.SeekToken(1);
			}
			while (stream.IsAtDocumentEnd == false);

			return -1;
		}
	}

	public enum TemplateContentLanguage
	{
		Assembly,
		BatchFile,
		C,
		Cpp,
		CSharp,
		Css,
		Html,
		IniFile,
		Java,
		JScript,
		Lua,
		Msil,
		Pascal,
		Perl,
		PHP,
		PlainText,
		PowerShell,
		Python,
		Rtf,
		Sql,
		VbDotNet,
		VbScript,
		Xaml,
		Xml
	}
}
