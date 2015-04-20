using System;
using ArchAngel.Interfaces.ITemplate;

namespace ArchAngel.Interfaces.TemplateInfo
{
	/// <summary>
	/// Summary description for Folder.
	/// </summary>
	[Serializable]
    public class Folder : IFolder
	{
		private IFolder[] m_subFolders	= new IFolder[0];
		private IScript[] m_scripts		= new IScript[0];
		private IFile[] m_files			= new IFile[0];

        public string Name
        {
            get;
            set;
        }

        public string IteratorName
        {
            get;
            set;
        }

        public string Id
        {
            get;
            set;
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