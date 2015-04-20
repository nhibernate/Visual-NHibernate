using ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping;

namespace ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.ExtensionMethods
{
	public static class ExtMethods_List
	{
		/// <summary>
		/// Gets the Element of the Bag if it exists.
		/// </summary>
		/// <param name="theList"></param>
		/// <returns></returns>
		public static element Element(this list theList)
		{
			if (theList.Item != null && theList.Item is element)
				return (element)theList.Item;
			else if (theList.Item1 != null && theList.Item1 is element)
				return (element)theList.Item1;

			return null;
		}

		/// <summary>
		/// Gets the Index of the list if it exists.
		/// </summary>
		/// <param name="theList"></param>
		/// <returns></returns>
		public static index Index(this list theList)
		{
			if (theList.Item != null && theList.Item is index)
				return (index)theList.Item;
			else if (theList.Item1 != null && theList.Item1 is index)
				return (index)theList.Item1;

			return null;
		}

		/// <summary>
		/// Gets the List-Index of the list if it exists.
		/// </summary>
		/// <param name="theList"></param>
		/// <returns></returns>
		public static listindex ListIndex(this list theList)
		{
			if (theList.Item != null && theList.Item is listindex)
				return (listindex)theList.Item;
			else if (theList.Item1 != null && theList.Item1 is listindex)
				return (listindex)theList.Item1;

			return null;
		}

		/// <summary>
		/// Gets the OneToMany of the Bag if it exists.
		/// </summary>
		/// <param name="theList"></param>
		/// <returns></returns>
		public static onetomany OneToMany(this list theList)
		{
			if (theList.Item != null && theList.Item is onetomany)
				return (onetomany)theList.Item;
			else if (theList.Item1 != null && theList.Item1 is onetomany)
				return (onetomany)theList.Item1;

			return null;
		}

		/// <summary>
		/// Gets the ManyToMany of the Bag if it exists.
		/// </summary>
		/// <param name="theList"></param>
		/// <returns></returns>
		public static manytomany ManyToMany(this list theList)
		{
			if (theList.Item != null && theList.Item is manytomany)
				return (manytomany)theList.Item;
			else if (theList.Item1 != null && theList.Item1 is manytomany)
				return (manytomany)theList.Item1;

			return null;
		}

		/// <summary>
		/// Gets the CompositeElement of the set if it exists.
		/// </summary>
		/// <param name="theList"></param>
		/// <returns></returns>
		public static compositeelement CompositeElement(this list theList)
		{
			if (theList.Item != null && theList.Item is compositeelement)
				return (compositeelement)theList.Item;
			else if (theList.Item1 != null && theList.Item1 is compositeelement)
				return (compositeelement)theList.Item1;

			return null;
		}

		/// <summary>
		/// Gets the ManyToAny of the Bag if it exists.
		/// </summary>
		/// <param name="theList"></param>
		/// <returns></returns>
		public static manytoany ManyToAny(this list theList)
		{
			if (theList.Item != null && theList.Item is manytoany)
				return (manytoany)theList.Item;
			else if (theList.Item1 != null && theList.Item1 is manytoany)
				return (manytoany)theList.Item1;

			return null;
		}
	}
}