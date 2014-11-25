using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ArchAngel.Providers.EntityModel.Model.DatabaseLayer
{
	[ArchAngel.Interfaces.Attributes.ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = false)]
	[DebuggerDisplay("Name = {Name}")]
	public class Column : ScriptBase, IColumn
	{
		private string _default;
		//private ArchAngel.Providers.EntityModel.Helper.SQLServer.UniDbTypes datatype;
		//private ArchAngel.Interfaces.ProjectOptions.TypeMappings.UniType uniDatatype;
		private string _originalDataType;
		private bool inPrimaryKey;
		private bool isCalculated;
		private bool isComputed;
		private bool isIdentity;
		private bool isNullable;
		private bool isReadOnly;
		private bool isUnique;
		private int ordinalPosition;
		private ITable parent;
		private int precision;
		private int scale;
		private long size;
		private bool isPseudoBoolean;
		private string pseudoTrue;
		private string pseudoFalse;

		public Column()
		{
		}

		public Column(string name)
			: this()
		{
			Name = name;
			IsUserDefined = false;
		}

		public override string DisplayName
		{
			get { return "Column:" + Name; }
		}

		#region IColumn Members

		public bool IsComputed
		{
			get { return isComputed; }
			set
			{
				isComputed = value;
				RaisePropertyChanged("IsComputed");
			}
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

		//[DatabaseDefined]
		//public ArchAngel.Providers.EntityModel.Helper.SQLServer.UniDbTypes Datatype
		//{
		//    get { return datatype; }
		//    set
		//    {
		//        datatype = value;
		//        RaisePropertyChanged("Datatype");
		//    }
		//}

		//[DatabaseDefined]
		//public ArchAngel.Interfaces.ProjectOptions.TypeMappings.UniType UniDatatype
		//{
		//    get { return uniDatatype; }
		//    set
		//    {
		//        uniDatatype = value;
		//        RaisePropertyChanged("UniDatatype");
		//    }
		//}

		[DatabaseDefined]
		public string OriginalDataType
		{
			get { return _originalDataType; }
			set
			{
				_originalDataType = value;
				RaisePropertyChanged("OriginalDataType");
			}
		}

		[DatabaseDefined]
		public string Default
		{
			get { return _default; }
			set
			{
				_default = value;
				RaisePropertyChanged("Default");
			}
		}

		[DatabaseDefined]
		public bool InPrimaryKey
		{
			get { return inPrimaryKey; }
			set
			{
				inPrimaryKey = value;
				RaisePropertyChanged("InPrimaryKey");
			}
		}

		[DatabaseDefined]
		public bool IsCalculated
		{
			get { return isCalculated; }
			set
			{
				isCalculated = value;
				RaisePropertyChanged("IsCalculated");
			}
		}

		[DatabaseDefined]
		public bool IsIdentity
		{
			get { return isIdentity; }
			set
			{
				isIdentity = value;
				RaisePropertyChanged("IsIdentity");
			}
		}

		[DatabaseDefined]
		public bool IsNullable
		{
			get { return isNullable; }
			set
			{
				isNullable = value;
				RaisePropertyChanged("IsNullable");
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
		public int OrdinalPosition
		{
			get { return ordinalPosition; }
			set
			{
				ordinalPosition = value;
				RaisePropertyChanged("OrdinalPosition");
			}
		}

		[DatabaseDefined]
		public int Precision
		{
			get { return precision; }
			set
			{
				precision = value;
				RaisePropertyChanged("Precision");
			}
		}

		[DatabaseDefined]
		public bool IsReadOnly
		{
			get { return isReadOnly; }
			set
			{
				isReadOnly = value;
				RaisePropertyChanged("IsReadOnly");
			}
		}

		[DatabaseDefined]
		public int Scale
		{
			get { return scale; }
			set
			{
				scale = value;
				RaisePropertyChanged("Scale");
			}
		}

		[DatabaseDefined]
		public long Size
		{
			get { return size; }
			set
			{
				size = value;
				RaisePropertyChanged("Size");
			}
		}

		public bool SizeIsMax
		{
			get { return Size == -1; }
			set { Size = value ? -1 : 0; }
		}

		[DatabaseDefined]
		public bool IsPseudoBoolean
		{
			get { return isPseudoBoolean; }
			set
			{
				isPseudoBoolean = value;
				RaisePropertyChanged("IsPseudoBoolean");
			}
		}

		[DatabaseDefined]
		public string PseudoTrue
		{
			get { return pseudoTrue; }
			set
			{
				pseudoTrue = value;
				RaisePropertyChanged("PseudoTrue");
			}
		}

		[DatabaseDefined]
		public string PseudoFalse
		{
			get { return pseudoFalse; }
			set
			{
				pseudoFalse = value;
				RaisePropertyChanged("PseudoFalse");
			}
		}

		#endregion

		public bool HasChanges(IColumn value, out string description)
		{
			StringBuilder sb = new StringBuilder();

			if (base.HasChanges(value))
				sb.Append("Value,");
			if (Size != value.Size)
				sb.Append("Size,");
			if (SizeIsMax != value.SizeIsMax)
				sb.Append("SizeIsMax,");
			if (Scale != value.Scale)
				sb.Append("Scale,");
			if (OriginalDataType != value.OriginalDataType)
				sb.Append("Datatype,");
			if (OrdinalPosition != value.OrdinalPosition)
				sb.Append("OrdinalPosition,");
			if (InPrimaryKey != value.InPrimaryKey)
				sb.Append("InPrimaryKey,");
			if (IsComputed != value.IsComputed)
				sb.Append("IsComputed,");
			if (IsCalculated != value.IsCalculated)
				sb.Append("IsCalculated,");
			if (IsIdentity != value.IsIdentity)
				sb.Append("IsIdentity,");
			if (IsNullable != value.IsNullable)
				sb.Append("IsNullable,");
			if (IsReadOnly != value.IsReadOnly)
				sb.Append("IsReadOnly,");
			if (Precision != value.Precision)
				sb.Append("Precision,");

			if (sb.Length > 0)
			{
				description = sb.ToString().TrimEnd(',');
				return true;
			}
			description = "";
			return false;
		}

		public IColumn Clone()
		{
			var col = new Column();
			CopyInto(col);
			return col;
		}

		public void CopyInto(IColumn column)
		{
			base.CopyInto(column);

			column.OriginalDataType = OriginalDataType;
			column.Default = Default;
			column.InPrimaryKey = InPrimaryKey;
			column.IsCalculated = IsCalculated;
			column.IsComputed = IsComputed;
			column.IsIdentity = IsIdentity;
			column.IsNullable = IsNullable;
			column.IsReadOnly = IsReadOnly;
			column.IsUnique = IsUnique;
			column.OrdinalPosition = OrdinalPosition;
			column.Precision = Precision;
			column.Scale = Scale;
			column.Size = Size;
		}

		public void DeleteSelf()
		{
			if (Parent != null)
				Parent.DeleteColumn(this);

			Parent = null;
			Database = null;
		}

		public string Serialise(DatabaseSerialisationScheme scheme)
		{
			return scheme.SerialiseColumn(this);
		}

		public bool Equals(Column obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return base.Equals(obj) && Equals(obj.parent, parent);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as Column);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				{
					return (base.GetHashCode() * 397) ^ (parent != null ? parent.GetHashCode() : 0);
				}
			}
		}

		/// <summary>
		/// Custom comparer for Column class: compares on Name.
		/// </summary>
		public class ColumnComparer : IEqualityComparer<IColumn>
		{
			// Tables are equal if their names are equal.
			public bool Equals(IColumn x, IColumn y)
			{
				// Check whether the compared objects reference the same data.
				if (Object.ReferenceEquals(x, y)) return true;

				// Check whether any of the compared objects is null.
				if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
					return false;

				//Check whether the columns' properties are equal.
				return x.Name == y.Name;
			}

			/// <summary>
			/// If Equals() returns true for a pair of objects 
			/// then GetHashCode() must return the same value for these objects.
			/// </summary>
			/// <param name="column"></param>
			/// <returns></returns>
			public int GetHashCode(IColumn column)
			{
				//Check whether the object is null
				if (Object.ReferenceEquals(column, null)) return 0;

				//Get hash code for the Name field if it is not null.
				int hashColumnName = column.Name == null ? 0 : column.Name.GetHashCode();

				return hashColumnName;
			}
		}
	}
}