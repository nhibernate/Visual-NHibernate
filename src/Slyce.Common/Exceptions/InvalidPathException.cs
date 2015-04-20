namespace Slyce.Common.Exceptions
{
	public class InvalidPathException : System.IO.FileLoadException
	{
		public InvalidPathException(string message, string path)
			: base(message, path)
		{
		}
	}
}
