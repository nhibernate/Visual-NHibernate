using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ArchAngel.Providers.Database.Controls.FilterWizard
{
    public partial class ucFilterColumns : Interfaces.Controls.ContentItems.ContentItem
    {
        private enum Cells
        {
            Selected = 0,
            Table = 1,
            Column = 2,
            Alias = 3,
            Comparer = 4,
            Logical = 5
        }
        private enum LogicalValues
        {
            And = 0,
            Or = 1,
            BLANK = 2
        }
        private enum ComparerValue
        {
            Equal,
            LessThan,
            GreaterThan,
            LessThanOrEqual,
            GreaterThanOrEqual,
            NotEqual,
            Like
        }
        readonly FormFilter2 Owner;

        public ucFilterColumns(FormFilter2 owner)
        {
            InitializeComponent();
            HasNext = true;
            HasPrev = false;
            Owner = owner;
            NextText = "&Next >";
            PageHeader = "Filter Columns";
            PageDescription = "Select which columns you want to use to filter the results. Specify how they should be filtered.";
            Populate();
        }

        private string GetComboValue(LogicalValues value)
        {
            if (value == LogicalValues.BLANK)
            {
                return "";
            }
            return value.ToString();
        }

        private string GetComboValue(ComparerValue value)
        {
            switch (value)
            {
                case ComparerValue.Equal: return "=";
                case ComparerValue.GreaterThan: return ">";
                case ComparerValue.GreaterThanOrEqual: return ">=";
                case ComparerValue.LessThan: return "<";
                case ComparerValue.LessThanOrEqual: return "<=";
                case ComparerValue.NotEqual: return "<>";
                case ComparerValue.Like: return "LIKE";
                default:
                    throw new NotImplementedException("Not coded yet: " + value.ToString());
            }
        }

        private void Populate()
        {
            FillComboboxes();

            // Add 'checked' columns at the top
            foreach (ArchAngel.Providers.Database.Model.Column column in Owner.TheFilter.Parent.Columns)
            {
                AddGridRow(column, true);
            }
            // Add non-checked columns
            foreach (ArchAngel.Providers.Database.Model.Column column in Owner.TheFilter.Parent.Columns)
            {
                AddGridRow(column, false);
            }
            HighlightSelectedRows();
            DeselectAllRows();
        }

        private void DeselectAllRows()
        {
            for (int i = gridFilterColumns.SelectedRows.Count - 1; i >= 0; i--)
            {
                DataGridViewRow row = gridFilterColumns.SelectedRows[i];
                row.Selected = false;
            }
        }

        public override void OnDisplaying()
        {
            DeselectAllRows();
        }

        private void FillComboboxes()
        {
            DataGridViewComboBoxColumn comparerColumn = (DataGridViewComboBoxColumn)gridFilterColumns.Columns[(int)Cells.Comparer];
            DataGridViewComboBoxColumn logicalColumn = (DataGridViewComboBoxColumn)gridFilterColumns.Columns[(int)Cells.Logical];
            comparerColumn.Items.Clear();
            logicalColumn.Items.Clear();
            //comparerColumn.Items.AddRange(new object[] { "=", "<", ">", "<=", ">=", "<>" });
            //logicalColumn.Items.AddRange(new object[] { "And", "Or" });

            logicalColumn.Items.Add(GetComboValue(LogicalValues.BLANK));
            logicalColumn.Items.Add(GetComboValue(LogicalValues.And));
            logicalColumn.Items.Add(GetComboValue(LogicalValues.Or));

            comparerColumn.Items.Add(GetComboValue(ComparerValue.Equal));
            comparerColumn.Items.Add(GetComboValue(ComparerValue.GreaterThan));
            comparerColumn.Items.Add(GetComboValue(ComparerValue.GreaterThanOrEqual));
            comparerColumn.Items.Add(GetComboValue(ComparerValue.LessThan));
            comparerColumn.Items.Add(GetComboValue(ComparerValue.LessThanOrEqual));
            comparerColumn.Items.Add(GetComboValue(ComparerValue.NotEqual));
            comparerColumn.Items.Add(GetComboValue(ComparerValue.Like));
        }

        public override bool Next()
        {
            Owner.TheFilter.ClearFilterColumns();
            gridFilterColumns.EndEdit();

            foreach (DataGridViewRow row in gridFilterColumns.Rows)
            {
                Model.Filter.FilterColumn filterColumn;

                if ((bool)row.Cells[(int)Cells.Selected].Value)
                {
                    string logicalValue = row.Cells[(int)Cells.Logical].Value == null ? "" : row.Cells[(int)Cells.Logical].Value.ToString().Trim();

                    if (row.Tag is Model.Filter.FilterColumn)
                    {
                        filterColumn = (Model.Filter.FilterColumn)row.Tag;
                        filterColumn.CompareOperator = row.Cells[(int)Cells.Comparer].Value.ToString();
                        filterColumn.LogicalOperator = logicalValue;
                    }
                    else
                    {
                        Model.Column column = (Model.Column)row.Tag;
                        filterColumn = new Model.Filter.FilterColumn(column, logicalValue, row.Cells[(int)Cells.Comparer].Value.ToString(), column.Name);
                    }
                    Owner.TheFilter.AddFilterColumn(filterColumn);
                }
            }
            return true;
        }

        public override bool Back()
        {
            return true;
        }

        public DataGridViewRow AddGridRow(Model.Column column, bool checkedColumnsOnly)
        {
            DataGridViewRow newRow = null;
            Model.Filter.FilterColumn filterColumn = null;

            foreach (ArchAngel.Providers.Database.Model.Filter.FilterColumn filterCol in Owner.TheFilter.FilterColumns)
            {
                if (filterCol.Column.Name == column.Name && filterCol.Column.Parent.Name == column.Parent.Name)
                {
                    filterColumn = filterCol;
                    break;
                }
            }
            if (filterColumn != null)
            {
                if (checkedColumnsOnly)
                {
                    string logicalOperator = filterColumn.LogicalOperator;
                    
                    if (string.IsNullOrEmpty(logicalOperator))
                    {
                        logicalOperator = " ";
                    }
                    int index = gridFilterColumns.Rows.Add(new object[] { true, filterColumn.Column.Parent.Name, filterColumn.Column.Name, filterColumn.Column.Alias, filterColumn.CompareOperator, logicalOperator });
                    gridFilterColumns.Rows[index].Tag = filterColumn;
                    newRow = gridFilterColumns.Rows[index];
                }
            }
            else
            {
                if (!checkedColumnsOnly)
                {
                    int index = gridFilterColumns.Rows.Add(new object[] { false, column.Parent.Name, column.Name, column.Alias, "=", "And" });
                    gridFilterColumns.Rows[index].Tag = column;
                    newRow = gridFilterColumns.Rows[index];
                }
            }
            return newRow;
        }

        private void gridFilterColumns_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (Owner.IsAddingNewFilter)
            {
                string filterName = string.Format("Get{0}By", Owner.TheFilter.Parent.Alias);
                bool empty = true;

                foreach (DataGridViewRow row in gridFilterColumns.Rows)
                {
                    if ((bool)row.Cells[(int)Cells.Selected].Value == true)
                    {
                        if (!empty)
                        {
                            filterName += "And";
                        }
                        empty = false;

                        if (row.Tag is Model.Filter.FilterColumn)
                        {
                            filterName += ((Model.Filter.FilterColumn)row.Tag).Alias;
                        }
                        else
                        {
                            filterName += ((Model.Column)row.Tag).Alias;
                        }
                    }
                }
                Owner.TheFilter.Name = filterName;
            }
            if (e.ColumnIndex == (int)Cells.Selected)
            {
                HighlightSelectedRows();
            }
        }

        private void HighlightSelectedRows()
        {
            foreach (DataGridViewRow row in gridFilterColumns.Rows)
            {
                bool selected = (bool)row.Cells[(int)Cells.Selected].Value;
                FontStyle fontStyle = selected ? FontStyle.Bold : FontStyle.Regular;
                row.DefaultCellStyle.BackColor = selected ? Color.GreenYellow : Color.White;    
                row.DefaultCellStyle.Font = new Font(gridFilterColumns.DefaultCellStyle.Font, fontStyle);
            }
        }

        private void gridFilterColumns_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            gridFilterColumns.BeginEdit(false);

            if (e.ColumnIndex == (int)Cells.Comparer ||
                e.ColumnIndex == (int)Cells.Logical)
            {
                if (gridFilterColumns.EditingControl != null &&
                    gridFilterColumns.EditingControl is ComboBox)
                {
                    ((ComboBox)gridFilterColumns.EditingControl).DroppedDown = true;
                }
            }
        }

        private void gridFilterColumns_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //string gg = "";
            throw e.Exception;
        }
    }
}
