using System.Collections.Generic;
using System.Linq;
using ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping;

namespace ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.ExtensionMethods
{
	public static class ExtMethods_ManyToMany
	{
		public static List<column> Columns(this manytomany manytomany)
		{
			List<column> columns = new List<column>();

			if (manytomany.Items == null)
				return columns;

			foreach (column col in manytomany.Items.Where(i => i is column))
				columns.Add(col);

			return columns;
		}
	}
}
