using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ArchAngel.Interfaces.ITemplate;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using Slyce.Common;
using Slyce.Common.EventExtensions;

namespace ArchAngel.Providers.EntityModel.UI.PropertyGrids
{
	public partial class FormEntity : UserControl, IEntityForm, IEventSender
	{
		private int currentMappedTableHeight = 5;
		private int currentChildEntityHeight = 5;
		private const int ComboTableRowHeight = 30;
		private const int ControlGap = 5;

		private readonly List<ITable> tables = new List<ITable>(10);
		private readonly List<ITable> mappedTables = new List<ITable>(10);
		private readonly List<Entity> entities = new List<Entity>(10);
		private readonly List<Property> properties = new List<Property>(10);
		private readonly List<Mapping> mappings = new List<Mapping>(10);
		private bool IgnoreMappedColumnChanges;
		private bool ignoreDiscriminatorChanges;
		private readonly List<IColumn> currentColumnList = new List<IColumn>(10);
		//private Discriminator discriminator;
		private List<IColumn> availableColumns;
		private Image editImage;

		public event EventHandler NameChanged;
		public event EventHandler AddNewProperty;
		public event EventHandler<PropertyNameChangeEventArgs> PropertyNameChanged;
		public event EventHandler<GenericEventArgs<Property>> RemoveProperty;
		public event EventHandler<GenericEventArgs<Property>> EditProperty;
		public event EventHandler MappingsChanged;
		public event EventHandler CreateNewTableFromEntity;
		public event EventHandler RemoveEntity;
		public event EventHandler DiscriminatorChanged;
		public event EventHandler ParentEntityChanged;
		public event EventHandler<MappingEventArgs> MappingRemoved;
		public event EventHandler<MappingEventArgs> NewMappingAdded;
		public event EventHandler<GenericEventArgs<Entity>> ChildEntityAdded;
		public event EventHandler<GenericEventArgs<Entity>> ChildEntityRemoved;
		public event EventHandler<GenericEventArgs<Property>> SingleMappingChanged;
		public event EventHandler<GenericEventArgs<Property>> CopyProperty;

		public bool EventRaisingDisabled { get; set; }

		public FormEntity()
		{
			InitializeComponent();

			tbName.TextChanged += (s, e) => NameChanged.RaiseEventEx(this);
			buttonCreateTable.Click += (s, e) => CreateNewTableFromEntity.RaiseEventEx(this);
			buttonRemoveEntity.Click += (s, e) => RemoveEntity.RaiseEventEx(this);
			buttonNewProperty.Click += (s, e) => AddNewProperty.RaiseEventEx(this);

			throw new NotImplementedException("TODO: ddiscriminator stuff");
			//comboBoxDiscriminatorColumn.SelectedIndexChanged += (s, e) => RecalculateDiscriminator();
			//comboBoxDiscriminatorOperation.SelectedIndexChanged += (s, e) => RecalculateDiscriminator();
			//textBoxDiscriminatorValue.TextChanged += (s, e) => RecalculateDiscriminator();
			buttonClearDiscriminator.Click += buttonClearDiscriminator_Click;

			comboBoxParentEntity.SelectedIndexChanged += comboBoxParentEntity_SelectedIndexChanged;

			comboBoxParentEntity.DropDownStyle = ComboBoxStyle.DropDownList;

			using (Stream editImageStream = GetType().Assembly.GetManifestResourceStream("ArchAngel.Providers.EntityModel.Resources.edit_16_h.png"))
			{
				if (editImageStream == null) return;

				editImage = Image.FromStream(editImageStream);
			}
		}

		private int NumberOfMappedTables { get { return panelMappedTables.Controls.Count; } }

		//public Discriminator Discriminator
		//{
		//    get
		//    {
		//        if (discriminator == null)
		//            discriminator = new DiscriminatorImpl();
		//        return discriminator;
		//    }
		//    set
		//    {
		//        discriminator = value;
		//        SetupDiscriminatorFields();
		//    }
		//}

		public Entity ParentEntity
		{
			get
			{
				return GetParentEntity();
			}
			set
			{
				EventRaisingDisabled = true;
				SetParentEntity(value);
				EventRaisingDisabled = false;
			}
		}

