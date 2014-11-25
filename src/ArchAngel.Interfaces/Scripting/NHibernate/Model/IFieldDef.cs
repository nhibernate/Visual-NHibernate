using System;
using System.Collections.Generic;

namespace ArchAngel.Interfaces.Scripting.NHibernate.Model
{
	[Serializable]
	public class IFieldDef
	{
		public string Name { get; set; }
		public string Type { get; set; }
		public bool IsSetterPrivate { get; set; }
		public List<string> PreviousNames { get; set; }
		public IScriptBaseObject ScriptObject { get; set; }
	}
}
