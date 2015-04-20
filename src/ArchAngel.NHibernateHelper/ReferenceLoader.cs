using System;
using System.Collections.Generic;
using System.Linq;
using ArchAngel.Interfaces;
using ArchAngel.NHibernateHelper.EntityExtensions;
using ArchAngel.NHibernateHelper.MappingFiles.Version_2_2;
using ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.ExtensionMethods;
using ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using log4net;
using Slyce.Common.StringExtensions;
using ArchAngel.Interfaces.NHibernateEnums;

namespace ArchAngel.NHibernateHelper
{
	internal class ReferenceLoader
	{
		private class AssociationInformation
		{
			public class TableNameType
			{
				public string SchemaName;
				public string TableName;

				public TableNameType(string schemaName, string tableName)
				{
					SchemaName = schemaName;
					TableName = tableName;
				}
			}
			/// <summary>
			/// The name of the property this end of the association is mapped to.
			/// </summary>
			public string PropertyName;
			/// <summary>
			/// Name of the column on this table that is the foreign key.
			/// </summary>
			private List<string> _ForeignKeyColumnNames = new List<string>();

			public List<string> ForeignKeyColumnNames
			{
				get { return _ForeignKeyColumnNames; }
				set
				{
					_ForeignKeyColumnNames = value;
				}
			}
			/// <summary>
			/// If true, the Foreign Key column specified exists on the table
			/// mapped to this entity.
			/// </summary>
			public bool ForeignKeyBelongsToThisTable;
			/// <summary>
			/// The name of the entity this entity has an association to.
			/// </summary>
			public string OtherEntityName;
			/// <summary>
			/// The name of this entity.
			/// </summary>
			public string ThisEntityName;
			/// <summary>
			/// If set, this is the column that contains the index of the entity in the list.
			/// </summary>
			public string IndexColumn;
			/// <summary>
			/// If set, the where clause that filters this reference.
			/// </summary>
			public string WhereClause;
			/// <summary>
			/// If set, this side of the association had a unique constraint of some sort.
			/// </summary>
			public bool HasUniqueConstraint;
			/// <summary>
			/// The Cardinality of this end of the association.
			/// </summary>
			public Cardinality Cardinality;
			/// <summary>
			/// The cascade option that was specified in the mapping information. If it is null or blank,
			/// the attribute was not specified.
			/// </summary>
			public ArchAngel.Interfaces.NHibernateEnums.CascadeTypes Cascade;
			/// <summary>
			/// If set, contains the name of the table that contains the association mappings.
			/// </summary>
			public TableNameType AssociationTableName;
			/// <summary>
			/// If set, the type of this end of the association.
			/// </summary>
			public AssociationType AssociationType = DefaultAssociationType;

			public FetchModes FetchMode;
			public CollectionFetchModes CollectionFetchMode;
			public ArchAngel.Interfaces.NHibernateEnums.CollectionLazyTypes CollectionLazy;
			public ArchAngel.Interfaces.NHibernateEnums.CollectionCascadeTypes CollectionCascade;
			public bool OrderByIsAsc;
			public bool Insert;
			public bool Update;
			public ArchAngel.Interfaces.NHibernateEnums.BooleanInheritedTypes Inverse;
			public string OrderByColumnName;

			public override string ToString()
			{
				return string.Format("PropertyName: {0}, Cardinality: {1}", PropertyName, Cardinality);
			}

			private static AssociationType DefaultAssociationType
			{
				get
				{
					// TODO: get this from a user-setting
					return NHibernateHelper.AssociationType.Set;
				}
			}
		}

		private readonly List<AssociationInformation> associationInformation
			= new List<AssociationInformation>();

		private static readonly ILog log = LogManager.GetLogger(typeof(ReferenceLoader));

