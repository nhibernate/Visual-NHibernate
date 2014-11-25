using System;

namespace ActiproSoftware.SyntaxEditor.Addons.VB {
	
	/// <summary>
	/// Represents a <c>Visual Basic</c> <see cref="IToken"/>.
	/// </summary>
	internal class VBToken : MergableToken {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>VBToken</c> class.
		/// </summary>
		/// <param name="startOffset">The start offset of the token.</param>
		/// <param name="length">The length of the token.</param>
		/// <param name="lexicalParseFlags">The <see cref="LexicalParseFlags"/> for the token.</param>
		/// <param name="parentToken">The <see cref="IToken"/> that starts the current state scope specified by the <see cref="IToken.LexicalState"/> property.</param>
		/// <param name="lexicalParseData">The <see cref="ITokenLexicalParseData"/> that contains lexical parse information about the token.</param>
		public VBToken(int startOffset, int length, LexicalParseFlags lexicalParseFlags, IToken parentToken, ITokenLexicalParseData lexicalParseData) : 
			base(startOffset, length, lexicalParseFlags, parentToken, lexicalParseData) {}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the text that should be used for this <see cref="IToken"/> when performing an auto case correction operation on it.
		/// </summary>
		/// <value>The text that should be used for this <see cref="IToken"/> when performing an auto case correction operation on it.</value>
		/// <remarks>
		/// Return a <see langword="null"/> value to indicate that there is no auto case correction text available.
		/// </remarks>
		public override string AutoCaseCorrectText { 
			get {
				if ((this.ID > VBTokenID.KeywordStart) && (this.ID < VBTokenID.KeywordEnd)) {
					string text = this.Key;
					if (text.EndsWith("Keyword"))
						text = text.Substring(0, text.Length - 7);
					return text;
				}
				else
					return null;
			}
		}
			
		/// <summary>
		/// Clones the data in the <see cref="IToken"/>.
		/// </summary>
		/// <param name="startOffset">The <see cref="IToken.StartOffset"/> of the cloned object.</param>
		/// <param name="length">The length of the cloned object.</param>
		/// <returns>The <see cref="IToken"/> that was created.</returns>
		public override IToken Clone(int startOffset, int length) {
			return new VBToken(startOffset, length, this.LexicalParseFlags, this.ParentToken, this.LexicalParseData);
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
					case VBTokenID.SingleLineComment:
					case VBTokenID.RemComment:
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
				return (this.ID == VBTokenID.DocumentEnd);
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
				return (this.ID == VBTokenID.Invalid);
			}
		}
		
		/// <summary>
		/// Returns whether the specified token ID is a keyword
		/// </summary>
		/// <param name="tokenID">The ID of the token to examine.</param>
		/// <returns>
		/// <c>true</c> if the specified token ID is a keyword; otherwise <c>false</c>.
		/// </returns>
		public static bool IsKeyword(int tokenID) { 
			return (tokenID > VBTokenID.KeywordStart) && (tokenID < VBTokenID.KeywordEnd);
		}
		
		/// <summary>
		/// Returns whether the specified token ID is a modifier
		/// </summary>
		/// <param name="tokenID">The ID of the token to examine.</param>
		/// <returns>
		/// <c>true</c> if the specified token ID is a modifier; otherwise <c>false</c>.
		/// </returns>
		public static bool IsModifier(int tokenID) { 
			switch (tokenID) {
				case VBTokenID.Public:
				case VBTokenID.Protected:
				case VBTokenID.Friend:
				case VBTokenID.Private:
				case VBTokenID.Default:
				case VBTokenID.Dim:
				case VBTokenID.MustInherit:
				case VBTokenID.MustOverride:
				case VBTokenID.Narrowing:
				case VBTokenID.NotInheritable:
				case VBTokenID.NotOverridable:
				case VBTokenID.Overloads:
				case VBTokenID.Overridable:
				case VBTokenID.Overrides:
				case VBTokenID.Partial:
				case VBTokenID.ReadOnly:
				case VBTokenID.Shadows:
				case VBTokenID.Shared:
				case VBTokenID.Static:
				case VBTokenID.Widening:
				case VBTokenID.WithEvents:
				case VBTokenID.WriteOnly:
					return true;
				default:
					return false;
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
				case VBTokenID.Boolean:
				case VBTokenID.Byte:
				case VBTokenID.Char:
				case VBTokenID.Date:
				case VBTokenID.Decimal:
				case VBTokenID.Double:
				case VBTokenID.Integer:
				case VBTokenID.Long:
				case VBTokenID.Object:
				case VBTokenID.SByte:
				case VBTokenID.Short:
				case VBTokenID.Single:
				case VBTokenID.String:
				case VBTokenID.UInteger:
				case VBTokenID.ULong:
				case VBTokenID.UShort:
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
					case VBTokenID.CloseParenthesis:
					case VBTokenID.CloseCurlyBrace:
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
					case VBTokenID.OpenParenthesis:
					case VBTokenID.OpenCurlyBrace:
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
					case VBTokenID.Whitespace:
					case VBTokenID.LineTerminator:
					case VBTokenID.DocumentEnd:
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
				return VBTokenID.GetTokenKey(this.ID);
			}
		}
		
		/// <summary>
		/// Gets the ID of the <see cref="IToken"/> that matches this <see cref="IToken"/> if this token is paired.
		/// </summary>
		/// <value>The ID of the <see cref="IToken"/> that matches this <see cref="IToken"/> if this token is paired.</value>
		public override int MatchingTokenID { 
			get {
				switch (this.ID) {
					case VBTokenID.OpenParenthesis:
						return (int)VBTokenID.CloseParenthesis;
					case VBTokenID.CloseParenthesis:
						return (int)VBTokenID.OpenParenthesis;
					case VBTokenID.OpenCurlyBrace:
						return (int)VBTokenID.CloseCurlyBrace;
					case VBTokenID.CloseCurlyBrace:
						return (int)VBTokenID.OpenCurlyBrace;
					default:
						return (int)VBTokenID.Invalid;
				}
			}
		}
		
		/// <summary>
		/// Creates and returns a string representation of the current object.
		/// </summary>
		/// <returns>A string representation of the current object.</returns>
		public override string ToString() {
			return this.ToString("VB Token");
		}

	}
}
 