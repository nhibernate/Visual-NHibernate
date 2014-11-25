using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using ArchAngel.Providers.EntityModel.Helper;

namespace ArchAngel.Providers.EntityModel.Model.DatabaseLayer
{
	[DebuggerDisplay("Name = {Name}, Table = {Parent}, IndexType = {IndexType}")]
	[ArchAngel.Interfaces.Attributes.ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = true, PreviewDisplayProperty = "Name")]
	public class Index : ScriptBase, IIndex
	{
		private List<IColumn> columns;
		private DatabaseIndexType datatype;
		private bool isClustered;
		private bool isUnique;
		private ITable parent;

		public Index()
		{
			Columns = new List<IColumn>();
		}

		public Index(string name)
			: this()
		{
			Name = name;
		}

		public override string DisplayName
		{
			get { return "Index:" + Name; }
		}

		public ITable Parent
		{
			get { return parent; }
			set
			{
				parent = value;
				RaisePropertyChanged("Parent");
			}
		}

		public List<IColumn> Columns
		{
			get { return columns; }
			private set
			{
				columns = value;
				RaisePropertyChanged("Columns");
			}
		}

		[DatabaseDefined]
		public bool IsClustered
		{
			get { return isClustered; }
			set
			{
				isClustered = value;
				RaisePropertyChanged("IsClustered");
			}
		}

		[DatabaseDefined]
		public bool IsUnique
		{
			get { return isUnique; }
			set
			{
				isUnique = value;
				RaisePropertyChanged("IsUnique");
			}
		}

		[DatabaseDefined]
		public DatabaseIndexType IndexType
		{
			get { return datatype; }
			set
			{
				datatype = value;
				RaisePropertyChanged("Datatype");
			}
		}

		public IColumn AddColumn(string columnName)
		{
			IColumn column = Parent.GetColumn(columnName);

			if (column != null)
			{
				Columns.Add(column);
				RaisePropertyChanged("Columns");
			}
			return column;
		}

		public void RemoveColumn(IColumn column)
		{
			Columns.Remove(column);
			RaisePropertyChanged("Columns");
		}

		public bool HasChanges(IIndex value, out string description)
		{
			StringBuilder sb = new StringBuilder();

			if (base.HasChanges(value))
				sb.Append("Value,");
			if (IsClustered != value.IsClustered)
				sb.Append("IsClustered,");
			if (IsUnique != value.IsUnique)
				sb.Append("IsUnique,");
			if (IndexType != value.IndexType)
				sb.Append("Datatype,");
			if (Columns.Count == value.Columns.Count)
			{
				for (int i = 0; i < Columns.Count; i++)
					if (Columns[i].Name != value.Columns[i].Name)
						sb.Append(string.Format("Column[{0}],", value.Columns[i].Name));
			}
			else
				sb.Append("CountOfColumns,");

			if (sb.Length > 0)
			{
				description = sb.ToString().TrimEnd(',');
				return true;
			}
			description = "";
			return false;
		}

		public IIndex Clone()
		{
			Index i = new Index();
			CopyInto(i);
			return i;
		}

		public void DeleteSelf()
		{
			if (parent != null)
				parent.RemoveIndex(this);
			parent = null;
			columns.Clear();
		}

		public void CopyInto(IIndex i)
		{
			base.CopyInto(i);
			i.IndexType = IndexType;
			i.IsClustered = IsClustered;
			i.IsUnique = IsUnique;
		}

		public string Serialise(DatabaseSerialisationScheme scheme)
		{
			return scheme.SerialiseIndex(this);
		}

		public override bool ValidateObject(List<ValidationFailure> failures)
		{
			base.ValidateObject(failures);

			if (ParentContainsAnotherObjectNamedTheSameAsThis())
			{
				failures.Add(new ValidationFailure("Duplicated name: " + Name, "Name"));
			}

			return failures.Count == 0;
		}

		private bool ParentContainsAnotherObjectNamedTheSameAsThis()
		{
			if (Parent == null)
				return false;

			return Parent.Indexes.Any(so => ReferenceEquals(this, so) == false && so.Name == Name);
		}

		/// <summary>
		/// Custom comparer for Index class: compares on Name.
		/// </summary>
		public class IndexComparer : IEqualityComparer<IIndex>
		{
			// Tables are equal if their names are equal.
			public bool Equals(IIndex x, IIndex y)
			{
				// Check whether the compared objects reference the same data.
				if (Object.ReferenceEquals(x, y)) return true;

				// Check whether any of the compared objects is null.
				if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
					return false;

				//Check whether the indexes' properties are equal.
				return x.Name == y.Name;
			}

			/// <summary>
			/// If Equals() returns true for a pair of objects 
			/// then GetHashCode() must return the same value for these objects.
			/// </summary>
			/// <param name="column"></param>
			/// <returns></returns>
			public int GetHashCode(IIndex index)
			{
				//Check whether the object is null
				if (Object.ReferenceEquals(index, null)) return 0;

				//Get hash code for the Name field if it is not null.
				int hashIndexName = index.Name == null ? 0 : index.Name.GetHashCode();

				return hashIndexName;
			}
		}
	}
}