		internal void ProcessReferences(IList<hibernatemapping> mappingFiles, MappingSet mappingSet)
		{
			associationInformation.Clear();

			List<string> processedClasses = new List<string>();

			foreach (var hm in mappingFiles)
			{
				foreach (var hClass in hm.Classes())
				{
					string @namespace;
					string name;
					EntityLoader.IsNameFullyQualified(hClass.name, out @namespace, out name);

					if (processedClasses.Contains(name))
						continue;

					processedClasses.Add(name);

					foreach (manytoone hManyToOne in hClass.ManyToOnes())
					{
						var fkColumnNames = GetColumnNames(hManyToOne.column, hManyToOne.Columns()).ToList();
						string thisEntityName;
						string otherEntityName;

						EntityLoader.IsNameFullyQualified(hClass.name, out thisEntityName);
						EntityLoader.IsNameFullyQualified(hManyToOne.@class, out otherEntityName);
						var many2OneInfo
							= new AssociationInformation
								{
									ThisEntityName = thisEntityName,
									OtherEntityName = otherEntityName,
									ForeignKeyColumnNames = fkColumnNames,
									ForeignKeyBelongsToThisTable = fkColumnNames.Count > 0,
									PropertyName = hManyToOne.name,
									Cardinality = Cardinality.One,
									HasUniqueConstraint = hManyToOne.unique,
									Insert = hManyToOne.insert,
									Update = hManyToOne.update,
									Cascade = ArchAngel.Interfaces.NHibernateEnums.Helper.GetCascadeType(hManyToOne.cascade)
								};
						if (hManyToOne.fetchSpecified)
							many2OneInfo.FetchMode = (FetchModes)Enum.Parse(typeof(FetchModes), hManyToOne.fetch.ToString(), true);

						if (hManyToOne.lazySpecified)
							many2OneInfo.CollectionLazy = ArchAngel.Interfaces.NHibernateEnums.Helper.GetCollectionLazyType(hManyToOne.lazy.ToString());

						associationInformation.Add(many2OneInfo);
					}

					foreach (var hOneToOne in hClass.OneToOnes())
					{
						string thisEntityName;
						string otherEntityName;
						EntityLoader.IsNameFullyQualified(hClass.name, out thisEntityName);
						EntityLoader.IsNameFullyQualified(hOneToOne.@class, out otherEntityName);

						var one2OneInfo
							= new AssociationInformation
								{
									ThisEntityName = thisEntityName,
									OtherEntityName = otherEntityName,
									PropertyName = hOneToOne.name,
									Cardinality = Cardinality.One
								};
						associationInformation.Add(one2OneInfo);
					}

					foreach (var hSet in hClass.Sets())
					{
						key keyNode = hSet.key;
						if (keyNode == null) throw new Exception("Cannot find key node in set " + hSet.name);

						string schema = "";

						if (hSet.schema != null)
							schema = hSet.schema;
						else if (hClass.schema != null)
							schema = hClass.schema;
						else if (hm.schema != null)
							schema = hm.schema;

						ProcessCollection(hClass, keyNode, null, hSet.ManyToMany(), GetClassName(hSet.Item), schema, hSet.table, hSet.name, hSet.cascade, AssociationType.Set, hSet.where, hSet.fetch, hSet.inverse, hSet.lazy, hSet.orderby);
					}

					foreach (var hMap in hClass.Maps())
					{
						key keyNode = hMap.key;
						index indexNode = hMap.Index();
						if (keyNode == null) throw new Exception("Cannot find key node in map " + hMap.name);
						if (indexNode == null) throw new Exception("Cannot find index node in map " + hMap.name);

						string schema = "";

						if (hMap.schema != null)
							schema = hMap.schema;
						else if (hClass.schema != null)
							schema = hClass.schema;
						else if (hm.schema != null)
							schema = hm.schema;

						ProcessCollection(hClass, keyNode, indexNode, hMap.ManyToMany(), GetClassName(hMap.Item1), schema, hMap.table, hMap.name, hMap.cascade, AssociationType.Map, hMap.where, hMap.fetch, hMap.inverse, hMap.lazy, hMap.orderby);
					}

					foreach (var hBag in hClass.Bags())
					{
						key keyNode = hBag.key;
						if (keyNode == null) throw new Exception("Cannot find key node in bag " + hBag.name);

						string schema = "";

						if (hBag.schema != null)
							schema = hBag.schema;
						else if (hClass.schema != null)
							schema = hClass.schema;
						else if (hm.schema != null)
							schema = hm.schema;

						ProcessCollection(hClass, keyNode, null, hBag.ManyToMany(), GetClassName(hBag.Item), schema, hBag.table, hBag.name, hBag.cascade, AssociationType.Bag, hBag.where, hBag.fetch, hBag.inverse, hBag.lazy, hBag.orderby);
					}

					foreach (var hList in hClass.Lists())
					{
						key keyNode = hList.key;
						index indexNode = hList.Index();
						if (keyNode == null) throw new Exception("Cannot find key node in list " + hList.name);
						if (indexNode == null) throw new Exception("Cannot find index node in list " + hList.name);

						string schema = "";

						if (hList.schema != null)
							schema = hList.schema;
						else if (hClass.schema != null)
							schema = hClass.schema;
						else if (hm.schema != null)
							schema = hm.schema;

						ProcessCollection(hClass, keyNode, indexNode, hList.ManyToMany(), GetClassName(hList.Item1), schema, hList.table, hList.name, hList.cascade, AssociationType.List, hList.where, hList.fetch, hList.inverse, hList.lazy, hList.orderby);
					}

					foreach (var hBag in hClass.IdBags())
					{
						key keyNode = hBag.key;
						if (keyNode == null) throw new Exception("Cannot find key node in idbag " + hBag.name);

						string schema = "";

						if (hBag.schema != null)
							schema = hBag.schema;
						else if (hClass.schema != null)
							schema = hClass.schema;
						else if (hm.schema != null)
							schema = hm.schema;

						ProcessCollection(hClass, keyNode, null, hBag.ManyToMany(), GetClassName(hBag.Item), schema, hBag.table, hBag.name, hBag.cascade, AssociationType.IDBag, hBag.where, hBag.fetch, hBag.inverse, hBag.lazy, hBag.orderby);
					}
				}
			}

			UseDiscoveredInformationToCreateReferences(mappingSet);
		}

