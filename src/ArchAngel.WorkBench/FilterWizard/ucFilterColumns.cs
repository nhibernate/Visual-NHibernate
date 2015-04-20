using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

namespace ArchAngel.Workbench.FilterWizard
{
    public partial class ucFilterColumns : ArchAngel.Interfaces.Controls.ContentItems.ContentItem
    {
        ArchAngel.Providers.Database.Controls.FormFilter2 Owner;
        DevExpress.XtraEditors.Repository.RepositoryItemComboBox ReposComboBoxComparisonOperator = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
        DevExpress.XtraEditors.Repository.RepositoryItemComboBox ReposComboBoxAndOrOperator = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();

        public ucFilterColumns(ArchAngel.Providers.Database.Controls.FormFilter2 owner)
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

        private void Populate()
        {
            treeList1.BeginUnboundLoad();
            foreach (ArchAngel.Providers.Database.Model.Column column in Owner.Filter.Parent.Columns)
            {
                AddTreeListNode(column);
            }
            ReposComboBoxComparisonOperator.Items.AddRange(new string[] { "=", "<", ">", ">=", "<=", "<>" });
            ReposComboBoxAndOrOperator.Items.AddRange(new string[] { "And", "Or" });
            treeList1.RepositoryItems.Add(ReposComboBoxAndOrOperator);
            treeList1.RepositoryItems.Add(ReposComboBoxComparisonOperator);
            treeList1.Columns[3].ColumnEdit = ReposComboBoxComparisonOperator;
            treeList1.Columns[4].ColumnEdit = ReposComboBoxAndOrOperator;
            treeList1.EndUnboundLoad();
        }

        public override bool Next()
        {
            return true;//FormFilter2.ScreenNames.ReturnOrder.ToString();
        }

        public override bool Back()
        {
            return true;//FormFilter2.ScreenNames.Nothing.ToString();
        }

        public TreeListNode AddTreeListNode(ArchAngel.Providers.Database.Model.Column column)
        {
            TreeListNode newNode;
            ArchAngel.Providers.Database.Model.Filter.FilterColumn filterColumn = null;

            foreach (ArchAngel.Providers.Database.Model.Filter.FilterColumn filterCol in Owner.Filter.FilterColumns)
            {
                if (filterCol.Column.Name == column.Name && filterCol.Column.Parent.Name == column.Parent.Name)
                {
                    filterColumn = filterCol;
                    break;
                }
            }
            if (filterColumn != null)
            {
                newNode = treeList1.AppendNode(new object[] { filterColumn.Column.Parent.Name, filterColumn.Column.Name, filterColumn.Column.Alias, filterColumn.LogicalOperator, filterColumn.CompareOperator }, null);
                newNode.StateImageIndex = 1;
                newNode.Tag = filterColumn;
            }
            else
            {
                newNode = treeList1.AppendNode(new object[] { column.Parent.Name, column.Name, column.Alias, "", "" }, null);
                newNode.StateImageIndex = 0;
                newNode.Tag = column;
            }
            return newNode;
        }

        private void treeList1_MouseDown(object sender, MouseEventArgs e)
        {
            DevExpress.XtraTreeList.TreeListHitInfo hInfo = treeList1.CalcHitInfo(new Point(e.X, e.Y));
            TreeListNode node = hInfo.Node;

            if (node == null)
            {
                return;
            }
            // Toggle Checkbox
            if (e.Button == MouseButtons.Left)
            {
                if (hInfo.HitInfoType == HitInfoType.StateImage)
                {
                    bool isChecked = node.StateImageIndex == 1;

                    if (isChecked)
                    {
                        node.StateImageIndex = 0;
                        node[3] = "";
                        node[4] = "";
                    }
                    else
                    {
                        node.StateImageIndex = 1;

                        if (string.IsNullOrEmpty((string)node[3]))
                        {
                            node[3] = "=";
                        }
                        if (string.IsNullOrEmpty((string)node[4]))
                        {
                            node[4] = "And";
                        }
                    }
                }
            }
            else
            {
                treeList1.FocusedNode = node;
            }
            if (Owner.IsAddingNewFilter)
            {
                string filterName = string.Format("Get{0}By", Owner.Filter.Parent.Alias);
                bool empty = true;

                foreach (TreeListNode nodeToCheck in treeList1.Nodes)
                {
                    if (nodeToCheck.StateImageIndex == 1)
                    {
                        if (!empty)
                        {
                            filterName += "And";
                        }
                        empty = false;
                        filterName += ((ArchAngel.Providers.Database.Model.Column)nodeToCheck.Tag).Alias;
                    }
                }
                Owner.Filter.Name = filterName;
            }
        }

        private void treeList1_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            Type nodeType = e.Node.Tag.GetType();
            
            if (e.Node.StateImageIndex == 0)
            {
                e.Appearance.BackColor = Color.White;
            }
            else
            {
                e.Appearance.BackColor = Color.GreenYellow;
                e.Appearance.ForeColor = Slyce.Common.Colors.IdealTextColor(e.Appearance.BackColor);
            }
            if (e.Node.Focused)
            {
                SetFocusedNodeProperties(e);
            }
        }

        private void SetFocusedNodeProperties(GetCustomNodeCellStyleEventArgs e)
        {
            e.Appearance.ForeColor = Slyce.Common.Colors.IdealTextColor(e.Appearance.BackColor);
            //double brightness = Slyce.Common.Colors.GetBrightness(e.Appearance.BackColor);
            //double lightBrightness = brightness > 0.5 ? brightness - 0.2 : brightness + 0.1;
            //double darkBrightness = brightness > 0.5 ? brightness - 0.6 : brightness - 0.4;

            //if (lightBrightness > 1) { lightBrightness = 1; }
            //if (darkBrightness < 0) { darkBrightness = 0; }

            //Color lightColor = Slyce.Common.Colors.ChangeBrightness(e.Appearance.BackColor, lightBrightness);
            //Color darkColor = Slyce.Common.Colors.ChangeBrightness(e.Appearance.BackColor, darkBrightness);

            //e.Appearance.ForeColor = Slyce.Common.Colors.IdealTextColor(darkColor);// Color.White;
            //e.Appearance.BackColor = lightColor;
            //e.Appearance.BackColor2 = darkColor;
            //e.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            //e.Appearance.Options.UseBackColor = true;
        }

        public override bool Save()
        {
            Owner.Filter.ClearFilterColumns();
            List<ArchAngel.Providers.Database.Model.Filter.FilterColumn> filterColumns = new List<ArchAngel.Providers.Database.Model.Filter.FilterColumn>();

            foreach (TreeListNode node in treeList1.Nodes)
            {
                ArchAngel.Providers.Database.Model.Filter.FilterColumn filterColumn;

                if (node.StateImageIndex == 1)
                {
                    Type type = node.Tag.GetType();

                    if (type == typeof(ArchAngel.Providers.Database.Model.Filter.FilterColumn))
                    {
                        filterColumn = (ArchAngel.Providers.Database.Model.Filter.FilterColumn)node.Tag;
                        filterColumn.CompareOperator = (string)node[3];
                        filterColumn.LogicalOperator = (string)node[4];
                    }
                    else
                    {
                        ArchAngel.Providers.Database.Model.Column column = (ArchAngel.Providers.Database.Model.Column)node.Tag;
                        filterColumn = new ArchAngel.Providers.Database.Model.Filter.FilterColumn(column, (string)node[4], (string)node[4], column.Name);
                    }
                    Owner.Filter.AddFilterColumn(filterColumn);
                }
            }
            return true;
        }


    }
}
