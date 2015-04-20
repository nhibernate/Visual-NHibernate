using System.Text.RegularExpressions;

namespace Slyce.Common
{
	/// <summary>
	/// Helper class for cleaning up XML.
	/// </summary>
	public static class XmlSqueezer
	{
		private static readonly Regex regex = new Regex(@"([\s]*)<", RegexOptions.Compiled);

		public static string RemoveWhitespaceBetweenElements(string xml)
		{
			return regex.Replace(xml, "<");
		}

		public static string RemoveWhitespaceBetweenXmlElements(this string xml)
		{
			return RemoveWhitespaceBetweenElements(xml);
		}
	}
}