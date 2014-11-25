using System;
using System.Collections.Generic;

namespace ArchAngel.Interfaces.Scripting.NHibernate.Model
{
	[Serializable]
	public class IProperty
	{
		public string Name { get; set; }
		public string Type { get; set; }
		public string TypeVB { get; set; }
		public bool IsInherited { get; set; }
		public bool IsSetterPrivate { get; set; }
		public bool IsKeyProperty { get; set; }
		public List<string> PreviousNames { get; set; }
		public IColumn MappedColumn { get; set; }
		public string MappedColumnName { get; set; }
		public IScriptBaseObject ScriptObject { get; set; }
		public PropertyAccessTypes Access { get; set; }
		public string Formula { get; set; }
		public bool Insert { get; set; }
		public bool Update { get; set; }
		public bool OptimisticLock { get; set; }
		public PropertyGeneratedTypes Generate { get; set; }
		public bool IsNullable { get; set; }
		public bool IsVersionProperty { get; set; }
		public IEntity Parent { get; set; }
	}
}
