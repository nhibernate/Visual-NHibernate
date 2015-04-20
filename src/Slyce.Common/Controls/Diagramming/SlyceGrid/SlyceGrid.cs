using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;

namespace Slyce.Common.Controls.Diagramming.SlyceGrid
{
	public partial class SlyceGrid : UserControl
	{
		public delegate void NewRowAddedDelegate(out object newObject);
		public delegate bool DeleteClickedDelegate(int row, object tag);
		public delegate void CellValueChangedDelegate(int row, int cell, int column, string columnHeader, ref object tag, object newValue);
		public event DeleteClickedDelegate DeleteClicked;
		public event CellValueChangedDelegate CellValueChanged;
		public event NewRowAddedDelegate NewRowAdded;

		private List<ColumnItem> _Columns = new List<ColumnItem>();
		private List<SlyceTreeGridItem> _Items = new List<SlyceTreeGridItem>();
		private List<LabelX> Labels = new List<LabelX>();
		private List<LabelX> CategoryLabels = new List<LabelX>();
		private bool BusyPopulatingColumns = false;
		internal static Image BlankImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Slyce.Common.Controls.Diagramming.SlyceGrid.Resources.plus_16.png"));
		internal static Image DeleteImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Slyce.Common.Controls.Diagramming.SlyceGrid.Resources.delete_x_16.png"));
		internal static Image NoEntryImage = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Slyce.Common.Controls.Diagramming.SlyceGrid.Resources.delete16.png"));
		public bool ShowDeleteColumn = true;
		private List<object> Tags = new List<object>();
		private bool BusyPopulating = false;
		private bool BusyClearing = false;
		private Dictionary<int, int> EditColumns = new Dictionary<int, int>();
		private Color _BackColor = Color.Black;
		public Color AlternatingBackColor = Color.Black;
		public Color NullColor = Color.LightGray;
		private Color _InvalidColor = Color.Orange;
		private Color _DisabledColor = Color.Gray;
		private int? _FrozenColumnIndex = null;

		public SlyceGrid()
		{
			InitializeComponent();

			SetStyle(
			ControlStyles.UserPaint |
			ControlStyles.AllPaintingInWmPaint |
			ControlStyles.OptimizedDoubleBuffer, true);

			dataGridViewX1.EnableHeadersVisualStyles = false;
			//dataGridViewX1.RowTemplate.DefaultCellStyle.BackColor = BackColor;
			dataGridViewX1.BackgroundColor = this.BackColor;// dataGridViewX1.RowTemplate.DefaultCellStyle.BackColor;
			dataGridViewX1.GridColor = Color.FromArgb(40, 40, 40);
			dataGridViewX1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(40, 40, 40);
			dataGridViewX1.RowHeadersVisible = false;
		}

		public int? FrozenColumnIndex
		{
			get { return _FrozenColumnIndex; }
			set
			{
				if (_FrozenColumnIndex != value)
				{
					_FrozenColumnIndex = value;

					for (int i = 0; i < dataGridViewX1.Columns.Count; i++)
						dataGridViewX1.Columns[i].Frozen = (_FrozenColumnIndex.HasValue && i <= _FrozenColumnIndex);
				}
			}
		}

		public Color BackColor
		{
			get { return _BackColor; }
			set
			{
				if (_BackColor != value)
				{
					_BackColor = value;
					dataGridViewX1.BackgroundColor = _BackColor;

					foreach (DataGridViewColumn column in dataGridViewX1.Columns)
					{
						if (column is DataGridViewTextBoxColumnEx)
							((DataGridViewTextBoxColumnEx)column).BackColor = value;
						else if (column is DataGridViewComboBoxColumnEx)
							((DataGridViewComboBoxColumnEx)column).BackColor = value;
					}
				}
			}
		}

		public List<ColumnItem> Columns
		{
			get { return _Columns; }
		}

		public List<SlyceTreeGridItem> Items
		{
			get { return _Items; }
		}

		public Color InvalidColor
		{
			get { return _InvalidColor; }
			set
			{
				if (_InvalidColor != value)
				{
					_InvalidColor = value;

					foreach (DataGridViewColumn column in dataGridViewX1.Columns)
					{
						if (column is DataGridViewTextBoxColumnEx)
							((DataGridViewTextBoxColumnEx)column).InvalidColor = value;
						else if (column is DataGridViewComboBoxColumnEx)
							((DataGridViewComboBoxColumnEx)column).InvalidColor = value;
					}
				}
			}
		}

