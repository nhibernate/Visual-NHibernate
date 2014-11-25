using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ActiproSoftware.SyntaxEditor;
using ActiproSoftware.SyntaxEditor.Addons.Dynamic;
using ArchAngel.Common.DesignerProject;
using ArchAngel.Designer.DesignerProject;

namespace ArchAngel.Designer
{
	class IntelliSense
	{
		private static Assembly[] ReferencedAssemblies;
		private static SyntaxEditor Editor;
		private static bool InAutoCompleteMode;
		private static ArrayList AddedProperties = new ArrayList();
		private static Dictionary<Assembly, List<LookupType>> AssemblyTypes = new Dictionary<Assembly, List<LookupType>>();

		public struct LookupType
		{
			public Type Type;
			public string[] NamespaceWords;
			public string[] AllWords;
			public string[] AllWordsLowerCase;
			public string FullTypeString;

			public LookupType(Type type)
			{
				Type = type;
				NamespaceWords = Type.Namespace == null ? new string[0] : Type.Namespace.Split('.');
				AllWords = Type.FullName.Replace("+", ".").Split('.');
				AllWordsLowerCase = new string[AllWords.Length];

				for (int i = 0; i < AllWords.Length; i++)
				{
					AllWordsLowerCase[i] = AllWords[i].ToLower();
				}
				FullTypeString = "";

				for (int i = 0; i < AllWordsLowerCase.Length; i++)
				{
					if (i > 0)
					{
						FullTypeString += ".";
					}
					FullTypeString += AllWordsLowerCase[i];
				}
			}
		}

		#region AutoComplete Functions
		/// <summary>
		/// AutoCompletes the current word.
		/// </summary>
		/// <param name="editor"></param>
		/// <param name="currentFunction"></param>
		public static void AutoComplete(ref SyntaxEditor editor, FunctionInfo currentFunction)
		{
			try
			{
				InAutoCompleteMode = true;
				Editor = editor;

				if (Editor.IntelliPrompt.MemberList.ImageList == null)
				{
					Editor.IntelliPrompt.MemberList.ImageList = SyntaxEditor.ReflectionImageList;
				}
				Editor.IntelliPrompt.MemberList.HideOnParentFormDeactivate = true;
				Editor.IntelliPrompt.MemberList.MatchBasedOnItemPreText = true;
				Editor.IntelliPrompt.MemberList.Clear();
				string fullTypeString = GetFullString();

				if (fullTypeString.Length > 0)
				{
					// Process in order of least expensive to most expensive
					AutoCompleteSpecialWords(fullTypeString);
					AutoCompleteUserOptions(fullTypeString);
					AutoCompleteConstants(fullTypeString);
					AutoCompleteFunctions(fullTypeString);
					AutoCompleteLocalVariables(fullTypeString, editor);
					AutoCompleteFunctionParameters(currentFunction, fullTypeString);
					if (FindInAssemblies(fullTypeString) && !InAutoCompleteMode) { return; }
				}
			}
			finally
			{
				InAutoCompleteMode = false;
			}
		}

		private static void AutoCompleteFunctionParameters(FunctionInfo function, string fullString)
		{
			string[] words = fullString.ToLower().Split('.');

			for (int i = 0; i < function.Parameters.Count; i++)
			{
				if (words.Length == 1 && function.Parameters[i].Name.ToLower().IndexOf(words[0]) == 0)
				{
					// AutoComplete
					IntelliPromptMemberListItem item = new IntelliPromptMemberListItem(function.Parameters[i].Name, (int)ActiproSoftware.Products.SyntaxEditor.IconResource.PublicField, "(parameter) " + function.Parameters[i].DataType + " " + function.Parameters[i].Name);
					bool alreadyExists = false;

					for (int itemCounter = 0; itemCounter < Editor.IntelliPrompt.MemberList.Count; itemCounter++)
					{
						if (Editor.IntelliPrompt.MemberList[itemCounter].Text == function.Parameters[i].Name)
						{
							alreadyExists = true;
							break;
						}
					}
					if (!alreadyExists)
					{
						Editor.IntelliPrompt.MemberList.Add(item);
					}
				}
			}
		}

		private static void AutoCompleteSpecialWords(string fullString)
		{
			string[] words = fullString.ToLower().Split('.');
			string[] specialWords = new[] { "Write", "WriteLine", "WriteFormat", "SkipCurrentFile" };

			foreach (string specialWord in specialWords)
			{
				if (words.Length == 1 && specialWord.ToLower().IndexOf(words[0]) == 0)
				{
					// AutoComplete
					IntelliPromptMemberListItem item = new IntelliPromptMemberListItem(specialWord, (int)ActiproSoftware.Products.SyntaxEditor.IconResource.InternalMethod, specialWord);
					bool alreadyExists = false;

					for (int itemCounter = 0; itemCounter < Editor.IntelliPrompt.MemberList.Count; itemCounter++)
					{
						if (Editor.IntelliPrompt.MemberList[itemCounter].Text == specialWord)
						{
							alreadyExists = true;
							break;
						}
					}
					if (!alreadyExists)
					{
						Editor.IntelliPrompt.MemberList.Add(item);
					}
				}
			}
		}

