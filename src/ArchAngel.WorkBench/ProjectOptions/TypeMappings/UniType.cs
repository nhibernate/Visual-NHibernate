
namespace ArchAngel.Workbench.ProjectOptions.TypeMappings
{
	public class UniType
	{
		public string Name;
		public string CSharpType;
		public string OriginalName;
		public string OriginalCSharpType;

		public UniType(string name, string csharpType)
		{
			Name = name;
			CSharpType = csharpType;
			OriginalName = Name;
			OriginalCSharpType = CSharpType;
		}

		public bool IsModified
		{
			get { return Name != OriginalName || CSharpType != OriginalCSharpType; }
		}
	}
}
