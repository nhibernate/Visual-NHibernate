using System;
using System.Collections.Generic;
using System.Linq;
using ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping;

namespace ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.ExtensionMethods
{
	public static class ExtMethods_ManyToOne
	{
		public static void AddColumn(this manytoone thisManyToOne, column column)
		{
			//throw new NotImplementedException("TODO: What are we meant to do here?");

			//if (thisManyToOne.column == null)
			//    thisManyToOne.column = new column[0];

			//var items = thisManyToOne.column;
			//Array.Resize(ref items, thisManyToOne.column.Length + 1);
			//items[items.Length - 1] = column;
			//thisManyToOne.column = items;

			if (thisManyToOne.Items == null)
				thisManyToOne.Items = new column[0];

			var items = thisManyToOne.Items;
			Array.Resize(ref items, thisManyToOne.Items.Length + 1);
			items[items.Length - 1] = column;
			thisManyToOne.Items = items;
		}

		public static List<column> Columns(this manytoone manytoone)
		{
			List<column> columns = new List<column>();

			if (manytoone.Items == null)
				return columns;

			foreach (column col in manytoone.Items.Where(i => i is column))
				columns.Add(col);

			return columns;
		}
	}
}