		public void SetChildEntities(IEnumerable<Entity> newChildEntities)
		{
			EventRaisingDisabled = true;

			foreach (var child in newChildEntities)
			{
				AddChildEntityRow(child);
			}
			AddChildEntityRow(null);

			EventRaisingDisabled = false;
		}

		public void SetSelectedPropertyName(Property property)
		{
			int rowIndex = properties.IndexOf(property);

			if (rowIndex < 0 || rowIndex >= gridViewProperties.RowCount)
				return;

			gridViewProperties.CurrentCell = gridViewProperties.Rows[rowIndex].Cells[PropertyNameColumn.Index];
		}

		public string EntityName
		{
			get
			{
				return tbName.Text;
			}
			set
			{
				EventRaisingDisabled = true;
				tbName.Text = value;
				EventRaisingDisabled = false;
			}
		}

		public IEnumerable<Mapping> Mappings
		{
			get
			{
				return mappings;
			}
			set
			{
				mappings.Clear();
				mappings.AddRange(value);
				RefreshMappings();
			}
		}

		private IEnumerable<ITable> SelectedTables
		{
			get
			{
				foreach (ComboBoxEx box in panelMappedTables.Controls)
				{
					if (box.SelectedItem == null) continue;

					yield return ((ComboBoxItemEx<ITable>)box.SelectedItem).Object;
				}
			}
		}

		public void SetAvailableTables(IEnumerable<ITable> newTableList)
		{
			tables.Clear();
			tables.AddRange(newTableList);
		}

		public void SetAvailableEntities(IEnumerable<Entity> newEntities)
		{
			entities.Clear();
			entities.AddRange(newEntities);

			ClearAndSetupEntityList();
		}

		public void SetVirtualProperties(IEnumerable<IUserOption> virtualProperties)
		{
			virtualPropertyGrid1.SetVirtualProperties(virtualProperties);
		}

		public void RefreshVirtualProperties()
		{
			virtualPropertyGrid1.RefreshVisibilities();
		}

		private void ClearAndSetupEntityList()
		{
			var entity = GetSelectedObject<Entity>(comboBoxParentEntity);

			EventRaisingDisabled = true;
			comboBoxParentEntity.Items.Clear();
			AddItemsToComboBox(comboBoxParentEntity, new Entity[] { null }.Concat(entities), entity, f => f.Name);
			EventRaisingDisabled = false;
		}

		private void SetParentEntity(Entity value)
		{
			SetSelectedObject(comboBoxParentEntity, value);
			PopulateCopyPropertyListFrom(value);
		}

		private void PopulateCopyPropertyListFrom(Entity entity)
		{
			buttonCopyProperty.DropDownItems.Clear();

			if (entity == null) return;

			foreach (var prop in entity.Properties)
			{
				var button = buttonCopyProperty.DropDownItems.Add(prop.Entity.Name + "." + prop.Name);
				button.Tag = prop;
				button.Click += copyProperty_Click;
			}
		}

		private void copyProperty_Click(object sender, EventArgs e)
		{
			var button = sender as ToolStripItem;
			if (button == null) return;

			CopyProperty.RaiseEvent(this, new GenericEventArgs<Property>((Property)button.Tag));
		}

		private Entity GetParentEntity()
		{
			return GetSelectedObject<Entity>(comboBoxParentEntity);
		}

		public void SetProperties(IEnumerable<Property> newPropList)
		{
			properties.Clear();
			properties.AddRange(newPropList);
			RefreshMappings();
		}

		public IColumn GetMappedColumnFor(Property property)
		{
			int rowIndex = properties.IndexOf(property);
			return currentColumnList[rowIndex];
		}

		//private void RecalculateDiscriminator()
		//{
		//    if (ignoreDiscriminatorChanges || EventRaisingDisabled) return;

		//    Grouping g = new AndGrouping();
		//    IColumn column = GetColumn();
		//    Operator op = GetOperator();
		//    ExpressionValue value = GetExpressionValue();
		//    if (column != null && op != null && value != null)
		//        g.AddCondition(new ConditionImpl(column, op, value));
		//    Discriminator.RootGrouping = g;
		//    DiscriminatorChanged.RaiseEventEx(this);
		//}

