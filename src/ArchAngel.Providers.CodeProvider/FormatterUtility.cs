using System;
using System.Collections.Generic;
using System.Text;
using ActiproSoftware.SyntaxEditor;
using ActiproSoftware.SyntaxEditor.Addons.CSharp;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider
{
	internal static class FormatterUtility
	{
		private static readonly Dictionary<OperatorType, string> OperatorNames = new Dictionary<OperatorType, string>();
		private static readonly Dictionary<string, string> LongPrimitiveNameToShortName =
			new Dictionary<string, string>();

		static FormatterUtility()
		{
			LongPrimitiveNameToShortName.Add("System.Int32", "int");
			LongPrimitiveNameToShortName.Add("System.String", "string");
			LongPrimitiveNameToShortName.Add("System.Int16", "short");
			LongPrimitiveNameToShortName.Add("System.Int64", "long");
			LongPrimitiveNameToShortName.Add("System.Single", "float");
			LongPrimitiveNameToShortName.Add("System.Double", "double");
			LongPrimitiveNameToShortName.Add("System.Boolean", "bool");
			LongPrimitiveNameToShortName.Add("System.Char", "char");
			LongPrimitiveNameToShortName.Add("System.Void", "void");
			LongPrimitiveNameToShortName.Add("System.Byte", "byte");
			LongPrimitiveNameToShortName.Add("System.Object", "object");
			LongPrimitiveNameToShortName.Add("System.Decimal", "decimal");
			// TODO: Find correspondent enum value for new ActiPro version
			//LongPrimitiveNameToShortName.Add(TypeReference.AnonymousTypeName, "var");


			OperatorNames.Add(OperatorType.Addition, "+");
			OperatorNames.Add(OperatorType.AddressOf, "&");
			OperatorNames.Add(OperatorType.BitwiseAnd, "&");
			OperatorNames.Add(OperatorType.BitwiseOr, "|");
			OperatorNames.Add(OperatorType.ConditionalAnd, "&&");
			OperatorNames.Add(OperatorType.ConditionalOr, "||");
			OperatorNames.Add(OperatorType.Division, "/");
			OperatorNames.Add(OperatorType.Equality, "==");
			OperatorNames.Add(OperatorType.ExclusiveOr, "^");
			OperatorNames.Add(OperatorType.Explicit, "explicit");
			OperatorNames.Add(OperatorType.False, "false");
			OperatorNames.Add(OperatorType.GreaterThan, ">");
			OperatorNames.Add(OperatorType.GreaterThanOrEqual, ">=");
			OperatorNames.Add(OperatorType.Implicit, "implicit");
			OperatorNames.Add(OperatorType.Inequality, "!=");
			OperatorNames.Add(OperatorType.LeftShift, "<<");
			OperatorNames.Add(OperatorType.LessThan, "<");
			OperatorNames.Add(OperatorType.LessThanOrEqual, "<=");
			OperatorNames.Add(OperatorType.Modulus, "%");
			OperatorNames.Add(OperatorType.Multiply, "*");
			OperatorNames.Add(OperatorType.Negation, "!");
			OperatorNames.Add(OperatorType.NullCoalescing, "??");
			OperatorNames.Add(OperatorType.OnesComplement, "~");
			OperatorNames.Add(OperatorType.PointerIndirection, "->");
			OperatorNames.Add(OperatorType.PostDecrement, "--");
			OperatorNames.Add(OperatorType.PostIncrement, "++");
			OperatorNames.Add(OperatorType.PreDecrement, "--");
			OperatorNames.Add(OperatorType.PreIncrement, "++");
			OperatorNames.Add(OperatorType.RightShift, ">>");
			OperatorNames.Add(OperatorType.Subtraction, "-");
			OperatorNames.Add(OperatorType.True, "true");
		}

		public static string GetOperatorName(OperatorType type)
		{
			string name;
			if (OperatorNames.TryGetValue(type, out name))
				return name;

			throw new Exception("Unsupported operator declaration: " + type);
		}

		/// <summary>
		/// This version of GetModifiersFromEnum checks to see if the internal keyword was actually
		/// in the user's code, and if it wasn't it removes it from the list of modifiers returned.
		/// </summary>
		/// <param name="typeDef">The type definition we are getting the list of modifiers for.</param>
		/// <param name="doc">The document that holds the original code.</param>
		/// <returns>A list of the modifiers on the given type declaration, in string form.</returns>
		internal static IEnumerable<string> GetModifiersFromEnumCheckInternal(TypeMemberDeclaration typeDef, Document doc)
		{
			Modifiers modifiers = typeDef.Modifiers;

			if ((modifiers & Modifiers.Assembly) != 0)
			{
				// Search for the internal modifier in the document.
				IToken startToken = doc.Tokens.GetTokenAtOffset(typeDef.StartOffset);
				IToken endToken = doc.Tokens.GetTokenAtOffset(typeDef.EndOffset);

				TokenStream stream = doc.GetTokenStream(startToken);
				while (startToken != endToken && startToken != null)
				{
					if (startToken.ID == CSharpTokenID.Internal)
					{
						return GetModifiersFromEnum(modifiers);
					}
					startToken = stream.Read();
				}

				// Remove the internal modifier 
				modifiers = (modifiers & ~Modifiers.Assembly);
			}
			IEnumerable<string> mods = GetModifiersFromEnum(modifiers);

			return mods;
		}

		internal static List<string> GetModifiersFromEnum(ParameterModifiers modifiers)
		{
			List<string> modifierStrings = new List<string>();

			if ((modifiers & ParameterModifiers.Out) != 0)
			{
				modifierStrings.Add("out");
			}
			if ((modifiers & ParameterModifiers.ParameterArray) != 0)
			{
				modifierStrings.Add("params");
			}
			if ((modifiers & ParameterModifiers.Ref) != 0)
			{
				modifierStrings.Add("ref");
			}

			return modifierStrings;
		}

		internal static IEnumerable<string> GetModifiersFromEnum(Modifiers modifiers)
		{
			List<string> modifierStrings = new List<string>();

			if ((modifiers & Modifiers.Private) != 0)
			{
				modifierStrings.Add("private");
			}
			if ((modifiers & Modifiers.Public) != 0)
			{
				modifierStrings.Add("public");
			}
			if ((modifiers & Modifiers.Family) != 0)
			{
				modifierStrings.Add("protected");
			}
			if ((modifiers & Modifiers.Assembly) != 0)
			{
				modifierStrings.Add("internal");
			}
			if ((modifiers & Modifiers.Abstract) != 0)
			{
				modifierStrings.Add("abstract");
			}
			if ((modifiers & Modifiers.Const) != 0)
			{
				modifierStrings.Add("const");
			}
			if ((modifiers & Modifiers.Extern) != 0)
			{
				modifierStrings.Add("extern");
			}
			//if ((modifiers & Modifiers.FamilyOrAssembly) != 0)
			//{
			//    modifierStrings.Add("protected internal");
			//}
			if ((modifiers & Modifiers.Final) != 0)
			{
				modifierStrings.Add("sealed");
			}
			if ((modifiers & Modifiers.New) != 0)
			{
				modifierStrings.Add("new");
			}
			if ((modifiers & Modifiers.Override) != 0)
			{
				modifierStrings.Add("override");
			}
			if ((modifiers & Modifiers.Partial) != 0)
			{
				modifierStrings.Add("partial");
			}
			if ((modifiers & Modifiers.ReadOnly) != 0)
			{
				modifierStrings.Add("readonly");
			}
			if ((modifiers & Modifiers.Static) != 0)
			{
				modifierStrings.Add("static");
			}
			if ((modifiers & Modifiers.Unsafe) != 0)
			{
				modifierStrings.Add("unsafe");
			}
			if ((modifiers & Modifiers.Virtual) != 0)
			{
				modifierStrings.Add("virtual");
			}
			if ((modifiers & Modifiers.Volatile) != 0)
			{
				modifierStrings.Add("volatile");
			}

			return modifierStrings;
		}

		internal static string FormatDataType(VariableDeclarator varDec, Document document, Controller controller)
		{
			if (varDec.IsImplicitlyTyped)
				return "var";

			return GetDataTypeFromTypeReference(varDec.ReturnType, document, controller).ToString();
		}

		internal static string FormatDataType(TypeReference typeRef, Document document, Controller controller)
		{
			return GetDataTypeFromTypeReference(typeRef, document, controller).ToString();
		}

		internal static DataType GetDataTypeFromTypeReference(TypeReference typeRef, Document document, Controller controller)
		{
			if (typeRef == null)
				throw new ParserException("Typeref cannot be null");
			DataType t = new DataType(controller, typeRef.Name);

			string originalName = document.GetSubstring(typeRef.TextRange);

			if (originalName == "var")
			{
				t.Name = "var";
			}
			else if (t.Name == "System.Object" &&
				(originalName.StartsWith("object") == false && originalName.StartsWith("Object") == false)
				&& originalName.StartsWith("System.Object") == false)
			{
				// Hack for problem where var type references aren't loaded properly
				t.Name = "var";
			}
			else
			{
				// TODO: Find correspondent enum value for new ActiPro version
				//if (t.Name == TypeReference.AnonymousTypeName)
				//{
				//	t.Name = "var";
				//}
				//else if (LongPrimitiveNameToShortName.ContainsKey(t.Name))
				//{
					t.Name = originalName;
				//}
			}

			foreach (IAstNode node in typeRef.GenericTypeArguments)
			{
				if (node is TypeReference)
				{
					t.GenericParameters.Add(GetDataTypeFromTypeReference(node as TypeReference, document, controller));
				}
			}

			if (typeRef.ArrayRanks != null && typeRef.ArrayRanks.Length > 0)
			{
				StringBuilder sb = new StringBuilder();

				for (int i = 0; i < typeRef.ArrayRanks.Length; i++)
				{
					sb.Append("[");
					sb.Append(new string(',', typeRef.ArrayRanks[i] - 1));
					sb.Append("]");
				}

				t.Name += sb.ToString();
			}

			return t;
		}

		internal static string FormatParameter(ParameterDeclaration param, Document document, Controller controller)
		{
			StringBuilder sb = new StringBuilder();
			foreach (string mod in GetModifiersFromEnum(param.Modifiers))
			{
				sb.Append(mod);
			}
			sb.Append(GetDataTypeFromTypeReference(param.ParameterType, document, controller)).Append(" ");
			sb.Append(param.Name);

			return sb.ToString();
		}
	}
}