
namespace ArchAngel.Workbench.ProjectOptions.TypeMappings
{
	public class DatabaseTypeMap
	{
		public DatabaseTypeMap(string typeName, UniType uniType)
		{
			TypeName = typeName;
			UniType = uniType;
			OringinalTypeName = TypeName;
			OriginalUniType = UniType;
		}

		public string TypeName;
		public UniType UniType;
		public string OringinalTypeName;
		public UniType OriginalUniType;

		public bool IsModified
		{
			get { return TypeName != OringinalTypeName || UniType != OriginalUniType; }
		}
	}
}