		//private void SetupDiscriminatorFields()
		//{
		//    IColumn column = null;
		//    Operator op = null;
		//    string exprText = "";

		//    if (discriminator != null && discriminator.RootGrouping != null)
		//    {
		//        Condition firstCondition = discriminator.RootGrouping.Conditions.ElementAtOrDefault(0);
		//        if (firstCondition != null)
		//        {
		//            column = firstCondition.Column;
		//            op = firstCondition.Operator;
		//            exprText = firstCondition.ExpressionValue.Value;
		//        }
		//    }

		//    ignoreDiscriminatorChanges = true;
		//    comboBoxDiscriminatorColumn.Items.Clear();
		//    AddItemsToComboBox(comboBoxDiscriminatorColumn, GetAvailableColumns(), column, c => c.Parent.Name + "." + c.Name);

		//    comboBoxDiscriminatorOperation.Items.Clear();
		//    AddItemsToComboBox(comboBoxDiscriminatorOperation, Operator.BuiltInOperations, op, o => o.DisplayText);

		//    textBoxDiscriminatorValue.Text = exprText;
		//    ignoreDiscriminatorChanges = false;
		//}

		private static void AddItemsToComboBox<T>(ComboBox comboBox, IEnumerable<T> list, T column, Func<T, string> displayTextFunction) where T : class
		{
			foreach (var col in list)
			{
				comboBox.Items.Add(new ComboBoxItemEx<T>(col, displayTextFunction));
				if (col == column)
					comboBox.SelectedIndex = comboBox.Items.Count - 1;
			}
			if (column == null)
				comboBox.SelectedIndex = -1;
		}

		//private ExpressionValue GetExpressionValue()
		//{
		//    return new ExpressionValueImpl(textBoxDiscriminatorValue.Text);
		//}

		//private Operator GetOperator()
		//{
		//    return GetSelectedObject<Operator>(comboBoxDiscriminatorOperation) ?? Operator.FromString("");
		//}

		private IColumn GetColumn()
		{
			var itemEx = comboBoxDiscriminatorColumn.SelectedItem as ComboBoxItemEx<IColumn>;

			return itemEx == null ? null : itemEx.Object;
		}

		private IColumn GetColumnFromRow(int rowIndex)
		{
			var item = GetItemFromRow<IColumn>(rowIndex, MappedColumnColumn.Index);
			return item;
		}

		private T GetItemFromRow<T>(int rowIndex, int columnIndex) where T : class
		{
			return gridViewProperties.Rows[rowIndex].Cells[columnIndex].Value as T;
		}

