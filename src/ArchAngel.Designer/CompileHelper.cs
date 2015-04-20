using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using ArchAngel.Designer.DesignerProject;
using ArchAngel.Designer.Properties;
using ArchAngel.Interfaces;
using Microsoft.CSharp;
using Slyce.Common;
using DefaultValueFunction = ArchAngel.Designer.DesignerProject.DefaultValueFunction;
using UserOption = ArchAngel.Common.DesignerProject.UserOption;

namespace ArchAngel.Designer
{
	class CompileHelper
	{
		public static string SourceFile = "";
		public static bool DebugVersion;
		internal static string ParsedCode = "";
		public static readonly List<string> ScriptFiles = new List<string>();
		/// <summary>
		/// The key to this dictionary is the line number in the compiled .cs file.
		/// </summary>
		public static Dictionary<int, List<CompiledToTemplateLineLookup>> TemplateLinesLookup = new Dictionary<int, List<CompiledToTemplateLineLookup>>();
		/// <summary>
		/// The key to this dictionary is the line number and name of the original template function.
		/// </summary>
		public static CompiledLineNumberMap CompiledLinesLookup;
		public static bool RandomiseTheNamespace;
		public static string NamespaceUsed = Project.Instance.ProjectNamespace;
		private static int RandomNamespaceNumber = 1;
		private static readonly Dictionary<string, FunctionInfo> FunctionHashes = new Dictionary<string, FunctionInfo>();
		public static string CompiledAssemblyFileName;
		const string openBrace = "{";
		const string closeBrace = "}";

		public static List<CompiledToTemplateLineLookup> GetNextCompiledToTemplateLineLookup(int lineNumber, out int nextLineNumber)
		{
			if (TemplateLinesLookup.ContainsKey(lineNumber))
			{
				nextLineNumber = lineNumber + 1;
				while (TemplateLinesLookup.ContainsKey(nextLineNumber) == false)
				{
					nextLineNumber++;
				}
				if (TemplateLinesLookup[nextLineNumber][0].Function == TemplateLinesLookup[lineNumber][0].Function)
				{
					return TemplateLinesLookup[nextLineNumber];
				}
			}
			nextLineNumber = -1;
			return null;
		}

		/// <summary>
		/// If the function is open, gets the text from the editor, otherwise gets the saved text.
		/// </summary>
		/// <param name="functionName">Name of function.</param>
		/// <param name="parameters"></param>
		public static string GetFunctionBody(string functionName, List<ParamInfo> parameters)
		{
			if (Controller.Instance.MainForm != null)
			{
				for (int i = 0; i < Controller.Instance.MainForm.UcFunctions.tabStrip1.Tabs.Count; i++)
				{
					if (Controller.Instance.MainForm.UcFunctions.tabStrip1.Tabs[i].Text == functionName)
					{
						return ((ucFunction)Controller.Instance.MainForm.UcFunctions.tabStrip1.Tabs[i].AttachedControl.Controls[0]).syntaxEditor1.Text.Replace("\r\n", "\n");
					}
				}
			}
			return Project.Instance.FindFunction(functionName, parameters).Body;
		}

		/// <summary>
		/// Adds debug symbols to the code of the function.
		/// </summary>
		/// <param name="code"></param>
		/// <param name="functionName"></param>
		/// <param name="language"></param>
		/// <returns></returns>
		public static string AddDebugSymbols(string code, string functionName, SyntaxEditorHelper.ScriptLanguageTypes language)
		{
			if (code.IndexOf("¬") < 0)
			{
				code = code.Replace("\n", "¬");
			}
			else
			{
				throw new Exception("Special substitution character (¬) has been used in code. Line end substitutions can't be performed.");
			}
			string[] lines = code.Split('¬');

			var sb2 = new StringBuilder(code.Length + lines.Length * 15);

			for (int lineCounter = 1; lineCounter <= lines.Length; lineCounter++)
			{
				switch (language)
				{
					case SyntaxEditorHelper.ScriptLanguageTypes.CSharp: // TODO: Investigate whether inserting at end is faster than appending, also whether using string.format is faster, also use AppendLine rather
						sb2.Append(lines[lineCounter - 1] + "/*DEBUG:" + lineCounter + "<" + functionName + ">*/" + Environment.NewLine);
						break;
					case SyntaxEditorHelper.ScriptLanguageTypes.VbNet:
						sb2.Append(lines[lineCounter - 1] + "'DEBUG:" + lineCounter + "<" + functionName + ">*/" + Environment.NewLine);
						break;
					default:
						throw new NotImplementedException("Language not catered for yet.");
				}
			}
			// Remove the last linebreak;
			if (sb2[sb2.Length - 1] == '\n')
			{
				sb2.Remove(sb2.Length - 1, 1);

				if (sb2[sb2.Length - 1] == '\r')
				{
					sb2.Remove(sb2.Length - 1, 1);
				}
			}
			else if (sb2[sb2.Length - 1] == '\r')
			{
				sb2.Remove(sb2.Length - 1, 1);
			}
			return sb2.ToString();
		}

		/// <summary>
		/// Gets a version of the text with UserOptions formatted for compiling.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static string ReplaceUserOptionCalls(string text)
		{
			StringBuilder sb = new StringBuilder(text);
			ReplaceUserOptionCalls(sb);
			return sb.ToString();
		}

