using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;

namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
	public partial class InheritanceHierarchy : UserControl
	{
		internal event EventHandler EntityDeleted;
		private const int IMAGE_DELETE = 0;
		//private const int IMAGE_REMOVE = 1;
		private ArchAngel.Providers.EntityModel.Model.DatabaseLayer.ITable _Table;
		private List<Entity> MappedEntities;
		private Timer deleteColumnTimer = new Timer();
		private int ColumnToDelete = -1;
		private bool BusyPopulating = false;

		public InheritanceHierarchy()
		{
			InitializeComponent();

			SetStyle(
					ControlStyles.UserPaint |
					ControlStyles.AllPaintingInWmPaint |
					ControlStyles.OptimizedDoubleBuffer, true);

			deleteColumnTimer.Tick += new EventHandler(deleteColumnTimer_Tick);
			deleteColumnTimer.Interval = 20;
			comboBoxDiscriminatorType.SelectedIndex = 0;
			LayoutDiscriminatorControls();
		}

		public ArchAngel.Providers.EntityModel.Model.DatabaseLayer.ITable Table
		{
			get { return _Table; }
			set
			{
				_Table = value;

				if (_Table != null)
				{
					MappedEntities = Table.MappedEntities().ToList();
					PopulateDiscriminatorColumns();
					Populate();
				}
			}
		}

		private void PopulateDiscriminatorColumns()
		{
			comboBoxDiscriminator.BeginUpdate();
			comboBoxDiscriminator.Items.Clear();

			for (int i = 0; i < Table.Columns.Count; i++)
				comboBoxDiscriminator.Items.Add(Table.Columns[i].Name);

			comboBoxDiscriminator.Sorted = true;
			comboBoxDiscriminator.EndUpdate();
		}

		private void Populate()
		{
			BusyPopulating = true;
			advTree1.BeginUpdate();
			advTree1.Nodes.Clear();
			advTree1.Columns.Clear();

			// Add Fields column
			advTree1.Columns.Add(new DevComponents.AdvTree.ColumnHeader("Fields"));

			// Add columns for Mapped Entities
			for (int i = 0; i < MappedEntities.Count; i++)
				advTree1.Columns.Add(new DevComponents.AdvTree.ColumnHeader(MappedEntities[i].Name) { Tag = MappedEntities[i] });

			//foreach (DevComponents.AdvTree.ColumnHeader column in advTree1.Columns)
			//    column.Width.AutoSize = true;

			advTree1.Columns[0].Width.AutoSize = true;

			for (int i = 0; i < advTree1.Columns.Count; i++)
				advTree1.Columns[i].Width.Absolute = 150;

			AddTopRow();

			for (int i = 0; i < Table.Columns.Count; i++)
				AddRow(Table.Columns[i]);

			AddBottomRow();

			#region Deal with the base entity
			List<Entity> baseEntities = MappedEntities.Where(e => e.Parent == null).ToList();

			if (baseEntities.Count == 1)
			{
				Entity baseEntity = baseEntities[0];

				if (baseEntity.Discriminator.DiscriminatorType == Enums.DiscriminatorTypes.Column)
				{
					comboBoxDiscriminatorType.SelectedIndex = 0;
					comboBoxDiscriminator.Text = baseEntity.Discriminator.ColumnName;
				}
				else
				{
					comboBoxDiscriminatorType.SelectedIndex = 1;
					textBoxFormula.Text = baseEntity.Discriminator.Formula;
				}
				// Set Base entity
				for (int i = 1; i < advTree1.Columns.Count; i++)
				{
					if (((Entity)advTree1.Columns[i].Tag).Parent == null)
					{
						((DevComponents.DotNetBar.CheckBoxItem)advTree1.Nodes[1].Cells[i].HostedItem).Checked = true;
						break;
					}
				}
				// Set Discriminator value
				for (int i = 1; i < advTree1.Columns.Count; i++)
					((DevComponents.DotNetBar.Controls.TextBoxX)advTree1.Nodes[2].Cells[i].HostedControl).Text = ((Entity)advTree1.Columns[i].Tag).DiscriminatorValue;
			}
			#endregion
			advTree1.EndUpdate();
			BusyPopulating = false;
		}

		private void AddEntityToGrid(Entity entity)
		{
			//advTree1.BeginUpdate();

			try
			{
				// Add a new column for the entity
				DevComponents.AdvTree.ColumnHeader columnHeader = new DevComponents.AdvTree.ColumnHeader(entity.Name) { Tag = entity };
				advTree1.Columns.Add(columnHeader);
				//advTree1.Columns.Insert(advTree1.Columns.Count - 1, columnHeader);
				advTree1.Columns[advTree1.Columns.Count - 1].Width.Absolute = 150;

				// Add top row (Name textbox)
				DevComponents.AdvTree.Cell cell = new DevComponents.AdvTree.Cell();
				DevComponents.DotNetBar.Controls.TextBoxX tb = new DevComponents.DotNetBar.Controls.TextBoxX();
				tb.Text = entity.Name;
				cell.HostedControl = tb;
				advTree1.Nodes[0].Cells.Add(cell);

				// Add second row (is base)
				cell = new DevComponents.AdvTree.Cell();
				DevComponents.DotNetBar.CheckBoxItem checkbox = new DevComponents.DotNetBar.CheckBoxItem();
				cell.HostedItem = checkbox;
				checkbox.CheckedChanged += new DevComponents.DotNetBar.CheckBoxChangeEventHandler(IsBaseCheckbox_CheckedChanged);
				advTree1.Nodes[1].Cells.Add(cell);

				// Add third row (discriminator-value textbox)
				cell = new DevComponents.AdvTree.Cell();
				tb = new DevComponents.DotNetBar.Controls.TextBoxX();
				tb.Text = entity.DiscriminatorValue;
				cell.HostedControl = tb;
				advTree1.Nodes[2].Cells.Add(cell);

				// Add row for all table columns (checkboxes)
				for (int i = 3; i < advTree1.Nodes.Count - 1; i++)
				{
					cell = new DevComponents.AdvTree.Cell();
					cell.CheckBoxAlignment = DevComponents.AdvTree.eCellPartAlignment.Default;
					cell.CheckBoxVisible = true;
					cell.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.CheckBox;
					cell.CheckBoxThreeState = false;
					cell.Checked = false;
					advTree1.Nodes[i].Cells.Add(cell);
				}

				// Add bottom row (delete button)
				cell = new DevComponents.AdvTree.Cell();
				DevComponents.DotNetBar.ButtonItem button = new DevComponents.DotNetBar.ButtonItem();
				button.Text = "Delete";
				button.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
				button.Image = imageList1.Images[IMAGE_DELETE];
				superTooltip1.SetSuperTooltip(button, new DevComponents.DotNetBar.SuperTooltipInfo("Delete", "", "Delete this entity - it will be totally deleted from the model and also from this inheritance hierarchy.", imageListLarge.Images[IMAGE_DELETE], null, DevComponents.DotNetBar.eTooltipColor.Silver));
				cell.HostedItem = button;
				button.Click += new EventHandler(DeleteButton_Click);
				advTree1.Nodes[advTree1.Nodes.Count - 1].Cells.Add(cell);

				//// Add bottom row (remove button)
				//cell = new DevComponents.AdvTree.Cell();
				//button = new DevComponents.DotNetBar.ButtonItem();
				//button.Text = "Remove";
				//button.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
				//button.Image = imageList1.Images[IMAGE_REMOVE];
				//superTooltip1.SetSuperTooltip(button, new DevComponents.DotNetBar.SuperTooltipInfo("Remove", "", "Remove this entity from this inheritance hierarchy.", imageListLarge.Images[IMAGE_REMOVE], null, DevComponents.DotNetBar.eTooltipColor.Silver));
				//cell.HostedItem = button;
				//button.Click += new EventHandler(RemoveButton_Click);
				//advTree1.Nodes[advTree1.Nodes.Count - 1].Cells.Add(cell);

				//if (advTree1.HScrollBarVisible && advTree1.HScrollBar != null)
				//{
				//    //advTree1.HorizontalScroll.Value = advTree1.HorizontalScroll.Maximum;

				//    //advTree1.HScrollBar.Value = advTree1.HScrollBar.Maximum;
				//    if (advTree1.HScrollBar.Maximum > advTree1.Width)
				//    {
				//        advTree1.AutoScroll = false;
				//        int v = advTree1.HScrollBar.Maximum - advTree1.Width;
				//        advTree1.HScrollBar.Value = v - 1;
				//        advTree1.Refresh();
				//        advTree1.HScrollBar.Value = v;
				//        advTree1.Refresh();
				//        advTree1.AutoScroll = true;
				//    }
				//    //double t = (double)((double)advTree1.HScrollBar.Value / advTree1.HScrollBar.Maximum) * advTree1.HorizontalScroll.Maximum;
				//    //advTree1.HorizontalScroll.Value = (int)Math.Floor(t);

				//}
			}
			finally
			{
				//advTree1.EndUpdate();
			}
			//this.Refresh();
		}

		private void AddTopRow()
		{
			DevComponents.AdvTree.Node node = new DevComponents.AdvTree.Node("Name");

			for (int i = 0; i < MappedEntities.Count; i++)
			{
				DevComponents.AdvTree.Cell cell = new DevComponents.AdvTree.Cell();
				DevComponents.DotNetBar.Controls.TextBoxX tb = new DevComponents.DotNetBar.Controls.TextBoxX();
				tb.Text = MappedEntities[i].Name;
				cell.HostedControl = tb;
				node.Cells.Add(cell);
			}
			advTree1.Nodes.Add(node);
			node = new DevComponents.AdvTree.Node("Is Base?");

			for (int i = 0; i < MappedEntities.Count; i++)
			{
				DevComponents.AdvTree.Cell cell = new DevComponents.AdvTree.Cell();
				DevComponents.DotNetBar.CheckBoxItem checkbox = new DevComponents.DotNetBar.CheckBoxItem();
				cell.HostedItem = checkbox;
				checkbox.CheckedChanged += new DevComponents.DotNetBar.CheckBoxChangeEventHandler(IsBaseCheckbox_CheckedChanged);
				node.Cells.Add(cell);
			}
			advTree1.Nodes.Add(node);
			node = new DevComponents.AdvTree.Node("Discriminator Value");

			for (int i = 0; i < MappedEntities.Count; i++)
			{
				DevComponents.AdvTree.Cell cell = new DevComponents.AdvTree.Cell();
				DevComponents.DotNetBar.Controls.TextBoxX tb = new DevComponents.DotNetBar.Controls.TextBoxX();
				cell.HostedControl = tb;
				node.Cells.Add(cell);
			}
			advTree1.Nodes.Add(node);
		}

		void tb_TextChanged(object sender, EventArgs e)
		{
			int columnIndex = advTree1.SelectedNode.Cells.IndexOf(advTree1.SelectedNode.SelectedCell);
			int row = advTree1.SelectedNode.Index;
			DevComponents.DotNetBar.Controls.TextBoxX tb = (DevComponents.DotNetBar.Controls.TextBoxX)sender;

			Entity entity = (Entity)advTree1.Columns[columnIndex].Tag;

			switch (row)
			{
				case 0:
					entity.Name = tb.Text;
					break;
				case 2:
					entity.DiscriminatorValue = tb.Text;
					break;
			}
		}

		private void AddBottomRow()
		{
			DevComponents.AdvTree.Node node = new DevComponents.AdvTree.Node("");

			for (int i = 0; i < MappedEntities.Count; i++)
			{
				DevComponents.AdvTree.Cell cell = new DevComponents.AdvTree.Cell();
				DevComponents.DotNetBar.ButtonItem button = new DevComponents.DotNetBar.ButtonItem();
				button.Text = "Delete";
				button.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
				button.Image = imageList1.Images[IMAGE_DELETE];
				superTooltip1.SetSuperTooltip(button, new DevComponents.DotNetBar.SuperTooltipInfo("Delete", "", "Delete this entity - it will be totally deleted from the model and also from this inheritance hierarchy.", imageListLarge.Images[IMAGE_DELETE], null, DevComponents.DotNetBar.eTooltipColor.Silver));
				cell.HostedItem = button;
				button.Click += new EventHandler(DeleteButton_Click);
				node.Cells.Add(cell);
			}
			advTree1.Nodes.Add(node);

			//node = new DevComponents.AdvTree.Node("");

			//for (int i = 0; i < MappedEntities.Count; i++)
			//{
			//    DevComponents.AdvTree.Cell cell = new DevComponents.AdvTree.Cell();
			//    DevComponents.DotNetBar.ButtonItem button = new DevComponents.DotNetBar.ButtonItem();
			//    button.Text = "Remove";
			//    button.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			//    button.Image = imageList1.Images[IMAGE_REMOVE];
			//    superTooltip1.SetSuperTooltip(button, new DevComponents.DotNetBar.SuperTooltipInfo("Remove", "", "Remove this entity from this inheritance hierarchy.", imageListLarge.Images[IMAGE_REMOVE], null, DevComponents.DotNetBar.eTooltipColor.Silver));
			//    cell.HostedItem = button;
			//    button.Click += new EventHandler(RemoveButton_Click);
			//    node.Cells.Add(cell);
			//}
			//advTree1.Nodes.Add(node);
		}

		//void RemoveButton_Click(object sender, EventArgs e)
		//{
		//    ColumnToDelete = advTree1.SelectedNode.Cells.IndexOf(advTree1.SelectedNode.SelectedCell);
		//    Entity entity = (Entity)advTree1.Columns[ColumnToDelete].Tag;

		//    if (MessageBox.Show(this, string.Format("Remove [{0}] from the inheritance hierarchy?", entity.Name), "Remove?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
		//    {
		//        if (EntityImpl.DetermineInheritanceTypeWithParent(entity) == EntityImpl.InheritanceType.TablePerClassHierarchy)
		//            entity.Parent.RemoveChild(entity);

		//        deleteColumnTimer.Start();
		//    }
		//}

		private void DeleteButton_Click(object sender, EventArgs e)
		{
			ColumnToDelete = advTree1.SelectedNode.Cells.IndexOf(advTree1.SelectedNode.SelectedCell);
			Entity entity = (Entity)advTree1.Columns[ColumnToDelete].Tag;

			if (MessageBox.Show(this, string.Format("Delete [{0}]?", entity.Name), "Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
			{
				entity.DeleteSelf();
				MappedEntities.Remove(entity);

				if (EntityDeleted != null)
					EntityDeleted(null, null);

				deleteColumnTimer.Start();
			}
		}

		private void deleteColumnTimer_Tick(object sender, EventArgs e)
		{
			deleteColumnTimer.Stop();
			advTree1.BeginUpdate();

			try
			{
				for (int i = 0; i < advTree1.Nodes.Count; i++)
				{
					if (advTree1.Nodes[i].Cells[ColumnToDelete].HostedControl != null)
						advTree1.Nodes[i].Cells[ColumnToDelete].HostedControl = null;

					advTree1.Nodes[i].Cells.RemoveAt(ColumnToDelete);
				}
				advTree1.Columns.RemoveAt(ColumnToDelete);
			}
			finally
			{
				advTree1.EndUpdate();
				advTree1.Refresh();
				ColumnToDelete = -1;
			}
		}

		void IsBaseCheckbox_CheckedChanged(object sender, DevComponents.DotNetBar.CheckBoxChangeEventArgs e)
		{
			if (BusyPopulating)
				return;

			advTree1.BeginUpdate();

			for (int i = 0; i < advTree1.SelectedNode.Cells.Count; i++)
			{
				if (advTree1.SelectedNode.Cells[i] != advTree1.SelectedNode.SelectedCell &&
					advTree1.SelectedNode.Cells[i].HostedItem is DevComponents.DotNetBar.CheckBoxItem)
				{
					DevComponents.DotNetBar.CheckBoxItem checkbox = (DevComponents.DotNetBar.CheckBoxItem)advTree1.SelectedNode.Cells[i].HostedItem;
					checkbox.Checked = false;
				}
			}
			int columnIndex = -1;

			for (int i = 0; i < advTree1.Columns.Count; i++)
			{
				if (advTree1.SelectedNode.Cells[i] == advTree1.SelectedNode.SelectedCell)
				{
					columnIndex = i;
					break;
				}
			}
			int counter = 1;

			if (advTree1.Columns[columnIndex].DisplayIndex != 0)
			{
				for (int i = 1; i < advTree1.Columns.Count; i++)
					if (i != columnIndex)
						advTree1.Columns[i].DisplayIndex = counter++;

				advTree1.Columns[columnIndex].DisplayIndex = 0;
			}
			advTree1.EndUpdate();
		}

		private void AddRow(ArchAngel.Providers.EntityModel.Model.DatabaseLayer.IColumn column)
		{
			DevComponents.AdvTree.Node node = new DevComponents.AdvTree.Node(column.Name);

			for (int i = 0; i < MappedEntities.Count; i++)
			{
				DevComponents.AdvTree.Cell cell = new DevComponents.AdvTree.Cell();
				cell.CheckBoxAlignment = DevComponents.AdvTree.eCellPartAlignment.Default;
				cell.CheckBoxVisible = true;
				cell.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.CheckBox;
				cell.CheckBoxThreeState = false;
				cell.Checked = MappedEntities[i].ConcreteProperties.Count(p => p.MappedColumn() == column) > 0;
				node.Cells.Add(cell);
			}
			advTree1.Nodes.Add(node);
		}

		private void buttonNewEntity_Click(object sender, EventArgs e)
		{
			AddNewEntity();
		}

		private void AddNewEntity()
		{
			Entity newEntity = new EntityImpl(textBoxNewEntityName.Text);
			newEntity.DiscriminatorValue = textBoxDiscriminatorValue.Text;
			MappedEntities.Add(newEntity);
			textBoxNewEntityName.Text = "";
			textBoxDiscriminatorValue.Text = "";
			AddEntityToGrid(newEntity);
			textBoxNewEntityName.Focus();
		}

		private void textBoxNewEntityName_TextChanged(object sender, EventArgs e)
		{
			buttonNewEntity.Enabled = !string.IsNullOrEmpty(textBoxNewEntityName.Text);
		}

		internal bool Save()
		{
			if (comboBoxDiscriminatorType.SelectedItem.ToString() == "Column" && comboBoxDiscriminator.SelectedIndex < 0)
			{
				MessageBox.Show(this, "Select a discriminator column.", "Missing data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			else if (comboBoxDiscriminatorType.SelectedItem.ToString() == "Formula" && string.IsNullOrWhiteSpace(textBoxFormula.Text))
			{
				MessageBox.Show(this, "Enter a formula.", "Missing data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			#region Check for entities with no columns selected
			List<string> entitiesWithNoColumns = new List<string>();

			for (int i = 1; i < advTree1.Columns.Count; i++)
			{
				bool hasSelectedColumns = false;

				for (int rowIndex = 3; rowIndex < advTree1.Nodes.Count; rowIndex++)
				{
					if (advTree1.Nodes[rowIndex].Cells[i].Checked)
					{
						hasSelectedColumns = true;
						break;
					}
				}
				if (!hasSelectedColumns)
					entitiesWithNoColumns.Add(((DevComponents.DotNetBar.Controls.TextBoxX)advTree1.Nodes[0].Cells[i].HostedControl).Text);
			}
			if (entitiesWithNoColumns.Count > 0)
			{
				string message = "Entites with no columns. Delete them or select some columns: " + Environment.NewLine + Environment.NewLine;

				foreach (var entityname in entitiesWithNoColumns)
					message += entityname + ", ";

				message = message.TrimEnd(' ', ',');

				MessageBox.Show(this, message, "Entities with no columns", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			#endregion

			#region Check that exactly one Base exists
			bool baseFound = false;
			int baseIndex = -1;

			for (int i = 1; i < advTree1.Columns.Count; i++)
			{
				if (((DevComponents.DotNetBar.CheckBoxItem)advTree1.Nodes[1].Cells[i].HostedItem).Checked)
				{
					baseFound = true;
					baseIndex = i;
					break;
				}
			}
			if (!baseFound)
			{
				MessageBox.Show(this, "Specify a base entity.", "Missing data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			#endregion

			EditModel.BusyPopulating = true;
			Entity baseEntity = null;
			List<Entity> childEntities = new List<Entity>();

			// Save all the entities
			for (int i = 1; i < advTree1.Columns.Count; i++)
			{
				Entity entity = (Entity)advTree1.Columns[i].Tag;

				if (entity.EntitySet == null) // This is a new entity
				{
					// Create a new entity
					List<ITable> tablesToProcess = new List<ITable>();
					tablesToProcess.Add(Table);
					EntityModel.Controller.MappingLayer.MappingProcessor proc = new EntityModel.Controller.MappingLayer.MappingProcessor(new EntityModel.Controller.MappingLayer.OneToOneEntityProcessor());
					List<Entity> newEntities = proc.CreateOneToOneMappingsFor(tablesToProcess, Table.Database.MappingSet);

					if (newEntities.Count != 1)
						throw new Exception("Only one entity should be created.");

					Entity newEntity = newEntities[0];
					newEntity.Name = ((DevComponents.DotNetBar.Controls.TextBoxX)advTree1.Nodes[0].Cells[i].HostedControl).Text;
					newEntity.DiscriminatorValue = ((DevComponents.DotNetBar.Controls.TextBoxX)advTree1.Nodes[2].Cells[i].HostedControl).Text;

					UpdateProperties(i, newEntity);
					// Delete the original entity
					entity.DeleteSelf();
					entity = newEntity;
				}
				else // This is an existing entity
				{
					entity.Name = ((DevComponents.DotNetBar.Controls.TextBoxX)advTree1.Nodes[0].Cells[i].HostedControl).Text;
					UpdateProperties(i, entity);
				}
				if (i == baseIndex)
					baseEntity = entity;
				else
					childEntities.Add(entity);
			}
			foreach (Entity child in childEntities)
			{
				child.Parent = baseEntity;
				baseEntity.AddChild(child);
			}
			if (comboBoxDiscriminatorType.SelectedItem.ToString() == "Column")
			{
				baseEntity.Discriminator.DiscriminatorType = Enums.DiscriminatorTypes.Column;
				baseEntity.Discriminator.ColumnName = comboBoxDiscriminator.SelectedItem.ToString();
			}
			else
			{
				baseEntity.Discriminator.DiscriminatorType = Enums.DiscriminatorTypes.Formula;
				baseEntity.Discriminator.Formula = textBoxFormula.Text;
			}
			EditModel.BusyPopulating = false;
			return true;
		}

		private void UpdateProperties(int i, Entity newEntity)
		{
			List<string> checkedColumnNames = new List<string>();

			for (int rowIndex = 3; rowIndex < advTree1.Nodes.Count; rowIndex++)
			{
				string columnName = advTree1.Nodes[rowIndex].Text;
				List<Property> mappedProperties = newEntity.Properties.Where(p => p.MappedColumn() != null && p.MappedColumn().Name == columnName).ToList();

				if (advTree1.Nodes[rowIndex].Cells[i].Checked)
				{
					if (mappedProperties.Count == 0)
					{
						IColumn column = Table.Columns.Single(c => c.Name == columnName);
						Property newProperty = ArchAngel.Providers.EntityModel.Controller.MappingLayer.OneToOneEntityProcessor.CreatePropertyFromColumn(column);
						newEntity.AddProperty(newProperty);
					}
				}
				else
					for (int propIndex = mappedProperties.Count - 1; propIndex >= 0; propIndex--)
						newEntity.RemoveProperty(mappedProperties[propIndex]);
			}
		}

		private void textBoxNewEntityName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				AddNewEntity();
			else if (e.KeyCode == Keys.Tab)
				textBoxDiscriminatorValue.Focus();
		}

		private void textBoxDiscriminatorValue_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				AddNewEntity();
			else if (e.KeyCode == Keys.Tab)
				buttonNewEntity.Focus();
		}

		private void comboBoxDiscriminatorType_SelectedIndexChanged(object sender, EventArgs e)
		{
			LayoutDiscriminatorControls();
		}

		private void LayoutDiscriminatorControls()
		{
			if (comboBoxDiscriminatorType.SelectedItem.ToString() == "Formula")
			{
				textBoxFormula.Left = comboBoxDiscriminatorType.Right + 10;
				textBoxFormula.Width = groupPanelDiscriminator.Width - textBoxFormula.Left - 10;
				comboBoxDiscriminator.Visible = false;
				textBoxFormula.Visible = true;
				textBoxFormula.BringToFront();
			}
			else
			{
				comboBoxDiscriminator.Left = comboBoxDiscriminatorType.Right + 10;
				comboBoxDiscriminator.Width = groupPanelDiscriminator.Width - comboBoxDiscriminator.Left - 10;
				textBoxFormula.Visible = false;
				comboBoxDiscriminator.Visible = true;
				comboBoxDiscriminator.BringToFront();
			}
		}
	}
}
