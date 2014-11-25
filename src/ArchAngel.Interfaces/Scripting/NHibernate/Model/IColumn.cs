using System;

namespace ArchAngel.Interfaces.Scripting.NHibernate.Model
{
	[Serializable]
	public class IColumn
	{
		public IColumn()
		{
		}

		public string Name { get; set; }
		public string Type { get; set; }
		public long Length { get; set; }
		public bool IsText { get; set; }
		public bool IsNullable { get; set; }
		public bool IsIdentity { get; set; }
		public bool SizeIsMax { get; set; }
		public IScriptBaseObject ScriptObject { get; set; }
	}
}
