using ArchAngel.Interfaces.Attributes;

namespace ArchAngel.NHibernateHelper
{
	[TemplateEnum]
	public enum DefaultCascadeTypes
	{
		[NullValue]
		none = 0,
		save_update,
		delete,
		all,
		all_delete_orphan,
		delete_orphan
	}
}