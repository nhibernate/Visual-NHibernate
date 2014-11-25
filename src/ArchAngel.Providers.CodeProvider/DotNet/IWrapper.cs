namespace ArchAngel.Providers.CodeProvider.DotNet
{
	public interface IWrapper
	{
		BaseConstruct GetChildren();
		IWrapper Clone();
	}
}
