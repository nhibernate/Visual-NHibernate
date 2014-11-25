using System;
using System.Collections;
using System.Diagnostics;
using System.Text;
using ActiproSoftware.Products.SyntaxEditor.Addons.DotNet;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;
using ActiproSoftware.SyntaxEditor.Addons;

namespace ActiproSoftware.SyntaxEditor.Addons.CSharp {

	#region Token IDs
	/// <summary>
	/// Contains the token IDs for the <c>C#</c> language.
	/// </summary>
	public class CSharpTokenID {

		/// <summary>
		/// Returns the string-based key for the specified token ID.
		/// </summary>
		/// <param name="id">The token ID to examine.</param>
		public static string GetTokenKey(int id) {
			System.Reflection.FieldInfo[] fields = typeof(CSharpTokenID).GetFields();
			foreach (System.Reflection.FieldInfo field in fields) {
				if ((field.IsStatic) && (field.IsLiteral) && (id.Equals(field.GetValue(null))))
					return field.Name;
			}
			return null;
		}

		/// <summary>
		/// The Invalid token ID.
		/// </summary>
		public const int Invalid = 0;

		/// <summary>
		/// The DocumentEnd token ID.
		/// </summary>
		public const int DocumentEnd = 1;

		/// <summary>
		/// The LanguageTransitionStart token ID.
		/// </summary>
		public const int LanguageTransitionStart = 2;

		/// <summary>
		/// The LanguageTransitionEnd token ID.
		/// </summary>
		public const int LanguageTransitionEnd = 3;

		/// <summary>
		/// The Whitespace token ID.
		/// </summary>
		public const int Whitespace = 4;

		/// <summary>
		/// The LineTerminator token ID.
		/// </summary>
		public const int LineTerminator = 5;

		/// <summary>
		/// The SingleLineComment token ID.
		/// </summary>
		public const int SingleLineComment = 6;

		/// <summary>
		/// The MultiLineComment token ID.
		/// </summary>
		public const int MultiLineComment = 7;

		/// <summary>
		/// The DocumentationCommentDelimiter token ID.
		/// </summary>
		public const int DocumentationCommentDelimiter = 8;

		/// <summary>
		/// The DocumentationCommentText token ID.
		/// </summary>
		public const int DocumentationCommentText = 9;

		/// <summary>
		/// The DocumentationCommentTag token ID.
		/// </summary>
		public const int DocumentationCommentTag = 10;

		/// <summary>
		/// The DecimalIntegerLiteral token ID.
		/// </summary>
		public const int DecimalIntegerLiteral = 11;

		/// <summary>
		/// The HexadecimalIntegerLiteral token ID.
		/// </summary>
		public const int HexadecimalIntegerLiteral = 12;

		/// <summary>
		/// The RealLiteral token ID.
		/// </summary>
		public const int RealLiteral = 13;

		/// <summary>
		/// The CharacterLiteral token ID.
		/// </summary>
		public const int CharacterLiteral = 14;

		/// <summary>
		/// The StringLiteral token ID.
		/// </summary>
		public const int StringLiteral = 15;

		/// <summary>
		/// The VerbatimStringLiteral token ID.
		/// </summary>
		public const int VerbatimStringLiteral = 16;

		/// <summary>
		/// The Identifier token ID.
		/// </summary>
		public const int Identifier = 17;

		/// <summary>
		/// The ContextualKeywordStart token ID.
		/// </summary>
		public const int ContextualKeywordStart = 18;

		/// <summary>
		/// The Ascending token ID.
		/// </summary>
		public const int Ascending = 19;

		/// <summary>
		/// The By token ID.
		/// </summary>
		public const int By = 20;

		/// <summary>
		/// The Descending token ID.
		/// </summary>
		public const int Descending = 21;

		/// <summary>
		/// The Equals token ID.
		/// </summary>
		public const int Equals = 22;

		/// <summary>
		/// The From token ID.
		/// </summary>
		public const int From = 23;

		/// <summary>
		/// The Group token ID.
		/// </summary>
		public const int Group = 24;

		/// <summary>
		/// The Into token ID.
		/// </summary>
		public const int Into = 25;

		/// <summary>
		/// The Join token ID.
		/// </summary>
		public const int Join = 26;

		/// <summary>
		/// The Let token ID.
		/// </summary>
		public const int Let = 27;

		/// <summary>
		/// The On token ID.
		/// </summary>
		public const int On = 28;

		/// <summary>
		/// The OrderBy token ID.
		/// </summary>
		public const int OrderBy = 29;

		/// <summary>
		/// The Select token ID.
		/// </summary>
		public const int Select = 30;

		/// <summary>
		/// The Where token ID.
		/// </summary>
		public const int Where = 31;

		/// <summary>
		/// The Var token ID.
		/// </summary>
		public const int Var = 32;

		/// <summary>
		/// The ContextualKeywordEnd token ID.
		/// </summary>
		public const int ContextualKeywordEnd = 33;

		/// <summary>
		/// The KeywordStart token ID.
		/// </summary>
		public const int KeywordStart = 34;

		/// <summary>
		/// The Abstract token ID.
		/// </summary>
		public const int Abstract = 35;

		/// <summary>
		/// The Add token ID.
		/// </summary>
		public const int Add = 36;

		/// <summary>
		/// The As token ID.
		/// </summary>
		public const int As = 37;

		/// <summary>
		/// The Base token ID.
		/// </summary>
		public const int Base = 38;

		/// <summary>
		/// The Bool token ID.
		/// </summary>
		public const int Bool = 39;

		/// <summary>
		/// The Break token ID.
		/// </summary>
		public const int Break = 40;

		/// <summary>
		/// The Byte token ID.
		/// </summary>
		public const int Byte = 41;

		/// <summary>
		/// The Case token ID.
		/// </summary>
		public const int Case = 42;

		/// <summary>
		/// The Catch token ID.
		/// </summary>
		public const int Catch = 43;

		/// <summary>
		/// The Char token ID.
		/// </summary>
		public const int Char = 44;

		/// <summary>
		/// The Checked token ID.
		/// </summary>
		public const int Checked = 45;

		/// <summary>
		/// The Class token ID.
		/// </summary>
		public const int Class = 46;

		/// <summary>
		/// The Const token ID.
		/// </summary>
		public const int Const = 47;

		/// <summary>
		/// The Continue token ID.
		/// </summary>
		public const int Continue = 48;

		/// <summary>
		/// The Decimal token ID.
		/// </summary>
		public const int Decimal = 49;

		/// <summary>
		/// The Default token ID.
		/// </summary>
		public const int Default = 50;

		/// <summary>
		/// The Delegate token ID.
		/// </summary>
		public const int Delegate = 51;

		/// <summary>
		/// The Do token ID.
		/// </summary>
		public const int Do = 52;

		/// <summary>
		/// The Double token ID.
		/// </summary>
		public const int Double = 53;

		/// <summary>
		/// The Dynamic token ID.
		/// </summary>
		public const int Dynamic = 54;

		/// <summary>
		/// The Else token ID.
		/// </summary>
		public const int Else = 55;

		/// <summary>
		/// The Enum token ID.
		/// </summary>
		public const int Enum = 56;

		/// <summary>
		/// The Event token ID.
		/// </summary>
		public const int Event = 57;

		/// <summary>
		/// The Explicit token ID.
		/// </summary>
		public const int Explicit = 58;

		/// <summary>
		/// The Extern token ID.
		/// </summary>
		public const int Extern = 59;

		/// <summary>
		/// The False token ID.
		/// </summary>
		public const int False = 60;

		/// <summary>
		/// The Finally token ID.
		/// </summary>
		public const int Finally = 61;

		/// <summary>
		/// The Fixed token ID.
		/// </summary>
		public const int Fixed = 62;

		/// <summary>
		/// The Float token ID.
		/// </summary>
		public const int Float = 63;

		/// <summary>
		/// The For token ID.
		/// </summary>
		public const int For = 64;

		/// <summary>
		/// The ForEach token ID.
		/// </summary>
		public const int ForEach = 65;

		/// <summary>
		/// The Get token ID.
		/// </summary>
		public const int Get = 66;

		/// <summary>
		/// The Goto token ID.
		/// </summary>
		public const int Goto = 67;

		/// <summary>
		/// The If token ID.
		/// </summary>
		public const int If = 68;

		/// <summary>
		/// The Implicit token ID.
		/// </summary>
		public const int Implicit = 69;

		/// <summary>
		/// The In token ID.
		/// </summary>
		public const int In = 70;

		/// <summary>
		/// The Int token ID.
		/// </summary>
		public const int Int = 71;

		/// <summary>
		/// The Interface token ID.
		/// </summary>
		public const int Interface = 72;

		/// <summary>
		/// The Internal token ID.
		/// </summary>
		public const int Internal = 73;

		/// <summary>
		/// The Is token ID.
		/// </summary>
		public const int Is = 74;

		/// <summary>
		/// The Lock token ID.
		/// </summary>
		public const int Lock = 75;

		/// <summary>
		/// The Long token ID.
		/// </summary>
		public const int Long = 76;

		/// <summary>
		/// The Namespace token ID.
		/// </summary>
		public const int Namespace = 77;

		/// <summary>
		/// The New token ID.
		/// </summary>
		public const int New = 78;

		/// <summary>
		/// The Null token ID.
		/// </summary>
		public const int Null = 79;

		/// <summary>
		/// The Object token ID.
		/// </summary>
		public const int Object = 80;

		/// <summary>
		/// The Operator token ID.
		/// </summary>
		public const int Operator = 81;

		/// <summary>
		/// The Out token ID.
		/// </summary>
		public const int Out = 82;

		/// <summary>
		/// The Override token ID.
		/// </summary>
		public const int Override = 83;

		/// <summary>
		/// The Params token ID.
		/// </summary>
		public const int Params = 84;

		/// <summary>
		/// The Partial token ID.
		/// </summary>
		public const int Partial = 85;

		/// <summary>
		/// The Private token ID.
		/// </summary>
		public const int Private = 86;

		/// <summary>
		/// The Protected token ID.
		/// </summary>
		public const int Protected = 87;

		/// <summary>
		/// The Public token ID.
		/// </summary>
		public const int Public = 88;

		/// <summary>
		/// The ReadOnly token ID.
		/// </summary>
		public const int ReadOnly = 89;

		/// <summary>
		/// The Ref token ID.
		/// </summary>
		public const int Ref = 90;

		/// <summary>
		/// The Remove token ID.
		/// </summary>
		public const int Remove = 91;

		/// <summary>
		/// The Return token ID.
		/// </summary>
		public const int Return = 92;

		/// <summary>
		/// The SByte token ID.
		/// </summary>
		public const int SByte = 93;

		/// <summary>
		/// The Sealed token ID.
		/// </summary>
		public const int Sealed = 94;

		/// <summary>
		/// The Set token ID.
		/// </summary>
		public const int Set = 95;

		/// <summary>
		/// The Short token ID.
		/// </summary>
		public const int Short = 96;

		/// <summary>
		/// The SizeOf token ID.
		/// </summary>
		public const int SizeOf = 97;

		/// <summary>
		/// The StackAlloc token ID.
		/// </summary>
		public const int StackAlloc = 98;

		/// <summary>
		/// The Static token ID.
		/// </summary>
		public const int Static = 99;

		/// <summary>
		/// The String token ID.
		/// </summary>
		public const int String = 100;

		/// <summary>
		/// The Struct token ID.
		/// </summary>
		public const int Struct = 101;

		/// <summary>
		/// The Switch token ID.
		/// </summary>
		public const int Switch = 102;

		/// <summary>
		/// The This token ID.
		/// </summary>
		public const int This = 103;

		/// <summary>
		/// The Throw token ID.
		/// </summary>
		public const int Throw = 104;

		/// <summary>
		/// The True token ID.
		/// </summary>
		public const int True = 105;

		/// <summary>
		/// The Try token ID.
		/// </summary>
		public const int Try = 106;

		/// <summary>
		/// The TypeOf token ID.
		/// </summary>
		public const int TypeOf = 107;

		/// <summary>
		/// The UInt token ID.
		/// </summary>
		public const int UInt = 108;

		/// <summary>
		/// The ULong token ID.
		/// </summary>
		public const int ULong = 109;

		/// <summary>
		/// The Unchecked token ID.
		/// </summary>
		public const int Unchecked = 110;

		/// <summary>
		/// The Unsafe token ID.
		/// </summary>
		public const int Unsafe = 111;

		/// <summary>
		/// The UShort token ID.
		/// </summary>
		public const int UShort = 112;

		/// <summary>
		/// The Using token ID.
		/// </summary>
		public const int Using = 113;

		/// <summary>
		/// The Virtual token ID.
		/// </summary>
		public const int Virtual = 114;

		/// <summary>
		/// The Void token ID.
		/// </summary>
		public const int Void = 115;

		/// <summary>
		/// The Volatile token ID.
		/// </summary>
		public const int Volatile = 116;

		/// <summary>
		/// The While token ID.
		/// </summary>
		public const int While = 117;

		/// <summary>
		/// The Yield token ID.
		/// </summary>
		public const int Yield = 118;

		/// <summary>
		/// The KeywordEnd token ID.
		/// </summary>
		public const int KeywordEnd = 119;

		/// <summary>
		/// The OperatorOrPunctuatorStart token ID.
		/// </summary>
		public const int OperatorOrPunctuatorStart = 120;

		/// <summary>
		/// The OpenCurlyBrace token ID.
		/// </summary>
		public const int OpenCurlyBrace = 121;

		/// <summary>
		/// The CloseCurlyBrace token ID.
		/// </summary>
		public const int CloseCurlyBrace = 122;

		/// <summary>
		/// The OpenSquareBrace token ID.
		/// </summary>
		public const int OpenSquareBrace = 123;

		/// <summary>
		/// The CloseSquareBrace token ID.
		/// </summary>
		public const int CloseSquareBrace = 124;

		/// <summary>
		/// The OpenParenthesis token ID.
		/// </summary>
		public const int OpenParenthesis = 125;

		/// <summary>
		/// The CloseParenthesis token ID.
		/// </summary>
		public const int CloseParenthesis = 126;

		/// <summary>
		/// The Dot token ID.
		/// </summary>
		public const int Dot = 127;

		/// <summary>
		/// The Comma token ID.
		/// </summary>
		public const int Comma = 128;

		/// <summary>
		/// The Colon token ID.
		/// </summary>
		public const int Colon = 129;

		/// <summary>
		/// The NamespaceAliasQualifier token ID.
		/// </summary>
		public const int NamespaceAliasQualifier = 130;

		/// <summary>
		/// The SemiColon token ID.
		/// </summary>
		public const int SemiColon = 131;

		/// <summary>
		/// The Addition token ID.
		/// </summary>
		public const int Addition = 132;

		/// <summary>
		/// The Subtraction token ID.
		/// </summary>
		public const int Subtraction = 133;

		/// <summary>
		/// The Multiplication token ID.
		/// </summary>
		public const int Multiplication = 134;

		/// <summary>
		/// The Division token ID.
		/// </summary>
		public const int Division = 135;

		/// <summary>
		/// The Modulus token ID.
		/// </summary>
		public const int Modulus = 136;

		/// <summary>
		/// The BitwiseAnd token ID.
		/// </summary>
		public const int BitwiseAnd = 137;

		/// <summary>
		/// The BitwiseOr token ID.
		/// </summary>
		public const int BitwiseOr = 138;

		/// <summary>
		/// The ExclusiveOr token ID.
		/// </summary>
		public const int ExclusiveOr = 139;

		/// <summary>
		/// The Negation token ID.
		/// </summary>
		public const int Negation = 140;

		/// <summary>
		/// The OnesComplement token ID.
		/// </summary>
		public const int OnesComplement = 141;

		/// <summary>
		/// The Assignment token ID.
		/// </summary>
		public const int Assignment = 142;

		/// <summary>
		/// The LessThan token ID.
		/// </summary>
		public const int LessThan = 143;

		/// <summary>
		/// The GreaterThan token ID.
		/// </summary>
		public const int GreaterThan = 144;

		/// <summary>
		/// The QuestionMark token ID.
		/// </summary>
		public const int QuestionMark = 145;

		/// <summary>
		/// The Increment token ID.
		/// </summary>
		public const int Increment = 146;

		/// <summary>
		/// The Decrement token ID.
		/// </summary>
		public const int Decrement = 147;

		/// <summary>
		/// The ConditionalAnd token ID.
		/// </summary>
		public const int ConditionalAnd = 148;

		/// <summary>
		/// The ConditionalOr token ID.
		/// </summary>
		public const int ConditionalOr = 149;

		/// <summary>
		/// The LeftShift token ID.
		/// </summary>
		public const int LeftShift = 150;

		/// <summary>
		/// The Equality token ID.
		/// </summary>
		public const int Equality = 151;

		/// <summary>
		/// The Inequality token ID.
		/// </summary>
		public const int Inequality = 152;

		/// <summary>
		/// The LessThanOrEqual token ID.
		/// </summary>
		public const int LessThanOrEqual = 153;

		/// <summary>
		/// The GreaterThanOrEqual token ID.
		/// </summary>
		public const int GreaterThanOrEqual = 154;

		/// <summary>
		/// The AdditionAssignment token ID.
		/// </summary>
		public const int AdditionAssignment = 155;

		/// <summary>
		/// The SubtractionAssignment token ID.
		/// </summary>
		public const int SubtractionAssignment = 156;

		/// <summary>
		/// The MultiplicationAssignment token ID.
		/// </summary>
		public const int MultiplicationAssignment = 157;

		/// <summary>
		/// The DivisionAssignment token ID.
		/// </summary>
		public const int DivisionAssignment = 158;

		/// <summary>
		/// The ModulusAssignment token ID.
		/// </summary>
		public const int ModulusAssignment = 159;

		/// <summary>
		/// The BitwiseAndAssignment token ID.
		/// </summary>
		public const int BitwiseAndAssignment = 160;

		/// <summary>
		/// The BitwiseOrAssignment token ID.
		/// </summary>
		public const int BitwiseOrAssignment = 161;

		/// <summary>
		/// The ExclusiveOrAssignment token ID.
		/// </summary>
		public const int ExclusiveOrAssignment = 162;

		/// <summary>
		/// The LeftShiftAssignment token ID.
		/// </summary>
		public const int LeftShiftAssignment = 163;

		/// <summary>
		/// The PointerDereference token ID.
		/// </summary>
		public const int PointerDereference = 164;

		/// <summary>
		/// The NullCoalescing token ID.
		/// </summary>
		public const int NullCoalescing = 165;

		/// <summary>
		/// The Lambda token ID.
		/// </summary>
		public const int Lambda = 166;

		/// <summary>
		/// The OperatorOrPunctuatorEnd token ID.
		/// </summary>
		public const int OperatorOrPunctuatorEnd = 167;

		/// <summary>
		/// The PreProcessorDirectiveKeywordStart token ID.
		/// </summary>
		public const int PreProcessorDirectiveKeywordStart = 168;

		/// <summary>
		/// The IfPreProcessorDirective token ID.
		/// </summary>
		public const int IfPreProcessorDirective = 169;

		/// <summary>
		/// The ElsePreProcessorDirective token ID.
		/// </summary>
		public const int ElsePreProcessorDirective = 170;

		/// <summary>
		/// The ElIfPreProcessorDirective token ID.
		/// </summary>
		public const int ElIfPreProcessorDirective = 171;

		/// <summary>
		/// The EndIfPreProcessorDirective token ID.
		/// </summary>
		public const int EndIfPreProcessorDirective = 172;

		/// <summary>
		/// The DefinePreProcessorDirective token ID.
		/// </summary>
		public const int DefinePreProcessorDirective = 173;

		/// <summary>
		/// The UndefPreProcessorDirective token ID.
		/// </summary>
		public const int UndefPreProcessorDirective = 174;

		/// <summary>
		/// The WarningPreProcessorDirective token ID.
		/// </summary>
		public const int WarningPreProcessorDirective = 175;

		/// <summary>
		/// The ErrorPreProcessorDirective token ID.
		/// </summary>
		public const int ErrorPreProcessorDirective = 176;

		/// <summary>
		/// The LinePreProcessorDirective token ID.
		/// </summary>
		public const int LinePreProcessorDirective = 177;

		/// <summary>
		/// The RegionPreProcessorDirective token ID.
		/// </summary>
		public const int RegionPreProcessorDirective = 178;

		/// <summary>
		/// The EndRegionPreProcessorDirective token ID.
		/// </summary>
		public const int EndRegionPreProcessorDirective = 179;

		/// <summary>
		/// The PragmaPreProcessorDirective token ID.
		/// </summary>
		public const int PragmaPreProcessorDirective = 180;

		/// <summary>
		/// The PreProcessorDirectiveKeywordEnd token ID.
		/// </summary>
		public const int PreProcessorDirectiveKeywordEnd = 181;

		/// <summary>
		/// The PreProcessorDirectiveText token ID.
		/// </summary>
		public const int PreProcessorDirectiveText = 182;

		/// <summary>
		/// The MaxTokenID token ID.
		/// </summary>
		public const int MaxTokenID = 183;

	}
	#endregion

	#region Lexical State IDs
	/// <summary>
	/// Contains the lexical state IDs for the <c>C#</c> language.
	/// </summary>
	public class CSharpLexicalStateID {

		/// <summary>
		/// Returns the string-based key for the specified lexical state ID.
		/// </summary>
		/// <param name="id">The lexical state ID to examine.</param>
		public static string GetLexicalStateKey(int id) {
			System.Reflection.FieldInfo[] fields = typeof(CSharpLexicalStateID).GetFields();
			foreach (System.Reflection.FieldInfo field in fields) {
				if ((field.IsStatic) && (field.IsLiteral) && (id.Equals(field.GetValue(null))))
					return field.Name;
			}
			return null;
		}

		/// <summary>
		/// The Default lexical state ID.
		/// </summary>
		public const int Default = 0;

		/// <summary>
		/// The DocumentationComment lexical state ID.
		/// </summary>
		public const int DocumentationComment = 1;

		/// <summary>
		/// The PreProcessorDirective lexical state ID.
		/// </summary>
		public const int PreProcessorDirective = 2;

	}
	#endregion

	#region Semantic Parser
	/// <summary>
	/// Provides a semantic parser for the <c>C#</c> language.
	/// </summary>
	internal class CSharpSemanticParser : ActiproSoftware.SyntaxEditor.ParserGenerator.RecursiveDescentSemanticParser {

		private Stack			blockStack;
		private CompilationUnit	compilationUnit;
		private int				curlyBraceLevel;
		private StringBuilder	identifierStringBuilder = new StringBuilder();

		/// <summary>
		/// Initializes a new instance of the <c>CSharpSemanticParser</c> class.
		/// </summary>
		/// <param name="lexicalParser">The <see cref="ActiproSoftware.SyntaxEditor.ParserGenerator.IRecursiveDescentLexicalParser"/> to use for lexical parsing.</param>
		public CSharpSemanticParser(ActiproSoftware.SyntaxEditor.ParserGenerator.IRecursiveDescentLexicalParser lexicalParser) : base(lexicalParser) {}
	
		/// <summary>
		/// Advances to the next <see cref="IToken"/>.
		/// </summary>
		/// <returns>
		/// The <see cref="IToken"/> that was read.
		/// </returns>
		protected override IToken AdvanceToNext() {
			IToken token = base.AdvanceToNext();
			if (!this.TokenIsLanguageChange(token)) {
				switch (token.ID) {
					case CSharpTokenID.OpenCurlyBrace:
						curlyBraceLevel++;
						break;
					case CSharpTokenID.CloseCurlyBrace:
						curlyBraceLevel--;
						break;
				}
			}
			return token;
		}

		/// <summary>
		/// Advances to (or past) the next matching close curly brace.
		/// </summary>
		/// <param name="openCurlyBraceLevel">The level of the open curly brace.</param>	
		/// <param name="movePast">Whether to move past the close curly brace.</param>
		private void AdvanceToNextCloseCurlyBrace(int openCurlyBraceLevel, bool movePast) {
			while (!this.IsAtEnd) {
				this.AdvanceToNext(CSharpTokenID.OpenCurlyBrace, CSharpTokenID.CloseCurlyBrace); 
				if ((this.TokenIs(this.LookAheadToken, CSharpTokenID.CloseCurlyBrace)) && (curlyBraceLevel <= openCurlyBraceLevel + 1))
					break;
				this.AdvanceToNext();
			}			
			if ((movePast) && (this.TokenIs(this.LookAheadToken, CSharpTokenID.CloseCurlyBrace)))
				this.AdvanceToNext();
		}
	
		/// <summary>
		/// Returns whether the next two <see cref="IToken"/> objects match the specified IDs.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the next two <see cref="IToken"/> objects match the specified IDs; otherwise, <c>false</c>.
		/// </returns>
		private bool AreNextTwo(int firstTokenID, int secondTokenID) {
			return (this.TokenIs(this.LookAheadToken, firstTokenID)) && (this.TokenIs(this.GetLookAheadToken(2), secondTokenID));
		}
		
		/// <summary>
		/// Returns whether the next two <see cref="IToken"/> objects match an identifier and the specified ID.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the next two <see cref="IToken"/> objects match an identifier and the specified ID; otherwise, <c>false</c>.
		/// </returns>
		private bool AreNextTwoIdentifierAnd(int secondTokenID) {
			return (this.IsIdentifier(this.LookAheadToken)) && 
				this.TokenIs(this.GetLookAheadToken(2), secondTokenID);
		}
		
		/// <summary>
		/// Adds a child node to the current block.
		/// </summary>
		/// <param name="node">The <see cref="AstNode"/> to add to the current block.</param>
		private void BlockAddChild(AstNode node) {
			// If the node is a type declaration, add it to the Types list
			if (node is TypeDeclaration) 
				compilationUnit.Types.Add(node);
		
			if (blockStack.Count == 0) {
				// Compilation unit
				if ((node is NamespaceDeclaration) || (node is TypeDeclaration)) {
					compilationUnit.NamespaceMembers.Add(node);
					return;
				}
			}
			else {
				AstNode currentBlock = (AstNode)blockStack.Peek();
				if (currentBlock is NamespaceDeclaration) {
					if ((node is NamespaceDeclaration) || (node is TypeDeclaration)) {
						((NamespaceDeclaration)currentBlock).NamespaceMembers.Add(node);
						return;
					}
				}
				else if (currentBlock is TypeDeclaration) {
					if (node is TypeMemberDeclaration) {
						((TypeDeclaration)currentBlock).Members.Add(node);
						return;
					}
				}
			}
			
			// 12/29/2009 (14A-12CCDF57-DE29) - throw new NotSupportedException();
		}

		/// <summary>
		/// Starts a block and pushes it on the block stack.
		/// </summary>
		/// <param name="node">The <see cref="AstNode"/> that is starting a block.</param>
		private void BlockStart(AstNode node) {
			blockStack.Push(node);
		}

		/// <summary>
		/// Ends the current block and pops it from the block stack.
		/// </summary>
		private void BlockEnd() {
			blockStack.Pop();
		}
		
		/// <summary>
		/// Gets the <see cref="CompilationUnit"/> that was parsed.
		/// </summary>
		/// <value>The <see cref="CompilationUnit"/> that was parsed.</value>
		public CompilationUnit CompilationUnit {
			get {
				return compilationUnit;
			}
		}
		
		/// <summary>
		/// Returns an inferred <see cref="TypeReference"/> for the return type of the specified <see cref="Expression"/> initializer.
		/// </summary>
		/// <param name="initializer">The <see cref="Expression"/> to examine.</param>
		/// <param name="allowNull">Whether to allow a null initializer.</param>
		/// <returns>An inferred <see cref="TypeReference"/> for the return type of the specified <see cref="Expression"/> initializer.</returns>
		private TypeReference GetImplicitType(Expression initializer, bool allowNull) {
			if (initializer == null) {
				this.ReportSyntaxError(this.Token.TextRange, AssemblyInfo.Instance.Resources.GetString("SemanticParserError_ImplicitlyTypedVariableDeclarationNoInitializer"));
				return null;
			}
			
			// NOTE: GetReturnType is not currently looking backwards to resolve SimpleName references, thus foreach use of var will
			//		 reflect System.Object most of the time
			
			// Try and determine what the type reference is from the initializer
			TypeReference typeReference = this.GetReturnType(initializer);
			
			// Clone the type reference
			if (typeReference != null)
				typeReference = typeReference.Clone();
			
			// TODO: Need to ensure that a collection initializer is not passed.
			
			if ((!allowNull) && (typeReference == null))
				this.ReportSyntaxError(this.Token.TextRange, AssemblyInfo.Instance.Resources.GetString("SemanticParserError_ImplicitlyTypedVariableDeclarationNullInitializer"));
				
			return typeReference;
		}
		
		/// <summary>
		/// Returns an inferred <see cref="TypeReference"/> for the return type of the specified <see cref="Expression"/>.
		/// </summary>
		/// <param name="expression">The <see cref="Expression"/> to examine.</param>
		/// <returns>An inferred <see cref="TypeReference"/> for the return type of the specified <see cref="Expression"/>.</returns>
		private TypeReference GetReturnType(Expression expression) {
			if (expression is LiteralExpression) {
				switch (((LiteralExpression)expression).LiteralType) {
					case LiteralType.True:
					case LiteralType.False:
						return new TypeReference("System.Boolean", expression.TextRange);
					case LiteralType.DecimalInteger:
					case LiteralType.HexadecimalInteger:
					case LiteralType.OctalInteger:
						return new TypeReference("System.Int32", expression.TextRange);
					case LiteralType.Real:
						return new TypeReference("System.Double", expression.TextRange);
					case LiteralType.Character:
						return new TypeReference("System.Char", expression.TextRange);
					case LiteralType.String:
					case LiteralType.VerbatimString:
						return new TypeReference("System.String", expression.TextRange);
					case LiteralType.Date:
						return new TypeReference("System.DateTime", expression.TextRange);
					case LiteralType.Null:
						return null;
				}
			}
			else if (expression is CastExpression)
				return ((CastExpression)expression).ReturnType;
			else if (expression is DefaultValueExpression)
				return ((DefaultValueExpression)expression).ReturnType;
			else if (expression is ObjectCreationExpression)
				return ((ObjectCreationExpression)expression).ObjectType;
			else if (expression is ParenthesizedExpression)
				return this.GetReturnType(((ParenthesizedExpression)expression).Expression);
			else if (expression is SizeOfExpression)
				return new TypeReference("System.Int32", expression.TextRange);
			else if (expression is TryCastExpression)
				return ((TryCastExpression)expression).ReturnType;
			else if (expression is TypeOfExpression)
				return new TypeReference("System.Type", expression.TextRange);
			else if (expression is TypeReferenceExpression)
				return ((TypeReferenceExpression)expression).TypeReference;
				
			// No type could be found within the initializer
			return new TypeReference("System.Object", expression.TextRange);
		}
	
		/// <summary>
		/// Returns whether the current <see cref="IToken"/> is an array with rank specifiers.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the current <see cref="IToken"/> is an array with rank specifiers; otherwise, <c>false</c>.
		/// </returns>
		private bool IsArrayRankSpecifiers() {
			if (!this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenSquareBrace))
				return false;
			IToken token = this.GetLookAheadToken(2);
			if (!this.TokenIsLanguageChange(token)) {
				switch (token.ID) {
					case CSharpTokenID.Comma:
					case CSharpTokenID.CloseSquareBrace:
						return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Returns whether the current <see cref="IToken"/> is a comma followed by an identifier.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the current <see cref="IToken"/> is a comma followed by an identifier; otherwise, <c>false</c>.
		/// </returns>
		private bool IsCommaAndIdentifier() {
			return (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) && (this.IsIdentifier(this.GetLookAheadToken(2)));
		}

		/// <summary>
		/// Returns whether the specified <see cref="IToken"/> is a contextual keyword.
		/// </summary>
		/// <param name="token">The <see cref="IToken"/> to examine.</param>
		/// <returns>
		/// <c>true</c> if the specified <see cref="IToken"/> is a Contextual keyword; otherwise, <c>false</c>.
		/// </returns>
		private bool IsContextualKeyword(IToken token) {
			if ((token.ID > CSharpTokenID.ContextualKeywordStart) && (token.ID < CSharpTokenID.ContextualKeywordEnd)) {
				// Double check for language change
				if (this.TokenIs(token, token.ID))
					return true;					
			}
			return false;
		}
		
		/// <summary>
		/// Returns whether the current <see cref="IToken"/> is the start of a default value expression.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the current <see cref="IToken"/> is the start of a default value expression; otherwise, <c>false</c>.
		/// </returns>
		private bool IsDefaultValueExpression() {
			return (this.TokenIs(this.LookAheadToken, CSharpTokenID.Default)) && (this.TokenIs(this.GetLookAheadToken(2), CSharpTokenID.OpenParenthesis));
		}

		/// <summary>
		/// Returns whether the current <see cref="IToken"/> is a qualifier identifier continuation.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the current <see cref="IToken"/> is a qualifier identifier continuation; otherwise, <c>false</c>.
		/// </returns>
		private bool IsGlobalAttributeSection() {
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenSquareBrace)) {
				IToken targetToken = this.GetLookAheadToken(2);
				return ((this.TokenIs(targetToken, CSharpTokenID.Identifier)) && (this.IsValidGlobalAttributeSectionTarget(this.GetTokenText(targetToken))));
			}
			else
				return false;
		}
		
		/// <summary>
		/// Returns whether the specified <see cref="IToken"/> is an identifier (or contextual keyword).
		/// </summary>
		/// <param name="token">The <see cref="IToken"/> to examine.</param>
		/// <returns>
		/// <c>true</c> if the specified <see cref="IToken"/> is an identifier (or contextual keyword); otherwise, <c>false</c>.
		/// </returns>
		private bool IsIdentifier(IToken token) {
			return (this.TokenIs(token, CSharpTokenID.Identifier)) ||
				(this.IsContextualKeyword(token));
		}
		
		/// <summary>
		/// Returns whether the current <see cref="IToken"/> is an implicitly typed lambda parameter list.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the current <see cref="IToken"/> is an implicitly typed lambda parameter list; otherwise, <c>false</c>.
		/// </returns>
		private bool IsImplicitlyTypedLambdaParameterList() {
			if (this.IsIdentifier(this.LookAheadToken)) {
				IToken targetToken = this.GetLookAheadToken(2);
				return ((this.TokenIs(targetToken, CSharpTokenID.Comma)) || (this.TokenIs(targetToken, CSharpTokenID.CloseParenthesis)));
			}
			else
				return false;
		}

		/// <summary>
		/// Returns whether the current <see cref="IToken"/> is the start of a lambda expression.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the current <see cref="IToken"/> is the start of a lambda expression; otherwise, <c>false</c>.
		/// </returns>
		private bool IsLambdaExpression() {
			if (this.IsIdentifier(this.LookAheadToken)) {
				// Simple case... check the next token for a Lambda
				return this.TokenIs(this.GetLookAheadToken(2), CSharpTokenID.Lambda);
			}
			else if (!this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenParenthesis))
				return false;
			
			// More difficult case... look for a Lambda after a parenthesis set
			this.StartPeek();
			try {
				this.Peek();  // Skip the open parenthesis
				while (!this.IsAtEnd) {						
					if (this.TokenIsLanguageChange(this.PeekToken))
						return false;
					switch (this.PeekToken.ID) {
						// Parameter modifiers
						case CSharpTokenID.Out:
						case CSharpTokenID.Ref:
						// Native types
						case CSharpTokenID.Bool:
						case CSharpTokenID.Decimal:
						case CSharpTokenID.SByte:
						case CSharpTokenID.Byte:
						case CSharpTokenID.Short:
						case CSharpTokenID.UShort:
						case CSharpTokenID.Int:
						case CSharpTokenID.UInt:
						case CSharpTokenID.Long:
						case CSharpTokenID.ULong:
						case CSharpTokenID.Char:
						case CSharpTokenID.Float:
						case CSharpTokenID.Double:
						case CSharpTokenID.Object:
						case CSharpTokenID.String:
						case CSharpTokenID.Dynamic:
						// Other
						case CSharpTokenID.Comma:
						case CSharpTokenID.Dot:
						case CSharpTokenID.LessThan:
						case CSharpTokenID.GreaterThan:
						case CSharpTokenID.OpenSquareBrace:
						case CSharpTokenID.CloseSquareBrace:
						case CSharpTokenID.QuestionMark:
						case CSharpTokenID.Multiplication:
							// Allowed token... continue on
							this.Peek();
							break;
						case CSharpTokenID.CloseParenthesis:
							// Return whether the next token is a Lambda
							this.Peek();
							return this.TokenIs(this.PeekToken, CSharpTokenID.Lambda);							
						default:
							if (this.IsIdentifier(this.PeekToken)) {
								// Allowed token... continue on
								this.Peek();
								break;
							}						
							return false;
					}
				}
				return false;
			}
			finally {
				this.StopPeek();
			}
		}
		
		/// <summary>
		/// Returns whether the current <see cref="IToken"/> is a keyword.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the current <see cref="IToken"/> is a keyword; otherwise, <c>false</c>.
		/// </returns>
		private bool IsKeyword() {
			return this.IsKeyword(this.LookAheadToken);
		}
		
		/// <summary>
		/// Returns whether the specified <see cref="IToken"/> is a keyword.
		/// </summary>
		/// <param name="token">The <see cref="IToken"/> to examine.</param>
		/// <returns>
		/// <c>true</c> if the specified <see cref="IToken"/> is a keyword; otherwise, <c>false</c>.
		/// </returns>
		private bool IsKeyword(IToken token) {
			if ((token.ID > CSharpTokenID.KeywordStart) && (token.ID < CSharpTokenID.KeywordEnd)) {
				// Double check for language change
				if (this.TokenIs(token, token.ID))
					return true;					
			}
			return false;
		}
	
		/// <summary>
		/// Returns whether the current <see cref="IToken"/> is a keyword or an identifier followed by a colon.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the current <see cref="IToken"/> is a keyword or an identifier followed by a colon; otherwise, <c>false</c>.
		/// </returns>
		private bool IsKeywordOrIdentifierAndColon() {
			return ((this.IsIdentifier(this.LookAheadToken)) || (this.IsKeyword())) && 
				(this.TokenIs(this.GetLookAheadToken(2), CSharpTokenID.Colon));
		}

		/// <summary>
		/// Returns whether the current <see cref="IToken"/> is the start of an object initializer.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the current <see cref="IToken"/> is the start of an object initializer; otherwise, <c>false</c>.
		/// </returns>
		private bool IsObjectInitializer() {
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.CloseCurlyBrace))  // Empty object initializer
				return true;
			else if ((this.IsIdentifier(this.LookAheadToken)) && (this.TokenIs(this.GetLookAheadToken(2), CSharpTokenID.Assignment)))
				return true;
			else
				return false;
		}
		
		/// <summary>
		/// Returns whether the current <see cref="IToken"/> is the start of a parameter array.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the current <see cref="IToken"/> is the start of a parameter array; otherwise, <c>false</c>.
		/// </returns>
		private bool IsParameterArray() {
			return (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) && (this.TokenIs(this.GetLookAheadToken(2), CSharpTokenID.Params));
		}
	
		/// <summary>
		/// Returns whether the current <see cref="IToken"/> is the start of a query expression.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the current <see cref="IToken"/> is the start of a query expression; otherwise, <c>false</c>.
		/// </returns>
		private bool IsQueryExpression() {
			if (!this.TokenIs(this.LookAheadToken, CSharpTokenID.From))
				return false;
				
			this.StartPeek();
			try {
				this.Peek();  // Skip the From
				return this.IsType();
			}
			finally {
				this.StopPeek();
			}
		}

		/// <summary>
		/// Returns whether the current <see cref="IToken"/> is a qualifier identifier continuation.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the current <see cref="IToken"/> is a qualifier identifier continuation; otherwise, <c>false</c>.
		/// </returns>
		private bool IsQualifierIdentifierContinuation() {
			return (this.TokenIs(this.LookAheadToken, CSharpTokenID.Dot)) && (this.IsIdentifier(this.GetLookAheadToken(2)));
		}
		
		/// <summary>
		/// Returns whether the current <see cref="IToken"/> is the start of a right shift.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the current <see cref="IToken"/> is the start of a right shift; otherwise, <c>false</c>.
		/// </returns>
		private bool IsRightShift() {
			return (this.TokenIs(this.LookAheadToken, CSharpTokenID.GreaterThan)) && (this.TokenIs(this.GetLookAheadToken(2), CSharpTokenID.GreaterThan));
		}
		
		/// <summary>
		/// Returns whether the current <see cref="IToken"/> is the start of a right shift.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the current <see cref="IToken"/> is the start of a right shift; otherwise, <c>false</c>.
		/// </returns>
		private bool IsRightShiftAssignment() {
			return (this.TokenIs(this.LookAheadToken, CSharpTokenID.GreaterThan)) && (this.TokenIs(this.GetLookAheadToken(2), CSharpTokenID.GreaterThanOrEqual));
		}
		
		/// <summary>
		/// Returns whether the current <see cref="IToken"/> is the start of a type.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the current <see cref="IToken"/> is the start of a type; otherwise, <c>false</c>.
		/// </returns>
		/// <remarks>
		/// This method can only be called after a peek operation has started.
		/// Types have this syntax: (Identifier "::")? (Identifier ("." Identifier)* | NativeType) ("&lt;" Type "&gt;")? ("?")? ("[" "]")?
		/// </remarks>
		private bool IsType() {
			if (!this.IsTypeCore())
				return false;
		
			// Skip over a type argument list
			while (this.IsTypeArgumentList()) {
				int level = 0;
				while (!this.IsAtEnd) {
					if (this.TokenIsLanguageChange(this.PeekToken))
						return false;
					switch (this.PeekToken.ID) {
						case CSharpTokenID.LessThan:
							level++;
							break;
						case CSharpTokenID.GreaterThan:
							level--;
							break;
					}
					this.Peek();				
					if (level <= 0)
						break;
				}
				
				if (this.TokenIs(this.PeekToken, CSharpTokenID.Dot)) {
					this.Peek();  // Skip over dot
					if (!this.IsTypeCore())
						return false;
				}
				else 
					break;
			}
			
			// Skip over a nullable type
			if (this.TokenIs(this.PeekToken, CSharpTokenID.QuestionMark))
				this.Peek();
			
			// Skip over the array ranks if any
			while (this.TokenIs(this.PeekToken, CSharpTokenID.OpenSquareBrace)) {
				this.Peek();
				bool exitLoop = false;
				while (!this.IsAtEnd) {						
					if (this.TokenIsLanguageChange(this.PeekToken))
						return false;
					switch (this.PeekToken.ID) {
						case CSharpTokenID.Comma:
							this.Peek();
							break;
						case CSharpTokenID.CloseSquareBrace:
							this.Peek();
							exitLoop = true;
							break;
						default:
							return false;
					}
					if (exitLoop)
						break;
				}
			}
			
			// Skip over pointers if any
			while (this.TokenIs(this.PeekToken, CSharpTokenID.Multiplication))
				this.Peek();
			
			return true;
		}

		/// <summary>
		/// Returns whether the current <see cref="IToken"/> is the start of a type.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the current <see cref="IToken"/> is the start of a type; otherwise, <c>false</c>.
		/// </returns>
		/// <remarks>
		/// This method can only be called from <see cref="IsType"/>.
		/// </remarks>
		private bool IsTypeCore() {
			IToken token = this.Peek();
			if (this.TokenIsLanguageChange(token))
				return false;			
			switch (token.ID) {
				case CSharpTokenID.Bool:
				case CSharpTokenID.Decimal:
				case CSharpTokenID.SByte:
				case CSharpTokenID.Byte:
				case CSharpTokenID.Short:
				case CSharpTokenID.UShort:
				case CSharpTokenID.Int:
				case CSharpTokenID.UInt:
				case CSharpTokenID.Long:
				case CSharpTokenID.ULong:
				case CSharpTokenID.Char:
				case CSharpTokenID.Float:
				case CSharpTokenID.Double:
				case CSharpTokenID.Object:
				case CSharpTokenID.String:
				case CSharpTokenID.Dynamic:
					break;
				case CSharpTokenID.Void:
					if (this.TokenIs(this.PeekToken, CSharpTokenID.Multiplication)) {
						this.Peek();
						return true;
					}
					else
						return false;
				default:
					if (this.IsIdentifier(token)) {
						// Skip over the qualified identifier
						bool exitLoop = false;
						bool nextIsDot = true;
						while (!this.IsAtEnd) {						
							if (this.TokenIsLanguageChange(this.PeekToken))
								return false;
							switch (this.PeekToken.ID) {
								case CSharpTokenID.Dot:
									if (!nextIsDot) {
										exitLoop = true;
										break;
									}
									this.Peek();
									nextIsDot = false;
									break;
								case CSharpTokenID.NamespaceAliasQualifier:
									this.Peek();
									if (!this.IsIdentifier(this.PeekToken))
										return false;
									nextIsDot = false;
									break;
								case CSharpTokenID.LessThan:
								case CSharpTokenID.QuestionMark:
								case CSharpTokenID.OpenSquareBrace:
								case CSharpTokenID.CloseParenthesis:
								case CSharpTokenID.Multiplication:
									exitLoop = true;
									break;
								default:
									if (this.IsIdentifier(this.PeekToken)) {
										if (nextIsDot) {
											exitLoop = true;
											break;
										}
										this.Peek();
										nextIsDot = true;
										break;
									}
									exitLoop = true;
									break;
							}
							if (exitLoop)
								break;
						}
						break;
					}
					return false;
			}

			return true;
		}
		
		/// <summary>
		/// Returns whether the current <see cref="IToken"/> is the start of a type argument list.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the current <see cref="IToken"/> is the start of a type argument list; otherwise, <c>false</c>.
		/// </returns>
		private bool IsTypeArgumentList() {
			if (!this.TokenIs((this.PeekToken != null ? this.PeekToken : this.LookAheadToken), CSharpTokenID.LessThan))
				return false;
				
			this.StartPeek();
			try {
				int level = 0;
				while (!this.IsAtEnd) {
					if (this.TokenIsLanguageChange(this.PeekToken))
						return false;
					switch (this.PeekToken.ID) {
						case CSharpTokenID.LessThan:
							level++;
							break;
						case CSharpTokenID.GreaterThan:
							level--;
							break;
						case CSharpTokenID.Comma:
						case CSharpTokenID.Dot:
						case CSharpTokenID.OpenSquareBrace:
						case CSharpTokenID.CloseSquareBrace:
						case CSharpTokenID.QuestionMark:
							// Continue
							break;
						default:
							if (CSharpToken.IsNativeType(this.PeekToken.ID)) {
								// Continue
								break;
							}
							else if (this.IsIdentifier(this.PeekToken)) {
								// Continue
								break;
							}
							return false;
					}
					if (level <= 0)
						break;
					this.Peek();				
				}
				this.Peek();				
				if (this.TokenIsLanguageChange(this.PeekToken))
					return false;
				switch (this.PeekToken.ID) {
					case CSharpTokenID.OpenParenthesis:
					case CSharpTokenID.CloseParenthesis:
					case CSharpTokenID.OpenSquareBrace:
					case CSharpTokenID.CloseSquareBrace:
					case CSharpTokenID.OpenCurlyBrace:
					case CSharpTokenID.GreaterThan:
					case CSharpTokenID.Colon:
					case CSharpTokenID.SemiColon:
					case CSharpTokenID.Comma:
					case CSharpTokenID.QuestionMark:
					case CSharpTokenID.Equality:
					case CSharpTokenID.Inequality:
					case CSharpTokenID.Operator:
					case CSharpTokenID.This:
						return true;
					case CSharpTokenID.Dot:
						this.Peek();
						return true;
					default:
						return this.IsIdentifier(this.PeekToken) || ((this.PeekToken.ID > CSharpTokenID.Addition) && (this.PeekToken.ID < CSharpTokenID.OperatorOrPunctuatorEnd));
				}
			}
			finally {
				this.StopPeek();
			}
		}

		/// <summary>
		/// Returns whether the current <see cref="IToken"/> is the start of a type cast.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the current <see cref="IToken"/> is the start of a type cast; otherwise, <c>false</c>.
		/// </returns>
		private bool IsTypeCast() {
			if (!this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenParenthesis))
				return false;
				
			this.StartPeek();
			try {
				this.Peek();  // Skip the open parenthesis
				if (!this.IsType())
					return false;
				if (!this.TokenIs(this.Peek(), CSharpTokenID.CloseParenthesis))
					return false;
				return ((this.IsInMultiMatchSet(0, this.PeekToken) || (this.IsDefaultValueExpression())));
			}
			finally {
				this.StopPeek();
			}
		}

		/// <summary>
		/// Returns whether the target specification is a valid global attribute section target.
		/// </summary>
		/// <param name="target">The target specification to examine.</param>
		/// <returns>
		/// <c>true</c> if the target specification is a valid global attribute section target; otherwise, <c>false</c>.
		/// </returns>
		private bool IsValidGlobalAttributeSectionTarget(string target) {
			switch (target) {
				case "assembly":
				case "module":
					return true;
				default:
					return false;
			}
		}

		/// <summary>
		/// Returns whether the current <see cref="IToken"/> is the start of a variable declaration.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the current <see cref="IToken"/> is the start of a variable declaration; otherwise, <c>false</c>.
		/// </returns>
		private bool IsVariableDeclaration() {
			this.StartPeek();
			try {
				if (this.TokenIs(this.PeekToken, CSharpTokenID.Var)) {
					// Skip over 'var'
					this.Peek();
				}
				else if (!this.IsType())
					return false;

				return (this.IsIdentifier(this.PeekToken));
			}
			finally {
				this.StopPeek();
			}
		}

		/// <summary>
		/// Returns whether the current <see cref="IToken"/> is the start of a variable declarator.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the current <see cref="IToken"/> is the start of a variable declarator; otherwise, <c>false</c>.
		/// </returns>
		private bool IsVariableDeclarator() {
			if (this.IsIdentifier(this.LookAheadToken)) {
				IToken token = this.GetLookAheadToken(2);
				if (!this.TokenIsLanguageChange(token)) {
					switch (token.ID) {
						case CSharpTokenID.Assignment:
						case CSharpTokenID.Comma:
						case CSharpTokenID.SemiColon:
							return true;
					}
				}
			}
			return false;
		}
		
		/// <summary>
		/// Flags any generic parameters that are located.
		/// </summary>
		/// <param name="typeParameterList">The list of type parameters that are defined.</param>
		/// <param name="typeReference">The <see cref="TypeReference"/> to examine.</param>
		/// <param name="allowTopLevelMark">Whether to allow the type reference to be marked.</param>
		private void MarkGenericParameters(IAstNodeList typeParameterList, TypeReference typeReference, bool allowTopLevelMark) {
			if ((typeParameterList == null) || (typeReference == null))
				return;

			if (allowTopLevelMark) {
				// Look for a match on the specified type reference
				foreach (TypeReference declaredTypeParameter in typeParameterList) {
					if (declaredTypeParameter.Name == typeReference.Name) {
						typeReference.IsGenericParameter = true;
						return;
					}
				}
			}

			// Recurse
			if (typeReference.GenericTypeArguments != null) {
				foreach (TypeReference genericTypeArgument in typeReference.GenericTypeArguments)
					this.MarkGenericParameters(typeParameterList, genericTypeArgument, true);
			}
		}
		
		/// <summary>
		/// Parses the code and returns the list of arguments that were parsed.
		/// </summary>
		/// <returns>
		/// An <see cref="IAstNodeList" /> containing the list of arguments that were parsed.
		/// </returns>
		internal IAstNodeList ParseArgumentList() {
			// Initialize
			blockStack = new Stack();
			compilationUnit = new CompilationUnit();
			compilationUnit.SourceLanguage = DotNetLanguage.CSharp;
			compilationUnit.StartOffset = this.LookAheadToken.StartOffset;

			// Match argument list		
			AstNodeList argumentList = new AstNodeList(null);
			this.MatchArgumentList(out argumentList);
			
			// Finalize
			compilationUnit.EndOffset = this.LookAheadToken.EndOffset;
			
			return argumentList;
		}

		/// <summary>
		/// Reaps the comments that have been collected since the last reaping and adds them to an <see cref="IAstNodeList"/>.
		/// </summary>
		/// <param name="nodes">The <see cref="IAstNodeList"/> that should receive any reaped comment nodes.</param>
		/// <param name="sort">Whether to sort the sibling nodes.</param>
		private void ReapComments(IAstNodeList nodes, bool sort) {
			if (this.LexicalParser is CSharpRecursiveDescentLexicalParser) {
				Comment[] comments = ((CSharpRecursiveDescentLexicalParser)this.LexicalParser).ReapComments(nodes.ParentNode.TextRange);
				if (comments != null) {
					foreach (Comment comment in comments)
						nodes.Add(comment);
					if (sort)
						nodes.ParentNode.ChildNodes.SortByStartOffset();
				}
			}
		}

		/// <summary>
		/// Reaps the documentation comments that have been collected since the last reaping.
		/// </summary>
		/// <returns>The documentation comments that have been collection since the last reaping.</returns>
		private string ReapDocumentationComments() {
			if (this.LexicalParser is CSharpRecursiveDescentLexicalParser)
				return ((CSharpRecursiveDescentLexicalParser)this.LexicalParser).ReapDocumentationComments();
			else
				return null;
		}

		/// <summary>
		/// Reports a syntax error.
		/// </summary>
		/// <param name="textRange">The <see cref="TextRange"/> of the error.</param>
		/// <param name="message">The error message.</param>
		protected override void ReportSyntaxError(TextRange textRange, string message) {
			// Don't allow multiple errors at the same offset
			if ((compilationUnit.SyntaxErrors.Count > 0) && (((SyntaxError)compilationUnit.SyntaxErrors[compilationUnit.SyntaxErrors.Count - 1]).TextRange.StartOffset == textRange.StartOffset))
				return;
			
			compilationUnit.SyntaxErrors.Add(new SyntaxError(textRange, message));
		}

		/// <summary>
		/// Parses the document and generates a document object model.
		/// </summary>
		public void Parse() {
			this.MatchCompilationUnit();
		}

		/// <summary>
		/// Matches a <c>NamespaceName</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>NamespaceName</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>.
		/// </remarks>
		protected virtual bool MatchNamespaceName(out QualifiedIdentifier namespaceName) {
			if (!this.MatchQualifiedIdentifier(out namespaceName))
				return false;
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.NamespaceAliasQualifier)) {
				if (!this.Match(CSharpTokenID.NamespaceAliasQualifier))
					return false;
				string alias = namespaceName.Text;
				if (!this.MatchQualifiedIdentifier(out namespaceName))
					return false;
				else {
					namespaceName.Text = alias + "." + namespaceName.Text;
				}
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>TypeName</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>TypeName</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>.
		/// </remarks>
		protected virtual bool MatchTypeName(bool unboundTypeArguments, out TypeReference typeReference) {
			typeReference = null;
			QualifiedIdentifier identifier;
			AstNodeList typeArgumentList = null;
			if (!this.MatchQualifiedIdentifier(out identifier))
				return false;
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.NamespaceAliasQualifier)) {
				this.Match(CSharpTokenID.NamespaceAliasQualifier);
				string alias = identifier.Text;
				if (!this.MatchQualifiedIdentifier(out identifier))
					return false;
				else {
					identifier.Text = alias + "." + identifier.Text;
				}
			}
			while (this.IsTypeArgumentList()) {
				if ((unboundTypeArguments) && ((this.AreNextTwo(CSharpTokenID.LessThan, CSharpTokenID.Comma)) || (this.AreNextTwo(CSharpTokenID.LessThan, CSharpTokenID.GreaterThan)))) {
					if (this.TokenIs(this.LookAheadToken, CSharpTokenID.LessThan)) {
						if (!this.Match(CSharpTokenID.LessThan))
							return false;
						while (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) {
							this.Match(CSharpTokenID.Comma);
						}
						if (!this.Match(CSharpTokenID.GreaterThan))
							return false;
					}
				}
				else {
					if (this.TokenIs(this.LookAheadToken, CSharpTokenID.LessThan)) {
						if (!this.MatchTypeArgumentList(out typeArgumentList))
							return false;
					}
				}
				
				QualifiedIdentifier nextIdentifier;
				if (this.IsQualifierIdentifierContinuation()) {
					if (!this.Match(CSharpTokenID.Dot))
						return false;
					if (!this.MatchQualifiedIdentifier(out nextIdentifier))
						return false;
					else {
						identifier.Text += "." + nextIdentifier.Text;
					}
					// Clear the type argument list since the target type has changed
					typeArgumentList = null;
				}
			}
			
			// Create the type reference
			typeReference = new TypeReference(identifier.Text, identifier.TextRange);
			if (typeArgumentList != null)
				typeReference.GenericTypeArguments.AddRange(typeArgumentList.ToArray());
			return true;
		}

		/// <summary>
		/// Matches a <c>Type</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>Type</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Void</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>.
		/// </remarks>
		protected virtual bool MatchType(out TypeReference typeReference) {
			if (!this.MatchTypeCore(false, false, out typeReference))
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>TypeCore</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>TypeCore</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Void</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>.
		/// </remarks>
		protected virtual bool MatchTypeCore(bool unboundTypeArguments, bool useExtendedNullableTypeCheck, out TypeReference typeReference) {
			int[] arrayRanks;
			if (!this.MatchNonArrayType(unboundTypeArguments, useExtendedNullableTypeCheck, out typeReference))
				return false;
			if (!this.MatchRankSpecifier(out arrayRanks))
				return false;
			else {
				typeReference.ArrayRanks = arrayRanks;
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>SimpleType</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>SimpleType</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>.
		/// </remarks>
		protected virtual bool MatchSimpleType(out TypeReference typeReference) {
			typeReference = null;
			if (this.IsInMultiMatchSet(1, this.LookAheadToken)) {
				if (!this.MatchNumericType(out typeReference))
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Bool)) {
				if (!this.Match(CSharpTokenID.Bool))
					return false;
				else {
					typeReference = new TypeReference("System.Boolean", this.Token.TextRange);
				}
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>NumericType</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>NumericType</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>.
		/// </remarks>
		protected virtual bool MatchNumericType(out TypeReference typeReference) {
			typeReference = null;
			if (this.IsInMultiMatchSet(2, this.LookAheadToken)) {
				if (!this.MatchIntegralType(out typeReference))
					return false;
			}
			else if (((this.TokenIs(this.LookAheadToken, CSharpTokenID.Float)) || (this.TokenIs(this.LookAheadToken, CSharpTokenID.Double)))) {
				if (!this.MatchFloatingPointType(out typeReference))
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Decimal)) {
				if (!this.Match(CSharpTokenID.Decimal))
					return false;
				else {
					typeReference = new TypeReference("System.Decimal", this.Token.TextRange);
				}
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>IntegralType</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>IntegralType</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>.
		/// </remarks>
		protected virtual bool MatchIntegralType(out TypeReference typeReference) {
			typeReference = null;
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.SByte)) {
				if (!this.Match(CSharpTokenID.SByte))
					return false;
				else {
					typeReference = new TypeReference("System.SByte", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Byte)) {
				if (!this.Match(CSharpTokenID.Byte))
					return false;
				else {
					typeReference = new TypeReference("System.Byte", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Short)) {
				if (!this.Match(CSharpTokenID.Short))
					return false;
				else {
					typeReference = new TypeReference("System.Int16", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.UShort)) {
				if (!this.Match(CSharpTokenID.UShort))
					return false;
				else {
					typeReference = new TypeReference("System.UInt16", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Int)) {
				if (!this.Match(CSharpTokenID.Int))
					return false;
				else {
					typeReference = new TypeReference("System.Int32", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.UInt)) {
				if (!this.Match(CSharpTokenID.UInt))
					return false;
				else {
					typeReference = new TypeReference("System.UInt32", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Long)) {
				if (!this.Match(CSharpTokenID.Long))
					return false;
				else {
					typeReference = new TypeReference("System.Int64", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.ULong)) {
				if (!this.Match(CSharpTokenID.ULong))
					return false;
				else {
					typeReference = new TypeReference("System.UInt64", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Char)) {
				if (!this.Match(CSharpTokenID.Char))
					return false;
				else {
					typeReference = new TypeReference("System.Char", this.Token.TextRange);
				}
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>FloatingPointType</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>FloatingPointType</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Float</c>, <c>Double</c>.
		/// </remarks>
		protected virtual bool MatchFloatingPointType(out TypeReference typeReference) {
			typeReference = null;
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Float)) {
				if (!this.Match(CSharpTokenID.Float))
					return false;
				else {
					typeReference = new TypeReference("System.Single", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Double)) {
				if (!this.Match(CSharpTokenID.Double))
					return false;
				else {
					typeReference = new TypeReference("System.Double", this.Token.TextRange);
				}
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>ClassType</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ClassType</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>.
		/// </remarks>
		protected virtual bool MatchClassType(bool unboundTypeArguments, out TypeReference typeReference) {
			typeReference = null;
			if (this.IsInMultiMatchSet(3, this.LookAheadToken)) {
				if (!this.MatchTypeName(unboundTypeArguments, out typeReference))
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Object)) {
				if (!this.Match(CSharpTokenID.Object))
					return false;
				else {
					typeReference = new TypeReference("System.Object", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.String)) {
				if (!this.Match(CSharpTokenID.String))
					return false;
				else {
					typeReference = new TypeReference("System.String", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Dynamic)) {
				if (!this.Match(CSharpTokenID.Dynamic))
					return false;
				else {
					typeReference = new TypeReference("System.Object", this.Token.TextRange);
				}
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>NonArrayType</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>NonArrayType</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Void</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>.
		/// </remarks>
		protected virtual bool MatchNonArrayType(bool unboundTypeArguments, bool useExtendedNullableTypeCheck, out TypeReference typeReference) {
			typeReference = null;
			if (this.IsInMultiMatchSet(4, this.LookAheadToken)) {
				if (!this.MatchClassType(unboundTypeArguments, out typeReference))
					return false;
			}
			else if (this.IsInMultiMatchSet(5, this.LookAheadToken)) {
				if (!this.MatchSimpleType(out typeReference))
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Void)) {
				int startOffset = this.LookAheadToken.StartOffset;
				if (!this.Match(CSharpTokenID.Void))
					return false;
				if (!this.Match(CSharpTokenID.Multiplication))
					return false;
				typeReference = new TypeReference("System.Void", new TextRange(startOffset, this.Token.EndOffset));
				typeReference.PointerLevel = 1;
			}
			else
				return false;
			if ((this.TokenIs(this.LookAheadToken, CSharpTokenID.QuestionMark)) && (typeReference.EndOffset == this.LookAheadToken.StartOffset) &&
					(
						(!useExtendedNullableTypeCheck) ||
						(Array.IndexOf(new int[] { CSharpTokenID.SemiColon, CSharpTokenID.Comma, CSharpTokenID.CloseParenthesis, CSharpTokenID.OpenSquareBrace, CSharpTokenID.Colon }, this.GetLookAheadToken(2).ID) != -1)
					)) {
				if (!this.Match(CSharpTokenID.QuestionMark))
					return false;
				TypeReference nullableTypeReference = typeReference;
				typeReference = new TypeReference("System.Nullable", new TextRange(nullableTypeReference.StartOffset, this.Token.EndOffset));
				typeReference.GenericTypeArguments.Add(nullableTypeReference);
			}
			while (this.TokenIs(this.LookAheadToken, CSharpTokenID.Multiplication)) {
				if (!this.Match(CSharpTokenID.Multiplication))
					return false;
				typeReference.PointerLevel++;
				typeReference.EndOffset = this.Token.EndOffset;
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>RankSpecifier</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>RankSpecifier</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>OpenSquareBrace</c>.
		/// </remarks>
		protected virtual bool MatchRankSpecifier(out int[] ranks) {
			ranks = null;
			ArrayList rankList = new ArrayList();
			int rank = 0;
			while (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenSquareBrace)) {
				if (!this.Match(CSharpTokenID.OpenSquareBrace))
					return false;
				else {
					rank = 1;
				}
				while (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) {
					if (this.Match(CSharpTokenID.Comma)) {
						rank++;
					}
				}
				if (!this.Match(CSharpTokenID.CloseSquareBrace))
					return false;
				else {
					rankList.Add(rank);
				}
			}
			if (rankList.Count > 0) {
				ranks = new int[rankList.Count];
				rankList.CopyTo(ranks, 0);
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>ArgumentList</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ArgumentList</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Multiplication</c>, <c>Ref</c>, <c>Out</c>, <c>True</c>, <c>False</c>, <c>DecimalIntegerLiteral</c>, <c>HexadecimalIntegerLiteral</c>, <c>RealLiteral</c>, <c>CharacterLiteral</c>, <c>StringLiteral</c>, <c>VerbatimStringLiteral</c>, <c>Null</c>, <c>OpenParenthesis</c>, <c>This</c>, <c>Base</c>, <c>New</c>, <c>TypeOf</c>, <c>SizeOf</c>, <c>Checked</c>, <c>Unchecked</c>, <c>Increment</c>, <c>Decrement</c>, <c>Addition</c>, <c>Subtraction</c>, <c>Negation</c>, <c>OnesComplement</c>, <c>BitwiseAnd</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>, <c>Delegate</c>.
		/// The non-terminal can start with: this.IsLambdaExpression().
		/// The non-terminal can start with: this.IsQueryExpression().
		/// The non-terminal can start with: this.IsDefaultValueExpression().
		/// </remarks>
		protected virtual bool MatchArgumentList(out AstNodeList initializerArgumentList) {
			initializerArgumentList = new AstNodeList(null);
			Expression expression;
			if (!this.MatchArgument(out expression)) {
				this.ReportSyntaxError(AssemblyInfo.Instance.Resources.GetString("SemanticParserError_ArgumentExpected")); return false;
			}
			else {
				initializerArgumentList.Add(expression);
			}
			while (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) {
				if (!this.Match(CSharpTokenID.Comma))
					return false;
				if (!this.MatchArgument(out expression)) {
					this.ReportSyntaxError(AssemblyInfo.Instance.Resources.GetString("SemanticParserError_ArgumentExpected")); return true;
				}
				else {
					initializerArgumentList.Add(expression);
				}
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>Argument</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>Argument</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Multiplication</c>, <c>Ref</c>, <c>Out</c>, <c>True</c>, <c>False</c>, <c>DecimalIntegerLiteral</c>, <c>HexadecimalIntegerLiteral</c>, <c>RealLiteral</c>, <c>CharacterLiteral</c>, <c>StringLiteral</c>, <c>VerbatimStringLiteral</c>, <c>Null</c>, <c>OpenParenthesis</c>, <c>This</c>, <c>Base</c>, <c>New</c>, <c>TypeOf</c>, <c>SizeOf</c>, <c>Checked</c>, <c>Unchecked</c>, <c>Increment</c>, <c>Decrement</c>, <c>Addition</c>, <c>Subtraction</c>, <c>Negation</c>, <c>OnesComplement</c>, <c>BitwiseAnd</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>, <c>Delegate</c>.
		/// The non-terminal can start with: this.IsLambdaExpression().
		/// The non-terminal can start with: this.IsQueryExpression().
		/// The non-terminal can start with: this.IsDefaultValueExpression().
		/// </remarks>
		protected virtual bool MatchArgument(out Expression expression) {
			QualifiedIdentifier name = null;
			expression = null;
			int startOffset = this.LookAheadToken.StartOffset;
			if (this.AreNextTwoIdentifierAnd(CSharpTokenID.Colon)) {
				// C# 4.0 named argument
				name = new QualifiedIdentifier(this.LookAheadTokenText, this.LookAheadToken.TextRange);
				this.AdvanceToNext();
				this.AdvanceToNext();
			}
			if ((this.IsInMultiMatchSet(0, this.LookAheadToken) || (this.IsLambdaExpression()) || (this.IsQueryExpression()) || (this.IsDefaultValueExpression()))) {
				if (!this.MatchExpression(out expression))
					return false;
				else {
					expression = new ArgumentExpression(ParameterModifiers.None, name, expression, expression.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Ref)) {
				if (!this.Match(CSharpTokenID.Ref))
					return false;
				if (!this.MatchExpression(out expression))
					return false;
				else {
					expression = new ArgumentExpression(ParameterModifiers.Ref, name, expression, new TextRange(startOffset, this.Token.EndOffset));
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Out)) {
				if (!this.Match(CSharpTokenID.Out))
					return false;
				if (!this.MatchExpression(out expression))
					return false;
				else {
					expression = new ArgumentExpression(ParameterModifiers.Out, name, expression, new TextRange(startOffset, this.Token.EndOffset));
				}
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>PrimaryExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>PrimaryExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>True</c>, <c>False</c>, <c>DecimalIntegerLiteral</c>, <c>HexadecimalIntegerLiteral</c>, <c>RealLiteral</c>, <c>CharacterLiteral</c>, <c>StringLiteral</c>, <c>VerbatimStringLiteral</c>, <c>Null</c>, <c>OpenParenthesis</c>, <c>This</c>, <c>Base</c>, <c>New</c>, <c>TypeOf</c>, <c>SizeOf</c>, <c>Checked</c>, <c>Unchecked</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>, <c>Delegate</c>.
		/// The non-terminal can start with: this.IsDefaultValueExpression().
		/// </remarks>
		protected virtual bool MatchPrimaryExpression(out Expression expression) {
			expression = null;
			int startOffset = this.LookAheadToken.StartOffset;
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.True)) {
				if (!this.Match(CSharpTokenID.True))
					return false;
				else {
					expression = new LiteralExpression(LiteralType.True, null, this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.False)) {
				if (!this.Match(CSharpTokenID.False))
					return false;
				else {
					expression = new LiteralExpression(LiteralType.False, null, this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.DecimalIntegerLiteral)) {
				if (!this.Match(CSharpTokenID.DecimalIntegerLiteral))
					return false;
				else {
					expression = new LiteralExpression(LiteralType.DecimalInteger, this.TokenText, this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.HexadecimalIntegerLiteral)) {
				if (!this.Match(CSharpTokenID.HexadecimalIntegerLiteral))
					return false;
				else {
					expression = new LiteralExpression(LiteralType.HexadecimalInteger, this.TokenText, this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.RealLiteral)) {
				if (!this.Match(CSharpTokenID.RealLiteral))
					return false;
				else {
					expression = new LiteralExpression(LiteralType.Real, this.TokenText, this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.CharacterLiteral)) {
				if (!this.Match(CSharpTokenID.CharacterLiteral))
					return false;
				else {
					expression = new LiteralExpression(LiteralType.Character, this.TokenText, this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.StringLiteral)) {
				if (!this.Match(CSharpTokenID.StringLiteral))
					return false;
				else {
					expression = new LiteralExpression(LiteralType.String, this.TokenText, this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.VerbatimStringLiteral)) {
				if (!this.Match(CSharpTokenID.VerbatimStringLiteral))
					return false;
				else {
					expression = new LiteralExpression(LiteralType.VerbatimString, this.TokenText, this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Null)) {
				if (!this.Match(CSharpTokenID.Null))
					return false;
				else {
					expression = new LiteralExpression(LiteralType.Null, null, this.Token.TextRange);
				}
			}
			else if (this.IsInMultiMatchSet(3, this.LookAheadToken)) {
				if (!this.MatchSimpleIdentifier())
					return false;
				if (this.TokenIs(this.LookAheadToken, CSharpTokenID.NamespaceAliasQualifier)) {
					// Create a type reference
					string alias = this.TokenText;
					if (!this.Match(CSharpTokenID.NamespaceAliasQualifier))
						return false;
					if (!this.MatchSimpleIdentifier())
						return false;
					expression = new TypeReferenceExpression(new TypeReference(alias + "." + this.TokenText, new TextRange(startOffset, this.Token.EndOffset)));
				}
				else {
					// Create a simple name
					expression = new SimpleName(this.TokenText, this.Token.TextRange);
					AstNodeList typeArgumentList = null;
					if (this.IsTypeArgumentList()) {
						if (this.TokenIs(this.LookAheadToken, CSharpTokenID.LessThan)) {
							if (!this.MatchTypeArgumentList(out typeArgumentList))
								return false;
						}
					}
					if (typeArgumentList != null)
						expression.GenericTypeArguments.AddRange(typeArgumentList.ToArray());
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenParenthesis)) {
				if (!this.Match(CSharpTokenID.OpenParenthesis))
					return false;
				if (!this.MatchExpression(out expression))
					return false;
				if (!this.Match(CSharpTokenID.CloseParenthesis))
					return false;
				expression = new ParenthesizedExpression(expression, new TextRange(startOffset, this.Token.EndOffset));
			}
			else if (this.IsInMultiMatchSet(6, this.LookAheadToken)) {
				QualifiedIdentifier memberName;
				if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Bool)) {
					if (!this.Match(CSharpTokenID.Bool))
						return false;
					else {
						expression = new TypeReferenceExpression(new TypeReference("System.Boolean", this.Token.TextRange));
					}
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Byte)) {
					if (!this.Match(CSharpTokenID.Byte))
						return false;
					else {
						expression = new TypeReferenceExpression(new TypeReference("System.Byte", this.Token.TextRange));
					}
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Char)) {
					if (!this.Match(CSharpTokenID.Char))
						return false;
					else {
						expression = new TypeReferenceExpression(new TypeReference("System.'Char", this.Token.TextRange));
					}
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Decimal)) {
					if (!this.Match(CSharpTokenID.Decimal))
						return false;
					else {
						expression = new TypeReferenceExpression(new TypeReference("System.Decimal", this.Token.TextRange));
					}
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Double)) {
					if (!this.Match(CSharpTokenID.Double))
						return false;
					else {
						expression = new TypeReferenceExpression(new TypeReference("System.Double", this.Token.TextRange));
					}
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Dynamic)) {
					if (!this.Match(CSharpTokenID.Dynamic))
						return false;
					else {
						expression = new TypeReferenceExpression(new TypeReference("System.Object", this.Token.TextRange));
					}
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Float)) {
					if (!this.Match(CSharpTokenID.Float))
						return false;
					else {
						expression = new TypeReferenceExpression(new TypeReference("System.Single", this.Token.TextRange));
					}
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Int)) {
					if (!this.Match(CSharpTokenID.Int))
						return false;
					else {
						expression = new TypeReferenceExpression(new TypeReference("System.Int32", this.Token.TextRange));
					}
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Long)) {
					if (!this.Match(CSharpTokenID.Long))
						return false;
					else {
						expression = new TypeReferenceExpression(new TypeReference("System.Int64", this.Token.TextRange));
					}
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Object)) {
					if (!this.Match(CSharpTokenID.Object))
						return false;
					else {
						expression = new TypeReferenceExpression(new TypeReference("System.Object", this.Token.TextRange));
					}
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.SByte)) {
					if (!this.Match(CSharpTokenID.SByte))
						return false;
					else {
						expression = new TypeReferenceExpression(new TypeReference("System.SByte", this.Token.TextRange));
					}
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Short)) {
					if (!this.Match(CSharpTokenID.Short))
						return false;
					else {
						expression = new TypeReferenceExpression(new TypeReference("System.Int16", this.Token.TextRange));
					}
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.String)) {
					if (!this.Match(CSharpTokenID.String))
						return false;
					else {
						expression = new TypeReferenceExpression(new TypeReference("System.String", this.Token.TextRange));
					}
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.UInt)) {
					if (!this.Match(CSharpTokenID.UInt))
						return false;
					else {
						expression = new TypeReferenceExpression(new TypeReference("System.UInt32", this.Token.TextRange));
					}
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.ULong)) {
					if (!this.Match(CSharpTokenID.ULong))
						return false;
					else {
						expression = new TypeReferenceExpression(new TypeReference("System.UInt64", this.Token.TextRange));
					}
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.UShort)) {
					if (!this.Match(CSharpTokenID.UShort))
						return false;
					else {
						expression = new TypeReferenceExpression(new TypeReference("System.UInt16", this.Token.TextRange));
					}
				}
				else
					return false;
				if (!this.Match(CSharpTokenID.Dot))
					return false;
				if (!this.MatchIdentifier(out memberName))
					return false;
				expression = new MemberAccess(expression, memberName, new TextRange(startOffset, this.Token.EndOffset));
				AstNodeList typeArgumentList = null;
				if (this.IsTypeArgumentList()) {
					if (this.TokenIs(this.LookAheadToken, CSharpTokenID.LessThan)) {
						if (!this.MatchTypeArgumentList(out typeArgumentList))
							return false;
					}
				}
				if (typeArgumentList != null)
					expression.GenericTypeArguments.AddRange(typeArgumentList.ToArray());
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.This)) {
				if (!this.Match(CSharpTokenID.This))
					return false;
				else {
					expression = new ThisAccess(this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Base)) {
				if (!this.Match(CSharpTokenID.Base))
					return false;
				else {
					expression = new BaseAccess(this.Token.TextRange);
				}
				if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Dot)) {
					QualifiedIdentifier memberName;
					if (!this.Match(CSharpTokenID.Dot))
						return false;
					if (!this.MatchIdentifier(out memberName))
						return false;
					expression = new MemberAccess(expression, memberName, new TextRange(startOffset, this.Token.EndOffset));
					AstNodeList typeArgumentList = null;
					if (this.IsTypeArgumentList()) {
						if (this.TokenIs(this.LookAheadToken, CSharpTokenID.LessThan)) {
							if (!this.MatchTypeArgumentList(out typeArgumentList))
								return false;
						}
					}
					if (typeArgumentList != null)
						expression.GenericTypeArguments.AddRange(typeArgumentList.ToArray());
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenSquareBrace)) {
					// Element access
					expression = new InvocationExpression(expression);
					((InvocationExpression)expression).IsIndexerInvocation = true;
					expression.StartOffset = this.LookAheadToken.StartOffset;
					Expression parameterExpression;
					if (!this.Match(CSharpTokenID.OpenSquareBrace))
						return false;
					if (!this.MatchExpression(out parameterExpression))
						return false;
					else {
						((InvocationExpression)expression).Arguments.Add(parameterExpression);
					}
					while (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) {
						if (!this.Match(CSharpTokenID.Comma))
							return false;
						if (!this.MatchExpression(out parameterExpression))
							return false;
						else {
							((InvocationExpression)expression).Arguments.Add(parameterExpression);
						}
					}
					if (!this.Match(CSharpTokenID.CloseSquareBrace))
						return false;
					expression.EndOffset = this.Token.EndOffset;
				}
				else
					return false;
			}
			else if (this.AreNextTwo(CSharpTokenID.New, CSharpTokenID.OpenCurlyBrace)) {
				if (!this.MatchAnonymousObjectCreationExpression(out expression))
					return false;
			}
			else if (this.AreNextTwo(CSharpTokenID.New, CSharpTokenID.OpenSquareBrace)) {
				if (!this.MatchImplicitlyTypedArrayCreationExpression(out expression))
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.New)) {
				if (!this.Match(CSharpTokenID.New))
					return false;
				TypeReference typeReference;
				AstNodeList argumentList = null;
				if (!this.MatchNonArrayType(false, false, out typeReference))
					return false;
				if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenParenthesis)) {
					expression = new ObjectCreationExpression(typeReference);
					expression.StartOffset = startOffset;
					Expression initializer = null;
					if (!this.Match(CSharpTokenID.OpenParenthesis))
						return false;
					if ((this.IsInMultiMatchSet(7, this.LookAheadToken) || (this.IsLambdaExpression()) || (this.IsQueryExpression()) || (this.IsDefaultValueExpression()))) {
						if (!this.MatchArgumentList(out argumentList))
							return false;
					}
					if (!this.Match(CSharpTokenID.CloseParenthesis))
						return false;
					if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenCurlyBrace)) {
						if (!this.MatchObjectOrCollectionInitializer(out initializer))
							return false;
					}
					// NOTE: This handles both object and delegate creation since their grammar is essentially the same
					if (argumentList != null)
						((ObjectCreationExpression)expression).Arguments.AddRange(argumentList.ToArray());
					expression.EndOffset = this.Token.EndOffset;
					((ObjectCreationExpression)expression).Initializer = (ObjectCollectionInitializerExpression)initializer;
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenCurlyBrace)) {
					// Object or collection initializer
					expression = new ObjectCreationExpression(typeReference);
					expression.StartOffset = startOffset;
					Expression initializer = null;
					if (!this.MatchObjectOrCollectionInitializer(out initializer))
						return false;
					expression.EndOffset = this.Token.EndOffset;
					((ObjectCreationExpression)expression).Initializer = (ObjectCollectionInitializerExpression)initializer;
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenSquareBrace)) {
					expression = new ObjectCreationExpression(typeReference);
					((ObjectCreationExpression)expression).IsArray = true;
					expression.StartOffset = startOffset;
					Expression parameterExpression;
					int[] arrayRanks;
					Expression initializer = null;
					if (this.IsArrayRankSpecifiers()) {
						if (!this.MatchRankSpecifier(out arrayRanks))
							return false;
						else {
							typeReference.ArrayRanks = arrayRanks;
						}
						if (!this.MatchArrayInitializer(out initializer))
							return false;
					}
					if (initializer == null) {
						if (!this.Match(CSharpTokenID.OpenSquareBrace))
							return false;
						if (!this.MatchExpression(out parameterExpression))
							return false;
						else {
							((ObjectCreationExpression)expression).Arguments.Add(parameterExpression);
						}
						while (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) {
							if (!this.Match(CSharpTokenID.Comma))
								return false;
							if (!this.MatchExpression(out parameterExpression))
								return false;
							else {
								((ObjectCreationExpression)expression).Arguments.Add(parameterExpression);
							}
						}
						if (!this.Match(CSharpTokenID.CloseSquareBrace))
							return false;
						if (!this.MatchRankSpecifier(out arrayRanks))
							return false;
						else {
							if (arrayRanks == null)
								arrayRanks = new int[] { ((ObjectCreationExpression)expression).Arguments.Count };
							typeReference.ArrayRanks = arrayRanks;
						}
						if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenCurlyBrace)) {
							if (!this.MatchArrayInitializer(out initializer))
								return false;
						}
					}
					((ObjectCreationExpression)expression).Initializer = (ObjectCollectionInitializerExpression)initializer;
					expression.EndOffset = this.Token.EndOffset;
				}
				else
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.TypeOf)) {
				if (!this.Match(CSharpTokenID.TypeOf))
					return false;
				if (!this.Match(CSharpTokenID.OpenParenthesis))
					return false;
				TypeReference typeReference = null;
				if ((this.TokenIs(this.LookAheadToken, CSharpTokenID.Void)) && (!this.TokenIs(this.GetLookAheadToken(2), CSharpTokenID.Multiplication))) {
					// Regular void (non-pointer)
					if (!this.Match(CSharpTokenID.Void))
						return false;
					else {
						typeReference = new TypeReference("System.Void", this.Token.TextRange);
					}
				}
				else {
					if (!this.MatchTypeCore(true, false, out typeReference))
						return false;
				}
				if (!this.Match(CSharpTokenID.CloseParenthesis))
					return false;
				expression = new TypeOfExpression(typeReference, new TextRange(startOffset, this.Token.EndOffset));
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.SizeOf)) {
				if (!this.Match(CSharpTokenID.SizeOf))
					return false;
				if (!this.Match(CSharpTokenID.OpenParenthesis))
					return false;
				TypeReference typeReference = null;
				if (!this.MatchType(out typeReference))
					return false;
				if (!this.Match(CSharpTokenID.CloseParenthesis))
					return false;
				expression = new SizeOfExpression(typeReference, new TextRange(startOffset, this.Token.EndOffset));
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Checked)) {
				if (!this.Match(CSharpTokenID.Checked))
					return false;
				if (!this.Match(CSharpTokenID.OpenParenthesis))
					return false;
				if (!this.MatchExpression(out expression))
					return false;
				if (!this.Match(CSharpTokenID.CloseParenthesis))
					return false;
				expression = new CheckedExpression(expression, new TextRange(startOffset, this.Token.EndOffset));
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Unchecked)) {
				if (!this.Match(CSharpTokenID.Unchecked))
					return false;
				if (!this.Match(CSharpTokenID.OpenParenthesis))
					return false;
				if (!this.MatchExpression(out expression))
					return false;
				if (!this.Match(CSharpTokenID.CloseParenthesis))
					return false;
				expression = new UncheckedExpression(expression, new TextRange(startOffset, this.Token.EndOffset));
			}
			else if (this.IsDefaultValueExpression()) {
				if (!this.Match(CSharpTokenID.Default))
					return false;
				if (!this.Match(CSharpTokenID.OpenParenthesis))
					return false;
				TypeReference typeReference = null;
				if (!this.MatchType(out typeReference))
					return false;
				if (!this.Match(CSharpTokenID.CloseParenthesis))
					return false;
				expression = new DefaultValueExpression(typeReference, new TextRange(startOffset, this.Token.EndOffset));
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Delegate)) {
				if (!this.MatchAnonymousMethodExpression(out expression))
					return false;
			}
			else
				return false;
			while (this.IsInMultiMatchSet(8, this.LookAheadToken)) {
				if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Dot)) {
					// Member access
					QualifiedIdentifier memberName;
					if (!this.Match(CSharpTokenID.Dot))
						return false;
					if (!this.MatchIdentifier(out memberName))
						return false;
					expression = new MemberAccess(expression, memberName, new TextRange(startOffset, this.Token.EndOffset));
					AstNodeList typeArgumentList = null;
					if (this.IsTypeArgumentList()) {
						if (this.TokenIs(this.LookAheadToken, CSharpTokenID.LessThan)) {
							if (!this.MatchTypeArgumentList(out typeArgumentList))
								return false;
						}
					}
					if (typeArgumentList != null)
						expression.GenericTypeArguments.AddRange(typeArgumentList.ToArray());
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.PointerDereference)) {
					// Member access (via pointer)
						QualifiedIdentifier memberName;
					if (!this.Match(CSharpTokenID.PointerDereference))
						return false;
					if (!this.MatchIdentifier(out memberName))
						return false;
					expression = new PointerMemberAccess(expression, memberName, new TextRange(startOffset, this.Token.EndOffset));
					AstNodeList typeArgumentList = null;
					if (this.IsTypeArgumentList()) {
						if (this.TokenIs(this.LookAheadToken, CSharpTokenID.LessThan)) {
							if (!this.MatchTypeArgumentList(out typeArgumentList))
								return false;
						}
					}
					if (typeArgumentList != null)
						expression.GenericTypeArguments.AddRange(typeArgumentList.ToArray());
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Increment)) {
					if (!this.Match(CSharpTokenID.Increment))
						return false;
					else {
						expression = new UnaryExpression(OperatorType.PostIncrement, expression);
					}
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Decrement)) {
					if (!this.Match(CSharpTokenID.Decrement))
						return false;
					else {
						expression = new UnaryExpression(OperatorType.PostDecrement, expression);
					}
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenParenthesis)) {
					// Invocation expression
					expression = new InvocationExpression(expression);
					expression.StartOffset = startOffset;
					AstNodeList argumentList = null;
					if (!this.Match(CSharpTokenID.OpenParenthesis))
						return false;
					if ((this.IsInMultiMatchSet(7, this.LookAheadToken) || (this.IsLambdaExpression()) || (this.IsQueryExpression()) || (this.IsDefaultValueExpression()))) {
						if (!this.MatchArgumentList(out argumentList))
							return false;
					}
					if (!this.Match(CSharpTokenID.CloseParenthesis))
						return false;
					if (argumentList != null)
						((InvocationExpression)expression).Arguments.AddRange(argumentList.ToArray());
					expression.EndOffset = this.Token.EndOffset;
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenSquareBrace)) {
					// Element access
					expression = new InvocationExpression(expression);
					((InvocationExpression)expression).IsIndexerInvocation = true;
					expression.StartOffset = startOffset;
					AstNodeList argumentList = null;  // C# 4.0 named arguments
					if (!this.Match(CSharpTokenID.OpenSquareBrace))
						return false;
					if (!this.MatchArgumentList(out argumentList))
						return false;
					if (!this.Match(CSharpTokenID.CloseSquareBrace))
						return false;
					if (argumentList != null)
						((InvocationExpression)expression).Arguments.AddRange(argumentList.ToArray());
					expression.EndOffset = this.Token.EndOffset;
				}
				else
					return false;
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>UnaryExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>UnaryExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Multiplication</c>, <c>True</c>, <c>False</c>, <c>DecimalIntegerLiteral</c>, <c>HexadecimalIntegerLiteral</c>, <c>RealLiteral</c>, <c>CharacterLiteral</c>, <c>StringLiteral</c>, <c>VerbatimStringLiteral</c>, <c>Null</c>, <c>OpenParenthesis</c>, <c>This</c>, <c>Base</c>, <c>New</c>, <c>TypeOf</c>, <c>SizeOf</c>, <c>Checked</c>, <c>Unchecked</c>, <c>Increment</c>, <c>Decrement</c>, <c>Addition</c>, <c>Subtraction</c>, <c>Negation</c>, <c>OnesComplement</c>, <c>BitwiseAnd</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>, <c>Delegate</c>.
		/// The non-terminal can start with: this.IsDefaultValueExpression().
		/// </remarks>
		protected virtual bool MatchUnaryExpression(out Expression expression) {
			expression = null;
			int startOffset = this.LookAheadToken.StartOffset;
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Addition)) {
				if (!this.Match(CSharpTokenID.Addition))
					return false;
				if (!this.MatchUnaryExpression(out expression))
					return false;
				expression = new UnaryExpression(OperatorType.Addition, expression);
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Subtraction)) {
				if (!this.Match(CSharpTokenID.Subtraction))
					return false;
				if (!this.MatchUnaryExpression(out expression))
					return false;
				expression = new UnaryExpression(OperatorType.Subtraction, expression);
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Negation)) {
				if (!this.Match(CSharpTokenID.Negation))
					return false;
				if (!this.MatchUnaryExpression(out expression))
					return false;
				expression = new UnaryExpression(OperatorType.Negation, expression);
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OnesComplement)) {
				if (!this.Match(CSharpTokenID.OnesComplement))
					return false;
				if (!this.MatchUnaryExpression(out expression))
					return false;
				expression = new UnaryExpression(OperatorType.OnesComplement, expression);
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Multiplication)) {
				if (!this.Match(CSharpTokenID.Multiplication))
					return false;
				if (!this.MatchUnaryExpression(out expression))
					return false;
				expression = new UnaryExpression(OperatorType.PointerIndirection, expression);
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.BitwiseAnd)) {
				if (!this.Match(CSharpTokenID.BitwiseAnd))
					return false;
				if (!this.MatchUnaryExpression(out expression))
					return false;
				expression = new UnaryExpression(OperatorType.AddressOf, expression);
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Increment)) {
				if (!this.Match(CSharpTokenID.Increment))
					return false;
				if (!this.MatchUnaryExpression(out expression))
					return false;
				expression = new UnaryExpression(OperatorType.PreIncrement, expression);
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Decrement)) {
				if (!this.Match(CSharpTokenID.Decrement))
					return false;
				if (!this.MatchUnaryExpression(out expression))
					return false;
				expression = new UnaryExpression(OperatorType.PreDecrement, expression);
			}
			else if (this.IsTypeCast()) {
				// NOTE: Have to do a check for type casts "( type )" since they are similar to PrimaryExpression "( expression )"
				if (!this.MatchCastExpression(out expression))
					return false;
			}
			else if ((this.IsInMultiMatchSet(9, this.LookAheadToken) || (this.IsDefaultValueExpression()))) {
				if (!this.MatchPrimaryExpression(out expression))
					return false;
			}
			else
				return false;
			if (expression != null) {
				expression.StartOffset	= startOffset;
				expression.EndOffset	= this.Token.EndOffset;
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>CastExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>CastExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>OpenParenthesis</c>.
		/// </remarks>
		protected virtual bool MatchCastExpression(out Expression expression) {
			int startOffset = this.LookAheadToken.StartOffset;
			TypeReference typeReference;
			expression = null;
			if (!this.Match(CSharpTokenID.OpenParenthesis))
				return false;
			if (!this.MatchType(out typeReference))
				return false;
			if (!this.Match(CSharpTokenID.CloseParenthesis))
				return false;
			if (!this.MatchUnaryExpression(out expression))
				return false;
			expression = new CastExpression(typeReference, expression, new TextRange(startOffset, this.Token.EndOffset));
			return true;
		}

		/// <summary>
		/// Matches a <c>MultiplicativeExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>MultiplicativeExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Multiplication</c>, <c>True</c>, <c>False</c>, <c>DecimalIntegerLiteral</c>, <c>HexadecimalIntegerLiteral</c>, <c>RealLiteral</c>, <c>CharacterLiteral</c>, <c>StringLiteral</c>, <c>VerbatimStringLiteral</c>, <c>Null</c>, <c>OpenParenthesis</c>, <c>This</c>, <c>Base</c>, <c>New</c>, <c>TypeOf</c>, <c>SizeOf</c>, <c>Checked</c>, <c>Unchecked</c>, <c>Increment</c>, <c>Decrement</c>, <c>Addition</c>, <c>Subtraction</c>, <c>Negation</c>, <c>OnesComplement</c>, <c>BitwiseAnd</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>, <c>Delegate</c>.
		/// The non-terminal can start with: this.IsDefaultValueExpression().
		/// </remarks>
		protected virtual bool MatchMultiplicativeExpression(out Expression expression) {
			int startOffset = this.LookAheadToken.StartOffset;
			Expression rightExpression;
			if (!this.MatchUnaryExpression(out expression))
				return false;
			while (((this.TokenIs(this.LookAheadToken, CSharpTokenID.Multiplication)) || (this.TokenIs(this.LookAheadToken, CSharpTokenID.Division)) || (this.TokenIs(this.LookAheadToken, CSharpTokenID.Modulus)))) {
				OperatorType operatorType = OperatorType.None;
				if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Multiplication)) {
					if (!this.Match(CSharpTokenID.Multiplication))
						return false;
					else {
						operatorType = OperatorType.Multiply;
					}
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Division)) {
					if (!this.Match(CSharpTokenID.Division))
						return false;
					else {
						operatorType = OperatorType.Division;
					}
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Modulus)) {
					if (!this.Match(CSharpTokenID.Modulus))
						return false;
					else {
						operatorType = OperatorType.Modulus;
					}
				}
				else
					return false;
				if (!this.MatchMultiplicativeExpression(out rightExpression))
					return false;
				expression = new BinaryExpression(operatorType, expression, rightExpression, new TextRange(startOffset, this.Token.EndOffset));
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>AdditiveExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>AdditiveExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Multiplication</c>, <c>True</c>, <c>False</c>, <c>DecimalIntegerLiteral</c>, <c>HexadecimalIntegerLiteral</c>, <c>RealLiteral</c>, <c>CharacterLiteral</c>, <c>StringLiteral</c>, <c>VerbatimStringLiteral</c>, <c>Null</c>, <c>OpenParenthesis</c>, <c>This</c>, <c>Base</c>, <c>New</c>, <c>TypeOf</c>, <c>SizeOf</c>, <c>Checked</c>, <c>Unchecked</c>, <c>Increment</c>, <c>Decrement</c>, <c>Addition</c>, <c>Subtraction</c>, <c>Negation</c>, <c>OnesComplement</c>, <c>BitwiseAnd</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>, <c>Delegate</c>.
		/// The non-terminal can start with: this.IsDefaultValueExpression().
		/// </remarks>
		protected virtual bool MatchAdditiveExpression(out Expression expression) {
			int startOffset = this.LookAheadToken.StartOffset;
			Expression rightExpression;
			if (!this.MatchMultiplicativeExpression(out expression))
				return false;
			while (((this.TokenIs(this.LookAheadToken, CSharpTokenID.Addition)) || (this.TokenIs(this.LookAheadToken, CSharpTokenID.Subtraction)))) {
				OperatorType operatorType = OperatorType.None;
				if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Addition)) {
					if (!this.Match(CSharpTokenID.Addition))
						return false;
					else {
						operatorType = OperatorType.Addition;
					}
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Subtraction)) {
					if (!this.Match(CSharpTokenID.Subtraction))
						return false;
					else {
						operatorType = OperatorType.Subtraction;
					}
				}
				else
					return false;
				if (!this.MatchAdditiveExpression(out rightExpression))
					return false;
				expression = new BinaryExpression(operatorType, expression, rightExpression, new TextRange(startOffset, this.Token.EndOffset));
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>ShiftExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ShiftExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Multiplication</c>, <c>True</c>, <c>False</c>, <c>DecimalIntegerLiteral</c>, <c>HexadecimalIntegerLiteral</c>, <c>RealLiteral</c>, <c>CharacterLiteral</c>, <c>StringLiteral</c>, <c>VerbatimStringLiteral</c>, <c>Null</c>, <c>OpenParenthesis</c>, <c>This</c>, <c>Base</c>, <c>New</c>, <c>TypeOf</c>, <c>SizeOf</c>, <c>Checked</c>, <c>Unchecked</c>, <c>Increment</c>, <c>Decrement</c>, <c>Addition</c>, <c>Subtraction</c>, <c>Negation</c>, <c>OnesComplement</c>, <c>BitwiseAnd</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>, <c>Delegate</c>.
		/// The non-terminal can start with: this.IsDefaultValueExpression().
		/// </remarks>
		protected virtual bool MatchShiftExpression(out Expression expression) {
			int startOffset = this.LookAheadToken.StartOffset;
			Expression rightExpression;
			if (!this.MatchAdditiveExpression(out expression))
				return false;
			while ((this.TokenIs(this.LookAheadToken, CSharpTokenID.LeftShift)) || (this.IsRightShift())) {
				OperatorType operatorType = OperatorType.None;
				if (this.TokenIs(this.LookAheadToken, CSharpTokenID.LeftShift)) {
					if (!this.Match(CSharpTokenID.LeftShift))
						return false;
					else {
						operatorType = OperatorType.LeftShift;
					}
				}
				else if (this.IsRightShift()) {
					if (!this.Match(CSharpTokenID.GreaterThan))
						return false;
					if (!this.Match(CSharpTokenID.GreaterThan))
						return false;
					else {
						operatorType = OperatorType.RightShift;
					}
				}
				else
					return false;
				if (!this.MatchShiftExpression(out rightExpression))
					return false;
				expression = new BinaryExpression(operatorType, expression, rightExpression, new TextRange(startOffset, this.Token.EndOffset));
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>RelationalExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>RelationalExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Multiplication</c>, <c>True</c>, <c>False</c>, <c>DecimalIntegerLiteral</c>, <c>HexadecimalIntegerLiteral</c>, <c>RealLiteral</c>, <c>CharacterLiteral</c>, <c>StringLiteral</c>, <c>VerbatimStringLiteral</c>, <c>Null</c>, <c>OpenParenthesis</c>, <c>This</c>, <c>Base</c>, <c>New</c>, <c>TypeOf</c>, <c>SizeOf</c>, <c>Checked</c>, <c>Unchecked</c>, <c>Increment</c>, <c>Decrement</c>, <c>Addition</c>, <c>Subtraction</c>, <c>Negation</c>, <c>OnesComplement</c>, <c>BitwiseAnd</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>, <c>Delegate</c>.
		/// The non-terminal can start with: this.IsDefaultValueExpression().
		/// </remarks>
		protected virtual bool MatchRelationalExpression(out Expression expression) {
			int startOffset = this.LookAheadToken.StartOffset;
			Expression rightExpression;
			if (!this.MatchShiftExpression(out expression))
				return false;
			while ((this.TokenIs(this.LookAheadToken, new int[] { CSharpTokenID.LessThan, CSharpTokenID.LessThanOrEqual, CSharpTokenID.GreaterThanOrEqual, CSharpTokenID.Is, CSharpTokenID.As })) || ((this.TokenIs(this.LookAheadToken, CSharpTokenID.GreaterThan)) && (!this.IsRightShiftAssignment()))) {
				if (((this.TokenIs(this.LookAheadToken, CSharpTokenID.LessThan)) || (this.TokenIs(this.LookAheadToken, CSharpTokenID.LessThanOrEqual)) || (this.TokenIs(this.LookAheadToken, CSharpTokenID.GreaterThanOrEqual)))) {
					OperatorType operatorType = OperatorType.None;
					if (this.TokenIs(this.LookAheadToken, CSharpTokenID.LessThan)) {
						if (!this.Match(CSharpTokenID.LessThan))
							return false;
						else {
							operatorType = OperatorType.LessThan;
						}
					}
					else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.LessThanOrEqual)) {
						if (!this.Match(CSharpTokenID.LessThanOrEqual))
							return false;
						else {
							operatorType = OperatorType.LessThanOrEqual;
						}
					}
					else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.GreaterThanOrEqual)) {
						if (!this.Match(CSharpTokenID.GreaterThanOrEqual))
							return false;
						else {
							operatorType = OperatorType.GreaterThanOrEqual;
						}
					}
					else
						return false;
					if (!this.MatchRelationalExpression(out rightExpression))
						return false;
					expression = new BinaryExpression(operatorType, expression, rightExpression, new TextRange(startOffset, this.Token.EndOffset));
				}
				else if ((this.TokenIs(this.LookAheadToken, CSharpTokenID.GreaterThan)) && (!this.IsRightShiftAssignment())) {
					OperatorType operatorType = OperatorType.None;
					if (!this.Match(CSharpTokenID.GreaterThan))
						return false;
					else {
						operatorType = OperatorType.GreaterThan;
					}
					if (!this.MatchRelationalExpression(out rightExpression))
						return false;
					expression = new BinaryExpression(operatorType, expression, rightExpression, new TextRange(startOffset, this.Token.EndOffset));
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Is)) {
					TypeReference typeReference;
					if (!this.Match(CSharpTokenID.Is))
						return false;
					if (!this.MatchTypeCore(false, true, out typeReference))
						return false;
					expression = new IsTypeOfExpression(expression, typeReference, new TextRange(startOffset, this.Token.EndOffset));
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.As)) {
					TypeReference typeReference;
					if (!this.Match(CSharpTokenID.As))
						return false;
					if (!this.MatchTypeCore(false, true, out typeReference))
						return false;
					expression = new TryCastExpression(expression, typeReference, new TextRange(startOffset, this.Token.EndOffset));
				}
				else
					return false;
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>EqualityExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>EqualityExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Multiplication</c>, <c>True</c>, <c>False</c>, <c>DecimalIntegerLiteral</c>, <c>HexadecimalIntegerLiteral</c>, <c>RealLiteral</c>, <c>CharacterLiteral</c>, <c>StringLiteral</c>, <c>VerbatimStringLiteral</c>, <c>Null</c>, <c>OpenParenthesis</c>, <c>This</c>, <c>Base</c>, <c>New</c>, <c>TypeOf</c>, <c>SizeOf</c>, <c>Checked</c>, <c>Unchecked</c>, <c>Increment</c>, <c>Decrement</c>, <c>Addition</c>, <c>Subtraction</c>, <c>Negation</c>, <c>OnesComplement</c>, <c>BitwiseAnd</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>, <c>Delegate</c>.
		/// The non-terminal can start with: this.IsDefaultValueExpression().
		/// </remarks>
		protected virtual bool MatchEqualityExpression(out Expression expression) {
			int startOffset = this.LookAheadToken.StartOffset;
			Expression rightExpression;
			if (!this.MatchRelationalExpression(out expression))
				return false;
			while (((this.TokenIs(this.LookAheadToken, CSharpTokenID.Equality)) || (this.TokenIs(this.LookAheadToken, CSharpTokenID.Inequality)))) {
				OperatorType operatorType = OperatorType.None;
				if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Equality)) {
					if (!this.Match(CSharpTokenID.Equality))
						return false;
					else {
						operatorType = OperatorType.Equality;
					}
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Inequality)) {
					if (!this.Match(CSharpTokenID.Inequality))
						return false;
					else {
						operatorType = OperatorType.Inequality;
					}
				}
				else
					return false;
				if (!this.MatchEqualityExpression(out rightExpression))
					return false;
				expression = new BinaryExpression(operatorType, expression, rightExpression, new TextRange(startOffset, this.Token.EndOffset));
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>AndExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>AndExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Multiplication</c>, <c>True</c>, <c>False</c>, <c>DecimalIntegerLiteral</c>, <c>HexadecimalIntegerLiteral</c>, <c>RealLiteral</c>, <c>CharacterLiteral</c>, <c>StringLiteral</c>, <c>VerbatimStringLiteral</c>, <c>Null</c>, <c>OpenParenthesis</c>, <c>This</c>, <c>Base</c>, <c>New</c>, <c>TypeOf</c>, <c>SizeOf</c>, <c>Checked</c>, <c>Unchecked</c>, <c>Increment</c>, <c>Decrement</c>, <c>Addition</c>, <c>Subtraction</c>, <c>Negation</c>, <c>OnesComplement</c>, <c>BitwiseAnd</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>, <c>Delegate</c>.
		/// The non-terminal can start with: this.IsDefaultValueExpression().
		/// </remarks>
		protected virtual bool MatchAndExpression(out Expression expression) {
			int startOffset = this.LookAheadToken.StartOffset;
			Expression rightExpression;
			if (!this.MatchEqualityExpression(out expression))
				return false;
			while (this.TokenIs(this.LookAheadToken, CSharpTokenID.BitwiseAnd)) {
				if (!this.Match(CSharpTokenID.BitwiseAnd))
					return false;
				if (!this.MatchAndExpression(out rightExpression))
					return false;
				expression = new BinaryExpression(OperatorType.BitwiseAnd, expression, rightExpression, new TextRange(startOffset, this.Token.EndOffset));
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>ExclusiveOrExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ExclusiveOrExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Multiplication</c>, <c>True</c>, <c>False</c>, <c>DecimalIntegerLiteral</c>, <c>HexadecimalIntegerLiteral</c>, <c>RealLiteral</c>, <c>CharacterLiteral</c>, <c>StringLiteral</c>, <c>VerbatimStringLiteral</c>, <c>Null</c>, <c>OpenParenthesis</c>, <c>This</c>, <c>Base</c>, <c>New</c>, <c>TypeOf</c>, <c>SizeOf</c>, <c>Checked</c>, <c>Unchecked</c>, <c>Increment</c>, <c>Decrement</c>, <c>Addition</c>, <c>Subtraction</c>, <c>Negation</c>, <c>OnesComplement</c>, <c>BitwiseAnd</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>, <c>Delegate</c>.
		/// The non-terminal can start with: this.IsDefaultValueExpression().
		/// </remarks>
		protected virtual bool MatchExclusiveOrExpression(out Expression expression) {
			int startOffset = this.LookAheadToken.StartOffset;
			Expression rightExpression;
			if (!this.MatchAndExpression(out expression))
				return false;
			while (this.TokenIs(this.LookAheadToken, CSharpTokenID.ExclusiveOr)) {
				if (!this.Match(CSharpTokenID.ExclusiveOr))
					return false;
				if (!this.MatchExclusiveOrExpression(out rightExpression))
					return false;
				expression = new BinaryExpression(OperatorType.ExclusiveOr, expression, rightExpression, new TextRange(startOffset, this.Token.EndOffset));
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>InclusiveOrExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>InclusiveOrExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Multiplication</c>, <c>True</c>, <c>False</c>, <c>DecimalIntegerLiteral</c>, <c>HexadecimalIntegerLiteral</c>, <c>RealLiteral</c>, <c>CharacterLiteral</c>, <c>StringLiteral</c>, <c>VerbatimStringLiteral</c>, <c>Null</c>, <c>OpenParenthesis</c>, <c>This</c>, <c>Base</c>, <c>New</c>, <c>TypeOf</c>, <c>SizeOf</c>, <c>Checked</c>, <c>Unchecked</c>, <c>Increment</c>, <c>Decrement</c>, <c>Addition</c>, <c>Subtraction</c>, <c>Negation</c>, <c>OnesComplement</c>, <c>BitwiseAnd</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>, <c>Delegate</c>.
		/// The non-terminal can start with: this.IsDefaultValueExpression().
		/// </remarks>
		protected virtual bool MatchInclusiveOrExpression(out Expression expression) {
			int startOffset = this.LookAheadToken.StartOffset;
			Expression rightExpression;
			if (!this.MatchExclusiveOrExpression(out expression))
				return false;
			while (this.TokenIs(this.LookAheadToken, CSharpTokenID.BitwiseOr)) {
				if (!this.Match(CSharpTokenID.BitwiseOr))
					return false;
				if (!this.MatchInclusiveOrExpression(out rightExpression))
					return false;
				expression = new BinaryExpression(OperatorType.BitwiseOr, expression, rightExpression, new TextRange(startOffset, this.Token.EndOffset));
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>ConditionalAndExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ConditionalAndExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Multiplication</c>, <c>True</c>, <c>False</c>, <c>DecimalIntegerLiteral</c>, <c>HexadecimalIntegerLiteral</c>, <c>RealLiteral</c>, <c>CharacterLiteral</c>, <c>StringLiteral</c>, <c>VerbatimStringLiteral</c>, <c>Null</c>, <c>OpenParenthesis</c>, <c>This</c>, <c>Base</c>, <c>New</c>, <c>TypeOf</c>, <c>SizeOf</c>, <c>Checked</c>, <c>Unchecked</c>, <c>Increment</c>, <c>Decrement</c>, <c>Addition</c>, <c>Subtraction</c>, <c>Negation</c>, <c>OnesComplement</c>, <c>BitwiseAnd</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>, <c>Delegate</c>.
		/// The non-terminal can start with: this.IsDefaultValueExpression().
		/// </remarks>
		protected virtual bool MatchConditionalAndExpression(out Expression expression) {
			int startOffset = this.LookAheadToken.StartOffset;
			Expression rightExpression;
			if (!this.MatchInclusiveOrExpression(out expression))
				return false;
			while (this.TokenIs(this.LookAheadToken, CSharpTokenID.ConditionalAnd)) {
				if (!this.Match(CSharpTokenID.ConditionalAnd))
					return false;
				if (!this.MatchConditionalAndExpression(out rightExpression))
					return false;
				expression = new BinaryExpression(OperatorType.ConditionalAnd, expression, rightExpression, new TextRange(startOffset, this.Token.EndOffset));
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>ConditionalOrExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ConditionalOrExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Multiplication</c>, <c>True</c>, <c>False</c>, <c>DecimalIntegerLiteral</c>, <c>HexadecimalIntegerLiteral</c>, <c>RealLiteral</c>, <c>CharacterLiteral</c>, <c>StringLiteral</c>, <c>VerbatimStringLiteral</c>, <c>Null</c>, <c>OpenParenthesis</c>, <c>This</c>, <c>Base</c>, <c>New</c>, <c>TypeOf</c>, <c>SizeOf</c>, <c>Checked</c>, <c>Unchecked</c>, <c>Increment</c>, <c>Decrement</c>, <c>Addition</c>, <c>Subtraction</c>, <c>Negation</c>, <c>OnesComplement</c>, <c>BitwiseAnd</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>, <c>Delegate</c>.
		/// The non-terminal can start with: this.IsDefaultValueExpression().
		/// </remarks>
		protected virtual bool MatchConditionalOrExpression(out Expression expression) {
			int startOffset = this.LookAheadToken.StartOffset;
			Expression rightExpression;
			if (!this.MatchConditionalAndExpression(out expression))
				return false;
			while (this.TokenIs(this.LookAheadToken, CSharpTokenID.ConditionalOr)) {
				if (!this.Match(CSharpTokenID.ConditionalOr))
					return false;
				if (!this.MatchConditionalOrExpression(out rightExpression))
					return false;
				expression = new BinaryExpression(OperatorType.ConditionalOr, expression, rightExpression, new TextRange(startOffset, this.Token.EndOffset));
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>ConditionalExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ConditionalExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Multiplication</c>, <c>True</c>, <c>False</c>, <c>DecimalIntegerLiteral</c>, <c>HexadecimalIntegerLiteral</c>, <c>RealLiteral</c>, <c>CharacterLiteral</c>, <c>StringLiteral</c>, <c>VerbatimStringLiteral</c>, <c>Null</c>, <c>OpenParenthesis</c>, <c>This</c>, <c>Base</c>, <c>New</c>, <c>TypeOf</c>, <c>SizeOf</c>, <c>Checked</c>, <c>Unchecked</c>, <c>Increment</c>, <c>Decrement</c>, <c>Addition</c>, <c>Subtraction</c>, <c>Negation</c>, <c>OnesComplement</c>, <c>BitwiseAnd</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>, <c>Delegate</c>.
		/// The non-terminal can start with: this.IsDefaultValueExpression().
		/// </remarks>
		protected virtual bool MatchConditionalExpression(out Expression expression) {
			int startOffset = this.LookAheadToken.StartOffset;
			if (!this.MatchConditionalOrExpression(out expression))
				return false;
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.NullCoalescing)) {
				Expression nullCoalescingExpression;
				if (!this.Match(CSharpTokenID.NullCoalescing))
					return false;
				if (!this.MatchExpression(out nullCoalescingExpression))
					return false;
				expression = new BinaryExpression(OperatorType.NullCoalescing, expression, nullCoalescingExpression, new TextRange(startOffset, this.Token.EndOffset));
			}
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.QuestionMark)) {
				Expression trueExpression, falseExpression;
				if (!this.Match(CSharpTokenID.QuestionMark))
					return false;
				if (!this.MatchExpression(out trueExpression))
					return false;
				if (!this.Match(CSharpTokenID.Colon))
					return false;
				if (!this.MatchExpression(out falseExpression))
					return false;
				expression = new ConditionalExpression(expression, trueExpression, falseExpression, new TextRange(startOffset, this.Token.EndOffset));
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>AssignmentOperator</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>AssignmentOperator</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Assignment</c>, <c>AdditionAssignment</c>, <c>SubtractionAssignment</c>, <c>MultiplicationAssignment</c>, <c>DivisionAssignment</c>, <c>ModulusAssignment</c>, <c>BitwiseAndAssignment</c>, <c>BitwiseOrAssignment</c>, <c>ExclusiveOrAssignment</c>, <c>LeftShiftAssignment</c>.
		/// The non-terminal can start with: this.IsRightShiftAssignment().
		/// </remarks>
		protected virtual bool MatchAssignmentOperator(out OperatorType operatorType) {
			operatorType = OperatorType.None;
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Assignment)) {
				if (!this.Match(CSharpTokenID.Assignment))
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.AdditionAssignment)) {
				if (!this.Match(CSharpTokenID.AdditionAssignment))
					return false;
				else {
					operatorType = OperatorType.Addition;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.SubtractionAssignment)) {
				if (!this.Match(CSharpTokenID.SubtractionAssignment))
					return false;
				else {
					operatorType = OperatorType.Subtraction;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.MultiplicationAssignment)) {
				if (!this.Match(CSharpTokenID.MultiplicationAssignment))
					return false;
				else {
					operatorType = OperatorType.Multiply;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.DivisionAssignment)) {
				if (!this.Match(CSharpTokenID.DivisionAssignment))
					return false;
				else {
					operatorType = OperatorType.Division;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.ModulusAssignment)) {
				if (!this.Match(CSharpTokenID.ModulusAssignment))
					return false;
				else {
					operatorType = OperatorType.Modulus;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.BitwiseAndAssignment)) {
				if (!this.Match(CSharpTokenID.BitwiseAndAssignment))
					return false;
				else {
					operatorType = OperatorType.BitwiseAnd;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.BitwiseOrAssignment)) {
				if (!this.Match(CSharpTokenID.BitwiseOrAssignment))
					return false;
				else {
					operatorType = OperatorType.BitwiseOr;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.ExclusiveOrAssignment)) {
				if (!this.Match(CSharpTokenID.ExclusiveOrAssignment))
					return false;
				else {
					operatorType = OperatorType.ExclusiveOr;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.LeftShiftAssignment)) {
				if (!this.Match(CSharpTokenID.LeftShiftAssignment))
					return false;
				else {
					operatorType = OperatorType.LeftShift;
				}
			}
			else if (this.IsRightShiftAssignment()) {
				// NOTE: This handles ambiguity between right shift and generic type specifications in .NET 2.0
				if (!this.Match(CSharpTokenID.GreaterThan))
					return false;
				if (!this.Match(CSharpTokenID.GreaterThanOrEqual))
					return false;
				else {
					operatorType = OperatorType.RightShift;
				}
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>NonAssignmentExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>NonAssignmentExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Multiplication</c>, <c>True</c>, <c>False</c>, <c>DecimalIntegerLiteral</c>, <c>HexadecimalIntegerLiteral</c>, <c>RealLiteral</c>, <c>CharacterLiteral</c>, <c>StringLiteral</c>, <c>VerbatimStringLiteral</c>, <c>Null</c>, <c>OpenParenthesis</c>, <c>This</c>, <c>Base</c>, <c>New</c>, <c>TypeOf</c>, <c>SizeOf</c>, <c>Checked</c>, <c>Unchecked</c>, <c>Increment</c>, <c>Decrement</c>, <c>Addition</c>, <c>Subtraction</c>, <c>Negation</c>, <c>OnesComplement</c>, <c>BitwiseAnd</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>, <c>Delegate</c>.
		/// The non-terminal can start with: this.IsLambdaExpression().
		/// The non-terminal can start with: this.IsQueryExpression().
		/// The non-terminal can start with: this.IsDefaultValueExpression().
		/// </remarks>
		protected virtual bool MatchNonAssignmentExpression(out Expression expression) {
			expression = null;
			if (((this.IsLambdaExpression()))) {
				if (!this.MatchLambdaExpression(out expression))
					return false;
			}
			else if (((this.IsQueryExpression()))) {
				if (!this.MatchQueryExpression(out expression))
					return false;
			}
			else if ((this.IsInMultiMatchSet(0, this.LookAheadToken) || (this.IsDefaultValueExpression()))) {
				if (!this.MatchConditionalExpression(out expression))
					return false;
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>Expression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>Expression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Multiplication</c>, <c>True</c>, <c>False</c>, <c>DecimalIntegerLiteral</c>, <c>HexadecimalIntegerLiteral</c>, <c>RealLiteral</c>, <c>CharacterLiteral</c>, <c>StringLiteral</c>, <c>VerbatimStringLiteral</c>, <c>Null</c>, <c>OpenParenthesis</c>, <c>This</c>, <c>Base</c>, <c>New</c>, <c>TypeOf</c>, <c>SizeOf</c>, <c>Checked</c>, <c>Unchecked</c>, <c>Increment</c>, <c>Decrement</c>, <c>Addition</c>, <c>Subtraction</c>, <c>Negation</c>, <c>OnesComplement</c>, <c>BitwiseAnd</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>, <c>Delegate</c>.
		/// The non-terminal can start with: this.IsLambdaExpression().
		/// The non-terminal can start with: this.IsQueryExpression().
		/// The non-terminal can start with: this.IsDefaultValueExpression().
		/// </remarks>
		protected virtual bool MatchExpression(out Expression expression) {
			int startOffset = this.LookAheadToken.StartOffset;
			if (!this.MatchNonAssignmentExpression(out expression))
				return false;
			// If the result of the expression is a unary expression...
			if ((expression != null) && (!((expression is ConditionalExpression) || (expression is IsTypeOfExpression) || (expression is TryCastExpression))) && ((this.IsInMultiMatchSet(10, this.LookAheadToken) || (this.IsRightShiftAssignment())))) {
				OperatorType operatorType;
				Expression rightExpression;
				if (!this.MatchAssignmentOperator(out operatorType))
					return false;
				if (!this.MatchExpression(out rightExpression))
					return false;
				expression = new AssignmentExpression(operatorType, expression, rightExpression, new TextRange(startOffset, this.Token.EndOffset));
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>Statement</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>Statement</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Void</c>, <c>Multiplication</c>, <c>True</c>, <c>False</c>, <c>DecimalIntegerLiteral</c>, <c>HexadecimalIntegerLiteral</c>, <c>RealLiteral</c>, <c>CharacterLiteral</c>, <c>StringLiteral</c>, <c>VerbatimStringLiteral</c>, <c>Null</c>, <c>OpenParenthesis</c>, <c>This</c>, <c>Base</c>, <c>New</c>, <c>TypeOf</c>, <c>SizeOf</c>, <c>Checked</c>, <c>Unchecked</c>, <c>Increment</c>, <c>Decrement</c>, <c>Addition</c>, <c>Subtraction</c>, <c>Negation</c>, <c>OnesComplement</c>, <c>BitwiseAnd</c>, <c>SemiColon</c>, <c>If</c>, <c>Switch</c>, <c>OpenCurlyBrace</c>, <c>While</c>, <c>Do</c>, <c>For</c>, <c>ForEach</c>, <c>Break</c>, <c>Continue</c>, <c>Goto</c>, <c>Return</c>, <c>Throw</c>, <c>Try</c>, <c>Lock</c>, <c>Using</c>, <c>Yield</c>, <c>Unsafe</c>, <c>Fixed</c>, <c>Const</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>, <c>Delegate</c>.
		/// The non-terminal can start with: this.IsLambdaExpression().
		/// The non-terminal can start with: this.IsQueryExpression().
		/// The non-terminal can start with: this.IsDefaultValueExpression().
		/// </remarks>
		protected virtual bool MatchStatement(out Statement statement) {
			statement = null;
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Const)) {
				if (!this.MatchLocalConstantDeclaration(out statement))
					return false;
				this.Match(CSharpTokenID.SemiColon);
				statement.EndOffset = this.Token.EndOffset;
			}
			else if (this.AreNextTwoIdentifierAnd(CSharpTokenID.Colon)) {
				if (!this.MatchLabeledStatement(out statement))
					return false;
			}
			else if ((this.IsVariableDeclaration()) && (!this.TokenIs(this.LookAheadToken, CSharpTokenID.From))) {
				if (!this.MatchLocalVariableDeclaration(out statement))
					return false;
				this.Match(CSharpTokenID.SemiColon);
				statement.EndOffset = this.Token.EndOffset;
			}
			else if ((this.IsInMultiMatchSet(11, this.LookAheadToken) || (this.IsLambdaExpression()) || (this.IsQueryExpression()) || (this.IsDefaultValueExpression()))) {
				if (!this.MatchEmbeddedStatement(out statement))
					return false;
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>EmbeddedStatement</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>EmbeddedStatement</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Multiplication</c>, <c>True</c>, <c>False</c>, <c>DecimalIntegerLiteral</c>, <c>HexadecimalIntegerLiteral</c>, <c>RealLiteral</c>, <c>CharacterLiteral</c>, <c>StringLiteral</c>, <c>VerbatimStringLiteral</c>, <c>Null</c>, <c>OpenParenthesis</c>, <c>This</c>, <c>Base</c>, <c>New</c>, <c>TypeOf</c>, <c>SizeOf</c>, <c>Checked</c>, <c>Unchecked</c>, <c>Increment</c>, <c>Decrement</c>, <c>Addition</c>, <c>Subtraction</c>, <c>Negation</c>, <c>OnesComplement</c>, <c>BitwiseAnd</c>, <c>SemiColon</c>, <c>If</c>, <c>Switch</c>, <c>OpenCurlyBrace</c>, <c>While</c>, <c>Do</c>, <c>For</c>, <c>ForEach</c>, <c>Break</c>, <c>Continue</c>, <c>Goto</c>, <c>Return</c>, <c>Throw</c>, <c>Try</c>, <c>Lock</c>, <c>Using</c>, <c>Yield</c>, <c>Unsafe</c>, <c>Fixed</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>, <c>Delegate</c>.
		/// The non-terminal can start with: this.IsLambdaExpression().
		/// The non-terminal can start with: this.IsQueryExpression().
		/// The non-terminal can start with: this.IsDefaultValueExpression().
		/// </remarks>
		protected virtual bool MatchEmbeddedStatement(out Statement statement) {
			statement = null;
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenCurlyBrace)) {
				if (!this.MatchBlock(out statement))
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.SemiColon)) {
				// Empty statement
				if (!this.Match(CSharpTokenID.SemiColon))
					return false;
				statement = new EmptyStatement(this.Token.TextRange);
			}
			else if (this.AreNextTwo(CSharpTokenID.Checked, CSharpTokenID.OpenCurlyBrace)) {
				// Checked statement (moved up here with special condition since Expression has 'checked' in its first set)
					Statement embeddedStatement;
				int startOffset = this.LookAheadToken.StartOffset;
				if (!this.Match(CSharpTokenID.Checked))
					return false;
				if (!this.MatchBlock(out embeddedStatement))
					return false;
				statement = new CheckedStatement(embeddedStatement, new TextRange(startOffset, this.Token.EndOffset));
			}
			else if (this.AreNextTwo(CSharpTokenID.Unchecked, CSharpTokenID.OpenCurlyBrace)) {
				// Unchecked statement (moved up here with special condition since Expression has 'unchecked' in its first set)
					Statement embeddedStatement;
				int startOffset = this.LookAheadToken.StartOffset;
				if (!this.Match(CSharpTokenID.Unchecked))
					return false;
				if (!this.MatchBlock(out embeddedStatement))
					return false;
				statement = new UncheckedStatement(embeddedStatement, new TextRange(startOffset, this.Token.EndOffset));
			}
			else if ((this.IsInMultiMatchSet(0, this.LookAheadToken) || (this.IsLambdaExpression()) || (this.IsQueryExpression()) || (this.IsDefaultValueExpression()))) {
				// Expression statement
				if (!this.MatchStatementExpression(out statement))
					return false;
				if (!this.Match(CSharpTokenID.SemiColon))
					return false;
				statement.EndOffset = this.Token.EndOffset;
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.If)) {
				// If statement
				Expression condition;
				Statement trueStatement;
				Statement falseStatement = null;
				int startOffset = this.LookAheadToken.StartOffset;
				if (!this.Match(CSharpTokenID.If))
					return false;
				if (!this.Match(CSharpTokenID.OpenParenthesis))
					return false;
				if (!this.MatchExpression(out condition))
					return false;
				if (!this.Match(CSharpTokenID.CloseParenthesis))
					return false;
				if (!this.MatchEmbeddedStatement(out trueStatement))
					return false;
				if (this.AreNextTwo(CSharpTokenID.Else, CSharpTokenID.If)) {
					Stack elseIfStatementStack = new Stack();
					while (this.AreNextTwo(CSharpTokenID.Else, CSharpTokenID.If)) {
						Expression elseIfCondition;
						Statement elseIfTrueStatement;
						int elseIfStartOffset = this.LookAheadToken.StartOffset;
						if (!this.Match(CSharpTokenID.Else))
							return false;
						if (!this.Match(CSharpTokenID.If))
							return false;
						if (!this.Match(CSharpTokenID.OpenParenthesis))
							return false;
						if (!this.MatchExpression(out elseIfCondition))
							return false;
						if (!this.Match(CSharpTokenID.CloseParenthesis))
							return false;
						if (!this.MatchEmbeddedStatement(out elseIfTrueStatement))
							return false;
						falseStatement = new IfStatement(elseIfCondition, elseIfTrueStatement);
						falseStatement.StartOffset = elseIfStartOffset;
						elseIfStatementStack.Push(falseStatement);
					}
					
					falseStatement = null;
					if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Else)) {
						if (!this.Match(CSharpTokenID.Else))
							return false;
						if (!this.MatchEmbeddedStatement(out falseStatement))
							return false;
					}
					int elseIfEndOffset = this.Token.EndOffset;
					while (elseIfStatementStack.Count > 0) {
						IfStatement parentElseIfStatement = (IfStatement)elseIfStatementStack.Pop();
						parentElseIfStatement.EndOffset = elseIfEndOffset;
						parentElseIfStatement.FalseStatement = falseStatement;
						falseStatement = parentElseIfStatement;
					}
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Else)) {
					if (!this.Match(CSharpTokenID.Else))
						return false;
					if (!this.MatchEmbeddedStatement(out falseStatement))
						return false;
				}
				statement = new IfStatement(condition, trueStatement, falseStatement, new TextRange(startOffset, this.Token.EndOffset));
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Switch)) {
				// Switch statement
				Expression expression;
				AstNodeList sections = new AstNodeList(null);
				SwitchSection switchSection;
				int startOffset = this.LookAheadToken.StartOffset;
				if (!this.Match(CSharpTokenID.Switch))
					return false;
				if (!this.Match(CSharpTokenID.OpenParenthesis))
					return false;
				if (!this.MatchExpression(out expression))
					return false;
				if (!this.Match(CSharpTokenID.CloseParenthesis))
					return false;
				int statementCurlyBraceLevel = curlyBraceLevel;
				if (!this.Match(CSharpTokenID.OpenCurlyBrace))
					return false;
				bool errorReported = false;
				while (!this.IsAtEnd) {
					if ((this.TokenIs(this.LookAheadToken, CSharpTokenID.CloseCurlyBrace)) && (curlyBraceLevel == statementCurlyBraceLevel + 1))
						break;
					else if ((this.IsInMultiMatchSet(12, this.LookAheadToken) || (this.IsLambdaExpression()) || (this.IsQueryExpression()) || (this.IsDefaultValueExpression()))) {
						errorReported = false;
						while ((this.IsInMultiMatchSet(13, this.LookAheadToken) || (this.IsLambdaExpression()) || (this.IsQueryExpression()) || (this.IsDefaultValueExpression()))) {
							if (!this.MatchSwitchSection(out switchSection)) {
								if (!this.TokenIs(this.LookAheadToken, CSharpTokenID.CloseCurlyBrace)) this.AdvanceToNext();
							}
							else {
								sections.Add(switchSection);
							}
						}
					}
					else {
						// Error recovery:  Advance to the next token since nothing was matched
						if (!errorReported) {
							this.ReportSyntaxError(AssemblyInfo.Instance.Resources.GetString("SemanticParserError_SwitchSectionExpected"));
							errorReported = true;
						}
						this.AdvanceToNext();
					}
				}
				this.Match(CSharpTokenID.CloseCurlyBrace);
				statement = new SwitchStatement(expression, new TextRange(startOffset, this.Token.EndOffset));
				((SwitchStatement)statement).Sections.AddRange(sections.ToArray());
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.While)) {
				// While statement
				Expression expression;
				Statement embeddedStatement;
				int startOffset = this.LookAheadToken.StartOffset;
				if (!this.Match(CSharpTokenID.While))
					return false;
				if (!this.Match(CSharpTokenID.OpenParenthesis))
					return false;
				if (!this.MatchExpression(out expression))
					return false;
				if (!this.Match(CSharpTokenID.CloseParenthesis))
					return false;
				this.MatchStatement(out embeddedStatement);
				statement = new WhileStatement(expression, embeddedStatement, new TextRange(startOffset, this.Token.EndOffset));
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Do)) {
				// Do statement
				Statement embeddedStatement;
				Expression expression;
				int startOffset = this.LookAheadToken.StartOffset;
				if (!this.Match(CSharpTokenID.Do))
					return false;
				if (!this.MatchStatement(out embeddedStatement))
					return false;
				if (!this.Match(CSharpTokenID.While))
					return false;
				if (!this.Match(CSharpTokenID.OpenParenthesis))
					return false;
				if (!this.MatchExpression(out expression))
					return false;
				if (!this.Match(CSharpTokenID.CloseParenthesis))
					return false;
				if (!this.Match(CSharpTokenID.SemiColon))
					return false;
				statement = new DoStatement(embeddedStatement, expression, new TextRange(startOffset, this.Token.EndOffset));
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.For)) {
				// For statement
				Statement embeddedStatement;
				Expression expression;
				statement = new ForStatement();
				statement.StartOffset = this.LookAheadToken.StartOffset;
				if (!this.Match(CSharpTokenID.For))
					return false;
				if (!this.Match(CSharpTokenID.OpenParenthesis))
					return false;
				if (this.IsVariableDeclaration()) {
					Statement initializerVariableDeclaration;
					if (!this.MatchLocalVariableDeclaration(out initializerVariableDeclaration))
						return false;
					else {
						((ForStatement)statement).Initializers.Add(initializerVariableDeclaration);
					}
				}
				if (((ForStatement)statement).Initializers.Count == 0) {
					if ((this.IsInMultiMatchSet(14, this.LookAheadToken) || (this.IsLambdaExpression()) || (this.IsQueryExpression()) || (this.IsDefaultValueExpression()))) {
						if (!this.MatchExpression(out expression))
							return false;
						else {
							((ForStatement)statement).Initializers.Add(expression);
						}
						while (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) {
							if (!this.Match(CSharpTokenID.Comma))
								return false;
							if (!this.MatchExpression(out expression))
								return false;
							else {
								((ForStatement)statement).Initializers.Add(expression);
							}
						}
					}
				}
				if (!this.Match(CSharpTokenID.SemiColon))
					return false;
				if ((this.IsInMultiMatchSet(14, this.LookAheadToken) || (this.IsLambdaExpression()) || (this.IsQueryExpression()) || (this.IsDefaultValueExpression()))) {
					if (!this.MatchExpression(out expression))
						return false;
					else {
						((ForStatement)statement).Condition = expression;
					}
				}
				if (!this.Match(CSharpTokenID.SemiColon))
					return false;
				if ((this.IsInMultiMatchSet(14, this.LookAheadToken) || (this.IsLambdaExpression()) || (this.IsQueryExpression()) || (this.IsDefaultValueExpression()))) {
					if (!this.MatchExpression(out expression))
						return false;
					else {
						((ForStatement)statement).Iterators.Add(expression);
					}
					while (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) {
						if (!this.Match(CSharpTokenID.Comma))
							return false;
						if (!this.MatchExpression(out expression))
							return false;
						else {
							((ForStatement)statement).Iterators.Add(expression);
						}
					}
				}
				if (!this.Match(CSharpTokenID.CloseParenthesis))
					return false;
				this.MatchStatement(out embeddedStatement);
				((ForStatement)statement).Statement = embeddedStatement;
				statement.EndOffset = this.Token.EndOffset;
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.ForEach)) {
				// For-each statement
				int startOffset = this.LookAheadToken.StartOffset;
				if (!this.Match(CSharpTokenID.ForEach))
					return false;
				if (!this.Match(CSharpTokenID.OpenParenthesis))
					return false;
				LocalVariableDeclaration variableDeclaration = new LocalVariableDeclaration();
				variableDeclaration.StartOffset = this.LookAheadToken.StartOffset;
				TypeReference typeReference = null;
				QualifiedIdentifier identifier;
				
				if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Var))
					this.AdvanceToNext();
				else {
					if (!this.MatchType(out typeReference))
						return false;
				}
				if (!this.MatchIdentifier(out identifier))
					return false;
				variableDeclaration.EndOffset = this.Token.EndOffset;
				Expression expression;
				Statement embeddedStatement;
				if (!this.Match(CSharpTokenID.In))
					return false;
				if (!this.MatchExpression(out expression))
					return false;
				// If in an implicitly typed declaration, try and locate the type reference
				bool isImplicitlyTyped = false;
				if (typeReference == null) {
					isImplicitlyTyped = true;
					typeReference = this.GetImplicitType(expression, false);
					if (typeReference == null)
						return false;
				}
				
				// Add the declarator
				VariableDeclarator variableDeclarator = new VariableDeclarator(typeReference, identifier, false, true);
				variableDeclarator.IsImplicitlyTyped = isImplicitlyTyped;
				variableDeclaration.Variables.Add(variableDeclarator);
				if (!this.Match(CSharpTokenID.CloseParenthesis))
					return false;
				this.MatchStatement(out embeddedStatement);
				statement = new ForEachStatement(variableDeclaration, expression, embeddedStatement, new TextRange(startOffset, this.Token.EndOffset));
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Break)) {
				// Break statement
				int startOffset = this.LookAheadToken.StartOffset;
				if (!this.Match(CSharpTokenID.Break))
					return false;
				if (!this.Match(CSharpTokenID.SemiColon))
					return false;
				statement = new BreakStatement(new TextRange(startOffset, this.Token.EndOffset));
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Continue)) {
				// Continue statement
				int startOffset = this.LookAheadToken.StartOffset;
				if (!this.Match(CSharpTokenID.Continue))
					return false;
				if (!this.Match(CSharpTokenID.SemiColon))
					return false;
				statement = new ContinueStatement(new TextRange(startOffset, this.Token.EndOffset));
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Goto)) {
				// Goto statement
				Expression expression = null;
				QualifiedIdentifier identifier = null;
				int startOffset = this.LookAheadToken.StartOffset;
				if (!this.Match(CSharpTokenID.Goto))
					return false;
				if (this.IsInMultiMatchSet(3, this.LookAheadToken)) {
					if (!this.MatchIdentifier(out identifier))
						return false;
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Case)) {
					if (!this.Match(CSharpTokenID.Case))
						return false;
					if (!this.MatchExpression(out expression))
						return false;
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Default)) {
					if (!this.Match(CSharpTokenID.Default))
						return false;
				}
				else
					return false;
				if (!this.Match(CSharpTokenID.SemiColon))
					return false;
				statement = new GotoStatement(identifier, expression, new TextRange(startOffset, this.Token.EndOffset));
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Return)) {
				// Return statement
				Expression expression = null;
				int startOffset = this.LookAheadToken.StartOffset;
				if (!this.Match(CSharpTokenID.Return))
					return false;
				if ((this.IsInMultiMatchSet(14, this.LookAheadToken) || (this.IsLambdaExpression()) || (this.IsQueryExpression()) || (this.IsDefaultValueExpression()))) {
					if (!this.MatchExpression(out expression))
						return false;
				}
				if (!this.Match(CSharpTokenID.SemiColon))
					return false;
				statement = new ReturnStatement(expression, new TextRange(startOffset, this.Token.EndOffset));
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Throw)) {
				// Throw statement
				Expression expression = null;
				int startOffset = this.LookAheadToken.StartOffset;
				if (!this.Match(CSharpTokenID.Throw))
					return false;
				if ((this.IsInMultiMatchSet(14, this.LookAheadToken) || (this.IsLambdaExpression()) || (this.IsQueryExpression()) || (this.IsDefaultValueExpression()))) {
					if (!this.MatchExpression(out expression))
						return false;
				}
				if (!this.Match(CSharpTokenID.SemiColon))
					return false;
				statement = new ThrowStatement(expression, new TextRange(startOffset, this.Token.EndOffset));
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Try)) {
				// Try statement
				Statement tryBlock;
				Statement finallyBlock = null;
				CatchClause catchClause;
				AstNodeList catchClauses = new AstNodeList(null);
				int startOffset = this.LookAheadToken.StartOffset;
				if (!this.Match(CSharpTokenID.Try))
					return false;
				if (!this.MatchBlock(out tryBlock))
					return false;
				if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Catch)) {
					if (!this.MatchCatchClause(out catchClause))
						return false;
					else {
						catchClauses.Add(catchClause);
					}
					while (this.TokenIs(this.LookAheadToken, CSharpTokenID.Catch)) {
						if (!this.MatchCatchClause(out catchClause))
							return false;
						else {
							catchClauses.Add(catchClause);
						}
					}
					if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Finally)) {
						if (!this.Match(CSharpTokenID.Finally))
							return false;
						if (!this.MatchBlock(out finallyBlock))
							return false;
					}
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Finally)) {
					if (!this.Match(CSharpTokenID.Finally))
						return false;
					if (!this.MatchBlock(out finallyBlock))
						return false;
				}
				else
					return false;
				statement = new TryStatement(tryBlock, finallyBlock, new TextRange(startOffset, this.Token.EndOffset));
				((TryStatement)statement).CatchClauses.AddRange(catchClauses.ToArray());
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Lock)) {
				// Lock statement
				Expression expression;
				Statement embeddedStatement;
				int startOffset = this.LookAheadToken.StartOffset;
				if (!this.Match(CSharpTokenID.Lock))
					return false;
				if (!this.Match(CSharpTokenID.OpenParenthesis))
					return false;
				if (!this.MatchExpression(out expression))
					return false;
				if (!this.Match(CSharpTokenID.CloseParenthesis))
					return false;
				this.MatchStatement(out embeddedStatement);
				statement = new LockStatement(expression, embeddedStatement, new TextRange(startOffset, this.Token.EndOffset));
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Using)) {
				// Using statement
				Statement variableDeclaration = null;
				Expression expression = null;
				Statement embeddedStatement;
				int startOffset = this.LookAheadToken.StartOffset;
				if (!this.Match(CSharpTokenID.Using))
					return false;
				if (!this.Match(CSharpTokenID.OpenParenthesis))
					return false;
				if (this.IsVariableDeclaration()) {
					if (!this.MatchLocalVariableDeclaration(out variableDeclaration))
						return false;
				}
				if (variableDeclaration == null) {
					if (!this.MatchExpression(out expression))
						return false;
				}
				if (!this.Match(CSharpTokenID.CloseParenthesis))
					return false;
				this.MatchStatement(out embeddedStatement);
				AstNodeList resourceAcquisitions = new AstNodeList(null);
				resourceAcquisitions.Add(variableDeclaration != null ? (AstNode)variableDeclaration : (AstNode)expression);
				statement = new UsingStatement(resourceAcquisitions, embeddedStatement, new TextRange(startOffset, this.Token.EndOffset));
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Yield)) {
				// Yield statement
				Expression expression = null;
				int startOffset = this.LookAheadToken.StartOffset;
				if (!this.Match(CSharpTokenID.Yield))
					return false;
				if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Return)) {
					if (!this.Match(CSharpTokenID.Return))
						return false;
					if ((this.IsInMultiMatchSet(14, this.LookAheadToken) || (this.IsLambdaExpression()) || (this.IsQueryExpression()) || (this.IsDefaultValueExpression()))) {
						if (!this.MatchExpression(out expression))
							return false;
					}
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Break)) {
					if (!this.Match(CSharpTokenID.Break))
						return false;
				}
				else
					return false;
				if (!this.Match(CSharpTokenID.SemiColon))
					return false;
				statement = new YieldStatement(expression, new TextRange(startOffset, this.Token.EndOffset));
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Unsafe)) {
				// Unsafe statement
				Statement blockStatement;
				int startOffset = this.LookAheadToken.StartOffset;
				if (!this.Match(CSharpTokenID.Unsafe))
					return false;
				this.MatchStatement(out blockStatement);
				statement = new UnsafeStatement(blockStatement, new TextRange(startOffset, this.Token.EndOffset));
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Fixed)) {
				// Fixed statement
				TypeReference typeReference;
				QualifiedIdentifier variableName;
				Expression expression;
				Statement embeddedStatement;
				statement = new FixedStatement();
				statement.StartOffset = this.LookAheadToken.StartOffset;
				if (!this.Match(CSharpTokenID.Fixed))
					return false;
				if (!this.Match(CSharpTokenID.OpenParenthesis))
					return false;
				if (!this.MatchType(out typeReference))
					return false;
				if (!this.MatchIdentifier(out variableName))
					return false;
				if (!this.Match(CSharpTokenID.Assignment))
					return false;
				if (!this.MatchExpression(out expression))
					return false;
				VariableDeclarator declarator = new VariableDeclarator(typeReference, variableName, false, true);
				declarator.Initializer = expression;
				((FixedStatement)statement).Declarators.Add(declarator);
				while (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) {
					if (!this.Match(CSharpTokenID.Comma))
						return false;
					if (!this.MatchIdentifier(out variableName))
						return false;
					if (!this.Match(CSharpTokenID.Assignment))
						return false;
					if (!this.MatchExpression(out expression))
						return false;
					declarator = new VariableDeclarator(typeReference, variableName, false, true);
					declarator.Initializer = expression;
					((FixedStatement)statement).Declarators.Add(declarator);
				}
				if (!this.Match(CSharpTokenID.CloseParenthesis))
					return false;
				this.MatchStatement(out embeddedStatement);
				((FixedStatement)statement).Statement = embeddedStatement;
				statement.EndOffset = this.Token.EndOffset;
			}
			else
				return false;
			if (statement != null) {
				// Reap comments
				this.ReapComments(statement.Comments, true);
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>Block</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>Block</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>OpenCurlyBrace</c>.
		/// </remarks>
		protected virtual bool MatchBlock(out Statement block) {
			block = new BlockStatement();
			block.StartOffset = this.LookAheadToken.StartOffset;
			Statement statement;
			int statementCurlyBraceLevel = curlyBraceLevel;
			if (!this.Match(CSharpTokenID.OpenCurlyBrace))
				return false;
			bool errorReported = false;
			while (!this.IsAtEnd) {
				if ((this.TokenIs(this.LookAheadToken, CSharpTokenID.CloseCurlyBrace)) && (curlyBraceLevel == statementCurlyBraceLevel + 1))
					break;
				else if ((this.IsInMultiMatchSet(15, this.LookAheadToken) || (this.IsLambdaExpression()) || (this.IsQueryExpression()) || (this.IsDefaultValueExpression()))) {
					errorReported = false;
					while ((this.IsInMultiMatchSet(16, this.LookAheadToken) || (this.IsLambdaExpression()) || (this.IsQueryExpression()) || (this.IsDefaultValueExpression()))) {
						if (!this.MatchStatement(out statement)) {
							if (!this.TokenIs(this.LookAheadToken, CSharpTokenID.CloseCurlyBrace)) this.AdvanceToNext();
						}
						else {
							((BlockStatement)block).Statements.Add(statement);
						}
					}
				}
				else {
					// Error recovery:  Advance to the next token since nothing was matched
					if (!errorReported) {
						this.ReportSyntaxError(AssemblyInfo.Instance.Resources.GetString("SemanticParserError_StatementExpected"));
						errorReported = true;
					}
					this.AdvanceToNext();
				}
			}
			
			// Reap comments
			this.ReapComments(((BlockStatement)block).Comments, false);
			this.Match(CSharpTokenID.CloseCurlyBrace);
			block.EndOffset = this.Token.EndOffset;
			return true;
		}

		/// <summary>
		/// Matches a <c>LabeledStatement</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>LabeledStatement</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>.
		/// </remarks>
		protected virtual bool MatchLabeledStatement(out Statement statement) {
			statement = null;
			QualifiedIdentifier label;
			int startOffset = this.LookAheadToken.StartOffset;
			if (!this.MatchIdentifier(out label))
				return false;
			if (!this.Match(CSharpTokenID.Colon))
				return false;
			if (!this.MatchStatement(out statement)) {
				this.ReportSyntaxError(AssemblyInfo.Instance.Resources.GetString("SemanticParserError_StatementExpected"));
			}
			statement = new LabeledStatement(new SimpleName(label.Text, label.TextRange), statement, new TextRange(startOffset, this.Token.EndOffset));
			return true;
		}

		/// <summary>
		/// Matches a <c>LocalVariableDeclaration</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>LocalVariableDeclaration</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Void</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>.
		/// </remarks>
		protected virtual bool MatchLocalVariableDeclaration(out Statement statement) {
			TypeReference typeReference = null;
			statement = new LocalVariableDeclaration();
			statement.StartOffset = this.LookAheadToken.StartOffset;
			
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Var))
				this.AdvanceToNext();
			else {
				if (!this.MatchType(out typeReference))
					return false;
			}
			if (!this.MatchLocalVariableDeclarator((LocalVariableDeclaration)statement, ref typeReference))
				return false;
			while (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) {
				if (!this.Match(CSharpTokenID.Comma))
					return false;
				if (!this.MatchLocalVariableDeclarator((LocalVariableDeclaration)statement, ref typeReference))
					return false;
			}
			statement.EndOffset = this.Token.EndOffset;
			return true;
		}

		/// <summary>
		/// Matches a <c>LocalVariableDeclarator</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>LocalVariableDeclarator</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>.
		/// </remarks>
		protected virtual bool MatchLocalVariableDeclarator(LocalVariableDeclaration variableDeclaration, ref TypeReference typeReference) {
			QualifiedIdentifier identifier;
			Expression initializer = null;
			int startOffset = this.LookAheadToken.StartOffset;
			if (!this.MatchIdentifier(out identifier))
				return false;
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Assignment)) {
				if (!this.Match(CSharpTokenID.Assignment))
					return false;
				if (!this.MatchVariableInitializer(out initializer))
					return false;
			}
			// If in an implicitly typed declaration, try and locate the type reference
			bool isImplicitlyTyped = false;
			if (typeReference == null) {
				isImplicitlyTyped = true;
				typeReference = this.GetImplicitType(initializer, false);
				if (typeReference == null)
					return false;
			}
			
			VariableDeclarator variableDeclarator = new VariableDeclarator(typeReference, identifier, false, true);
			variableDeclarator.IsImplicitlyTyped = isImplicitlyTyped;
			variableDeclarator.Initializer = initializer;
			variableDeclarator.StartOffset = startOffset;
			variableDeclarator.EndOffset = this.Token.EndOffset;
			variableDeclaration.Variables.Add(variableDeclarator);
			return true;
		}

		/// <summary>
		/// Matches a <c>LocalConstantDeclaration</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>LocalConstantDeclaration</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Const</c>.
		/// </remarks>
		protected virtual bool MatchLocalConstantDeclaration(out Statement statement) {
			TypeReference typeReference;
			statement = new LocalVariableDeclaration();
			((LocalVariableDeclaration)statement).Modifiers = Modifiers.Const;
			statement.StartOffset = this.LookAheadToken.StartOffset;
			if (!this.Match(CSharpTokenID.Const))
				return false;
			if (!this.MatchType(out typeReference))
				return false;
			if (!this.MatchLocalConstantDeclarator((LocalVariableDeclaration)statement, typeReference))
				return false;
			while (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) {
				if (!this.Match(CSharpTokenID.Comma))
					return false;
				if (!this.MatchLocalConstantDeclarator((LocalVariableDeclaration)statement, typeReference))
					return false;
			}
			statement.EndOffset = this.Token.EndOffset;
			return true;
		}

		/// <summary>
		/// Matches a <c>LocalConstantDeclarator</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>LocalConstantDeclarator</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>.
		/// </remarks>
		protected virtual bool MatchLocalConstantDeclarator(LocalVariableDeclaration constantDeclaration, TypeReference typeReference) {
			QualifiedIdentifier identifier;
			Expression initializer = null;
			int startOffset = this.LookAheadToken.StartOffset;
			if (!this.MatchIdentifier(out identifier))
				return false;
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Assignment)) {
				if (!this.Match(CSharpTokenID.Assignment))
					return false;
				if (!this.MatchExpression(out initializer))
					return false;
			}
			VariableDeclarator variableDeclarator = new VariableDeclarator(typeReference, identifier, true, true);
			variableDeclarator.Initializer = initializer;
			variableDeclarator.StartOffset = startOffset;
			variableDeclarator.EndOffset = this.Token.EndOffset;
			constantDeclaration.Variables.Add(variableDeclarator);
			return true;
		}

		/// <summary>
		/// Matches a <c>StatementExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>StatementExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Multiplication</c>, <c>True</c>, <c>False</c>, <c>DecimalIntegerLiteral</c>, <c>HexadecimalIntegerLiteral</c>, <c>RealLiteral</c>, <c>CharacterLiteral</c>, <c>StringLiteral</c>, <c>VerbatimStringLiteral</c>, <c>Null</c>, <c>OpenParenthesis</c>, <c>This</c>, <c>Base</c>, <c>New</c>, <c>TypeOf</c>, <c>SizeOf</c>, <c>Checked</c>, <c>Unchecked</c>, <c>Increment</c>, <c>Decrement</c>, <c>Addition</c>, <c>Subtraction</c>, <c>Negation</c>, <c>OnesComplement</c>, <c>BitwiseAnd</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>, <c>Delegate</c>.
		/// The non-terminal can start with: this.IsLambdaExpression().
		/// The non-terminal can start with: this.IsQueryExpression().
		/// The non-terminal can start with: this.IsDefaultValueExpression().
		/// </remarks>
		protected virtual bool MatchStatementExpression(out Statement statement) {
			statement = null;
			Expression expression;
			// NOTE: This allows matching for any expression, even though it really should be more restrictive
			if (!this.MatchExpression(out expression))
				return false;
			statement = new StatementExpression(expression);
			return true;
		}

		/// <summary>
		/// Matches a <c>SwitchSection</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>SwitchSection</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Void</c>, <c>Multiplication</c>, <c>True</c>, <c>False</c>, <c>DecimalIntegerLiteral</c>, <c>HexadecimalIntegerLiteral</c>, <c>RealLiteral</c>, <c>CharacterLiteral</c>, <c>StringLiteral</c>, <c>VerbatimStringLiteral</c>, <c>Null</c>, <c>OpenParenthesis</c>, <c>This</c>, <c>Base</c>, <c>New</c>, <c>TypeOf</c>, <c>SizeOf</c>, <c>Checked</c>, <c>Unchecked</c>, <c>Default</c>, <c>Increment</c>, <c>Decrement</c>, <c>Addition</c>, <c>Subtraction</c>, <c>Negation</c>, <c>OnesComplement</c>, <c>BitwiseAnd</c>, <c>SemiColon</c>, <c>If</c>, <c>Switch</c>, <c>OpenCurlyBrace</c>, <c>While</c>, <c>Do</c>, <c>For</c>, <c>ForEach</c>, <c>Break</c>, <c>Continue</c>, <c>Goto</c>, <c>Case</c>, <c>Return</c>, <c>Throw</c>, <c>Try</c>, <c>Lock</c>, <c>Using</c>, <c>Yield</c>, <c>Unsafe</c>, <c>Fixed</c>, <c>Const</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>, <c>Delegate</c>.
		/// The non-terminal can start with: this.IsLambdaExpression().
		/// The non-terminal can start with: this.IsQueryExpression().
		/// The non-terminal can start with: this.IsDefaultValueExpression().
		/// </remarks>
		protected virtual bool MatchSwitchSection(out SwitchSection switchSection) {
			switchSection = null;
			Statement statement;
			switchSection = new SwitchSection();
			switchSection.StartOffset = this.LookAheadToken.StartOffset;
			while (((this.TokenIs(this.LookAheadToken, CSharpTokenID.Default)) || (this.TokenIs(this.LookAheadToken, CSharpTokenID.Case)))) {
				int startOffset = this.LookAheadToken.StartOffset;
				Expression expression = null;
				if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Case)) {
					if (!this.Match(CSharpTokenID.Case))
						return false;
					if (!this.MatchExpression(out expression))
						return false;
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Default)) {
					if (!this.Match(CSharpTokenID.Default))
						return false;
				}
				else
					return false;
				if (!this.Match(CSharpTokenID.Colon))
					return false;
				switchSection.Labels.Add(new SwitchLabel(expression, new TextRange(startOffset, this.Token.EndOffset)));
			}
			while ((this.IsInMultiMatchSet(16, this.LookAheadToken) || (this.IsLambdaExpression()) || (this.IsQueryExpression()) || (this.IsDefaultValueExpression()))) {
				if (!this.MatchStatement(out statement))
					return false;
				else {
					switchSection.Statements.Add(statement);
				}
			}
			switchSection.EndOffset = this.Token.EndOffset;
			
			// Reap comments
			this.ReapComments(((SwitchSection)switchSection).Comments, false);
			return true;
		}

		/// <summary>
		/// Matches a <c>CatchClause</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>CatchClause</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Catch</c>.
		/// </remarks>
		protected virtual bool MatchCatchClause(out CatchClause catchClause) {
			catchClause = null;
			VariableDeclarator variableDeclarator = null;
			Statement catchBlock;
			int startOffset = this.LookAheadToken.StartOffset;
			if (!this.Match(CSharpTokenID.Catch))
				return false;
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenParenthesis)) {
				if (!this.Match(CSharpTokenID.OpenParenthesis))
					return false;
				TypeReference typeReference = null;
				QualifiedIdentifier variableName = null;
				int variableStartOffset = this.LookAheadToken.StartOffset;
				if (!this.MatchType(out typeReference))
					return false;
				if (this.IsInMultiMatchSet(17, this.LookAheadToken)) {
					if (!this.MatchIdentifier(out variableName))
						return false;
				}
				variableDeclarator = new VariableDeclarator(typeReference, variableName, false, true);
				variableDeclarator.TextRange = new TextRange(variableStartOffset, this.Token.EndOffset);
				if (!this.Match(CSharpTokenID.CloseParenthesis))
					return false;
			}
			if (!this.MatchBlock(out catchBlock))
				return false;
			catchClause = new CatchClause(variableDeclarator, (BlockStatement)catchBlock, new TextRange(startOffset, this.Token.EndOffset));
			return true;
		}

		/// <summary>
		/// Matches a <c>CompilationUnit</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>CompilationUnit</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>OpenSquareBrace</c>, <c>New</c>, <c>Using</c>, <c>Unsafe</c>, <c>Namespace</c>, <c>Public</c>, <c>Protected</c>, <c>Internal</c>, <c>Private</c>, <c>Abstract</c>, <c>Extern</c>, <c>Partial</c>, <c>Sealed</c>, <c>Static</c>, <c>Override</c>, <c>ReadOnly</c>, <c>Virtual</c>, <c>Volatile</c>, <c>Class</c>, <c>Struct</c>, <c>Interface</c>, <c>Enum</c>, <c>Delegate</c>.
		/// </remarks>
		protected virtual bool MatchCompilationUnit() {
			blockStack = new Stack();
			curlyBraceLevel = 0;
			compilationUnit = new CompilationUnit();
			compilationUnit.SourceLanguage = DotNetLanguage.CSharp;
			compilationUnit.StartOffset = this.LookAheadToken.StartOffset;
			while (this.TokenIs(this.LookAheadToken, CSharpTokenID.Extern)) {
				if (!this.MatchExternAliasDirective()) {
					// Error recovery:  Go to the next extern keyword, using keyword, or token that starts a GlobalAttributeSection or NamespaceMemberDeclaration
					while (!this.IsAtEnd) {
						if ((this.TokenIs(this.LookAheadToken, CSharpTokenID.Extern)) || (this.TokenIs(this.LookAheadToken, CSharpTokenID.Using)) ||
						(this.IsGlobalAttributeSection()) || (this.IsInMultiMatchSet(18, this.LookAheadToken)))
							break;
						this.AdvanceToNext();
					}
				}
			}
			while (this.TokenIs(this.LookAheadToken, CSharpTokenID.Using)) {
				if (!this.MatchUsingDirective(compilationUnit)) {
					// Error recovery:  Go to the next using keyword, or token that starts a GlobalAttributeSection or NamespaceMemberDeclaration
					while (!this.IsAtEnd) {
						if ((this.TokenIs(this.LookAheadToken, CSharpTokenID.Using)) || (this.IsGlobalAttributeSection()) || (this.IsInMultiMatchSet(18, this.LookAheadToken)))
							break;
						this.AdvanceToNext();
					}
				}
			}
			// Reap comments
			this.ReapComments(compilationUnit.Comments, false);
			while (this.IsGlobalAttributeSection()) {
				if (!this.MatchGlobalAttributeSection())
					return false;
			}
			bool errorReported = false;
			while (!this.IsAtEnd) {
				// Check for using statements in the wrong location
				if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Extern)) {
					this.ReportSyntaxError(this.LookAheadToken.TextRange, AssemblyInfo.Instance.Resources.GetString("SemanticParserError_ExternalAliasBeforeNamespace"));
					this.AdvanceToNext(CSharpTokenID.SemiColon);
					this.AdvanceToNext();
					continue;
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Using)) {
					this.ReportSyntaxError(this.LookAheadToken.TextRange, AssemblyInfo.Instance.Resources.GetString("SemanticParserError_UsingBeforeNamespace"));
					this.AdvanceToNext(CSharpTokenID.SemiColon);
					this.AdvanceToNext();
					continue;
				}
				else if (this.IsInMultiMatchSet(18, this.LookAheadToken)) {
					errorReported = false;
					while (this.IsInMultiMatchSet(19, this.LookAheadToken)) {
						if (!this.MatchNamespaceMemberDeclaration())
							return false;
					}
				}
				else {
					// Error recovery:  Advance to the next token since nothing was matched
					if (!errorReported) {
						this.ReportSyntaxError(AssemblyInfo.Instance.Resources.GetString("SemanticParserError_NamespaceMemberDeclarationExpected"));
						errorReported = true;
					}
					this.AdvanceToNext();
				}
			}
			compilationUnit.EndOffset = this.LookAheadToken.EndOffset;
			blockStack = null;
			
			// Reap comments
			this.ReapComments(compilationUnit.Comments, false);
			
			// Get the comment and region text ranges
			if (this.LexicalParser is CSharpRecursiveDescentLexicalParser) {
				compilationUnit.DocumentationCommentTextRanges = ((CSharpRecursiveDescentLexicalParser)this.LexicalParser).DocumentationCommentTextRanges;
				compilationUnit.MultiLineCommentTextRanges = ((CSharpRecursiveDescentLexicalParser)this.LexicalParser).MultiLineCommentTextRanges;
				compilationUnit.RegionTextRanges = ((CSharpRecursiveDescentLexicalParser)this.LexicalParser).RegionTextRanges;
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>NamespaceDeclaration</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>NamespaceDeclaration</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Namespace</c>.
		/// </remarks>
		protected virtual bool MatchNamespaceDeclaration() {
			NamespaceDeclaration namespaceDeclaration = new NamespaceDeclaration();
			if (!this.Match(CSharpTokenID.Namespace))
				return false;
			namespaceDeclaration.StartOffset = this.Token.StartOffset;
			QualifiedIdentifier name;
			if (!this.MatchQualifiedIdentifier(out name))
				return false;
			namespaceDeclaration.Name = name;
			this.BlockAddChild(namespaceDeclaration);
			this.BlockStart(namespaceDeclaration);
			namespaceDeclaration.BlockStartOffset = this.LookAheadToken.StartOffset;
			int namespaceCurlyBraceLevel = curlyBraceLevel;
			this.ReapDocumentationComments();
			if (!this.Match(CSharpTokenID.OpenCurlyBrace)) {
				// Error recovery:  Go to the next open curly brace and then find the next matching close curly brace
				this.AdvanceToNext(CSharpTokenID.OpenCurlyBrace);
				this.AdvanceToNextCloseCurlyBrace(namespaceCurlyBraceLevel, false);
			}
			while (this.TokenIs(this.LookAheadToken, CSharpTokenID.Using)) {
				if (!this.MatchUsingDirective(namespaceDeclaration)) {
					// Error recovery:  Go past the next close curly brace, using keyword, or token that starts NamespaceMemberDeclaration
					while (!this.IsAtEnd) {
						if ((this.TokenIs(this.LookAheadToken, CSharpTokenID.CloseCurlyBrace)) || (this.TokenIs(this.LookAheadToken, CSharpTokenID.Using)) || (this.IsInMultiMatchSet(18, this.LookAheadToken)))
							break;
						this.AdvanceToNext();
					}
				}
			}
			while (this.IsInMultiMatchSet(19, this.LookAheadToken)) {
				if (!this.MatchNamespaceMemberDeclaration()) {
					// Error recovery:  Go past the next matching close curly brace
					this.AdvanceToNextCloseCurlyBrace(namespaceCurlyBraceLevel, true);
				}
			}
			this.ReapDocumentationComments();
			if (!this.Match(CSharpTokenID.CloseCurlyBrace)) {
				// Error recovery:  Go past the next matching close curly brace
				this.AdvanceToNextCloseCurlyBrace(namespaceCurlyBraceLevel, true);
			}
			namespaceDeclaration.BlockEndOffset = this.Token.EndOffset;
			this.BlockEnd();
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.SemiColon)) {
				this.Match(CSharpTokenID.SemiColon);
			}
			namespaceDeclaration.EndOffset = this.Token.EndOffset;
			
			// Reap comments
			this.ReapComments(namespaceDeclaration.Comments, false);
			return true;
		}

		/// <summary>
		/// Matches a <c>QualifiedIdentifier</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>QualifiedIdentifier</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>.
		/// </remarks>
		protected virtual bool MatchQualifiedIdentifier(out QualifiedIdentifier identifier) {
			identifier = null;
			int startOffset = this.LookAheadToken.StartOffset;
			this.MatchSimpleIdentifier();
			identifierStringBuilder.Length = 0;
			identifierStringBuilder.Append(this.TokenText);
			while (this.IsQualifierIdentifierContinuation()) {
				if (!this.Match(CSharpTokenID.Dot))
					return false;
				if (!this.MatchSimpleIdentifier())
					return false;
				identifierStringBuilder.Append(".");
				identifierStringBuilder.Append(this.TokenText);
			}
			identifier = new QualifiedIdentifier(identifierStringBuilder.ToString(), new TextRange(startOffset, this.Token.EndOffset));
			return true;
		}

		/// <summary>
		/// Matches a <c>SimpleIdentifier</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>SimpleIdentifier</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>.
		/// </remarks>
		protected virtual bool MatchSimpleIdentifier() {
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Identifier)) {
				this.Match(CSharpTokenID.Identifier);
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Ascending)) {
				this.Match(CSharpTokenID.Ascending);
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.By)) {
				this.Match(CSharpTokenID.By);
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Descending)) {
				this.Match(CSharpTokenID.Descending);
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Equals)) {
				this.Match(CSharpTokenID.Equals);
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.From)) {
				this.Match(CSharpTokenID.From);
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Group)) {
				this.Match(CSharpTokenID.Group);
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Into)) {
				this.Match(CSharpTokenID.Into);
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Join)) {
				this.Match(CSharpTokenID.Join);
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Let)) {
				this.Match(CSharpTokenID.Let);
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.On)) {
				this.Match(CSharpTokenID.On);
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OrderBy)) {
				this.Match(CSharpTokenID.OrderBy);
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Select)) {
				this.Match(CSharpTokenID.Select);
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Where)) {
				this.Match(CSharpTokenID.Where);
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Var)) {
				this.Match(CSharpTokenID.Var);
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>Identifier</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>Identifier</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>.
		/// </remarks>
		protected virtual bool MatchIdentifier(out QualifiedIdentifier identifier) {
			identifier = new QualifiedIdentifier(this.LookAheadTokenText, this.LookAheadToken.TextRange);
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Identifier)) {
				this.Match(CSharpTokenID.Identifier);
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Ascending)) {
				this.Match(CSharpTokenID.Ascending);
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.By)) {
				this.Match(CSharpTokenID.By);
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Descending)) {
				this.Match(CSharpTokenID.Descending);
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Equals)) {
				this.Match(CSharpTokenID.Equals);
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.From)) {
				this.Match(CSharpTokenID.From);
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Group)) {
				this.Match(CSharpTokenID.Group);
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Into)) {
				this.Match(CSharpTokenID.Into);
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Join)) {
				this.Match(CSharpTokenID.Join);
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Let)) {
				this.Match(CSharpTokenID.Let);
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.On)) {
				this.Match(CSharpTokenID.On);
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OrderBy)) {
				this.Match(CSharpTokenID.OrderBy);
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Select)) {
				this.Match(CSharpTokenID.Select);
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Where)) {
				this.Match(CSharpTokenID.Where);
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Var)) {
				this.Match(CSharpTokenID.Var);
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>UsingDirective</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>UsingDirective</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Using</c>.
		/// </remarks>
		protected virtual bool MatchUsingDirective(AstNode parentNode) {
			this.Match(CSharpTokenID.Using);
			this.ReapDocumentationComments();
			
			UsingDirectiveSection usingDirectives = (parentNode is CompilationUnit ? ((CompilationUnit)parentNode).UsingDirectives : ((NamespaceDeclaration)parentNode).UsingDirectives);
			if (usingDirectives == null) {
				usingDirectives = new UsingDirectiveSection();
				if (parentNode is CompilationUnit)
					((CompilationUnit)parentNode).UsingDirectives = usingDirectives;
				else
					((NamespaceDeclaration)parentNode).UsingDirectives = usingDirectives;
			}
			
			if (usingDirectives.Directives.Count == 0)
				usingDirectives.StartOffset = this.Token.StartOffset;
			if (this.AreNextTwoIdentifierAnd(CSharpTokenID.Assignment)) {
				this.MatchUsingAliasDirective(usingDirectives);
			}
			else {
				this.MatchUsingNamespaceDirective(usingDirectives);
			}
			usingDirectives.EndOffset = this.Token.EndOffset;
			return true;
		}

		/// <summary>
		/// Matches a <c>UsingAliasDirective</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>UsingAliasDirective</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>.
		/// </remarks>
		protected virtual bool MatchUsingAliasDirective(UsingDirectiveSection usingDirectives) {
			int startOffset = this.Token.StartOffset;
			this.MatchSimpleIdentifier();
			string alias = this.TokenText;
			TypeReference typeReference;
			this.Match(CSharpTokenID.Assignment);
			if (!this.MatchTypeName(true, out typeReference))
				return false;
			this.Match(CSharpTokenID.SemiColon);
			UsingDirective usingDirective = new UsingDirective(new TextRange(startOffset, this.Token.EndOffset));
			
			// Reap comments
			this.ReapComments(usingDirective.Comments, false);
			
			usingDirective.NamespaceName = new QualifiedIdentifier(typeReference.Name, typeReference.TextRange);
			usingDirective.Alias = alias;
			usingDirectives.Directives.Add(usingDirective);
			return true;
		}

		/// <summary>
		/// Matches a <c>UsingNamespaceDirective</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>UsingNamespaceDirective</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>.
		/// </remarks>
		protected virtual bool MatchUsingNamespaceDirective(UsingDirectiveSection usingDirectives) {
			int startOffset = this.Token.StartOffset;
			QualifiedIdentifier namespaceName;
			if (!this.MatchNamespaceName(out namespaceName))
				return false;
			this.Match(CSharpTokenID.SemiColon);
			UsingDirective usingDirective = new UsingDirective(new TextRange(startOffset, this.Token.EndOffset));
			
			// Reap comments
			this.ReapComments(usingDirective.Comments, false);
			
			usingDirective.NamespaceName = namespaceName;
			usingDirectives.Directives.Add(usingDirective);
			return true;
		}

		/// <summary>
		/// Matches a <c>NamespaceMemberDeclaration</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>NamespaceMemberDeclaration</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>OpenSquareBrace</c>, <c>New</c>, <c>Unsafe</c>, <c>Namespace</c>, <c>Public</c>, <c>Protected</c>, <c>Internal</c>, <c>Private</c>, <c>Abstract</c>, <c>Extern</c>, <c>Partial</c>, <c>Sealed</c>, <c>Static</c>, <c>Override</c>, <c>ReadOnly</c>, <c>Virtual</c>, <c>Volatile</c>, <c>Class</c>, <c>Struct</c>, <c>Interface</c>, <c>Enum</c>, <c>Delegate</c>.
		/// </remarks>
		protected virtual bool MatchNamespaceMemberDeclaration() {
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Namespace)) {
				if (!this.MatchNamespaceDeclaration())
					return false;
			}
			else if (this.IsInMultiMatchSet(20, this.LookAheadToken)) {
				AttributeSection attributeSection;
				AstNodeList attributeSections = new AstNodeList(null);
				Modifiers modifiers = Modifiers.None;
				Modifiers singleModifier;
				while (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenSquareBrace)) {
					if (this.MatchAttributeSection(out attributeSection)) {
						attributeSections.Add(attributeSection);
					}
				}
				int startOffset = this.LookAheadToken.StartOffset;
				while (this.IsInMultiMatchSet(21, this.LookAheadToken)) {
					if (!this.MatchModifier(out singleModifier))
						return false;
					else {
						modifiers |= singleModifier;
					}
				}
				if (!this.MatchTypeDeclaration(startOffset, attributeSections, modifiers))
					return false;
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>TypeDeclaration</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>TypeDeclaration</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Class</c>, <c>Struct</c>, <c>Interface</c>, <c>Enum</c>, <c>Delegate</c>.
		/// </remarks>
		protected virtual bool MatchTypeDeclaration(int startOffset, AstNodeList attributeSections, Modifiers modifiers) {
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Class)) {
				if (!this.MatchClassDeclaration(startOffset, attributeSections, modifiers))
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Struct)) {
				if (!this.MatchStructDeclaration(startOffset, attributeSections, modifiers))
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Interface)) {
				if (!this.MatchInterfaceDeclaration(startOffset, attributeSections, modifiers))
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Enum)) {
				if (!this.MatchEnumDeclaration(startOffset, attributeSections, modifiers))
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Delegate)) {
				if (!this.MatchDelegateDeclaration(startOffset, attributeSections, modifiers))
					return false;
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>Modifier</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>Modifier</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>New</c>, <c>Unsafe</c>, <c>Public</c>, <c>Protected</c>, <c>Internal</c>, <c>Private</c>, <c>Abstract</c>, <c>Extern</c>, <c>Partial</c>, <c>Sealed</c>, <c>Static</c>, <c>Override</c>, <c>ReadOnly</c>, <c>Virtual</c>, <c>Volatile</c>.
		/// </remarks>
		protected virtual bool MatchModifier(out Modifiers modifier) {
			modifier = Modifiers.None;
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Public)) {
				if (this.Match(CSharpTokenID.Public)) {
					modifier = Modifiers.Public;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Protected)) {
				if (this.Match(CSharpTokenID.Protected)) {
					modifier = Modifiers.Family;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Internal)) {
				if (this.Match(CSharpTokenID.Internal)) {
					modifier = Modifiers.Assembly;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Private)) {
				if (this.Match(CSharpTokenID.Private)) {
					modifier = Modifiers.Private;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Abstract)) {
				if (this.Match(CSharpTokenID.Abstract)) {
					modifier = Modifiers.Abstract;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Extern)) {
				if (this.Match(CSharpTokenID.Extern)) {
					modifier = Modifiers.Extern;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.New)) {
				if (this.Match(CSharpTokenID.New)) {
					modifier = Modifiers.New;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Partial)) {
				if (this.Match(CSharpTokenID.Partial)) {
					modifier = Modifiers.Partial;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Sealed)) {
				if (this.Match(CSharpTokenID.Sealed)) {
					modifier = Modifiers.Final;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Static)) {
				if (this.Match(CSharpTokenID.Static)) {
					modifier = Modifiers.Static;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Override)) {
				if (this.Match(CSharpTokenID.Override)) {
					modifier = Modifiers.Override;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.ReadOnly)) {
				if (this.Match(CSharpTokenID.ReadOnly)) {
					modifier = Modifiers.ReadOnly;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Virtual)) {
				if (this.Match(CSharpTokenID.Virtual)) {
					modifier = Modifiers.Virtual;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Volatile)) {
				if (this.Match(CSharpTokenID.Volatile)) {
					modifier = Modifiers.Volatile;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Unsafe)) {
				if (this.Match(CSharpTokenID.Unsafe)) {
					modifier = Modifiers.Unsafe;
				}
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>ClassDeclaration</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ClassDeclaration</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Class</c>.
		/// </remarks>
		protected virtual bool MatchClassDeclaration(int startOffset, AstNodeList attributeSections, Modifiers modifiers) {
			if (!this.Match(CSharpTokenID.Class))
				return false;
			QualifiedIdentifier identifier;
			if (!this.MatchIdentifier(out identifier))
				return false;
			// Default to internal access
			if (!AstNode.IsAccessSpecified(modifiers))
				modifiers |= Modifiers.Assembly;
			
			ClassDeclaration classDeclaration = new ClassDeclaration(modifiers, identifier);
			classDeclaration.Documentation = this.ReapDocumentationComments();
			classDeclaration.StartOffset = startOffset;
			classDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
			AstNodeList typeParameterList = null;
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.LessThan)) {
				if (!this.MatchTypeParameterList(out typeParameterList))
					return false;
			}
			if (typeParameterList != null)
				classDeclaration.GenericTypeArguments.AddRange(typeParameterList.ToArray());
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Colon)) {
				this.Match(CSharpTokenID.Colon);
				TypeReference typeReference;
				if (!this.MatchClassType(false, out typeReference)) {
					// Error recovery:  Go to the next open curly brace
					this.AdvanceToNext(CSharpTokenID.OpenCurlyBrace);
				}
				else {
					// Ensure that generic type parameters are marked properly
					this.MarkGenericParameters(typeParameterList, typeReference, false);
					
					classDeclaration.BaseTypes.Add(typeReference);
				}
				while (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) {
					if (!this.Match(CSharpTokenID.Comma)) {
						// Error recovery:  Go to the next open curly brace
						this.AdvanceToNext(CSharpTokenID.OpenCurlyBrace);
					}
					if (!this.MatchClassType(false, out typeReference)) {
						// Error recovery:  Go to the next open curly brace
						this.AdvanceToNext(CSharpTokenID.OpenCurlyBrace);
					}
					else {
						// Ensure that generic type parameters are marked properly
						this.MarkGenericParameters(typeParameterList, typeReference, false);
						
						classDeclaration.BaseTypes.Add(typeReference);
					}
				}
			}
			if (this.IsInMultiMatchSet(17, this.LookAheadToken)) {
				if (!this.MatchTypeParameterConstraintsClauses(typeParameterList))
					return false;
			}
			this.BlockAddChild(classDeclaration);
			AttributeSection attributeSection;
			Modifiers singleModifier;
			classDeclaration.BlockStartOffset = this.LookAheadToken.StartOffset;
			int classCurlyBraceLevel = curlyBraceLevel;
			if (!this.Match(CSharpTokenID.OpenCurlyBrace))
				return false;
			this.BlockStart(classDeclaration);
			bool errorReported = false;
			while (!this.IsAtEnd) {
				if ((this.TokenIs(this.LookAheadToken, CSharpTokenID.CloseCurlyBrace)) && (curlyBraceLevel == classCurlyBraceLevel + 1))
					break;
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenSquareBrace) || this.IsInMultiMatchSet(22, this.LookAheadToken) || this.IsInMultiMatchSet(23, this.LookAheadToken)) {
					errorReported = false;
					attributeSections = new AstNodeList(null);
					modifiers = Modifiers.None;
					while (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenSquareBrace)) {
						if (this.MatchAttributeSection(out attributeSection)) {
							attributeSections.Add(attributeSection);
						}
					}
					startOffset = this.LookAheadToken.StartOffset;
					while (this.IsInMultiMatchSet(21, this.LookAheadToken)) {
						if (this.MatchModifier(out singleModifier)) {
							modifiers |= singleModifier;
						}
					}
					this.MatchClassMemberDeclaration(classDeclaration, startOffset, attributeSections, modifiers);
				}
				else {
					// Error recovery:  Advance to the next token since nothing was matched
					if (!errorReported) {
						this.ReportSyntaxError(AssemblyInfo.Instance.Resources.GetString("SemanticParserError_ClassMemberDeclarationExpected"));
						errorReported = true;
					}
					this.AdvanceToNext();
				}
			}
			this.ReapDocumentationComments();
			this.Match(CSharpTokenID.CloseCurlyBrace);
			classDeclaration.BlockEndOffset = this.Token.EndOffset;
			this.BlockEnd();
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.SemiColon)) {
				this.Match(CSharpTokenID.SemiColon);
			}
			classDeclaration.EndOffset = this.Token.EndOffset;
			
			// Reap comments
			this.ReapComments(classDeclaration.Comments, false);
			return true;
		}

		/// <summary>
		/// Matches a <c>ClassMemberDeclaration</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ClassMemberDeclaration</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Void</c>, <c>OnesComplement</c>, <c>Const</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>, <c>Class</c>, <c>Struct</c>, <c>Implicit</c>, <c>Explicit</c>, <c>Event</c>, <c>Interface</c>, <c>Enum</c>, <c>Delegate</c>.
		/// </remarks>
		protected virtual bool MatchClassMemberDeclaration(TypeDeclaration parentTypeDeclaration, int startOffset, AstNodeList attributeSections, Modifiers modifiers) {
			if (this.IsInMultiMatchSet(24, this.LookAheadToken)) {
				if (!this.MatchStructMemberDeclaration(parentTypeDeclaration, startOffset, attributeSections, modifiers))
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OnesComplement)) {
				if (!this.Match(CSharpTokenID.OnesComplement))
					return false;
				QualifiedIdentifier identifier;
				if (!this.MatchIdentifier(out identifier))
					return false;
				// Destructor declaration
				DestructorDeclaration destructorDeclaration = new DestructorDeclaration(modifiers, identifier);
				destructorDeclaration.Documentation = this.ReapDocumentationComments();
				destructorDeclaration.StartOffset = startOffset;
				destructorDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
				if (!this.Match(CSharpTokenID.OpenParenthesis))
					return false;
				if (!this.Match(CSharpTokenID.CloseParenthesis))
					return false;
				if (this.TokenIs(this.LookAheadToken, CSharpTokenID.SemiColon)) {
					if (!this.Match(CSharpTokenID.SemiColon))
						return false;
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenCurlyBrace)) {
					destructorDeclaration.BlockStartOffset = this.LookAheadToken.StartOffset;
					Statement statement;
					int memberCurlyBraceLevel = curlyBraceLevel;
					if (!this.Match(CSharpTokenID.OpenCurlyBrace))
						return false;
					bool errorReported = false;
					while (!this.IsAtEnd) {
						if ((this.TokenIs(this.LookAheadToken, CSharpTokenID.CloseCurlyBrace)) && (curlyBraceLevel == memberCurlyBraceLevel + 1))
							break;
						else if ((this.IsInMultiMatchSet(15, this.LookAheadToken) || (this.IsLambdaExpression()) || (this.IsQueryExpression()) || (this.IsDefaultValueExpression()))) {
							errorReported = false;
							while ((this.IsInMultiMatchSet(16, this.LookAheadToken) || (this.IsLambdaExpression()) || (this.IsQueryExpression()) || (this.IsDefaultValueExpression()))) {
								if (!this.MatchStatement(out statement)) {
									if (!this.TokenIs(this.LookAheadToken, CSharpTokenID.CloseCurlyBrace)) this.AdvanceToNext();
								}
								else {
									destructorDeclaration.Statements.Add(statement);
								}
							}
						}
						else {
							// Error recovery:  Advance to the next token since nothing was matched
							if (!errorReported) {
								this.ReportSyntaxError(AssemblyInfo.Instance.Resources.GetString("SemanticParserError_StatementExpected"));
								errorReported = true;
							}
							this.AdvanceToNext();
						}
					}
					this.ReapDocumentationComments();
					
					// Reap comments
					this.ReapComments(destructorDeclaration.Comments, false);
					this.Match(CSharpTokenID.CloseCurlyBrace);
					destructorDeclaration.BlockEndOffset = this.Token.EndOffset;
				}
				else
					return false;
				destructorDeclaration.EndOffset = this.Token.EndOffset;
				this.BlockAddChild(destructorDeclaration);
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>ConstantDeclarator</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ConstantDeclarator</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>.
		/// </remarks>
		protected virtual bool MatchConstantDeclarator(FieldDeclaration constantDeclaration, TypeReference typeReference) {
			QualifiedIdentifier identifier;
			Expression initializer = null;
			int startOffset = this.LookAheadToken.StartOffset;
			if (!this.MatchIdentifier(out identifier))
				return false;
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Assignment)) {
				if (!this.Match(CSharpTokenID.Assignment))
					return false;
				if (!this.MatchExpression(out initializer))
					return false;
			}
			VariableDeclarator variableDeclarator = new VariableDeclarator(typeReference, identifier, true, false);
			variableDeclarator.Initializer = initializer;
			variableDeclarator.StartOffset = startOffset;
			variableDeclarator.EndOffset = this.Token.EndOffset;
			constantDeclaration.Variables.Add(variableDeclarator);
			return true;
		}

		/// <summary>
		/// Matches a <c>VariableDeclarator</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>VariableDeclarator</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>.
		/// </remarks>
		protected virtual bool MatchVariableDeclarator(FieldDeclaration fieldDeclaration, TypeReference typeReference) {
			QualifiedIdentifier identifier;
			Expression initializer = null;
			int startOffset = this.LookAheadToken.StartOffset;
			if (!this.MatchIdentifier(out identifier))
				return false;
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Assignment)) {
				if (!this.Match(CSharpTokenID.Assignment))
					return false;
				if (!this.MatchVariableInitializer(out initializer))
					return false;
			}
			VariableDeclarator variableDeclarator = new VariableDeclarator(typeReference, identifier, false, false);
			variableDeclarator.Initializer = initializer;
			variableDeclarator.StartOffset = startOffset;
			variableDeclarator.EndOffset = this.Token.EndOffset;
			fieldDeclaration.Variables.Add(variableDeclarator);
			return true;
		}

		/// <summary>
		/// Matches a <c>VariableInitializer</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>VariableInitializer</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Multiplication</c>, <c>True</c>, <c>False</c>, <c>DecimalIntegerLiteral</c>, <c>HexadecimalIntegerLiteral</c>, <c>RealLiteral</c>, <c>CharacterLiteral</c>, <c>StringLiteral</c>, <c>VerbatimStringLiteral</c>, <c>Null</c>, <c>OpenParenthesis</c>, <c>This</c>, <c>Base</c>, <c>New</c>, <c>TypeOf</c>, <c>SizeOf</c>, <c>Checked</c>, <c>Unchecked</c>, <c>Increment</c>, <c>Decrement</c>, <c>Addition</c>, <c>Subtraction</c>, <c>Negation</c>, <c>OnesComplement</c>, <c>BitwiseAnd</c>, <c>OpenCurlyBrace</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>, <c>Delegate</c>, <c>StackAlloc</c>.
		/// The non-terminal can start with: this.IsLambdaExpression().
		/// The non-terminal can start with: this.IsQueryExpression().
		/// The non-terminal can start with: this.IsDefaultValueExpression().
		/// </remarks>
		protected virtual bool MatchVariableInitializer(out Expression expression) {
			expression = null;
			if ((this.IsInMultiMatchSet(0, this.LookAheadToken) || (this.IsLambdaExpression()) || (this.IsQueryExpression()) || (this.IsDefaultValueExpression()))) {
				if (!this.MatchExpression(out expression))
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenCurlyBrace)) {
				if (!this.MatchArrayInitializer(out expression))
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.StackAlloc)) {
				if (!this.MatchStackAllocInitializer(out expression))
					return false;
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>ReturnType</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ReturnType</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Void</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>.
		/// </remarks>
		protected virtual bool MatchReturnType(out TypeReference typeReference) {
			typeReference = null;
			if ((!this.TokenIs(this.LookAheadToken, CSharpTokenID.Void)) || (this.TokenIs(this.GetLookAheadToken(2), CSharpTokenID.Multiplication))) {
				if (!this.MatchType(out typeReference))
					return false;
			}
			else {
				// Regular void (non-pointer)
				if (!this.Match(CSharpTokenID.Void))
					return false;
				else {
					typeReference = new TypeReference("System.Void", this.Token.TextRange);
				}
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>MemberName</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>MemberName</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>This</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>.
		/// </remarks>
		protected virtual bool MatchMemberName(out TypeReference interfaceType, out QualifiedIdentifier identifier, out AstNodeList typeParameterList) {
			interfaceType = null;
			identifier = null;
			typeParameterList = null;
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.This)) {
				if (!this.Match(CSharpTokenID.This))
					return false;
				else {
					identifier = new QualifiedIdentifier(this.TokenText, this.Token.TextRange);
					return true;
				}
			}
			else if (this.IsInMultiMatchSet(3, this.LookAheadToken)) {
				if (!this.MatchTypeName(true, out interfaceType))
					return false;
				if (this.IsQualifierIdentifierContinuation()) {
					// The interface type is probably a generic... the next item is the name
					if (!this.Match(CSharpTokenID.Dot))
						return false;
					if (!this.MatchIdentifier(out identifier))
						return false;
				}
				else if (this.AreNextTwo(CSharpTokenID.Dot, CSharpTokenID.This)) {
					// Ends with .this
					if (!this.Match(CSharpTokenID.Dot))
						return false;
					if (!this.Match(CSharpTokenID.This))
						return false;
					else {
						identifier = new QualifiedIdentifier(this.TokenText, this.Token.TextRange);
					}
				}
				else {
					int index = interfaceType.Name.LastIndexOf(".");
					if (index != -1) {
						// Interface type... remove the last item
						string name = interfaceType.Name.Substring(index + 1);
						identifier = new QualifiedIdentifier(name, new TextRange(interfaceType.EndOffset - name.Length, interfaceType.EndOffset));
						interfaceType.EndOffset -= (name.Length + 1);
						interfaceType.Name = interfaceType.Name.Substring(0, index);
					}
					else {
						// No interface
						identifier = new QualifiedIdentifier(interfaceType.Name, interfaceType.TextRange);
						if (interfaceType.GenericTypeArguments != null) {
							typeParameterList = new AstNodeList(null);
							typeParameterList.AddRange(interfaceType.GenericTypeArguments.ToArray());
							
							// Flag each type parameter as a generic parameter
							foreach (TypeReference typeParameter in typeParameterList)
								typeParameter.IsGenericParameter = true;
						}
						interfaceType = null;
					}
				}
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>FormalParameterList</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>FormalParameterList</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Void</c>, <c>OpenSquareBrace</c>, <c>Ref</c>, <c>Out</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>, <c>Params</c>.
		/// </remarks>
		protected virtual bool MatchFormalParameterList(IAstNodeList genericTypeArguments, out AstNodeList parameterList) {
			AttributeSection attributeSection;
			AstNodeList attributeSections = new AstNodeList(null);
			parameterList = new AstNodeList(null);
			ParameterDeclaration parameterDeclaration;
			while (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenSquareBrace)) {
				if (!this.MatchAttributeSection(out attributeSection))
					return false;
				else {
					attributeSections.Add(attributeSection);
				}
			}
			if (this.IsInMultiMatchSet(25, this.LookAheadToken)) {
				if (!this.MatchFixedParameter(genericTypeArguments, attributeSections, out parameterDeclaration))
					return false;
				else {
					parameterList.Add(parameterDeclaration);
				}
				attributeSections = new AstNodeList(null);
				while ((this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) && (!this.IsParameterArray())) {
					if (!this.Match(CSharpTokenID.Comma))
						return false;
					while (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenSquareBrace)) {
						if (!this.MatchAttributeSection(out attributeSection))
							return false;
						else {
							attributeSections.Add(attributeSection);
						}
					}
					if (!this.MatchFixedParameter(genericTypeArguments, attributeSections, out parameterDeclaration))
						return false;
					else {
						parameterList.Add(parameterDeclaration);
					}
					attributeSections = new AstNodeList(null);
				}
				if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) {
					if (!this.Match(CSharpTokenID.Comma))
						return false;
					while (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenSquareBrace)) {
						if (!this.MatchAttributeSection(out attributeSection))
							return false;
						else {
							attributeSections.Add(attributeSection);
						}
					}
					if (!this.MatchParameterArray(genericTypeArguments, attributeSections, out parameterDeclaration))
						return false;
					else {
						parameterList.Add(parameterDeclaration);
					}
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Params)) {
				if (!this.MatchParameterArray(genericTypeArguments, attributeSections, out parameterDeclaration))
					return false;
				else {
					parameterList.Add(parameterDeclaration);
				}
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>FixedParameter</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>FixedParameter</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Void</c>, <c>Ref</c>, <c>Out</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>.
		/// </remarks>
		protected virtual bool MatchFixedParameter(IAstNodeList genericTypeArguments, AstNodeList attributeSections, out ParameterDeclaration fixedParameter) {
			fixedParameter = null;
			ParameterModifiers modifiers = ParameterModifiers.None;
			TypeReference typeReference;
			Expression initializer = null;
			int startOffset = this.LookAheadToken.StartOffset;
			if (((this.TokenIs(this.LookAheadToken, CSharpTokenID.Ref)) || (this.TokenIs(this.LookAheadToken, CSharpTokenID.Out)))) {
				if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Ref)) {
					if (!this.Match(CSharpTokenID.Ref))
						return false;
					else {
						modifiers = ParameterModifiers.Ref;
					}
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Out)) {
					if (!this.Match(CSharpTokenID.Out))
						return false;
					else {
						modifiers = ParameterModifiers.Out;
					}
				}
				else
					return false;
			}
			if (!this.MatchType(out typeReference))
				return false;
			else {
				this.MarkGenericParameters(genericTypeArguments, typeReference, true);
			}
			if (!this.MatchSimpleIdentifier())
				return false;
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Assignment)) {
				if (!this.Match(CSharpTokenID.Assignment))
					return false;
				if (this.MatchExpression(out initializer)) {
					modifiers = ParameterModifiers.Optional;
				}
			}
			fixedParameter = new ParameterDeclaration(modifiers, this.TokenText);
			fixedParameter.StartOffset = startOffset;
			fixedParameter.EndOffset = this.Token.EndOffset;
			if (attributeSections != null)
				fixedParameter.AttributeSections.AddRange(attributeSections.ToArray());
			fixedParameter.ParameterType = typeReference;
			fixedParameter.Initializer = initializer;
			return true;
		}

		/// <summary>
		/// Matches a <c>ParameterArray</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ParameterArray</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Params</c>.
		/// </remarks>
		protected virtual bool MatchParameterArray(IAstNodeList genericTypeArguments, AstNodeList attributeSections, out ParameterDeclaration parameterArray) {
			parameterArray = null;
			TypeReference typeReference;
			int startOffset = this.LookAheadToken.StartOffset;
			if (!this.Match(CSharpTokenID.Params))
				return false;
			if (!this.MatchType(out typeReference))
				return false;
			else {
				this.MarkGenericParameters(genericTypeArguments, typeReference, true);
			}
			if (!this.MatchSimpleIdentifier())
				return false;
			parameterArray = new ParameterDeclaration(ParameterModifiers.ParameterArray, this.TokenText);
			parameterArray.StartOffset = startOffset;
			parameterArray.EndOffset = this.Token.EndOffset;
			parameterArray.AttributeSections.AddRange(attributeSections.ToArray());
			parameterArray.ParameterType = typeReference;
			return true;
		}

		/// <summary>
		/// Matches a <c>OverloadableOperator</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>OverloadableOperator</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>LessThan</c>, <c>GreaterThan</c>, <c>Multiplication</c>, <c>True</c>, <c>False</c>, <c>Increment</c>, <c>Decrement</c>, <c>Addition</c>, <c>Subtraction</c>, <c>Negation</c>, <c>OnesComplement</c>, <c>BitwiseAnd</c>, <c>Division</c>, <c>Modulus</c>, <c>LeftShift</c>, <c>LessThanOrEqual</c>, <c>GreaterThanOrEqual</c>, <c>Equality</c>, <c>Inequality</c>, <c>ExclusiveOr</c>, <c>BitwiseOr</c>.
		/// The non-terminal can start with: this.IsRightShift().
		/// </remarks>
		protected virtual bool MatchOverloadableOperator(out OperatorType operatorType) {
			operatorType = OperatorType.None;
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Addition)) {
				if (!this.Match(CSharpTokenID.Addition))
					return false;
				else {
					operatorType = OperatorType.Addition;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Subtraction)) {
				if (!this.Match(CSharpTokenID.Subtraction))
					return false;
				else {
					operatorType = OperatorType.Subtraction;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Multiplication)) {
				if (!this.Match(CSharpTokenID.Multiplication))
					return false;
				else {
					operatorType = OperatorType.Multiply;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Division)) {
				if (!this.Match(CSharpTokenID.Division))
					return false;
				else {
					operatorType = OperatorType.Division;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Modulus)) {
				if (!this.Match(CSharpTokenID.Modulus))
					return false;
				else {
					operatorType = OperatorType.Modulus;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.BitwiseAnd)) {
				if (!this.Match(CSharpTokenID.BitwiseAnd))
					return false;
				else {
					operatorType = OperatorType.BitwiseAnd;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.BitwiseOr)) {
				if (!this.Match(CSharpTokenID.BitwiseOr))
					return false;
				else {
					operatorType = OperatorType.BitwiseOr;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.ExclusiveOr)) {
				if (!this.Match(CSharpTokenID.ExclusiveOr))
					return false;
				else {
					operatorType = OperatorType.ExclusiveOr;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Negation)) {
				if (!this.Match(CSharpTokenID.Negation))
					return false;
				else {
					operatorType = OperatorType.Negation;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OnesComplement)) {
				if (!this.Match(CSharpTokenID.OnesComplement))
					return false;
				else {
					operatorType = OperatorType.OnesComplement;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.LeftShift)) {
				if (!this.Match(CSharpTokenID.LeftShift))
					return false;
				else {
					operatorType = OperatorType.LeftShift;
				}
			}
			else if (this.IsRightShift()) {
				// NOTE: This handles ambiguity between right shift and generic type specifications in .NET 2.0
				if (!this.Match(CSharpTokenID.GreaterThan))
					return false;
				if (!this.Match(CSharpTokenID.GreaterThan))
					return false;
				else {
					operatorType = OperatorType.RightShift;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.LessThan)) {
				if (!this.Match(CSharpTokenID.LessThan))
					return false;
				else {
					operatorType = OperatorType.LessThan;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.GreaterThan)) {
				if (!this.Match(CSharpTokenID.GreaterThan))
					return false;
				else {
					operatorType = OperatorType.GreaterThan;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Increment)) {
				if (!this.Match(CSharpTokenID.Increment))
					return false;
				else {
					operatorType = OperatorType.PreIncrement; // NOTE: Means the same as PostIncrement
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Decrement)) {
				if (!this.Match(CSharpTokenID.Decrement))
					return false;
				else {
					operatorType = OperatorType.PreDecrement; // NOTE: Means the same as PostDecrement
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Equality)) {
				if (!this.Match(CSharpTokenID.Equality))
					return false;
				else {
					operatorType = OperatorType.Equality;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Inequality)) {
				if (!this.Match(CSharpTokenID.Inequality))
					return false;
				else {
					operatorType = OperatorType.Inequality;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.LessThanOrEqual)) {
				if (!this.Match(CSharpTokenID.LessThanOrEqual))
					return false;
				else {
					operatorType = OperatorType.LessThanOrEqual;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.GreaterThanOrEqual)) {
				if (!this.Match(CSharpTokenID.GreaterThanOrEqual))
					return false;
				else {
					operatorType = OperatorType.GreaterThanOrEqual;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.True)) {
				if (!this.Match(CSharpTokenID.True))
					return false;
				else {
					operatorType = OperatorType.True;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.False)) {
				if (!this.Match(CSharpTokenID.False))
					return false;
				else {
					operatorType = OperatorType.False;
				}
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>StructDeclaration</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>StructDeclaration</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Struct</c>.
		/// </remarks>
		protected virtual bool MatchStructDeclaration(int startOffset, AstNodeList attributeSections, Modifiers modifiers) {
			if (!this.Match(CSharpTokenID.Struct))
				return false;
			QualifiedIdentifier identifier;
			if (!this.MatchIdentifier(out identifier))
				return false;
			// Default to internal access
			if (!AstNode.IsAccessSpecified(modifiers))
				modifiers |= Modifiers.Assembly;
			
			StructureDeclaration structureDeclaration = new StructureDeclaration(modifiers, identifier);
			structureDeclaration.Documentation = this.ReapDocumentationComments();
			structureDeclaration.StartOffset = startOffset;
			structureDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
			AstNodeList typeParameterList = null;
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.LessThan)) {
				if (!this.MatchTypeParameterList(out typeParameterList))
					return false;
			}
			if (typeParameterList != null)
				structureDeclaration.GenericTypeArguments.AddRange(typeParameterList.ToArray());
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Colon)) {
				this.Match(CSharpTokenID.Colon);
				TypeReference typeReference;
				if (!this.MatchClassType(false, out typeReference)) {
					// Error recovery:  Go to the next open curly brace
					this.AdvanceToNext(CSharpTokenID.OpenCurlyBrace);
				}
				else {
					structureDeclaration.BaseTypes.Add(typeReference);
				}
				while (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) {
					if (!this.Match(CSharpTokenID.Comma)) {
						// Error recovery:  Go to the next open curly brace
						this.AdvanceToNext(CSharpTokenID.OpenCurlyBrace);
					}
					if (!this.MatchClassType(false, out typeReference)) {
						// Error recovery:  Go to the next open curly brace
						this.AdvanceToNext(CSharpTokenID.OpenCurlyBrace);
					}
					else {
						structureDeclaration.BaseTypes.Add(typeReference);
					}
				}
			}
			if (this.IsInMultiMatchSet(17, this.LookAheadToken)) {
				if (!this.MatchTypeParameterConstraintsClauses(typeParameterList))
					return false;
			}
			this.BlockAddChild(structureDeclaration);
			AttributeSection attributeSection;
			Modifiers singleModifier;
			structureDeclaration.BlockStartOffset = this.LookAheadToken.StartOffset;
			int structureCurlyBraceLevel = curlyBraceLevel;
			if (!this.Match(CSharpTokenID.OpenCurlyBrace))
				return false;
			this.BlockStart(structureDeclaration);
			bool errorReported = false;
			while (!this.IsAtEnd) {
				if ((this.TokenIs(this.LookAheadToken, CSharpTokenID.CloseCurlyBrace)) && (curlyBraceLevel == structureCurlyBraceLevel + 1))
					break;
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenSquareBrace) || this.IsInMultiMatchSet(22, this.LookAheadToken) || this.IsInMultiMatchSet(24, this.LookAheadToken)) {
					errorReported = false;
					attributeSections = new AstNodeList(null);
					modifiers = Modifiers.None;
					while (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenSquareBrace)) {
						if (this.MatchAttributeSection(out attributeSection)) {
							attributeSections.Add(attributeSection);
						}
					}
					startOffset = this.LookAheadToken.StartOffset;
					while (this.IsInMultiMatchSet(21, this.LookAheadToken)) {
						if (this.MatchModifier(out singleModifier)) {
							modifiers |= singleModifier;
						}
					}
					if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Fixed)) {
						if (!this.MatchFixedSizeBufferDeclaration(startOffset, attributeSections, modifiers)) {
							if (!this.TokenIs(this.LookAheadToken, CSharpTokenID.CloseCurlyBrace)) this.AdvanceToNext();
						}
					}
					else if (this.IsInMultiMatchSet(24, this.LookAheadToken)) {
						if (!this.MatchStructMemberDeclaration(structureDeclaration, startOffset, attributeSections, modifiers)) {
							if (!this.TokenIs(this.LookAheadToken, CSharpTokenID.CloseCurlyBrace)) this.AdvanceToNext();
						}
					}
					else
						return false;
				}
				else {
					// Error recovery:  Advance to the next token since nothing was matched
					if (!errorReported) {
						this.ReportSyntaxError(AssemblyInfo.Instance.Resources.GetString("SemanticParserError_StructureMemberDeclarationExpected"));
						errorReported = true;
					}
					this.AdvanceToNext();
				}
			}
			this.ReapDocumentationComments();
			this.Match(CSharpTokenID.CloseCurlyBrace);
			structureDeclaration.BlockEndOffset = this.Token.EndOffset;
			this.BlockEnd();
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.SemiColon)) {
				this.Match(CSharpTokenID.SemiColon);
			}
			structureDeclaration.EndOffset = this.Token.EndOffset;
			
			// Reap comments
			this.ReapComments(structureDeclaration.Comments, false);
			return true;
		}

		/// <summary>
		/// Matches a <c>StructMemberDeclaration</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>StructMemberDeclaration</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Void</c>, <c>Const</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>, <c>Class</c>, <c>Struct</c>, <c>Implicit</c>, <c>Explicit</c>, <c>Event</c>, <c>Interface</c>, <c>Enum</c>, <c>Delegate</c>.
		/// </remarks>
		protected virtual bool MatchStructMemberDeclaration(TypeDeclaration parentTypeDeclaration, int startOffset, AstNodeList attributeSections, Modifiers modifiers) {
			// Build a list of generic type arguments for the parent type... will add to this collection if a generic method
			AstNodeList scopedGenericTypeArguments = new AstNodeList(null);
			if ((parentTypeDeclaration.GenericTypeArguments != null) && (parentTypeDeclaration.GenericTypeArguments.Count > 0))
				scopedGenericTypeArguments.AddRange(parentTypeDeclaration.GenericTypeArguments.ToArray());
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Const)) {
				// Constant declaration
				// NOTE: Attributes and ConstantModifier (alias Modifier) moved to callers of StructMemberDeclaration to reduce ambiguity
				if (!this.Match(CSharpTokenID.Const))
					return false;
				TypeReference typeReference;
				FieldDeclaration constantDeclaration = new FieldDeclaration(modifiers);
				constantDeclaration.Documentation = this.ReapDocumentationComments();
				constantDeclaration.StartOffset = startOffset;
				constantDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
				if (!this.MatchType(out typeReference))
					return false;
				else {
					this.MarkGenericParameters(scopedGenericTypeArguments, typeReference, true);
				}
				if (!this.MatchConstantDeclarator(constantDeclaration, typeReference))
					return false;
				while (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) {
					if (!this.Match(CSharpTokenID.Comma))
						return false;
					if (!this.MatchConstantDeclarator(constantDeclaration, typeReference))
						return false;
				}
				if (!this.Match(CSharpTokenID.SemiColon))
					return false;
				constantDeclaration.EndOffset = this.Token.EndOffset;
				this.BlockAddChild(constantDeclaration);
				this.ReapDocumentationComments();
				return true;
			}
			else if (((this.TokenIs(this.LookAheadToken, CSharpTokenID.Void)) && (!this.TokenIs(this.GetLookAheadToken(2), CSharpTokenID.Multiplication)))) {
				// Void method declaration
				TypeReference typeReference = new TypeReference("System.Void", this.LookAheadToken.TextRange);
				TypeReference interfaceType;
				QualifiedIdentifier name;
				AstNodeList typeParameterList;
				bool isExtension = false;
				if (!this.Match(CSharpTokenID.Void))
					return false;
				if (!this.MatchMemberName(out interfaceType, out name, out typeParameterList))
					return false;
				// Add to the scoped generic type arguments
				if ((typeParameterList != null) && (typeParameterList.Count > 0))
					scopedGenericTypeArguments.AddRange(typeParameterList.ToArray());
				
				MethodDeclaration methodDeclaration = new MethodDeclaration(modifiers, name);
				methodDeclaration.Documentation = this.ReapDocumentationComments();
				methodDeclaration.StartOffset = startOffset;
				methodDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
				methodDeclaration.ReturnType = typeReference;
				if (interfaceType != null)
					methodDeclaration.ImplementedMembers.Add(new MemberSpecifier(interfaceType, name));
				if (typeParameterList != null)
					methodDeclaration.GenericTypeArguments.AddRange(typeParameterList.ToArray());
				AstNodeList parameterList = null;
				if (!this.Match(CSharpTokenID.OpenParenthesis))
					return false;
				if (this.TokenIs(this.LookAheadToken, CSharpTokenID.This)) {
					if (this.Match(CSharpTokenID.This)) {
						// Ensure that the containing block is a static class declaration
						if ((parentTypeDeclaration == null) || (!(parentTypeDeclaration is ClassDeclaration)) ||
						((((ClassDeclaration)parentTypeDeclaration).Modifiers & Modifiers.Static) == 0)) {
							this.ReportSyntaxError(this.Token.TextRange, AssemblyInfo.Instance.Resources.GetString("SemanticParserError_ExtensionMethodsInStaticClass"));
						}
						else {
							isExtension = true;
							
							// Add an Extension attribute to the method
							AttributeSection attributeSection = new AttributeSection();
							attributeSection.Attributes.Add(new ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast.Attribute(new TypeReference("System.Runtime.CompilerServices.Extension", TextRange.Deleted)));
							methodDeclaration.AttributeSections.Add(attributeSection);
							
							// Add an Extension attribute to the type declaration
							if (!parentTypeDeclaration.IsExtension)
								parentTypeDeclaration.AttributeSections.Add(attributeSection);
						}
					}
				}
				if (this.IsInMultiMatchSet(26, this.LookAheadToken)) {
					if (!this.MatchFormalParameterList(scopedGenericTypeArguments, out parameterList))
						return false;
				}
				if ((parameterList != null) && (parameterList.Count > 0))
					methodDeclaration.Parameters.AddRange(parameterList.ToArray());
				else if (isExtension)
					this.ReportSyntaxError(this.Token.TextRange, AssemblyInfo.Instance.Resources.GetString("SemanticParserError_ExtensionMethodsRequireOneParameter"));
				if (!this.Match(CSharpTokenID.CloseParenthesis))
					return false;
				if (this.IsInMultiMatchSet(17, this.LookAheadToken)) {
					if (!this.MatchTypeParameterConstraintsClauses(typeParameterList))
						return false;
				}
				if (this.TokenIs(this.LookAheadToken, CSharpTokenID.SemiColon)) {
					if (!this.Match(CSharpTokenID.SemiColon))
						return false;
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenCurlyBrace)) {
					methodDeclaration.BlockStartOffset = this.LookAheadToken.StartOffset;
					Statement statement;
					int memberCurlyBraceLevel = curlyBraceLevel;
					if (!this.Match(CSharpTokenID.OpenCurlyBrace))
						return false;
					bool errorReported = false;
					while (!this.IsAtEnd) {
						if ((this.TokenIs(this.LookAheadToken, CSharpTokenID.CloseCurlyBrace)) && (curlyBraceLevel == memberCurlyBraceLevel + 1))
							break;
						else if ((this.IsInMultiMatchSet(15, this.LookAheadToken) || (this.IsLambdaExpression()) || (this.IsQueryExpression()) || (this.IsDefaultValueExpression()))) {
							errorReported = false;
							while ((this.IsInMultiMatchSet(16, this.LookAheadToken) || (this.IsLambdaExpression()) || (this.IsQueryExpression()) || (this.IsDefaultValueExpression()))) {
								if (!this.MatchStatement(out statement)) {
									if (!this.TokenIs(this.LookAheadToken, CSharpTokenID.CloseCurlyBrace)) this.AdvanceToNext();
								}
								else {
									methodDeclaration.Statements.Add(statement);
								}
							}
						}
						else {
							// Error recovery:  Advance to the next token since nothing was matched
							if (!errorReported) {
								this.ReportSyntaxError(AssemblyInfo.Instance.Resources.GetString("SemanticParserError_StatementExpected"));
								errorReported = true;
							}
							this.AdvanceToNext();
						}
					}
					this.ReapDocumentationComments();
					
					// Reap comments
					this.ReapComments(methodDeclaration.Comments, false);
					this.Match(CSharpTokenID.CloseCurlyBrace);
					methodDeclaration.BlockEndOffset = this.Token.EndOffset;
				}
				else
					return false;
				methodDeclaration.EndOffset = this.Token.EndOffset;
				this.BlockAddChild(methodDeclaration);
			}
			else if (((this.TokenIs(this.LookAheadToken, CSharpTokenID.Implicit)) || (this.TokenIs(this.LookAheadToken, CSharpTokenID.Explicit)))) {
				// Implicit/explicit operator declaration
				OperatorType operatorType = OperatorType.None;
				TypeReference typeReference;
				TypeReference parameterTypeReference;
				ParameterDeclaration parameter;
				if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Implicit)) {
					if (!this.Match(CSharpTokenID.Implicit))
						return false;
					else {
						operatorType = OperatorType.Implicit;
					}
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Explicit)) {
					if (!this.Match(CSharpTokenID.Explicit))
						return false;
					else {
						operatorType = OperatorType.Explicit;
					}
				}
				else
					return false;
				if (!this.Match(CSharpTokenID.Operator))
					return false;
				if (!this.MatchType(out typeReference))
					return false;
				else {
					this.MarkGenericParameters(scopedGenericTypeArguments, typeReference, true);
				}
				if (!this.Match(CSharpTokenID.OpenParenthesis))
					return false;
				int parameterStartOffset = this.LookAheadToken.StartOffset;
				if (!this.MatchType(out parameterTypeReference))
					return false;
				else {
					this.MarkGenericParameters(scopedGenericTypeArguments, parameterTypeReference, true);
				}
				if (!this.MatchSimpleIdentifier())
					return false;
				parameter = new ParameterDeclaration(ParameterModifiers.None, this.TokenText);
				parameter.StartOffset = parameterStartOffset;
				parameter.EndOffset = this.Token.EndOffset;
				parameter.ParameterType = parameterTypeReference;
				if (!this.Match(CSharpTokenID.CloseParenthesis))
					return false;
				OperatorDeclaration operatorDeclaration = new OperatorDeclaration(modifiers, operatorType);
				operatorDeclaration.Documentation = this.ReapDocumentationComments();
				operatorDeclaration.StartOffset = startOffset;
				operatorDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
				operatorDeclaration.ReturnType = typeReference;
				if (parameter != null)
					operatorDeclaration.Parameters.Add(parameter);
				if (this.TokenIs(this.LookAheadToken, CSharpTokenID.SemiColon)) {
					if (!this.Match(CSharpTokenID.SemiColon))
						return false;
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenCurlyBrace)) {
					operatorDeclaration.BlockStartOffset = this.LookAheadToken.StartOffset;
					Statement statement;
					int memberCurlyBraceLevel = curlyBraceLevel;
					if (!this.Match(CSharpTokenID.OpenCurlyBrace))
						return false;
					bool errorReported = false;
					while (!this.IsAtEnd) {
						if ((this.TokenIs(this.LookAheadToken, CSharpTokenID.CloseCurlyBrace)) && (curlyBraceLevel == memberCurlyBraceLevel + 1))
							break;
						else if ((this.IsInMultiMatchSet(15, this.LookAheadToken) || (this.IsLambdaExpression()) || (this.IsQueryExpression()) || (this.IsDefaultValueExpression()))) {
							errorReported = false;
							while ((this.IsInMultiMatchSet(16, this.LookAheadToken) || (this.IsLambdaExpression()) || (this.IsQueryExpression()) || (this.IsDefaultValueExpression()))) {
								if (!this.MatchStatement(out statement)) {
									if (!this.TokenIs(this.LookAheadToken, CSharpTokenID.CloseCurlyBrace)) this.AdvanceToNext();
								}
								else {
									operatorDeclaration.Statements.Add(statement);
								}
							}
						}
						else {
							// Error recovery:  Advance to the next token since nothing was matched
							if (!errorReported) {
								this.ReportSyntaxError(AssemblyInfo.Instance.Resources.GetString("SemanticParserError_StatementExpected"));
								errorReported = true;
							}
							this.AdvanceToNext();
						}
					}
					this.ReapDocumentationComments();
					
					// Reap comments
					this.ReapComments(operatorDeclaration.Comments, false);
					this.Match(CSharpTokenID.CloseCurlyBrace);
					operatorDeclaration.BlockEndOffset = this.Token.EndOffset;
				}
				else
					return false;
				operatorDeclaration.EndOffset = this.Token.EndOffset;
				this.BlockAddChild(operatorDeclaration);
				this.ReapDocumentationComments();
				return true;
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Event)) {
				// Event declaration
				// NOTE: Attributes and EventModifier (alias Modifier) moved to callers of StructMemberDeclaration to reduce ambiguity
				if (!this.Match(CSharpTokenID.Event))
					return false;
				TypeReference typeReference;
				TypeReference interfaceType;
				QualifiedIdentifier identifier;
				AstNodeList typeParameterList;
				if (!this.MatchType(out typeReference))
					return false;
				else {
					this.MarkGenericParameters(scopedGenericTypeArguments, typeReference, true);
				}
				if (!this.MatchMemberName(out interfaceType, out identifier, out typeParameterList))
					return false;
				EventDeclaration eventDeclaration = new EventDeclaration(modifiers, identifier);
				eventDeclaration.Documentation = this.ReapDocumentationComments();
				eventDeclaration.StartOffset = startOffset;
				eventDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
				eventDeclaration.EventType = typeReference;
				if (interfaceType != null)
					eventDeclaration.ImplementedMembers.Add(new MemberSpecifier(interfaceType, identifier));
				if (this.TokenIs(this.LookAheadToken, CSharpTokenID.SemiColon)) {
					if (!this.Match(CSharpTokenID.SemiColon))
						return false;
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenCurlyBrace)) {
					while (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenCurlyBrace)) {
						AttributeSection attributeSection;
						bool isAddAccessor = true;
						Statement block = null;
						if (!this.Match(CSharpTokenID.OpenCurlyBrace))
							return false;
						AccessorDeclaration accessorDeclaration = new AccessorDeclaration();
						accessorDeclaration.StartOffset = this.LookAheadToken.StartOffset;
						Modifiers singleModifier;
						while (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenSquareBrace)) {
							if (!this.MatchAttributeSection(out attributeSection))
								return false;
							else {
								accessorDeclaration.AttributeSections.Add(attributeSection);
							}
						}
						while (this.IsInMultiMatchSet(21, this.LookAheadToken)) {
							if (!this.MatchModifier(out singleModifier))
								return false;
							else {
								accessorDeclaration.Modifiers |= singleModifier;
							}
						}
						if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Add)) {
							if (!this.Match(CSharpTokenID.Add))
								return false;
							else {
								isAddAccessor = true;
							}
						}
						else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Remove)) {
							if (!this.Match(CSharpTokenID.Remove))
								return false;
							else {
								isAddAccessor = false;
							}
						}
						else
							return false;
						if (this.TokenIs(this.LookAheadToken, CSharpTokenID.SemiColon)) {
							if (!this.Match(CSharpTokenID.SemiColon))
								return false;
						}
						else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenCurlyBrace)) {
							if (!this.MatchBlock(out block))
								return false;
						}
						else
							return false;
						accessorDeclaration.BlockStatement = block as BlockStatement;
						accessorDeclaration.EndOffset = this.Token.EndOffset;
						if (isAddAccessor)
							eventDeclaration.AddAccessor = accessorDeclaration;
						else
							eventDeclaration.RemoveAccessor = accessorDeclaration;
						if (this.IsInMultiMatchSet(27, this.LookAheadToken)) {
							isAddAccessor = true;
							block = null;
							accessorDeclaration = new AccessorDeclaration();
							accessorDeclaration.StartOffset = this.LookAheadToken.StartOffset;
							while (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenSquareBrace)) {
								if (!this.MatchAttributeSection(out attributeSection))
									return false;
								else {
									accessorDeclaration.AttributeSections.Add(attributeSection);
								}
							}
							while (this.IsInMultiMatchSet(21, this.LookAheadToken)) {
								if (!this.MatchModifier(out singleModifier))
									return false;
								else {
									accessorDeclaration.Modifiers |= singleModifier;
								}
							}
							if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Add)) {
								if (!this.Match(CSharpTokenID.Add))
									return false;
								else {
									isAddAccessor = true;
								}
							}
							else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Remove)) {
								if (!this.Match(CSharpTokenID.Remove))
									return false;
								else {
									isAddAccessor = false;
								}
							}
							else
								return false;
							if (this.TokenIs(this.LookAheadToken, CSharpTokenID.SemiColon)) {
								if (!this.Match(CSharpTokenID.SemiColon))
									return false;
							}
							else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenCurlyBrace)) {
								if (!this.MatchBlock(out block))
									return false;
							}
							else
								return false;
							accessorDeclaration.BlockStatement = block as BlockStatement;
							accessorDeclaration.EndOffset = this.Token.EndOffset;
							if (isAddAccessor)
								eventDeclaration.AddAccessor = accessorDeclaration;
							else
								eventDeclaration.RemoveAccessor = accessorDeclaration;
						}
						this.ReapDocumentationComments();
						this.Match(CSharpTokenID.CloseCurlyBrace);
					}
				}
				else
					return false;
				eventDeclaration.EndOffset = this.Token.EndOffset;
				this.BlockAddChild(eventDeclaration);
				this.ReapDocumentationComments();
				return true;
			}
			else if (this.IsInMultiMatchSet(28, this.LookAheadToken)) {
				if (!this.MatchTypeDeclaration(startOffset, attributeSections, modifiers))
					return false;
				return true;
			}
			else if (this.IsInMultiMatchSet(29, this.LookAheadToken)) {
				if (!this.AreNextTwoIdentifierAnd(CSharpTokenID.OpenParenthesis)) {
					TypeReference typeReference;
					if (!this.MatchType(out typeReference))
						return false;
					else {
						this.MarkGenericParameters(scopedGenericTypeArguments, typeReference, true);
					}
					if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Operator)) {
						if (!this.Match(CSharpTokenID.Operator))
							return false;
						// Operator declaration
						// NOTE: Attributes and OperatorModifier (alias Modifier) moved to callers of StructMemberDeclaration to reduce ambiguity
						OperatorType operatorType;
						TypeReference parameter1TypeReference;
						ParameterDeclaration parameter1;
						TypeReference parameter2TypeReference = null;
						ParameterDeclaration parameter2 = null;
						if (!this.MatchOverloadableOperator(out operatorType))
							return false;
						if (!this.Match(CSharpTokenID.OpenParenthesis))
							return false;
						int parameterStartOffset = this.LookAheadToken.StartOffset;
						if (!this.MatchType(out parameter1TypeReference))
							return false;
						else {
							this.MarkGenericParameters(scopedGenericTypeArguments, parameter1TypeReference, true);
						}
						if (!this.MatchSimpleIdentifier())
							return false;
						parameter1 = new ParameterDeclaration(ParameterModifiers.None, this.TokenText);
						parameter1.StartOffset = parameterStartOffset;
						parameter1.EndOffset = this.Token.EndOffset;
						parameter1.ParameterType = parameter1TypeReference;
						if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) {
							parameterStartOffset = this.LookAheadToken.StartOffset;
							if (!this.Match(CSharpTokenID.Comma))
								return false;
							if (!this.MatchType(out parameter2TypeReference))
								return false;
							else {
								this.MarkGenericParameters(scopedGenericTypeArguments, parameter2TypeReference, true);
							}
							if (!this.MatchSimpleIdentifier())
								return false;
							parameter2 = new ParameterDeclaration(ParameterModifiers.None, this.TokenText);
							parameter2.StartOffset = parameterStartOffset;
							parameter2.EndOffset = this.Token.EndOffset;
							parameter2.ParameterType = parameter2TypeReference;
						}
						if (!this.Match(CSharpTokenID.CloseParenthesis))
							return false;
						OperatorDeclaration operatorDeclaration = new OperatorDeclaration(modifiers, operatorType);
						operatorDeclaration.Documentation = this.ReapDocumentationComments();
						operatorDeclaration.StartOffset = startOffset;
						operatorDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
						operatorDeclaration.ReturnType = typeReference;
						if (parameter1 != null)
							operatorDeclaration.Parameters.Add(parameter1);
						if (parameter2 != null)
							operatorDeclaration.Parameters.Add(parameter2);
						if (this.TokenIs(this.LookAheadToken, CSharpTokenID.SemiColon)) {
							if (!this.Match(CSharpTokenID.SemiColon))
								return false;
						}
						else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenCurlyBrace)) {
							operatorDeclaration.BlockStartOffset = this.LookAheadToken.StartOffset;
							Statement statement;
							int memberCurlyBraceLevel = curlyBraceLevel;
							if (!this.Match(CSharpTokenID.OpenCurlyBrace))
								return false;
							bool errorReported = false;
							while (!this.IsAtEnd) {
								if ((this.TokenIs(this.LookAheadToken, CSharpTokenID.CloseCurlyBrace)) && (curlyBraceLevel == memberCurlyBraceLevel + 1))
									break;
								else if ((this.IsInMultiMatchSet(15, this.LookAheadToken) || (this.IsLambdaExpression()) || (this.IsQueryExpression()) || (this.IsDefaultValueExpression()))) {
									errorReported = false;
									while ((this.IsInMultiMatchSet(16, this.LookAheadToken) || (this.IsLambdaExpression()) || (this.IsQueryExpression()) || (this.IsDefaultValueExpression()))) {
										if (!this.MatchStatement(out statement)) {
											if (!this.TokenIs(this.LookAheadToken, CSharpTokenID.CloseCurlyBrace)) this.AdvanceToNext();
										}
										else {
											operatorDeclaration.Statements.Add(statement);
										}
									}
								}
								else {
									// Error recovery:  Advance to the next token since nothing was matched
									if (!errorReported) {
										this.ReportSyntaxError(AssemblyInfo.Instance.Resources.GetString("SemanticParserError_StatementExpected"));
										errorReported = true;
									}
									this.AdvanceToNext();
								}
							}
							this.ReapDocumentationComments();
							
							// Reap comments
							this.ReapComments(operatorDeclaration.Comments, false);
							this.Match(CSharpTokenID.CloseCurlyBrace);
							operatorDeclaration.BlockEndOffset = this.Token.EndOffset;
						}
						else
							return false;
						operatorDeclaration.EndOffset = this.Token.EndOffset;
						this.BlockAddChild(operatorDeclaration);
					}
					else if (this.IsInMultiMatchSet(30, this.LookAheadToken)) {
						if (!this.IsVariableDeclarator()) {
							TypeReference interfaceType;
							QualifiedIdentifier name;
							AstNodeList typeParameterList;
							if (!this.MatchMemberName(out interfaceType, out name, out typeParameterList))
								return false;
							// Add to the scoped generic type arguments
							if ((typeParameterList != null) && (typeParameterList.Count > 0))
								scopedGenericTypeArguments.AddRange(typeParameterList.ToArray());
							if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenSquareBrace)) {
								// Indexer declaration
								// NOTE: Attributes and IndexerModifier (alias Modifier) moved to callers of StructMemberDeclaration to reduce ambiguity
								if (!this.Match(CSharpTokenID.OpenSquareBrace))
									return false;
								AstNodeList parameterList;
								if (!this.MatchFormalParameterList(scopedGenericTypeArguments, out parameterList))
									return false;
								if (!this.Match(CSharpTokenID.CloseSquareBrace))
									return false;
								PropertyDeclaration indexerDeclaration = new PropertyDeclaration(modifiers, null);
								indexerDeclaration.Documentation = this.ReapDocumentationComments();
								indexerDeclaration.StartOffset = startOffset;
								indexerDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
								indexerDeclaration.ReturnType = typeReference;
								if (interfaceType != null)
									indexerDeclaration.ImplementedMembers.Add(new MemberSpecifier(interfaceType, name));
								indexerDeclaration.Parameters.AddRange(parameterList.ToArray());
								indexerDeclaration.BlockStartOffset = this.LookAheadToken.StartOffset;
								AttributeSection attributeSection;
								bool isGetAccessor = true;
								Statement block = null;
								Modifiers singleModifier;
								if (!this.Match(CSharpTokenID.OpenCurlyBrace))
									return false;
								AccessorDeclaration accessorDeclaration = new AccessorDeclaration();
								accessorDeclaration.StartOffset = this.LookAheadToken.StartOffset;
								while (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenSquareBrace)) {
									if (!this.MatchAttributeSection(out attributeSection))
										return false;
									else {
										accessorDeclaration.AttributeSections.Add(attributeSection);
									}
								}
								while (this.IsInMultiMatchSet(21, this.LookAheadToken)) {
									if (!this.MatchModifier(out singleModifier))
										return false;
									else {
										accessorDeclaration.Modifiers |= singleModifier;
									}
								}
								if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Get)) {
									if (!this.Match(CSharpTokenID.Get))
										return false;
									else {
										isGetAccessor = true;
									}
								}
								else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Set)) {
									if (!this.Match(CSharpTokenID.Set))
										return false;
									else {
										isGetAccessor = false;
									}
								}
								else
									return false;
								if (this.TokenIs(this.LookAheadToken, CSharpTokenID.SemiColon)) {
									if (!this.Match(CSharpTokenID.SemiColon))
										return false;
								}
								else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenCurlyBrace)) {
									if (!this.MatchBlock(out block))
										return false;
								}
								else
									return false;
								accessorDeclaration.BlockStatement = block as BlockStatement;
								accessorDeclaration.EndOffset = this.Token.EndOffset;
								if (isGetAccessor)
									indexerDeclaration.GetAccessor = accessorDeclaration;
								else
									indexerDeclaration.SetAccessor = accessorDeclaration;
								if (this.IsInMultiMatchSet(31, this.LookAheadToken)) {
									isGetAccessor = true;
									block = null;
									accessorDeclaration = new AccessorDeclaration();
									accessorDeclaration.StartOffset = this.LookAheadToken.StartOffset;
									while (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenSquareBrace)) {
										if (!this.MatchAttributeSection(out attributeSection))
											return false;
										else {
											accessorDeclaration.AttributeSections.Add(attributeSection);
										}
									}
									while (this.IsInMultiMatchSet(21, this.LookAheadToken)) {
										if (!this.MatchModifier(out singleModifier))
											return false;
										else {
											accessorDeclaration.Modifiers |= singleModifier;
										}
									}
									if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Get)) {
										if (!this.Match(CSharpTokenID.Get))
											return false;
										else {
											isGetAccessor = true;
										}
									}
									else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Set)) {
										if (!this.Match(CSharpTokenID.Set))
											return false;
										else {
											isGetAccessor = false;
										}
									}
									else
										return false;
									if (this.TokenIs(this.LookAheadToken, CSharpTokenID.SemiColon)) {
										if (!this.Match(CSharpTokenID.SemiColon))
											return false;
									}
									else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenCurlyBrace)) {
										if (!this.MatchBlock(out block))
											return false;
									}
									else
										return false;
									accessorDeclaration.BlockStatement = block as BlockStatement;
									accessorDeclaration.EndOffset = this.Token.EndOffset;
									if (isGetAccessor)
										indexerDeclaration.GetAccessor = accessorDeclaration;
									else
										indexerDeclaration.SetAccessor = accessorDeclaration;
								}
								this.ReapDocumentationComments();
								this.Match(CSharpTokenID.CloseCurlyBrace);
								indexerDeclaration.BlockEndOffset = this.Token.EndOffset;
								indexerDeclaration.EndOffset = this.Token.EndOffset;
								this.BlockAddChild(indexerDeclaration);
							}
							else if (((this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenParenthesis)) || (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenCurlyBrace)))) {
								if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenParenthesis)) {
									// Method declaration
									MethodDeclaration methodDeclaration = new MethodDeclaration(modifiers, name);
									methodDeclaration.Documentation = this.ReapDocumentationComments();
									methodDeclaration.StartOffset = startOffset;
									methodDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
									if (typeReference != null) {
										methodDeclaration.ReturnType = typeReference;
										this.MarkGenericParameters(scopedGenericTypeArguments, typeReference, true);
									}
									if (interfaceType != null)
										methodDeclaration.ImplementedMembers.Add(new MemberSpecifier(interfaceType, name));
									if (typeParameterList != null)
										methodDeclaration.GenericTypeArguments.AddRange(typeParameterList.ToArray());
									AstNodeList parameterList = null;
									bool isExtension = false;
									if (!this.Match(CSharpTokenID.OpenParenthesis))
										return false;
									if (this.TokenIs(this.LookAheadToken, CSharpTokenID.This)) {
										if (this.Match(CSharpTokenID.This)) {
											// Ensure that the containing block is a static class declaration
											if ((parentTypeDeclaration == null) || (!(parentTypeDeclaration is ClassDeclaration)) ||
											((((ClassDeclaration)parentTypeDeclaration).Modifiers & Modifiers.Static) == 0)) {
												this.ReportSyntaxError(this.Token.TextRange, AssemblyInfo.Instance.Resources.GetString("SemanticParserError_ExtensionMethodsInStaticClass"));
											}
											else {
												isExtension = true;
												
												// Add an Extension attribute to the method
												AttributeSection attributeSection = new AttributeSection();
												attributeSection.Attributes.Add(new ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast.Attribute(new TypeReference("System.Runtime.CompilerServices.Extension", TextRange.Deleted)));
												methodDeclaration.AttributeSections.Add(attributeSection);
												
												// Add an Extension attribute to the type declaration
												if (!parentTypeDeclaration.IsExtension)
													parentTypeDeclaration.AttributeSections.Add(attributeSection);
											}
										}
									}
									if (this.IsInMultiMatchSet(26, this.LookAheadToken)) {
										if (!this.MatchFormalParameterList(scopedGenericTypeArguments, out parameterList))
											return false;
									}
									if ((parameterList != null) && (parameterList.Count > 0))
										methodDeclaration.Parameters.AddRange(parameterList.ToArray());
									else if (isExtension)
										this.ReportSyntaxError(this.Token.TextRange, AssemblyInfo.Instance.Resources.GetString("SemanticParserError_ExtensionMethodsRequireOneParameter"));
									if (!this.Match(CSharpTokenID.CloseParenthesis))
										return false;
									if (this.IsInMultiMatchSet(17, this.LookAheadToken)) {
										if (!this.MatchTypeParameterConstraintsClauses(typeParameterList))
											return false;
									}
									if (this.TokenIs(this.LookAheadToken, CSharpTokenID.SemiColon)) {
										if (!this.Match(CSharpTokenID.SemiColon))
											return false;
									}
									else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenCurlyBrace)) {
										methodDeclaration.BlockStartOffset = this.LookAheadToken.StartOffset;
										Statement statement;
										int memberCurlyBraceLevel = curlyBraceLevel;
										if (!this.Match(CSharpTokenID.OpenCurlyBrace))
											return false;
										bool errorReported = false;
										while (!this.IsAtEnd) {
											if ((this.TokenIs(this.LookAheadToken, CSharpTokenID.CloseCurlyBrace)) && (curlyBraceLevel == memberCurlyBraceLevel + 1))
												break;
											else if ((this.IsInMultiMatchSet(15, this.LookAheadToken) || (this.IsLambdaExpression()) || (this.IsQueryExpression()) || (this.IsDefaultValueExpression()))) {
												errorReported = false;
												while ((this.IsInMultiMatchSet(16, this.LookAheadToken) || (this.IsLambdaExpression()) || (this.IsQueryExpression()) || (this.IsDefaultValueExpression()))) {
													if (!this.MatchStatement(out statement)) {
														if (!this.TokenIs(this.LookAheadToken, CSharpTokenID.CloseCurlyBrace)) this.AdvanceToNext();
													}
													else {
														methodDeclaration.Statements.Add(statement);
													}
												}
											}
											else {
												// Error recovery:  Advance to the next token since nothing was matched
												if (!errorReported) {
													this.ReportSyntaxError(AssemblyInfo.Instance.Resources.GetString("SemanticParserError_StatementExpected"));
													errorReported = true;
												}
												this.AdvanceToNext();
											}
										}
										this.ReapDocumentationComments();
										
										// Reap comments
										this.ReapComments(methodDeclaration.Comments, false);
										this.Match(CSharpTokenID.CloseCurlyBrace);
										methodDeclaration.BlockEndOffset = this.Token.EndOffset;
									}
									else
										return false;
									methodDeclaration.EndOffset = this.Token.EndOffset;
									this.BlockAddChild(methodDeclaration);
								}
								else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenCurlyBrace)) {
									// Property declaration
									PropertyDeclaration propertyDeclaration = new PropertyDeclaration(modifiers, name);
									propertyDeclaration.Documentation = this.ReapDocumentationComments();
									propertyDeclaration.StartOffset = startOffset;
									propertyDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
									propertyDeclaration.ReturnType = typeReference;
									if (interfaceType != null)
										propertyDeclaration.ImplementedMembers.Add(new MemberSpecifier(interfaceType, name));
									propertyDeclaration.BlockStartOffset = this.LookAheadToken.StartOffset;
									AttributeSection attributeSection;
									bool isGetAccessor = true;
									Statement block = null;
									Modifiers singleModifier;
									if (!this.Match(CSharpTokenID.OpenCurlyBrace))
										return false;
									AccessorDeclaration accessorDeclaration = new AccessorDeclaration();
									accessorDeclaration.StartOffset = this.LookAheadToken.StartOffset;
									while (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenSquareBrace)) {
										if (!this.MatchAttributeSection(out attributeSection))
											return false;
										else {
											accessorDeclaration.AttributeSections.Add(attributeSection);
										}
									}
									while (this.IsInMultiMatchSet(21, this.LookAheadToken)) {
										if (!this.MatchModifier(out singleModifier))
											return false;
										else {
											accessorDeclaration.Modifiers |= singleModifier;
										}
									}
									if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Get)) {
										if (!this.Match(CSharpTokenID.Get))
											return false;
										else {
											isGetAccessor = true;
										}
									}
									else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Set)) {
										if (!this.Match(CSharpTokenID.Set))
											return false;
										else {
											isGetAccessor = false;
										}
									}
									else
										return false;
									if (this.TokenIs(this.LookAheadToken, CSharpTokenID.SemiColon)) {
										if (!this.Match(CSharpTokenID.SemiColon))
											return false;
									}
									else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenCurlyBrace)) {
										if (!this.MatchBlock(out block))
											return false;
									}
									else
										return false;
									accessorDeclaration.BlockStatement = block as BlockStatement;
									accessorDeclaration.EndOffset = this.Token.EndOffset;
									if (isGetAccessor)
										propertyDeclaration.GetAccessor = accessorDeclaration;
									else
										propertyDeclaration.SetAccessor = accessorDeclaration;
									if (this.IsInMultiMatchSet(31, this.LookAheadToken)) {
										isGetAccessor = true;
										block = null;
										accessorDeclaration = new AccessorDeclaration();
										accessorDeclaration.StartOffset = this.LookAheadToken.StartOffset;
										while (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenSquareBrace)) {
											if (!this.MatchAttributeSection(out attributeSection))
												return false;
											else {
												accessorDeclaration.AttributeSections.Add(attributeSection);
											}
										}
										while (this.IsInMultiMatchSet(21, this.LookAheadToken)) {
											if (!this.MatchModifier(out singleModifier))
												return false;
											else {
												accessorDeclaration.Modifiers |= singleModifier;
											}
										}
										if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Get)) {
											if (!this.Match(CSharpTokenID.Get))
												return false;
											else {
												isGetAccessor = true;
											}
										}
										else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Set)) {
											if (!this.Match(CSharpTokenID.Set))
												return false;
											else {
												isGetAccessor = false;
											}
										}
										else
											return false;
										if (this.TokenIs(this.LookAheadToken, CSharpTokenID.SemiColon)) {
											if (!this.Match(CSharpTokenID.SemiColon))
												return false;
										}
										else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenCurlyBrace)) {
											if (!this.MatchBlock(out block))
												return false;
										}
										else
											return false;
										accessorDeclaration.BlockStatement = block as BlockStatement;
										accessorDeclaration.EndOffset = this.Token.EndOffset;
										if (isGetAccessor)
											propertyDeclaration.GetAccessor = accessorDeclaration;
										else
											propertyDeclaration.SetAccessor = accessorDeclaration;
									}
									this.ReapDocumentationComments();
									this.Match(CSharpTokenID.CloseCurlyBrace);
									propertyDeclaration.BlockEndOffset = this.Token.EndOffset;
									propertyDeclaration.EndOffset = this.Token.EndOffset;
									this.BlockAddChild(propertyDeclaration);
								}
								else
									return false;
							}
							else
								return false;
						}
						else {
							// Field declaration
							//  NOTE: Attributes and FieldModifier (alias Modifier) moved to callers of StructMemberDeclaration to reduce ambiguity
							FieldDeclaration fieldDeclaration = new FieldDeclaration(modifiers);
							fieldDeclaration.Documentation = this.ReapDocumentationComments();
							fieldDeclaration.StartOffset = startOffset;
							fieldDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
							if (!this.MatchVariableDeclarator(fieldDeclaration, typeReference))
								return false;
							while (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) {
								if (!this.Match(CSharpTokenID.Comma))
									return false;
								if (!this.MatchVariableDeclarator(fieldDeclaration, typeReference))
									return false;
							}
							if (!this.Match(CSharpTokenID.SemiColon))
								return false;
							fieldDeclaration.EndOffset = this.Token.EndOffset;
							this.BlockAddChild(fieldDeclaration);
						}
					}
					else
						return false;
				}
				else {
					QualifiedIdentifier identifier;
					if (!this.MatchIdentifier(out identifier))
						return false;
					// Constructor declaration
					bool isStatic = ((modifiers & Modifiers.Static) == Modifiers.Static);
					ConstructorDeclaration constructorDeclaration = new ConstructorDeclaration(modifiers, identifier);
					constructorDeclaration.Documentation = this.ReapDocumentationComments();
					constructorDeclaration.StartOffset = startOffset;
					constructorDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
					AstNodeList parameterList = null;
					if (!this.Match(CSharpTokenID.OpenParenthesis))
						return false;
					if (!isStatic) {
						if (this.IsInMultiMatchSet(26, this.LookAheadToken)) {
							if (!this.MatchFormalParameterList(scopedGenericTypeArguments, out parameterList))
								return false;
						}
						if (parameterList != null)
							constructorDeclaration.Parameters.AddRange(parameterList.ToArray());
					}
					if (!this.Match(CSharpTokenID.CloseParenthesis))
						return false;
					if (!isStatic) {
						if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Colon)) {
							if (!this.Match(CSharpTokenID.Colon))
								return false;
							if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Base)) {
								if (!this.Match(CSharpTokenID.Base))
									return false;
								else {
									constructorDeclaration.InitializerType = ConstructorInitializerType.Base;
								}
							}
							else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.This)) {
								if (!this.Match(CSharpTokenID.This))
									return false;
								else {
									constructorDeclaration.InitializerType = ConstructorInitializerType.This;
								}
							}
							else
								return false;
							AstNodeList initializerArgumentList = null;
							if (!this.Match(CSharpTokenID.OpenParenthesis))
								return false;
							if ((this.IsInMultiMatchSet(7, this.LookAheadToken) || (this.IsLambdaExpression()) || (this.IsQueryExpression()) || (this.IsDefaultValueExpression()))) {
								if (!this.MatchArgumentList(out initializerArgumentList))
									return false;
							}
							if (!this.Match(CSharpTokenID.CloseParenthesis))
								return false;
							if (initializerArgumentList != null)
								constructorDeclaration.InitializerArguments.AddRange(initializerArgumentList.ToArray());
						}
					}
					if (this.TokenIs(this.LookAheadToken, CSharpTokenID.SemiColon)) {
						if (!this.Match(CSharpTokenID.SemiColon))
							return false;
					}
					else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenCurlyBrace)) {
						constructorDeclaration.BlockStartOffset = this.LookAheadToken.StartOffset;
						Statement statement;
						int memberCurlyBraceLevel = curlyBraceLevel;
						if (!this.Match(CSharpTokenID.OpenCurlyBrace))
							return false;
						bool errorReported = false;
						while (!this.IsAtEnd) {
							if ((this.TokenIs(this.LookAheadToken, CSharpTokenID.CloseCurlyBrace)) && (curlyBraceLevel == memberCurlyBraceLevel + 1))
								break;
							else if ((this.IsInMultiMatchSet(15, this.LookAheadToken) || (this.IsLambdaExpression()) || (this.IsQueryExpression()) || (this.IsDefaultValueExpression()))) {
								errorReported = false;
								while ((this.IsInMultiMatchSet(16, this.LookAheadToken) || (this.IsLambdaExpression()) || (this.IsQueryExpression()) || (this.IsDefaultValueExpression()))) {
									if (!this.MatchStatement(out statement)) {
										if (!this.TokenIs(this.LookAheadToken, CSharpTokenID.CloseCurlyBrace)) this.AdvanceToNext();
									}
									else {
										constructorDeclaration.Statements.Add(statement);
									}
								}
							}
							else {
								// Error recovery:  Advance to the next token since nothing was matched
								if (!errorReported) {
									this.ReportSyntaxError(AssemblyInfo.Instance.Resources.GetString("SemanticParserError_StatementExpected"));
									errorReported = true;
								}
								this.AdvanceToNext();
							}
						}
						this.ReapDocumentationComments();
						
						// Reap comments
						this.ReapComments(constructorDeclaration.Comments, false);
						this.Match(CSharpTokenID.CloseCurlyBrace);
						constructorDeclaration.BlockEndOffset = this.Token.EndOffset;
					}
					else
						return false;
					constructorDeclaration.EndOffset = this.Token.EndOffset;
					this.BlockAddChild(constructorDeclaration);
				}
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>ArrayInitializer</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ArrayInitializer</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>OpenCurlyBrace</c>.
		/// </remarks>
		protected virtual bool MatchArrayInitializer(out Expression expression) {
			expression = new ObjectCollectionInitializerExpression();
			expression.StartOffset = this.LookAheadToken.StartOffset;
			if (!this.Match(CSharpTokenID.OpenCurlyBrace))
				return false;
			if ((this.IsInMultiMatchSet(32, this.LookAheadToken) || (this.IsLambdaExpression()) || (this.IsQueryExpression()) || (this.IsDefaultValueExpression()))) {
				Expression variableInitializer;
				if (!this.MatchVariableInitializer(out variableInitializer))
					return false;
				else {
					((ObjectCollectionInitializerExpression)expression).Initializers.Add(variableInitializer);
				}
				while (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) {
					if (!this.Match(CSharpTokenID.Comma))
						return false;
					if (this.TokenIs(this.LookAheadToken, CSharpTokenID.CloseCurlyBrace))
						break;
					if (!this.MatchVariableInitializer(out variableInitializer))
						return false;
					else {
						((ObjectCollectionInitializerExpression)expression).Initializers.Add(variableInitializer);
					}
				}
			}
			if (!this.Match(CSharpTokenID.CloseCurlyBrace))
				return false;
			expression.EndOffset = this.Token.EndOffset;
			return true;
		}

		/// <summary>
		/// Matches a <c>InterfaceDeclaration</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>InterfaceDeclaration</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Interface</c>.
		/// </remarks>
		protected virtual bool MatchInterfaceDeclaration(int startOffset, AstNodeList attributeSections, Modifiers modifiers) {
			if (!this.Match(CSharpTokenID.Interface))
				return false;
			QualifiedIdentifier identifier;
			if (!this.MatchIdentifier(out identifier))
				return false;
			// Default to internal access
			if (!AstNode.IsAccessSpecified(modifiers))
				modifiers |= Modifiers.Assembly;
			
			InterfaceDeclaration interfaceDeclaration = new InterfaceDeclaration(modifiers, identifier);
			interfaceDeclaration.Documentation = this.ReapDocumentationComments();
			interfaceDeclaration.StartOffset = startOffset;
			interfaceDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
			AstNodeList typeParameterList = null;
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.LessThan)) {
				if (!this.MatchVariantTypeParameterList(out typeParameterList))
					return false;
			}
			if (typeParameterList != null)
				interfaceDeclaration.GenericTypeArguments.AddRange(typeParameterList.ToArray());
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Colon)) {
				this.Match(CSharpTokenID.Colon);
				TypeReference typeReference;
				if (!this.MatchClassType(false, out typeReference)) {
					// Error recovery:  Go to the next open curly brace
					this.AdvanceToNext(CSharpTokenID.OpenCurlyBrace);
				}
				else {
					interfaceDeclaration.BaseTypes.Add(typeReference);
				}
				while (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) {
					if (!this.Match(CSharpTokenID.Comma)) {
						// Error recovery:  Go to the next open curly brace
						this.AdvanceToNext(CSharpTokenID.OpenCurlyBrace);
					}
					if (!this.MatchClassType(false, out typeReference)) {
						// Error recovery:  Go to the next open curly brace
						this.AdvanceToNext(CSharpTokenID.OpenCurlyBrace);
					}
					else {
						interfaceDeclaration.BaseTypes.Add(typeReference);
					}
				}
			}
			if (this.IsInMultiMatchSet(17, this.LookAheadToken)) {
				if (!this.MatchTypeParameterConstraintsClauses(typeParameterList))
					return false;
			}
			this.BlockAddChild(interfaceDeclaration);
			interfaceDeclaration.BlockStartOffset = this.LookAheadToken.StartOffset;
			int interfaceCurlyBraceLevel = curlyBraceLevel;
			if (!this.Match(CSharpTokenID.OpenCurlyBrace))
				return false;
			this.BlockStart(interfaceDeclaration);
			bool errorReported = false;
			while (!this.IsAtEnd) {
				if ((this.TokenIs(this.LookAheadToken, CSharpTokenID.CloseCurlyBrace)) && (curlyBraceLevel == interfaceCurlyBraceLevel + 1))
					break;
				else if (this.IsInMultiMatchSet(33, this.LookAheadToken)) {
					errorReported = false;
					this.MatchInterfaceMemberDeclaration(interfaceDeclaration);
				}
				else {
					// Error recovery:  Advance to the next token since nothing was matched
					if (!errorReported) {
						this.ReportSyntaxError(AssemblyInfo.Instance.Resources.GetString("SemanticParserError_InterfaceMemberDeclarationExpected"));
						errorReported = true;
					}
					this.AdvanceToNext();
				}
			}
			this.ReapDocumentationComments();
			this.Match(CSharpTokenID.CloseCurlyBrace);
			interfaceDeclaration.BlockEndOffset = this.Token.EndOffset;
			this.BlockEnd();
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.SemiColon)) {
				this.Match(CSharpTokenID.SemiColon);
			}
			interfaceDeclaration.EndOffset = this.Token.EndOffset;
			
			// Reap comments
			this.ReapComments(interfaceDeclaration.Comments, false);
			return true;
		}

		/// <summary>
		/// Matches a <c>InterfaceMemberDeclaration</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>InterfaceMemberDeclaration</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Void</c>, <c>OpenSquareBrace</c>, <c>New</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>, <c>Event</c>.
		/// </remarks>
		protected virtual bool MatchInterfaceMemberDeclaration(TypeDeclaration parentTypeDeclaration) {
			AttributeSection attributeSection;
			AstNodeList attributeSections = new AstNodeList(null);
			Modifiers modifiers = Modifiers.None;
			TypeReference typeReference;
			while (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenSquareBrace)) {
				if (!this.MatchAttributeSection(out attributeSection))
					return false;
				else {
					attributeSections.Add(attributeSection);
				}
			}
			int startOffset = this.LookAheadToken.StartOffset;
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.New)) {
				if (this.Match(CSharpTokenID.New)) {
					modifiers = Modifiers.New;
				}
			}
			if (this.IsInMultiMatchSet(29, this.LookAheadToken)) {
				// Build a list of generic type arguments for the parent type... will add to this collection if a generic method
				AstNodeList scopedGenericTypeArguments = new AstNodeList(null);
				if ((parentTypeDeclaration.GenericTypeArguments != null) && (parentTypeDeclaration.GenericTypeArguments.Count > 0))
					scopedGenericTypeArguments.AddRange(parentTypeDeclaration.GenericTypeArguments.ToArray());
				if (!this.MatchReturnType(out typeReference))
					return false;
				else {
					this.MarkGenericParameters(scopedGenericTypeArguments, typeReference, true);
				}
				if (this.TokenIs(this.LookAheadToken, CSharpTokenID.This)) {
					if (!this.Match(CSharpTokenID.This))
						return false;
					if (!this.Match(CSharpTokenID.OpenSquareBrace))
						return false;
					AstNodeList parameterList;
					AstNodeList accessorAttributeSections = new AstNodeList(null);
					InterfaceAccessor getAccessor;
					InterfaceAccessor setAccessor;
					if (!this.MatchFormalParameterList(scopedGenericTypeArguments, out parameterList))
						return false;
					if (!this.Match(CSharpTokenID.CloseSquareBrace))
						return false;
					InterfacePropertyDeclaration indexerDeclaration = new InterfacePropertyDeclaration(modifiers, null);
					indexerDeclaration.Documentation = this.ReapDocumentationComments();
					indexerDeclaration.StartOffset = startOffset;
					if (!this.Match(CSharpTokenID.OpenCurlyBrace))
						return false;
					if (!this.MatchInterfaceAccessors(out getAccessor, out setAccessor))
						return false;
					if (!this.Match(CSharpTokenID.CloseCurlyBrace))
						return false;
					indexerDeclaration.EndOffset = this.Token.EndOffset;
					indexerDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
					indexerDeclaration.ReturnType = typeReference;
					indexerDeclaration.Parameters.AddRange(parameterList.ToArray());
					indexerDeclaration.GetAccessor = getAccessor;
					indexerDeclaration.SetAccessor = setAccessor;
					this.BlockAddChild(indexerDeclaration);
				}
				else if (this.IsInMultiMatchSet(3, this.LookAheadToken)) {
					QualifiedIdentifier identifier;
					AstNodeList typeParameterList = null;
					if (!this.MatchIdentifier(out identifier))
						return false;
					if (((this.TokenIs(this.LookAheadToken, CSharpTokenID.LessThan)) || (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenParenthesis)))) {
						if (this.TokenIs(this.LookAheadToken, CSharpTokenID.LessThan)) {
							if (!this.MatchTypeParameterList(out typeParameterList))
								return false;
						}
						if (!this.Match(CSharpTokenID.OpenParenthesis))
							return false;
						AstNodeList parameterList = null;
						if (this.IsInMultiMatchSet(26, this.LookAheadToken)) {
							if (!this.MatchFormalParameterList(scopedGenericTypeArguments, out parameterList))
								return false;
						}
						if (!this.Match(CSharpTokenID.CloseParenthesis))
							return false;
						if (this.IsInMultiMatchSet(17, this.LookAheadToken)) {
							if (!this.MatchTypeParameterConstraintsClauses(typeParameterList))
								return false;
						}
						InterfaceMethodDeclaration methodDeclaration = new InterfaceMethodDeclaration(modifiers, identifier);
						methodDeclaration.Documentation = this.ReapDocumentationComments();
						methodDeclaration.StartOffset = startOffset;
						this.Match(CSharpTokenID.SemiColon);
						methodDeclaration.EndOffset = this.Token.EndOffset;
						methodDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
						if (typeReference != null)
							methodDeclaration.ReturnType = typeReference;
						if (typeParameterList != null)
							methodDeclaration.GenericTypeArguments.AddRange(typeParameterList.ToArray());
						if (parameterList != null)
							methodDeclaration.Parameters.AddRange(parameterList.ToArray());
						this.BlockAddChild(methodDeclaration);
					}
					else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenCurlyBrace)) {
						InterfaceAccessor getAccessor;
						InterfaceAccessor setAccessor;
						if (!this.Match(CSharpTokenID.OpenCurlyBrace))
							return false;
						if (!this.MatchInterfaceAccessors(out getAccessor, out setAccessor))
							return false;
						InterfacePropertyDeclaration propertyDeclaration = new InterfacePropertyDeclaration(modifiers, identifier);
						propertyDeclaration.Documentation = this.ReapDocumentationComments();
						propertyDeclaration.StartOffset = startOffset;
						if (!this.Match(CSharpTokenID.CloseCurlyBrace))
							return false;
						propertyDeclaration.EndOffset = this.Token.EndOffset;
						propertyDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
						propertyDeclaration.ReturnType = typeReference;
						propertyDeclaration.GetAccessor = getAccessor;
						propertyDeclaration.SetAccessor = setAccessor;
						this.BlockAddChild(propertyDeclaration);
					}
					else
						return false;
				}
				else
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Event)) {
				if (!this.Match(CSharpTokenID.Event))
					return false;
				if (!this.MatchType(out typeReference))
					return false;
				QualifiedIdentifier identifier;
				if (!this.MatchIdentifier(out identifier))
					return false;
				InterfaceEventDeclaration eventDeclaration = new InterfaceEventDeclaration(modifiers, identifier);
				eventDeclaration.Documentation = this.ReapDocumentationComments();
				eventDeclaration.StartOffset = startOffset;
				eventDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
				eventDeclaration.EventType = typeReference;
				this.BlockAddChild(eventDeclaration);
				if (!this.Match(CSharpTokenID.SemiColon))
					return false;
				eventDeclaration.EndOffset = this.Token.EndOffset;
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>InterfaceAccessors</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>InterfaceAccessors</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>OpenSquareBrace</c>, <c>Get</c>, <c>Set</c>.
		/// </remarks>
		protected virtual bool MatchInterfaceAccessors(out InterfaceAccessor getAccessor, out InterfaceAccessor setAccessor) {
			getAccessor = null;
			setAccessor = null;
			AttributeSection attributeSection;
			AstNodeList attributeSections = new AstNodeList(null);
			while (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenSquareBrace)) {
				if (!this.MatchAttributeSection(out attributeSection))
					return false;
				else {
					attributeSections.Add(attributeSection);
				}
			}
			int startOffset = this.LookAheadToken.StartOffset;
			if (((this.TokenIs(this.LookAheadToken, CSharpTokenID.Get)) || (this.TokenIs(this.LookAheadToken, CSharpTokenID.Set)))) {
				if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Get)) {
					if (!this.Match(CSharpTokenID.Get))
						return false;
					if (!this.Match(CSharpTokenID.SemiColon))
						return false;
					getAccessor = new InterfaceAccessor(InterfaceAccessorType.Get);
					getAccessor.StartOffset = startOffset;
					getAccessor.EndOffset = this.Token.EndOffset;
					getAccessor.AttributeSections.AddRange(attributeSections.ToArray());
					attributeSections = new AstNodeList(null);
					startOffset = this.LookAheadToken.StartOffset;
					
					attributeSections = new AstNodeList(null);
					while (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenSquareBrace)) {
						if (!this.MatchAttributeSection(out attributeSection))
							return false;
						else {
							attributeSections.Add(attributeSection);
						}
					}
					if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Set)) {
						if (!this.Match(CSharpTokenID.Set))
							return false;
						if (!this.Match(CSharpTokenID.SemiColon))
							return false;
						setAccessor = new InterfaceAccessor(InterfaceAccessorType.Set);
						setAccessor.StartOffset = startOffset;
						setAccessor.EndOffset = this.Token.EndOffset;
						setAccessor.AttributeSections.AddRange(attributeSections.ToArray());
						attributeSections = new AstNodeList(null);
					}
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Set)) {
					if (!this.Match(CSharpTokenID.Set))
						return false;
					if (!this.Match(CSharpTokenID.SemiColon))
						return false;
					setAccessor = new InterfaceAccessor(InterfaceAccessorType.Set);
					setAccessor.StartOffset = startOffset;
					setAccessor.EndOffset = this.Token.EndOffset;
					setAccessor.AttributeSections.AddRange(attributeSections.ToArray());
					attributeSections = new AstNodeList(null);
					startOffset = this.LookAheadToken.StartOffset;
					
					attributeSections = new AstNodeList(null);
					while (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenSquareBrace)) {
						if (!this.MatchAttributeSection(out attributeSection))
							return false;
						else {
							attributeSections.Add(attributeSection);
						}
					}
					if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Get)) {
						if (!this.Match(CSharpTokenID.Get))
							return false;
						if (!this.Match(CSharpTokenID.SemiColon))
							return false;
						getAccessor = new InterfaceAccessor(InterfaceAccessorType.Get);
						getAccessor.StartOffset = startOffset;
						getAccessor.EndOffset = this.Token.EndOffset;
						getAccessor.AttributeSections.AddRange(attributeSections.ToArray());
						attributeSections = new AstNodeList(null);
					}
				}
				else
					return false;
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>EnumDeclaration</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>EnumDeclaration</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Enum</c>.
		/// </remarks>
		protected virtual bool MatchEnumDeclaration(int startOffset, AstNodeList attributeSections, Modifiers modifiers) {
			if (!this.Match(CSharpTokenID.Enum))
				return false;
			QualifiedIdentifier identifier;
			if (!this.MatchIdentifier(out identifier))
				return false;
			// Default to internal access
			if (!AstNode.IsAccessSpecified(modifiers))
				modifiers |= Modifiers.Assembly;
			
			EnumerationDeclaration enumerationDeclaration = new EnumerationDeclaration(modifiers, identifier);
			enumerationDeclaration.Documentation = this.ReapDocumentationComments();
			enumerationDeclaration.StartOffset = startOffset;
			enumerationDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Colon)) {
				this.Match(CSharpTokenID.Colon);
				TypeReference typeReference;
				if (!this.MatchIntegralType(out typeReference)) {
					// Error recovery:  Go to the next open curly brace
					this.AdvanceToNext(CSharpTokenID.OpenCurlyBrace);
				}
				else {
					enumerationDeclaration.BaseTypes.Add(typeReference);
				}
			}
			this.BlockAddChild(enumerationDeclaration);
			enumerationDeclaration.BlockStartOffset = this.LookAheadToken.StartOffset;
			int enumerationCurlyBraceLevel = curlyBraceLevel;
			if (!this.Match(CSharpTokenID.OpenCurlyBrace))
				return false;
			this.BlockStart(enumerationDeclaration);
			bool errorReported = false;
			while (!this.IsAtEnd) {
				if ((this.TokenIs(this.LookAheadToken, CSharpTokenID.CloseCurlyBrace)) && (curlyBraceLevel == enumerationCurlyBraceLevel + 1))
					break;
				else if (this.IsInMultiMatchSet(34, this.LookAheadToken)) {
					errorReported = false;
					this.MatchEnumMemberDeclaration();
					if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) {
						this.Match(CSharpTokenID.Comma);
					}
				}
				else {
					// Error recovery:  Advance to the next token since nothing was matched
					if (!errorReported) {
						this.ReportSyntaxError(AssemblyInfo.Instance.Resources.GetString("SemanticParserError_EnumerationMemberDeclarationExpected"));
						errorReported = true;
					}
					this.AdvanceToNext();
				}
			}
			this.ReapDocumentationComments();
			this.Match(CSharpTokenID.CloseCurlyBrace);
			enumerationDeclaration.BlockEndOffset = this.Token.EndOffset;
			this.BlockEnd();
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.SemiColon)) {
				this.Match(CSharpTokenID.SemiColon);
			}
			enumerationDeclaration.EndOffset = this.Token.EndOffset;
			
			// Reap comments
			this.ReapComments(enumerationDeclaration.Comments, false);
			return true;
		}

		/// <summary>
		/// Matches a <c>EnumMemberDeclaration</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>EnumMemberDeclaration</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>OpenSquareBrace</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>.
		/// </remarks>
		protected virtual bool MatchEnumMemberDeclaration() {
			AttributeSection attributeSection;
			AstNodeList attributeSections = new AstNodeList(null);
			while (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenSquareBrace)) {
				if (!this.MatchAttributeSection(out attributeSection))
					return false;
				else {
					attributeSections.Add(attributeSection);
				}
			}
			int startOffset = this.LookAheadToken.StartOffset;
			QualifiedIdentifier identifier;
			if (!this.MatchIdentifier(out identifier))
				return false;
			EnumerationMemberDeclaration memberDeclaration = new EnumerationMemberDeclaration(identifier);
			memberDeclaration.Documentation = this.ReapDocumentationComments();
			memberDeclaration.StartOffset = startOffset;
			memberDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
			this.BlockAddChild(memberDeclaration);
			Expression initializer;
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Assignment)) {
				if (!this.Match(CSharpTokenID.Assignment))
					return false;
				if (!this.MatchExpression(out initializer))
					return false;
				else {
					memberDeclaration.Initializer = initializer;
				}
			}
			memberDeclaration.EndOffset = this.Token.EndOffset;
			return true;
		}

		/// <summary>
		/// Matches a <c>DelegateDeclaration</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>DelegateDeclaration</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Delegate</c>.
		/// </remarks>
		protected virtual bool MatchDelegateDeclaration(int startOffset, AstNodeList attributeSections, Modifiers modifiers) {
			TypeReference typeReference;
			AstNodeList parameterList = null;
			if (!this.Match(CSharpTokenID.Delegate))
				return false;
			if (!this.MatchReturnType(out typeReference))
				return false;
			QualifiedIdentifier identifier;
			if (!this.MatchIdentifier(out identifier))
				return false;
			// Default to internal access
			if (!AstNode.IsAccessSpecified(modifiers))
				modifiers |= Modifiers.Assembly;
			
			DelegateDeclaration delegateDeclaration = new DelegateDeclaration(modifiers, identifier);
			delegateDeclaration.Documentation = this.ReapDocumentationComments();
			delegateDeclaration.StartOffset = startOffset;
			delegateDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
			delegateDeclaration.ReturnType = typeReference;
			AstNodeList typeParameterList = null;
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.LessThan)) {
				if (!this.MatchVariantTypeParameterList(out typeParameterList))
					return false;
			}
			if (typeParameterList != null)
				delegateDeclaration.GenericTypeArguments.AddRange(typeParameterList.ToArray());
			
			// Ensure that generic type parameters are marked properly
			this.MarkGenericParameters(typeParameterList, typeReference, true);
			if (!this.Match(CSharpTokenID.OpenParenthesis))
				return false;
			if (this.IsInMultiMatchSet(26, this.LookAheadToken)) {
				if (!this.MatchFormalParameterList(delegateDeclaration.GenericTypeArguments, out parameterList))
					return false;
			}
			if (!this.Match(CSharpTokenID.CloseParenthesis))
				return false;
			if (this.IsInMultiMatchSet(17, this.LookAheadToken)) {
				if (!this.MatchTypeParameterConstraintsClauses(typeParameterList))
					return false;
			}
			if (parameterList != null)
				delegateDeclaration.Parameters.AddRange(parameterList.ToArray());
			this.BlockAddChild(delegateDeclaration);
			delegateDeclaration.GenerateInvokeMembers();
			if (!this.Match(CSharpTokenID.SemiColon))
				return false;
			delegateDeclaration.EndOffset = this.Token.EndOffset;
			
			// Reap comments
			this.ReapComments(delegateDeclaration.Comments, false);
			return true;
		}

		/// <summary>
		/// Matches a <c>GlobalAttributeSection</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>GlobalAttributeSection</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>OpenSquareBrace</c>.
		/// </remarks>
		protected virtual bool MatchGlobalAttributeSection() {
			if (!this.Match(CSharpTokenID.OpenSquareBrace))
				return false;
			AttributeSection attributeSection = new AttributeSection();
			attributeSection.StartOffset = this.Token.StartOffset;
			
			string target = null;
			if (!this.IsKeywordOrIdentifierAndColon()) {
				while (!this.IsAtEnd) {
					if ((this.TokenIs(this.LookAheadToken, CSharpTokenID.CloseSquareBrace)) || (this.IsIdentifier(this.LookAheadToken)))
						break;
					this.AdvanceToNext();
				}
				return false;
			}
			this.AdvanceToNext();
			target = this.TokenText;
			if (!this.Match(CSharpTokenID.Colon))
				return false;
			if (!this.MatchAttributeList(attributeSection))
				return false;
			if (!this.Match(CSharpTokenID.CloseSquareBrace))
				return false;
			foreach (DotNet.Ast.Attribute attribute in attributeSection.Attributes)
				attribute.Target = target;
			attributeSection.EndOffset = this.Token.EndOffset;
			compilationUnit.GlobalAttributeSections.Add(attributeSection);
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) {
				this.Match(CSharpTokenID.Comma);
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>AttributeSection</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>AttributeSection</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>OpenSquareBrace</c>.
		/// </remarks>
		protected virtual bool MatchAttributeSection(out AttributeSection attributeSection) {
			attributeSection = new AttributeSection();
			attributeSection.StartOffset = this.LookAheadToken.StartOffset;
			string target = null;
			if (!this.Match(CSharpTokenID.OpenSquareBrace))
				return false;
			if (this.IsKeywordOrIdentifierAndColon()) {
				this.AdvanceToNext();
				target = this.TokenText;
				if (!this.Match(CSharpTokenID.Colon))
					return false;
			}
			if (!this.MatchAttributeList(attributeSection))
				return false;
			if (!this.Match(CSharpTokenID.CloseSquareBrace))
				return false;
			foreach (DotNet.Ast.Attribute attribute in attributeSection.Attributes)
				attribute.Target = target;
			attributeSection.EndOffset = this.Token.EndOffset;
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) {
				this.Match(CSharpTokenID.Comma);
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>AttributeList</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>AttributeList</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>.
		/// </remarks>
		protected virtual bool MatchAttributeList(AttributeSection attributeSection) {
			if (!this.MatchAttribute(attributeSection))
				return false;
			while (this.IsCommaAndIdentifier()) {
				if (!this.Match(CSharpTokenID.Comma))
					return false;
				if (!this.MatchAttribute(attributeSection))
					return false;
			}
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) {
				this.Match(CSharpTokenID.Comma);
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>Attribute</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>Attribute</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>.
		/// </remarks>
		protected virtual bool MatchAttribute(AttributeSection attributeSection) {
			int startOffset = this.LookAheadToken.StartOffset;
			TypeReference typeReference;
			if (!this.MatchTypeName(false, out typeReference))
				return false;
			ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast.Attribute attribute = new ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast.Attribute(typeReference);
			attribute.StartOffset = startOffset;
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenParenthesis)) {
				if (!this.MatchAttributeArguments(attribute))
					return false;
			}
			attribute.EndOffset = this.Token.EndOffset;
			attributeSection.Attributes.Add(attribute);
			return true;
		}

		/// <summary>
		/// Matches a <c>AttributeArguments</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>AttributeArguments</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>OpenParenthesis</c>.
		/// </remarks>
		protected virtual bool MatchAttributeArguments(ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast.Attribute attribute) {
			if (!this.Match(CSharpTokenID.OpenParenthesis))
				return false;
			if ((this.IsInMultiMatchSet(14, this.LookAheadToken) || (this.IsLambdaExpression()) || (this.IsQueryExpression()) || (this.IsDefaultValueExpression()))) {
				if (!this.MatchAttributeArgument(attribute))
					return false;
				while (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) {
					if (!this.Match(CSharpTokenID.Comma))
						return false;
					if (!this.MatchAttributeArgument(attribute))
						return false;
				}
			}
			if (!this.Match(CSharpTokenID.CloseParenthesis))
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>AttributeArgument</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>AttributeArgument</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Multiplication</c>, <c>True</c>, <c>False</c>, <c>DecimalIntegerLiteral</c>, <c>HexadecimalIntegerLiteral</c>, <c>RealLiteral</c>, <c>CharacterLiteral</c>, <c>StringLiteral</c>, <c>VerbatimStringLiteral</c>, <c>Null</c>, <c>OpenParenthesis</c>, <c>This</c>, <c>Base</c>, <c>New</c>, <c>TypeOf</c>, <c>SizeOf</c>, <c>Checked</c>, <c>Unchecked</c>, <c>Increment</c>, <c>Decrement</c>, <c>Addition</c>, <c>Subtraction</c>, <c>Negation</c>, <c>OnesComplement</c>, <c>BitwiseAnd</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>, <c>Delegate</c>.
		/// The non-terminal can start with: this.IsLambdaExpression().
		/// The non-terminal can start with: this.IsQueryExpression().
		/// The non-terminal can start with: this.IsDefaultValueExpression().
		/// </remarks>
		protected virtual bool MatchAttributeArgument(ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast.Attribute attribute) {
			int startOffset = this.LookAheadToken.StartOffset;
			string name = null;
			Expression expression;
			if (this.IsInMultiMatchSet(17, this.LookAheadToken)) {
				if (this.AreNextTwoIdentifierAnd(CSharpTokenID.Assignment)) {
					name = this.LookAheadTokenText;
					if (!this.MatchSimpleIdentifier())
						return false;
					if (!this.Match(CSharpTokenID.Assignment))
						return false;
				}
				else if (this.AreNextTwoIdentifierAnd(CSharpTokenID.Colon)) {
					// C# 4.0 named arguments
					name = this.LookAheadTokenText;
					if (!this.MatchSimpleIdentifier())
						return false;
					if (!this.Match(CSharpTokenID.Colon))
						return false;
				}
			}
			if (!this.MatchExpression(out expression))
				return false;
			AttributeArgument argument = new AttributeArgument(name, expression, new TextRange(startOffset, this.Token.EndOffset));
			attribute.Arguments.Add(argument);
			return true;
		}

		/// <summary>
		/// Matches a <c>StackAllocInitializer</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>StackAllocInitializer</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>StackAlloc</c>.
		/// </remarks>
		protected virtual bool MatchStackAllocInitializer(out Expression expression) {
			expression = null;
			int startOffset = this.LookAheadToken.StartOffset;
			TypeReference typeReference;
			Expression childExpression;
			if (!this.Match(CSharpTokenID.StackAlloc))
				return false;
			if (!this.MatchNonArrayType(false, false, out typeReference))
				return false;
			if (!this.Match(CSharpTokenID.OpenSquareBrace))
				return false;
			if (!this.MatchExpression(out childExpression))
				return false;
			if (!this.Match(CSharpTokenID.CloseSquareBrace))
				return false;
			expression = new StackAllocInitializer(typeReference, childExpression, new TextRange(startOffset, this.Token.EndOffset));
			return true;
		}

		/// <summary>
		/// Matches a <c>ExternAliasDirective</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ExternAliasDirective</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Extern</c>.
		/// </remarks>
		protected virtual bool MatchExternAliasDirective() {
			this.Match(CSharpTokenID.Extern);
			int startOffset = this.Token.StartOffset;
			if (compilationUnit.ExternAliasDirectives == null)
				compilationUnit.ExternAliasDirectives = new ExternAliasDirectiveSection();
			if (compilationUnit.ExternAliasDirectives.Directives.Count == 0)
				compilationUnit.ExternAliasDirectives.StartOffset = startOffset;
			if (!this.MatchSimpleIdentifier())
				return false;
			if (this.TokenText != "alias") {
				this.ReportSyntaxError(AssemblyInfo.Instance.Resources.GetString("SemanticParserError_AliasExpected"));
				return false;
			}
			if (!this.MatchSimpleIdentifier())
				return false;
			ExternAliasDirective externAliasDirective = new ExternAliasDirective(this.TokenText, new TextRange(startOffset, this.Token.EndOffset));
			
			// Reap comments
			this.ReapComments(externAliasDirective.Comments, false);
			
			compilationUnit.ExternAliasDirectives.Directives.Add(externAliasDirective);
			compilationUnit.ExternAliasDirectives.EndOffset = this.Token.EndOffset;
			this.Match(CSharpTokenID.SemiColon);
			return true;
		}

		/// <summary>
		/// Matches a <c>TypeParameterList</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>TypeParameterList</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>LessThan</c>.
		/// </remarks>
		protected virtual bool MatchTypeParameterList(out AstNodeList typeParameterList) {
			TypeReference typeParameter;
			typeParameterList = new AstNodeList(null);
			if (!this.Match(CSharpTokenID.LessThan))
				return false;
			if (!this.MatchTypeParameter(out typeParameter))
				return false;
			else {
				typeParameterList.Add(typeParameter);
			}
			while (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) {
				if (!this.Match(CSharpTokenID.Comma))
					return false;
				if (!this.MatchTypeParameter(out typeParameter))
					return false;
				else {
					typeParameterList.Add(typeParameter);
				}
			}
			this.Match(CSharpTokenID.GreaterThan);
			return true;
		}

		/// <summary>
		/// Matches a <c>TypeParameter</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>TypeParameter</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>OpenSquareBrace</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>.
		/// </remarks>
		protected virtual bool MatchTypeParameter(out TypeReference typeParameter) {
			typeParameter = null;
			AttributeSection attributeSection;
			AstNodeList attributeSections = new AstNodeList(null);
			while (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenSquareBrace)) {
				if (!this.MatchAttributeSection(out attributeSection))
					return false;
				else {
					attributeSections.Add(attributeSection);
				}
			}
			if (!this.MatchSimpleIdentifier())
				return false;
			typeParameter = new TypeReference(this.TokenText, this.Token.TextRange);
			typeParameter.IsGenericParameter = true;
			if (attributeSections != null)
				typeParameter.AttributeSections.AddRange(attributeSections.ToArray());
			return true;
		}

		/// <summary>
		/// Matches a <c>VariantTypeParameterList</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>VariantTypeParameterList</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>LessThan</c>.
		/// </remarks>
		protected virtual bool MatchVariantTypeParameterList(out AstNodeList typeParameterList) {
			TypeReference typeParameter;
			typeParameterList = new AstNodeList(null);
			if (!this.Match(CSharpTokenID.LessThan))
				return false;
			if (!this.MatchVariantTypeParameter(out typeParameter))
				return false;
			else {
				typeParameterList.Add(typeParameter);
			}
			while (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) {
				if (!this.Match(CSharpTokenID.Comma))
					return false;
				if (!this.MatchVariantTypeParameter(out typeParameter))
					return false;
				else {
					typeParameterList.Add(typeParameter);
				}
			}
			this.Match(CSharpTokenID.GreaterThan);
			return true;
		}

		/// <summary>
		/// Matches a <c>VariantTypeParameter</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>VariantTypeParameter</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>OpenSquareBrace</c>, <c>Out</c>, <c>In</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>.
		/// </remarks>
		protected virtual bool MatchVariantTypeParameter(out TypeReference typeParameter) {
			typeParameter = null;
			AttributeSection attributeSection;
			AstNodeList attributeSections = new AstNodeList(null);
			while (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenSquareBrace)) {
				if (!this.MatchAttributeSection(out attributeSection))
					return false;
				else {
					attributeSections.Add(attributeSection);
				}
			}
			if (((this.TokenIs(this.LookAheadToken, CSharpTokenID.Out)) || (this.TokenIs(this.LookAheadToken, CSharpTokenID.In)))) {
				if (this.TokenIs(this.LookAheadToken, CSharpTokenID.In)) {
					if (!this.Match(CSharpTokenID.In))
						return false;
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Out)) {
					if (!this.Match(CSharpTokenID.Out))
						return false;
				}
				else
					return false;
			}
			if (!this.MatchSimpleIdentifier())
				return false;
			typeParameter = new TypeReference(this.TokenText, this.Token.TextRange);
			typeParameter.IsGenericParameter = true;
			if (attributeSections != null)
				typeParameter.AttributeSections.AddRange(attributeSections.ToArray());
			return true;
		}

		/// <summary>
		/// Matches a <c>TypeArgumentList</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>TypeArgumentList</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>LessThan</c>.
		/// </remarks>
		protected virtual bool MatchTypeArgumentList(out AstNodeList typeArgumentList) {
			TypeReference typeReference;
			typeArgumentList = new AstNodeList(null);
			if (!this.Match(CSharpTokenID.LessThan))
				return false;
			if (!this.MatchType(out typeReference))
				return false;
			else {
				typeArgumentList.Add(typeReference);
			}
			while (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) {
				if (!this.Match(CSharpTokenID.Comma))
					return false;
				if (!this.MatchType(out typeReference))
					return false;
				else {
					typeArgumentList.Add(typeReference);
				}
			}
			this.Match(CSharpTokenID.GreaterThan);
			return true;
		}

		/// <summary>
		/// Matches a <c>TypeParameterConstraintsClauses</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>TypeParameterConstraintsClauses</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>.
		/// </remarks>
		protected virtual bool MatchTypeParameterConstraintsClauses(AstNodeList typeParameterList) {
			while (this.IsInMultiMatchSet(17, this.LookAheadToken)) {
				if (!this.MatchSimpleIdentifier())
					return false;
				if (this.TokenText != "where") {
					this.ReportSyntaxError(AssemblyInfo.Instance.Resources.GetString("SemanticParserError_WhereExpected"));
					return false;
				}
				TypeReference typeParameterReference;
				if (!this.MatchTypeParameter(out typeParameterReference))
					return false;
				TypeReference matchingTypeParameter = null;
				if (typeParameterList != null) {
					foreach (TypeReference typeParameter in typeParameterList) {
						if (typeParameter.Name == typeParameterReference.Name) {
							matchingTypeParameter = typeParameter;
							break;
						}
					}
				}
				// Statements like this no longer work due to changes... need better identifier storage: static T NullableHelper_HACK<T>() where T : struct
				// if (matchingTypeParameter == null)
					//	this.ReportSyntaxError("No matching type parameter was found for the constraint clause.");
				if (!this.Match(CSharpTokenID.Colon))
					return false;
				if (!this.MatchTypeParameterConstraint(matchingTypeParameter))
					return false;
				while (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) {
					if (!this.Match(CSharpTokenID.Comma))
						return false;
					if (!this.MatchTypeParameterConstraint(matchingTypeParameter))
						return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>TypeParameterConstraint</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>TypeParameterConstraint</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Void</c>, <c>New</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>, <c>Class</c>, <c>Struct</c>.
		/// </remarks>
		protected virtual bool MatchTypeParameterConstraint(TypeReference typeParameter) {
			if (this.IsInMultiMatchSet(29, this.LookAheadToken)) {
				TypeReference typeConstraint;
				if (!this.MatchType(out typeConstraint))
					return false;
				else {
					if (typeParameter != null)
						typeParameter.GenericTypeParameterConstraints.Add(typeConstraint);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Class)) {
				if (!this.Match(CSharpTokenID.Class))
					return false;
				else {
					if (typeParameter != null)
						typeParameter.HasGenericParameterReferenceTypeConstraint = true;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Struct)) {
				if (!this.Match(CSharpTokenID.Struct))
					return false;
				else {
					if (typeParameter != null) {
						typeParameter.HasGenericParameterNotNullableValueTypeConstraint = true;
						typeParameter.GenericTypeParameterConstraints.Add(new TypeReference("System.ValueType", this.Token.TextRange));
					}
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.New)) {
				if (!this.Match(CSharpTokenID.New))
					return false;
				else {
					if (typeParameter != null)
						typeParameter.HasGenericParameterDefaultConstructorConstraint = true;
				}
				if (!this.Match(CSharpTokenID.OpenParenthesis))
					return false;
				if (!this.Match(CSharpTokenID.CloseParenthesis))
					return false;
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>AnonymousMethodExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>AnonymousMethodExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Delegate</c>.
		/// </remarks>
		protected virtual bool MatchAnonymousMethodExpression(out Expression expression) {
			expression = null;
			Statement statement;
			int startOffset = this.LookAheadToken.StartOffset;
			AstNodeList anonymousMethodParameterList = new AstNodeList(null);
			ParameterDeclaration parameterDeclaration;
			if (!this.Match(CSharpTokenID.Delegate))
				return false;
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenParenthesis)) {
				if (!this.Match(CSharpTokenID.OpenParenthesis))
					return false;
				if (this.IsInMultiMatchSet(35, this.LookAheadToken)) {
					if (!this.MatchFixedParameter(null, null, out parameterDeclaration))
						return false;
					else {
						anonymousMethodParameterList.Add(parameterDeclaration);
					}
					while (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) {
						if (!this.Match(CSharpTokenID.Comma))
							return false;
						if (!this.MatchFixedParameter(null, null, out parameterDeclaration))
							return false;
						else {
							anonymousMethodParameterList.Add(parameterDeclaration);
						}
					}
				}
				if (!this.Match(CSharpTokenID.CloseParenthesis))
					return false;
			}
			if (!this.MatchBlock(out statement))
				return false;
			expression = new AnonymousMethodExpression((BlockStatement)statement, new TextRange(startOffset, this.Token.EndOffset));
			((AnonymousMethodExpression)expression).GenericTypeParameters.AddRange(anonymousMethodParameterList.ToArray());
			return true;
		}

		/// <summary>
		/// Matches a <c>FixedSizeBufferDeclaration</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>FixedSizeBufferDeclaration</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Fixed</c>.
		/// </remarks>
		protected virtual bool MatchFixedSizeBufferDeclaration(int startOffset, AstNodeList attributeSections, Modifiers modifiers) {
			// NOTE: Attributes and FixedSizeBufferModifier (alias Modifier) moved to callers of FixedSizeBufferDeclaration to reduce ambiguity
			if (!this.Match(CSharpTokenID.Fixed))
				return false;
			TypeReference typeReference;
			if (!this.MatchType(out typeReference))
				return false;
			FixedSizeBufferDeclaration fixedSizeBufferDeclaration = new FixedSizeBufferDeclaration(modifiers);
			fixedSizeBufferDeclaration.Documentation = this.ReapDocumentationComments();
			fixedSizeBufferDeclaration.StartOffset = startOffset;
			fixedSizeBufferDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
			if (!this.MatchFixedSizeBufferDeclarator(fixedSizeBufferDeclaration, typeReference))
				return false;
			while (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) {
				if (!this.Match(CSharpTokenID.Comma))
					return false;
				if (!this.MatchFixedSizeBufferDeclarator(fixedSizeBufferDeclaration, typeReference))
					return false;
			}
			this.Match(CSharpTokenID.SemiColon);
			fixedSizeBufferDeclaration.EndOffset = this.Token.EndOffset;
			this.BlockAddChild(fixedSizeBufferDeclaration);
			this.ReapDocumentationComments();
			return true;
		}

		/// <summary>
		/// Matches a <c>FixedSizeBufferDeclarator</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>FixedSizeBufferDeclarator</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>.
		/// </remarks>
		protected virtual bool MatchFixedSizeBufferDeclarator(FixedSizeBufferDeclaration declaration, TypeReference typeReference) {
			int startOffset = this.LookAheadToken.StartOffset;
			QualifiedIdentifier identifier;
			Expression sizeExpression;
			if (!this.MatchIdentifier(out identifier))
				return false;
			if (!this.Match(CSharpTokenID.OpenSquareBrace))
				return false;
			if (!this.MatchExpression(out sizeExpression))
				return false;
			if (!this.Match(CSharpTokenID.CloseSquareBrace))
				return false;
			FixedSizeBufferDeclarator declarator = new FixedSizeBufferDeclarator(typeReference, identifier, new TextRange(startOffset, this.Token.EndOffset));
			declarator.SizeExpression = sizeExpression;
			declaration.Variables.Add(declarator);
			return true;
		}

		/// <summary>
		/// Matches a <c>LambdaExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>LambdaExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: this.IsLambdaExpression().
		/// </remarks>
		protected virtual bool MatchLambdaExpression(out Expression expression) {
			expression = null;
			AstNodeList parameterList = null;
			int startOffset = this.LookAheadToken.StartOffset;
			if (this.IsInMultiMatchSet(3, this.LookAheadToken)) {
				parameterList = new AstNodeList(null);
				ParameterDeclaration parameter;
				if (!this.MatchImplicitlyTypedLambdaParameter(out parameter))
					return false;
				else {
					parameterList.Add(parameter);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenParenthesis)) {
				if (!this.Match(CSharpTokenID.OpenParenthesis))
					return false;
				if ((this.IsInMultiMatchSet(35, this.LookAheadToken) || (this.IsImplicitlyTypedLambdaParameterList()))) {
					if (((this.IsImplicitlyTypedLambdaParameterList()))) {
						if (!this.MatchImplicitlyTypedLambdaParameterList(out parameterList))
							return false;
					}
					else if (this.IsInMultiMatchSet(25, this.LookAheadToken)) {
						if (!this.MatchExplicitlyTypedLambdaParameterList(out parameterList))
							return false;
					}
					else
						return false;
				}
				if (!this.Match(CSharpTokenID.CloseParenthesis))
					return false;
			}
			else
				return false;
			if (!this.Match(CSharpTokenID.Lambda))
				return false;
			Statement statement = null;
			if ((this.IsInMultiMatchSet(0, this.LookAheadToken) || (this.IsLambdaExpression()) || (this.IsQueryExpression()) || (this.IsDefaultValueExpression()))) {
				this.MatchStatementExpression(out statement);
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenCurlyBrace)) {
				this.MatchBlock(out statement);
			}
			else
				return false;
			expression = new LambdaExpression(statement, new TextRange(startOffset, this.Token.EndOffset));
			if (parameterList != null)
				((LambdaExpression)expression).Parameters.AddRange(parameterList.ToArray());
			return true;
		}

		/// <summary>
		/// Matches a <c>ExplicitlyTypedLambdaParameterList</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ExplicitlyTypedLambdaParameterList</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Void</c>, <c>Ref</c>, <c>Out</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>.
		/// </remarks>
		protected virtual bool MatchExplicitlyTypedLambdaParameterList(out AstNodeList parameterList) {
			parameterList = new AstNodeList(null);
			ParameterDeclaration parameter;
			if (!this.MatchExplicitlyTypedLambdaParameter(out parameter))
				return false;
			else {
				parameterList.Add(parameter);
			}
			while (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) {
				if (!this.Match(CSharpTokenID.Comma))
					return false;
				if (!this.MatchExplicitlyTypedLambdaParameter(out parameter))
					return false;
				else {
					parameterList.Add(parameter);
				}
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>ExplicitlyTypedLambdaParameter</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ExplicitlyTypedLambdaParameter</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Void</c>, <c>Ref</c>, <c>Out</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>.
		/// </remarks>
		protected virtual bool MatchExplicitlyTypedLambdaParameter(out ParameterDeclaration parameter) {
			parameter = null;
			ParameterModifiers modifiers = ParameterModifiers.None;
			TypeReference typeReference;
			int startOffset = this.LookAheadToken.StartOffset;
			if (((this.TokenIs(this.LookAheadToken, CSharpTokenID.Ref)) || (this.TokenIs(this.LookAheadToken, CSharpTokenID.Out)))) {
				if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Ref)) {
					if (!this.Match(CSharpTokenID.Ref))
						return false;
					else {
						modifiers = ParameterModifiers.Ref;
					}
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Out)) {
					if (!this.Match(CSharpTokenID.Out))
						return false;
					else {
						modifiers = ParameterModifiers.Out;
					}
				}
				else
					return false;
			}
			if (!this.MatchType(out typeReference))
				return false;
			if (!this.MatchSimpleIdentifier())
				return false;
			parameter = new ParameterDeclaration(modifiers, this.TokenText);
			parameter.StartOffset = startOffset;
			parameter.EndOffset = this.Token.EndOffset;
			parameter.ParameterType = typeReference;
			return true;
		}

		/// <summary>
		/// Matches a <c>ImplicitlyTypedLambdaParameterList</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ImplicitlyTypedLambdaParameterList</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: this.IsImplicitlyTypedLambdaParameterList().
		/// </remarks>
		protected virtual bool MatchImplicitlyTypedLambdaParameterList(out AstNodeList parameterList) {
			parameterList = new AstNodeList(null);
			ParameterDeclaration parameter;
			if (!this.MatchImplicitlyTypedLambdaParameter(out parameter))
				return false;
			else {
				parameterList.Add(parameter);
			}
			while (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) {
				if (!this.Match(CSharpTokenID.Comma))
					return false;
				if (!this.MatchImplicitlyTypedLambdaParameter(out parameter))
					return false;
				else {
					parameterList.Add(parameter);
				}
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>ImplicitlyTypedLambdaParameter</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ImplicitlyTypedLambdaParameter</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>.
		/// </remarks>
		protected virtual bool MatchImplicitlyTypedLambdaParameter(out ParameterDeclaration parameter) {
			this.MatchSimpleIdentifier();
			parameter = new ParameterDeclaration(ParameterModifiers.None, this.TokenText);
			parameter.StartOffset = this.Token.StartOffset;
			parameter.EndOffset = this.Token.EndOffset;
			return true;
		}

		/// <summary>
		/// Matches a <c>ObjectOrCollectionInitializer</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ObjectOrCollectionInitializer</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>OpenCurlyBrace</c>.
		/// </remarks>
		protected virtual bool MatchObjectOrCollectionInitializer(out Expression expression) {
			expression = null;
			AstNodeList initializerList = new AstNodeList(null);
			Expression initializer;
			int startOffset = this.LookAheadToken.StartOffset;
			if (!this.Match(CSharpTokenID.OpenCurlyBrace))
				return false;
			if (this.IsObjectInitializer()) {
				if (this.IsInMultiMatchSet(17, this.LookAheadToken)) {
					if (!this.MatchMemberInitializer(out initializer))
						return false;
					else {
						initializerList.Add(initializer);
					}
					while ((this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) && (!this.TokenIs(this.GetLookAheadToken(2), CSharpTokenID.CloseCurlyBrace))) {
						this.Match(CSharpTokenID.Comma);
						if (!this.MatchMemberInitializer(out initializer))
							return false;
						else {
							initializerList.Add(initializer);
						}
					}
					if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) {
						this.Match(CSharpTokenID.Comma);
					}
				}
			}
			else {
				if (!this.MatchElementInitializer(out initializer))
					return false;
				else {
					initializerList.Add(initializer);
				}
				while ((this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) && (!this.TokenIs(this.GetLookAheadToken(2), CSharpTokenID.CloseCurlyBrace))) {
					this.Match(CSharpTokenID.Comma);
					if (!this.MatchElementInitializer(out initializer))
						return false;
					else {
						initializerList.Add(initializer);
					}
				}
				if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) {
					this.Match(CSharpTokenID.Comma);
				}
			}
			this.Match(CSharpTokenID.CloseCurlyBrace);
			expression = new ObjectCollectionInitializerExpression(new TextRange(startOffset, this.Token.EndOffset));
			((ObjectCollectionInitializerExpression)expression).Initializers.AddRange(initializerList.ToArray());
			return true;
		}

		/// <summary>
		/// Matches a <c>MemberInitializer</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>MemberInitializer</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>.
		/// </remarks>
		protected virtual bool MatchMemberInitializer(out Expression expression) {
			expression = null;
			SimpleName memberName;
			Expression valueExpression;
			int startOffset = this.LookAheadToken.StartOffset;
			if (!this.MatchSimpleIdentifier())
				return false;
			else {
				memberName = new SimpleName(this.TokenText, this.Token.TextRange);
			}
			if (!this.Match(CSharpTokenID.Assignment))
				return false;
			if ((this.IsInMultiMatchSet(0, this.LookAheadToken) || (this.IsLambdaExpression()) || (this.IsQueryExpression()) || (this.IsDefaultValueExpression()))) {
				if (!this.MatchExpression(out valueExpression))
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenCurlyBrace)) {
				if (!this.MatchObjectOrCollectionInitializer(out valueExpression))
					return false;
			}
			else
				return false;
			expression = new AssignmentExpression(OperatorType.None, memberName, valueExpression, new TextRange(startOffset, this.Token.EndOffset));
			return true;
		}

		/// <summary>
		/// Matches a <c>ElementInitializer</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ElementInitializer</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Multiplication</c>, <c>True</c>, <c>False</c>, <c>DecimalIntegerLiteral</c>, <c>HexadecimalIntegerLiteral</c>, <c>RealLiteral</c>, <c>CharacterLiteral</c>, <c>StringLiteral</c>, <c>VerbatimStringLiteral</c>, <c>Null</c>, <c>OpenParenthesis</c>, <c>This</c>, <c>Base</c>, <c>New</c>, <c>TypeOf</c>, <c>SizeOf</c>, <c>Checked</c>, <c>Unchecked</c>, <c>Increment</c>, <c>Decrement</c>, <c>Addition</c>, <c>Subtraction</c>, <c>Negation</c>, <c>OnesComplement</c>, <c>BitwiseAnd</c>, <c>OpenCurlyBrace</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>, <c>Delegate</c>.
		/// The non-terminal can start with: this.IsLambdaExpression().
		/// The non-terminal can start with: this.IsQueryExpression().
		/// The non-terminal can start with: this.IsDefaultValueExpression().
		/// </remarks>
		protected virtual bool MatchElementInitializer(out Expression expression) {
			expression = null;
			if ((this.IsInMultiMatchSet(0, this.LookAheadToken) || (this.IsLambdaExpression()) || (this.IsQueryExpression()) || (this.IsDefaultValueExpression()))) {
				if (!this.MatchNonAssignmentExpression(out expression))
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OpenCurlyBrace)) {
				expression = new ObjectCollectionInitializerExpression();
				expression.StartOffset = this.LookAheadToken.StartOffset;
				Expression initializerExpression;
				if (!this.Match(CSharpTokenID.OpenCurlyBrace))
					return false;
				if (!this.MatchExpression(out initializerExpression))
					return false;
				else {
					((ObjectCollectionInitializerExpression)expression).Initializers.Add(initializerExpression);
				}
				while (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) {
					if (!this.Match(CSharpTokenID.Comma))
						return false;
					if (!this.MatchExpression(out initializerExpression))
						return false;
					else {
						((ObjectCollectionInitializerExpression)expression).Initializers.Add(initializerExpression);
					}
				}
				this.Match(CSharpTokenID.CloseCurlyBrace);
				expression.EndOffset = this.Token.EndOffset;
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>AnonymousObjectCreationExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>AnonymousObjectCreationExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>New</c>.
		/// </remarks>
		protected virtual bool MatchAnonymousObjectCreationExpression(out Expression expression) {
			expression = null;
			ObjectCollectionInitializerExpression initializerExpression;
			int startOffset = this.LookAheadToken.StartOffset;
			TextRange newTextRange = this.LookAheadToken.TextRange;
			if (!this.Match(CSharpTokenID.New))
				return false;
			if (!this.MatchAnonymousObjectInitializer(out initializerExpression))
				return false;
			expression = new ObjectCreationExpression(new TypeReference(TypeReference.AnonymousTypeName, newTextRange), new TextRange(startOffset, this.Token.EndOffset));
			((ObjectCreationExpression)expression).Initializer = initializerExpression;
			return true;
		}

		/// <summary>
		/// Matches a <c>AnonymousObjectInitializer</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>AnonymousObjectInitializer</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>OpenCurlyBrace</c>.
		/// </remarks>
		protected virtual bool MatchAnonymousObjectInitializer(out ObjectCollectionInitializerExpression expression) {
			expression = null;
			AstNodeList initializerList = new AstNodeList(null);
			Expression initializer;
			int startOffset = this.LookAheadToken.StartOffset;
			if (!this.Match(CSharpTokenID.OpenCurlyBrace))
				return false;
			if ((this.IsInMultiMatchSet(14, this.LookAheadToken) || (this.IsLambdaExpression()) || (this.IsQueryExpression()) || (this.IsDefaultValueExpression()))) {
				if (!this.MatchMemberDeclarator(out initializer))
					return false;
				else {
					initializerList.Add(initializer);
				}
				while ((this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) && (!this.TokenIs(this.GetLookAheadToken(2), CSharpTokenID.CloseCurlyBrace))) {
					this.Match(CSharpTokenID.Comma);
					if (!this.MatchMemberDeclarator(out initializer))
						return false;
					else {
						initializerList.Add(initializer);
					}
				}
				if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) {
					this.Match(CSharpTokenID.Comma);
				}
			}
			this.Match(CSharpTokenID.CloseCurlyBrace);
			expression = new ObjectCollectionInitializerExpression(new TextRange(startOffset, this.Token.EndOffset));
			((ObjectCollectionInitializerExpression)expression).Initializers.AddRange(initializerList.ToArray());
			return true;
		}

		/// <summary>
		/// Matches a <c>MemberDeclarator</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>MemberDeclarator</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Multiplication</c>, <c>True</c>, <c>False</c>, <c>DecimalIntegerLiteral</c>, <c>HexadecimalIntegerLiteral</c>, <c>RealLiteral</c>, <c>CharacterLiteral</c>, <c>StringLiteral</c>, <c>VerbatimStringLiteral</c>, <c>Null</c>, <c>OpenParenthesis</c>, <c>This</c>, <c>Base</c>, <c>New</c>, <c>TypeOf</c>, <c>SizeOf</c>, <c>Checked</c>, <c>Unchecked</c>, <c>Increment</c>, <c>Decrement</c>, <c>Addition</c>, <c>Subtraction</c>, <c>Negation</c>, <c>OnesComplement</c>, <c>BitwiseAnd</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>, <c>Delegate</c>.
		/// The non-terminal can start with: this.IsLambdaExpression().
		/// The non-terminal can start with: this.IsQueryExpression().
		/// The non-terminal can start with: this.IsDefaultValueExpression().
		/// </remarks>
		protected virtual bool MatchMemberDeclarator(out Expression expression) {
			expression = null;
			Expression primaryExpression;
			if (!this.MatchExpression(out primaryExpression))
				return false;
			if (primaryExpression is SimpleName) {
				// identifer => identifer = identifier
				expression = new AssignmentExpression(OperatorType.None, (SimpleName)primaryExpression, primaryExpression, primaryExpression.TextRange);
			}
			else if (primaryExpression is MemberAccess) {
				// expr . identifier => identifier = expr . identifier
				expression = new AssignmentExpression(OperatorType.None,
				new SimpleName(((MemberAccess)primaryExpression).MemberName.Text, ((MemberAccess)primaryExpression).MemberName.TextRange),
				primaryExpression, primaryExpression.TextRange);
			}
			else if (primaryExpression is AssignmentExpression) {
				// identifier = expression
				if (!(((AssignmentExpression)primaryExpression).LeftExpression is SimpleName)) {
					this.ReportSyntaxError(this.Token.TextRange, AssemblyInfo.Instance.Resources.GetString("SemanticParserError_AnonymousTypeMemberDeclaratorAssignment"));
					return false;
				}
				expression = primaryExpression;
			}
			else {
				this.ReportSyntaxError(this.Token.TextRange, AssemblyInfo.Instance.Resources.GetString("SemanticParserError_AnonymousTypeMemberDeclaratorSyntax"));
				return false;
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>ImplicitlyTypedArrayCreationExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ImplicitlyTypedArrayCreationExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>New</c>.
		/// </remarks>
		protected virtual bool MatchImplicitlyTypedArrayCreationExpression(out Expression expression) {
			expression = null;
			Expression initializer;
			int startOffset = this.LookAheadToken.StartOffset;
			if (!this.Match(CSharpTokenID.New))
				return false;
			if (!this.Match(CSharpTokenID.OpenSquareBrace))
				return false;
			if (!this.Match(CSharpTokenID.CloseSquareBrace))
				return false;
			if (!this.MatchArrayInitializer(out initializer))
				return false;
			// Determine the type
			TypeReference typeReference = null;
			TypeReference currentTypeReference = null;
			foreach (Expression varInitializer in ((ObjectCollectionInitializerExpression)initializer).Initializers) {
				currentTypeReference = this.GetImplicitType(varInitializer, true);
				if (typeReference == null)
					typeReference = currentTypeReference;
				else if (currentTypeReference != null) {
					if ((typeReference.Name == "System.Int32") && (currentTypeReference.Name == "System.Double")) {
						// Implicitly cast
						typeReference = currentTypeReference;
					}
				}
			}
			if (typeReference == null) {
				this.ReportSyntaxError(this.Token.TextRange, AssemblyInfo.Instance.Resources.GetString("SemanticParserError_ImplicitlyTypedArrayNullInitializer"));
				return false;
			}
			
			expression = new ObjectCreationExpression(typeReference);
			((ObjectCreationExpression)expression).IsArray = true;
			((ObjectCreationExpression)expression).IsImplicitlyTyped = true;
			expression.StartOffset = startOffset;
			expression.EndOffset = this.Token.EndOffset;
			((ObjectCreationExpression)expression).Initializer = (ObjectCollectionInitializerExpression)initializer;
			return true;
		}

		/// <summary>
		/// Matches a <c>QueryExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>QueryExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: this.IsQueryExpression().
		/// </remarks>
		protected virtual bool MatchQueryExpression(out Expression expression) {
			expression = new QueryExpression();
			expression.StartOffset = this.LookAheadToken.StartOffset;
			
			AstNode queryOperator;
			if (!this.MatchFromClause(out queryOperator))
				return false;
			else {
				((QueryExpression)expression).QueryOperators.Add(queryOperator);
			}
			if (!this.MatchQueryBody(((QueryExpression)expression).QueryOperators))
				return false;
			expression.EndOffset = this.Token.EndOffset;
			return true;
		}

		/// <summary>
		/// Matches a <c>CollectionRangeVariableDeclaration</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>CollectionRangeVariableDeclaration</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Void</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>.
		/// </remarks>
		protected virtual bool MatchCollectionRangeVariableDeclaration(out CollectionRangeVariableDeclaration variableDeclaration) {
			variableDeclaration = new CollectionRangeVariableDeclaration();
			variableDeclaration.StartOffset = this.LookAheadToken.StartOffset;
			
			TypeReference typeReference = new TypeReference(TypeReference.AnonymousTypeName, new TextRange(this.LookAheadToken.StartOffset));
			QualifiedIdentifier variableName;
			Expression source;
			if ((!this.AreNextTwoIdentifierAnd(CSharpTokenID.In)) && (this.IsInMultiMatchSet(29, this.LookAheadToken))) {
				if (!this.MatchType(out typeReference))
					return false;
			}
			if (!this.MatchIdentifier(out variableName))
				return false;
			else {
				variableDeclaration.VariableDeclarator = new VariableDeclarator(typeReference, variableName, false, true);
			}
			if (!this.Match(CSharpTokenID.In))
				return false;
			if (!this.MatchExpression(out source))
				return false;
			else {
				variableDeclaration.Source = source;
			}
			variableDeclaration.EndOffset = this.Token.EndOffset;
			return true;
		}

		/// <summary>
		/// Matches a <c>FromClause</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>FromClause</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>From</c>.
		/// </remarks>
		protected virtual bool MatchFromClause(out AstNode fromQueryOperator) {
			fromQueryOperator = new FromQueryOperator();
			fromQueryOperator.StartOffset = this.LookAheadToken.StartOffset;
			
			CollectionRangeVariableDeclaration variableDeclaration = null;
			if (!this.Match(CSharpTokenID.From))
				return false;
			if (!this.MatchCollectionRangeVariableDeclaration(out variableDeclaration))
				return false;
			else {
				((FromQueryOperator)fromQueryOperator).CollectionRangeVariableDeclarations.Add(variableDeclaration);
			}
			fromQueryOperator.EndOffset = this.Token.EndOffset;
			return true;
		}

		/// <summary>
		/// Matches a <c>QueryBody</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>QueryBody</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>From</c>, <c>Group</c>, <c>Join</c>, <c>Let</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>.
		/// </remarks>
		protected virtual bool MatchQueryBody(IAstNodeList queryOperators) {
			AstNode queryOperator;
			bool hasQueryContinuation = false;
			// QueryBodyClauses
			while (this.IsInMultiMatchSet(36, this.LookAheadToken)) {
				if (this.TokenIs(this.LookAheadToken, CSharpTokenID.From)) {
					if (!this.MatchFromClause(out queryOperator))
						return false;
					else {
						queryOperators.Add(queryOperator);
					}
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Let)) {
					if (!this.MatchLetClause(out queryOperator))
						return false;
					else {
						queryOperators.Add(queryOperator);
					}
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Where)) {
					if (!this.MatchWhereClause(out queryOperator))
						return false;
					else {
						queryOperators.Add(queryOperator);
					}
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Join)) {
					if (!this.MatchJoinClause(out queryOperator))
						return false;
					else {
						queryOperators.Add(queryOperator);
					}
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.OrderBy)) {
					if (!this.MatchOrderByClause(out queryOperator))
						return false;
					else {
						queryOperators.Add(queryOperator);
					}
				}
				else
					return false;
			}
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Select)) {
				if (!this.MatchSelectClause(out queryOperator))
					return false;
				else {
					queryOperators.Add(queryOperator);
					if (((SelectQueryOperator)queryOperator).VariableDeclarators.Count > 0) {
						VariableDeclarator variableDeclarator = (VariableDeclarator)((SelectQueryOperator)queryOperator).VariableDeclarators[0];
						hasQueryContinuation = (variableDeclarator.Name != null);
					}
				}
			}
			else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Group)) {
				if (!this.MatchGroupClause(out queryOperator))
					return false;
				else {
					queryOperators.Add(queryOperator);
					hasQueryContinuation = (((GroupQueryOperator)queryOperator).TargetExpressions.Count > 0);
				}
			}
			else
				return false;
			if (hasQueryContinuation) {
				if (!this.MatchQueryBody(queryOperators))
					return false;
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>LetClause</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>LetClause</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Let</c>.
		/// </remarks>
		protected virtual bool MatchLetClause(out AstNode letQueryOperator) {
			letQueryOperator = new LetQueryOperator();
			letQueryOperator.StartOffset = this.LookAheadToken.StartOffset;
			
			QualifiedIdentifier variableName;
			VariableDeclarator variableDeclarator;
			Expression initializer;
			if (!this.Match(CSharpTokenID.Let))
				return false;
			TypeReference typeReference = new TypeReference(TypeReference.AnonymousTypeName, new TextRange(this.LookAheadToken.StartOffset));
			if (!this.MatchIdentifier(out variableName))
				return false;
			else {
				variableDeclarator = new VariableDeclarator(typeReference, variableName, false, true);
			}
			if (!this.Match(CSharpTokenID.Assignment))
				return false;
			if (!this.MatchExpression(out initializer))
				return false;
			else {
				variableDeclarator.Initializer = initializer;
			}
			((LetQueryOperator)letQueryOperator).VariableDeclarators.Add(variableDeclarator);
			letQueryOperator.EndOffset = this.Token.EndOffset;
			return true;
		}

		/// <summary>
		/// Matches a <c>WhereClause</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>WhereClause</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Where</c>.
		/// </remarks>
		protected virtual bool MatchWhereClause(out AstNode whereQueryOperator) {
			whereQueryOperator = new WhereQueryOperator();
			whereQueryOperator.StartOffset = this.LookAheadToken.StartOffset;
			
			Expression condition;
			if (!this.Match(CSharpTokenID.Where))
				return false;
			if (!this.MatchExpression(out condition))
				return false;
			else {
				((WhereQueryOperator)whereQueryOperator).Condition = condition;
			}
			whereQueryOperator.EndOffset = this.Token.EndOffset;
			return true;
		}

		/// <summary>
		/// Matches a <c>JoinClause</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>JoinClause</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Join</c>.
		/// </remarks>
		protected virtual bool MatchJoinClause(out AstNode joinQueryOperator) {
			joinQueryOperator = new JoinQueryOperator();
			joinQueryOperator.StartOffset = this.LookAheadToken.StartOffset;
			
			CollectionRangeVariableDeclaration variableDeclaration = null;
			Expression expression;
			JoinCondition condition = new JoinCondition();
			QualifiedIdentifier variableName;
			if (!this.Match(CSharpTokenID.Join))
				return false;
			if (!this.MatchCollectionRangeVariableDeclaration(out variableDeclaration))
				return false;
			else {
				((JoinQueryOperator)joinQueryOperator).CollectionRangeVariableDeclaration = variableDeclaration;
			}
			if (!this.Match(CSharpTokenID.On))
				return false;
			condition.StartOffset = this.LookAheadToken.StartOffset;
			if (!this.MatchExpression(out expression))
				return false;
			else {
				condition.LeftConditionExpression = expression;
			}
			if (!this.Match(CSharpTokenID.Equals))
				return false;
			if (!this.MatchExpression(out expression))
				return false;
			else {
				condition.RightConditionExpression = expression;
			}
			condition.EndOffset = this.Token.EndOffset;
			((JoinQueryOperator)joinQueryOperator).Conditions.Add(condition);
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Into)) {
				if (!this.Match(CSharpTokenID.Into))
					return false;
				if (!this.MatchIdentifier(out variableName))
					return false;
				else {
					((JoinQueryOperator)joinQueryOperator).TargetExpressions.Add(new SimpleName(variableName.Text, variableName.TextRange));
				}
			}
			joinQueryOperator.EndOffset = this.Token.EndOffset;
			return true;
		}

		/// <summary>
		/// Matches a <c>OrderByClause</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>OrderByClause</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>OrderBy</c>.
		/// </remarks>
		protected virtual bool MatchOrderByClause(out AstNode orderByQueryOperator) {
			orderByQueryOperator = new OrderByQueryOperator();
			orderByQueryOperator.StartOffset = this.LookAheadToken.StartOffset;
			
			Ordering ordering;
			if (!this.Match(CSharpTokenID.OrderBy))
				return false;
			if (!this.MatchOrdering(out ordering))
				return false;
			else {
				((OrderByQueryOperator)orderByQueryOperator).Orderings.Add(ordering);
			}
			while (this.TokenIs(this.LookAheadToken, CSharpTokenID.Comma)) {
				if (!this.Match(CSharpTokenID.Comma))
					return false;
				if (!this.MatchOrdering(out ordering))
					return false;
				else {
					((OrderByQueryOperator)orderByQueryOperator).Orderings.Add(ordering);
				}
			}
			orderByQueryOperator.EndOffset = this.Token.EndOffset;
			return true;
		}

		/// <summary>
		/// Matches a <c>Ordering</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>Ordering</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Bool</c>, <c>Decimal</c>, <c>SByte</c>, <c>Byte</c>, <c>Short</c>, <c>UShort</c>, <c>Int</c>, <c>UInt</c>, <c>Long</c>, <c>ULong</c>, <c>Char</c>, <c>Float</c>, <c>Double</c>, <c>Object</c>, <c>String</c>, <c>Dynamic</c>, <c>Multiplication</c>, <c>True</c>, <c>False</c>, <c>DecimalIntegerLiteral</c>, <c>HexadecimalIntegerLiteral</c>, <c>RealLiteral</c>, <c>CharacterLiteral</c>, <c>StringLiteral</c>, <c>VerbatimStringLiteral</c>, <c>Null</c>, <c>OpenParenthesis</c>, <c>This</c>, <c>Base</c>, <c>New</c>, <c>TypeOf</c>, <c>SizeOf</c>, <c>Checked</c>, <c>Unchecked</c>, <c>Increment</c>, <c>Decrement</c>, <c>Addition</c>, <c>Subtraction</c>, <c>Negation</c>, <c>OnesComplement</c>, <c>BitwiseAnd</c>, <c>Identifier</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Let</c>, <c>On</c>, <c>OrderBy</c>, <c>Select</c>, <c>Where</c>, <c>Var</c>, <c>Delegate</c>.
		/// The non-terminal can start with: this.IsLambdaExpression().
		/// The non-terminal can start with: this.IsQueryExpression().
		/// The non-terminal can start with: this.IsDefaultValueExpression().
		/// </remarks>
		protected virtual bool MatchOrdering(out Ordering ordering) {
			ordering = new Ordering();
			ordering.StartOffset = this.LookAheadToken.StartOffset;
			
			Expression expression;
			if (!this.MatchExpression(out expression))
				return false;
			else {
				ordering.Expression = expression;
			}
			if (((this.TokenIs(this.LookAheadToken, CSharpTokenID.Ascending)) || (this.TokenIs(this.LookAheadToken, CSharpTokenID.Descending)))) {
				if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Ascending)) {
					if (!this.Match(CSharpTokenID.Ascending))
						return false;
					else {
						ordering.Direction = OrderingDirection.Ascending;
					}
				}
				else if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Descending)) {
					if (!this.Match(CSharpTokenID.Descending))
						return false;
					else {
						ordering.Direction = OrderingDirection.Descending;
					}
				}
				else
					return false;
			}
			ordering.EndOffset = this.Token.EndOffset;
			return true;
		}

		/// <summary>
		/// Matches a <c>SelectClause</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>SelectClause</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Select</c>.
		/// </remarks>
		protected virtual bool MatchSelectClause(out AstNode selectQueryOperator) {
			selectQueryOperator = new SelectQueryOperator();
			selectQueryOperator.StartOffset = this.LookAheadToken.StartOffset;
			
			Expression expression;
			QualifiedIdentifier variableName;
			if (!this.Match(CSharpTokenID.Select))
				return false;
			TypeReference typeReference = new TypeReference(TypeReference.AnonymousTypeName, new TextRange(this.LookAheadToken.StartOffset));
			VariableDeclarator variableDeclarator = new VariableDeclarator(typeReference, null, false, true);
			if (!this.MatchExpression(out expression))
				return false;
			else {
				variableDeclarator.Initializer = expression;
				((SelectQueryOperator)selectQueryOperator).VariableDeclarators.Add(variableDeclarator);
			}
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Into)) {
				if (!this.Match(CSharpTokenID.Into))
					return false;
				if (!this.MatchIdentifier(out variableName))
					return false;
				else {
					variableDeclarator.Name = variableName; variableDeclarator.TextRange = variableName.TextRange;
				}
			}
			selectQueryOperator.EndOffset = this.Token.EndOffset;
			return true;
		}

		/// <summary>
		/// Matches a <c>GroupClause</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>GroupClause</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Group</c>.
		/// </remarks>
		protected virtual bool MatchGroupClause(out AstNode groupQueryOperator) {
			groupQueryOperator = new GroupQueryOperator();
			groupQueryOperator.StartOffset = this.LookAheadToken.StartOffset;
			
			Expression expression;
			if (!this.Match(CSharpTokenID.Group))
				return false;
			TypeReference typeReference = new TypeReference(TypeReference.AnonymousTypeName, new TextRange(this.LookAheadToken.StartOffset));
			VariableDeclarator variableDeclarator = new VariableDeclarator(typeReference, null, false, true);
			if (!this.MatchExpression(out expression))
				return false;
			else {
				variableDeclarator.Initializer = expression;
				((GroupQueryOperator)groupQueryOperator).Groupings.Add(variableDeclarator);
			}
			if (!this.Match(CSharpTokenID.By))
				return false;
			typeReference = new TypeReference(TypeReference.AnonymousTypeName, new TextRange(this.LookAheadToken.StartOffset));
			variableDeclarator = new VariableDeclarator(typeReference, null, false, true);
			if (!this.MatchExpression(out expression))
				return false;
			else {
				variableDeclarator.Initializer = expression;
				((GroupQueryOperator)groupQueryOperator).GroupBys.Add(variableDeclarator);
			}
			if (this.TokenIs(this.LookAheadToken, CSharpTokenID.Into)) {
				if (!this.Match(CSharpTokenID.Into))
					return false;
				typeReference = new TypeReference(TypeReference.AnonymousTypeName, new TextRange(this.LookAheadToken.StartOffset));
				variableDeclarator = new VariableDeclarator(typeReference, null, false, true);
				if (!this.MatchExpression(out expression))
					return false;
				else {
					variableDeclarator.Initializer = expression;
					((GroupQueryOperator)groupQueryOperator).TargetExpressions.Add(variableDeclarator);
				}
			}
			groupQueryOperator.EndOffset = this.Token.EndOffset;
			return true;
		}

		/// <summary>
		/// Gets the multi-match sets array.
		/// </summary>
		/// <value>The multi-match sets array.</value>
		protected override bool[,] MultiMatchSets {
			get {
				return multiMatchSets;
			}
		}

		private const bool Y = true;
		private const bool n = false;
		private static bool[,] multiMatchSets = {
			// 0: Bool Decimal SByte Byte Short UShort Int UInt Long ULong Char Float Double Object String Dynamic Multiplication True False DecimalIntegerLiteral HexadecimalIntegerLiteral RealLiteral CharacterLiteral StringLiteral VerbatimStringLiteral Null OpenParenthesis This Base New TypeOf SizeOf Checked Unchecked Increment Decrement Addition Subtraction Negation OnesComplement BitwiseAnd Identifier Ascending By Descending Equals From Group Into Join Let On OrderBy Select Where Var Delegate 
			{n,n,n,n,n,n,n,n,n,n,n,Y,Y,Y,Y,Y,Y,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,n,n,Y,Y,n,Y,n,n,Y,Y,n,n,n,Y,n,Y,n,Y,Y,n,n,n,n,n,Y,n,n,Y,n,n,n,n,n,n,n,Y,n,n,n,n,Y,n,Y,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,Y,Y,n,n,Y,n,n,Y,n,Y,n,Y,Y,Y,Y,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,Y,Y,Y,n,n,Y,n,n,Y,Y,n,n,n,n,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 1: Decimal SByte Byte Short UShort Int UInt Long ULong Char Float Double 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,Y,n,n,n,n,Y,n,n,n,Y,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,Y,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,Y,Y,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 2: SByte Byte Short UShort Int UInt Long ULong Char 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,Y,Y,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 3: Identifier Ascending By Descending Equals From Group Into Join Let On OrderBy Select Where Var 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 4: Object String Dynamic Identifier Ascending By Descending Equals From Group Into Join Let On OrderBy Select Where Var 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 5: Bool Decimal SByte Byte Short UShort Int UInt Long ULong Char Float Double 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,n,n,Y,n,n,n,n,Y,n,n,n,Y,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,Y,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,Y,Y,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 6: Bool Decimal SByte Byte Short UShort Int UInt Long ULong Char Float Double Object String Dynamic 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,n,n,Y,n,n,n,n,Y,n,n,n,Y,Y,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,Y,n,n,n,n,Y,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,Y,n,n,n,Y,n,n,n,n,n,n,n,Y,Y,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 7: Bool Decimal SByte Byte Short UShort Int UInt Long ULong Char Float Double Object String Dynamic Multiplication Ref Out True False DecimalIntegerLiteral HexadecimalIntegerLiteral RealLiteral CharacterLiteral StringLiteral VerbatimStringLiteral Null OpenParenthesis This Base New TypeOf SizeOf Checked Unchecked Increment Decrement Addition Subtraction Negation OnesComplement BitwiseAnd Identifier Ascending By Descending Equals From Group Into Join Let On OrderBy Select Where Var Delegate 
			{n,n,n,n,n,n,n,n,n,n,n,Y,Y,Y,Y,Y,Y,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,n,n,Y,Y,n,Y,n,n,Y,Y,n,n,n,Y,n,Y,n,Y,Y,n,n,n,n,n,Y,n,n,Y,n,n,n,n,n,n,n,Y,n,n,n,n,Y,n,Y,Y,Y,n,Y,n,n,n,n,n,n,n,Y,n,n,Y,n,n,Y,Y,n,n,Y,n,n,Y,n,Y,n,Y,Y,Y,Y,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,Y,Y,Y,n,n,Y,n,n,Y,Y,n,n,n,n,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 8: Dot OpenSquareBrace OpenParenthesis PointerDereference Increment Decrement 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 9: Bool Decimal SByte Byte Short UShort Int UInt Long ULong Char Float Double Object String Dynamic True False DecimalIntegerLiteral HexadecimalIntegerLiteral RealLiteral CharacterLiteral StringLiteral VerbatimStringLiteral Null OpenParenthesis This Base New TypeOf SizeOf Checked Unchecked Identifier Ascending By Descending Equals From Group Into Join Let On OrderBy Select Where Var Delegate 
			{n,n,n,n,n,n,n,n,n,n,n,Y,Y,Y,Y,Y,Y,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,n,n,Y,Y,n,Y,n,n,Y,Y,n,n,n,Y,n,Y,n,Y,Y,n,n,n,n,n,Y,n,n,Y,n,n,n,n,n,n,n,Y,n,n,n,n,Y,n,Y,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,Y,Y,n,n,Y,n,n,Y,n,Y,n,Y,Y,Y,Y,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 10: Assignment AdditionAssignment SubtractionAssignment MultiplicationAssignment DivisionAssignment ModulusAssignment BitwiseAndAssignment BitwiseOrAssignment ExclusiveOrAssignment LeftShiftAssignment 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 11: Bool Decimal SByte Byte Short UShort Int UInt Long ULong Char Float Double Object String Dynamic Multiplication True False DecimalIntegerLiteral HexadecimalIntegerLiteral RealLiteral CharacterLiteral StringLiteral VerbatimStringLiteral Null OpenParenthesis This Base New TypeOf SizeOf Checked Unchecked Increment Decrement Addition Subtraction Negation OnesComplement BitwiseAnd SemiColon If Switch OpenCurlyBrace While Do For ForEach Break Continue Goto Return Throw Try Lock Using Yield Unsafe Fixed Identifier Ascending By Descending Equals From Group Into Join Let On OrderBy Select Where Var Delegate 
			{n,n,n,n,n,n,n,n,n,n,n,Y,Y,Y,Y,Y,Y,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,n,n,Y,Y,Y,Y,n,n,Y,Y,n,n,Y,Y,n,Y,Y,Y,Y,n,n,n,n,n,Y,n,Y,Y,Y,Y,n,Y,Y,n,n,Y,n,n,n,Y,Y,n,Y,Y,Y,n,n,n,n,n,n,n,n,n,n,n,Y,Y,n,n,Y,Y,n,n,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,Y,Y,n,n,Y,n,n,n,Y,n,n,n,n,n,Y,Y,Y,Y,n,n,Y,n,n,Y,Y,n,n,n,n,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 12: Bool Decimal SByte Byte Short UShort Int UInt Long ULong Char Float Double Object String Dynamic Void Multiplication True False DecimalIntegerLiteral HexadecimalIntegerLiteral RealLiteral CharacterLiteral StringLiteral VerbatimStringLiteral Null OpenParenthesis This Base New TypeOf SizeOf Checked Unchecked Default Increment Decrement Addition Subtraction Negation OnesComplement BitwiseAnd SemiColon If Switch OpenCurlyBrace While Do For ForEach Break Continue Goto Case Return Throw Try Lock Using Yield Unsafe Fixed Const Identifier Ascending By Descending Equals From Group Into Join Let On OrderBy Select Where Var Delegate 
			{n,n,n,n,n,n,n,n,n,n,n,Y,Y,Y,Y,Y,Y,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,n,n,Y,Y,Y,Y,Y,n,Y,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,n,n,Y,n,Y,Y,Y,Y,n,Y,Y,n,n,Y,n,n,n,Y,Y,n,Y,Y,Y,n,n,n,n,n,n,n,n,n,n,n,Y,Y,n,n,Y,Y,n,n,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,Y,n,Y,Y,n,n,Y,n,n,n,Y,n,n,n,n,n,Y,Y,Y,Y,n,n,Y,n,n,Y,Y,n,n,n,n,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 13: Bool Decimal SByte Byte Short UShort Int UInt Long ULong Char Float Double Object String Dynamic Void Multiplication True False DecimalIntegerLiteral HexadecimalIntegerLiteral RealLiteral CharacterLiteral StringLiteral VerbatimStringLiteral Null OpenParenthesis This Base New TypeOf SizeOf Checked Unchecked Default Increment Decrement Addition Subtraction Negation OnesComplement BitwiseAnd SemiColon If Switch OpenCurlyBrace While Do For ForEach Break Continue Goto Case Return Throw Try Lock Using Yield Unsafe Fixed Const Identifier Ascending By Descending Equals From Group Into Join Let On OrderBy Select Where Var Delegate 
			{n,n,n,n,n,n,n,n,n,n,n,Y,Y,Y,Y,Y,Y,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,n,n,Y,Y,Y,Y,Y,n,Y,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,n,n,Y,n,Y,Y,Y,Y,n,Y,Y,n,n,Y,n,n,n,Y,Y,n,Y,Y,Y,n,n,n,n,n,n,n,n,n,n,n,Y,Y,n,n,Y,Y,n,n,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,Y,n,Y,Y,n,n,Y,n,n,n,Y,n,n,n,n,n,Y,Y,Y,Y,n,n,Y,n,n,Y,Y,n,n,n,n,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 14: Bool Decimal SByte Byte Short UShort Int UInt Long ULong Char Float Double Object String Dynamic Multiplication True False DecimalIntegerLiteral HexadecimalIntegerLiteral RealLiteral CharacterLiteral StringLiteral VerbatimStringLiteral Null OpenParenthesis This Base New TypeOf SizeOf Checked Unchecked Increment Decrement Addition Subtraction Negation OnesComplement BitwiseAnd Identifier Ascending By Descending Equals From Group Into Join Let On OrderBy Select Where Var Delegate 
			{n,n,n,n,n,n,n,n,n,n,n,Y,Y,Y,Y,Y,Y,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,n,n,Y,Y,n,Y,n,n,Y,Y,n,n,n,Y,n,Y,n,Y,Y,n,n,n,n,n,Y,n,n,Y,n,n,n,n,n,n,n,Y,n,n,n,n,Y,n,Y,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,Y,Y,n,n,Y,n,n,Y,n,Y,n,Y,Y,Y,Y,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,Y,Y,Y,n,n,Y,n,n,Y,Y,n,n,n,n,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 15: Bool Decimal SByte Byte Short UShort Int UInt Long ULong Char Float Double Object String Dynamic Void Multiplication True False DecimalIntegerLiteral HexadecimalIntegerLiteral RealLiteral CharacterLiteral StringLiteral VerbatimStringLiteral Null OpenParenthesis This Base New TypeOf SizeOf Checked Unchecked Increment Decrement Addition Subtraction Negation OnesComplement BitwiseAnd SemiColon If Switch OpenCurlyBrace While Do For ForEach Break Continue Goto Return Throw Try Lock Using Yield Unsafe Fixed Const Identifier Ascending By Descending Equals From Group Into Join Let On OrderBy Select Where Var Delegate 
			{n,n,n,n,n,n,n,n,n,n,n,Y,Y,Y,Y,Y,Y,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,n,n,Y,Y,Y,Y,n,n,Y,Y,n,Y,Y,Y,n,Y,Y,Y,Y,n,n,n,n,n,Y,n,Y,Y,Y,Y,n,Y,Y,n,n,Y,n,n,n,Y,Y,n,Y,Y,Y,n,n,n,n,n,n,n,n,n,n,n,Y,Y,n,n,Y,Y,n,n,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,Y,n,Y,Y,n,n,Y,n,n,n,Y,n,n,n,n,n,Y,Y,Y,Y,n,n,Y,n,n,Y,Y,n,n,n,n,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 16: Bool Decimal SByte Byte Short UShort Int UInt Long ULong Char Float Double Object String Dynamic Void Multiplication True False DecimalIntegerLiteral HexadecimalIntegerLiteral RealLiteral CharacterLiteral StringLiteral VerbatimStringLiteral Null OpenParenthesis This Base New TypeOf SizeOf Checked Unchecked Increment Decrement Addition Subtraction Negation OnesComplement BitwiseAnd SemiColon If Switch OpenCurlyBrace While Do For ForEach Break Continue Goto Return Throw Try Lock Using Yield Unsafe Fixed Const Identifier Ascending By Descending Equals From Group Into Join Let On OrderBy Select Where Var Delegate 
			{n,n,n,n,n,n,n,n,n,n,n,Y,Y,Y,Y,Y,Y,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,n,n,Y,Y,Y,Y,n,n,Y,Y,n,Y,Y,Y,n,Y,Y,Y,Y,n,n,n,n,n,Y,n,Y,Y,Y,Y,n,Y,Y,n,n,Y,n,n,n,Y,Y,n,Y,Y,Y,n,n,n,n,n,n,n,n,n,n,n,Y,Y,n,n,Y,Y,n,n,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,Y,n,Y,Y,n,n,Y,n,n,n,Y,n,n,n,n,n,Y,Y,Y,Y,n,n,Y,n,n,Y,Y,n,n,n,n,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 17: Identifier Ascending By Descending Equals From Group Into Join Let On OrderBy Select Where Var 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 18: OpenSquareBrace New Unsafe Namespace Public Protected Internal Private Abstract Extern Partial Sealed Static Override ReadOnly Virtual Volatile Class Struct Interface Enum Delegate 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,Y,n,n,n,n,Y,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,Y,Y,n,n,n,Y,Y,n,n,n,n,Y,n,Y,Y,Y,Y,Y,n,n,n,n,Y,n,n,n,n,Y,n,Y,n,n,n,n,n,n,n,n,n,Y,n,n,Y,n,Y,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 19: OpenSquareBrace New Unsafe Namespace Public Protected Internal Private Abstract Extern Partial Sealed Static Override ReadOnly Virtual Volatile Class Struct Interface Enum Delegate 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,Y,n,n,n,n,Y,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,Y,Y,n,n,n,Y,Y,n,n,n,n,Y,n,Y,Y,Y,Y,Y,n,n,n,n,Y,n,n,n,n,Y,n,Y,n,n,n,n,n,n,n,n,n,Y,n,n,Y,n,Y,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 20: OpenSquareBrace New Unsafe Public Protected Internal Private Abstract Extern Partial Sealed Static Override ReadOnly Virtual Volatile Class Struct Interface Enum Delegate 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,Y,n,n,n,n,Y,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,Y,Y,n,n,n,n,Y,n,n,n,n,Y,n,Y,Y,Y,Y,Y,n,n,n,n,Y,n,n,n,n,Y,n,Y,n,n,n,n,n,n,n,n,n,Y,n,n,Y,n,Y,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 21: New Unsafe Public Protected Internal Private Abstract Extern Partial Sealed Static Override ReadOnly Virtual Volatile 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,Y,n,n,n,n,Y,n,Y,Y,Y,Y,Y,n,n,n,n,Y,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,Y,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 22: New Unsafe Public Protected Internal Private Abstract Extern Partial Sealed Static Override ReadOnly Virtual Volatile 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,Y,n,n,n,n,Y,n,Y,Y,Y,Y,Y,n,n,n,n,Y,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,Y,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 23: Bool Decimal SByte Byte Short UShort Int UInt Long ULong Char Float Double Object String Dynamic Void OnesComplement Const Identifier Ascending By Descending Equals From Group Into Join Let On OrderBy Select Where Var Class Struct Implicit Explicit Event Interface Enum Delegate 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,n,n,n,Y,n,Y,n,n,Y,n,Y,Y,n,Y,n,Y,n,Y,Y,n,Y,Y,Y,n,n,n,n,Y,n,n,n,n,n,Y,n,Y,Y,n,n,n,Y,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,Y,n,n,n,Y,Y,n,n,n,n,n,n,Y,Y,n,n,Y,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 24: Bool Decimal SByte Byte Short UShort Int UInt Long ULong Char Float Double Object String Dynamic Void Const Identifier Ascending By Descending Equals From Group Into Join Let On OrderBy Select Where Var Class Struct Implicit Explicit Event Interface Enum Delegate 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,n,n,n,Y,n,Y,n,n,Y,n,Y,Y,n,Y,n,Y,n,Y,Y,n,Y,Y,Y,n,n,n,n,Y,n,n,n,n,n,Y,n,Y,Y,n,n,n,Y,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,Y,n,n,n,Y,Y,n,n,n,n,n,n,Y,Y,n,n,Y,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 25: Bool Decimal SByte Byte Short UShort Int UInt Long ULong Char Float Double Object String Dynamic Void Ref Out Identifier Ascending By Descending Equals From Group Into Join Let On OrderBy Select Where Var 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,n,n,n,Y,n,Y,n,n,Y,n,n,n,n,Y,n,n,n,Y,Y,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,Y,n,n,n,n,Y,n,n,n,Y,n,Y,n,n,n,n,n,n,n,Y,n,n,Y,n,n,Y,n,n,n,Y,n,n,n,n,n,n,n,Y,Y,n,n,Y,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 26: Bool Decimal SByte Byte Short UShort Int UInt Long ULong Char Float Double Object String Dynamic Void OpenSquareBrace Ref Out Identifier Ascending By Descending Equals From Group Into Join Let On OrderBy Select Where Var Params 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,n,n,n,Y,n,Y,n,n,Y,n,n,n,n,Y,n,n,n,Y,Y,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,Y,n,n,n,n,Y,n,n,n,Y,n,Y,n,Y,n,n,n,n,n,Y,n,n,Y,n,n,Y,n,n,n,Y,n,n,n,n,n,n,n,Y,Y,n,n,Y,n,n,Y,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 27: OpenSquareBrace New Unsafe Public Protected Internal Private Abstract Extern Partial Sealed Static Override ReadOnly Virtual Volatile Add Remove 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,Y,n,n,n,n,Y,n,Y,Y,Y,Y,Y,n,Y,n,n,Y,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,Y,n,Y,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 28: Class Struct Interface Enum Delegate 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,Y,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 29: Bool Decimal SByte Byte Short UShort Int UInt Long ULong Char Float Double Object String Dynamic Void Identifier Ascending By Descending Equals From Group Into Join Let On OrderBy Select Where Var 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,n,n,n,Y,n,Y,n,n,Y,n,n,n,n,Y,n,n,n,Y,Y,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,Y,n,n,n,n,Y,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,Y,n,n,n,Y,n,n,n,n,n,n,n,Y,Y,n,n,Y,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 30: This Identifier Ascending By Descending Equals From Group Into Join Let On OrderBy Select Where Var 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 31: OpenSquareBrace New Unsafe Public Protected Internal Private Abstract Extern Partial Sealed Static Override ReadOnly Virtual Volatile Get Set 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,Y,n,n,n,n,n,n,Y,n,n,n,n,Y,n,n,n,n,Y,n,Y,Y,Y,Y,Y,n,n,n,n,Y,Y,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,Y,n,Y,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 32: Bool Decimal SByte Byte Short UShort Int UInt Long ULong Char Float Double Object String Dynamic Multiplication True False DecimalIntegerLiteral HexadecimalIntegerLiteral RealLiteral CharacterLiteral StringLiteral VerbatimStringLiteral Null OpenParenthesis This Base New TypeOf SizeOf Checked Unchecked Increment Decrement Addition Subtraction Negation OnesComplement BitwiseAnd OpenCurlyBrace Identifier Ascending By Descending Equals From Group Into Join Let On OrderBy Select Where Var Delegate StackAlloc 
			{n,n,n,n,n,n,n,n,n,n,n,Y,Y,Y,Y,Y,Y,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,n,n,Y,Y,n,Y,n,n,Y,Y,n,n,n,Y,n,Y,n,Y,Y,n,n,n,n,n,Y,n,n,Y,n,n,n,n,n,n,n,Y,n,n,n,n,Y,n,Y,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,Y,Y,Y,n,Y,n,n,Y,n,Y,n,Y,Y,Y,Y,n,Y,n,n,n,n,n,n,n,n,Y,n,n,n,Y,n,n,n,n,n,n,Y,Y,Y,n,n,Y,n,n,Y,Y,n,n,n,n,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 33: Bool Decimal SByte Byte Short UShort Int UInt Long ULong Char Float Double Object String Dynamic Void OpenSquareBrace New Identifier Ascending By Descending Equals From Group Into Join Let On OrderBy Select Where Var Event 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,n,n,n,Y,n,Y,n,n,Y,n,n,n,n,Y,n,n,n,Y,Y,n,n,Y,n,n,n,n,n,Y,n,n,n,n,n,n,n,Y,n,n,n,n,Y,n,Y,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,Y,n,n,n,Y,n,n,n,n,n,n,n,Y,Y,n,n,Y,n,n,Y,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 34: OpenSquareBrace Identifier Ascending By Descending Equals From Group Into Join Let On OrderBy Select Where Var 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 35: Bool Decimal SByte Byte Short UShort Int UInt Long ULong Char Float Double Object String Dynamic Void Ref Out Identifier Ascending By Descending Equals From Group Into Join Let On OrderBy Select Where Var 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,n,n,n,Y,n,Y,n,n,Y,n,n,n,n,Y,n,n,n,Y,Y,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,Y,n,n,n,n,Y,n,n,n,Y,n,Y,n,n,n,n,n,n,n,Y,n,n,Y,n,n,Y,n,n,n,Y,n,n,n,n,n,n,n,Y,Y,n,n,Y,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 36: From Join Let OrderBy Where 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,Y,Y,n,Y,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n}
		};

	}
	#endregion

}

