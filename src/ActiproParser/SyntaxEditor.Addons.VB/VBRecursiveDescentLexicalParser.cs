using System;
using System.Collections;
using System.Diagnostics;
using System.Text;
using ActiproSoftware.SyntaxEditor.ParserGenerator;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;

namespace ActiproSoftware.SyntaxEditor.Addons.VB {

	/// <summary>
	/// Represents a <c>Visual Basic</c> recursive descent lexical parser implementation.
	/// </summary>
	internal class VBRecursiveDescentLexicalParser : MergableRecursiveDescentLexicalParser {

		private ArrayList						comments								= new ArrayList();
		private StringBuilder					documentationComment					= new StringBuilder();
		private ArrayList						documentationCommentTextRanges			= new ArrayList();
		private TextRange						currentDocumentationCommentTextRange	= TextRange.Deleted;
		private ArrayList						regionTextRanges						= new ArrayList();
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>VBRecursiveDescentLexicalParser</c> class.
		/// </summary>
		/// <param name="language">The <see cref="VBSyntaxLanguage"/> to use.</param>
		/// <param name="manager">The <see cref="MergableLexicalParserManager"/> to use for coordinating merged languages.</param>
		public VBRecursiveDescentLexicalParser(VBSyntaxLanguage language, MergableLexicalParserManager manager) : base(language, manager) {}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Closes the current documentation comment.
		/// </summary>
		private void CloseDocumentationComment() {
			// If tracking a documentation comment...
			if ((!currentDocumentationCommentTextRange.IsDeleted) && (!this.TextBufferReader.HasStackEntries)) {
				// Terminate the documentation comment
				documentationCommentTextRanges.Add(currentDocumentationCommentTextRange);
				currentDocumentationCommentTextRange = TextRange.Deleted;
			}
		}
		
		/// <summary>
		/// Consumes any implicit line continuation after the current token.
		/// </summary>
		private void ConsumeImplicitLineContinuationAfter() {
			// Peek ahead to see if there is optional whitespace followed by a line terminator
			int targetOffset = int.MinValue;
			ITextBufferReader reader = this.Manager.TextBufferReader;
			reader.Push();

			// Read characters forward
			char ch;
			while (!reader.IsAtEnd) {
				ch = reader.Read();
				if (Char.IsWhiteSpace(ch)) {
					if (ch == '\n') {
						targetOffset = reader.Offset;
						break;
					}
				}
				else
					break;
			}
			reader.Pop();

			// Quit if no implicit line continuation
			if (targetOffset == int.MinValue)
				return;

			// Consume characters through past the line terminator
			while (reader.Offset < targetOffset)
				reader.Read();
		}

