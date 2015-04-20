using System;
using System.Collections.Generic;
using System.Linq;
using ArchAngel.Interfaces;
using ArchAngel.Interfaces.NHibernateEnums;
using ArchAngel.NHibernateHelper.EntityExtensions;
using ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.ExtensionMethods;
using ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Mapping;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using log4net;
using Slyce.Common.StringExtensions;

namespace ArchAngel.NHibernateHelper.MappingFiles.Version_2_2
{
	public class ReferenceMapper
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(EntityMapper));

		public void ProcessReferences(Entity entity, Action<object> addItem)
		{
			bool topLevelLazy = ArchAngel.Interfaces.SharedData.CurrentProject.GetProjectDefaultCollectionLazy();
			ArchAngel.Interfaces.NHibernateEnums.TopLevelAccessTypes topLevelAccess = ArchAngel.Interfaces.SharedData.CurrentProject.GetProjectDefaultAccess();
			ArchAngel.Interfaces.NHibernateEnums.TopLevelCascadeTypes topLevelCascade = ArchAngel.Interfaces.SharedData.CurrentProject.GetProjectDefaultCascade();
			ArchAngel.Interfaces.NHibernateEnums.CollectionLazyTypes entityDefaultLazy;
			ArchAngel.Interfaces.NHibernateEnums.CascadeTypes entityDefaultCascade;
			ArchAngel.Interfaces.NHibernateEnums.PropertyAccessTypes entityDefaultAccess;
			NHibernateProjectPreprocessor.GetEntityCollectionDefaults(topLevelLazy, topLevelCascade, topLevelAccess, entity, out entityDefaultLazy, out entityDefaultCascade, out entityDefaultAccess);
			Dictionary<ITable, int> manyToManySameTableProcessingCounts = new Dictionary<ITable, int>();

			foreach (var reference in entity.DirectedReferences.OrderBy(r => r.FromName))
			{
				#region Handle default values

				#region Cascade
				string cascade = reference.Entity1IsFromEnd ? reference.Reference.GetReferenceEnd1Cascade().ToString() : reference.Reference.GetReferenceEnd2Cascade().ToString();

				if (cascade == ArchAngel.Interfaces.NHibernateEnums.CascadeTypes.inherit_default.ToString())
					cascade = entityDefaultCascade.ToString();

				cascade = cascade.Replace("_", "-");
				#endregion

				#region Lazy
				string lazy = reference.Entity1IsFromEnd ? reference.Reference.GetReferenceEnd1Lazy().ToString() : reference.Reference.GetReferenceEnd2Lazy().ToString();

				if (lazy == CollectionLazyTypes.inherit_default.ToString())
					lazy = entityDefaultLazy.ToString().Replace("@", "");

				lazy = lazy.ToString().Replace("@", "");
				#endregion

				#region CollectionCascade
				string collectionCascade = reference.Entity1IsFromEnd ? reference.Reference.GetReferenceEnd1CollectionCascade().ToString() : reference.Reference.GetReferenceEnd2CollectionCascade().ToString();

				if (collectionCascade == CollectionCascadeTypes.inherit_default.ToString())
					collectionCascade = entityDefaultCascade.ToString();

				collectionCascade = collectionCascade.Replace("_", "-");
				#endregion

				#region Inverse
				bool topLevelInverse = ArchAngel.Interfaces.SharedData.CurrentProject.GetProjectDefaultInverse();
				BooleanInheritedTypes inverseIneritedType = reference.FromEntity == reference.Reference.Entity1 ? reference.Reference.GetReferenceEnd1Inverse() : reference.Reference.GetReferenceEnd2Inverse();

				if (inverseIneritedType == BooleanInheritedTypes.inherit_default)
				{
					if (topLevelInverse) inverseIneritedType = BooleanInheritedTypes.@true;
					else inverseIneritedType = BooleanInheritedTypes.@false;
				}
				bool inverse = inverseIneritedType == BooleanInheritedTypes.@true;
				#endregion

				#region OrderBy
				Property orderByProperty = null;
				string orderByColumnName = "";
				bool orderByIsAsc = true;
				string orderByClause = "";

				if (reference.Entity1IsFromEnd)
				{
					orderByIsAsc = reference.Reference.GetReferenceEnd1OrderByIsAsc();
					orderByProperty = reference.Reference.GetReferenceEnd1OrderByProperty();
				}
				else
				{
					orderByIsAsc = reference.Reference.GetReferenceEnd2OrderByIsAsc();
					orderByProperty = reference.Reference.GetReferenceEnd2OrderByProperty();
				}
				if (orderByProperty != null)
				{
					var col = orderByProperty.MappedColumn();

					if (col != null)
						orderByColumnName = col.Name;

					if (orderByColumnName.Length > 0)
					{
						if (orderByIsAsc)
							orderByClause = orderByColumnName;
						else
							orderByClause = orderByColumnName + " desc";
					}
				}

				#endregion

				#endregion

				switch (DetermineReferenceType(reference.Reference))
				{
					case ReferenceType.OneToOne:
						ProcessOneToOneReference(entity, reference, addItem, cascade, lazy);
						break;
					case ReferenceType.ManyToOne:
						ProcessManyToOneReference(entity, reference, addItem, cascade, collectionCascade, lazy, orderByClause, inverse);
						break;
					case ReferenceType.ManyToMany:
						ProcessManyToManyReference(entity, reference, addItem, manyToManySameTableProcessingCounts, collectionCascade, lazy, orderByClause, inverse);
						break;
					case ReferenceType.Unsupported:
						throw new NotImplementedException(string.Format("The cardinalities on this reference have not been handled yet: {0} to {1}. (Between {2} and {3})", reference.Reference.Cardinality1, reference.Reference.Cardinality2, reference.FromEntity.Name, reference.ToEntity.Name));
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}

		private void ProcessManyToManyReference(
			Entity entity,
			DirectedReference directedReference,
			Action<object> addItem,
			Dictionary<ITable, int> manyToManySameTableProcessingCounts,
			string collectionCascade,
			string lazy,
			string orderByClause,
			bool inverse)
		{
			if (directedReference.FromEndEnabled == false) return;

			ITable referenceMappedTable = directedReference.Reference.MappedTable();

			ITable fromPrimaryMappedTable = EntityMapper.GetPrimaryTable(entity);
			ITable toPrimaryMappedTable = EntityMapper.GetPrimaryTable(directedReference.ToEntity);
			Cardinality cardinalityPrimary;
			Cardinality cardinalityForeign;
			IKey mainKey;
			IKey associationKey;
			ITable associationTable = entity.GetAssociationTable(directedReference.ToEntity, out cardinalityPrimary, out cardinalityForeign, out mainKey, out associationKey);

			key keyNode = new key();
			List<IColumn> fromInPrimaryKey = new List<IColumn>();
			List<IColumn> toInPrimaryKey = new List<IColumn>();

			if (fromPrimaryMappedTable == toPrimaryMappedTable)
			{
				// This many-to-many relationship is to the same table
				if (manyToManySameTableProcessingCounts.ContainsKey(toPrimaryMappedTable))
				{
					int index = manyToManySameTableProcessingCounts[toPrimaryMappedTable];
					index++;
					fromInPrimaryKey.AddRange(referenceMappedTable.Relationships.Where(t => t.PrimaryTable == fromPrimaryMappedTable).ElementAt(index).PrimaryKey.Columns);
					toInPrimaryKey.AddRange(referenceMappedTable.Relationships.Where(t => t.PrimaryTable == toPrimaryMappedTable).ElementAt(index).ForeignKey.Columns);
					manyToManySameTableProcessingCounts[toPrimaryMappedTable] = index;
				}
				else
				{
					fromInPrimaryKey.AddRange(referenceMappedTable.Relationships.Where(t => t.PrimaryTable == fromPrimaryMappedTable).ElementAt(0).PrimaryKey.Columns);
					toInPrimaryKey.AddRange(referenceMappedTable.Relationships.Where(t => t.PrimaryTable == toPrimaryMappedTable).ElementAt(0).ForeignKey.Columns);
					manyToManySameTableProcessingCounts.Add(toPrimaryMappedTable, 0);
				}
			}
			else
			{
				foreach (var coll in referenceMappedTable.Relationships.Where(t => t.PrimaryTable == fromPrimaryMappedTable).Select(r => r.ForeignKey.Columns))
					foreach (var c in coll)
						fromInPrimaryKey.Add(c);

				foreach (var coll in referenceMappedTable.Relationships.Where(t => t.PrimaryTable == toPrimaryMappedTable).Select(r => r.ForeignKey.Columns))
					foreach (var c in coll)
						toInPrimaryKey.Add(c);
			}

			if (fromInPrimaryKey.Count() == 1)
				keyNode.column1 = fromInPrimaryKey.First().Name.BackTick();
			else
				foreach (var columnNode in GetColumnNodes(fromInPrimaryKey))
					keyNode.AddColumn(columnNode);

			manytomany manyToManyNode = new manytomany();
			manyToManyNode.@class = directedReference.ToEntity.Name;

			if (toInPrimaryKey.Count() == 1)
				manyToManyNode.column = toInPrimaryKey.First().Name.BackTick();
			else
				foreach (var columnNode in GetColumnNodes(toInPrimaryKey))
					keyNode.AddColumn(columnNode);

			collectionFetchMode collFetchMode;

			if (directedReference.Entity1IsFromEnd)
				collFetchMode = (collectionFetchMode)Enum.Parse(typeof(collectionFetchMode), directedReference.Reference.GetReferenceEnd1CollectionFetchMode().ToString(), true);
			else
				collFetchMode = (collectionFetchMode)Enum.Parse(typeof(collectionFetchMode), directedReference.Reference.GetReferenceEnd2CollectionFetchMode().ToString(), true);

			AssociationType type = NHCollections.GetAssociationType(directedReference);

			switch (type)
			{
				case AssociationType.None:
					Log.WarnFormat("No association type was set on reference {0} for the end {1}. This is usually an error.", directedReference.Reference.Name, directedReference.Entity1IsFromEnd ? "One" : "Two");
					return;
				case AssociationType.Set:
					set setNode = CreateSetNode(directedReference, keyNode, collectionCascade, collFetchMode, lazy, inverse);
					setNode.table = referenceMappedTable.Name.BackTick();
					setNode.Item = manyToManyNode;

					if (orderByClause.Length > 0)
						setNode.orderby = orderByClause;

					addItem(setNode);
					break;
				case AssociationType.Bag:
					bag bagNode = CreateBagNode(directedReference, keyNode, collectionCascade, collFetchMode, lazy, inverse);
					bagNode.table = referenceMappedTable.Name.BackTick();
					bagNode.Item = manyToManyNode;

					if (orderByClause.Length > 0)
						bagNode.orderby = orderByClause;

					addItem(bagNode);
					break;
				case AssociationType.Map:
					map mapNode = CreateMapNode(directedReference, keyNode, collectionCascade, collFetchMode, lazy, inverse);
					mapNode.table = referenceMappedTable.Name.BackTick();
					mapNode.Item = new index
										{
											column1 = NHCollections.GetIndexColumnName(directedReference),
											type = NHCollections.GetIndexColumnTypeName(directedReference, toPrimaryMappedTable /*fromPrimaryMappedTable*/)
										};
					mapNode.Item1 = manyToManyNode;

					if (orderByClause.Length > 0)
						mapNode.orderby = orderByClause;

					addItem(mapNode);
					break;
				case AssociationType.List:
					list listNode = CreateListNode(directedReference, keyNode, collectionCascade, collFetchMode, lazy, inverse);
					listNode.table = referenceMappedTable.Name.BackTick();
					listNode.Item = new index
					{
						column1 = NHCollections.GetIndexColumnName(directedReference),
					};
					listNode.Item1 = manyToManyNode;

					if (orderByClause.Length > 0)
						listNode.orderby = orderByClause;

					addItem(listNode);
					break;
				// case AssociationType.IDBag:
				//     throw new NotImplementedException(
				//         string.Format("Have not implemented {0} association type for Many To Many relationships", type));
				default:
					throw new NotImplementedException("AssociationType not handled yet: " + type.ToString());
			}
		}

		private void ProcessOneToOneReference(Entity entity, DirectedReference directedReference, Action<object> addItem, string cascade, string lazy)
		{
			if (directedReference.FromEndEnabled == false) return;

			DirectedRelationship directedRelationship = GetDirectedMappedRelationship(entity, directedReference.Reference);

			fetchMode fetchMode;

			if (directedReference.Entity1IsFromEnd)
				fetchMode = (fetchMode)Enum.Parse(typeof(fetchMode), directedReference.Reference.GetReferenceEnd1FetchMode().ToString(), true);
			else
				fetchMode = (fetchMode)Enum.Parse(typeof(fetchMode), directedReference.Reference.GetReferenceEnd2FetchMode().ToString(), true);

			// if this side has the foreign key, it gets the many to one node
			if (directedRelationship.FromKey.Keytype == DatabaseKeyType.Foreign)
			{
				manytoone manyToOneNode = CreateManyToOneNode(directedReference, directedRelationship, cascade);
				manyToOneNode.unique = true;
				manyToOneNode.fetch = fetchMode;
				manyToOneNode.fetchSpecified = true;

				addItem(manyToOneNode);
			}
			else
			{
				onetoone oneToOneNode = new onetoone();
				oneToOneNode.@class = directedReference.ToEntity.Name;
				oneToOneNode.name = directedReference.FromName;
				oneToOneNode.propertyref = directedReference.ToName;
				oneToOneNode.fetch = fetchMode;
				oneToOneNode.fetchSpecified = true;

				addItem(oneToOneNode);
			}
		}

		private void ProcessManyToOneReference(
			Entity entity,
			DirectedReference directedReference,
			Action<object> addItem,
			string cascade,
			string collectionCascade,
			string lazy,
			string orderByClause,
			bool inverse)
		{
			if (directedReference.FromEndEnabled == false) return;

			ITable referenceMappedTable = directedReference.Reference.MappedTable();
			DirectedRelationship directedRelationship = null;

			if (referenceMappedTable == null)
				directedRelationship = GetDirectedMappedRelationship(entity, directedReference.Reference);

			if (directedReference.FromEndCardinality == Cardinality.One)
			{
				fetchMode fetchMode;
				bool insert;
				bool update;

				if (directedReference.Entity1IsFromEnd)
				{
					fetchMode = (fetchMode)Enum.Parse(typeof(fetchMode), directedReference.Reference.GetReferenceEnd1FetchMode().ToString(), true);
					insert = directedReference.Reference.GetReferenceEnd1Insert();
					update = directedReference.Reference.GetReferenceEnd1Update();
				}
				else
				{
					fetchMode = (fetchMode)Enum.Parse(typeof(fetchMode), directedReference.Reference.GetReferenceEnd2FetchMode().ToString(), true);
					insert = directedReference.Reference.GetReferenceEnd2Insert();
					update = directedReference.Reference.GetReferenceEnd2Update();
				}
				manytoone manyToOneNode;

				if (referenceMappedTable == null)
					manyToOneNode = CreateManyToOneNode(directedReference, directedRelationship, cascade);
				else
					manyToOneNode = CreateManyToOneNode(directedReference, referenceMappedTable);

				manyToOneNode.fetch = fetchMode;
				manyToOneNode.fetchSpecified = true;
				manyToOneNode.insert = insert;
				manyToOneNode.update = update;

				addItem(manyToOneNode);
			}
			else
			{
				key keyNode = new key();

				if (referenceMappedTable == null &&
					directedRelationship.ToKey.Columns.Count > 1)
				{
					foreach (var columnNode in GetColumnNodes(directedRelationship.ToKey.Columns))
						keyNode.AddColumn(columnNode);
				}
				else if (referenceMappedTable != null)
				{
					ITable toPrimaryMappedTable = EntityMapper.GetPrimaryTable(directedReference.ToEntity);
					var toColumnsInPrimaryKey = referenceMappedTable.Relationships.First(t => t.PrimaryTable == toPrimaryMappedTable || t.ForeignTable == toPrimaryMappedTable).ForeignKey.Columns;

					foreach (var columnNode in GetColumnNodes(toColumnsInPrimaryKey))
						keyNode.AddColumn(columnNode);
				}
				else
					keyNode.column1 = directedRelationship.ToKey.Columns[0].Name.BackTick();

				onetomany oneToManyNode = new onetomany();
				oneToManyNode.@class = directedReference.ToEntity.Name;

				collectionFetchMode collFetchMode;

				if (directedReference.Entity1IsFromEnd)
					collFetchMode = (collectionFetchMode)Enum.Parse(typeof(collectionFetchMode), directedReference.Reference.GetReferenceEnd1CollectionFetchMode().ToString(), true);
				else
					collFetchMode = (collectionFetchMode)Enum.Parse(typeof(collectionFetchMode), directedReference.Reference.GetReferenceEnd2CollectionFetchMode().ToString(), true);

				AssociationType type = NHCollections.GetAssociationType(directedReference);

				switch (type)
				{
					case AssociationType.None:
						Log.WarnFormat("No association type was set on reference {0} for the end {1}. This is usually an error.", directedReference.Reference.Name, directedReference.Entity1IsFromEnd ? "1" : "2");
						return;
					case AssociationType.Set:
						var set = CreateSetNode(directedReference, keyNode, collectionCascade, collFetchMode, lazy, inverse);
						set.Item = oneToManyNode;

						if (orderByClause.Length > 0)
							set.orderby = orderByClause;

						addItem(set);
						break;
					case AssociationType.Map:
						var mapNode = CreateMapNode(directedReference, keyNode, collectionCascade, collFetchMode, lazy, inverse);
						mapNode.Item = new index
						{
							column1 = NHCollections.GetIndexColumnName(directedReference),
							type = NHCollections.GetIndexColumnTypeName(directedReference, EntityMapper.GetPrimaryTable(directedReference.ToEntity))
						};
						mapNode.Item1 = oneToManyNode;

						if (orderByClause.Length > 0)
							mapNode.orderby = orderByClause;

						addItem(mapNode);
						break;
					case AssociationType.Bag:
						var bag = CreateBagNode(directedReference, keyNode, collectionCascade, collFetchMode, lazy, inverse);
						bag.Item = oneToManyNode;

						if (orderByClause.Length > 0)
							bag.orderby = orderByClause;

						addItem(bag);
						break;
					case AssociationType.List:
						list listNode = CreateListNode(directedReference, keyNode, collectionCascade, collFetchMode, lazy, inverse);
						listNode.Item = new index { column1 = NHCollections.GetIndexColumnName(directedReference) };
						listNode.Item1 = oneToManyNode;

						if (orderByClause.Length > 0)
							listNode.orderby = orderByClause;

						addItem(listNode);
						break;
					case AssociationType.IDBag:
						idbag idbagNode = CreateIdBagNode(directedReference, keyNode, collectionCascade, collFetchMode, lazy, inverse);
						idbagNode.collectionid = new collectionid
						{
							column1 = NHCollections.GetIndexColumnName(directedReference),
							generator = new generator { @class = "sequence" },
							type = NHCollections.GetIndexColumnTypeName(directedReference, EntityMapper.GetPrimaryTable(directedReference.ToEntity))
						};

						addItem(idbagNode);
						break;
					default:
						throw new ArgumentOutOfRangeException("AssociationType not handled yet: " + type.ToString());
				}
			}
		}

		public static ReferenceType DetermineReferenceType(Reference reference)
		{
			if (reference.Cardinality1 == Cardinality.Zero && reference.Cardinality2 == Cardinality.Zero)
				return ReferenceType.OneToOne;

			if (reference.Cardinality1 == Cardinality.One && reference.Cardinality2 == Cardinality.One)
				return ReferenceType.OneToOne;

			if (reference.Cardinality1 == Cardinality.One && reference.Cardinality2 == Cardinality.Zero)
				return ReferenceType.OneToOne;

			if (reference.Cardinality1 == Cardinality.Zero && reference.Cardinality2 == Cardinality.One)
				return ReferenceType.OneToOne;

			if (reference.Cardinality1 == Cardinality.One && reference.Cardinality2 == Cardinality.Many)
				return ReferenceType.ManyToOne;

			if (reference.Cardinality1 == Cardinality.Many && reference.Cardinality2 == Cardinality.One)
				return ReferenceType.ManyToOne;

			if (reference.Cardinality1 == Cardinality.Many && reference.Cardinality2 == Cardinality.Zero)
				return ReferenceType.ManyToOne;

			if (reference.Cardinality1 == Cardinality.Zero && reference.Cardinality2 == Cardinality.Many)
				return ReferenceType.ManyToOne;

			if (reference.Cardinality1 == Cardinality.Many && reference.Cardinality2 == Cardinality.Many)
				return ReferenceType.ManyToMany;

			return ReferenceType.Unsupported;
		}


		private manytoone CreateManyToOneNode(DirectedReference directedReference, DirectedRelationship directedRelationship, string cascade)
		{
			fetchMode fetchMode;
			bool insert;
			bool update;

			if (directedReference.Entity1IsFromEnd)
			{
				fetchMode = (fetchMode)Enum.Parse(typeof(fetchMode), directedReference.Reference.GetReferenceEnd1FetchMode().ToString(), true);
				insert = directedReference.Reference.GetReferenceEnd1Insert();
				update = directedReference.Reference.GetReferenceEnd1Update();
			}
			else
			{
				fetchMode = (fetchMode)Enum.Parse(typeof(fetchMode), directedReference.Reference.GetReferenceEnd2FetchMode().ToString(), true);
				insert = directedReference.Reference.GetReferenceEnd2Insert();
				update = directedReference.Reference.GetReferenceEnd2Update();
			}
			manytoone manyToOneNode = new manytoone();
			manyToOneNode.@class = directedReference.ToEntity.Name;
			manyToOneNode.name = directedReference.FromName;
			bool notNullableColumnsExist = directedRelationship.MappedColumns.Any(mc => !mc.Source.IsNullable);
			manyToOneNode.notnull = notNullableColumnsExist;
			manyToOneNode.notnullSpecified = true;
			manyToOneNode.fetch = fetchMode;
			manyToOneNode.fetchSpecified = true;
			manyToOneNode.insert = insert;
			manyToOneNode.update = update;
			manyToOneNode.cascade = cascade == "none" ? null : cascade;

			if (directedRelationship.ToTable == directedRelationship.FromTable)
			{
				if (directedRelationship.ToKey.Keytype == Providers.EntityModel.Helper.DatabaseKeyType.Primary)
					if (directedRelationship.FromKey.Columns.Count > 1)
						foreach (var column in GetColumnNodes(directedRelationship.FromKey.Columns))
							manyToOneNode.AddColumn(column);
					else
						manyToOneNode.column = directedRelationship.FromKey.Columns[0].Name.BackTick();
				else
					if (directedRelationship.ToKey.Columns.Count > 1)
						foreach (var column in GetColumnNodes(directedRelationship.ToKey.Columns))
							manyToOneNode.AddColumn(column);
					else
						manyToOneNode.column = directedRelationship.ToKey.Columns[0].Name.BackTick();
			}
			else
			{
				if (directedRelationship.FromKey.Columns.Count > 1)
					foreach (var column in GetColumnNodes(directedRelationship.FromKey.Columns))
						manyToOneNode.AddColumn(column);
				else
					manyToOneNode.column = directedRelationship.FromKey.Columns[0].Name.BackTick();
			}
			return manyToOneNode;
		}

		private manytoone CreateManyToOneNode(DirectedReference directedReference, ITable referenceMappedTable)
		{
			fetchMode fetchMode;
			bool insert;
			bool update;

			if (directedReference.Entity1IsFromEnd)
			{
				fetchMode = (fetchMode)Enum.Parse(typeof(fetchMode), directedReference.Reference.GetReferenceEnd1FetchMode().ToString(), true);
				insert = directedReference.Reference.GetReferenceEnd1Insert();
				update = directedReference.Reference.GetReferenceEnd1Update();
			}
			else
			{
				fetchMode = (fetchMode)Enum.Parse(typeof(fetchMode), directedReference.Reference.GetReferenceEnd2FetchMode().ToString(), true);
				insert = directedReference.Reference.GetReferenceEnd2Insert();
				update = directedReference.Reference.GetReferenceEnd2Update();
			}
			manytoone manyToOneNode = new manytoone();
			manyToOneNode.@class = directedReference.ToEntity.Name;
			manyToOneNode.name = directedReference.FromName;
			//bool notNullableColumnsExist = directedRelationship.MappedColumns.Any(mc => !mc.Source.IsNullable);
			manyToOneNode.notnull = true;
			manyToOneNode.notnullSpecified = true;
			manyToOneNode.fetch = fetchMode;
			manyToOneNode.fetchSpecified = true;
			manyToOneNode.insert = insert;
			manyToOneNode.update = update;

			ITable fromPrimaryMappedTable = EntityMapper.GetPrimaryTable(directedReference.FromEntity);
			List<IColumn> fromKeyColumns = referenceMappedTable.Relationships.First(t => t.PrimaryTable == fromPrimaryMappedTable || t.ForeignTable == fromPrimaryMappedTable).ForeignKey.Columns.ToList();

			if (fromKeyColumns.Count > 1)
				foreach (var columnNode in GetColumnNodes(fromKeyColumns))
					manyToOneNode.AddColumn(columnNode);
			else
				manyToOneNode.column = fromKeyColumns[0].Name.BackTick();

			return manyToOneNode;
		}

		private static IEnumerable<column> GetColumnNodes(IEnumerable<IColumn> columns)
		{
			List<column> columnNodes = new List<column>();

			foreach (var mappedColumn in columns)
			{
				var columnNode = new column { name = mappedColumn.Name.BackTick() };
				columnNodes.Add(columnNode);
			}
			return columnNodes;
		}

		public static DirectedRelationship GetDirectedMappedRelationship(Entity entity, Reference reference)
		{
			Relationship relationship = reference.MappedRelationship();
			if (relationship == null)
				throw new NHibernateMappingException("Could not find mapped relationship for Reference: " +
													 reference.Name);

			ITable mappedTable = EntityMapper.GetPrimaryTable(entity);
			return new DirectedRelationship(relationship, mappedTable);
		}

		private bag CreateBagNode(DirectedReference reference, key keyNode, string cascade, collectionFetchMode colFetchMode, string lazyString, bool inverse)
		{
			var lazy = (collectionLazy)Enum.Parse(typeof(collectionLazy), lazyString.ToString().Replace("_", ""), true);

			bag bagNode = new bag();
			bagNode.name = reference.FromName;
			bagNode.key = keyNode;
			bagNode.cascade = cascade == "none" ? null : cascade;
			bagNode.inverse = inverse;

			bagNode.fetchSpecified = colFetchMode != collectionFetchMode.select;
			bagNode.fetch = colFetchMode;
			bagNode.lazySpecified = lazy != collectionLazy.@true;
			bagNode.lazy = (collectionLazy)Enum.Parse(typeof(collectionLazy), lazy.ToString().Replace("_", ""), true);

			var sqlWhereClause = NHCollections.GetSqlWhereClause(reference);

			if (string.IsNullOrEmpty(sqlWhereClause) == false)
				bagNode.where = sqlWhereClause;

			return bagNode;
		}

		private idbag CreateIdBagNode(DirectedReference reference, key keyNode, string cascade, collectionFetchMode colFetchMode, string lazyString, bool inverse)
		{
			var lazy = (collectionLazy)Enum.Parse(typeof(collectionLazy), lazyString.ToString().Replace("_", ""), true);

			idbag idbagNode = new idbag();
			idbagNode.name = reference.FromName;
			idbagNode.key = keyNode;
			idbagNode.cascade = cascade == "none" ? null : cascade;
			idbagNode.inverse = inverse;

			idbagNode.fetchSpecified = colFetchMode != collectionFetchMode.select;
			idbagNode.fetch = colFetchMode;
			idbagNode.lazySpecified = lazy != collectionLazy.@true;
			idbagNode.lazy = (collectionLazy)Enum.Parse(typeof(collectionLazy), lazy.ToString().Replace("_", ""), true);

			var sqlWhereClause = NHCollections.GetSqlWhereClause(reference);

			if (string.IsNullOrEmpty(sqlWhereClause) == false)
				idbagNode.where = sqlWhereClause;

			return idbagNode;
		}

		private set CreateSetNode(DirectedReference reference, key keyNode, string cascade, collectionFetchMode colFetchMode, string lazyString, bool inverse)
		{
			var lazy = (collectionLazy)Enum.Parse(typeof(collectionLazy), lazyString.ToString().Replace("_", ""), true);

			set setNode = new set();
			setNode.name = reference.FromName;
			setNode.key = keyNode;
			setNode.cascade = cascade == "none" ? null : cascade;
			setNode.inverse = inverse;

			setNode.fetchSpecified = colFetchMode != collectionFetchMode.select;
			setNode.fetch = colFetchMode;
			setNode.lazySpecified = lazy != collectionLazy.@true;
			setNode.lazy = (collectionLazy)Enum.Parse(typeof(collectionLazy), lazy.ToString().Replace("_", ""), true);

			var sqlWhereClause = NHCollections.GetSqlWhereClause(reference);

			if (string.IsNullOrEmpty(sqlWhereClause) == false)
				setNode.where = sqlWhereClause;

			return setNode;
		}

		private list CreateListNode(DirectedReference reference, key keyNode, string cascade, collectionFetchMode colFetchMode, string lazyString, bool inverse)
		{
			var lazy = (collectionLazy)Enum.Parse(typeof(collectionLazy), lazyString.ToString().Replace("_", ""), true);

			list listNode = new list();
			listNode.name = reference.FromName;
			listNode.key = keyNode;
			listNode.cascade = cascade == "none" ? null : cascade;
			listNode.inverse = inverse;

			listNode.fetchSpecified = colFetchMode != collectionFetchMode.select;
			listNode.fetch = colFetchMode;
			listNode.lazySpecified = lazy != collectionLazy.@true;
			listNode.lazy = (collectionLazy)Enum.Parse(typeof(collectionLazy), lazy.ToString().Replace("_", ""), true);

			var sqlWhereClause = NHCollections.GetSqlWhereClause(reference);

			if (string.IsNullOrEmpty(sqlWhereClause) == false)
				listNode.where = sqlWhereClause;

			return listNode;
		}

		private map CreateMapNode(DirectedReference reference, key keyNode, string cascade, collectionFetchMode colFetchMode, string lazyString, bool inverse)
		{
			var lazy = (collectionLazy)Enum.Parse(typeof(collectionLazy), lazyString.ToString().Replace("_", ""), true);

			map mapNode = new map();
			mapNode.name = reference.FromName;
			mapNode.key = keyNode;
			mapNode.cascade = cascade == "none" ? null : cascade;
			mapNode.inverse = inverse;

			mapNode.fetchSpecified = colFetchMode != collectionFetchMode.select;
			mapNode.fetch = colFetchMode;
			mapNode.lazySpecified = lazy != collectionLazy.@true;
			mapNode.lazy = (collectionLazy)Enum.Parse(typeof(collectionLazy), lazy.ToString().Replace("_", ""), true);

			var sqlWhereClause = NHCollections.GetSqlWhereClause(reference);

			if (string.IsNullOrEmpty(sqlWhereClause) == false)
				mapNode.where = sqlWhereClause;

			return mapNode;
		}
	}
}
