using System;
using System.Collections.Generic;

namespace ArchAngel.Interfaces.Scripting.NHibernate.Model
{
	[Serializable]
	public class IComponent
	{
		public IComponent()
		{
			Properties = new List<IFieldDef>();
		}

		public string Name { get; set; }
		public List<IFieldDef> Properties { get; set; }
		public IScriptBaseObject ScriptObject { get; set; }
	}
}
