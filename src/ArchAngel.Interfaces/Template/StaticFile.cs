
namespace ArchAngel.Interfaces.Template
{
	public class StaticFile
	{
		public StaticFile()
		{
			SkipThisFileScript = "// Eg: to skip this file set SkipThisFile = true";
		}

		public int Id { get; set; }
		public string ResourceName { get; set; }
		public string Name { get; set; }
		public IteratorTypes Iterator { get; set; }
		public Folder ParentFolder { get; set; }
		public string SkipThisFileScript { get; set; }
	}
}