		private static void AutoCompleteUserOptions(string fullString)
		{
			string[] words = fullString.ToLower().Split('.');

			if (words.Length == 1 && "UserOptions".IndexOf(words[0], StringComparison.InvariantCultureIgnoreCase) == 0)
			{
				IntelliPromptMemberListItem item = new IntelliPromptMemberListItem("UserOptions", (int)ActiproSoftware.Products.SyntaxEditor.IconResource.PublicField, "UserOptions");
				Editor.IntelliPrompt.MemberList.Add(item);
				return;
			}
			for (int i = 0; i < Project.Instance.UserOptions.Count; i++)
			{
				if (words.Length == 1 && Project.Instance.UserOptions[i].VariableName.ToLower().IndexOf(words[0]) == 0)
				{
					// AutoComplete
					IntelliPromptMemberListItem item = new IntelliPromptMemberListItem(Project.Instance.UserOptions[i].VariableName, (int)ActiproSoftware.Products.SyntaxEditor.IconResource.PublicField, "(user option) " + Project.Instance.UserOptions[i].VarType + " " + Project.Instance.UserOptions[i].VariableName);
					bool alreadyExists = false;

					for (int itemCounter = 0; itemCounter < Editor.IntelliPrompt.MemberList.Count; itemCounter++)
					{
						if (Editor.IntelliPrompt.MemberList[itemCounter].Text == Project.Instance.UserOptions[i].VariableName)
						{
							alreadyExists = true;
							break;
						}
					}
					if (!alreadyExists)
					{
						Editor.IntelliPrompt.MemberList.Add(item);
					}
				}
			}
		}

		private static void AutoCompleteConstants(string fullString)
		{
			string[] words = fullString.ToLower().Split('.');

			for (int i = 0; i < Project.Instance.Constants.Length; i++)
			{
				if (words.Length == 1 && Project.Instance.Constants[i].Name.ToLower().IndexOf(words[0]) == 0)
				{
					// AutoComplete
					IntelliPromptMemberListItem item = new IntelliPromptMemberListItem(Project.Instance.Constants[i].Name, (int)ActiproSoftware.Products.SyntaxEditor.IconResource.PublicField, "(constant) " + Project.Instance.Constants[i].DataType + " " + Project.Instance.Constants[i].Name);
					bool alreadyExists = false;

					for (int itemCounter = 0; itemCounter < Editor.IntelliPrompt.MemberList.Count; itemCounter++)
					{
						if (Editor.IntelliPrompt.MemberList[itemCounter].Text == Project.Instance.Constants[i].Name)
						{
							alreadyExists = true;
							break;
						}
					}
					if (!alreadyExists)
					{
						Editor.IntelliPrompt.MemberList.Add(item);
					}
				}
			}
		}

		private static void AutoCompleteFunctions(string fullString)
		{
			string[] words = fullString.ToLower().Split('.');

			for (int i = 0; i < Project.Instance.Functions.Count; i++)
			{
				if (words.Length == 1 && Project.Instance.Functions[i].Name.ToLower().IndexOf(words[0]) == 0)
				{
					// AutoComplete
					IntelliPromptMemberListItem item = new IntelliPromptMemberListItem(Project.Instance.Functions[i].Name, (int)ActiproSoftware.Products.SyntaxEditor.IconResource.PublicField, "(function) " + Project.Instance.Functions[i].ReturnType + " " + Project.Instance.Functions[i].Name);
					bool alreadyExists = false;

					for (int itemCounter = 0; itemCounter < Editor.IntelliPrompt.MemberList.Count; itemCounter++)
					{
						if (Editor.IntelliPrompt.MemberList[itemCounter].Text == Project.Instance.Functions[i].Name)
						{
							alreadyExists = true;
							break;
						}
					}
					if (!alreadyExists)
					{
						Editor.IntelliPrompt.MemberList.Add(item);
					}
				}
			}
		}

		private static void AutoCompleteLocalVariables(string fullString, SyntaxEditor editor)
		{
			// Let's find if this variable was declared anywhere
			string parentVar2 = fullString;

			if (fullString.IndexOf(".") > 0)
			{
				parentVar2 = fullString.Substring(0, fullString.IndexOf("."));
			}
			Regex regex = new Regex(@"([a-zA-Z0-9]+)\s+(" + parentVar2 + "[a-zA-Z0-9]+)", RegexOptions.RightToLeft | RegexOptions.Multiline);
			MatchCollection matches = regex.Matches(editor.Text, editor.Caret.Offset - fullString.Length);

			foreach (Match match in matches)
			{
				if (match != null)
				{
					string dataTypeName = match.Value;
					string variableName;

					if (match.Groups.Count > 1)
					{
						dataTypeName = match.Groups[1].Value.Trim();
						variableName = match.Groups[2].Value.Trim();
						bool alreadyExists = false;

						for (int itemCounter = 0; itemCounter < Editor.IntelliPrompt.MemberList.Count; itemCounter++)
						{
							if (Editor.IntelliPrompt.MemberList[itemCounter].Text == variableName)
							{
								alreadyExists = true;
								break;
							}
						}
						if (!alreadyExists)
						{
							IntelliPromptMemberListItem item = new IntelliPromptMemberListItem(variableName, (int)ActiproSoftware.Products.SyntaxEditor.IconResource.InternalField, "(local variable) " + dataTypeName + " " + variableName);
							Editor.IntelliPrompt.MemberList.Add(item);
						}
					}
				}
			}
			return;
		}
		#endregion // AutoComplete Functions

