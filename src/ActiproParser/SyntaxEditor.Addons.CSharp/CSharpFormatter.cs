using System;
using System.Collections;
using System.Diagnostics;
using System.Text;

namespace ActiproSoftware.SyntaxEditor.Addons.CSharp {
	
	/// <summary>
	/// Provides token-based formatting routines for the the <c>C#</c> language.
	/// </summary>
	internal class CSharpFormatter {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Auto-indents line-starting text before the specified offset.
		/// </summary>
		/// <param name="options">The <see cref="CSharpFormattingOptions"/> to use.</param>
		/// <param name="document">The <see cref="Document"/> to examine.</param>
		/// <param name="offset">The offset that the line-starting text ends at.</param>
		/// <param name="lineStartText">The line-starting text.</param>
		internal static void AutoIndent(CSharpFormattingOptions options, Document document, int offset, string lineStartText) {
			if (offset > 0) {
				int documentLineIndex = document.Lines.IndexOf(offset);
				TextRange textRange = new TextRange(document.Lines[documentLineIndex].StartOffset, offset);

				// If the text range is not read-only and the line-starting text matches...
				if (!document.IsTextRangeReadOnly(textRange)) {
					string trimmedText = document.GetSubstring(textRange).Trim();
					if ((trimmedText.Length == 0) || (trimmedText == lineStartText)) {
						// Auto-indent the brace
						int tabStopLevel = CSharpFormatter.GetIndentationForOffset(options, document, offset - lineStartText.Length);
						if (document.Lines[documentLineIndex].TabStopLevel != tabStopLevel)
							document.Lines[documentLineIndex].TabStopLevel = tabStopLevel;
					}
				}
			}
		}