		internal static IEnumerable<string> GetColumnNames(string column1, List<column> column)
		{
			if (string.IsNullOrEmpty(column1) == false)
			{
				yield return column1;
			}
			else if (column != null)
			{
				foreach (var col in column)
				{
					yield return col.name;
				}
			}
			else
			{
				yield break;
			}
		}

		internal static IEnumerable<string> GetColumnNames(string column1, List<string> columnNames)
		{
			if (string.IsNullOrEmpty(column1) == false)
			{
				yield return column1;
			}
			else if (columnNames.Count > 0)
			{
				foreach (var col in columnNames)
				{
					yield return col;
				}
			}
			else
			{
				yield break;
			}
		}

		private void ProcessCollection(
			@class hClass,
			key keyNode,
			index indexNode,
			manytomany many,
			string className,
			string schema,
			string tableName,
			string propertyName,
			string cascade,
			AssociationType associationType,
			string whereClause,
			collectionFetchMode fetchMode,
			bool inverse,
			collectionLazy lazy,
			string orderByClause)
		{
			#region OrderBy
			string orderByPropertyName = "";
			bool orderByIsAsc = true;

			if (!string.IsNullOrWhiteSpace(orderByClause))
			{
				orderByClause = orderByClause.Trim();

				if (orderByClause.EndsWith(" desc", StringComparison.InvariantCultureIgnoreCase))
				{
					orderByIsAsc = false;
					orderByPropertyName = orderByClause.Substring(0, orderByClause.LastIndexOf(" desc", StringComparison.InvariantCultureIgnoreCase)).Trim();
				}
				else if (orderByClause.EndsWith(" asc", StringComparison.InvariantCultureIgnoreCase))
				{
					orderByIsAsc = false;
					orderByPropertyName = orderByClause.Substring(0, orderByClause.LastIndexOf(" asc", StringComparison.InvariantCultureIgnoreCase)).Trim();
				}
				else
					orderByPropertyName = orderByClause;
			}
			#endregion

			string indexName = null;

			if (indexNode != null)
			{
				if (indexNode.column != null && indexNode.column.Count() > 0)
					indexName = indexNode.column[0].name;
				else
					indexName = indexNode.column1;
			}
			if (many != null)
			{
				var fkColumns = GetColumnNames(many.column, many.Columns()).ToList();
				string thisEntityName;
				string otherEntityName;
				EntityLoader.IsNameFullyQualified(hClass.name, out thisEntityName);
				EntityLoader.IsNameFullyQualified(many.@class, out otherEntityName);

				var collectionInfo = new AssociationInformation
										{
											PropertyName = propertyName,
											ForeignKeyColumnNames = fkColumns,
											ForeignKeyBelongsToThisTable = !ForeignKeyBelongsToThisTable(hClass, fkColumns),
											AssociationTableName = new AssociationInformation.TableNameType(schema, tableName),
											ThisEntityName = thisEntityName,
											OtherEntityName = otherEntityName,
											Cardinality = Cardinality.Many,
											CollectionCascade = ArchAngel.Interfaces.NHibernateEnums.Helper.GetCollectionCascadeType(cascade),
											CollectionLazy = ArchAngel.Interfaces.NHibernateEnums.Helper.GetCollectionLazyType(lazy.ToString()),
											CollectionFetchMode = (CollectionFetchModes)Enum.Parse(typeof(CollectionFetchModes), fetchMode.ToString(), true),
											IndexColumn = indexName,
											WhereClause = whereClause,
											AssociationType = associationType,
											Inverse = inverse ? ArchAngel.Interfaces.NHibernateEnums.BooleanInheritedTypes.@true : ArchAngel.Interfaces.NHibernateEnums.BooleanInheritedTypes.@false,
											OrderByColumnName = orderByPropertyName,
											OrderByIsAsc = orderByIsAsc
										};
				associationInformation.Add(collectionInfo);
			}
			else
			{
				var fkColumns = GetColumnNames(keyNode.column1, keyNode.Columns()).ToList();
				string thisEntityName;
				string otherEntityName;
				EntityLoader.IsNameFullyQualified(hClass.name, out thisEntityName);
				EntityLoader.IsNameFullyQualified(className, out otherEntityName);

				bool topLevelInverse = ArchAngel.Interfaces.SharedData.CurrentProject.GetProjectDefaultInverse();
				BooleanInheritedTypes inverseValue = inverse ? BooleanInheritedTypes.@true : BooleanInheritedTypes.@false;

				if ((inverseValue == BooleanInheritedTypes.@false && topLevelInverse == false) ||
					(inverseValue == BooleanInheritedTypes.@true && topLevelInverse == true))
					inverseValue = BooleanInheritedTypes.inherit_default;

				var collectionInfo = new AssociationInformation
										{
											PropertyName = propertyName,
											ForeignKeyColumnNames = fkColumns,
											ForeignKeyBelongsToThisTable = ForeignKeyBelongsToThisTable(hClass, fkColumns), // GFH
											ThisEntityName = thisEntityName,
											OtherEntityName = otherEntityName,
											Cardinality = Cardinality.Many,
											CollectionCascade = ArchAngel.Interfaces.NHibernateEnums.Helper.GetCollectionCascadeType(cascade),
											CollectionLazy = ArchAngel.Interfaces.NHibernateEnums.Helper.GetCollectionLazyType(lazy.ToString()),
											CollectionFetchMode = (CollectionFetchModes)Enum.Parse(typeof(CollectionFetchModes), fetchMode.ToString(), true),
											IndexColumn = indexName,
											AssociationType = associationType,
											Inverse = inverseValue,
											OrderByColumnName = orderByPropertyName,
											OrderByIsAsc = orderByIsAsc
										};
				associationInformation.Add(collectionInfo);
			}
		}

