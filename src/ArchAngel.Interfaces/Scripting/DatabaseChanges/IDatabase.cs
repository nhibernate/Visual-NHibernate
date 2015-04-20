
using System.Collections.Generic;
namespace ArchAngel.Interfaces.Scripting.DatabaseChanges
{
	public class IDatabase
	{
		public IDatabase(string name)
		{
			Name = name;
			NewTables = new List<NHibernate.Model.ITable>();
			ChangedTables = new List<IChangedTable>();
			RemovedTables = new List<NHibernate.Model.ITable>();
		}

		public string Name { get; set; }
		public ArchAngel.Interfaces.ProjectOptions.TypeMappings.Utility.DatabaseTypes DatabaseType { get; set; }
		public List<NHibernate.Model.ITable> NewTables { get; set; }
		public List<NHibernate.Model.ITable> RemovedTables { get; set; }
		public List<IChangedTable> ChangedTables { get; set; }
	}
}
