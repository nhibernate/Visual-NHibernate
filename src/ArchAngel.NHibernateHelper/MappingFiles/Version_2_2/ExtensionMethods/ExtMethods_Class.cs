using System;
using System.Collections.Generic;
using ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping;
using any = ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping.any;
using array = ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping.array;
using bag = ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping.bag;
using @class = ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping.@class;
using component = ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping.component;
using compositeid = ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping.compositeid;
using dynamiccomponent = ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping.dynamiccomponent;
using id = ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping.id;
using idbag = ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping.idbag;
using list = ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping.list;
using manytoone = ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping.manytoone;
using map = ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping.map;
using onetoone = ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping.onetoone;
using primitivearray = ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping.primitivearray;
using set = ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping.set;
using subclass = ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping.subclass;
using version = ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping.version;

namespace ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.ExtensionMethods
{
	public static class ExtMethods_Class
	{
		/// <summary>
		/// Gets the Version of the @class if it exists
		/// </summary>
		/// <param name="theClass"></param>
		/// <returns></returns>
		public static version Version(this @class theClass)
		{
			if (theClass.Item1 != null && theClass.Item1 is version)
				return (version)theClass.Item1;

			return null;
		}

		/// <summary>
		/// Sets the Version of the @class
		/// </summary>
		/// <param name="theClass"></param>
		/// <param name="version"></param>
		/// <returns></returns>
		public static void SetVersion(this @class theClass, version version)
		{
			theClass.Item1 = version;
		}

		/// <summary>
		/// Gets the Id of the @class if it exists
		/// </summary>
		/// <param name="theClass"></param>
		/// <returns></returns>
		public static id Id(this @class theClass)
		{
			return theClass.Item as id;
		}

		/// <summary>
		/// Gets the CompositeId of the @class if it exists
		/// </summary>
		/// <param name="theClass"></param>
		/// <returns></returns>
		public static compositeid CompositeId(this @class theClass)
		{
			if (theClass.Item != null && theClass.Item is compositeid)
				return (compositeid)theClass.Item;

			return null;
		}

		/// <summary>
		/// Gets a collection of PrimitiveArrays in the @class
		/// </summary>
		/// <param name="theClass"></param>
		/// <returns></returns>
		public static IEnumerable<primitivearray> PrimitiveArrays(this @class theClass)
		{
			if (theClass.Items == null)
			{
				yield break;
			}
			foreach (var item in theClass.Items)
			{
				if (item is primitivearray)
					yield return item as primitivearray;
			}
		}

		/// <summary>
		/// Gets a collection of Arrays in the @class
		/// </summary>
		/// <param name="theClass"></param>
		/// <returns></returns>
		public static IEnumerable<array> Arrays(this @class theClass)
		{
			if (theClass.Items == null)
			{
				yield break;
			}
			foreach (var item in theClass.Items)
			{
				if (item is array)
					yield return item as array;
			}
		}

		/// <summary>
		/// Gets a collection of IdBags in the @class
		/// </summary>
		/// <param name="theClass"></param>
		/// <returns></returns>
		public static IEnumerable<idbag> IdBags(this @class theClass)
		{
			if (theClass.Items == null)
			{
				yield break;
			}
			foreach (var item in theClass.Items)
			{
				if (item is idbag)
					yield return item as idbag;
			}
		}

		/// <summary>
		/// Gets a collection of Bags in the @class
		/// </summary>
		/// <param name="theClass"></param>
		/// <returns></returns>
		public static IEnumerable<bag> Bags(this @class theClass)
		{
			if (theClass.Items == null)
			{
				yield break;
			}
			foreach (var item in theClass.Items)
			{
				if (item is bag)
					yield return item as bag;
			}
		}

		/// <summary>
		/// Gets a collection of Lists in the @class
		/// </summary>
		/// <param name="theClass"></param>
		/// <returns></returns>
		public static IEnumerable<list> Lists(this @class theClass)
		{
			if (theClass.Items == null)
			{
				yield break;
			}
			foreach (var item in theClass.Items)
			{
				if (item is list)
					yield return item as list;
			}
		}

		/// <summary>
		/// Gets a collection of Sets in the @class
		/// </summary>
		/// <param name="theClass"></param>
		/// <returns></returns>
		public static IEnumerable<set> Sets(this @class theClass)
		{
			if (theClass.Items == null)
			{
				yield break;
			}
			foreach (var item in theClass.Items)
			{
				if (item is set)
					yield return item as set;
			}
		}

