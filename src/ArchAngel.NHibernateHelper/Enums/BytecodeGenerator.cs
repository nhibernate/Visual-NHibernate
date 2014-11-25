using ArchAngel.Interfaces.Attributes;

namespace ArchAngel.NHibernateHelper
{
	[TemplateEnum]
	public enum BytecodeGenerator
	{
		[NullValue]
		DynamicProxy, LinFu, Spring
	}
}