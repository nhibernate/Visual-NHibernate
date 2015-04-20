using ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping;

namespace ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.ExtensionMethods
{
	public static class ExtMethods_Set
	{
		/// <summary>
		/// Gets the Element of the set if it exists.
		/// </summary>
		/// <param name="theSet"></param>
		/// <returns></returns>
		public static element Element(this set theSet)
		{
			if (theSet.Item != null && theSet.Item is element)
				return (element)theSet.Item;

			return null;
		}

		/// <summary>
		/// Gets the OneToMany of the set if it exists.
		/// </summary>
		/// <param name="theSet"></param>
		/// <returns></returns>
		public static onetomany OneToMany(this set theSet)
		{
			if (theSet.Item != null && theSet.Item is onetomany)
				return (onetomany)theSet.Item;

			return null;
		}

		/// <summary>
		/// Gets the ManyToMany of the set if it exists.
		/// </summary>
		/// <param name="theSet"></param>
		/// <returns></returns>
		public static manytomany ManyToMany(this set theSet)
		{
			if (theSet.Item != null && theSet.Item is manytomany)
				return (manytomany)theSet.Item;

			return null;
		}

		/// <summary>
		/// Gets the CompositeElement of the set if it exists.
		/// </summary>
		/// <param name="theSet"></param>
		/// <returns></returns>
		public static compositeelement CompositeElement(this set theSet)
		{
			if (theSet.Item != null && theSet.Item is compositeelement)
				return (compositeelement)theSet.Item;

			return null;
		}

		/// <summary>
		/// Gets the ManyToAny of the set if it exists.
		/// </summary>
		/// <param name="theSet"></param>
		/// <returns></returns>
		public static manytoany ManyToAny(this set theSet)
		{
			if (theSet.Item != null && theSet.Item is manytoany)
				return (manytoany)theSet.Item;

			return null;
		}
	}
}