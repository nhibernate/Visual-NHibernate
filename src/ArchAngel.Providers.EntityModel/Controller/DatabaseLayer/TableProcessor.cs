using System;
using System.Collections.Generic;
using System.Linq;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using Slyce.Common.IEnumerableExtensions;

namespace ArchAngel.Providers.EntityModel.Controller.DatabaseLayer
{
	public class TableProcessor
	{
		Table.TableComparer TableComparer = new Table.TableComparer();

		public DatabaseMergeResult ProcessTables(IEnumerable<ITable> tables1, IEnumerable<ITable> tables2)
		{
			DatabaseMergeResult result = new DatabaseMergeResult();

			// Get all of the tables with the same name in the existing and refreshed table lists
			IEnumerable<KeyValuePair<ITable, ITable>> union = GetEqualTables(tables1, tables2);

			//Find changed table components.
			foreach (var tablePair in union)
			{
				var r = ProcessTable(tablePair.Key, tablePair.Value);
				result.CopyFrom(r);
			}

			return result;
		}

		internal IEnumerable<KeyValuePair<ITable, ITable>> GetEqualTables(IEnumerable<ITable> tables1, IEnumerable<ITable> tables2)
		{
			var leftTables = tables1.Intersect(tables2, TableComparer).OrderBy(t => t.Schema).ThenBy(t => t.Name);
			var rightTables = tables2.Intersect(tables1, TableComparer).OrderBy(t => t.Schema).ThenBy(t => t.Name);
			return leftTables.Zip(rightTables);
		}

		public DatabaseMergeResult ProcessTable(ITable key, ITable value)
		{
			var columnResult = ProcessTableForTableMemberChanges(key, key.Columns, value.Columns, n => n.Name, new ColumnOperationFactory(), new Column.ColumnComparer());
			var keyResult = ProcessTableForTableMemberChanges(key, key.Keys, value.Keys, n => n.Name, new KeyOperationFactory(), new Key.KeyComparer());
			var indexResult = ProcessTableForTableMemberChanges(key, key.Indexes.Where(i => i.IndexType != ArchAngel.Providers.EntityModel.Helper.DatabaseIndexType.PrimaryKey && i.IndexType != ArchAngel.Providers.EntityModel.Helper.DatabaseIndexType.ForeignKey), value.Indexes.Where(i => i.IndexType != ArchAngel.Providers.EntityModel.Helper.DatabaseIndexType.PrimaryKey && i.IndexType != ArchAngel.Providers.EntityModel.Helper.DatabaseIndexType.ForeignKey), n => n.Name, new IndexOperationFactory(), new Index.IndexComparer());
			var relationshipResult = ProcessTableForTableMemberChanges(key, key.Relationships, value.Relationships, n => n.Name, new RelationshipOperationFactory(), new RelationshipImpl.RelationshipComparer());

			DatabaseMergeResult result = new DatabaseMergeResult();
			result.AddColumnOperations(columnResult);
			result.AddKeyOperations(keyResult);
			result.AddIndexOperations(indexResult);
			result.AddRelationshipOperations(relationshipResult);

			return result;
		}

		private IEnumerable<IMergeOperation<Q>> ProcessTableForTableMemberChanges<Q>(ITable sourceTable, IEnumerable<Q> originalMembers, IEnumerable<Q> refreshedMembers, Func<Q, object> sorter, OperationFactory<Q> factory, IEqualityComparer<Q> comparer) where Q : class, ITableMember<Q>
		{
			var results = new HashSet<IMergeOperation<Q>>();

			// Check for added members
			List<Q> newMembers = originalMembers.Compliment(refreshedMembers).ToList();
			newMembers.ForEach(
				t => results.Add(factory.AdditionOp(sourceTable, t)));

			// Check for removed members
			List<Q> removedMembers = refreshedMembers.Compliment(originalMembers).ToList();
			removedMembers.ForEach(
				t => results.Add(factory.RemovalOp(t)));

			// Check for changed members
			IEnumerable<Q> intersection1 = originalMembers.Intersect(refreshedMembers, comparer).OrderBy(sorter);
			var x = refreshedMembers.Intersect(originalMembers, comparer).OrderBy(sorter);
			var commonItems = intersection1.Zip(refreshedMembers.Intersect(originalMembers, comparer).OrderBy(sorter));
			string descriptionOfChanges;

			foreach (var memberPair in commonItems)
				if (memberPair.Key.HasChanges(memberPair.Value, out descriptionOfChanges))
					results.Add(factory.ChangeOp(memberPair.Key, memberPair.Value, descriptionOfChanges));

			return results;
		}

		internal interface OperationFactory<T> where T : class
		{
			IMergeOperation<T> AdditionOp(ITable table, T obj);
			IMergeOperation<T> RemovalOp(T obj);
			IMergeOperation<T> ChangeOp(T toChange, T toCopyFrom, string description);
		}

		internal class ColumnOperationFactory : OperationFactory<IColumn>
		{
			public IMergeOperation<IColumn> AdditionOp(ITable table, IColumn obj)
			{
				return new ColumnAdditionOperation(table, obj);
			}

			public IMergeOperation<IColumn> RemovalOp(IColumn obj)
			{
				return new ColumnRemovalOperation(obj);
			}

			public IMergeOperation<IColumn> ChangeOp(IColumn toChange, IColumn toCopyFrom, string description)
			{
				return new ColumnChangeOperation(toChange, toCopyFrom, description);
			}
		}

		internal class KeyOperationFactory : OperationFactory<IKey>
		{
			public IMergeOperation<IKey> AdditionOp(ITable table, IKey obj)
			{
				return new KeyAdditionOperation(table, obj);
			}

			public IMergeOperation<IKey> RemovalOp(IKey obj)
			{
				return new KeyRemovalOperation(obj);
			}

			public IMergeOperation<IKey> ChangeOp(IKey toChange, IKey toCopyFrom, string description)
			{
				return new KeyChangeOperation(toChange, toCopyFrom, description);
			}
		}

		internal class RelationshipOperationFactory : OperationFactory<Relationship>
		{
			public IMergeOperation<Relationship> AdditionOp(ITable table, Relationship obj)
			{
				return new RelationshipAdditionOperation(table, obj);
			}

			public IMergeOperation<Relationship> RemovalOp(Relationship obj)
			{
				return new RelationshipRemovalOperation(obj);
			}

			public IMergeOperation<Relationship> ChangeOp(Relationship toChange, Relationship toCopyFrom, string description)
			{
				return new RelationshipChangeOperation(toChange, toCopyFrom, description);
			}
		}

		internal class IndexOperationFactory : OperationFactory<IIndex>
		{
			public IMergeOperation<IIndex> AdditionOp(ITable table, IIndex obj)
			{
				return new IndexAdditionOperation(table, obj);
			}

			public IMergeOperation<IIndex> RemovalOp(IIndex obj)
			{
				return new IndexRemovalOperation(obj);
			}

			public IMergeOperation<IIndex> ChangeOp(IIndex toChange, IIndex toCopyFrom, string description)
			{
				return new IndexChangeOperation(toChange, toCopyFrom, description);
			}
		}
	}
}