		private void RefreshMappings()
		{
			Utility.SuspendPainting(this);
			ClearAllMappings();

			AddAllPropertiesToGrid();

			// Add the mapped tables to the list.
			foreach (Mapping mapping in mappings)
			{
				AddMappedTableRow(mapping.FromTable);
			}

			// Add the user selected tables that aren't in the list of mappings.
			var extraTables = mappedTables.Except(mappings.Select(m => m.FromTable)).ToList();
			foreach (var table in extraTables)
			{
				if (table == null) continue;
				AddMappedTableRow(table);
			}

			AddMappedTableRow(null);
			CleanUpMappedTablePanel();

			ResetAllPropertyComboBoxes();
			Utility.ResumePainting(this);
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

		private void ResetAllPropertyComboBoxes()
		{
			RecalculateMappedColumns();

			// Setup the columns that can be selected in the mapped column combo boxes
			BindingList<ColumnItem> bindingList = new BindingList<ColumnItem>();
			bindingList.Add(new ColumnItem(null));

			availableColumns = GetAvailableColumns().ToList();
			foreach (var column in availableColumns)
			{
				bindingList.Add(new ColumnItem(column));
			}

			MappedColumnColumn.DataSource = new BindingSource { DataSource = bindingList };
			MappedColumnColumn.DataPropertyName = "Column";
			MappedColumnColumn.DisplayMember = "Label";
			MappedColumnColumn.ValueMember = "Column";

			if (mappings.Count == 0) // Mappings have not been set, so cannot set the mapped columsn yet
				return;
			// This code assumes that the properties in the collection are in the same
			// order as the ones on the UI.
			for (int i = 0; i < properties.Count; i++)
			{
				IgnoreMappedColumnChanges = true;

				gridViewProperties.Rows[i].Cells[MappedColumnColumn.Index].Value = currentColumnList[i];

				IgnoreMappedColumnChanges = false;
			}
		}

		private void RecalculateMappedColumns()
		{
			currentColumnList.Clear();

			for (int i = 0; i < properties.Count; i++)
			{
				currentColumnList.Add(properties[i].MappedColumn());
			}
		}

		private void CleanUpMappedTablePanel()
		{
			// The +5 is to add a small gap at the bottom of the panel
			int heightNeeded = panelMappedTables.Controls.Count * ComboTableRowHeight + 5;
			panelMappedTablesContainer.Height = Math.Max(heightNeeded, ComboTableRowHeight);

			int bottomOfPanel = panelMappedTablesContainer.Bottom + 5;
			gridViewProperties.Top = bottomOfPanel;
			gridViewProperties.Height = gridViewProperties.Parent.Height - bottomOfPanel - 5;
		}

		private void AddAllPropertiesToGrid()
		{
			foreach (var property in properties)
			{
				AddNewColumnPropertyMappingRow(property);
			}
		}

		private void AddNewColumnPropertyMappingRow(Property property)
		{
			var row = new DataGridViewRow();
			row.Tag = property;
			gridViewProperties.AllowUserToAddRows = true;
			row.CreateCells(gridViewProperties);
			row.Cells[PropertyNameColumn.Index].Value = property.Name;
			gridViewProperties.Rows.Add(row);

			gridViewProperties.AllowUserToAddRows = false;
		}

		private IEnumerable<IColumn> GetAvailableColumns()
		{
			return SelectedTables.SelectMany(t => t.Columns);
		}

		private void AddChildEntityRow(Entity child)
		{
			CreateRow(child, entities, f => f.Name, panelChildEntities, panelRemoveChild, ref currentChildEntityHeight, childCombo_SelectedIndexChanged, removeChildButton_Click);
		}

		private void AddMappedTableRow(ITable table)
		{
			CreateRow(table, tables, f => f.Name, panelMappedTables, panelRemoveTableButtons, ref currentMappedTableHeight, tableCombo_SelectedIndexChanged, button_Click);
		}

		private void CreateRow<T>(T obj, List<T> objects, Func<T, string> displayFunction, PanelEx comboPanel, PanelEx buttonPanel, ref int currentHeight, EventHandler selectedIndexChanged, EventHandler removeButtonClick) where T : class
		{
			ComboBoxEx combo = new ComboBoxEx();
			combo.Top = currentHeight;
			combo.Left = ControlGap;
			combo.Width = comboPanel.Width - (2 * ControlGap);
			combo.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
			combo.DropDownStyle = ComboBoxStyle.DropDownList;

			AddItemsToComboBox(combo, objects, obj, displayFunction);

			combo.SelectedIndexChanged += selectedIndexChanged;

			comboPanel.Controls.Add(combo);

			Button button = new Button();
			button.Text = "X";
			button.BackColor = Color.Red;
			button.Click += removeButtonClick;
			button.Top = currentHeight;
			button.Width = 25;
			button.Left = 5;
			if (obj == null)
				button.Enabled = false;
			buttonPanel.Controls.Add(button);

			currentHeight += ComboTableRowHeight;
		}

		private void RemoveTableRow(int index)
		{
			int tableIndex = ((ComboBoxEx)panelMappedTables.Controls[index]).SelectedIndex;

			Utility.SuspendPainting(this);

			panelMappedTables.Controls.RemoveAt(index);
			panelRemoveTableButtons.Controls.RemoveAt(index);

			for (int i = index; i < panelMappedTables.Controls.Count; i++)
			{
				panelMappedTables.Controls[i].Top -= ComboTableRowHeight;
				panelRemoveTableButtons.Controls[i].Top -= ComboTableRowHeight;
			}

			for (int i = 0; i < gridViewProperties.RowCount; i++)
			{
				IColumn column = GetItemFromRow<IColumn>(i, MappedColumnColumn.Index);
				if (column == null) continue;

				if (tables.IndexOf(column.Parent) == tableIndex)
				{
					// If this column's parent is the removed table, set the mapped column combobox's selected
					// value to null
					var cell = gridViewProperties.Rows[i].Cells[MappedColumnColumn.Index];
					cell.Value = null;
				}
			}

			CleanUpMappedTablePanel();
			ResetAllPropertyComboBoxes();

			Utility.ResumePainting(this);
		}

		private void ClearAllMappings()
		{
			gridViewProperties.Rows.Clear();
			toolTipGridControl.RemoveAll();

			foreach (ComboBoxEx combo in panelMappedTables.Controls)
				combo.SelectedIndexChanged -= tableCombo_SelectedIndexChanged;
			panelMappedTables.Controls.Clear();

			foreach (Button button in panelRemoveTableButtons.Controls)
				button.Click -= button_Click;
			panelRemoveTableButtons.Controls.Clear();
			currentMappedTableHeight = 5;
		}

		public void Clear()
		{
			EventRaisingDisabled = true;

			tbName.Text = "";
			ClearAllMappings();
			tables.Clear();
			mappedTables.Clear();
			properties.Clear();
			mappings.Clear();

			comboBoxDiscriminatorColumn.Items.Clear();
			comboBoxDiscriminatorColumn.SelectedIndex = -1;
			comboBoxDiscriminatorOperation.Items.Clear();
			comboBoxDiscriminatorOperation.SelectedIndex = -1;

			textBoxDiscriminatorValue.Text = "";

			comboBoxParentEntity.Items.Clear();
			comboBoxParentEntity.SelectedIndex = -1;

			ClearChildEntities();

			EventRaisingDisabled = false;
		}

		private void ClearChildEntities()
		{
			foreach (ComboBoxEx control in panelChildEntities.Controls)
				control.SelectedIndexChanged -= childCombo_SelectedIndexChanged;
			foreach (Button control in panelRemoveChild.Controls)
				control.Click -= removeChildButton_Click;

			panelChildEntities.Controls.Clear();
			panelRemoveChild.Controls.Clear();

			currentChildEntityHeight = 5;
		}

		private T GetSelectedObject<T>(ComboBox comboBox) where T : class
		{
			if (comboBox.SelectedIndex == -1)
				return null;

			return ((ComboBoxItemEx<T>)comboBox.SelectedItem).Object;
		}

		private void SetSelectedObject<T>(ComboBox comboBox, T value) where T : class
		{
			for (int i = 0; i < comboBox.Items.Count; i++)
			{
				ComboBoxItemEx<T> item = (ComboBoxItemEx<T>)comboBox.Items[i];
				if (item.Object == value) comboBox.SelectedIndex = i;
			}
		}

		void button_Click(object sender, EventArgs e)
		{
			Button button = sender as Button;
			if (button == null) return;

			int rowIndex = GetTableRowIndex(button);
			RemoveTableRow(rowIndex);
		}

		private void removeChildButton_Click(object sender, EventArgs e)
		{
			Button button = sender as Button;
			if (button == null) return;

			int rowIndex = GetChildRowIndex(button);
			RemoveChildRow(rowIndex);
		}

		private void RemoveChildRow(int index)
		{
			Entity child = GetSelectedObject<Entity>((ComboBox)panelChildEntities.Controls[index]);

			Utility.SuspendPainting(this);

			panelChildEntities.Controls.RemoveAt(index);
			panelRemoveChild.Controls.RemoveAt(index);

			for (int i = index; i < panelChildEntities.Controls.Count; i++)
			{
				panelChildEntities.Controls[i].Top -= ComboTableRowHeight;
				panelRemoveChild.Controls[i].Top -= ComboTableRowHeight;
			}

			Utility.ResumePainting(this);

			ChildEntityRemoved.RaiseEventEx(this, new GenericEventArgs<Entity>(child));
		}

		private int GetChildRowIndex(Button button)
		{
			return panelRemoveChild.Controls.IndexOf(button);
		}

		private void childCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			var combo = sender as ComboBox;
			if (sender == null) return;

			Entity child = GetSelectedObject<Entity>(combo);

			int rowIndex = panelChildEntities.Controls.Count - 1;
			panelRemoveChild.Controls[rowIndex].Enabled = true;

			AddChildEntityRow(null);

			ChildEntityAdded.RaiseEventEx(this, new GenericEventArgs<Entity>(child));
		}

