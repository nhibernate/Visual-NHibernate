using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArchAngel.Providers.EntityModel.Model.EntityLayer
{
	public class EntityGenerator : IEntityGenerator
	{
		public class Parameter// : IEntityGeneratorParameter
		{
			public string Name { get; set; }
			public string Value { get; set; }

			public Parameter(string name, string value)
			{
				Name = name;
				Value = value;
			}
		}

		public EntityGenerator(string className)
		{
			ClassName = className;
			Parameters = new List<Parameter>();
		}

		public string ClassName { get; set; }
		public IList<Parameter> Parameters { get; set; }
	}
}
