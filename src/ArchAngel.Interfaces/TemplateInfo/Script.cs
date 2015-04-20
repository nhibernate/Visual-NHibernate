using System;

namespace ArchAngel.Interfaces.TemplateInfo
{
	/// <summary>
	/// Summary description for Script.
	/// </summary>
	[Serializable]
    public class Script : ArchAngel.Interfaces.ITemplate.IScript
	{
		public string FileName { get;set; }
		public string ScriptName { get;set; }
		public string IteratorName { get;set; }
        public string Id { get; set; }

		public Script()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}
}
