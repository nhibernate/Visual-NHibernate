using System;

namespace ArchAngel.Designer
{
	/// <summary>
	/// Summary description for CompilerError.
	/// </summary>
	[Serializable]
	public class CompilerError
	{
	    public int Line { get; set; }

	    public string File { get; set; }

	    public int Column { get; set; }

	    public string Text { get; set; }

	    public string Number { get; set; }

	    public bool IsWarning { get; set; }

	    public CompilerError()
		{
		}
	
		public CompilerError(System.CodeDom.Compiler.CompilerError error)
		{
            Slyce.Common.Utility.CheckForNulls(new object[] { error }, new[] { "error" });

			Column		= error.Column;
			File		= error.FileName;
			Line		= error.Line;
			Number		= error.ErrorNumber;
			Text		= error.ErrorText;
			IsWarning	= error.IsWarning;
		}
	}
}