		/// <summary>
		/// Implements Intellisense dropdown.
		/// </summary>
		/// <param name="editor"></param>
		/// <param name="currentFunction"></param>
		public static void GetIntelliSenseList(ref SyntaxEditor editor, FunctionInfo currentFunction)
		{
			Editor = editor;

			if (Editor.IntelliPrompt.MemberList.ImageList == null)
			{
				Editor.IntelliPrompt.MemberList.ImageList = SyntaxEditor.ReflectionImageList;
			}
			Editor.IntelliPrompt.MemberList.HideOnParentFormDeactivate = true;
			Editor.IntelliPrompt.MemberList.MatchBasedOnItemPreText = true;
			Editor.IntelliPrompt.MemberList.Clear();
			string fullTypeString = GetFullString();

			if (fullTypeString == "UserOptions")
			{
				ListAllUserOptions(null);
				return;
			}
			if (fullTypeString.Length > 0)
			{
				// Process in order of least expensive to most expensive
				if (FindInUserOptions(fullTypeString)) { return; }
				if (FindInFunctions(fullTypeString)) { return; }
				if (FindInFunctionParameters(currentFunction, fullTypeString)) { return; }
				AddedProperties.Clear();
				if (FindInLocalVariables(fullTypeString, editor)) { return; }
				if (FindInAssemblies(fullTypeString)) { return; }
			}
		}

		public static void GetIntelliSenseParameterList(ref SyntaxEditor editor, FunctionInfo currentFunction)
		{
			Editor = editor;

			if (Editor.IntelliPrompt.MemberList.ImageList == null)
			{
				Editor.IntelliPrompt.MemberList.ImageList = SyntaxEditor.ReflectionImageList;
			}
			Editor.IntelliPrompt.MemberList.HideOnParentFormDeactivate = true;
			Editor.IntelliPrompt.MemberList.MatchBasedOnItemPreText = true;
			Editor.IntelliPrompt.MemberList.Clear();
			string fullTypeString = GetFullString();

			if (fullTypeString.Length > 0)
			{
				// Process in order of least expensive to most expensive
				if (FindInUserOptions(fullTypeString)) { return; }
				if (FindInFunctions(fullTypeString)) { return; }
				if (FindInFunctionParameters(currentFunction, fullTypeString)) { return; }
				AddedProperties.Clear();
				if (FindInLocalVariables(fullTypeString, editor)) { return; }
				if (FindInAssemblies(fullTypeString)) { return; }
			}
		}

		#region Find Methods

		private static void AddItemToMemberList(string displayName, string tooltipText, ActiproSoftware.Products.SyntaxEditor.IconResource icon)
		{
			bool alreadyExists = false;

			for (int i = 0; i < Editor.IntelliPrompt.MemberList.Count; i++)
			{
				if (Editor.IntelliPrompt.MemberList[i].Text == displayName &&
					Editor.IntelliPrompt.MemberList[i].ImageIndex == (int)icon)
				{
					alreadyExists = true;
					break;
				}
			}
			if (!alreadyExists)
			{
				Editor.IntelliPrompt.MemberList.Add(new IntelliPromptMemberListItem(displayName, (int)icon, tooltipText));
			}

		}

