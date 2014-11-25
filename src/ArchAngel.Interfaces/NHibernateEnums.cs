using System;
using ArchAngel.Interfaces.Attributes;

namespace ArchAngel.Interfaces.NHibernateEnums
{
	[TemplateEnum]
	public enum TopLevelAccessTypes
	{
		[NullValue]
		property = 0,
		field,
		ClassName
	}

	[TemplateEnum]
	public enum PropertyAccessTypes
	{
		[NullValue]
		inherit_default = 0,
		property,
		field,
		ClassName
	}

	[TemplateEnum]
	public enum PropertyGeneratedTypes
	{
		[NullValue]
		never = 0,
		insert,
		always
	}

	[TemplateEnum]
	public enum TopLevelCascadeTypes
	{
		[NullValue]
		none = 0,
		all,
		save_update,
		delete
	}

	[TemplateEnum]
	public enum CascadeTypes
	{
		[NullValue]
		inherit_default = 0,
		none,
		all,
		save_update,
		delete
	}

	[TemplateEnum]
	public enum TopLevelCollectionCascadeTypes
	{
		[NullValue]
		none = 0,
		all,
		save_update,
		delete,
		delete_orphan,
		all_delete_orphan
	}

	[TemplateEnum]
	public enum CollectionCascadeTypes
	{
		[NullValue]
		inherit_default = 0,
		none,
		all,
		save_update,
		delete,
		delete_orphan,
		all_delete_orphan
	}

	[TemplateEnum]
	public enum EntityLazyTypes
	{
		[NullValue]
		inherit_default = 0,
		@true,
		@false
	}

	[TemplateEnum]
	public enum BooleanInheritedTypes
	{
		[NullValue]
		inherit_default = 0,
		@true,
		@false
	}

	[TemplateEnum]
	public enum CollectionLazyTypes
	{
		[NullValue]
		inherit_default = 0,
		@true,
		@false,
		extra
	}

	[TemplateEnum]
	public enum PropertiesForThisEntity
	{
		none
	}

	[TemplateEnum]
	public enum PropertiesForOtherEntity
	{
		none
	}

	public static class Helper
	{
		public static CascadeTypes GetCascadeType(string value)
		{
			if (string.IsNullOrEmpty(value))
				return CascadeTypes.inherit_default;

			return (CascadeTypes)Enum.Parse(typeof(CascadeTypes), value.Replace("-", "_"), true);
		}

		public static CollectionCascadeTypes GetCollectionCascadeType(string value)
		{
			if (string.IsNullOrEmpty(value))
				return CollectionCascadeTypes.inherit_default;

			return (CollectionCascadeTypes)Enum.Parse(typeof(CollectionCascadeTypes), value.Replace("-", "_"), true);
		}

		public static CollectionLazyTypes GetCollectionLazyType(string value)
		{
			if (string.IsNullOrEmpty(value))
				return CollectionLazyTypes.@true;

			if (value.StartsWith("true")) return CollectionLazyTypes.@true;
			if (value.StartsWith("false")) return CollectionLazyTypes.@false;

			return CollectionLazyTypes.extra;
		}
	}
}