		/// <summary>
		/// Gets a collection of Maps in the @class
		/// </summary>
		/// <param name="theClass"></param>
		/// <returns></returns>
		public static IEnumerable<map> Maps(this @class theClass)
		{
			if (theClass.Items == null)
			{
				yield break;
			}
			foreach (var item in theClass.Items)
			{
				if (item is map)
					yield return item as map;
			}
		}

		/// <summary>
		/// Gets a collection of Anys in the @class
		/// </summary>
		/// <param name="theClass"></param>
		/// <returns></returns>
		public static IEnumerable<any> Anys(this @class theClass)
		{
			if (theClass.Items == null)
			{
				yield break;
			}
			foreach (var item in theClass.Items)
			{
				if (item is any)
					yield return item as any;
			}
		}

		/// <summary>
		/// Gets a collection of DynamicComponents in the @class
		/// </summary>
		/// <param name="theClass"></param>
		/// <returns></returns>
		public static IEnumerable<dynamiccomponent> DynamicComponents(this @class theClass)
		{
			if (theClass.Items == null)
			{
				yield break;
			}
			foreach (var item in theClass.Items)
			{
				if (item is dynamiccomponent)
					yield return item as dynamiccomponent;
			}
		}

		/// <summary>
		/// Gets a collection of Components in the @class
		/// </summary>
		/// <param name="theClass"></param>
		/// <returns></returns>
		public static IEnumerable<component> Components(this @class theClass)
		{
			if (theClass.Items == null)
			{
				yield break;
			}
			foreach (var item in theClass.Items)
			{
				if (item is component)
					yield return item as component;
			}
		}

		/// <summary>
		/// Gets a collection of OneToOnes in the @class
		/// </summary>
		/// <param name="theClass"></param>
		/// <returns></returns>
		public static IEnumerable<onetoone> OneToOnes(this @class theClass)
		{
			if (theClass.Items == null)
			{
				yield break;
			}
			foreach (var item in theClass.Items)
			{
				if (item is onetoone)
					yield return item as onetoone;
			}
		}

		/// <summary>
		/// Gets a collection of ManyToOnes in the @class
		/// </summary>
		/// <param name="theClass"></param>
		/// <returns></returns>
		public static IEnumerable<manytoone> ManyToOnes(this @class theClass)
		{
			if (theClass.Items == null)
			{
				yield break;
			}
			foreach (var item in theClass.Items)
			{
				if (item is manytoone)
					yield return item as manytoone;
			}
		}

		/// <summary>
		/// Gets a collection of properties in the @class
		/// </summary>
		/// <param name="theClass"></param>
		/// <returns></returns>
		public static IEnumerable<property> Properties(this @class theClass)
		{
			if (theClass.Items == null)
			{
				yield break;
			}
			foreach (var item in theClass.Items)
			{
				if (item is property)
					yield return item as property;
			}
		}

		/// <summary>
		/// Gets a collection of properties in the subclass
		/// </summary>
		/// <param name="theClass"></param>
		/// <returns></returns>
		public static IEnumerable<property> Properties(this subclass theClass)
		{
			if (theClass.Items == null)
			{
				yield break;
			}
			foreach (var item in theClass.Items)
			{
				if (item is property)
					yield return item as property;
			}
		}

		/// <summary>
		/// Gets a collection of properties in the subclass
		/// </summary>
		/// <param name="theClass"></param>
		/// <returns></returns>
		public static IEnumerable<property> Properties(this joinedsubclass theClass)
		{
			if (theClass.Items == null)
			{
				yield break;
			}
			foreach (var item in theClass.Items)
			{
				if (item is property)
					yield return item as property;
			}
		}

		/// <summary>
		/// Gets a collection of properties in the subclass
		/// </summary>
		/// <param name="theClass"></param>
		/// <returns></returns>
		public static IEnumerable<property> Properties(this unionsubclass theClass)
		{
			if (theClass.Items == null)
			{
				yield break;
			}
			foreach (var item in theClass.Items)
			{
				if (item is property)
					yield return item as property;
			}
		}

		/// <summary>
		/// Gets a collection of subclasses in the @class
		/// </summary>
		/// <param name="theClass"></param>
		/// <returns></returns>
		public static IEnumerable<subclass> SubClasses(this @class theClass)
		{
			if (theClass.Items1 == null)
			{
				yield break;
			}
			foreach (var item in theClass.Items1)
			{
				if (item is subclass)
					yield return item as subclass;
			}
		}

		/// <summary>
		/// Gets a collection of joined-subclasses in the @class
		/// </summary>
		/// <param name="theClass"></param>
		/// <returns></returns>
		public static IEnumerable<joinedsubclass> JoinedSubClasses(this @class theClass)
		{
			if (theClass.Items1 == null)
			{
				yield break;
			}
			foreach (var item in theClass.Items1)
			{
				if (item is joinedsubclass)
					yield return item as joinedsubclass;
			}
		}

