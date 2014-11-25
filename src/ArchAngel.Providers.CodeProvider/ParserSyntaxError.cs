using System;
namespace ArchAngel.Providers.CodeProvider
{
    /// <summary>
    /// Represents a single error that occured during the parsing of source code.
    /// </summary>
	[Serializable]
    public class ParserSyntaxError
    {
        /// <summary>
        /// The error message returned from the parser.
        /// </summary>
        private readonly string errorMessage;
        /// <summary>
        /// The start offset of the error in the parsed code. -1 if the source of the error could not be found.
        /// </summary>
        private readonly int startOffset;
        /// <summary>
        /// The length of the error in the parsed code. 0 if the source of the error could not be found.
        /// </summary>
        private readonly int length;
		/// <summary>
		/// The line number of the error.
		/// </summary>
    	private readonly int lineNumber;
    	/// <summary>
    	/// The name of the file that the error occurred in.
    	/// </summary>
    	private readonly string filename;
        /// <summary>
        /// The exact text that the error occurred in.
        /// </summary>
        private readonly string errorText;

        public ParserSyntaxError(string errorMessage, int startOffset, int length, int lineNumber, string filename, string errorText)
        {
            this.errorMessage = errorMessage;
            this.errorText = errorText;
            this.filename = filename;
        	this.lineNumber = lineNumber;
        	this.startOffset = startOffset;
            this.length = length;
        }

        /// <summary>
        /// The exact text that the error occurred in.
        /// </summary>
        public string ErrorText
        {
            get { return errorText; }
        }

        /// <summary>
		/// The name of the file that the error occurred in.
		/// </summary>
    	public string Filename
    	{
    		get { return filename; }
    	}

    	/// <summary>
		/// The line number of the error.
		/// </summary>
    	public int LineNumber
    	{
    		get { return lineNumber; }
    	}

    	/// <summary>
        /// The error message returned from the parser.
        /// </summary>
        public string ErrorMessage
        {
            get { return errorMessage; }
        }
        /// <summary>
        /// The start offset of the error in the parsed code. -1 if the source of the error could not be found.
        /// </summary>
        public int StartOffset
        {
            get { return startOffset; }
        }
        /// <summary>
        /// The length of the error in the parsed code. 0 if the source of the error could not be found.
        /// </summary>
        public int Length
        {
            get { return length; }
        }

    	public override string ToString()
    	{
    		return "("+StartOffset + ", " + Length + "): " + ErrorMessage;
    	}
    }
}
