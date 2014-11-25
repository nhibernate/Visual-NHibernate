using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Text;
using ActiproSoftware.ComponentModel;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Context;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom {

	/// <summary>
	/// Represents a resolver for a .NET project's type/member information.
	/// </summary>
	public class DotNetProjectResolver : DisposableObject {

		private StringCollection			externalReferences		= new StringCollection();
		private string						cachePath;
		private CodeSnippetFolderCollection	codeSnippetFolders		= new CodeSnippetFolderCollection();
		private ResolveEventHandler			hostAppDomainResolver;
		private SourceProjectContent		sourceProjectContent	= new SourceProjectContent();
		private string						systemPath;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>DotNetProjectResolver</c> class.
		/// </summary>
		public DotNetProjectResolver() {
			// Get the system path
			Assembly mscorlibAssembly = this.GetAssemblyFromAppDomain("mscorlib");
			systemPath = Path.GetDirectoryName(mscorlibAssembly.Location);

			try {
				// Get a default cache path that should be overridden but will help when customers forget to set it (http://www.actiprosoftware.com/Support/Forums/ViewForumTopic.aspx?ForumTopicID=2837#10475)
				cachePath = Path.Combine(Path.GetTempPath(), "SyntaxEditorDotNetProjectResolverCache");
			} catch {}
		}

		/// <summary>
		/// Initializes a new instance of the <c>DotNetProjectResolver</c> class.
		/// </summary>
		/// <param name="hostAppDomainResolver">The custom <see cref="ResolveEventHandler"/> to use when attempting to resolve assemblies loaded in the host <c>AppDomain</c> by the <see cref="AssemblyCodeRepository"/>.</param>
		public DotNetProjectResolver(ResolveEventHandler hostAppDomainResolver) : this() {
			// Initialize
			this.hostAppDomainResolver = hostAppDomainResolver;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// MARKUP PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the markup for a comment.
		/// </summary>
		/// <param name="comment">The comment text.</param>
		/// <returns>The markup for a comment.</returns>
		private static string GetMarkupForComment(string comment) {
			return "<span style=\"color: green;\">" + comment + "</span>";
		}
		
		/// <summary>
		/// Gets the markup for a keyword.
		/// </summary>
		/// <param name="keyword">The keyword text.</param>
		/// <returns>The markup for a keyword.</returns>
		private static string GetMarkupForKeyword(string keyword) {
			return "<span style=\"color: blue;\">" + keyword + "</span>";
		}

		/// <summary>
		/// Gets the markup for an overload count.
		/// </summary>
		/// <param name="overloadCount">The overload count.</param>
		/// <returns>The markup for an overload count.</returns>
		private static string GetMarkupForOverloadCount(int overloadCount) {
			if (overloadCount > 1)
				return " <i style=\"color: gray;\">(+" + (overloadCount - 1) + " overload" + (overloadCount > 2 ? "s" : String.Empty) + ")</i>";
			else
				return String.Empty;
		}
		
		/// <summary>
		/// Gets the markup for a type.
		/// </summary>
		/// <param name="language">The <see cref="DotNetLanguage"/> requesting the markup.</param>
		/// <param name="typeReference">The <see cref="IDomTypeReference"/> to examine.</param>
		/// <param name="nameOnly">Whether to only include the type name and strip any namespaces or declaring types.</param>
		/// <param name="bold">Whether to bold the type name.</param>
		/// <returns>The markup for a type.</returns>
		private static string GetMarkupForType(DotNetLanguage language, IDomTypeReference typeReference, bool nameOnly, bool bold) {
			return DotNetProjectResolver.GetMarkupForType(language, typeReference.FullName, nameOnly, bold, typeReference.GenericTypeArguments);
		}

		/// <summary>
		/// Gets the markup for a type.
		/// </summary>
		/// <param name="language">The <see cref="DotNetLanguage"/> requesting the markup.</param>
		/// <param name="fullTypeName">The full type name.</param>
		/// <param name="nameOnly">Whether to only include the type name and strip any namespaces or declaring types.</param>
		/// <param name="bold">Whether to bold the type name.</param>
		/// <returns>The markup for a type.</returns>
		private static string GetMarkupForType(DotNetLanguage language, string fullTypeName, bool nameOnly, bool bold) {
			return DotNetProjectResolver.GetMarkupForType(language, fullTypeName, nameOnly, bold, null);
		}

		/// <summary>
		/// Gets the markup for a type.
		/// </summary>
		/// <param name="language">The <see cref="DotNetLanguage"/> requesting the markup.</param>
		/// <param name="fullTypeName">The full type name.</param>
		/// <param name="nameOnly">Whether to only include the type name and strip any namespaces or declaring types.</param>
		/// <param name="bold">Whether to bold the type name.</param>
		/// <param name="genericTypeArguments">The collection of generic type arguments.</param>
		/// <returns>The markup for a type.</returns>
		private static string GetMarkupForType(DotNetLanguage language, string fullTypeName, bool nameOnly, bool bold, ICollection genericTypeArguments) {
			StringBuilder result = new StringBuilder();

			// Append bold tag if desired
			if (bold)
				result.Append("<b>");

			// Remove any trailing & mark for by-reference
			if (fullTypeName.EndsWith("&"))
				fullTypeName = fullTypeName.Substring(0, fullTypeName.Length - 1);

			// Trim off the pointer/array suffix
			int suffixIndex = fullTypeName.IndexOfAny(new char[] { '*', '[' });
			string suffix = (suffixIndex != -1 ? fullTypeName.Substring(suffixIndex) : String.Empty);
			if (suffixIndex != -1) {
				fullTypeName = fullTypeName.Substring(0, suffixIndex);

				if (language == DotNetLanguage.VB)
					suffix = suffix.Replace('[', '(').Replace(']', ')');
			}

			// If the type name is a generic type, find the index of the mark
			int markIndex = fullTypeName.IndexOf('`');
			int dotIndex = fullTypeName.LastIndexOfAny(new char[] { '.', '+' });
			if (markIndex != -1) {
				if (dotIndex > markIndex)
					markIndex = -1;
				else
					fullTypeName = fullTypeName.Substring(0, markIndex);
			}
			
			// Resolve the full type to a language shortcut if possible
			fullTypeName = DotNetProjectResolver.GetTypeShortcutName(language, fullTypeName);

			// Get whether the type is a keyword
			bool isKeyword = false;
			switch (language) {
				case DotNetLanguage.CSharp:
					switch (fullTypeName) {
						case "bool":
						case "byte":
						case "char":
						case "decimal":
						case "double":
						case "dynamic":
						case "float":
						case "int":
						case "long":
						case "object":
						case "sbyte":
						case "short":
						case "string":
						case "ushort":
						case "uint":
						case "ulong":
						case "void":
							isKeyword = true;
							break;
					}
					break;
				case DotNetLanguage.VB:
					switch (fullTypeName) {
						case "Boolean":
						case "Byte":
						case "Char":
						case "Date":
						case "Decimal":
						case "Double":
						case "Short":
						case "Integer":
						case "Long":
						case "Object":
						case "SByte":
						case "Single":
						case "String":
						case "UShort":
						case "UInteger":
						case "ULong":
						case "Void":
							isKeyword = true;
							break;
					}
					break;
			}

			if (isKeyword) {
				// Append the keyword
				if (bold)
					result.Append(fullTypeName);
				else
					result.Append("<span style=\"color: blue;\">" + fullTypeName + "</span>");
			}
			else if (dotIndex == -1) {
				// For optimization
				if (bold)
					result.Append(fullTypeName);
				else
					result.Append("<span style=\"color: teal;\">" + fullTypeName + "</span>");
			}
			else if (nameOnly) {
				// For optimization
				if (bold)
					result.Append(fullTypeName.Substring(dotIndex + 1));
				else
					result.Append("<span style=\"color: teal;\">" + fullTypeName.Substring(dotIndex + 1) + "</span>");
			}
			else if (fullTypeName[dotIndex] == '.') {
				// For optimization... not a nested type
				if (bold)
					result.Append(fullTypeName);
				else
					result.Append(fullTypeName.Substring(0, dotIndex + 1) + "<span style=\"color: teal;\">" + fullTypeName.Substring(dotIndex + 1) + "</span>");
			}
			else {
				// Use complex processing to handle nested types 

				// Append namespaces
				string[] identifiers = fullTypeName.Split(new char[] { '.' } );
				for (int index = 0; index < identifiers.Length - 1; index++) {
					result.Append(identifiers[index]);
					result.Append(".");
				}

				// Append type names
				identifiers = identifiers[identifiers.Length - 1].Split(new char[] { '+' } );
				for (int index = 0; index < identifiers.Length; index++) {
					if ((!nameOnly) && (index > 0))
						result.Append(".");
					
					if (bold)
						result.Append(identifiers[index]);
					else
						result.Append("<span style=\"color: teal;\">" + identifiers[index] + "</span>");
				}
			}

			// Append generic type arguments
			if (markIndex != -1) {
				result.Append("&lt;");
				if (genericTypeArguments != null) {
					int count = 0;
					foreach (IDomTypeReference genericTypeArgument in genericTypeArguments) {
						if (count++ > 0)
							result.Append(",");
						if (genericTypeArgument != null)
							result.Append(DotNetProjectResolver.GetMarkupForType(language, genericTypeArgument, true, bold));
					}
				}
				result.Append("&gt;");
			}

			// Append pointer/array suffix
			if (suffixIndex != -1)
				result.Append(suffix);

			// Append bold tag if desired
			if (bold)
				result.Append("</b>");
			return result.ToString();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// QUICK INFO PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the formatted text to display in quick info for the specified <see cref="CodeSnippet"/>.
		/// </summary>
		/// <param name="language">The <see cref="DotNetLanguage"/> requesting the quick info.</param>
		/// <param name="codeSnippet">The <see cref="CodeSnippet"/> to examine.</param>
		/// <returns>The formatted text to display in quick info for the specified <see cref="CodeSnippet"/>.</returns>
		internal string GetQuickInfoForCodeSnippet(DotNetLanguage language, CodeSnippet codeSnippet) {
			string quickInfo = "<i style=\"color: gray;\">(code snippet)</i> <b>" + IntelliPrompt.EscapeMarkupText(codeSnippet.Title) + "</b>";

			if (codeSnippet.Description != null)
				quickInfo += "<br/>" + DotNetProjectResolver.GetMarkupForComment(codeSnippet.Description);

			return quickInfo;
		}
		
		/// <summary>
		/// Gets the formatted text to display in quick info for the specified decimal integer number.
		/// </summary>
		/// <param name="language">The <see cref="DotNetLanguage"/> requesting the quick info.</param>
		/// <param name="number">The number to examine.</param>
		/// <param name="isHex">Whether the number is a hexidecimal number.</param>
		/// <returns>The formatted text to display in quick info for the specified decimal integer number.</returns>
		internal string GetQuickInfoForDecimalIntegerLiteral(DotNetLanguage language, string number, bool isHex) {
			bool isNegative = number.StartsWith("-");
			bool hasUnsignedSuffix = false;
			bool hasLongSuffix = false;
			string decimalValue = null;
			string hexadecimalValue = null;

			if (language == DotNetLanguage.VB) {
				string typeCharacter = String.Empty;
				int index = number.Length;
				while (index > 1) {
					if ("0123456789ABCDEF".IndexOf(Char.ToUpper(number[index - 1])) != -1)
						break;
					typeCharacter = Char.ToUpper(number[--index]) + typeCharacter;
				}
				if (typeCharacter.Length > 0)
					number = number.Substring(0, number.Length - typeCharacter.Length);

				if (typeCharacter == "&")
					hasLongSuffix = true;
				if (typeCharacter.StartsWith("U"))
					hasUnsignedSuffix = true;
				if (typeCharacter.EndsWith("L"))
					hasLongSuffix = true;
			}
			else {
				if (Char.ToUpper(number[number.Length - 1]) == 'L') {
					hasLongSuffix = true;
					number = number.Substring(0, number.Length - 1);
				}
				if (Char.ToUpper(number[number.Length - 1]) == 'U') {
					hasUnsignedSuffix = true;
					number = number.Substring(0, number.Length - 1);
				}
				if (Char.ToUpper(number[number.Length - 1]) == 'L') {  // NOTE: L check is done again since U and L can come in any order
					hasLongSuffix = true;
					number = number.Substring(0, number.Length - 1);
				}
			}
			if (isHex) {
				// Trim off 0x / &H
				number = number.Substring((isNegative ? 3 : 2), number.Length - (isNegative ? 3 : 2));
				ulong result = 0;
				int factor;
				int weight = 0;
				for (int index = number.Length - 1; index >= 0; index--) {
					switch (Char.ToUpper(number[index])) {
						case 'A':
							factor = 10;
							break;
						case 'B':
							factor = 11;
							break;
						case 'C':
							factor = 12;
							break;
						case 'D':
							factor = 13;
							break;
						case 'E':
							factor = 14;
							break;
						case 'F':
							factor = 15;
							break;
						default:
							factor = Convert.ToInt32(number[index].ToString());
							break;
					}
					result += (ulong)(factor * Math.Pow(16, weight++));
				}
				number = result.ToString();
				if (isNegative)
					number = "-" + number;
			}
			
			// int:		-2,147,483,648 to 2,147,483,647
			// uint:	0 to 4,294,967,295
			// long:	–9,223,372,036,854,775,808 to 9,223,372,036,854,775,807
			// ulong:	0 to 18,446,744,073,709,551,615

			string type = null;
			
			try {
				ulong result;
				#if NET11
				result = ulong.Parse(number);
				#else
				if (ulong.TryParse(number, out result))
				#endif
				{
					type = (language == DotNetLanguage.VB ? "ULong" : "ulong");
					decimalValue = result.ToString("N0");
					hexadecimalValue = result.ToString("X16");
				}
			}
			catch {}
			
			if (!hasUnsignedSuffix) {
				try {
					long result;
					#if NET11
					result = long.Parse(number);
					#else
					if (long.TryParse(number, out result))
					#endif
					{
						type = (language == DotNetLanguage.VB ? "Long" : "long");
						decimalValue = result.ToString("N0");
						hexadecimalValue = result.ToString("X16");
					}
				}
				catch {}
			}

			if (!hasLongSuffix) {
				try {
					uint result;
					#if NET11
					result = uint.Parse(number);
					#else
					if (uint.TryParse(number, out result))
					#endif
					{
						type = (language == DotNetLanguage.VB ? "UInteger" : "uint");
						decimalValue = result.ToString("N0");
						hexadecimalValue = result.ToString("X8");
					}
				}
				catch {}
			}

			if ((!hasUnsignedSuffix) && (!hasLongSuffix)) {
				try {
					int result;
					#if NET11
					result = int.Parse(number);
					#else
					if (int.TryParse(number, out result))
					#endif
					{
						type = (language == DotNetLanguage.VB ? "Integer" : "int");
						decimalValue = result.ToString("N0");
						hexadecimalValue = result.ToString("X8");
					}
				}
				catch {}
			}

			if (type == null)
				return null;

			if (isHex) {
				if (language == DotNetLanguage.VB)
					return "&amp;H" + hexadecimalValue + DotNetProjectResolver.GetMarkupForKeyword(" As " + type) + DotNetProjectResolver.GetMarkupForComment("&nbsp;&nbsp;&nbsp;&apos; " + decimalValue);
				else
					return DotNetProjectResolver.GetMarkupForKeyword(type) + " 0x" + hexadecimalValue + DotNetProjectResolver.GetMarkupForComment("&nbsp;&nbsp;&nbsp;// " + decimalValue);
			}
			else {
				if (language == DotNetLanguage.VB)
					return decimalValue + DotNetProjectResolver.GetMarkupForKeyword(" As " + type) + DotNetProjectResolver.GetMarkupForComment("&nbsp;&nbsp;&nbsp;&apos; &amp;H" + hexadecimalValue);
				else
					return DotNetProjectResolver.GetMarkupForKeyword(type) + " " + decimalValue + DotNetProjectResolver.GetMarkupForComment("&nbsp;&nbsp;&nbsp;// 0x" + hexadecimalValue);
			}
		}
		
		/// <summary>
		/// Gets the formatted text to display in quick info for the specified keyword.
		/// </summary>
		/// <param name="language">The <see cref="DotNetLanguage"/> requesting the quick info.</param>
		/// <param name="keyword">The keyword to examine.</param>
		/// <returns>The formatted text to display in quick info for the specified keyword.</returns>
		internal string GetQuickInfoForKeyword(DotNetLanguage language, string keyword) {
			string resolvedType = DotNetProjectResolver.GetTypeFullNameFromShortcut(language, keyword);
			if (keyword != resolvedType) {
				// Get the actual type's quick info
				string typeQuickInfo = String.Empty;
				IDomType type = this.GetNativeType(resolvedType);
				if (type != null) {
					typeQuickInfo = "<br/>Resolves to " + resolvedType + ".";

					// Get the summary
					DomDocumentationProvider documentationProvider = type.DocumentationProvider;
					string summary = documentationProvider.GetSummary(this);
					if ((summary != null) && (summary.Length > 0))
						typeQuickInfo += "<br/>" + DotNetProjectResolver.GetMarkupForComment(summary);
				}
			
				// Native type
				return "<i style=\"color: gray;\">(native type)</i> <b>" + IntelliPrompt.EscapeMarkupText(keyword) + "</b>" + typeQuickInfo;
			}
			else {
				// Regular keyword
				return "<i style=\"color: gray;\">(keyword)</i> <b>" + IntelliPrompt.EscapeMarkupText(keyword) + "</b>";
			}
		}
		
		/// <summary>
		/// Gets the formatted text to display in quick info for the specified <see cref="IDomTypeReference"/> that is from a local variable reference.
		/// </summary>
		/// <param name="language">The <see cref="DotNetLanguage"/> requesting the quick info.</param>
		/// <param name="context">The <see cref="DotNetContext"/>, if known.</param>
		/// <param name="typeReference">The <see cref="IDomTypeReference"/> of the variable.</param>
		/// <param name="variableName">The variable name.</param>
		/// <param name="reflectionIconsEnabled">Whether reflection icons are enabled.</param>
		/// <returns>The formatted text to display in quick info for the specified <see cref="IDomTypeReference"/> that is from a local variable reference.</returns>
		internal string GetQuickInfoForLocalVariable(DotNetLanguage language, DotNetContext context, IDomTypeReference typeReference, string variableName, bool reflectionIconsEnabled) {
			if ((typeReference == null) || (variableName == null))
				return null;
			
			// Try and get the type
			IDomType type = typeReference as IDomType;
			if ((type == null) && (context != null)) {
				type = typeReference.Resolve(this);
				if (type != null)
					typeReference = type;
			}

			// Get the general info
			string quickInfo = "<i style=\"color: gray;\">(local variable)</i> " +
				(reflectionIconsEnabled ? @"<img src=""resource:PublicField"" align=""absbottom""/> " : String.Empty) +
				DotNetProjectResolver.GetMarkupForType(language, typeReference, false, true) + " " +
				IntelliPrompt.EscapeMarkupText(variableName);
			
			if (type != null) {
				// Get the summary of the type, if known
				DomDocumentationProvider documentationProvider = type.DocumentationProvider;
				string summary = documentationProvider.GetSummary(this);
				if ((summary == null) && (type.FullName == TypeReference.AnonymousTypeName))
					summary = "An anonymous type.";
				if ((summary != null) && (summary.Length > 0))
					quickInfo += "<br/>" + DotNetProjectResolver.GetMarkupForComment(summary);
			}

			return quickInfo;
		}

		/// <summary>
		/// Gets the formatted text to display in quick info for the specified <see cref="IDomMember"/>.
		/// </summary>
		/// <param name="language">The <see cref="DotNetLanguage"/> requesting the quick info.</param>
		/// <param name="context">The <see cref="DotNetContext"/>, if known.</param>
		/// <param name="type">The <see cref="IDomType"/> that references the member.</param>
		/// <param name="member">The <see cref="IDomMember"/> to examine.</param>
		/// <param name="selectedParameterIndex">The index of the selected parameter, or <c>-1</c> if none.</param>
		/// <param name="reflectionIconsEnabled">Whether reflection icons are enabled.</param>
		/// <returns>The formatted text to display in quick info for the specified <see cref="IDomMember"/>.</returns>
		internal string GetQuickInfoForMember(DotNetLanguage language, DotNetContext context, 
			IDomType type, IDomMember member, int selectedParameterIndex, bool reflectionIconsEnabled) {

			if ((member == null) || (member.IsEditorBrowsableNever))
				return null;

			// Get extension method info
			bool isExtensionMethod = member.IsExtension;
			bool isExtensionMethodApplied = (isExtensionMethod) && (type != null) && (!type.IsExtension);

			// Get the declaring type
			IDomType declaringType = null;
			if (member.DeclaringType != null)
				declaringType = this.ConstructGenericType(member.DeclaringType.Resolve(this), type);

			// Try to construct the member if it is a generic method defintion
			if ((member.IsGenericMethodDefinition) && (context != null) && (context.TargetItem != null))  // TODO: && (context.TargetItem.ResolvedInfo == member))
				member = this.ConstructGenericMethod(member, (isExtensionMethodApplied ? type : null), context);
			
			// Get the parameter list
			StringBuilder parameterText = new StringBuilder();
			IDomParameter[] parameters = member.Parameters;
			if (parameters != null) {
				if (isExtensionMethodApplied)
					selectedParameterIndex++;
				if (selectedParameterIndex >= parameters.Length)
					selectedParameterIndex = -1;

				for (int parameterIndex = 0; parameterIndex < parameters.Length; parameterIndex++) {
					IDomParameter parameter = parameters[parameterIndex];

					IDomType parameterType = (parameter.ParameterType != null ? this.ConstructAndResolve(parameter.ParameterType, declaringType) : null);
					string parameterTypeText;
					if (parameterType != null)
						parameterTypeText = DotNetProjectResolver.GetMarkupForType(language, parameterType, true, (selectedParameterIndex == parameterIndex));
					else if ((parameter.ParameterType != null) && (parameter.ParameterType.RawFullName != null))
						parameterTypeText = DotNetProjectResolver.GetMarkupForType(language, parameter.ParameterType.RawFullName, true, (selectedParameterIndex == parameterIndex));
					else
						parameterTypeText = "?";

					string parameterNameText;
					if (selectedParameterIndex == parameterIndex)
						parameterNameText = String.Format("<b>{0}</b>", IntelliPrompt.EscapeMarkupText(parameter.Name));
					else
						parameterNameText = IntelliPrompt.EscapeMarkupText(parameter.Name);

					if (parameterText.Length > 0)
						parameterText.Append(", ");
					
					// Add data for extension methods
					if ((isExtensionMethod) && (parameterIndex == 0)) {
						if (isExtensionMethodApplied)
							continue;
						else if (language == DotNetLanguage.CSharp)
							parameterText.Append(DotNetProjectResolver.GetMarkupForKeyword("this "));
					}
					
					// Add ref and out modifiers
					if (parameter.IsByReference)
						parameterText.Append(DotNetProjectResolver.GetMarkupForKeyword(language == DotNetLanguage.VB ? "ByRef " : "ref "));
					if (parameter.IsOutput) {
						if (language == DotNetLanguage.VB)
							parameterText.Append("&lt;" + DotNetProjectResolver.GetMarkupForKeyword("Out") + "&gt; ");
						parameterText.Append(DotNetProjectResolver.GetMarkupForKeyword(language == DotNetLanguage.VB ? "ByRef " : "out "));
					}

					if (language == DotNetLanguage.VB) {
						if ((!parameter.IsByReference) && (!parameter.IsOutput)) {
							if (selectedParameterIndex == parameterIndex)
								parameterText.Append("<b>ByVal </b>");
							else
								parameterText.Append(DotNetProjectResolver.GetMarkupForKeyword("ByVal "));
						}
						parameterText.Append(parameterNameText);
						if (selectedParameterIndex == parameterIndex)
							parameterText.Append("<b> As </b>");
						else
							parameterText.Append(DotNetProjectResolver.GetMarkupForKeyword(" As "));
						parameterText.Append(parameterTypeText);
					}
					else {
						parameterText.Append(parameterTypeText);
						parameterText.Append(" ");
						parameterText.Append(parameterNameText);
					}
				}
			}
			else
				selectedParameterIndex = -1;

			string quickInfo;
			bool parameterSpecified = (selectedParameterIndex >= (isExtensionMethodApplied ? 1 : 0));
			
			// Get the declaring type name
			string declaringTypeName = null;
			if (member.DeclaringType != null) {
				if (declaringType != null)
					declaringTypeName = DotNetProjectResolver.GetMarkupForType(language, declaringType, true, !parameterSpecified);
				else if ((member.DeclaringType != null) && (member.DeclaringType.RawFullName != null))
					declaringTypeName = DotNetProjectResolver.GetMarkupForType(language, member.DeclaringType.RawFullName, true, !parameterSpecified);
				else
					return null;
			}

			// Get the return type name
			string returnTypeName = null;
			if (member.ReturnType != null) {
				IDomType returnType = this.ConstructAndResolve(member.ReturnType, declaringType);
				if (returnType != null)
					returnTypeName = DotNetProjectResolver.GetMarkupForType(language, returnType, true, false);
				else if ((member.ReturnType != null) && (member.ReturnType.RawFullName != null))
					returnTypeName = DotNetProjectResolver.GetMarkupForType(language, member.ReturnType.RawFullName, true, false);
				else
					return null;

				if ((language == DotNetLanguage.VB) && (member.ReturnType.FullName == "System.Void"))
					returnTypeName = null;
			}

			// Get the resolved member type
			DomMemberType resolvedMemberType = member.MemberType;
			if ((resolvedMemberType == DomMemberType.Property) && (parameterText.Length > 0))
				resolvedMemberType = DomMemberType.Method;

			switch (resolvedMemberType) {
				case DomMemberType.Constructor:
				case DomMemberType.Method:
					// Add method generic type arguments
					string genericTypeArgumentsQuickInfo = String.Empty;
					if (member.IsGenericMethod) {
						ICollection genericTypeArguments = member.GenericTypeArguments;
						if (genericTypeArguments != null) {
							int count = 0;
							if (language == DotNetLanguage.VB)
								genericTypeArgumentsQuickInfo += "(" + (selectedParameterIndex == -1 ? "Of " : DotNetProjectResolver.GetMarkupForKeyword("Of "));
							else
								genericTypeArgumentsQuickInfo += "&lt;";
							foreach (IDomTypeReference genericTypeArgument in genericTypeArguments) {
								if (count++ > 0)
									genericTypeArgumentsQuickInfo += ",";
								genericTypeArgumentsQuickInfo += DotNetProjectResolver.GetMarkupForType(language, genericTypeArgument, true, (selectedParameterIndex == -1));
							}
							if (language == DotNetLanguage.VB)
								genericTypeArgumentsQuickInfo += ")";
							else
								genericTypeArgumentsQuickInfo += "&gt;";
						}
					}
					
					if ((language != DotNetLanguage.VB) && (member.MemberType == DomMemberType.Property))
						quickInfo = "[" + parameterText + "]";
					else
						quickInfo = "(" + parameterText + ")";

					switch (member.MemberType) {
						case DomMemberType.Constructor:
							if (language == DotNetLanguage.VB) {
								quickInfo = DotNetProjectResolver.GetMarkupForKeyword("Sub ") +
									(selectedParameterIndex == -1 ? "<b>" : String.Empty) + declaringTypeName + "." +
									declaringTypeName + (selectedParameterIndex == -1 ? "</b>" : String.Empty) + quickInfo;
							}
							else {
								quickInfo = (selectedParameterIndex == -1 ? "<b>" : String.Empty) + declaringTypeName + "." +
									declaringTypeName + (selectedParameterIndex == -1 ? "</b>" : String.Empty) + quickInfo;
							}
							break;
						case DomMemberType.Property:
							if (language == DotNetLanguage.VB) {
								quickInfo = 
									((member.Modifiers & Modifiers.ReadOnly) == Modifiers.ReadOnly ? 
										DotNetProjectResolver.GetMarkupForKeyword("ReadOnly ") : String.Empty) +
									((member.Modifiers & Modifiers.WriteOnly) == Modifiers.WriteOnly ? 
										DotNetProjectResolver.GetMarkupForKeyword("WriteOnly ") : String.Empty) +
									(selectedParameterIndex == -1 ? "<b>" : String.Empty) + declaringTypeName + 
									(selectedParameterIndex == -1 ? "</b>" : String.Empty) + quickInfo +
									DotNetProjectResolver.GetMarkupForKeyword(" As ") + returnTypeName;
							}
							else {
								quickInfo = returnTypeName + " " + (selectedParameterIndex == -1 ? "<b>" : String.Empty) + declaringTypeName + 
									(selectedParameterIndex == -1 ? "</b>" : String.Empty) + quickInfo;
							}
							break;
						default:
							if (language == DotNetLanguage.VB) {
								quickInfo = DotNetProjectResolver.GetMarkupForKeyword(returnTypeName != null ? "Function " : "Sub ") +
									(selectedParameterIndex == -1 ? "<b>" : String.Empty) + declaringTypeName + "." +
									IntelliPrompt.EscapeMarkupText(member.Name) + genericTypeArgumentsQuickInfo + (selectedParameterIndex == -1 ? "</b>" : String.Empty) + quickInfo +
									(returnTypeName != null ? DotNetProjectResolver.GetMarkupForKeyword(" As ") + returnTypeName : String.Empty);
							}
							else {
								quickInfo = returnTypeName + " " + (selectedParameterIndex == -1 ? "<b>" : String.Empty) + declaringTypeName + "." +
									IntelliPrompt.EscapeMarkupText(member.Name) + genericTypeArgumentsQuickInfo + (selectedParameterIndex == -1 ? "</b>" : String.Empty) + quickInfo;
							}
							break;
					}
			
					if ((!parameterSpecified) && (type != null)) {
						DomBindingFlags flags = (context != null ? context.AdditionalBindingFlags : DomBindingFlags.None);
						int overloadCount = 0;

						// Get the member overloads
						IDomMember[] overloads = this.GetMemberOverloads((context != null ? context.TypeDeclarationNode : null), type, member.Name, 
							DomBindingFlags.ExcludeIndexers | (member.IsStatic ? DomBindingFlags.Static : DomBindingFlags.Instance) | flags | DomBindingFlags.AllAccessTypes | DomBindingFlags.ExcludeEditorNeverBrowsable);
						if (overloads != null) {
							// Simply add to the overload count
							overloadCount += overloads.Length;
						}

						// Get the extension method overloads
						if ((context != null) && ((isExtensionMethod) || (!member.IsStatic))) {
							overloads = this.GetExtensionMethods(context.TypeDeclarationNode, context.ImportedNamespaces, type, member.Name, 
								DomBindingFlags.Instance | flags | DomBindingFlags.AllAccessTypes);
							if (overloads != null) {
								// If showing quick info for a member list and this is an applied extension method...
								if ((!reflectionIconsEnabled) && (isExtensionMethodApplied)) {
									// Only add overload count if generic method def flag matches
									foreach (IDomMember overload in overloads) {
										if (overload.IsGenericMethod == member.IsGenericMethod)
											overloadCount++;
									}
								}
								else {
									// Simply add to the overload count
									overloadCount += overloads.Length;
								}
							}
						}

						if (overloadCount > 0)
							quickInfo += DotNetProjectResolver.GetMarkupForOverloadCount(overloadCount);
					}
					break;
				case DomMemberType.Constant:
				case DomMemberType.Event:
				case DomMemberType.Field:
				case DomMemberType.Property:
					if (language == DotNetLanguage.VB) {
						quickInfo = 
							((((member.MemberType == DomMemberType.Field) || (member.MemberType == DomMemberType.Property)) && 
								((member.Modifiers & Modifiers.ReadOnly) == Modifiers.ReadOnly)) ? DotNetProjectResolver.GetMarkupForKeyword("ReadOnly ") : String.Empty) +
							((((member.MemberType == DomMemberType.Field) || (member.MemberType == DomMemberType.Property)) && 
								((member.Modifiers & Modifiers.WriteOnly) == Modifiers.WriteOnly)) ? DotNetProjectResolver.GetMarkupForKeyword("WriteOnly ") : String.Empty) +
							declaringTypeName + "<b>." + IntelliPrompt.EscapeMarkupText(member.Name) + "</b>" +
							DotNetProjectResolver.GetMarkupForKeyword(" As ") + returnTypeName;
					}
					else
						quickInfo = returnTypeName + " " + declaringTypeName + "<b>." + IntelliPrompt.EscapeMarkupText(member.Name) + "</b>";
					break;
				default:
					quickInfo = declaringTypeName + "<b>." + IntelliPrompt.EscapeMarkupText(member.Name) + "</b>";
					break;
			}

			// If an extension, add a header
			if (isExtensionMethod)
				quickInfo = "<i style=\"color: gray;\">(extension)</i> " + quickInfo;
			
			// If there is an icon that should display...
			if ((reflectionIconsEnabled) && (member.ImageIndex != -1))
				quickInfo = @"<img src=""resource:" + member.ImageIndex + @""" align=""absbottom""/> " + quickInfo;

			// Get the summary
			DomDocumentationProvider documentationProvider = member.DocumentationProvider;
			if (documentationProvider != null) {
				string summary = documentationProvider.GetSummary(this);
				if ((summary == null) && (member.ReturnType != null) && (member.ReturnType.Name == TypeReference.AnonymousTypeName))
					summary = "An anonymous type.";
				if ((summary == null) || (summary.Length == 0)) {
					switch (member.MemberType) {
						case DomMemberType.Constant:
						case DomMemberType.Field:
							if ((context != null) && (member.ReturnType != null)) {
								// Since there was no summary for the field, try to get the return type summary
								IDomType returnType = this.GetType(context, member.ReturnType.FullName, DomBindingFlags.Default);
								if (returnType != null) {
									DomDocumentationProvider returnTypeDocumentationProvider = returnType.DocumentationProvider;
									summary = returnTypeDocumentationProvider.GetSummary(this);
								}
							}
							break;
					}
				}
				if ((summary != null) && (summary.Length > 0))
					quickInfo += "<br/>" + DotNetProjectResolver.GetMarkupForComment(summary);

				// Get the current parameter documentation, if any
				if (selectedParameterIndex > (isExtensionMethodApplied ? 0 : -1)) {
					string parameterDocumentation = documentationProvider.GetParameter(this, parameters[selectedParameterIndex].Name);
					if ((parameterDocumentation != null) && (parameterDocumentation.Length > 0))
						quickInfo += "<br/><b>" + parameters[selectedParameterIndex].Name + ": </b>" + DotNetProjectResolver.GetMarkupForComment(parameterDocumentation);
				}

				// Add exceptions
				string[] exceptionTypeNames = documentationProvider.ExceptionTypeNames;
				if (exceptionTypeNames != null) {
					quickInfo += "<br/>Exceptions:<span style=\"font-size: 12pt;\"> </span>";
					foreach (string typeName in exceptionTypeNames)
						quickInfo += "<br/>&nbsp;&nbsp;&nbsp;" + DotNetProjectResolver.GetMarkupForType(language, typeName, false, false);
				}
			}

			return quickInfo;
		}
		
		/// <summary>
		/// Gets the formatted text to display in quick info for the specified namespace.
		/// </summary>
		/// <param name="language">The <see cref="DotNetLanguage"/> requesting the quick info.</param>
		/// <param name="namespaceName">The namespace to examine.</param>
		/// <param name="reflectionIconsEnabled">Whether reflection icons are enabled.</param>
		/// <returns>The formatted text to display in quick info for the specified namespace.</returns>
		internal string GetQuickInfoForNamespace(DotNetLanguage language, string namespaceName, bool reflectionIconsEnabled) {
			string keyword = "Namespace";
			if (language != DotNetLanguage.VB)
				keyword = keyword.ToLower();

			return (reflectionIconsEnabled ? @"<img src=""resource:Namespace"" align=""absbottom""/> " : String.Empty) +
				DotNetProjectResolver.GetMarkupForKeyword(keyword) + " <b>" + IntelliPrompt.EscapeMarkupText(namespaceName) + "</b>";
		}
		
		/// <summary>
		/// Gets the formatted text to display in quick info for the specified <see cref="IDomType"/> that is from a parameter reference.
		/// </summary>
		/// <param name="language">The <see cref="DotNetLanguage"/> requesting the quick info.</param>
		/// <param name="context">The <see cref="DotNetContext"/>, if known.</param>
		/// <param name="typeReference">The <see cref="IDomTypeReference"/> of the parameter.</param>
		/// <param name="parameterName">The variable name.</param>
		/// <param name="reflectionIconsEnabled">Whether reflection icons are enabled.</param>
		/// <returns>The formatted text to display in quick info for the specified <see cref="IDomType"/> that is from a parameter reference.</returns>
		internal string GetQuickInfoForParameter(DotNetLanguage language, DotNetContext context, IDomTypeReference typeReference, string parameterName, bool reflectionIconsEnabled) {
			if ((typeReference == null) || (parameterName == null))
				return null;

			// Try and get the type
			IDomType type = typeReference as IDomType;
			if ((type == null) && (context != null)) {
				type = typeReference.Resolve(this);
				if (type != null)
					typeReference = type;
			}

			// Get the general info
			string quickInfo = "<i style=\"color: gray;\">(parameter)</i> " + 
				(reflectionIconsEnabled ? @"<img src=""resource:PublicField"" align=""absbottom""/> " : String.Empty) + 
				DotNetProjectResolver.GetMarkupForType(language, typeReference, false, true) + " " +
				IntelliPrompt.EscapeMarkupText(parameterName);
			
			bool parameterDocumentationFound = false;
			IDomMember member = (context != null ? context.MemberDeclarationNode : null);
			if (member != null) {
				// Try and get the parameter documentation from the member
				DomDocumentationProvider documentationProvider = member.DocumentationProvider;
				if (documentationProvider != null) {
					string parameterDocumentation = documentationProvider.GetParameter(this, parameterName);
					if ((parameterDocumentation != null) && (parameterDocumentation.Length > 0)) {
						parameterDocumentationFound = true;
						quickInfo += "<br/>" + DotNetProjectResolver.GetMarkupForComment(parameterDocumentation);
					}
				}
			}

			if ((!parameterDocumentationFound) && (type != null)) {
				// Get the summary of the type, if known
				DomDocumentationProvider documentationProvider = type.DocumentationProvider;
				string summary = documentationProvider.GetSummary(this);
				if ((summary != null) && (summary.Length > 0))
					quickInfo += "<br/>" + DotNetProjectResolver.GetMarkupForComment(summary);
			}

			return quickInfo;
		}
		
		/// <summary>
		/// Gets the formatted text to display in quick info for the specified <see cref="IDomType"/>.
		/// </summary>
		/// <param name="language">The <see cref="DotNetLanguage"/> requesting the quick info.</param>
		/// <param name="type">The <see cref="IDomType"/> to examine.</param>
		/// <param name="reflectionIconsEnabled">Whether reflection icons are enabled.</param>
		/// <returns>The formatted text to display in quick info for the specified <see cref="IDomType"/>.</returns>
		internal string GetQuickInfoForType(DotNetLanguage language, IDomType type, bool reflectionIconsEnabled) {
			if (type == null) 
				return null;

			string quickInfo;
			switch (type.Type) {
				case DomTypeType.Class:
					quickInfo = "Class";
					break;
				case DomTypeType.Delegate:
					quickInfo = "Delegate";
					break;
				case DomTypeType.Enumeration:
					quickInfo = "Enum";
					break;
				case DomTypeType.Interface:
					quickInfo = "Interface";
					break;
				case DomTypeType.StandardModule:
					quickInfo = "Module";
					break;
				case DomTypeType.Structure:
					quickInfo = "Struct" + (language == DotNetLanguage.VB ? "ure" : String.Empty);
					break;
				default:
					return null;
			}
			if (language != DotNetLanguage.VB)
				quickInfo = quickInfo.ToLower();
			quickInfo = DotNetProjectResolver.GetMarkupForKeyword(quickInfo) + " " + 
				DotNetProjectResolver.GetMarkupForType(language, type, false, true);

			// If there is an icon that should display...
			if ((reflectionIconsEnabled) && (type.ImageIndex != -1))
				quickInfo = @"<img src=""resource:" + type.ImageIndex + @""" align=""absbottom""/> " + quickInfo;

			// Get the summary
			DomDocumentationProvider documentationProvider = type.DocumentationProvider;
			string summary = documentationProvider.GetSummary(this);
			if ((summary != null) && (summary.Length > 0))
				quickInfo += "<br/>" + DotNetProjectResolver.GetMarkupForComment(summary);
		
			if (type.IsGenericTypeDefinition) {
				// NOTE: Add generic type arguments once the constructed type is known
				/*
				ICollection genericTypeArguments = type.GenericTypeArguments;
				if ((genericTypeArguments != null) && (genericTypeArguments.Count > 0)) {
					quickInfo += "<br/>Generic Type Arguments:<span style=\"font-size: 12pt;\"> </span>";
					foreach (IDomTypeReference typeReference in genericTypeArguments) {
						quickInfo += "<br/>&nbsp;&nbsp;&nbsp;<b>" + typeReference.Name + "</b>: ";
					}
				}
				*/
			}

			return quickInfo;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// MEMBER LIST PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Adds <see cref="IntelliPromptMemberListItem"/> items that indicate the child namespaces of the specified namespace to a <see cref="Hashtable"/>.
		/// </summary>
		/// <param name="memberListItemHashtable">A <see cref="Hashtable"/> of <see cref="IntelliPromptMemberListItem"/> objects, keyed by name.</param>
		/// <param name="namespaceName">The namespace name for which to search.</param>
		internal void AddMemberListItemsForChildNamespaces(Hashtable memberListItemHashtable, string namespaceName) {
			// Get the data
			ICollection childNamespaceNames = this.GetChildNamespaceNames(namespaceName);

			// Add the items
			foreach (string childNamespaceName in childNamespaceNames)
				memberListItemHashtable[childNamespaceName] = new IntelliPromptMemberListItem(childNamespaceName, 
					(int)ActiproSoftware.Products.SyntaxEditor.IconResource.Namespace, 
					(namespaceName != null ? (object)(namespaceName + "." + childNamespaceName) : childNamespaceName));
		}

		/// <summary>
		/// Adds <see cref="IntelliPromptMemberListItem"/> items that indicate code snippets to a <see cref="Hashtable"/>.
		/// </summary>
		/// <param name="memberListItemHashtable">A <see cref="Hashtable"/> of <see cref="IntelliPromptMemberListItem"/> objects, keyed by name.</param>
		internal void AddMemberListItemsForCodeSnippets(Hashtable memberListItemHashtable) {
			foreach (CodeSnippetFolder childFolder in codeSnippetFolders)
				this.AddMemberListItemsForCodeSnippets(memberListItemHashtable, childFolder);
		}

		/// <summary>
		/// Recursively adds <see cref="IntelliPromptMemberListItem"/> items that indicate code snippets to a <see cref="Hashtable"/>.
		/// </summary>
		/// <param name="memberListItemHashtable">A <see cref="Hashtable"/> of <see cref="IntelliPromptMemberListItem"/> objects, keyed by name.</param>
		/// <param name="folder">The <see cref="CodeSnippetFolder"/> to examine.</param>
		private void AddMemberListItemsForCodeSnippets(Hashtable memberListItemHashtable, CodeSnippetFolder folder) {
			// Add an item for each snippet
			foreach (CodeSnippet codeSnippet in folder.CodeSnippets) {
				if ((codeSnippet.Shortcut != null) && (codeSnippet.Shortcut.Trim().Length > 0))
					memberListItemHashtable[codeSnippet.Shortcut.Trim()] = new IntelliPromptMemberListItem(codeSnippet.Shortcut.Trim(), 
						(int)ActiproSoftware.Products.SyntaxEditor.IconResource.CodeSnippet, codeSnippet);
			}
			
			// Recurse
			foreach (CodeSnippetFolder childFolder in folder.ChildFolders)
				this.AddMemberListItemsForCodeSnippets(memberListItemHashtable, childFolder);
		}
		
		/// <summary>
		/// Adds <see cref="IntelliPromptMemberListItem"/> items that indicate the members of declaring types of the specified <see cref="IDomType"/> to a <see cref="Hashtable"/>.
		/// </summary>
		/// <param name="memberListItemHashtable">A <see cref="Hashtable"/> of <see cref="IntelliPromptMemberListItem"/> objects, keyed by name.</param>
		/// <param name="contextType">The <see cref="IDomType"/> that provides the context of the lookup.</param>
		/// <param name="type">The <see cref="IDomType"/> for which to search.</param>
		/// <param name="flags">The <see cref="DomBindingFlags"/> to match.</param>
		internal void AddMemberListItemsForDeclaringTypeMembers(Hashtable memberListItemHashtable, IDomType contextType, IDomType type, DomBindingFlags flags) {
			// If there is a declaring type for the specified type, recurse up 
			if (type.DeclaringType is IDomType)
				this.AddMemberListItemsForDeclaringTypeMembers(memberListItemHashtable, contextType, (IDomType)type.DeclaringType, flags);

			// Add members
			this.AddMemberListItemsForMembers(memberListItemHashtable, contextType, type, flags);
		}
		
		/// <summary>
		/// Adds <see cref="IntelliPromptMemberListItem"/> items that indicate documentation comments to a <see cref="Hashtable"/>.
		/// </summary>
		/// <param name="memberListItemHashtable">A <see cref="Hashtable"/> of <see cref="IntelliPromptMemberListItem"/> objects, keyed by name.</param>
		/// <param name="context">The <see cref="DotNetContext"/> to examine.</param>
		/// <param name="addStartBrace">Whether to add a start brace.</param>
		internal void AddMemberListItemsForDocumentationComments(Hashtable memberListItemHashtable, DotNetContext context, bool addStartBrace) {
			// Set flags
			int tagIndex = 0;
			bool isInRoot = true;
			bool isInTagContainer = true;
			bool isInList = false;
			bool isInItem = false;
			if (context.TargetItem != null) {
				switch (context.TargetItem.Text) {
					case "description":
					case "example":
					case "exception":
					case "para":
					case "param":
					case "permission":
					case "remarks":
					case "returns":
					case "summary":
					case "term":
					case "typeparam":
					case "value":
						isInRoot = false;
						break;
					case "item":
					case "listheader":
						isInRoot = false;
						isInTagContainer = false;
						isInItem = true;
						break;
					case "list":
						isInRoot = false;
						isInTagContainer = false;
						isInList = true;
						break;
					default:
						isInRoot = false;
						isInTagContainer = false;
						break;
				}
			}

			// Get the member list items
			int imageIndex = (int)ActiproSoftware.Products.SyntaxEditor.IconResource.Keyword;
			if ((!isInRoot) && (isInTagContainer))
				memberListItemHashtable.Add(tagIndex++, new IntelliPromptMemberListItem("c", imageIndex, "Indicates that text within the tag should be marked as code.  Use &lt;code&gt; to indicate multiple lines as code."));
			if ((!isInRoot) && (isInTagContainer))
				memberListItemHashtable.Add(tagIndex++, new IntelliPromptMemberListItem("code", imageIndex, "Indicates multiple lines as code. Use &lt;c&gt; to indicate that text within a description should be marked as code."));
			if ((isInRoot) && (isInTagContainer)) {
				// Determine if a example tag is appropriate
				if (context.DocumentationComment.IndexOf("<remarks>") == -1)
					memberListItemHashtable.Add(tagIndex++, new IntelliPromptMemberListItem("example", imageIndex, "Specifies an example of how to use a method or other library member."));
			}
			if ((isInRoot) && (isInTagContainer))
				memberListItemHashtable.Add(tagIndex++, new IntelliPromptMemberListItem("exception", imageIndex, "Specifies which exceptions a class can throw.", "exception cref=\"", "\""));
			if ((isInRoot) && (isInTagContainer))
				memberListItemHashtable.Add(tagIndex++, new IntelliPromptMemberListItem("include", imageIndex, "Refers to comments in another file that describe the types and members in your source code.", "include file='", "' path='[@name=\"\"]'/>"));
			if ((!isInRoot) && (isInTagContainer))
				memberListItemHashtable.Add(tagIndex++, new IntelliPromptMemberListItem("list", imageIndex, "Provides a container for list items.", "list type=\"", "\""));
			if (isInList)
				memberListItemHashtable.Add(tagIndex++, new IntelliPromptMemberListItem("listheader", imageIndex, "Defines the heading row of either a table or definition list."));
			if (isInList)
				memberListItemHashtable.Add(tagIndex++, new IntelliPromptMemberListItem("item", imageIndex, "Defines an item in a table or definition list."));
			if (isInItem)
				memberListItemHashtable.Add(tagIndex++, new IntelliPromptMemberListItem("term", imageIndex, "A term to define, which will be defined in text."));
			if (isInItem)
				memberListItemHashtable.Add(tagIndex++, new IntelliPromptMemberListItem("description", imageIndex, "Either an item in a bullet or numbered list or the definition of a term."));
			if ((!isInRoot) && (isInTagContainer))
				memberListItemHashtable.Add(tagIndex++, new IntelliPromptMemberListItem("para", imageIndex, "Provides a paragraph container."));
			if ((isInRoot) && (isInTagContainer) && (context.MemberDeclarationNode != null)) {
				// Determine if any parameters should show
				IDomParameter[] parameters = ((IDomMember)context.MemberDeclarationNode).Parameters;
				if (parameters != null) {
					foreach (IDomParameter parameter in parameters) {
						if (context.DocumentationComment.IndexOf(String.Format("<param name=\"{0}\"", parameter.Name)) == -1)
							memberListItemHashtable.Add(tagIndex++, new IntelliPromptMemberListItem(String.Format("param name=\"{0}\"", parameter.Name), imageIndex, 
								"Describes one of the parameters for the method.", String.Format("param name=\"{0}\"", parameter.Name), null));
					}
				}
			}
			if ((!isInRoot) && (isInTagContainer) && (context.MemberDeclarationNode != null)) {
				// Determine if any parameter references should show
				IDomParameter[] parameters = ((IDomMember)context.MemberDeclarationNode).Parameters;
				if (parameters != null) {
					foreach (IDomParameter parameter in parameters)
						memberListItemHashtable.Add(tagIndex++, new IntelliPromptMemberListItem(String.Format("paramref name=\"{0}\"", parameter.Name), imageIndex, 
							"Indicates that a word is a parameter.", String.Format("paramref name=\"{0}\"/>", parameter.Name), null));
				}
			}
			if ((isInRoot) && (isInTagContainer))
				memberListItemHashtable.Add(tagIndex++, new IntelliPromptMemberListItem("permission", imageIndex, "Documents the access of a member.", "permission cref=\"", "\""));
			if ((isInRoot) && (isInTagContainer)) {
				// Determine if a remarks tag is appropriate
				if (context.DocumentationComment.IndexOf("<remarks>") == -1)
					memberListItemHashtable.Add(tagIndex++, new IntelliPromptMemberListItem("remarks", imageIndex, "Specifies overview information about a class or other type."));
			}
			if ((isInRoot) && (isInTagContainer) && (context.MemberDeclarationNode != null)) {
				// Determine if a returns tag is appropriate
				if (
					(context.DocumentationComment.IndexOf("<returns>") == -1) &&
					(((IDomMember)context.MemberDeclarationNode).MemberType == DomMemberType.Method) &&
					(((IDomMember)context.MemberDeclarationNode).ReturnType != null) && 
					(!(((IDomMember)context.MemberDeclarationNode).ReturnType.RawFullName.ToLower().EndsWith("void")))
					)
					memberListItemHashtable.Add(tagIndex++, new IntelliPromptMemberListItem("returns", imageIndex, "Describes the return value for a method declaration."));
			}
			if ((!isInRoot) && (isInTagContainer))
				memberListItemHashtable.Add(tagIndex++, new IntelliPromptMemberListItem("see", imageIndex, "Specifies a link from within text.", "see cref=\"", "\"/>"));
			if ((isInRoot) && (isInTagContainer))
				memberListItemHashtable.Add(tagIndex++, new IntelliPromptMemberListItem("seealso", imageIndex, "Specifies the text that you might want to appear in a See Also section.", "seealso cref=\"", "\"/>"));
			if ((isInRoot) && (isInTagContainer)) {
				// Determine if a summary tag is appropriate
				if (context.DocumentationComment.IndexOf("<summary>") == -1)
					memberListItemHashtable.Add(tagIndex++, new IntelliPromptMemberListItem("summary", imageIndex, "Describes a member for a type."));
			}
			if ((isInRoot) && (isInTagContainer) && ((context.TypeDeclarationNode != null) || (context.MemberDeclarationNode != null))) {
				// Determine if any type parameters should show
				ICollection parameters = (context.MemberDeclarationNode != null ? ((IDomMember)context.MemberDeclarationNode).GenericTypeArguments : ((IDomType)context.TypeDeclarationNode).GenericTypeArguments);
				if (parameters != null) {
					foreach (IDomTypeReference parameter in parameters) {
						if (context.DocumentationComment.IndexOf(String.Format("<typeparam name=\"{0}\"", parameter.Name)) == -1)
							memberListItemHashtable.Add(tagIndex++, new IntelliPromptMemberListItem(String.Format("typeparam name=\"{0}\"", parameter.Name), imageIndex, 
								"Used for a generic type or method declaration to describe a type parameter.", String.Format("typeparam name=\"{0}\"", parameter.Name), null));
					}
				}
			}
			if ((!isInRoot) && (isInTagContainer) && ((context.TypeDeclarationNode != null) || (context.MemberDeclarationNode != null))) {
				// Determine if any type parameter references should show
				ICollection parameters = (context.MemberDeclarationNode != null ? ((IDomMember)context.MemberDeclarationNode).GenericTypeArguments : ((IDomType)context.TypeDeclarationNode).GenericTypeArguments);
				if (parameters != null) {
					foreach (IDomTypeReference parameter in parameters)
						memberListItemHashtable.Add(tagIndex++, new IntelliPromptMemberListItem(String.Format("typeparamref name=\"{0}\"", parameter.Name), imageIndex, 
							"Used for a generic type or method declaration to describe a type parameter reference.", String.Format("typeparamref name=\"{0}\"/>", parameter.Name), null));
				}
			}
			if ((isInRoot) && (isInTagContainer) && (context.MemberDeclarationNode != null)) {
				// Determine if a value tag is appropriate
				if (
					(context.DocumentationComment.IndexOf("<value>") == -1) &&
					(
						(((IDomMember)context.MemberDeclarationNode).MemberType == DomMemberType.Constant) ||
						(((IDomMember)context.MemberDeclarationNode).MemberType == DomMemberType.Field) ||
						(((IDomMember)context.MemberDeclarationNode).MemberType == DomMemberType.Property) 
					) &&
					(((IDomMember)context.MemberDeclarationNode).ReturnType != null) && 
					(!(((IDomMember)context.MemberDeclarationNode).ReturnType.RawFullName.ToLower().EndsWith("void")))
					)
					memberListItemHashtable.Add(tagIndex++, new IntelliPromptMemberListItem("value", imageIndex, "Describes the value for a property declaration."));
			}

			// Add in a start < if one is needed
			if (addStartBrace) {
				foreach (IntelliPromptMemberListItem item in memberListItemHashtable.Values)
					item.AutoCompletePreText = "<" + item.AutoCompletePreText;
			}
		}
		
		/// <summary>
		/// Adds <see cref="IntelliPromptMemberListItem"/> items that indicate the members of the specified <see cref="IDomType"/> to a <see cref="Hashtable"/>.
		/// </summary>
		/// <param name="memberListItemHashtable">A <see cref="Hashtable"/> of <see cref="IntelliPromptMemberListItem"/> objects, keyed by name.</param>
		/// <param name="context">The <see cref="DotNetContext"/> that provides the context of the lookup.</param>
		/// <param name="targetType">The <see cref="IDomType"/> to examine.</param>
		/// <param name="flags">The <see cref="DomBindingFlags"/> to match.</param>
		internal void AddMemberListItemsForExtensionMethods(Hashtable memberListItemHashtable, DotNetContext context, IDomType targetType, DomBindingFlags flags) {
			// Quit if not for instance
			if ((flags & DomBindingFlags.Instance) != DomBindingFlags.Instance)
				return;

			// Get the data
			IDomMember[] members = this.GetExtensionMethods(context.TypeDeclarationNode, context.ImportedNamespaces, targetType, null, flags);
			if (members == null)
				return;

			// Add the items
			foreach (IDomMember member in members) {
				if (!member.IsEditorBrowsableNever) {
					string memberName = member.Name + (member.IsGenericMethod ? "<>" : String.Empty);
					memberListItemHashtable[memberName] = new IntelliPromptMemberListItem(memberName, member.ImageIndex, null, member.Name, null, member);
				}
			}
		}

		/// <summary>
		/// Adds <see cref="IntelliPromptMemberListItem"/> items that indicate the nested types of the specified <see cref="IDomType"/> to a <see cref="Hashtable"/>.
		/// </summary>
		/// <param name="memberListItemHashtable">A <see cref="Hashtable"/> of <see cref="IntelliPromptMemberListItem"/> objects, keyed by name.</param>
		/// <param name="contextType">The <see cref="IDomType"/> that provides the context of the lookup.</param>
		/// <param name="type">The <see cref="IDomType"/> for which to search.</param>
		/// <param name="flags">The <see cref="DomBindingFlags"/> to match.</param>
		/// <param name="examineDeclaringTypes">Whether to examine declaring types too.</param>
		internal void AddMemberListItemsForNestedTypes(Hashtable memberListItemHashtable, IDomType contextType, IDomType type, DomBindingFlags flags, bool examineDeclaringTypes) {
			while (type != null) {
				// Get the nested types
				ICollection nestedTypes = this.GetNestedTypes(contextType, type.FullName, flags);

				// Write out the items
				foreach (IDomType nestedType in nestedTypes) {
					if (!nestedType.IsEditorBrowsableNever) {
						string typeName = DotNetProjectResolver.GetTypeFullNameForDisplay(nestedType.Name) + (nestedType.IsGenericTypeDefinition ? "<>" : String.Empty);
						memberListItemHashtable[typeName] = new IntelliPromptMemberListItem(typeName, nestedType.ImageIndex, null, DotNetProjectResolver.GetTypeFullNameForDisplay(nestedType.Name), null, nestedType);
					}
				}

				// Quit the loop if not allowed to examine declaring types
				if (!examineDeclaringTypes)
					break;

				// Go to declaring type (if type is a nested type)
				type = type.DeclaringType as IDomType;
			}
		}
		
		/// <summary>
		/// Adds <see cref="IntelliPromptMemberListItem"/> items that indicate the members of the specified <see cref="IDomType"/> to a <see cref="Hashtable"/>.
		/// </summary>
		/// <param name="memberListItemHashtable">A <see cref="Hashtable"/> of <see cref="IntelliPromptMemberListItem"/> objects, keyed by name.</param>
		/// <param name="contextType">The <see cref="IDomType"/> that provides the context of the lookup.</param>
		/// <param name="targetType">The <see cref="IDomType"/> to examine.</param>
		/// <param name="flags">The <see cref="DomBindingFlags"/> to match.</param>
		internal void AddMemberListItemsForMembers(Hashtable memberListItemHashtable, IDomType contextType, IDomType targetType, DomBindingFlags flags) {
			// Quit if for System.Void
			if ((targetType.Name == "Void") && (targetType.FullName == "System.Void"))
				return;

			// Get the data
			IDomMember[] members = this.GetMembers(contextType, targetType, null, flags | DomBindingFlags.ExcludeEditorNeverBrowsable);
			if (members == null)
				return;

			// Add the items
			foreach (IDomMember member in members) {
				// Don't add the item if it is a constructor or is an interface implementation
				if ((member.Name == null) || (member.Name.IndexOf(".") != -1))
					continue;
				
				// Don't add the item if it is not the appropriate scope
				if ((targetType.Type == DomTypeType.Enumeration) && (((member.MemberType != DomMemberType.Field) && (member.MemberType != DomMemberType.Constant)) == ((flags & DomBindingFlags.Static) == DomBindingFlags.Static)))
					continue;

				// Add the item
				string memberName = member.Name + (member.IsGenericMethod ? "<>" : String.Empty);
				memberListItemHashtable[memberName] = new IntelliPromptMemberListItem(memberName, member.ImageIndex, null, member.Name, null, member);
			}
		}

		/// <summary>
		/// Adds <see cref="IntelliPromptMemberListItem"/> items that indicate the native types to a <see cref="Hashtable"/>.
		/// </summary>
		/// <param name="language">The <see cref="DotNetLanguage"/> to use for examining language native types.</param>
		/// <param name="memberListItemHashtable">A <see cref="Hashtable"/> of <see cref="IntelliPromptMemberListItem"/> objects, keyed by name.</param>
		/// <param name="context">The <see cref="DotNetContext"/> in which to search.</param>
		internal void AddMemberListItemsForNativeTypes(DotNetLanguage language, Hashtable memberListItemHashtable, DotNetContext context) {
			string[] nativeTypes = null;
			switch (language) {
				case DotNetLanguage.CSharp:
					nativeTypes = new string[] {
						"bool", "byte", "char", "decimal", "double", "dynamic", "short", "int", "long", 
						"object", "sbyte", "float", "string", "ushort", "uint", "ulong"
						};
					break;
				case DotNetLanguage.VB:
					nativeTypes = new string[] {
						"Boolean", "Byte", "Char", "Decimal", "Double", "Short", "Integer", "Long", 
						"Object", "SByte", "Single", "String", "UShort", "UInteger", "ULong"
						};
					break;
			}

			if (nativeTypes != null) {
				foreach (string keyword in nativeTypes)
					memberListItemHashtable[keyword] = new IntelliPromptMemberListItem(keyword, (int)ActiproSoftware.Products.SyntaxEditor.IconResource.Keyword);
			}
		}
		
		/// <summary>
		/// Adds <see cref="IntelliPromptMemberListItem"/> items that indicate the types of the specified namespace to a <see cref="Hashtable"/>.
		/// </summary>
		/// <param name="memberListItemHashtable">A <see cref="Hashtable"/> of <see cref="IntelliPromptMemberListItem"/> objects, keyed by name.</param>
		/// <param name="contextType">The <see cref="IDomType"/> that provides the context of the lookup.</param>
		/// <param name="namespaceName">The namespace name for which to search.</param>
		/// <param name="flags">The <see cref="DomBindingFlags"/> to match.</param>
		/// <param name="includeStandardModuleMembers">Whether to add standard module members.</param>
		internal void AddMemberListItemsForTypes(Hashtable memberListItemHashtable, IDomType contextType, string namespaceName, DomBindingFlags flags, bool includeStandardModuleMembers) {
			// Get the data
			ICollection types = this.GetTypes(contextType, namespaceName, flags);

			// Write out the items
			foreach (IDomType type in types) {
				if (!type.IsEditorBrowsableNever) {
					string typeName = DotNetProjectResolver.GetTypeFullNameForDisplay(type.Name) + (type.IsGenericTypeDefinition ? "<>" : String.Empty);
					memberListItemHashtable[typeName] = new IntelliPromptMemberListItem(typeName, type.ImageIndex, null, DotNetProjectResolver.GetTypeFullNameForDisplay(type.Name), null, type);

					if ((includeStandardModuleMembers) && (type.Type == DomTypeType.StandardModule))
						this.AddMemberListItemsForMembers(memberListItemHashtable, contextType, type, flags);
				}
			}
		}
		
		/// <summary>
		/// Adds <see cref="IntelliPromptMemberListItem"/> items that indicate the in-scope variables to a <see cref="Hashtable"/>.
		/// </summary>
		/// <param name="memberListItemHashtable">A <see cref="Hashtable"/> of <see cref="IntelliPromptMemberListItem"/> objects, keyed by name.</param>
		/// <param name="context">The <see cref="DotNetContext"/> in which to search.</param>
		internal void AddMemberListItemsForVariables(Hashtable memberListItemHashtable, DotNetContext context) {
			// Get the variables and parameters
			ArrayList variableDeclarators = new ArrayList();
			DotNetContext.GetVariables(context, variableDeclarators);
			
			// Add each variable or parameter
			foreach (IAstNode declarator in variableDeclarators) {
				string name = DotNetContext.GetVariableName(declarator);
				if (declarator is VariableDeclarator) {
					VariableDeclarator variableDeclarator = (VariableDeclarator)declarator;
					memberListItemHashtable[name] = new IntelliPromptMemberListItem(name, 
						(int)(variableDeclarator.IsConstant ? ActiproSoftware.Products.SyntaxEditor.IconResource.PublicConstant : ActiproSoftware.Products.SyntaxEditor.IconResource.PublicField), declarator);
				}
				else if (declarator is ParameterDeclaration)
					memberListItemHashtable[name] = new IntelliPromptMemberListItem(name, 
						(int)ActiproSoftware.Products.SyntaxEditor.IconResource.PublicField, declarator);
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Examines the specified <see cref="IDomTypeReference"/> and returns a table of all the generic parameters that were found, 
		/// optionally with a certain name.
		/// </summary>
		/// <param name="typeReference">The <see cref="IDomTypeReference"/> to examine.</param>
		/// <param name="name">The optional name for which to search.  A <see langword="null"/> returns all located generic parameters.</param>
		/// <returns>
		/// The table of generic parameters that were found.
		/// </returns>
		private static Hashtable CollectGenericParameters(IDomTypeReference typeReference, string name) {
			Hashtable results = new Hashtable();
			DotNetProjectResolver.CollectGenericParameters(results, typeReference, name, null);
			return results;
		}
		
		/// <summary>
		/// Examines the specified <see cref="IDomTypeReference"/> and updates a table of all the generic parameters that were found, 
		/// optionally with a certain name.
		/// </summary>
		/// <param name="results">The table to update.</param>
		/// <param name="typeReference">The <see cref="IDomTypeReference"/> to examine.</param>
		/// <param name="name">The optional name for which to search.  A <see langword="null"/> returns all located generic parameters.</param>
		/// <param name="typeReferenceStack">A <see cref="Stack"/> of type references traversed to prevent any possible infinite recursion.</param>
		private static void CollectGenericParameters(Hashtable results, IDomTypeReference typeReference, string name, Stack typeReferenceStack) {
			if (typeReference == null)
				return;

			// If the type reference is a generic parameter...
			if (typeReference.IsGenericParameter) {
				// If looking for any, or if its name (without array/pointer) matches the desired name, add to the result
				string genericParameterName = DotNetProjectResolver.GetTypeName(DotNetProjectResolver.GetTypeNameWithoutArrayPointerSpec(typeReference.FullName));
				if ((name == null) || (genericParameterName == name)) {
					if (!results.ContainsKey(genericParameterName))
						results[genericParameterName] = null;
				}
				return;
			}

			// If the type reference is a generic type...
			if (typeReference.IsGenericType) {
				// If there are generic type parameters that we can recurse into...
				ICollection genericParameters = typeReference.GenericTypeArguments;
				if (genericParameters != null) {
					// Loop through the generic parameters
					foreach (IDomTypeReference genericParameter in genericParameters) {
						if (typeReferenceStack == null) {
							// Create a new stack
							typeReferenceStack = new Stack();
						}
						else {
							// Check for recursion
							if (typeReferenceStack.Contains(typeReferenceStack))
								return;
						}

						// Add to the stack
						typeReferenceStack.Push(typeReference);

						// Recurse into each generic parameter
						DotNetProjectResolver.CollectGenericParameters(results, genericParameter, name, typeReferenceStack);

						// Quit if we are looking for a specific name and it has already been located
						if ((name != null) && (results.Count > 0))
							return;

						// Pop the stack entry
						typeReferenceStack.Pop();
					}
				}
			}

			return;
		}

		/// <summary>
		/// Constructs and resolves a type reference from itself.
		/// </summary>
		/// <param name="typeReference">The <see cref="IDomTypeReference"/> to examine.</param>
		/// <returns>
		/// The resolved <see cref="IDomType"/>.
		/// </returns>
		internal IDomType ConstructAndResolveFromSelf(IDomTypeReference typeReference) {
			if (typeReference == null)
				return null;

			IDomType type = typeReference.Resolve(this);
			if ((type != null) && (type.IsGenericTypeDefinition)) {
				ICollection constructedGenericTypeArguments = typeReference.GenericTypeArguments;
				ICollection declaredGenericTypeArguments = type.GenericTypeArguments;
				if ((constructedGenericTypeArguments != null) && (declaredGenericTypeArguments != null) && 
					(constructedGenericTypeArguments.Count == declaredGenericTypeArguments.Count)) {
					type = new ConstructedGenericType(type, constructedGenericTypeArguments);
				}
			}
			return type;
		}

		/// <summary>
		/// Constructs a member return generic type reference if one is passed and resolves the type reference.
		/// </summary>
		/// <param name="context">The <see cref="DotNetContext"/> to examine.</param>
		/// <param name="itemIndex">The index of the item specifying a member.</param>
		/// <param name="contextType">A <see cref="IDomType"/> that provides contextual information and is already constructed.</param>
		/// <returns>
		/// The resolved <see cref="IDomType"/>.
		/// </returns>
		internal IDomType ConstructAndResolveMemberReturnType(DotNetContext context, int itemIndex, IDomType contextType) {
			DotNetContextItem item = context.Items[itemIndex];

			// If the member is a generic method definition...
			IDomMember member = item.ResolvedInfo as IDomMember;
			if (member != null) {
				// If there is a return type...
				if (member.ReturnType != null) {
					// Collect the generic parameters that are in the return value
					Hashtable returnValueGenericParameters = DotNetProjectResolver.CollectGenericParameters(member.ReturnType, null);

					// If there is at least one generic parameter in the return type...
					if (returnValueGenericParameters.Count > 0) {
						// Find the index of a matching generic type argument, if one is available
						int index = int.MaxValue;
						string returnTypeParameterName = DotNetProjectResolver.GetTypeName(DotNetProjectResolver.GetTypeNameWithoutArrayPointerSpec(member.ReturnType.FullName));
						if ((member.IsGenericMethod) && (member.GenericTypeArguments != null)) {
							index = 0;
							foreach (IDomTypeReference typeReference in member.GenericTypeArguments) {
								if (DotNetProjectResolver.GetTypeName(DotNetProjectResolver.GetTypeNameWithoutArrayPointerSpec(typeReference.FullName)) == returnTypeParameterName)
									break;
								index++;
							}
						}

						// If a type argument can be used...
						if ((item.GenericTypeArguments != null) && (index < item.GenericTypeArguments.Length)) {
							// Use a supplied generic type argument
							return this.ConstructAndResolve(item.GenericTypeArguments[index], contextType);
						}
						else {
							// Get the generic parameter names
							string[] genericParameterNames = new string[returnValueGenericParameters.Count];
							returnValueGenericParameters.Keys.CopyTo(genericParameterNames, 0);

							// Check the declaring type to see if we should try using any of its generic parameters
							if (itemIndex > 0) {
								ConstructedGenericType declaringType = this.ConstructAndResolveMemberReturnType(context, itemIndex - 1, contextType) as ConstructedGenericType;
								if ((declaringType != null) && (declaringType.GenericTypeArguments != null) && (declaringType.GenericDefinitionType.GenericTypeArguments != null)) {
									IDomTypeReference[] unresolvedGenericTypeArguments = new IDomTypeReference[declaringType.GenericDefinitionType.GenericTypeArguments.Count];
									IDomTypeReference[] resolvedGenericTypeArguments = new IDomTypeReference[declaringType.GenericTypeArguments.Count];
									declaringType.GenericDefinitionType.GenericTypeArguments.CopyTo(unresolvedGenericTypeArguments, 0);
									declaringType.GenericTypeArguments.CopyTo(resolvedGenericTypeArguments, 0);

									foreach (string genericParameterName in genericParameterNames) {
										// If the generic parameter name has not yet been resolved...
										if (returnValueGenericParameters[genericParameterName] == null) {
											for (index = 0; index < unresolvedGenericTypeArguments.Length; index++) {
												// If there is a match on the unresolve type argument
												if ((unresolvedGenericTypeArguments[index].Name == genericParameterName) && (index < resolvedGenericTypeArguments.Length)) {
													// Use the type
													returnValueGenericParameters[genericParameterName] = this.ConstructAndResolve(resolvedGenericTypeArguments[index], contextType);
													break;
												}
											}
										}
									}
								}
							}

							// No type arguments have been explicitly passed to the method... need to determine the return type based on parameter arguments

							// Get the declared parameters
							IDomParameter[] parameters = member.Parameters;
							if ((parameters != null) && (parameters.Length > 0)) {
								// Loop through each return value generic parameter
								bool allResolved = true;
								foreach (string genericParameterName in genericParameterNames) {
									// Scan parameters to look for one that includes the return type
									for (index = 0; index < parameters.Length; index++) {
										IDomParameter parameter = parameters[index];

										// If the parameter includes the generic parameter somewhere within it...
										if (DotNetProjectResolver.CollectGenericParameters(parameter.ParameterType, genericParameterName).Count > 0) {
											IDomType constructedGenericParameter = null;

											// If the member is an extension method...
											if (member.IsExtension) {
												// Get the type of the previous item
												IDomType declaringType = context.Items[itemIndex - 1].ResolvedInfo as IDomType;
												if (declaringType == null)
													declaringType = this.ConstructAndResolveContextItemMemberReturnType(context, itemIndex - 1);
												if (declaringType != null) {
													// Try and resolve the declaring type into a constructed generic parameter type reference
													constructedGenericParameter = this.GetConstructedGenericParameter(parameters[0].ParameterType, declaringType, genericParameterName, null, contextType);
												}
											}

											// If there are arguments that can be examined...
											if ((constructedGenericParameter == null) && ((item.ArgumentsText != null) || (item.UnresolvedArguments != null))) {
												// Ensure that all the parameter arguments have been resolved
												context.ResolveArguments(item, contextType);

												// If there are now resolved arguments...
												if (item.ResolvedArguments != null) {
													// Determine if the extension method is applied so we can offset arguments by 1
													bool isExtensionMethodApplied = (member.IsExtension) && (item.ResolvedArguments.Length == parameters.Length - 1);
													int argumentIndex = Math.Max(0, index - (isExtensionMethodApplied ? 1 : 0));

													// If a resolved parameter argument can be used to resolve the generic parameter...
													if (argumentIndex < item.ResolvedArguments.Length) {
														// Try and resolve the argument expression into a constructed generic parameter type reference
														IDomType argumentType = this.ConstructAndResolve(item.ResolvedArguments[argumentIndex], contextType);
														if (argumentType != null)
															constructedGenericParameter = this.GetConstructedGenericParameter(parameter.ParameterType, argumentType, genericParameterName, null, contextType);
													}
												}
											}

											// If a constructed generic parameter was found and resolved to an IDomType...
											if (constructedGenericParameter != null)
												returnValueGenericParameters[genericParameterName] = constructedGenericParameter;
											else if (returnValueGenericParameters[genericParameterName] == null) {
												// Flag that not all the generic parameters were resolved, and quit
												allResolved = false;
												break;
											}
										}
									}

									// Quit the loop if not everything could be resolved
									if (!allResolved)
										break;
								}

								// If all the generic parameters were resolved...
								if (allResolved) {
									// If the return type is a generic parameter itself...
									if ((genericParameterNames.Length == 1) && (genericParameterNames[0] == returnTypeParameterName)) {
										IDomType returnValueGenericParameter = (IDomType)returnValueGenericParameters[returnTypeParameterName];

										// If the return type is an array...
										if (member.ReturnType.ArrayRanks != null) {
											// Create a new array reference
											TypeReference returnTypeReference = new TypeReference(returnValueGenericParameter.FullName, TextRange.Deleted);
											returnTypeReference.ArrayRanks = member.ReturnType.ArrayRanks;
											IDomType type = this.ConstructAndResolve(returnTypeReference, contextType);
											return type;
										}
										else {
											// The simplest case: the return type is a generic parameter with no array ranks
											return returnValueGenericParameter;
										}
									}
									else if (member.ReturnType.IsGenericType) {
										// The return type is a generic type, so construct it based on the table of resolved parameters
										IDomType type = this.ConstructAndResolveFromTable(member.ReturnType, returnValueGenericParameters, contextType);
										return type;
									}

								}
							}
						}
					}
				}

				return this.ConstructAndResolve(member.ReturnType, this.ConstructGenericType(member.DeclaringType.Resolve(this), contextType));
			}
			else
				return item.ResolvedInfo as IDomType;
		}

		/// <summary>
		/// Constructs a generic member return type if one is passed and resolves the type reference.
		/// </summary>
		/// <param name="context">The <see cref="DotNetContext"/> to examine.</param>
		/// <param name="itemIndex">The index of the item.</param>
		/// <returns>
		/// The resolved <see cref="IDomType"/>.
		/// </returns>
		internal IDomType ConstructAndResolveContextItemMemberReturnType(DotNetContext context, int itemIndex) {
			IDomMember member = context.Items[itemIndex].ResolvedInfo as IDomMember;
			if (member == null)
				return null;			

			if (itemIndex > 0) {
				IDomType type = context.Items[itemIndex - 1].ResolvedInfo as IDomType;
				if (type == null)
					type = context.ProjectResolver.ConstructAndResolveContextItemMemberReturnType(context, itemIndex - 1);
				if (type != null) {
					type = this.ConstructAndResolveMemberReturnType(context, itemIndex, type);
					if (type != null)
						return type;
				}
			}

			return (member.ReturnType != null ? member.ReturnType.Resolve(this) : null);
		}

		/// <summary>
		/// Constructs a generic type reference if one is passed and resolves the type reference.
		/// </summary>
		/// <param name="typeReference">The <see cref="IDomTypeReference"/> to examine.  If it is not a generic type definition, it is directly returned.</param>
		/// <param name="contextType">A <see cref="IDomType"/> that provides contextual information and is already constructed.</param>
		/// <returns>
		/// The resolved <see cref="IDomType"/>.
		/// </returns>
		internal IDomType ConstructAndResolve(IDomTypeReference typeReference, IDomType contextType) {
			// Quit if not a generic type definition or type parameter
			if ((typeReference == null) || ((!typeReference.IsGenericType) && (!typeReference.IsGenericParameter)))
				return (typeReference != null ? typeReference.Resolve(this) : null);
			
			// Quit if the context type is not constructed
			ConstructedGenericType constructedContextType = contextType as ConstructedGenericType;
			if ((constructedContextType == null) && (typeReference.IsGenericType) && (typeReference.GenericTypeArguments != null) &&
				(typeReference.GenericTypeArguments.Count == 1) && (contextType.BaseType != null) && 
				((contextType.BaseType.FullName == "System.Array") || (contextType.FullName == "System.String"))) {
				// Process special case types
				if (contextType.BaseType.FullName == "System.Array") {
					// Arrays need to be treated as a generic of the array type
					return new ConstructedGenericType(typeReference.Resolve(this), new IDomTypeReference[] { new TypeReference(contextType.RawFullName, TextRange.Deleted) });				
				}
				else {  // System.String
					// Strings need to be treated as a generic of the Char type
					return new ConstructedGenericType(typeReference.Resolve(this), new IDomTypeReference[] { new TypeReference("System.Char", TextRange.Deleted) });				
				}
			}
			if ((constructedContextType == null) || (constructedContextType.GenericTypeArguments.Count == 0) || 
				(constructedContextType.GenericTypeArguments.Count != constructedContextType.GenericDefinitionType.GenericTypeArguments.Count)) {
				// If there is a type reference...
				if (typeReference != null) {
					// Resolve the type
					IDomType resolvedType = typeReference.Resolve(this);

					// If the type reference has constructed type parameters, and they align with the resolved type, continue on
					if ((resolvedType != null) && (typeReference.IsGenericType) && (!typeReference.IsGenericTypeDefinition) &&
						(resolvedType.IsGenericType) && (resolvedType.IsGenericTypeDefinition) && 
						(typeReference.GenericTypeArguments != null) && (resolvedType.GenericTypeArguments != null) &&
						(typeReference.GenericTypeArguments.Count > 0) &&
						(typeReference.GenericTypeArguments.Count == resolvedType.GenericTypeArguments.Count)) {
						// Create a constructed type
						return new ConstructedGenericType(resolvedType, typeReference.GenericTypeArguments);
					}
					else
						return resolvedType;
				}
				else
					return null;
			}

			IDomTypeReference[] unresolvedTypeParameters = new IDomTypeReference[constructedContextType.GenericTypeArguments.Count];
			IDomTypeReference[] resolvedTypeParameters = new IDomTypeReference[constructedContextType.GenericTypeArguments.Count];
			constructedContextType.GenericDefinitionType.GenericTypeArguments.CopyTo(unresolvedTypeParameters, 0);
			constructedContextType.GenericTypeArguments.CopyTo(resolvedTypeParameters, 0);

			if (typeReference.IsGenericParameter) {
				// Look for a match
				for (int index = 0; index < unresolvedTypeParameters.Length; index++) {
					if (typeReference.FullName == unresolvedTypeParameters[index].FullName)
						return resolvedTypeParameters[index].Resolve(this);
				}
			}
			else if (typeReference.GenericTypeArguments != null) {
				// Resolve all generic type parameters
				IDomTypeReference[] unresolvedGenericTypeArguments = new IDomTypeReference[typeReference.GenericTypeArguments.Count];
				IDomTypeReference[] resolvedGenericTypeArguments = new IDomTypeReference[typeReference.GenericTypeArguments.Count];
				typeReference.GenericTypeArguments.CopyTo(unresolvedGenericTypeArguments, 0);

				bool success = false;
				for (int index = 0; index < resolvedGenericTypeArguments.Length; index++) {
					success = false;
					for (int typeParameterIndex = 0; typeParameterIndex < unresolvedTypeParameters.Length; typeParameterIndex++) {
						IDomTypeReference existingResolvedGenericTypeArgument = unresolvedGenericTypeArguments[index] as IDomTypeReference;
						if (
							((existingResolvedGenericTypeArgument != null) && (!existingResolvedGenericTypeArgument.IsGenericParameter)) ||
							(unresolvedGenericTypeArguments[index].Name == unresolvedTypeParameters[typeParameterIndex].Name)
							) {
							resolvedGenericTypeArguments[index] = resolvedTypeParameters[typeParameterIndex];
							success = true;
						}
					}
					if (!success)
						break;
				}

				if (success) {
					typeReference = typeReference.Resolve(this);
					if ((typeReference != null) && (typeReference.IsGenericTypeDefinition))
						return new ConstructedGenericType((IDomType)typeReference, resolvedGenericTypeArguments);
					else
						return typeReference as IDomType;
				}
			}

			return (typeReference != null ? typeReference.Resolve(this) : null);
		}
		
		/// <summary>
		/// Constructs a type based on the supplied generic parameter resolution table.
		/// </summary>
		/// <param name="typeReference">The <see cref="IDomTypeReference"/> to resolve.</param>
		/// <param name="genericParametersTable">A table of generic parameters and their resolved <see cref="IDomType"/> values.</param>
		/// <param name="contextType">A <see cref="IDomType"/> that provides contextual information and is already constructed.</param>
		/// <returns>The <see cref="IDomType"/> that was constructed.</returns>
		private IDomType ConstructAndResolveFromTable(IDomTypeReference typeReference, Hashtable genericParametersTable, IDomType contextType) {
			if (typeReference.IsGenericParameter) {
				// The type reference is a generic parameter so look up its value from the table
				return (IDomType)genericParametersTable[DotNetProjectResolver.GetTypeName(DotNetProjectResolver.GetTypeNameWithoutArrayPointerSpec(typeReference.FullName))];
			}
			else if ((typeReference.IsGenericType) && (typeReference.GenericTypeArguments != null)) {
				// Get generic parameters for this type reference
				IDomTypeReference[] genericTypeParameters = new IDomTypeReference[typeReference.GenericTypeArguments.Count];
				typeReference.GenericTypeArguments.CopyTo(genericTypeParameters, 0);

				// Build an array of generic type arguments
				IDomTypeReference[] genericTypeArguments = new IDomTypeReference[typeReference.GenericTypeArguments.Count];
				for (int index = 0; index < genericTypeArguments.Length; index++) {
					genericTypeArguments[index] = this.ConstructAndResolveFromTable(genericTypeParameters[index], genericParametersTable, contextType);
					if (genericTypeArguments[index] == null)
						return null;
				}

				IDomType resolvedType = this.ConstructAndResolve(typeReference, contextType);
				return new ConstructedGenericType(resolvedType, genericTypeArguments);
			}
			else {
				// Fallback
				return this.ConstructAndResolve(typeReference, contextType);
			}
		}
		
		/// <summary>
		/// Constructs a generic type if one is passed.
		/// </summary>
		/// <param name="genericTypeDefinition">The <see cref="IDomType"/> to examine.  If it is not a generic type definition, it is directly returned.</param>
		/// <param name="contextType">A <see cref="IDomType"/> that provides contextual information.</param>
		/// <returns>
		/// The resolved <see cref="IDomType"/>.
		/// </returns>
		private IDomType ConstructGenericType(IDomType genericTypeDefinition, IDomType contextType) {
			// Quit if not a generic type definition
			if ((genericTypeDefinition == null) || (!genericTypeDefinition.IsGenericTypeDefinition) || (genericTypeDefinition.GenericTypeArguments.Count == 0))
				return genericTypeDefinition;

			// Quit if no the context type is passed or it is a generic type definition
			if ((contextType == null) || (contextType.IsGenericTypeDefinition))
				return genericTypeDefinition;

			// Get the type hierarchy for the context type
			IDomType[] typeHierarchy = this.GetTypeInheritanceHierarchy(contextType);
			if (typeHierarchy == null)
				return genericTypeDefinition;

			// Ensure the generic type definition is in the hierarchy
			for (int index = 0; index < typeHierarchy.Length; index++) {
				IDomType type = typeHierarchy[index];

				if (type.FullName == genericTypeDefinition.FullName) {
					if (index == 0) {
						// This matches the context type... return the context type if it is already constructed
						if (contextType is ConstructedGenericType)
							return (ConstructedGenericType)contextType;
					}
					else {
						// Resolve all generic type parameters
						bool success = true;
						object[] resolvedGenericTypeArguments = new object[genericTypeDefinition.GenericTypeArguments.Count];
						for (int typeParameterIndex = 0; typeParameterIndex < resolvedGenericTypeArguments.Length; typeParameterIndex++) {
							resolvedGenericTypeArguments[typeParameterIndex] = this.ResolveTypeParameter(typeHierarchy, index, typeParameterIndex);
							if (resolvedGenericTypeArguments[typeParameterIndex] == null) {
								success = false;
								break;
							}
						}

						if (success)
							return new ConstructedGenericType(genericTypeDefinition, resolvedGenericTypeArguments);
					}
				}
			}

			return genericTypeDefinition;
		}

		/// <summary>
		/// Attempts to construct a generic method.
		/// </summary>
		/// <param name="member">The <see cref="IDomMember"/> to examine.</param>
		/// <param name="extensionMethodTargetType">A optional <see cref="IDomType"/> that is passed if the method is an extension method applied to a type.</param>
		/// <param name="context">The <see cref="DotNetContext"/> to use.</param>
		/// <returns>The constructed <see cref="IDomMember"/>.</returns>
		private IDomMember ConstructGenericMethod(IDomMember member, IDomType extensionMethodTargetType, DotNetContext context) {
			// Quit if not a generic method definition
			if ((member == null) || (!member.IsGenericMethodDefinition) || (member.GenericTypeArguments == null) || (member.GenericTypeArguments.Count == 0))
				return member;

			// TODO:
			//*
			// Get the declared parameters
			IDomParameter[] parameters = member.Parameters;
			if ((parameters != null) && (parameters.Length > 0)) {
				// Initialize
				DotNetContextItem item = context.TargetItem;
				IDomType contextType = context.TypeDeclarationNode;

				// Get the generic type parameters
				IDomTypeReference[] unresolvedGenericTypeArguments = new IDomTypeReference[member.GenericTypeArguments.Count];
				IDomTypeReference[] resolvedGenericTypeArguments = new IDomTypeReference[member.GenericTypeArguments.Count];
				member.GenericTypeArguments.CopyTo(unresolvedGenericTypeArguments, 0);
				member.GenericTypeArguments.CopyTo(resolvedGenericTypeArguments, 0);

				// If there are any generic arguments in the context item, apply them
				if (item.GenericTypeArguments != null) {
					int genericTypeArgumentIndex = 0;
					foreach (IDomTypeReference genericTypeArgument in item.GenericTypeArguments) {
						// Quit the loop if there are more specified arguments than the method takes
						if (genericTypeArgumentIndex >= resolvedGenericTypeArguments.Length)
							break;

						resolvedGenericTypeArguments[genericTypeArgumentIndex++] = genericTypeArgument;
					}
				}

				for (int genericTypeArgumentIndex = 0; genericTypeArgumentIndex < unresolvedGenericTypeArguments.Length; genericTypeArgumentIndex++) {
					// If the resolved generic type argument is a generic parameter...
					IDomTypeReference unresolvedGenericTypeArgument = unresolvedGenericTypeArguments[genericTypeArgumentIndex];
					if (resolvedGenericTypeArguments[genericTypeArgumentIndex].IsGenericParameter) {
						// Scan parameters 
						for (int index = 0; index < parameters.Length; index++) {
							IDomParameter parameter = parameters[index];
							string genericParameterName = unresolvedGenericTypeArgument.Name;

							// If the parameter includes the generic parameter somewhere within it...
							if (DotNetProjectResolver.CollectGenericParameters(parameter.ParameterType, genericParameterName).Count > 0) {
								IDomType constructedGenericParameter = null;

								// If the member is an extension method...
								if ((member.IsExtension) && (index == 0) && (extensionMethodTargetType != null)) {
									// Try and resolve the extension method target type into a constructed generic parameter type reference
									constructedGenericParameter = this.GetConstructedGenericParameter(parameter.ParameterType, extensionMethodTargetType, genericParameterName, null, contextType);
								}

								// If there are arguments that can be examined...
								if ((constructedGenericParameter == null) && ((item.ArgumentsText != null) || (item.UnresolvedArguments != null))) {
									// Ensure that all the parameter arguments have been resolved
									context.ResolveArguments(item, contextType);

									// If there are now resolved arguments...
									if (item.ResolvedArguments != null) {
										// Determine if the extension method is applied so we can offset arguments by 1
										bool isExtensionMethodApplied = (member.IsExtension) && (item.ResolvedArguments.Length == parameters.Length - 1);
										int argumentIndex = Math.Max(0, index - (isExtensionMethodApplied ? 1 : 0));

										// If a resolved parameter argument can be used to resolve the generic parameter...
										if (argumentIndex < item.ResolvedArguments.Length) {
											// Try and resolve the argument expression into a constructed generic parameter type reference
											IDomType argumentType = this.ConstructAndResolve(item.ResolvedArguments[argumentIndex], contextType);
											if (argumentType != null)
												constructedGenericParameter = this.GetConstructedGenericParameter(parameter.ParameterType, argumentType, genericParameterName, null, contextType);
										}
									}
								}

								// If a constructed generic parameter was found and resolved to an IDomType...
								if (constructedGenericParameter != null)
									resolvedGenericTypeArguments[genericTypeArgumentIndex] = constructedGenericParameter;
							}
						}
					}
				}

				// See if all generic type arguments have been resolved
				bool allResolved = true;
				foreach (IDomTypeReference resolvedGenericTypeArgument in resolvedGenericTypeArguments) {
					if (resolvedGenericTypeArgument.IsGenericParameter) {
						allResolved = false;
						break;
					}
				}
				if (allResolved) {
					// Construct the method
					return new ConstructedGenericMember(member, resolvedGenericTypeArguments);
				}
			}
			 //*/

			return member;
		}
		
		/// <summary>
		/// Returns an assembly from the current <see cref="AppDomain"/> that has the specified partial name, if one exists.
		/// </summary>
		/// <param name="partialAssemblyName">The partial assembly name to look for.</param>
		/// <returns>An assembly from the current <see cref="AppDomain"/> that has the specified partial name, if one exists.</returns>
		private Assembly GetAssemblyFromAppDomain(string partialAssemblyName) {
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			foreach (Assembly assembly in assemblies) {
				if (String.Compare(assembly.GetName().Name, partialAssemblyName, true) == 0)
					return assembly;
			}
			return null;
		}
		
		/// <summary>
		/// Returns a constructed generic parameter, by searching through a member parameter type reference and comparing it with supplied arguments.
		/// </summary>
		/// <param name="parameterTypeReference">The parameter to examine for a match of <paramref name="name"/>.</param>
		/// <param name="argumentType">The argument that is passed to the parameter.</param>
		/// <param name="name">The name of the generic parameter that should be located.</param>
		/// <param name="typeReferenceStack">A <see cref="Stack"/> of type references traversed to prevent any possible infinite recursion.</param>
		/// <param name="contextType">A <see cref="IDomType"/> that provides contextual information and is already constructed.</param>
		/// <returns>
		/// The <see cref="IDomType"/> that was constructed for the generic parameter; otherwise, <see langword="null"/>.
		/// </returns>
		private IDomType GetConstructedGenericParameter(IDomTypeReference parameterTypeReference, IDomType argumentType, string name, Stack typeReferenceStack, IDomType contextType) {
			if ((parameterTypeReference == null) || (argumentType == null))
				return null;

			// If the type reference is a generic parameter and its name (without array/pointer) matches the desired name...
			if ((parameterTypeReference.IsGenericParameter) && 
				(DotNetProjectResolver.GetTypeName(DotNetProjectResolver.GetTypeNameWithoutArrayPointerSpec(parameterTypeReference.FullName)) == name))
				return argumentType;

			// If the type reference is a generic type...
			if (parameterTypeReference.IsGenericType) {
				// If there are generic type parameters that we can recurse into...
				ICollection genericParameters = parameterTypeReference.GenericTypeArguments;
				if (genericParameters != null) {
					ICollection genericTypeArguments = null;
					
					// If the argument type is a generic type, use its type arguments
					if (argumentType.IsGenericType)
						genericTypeArguments = argumentType.GenericTypeArguments;
					else if ((argumentType.BaseType != null) && (argumentType.BaseType.FullName == "System.Array")) {
						// Special case for arrays, need to be treated like generic types
						genericTypeArguments = new IDomTypeReference[] { new TypeReference(argumentType.RawFullName, TextRange.Deleted) };
					}
					else if (argumentType.FullName == "System.String") {
						// Special case for strings, need to be treated like generic types
						genericTypeArguments = new IDomTypeReference[] { new TypeReference("System.Char", TextRange.Deleted) };
					}

					// If there are generic type arguments...
					if (genericTypeArguments != null) {
						// Get an array of the generic arguments
						IDomTypeReference[] genericTypeArgumentArray = new IDomTypeReference[genericTypeArguments.Count];
						genericTypeArguments.CopyTo(genericTypeArgumentArray, 0);

						// Loop through the generic parameters
						int index = 0;
						foreach (IDomTypeReference genericParameter in genericParameters) {
							// Quit if past the available generic type arguments
							if (index >= genericTypeArgumentArray.Length)
								break;

							if (typeReferenceStack == null) {
								// Create a new stack
								typeReferenceStack = new Stack();
							}
							else {
								// Check for recursion
								if (typeReferenceStack.Contains(typeReferenceStack))
									return null;
							}

							// Add to the stack
							typeReferenceStack.Push(parameterTypeReference);

							// Recurse into each generic parameter
							IDomType result = this.GetConstructedGenericParameter(genericParameter, 
								this.ConstructAndResolve(genericTypeArgumentArray[index], contextType), 
								name, typeReferenceStack, contextType);
							if (result != null)
								return result;

							// Pop the stack entry
							typeReferenceStack.Pop();

							// Increment index
							index++;
						}
					}


				}
			}

			return null;
		}

		/// <summary>
		/// Returns a <see cref="StringCollection"/> containing the dependency search paths.
		/// </summary>
		/// <returns>A <see cref="StringCollection"/> containing the dependency search paths.</returns>
		internal StringCollection GetDependencySearchPaths() {
			StringCollection dependencySearchPaths = new StringCollection();

			// 8/27/2008 - Add the application base path of the current AppDomain as a search path
			//   This cannot be implemented after all because it causes issues for some scenarios when loading assemblies 
			/*
			string applicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
			if ((applicationBase != null) && (applicationBase.Trim().Length > 0))
				dependencySearchPaths.Add(applicationBase);
			*/

			foreach (string externalReference in externalReferences) {
				IProjectContent projectContent = AssemblyCodeRepository.GetProjectContent(externalReference);
				if ((projectContent != null) && (projectContent.AssemblyLocation != null)) {
					if ((projectContent.AssemblyLocation != null) && (projectContent.AssemblyLocation.Length > 0)) {
						string path = Path.GetDirectoryName(projectContent.AssemblyLocation).ToLower();
						if ((path.IndexOf("assembly\\gac_") == 0) && (Directory.Exists(path)) && (!dependencySearchPaths.Contains(path)))
							dependencySearchPaths.Add(path);
					}
				}
			}
			return dependencySearchPaths;
		}
		
		/// <summary>
		/// Gets the generic specification for the specified <see cref="IDomTypeReference"/>.
		/// </summary>
		/// <param name="typeReference">The <see cref="IDomTypeReference"/> to examine.</param>
		/// <returns>The generic specification for the specified <see cref="IDomTypeReference"/>.</returns>
		internal static string GetGenericSpecification(IDomTypeReference typeReference) {
			// Build the generic specification, if any
			if (typeReference.IsGenericTypeDefinition) {
				StringBuilder result = new StringBuilder();
				result.Append("<");
				foreach (IDomTypeReference parameter in typeReference.GenericTypeArguments) {
					if (result.Length > 1)
						result.Append(",");
					result.Append(parameter.Name);
				}
				result.Append(">");
				return result.ToString();
			}
			return String.Empty;
		}

		/// <summary>
		/// Gets all the indexers in the <see cref="IDomType"/> that match the specified parameter counts, which includes inherited members.
		/// </summary>
		/// <param name="contextType">The <see cref="IDomType"/> that provides the context of the lookup.</param>
		/// <param name="targetType">The <see cref="IDomType"/> to examine.</param>
		/// <param name="flags">The <see cref="DomBindingFlags"/> to match.</param>
		/// <param name="indexerParameterCount">The indexer parameter count.</param>
		/// <returns>An <see cref="IDomMember"/> array including all the matching indexers in the <see cref="IDomType"/>.</returns>
		internal IDomMember[] GetIndexers(IDomType contextType, IDomType targetType, DomBindingFlags flags, int indexerParameterCount) {
			// Get the members
			IDomMember[] memberArray = this.GetMembers(contextType, targetType, null, DomBindingFlags.OnlyIndexers | flags);

			// Loop through the types in the inheritance hierarchy...
			ArrayList members = new ArrayList();
			if (memberArray != null) {
				foreach (IDomMember member in memberArray) {
					IDomParameter[] parameters = member.Parameters;
					if ((parameters != null) && (parameters.Length == indexerParameterCount))
						members.Add(member);
				}
			}

			return (IDomMember[])members.ToArray(typeof(IDomMember));
		}
		
		/// <summary>
		/// Gets the namespace name from the specified full type name.
		/// </summary>
		/// <param name="fullTypeName">The full type name to examine.</param>
		/// <returns>The namespace name from the specified full type name.</returns>
		internal static string GetNamespaceName(string fullTypeName) {
			if (fullTypeName != null) {
				int index = fullTypeName.LastIndexOf('.');
				if (index != -1)
					return fullTypeName.Substring(0, index);
				else
					return null;
			}
			return null;
		}
		
		/// <summary>
		/// Walks up a parent type level and gets the <see cref="IDomType"/> for the specified type full name using context information.
		/// </summary>
		/// <param name="typeDeclarationNode">The <see cref="IDomType"/> to examine.</param>
		/// <param name="namespaceName">The namespace name.</param>
		/// <param name="fullTypeName">The full type name.</param>
		/// <param name="ignoreCase">Whether to ignore case.</param>
		/// <returns>The <see cref="IDomType"/> for the specified type full name using context information.</returns>
		private IDomType GetParentType(IDomType typeDeclarationNode, string namespaceName, string fullTypeName, bool ignoreCase) {
			if ((typeDeclarationNode == null) || (typeDeclarationNode.Name == null))
				return null;

			if (namespaceName.EndsWith(typeDeclarationNode.Name)) {
				// Get the root namespace name
				namespaceName = namespaceName.Substring(0, namespaceName.Length - typeDeclarationNode.Name.Length);

				// Check for other types in the same namespace
				if (typeDeclarationNode is AstNode) {
					CompilationUnit compilationUnit = (CompilationUnit)((AstNode)typeDeclarationNode).FindAncestor(typeof(CompilationUnit));
					foreach (IDomType compilationUnitType in compilationUnit.Types) {
						if (String.Compare(compilationUnitType.FullName, namespaceName + fullTypeName, ignoreCase) == 0)
							return compilationUnitType.Resolve(this);
					}
				}
				else if (typeDeclarationNode is SourceMergedPartialType) {
					lock (typeDeclarationNode) {
						foreach (IDomType type in ((SourceMergedPartialType)typeDeclarationNode).PartialTypes) {
							if (type is AstNode) {
								CompilationUnit compilationUnit = (CompilationUnit)((AstNode)type).FindAncestor(typeof(CompilationUnit));
								foreach (IDomType compilationUnitType in compilationUnit.Types) {
									if (String.Compare(compilationUnitType.FullName, namespaceName + fullTypeName, ignoreCase) == 0)
										return compilationUnitType.Resolve(this);
								}
							}
						}
					}
				}

				// Remove the period
				if (namespaceName.EndsWith("."))
					namespaceName = namespaceName.Substring(0, namespaceName.Length - 1);

			}

			// Move up to a parent type, if any
			if (typeDeclarationNode is AstNode) {
				typeDeclarationNode = ((AstNode)typeDeclarationNode).ParentNode as TypeDeclaration;
				if (typeDeclarationNode != null)
					return this.GetParentType(typeDeclarationNode.Resolve(this), namespaceName, fullTypeName, ignoreCase);
			}
			else if (typeDeclarationNode is SourceMergedPartialType) {
				lock (typeDeclarationNode) {
					foreach (IDomType type in ((SourceMergedPartialType)typeDeclarationNode).PartialTypes) {
						if (type is AstNode) {
							IDomType parentTypeDeclarationNode = ((AstNode)type).ParentNode as TypeDeclaration;
							if (parentTypeDeclarationNode != null)
								return this.GetParentType(parentTypeDeclarationNode.Resolve(this), namespaceName, fullTypeName, ignoreCase);
						}
					}
				}
			}
			
			return null;
		}
		
		/// <summary>
		/// Gets the <see cref="IDomType"/> for the specified type full name within the assembly.
		/// </summary>
		/// <param name="assemblyHint">The assembly that contains the type.</param>
		/// <param name="typeFullName">The full type name.</param>
		/// <returns>The <see cref="IDomType"/> for the specified type full name within the assembly.</returns>
		internal IDomType GetType(string assemblyHint, string typeFullName) {
			int commaIndex = assemblyHint.IndexOf(',');
			if (commaIndex != -1)
				assemblyHint = assemblyHint.Substring(0, commaIndex + 1);

			foreach (string externalReference in externalReferences) {
				if (externalReference.ToLower().StartsWith(assemblyHint)) {
					IProjectContent projectContent = AssemblyCodeRepository.GetProjectContent(externalReference);
					if (projectContent != null) {
						IDomType type = projectContent.GetType(null, typeFullName, DomBindingFlags.Default);
						if (type != null)
							return type;
					}
				}
			}

			return null;
		}
		
		/// <summary>
		/// Gets the full type name from a possible language shortcut type name.
		/// </summary>
		/// <param name="language">The <see cref="DotNetLanguage"/> to use for examining language shortcut type names.</param>
		/// <param name="name">The type name to examine.</param>
		/// <returns>The full type name from a possible language shortcut type name.</returns>
		internal static string GetTypeFullNameFromShortcut(DotNetLanguage language, string name) {
			switch (language) {
				case DotNetLanguage.CSharp:
					switch (name) {
						case "bool":
							return "System.Boolean";
						case "byte":
							return "System.Byte";
						case "char":
							return "System.Char";
						case "decimal":
							return "System.Decimal";
						case "double":
							return "System.Double";
						case "dynamic":
							return "System.Object";
						case "short":
							return "System.Int16";
						case "int":
							return "System.Int32";
						case "long":
							return "System.Int64";
						case "object":
							return "System.Object";
						case "sbyte":
							return "System.SByte";
						case "float":
							return "System.Single";
						case "string":
							return "System.String";
						case "ushort":
							return "System.UInt16";
						case "uint":
							return "System.UInt32";
						case "ulong":
							return "System.UInt64";
						case "void":
							return "System.Void";
					}
					break;
				case DotNetLanguage.VB:
					switch (name.ToLower()) {
						case "boolean":
							return "System.Boolean";
						case "byte":
							return "System.Byte";
						case "char":
							return "System.Char";
						case "date":
							return "System.DateTime";
						case "decimal":
							return "System.Decimal";
						case "double":
							return "System.Double";
						case "short":
							return "System.Int16";
						case "integer":
							return "System.Int32";
						case "long":
							return "System.Int64";
						case "object":
							return "System.Object";
						case "sbyte":
							return "System.SByte";
						case "single":
							return "System.Single";
						case "string":
							return "System.String";
						case "ushort":
							return "System.UInt16";
						case "uinteger":
							return "System.UInt32";
						case "ulong":
							return "System.UInt64";
					}
					break;
			}

			return name;
		}
		
		/// <summary>
		/// Converts a full type name so that it will appear properly for display in the user interface.
		/// </summary>
		/// <param name="fullTypeName">The full type name to examine.</param>
		/// <returns>The modified full type name.</returns>
		internal static string GetTypeFullNameForDisplay(string fullTypeName) {
			// Remove any trailing & mark for by-reference
			if (fullTypeName.EndsWith("&"))
				fullTypeName = fullTypeName.Substring(0, fullTypeName.Length - 1);

			// Change nested type indicator to a dot
			fullTypeName = fullTypeName.Replace('+', '.');

			// If the type name is a generic type, find the index of the mark
			int markIndex = fullTypeName.IndexOf('`');
			if (markIndex == -1)
				return fullTypeName;

			// Remove the generic type name mark (`number) and leave any <... that follows
			int argumentStartIndex = fullTypeName.IndexOf('<', markIndex + 1);
			if (argumentStartIndex != -1)
				return fullTypeName.Substring(0, markIndex) + fullTypeName.Substring(argumentStartIndex);
			else
				return fullTypeName.Substring(0, markIndex);
		}
		
		/// <summary>
		/// Returns the inheritance hierarchy for the specified <see cref="IDomType"/>.
		/// </summary>
		/// <param name="type">The <see cref="IDomType"/> to examine.</param>
		/// <param name="targetTypeName">The target type name, which can be optionally provided to prevent circular recursion, like when resolving "Foo" in this example: "class MyClass : Foo {}" since Foo requires a context hierarchy.</param>
		/// <returns>The inheritance hierarchy for the specified <see cref="IDomType"/>.</returns>
		private IDomType[] GetTypeInheritanceHierarchy(IDomType type, string targetTypeName) {
			if (type == null)
				return null;
			if ((type.Type == DomTypeType.StandardModule) || (type.FullName == "System.Void"))
				return new IDomType[] { type };

			bool isInterface = (type.Type == DomTypeType.Interface);

			ArrayList hierarchy = new ArrayList();

			// Strip off generic marker
			if (targetTypeName != null) {
				int markIndex = targetTypeName.IndexOf('`');
				if (markIndex != -1)
					targetTypeName = targetTypeName.Substring(0, markIndex);
			}
			
			if (isInterface) {
				ArrayList remaining = new ArrayList();

				// Call this simply to update whether the first base type is an interface
				if (targetTypeName == null)
					type = type.Resolve(this);

				// Process an interface inheritance hierarchy
				while (type != null) {
					// Check to ensure that there is no circular reference...
					if (type.ProjectContent == null) {
						// Circular references cannot occur in assemblies so only look at source project content, which will be at the end
						for (int index = hierarchy.Count - 1; index >= 0; index--) {
							if (hierarchy[index] == type) {
								// Set the type to null to indicate to not add the duplicate item
								type = null;
								break;
							}
							if (((IDomType)hierarchy[index]).ProjectContent != null)
								break;
						}
					}

					if (type != null) {
						// Add the type
						hierarchy.Add(type);

						// Get the interface types
						// NOTE: This doesn't seem to be pulling in multi-inherited interfaces when they are defined in source code
						IDomTypeReference[] interfaceTypes = type.GetInterfaces();

						if (interfaceTypes != null) {
							foreach (IDomTypeReference interfaceType in interfaceTypes) {
								// See if the interface type is already queued up
								bool alreadyQueued = false;
								foreach (IDomTypeReference typeReference in remaining) {
									if (typeReference.FullName == interfaceType.FullName) {
										alreadyQueued = true;
										break;
									}
								}

								// Add the to the remaining collection if it isn't already queued
								if (!alreadyQueued)
									remaining.Add(interfaceType);
							}
						}
					}

					// Get the next type to examine
					type = null;
					if (remaining.Count > 0) {
						IDomTypeReference nextTypeReference = (IDomTypeReference)remaining[0];
						remaining.RemoveAt(0);
						if (nextTypeReference.RawFullName != targetTypeName)
							type = nextTypeReference.Resolve(this);
					}	
				}
					
				// Ensure System.Object is in the list
				if ((hierarchy.Count == 0) || (((IDomTypeReference)hierarchy[hierarchy.Count - 1]).FullName != "System.Object")) {
					type = this.GetType(null, new string[] {}, "System.Object", DomBindingFlags.Default);
					if (type != null)
						hierarchy.Add(type);
				}
			}
			else {
				// Process a non-interface inheritance hierarchy
				while (type != null) {
					// Check to ensure that there is no circular reference...
					if (type.ProjectContent == null) {
						// Circular references cannot occur in assemblies so only look at source project content, which will be at the end
						for (int index = hierarchy.Count - 1; index >= 0; index--) {
							if (hierarchy[index] == type) {
								// Assume System.Object instead since there is a circular reference
								type = this.GetType(null, new string[] {}, "System.Object", DomBindingFlags.Default);
								break;
							}
							if (((IDomType)hierarchy[index]).ProjectContent != null)
								break;
						}
						if (type == null)
							break;
					}

					// Add the type
					hierarchy.Add(type);

					// Get the base type name and strip off generic marker (for comparison to targetTypeName since its ` marker is removed above)
					string baseTypeName = null;
					if (type.BaseType != null) {
						baseTypeName = DotNetProjectResolver.GetTypeReferenceRawFullName(type.BaseType);
						if (baseTypeName != null) {
							int markIndex = baseTypeName.IndexOf('`');
							if (markIndex != -1)
								baseTypeName = baseTypeName.Substring(0, markIndex);
						}
					}

					// Resolve to the base type
					if ((baseTypeName != null) && (baseTypeName != targetTypeName))
						type = type.BaseType.Resolve(this);
					else
						type = null;
				}
			}

			return (IDomType[])hierarchy.ToArray(typeof(IDomType));
		}

		/// <summary>
		/// Gets the type name from the specified full type name.
		/// </summary>
		/// <param name="fullTypeName">The full type name to examine.</param>
		/// <returns>The type name from the specified full type name.</returns>
		internal static string GetTypeName(string fullTypeName) {
			if (fullTypeName != null) {
				int index = fullTypeName.LastIndexOfAny(new char[] { '.', '+' });
				if (index != -1)
					fullTypeName = fullTypeName.Substring(index + 1);
			}
			return fullTypeName;
		}
		
		/// <summary>
		/// Gets the type name for debugging purposes.
		/// </summary>
		/// <param name="typeReference">The <see cref="IDomTypeReference"/> to examine.</param>
		/// <returns>The type name for debugging purposes.</returns>
		internal static string GetTypeNameForDebugging(IDomTypeReference typeReference) {
			string typeName = typeReference.Name;

			ICollection genericTypeArguments = typeReference.GenericTypeArguments;
			if ((genericTypeArguments != null) && (genericTypeArguments.Count > 0)) {
				typeName += "<";
				foreach (IDomTypeReference genericTypeArgument in genericTypeArguments) {
					if (!typeName.EndsWith("<"))
						typeName += ", ";
					typeName += DotNetProjectResolver.GetTypeNameForDebugging(genericTypeArgument);
				}
				typeName += ">";
			}
			
			if (typeReference.PointerLevel > 0)
				typeName += new string('*', typeReference.PointerLevel);

			if (typeReference.ArrayRanks != null) {
				foreach (int arrayRank in typeReference.ArrayRanks) {
					typeName += "[";
					for (int index = 1; index < arrayRank; index++)
						typeName += ",";
					typeName += "]";
				}
			}

			return typeName;
		}
		
		/// <summary>
		/// Gets the type name with any related array rank and pointer level data appended.
		/// </summary>
		/// <param name="typeName">The name of the type.</param>
		/// <param name="genericTypeArgumentCount">The generic type argument count.</param>
		/// <param name="arrayRanks">The array rank dimensions.</param>
		/// <param name="pointerLevel">The pointer level.</param>
		/// <returns>The type name with any related array rank and pointer level data appended.</returns>
		internal static string GetTypeNameWithArrayPointerSpec(string typeName, int genericTypeArgumentCount, int[] arrayRanks, int pointerLevel) {
			if (genericTypeArgumentCount > 0)
				typeName += "`" + genericTypeArgumentCount;
			
			if (pointerLevel > 0)
				typeName += new string('*', pointerLevel);

			if (arrayRanks != null) {
				foreach (int arrayRank in arrayRanks) {
					typeName += "[";
					for (int index = 1; index < arrayRank; index++)
						typeName += ",";
					typeName += "]";
				}
			}

			return typeName;
		}
		
		/// <summary>
		/// Gets the type name without any related array rank and pointer level data appended.
		/// </summary>
		/// <param name="typeName">The name of the type.</param>
		/// <returns>The type name without any related array rank and pointer level data appended.</returns>
		internal static string GetTypeNameWithoutArrayPointerSpec(string typeName) {
			if (typeName.EndsWith("]")) {
				int index = typeName.IndexOf('[');
				if (index != -1)
					typeName = typeName.Substring(0, index);
			}

			if (typeName.EndsWith("*")) {
				int index = typeName.IndexOf('*');
				if (index != -1)
					typeName = typeName.Substring(0, index);
			}

			return typeName;
		}
		
		/// <summary>
		/// Returns the raw full name of the specified type reference, doing a quick check to see if any namespace aliases should be resolved.
		/// </summary>
		/// <param name="domTypeReference">The <see cref="IDomTypeReference"/> to examine.</param>
		/// <returns>The raw full name of the specified type reference.</returns>
		private static string GetTypeReferenceRawFullName(IDomTypeReference domTypeReference) {
			// If working with an AST type reference...
			TypeReference typeReference = domTypeReference as TypeReference;
			if (typeReference != null) {
				// If the namespace can possibly be an alias...
				string possibleAlias = domTypeReference.Namespace;
				if ((possibleAlias != null) && (possibleAlias.Length > 0) && (possibleAlias.IndexOf('.') == -1)) {
					CompilationUnit compilationUnit = typeReference.RootNode as CompilationUnit;
					if (compilationUnit != null) {
						// Get the namespace aliases
						string[] importedNamespaces;
						Hashtable namespaceAliases;
						compilationUnit.GetImportedNamespaces(typeReference, out importedNamespaces, out namespaceAliases);

						// If an alias is found, use it
						string resolvedNamespace = (string)namespaceAliases[possibleAlias];
						if (resolvedNamespace != null)
							return resolvedNamespace + "." + domTypeReference.Name;
					}
				}
			}

			return domTypeReference.RawFullName;
		}

		/// <summary>
		/// Gets the full type name, or a language shortcut keyword specifying the type if possible.
		/// </summary>
		/// <param name="language">The <see cref="DotNetLanguage"/> for which to return a shortcut keyword if possible.</param>
		/// <param name="name">The type name to examine.</param>
		/// <returns>The full type name, or a language shortcut keyword specifying the type if possible.</returns>
		internal static string GetTypeShortcutName(DotNetLanguage language, string name) {
			switch (language) {
				case DotNetLanguage.CSharp:
					switch (name.ToLower()) {
						case "system.boolean":
							return "bool";
						case "system.byte":
							return "byte";
						case "system.char":
							return "char";
						case "system.decimal":
							return "decimal";
						case "system.double":
							return "double";
						case "system.int16":
							return "short";
						case "system.int32":
							return "int";
						case "system.int64":
							return "long";
						case "system.object":
							return "object";
						case "system.sbyte":
							return "sbyte";
						case "system.single":
							return "float";
						case "system.string":
							return "string";
						case "system.uint16":
							return "ushort";
						case "system.uint32":
							return "uint";
						case "system.uint64":
							return "ulong";
						case "system.void":
							return "void";
					}
					break;
				case DotNetLanguage.VB:
					switch (name.ToLower()) {
						case "system.boolean":
							return "Boolean";
						case "system.byte":
							return "Byte";
						case "system.char":
							return "Char";
						case "system.datetime":
							return "Date";
						case "system.decimal":
							return "Decimal";
						case "system.double":
							return "Double";
						case "system.int16":
							return "Short";
						case "system.int32":
							return "Integer";
						case "system.int64":
							return "Long";
						case "system.object":
							return "Object";
						case "system.sbyte":
							return "SByte";
						case "system.single":
							return "Single";
						case "system.string":
							return "String";
						case "system.uint16":
							return "UShort";
						case "system.uint32":
							return "UInteger";
						case "system.uint64":
							return "ULong";
					}
					break;
			}

			return name;
		}
		
		/// <summary>
		/// Gets the custom <see cref="ResolveEventHandler"/> to use when attempting to resolve assemblies loaded in the host <c>AppDomain</c> by the <see cref="AssemblyCodeRepository"/>.
		/// </summary>
		/// <value>The custom <see cref="ResolveEventHandler"/> to use when attempting to resolve assemblies loaded in the host <c>AppDomain</c> by the <see cref="AssemblyCodeRepository"/>.</value>
		internal ResolveEventHandler HostAppDomainResolver {
			get {
				return hostAppDomainResolver;
			}
		}

		/// <summary>
		/// Resolves a specific generic type parameter by walking up a type hierarchy chain and looking for a construction of the parameter.
		/// </summary>
		/// <param name="typeHierarchy">The <see cref="IDomType"/> hierarchy to examine.</param>
		/// <param name="typeIndex">The index of the type within the hierarchy.</param>
		/// <param name="typeParameterIndex">The type parameter index to locate.</param>
		/// <returns></returns>
		private object ResolveTypeParameter(IDomType[] typeHierarchy, int typeIndex, int typeParameterIndex) {
			if (typeIndex > 0) {
				IDomType baseType = typeHierarchy[typeIndex];
				IDomType inheritedType = typeHierarchy[typeIndex - 1];

				// If the base class is a generic type definition...
				ConstructedGenericType constructedBaseType = baseType as ConstructedGenericType;
				if ((baseType.IsGenericTypeDefinition) || (constructedBaseType != null)) { 
					// See if the inherited type is a constructed type... if so switch to look at its generic definition type
					ConstructedGenericType constructedInheritedType = inheritedType as ConstructedGenericType;
					if (constructedInheritedType != null) 
						inheritedType = constructedInheritedType.GenericDefinitionType;

					// Check to see if the inherited type is defined in an assembly...
					TypeDeclaration inheritedTypeDeclaration = inheritedType as TypeDeclaration;
					AssemblyDomType inheritedDomType = inheritedType as AssemblyDomType;
					if ((inheritedTypeDeclaration != null) || (inheritedDomType != null)) {
						// Get the collection of base types
						ICollection baseTypeRefs = null;
						if (inheritedTypeDeclaration != null)
							baseTypeRefs = inheritedTypeDeclaration.BaseTypes;
						else if ((inheritedDomType != null) && (inheritedDomType.BaseType != null))
							baseTypeRefs = new IDomTypeReference[] { inheritedDomType.BaseType };

						if (baseTypeRefs != null) {
							// The inherited type is declared in code... loop through its base type references
							foreach (IDomTypeReference baseTypeRef in baseTypeRefs) {
								// If a base type reference match is found...
								if (baseType.FullName == baseTypeRef.FullName) {
									// If there are generic type arguments available...
									if (baseTypeRef.GenericTypeArguments != null) {
										IDomTypeReference[] baseTypeParameters = new IDomTypeReference[baseTypeRef.GenericTypeArguments.Count];
										baseTypeRef.GenericTypeArguments.CopyTo(baseTypeParameters, 0);
										
										// If the desired type parameter index is within bounds...
										if (typeParameterIndex < baseTypeParameters.Length) {
											if (baseTypeParameters[typeParameterIndex].IsGenericParameter) {
												// The parameter is a generic parameter
												if (inheritedType.GenericTypeArguments != null) {
													IDomTypeReference[] inheritedTypeGenericTypeArguments = new IDomTypeReference[inheritedType.GenericTypeArguments.Count];
													inheritedType.GenericTypeArguments.CopyTo(inheritedTypeGenericTypeArguments, 0);

													for (int declaredTypeParameterIndex = 0; declaredTypeParameterIndex < inheritedTypeGenericTypeArguments.Length; declaredTypeParameterIndex++) {
														if (baseTypeParameters[typeParameterIndex].FullName == inheritedTypeGenericTypeArguments[declaredTypeParameterIndex].FullName) {
															// Recurse up
															return this.ResolveTypeParameter(typeHierarchy, typeIndex - 1, declaredTypeParameterIndex);
														}
													}
												}
											}
											else {
												// The parameter is a constructed type so return it
												return baseTypeParameters[typeParameterIndex];
											}
										}
									}
									break;
								}
							}
						}
						return null;
					}
					else {
						// Not a type declaration... see if it is a constructed generic type
						if ((constructedInheritedType != null) && (constructedInheritedType.GenericTypeArguments.Count > 0)) {
							if (typeParameterIndex < constructedInheritedType.GenericTypeArguments.Count) {
								IDomTypeReference[] inheritedTypeParameters = new IDomTypeReference[constructedInheritedType.GenericTypeArguments.Count];
								constructedInheritedType.GenericTypeArguments.CopyTo(inheritedTypeParameters, 0);

								return inheritedTypeParameters[typeParameterIndex];
							}
						}
					}
				}				
			}
			else if (typeHierarchy.Length > 0) {
				ConstructedGenericType constructedInheritedType = typeHierarchy[0] as ConstructedGenericType;
				if ((constructedInheritedType != null) && (constructedInheritedType.GenericTypeArguments.Count > 0)) {
					if (typeParameterIndex < constructedInheritedType.GenericTypeArguments.Count) {
						IDomTypeReference[] inheritedTypeParameters = new IDomTypeReference[constructedInheritedType.GenericTypeArguments.Count];
						constructedInheritedType.GenericTypeArguments.CopyTo(inheritedTypeParameters, 0);

						if (typeParameterIndex < inheritedTypeParameters.Length)
							return inheritedTypeParameters[typeParameterIndex];
					}
				}
			}

			return null;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Adds all assemblies in the current <see cref="AppDomain"/> as external references.
		/// </summary>
		/// <remarks>
		/// If an exception occurs during the loading of one of the assemblies, it will continue loading the remaining
		/// assemblies and then re-throw the first exception.
		/// </remarks>
		public void AddAllAssembliesInAppDomainAsExternalReferences() {
			Exception ex = null;

			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			foreach (Assembly assembly in assemblies) {
				try {
					this.AddExternalReference(assembly);
				}
				catch (Exception innerEx) {
					if (ex == null)
						ex = innerEx;
				}
			}

			if (ex != null)
				throw ex;
		}
		
		/// <summary>
		/// Adds an assembly as an external reference.
		/// </summary>
		/// <param name="assembly">The assembly to add.</param>
		public void AddExternalReference(Assembly assembly) {
			this.AddExternalReference(assembly, assembly.FullName);
		}
		
		/// <summary>
		/// Adds an assembly as an external reference, but with the specified assembly full name.
		/// </summary>
		/// <param name="assembly">The assembly to add.</param>
		/// <param name="assemblyFullName">The full name of the assembly to use for storing/accessing reflection data.</param>
		/// <remarks>
		/// You can use this overload if you do dynamic generation of assemblies in memory but want to
		/// use the same assembly full name for the generated assembly reflection data.
		/// </remarks>
		public void AddExternalReference(Assembly assembly, string assemblyFullName) {
			// Load into the respository
			AssemblyCodeRepository.Add(assembly, assemblyFullName, this);

			// Add to the external references
			if (!externalReferences.Contains(assemblyFullName))
				externalReferences.Add(assemblyFullName);
		}

		/// <summary>
		/// Adds an assembly as an external reference.
		/// </summary>
		/// <param name="assemblyName">The full name, or path, of the assembly to add.</param>
		/// <remarks>
		/// <c>true</c> if an assembly was added successfully; otherwise, <c>false</c>.
		/// </remarks>
		public bool AddExternalReference(string assemblyName) {
			// Load into the respository and get the assembly full name
			assemblyName = AssemblyCodeRepository.Add(assemblyName, this);

			if (assemblyName != null) {
				// Add to the external references
				if (!externalReferences.Contains(assemblyName))
					externalReferences.Add(assemblyName);
				return true;
			}

			return false;
		}
		
		/// <summary>
		/// Adds the <c>MSCorLib</c> assembly as an external reference.
		/// </summary>
		public void AddExternalReferenceForMSCorLib() {
			this.AddExternalReference(this.GetAssemblyFromAppDomain("mscorlib"));
		}

		/// <summary>
		/// Adds a system assembly as an external reference.
		/// </summary>
		/// <param name="assemblyName">The partial assembly name.</param>
		/// <remarks>
		/// <c>true</c> if an assembly was added successfully; otherwise, <c>false</c>.
		/// </remarks>
		public bool AddExternalReferenceForSystemAssembly(string assemblyName) {
			// Remove any trailing .dll
			if (assemblyName.ToLower().EndsWith(".dll"))
				assemblyName = assemblyName.Substring(0, assemblyName.Length - 4);

			// First try and find the assembly in the app domain
			Assembly assembly = this.GetAssemblyFromAppDomain(assemblyName);
			if (assembly != null) {
				this.AddExternalReference(assembly);
				return true;
			}

			// Look for the assembly in the file system
			assemblyName += ".dll";

			// Start search in the system path
			string path = Path.Combine(systemPath, assemblyName);
			if (File.Exists(path))
				return this.AddExternalReference(path);
			
			// Get the search paths
			string[] searchPaths = new string[] {
				@"C:\Program Files\Reference Assemblies\Microsoft\Framework\v3.0",
				@"C:\Program Files\Reference Assemblies\Microsoft\Framework\v3.5",
				@"C:\Program Files\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0",
				@"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\v3.0",
				@"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\v3.5",
				@"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0",
			};
			foreach (string searchPath in searchPaths) {
				if (Directory.Exists(searchPath)) {
					// See if there the assembly file is in the search path
					path = Path.Combine(searchPath, assemblyName);
					if (File.Exists(path))
						return this.AddExternalReference(path);
				}
			}

			return false;
		}

		/// <summary>
		/// Gets or sets the path to use for reflection and documentation cache files.
		/// </summary>
		/// <value>The path to use for reflection and documentation cache files.</value>
		public string CachePath {
			get {
				return cachePath;
			}
			set {
				cachePath = value;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="CodeSnippetFolderCollection"/> containing the available code snippet folders.
		/// </summary>
		/// <value>The <see cref="CodeSnippetFolderCollection"/> containing the available code snippet folders.</value>
		public CodeSnippetFolderCollection CodeSnippetFolders {
			get {
				return codeSnippetFolders;
			}
			set {
				if (value == null)
					throw new ArgumentNullException();

				codeSnippetFolders = value;
			}
		}
		
		/// <summary>
		/// Releases the unmanaged resources used by the object and optionally releases the managed resources.
		/// </summary>
		/// <param name="disposing">
		/// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources. 
		/// </param>
		/// <remarks>
		/// This method is called by the public <c>Dispose</c> method and the <c>Finalize</c> method. 
		/// <c>Dispose</c> invokes this method with the <paramref name="disposing"/> parameter set to <c>true</c>. 
		/// <c>Finalize</c> invokes this method with <paramref name="disposing"/> set to <c>false</c>.
		/// </remarks>
		protected override void Dispose(bool disposing) {
			if (disposing) {
				for (int index = externalReferences.Count - 1; index >= 0; index--)
					this.RemoveExternalReference(externalReferences[index]);
				externalReferences.Clear();
				sourceProjectContent.Clear();
			}
		}
	
		/// <summary>
		/// Gets the <see cref="ICollection"/> that contains the external reference keys that are currently loaded.
		/// </summary>
		/// <value>The <see cref="ICollection"/> that contains the external reference keys that are currently loaded.</value>
		public ICollection ExternalReferences {
			get {
				return externalReferences;
			}
		}
		
		/// <summary>
		/// Gets the collection of child namespace names for the specified namespace name.
		/// </summary>
		/// <param name="namespaceName">The namespace name for which to search.</param>
		/// <returns>The collection of child namespace names for the specified namespace name.</returns>
		public ICollection GetChildNamespaceNames(string namespaceName) {
			Hashtable names = new Hashtable();
			foreach (string externalReference in externalReferences) {
				IProjectContent projectContent = AssemblyCodeRepository.GetProjectContent(externalReference);
				if (projectContent != null) {
					ICollection projectContentNames = projectContent.GetChildNamespaceNames(namespaceName);
					foreach (string name in projectContentNames)
						names[name] = true;
				}
			}
			if (sourceProjectContent != null) {
				ICollection projectContentNames = sourceProjectContent.GetChildNamespaceNames(namespaceName);
				foreach (string name in projectContentNames)
					names[name] = true;
			}
			return names.Keys;
		}
		
		/// <summary>
		/// Gets an available extension method that targets the specified type.
		/// </summary>
		/// <param name="contextType">The <see cref="IDomType"/> that provides the context of the lookup.</param>
		/// <param name="importedNamespaces">The imported namespaces.</param>
		/// <param name="targetType">The <see cref="IDomType"/> for which to search.</param>
		/// <param name="name">The name of the desired members.</param>
		/// <param name="flags">The <see cref="DomBindingFlags"/> to match.</param>
		/// <returns>An available extension method that targets the specified type.</returns>
		internal IDomMember GetExtensionMethod(IDomType contextType, string[] importedNamespaces, IDomType targetType, string name, DomBindingFlags flags) {
			// Get the type inheritance hierarchies
			IDomType[] contextInheritanceHierarchy = this.GetTypeInheritanceHierarchy(contextType);
			IDomType[] targetInheritanceHierarchy = this.GetTypeInheritanceHierarchyAndImplementedInterfaces(targetType);

			ArrayList members = new ArrayList();
			foreach (string externalReference in externalReferences) {
				IProjectContent projectContent = AssemblyCodeRepository.GetProjectContent(externalReference);
				if (projectContent != null) {
					ICollection projectContentMethods = projectContent.GetExtensionMethods(this, contextInheritanceHierarchy, importedNamespaces, targetInheritanceHierarchy, name, flags);
					foreach (IDomMember member in projectContentMethods)
						return member;
				}
			}
			if (sourceProjectContent != null) {
				ICollection projectContentMethods = sourceProjectContent.GetExtensionMethods(this, contextInheritanceHierarchy, importedNamespaces, targetInheritanceHierarchy, name, flags);
				foreach (IDomMember member in projectContentMethods)
					return member;
			}

			return null;
		}

		/// <summary>
		/// Gets the collection of available extension methods that target the specified type.
		/// </summary>
		/// <param name="contextType">The <see cref="IDomType"/> that provides the context of the lookup.</param>
		/// <param name="importedNamespaces">The imported namespaces.</param>
		/// <param name="targetType">The <see cref="IDomType"/> for which to search.</param>
		/// <param name="name">The name of the desired members.</param>
		/// <param name="flags">The <see cref="DomBindingFlags"/> to match.</param>
		/// <returns>The collection of available extension methods that target the specified type.</returns>
		public IDomMember[] GetExtensionMethods(IDomType contextType, string[] importedNamespaces, IDomType targetType, string name, DomBindingFlags flags) {
			// Get the type inheritance hierarchies
			IDomType[] contextInheritanceHierarchy = this.GetTypeInheritanceHierarchy(contextType);
			IDomType[] targetInheritanceHierarchy = this.GetTypeInheritanceHierarchyAndImplementedInterfaces(targetType);

			ArrayList members = new ArrayList();
			foreach (string externalReference in externalReferences) {
				IProjectContent projectContent = AssemblyCodeRepository.GetProjectContent(externalReference);
				if (projectContent != null) {
					ICollection projectContentMethods = projectContent.GetExtensionMethods(this, contextInheritanceHierarchy, importedNamespaces, targetInheritanceHierarchy, name, flags);
					foreach (IDomMember member in projectContentMethods)
						members.Add(member);
				}
			}
			if (sourceProjectContent != null) {
				ICollection projectContentMethods = sourceProjectContent.GetExtensionMethods(this, contextInheritanceHierarchy, importedNamespaces, targetInheritanceHierarchy, name, flags);
				foreach (IDomMember member in projectContentMethods)
					members.Add(member);
			}

			return (IDomMember[])members.ToArray(typeof(IDomMember));
		}

		/// <summary>
		/// Gets a member in the <see cref="IDomType"/> with the specified name, which includes inherited members.
		/// </summary>
		/// <param name="contextType">The <see cref="IDomType"/> that provides the context of the lookup.</param>
		/// <param name="targetType">The <see cref="IDomType"/> to examine.</param>
		/// <param name="name">The name of the desired member.</param>
		/// <param name="flags">The <see cref="DomBindingFlags"/> to match.</param>
		/// <returns>A member in the <see cref="IDomType"/> with the specified name.</returns>
		public IDomMember GetMember(IDomType contextType, IDomType targetType, string name, DomBindingFlags flags) {
			// Get the type inheritance hierarchies
			IDomType[] targetInheritanceHierarchy = (((flags & DomBindingFlags.DeclaringTypeOnly) == DomBindingFlags.DeclaringTypeOnly) ? new IDomType[] { targetType } : this.GetTypeInheritanceHierarchy(targetType));
			IDomType[] contextInheritanceHierarchy = this.GetTypeInheritanceHierarchy(contextType);
			
			// Flag whether the context type is or is an inheritor of the target type (for "family" members)
			if ((contextInheritanceHierarchy != null) && (contextInheritanceHierarchy.Length > 0)) {
				for (int index = 0; index < contextInheritanceHierarchy.Length; index++) {
					if (
						((targetType.ProjectContent != null) && (contextInheritanceHierarchy[index] == targetType)) ||
						((targetType.ProjectContent == null) && (contextInheritanceHierarchy[index].FullName == targetType.FullName))
						) {
						flags |= DomBindingFlags.ContextIsTargetFamily;
						break;
					}
				}
			}

			// Loop through the types in the inheritance hierarchy...
			foreach (IDomType inheritedType in targetInheritanceHierarchy) {
				// Look for the first matching member in the type
				IDomMember member = inheritedType.GetMember(contextInheritanceHierarchy, name, flags);
				if (member != null)
					return member;
			}
			
			return null;
		}
		
		/// <summary>
		/// Gets all the member overloads in the <see cref="IDomType"/> with the specified name, which includes inherited members.
		/// </summary>
		/// <param name="contextType">The <see cref="IDomType"/> that provides the context of the lookup.</param>
		/// <param name="targetType">The <see cref="IDomType"/> to examine.</param>
		/// <param name="name">The name of the desired members.</param>
		/// <param name="flags">The <see cref="DomBindingFlags"/> to match.</param>
		/// <returns>An <see cref="IDomMember"/> array including all the member overloads in the <see cref="IDomType"/> with the specified name.</returns>
		public IDomMember[] GetMemberOverloads(IDomType contextType, IDomType targetType, string name, DomBindingFlags flags) {
			// Get the type inheritance hierarchies
			IDomType[] targetInheritanceHierarchy = (((flags & DomBindingFlags.DeclaringTypeOnly) == DomBindingFlags.DeclaringTypeOnly) ? new IDomType[] { targetType } : this.GetTypeInheritanceHierarchy(targetType));
			IDomType[] contextInheritanceHierarchy = this.GetTypeInheritanceHierarchy(contextType);

			// Flag whether the context type is or is an inheritor of the target type (for "family" members)
			if ((contextInheritanceHierarchy != null) && (contextInheritanceHierarchy.Length > 0)) {
				for (int index = 0; index < contextInheritanceHierarchy.Length; index++) {
					if (
						((targetType.ProjectContent != null) && (contextInheritanceHierarchy[index] == targetType)) ||
						((targetType.ProjectContent == null) && (contextInheritanceHierarchy[index].FullName == targetType.FullName))
						) {
						flags |= DomBindingFlags.ContextIsTargetFamily;
						break;
					}
				}
			}
			
			// Loop through the types in the inheritance hierarchy...
			Hashtable members = new Hashtable();
			for (int index = targetInheritanceHierarchy.Length - 1; index >= 0; index--) {
				IDomType inheritedType = targetInheritanceHierarchy[index];

				// Add the members declared in the type
				IDomMember[] memberArray = inheritedType.GetMembers(contextInheritanceHierarchy, name, flags);
				if (memberArray != null) {
					foreach (IDomMember member in memberArray) {
						StringBuilder hashKey = new StringBuilder();
						if (member.Parameters != null) {
							foreach (IDomParameter parameter in member.Parameters) {
								hashKey.Append(",");
								IDomType parameterType = (parameter.ParameterType != null ? parameter.ParameterType.Resolve(this) : null);
								if (parameterType != null)
									hashKey.Append(parameterType.FullName);
								else if (parameter.ParameterType != null)
									hashKey.Append(parameter.ParameterType.RawFullName);
								else
									hashKey.Append("?");
							}
						}
						members[hashKey.ToString()] = member;
					}
				}
			}

			// Build the result array
			IDomMember[] result = new IDomMember[members.Values.Count];
			members.Values.CopyTo(result, 0);
			return result;
		}

		/// <summary>
		/// Gets all the members in the <see cref="IDomType"/>, which includes inherited members.
		/// </summary>
		/// <param name="targetType">The <see cref="IDomType"/> to examine.</param>
		/// <returns>An <see cref="IDomMember"/> array including all the members in the <see cref="IDomType"/>.</returns>
		public IDomMember[] GetMembers(IDomType targetType) {
			// Get the type inheritance hierarchies
			IDomType[] targetInheritanceHierarchy = this.GetTypeInheritanceHierarchy(targetType);

			// Loop through the types in the inheritance hierarchy...
			ArrayList members = new ArrayList();
			for (int index = targetInheritanceHierarchy.Length - 1; index >= 0; index--) {
				IDomType inheritedType = targetInheritanceHierarchy[index];

				// Add the members declared in the type
				IDomMember[] memberArray = inheritedType.GetMembers();
				if (memberArray != null)
					members.AddRange(memberArray);
			}

			return (IDomMember[])members.ToArray(typeof(IDomMember));
		}
		
		/// <summary>
		/// Gets all the members in the <see cref="IDomType"/> with the specified name, which includes inherited members.
		/// </summary>
		/// <param name="contextType">The <see cref="IDomType"/> that provides the context of the lookup.</param>
		/// <param name="targetType">The <see cref="IDomType"/> to examine.</param>
		/// <param name="name">The name of the desired members.</param>
		/// <param name="flags">The <see cref="DomBindingFlags"/> to match.</param>
		/// <returns>An <see cref="IDomMember"/> array including all the members in the <see cref="IDomType"/> with the specified name.</returns>
		public IDomMember[] GetMembers(IDomType contextType, IDomType targetType, string name, DomBindingFlags flags) {
			// Get the type inheritance hierarchies
			IDomType[] targetInheritanceHierarchy = (((flags & DomBindingFlags.DeclaringTypeOnly) == DomBindingFlags.DeclaringTypeOnly) ? new IDomType[] { targetType } : this.GetTypeInheritanceHierarchy(targetType));
			IDomType[] contextInheritanceHierarchy = this.GetTypeInheritanceHierarchy(contextType);

			// Flag whether the context type is or is an inheritor of the target type (for "family" members)
			if ((contextInheritanceHierarchy != null) && (contextInheritanceHierarchy.Length > 0)) {
				for (int index = 0; index < contextInheritanceHierarchy.Length; index++) {
					if (
						((targetType.ProjectContent != null) && (contextInheritanceHierarchy[index] == targetType)) ||
						((targetType.ProjectContent == null) && (contextInheritanceHierarchy[index].FullName == targetType.FullName))
						) {
						flags |= DomBindingFlags.ContextIsTargetFamily;
						break;
					}
				}
			}
			
			// Loop through the types in the inheritance hierarchy...
			ArrayList members = new ArrayList();
			for (int index = targetInheritanceHierarchy.Length - 1; index >= 0; index--) {
				IDomType inheritedType = targetInheritanceHierarchy[index];

				// Add the members declared in the type
				IDomMember[] memberArray = inheritedType.GetMembers(contextInheritanceHierarchy, name, flags);
				if (memberArray != null)
					members.AddRange(memberArray);
			}

			return (IDomMember[])members.ToArray(typeof(IDomMember));
		}

		/// <summary>
		/// Gets the <see cref="IDomType"/> for the specified native type full name.
		/// </summary>
		/// <param name="typeFullName">The full type name.</param>
		/// <returns>The <see cref="IDomType"/> for the specified native type.</returns>
		/// <remarks>
		/// This method will only search <c>mscorlib</c> for the type.
		/// </remarks>
		public IDomType GetNativeType(string typeFullName) {
			IProjectContent projectContent = AssemblyCodeRepository.GetProjectContentWithPartialName("mscorlib");
			if (projectContent != null)
				return projectContent.GetType(null, typeFullName, DomBindingFlags.Default);
			return null;
		}
		
		/// <summary>
		/// Gets the collection of nested types within the specified type.
		/// </summary>
		/// <param name="contextType">The <see cref="IDomType"/> that provides the context of the lookup.</param>
		/// <param name="typeFullName">The full name of the type for which to search.</param>
		/// <param name="flags">The <see cref="DomBindingFlags"/> to match.</param>
		/// <returns>The collection of nested types within the specified type.</returns>
		public ICollection GetNestedTypes(IDomType contextType, string typeFullName, DomBindingFlags flags) {
			// Get the type inheritance hierarchy
			IDomType[] contextInheritanceHierarchy = this.GetTypeInheritanceHierarchy(contextType);
			
			ArrayList types = new ArrayList();
			foreach (string externalReference in externalReferences) {
				IProjectContent projectContent = AssemblyCodeRepository.GetProjectContent(externalReference);
				if (projectContent != null) {
					ICollection nestedTypes = projectContent.GetNestedTypes(contextInheritanceHierarchy, typeFullName, flags);
					foreach (IDomType type in nestedTypes)
						types.Add(type);
				}
			}
			if (sourceProjectContent != null) {
				ICollection nestedTypes = sourceProjectContent.GetNestedTypes(contextInheritanceHierarchy, typeFullName, flags);
				foreach (IDomType type in nestedTypes)
					types.Add(type);
			}
			return types;
		}

		/// <summary>
		/// Returns the quick info for the specified <see cref="DotNetContext"/>. 
		/// </summary>
		/// <param name="language">The <see cref="DotNetLanguage"/> to use for quick info formatting.</param>
		/// <param name="context">The <see cref="DotNetContext"/> for which quick info markup will be returned.</param>
		/// <returns>The quick info for the specified <see cref="DotNetContext"/>.</returns>
		public string GetQuickInfo(DotNetLanguage language, DotNetContext context) {
			// Set the appropriate tooltip text
			if ((context != null) && (context.TargetItem != null)) {
				switch (context.Type) {
					case DotNetContextType.AsType:
					case DotNetContextType.BaseMemberAccess:
					case DotNetContextType.IsTypeOfType:
					case DotNetContextType.NamespaceTypeOrMember:
					case DotNetContextType.NewObjectDeclaration:
					case DotNetContextType.ThisMemberAccess:
					case DotNetContextType.TryCastType:
					case DotNetContextType.TypeOfType:
						switch (context.TargetItem.Type) {
							case DotNetContextItemType.Constant:
							case DotNetContextItemType.Variable:
								return context.ProjectResolver.GetQuickInfoForLocalVariable(language, context, (IDomType)context.TargetItem.ResolvedInfo, context.TargetItem.Text, true);
							case DotNetContextItemType.Member: {
								// Get the type of the item that calls the member
								IDomType type = null;
								if (context.Items.Length > 1) {
									type = context.Items[context.Items.Length - 2].ResolvedInfo as IDomType;
									if (type == null)
										type = context.ProjectResolver.ConstructAndResolveContextItemMemberReturnType(context, context.Items.Length - 2);
								}
								return context.ProjectResolver.GetQuickInfoForMember(language, context, 
									type, (IDomMember)context.TargetItem.ResolvedInfo, -1, true);
							}
							case DotNetContextItemType.Namespace:
							case DotNetContextItemType.NamespaceAlias:
								return context.ProjectResolver.GetQuickInfoForNamespace(language, context.TargetItem.ResolvedInfo.ToString(), true);
							case DotNetContextItemType.Parameter:
								return context.ProjectResolver.GetQuickInfoForParameter(language, context, (IDomType)context.TargetItem.ResolvedInfo, context.TargetItem.Text, true);
							case DotNetContextItemType.ArrayItem:
							case DotNetContextItemType.Type:
								return context.ProjectResolver.GetQuickInfoForType(language, (IDomType)context.TargetItem.ResolvedInfo, true);
						}
						return null;
					case DotNetContextType.DecimalIntegerLiteral:
						return context.ProjectResolver.GetQuickInfoForDecimalIntegerLiteral(language, (string)context.TargetItem.Text, false);
					case DotNetContextType.HexadecimalIntegerLiteral:
						return context.ProjectResolver.GetQuickInfoForDecimalIntegerLiteral(language, (string)context.TargetItem.Text, true);
					case DotNetContextType.BaseAccess:
					case DotNetContextType.NativeType:
					case DotNetContextType.ThisAccess:
						return context.ProjectResolver.GetQuickInfoForType(language, (IDomType)context.TargetItem.ResolvedInfo, true);
					case DotNetContextType.UsingDeclaration:
						return context.ProjectResolver.GetQuickInfoForNamespace(language, context.TargetItem.ResolvedInfo.ToString(), true);
				}
			}
			return null;
		}
		
		/// <summary>
		/// Gets the collection of standard modules within the specified namespace name.
		/// </summary>
		/// <param name="contextType">The <see cref="IDomType"/> that provides the context of the lookup.</param>
		/// <param name="namespaceName">The namespace name for which to search.</param>
		/// <param name="flags">The <see cref="DomBindingFlags"/> to match.</param>
		/// <returns>The collection of standard modules within the specified namespace name.</returns>
		public ICollection GetStandardModules(IDomType contextType, string namespaceName, DomBindingFlags flags) {
			// Get the type inheritance hierarchy
			IDomType[] contextInheritanceHierarchy = this.GetTypeInheritanceHierarchy(contextType);

			ArrayList types = new ArrayList();
			foreach (string externalReference in externalReferences) {
				IProjectContent projectContent = AssemblyCodeRepository.GetProjectContent(externalReference);
				if (projectContent != null) {
					ICollection projectContentTypes = projectContent.GetStandardModules(contextInheritanceHierarchy, namespaceName, flags);
					foreach (IDomType type in projectContentTypes)
						types.Add(type);
				}
			}
			if (sourceProjectContent != null) {
				ICollection projectContentTypes = sourceProjectContent.GetStandardModules(contextInheritanceHierarchy, namespaceName, flags);
				foreach (IDomType type in projectContentTypes)
					types.Add(type);
			}
			return types;
		}
		
		/// <summary>
		/// Gets the <see cref="IDomType"/> for the specified type full name within the imported namespaces.
		/// </summary>
		/// <param name="contextType">The <see cref="IDomType"/> that provides the context of the lookup.</param>
		/// <param name="importedNamespaces">The imported namespaces.</param>
		/// <param name="typeFullName">The full type name.</param>
		/// <param name="flags">The <see cref="DomBindingFlags"/> to match.</param>
		/// <returns>The <see cref="IDomType"/> for the specified type full name within the imported namespaces.</returns>
		/// <remarks>
		/// The global namespace doesn't need to be passed in the imported namespace list since it will be automatically included.
		/// </remarks>
		public IDomType GetType(IDomType contextType, string[] importedNamespaces, string typeFullName, DomBindingFlags flags) {
			// If there is a context type, first check for a nested type
			if (contextType != null) {
				// 5/21/2009 - Added to fix scenario where nested types weren't being located from sibling nested types
				IDomType contextTypeTemp = contextType;
				while (contextTypeTemp != null) {
					IProjectContent projectContent = (contextTypeTemp.ProjectContent != null ? contextTypeTemp.ProjectContent : sourceProjectContent);
					if (projectContent != null) {
						IDomType type = projectContent.GetType(null, contextTypeTemp.FullName + "." + typeFullName, flags);
						if (type != null)
							return type;
					}

					// If the context type is a nested type, move up a level
					contextTypeTemp = contextTypeTemp.DeclaringType as IDomType;
				}
			}

			// Get the type inheritance hierarchy
			IDomType[] contextInheritanceHierarchy = this.GetTypeInheritanceHierarchy(contextType, typeFullName);

			// Check for a fully-qualified type name first (same as in global namespace)
			if (sourceProjectContent != null) {
				IDomType type = sourceProjectContent.GetType(contextInheritanceHierarchy, typeFullName, flags);
				if (type != null)
					return type;
			}
			foreach (string externalReference in externalReferences) {
				IProjectContent projectContent = AssemblyCodeRepository.GetProjectContent(externalReference);
				if (projectContent != null) {
					IDomType type = projectContent.GetType(contextInheritanceHierarchy, typeFullName, flags);
					if (type != null)
						return type;
				}
			}

			// Check for a partially-qualified type name 
			if (importedNamespaces != null) {
				foreach (string importedNamespace in importedNamespaces) {
					if (sourceProjectContent != null) {
						IDomType type = sourceProjectContent.GetType(contextInheritanceHierarchy, importedNamespace + "." + typeFullName, flags);
						if (type != null)
							return type;
					}
					foreach (string externalReference in externalReferences) {
						IProjectContent projectContent = AssemblyCodeRepository.GetProjectContent(externalReference);
						if (projectContent != null) {
							IDomType type = projectContent.GetType(contextInheritanceHierarchy, importedNamespace + "." + typeFullName, flags);
							if (type != null)
								return type;
						}
					}
				}
			}

			return null;
		}
		
		/// <summary>
		/// Gets the <see cref="IDomType"/> for the specified type full name using context information.
		/// </summary>
		/// <param name="context">The <see cref="DotNetContext"/> to use.</param>
		/// <param name="fullTypeName">The full type name.</param>
		/// <param name="flags">The <see cref="DomBindingFlags"/> to match.</param>
		/// <returns>The <see cref="IDomType"/> for the specified type full name using context information.</returns>
		/// <remarks>
		/// The global namespace doesn't need to be passed in the imported namespace list since it will be automatically included.
		/// </remarks>
		public IDomType GetType(DotNetContext context, string fullTypeName, DomBindingFlags flags) {
			if ((context == null) || (fullTypeName == null))
				return null;

			bool ignoreCase = ((flags & DomBindingFlags.IgnoreCase) == DomBindingFlags.IgnoreCase);

			// Check for a fully-qualified type in the compilation unit itself
			foreach (IDomType compilationUnitType in context.CompilationUnit.Types) {
				if (String.Compare(compilationUnitType.FullName, fullTypeName, ignoreCase) == 0)
					return compilationUnitType;
			}

			// Check for a partially-qualified type in the compilation unit itself
			IDomType typeDeclarationNode = context.TypeDeclarationNode;
			if ((typeDeclarationNode != null) && (typeDeclarationNode.Name != null)) {
				string namespaceName = typeDeclarationNode.FullName;
				if (namespaceName.EndsWith(typeDeclarationNode.Name)) {
					// Check for nested types 
					foreach (IDomType compilationUnitType in context.CompilationUnit.Types) {
						if (String.Compare(compilationUnitType.FullName, namespaceName + "." + fullTypeName, ignoreCase) == 0)
							return compilationUnitType.Resolve(this);
					}
				}

				// Loop up parent types...
				typeDeclarationNode = this.GetParentType(typeDeclarationNode.Resolve(this), namespaceName, fullTypeName, ignoreCase);
				if (typeDeclarationNode != null)
					return typeDeclarationNode;
			}

			// Check for a type in the project references...
			return this.GetType(context.TypeDeclarationNode, context.ImportedNamespaces, fullTypeName, flags);
		}
		
		/// <summary>
		/// Returns the inheritance hierarchy for the specified <see cref="IDomType"/>.
		/// </summary>
		/// <param name="type">The <see cref="IDomType"/> to examine.</param>
		/// <returns>The inheritance hierarchy for the specified <see cref="IDomType"/>.</returns>
		public IDomType[] GetTypeInheritanceHierarchy(IDomType type) {
			return this.GetTypeInheritanceHierarchy(type, null);
		}
		
		/// <summary>
		/// Returns an array containing the inheritance hierarchy of the <see cref="IDomType"/> and its implemented interfaces.
		/// </summary>
		/// <param name="type">The <see cref="IDomType"/> to examine.</param>
		/// <returns>An array containing the inheritance hierarchy of the <see cref="IDomType"/> and its implemented interfaces.</returns>
		internal IDomType[] GetTypeInheritanceHierarchyAndImplementedInterfaces(IDomType type) {
			// Get the inheritance hierarchy
			ArrayList types = new ArrayList();
			IDomType[] typeHierarchy = this.GetTypeInheritanceHierarchy(type);
			types.AddRange(typeHierarchy);

			// Loop through each item
			foreach (IDomType typeItem in typeHierarchy) {
				// Add implemented interfaces
				IDomTypeReference[] interfaceReferences = typeItem.GetInterfaces();
				if (interfaceReferences != null) {
					foreach (IDomTypeReference interfaceReference in interfaceReferences) {
						// TODO: There seems to be a bug here where array interfaces with T as generic params are being passed back
						// ((!interfaceType.IsGenericType) || (!interfaceType.IsGenericTypeDefinition)) && 
						IDomType interfaceType = this.ConstructAndResolve(interfaceReference, type);
						if ((interfaceType != null) && (!types.Contains(interfaceType))) {
							IDomType[] interfaceTypeHierarchy = this.GetTypeInheritanceHierarchy(interfaceType);
							if (interfaceTypeHierarchy != null) {
								foreach (IDomType interfaceTypeItem in interfaceTypeHierarchy) {
									// Try and construct any generic types
									IDomType constructedType = interfaceTypeItem;
									if (constructedType.IsGenericTypeDefinition)
										constructedType = this.ConstructAndResolve(constructedType, interfaceType);

									if (!types.Contains(constructedType))
										types.Add(constructedType);
								}
							}
						}
					}				
				}
			}

			return (IDomType[])types.ToArray(typeof(IDomType));
		}

		/// <summary>
		/// Gets the collection of types within the specified namespace name.
		/// </summary>
		/// <param name="contextType">The <see cref="IDomType"/> that provides the context of the lookup.</param>
		/// <param name="namespaceName">The namespace name for which to search.</param>
		/// <param name="flags">The <see cref="DomBindingFlags"/> to match.</param>
		/// <returns>The collection of types within the specified namespace name.</returns>
		public ICollection GetTypes(IDomType contextType, string namespaceName, DomBindingFlags flags) {
			// Get the type inheritance hierarchy
			IDomType[] contextInheritanceHierarchy = this.GetTypeInheritanceHierarchy(contextType);

			ArrayList types = new ArrayList();
			foreach (string externalReference in externalReferences) {
				IProjectContent projectContent = AssemblyCodeRepository.GetProjectContent(externalReference);
				if (projectContent != null) {
					ICollection projectContentTypes = projectContent.GetTypes(contextInheritanceHierarchy, namespaceName, flags);
					foreach (IDomType type in projectContentTypes)
						types.Add(type);
				}
			}
			if (sourceProjectContent != null) {
				ICollection projectContentTypes = sourceProjectContent.GetTypes(contextInheritanceHierarchy, namespaceName, flags);
				foreach (IDomType type in projectContentTypes)
					types.Add(type);
			}
			return types;
		}
		
		/// <summary>
		/// Returns whether the specified namespace name is defined.
		/// </summary>
		/// <param name="namespaceName">The namespace name for which to search.</param>
		/// <returns>
		/// <c>true</c> if the specified namespace name is defined; otherwise, <c>false</c>.
		/// </returns>
		public bool HasNamespace(string namespaceName) {
			if (sourceProjectContent != null) {
				if (sourceProjectContent.HasNamespace(namespaceName))
					return true;
			}
			foreach (string externalReference in externalReferences) {
				IProjectContent projectContent = AssemblyCodeRepository.GetProjectContent(externalReference);
				if (projectContent != null) {
					if (projectContent.HasNamespace(namespaceName))
						return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Prunes the cache folder of data files that are no longer valid.
		/// </summary>
		/// <remarks>
		/// Call this method upon the shutdown of your application and after the project resolver has been disposed.
		/// </remarks>
		public void PruneCache() {
			if (cachePath == null)
				return;

			string[] filenames = Directory.GetFiles(cachePath, "*.Reflection.dat");
			foreach (string filename in filenames) {
				AssemblyProjectContent.CacheHeader header = AssemblyProjectContent.LoadHeaderFromCache(filename);
				if (!header.IsValid())
					File.Delete(filename);
			}

			filenames = Directory.GetFiles(cachePath, "*.Documentation.dat");
			foreach (string filename in filenames) {
				AssemblyDocumentation.CacheHeader header = AssemblyDocumentation.LoadHeaderFromCache(filename);
				if (!header.IsValid())
					File.Delete(filename);
			}
		}

		/// <summary>
		/// Removes an assembly as an external reference.
		/// </summary>
		/// <param name="assemblyName">The full name of the assembly to remove.</param>
		/// <remarks>
		/// <c>true</c> if an assembly was added successfully; otherwise, <c>false</c>.
		/// </remarks>
		public bool RemoveExternalReference(string assemblyName) {
			if ((assemblyName == null) || (assemblyName.Length == 0))
				return false;

			// Remove from the external references
			if (externalReferences.Contains(assemblyName))
				externalReferences.Remove(assemblyName);

			// Load into the respository
			AssemblyCodeRepository.Remove(assemblyName);
			return true;
		}

		/// <summary>
		/// Gets the <see cref="SourceProjectContent"/> that contains the source code project content.
		/// </summary>
		/// <value>The <see cref="SourceProjectContent"/> that contains the source code project content.</value>
		public SourceProjectContent SourceProjectContent {
			get {
				return sourceProjectContent;
			}
		}
		
	}
}
