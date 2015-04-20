using System;
using System.Collections.Generic;

namespace ArchAngel.Interfaces.Scripting.NHibernate.Model
{
	[Serializable]
	public class ITable
	{
		public ITable(string databaseName)
		{
			DatabaseName = databaseName;
			Columns = new List<IColumn>();
			Indexes = new List<IIndex>();
			ForeignKeys = new List<IKey>();
		}

		public string DatabaseName { get; set; }
		public string Name { get; set; }
		public bool IsView { get; set; }
		public string Schema { get; set; }
		public List<IColumn> Columns { get; set; }
		public List<IIndex> Indexes { get; set; }
		public List<IKey> ForeignKeys { get; set; }
		public IKey PrimaryKey { get; set; }
		public IScriptBaseObject ScriptObject { get; set; }
	}
}
