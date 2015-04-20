using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using ArchAngel.Interfaces.ITemplate;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using Slyce.Common;
using Slyce.Common.EventExtensions;

namespace ArchAngel.Providers.EntityModel.UI.PropertyGrids
{
	public partial class FormComponent : UserControl, IComponentForm, IEventSender
	{
		public event EventHandler<GenericEventArgs<ComponentPropertyMarker>> PropertyMappingChanged;
		public event EventHandler ComponentNameChanged;
		public event EventHandler DeleteComponent;

		public bool EventRaisingDisabled { get; set; }

		private readonly List<IColumn> possibleColumns = new List<IColumn>();
		private readonly List<Entity>  possibleParents = new List<Entity>();
		private readonly List<ColumnComponentPropertyMapping> mappings = new List<ColumnComponentPropertyMapping>();
		private readonly List<IColumn> currentColumnList = new List<IColumn>();
		private readonly List<ComponentPropertyMarker> currentProperties = new List<ComponentPropertyMarker>();
		private bool IgnoreMappedColumnChanges;

		public FormComponent()
		{
			InitializeComponent();
		}

		public string ComponentName
		{
			get { return tbName.Text; }
			set
			{
				EventRaisingDisabled = true;
				tbName.Text = value;
				EventRaisingDisabled = false;
			}
		}

		public void Clear()
		{
			EventRaisingDisabled = true;
			possibleColumns.Clear();
			possibleParents.Clear();
			mappings.Clear();
			currentProperties.Clear();
			currentColumnList.Clear();
			EventRaisingDisabled = false;
		}

		public void StartBulkUpdate()
		{
			Utility.SuspendPainting(this);
		}

		public void EndBulkUpdate()
		{
			Utility.ResumePainting(this);
		}

		public void SetVirtualProperties(IEnumerable<IUserOption> virtualProperties)
		{
			virtualPropertyGrid1.SetVirtualProperties(virtualProperties);
		}

		public void RefreshVirtualProperties()
		{
			virtualPropertyGrid1.RefreshVisibilities();
		}

		public void SetParentEntity(string name)
		{
			labelParentEntity.Text = name;
		}

		public void SetProperties(IEnumerable<ComponentPropertyMarker> properties)
		{
			EventRaisingDisabled = true;
			currentProperties.Clear();
			currentProperties.AddRange(properties);
			currentColumnList.AddRange(properties.Select(p => p.MappedColumn()));

			AddAllPropertiesToGrid();
			ResetAllPropertyComboBoxes();
			EventRaisingDisabled = false;
		}

		public IColumn GetMappedColumnFor(ComponentPropertyMarker property)
		{
			int rowIndex = currentProperties.FindIndex(p => ReferenceEquals(p, property));
			if (rowIndex == -1) return null;
			return currentColumnList[rowIndex];
		}

		public void SetMappings(IEnumerable<ColumnComponentPropertyMapping> newMappings)
		{
			EventRaisingDisabled = true;
			
			mappings.Clear();
			mappings.AddRange(newMappings);
			AddAllPropertiesToGrid();
			ResetAllPropertyComboBoxes();

			EventRaisingDisabled = false;
		}

		private void AddAllPropertiesToGrid()
		{
			gridMappings.Rows.Clear();

			foreach (var marker in currentProperties)
			{
				AddNewColumnPropertyMappingRow(marker);
			}
		}

		private void AddNewColumnPropertyMappingRow(ComponentPropertyMarker marker)
		{
			var row = new DataGridViewRow();
			row.Tag = marker;
			gridMappings.AllowUserToAddRows = true;
			row.CreateCells(gridMappings);
			row.Cells[NameColumn.Index].Value = marker.RepresentedProperty.Name;
			gridMappings.Rows.Add(row);

			gridMappings.AllowUserToAddRows = false;
		}

		private void ResetAllPropertyComboBoxes()
		{
			// Setup the columns that can be selected in the mapped column combo boxes
			BindingList<ColumnItem> bindingList = new BindingList<ColumnItem>();
			bindingList.Add(new ColumnItem(null));

			foreach (var column in possibleColumns)
			{
				bindingList.Add(new ColumnItem(column));
			}

			MappedColumn.DataSource = new BindingSource { DataSource = bindingList };
			MappedColumn.DataPropertyName = "Column";
			MappedColumn.DisplayMember = "Label";
			MappedColumn.ValueMember = "Column";

			if (mappings.Count == 0) // Mappings have not been set, so cannot set the mapped columsn yet
				return;

			currentColumnList.Clear();

			// This code assumes that the mappings in the collection are in the same
			// order as the ones on the UI.
			for (int i = 0; i < mappings.Count; i++)
			{
				IgnoreMappedColumnChanges = true;

				IColumn column = mappings[i].Column;

				if(column != null)
					gridMappings.Rows[i].Cells[MappedColumn.Index].Value = column;
				currentColumnList.Add(column);

				IgnoreMappedColumnChanges = false;
			}
		}

		private class ColumnItem
		{
			public ColumnItem(IColumn column)
			{
				Column = column;
			}

			public IColumn Column { get; set; }
			public string Label
			{
				get
				{
					if (Column == null || Column.Parent == null) return "";

					return Column.Parent.Name + "." + Column.Name;
				}
			}
		}

		public void SetPossibleColumns(IEnumerable<IColumn> columns)
		{
			EventRaisingDisabled = true;
			possibleColumns.Clear();
			possibleColumns.AddRange(columns);
			ResetAllPropertyComboBoxes();
			EventRaisingDisabled = false;
		}

		private void gridMappings_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			var row = gridMappings.Rows[e.RowIndex];

			if (e.ColumnIndex == MappedColumn.Index)
			{
				if (IgnoreMappedColumnChanges) return;
				// Update our column cache.
				currentColumnList[e.RowIndex] = gridMappings.Rows[e.RowIndex].Cells[MappedColumn.Index].Value as IColumn;

				PropertyMappingChanged.RaiseEventEx(this, new GenericEventArgs<ComponentPropertyMarker>(row.Tag as ComponentPropertyMarker));
			}
		}

		private void gridMappings_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			//throw new Exception("A data validation error occurred where it should not have", e.Exception);
		}

		private void tbName_TextChanged(object sender, EventArgs e)
		{
			ComponentNameChanged.RaiseEventEx(this);
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DeleteComponent.RaiseEventEx(this);
		}
	}
}
