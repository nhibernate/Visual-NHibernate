
namespace ArchAngel.Interfaces.Template
{
	public class Script
	{
		public Script()
		{
			Syntax = Slyce.Common.TemplateContentLanguage.CSharp;
			Body = "";
		}

		public Slyce.Common.TemplateContentLanguage Syntax { get; set; }
		public string Body { get; set; }
	}
}
