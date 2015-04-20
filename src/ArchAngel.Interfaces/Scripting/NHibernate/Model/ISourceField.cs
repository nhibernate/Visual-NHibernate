using System.Collections.Generic;

namespace ArchAngel.Interfaces.Scripting.NHibernate.Model
{
	public class ISourceField
	{
		public ISourceField()
		{
			PreviousNames = new List<string>();
			Modifiers = new List<string>();
		}

		public string Name { get; set; }
		public string Type { get; set; }
		public string InitialValue { get; set; }
		public List<string> PreviousNames { get; set; }
		public List<string> Modifiers { get; set; }
	}
}
