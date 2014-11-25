namespace ArchAngel.Providers.CodeProvider.DotNet
{
	public interface IPrinter
	{
		string FullyQualifiedName{ get; }
		string Print();
	    string GetOuterText();
	    string GetInnerText();
	}
}
