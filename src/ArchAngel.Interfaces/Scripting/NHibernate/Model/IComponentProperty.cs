using System;
using System.Collections.Generic;

namespace ArchAngel.Interfaces.Scripting.NHibernate.Model
{
	[Serializable]
	public class IComponentProperty
	{
		public IComponentProperty()
		{
			Properties = new List<IField>();
		}

		public string Name { get; set; }
		public string Type { get; set; }
		public bool IsSetterPrivate { get; set; }
		public List<IField> Properties { get; set; }
		public IScriptBaseObject ScriptObject { get; set; }
	}
}
