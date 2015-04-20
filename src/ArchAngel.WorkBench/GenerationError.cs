namespace ArchAngel.Workbench
{
    public class GenerationError
    {
        public string FileName;
        public string ErrorDescription;

        public GenerationError(string fileName, string errorDescription)
        {
            FileName = fileName;
            ErrorDescription = errorDescription;
        }
    }

	public class MergeError
	{
		public readonly string BaseConstructName;
		public readonly string BaseConstructType;
        public readonly string ErrorDescription;

		public MergeError(string baseConstructName, string baseConstructType, string errorDescription)
        {
			BaseConstructName = baseConstructName;
			BaseConstructType = baseConstructType;
            ErrorDescription = errorDescription;
        }
	}
}
