using System;
using System.Text;

namespace Slyce.IntelliMerge
{
	public static class StringUtility
	{
		/// <summary>
		/// Delete all forms of linebreak on the end, then add an Environment.NewLine
		/// </summary>
		/// <param name="text">string to process</param>
		public static void RemoveTrailingLineBreaks(ref string text)
		{
			StringBuilder sb = new StringBuilder(text);

			if (sb.Length == 0)
			{
				return;
			}
			string lastChar = sb[sb.Length - 1].ToString();

			while ((lastChar == Environment.NewLine ||
					lastChar == "\r" ||
					lastChar == "\n") &&
				   sb.Length > 0)
			{
				sb.Remove(sb.Length - 1, 1);

				if (sb.Length > 0)
				{
					lastChar = sb[sb.Length - 1].ToString();
				}
			}
			if (sb.Length > 0)
			{

				sb.Append(Environment.NewLine);
			}

			text = sb.ToString();
		}
	}
}
