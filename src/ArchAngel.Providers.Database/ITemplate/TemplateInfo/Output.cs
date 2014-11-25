using System;
//using CodeSpew.Model.ITemplateConfig;
using Slyce.ITemplate;

namespace Slyce.TemplateInfo
{
	/// <summary>
	/// Summary description for Output.
	/// </summary>
	[Serializable]
	public class Output : Slyce.ITemplate.IOutput
	{
		public string m_name;
		public IFolder m_rootFolder;
		public IScript[] m_scripts = new IScript[0];

		public string Name
		{
			get {return m_name;}
			set {m_name = value;}
		}

		public IFolder RootFolder
		{
			get {return m_rootFolder;}
			set {m_rootFolder = value;}
		}

		public Output()
        {
		}
	}
}