		public Color DisabledColor
		{
			get { return _DisabledColor; }
			set
			{
				if (_DisabledColor != value)
				{
					_DisabledColor = value;

					foreach (DataGridViewColumn column in dataGridViewX1.Columns)
					{
						if (column is DataGridViewTextBoxColumnEx)
							((DataGridViewTextBoxColumnEx)column).DisabledColor = value;
						else if (column is DataGridViewComboBoxColumnEx)
							((DataGridViewComboBoxColumnEx)column).DisabledColor = value;
					}
				}
			}
		}

		/// <summary>
		/// This should only be called when populating the Grid for the first time, 
		/// or when the number of columns changes.
		/// </summary>
		private void ResetLabelCache()
		{
			foreach (LabelX label in Labels)
				this.Controls.Remove(label);

			foreach (LabelX label in CategoryLabels)
				this.Controls.Remove(label);

			Labels.Clear();
			CategoryLabels.Clear();

			if (Columns.Count > 0)
			{
				LabelX catLabel = new LabelX();
				catLabel.BackColor = Color.Transparent;
				catLabel.Text = Columns[0].CategoryName;
				catLabel.TextAlignment = StringAlignment.Center;
				catLabel.BackgroundStyle.BackColor = Columns[0].CategoryBackColor;// Color.FromArgb(82, 128, 208);
				catLabel.BackgroundStyle.BackColor2 = Columns[0].CategoryBackColor2;// Color.Navy;
				catLabel.ForeColor = Columns[0].CategoryForeColor;
				catLabel.BackgroundStyle.BackColorGradientAngle = 90;
				//catLabel.ForeColor = Color.White;
				catLabel.Visible = false;

				this.Controls.Add(catLabel);
				CategoryLabels.Add(catLabel);
				catLabel.BringToFront();
			}
			for (int i = 0; i < Columns.Count; i++)
			{
				LabelX label = new LabelX();
				label.BackColor = Color.Transparent;
				label.Text = Columns[i].Text;
				label.TextAlignment = StringAlignment.Center;
				label.BackgroundStyle.BackColor = Color.FromArgb(100, 100, 100);
				label.BackgroundStyle.BackColor2 = Color.FromArgb(8, 8, 8);
				label.BackgroundStyle.BackColorGradientAngle = 90;
				label.ForeColor = Color.White;
				label.Visible = false;

				if (Columns[i].IsLink)
				{
					label.MouseEnter += new EventHandler(label_MouseEnter);
					label.MouseLeave += new EventHandler(label_MouseLeave);
				}
				label.MouseClick += new MouseEventHandler(label_MouseClick);
				label.Tag = i.ToString();

				this.Controls.Add(label);
				Labels.Add(label);
				label.BringToFront();

				if (Columns[i].CategoryName != CategoryLabels[CategoryLabels.Count - 1].Text)
				{
					LabelX catLabel = new LabelX();
					catLabel.BackColor = Color.Transparent;
					catLabel.Text = Columns[i].CategoryName;
					catLabel.TextAlignment = StringAlignment.Center;
					catLabel.BackgroundStyle.BackColor = Columns[i].CategoryBackColor;
					catLabel.BackgroundStyle.BackColor2 = Columns[i].CategoryBackColor2;
					catLabel.BackgroundStyle.BackColorGradientAngle = 90;
					catLabel.ForeColor = Columns[i].CategoryForeColor;
					catLabel.Visible = false;

					this.Controls.Add(catLabel);
					CategoryLabels.Add(catLabel);
					catLabel.BringToFront();
				}
			}
		}

		void label_MouseClick(object sender, MouseEventArgs e)
		{
			LabelX label = (LabelX)sender;
			int columnIndex = int.Parse((label).Tag.ToString());
			Point pt = e.Location;
			pt.Offset(label.Location);
			MouseEventArgs a = new MouseEventArgs(e.Button, e.Clicks, pt.X, pt.Y, e.Delta);
			Columns[columnIndex].RaiseClick(a);
		}

