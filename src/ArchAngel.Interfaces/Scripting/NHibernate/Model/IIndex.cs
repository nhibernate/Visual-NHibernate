
using System.Collections.Generic;
namespace ArchAngel.Interfaces.Scripting.NHibernate.Model
{
	public class IIndex
	{
		public enum DatabaseIndexType
		{
			Unique, Check, None, ForeignKey, PrimaryKey
		}

		public IIndex()
		{
			Columns = new List<IColumn>();
		}

		public string Name { get; set; }
		public List<IColumn> Columns { get; set; }
		public DatabaseIndexType IndexType { get; set; }
		public bool IsClustered { get; set; }
		public bool IsUnique { get; set; }
		public bool IsUserDefined { get; set; }
	}
}