		/// <summary>
		/// Gets a collection of union-subclasses in the @class
		/// </summary>
		/// <param name="theClass"></param>
		/// <returns></returns>
		public static IEnumerable<unionsubclass> UnionSubClasses(this @class theClass)
		{
			if (theClass.Items1 == null)
			{
				yield break;
			}
			foreach (var item in theClass.Items1)
			{
				if (item is unionsubclass)
					yield return item as unionsubclass;
			}
		}

		public static void AddSubClass(this @class theClass, @class subClass)
		{
			if (theClass.Items1 == null)
				theClass.Items1 = new object[0];

			object[] items = theClass.Items1;
			Array.Resize(ref items, theClass.Items1.Length + 1);
			items[items.Length - 1] = subClass;
			theClass.Items1 = items;
		}

		public static void AddComponent(this @class theClass, component component)
		{
			if (theClass.Items == null)
				theClass.Items = new object[0];

			object[] items = theClass.Items;
			Array.Resize(ref items, theClass.Items.Length + 1);
			items[items.Length - 1] = component;
			theClass.Items = items;
		}

		public static void AddProperty(this @class theClass, property property)
		{
			if (theClass.Items == null)
				theClass.Items = new object[0];

			object[] items = theClass.Items;
			Array.Resize(ref items, theClass.Items.Length + 1);
			items[items.Length - 1] = property;
			theClass.Items = items;
		}

		public static void AddProperty(this subclass theClass, property property)
		{
			if (theClass.Items == null)
				theClass.Items = new object[0];

			object[] items = theClass.Items;
			Array.Resize(ref items, theClass.Items.Length + 1);
			items[items.Length - 1] = property;
			theClass.Items = items;
		}

		public static void AddProperty(this joinedsubclass theClass, property property)
		{
			if (theClass.Items == null)
				theClass.Items = new object[0];

			object[] items = theClass.Items;
			Array.Resize(ref items, theClass.Items.Length + 1);
			items[items.Length - 1] = property;
			theClass.Items = items;
		}

		public static void AddItem<T>(this @class theClass, T property) where T : class
		{
			AddItem(theClass, property);
		}

		public static void AddItem(this @class theClass, object property)
		{
			if (theClass.Items == null)
				theClass.Items = new object[0];

			object[] items = theClass.Items;
			Array.Resize(ref items, theClass.Items.Length + 1);
			items[items.Length - 1] = property;
			theClass.Items = items;
		}

		public static void AddItem(this subclass theClass, object property)
		{
			if (theClass.Items == null)
				theClass.Items = new object[0];

			object[] items = theClass.Items;
			Array.Resize(ref items, theClass.Items.Length + 1);
			items[items.Length - 1] = property;
			theClass.Items = items;
		}

		public static void AddItem(this joinedsubclass theClass, object property)
		{
			if (theClass.Items == null)
				theClass.Items = new object[0];

			object[] items = theClass.Items;
			Array.Resize(ref items, theClass.Items.Length + 1);
			items[items.Length - 1] = property;
			theClass.Items = items;
		}

		public static void AddJoinedSubclass(this joinedsubclass theJoinedSubclass, joinedsubclass child)
		{
			if (theJoinedSubclass.joinedsubclass1 == null)
				theJoinedSubclass.joinedsubclass1 = new joinedsubclass[0];

			joinedsubclass[] items = theJoinedSubclass.joinedsubclass1;
			Array.Resize(ref items, theJoinedSubclass.joinedsubclass1.Length + 1);
			items[items.Length - 1] = child;
			theJoinedSubclass.joinedsubclass1 = items;
		}

		public static void AddItem1<T>(this @class theClass, T property) where T : class
		{
			if (theClass.Items1 == null)
				theClass.Items1 = new object[0];

			object[] items = theClass.Items1;
			Array.Resize(ref items, theClass.Items1.Length + 1);
			items[items.Length - 1] = property;
			theClass.Items1 = items;
		}

		public static void AddProperty(this join theJoinedClass, property property)
		{
			if (theJoinedClass.Items == null)
				theJoinedClass.Items = new object[0];

			object[] items = theJoinedClass.Items;
			Array.Resize(ref items, theJoinedClass.Items.Length + 1);
			items[items.Length - 1] = property;
			theJoinedClass.Items = items;
		}

		public static void SetId(this @class theClass, id Id)
		{
			theClass.Item = Id;
		}

		public static void SetCompositeId(this @class theClass, compositeid compositeId)
		{
			theClass.Item = compositeId;
		}
	}
}