using System.Collections.Generic;
using System.Linq;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;

namespace ArchAngel.Providers.EntityModel.Controller.DatabaseLayer
{
	public class DatabaseMergeResult
	{
		private readonly List<IMergeOperation<ITable>> tableOperations;
		private readonly List<IMergeOperation<ITable>> viewOperations;

		public IEnumerable<IMergeOperation<ITable>> TableOperations
		{
			get { return tableOperations; }
		}

		public IEnumerable<IMergeOperation<ITable>> ViewOperations
		{
			get { return viewOperations; }
		}

		private readonly List<IMergeOperation<IColumn>> columnOperations;
		public IEnumerable<IMergeOperation<IColumn>> ColumnOperations
		{
			get { return columnOperations; }
		}

		private readonly List<IMergeOperation<IKey>> keyOperations;
		public IEnumerable<IMergeOperation<IKey>> KeyOperations
		{
			get { return keyOperations; }
		}

		private readonly HashSet<IMergeOperation<Relationship>> relationshipOperations;
		public IEnumerable<IMergeOperation<Relationship>> RelationshipOperations
		{
			get { return relationshipOperations; }
		}

		public bool AnyChanges
		{
			get
			{
				return columnOperations.Count != 0 ||
					tableOperations.Count != 0 ||
					indexOperations.Count != 0 ||
					keyOperations.Count != 0 ||
					relationshipOperations.Count != 0 ||
					viewOperations.Count != 0;
			}
		}

		private readonly List<IMergeOperation<IIndex>> indexOperations;
		public IEnumerable<IMergeOperation<IIndex>> IndexOperations
		{
			get { return indexOperations; }
		}

		public IEnumerable<ITwoStepMergeOperation> TwoStepOperations
		{
			get
			{
				var list = new List<ITwoStepMergeOperation>();

				list.AddRange(GetTwoStepOperations(tableOperations));
				list.AddRange(GetTwoStepOperations(columnOperations));
				list.AddRange(GetTwoStepOperations(keyOperations));
				list.AddRange(GetTwoStepOperations(indexOperations));
				list.AddRange(GetTwoStepOperations(relationshipOperations));
				list.AddRange(GetTwoStepOperations(viewOperations));

				return list;
			}
		}

		private static IEnumerable<ITwoStepMergeOperation> GetTwoStepOperations<T>(IEnumerable<T> list)
		{
			var ops = list.Where(t => t is ITwoStepMergeOperation);
			return ops.Cast<ITwoStepMergeOperation>();
		}

		public DatabaseMergeResult()
		{
			tableOperations = new List<IMergeOperation<ITable>>();
			columnOperations = new List<IMergeOperation<IColumn>>();
			keyOperations = new List<IMergeOperation<IKey>>();
			indexOperations = new List<IMergeOperation<IIndex>>();
			relationshipOperations = new HashSet<IMergeOperation<Relationship>>();
			viewOperations = new List<IMergeOperation<ITable>>();
		}

		public void AddTableOperation(IMergeOperation<ITable> op)
		{
			tableOperations.Add(op);
		}

		public void AddViewOperation(IMergeOperation<ITable> op)
		{
			viewOperations.Add(op);
		}

		public void AddColumnOperation(IMergeOperation<IColumn> op)
		{
			columnOperations.Add(op);
		}

		public void AddColumnOperations(IEnumerable<IMergeOperation<IColumn>> result)
		{
			foreach (var item in result)
				columnOperations.Add(item);
		}

		public void AddKeyOperation(IMergeOperation<IKey> operation)
		{
			keyOperations.Add(operation);
		}

		public void AddKeyOperations(IEnumerable<IMergeOperation<IKey>> result)
		{
			foreach (var item in result)
				keyOperations.Add(item);
		}

		public void AddRelationshipOperation(IMergeOperation<Relationship> operation)
		{
			relationshipOperations.Add(operation);
		}

		public void AddRelationshipOperations(IEnumerable<IMergeOperation<Relationship>> result)
		{
			foreach (var item in result)
				relationshipOperations.Add(item);
		}

		public void AddIndexOperation(IMergeOperation<IIndex> operation)
		{
			indexOperations.Add(operation);
		}

		public void AddIndexOperations(IEnumerable<IMergeOperation<IIndex>> result)
		{
			foreach (var item in result)
				indexOperations.Add(item);
		}

		public void CopyFrom(DatabaseMergeResult r)
		{
			foreach (var item in r.ColumnOperations)
				columnOperations.Add(item);
			foreach (var item in r.KeyOperations)
				keyOperations.Add(item);
			foreach (var item in r.IndexOperations)
				indexOperations.Add(item);
			foreach (var item in r.TableOperations)
				tableOperations.Add(item);
			foreach (var item in r.ViewOperations)
				viewOperations.Add(item);
			foreach (var item in r.RelationshipOperations)
				if (!relationshipOperations.Any(o => o.Object == item.Object))
					relationshipOperations.Add(item);
		}
	}
}