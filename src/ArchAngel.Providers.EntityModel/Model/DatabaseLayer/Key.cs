using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using ArchAngel.Providers.EntityModel.Helper;

namespace ArchAngel.Providers.EntityModel.Model.DatabaseLayer
{
	[ArchAngel.Interfaces.Attributes.ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = false)]
	[DebuggerDisplay("Name = {Name}, Parent = {Parent}, Keytype = {Keytype}")]
	public class Key : ScriptBase, IKey
	{
		private List<IColumn> columns;
		private DatabaseKeyType keytype;
		private ITable parent;
		private IKey referencedKey;
		private bool isUnique;

		public Key()
		{
			columns = new List<IColumn>();
		}

		public Key(string name)
			: this()
		{
			Name = name;
		}

		public Key(string name, DatabaseKeyType keyType)
			: this(name)
		{
			this.keytype = keyType;
		}

		public override string DisplayName
		{
			get { return "Key:" + Name; }
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

		public ReadOnlyCollection<IColumn> Columns
		{
			get { return columns.AsReadOnly(); }
		}

		public IKey ReferencedKey
		{
			get { return referencedKey; }
			set
			{
				referencedKey = value;

				//if (!this.IsUnique && referencedKey.IsUnique)
				//    this.IsUnique = true;

				RaisePropertyChanged("ReferencedKey");
			}
		}

		[DatabaseDefined]
		public DatabaseKeyType Keytype
		{
			get { return keytype; }
			set
			{
				keytype = value;
				RaisePropertyChanged("Keytype");
			}
		}

		[DatabaseDefined]
		//public bool IsUnique
		//{
		//    get { return Columns.Any(p => p.IsUnique || p.InPrimaryKey); }
		//}
		public bool IsUnique
		{
			get
			{
				//if (ReferencedKey.IsUnique)
				//    return true;

				return isUnique;
			}
			set
			{
				isUnique = value;
				RaisePropertyChanged("IsUnique");
			}
		}

		public IKey AddColumn(string columnName)
		{
			// Check whether this column has already been added
			if (columns.Count(c => c.Name == columnName) > 0)
				return this;

			IColumn column = Parent.GetColumn(columnName);
			columns.Add(column);

			if (this.Keytype == DatabaseKeyType.Primary)
				column.InPrimaryKey = true;

			RaisePropertyChanged("Columns");
			return this;
		}

		public void ClearColumns()
		{
			columns.Clear();
		}

		public void RemoveColumn(IColumn column)
		{
			columns.Remove(column);

			if (this.Keytype == DatabaseKeyType.Primary)
				column.InPrimaryKey = false;

			RaisePropertyChanged("Columns");
		}

		public bool IsOneToOneRelationship()
		{
			// Foreign key must reference primary key
			if (ReferencedKey.Keytype != DatabaseKeyType.Primary)
			{
				return false;
			}
			// Check to see if this key columns are the same as the primary key columns
			ITable table = Parent;

			if (Columns.Count != table.ColumnsInPrimaryKey.Count())
			{
				return false;
			}
			foreach (Column column in Columns)
			{
				if (!column.InPrimaryKey)
				{
					return false;
				}
			}
			return true;
		}

		public bool HasChanges(IKey value, out string description)
		{
			StringBuilder sb = new StringBuilder();

			// Note: IsUnique is dependant on Column changes, so we don't check it here.
			if (base.HasChanges(value))
				sb.Append("Value,");
			if (ReferencedKey != null && value.ReferencedKey != null && ReferencedKey.Name != value.ReferencedKey.Name)
				sb.Append("ReferencedKeyName,");
			if (Keytype != value.Keytype)
				sb.Append("KeyType,");
			if (Columns.Count != value.Columns.Count)
				sb.Append("CountOfColumns,");

			for (int i = 0; i < Columns.Count; i++)
			{
				IColumn matchingColumn = value.Columns.SingleOrDefault(c => c.OrdinalPosition == Columns[i].OrdinalPosition);

				if (matchingColumn == null)
					sb.Append(string.Format("MissingColumn[{0}],", Columns[i].Name));
				else if (Columns[i].Name != matchingColumn.Name)
					sb.Append(string.Format("RenamedColumn[{0} -> {1}],", Columns[i].Name, matchingColumn.Name));
			}
			for (int i = 0; i < value.Columns.Count; i++)
			{
				IColumn matchingColumn = Columns.SingleOrDefault(c => c.OrdinalPosition == value.Columns[i].OrdinalPosition);

				if (matchingColumn == null)
					sb.Append(string.Format("NewColumn[{0}],", value.Columns[i].Name));
			}
			if (sb.Length > 0)
			{
				description = sb.ToString().TrimEnd(',');
				return true;
			}
			description = "";
			return false;
		}

		public IKey Clone()
		{
			Key key = new Key();

			CopyInto(key);

			return key;
		}

		public void CopyInto(IKey key)
		{
			base.CopyInto(key);

			key.Keytype = Keytype;
			key.IsUnique = this.IsUnique;
			key.Description = this.Description;
			key.Enabled = this.Enabled;
			key.IsUserDefined = this.IsUserDefined;
			//key.Parent
			//key.ReferencedKey = this.ReferencedKey.Clone();
			key.Schema = this.Schema;
		}

		public void DeleteSelf()
		{
			if (Parent != null)
				parent.RemoveKey(this);

			columns.Clear();

			if (referencedKey != null)
				referencedKey.ReferencedKey = null;

			referencedKey = null;
		}

		public string Serialise(IDatabaseSerialisationScheme scheme)
		{
			return scheme.SerialiseKey(this);
		}

		/// <summary>
		/// Custom comparer for Key class: compares on Name.
		/// </summary>
		public class KeyComparer : IEqualityComparer<IKey>
		{
			// Tables are equal if their names are equal.
			public bool Equals(IKey x, IKey y)
			{
				// Check whether the compared objects reference the same data.
				if (Object.ReferenceEquals(x, y)) return true;

				// Check whether any of the compared objects is null.
				if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
					return false;

				//Check whether the keys' properties are equal.
				return x.Name == y.Name;
			}

			/// <summary>
			/// If Equals() returns true for a pair of objects 
			/// then GetHashCode() must return the same value for these objects.
			/// </summary>
			/// <param name="column"></param>
			/// <returns></returns>
			public int GetHashCode(IKey key)
			{
				//Check whether the object is null
				if (Object.ReferenceEquals(key, null)) return 0;

				//Get hash code for the Name field if it is not null.
				int hashKeyName = key.Name == null ? 0 : key.Name.GetHashCode();

				return hashKeyName;
			}
		}
	}
}