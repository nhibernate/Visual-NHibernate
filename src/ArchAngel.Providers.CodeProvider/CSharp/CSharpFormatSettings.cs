namespace ArchAngel.Providers.CodeProvider.CSharp
{
	/// <summary>
	/// Holds the settings used when formatting code using the CSharpCodeFormatter.
	/// </summary>
	public class CSharpFormatSettings
	{
		/// <summary>
		/// If true, does not output empty statements (;)
		/// </summary>
		public bool OmitEmptyStatements = true;
		/// <summary>
		/// If true, will add braces to all single line blocks - if (true) ; becomes if (true) { ; }
		/// </summary>
		public bool AddBracesToSingleLineBlocks = true;
		/// <summary>
		/// If true, will put single line blocks on the same line as their parent construct:
		/// if (true)
		///     ;
		/// becomes:
		/// if (true) ;
		/// AddBracesToSingleLineBlocks still applies, so if that is set then if will become:
		/// if (true) { ; }
		/// </summary>
		public bool SingleLineBlocksOnSameLineAsParent = false;
		/// <summary>
		/// If true, will put braces on new lines and automatically indent the code inside the block.
		/// if (true) { ; } becomes
		/// if (true)
		/// {
		///     ;
		/// }
		/// 
		/// Otherwise, code will be written like this:
		/// if (true) {
		///     ;
		/// }
		/// </summary>
		public bool PutBracesOnNewLines = true;
		/// <summary>
		/// If true, will make sure that line breaks present in the previous file will be present in this one.
		/// if two functions were defined line so:
		/// public void Function1() { }
		/// 
		/// 
		/// public void Function2() {}
		/// 
		/// then they would be output line this as well.
		/// </summary>
		public bool MaintainWhitespace = true;
		/// <summary>
		/// If true, single line getters and setters will be put on a single line, like so:
		/// get { blah; }
		/// instead of being expanded.
		/// </summary>
		public bool InlineSingleLineGettersAndSetters = true;

		/// <summary>
		/// We set Controller.Reorder to this value.
		/// </summary>
		public bool ReorderBaseConstructs = false;
		/// <summary>
		/// If true, all comments will be block comments.
		/// </summary>
		public bool CommentLinesAsCommentBlock = false;

		///<summary>
		/// Copies the format settings from the given object.
		///</summary>
		///<param name="formatSettings">The object to copy the settings from.</param>
		public void SetFrom(CSharpFormatSettings formatSettings)
		{
			AddBracesToSingleLineBlocks = formatSettings.AddBracesToSingleLineBlocks;
			InlineSingleLineGettersAndSetters = formatSettings.InlineSingleLineGettersAndSetters;
			MaintainWhitespace = formatSettings.MaintainWhitespace;
			OmitEmptyStatements = formatSettings.OmitEmptyStatements;
			PutBracesOnNewLines = formatSettings.PutBracesOnNewLines;
			SingleLineBlocksOnSameLineAsParent = formatSettings.SingleLineBlocksOnSameLineAsParent;
			ReorderBaseConstructs = formatSettings.ReorderBaseConstructs;
			CommentLinesAsCommentBlock = formatSettings.CommentLinesAsCommentBlock;
		}

	}
}