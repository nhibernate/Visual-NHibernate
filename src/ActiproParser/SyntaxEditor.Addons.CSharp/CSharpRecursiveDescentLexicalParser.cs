using System;
using System.Collections;
using System.Diagnostics;
using System.Text;
using ActiproSoftware.SyntaxEditor.ParserGenerator;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom;

namespace ActiproSoftware.SyntaxEditor.Addons.CSharp {

	/// <summary>
	/// Represents a <c>C#</c> recursive descent lexical parser implementation.
	/// </summary>
	internal class CSharpRecursiveDescentLexicalParser : MergableRecursiveDescentLexicalParser {

		private ArrayList						comments								= new ArrayList();
		private StringBuilder					documentationComment					= new StringBuilder();
		private ArrayList						documentationCommentTextRanges			= new ArrayList();
		private TextRange						currentDocumentationCommentTextRange	= TextRange.Deleted;
		private ArrayList						multiLineCommentTextRanges				= new ArrayList();
		private ArrayList						regionTextRanges						= new ArrayList();
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>CSharpRecursiveDescentLexicalParser</c> class.
		/// </summary>
		/// <param name="language">The <see cref="DotNetSyntaxLanguage"/> to use.</param>
		/// <param name="manager">The <see cref="MergableLexicalParserManager"/> to use for coordinating merged languages.</param>
		public CSharpRecursiveDescentLexicalParser(DotNetSyntaxLanguage language, MergableLexicalParserManager manager) : base(language, manager) {}
		
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

			while (!this.IsAtEnd) {
				// Get the next token
				token = this.Manager.GetNextToken();

				// Update whether there is non-whitespace since the last line start
				if (token.LexicalState == this.Language.DefaultLexicalState) {
					switch (token.ID) {
						case CSharpTokenID.DocumentationCommentDelimiter:
							if (!this.TextBufferReader.HasStackEntries) {
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
						case CSharpTokenID.Whitespace:
							// Consume non-significant token
							break;
						case CSharpTokenID.LineTerminator:
							// Close the current documentation comment
							this.CloseDocumentationComment();

							// Consume non-significant token
							break;
						case CSharpTokenID.SingleLineComment:
							// Close the current documentation comment
							this.CloseDocumentationComment();

							if (!this.TextBufferReader.HasStackEntries) {
								// Store the comment
								Comment comment = new Comment(CommentType.SingleLine, new TextRange(token.StartOffset, token.EndOffset), 
									this.TextBufferReader.GetSubstring(token.StartOffset, token.Length));
								comments.Add(comment);
							}

							// Consume non-significant token
							break;
						case CSharpTokenID.MultiLineComment:
							// Close the current documentation comment
							this.CloseDocumentationComment();

							if (!this.TextBufferReader.HasStackEntries) {
								// Store the comment
								Comment comment = new Comment(CommentType.MultiLine, new TextRange(token.StartOffset, token.EndOffset), 
									this.TextBufferReader.GetSubstring(token.StartOffset, token.Length));
								comments.Add(comment);

								// Add a multi-line comment range
								multiLineCommentTextRanges.Add(new TextRange(token.StartOffset, token.EndOffset));
							}
							
							// Consume non-significant token
							break;
						default:
							if ((token.ID > CSharpTokenID.PreProcessorDirectiveKeywordStart) && (token.ID < CSharpTokenID.PreProcessorDirectiveKeywordEnd)) {
								// Close the current documentation comment
								this.CloseDocumentationComment();

								if (!this.TextBufferReader.HasStackEntries) {
									// Get the token text
									string tokenText = this.TextBufferReader.GetSubstring(token.StartOffset, token.Length);
									if (tokenText.StartsWith("#region")) {
										// Start a new region... save its start offset
										regionTextRanges.Add(new TextRange(token.StartOffset, this.TextBufferReader.Length));
									}
									else if (tokenText.StartsWith("#endregion")) {
										// If tracking an open region, close it
										for (int index = regionTextRanges.Count - 1; index >= 0; index--) {
											TextRange existingTextRange = (TextRange)regionTextRanges[index];
											if (existingTextRange.EndOffset == this.TextBufferReader.Length) {
												// Terminate the region
												regionTextRanges[index] = new TextRange(existingTextRange.StartOffset, token.EndOffset);

												// 10/8/2008 - Try and extend the region to fold over text following the #endregion
												// If there are still at least two characters remaining...
												if (token.EndOffset < this.TextBufferReader.Length - 2) {
													tokenText = this.TextBufferReader.GetSubstring(token.EndOffset, 2);

													// If not ending the line or starting a comment...
													if ((!tokenText.StartsWith("\n")) && (tokenText != "//") && (!this.IsAtEnd)) {
														// Get the next token (it should be preprocessor directive text) and extend the region
														token = this.Manager.GetNextToken();
														if (token.ID == CSharpTokenID.PreProcessorDirectiveText) {
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
		/// Gets the collection of <see cref="TextRange"/> objects that indicate the range of each multi-line comment in the compilation unit.
		/// </summary>
		/// <value>The collection of <see cref="TextRange"/> objects that indicate the range of each multi-line comment in the compilation unit.</value>
		/// <remarks>
		/// Do not access this property until parsing is complete.
		/// </remarks>
		public IList MultiLineCommentTextRanges {
			get {
				return multiLineCommentTextRanges;
			}
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
