using System;
using System.Collections.Generic;
using ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping;

namespace ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.ExtensionMethods
{
	public static class ExtMethods_CompositeId
	{
		/// <summary>
		/// Gets a collection of KeyProperties in the CompositeId
		/// </summary>
		/// <param name="theCompositeId"></param>
		/// <returns></returns>
		public static IEnumerable<keyproperty> KeyProperties(this compositeid theCompositeId)
		{
			if (theCompositeId.Items == null)
			{
				yield break;
			}
			foreach (var item in theCompositeId.Items)
			{
				if (item is keyproperty)
					yield return item as keyproperty;
			}
		}

		public static void AddKeyProperty(this compositeid theCompositeId, keyproperty keyProperty)
		{
			if (theCompositeId.Items == null)
				theCompositeId.Items = new object[0];

			object[] items = theCompositeId.Items;
			Array.Resize(ref items, theCompositeId.Items.Length + 1);
			items[items.Length - 1] = keyProperty;
			theCompositeId.Items = items;
		}
	}
}