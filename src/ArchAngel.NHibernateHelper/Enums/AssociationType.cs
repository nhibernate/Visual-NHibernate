using ArchAngel.Interfaces.Attributes;

namespace ArchAngel.NHibernateHelper
{
	[TemplateEnum]
	public enum AssociationType
	{
		[NullValue] None = 0,
		
		Map, Bag, Set, List, IDBag
	}
}
