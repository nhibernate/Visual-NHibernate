
namespace ArchAngel.Interfaces.ProjectOptions.TypeMappings
{
	public class DatabaseTypeMap
	{
		public DatabaseTypeMap(string typeName, DotNetType dotNetType)
		{
			TypeName = typeName;
			DotNetType = dotNetType;
			OringinalTypeName = TypeName;
			OriginalDotNetType = DotNetType;
		}

		public string TypeName;
		public DotNetType DotNetType;
		public string OringinalTypeName;
		public DotNetType OriginalDotNetType;

		public bool IsModified
		{
			get { return TypeName != OringinalTypeName || DotNetType != OriginalDotNetType; }
		}
	}
}
