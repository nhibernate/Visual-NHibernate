using System;

namespace ArchAngel.Interfaces.Scripting.NHibernate.Model
{
	[Serializable]
	public class IField : IFieldDef
	{
		public IColumn MappedColumn { get; set; }
	}
}
