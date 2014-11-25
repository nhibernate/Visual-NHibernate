using System;
using System.Collections.Generic;

namespace ArchAngel.Interfaces.Scripting.NHibernate.Model
{
	[Serializable]
	public class IProject
	{
		public IProject()
		{
			Entities = new List<IEntity>();
			Components = new List<IComponent>();
			Tables = new List<ITable>();
			Views = new List<ITable>();
		}

		public Slyce.Common.CSProjFile ExistingCsProjectFile { get; set; }
		public INhConfig NHibernateConfig { get; set; }
		//public string ExistingNhConfigFilePath { get; set; }

		public List<IEntity> Entities { get; set; }
		public List<IComponent> Components { get; set; }
		public List<ITable> Tables { get; set; }
		public List<ITable> Views { get; set; }

		public string OutputFolder { get; set; }
		public string TempFolder { get; set; }
		public bool OverwriteFiles { get; set; }
	}
}
