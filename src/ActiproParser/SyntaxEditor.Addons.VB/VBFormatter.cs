using System;
using System.Collections;
using System.Diagnostics;
using System.Text;

namespace ActiproSoftware.SyntaxEditor.Addons.VB {
	
	/// <summary>
	/// Provides token-based formatting routines for the the <c>Visual Basic</c> language.
	/// </summary>
	internal class VBFormatter {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Auto-indents line-starting text before the specified offset.
		/// </summary>
		/// <param name="document">The <see cref="Document"/> to examine.</param>
		/// <param name="offset">The offset that the line-starting text ends at.</param>
		/// <param name="lineStartText">The line-starting text.</param>
		internal static void AutoIndent(Document document, int offset, string lineStartText) {
			if (offset > 0) {
				int documentLineIndex = document.Lines.IndexOf(offset);
				TextRange textRange = new TextRange(document.Lines[documentLineIndex].StartOffset, offset);

				// If the text range is not read-only and the line-starting text matches...
				if (!document.IsTextRangeReadOnly(textRange)) {
					string trimmedText = document.GetSubstring(textRange).Trim();
					if ((trimmedText.Length == 0) || (trimmedText == lineStartText)) {
						// Auto-indent the brace
						int tabStopLevel = VBFormatter.GetIndentationForOffset(document, offset - lineStartText.Length);
						if (document.Lines[documentLineIndex].TabStopLevel != tabStopLevel)
							document.Lines[documentLineIndex].TabStopLevel = tabStopLevel;
					}
				}
			}
		}

		/// <summary>
		/// Returns the desired line indentation of the specified offset.
		/// </summary>
		/// <param name="document">The <see cref="Document"/> to examine.</param>
		/// <param name="offset">The offset that requires desired indentation information.</param>
		/// <returns>The desired line indentation of the specified offset.</returns>
		internal static int GetIndentationForOffset(Document document, int offset) {
			// Get a text stream
			TextStream stream = document.GetTextStream(offset);
			int originalDocumentLineIndex = stream.DocumentLineIndex;
			if (originalDocumentLineIndex <= 0)
				return 0;

			// Ensure we are at the start of the current token
			if (!stream.IsAtTokenStart)
				stream.GoToCurrentTokenStart();

			// Default to the tabstop level of the previous line
			int tabStopLevel = document.Lines[originalDocumentLineIndex - 1].TabStopLevel;

			bool isForEnd = (stream.Token.ID == VBTokenID.End);
			while (stream.Offset > 0) {
				// Move to the previous statement terminator
				int terminatorOffset = stream.Offset;
				if (!stream.GoToPreviousTokenWithID(new int[] { VBTokenID.LineTerminator, VBTokenID.Comma }))
					break;

				// Examine the line
				int startOffset = stream.Offset;
				bool outdentBefore;
				bool indentAfter;
				int significantOffset;
				if (VBFormatter.ParseLine(stream, terminatorOffset, out significantOffset, out outdentBefore, out indentAfter)) {
					tabStopLevel = document.Lines[document.Lines.IndexOf(significantOffset)].TabStopLevel + (outdentBefore ? -1 : 0) + (indentAfter ? 1 : 0);
					break;
				}

				// Move back to the start offset
				stream.Offset = startOffset;
			}

			return Math.Max(0, tabStopLevel - (isForEnd ? 1 : 0));
		}

		/// <summary>
		/// Returns whether the specified token ID is a block start.
		/// </summary>
		/// <param name="tokenID">The ID of the token to examine.</param>
		/// <returns>
		/// <c>true</c> if the specified token ID is a block start; otherwise, <c>false</c>.
		/// </returns>
		private static bool IsBlockEnd(int tokenID) {
			switch (tokenID) {
				case VBTokenID.Case:
				case VBTokenID.Catch:
				case VBTokenID.Else:
				case VBTokenID.ElseIf:
				case VBTokenID.End:  // Shouldn't have a line terminator after though
				case VBTokenID.Finally:
				case VBTokenID.Loop:
				case VBTokenID.Next:
					return true;
				default:
					return false;
			}
		}

		/// <summary>
		/// Returns whether the specified token ID is a block start.
		/// </summary>
		/// <param name="tokenID">The ID of the token to examine.</param>
		/// <returns>
		/// <c>true</c> if the specified token ID is a block start; otherwise, <c>false</c>.
		/// </returns>
		private static bool IsBlockStart(int tokenID) {
			switch (tokenID) {
				// Keywords that match with 'End'
				case VBTokenID.AddHandler:
				case VBTokenID.Class:
				case VBTokenID.Enum:
				case VBTokenID.Event:
				case VBTokenID.Function:
				case VBTokenID.Get:
				case VBTokenID.If:
				case VBTokenID.Interface:
				case VBTokenID.Module:
				case VBTokenID.Namespace:
				case VBTokenID.Operator:
				case VBTokenID.Property:
				case VBTokenID.RaiseEvent:
				case VBTokenID.RemoveHandler:
				case VBTokenID.Select:
				case VBTokenID.Set:
				case VBTokenID.Structure:
				case VBTokenID.Sub:
				case VBTokenID.SyncLock:
				case VBTokenID.Try:
				case VBTokenID.While:
				case VBTokenID.With:
				// Other keywords
				case VBTokenID.Case:
				case VBTokenID.Catch:
				case VBTokenID.Do:
				case VBTokenID.Else:
				case VBTokenID.ElseIf:
				case VBTokenID.Finally:
				case VBTokenID.For:
					return true;
				default:
					return false;
			}
		}
		
		/// <summary>
		/// Parses a range of text between statement terminators.
		/// </summary>
		/// <param name="stream">The <see cref="TextStream"/> to examine.</param>
		/// <param name="terminatorOffset">The offset at which to stop searching.</param>
		/// <param name="significantOffset">Returns the significant token's start offset.</param>
		/// <param name="outdentBefore">Returns whether to outdent before the line.</param>
		/// <param name="indentAfter">Returns whether to indent after the line.</param>
		/// <returns>
		/// <c>true</c> if data was found; otherwise, <c>false</c>.
		/// </returns>
		private static bool ParseLine(TextStream stream, int terminatorOffset, out int significantOffset, out bool outdentBefore, out bool indentAfter) {
			// Initialize output variables
			significantOffset = stream.Offset;
			outdentBefore = false;
			indentAfter = false;

			// Assume we start on a statement terminator so skip over it
			if (stream.Offset > 0)
				stream.ReadToken();

			while (stream.Offset < terminatorOffset) {
				switch (stream.Token.ID) {
					case VBTokenID.LineContinuation:
					case VBTokenID.Whitespace:
						// Skip
						break;
					default:
						if (VBToken.IsKeyword(stream.Token.ID)) {
							if (VBToken.IsModifier(stream.Token.ID)) {
								// Skip modifiers
								break;
							}
							outdentBefore = VBFormatter.IsBlockEnd(stream.Token.ID);
							indentAfter = VBFormatter.IsBlockStart(stream.Token.ID);
						}
						significantOffset = stream.Offset;
						return true;
				}

				// Read the next token
				stream.ReadToken();
			}

			return false;
		}

	}
}

