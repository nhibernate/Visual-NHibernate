using System;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom {

	/// <summary>
	/// Provides an abstract base class with numerous statistics for .NET code.
	/// </summary>
	public abstract class DotNetTextStatistics : TextStatistics {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>DotNetTextStatistics</c> class. 
		/// </summary>
		/// <remarks>
		/// The <see cref="TextStatistics.CalculateStatistics"/> method should be called manually to populate the statistics data.
		/// </remarks>
		public DotNetTextStatistics() : base() {}
		
		/// <summary>
		/// Initializes a new instance of the <c>DotNetTextStatistics</c> class. 
		/// </summary>
		/// <param name="text">The text to examine and create statistics data.</param>
		public DotNetTextStatistics(string text) : base(text) {}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Adds the appropriate statistics to the specified <see cref="ListView"/>.
		/// </summary>
		/// <param name="listView">The <see cref="ListView"/> of which to add the statistics.</param>
		public override void AddStatisticsToListView(ListView listView) {
			ActiproSoftware.Products.SyntaxEditor.Resources resources = ActiproSoftware.Products.SyntaxEditor.AssemblyInfo.Instance.Resources;

			listView.Items.Add(new ListViewItem(new string[] { resources.GetString("TextStatisticsForm_Statistic_Lines"), this.Lines.ToString() }));
			listView.Items.Add(new ListViewItem(new string[] { resources.GetString("TextStatisticsForm_Statistic_NonWhitespaceLines"), (this.Lines - this.WhitespaceLines ).ToString() }));
			listView.Items.Add(new ListViewItem(new string[] { resources.GetString("TextStatisticsForm_Statistic_CommentLines"), this.CommentLines.ToString() }));
			listView.Items.Add(new ListViewItem(new string[] { resources.GetString("TextStatisticsForm_Statistic_PureCommentLines"), this.PureCommentLines.ToString() }));
			listView.Items.Add(new ListViewItem(new string[] { resources.GetString("TextStatisticsForm_Statistic_CommentLineCoveragePercentage"), this.CommentLineCoveragePercentage.ToString("##0.0%") }));
			listView.Items.Add(new ListViewItem(new string[] { resources.GetString("TextStatisticsForm_Statistic_Characters"), this.Characters.ToString() }));
			listView.Items.Add(new ListViewItem(new string[] { resources.GetString("TextStatisticsForm_Statistic_NonWhitespaceCharacters"), (this.Characters - this.WhitespaceCharacters).ToString() }));
		}

		/// <summary>
		/// Gets the number of lines in the text that contain code.
		/// </summary>
		/// <value>The number of lines in the text that contain code.</value>
		public int CodeLines {
			get {
				return this.Lines - this.WhitespaceLines - this.PureCommentLines;
			}
		}
		
		/// <summary>
		/// Gets the average number of letters per word.
		/// </summary>
		/// <value>The average number of letters per word.</value>
		public float CommentLineCoveragePercentage {
			get {
				return Math.Min(999f, (float)this.CommentLines / (this.NonWhitespaceLines == 0 ? 1 : this.NonWhitespaceLines));
			}
		}

		/// <summary>
		/// Gets the number of lines in the text that contain comments.
		/// </summary>
		/// <value>The number of lines in the text that contain comments.</value>
		public abstract int CommentLines { get; }

		/// <summary>
		/// Gets the number of lines in the text that are purely comments.
		/// </summary>
		/// <value>The number of lines in the text that are purely comments.</value>
		public abstract int PureCommentLines { get; }

	}

}
