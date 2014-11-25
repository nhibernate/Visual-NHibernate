using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Slyce.Common.StringExtensions;

namespace Slyce.Common
{
	public static class NamingHelper
	{
		//Compliant with item 2.4.2 of the C# specification			
		private static readonly Regex CleanNameRegex = new Regex(@"[^\p{Ll}\p{Lu}\p{Lt}\p{Lo}\p{Nd}\p{Nl}\p{Mn}\p{Mc}\p{Cf}\p{Pc}\p{Lm}]", RegexOptions.Compiled);
		private static readonly CodeDomProvider CSharpProvider = CodeDomProvider.CreateProvider("C#");
		public static string CleanNameCSharp(string name)
		{
			if (string.IsNullOrEmpty(name)) return "";

			Slyce.Common.Utility.GetCamelCase(name);
			string ret = CleanNameRegex.Replace(name, "_"); //The identifier must start with a character 

			if (!char.IsLetter(ret, 0))
				ret = string.Concat("_", ret);
			else if (!CSharpProvider.IsValidIdentifier(ret)) // This means it is a keyword
				ret = string.Concat("@", ret);
			return ret;
		}

		public static string Pluralize(this string word)
		{
			return Inflector.Net.Inflector.Pluralize(word) ?? word;
		}

		public static string Singularize(this string word)
		{
			return Inflector.Net.Inflector.Singularize(word) ?? word;
		}

		/// <summary>
		/// Returns the next name in a incrementing series. 
		/// For example: Given the base name Example and the name list 
		/// { Example1, Example2, Example4 } it would return Example3. 
		/// </summary>
		/// <param name="baseName">The name to which we will be appending a number</param>
		/// <param name="strings">The names that already exist.</param>
		/// <returns>The next name in the incrementing series, or the base name if it is not in use.</returns>
		public static string GetNextName(this string baseName, IEnumerable<string> strings)
		{
			int extension = 1;

			HashSet<string> stringSet = new HashSet<string>(strings);

			if (stringSet.Contains(baseName) == false)
				return baseName;

			while (stringSet.Contains(baseName + extension))
				extension++;

			return baseName + extension;
		}

		/// <summary>
		/// Used to convert an Entity name like "Order Details" into a string that
		/// can be used as a CSharp identifier, like "Order_Details' in this case
		/// </summary>
		/// <param name="name">The name to convert into a valid CSharp identifier.</param>
		/// <returns>A valid CSharp identifier based on the given name.</returns>
		public static string GetCSharpFriendlyIdentifier(this string name)
		{
			return CleanNameCSharp(name.ToTitleCase());
		}

		public static string RemovePrefixes(this string name, IList<string> prefixes)
		{
			if (prefixes == null)
				return name;

			foreach (var prefix in prefixes)
				if (name.StartsWith(prefix) && prefix.Length > 0 && name.Length > prefix.Length)
					return name.Remove(0, prefix.Length);

			return name;
		}

		public static string RemoveSuffixes(this string name, IList<string> postfixes)
		{
			if (postfixes == null)
				return name;

			foreach (var prefix in postfixes)
				if (name.EndsWith(prefix) && prefix.Length > 0 && name.Length > prefix.Length)
					return name.Remove(name.Length - prefix.Length);

			return name;
		}
	}
}
