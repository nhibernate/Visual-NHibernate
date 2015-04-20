using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ArchAngel.Interfaces.ITemplate;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using DevComponents.DotNetBar.Controls;
using Slyce.Common;
using Slyce.Common.EventExtensions;

namespace ArchAngel.Providers.EntityModel.UI.PropertyGrids
{
	public partial class FormMapping : UserControl, IMappingForm, IEventSender
	{
		public bool EventRaisingDisabled { get; set; }

		private readonly List<Entity> entities = new List<Entity>();
		private readonly List<ITable> tables = new List<ITable>();

		public FormMapping()
		{
			InitializeComponent();

			cbEntity.SelectedIndexChanged += cbEntity_SelectedIndexChanged;
			cbTable.SelectedIndexChanged += cbTable_SelectedIndexChanged;
			buttonRemoveMapping.Click += (s, e) => RemoveMapping.RaiseEventEx(this);
		}

		public void Clear()
		{
			using (new EventDisabler(this))
			{
				entities.Clear();
				tables.Clear();

				gridControl.Clear();
				cbTable.Items.Clear();
				cbEntity.Items.Clear();
			}
		}

		public void SetVirtualProperties(IEnumerable<IUserOption> virtualProperties)
		{
			virtualPropertyGrid1.SetVirtualProperties(virtualProperties);
		}

	    public void RefreshVirtualProperties()
	    {
	        virtualPropertyGrid1.RefreshVisibilities();
	    }

	    public IEnumerable<Entity> Entities 
		{
			get {  return entities; }
			set
			{
				entities.Clear(); 
				entities.AddRange(value);
				RefreshEntities();
			} 
		}

		public IEnumerable<ITable> Tables
		{
			get { return tables; }
			set
			{
				tables.Clear(); 
				tables.AddRange(value);
				RefreshTables();
			}
		}

		public Entity ToEntity
		{
			get
			{
				return GetSelectedEntity();
			}
			set
			{
				using(new EventDisabler(this))
				{
					var index = entities.IndexOf(value);
					if(index == -1)
					{
						index = entities.Count;
						entities.Add(value);
						cbEntity.Items.Add(new ComboBoxItemEx<Entity>(value, c => c.Name));
					}
					cbEntity.SelectedIndex = index;
				}
			}
		}

		public ITable FromTable
		{
			get
			{
				return GetSelectedTable();
			}
			set
			{
				using (new EventDisabler(this))
				{
					var index = tables.IndexOf(value);
					if (index == -1)
					{
						index = tables.Count;
						tables.Add(value);
						cbTable.Items.Add(new ComboBoxItemEx<ITable>(value, c => c.Name));
					}
					cbTable.SelectedIndex = index;
				}
			}
		}

		public int GetSelectedIndex(ComboTreeGridControl.Side side, int rowIndex)
		{
			return gridControl.GetControlFrom<ComboBox>(side, rowIndex).SelectedIndex;
		}

		public object GetSelectedItem(ComboTreeGridControl.Side side, int rowIndex)
		{
			return gridControl.GetControlFrom<ComboBox>(side, rowIndex).SelectedItem;
		}

		public IEnumerable<ColumnPropertyMapping> Mappings
		{
			get
			{
				for(int i = 0; i < gridControl.ItemCount; i++)
				{
					var propertyIndex = GetSelectedItem(ComboTreeGridControl.Side.Left, i) as ComboBoxItemEx<Property>;
					var columnIndex = GetSelectedItem(ComboTreeGridControl.Side.Right, i) as ComboBoxItemEx<IColumn>;

					// Skip incomplete rows
					if (propertyIndex == null || columnIndex == null
						|| propertyIndex.Object == null || columnIndex.Object == null) continue;

					yield return new ColumnPropertyMapping(propertyIndex.Object, columnIndex.Object);
				}
			}
			set
			{
				gridControl.Clear();
				foreach(var m in value)
				{
					AddRow(m);
				}
				AddEmptyRow();
			}
		}

		private void AddRow(ColumnPropertyMapping m)
		{
			Entity entity = GetSelectedEntity();
			ITable table = GetSelectedTable();

			var leftCT = CreatePropertyComboBox(entity, m.Property);
			var rightCT = CreateColumnComboBox(table, m.Column);
			var removeButton = CreateRemoveButton();

			gridControl.AddRow(leftCT, rightCT, removeButton);
		}

		private void AddEmptyRow()
		{
			var leftCT = CreatePropertyComboBox(GetSelectedEntity(), null);
			var rightCT = CreateColumnComboBox(GetSelectedTable(), null);
			var removeButton = CreateRemoveButton();
			removeButton.Enabled = false;

			gridControl.AddRow(leftCT, rightCT, removeButton);
		}

		private Button CreateRemoveButton()
		{
			var b = new Button();
			b.Text = "X";
			b.BackColor = Color.Red;
			b.Click += removeButton_Click;
			return b;
		}

