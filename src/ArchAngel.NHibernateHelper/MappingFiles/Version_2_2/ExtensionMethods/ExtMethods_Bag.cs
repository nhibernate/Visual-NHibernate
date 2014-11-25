using ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping;

namespace ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.ExtensionMethods
{
	public static class ExtMethods_Bag
	{
		/// <summary>
		/// Gets the Element of the Bag if it exists.
		/// </summary>
		/// <param name="theBag"></param>
		/// <returns></returns>
		public static element Element(this bag theBag)
		{
			if (theBag.Item != null && theBag.Item is element)
				return (element)theBag.Item;

			return null;
		}

		/// <summary>
		/// Gets the OneToMany of the Bag if it exists.
		/// </summary>
		/// <param name="theBag"></param>
		/// <returns></returns>
		public static onetomany OneToMany(this bag theBag)
		{
			if (theBag.Item != null && theBag.Item is onetomany)
				return (onetomany)theBag.Item;

			return null;
		}

		/// <summary>
		/// Gets the ManyToMany of the Bag if it exists.
		/// </summary>
		/// <param name="theBag"></param>
		/// <returns></returns>
		public static manytomany ManyToMany(this bag theBag)
		{
			if (theBag.Item != null && theBag.Item is manytomany)
				return (manytomany)theBag.Item;

			return null;
		}

		/// <summary>
		/// Gets the CompositeElement of the set if it exists.
		/// </summary>
		/// <param name="theBag"></param>
		/// <returns></returns>
		public static compositeelement CompositeElement(this bag theBag)
		{
			if (theBag.Item != null && theBag.Item is compositeelement)
				return (compositeelement)theBag.Item;

			return null;
		}

		/// <summary>
		/// Gets the ManyToAny of the Bag if it exists.
		/// </summary>
		/// <param name="theBag"></param>
		/// <returns></returns>
		public static manytoany ManyToAny(this bag theBag)
		{
			if (theBag.Item != null && theBag.Item is manytoany)
				return (manytoany)theBag.Item;

			return null;
		}
	}
}