		private bool ForeignKeyBelongsToThisTable(@class hClass, List<string> fkColumns)
		{
			foreach (var fkCol in fkColumns)
				if (hClass.Properties().Count(p => p.name == fkCol.UnBackTick()) == 0)
					if (hClass.CompositeId() != null && hClass.CompositeId().KeyProperties().Count(p => p.name.ToLowerInvariant() == fkCol.UnBackTick().ToLowerInvariant()) == 0)
						return false;

			return true;
		}

		private void UseDiscoveredInformationToCreateReferences(MappingSet mappingSet)
		{
			while (associationInformation.Count > 0)
			{
				var info = associationInformation[0];
				associationInformation.RemoveAt(0);
				// Get existing objects
				var fromEntity = mappingSet.EntitySet.GetEntity(info.ThisEntityName);
				var fromTable = fromEntity.MappedTables().First();
				var toEntity = mappingSet.EntitySet.GetEntity(info.OtherEntityName);
				var toTable = toEntity.MappedTables().First();

				// Sort out this side of the reference.
				Reference reference = new ReferenceImpl();
				reference.Entity1 = fromEntity;
				reference.End1Name = info.PropertyName;
				reference.End1Enabled = true;
				reference.Cardinality1 = info.Cardinality;
				reference.SetEnd1AssociationType(info.AssociationType);
				reference.SetEnd1IndexColumnName(info.IndexColumn);
				reference.SetEnd1SqlWhereClause(info.WhereClause);
				reference.SetReferenceEnd1FetchMode(info.FetchMode);
				reference.SetReferenceEnd1CollectionFetchMode(info.CollectionFetchMode);
				reference.SetReferenceEnd1Insert(info.Insert);
				reference.SetReferenceEnd1Update(info.Update);
				reference.SetReferenceEnd1Inverse(info.Inverse);
				reference.SetReferenceEnd1FetchMode(info.FetchMode);
				reference.SetReferenceEnd1Lazy(info.CollectionLazy);
				reference.SetReferenceEnd1Cascade(info.Cascade);
				reference.SetReferenceEnd1CollectionCascade(info.CollectionCascade);

				if (!string.IsNullOrWhiteSpace(info.OrderByColumnName))
				{
					var orderByProp = fromEntity.Properties.SingleOrDefault(p => p.MappedColumn() != null && p.MappedColumn().Name.Equals(info.OrderByColumnName, StringComparison.InvariantCultureIgnoreCase));

					if (orderByProp != null)
						reference.SetReferenceEnd1OrderByProperty(orderByProp.Name);
				}
				reference.SetReferenceEnd1OrderByIsAsc(info.OrderByIsAsc);
				// Find the other side of the reference.
				ProcessOtherEndOfReference(info, toEntity, reference);
				//associationInformation.RemoveAt(0);
				fromEntity.AddReference(reference);

				if (fromEntity.InternalIdentifier != toEntity.InternalIdentifier)
					toEntity.AddReference(reference);

				if (info.AssociationTableName != null &&
					!string.IsNullOrEmpty(info.AssociationTableName.TableName))
				{
					// Map Reference to Table
					string schema = /*string.IsNullOrEmpty(info.AssociationTableName.SchemaName) ? "" :*/ info.AssociationTableName.SchemaName.UnBackTick();
					var mappedTable = mappingSet.Database.GetTable(info.AssociationTableName.TableName.UnBackTick(), schema);

					if (mappedTable == null)
						throw new NHibernateMappingException(string.Format("Could not find association table {0} to map to reference {1}.", info.AssociationTableName, reference.Name));

					mappingSet.ChangeMappingFor(reference).To(mappedTable);
				}
				else
				{
					IEnumerable<IColumn> foreignKeyColumns;
					Func<DirectedRelationship, bool> predicate;

					if (info.ForeignKeyColumnNames.Count == 0 && info.Cardinality.Start == 1 && info.Cardinality.End == 1 && fromTable != toTable)
					{
						predicate = r =>
							r.ToKey.Columns.OrderBy(c => c.Name).SequenceEqual(r.ToTable.ColumnsInPrimaryKey.OrderBy(c => c.Name)) &&
							r.FromKey.Columns.OrderBy(c => c.Name).SequenceEqual(r.FromTable.ColumnsInPrimaryKey.OrderBy(c => c.Name));
					}
					else
					{
						if (info.ForeignKeyBelongsToThisTable)
						{
							foreignKeyColumns = info.ForeignKeyColumnNames.Select(f => fromTable.GetColumn(f.UnBackTick()));

							if (fromTable == toTable)
							{
								// Self referencing keys might have the primary key at either end.
								predicate = r => (r.FromKey.Columns.SequenceEqual(foreignKeyColumns) || r.ToKey.Columns.SequenceEqual(foreignKeyColumns));
							}
							else
								predicate = r => r.FromKey.Columns.SequenceEqual(foreignKeyColumns);
						}
						else
						{
							foreignKeyColumns = info.ForeignKeyColumnNames.Select(f => toTable.GetColumn(f.UnBackTick()));
							predicate = r => r.ToKey.Columns.SequenceEqual(foreignKeyColumns);
						}
					}
					var possibleRelationships = fromTable.DirectedRelationships.Where(r => r.ToTable == toTable);
					var relationshipToMap = possibleRelationships.FirstOrDefault(predicate);

					if (relationshipToMap != null)
						mappingSet.ChangeMappingFor(reference).To(relationshipToMap.Relationship);
					else
						throw new NHibernateMappingException(string.Format("Could not find relationship to map to for reference between Entities \"{0}\" and \"{1}\"", info.ThisEntityName, info.OtherEntityName));
				}
			}
		}