		/// <summary>
		/// Returns the desired line indentation of the specified offset.
		/// </summary>
		/// <param name="options">The <see cref="CSharpFormattingOptions"/> to use.</param>
		/// <param name="document">The <see cref="Document"/> to examine.</param>
		/// <param name="offset">The offset that requires desired indentation information.</param>
		/// <returns>The desired line indentation of the specified offset.</returns>
		internal static int GetIndentationForOffset(CSharpFormattingOptions options, Document document, int offset) {
			// Get a text stream
			TextStream stream = document.GetTextStream(offset);

			// Get the indentation base line
			int indentationBaseLineIndex = Math.Max(0, stream.DocumentLineIndex - 1);

			// If in a verbatim string, force the indentation to the line start
			if ((stream.Token.ID == CSharpTokenID.VerbatimStringLiteral) && (!stream.IsAtTokenStart))
				return 0;

			// Ensure we are at the start of the current token
			if (!stream.IsAtTokenStart)
				stream.GoToCurrentTokenStart();

			// If finding indentation for an open curly brace, move back a token
			bool isForOpenCurlyBrace = (stream.Token.ID == CSharpTokenID.OpenCurlyBrace);
			if (isForOpenCurlyBrace)
				stream.GoToPreviousToken();

			// Loop backwards
			bool colonFound = false;
			bool isForCloseCurlyBrace = (stream.Token.ID == CSharpTokenID.CloseCurlyBrace);
			bool keywordFoundAfterStatement = false;
			bool statementFound = false;
			while (true) {
				switch (stream.Token.ID) {
					case CSharpTokenID.OpenCurlyBrace: {
						// Get the indent level of the {
						int tabStopLevel = stream.DocumentLine.TabStopLevel;

						if (isForOpenCurlyBrace) {
							// Indenting an open curly brace so indent it based on the last open curly brace
							return tabStopLevel + (options.IndentOpenAndCloseBraces ? 2 : 1);
						}
						else if (!options.IndentOpenAndCloseBraces) {
							// If block contents should be indented...
							if (options.IndentBlockContents)
								return tabStopLevel + 1 + (keywordFoundAfterStatement ? 1 : 0);
						
							// Block content indenting is off so see if the curly brace is for a namespace or type declaration
							return tabStopLevel + (CSharpFormatter.IsNamespaceOrTypeDeclarationHeader(stream) ? 1 : 0);
						}
						else {
							// Indent level should match brace level
							return tabStopLevel + (keywordFoundAfterStatement ? 1 : 0);
						}
					}
					case CSharpTokenID.CloseCurlyBrace:
						// Get the indent level of the matching {
						stream.GoToPreviousMatchingToken(stream.Token);
						if ((isForOpenCurlyBrace) || (isForCloseCurlyBrace)) {
							// Indenting a curly brace so indent it based on the previous open curly brace at the same level
							return stream.DocumentLine.TabStopLevel;
						}
						else {
							// Indent so that if indenting open and close braces option is set, this will move back out a level
							return stream.DocumentLine.TabStopLevel + (keywordFoundAfterStatement ? 1 : 0) - (options.IndentOpenAndCloseBraces ? 1 : 0);
						}
					case CSharpTokenID.CloseParenthesis:
					case CSharpTokenID.CloseSquareBrace:
						// Skip over () and []
						stream.GoToPreviousMatchingToken(stream.Token);
						break;
					case CSharpTokenID.SemiColon:
						if (!statementFound) {
							// Flag that a statement was found
							statementFound = true;

							if (!keywordFoundAfterStatement) {
								// Use this line as indentation base
								indentationBaseLineIndex = stream.DocumentLineIndex;
							}
						}
						break;
					case CSharpTokenID.Colon:
						// Flag that a colon was found
						colonFound = true;
						break;
					case CSharpTokenID.Case:
					case CSharpTokenID.Default:
						if (stream.Token.ID == CSharpTokenID.Default) {
							// See if the "default" is followed by a colon, indicating a "switch" statement block
							stream.ReadToken();
							bool isDefaultColon = (stream.PeekToken().ID == CSharpTokenID.Colon);
							stream.ReadTokenReverse();

							if (!isDefaultColon)
								break;
						}

						// Process the "case" and "default" keywords... look at parent "switch" level
						stream.GoToPreviousTokenWithID(CSharpTokenID.Switch);
						return stream.DocumentLine.TabStopLevel + ((options.IndentCaseLabels) && (!isForOpenCurlyBrace) ? 1 : 0) + 
							((colonFound) && (options.IndentCaseContents) ? 1 : 0);
					case CSharpTokenID.Do:
					case CSharpTokenID.For:
					case CSharpTokenID.ForEach:
					case CSharpTokenID.If:
					case CSharpTokenID.Lock:
					case CSharpTokenID.Using:
					case CSharpTokenID.While:
						// Process single-child statement keywords
						if (isForOpenCurlyBrace) {
							// Indent open brace based on the keyword indent level
							return stream.DocumentLine.TabStopLevel + (options.IndentOpenAndCloseBraces ? 1 : 0);
						}
						else if (stream.Offset < offset) {
							// Indent a level if on the statement after the keyword
							return stream.DocumentLine.TabStopLevel + ((statementFound) && (!keywordFoundAfterStatement) ? 0 : 1);
						}
						break;
					default:
						if ((!keywordFoundAfterStatement) && (!statementFound) && (stream.Offset < offset) && 
							(stream.Token.ID > CSharpTokenID.KeywordStart) && (stream.Token.ID < CSharpTokenID.KeywordEnd)) {
							// Flag that a keyword was found
							keywordFoundAfterStatement = true;

							// Use this line as indentation base
							indentationBaseLineIndex = stream.DocumentLineIndex;
						}
						break;
				}

				// Go to the previous token
				if (!stream.GoToPreviousToken())
					break;
			}

			if (isForOpenCurlyBrace) {
				// Indent open brace based on the keyword indent level
				return document.Lines[Math.Max(0, indentationBaseLineIndex)].TabStopLevel + (options.IndentOpenAndCloseBraces ? 1 : 0);
			}
			else {
				// Indent a level if on the statement after the keyword
				return document.Lines[Math.Max(0, indentationBaseLineIndex)].TabStopLevel + (keywordFoundAfterStatement ? 1 : 0);
			}
		}

		/// <summary>
		/// Returns whether the <see cref="TextStream"/> is in the header of a namespace or type declaration.
		/// </summary>
		/// <param name="stream">The <see cref="TextStream"/> to examine.</param>
		/// <returns>
		/// <c>true</c> if the <see cref="TextStream"/> is in the header of a namespace or type declaration; otherwise, <c>false</c>.
		/// </returns>
		private static bool IsNamespaceOrTypeDeclarationHeader(TextStream stream) {
			// Skip the { that the stream should be sitting at
			stream.GoToPreviousToken();

			while (!stream.IsAtDocumentStart) {
				switch (stream.Token.ID) {
					case CSharpTokenID.Colon:
					case CSharpTokenID.Comma:
					case CSharpTokenID.Dot:
					case CSharpTokenID.Identifier:
					case CSharpTokenID.LineTerminator:
					case CSharpTokenID.MultiLineComment:
					case CSharpTokenID.SingleLineComment:
					case CSharpTokenID.Whitespace:
						// Do nothing
						break;
					case CSharpTokenID.Class:
					case CSharpTokenID.Interface:
					case CSharpTokenID.Event:
					case CSharpTokenID.Namespace:
					case CSharpTokenID.Struct:
						return true;
					default:
						if (CSharpToken.IsNativeType(stream.Token.ID))
							break;
						else
							return false;
				}
				stream.GoToPreviousToken();
			}
			return false;
		}

	}
}

