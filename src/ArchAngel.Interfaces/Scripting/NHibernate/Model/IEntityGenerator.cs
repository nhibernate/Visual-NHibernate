using System.Collections.Generic;

namespace ArchAngel.Interfaces.Scripting.NHibernate.Model
{
	public class IEntityGenerator
	{
		public class IParameter
		{
			public string Name { get; set; }
			public string Value { get; set; }

			public IParameter(string name, string value)
			{
				Name = name;
				Value = value;
			}
		}

		public IEntityGenerator(string className)
		{
			ClassName = className;
			Parameters = new List<IParameter>();
		}

		public string ClassName { get; set; }
		public IList<IParameter> Parameters { get; set; }
	}
}
