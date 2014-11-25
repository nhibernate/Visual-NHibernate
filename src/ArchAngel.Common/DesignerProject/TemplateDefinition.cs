using System.Collections.Generic;

namespace ArchAngel.Common.DesignerProject
{
	public class TemplateDefinition
	{
		public readonly string Path;
		public readonly string Name;
		public readonly string Description;
		public readonly List<string> Providers = new List<string>();

		public TemplateDefinition(string name, string description, string path, List<string> providers)
		{
			Name = name;
			Description = description;
			Path = path;
			Providers = providers;
		}
	}
}
