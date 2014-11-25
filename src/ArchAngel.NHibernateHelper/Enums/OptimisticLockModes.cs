using ArchAngel.Interfaces.Attributes;

namespace ArchAngel.NHibernateHelper
{
	[TemplateEnum]
	public enum OptimisticLockModes
	{
		[NullValue]
		version = 0,
		none,
		dirty,
		all
	}
}
