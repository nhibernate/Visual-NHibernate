using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping;
using hibernatemapping = ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping.hibernatemapping;
using joinedsubclass = ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping.joinedsubclass;
using subclass = ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping.subclass;
using unionsubclass = ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping.unionsubclass;

namespace ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.ExtensionMethods
{
	public static class ExtMethods_nhibernatemapping
	{
		/// <summary>
		/// Gets a collection of UnionSubClasses in the hibernatemapping.
		/// </summary>
		/// <param name="hm"></param>
		/// <returns></returns>
		public static IEnumerable<unionsubclass> UnionSubClasses(this hibernatemapping hm)
		{
			if (hm.Items == null)
				yield break;

			foreach (var item in hm.Items)
			{
				if (item is unionsubclass)
					yield return item as unionsubclass;
			}
		}

		/// <summary>
		/// Gets a collection of JoinedSubClasses in the hibernatemapping.
		/// </summary>
		/// <param name="hm"></param>
		/// <returns></returns>
		public static IEnumerable<joinedsubclass> JoinedSubClasses(this hibernatemapping hm)
		{
			if (hm.Items == null)
				yield break;

			foreach (var item in hm.Items)
			{
				if (item is joinedsubclass)
					yield return item as joinedsubclass;
			}
		}

		/// <summary>
		/// Gets a collection of SubClasses in the hibernatemapping.
		/// </summary>
		/// <param name="hm"></param>
		/// <returns></returns>
		public static IEnumerable<subclass> SubClasses(this hibernatemapping hm)
		{
			if (hm.Items == null)
				yield break;

			foreach (var item in hm.Items)
			{
				if (item is subclass)
					yield return item as subclass;
			}
		}

		/// <summary>
		/// Gets a collection of @classes in the hibernatemapping.
		/// </summary>
		/// <param name="hm"></param>
		/// <returns></returns>
		public static IEnumerable<Mapping.@class> Classes(this hibernatemapping hm)
		{
			if (hm.Items == null)
				yield break;

			foreach (var item in hm.Items)
			{
				if (item is Mapping.@class)
					yield return item as Mapping.@class;
			}
		}

		public static void AddClass(this hibernatemapping hm, @class @class)
		{
			if (hm.Items == null)
				hm.Items = new object[0];

			object[] items = hm.Items;
			Array.Resize(ref items, hm.Items.Length + 1);
			items[items.Length - 1] = @class;
			hm.Items = items;
		}

		public static string ToXml(this hibernatemapping hm)
		{
			// We need to use a memory stream or else the XML ends up
			// encoded as Unicode, which causes NHibernate to flip out.
			using (MemoryStream stream = new MemoryStream())
			{
				using (XmlTextWriter xtw = new XmlTextWriter(stream, Encoding.UTF8))
				{
					xtw.Formatting = Formatting.Indented;
					xtw.Indentation = 1;
					xtw.IndentChar = '\t';
					Utility.HibernateXmlSerializer.Serialize(xtw, hm);
					stream.Flush();
					return Encoding.UTF8.GetString(stream.ToArray());
				}
			}
		}
	}
}
