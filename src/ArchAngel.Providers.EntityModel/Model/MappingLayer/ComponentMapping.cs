using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ArchAngel.Interfaces.Attributes;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using Slyce.Common.IEnumerableExtensions;

namespace ArchAngel.Providers.EntityModel.Model.MappingLayer
{
	[ArchAngelEditor(VirtualPropertiesAllowed = true, IsGeneratorIterator = false, PreviewDisplayProperty = "Name")]
	public class ComponentMappingImpl : ModelObject, ComponentMapping
	{
		private readonly List<ComponentPropertyMarker> properties = new List<ComponentPropertyMarker>();
		private readonly List<IColumn> columns = new List<IColumn>();

        public override string DisplayName
        {
            get { return "Mapping"; }
        }

		private Component _toComponent;
		public Component ToComponent
		{
			get { return _toComponent; }
			set { _toComponent = value; RaisePropertyChanged("ToComponent");}
		}

		private ITable _fromTable;
		public ITable FromTable
		{
			get { return _fromTable; }
			set { _fromTable = value; RaisePropertyChanged("FromTable");}
		}

		public MappingSet MappingSet { get; set; }

		public ReadOnlyCollection<ComponentPropertyMarker> ToProperties
		{
			get { return properties.AsReadOnly(); }
		}

		public ReadOnlyCollection<IColumn> FromColumns
		{
			get { return columns.AsReadOnly(); }
		}

		public IEnumerable<ColumnComponentPropertyMapping> Mappings
		{
			get
			{
				return properties.Zip(columns).Select(map => new ColumnComponentPropertyMapping(map.Key, map.Value));
			}
		}

		public void AddPropertyAndColumn(ComponentPropertyMarker prop, IColumn col)
		{
			if (FromTable == null)
			{
				FromTable = col.Parent;
			}
			if (ToComponent == null)
			{
				ToComponent = prop.Component;
			}

			if (col.Parent != FromTable)
			{
				throw new ArgumentException("Cannot add columns from multiple tables to a single Mapping object");
			}
			if (prop.Component != ToComponent)
			{
				throw new ArgumentException("Cannot add properties from multiple components to a single Mapping object");
			}

			properties.Add(prop);
			columns.Add(col);

			if (MappingSet != null) MappingSet.InvalidateCache();

			RaisePropertyChanged("Mappings");
		}

		public void SetMappings(IEnumerable<ColumnComponentPropertyMapping> mappings)
		{
			properties.Clear();
			columns.Clear();

			foreach (var mapping in mappings)
				AddPropertyAndColumn(mapping.Property, mapping.Column);
		}

		public void Delete()
		{
			if (MappingSet != null) MappingSet.RemoveMapping(this);
		}

		public void SetMapping(ComponentPropertyMarker property, IColumn column)
		{
			int index = properties.IndexOf(property);

			if (index == -1 && column != null)
			{
				AddPropertyAndColumn(property, column);
				return;
			}

			if (column == null)
			{
				properties.RemoveAt(index);
				columns.RemoveAt(index);
				if (MappingSet != null) MappingSet.InvalidateCache();

				RaisePropertyChanged("Mappings");
				return;
			}

			columns[index] = column;

			if (MappingSet != null) MappingSet.InvalidateCache();

			RaisePropertyChanged("Mappings");
		}

		public void RemovePropertyAndMappedColumn(ComponentPropertyMarker property)
		{
			int index = properties.IndexOf(property);
			if (index == -1)
				return;

			properties.RemoveAt(index);
			columns.RemoveAt(index);

			if (MappingSet != null) MappingSet.InvalidateCache();

			RaisePropertyChanged("Mappings");
		}
	}

	public class ColumnComponentPropertyMapping
	{
		public ComponentPropertyMarker Property { get; private set; }
		public IColumn Column { get; private set; }

		public ColumnComponentPropertyMapping(ComponentPropertyMarker property, IColumn column)
		{
			Property = property;
			Column = column;
		}
	}
}