		void buttonClearDiscriminator_Click(object sender, EventArgs e)
		{
			//Discriminator = new DiscriminatorImpl();
			//DiscriminatorChanged.RaiseEventEx(this);
		}

		void comboBoxParentEntity_SelectedIndexChanged(object sender, EventArgs e)
		{
			ParentEntityChanged.RaiseEventEx(this);
			PopulateCopyPropertyListFrom(ParentEntity);
		}

		void tableCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBoxEx combo = sender as ComboBoxEx;
			if (combo == null) return;

			int rowIndex = GetTableRowIndex(combo);
			if (rowIndex == -1) return;

			Utility.SuspendPainting(this);
			if (rowIndex == NumberOfMappedTables - 1)
			{
				GetRemoveButtonAt(rowIndex).Enabled = true;
				AddMappedTableRow(null);
				CleanUpMappedTablePanel();
			}
			else
				mappedTables[rowIndex] = ((ComboBoxItemEx<ITable>)combo.SelectedItem).Object;

			ResetAllPropertyComboBoxes();
			Utility.ResumePainting(this);
		}

		private Button GetRemoveButtonAt(int rowIndex)
		{
			return (Button)panelRemoveTableButtons.Controls[rowIndex];
		}

		private int GetTableRowIndex(ComboBoxEx combo)
		{
			return panelMappedTables.Controls.IndexOf(combo);
		}

