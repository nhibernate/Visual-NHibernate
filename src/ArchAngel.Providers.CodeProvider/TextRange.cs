namespace ArchAngel.Providers.CodeProvider
{
	public class TextRange
	{
		private int _StartOffset;
		private int _EndOffset;

		public TextRange(int startOffset, int endOffset)
		{
			_StartOffset = startOffset;
			_EndOffset = endOffset;
		}

		public int StartOffset
		{
			get { return _StartOffset; }
			set { _StartOffset = value; }
		}

		public int EndOffset
		{
			get { return _EndOffset; }
			set { _EndOffset = value; }
		}
	}
}