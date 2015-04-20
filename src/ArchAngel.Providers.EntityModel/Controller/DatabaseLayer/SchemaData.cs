using System.Collections.Generic;

namespace ArchAngel.Providers.EntityModel.Controller.DatabaseLayer
{
	public class SchemaData
	{
		public SchemaData(string name, IList<string> tableNames, IList<string> viewNames)
		{
			Name = name;
			TableNames = tableNames;
			ViewNames = viewNames;
		}

		public string Name { get; set; }
		public IList<string> TableNames { get; set; }
		public IList<string> ViewNames { get; set; }
	}
}