		/// <summary>
		/// Formats the text in the stringbuilder ready for compiling, wrt UserOptions.
		/// </summary>
		/// <param name="sb"></param>
		private static void ReplaceUserOptionCalls(StringBuilder sb)
		{
			Type systemEnumType = typeof(Enum);
			string originalText = Utility.StandardizeLineBreaks(sb.ToString(), Utility.LineBreaks.Unix);

			// Clear the string builder and set it to the new text
			sb.Length = 0;
			sb.Append(originalText);

			int nextIndex = 0;
			int offset = 0;
			int searchWordLength = ".UserOptions.".Length;
			var validWordChars = new[] { '_', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
			var validCallChars = new char[validWordChars.Length + 1];
			Array.Copy(validWordChars, validCallChars, validWordChars.Length);
			validCallChars[validCallChars.Length - 1] = '.';
			Array.Sort(validWordChars);
			Array.Sort(validCallChars);
			int lengthDiff = ".GetUserOptionValue(\"".Length - searchWordLength;
			var sbUserOptionName = new StringBuilder();

			while (nextIndex >= 0)
			{
				nextIndex = originalText.IndexOf(".UserOptions.", nextIndex);
				// Make sure preceeding word isn't '.TemplateGen.'

				if (nextIndex > 0)
				{
					if (nextIndex >= 12 && originalText.Substring(nextIndex - 12, 12) == ".TemplateGen")
					{
						nextIndex += searchWordLength;
						continue;
					}
					int originalInsertPoint = nextIndex + offset;
					sb.Remove(nextIndex + offset, searchWordLength);
					sb.Insert(nextIndex + offset, ".GetUserOptionValue(\"");
					offset += lengthDiff;
					nextIndex += searchWordLength;
					sbUserOptionName.Remove(0, sbUserOptionName.Length);

					while (Array.BinarySearch(validWordChars, originalText[nextIndex]) >= 0)
					{
						sbUserOptionName.Append(originalText[nextIndex]);
						nextIndex++;
					}
					UserOption userOption = Project.Instance.FindUserOption(sbUserOptionName.ToString());

					if (userOption == null)
					{
						// TODO: Make this message appear in the error list on the functions screen.
						int debugInfoStart = originalText.IndexOf("/*DEBUG:", nextIndex) + "/*DEBUG:".Length;
						int debugInfoLength = originalText.IndexOf("<", debugInfoStart) - debugInfoStart;
						int line = int.Parse(originalText.Substring(debugInfoStart, debugInfoLength));
						debugInfoStart = debugInfoStart + debugInfoLength + 1;
						debugInfoLength = originalText.IndexOf("(", debugInfoStart) - debugInfoStart;
						string function = originalText.Substring(debugInfoStart, debugInfoLength);

						//Project.DefaultValueFunction defValFunction = Project.Instance.FindDefaultValueFunction(function, new Project.ParamInfo[0]);
						string message = string.Format("UserOption does not exist: {0}.\n\nFunction: [{2}]\nLine {1}.", sbUserOptionName, line, function);
						throw new MissingMemberException(message);
					}
					sb.Insert(nextIndex + offset, "\"))");
					offset += 3;

					while (Array.BinarySearch(validCallChars, sb[originalInsertPoint]) >= 0)
					{
						originalInsertPoint--;
					}
					string userOptionType = userOption.VarType == systemEnumType ? "string" : userOption.VarType.FullName;
					string cast = "((" + userOptionType + ")";
					sb.Insert(originalInsertPoint + 1, cast);
					//offset += 2;
					offset += cast.Length;
				}
			}
		}

		private const string VirtualPropertiesGetterRegex = "VirtualProperties\\.(?<Name>\\w*)";
		private const string VirtualPropertiesSetterRegex = "VirtualProperties\\.(?<Name>\\w*)\\s*=\\s*(?<Value>.*)(?=\\s*;)";
		internal static string ReplaceVirtualPropertyCalls(string text)
		{
			string output = Regex.Replace(text, VirtualPropertiesSetterRegex, match => "set_" + match.Groups["Name"].Value + "(" + match.Groups["Value"].Value + ")");
			output = Regex.Replace(output, VirtualPropertiesGetterRegex, match => "get_" + match.Groups["Name"].Value + "()");

			return output;
		}

		private static bool VerifyProjectIntegrity()
		{
			//foreach (Project.UserOption userOption in Project.Instance.UserOptions)
			//{
			//#region DisplayToUserFunction
			//Project.FunctionInfo func = Project.Instance.FindFunction(userOption.DisplayToUserFunction, new Project.ParamInfo[0]);

			//if (func == null)
			//{
			//    userOption.DisplayToUserValue = true;
			//}
			//else if (func.Body.Trim() == "return true;")
			//{
			//    userOption.DisplayToUserValue = true;
			//}
			//else if (func.Body.Trim() == "return false;")
			//{
			//    userOption.DisplayToUserValue = false;
			//}
			//else
			//{
			//    userOption.DisplayToUserValue = null;
			//}
			//#endregion

			//#region ValidatorFunction
			//func = Project.Instance.FindFunction(userOption.ValidatorFunction, new Project.ParamInfo[2] { new Project.ParamInfo("xxx", userOption.VarType), new Project.ParamInfo("xxx", typeof(string)) });

			//if (func == null)
			//{
			//    userOption.IsValidValue = true;
			//}
			//else if (func.Body.Replace("\n", "").Trim() == "failReason = \"\";return true;")
			//{
			//    userOption.IsValidValue = true;
			//}
			//else
			//{
			//    userOption.IsValidValue = null;
			//}
			//#endregion
			//}
			List<string> invalidFileNames = FileAndFolderNamesAreValid();

			if (invalidFileNames.Count > 0)
			{
				var sb = new StringBuilder(200);

				foreach (string error in invalidFileNames)
				{
					sb.AppendLine(error);
				}
				System.Windows.Forms.MessageBox.Show("Some file/folder names are using invalid placeholders: " + Environment.NewLine + sb, "Invalid File Names", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
				return false;
			}
			return true;
		}

		/// <summary>
		/// Gets whether filenames and foldernames use valid placeholders.
		/// </summary>
		/// <returns></returns>
		private static List<string> FileAndFolderNamesAreValid()
		{
			var invalidFilenames = new List<string>();
			List<string> placeholders;

			foreach (OutputFile file in Project.Instance.RootOutput.Files)
			{
				placeholders = GetPlaceholderNames(file.Name);

				foreach (string placeholder in placeholders)
				{
					// Check whether this reference is valid
					if (placeholder.IndexOf("UserOptions.") == 0)
					{
						if (Project.Instance.FindUserOption(placeholder.Replace("UserOptions.", "")) == null)
						{
							invalidFilenames.Add(file.Name);
						}
					}
				}
			}
			foreach (OutputFolder subFolder in Project.Instance.RootOutput.Folders)
			{
				placeholders = GetPlaceholderNames(subFolder.Name);

				foreach (string placeholder in placeholders)
				{
					// Check whether this reference is valid
					if (placeholder.IndexOf("UserOptions.") == 0)
					{
						if (Project.Instance.FindUserOption(placeholder.Replace("UserOptions.", "")) == null)
						{
							invalidFilenames.Add(subFolder.Name);
						}
					}
				}
				invalidFilenames.AddRange(FileAndFolderNamesAreValid(subFolder));
			}
			return invalidFilenames;

		}

		private static List<string> FileAndFolderNamesAreValid(OutputFolder folder)
		{
			var invalidFilenames = new List<string>();
			List<string> placeholders;

			foreach (OutputFile file in folder.Files)
			{
				placeholders = GetPlaceholderNames(file.Name);

				foreach (string placeholder in placeholders)
				{
					// Check whether this reference is valid
					if (placeholder.IndexOf("UserOptions.") == 0)
					{
						if (Project.Instance.FindUserOption(placeholder.Replace("UserOptions.", "")) == null)
						{
							invalidFilenames.Add(file.Name);
						}
					}
				}
			}
			foreach (OutputFolder subFolder in folder.Folders)
			{
				placeholders = GetPlaceholderNames(subFolder.Name);

				foreach (string placeholder in placeholders)
				{
					// Check whether this reference is valid
					if (placeholder.IndexOf("UserOptions.") == 0)
					{
						if (Project.Instance.FindUserOption(placeholder.Replace("UserOptions.", "")) == null)
						{
							invalidFilenames.Add(subFolder.Name);
						}
					}
				}
				invalidFilenames.AddRange(FileAndFolderNamesAreValid(subFolder));
			}
			return invalidFilenames;
		}

		/// <summary>
		/// Gets a list of placeholders in a filename eg: My#UserOptions.ProjectName#File.txt will return a single-item list: 'UserOptions.ProjectName'
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		private static List<string> GetPlaceholderNames(string filename)
		{
			string[] parts = filename.Split('#');

			if (parts.Length > 1)
			{
				var results = new List<string>();

				for (int i = 1; i < parts.Length; i += 2)
				{
					results.Add(parts[i].Trim());
				}
				return results;
			}
			return new List<string>();
		}

		/// <summary>
		/// Gets a comma-separated list of parameter-types.
		/// </summary>
		/// <param name="parameters"></param>
		/// <returns></returns>
		private static string GetParamTypeString(IEnumerable<ParamInfo> parameters)
		{
			StringBuilder sb = new StringBuilder(100);

			foreach (ParamInfo parameter in parameters)
			{
				sb.Append(parameter.DataType.FullName + ",");
			}
			return sb.ToString().TrimEnd(',').Replace("+", ".");
		}

		/// <summary>
		/// Gets a comma-separated list of parameter-types.
		/// </summary>
		/// <param name="parameters"></param>
		/// <returns></returns>
		private static string GetParamTypeString(IEnumerable<ParameterInfo> parameters)
		{
			StringBuilder sb = new StringBuilder(100);

			foreach (ParameterInfo parameter in parameters)
			{
				sb.Append(parameter.ParameterType.FullName + ",");
			}
			return sb.ToString().TrimEnd(',').Replace("+", ".");
		}

		private static string FormatParameter(ParamInfo paramInfo)
		{
			string param = "";

			string modifierText = "";
			if (paramInfo.Modifiers.Length > 0)
			{
				modifierText = paramInfo.Modifiers + " ";
			}

			param += string.Format("{2}{0} {1}", Utility.GetDemangledGenericTypeName(paramInfo.DataType), paramInfo.Name, modifierText);


			return param;
		}

		private static string FormatParameters(IEnumerable<ParamInfo> parameters)
		{
			var paramList = new List<string>();
			foreach (var param in parameters)
			{
				paramList.Add(FormatParameter(param));
			}

			return string.Join(", ", paramList.ToArray());
		}

		/// <summary>
		/// Gets the parsed template code.
		/// </summary>
		/// <returns>The C# code created from the template functions.</returns>
		public static string ParseTemplateFileCSharp()
		{
			var sb = new StringBuilder(1000);
			string namespaces = "";

			// Get Designer version info
			string versionNumber;
			{
				Assembly designerAssembly = Assembly.GetAssembly(typeof(CompileHelper));
				versionNumber = designerAssembly.GetName().Version.ToString();
			}

			// Create namespace list
			for (int i = 0; i < Project.Instance.Namespaces.Count; i++)
			{
				if (Project.Instance.Namespaces[i].Trim().Length > 0)
				{
					namespaces += "using " + Project.Instance.Namespaces[i] + ";\n";
				}
			}

			if (RandomiseTheNamespace)
			{
				NamespaceUsed = Project.Instance.ProjectNamespace + RandomNamespaceNumber++;
			}
			else
			{
				NamespaceUsed = Project.Instance.ProjectNamespace;
			}

			string versioningInfo =
				string.Format(
					"[assembly: AssemblyVersion(\"{1}\")]{0}[assembly: AssemblyFileVersion(\"{1}\")]{0}[assembly: AssemblyInformationalVersion(\"{2}\")]{0}[assembly: AssemblyProduct(\"Compiled by ArchAngel Designer Version {2}\")]{0}",
					Environment.NewLine, (string.IsNullOrEmpty(Project.Instance.Version) ? "0.0.0.0" : Project.Instance.Version),
					versionNumber);

			sb.Append(@"
					" + namespaces +
					  @"
					using System.Reflection;
					using System.Collections.Generic;
					" +
					  versioningInfo + @"
					namespace " + NamespaceUsed + @"
					{");

			#region ScriptFunctionWrappers_String

			sb.Append(
				@"
							public class ScriptFunctionWrapper : ArchAngel.Interfaces.ScriptFunctionWrapper
							{
						");

			#region RunScriptFunction

			sb.Append(
				@"
								private string GetParamTypeString(object[] parameters)
								{
									StringBuilder sb = new StringBuilder(100);

									foreach (object parameter in parameters)
									{
										sb.Append(parameter.GetType().FullName + "","");
									}
									return sb.ToString().TrimEnd(',').Replace(""+"", ""."");
								}

								private static TemplateGen instance = new TemplateGen();

								public override object RunScriptFunction(string functionName, ref object[] parameters)
								{
									object result = null;

									switch (functionName)
									{
					   ");
			var functionComparer = new Comparers.FunctionComparer();
			Project.Instance.Functions.Sort(functionComparer);
			string prevFunctionName = "";

			for (int functionCounter = 0; functionCounter < Project.Instance.Functions.Count; functionCounter++)
			{
				FunctionInfo fi = Project.Instance.Functions[functionCounter];

				if (fi.IsExtensionMethod)
					continue;

				DefaultValueFunction defaultValueFunction = Project.Instance.FindDefaultValueFunction(fi.Name,
																											  fi.Parameters);
				if (defaultValueFunction != null)
					continue;

				//if (defaultValueFunction != null && !defaultValueFunction.UseCustomCode)
				//{
				//    continue;
				//}
				string unboxedString = "";
				string localVariables = "";
				string callee = "TemplateGen";

				if (fi.IsTemplateFunction)
					callee = "instance";

				for (int i = 0; i < fi.Parameters.Count; i++)
				{
					localVariables += string.Format("{0} {2}param{3}_{1} = ({0})parameters[{1}];",
													Utility.GetDemangledGenericTypeName(fi.Parameters[i].DataType),
													i, fi.Name, functionCounter);
					if (i > 0)
					{
						unboxedString += ", ";
					}
					unboxedString += string.Format("{1} {2}param{3}_{0}", i, fi.Parameters[i].Modifiers, fi.Name, functionCounter);
				}
				// Check whether the function is overloaded - add inner switch if it is
				bool functionIsOverloaded = Project.Instance.FindFunctions(fi.Name).Count > 1;

				if (!functionIsOverloaded ||
					!Utility.StringsAreEqual(prevFunctionName, Project.Instance.Functions[functionCounter].Name,
														  true))
				{
					// This is the first function with this name
					sb.AppendFormat("case \"{0}\":", fi.Name);

					if (functionIsOverloaded)
					{
						sb.Append(@"
									switch (GetParamTypeString(parameters))
									{");
					}
				}
				prevFunctionName = Project.Instance.Functions[functionCounter].Name;

				if (functionIsOverloaded)
				{
					sb.AppendFormat("case \"{0}\":", GetParamTypeString(Project.Instance.Functions[functionCounter].Parameters));
				}
				if (fi.ReturnType != null && fi.ReturnType.FullName != "System.Void")
				{
					sb.Append(@"
								" + localVariables +
							  @"
								result = " + callee + "." + fi.Name + @"(" + unboxedString +
							  @");
								");
				}
				else
				{
					sb.Append(@"
								" + localVariables + @"
								" +
							  callee + "." + fi.Name + @"(" + unboxedString +
							  @");
								result = null;
								");
				}
				for (int i = 0; i < fi.Parameters.Count; i++)
				{
					sb.Append(string.Format("parameters[{0}] = {1}param{2}_{0};", i, fi.Name, functionCounter));
				}
				sb.Append(
					@"
												break;
											");
				if (functionIsOverloaded &&
					(functionCounter == Project.Instance.Functions.Count - 1 ||
					 (functionCounter < Project.Instance.Functions.Count - 1 &&
					  Project.Instance.Functions[functionCounter].Name != Project.Instance.Functions[functionCounter + 1].Name)))
				{
					// This is the last overloaded function with this name
					//firstFuncWithName = true;
					sb.Append(@"}
								break;");
				}
			}

			#region Wrap calls to Dynamic Filenames

			foreach (OutputFile file in Project.Instance.GetAllFiles())
			{
				string paramString = string.IsNullOrEmpty(file.IteratorTypes)
										? ""
										: string.Format("({0})parameters[0]", file.IteratorTypes);
				string[] parts = file.Name.Split('#');
				int numAdded = 0;

				for (int partCounter = 0; partCounter < parts.Length; partCounter++)
				{
					if (partCounter % 2 == 1)
					{
						sb.AppendFormat(
							@"
							case ""DynamicFilenames.File_{0}_{1}"":
								return TemplateGen.DynamicFilenames.File_{0}_{1}({2});
							",
							file.Id, numAdded, paramString);
						numAdded++;
					}
				}
			}

			#endregion

			#region Wrap calls to Dynamic FolderNames

			foreach (OutputFolder folder in Project.Instance.GetAllFolders())
			{
				string paramString = folder.IteratorType == null
										? ""
										: string.Format("({0})parameters[0]", folder.IteratorType.FullName);
				string[] parts = folder.Name.Split('#');
				int numAdded = 0;

				for (int partCounter = 0; partCounter < parts.Length; partCounter++)
				{
					if (partCounter % 2 == 1)
					{
						sb.AppendFormat(
							@"
							case ""DynamicFolderNames.Folder_{0}_{1}"":
								return TemplateGen.DynamicFolderNames.Folder_{0}_{1}({2});
							",
							folder.Id, numAdded, paramString);
						numAdded++;
					}
				}
			}

			#endregion

			sb.Append(
				@"
											case ""InternalFunctions.MustSkipCurrentFile"":
														return instance.SkipCurrentFile;
											case ""InternalFunctions.ResetSkipCurrentFile"":
														instance.SkipCurrentFile = false;
														return null;
											case ""InternalFunctions.GetCurrentFileName"":
														return instance.CurrentFileName;
											case ""InternalFunctions.ResetCurrentFileName"":
														instance.CurrentFileName = """";
														return null;
											case ""InternalFunctions.SetGeneratedFileName"":
														instance.GeneratedFileName = (string)parameters[0];
														return null;
											case ""InternalFunctions.ClearTemplateCache"":
														instance.ClearTemplateCache();
														return null;
											default:    
											throw new Exception(""Function not handled in RunScriptFunction:""+ functionName);
									}
									return result;
								}
						");

			#endregion

			#region RunApiExtensionFunction

			sb.Append(
				@"
								public override bool RunApiExtensionFunction(string functionName, out object result, ref object[] parameters)
								{
									result = null;
									functionName = functionName.Replace(""."", ""_"");

									switch (functionName)
									{
					   ");

			prevFunctionName = "";

			foreach (ApiExtensionMethod extensionMethod in Project.Instance.ApiExtensions)
			{
				if (!extensionMethod.HasOverride)
				{
					continue;
				}
				string unboxedString = "";
				string localVariables = "";

				var method = extensionMethod.ExtendedMethod;
				var parameters = method.GetParameters();
				for (int i = 0; i < parameters.Length; i++)
				{
					localVariables += string.Format("{0} {2}param{1} = ({0})parameters[{1}];",
													Utility.GetDemangledGenericTypeName(parameters[i].ParameterType),
													i, method.Name);
					if (i > 0)
					{
						unboxedString += ", ";
					}
					unboxedString += string.Format("{1} {2}param{0}", i, GetModifiers(parameters[i]), method.Name);
				}
				// Check whether the function is overloaded - add inner switch if it is
				bool functionIsOverloaded = Project.Instance.FindFunctions(method.Name).Count > 1;

				if (!Utility.StringsAreEqual(prevFunctionName, method.Name, true))
				{
					// This is the first function with this name
					sb.AppendFormat("case \"{0}\":", method.Name);

					if (functionIsOverloaded)
					{
						sb.Append(@"
									switch (GetParamTypeString(parameters))
									{");
					}
				}
				prevFunctionName = method.Name;

				if (functionIsOverloaded)
				{
					sb.AppendFormat("case \"{0}\":", GetParamTypeString(parameters));
				}
				sb.Append( /*string.Format("case \"{0}\":", func.Name) +*/
					@"
												" + localVariables +
					@"
												result = ApiExtensionMethods." + GetApiExtensionMethodName(extensionMethod) + @"(" + unboxedString +
					@");
												");
				for (int i = 0; i < parameters.Length; i++)
				{
					sb.Append(string.Format("parameters[{0}] = {1}param{0};", i, method.Name));
				}
				sb.Append(
					@"
												return true;
											");
			}

			sb.Append(
				@"
											default:    
												return false;
									}
								}
						");

			#endregion

			sb.Append(@"
							}");

			#endregion

			#region LanguageAttribute

			sb.Append(
				@"
					[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true) ]
					public class LanguageAttribute : Attribute
					{
						public LanguageAttribute(string language)
						{
							this.language = language;
						}
						
						protected string language;
						public String Language
						{
							get 
							{
								return this.language;
							}            
						}
						
						public override string ToString()
						{
							return language;
						}
					}
");

			#endregion

			InsertApiExtensionClass(sb, Project.Instance.ApiExtensions);

			// These need to go outside TemplateGen, as extension methods must be in a non-generic, static classes
			#region Extension Methods

			sb.Append(@"
			public static class TemplateGenExtensionMethods
			{
");

			foreach (var fi in Project.Instance.Functions)
			{
				if (fi.IsExtensionMethod == false) continue;

				/* Create Method Signature */
				sb.AppendLine("[Language(\"" + fi.TemplateReturnLanguage + "\")]");
				sb.AppendFormat("\t\t\tpublic static {0} {1}(", fi.ReturnType == null ? "void" : fi.ReturnType.FullName, fi.Name);
				// Create parameter list
				sb.Append(FormatParameters(fi.Parameters)).AppendLine(")");
				sb.AppendLine("\t\t\t{");
				sb.AppendLine(FormatFunctionBody(fi));
				sb.AppendLine("\t\t\t}");
			}

			sb.Append(@"
			}
");

			#endregion

			InsertVirtualProperties(sb, Project.Instance.UserOptions);
			//InsertDefaultValueFunctions(sb);

			InsertTemplateGenClass(sb);

			// Finish the namespace off
			sb.AppendLine(
@"
			}
");

			// Fill the Line Number lookup objects with the information they need,
			// and remove debugging information.
			FillFunctionHashes();
			return ProcessLineCrossReferences(sb);
		}

		private static string GetModifiers(ParameterInfo parameter)
		{
			List<string> modifiers = new List<string>();
			if (parameter.IsOut)
				modifiers.Add("out");
			if (parameter.IsRetval)
				modifiers.Add("ref");

			return string.Join(" ", modifiers.ToArray());
		}

		private static void InsertApiExtensionClass(StringBuilder sb, IEnumerable<ApiExtensionMethod> extensions)
		{
			sb.AppendLine(@"
			public static class ApiExtensionMethods
			{");
			foreach (var method in extensions)
			{
				if (method.HasOverride == false) continue;

				var parameters = method.ExtendedMethod.GetParameters().Select(p => p.ParameterType.FullName + " " + p.Name);
				string parameterString = string.Join(", ", parameters.ToArray());
				sb.AppendFormat("\t\t\t\tpublic static {0} {1}({2})", method.ExtendedMethod.ReturnType.FullName,
								GetApiExtensionMethodName(method), parameterString);
				sb.AppendLine("\t\t\t\t{");
				sb.AppendLine(method.OverridingFunctionBody);
				sb.AppendLine("\t\t\t\t}");
			}
			sb.AppendLine("\t\t\t}");
		}

		private static object GetApiExtensionMethodName(ApiExtensionMethod method)
		{
			return method.ExtendedMethod.DeclaringType.FullName.Replace(".", "_").Replace("+", "_") + "_" + method.ExtendedMethod.Name;
		}

		private static void InsertTemplateGenClass(StringBuilder sb)
		{
			sb.Append(@"
						[Serializable]
						public class TemplateGen
						{
						");

			InsertDynamicFileNames(sb);
			InsertDynamicFolderNames(sb);
			InsertTemplateGenStaticFunctionsAndInstanceVariables(sb);
			InsertUserOptions(sb, Project.Instance.UserOptions);
			InsertFunctions(sb);
			ReplaceUserOptionCalls(sb);
			InsertWriteCalls(sb);
			sb.AppendLine(
@"
						}");
		}

		private static void InsertWriteCalls(StringBuilder sb)
		{
			sb.Append(
				@"
							public StringBuilder GetCurrentStringBuilder()
							{
								return _SBStack.Peek();
							}

							private void Write(object s)
							{
								if (s != null)
								{
									StringBuilder b = _SBStack.Peek();
									b.Insert(b.Length, s.ToString());
								}
							}
							
							private void WriteLine(object s)
							{
								if (s != null)
								{
									StringBuilder b = _SBStack.Peek();
									b.Insert(b.Length, s.ToString() + Environment.NewLine);
								}
							}

							private void WriteFormat(string format, params object[] args)
							{
								if (!string.IsNullOrEmpty(format))
								{
									StringBuilder b = _SBStack.Peek();
									b.Insert(b.Length, string.Format(format, args));
								}
							}

							private void Write(bool val, object trueText)
							{
								if (val && trueText != null)
								{
									StringBuilder b = _SBStack.Peek();
									b.Insert(b.Length, trueText.ToString());
								}
							}

							private void Write(bool val, object trueText, object falseText)
							{
								if (val && trueText != null)
								{
									StringBuilder b = _SBStack.Peek();
									b.Insert(b.Length, trueText.ToString());
								}
								else if (!val && falseText != null)
								{
									StringBuilder b = _SBStack.Peek();
									b.Insert(b.Length, falseText.ToString());
								}
							}
");
		}

		private static void InsertTemplateGenStaticFunctionsAndInstanceVariables(StringBuilder sb)
		{
			sb.Append(
				@"
							private static List<string> m_assemblySearchPaths = new List<string>();
							public bool SkipCurrentFile = false;
							public string CurrentFileName = """";
							public string GeneratedFileName = """";
							public Dictionary<string, object> TemplateCache = new Dictionary<string, object>();
							private Stack<StringBuilder> _SBStack = new Stack<StringBuilder>();

							static TemplateGen()
							{
							}

							public void ClearTemplateCache()
							{
								TemplateCache.Clear();
								VirtualProperties.ClearCache();
							}
							
							public static List<string> AssemblySearchPaths
							{
								get {return m_assemblySearchPaths;}
								set {m_assemblySearchPaths = value;}
							}

							public static System.IO.Stream GetProjectInfoXml()
							{
								return System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(""options.xml"");
							}
							");
		}

		private static void InsertFunctions(StringBuilder sb)
		{
			TemplateLinesLookup = new Dictionary<int, List<CompiledToTemplateLineLookup>>(Project.Instance.Functions.Count);
			CompiledLinesLookup = new CompiledLineNumberMap(Project.Instance.Functions.Count);

			foreach (FunctionInfo fi in Project.Instance.Functions)
			{
				// Extension methods are handled above.
				if (fi.IsExtensionMethod)
					continue;

				DefaultValueFunction defaultValueFunction = Project.Instance.FindDefaultValueFunction(fi.Name,
																											  fi.Parameters);

				if (defaultValueFunction != null && defaultValueFunction.UseCustomCode == false)
				{
					continue;
				}

				string param = "";
				string vbParams = "";

				// Create parameter list
				for (int pCount = 0; pCount < fi.Parameters.Count; pCount++)
				{
					string modifierText = "";

					if (fi.Parameters[pCount].Modifiers.Length > 0)
					{
						modifierText = fi.Parameters[pCount].Modifiers + " ";
					}

					string paramDataType = Utility.GetDemangledGenericTypeName(fi.Parameters[pCount].DataType);
					if (pCount < fi.Parameters.Count - 1)
					{
						param += string.Format("{2}{0} {1}, ",
											   paramDataType,
											   fi.Parameters[pCount].Name, modifierText);
						vbParams += fi.Parameters[pCount].Name + ", ";
					}
					else
					{
						param += string.Format("{2}{0} {1}",
											   paramDataType,
											   fi.Parameters[pCount].Name, modifierText);
						vbParams += fi.Parameters[pCount].Name;
					}
				}
				string returnTypeName;

				if (fi.IsTemplateFunction)
				{
					returnTypeName = "string";
				}
				else if (fi.ReturnType == null || fi.ReturnType.FullName == "System.Void")
				{
					returnTypeName = "void";
				}
				else
				{
					returnTypeName = fi.ReturnType.FullName;
				}
				// TODO: rethink this
				string scope = "public"; // fi.IsTemplateFunction ? "public" : "private";
				sb.AppendLine("[Language(\"" + fi.TemplateReturnLanguage + "\")]");
				if (fi.ScriptLanguage == SyntaxEditorHelper.ScriptLanguageTypes.VbNet)
				{
					if (vbParams.Length == 0)
					{
						vbParams = "null";
					}
					sb.Append(
						string.Format(@"{0} {1} {2}({3})
							{4}
								", scope,
									  returnTypeName, fi.Name, param, openBrace));

					if (fi.ReturnType == null)
					{
						sb.AppendLine(string.Format("VBTemplateGenType.GetMethod(\"{0}\").Invoke(null, {1});", fi.Name, vbParams));
						sb.AppendLine("return;");
					}
					else
					{
						sb.AppendLine(
							string.Format("return ({0})VBTemplateGenType.GetMethod(\"{1}\").Invoke(null, new object[] {3}{2}{4});",
										  returnTypeName, fi.Name, vbParams, openBrace, closeBrace));
					}
					sb.AppendLine("}");
				}
				else
				{
					if (fi.IsTemplateFunction)
					{
						sb.Append(string.Format("{0} {1} {2}({3})\n{4}\n", scope, returnTypeName, fi.Name, param, openBrace));
					}
					else
					{
						sb.Append(string.Format("{0} static {1} {2}({3})\n{4}\n", scope, returnTypeName, fi.Name, param, openBrace));
					}

					// Start of Function body
					sb.Append(FormatFunctionBody(fi));
					sb.Append(@"
							}
					");
				}
			}
		}

		private static void InsertDynamicFileNames(StringBuilder sb)
		{
			sb.AppendFormat(
				@"
						internal class {0}
						{{
						", TemplateHelper.DynamicFilenamesClassName);
			foreach (OutputFile file in Project.Instance.GetAllFiles())
			{
				string paramType = file.IteratorTypes;

				if (!string.IsNullOrEmpty(paramType))
				{
					paramType += " iterator";
				}
				string[] parts = file.Name.Split('#');
				int numAdded = 0;

				for (int partCounter = 0; partCounter < parts.Length; partCounter++)
				{
					if (partCounter % 2 == 1)
					{
						string methodName = TemplateHelper.ContructFileNameMethodName(file.Id, numAdded);
						string identifier = string.Format("{0},{1}", file.Id, numAdded);
						sb.AppendFormat(
							@"
							public static string {0}({1})
							{{
								return {2}; // DynamicFilename: ({3})
							}}
							",
							methodName, paramType, parts[partCounter], identifier);
						numAdded++;
					}
				}
			}
			sb.Append("}");
		}

		private static void InsertDynamicFolderNames(StringBuilder sb)
		{
			sb.AppendFormat(
				@"
						internal class {0}
						{{
						", TemplateHelper.DynamicFolderNamesClassName);
			foreach (OutputFolder folder in Project.Instance.GetAllFolders())
			{
				string paramType = folder.IteratorType == null ? "" : folder.IteratorType.FullName;

				if (!string.IsNullOrEmpty(paramType))
				{
					paramType += " iterator";
				}
				string[] parts = folder.Name.Split('#');
				int numAdded = 0;

				for (int partCounter = 0; partCounter < parts.Length; partCounter++)
				{
					if (partCounter % 2 == 1)
					{
						string identifier = string.Format("{0},{1}", folder.Id, numAdded);
						string methodName = TemplateHelper.ContructFolderNameMethodName(folder.Id, numAdded);
						sb.AppendFormat(
							@"
							public static string {0}({1})
							{{
								return {2}; // DynamicFolderName: ({3})
							}}
							",
							methodName, paramType, parts[partCounter], identifier);
						numAdded++;
					}
				}
			}
			sb.Append("}");
		}

		private static void InsertVirtualProperties(StringBuilder sb, ReadOnlyCollection<UserOption> userOptions)
		{
			sb.Append(@"public static class VirtualProperties
						{
							static Dictionary<object, Dictionary<string, object>> VirtualPropertyValues = new Dictionary<object, Dictionary<string, object>>();

							public static void ClearCache() { VirtualPropertyValues.Clear(); }
						");
			Type nullableBool = typeof(bool?);
			Type nullableInt = typeof(int?);

			foreach (UserOption opt in userOptions)
			{
				// Only process Virtual Properties, normal UserOptions get processed later.
				if (opt.IteratorType == null)
				{
					continue;
				}
				string typeName = opt.VarType.FullName;

				if (opt.VarType == nullableBool)
					typeName = "bool?";
				else if (opt.VarType == nullableInt)
					typeName = "int?";

				sb.AppendLine(string.Format(@"
												public static void {1}_EnsureInit({2} obj)
												{{
													if (!VirtualPropertyValues.ContainsKey(obj))
													{{
														VirtualPropertyValues.Add(obj, new Dictionary<string, object>());
													}}
													if (!VirtualPropertyValues[obj].ContainsKey(""{1}""))
													{{
														if(!obj.Ex.Any(uo => uo.Name == ""{1}""))
															VirtualPropertyValues[obj].Add(""{1}"", obj.{1}_DefaultValue());
														else
														{{
															VirtualPropertyValues[obj].Add(""{1}"", ({0})obj.Ex.First(uo => uo.Name == ""{1}"").Value);
														}}
													}}
												}} 

												public static {0} get_{1}(this {2} obj)
												{{
													{1}_EnsureInit(obj);
													return ({0})VirtualPropertyValues[obj][""{1}""];
												}} 

												public static void set_{1}(this {2} obj, {0} value)
												{{
													{1}_EnsureInit(obj);
													VirtualPropertyValues[obj][""{1}""] = value; 
												}}

												public static {0} {1}_DefaultValue(this {2} {3})
												{{
													{4}
												}}

												public static bool {1}_DisplayToUser(this {2} {3})
												{{
													{5}
												}}

												public static bool {1}_IsValid(this {2} {3}, out string failReason)
												{{
													{6}
												}}
												",
												typeName,
												opt.VariableName,
												opt.IteratorType.FullName,
												opt.IteratorType.Name.ToLower(),
												opt.DefaultValueFunctionBody,
												opt.DisplayToUserFunctionBody,
												opt.ValidatorFunctionBody));
			}
			sb.AppendLine("}");
		}

		private static void InsertUserOptions(StringBuilder sb, ReadOnlyCollection<UserOption> userOptions)
		{
			sb.Append(@"
						public static class UserOptions
						{
							static Dictionary<string, object> UserOptionValues = new Dictionary<string, object>();
						");

			Type nullableBool = typeof(bool?);
			Type nullableInt = typeof(int?);

			foreach (UserOption opt in userOptions)
			{
				// Only process normal UserOptions, not Virtual Properties
				if (opt.IteratorType != null)
					continue;

				string typeName = opt.VarType.FullName;

				if (opt.VarType == nullableBool)
					typeName = "bool?";
				else if (opt.VarType == nullableInt)
					typeName = "int?";

				if (opt.VarType.Name.ToLower() == "enum")
				{
					typeName = string.Format("{0}Values", opt.VariableName);

					sb.AppendLine(string.Format(@"
												public static enum {0}Values
												{{
													{1}
												}}
												
												",
												opt.VariableName,
												string.Join(", ", opt.Values.ToArray())));
				}
				sb.AppendLine(string.Format(@"
												private static {0} _{1} = default({0});
												private static bool _{1}_Loaded = false;

												public static {0} {1}
												{{
													get
													{{
														if (!_{1}_Loaded)
														{{
															_{1} = {1}_DefaultValue();
															_{1}_Loaded = true;
														}}
														return _{1};
													}}
													set {{ _{1} = value; _{1}_Loaded = true; }}
												}}

												public static {0} {1}_DefaultValue()
												{{
													{2}
												}}

												public static bool {1}_DisplayToUser()
												{{
													{3}
												}}

												public static bool {1}_IsValid({0} value, out string failReason)
												{{
													{4}
												}}
												",
												typeName,
												opt.VariableName,
												opt.DefaultValueFunctionBody,
												opt.DisplayToUserFunctionBody,
												opt.ValidatorFunctionBody));
			}
			sb.AppendLine("}");
		}

		private static string FormatFunctionBody(FunctionInfo fi)
		{
			var sb = new StringBuilder();

			string code;
			// For the function in the editor, use the code in the editor, 
			// not in fi.Body because the user might not have saved yet, 
			// but still expect to compile or debug what they have just coded.
			//if (DebugVersion &&
			//    fi.Name.Length > 0)
			//{
			//    code = "";
			//}
			//else
			//{
			//code = ReplaceConstants(GetFunctionBody(fi.Name, fi.Parameters));
			code = GetFunctionBody(fi.Name, fi.Parameters);
			//}
			string snippet;

			//if (DebugVersion &&
			//    fi.Name.Length > 0)
			//{
			//    sb.AppendLine(string.Format("{0}#line {1}", Environment.NewLine, Debugger.Debugger.LAST_LINE_IN_FUNCTION));

			//    if (fi.ReturnType != null && fi.ReturnType.Name != "Void")
			//    {
			//        sb.Append("return null;\n");
			//    }
			//    else
			//    {
			//        sb.Append("return;\n");
			//    }

			//    sb.AppendLine("#line default");
			//}

			//if (!DebugVersion ||
			//    DebugVersion && fi.Name.Length == 0)
			//{
			// We need to put EOL markers into the code,
			// so that we can identify lines that errors occur on.
			code = AddDebugSymbols(code, fi.DisplayName, SyntaxEditorHelper.ScriptLanguageTypes.CSharp);
			//}
			if (fi.IsTemplateFunction)
			{
				sb.Append("\tstring __output = \"\";" + Environment.NewLine);
				sb.Append("\t_SBStack.Push(new StringBuilder(10000));" + Environment.NewLine);
				sb.Append("try {" + Environment.NewLine);
				bool eof = false;
				bool inCode = false;
				int currentPos = 0;
				int nextPos = -1;
				while (!eof)
				{
					snippet = "";
					currentPos = currentPos > 0 ? currentPos + 2 : currentPos;

					if (currentPos == nextPos &&
						currentPos == 0 &&
						code.Length > 2)
					{
						currentPos += 2;
					}

					if (inCode) // We are in script code
					{
						nextPos = code.IndexOf("%>", currentPos);
						bool isAssign = (code.IndexOf("=", currentPos) == currentPos);

						if (nextPos > currentPos)
						{
							if (!isAssign)
							{
								string debugColumnOffset = "";
								int lastLineBreak = code.LastIndexOf("\r\n", currentPos);
								string cleanString = code.Substring(lastLineBreak + 2, currentPos - lastLineBreak - 2);

								int columnOffset = RemoveDebugSymbols(cleanString).Length;
								string currentCodeSnippet = code.Substring(currentPos, nextPos - currentPos);

								if (columnOffset > 0)
								{
									debugColumnOffset = string.Format("/*COLUMN_OFFSET:{0},{1}*/", columnOffset, currentCodeSnippet.Length);
								}
								snippet = debugColumnOffset +
										  currentCodeSnippet;
								//string lastChar = snippet.Substring(snippet.Length - 1, 1);

								//if (lastChar == "\n" )
								//{
								//    snippet += "\n";
								//}
							}
							else
							{
								currentPos++;
								snippet = code.Substring(currentPos, nextPos - currentPos).Trim();

								if (!string.IsNullOrEmpty(snippet) && snippet.LastIndexOf(";") == snippet.Length - 1)
								{
									snippet = snippet.Substring(0, snippet.Length - 1);
								}
								string debugColumnOffset;
								int lastLineBreak = code.LastIndexOf("\r\n", currentPos);

								if (lastLineBreak >= 0)
								{
									debugColumnOffset =
										string.Format("/*COLUMN_OFFSET:{0},{1}*/", currentPos - lastLineBreak, snippet.Length);
								}
								else // We are on the first line of the code
								{
									debugColumnOffset = string.Format("/*COLUMN_OFFSET:{0},{1}*/", currentPos, snippet.Length);
								}
								//snippet = string.Format("{1}Write({0});" + Environment.NewLine, snippet, debugColumnOffset);
								snippet = string.Format("{1}Write({0});", snippet, debugColumnOffset);
							}

							snippet = ReplaceVirtualPropertyCalls(snippet);
						}
						else // Read to EOF
						{
							if (currentPos < code.Length)
							{
								snippet = code.Substring(currentPos) + Environment.NewLine;
							}
						}
						inCode = false;
					}
					else // We are in template text, not code
					{
						nextPos = code.IndexOf("<%", currentPos);

						// Check for the escape character eg: '\<%' which users can use to output ASP style delimiters.
						// A double '\\' negates the escaping.
						while (nextPos > 0 && code[nextPos - 1] == '\\' && nextPos > 1 &&
							   code[nextPos - 2] != '\\')
						{
							code = code.Remove(nextPos - 1, 1);
							nextPos--;
							nextPos = code.IndexOf("<%", nextPos + 1);
						}
						// Check for double negation
						if (nextPos > 1 && code[nextPos - 1] == '\\' && code[nextPos - 2] == '\\')
						{
							code = code.Remove(nextPos - 1, 1);
							nextPos--;
						}

						if (nextPos >= currentPos)
						{
							snippet = code.Substring(currentPos, nextPos - currentPos);
							snippet = snippet.Replace("\"", "\"\"");
							// If the template text is empty, don't create a write.
							if (string.IsNullOrEmpty(snippet) == false)
								snippet = string.Format("Write(@{0}{1}{0});", '"', snippet);
						}
						else if (currentPos < code.Length) // Read to EOF
						{
							if (currentPos < code.Length)
							{
								snippet = code.Substring(currentPos);
								snippet = snippet.Replace("\"", "\"\"");
								// If the template text is empty, don't create a write.
								if (string.IsNullOrEmpty(snippet) == false)
									snippet = string.Format("Write(@{0}{1}{0});", '"', snippet);
							}
						}
						inCode = true;
					}
					sb.Append(snippet);
					currentPos = nextPos;
					eof = nextPos < 0;
				}

				sb.AppendLine("} finally");
				sb.AppendLine("{");
				//sb.AppendLine(string.Format("{0}#line {1}", Environment.NewLine, Debugger.Debugger.LAST_LINE_IN_FUNCTION));
				sb.Append(@"
	__output = _SBStack.Pop().ToString();");
				sb.AppendLine(string.Format("{0}#line default{0}", Environment.NewLine));
				sb.AppendLine("}" + Environment.NewLine);
				sb.Append("\treturn __output;");
			}
			else
			{
				sb.Append(code);
			}
			//}

			return sb.ToString();
		}

		private static void FillFunctionHashes()
		{
			FunctionHashes.Clear();

			for (int i = 0; i < Project.Instance.Functions.Count; i++)
			{
				FunctionHashes.Add(Project.Instance.Functions[i].DisplayName, Project.Instance.Functions[i]);
			}
		}

		private static string RemoveDebugSymbols(string text)
		{
			int nextDebugSymbol = text.IndexOf("/*DEBUG:");
			int debugSymbolEnd;
			var sb = new StringBuilder(text);

			while (nextDebugSymbol > 0)
			{
				debugSymbolEnd = sb.ToString().IndexOf("*/", nextDebugSymbol) + 2;
				nextDebugSymbol = sb.ToString().IndexOf("/*DEBUG:");
				sb.Remove(nextDebugSymbol, debugSymbolEnd - nextDebugSymbol);
			}
			nextDebugSymbol = text.IndexOf("/*COLUMN_OFFSET:");

			while (nextDebugSymbol > 0)
			{
				debugSymbolEnd = sb.ToString().IndexOf("*/", nextDebugSymbol) + 2;
				nextDebugSymbol = sb.ToString().IndexOf("/*COLUMN_OFFSET:");
				sb.Remove(nextDebugSymbol, debugSymbolEnd - nextDebugSymbol);
			}
			return sb.ToString();
		}

		private static string ProcessLineCrossReferences(StringBuilder sb)
		{
			string[] compiledLines = Utility.StandardizeLineBreaks(sb.ToString(), Utility.LineBreaks.Unix).Split('\n');
			sb = new StringBuilder(sb.Length);
			var bufferedColumns = new List<ColumnInfo>();

			for (int compiledLineNumber = 0; compiledLineNumber < compiledLines.Length; compiledLineNumber++)
			{
				#region NextOffsetOfInterest
				int nextOffsetOfInterest = -1; // Holds the offset of the next column offset we need to look at.
				int nextTemplateColumnOffset = compiledLines[compiledLineNumber].IndexOf("/*COLUMN_OFFSET:");
				int nextDebugColumnOffset = compiledLines[compiledLineNumber].IndexOf("/*DEBUG:");

				if (nextTemplateColumnOffset >= 0)
				{
					nextOffsetOfInterest = nextTemplateColumnOffset;
				}
				if (nextDebugColumnOffset >= 0)
				{
					if (nextOffsetOfInterest == -1)
					{
						nextOffsetOfInterest = nextDebugColumnOffset;
					}
					else
					{
						nextOffsetOfInterest = Math.Min(nextTemplateColumnOffset, nextDebugColumnOffset);
					}
				}
				#endregion

				while (nextOffsetOfInterest >= 0)
				{
					bool nextOffsetIsColumnOffset = (nextTemplateColumnOffset == nextOffsetOfInterest);

					// get the last column of the offset text.
					int columnEndPos = compiledLines[compiledLineNumber].IndexOf("*/", nextOffsetOfInterest + 1);
					// Column offsets
					if (nextOffsetOfInterest >= 0 && nextOffsetIsColumnOffset)
					{
						string[] offsetNumberText = compiledLines[compiledLineNumber].Substring(nextOffsetOfInterest + 16, columnEndPos - 16 - nextOffsetOfInterest).Split(',');

						// Get the original column offset from the offset text
						int originalColumnNum = int.Parse(offsetNumberText[0]);
						// Get snippet length
						int snippetLength = int.Parse(offsetNumberText[1]);
						// Add the column information to the list of column offsets to process.
						bufferedColumns.Add(new ColumnInfo(compiledLineNumber, originalColumnNum, nextTemplateColumnOffset, snippetLength)); // Include the original and actual column number, so that we can decide how to interpret errors in the GUI
						// Remove column offset text from the line.
						compiledLines[compiledLineNumber] = compiledLines[compiledLineNumber].Substring(0, nextOffsetOfInterest) + compiledLines[compiledLineNumber].Substring(columnEndPos + 2);
					}
					// Process Debug Offsets.
					// Any column offsets that occurred before this debug offset will be
					// processed here.
					else if (nextOffsetOfInterest >= 0 && !nextOffsetIsColumnOffset)
					{
						// The name of the function this line is part of is
						// enclosed in < and > symbols.
						int funcNameStart = compiledLines[compiledLineNumber].IndexOf("<", nextOffsetOfInterest + 1);
						string functionName = compiledLines[compiledLineNumber].Substring(funcNameStart + 1, columnEndPos - funcNameStart - 2);
						// Get the line number of the line of code in the template function.
						int originalLineNum = int.Parse(compiledLines[compiledLineNumber].Substring(nextOffsetOfInterest + 8, funcNameStart - nextOffsetOfInterest - 8));
						originalLineNum -= 1; // Off by one error
						// Remove the debug offset information.
						compiledLines[compiledLineNumber] = compiledLines[compiledLineNumber].Substring(0, nextOffsetOfInterest) + compiledLines[compiledLineNumber].Substring(columnEndPos + 2);

						// If this is the first time we have put something on this line, set up
						// the lookup table.
						if (TemplateLinesLookup.ContainsKey(compiledLineNumber) == false)
						{
							TemplateLinesLookup.Add(compiledLineNumber, new List<CompiledToTemplateLineLookup>());
						}
						// If there are any column offsets to process
						if (bufferedColumns.Count > 0)
						{
							// Get each column offset
							for (int colCounter = 0; colCounter < bufferedColumns.Count; colCounter++)
							{
								ColumnInfo column = bufferedColumns[colCounter];

								// Additional check in case the line number hasn't been checked yet for some reason.
								if (TemplateLinesLookup.ContainsKey(column.LineNumber) == false)
								{
									TemplateLinesLookup[column.LineNumber] = new List<CompiledToTemplateLineLookup>();
								}
								// Add the offset information to the template line lookup
								TemplateLinesLookup[column.LineNumber].Add(new CompiledToTemplateLineLookup(FunctionHashes[functionName], originalLineNum, column.TemplateColumnOffset, column.CompiledColumnOffset, column.SnippetLength));
								// Add the offset information to the compiled line lookup
								CompiledLinesLookup.AddLookup(FunctionHashes[functionName], originalLineNum, new TemplateToCompiledLineLookup(column.LineNumber, column.TemplateColumnOffset, column.CompiledColumnOffset));
							}
							// Remove all column information from the temporary store now that we have dealt with it.
							bufferedColumns.Clear();
						}
						else
						{
							// If there were no column offsets, just add a link between the two lines.
							TemplateLinesLookup[compiledLineNumber].Add(new CompiledToTemplateLineLookup(FunctionHashes[functionName], originalLineNum, 0, 0, 0));
							CompiledLinesLookup.AddLookup(FunctionHashes[functionName], originalLineNum, new TemplateToCompiledLineLookup(compiledLineNumber, 0, 0));
						}
					}
					// Check the next offset of interest again.
					nextOffsetOfInterest = -1;
					nextTemplateColumnOffset = compiledLines[compiledLineNumber].IndexOf("/*COLUMN_OFFSET:");
					nextDebugColumnOffset = compiledLines[compiledLineNumber].IndexOf("/*DEBUG:");

					if (nextTemplateColumnOffset >= 0)
					{
						nextOffsetOfInterest = nextTemplateColumnOffset;
					}
					if (nextDebugColumnOffset >= 0)
					{
						if (nextOffsetOfInterest == -1)
						{
							nextOffsetOfInterest = nextDebugColumnOffset;
						}
						else
						{
							nextOffsetOfInterest = Math.Min(nextTemplateColumnOffset, nextDebugColumnOffset);
						}
					}
				}
				// Remove any trailing linebreak
				if (compiledLines[compiledLineNumber].Length > 0)
				{
					if (compiledLines[compiledLineNumber][compiledLines[compiledLineNumber].Length - 1] == '\r')
					{
						compiledLines[compiledLineNumber] = compiledLines[compiledLineNumber].Substring(0, compiledLines[compiledLineNumber].Length - 1);
					}
				}
				sb.AppendLine(compiledLines[compiledLineNumber]);
			}
			return sb.ToString();
		}

		/// <summary>
		/// Gets the parsed template code for functions with ScriptLanguage = VB.Net
		/// </summary>
		/// <param name="functionName"></param>
		/// <returns></returns>
		public static string ParseTemplateFileVB(string functionName)
		{
			string code = "";
			// Replace all constants
			//code = ReplaceConstants(code);
			var sb = new StringBuilder(code.Length + 1000);
			bool inCode;
			int currentPos;
			int nextPos = -1;
			string snippet = "";
			string namespaces = "";
			char quote = '"';

			// Create namespace list
			for (int i = 0; i < Project.Instance.Namespaces.Count; i++)
			{
				if (Project.Instance.Namespaces[i].Trim().Length > 0)
				{
					namespaces += "Imports " + Project.Instance.Namespaces[i] + "\n";
				}
			}
			sb.Append(@"
					" + namespaces + @"
					Imports TemplateGen

					Namespace " + Project.Instance.ProjectName.Replace(" ", "_") + @"
					");


			sb.Append(@"
						<Serializable> _
						Public Class TemplateGenVB 
								'Implements ITemplateGenVB

							'Private Shared m_assemblySearchPaths() As String =  New String(0) {} 

							Public Sub New()
							End Sub

							'Public Shared Property AssemblySearchPaths() As String()
							'	Get 
							'   	Return m_assemblySearchPaths
							'	End Get
							'	Set (ByVal Value As String()) 
							'    	m_assemblySearchPaths = Value
							'	End Set
							'End Property

							'Public Shared Function GetProjectInfoXml() As System.IO.Stream
							'	Return System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(" + quote + @"options.xml" + quote + @")
							'End Function
							");

			#region Constants
			//              foreach (Project.Constant con in Project.Instance.Constants)
			//              {
			//                  string conString = "";

			//                  // Write the constant variables
			//                  if (con.DataType == "Runtime")
			//                  {
			//                      conString = string.Format("Private Shared m_{1} As String = {2}", con.DataType, con.Name, con.Value, quote);
			//                      // Write accessor method
			//                      conString += string.Format(@"
			//						Public Shared Property {1} As String
			//                            Get 
			//                                Return m_{1}
			//                            End Get
			//						End Function
			//						", con.DataType, con.Name);
			//                  }
			//                  else if (con.DataType == "string" && con.Value.IndexOf("(") < 0)
			//                  {
			//                      conString = string.Format("Private Static m_{1} As {0} = {3}{2}{3}", con.DataType, con.Name, con.Value, quote);
			//                      conString += string.Format(@"
			//						Public Static Property {1} As {0}
			//                            Get 
			//                                Return m_{1}
			//                            End Get
			//	                        Set (ByVal Value As {0}) 
			//	                            m_{1} = Value
			//	                        End Set
			//						End Property
			//						", con.DataType, con.Name);
			//                  }
			//                  else
			//                  {
			//                      conString = string.Format("Private Shared m_{1} As {0} = {2}", con.DataType, con.Name, con.Value, quote);
			//                      conString += string.Format(@"
			//						Public Shared Property {1} As {0}
			//                            Get 
			//                                Return m_{1}
			//                            End Get
			//	                        Set (ByVal Value As {0}) 
			//	                            m_{1} = Value
			//	                        End Set
			//						End Property
			//						", con.DataType, con.Name);
			//                  }
			//                  sb.Append(@"
			//					" + conString + @"
			//					");
			//              }
			#endregion

			#region User-options
			//              foreach (Project.UserOption opt in Project.Instance.UserOptions)
			//              {
			//                  string conString = "";

			//                  switch (opt.VarType)
			//                  {
			//                      case "string":
			//                          conString = string.Format("Private Shared m_{1} As {0} = \"{2}\"", opt.VarType, opt.VariableName, opt.DefaultValue);
			//                          conString += string.Format(@"
			//						Public Shared Property {1} As {0}
			//                            Get 
			//                                Return m_{1}
			//                            End Get
			//	                        Set (ByVal Value As {0}) 
			//	                            m_{1} = Value
			//	                        End Set
			//						End Property
			//						", opt.VarType, opt.VariableName);
			//                          break;
			//                      case "enum":
			//                          string ifClause = "";
			//                          string enumVals = "";

			//                          foreach (string val in opt.Values)
			//                          {
			//                              ifClause += string.Format("Value <> {1}{0}{1} AndAlso ", val, quote);
			//                              enumVals += string.Format(""+ quote +"{0}"+ quote +"c, ", val);
			//                          }
			//                          if (opt.Values.Length > 0)
			//                          {
			//                              ifClause = ifClause.Substring(0, ifClause.Length - 4);
			//                              enumVals = enumVals.Substring(0, enumVals.Length - 2);
			//                          }
			//                          conString = string.Format("Private Shared m_{0} As String = {1}", opt.VariableName, opt.Values[0]);

			//                          conString += string.Format(@"
			//						Public Shared Property {0} As String
			//                            Get 
			//                                Return m_{0}
			//                            End Get
			//	                        Set (ByVal Value As String) 
			//								If ({1}) Then
			//									Throw New Exception({3}Invalid value for an enum. Allowed values are: {2}. Supplied value was:{3}+ Value)
			//								End If
			//								m_{0} = Value
			//	                        End Set
			//						End Property
			//						", opt.VariableName, ifClause, enumVals, quote);
			//                          break;
			//                      default:
			//                          conString = string.Format("Private Shared m_{1} As {0} = {2}", opt.VarType, opt.VariableName, opt.DefaultValue);
			//                          conString += string.Format(@"
			//						Public Shared Property {1} As {0}
			//                            Get 
			//                                Return m_{1}
			//                            End Get
			//	                        Set (ByVal Value As {0}) 
			//	                            m_{1} = Value
			//	                        End Set
			//						End Property
			//						", opt.VarType, opt.VariableName);
			//                          break;
			//                  }
			//                  sb.Append(@"
			//					" + conString + @"
			//					");
			//              }
			#endregion

			#region Functions
			foreach (FunctionInfo fi in Project.Instance.Functions)
			{
				DefaultValueFunction defValFunc = Project.Instance.FindDefaultValueFunction(fi.Name, fi.Parameters);

				if (defValFunc != null && !defValFunc.UseCustomCode)
				{
					continue;
				}
				if (fi.ScriptLanguage != SyntaxEditorHelper.ScriptLanguageTypes.VbNet)
				{
					continue;
				}
				currentPos = 0;
				nextPos = -1;
				bool eof = false;
				inCode = false;

				// For the function in the editor, use the code in the editor, 
				// not in fi.Body because the user might not have saved yet, 
				// but still expect to compile or debug what they have just coded.
				if (DebugVersion &&
					fi.Name != functionName &&
					functionName.Length > 0)
				{
					code = "";
				}
				else
				{
					//code = ReplaceConstants(GetFunctionBody(fi.Name, fi.Parameters));
					throw new Exception("GFH thought this wasn't used anymore.");
				}
				string param = "";

				// Create parameter list
				for (int pCount = 0; pCount < fi.Parameters.Count; pCount++)
				{
					if (pCount < fi.Parameters.Count - 1)
					{
						param += string.Format("ByVal {1} As {0}, ", fi.Parameters[pCount].DataType, fi.Parameters[pCount].Name);
					}
					else
					{
						param += string.Format("ByVal {1} As {0}", fi.Parameters[pCount].DataType, fi.Parameters[pCount].Name);
					}
				}
				Type returnType = fi.ReturnType;

				if (fi.IsTemplateFunction)
				{
					returnType = typeof(string);
				}
				// TODO: rethink this
				const string scope = "Public";// fi.IsTemplateFunction ? "public" : "private";

				if (fi.ReturnType != null)
				{
					sb.Append(string.Format("{0} Shared Function {2}({3}) As {1}\n", scope, returnType, fi.Name, param));
				}
				else
				{
					sb.Append(string.Format("{0} Shared Sub {1}({2})\n", scope, fi.Name, param));
				}
				// Start of Function body
				if (DebugVersion &&
					!(fi.Name == functionName) &&
					functionName.Length > 0)
				{
					if (fi.ReturnType != null)
					{
						sb.Append("Return Nothing\n");
					}
					else
					{
						sb.Append("Return\n");
					}
				}
				if (!DebugVersion ||
					(DebugVersion && fi.Name == functionName) ||
					(DebugVersion && functionName.Length == 0))
				{
					// If this is a debug version we need to put EOL markers into the code,
					// so that we can identify lines that errors occur on.
					//if (DebugVersion)
					//{
					//    code = AddDebugSymbols(code, fi.Name);
					//}
					if (fi.IsTemplateFunction)
					{
						sb.Append("\tDim sb As StringBuilder = New StringBuilder(10000)" + Environment.NewLine);

						while (!eof)
						{
							snippet = "";
							currentPos = currentPos > 0 ? currentPos + 2 : currentPos;

							if (currentPos == nextPos &&
								  currentPos == 0 &&
								  code.Length > 2)
							{
								currentPos += 2;
							}

							if (inCode)
							{
								nextPos = code.IndexOf("%>", currentPos);
								bool isAssign = (code.IndexOf("=", currentPos) == currentPos);

								if (nextPos > currentPos)
								{
									if (!isAssign)
									{
										snippet = code.Substring(currentPos, nextPos - currentPos);

										string lastChar = snippet.Substring(snippet.Length - 1, 1);

										if (lastChar == "\n" ||
											  lastChar == "}")
										{
											snippet += "\n";
										}
									}
									else
									{
										snippet = code.Substring(currentPos + 1, nextPos - (currentPos + 1)).Trim();

										if (snippet.IndexOf(";") == snippet.Length - 1) // TODO: Not sure about this in VB code??
										{
											snippet = snippet.Substring(0, snippet.Length - 1);
										}
										snippet = string.Format("Write({0})" + Environment.NewLine, ReplaceLineBreaks(snippet));
									}
								}
								else // Read to EOF
								{
									if (currentPos < code.Length)
									{
										snippet = code.Substring(currentPos) + Environment.NewLine;
									}
								}
								// Add stringbuilder parameter to Write() calls in snippet
								inCode = false;
							}
							else
							{
								nextPos = code.IndexOf("<%", currentPos);

								if (nextPos >= currentPos)
								{
									snippet = string.Format("Write({0}{1}{0})" + Environment.NewLine, '"', ReplaceLineBreaks(code.Substring(currentPos, nextPos - currentPos).Replace("\"", "\"\"")));
								}
								else if (currentPos < code.Length) // Read to EOF
								{
									if (currentPos < code.Length)
									{
										snippet = string.Format("Write({0}{1}{0})" + Environment.NewLine, '"', ReplaceLineBreaks(code.Substring(currentPos).Replace("\"", "\"\"")));
									}
								}
								inCode = true;
							}
							sb.Append(snippet);
							currentPos = nextPos;
							eof = nextPos < 0;
						}
						sb.Append(@"
								Return sb.ToString()
							");
					}
					else // Not a template function, just a normal function
					{
						sb.Append(code);
					}
				}
				if (fi.ReturnType == null)
				{
					sb.Append(@"
							End Sub
					");
				}
				else
				{
					sb.Append(@"
							End Function
					");
				}
			}
			#endregion

			// Replace Write calls
			int writeIndex = sb.ToString().IndexOf("Write(");

			while (writeIndex > 0)
			{
				// Make sure we don't replace other Write calls in the script or template code
				if (sb.ToString().Substring(writeIndex - 1, 1) != ".")
				{
					sb.Replace("Write(", "Write(sb, ", writeIndex, "Write(".Length);
				}
				writeIndex = sb.ToString().IndexOf("Write(", writeIndex + 1);
			}
			string output = sb + @"

							Private Shared Sub Write(ByVal sb As StringBuilder, ByVal s As String)
								sb.Append(s)
							End Sub
						End Class
					End Namespace
					";
			// Make sure all line-breaks are hard-coded to Windows line-break. This means that 
			// ArchAngel only needs to replace line-breaks on other environments such as 
			// Unix ('\n') and Mac ('\r').
			output = output.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\r\n");
			return output;
		}

		/// <summary>
		/// For VB.net, which doesn't handle multi-line strings elegantly like C#, we need to munge
		/// the string into multiple lines with added line-breaks.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		private static string ReplaceLineBreaks(string text)
		{
			text = text.Replace("\n", string.Format("{0} & Environment.NewLine & _ \n{0}", '"'));

			return text;
		}

		public static string SaveSourceFile(string functionName)
		{
			string filename = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(Settings.Default.CodeFile) + Path.GetRandomFileName());
			string code = ParseTemplateFileCSharp();
			File.WriteAllText(filename + ".cs", code, Encoding.Unicode);

			if (Project.Instance.HasVbFunctions())
			{
				code = ParseTemplateFileVB(functionName);
				File.WriteAllText(filename + ".vb", code, Encoding.Unicode);
			}
			return filename;
		}

		public static bool CompileAndExecuteFile(string[] files, string[] args, string[] filesToEmbed, SyntaxEditorHelper.ScriptLanguageTypes language)
		{
			List<string> arrExtraFiles = new List<string>();
			string sourceExtension;

			switch (language)
			{
				case SyntaxEditorHelper.ScriptLanguageTypes.CSharp:
					sourceExtension = ".cs";
					break;
				case SyntaxEditorHelper.ScriptLanguageTypes.VbNet:
					sourceExtension = ".vb";
					break;
				default:
					throw new NotImplementedException("Invalid script language.");
			}
			for (int i = 0; i < files.Length; i++)
			{
				if (Utility.StringsAreEqual(Path.GetExtension(files[i]), sourceExtension, false))
				{
					arrExtraFiles.Add(files[i]);
				}
			}
			files = arrExtraFiles.ToArray();
			string outputFileName = "";
			string nrfFileName = "";

			foreach (string arg in args)
			{
				if (arg.IndexOf("outputFileName=") >= 0)
				{
					outputFileName = arg.Substring("outputFileName=".Length);
				}
				else if (arg.IndexOf("nrfFileName=") >= 0)
				{
					nrfFileName = arg.Substring("nrfFileName=".Length);
				}
			}
			if (outputFileName.Length == 0)
			{
				throw new ApplicationException("Output file type is missing.");
			}
			bool isExe = (outputFileName.IndexOf(".exe") > 0);
			//Currently only csharp scripting is supported
			CodeDomProvider provider;

			switch (sourceExtension)
			{
				case ".cs":
				case ".ncs":
					//provider = new CSharpCodeProvider(new Dictionary<string, string> { { "CompilerVersion", "v2.0" } });
					//provider = new CSharpCodeProvider(new Dictionary<string, string> { { "CompilerVersion", "v3.5" } });
					provider = new CSharpCodeProvider(new Dictionary<string, string> { { "CompilerVersion", "v4.0" } });
					//provider = new CSharpCodeProvider();
					break;
				case ".vb":
				case ".nvb":
					provider = CodeDomProvider.CreateProvider("VisualBasic");
					break;
				case ".njs":
				case ".js":
					provider = (CodeDomProvider)Activator.CreateInstance("Microsoft.JScript", "Microsoft.JScript.JScriptCodeProvider").Unwrap();
					break;
				default:
					throw new UnsupportedLanguageExecption(sourceExtension);
			}


			CompilerParameters compilerparams = new CompilerParameters();
			compilerparams.GenerateInMemory = false; // Set to true if you just want to compile and run, false to write to disk.
			//compilerparams.GenerateInMemory = CompileHelper.DebugVersion; // Set to true if you just want to compile and run, false to write to disk.
			compilerparams.GenerateExecutable = isExe;
			compilerparams.OutputAssembly = outputFileName;
			compilerparams.IncludeDebugInformation = true;
			// Embed resource file
			string optionPath = Path.Combine(Path.GetTempPath(), "options.xml");
			string allCompilerOptions = "/res:\"" + optionPath + "\" ";

			// Embed extra files as resources.
			foreach (string filePath in filesToEmbed)
			{
				if (!File.Exists(filePath) && filePath.Length > 0)
				{
					System.Windows.Forms.MessageBox.Show(Controller.Instance.MainForm, "File not found: " + filePath + ". Compile aborted.", "Missing file", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
					return false;
				}
				allCompilerOptions += "/res:\"" + filePath + "\" ";
			}

			allCompilerOptions += "/nowarn:1687 ";

			//if (DebugVersion)
			//{
			//    allCompilerOptions += @" /debug ";
			//}
			// Remove the trailing comma
			compilerparams.CompilerOptions = allCompilerOptions;

			//Add assembly references from nscript.nrf or <file>.nrf
			if (File.Exists(nrfFileName))
			{
				AddReferencesFromFile(compilerparams, nrfFileName);
			}
			else
			{
				//Use nscript.nrf
				string nrfFile = Path.Combine(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), "nscript.nrf");

				if (File.Exists(nrfFile))
				{
					AddReferencesFromFile(compilerparams, nrfFile);
				}
			}
			if (language == SyntaxEditorHelper.ScriptLanguageTypes.VbNet)
			{
				compilerparams.ReferencedAssemblies.Add(outputFileName.Replace("_VB", "_CS"));
			}
			// Replace the Namespace placeholder with the filename
			string body;

			using (var sr2 = new StreamReader(files[0]))
			{
				// Namespaces can only consist of letters and numbers, and the first character must be a letter
				body = sr2.ReadToEnd();//.Replace("#NAMESPACE#", Project.Instance.ProjectName);
				ParsedCode = body;
				sr2.Close();
			}
			File.Delete(files[0]);
			Controller.Instance.ParsedCode = body;

			using (var sw2 = new StreamWriter(files[0]))
			{
				sw2.Write(body);
				sw2.Flush();
				sw2.Close();
			}
			CompilerResults results = provider.CompileAssemblyFromFile(compilerparams, files);

			CompiledAssemblyFileName = results.PathToAssembly;

			if (results.Errors.HasErrors)
			{
				List<CompilerError> templist = new List<CompilerError>();
				body = Utility.StandardizeLineBreaks(body, Utility.LineBreaks.Unix);
				string[] lines = body.Split('\n');
				foreach (System.CodeDom.Compiler.CompilerError error in results.Errors)
				{
					if (error.ErrorText.IndexOf("The type or namespace name") >= 0 && lines[error.Line - 1].IndexOf("using ") >= 0)
					{
						string invalidNamespace = lines[error.Line - 1].Substring("using ".Length);
						invalidNamespace = invalidNamespace.Substring(0, invalidNamespace.Length - 1);
						error.ErrorText = "Namespace (" + invalidNamespace + ") can't be found in referenced assemblies. On the Project Details screen, add the required assembly or remove the invalid namespace.";
						error.Line = -1;
						error.FileName = "<Project Details>";
					}
					templist.Add(new CompilerError(error));
				}
				OnCompilerError(templist.ToArray());
				return false;
			}

			return true;
		}

		public static void OnCompilerError(CompilerError[] errors)
		{
			Controller.Instance.ShowCompileErrors(errors, DebugVersion, ParsedCode);
		}

		private static void AddReferencesFromFile(CompilerParameters compilerParams, string nrfFile)
		{
			using (var reader = new StreamReader(nrfFile))
			{
				string line;
				bool systemXmlFound = false;
				bool systemFound = false;
				bool systemWindowsForms = false;
				bool slyceCommonFound = false;
				bool systemCoreFound = false;
				bool inflectorFound = false;

				while ((line = reader.ReadLine()) != null)
				{
					if (line.EndsWith("system.xml.dll", StringComparison.OrdinalIgnoreCase)) { systemXmlFound = true; }
					if (line.EndsWith("system.core.dll", StringComparison.OrdinalIgnoreCase)) { systemCoreFound = true; }
					if (line.EndsWith("system.dll", StringComparison.OrdinalIgnoreCase)) { systemFound = true; }
					if (line.EndsWith("system.windows.forms.dll", StringComparison.OrdinalIgnoreCase)) { systemWindowsForms = true; }
					if (line.EndsWith("slyce.common.dll", StringComparison.OrdinalIgnoreCase)) { slyceCommonFound = true; }
					if (line.EndsWith("inflector.net.dll", StringComparison.OrdinalIgnoreCase)) { inflectorFound = true; }
					compilerParams.ReferencedAssemblies.Add(line);

					//System.Windows.Forms.MessageBox.Show("Line: " + line);
				}
				// Ensure that System.Xml is always included
				if (!systemXmlFound) { compilerParams.ReferencedAssemblies.Add("System.Xml.dll"); }
				if (!systemFound) { compilerParams.ReferencedAssemblies.Add("System.dll"); }
				if (!systemCoreFound) { compilerParams.ReferencedAssemblies.Add("System.Core.dll"); }
				if (!systemWindowsForms) { compilerParams.ReferencedAssemblies.Add("System.Windows.Forms.dll"); }

				if (!slyceCommonFound)
				{
					// We need to provide a fullpath to Slyce.Common.dll, because it is not loaded in the GAC
					string path = Path.Combine(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), "Slyce.Common.dll");

					if (!File.Exists(path))
					{
						throw new FileNotFoundException("File is missing: " + path);
					}
					compilerParams.ReferencedAssemblies.Add(path);
				}
				if (!inflectorFound)
				{
#if DEBUG
					compilerParams.ReferencedAssemblies.Add(Slyce.Common.RelativePaths.GetFullPath(System.Reflection.Assembly.GetExecutingAssembly().Location, @"..\..\..\..\3rd_Party_Libs\Inflector.Net.dll"));
#else
					compilerParams.ReferencedAssemblies.Add(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Inflector.Net.dll"));
#endif
				}
			}
		}

		public static bool Compile(bool debugMode)
		{
			return Compile(debugMode, "");
		}

		/// <summary>
		/// Compile the project.
		/// </summary>
		/// <param name="debugMode">True for debug mode, false for release version.</param>
		/// <param name="singleFunctionName">Name of single function to compile, empty string if all functions to be compiled.</param>
		/// <returns>True if compile was successful, false otherwise.</returns>
		public static bool Compile(bool debugMode, string singleFunctionName)
		{
			lock (typeof(CompileHelper))
			{
				if (!VerifyProjectIntegrity())
				{
					return false;
				}
				DebugVersion = debugMode;
				ScriptFiles.Clear();
				SourceFile = SaveSourceFile(singleFunctionName);

				if (!DebugVersion && Project.Instance.CompileFolderName.Length == 0)
				{
					throw new Exception("No location has been specified for the compile. Add via the Template Details page:\n\n" + Project.Instance.CompileFolderName);
				}
				if (!DebugVersion && !Directory.Exists(Project.Instance.CompileFolderName))
				{
					throw new Exception("Invalid directory has been specified for the compile. Modify via the Template Details page:\n\n" + Project.Instance.CompileFolderName);
				}

				if (SourceFile.Length > 0)
				{
					string tempSourceCodeFile = Path.Combine(Path.GetTempPath(), SourceFile);

					// Check for CS and vb source files
					if (File.Exists(tempSourceCodeFile + ".cs"))
					{
						ScriptFiles.Add(tempSourceCodeFile + ".cs");
					}
					if (File.Exists(tempSourceCodeFile + ".vb"))
					{
						ScriptFiles.Add(tempSourceCodeFile + ".vb");
					}
				}
				bool success = Compile();

				return success;
			}
		}

		private static bool Compile()
		{
			lock (typeof(CompileHelper))
			{
				if (Controller.Instance.MainForm != null && Controller.Instance.MainForm.UcFunctions != null)
				{
					Controller.Instance.MainForm.UcFunctions.ClearErrors();
				}

				#region Copy the xml project configuration and settings file to the temp path
				string optionPath = Path.Combine(Path.GetTempPath(), "options.xml");

				if (File.Exists(optionPath))
				{
					File.Delete(optionPath);
				}

				TextWriter tr = null;
				try
				{
					tr = new StreamWriter(optionPath);
					tr.Write(Project.Instance.GetProjectXml());
					tr.Flush();
				}
				finally
				{
					if (tr != null) { tr.Close(); }
				}
				#endregion

				#region Get the source-code files
				var files = new string[ScriptFiles.Count];

				for (int i = 0; i < ScriptFiles.Count; i++)
				{
					string file = ScriptFiles[i];

					if (!File.Exists(file))
					{
						throw new FileNotFoundException("Code file not found: " + file);
					}
					files[i] = file;
				}
				#endregion

				#region IncludeFiles

				List<string> filesToEmbed = new List<string>();

				foreach (var file in Project.Instance.IncludedFiles)
				{
					if (!File.Exists(file.FullFilePath))
					{
						// If it doesn't exist then alert the user.
						throw new FileNotFoundException("Include file not found: " + file);
					}
					filesToEmbed.Add(file.FullFilePath);
				}
				#endregion

				#region Create the references file
				string ns = "";
				//string ilMergeFiles = "";
				var searchPaths = new List<string>();
				var referencedFiles = new List<string>();

				foreach (var refFile in Project.Instance.References)
				{
					if (!string.IsNullOrEmpty(refFile.FileName))
					{
						referencedFiles.Add(refFile.FileName);
						searchPaths.Add(Path.GetDirectoryName(refFile.FileName));
					}
				}
				searchPaths.Sort();
				referencedFiles = VersionNumber.GetLocationsWithLatestVersions(referencedFiles);

				foreach (string referencedFile in referencedFiles)
				{
					ns += referencedFile + Environment.NewLine;
				}
				// Make sure we are using the latest version of all found files
				string nrfFilePath = Path.Combine(Project.Instance.CompileFolderName, "ns.nrf");
				File.WriteAllText(nrfFilePath, ns, Encoding.Unicode);

				if (!File.Exists(nrfFilePath))
				{
					throw new FileNotFoundException("NRF file not found.");
				}
				#endregion

				string compileFileNameVB = Path.Combine(Project.Instance.CompileFolderName, Project.Instance.ProjectName.Replace(" ", "_") + "_VB.AAT.DLL");
				string compileFileNameCSharp;

				if (Project.Instance.HasVbFunctions())
				{
					compileFileNameCSharp = Path.Combine(Project.Instance.CompileFolderName, Project.Instance.ProjectName.Replace(" ", "_") + "_CS.AAT.DLL");
				}
				else
				{
					// If we only have C# functions, then we only need to create a single aal file, not have a separate 'common' one for VB
					compileFileNameCSharp = ConstructAalFilename(Project.Instance.CompileFolderName, Project.Instance.ProjectName);
				}
				string[] argsCSharp = new[] { "outputFileName=" + compileFileNameCSharp, "nrfFileName=" + nrfFilePath };
				string[] argsVB = new[] { "outputFileName=" + compileFileNameVB, "nrfFileName=" + nrfFilePath };
				bool resultCSharp = CompileAndExecuteFile(files, argsCSharp, filesToEmbed.ToArray(), SyntaxEditorHelper.ScriptLanguageTypes.CSharp);
				bool resultVB = true;

				if (resultCSharp && Project.Instance.HasVbFunctions())
				{
					// Only compile the VB project if the C# compilation was a success
					resultVB = CompileAndExecuteFile(files, argsVB, filesToEmbed.ToArray(), SyntaxEditorHelper.ScriptLanguageTypes.VbNet);
				}
				File.Delete(nrfFilePath);

				if (File.Exists(compileFileNameCSharp))
				{
					if (DebugVersion)
					{
						File.Delete(compileFileNameCSharp);
					}
					else
					{
						File.SetLastWriteTime(compileFileNameCSharp, DateTime.Now);
					}
				}
				if (Project.Instance.HasVbFunctions() && File.Exists(compileFileNameVB))
				{
					if (DebugVersion)
					{
						File.Delete(compileFileNameVB);
					}
					else
					{
						File.SetLastWriteTime(compileFileNameVB, DateTime.Now);
					}
				}
				return resultCSharp && resultVB;
			}
		}

		public static string ConstructAalFilename(string compileFolder, string projectName)
		{
			return compileFolder.PathSlash(projectName.Replace(" ", "_") + ".AAT.DLL");
		}

		public static void DeleteScriptFiles()
		{
			for (int i = 0; i < ScriptFiles.Count; i++)
			{
				if (File.Exists(ScriptFiles[i]))
				{
					File.Delete(ScriptFiles[i]);
				}
			}
		}

	}
}
