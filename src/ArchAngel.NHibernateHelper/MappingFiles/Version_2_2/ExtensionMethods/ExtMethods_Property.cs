using System.Collections.Generic;
using ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping;

namespace ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.ExtensionMethods
{
	public static class ExtMethods_Property
	{
		public static List<column> Columns(this property prop)
		{
			List<column> columns = new List<column>();

			if (prop.Items == null)
				return columns;

			foreach (column col in prop.Items)
				columns.Add(col);

			return columns;
		}
	}
}
