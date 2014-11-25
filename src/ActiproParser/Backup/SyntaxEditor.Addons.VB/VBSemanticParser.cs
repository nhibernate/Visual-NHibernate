using System;
using System.Collections;
using System.Diagnostics;
using System.Text;
using ActiproSoftware.Products.SyntaxEditor.Addons.DotNet;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;
using ActiproSoftware.SyntaxEditor.Addons;

namespace ActiproSoftware.SyntaxEditor.Addons.VB {

	#region Token IDs
	/// <summary>
	/// Contains the token IDs for the <c>Visual Basic</c> language.
	/// </summary>
	public class VBTokenID {

		/// <summary>
		/// Returns the string-based key for the specified token ID.
		/// </summary>
		/// <param name="id">The token ID to examine.</param>
		public static string GetTokenKey(int id) {
			System.Reflection.FieldInfo[] fields = typeof(VBTokenID).GetFields();
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
		/// The RemComment token ID.
		/// </summary>
		public const int RemComment = 7;

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
		/// The OctalIntegerLiteral token ID.
		/// </summary>
		public const int OctalIntegerLiteral = 13;

		/// <summary>
		/// The FloatingPointLiteral token ID.
		/// </summary>
		public const int FloatingPointLiteral = 14;

		/// <summary>
		/// The CharacterLiteral token ID.
		/// </summary>
		public const int CharacterLiteral = 15;

		/// <summary>
		/// The StringLiteral token ID.
		/// </summary>
		public const int StringLiteral = 16;

		/// <summary>
		/// The DateLiteral token ID.
		/// </summary>
		public const int DateLiteral = 17;

		/// <summary>
		/// The XmlLiteral token ID.
		/// </summary>
		public const int XmlLiteral = 18;

		/// <summary>
		/// The XmlAttribute token ID.
		/// </summary>
		public const int XmlAttribute = 19;

		/// <summary>
		/// The Identifier token ID.
		/// </summary>
		public const int Identifier = 20;

		/// <summary>
		/// The ContextualKeywordStart token ID.
		/// </summary>
		public const int ContextualKeywordStart = 21;

		/// <summary>
		/// The Aggregate token ID.
		/// </summary>
		public const int Aggregate = 22;

		/// <summary>
		/// The Ascending token ID.
		/// </summary>
		public const int Ascending = 23;

		/// <summary>
		/// The By token ID.
		/// </summary>
		public const int By = 24;

		/// <summary>
		/// The Descending token ID.
		/// </summary>
		public const int Descending = 25;

		/// <summary>
		/// The Distinct token ID.
		/// </summary>
		public const int Distinct = 26;

		/// <summary>
		/// The Equals token ID.
		/// </summary>
		public const int Equals = 27;

		/// <summary>
		/// The From token ID.
		/// </summary>
		public const int From = 28;

		/// <summary>
		/// The Group token ID.
		/// </summary>
		public const int Group = 29;

		/// <summary>
		/// The Into token ID.
		/// </summary>
		public const int Into = 30;

		/// <summary>
		/// The Join token ID.
		/// </summary>
		public const int Join = 31;

		/// <summary>
		/// The Order token ID.
		/// </summary>
		public const int Order = 32;

		/// <summary>
		/// The Out token ID.
		/// </summary>
		public const int Out = 33;

		/// <summary>
		/// The Skip token ID.
		/// </summary>
		public const int Skip = 34;

		/// <summary>
		/// The Take token ID.
		/// </summary>
		public const int Take = 35;

		/// <summary>
		/// The Where token ID.
		/// </summary>
		public const int Where = 36;

		/// <summary>
		/// The ContextualKeywordEnd token ID.
		/// </summary>
		public const int ContextualKeywordEnd = 37;

		/// <summary>
		/// The KeywordStart token ID.
		/// </summary>
		public const int KeywordStart = 38;

		/// <summary>
		/// The AddHandler token ID.
		/// </summary>
		public const int AddHandler = 39;

		/// <summary>
		/// The AddressOf token ID.
		/// </summary>
		public const int AddressOf = 40;

		/// <summary>
		/// The Alias token ID.
		/// </summary>
		public const int Alias = 41;

		/// <summary>
		/// The And token ID.
		/// </summary>
		public const int And = 42;

		/// <summary>
		/// The AndAlso token ID.
		/// </summary>
		public const int AndAlso = 43;

		/// <summary>
		/// The As token ID.
		/// </summary>
		public const int As = 44;

		/// <summary>
		/// The Boolean token ID.
		/// </summary>
		public const int Boolean = 45;

		/// <summary>
		/// The ByRef token ID.
		/// </summary>
		public const int ByRef = 46;

		/// <summary>
		/// The Byte token ID.
		/// </summary>
		public const int Byte = 47;

		/// <summary>
		/// The ByVal token ID.
		/// </summary>
		public const int ByVal = 48;

		/// <summary>
		/// The Call token ID.
		/// </summary>
		public const int Call = 49;

		/// <summary>
		/// The Case token ID.
		/// </summary>
		public const int Case = 50;

		/// <summary>
		/// The Catch token ID.
		/// </summary>
		public const int Catch = 51;

		/// <summary>
		/// The CBool token ID.
		/// </summary>
		public const int CBool = 52;

		/// <summary>
		/// The CByte token ID.
		/// </summary>
		public const int CByte = 53;

		/// <summary>
		/// The CChar token ID.
		/// </summary>
		public const int CChar = 54;

		/// <summary>
		/// The CDate token ID.
		/// </summary>
		public const int CDate = 55;

		/// <summary>
		/// The CDbl token ID.
		/// </summary>
		public const int CDbl = 56;

		/// <summary>
		/// The CDec token ID.
		/// </summary>
		public const int CDec = 57;

		/// <summary>
		/// The Char token ID.
		/// </summary>
		public const int Char = 58;

		/// <summary>
		/// The CInt token ID.
		/// </summary>
		public const int CInt = 59;

		/// <summary>
		/// The Class token ID.
		/// </summary>
		public const int Class = 60;

		/// <summary>
		/// The CLng token ID.
		/// </summary>
		public const int CLng = 61;

		/// <summary>
		/// The CObj token ID.
		/// </summary>
		public const int CObj = 62;

		/// <summary>
		/// The Const token ID.
		/// </summary>
		public const int Const = 63;

		/// <summary>
		/// The Continue token ID.
		/// </summary>
		public const int Continue = 64;

		/// <summary>
		/// The CSByte token ID.
		/// </summary>
		public const int CSByte = 65;

		/// <summary>
		/// The CShort token ID.
		/// </summary>
		public const int CShort = 66;

		/// <summary>
		/// The CSng token ID.
		/// </summary>
		public const int CSng = 67;

		/// <summary>
		/// The CStr token ID.
		/// </summary>
		public const int CStr = 68;

		/// <summary>
		/// The CType token ID.
		/// </summary>
		public const int CType = 69;

		/// <summary>
		/// The CUInt token ID.
		/// </summary>
		public const int CUInt = 70;

		/// <summary>
		/// The CULng token ID.
		/// </summary>
		public const int CULng = 71;

		/// <summary>
		/// The CUShort token ID.
		/// </summary>
		public const int CUShort = 72;

		/// <summary>
		/// The Custom token ID.
		/// </summary>
		public const int Custom = 73;

		/// <summary>
		/// The Date token ID.
		/// </summary>
		public const int Date = 74;

		/// <summary>
		/// The Decimal token ID.
		/// </summary>
		public const int Decimal = 75;

		/// <summary>
		/// The Declare token ID.
		/// </summary>
		public const int Declare = 76;

		/// <summary>
		/// The Default token ID.
		/// </summary>
		public const int Default = 77;

		/// <summary>
		/// The Delegate token ID.
		/// </summary>
		public const int Delegate = 78;

		/// <summary>
		/// The Dim token ID.
		/// </summary>
		public const int Dim = 79;

		/// <summary>
		/// The DirectCast token ID.
		/// </summary>
		public const int DirectCast = 80;

		/// <summary>
		/// The Do token ID.
		/// </summary>
		public const int Do = 81;

		/// <summary>
		/// The Double token ID.
		/// </summary>
		public const int Double = 82;

		/// <summary>
		/// The Each token ID.
		/// </summary>
		public const int Each = 83;

		/// <summary>
		/// The Else token ID.
		/// </summary>
		public const int Else = 84;

		/// <summary>
		/// The ElseIf token ID.
		/// </summary>
		public const int ElseIf = 85;

		/// <summary>
		/// The End token ID.
		/// </summary>
		public const int End = 86;

		/// <summary>
		/// The EndIf token ID.
		/// </summary>
		public const int EndIf = 87;

		/// <summary>
		/// The Enum token ID.
		/// </summary>
		public const int Enum = 88;

		/// <summary>
		/// The Erase token ID.
		/// </summary>
		public const int Erase = 89;

		/// <summary>
		/// The Error token ID.
		/// </summary>
		public const int Error = 90;

		/// <summary>
		/// The Event token ID.
		/// </summary>
		public const int Event = 91;

		/// <summary>
		/// The Exit token ID.
		/// </summary>
		public const int Exit = 92;

		/// <summary>
		/// The False token ID.
		/// </summary>
		public const int False = 93;

		/// <summary>
		/// The Finally token ID.
		/// </summary>
		public const int Finally = 94;

		/// <summary>
		/// The For token ID.
		/// </summary>
		public const int For = 95;

		/// <summary>
		/// The Friend token ID.
		/// </summary>
		public const int Friend = 96;

		/// <summary>
		/// The Function token ID.
		/// </summary>
		public const int Function = 97;

		/// <summary>
		/// The Get token ID.
		/// </summary>
		public const int Get = 98;

		/// <summary>
		/// The GetTypeKeyword token ID.
		/// </summary>
		public const int GetTypeKeyword = 99;

		/// <summary>
		/// The GetXmlNamespace token ID.
		/// </summary>
		public const int GetXmlNamespace = 100;

		/// <summary>
		/// The Global token ID.
		/// </summary>
		public const int Global = 101;

		/// <summary>
		/// The GoSub token ID.
		/// </summary>
		public const int GoSub = 102;

		/// <summary>
		/// The GoTo token ID.
		/// </summary>
		public const int GoTo = 103;

		/// <summary>
		/// The Handles token ID.
		/// </summary>
		public const int Handles = 104;

		/// <summary>
		/// The If token ID.
		/// </summary>
		public const int If = 105;

		/// <summary>
		/// The IIf token ID.
		/// </summary>
		public const int IIf = 106;

		/// <summary>
		/// The Implements token ID.
		/// </summary>
		public const int Implements = 107;

		/// <summary>
		/// The Imports token ID.
		/// </summary>
		public const int Imports = 108;

		/// <summary>
		/// The In token ID.
		/// </summary>
		public const int In = 109;

		/// <summary>
		/// The Inherits token ID.
		/// </summary>
		public const int Inherits = 110;

		/// <summary>
		/// The Integer token ID.
		/// </summary>
		public const int Integer = 111;

		/// <summary>
		/// The Interface token ID.
		/// </summary>
		public const int Interface = 112;

		/// <summary>
		/// The Is token ID.
		/// </summary>
		public const int Is = 113;

		/// <summary>
		/// The IsFalse token ID.
		/// </summary>
		public const int IsFalse = 114;

		/// <summary>
		/// The IsNot token ID.
		/// </summary>
		public const int IsNot = 115;

		/// <summary>
		/// The IsTrue token ID.
		/// </summary>
		public const int IsTrue = 116;

		/// <summary>
		/// The Let token ID.
		/// </summary>
		public const int Let = 117;

		/// <summary>
		/// The Lib token ID.
		/// </summary>
		public const int Lib = 118;

		/// <summary>
		/// The Like token ID.
		/// </summary>
		public const int Like = 119;

		/// <summary>
		/// The Long token ID.
		/// </summary>
		public const int Long = 120;

		/// <summary>
		/// The Loop token ID.
		/// </summary>
		public const int Loop = 121;

		/// <summary>
		/// The Me token ID.
		/// </summary>
		public const int Me = 122;

		/// <summary>
		/// The Mid token ID.
		/// </summary>
		public const int Mid = 123;

		/// <summary>
		/// The Mod token ID.
		/// </summary>
		public const int Mod = 124;

		/// <summary>
		/// The Module token ID.
		/// </summary>
		public const int Module = 125;

		/// <summary>
		/// The MustInherit token ID.
		/// </summary>
		public const int MustInherit = 126;

		/// <summary>
		/// The MustOverride token ID.
		/// </summary>
		public const int MustOverride = 127;

		/// <summary>
		/// The MyBase token ID.
		/// </summary>
		public const int MyBase = 128;

		/// <summary>
		/// The MyClass token ID.
		/// </summary>
		public const int MyClass = 129;

		/// <summary>
		/// The Namespace token ID.
		/// </summary>
		public const int Namespace = 130;

		/// <summary>
		/// The Narrowing token ID.
		/// </summary>
		public const int Narrowing = 131;

		/// <summary>
		/// The New token ID.
		/// </summary>
		public const int New = 132;

		/// <summary>
		/// The Next token ID.
		/// </summary>
		public const int Next = 133;

		/// <summary>
		/// The Not token ID.
		/// </summary>
		public const int Not = 134;

		/// <summary>
		/// The Nothing token ID.
		/// </summary>
		public const int Nothing = 135;

		/// <summary>
		/// The NotInheritable token ID.
		/// </summary>
		public const int NotInheritable = 136;

		/// <summary>
		/// The NotOverridable token ID.
		/// </summary>
		public const int NotOverridable = 137;

		/// <summary>
		/// The Object token ID.
		/// </summary>
		public const int Object = 138;

		/// <summary>
		/// The Of token ID.
		/// </summary>
		public const int Of = 139;

		/// <summary>
		/// The On token ID.
		/// </summary>
		public const int On = 140;

		/// <summary>
		/// The Operator token ID.
		/// </summary>
		public const int Operator = 141;

		/// <summary>
		/// The Option token ID.
		/// </summary>
		public const int Option = 142;

		/// <summary>
		/// The Optional token ID.
		/// </summary>
		public const int Optional = 143;

		/// <summary>
		/// The Or token ID.
		/// </summary>
		public const int Or = 144;

		/// <summary>
		/// The OrElse token ID.
		/// </summary>
		public const int OrElse = 145;

		/// <summary>
		/// The Overloads token ID.
		/// </summary>
		public const int Overloads = 146;

		/// <summary>
		/// The Overridable token ID.
		/// </summary>
		public const int Overridable = 147;

		/// <summary>
		/// The Overrides token ID.
		/// </summary>
		public const int Overrides = 148;

		/// <summary>
		/// The ParamArray token ID.
		/// </summary>
		public const int ParamArray = 149;

		/// <summary>
		/// The Partial token ID.
		/// </summary>
		public const int Partial = 150;

		/// <summary>
		/// The Private token ID.
		/// </summary>
		public const int Private = 151;

		/// <summary>
		/// The Property token ID.
		/// </summary>
		public const int Property = 152;

		/// <summary>
		/// The Protected token ID.
		/// </summary>
		public const int Protected = 153;

		/// <summary>
		/// The Public token ID.
		/// </summary>
		public const int Public = 154;

		/// <summary>
		/// The RaiseEvent token ID.
		/// </summary>
		public const int RaiseEvent = 155;

		/// <summary>
		/// The ReadOnly token ID.
		/// </summary>
		public const int ReadOnly = 156;

		/// <summary>
		/// The ReDim token ID.
		/// </summary>
		public const int ReDim = 157;

		/// <summary>
		/// The REM token ID.
		/// </summary>
		public const int REM = 158;

		/// <summary>
		/// The RemoveHandler token ID.
		/// </summary>
		public const int RemoveHandler = 159;

		/// <summary>
		/// The Resume token ID.
		/// </summary>
		public const int Resume = 160;

		/// <summary>
		/// The Return token ID.
		/// </summary>
		public const int Return = 161;

		/// <summary>
		/// The SByte token ID.
		/// </summary>
		public const int SByte = 162;

		/// <summary>
		/// The Select token ID.
		/// </summary>
		public const int Select = 163;

		/// <summary>
		/// The Set token ID.
		/// </summary>
		public const int Set = 164;

		/// <summary>
		/// The Shadows token ID.
		/// </summary>
		public const int Shadows = 165;

		/// <summary>
		/// The Shared token ID.
		/// </summary>
		public const int Shared = 166;

		/// <summary>
		/// The Short token ID.
		/// </summary>
		public const int Short = 167;

		/// <summary>
		/// The Single token ID.
		/// </summary>
		public const int Single = 168;

		/// <summary>
		/// The Static token ID.
		/// </summary>
		public const int Static = 169;

		/// <summary>
		/// The Step token ID.
		/// </summary>
		public const int Step = 170;

		/// <summary>
		/// The Stop token ID.
		/// </summary>
		public const int Stop = 171;

		/// <summary>
		/// The String token ID.
		/// </summary>
		public const int String = 172;

		/// <summary>
		/// The Structure token ID.
		/// </summary>
		public const int Structure = 173;

		/// <summary>
		/// The Sub token ID.
		/// </summary>
		public const int Sub = 174;

		/// <summary>
		/// The SyncLock token ID.
		/// </summary>
		public const int SyncLock = 175;

		/// <summary>
		/// The Then token ID.
		/// </summary>
		public const int Then = 176;

		/// <summary>
		/// The Throw token ID.
		/// </summary>
		public const int Throw = 177;

		/// <summary>
		/// The To token ID.
		/// </summary>
		public const int To = 178;

		/// <summary>
		/// The True token ID.
		/// </summary>
		public const int True = 179;

		/// <summary>
		/// The Try token ID.
		/// </summary>
		public const int Try = 180;

		/// <summary>
		/// The TryCast token ID.
		/// </summary>
		public const int TryCast = 181;

		/// <summary>
		/// The TypeOf token ID.
		/// </summary>
		public const int TypeOf = 182;

		/// <summary>
		/// The UInteger token ID.
		/// </summary>
		public const int UInteger = 183;

		/// <summary>
		/// The ULong token ID.
		/// </summary>
		public const int ULong = 184;

		/// <summary>
		/// The Until token ID.
		/// </summary>
		public const int Until = 185;

		/// <summary>
		/// The UShort token ID.
		/// </summary>
		public const int UShort = 186;

		/// <summary>
		/// The Using token ID.
		/// </summary>
		public const int Using = 187;

		/// <summary>
		/// The Variant token ID.
		/// </summary>
		public const int Variant = 188;

		/// <summary>
		/// The Wend token ID.
		/// </summary>
		public const int Wend = 189;

		/// <summary>
		/// The When token ID.
		/// </summary>
		public const int When = 190;

		/// <summary>
		/// The While token ID.
		/// </summary>
		public const int While = 191;

		/// <summary>
		/// The Widening token ID.
		/// </summary>
		public const int Widening = 192;

		/// <summary>
		/// The With token ID.
		/// </summary>
		public const int With = 193;

		/// <summary>
		/// The WithEvents token ID.
		/// </summary>
		public const int WithEvents = 194;

		/// <summary>
		/// The WriteOnly token ID.
		/// </summary>
		public const int WriteOnly = 195;

		/// <summary>
		/// The Xor token ID.
		/// </summary>
		public const int Xor = 196;

		/// <summary>
		/// The KeywordEnd token ID.
		/// </summary>
		public const int KeywordEnd = 197;

		/// <summary>
		/// The OperatorOrPunctuatorStart token ID.
		/// </summary>
		public const int OperatorOrPunctuatorStart = 198;

		/// <summary>
		/// The LineContinuation token ID.
		/// </summary>
		public const int LineContinuation = 199;

		/// <summary>
		/// The OpenParenthesis token ID.
		/// </summary>
		public const int OpenParenthesis = 200;

		/// <summary>
		/// The CloseParenthesis token ID.
		/// </summary>
		public const int CloseParenthesis = 201;

		/// <summary>
		/// The OpenCurlyBrace token ID.
		/// </summary>
		public const int OpenCurlyBrace = 202;

		/// <summary>
		/// The CloseCurlyBrace token ID.
		/// </summary>
		public const int CloseCurlyBrace = 203;

		/// <summary>
		/// The OpenSquareBrace token ID.
		/// </summary>
		public const int OpenSquareBrace = 204;

		/// <summary>
		/// The CloseSquareBrace token ID.
		/// </summary>
		public const int CloseSquareBrace = 205;

		/// <summary>
		/// The Comma token ID.
		/// </summary>
		public const int Comma = 206;

		/// <summary>
		/// The Dot token ID.
		/// </summary>
		public const int Dot = 207;

		/// <summary>
		/// The Colon token ID.
		/// </summary>
		public const int Colon = 208;

		/// <summary>
		/// The ColonEquals token ID.
		/// </summary>
		public const int ColonEquals = 209;

		/// <summary>
		/// The ExclamationPoint token ID.
		/// </summary>
		public const int ExclamationPoint = 210;

		/// <summary>
		/// The QuestionMark token ID.
		/// </summary>
		public const int QuestionMark = 211;

		/// <summary>
		/// The DotAt token ID.
		/// </summary>
		public const int DotAt = 212;

		/// <summary>
		/// The TripleDot token ID.
		/// </summary>
		public const int TripleDot = 213;

		/// <summary>
		/// The StringConcatenation token ID.
		/// </summary>
		public const int StringConcatenation = 214;

		/// <summary>
		/// The Multiplication token ID.
		/// </summary>
		public const int Multiplication = 215;

		/// <summary>
		/// The Addition token ID.
		/// </summary>
		public const int Addition = 216;

		/// <summary>
		/// The Subtraction token ID.
		/// </summary>
		public const int Subtraction = 217;

		/// <summary>
		/// The FloatingPointDivision token ID.
		/// </summary>
		public const int FloatingPointDivision = 218;

		/// <summary>
		/// The IntegerDivision token ID.
		/// </summary>
		public const int IntegerDivision = 219;

		/// <summary>
		/// The Exponentiation token ID.
		/// </summary>
		public const int Exponentiation = 220;

		/// <summary>
		/// The LessThan token ID.
		/// </summary>
		public const int LessThan = 221;

		/// <summary>
		/// The Equality token ID.
		/// </summary>
		public const int Equality = 222;

		/// <summary>
		/// The GreaterThan token ID.
		/// </summary>
		public const int GreaterThan = 223;

		/// <summary>
		/// The LessThanOrEqual token ID.
		/// </summary>
		public const int LessThanOrEqual = 224;

		/// <summary>
		/// The GreaterThanOrEqual token ID.
		/// </summary>
		public const int GreaterThanOrEqual = 225;

		/// <summary>
		/// The Inequality token ID.
		/// </summary>
		public const int Inequality = 226;

		/// <summary>
		/// The LeftShift token ID.
		/// </summary>
		public const int LeftShift = 227;

		/// <summary>
		/// The RightShift token ID.
		/// </summary>
		public const int RightShift = 228;

		/// <summary>
		/// The StringConcatenationAssignment token ID.
		/// </summary>
		public const int StringConcatenationAssignment = 229;

		/// <summary>
		/// The MultiplicationAssignment token ID.
		/// </summary>
		public const int MultiplicationAssignment = 230;

		/// <summary>
		/// The AdditionAssignment token ID.
		/// </summary>
		public const int AdditionAssignment = 231;

		/// <summary>
		/// The SubtractionAssignment token ID.
		/// </summary>
		public const int SubtractionAssignment = 232;

		/// <summary>
		/// The FloatingPointDivisionAssignment token ID.
		/// </summary>
		public const int FloatingPointDivisionAssignment = 233;

		/// <summary>
		/// The IntegerDivisionAssignment token ID.
		/// </summary>
		public const int IntegerDivisionAssignment = 234;

		/// <summary>
		/// The ExponentiationAssignment token ID.
		/// </summary>
		public const int ExponentiationAssignment = 235;

		/// <summary>
		/// The LeftShiftAssignment token ID.
		/// </summary>
		public const int LeftShiftAssignment = 236;

		/// <summary>
		/// The RightShiftAssignment token ID.
		/// </summary>
		public const int RightShiftAssignment = 237;

		/// <summary>
		/// The OperatorOrPunctuatorEnd token ID.
		/// </summary>
		public const int OperatorOrPunctuatorEnd = 238;

		/// <summary>
		/// The PreProcessorDirectiveKeywordStart token ID.
		/// </summary>
		public const int PreProcessorDirectiveKeywordStart = 239;

		/// <summary>
		/// The ConstPreProcessorDirective token ID.
		/// </summary>
		public const int ConstPreProcessorDirective = 240;

		/// <summary>
		/// The IfPreProcessorDirective token ID.
		/// </summary>
		public const int IfPreProcessorDirective = 241;

		/// <summary>
		/// The ElseIfPreProcessorDirective token ID.
		/// </summary>
		public const int ElseIfPreProcessorDirective = 242;

		/// <summary>
		/// The ElsePreProcessorDirective token ID.
		/// </summary>
		public const int ElsePreProcessorDirective = 243;

		/// <summary>
		/// The EndIfPreProcessorDirective token ID.
		/// </summary>
		public const int EndIfPreProcessorDirective = 244;

		/// <summary>
		/// The ExternalSourcePreProcessorDirective token ID.
		/// </summary>
		public const int ExternalSourcePreProcessorDirective = 245;

		/// <summary>
		/// The EndExternalSourcePreProcessorDirective token ID.
		/// </summary>
		public const int EndExternalSourcePreProcessorDirective = 246;

		/// <summary>
		/// The RegionPreProcessorDirective token ID.
		/// </summary>
		public const int RegionPreProcessorDirective = 247;

		/// <summary>
		/// The EndRegionPreProcessorDirective token ID.
		/// </summary>
		public const int EndRegionPreProcessorDirective = 248;

		/// <summary>
		/// The ExternalChecksumPreProcessorDirective token ID.
		/// </summary>
		public const int ExternalChecksumPreProcessorDirective = 249;

		/// <summary>
		/// The PreProcessorDirectiveKeywordEnd token ID.
		/// </summary>
		public const int PreProcessorDirectiveKeywordEnd = 250;

		/// <summary>
		/// The PreProcessorDirectiveText token ID.
		/// </summary>
		public const int PreProcessorDirectiveText = 251;

		/// <summary>
		/// The MaxTokenID token ID.
		/// </summary>
		public const int MaxTokenID = 252;

	}
	#endregion

	#region Lexical State IDs
	/// <summary>
	/// Contains the lexical state IDs for the <c>Visual Basic</c> language.
	/// </summary>
	public class VBLexicalStateID {

		/// <summary>
		/// Returns the string-based key for the specified lexical state ID.
		/// </summary>
		/// <param name="id">The lexical state ID to examine.</param>
		public static string GetLexicalStateKey(int id) {
			System.Reflection.FieldInfo[] fields = typeof(VBLexicalStateID).GetFields();
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
	/// Provides a semantic parser for the <c>Visual Basic</c> language.
	/// </summary>
	internal class VBSemanticParser : ActiproSoftware.SyntaxEditor.ParserGenerator.RecursiveDescentSemanticParser {

		private Stack			blockStack;
		private CompilationUnit	compilationUnit;
		private StringBuilder	identifierStringBuilder = new StringBuilder();
		private int				nestingLevel;
		
		/// <summary>
		/// Stores information about a block node.
		/// </summary>
		private class BlockData {
		
			public AstNode	Node;
			public int		TokenID;
				
			/// <summary>
			/// Initializes a new instance of the <c>BlockData</c> class.
			/// </summary>
			/// <param name="tokenID">The ID of the token for the block.</param>
			/// <param name="node">The <see cref="AstNode"/> that is starting a block.</param>
			public BlockData(int tokenID, AstNode node) {
				// Initialize parameters
				this.TokenID	= tokenID;
				this.Node		= node;
			}
		
		}

		/// <summary>
		/// Initializes a new instance of the <c>VBSemanticParser</c> class.
		/// </summary>
		/// <param name="lexicalParser">The <see cref="ActiproSoftware.SyntaxEditor.ParserGenerator.IRecursiveDescentLexicalParser"/> to use for lexical parsing.</param>
		public VBSemanticParser(ActiproSoftware.SyntaxEditor.ParserGenerator.IRecursiveDescentLexicalParser lexicalParser) : base(lexicalParser) {}
	
		/// <summary>
		/// Advances past any line terminators.
		/// </summary>
		private void AdvancePastTerminators() {
			while (!this.IsAtEnd) {
				if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator))
					this.AdvanceToNext();
				else
					break;
			}			
		}

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
					case VBTokenID.While:
						nestingLevel++;
						break;
					case VBTokenID.End:
						nestingLevel--;
						break;
				}
			}
			return token;
		}

		/// <summary>
		/// Advances past the next matching end.
		/// </summary>
		/// <param name="tokenID">The ID of the desired end token.</param>
		/// <returns>
		/// <c>true</c> if a match was found; otherwise, <c>false</c>.
		/// </returns>
		private bool AdvanceToNextEnd(int tokenID) {
			return this.AdvanceToNextEnd(tokenID, false);
		}
	
		/// <summary>
		/// Advances past the next matching end.
		/// </summary>
		/// <param name="tokenID">The ID of the desired end token.</param>
		/// <param name="quitOnNonStatementEnd">Whether to quit on a non-statement end.</param>
		/// <returns>
		/// <c>true</c> if a match was found; otherwise, <c>false</c>.
		/// </returns>
		private bool AdvanceToNextEnd(int tokenID, bool quitOnNonStatementEnd) {
			int depth = 1;
			while (!this.IsAtEnd) {
				if ((this.IsEnd(tokenID)) && (--depth == 0))
					break;
				if ((quitOnNonStatementEnd) && (this.IsEndOfNonStatement()))
					return false;
				if (this.TokenIs(this.LookAheadToken, tokenID))
					depth++;
				this.AdvanceToNext();
			}			
			
			// Quit if at the end of the document
			if (this.IsAtEnd)
				return false;
				
			// Skip over the End and next token
			this.AdvanceToNext();
			this.AdvanceToNext();
			return true;
		}
	
		/// <summary>
		/// Advances past the next statement terminator.
		/// </summary>
		private void AdvanceToNextStatementTerminator() {
			this.AdvanceToNextStatementTerminator(true);
		}

		/// <summary>
		/// Advances past the next statement terminator.
		/// </summary>
		/// <param name="movePast">Whether to move past the statement terminator.</param>
		private void AdvanceToNextStatementTerminator(bool movePast) {
			while (!this.IsAtEnd) {
				if ((this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) || (this.TokenIs(this.LookAheadToken, VBTokenID.Colon))) {
					if (movePast) {
						this.AdvanceToNext();
						this.AdvancePastTerminators();
					}
					break;
				}
				this.AdvanceToNext();
			}			
		}

		/// <summary>
		/// Returns whether the next two <see cref="IToken"/> objects match the specified IDs.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the next two <see cref="IToken"/> objects match the specified IDs; otherwise, <c>false</c>.
		/// </returns>
		private bool AreNextTwo(int firstTokenID, params int[] secondTokenIDs) {
			return (this.TokenIs(this.LookAheadToken, firstTokenID)) && (this.TokenIs(this.GetLookAheadToken(2), secondTokenIDs));
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
			AstNode currentBlock = (blockStack.Count > 0 ? (AstNode)((BlockData)blockStack.Peek()).Node : null);
		
			// If the node is a type declaration, add it to the Types list
			if (node is TypeDeclaration) 
				compilationUnit.Types.Add(node);
		
			if (currentBlock == null) {
				// Compilation unit
				if ((node is NamespaceDeclaration) || (node is TypeDeclaration)) {
					compilationUnit.NamespaceMembers.Add(node);
					return;
				}
			}
			else {
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
		/// <param name="tokenID">The ID of the token for the block.</param>
		/// <param name="node">The <see cref="AstNode"/> that is starting a block.</param>
		private void BlockStart(int tokenID, AstNode node) {
			// Push the block data on the stack
			BlockData data = new BlockData(tokenID, node);
			this.BlockAddChild(node);
			blockStack.Push(data);
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
		/// Returns whether the current <see cref="IToken"/> is the start of an array creation expression (after the type name).
		/// </summary>
		/// <returns>
		/// <c>true</c> if the current <see cref="IToken"/> is the start of an array creation expression (after the type name); otherwise, <c>false</c>.
		/// </returns>
		private bool IsArrayCreationExpression() {
			if (!this.TokenIs(this.LookAheadToken, VBTokenID.OpenParenthesis))
				return false;
			
			// Look for an open curly brace after matching pairs of parenthesis
			int curlyBraceLevel = 0;
			int parenthesisLevel = 1;
			this.StartPeek();
			try {
				this.Peek();  // Skip the open parenthesis
				while (!this.IsAtEnd) {						
					if (this.TokenIsLanguageChange(this.PeekToken))
						return false;
					switch (this.PeekToken.ID) {
						case VBTokenID.OpenCurlyBrace:
							curlyBraceLevel++;							
							break;
						case VBTokenID.CloseCurlyBrace:
							--curlyBraceLevel;							
							break;
						case VBTokenID.OpenParenthesis:
							parenthesisLevel++;							
							break;
						case VBTokenID.CloseParenthesis:
							if (--parenthesisLevel < 1) {
								this.Peek();
								return this.TokenIs(this.PeekToken, VBTokenID.OpenCurlyBrace);
							}
							break;
						case VBTokenID.LineTerminator:
							return false;
					}
					this.Peek();
				}
				return false;
			}
			finally {
				this.StopPeek();
			}
		}
				
		/// <summary>
		/// Returns whether the current <see cref="IToken"/> is the start of an array type modifier.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the current <see cref="IToken"/> is the start of an array type modifier; otherwise, <c>false</c>.
		/// </returns>
		private bool IsArrayTypeModifier() {
			return (this.TokenIs(this.LookAheadToken, VBTokenID.OpenParenthesis)) && (
				(this.TokenIs(this.GetLookAheadToken(2), VBTokenID.CloseParenthesis)) || (this.TokenIs(this.GetLookAheadToken(2), VBTokenID.Comma))
				);
		}
	
		/// <summary>
		/// Returns whether the current <see cref="IToken"/> is a statement block terminator.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the current <see cref="IToken"/> is a statement block terminator; otherwise, <c>false</c>.
		/// </returns>
		private bool IsBlockTerminator() {
			switch (this.LookAheadToken.ID) {
				case VBTokenID.Case:
				case VBTokenID.Catch:
				case VBTokenID.Else:
				case VBTokenID.ElseIf:
				case VBTokenID.Finally:
				case VBTokenID.Loop:
				case VBTokenID.Next:
					// Double check for language change
					if (this.TokenIs(this.LookAheadToken, this.LookAheadToken.ID))
						return true;					
					break;
				case VBTokenID.End:
					// Double check for language change
					if ((this.TokenIs(this.LookAheadToken, this.LookAheadToken.ID)) && (!this.IsEndStatement()))
						return true;					
					break;
			}
			return false;
		}
		
		/// <summary>
		/// Returns whether the current <see cref="IToken"/> is the start of a conditional expression.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the current <see cref="IToken"/> is the start of a conditional expression; otherwise, <c>false</c>.
		/// </returns>
		private bool IsConditionalExpression() {
			if (!this.AreNextTwo(VBTokenID.If, VBTokenID.OpenParenthesis))
				return false;
			
			// Look for a comma
			int curlyBraceLevel = 0;
			int parenthesisLevel = 1;
			this.StartPeek();
			try {
				this.Peek();  // Skip the If
				this.Peek();  // Skip the open parenthesis
				while (!this.IsAtEnd) {						
					if (this.TokenIsLanguageChange(this.PeekToken))
						return false;
					switch (this.PeekToken.ID) {
						case VBTokenID.OpenCurlyBrace:
							curlyBraceLevel++;							
							break;
						case VBTokenID.CloseCurlyBrace:
							--curlyBraceLevel;							
							break;
						case VBTokenID.OpenParenthesis:
							parenthesisLevel++;							
							break;
						case VBTokenID.CloseParenthesis:
							if (--parenthesisLevel < 1)
								return false;
							break;
						case VBTokenID.Comma:
							if (parenthesisLevel == 1)
								return true;
							break;
						case VBTokenID.LineTerminator:
							return false;
					}
					this.Peek();
				}
				return false;
			}
			finally {
				this.StopPeek();
			}
		}
		
		/// <summary>
		/// Returns whether the specified <see cref="IToken"/> is a contextual keyword.
		/// </summary>
		/// <param name="token">The <see cref="IToken"/> to examine.</param>
		/// <returns>
		/// <c>true</c> if the specified <see cref="IToken"/> is a Contextual keyword; otherwise, <c>false</c>.
		/// </returns>
		private bool IsContextualKeyword(IToken token) {
			if ((token.ID > VBTokenID.ContextualKeywordStart) && (token.ID < VBTokenID.ContextualKeywordEnd)) {
				// Double check for language change
				if (this.TokenIs(token, token.ID))
					return true;					
			}
			return false;
		}
			
		/// <summary>
		/// Returns whether the next two <see cref="IToken"/> objects are a dot followed by an identifier or keyword.
		/// </summary>
		/// <param name="allowXml">Whether to allow XML literals and attributes as well.</param>
		/// <returns>
		/// <c>true</c> if the next two <see cref="IToken"/> objects are a dot followed by an identifier or keyword; otherwise, <c>false</c>.
		/// </returns>
		private bool IsDotIdentifierOrKeyword(bool allowXml) {
			if (!this.TokenIs(this.LookAheadToken, VBTokenID.Dot))
				return false;
				
			IToken token = this.GetLookAheadToken(2);
			if ((this.IsIdentifier(token)) || (this.IsKeyword(token)))
				return true;

			if (allowXml)
				return (this.TokenIs(token, VBTokenID.XmlLiteral)) || (this.TokenIs(token, VBTokenID.XmlAttribute));
			else
				return false;
		}
		
		/// <summary>
		/// Returns whether the current <see cref="IToken"/> is a specified end.
		/// </summary>
		/// <param name="tokenID">The ID of the desired end token.</param>
		/// <returns>
		/// <c>true</c> if the current <see cref="IToken"/> is a specified end; otherwise, <c>false</c>.
		/// </returns>
		private bool IsEnd(int tokenID) {
			return (this.TokenIs(this.LookAheadToken, VBTokenID.End)) && (this.TokenIs(this.GetLookAheadToken(2), tokenID));
		}
		
		/// <summary>
		/// Returns whether the current <see cref="IToken"/> is an end of a non-statement.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the current <see cref="IToken"/> is an end of a non-statement; otherwise, <c>false</c>.
		/// </returns>
		private bool IsEndOfNonStatement() {
			return (this.TokenIs(this.LookAheadToken, VBTokenID.End)) && (this.TokenIs(this.GetLookAheadToken(2), new int[] { 
				VBTokenID.Sub, VBTokenID.Function, VBTokenID.Get, VBTokenID.Set, VBTokenID.Class, VBTokenID.Structure, 
				VBTokenID.Module, VBTokenID.Interface, VBTokenID.Namespace, VBTokenID.Operator, 
				VBTokenID.AddHandler, VBTokenID.RemoveHandler, VBTokenID.RaiseEvent, VBTokenID.Event }));
		}
		
		/// <summary>
		/// Returns whether the current <see cref="IToken"/> is an end statement.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the current <see cref="IToken"/> is an end statement; otherwise, <c>false</c>.
		/// </returns>
		private bool IsEndStatement() {
			return (this.TokenIs(this.LookAheadToken, VBTokenID.End)) && (
				(this.TokenIs(this.GetLookAheadToken(2), VBTokenID.LineTerminator)) || (this.TokenIs(this.GetLookAheadToken(2), VBTokenID.Colon)));
		}
		
		/// <summary>
		/// Returns whether the specified <see cref="IToken"/> is an identifier (or contextual keyword).
		/// </summary>
		/// <param name="token">The <see cref="IToken"/> to examine.</param>
		/// <returns>
		/// <c>true</c> if the specified <see cref="IToken"/> is an identifier (or contextual keyword); otherwise, <c>false</c>.
		/// </returns>
		private bool IsIdentifier(IToken token) {
			return (this.TokenIs(token, VBTokenID.Identifier)) ||
				(this.IsContextualKeyword(token));
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
			if ((token.ID > VBTokenID.KeywordStart) && (token.ID < VBTokenID.KeywordEnd)) {
				// Double check for language change
				if (this.TokenIs(token, token.ID))
					return true;					
			}
			return false;
		}
			
		/// <summary>
		/// Returns whether the look-ahead <see cref="IToken"/> comes immediately after the previous token.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the look-ahead <see cref="IToken"/> comes immediately after the previous token; otherwise, <c>false</c>.
		/// </returns>
		private bool IsLookAheadTokenSequential() {
			return (this.Token.EndOffset == this.LookAheadToken.StartOffset);
		}
	
		/// <summary>
		/// Returns whether the current <see cref="IToken"/> is the start of a variable declarator.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the current <see cref="IToken"/> is the start of a variable declarator; otherwise, <c>false</c>.
		/// </returns>
		private bool IsVariableDeclarator() {
			// Must start with an identifier
			if (!this.IsIdentifier(this.LookAheadToken))
				return false;
			
			this.StartPeek();
			try {
				this.Peek();  // Skip the identifier
				while (!this.IsAtEnd) {						
					if (this.TokenIsLanguageChange(this.PeekToken))
						return false;
					switch (this.PeekToken.ID) {
						case VBTokenID.QuestionMark:
						case VBTokenID.OpenParenthesis:
							// Allow
							break;
						case VBTokenID.As:
						case VBTokenID.Equality:
						case VBTokenID.Comma:
						case VBTokenID.CloseParenthesis:
							return true;
						default:
							return false;
					}
					this.Peek();
				}
				return false;
			}
			finally {
				this.StopPeek();
			}
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
			compilationUnit.SourceLanguage = DotNetLanguage.VB;
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
			if (this.LexicalParser is VBRecursiveDescentLexicalParser) {
				Comment[] comments = ((VBRecursiveDescentLexicalParser)this.LexicalParser).ReapComments(nodes.ParentNode.TextRange);
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
			if (this.LexicalParser is VBRecursiveDescentLexicalParser)
				return ((VBRecursiveDescentLexicalParser)this.LexicalParser).ReapDocumentationComments();
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
		/// Matches a <c>SimpleIdentifier</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>SimpleIdentifier</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>.
		/// </remarks>
		protected virtual bool MatchSimpleIdentifier() {
			if (this.TokenIs(this.LookAheadToken, VBTokenID.Identifier)) {
				this.Match(VBTokenID.Identifier);
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Aggregate)) {
				this.Match(VBTokenID.Aggregate);
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Ascending)) {
				this.Match(VBTokenID.Ascending);
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.By)) {
				this.Match(VBTokenID.By);
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Descending)) {
				this.Match(VBTokenID.Descending);
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Distinct)) {
				this.Match(VBTokenID.Distinct);
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Equals)) {
				this.Match(VBTokenID.Equals);
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.From)) {
				this.Match(VBTokenID.From);
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Group)) {
				this.Match(VBTokenID.Group);
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Into)) {
				this.Match(VBTokenID.Into);
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Join)) {
				this.Match(VBTokenID.Join);
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Order)) {
				this.Match(VBTokenID.Order);
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Out)) {
				this.Match(VBTokenID.Out);
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Skip)) {
				this.Match(VBTokenID.Skip);
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Take)) {
				this.Match(VBTokenID.Take);
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Where)) {
				this.Match(VBTokenID.Where);
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>NonQueryIdentifier</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>NonQueryIdentifier</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>.
		/// </remarks>
		protected virtual bool MatchNonQueryIdentifier(out QualifiedIdentifier identifier) {
			identifier = new QualifiedIdentifier(this.LookAheadTokenText, this.LookAheadToken.TextRange);
			this.Match(VBTokenID.Identifier);
			// Remove escape chars if they were used
			if ((identifier.Text.Length > 2) && (identifier.Text.StartsWith("[")) && (identifier.Text.EndsWith("]")))
				identifier.Text = identifier.Text.Substring(1, identifier.Text.Length - 2);
			return true;
		}

		/// <summary>
		/// Matches a <c>Identifier</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>Identifier</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>.
		/// </remarks>
		protected virtual bool MatchIdentifier(out QualifiedIdentifier identifier) {
			identifier = new QualifiedIdentifier(this.LookAheadTokenText, this.LookAheadToken.TextRange);
			if (this.TokenIs(this.LookAheadToken, VBTokenID.Identifier)) {
				this.Match(VBTokenID.Identifier);
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Aggregate)) {
				this.Match(VBTokenID.Aggregate);
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Ascending)) {
				this.Match(VBTokenID.Ascending);
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.By)) {
				this.Match(VBTokenID.By);
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Descending)) {
				this.Match(VBTokenID.Descending);
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Distinct)) {
				this.Match(VBTokenID.Distinct);
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Equals)) {
				this.Match(VBTokenID.Equals);
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.From)) {
				this.Match(VBTokenID.From);
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Group)) {
				this.Match(VBTokenID.Group);
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Into)) {
				this.Match(VBTokenID.Into);
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Join)) {
				this.Match(VBTokenID.Join);
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Order)) {
				this.Match(VBTokenID.Order);
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Out)) {
				this.Match(VBTokenID.Out);
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Skip)) {
				this.Match(VBTokenID.Skip);
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Take)) {
				this.Match(VBTokenID.Take);
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Where)) {
				this.Match(VBTokenID.Where);
			}
			else
				return false;
			// Remove escape chars if they were used
			if ((identifier.Text.Length > 2) && (identifier.Text.StartsWith("[")) && (identifier.Text.EndsWith("]")))
				identifier.Text = identifier.Text.Substring(1, identifier.Text.Length - 2);
			return true;
		}

		/// <summary>
		/// Matches a <c>IdentifierOrKeyword</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>IdentifierOrKeyword</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>.
		/// The non-terminal can start with: this.IsKeyword().
		/// </remarks>
		protected virtual bool MatchIdentifierOrKeyword(out QualifiedIdentifier identifier) {
			identifier = null;
			if (this.IsKeyword()) {
				identifier = new QualifiedIdentifier(this.TokenText, this.Token.TextRange);
				this.AdvanceToNext();
				return true;
			}
			if (!this.MatchIdentifier(out identifier))
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>Modifier</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>Modifier</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Public</c>, <c>Protected</c>, <c>Friend</c>, <c>Private</c>, <c>Default</c>, <c>Dim</c>, <c>MustInherit</c>, <c>MustOverride</c>, <c>Narrowing</c>, <c>NotInheritable</c>, <c>NotOverridable</c>, <c>Overloads</c>, <c>Overridable</c>, <c>Overrides</c>, <c>Partial</c>, <c>ReadOnly</c>, <c>Shadows</c>, <c>Shared</c>, <c>Static</c>, <c>Widening</c>, <c>WithEvents</c>, <c>WriteOnly</c>.
		/// </remarks>
		protected virtual bool MatchModifier(out Modifiers modifier) {
			modifier = Modifiers.None;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.Public)) {
				if (this.Match(VBTokenID.Public)) {
					modifier = Modifiers.Public;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Protected)) {
				if (this.Match(VBTokenID.Protected)) {
					modifier = Modifiers.Family;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Friend)) {
				if (this.Match(VBTokenID.Friend)) {
					modifier = Modifiers.Assembly;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Private)) {
				if (this.Match(VBTokenID.Private)) {
					modifier = Modifiers.Private;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Default)) {
				if (this.Match(VBTokenID.Default)) {
					modifier = Modifiers.Default;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Dim)) {
				if (this.Match(VBTokenID.Dim)) {
					modifier = Modifiers.Dim;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.MustInherit)) {
				if (this.Match(VBTokenID.MustInherit)) {
					modifier = Modifiers.Abstract;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.MustOverride)) {
				if (this.Match(VBTokenID.MustOverride)) {
					modifier = Modifiers.Abstract;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Narrowing)) {
				if (this.Match(VBTokenID.Narrowing)) {
					modifier = Modifiers.Narrowing;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.NotInheritable)) {
				if (this.Match(VBTokenID.NotInheritable)) {
					modifier = Modifiers.Final;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.NotOverridable)) {
				if (this.Match(VBTokenID.NotOverridable)) {
					modifier = Modifiers.Final;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Overloads)) {
				if (this.Match(VBTokenID.Overloads)) {
					modifier = Modifiers.Overloads;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Overridable)) {
				if (this.Match(VBTokenID.Overridable)) {
					modifier = Modifiers.Virtual;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Overrides)) {
				if (this.Match(VBTokenID.Overrides)) {
					modifier = Modifiers.Override;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Partial)) {
				if (this.Match(VBTokenID.Partial)) {
					modifier = Modifiers.Partial;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.ReadOnly)) {
				if (this.Match(VBTokenID.ReadOnly)) {
					modifier = Modifiers.ReadOnly;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Shadows)) {
				if (this.Match(VBTokenID.Shadows)) {
					modifier = Modifiers.New;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Shared)) {
				if (this.Match(VBTokenID.Shared)) {
					modifier = Modifiers.Static;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Static)) {
				if (this.Match(VBTokenID.Static)) {
					modifier = Modifiers.Static;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Widening)) {
				if (this.Match(VBTokenID.Widening)) {
					modifier = Modifiers.Widening;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.WithEvents)) {
				if (this.Match(VBTokenID.WithEvents)) {
					modifier = Modifiers.WithEvents;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.WriteOnly)) {
				if (this.Match(VBTokenID.WriteOnly)) {
					modifier = Modifiers.WriteOnly;
				}
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>Modifiers</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>Modifiers</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Public</c>, <c>Protected</c>, <c>Friend</c>, <c>Private</c>, <c>Default</c>, <c>Dim</c>, <c>MustInherit</c>, <c>MustOverride</c>, <c>Narrowing</c>, <c>NotInheritable</c>, <c>NotOverridable</c>, <c>Overloads</c>, <c>Overridable</c>, <c>Overrides</c>, <c>Partial</c>, <c>ReadOnly</c>, <c>Shadows</c>, <c>Shared</c>, <c>Static</c>, <c>Widening</c>, <c>WithEvents</c>, <c>WriteOnly</c>.
		/// </remarks>
		protected virtual bool MatchModifiers(out Modifiers modifiers) {
			modifiers = Modifiers.None;
			Modifiers singleModifier;
			while (this.IsInMultiMatchSet(0, this.LookAheadToken)) {
				if (!this.MatchModifier(out singleModifier))
					return false;
				else {
					modifiers |= singleModifier;
				}
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>QualifiedIdentifier</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>QualifiedIdentifier</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Global</c>.
		/// </remarks>
		protected virtual bool MatchQualifiedIdentifier(out QualifiedIdentifier identifier) {
			identifier = null;
			int startOffset = this.LookAheadToken.StartOffset;
			identifierStringBuilder.Length = 0;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.Global)) {
				if (this.Match(VBTokenID.Global)) {
					identifierStringBuilder.Append("Global");
				}
				if (!this.Match(VBTokenID.Dot))
					return false;
				else {
					identifierStringBuilder.Append(".");
				}
				if (!this.MatchIdentifierOrKeyword(out identifier))
					return false;
				else {
					identifierStringBuilder.Append(identifier.Text);
				}
			}
			else if (this.IsInMultiMatchSet(1, this.LookAheadToken)) {
				if (this.MatchSimpleIdentifier()) {
					identifierStringBuilder.Append(this.TokenText);
				}
			}
			else
				return false;
			while (this.IsDotIdentifierOrKeyword(false)) {
				if (!this.Match(VBTokenID.Dot))
					return false;
				this.AdvanceToNext();
				identifierStringBuilder.Append(".");
				identifierStringBuilder.Append(this.TokenText);
			}
			identifier = new QualifiedIdentifier(identifierStringBuilder.ToString(), new TextRange(startOffset, this.Token.EndOffset));
			return true;
		}

		/// <summary>
		/// Matches a <c>TypeParameterList</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>TypeParameterList</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: this.AreNextTwo(VBTokenID.OpenParenthesis, VBTokenID.Of).
		/// </remarks>
		protected virtual bool MatchTypeParameterList(out AstNodeList typeParameterList) {
			TypeReference typeParameter;
			typeParameterList = new AstNodeList(null);
			if (!this.Match(VBTokenID.OpenParenthesis))
				return false;
			if (!this.Match(VBTokenID.Of))
				return false;
			if (!this.MatchTypeParameter(out typeParameter))
				return false;
			else {
				typeParameterList.Add(typeParameter);
			}
			while (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
				if (!this.Match(VBTokenID.Comma))
					return false;
				if (!this.MatchTypeParameter(out typeParameter))
					return false;
				else {
					typeParameterList.Add(typeParameter);
				}
			}
			this.Match(VBTokenID.CloseParenthesis);
			return true;
		}

		/// <summary>
		/// Matches a <c>TypeParameter</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>TypeParameter</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>In</c>.
		/// </remarks>
		protected virtual bool MatchTypeParameter(out TypeReference typeParameter) {
			typeParameter = null;
			if (((this.TokenIs(this.LookAheadToken, VBTokenID.Out)) || (this.TokenIs(this.LookAheadToken, VBTokenID.In)))) {
				if (this.TokenIs(this.LookAheadToken, VBTokenID.In)) {
					if (!this.Match(VBTokenID.In))
						return false;
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.Out)) {
					if (!this.Match(VBTokenID.Out))
						return false;
				}
				else
					return false;
			}
			if (!this.MatchSimpleIdentifier())
				return false;
			typeParameter = new TypeReference(this.TokenText, this.Token.TextRange);
			typeParameter.IsGenericParameter = true;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.As)) {
				if (!this.Match(VBTokenID.As))
					return false;
				if (this.IsInMultiMatchSet(2, this.LookAheadToken)) {
					if (!this.MatchConstraint(typeParameter))
						return false;
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.OpenCurlyBrace)) {
					this.Match(VBTokenID.OpenCurlyBrace);
					if (!this.MatchConstraint(typeParameter))
						return false;
					while (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
						if (!this.Match(VBTokenID.Comma))
							return false;
						if (!this.MatchConstraint(typeParameter))
							return false;
					}
					this.Match(VBTokenID.CloseCurlyBrace);
				}
				else
					return false;
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>Constraint</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>Constraint</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Global</c>, <c>New</c>, <c>Object</c>, <c>Single</c>, <c>Double</c>, <c>Decimal</c>, <c>Boolean</c>, <c>Date</c>, <c>Char</c>, <c>String</c>, <c>Byte</c>, <c>SByte</c>, <c>UShort</c>, <c>Short</c>, <c>UInteger</c>, <c>Integer</c>, <c>ULong</c>, <c>Long</c>.
		/// </remarks>
		protected virtual bool MatchConstraint(TypeReference typeParameter) {
			if (this.IsInMultiMatchSet(3, this.LookAheadToken)) {
				TypeReference typeConstraint;
				if (!this.MatchTypeName(out typeConstraint, false))
					return false;
				else {
					if (typeParameter != null)
						typeParameter.GenericTypeParameterConstraints.Add(typeConstraint);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.New)) {
				if (!this.Match(VBTokenID.New))
					return false;
				else {
					if (typeParameter != null)
						typeParameter.HasGenericParameterDefaultConstructorConstraint = true;
				}
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>Attributes</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>Attributes</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>LessThan</c>.
		/// </remarks>
		protected virtual bool MatchAttributes(IAstNodeList attributeSections) {
			while (this.TokenIs(this.LookAheadToken, VBTokenID.LessThan)) {
				AttributeSection attributeSection = new AttributeSection();
				attributeSection.StartOffset = this.LookAheadToken.StartOffset;
				if (!this.Match(VBTokenID.LessThan))
					return false;
				if (!this.MatchAttributeList(attributeSection))
					return false;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
					this.Match(VBTokenID.LineTerminator);
				}
				this.Match(VBTokenID.GreaterThan);
				attributeSection.EndOffset = this.Token.EndOffset;
				attributeSections.Add(attributeSection);
				if (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
					this.Match(VBTokenID.Comma);
				}
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>AttributeList</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>AttributeList</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Global</c>, <c>Object</c>, <c>Single</c>, <c>Double</c>, <c>Decimal</c>, <c>Boolean</c>, <c>Date</c>, <c>Char</c>, <c>String</c>, <c>Byte</c>, <c>SByte</c>, <c>UShort</c>, <c>Short</c>, <c>UInteger</c>, <c>Integer</c>, <c>ULong</c>, <c>Long</c>.
		/// </remarks>
		protected virtual bool MatchAttributeList(AttributeSection attributeSection) {
			if (!this.MatchAttribute(attributeSection))
				return false;
			while ((this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) && ((this.IsIdentifier(this.GetLookAheadToken(2)))) || (this.TokenIs(this.GetLookAheadToken(2), VBTokenID.Global))) {
				if (!this.Match(VBTokenID.Comma))
					return false;
				if (!this.MatchAttribute(attributeSection))
					return false;
			}
			if (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
				this.Match(VBTokenID.Comma);
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>Attribute</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>Attribute</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Global</c>, <c>Object</c>, <c>Single</c>, <c>Double</c>, <c>Decimal</c>, <c>Boolean</c>, <c>Date</c>, <c>Char</c>, <c>String</c>, <c>Byte</c>, <c>SByte</c>, <c>UShort</c>, <c>Short</c>, <c>UInteger</c>, <c>Integer</c>, <c>ULong</c>, <c>Long</c>.
		/// </remarks>
		protected virtual bool MatchAttribute(AttributeSection attributeSection) {
			int startOffset = this.LookAheadToken.StartOffset;
			string target = null;
			if (this.AreNextTwoIdentifierAnd(VBTokenID.Colon)) {
				if (!this.MatchSimpleIdentifier())
					return false;
				else {
					target = this.TokenText;
				}
				if (!this.Match(VBTokenID.Colon))
					return false;
			}
			TypeReference typeReference = null;
			if (!this.MatchTypeName(out typeReference, false))
				return false;
			ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast.Attribute attribute = new ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast.Attribute(typeReference);
			attribute.StartOffset = startOffset;
			attribute.Target = target;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.OpenParenthesis)) {
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
			if (!this.Match(VBTokenID.OpenParenthesis))
				return false;
			if ((this.IsIdentifier(this.LookAheadToken)) || (this.IsKeyword(this.LookAheadToken)) || ((this.IsInMultiMatchSet(4, this.LookAheadToken) || (this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression()) || ((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis })))))) {
				if (!this.MatchAttributeArgument(attribute))
					return false;
				while (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
					if (!this.Match(VBTokenID.Comma))
						return false;
					if (!this.MatchAttributeArgument(attribute))
						return false;
				}
			}
			if (!this.Match(VBTokenID.CloseParenthesis))
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>AttributeArgument</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>AttributeArgument</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Global</c>, <c>OpenParenthesis</c>, <c>OpenCurlyBrace</c>, <c>New</c>, <c>XmlLiteral</c>, <c>Object</c>, <c>Single</c>, <c>Double</c>, <c>Decimal</c>, <c>Boolean</c>, <c>Date</c>, <c>Char</c>, <c>String</c>, <c>Byte</c>, <c>SByte</c>, <c>UShort</c>, <c>Short</c>, <c>UInteger</c>, <c>Integer</c>, <c>ULong</c>, <c>Long</c>, <c>Sub</c>, <c>Function</c>, <c>Addition</c>, <c>Subtraction</c>, <c>Not</c>, <c>CType</c>, <c>StringLiteral</c>, <c>MyBase</c>, <c>Me</c>, <c>DecimalIntegerLiteral</c>, <c>Mid</c>, <c>True</c>, <c>False</c>, <c>HexadecimalIntegerLiteral</c>, <c>OctalIntegerLiteral</c>, <c>FloatingPointLiteral</c>, <c>CharacterLiteral</c>, <c>DateLiteral</c>, <c>Nothing</c>, <c>AddressOf</c>, <c>GetTypeKeyword</c>, <c>TypeOf</c>, <c>GetXmlNamespace</c>, <c>MyClass</c>, <c>DirectCast</c>, <c>TryCast</c>, <c>IIf</c>, <c>CBool</c>, <c>CByte</c>, <c>CChar</c>, <c>CDate</c>, <c>CDec</c>, <c>CDbl</c>, <c>CInt</c>, <c>CLng</c>, <c>CObj</c>, <c>CSByte</c>, <c>CShort</c>, <c>CSng</c>, <c>CStr</c>, <c>CUInt</c>, <c>CULng</c>, <c>CUShort</c>.
		/// The non-terminal can start with: this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression().
		/// The non-terminal can start with: (this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) &amp;&amp; (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis })).
		/// </remarks>
		protected virtual bool MatchAttributeArgument(ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast.Attribute attribute) {
			string name = null;
			Expression expression;
			int startOffset = this.LookAheadToken.StartOffset;
			
			if (((this.IsIdentifier(this.LookAheadToken)) || (this.IsKeyword(this.LookAheadToken))) && (this.TokenIs(this.GetLookAheadToken(2), VBTokenID.ColonEquals))) {
				name = this.LookAheadTokenText;
				this.AdvanceToNext();  // Identifier/keyword
				this.AdvanceToNext();  // :=
			}
			if (!this.MatchExpression(out expression))
				return false;
			attribute.Arguments.Add(new AttributeArgument(name, expression, new TextRange(startOffset, this.Token.EndOffset)));
			return true;
		}

		/// <summary>
		/// Matches a <c>CompilationUnit</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>CompilationUnit</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Public</c>, <c>Protected</c>, <c>Friend</c>, <c>Private</c>, <c>Default</c>, <c>Dim</c>, <c>MustInherit</c>, <c>MustOverride</c>, <c>Narrowing</c>, <c>NotInheritable</c>, <c>NotOverridable</c>, <c>Overloads</c>, <c>Overridable</c>, <c>Overrides</c>, <c>Partial</c>, <c>ReadOnly</c>, <c>Shadows</c>, <c>Shared</c>, <c>Static</c>, <c>Widening</c>, <c>WithEvents</c>, <c>WriteOnly</c>, <c>LessThan</c>, <c>LineTerminator</c>, <c>Colon</c>, <c>Option</c>, <c>Imports</c>, <c>Namespace</c>, <c>Enum</c>, <c>Class</c>, <c>Structure</c>, <c>Module</c>, <c>Interface</c>, <c>Delegate</c>.
		/// </remarks>
		protected virtual bool MatchCompilationUnit() {
			blockStack = new Stack();
			nestingLevel = 0;
			compilationUnit = new CompilationUnit();
			compilationUnit.SourceLanguage = DotNetLanguage.VB;
			compilationUnit.StartOffset = this.LookAheadToken.StartOffset;
			while (((this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) || (this.TokenIs(this.LookAheadToken, VBTokenID.Colon)) || (this.TokenIs(this.LookAheadToken, VBTokenID.Option)))) {
				if (((this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) || (this.TokenIs(this.LookAheadToken, VBTokenID.Colon)))) {
					this.MatchStatementTerminator();
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.Option)) {
					if (!this.MatchOptionsStatement()) {
						// Error recovery:  Go to the next Option keyword, Imports keyword, or token that starts a AttributeStatement or NamespaceMemberDeclaration
						while (!this.IsAtEnd) {
							if ((this.TokenIs(this.LookAheadToken, VBTokenID.Option)) || (this.TokenIs(this.LookAheadToken, VBTokenID.Imports)) ||
							(((this.TokenIs(this.LookAheadToken, VBTokenID.LessThan)) || (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) || (this.TokenIs(this.LookAheadToken, VBTokenID.Colon)))) || (this.IsInMultiMatchSet(5, this.LookAheadToken)))
								break;
							this.AdvanceToNextStatementTerminator();
						}
					}
				}
				else
					return false;
			}
			while (((this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) || (this.TokenIs(this.LookAheadToken, VBTokenID.Colon)) || (this.TokenIs(this.LookAheadToken, VBTokenID.Imports)))) {
				if (((this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) || (this.TokenIs(this.LookAheadToken, VBTokenID.Colon)))) {
					this.MatchStatementTerminator();
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.Imports)) {
					if (!this.MatchImportsStatement()) {
						// Error recovery:  Go to the next Imports keyword, or token that starts a AttributeStatement or NamespaceMemberDeclaration
						while (!this.IsAtEnd) {
							if ((this.TokenIs(this.LookAheadToken, VBTokenID.Imports)) ||
							(((this.TokenIs(this.LookAheadToken, VBTokenID.LessThan)) || (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) || (this.TokenIs(this.LookAheadToken, VBTokenID.Colon)))) || (this.IsInMultiMatchSet(5, this.LookAheadToken)))
								break;
							this.AdvanceToNextStatementTerminator();
						}
					}
				}
				else
					return false;
			}
			// Reap comments
			this.ReapComments(compilationUnit.Comments, false);
			while (((this.TokenIs(this.LookAheadToken, VBTokenID.LessThan)) || (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) || (this.TokenIs(this.LookAheadToken, VBTokenID.Colon)))) {
				if (!this.MatchAttributesStatement())
					return false;
			}
			bool errorReported = false;
			while (!this.IsAtEnd) {
				if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
					// Skip over blank lines
					this.AdvancePastTerminators();
					continue;
				}
				
				// Check for using statements in the wrong location
				if (this.TokenIs(this.LookAheadToken, VBTokenID.Option)) {
					this.ReportSyntaxError(this.LookAheadToken.TextRange, AssemblyInfo.Instance.Resources.GetString("SemanticParserError_OptionBeforeDeclarations"));
					this.AdvanceToNextStatementTerminator();
					continue;
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.Imports)) {
					this.ReportSyntaxError(this.LookAheadToken.TextRange, AssemblyInfo.Instance.Resources.GetString("SemanticParserError_ImportsBeforeDeclarations"));
					this.AdvanceToNextStatementTerminator();
					continue;
				}
				else if (this.IsInMultiMatchSet(5, this.LookAheadToken)) {
					errorReported = false;
					while (this.IsInMultiMatchSet(6, this.LookAheadToken)) {
						if (!this.MatchNamespaceMemberDeclaration())
							return false;
					}
				}
				else {
					// Error recovery:  Advance to the next statement terminator since nothing was matched
					if (!errorReported) {
						this.ReportSyntaxError(AssemblyInfo.Instance.Resources.GetString("SemanticParserError_NamespaceMemberDeclarationExpected"));
						errorReported = true;
					}
					this.AdvanceToNextStatementTerminator();
				}
			}
			compilationUnit.EndOffset = this.LookAheadToken.EndOffset;
			blockStack = null;
			
			// Reap comments
			this.ReapComments(compilationUnit.Comments, false);
			
			// Get the comment and region text ranges
			if (this.LexicalParser is VBRecursiveDescentLexicalParser) {
				compilationUnit.DocumentationCommentTextRanges = ((VBRecursiveDescentLexicalParser)this.LexicalParser).DocumentationCommentTextRanges;
				compilationUnit.RegionTextRanges = ((VBRecursiveDescentLexicalParser)this.LexicalParser).RegionTextRanges;
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>StatementTerminator</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>StatementTerminator</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>LineTerminator</c>, <c>Colon</c>.
		/// </remarks>
		protected virtual bool MatchStatementTerminator() {
			if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
				if (!this.Match(VBTokenID.LineTerminator)) {
					this.AdvanceToNextStatementTerminator();
				}
				else {
					this.AdvancePastTerminators();
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Colon)) {
				if (!this.Match(VBTokenID.Colon)) {
					this.AdvanceToNextStatementTerminator();
				}
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>AttributesStatement</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>AttributesStatement</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>LessThan</c>, <c>LineTerminator</c>, <c>Colon</c>.
		/// </remarks>
		protected virtual bool MatchAttributesStatement() {
			if (this.TokenIs(this.LookAheadToken, VBTokenID.LessThan)) {
				this.MatchAttributes(compilationUnit.GlobalAttributeSections);
			}
			this.MatchStatementTerminator();
			return true;
		}

		/// <summary>
		/// Matches a <c>OptionsStatement</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>OptionsStatement</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Option</c>.
		/// </remarks>
		protected virtual bool MatchOptionsStatement() {
			this.Match(VBTokenID.Option);
			if (!this.MatchSimpleIdentifier()) {
				this.AdvanceToNextStatementTerminator();
			}
			switch (this.TokenText.ToLower()) {
				case "explicit":
				switch (this.LookAheadTokenText.ToLower()) {
					case "on":  // Default
					compilationUnit.OptionExplicit = "On";
					this.AdvanceToNext();
					break;
					case "off":
					compilationUnit.OptionExplicit = "Off";
					this.AdvanceToNext();
					break;
					default:
					if (!(((this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) || (this.TokenIs(this.LookAheadToken, VBTokenID.Colon))))) {
						this.ReportSyntaxError(this.Token.TextRange, AssemblyInfo.Instance.Resources.GetString("SemanticParserError_OptionExplicitSyntax"));
						this.AdvanceToNextStatementTerminator();
						return false;
					}
					break;
				}
				break;
				case "compare":
				switch (this.LookAheadTokenText.ToLower()) {
					case "binary":  // Default value
					compilationUnit.OptionCompare = "Binary";
					this.AdvanceToNext();
					break;
					case "text":
					compilationUnit.OptionCompare = "Text";
					this.AdvanceToNext();
					break;
					default:
					this.ReportSyntaxError(this.Token.TextRange, AssemblyInfo.Instance.Resources.GetString("SemanticParserError_OptionCompareSyntax"));
					this.AdvanceToNextStatementTerminator();
					return false;
				}
				break;
				case "strict":
				switch (this.LookAheadTokenText.ToLower()) {
					case "on":
					compilationUnit.OptionStrict = "On";
					this.AdvanceToNext();
					break;
					case "off":
					compilationUnit.OptionStrict = "Off";
					this.AdvanceToNext();
					break;
					default:
					if (!(((this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) || (this.TokenIs(this.LookAheadToken, VBTokenID.Colon))))) {
						this.ReportSyntaxError(this.Token.TextRange, AssemblyInfo.Instance.Resources.GetString("SemanticParserError_OptionStrictSyntax"));
						this.AdvanceToNextStatementTerminator();
						return false;
					}
					break;
				}
				break;
				case "infer":
				switch (this.LookAheadTokenText.ToLower()) {
					case "on":
					compilationUnit.OptionInfer = "On";
					this.AdvanceToNext();
					break;
					case "off":
					compilationUnit.OptionInfer = "Off";
					this.AdvanceToNext();
					break;
					default:
					if (!(((this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) || (this.TokenIs(this.LookAheadToken, VBTokenID.Colon))))) {
						this.ReportSyntaxError(this.Token.TextRange, AssemblyInfo.Instance.Resources.GetString("SemanticParserError_OptionInferSyntax"));
						this.AdvanceToNextStatementTerminator();
						return false;
					}
					break;
				}
				break;
				default:
				this.ReportSyntaxError(this.Token.TextRange, AssemblyInfo.Instance.Resources.GetString("SemanticParserError_OptionFollowedBy"));
				this.AdvanceToNextStatementTerminator();
				return false;
			}
			this.MatchStatementTerminator();
			return true;
		}

		/// <summary>
		/// Matches a <c>ImportsStatement</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ImportsStatement</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Imports</c>.
		/// </remarks>
		protected virtual bool MatchImportsStatement() {
			this.Match(VBTokenID.Imports);
			this.ReapDocumentationComments();
			
			if (compilationUnit.UsingDirectives == null)
				compilationUnit.UsingDirectives = new UsingDirectiveSection();
			if (compilationUnit.UsingDirectives.Directives.Count == 0)
				compilationUnit.UsingDirectives.StartOffset = this.Token.StartOffset;
			if (!this.MatchImportsClause())
				return false;
			while (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
				if (!this.Match(VBTokenID.Comma))
					return false;
				if (!this.MatchImportsClause())
					return false;
			}
			compilationUnit.UsingDirectives.EndOffset = this.Token.EndOffset;
			this.MatchStatementTerminator();
			return true;
		}

		/// <summary>
		/// Matches a <c>ImportsClause</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ImportsClause</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>XmlLiteral</c>.
		/// </remarks>
		protected virtual bool MatchImportsClause() {
			if (this.TokenIs(this.LookAheadToken, VBTokenID.XmlLiteral)) {
				if (!this.Match(VBTokenID.XmlLiteral))
					return false;
				UsingDirective usingDirective = new UsingDirective(new TextRange(this.Token.StartOffset, this.Token.EndOffset));
				
				// Reap comments
				this.ReapComments(usingDirective.Comments, false);
				
				usingDirective.NamespaceName = new QualifiedIdentifier(this.TokenText, this.Token.TextRange);
				compilationUnit.UsingDirectives.Directives.Add(usingDirective);
			}
			else if (this.IsInMultiMatchSet(1, this.LookAheadToken)) {
				if (this.AreNextTwoIdentifierAnd(VBTokenID.Equality)) {
					this.MatchImportsAliasClause();
				}
				else {
					this.MatchImportsNamespaceClause();
				}
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>ImportsAliasClause</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ImportsAliasClause</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>.
		/// </remarks>
		protected virtual bool MatchImportsAliasClause() {
			int startOffset = this.Token.StartOffset;
			this.MatchSimpleIdentifier();
			string alias = this.TokenText;
			QualifiedIdentifier namespaceName;
			this.Match(VBTokenID.Equality);
			if (!this.MatchQualifiedIdentifier(out namespaceName))
				return false;
			UsingDirective usingDirective = new UsingDirective(new TextRange(startOffset, this.Token.EndOffset));
			
			// Reap comments
			this.ReapComments(usingDirective.Comments, false);
			
			usingDirective.NamespaceName = namespaceName;
			usingDirective.Alias = alias;
			compilationUnit.UsingDirectives.Directives.Add(usingDirective);
			return true;
		}

		/// <summary>
		/// Matches a <c>ImportsNamespaceClause</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ImportsNamespaceClause</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Global</c>.
		/// </remarks>
		protected virtual bool MatchImportsNamespaceClause() {
			int startOffset = this.Token.StartOffset;
			QualifiedIdentifier namespaceName;
			if (!this.MatchQualifiedIdentifier(out namespaceName))
				return false;
			UsingDirective usingDirective = new UsingDirective(new TextRange(startOffset, this.Token.EndOffset));
			
			// Reap comments
			this.ReapComments(usingDirective.Comments, false);
			
			usingDirective.NamespaceName = namespaceName;
			compilationUnit.UsingDirectives.Directives.Add(usingDirective);
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
			if (!this.Match(VBTokenID.Namespace))
				return false;
			namespaceDeclaration.StartOffset = this.Token.StartOffset;
			namespaceDeclaration.BlockStartOffset = namespaceDeclaration.StartOffset;
			QualifiedIdentifier name;
			if (!this.MatchQualifiedIdentifier(out name)) {
				// Error recovery:  Go to the next statement terminator
				this.AdvanceToNextStatementTerminator();
				return false;
			}
			namespaceDeclaration.Name = name;
			this.BlockStart(VBTokenID.Namespace, namespaceDeclaration);
			int namespaceNestingLevel = nestingLevel;
			this.ReapDocumentationComments();
			this.MatchStatementTerminator();
			while (this.IsInMultiMatchSet(6, this.LookAheadToken)) {
				if (!this.MatchNamespaceMemberDeclaration()) {
					// Error recovery:  Go to the next statement terminator
					this.AdvanceToNextStatementTerminator(false);
				}
			}
			this.BlockEnd();
			this.AdvanceToNextEnd(VBTokenID.Namespace);
			namespaceDeclaration.BlockEndOffset = this.Token.EndOffset;
			namespaceDeclaration.EndOffset = this.Token.EndOffset;
			
			// Reap comments
			this.ReapDocumentationComments();
			this.ReapComments(namespaceDeclaration.Comments, false);
			this.MatchStatementTerminator();
			return true;
		}

		/// <summary>
		/// Matches a <c>NamespaceMemberDeclaration</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>NamespaceMemberDeclaration</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Public</c>, <c>Protected</c>, <c>Friend</c>, <c>Private</c>, <c>Default</c>, <c>Dim</c>, <c>MustInherit</c>, <c>MustOverride</c>, <c>Narrowing</c>, <c>NotInheritable</c>, <c>NotOverridable</c>, <c>Overloads</c>, <c>Overridable</c>, <c>Overrides</c>, <c>Partial</c>, <c>ReadOnly</c>, <c>Shadows</c>, <c>Shared</c>, <c>Static</c>, <c>Widening</c>, <c>WithEvents</c>, <c>WriteOnly</c>, <c>LessThan</c>, <c>LineTerminator</c>, <c>Colon</c>, <c>Namespace</c>, <c>Enum</c>, <c>Class</c>, <c>Structure</c>, <c>Module</c>, <c>Interface</c>, <c>Delegate</c>.
		/// </remarks>
		protected virtual bool MatchNamespaceMemberDeclaration() {
			if (((this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) || (this.TokenIs(this.LookAheadToken, VBTokenID.Colon)))) {
				this.MatchStatementTerminator();
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Namespace)) {
				if (!this.MatchNamespaceDeclaration())
					return false;
			}
			else if (this.IsInMultiMatchSet(7, this.LookAheadToken)) {
				AstNodeList attributeSections = new AstNodeList(null);
				Modifiers modifiers = Modifiers.None;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.LessThan)) {
					this.MatchAttributes(attributeSections);
				}
				int startOffset = this.LookAheadToken.StartOffset;
				if (this.IsInMultiMatchSet(0, this.LookAheadToken)) {
					if (!this.MatchModifiers(out modifiers))
						return false;
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
		/// The non-terminal can start with: <c>Enum</c>, <c>Class</c>, <c>Structure</c>, <c>Module</c>, <c>Interface</c>, <c>Delegate</c>.
		/// </remarks>
		protected virtual bool MatchTypeDeclaration(int startOffset, AstNodeList attributeSections, Modifiers modifiers) {
			if (this.TokenIs(this.LookAheadToken, VBTokenID.Module)) {
				if (!this.MatchModuleDeclaration(startOffset, attributeSections, modifiers))
					return false;
			}
			else if (this.IsInMultiMatchSet(8, this.LookAheadToken)) {
				if (!this.MatchNonModuleDeclaration(startOffset, attributeSections, modifiers))
					return false;
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>NonModuleDeclaration</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>NonModuleDeclaration</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Enum</c>, <c>Class</c>, <c>Structure</c>, <c>Interface</c>, <c>Delegate</c>.
		/// </remarks>
		protected virtual bool MatchNonModuleDeclaration(int startOffset, AstNodeList attributeSections, Modifiers modifiers) {
			if (this.TokenIs(this.LookAheadToken, VBTokenID.Enum)) {
				if (!this.MatchEnumDeclaration(startOffset, attributeSections, modifiers))
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Delegate)) {
				if (!this.MatchDelegateDeclaration(startOffset, attributeSections, modifiers))
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Class)) {
				if (!this.MatchClassDeclaration(startOffset, attributeSections, modifiers))
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Structure)) {
				if (!this.MatchStructureDeclaration(startOffset, attributeSections, modifiers))
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Interface)) {
				if (!this.MatchInterfaceDeclaration(startOffset, attributeSections, modifiers))
					return false;
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>TypeName</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>TypeName</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Global</c>, <c>Object</c>, <c>Single</c>, <c>Double</c>, <c>Decimal</c>, <c>Boolean</c>, <c>Date</c>, <c>Char</c>, <c>String</c>, <c>Byte</c>, <c>SByte</c>, <c>UShort</c>, <c>Short</c>, <c>UInteger</c>, <c>Integer</c>, <c>ULong</c>, <c>Long</c>.
		/// </remarks>
		protected virtual bool MatchTypeName(out TypeReference typeReference, bool canBeUnbound) {
			int[] arrayRanks;
			if (!this.MatchNonArrayTypeName(out typeReference, canBeUnbound))
				return false;
			while (this.IsArrayTypeModifier()) {
				if (!this.MatchArrayTypeModifier(out arrayRanks))
					return false;
				else {
					typeReference.ArrayRanks = arrayRanks;
				}
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>NonArrayTypeName</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>NonArrayTypeName</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Global</c>, <c>Object</c>, <c>Single</c>, <c>Double</c>, <c>Decimal</c>, <c>Boolean</c>, <c>Date</c>, <c>Char</c>, <c>String</c>, <c>Byte</c>, <c>SByte</c>, <c>UShort</c>, <c>Short</c>, <c>UInteger</c>, <c>Integer</c>, <c>ULong</c>, <c>Long</c>.
		/// </remarks>
		protected virtual bool MatchNonArrayTypeName(out TypeReference typeReference, bool canBeUnbound) {
			typeReference = null;
			if (this.IsInMultiMatchSet(9, this.LookAheadToken)) {
				QualifiedIdentifier identifier;
				AstNodeList typeArgumentList = null;
				if (!this.MatchQualifiedIdentifier(out identifier))
					return false;
				if (this.AreNextTwo(VBTokenID.OpenParenthesis, VBTokenID.Of)) {
					if (!this.Match(VBTokenID.OpenParenthesis))
						return false;
					if (!this.Match(VBTokenID.Of))
						return false;
					if (this.IsInMultiMatchSet(3, this.LookAheadToken)) {
						if (!this.MatchTypeArgumentList(out typeArgumentList))
							return false;
					}
					else if ((canBeUnbound) && ((this.TokenIs(this.LookAheadToken, VBTokenID.OpenParenthesis)) || (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)))) {
						// TypeArityList
						while (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
							if (this.Match(VBTokenID.Comma)) {
								typeArgumentList.Add(new TypeReference(null, TextRange.Deleted));  // Add a null type reference
							}
						}
					}
					else
						return false;
					if (!this.Match(VBTokenID.CloseParenthesis))
						return false;
				}
				typeReference = new TypeReference(identifier.Text, identifier.TextRange);
				if (typeArgumentList != null)
					typeReference.GenericTypeArguments.AddRange(typeArgumentList.ToArray());
			}
			else if (this.IsInMultiMatchSet(10, this.LookAheadToken)) {
				if (!this.MatchBuiltInTypeName(out typeReference))
					return false;
			}
			else
				return false;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.QuestionMark)) {
				if (!this.Match(VBTokenID.QuestionMark))
					return false;
				TypeReference nullableTypeReference = typeReference;
				typeReference = new TypeReference("System.Nullable", new TextRange(nullableTypeReference.StartOffset, this.Token.EndOffset));
				typeReference.GenericTypeArguments.Add(nullableTypeReference);
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>BuiltInTypeName</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>BuiltInTypeName</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Object</c>, <c>Single</c>, <c>Double</c>, <c>Decimal</c>, <c>Boolean</c>, <c>Date</c>, <c>Char</c>, <c>String</c>, <c>Byte</c>, <c>SByte</c>, <c>UShort</c>, <c>Short</c>, <c>UInteger</c>, <c>Integer</c>, <c>ULong</c>, <c>Long</c>.
		/// </remarks>
		protected virtual bool MatchBuiltInTypeName(out TypeReference typeReference) {
			typeReference = null;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.Object)) {
				if (!this.Match(VBTokenID.Object))
					return false;
				else {
					typeReference = new TypeReference("System.Object", this.Token.TextRange);
				}
			}
			else if (this.IsInMultiMatchSet(11, this.LookAheadToken)) {
				if (!this.MatchIntegralTypeName(out typeReference))
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Single)) {
				if (!this.Match(VBTokenID.Single))
					return false;
				else {
					typeReference = new TypeReference("System.Single", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Double)) {
				if (!this.Match(VBTokenID.Double))
					return false;
				else {
					typeReference = new TypeReference("System.Double", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Decimal)) {
				if (!this.Match(VBTokenID.Decimal))
					return false;
				else {
					typeReference = new TypeReference("System.Decimal", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Boolean)) {
				if (!this.Match(VBTokenID.Boolean))
					return false;
				else {
					typeReference = new TypeReference("System.Boolean", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Date)) {
				if (!this.Match(VBTokenID.Date))
					return false;
				else {
					typeReference = new TypeReference("System.DateTime", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Char)) {
				if (!this.Match(VBTokenID.Char))
					return false;
				else {
					typeReference = new TypeReference("System.Char", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.String)) {
				if (!this.Match(VBTokenID.String))
					return false;
				else {
					typeReference = new TypeReference("System.String", this.Token.TextRange);
				}
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>TypeImplementsClause</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>TypeImplementsClause</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Implements</c>.
		/// </remarks>
		protected virtual bool MatchTypeImplementsClause(out AstNodeList implementsList) {
			implementsList = null;
			if (!this.Match(VBTokenID.Implements))
				return false;
			if (!this.MatchImplements(out implementsList))
				return false;
			this.MatchStatementTerminator();
			return true;
		}

		/// <summary>
		/// Matches a <c>Implements</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>Implements</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Global</c>, <c>Object</c>, <c>Single</c>, <c>Double</c>, <c>Decimal</c>, <c>Boolean</c>, <c>Date</c>, <c>Char</c>, <c>String</c>, <c>Byte</c>, <c>SByte</c>, <c>UShort</c>, <c>Short</c>, <c>UInteger</c>, <c>Integer</c>, <c>ULong</c>, <c>Long</c>.
		/// </remarks>
		protected virtual bool MatchImplements(out AstNodeList implementsList) {
			TypeReference typeReference;
			implementsList = new AstNodeList(null);
			if (!this.MatchNonArrayTypeName(out typeReference, false))
				return false;
			else {
				implementsList.Add(typeReference);
			}
			while (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
				if (!this.Match(VBTokenID.Comma))
					return false;
				if (!this.MatchNonArrayTypeName(out typeReference, false))
					return false;
				else {
					implementsList.Add(typeReference);
				}
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>IntegralTypeName</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>IntegralTypeName</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Byte</c>, <c>SByte</c>, <c>UShort</c>, <c>Short</c>, <c>UInteger</c>, <c>Integer</c>, <c>ULong</c>, <c>Long</c>.
		/// </remarks>
		protected virtual bool MatchIntegralTypeName(out TypeReference typeReference) {
			typeReference = null;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.Byte)) {
				if (!this.Match(VBTokenID.Byte))
					return false;
				else {
					typeReference = new TypeReference("System.Byte", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.SByte)) {
				if (!this.Match(VBTokenID.SByte))
					return false;
				else {
					typeReference = new TypeReference("System.SByte", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.UShort)) {
				if (!this.Match(VBTokenID.UShort))
					return false;
				else {
					typeReference = new TypeReference("System.UInt16", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Short)) {
				if (!this.Match(VBTokenID.Short))
					return false;
				else {
					typeReference = new TypeReference("System.Int16", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.UInteger)) {
				if (!this.Match(VBTokenID.UInteger))
					return false;
				else {
					typeReference = new TypeReference("System.UInt32", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Integer)) {
				if (!this.Match(VBTokenID.Integer))
					return false;
				else {
					typeReference = new TypeReference("System.Int32", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.ULong)) {
				if (!this.Match(VBTokenID.ULong))
					return false;
				else {
					typeReference = new TypeReference("System.UInt64", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Long)) {
				if (!this.Match(VBTokenID.Long))
					return false;
				else {
					typeReference = new TypeReference("System.Int64", this.Token.TextRange);
				}
			}
			else
				return false;
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
			if (!this.Match(VBTokenID.Enum))
				return false;
			QualifiedIdentifier identifier;
			if (!this.MatchIdentifier(out identifier))
				return false;
			// Default to public access
			if (!AstNode.IsAccessSpecified(modifiers))
				modifiers |= Modifiers.Public;
			
			EnumerationDeclaration enumerationDeclaration = new EnumerationDeclaration(modifiers, identifier);
			enumerationDeclaration.Documentation = this.ReapDocumentationComments();
			enumerationDeclaration.StartOffset = startOffset;
			enumerationDeclaration.BlockStartOffset = startOffset;
			enumerationDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
			if (this.TokenIs(this.LookAheadToken, VBTokenID.As)) {
				if (!this.Match(VBTokenID.As))
					return false;
				TypeReference typeReference;
				if (!this.MatchNonArrayTypeName(out typeReference, false))
					return false;
				else {
					enumerationDeclaration.BaseTypes.Add(typeReference);
				}
			}
			this.MatchStatementTerminator();
			int enumerationNestingLevel = nestingLevel;
			
			this.BlockStart(VBTokenID.Enum, enumerationDeclaration);
			bool errorReported = false;
			while (!this.IsAtEnd) {
				if (this.IsEnd(VBTokenID.Enum))
					break;
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
					// Skip over line terminators
					this.AdvancePastTerminators();
				}
				else if (this.IsInMultiMatchSet(12, this.LookAheadToken)) {
					errorReported = false;
					this.MatchEnumMemberDeclaration();
				}
				else {
					// Error recovery:  Advance to the next statement terminator since nothing was matched
					if (!errorReported) {
						this.ReportSyntaxError(AssemblyInfo.Instance.Resources.GetString("SemanticParserError_EnumerationMemberDeclarationExpected"));
						errorReported = true;
					}
					this.AdvanceToNextStatementTerminator();
				}
			}
			
			this.BlockEnd();
			this.AdvanceToNextEnd(VBTokenID.Enum);
			enumerationDeclaration.BlockEndOffset = this.Token.EndOffset;
			enumerationDeclaration.EndOffset = this.Token.EndOffset;
			
			// Reap comments
			this.ReapDocumentationComments();
			this.ReapComments(enumerationDeclaration.Comments, false);
			return true;
		}

		/// <summary>
		/// Matches a <c>EnumMemberDeclaration</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>EnumMemberDeclaration</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>LessThan</c>.
		/// </remarks>
		protected virtual bool MatchEnumMemberDeclaration() {
			AstNodeList attributeSections = new AstNodeList(null);
			if (this.TokenIs(this.LookAheadToken, VBTokenID.LessThan)) {
				this.MatchAttributes(attributeSections);
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
			if (this.TokenIs(this.LookAheadToken, VBTokenID.Equality)) {
				if (!this.Match(VBTokenID.Equality))
					return false;
				if (!this.MatchExpression(out initializer))
					return false;
				else {
					memberDeclaration.Initializer = initializer;
				}
			}
			memberDeclaration.EndOffset = this.Token.EndOffset;
			this.MatchStatementTerminator();
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
			if (!this.Match(VBTokenID.Class))
				return false;
			QualifiedIdentifier identifier;
			if (!this.MatchIdentifier(out identifier))
				return false;
			// Default to public access
			if (!AstNode.IsAccessSpecified(modifiers))
				modifiers |= Modifiers.Public;
			
			ClassDeclaration classDeclaration = new ClassDeclaration(modifiers, identifier);
			classDeclaration.Documentation = this.ReapDocumentationComments();
			classDeclaration.StartOffset = startOffset;
			classDeclaration.BlockStartOffset = startOffset;
			classDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
			AstNodeList typeParameterList = null;
			AstNodeList implementsList = null;
			if (((this.AreNextTwo(VBTokenID.OpenParenthesis, VBTokenID.Of)))) {
				if (!this.MatchTypeParameterList(out typeParameterList))
					return false;
			}
			if (typeParameterList != null)
				classDeclaration.GenericTypeArguments.AddRange(typeParameterList.ToArray());
			this.MatchStatementTerminator();
			if (this.TokenIs(this.LookAheadToken, VBTokenID.Inherits)) {
				this.Match(VBTokenID.Inherits);
				TypeReference typeReference;
				if (!this.MatchNonArrayTypeName(out typeReference, false)) {
					// Error recovery:  Go to the next statement terminator
					this.AdvanceToNextStatementTerminator();
				}
				else {
					// Ensure that generic type parameters are marked properly
					this.MarkGenericParameters(typeParameterList, typeReference, false);
					
					classDeclaration.BaseTypes.Add(typeReference);
				}
				this.MatchStatementTerminator();
			}
			while (this.TokenIs(this.LookAheadToken, VBTokenID.Implements)) {
				if (!this.MatchTypeImplementsClause(out implementsList))
					return false;
				else {
					if (implementsList != null) {
						// Ensure that generic type parameters are marked properly
						foreach (TypeReference typeReference in implementsList)
							this.MarkGenericParameters(typeParameterList, typeReference, false);
						
						classDeclaration.BaseTypes.AddRange(implementsList.ToArray());
					}
				}
			}
			int classNestingLevel = nestingLevel;
			
			this.BlockStart(VBTokenID.Class, classDeclaration);
			bool errorReported = false;
			while (!this.IsAtEnd) {
				if (this.IsEnd(VBTokenID.Class))
					break;
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
					// Skip over line terminators
					this.AdvancePastTerminators();
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.LessThan) || this.IsInMultiMatchSet(13, this.LookAheadToken) || this.IsInMultiMatchSet(14, this.LookAheadToken)) {
					errorReported = false;
					attributeSections = new AstNodeList(null);
					if (this.TokenIs(this.LookAheadToken, VBTokenID.LessThan)) {
						this.MatchAttributes(attributeSections);
					}
					startOffset = this.LookAheadToken.StartOffset;
					modifiers = Modifiers.None;
					if (this.IsInMultiMatchSet(0, this.LookAheadToken)) {
						if (!this.MatchModifiers(out modifiers))
							return false;
					}
					this.MatchStructureMemberDeclaration(classDeclaration, startOffset, attributeSections, modifiers);
				}
				else {
					// Error recovery:  Advance to the next statement terminator since nothing was matched
					if (!errorReported) {
						this.ReportSyntaxError(AssemblyInfo.Instance.Resources.GetString("SemanticParserError_ClassMemberDeclarationExpected"));
						errorReported = true;
					}
					this.AdvanceToNextStatementTerminator();
				}
			}
			
			this.BlockEnd();
			this.AdvanceToNextEnd(VBTokenID.Class);
			classDeclaration.BlockEndOffset = this.Token.EndOffset;
			classDeclaration.EndOffset = this.Token.EndOffset;
			
			// Reap comments
			this.ReapDocumentationComments();
			this.ReapComments(classDeclaration.Comments, false);
			this.MatchStatementTerminator();
			return true;
		}

		/// <summary>
		/// Matches a <c>StructureDeclaration</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>StructureDeclaration</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Structure</c>.
		/// </remarks>
		protected virtual bool MatchStructureDeclaration(int startOffset, AstNodeList attributeSections, Modifiers modifiers) {
			if (!this.Match(VBTokenID.Structure))
				return false;
			QualifiedIdentifier identifier;
			if (!this.MatchIdentifier(out identifier))
				return false;
			// Default to public access
			if (!AstNode.IsAccessSpecified(modifiers))
				modifiers |= Modifiers.Public;
			
			StructureDeclaration structureDeclaration = new StructureDeclaration(modifiers, identifier);
			structureDeclaration.Documentation = this.ReapDocumentationComments();
			structureDeclaration.StartOffset = startOffset;
			structureDeclaration.BlockStartOffset = startOffset;
			structureDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
			AstNodeList typeParameterList = null;
			AstNodeList implementsList = null;
			if (((this.AreNextTwo(VBTokenID.OpenParenthesis, VBTokenID.Of)))) {
				if (!this.MatchTypeParameterList(out typeParameterList))
					return false;
			}
			if (typeParameterList != null)
				structureDeclaration.GenericTypeArguments.AddRange(typeParameterList.ToArray());
			this.MatchStatementTerminator();
			while (this.TokenIs(this.LookAheadToken, VBTokenID.Implements)) {
				if (!this.MatchTypeImplementsClause(out implementsList))
					return false;
				else {
					if (implementsList != null)
						structureDeclaration.BaseTypes.AddRange(implementsList.ToArray());
				}
			}
			int structureNestingLevel = nestingLevel;
			
			this.BlockStart(VBTokenID.Structure, structureDeclaration);
			bool errorReported = false;
			while (!this.IsAtEnd) {
				if (this.IsEnd(VBTokenID.Structure))
					break;
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
					// Skip over line terminators
					this.AdvancePastTerminators();
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.LessThan) || this.IsInMultiMatchSet(13, this.LookAheadToken) || this.IsInMultiMatchSet(14, this.LookAheadToken)) {
					errorReported = false;
					attributeSections = new AstNodeList(null);
					if (this.TokenIs(this.LookAheadToken, VBTokenID.LessThan)) {
						this.MatchAttributes(attributeSections);
					}
					startOffset = this.LookAheadToken.StartOffset;
					modifiers = Modifiers.None;
					if (this.IsInMultiMatchSet(0, this.LookAheadToken)) {
						if (!this.MatchModifiers(out modifiers))
							return false;
					}
					this.MatchStructureMemberDeclaration(structureDeclaration, startOffset, attributeSections, modifiers);
				}
				else {
					// Error recovery:  Advance to the next statement terminator since nothing was matched
					if (!errorReported) {
						this.ReportSyntaxError(AssemblyInfo.Instance.Resources.GetString("SemanticParserError_StructureMemberDeclarationExpected"));
						errorReported = true;
					}
					this.AdvanceToNextStatementTerminator();
				}
			}
			
			this.BlockEnd();
			this.AdvanceToNextEnd(VBTokenID.Structure);
			structureDeclaration.BlockEndOffset = this.Token.EndOffset;
			structureDeclaration.EndOffset = this.Token.EndOffset;
			
			// Reap comments
			this.ReapDocumentationComments();
			this.ReapComments(structureDeclaration.Comments, false);
			this.MatchStatementTerminator();
			return true;
		}

		/// <summary>
		/// Matches a <c>StructureMemberDeclaration</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>StructureMemberDeclaration</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Enum</c>, <c>Class</c>, <c>Structure</c>, <c>Const</c>, <c>Event</c>, <c>Custom</c>, <c>Sub</c>, <c>Function</c>, <c>Declare</c>, <c>Property</c>, <c>Operator</c>, <c>Interface</c>, <c>Delegate</c>.
		/// </remarks>
		protected virtual bool MatchStructureMemberDeclaration(TypeDeclaration parentTypeDeclaration, int startOffset, AstNodeList attributeSections, Modifiers modifiers) {
			// Build a list of generic type arguments for the parent type... will add to this collection if a generic method
			AstNodeList scopedGenericTypeArguments = new AstNodeList(null);
			if ((parentTypeDeclaration.GenericTypeArguments != null) && (parentTypeDeclaration.GenericTypeArguments.Count > 0))
				scopedGenericTypeArguments.AddRange(parentTypeDeclaration.GenericTypeArguments.ToArray());
			if (this.IsInMultiMatchSet(8, this.LookAheadToken)) {
				if (!this.MatchNonModuleDeclaration(startOffset, attributeSections, modifiers))
					return false;
			}
			else if (this.IsInMultiMatchSet(1, this.LookAheadToken)) {
				// VariableMemberDeclaration
				FieldDeclaration fieldDeclaration = new FieldDeclaration(modifiers);
				fieldDeclaration.Documentation = this.ReapDocumentationComments();
				fieldDeclaration.StartOffset = startOffset;
				fieldDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
				if (!this.MatchVariableDeclarator(scopedGenericTypeArguments, fieldDeclaration))
					return false;
				while (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
					if (!this.Match(VBTokenID.Comma))
						return false;
					if (!this.MatchVariableDeclarator(scopedGenericTypeArguments, fieldDeclaration))
						return false;
				}
				fieldDeclaration.EndOffset = this.Token.EndOffset;
				this.BlockAddChild(fieldDeclaration);
				this.ReapDocumentationComments();
				this.MatchStatementTerminator();
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Const)) {
				// ConstantMemberDeclaration
				if (!this.Match(VBTokenID.Const))
					return false;
				FieldDeclaration constantDeclaration = new FieldDeclaration(modifiers);
				constantDeclaration.Documentation = this.ReapDocumentationComments();
				constantDeclaration.StartOffset = startOffset;
				constantDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
				if (!this.MatchConstantDeclarator(constantDeclaration))
					return false;
				while (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
					if (!this.Match(VBTokenID.Comma))
						return false;
					if (!this.MatchConstantDeclarator(constantDeclaration))
						return false;
				}
				constantDeclaration.EndOffset = this.Token.EndOffset;
				this.BlockAddChild(constantDeclaration);
				this.ReapDocumentationComments();
				this.MatchStatementTerminator();
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Event)) {
				// RegularEventMemberDeclaration (via EventMemberDeclaration)
				if (!this.Match(VBTokenID.Event))
					return false;
				QualifiedIdentifier identifier;
				AstNodeList parameterList = null;
				TypeReference typeReference = null;
				AstNodeList implementsList = null;
				if (!this.MatchIdentifier(out identifier))
					return false;
				if (((this.TokenIs(this.LookAheadToken, VBTokenID.OpenParenthesis)) || (this.TokenIs(this.LookAheadToken, VBTokenID.As)))) {
					if (!this.MatchParametersOrType(scopedGenericTypeArguments, out parameterList, out typeReference))
						return false;
					else {
						this.MarkGenericParameters(scopedGenericTypeArguments, typeReference, true);
					}
				}
				if (this.TokenIs(this.LookAheadToken, VBTokenID.Implements)) {
					if (!this.MatchImplementsClause(out implementsList))
						return false;
				}
				// Default to public access
				if (!AstNode.IsAccessSpecified(modifiers))
					modifiers |= Modifiers.Public;
				
				EventDeclaration eventDeclaration = new EventDeclaration(modifiers, identifier);
				eventDeclaration.Documentation = this.ReapDocumentationComments();
				eventDeclaration.StartOffset = startOffset;
				eventDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
				eventDeclaration.EventType = typeReference;
				if (parameterList != null)
					eventDeclaration.Parameters.AddRange(parameterList.ToArray());
				if (implementsList != null)
					eventDeclaration.ImplementedMembers.AddRange(implementsList.ToArray());
				eventDeclaration.EndOffset = this.Token.EndOffset;
				this.BlockAddChild(eventDeclaration);
				
				// Reap comments
				this.ReapDocumentationComments();
				this.ReapComments(eventDeclaration.Comments, false);
				this.MatchStatementTerminator();
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Custom)) {
				// CustomEventMemberDeclaration
				if (!this.Match(VBTokenID.Custom))
					return false;
				if (!this.Match(VBTokenID.Event))
					return false;
				QualifiedIdentifier identifier;
				TypeReference typeReference;
				AstNodeList implementsList = null;
				if (!this.MatchIdentifier(out identifier))
					return false;
				if (!this.Match(VBTokenID.As))
					return false;
				if (!this.MatchTypeName(out typeReference, false))
					return false;
				else {
					this.MarkGenericParameters(scopedGenericTypeArguments, typeReference, true);
				}
				if (this.TokenIs(this.LookAheadToken, VBTokenID.Implements)) {
					if (!this.MatchImplementsClause(out implementsList))
						return false;
				}
				// Default to public access
				if (!AstNode.IsAccessSpecified(modifiers))
					modifiers |= Modifiers.Public;
				
				EventDeclaration eventDeclaration = new EventDeclaration(modifiers, identifier);
				eventDeclaration.Documentation = this.ReapDocumentationComments();
				eventDeclaration.StartOffset = startOffset;
				eventDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
				eventDeclaration.EventType = typeReference;
				if (implementsList != null)
					eventDeclaration.ImplementedMembers.AddRange(implementsList.ToArray());
				
				attributeSections = new AstNodeList(null);
				if (!this.MatchStatementTerminator())
					return false;
				while (this.IsInMultiMatchSet(15, this.LookAheadToken)) {
					if (this.TokenIs(this.LookAheadToken, VBTokenID.LessThan)) {
						this.MatchAttributes(attributeSections);
					}
					if (this.TokenIs(this.LookAheadToken, VBTokenID.AddHandler)) {
						// AddHandlerDeclaration
						AstNodeList parameterList = null;
						Statement block = null;
						
						AccessorDeclaration accessorDeclaration = new AccessorDeclaration();
						accessorDeclaration.StartOffset = this.LookAheadToken.StartOffset;
						if (!this.Match(VBTokenID.AddHandler))
							return false;
						if (!this.Match(VBTokenID.OpenParenthesis))
							return false;
						if (!this.MatchParameterList(scopedGenericTypeArguments, out parameterList))
							return false;
						if (parameterList != null)
							accessorDeclaration.Parameters.AddRange(parameterList.ToArray());
						if (!this.Match(VBTokenID.CloseParenthesis))
							return false;
						if (!this.Match(VBTokenID.LineTerminator))
							return false;
						if ((this.IsInMultiMatchSet(16, this.LookAheadToken) || (this.IsEndStatement()) || ((this.AreNextTwoIdentifierAnd(VBTokenID.Colon)) || (this.AreNextTwo(VBTokenID.DecimalIntegerLiteral, VBTokenID.Colon))) || (this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression()) || ((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis }))))) {
							if (!this.MatchBlock(out block))
								return false;
						}
						this.AdvanceToNextEnd(VBTokenID.AddHandler);
						accessorDeclaration.BlockStatement = block as BlockStatement;
						accessorDeclaration.EndOffset = this.Token.EndOffset;
						eventDeclaration.AddAccessor = accessorDeclaration;
						this.ReapDocumentationComments();
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.RemoveHandler)) {
						// RemoveHandlerDeclaration
						AstNodeList parameterList = null;
						Statement block = null;
						
						AccessorDeclaration accessorDeclaration = new AccessorDeclaration();
						accessorDeclaration.StartOffset = this.LookAheadToken.StartOffset;
						if (!this.Match(VBTokenID.RemoveHandler))
							return false;
						if (!this.Match(VBTokenID.OpenParenthesis))
							return false;
						if (!this.MatchParameterList(scopedGenericTypeArguments, out parameterList))
							return false;
						if (parameterList != null)
							accessorDeclaration.Parameters.AddRange(parameterList.ToArray());
						if (!this.Match(VBTokenID.CloseParenthesis))
							return false;
						if (!this.Match(VBTokenID.LineTerminator))
							return false;
						if ((this.IsInMultiMatchSet(16, this.LookAheadToken) || (this.IsEndStatement()) || ((this.AreNextTwoIdentifierAnd(VBTokenID.Colon)) || (this.AreNextTwo(VBTokenID.DecimalIntegerLiteral, VBTokenID.Colon))) || (this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression()) || ((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis }))))) {
							if (!this.MatchBlock(out block))
								return false;
						}
						this.AdvanceToNextEnd(VBTokenID.RemoveHandler);
						accessorDeclaration.BlockStatement = block as BlockStatement;
						accessorDeclaration.EndOffset = this.Token.EndOffset;
						eventDeclaration.RemoveAccessor = accessorDeclaration;
						this.ReapDocumentationComments();
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.RaiseEvent)) {
						// RaiseEventDeclaration
						AstNodeList parameterList = null;
						Statement block = null;
						
						AccessorDeclaration accessorDeclaration = new AccessorDeclaration();
						accessorDeclaration.StartOffset = this.LookAheadToken.StartOffset;
						if (!this.Match(VBTokenID.RaiseEvent))
							return false;
						if (!this.Match(VBTokenID.OpenParenthesis))
							return false;
						if (!this.MatchParameterList(scopedGenericTypeArguments, out parameterList))
							return false;
						if (parameterList != null)
							accessorDeclaration.Parameters.AddRange(parameterList.ToArray());
						if (!this.Match(VBTokenID.CloseParenthesis))
							return false;
						if (!this.Match(VBTokenID.LineTerminator))
							return false;
						if ((this.IsInMultiMatchSet(16, this.LookAheadToken) || (this.IsEndStatement()) || ((this.AreNextTwoIdentifierAnd(VBTokenID.Colon)) || (this.AreNextTwo(VBTokenID.DecimalIntegerLiteral, VBTokenID.Colon))) || (this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression()) || ((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis }))))) {
							if (!this.MatchBlock(out block))
								return false;
						}
						this.AdvanceToNextEnd(VBTokenID.RaiseEvent);
						accessorDeclaration.BlockStatement = block as BlockStatement;
						accessorDeclaration.EndOffset = this.Token.EndOffset;
						eventDeclaration.RaiseEventAccessor = accessorDeclaration;
						this.ReapDocumentationComments();
					}
					else
						return false;
				}
				this.AdvanceToNextEnd(VBTokenID.Event);
				eventDeclaration.EndOffset = this.Token.EndOffset;
				this.BlockAddChild(eventDeclaration);
				
				// Reap comments
				this.ReapDocumentationComments();
				this.ReapComments(eventDeclaration.Comments, false);
			}
			else if (((this.TokenIs(this.LookAheadToken, VBTokenID.Sub)) || (this.TokenIs(this.LookAheadToken, VBTokenID.Function)))) {
				// ConstructorMemberDeclaration and SubDeclaration/MustOverrideSubDeclaration/FunctionDeclaration/MustOverrideFunctionDeclaration (via MethodMemberDeclaration/MethodDeclaration)
				// Default to public access
				if (!AstNode.IsAccessSpecified(modifiers))
					modifiers |= Modifiers.Public;
				
				bool isFunction = false;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.Sub)) {
					if (!this.Match(VBTokenID.Sub))
						return false;
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.Function)) {
					if (!this.Match(VBTokenID.Function))
						return false;
					else {
						isFunction = true;
					}
				}
				else
					return false;
				if ((!isFunction) && (this.TokenIs(this.LookAheadToken, VBTokenID.New))) {
					if (!this.Match(VBTokenID.New))
						return false;
					ConstructorDeclaration constructorDeclaration = new ConstructorDeclaration(modifiers, null);
					constructorDeclaration.Documentation = this.ReapDocumentationComments();
					constructorDeclaration.StartOffset = startOffset;
					constructorDeclaration.BlockStartOffset = startOffset;
					constructorDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
					AstNodeList parameterList = null;
					Statement statement;
					if (this.TokenIs(this.LookAheadToken, VBTokenID.OpenParenthesis)) {
						if (!this.Match(VBTokenID.OpenParenthesis))
							return false;
						if (this.IsInMultiMatchSet(17, this.LookAheadToken)) {
							if (!this.MatchParameterList(scopedGenericTypeArguments, out parameterList))
								return false;
						}
						if (parameterList != null)
							constructorDeclaration.Parameters.AddRange(parameterList.ToArray());
						if (!this.Match(VBTokenID.CloseParenthesis))
							return false;
					}
					if (!this.Match(VBTokenID.LineTerminator))
						return false;
					if (!this.MatchBlock(out statement))
						return false;
					else {
						constructorDeclaration.Statements.Add(statement);
					}
					this.AdvanceToNextEnd(VBTokenID.Sub);
					constructorDeclaration.BlockEndOffset = this.Token.EndOffset;
					constructorDeclaration.EndOffset = this.Token.EndOffset;
					this.BlockAddChild(constructorDeclaration);
					
					// Reap comments
					this.ReapDocumentationComments();
					this.ReapComments(constructorDeclaration.Comments, false);
					this.MatchStatementTerminator();
				}
				else {
					QualifiedIdentifier name = null;
					AstNodeList typeParameterList = null;
					AstNodeList parameterList = null;
					TypeReference typeReference = null;
					AstNodeList implementsList = null;
					bool isAbstract = ((modifiers & Modifiers.Abstract) == Modifiers.Abstract);
					if (!isFunction) {
						if (!this.MatchSubSignature(scopedGenericTypeArguments, out name, out typeParameterList, out parameterList))
							return false;
					}
					if (isFunction) {
						if (!this.MatchFunctionSignature(scopedGenericTypeArguments, out name, out typeParameterList, out parameterList, out typeReference))
							return false;
					}
					MethodDeclaration methodDeclaration = new MethodDeclaration(modifiers, name);
					methodDeclaration.Documentation = this.ReapDocumentationComments();
					methodDeclaration.StartOffset = startOffset;
					methodDeclaration.BlockStartOffset = startOffset;
					methodDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
					if (typeParameterList != null)
						methodDeclaration.GenericTypeArguments.AddRange(typeParameterList.ToArray());
					if (parameterList != null)
						methodDeclaration.Parameters.AddRange(parameterList.ToArray());
					methodDeclaration.ReturnType = typeReference;
					
					// If an extension method...
					if (methodDeclaration.IsExtension) {
						// Ensure that the containing block is a static class declaration
						if ((parentTypeDeclaration != null) && (parentTypeDeclaration is ClassDeclaration) &&
						((((ClassDeclaration)parentTypeDeclaration).Modifiers & Modifiers.Static) == Modifiers.Static)) {
							// Add extension method attribute to parent type declaration
							if (!parentTypeDeclaration.IsExtension) {
								AttributeSection attributeSection = new AttributeSection();
								attributeSection.Attributes.Add(new ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast.Attribute(new TypeReference("System.Runtime.CompilerServices.Extension", TextRange.Deleted)));
								parentTypeDeclaration.AttributeSections.Add(attributeSection);
							}
						}
					}
					if (((this.TokenIs(this.LookAheadToken, VBTokenID.Implements)) || (this.TokenIs(this.LookAheadToken, VBTokenID.Handles)))) {
						if (this.TokenIs(this.LookAheadToken, VBTokenID.Handles)) {
							if (!this.Match(VBTokenID.Handles))
								return false;
							this.MatchEventHandlesList(out implementsList);
						}
						else if (this.TokenIs(this.LookAheadToken, VBTokenID.Implements)) {
							this.MatchImplementsClause(out implementsList);
						}
						else
							return false;
					}
					if (implementsList != null)
						methodDeclaration.ImplementedMembers.AddRange(implementsList.ToArray());
					if (!isAbstract) {
						Statement statement;
						if (!this.Match(VBTokenID.LineTerminator))
							return false;
						if (!this.MatchBlock(out statement))
							return false;
						else {
							methodDeclaration.Statements.Add(statement);
						}
						if (!this.Match(VBTokenID.End))
							return false;
						if (!isFunction) {
							if (!this.Match(VBTokenID.Sub))
								return false;
						}
						if (isFunction) {
							if (!this.Match(VBTokenID.Function))
								return false;
						}
					}
					methodDeclaration.BlockEndOffset = this.Token.EndOffset;
					methodDeclaration.EndOffset = this.Token.EndOffset;
					this.BlockAddChild(methodDeclaration);
					
					// Reap comments
					this.ReapDocumentationComments();
					this.ReapComments(methodDeclaration.Comments, false);
					this.MatchStatementTerminator();
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Declare)) {
				// MethodMemberDeclaration/ExternalMethodDeclaration start
				AstNodeList parameterList = null;
				bool isFunction = false;
				QualifiedIdentifier name = null;
				TypeReference typeReference = new TypeReference("System.Object", TextRange.Deleted);
				if (!this.Match(VBTokenID.Declare))
					return false;
				if (this.IsInMultiMatchSet(18, this.LookAheadToken)) {
					if (!this.MatchCharsetModifier())
						return false;
				}
				if (this.TokenIs(this.LookAheadToken, VBTokenID.Sub)) {
					if (!this.Match(VBTokenID.Sub))
						return false;
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.Function)) {
					if (!this.Match(VBTokenID.Function))
						return false;
					else {
						isFunction = true;
					}
				}
				else
					return false;
				if (!this.MatchIdentifier(out name))
					return false;
				if (!this.MatchLibraryClause())
					return false;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.Alias)) {
					if (!this.MatchAliasClause())
						return false;
				}
				if (this.TokenIs(this.LookAheadToken, VBTokenID.OpenParenthesis)) {
					if (!this.Match(VBTokenID.OpenParenthesis))
						return false;
					if (this.IsInMultiMatchSet(17, this.LookAheadToken)) {
						if (!this.MatchParameterList(scopedGenericTypeArguments, out parameterList))
							return false;
					}
					if (!this.Match(VBTokenID.CloseParenthesis))
						return false;
				}
				if ((isFunction) && (this.TokenIs(this.LookAheadToken, VBTokenID.As))) {
					AstNodeList typeAttributeSections = new AstNodeList(null);
					if (!this.Match(VBTokenID.As))
						return false;
					if (this.TokenIs(this.LookAheadToken, VBTokenID.LessThan)) {
						if (!this.MatchAttributes(typeAttributeSections))
							return false;
					}
					if (!this.MatchTypeName(out typeReference, false))
						return false;
					else {
						this.MarkGenericParameters(scopedGenericTypeArguments, typeReference, true);
					}
					if (attributeSections != null)
						typeReference.AttributeSections.AddRange(attributeSections.ToArray());
				}
				// Default to public access
				if (!AstNode.IsAccessSpecified(modifiers))
					modifiers |= Modifiers.Public;
				
				MethodDeclaration methodDeclaration = new MethodDeclaration(modifiers, name);
				methodDeclaration.Documentation = this.ReapDocumentationComments();
				methodDeclaration.StartOffset = startOffset;
				methodDeclaration.BlockStartOffset = startOffset;
				methodDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
				if (parameterList != null)
					methodDeclaration.Parameters.AddRange(parameterList.ToArray());
				methodDeclaration.ReturnType = typeReference;
				methodDeclaration.BlockEndOffset = this.Token.EndOffset;
				methodDeclaration.EndOffset = this.Token.EndOffset;
				this.BlockAddChild(methodDeclaration);
				
				// Reap comments
				this.ReapDocumentationComments();
				this.ReapComments(methodDeclaration.Comments, false);
				this.MatchStatementTerminator();
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Property)) {
				// PropertySignature/RegularPropertyMemberDeclaration/MustOverridePropertyMemberDeclaration/AutoPropertyMemberDeclaration (via PropertyMemberDeclaration)
				QualifiedIdentifier name;
				AstNodeList typeParameterList;
				AstNodeList parameterList;
				TypeReference typeReference = new TypeReference("System.Object", TextRange.Deleted);
				AstNodeList implementsList = null;
				bool isAbstract = ((modifiers & Modifiers.Abstract) == Modifiers.Abstract);
				bool isNewExpression = false;
				Expression initializer = null;
				if (!this.Match(VBTokenID.Property))
					return false;
				if (!this.MatchSubSignature(scopedGenericTypeArguments, out name, out typeParameterList, out parameterList))
					return false;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.As)) {
					if (!this.Match(VBTokenID.As))
						return false;
					AstNodeList typeRefAttributeSections = new AstNodeList(null);
					if (this.TokenIs(this.LookAheadToken, VBTokenID.LessThan)) {
						this.MatchAttributes(typeRefAttributeSections);
					}
					if (((this.TokenIs(this.LookAheadToken, VBTokenID.OpenCurlyBrace)) || (this.TokenIs(this.LookAheadToken, VBTokenID.New)))) {
						if (!this.MatchNewExpression(out initializer))
							return false;
						else {
							isNewExpression = true;
						}
					}
					else if (this.IsInMultiMatchSet(3, this.LookAheadToken)) {
						if (!this.MatchTypeName(out typeReference, false))
							return false;
						else {
							this.MarkGenericParameters(scopedGenericTypeArguments, typeReference, true);
						}
						if (typeRefAttributeSections != null)
							typeReference.AttributeSections.AddRange(typeRefAttributeSections.ToArray());
					}
					else
						return false;
				}
				if (!isNewExpression) {
					if (this.TokenIs(this.LookAheadToken, VBTokenID.Equality)) {
						this.Match(VBTokenID.Equality);
						if (!this.MatchExpression(out initializer))
							return false;
					}
				}
				
				// Default to public access
				if (!AstNode.IsAccessSpecified(modifiers))
					modifiers |= Modifiers.Public;
				
				PropertyDeclaration propertyDeclaration = new PropertyDeclaration(modifiers, name);
				propertyDeclaration.Documentation = this.ReapDocumentationComments();
				propertyDeclaration.StartOffset = startOffset;
				propertyDeclaration.BlockStartOffset = startOffset;
				propertyDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
				if (parameterList != null)
					propertyDeclaration.Parameters.AddRange(parameterList.ToArray());
				propertyDeclaration.Initializer = initializer;
				propertyDeclaration.ReturnType = typeReference;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.Implements)) {
					if (!this.MatchImplementsClause(out implementsList))
						return false;
				}
				if (implementsList != null)
					propertyDeclaration.ImplementedMembers.AddRange(implementsList.ToArray());
				if (!isAbstract) {
					if (!this.Match(VBTokenID.LineTerminator))
						return false;
					this.AdvancePastTerminators();
					
					if (this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Get, VBTokenID.Set })) {
						bool isGetAccessor = true;
						Statement block = null;
						Modifiers singleModifier;
						
						parameterList = null;
						AccessorDeclaration accessorDeclaration = new AccessorDeclaration();
						accessorDeclaration.StartOffset = this.LookAheadToken.StartOffset;
						if (this.TokenIs(this.LookAheadToken, VBTokenID.LessThan)) {
							if (!this.MatchAttributes(accessorDeclaration.AttributeSections))
								return false;
						}
						while (this.IsInMultiMatchSet(0, this.LookAheadToken)) {
							if (!this.MatchModifier(out singleModifier))
								return false;
							else {
								accessorDeclaration.Modifiers |= singleModifier;
							}
						}
						if (this.TokenIs(this.LookAheadToken, VBTokenID.Get)) {
							if (!this.Match(VBTokenID.Get))
								return false;
							else {
								isGetAccessor = true;
							}
						}
						else if (this.TokenIs(this.LookAheadToken, VBTokenID.Set)) {
							if (!this.Match(VBTokenID.Set))
								return false;
							else {
								isGetAccessor = false;
							}
						}
						else
							return false;
						if ((!isGetAccessor) && (this.TokenIs(this.LookAheadToken, VBTokenID.OpenParenthesis))) {
							if (!this.Match(VBTokenID.OpenParenthesis))
								return false;
							this.MatchParameterList(scopedGenericTypeArguments, out parameterList);
							if (parameterList != null)
								accessorDeclaration.Parameters.AddRange(parameterList.ToArray());
							this.Match(VBTokenID.CloseParenthesis);
						}
						this.Match(VBTokenID.LineTerminator);
						if ((this.IsInMultiMatchSet(16, this.LookAheadToken) || (this.IsEndStatement()) || ((this.AreNextTwoIdentifierAnd(VBTokenID.Colon)) || (this.AreNextTwo(VBTokenID.DecimalIntegerLiteral, VBTokenID.Colon))) || (this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression()) || ((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis }))))) {
							if (!this.MatchBlock(out block))
								return false;
						}
						this.AdvanceToNextEnd(isGetAccessor ? VBTokenID.Get : VBTokenID.Set);
						accessorDeclaration.BlockStatement = block as BlockStatement;
						accessorDeclaration.EndOffset = this.Token.EndOffset;
						if (isGetAccessor)
							propertyDeclaration.GetAccessor = accessorDeclaration;
						else
							propertyDeclaration.SetAccessor = accessorDeclaration;
						
						this.AdvancePastTerminators();
						if (this.IsInMultiMatchSet(19, this.LookAheadToken)) {
							parameterList = null;
							accessorDeclaration = new AccessorDeclaration();
							accessorDeclaration.StartOffset = this.LookAheadToken.StartOffset;
							if (this.TokenIs(this.LookAheadToken, VBTokenID.LessThan)) {
								if (!this.MatchAttributes(accessorDeclaration.AttributeSections))
									return false;
							}
							while (this.IsInMultiMatchSet(0, this.LookAheadToken)) {
								if (!this.MatchModifier(out singleModifier))
									return false;
								else {
									accessorDeclaration.Modifiers |= singleModifier;
								}
							}
							if (this.TokenIs(this.LookAheadToken, VBTokenID.Get)) {
								if (!this.Match(VBTokenID.Get))
									return false;
								else {
									isGetAccessor = true;
								}
							}
							else if (this.TokenIs(this.LookAheadToken, VBTokenID.Set)) {
								if (!this.Match(VBTokenID.Set))
									return false;
								else {
									isGetAccessor = false;
								}
							}
							else
								return false;
							if ((!isGetAccessor) && (this.TokenIs(this.LookAheadToken, VBTokenID.OpenParenthesis))) {
								if (!this.Match(VBTokenID.OpenParenthesis))
									return false;
								this.MatchParameterList(scopedGenericTypeArguments, out parameterList);
								if (parameterList != null)
									accessorDeclaration.Parameters.AddRange(parameterList.ToArray());
								this.Match(VBTokenID.CloseParenthesis);
							}
							this.Match(VBTokenID.LineTerminator);
							if ((this.IsInMultiMatchSet(16, this.LookAheadToken) || (this.IsEndStatement()) || ((this.AreNextTwoIdentifierAnd(VBTokenID.Colon)) || (this.AreNextTwo(VBTokenID.DecimalIntegerLiteral, VBTokenID.Colon))) || (this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression()) || ((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis }))))) {
								if (!this.MatchBlock(out block))
									return false;
							}
							this.AdvanceToNextEnd(isGetAccessor ? VBTokenID.Get : VBTokenID.Set);
							accessorDeclaration.BlockStatement = block as BlockStatement;
							accessorDeclaration.EndOffset = this.Token.EndOffset;
							if (isGetAccessor)
								propertyDeclaration.GetAccessor = accessorDeclaration;
							else
								propertyDeclaration.SetAccessor = accessorDeclaration;
							
							this.AdvancePastTerminators();
						}
						if (!this.Match(VBTokenID.End))
							return false;
						if (!this.Match(VBTokenID.Property))
							return false;
					}
				}
				propertyDeclaration.BlockEndOffset = this.Token.EndOffset;
				propertyDeclaration.EndOffset = this.Token.EndOffset;
				this.BlockAddChild(propertyDeclaration);
				
				// Reap comments
				this.ReapDocumentationComments();
				this.ReapComments(propertyDeclaration.Comments, false);
				this.MatchStatementTerminator();
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Operator)) {
				// OperatorDeclaration
				OperatorType operatorType = OperatorType.None;
				ParameterDeclaration parameter1 = null;
				ParameterDeclaration parameter2 = null;
				TypeReference typeReference = new TypeReference("System.Object", TextRange.Deleted);
				Statement block;
				bool isAbstract = ((modifiers & Modifiers.Abstract) == Modifiers.Abstract);
				if (!this.Match(VBTokenID.Operator))
					return false;
				if (this.IsInMultiMatchSet(20, this.LookAheadToken)) {
					// UnaryOperatorDeclaration / BinaryOperatorDeclaration
					bool requiresSingleOperand = false;
					if (this.TokenIs(this.LookAheadToken, VBTokenID.Addition)) {
						if (!this.Match(VBTokenID.Addition))
							return false;
						else {
							operatorType = OperatorType.Addition;
						}
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.Subtraction)) {
						if (!this.Match(VBTokenID.Subtraction))
							return false;
						else {
							operatorType = OperatorType.Subtraction;
						}
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.Multiplication)) {
						if (!this.Match(VBTokenID.Multiplication))
							return false;
						else {
							operatorType = OperatorType.Multiply;
						}
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.FloatingPointDivision)) {
						if (!this.Match(VBTokenID.FloatingPointDivision))
							return false;
						else {
							operatorType = OperatorType.Division;
						}
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.IntegerDivision)) {
						if (!this.Match(VBTokenID.IntegerDivision))
							return false;
						else {
							operatorType = OperatorType.IntegerDivision;
						}
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.StringConcatenation)) {
						if (!this.Match(VBTokenID.StringConcatenation))
							return false;
						else {
							operatorType = OperatorType.StringConcatenation;
						}
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.Like)) {
						if (!this.Match(VBTokenID.Like))
							return false;
						else {
							operatorType = OperatorType.Like;
						}
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.Mod)) {
						if (!this.Match(VBTokenID.Mod))
							return false;
						else {
							operatorType = OperatorType.Modulus;
						}
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.And)) {
						if (!this.Match(VBTokenID.And))
							return false;
						else {
							operatorType = OperatorType.ConditionalAnd;
						}
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.Or)) {
						if (!this.Match(VBTokenID.Or))
							return false;
						else {
							operatorType = OperatorType.ConditionalOr;
						}
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.Xor)) {
						if (!this.Match(VBTokenID.Xor))
							return false;
						else {
							operatorType = OperatorType.ExclusiveOr;
						}
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.Exponentiation)) {
						if (!this.Match(VBTokenID.Exponentiation))
							return false;
						else {
							operatorType = OperatorType.Exponentiation;
						}
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.LeftShift)) {
						if (!this.Match(VBTokenID.LeftShift))
							return false;
						else {
							operatorType = OperatorType.LeftShift;
						}
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.RightShift)) {
						if (!this.Match(VBTokenID.RightShift))
							return false;
						else {
							operatorType = OperatorType.RightShift;
						}
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.Equality)) {
						if (!this.Match(VBTokenID.Equality))
							return false;
						else {
							operatorType = OperatorType.Equality;
						}
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.Inequality)) {
						if (!this.Match(VBTokenID.Inequality))
							return false;
						else {
							operatorType = OperatorType.Inequality;
						}
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.GreaterThan)) {
						if (!this.Match(VBTokenID.GreaterThan))
							return false;
						else {
							operatorType = OperatorType.GreaterThan;
						}
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.LessThan)) {
						if (!this.Match(VBTokenID.LessThan))
							return false;
						else {
							operatorType = OperatorType.LessThan;
						}
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.GreaterThanOrEqual)) {
						if (!this.Match(VBTokenID.GreaterThanOrEqual))
							return false;
						else {
							operatorType = OperatorType.GreaterThanOrEqual;
						}
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.LessThanOrEqual)) {
						if (!this.Match(VBTokenID.LessThanOrEqual))
							return false;
						else {
							operatorType = OperatorType.LessThanOrEqual;
						}
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.Not)) {
						if (!this.Match(VBTokenID.Not))
							return false;
						else {
							operatorType = OperatorType.Negation; requiresSingleOperand = true;
						}
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.IsTrue)) {
						if (!this.Match(VBTokenID.IsTrue))
							return false;
						else {
							operatorType = OperatorType.True; requiresSingleOperand = true;
						}
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.IsFalse)) {
						if (!this.Match(VBTokenID.IsFalse))
							return false;
						else {
							operatorType = OperatorType.False; requiresSingleOperand = true;
						}
					}
					else
						return false;
					if (!this.Match(VBTokenID.OpenParenthesis))
						return false;
					if (!this.MatchOperand(out parameter1))
						return false;
					if (!requiresSingleOperand) {
						if (!this.Match(VBTokenID.Comma))
							return false;
						if (!this.MatchOperand(out parameter2))
							return false;
					}
					if (!this.Match(VBTokenID.CloseParenthesis))
						return false;
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.CType)) {
					// ConversionOperatorDeclaration
					if (!this.Match(VBTokenID.CType))
						return false;
					else {
						operatorType = OperatorType.Explicit;
					}
					if (!this.Match(VBTokenID.OpenParenthesis))
						return false;
					if (!this.MatchOperand(out parameter1))
						return false;
					if (!this.Match(VBTokenID.CloseParenthesis))
						return false;
				}
				else
					return false;
				// Default to public access
				if (!AstNode.IsAccessSpecified(modifiers))
					modifiers |= Modifiers.Public;
				
				OperatorDeclaration operatorDeclaration = new OperatorDeclaration(modifiers, operatorType);
				operatorDeclaration.Documentation = this.ReapDocumentationComments();
				operatorDeclaration.StartOffset = startOffset;
				operatorDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
				if (parameter1 != null)
					operatorDeclaration.Parameters.Add(parameter1);
				if (parameter2 != null)
					operatorDeclaration.Parameters.Add(parameter2);
				if (!isAbstract) {
					AstNodeList typeAttributeSections = new AstNodeList(null);
					if (!this.Match(VBTokenID.As))
						return false;
					if (this.TokenIs(this.LookAheadToken, VBTokenID.LessThan)) {
						if (!this.MatchAttributes(typeAttributeSections))
							return false;
					}
					if (!this.MatchTypeName(out typeReference, false))
						return false;
					else {
						this.MarkGenericParameters(scopedGenericTypeArguments, typeReference, true);
					}
					if (attributeSections != null)
						typeReference.AttributeSections.AddRange(attributeSections.ToArray());
				}
				if (!this.Match(VBTokenID.LineTerminator))
					return false;
				if ((this.IsInMultiMatchSet(16, this.LookAheadToken) || (this.IsEndStatement()) || ((this.AreNextTwoIdentifierAnd(VBTokenID.Colon)) || (this.AreNextTwo(VBTokenID.DecimalIntegerLiteral, VBTokenID.Colon))) || (this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression()) || ((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis }))))) {
					if (!this.MatchBlock(out block))
						return false;
					else {
						operatorDeclaration.Statements.Add(block);
					}
				}
				this.AdvanceToNextEnd(VBTokenID.Operator);
				operatorDeclaration.BlockEndOffset = this.Token.EndOffset;
				operatorDeclaration.EndOffset = this.Token.EndOffset;
				this.BlockAddChild(operatorDeclaration);
				
				// Reap comments
				this.ReapDocumentationComments();
				this.ReapComments(operatorDeclaration.Comments, false);
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>ModuleDeclaration</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ModuleDeclaration</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Module</c>.
		/// </remarks>
		protected virtual bool MatchModuleDeclaration(int startOffset, AstNodeList attributeSections, Modifiers modifiers) {
			if (!this.Match(VBTokenID.Module))
				return false;
			QualifiedIdentifier identifier;
			if (!this.MatchIdentifier(out identifier))
				return false;
			// Default to internal access
			if (!AstNode.IsAccessSpecified(modifiers))
				modifiers |= Modifiers.Assembly;
			
			StandardModuleDeclaration moduleDeclaration = new StandardModuleDeclaration(modifiers, identifier);
			moduleDeclaration.Documentation = this.ReapDocumentationComments();
			moduleDeclaration.StartOffset = startOffset;
			moduleDeclaration.BlockStartOffset = startOffset;
			moduleDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
			this.MatchStatementTerminator();
			this.BlockStart(VBTokenID.Module, moduleDeclaration);
			bool errorReported = false;
			while (!this.IsAtEnd) {
				if (this.IsEnd(VBTokenID.Module))
					break;
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
					// Skip over line terminators
					this.AdvancePastTerminators();
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.LessThan) || this.IsInMultiMatchSet(13, this.LookAheadToken) || this.IsInMultiMatchSet(14, this.LookAheadToken)) {
					errorReported = false;
					attributeSections = new AstNodeList(null);
					if (this.TokenIs(this.LookAheadToken, VBTokenID.LessThan)) {
						this.MatchAttributes(attributeSections);
					}
					startOffset = this.LookAheadToken.StartOffset;
					modifiers = Modifiers.None;
					if (this.IsInMultiMatchSet(0, this.LookAheadToken)) {
						if (!this.MatchModifiers(out modifiers))
							return false;
					}
					// Ensure all members are shared
					modifiers |= Modifiers.Static;
					this.MatchStructureMemberDeclaration(moduleDeclaration, startOffset, attributeSections, modifiers);
				}
				else {
					// Error recovery:  Advance to the next statement terminator since nothing was matched
					if (!errorReported) {
						this.ReportSyntaxError(AssemblyInfo.Instance.Resources.GetString("SemanticParserError_ModuleMemberDeclarationExpected"));
						errorReported = true;
					}
					this.AdvanceToNextStatementTerminator();
				}
			}
			
			this.BlockEnd();
			this.AdvanceToNextEnd(VBTokenID.Module);
			moduleDeclaration.BlockEndOffset = this.Token.EndOffset;
			moduleDeclaration.EndOffset = this.Token.EndOffset;
			
			// Reap comments
			this.ReapDocumentationComments();
			this.ReapComments(moduleDeclaration.Comments, false);
			this.MatchStatementTerminator();
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
			if (!this.Match(VBTokenID.Interface))
				return false;
			QualifiedIdentifier identifier;
			if (!this.MatchIdentifier(out identifier))
				return false;
			// Default to public access
			if (!AstNode.IsAccessSpecified(modifiers))
				modifiers |= Modifiers.Public;
			
			InterfaceDeclaration interfaceDeclaration = new InterfaceDeclaration(modifiers, identifier);
			interfaceDeclaration.Documentation = this.ReapDocumentationComments();
			interfaceDeclaration.StartOffset = startOffset;
			interfaceDeclaration.BlockStartOffset = startOffset;
			interfaceDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
			AstNodeList typeParameterList = null;
			if (((this.AreNextTwo(VBTokenID.OpenParenthesis, VBTokenID.Of)))) {
				if (!this.MatchTypeParameterList(out typeParameterList))
					return false;
			}
			if (typeParameterList != null)
				interfaceDeclaration.GenericTypeArguments.AddRange(typeParameterList.ToArray());
			this.MatchStatementTerminator();
			while (this.TokenIs(this.LookAheadToken, VBTokenID.Inherits)) {
				AstNodeList implementsList;
				if (!this.Match(VBTokenID.Inherits))
					return false;
				if (!this.MatchImplements(out implementsList))
					return false;
				this.MatchStatementTerminator();
				if (implementsList != null)
					interfaceDeclaration.BaseTypes.AddRange(implementsList.ToArray());
			}
			this.BlockStart(VBTokenID.Interface, interfaceDeclaration);
			bool errorReported = false;
			while (!this.IsAtEnd) {
				if (this.IsEnd(VBTokenID.Interface))
					break;
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
					// Skip over line terminators
					this.AdvancePastTerminators();
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.LessThan) || this.IsInMultiMatchSet(13, this.LookAheadToken) || this.IsInMultiMatchSet(21, this.LookAheadToken)) {
					errorReported = false;
					attributeSections = new AstNodeList(null);
					if (this.TokenIs(this.LookAheadToken, VBTokenID.LessThan)) {
						this.MatchAttributes(attributeSections);
					}
					startOffset = this.LookAheadToken.StartOffset;
					modifiers = Modifiers.None;
					if (this.IsInMultiMatchSet(0, this.LookAheadToken)) {
						if (!this.MatchModifiers(out modifiers))
							return false;
					}
					this.MatchInterfaceMemberDeclaration(interfaceDeclaration, startOffset, attributeSections, modifiers);
				}
				else {
					// Error recovery:  Advance to the next statement terminator since nothing was matched
					if (!errorReported) {
						this.ReportSyntaxError(AssemblyInfo.Instance.Resources.GetString("SemanticParserError_InterfaceMemberDeclarationExpected"));
						errorReported = true;
					}
					this.AdvanceToNextStatementTerminator();
				}
			}
			
			this.BlockEnd();
			this.AdvanceToNextEnd(VBTokenID.Interface);
			interfaceDeclaration.BlockEndOffset = this.Token.EndOffset;
			interfaceDeclaration.EndOffset = this.Token.EndOffset;
			
			// Reap comments
			this.ReapDocumentationComments();
			this.ReapComments(interfaceDeclaration.Comments, false);
			this.MatchStatementTerminator();
			return true;
		}

		/// <summary>
		/// Matches a <c>InterfaceMemberDeclaration</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>InterfaceMemberDeclaration</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Enum</c>, <c>Class</c>, <c>Structure</c>, <c>Event</c>, <c>Sub</c>, <c>Function</c>, <c>Property</c>, <c>Interface</c>, <c>Delegate</c>.
		/// </remarks>
		protected virtual bool MatchInterfaceMemberDeclaration(TypeDeclaration parentTypeDeclaration, int startOffset, AstNodeList attributeSections, Modifiers modifiers) {
			// Build a list of generic type arguments for the parent type... will add to this collection if a generic method
			AstNodeList scopedGenericTypeArguments = new AstNodeList(null);
			if ((parentTypeDeclaration.GenericTypeArguments != null) && (parentTypeDeclaration.GenericTypeArguments.Count > 0))
				scopedGenericTypeArguments.AddRange(parentTypeDeclaration.GenericTypeArguments.ToArray());
			
			QualifiedIdentifier identifier;
			AstNodeList typeParameterList;
			AstNodeList parameterList = null;
			TypeReference typeReference = null;
			if (this.IsInMultiMatchSet(8, this.LookAheadToken)) {
				if (!this.MatchNonModuleDeclaration(startOffset, attributeSections, modifiers))
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Event)) {
				if (!this.Match(VBTokenID.Event))
					return false;
				if (!this.MatchIdentifier(out identifier))
					return false;
				if (((this.TokenIs(this.LookAheadToken, VBTokenID.OpenParenthesis)) || (this.TokenIs(this.LookAheadToken, VBTokenID.As)))) {
					if (!this.MatchParametersOrType(scopedGenericTypeArguments, out parameterList, out typeReference))
						return false;
				}
				// Default to public access
				if (!AstNode.IsAccessSpecified(modifiers))
					modifiers |= Modifiers.Public;
				
				InterfaceEventDeclaration eventDeclaration = new InterfaceEventDeclaration(modifiers, identifier);
				eventDeclaration.Documentation = this.ReapDocumentationComments();
				eventDeclaration.StartOffset = startOffset;
				eventDeclaration.EndOffset = this.Token.EndOffset;
				eventDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
				eventDeclaration.EventType = typeReference;
				if (parameterList != null)
					eventDeclaration.Parameters.AddRange(parameterList.ToArray());
				this.BlockAddChild(eventDeclaration);
				this.MatchStatementTerminator();
			}
			else if (((this.TokenIs(this.LookAheadToken, VBTokenID.Sub)) || (this.TokenIs(this.LookAheadToken, VBTokenID.Function)))) {
				if (this.TokenIs(this.LookAheadToken, VBTokenID.Sub)) {
					if (!this.Match(VBTokenID.Sub))
						return false;
					if (!this.MatchSubSignature(scopedGenericTypeArguments, out identifier, out typeParameterList, out parameterList))
						return false;
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.Function)) {
					if (!this.Match(VBTokenID.Function))
						return false;
					if (!this.MatchFunctionSignature(scopedGenericTypeArguments, out identifier, out typeParameterList, out parameterList, out typeReference))
						return false;
				}
				else
					return false;
				// Default to public access
				if (!AstNode.IsAccessSpecified(modifiers))
					modifiers |= Modifiers.Public;
				
				InterfaceMethodDeclaration methodDeclaration = new InterfaceMethodDeclaration(modifiers, identifier);
				methodDeclaration.Documentation = this.ReapDocumentationComments();
				methodDeclaration.StartOffset = startOffset;
				methodDeclaration.EndOffset = this.Token.EndOffset;
				methodDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
				methodDeclaration.ReturnType = typeReference;
				if (typeParameterList != null)
					methodDeclaration.GenericTypeArguments.AddRange(typeParameterList.ToArray());
				if (parameterList != null)
					methodDeclaration.Parameters.AddRange(parameterList.ToArray());
				this.BlockAddChild(methodDeclaration);
				this.MatchStatementTerminator();
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Property)) {
				if (!this.Match(VBTokenID.Property))
					return false;
				if (!this.MatchFunctionSignature(scopedGenericTypeArguments, out identifier, out typeParameterList, out parameterList, out typeReference))
					return false;
				// Default to public access
				if (!AstNode.IsAccessSpecified(modifiers))
					modifiers |= Modifiers.Public;
				
				InterfacePropertyDeclaration propertyDeclaration = new InterfacePropertyDeclaration(modifiers, identifier);
				propertyDeclaration.Documentation = this.ReapDocumentationComments();
				propertyDeclaration.StartOffset = startOffset;
				propertyDeclaration.EndOffset = this.Token.EndOffset;
				propertyDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
				propertyDeclaration.ReturnType = typeReference;
				if (parameterList != null)
					propertyDeclaration.Parameters.AddRange(parameterList.ToArray());
				this.BlockAddChild(propertyDeclaration);
				this.MatchStatementTerminator();
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>ArrayTypeModifier</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ArrayTypeModifier</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>OpenParenthesis</c>.
		/// </remarks>
		protected virtual bool MatchArrayTypeModifier(out int[] ranks) {
			ranks = null;
			ArrayList rankList = new ArrayList();
			int rank = 0;
			while (this.TokenIs(this.LookAheadToken, VBTokenID.OpenParenthesis)) {
				if (!this.Match(VBTokenID.OpenParenthesis))
					return false;
				else {
					rank = 1;
				}
				while (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
					if (this.Match(VBTokenID.Comma)) {
						rank++;
					}
				}
				if (!this.Match(VBTokenID.CloseParenthesis))
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
		/// Matches a <c>DelegateDeclaration</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>DelegateDeclaration</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Delegate</c>.
		/// </remarks>
		protected virtual bool MatchDelegateDeclaration(int startOffset, AstNodeList attributeSections, Modifiers modifiers) {
			QualifiedIdentifier identifier = null;
			AstNodeList typeParameterList = null;
			AstNodeList parameterList = null;
			TypeReference typeReference = null;
			bool isFunction = false;
			AstNodeList scopedGenericTypeArguments = new AstNodeList(null);
			if (!this.Match(VBTokenID.Delegate))
				return false;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.Sub)) {
				if (!this.Match(VBTokenID.Sub))
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Function)) {
				if (!this.Match(VBTokenID.Function))
					return false;
				else {
					isFunction = true;
				}
			}
			else
				return false;
			if (!isFunction) {
				if (!this.MatchSubSignature(scopedGenericTypeArguments, out identifier, out typeParameterList, out parameterList))
					return false;
			}
			if (isFunction) {
				if (!this.MatchFunctionSignature(scopedGenericTypeArguments, out identifier, out typeParameterList, out parameterList, out typeReference))
					return false;
			}
			// Default to public access
			if (!AstNode.IsAccessSpecified(modifiers))
				modifiers |= Modifiers.Public;
			
			DelegateDeclaration delegateDeclaration = new DelegateDeclaration(modifiers, identifier);
			delegateDeclaration.Documentation = this.ReapDocumentationComments();
			delegateDeclaration.StartOffset = startOffset;
			delegateDeclaration.AttributeSections.AddRange(attributeSections.ToArray());
			if (typeParameterList != null)
				delegateDeclaration.GenericTypeArguments.AddRange(typeParameterList.ToArray());
			if (parameterList != null)
				delegateDeclaration.Parameters.AddRange(parameterList.ToArray());
			delegateDeclaration.ReturnType = typeReference;
			delegateDeclaration.EndOffset = this.Token.EndOffset;
			this.BlockAddChild(delegateDeclaration);
			delegateDeclaration.GenerateInvokeMembers();
			
			// Reap comments
			this.ReapDocumentationComments();
			this.ReapComments(delegateDeclaration.Comments, false);
			this.MatchStatementTerminator();
			return true;
		}

		/// <summary>
		/// Matches a <c>TypeArgumentList</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>TypeArgumentList</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Global</c>, <c>Object</c>, <c>Single</c>, <c>Double</c>, <c>Decimal</c>, <c>Boolean</c>, <c>Date</c>, <c>Char</c>, <c>String</c>, <c>Byte</c>, <c>SByte</c>, <c>UShort</c>, <c>Short</c>, <c>UInteger</c>, <c>Integer</c>, <c>ULong</c>, <c>Long</c>.
		/// </remarks>
		protected virtual bool MatchTypeArgumentList(out AstNodeList typeArgumentList) {
			typeArgumentList = new AstNodeList(null);
			TypeReference typeReference;
			if (!this.MatchTypeName(out typeReference, false))
				return false;
			else {
				typeArgumentList.Add(typeReference);
			}
			while (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
				if (!this.Match(VBTokenID.Comma))
					return false;
				if (!this.MatchTypeName(out typeReference, false))
					return false;
				else {
					typeArgumentList.Add(typeReference);
				}
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>ImplementsClause</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ImplementsClause</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Implements</c>.
		/// </remarks>
		protected virtual bool MatchImplementsClause(out AstNodeList implementsList) {
			implementsList = new AstNodeList(null);
			MemberSpecifier memberSpecifier;
			if (!this.Match(VBTokenID.Implements))
				return false;
			if (!this.MatchInterfaceMemberSpecifier(out memberSpecifier))
				return false;
			else {
				implementsList.Add(memberSpecifier);
			}
			while (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
				if (!this.Match(VBTokenID.Comma))
					return false;
				if (!this.MatchInterfaceMemberSpecifier(out memberSpecifier))
					return false;
				else {
					implementsList.Add(memberSpecifier);
				}
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>InterfaceMemberSpecifier</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>InterfaceMemberSpecifier</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Global</c>, <c>Object</c>, <c>Single</c>, <c>Double</c>, <c>Decimal</c>, <c>Boolean</c>, <c>Date</c>, <c>Char</c>, <c>String</c>, <c>Byte</c>, <c>SByte</c>, <c>UShort</c>, <c>Short</c>, <c>UInteger</c>, <c>Integer</c>, <c>ULong</c>, <c>Long</c>.
		/// </remarks>
		protected virtual bool MatchInterfaceMemberSpecifier(out MemberSpecifier memberSpecifier) {
			memberSpecifier = null;
			TypeReference typeReference;
			QualifiedIdentifier memberName;
			bool isIdentifier = this.IsIdentifier(this.LookAheadToken);
			if (!this.MatchNonArrayTypeName(out typeReference, false))
				return false;
			bool requiresIdentifier = !isIdentifier;
			if (!requiresIdentifier) {
				requiresIdentifier = (typeReference.GenericTypeArguments.Count > 0);
				if (!requiresIdentifier)
					requiresIdentifier = (typeReference.Name.IndexOf('.') == -1);
			}
			if (!requiresIdentifier) {
				// Remove the last part of the qualified identifier and use that as the member name
				int index = typeReference.Name.LastIndexOf('.');
				memberName = new QualifiedIdentifier(typeReference.Name.Substring(index + 1));
				typeReference.Name = typeReference.Name.Substring(0, index);
				memberName.StartOffset = typeReference.EndOffset - memberName.Text.Length;
				memberName.EndOffset = typeReference.EndOffset;
				typeReference.EndOffset -= (memberName.Text.Length + 1);
				memberSpecifier = new MemberSpecifier(typeReference, memberName);
				return true;
			}
			if (!this.Match(VBTokenID.Dot))
				return false;
			if (!this.MatchIdentifierOrKeyword(out memberName))
				return false;
			else {
				memberSpecifier = new MemberSpecifier(typeReference, memberName);
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>SubSignature</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>SubSignature</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>.
		/// </remarks>
		protected virtual bool MatchSubSignature(IAstNodeList genericTypeArguments, out QualifiedIdentifier identifier, out AstNodeList typeParameterList, out AstNodeList parameterList) {
			identifier = null;
			typeParameterList = null;
			parameterList = null;
			if (!this.MatchIdentifier(out identifier))
				return false;
			if (((this.AreNextTwo(VBTokenID.OpenParenthesis, VBTokenID.Of)))) {
				if (this.MatchTypeParameterList(out typeParameterList)) {
					// Add to the scoped generic type arguments
					if ((typeParameterList != null) && (typeParameterList.Count > 0))
						genericTypeArguments.AddRange(typeParameterList.ToArray());
				}
			}
			if (this.TokenIs(this.LookAheadToken, VBTokenID.OpenParenthesis)) {
				if (!this.Match(VBTokenID.OpenParenthesis))
					return false;
				if (this.IsInMultiMatchSet(17, this.LookAheadToken)) {
					this.MatchParameterList(genericTypeArguments, out parameterList);
				}
				this.Match(VBTokenID.CloseParenthesis);
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>FunctionSignature</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>FunctionSignature</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>.
		/// </remarks>
		protected virtual bool MatchFunctionSignature(IAstNodeList genericTypeArguments, out QualifiedIdentifier identifier, out AstNodeList typeParameterList, out AstNodeList parameterList, out TypeReference typeReference) {
			typeReference = new TypeReference("System.Object", TextRange.Deleted);
			if (!this.MatchSubSignature(genericTypeArguments, out identifier, out typeParameterList, out parameterList))
				return false;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.As)) {
				if (!this.Match(VBTokenID.As))
					return false;
				AstNodeList attributeSections = new AstNodeList(null);
				if (this.TokenIs(this.LookAheadToken, VBTokenID.LessThan)) {
					this.MatchAttributes(attributeSections);
				}
				if (!this.MatchTypeName(out typeReference, false))
					return false;
				else {
					this.MarkGenericParameters(genericTypeArguments, typeReference, true);
				}
				if (attributeSections != null)
					typeReference.AttributeSections.AddRange(attributeSections.ToArray());
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>CharsetModifier</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>CharsetModifier</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>.
		/// </remarks>
		protected virtual bool MatchCharsetModifier() {
			if (!this.MatchSimpleIdentifier())
				return false;
			// Must be 'Ansi' | 'Unicode' | 'Auto'
			return true;
		}

		/// <summary>
		/// Matches a <c>LibraryClause</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>LibraryClause</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Lib</c>.
		/// </remarks>
		protected virtual bool MatchLibraryClause() {
			if (!this.Match(VBTokenID.Lib))
				return false;
			if (!this.Match(VBTokenID.StringLiteral))
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>AliasClause</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>AliasClause</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Alias</c>.
		/// </remarks>
		protected virtual bool MatchAliasClause() {
			if (!this.Match(VBTokenID.Alias))
				return false;
			if (!this.Match(VBTokenID.StringLiteral))
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>ParameterList</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ParameterList</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>LessThan</c>, <c>ByVal</c>, <c>ByRef</c>, <c>Optional</c>, <c>ParamArray</c>.
		/// </remarks>
		protected virtual bool MatchParameterList(IAstNodeList genericTypeArguments, out AstNodeList parameterList) {
			parameterList = new AstNodeList(null);
			ParameterDeclaration parameter;
			if (!this.MatchParameter(genericTypeArguments, out parameter))
				return false;
			else {
				parameterList.Add(parameter);
			}
			while (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
				if (!this.Match(VBTokenID.Comma))
					return false;
				if (!this.MatchParameter(genericTypeArguments, out parameter))
					return false;
				else {
					parameterList.Add(parameter);
				}
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>Parameter</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>Parameter</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>LessThan</c>, <c>ByVal</c>, <c>ByRef</c>, <c>Optional</c>, <c>ParamArray</c>.
		/// </remarks>
		protected virtual bool MatchParameter(IAstNodeList genericTypeArguments, out ParameterDeclaration parameter) {
			parameter = null;
			int startOffset = this.LookAheadToken.StartOffset;
			AstNodeList attributeSections = new AstNodeList(null);
			ParameterModifiers modifiers = ParameterModifiers.None;
			TypeReference typeReference = new TypeReference("System.Object", TextRange.Deleted);
			Expression initializer = null;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.LessThan)) {
				this.MatchAttributes(attributeSections);
			}
			while (this.IsInMultiMatchSet(22, this.LookAheadToken)) {
				if (this.TokenIs(this.LookAheadToken, VBTokenID.ByVal)) {
					if (!this.Match(VBTokenID.ByVal))
						return false;
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.ByRef)) {
					if (!this.Match(VBTokenID.ByRef))
						return false;
					else {
						modifiers |= ParameterModifiers.Ref;
					}
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.Optional)) {
					if (!this.Match(VBTokenID.Optional))
						return false;
					else {
						modifiers |= ParameterModifiers.Optional;
					}
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.ParamArray)) {
					if (!this.Match(VBTokenID.ParamArray))
						return false;
					else {
						modifiers |= ParameterModifiers.ParameterArray;
					}
				}
				else
					return false;
			}
			string name;
			int[] arrayRanks;
			if (!this.MatchSimpleIdentifier())
				return false;
			else {
				name = this.TokenText;
			}
			if (this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.QuestionMark, VBTokenID.OpenParenthesis })) {
				if (this.TokenIs(this.LookAheadToken, VBTokenID.QuestionMark)) {
					if (!this.Match(VBTokenID.QuestionMark))
						return false;
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.OpenParenthesis)) {
					bool isArrayTypeModifier = false;
					while (this.IsArrayTypeModifier()) {
						if (!this.MatchArrayTypeModifier(out arrayRanks))
							return false;
						else {
							isArrayTypeModifier = true;
						}
					}
					if (!isArrayTypeModifier) {
						if (!this.MatchArraySizeInitializationModifier(out arrayRanks))
							return false;
					}
				}
				else
					return false;
			}
			if (this.TokenIs(this.LookAheadToken, VBTokenID.As)) {
				if (!this.Match(VBTokenID.As))
					return false;
				if (!this.MatchTypeName(out typeReference, false))
					return false;
				else {
					this.MarkGenericParameters(genericTypeArguments, typeReference, true);
				}
			}
			if (this.TokenIs(this.LookAheadToken, VBTokenID.Equality)) {
				if (!this.Match(VBTokenID.Equality))
					return false;
				this.MatchExpression(out initializer);
			}
			parameter = new ParameterDeclaration(modifiers, name);
			parameter.StartOffset = startOffset;
			parameter.EndOffset = this.Token.EndOffset;
			parameter.AttributeSections.AddRange(attributeSections.ToArray());
			parameter.ParameterType = typeReference;
			parameter.Initializer = initializer;
			return true;
		}

		/// <summary>
		/// Matches a <c>EventHandlesList</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>EventHandlesList</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Global</c>, <c>MyBase</c>, <c>Me</c>.
		/// </remarks>
		protected virtual bool MatchEventHandlesList(out AstNodeList handlesList) {
			handlesList = new AstNodeList(null);
			EventMemberSpecifier memberSpecifier;
			if (!this.MatchEventMemberSpecifier(out memberSpecifier))
				return false;
			else {
				handlesList.Add(memberSpecifier);
			}
			while (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
				if (!this.Match(VBTokenID.Comma))
					return false;
				if (!this.MatchEventMemberSpecifier(out memberSpecifier))
					return false;
				else {
					handlesList.Add(memberSpecifier);
				}
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>EventMemberSpecifier</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>EventMemberSpecifier</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Global</c>, <c>MyBase</c>, <c>Me</c>.
		/// </remarks>
		protected virtual bool MatchEventMemberSpecifier(out EventMemberSpecifier memberSpecifier) {
			memberSpecifier = null;
			QualifiedIdentifier target;
			QualifiedIdentifier memberName = null;
			if (this.IsInMultiMatchSet(9, this.LookAheadToken)) {
				if (!this.MatchQualifiedIdentifier(out target))
					return false;
				int index = target.Text.LastIndexOf('.');
				if (index != -1) {
					int length = target.Text.Length - index;
					memberName = new QualifiedIdentifier(target.Text.Substring(index + 1), new TextRange(target.EndOffset - length, target.EndOffset));
					target.Text = target.Text.Substring(0, index);
					target.EndOffset -= length;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.MyBase)) {
				if (!this.Match(VBTokenID.MyBase))
					return false;
				else {
					target = new QualifiedIdentifier(this.TokenText, this.Token.TextRange);
				}
				if (!this.Match(VBTokenID.Dot))
					return false;
				if (!this.MatchIdentifier(out memberName))
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Me)) {
				if (!this.Match(VBTokenID.Me))
					return false;
				else {
					target = new QualifiedIdentifier(this.TokenText, this.Token.TextRange);
				}
				if (!this.Match(VBTokenID.Dot))
					return false;
				if (!this.MatchIdentifier(out memberName))
					return false;
			}
			else
				return false;
			memberSpecifier = new EventMemberSpecifier(target, memberName);
			return true;
		}

		/// <summary>
		/// Matches a <c>ParametersOrType</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ParametersOrType</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>OpenParenthesis</c>, <c>As</c>.
		/// </remarks>
		protected virtual bool MatchParametersOrType(IAstNodeList genericTypeArguments, out AstNodeList parameterList, out TypeReference typeReference) {
			parameterList = null;
			typeReference = new TypeReference("System.Object", TextRange.Deleted);
			if (this.TokenIs(this.LookAheadToken, VBTokenID.OpenParenthesis)) {
				if (!this.Match(VBTokenID.OpenParenthesis))
					return false;
				if (this.IsInMultiMatchSet(17, this.LookAheadToken)) {
					if (!this.MatchParameterList(genericTypeArguments, out parameterList))
						return false;
				}
				if (!this.Match(VBTokenID.CloseParenthesis))
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.As)) {
				if (!this.Match(VBTokenID.As))
					return false;
				if (!this.MatchNonArrayTypeName(out typeReference, false))
					return false;
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
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>.
		/// </remarks>
		protected virtual bool MatchConstantDeclarator(FieldDeclaration constantDeclaration) {
			QualifiedIdentifier identifier;
			TypeReference typeReference = new TypeReference("System.Object", TextRange.Deleted);
			Expression initializer;
			int startOffset = this.LookAheadToken.StartOffset;
			if (!this.MatchIdentifier(out identifier))
				return false;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.As)) {
				if (!this.Match(VBTokenID.As))
					return false;
				if (!this.MatchTypeName(out typeReference, false))
					return false;
			}
			if (!this.Match(VBTokenID.Equality))
				return false;
			if (!this.MatchExpression(out initializer))
				return false;
			VariableDeclarator variableDeclarator = new VariableDeclarator(typeReference, identifier, true, false);
			variableDeclarator.Initializer = initializer;
			variableDeclarator.StartOffset = startOffset;
			variableDeclarator.EndOffset = this.Token.EndOffset;
			constantDeclaration.Variables.Add(variableDeclarator);
			this.MatchStatementTerminator();
			return true;
		}

		/// <summary>
		/// Matches a <c>VariableDeclarator</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>VariableDeclarator</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>.
		/// </remarks>
		protected virtual bool MatchVariableDeclarator(IAstNodeList genericTypeArguments, IVariableDeclarationSection declaration) {
			QualifiedIdentifier identifier;
			int[] arrayRanks = null;
			AstNodeList identifierList = new AstNodeList(null);
			TypeReference typeReference = new TypeReference("System.Object", TextRange.Deleted);
			Expression initializer = null;
			int startOffset = this.LookAheadToken.StartOffset;
			bool isNewExpression = false;
			if (!this.MatchVariableIdentifier(out identifier, out arrayRanks))
				return false;
			else {
				identifierList.Add(identifier);
			}
			while (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
				if (!this.Match(VBTokenID.Comma))
					return false;
				if (!this.MatchVariableIdentifier(out identifier, out arrayRanks))
					return false;
				else {
					identifierList.Add(identifier);
				}
			}
			if (this.TokenIs(this.LookAheadToken, VBTokenID.As)) {
				if (!this.Match(VBTokenID.As))
					return false;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.New)) {
					if (!this.MatchObjectCreationExpression(out initializer))
						return false;
					else {
						isNewExpression = true;
					}
				}
				else if (this.IsInMultiMatchSet(3, this.LookAheadToken)) {
					if (!this.MatchTypeName(out typeReference, false))
						return false;
					else {
						this.MarkGenericParameters(genericTypeArguments, typeReference, true);
					}
				}
				else
					return false;
			}
			if ((!isNewExpression) && (this.TokenIs(this.LookAheadToken, VBTokenID.Equality))) {
				if (!this.Match(VBTokenID.Equality))
					return false;
				if (!this.MatchExpression(out initializer))
					return false;
			}
			for (int index = 0; index < identifierList.Count; index++) {
				bool isImplicitlyTyped = false;
				if ((initializer != null) && ((typeReference == null) ||
				((!typeReference.HasStartOffset) && (typeReference.Name == "System.Object")))) {
					
					// If in an implicitly typed declaration, try and locate the type reference
					isImplicitlyTyped = true;
					TypeReference implicitTypeReference = this.GetImplicitType(initializer, false);
					if (implicitTypeReference != null)
						typeReference = implicitTypeReference;
				}
				else if (typeReference != null)
					typeReference.ArrayRanks = arrayRanks;
				
				VariableDeclarator variableDeclarator = new VariableDeclarator(typeReference, (QualifiedIdentifier)identifierList[index], false, false);
				variableDeclarator.Initializer = initializer;
				variableDeclarator.IsImplicitlyTyped = isImplicitlyTyped;
				variableDeclarator.StartOffset = startOffset;
				variableDeclarator.EndOffset = this.Token.EndOffset;
				declaration.Variables.Add(variableDeclarator);
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>VariableIdentifier</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>VariableIdentifier</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>.
		/// </remarks>
		protected virtual bool MatchVariableIdentifier(out QualifiedIdentifier identifier, out int[] arrayRanks) {
			arrayRanks = null;
			if (!this.MatchIdentifier(out identifier))
				return false;
			if ((this.TokenIs(this.LookAheadToken, VBTokenID.QuestionMark)) || (this.TokenIs(this.LookAheadToken, VBTokenID.OpenParenthesis))) {
				if (this.TokenIs(this.LookAheadToken, VBTokenID.QuestionMark)) {
					if (!this.Match(VBTokenID.QuestionMark))
						return false;
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.OpenParenthesis)) {
					bool isArrayTypeModifier = false;
					while (this.IsArrayTypeModifier()) {
						if (!this.MatchArrayTypeModifier(out arrayRanks))
							return false;
						else {
							isArrayTypeModifier = true;
						}
					}
					if (!isArrayTypeModifier) {
						if (!this.MatchArraySizeInitializationModifier(out arrayRanks))
							return false;
					}
				}
				else
					return false;
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>ArraySizeInitializationModifier</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ArraySizeInitializationModifier</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>OpenParenthesis</c>.
		/// </remarks>
		protected virtual bool MatchArraySizeInitializationModifier(out int[] arrayRanks) {
			arrayRanks = null;
			// NOTE: Expressions are ignored until further implementation
			while (this.IsArrayTypeModifier()) {
				if (!this.MatchArrayTypeModifier(out arrayRanks))
					return false;
				else {
					return true;
				}
			}
			if (!this.Match(VBTokenID.OpenParenthesis))
				return false;
			Expression expression;
			if (this.AreNextTwo(VBTokenID.DecimalIntegerLiteral, VBTokenID.To)) {
				if (!this.Match(VBTokenID.DecimalIntegerLiteral))
					return false;
				if (!this.Match(VBTokenID.To))
					return false;
				if (!this.MatchExpression(out expression))
					return false;
			}
			else {
				if (!this.MatchExpression(out expression))
					return false;
				while (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
					if (!this.Match(VBTokenID.Comma))
						return false;
					if (!this.MatchExpression(out expression))
						return false;
				}
			}
			if (!this.Match(VBTokenID.CloseParenthesis))
				return false;
			while (this.IsArrayTypeModifier()) {
				if (!this.MatchArrayTypeModifier(out arrayRanks))
					return false;
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>Operand</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>Operand</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>ByVal</c>.
		/// </remarks>
		protected virtual bool MatchOperand(out ParameterDeclaration parameter) {
			parameter = null;
			TypeReference parameterTypeReference = new TypeReference("System.Object", TextRange.Deleted);
			string name = null;
			int startOffset = this.LookAheadToken.StartOffset;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.ByVal)) {
				this.Match(VBTokenID.ByVal);
			}
			if (!this.MatchSimpleIdentifier())
				return false;
			else {
				name = this.TokenText;
			}
			if (this.TokenIs(this.LookAheadToken, VBTokenID.As)) {
				if (!this.Match(VBTokenID.As))
					return false;
				if (!this.MatchTypeName(out parameterTypeReference, false))
					return false;
			}
			parameter = new ParameterDeclaration(ParameterModifiers.None, name);
			parameter.StartOffset = startOffset;
			parameter.EndOffset = this.Token.EndOffset;
			parameter.ParameterType = parameterTypeReference;
			return true;
		}

		/// <summary>
		/// Matches a <c>Statement</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>Statement</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Dim</c>, <c>Static</c>, <c>Global</c>, <c>OpenParenthesis</c>, <c>OpenCurlyBrace</c>, <c>New</c>, <c>XmlLiteral</c>, <c>Object</c>, <c>Single</c>, <c>Double</c>, <c>Decimal</c>, <c>Boolean</c>, <c>Date</c>, <c>Char</c>, <c>String</c>, <c>Byte</c>, <c>SByte</c>, <c>UShort</c>, <c>Short</c>, <c>UInteger</c>, <c>Integer</c>, <c>ULong</c>, <c>Long</c>, <c>Const</c>, <c>AddHandler</c>, <c>RemoveHandler</c>, <c>RaiseEvent</c>, <c>Sub</c>, <c>Function</c>, <c>CType</c>, <c>StringLiteral</c>, <c>MyBase</c>, <c>Me</c>, <c>DecimalIntegerLiteral</c>, <c>With</c>, <c>SyncLock</c>, <c>Mid</c>, <c>Call</c>, <c>If</c>, <c>Select</c>, <c>While</c>, <c>Do</c>, <c>For</c>, <c>Throw</c>, <c>Try</c>, <c>Error</c>, <c>On</c>, <c>GoTo</c>, <c>Resume</c>, <c>Exit</c>, <c>Continue</c>, <c>Stop</c>, <c>Return</c>, <c>ReDim</c>, <c>Erase</c>, <c>Using</c>, <c>True</c>, <c>False</c>, <c>HexadecimalIntegerLiteral</c>, <c>OctalIntegerLiteral</c>, <c>FloatingPointLiteral</c>, <c>CharacterLiteral</c>, <c>DateLiteral</c>, <c>Nothing</c>, <c>AddressOf</c>, <c>GetTypeKeyword</c>, <c>TypeOf</c>, <c>GetXmlNamespace</c>, <c>MyClass</c>, <c>DirectCast</c>, <c>TryCast</c>, <c>IIf</c>, <c>CBool</c>, <c>CByte</c>, <c>CChar</c>, <c>CDate</c>, <c>CDec</c>, <c>CDbl</c>, <c>CInt</c>, <c>CLng</c>, <c>CObj</c>, <c>CSByte</c>, <c>CShort</c>, <c>CSng</c>, <c>CStr</c>, <c>CUInt</c>, <c>CULng</c>, <c>CUShort</c>.
		/// The non-terminal can start with: this.IsEndStatement().
		/// The non-terminal can start with: (this.AreNextTwoIdentifierAnd(VBTokenID.Colon)) || (this.AreNextTwo(VBTokenID.DecimalIntegerLiteral, VBTokenID.Colon)).
		/// The non-terminal can start with: this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression().
		/// The non-terminal can start with: (this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) &amp;&amp; (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis })).
		/// </remarks>
		protected virtual bool MatchStatement(out Statement statement) {
			statement = null;
			
			// Skip over line terminators
			this.AdvancePastTerminators();
			
			int startOffset = this.LookAheadToken.StartOffset;
			
			if (this.IsEndStatement()) {
				// Process 'End' statements separately because their conditions conflict with normal End statements
				this.Match(VBTokenID.End);
				this.MatchStatementTerminator();
				statement = new BranchStatement(BranchStatementType.End, new TextRange(startOffset, this.Token.EndOffset));
				return true;
			}
			if ((((this.AreNextTwoIdentifierAnd(VBTokenID.Colon)) || (this.AreNextTwo(VBTokenID.DecimalIntegerLiteral, VBTokenID.Colon))))) {
				Expression expression;
				if (!this.MatchLabelName(out expression))
					return false;
				if (!this.Match(VBTokenID.Colon))
					return false;
				statement = new LabeledStatement(expression, new EmptyStatement(new TextRange(this.Token.EndOffset)), new TextRange(startOffset, this.Token.EndOffset));
			}
			else if (((this.TokenIs(this.LookAheadToken, VBTokenID.Dim)) || (this.TokenIs(this.LookAheadToken, VBTokenID.Static)) || (this.TokenIs(this.LookAheadToken, VBTokenID.Const)))) {
				Modifiers modifier = Modifiers.None;
				statement = new LocalVariableDeclaration();
				statement.StartOffset = this.LookAheadToken.StartOffset;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.Dim)) {
					if (this.Match(VBTokenID.Dim)) {
						modifier = Modifiers.Dim;
					}
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.Static)) {
					if (this.Match(VBTokenID.Static)) {
						modifier = Modifiers.Static;
					}
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.Const)) {
					if (this.Match(VBTokenID.Const)) {
						modifier = Modifiers.Default;
					}
				}
				else
					return false;
				((LocalVariableDeclaration)statement).Modifiers = modifier;
				if (!this.MatchVariableDeclarator(null, (LocalVariableDeclaration)statement))
					return false;
				while (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
					if (!this.Match(VBTokenID.Comma))
						return false;
					if (!this.MatchVariableDeclarator(null, (LocalVariableDeclaration)statement))
						return false;
				}
				statement.EndOffset = this.Token.EndOffset;
				this.MatchStatementTerminator();
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.With)) {
				// WithStatement
				Expression expression;
				Statement embeddedStatement = null;
				if (!this.Match(VBTokenID.With))
					return false;
				if (!this.MatchExpression(out expression)) {
					this.AdvanceToNextEnd(VBTokenID.With, true); return false;
				}
				this.MatchStatementTerminator();
				if ((this.IsInMultiMatchSet(16, this.LookAheadToken) || (this.IsEndStatement()) || ((this.AreNextTwoIdentifierAnd(VBTokenID.Colon)) || (this.AreNextTwo(VBTokenID.DecimalIntegerLiteral, VBTokenID.Colon))) || (this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression()) || ((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis }))))) {
					if (!this.MatchBlock(out embeddedStatement)) {
						this.AdvanceToNextEnd(VBTokenID.With, true); return false;
					}
				}
				this.AdvanceToNextEnd(VBTokenID.With, true);
				statement = new WithStatement(expression, embeddedStatement, new TextRange(startOffset, this.Token.EndOffset));
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.SyncLock)) {
				// SyncLockStatement
				Expression expression;
				Statement embeddedStatement = null;
				if (!this.Match(VBTokenID.SyncLock))
					return false;
				if (!this.MatchExpression(out expression)) {
					this.AdvanceToNextEnd(VBTokenID.SyncLock, true); return false;
				}
				this.MatchStatementTerminator();
				if ((this.IsInMultiMatchSet(16, this.LookAheadToken) || (this.IsEndStatement()) || ((this.AreNextTwoIdentifierAnd(VBTokenID.Colon)) || (this.AreNextTwo(VBTokenID.DecimalIntegerLiteral, VBTokenID.Colon))) || (this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression()) || ((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis }))))) {
					if (!this.MatchBlock(out embeddedStatement)) {
						this.AdvanceToNextEnd(VBTokenID.SyncLock, true); return false;
					}
				}
				this.AdvanceToNextEnd(VBTokenID.SyncLock, true);
				statement = new LockStatement(expression, embeddedStatement, new TextRange(startOffset, this.Token.EndOffset));
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.RaiseEvent)) {
				// RaiseEventStatement via EventStatement
				QualifiedIdentifier identifier;
				AstNodeList argumentList = null;
				if (!this.Match(VBTokenID.RaiseEvent))
					return false;
				if (!this.MatchIdentifierOrKeyword(out identifier))
					return false;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.OpenParenthesis)) {
					if (!this.Match(VBTokenID.OpenParenthesis))
						return false;
					if ((this.IsInMultiMatchSet(23, this.LookAheadToken) || (this.IsKeyword()) || (this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression()) || ((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis }))))) {
						if (!this.MatchArgumentList(out argumentList))
							return false;
					}
					if (!this.Match(VBTokenID.CloseParenthesis))
						return false;
				}
				statement = new RaiseEventStatement(identifier, argumentList, new TextRange(startOffset, this.Token.EndOffset));
				this.MatchStatementTerminator();
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.AddHandler)) {
				// AddHandlerStatement via EventStatement
				Expression @event;
				Expression eventHandler;
				if (!this.Match(VBTokenID.AddHandler))
					return false;
				if (!this.MatchExpression(out @event))
					return false;
				if (!this.Match(VBTokenID.Comma))
					return false;
				if (!this.MatchExpression(out eventHandler))
					return false;
				statement = new ModifyEventHandlerStatement(ModifyEventHandlerStatementType.Add, @event, eventHandler, new TextRange(startOffset, this.Token.EndOffset));
				this.MatchStatementTerminator();
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.RemoveHandler)) {
				// RemoveHandlerStatement via EventStatement
				Expression @event;
				Expression eventHandler;
				if (!this.Match(VBTokenID.RemoveHandler))
					return false;
				if (!this.MatchExpression(out @event))
					return false;
				if (!this.Match(VBTokenID.Comma))
					return false;
				if (!this.MatchExpression(out eventHandler))
					return false;
				statement = new ModifyEventHandlerStatement(ModifyEventHandlerStatementType.Remove, @event, eventHandler, new TextRange(startOffset, this.Token.EndOffset));
				this.MatchStatementTerminator();
			}
			else if (this.AreNextTwo(VBTokenID.Mid, VBTokenID.OpenParenthesis )) {
				// MidAssignmentStatement
				Expression leftExpression, startIndexExpression, lengthExpression = null, rightExpression;
				if (!this.Match(VBTokenID.Mid))
					return false;
				// NOTE: Should allow $
				if (!this.Match(VBTokenID.OpenParenthesis))
					return false;
				if (!this.MatchExpression(out leftExpression))
					return false;
				if (!this.Match(VBTokenID.Comma))
					return false;
				if (!this.MatchExpression(out startIndexExpression))
					return false;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
					if (!this.Match(VBTokenID.Comma))
						return false;
					if (!this.MatchExpression(out lengthExpression))
						return false;
				}
				if (!this.Match(VBTokenID.CloseParenthesis))
					return false;
				if (!this.Match(VBTokenID.Equality))
					return false;
				if (!this.MatchExpression(out rightExpression))
					return false;
				AssignmentExpression assignmentExpression = new AssignmentExpression(OperatorType.Mid, leftExpression, rightExpression, new TextRange(startOffset, this.Token.EndOffset));
				assignmentExpression.StartIndexExpression = startIndexExpression;
				assignmentExpression.LengthExpression = lengthExpression;
				statement = new StatementExpression(assignmentExpression);
				this.MatchStatementTerminator();
			}
			else if ((this.IsInMultiMatchSet(24, this.LookAheadToken) || (this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression()) || ((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis }))))) {
				// RegularAssignmentStatement / CompoundAssignmentStatement / InvocationStatement (handled partly in PrimaryExpression)
				Expression leftExpression, rightExpression;
				OperatorType operatorType = OperatorType.None;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.Call)) {
					this.Match(VBTokenID.Call);
				}
				if (!this.MatchPrimaryExpression(true, out leftExpression))
					return false;
				if (this.IsInMultiMatchSet(25, this.LookAheadToken)) {
					if (this.TokenIs(this.LookAheadToken, VBTokenID.Equality)) {
						if (!this.Match(VBTokenID.Equality))
							return false;
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.AdditionAssignment)) {
						if (!this.Match(VBTokenID.AdditionAssignment))
							return false;
						else {
							operatorType = OperatorType.Addition;
						}
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.SubtractionAssignment)) {
						if (!this.Match(VBTokenID.SubtractionAssignment))
							return false;
						else {
							operatorType = OperatorType.Subtraction;
						}
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.MultiplicationAssignment)) {
						if (!this.Match(VBTokenID.MultiplicationAssignment))
							return false;
						else {
							operatorType = OperatorType.Multiply;
						}
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.FloatingPointDivisionAssignment)) {
						if (!this.Match(VBTokenID.FloatingPointDivisionAssignment))
							return false;
						else {
							operatorType = OperatorType.Division;
						}
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.IntegerDivisionAssignment)) {
						if (!this.Match(VBTokenID.IntegerDivisionAssignment))
							return false;
						else {
							operatorType = OperatorType.IntegerDivision;
						}
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.StringConcatenationAssignment)) {
						if (!this.Match(VBTokenID.StringConcatenationAssignment))
							return false;
						else {
							operatorType = OperatorType.StringConcatenation;
						}
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.ExponentiationAssignment)) {
						if (!this.Match(VBTokenID.ExponentiationAssignment))
							return false;
						else {
							operatorType = OperatorType.Exponentiation;
						}
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.LeftShiftAssignment)) {
						if (!this.Match(VBTokenID.LeftShiftAssignment))
							return false;
						else {
							operatorType = OperatorType.LeftShift;
						}
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.RightShiftAssignment)) {
						if (!this.Match(VBTokenID.RightShiftAssignment))
							return false;
						else {
							operatorType = OperatorType.RightShift;
						}
					}
					else
						return false;
					if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
						this.Match(VBTokenID.LineTerminator);
					}
					if (!this.MatchExpression(out rightExpression)) {
						this.ReportSyntaxError(AssemblyInfo.Instance.Resources.GetString("SemanticParserError_ExpressionExpected"));
					}
					statement = new StatementExpression(new AssignmentExpression(operatorType, leftExpression, rightExpression, new TextRange(startOffset, this.Token.EndOffset)));
					this.MatchStatementTerminator();
				}
				else if (leftExpression is InvocationExpression) {
					// InvocationStatement
					statement = new StatementExpression(leftExpression, new TextRange(startOffset, this.Token.EndOffset));
				}
				else if (true) {
					// Error recovery: Go to the next statement terminator
					this.ReportSyntaxError(AssemblyInfo.Instance.Resources.GetString("SemanticParserError_StatementExpected"));
					return false;
				}
				else
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.If)) {
				// BlockIfStatement/LineIfThenStatement via ConditionalStatement/IfStatement
				Expression condition;
				Statement trueStatement = null;
				Statement falseStatement = null;
				AstNodeList elseIfSections = new AstNodeList(null);
				bool hasThen = false;
				if (!this.Match(VBTokenID.If))
					return false;
				if (!this.MatchExpression(out condition)) {
					this.AdvanceToNextEnd(VBTokenID.If, true); return false;
				}
				if (this.TokenIs(this.LookAheadToken, VBTokenID.Then)) {
					if (this.Match(VBTokenID.Then)) {
						hasThen = true;
					}
				}
				if (hasThen) {
					if ((this.IsInMultiMatchSet(16, this.LookAheadToken) || (this.IsEndStatement()) || ((this.AreNextTwoIdentifierAnd(VBTokenID.Colon)) || (this.AreNextTwo(VBTokenID.DecimalIntegerLiteral, VBTokenID.Colon))) || (this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression()) || ((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis }))))) {
						if (!this.MatchStatements(out trueStatement))
							return false;
						if (this.TokenIs(this.LookAheadToken, VBTokenID.Else)) {
							if (!this.Match(VBTokenID.Else))
								return false;
							if (!this.MatchStatements(out falseStatement))
								return false;
						}
						statement = new IfStatement(condition, trueStatement, falseStatement, new TextRange(startOffset, this.Token.EndOffset));
						return true;
					}
				}
				this.MatchStatementTerminator();
				if ((this.IsInMultiMatchSet(16, this.LookAheadToken) || (this.IsEndStatement()) || ((this.AreNextTwoIdentifierAnd(VBTokenID.Colon)) || (this.AreNextTwo(VBTokenID.DecimalIntegerLiteral, VBTokenID.Colon))) || (this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression()) || ((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis }))))) {
					if (!this.MatchBlock(out trueStatement)) {
						this.AdvanceToNextEnd(VBTokenID.If, true); return false;
					}
				}
				while ((this.TokenIs(this.LookAheadToken, VBTokenID.ElseIf)) || (this.AreNextTwo(VBTokenID.Else, VBTokenID.If))) {
					Expression elseIfCondition;
					Statement elseIfStatement = null;
					int elseIfStartOffset = this.LookAheadToken.StartOffset;
					if (this.TokenIs(this.LookAheadToken, VBTokenID.ElseIf)) {
						if (!this.Match(VBTokenID.ElseIf))
							return false;
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.Else)) {
						if (!this.Match(VBTokenID.Else))
							return false;
						if (!this.Match(VBTokenID.If))
							return false;
					}
					else
						return false;
					if (!this.MatchExpression(out elseIfCondition)) {
						this.AdvanceToNextEnd(VBTokenID.If, true); return false;
					}
					if (this.TokenIs(this.LookAheadToken, VBTokenID.Then)) {
						this.Match(VBTokenID.Then);
					}
					this.MatchStatementTerminator();
					if ((this.IsInMultiMatchSet(16, this.LookAheadToken) || (this.IsEndStatement()) || ((this.AreNextTwoIdentifierAnd(VBTokenID.Colon)) || (this.AreNextTwo(VBTokenID.DecimalIntegerLiteral, VBTokenID.Colon))) || (this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression()) || ((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis }))))) {
						if (!this.MatchBlock(out elseIfStatement)) {
							this.AdvanceToNextEnd(VBTokenID.If, true); return false;
						}
					}
					elseIfSections.Add(new ElseIfSection(elseIfCondition, elseIfStatement, new TextRange(elseIfStartOffset, this.Token.EndOffset)));
				}
				if (this.TokenIs(this.LookAheadToken, VBTokenID.Else)) {
					if (!this.Match(VBTokenID.Else))
						return false;
					this.MatchStatementTerminator();
					if ((this.IsInMultiMatchSet(16, this.LookAheadToken) || (this.IsEndStatement()) || ((this.AreNextTwoIdentifierAnd(VBTokenID.Colon)) || (this.AreNextTwo(VBTokenID.DecimalIntegerLiteral, VBTokenID.Colon))) || (this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression()) || ((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis }))))) {
						if (!this.MatchBlock(out falseStatement)) {
							this.AdvanceToNextEnd(VBTokenID.If, true); return false;
						}
					}
				}
				this.AdvanceToNextEnd(VBTokenID.If, true);
				statement = new IfStatement(condition, trueStatement, falseStatement, new TextRange(startOffset, this.Token.EndOffset));
				if (elseIfSections.Count > 0)
					((IfStatement)statement).ElseIfSections.AddRange(elseIfSections.ToArray());
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Select)) {
				// SelectStatement via ConditionalStatement
				Expression expression;
				AstNodeList sections = new AstNodeList(null);
				if (!this.Match(VBTokenID.Select))
					return false;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.Case)) {
					this.Match(VBTokenID.Case);
				}
				if (!this.MatchExpression(out expression)) {
					this.AdvanceToNextEnd(VBTokenID.Select, true); return false;
				}
				this.MatchStatementTerminator();
				while ((this.TokenIs(this.LookAheadToken, VBTokenID.Case)) && (!this.TokenIs(this.GetLookAheadToken(2), VBTokenID.Else))) {
					SwitchSection switchSection = new SwitchSection();
					switchSection.StartOffset = this.LookAheadToken.StartOffset;
					Statement embeddedStatement;
					if (!this.Match(VBTokenID.Case))
						return false;
					if (!this.MatchCaseClause(switchSection))
						return false;
					while (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
						if (!this.Match(VBTokenID.Comma))
							return false;
						if (!this.MatchCaseClause(switchSection))
							return false;
					}
					this.MatchStatementTerminator();
					if ((this.IsInMultiMatchSet(16, this.LookAheadToken) || (this.IsEndStatement()) || ((this.AreNextTwoIdentifierAnd(VBTokenID.Colon)) || (this.AreNextTwo(VBTokenID.DecimalIntegerLiteral, VBTokenID.Colon))) || (this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression()) || ((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis }))))) {
						if (!this.MatchBlock(out embeddedStatement)) {
							this.AdvanceToNextEnd(VBTokenID.Select, true); return false;
						}
						else {
							switchSection.Statements.Add(embeddedStatement);
						}
					}
					switchSection.EndOffset = this.Token.EndOffset;
					sections.Add(switchSection);
					
					// Reap comments
					this.ReapComments(switchSection.Comments, false);
				}
				if (this.AreNextTwo(VBTokenID.Case, VBTokenID.Else)) {
					SwitchSection switchSection = new SwitchSection();
					switchSection.StartOffset = this.LookAheadToken.StartOffset;
					Statement embeddedStatement;
					if (!this.Match(VBTokenID.Case))
						return false;
					if (!this.Match(VBTokenID.Else))
						return false;
					this.MatchStatementTerminator();
					if ((this.IsInMultiMatchSet(16, this.LookAheadToken) || (this.IsEndStatement()) || ((this.AreNextTwoIdentifierAnd(VBTokenID.Colon)) || (this.AreNextTwo(VBTokenID.DecimalIntegerLiteral, VBTokenID.Colon))) || (this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression()) || ((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis }))))) {
						if (!this.MatchBlock(out embeddedStatement)) {
							this.AdvanceToNextEnd(VBTokenID.Select, true); return false;
						}
						else {
							switchSection.Statements.Add(embeddedStatement);
						}
					}
					switchSection.EndOffset = this.Token.EndOffset;
					sections.Add(switchSection);
					
					// Reap comments
					this.ReapComments(switchSection.Comments, false);
				}
				this.AdvanceToNextEnd(VBTokenID.Select, true);
				statement = new SwitchStatement(expression, new TextRange(startOffset, this.Token.EndOffset));
				((SwitchStatement)statement).Sections.AddRange(sections.ToArray());
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.While)) {
				// WhileStatement via LoopStatement
				Expression expression;
				Statement embeddedStatement = null;
				if (!this.Match(VBTokenID.While))
					return false;
				if (!this.MatchExpression(out expression)) {
					this.AdvanceToNextEnd(VBTokenID.While, true); return false;
				}
				this.MatchStatementTerminator();
				if ((this.IsInMultiMatchSet(16, this.LookAheadToken) || (this.IsEndStatement()) || ((this.AreNextTwoIdentifierAnd(VBTokenID.Colon)) || (this.AreNextTwo(VBTokenID.DecimalIntegerLiteral, VBTokenID.Colon))) || (this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression()) || ((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis }))))) {
					if (!this.MatchBlock(out embeddedStatement)) {
						this.AdvanceToNextEnd(VBTokenID.While, true); return false;
					}
				}
				this.AdvanceToNextEnd(VBTokenID.While, true);
				statement = new WhileStatement(expression, embeddedStatement, new TextRange(startOffset, this.Token.EndOffset));
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Do)) {
				// DoTopLoopStatement/DoBottomLoopStatement via LoopStatement/DoLoopStatement
				Expression expression = null;
				Statement embeddedStatement = null;
				bool isTopLoop = false;
				// bool isBottomLoop = false;	// NOTE: This variable determines whether the end result is a top or bottom loop
				if (!this.Match(VBTokenID.Do))
					return false;
				if (((this.TokenIs(this.LookAheadToken, VBTokenID.While)) || (this.TokenIs(this.LookAheadToken, VBTokenID.Until)))) {
					isTopLoop = true;
					if (this.TokenIs(this.LookAheadToken, VBTokenID.While)) {
						if (!this.Match(VBTokenID.While))
							return false;
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.Until)) {
						if (!this.Match(VBTokenID.Until))
							return false;
					}
					else
						return false;
					if (!this.MatchExpression(out expression))
						return false;
				}
				this.MatchStatementTerminator();
				if ((this.IsInMultiMatchSet(16, this.LookAheadToken) || (this.IsEndStatement()) || ((this.AreNextTwoIdentifierAnd(VBTokenID.Colon)) || (this.AreNextTwo(VBTokenID.DecimalIntegerLiteral, VBTokenID.Colon))) || (this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression()) || ((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis }))))) {
					if (!this.MatchBlock(out embeddedStatement))
						return false;
				}
				if (!this.Match(VBTokenID.Loop))
					return false;
				if (!isTopLoop) {
					if (((this.TokenIs(this.LookAheadToken, VBTokenID.While)) || (this.TokenIs(this.LookAheadToken, VBTokenID.Until)))) {
						// isBottomLoop = true;
						if (this.TokenIs(this.LookAheadToken, VBTokenID.While)) {
							if (!this.Match(VBTokenID.While))
								return false;
						}
						else if (this.TokenIs(this.LookAheadToken, VBTokenID.Until)) {
							if (!this.Match(VBTokenID.Until))
								return false;
						}
						else
							return false;
						if (!this.MatchExpression(out expression))
							return false;
					}
				}
				statement = new DoStatement(embeddedStatement, expression, new TextRange(startOffset, this.Token.EndOffset));
				this.MatchStatementTerminator();
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.For)) {
				// ForStatement/ForEachStatement via LoopStatement
				IAstNode initializer;
				Statement embeddedStatement = null;
				bool isForEach = false;
				if (!this.Match(VBTokenID.For))
					return false;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.Each)) {
					if (this.Match(VBTokenID.Each)) {
						isForEach = true;
					}
				}
				initializer = new LocalVariableDeclaration();
				initializer.StartOffset = this.LookAheadToken.StartOffset;
				QualifiedIdentifier name;
				int[] arrayRanks = null;
				TypeReference typeReference = new TypeReference("System.Object", TextRange.Deleted);
				if (!this.MatchIdentifier(out name))
					return false;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.QuestionMark)) {
					if (this.TokenIs(this.LookAheadToken, VBTokenID.QuestionMark)) {
						if (!this.Match(VBTokenID.QuestionMark))
							return false;
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.OpenParenthesis)) {
						bool isArrayTypeModifier = false;
						while (this.IsArrayTypeModifier()) {
							if (!this.MatchArrayTypeModifier(out arrayRanks))
								return false;
							else {
								isArrayTypeModifier = true;
							}
						}
						if (!isArrayTypeModifier) {
							if (!this.MatchArraySizeInitializationModifier(out arrayRanks))
								return false;
						}
					}
					else
						return false;
				}
				// NOTE: If Expression is later allowed before, this should not be optional
				if (this.TokenIs(this.LookAheadToken, VBTokenID.As)) {
					if (!this.Match(VBTokenID.As))
						return false;
					if (!this.MatchTypeName(out typeReference, false))
						return false;
					else {
						typeReference.ArrayRanks = arrayRanks;
					}
				}
				initializer.EndOffset = this.Token.EndOffset;
				if (typeReference != null)
					((LocalVariableDeclaration)initializer).Variables.Add(new VariableDeclarator(typeReference, name, false, true));
				else
					initializer = new SimpleName(name.Text, name.TextRange);
				// NOTE: This is currently left out due to Identifier ambiguity with the above
				// | (
				// <%
				// Expression expression;
				// %>
				// "Expression<@ out expression @><+ initializer = expression; +>"
				// )
				if (isForEach) {
					Expression variableDeclaration;
					Expression expression;
					if (!this.Match(VBTokenID.In))
						return false;
					if (!this.MatchExpression(out variableDeclaration))
						return false;
					this.MatchStatementTerminator();
					if ((this.IsInMultiMatchSet(16, this.LookAheadToken) || (this.IsEndStatement()) || ((this.AreNextTwoIdentifierAnd(VBTokenID.Colon)) || (this.AreNextTwo(VBTokenID.DecimalIntegerLiteral, VBTokenID.Colon))) || (this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression()) || ((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis }))))) {
						if (!this.MatchBlock(out embeddedStatement))
							return false;
					}
					if (!this.Match(VBTokenID.Next))
						return false;
					while ((this.IsInMultiMatchSet(23, this.LookAheadToken) || (this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression()) || ((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis }))))) {
						if (!this.MatchExpression(out expression))
							return false;
					}
					statement = new ForEachStatement(initializer, variableDeclaration, embeddedStatement, new TextRange(startOffset, this.Token.EndOffset));
					this.MatchStatementTerminator();
				}
				else {
					statement = new ForStatement();
					statement.StartOffset = startOffset;
					((ForStatement)statement).Initializers.Add(initializer);
					Expression expression;
					if (!this.Match(VBTokenID.Equality))
						return false;
					if (!this.MatchExpression(out expression))
						return false;
					else {
						if (initializer is LocalVariableDeclaration)
							((VariableDeclarator)((LocalVariableDeclaration)initializer).Variables[0]).Initializer = expression;
					}
					if (!this.Match(VBTokenID.To))
						return false;
					if (!this.MatchExpression(out expression))
						return false;
					else {
						((ForStatement)statement).Condition = expression;
					}
					if (this.TokenIs(this.LookAheadToken, VBTokenID.Step)) {
						if (!this.Match(VBTokenID.Step))
							return false;
						if (!this.MatchExpression(out expression))
							return false;
						else {
							((ForStatement)statement).Iterators.Add(expression);
						}
					}
					this.MatchStatementTerminator();
					if ((this.IsInMultiMatchSet(16, this.LookAheadToken) || (this.IsEndStatement()) || ((this.AreNextTwoIdentifierAnd(VBTokenID.Colon)) || (this.AreNextTwo(VBTokenID.DecimalIntegerLiteral, VBTokenID.Colon))) || (this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression()) || ((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis }))))) {
						if (!this.MatchBlock(out embeddedStatement))
							return false;
						else {
							((ForStatement)statement).Statement = embeddedStatement;
						}
					}
					if (!this.Match(VBTokenID.Next))
						return false;
					if ((this.IsInMultiMatchSet(23, this.LookAheadToken) || (this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression()) || ((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis }))))) {
						if (!this.MatchExpression(out expression))
							return false;
						while (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
							if (!this.Match(VBTokenID.Comma))
								return false;
							if (!this.MatchExpression(out expression))
								return false;
						}
					}
					statement.EndOffset = this.Token.EndOffset;
					this.MatchStatementTerminator();
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Throw)) {
				// ThrowStatement via ErrorHandlingStatement/StructuredErrorStatement
				Expression expression = null;
				if (!this.Match(VBTokenID.Throw))
					return false;
				if ((this.IsInMultiMatchSet(23, this.LookAheadToken) || (this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression()) || ((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis }))))) {
					if (!this.MatchExpression(out expression))
						return false;
				}
				statement = new ThrowStatement(expression, new TextRange(startOffset, this.Token.EndOffset));
				this.MatchStatementTerminator();
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Try)) {
				// TryStatement via ErrorHandlingStatement/StructuredErrorStatement
				Statement tryBlock = null;
				Statement finallyBlock = null;
				AstNodeList catchClauses = new AstNodeList(null);
				if (!this.Match(VBTokenID.Try))
					return false;
				this.MatchStatementTerminator();
				if ((this.IsInMultiMatchSet(16, this.LookAheadToken) || (this.IsEndStatement()) || ((this.AreNextTwoIdentifierAnd(VBTokenID.Colon)) || (this.AreNextTwo(VBTokenID.DecimalIntegerLiteral, VBTokenID.Colon))) || (this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression()) || ((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis }))))) {
					if (!this.MatchBlock(out tryBlock))
						return false;
				}
				while (this.TokenIs(this.LookAheadToken, VBTokenID.Catch)) {
					VariableDeclarator variableDeclarator = null;
					QualifiedIdentifier variableName = null;
					TypeReference typeReference = new TypeReference("System.Object", TextRange.Deleted);
					Expression evaluationExpression = null;
					Statement catchBlock = null;
					int catchStartOffset = this.LookAheadToken.StartOffset;
					if (!this.Match(VBTokenID.Catch))
						return false;
					if (this.IsInMultiMatchSet(18, this.LookAheadToken)) {
						int variableStartOffset = this.LookAheadToken.StartOffset;
						if (!this.MatchIdentifier(out variableName))
							return false;
						if (this.TokenIs(this.LookAheadToken, VBTokenID.As)) {
							if (!this.Match(VBTokenID.As))
								return false;
							if (!this.MatchNonArrayTypeName(out typeReference, false))
								return false;
						}
						variableDeclarator = new VariableDeclarator(typeReference, variableName, false, true);
						variableDeclarator.TextRange = new TextRange(variableStartOffset, this.Token.EndOffset);
					}
					if (this.TokenIs(this.LookAheadToken, VBTokenID.When)) {
						if (!this.Match(VBTokenID.When))
							return false;
						if (!this.MatchExpression(out evaluationExpression))
							return false;
					}
					this.MatchStatementTerminator();
					if ((this.IsInMultiMatchSet(16, this.LookAheadToken) || (this.IsEndStatement()) || ((this.AreNextTwoIdentifierAnd(VBTokenID.Colon)) || (this.AreNextTwo(VBTokenID.DecimalIntegerLiteral, VBTokenID.Colon))) || (this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression()) || ((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis }))))) {
						if (!this.MatchBlock(out catchBlock))
							return false;
					}
					CatchClause catchClause = new CatchClause(variableDeclarator, (BlockStatement)catchBlock, new TextRange(catchStartOffset, this.Token.EndOffset));
					catchClause.EvaluationExpression = evaluationExpression;
					catchClauses.Add(catchClause);
				}
				if (this.TokenIs(this.LookAheadToken, VBTokenID.Finally)) {
					if (!this.Match(VBTokenID.Finally))
						return false;
					this.MatchStatementTerminator();
					if ((this.IsInMultiMatchSet(16, this.LookAheadToken) || (this.IsEndStatement()) || ((this.AreNextTwoIdentifierAnd(VBTokenID.Colon)) || (this.AreNextTwo(VBTokenID.DecimalIntegerLiteral, VBTokenID.Colon))) || (this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression()) || ((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis }))))) {
						if (!this.MatchBlock(out finallyBlock))
							return false;
					}
				}
				this.AdvanceToNextEnd(VBTokenID.Try, true);
				statement = new TryStatement(tryBlock, finallyBlock, new TextRange(startOffset, this.Token.EndOffset));
				if (catchClauses.Count > 0)
					((TryStatement)statement).CatchClauses.AddRange(catchClauses.ToArray());
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Error)) {
				// ErrorStatement via ErrorHandlingStatement/UnstructuredErrorStatement
				Expression expression = null;
				if (!this.Match(VBTokenID.Error))
					return false;
				if (!this.MatchExpression(out expression))
					return false;
				statement = new UnstructuredErrorErrorStatement(expression, new TextRange(startOffset, this.Token.EndOffset));
				this.MatchStatementTerminator();
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.On)) {
				// OnErrorStatement via ErrorHandlingStatement/UnstructuredErrorStatement
				Expression expression = null;
				UnstructuredErrorOnErrorStatementType actionType = UnstructuredErrorOnErrorStatementType.EstablishResumeNext;
				if (!this.Match(VBTokenID.On))
					return false;
				if (!this.Match(VBTokenID.Error))
					return false;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.GoTo)) {
					if (!this.Match(VBTokenID.GoTo))
						return false;
					if (this.TokenIs(this.LookAheadToken, VBTokenID.Subtraction)) {
						if (!this.Match(VBTokenID.Subtraction))
							return false;
						if (!this.Match(VBTokenID.DecimalIntegerLiteral))
							return false;
						else {
							actionType = UnstructuredErrorOnErrorStatementType.ResetException;
						}
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.DecimalIntegerLiteral)) {
						if (!this.Match(VBTokenID.DecimalIntegerLiteral))
							return false;
						else {
							actionType = UnstructuredErrorOnErrorStatementType.ResetExceptionHandlerLocation;
						}
					}
					else if ((((this.AreNextTwoIdentifierAnd(VBTokenID.Colon)) || (this.AreNextTwo(VBTokenID.DecimalIntegerLiteral, VBTokenID.Colon))))) {
						if (!this.MatchLabelName(out expression))
							return false;
						else {
							actionType = UnstructuredErrorOnErrorStatementType.EstablishHandlerLocation;
						}
					}
					else
						return false;
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.Resume)) {
					if (!this.Match(VBTokenID.Resume))
						return false;
					if (!this.Match(VBTokenID.Next))
						return false;
				}
				else
					return false;
				statement = new UnstructuredErrorOnErrorStatement(actionType, expression, new TextRange(startOffset, this.Token.EndOffset));
				this.MatchStatementTerminator();
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Resume)) {
				// ResumeStatement via ErrorHandlingStatement/UnstructuredErrorStatement
				Expression expression = null;
				if (!this.Match(VBTokenID.Resume))
					return false;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.Next)) {
					if (!this.Match(VBTokenID.Next))
						return false;
					if (!this.MatchLabelName(out expression))
						return false;
				}
				statement = new UnstructuredErrorResumeNextStatement(expression, new TextRange(startOffset, this.Token.EndOffset));
				this.MatchStatementTerminator();
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.GoTo)) {
				// GotoStatement
				Expression expression;
				if (!this.Match(VBTokenID.GoTo))
					return false;
				if (!this.MatchLabelName(out expression))
					return false;
				statement = new GotoStatement(null, expression, new TextRange(startOffset, this.Token.EndOffset));
				this.MatchStatementTerminator();
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Exit)) {
				// ExitStatement
				if (!this.Match(VBTokenID.Exit))
					return false;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.Do)) {
					if (!this.Match(VBTokenID.Do))
						return false;
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.For)) {
					if (!this.Match(VBTokenID.For))
						return false;
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.While)) {
					if (!this.Match(VBTokenID.While))
						return false;
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.Select)) {
					if (!this.Match(VBTokenID.Select))
						return false;
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.Sub)) {
					if (!this.Match(VBTokenID.Sub))
						return false;
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.Function)) {
					if (!this.Match(VBTokenID.Function))
						return false;
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.Property)) {
					if (!this.Match(VBTokenID.Property))
						return false;
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.Try)) {
					if (!this.Match(VBTokenID.Try))
						return false;
				}
				else
					return false;
				statement = new ExitStatement(new TextRange(startOffset, this.Token.EndOffset));
				this.MatchStatementTerminator();
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Continue)) {
				// ContinueStatement
				if (!this.Match(VBTokenID.Continue))
					return false;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.Do)) {
					if (!this.Match(VBTokenID.Do))
						return false;
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.For)) {
					if (!this.Match(VBTokenID.For))
						return false;
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.While)) {
					if (!this.Match(VBTokenID.While))
						return false;
				}
				else
					return false;
				statement = new ContinueStatement(new TextRange(startOffset, this.Token.EndOffset));
				this.MatchStatementTerminator();
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Stop)) {
				// StopStatement
				if (!this.Match(VBTokenID.Stop))
					return false;
				statement = new BranchStatement(BranchStatementType.Stop, new TextRange(startOffset, this.Token.EndOffset));
				this.MatchStatementTerminator();
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Return)) {
				// ReturnStatement
				Expression expression = null;
				if (!this.Match(VBTokenID.Return))
					return false;
				if ((this.IsInMultiMatchSet(23, this.LookAheadToken) || (this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression()) || ((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis }))))) {
					if (!this.MatchExpression(out expression))
						return false;
				}
				statement = new ReturnStatement(expression, new TextRange(startOffset, this.Token.EndOffset));
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.ReDim)) {
				// ReDimStatement (via ArrayHandlingStatement)
				statement = new ArrayRedimStatement();
				statement.StartOffset = startOffset;
				ArrayRedimClause redimClause;
				if (!this.Match(VBTokenID.ReDim))
					return false;
				if ((this.TokenIs(this.LookAheadToken, VBTokenID.Identifier)) && (this.LookAheadTokenText.ToLower() == "preserve")) {
					if (!this.MatchSimpleIdentifier())
						return false;
				}
				if (!this.MatchRedimClause(out redimClause))
					return false;
				else {
					((ArrayRedimStatement)statement).Clauses.Add(redimClause);
				}
				while (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
					if (!this.Match(VBTokenID.Comma))
						return false;
					if (!this.MatchRedimClause(out redimClause))
						return false;
					else {
						((ArrayRedimStatement)statement).Clauses.Add(redimClause);
					}
				}
				statement.EndOffset = this.Token.EndOffset;
				this.MatchStatementTerminator();
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Erase)) {
				// EraseStatement (via ArrayHandlingStatement)
				if (!this.Match(VBTokenID.Erase))
					return false;
				statement = new ArrayEraseStatement();
				statement.StartOffset = startOffset;
				Expression expression;
				if (!this.MatchExpression(out expression))
					return false;
				else {
					((ArrayEraseStatement)statement).Expressions.Add(expression);
				}
				while (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
					if (!this.Match(VBTokenID.Comma))
						return false;
					if (!this.MatchExpression(out expression))
						return false;
					else {
						((ArrayEraseStatement)statement).Expressions.Add(expression);
					}
				}
				statement.EndOffset = this.Token.EndOffset;
				this.MatchStatementTerminator();
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Using)) {
				// UsingStatement
				Statement embeddedStatement = null;
				Expression expression = null;
				AstNodeList resourceAcquisitions = new AstNodeList(null);
				if (!this.Match(VBTokenID.Using))
					return false;
				LocalVariableDeclaration declaration = new LocalVariableDeclaration();
				declaration.StartOffset = this.LookAheadToken.StartOffset;
				if (this.IsVariableDeclarator()) {
					if (!this.MatchVariableDeclarator(null, declaration))
						return false;
					while (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
						if (!this.Match(VBTokenID.Comma))
							return false;
						if (!this.MatchVariableDeclarator(null, declaration))
							return false;
					}
					declaration.EndOffset = this.Token.EndOffset;
					resourceAcquisitions.Add(declaration);
				}
				else {
					if (!this.MatchExpression(out expression))
						return false;
					else {
						resourceAcquisitions.Add(expression);
					}
				}
				this.MatchStatementTerminator();
				if ((this.IsInMultiMatchSet(16, this.LookAheadToken) || (this.IsEndStatement()) || ((this.AreNextTwoIdentifierAnd(VBTokenID.Colon)) || (this.AreNextTwo(VBTokenID.DecimalIntegerLiteral, VBTokenID.Colon))) || (this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression()) || ((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis }))))) {
					if (!this.MatchBlock(out embeddedStatement))
						return false;
				}
				this.AdvanceToNextEnd(VBTokenID.Using, true);
				statement = new UsingStatement(resourceAcquisitions, embeddedStatement, new TextRange(startOffset, this.Token.EndOffset));
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>Block</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>Block</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Dim</c>, <c>Static</c>, <c>Global</c>, <c>OpenParenthesis</c>, <c>OpenCurlyBrace</c>, <c>New</c>, <c>XmlLiteral</c>, <c>Object</c>, <c>Single</c>, <c>Double</c>, <c>Decimal</c>, <c>Boolean</c>, <c>Date</c>, <c>Char</c>, <c>String</c>, <c>Byte</c>, <c>SByte</c>, <c>UShort</c>, <c>Short</c>, <c>UInteger</c>, <c>Integer</c>, <c>ULong</c>, <c>Long</c>, <c>Const</c>, <c>AddHandler</c>, <c>RemoveHandler</c>, <c>RaiseEvent</c>, <c>Sub</c>, <c>Function</c>, <c>CType</c>, <c>StringLiteral</c>, <c>MyBase</c>, <c>Me</c>, <c>DecimalIntegerLiteral</c>, <c>With</c>, <c>SyncLock</c>, <c>Mid</c>, <c>Call</c>, <c>If</c>, <c>Select</c>, <c>While</c>, <c>Do</c>, <c>For</c>, <c>Throw</c>, <c>Try</c>, <c>Error</c>, <c>On</c>, <c>GoTo</c>, <c>Resume</c>, <c>Exit</c>, <c>Continue</c>, <c>Stop</c>, <c>Return</c>, <c>ReDim</c>, <c>Erase</c>, <c>Using</c>, <c>True</c>, <c>False</c>, <c>HexadecimalIntegerLiteral</c>, <c>OctalIntegerLiteral</c>, <c>FloatingPointLiteral</c>, <c>CharacterLiteral</c>, <c>DateLiteral</c>, <c>Nothing</c>, <c>AddressOf</c>, <c>GetTypeKeyword</c>, <c>TypeOf</c>, <c>GetXmlNamespace</c>, <c>MyClass</c>, <c>DirectCast</c>, <c>TryCast</c>, <c>IIf</c>, <c>CBool</c>, <c>CByte</c>, <c>CChar</c>, <c>CDate</c>, <c>CDec</c>, <c>CDbl</c>, <c>CInt</c>, <c>CLng</c>, <c>CObj</c>, <c>CSByte</c>, <c>CShort</c>, <c>CSng</c>, <c>CStr</c>, <c>CUInt</c>, <c>CULng</c>, <c>CUShort</c>.
		/// The non-terminal can start with: this.IsEndStatement().
		/// The non-terminal can start with: (this.AreNextTwoIdentifierAnd(VBTokenID.Colon)) || (this.AreNextTwo(VBTokenID.DecimalIntegerLiteral, VBTokenID.Colon)).
		/// The non-terminal can start with: this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression().
		/// The non-terminal can start with: (this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) &amp;&amp; (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis })).
		/// </remarks>
		protected virtual bool MatchBlock(out Statement block) {
			block = new BlockStatement();
			block.StartOffset = this.LookAheadToken.StartOffset;
			Statement statement;
			
			bool errorReported = false;
			while (!this.IsAtEnd) {
				if (this.IsBlockTerminator())
					break;
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
					// Skip over line terminators
					this.AdvancePastTerminators();
				}
				else if ((this.IsInMultiMatchSet(26, this.LookAheadToken) || (this.IsEndStatement()) || ((this.AreNextTwoIdentifierAnd(VBTokenID.Colon)) || (this.AreNextTwo(VBTokenID.DecimalIntegerLiteral, VBTokenID.Colon))) || (this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression()) || ((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis }))))) {
					errorReported = false;
					while ((this.IsInMultiMatchSet(16, this.LookAheadToken) || (this.IsEndStatement()) || ((this.AreNextTwoIdentifierAnd(VBTokenID.Colon)) || (this.AreNextTwo(VBTokenID.DecimalIntegerLiteral, VBTokenID.Colon))) || (this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression()) || ((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis }))))) {
						if (!this.MatchStatements(out statement)) {
							// Error recovery:  Advance to the next statement terminator since nothing was matched or quit if at a block terminator
							if (!this.IsBlockTerminator())
								this.AdvanceToNextStatementTerminator();
							else
								break;
						}
						else {
							((BlockStatement)block).Statements.Add(statement);
						}
					}
				}
				else {
					// Error recovery:  Advance to the next statement terminator since nothing was matched
					if (!errorReported) {
						this.ReportSyntaxError(AssemblyInfo.Instance.Resources.GetString("SemanticParserError_StatementExpected"));
						errorReported = true;
					}
					this.AdvanceToNextStatementTerminator();
				}
			}
			
			// Reap comments
			this.ReapComments(((BlockStatement)block).Comments, false);
			
			block.EndOffset = this.Token.EndOffset;
			return true;
		}

		/// <summary>
		/// Matches a <c>LabelName</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>LabelName</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: (this.AreNextTwoIdentifierAnd(VBTokenID.Colon)) || (this.AreNextTwo(VBTokenID.DecimalIntegerLiteral, VBTokenID.Colon)).
		/// </remarks>
		protected virtual bool MatchLabelName(out Expression expression) {
			expression = null;
			if (this.IsInMultiMatchSet(1, this.LookAheadToken)) {
				if (!this.MatchSimpleIdentifier())
					return false;
				else {
					expression = new SimpleName(this.TokenText, this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.DecimalIntegerLiteral)) {
				if (!this.Match(VBTokenID.DecimalIntegerLiteral))
					return false;
				else {
					expression = new LiteralExpression(LiteralType.DecimalInteger, this.TokenText, this.Token.TextRange);
				}
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>Statements</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>Statements</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Dim</c>, <c>Static</c>, <c>Global</c>, <c>OpenParenthesis</c>, <c>OpenCurlyBrace</c>, <c>New</c>, <c>XmlLiteral</c>, <c>Object</c>, <c>Single</c>, <c>Double</c>, <c>Decimal</c>, <c>Boolean</c>, <c>Date</c>, <c>Char</c>, <c>String</c>, <c>Byte</c>, <c>SByte</c>, <c>UShort</c>, <c>Short</c>, <c>UInteger</c>, <c>Integer</c>, <c>ULong</c>, <c>Long</c>, <c>Const</c>, <c>AddHandler</c>, <c>RemoveHandler</c>, <c>RaiseEvent</c>, <c>Sub</c>, <c>Function</c>, <c>CType</c>, <c>StringLiteral</c>, <c>MyBase</c>, <c>Me</c>, <c>DecimalIntegerLiteral</c>, <c>With</c>, <c>SyncLock</c>, <c>Mid</c>, <c>Call</c>, <c>If</c>, <c>Select</c>, <c>While</c>, <c>Do</c>, <c>For</c>, <c>Throw</c>, <c>Try</c>, <c>Error</c>, <c>On</c>, <c>GoTo</c>, <c>Resume</c>, <c>Exit</c>, <c>Continue</c>, <c>Stop</c>, <c>Return</c>, <c>ReDim</c>, <c>Erase</c>, <c>Using</c>, <c>True</c>, <c>False</c>, <c>HexadecimalIntegerLiteral</c>, <c>OctalIntegerLiteral</c>, <c>FloatingPointLiteral</c>, <c>CharacterLiteral</c>, <c>DateLiteral</c>, <c>Nothing</c>, <c>AddressOf</c>, <c>GetTypeKeyword</c>, <c>TypeOf</c>, <c>GetXmlNamespace</c>, <c>MyClass</c>, <c>DirectCast</c>, <c>TryCast</c>, <c>IIf</c>, <c>CBool</c>, <c>CByte</c>, <c>CChar</c>, <c>CDate</c>, <c>CDec</c>, <c>CDbl</c>, <c>CInt</c>, <c>CLng</c>, <c>CObj</c>, <c>CSByte</c>, <c>CShort</c>, <c>CSng</c>, <c>CStr</c>, <c>CUInt</c>, <c>CULng</c>, <c>CUShort</c>.
		/// The non-terminal can start with: this.IsEndStatement().
		/// The non-terminal can start with: (this.AreNextTwoIdentifierAnd(VBTokenID.Colon)) || (this.AreNextTwo(VBTokenID.DecimalIntegerLiteral, VBTokenID.Colon)).
		/// The non-terminal can start with: this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression().
		/// The non-terminal can start with: (this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) &amp;&amp; (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis })).
		/// </remarks>
		protected virtual bool MatchStatements(out Statement statement) {
			if (!this.MatchStatement(out statement))
				return false;
			while (this.TokenIs(this.LookAheadToken, VBTokenID.Colon)) {
				Statement otherStatement;
				if (!this.Match(VBTokenID.Colon))
					return false;
				if (!this.MatchStatement(out otherStatement))
					return false;
				BlockStatement blockStatement = statement as BlockStatement;
				if (blockStatement == null) {
					blockStatement = new BlockStatement();
					blockStatement.StartOffset = statement.StartOffset;
					blockStatement.Statements.Add(statement);
				}
				blockStatement.Statements.Add(otherStatement);
				blockStatement.EndOffset = otherStatement.EndOffset;
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>CaseClause</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>CaseClause</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Global</c>, <c>OpenParenthesis</c>, <c>OpenCurlyBrace</c>, <c>New</c>, <c>LessThan</c>, <c>GreaterThan</c>, <c>XmlLiteral</c>, <c>Equality</c>, <c>Object</c>, <c>Single</c>, <c>Double</c>, <c>Decimal</c>, <c>Boolean</c>, <c>Date</c>, <c>Char</c>, <c>String</c>, <c>Byte</c>, <c>SByte</c>, <c>UShort</c>, <c>Short</c>, <c>UInteger</c>, <c>Integer</c>, <c>ULong</c>, <c>Long</c>, <c>Sub</c>, <c>Function</c>, <c>Addition</c>, <c>Subtraction</c>, <c>Inequality</c>, <c>GreaterThanOrEqual</c>, <c>LessThanOrEqual</c>, <c>Not</c>, <c>CType</c>, <c>StringLiteral</c>, <c>MyBase</c>, <c>Me</c>, <c>DecimalIntegerLiteral</c>, <c>Mid</c>, <c>Is</c>, <c>True</c>, <c>False</c>, <c>HexadecimalIntegerLiteral</c>, <c>OctalIntegerLiteral</c>, <c>FloatingPointLiteral</c>, <c>CharacterLiteral</c>, <c>DateLiteral</c>, <c>Nothing</c>, <c>AddressOf</c>, <c>GetTypeKeyword</c>, <c>TypeOf</c>, <c>GetXmlNamespace</c>, <c>MyClass</c>, <c>DirectCast</c>, <c>TryCast</c>, <c>IIf</c>, <c>CBool</c>, <c>CByte</c>, <c>CChar</c>, <c>CDate</c>, <c>CDec</c>, <c>CDbl</c>, <c>CInt</c>, <c>CLng</c>, <c>CObj</c>, <c>CSByte</c>, <c>CShort</c>, <c>CSng</c>, <c>CStr</c>, <c>CUInt</c>, <c>CULng</c>, <c>CUShort</c>.
		/// The non-terminal can start with: this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression().
		/// The non-terminal can start with: (this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) &amp;&amp; (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis })).
		/// </remarks>
		protected virtual bool MatchCaseClause(SwitchSection switchSection) {
			Expression expression;
			int startOffset = this.LookAheadToken.StartOffset;
			OperatorType operatorType = OperatorType.None;
			if (this.IsInMultiMatchSet(27, this.LookAheadToken)) {
				if (this.TokenIs(this.LookAheadToken, VBTokenID.Is)) {
					this.Match(VBTokenID.Is);
				}
				if (this.TokenIs(this.LookAheadToken, VBTokenID.Equality)) {
					if (!this.Match(VBTokenID.Equality))
						return false;
					else {
						operatorType = OperatorType.Equality;
					}
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.Inequality)) {
					if (!this.Match(VBTokenID.Inequality))
						return false;
					else {
						operatorType = OperatorType.Inequality;
					}
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.GreaterThan)) {
					if (!this.Match(VBTokenID.GreaterThan))
						return false;
					else {
						operatorType = OperatorType.GreaterThan;
					}
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.LessThan)) {
					if (!this.Match(VBTokenID.LessThan))
						return false;
					else {
						operatorType = OperatorType.LessThan;
					}
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.GreaterThanOrEqual)) {
					if (!this.Match(VBTokenID.GreaterThanOrEqual))
						return false;
					else {
						operatorType = OperatorType.GreaterThanOrEqual;
					}
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.LessThanOrEqual)) {
					if (!this.Match(VBTokenID.LessThanOrEqual))
						return false;
					else {
						operatorType = OperatorType.LessThanOrEqual;
					}
				}
				else
					return false;
				if (!this.MatchExpression(out expression))
					return false;
				else {
					expression = new UnaryExpression(operatorType, expression);
				}
			}
			else if ((this.IsInMultiMatchSet(4, this.LookAheadToken) || (this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression()) || ((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis }))))) {
				if (!this.MatchExpression(out expression))
					return false;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.To)) {
					Expression rightExpression;
					if (!this.Match(VBTokenID.To))
						return false;
					if (!this.MatchExpression(out rightExpression))
						return false;
					else {
						expression = new BinaryExpression(operatorType, expression, rightExpression, new TextRange(expression.StartOffset, this.Token.EndOffset));
					}
				}
			}
			else
				return false;
			switchSection.Labels.Add(new SwitchLabel(expression, new TextRange(startOffset, this.Token.EndOffset)));
			return true;
		}

		/// <summary>
		/// Matches a <c>RedimClause</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>RedimClause</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Global</c>, <c>OpenParenthesis</c>, <c>OpenCurlyBrace</c>, <c>New</c>, <c>XmlLiteral</c>, <c>Object</c>, <c>Single</c>, <c>Double</c>, <c>Decimal</c>, <c>Boolean</c>, <c>Date</c>, <c>Char</c>, <c>String</c>, <c>Byte</c>, <c>SByte</c>, <c>UShort</c>, <c>Short</c>, <c>UInteger</c>, <c>Integer</c>, <c>ULong</c>, <c>Long</c>, <c>Sub</c>, <c>Function</c>, <c>CType</c>, <c>StringLiteral</c>, <c>MyBase</c>, <c>Me</c>, <c>DecimalIntegerLiteral</c>, <c>Mid</c>, <c>True</c>, <c>False</c>, <c>HexadecimalIntegerLiteral</c>, <c>OctalIntegerLiteral</c>, <c>FloatingPointLiteral</c>, <c>CharacterLiteral</c>, <c>DateLiteral</c>, <c>Nothing</c>, <c>AddressOf</c>, <c>GetTypeKeyword</c>, <c>TypeOf</c>, <c>GetXmlNamespace</c>, <c>MyClass</c>, <c>DirectCast</c>, <c>TryCast</c>, <c>IIf</c>, <c>CBool</c>, <c>CByte</c>, <c>CChar</c>, <c>CDate</c>, <c>CDec</c>, <c>CDbl</c>, <c>CInt</c>, <c>CLng</c>, <c>CObj</c>, <c>CSByte</c>, <c>CShort</c>, <c>CSng</c>, <c>CStr</c>, <c>CUInt</c>, <c>CULng</c>, <c>CUShort</c>.
		/// The non-terminal can start with: this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression().
		/// The non-terminal can start with: (this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) &amp;&amp; (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis })).
		/// </remarks>
		protected virtual bool MatchRedimClause(out ArrayRedimClause redimClause) {
			redimClause = null;
			int startOffset = this.LookAheadToken.StartOffset;
			Expression expression;
			int[] arrayRanks;
			if (!this.MatchPrimaryExpression(false, out expression))
				return false;
			if (!this.MatchArraySizeInitializationModifier(out arrayRanks))
				return false;
			redimClause = new ArrayRedimClause(expression, arrayRanks, new TextRange(startOffset, this.Token.EndOffset));
			return true;
		}

		/// <summary>
		/// Matches a <c>Expression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>Expression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Global</c>, <c>OpenParenthesis</c>, <c>OpenCurlyBrace</c>, <c>New</c>, <c>XmlLiteral</c>, <c>Object</c>, <c>Single</c>, <c>Double</c>, <c>Decimal</c>, <c>Boolean</c>, <c>Date</c>, <c>Char</c>, <c>String</c>, <c>Byte</c>, <c>SByte</c>, <c>UShort</c>, <c>Short</c>, <c>UInteger</c>, <c>Integer</c>, <c>ULong</c>, <c>Long</c>, <c>Sub</c>, <c>Function</c>, <c>Addition</c>, <c>Subtraction</c>, <c>Not</c>, <c>CType</c>, <c>StringLiteral</c>, <c>MyBase</c>, <c>Me</c>, <c>DecimalIntegerLiteral</c>, <c>Mid</c>, <c>True</c>, <c>False</c>, <c>HexadecimalIntegerLiteral</c>, <c>OctalIntegerLiteral</c>, <c>FloatingPointLiteral</c>, <c>CharacterLiteral</c>, <c>DateLiteral</c>, <c>Nothing</c>, <c>AddressOf</c>, <c>GetTypeKeyword</c>, <c>TypeOf</c>, <c>GetXmlNamespace</c>, <c>MyClass</c>, <c>DirectCast</c>, <c>TryCast</c>, <c>IIf</c>, <c>CBool</c>, <c>CByte</c>, <c>CChar</c>, <c>CDate</c>, <c>CDec</c>, <c>CDbl</c>, <c>CInt</c>, <c>CLng</c>, <c>CObj</c>, <c>CSByte</c>, <c>CShort</c>, <c>CSng</c>, <c>CStr</c>, <c>CUInt</c>, <c>CULng</c>, <c>CUShort</c>.
		/// The non-terminal can start with: this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression().
		/// The non-terminal can start with: (this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) &amp;&amp; (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis })).
		/// </remarks>
		protected virtual bool MatchExpression(out Expression expression) {
			if (!this.MatchExclusiveDisjunctionExpression(out expression))
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>PrimaryExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>PrimaryExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Global</c>, <c>OpenParenthesis</c>, <c>OpenCurlyBrace</c>, <c>New</c>, <c>XmlLiteral</c>, <c>Object</c>, <c>Single</c>, <c>Double</c>, <c>Decimal</c>, <c>Boolean</c>, <c>Date</c>, <c>Char</c>, <c>String</c>, <c>Byte</c>, <c>SByte</c>, <c>UShort</c>, <c>Short</c>, <c>UInteger</c>, <c>Integer</c>, <c>ULong</c>, <c>Long</c>, <c>Sub</c>, <c>Function</c>, <c>CType</c>, <c>StringLiteral</c>, <c>MyBase</c>, <c>Me</c>, <c>DecimalIntegerLiteral</c>, <c>Mid</c>, <c>True</c>, <c>False</c>, <c>HexadecimalIntegerLiteral</c>, <c>OctalIntegerLiteral</c>, <c>FloatingPointLiteral</c>, <c>CharacterLiteral</c>, <c>DateLiteral</c>, <c>Nothing</c>, <c>AddressOf</c>, <c>GetTypeKeyword</c>, <c>TypeOf</c>, <c>GetXmlNamespace</c>, <c>MyClass</c>, <c>DirectCast</c>, <c>TryCast</c>, <c>IIf</c>, <c>CBool</c>, <c>CByte</c>, <c>CChar</c>, <c>CDate</c>, <c>CDec</c>, <c>CDbl</c>, <c>CInt</c>, <c>CLng</c>, <c>CObj</c>, <c>CSByte</c>, <c>CShort</c>, <c>CSng</c>, <c>CStr</c>, <c>CUInt</c>, <c>CULng</c>, <c>CUShort</c>.
		/// The non-terminal can start with: this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression().
		/// The non-terminal can start with: (this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) &amp;&amp; (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis })).
		/// </remarks>
		protected virtual bool MatchPrimaryExpression(bool allowParenMatchAtEnd, out Expression expression) {
			expression = null;
			int startOffset = this.LookAheadToken.StartOffset;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.True)) {
				if (!this.Match(VBTokenID.True))
					return false;
				else {
					expression = new LiteralExpression(LiteralType.True, null, this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.False)) {
				if (!this.Match(VBTokenID.False))
					return false;
				else {
					expression = new LiteralExpression(LiteralType.False, null, this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.DecimalIntegerLiteral)) {
				if (!this.Match(VBTokenID.DecimalIntegerLiteral))
					return false;
				else {
					expression = new LiteralExpression(LiteralType.DecimalInteger, this.TokenText, this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.HexadecimalIntegerLiteral)) {
				if (!this.Match(VBTokenID.HexadecimalIntegerLiteral))
					return false;
				else {
					expression = new LiteralExpression(LiteralType.HexadecimalInteger, this.TokenText, this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.OctalIntegerLiteral)) {
				if (!this.Match(VBTokenID.OctalIntegerLiteral))
					return false;
				else {
					expression = new LiteralExpression(LiteralType.OctalInteger, this.TokenText, this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.FloatingPointLiteral)) {
				if (!this.Match(VBTokenID.FloatingPointLiteral))
					return false;
				else {
					expression = new LiteralExpression(LiteralType.Real, this.TokenText, this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.CharacterLiteral)) {
				if (!this.Match(VBTokenID.CharacterLiteral))
					return false;
				else {
					expression = new LiteralExpression(LiteralType.Character, this.TokenText, this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.StringLiteral)) {
				if (!this.Match(VBTokenID.StringLiteral))
					return false;
				else {
					expression = new LiteralExpression(LiteralType.String, this.TokenText, this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.DateLiteral)) {
				if (!this.Match(VBTokenID.DateLiteral))
					return false;
				else {
					expression = new LiteralExpression(LiteralType.Date, this.TokenText, this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.XmlLiteral)) {
				if (!this.Match(VBTokenID.XmlLiteral))
					return false;
				else {
					expression = new LiteralExpression(LiteralType.Xml, this.TokenText, this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Nothing)) {
				if (!this.Match(VBTokenID.Nothing))
					return false;
				else {
					expression = new LiteralExpression(LiteralType.Null, null, this.Token.TextRange);
				}
			}
			else if ((((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis }))))) {
				if (!this.MatchQueryExpression(out expression))
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.OpenParenthesis)) {
				// ParenthesizedExpression
				if (!this.Match(VBTokenID.OpenParenthesis))
					return false;
				if (!this.MatchExpression(out expression))
					return false;
				if (!this.Match(VBTokenID.CloseParenthesis))
					return false;
				expression = new ParenthesizedExpression(expression, new TextRange(startOffset, this.Token.EndOffset));
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Me)) {
				// InstanceExpression
				if (!this.Match(VBTokenID.Me))
					return false;
				else {
					expression = new ThisAccess(this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Mid)) {
				if (!this.Match(VBTokenID.Mid))
					return false;
				else {
					expression = new SimpleName(this.TokenText, this.Token.TextRange);
				}
			}
			else if (this.IsInMultiMatchSet(1, this.LookAheadToken)) {
				// SimpleNameExpression
				if (!this.MatchSimpleIdentifier())
					return false;
				else {
					expression = new SimpleName(this.TokenText, this.Token.TextRange);
				}
				if (this.AreNextTwo(VBTokenID.OpenParenthesis, VBTokenID.Of)) {
					if (!this.Match(VBTokenID.OpenParenthesis))
						return false;
					if (!this.Match(VBTokenID.Of))
						return false;
					AstNodeList typeArgumentList;
					if (!this.MatchTypeArgumentList(out typeArgumentList))
						return false;
					else {
						if (typeArgumentList != null)
							expression.GenericTypeArguments.AddRange(typeArgumentList.ToArray());
					}
					if (!this.Match(VBTokenID.CloseParenthesis))
						return false;
				}
				// This occurs when there is a call like this without parens:  MyMethod
				if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
					expression = new InvocationExpression(expression);
					expression.TextRange = ((InvocationExpression)expression).Expression.TextRange;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.AddressOf)) {
				// AddressOfExpression
				if (!this.Match(VBTokenID.AddressOf))
					return false;
				if (!this.MatchExpression(out expression))
					return false;
				else {
					expression = new AddressOfExpression(expression, new TextRange(startOffset, this.Token.EndOffset));
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.GetTypeKeyword)) {
				// GetTypeExpression
				TypeReference typeReference = null;
				if (!this.Match(VBTokenID.GetTypeKeyword))
					return false;
				if (!this.Match(VBTokenID.OpenParenthesis))
					return false;
				if (!this.MatchTypeName(out typeReference, true))
					return false;
				// TODO:
				// | (
				// <%
				// QualifiedIdentifier identifier;
				// AstNodeList typeArgumentList = null;
				// %>
				// "QualifiedIdentifier<@ out identifier @>"
				// 'OpenParenthesis'
				// 'Of'
				// [
				// TypeArityList
				// [ "TypeParameterList<@ out typeArgumentList @>" ]
				// 'Comma<+ typeArgumentList.Add(new TypeReference(null, TextRange.Deleted));  // Add a null type reference +>'
				// ]
				// 'CloseParenthesis'
				// <%
				// typeReference = new TypeReference(identifier.Text, new TextRange(identifier.StartOffset, this.Token.EndOffset));
				// if (typeArgumentList != null)
				// typeReference.GenericTypeArguments.AddRange(typeArgumentList.ToArray());
				// %>
				// )
				if (!this.Match(VBTokenID.CloseParenthesis))
					return false;
				expression = new TypeOfExpression(typeReference, new TextRange(startOffset, this.Token.EndOffset));
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.TypeOf)) {
				// TypeOfIsExpression
				TypeReference typeReference;
				if (!this.Match(VBTokenID.TypeOf))
					return false;
				if (!this.MatchPrimaryExpression(true, out expression))
					return false;
				if (!this.Match(VBTokenID.Is))
					return false;
				if (!this.MatchTypeName(out typeReference, false))
					return false;
				expression = new IsTypeOfExpression(expression, typeReference, new TextRange(startOffset, this.Token.EndOffset));
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.GetXmlNamespace)) {
				// GetXmlNamespaceExpression
				if (!this.Match(VBTokenID.GetXmlNamespace))
					return false;
				if (!this.Match(VBTokenID.OpenParenthesis))
					return false;
				string namespaceName = String.Empty;
				int nameStartOffset = this.LookAheadToken.StartOffset;
				while (this.IsIdentifier(this.LookAheadToken) || this.TokenIs(this.LookAheadToken, VBTokenID.Subtraction)) {
					namespaceName += this.LookAheadTokenText;
					this.AdvanceToNext();
				}
				int nameEndOffset = this.Token.EndOffset;
				if (!this.Match(VBTokenID.CloseParenthesis))
					return false;
				expression = new GetXmlNamespaceExpression(new SimpleName(namespaceName, new TextRange(nameStartOffset, nameEndOffset)), new TextRange(startOffset, this.Token.EndOffset));
			}
			else if (this.IsInMultiMatchSet(10, this.LookAheadToken)) {
				// MemberAccessBase
				TypeReference typeReference;
				if (!this.MatchBuiltInTypeName(out typeReference))
					return false;
				else {
					expression = new TypeReferenceExpression(typeReference);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Global)) {
				// MemberAccessBase
				if (!this.Match(VBTokenID.Global))
					return false;
				if (!this.Match(VBTokenID.Dot))
					return false;
				TypeReference typeReference;
				if (!this.MatchTypeName(out typeReference, false))
					return false;
				expression = new TypeReferenceExpression(typeReference);
				expression.StartOffset = startOffset;  // Update the start offset to include the Global.
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.MyBase)) {
				// MemberAccessBase
				if (!this.Match(VBTokenID.MyBase))
					return false;
				else {
					expression = new BaseAccess(this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.MyClass)) {
				// MemberAccessBase
				if (!this.Match(VBTokenID.MyClass))
					return false;
				else {
					expression = new ClassAccess(this.Token.TextRange);
				}
			}
			else if (((this.TokenIs(this.LookAheadToken, VBTokenID.OpenCurlyBrace)) || (this.TokenIs(this.LookAheadToken, VBTokenID.New)))) {
				if (!this.MatchNewExpression(out expression))
					return false;
			}
			else if (((this.TokenIs(this.LookAheadToken, VBTokenID.CType)) || (this.TokenIs(this.LookAheadToken, VBTokenID.DirectCast)))) {
				// CastExpression
				TypeReference typeReference;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.DirectCast)) {
					if (!this.Match(VBTokenID.DirectCast))
						return false;
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.CType)) {
					if (!this.Match(VBTokenID.CType))
						return false;
				}
				else
					return false;
				if (!this.Match(VBTokenID.OpenParenthesis))
					return false;
				if (!this.MatchExpression(out expression))
					return false;
				if (!this.Match(VBTokenID.Comma))
					return false;
				if (!this.MatchTypeName(out typeReference, false))
					return false;
				if (!this.Match(VBTokenID.CloseParenthesis))
					return false;
				expression = new CastExpression(typeReference, expression, new TextRange(startOffset, this.Token.EndOffset));
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.TryCast)) {
				// CastExpression
				TypeReference typeReference;
				if (!this.Match(VBTokenID.TryCast))
					return false;
				if (!this.Match(VBTokenID.OpenParenthesis))
					return false;
				if (!this.MatchExpression(out expression))
					return false;
				if (!this.Match(VBTokenID.Comma))
					return false;
				if (!this.MatchTypeName(out typeReference, false))
					return false;
				if (!this.Match(VBTokenID.CloseParenthesis))
					return false;
				expression = new TryCastExpression(expression, typeReference, new TextRange(startOffset, this.Token.EndOffset));
			}
			else if (this.IsInMultiMatchSet(28, this.LookAheadToken)) {
				// CastExpression
				TypeReference typeReference;
				if (!this.MatchCastTarget(out typeReference))
					return false;
				if (!this.Match(VBTokenID.OpenParenthesis))
					return false;
				if (!this.MatchExpression(out expression))
					return false;
				if (!this.Match(VBTokenID.CloseParenthesis))
					return false;
				expression = new CastExpression(typeReference, expression, new TextRange(startOffset, this.Token.EndOffset));
			}
			else if (this.IsConditionalExpression()) {
				Expression trueExpression;
				Expression falseExpression = null;
				if (!this.Match(VBTokenID.If))
					return false;
				if (!this.Match(VBTokenID.OpenParenthesis))
					return false;
				if (!this.MatchExpression(out expression))
					return false;
				if (!this.Match(VBTokenID.Comma))
					return false;
				if (!this.MatchExpression(out trueExpression))
					return false;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
					if (!this.Match(VBTokenID.Comma))
						return false;
					if (!this.MatchExpression(out falseExpression))
						return false;
				}
				if (!this.Match(VBTokenID.CloseParenthesis))
					return false;
				if (falseExpression != null)
					expression = new ConditionalExpression(expression, trueExpression, falseExpression, new TextRange(startOffset, this.Token.EndOffset));
				else
					expression = new BinaryExpression(OperatorType.NullCoalescing, expression, trueExpression, new TextRange(startOffset, this.Token.EndOffset));
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.IIf)) {
				// IIfExpression (NOTE: Not really part of grammar but is a recognized function)
				Expression trueExpression;
				Expression falseExpression;
				if (!this.Match(VBTokenID.IIf))
					return false;
				if (!this.Match(VBTokenID.OpenParenthesis))
					return false;
				if (!this.MatchExpression(out expression))
					return false;
				if (!this.Match(VBTokenID.Comma))
					return false;
				if (!this.MatchExpression(out trueExpression))
					return false;
				if (!this.Match(VBTokenID.Comma))
					return false;
				if (!this.MatchExpression(out falseExpression))
					return false;
				if (!this.Match(VBTokenID.CloseParenthesis))
					return false;
				expression = new ConditionalExpression(expression, trueExpression, falseExpression, new TextRange(startOffset, this.Token.EndOffset));
			}
			else if (((this.TokenIs(this.LookAheadToken, VBTokenID.Sub)) || (this.TokenIs(this.LookAheadToken, VBTokenID.Function)))) {
				if (!this.MatchLambdaExpression(out expression))
					return false;
			}
			else if (this.IsDotIdentifierOrKeyword(true)) {
				// If there is a .Identifier, then this is probably within a With statement block...
				//   this code will create a MemberAccessExpression below with a null target
				// NOTE: In the future, perhaps pass along what context we are in to ensure there is a valid With statement above
			}
			else
				return false;
			while (this.IsInMultiMatchSet(29, this.LookAheadToken)) {
				if (this.TokenIs(this.LookAheadToken, VBTokenID.Dot)) {
					// MemberAccessExpression
					QualifiedIdentifier memberName;
					if (!this.Match(VBTokenID.Dot))
						return false;
					if (((this.TokenIs(this.LookAheadToken, VBTokenID.XmlLiteral)) || (this.TokenIs(this.LookAheadToken, VBTokenID.XmlAttribute)))) {
						if (this.TokenIs(this.LookAheadToken, VBTokenID.XmlLiteral)) {
							this.Match(VBTokenID.XmlLiteral);
						}
						else if (this.TokenIs(this.LookAheadToken, VBTokenID.XmlAttribute)) {
							this.Match(VBTokenID.XmlAttribute);
						}
						else
							return false;
						memberName = new QualifiedIdentifier(this.TokenText, this.Token.TextRange);
						expression = new MemberAccess(expression, memberName, new TextRange(startOffset, this.Token.EndOffset));
					}
					else if ((this.IsInMultiMatchSet(1, this.LookAheadToken) || (this.IsKeyword()))) {
						if (!this.MatchIdentifierOrKeyword(out memberName))
							return false;
						expression = new MemberAccess(expression, memberName, new TextRange(startOffset, this.Token.EndOffset));
						AstNodeList typeArgumentList = null;
						if (this.AreNextTwo(VBTokenID.OpenParenthesis, VBTokenID.Of)) {
							if (!this.Match(VBTokenID.OpenParenthesis))
								return false;
							if (!this.Match(VBTokenID.Of))
								return false;
							if (this.IsInMultiMatchSet(30, this.LookAheadToken)) {
								if (!this.MatchTypeArgumentList(out typeArgumentList))
									return false;
							}
							if (!this.Match(VBTokenID.CloseParenthesis))
								return false;
						}
						if (typeArgumentList != null)
							expression.GenericTypeArguments.AddRange(typeArgumentList.ToArray());
						
						// This occurs when there is a call like this without parens:  MyBase.New
						if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
							expression = new InvocationExpression(expression);
							expression.TextRange = ((InvocationExpression)expression).Expression.TextRange;
						}
					}
					else
						return false;
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.DotAt)) {
					// MemberAccessExpression (XML attribute)
					QualifiedIdentifier memberName;
					if (!this.Match(VBTokenID.DotAt))
						return false;
					if (this.TokenIs(this.LookAheadToken, VBTokenID.XmlLiteral)) {
						this.Match(VBTokenID.XmlLiteral);
					}
					else if (this.TokenIs(this.LookAheadToken, VBTokenID.XmlAttribute)) {
						this.Match(VBTokenID.XmlAttribute);
					}
					else
						return false;
					memberName = new QualifiedIdentifier(this.TokenText, this.Token.TextRange);
					expression = new MemberAccess(expression, memberName, new TextRange(startOffset, this.Token.EndOffset));
					((MemberAccess)expression).MemberAccessType = MemberAccessType.XmlAttribute;
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.TripleDot)) {
					// MemberAccessExpression (XML descendent)
					QualifiedIdentifier memberName;
					if (!this.Match(VBTokenID.TripleDot))
						return false;
					if (!this.Match(VBTokenID.XmlLiteral))
						return false;
					memberName = new QualifiedIdentifier(this.TokenText, this.Token.TextRange);
					expression = new MemberAccess(expression, memberName, new TextRange(startOffset, this.Token.EndOffset));
					((MemberAccess)expression).MemberAccessType = MemberAccessType.XmlDescendent;
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.ExclamationPoint)) {
					// DictionaryAccessExpression
					if (!allowParenMatchAtEnd)
						return true;
					QualifiedIdentifier key;
					if (!this.Match(VBTokenID.ExclamationPoint))
						return false;
					if (!this.MatchIdentifierOrKeyword(out key))
						return false;
					expression = new MemberAccess(expression, key, new TextRange(startOffset, this.Token.EndOffset));
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.OpenParenthesis)) {
					if (!allowParenMatchAtEnd)
						return true;
					if (!this.MatchInvocationExpression(ref expression))
						return false;
				}
				else
					return false;
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>InvocationExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>InvocationExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>OpenParenthesis</c>.
		/// </remarks>
		protected virtual bool MatchInvocationExpression(ref Expression expression) {
			// NOTE: The real invocation expression parses an Expression first but it is up to the caller to supply that
			int startOffset = expression.StartOffset;
			expression = new InvocationExpression(expression);
			expression.StartOffset = startOffset;
			AstNodeList argumentList = null;
			if (!this.Match(VBTokenID.OpenParenthesis))
				return false;
			if ((this.IsInMultiMatchSet(23, this.LookAheadToken) || (this.IsKeyword()) || (this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression()) || ((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis }))))) {
				if (!this.MatchArgumentList(out argumentList))
					return false;
			}
			if (!this.Match(VBTokenID.CloseParenthesis))
				return false;
			if (argumentList != null)
				((InvocationExpression)expression).Arguments.AddRange(argumentList.ToArray());
			expression.EndOffset = this.Token.EndOffset;
			return true;
		}

		/// <summary>
		/// Matches a <c>ArgumentList</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ArgumentList</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Global</c>, <c>OpenParenthesis</c>, <c>OpenCurlyBrace</c>, <c>New</c>, <c>XmlLiteral</c>, <c>Object</c>, <c>Single</c>, <c>Double</c>, <c>Decimal</c>, <c>Boolean</c>, <c>Date</c>, <c>Char</c>, <c>String</c>, <c>Byte</c>, <c>SByte</c>, <c>UShort</c>, <c>Short</c>, <c>UInteger</c>, <c>Integer</c>, <c>ULong</c>, <c>Long</c>, <c>Sub</c>, <c>Function</c>, <c>Addition</c>, <c>Subtraction</c>, <c>Not</c>, <c>CType</c>, <c>StringLiteral</c>, <c>MyBase</c>, <c>Me</c>, <c>DecimalIntegerLiteral</c>, <c>Mid</c>, <c>True</c>, <c>False</c>, <c>HexadecimalIntegerLiteral</c>, <c>OctalIntegerLiteral</c>, <c>FloatingPointLiteral</c>, <c>CharacterLiteral</c>, <c>DateLiteral</c>, <c>Nothing</c>, <c>AddressOf</c>, <c>GetTypeKeyword</c>, <c>TypeOf</c>, <c>GetXmlNamespace</c>, <c>MyClass</c>, <c>DirectCast</c>, <c>TryCast</c>, <c>IIf</c>, <c>CBool</c>, <c>CByte</c>, <c>CChar</c>, <c>CDate</c>, <c>CDec</c>, <c>CDbl</c>, <c>CInt</c>, <c>CLng</c>, <c>CObj</c>, <c>CSByte</c>, <c>CShort</c>, <c>CSng</c>, <c>CStr</c>, <c>CUInt</c>, <c>CULng</c>, <c>CUShort</c>.
		/// The non-terminal can start with: this.IsKeyword().
		/// The non-terminal can start with: this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression().
		/// The non-terminal can start with: (this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) &amp;&amp; (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis })).
		/// </remarks>
		protected virtual bool MatchArgumentList(out AstNodeList argumentList) {
			argumentList = new AstNodeList(null);
			int startOffset = this.LookAheadToken.StartOffset;
			QualifiedIdentifier name = null;
			Expression expression;
			if (((this.TokenIs(this.LookAheadToken, VBTokenID.Identifier)) || (this.IsKeyword())) && (this.TokenIs(this.GetLookAheadToken(2), VBTokenID.ColonEquals))) {
				if (!this.MatchIdentifierOrKeyword(out name))
					return false;
				if (!this.Match(VBTokenID.ColonEquals))
					return false;
			}
			if (!this.MatchExpression(out expression))
				return false;
			else {
				argumentList.Add(new ArgumentExpression(ParameterModifiers.None, name, expression, new TextRange(startOffset, this.Token.EndOffset)));
			}
			while (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
				startOffset = this.LookAheadToken.StartOffset;
				name = null;
				if (!this.Match(VBTokenID.Comma))
					return false;
				if (((this.TokenIs(this.LookAheadToken, VBTokenID.Identifier)) || (this.IsKeyword())) && (this.TokenIs(this.GetLookAheadToken(2), VBTokenID.ColonEquals))) {
					if (!this.MatchIdentifierOrKeyword(out name))
						return false;
					if (!this.Match(VBTokenID.ColonEquals))
						return false;
				}
				if (!this.MatchExpression(out expression))
					return false;
				else {
					argumentList.Add(new ArgumentExpression(ParameterModifiers.None, name, expression, new TextRange(startOffset, this.Token.EndOffset)));
				}
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>NewExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>NewExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>OpenCurlyBrace</c>, <c>New</c>.
		/// </remarks>
		protected virtual bool MatchNewExpression(out Expression expression) {
			expression = null;
			// AnonymousArrayCreationExpression
			if (this.TokenIs(this.LookAheadToken, VBTokenID.OpenCurlyBrace)) {
				if (!this.MatchCollectionInitializer(out expression))
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.New)) {
				int startOffset = this.LookAheadToken.StartOffset;
				TypeReference typeReference = null;
				AstNodeList argumentList = null;
				Expression fieldInitializer = null;
				bool isArrayCreation = false;
				if (!this.Match(VBTokenID.New))
					return false;
				// AnonymousObjectCreationExpression
				if (this.TokenIs(this.LookAheadToken, VBTokenID.With)) {
					if (!this.MatchObjectMemberInitializer(out fieldInitializer))
						return false;
					else {
						typeReference = new TypeReference(TypeReference.AnonymousTypeName, fieldInitializer.TextRange);
					}
				}
				else if (this.IsInMultiMatchSet(3, this.LookAheadToken)) {
					if (!this.MatchNonArrayTypeName(out typeReference, false))
						return false;
					isArrayCreation = this.IsArrayCreationExpression();
					if ((!isArrayCreation) && (this.TokenIs(this.LookAheadToken, VBTokenID.OpenParenthesis))) {
						if (!this.Match(VBTokenID.OpenParenthesis))
							return false;
						if ((this.IsInMultiMatchSet(23, this.LookAheadToken) || (this.IsKeyword()) || (this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression()) || ((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis }))))) {
							if (!this.MatchArgumentList(out argumentList))
								return false;
						}
						if (!this.Match(VBTokenID.CloseParenthesis))
							return false;
					}
					// If this is an ArrayCreationExpression...
					if ((isArrayCreation) && (typeReference != null)) {
						int[] arrayRanks = null;
						Expression initializer = null;
						if (this.TokenIs(this.LookAheadToken, VBTokenID.OpenParenthesis)) {
							if (!this.MatchArraySizeInitializationModifier(out arrayRanks))
								return false;
							if (!this.MatchCollectionInitializer(out initializer))
								return false;
							expression = new ObjectCreationExpression(typeReference, new TextRange(startOffset, this.Token.EndOffset));
							((ObjectCreationExpression)expression).IsArray = true;
							typeReference.ArrayRanks = arrayRanks;
							((ObjectCreationExpression)expression).Initializer = (ObjectCollectionInitializerExpression)initializer;
							return true;
						}
					}
					
					// ObjectCreationExpression
					if (((this.TokenIs(this.LookAheadToken, VBTokenID.From)) || (this.TokenIs(this.LookAheadToken, VBTokenID.With)))) {
						if (this.TokenIs(this.LookAheadToken, VBTokenID.With)) {
							if (!this.MatchObjectMemberInitializer(out fieldInitializer))
								return false;
						}
						else if (this.TokenIs(this.LookAheadToken, VBTokenID.From)) {
							if (!this.MatchObjectCollectionInitializer(out fieldInitializer))
								return false;
						}
						else
							return false;
					}
				}
				else
					return false;
				expression = new ObjectCreationExpression(typeReference, new TextRange(startOffset, this.Token.EndOffset));
				if (argumentList != null)
					((ObjectCreationExpression)expression).Arguments.AddRange(argumentList.ToArray());
				((ObjectCreationExpression)expression).Initializer = fieldInitializer as ObjectCollectionInitializerExpression;
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>ObjectCreationExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ObjectCreationExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>New</c>.
		/// </remarks>
		protected virtual bool MatchObjectCreationExpression(out Expression expression) {
			expression = null;
			int startOffset = this.LookAheadToken.StartOffset;
			TypeReference typeReference = null;
			AstNodeList argumentList = null;
			Expression fieldInitializer = null;
			if (!this.Match(VBTokenID.New))
				return false;
			if (!this.MatchNonArrayTypeName(out typeReference, false))
				return false;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.OpenParenthesis)) {
				if (!this.Match(VBTokenID.OpenParenthesis))
					return false;
				if ((this.IsInMultiMatchSet(23, this.LookAheadToken) || (this.IsKeyword()) || (this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression()) || ((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis }))))) {
					if (!this.MatchArgumentList(out argumentList))
						return false;
				}
				if (!this.Match(VBTokenID.CloseParenthesis))
					return false;
			}
			if (((this.TokenIs(this.LookAheadToken, VBTokenID.From)) || (this.TokenIs(this.LookAheadToken, VBTokenID.With)))) {
				if (this.TokenIs(this.LookAheadToken, VBTokenID.With)) {
					if (!this.MatchObjectMemberInitializer(out fieldInitializer))
						return false;
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.From)) {
					if (!this.MatchObjectCollectionInitializer(out fieldInitializer))
						return false;
				}
				else
					return false;
			}
			expression = new ObjectCreationExpression(typeReference, new TextRange(startOffset, this.Token.EndOffset));
			if (argumentList != null)
				((ObjectCreationExpression)expression).Arguments.AddRange(argumentList.ToArray());
			((ObjectCreationExpression)expression).Initializer = fieldInitializer as ObjectCollectionInitializerExpression;
			return true;
		}

		/// <summary>
		/// Matches a <c>ObjectMemberInitializer</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ObjectMemberInitializer</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>With</c>.
		/// </remarks>
		protected virtual bool MatchObjectMemberInitializer(out Expression expression) {
			expression = null;
			Expression initializer;
			AstNodeList initializerList = new AstNodeList(null);
			int startOffset = this.LookAheadToken.StartOffset;
			if (!this.Match(VBTokenID.With))
				return false;
			if (!this.Match(VBTokenID.OpenCurlyBrace))
				return false;
			if (!this.MatchFieldInitializer(out initializer))
				return false;
			else {
				initializerList.Add(initializer);
			}
			while (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
				if (!this.Match(VBTokenID.Comma))
					return false;
				if (!this.MatchFieldInitializer(out initializer))
					return false;
				else {
					initializerList.Add(initializer);
				}
			}
			if (!this.Match(VBTokenID.CloseCurlyBrace))
				return false;
			expression = new ObjectCollectionInitializerExpression(new TextRange(startOffset, this.Token.EndOffset));
			((ObjectCollectionInitializerExpression)expression).Initializers.AddRange(initializerList.ToArray());
			return true;
		}

		/// <summary>
		/// Matches a <c>FieldInitializer</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>FieldInitializer</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Dot</c>.
		/// The non-terminal can start with: this.IsKeyword().
		/// </remarks>
		protected virtual bool MatchFieldInitializer(out Expression expression) {
			expression = null;
			QualifiedIdentifier identifier;
			SimpleName memberName;
			Expression valueExpression = null;
			int startOffset = this.LookAheadToken.StartOffset;
			
			if (this.AreNextTwoIdentifierAnd(VBTokenID.Dot)) {
				// Skip over Key
				this.AdvanceToNext();
			}
			if (this.TokenIs(this.LookAheadToken, VBTokenID.Dot)) {
				this.Match(VBTokenID.Dot);
			}
			if (!this.MatchIdentifierOrKeyword(out identifier))
				return false;
			else {
				memberName = new SimpleName(identifier.Text, identifier.TextRange);
			}
			if (this.TokenIs(this.LookAheadToken, VBTokenID.Equality)) {
				if (!this.Match(VBTokenID.Equality))
					return false;
				if (!this.MatchExpression(out valueExpression))
					return false;
			}
			if (valueExpression != null)
				expression = new AssignmentExpression(OperatorType.None, memberName, valueExpression, new TextRange(startOffset, this.Token.EndOffset));
			else
				expression = memberName;
			return true;
		}

		/// <summary>
		/// Matches a <c>ObjectCollectionInitializer</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ObjectCollectionInitializer</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>From</c>.
		/// </remarks>
		protected virtual bool MatchObjectCollectionInitializer(out Expression expression) {
			expression = null;
			if (!this.Match(VBTokenID.From))
				return false;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
				this.Match(VBTokenID.LineTerminator);
			}
			if (!this.MatchCollectionInitializer(out expression))
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>CollectionInitializer</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>CollectionInitializer</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>OpenCurlyBrace</c>.
		/// </remarks>
		protected virtual bool MatchCollectionInitializer(out Expression expression) {
			expression = new ObjectCollectionInitializerExpression();
			expression.StartOffset = this.LookAheadToken.StartOffset;
			Expression initializer;
			AstNodeList initializerList = new AstNodeList(null);
			if (!this.Match(VBTokenID.OpenCurlyBrace))
				return false;
			if ((this.IsInMultiMatchSet(23, this.LookAheadToken) || (this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression()) || ((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis }))))) {
				if (!this.MatchExpression(out initializer))
					return false;
				else {
					((ObjectCollectionInitializerExpression)expression).Initializers.Add(initializer);
				}
				while (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
					if (!this.Match(VBTokenID.Comma))
						return false;
					if (!this.MatchExpression(out initializer))
						return false;
					else {
						((ObjectCollectionInitializerExpression)expression).Initializers.Add(initializer);
					}
				}
			}
			this.Match(VBTokenID.CloseCurlyBrace);
			expression.EndOffset = this.Token.EndOffset;
			return true;
		}

		/// <summary>
		/// Matches a <c>CastTarget</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>CastTarget</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>CBool</c>, <c>CByte</c>, <c>CChar</c>, <c>CDate</c>, <c>CDec</c>, <c>CDbl</c>, <c>CInt</c>, <c>CLng</c>, <c>CObj</c>, <c>CSByte</c>, <c>CShort</c>, <c>CSng</c>, <c>CStr</c>, <c>CUInt</c>, <c>CULng</c>, <c>CUShort</c>.
		/// </remarks>
		protected virtual bool MatchCastTarget(out TypeReference typeReference) {
			typeReference = null;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.CBool)) {
				if (!this.Match(VBTokenID.CBool))
					return false;
				else {
					typeReference = new TypeReference("System.Boolean", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.CByte)) {
				if (!this.Match(VBTokenID.CByte))
					return false;
				else {
					typeReference = new TypeReference("System.Byte", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.CChar)) {
				if (!this.Match(VBTokenID.CChar))
					return false;
				else {
					typeReference = new TypeReference("System.Char", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.CDate)) {
				if (!this.Match(VBTokenID.CDate))
					return false;
				else {
					typeReference = new TypeReference("System.DateTime", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.CDec)) {
				if (!this.Match(VBTokenID.CDec))
					return false;
				else {
					typeReference = new TypeReference("System.Decimal", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.CDbl)) {
				if (!this.Match(VBTokenID.CDbl))
					return false;
				else {
					typeReference = new TypeReference("System.Double", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.CInt)) {
				if (!this.Match(VBTokenID.CInt))
					return false;
				else {
					typeReference = new TypeReference("System.Int32", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.CLng)) {
				if (!this.Match(VBTokenID.CLng))
					return false;
				else {
					typeReference = new TypeReference("System.Int64", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.CObj)) {
				if (!this.Match(VBTokenID.CObj))
					return false;
				else {
					typeReference = new TypeReference("System.Object", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.CSByte)) {
				if (!this.Match(VBTokenID.CSByte))
					return false;
				else {
					typeReference = new TypeReference("System.SByte", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.CShort)) {
				if (!this.Match(VBTokenID.CShort))
					return false;
				else {
					typeReference = new TypeReference("System.Int16", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.CSng)) {
				if (!this.Match(VBTokenID.CSng))
					return false;
				else {
					typeReference = new TypeReference("System.Single", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.CStr)) {
				if (!this.Match(VBTokenID.CStr))
					return false;
				else {
					typeReference = new TypeReference("System.String", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.CUInt)) {
				if (!this.Match(VBTokenID.CUInt))
					return false;
				else {
					typeReference = new TypeReference("System.UInt32", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.CULng)) {
				if (!this.Match(VBTokenID.CULng))
					return false;
				else {
					typeReference = new TypeReference("System.UInt64", this.Token.TextRange);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.CUShort)) {
				if (!this.Match(VBTokenID.CUShort))
					return false;
				else {
					typeReference = new TypeReference("System.UInt16", this.Token.TextRange);
				}
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>UnaryExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>UnaryExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Global</c>, <c>OpenParenthesis</c>, <c>OpenCurlyBrace</c>, <c>New</c>, <c>XmlLiteral</c>, <c>Object</c>, <c>Single</c>, <c>Double</c>, <c>Decimal</c>, <c>Boolean</c>, <c>Date</c>, <c>Char</c>, <c>String</c>, <c>Byte</c>, <c>SByte</c>, <c>UShort</c>, <c>Short</c>, <c>UInteger</c>, <c>Integer</c>, <c>ULong</c>, <c>Long</c>, <c>Sub</c>, <c>Function</c>, <c>Addition</c>, <c>Subtraction</c>, <c>CType</c>, <c>StringLiteral</c>, <c>MyBase</c>, <c>Me</c>, <c>DecimalIntegerLiteral</c>, <c>Mid</c>, <c>True</c>, <c>False</c>, <c>HexadecimalIntegerLiteral</c>, <c>OctalIntegerLiteral</c>, <c>FloatingPointLiteral</c>, <c>CharacterLiteral</c>, <c>DateLiteral</c>, <c>Nothing</c>, <c>AddressOf</c>, <c>GetTypeKeyword</c>, <c>TypeOf</c>, <c>GetXmlNamespace</c>, <c>MyClass</c>, <c>DirectCast</c>, <c>TryCast</c>, <c>IIf</c>, <c>CBool</c>, <c>CByte</c>, <c>CChar</c>, <c>CDate</c>, <c>CDec</c>, <c>CDbl</c>, <c>CInt</c>, <c>CLng</c>, <c>CObj</c>, <c>CSByte</c>, <c>CShort</c>, <c>CSng</c>, <c>CStr</c>, <c>CUInt</c>, <c>CULng</c>, <c>CUShort</c>.
		/// The non-terminal can start with: this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression().
		/// The non-terminal can start with: (this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) &amp;&amp; (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis })).
		/// </remarks>
		protected virtual bool MatchUnaryExpression(out Expression expression) {
			OperatorType operatorType = OperatorType.None;
			expression = null;
			if (((this.TokenIs(this.LookAheadToken, VBTokenID.Addition)) || (this.TokenIs(this.LookAheadToken, VBTokenID.Subtraction)))) {
				if (this.TokenIs(this.LookAheadToken, VBTokenID.Addition)) {
					if (!this.Match(VBTokenID.Addition))
						return false;
					else {
						operatorType = OperatorType.Addition;
					}
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.Subtraction)) {
					if (!this.Match(VBTokenID.Subtraction))
						return false;
					else {
						operatorType = OperatorType.Subtraction;
					}
				}
				else
					return false;
			}
			if (!this.MatchExponentOperatorExpression(out expression))
				return false;
			if (operatorType != OperatorType.None)
				expression = new UnaryExpression(operatorType, expression);
			return true;
		}

		/// <summary>
		/// Matches a <c>AdditiveExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>AdditiveExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Global</c>, <c>OpenParenthesis</c>, <c>OpenCurlyBrace</c>, <c>New</c>, <c>XmlLiteral</c>, <c>Object</c>, <c>Single</c>, <c>Double</c>, <c>Decimal</c>, <c>Boolean</c>, <c>Date</c>, <c>Char</c>, <c>String</c>, <c>Byte</c>, <c>SByte</c>, <c>UShort</c>, <c>Short</c>, <c>UInteger</c>, <c>Integer</c>, <c>ULong</c>, <c>Long</c>, <c>Sub</c>, <c>Function</c>, <c>Addition</c>, <c>Subtraction</c>, <c>CType</c>, <c>StringLiteral</c>, <c>MyBase</c>, <c>Me</c>, <c>DecimalIntegerLiteral</c>, <c>Mid</c>, <c>True</c>, <c>False</c>, <c>HexadecimalIntegerLiteral</c>, <c>OctalIntegerLiteral</c>, <c>FloatingPointLiteral</c>, <c>CharacterLiteral</c>, <c>DateLiteral</c>, <c>Nothing</c>, <c>AddressOf</c>, <c>GetTypeKeyword</c>, <c>TypeOf</c>, <c>GetXmlNamespace</c>, <c>MyClass</c>, <c>DirectCast</c>, <c>TryCast</c>, <c>IIf</c>, <c>CBool</c>, <c>CByte</c>, <c>CChar</c>, <c>CDate</c>, <c>CDec</c>, <c>CDbl</c>, <c>CInt</c>, <c>CLng</c>, <c>CObj</c>, <c>CSByte</c>, <c>CShort</c>, <c>CSng</c>, <c>CStr</c>, <c>CUInt</c>, <c>CULng</c>, <c>CUShort</c>.
		/// The non-terminal can start with: this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression().
		/// The non-terminal can start with: (this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) &amp;&amp; (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis })).
		/// </remarks>
		protected virtual bool MatchAdditiveExpression(out Expression expression) {
			int startOffset = this.LookAheadToken.StartOffset;
			Expression rightExpression;
			if (!this.MatchModuloOperatorExpression(out expression))
				return false;
			while (((this.TokenIs(this.LookAheadToken, VBTokenID.Addition)) || (this.TokenIs(this.LookAheadToken, VBTokenID.Subtraction)))) {
				OperatorType operatorType = OperatorType.None;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.Addition)) {
					if (!this.Match(VBTokenID.Addition))
						return false;
					else {
						operatorType = OperatorType.Addition;
					}
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.Subtraction)) {
					if (!this.Match(VBTokenID.Subtraction))
						return false;
					else {
						operatorType = OperatorType.Subtraction;
					}
				}
				else
					return false;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
					this.Match(VBTokenID.LineTerminator);
				}
				if (!this.MatchAdditiveExpression(out rightExpression))
					return false;
				expression = new BinaryExpression(operatorType, expression, rightExpression, new TextRange(startOffset, this.Token.EndOffset));
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>MultiplicativeExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>MultiplicativeExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Global</c>, <c>OpenParenthesis</c>, <c>OpenCurlyBrace</c>, <c>New</c>, <c>XmlLiteral</c>, <c>Object</c>, <c>Single</c>, <c>Double</c>, <c>Decimal</c>, <c>Boolean</c>, <c>Date</c>, <c>Char</c>, <c>String</c>, <c>Byte</c>, <c>SByte</c>, <c>UShort</c>, <c>Short</c>, <c>UInteger</c>, <c>Integer</c>, <c>ULong</c>, <c>Long</c>, <c>Sub</c>, <c>Function</c>, <c>Addition</c>, <c>Subtraction</c>, <c>CType</c>, <c>StringLiteral</c>, <c>MyBase</c>, <c>Me</c>, <c>DecimalIntegerLiteral</c>, <c>Mid</c>, <c>True</c>, <c>False</c>, <c>HexadecimalIntegerLiteral</c>, <c>OctalIntegerLiteral</c>, <c>FloatingPointLiteral</c>, <c>CharacterLiteral</c>, <c>DateLiteral</c>, <c>Nothing</c>, <c>AddressOf</c>, <c>GetTypeKeyword</c>, <c>TypeOf</c>, <c>GetXmlNamespace</c>, <c>MyClass</c>, <c>DirectCast</c>, <c>TryCast</c>, <c>IIf</c>, <c>CBool</c>, <c>CByte</c>, <c>CChar</c>, <c>CDate</c>, <c>CDec</c>, <c>CDbl</c>, <c>CInt</c>, <c>CLng</c>, <c>CObj</c>, <c>CSByte</c>, <c>CShort</c>, <c>CSng</c>, <c>CStr</c>, <c>CUInt</c>, <c>CULng</c>, <c>CUShort</c>.
		/// The non-terminal can start with: this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression().
		/// The non-terminal can start with: (this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) &amp;&amp; (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis })).
		/// </remarks>
		protected virtual bool MatchMultiplicativeExpression(out Expression expression) {
			int startOffset = this.LookAheadToken.StartOffset;
			Expression rightExpression;
			if (!this.MatchUnaryExpression(out expression))
				return false;
			while (((this.TokenIs(this.LookAheadToken, VBTokenID.Multiplication)) || (this.TokenIs(this.LookAheadToken, VBTokenID.FloatingPointDivision)))) {
				OperatorType operatorType = OperatorType.None;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.Multiplication)) {
					if (!this.Match(VBTokenID.Multiplication))
						return false;
					else {
						operatorType = OperatorType.Multiply;
					}
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.FloatingPointDivision)) {
					if (!this.Match(VBTokenID.FloatingPointDivision))
						return false;
					else {
						operatorType = OperatorType.Division;
					}
				}
				else
					return false;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
					this.Match(VBTokenID.LineTerminator);
				}
				if (!this.MatchMultiplicativeExpression(out rightExpression))
					return false;
				expression = new BinaryExpression(operatorType, expression, rightExpression, new TextRange(startOffset, this.Token.EndOffset));
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>IntegerDivisionOperatorExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>IntegerDivisionOperatorExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Global</c>, <c>OpenParenthesis</c>, <c>OpenCurlyBrace</c>, <c>New</c>, <c>XmlLiteral</c>, <c>Object</c>, <c>Single</c>, <c>Double</c>, <c>Decimal</c>, <c>Boolean</c>, <c>Date</c>, <c>Char</c>, <c>String</c>, <c>Byte</c>, <c>SByte</c>, <c>UShort</c>, <c>Short</c>, <c>UInteger</c>, <c>Integer</c>, <c>ULong</c>, <c>Long</c>, <c>Sub</c>, <c>Function</c>, <c>Addition</c>, <c>Subtraction</c>, <c>CType</c>, <c>StringLiteral</c>, <c>MyBase</c>, <c>Me</c>, <c>DecimalIntegerLiteral</c>, <c>Mid</c>, <c>True</c>, <c>False</c>, <c>HexadecimalIntegerLiteral</c>, <c>OctalIntegerLiteral</c>, <c>FloatingPointLiteral</c>, <c>CharacterLiteral</c>, <c>DateLiteral</c>, <c>Nothing</c>, <c>AddressOf</c>, <c>GetTypeKeyword</c>, <c>TypeOf</c>, <c>GetXmlNamespace</c>, <c>MyClass</c>, <c>DirectCast</c>, <c>TryCast</c>, <c>IIf</c>, <c>CBool</c>, <c>CByte</c>, <c>CChar</c>, <c>CDate</c>, <c>CDec</c>, <c>CDbl</c>, <c>CInt</c>, <c>CLng</c>, <c>CObj</c>, <c>CSByte</c>, <c>CShort</c>, <c>CSng</c>, <c>CStr</c>, <c>CUInt</c>, <c>CULng</c>, <c>CUShort</c>.
		/// The non-terminal can start with: this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression().
		/// The non-terminal can start with: (this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) &amp;&amp; (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis })).
		/// </remarks>
		protected virtual bool MatchIntegerDivisionOperatorExpression(out Expression expression) {
			int startOffset = this.LookAheadToken.StartOffset;
			Expression rightExpression;
			if (!this.MatchMultiplicativeExpression(out expression))
				return false;
			while (this.TokenIs(this.LookAheadToken, VBTokenID.IntegerDivision)) {
				if (!this.Match(VBTokenID.IntegerDivision))
					return false;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
					this.Match(VBTokenID.LineTerminator);
				}
				if (!this.MatchIntegerDivisionOperatorExpression(out rightExpression))
					return false;
				expression = new BinaryExpression(OperatorType.IntegerDivision, expression, rightExpression, new TextRange(startOffset, this.Token.EndOffset));
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>ModuloOperatorExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ModuloOperatorExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Global</c>, <c>OpenParenthesis</c>, <c>OpenCurlyBrace</c>, <c>New</c>, <c>XmlLiteral</c>, <c>Object</c>, <c>Single</c>, <c>Double</c>, <c>Decimal</c>, <c>Boolean</c>, <c>Date</c>, <c>Char</c>, <c>String</c>, <c>Byte</c>, <c>SByte</c>, <c>UShort</c>, <c>Short</c>, <c>UInteger</c>, <c>Integer</c>, <c>ULong</c>, <c>Long</c>, <c>Sub</c>, <c>Function</c>, <c>Addition</c>, <c>Subtraction</c>, <c>CType</c>, <c>StringLiteral</c>, <c>MyBase</c>, <c>Me</c>, <c>DecimalIntegerLiteral</c>, <c>Mid</c>, <c>True</c>, <c>False</c>, <c>HexadecimalIntegerLiteral</c>, <c>OctalIntegerLiteral</c>, <c>FloatingPointLiteral</c>, <c>CharacterLiteral</c>, <c>DateLiteral</c>, <c>Nothing</c>, <c>AddressOf</c>, <c>GetTypeKeyword</c>, <c>TypeOf</c>, <c>GetXmlNamespace</c>, <c>MyClass</c>, <c>DirectCast</c>, <c>TryCast</c>, <c>IIf</c>, <c>CBool</c>, <c>CByte</c>, <c>CChar</c>, <c>CDate</c>, <c>CDec</c>, <c>CDbl</c>, <c>CInt</c>, <c>CLng</c>, <c>CObj</c>, <c>CSByte</c>, <c>CShort</c>, <c>CSng</c>, <c>CStr</c>, <c>CUInt</c>, <c>CULng</c>, <c>CUShort</c>.
		/// The non-terminal can start with: this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression().
		/// The non-terminal can start with: (this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) &amp;&amp; (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis })).
		/// </remarks>
		protected virtual bool MatchModuloOperatorExpression(out Expression expression) {
			int startOffset = this.LookAheadToken.StartOffset;
			Expression rightExpression;
			if (!this.MatchIntegerDivisionOperatorExpression(out expression))
				return false;
			while (this.TokenIs(this.LookAheadToken, VBTokenID.Mod)) {
				if (!this.Match(VBTokenID.Mod))
					return false;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
					this.Match(VBTokenID.LineTerminator);
				}
				if (!this.MatchModuloOperatorExpression(out rightExpression))
					return false;
				expression = new BinaryExpression(OperatorType.Modulus, expression, rightExpression, new TextRange(startOffset, this.Token.EndOffset));
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>XmlDescendantExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>XmlDescendantExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Global</c>, <c>OpenParenthesis</c>, <c>OpenCurlyBrace</c>, <c>New</c>, <c>XmlLiteral</c>, <c>Object</c>, <c>Single</c>, <c>Double</c>, <c>Decimal</c>, <c>Boolean</c>, <c>Date</c>, <c>Char</c>, <c>String</c>, <c>Byte</c>, <c>SByte</c>, <c>UShort</c>, <c>Short</c>, <c>UInteger</c>, <c>Integer</c>, <c>ULong</c>, <c>Long</c>, <c>Sub</c>, <c>Function</c>, <c>CType</c>, <c>StringLiteral</c>, <c>MyBase</c>, <c>Me</c>, <c>DecimalIntegerLiteral</c>, <c>Mid</c>, <c>True</c>, <c>False</c>, <c>HexadecimalIntegerLiteral</c>, <c>OctalIntegerLiteral</c>, <c>FloatingPointLiteral</c>, <c>CharacterLiteral</c>, <c>DateLiteral</c>, <c>Nothing</c>, <c>AddressOf</c>, <c>GetTypeKeyword</c>, <c>TypeOf</c>, <c>GetXmlNamespace</c>, <c>MyClass</c>, <c>DirectCast</c>, <c>TryCast</c>, <c>IIf</c>, <c>CBool</c>, <c>CByte</c>, <c>CChar</c>, <c>CDate</c>, <c>CDec</c>, <c>CDbl</c>, <c>CInt</c>, <c>CLng</c>, <c>CObj</c>, <c>CSByte</c>, <c>CShort</c>, <c>CSng</c>, <c>CStr</c>, <c>CUInt</c>, <c>CULng</c>, <c>CUShort</c>.
		/// The non-terminal can start with: this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression().
		/// The non-terminal can start with: (this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) &amp;&amp; (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis })).
		/// </remarks>
		protected virtual bool MatchXmlDescendantExpression(out Expression expression) {
			expression = null;
			if (!this.MatchPrimaryExpression(true, out expression))
				return false;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.TripleDot)) {
				Expression targetExpression;
				if (!this.Match(VBTokenID.TripleDot))
					return false;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
					this.Match(VBTokenID.LineTerminator);
				}
				if (!this.MatchXmlDescendantExpression(out targetExpression))
					return false;
				else {
					expression = new BinaryExpression(OperatorType.XmlDescendant, expression, targetExpression, new TextRange(expression.StartOffset, this.Token.EndOffset));
				}
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>ExponentOperatorExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ExponentOperatorExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Global</c>, <c>OpenParenthesis</c>, <c>OpenCurlyBrace</c>, <c>New</c>, <c>XmlLiteral</c>, <c>Object</c>, <c>Single</c>, <c>Double</c>, <c>Decimal</c>, <c>Boolean</c>, <c>Date</c>, <c>Char</c>, <c>String</c>, <c>Byte</c>, <c>SByte</c>, <c>UShort</c>, <c>Short</c>, <c>UInteger</c>, <c>Integer</c>, <c>ULong</c>, <c>Long</c>, <c>Sub</c>, <c>Function</c>, <c>CType</c>, <c>StringLiteral</c>, <c>MyBase</c>, <c>Me</c>, <c>DecimalIntegerLiteral</c>, <c>Mid</c>, <c>True</c>, <c>False</c>, <c>HexadecimalIntegerLiteral</c>, <c>OctalIntegerLiteral</c>, <c>FloatingPointLiteral</c>, <c>CharacterLiteral</c>, <c>DateLiteral</c>, <c>Nothing</c>, <c>AddressOf</c>, <c>GetTypeKeyword</c>, <c>TypeOf</c>, <c>GetXmlNamespace</c>, <c>MyClass</c>, <c>DirectCast</c>, <c>TryCast</c>, <c>IIf</c>, <c>CBool</c>, <c>CByte</c>, <c>CChar</c>, <c>CDate</c>, <c>CDec</c>, <c>CDbl</c>, <c>CInt</c>, <c>CLng</c>, <c>CObj</c>, <c>CSByte</c>, <c>CShort</c>, <c>CSng</c>, <c>CStr</c>, <c>CUInt</c>, <c>CULng</c>, <c>CUShort</c>.
		/// The non-terminal can start with: this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression().
		/// The non-terminal can start with: (this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) &amp;&amp; (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis })).
		/// </remarks>
		protected virtual bool MatchExponentOperatorExpression(out Expression expression) {
			int startOffset = this.LookAheadToken.StartOffset;
			Expression rightExpression;
			if (!this.MatchXmlDescendantExpression(out expression))
				return false;
			while (this.TokenIs(this.LookAheadToken, VBTokenID.Exponentiation)) {
				if (!this.Match(VBTokenID.Exponentiation))
					return false;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
					this.Match(VBTokenID.LineTerminator);
				}
				if (!this.MatchExponentOperatorExpression(out rightExpression))
					return false;
				expression = new BinaryExpression(OperatorType.Exponentiation, expression, rightExpression, new TextRange(startOffset, this.Token.EndOffset));
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>ConcatenationOperatorExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ConcatenationOperatorExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Global</c>, <c>OpenParenthesis</c>, <c>OpenCurlyBrace</c>, <c>New</c>, <c>XmlLiteral</c>, <c>Object</c>, <c>Single</c>, <c>Double</c>, <c>Decimal</c>, <c>Boolean</c>, <c>Date</c>, <c>Char</c>, <c>String</c>, <c>Byte</c>, <c>SByte</c>, <c>UShort</c>, <c>Short</c>, <c>UInteger</c>, <c>Integer</c>, <c>ULong</c>, <c>Long</c>, <c>Sub</c>, <c>Function</c>, <c>Addition</c>, <c>Subtraction</c>, <c>CType</c>, <c>StringLiteral</c>, <c>MyBase</c>, <c>Me</c>, <c>DecimalIntegerLiteral</c>, <c>Mid</c>, <c>True</c>, <c>False</c>, <c>HexadecimalIntegerLiteral</c>, <c>OctalIntegerLiteral</c>, <c>FloatingPointLiteral</c>, <c>CharacterLiteral</c>, <c>DateLiteral</c>, <c>Nothing</c>, <c>AddressOf</c>, <c>GetTypeKeyword</c>, <c>TypeOf</c>, <c>GetXmlNamespace</c>, <c>MyClass</c>, <c>DirectCast</c>, <c>TryCast</c>, <c>IIf</c>, <c>CBool</c>, <c>CByte</c>, <c>CChar</c>, <c>CDate</c>, <c>CDec</c>, <c>CDbl</c>, <c>CInt</c>, <c>CLng</c>, <c>CObj</c>, <c>CSByte</c>, <c>CShort</c>, <c>CSng</c>, <c>CStr</c>, <c>CUInt</c>, <c>CULng</c>, <c>CUShort</c>.
		/// The non-terminal can start with: this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression().
		/// The non-terminal can start with: (this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) &amp;&amp; (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis })).
		/// </remarks>
		protected virtual bool MatchConcatenationOperatorExpression(out Expression expression) {
			int startOffset = this.LookAheadToken.StartOffset;
			Expression rightExpression;
			if (!this.MatchAdditiveExpression(out expression))
				return false;
			while (this.TokenIs(this.LookAheadToken, VBTokenID.StringConcatenation)) {
				if (!this.Match(VBTokenID.StringConcatenation))
					return false;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
					this.Match(VBTokenID.LineTerminator);
				}
				if (!this.MatchConcatenationOperatorExpression(out rightExpression))
					return false;
				expression = new BinaryExpression(OperatorType.StringConcatenation, expression, rightExpression, new TextRange(startOffset, this.Token.EndOffset));
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>ComparisonExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ComparisonExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Global</c>, <c>OpenParenthesis</c>, <c>OpenCurlyBrace</c>, <c>New</c>, <c>XmlLiteral</c>, <c>Object</c>, <c>Single</c>, <c>Double</c>, <c>Decimal</c>, <c>Boolean</c>, <c>Date</c>, <c>Char</c>, <c>String</c>, <c>Byte</c>, <c>SByte</c>, <c>UShort</c>, <c>Short</c>, <c>UInteger</c>, <c>Integer</c>, <c>ULong</c>, <c>Long</c>, <c>Sub</c>, <c>Function</c>, <c>Addition</c>, <c>Subtraction</c>, <c>CType</c>, <c>StringLiteral</c>, <c>MyBase</c>, <c>Me</c>, <c>DecimalIntegerLiteral</c>, <c>Mid</c>, <c>True</c>, <c>False</c>, <c>HexadecimalIntegerLiteral</c>, <c>OctalIntegerLiteral</c>, <c>FloatingPointLiteral</c>, <c>CharacterLiteral</c>, <c>DateLiteral</c>, <c>Nothing</c>, <c>AddressOf</c>, <c>GetTypeKeyword</c>, <c>TypeOf</c>, <c>GetXmlNamespace</c>, <c>MyClass</c>, <c>DirectCast</c>, <c>TryCast</c>, <c>IIf</c>, <c>CBool</c>, <c>CByte</c>, <c>CChar</c>, <c>CDate</c>, <c>CDec</c>, <c>CDbl</c>, <c>CInt</c>, <c>CLng</c>, <c>CObj</c>, <c>CSByte</c>, <c>CShort</c>, <c>CSng</c>, <c>CStr</c>, <c>CUInt</c>, <c>CULng</c>, <c>CUShort</c>.
		/// The non-terminal can start with: this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression().
		/// The non-terminal can start with: (this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) &amp;&amp; (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis })).
		/// </remarks>
		protected virtual bool MatchComparisonExpression(out Expression expression) {
			int startOffset = this.LookAheadToken.StartOffset;
			Expression rightExpression;
			if (!this.MatchShiftOperatorExpression(out expression))
				return false;
			while (this.IsInMultiMatchSet(31, this.LookAheadToken)) {
				OperatorType operatorType = OperatorType.None;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.Equality)) {
					if (!this.Match(VBTokenID.Equality))
						return false;
					else {
						operatorType = OperatorType.Equality;
					}
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.Inequality)) {
					if (!this.Match(VBTokenID.Inequality))
						return false;
					else {
						operatorType = OperatorType.Inequality;
					}
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.LessThan)) {
					if (!this.Match(VBTokenID.LessThan))
						return false;
					else {
						operatorType = OperatorType.LessThan;
					}
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.GreaterThan)) {
					if (!this.Match(VBTokenID.GreaterThan))
						return false;
					else {
						operatorType = OperatorType.GreaterThan;
					}
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.LessThanOrEqual)) {
					if (!this.Match(VBTokenID.LessThanOrEqual))
						return false;
					else {
						operatorType = OperatorType.LessThanOrEqual;
					}
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.GreaterThanOrEqual)) {
					if (!this.Match(VBTokenID.GreaterThanOrEqual))
						return false;
					else {
						operatorType = OperatorType.GreaterThanOrEqual;
					}
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.Is)) {
					if (!this.Match(VBTokenID.Is))
						return false;
					else {
						operatorType = OperatorType.ReferenceEquality;
					}
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.IsNot)) {
					if (!this.Match(VBTokenID.IsNot))
						return false;
					else {
						operatorType = OperatorType.ReferenceInequality;
					}
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.Like)) {
					if (!this.Match(VBTokenID.Like))
						return false;
					else {
						operatorType = OperatorType.Like;
					}
				}
				else
					return false;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
					this.Match(VBTokenID.LineTerminator);
				}
				if (!this.MatchComparisonExpression(out rightExpression))
					return false;
				expression = new BinaryExpression(operatorType, expression, rightExpression, new TextRange(startOffset, this.Token.EndOffset));
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>NegationExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>NegationExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Global</c>, <c>OpenParenthesis</c>, <c>OpenCurlyBrace</c>, <c>New</c>, <c>XmlLiteral</c>, <c>Object</c>, <c>Single</c>, <c>Double</c>, <c>Decimal</c>, <c>Boolean</c>, <c>Date</c>, <c>Char</c>, <c>String</c>, <c>Byte</c>, <c>SByte</c>, <c>UShort</c>, <c>Short</c>, <c>UInteger</c>, <c>Integer</c>, <c>ULong</c>, <c>Long</c>, <c>Sub</c>, <c>Function</c>, <c>Addition</c>, <c>Subtraction</c>, <c>Not</c>, <c>CType</c>, <c>StringLiteral</c>, <c>MyBase</c>, <c>Me</c>, <c>DecimalIntegerLiteral</c>, <c>Mid</c>, <c>True</c>, <c>False</c>, <c>HexadecimalIntegerLiteral</c>, <c>OctalIntegerLiteral</c>, <c>FloatingPointLiteral</c>, <c>CharacterLiteral</c>, <c>DateLiteral</c>, <c>Nothing</c>, <c>AddressOf</c>, <c>GetTypeKeyword</c>, <c>TypeOf</c>, <c>GetXmlNamespace</c>, <c>MyClass</c>, <c>DirectCast</c>, <c>TryCast</c>, <c>IIf</c>, <c>CBool</c>, <c>CByte</c>, <c>CChar</c>, <c>CDate</c>, <c>CDec</c>, <c>CDbl</c>, <c>CInt</c>, <c>CLng</c>, <c>CObj</c>, <c>CSByte</c>, <c>CShort</c>, <c>CSng</c>, <c>CStr</c>, <c>CUInt</c>, <c>CULng</c>, <c>CUShort</c>.
		/// The non-terminal can start with: this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression().
		/// The non-terminal can start with: (this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) &amp;&amp; (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis })).
		/// </remarks>
		protected virtual bool MatchNegationExpression(out Expression expression) {
			OperatorType operatorType = OperatorType.None;
			while (this.TokenIs(this.LookAheadToken, VBTokenID.Not)) {
				if (this.Match(VBTokenID.Not)) {
					operatorType = OperatorType.Negation;
				}
				if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
					this.Match(VBTokenID.LineTerminator);
				}
			}
			if (!this.MatchComparisonExpression(out expression))
				return false;
			if (operatorType != OperatorType.None)
				expression = new UnaryExpression(operatorType, expression);
			return true;
		}

		/// <summary>
		/// Matches a <c>ConjunctionExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ConjunctionExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Global</c>, <c>OpenParenthesis</c>, <c>OpenCurlyBrace</c>, <c>New</c>, <c>XmlLiteral</c>, <c>Object</c>, <c>Single</c>, <c>Double</c>, <c>Decimal</c>, <c>Boolean</c>, <c>Date</c>, <c>Char</c>, <c>String</c>, <c>Byte</c>, <c>SByte</c>, <c>UShort</c>, <c>Short</c>, <c>UInteger</c>, <c>Integer</c>, <c>ULong</c>, <c>Long</c>, <c>Sub</c>, <c>Function</c>, <c>Addition</c>, <c>Subtraction</c>, <c>Not</c>, <c>CType</c>, <c>StringLiteral</c>, <c>MyBase</c>, <c>Me</c>, <c>DecimalIntegerLiteral</c>, <c>Mid</c>, <c>True</c>, <c>False</c>, <c>HexadecimalIntegerLiteral</c>, <c>OctalIntegerLiteral</c>, <c>FloatingPointLiteral</c>, <c>CharacterLiteral</c>, <c>DateLiteral</c>, <c>Nothing</c>, <c>AddressOf</c>, <c>GetTypeKeyword</c>, <c>TypeOf</c>, <c>GetXmlNamespace</c>, <c>MyClass</c>, <c>DirectCast</c>, <c>TryCast</c>, <c>IIf</c>, <c>CBool</c>, <c>CByte</c>, <c>CChar</c>, <c>CDate</c>, <c>CDec</c>, <c>CDbl</c>, <c>CInt</c>, <c>CLng</c>, <c>CObj</c>, <c>CSByte</c>, <c>CShort</c>, <c>CSng</c>, <c>CStr</c>, <c>CUInt</c>, <c>CULng</c>, <c>CUShort</c>.
		/// The non-terminal can start with: this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression().
		/// The non-terminal can start with: (this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) &amp;&amp; (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis })).
		/// </remarks>
		protected virtual bool MatchConjunctionExpression(out Expression expression) {
			int startOffset = this.LookAheadToken.StartOffset;
			Expression rightExpression;
			if (!this.MatchNegationExpression(out expression))
				return false;
			while (((this.TokenIs(this.LookAheadToken, VBTokenID.And)) || (this.TokenIs(this.LookAheadToken, VBTokenID.AndAlso)))) {
				if (this.TokenIs(this.LookAheadToken, VBTokenID.And)) {
					if (!this.Match(VBTokenID.And))
						return false;
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.AndAlso)) {
					if (!this.Match(VBTokenID.AndAlso))
						return false;
				}
				else
					return false;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
					this.Match(VBTokenID.LineTerminator);
				}
				if (!this.MatchConjunctionExpression(out rightExpression))
					return false;
				expression = new BinaryExpression(OperatorType.ConditionalAnd, expression, rightExpression, new TextRange(startOffset, this.Token.EndOffset));
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>InclusiveDisjunctionExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>InclusiveDisjunctionExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Global</c>, <c>OpenParenthesis</c>, <c>OpenCurlyBrace</c>, <c>New</c>, <c>XmlLiteral</c>, <c>Object</c>, <c>Single</c>, <c>Double</c>, <c>Decimal</c>, <c>Boolean</c>, <c>Date</c>, <c>Char</c>, <c>String</c>, <c>Byte</c>, <c>SByte</c>, <c>UShort</c>, <c>Short</c>, <c>UInteger</c>, <c>Integer</c>, <c>ULong</c>, <c>Long</c>, <c>Sub</c>, <c>Function</c>, <c>Addition</c>, <c>Subtraction</c>, <c>Not</c>, <c>CType</c>, <c>StringLiteral</c>, <c>MyBase</c>, <c>Me</c>, <c>DecimalIntegerLiteral</c>, <c>Mid</c>, <c>True</c>, <c>False</c>, <c>HexadecimalIntegerLiteral</c>, <c>OctalIntegerLiteral</c>, <c>FloatingPointLiteral</c>, <c>CharacterLiteral</c>, <c>DateLiteral</c>, <c>Nothing</c>, <c>AddressOf</c>, <c>GetTypeKeyword</c>, <c>TypeOf</c>, <c>GetXmlNamespace</c>, <c>MyClass</c>, <c>DirectCast</c>, <c>TryCast</c>, <c>IIf</c>, <c>CBool</c>, <c>CByte</c>, <c>CChar</c>, <c>CDate</c>, <c>CDec</c>, <c>CDbl</c>, <c>CInt</c>, <c>CLng</c>, <c>CObj</c>, <c>CSByte</c>, <c>CShort</c>, <c>CSng</c>, <c>CStr</c>, <c>CUInt</c>, <c>CULng</c>, <c>CUShort</c>.
		/// The non-terminal can start with: this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression().
		/// The non-terminal can start with: (this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) &amp;&amp; (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis })).
		/// </remarks>
		protected virtual bool MatchInclusiveDisjunctionExpression(out Expression expression) {
			int startOffset = this.LookAheadToken.StartOffset;
			Expression rightExpression;
			if (!this.MatchConjunctionExpression(out expression))
				return false;
			while (((this.TokenIs(this.LookAheadToken, VBTokenID.Or)) || (this.TokenIs(this.LookAheadToken, VBTokenID.OrElse)))) {
				if (this.TokenIs(this.LookAheadToken, VBTokenID.Or)) {
					if (!this.Match(VBTokenID.Or))
						return false;
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.OrElse)) {
					if (!this.Match(VBTokenID.OrElse))
						return false;
				}
				else
					return false;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
					this.Match(VBTokenID.LineTerminator);
				}
				if (!this.MatchInclusiveDisjunctionExpression(out rightExpression))
					return false;
				expression = new BinaryExpression(OperatorType.ConditionalOr, expression, rightExpression, new TextRange(startOffset, this.Token.EndOffset));
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>ExclusiveDisjunctionExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ExclusiveDisjunctionExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Global</c>, <c>OpenParenthesis</c>, <c>OpenCurlyBrace</c>, <c>New</c>, <c>XmlLiteral</c>, <c>Object</c>, <c>Single</c>, <c>Double</c>, <c>Decimal</c>, <c>Boolean</c>, <c>Date</c>, <c>Char</c>, <c>String</c>, <c>Byte</c>, <c>SByte</c>, <c>UShort</c>, <c>Short</c>, <c>UInteger</c>, <c>Integer</c>, <c>ULong</c>, <c>Long</c>, <c>Sub</c>, <c>Function</c>, <c>Addition</c>, <c>Subtraction</c>, <c>Not</c>, <c>CType</c>, <c>StringLiteral</c>, <c>MyBase</c>, <c>Me</c>, <c>DecimalIntegerLiteral</c>, <c>Mid</c>, <c>True</c>, <c>False</c>, <c>HexadecimalIntegerLiteral</c>, <c>OctalIntegerLiteral</c>, <c>FloatingPointLiteral</c>, <c>CharacterLiteral</c>, <c>DateLiteral</c>, <c>Nothing</c>, <c>AddressOf</c>, <c>GetTypeKeyword</c>, <c>TypeOf</c>, <c>GetXmlNamespace</c>, <c>MyClass</c>, <c>DirectCast</c>, <c>TryCast</c>, <c>IIf</c>, <c>CBool</c>, <c>CByte</c>, <c>CChar</c>, <c>CDate</c>, <c>CDec</c>, <c>CDbl</c>, <c>CInt</c>, <c>CLng</c>, <c>CObj</c>, <c>CSByte</c>, <c>CShort</c>, <c>CSng</c>, <c>CStr</c>, <c>CUInt</c>, <c>CULng</c>, <c>CUShort</c>.
		/// The non-terminal can start with: this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression().
		/// The non-terminal can start with: (this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) &amp;&amp; (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis })).
		/// </remarks>
		protected virtual bool MatchExclusiveDisjunctionExpression(out Expression expression) {
			int startOffset = this.LookAheadToken.StartOffset;
			Expression rightExpression;
			if (!this.MatchInclusiveDisjunctionExpression(out expression))
				return false;
			while (this.TokenIs(this.LookAheadToken, VBTokenID.Xor)) {
				if (!this.Match(VBTokenID.Xor))
					return false;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
					this.Match(VBTokenID.LineTerminator);
				}
				if (!this.MatchExclusiveDisjunctionExpression(out rightExpression))
					return false;
				expression = new BinaryExpression(OperatorType.ExclusiveOr, expression, rightExpression, new TextRange(startOffset, this.Token.EndOffset));
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>ShiftOperatorExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ShiftOperatorExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Global</c>, <c>OpenParenthesis</c>, <c>OpenCurlyBrace</c>, <c>New</c>, <c>XmlLiteral</c>, <c>Object</c>, <c>Single</c>, <c>Double</c>, <c>Decimal</c>, <c>Boolean</c>, <c>Date</c>, <c>Char</c>, <c>String</c>, <c>Byte</c>, <c>SByte</c>, <c>UShort</c>, <c>Short</c>, <c>UInteger</c>, <c>Integer</c>, <c>ULong</c>, <c>Long</c>, <c>Sub</c>, <c>Function</c>, <c>Addition</c>, <c>Subtraction</c>, <c>CType</c>, <c>StringLiteral</c>, <c>MyBase</c>, <c>Me</c>, <c>DecimalIntegerLiteral</c>, <c>Mid</c>, <c>True</c>, <c>False</c>, <c>HexadecimalIntegerLiteral</c>, <c>OctalIntegerLiteral</c>, <c>FloatingPointLiteral</c>, <c>CharacterLiteral</c>, <c>DateLiteral</c>, <c>Nothing</c>, <c>AddressOf</c>, <c>GetTypeKeyword</c>, <c>TypeOf</c>, <c>GetXmlNamespace</c>, <c>MyClass</c>, <c>DirectCast</c>, <c>TryCast</c>, <c>IIf</c>, <c>CBool</c>, <c>CByte</c>, <c>CChar</c>, <c>CDate</c>, <c>CDec</c>, <c>CDbl</c>, <c>CInt</c>, <c>CLng</c>, <c>CObj</c>, <c>CSByte</c>, <c>CShort</c>, <c>CSng</c>, <c>CStr</c>, <c>CUInt</c>, <c>CULng</c>, <c>CUShort</c>.
		/// The non-terminal can start with: this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression().
		/// The non-terminal can start with: (this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) &amp;&amp; (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis })).
		/// </remarks>
		protected virtual bool MatchShiftOperatorExpression(out Expression expression) {
			int startOffset = this.LookAheadToken.StartOffset;
			Expression rightExpression;
			if (!this.MatchConcatenationOperatorExpression(out expression))
				return false;
			while (((this.TokenIs(this.LookAheadToken, VBTokenID.LeftShift)) || (this.TokenIs(this.LookAheadToken, VBTokenID.RightShift)))) {
				OperatorType operatorType = OperatorType.None;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.LeftShift)) {
					if (!this.Match(VBTokenID.LeftShift))
						return false;
					else {
						operatorType = OperatorType.LeftShift;
					}
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.RightShift)) {
					if (!this.Match(VBTokenID.RightShift))
						return false;
					else {
						operatorType = OperatorType.RightShift;
					}
				}
				else
					return false;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
					this.Match(VBTokenID.LineTerminator);
				}
				if (!this.MatchShiftOperatorExpression(out rightExpression))
					return false;
				expression = new BinaryExpression(operatorType, expression, rightExpression, new TextRange(startOffset, this.Token.EndOffset));
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>LambdaExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>LambdaExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Sub</c>, <c>Function</c>.
		/// </remarks>
		protected virtual bool MatchLambdaExpression(out Expression expression) {
			expression = null;
			int startOffset = this.LookAheadToken.StartOffset;
			AstNodeList parameterList = null;
			Expression childExpression;
			bool isFunction = false;
			TypeReference typeReference = null;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.Sub)) {
				if (!this.Match(VBTokenID.Sub))
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Function)) {
				if (!this.Match(VBTokenID.Function))
					return false;
				else {
					isFunction = true;
				}
			}
			else
				return false;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.OpenParenthesis)) {
				if (!this.Match(VBTokenID.OpenParenthesis))
					return false;
				if (this.IsInMultiMatchSet(17, this.LookAheadToken)) {
					if (!this.MatchParameterList(null, out parameterList))
						return false;
				}
				if (!this.Match(VBTokenID.CloseParenthesis))
					return false;
			}
			if (((this.TokenIs(this.LookAheadToken, VBTokenID.As)) || (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)))) {
				// Multi-line
				Statement statement;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.As)) {
					if (!this.Match(VBTokenID.As))
						return false;
					if (!this.MatchTypeName(out typeReference, false))
						return false;
				}
				this.Match(VBTokenID.LineTerminator);
				if (!this.MatchBlock(out statement))
					return false;
				if (!this.Match(VBTokenID.End))
					return false;
				if (!isFunction) {
					if (!this.Match(VBTokenID.Sub))
						return false;
				}
				if (isFunction) {
					if (!this.Match(VBTokenID.Function))
						return false;
				}
				expression = new LambdaExpression(statement, new TextRange(startOffset, this.Token.EndOffset));
				if (parameterList != null)
					((LambdaExpression)expression).Parameters.AddRange(parameterList.ToArray());
			}
			else if ((this.IsInMultiMatchSet(4, this.LookAheadToken) || (this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression()) || ((this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) && (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis }))))) {
				if (!this.MatchExpression(out childExpression))
					return false;
				// In-line
				expression = new LambdaExpression(new StatementExpression(childExpression), new TextRange(startOffset, this.Token.EndOffset));
				if (parameterList != null)
					((LambdaExpression)expression).Parameters.AddRange(parameterList.ToArray());
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>QueryExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>QueryExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: (this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) &amp;&amp; (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis })).
		/// </remarks>
		protected virtual bool MatchQueryExpression(out Expression expression) {
			expression = new QueryExpression();
			expression.StartOffset = this.LookAheadToken.StartOffset;
			
			AstNode queryOperator;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.From)) {
				if (!this.MatchFromQueryOperator(out queryOperator))
					return false;
				else {
					((QueryExpression)expression).QueryOperators.Add(queryOperator);
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Aggregate)) {
				if (!this.MatchAggregateQueryOperator(out queryOperator))
					return false;
				else {
					((QueryExpression)expression).QueryOperators.Add(queryOperator);
				}
			}
			else
				return false;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
				this.Match(VBTokenID.LineTerminator);
			}
			if (!this.MatchQueryOperator(out queryOperator))
				return false;
			else {
				((QueryExpression)expression).QueryOperators.Add(queryOperator);
			}
			if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
				this.Match(VBTokenID.LineTerminator);
			}
			while ((this.IsInMultiMatchSet(32, this.LookAheadToken) || (this.AreNextTwo(VBTokenID.Group, VBTokenID.Join)))) {
				if (!this.MatchQueryOperator(out queryOperator))
					return false;
				else {
					((QueryExpression)expression).QueryOperators.Add(queryOperator);
				}
				if (this.AreNextTwo(VBTokenID.LineTerminator, VBTokenID.From, VBTokenID.Aggregate, VBTokenID.Select, VBTokenID.Distinct, VBTokenID.Where, VBTokenID.Order, VBTokenID.Skip, VBTokenID.Take, VBTokenID.Let, VBTokenID.Group, VBTokenID.Join)) {
					if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
						this.Match(VBTokenID.LineTerminator);
					}
				}
			}
			expression.EndOffset = this.Token.EndOffset;
			return true;
		}

		/// <summary>
		/// Matches a <c>QueryOperator</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>QueryOperator</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Aggregate</c>, <c>Distinct</c>, <c>From</c>, <c>Group</c>, <c>Join</c>, <c>Order</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Select</c>, <c>Let</c>.
		/// The non-terminal can start with: this.AreNextTwo(VBTokenID.Group, VBTokenID.Join).
		/// </remarks>
		protected virtual bool MatchQueryOperator(out AstNode queryOperator) {
			queryOperator = null;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.From)) {
				if (!this.MatchFromQueryOperator(out queryOperator))
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Aggregate)) {
				if (!this.MatchAggregateQueryOperator(out queryOperator))
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Select)) {
				if (!this.MatchSelectQueryOperator(out queryOperator))
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Distinct)) {
				if (!this.MatchDistinctQueryOperator(out queryOperator))
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Where)) {
				if (!this.MatchWhereQueryOperator(out queryOperator))
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Order)) {
				if (!this.MatchOrderByQueryOperator(out queryOperator))
					return false;
			}
			else if (((this.TokenIs(this.LookAheadToken, VBTokenID.Skip)) || (this.TokenIs(this.LookAheadToken, VBTokenID.Take)))) {
				if (!this.MatchPartitionQueryOperator(out queryOperator))
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Let)) {
				if (!this.MatchLetQueryOperator(out queryOperator))
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Group)) {
				if (!this.MatchGroupByQueryOperator(out queryOperator))
					return false;
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Join)) {
				if (!this.MatchJoinQueryOperator(out queryOperator))
					return false;
			}
			else if (((this.AreNextTwo(VBTokenID.Group, VBTokenID.Join)))) {
				if (!this.MatchGroupJoinQueryOperator(out queryOperator))
					return false;
			}
			else
				return false;
			return true;
		}

		/// <summary>
		/// Matches a <c>CollectionRangeVariableDeclaration</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>CollectionRangeVariableDeclaration</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>.
		/// </remarks>
		protected virtual bool MatchCollectionRangeVariableDeclaration(out CollectionRangeVariableDeclaration variableDeclaration) {
			variableDeclaration = new CollectionRangeVariableDeclaration();
			variableDeclaration.StartOffset = this.LookAheadToken.StartOffset;
			
			QualifiedIdentifier variableName;
			TypeReference typeReference = new TypeReference(TypeReference.AnonymousTypeName, new TextRange(this.LookAheadToken.StartOffset));
			Expression source;
			if (!this.MatchNonQueryIdentifier(out variableName))
				return false;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.As)) {
				if (!this.Match(VBTokenID.As))
					return false;
				if (!this.MatchTypeName(out typeReference, false))
					return false;
			}
			variableDeclaration.VariableDeclarator = new VariableDeclarator(typeReference, variableName, false, true);
			if (!this.Match(VBTokenID.In))
				return false;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
				this.Match(VBTokenID.LineTerminator);
			}
			if (!this.MatchExpression(out source))
				return false;
			else {
				variableDeclaration.Source = source;
			}
			variableDeclaration.EndOffset = this.Token.EndOffset;
			return true;
		}

		/// <summary>
		/// Matches a <c>ExpressionRangeVariableDeclaration</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>ExpressionRangeVariableDeclaration</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Global</c>, <c>OpenParenthesis</c>, <c>OpenCurlyBrace</c>, <c>New</c>, <c>XmlLiteral</c>, <c>Object</c>, <c>Single</c>, <c>Double</c>, <c>Decimal</c>, <c>Boolean</c>, <c>Date</c>, <c>Char</c>, <c>String</c>, <c>Byte</c>, <c>SByte</c>, <c>UShort</c>, <c>Short</c>, <c>UInteger</c>, <c>Integer</c>, <c>ULong</c>, <c>Long</c>, <c>Sub</c>, <c>Function</c>, <c>Addition</c>, <c>Subtraction</c>, <c>Not</c>, <c>CType</c>, <c>StringLiteral</c>, <c>MyBase</c>, <c>Me</c>, <c>DecimalIntegerLiteral</c>, <c>Mid</c>, <c>True</c>, <c>False</c>, <c>HexadecimalIntegerLiteral</c>, <c>OctalIntegerLiteral</c>, <c>FloatingPointLiteral</c>, <c>CharacterLiteral</c>, <c>DateLiteral</c>, <c>Nothing</c>, <c>AddressOf</c>, <c>GetTypeKeyword</c>, <c>TypeOf</c>, <c>GetXmlNamespace</c>, <c>MyClass</c>, <c>DirectCast</c>, <c>TryCast</c>, <c>IIf</c>, <c>CBool</c>, <c>CByte</c>, <c>CChar</c>, <c>CDate</c>, <c>CDec</c>, <c>CDbl</c>, <c>CInt</c>, <c>CLng</c>, <c>CObj</c>, <c>CSByte</c>, <c>CShort</c>, <c>CSng</c>, <c>CStr</c>, <c>CUInt</c>, <c>CULng</c>, <c>CUShort</c>.
		/// The non-terminal can start with: this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression().
		/// The non-terminal can start with: (this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) &amp;&amp; (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis })).
		/// </remarks>
		protected virtual bool MatchExpressionRangeVariableDeclaration(out VariableDeclarator variableDeclarator) {
			variableDeclarator = null;
			
			QualifiedIdentifier variableName = null;
			TypeReference typeReference = new TypeReference(TypeReference.AnonymousTypeName, new TextRange(this.LookAheadToken.StartOffset));
			Expression initializer;
			int startOffset = this.LookAheadToken.StartOffset;
			if ((this.AreNextTwoIdentifierAnd(VBTokenID.As)) || (this.AreNextTwoIdentifierAnd(VBTokenID.Equality))) {
				if (!this.MatchNonQueryIdentifier(out variableName))
					return false;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.As)) {
					if (!this.Match(VBTokenID.As))
						return false;
					if (!this.MatchTypeName(out typeReference, false))
						return false;
				}
				if (!this.Match(VBTokenID.Equality))
					return false;
			}
			variableDeclarator = new VariableDeclarator(typeReference, variableName, false, true);
			variableDeclarator.StartOffset = startOffset;
			if (!this.MatchExpression(out initializer))
				return false;
			else {
				variableDeclarator.Initializer = initializer;
			}
			variableDeclarator.EndOffset = this.Token.EndOffset;
			return true;
		}

		/// <summary>
		/// Matches a <c>FromQueryOperator</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>FromQueryOperator</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>From</c>.
		/// </remarks>
		protected virtual bool MatchFromQueryOperator(out AstNode fromQueryOperator) {
			fromQueryOperator = new FromQueryOperator();
			fromQueryOperator.StartOffset = this.LookAheadToken.StartOffset;
			
			CollectionRangeVariableDeclaration variableDeclaration = null;
			if (!this.Match(VBTokenID.From))
				return false;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
				this.Match(VBTokenID.LineTerminator);
			}
			if (!this.MatchCollectionRangeVariableDeclaration(out variableDeclaration))
				return false;
			else {
				((FromQueryOperator)fromQueryOperator).CollectionRangeVariableDeclarations.Add(variableDeclaration);
			}
			while (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
				if (!this.Match(VBTokenID.Comma))
					return false;
				if (!this.MatchCollectionRangeVariableDeclaration(out variableDeclaration))
					return false;
				else {
					((FromQueryOperator)fromQueryOperator).CollectionRangeVariableDeclarations.Add(variableDeclaration);
				}
			}
			fromQueryOperator.EndOffset = this.Token.EndOffset;
			return true;
		}

		/// <summary>
		/// Matches a <c>JoinQueryOperator</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>JoinQueryOperator</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Join</c>.
		/// </remarks>
		protected virtual bool MatchJoinQueryOperator(out AstNode joinQueryOperator) {
			joinQueryOperator = new JoinQueryOperator();
			joinQueryOperator.StartOffset = this.LookAheadToken.StartOffset;
			
			CollectionRangeVariableDeclaration variableDeclaration = null;
			AstNode childJoinQueryOperator;
			JoinCondition condition;
			if (!this.Match(VBTokenID.Join))
				return false;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
				this.Match(VBTokenID.LineTerminator);
			}
			if (!this.MatchCollectionRangeVariableDeclaration(out variableDeclaration))
				return false;
			else {
				((JoinQueryOperator)joinQueryOperator).CollectionRangeVariableDeclaration = variableDeclaration;
			}
			if (this.TokenIs(this.LookAheadToken, VBTokenID.Join)) {
				if (!this.MatchJoinQueryOperator(out childJoinQueryOperator))
					return false;
				else {
					((JoinQueryOperator)joinQueryOperator).ChildJoin = (JoinQueryOperator)childJoinQueryOperator;
				}
			}
			if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
				this.Match(VBTokenID.LineTerminator);
			}
			if (!this.Match(VBTokenID.On))
				return false;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
				this.Match(VBTokenID.LineTerminator);
			}
			if (!this.MatchJoinCondition(out condition))
				return false;
			else {
				((JoinQueryOperator)joinQueryOperator).Conditions.Add(condition);
			}
			while (this.TokenIs(this.LookAheadToken, VBTokenID.And)) {
				if (!this.Match(VBTokenID.And))
					return false;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
					this.Match(VBTokenID.LineTerminator);
				}
				if (!this.MatchJoinCondition(out condition))
					return false;
				else {
					((JoinQueryOperator)joinQueryOperator).Conditions.Add(condition);
				}
			}
			joinQueryOperator.EndOffset = this.Token.EndOffset;
			return true;
		}

		/// <summary>
		/// Matches a <c>JoinCondition</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>JoinCondition</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Global</c>, <c>OpenParenthesis</c>, <c>OpenCurlyBrace</c>, <c>New</c>, <c>XmlLiteral</c>, <c>Object</c>, <c>Single</c>, <c>Double</c>, <c>Decimal</c>, <c>Boolean</c>, <c>Date</c>, <c>Char</c>, <c>String</c>, <c>Byte</c>, <c>SByte</c>, <c>UShort</c>, <c>Short</c>, <c>UInteger</c>, <c>Integer</c>, <c>ULong</c>, <c>Long</c>, <c>Sub</c>, <c>Function</c>, <c>Addition</c>, <c>Subtraction</c>, <c>Not</c>, <c>CType</c>, <c>StringLiteral</c>, <c>MyBase</c>, <c>Me</c>, <c>DecimalIntegerLiteral</c>, <c>Mid</c>, <c>True</c>, <c>False</c>, <c>HexadecimalIntegerLiteral</c>, <c>OctalIntegerLiteral</c>, <c>FloatingPointLiteral</c>, <c>CharacterLiteral</c>, <c>DateLiteral</c>, <c>Nothing</c>, <c>AddressOf</c>, <c>GetTypeKeyword</c>, <c>TypeOf</c>, <c>GetXmlNamespace</c>, <c>MyClass</c>, <c>DirectCast</c>, <c>TryCast</c>, <c>IIf</c>, <c>CBool</c>, <c>CByte</c>, <c>CChar</c>, <c>CDate</c>, <c>CDec</c>, <c>CDbl</c>, <c>CInt</c>, <c>CLng</c>, <c>CObj</c>, <c>CSByte</c>, <c>CShort</c>, <c>CSng</c>, <c>CStr</c>, <c>CUInt</c>, <c>CULng</c>, <c>CUShort</c>.
		/// The non-terminal can start with: this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression().
		/// The non-terminal can start with: (this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) &amp;&amp; (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis })).
		/// </remarks>
		protected virtual bool MatchJoinCondition(out JoinCondition condition) {
			condition = new JoinCondition();
			condition.StartOffset = this.LookAheadToken.StartOffset;
			
			Expression expression;
			if (!this.MatchExpression(out expression))
				return false;
			else {
				condition.LeftConditionExpression = expression;
			}
			if (!this.Match(VBTokenID.Equals))
				return false;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
				this.Match(VBTokenID.LineTerminator);
			}
			if (!this.MatchExpression(out expression))
				return false;
			else {
				condition.RightConditionExpression = expression;
			}
			condition.EndOffset = this.Token.EndOffset;
			return true;
		}

		/// <summary>
		/// Matches a <c>LetQueryOperator</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>LetQueryOperator</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Let</c>.
		/// </remarks>
		protected virtual bool MatchLetQueryOperator(out AstNode letQueryOperator) {
			letQueryOperator = new LetQueryOperator();
			letQueryOperator.StartOffset = this.LookAheadToken.StartOffset;
			
			VariableDeclarator variableDeclarator;
			if (!this.Match(VBTokenID.Let))
				return false;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
				this.Match(VBTokenID.LineTerminator);
			}
			if (!this.MatchExpressionRangeVariableDeclaration(out variableDeclarator))
				return false;
			else {
				((LetQueryOperator)letQueryOperator).VariableDeclarators.Add(variableDeclarator);
			}
			while (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
				if (!this.Match(VBTokenID.Comma))
					return false;
				if (!this.MatchExpressionRangeVariableDeclaration(out variableDeclarator))
					return false;
				else {
					((LetQueryOperator)letQueryOperator).VariableDeclarators.Add(variableDeclarator);
				}
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>SelectQueryOperator</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>SelectQueryOperator</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Select</c>.
		/// </remarks>
		protected virtual bool MatchSelectQueryOperator(out AstNode selectQueryOperator) {
			selectQueryOperator = new SelectQueryOperator();
			selectQueryOperator.StartOffset = this.LookAheadToken.StartOffset;
			
			VariableDeclarator variableDeclarator;
			if (!this.Match(VBTokenID.Select))
				return false;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
				this.Match(VBTokenID.LineTerminator);
			}
			if (!this.MatchExpressionRangeVariableDeclaration(out variableDeclarator))
				return false;
			else {
				((SelectQueryOperator)selectQueryOperator).VariableDeclarators.Add(variableDeclarator);
			}
			while (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
				if (!this.Match(VBTokenID.Comma))
					return false;
				if (!this.MatchExpressionRangeVariableDeclaration(out variableDeclarator))
					return false;
				else {
					((SelectQueryOperator)selectQueryOperator).VariableDeclarators.Add(variableDeclarator);
				}
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>DistinctQueryOperator</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>DistinctQueryOperator</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Distinct</c>.
		/// </remarks>
		protected virtual bool MatchDistinctQueryOperator(out AstNode distinctQueryOperator) {
			distinctQueryOperator = new DistinctQueryOperator();
			distinctQueryOperator.TextRange = new TextRange(this.LookAheadToken.StartOffset);
			if (!this.Match(VBTokenID.Distinct))
				return false;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
				this.Match(VBTokenID.LineTerminator);
			}
			return true;
		}

		/// <summary>
		/// Matches a <c>WhereQueryOperator</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>WhereQueryOperator</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Where</c>.
		/// </remarks>
		protected virtual bool MatchWhereQueryOperator(out AstNode whereQueryOperator) {
			whereQueryOperator = new WhereQueryOperator();
			whereQueryOperator.StartOffset = this.LookAheadToken.StartOffset;
			
			Expression condition;
			if (!this.Match(VBTokenID.Where))
				return false;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
				this.Match(VBTokenID.LineTerminator);
			}
			if (!this.MatchExpression(out condition))
				return false;
			else {
				((WhereQueryOperator)whereQueryOperator).Condition = condition;
			}
			whereQueryOperator.EndOffset = this.Token.EndOffset;
			return true;
		}

		/// <summary>
		/// Matches a <c>PartitionQueryOperator</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>PartitionQueryOperator</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Skip</c>, <c>Take</c>.
		/// </remarks>
		protected virtual bool MatchPartitionQueryOperator(out AstNode partitionQueryOperator) {
			partitionQueryOperator = null;
			bool hasWhile = false;
			Expression expression;
			int startOffset = this.LookAheadToken.StartOffset;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.Skip)) {
				if (!this.Match(VBTokenID.Skip))
					return false;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.While)) {
					if (this.Match(VBTokenID.While)) {
						hasWhile = true;
					}
				}
				if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
					this.Match(VBTokenID.LineTerminator);
				}
				if (!this.MatchExpression(out expression))
					return false;
				if (hasWhile) {
					partitionQueryOperator = new SkipWhileQueryOperator();
					((SkipWhileQueryOperator)partitionQueryOperator).Expression = expression;
				}
				else {
					partitionQueryOperator = new SkipQueryOperator();
					((SkipQueryOperator)partitionQueryOperator).Expression = expression;
				}
			}
			else if (this.TokenIs(this.LookAheadToken, VBTokenID.Take)) {
				if (!this.Match(VBTokenID.Take))
					return false;
				if (this.TokenIs(this.LookAheadToken, VBTokenID.While)) {
					if (this.Match(VBTokenID.While)) {
						hasWhile = true;
					}
				}
				if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
					this.Match(VBTokenID.LineTerminator);
				}
				if (!this.MatchExpression(out expression))
					return false;
				if (hasWhile) {
					partitionQueryOperator = new TakeWhileQueryOperator();
					((TakeWhileQueryOperator)partitionQueryOperator).Expression = expression;
				}
				else {
					partitionQueryOperator = new TakeQueryOperator();
					((TakeQueryOperator)partitionQueryOperator).Expression = expression;
				}
			}
			else
				return false;
			if (partitionQueryOperator != null)
				partitionQueryOperator.TextRange = new TextRange(startOffset, this.Token.EndOffset);
			return true;
		}

		/// <summary>
		/// Matches a <c>OrderByQueryOperator</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>OrderByQueryOperator</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Order</c>.
		/// </remarks>
		protected virtual bool MatchOrderByQueryOperator(out AstNode orderByQueryOperator) {
			orderByQueryOperator = new OrderByQueryOperator();
			orderByQueryOperator.StartOffset = this.LookAheadToken.StartOffset;
			
			Ordering ordering;
			if (!this.Match(VBTokenID.Order))
				return false;
			if (!this.Match(VBTokenID.By))
				return false;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
				this.Match(VBTokenID.LineTerminator);
			}
			if (!this.MatchOrderExpression(out ordering))
				return false;
			else {
				((OrderByQueryOperator)orderByQueryOperator).Orderings.Add(ordering);
			}
			while (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
				if (!this.Match(VBTokenID.Comma))
					return false;
				if (!this.MatchOrderExpression(out ordering))
					return false;
				else {
					((OrderByQueryOperator)orderByQueryOperator).Orderings.Add(ordering);
				}
			}
			orderByQueryOperator.EndOffset = this.Token.EndOffset;
			return true;
		}

		/// <summary>
		/// Matches a <c>OrderExpression</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>OrderExpression</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Identifier</c>, <c>Aggregate</c>, <c>Ascending</c>, <c>By</c>, <c>Descending</c>, <c>Distinct</c>, <c>Equals</c>, <c>From</c>, <c>Group</c>, <c>Into</c>, <c>Join</c>, <c>Order</c>, <c>Out</c>, <c>Skip</c>, <c>Take</c>, <c>Where</c>, <c>Global</c>, <c>OpenParenthesis</c>, <c>OpenCurlyBrace</c>, <c>New</c>, <c>XmlLiteral</c>, <c>Object</c>, <c>Single</c>, <c>Double</c>, <c>Decimal</c>, <c>Boolean</c>, <c>Date</c>, <c>Char</c>, <c>String</c>, <c>Byte</c>, <c>SByte</c>, <c>UShort</c>, <c>Short</c>, <c>UInteger</c>, <c>Integer</c>, <c>ULong</c>, <c>Long</c>, <c>Sub</c>, <c>Function</c>, <c>Addition</c>, <c>Subtraction</c>, <c>Not</c>, <c>CType</c>, <c>StringLiteral</c>, <c>MyBase</c>, <c>Me</c>, <c>DecimalIntegerLiteral</c>, <c>Mid</c>, <c>True</c>, <c>False</c>, <c>HexadecimalIntegerLiteral</c>, <c>OctalIntegerLiteral</c>, <c>FloatingPointLiteral</c>, <c>CharacterLiteral</c>, <c>DateLiteral</c>, <c>Nothing</c>, <c>AddressOf</c>, <c>GetTypeKeyword</c>, <c>TypeOf</c>, <c>GetXmlNamespace</c>, <c>MyClass</c>, <c>DirectCast</c>, <c>TryCast</c>, <c>IIf</c>, <c>CBool</c>, <c>CByte</c>, <c>CChar</c>, <c>CDate</c>, <c>CDec</c>, <c>CDbl</c>, <c>CInt</c>, <c>CLng</c>, <c>CObj</c>, <c>CSByte</c>, <c>CShort</c>, <c>CSng</c>, <c>CStr</c>, <c>CUInt</c>, <c>CULng</c>, <c>CUShort</c>.
		/// The non-terminal can start with: this.IsDotIdentifierOrKeyword(true) || this.IsConditionalExpression().
		/// The non-terminal can start with: (this.TokenIs(this.LookAheadToken, new int[] { VBTokenID.Aggregate, VBTokenID.From })) &amp;&amp; (!this.TokenIs(this.GetLookAheadToken(2), new int[] { VBTokenID.Comma, VBTokenID.CloseParenthesis })).
		/// </remarks>
		protected virtual bool MatchOrderExpression(out Ordering ordering) {
			ordering = new Ordering();
			ordering.StartOffset = this.LookAheadToken.StartOffset;
			
			Expression expression;
			if (!this.MatchExpression(out expression))
				return false;
			else {
				ordering.Expression = expression;
			}
			if (((this.TokenIs(this.LookAheadToken, VBTokenID.Ascending)) || (this.TokenIs(this.LookAheadToken, VBTokenID.Descending)))) {
				if (this.TokenIs(this.LookAheadToken, VBTokenID.Ascending)) {
					if (!this.Match(VBTokenID.Ascending))
						return false;
					else {
						ordering.Direction = OrderingDirection.Ascending;
					}
				}
				else if (this.TokenIs(this.LookAheadToken, VBTokenID.Descending)) {
					if (!this.Match(VBTokenID.Descending))
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
		/// Matches a <c>GroupByQueryOperator</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>GroupByQueryOperator</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Group</c>.
		/// </remarks>
		protected virtual bool MatchGroupByQueryOperator(out AstNode groupQueryOperator) {
			groupQueryOperator = new GroupQueryOperator();
			groupQueryOperator.StartOffset = this.LookAheadToken.StartOffset;
			
			VariableDeclarator variableDeclarator;
			if (!this.Match(VBTokenID.Group))
				return false;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
				this.Match(VBTokenID.LineTerminator);
			}
			if (!this.TokenIs(this.LookAheadToken, VBTokenID.By)) {
				if (!this.MatchExpressionRangeVariableDeclaration(out variableDeclarator))
					return false;
				else {
					((GroupQueryOperator)groupQueryOperator).Groupings.Add(variableDeclarator);
				}
				while (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
					if (!this.Match(VBTokenID.Comma))
						return false;
					if (!this.MatchExpressionRangeVariableDeclaration(out variableDeclarator))
						return false;
					else {
						((GroupQueryOperator)groupQueryOperator).Groupings.Add(variableDeclarator);
					}
				}
			}
			if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
				this.Match(VBTokenID.LineTerminator);
			}
			if (!this.Match(VBTokenID.By))
				return false;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
				this.Match(VBTokenID.LineTerminator);
			}
			if (!this.MatchExpressionRangeVariableDeclaration(out variableDeclarator))
				return false;
			else {
				((GroupQueryOperator)groupQueryOperator).GroupBys.Add(variableDeclarator);
			}
			while (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
				if (!this.Match(VBTokenID.Comma))
					return false;
				if (!this.MatchExpressionRangeVariableDeclaration(out variableDeclarator))
					return false;
				else {
					((GroupQueryOperator)groupQueryOperator).GroupBys.Add(variableDeclarator);
				}
			}
			if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
				this.Match(VBTokenID.LineTerminator);
			}
			if (!this.Match(VBTokenID.Into))
				return false;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
				this.Match(VBTokenID.LineTerminator);
			}
			if (!this.MatchExpressionRangeVariableDeclaration(out variableDeclarator))
				return false;
			else {
				((GroupQueryOperator)groupQueryOperator).TargetExpressions.Add(variableDeclarator);
			}
			while (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
				if (!this.Match(VBTokenID.Comma))
					return false;
				if (!this.MatchExpressionRangeVariableDeclaration(out variableDeclarator))
					return false;
				else {
					((GroupQueryOperator)groupQueryOperator).TargetExpressions.Add(variableDeclarator);
				}
			}
			groupQueryOperator.EndOffset = this.Token.EndOffset;
			return true;
		}

		/// <summary>
		/// Matches a <c>AggregateQueryOperator</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>AggregateQueryOperator</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: <c>Aggregate</c>.
		/// </remarks>
		protected virtual bool MatchAggregateQueryOperator(out AstNode aggregateQueryOperator) {
			aggregateQueryOperator = new AggregateQueryOperator();
			aggregateQueryOperator.StartOffset = this.LookAheadToken.StartOffset;
			
			CollectionRangeVariableDeclaration variableDeclaration = null;
			AstNode queryOperator;
			VariableDeclarator variableDeclarator;
			if (!this.Match(VBTokenID.Aggregate))
				return false;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
				this.Match(VBTokenID.LineTerminator);
			}
			if (!this.MatchCollectionRangeVariableDeclaration(out variableDeclaration))
				return false;
			else {
				((AggregateQueryOperator)aggregateQueryOperator).CollectionRangeVariableDeclarations.Add(variableDeclaration);
			}
			while (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
				if (!this.Match(VBTokenID.Comma))
					return false;
				if (!this.MatchCollectionRangeVariableDeclaration(out variableDeclaration))
					return false;
				else {
					((AggregateQueryOperator)aggregateQueryOperator).CollectionRangeVariableDeclarations.Add(variableDeclaration);
				}
			}
			if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
				this.Match(VBTokenID.LineTerminator);
			}
			while ((this.IsInMultiMatchSet(32, this.LookAheadToken) || (this.AreNextTwo(VBTokenID.Group, VBTokenID.Join)))) {
				if (!this.MatchQueryOperator(out queryOperator))
					return false;
				else {
					((AggregateQueryOperator)aggregateQueryOperator).QueryOperators.Add(queryOperator);
				}
				if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
					this.Match(VBTokenID.LineTerminator);
				}
			}
			if (!this.Match(VBTokenID.Into))
				return false;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
				this.Match(VBTokenID.LineTerminator);
			}
			if (!this.MatchExpressionRangeVariableDeclaration(out variableDeclarator))
				return false;
			else {
				((AggregateQueryOperator)aggregateQueryOperator).TargetExpressions.Add(variableDeclarator);
			}
			while (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
				if (!this.Match(VBTokenID.Comma))
					return false;
				if (!this.MatchExpressionRangeVariableDeclaration(out variableDeclarator))
					return false;
				else {
					((AggregateQueryOperator)aggregateQueryOperator).TargetExpressions.Add(variableDeclarator);
				}
			}
			aggregateQueryOperator.EndOffset = this.Token.EndOffset;
			return true;
		}

		/// <summary>
		/// Matches a <c>GroupJoinQueryOperator</c> non-terminal.
		/// </summary>
		/// <returns><c>true</c> if the <c>GroupJoinQueryOperator</c> was matched successfully; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// The non-terminal can start with: this.AreNextTwo(VBTokenID.Group, VBTokenID.Join).
		/// </remarks>
		protected virtual bool MatchGroupJoinQueryOperator(out AstNode joinQueryOperator) {
			joinQueryOperator = null;
			int startOffset = this.LookAheadToken.StartOffset;
			VariableDeclarator variableDeclarator;
			if (!this.Match(VBTokenID.Group))
				return false;
			if (!this.MatchJoinQueryOperator(out joinQueryOperator))
				return false;
			else {
				joinQueryOperator.StartOffset = startOffset;
			}
			if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
				this.Match(VBTokenID.LineTerminator);
			}
			if (!this.Match(VBTokenID.Into))
				return false;
			if (this.TokenIs(this.LookAheadToken, VBTokenID.LineTerminator)) {
				this.Match(VBTokenID.LineTerminator);
			}
			if (!this.MatchExpressionRangeVariableDeclaration(out variableDeclarator))
				return false;
			else {
				((JoinQueryOperator)joinQueryOperator).TargetExpressions.Add(variableDeclarator);
			}
			while (this.TokenIs(this.LookAheadToken, VBTokenID.Comma)) {
				if (!this.Match(VBTokenID.Comma))
					return false;
				if (!this.MatchExpressionRangeVariableDeclaration(out variableDeclarator))
					return false;
				else {
					((JoinQueryOperator)joinQueryOperator).TargetExpressions.Add(variableDeclarator);
				}
			}
			joinQueryOperator.EndOffset = this.Token.EndOffset;
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
			// 0: Public Protected Friend Private Default Dim MustInherit MustOverride Narrowing NotInheritable NotOverridable Overloads Overridable Overrides Partial ReadOnly Shadows Shared Static Widening WithEvents WriteOnly 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,Y,n,n,n,Y,n,n,n,n,Y,Y,n,n,n,n,n,n,n,n,Y,Y,Y,n,Y,Y,n,Y,Y,n,Y,n,n,n,n,n,n,n,n,Y,Y,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 1: Identifier Aggregate Ascending By Descending Distinct Equals From Group Into Join Order Out Skip Take Where 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 2: Identifier Aggregate Ascending By Descending Distinct Equals From Group Into Join Order Out Skip Take Where Global New Object Single Double Decimal Boolean Date Char String Byte SByte UShort Short UInteger Integer ULong Long 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,n,n,n,n,n,Y,n,Y,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,Y,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,Y,Y,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,Y,Y,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 3: Identifier Aggregate Ascending By Descending Distinct Equals From Group Into Join Order Out Skip Take Where Global Object Single Double Decimal Boolean Date Char String Byte SByte UShort Short UInteger Integer ULong Long 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,n,n,n,n,n,Y,n,Y,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,Y,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,Y,Y,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,Y,Y,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 4: Identifier Aggregate Ascending By Descending Distinct Equals From Group Into Join Order Out Skip Take Where Global OpenParenthesis OpenCurlyBrace New XmlLiteral Object Single Double Decimal Boolean Date Char String Byte SByte UShort Short UInteger Integer ULong Long Sub Function Addition Subtraction Not CType StringLiteral MyBase Me DecimalIntegerLiteral Mid True False HexadecimalIntegerLiteral OctalIntegerLiteral FloatingPointLiteral CharacterLiteral DateLiteral Nothing AddressOf GetTypeKeyword TypeOf GetXmlNamespace MyClass DirectCast TryCast IIf CBool CByte CChar CDate CDec CDbl CInt CLng CObj CSByte CShort CSng CStr CUInt CULng CUShort 
			{n,n,n,n,n,n,n,n,n,n,n,Y,Y,Y,Y,Y,Y,Y,Y,n,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,Y,n,n,n,n,Y,n,Y,n,n,n,n,Y,Y,Y,Y,Y,Y,Y,Y,n,Y,Y,n,n,Y,Y,Y,Y,Y,Y,Y,Y,n,Y,Y,n,n,n,n,Y,n,Y,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,Y,n,Y,Y,Y,n,n,n,n,Y,n,n,n,n,Y,n,n,n,n,n,n,n,n,Y,n,Y,Y,n,n,n,n,Y,Y,n,n,Y,n,Y,Y,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,Y,Y,n,n,n,Y,n,Y,n,n,n,n,Y,n,Y,Y,Y,Y,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 5: Public Protected Friend Private Default Dim MustInherit MustOverride Narrowing NotInheritable NotOverridable Overloads Overridable Overrides Partial ReadOnly Shadows Shared Static Widening WithEvents WriteOnly LessThan LineTerminator Colon Namespace Enum Class Structure Module Interface Delegate 
			{n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,Y,Y,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,Y,Y,Y,n,n,Y,Y,n,n,n,n,Y,Y,n,n,n,n,n,n,n,n,Y,Y,Y,n,Y,Y,n,Y,Y,n,Y,n,n,n,n,n,n,n,n,Y,Y,n,n,Y,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 6: Public Protected Friend Private Default Dim MustInherit MustOverride Narrowing NotInheritable NotOverridable Overloads Overridable Overrides Partial ReadOnly Shadows Shared Static Widening WithEvents WriteOnly LessThan LineTerminator Colon Namespace Enum Class Structure Module Interface Delegate 
			{n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,Y,Y,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,Y,Y,Y,n,n,Y,Y,n,n,n,n,Y,Y,n,n,n,n,n,n,n,n,Y,Y,Y,n,Y,Y,n,Y,Y,n,Y,n,n,n,n,n,n,n,n,Y,Y,n,n,Y,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 7: Public Protected Friend Private Default Dim MustInherit MustOverride Narrowing NotInheritable NotOverridable Overloads Overridable Overrides Partial ReadOnly Shadows Shared Static Widening WithEvents WriteOnly LessThan Enum Class Structure Module Interface Delegate 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,Y,Y,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,Y,Y,Y,n,n,n,Y,n,n,n,n,Y,Y,n,n,n,n,n,n,n,n,Y,Y,Y,n,Y,Y,n,Y,Y,n,Y,n,n,n,n,n,n,n,n,Y,Y,n,n,Y,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 8: Enum Class Structure Interface Delegate 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 9: Identifier Aggregate Ascending By Descending Distinct Equals From Group Into Join Order Out Skip Take Where Global 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 10: Object Single Double Decimal Boolean Date Char String Byte SByte UShort Short UInteger Integer ULong Long 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,Y,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,Y,Y,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,Y,Y,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 11: Byte SByte UShort Short UInteger Integer ULong Long 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,Y,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 12: Identifier Aggregate Ascending By Descending Distinct Equals From Group Into Join Order Out Skip Take Where LessThan 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 13: Public Protected Friend Private Default Dim MustInherit MustOverride Narrowing NotInheritable NotOverridable Overloads Overridable Overrides Partial ReadOnly Shadows Shared Static Widening WithEvents WriteOnly 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,Y,n,n,n,Y,n,n,n,n,Y,Y,n,n,n,n,n,n,n,n,Y,Y,Y,n,Y,Y,n,Y,Y,n,Y,n,n,n,n,n,n,n,n,Y,Y,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 14: Identifier Aggregate Ascending By Descending Distinct Equals From Group Into Join Order Out Skip Take Where Enum Class Structure Const Event Custom Sub Function Declare Property Operator Interface Delegate 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,Y,n,n,n,n,n,n,n,n,n,Y,n,n,Y,n,Y,n,n,n,n,n,n,n,n,n,Y,n,n,Y,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 15: LessThan AddHandler RemoveHandler RaiseEvent 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 16: Identifier Aggregate Ascending By Descending Distinct Equals From Group Into Join Order Out Skip Take Where Dim Static Global OpenParenthesis OpenCurlyBrace New XmlLiteral Object Single Double Decimal Boolean Date Char String Byte SByte UShort Short UInteger Integer ULong Long Const AddHandler RemoveHandler RaiseEvent Sub Function CType StringLiteral MyBase Me DecimalIntegerLiteral With SyncLock Mid Call If Select While Do For Throw Try Error On GoTo Resume Exit Continue Stop Return ReDim Erase Using True False HexadecimalIntegerLiteral OctalIntegerLiteral FloatingPointLiteral CharacterLiteral DateLiteral Nothing AddressOf GetTypeKeyword TypeOf GetXmlNamespace MyClass DirectCast TryCast IIf CBool CByte CChar CDate CDec CDbl CInt CLng CObj CSByte CShort CSng CStr CUInt CULng CUShort 
			{n,n,n,n,n,n,n,n,n,n,n,Y,Y,Y,Y,Y,Y,Y,Y,n,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,Y,Y,n,n,n,n,Y,n,Y,n,Y,n,n,Y,Y,Y,Y,Y,Y,Y,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,Y,Y,n,n,n,Y,Y,Y,Y,n,n,n,n,n,n,Y,Y,n,Y,Y,n,Y,n,Y,n,Y,Y,Y,n,Y,n,Y,Y,n,n,n,n,Y,n,n,n,n,n,n,n,n,Y,n,Y,Y,n,n,n,n,Y,Y,n,n,Y,n,n,Y,n,n,Y,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,n,Y,Y,Y,Y,Y,n,n,n,Y,Y,Y,n,Y,Y,n,Y,Y,n,Y,n,Y,Y,Y,Y,Y,Y,n,Y,Y,n,n,n,Y,n,Y,n,n,n,n,n,n,Y,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 17: Identifier Aggregate Ascending By Descending Distinct Equals From Group Into Join Order Out Skip Take Where LessThan ByVal ByRef Optional ParamArray 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,n,n,n,n,n,n,Y,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 18: Identifier Aggregate Ascending By Descending Distinct Equals From Group Into Join Order Out Skip Take Where 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 19: Public Protected Friend Private Default Dim MustInherit MustOverride Narrowing NotInheritable NotOverridable Overloads Overridable Overrides Partial ReadOnly Shadows Shared Static Widening WithEvents WriteOnly LessThan Get Set 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,Y,n,n,n,Y,n,n,n,n,Y,Y,n,n,n,n,n,n,n,n,Y,Y,Y,n,Y,Y,n,Y,Y,n,Y,n,n,n,n,n,n,n,Y,Y,Y,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 20: LessThan GreaterThan Equality Addition Subtraction Multiplication FloatingPointDivision IntegerDivision StringConcatenation Like Mod And Or Xor Exponentiation LeftShift RightShift Inequality GreaterThanOrEqual LessThanOrEqual Not IsTrue IsFalse 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,n,n,Y,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 21: Enum Class Structure Event Sub Function Property Interface Delegate 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,Y,n,n,Y,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 22: ByVal ByRef Optional ParamArray 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 23: Identifier Aggregate Ascending By Descending Distinct Equals From Group Into Join Order Out Skip Take Where Global OpenParenthesis OpenCurlyBrace New XmlLiteral Object Single Double Decimal Boolean Date Char String Byte SByte UShort Short UInteger Integer ULong Long Sub Function Addition Subtraction Not CType StringLiteral MyBase Me DecimalIntegerLiteral Mid True False HexadecimalIntegerLiteral OctalIntegerLiteral FloatingPointLiteral CharacterLiteral DateLiteral Nothing AddressOf GetTypeKeyword TypeOf GetXmlNamespace MyClass DirectCast TryCast IIf CBool CByte CChar CDate CDec CDbl CInt CLng CObj CSByte CShort CSng CStr CUInt CULng CUShort 
			{n,n,n,n,n,n,n,n,n,n,n,Y,Y,Y,Y,Y,Y,Y,Y,n,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,Y,n,n,n,n,Y,n,Y,n,n,n,n,Y,Y,Y,Y,Y,Y,Y,Y,n,Y,Y,n,n,Y,Y,Y,Y,Y,Y,Y,Y,n,Y,Y,n,n,n,n,Y,n,Y,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,Y,n,Y,Y,Y,n,n,n,n,Y,n,n,n,n,Y,n,n,n,n,n,n,n,n,Y,n,Y,Y,n,n,n,n,Y,Y,n,n,Y,n,Y,Y,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,Y,Y,n,n,n,Y,n,Y,n,n,n,n,Y,n,Y,Y,Y,Y,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 24: Identifier Aggregate Ascending By Descending Distinct Equals From Group Into Join Order Out Skip Take Where Global OpenParenthesis OpenCurlyBrace New XmlLiteral Object Single Double Decimal Boolean Date Char String Byte SByte UShort Short UInteger Integer ULong Long Sub Function CType StringLiteral MyBase Me DecimalIntegerLiteral Mid Call True False HexadecimalIntegerLiteral OctalIntegerLiteral FloatingPointLiteral CharacterLiteral DateLiteral Nothing AddressOf GetTypeKeyword TypeOf GetXmlNamespace MyClass DirectCast TryCast IIf CBool CByte CChar CDate CDec CDbl CInt CLng CObj CSByte CShort CSng CStr CUInt CULng CUShort 
			{n,n,n,n,n,n,n,n,n,n,n,Y,Y,Y,Y,Y,Y,Y,Y,n,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,Y,n,n,n,n,Y,n,Y,n,Y,n,n,Y,Y,Y,Y,Y,Y,Y,Y,n,Y,Y,n,n,Y,Y,Y,Y,Y,Y,Y,Y,n,Y,Y,n,n,n,n,Y,n,Y,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,Y,n,Y,Y,Y,n,n,n,n,Y,n,n,n,n,Y,n,n,n,n,n,n,n,n,Y,n,Y,Y,n,n,n,n,Y,Y,n,n,Y,n,n,Y,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,Y,Y,n,n,n,Y,n,Y,n,n,n,n,Y,n,Y,Y,Y,Y,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 25: Equality AdditionAssignment SubtractionAssignment MultiplicationAssignment FloatingPointDivisionAssignment IntegerDivisionAssignment StringConcatenationAssignment ExponentiationAssignment LeftShiftAssignment RightShiftAssignment 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 26: Identifier Aggregate Ascending By Descending Distinct Equals From Group Into Join Order Out Skip Take Where Dim Static Global OpenParenthesis OpenCurlyBrace New XmlLiteral Object Single Double Decimal Boolean Date Char String Byte SByte UShort Short UInteger Integer ULong Long Const AddHandler RemoveHandler RaiseEvent Sub Function CType StringLiteral MyBase Me DecimalIntegerLiteral With SyncLock Mid Call If Select While Do For Throw Try Error On GoTo Resume Exit Continue Stop Return ReDim Erase Using True False HexadecimalIntegerLiteral OctalIntegerLiteral FloatingPointLiteral CharacterLiteral DateLiteral Nothing AddressOf GetTypeKeyword TypeOf GetXmlNamespace MyClass DirectCast TryCast IIf CBool CByte CChar CDate CDec CDbl CInt CLng CObj CSByte CShort CSng CStr CUInt CULng CUShort 
			{n,n,n,n,n,n,n,n,n,n,n,Y,Y,Y,Y,Y,Y,Y,Y,n,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,Y,Y,n,n,n,n,Y,n,Y,n,Y,n,n,Y,Y,Y,Y,Y,Y,Y,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,Y,Y,n,n,n,Y,Y,Y,Y,n,n,n,n,n,n,Y,Y,n,Y,Y,n,Y,n,Y,n,Y,Y,Y,n,Y,n,Y,Y,n,n,n,n,Y,n,n,n,n,n,n,n,n,Y,n,Y,Y,n,n,n,n,Y,Y,n,n,Y,n,n,Y,n,n,Y,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,n,Y,Y,Y,Y,Y,n,n,n,Y,Y,Y,n,Y,Y,n,Y,Y,n,Y,n,Y,Y,Y,Y,Y,Y,n,Y,Y,n,n,n,Y,n,Y,n,n,n,n,n,n,Y,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 27: LessThan GreaterThan Equality Inequality GreaterThanOrEqual LessThanOrEqual Is 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,Y,Y,Y,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 28: CBool CByte CChar CDate CDec CDbl CInt CLng CObj CSByte CShort CSng CStr CUInt CULng CUShort 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,Y,Y,Y,Y,Y,n,Y,n,Y,Y,n,n,Y,Y,Y,Y,n,Y,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 29: Dot OpenParenthesis DotAt TripleDot ExclamationPoint 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,Y,n,n,Y,n,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 30: Identifier Aggregate Ascending By Descending Distinct Equals From Group Into Join Order Out Skip Take Where Global Object Single Double Decimal Boolean Date Char String Byte SByte UShort Short UInteger Integer ULong Long 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,Y,n,n,n,n,n,n,n,n,Y,n,Y,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,Y,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,Y,Y,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,Y,Y,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 31: LessThan GreaterThan Equality Like Inequality GreaterThanOrEqual LessThanOrEqual Is IsNot 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,Y,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,Y,Y,Y,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n},
			// 32: Aggregate Distinct From Group Join Order Skip Take Where Select Let 
			{n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,Y,n,Y,Y,n,Y,Y,n,Y,Y,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,Y,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n,n}
		};

	}
	#endregion

}