		private static bool FindInAssemblies(string fullString)
		{
			List<string> namespaces = Project.Instance.Namespaces;
			Type parentType = null;
			string[] words = fullString.Split('.');

			if (InAutoCompleteMode)
			{
				words[words.Length - 1] = words[words.Length - 1].ToLower();
			}
			fullString = "";

			for (int i = 0; i < words.Length; i++)
			{
				if (i > 0)
				{
					fullString += ".";
				}
				fullString += words[i];
			}
			GetValueType(words[0], ref parentType);

			if (words.Length > 0)
			{
				foreach (Assembly ass in GetReferencedAssemblies())
				{
					string fullAssemblyName = ass.GetName().Name;

					if (!AssemblyTypes.ContainsKey(ass))
					{
						List<LookupType> luTypes = new List<LookupType>();

						foreach (Type type in ass.GetTypes())
						{
							if (type.IsPublic ||
								type.IsNestedPublic)
							{
								luTypes.Add(new LookupType(type));
							}
						}
						AssemblyTypes.Add(ass, luTypes);
					}
					foreach (LookupType subType in AssemblyTypes[ass])
					{
						string[] originalWords = words;

						for (int nsCounter = namespaces.Count; nsCounter >= 0; nsCounter--)
						{
							if (nsCounter == namespaces.Count)
							{
								words = originalWords;
							}
							else
							{
								if (namespaces[nsCounter].IndexOf(fullAssemblyName) < 0)
								{
									continue;
								}
								string[] nsParts = namespaces[nsCounter].Split('.');
								words = new string[nsParts.Length + originalWords.Length];
								Array.Copy(nsParts, words, nsParts.Length);
								Array.Copy(originalWords, 0, words, nsParts.Length, originalWords.Length);
							}
							if (InAutoCompleteMode)
							{
								words[words.Length - 1] = words[words.Length - 1].ToLower();
							}
							string wordToFind = "";

							for (int i = 0; i < words.Length; i++)
							{
								if (i > 0)
								{
									wordToFind += ".";
								}
								wordToFind += words[i];
							}
							// Check whether the user has omitted the full namespace name
							if (subType.FullTypeString.IndexOf(wordToFind.ToLower()) > 0)
							{
								foreach (string ns in namespaces)
								{
									string tempTypeName = string.Format("{0}.{1}", ns, wordToFind);

									if (subType.FullTypeString.IndexOf(tempTypeName.ToLower()) == 0)
									{
										wordToFind = subType.Type.FullName;
										//wordToFind = tempTypeName.ToLower();
										words = wordToFind.Replace('+', '.').Split('.');

										//if (subType.Type.IsEnum)
										//{
										//    int lastPeriod = wordToFind
										//}
										//wordToFind = subType.FullTypeString;
										break;
									}
								}
								//wordToFind = subType.FullTypeString;
							}
							if (subType.FullTypeString.IndexOf(wordToFind.ToLower()) == 0
								|| subType.Type.FullName == wordToFind)
							{
								// We have found a matching type
								string displayName = "";

								if (InAutoCompleteMode)
								{
									if (words.Length - 1 < subType.AllWordsLowerCase.Length)
									{
										displayName += subType.AllWords[words.Length - 1];
									}
								}
								else // Intellisense mode
								{
									if (words.Length < subType.AllWordsLowerCase.Length)
									{
										displayName += subType.AllWords[words.Length];
									}
								}
								if (Slyce.Common.Utility.StringsAreEqual(subType.Type.FullName, wordToFind, true))
								{
									AutoCompleteFromType(subType.Type, "");
								}
								else
								{
									if (displayName.Length > 0 &&
										(subType.Type.FullName.LastIndexOf(".") > 0 || subType.Type.FullName.LastIndexOf(".") > 0) &&
										(displayName == subType.Type.FullName.Substring(subType.Type.FullName.LastIndexOf(".") + 1) ||
										displayName == subType.Type.FullName.Substring(subType.Type.FullName.LastIndexOf("+") + 1)))
									{
										//AddItemToMemberList(displayName, string.Format("(class) {0} {1}", displayName, ""), ActiproSoftware.Products.SyntaxEditor.IconResource.PublicClass);
										AddItemToMemberList(displayName, string.Format("(class) {0} {1}", subType.Type.FullName, ""), ActiproSoftware.Products.SyntaxEditor.IconResource.PublicClass);
									}
									else if (displayName.Length > 0)
									{
										//AddItemToMemberList(displayName, string.Format("(namespace) {0} {1}", displayName, ""), ActiproSoftware.Products.SyntaxEditor.IconResource.Namespace);
										AddItemToMemberList(displayName, string.Format("(namespace) {0} {1}", subType.Type.FullName.Replace("+", "."), ""), ActiproSoftware.Products.SyntaxEditor.IconResource.Namespace);
									}
								}
							}
							else if (InAutoCompleteMode && wordToFind.Substring(wordToFind.Length - 1) == "." && subType.FullTypeString.IndexOf(wordToFind.Substring(0, wordToFind.Length - 1)) == 0)
							{
								// The last char was a period, so wee need to get all sub types and properties, methods, fields etc.
								AutoCompleteFromType(subType.Type, "");
							}
							else if (!InAutoCompleteMode && subType.FullTypeString.IndexOf(wordToFind) == 0)
							{
								// The last char was a period, so we need to get all sub types and properties, methods, fields etc.
								AutoCompleteFromType(subType.Type, "");
							}
							else if (Slyce.Common.Utility.StringsAreEqual(wordToFind, subType.FullTypeString, true))
							{
								// We need to now check properties, methods, fields etc
								string propName = wordToFind;

								if (wordToFind.ToLower().IndexOf(subType.FullTypeString + ".") == 0)
								{
									propName = propName.Substring(subType.FullTypeString.Length + 1);
								}
								//string propName = wordToFind.ToLower().Replace(subType.FullTypeString + ".", "");
								Type parentTempType = ass.GetType(subType.Type.FullName, false, true);
								ProcessChildren(parentTempType, propName.Split('.'));
							}
						}
						words = originalWords;
					}
				}
			}
			return false;
		}

		/// <summary>
		/// Adds all UserOptions to the IntelliSense dropdown list. Happens when the user types 'UserOptions.'
		/// </summary>
		private static void ListAllUserOptions(Type objectType)
		{
			foreach (UserOption userOption in Project.Instance.UserOptions)
			{
				// Only add global UserOptions, ie: ones that have no iterator
				if (objectType == null)
				{
					if (userOption.IteratorType == null)
					{
						AddItemToMemberList(userOption.VariableName, string.Format("(field) {0} {1}", userOption.VariableName, userOption.VarType), ActiproSoftware.Products.SyntaxEditor.IconResource.PublicField);
					}
				}
				else if (userOption.IteratorType != null && (userOption.IteratorType == objectType || objectType.IsSubclassOf(userOption.IteratorType)))
				{
					AddItemToMemberList(userOption.VariableName, string.Format("(field) {0} {1}", userOption.VariableName, userOption.VarType), ActiproSoftware.Products.SyntaxEditor.IconResource.PublicField);
				}
			}
		}

