using ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping;

namespace ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.ExtensionMethods
{
	public static class ExtMethods_Map
	{
		/// <summary>
		/// Gets the Element of the map if it exists.
		/// </summary>
		/// <param name="theMap"></param>
		/// <returns></returns>
		public static element Element(this map theMap)
		{
			if (theMap.Item != null && theMap.Item is element)
				return (element)theMap.Item;
			else if (theMap.Item1 != null && theMap.Item1 is element)
				return (element)theMap.Item1;

			return null;
		}

		/// <summary>
		/// Gets the OneToMany of the map if it exists.
		/// </summary>
		/// <param name="theMap"></param>
		/// <returns></returns>
		public static onetomany OneToMany(this map theMap)
		{
			if (theMap.Item != null && theMap.Item is onetomany)
				return (onetomany)theMap.Item;
			else if (theMap.Item1 != null && theMap.Item1 is onetomany)
				return (onetomany)theMap.Item1;

			return null;
		}

		/// <summary>
		/// Gets the ManyToMany of the map if it exists.
		/// </summary>
		/// <param name="theMap"></param>
		/// <returns></returns>
		public static manytomany ManyToMany(this map theMap)
		{
			if (theMap.Item != null && theMap.Item is manytomany)
				return (manytomany)theMap.Item;
			else if (theMap.Item1 != null && theMap.Item1 is manytomany)
				return (manytomany)theMap.Item1;

			return null;
		}

		/// <summary>
		/// Gets the CompositeElement of the map if it exists.
		/// </summary>
		/// <param name="theMap"></param>
		/// <returns></returns>
		public static compositeelement CompositeElement(this map theMap)
		{
			if (theMap.Item != null && theMap.Item is compositeelement)
				return (compositeelement)theMap.Item;
			else if (theMap.Item1 != null && theMap.Item1 is compositeelement)
				return (compositeelement)theMap.Item1;

			return null;
		}

		/// <summary>
		/// Gets the ManyToAny of the map if it exists.
		/// </summary>
		/// <param name="theMap"></param>
		/// <returns></returns>
		public static manytoany ManyToAny(this map theMap)
		{
			if (theMap.Item != null && theMap.Item is manytoany)
				return (manytoany)theMap.Item;
			else if (theMap.Item1 != null && theMap.Item1 is manytoany)
				return (manytoany)theMap.Item1;

			return null;
		}

		/// <summary>
		/// Gets the Index of the map if it exists.
		/// </summary>
		/// <param name="theMap"></param>
		/// <returns></returns>
		public static index Index(this map theMap)
		{
			if (theMap.Item != null && theMap.Item is index)
				return (index)theMap.Item;
			else if (theMap.Item1 != null && theMap.Item1 is index)
				return (index)theMap.Item1;

			return null;
		}

		/// <summary>
		/// Gets the CompositeIndex of the map if it exists.
		/// </summary>
		/// <param name="theMap"></param>
		/// <returns></returns>
		public static compositeindex CompositeIndex(this map theMap)
		{
			if (theMap.Item != null && theMap.Item is compositeindex)
				return (compositeindex)theMap.Item;
			else if (theMap.Item1 != null && theMap.Item1 is compositeindex)
				return (compositeindex)theMap.Item1;

			return null;
		}

		/// <summary>
		/// Gets the IndexManyToMany of the map if it exists.
		/// </summary>
		/// <param name="theMap"></param>
		/// <returns></returns>
		public static indexmanytomany IndexManyToMany(this map theMap)
		{
			if (theMap.Item != null && theMap.Item is indexmanytomany)
				return (indexmanytomany)theMap.Item;
			else if (theMap.Item1 != null && theMap.Item1 is indexmanytomany)
				return (indexmanytomany)theMap.Item1;

			return null;
		}

		/// <summary>
		/// Gets the IndexManyToAny of the map if it exists.
		/// </summary>
		/// <param name="theMap"></param>
		/// <returns></returns>
		public static indexmanytoany IndexManyToAny(this map theMap)
		{
			if (theMap.Item != null && theMap.Item is indexmanytoany)
				return (indexmanytoany)theMap.Item;
			else if (theMap.Item1 != null && theMap.Item1 is indexmanytoany)
				return (indexmanytoany)theMap.Item1;

			return null;
		}
	}
}