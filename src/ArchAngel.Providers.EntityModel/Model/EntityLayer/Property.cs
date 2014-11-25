using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;

namespace ArchAngel.Providers.EntityModel.Model.EntityLayer
{
	[ArchAngel.Interfaces.Attributes.ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = false)]
	[DebuggerDisplay("Property(Name={Name})")]
	public class PropertyImpl : ModelObject, Property
	{
		public enum NHibernateTypes
		{
			AnsiChar,
			AnsiString,
			Binary,
			BinaryBlob,
			Boolean,
			Byte,
			Char,
			CultureInfo,
			DateTime,
			LocalDateTime,
			UtcDateTime,
			Decimal,
			Double,
			Guid,
			IGeography,
			IGeometry,
			Int16,
			Int32,
			Int64,
			PersistentEnum,
			Serializable,
			Single,
			String,
			StringClob,
			Ticks,
			TimeSpan,
			TimeStamp,
			TrueFalse,
			Type,
			YesNo
		}
		private string name;
		private IList<string> _OldNames = new List<string>();
		private string type;
		private string nhibernateType;
		private bool readOnly;
		private ValidationOptions options;
		//private bool isVirtual;
		private bool isKeyProperty;

		public PropertyImpl()
		{
			ValidationOptions = new ValidationOptions();
		}

		public PropertyImpl(string name)
			: this()
		{
			this.name = name;

			if (!string.IsNullOrEmpty(name))
				OldNames.Add(name);
		}

		/// <summary>
		/// Copy constructor.
		/// </summary>
		/// <param name="p"></param>
		public PropertyImpl(Property p)
		{
			name = p.Name;

			if (!string.IsNullOrEmpty(name))
				OldNames.Add(name);

			type = p.Type;
			nhibernateType = p.NHibernateType;
			readOnly = p.ReadOnly;
			options = new ValidationOptions(p.ValidationOptions);
			//isVirtual = p.IsVirtual;
			isKeyProperty = p.IsKeyProperty;
		}

		public override string DisplayName
		{
			get { return "Property: " + Name; }
		}

		public bool HasParentEntity
		{
			get
			{
				return Entity != null;
			}
		}

		public bool IsOverridden
		{
			get
			{
				if (HasParentEntity == false) return false;
				if (Entity.HasChildren == false) return false;

				return Entity.AnyChildHasPropertyNamed(name);
			}
		}

		public bool IsInherited
		{
			get
			{
				if (HasParentEntity == false) return false;
				if (Entity.HasParent == false) return false;

				return Entity.InheritedProperties.Any(p => p.Name == Name);
			}
		}

		/// <summary>
		/// True if this Property is part of an abstract parent (Table Per Concrete Class inheritance),
		/// but we still need to access the mapped column.
		/// </summary>
		public bool IsHiddenByAbstractParent { get; set; }

		/// <summary>
		/// True if this Property is part of a concrete parent (Table Per SubClass inheritance),
		/// but we still need to access the mapped column.
		/// </summary>
		public bool IsPartOfHiddenKey { get; set; }

		private Entity _Entity = null;

		public Entity Entity
		{
			get { return _Entity; }
			set { _Entity = value; }
		}

		public string Name
		{
			get { return name; }
			set
			{
				if (OldNames.Count == 0 && !string.IsNullOrEmpty(value))
					OldNames.Add(value);

				if (name != value)
				{
					name = value;
					RaisePropertyChanged("Name");
				}
			}
		}

		public IList<string> OldNames
		{
			get { return _OldNames; }
		}

		public ValidationOptions ValidationOptions
		{
			get { return options; }
			set { options = value; RaisePropertyChanged("ValidationOptions"); }
		}

		public string Type
		{
			get { return type; }
			set { type = value; RaisePropertyChanged("Type"); }
		}

		public string NHibernateType
		{
			get
			{
				if (string.IsNullOrWhiteSpace(nhibernateType))
				{
					string t = type.ToLowerInvariant().TrimEnd('?');

					if (t == "string") nhibernateType = "String";
					else if (t == "int16" || t == "short") nhibernateType = "Int16";
					else if (t == "int32" || t == "int") nhibernateType = "Int32";
					else if (t == "int64" || t == "long") nhibernateType = "Int64";
					else if (t == "bool" || t == "boolean") nhibernateType = "Boolean";
					else if (t == "char") nhibernateType = "Char";
					else if (t == "byte") nhibernateType = "Byte";
					else if (t == "datetime") nhibernateType = "DateTime";
					else if (t == "decimal") nhibernateType = "Decimal";
					else if (t == "double") nhibernateType = "Double";
					else if (t == "guid") nhibernateType = "Guid";
					else if (t == "enum") nhibernateType = "PersistentEnum";
					else if (t == "single") nhibernateType = "Single";
					else if (t == "timespan") nhibernateType = "TimeSpan";
					else if (t.EndsWith("cultureinfo")) nhibernateType = "CultureInfo";
					else if (t.EndsWith("byte[]")) nhibernateType = "BinaryBlob";
					else if (t == "type") nhibernateType = "Type";
					else if (t == "igeometry") nhibernateType = "IGeometry";
					else if (t == "igeography") nhibernateType = "IGeography";
				}
				return nhibernateType;
			}
			set { nhibernateType = value; RaisePropertyChanged("NHibernateType"); }
		}

		public bool ReadOnly
		{
			get { return readOnly; }
			set { readOnly = value; RaisePropertyChanged("ReadOnly"); }
		}

		//public bool IsVirtual
		//{
		//    get { return isVirtual; }
		//    set { isVirtual = value; RaisePropertyChanged("IsVirtual"); }
		//}

		public void SetIsKeyPropertyDirectly(bool newValue)
		{
			if (isKeyProperty == newValue) return;
			isKeyProperty = newValue;
			RaisePropertyChanged("IsKeyProperty");
		}

		public bool IsKeyProperty
		{
			get
			{
				var mappedColumn = this.MappedColumn();

				if (mappedColumn != null)
					return mappedColumn.InPrimaryKey;

				return isKeyProperty;
			}
			set
			{
				if (isKeyProperty == value) return;

				isKeyProperty = value;
				if (isKeyProperty && Entity != null)
				{
					Entity.Key.AddProperty(this);
				}
				else if (!isKeyProperty && Entity != null)
				{
					Entity.Key.RemoveProperty(this);
				}
				#region Update mapped column, table, key
				ArchAngel.Providers.EntityModel.Model.DatabaseLayer.IColumn mappedCol = this.MappedColumn();

				if (mappedCol != null)
				{
					if (isKeyProperty && !mappedCol.InPrimaryKey)
					{
						if (mappedCol.Parent.FirstPrimaryKey != null)
						{
							if (!mappedCol.Parent.FirstPrimaryKey.Columns.Contains(mappedCol))
								mappedCol.Parent.FirstPrimaryKey.AddColumn(mappedCol.Name);
						}
						else
						{
							Key primaryKey = new Key("Primary", Helper.DatabaseKeyType.Primary);
							mappedCol.Parent.AddKey(primaryKey);
							primaryKey.AddColumn(mappedCol.Name);
						}
					}
					else if (!isKeyProperty && mappedCol.InPrimaryKey)
					{
						if (mappedCol.Parent.FirstPrimaryKey != null)
						{
							if (mappedCol.Parent.FirstPrimaryKey.Columns.Contains(mappedCol))
								mappedCol.Parent.FirstPrimaryKey.RemoveColumn(mappedCol);
						}
					}
				}
				#endregion

				RaisePropertyChanged("IsKeyProperty");
			}
		}

		public void DeleteSelf()
		{
			if (Entity != null)
			{
				Entity.RemoveProperty(this);
				Entity = null;
			}
		}

		public override string ToString()
		{
			return string.Format("PropertyImpl(Name: {0})", name);
		}

		public bool Equals(PropertyImpl obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj.name, name) && Equals(obj.Entity, Entity);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(PropertyImpl)) return false;
			return Equals((PropertyImpl)obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((name != null ? name.GetHashCode() : 0) * 397) ^ (Entity != null ? Entity.GetHashCode() : 0);
			}
		}
	}
}