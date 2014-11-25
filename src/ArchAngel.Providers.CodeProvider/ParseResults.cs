using System.Collections.Generic;
using System.IO;
using System.Linq;
using ArchAngel.Providers.CodeProvider.CSharp;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider
{
	public class ParseResults
	{
		public readonly DoubleLookup<string, CodeRoot> parsedFiles = new DoubleLookup<string, CodeRoot>();
		public readonly LookupList<string, Class> parsedClasses = new LookupList<string, Class>();

		public static ParseResults ParseCSharpFiles(IEnumerable<string> csharpFiles)
		{
			var parser = new CSharpParser();
			var parseResults = new ParseResults();

			foreach (var file in csharpFiles)
			{
				if (File.Exists(file) == false)
					continue;

				parser.Reset();
				parser.ParseCode(file, File.ReadAllText(file));
				parseResults.AddParsedFile(file, parser.CreatedCodeRoot as CodeRoot);
			}

			return parseResults;
		}

		/// <summary>
		/// Parses the source-code in the csharpFileText.
		/// </summary>
		/// <param name="csharpFileText"></param>
		/// <returns></returns>
		public static CodeRoot ParseCSharpCode(string csharpFileText)
		{
			var parser = new CSharpParser();
			var parseResults = new ParseResults();

			parser.Reset();
			parser.ParseCode(csharpFileText);

			return (CodeRoot)parser.CreatedCodeRoot;
		}

		/// <summary>
		/// Parses the source-code in the csharpFileText.
		/// </summary>
		/// <param name="vbFileText"></param>
		/// <returns></returns>
		public static CodeRoot ParseVbCode(string vbFileText)
		{
			var parser = new CSharpParser();
			var parseResults = new ParseResults();

			parser.Reset();
			parser.ParseCode(vbFileText);

			return (CodeRoot)parser.CreatedCodeRoot;
		}

		public void AddParsedFile(string filename, CodeRoot codeRoot)
		{
			parsedFiles.Add(filename, codeRoot);

			foreach (var topLevelClass in codeRoot.Classes)
			{
				parsedClasses.Add("", topLevelClass);
			}

			ProcessNewNamespaces("", codeRoot.Namespaces);
		}

		private void ProcessNewNamespaces(string baseNamespace, IEnumerable<Namespace> namespaces)
		{
			foreach (var ns in namespaces)
			{
				ProcessInnerClasses(baseNamespace, ns.Name, ns.Classes);
				ProcessNewNamespaces(baseNamespace + ns.Name, ns.InnerNamespaces);
			}
		}

		private void ProcessInnerClasses(string baseNamespace, string className, IEnumerable<Class> innerClasses)
		{
			var currentNamespace = baseNamespace + className;
			foreach (var clazz in innerClasses)
			{
				parsedClasses.Add(currentNamespace, clazz);
				ProcessInnerClasses(currentNamespace, clazz.Name, clazz.InnerClasses);
			}
		}

		public IEnumerable<Class> GetClassesInNamespace(string @namespace)
		{
			if (string.IsNullOrEmpty(@namespace))
				return parsedClasses.Values;

			if (parsedClasses.ContainsKey(@namespace))
				return parsedClasses[@namespace];

			return new List<Class>();
		}

		public Class FindClass(string name, List<string> withMajorityOfProperties)
		{
			if (string.IsNullOrEmpty(name)) return null;

			List<Class> possibleClasses = new List<Class>();
			int index = name.LastIndexOf(".");
			if (index == -1)
			{
				// check top level classes if we are given an unqualified class name.
				possibleClasses = GetClassesInNamespace("").Where(c => c.Name == name).ToList();
			}
			else
			{
				var ns = name.Substring(0, index);
				var className = name.Substring(index + 1);
				possibleClasses = GetClassesInNamespace(ns).Where(c => c.Name == className).ToList();
			}
			if (possibleClasses.Count == 0)
				return null;
			else if (possibleClasses.Count == 1)
				return possibleClasses[0];
			else
			{
				Class bestMatchClass = null;
				int currentBestCount = -1;

				// Find the class with the largest proportion of matching properties
				foreach (var c in possibleClasses)
				{
					int count = c.Properties.Count(p => withMajorityOfProperties.Contains(p.Name));

					if (count > currentBestCount)
					{
						currentBestCount = count;
						bestMatchClass = c;
					}
				}
				return bestMatchClass;
			}
		}

		public string GetFilenameForParsedClass(Class @class)
		{
			var codeRoot = @class.Controller.Root;

			if (parsedFiles.Contains(codeRoot))
			{
				return parsedFiles[codeRoot];
			}
			return "";
		}
	}
}
