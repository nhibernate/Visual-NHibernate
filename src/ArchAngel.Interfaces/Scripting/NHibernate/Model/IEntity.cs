using System;
using System.Collections.Generic;

namespace ArchAngel.Interfaces.Scripting.NHibernate.Model
{
	[Serializable]
	public class IEntity
	{
		public enum InheritanceTypes
		{
			None,
			TablePerConcreteClass,
			TablePerHierarchy,
			TablePerSubClass,
			TablePerSubClassWithDiscriminator
		}

		public IEntity()
		{
			Properties = new List<IProperty>();
			Components = new List<IComponentProperty>();
			References = new List<IReference>();
			InheritanceTypeWithParent = InheritanceTypes.None;
			Children = new List<IEntity>();
		}

		public string Name { get; set; }
		public string NamePlural { get; set; }
		public string NameCamelCase { get; set; }
		public string NameCamelCasePlural { get; set; }
		public IEntity Parent { get; set; }
		public List<IEntity> Children { get; set; }
		public string ParentName { get; set; }
		public bool IsInherited { get; set; }
		public bool IsAbstract { get; set; }
		public bool IsMapped { get; set; }
		public bool IsReadOnly { get; set; }
		public ITable PrimaryMappedTable { get; set; }
		public IEntityKey Key { get; set; }
		public IDiscriminator Discriminator { get; set; }
		public ICache Cache { get; set; }
		public string DiscriminatorValue { get; set; }
		public ISourceClass MappedClass { get; set; }
		public List<IProperty> Properties { get; set; }
		public List<IComponentProperty> Components { get; set; }
		public List<IReference> References { get; set; }
		public IScriptBaseObject ScriptObject { get; set; }
		public InheritanceTypes InheritanceTypeWithParent { get; set; }
		public IEntityGenerator Generator { get; set; }
		public string Proxy { get; set; }
		public bool SelectBeforeUpdate { get; set; }
		public int BatchSize { get; set; }
		public bool LazyLoad { get; set; }
		public bool IsDynamicUpdate { get; set; }
		public bool IsDynamicInsert { get; set; }
		public OptimisticLocks OptimisticLock { get; set; }
		public CascadeTypes DefaultCascade { get; set; }
	}
}