		void label_MouseLeave(object sender, EventArgs e)
		{
			LabelX label = (LabelX)sender;
			label.Font = new Font(label.Font, FontStyle.Regular);
			Cursor = Cursors.Default;
		}

		void label_MouseEnter(object sender, EventArgs e)
		{
			LabelX label = (LabelX)sender;
			label.Font = new Font(label.Font, FontStyle.Underline);
			Cursor = Cursors.Hand;
		}

		public bool AllowUserToAddRows
		{
			get { return dataGridViewX1.AllowUserToAddRows; }
			set { dataGridViewX1.AllowUserToAddRows = value; }
		}

		private void RefreshHeaderLabels()
		{
			if (BusyPopulatingColumns)
				return;

			if (dataGridViewX1.Columns.Count == 0)
				return;

			int fullWidth = dataGridViewX1.RowHeadersVisible ? dataGridViewX1.RowHeadersWidth : 0;

			foreach (DataGridViewColumn col in dataGridViewX1.Columns)
			{
				if (!col.Frozen)
					fullWidth += col.Width;
			}
			int horzOffset = Convert.ToInt32(dataGridViewX1.HorizontalScrollingOffsetValue * fullWidth);
			int headerRowHeight = dataGridViewX1.ColumnHeadersHeight;
			int realColumnIndex = 0;
			int widthPrevCols = dataGridViewX1.RowHeadersVisible ? dataGridViewX1.RowHeadersWidth : 0;
			int visibleGridWidth = dataGridViewX1.Width;
			int widthOfFrozenColumns = 0;

			if (dataGridViewX1.VerticalScrollbarVisible)
				visibleGridWidth -= dataGridViewX1.VerticalScrollbarWidth;

			if (ShowDeleteColumn && dataGridViewX1.Columns.Count > 0)
			{
				widthPrevCols = dataGridViewX1.Columns[0].Width;
				realColumnIndex = 1;
			}
			for (int i = 0; i < Columns.Count; i++)
			{
				int colWidth = dataGridViewX1.Columns[realColumnIndex].Width;

				//if (Columns[i].IsNullable)
				//    colWidth += dataGridViewX1.Columns[realColumnIndex + 1].Width;

				Rectangle rectColumn1 = new Rectangle();

				if ((!FrozenColumnIndex.HasValue || FrozenColumnIndex.Value < i + 1) &&
					(widthPrevCols + colWidth < horzOffset ||
					widthPrevCols >= horzOffset + visibleGridWidth))
				{
					widthPrevCols += colWidth;

					// This column is not in the current view window
					//if (Columns[i].IsNullable)
					//    realColumnIndex += 2;
					//else
					realColumnIndex += 1;

					Labels[i].Visible = false;
					continue;
				}
				if (FrozenColumnIndex.HasValue && i + 1 <= FrozenColumnIndex.Value)
				{
					// Column is fully displayed
					rectColumn1 = new Rectangle(widthPrevCols + horzOffset, 0, colWidth, headerRowHeight);

					if (i + 1 == FrozenColumnIndex.Value)
					{
						widthOfFrozenColumns += widthPrevCols + colWidth;// +2;
						//widthPrevCols = 0;
						//    //horzOffset = widthPrevCols + horzOffset + colWidth;
						//    widthPrevCols = widthPrevCols + horzOffset;
					}
				}
				else
				{
					if (widthPrevCols < widthOfFrozenColumns + horzOffset)
					{
						colWidth -= widthOfFrozenColumns + horzOffset - widthPrevCols;// -4;
						rectColumn1.X = widthOfFrozenColumns + horzOffset;
						widthPrevCols = widthOfFrozenColumns + horzOffset;// +2;
						rectColumn1.Width = colWidth;
					}
					else if (widthPrevCols + colWidth > horzOffset + visibleGridWidth)
					{
						colWidth = horzOffset + visibleGridWidth - widthPrevCols;
						rectColumn1.X = widthPrevCols;
						rectColumn1.Width = colWidth;
					}
					else
						rectColumn1 = new Rectangle(widthPrevCols, 0, colWidth, headerRowHeight);
				}
				rectColumn1.Height = headerRowHeight / 2;
				rectColumn1.X += 2;
				rectColumn1.Y = 1;
				rectColumn1.Width -= 4;

				widthPrevCols += colWidth;
				//realColumnIndex += Columns[i].IsNullable ? 2 : 1;
				realColumnIndex += 1;

				if (rectColumn1.Width <= 0)
				{
					Labels[i].Visible = false;
					continue;
				}
				Labels[i].Size = rectColumn1.Size;
				Labels[i].Top = dataGridViewX1.Top + rectColumn1.Height;
				Labels[i].Left = rectColumn1.Left - horzOffset + dataGridViewX1.Left;
				Labels[i].Visible = true;
				Labels[i].Refresh();
			}

			#region Categories
			for (int catCounter = CategoryLabels.Count - 1; catCounter >= 0; catCounter--)
			{
				LabelX catLabel = CategoryLabels[catCounter];
				int left = -1;
				int width = 0;
				bool found = false;

				for (int i = 0; i < Columns.Count; i++)
				{
					if (Columns[i].CategoryName == catLabel.Text)
					{
						found = true;

						if (left < 0)
							left = Labels[i].Left;

						if (Labels[i].Width > 0)
							width = Labels[i].Right - left;
					}
					else if (found)
						break;
				}
				catLabel.Left = left;

				if (left > 0)
					width += 2;

				catLabel.Width = width;
				catLabel.Top = dataGridViewX1.Top;
				catLabel.Height = headerRowHeight / 2;
				catLabel.Visible = true;
				catLabel.BringToFront();
				catLabel.Refresh();
			}
			#endregion
		}

