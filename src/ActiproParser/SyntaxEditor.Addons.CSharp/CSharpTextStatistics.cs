using System;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom;

namespace ActiproSoftware.SyntaxEditor.Addons.CSharp {

	/// <summary>
	/// Provides numerous statistics for some <c>C#</c> code.
	/// </summary>
	public class CSharpTextStatistics : DotNetTextStatistics {

		private	int		commentLines;
		private	int		pureCommentLines;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>CSharpTextStatistics</c> class. 
		/// </summary>
		/// <remarks>
		/// The <see cref="CalculateStatistics"/> method should be called manually to populate the statistics data.
		/// </remarks>
		public CSharpTextStatistics() : base() {}
		
		/// <summary>
		/// Initializes a new instance of the <c>CSharpTextStatistics</c> class. 
		/// </summary>
		/// <param name="text">The text to examine and create statistics data.</param>
		public CSharpTextStatistics(string text) : base(text) {}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Calculates statistics for the specified text.
		/// </summary>
		/// <param name="text">The text to score.</param>
		public override void CalculateStatistics(string text) {
			// Call the base method
			base.CalculateStatistics(text);

			// Calculate multiple line comments
			string multiLineCommentPattern = @"^.*(?<!//.*)(?<Comment>/\*([^\*]|(\*(?!/)))*\*/)";
			MatchCollection commentMatches = Regex.Matches(text, multiLineCommentPattern, RegexOptions.Multiline);
			commentLines = 0;
			pureCommentLines = 0;
			int lastCommentEndOffset = -1;
			foreach (Match commentMatch in commentMatches) {
				Capture comment = commentMatch.Groups["Comment"].Captures[0];

				bool onSameLineAsLastComment = false;
				if ((lastCommentEndOffset != -1) && (comment.Index > lastCommentEndOffset)) {
					// Check to see if this is another comment on the same line as the old one
					onSameLineAsLastComment = (text.Substring(lastCommentEndOffset, comment.Index - lastCommentEndOffset).IndexOf('\n') == -1);
				}

				// Update the number of comment lines
				int singleCommentLines = Regex.Matches(comment.Value, @"\n", RegexOptions.Multiline).Count + 1;
				commentLines += (singleCommentLines - (onSameLineAsLastComment ? 1 : 0));

				// Update the number of pure comment lines
				TextRange lineTextRange = this.GetLineTextRange(text, comment.Index);
				bool startIsPure = (Regex.Matches(text.Substring(lineTextRange.StartOffset, comment.Index - lineTextRange.StartOffset + 2), @"^(\s){" + (comment.Index - lineTextRange.StartOffset) + @"}", RegexOptions.Multiline).Count == 1);
				lineTextRange = this.GetLineTextRange(text, comment.Index + comment.Length);
				bool endIsPure = (Regex.Matches(text.Substring(comment.Index + comment.Length, lineTextRange.EndOffset - (comment.Index + comment.Length)), @"(\s){" + (lineTextRange.EndOffset - (comment.Index + comment.Length)) + @"}$", RegexOptions.Multiline).Count == 1);
				if (onSameLineAsLastComment)
					pureCommentLines += (startIsPure && endIsPure ? 1 : 0);
				else
					pureCommentLines += (singleCommentLines - 2 + (startIsPure ? 1 : 0) + (endIsPure ? 1 : 0));

				// Save the end offset
				lastCommentEndOffset = comment.Index + comment.Length;
			}

			// Strip off multi-line comments
			text = Regex.Replace(text, multiLineCommentPattern, String.Empty, RegexOptions.Multiline);

			// Calculate single line comment 
			commentLines += Regex.Matches(text, @"^.*//", RegexOptions.Multiline).Count;
			pureCommentLines += Regex.Matches(text, @"^(\s)*//", RegexOptions.Multiline).Count;
		}

		/// <summary>
		/// Gets the number of lines in the text that contain comments.
		/// </summary>
		/// <value>The number of lines in the text that contain comments.</value>
		public override int CommentLines {
			get {
				return commentLines;
			}
		}

		/// <summary>
		/// Gets the number of lines in the text that are purely comments.
		/// </summary>
		/// <value>The number of lines in the text that are purely comments.</value>
		public override int PureCommentLines {
			get {
				return pureCommentLines;
			}
		}

	}

}
