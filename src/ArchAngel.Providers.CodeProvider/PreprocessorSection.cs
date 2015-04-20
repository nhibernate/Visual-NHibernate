using System.Collections;

namespace ArchAngel.Providers
{
	/// <summary>
	/// Summary description for PreprocessorSection.
	/// </summary>
	public class PreprocessorSection
	{
		public int StartLine = 0;
		public int EndLine = 0;
		private ArrayList m_elseLines = new ArrayList();
		public ArrayList FormattedCode = new ArrayList();
		public string PrePPFormattedCode = "";
		public ArrayList BodySections = new ArrayList();

		public void AddElseLine(int line)
		{
			m_elseLines.Add(line);
		}

		public int[] ElseLineNumbers
		{
			get { return (int[])m_elseLines.ToArray(typeof(int)); }
		}
	}

	/// <summary>
	/// Summary description for PreprocessorSection.
	/// </summary>
	public class BodySection
	{
		public int StartLine = 0;
		public int EndLine = 0;
		public ArrayList FormattedCode = new ArrayList();
		public ArrayList ChildSections = new ArrayList();
	}
}
