using System;
using System.Drawing;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using Slyce.Common.Controls.Diagramming.SlyceGrid;

namespace ArchAngel.Providers.EntityModel.UI.Editors
{
	public partial class TableKeysEditor : UserControl
	{
		public delegate void KeysChangedDelegate();
		public event KeysChangedDelegate KeysChanged;

		private Table _Table;

		public TableKeysEditor()
		{
			InitializeComponent();

			slyceGrid1.DeleteClicked += new SlyceGrid.DeleteClickedDelegate(slyceGrid1_DeleteClicked);
			slyceGrid1.CellValueChanged += new SlyceGrid.CellValueChangedDelegate(slyceGrid1_CellValueChanged);
			slyceGrid1.NewRowAdded += new SlyceGrid.NewRowAddedDelegate(slyceGrid1_NewRowAdded);

			slyceGrid1.InvalidColor = Color.FromArgb(100, 100, 100);
			slyceGrid1.DisabledColor = Color.FromArgb(25, 25, 25);
			slyceGrid1.BackColor = Color.Black;//.FromArgb(13, 13, 13);
		}

		internal void FinaliseEdits()
		{
			slyceGrid1.FinaliseEdits();
		}

		public Table Table
		{
			get { return _Table; }
			set
			{
				_Table = value;

				if (_Table != null)
				{
					Populate();
				}
			}
		}

		void slyceGrid1_NewRowAdded(out object newObject)
		{
			Key key = new Key("NewKey", ArchAngel.Providers.EntityModel.Helper.DatabaseKeyType.Unique);
			Table.AddKey(key);
			newObject = key;

			AddKeyToKeysGrid(key);

			RaiseKeysChangedEvent();
		}

		private void RaiseKeysChangedEvent()
		{
			if (KeysChanged != null)
				KeysChanged();
		}

		private void Populate()
		{
			Slyce.Common.Utility.SuspendPainting(slyceGrid1);
			slyceGrid1.Clear();
			// Populate Columns from Entity
			slyceGrid1.Columns.Add(new ColumnItem("Name", ColumnItem.ColumnTypes.Textbox, "NewProp", "General"));

			ColumnItem keyTypeColumn = new ColumnItem("Type", ColumnItem.ColumnTypes.ComboBox, "", "General");

			foreach (string name in Enum.GetNames(typeof(Helper.DatabaseKeyType)))
				keyTypeColumn.ComboItems.Add(name, name);

			slyceGrid1.Columns.Add(keyTypeColumn);
			slyceGrid1.Columns.Add(new ColumnItem("Description", ColumnItem.ColumnTypes.Textbox, "", "General"));

			foreach (Key key in Table.Keys)
				AddKeyToKeysGrid(key);

			slyceGrid1.Populate();
			Slyce.Common.Utility.ResumePainting(slyceGrid1);
		}

		private void AddKeyToKeysGrid(Key key)
		{
			SlyceTreeGridItem gridItem = new SlyceTreeGridItem();
			gridItem.Tag = key;
			gridItem.SubItems.Add(new SlyceTreeGridCellItem(key.Name));
			gridItem.SubItems.Add(new SlyceTreeGridCellItem(key.Keytype.ToString()));
			gridItem.SubItems.Add(new SlyceTreeGridCellItem(key.Description));
			slyceGrid1.Items.Add(gridItem);
		}

		private bool slyceGrid1_DeleteClicked(int row, object tag)
		{
			if (MessageBox.Show(this, string.Format("Delete {0}?", ((Key)tag).Name), "Delete Key", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				Table.RemoveKey((Key)tag);
				RaiseKeysChangedEvent();
				return true;
			}
			return false;
		}

		void slyceGrid1_CellValueChanged(int row, int cell, int columnIndex, string columnHeader, ref object tag, object newValue)
		{
			if (cell == 0) // delete image column
				return;

			Key key = (Key)tag;

			if (key == null)
				return;

			bool nullableColumn = string.IsNullOrEmpty(columnHeader);

			if (nullableColumn)
				columnHeader = slyceGrid1.Columns[columnIndex].Text;

			switch (columnHeader)
			{
				case "Name":
					var originalName = key.Name;
					key.Name = (string)newValue;

					if (originalName != key.Name)
						ArchAngel.Interfaces.Events.RaiseIsDirtyEvent();
					break;
				case "Type":
					var originalKeyType = key.Keytype;
					key.Keytype = (Helper.DatabaseKeyType)Enum.Parse(typeof(Helper.DatabaseKeyType), (string)newValue, true);

					if (originalKeyType != key.Keytype)
						ArchAngel.Interfaces.Events.RaiseIsDirtyEvent();
					break;
				case "Description":
					var originalDesc = key.Description;
					key.Description = (string)newValue;

					if (originalDesc != key.Description)
						ArchAngel.Interfaces.Events.RaiseIsDirtyEvent();
					break;
				default:
					throw new NotImplementedException("Column header not handled yet: " + columnHeader);
			}
			RaiseKeysChangedEvent();
		}

	}
}