		private static bool FindInUserOptions(string fullString)
		{
			string[] words = fullString.Split('.');

			for (int i = 0; i < Project.Instance.UserOptions.Count; i++)
			{
				if (words[0] == Project.Instance.UserOptions[i].VariableName)
				{
					Type parentType = Project.Instance.UserOptions[i].VarType;

					if (parentType == null)
					{
						parentType = Project.Instance.UserOptions[i].VarType;
					}
					if (parentType != null)
					{
						return ProcessChildren(parentType, words);
					}
					return parentType != null;
				}
			}
			return false;
		}

		private static bool FindInFunctions(string fullString)
		{
			string[] words = fullString.Split('.');

			foreach (FunctionInfo function in Project.Instance.Functions)
			{
				if (function.Name == words[0])
				{
					Type parentType;

					if (function.IsTemplateFunction)
					{
						parentType = typeof(string);
					}
					else
					{
						parentType = function.ReturnType;

						if (parentType == null)
						{
							parentType = function.ReturnType;
						}
					}
					if (parentType != null)
					{
						return ProcessChildren(parentType, words);
					}
				}
			}
			return false;
		}

		private static bool FindInFunctionParameters(FunctionInfo function, string fullString)
		{
			string[] words = fullString.Split('.');

			if (function != null)
			{
				for (int i = 0; i < function.Parameters.Count; i++)
				{
					// Get the full type
					ParamInfo param = function.Parameters[i];

					if (param.Name == words[0])
					{
						//Type parentType = GetTypeFromAssemblies(param.DataType, words.Length == 1);
						Type parentType = param.DataType;

						if (parentType != null)
						{
							string[] subWords = new string[words.Length - 1];
							Array.Copy(words, 1, subWords, 0, words.Length - 1);

							return ProcessChildren(parentType, subWords);
						}
					}
				}
			}
			return false;
		}

		private static bool FindInLocalVariables(string fullString, SyntaxEditor editor)
		{
			string[] words = fullString.Split('.');
			Type parentType = null;

			// Let's find if this variable was declared anywhere
			string parentVar2 = fullString;

			if (fullString.IndexOf(".") > 0)
			{
				parentVar2 = fullString.Substring(0, fullString.IndexOf("."));
			}
			if (parentVar2.IndexOf("[") >= 0)
			{
				// Typing attributes such as [Serializable] causes the Regex to blow up.
				int startOfWord = fullString.IndexOf("[") + 1;

				if (fullString.Length >= startOfWord)
				{
					parentVar2 = fullString.Substring(startOfWord);
				}
			}
			//Regex regex = new Regex(@"([a-zA-Z0-9]+)\s+" + parentVar2, RegexOptions.RightToLeft | RegexOptions.Multiline);
			// Look for type followed by a space then the variable name eg: 'String mystring.....'
			Regex regex = new Regex(@"([a-zA-Z0-9.]+)\s+" + parentVar2 + @"(\s+|;|,)", RegexOptions.RightToLeft | RegexOptions.Multiline);
			MatchCollection matches = regex.Matches(editor.Document.GetText(LineTerminator.Newline), editor.Caret.Offset - fullString.Length);

			foreach (Match match in matches)
			{
				if (match != null)
				{
					if (match.Groups.Count > 1)
					{
						string dataTypeName = match.Groups[1].Value.Trim();

						if (dataTypeName == "in") { continue; }

						dataTypeName = GetValueType(dataTypeName);

						GetValueType(dataTypeName, ref parentType);

						if (parentType == null)
						{
							parentType = GetTypeFromAssemblies(dataTypeName, words.Length == 1);

							if (fullString.IndexOf(".") > 0)
							{
								FindInAssemblies(dataTypeName + "." + fullString.Substring(fullString.IndexOf(".") + 1));
							}
							else
							{
								FindInAssemblies(dataTypeName);
							}
							//parentType = Project.Instance.GetTypeFromReferencedAssemblies(dataTypeName, false);
						}
						if (parentType != null)
						{
							ProcessChildren(parentType, words);
						}
						return parentType != null;
					}
				}
			}
			return false;
		}

		#endregion

		#region Helper Methods

