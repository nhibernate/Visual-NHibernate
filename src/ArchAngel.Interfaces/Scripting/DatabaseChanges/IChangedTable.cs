using System.Collections.Generic;

namespace ArchAngel.Interfaces.Scripting.DatabaseChanges
{
	public class IChangedTable : NHibernate.Model.ITable
	{
		public IChangedTable(string databaseName)
			: base(databaseName)
		{
			NewColumns = new List<NHibernate.Model.IColumn>();
			NewIndexes = new List<NHibernate.Model.IIndex>();
			NewPrimaryKeys = new List<NHibernate.Model.IKey>();
			NewForeignKeys = new List<NHibernate.Model.IKey>();

			ChangedColumns = new List<IChangedColumn>();
			ChangedIndexes = new List<NHibernate.Model.IIndex>();
			ChangedPrimaryKeys = new List<NHibernate.Model.IKey>();
			ChangedForeignKeys = new List<NHibernate.Model.IKey>();

			RemovedColumns = new List<NHibernate.Model.IColumn>();
			RemovedIndexes = new List<NHibernate.Model.IIndex>();
			RemovedPrimaryKeys = new List<NHibernate.Model.IKey>();
			RemovedForeignKeys = new List<NHibernate.Model.IKey>();
		}

		public List<NHibernate.Model.IColumn> NewColumns { get; set; }
		public List<NHibernate.Model.IIndex> NewIndexes { get; set; }
		public List<NHibernate.Model.IKey> NewPrimaryKeys { get; set; }
		public List<NHibernate.Model.IKey> NewForeignKeys { get; set; }

		public List<IChangedColumn> ChangedColumns { get; set; }
		public List<NHibernate.Model.IIndex> ChangedIndexes { get; set; }
		public List<NHibernate.Model.IKey> ChangedPrimaryKeys { get; set; }
		public List<NHibernate.Model.IKey> ChangedForeignKeys { get; set; }

		public List<NHibernate.Model.IColumn> RemovedColumns { get; set; }
		public List<NHibernate.Model.IIndex> RemovedIndexes { get; set; }
		public List<NHibernate.Model.IKey> RemovedPrimaryKeys { get; set; }
		public List<NHibernate.Model.IKey> RemovedForeignKeys { get; set; }

		public int CountOfChanges
		{
			get
			{
				return NewColumns.Count + NewIndexes.Count + NewPrimaryKeys.Count + NewForeignKeys.Count +
					ChangedColumns.Count + ChangedIndexes.Count + ChangedPrimaryKeys.Count + ChangedForeignKeys.Count +
					RemovedColumns.Count + RemovedIndexes.Count + RemovedPrimaryKeys.Count + RemovedForeignKeys.Count;
			}
		}
	}
}
