
namespace ArchAngel.Interfaces
{
	public class GeneratedFile
	{
		private string _RawFilename;
		private string _Filename;
		private string _RelativePath;
		private string _ScriptName = string.Empty;
		private string _IteratorTypeName;

		public GeneratedFile(string rawFileName, string filename, string relativePath, string scriptName, string iteratorTypeName)
		{
			RawFilename = rawFileName;
			Filename = filename;
			RelativePath = relativePath;
			ScriptName = scriptName;
			IteratorTypeName = iteratorTypeName;
		}

		public string RawFilename
		{
			get { return _RawFilename; }
			set { _RawFilename = value; }
		}

		public string Filename
		{
			get { return _Filename; }
			set { _Filename = value; }
		}

		public string IteratorTypeName
		{
			get { return _IteratorTypeName; }
			set { _IteratorTypeName = value; }
		}

		public string RelativePath
		{
			get { return _RelativePath; }
			set { _RelativePath = value; }
		}

		public string ScriptName
		{
			get { return _ScriptName; }
			set { _ScriptName = value; }
		}

		public bool Equals(GeneratedFile obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj._RawFilename, _RawFilename) && Equals(obj._Filename, _Filename) && Equals(obj._RelativePath, _RelativePath) && Equals(obj._ScriptName, _ScriptName) && Equals(obj._IteratorTypeName, _IteratorTypeName);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (GeneratedFile)) return false;
			return Equals((GeneratedFile) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int result = (_RawFilename != null ? _RawFilename.GetHashCode() : 0);
				result = (result*397) ^ (_Filename != null ? _Filename.GetHashCode() : 0);
				result = (result*397) ^ (_RelativePath != null ? _RelativePath.GetHashCode() : 0);
				result = (result*397) ^ (_ScriptName != null ? _ScriptName.GetHashCode() : 0);
				result = (result*397) ^ (_IteratorTypeName != null ? _IteratorTypeName.GetHashCode() : 0);
				return result;
			}
		}
	}
}
