using System.Collections.Generic;

namespace ArchAngel.Interfaces.Scripting.NHibernate.Model
{
	public class IKey
	{
		public enum KeyTypes
		{
			Primary,
			Foreign,
			Unique,
			None
		}

		public IKey()
		{
			Columns = new List<IColumn>();
		}

		public string Name { get; set; }
		public string TableSchema { get; set; }
		public string TableName { get; set; }
		public KeyTypes KeyType { get; set; }
		public List<IColumn> Columns { get; set; }
		public IKey ReferencedPrimaryKey { get; set; }
	}
}