		/// <summary>
		/// Returns whether the line terminator is an implicit line continuation before another token.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the line terminator is an implicit line continuation; otherwise, <c>false</c>.
		/// </returns>
		private bool IsImplicitLineContinuationBefore() {
			// Peek ahead to see if there is optional whitespace followed by a line terminator
			bool isImplicitLineContinuation = false;
			ITextBufferReader reader = this.Manager.TextBufferReader;
			reader.Push();

			// Read characters forward
			char ch;
			while (!reader.IsAtEnd) {
				ch = reader.Read();
				if (Char.IsWhiteSpace(ch)) {
					if (ch == '\n')
						break;
				}
				else {
					switch (ch) {
						case ',':
						case ')':
						case '}':
							isImplicitLineContinuation = true;
							break;
					}
					break;
				}
			}
			reader.Pop();

			return isImplicitLineContinuation;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the collection of <see cref="TextRange"/> objects that indicate the range of each documentation comment in the compilation unit.
		/// </summary>
		/// <value>The collection of <see cref="TextRange"/> objects that indicate the range of each documentation comment in the compilation unit.</value>
		/// <remarks>
		/// Do not access this property until parsing is complete.
		/// </remarks>
		public IList DocumentationCommentTextRanges {
			get {
				return documentationCommentTextRanges;
			}
		}

		/// <summary>
		/// Returns the next <see cref="IToken"/> and seeks past it.
		/// </summary>
		/// <returns>The next <see cref="IToken"/>.</returns>
		protected override IToken GetNextTokenCore() {
			IToken token = null;
			int startOffset = this.TextBufferReader.Offset;
			bool consumeThroughLineTerminator = false;

			while (!this.IsAtEnd) {
				// Get the next token
				token = this.Manager.GetNextToken();				

				// Update whether there is non-whitespace since the last line start
				if (token.LexicalState == this.Language.DefaultLexicalState) {
					switch (token.ID) {
						case VBTokenID.DocumentationCommentDelimiter:
							if ((!consumeThroughLineTerminator) && (!this.TextBufferReader.HasStackEntries)) {
								// Append documentation 
								StringBuilder documentationComment = new StringBuilder();
								while ((!this.TextBufferReader.IsAtEnd) && (this.TextBufferReader.Peek() != '\n'))
									documentationComment.Append(this.TextBufferReader.Read());
								this.documentationComment.Append(documentationComment.ToString().Trim() + " ");

								if (currentDocumentationCommentTextRange.IsDeleted) {
									// Store the start of the documentation comment
									currentDocumentationCommentTextRange = new TextRange(token.StartOffset, this.TextBufferReader.Offset);
								}
								else {
									// Extend the documentation comment
									currentDocumentationCommentTextRange = new TextRange(currentDocumentationCommentTextRange.StartOffset, this.TextBufferReader.Offset);
								}
							}							
							// Consume non-significant token
							break;
						case VBTokenID.Whitespace:
							// Consume non-significant token
							break;
						case VBTokenID.LineContinuation:
							// Consume through the line terminator
							consumeThroughLineTerminator = true;
							break;
						case VBTokenID.SingleLineComment:
						case VBTokenID.RemComment:
							if (!consumeThroughLineTerminator) {
								// Close the current documentation comment
								this.CloseDocumentationComment();

								if (!this.TextBufferReader.HasStackEntries) {
									// Store the comment
									Comment comment = new Comment(CommentType.SingleLine, new TextRange(token.StartOffset, token.EndOffset), 
										this.TextBufferReader.GetSubstring(token.StartOffset, token.Length));
									comments.Add(comment);
								}
							}
							// Consume non-significant token
							break;
						default:
							if (consumeThroughLineTerminator) {
								if (token.ID == VBTokenID.LineTerminator)
									consumeThroughLineTerminator = false;
								// Consume non-significant token
								break;
							}
							else if ((token.ID > VBTokenID.PreProcessorDirectiveKeywordStart) && (token.ID < VBTokenID.PreProcessorDirectiveKeywordEnd)) {
								// Close the current documentation comment
								this.CloseDocumentationComment();

								if (!this.TextBufferReader.HasStackEntries) {
									// Get the token text
									string tokenText = this.TextBufferReader.GetSubstring(token.StartOffset, token.Length);
									if (tokenText.ToLower().StartsWith("#region")) {
										// Start a new region... save its start offset
										regionTextRanges.Add(new TextRange(token.StartOffset, this.TextBufferReader.Length));
									}
									else if (tokenText.ToLower().StartsWith("#end region")) {
										// If tracking an open region, close it
										for (int index = regionTextRanges.Count - 1; index >= 0; index--) {
											TextRange existingTextRange = (TextRange)regionTextRanges[index];
											if (existingTextRange.EndOffset == this.TextBufferReader.Length) {
												// Terminate the region
												regionTextRanges[index] = new TextRange(existingTextRange.StartOffset, token.EndOffset);

												// 10/8/2008 - Try and extend the region to fold over text following the #endregion
												// If there are still at least two characters remaining...
												if (token.EndOffset < this.TextBufferReader.Length - 1) {
													tokenText = this.TextBufferReader.GetSubstring(token.EndOffset, 1);

													// If not ending the line or starting a comment...
													if ((tokenText != "\n") && (tokenText != "'") && (!this.IsAtEnd)) {
														// Get the next token (it should be preprocessor directive text) and extend the region
														token = this.Manager.GetNextToken();
														if (token.ID == VBTokenID.PreProcessorDirectiveText) {
															// Extend the region
															regionTextRanges[index] = new TextRange(existingTextRange.StartOffset, token.EndOffset);
														}
													}
												}
												
												break;
											}
										}
									}
								}

								// Consume non-significant token
								break;
							}
							else {
								// Close the current documentation comment
								this.CloseDocumentationComment();

								// Handle VB 10.0 implicit line continuations
								switch (token.ID) {
									case VBTokenID.ColonEquals:
									case VBTokenID.Comma:
									case VBTokenID.Equality:
									case VBTokenID.OpenCurlyBrace:
									case VBTokenID.OpenParenthesis:
										this.ConsumeImplicitLineContinuationAfter();
										break;
									case VBTokenID.LineTerminator:
										// Skip the line terminator if it is an implicit line continuation
										if (this.IsImplicitLineContinuationBefore())
											continue;
										break;
								}
									
								// Return the significant token
								return token;
							}
					}
				}
				else if (token.HasFlag(LexicalParseFlags.LanguageStart)) {
					// Close the current documentation comment
					this.CloseDocumentationComment();
						
					// Return the significant token (which is in a different language)
					return token;
				}

				// Advance the start offset
				startOffset = this.TextBufferReader.Offset;
			}

			// Close the current documentation comment
			this.CloseDocumentationComment();

			// Return an end of document token
			if (this.Token != null)
				return this.Language.CreateDocumentEndToken(startOffset, this.Token.LexicalState);
			else
				return this.Language.CreateDocumentEndToken(startOffset, this.Language.DefaultLexicalState);
		}

		/// <summary>
		/// Reaps the comments that have been collected since the last reaping.
		/// </summary>
		/// <param name="textRange">The <see cref="TextRange"/> before which the comments must start.</param>
		/// <returns>The comments that have been collection since the last reaping.</returns>
		public Comment[] ReapComments(TextRange textRange) {
			int count = 0;
			for (int index = 0; index < comments.Count; index++) {
				// EndOffset = StartOffset - 1 when there is no end defined
				if ((textRange.EndOffset != textRange.StartOffset - 1) && (((Comment)comments[index]).StartOffset >= textRange.EndOffset))
					break;
				count++;
			}

			if (count == 0)
				return null;

			Comment[] commentArray = new Comment[count];
			comments.CopyTo(0, commentArray, 0, count);
			comments.RemoveRange(0, count);
			return commentArray;
		}
		
		/// <summary>
		/// Reaps the documentation comments that have been collected since the last reaping.
		/// </summary>
		/// <returns>The documentation comments that have been collection since the last reaping.</returns>
		public string ReapDocumentationComments() {
			string comment = documentationComment.ToString();
			documentationComment.Remove(0, documentationComment.Length);
			return comment;
		}
		
		/// <summary>
		/// Gets the collection of <see cref="TextRange"/> objects that indicate the range of each region pre-processor directive in the compilation unit.
		/// </summary>
		/// <value>The collection of <see cref="TextRange"/> objects that indicate the range of each region pre-processor directive in the compilation unit.</value>
		/// <remarks>
		/// Do not access this property until parsing is complete.
		/// </remarks>
		public IList RegionTextRanges {
			get {
				return regionTextRanges;
			}
		}

	}
}
