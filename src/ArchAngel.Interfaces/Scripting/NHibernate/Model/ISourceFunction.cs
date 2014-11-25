using System.Collections.Generic;

namespace ArchAngel.Interfaces.Scripting.NHibernate.Model
{
	public class ISourceFunction
	{
		public ISourceFunction()
		{
			PreviousNames = new List<string>();
			Modifiers = new List<string>();
			Parameters = new List<ISourceParameter>();
		}

		public string Name { get; set; }
		public List<ISourceParameter> Parameters { get; set; }
		public string Body { get; set; }
		public string ReturnType { get; set; }
		public List<string> PreviousNames { get; set; }
		public List<string> Modifiers { get; set; }
	}
}
