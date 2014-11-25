
using System;
using System.Text;
namespace ArchAngel.Providers.CodeProvider
{
	internal static class Helper
	{
		public enum LineBreaks
		{
			/// <summary>
			/// "\r\n"
			/// </summary>
			Windows,
			/// <summary>
			/// "\n"
			/// </summary>
			Unix,
			/// <summary>
			/// "\r"
			/// </summary>
			Mac
		}

		public static string StandardizeLineBreaks(string text, LineBreaks lineBreak)
		{
			switch (lineBreak)
			{
				case LineBreaks.Mac:
					return StandardizeLineBreaks(text, "\r");
				case LineBreaks.Unix:
					return StandardizeLineBreaks(text, "\n");
				case LineBreaks.Windows:
					return StandardizeLineBreaks(text, "\r\n");
				default:
					throw new NotImplementedException("This LineBreak hasn't been coded yet.");
			}
		}

		public static string StandardizeLineBreaks(string text, string lineBreak)
		{
			if (text == null) { return text; }

			StringBuilder sb = new StringBuilder(text, text.Length + 1000);
			sb.Replace("\r\n", "\n").Replace("\r", "\n");

			if (lineBreak != "\n")
			{
				sb.Replace("\n", lineBreak);
			}
			return sb.ToString();
		}

		public static string RemoveTrailingLineBreaks(string text)
		{
			if (text.Length == 0)
			{
				return text;
			}
			string lastChar = text.Substring(text.Length - 1, 1);

			while (lastChar == "\r" || lastChar == "\n" || lastChar == "\t")
			{
				text = text.Remove(text.Length - 1, 1);
				lastChar = text.Substring(text.Length - 1, 1);
			}
			return text;
		}

		/// <summary>
		/// Gets the number of lines appearing in the text.
		/// </summary>
		/// <param name="text">Text to check.</param>
		/// <returns>Number of lines.</returns>
		public static int GetNumberOfLines(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return 0;
			}
			int numLines = 0;
			int index = 0;

			// Check for Windows and Unix style linebreaks first
			while ((index = text.IndexOf('\n', index)) >= 0)
			{
				numLines++;
				index += 1;
			}
			if (numLines == 0)
			{
				index = 0;
				// Maybe we are dealing with Mac style linebreaks, so check for those
				while ((index = text.IndexOf('\r', index)) >= 0)
				{
					numLines++;
					index += 1;
				}
			}
			return numLines;
		}
	}
}
