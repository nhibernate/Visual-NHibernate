using ArchAngel.Interfaces.Attributes;

namespace ArchAngel.NHibernateHelper
{
	[TemplateEnum]
	public enum NHibernateVersions
	{
		[NullValue]
		v2 = 0, // default TODO: change to 3
		v3
	}
}
