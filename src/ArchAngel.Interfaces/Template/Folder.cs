using System.Collections.Generic;

namespace ArchAngel.Interfaces.Template
{
	public class Folder
	{
		private int _ID = 0;

		public Folder()
		{
			Folders = new List<Folder>();
			Files = new List<File>();
			StaticFiles = new List<StaticFile>();
			Iterator = IteratorTypes.None;
		}

		public string Name { get; set; }
		public IteratorTypes Iterator { get; set; }
		public Folder ParentFolder { get; set; }
		public List<Folder> Folders { get; set; }
		public List<File> Files { get; set; }
		public List<StaticFile> StaticFiles { get; set; }

		public int ID
		{
			get { return _ID; }
			set
			{
				_ID = value;
			}
		}
	}
}
