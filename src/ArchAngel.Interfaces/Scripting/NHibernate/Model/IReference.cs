using System;
using System.Collections.Generic;

namespace ArchAngel.Interfaces.Scripting.NHibernate.Model
{
	public enum ReferenceTypes
	{
		OneToOne,
		OneToMany,
		ManyToOne,
		ManyToMany,
		Unsupported
	}
	public enum ReferenceKeyTypes
	{
		Primary,
		Foreign,
		Unique,
		None
	}
	public enum CollectionTypes
	{
		None,
		Map,
		Bag,
		Set,
		List,
		IDBag
	}
	public enum FetchTypes
	{
		Select,
		Join,
		Subselect
	}
	public enum LazyTypes
	{
		True,
		False,
		Extra
	}
	public enum CascadeTypes
	{
		None,
		All,
		SaveUpdate,
		Delete
	}
	public enum CollectionCascadeTypes
	{
		None,
		All,
		SaveUpdate,
		Delete,
		DeleteOrphan,
		AllDeleteOrphan
	}
	public enum PropertyAccessTypes
	{
		Property,
		Field,
		ClassName
	}
	public enum PropertyGeneratedTypes
	{
		Never,
		Insert,
		Always
	}
	public enum CacheUsageTypes
	{
		None,
		Read_Only,
		Read_Write,
		NonStrict_Read_Write,
		Transactional
	}
	public enum CacheIncludeTypes
	{
		All,
		Non_Lazy
	}
	public enum OptimisticLocks
	{
		Version,
		None,
		Dirty,
		All
	}

	[Serializable]
	public class IReference
	{
		public IReference()
		{
			KeyColumns = new List<IColumn>();
			ToKeyColumns = new List<IColumn>();
			CollectionType = CollectionTypes.Set;
		}

		public string Name { get; set; }
		public string ToName { get; set; }
		public string Type { get; set; }
		public bool IsSetterPrivate { get; set; }
		public List<string> PreviousNames { get; set; }
		public IEntity ToEntity { get; set; }
		public IColumn CollectionIndexColumn { get; set; }
		public ITable ManyToManyAssociationTable { get; set; }
		public CollectionTypes CollectionType { get; set; }
		public FetchTypes FetchType { get; set; }
		public LazyTypes LazyType { get; set; }
		public CascadeTypes CascadeType { get; set; }
		public CollectionCascadeTypes CollectionCascadeType { get; set; }
		public ReferenceTypes ReferenceType { get; set; }
		public ReferenceKeyTypes KeyType { get; set; }
		public List<IColumn> KeyColumns { get; set; }
		public List<IColumn> ToKeyColumns { get; set; }
		public bool Inverse { get; set; }
		public bool Insert { get; set; }
		public bool Update { get; set; }
		//IScriptBaseObject ScriptObject { get; set; }
		public IProperty OrderByProperty { get; set; }
		public bool OrderByIsAsc { get; set; }

		public string OrderByClause
		{
			get
			{
				if (OrderByProperty != null && OrderByProperty.MappedColumnName.Length > 0)
				{
					if (OrderByIsAsc)
						return OrderByProperty.MappedColumnName;
					else
						return OrderByProperty.MappedColumnName + " desc";
				}
				return "";
			}
		}
	}
}
