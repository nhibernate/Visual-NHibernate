using ArchAngel.Interfaces.Attributes;

namespace ArchAngel.NHibernateHelper
{
	[TemplateEnum]
	public enum FetchModes
	{
		[NullValue]
		select = 0,
		join
	}
}
