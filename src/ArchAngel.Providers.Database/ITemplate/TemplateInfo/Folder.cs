using System;
//using CodeSpew.Model.ITemplateConfig;
//using Slyce.ITemplateConfig;
using Slyce.ITemplate;

namespace Slyce.TemplateInfo
{
	/// <summary>
	/// Summary description for Folder.
	/// </summary>
	[Serializable]
	public class Folder : Slyce.ITemplate.IFolder
	{
		private string m_name;
		private IFolder[] m_subFolders	= new IFolder[0];
		private IScript[] m_scripts		= new IScript[0];
		private IFile[] m_files			= new IFile[0];

		public string Name
		{
			get {return m_name;}
			set {m_name = value;}
		}

		public IFolder[] SubFolders
		{
			get {return m_subFolders;}
			set {m_subFolders = value;}
		}

		public IScript[] Scripts
		{
			get {return m_scripts;}
			set {m_scripts = value;}
		}

		public IFile[] Files
		{
			get {return m_files;}
			set {m_files = value;}
		}

		public Folder()
		{
		}
	}
}