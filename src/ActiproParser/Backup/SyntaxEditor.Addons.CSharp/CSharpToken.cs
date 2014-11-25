using System;

namespace ActiproSoftware.SyntaxEditor.Addons.CSharp {
	
	/// <summary>
	/// Represents a <c>C#</c> <see cref="IToken"/>.
	/// </summary>
	internal class CSharpToken : MergableToken {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>CSharpToken</c> class.
		/// </summary>
		/// <param name="startOffset">The start offset of the token.</param>
		/// <param name="length">The length of the token.</param>
		/// <param name="lexicalParseFlags">The <see cref="LexicalParseFlags"/> for the token.</param>
		/// <param name="parentToken">The <see cref="IToken"/> that starts the current state scope specified by the <see cref="IToken.LexicalState"/> property.</param>
		/// <param name="lexicalParseData">The <see cref="ITokenLexicalParseData"/> that contains lexical parse information about the token.</param>
		public CSharpToken(int startOffset, int length, LexicalParseFlags lexicalParseFlags, IToken parentToken, ITokenLexicalParseData lexicalParseData) : 
			base(startOffset, length, lexicalParseFlags, parentToken, lexicalParseData) {}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Clones the data in the <see cref="IToken"/>.
		/// </summary>
		/// <param name="startOffset">The <see cref="IToken.StartOffset"/> of the cloned object.</param>
		/// <param name="length">The length of the cloned object.</param>
		/// <returns>The <see cref="IToken"/> that was created.</returns>
		public override IToken Clone(int startOffset, int length) {
			return new CSharpToken(startOffset, length, this.LexicalParseFlags, this.ParentToken, this.LexicalParseData);
		}
		
		/// <summary>
		/// Gets whether the token represents a comment.
		/// </summary>
		/// <value>
		/// <c>true</c> if the token represents a comment; otherwise <c>false</c>.
		/// </value>
		public override bool IsComment { 
			get {
				switch (this.ID) {
					case CSharpTokenID.SingleLineComment:
					case CSharpTokenID.MultiLineComment:
						return true;
					default:
						return false;
				}
			}
		}

		/// <summary>
		/// Gets whether the token marks the end of the document.
		/// </summary>
		/// <value>
		/// <c>true</c> if the token marks the end of the document; otherwise <c>false</c>.
		/// </value>
		public override bool IsDocumentEnd { 
			get {
				return (this.ID == CSharpTokenID.DocumentEnd);
			}
		}
		
		/// <summary>
		/// Gets whether the token marks an invalid range of text.
		/// </summary>
		/// <value>
		/// <c>true</c> if the token marks invalid range of text; otherwise <c>false</c>.
		/// </value>
		public override bool IsInvalid { 
			get {
				return (this.ID == CSharpTokenID.Invalid);
			}
		}
		
		/// <summary>
		/// Returns whether the specified token ID is a native type.
		/// </summary>
		/// <param name="tokenID">The ID of the token to examine.</param>
		/// <returns>
		/// <c>true</c> if the specified token ID is a native type; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsNativeType(int tokenID) {
			switch (tokenID) {
				case CSharpTokenID.Bool:
				case CSharpTokenID.Byte:
				case CSharpTokenID.Char:
				case CSharpTokenID.Decimal:
				case CSharpTokenID.Double:
				case CSharpTokenID.Dynamic:
				case CSharpTokenID.Float:
				case CSharpTokenID.Int:
				case CSharpTokenID.Long:
				case CSharpTokenID.Object:
				case CSharpTokenID.SByte:
				case CSharpTokenID.Short:
				case CSharpTokenID.String:
				case CSharpTokenID.UShort:
				case CSharpTokenID.UInt:
				case CSharpTokenID.ULong:
					return true;
				default:
					return false;
			}
		}

		/// <summary>
		/// Gets whether the <see cref="IToken"/> is the end <see cref="IToken"/> of an <see cref="IToken"/> pair.
		/// </summary>
		/// <value>
		/// <c>true</c> if the <see cref="IToken"/> is the end <see cref="IToken"/> of an <see cref="IToken"/> pair; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>
		/// A token pair is generally a pair of brackets.
		/// </remarks>
		public override bool IsPairedEnd {
			get {
				switch (this.ID) {
					case CSharpTokenID.CloseParenthesis:
					case CSharpTokenID.CloseCurlyBrace:
					case CSharpTokenID.CloseSquareBrace:
						return true;
					default:
						return false;
				}
			}
		}

		/// <summary>
		/// Gets whether the <see cref="IToken"/> is the start <see cref="IToken"/> of an <see cref="IToken"/> pair.
		/// </summary>
		/// <value>
		/// <c>true</c> if the <see cref="IToken"/> is the start <see cref="IToken"/> of an <see cref="IToken"/> pair; otherwise, <c>false</c>.
		/// </value>
		/// <remarks>
		/// A token pair is generally a pair of brackets.
		/// </remarks>
		public override bool IsPairedStart {
			get {
				switch (this.ID) {
					case CSharpTokenID.OpenParenthesis:
					case CSharpTokenID.OpenCurlyBrace:
					case CSharpTokenID.OpenSquareBrace:
						return true;
					default:
						return false;
				}
			}
		}

		/// <summary>
		/// Gets whether the token represents whitespace.
		/// </summary>
		/// <value>
		/// <c>true</c> if the token represents whitespace; otherwise <c>false</c>.
		/// </value>
		public override bool IsWhitespace { 
			get {
				switch (this.ID) {
					case CSharpTokenID.Whitespace:
					case CSharpTokenID.LineTerminator:
					case CSharpTokenID.DocumentEnd:
						return true;
					default:
						return false;
				}
			}
		}

		/// <summary>
		/// Gets the key assigned to the token.
		/// </summary>
		/// <value>The key assigned to the token.</value>
		public override string Key { 
			get {
				return CSharpTokenID.GetTokenKey(this.ID);
			}
		}
		
		/// <summary>
		/// Gets the ID of the <see cref="IToken"/> that matches this <see cref="IToken"/> if this token is paired.
		/// </summary>
		/// <value>The ID of the <see cref="IToken"/> that matches this <see cref="IToken"/> if this token is paired.</value>
		public override int MatchingTokenID { 
			get {
				switch (this.ID) {
					case CSharpTokenID.OpenParenthesis:
						return (int)CSharpTokenID.CloseParenthesis;
					case CSharpTokenID.CloseParenthesis:
						return (int)CSharpTokenID.OpenParenthesis;
					case CSharpTokenID.OpenCurlyBrace:
						return (int)CSharpTokenID.CloseCurlyBrace;
					case CSharpTokenID.CloseCurlyBrace:
						return (int)CSharpTokenID.OpenCurlyBrace;
					case CSharpTokenID.OpenSquareBrace:
						return (int)CSharpTokenID.CloseSquareBrace;
					case CSharpTokenID.CloseSquareBrace:
						return (int)CSharpTokenID.OpenSquareBrace;
					default:
						return (int)CSharpTokenID.Invalid;
				}
			}
		}
		
		/// <summary>
		/// Creates and returns a string representation of the current object.
		/// </summary>
		/// <returns>A string representation of the current object.</returns>
		public override string ToString() {
			return this.ToString("C# Token");
		}

	}
}
 