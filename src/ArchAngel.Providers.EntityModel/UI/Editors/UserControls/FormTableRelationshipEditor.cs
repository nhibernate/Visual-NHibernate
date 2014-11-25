using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Controller.MappingLayer;
using ArchAngel.Providers.EntityModel.Helper;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using DevComponents.DotNetBar.Controls;
using Slyce.Common;

namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
	public partial class FormTableRelationshipEditor : UserControl
	{
		internal event EventHandler RelationshipAdded;
		private static Image RemoveImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ArchAngel.Providers.EntityModel.UI.Editors.Resources.Exit2.png"));

		private RelationshipImpl Relationship = null;
		private Table MainTable = null;
		private Table RelatedTable = null;
		private List<ITable> UnavailableTables;
		private int Gap = 10;
		private List<ComboBoxEx> MainColumnCombos = new List<ComboBoxEx>();
		private List<ComboBoxEx> RelatedColumnCombos = new List<ComboBoxEx>();
		private List<PictureBox> RemovePics = new List<PictureBox>();

		public FormTableRelationshipEditor()
		{
			InitializeComponent();

			SetStyle(
		   ControlStyles.UserPaint |
		   ControlStyles.AllPaintingInWmPaint |
		   ControlStyles.OptimizedDoubleBuffer, true);
		}

		internal void Fill(RelationshipImpl relationship, Table primaryTable, Table foreignTable, List<ITable> unavailableTables)
		{
			Relationship = relationship;
			MainTable = primaryTable;
			RelatedTable = foreignTable;
			UnavailableTables = unavailableTables;
			comboBoxForeignTable.Items.Clear();
			labelSelectForeignTable.Text = string.Format("Create a relationship between {0} and: ", MainTable.Name);

			Graphics g = Graphics.FromHwnd(labelSelectForeignTable.Handle);
			//comboBoxForeignTable.Left = labelSelectForeignTable.Left + (int)g.MeasureString(labelSelectForeignTable.Text, labelSelectForeignTable.Font).Width + 10;
			//comboBoxForeignTable.Left = labelSelectForeignTable.Right;
			this.Width = Math.Max(this.Width, comboBoxForeignTable.Right + Gap * 3);
			labelTableName.Text = string.Format("[Table] {0}", MainTable.Name);

			foreach (var combo in MainColumnCombos.Union(RelatedColumnCombos))
				this.Controls.Remove(combo);

			foreach (var pic in RemovePics)
			{
				pic.Click -= pic_Click;
				this.Controls.Remove(pic);
			}
			MainColumnCombos.Clear();
			RelatedColumnCombos.Clear();
			RemovePics.Clear();
			Populate();
		}

		private class DisplayItem
		{
			public DisplayItem(string text, object value)
			{
				Text = text;
				Value = value;
			}
			public string Text { get; set; }
			public object Value { get; set; }

			public override string ToString()
			{
				return Text;
			}
		}

		private void Populate()
		{
			List<DisplayItem> primaryColumnItems = new List<DisplayItem>();
			List<DisplayItem> foreignColumnItems = new List<DisplayItem>();

			foreach (var obj in MainTable.Columns)
				primaryColumnItems.Add(new DisplayItem(string.Format("{0} [{1}]", obj.Name, obj.OriginalDataType), obj));

			if (RelatedTable != null)
			{
				foreach (var obj in RelatedTable.Columns)
					foreignColumnItems.Add(new DisplayItem(string.Format("{0} [{1}]", obj.Name, obj.OriginalDataType), obj));
			}
			int top = 0;

			if (Relationship != null)
			{
				labelTableName.Text = string.Format("[Table] {0}", MainTable.Name);
				labelForeignTable.Text = string.Format("[Table] {0}", RelatedTable.Name);
				top = labelTableName.Bottom + 5;

				for (int i = 0; i < Relationship.PrimaryKey.Columns.Count; i++)
				{
					ComboBoxEx combo = new ComboBoxEx();
					combo.DisplayMember = "Text";
					combo.Items.Add(primaryColumnItems);
					//combo.SelectedItem = Relationship.PrimaryKey.Columns[i];
					combo.Left = Gap;
					combo.Top = top;
					this.Controls.Add(combo);

					ComboBoxEx comboForeign = new ComboBoxEx();
					comboForeign.DisplayMember = "Text";
					comboForeign.Items.Add(foreignColumnItems);
					//comboForeign.SelectedItem = Relationship.ForeignKey.Columns[i];
					comboForeign.Left = Gap;
					comboForeign.Top = top;
					this.Controls.Add(comboForeign);

					top = combo.Bottom + 5;
				}
				SetControlVisibility(true);
			}
			else
			{
				comboBoxForeignTable.Sorted = false;
				comboBoxForeignTable.Items.Clear();
				comboBoxForeignTable.DisplayMember = "Text";
				top = comboBoxForeignTable.Bottom + 5;

				foreach (ITable table in MainTable.Database.Tables)
				{
					if (!UnavailableTables.Contains(table))
						comboBoxForeignTable.Items.Add(table);
				}
				comboBoxForeignTable.Sorted = true;
				top = comboBoxForeignTable.Bottom + 5;
				SetControlVisibility(false);
			}
			LayoutTheControls();
		}

		private void SetControlVisibility(bool foreignTableIsSelected)
		{
			if (foreignTableIsSelected)
			{
				labelSelectForeignTable.Visible = false;
				comboBoxForeignTable.Visible = false;
				labelTableName.Visible = true;
				labelForeignTable.Visible = true;
				buttonAddColumn.Visible = true;
				checkBoxForeignUnique.Visible = true;
				checkBoxPrimaryUnique.Visible = true;
				buttonOk.Enabled = true;
			}
			else
			{
				labelSelectForeignTable.Visible = true;
				comboBoxForeignTable.Visible = true;
				labelTableName.Visible = false;
				labelForeignTable.Visible = false;
				buttonAddColumn.Visible = false;
				checkBoxForeignUnique.Visible = false;
				checkBoxPrimaryUnique.Visible = false;
				buttonOk.Enabled = false;
			}
		}

		private void buttonOk_Click(object sender, EventArgs e)
		{
			Save();
		}

		private void Save()
		{
			if (RelatedTable == null)
			{
				MessageBox.Show(this, "You need to select a foreign table.", "Foreign table missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (RelatedColumnCombos.Count == 0)
			{
				MessageBox.Show(this, "No columns selected.", "Columns missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			bool mainColsAreUnique;// = checkBoxPrimaryUnique.Checked ? true : ColumnsAreUnique(GetMainColumns());
			bool relatedColsAreUnique;// = checkBoxForeignUnique.Checked ? true : ColumnsAreUnique(GetRelatedColumns());

			// Check if the Main columns are unique
			if (checkBoxPrimaryUnique.Checked)
				mainColsAreUnique = true;
			else
			{
				List<Column> mainColumns = GetMainColumns();
				bool selectedColsContainAllPrimaryCols = MainTable.ColumnsInPrimaryKey.Count() > 0;

				foreach (var col in MainTable.ColumnsInPrimaryKey)
				{
					if (!mainColumns.Contains(col))
					{
						selectedColsContainAllPrimaryCols = false;
						break;
					}
				}
				if (selectedColsContainAllPrimaryCols)
					mainColsAreUnique = true;
				else
					mainColsAreUnique = ColumnsAreUnique(mainColumns);
			}
			// Check if the Realted columns are unique
			if (checkBoxForeignUnique.Checked)
				relatedColsAreUnique = true;
			else
			{
				List<Column> relatedColumns = GetRelatedColumns();
				bool selectedColsContainAllPrimaryCols = RelatedTable.ColumnsInPrimaryKey.Count() > 0;

				foreach (var col in RelatedTable.ColumnsInPrimaryKey)
				{
					if (!relatedColumns.Contains(col))
					{
						selectedColsContainAllPrimaryCols = false;
						break;
					}
				}
				if (selectedColsContainAllPrimaryCols)
					relatedColsAreUnique = true;
				else
					relatedColsAreUnique = ColumnsAreUnique(relatedColumns);
			}

			IKey mainTableKey = null;

			foreach (var key in MainTable.Keys)
			{
				if (MainColumnCombos.Count == key.Columns.Count)
				{
					bool keyMatches = true;

					foreach (var combo in MainColumnCombos)
					{
						if (!key.Columns.Contains((Column)((DisplayItem)combo.SelectedItem).Value))
						{
							keyMatches = false;
							break;
						}
					}
					if (keyMatches)
					{
						// Key is found
						mainTableKey = key;
						break;
					}
				}
			}
			IKey relatedTableKey = null;

			foreach (var key in RelatedTable.Keys)
			{
				if (RelatedColumnCombos.Count == key.Columns.Count)
				{
					bool keyMatches = true;

					foreach (var combo in RelatedColumnCombos)
					{
						// Can't have two primary keys, but there might be a foreign key on the same columns, so skip for now. Otherwise we'll need to create a new foreign key anyway.
						if (!key.Columns.Contains((Column)((DisplayItem)combo.SelectedItem).Value) &&
							!((mainTableKey != null && mainTableKey.Keytype == DatabaseKeyType.Primary) && key.Keytype == DatabaseKeyType.Primary))
						{
							keyMatches = false;
							break;
						}
					}
					if (keyMatches)
					{
						// Key is found
						relatedTableKey = key;
						break;
					}
				}
			}
			if (mainTableKey != null &&
				relatedTableKey != null)
			{
				// Both keys exist, so let's check whether this relationship already exists
				foreach (var relationship in MainTable.Relationships)
				{
					if ((relationship.PrimaryKey == mainTableKey &&
						relationship.ForeignKey == relatedTableKey) ||
						(relationship.PrimaryKey == relatedTableKey &&
						relationship.ForeignKey == mainTableKey))
					{
						MessageBox.Show(this, "This relationship already exists.", "Relationship exists", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
				}
			}
			if (mainTableKey != null &&
				relatedTableKey != null &&
				(mainTableKey.Keytype == DatabaseKeyType.Primary &&
				relatedTableKey.Keytype == DatabaseKeyType.Primary))
			{
				// A relationship can't have two primary keys, so we'll need to create a new 'foreign-key'
				relatedTableKey = null;
			}
			RelatedTable = (Table)comboBoxForeignTable.SelectedItem;

			if (mainTableKey == null)
			{
				mainTableKey = new Key
					{
						Name = string.Format("FK_{0}_{1}", MainTable.Name, RelatedTable.Name).GetNextName(MainTable.Relationships.Select(r => r.Name).ToList()),
						IsUserDefined = true,
						Keytype = DatabaseKeyType.Foreign,
						Parent = MainTable,
						IsUnique = mainColsAreUnique
					};
				foreach (Column column in GetMainColumns())
					mainTableKey.AddColumn(column.Name);
			}
			if (relatedTableKey == null)
			{
				relatedTableKey = new Key
				{
					Name = string.Format("FK_{0}_{1}", RelatedTable.Name, MainTable.Name).GetNextName(RelatedTable.Relationships.Select(r => r.Name).ToList()),
					IsUserDefined = true,
					Keytype = DatabaseKeyType.Foreign,
					Parent = RelatedTable,
					IsUnique = relatedColsAreUnique
				};
				foreach (Column column in GetRelatedColumns())
					relatedTableKey.AddColumn(column.Name);
			}
			mainTableKey.ReferencedKey = relatedTableKey;
			relatedTableKey.ReferencedKey = mainTableKey;

			if (Relationship == null)
			{
				Relationship newRelationship = new RelationshipImpl();
				newRelationship.Database = MainTable.Database;
				newRelationship.IsUserDefined = true;
				newRelationship.Schema = MainTable.Schema;

				if (relatedTableKey.Keytype == DatabaseKeyType.Primary)
				{
					newRelationship.AddThisTo(RelatedTable, MainTable);
					newRelationship.Name = string.Format("FK_NH_{0}_{1}", RelatedTable, MainTable).GetNextName(RelatedTable.Relationships.Select(r => r.Name).ToList());
					//newRelationship.PrimaryCardinality = mainColsAreUnique ? ArchAngel.Interfaces.Cardinality.One : ArchAngel.Interfaces.Cardinality.Many;
					//newRelationship.ForeignCardinality = relatedColsAreUnique ? ArchAngel.Interfaces.Cardinality.One : ArchAngel.Interfaces.Cardinality.Many;
					newRelationship.ForeignCardinality = mainColsAreUnique ? ArchAngel.Interfaces.Cardinality.One : ArchAngel.Interfaces.Cardinality.Many;
					newRelationship.PrimaryCardinality = relatedColsAreUnique ? ArchAngel.Interfaces.Cardinality.One : ArchAngel.Interfaces.Cardinality.Many;

					newRelationship.PrimaryKey = relatedTableKey;
					newRelationship.ForeignKey = mainTableKey;

					if (!newRelationship.PrimaryTable.Keys.Contains(relatedTableKey))
						newRelationship.PrimaryTable.AddKey(relatedTableKey);

					if (!newRelationship.ForeignTable.Keys.Contains(mainTableKey))
						newRelationship.ForeignTable.AddKey(mainTableKey);
				}
				else
				{
					newRelationship.AddThisTo(MainTable, RelatedTable);
					newRelationship.Name = string.Format("FK_NH_{0}_{1}", MainTable, RelatedTable);
					//newRelationship.PrimaryCardinality = mainColsAreUnique ? ArchAngel.Interfaces.Cardinality.One : ArchAngel.Interfaces.Cardinality.Many;
					//newRelationship.ForeignCardinality = relatedColsAreUnique ? ArchAngel.Interfaces.Cardinality.One : ArchAngel.Interfaces.Cardinality.Many;
					newRelationship.ForeignCardinality = mainColsAreUnique ? ArchAngel.Interfaces.Cardinality.One : ArchAngel.Interfaces.Cardinality.Many;
					newRelationship.PrimaryCardinality = relatedColsAreUnique ? ArchAngel.Interfaces.Cardinality.One : ArchAngel.Interfaces.Cardinality.Many;

					newRelationship.PrimaryKey = mainTableKey;
					newRelationship.ForeignKey = relatedTableKey;

					if (!newRelationship.PrimaryTable.Keys.Contains(mainTableKey))
						newRelationship.PrimaryTable.AddKey(mainTableKey);

					if (!newRelationship.ForeignTable.Keys.Contains(relatedTableKey))
						newRelationship.ForeignTable.AddKey(relatedTableKey);
				}
				//MainTable.Database.AddRelationship(newRelationship);
				OneToOneEntityProcessor proc = new OneToOneEntityProcessor(MainTable.Database.MappingSet.EntitySet.Entities.Select(e => e.Name));
				//List<ITable> tables = new List<ITable>();
				//tables.Add((ITable)MainTable);
				//proc.CreateOneToOneMappingsFor(tables, MainTable.Database.MappingSet);
				//proc.CreateReference(newRelationship, MainTable.Database.MappingSet.EntitySet);

				MappingProcessor.ProcessRelationshipInternal(MainTable.Database.MappingSet, newRelationship, proc);

				if (RelationshipAdded != null)
					RelationshipAdded(null, null);
			}
			this.Visible = false;
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			this.Visible = false;
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
			this.Visible = false;
		}

		private void comboBoxForeignTable_SelectedIndexChanged(object sender, EventArgs e)
		{
			RelatedTable = (Table)comboBoxForeignTable.SelectedItem;
			labelForeignTable.Text = string.Format("[Table] {0}", RelatedTable.Name);
			SetControlVisibility(true);
			AddNewColumnRow();

			SetCheckboxForForeignTableColumns();
			SetCheckboxForPrimaryTableColumns();
		}

		private void AddNewColumnRow()
		{
			List<DisplayItem> primaryColumnItems = new List<DisplayItem>();
			List<DisplayItem> foreignColumnItems = new List<DisplayItem>();

			foreach (var obj in MainTable.Columns)
				primaryColumnItems.Add(new DisplayItem(string.Format("{0} [{1}]", obj.Name, obj.OriginalDataType), obj));

			foreach (var obj in RelatedTable.Columns)
				foreignColumnItems.Add(new DisplayItem(string.Format("{0} [{1}]", obj.Name, obj.OriginalDataType), obj));

			ComboBoxEx combo = new ComboBoxEx();
			combo.DisplayMember = "Name";
			combo.DataSource = primaryColumnItems;
			combo.DropDownStyle = ComboBoxStyle.DropDownList;
			combo.SelectedIndexChanged += new EventHandler(PrimaryCombo_SelectedIndexChanged);
			this.Controls.Add(combo);
			MainColumnCombos.Add(combo);

			ComboBoxEx comboForeign = new ComboBoxEx();
			comboForeign.DisplayMember = "Name";
			comboForeign.DataSource = foreignColumnItems;
			comboForeign.DropDownStyle = ComboBoxStyle.DropDownList;
			comboForeign.SelectedIndexChanged += new EventHandler(ForeignCombo_SelectedIndexChanged);
			this.Controls.Add(comboForeign);
			RelatedColumnCombos.Add(comboForeign);

			PictureBox pic = new PictureBox();
			pic.Image = RemoveImage;
			pic.BackColor = Color.Transparent;
			pic.SizeMode = PictureBoxSizeMode.AutoSize;
			this.Controls.Add(pic);
			RemovePics.Add(pic);
			pic.Click += new EventHandler(pic_Click);

			LayoutTheControls();
		}

		void PrimaryCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetCheckboxForPrimaryTableColumns();
		}

		private List<Column> GetMainColumns()
		{
			List<Column> columns = new List<Column>();

			foreach (ComboBoxEx combo in MainColumnCombos)
				columns.Add((Column)((DisplayItem)combo.SelectedItem).Value);

			return columns;
		}

		private List<Column> GetRelatedColumns()
		{
			List<Column> columns = new List<Column>();

			foreach (ComboBoxEx combo in RelatedColumnCombos)
				columns.Add((Column)((DisplayItem)combo.SelectedItem).Value);

			return columns;
		}

		private void SetCheckboxForPrimaryTableColumns()
		{
			checkBoxPrimaryUnique.Checked = ColumnsAreUnique(GetMainColumns());
		}

		private void SetCheckboxForForeignTableColumns()
		{
			checkBoxForeignUnique.Checked = ColumnsAreUnique(GetRelatedColumns());
		}

		void ForeignCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetCheckboxForForeignTableColumns();
		}

		private bool ColumnsAreUnique(List<Column> columns)
		{
			foreach (var column in columns)
			{
				// The collection of columns is unique if even just one column is unique.
				if (column.IsUnique)
					return true;
			}
			if (columns.Count > 0)
			{
				ITable table = columns[0].Parent;

				// Check whether a unique key's columns are in the column collection.
				foreach (var key in table.Keys)
				{
					if (key.IsUnique || key.Keytype == DatabaseKeyType.Primary)
					{
						bool allColumnsAreSelected = key.Columns.Count > 0;

						foreach (Column col in key.Columns)
						{
							if (!columns.Contains(col))
							{
								allColumnsAreSelected = false;
								break;
							}
						}
						if (allColumnsAreSelected)
							return true;
					}
				}
			}
			return false;
		}

		private void LayoutTheControls()
		{
			int comboWidth = 170;
			comboBoxForeignTable.Width = comboWidth;
			//comboBoxForeignTable.Left = Gap + comboWidth + 30;

			if (RelatedTable == null)
				comboBoxForeignTable.Left = labelSelectForeignTable.Right;
			else
				comboBoxForeignTable.Left = Gap + comboWidth + 30;

			labelForeignTable.Left = comboBoxForeignTable.Left;
			labelTableName.Left = Gap;
			int top = RelatedTable == null ? comboBoxForeignTable.Bottom : labelForeignTable.Bottom;
			top += 5;

			foreach (var combo in MainColumnCombos)
			{
				combo.Left = Gap;
				combo.Top = top;
				combo.Width = comboWidth;
				top = combo.Bottom + 5;
			}
			top = RelatedTable == null ? comboBoxForeignTable.Bottom : labelForeignTable.Bottom;
			top += 5;

			for (int i = 0; i < RelatedColumnCombos.Count; i++)
			{
				var combo = RelatedColumnCombos[i];
				combo.Left = Gap + comboWidth + 30;
				combo.Top = top;
				combo.Width = comboWidth;

				var pic = RemovePics[i];
				pic.Left = combo.Right + 2;
				pic.Top = top;

				top = combo.Bottom + 5;
			}
			top += 5;
			checkBoxPrimaryUnique.Top = top;
			checkBoxPrimaryUnique.Left = Gap + (comboWidth - checkBoxPrimaryUnique.Width) / 2;// labelTableName.Left;
			checkBoxForeignUnique.Top = top;
			checkBoxForeignUnique.Left = labelForeignTable.Left + (comboWidth - checkBoxForeignUnique.Width) / 2;

			top = checkBoxPrimaryUnique.Bottom + 5;
			buttonAddColumn.Enabled = RelatedTable != null;
			buttonAddColumn.Top = top;

			this.Height = buttonAddColumn.Bottom + 15 + buttonOk.Height + Gap;

			if (RelatedTable == null)
				this.Width = comboBoxForeignTable.Right + Gap * 2;
			else
				this.Width = Gap * 2 + comboWidth * 2 + 30 + 20;

			buttonAddColumn.Left = (this.ClientSize.Width - buttonAddColumn.Width) / 2;
			this.Refresh();
		}

		void pic_Click(object sender, EventArgs e)
		{
			int index = RemovePics.IndexOf((PictureBox)sender);

			RemovePics[index].Click -= pic_Click;
			this.Controls.Remove(MainColumnCombos[index]);
			this.Controls.Remove(RelatedColumnCombos[index]);
			this.Controls.Remove(RemovePics[index]);

			MainColumnCombos.RemoveAt(index);
			RelatedColumnCombos.RemoveAt(index);
			RemovePics.RemoveAt(index);

			LayoutTheControls();
		}

		private void buttonAddColumn_Click(object sender, EventArgs e)
		{
			AddNewColumnRow();
		}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
			//base.OnPaintBackground(e);
			System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(this.ClientRectangle, Color.FromArgb(10, 10, 10), Color.FromArgb(100, 100, 100), 90);

			e.Graphics.FillRectangle(brush, this.ClientRectangle);
			e.Graphics.DrawRectangle(new Pen(Color.White), 0, 0, this.ClientSize.Width - 1, this.ClientSize.Height - 1);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			SolidBrush brush = new SolidBrush(Color.White);

			for (int i = 0; i < MainColumnCombos.Count; i++)
			{
				Point pt = new Point(MainColumnCombos[i].Right + (RelatedColumnCombos[i].Left - MainColumnCombos[i].Right - 10) / 2, MainColumnCombos[i].Top + 2);
				e.Graphics.DrawString("=", this.Font, brush, pt);
			}
		}

	}
}
