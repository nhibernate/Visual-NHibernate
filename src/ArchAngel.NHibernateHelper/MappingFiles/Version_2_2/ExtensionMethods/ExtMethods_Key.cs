using System;
using System.Collections.Generic;
using System.Linq;
using ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping;

namespace ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.ExtensionMethods
{
	public static class ExtMethods_Key
	{
		public static void AddColumn(this key thisKey, column column)
		{
			if (thisKey.column == null)
				thisKey.column = new column[0];

			column[] items = thisKey.column;
			Array.Resize(ref items, thisKey.column.Length + 1);
			items[items.Length - 1] = column;
			thisKey.column = items;
		}

		public static List<column> Columns(this key key)
		{
			if (key.column == null)
				return new List<column>();

			return key.column.ToList();
		}
	}
}