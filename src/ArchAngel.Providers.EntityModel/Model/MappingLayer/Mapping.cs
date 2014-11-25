using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArchAngel.Interfaces.SchemaDiagrammer;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using Slyce.Common.IEnumerableExtensions;

namespace ArchAngel.Providers.EntityModel.Model.MappingLayer
{
	[Interfaces.Attributes.ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = true, PreviewDisplayProperty = "Name")]
	public class MappingImpl : ModelObject, Mapping
	{
		private readonly List<Property> properties = new List<Property>();
		private readonly List<IColumn> columns = new List<IColumn>();
		private IEntityObject fromTable;
		private Entity toEntity;
		private MappingSet mappingSet;

		[Interfaces.Attributes.ArchAngelEditor(IsGeneratorIterator = true)]
		public MappingSet MappingSet
		{
			get { return mappingSet; }
			set
			{
				mappingSet = value;
				RaisePropertyChanged("MappingSet");
			}
		}

		[Interfaces.Attributes.ArchAngelEditor(IsGeneratorIterator = true)]
		public Entity ToEntity
		{
			get { return toEntity; }
			set
			{
				toEntity = value;
				RaisePropertyChanged("ToEntity");
				RaisePropertyChanged("Name");
			}
		}

		[Interfaces.Attributes.ArchAngelEditor(IsGeneratorIterator = true)]
		public IEntityObject FromTable
		{
			get { return fromTable; }
			set
			{
				fromTable = value;
				RaisePropertyChanged("FromTable");
				RaisePropertyChanged("Name");
			}
		}

		public string Name
		{
			get { return FromTable.Name + " to " + ToEntity.Name; }
			set { }
		}

		IEntity IRelationship.SourceEntity
		{
			get { return FromTable; }
			set { FromTable = (IEntityObject) value; }
		}

		IEntity IRelationship.TargetEntity
		{
			get { return ToEntity; }
			set { ToEntity = (Entity) value; }
		}

		public Cardinality SourceCardinality
		{
			get { return Cardinality.One; } set{ }
		}

		public Cardinality TargetCardinality
		{
			get { return Cardinality.One; }
			set { }
		}

		public ReadOnlyCollection<Property> ToProperties
		{
			get { return properties.AsReadOnly(); }
		}

		public ReadOnlyCollection<IColumn> FromColumns 
		{ 
			get { return columns.AsReadOnly(); } 
		}

		public IEnumerable<ColumnPropertyMapping> Mappings
		{
			get
			{
				return properties.Zip(columns).Map(map => new ColumnPropertyMapping(map.Key, map.Value));
			}
		}

		public void AddPropertyAndColumn(Property prop, IColumn col)
		{
			if (prop == null) throw new ArgumentNullException("prop");
			if (col == null) throw new ArgumentNullException("col");

			if(FromTable == null)
			{
				FromTable = col.Parent;
			}
			if(ToEntity == null)
			{
				ToEntity = prop.Entity;
			}

			if(col.Parent != FromTable)
			{
				throw new ArgumentException("Cannot add columns from multiple tables to a single Mapping object");
			}
			if(prop.Entity != ToEntity)
			{
				throw new ArgumentException("Cannot add properties from multiple entities to a single Mapping object");
			}

			properties.Add(prop);
			columns.Add(col);

			if (mappingSet != null) mappingSet.InvalidateCache();

			RaisePropertyChanged("Mappings");
		}

		/// <summary>
		/// Clears the properties and columns from this mapping, and adds the
		/// ones from the given mappings collection.
		/// </summary>
		/// <param name="mappings">The new mappings to use.</param>
		public void SetMappings(IEnumerable<ColumnPropertyMapping> mappings)
		{
			properties.Clear();
			columns.Clear();

			foreach (var mapping in mappings)
				AddPropertyAndColumn(mapping.Property, mapping.Column);
		}

		public void Delete()
		{
			if(MappingSet != null) MappingSet.RemoveMapping(this);
		}

		public void SetMapping(Property property, IColumn column)
		{
			int index = properties.IndexOf(property);

			if (index == -1 && column != null)
			{
				AddPropertyAndColumn(property, column);
				return;
			}

			if(column == null)
			{
				properties.RemoveAt(index);
				columns.RemoveAt(index);
				if (mappingSet != null) mappingSet.InvalidateCache();

				RaisePropertyChanged("Mappings");
				return;
			}
			
			columns[index] = column;

			if(mappingSet != null) mappingSet.InvalidateCache();

			RaisePropertyChanged("Mappings");
		}

		public void RemovePropertyAndMappedColumn(Property property)
		{
			int index = properties.IndexOf(property);
			if(index == -1)
				return;

			properties.RemoveAt(index);
			columns.RemoveAt(index);
			
			if (mappingSet != null) mappingSet.InvalidateCache();

			RaisePropertyChanged("Mappings");
		}
	}
}