		public void Populate()
		{
			try
			{
				BusyPopulating = true;
				ResetLabelCache();
				Slyce.Common.Utility.SuspendPainting(dataGridViewX1);
				dataGridViewX1.Rows.Clear();

				PopulateColumns();

				for (int i = 0; i < Items.Count; i++)
				{
					SlyceTreeGridItem item = Items[i];
					AddItemToGrid(item, false);
				}
				//RefreshHeaderLabels();

				if (dataGridViewX1.Rows.Count > 0)
				{
					if (ShowDeleteColumn)
						dataGridViewX1.Rows[0].Cells[1].Selected = true;
					else
						dataGridViewX1.Rows[0].Cells[0].Selected = true;
				}
				ResetAllDeleteImages();
			}
			finally
			{
				Slyce.Common.Utility.ResumePainting(dataGridViewX1);
				BusyPopulating = false;
				dataGridViewX1.Refresh();
				ResetLabelCache();
				RefreshHeaderLabels();
			}
		}

		private void AddItemToGrid(SlyceTreeGridItem item, bool updateLastRow)
		{
			List<object> subItemValues = new List<object>();

			if (ShowDeleteColumn)
				subItemValues.Add(DeleteImage);

			List<int> cellsToIgnore = new List<int>();
			List<int> cellsToDisable = new List<int>();

			for (int subItemCounter = 0; subItemCounter < item.SubItems.Count; subItemCounter++)
			{
				SlyceTreeGridCellItem subItem = item.SubItems[subItemCounter];

				//if (subItem.IsNullable)
				//{
				//    subItemValues.Add(subItem.HasValue);

				//    if (subItem.Ignore)
				//        cellsToIgnore.Add(subItemValues.Count - 1);
				//}
				subItemValues.Add(subItem.Value);

				if (subItem.Ignore)
					cellsToIgnore.Add(subItemValues.Count - 1);

				if (!subItem.Enabled)
					cellsToDisable.Add(subItemValues.Count - 1);

				//else if (subItem.IsNullable && !subItem.HasValue)
				//    cellsToDisable.Add(subItemValues.Count - 1);
			}
			int i;

			if (updateLastRow)
			{
				i = dataGridViewX1.Rows.Count - 2;
				dataGridViewX1.Rows[i].SetValues(subItemValues.ToArray());
			}
			else
				i = dataGridViewX1.Rows.Add(subItemValues.ToArray());

			foreach (int cellIndex in cellsToIgnore)
			{
				if (cellIndex >= dataGridViewX1.Rows[i].Cells.Count)
					continue;

				object ccc = dataGridViewX1.Rows[i].Cells[cellIndex];

				if (ccc is DataGridViewTextBoxCellEx)
					((DataGridViewTextBoxCellEx)ccc).Enabled = false;
				else if (ccc is DataGridViewComboBoxCellEx)
					((DataGridViewComboBoxCellEx)ccc).Enabled = false;
				else
				{
					dataGridViewX1.Rows[i].Cells[cellIndex].ReadOnly = true;
					dataGridViewX1.Rows[i].Cells[cellIndex].Style.BackColor = Color.Pink;
					//dataGridViewX1.Rows[i].Cells[cellIndex - 1].Style.BackColor = Color.Pink;
				}
			}
			foreach (int cellIndex in cellsToDisable)
			{
				dataGridViewX1.Rows[i].Cells[cellIndex].ReadOnly = true;
				//dataGridViewX1.Rows[i].Cells[cellIndex].Style.BackColor = Color.LightGray;
				//dataGridViewX1.Rows[i].Cells[cellIndex - 1].Style.BackColor = Color.LightGray;
			}
			for (int rowCounter = 0; rowCounter < dataGridViewX1.Rows.Count; rowCounter++)
				dataGridViewX1.Rows[rowCounter].DefaultCellStyle.BackColor = rowCounter % 2 == 0 ? BackColor : AlternatingBackColor;

			Tags.Add(item.Tag);
		}

