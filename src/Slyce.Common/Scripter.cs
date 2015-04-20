using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Slyce.Common
{
	public class Scripter
	{
		public class FileToCompile
		{
			public FileToCompile(string name, string code, object tag)
			{
				Name = name;
				Code = code;
				Tag = tag;
			}

			public string Name { get; set; }
			public string Code { get; set; }
			public object Tag { get; set; }
		}

		private const int LAST_LINE_IN_FUNCTION = 16707563;

		public static Assembly CompileCode(
			List<FileToCompile> codeFiles,
			List<string> referencedAssemblyPaths,
			List<string> embeddedResourcesPaths,
			out List<CompilerError> errors)
		{
			return CompileCode(codeFiles, referencedAssemblyPaths, embeddedResourcesPaths, out errors, null);
		}

		public static Assembly CompileCode(
			List<FileToCompile> codeFiles,
			List<string> referencedAssemblyPaths,
			List<string> embeddedResourcesPaths,
			out List<CompilerError> errors,
			bool forExecution,
			string assemblyPath)
		{
			return CompileCode(codeFiles, referencedAssemblyPaths, embeddedResourcesPaths, out errors, assemblyPath);
		}

		public static Assembly CompileCode(
			List<FileToCompile> codeFiles,
			List<string> referencedAssemblyPaths,
			List<string> embeddedResourcesPaths,
			out List<CompilerError> errors,
			string outputFilePath)
		{
			errors = new List<CompilerError>();
			// Create a code provider
			// This class implements the 'CodeDomProvider' class as its base. All of the current .Net languages (at least Microsoft ones)
			// come with thier own implemtation, thus you can allow the user to use the language of thier choice (though i recommend that
			// you don't allow the use of c++, which is too volatile for scripting use - memory leaks anyone?)
			//Microsoft.CSharp.CSharpCodeProvider csProvider = new Microsoft.CSharp.CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v3.5" } });
			Microsoft.CSharp.CSharpCodeProvider csProvider = new Microsoft.CSharp.CSharpCodeProvider();
			//csProvider = new Microsoft.CSharp.CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v2.0" } });

			// Setup our options
			CompilerParameters options = new CompilerParameters();
			options.GenerateExecutable = false; // we want a Dll (or "Class Library" as its called in .Net)

			if (string.IsNullOrEmpty(outputFilePath))
				options.GenerateInMemory = true; // Saves us from deleting the Dll when we are done with it, though you could set this to false and save start-up time by next time by not having to re-compile
			else
			{
				options.GenerateInMemory = false;
				options.OutputAssembly = outputFilePath;
			}
			// And set any others you want, there a quite a few, take some time to look through them all and decide which fit your application best!

			// Add any references you want the users to be able to access, be warned that giving them access to some classes can allow
			// harmful code to be written and executed. I recommend that you write your own Class library that is the only reference it allows
			// thus they can only do the things you want them to.
			// (though things like "System.Xml.dll" can be useful, just need to provide a way users can read a file to pass in to it)
			// Just to avoid bloatin this example to much, we will just add THIS program to its references, that way we don't need another
			// project to store the interfaces that both this class and the other uses. Just remember, this will expose ALL public classes to
			// the "script"
			options.ReferencedAssemblies.Add(Assembly.GetExecutingAssembly().Location);
			options.ReferencedAssemblies.Add("mscorlib.dll");
			options.ReferencedAssemblies.Add("System.dll");
			options.ReferencedAssemblies.Add("System.Core.dll");
			options.ReferencedAssemblies.Add("System.Xml.dll");

			options.IncludeDebugInformation = true;
			options.TempFiles.KeepFiles = true;

			foreach (var assembly in referencedAssemblyPaths)
				options.ReferencedAssemblies.Add(assembly);

			if (embeddedResourcesPaths != null)
				foreach (var embeddedResource in embeddedResourcesPaths)
					options.EmbeddedResources.Add(embeddedResource);

			// Compile our code
			CompilerResults result;
			string[] codes = codeFiles.Select(cf => cf.Code).ToArray<string>();
			result = csProvider.CompileAssemblyFromSource(options, codes);

			if (result.Errors.HasErrors)
			{
				// TODO: report back to the user that the script has errored
				//return null;

				//throw new NotImplementedException("Handle multiple files in compilation error handling");
				string body = "";// Utility.StandardizeLineBreaks(code, Utility.LineBreaks.Unix);
				string[] lines = body.Split('\n');
				foreach (System.CodeDom.Compiler.CompilerError error in result.Errors)
				{
					if (error.IsWarning)
						continue;

					//if (error.ErrorText.IndexOf("The type or namespace name") >= 0 && lines[error.Line - 1].IndexOf("using ") >= 0)
					//{
					//    string invalidNamespace = lines[error.Line - 1].Substring("using ".Length);
					//    invalidNamespace = invalidNamespace.Substring(0, invalidNamespace.Length - 1);
					//    error.ErrorText = "Namespace (" + invalidNamespace + ") can't be found in referenced assemblies. On the Project Details screen, add the required assembly or remove the invalid namespace.";
					//    error.Line = -1;
					//    error.FileName = "<Project Details>";
					//}
					if (!string.IsNullOrEmpty(error.FileName))
					{
						int fileIndex = int.Parse(System.IO.Path.GetExtension(System.IO.Path.GetFileNameWithoutExtension(error.FileName)).Substring(1));
						error.FileName = codeFiles[fileIndex].Name;
					}
					else
					{
						error.FileName = "";
					}
					errors.Add(error);
				}
				return null;
			}

			if (result.Errors.HasWarnings)
			{
				// TODO: tell the user about the warnings, might want to prompt them if they want to continue
				// runnning the "script"
			}
			if (string.IsNullOrEmpty(outputFilePath))
				return result.CompiledAssembly;
			else
				return Assembly.LoadFile(outputFilePath);

		}

		public static string FormatFunctionBodyAsTemplate(string functionName, string code, string delimiterStart, string delimiterEnd)
		{
			code = code.Replace("\r\n", "\n");
			var sb = new StringBuilder();
			string snippet;

			// We need to put EOL markers into the code,
			// so that we can identify lines that errors occur on.
			code = AddDebugSymbols(code, functionName, SyntaxEditorHelper.ScriptLanguageTypes.CSharp);

			//if (fi.IsTemplateFunction)
			//{
			sb.Append("\tstring __output = \"\";\n");
			sb.Append("\t_SBStack.Push(new StringBuilder(10000));\n");
			sb.Append("try {\n");
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
					nextPos = code.IndexOf(delimiterEnd, currentPos);
					bool isAssign = (code.IndexOf("=", currentPos) == currentPos);

					if (nextPos > currentPos)
					{
						if (!isAssign)
						{
							string debugColumnOffset = "";
							int lastLineBreak = code.LastIndexOf("\n", currentPos);
							string cleanString = code.Substring(lastLineBreak + 1, currentPos - lastLineBreak - 1);

							int columnOffset = RemoveDebugSymbols(cleanString).Length;
							string currentCodeSnippet = code.Substring(currentPos, nextPos - currentPos) + ";";

							if (columnOffset > 0)
							{
								debugColumnOffset = string.Format("/*COLUMN_OFFSET:{0},{1}*/", columnOffset, currentCodeSnippet.Length);
							}
							snippet = string.Format("Write(@{0}{1}{0});", '"', snippet);
							string offsetString = "\n" + new string(' ', columnOffset);
							snippet = offsetString + currentCodeSnippet;
							//snippet = debugColumnOffset +
							//          currentCodeSnippet;
						}
						else
						{
							currentPos++;
							snippet = code.Substring(currentPos, nextPos - currentPos);//.Trim();

							if (!string.IsNullOrEmpty(snippet))
							{
								if (snippet.LastIndexOf(";") == snippet.Length - 1)
									snippet = snippet.Substring(0, snippet.Length - 1);

								string debugColumnOffset;
								int lastLineBreak = code.LastIndexOf("\n", currentPos);
								int columnOffset = lastLineBreak >= 0 ? currentPos - lastLineBreak - 1 : currentPos;
								debugColumnOffset = string.Format("/*COLUMN_OFFSET:{0},{1}*/", columnOffset, snippet.Length);
								snippet = string.Format("Write({1});", '"', snippet);
								string offsetString = "\n" + new string(' ', columnOffset - "Write(".Length);
								snippet = offsetString + snippet;
							}
						}
					}
					else // Read to EOF
					{
						if (currentPos < code.Length)
						{
							snippet = code.Substring(currentPos) + '\n';
						}
					}
					inCode = false;
				}
				else // We are in template text, not code
				{
					nextPos = code.IndexOf(delimiterStart, currentPos);

					// Check for the escape character eg: '\<%' which users can use to output ASP style delimiters.
					// A double '\\' negates the escaping.
					while (nextPos > 0 && code[nextPos - 1] == '\\' && nextPos > 1 &&
						   code[nextPos - 2] != '\\')
					{
						code = code.Remove(nextPos - 1, 1);
						nextPos--;
						nextPos = code.IndexOf(delimiterStart, nextPos + 1);
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
			sb.AppendLine(string.Format("\n#line {0}", LAST_LINE_IN_FUNCTION));
			sb.Append(@"
	__output = _SBStack.Pop().ToString();");
			sb.AppendLine("\n#line default\n");
			sb.AppendLine("}\n");
			sb.Append("\treturn __output;");
			return sb.ToString();
		}

		public static string FormatFunctionBodyAsCodeOnly(string functionName, string code)
		{
			code = code.Replace("\r\n", "\n");

			// We need to put EOL markers into the code,
			// so that we can identify lines that errors occur on.
			return AddDebugSymbols(code, functionName, SyntaxEditorHelper.ScriptLanguageTypes.CSharp);
		}

		private static string AddDebugSymbols(string code, string functionName, SyntaxEditorHelper.ScriptLanguageTypes language)
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

			// Add a 10-space buffer to the start of every line, so that we can determine correct column offsets
			// even when "Write(" is prepended to certain calls
			for (int i = 0; i < lines.Length; i++)
				lines[i] = "          " + lines[i];

			var sb2 = new StringBuilder(code.Length + lines.Length * 15);

			for (int lineCounter = 1; lineCounter <= lines.Length; lineCounter++)
			{
				switch (language)
				{
					case SyntaxEditorHelper.ScriptLanguageTypes.CSharp: // TODO: Investigate whether inserting at end is faster than appending, also whether using string.format is faster, also use AppendLine rather
						sb2.AppendFormat("{0}/*DEBUG:{1}:{2}*/\n", lines[lineCounter - 1], functionName, lineCounter);
						break;
					case SyntaxEditorHelper.ScriptLanguageTypes.VbNet:
						sb2.AppendFormat("{0}'DEBUG:{1}:{2}*/\n", lines[lineCounter - 1], functionName, lineCounter);
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

		public static string RemoveDebugSymbols(string text)
		{
			int nextDebugSymbol = text.IndexOf("/*DEBUG:");
			int debugSymbolEnd;
			var sb = new StringBuilder(text);

			while (nextDebugSymbol > 0)
			{
				nextDebugSymbol = sb.ToString().IndexOf("/*DEBUG:");

				if (nextDebugSymbol < 0)
					break;

				debugSymbolEnd = sb.ToString().IndexOf("*/", nextDebugSymbol) + 2;

				if (debugSymbolEnd < nextDebugSymbol)
					break;

				if (nextDebugSymbol >= 0)
					sb.Remove(nextDebugSymbol, debugSymbolEnd - nextDebugSymbol);
			}
			nextDebugSymbol = text.IndexOf("/*COLUMN_OFFSET:");

			while (nextDebugSymbol > 0)
			{
				debugSymbolEnd = sb.ToString().IndexOf("*/", nextDebugSymbol) + 2;
				nextDebugSymbol = sb.ToString().IndexOf("/*COLUMN_OFFSET:");

				if (nextDebugSymbol >= 0)
					sb.Remove(nextDebugSymbol, debugSymbolEnd - nextDebugSymbol);
			}
			return sb.ToString();
		}

		private const string VirtualPropertiesGetterRegex = "VirtualProperties\\.(?<Name>\\w*)";
		private const string VirtualPropertiesSetterRegex = "VirtualProperties\\.(?<Name>\\w*)\\s*=\\s*(?<Value>.*)(?=\\s*;)";
		private static string ReplaceVirtualPropertyCalls(string text)
		{
			string output = Regex.Replace(text, VirtualPropertiesSetterRegex, match => "set_" + match.Groups["Name"].Value + "(" + match.Groups["Value"].Value + ")");
			output = Regex.Replace(output, VirtualPropertiesGetterRegex, match => "get_" + match.Groups["Name"].Value + "()");

			return output;
		}

		public static void InsertWriteCalls(StringBuilder sb)
		{
			sb.Append(
				@"
							public static StringBuilder GetCurrentStringBuilder()
							{
								return _SBStack.Peek();
							}

							private static void Write(object s)
							{
								if (s != null)
								{
									StringBuilder b = _SBStack.Peek();
									b.Insert(b.Length, s.ToString());
								}
							}
							
							private static void WriteLine(object s)
							{
								if (s != null)
								{
									StringBuilder b = _SBStack.Peek();
									b.Insert(b.Length, s.ToString() + Environment.NewLine);
								}
							}

							private static void WriteFormat(string format, params object[] args)
							{
								if (!string.IsNullOrEmpty(format))
								{
									StringBuilder b = _SBStack.Peek();
									b.Insert(b.Length, string.Format(format, args));
								}
							}

							private static void WriteIf(bool val, object trueText)
							{
								if (val && trueText != null)
								{
									StringBuilder b = _SBStack.Peek();
									b.Insert(b.Length, trueText.ToString());
								}
							}

							private static void WriteIf(bool val, object trueText, object falseText)
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
	}
}
