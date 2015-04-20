using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using ActiproSoftware.SyntaxEditor.ParserGenerator;

namespace ActiproSoftware.SyntaxEditor.Addons.CSharp {

	/// <summary>
	/// Represents a <c>C#</c> lexical parser implementation.
	/// </summary>
	internal class CSharpLexicalParser : IMergableLexicalParser {

		private Hashtable				keywords				= new Hashtable();
		
		// NOTE: These additional rules have not been implemented.
		// - "alias" is a keyword but only after an extern.
		// - "global" is a keyword but only before a "::".
		// - "where" is a keyword but only after a generic type declaration.
		// - "value" is a keyword but only in a "set" accessor.
		// - New query expression keywords shouldn't really be lexically parsed as keyword unless they are in a query expression
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>CSharpLexicalParser</c> class.
		/// </summary>
		public CSharpLexicalParser() {
			// Initialize keywords
			for (int index = (int)CSharpTokenID.ContextualKeywordStart + 1; index < (int)CSharpTokenID.ContextualKeywordEnd; index++)
				keywords.Add(CSharpTokenID.GetTokenKey(index).ToLowerInvariant(), index);
			for (int index = (int)CSharpTokenID.KeywordStart + 1; index < (int)CSharpTokenID.KeywordEnd; index++)
				keywords.Add(CSharpTokenID.GetTokenKey(index).ToLowerInvariant(), index);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Looks backward (skipping whitespace) to get the previous word.
		/// </summary>
		/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
		/// <returns>The previous word.</returns>
		/// <remarks>
		/// This method only looks at letters for sequential runs and will not scan words longer than 10 characters.
		/// </remarks>
		private string GetPreviousText(ITextBufferReader reader) {
			int offset = reader.Offset;
			
			char ch;
			string result = String.Empty;
			while ((!reader.IsAtStart) && (result.Length <= 10)) {
				ch = reader.ReadReverse();
				if ((Char.IsWhiteSpace(ch)) && (ch != '\n')) {
					// Break out of the loop if we already have a result
					if (result.Length > 0)
						break;
				}
				else if (Char.IsLetter(ch)) {
					// Append the letter
					result = ch + result;
				}
				else {
					// Set the result to the character if there is no result yet
					if (result.Length == 0)
						result = ch.ToString();
					break;
				}
			}

			// Return to the original offset
			while (reader.Offset < offset)
				reader.Read();

			return result;
		}

		/// <summary>
		/// Looks forward (skipping whitespace) to see if the next text matches the specified text.
		/// </summary>
		/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
		/// <param name="nextText">The next text to match.</param>
		/// <returns>
		/// <c>true</c> if the next text matches the specified text; otherwise, <c>false</c>.
		/// </returns>
		private bool IsNextText(ITextBufferReader reader, string nextText) {
			int offset = reader.Offset;
			
			char ch;
			bool result = false;
			int nextTextIndex = 0;
			while (!reader.IsAtEnd) {
				ch = reader.Read();
				if (ch == nextText[nextTextIndex]) {
					nextTextIndex++;
					while ((nextTextIndex < nextText.Length) && (reader.Read() == nextText[nextTextIndex])) {
						nextTextIndex++;
					}
					if (nextTextIndex == nextText.Length) {
						// Flag that a match was found
						result = true;
					}
				}
				else if (Char.IsWhiteSpace(ch)) {
					// Continue if whitespace
					continue;
				}

				// Break out of the loop
				break;
			}

			// Back up to the original offset
			while (reader.Offset > offset)
				reader.ReadReverse();

			return result;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Returns a single-character <see cref="ITokenLexicalParseData"/> representing the lexical parse data for the
		/// default token in the <see cref="ILexicalState"/> and seeks forward one position in the <see cref="ITextBufferReader"/>
		/// </summary>
		/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
		/// <param name="lexicalState">The <see cref="ILexicalState"/> that specifies the current context.</param>
		/// <returns>The <see cref="ITokenLexicalParseData"/> for default text in the <see cref="ILexicalState"/>.</returns>
		public ITokenLexicalParseData GetLexicalStateDefaultTokenLexicalParseData(ITextBufferReader reader, ILexicalState lexicalState) {
			reader.Read();
			return new LexicalStateAndIDTokenLexicalParseData(lexicalState, (byte)lexicalState.DefaultTokenID);
		}

		/// <summary>
		/// Performs a lexical parse to return the next <see cref="ITokenLexicalParseData"/> 
		/// from a <see cref="ITextBufferReader"/> and seeks past it if there is a match.
		/// </summary>
		/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
		/// <param name="lexicalState">The <see cref="ILexicalState"/> that specifies the current context.</param>
		/// <param name="lexicalParseData">Returns the next <see cref="ITokenLexicalParseData"/> from a <see cref="ITextBufferReader"/>.</param>
		/// <returns>A <see cref="MatchType"/> indicating the type of match that was made.</returns>
		public MatchType GetNextTokenLexicalParseData(ITextBufferReader reader, ILexicalState lexicalState, ref ITokenLexicalParseData lexicalParseData) {
			// Initialize
			int tokenID = CSharpTokenID.Invalid;

			// Get the next character
			char ch = reader.Read();

			switch (lexicalState.ID) {
				case CSharpLexicalStateID.DocumentationComment:
					switch (ch) {
						case '\n':
							// Ignore and let exit scope handle
							break;
						case '<':
							// Parse the XML tag
							while ((!reader.IsAtEnd) && (reader.Peek() != '\n')) {
								if (reader.Read() == '>')
									break;
							}
							tokenID = CSharpTokenID.DocumentationCommentTag;
							break;
						default:
							// Parse the comment text
							while ((!reader.IsAtEnd) && (reader.Peek() != '<') && (reader.Peek() != '\n'))
								reader.Read();
							tokenID = CSharpTokenID.DocumentationCommentText;
							break;
					}
					break;
				case CSharpLexicalStateID.PreProcessorDirective:
					switch (ch) {
						case '\n':
							// Ignore and let exit scope handle
							break;
						default:
							if ((ch == '/') && (!reader.IsAtEnd) && (reader.Peek() == '/')) {
								// Parse a single-line comment
								tokenID = this.ParseSingleLineComment(reader);
								break;
							}

							// Parse the directive text
							while ((!reader.IsAtEnd) && (reader.Peek() != '\n')) {
								reader.Read();
								if ((!reader.IsAtEnd) && (reader.Peek() == '/') && (reader.PeekReverse() == '/')) {
									reader.ReadReverse();
									break;
								}
							}
							tokenID = CSharpTokenID.PreProcessorDirectiveText;
							break;
					}
					break;
				case CSharpLexicalStateID.Default:
					// If the character is a letter or digit...
					if ((Char.IsLetter(ch) || (ch == '_'))) {
						// Parse the identifier
						tokenID = this.ParseIdentifier(reader, ch);
					}
					else if ((ch != '\n') && (Char.IsWhiteSpace(ch))) {
						while ((reader.Peek() != '\n') && (Char.IsWhiteSpace(reader.Peek()))) 
							reader.Read();
						tokenID = CSharpTokenID.Whitespace;
					}
					else {
						tokenID = CSharpTokenID.Invalid;
						switch (ch) {
							case '.': {
								char ch2 = reader.Peek();
								if ((ch2 >= '0') && (ch2 <= '9')) {
									// Parse the number
									tokenID = this.ParseNumber(reader, ch);
								}
								else
									tokenID = CSharpTokenID.Dot;
								break;
							}
							case ',':
								tokenID = CSharpTokenID.Comma;
								break;
							case '(':
								tokenID = CSharpTokenID.OpenParenthesis;
								break;
							case ')':
								tokenID = CSharpTokenID.CloseParenthesis;
								break;
							case ';':
								tokenID = CSharpTokenID.SemiColon;
								break;
							case '\n':
								// Line terminator
								tokenID = CSharpTokenID.LineTerminator;
								break;
							case '{':
								tokenID = CSharpTokenID.OpenCurlyBrace;
								break;
							case '}':
								tokenID = CSharpTokenID.CloseCurlyBrace;
								break;
							case '[':
								tokenID = CSharpTokenID.OpenSquareBrace;
								break;
							case ']':
								tokenID = CSharpTokenID.CloseSquareBrace;
								break;
							case '/':						
								tokenID = CSharpTokenID.Division;
								switch (reader.Peek()) {
									case '/':
										// Parse a single-line comment
										tokenID = this.ParseSingleLineComment(reader);
										break;
									case '*':
										// Parse a multi-line comment
										tokenID = this.ParseMultiLineComment(reader);
										break;
									case '=':
										reader.Read();
										tokenID = CSharpTokenID.DivisionAssignment;
										break;
								}
								break;
							case '\"':
								// Parse a string literal
								tokenID = this.ParseStringLiteral(reader);
								break;
							case '\'': 
								// Parse a character literal
								tokenID = this.ParseCharacterLiteral(reader);
								break;
							case '=':
								tokenID = CSharpTokenID.Assignment;
								switch (reader.Peek()) {
									case '=':
										reader.Read();
										tokenID = CSharpTokenID.Equality;
										break;
									case '>':
										reader.Read();
										tokenID = CSharpTokenID.Lambda;
										break;
								}
								break;
							case '!':
								if (reader.Peek() == '=') {
									reader.Read();
									tokenID = CSharpTokenID.Inequality;
								}
								else
									tokenID = CSharpTokenID.Negation;
								break;
							case ':':
								if (reader.Peek() == ':') {
									reader.Read();
									tokenID = CSharpTokenID.NamespaceAliasQualifier;
								}
								else
									tokenID = CSharpTokenID.Colon;
								break;
							case '+':
								tokenID = CSharpTokenID.Addition;
								switch (reader.Peek()) {
									case '+':
										reader.Read();
										tokenID = CSharpTokenID.Increment;
										break;
									case '=':
										reader.Read();
										tokenID = CSharpTokenID.AdditionAssignment;
										break;
								}
								break;
							case '-':
								tokenID = CSharpTokenID.Subtraction;
								switch (reader.Peek()) {
									case '-':
										reader.Read();
										tokenID = CSharpTokenID.Decrement;
										break;
									case '=':
										reader.Read();
										tokenID = CSharpTokenID.SubtractionAssignment;
										break;
									case '>':
										reader.Read();
										tokenID = CSharpTokenID.PointerDereference;
										break;
								}
								break;
							case '<':
								tokenID = CSharpTokenID.LessThan;
								switch (reader.Peek()) {
									case '=':
										reader.Read();
										tokenID = CSharpTokenID.LessThanOrEqual;
										break;
									case '<':
										reader.Read();
										if (reader.Peek() == '=') {
											reader.Read();
											tokenID = CSharpTokenID.LeftShiftAssignment;
										}
										else
											tokenID = CSharpTokenID.LeftShift;
										break;
								}
								break;
							case '>':
								tokenID = CSharpTokenID.GreaterThan;
								switch (reader.Peek()) {
									case '=':
										reader.Read();
										tokenID = CSharpTokenID.GreaterThanOrEqual;
										break;
								}
								break;
							case '*':
								if (reader.Peek() == '=') {
									reader.Read();
									tokenID = CSharpTokenID.MultiplicationAssignment;
								}
								else
									tokenID = CSharpTokenID.Multiplication;
								break;
							case '%':
								if (reader.Peek() == '=') {
									reader.Read();
									tokenID = CSharpTokenID.ModulusAssignment;
								}
								else
									tokenID = CSharpTokenID.Modulus;
								break;
							case '&':
								tokenID = CSharpTokenID.BitwiseAnd;
								switch (reader.Peek()) {
									case '&':
										reader.Read();
										tokenID = CSharpTokenID.ConditionalAnd;
										break;
									case '=':
										reader.Read();
										tokenID = CSharpTokenID.BitwiseAndAssignment;
										break;
								}
								break;
							case '|':
								tokenID = CSharpTokenID.BitwiseOr;
								switch (reader.Peek()) {
									case '|':
										reader.Read();
										tokenID = CSharpTokenID.ConditionalOr;
										break;
									case '=':
										reader.Read();
										tokenID = CSharpTokenID.BitwiseOrAssignment;
										break;
								}
								break;
							case '^':
								if (reader.Peek() == '=') {
									reader.Read();
									tokenID = CSharpTokenID.ExclusiveOrAssignment;
								}
								else
									tokenID = CSharpTokenID.ExclusiveOr;
								break;
							case '?':
								if (reader.Peek() == '?') {
									reader.Read();
									tokenID = CSharpTokenID.NullCoalescing;
								}
								else
									tokenID = CSharpTokenID.QuestionMark;
								break;
							case '~':
								tokenID = CSharpTokenID.OnesComplement;
								break;
							case '@':
								if (reader.Peek() == '\"') {
									// Parse a verbatim string
									reader.Read();
									tokenID = this.ParseVerbatimStringLiteral(reader);
								}
								else if ((Char.IsLetter(reader.Peek())) || (reader.Peek() == '_')) {
									// Parse the identifier
									tokenID = this.ParseIdentifier(reader, ch);
								}
								break;
							default:
								if ((ch >= '0') && (ch <= '9')) {
									// Parse the number
									tokenID = this.ParseNumber(reader, ch);
								}
								break;
						}
					}
					break;
			}

			if (tokenID != CSharpTokenID.Invalid) {
				lexicalParseData = new LexicalStateAndIDTokenLexicalParseData(lexicalState, (byte)tokenID);
				return MatchType.ExactMatch;
			}
			else {
				reader.ReadReverse();
				return MatchType.NoMatch;
			}
		}

		/// <summary>
		/// Represents the method that will handle <see cref="ITokenLexicalParseData"/> matching callbacks.
		/// </summary>
		/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
		/// <param name="lexicalScope">The <see cref="ILexicalScope"/> that specifies the lexical scope to check.</param>
		/// <param name="lexicalParseData">Returns the <see cref="ITokenLexicalParseData"/> that was parsed, if any.</param>
		/// <returns>A <see cref="MatchType"/> indicating the type of match that was made.</returns>
		public MatchType IsDocumentationCommentStateScopeEnd(ITextBufferReader reader, ILexicalScope lexicalScope, ref ITokenLexicalParseData lexicalParseData) {
			if (reader.Peek() == '\n') {
				reader.Read();
				lexicalParseData = new LexicalScopeAndIDTokenLexicalParseData(lexicalScope, CSharpTokenID.LineTerminator);
				return MatchType.ExactMatch;
			}
			return MatchType.NoMatch;
		}

		/// <summary>
		/// Represents the method that will handle <see cref="ITokenLexicalParseData"/> matching callbacks.
		/// </summary>
		/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
		/// <param name="lexicalScope">The <see cref="ILexicalScope"/> that specifies the lexical scope to check.</param>
		/// <param name="lexicalParseData">Returns the <see cref="ITokenLexicalParseData"/> that was parsed, if any.</param>
		/// <returns>A <see cref="MatchType"/> indicating the type of match that was made.</returns>
		public MatchType IsDocumentationCommentStateScopeStart(ITextBufferReader reader, ILexicalScope lexicalScope, ref ITokenLexicalParseData lexicalParseData) {
			if (reader.Peek() == '/') {
				reader.Read();
				if (reader.Peek() == '/') {
					reader.Read();
					if (reader.Peek() == '/') {
						reader.Read();
						lexicalParseData = new LexicalScopeAndIDTokenLexicalParseData(lexicalScope, CSharpTokenID.DocumentationCommentDelimiter);
						return MatchType.ExactMatch;
					}
					reader.ReadReverse();
				}
				reader.ReadReverse();
			}
			return MatchType.NoMatch;
		}
					
		/// <summary>
		/// Represents the method that will handle <see cref="ITokenLexicalParseData"/> matching callbacks.
		/// </summary>
		/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
		/// <param name="lexicalScope">The <see cref="ILexicalScope"/> that specifies the lexical scope to check.</param>
		/// <param name="lexicalParseData">Returns the <see cref="ITokenLexicalParseData"/> that was parsed, if any.</param>
		/// <returns>A <see cref="MatchType"/> indicating the type of match that was made.</returns>
		public MatchType IsPreProcessorDirectiveStateScopeEnd(ITextBufferReader reader, ILexicalScope lexicalScope, ref ITokenLexicalParseData lexicalParseData) {
			if (reader.Peek() == '\n') {
				reader.Read();
				lexicalParseData = new LexicalScopeAndIDTokenLexicalParseData(lexicalScope, CSharpTokenID.LineTerminator);
				return MatchType.ExactMatch;
			}
			return MatchType.NoMatch;
		}

		/// <summary>
		/// Represents the method that will handle <see cref="ITokenLexicalParseData"/> matching callbacks.
		/// </summary>
		/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
		/// <param name="lexicalScope">The <see cref="ILexicalScope"/> that specifies the lexical scope to check.</param>
		/// <param name="lexicalParseData">Returns the <see cref="ITokenLexicalParseData"/> that was parsed, if any.</param>
		/// <returns>A <see cref="MatchType"/> indicating the type of match that was made.</returns>
		public MatchType IsPreProcessorDirectiveStateScopeStart(ITextBufferReader reader, ILexicalScope lexicalScope, ref ITokenLexicalParseData lexicalParseData) {
			if ((reader.Peek() == '#') && (reader.IsWhitespaceOnlyBeforeOnLine)) {
				// Consume the #
				reader.Read();
				
				// Read directive
				int startOffset = reader.Offset;
				while ((!reader.IsAtEnd) && (Char.IsLetter(reader.Peek())))
					reader.Read();

				int tokenID = CSharpTokenID.Invalid;
				if (reader.Offset != startOffset) {
					switch (reader.GetSubstring(startOffset, reader.Offset - startOffset)) {
						case "define":
							tokenID = CSharpTokenID.DefinePreProcessorDirective;
							break;
						case "elif":
							tokenID = CSharpTokenID.ElIfPreProcessorDirective;
							break;
						case "else":
							tokenID = CSharpTokenID.ElsePreProcessorDirective;
							break;
						case "endif":
							tokenID = CSharpTokenID.EndIfPreProcessorDirective;
							break;
						case "endregion":
							tokenID = CSharpTokenID.EndRegionPreProcessorDirective;
							break;
						case "error":
							tokenID = CSharpTokenID.ErrorPreProcessorDirective;
							break;
						case "if":
							tokenID = CSharpTokenID.IfPreProcessorDirective;
							break;
						case "line":
							tokenID = CSharpTokenID.LinePreProcessorDirective;
							break;
						case "pragma":
							tokenID = CSharpTokenID.PragmaPreProcessorDirective;
							break;
						case "region":
							tokenID = CSharpTokenID.RegionPreProcessorDirective;
							break;
						case "undef":
							tokenID = CSharpTokenID.UndefPreProcessorDirective;
							break;
						case "warning":
							tokenID = CSharpTokenID.WarningPreProcessorDirective;
							break;
					}
				}

				// Back up if it is an invalid directive
				if (tokenID == CSharpTokenID.Invalid) {
					while (reader.Offset >= startOffset)
						reader.ReadReverse();
				}
				else {
					lexicalParseData = new LexicalScopeAndIDTokenLexicalParseData(lexicalScope, (byte)tokenID);
					return MatchType.ExactMatch;
				}
			}
			return MatchType.NoMatch;
		}
		
		/// <summary>
		/// Parses a character literal.
		/// </summary>
		/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
		/// <returns>The ID of the token that was matched.</returns>
		protected virtual int ParseCharacterLiteral(ITextBufferReader reader) {
			bool exitLoop = false;
			while (reader.Offset < reader.Length) {
				switch (reader.Read()) {
					case '\\':
						// If escaping, skip over the next character
						if (reader.Peek() != '\n')
							reader.Read();
						break;
					case '\'':
						exitLoop = true;
						break;
					case '\n':
						reader.ReadReverse();
						exitLoop = true;
						break;
				}
				if (exitLoop)
					break;
			}
			return CSharpTokenID.CharacterLiteral;
		}

		/// <summary>
		/// Parses an identifier.
		/// </summary>
		/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
		/// <param name="ch">The first character of the identifier.</param>
		/// <returns>The ID of the token that was matched.</returns>
		protected virtual int ParseIdentifier(ITextBufferReader reader, char ch) {
			// Get the entire word
			int startOffset = reader.Offset - 1;
			while (!reader.IsAtEnd) {
				char ch2 = reader.Read();
				if ((!char.IsLetterOrDigit(ch2)) && (ch2 != '_')) {
					if ((ch2 == '\\') && (Char.ToLowerInvariant(reader.Peek()) == 'u')) {
						// \u or \U escape sequence
						ch2 = reader.Read();
						int charactersRequired = (ch2 == 'U' ? 8 : 4);
						string escapeSequence = String.Empty;
						while ((charactersRequired > 0) && ("0123456789AaBbCcDdEeFf".IndexOf(reader.Peek()) != -1)) {
							charactersRequired--;
							escapeSequence += reader.Read();
						}
						continue;
					}
					else if (ch2 > '\u00FF') {
						// 6/28/2009 - Check for Nl, Mn, Mc, Pc, and Cf 
						switch (char.GetUnicodeCategory(ch2)) {
							case UnicodeCategory.LetterNumber:
							case UnicodeCategory.NonSpacingMark:
							case UnicodeCategory.SpacingCombiningMark:
							case UnicodeCategory.ConnectorPunctuation:
							case UnicodeCategory.Format:
								// These Unicode categories are allowed as identifiers
								continue;
						}
					}
					reader.ReadReverse();
					break;
				}
			}

			// Determine if the word is a keyword
			if (((ch >= 'a') && (ch <= 'z'))) {
				object value = keywords[reader.GetSubstring(startOffset, reader.Offset - startOffset)];
				if (value != null) {
					// A keyword was matched
					switch ((int)value) {
						case CSharpTokenID.Add:
						case CSharpTokenID.Remove:
							// These keywords require that a { follows
							if (this.IsNextText(reader, "{")) {
								reader.Push();
								try {
									reader.Offset = startOffset;
									switch (this.GetPreviousText(reader)) {
										case "\n":  // \n isn't really a character that should allow these keywords but it could be there in case of a comment line before the keyword
										case "{":
										case "}":
										case "]":
											return (int)value;
									}
								}
								finally {
									reader.Pop();
								}
							}
							break;
						case CSharpTokenID.Get:
						case CSharpTokenID.Set:
							// These keywords require that a { or ; follows
							if ((this.IsNextText(reader, "{")) || (this.IsNextText(reader, ";"))) {
								reader.Push();
								try {
									reader.Offset = startOffset;
									switch (this.GetPreviousText(reader)) {
										case "\n":  // \n isn't really a character that should allow these keywords but it could be there in case of a comment line before the keyword
										case ";":
										case "{":
										case "}":
										case "]":
										case "protected":
										case "internal":
										case "private":
											return (int)value;
									}
								}
								finally {
									reader.Pop();
								}
							}
							break;
						/* 
						case CSharpTokenID.Global:
							// This keyword requires that a "::" follows
							if (this.IsNextText(reader, "::"))
								return (int)value;
							break;
						*/
						case CSharpTokenID.Yield:
							// This keyword requires that a "break" or "return" follows
							if ((this.IsNextText(reader, "break")) || (this.IsNextText(reader, "return")))
								return (int)value;
							break;
						default:
							return (int)value;
					}
				}
			}
			
			return CSharpTokenID.Identifier;
		}

		/// <summary>
		/// Parses a multiple line comment.
		/// </summary>
		/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
		/// <returns>The ID of the token that was matched.</returns>
		protected virtual int ParseMultiLineComment(ITextBufferReader reader) {
			reader.Read();
			while (reader.Offset < reader.Length) {
				if (reader.Peek() == '*') {
					if (reader.Offset + 1 < reader.Length) {
						if (reader.Peek(2) == '/') {
							reader.Read();
							reader.Read();
							break;
						}
					}
					else {
						reader.Read();
						break;
					}
				}
				reader.Read();
			}
			return CSharpTokenID.MultiLineComment;
		}
		
		/// <summary>
		/// Parses a number.
		/// </summary>
		/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
		/// <param name="ch">The first character of the number.</param>
		/// <returns>The ID of the token that was matched.</returns>
		protected virtual int ParseNumber(ITextBufferReader reader, char ch) {
			int tokenID = CSharpTokenID.DecimalIntegerLiteral;

			if ((ch == '0') && (Char.ToLowerInvariant(reader.Peek()) == 'x')) {
				// Read the digits of the hex number
				reader.Read();
				while ("0123456789AaBbCcDdEeFf".IndexOf(reader.Peek()) != -1)
					reader.Read();
				tokenID = CSharpTokenID.HexadecimalIntegerLiteral;
			}
			else {
				if (ch != '.') {
					// Get the first set of digits
					while (Char.IsNumber(reader.Peek()))
						reader.Read();
				}
				if ((ch == '.') || (reader.Peek() == '.')) {
					// Flag as a real number
					tokenID = CSharpTokenID.RealLiteral;

					// Get the dot
					if (ch != '.')
						reader.Read();

					if (Char.IsNumber(reader.Peek())) {
						// Get the next set of digits
						while (Char.IsNumber(reader.Peek()))
							reader.Read();
					}
					else {
						// Don't consume the . since there is no number next, and flip back to an integer
						reader.ReadReverse();
						tokenID = CSharpTokenID.DecimalIntegerLiteral;
					}
				}
				if (Char.ToLowerInvariant(reader.Peek()) == 'e') {
					// Flag as a real number
					tokenID = CSharpTokenID.RealLiteral;

					// Get the exponent
					reader.Read();
					if ("+-".IndexOf(reader.Peek()) != -1)
						reader.Read();

					// Get the next set of digits
					while (Char.IsNumber(reader.Peek()))
						reader.Read();
				}
				if ("FfDdMm".IndexOf(reader.Peek()) != -1) {
					// Flag as a real number
					tokenID = CSharpTokenID.RealLiteral;

					// Get the real type suffix
					reader.Read();
				}
			}

			switch (tokenID) {
				case CSharpTokenID.DecimalIntegerLiteral:
				case CSharpTokenID.HexadecimalIntegerLiteral:
					// Parse integer type suffix
					if (Char.ToLowerInvariant(reader.Peek()) == 'u') {
						reader.Read();
						if (Char.ToLowerInvariant(reader.Peek()) == 'l')
							reader.Read();
					}
					else if (Char.ToLowerInvariant(reader.Peek()) == 'l') {
						reader.Read();
						if (Char.ToLowerInvariant(reader.Peek()) == 'u')
							reader.Read();
					}
					break;
			}

			return tokenID;
		}

		/// <summary>
		/// Parses a single line comment.
		/// </summary>
		/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
		/// <returns>The ID of the token that was matched.</returns>
		protected virtual int ParseSingleLineComment(ITextBufferReader reader) {
			reader.Read();
			while ((!reader.IsAtEnd) && (reader.Peek() != '\n'))
				reader.Read();
			return CSharpTokenID.SingleLineComment;
		}
		
		/// <summary>
		/// Parses a string literal.
		/// </summary>
		/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
		/// <returns>The ID of the token that was matched.</returns>
		protected virtual int ParseStringLiteral(ITextBufferReader reader) {
			bool exitLoop = false;
			while (reader.Offset < reader.Length) {
				switch (reader.Read()) {
					case '\\':
						// If escaping, skip over the next character
						if (reader.Peek() != '\n')
							reader.Read();
						break;
					case '\"':
						exitLoop = true;
						break;
					case '\n':
						reader.ReadReverse();
						exitLoop = true;
						break;
				}
				if (exitLoop)
					break;
			}
			return CSharpTokenID.StringLiteral;
		}
		
		/// <summary>
		/// Parses a verbatim string literal.
		/// </summary>
		/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
		/// <returns>The ID of the token that was matched.</returns>
		protected virtual int ParseVerbatimStringLiteral(ITextBufferReader reader) {
			while (reader.Offset < reader.Length) {
				if (reader.Read() == '\"') {
					if (reader.Peek() == '\"')
						reader.Read();
					else
						break;
				}
			}
			return CSharpTokenID.VerbatimStringLiteral;
		}


	}
}