		public void Clear()
		{
			BusyClearing = true;
			dataGridViewX1.Rows.Clear();
			dataGridViewX1.Columns.Clear();

			foreach (Control control in Labels)
				this.Controls.Remove(control);

			foreach (Control control in CategoryLabels)
				this.Controls.Remove(control);

			Labels.Clear();
			CategoryLabels.Clear();
			Items.Clear();
			Columns.Clear();
			Tags.Clear();
			BusyClearing = false;
		}

		private void PopulateColumns()
		{
			BusyPopulatingColumns = true;
			EditColumns.Clear();
			dataGridViewX1.Columns.Clear();

			if (ShowDeleteColumn)
			{
				DataGridViewImageColumn col = new DataGridViewImageColumn();
				col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
				col.DefaultCellStyle.NullValue = DeleteImage;
				col.HeaderText = "";
				col.Frozen = FrozenColumnIndex.HasValue;
				dataGridViewX1.Columns.Add(col);
			}
			for (int i = 0; i < Columns.Count; i++)
			{
				ColumnItem column = Columns[i];
				//DataGridViewColumn colNullableCheckbox = null;
				DataGridViewColumn col = null;

				switch (column.ControlType)
				{
					case ColumnItem.ColumnTypes.Checkbox:
						col = new DataGridViewCheckBoxColumn();
						break;
					case ColumnItem.ColumnTypes.Textbox:
						col = new DataGridViewTextBoxColumnEx();
						((DataGridViewTextBoxColumnEx)col).DisabledColor = DisabledColor;
						((DataGridViewTextBoxColumnEx)col).InvalidColor = InvalidColor;
						((DataGridViewTextBoxColumnEx)col).BackColor = BackColor;
						break;
					case ColumnItem.ColumnTypes.IntegerInput:
						col = new DataGridViewIntegerInputColumn();
						((DataGridViewIntegerInputColumn)col).DisplayControlForCurrentCellOnly = true;
						((DataGridViewIntegerInputColumn)col).BackgroundStyle.Border = eStyleBorderType.None;
						((DataGridViewIntegerInputColumn)col).ShowUpDown = true;
						break;
					case ColumnItem.ColumnTypes.NullableCheckBox:
						col = new DataGridViewComboBoxColumnEx();
						List<string> vals = new List<string>(new string[] { "", "True", "False" });
						((DataGridViewComboBoxColumnEx)col).DataSource = vals;
						((DataGridViewComboBoxColumnEx)col).DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
						((DataGridViewComboBoxColumnEx)col).FlatStyle = FlatStyle.Flat;
						((DataGridViewComboBoxColumnEx)col).DisabledColor = DisabledColor;
						((DataGridViewComboBoxColumnEx)col).InvalidColor = InvalidColor;
						((DataGridViewComboBoxColumnEx)col).BackColor = BackColor;
						break;
					case ColumnItem.ColumnTypes.NullableTextBox:
						col = new DataGridViewTextBoxColumnEx();
						((DataGridViewTextBoxColumnEx)col).DisabledColor = DisabledColor;
						((DataGridViewTextBoxColumnEx)col).InvalidColor = InvalidColor;
						((DataGridViewTextBoxColumnEx)col).BackColor = BackColor;
						break;
					case ColumnItem.ColumnTypes.NullableIntegerInput:
						col = new DataGridViewTextBoxColumnEx();
						((DataGridViewTextBoxColumnEx)col).DisabledColor = DisabledColor;
						((DataGridViewTextBoxColumnEx)col).InvalidColor = InvalidColor;
						((DataGridViewTextBoxColumnEx)col).BackColor = BackColor;
						((DataGridViewTextBoxColumnEx)col).Alignment = DataGridViewContentAlignment.MiddleRight;
						break;
					case ColumnItem.ColumnTypes.ComboBox:
						col = new DataGridViewComboBoxColumnEx();
						List<string> keys = column.ComboItems.Keys.ToList();
						keys.Sort();
						((DataGridViewComboBoxColumnEx)col).DataSource = keys;
						((DataGridViewComboBoxColumnEx)col).DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
						((DataGridViewComboBoxColumnEx)col).FlatStyle = FlatStyle.Flat;
						((DataGridViewComboBoxColumnEx)col).DisabledColor = DisabledColor;
						((DataGridViewComboBoxColumnEx)col).InvalidColor = InvalidColor;
						((DataGridViewComboBoxColumnEx)col).BackColor = BackColor;
						break;
					case ColumnItem.ColumnTypes.Image:
						col = new DataGridViewImageColumn();
						break;
					case ColumnItem.ColumnTypes.Link:
						col = new DataGridViewLinkColumn();
						break;
					case ColumnItem.ColumnTypes.None:
						col = new DataGridViewLinkColumn();
						break;
					default:
						throw new NotImplementedException("Not handled yet");
				}
				//if (colNullableCheckbox != null)
				//{
				//    colNullableCheckbox.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;// true;
				//    colNullableCheckbox.Resizable = DataGridViewTriState.False;// false;
				//    dataGridViewX1.Columns.Add(colNullableCheckbox);
				//}
				col.Frozen = (FrozenColumnIndex.HasValue && i < FrozenColumnIndex);
				col.HeaderText = column.Text;
				col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;//.AllCells;
				col.Resizable = DataGridViewTriState.True;
				col.ReadOnly = column.ReadOnly;
				dataGridViewX1.Columns.Add(col);
				EditColumns.Add(dataGridViewX1.Columns.Count - 1, i);
			}
			//dataGridViewX1.Columns[1].Frozen = true;
			BusyPopulatingColumns = false;
		}