		private ComboBoxEx CreateColumnComboBox(ITable entity, IColumn column)
		{
			var cb = CreateComboBox();

			int index = -1;
			if(entity != null)
				for (int i = 0; i < entity.Columns.Count; i++)
				{
					var item = entity.Columns[i];
					cb.Items.Add(new ComboBoxItemEx<IColumn>(item, c => c.Name));
					if (item == column) index = i;
				}
			cb.SelectedIndex = index;
			cb.SelectedIndexChanged += control_SelectedIndexChanged;

			return cb;
		}

		private ComboBoxEx CreatePropertyComboBox(Entity entity, Property property)
		{
			var cb = CreateComboBox();

			int index = -1;
			int i = 0;
			Func<Property, string> function = p => p.Name;
			if (entity != null)
				foreach (var item in entity.Properties)
				{
					var cbi = new ComboBoxItemEx<Property>(item, function);
					cb.Items.Add(cbi);
					if(property == item) index = i;
					i++;
				}
			cb.SelectedIndex = index;
			cb.SelectedIndexChanged += control_SelectedIndexChanged;

			return cb;
		}

		private ComboBoxEx CreateComboBox()
		{
			var control = new ComboBoxEx();

			control.Width = 50;
			control.DropDownStyle = ComboBoxStyle.DropDownList;
			control.BackColor = Color.Red;

			return control;
		}

		private void RefreshEntities()
		{
			Entity currentlySelectedEntity = GetSelectedEntity();
			cbEntity.Items.Clear();
			foreach(var e in entities)
			{
				AddEntityToComboBox(e);
			}
			
			cbEntity.SelectedIndex = entities.IndexOf(currentlySelectedEntity);
		}

		private void RefreshTables()
		{
			ITable currentlySelectedTable = GetSelectedTable();
			cbTable.Items.Clear();
			foreach (var t in tables)
			{
				AddTableToComboBox(t);
			}

			cbTable.SelectedIndex = tables.IndexOf(currentlySelectedTable);
		}

		private void AddEntityToComboBox(Entity e)
		{
			cbEntity.Items.Add(new ComboBoxItemEx<Entity>(e, en=>en.Name));
		}

		private void AddTableToComboBox(ITable t)
		{
			cbTable.Items.Add(new ComboBoxItemEx<ITable>(t, te => te.Name));
		}

		private Entity GetSelectedEntity()
		{
			if (cbEntity.SelectedIndex == -1)
				return null;

			return ((ComboBoxItemEx<Entity>) cbEntity.SelectedItem).Object;
		}

		private ITable GetSelectedTable()
		{
			if (cbTable.SelectedIndex == -1)
				return null;

			return ((ComboBoxItemEx<ITable>) cbTable.SelectedItem).Object;
		}


		private void control_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBoxEx box = sender as ComboBoxEx;
			if (box == null) return;

			// Get the other CB on this row, then re-sort its items.
			int row = gridControl.IndexOf(box);

			var boxes = gridControl.GetControlFrom<ComboBox>(row);

			// If this is the last row and it now has both columns set,
			// add a new blank row
			if (row == gridControl.ItemCount - 1
				&& boxes.Left.SelectedIndex > -1
				&& boxes.Right.SelectedIndex > -1)
			{
				// By default the remove button in the empty row is disabled.
				// Now that the row has data, enable it.
				gridControl.GetButtonFrom(row).Enabled = true;

				AddEmptyRow();
			}

			// Only trigger the event if both sides are set to something.
			// Otherwise, only half the mapping is set, and nothing will 
			// have actually changed yet.
			if(boxes.Left.SelectedIndex > -1 && boxes.Right.SelectedIndex > -1)
				MappingsChanged.RaiseEventEx(this);
		}

		private void removeButton_Click(object sender, EventArgs e)
		{
			Button button = sender as Button;
			if (button == null) return;

			int row = gridControl.IndexOf(button);

			gridControl.RemoveRow(row);

			MappingsChanged.RaiseEventEx(this);
		}

		void cbTable_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Clear the mappings
			Mappings = new List<ColumnPropertyMapping>();
			FromTableChanged.RaiseEventEx(this);
			MappingsChanged.RaiseEventEx(this);
		}

		void cbEntity_SelectedIndexChanged(object sender, EventArgs e)
		{
			Mappings = new List<ColumnPropertyMapping>();
			ToEntityChanged.RaiseEventEx(this);
			MappingsChanged.RaiseEventEx(this);
		}

		public void StartBulkUpdate()
		{
			Slyce.Common.Utility.SuspendPainting(this);
		}

		public void EndBulkUpdate()
		{
			Slyce.Common.Utility.ResumePainting(this);
		}

		public event EventHandler ToEntityChanged;
		public event EventHandler FromTableChanged;
		public event EventHandler MappingsChanged;
		public event EventHandler RemoveMapping;
	}
}
