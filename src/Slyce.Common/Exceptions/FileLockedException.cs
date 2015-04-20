namespace Slyce.Common.Exceptions
{
    public class FileLockedException : System.IO.FileLoadException
    {
        public FileLockedException(string message, string filename)
            : base(message, filename)
        {
        }
    }
}