		private void dataGridViewX1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
		{
			RefreshHeaderLabels();
		}

		private void dataGridViewX1_Scroll(object sender, ScrollEventArgs e)
		{
			RefreshHeaderLabels();
		}

		private void dataGridViewX1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			if (e.RowIndex == -1 && e.ColumnIndex > -1)
			{
				Rectangle r2 = e.CellBounds;
				r2.Y += e.CellBounds.Height / 2;
				r2.Height = e.CellBounds.Height / 2;

				e.PaintBackground(r2, true);

				e.PaintContent(r2);
				e.Handled = true;
			}
		}

		private void dataGridViewX1_Resize(object sender, EventArgs e)
		{
			RefreshHeaderLabels();
		}

		private void dataGridViewX1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			if (BusyPopulating || BusyClearing)
				return;

			if (CellValueChanged != null)
			{
				object obj = e.RowIndex < Tags.Count ? Tags[e.RowIndex] : null;

				//CellValueChanged(e.RowIndex, e.ColumnIndex, dataGridViewX1.Columns[e.ColumnIndex].HeaderText, dataGridViewX1.Rows[e.RowIndex].Tag, GetValue(e.RowIndex, e.ColumnIndex));
				if (string.IsNullOrEmpty(dataGridViewX1.Columns[e.ColumnIndex].HeaderText) &&
					e.ColumnIndex < dataGridViewX1.Columns.Count - 1 &&
					!string.IsNullOrEmpty(dataGridViewX1.Columns[e.ColumnIndex + 1].HeaderText) &&
					EditColumns.ContainsKey(e.ColumnIndex + 1))
				{
					CellValueChanged(e.RowIndex, e.ColumnIndex, EditColumns[e.ColumnIndex + 1], dataGridViewX1.Columns[e.ColumnIndex].HeaderText, ref obj, GetValue(e.RowIndex, e.ColumnIndex));
				}
				else
				{
					CellValueChanged(e.RowIndex, e.ColumnIndex, EditColumns[e.ColumnIndex], dataGridViewX1.Columns[e.ColumnIndex].HeaderText, ref obj, GetValue(e.RowIndex, e.ColumnIndex));
				}
				if (e.RowIndex < Tags.Count)
					Tags[e.RowIndex] = obj;
			}
		}

		private void dataGridViewX1_CellEnter(object sender, DataGridViewCellEventArgs e)
		{
			// Workaround for bug. See 'Workarounds' tab here: http://connect.microsoft.com/VisualStudio/feedback/details/98504/datagridview-row-select-with-editmode-editonenter
			((DataGridView)sender).EditMode = DataGridViewEditMode.EditOnEnter;
		}

		private void dataGridViewX1_MouseClick(object sender, MouseEventArgs e)
		{
			// Workaround for bug. See 'Workarounds' tab here: http://connect.microsoft.com/VisualStudio/feedback/details/98504/datagridview-row-select-with-editmode-editonenter
			DataGridView.HitTestInfo hti = dataGridViewX1.HitTest(e.X, e.Y);

			if (hti.Type == DataGridViewHitTestType.RowHeader)
			{

				((DataGridView)sender).EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
				((DataGridView)sender).EndEdit();

				//dataGridViewX1.FirstDisplayedScrollingRowIndex = dataGridViewX1.Rows[hti.RowIndex].Index;
				//dataGridViewX1.Refresh();
				//dataGridViewX1.CurrentCell = dataGridViewX1.Rows[hti.RowIndex].Cells[0];
				//dataGridViewX1.CurrentCell.Selected = false;
				//dataGridViewX1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
				//dataGridViewX1.Rows[hti.RowIndex].Selected = true;
				//dataGridViewX1.Refresh();
			}
			else
			{
				dataGridViewX1.SelectionMode = DataGridViewSelectionMode.CellSelect;
			}
		}

		public void RemoveItem(int index)
		{
			dataGridViewX1.Rows.RemoveAt(index);
			Items.RemoveAt(index);
		}

		private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0)
				return;

			if (ShowDeleteColumn && e.ColumnIndex == 0 &&
				e.RowIndex != dataGridViewX1.Rows.Count - 1 &&
				DeleteClicked != null)
			{
				if (DeleteClicked(e.RowIndex, Tags[e.RowIndex]))
				{
					dataGridViewX1.Rows.RemoveAt(e.RowIndex);
					// Re-populate the Tags colection, so that Tags indexes remain in-sync with the grid
					Tags.RemoveAt(e.RowIndex);
				}
			}
			else if (ShowDeleteColumn && e.ColumnIndex == 0 &&
				e.RowIndex == dataGridViewX1.Rows.Count - 1)// &&
			// DeleteClicked != null)
			{
				// Add clicked
			}
			else
			{
				if (string.IsNullOrEmpty(dataGridViewX1.Columns[e.ColumnIndex].HeaderText) &&
					EditColumns.ContainsKey(e.ColumnIndex + 1))
				{
					DataGridViewCell checkboxCell = dataGridViewX1.Rows[e.RowIndex].Cells[e.ColumnIndex];

					if (checkboxCell.Value != null)
					{
						bool isChecked = (bool)checkboxCell.Value;
						DataGridViewCell dataCell = dataGridViewX1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1];
						dataCell.ReadOnly = isChecked;
						dataCell.Style.BackColor = isChecked ? Color.LightGray : BackColor;
						checkboxCell.Style.BackColor = isChecked ? Color.LightGray : BackColor;

						if (!isChecked)
							dataCell.Selected = true;
					}
				}
			}
		}

		public object GetValue(int row, int column)
		{
			return dataGridViewX1.Rows[row].Cells[column].Value;
		}

		public void SetValue(int row, int column, object value)
		{
			dataGridViewX1.Rows[row].Cells[column].Value = value;

			object ccc = dataGridViewX1.Rows[row].Cells[column];

			if (ccc is DataGridViewComboBoxCell)
			{
				DataGridViewComboBoxCell combo = (DataGridViewComboBoxCell)ccc;
				combo.Value = value;
			}
		}

		private void dataGridViewX1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			if (BusyPopulating || BusyClearing)
				return;

			if (ShowDeleteColumn)
				dataGridViewX1.Rows[e.RowCount - 1].Cells[0].Value = DeleteImage;

			ResetAllDeleteImages();

			if (NewRowAdded != null)
			{
				object newObject;
				NewRowAdded(out newObject);
				AddItemToGrid(Items[Items.Count - 1], true);
			}
		}

		private void ResetAllDeleteImages()
		{
			if (!ShowDeleteColumn)
				return;

			for (int i = 0; i < dataGridViewX1.Rows.Count; i++)
			{
				if (i < dataGridViewX1.Rows.Count - 1)
					dataGridViewX1.Rows[i].Cells[0].Value = DeleteImage;
				else
					dataGridViewX1.Rows[i].Cells[0].Value = BlankImage;
			}
		}

		private void dataGridViewX1_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			string gg = e.Exception.Message;
		}

		private void dataGridViewX1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			if (EditColumns.ContainsKey(e.ColumnIndex))
			{
				if (e.RowIndex >= Items.Count)
					return;

				bool ignore = Items[e.RowIndex].SubItems[EditColumns[e.ColumnIndex]].Ignore;

				if (ignore)
					return;

				ColumnItem col = Columns[EditColumns[e.ColumnIndex]];
				string val = e.FormattedValue.ToString();

				switch (col.ControlType)
				{
					case ColumnItem.ColumnTypes.NullableIntegerInput:
						int result;

						if (string.IsNullOrEmpty(val))
						{
							//dataGridViewX1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = NullColor;
							return;
						}
						else if (!Int32.TryParse((string)e.FormattedValue, out result))
						{
							dataGridViewX1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.DarkRed;
							return;
						}
						else
						{
							dataGridViewX1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = BackColor;
							return;
						}
					case ColumnItem.ColumnTypes.IntegerInput:
						int result2;

						if (string.IsNullOrEmpty(val))
						{
							// Nulls not allowed
							dataGridViewX1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.DarkRed;
							return;
						}
						else if (!Int32.TryParse((string)e.FormattedValue, out result2))
						{
							dataGridViewX1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.DarkRed;
							return;
						}
						else
						{
							dataGridViewX1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = BackColor;
							return;
						}
					//default:
					//    throw new NotImplementedException("Not handled yet");
				}
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			RefreshHeaderLabels();
		}

		/// <summary>
		/// Ends all current cell-edits, comitting changes.
		/// </summary>
		public void FinaliseEdits()
		{
			if (dataGridViewX1.SelectedCells.Count == 0)
				return;

			int row = dataGridViewX1.SelectedCells[0].RowIndex;
			int col = dataGridViewX1.SelectedCells[0].ColumnIndex;

			if (row < 0 || col < 0)
				return;

			dataGridViewX1.Rows[dataGridViewX1.SelectedCells[0].RowIndex].DataGridView.EndEdit();

			if (dataGridViewX1.CurrentRow != null)
				dataGridViewX1.CurrentRow.DataGridView.EndEdit();

			dataGridViewX1.EndEdit();//.Rows[row].Cells[column].IsInEditMode.Selected = true;

			if (CellValueChanged != null)
			{
				object obj = row < Tags.Count ? Tags[row] : null;

				//CellValueChanged(e.RowIndex, e.ColumnIndex, dataGridViewX1.Columns[e.ColumnIndex].HeaderText, dataGridViewX1.Rows[e.RowIndex].Tag, GetValue(e.RowIndex, e.ColumnIndex));
				if (string.IsNullOrEmpty(dataGridViewX1.Columns[col].HeaderText) &&
					col < dataGridViewX1.Columns.Count - 1 &&
					!string.IsNullOrEmpty(dataGridViewX1.Columns[col + 1].HeaderText) &&
					EditColumns.ContainsKey(col + 1))
				{
					CellValueChanged(row, col, EditColumns[col + 1], dataGridViewX1.Columns[col].HeaderText, ref obj, GetValue(row, col));
				}
				else
				{
					CellValueChanged(row, col, EditColumns[col], dataGridViewX1.Columns[col].HeaderText, ref obj, GetValue(row, col));
				}
				if (row < Tags.Count)
					Tags[row] = obj;
			}

		}
	}
}