		private void ProcessOtherEndOfReference(AssociationInformation info, Entity toEntity, Reference reference)
		{
			var potentialOtherEnds =
				associationInformation.Where(inf => inf.ThisEntityName == info.OtherEntityName
				&& inf.OtherEntityName == info.ThisEntityName);

			AssociationInformation otherEndInfo = null;

			if (potentialOtherEnds.Count() == 1)
				otherEndInfo = potentialOtherEnds.FirstOrDefault();
			else if (potentialOtherEnds.Count() > 1)
			{
				foreach (var pot in potentialOtherEnds)
				{
					if (info.ForeignKeyColumnNames.Count == pot.ForeignKeyColumnNames.Count)
					{
						bool found = true;

						for (int i = 0; i < info.ForeignKeyColumnNames.Count; i++)
						{
							if (pot.ForeignKeyColumnNames[i] != info.ForeignKeyColumnNames[i])
							{
								found = false;
								break;
							}
						}
						if (found)
						{
							otherEndInfo = pot;
							break;
						}
					}
				}
				//log.Error("While processing references, ");
			}
			reference.Entity2 = toEntity;

			if (info.ForeignKeyColumnNames.Count == 0 && otherEndInfo != null)
			{
				info.ForeignKeyColumnNames = otherEndInfo.ForeignKeyColumnNames;
				info.ForeignKeyBelongsToThisTable = false;
			}
			if (otherEndInfo == null)
			{
				reference.End2Enabled = false;
				Cardinality cardinality;

				if (info.AssociationTableName != null && !string.IsNullOrEmpty(info.AssociationTableName.TableName))
					cardinality = Cardinality.Many;
				else if (info.HasUniqueConstraint)
					cardinality = Cardinality.One;
				else
					cardinality = Cardinality.Many;

				reference.Cardinality2 = cardinality;
			}
			else
			{
				reference.End2Enabled = true;
				reference.End2Name = otherEndInfo.PropertyName;
				reference.Cardinality2 = otherEndInfo.Cardinality;
				reference.SetEnd2AssociationType(otherEndInfo.AssociationType);
				reference.SetEnd2IndexColumnName(otherEndInfo.IndexColumn);
				reference.SetEnd2SqlWhereClause(otherEndInfo.WhereClause);
				reference.SetReferenceEnd2FetchMode(otherEndInfo.FetchMode);
				reference.SetReferenceEnd2CollectionFetchMode(otherEndInfo.CollectionFetchMode);
				reference.SetReferenceEnd2Insert(otherEndInfo.Insert);
				reference.SetReferenceEnd2Update(otherEndInfo.Update);
				reference.SetReferenceEnd2Inverse(otherEndInfo.Inverse);
				reference.SetReferenceEnd2Cascade(otherEndInfo.Cascade);
				reference.SetReferenceEnd2CollectionCascade(otherEndInfo.CollectionCascade);
				reference.SetReferenceEnd2Lazy(otherEndInfo.CollectionLazy);
				reference.SetReferenceEnd2OrderByIsAsc(otherEndInfo.OrderByIsAsc);

				if (!string.IsNullOrWhiteSpace(otherEndInfo.OrderByColumnName))
				{
					var orderByProp = toEntity.Properties.SingleOrDefault(p => p.MappedColumn() != null && p.MappedColumn().Name.Equals(otherEndInfo.OrderByColumnName, StringComparison.InvariantCultureIgnoreCase));

					if (orderByProp != null)
						reference.SetReferenceEnd2OrderByProperty(orderByProp.Name);
				}
				//reference.SetReferenceEnd2OrderByProperty(otherEndInfo.OrderByColumnName);
				associationInformation.Remove(otherEndInfo);
			}
		}

		private static string GetClassName(object item1)
		{
			if (item1 == null)
				throw new Exception("Cannot find the a mapping node for the current set.");

			if (item1 is onetomany)
				return ((onetomany)item1).@class;

			if (item1 is manytomany)
				return ((manytomany)item1).@class;

			if (item1 is onetoone)
				return ((onetoone)item1).@class;

			throw new Exception("Given a node without a class attribute: " + item1.GetType().Name);
		}
	}
}