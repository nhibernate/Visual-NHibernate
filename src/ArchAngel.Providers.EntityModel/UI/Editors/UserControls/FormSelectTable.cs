using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using Slyce.Common;

namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
	public partial class FormSelectTable : Form
	{
		private bool BaseIsEntity = true;
		private Entity Entity;
		private Table Table;
		private List<ITable> UnavailableTables;
		public ITable SelectedTable = null;
		public ITable TempSelectedTable = null;

		public FormSelectTable(Entity entity, List<ITable> unavailableTables, ITable selectedTable, string title)
		{
			InitializeComponent();

			BaseIsEntity = true;
			Entity = entity;
			UnavailableTables = unavailableTables;
			Text = title;
			SelectedTable = selectedTable;
			TempSelectedTable = SelectedTable;
			Populate();
		}

		public FormSelectTable(Table table, List<ITable> unavailableTables, ITable selectedTable, string title)
		{
			InitializeComponent();

			BaseIsEntity = false;
			Table = table;
			UnavailableTables = unavailableTables;
			Text = title;
			SelectedTable = selectedTable;
			TempSelectedTable = SelectedTable;
			Populate();
		}

		private void Populate()
		{
			listViewColumns.Items.Clear();
			listBox1.Items.Clear();
			listBox1.DisplayMember = "Name";
			listBox1.Sorted = true;

			if (BaseIsEntity)
			{
				labelDescription.Text = "Only tables that are mapped 1:1 or m:1 to currently mapped tables are available.";
				//List<ITable> tables = Entity.MappedTables().ToList();
				List<ITable> mappedtables = Entity.MappedTables().ToList();

				if (mappedtables.Count == 0)
				{
					foreach (ITable table in Entity.EntitySet.MappingSet.Database.Tables)
					{
						if (!UnavailableTables.Contains(table))
							listBox1.Items.Add(table);
					}
					foreach (ITable view in Entity.EntitySet.MappingSet.Database.Views)
					{
						if (!UnavailableTables.Contains(view))
							listBox1.Items.Add(view);
					}
				}
				else
				{
					foreach (ITable table in mappedtables)
					{
						foreach (var relationship in table.Relationships)
						{
							if (relationship.PrimaryTable == table &&
								ArchAngel.Interfaces.Cardinality.One.Equals(relationship.PrimaryCardinality))
							{
								if (!listBox1.Items.Contains(relationship.ForeignTable) &&
									!mappedtables.Contains(relationship.ForeignTable))
									listBox1.Items.Add(relationship.ForeignTable);
							}
							else if (relationship.ForeignTable == table &&
								ArchAngel.Interfaces.Cardinality.One.Equals(relationship.ForeignCardinality))
							{
								if (!listBox1.Items.Contains(relationship.PrimaryTable) &&
									!mappedtables.Contains(relationship.PrimaryTable))
									listBox1.Items.Add(relationship.PrimaryTable);
							}
						}
					}
				}
			}
			else
			{
				foreach (ITable table in Table.Database.Tables)
				{
					if (!UnavailableTables.Contains(table))
						listBox1.Items.Add(table);
				}
			}
			if (TempSelectedTable != null)
				listBox1.SelectedItem = TempSelectedTable;
		}

		private void buttonOk_Click(object sender, EventArgs e)
		{
			if (listBox1.SelectedItem != null)
			{
				SelectedTable = (ITable)listBox1.SelectedItem;
				HashSet<ITable> tables = new HashSet<ITable>();

				foreach (ListViewItem item in listViewColumns.CheckedItems)
					tables.Add(((IColumn)item.Tag).Parent);

				StringBuilder sb = new StringBuilder(100);

				for (int i = 0; i < tables.Count; i++)
				{
					sb.Append(tables.ElementAt(i).Name);

					if (i < tables.Count - 1)
						sb.Append(", ");
				}
				if (MessageBox.Show(this, string.Format("Delete all entities currently mapped to this tables [{0}]?", sb), "Delete mapped entities?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
					foreach (var table in tables)
					{
						var entities = table.MappedEntities().ToList();

						for (int i = entities.Count - 1; i >= 0; i--)
						{
							var entity = entities[i];
							entity.DeleteSelf();
						}
					}
				foreach (ListViewItem item in listViewColumns.CheckedItems)
				{
					IColumn column = (IColumn)item.Tag;
					Property newProperty = Controller.MappingLayer.OneToOneEntityProcessor.CreatePropertyFromColumn(column);
					newProperty.Name = newProperty.Name.GetNextName(Entity.Properties.Select(p => p.Name));
					Entity.AddProperty(newProperty);
					newProperty.SetMappedColumn(column);
				}
				//if (checkBoxAddReferences.Checked)
				//{
				//    var mappedEntities = SelectedTable.MappedEntities();

				//    if (mappedEntities.Count() == 1)
				//    {
				//        if (mappedEntities.ElementAt(0).MappedTables().Count() == 1)
				//    }
				//}
			}
			Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listBox1.SelectedItem != null)
			{
				buttonOk.Enabled = true;
				TempSelectedTable = (ITable)listBox1.SelectedItem;
				PopulateColumns();

				//var mappedEntities = SelectedTable.MappedEntities();

				//if (mappedEntities.Count() == 1 &&
				//    mappedEntities.ElementAt(0).MappedTables().Count() == 1)
				//{
				//    checkBoxAddReferences.Checked = true;
				//    checkBoxAddReferences.Enabled = true;
				//    checkBoxDeleteMappedEntity.Checked = true;
				//    checkBoxDeleteMappedEntity.Enabled = true;
				//}
				//else
				//{
				//    checkBoxAddReferences.Checked = false;
				//    checkBoxAddReferences.Enabled = false;
				//    checkBoxDeleteMappedEntity.Checked = false;
				//    checkBoxDeleteMappedEntity.Enabled = false;
				//}
			}
		}

		private void PopulateColumns()
		{
			listViewColumns.Items.Clear();

			foreach (var column in TempSelectedTable.Columns)
			{
				ListViewItem item = new ListViewItem(column.Name);
				item.Tag = column;
				item.Checked = column.InPrimaryKey ? false : true;
				string primaryKeyText = column.InPrimaryKey ? "Yes" : "";
				item.SubItems.Add(column.OriginalDataType.ToString());
				item.SubItems.Add(primaryKeyText);
				listViewColumns.Items.Add(item);
			}
		}
	}
}
