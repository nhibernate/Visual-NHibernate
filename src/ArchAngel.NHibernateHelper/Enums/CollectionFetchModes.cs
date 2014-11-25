using ArchAngel.Interfaces.Attributes;

namespace ArchAngel.NHibernateHelper
{
	[TemplateEnum]
	public enum CollectionFetchModes
	{
		[NullValue]
		select = 0,
		join,
		subselect
	}
}
