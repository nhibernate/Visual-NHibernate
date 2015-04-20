using System;
using System.Collections.Generic;
using ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping;

namespace ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.ExtensionMethods
{
	public static class ExtMethods_Component
	{
		public static void AddProperty(this component theComponent, property property)
		{
			if (theComponent.Items == null)
				theComponent.Items = new object[0];

			object[] items = theComponent.Items;
			Array.Resize(ref items, theComponent.Items.Length + 1);
			items[items.Length - 1] = property;
			theComponent.Items = items;
		}

		public static IEnumerable<property> Properties(this component theComponent)
		{
			if (theComponent.Items == null)
			{
				yield break;
			}
			foreach (var item in theComponent.Items)
			{
				if (item is property)
					yield return item as property;
			}
		}
	}
}