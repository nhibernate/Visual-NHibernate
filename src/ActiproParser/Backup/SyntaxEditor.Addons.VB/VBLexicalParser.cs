using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using ActiproSoftware.SyntaxEditor.ParserGenerator;

namespace ActiproSoftware.SyntaxEditor.Addons.VB {

	/// <summary>
	/// Represents a <c>Visual Basic</c> lexical parser implementation.
	/// </summary>
	internal class VBLexicalParser : IMergableLexicalParser {

		private Hashtable				keywords		= new Hashtable();
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>VBLexicalParser</c> class.
		/// </summary>
		public VBLexicalParser() {
			// Initialize keywords
			for (int index = (int)VBTokenID.ContextualKeywordStart + 1; index < (int)VBTokenID.ContextualKeywordEnd; index++)
				keywords.Add(VBTokenID.GetTokenKey(index).ToLowerInvariant(), index);
			for (int index = (int)VBTokenID.KeywordStart + 1; index < (int)VBTokenID.KeywordEnd; index++) {
				string keyword = VBTokenID.GetTokenKey(index).ToLowerInvariant();
				if (keyword.EndsWith("keyword"))
					keyword = keyword.Substring(0, keyword.Length - 7);
				keywords.Add(keyword, index);
			}
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Consumes the XML comment or CDATA section.
		/// </summary>
		/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
		private void ConsumeXmlCommentOrCData(ITextBufferReader reader) {
			char ch;
			reader.Read();
			if (this.IsNextText(reader, "--", false)) {
				// Comment
				reader.Read();
				reader.Read();
				int hyphenLevel = 0;
				while (!reader.IsAtEnd) {
					ch = reader.Read();

					if (ch == '-') {
						hyphenLevel = Math.Min(2, hyphenLevel + 1);
						continue;
					}
					else if ((ch == '>') && (hyphenLevel == 2)) {
						// Exit the loop
						break;
					}

					hyphenLevel = 0;
				}
			}
			else if (this.IsNextText(reader, "[cdata[", false)) {
				// CDATA
				int braceLevel = 0;
				while (!reader.IsAtEnd) {
					ch = reader.Read();

					if (ch == ']') {
						braceLevel = Math.Min(2, braceLevel + 1);
						continue;
					}
					else if ((ch == '>') && (braceLevel == 2)) {
						// Exit the loop
						break;
					}

					braceLevel = 0;
				}
			}
			else {
				// Other invalid tag... scan to the next >
				this.ConsumeXmlTag(reader);
			}
		}
		
		/// <summary>
		/// Consumes the XML embedded expression.
		/// </summary>
		/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
		private void ConsumeXmlEmbeddedExpression(ITextBufferReader reader) {
			char ch;
			reader.Read();
			bool lastWasPercentage = false;
			int count = 1;
			while (!reader.IsAtEnd) {
				ch = reader.Read();

				if (ch == '%') {
					lastWasPercentage = true;
					continue;
				}
				else if ((ch == '>') && (lastWasPercentage)) {
					if (--count <= 0) {
						// Exit the loop
						break;
					}
				}
				else if (ch == '<') {
					if ((!reader.IsAtEnd) && (reader.Read() == '%')) {
						// Increment nesting count
						count++;
					}
				}

				lastWasPercentage = false;
			}
		}
		
		/// <summary>
		/// Consumes the XML processing instruction.
		/// </summary>
		/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
		/// <returns>
		/// <c>true</c> if the PI is an XML declaration; otherwise, <c>false</c>.
		/// </returns>
		private bool ConsumeXmlProcessingInstruction(ITextBufferReader reader) {
			char ch;
			reader.Read();
			bool isXmlDeclaration = this.IsNextText(reader, "xml", false);
			bool lastWasQuestionMark = false;
			while (!reader.IsAtEnd) {
				ch = reader.Read();

				if (ch == '?') {
					lastWasQuestionMark = true;
					continue;
				}
				else if ((ch == '>') && (lastWasQuestionMark)) {
					// Exit the loop
					break;
				}

				lastWasQuestionMark = false;
			}
			return isXmlDeclaration;
		}
		
		/// <summary>
		/// Consumes the XML tag.
		/// </summary>
		/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
		/// <returns>
		/// <c>true</c> if the XML tag is a block start; otherwise, <c>false</c>.
		/// </returns>
		private bool ConsumeXmlTag(ITextBufferReader reader) {
			bool isBlockStart = true;
			while (!reader.IsAtEnd) {
				switch (reader.Read()) {
					case '/':
						isBlockStart = (reader.Peek() != '>');
						break;
					case '>':
						return isBlockStart;
					case '<':
						// Skip over embedded expressions in tags
						if (this.IsNextText(reader, "%=", false))
							this.ConsumeXmlEmbeddedExpression(reader);
						break;
				}
			}
			return isBlockStart;
		}

		/// <summary>
		/// Looks forward (skipping whitespace) to see if the next text matches the specified text.
		/// </summary>
		/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
		/// <param name="nextText">The next text to match.</param>
		/// <param name="allowWhitespaceAndLineContinuations">Whether to allow whitespace and line continuations.</param>
		/// <returns>
		/// <c>true</c> if the next text matches the specified text; otherwise, <c>false</c>.
		/// </returns>
		private bool IsNextText(ITextBufferReader reader, string nextText, bool allowWhitespaceAndLineContinuations) {
			int offset = reader.Offset;
			
			char ch;
			bool result = false;
			int nextTextIndex = 0;
			while (!reader.IsAtEnd) {
				ch = Char.ToLowerInvariant(reader.Read());
				if (ch == nextText[nextTextIndex]) {
					nextTextIndex++;
					while ((nextTextIndex < nextText.Length) && (Char.ToLowerInvariant(reader.Read()) == nextText[nextTextIndex])) {
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

		/// <summary>
		/// Returns whether the current "&lt;" character starts an XML literal.
		/// </summary>
		/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
		/// <param name="checkNextCharacter">Whether to check the next character for a valid XML literal start.</param>
		/// <returns>
		/// <c>true</c> if the current "&lt;" character starts an XML literal; otherwise, <c>false</c>.
		/// </returns>
		private bool IsXmlLiteral(ITextBufferReader reader, bool checkNextCharacter) {
			// If the next character after < is a letter or other valid XML character...
			if ((!checkNextCharacter) || (Char.IsLetter(reader.Peek())) || ("_:?!".IndexOf(reader.Peek()) != -1)) {
				// Now do more complex logic to see if the characters before the < indicate an attribute context
				reader.Push();
				try {
					reader.ReadReverse();
					while (!reader.IsAtStart) {
						char ch = reader.ReadReverse();

						switch (ch) {
							case '|':
								// | characters before < indicates attribute
								return false;
							case '\n':
								// Line terminator before < indicates attribute but check the previous line for a line continuation
								while (!reader.IsAtStart) {
									ch = reader.ReadReverse();

									if (ch == '\n') {
										// Another line terminator means an attribute
										return false;
									}

									// Skip over whitespace
									if (Char.IsWhiteSpace(ch))
										continue;

									if (ch == '_') {
										// Is a line continuation
										if (this.IsXmlLiteral(reader, false)) {
											// The characters before _ indicate the possibility of an XML literal
											// NOTE: Should check that the line is not a comment for completeness
											return true;
										}
									}
									break;
								}
								return false;
							case '>':
								// > before < indicates attribute
								return false;
							case '(':
							case ',':
								// After a ( or , means that we are in a possible ParameterList... could be either an attribute or XML literal...
								// Parameter is defined like this: 
								//   Parameter ::= [ Attributes ] ParameterModifier+ ParameterIdentifier [ As TypeName ] [ = ConstantExpression ]
								//   ParameterModifier ::= ByVal | ByRef | Optional | ParamArray
								// So a tag followed by ParameterModifier and then identifier we can assume is attribute

								// Return to the start offset
								reader.Pop();
								reader.Push();

								// Read the XML tag 
								this.ConsumeXmlTag(reader);

								// Now check the following characters
								if (
									(this.IsNextText(reader, "byval", true)) ||
									(this.IsNextText(reader, "byref", true)) ||
									(this.IsNextText(reader, "optional", true)) ||
									(this.IsNextText(reader, "paramarray", true))
									) {
									// These keywords after the tag indicate an attribute
									return false;
								}

								// Allow XML literal
								return true;
							case '.':
							case '=':
							case '&':
							case '*':
							case '+':
							case '-':
							case '/':
							case '\\':
							case '^':
							case '<':
								// Allow XML literal when following dots or operators 
								return true;
							default:
								// Skip over whitespace
								if (Char.IsWhiteSpace(ch))
									continue;

								// If the character is a letter...
								if (Char.IsLetter(ch)) {
									// Scan to get the whole word
									int startOffset = reader.Offset;
									int endOffset = reader.Offset + 1;
									while (!reader.IsAtStart) {
										ch = reader.ReadReverse();
										if (!Char.IsLetter(ch))
											break;
										startOffset--;
									}

									// If the word is a keyword, get its token ID
									string word = reader.GetSubstring(startOffset, endOffset - startOffset);
									object tokenID = keywords[word.ToLowerInvariant()];
									if (tokenID != null) {
										// Ensure that it is not a keyword and not a contextual keyword
										if (((int)tokenID > VBTokenID.KeywordStart) && ((int)tokenID < VBTokenID.KeywordEnd)) {
											if (word.ToLowerInvariant() == "as") {
												// These characters can come before an As keyword: <space>, \t, \n, ), ?, \"
												// 'As' keyword before < indicates attribute
												return false;
											}

											// Allow XML literal since this is most likely an expression
											return true;
										}
									}
								}

								// Don't Allow XML literal
								return false;
						}
					}
				}
				finally {
					// Return to the starting point
					reader.Pop();
				}
			}
			return false;
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
			int tokenID = VBTokenID.Invalid;
			bool isLooseMatch = false;

			// Get the next character
			char ch = reader.Read();

			switch (lexicalState.ID) {
				case VBLexicalStateID.DocumentationComment:
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
							tokenID = VBTokenID.DocumentationCommentTag;
							break;
						default:
							// Parse the comment text
							while ((!reader.IsAtEnd) && (reader.Peek() != '<') && (reader.Peek() != '\n'))
								reader.Read();
							tokenID = VBTokenID.DocumentationCommentText;
							break;
					}
					break;
				case VBLexicalStateID.PreProcessorDirective:
					switch (ch) {
						case '\n':
							// Ignore and let exit scope handle
							break;
						default:
							if (ch == '\'') {
								// Parse a single-line comment
								tokenID = this.ParseSingleLineComment(reader);
								break;
							}

							// Parse the directive text
							while ((!reader.IsAtEnd) && (reader.Peek() != '\n') && (reader.Peek() != '\''))
								reader.Read();
							tokenID = VBTokenID.PreProcessorDirectiveText;
							break;
					}
					break;
				case VBLexicalStateID.Default:
					// If the character is a letter or digit...
					if ((Char.IsLetter(ch) || (ch == '_'))) {
						reader.ReadReverse();
						if (reader.PeekReverse() == '@') {
							// Parse the XML attribute
							reader.Read();
							tokenID = this.ParseXmlAttribute(reader);
						}
						else {
							// Parse the identifier
							reader.Read();
							tokenID = this.ParseIdentifier(reader, ch, false, ref isLooseMatch);
						}
					}
					else if ((ch != '\n') && (Char.IsWhiteSpace(ch))) {
						while ((reader.Peek() != '\n') && (Char.IsWhiteSpace(reader.Peek()))) 
							reader.Read();
						tokenID = VBTokenID.Whitespace;
					}
					else {
						tokenID = VBTokenID.Invalid;
						switch (ch) {
							case '.': {
								tokenID = VBTokenID.Dot;
								char ch2 = reader.Peek();
								if ((ch2 >= '0') && (ch2 <= '9')) {
									// Parse the number
									tokenID = this.ParseNumber(reader, ch, false, false);
								}
								else if (ch2 == '.') {
									reader.Read();
									if (reader.Peek() == '.') {
										// XML descendant access
										reader.Read();
										tokenID = VBTokenID.TripleDot;
									}
									else {
										// Back up
										reader.ReadReverse();
									}
								}	
								else if (ch2 == '@') {
									// XML attribute access
									reader.Read();
									tokenID = VBTokenID.DotAt;
								}
								break;
							}
							case ',':
								tokenID = VBTokenID.Comma;
								break;
							case '(':
								tokenID = VBTokenID.OpenParenthesis;
								break;
							case ')':
								tokenID = VBTokenID.CloseParenthesis;
								break;
							case '\n':
								// Line terminator
								tokenID = VBTokenID.LineTerminator;
								break;
							case '{':
								tokenID = VBTokenID.OpenCurlyBrace;
								break;
							case '}':
								tokenID = VBTokenID.CloseCurlyBrace;
								break;
							case '\"':
								// Parse a string literal
								tokenID = this.ParseStringLiteral(reader);
								break;
							case '\'': 
								// Parse a single-line comment
								tokenID = this.ParseSingleLineComment(reader);
								break;
							case '=':
								tokenID = VBTokenID.Equality;
								break;
							case '[':
								if ((Char.IsLetter(reader.Peek()) || (reader.Peek() == '_'))) {
									// Parse the escaped identifier
									tokenID = this.ParseIdentifier(reader, reader.Read(), true, ref isLooseMatch);
								}
								break;
							case '&':
								tokenID = VBTokenID.StringConcatenation;
								switch (reader.Peek()) {
									case 'H':
									case 'h':
										reader.Read();
										ch = reader.Peek();
										if ("0123456789AaBbCcDdEeFf".IndexOf(ch) != -1) {
											// Parse the number
											tokenID = this.ParseNumber(reader, ch, true, false);
										}
										else {
											// Back up
											reader.ReadReverse();
										}
										break;
									case 'O':
									case 'o':
										reader.Read();
										ch = reader.Peek();
										if ((ch >= '0') && (ch <= '7')) {
											// Parse the number
											tokenID = this.ParseNumber(reader, ch, false, true);
										}
										else {
											// Back up
											reader.ReadReverse();
										}
										break;
									case '=':
										reader.Read();
										tokenID = VBTokenID.StringConcatenationAssignment;
										break;
								}
								break;
							case '*':
								if (reader.Peek() == '=') {
									reader.Read();
									tokenID = VBTokenID.MultiplicationAssignment;
								}
								else
									tokenID = VBTokenID.Multiplication;
								break;
							case '+':
								if (reader.Peek() == '=') {
									reader.Read();
									tokenID = VBTokenID.AdditionAssignment;
								}
								else
									tokenID = VBTokenID.Addition;
								break;
							case '-':
								if (reader.Peek() == '=') {
									reader.Read();
									tokenID = VBTokenID.SubtractionAssignment;
								}
								else
									tokenID = VBTokenID.Subtraction;
								break;
							case '/':
								if (reader.Peek() == '=') {
									reader.Read();
									tokenID = VBTokenID.FloatingPointDivisionAssignment;
								}
								else
									tokenID = VBTokenID.FloatingPointDivision;
								break;
							case '\\':
								if (reader.Peek() == '=') {
									reader.Read();
									tokenID = VBTokenID.IntegerDivisionAssignment;
								}
								else
									tokenID = VBTokenID.IntegerDivision;
								break;
							case '^':
								if (reader.Peek() == '=') {
									reader.Read();
									tokenID = VBTokenID.ExponentiationAssignment;
								}
								else
									tokenID = VBTokenID.Exponentiation;
								break;
							case '<':
								tokenID = VBTokenID.LessThan;
								switch (reader.Peek()) {
									case '>':
										reader.Read();
										tokenID = VBTokenID.Inequality;
										break;
									case '=':
										reader.Read();
										tokenID = VBTokenID.LessThanOrEqual;
										break;
									case '<':
										reader.Read();
										switch (reader.Peek()) {
											case '=':
												reader.Read();
												tokenID = VBTokenID.LeftShiftAssignment;
												break;
											case '%':
												reader.ReadReverse();  // Read back to second '<'
												tokenID = this.ParseXmlLiteral(reader);
												break;
											default:
												tokenID = VBTokenID.LeftShift;
												break;
										}
										break;
									default:
										reader.ReadReverse();
										if (reader.PeekReverse() == '@') {
											// Parse the XML attribute
											reader.Read();
											tokenID = this.ParseXmlAttribute(reader);
										}
										else if ((reader.Read() != '\0') && (this.IsXmlLiteral(reader, true))) {
											// Parse the XML literal
											tokenID = this.ParseXmlLiteral(reader);
										}
										break;
								}
								break;
							case '>':
								tokenID = VBTokenID.GreaterThan;
								switch (reader.Peek()) {
									case '=':
										reader.Read();
										tokenID = VBTokenID.GreaterThanOrEqual;
										break;
									case '>':
										reader.Read();
										if (reader.Peek() == '=') {
											reader.Read();
											tokenID = VBTokenID.RightShiftAssignment;
										}
										else
											tokenID = VBTokenID.RightShift;
										break;
								}
								break;
							case '#':
								if ((Char.IsNumber(reader.Peek())) || (Char.IsWhiteSpace(reader.Peek()))) {
									// Parse the date literal
									tokenID = this.ParseDateLiteral(reader);
								}
								break;
							case ':':
								if (reader.Peek() == '=') {
									reader.Read();
									tokenID = VBTokenID.ColonEquals;
								}
								else
									tokenID = VBTokenID.Colon;
								break;
							case '!':
								tokenID = VBTokenID.ExclamationPoint;
								break;
							case '?':
								tokenID = VBTokenID.QuestionMark;
								break;
							default:
								if ((ch >= '0') && (ch <= '9')) {
									// Parse the number
									tokenID = this.ParseNumber(reader, ch, false, false);
								}
								break;
						}
					}
					break;
			}

			if (tokenID != VBTokenID.Invalid) {
				lexicalParseData = new LexicalStateAndIDTokenLexicalParseData(lexicalState, (byte)tokenID);
				return (isLooseMatch ? MatchType.LooseMatch : MatchType.ExactMatch);
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
				lexicalParseData = new LexicalScopeAndIDTokenLexicalParseData(lexicalScope, VBTokenID.LineTerminator);
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
			if (reader.Peek() == '\'') {
				reader.Read();
				if (reader.Peek() == '\'') {
					reader.Read();
					if (reader.Peek() == '\'') {
						reader.Read();
						lexicalParseData = new LexicalScopeAndIDTokenLexicalParseData(lexicalScope, VBTokenID.DocumentationCommentDelimiter);
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
				lexicalParseData = new LexicalScopeAndIDTokenLexicalParseData(lexicalScope, VBTokenID.LineTerminator);
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
				string text = (reader.Offset != startOffset ? reader.GetSubstring(startOffset, reader.Offset - startOffset).ToLowerInvariant() : null);
				if (text == "end") {
					// Store the current offset
					int offset = reader.Offset;

					// Skip over whitespace
					while ((!reader.IsAtEnd) && (Char.IsWhiteSpace(reader.Peek())) && (reader.Peek() != '\n'))
						reader.Read();

					// Gather the next word
					while ((!reader.IsAtEnd) && (Char.IsLetter(reader.Peek())))
						reader.Read();

					string endWhat = (reader.Offset != offset ? reader.GetSubstring(offset, reader.Offset - offset).ToLowerInvariant().Trim() : null);
					if (endWhat != null)
						text += " " + endWhat;
					else
						reader.Offset = offset;
				}

				int tokenID = VBTokenID.Invalid;
				switch (text) {
					case "const":
						tokenID = VBTokenID.ConstPreProcessorDirective;
						break;
					case "else":
						tokenID = VBTokenID.ElsePreProcessorDirective;
						break;
					case "elseif":
						tokenID = VBTokenID.ElseIfPreProcessorDirective;
						break;
					case "end externalsource":
						tokenID = VBTokenID.EndExternalSourcePreProcessorDirective;
						break;
					case "end if":
						tokenID = VBTokenID.EndIfPreProcessorDirective;
						break;
					case "end region":
						tokenID = VBTokenID.EndRegionPreProcessorDirective;
						break;
					case "externalchecksum":
						tokenID = VBTokenID.ExternalChecksumPreProcessorDirective;
						break;
					case "externalsource":
						tokenID = VBTokenID.ExternalSourcePreProcessorDirective;
						break;
					case "if":
						tokenID = VBTokenID.IfPreProcessorDirective;
						break;
					case "region":
						tokenID = VBTokenID.RegionPreProcessorDirective;
						break;
				}

				// Back up if it is an invalid directive
				if (tokenID == VBTokenID.Invalid) {
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
		/// Parses a date literal.
		/// </summary>
		/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
		/// <returns>The ID of the token that was matched.</returns>
		protected virtual int ParseDateLiteral(ITextBufferReader reader) {
			reader.Read();
			while (!reader.IsAtEnd) {
				switch (reader.Peek()) {
					case '\n':
						return VBTokenID.DateLiteral;
					case '#':
						reader.Read();
						return VBTokenID.DateLiteral;
					default:
						reader.Read();
						break;
				}
			}
			return VBTokenID.DateLiteral;
		}
		
		/// <summary>
		/// Parses an identifier.
		/// </summary>
		/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
		/// <param name="ch">The first character of the identifier.</param>
		/// <param name="isEscaped">Whether the identifier is escaped.</param>
		/// <param name="isLooseMatch">Whether the match is a loose match.</param>
		/// <returns>The ID of the token that was matched.</returns>
		protected virtual int ParseIdentifier(ITextBufferReader reader, char ch, bool isEscaped, ref bool isLooseMatch) {
			bool allowKeyword = true;

			// First see if a keyword is allowed (not allowed after a dot)
			int startOffset = reader.Offset - 1;
			reader.ReadReverse();
			while (!reader.IsAtStart) {
				char ch2 = reader.ReadReverse();
				if (ch2 == '.') {
					allowKeyword = false;
					break;
				}
				if ((ch2 == ' ') || (ch2 == '\t'))
					continue;
				else
					break;
			}

			// Get the entire word
			reader.Offset = startOffset + 1;
			while (!reader.IsAtEnd) {
				char ch2 = reader.Read();
				// NOTE: This could be improved by supporting \u escape sequences
				if ((!char.IsLetterOrDigit(ch2)) && (ch2 != '_')) {
					if (ch2 > '\u00FF') {
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

			// If the identifier is escaped, read the ]
			if (isEscaped) {
				if (reader.Peek() == ']')
					reader.Read();
				return VBTokenID.Identifier;
			}

			// If a single _, then it is a line continuation
			if ((ch == '_') && (startOffset == reader.Offset - 1))
				return VBTokenID.LineContinuation;

			// Determine if the word is a REM comment
			if (((ch == 'R') || (ch == 'r')) && (reader.Offset - startOffset == 3) && (reader.GetSubstring(startOffset, reader.Offset - startOffset).ToUpperInvariant() == "REM")) {
				while ((!reader.IsAtEnd) && (reader.Peek() != '\n'))
					reader.Read();
				return VBTokenID.RemComment;
			}

			// See if a type character is added next
			if (!reader.IsAtEnd) {
				switch (reader.Peek()) {
					case '%':
					case '&':
					case '@':
					case '#':
					case '$':
						reader.Read();
						return VBTokenID.Identifier;
					case '!':
						reader.Read();

						ch = reader.Peek();
						if ((Char.IsLetter(ch) || (ch == '_'))) {
							// Dictionary access, so exclude the '!' from the identifier
							reader.ReadReverse();
						}

						return VBTokenID.Identifier;
				}
			}

			// Determine if the word is a keyword
			if ((allowKeyword) && (((ch >= 'A') && (ch <= 'Z')) || ((ch >= 'a') && (ch <= 'z')))) {
				string text = reader.GetSubstring(startOffset, reader.Offset - startOffset);
				object value = keywords[text.ToLowerInvariant()];
				if (value != null) {
					// A keyword was matched
					switch ((int)value) {
						case VBTokenID.Custom:
							if (!this.IsNextText(reader, "event", true))
								value = null;
							break;
					}

					if (value != null) {
						isLooseMatch = (text != (VBTokenID.GetTokenKey((int)value)));
						return (int)value;
					}
				}
			}
			
			return VBTokenID.Identifier;
		}
		
		/// <summary>
		/// Parses a number.
		/// </summary>
		/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
		/// <param name="ch">The first character of the number.</param>
		/// <param name="isHex">Whether the number is a hexadecimal number.</param>
		/// <param name="isOctal">Whether the number is an octal number.</param>
		/// <returns>The ID of the token that was matched.</returns>
		protected virtual int ParseNumber(ITextBufferReader reader, char ch, bool isHex, bool isOctal) {
			int tokenID = VBTokenID.DecimalIntegerLiteral;

			if (isHex) {
				// Get the set of digits
				tokenID = VBTokenID.HexadecimalIntegerLiteral;
				while ("0123456789AaBbCcDdEeFf".IndexOf(reader.Peek()) != -1)
					reader.Read();
			}
			else if (isOctal) {
				// Get the set of digits
				tokenID = VBTokenID.OctalIntegerLiteral;
				while ((reader.Peek() >= '0') && (reader.Peek() <= '7'))
					reader.Read();
			}
			else {
				if (ch != '.') {
					// Get the first set of digits
					while (Char.IsNumber(reader.Peek()))
						reader.Read();
				}
				if ((ch == '.') || (reader.Peek() == '.')) {
					// Flag as a real number
					tokenID = VBTokenID.FloatingPointLiteral;

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
						tokenID = VBTokenID.DecimalIntegerLiteral;
					}
				}
				if (Char.ToLowerInvariant(reader.Peek()) == 'e') {
					// Flag as a real number
					tokenID = VBTokenID.FloatingPointLiteral;

					// Get the exponent
					reader.Read();
					if ("+-".IndexOf(reader.Peek()) != -1)
						reader.Read();

					// Get the next set of digits
					while (Char.IsNumber(reader.Peek()))
						reader.Read();
				}
				if ("FfDdRr".IndexOf(reader.Peek()) != -1) {
					// Flag as a floating point number
					tokenID = VBTokenID.FloatingPointLiteral;

					// Get the floating point type suffix
					reader.Read();
				}
			}

			switch (tokenID) {
				case VBTokenID.DecimalIntegerLiteral:
				case VBTokenID.HexadecimalIntegerLiteral:
				case VBTokenID.OctalIntegerLiteral:
					// Parse integer type suffix
					switch (Char.ToUpperInvariant(reader.Peek())) {
						case '%':
						case '&':
						case 'I':
						case 'L':
						case 'S':
							reader.Read();
							break;
						case 'U':
							reader.Read();
							switch (Char.ToUpperInvariant(reader.Peek())) {
								case 'I':
								case 'L':
								case 'S':
									reader.Read();
									break;
								default:
									reader.ReadReverse();
									break;
							}
							break;
					}

					// Parse type characters
					switch (Char.ToUpperInvariant(reader.Peek())) {
						case '%':
						case '&':
							reader.Read();
							break;
						case '@':
						case '!':
						case '#':
							if (tokenID == VBTokenID.DecimalIntegerLiteral)
								reader.Read();
							break;
					}
					break;
				case VBTokenID.FloatingPointLiteral:
					// Parse type characters
					switch (Char.ToUpperInvariant(reader.Peek())) {
						case '@':
						case '!':
						case '#':
							reader.Read();
							break;
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
			while ((!reader.IsAtEnd) && (reader.Peek() != '\n'))
				reader.Read();
			return VBTokenID.SingleLineComment;
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
					case '\"':
						if (reader.Peek() == '\"')
							reader.Read();
						else
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

			if ((!reader.IsAtEnd) && (Char.ToUpperInvariant(reader.Peek()) == 'C')) {
				reader.Read();
				return VBTokenID.CharacterLiteral;
			}
			else
				return VBTokenID.StringLiteral;
		}
		
		/// <summary>
		/// Parses an XML attribute.
		/// </summary>
		/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
		/// <returns>The ID of the token that was matched.</returns>
		protected virtual int ParseXmlAttribute(ITextBufferReader reader) {
			char ch;
			while (!reader.IsAtEnd) {
				ch = reader.Read();
				switch (ch) {
					case '>':
						return VBTokenID.XmlAttribute;
					case '_':
					case ':':
					case '-':
					case '.':
						break;
					default:
						if (!Char.IsLetterOrDigit(ch)) {
							// Back up and quit
							reader.ReadReverse();
							return VBTokenID.XmlAttribute;
						}
						break;
				}
			}
			return VBTokenID.XmlAttribute;
		}

		/// <summary>
		/// Parses an XML literal.
		/// </summary>
		/// <param name="reader">An <see cref="ITextBufferReader"/> that is reading a text source.</param>
		/// <returns>The ID of the token that was matched.</returns>
		protected virtual int ParseXmlLiteral(ITextBufferReader reader) {
			bool allowStartTagOnly = false;
			bool isXmlDocument = false;
			char ch;

			// Scan back before the < to see if there was a . before... if so, only allow start tag
			reader.Push();
			try {
				reader.ReadReverse();
				while (!reader.IsAtStart) {
					ch = reader.ReadReverse();
					if (ch == '.') {
						allowStartTagOnly = true;
						break;
					}
					else if (ch != '\n') {
						// Skip over whitespace
						if (Char.IsWhiteSpace(ch))
							continue;
					}
					break;
				}
			}
			finally {
				reader.Pop();
			}

			switch (reader.Peek()) {
				case '?': {
					// Processing instruction
					bool isXmlDeclaration = this.ConsumeXmlProcessingInstruction(reader);
					if (!isXmlDeclaration)
						return VBTokenID.XmlLiteral;
					isXmlDocument = true;

					// XML documents allow optional comments/PIs, followed by a tag block or embedded expression after
					while (!reader.IsAtEnd) {
						ch = reader.Read();
						if (ch == '<') {
							switch (reader.Peek()) {
								case '?':
									// Consume processing instruction
									this.ConsumeXmlProcessingInstruction(reader);
									continue;
								case '!':
									if (this.IsNextText(reader, "!--", false)) {
										// Consume comment 
										this.ConsumeXmlCommentOrCData(reader);
										continue;
									}
									break;
								case '%':
									// Embedded VB expression
									this.ConsumeXmlEmbeddedExpression(reader);
									return VBTokenID.XmlLiteral;
							}
							
							if ((Char.IsLetter(reader.Peek())) || ("_:?!".IndexOf(reader.Peek()) != -1)) {
								// Start tag
								break;
							}
							else {
								// Quit since the XML declaration must not be complete
								reader.ReadReverse();
								return VBTokenID.XmlLiteral;
							}
						}
						else if (!Char.IsWhiteSpace(ch)) {
							// Quit since the XML declaration must not be complete
							reader.ReadReverse();
							return VBTokenID.XmlLiteral;
						}
					}
					break;
				}
				case '!':
					// Comment or CDATA
					this.ConsumeXmlCommentOrCData(reader);
					return VBTokenID.XmlLiteral;
				default:
					// See if the tag is an xmlns declaration
					allowStartTagOnly |= this.IsNextText(reader, "xmlns:", false);
					break;
			}

			// Consume tag and if it is a block start, continue on
			if ((this.ConsumeXmlTag(reader)) && (!allowStartTagOnly)) {
				int tagLevel = 1;
				bool exitLoop = false;
				while ((!exitLoop) && (!reader.IsAtEnd)) {
					ch = reader.Read();
					if (ch == '<') {
						switch (reader.Peek()) {
							case '?':
								// Processing instruction
								this.ConsumeXmlProcessingInstruction(reader);
								break;
							case '!':
								// Comment or CDATA
								this.ConsumeXmlCommentOrCData(reader);
								break;
							case '/':
								// A block end tag... quit if we are at the end of the element structure
								if (--tagLevel <= 0) {
									this.ConsumeXmlTag(reader);
									exitLoop = true;
								}
								break;
							case '%':
								// Embedded VB expression
								this.ConsumeXmlEmbeddedExpression(reader);
								break;
							default:
								if (this.ConsumeXmlTag(reader)) {
									// Another block start tag
									tagLevel++;
								}
								break;
						}
					}
				}
			}

			if (isXmlDocument) {
				// XML documents allow optional comments/PIs after the tag block
				int xmlLiteralEndOffset = reader.Offset;
				while (!reader.IsAtEnd) {
					ch = reader.Read();
					if (ch == '<') {
						switch (reader.Peek()) {
							case '?':
								// Consume processing instruction
								this.ConsumeXmlProcessingInstruction(reader);
								xmlLiteralEndOffset = reader.Offset;
								continue;
							case '!':
								if (this.IsNextText(reader, "!--", false)) {
									// Consume comment 
									this.ConsumeXmlCommentOrCData(reader);
									xmlLiteralEndOffset = reader.Offset;
									continue;
								}
								break;
						}
						
						// Quit since the character is not a comment or PI start
						break;
					}
					else if (!Char.IsWhiteSpace(ch)) {
						// Quit since the character is not a comment or PI start
						break;
					}
				}	

				// Back up to the last character in the XML literal
				while (reader.Offset > xmlLiteralEndOffset)
					reader.ReadReverse();
			}

			return VBTokenID.XmlLiteral;
		}

	}
}
