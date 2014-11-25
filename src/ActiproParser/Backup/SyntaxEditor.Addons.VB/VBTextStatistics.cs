using System;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom;

namespace ActiproSoftware.SyntaxEditor.Addons.VB {

	/// <summary>
	/// Provides numerous statistics for some <c>Visual Basic</c> code.
	/// </summary>
	public class VBTextStatistics : DotNetTextStatistics {

		private	int		commentLines;
		private	int		pureCommentLines;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>VBTextStatistics</c> class. 
		/// </summary>
		/// <remarks>
		/// The <see cref="CalculateStatistics"/> method should be called manually to populate the statistics data.
		/// </remarks>
		public VBTextStatistics() : base() {}
		
		/// <summary>
		/// Initializes a new instance of the <c>VBTextStatistics</c> class. 
		/// </summary>
		/// <param name="text">The text to examine and create statistics data.</param>
		public VBTextStatistics(string text) : base(text) {}
		
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

			// Calculate single line comment 
			commentLines += Regex.Matches(text, @"^.*(\'|([Rr][Ee][Mm](?!\w)))", RegexOptions.Multiline).Count;
			pureCommentLines += Regex.Matches(text, @"^(\s)*(\'|([Rr][Ee][Mm](?!\w)))", RegexOptions.Multiline).Count;
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
