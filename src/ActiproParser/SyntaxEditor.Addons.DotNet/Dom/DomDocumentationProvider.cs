using System;
using System.Collections;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom {

	/// <summary>
	/// Represents a .NET documentation provider.
	/// </summary>
	public class DomDocumentationProvider {
		
		#region Documentation Key Format
		/*

		 Documentation Key Format:
		 - No whitespace permitted in key
		 - First part is a item type ID, followed by a ":"
		 - Second part is full name of the item, separated by only "." characters, and including containing namespaces/types
		 - "#" is used in place of "." when "." is in a name (e.g. M:System.String.#ctor)
		 - For members with parameters, "(" and ")" enclose the parameter list and each parameter is delimited by a ","
		 - Parameterless members do not include a "(" or ")"
		 - Pass by-reference parameters have a "@" following the parameter type name
		 - Array parameters have a "[]" following the parameter type name
		 - `<number> = Added at the end of a type to indicate the generic argument count (e.g. M:System.Collections.Generic.LinkedList`1.Remove(`0))
		   - Also used as a zero-based index backreference to a generic argument
		 - ``<number> = Added at the end of a member to indicate the generic argument count (e.g. M:System.Array.Exists``1(``0[],System.Predicate{``0}))
		   - Also used as a zero-based index backreference to a generic argument
		 - Both type and member generic arguments can be used (e.g. M:System.Collections.Generic.List`1.ConvertAll``1(System.Converter{`0,``0}))
		 - Generic arguments used as parameters are enclosed by a "{" and "}"
		 
		 Item Type IDs:
		 - E = Event
		 - F = Field
		 - M = Method (includes constructors, operators, etc.)
		 - N = Namespace
		 - P = Property (includes indexers)
		 - T = Type
		 
		 */
		#endregion

		private string		documentation;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>DomDocumentationProvider</c> class.
		/// </summary>
		/// <param name="documentation">The XML documentation.</param>
		public DomDocumentationProvider(string documentation) {
			// Initialize parameters
			this.documentation = documentation;

		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Appends a parameter to the <see cref="StringBuilder"/>.
		/// </summary>
		/// <param name="key">The <see cref="StringBuilder"/> to modify.</param>
		/// <param name="declaringTypeGenericTypeArguments">The collection of declaring type generic type arguments.</param>
		/// <param name="memberGenericTypeArguments">The collection of member generic type arguments.</param>
		/// <param name="parameterType">The parameter type.</param>
		private static void AppendParameter(StringBuilder key, ICollection declaringTypeGenericTypeArguments, ICollection memberGenericTypeArguments, IDomTypeReference parameterType) {
			// Get the parameter name without any [] or &
			string parameterTypeName = parameterType.Name;
			if (parameterTypeName.EndsWith("&"))
				parameterTypeName = parameterTypeName.Substring(0, parameterTypeName.Length - 1);
			bool isArray = false;
			if (parameterTypeName.EndsWith("[]")) {
				isArray = true;
				parameterTypeName = parameterTypeName.Substring(0, parameterTypeName.Length - 2);
			}

			// Look for the index of the parameter in the member's generic type arguments
			int genericParameterIndex = -1;
			if (memberGenericTypeArguments != null) {
				int index = 0;
				foreach (IDomTypeReference genericTypeArguments in memberGenericTypeArguments) {
					if ((parameterType.IsGenericParameter) && (genericTypeArguments.Name == parameterTypeName)) {
						genericParameterIndex = index;
						break;
					}
					index++;
				}
			}
			if (genericParameterIndex != -1) {
				key.Append("``");
				key.Append(genericParameterIndex);
				if (isArray)
					key.Append("[]");
			}
			else {
				// Look for the index of the parameter in the declaring type's generic type arguments
				if (declaringTypeGenericTypeArguments != null) {
					int index = 0;
					foreach (IDomTypeReference genericTypeArguments in declaringTypeGenericTypeArguments) {
						if ((parameterType.IsGenericParameter) && (genericTypeArguments.Name == parameterTypeName)) {
							genericParameterIndex = index;
							break;
						}
						index++;
					}
				}
				if (genericParameterIndex != -1) {
					key.Append("`");
					key.Append(genericParameterIndex);
					if (isArray)
						key.Append("[]");
				}
				else {
					// Add the full name of the parameter type
					key.Append(DotNetProjectResolver.GetTypeFullNameForDisplay(parameterType.FullName));

					if (parameterType.IsGenericType) {
						key.Append("{");
						genericParameterIndex = 0;
						ICollection parameterGenericTypeArguments = parameterType.GenericTypeArguments;
						foreach (IDomTypeReference genericTypeArgument in parameterGenericTypeArguments) {
							if (genericParameterIndex++ > 0)
								key.Append(",");
							DomDocumentationProvider.AppendParameter(key, parameterGenericTypeArguments, memberGenericTypeArguments, genericTypeArgument); 
						}
						key.Append("}");
					}
				}
			}
		}

		/// <summary>
		/// Gets the documentation key for an <see cref="IDomMember"/>.
		/// </summary>
		/// <param name="member">The <see cref="IDomMember"/> to examine.</param>
		/// <returns>The documentation key for an <see cref="IDomMember"/>.</returns>
		internal static string GetMemberDocumentationKey(IDomMember member) {
			// Get the keyroot
			char keyRoot;
			switch (member.MemberType) {
				case DomMemberType.Event:
					keyRoot = 'E';
					break;
				case DomMemberType.Constant:
				case DomMemberType.Field:
					keyRoot = 'F';
					break;
				case DomMemberType.Property:
					keyRoot = 'P';
					break;
				default:
					keyRoot = 'M';
					break;
			}

			// Start building the lookup key
			StringBuilder key = new StringBuilder();
			key.Append(keyRoot);
			key.Append(":");

			// Remove any nested type delimiters
			key.Append(member.DeclaringType.FullName.Replace('+', '.'));

			// Append the member name
			key.Append(".");
			switch (member.Name) {
				case ".ctor": 
					key.Append("#ctor");
					break;
				case ".cctor": 
					key.Append("#cctor");
					break;
				default: 
					key.Append(member.Name);
					break;
			}

			// Append generic member mark if appropriate
			ICollection memberGenericTypeArguments = member.GenericTypeArguments;
			if ((member.IsGenericMethod) && (memberGenericTypeArguments != null) && (memberGenericTypeArguments.Count > 0)) {
				key.Append("``");
				key.Append(memberGenericTypeArguments.Count);
			}

			// Add parameters
			if ((member.Parameters != null) && (member.Parameters.Length > 0)) {
				key.Append("(");
				ICollection declaringTypeGenericTypeArguments = member.DeclaringType.GenericTypeArguments;
				for (int index = 0; index < member.Parameters.Length; index++) {
					IDomParameter parameter = (IDomParameter)member.Parameters[index];
					if (parameter.ParameterType != null) {
						if (index > 0)
							key.Append(",");

						// Append the parameter
						DomDocumentationProvider.AppendParameter(key, declaringTypeGenericTypeArguments, memberGenericTypeArguments, parameter.ParameterType);

						// Append @ mark for by-ref and output parameters
						if ((parameter.IsByReference) || (parameter.IsOutput))
							key.Append("@");
					}						
				}
				key.Append(")");
			}

			return key.ToString();
		}

		/// <summary>
		/// Gets the documentation key for an <see cref="IDomTypeReference"/>.
		/// </summary>
		/// <param name="typeReference">The <see cref="IDomTypeReference"/> to examine.</param>
		/// <returns>The documentation key for an <see cref="IDomTypeReference"/>.</returns>
		internal static string GetTypeReferenceDocumentationKey(IDomTypeReference typeReference) {
			return "T:" + typeReference.FullName.Replace('+', '.');
		}

		/// <summary>
		/// Escapes the type name for display in a documentation comment.
		/// </summary>
		/// <param name="typeName">The type name to escape.</param>
		/// <returns>The escaped type name.</returns>
		private static string EscapeTypeName(string typeName) {
			// NOTE: Maybe improve this in the future to actually show the generic type arguments
			if (typeName.LastIndexOf('`') != -1)
				typeName = DotNetProjectResolver.GetTypeFullNameForDisplay(typeName) + "<>";

			return IntelliPrompt.EscapeMarkupText(typeName);
		}
		
		/// <summary>
		/// Escapes the specified XML comment text.
		/// </summary>
		/// <param name="projectResolver">The <see cref="DotNetProjectResolver"/> to use for resolving any type names.</param>
		/// <param name="text">The text to escape.</param>
		/// <returns>The escaped XML comment text.</returns>
		private static string EscapeXmlComment(DotNetProjectResolver projectResolver, string text) {
			if (text == null)
				return null;

			// Replace all line terminators with a space 
			text = text.Replace("\r\n", " ").Trim();
			
			// Find all matches
			string pattern = @"(\<c\>(?<c>[^\<]*)\<\/c\>)|" +
				@"(\<paramref name\=\""(.\:)?(?<see>[^""]*)\""((\>\<\/paramref\>)|(\s*\/\>)))|" +
				@"(\<see(also)? cref\=\""(.\:)?[^""]*\""\s*\>(?<see>[^\<]+)\</see(also)?\>)|" +
				@"(\<see(also)? cref\=\""(.\:)?(?<see>[^""]*)\""\s*\/?\>)|" +
				@"(\</?(para|see(also)?)\>)";
			MatchCollection matches = Regex.Matches(text, pattern);
			
			// Replace matches 
			int endOffset = text.Length;
			StringBuilder escapedText = new StringBuilder();
			for (int index = matches.Count - 1; index >= 0; index--) {
				Match match = matches[index];
				if (match.Index + match.Length < endOffset)
					escapedText.Insert(0, IntelliPrompt.EscapeMarkupText(text.Substring(match.Index + match.Length, endOffset - (match.Index + match.Length))));

				if (match.Groups["see"].Success) {
					string typeName = match.Groups["see"].Value;
					if (projectResolver != null) {
						IDomType type = projectResolver.GetType(null, (string[])null, typeName, DomBindingFlags.Default);
						if ((type != null) && (type.IsGenericTypeDefinition)) {
							typeName = DotNetProjectResolver.GetTypeFullNameForDisplay(type.FullName) + "<";
							ICollection genericTypeArguments = type.GenericTypeArguments;
							foreach (IDomTypeReference genericTypeArgument in genericTypeArguments) {
								if (!typeName.EndsWith("<"))
									typeName += ',';
								typeName += genericTypeArgument.Name;
							}
							typeName += ">";
						}
					}
					escapedText.Insert(0, String.Format("<b>{0}</b>", DomDocumentationProvider.EscapeTypeName(typeName)));
				}
				else if (match.Groups["c"].Success)
					escapedText.Insert(0, String.Format("<b>{0}</b>", DomDocumentationProvider.EscapeTypeName(match.Groups["c"].Value)));

				endOffset = match.Index;
			}
			if (endOffset > 0)
				escapedText.Insert(0, IntelliPrompt.EscapeMarkupText(text.Substring(0, endOffset)));

			return escapedText.ToString();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the raw XML documentation.
		/// </summary>
		/// <value>The raw XML documentation.</value>
		public string Documentation {
			get {
				return documentation;
			}
		}

		/// <summary>
		/// Gets an array of the names of exception types that can be thrown from the member.
		/// </summary>
		/// <value>An array of the names of exception types that can be thrown from the member.</value>
		public string[] ExceptionTypeNames {
			get {
				XmlDocument document = new XmlDocument();
				try {
					document.LoadXml("<root>" + documentation + "</root>");
					XmlNodeList nodes = document.DocumentElement.SelectNodes("exception/@cref");
					if ((nodes != null) && (nodes.Count > 0)) {
						string[] typeNames = new string[nodes.Count];
						for (int index = 0; index < nodes.Count; index++)
							typeNames[index] = (nodes[index].InnerText.Length > 2 ? nodes[index].InnerText.Substring(2) : nodes[index].InnerText);
						return typeNames;
					}
				}
				catch (XmlException) {}
				return null;
			}
		}

		/// <summary>
		/// Returns the escaped value of the <c>param</c> tag for the specified parameter in the documentation.
		/// </summary>
		/// <param name="projectResolver">The <see cref="DotNetProjectResolver"/> currently in use, if known.</param>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <returns>The escaped value of the <c>param</c> tag for the specified parameter in the documentation.</returns>
		public string GetParameter(DotNetProjectResolver projectResolver, string parameterName) {
			XmlDocument document = new XmlDocument();
			try {
				document.LoadXml("<root>" + documentation + "</root>");
				XmlNode node = document.DocumentElement.SelectSingleNode(String.Format("param[@name='{0}']", parameterName));
				if (node != null)
					return DomDocumentationProvider.EscapeXmlComment(projectResolver, node.InnerXml.Trim());
			}
			catch {}
			return null;
		}

		/// <summary>
		/// Returns the escaped value of the <c>summary</c> tag in the documentation.
		/// </summary>
		/// <param name="projectResolver">The <see cref="DotNetProjectResolver"/> currently in use, if known.</param>
		/// <value>The escaped value of the <c>summary</c> tag in the documentation.</value>
		public string GetSummary(DotNetProjectResolver projectResolver) {
			XmlDocument document = new XmlDocument();
			try {
				document.LoadXml("<root>" + documentation + "</root>");
				XmlNode node = document.DocumentElement.SelectSingleNode("summary");
				if (node != null)
					return DomDocumentationProvider.EscapeXmlComment(projectResolver, node.InnerXml.Trim());
			}
			catch {}
			return null;
		}

	}
}
