using System.Collections.Generic;

namespace ArchAngel.Interfaces.Scripting.NHibernate.Model
{
	public class ISourceProperty
	{
		public class Accessor
		{
			public string Modifier { get; set; }
			public string Body { get; set; }
		}

		public ISourceProperty()
		{
			PreviousNames = new List<string>();
			Modifiers = new List<string>();
		}

		public string Name { get; set; }
		public string Type { get; set; }
		public List<string> PreviousNames { get; set; }
		public List<string> Modifiers { get; set; }
		public Accessor GetAccessor { get; set; }
		public Accessor SetAccessor { get; set; }
	}
}
