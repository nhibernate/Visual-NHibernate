using System;

namespace ArchAngel.Interfaces.TemplateInfo
{
	/// <summary>
	/// Summary description for File.
	/// </summary>
	[Serializable]
	public class File : ITemplate.IFile
	{
		public string StaticFileName { get; set; }
		public string StaticFileSkipFunction { get; set; }
		public string Name { get; set; }
		public string IteratorName { get; set; }
		public string Id { get; set; }
	}
}