		private static bool ProcessChildren(Type parentType, string[] words)
		{
			if (words.Length == 1 && words[0] == "UserOptions")
			{
				ListAllUserOptions(parentType);
				return true;
			}
			if (words.Length >= 2 && words[words.Length - 2] == "UserOptions")
			{
				// We are dealing with a user option for a specific type of iterator
				foreach (UserOption userOption in Project.Instance.UserOptions)
				{
					if (userOption.VariableName == words[words.Length - 1])
					{
						if (userOption.VarType == typeof(Enum))
						{
							foreach (string val in userOption.Values)
							{
								AddItemToMemberList(val, string.Format("(field) {0} {1}", val, "string"), ActiproSoftware.Products.SyntaxEditor.IconResource.PublicField);
							}
							return true;
						}
						AutoCompleteFromType(userOption.VarType, "");
						return true;
					}
				}
				return false;
			}
			if (words.Length >= 2 && words[words.Length - 1] == "UserOptions")
			{
				// We are dealing with a user option for a specific type of iterator
				foreach (UserOption userOption in Project.Instance.UserOptions)
				{
					if (userOption.IteratorType == parentType)
					{
						AddItemToMemberList(userOption.VariableName, string.Format("(field) {0} {1}", userOption.VariableName, userOption.VarType.Name), ActiproSoftware.Products.SyntaxEditor.IconResource.PublicField);
					}
				}
				return true;
			}
			int currentDepth = 0;
			Type originalType = parentType;
			Type tempType;

			while (currentDepth < words.Length && parentType != null)
			{
				if (words[currentDepth] == "[]")
				{
					if (parentType.FullName.LastIndexOf("[]") > 0)
					{
						tempType = GetTypeFromAssemblies(parentType.FullName.Substring(0, parentType.FullName.LastIndexOf("[]")), words.Length == 1);
					}
					else
					{
						tempType = GetTypeFromAssemblies(parentType.FullName, words.Length == 1);
					}
					if (tempType != null)
					{
						if (tempType.IsGenericType)
						{
							Type[] genericArgs = tempType.GetGenericArguments();
							tempType = genericArgs[genericArgs.Length - 1];
						}
						parentType = tempType;
					}
				}
				else if (parentType.GetProperty(words[currentDepth]) != null)
				{
					tempType = parentType.GetProperty(words[currentDepth]).PropertyType;
					if (tempType != null) { parentType = tempType; }
				}
				else if (parentType.GetField(words[currentDepth]) != null)
				{
					tempType = parentType.GetField(words[currentDepth]).FieldType;
					if (tempType != null) { parentType = tempType; }
				}
				else if (parentType.GetNestedType(words[currentDepth]) != null)
				{
					tempType = parentType.GetNestedType(words[currentDepth]);
					if (tempType != null) { parentType = tempType; }
				}
				else
				{
					MethodInfo[] methods = parentType.GetMethods();
					bool matchingMethodFound = false;

					foreach (MethodInfo method in methods)
					{
						if (method.Name == words[currentDepth])
						{
							parentType = method.ReturnType;
							matchingMethodFound = true;
						}
					}
					foreach (var func in Project.Instance.Functions)
					{
						if (func.IsExtensionMethod && words[currentDepth] == func.Name)
						{
							parentType = func.ReturnType;
							matchingMethodFound = true;
						}
					}
					if (!matchingMethodFound)
					{
						if (InAutoCompleteMode)
						{
							AutoCompleteFromType(parentType, words[currentDepth]);
							return true;
						}
					}
				}
				currentDepth++;
			}
			if (parentType != null &&
				parentType != typeof(void) &&
				!InAutoCompleteMode)
			{
				AutoCompleteFromType(parentType, "");
				return true;
			}
			if (originalType != null &&
				parentType != typeof(void))
			{
				AutoCompleteFromType(originalType, "");
				return true;
			}
			return false;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="parentType"></param>
		/// <param name="partialWord">Empty string for Intellisense, partialType for AutoCompletion</param>
		private static void AutoCompleteFromType(Type parentType, string partialWord)
		{
			partialWord = partialWord.ToLower();

			if (parentType.IsEnum)
			{
				foreach (string name in Enum.GetNames(parentType))
				{
					if (partialWord.Length == 0 || name.ToLower().IndexOf(partialWord) == 0)
					{
						AddItemToMemberList(name, string.Format("{0}", name), ActiproSoftware.Products.SyntaxEditor.IconResource.EnumerationItem);
					}
				}
				return;
			}
			foreach (PropertyInfo prop in parentType.GetProperties())
			{
				if (partialWord.Length == 0 || prop.Name.ToLower().IndexOf(partialWord) == 0)
				{
					AddItemToMemberList(prop.Name, string.Format("(property) {0} {1}", prop.Name, prop.PropertyType), ActiproSoftware.Products.SyntaxEditor.IconResource.PublicProperty);
				}
			}
			foreach (MethodInfo meth in parentType.GetMethods())
			{
				if (partialWord.Length == 0 || meth.Name.ToLower().IndexOf(partialWord) == 0)
				{
					bool isSpecialName = (meth.Attributes & MethodAttributes.SpecialName) == MethodAttributes.SpecialName;

					if (!isSpecialName)
					{
						AddItemToMemberList(meth.Name, string.Format("(method) {0} {1}", meth.Name, meth.ReturnType), ActiproSoftware.Products.SyntaxEditor.IconResource.PublicMethod);
					}
				}
			}
			foreach (Type nestedType in parentType.GetNestedTypes())
			{
				if (!nestedType.IsEnum)
					continue;

				if (partialWord.Length == 0 || nestedType.Name.ToLower().IndexOf(partialWord) == 0)
				{
					AddItemToMemberList(nestedType.Name, string.Format("(enum) {0}", nestedType.Name), ActiproSoftware.Products.SyntaxEditor.IconResource.PublicEnumeration);
				}
			}
			foreach (FieldInfo field in parentType.GetFields())
			{
				if (partialWord.Length == 0 || field.Name.ToLower().IndexOf(partialWord) == 0)
				{
					AddItemToMemberList(field.Name, string.Format("(field) {0} {1}", field.Name, field.FieldType), ActiproSoftware.Products.SyntaxEditor.IconResource.PublicField);
				}
			}
			// Are there any virtual properties for this type?
			foreach (UserOption userOption in Project.Instance.UserOptions)
			{
				if (userOption.IteratorType == parentType)
				{
					AddItemToMemberList("UserOptions", string.Format("(field) {0} {1}", "UserOptions", "Project.UserOption"),
										ActiproSoftware.Products.SyntaxEditor.IconResource.PublicField);
					break;
				}
			}
			// Check for Extension Methods
			foreach (var function in Project.Instance.Functions)
			{
				if (function.IsExtensionMethod)
					if (function.ExtendedType == parentType.FullName)
						AddItemToMemberList(function.Name, string.Format("(method) {0} {1}", "UserOptions", "Project.UserOption"),
										ActiproSoftware.Products.SyntaxEditor.IconResource.PublicExtensionMethod);

			}
		}

		/// <summary>
		/// Gets the full paths of all referenced assemblies
		/// </summary>
		/// <returns></returns>
		private static Assembly[] GetReferencedAssemblies()
		{
			// TODO: performance improvement: cache these assemblies and refresh only when new assemblies
			// or namespaces are added via the project detail screen.
			if (ReferencedAssemblies == null)
			{
				string[] envPaths1 = System.Environment.GetEnvironmentVariable("Path").Split(';');
				string[] envPaths = new string[envPaths1.Length + 2];
				List<string> validFiles = new List<string>();
				List<ReferencedFile> invalidFiles = new List<ReferencedFile>();

				for (int i = 0; i < envPaths1.Length; i++)
				{
					envPaths[i + 2] = envPaths1[i];
				}
				envPaths[0] = System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory();
				envPaths[1] = Path.GetDirectoryName(Application.ExecutablePath);
				string fileName;

				foreach (var refFile in Project.Instance.References)
				{
					fileName = refFile.FileName;

					if (File.Exists(fileName))
					{
						validFiles.Add(fileName);
					}
					else
					{
						if (fileName.Length > 0)
						{
							string dir = Path.GetDirectoryName(fileName);

							if (dir.Length == 0)
							{
								bool found = false;

								for (int x = 0; x < envPaths.Length; x++)
								{
									fileName = Path.Combine(envPaths[x], Path.GetFileName(fileName));

									if (File.Exists(fileName))
									{
										found = true;
										validFiles.Add(fileName);
										break;
									}
								}
								if (!found)
								{
									invalidFiles.Add(refFile);
								}
							}
						}
					}
				}
				Assembly[] assemblies = new Assembly[validFiles.Count + 2];

				for (int i = 0; i < validFiles.Count; i++)
				{
					assemblies[i] = Assembly.Load(Path.GetFileNameWithoutExtension(validFiles[i]));
				}
				foreach (AssemblyName assemblyName in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
				{
					if (assemblyName.Name == "System")
					{
						assemblies[assemblies.Length - 1] = Assembly.Load(assemblyName);
					}
					else if (assemblyName.Name == "mscorlib")
					{
						assemblies[assemblies.Length - 2] = Assembly.Load(assemblyName);
					}
				}
				ReferencedAssemblies = assemblies;

				string invalid = "The following referenced assemblies could not be found:\n\n";
				for (int i = 0; i < invalidFiles.Count; i++)
				{
					invalid += invalidFiles[i] + "\n";
				}
				if (invalidFiles.Count > 0)
				{
					MessageBox.Show(Controller.Instance.MainForm, invalid, "Missing files", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			return ReferencedAssemblies;
		}

		/// <summary>
		/// Get the short name version of system types
		/// </summary>
		/// <param name="fullName"></param>
		/// <returns></returns>
		private static string GetValueType(string fullName)
		{
			Type dummy = typeof(string);
			return GetValueType(fullName, ref dummy);
		}

		private static string GetValueType(string fullName, ref Type type)
		{
			switch (fullName)
			{
				case "bool":
				case "Boolean":
				case "System.Boolean":
					fullName = "Boolean";
					type = typeof(bool);
					break;
				case "byte":
				case "Byte":
				case "System.Byte":
					fullName = "Byte";
					type = typeof(byte);
					break;
				case "char":
				case "system.char":
					fullName = "Char";
					type = typeof(char);
					break;
				case "decimal":
				case "Decimal":
				case "System.Decimal":
					fullName = "Decimal";
					type = typeof(decimal);
					break;
				case "double":
				case "Double":
				case "System.Double":
					fullName = "Double";
					type = typeof(double);
					break;
				case "short":
				case "Int16":
				case "System.Int16":
					fullName = "Int16";
					type = typeof(short);
					break;
				case "int":
				case "Int32":
				case "System.Int32":
					fullName = "Int32";
					type = typeof(int);
					break;
				case "long":
				case "Int64":
				case "System.Int64":
					fullName = "Int64";
					type = typeof(long);
					break;
				case "object":
				case "Object":
				case "System.Object":
					fullName = "Object";
					type = typeof(object);
					break;
				case "sbyte":
				case "SByte":
				case "System.SByte":
					fullName = "SByte";
					type = typeof(sbyte);
					break;
				case "float":
				case "Single":
				case "System.Single":
					fullName = "Single";
					type = typeof(float);
					break;
				case "string":
				case "String":
				case "System.String":
					fullName = "String";
					type = typeof(string);
					break;
				case "ushort":
				case "UInt16":
				case "System.UInt16":
					fullName = "UInt16";
					type = typeof(ushort);
					break;
				case "uint":
				case "UInt32":
				case "System.UInt32":
					fullName = "UInt32";
					type = typeof(uint);
					break;
				case "ulong":
				case "UInt64":
				case "System.UInt64":
					fullName = "UInt64";
					type = typeof(ulong);
					break;
				case "void":
				case "Void":
				case "System.Void":
					fullName = "Void";
					type = typeof(void);
					break;
				default:
					type = null;
					break;
			}
			return fullName;
		}

		private static string GetFullString()
		{
			// Construct full name of item to see if reflection can be used... iterate backwards through the token stream
			TokenStream stream;

			if (InAutoCompleteMode)
			{
				stream = Editor.Document.GetTokenStream(Editor.Document.Tokens.IndexOf(Editor.SelectedView.Selection.EndOffset));
			}
			else
			{
				stream = Editor.Document.GetTokenStream(Editor.Document.Tokens.IndexOf(Editor.SelectedView.Selection.EndOffset - 1));
			}
			string fullName = String.Empty;
			int periods = 0;
			int numOpenParentheses = 0;
			int numClosedParentheses = 0;
			int numOpenSquareBraces = 0;
			int numClosedSquareBraces = 0;

			while (stream.Position > 0)
			{
				DynamicToken token = (DynamicToken)stream.ReadReverse();

				switch (token.Key)
				{
					case "IdentifierToken":
					case "NativeTypeToken":
						if (numClosedParentheses == numOpenParentheses)
						{
							fullName = Editor.Document.GetTokenText(token) + fullName;
						}
						break;
					case "PunctuationToken":
						if ((token.Length == 1) && (Editor.Document.GetTokenText(token) == "."))
						{
							if (numClosedParentheses == numOpenParentheses)
							{
								fullName = Editor.Document.GetTokenText(token) + fullName;
								periods++;
							}
						}
						else
							stream.Position = 0;
						break;
					case "CloseParenthesisToken":
						numClosedParentheses++;
						break;
					case "OpenParenthesisToken":
						numOpenParentheses++;

						if (numOpenParentheses != numClosedParentheses)
						{
							stream.Position = 0;
						}
						break;
					case "CloseSquareBraceToken":
						numClosedSquareBraces++;
						fullName = "]" + fullName;
						break;
					case "OpenSquareBraceToken":
						numOpenSquareBraces++;
						fullName = ".[" + fullName;

						if (numOpenSquareBraces != numClosedSquareBraces)
						{
							stream.Position = 0;
						}
						break;
					case "IntegerNumberToken":
						if (numClosedSquareBraces <= numOpenSquareBraces)
						{
							stream.Position = 0;
						}
						break;
					case "StringEndToken":
						fullName = "System.String";
						break;
					default:
						stream.Position = 0;
						break;
				}
			}
			return fullName;
		}

		private static Type GetTypeFromAssemblies(string typeName, bool isSingleWord)
		{
			bool isArray = false;

			if (typeName.Length > 1)
			{
				isArray = typeName.Substring(typeName.Length - 2) == "[]";
			}
			if (isArray)
			{
				typeName = typeName.Substring(0, typeName.Length - 2);
			}
			Type theType = null;

			Type valueType = null;
			GetValueType(typeName, ref valueType);

			if (valueType != null)
			{
				if (isArray)
				{
					valueType = Array.CreateInstance(valueType, 0).GetType();
				}
				return valueType;
			}
			// Check actual assembly names first, but only if one word exists
			foreach (Assembly ass in GetReferencedAssemblies())
			{
				if (ass.GetName().Name == typeName)
				{
					return ass.GetType();
				}
			}
			foreach (Assembly ass in GetReferencedAssemblies())
			{
				if (ass.GetType(typeName, false, true) != null)
				{
					theType = ass.GetType(typeName, false, true);

					if (isArray)
					{
						theType = Array.CreateInstance(theType, 0).GetType();
					}
					return theType;
				}
				// Check whether we can find it in any of the referenced namespaces
				string name = ass.GetName().Name;

				foreach (string nameSpace in Project.Instance.Namespaces)
				{
					if (nameSpace.IndexOf(name) == 0 && nameSpace.IndexOf(".") > 0)
					{
						string fullyQualifiedDataType = nameSpace + "." + typeName;

						if (ass.GetType(fullyQualifiedDataType, false, true) != null)
						{
							theType = ass.GetType(fullyQualifiedDataType, false, true);

							if (isArray)
							{
								theType = Array.CreateInstance(theType, 0).GetType();
							}
							return theType;
						}
					}
				}
			}
			if (theType != null && isArray)
			{
				theType = Array.CreateInstance(theType, 0).GetType();
			}
			return theType;
		}
		#endregion
	}
}