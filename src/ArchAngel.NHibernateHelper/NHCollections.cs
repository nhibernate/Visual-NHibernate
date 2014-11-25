using System;
using System.Linq;
using ArchAngel.NHibernateHelper.MappingFiles.Version_2_2;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;

namespace ArchAngel.NHibernateHelper
{
	public static class NHCollections
	{
		/// <summary>
		/// This method is used in the Template to get the C# Collection type name for the CollectionType of the given reference.
		/// </summary>
		/// <param name="reference"></param>
		/// <returns></returns>
		public static string GetCollectionType(DirectedReference reference)
		{
			switch (GetAssociationType(reference))
			{
				case AssociationType.None:
					//throw new ArgumentException("Reference " + reference.Reference.Name + " does not have an association type for End" + (reference.Entity1IsFromEnd ? "1" : "2"));
					return "XXXXXX";
				case AssociationType.Map:
					// {0} should be the primary key column type, {1} is the entity type.
					//return string.Format("System.Collections.Generic.IDictionary<{0}, {1}>", 
					//    GetIndexColumnTypeName(reference, EntityMapper.GetPrimaryTable(reference.FromEntity)),
					//    reference.FromEntity.Name.GetCSharpFriendlyIdentifier());
					return string.Format("IDictionary<{0}, {1}>",
						GetIndexColumnTypeName(reference, EntityMapper.GetPrimaryTable(reference.ToEntity)),
						reference.ToEntity.Name);
				case AssociationType.List:
				case AssociationType.Bag:
					//return string.Format("System.Collections.Generic.IList<{0}>", reference.ToEntity.Name.GetCSharpFriendlyIdentifier());
					return string.Format("IList<{0}>", reference.ToEntity.Name);
				case AssociationType.Set:
					//return string.Format("Iesi.Collections.Generic.ISet<{0}>", reference.ToEntity.Name.GetCSharpFriendlyIdentifier());
					return string.Format("Iesi.Collections.Generic.ISet<{0}>", reference.ToEntity.Name);
				//case AssociationType.IDBag:
				// Should this also be an IDictionary?
				//    throw new NotImplementedException("Have not implemented IDBag types yet.");
				case AssociationType.IDBag:
					return string.Format("Iesi.Collections.Generic.ISet<{0}>", reference.ToEntity.Name);
				default:
					throw new NotImplementedException("Collection type not handled yet in GetCollectionType(): " + GetAssociationType(reference));
			}
		}

		public static ArchAngel.Providers.EntityModel.Model.DatabaseLayer.IColumn GetIndexColumn(DirectedReference directedReference, ITable table)
		{
			var columnName = GetIndexColumnName(directedReference);
			ArchAngel.Providers.EntityModel.Model.DatabaseLayer.IColumn column = table.GetColumn(columnName, StringComparison.InvariantCultureIgnoreCase);

			if (column == null)
			{
				Property prop = directedReference.ToEntity.Properties.SingleOrDefault(p => p.Name.Equals(columnName, StringComparison.InvariantCultureIgnoreCase));

				if (prop != null)
					column = prop.MappedColumn();
			}
			return column;
		}

		public static string GetIndexColumnTypeName(DirectedReference directedReference, ITable table)
		{
			var columnName = GetIndexColumnName(directedReference);
			var column = table.GetColumn(columnName, StringComparison.InvariantCultureIgnoreCase);
			if (column == null) throw new NHibernateMappingException(string.Format("Could not find the Index Column named {0} for collection {1} on entity {2}", columnName, directedReference.FromName, directedReference.FromEntity.Name));

			ArchAngel.Interfaces.ProjectOptions.TypeMappings.Utility.ColumnInfo columnInfo = new Interfaces.ProjectOptions.TypeMappings.Utility.ColumnInfo()
			{
				IsNullable = column.IsNullable,
				Name = column.Name,
				Precision = column.Precision,
				Scale = column.Scale,
				Size = column.Size,
				TypeName = column.OriginalDataType
			};
			return ArchAngel.Interfaces.ProjectOptions.TypeMappings.Utility.GetCSharpTypeName(column.Parent.Database.DatabaseType.ToString(), columnInfo);
		}

		public static string GetIndexColumnName(DirectedReference directedReference)
		{
			string propertyName = directedReference.Entity1IsFromEnd ? "End1IndexColumn" : "End2IndexColumn";
			string indexColumnName = (string)directedReference.Reference.GetUserOptionValue(propertyName);
			return indexColumnName;
		}

		public static AssociationType GetAssociationType(DirectedReference directedReference)
		{
			string propertyName =
				directedReference.Entity1IsFromEnd ? "End1CollectionType" : "End2CollectionType";
			return (AssociationType)directedReference.Reference.GetUserOptionValue(propertyName);
		}

		public static CollectionFetchModes GetFetchType(DirectedReference directedReference)
		{
			string propertyName =
				directedReference.Entity1IsFromEnd ? "Reference_End1CollectionFetchMode" : "Reference_End2CollectionFetchMode";
			return (CollectionFetchModes)directedReference.Reference.GetUserOptionValue(propertyName);
		}

		public static ArchAngel.Interfaces.NHibernateEnums.CollectionLazyTypes GetLazyType(DirectedReference directedReference)
		{
			string propertyName =
				directedReference.Entity1IsFromEnd ? "Reference_End1Lazy" : "Reference_End2Lazy";
			return (ArchAngel.Interfaces.NHibernateEnums.CollectionLazyTypes)directedReference.Reference.GetUserOptionValue(propertyName);
		}

		public static ArchAngel.Interfaces.NHibernateEnums.CollectionCascadeTypes GetCollectionCascadeType(DirectedReference directedReference)
		{
			string propertyName =
				directedReference.Entity1IsFromEnd ? "Reference_End1CollectionCascade" : "Reference_End2CollectionCascade";
			return (ArchAngel.Interfaces.NHibernateEnums.CollectionCascadeTypes)directedReference.Reference.GetUserOptionValue(propertyName);
		}

		public static ArchAngel.Interfaces.NHibernateEnums.CascadeTypes GetCascadeType(DirectedReference directedReference)
		{
			string propertyName =
				directedReference.Entity1IsFromEnd ? "Reference_End1Cascade" : "Reference_End2Cascade";
			return (ArchAngel.Interfaces.NHibernateEnums.CascadeTypes)directedReference.Reference.GetUserOptionValue(propertyName);
		}

		/// <summary>
		/// Gets the EndXSqlWhereClause property, or returns null if it has not been set.
		/// </summary>
		/// <param name="directedReference"></param>
		/// <returns></returns>
		public static string GetSqlWhereClause(DirectedReference directedReference)
		{
			string propertyName = directedReference.Entity1IsFromEnd ? "End1SqlWhereClause" : "End2SqlWhereClause";
			if (directedReference.Reference.HasUserOption(propertyName))
				return (string)directedReference.Reference.GetUserOptionValue(propertyName);

			return null;
		}


	}
}