		private int GetTableRowIndex(Button button)
		{
			return panelRemoveTableButtons.Controls.IndexOf(button);
		}

		public void StartBulkUpdate()
		{
			Utility.SuspendPainting(this);
		}

		public void EndBulkUpdate()
		{
			Utility.ResumePainting(this);
		}

		private void gridViewProperties_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			Property property = gridViewProperties.Rows[e.RowIndex].Tag as Property;

			if (e.ColumnIndex == DeletePropertyColumn.Index)
			{
				RemoveProperty.RaiseEvent(this, new GenericEventArgs<Property>(property));
			}
			else if (e.ColumnIndex == EditPropertyColumn.Index)
			{
				EditProperty.RaiseEvent(this, new GenericEventArgs<Property>(property));
			}
		}

		private void gridViewProperties_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			var row = gridViewProperties.Rows[e.RowIndex];

			if (e.ColumnIndex == MappedColumnColumn.Index)
			{
				if (IgnoreMappedColumnChanges) return;
				// Update our column cache.
				currentColumnList[e.RowIndex] = GetColumnFromRow(e.RowIndex);

				SingleMappingChanged.RaiseEventEx(this, new GenericEventArgs<Property>(row.Tag as Property));
			}
			else if (e.ColumnIndex == PropertyNameColumn.Index)
			{
				Property changedProperty = row.Tag as Property;
				PropertyNameChanged.RaiseEventEx(this, new PropertyNameChangeEventArgs(changedProperty, row.Cells[e.ColumnIndex].Value as string));
			}
		}

		private void gridViewProperties_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			throw new Exception("A data validation error occurred where it should not have", e.Exception);
		}

		private void gridViewProperties_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			if (e.ColumnIndex == EditPropertyColumn.Index && e.RowIndex >= 0)
			{
				if (editImage == null) return;

				e.Paint(e.CellBounds, DataGridViewPaintParts.All);
				int topPadding = (e.CellBounds.Height - 16) / 2;
				int leftPadding = (e.CellBounds.Width - 16) / 2;
				e.Graphics.DrawImage(editImage, e.CellBounds.Left + leftPadding, e.CellBounds.Top + topPadding, 16, 16);

				e.Handled = true;
			}
		}
	}
}
