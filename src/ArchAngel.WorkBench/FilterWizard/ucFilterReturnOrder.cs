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
    public partial class ucFilterReturnOrder : ArchAngel.Interfaces.Controls.ContentItems.ContentItem
    {
        ArchAngel.Providers.Database.Controls.FormFilter2 Owner;
        DevExpress.XtraEditors.Repository.RepositoryItemComboBox ReposComboBoxSortDirection = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();

        public ucFilterReturnOrder(ArchAngel.Providers.Database.Controls.FormFilter2 owner)
        {
            InitializeComponent();
            HasNext = true;
            HasPrev = true;
            Owner = owner;
            PageHeader = "Result Ordering";
            PageDescription = "Select which columns you want to order the results by.";
            Populate();
        }

        public override bool Next()
        {
            return true;// FormFilter2.ScreenNames.WhereClause.ToString();
        }

        public override bool Back()
        {
            return true;//FormFilter2.ScreenNames.Columns.ToString();
        }

        private void Populate()
        {
            treeList1.BeginUnboundLoad();
            foreach (ArchAngel.Providers.Database.Model.Column column in Owner.Filter.Parent.Columns)
            {
                AddTreeListNode(column, true);
            }
            foreach (ArchAngel.Providers.Database.Model.Column column in Owner.Filter.Parent.Columns)
            {
                AddTreeListNode(column, false);
            }
            ReposComboBoxSortDirection.Items.AddRange(new string[] { "ASC", "DESC" });
            treeList1.RepositoryItems.Add(ReposComboBoxSortDirection);
            treeList1.Columns[2].ColumnEdit = ReposComboBoxSortDirection;
            treeList1.EndUnboundLoad();
        }

        public TreeListNode AddTreeListNode(ArchAngel.Providers.Database.Model.Column column, bool selectedOnly)
        {
            TreeListNode newNode = null;
            ArchAngel.Providers.Database.Model.Filter.OrderByColumn orderColumn = null;

            foreach (ArchAngel.Providers.Database.Model.Filter.OrderByColumn orderCol in Owner.Filter.OrderByColumns)
            {
                if (orderCol.Column.Name == column.Name && orderCol.Column.Parent.Name == column.Parent.Name)
                {
                    orderColumn = orderCol;
                    break;
                }
            }
            if (orderColumn != null && selectedOnly)
            {
                newNode = treeList1.AppendNode(new object[] { orderColumn.Column.Parent.Name, orderColumn.Column.Name, orderColumn.SortOperator }, null);
                newNode.StateImageIndex = 1;
                newNode.Tag = orderColumn;
            }
            else if (orderColumn == null && !selectedOnly)
            {
                newNode = treeList1.AppendNode(new object[] { column.Parent.Name, column.Name, "" }, null);
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
                        node[2] = "";
                        //TreeListNode lastSelectedNode = node;

                        //while (lastSelectedNode.NextNode != null && lastSelectedNode.NextNode.StateImageIndex == 1)
                        //{
                        //    lastSelectedNode = lastSelectedNode.NextNode;
                        //}
                        //if (lastSelectedNode != node)
                        //{
                        //    int index = treeList1.GetNodeIndex(lastSelectedNode);
                        //    treeList1.SetNodeIndex(node, index);
                        //    treeList1.Selection.Clear();
                        //    treeList1.FocusedNode = treeList1.Nodes[0];
                        //    //treeList1.SetFocusedNode(treeList1.Nodes[0]);
                        //    //treeList1.Selection.Set(treeList1.Nodes[0]);
                        //}
                    }
                    else
                    {
                        node.StateImageIndex = 1;
                        if (string.IsNullOrEmpty((string)node[2]))
                        {
                            node[2] = "ASC";
                        }
                        //TreeListNode firstNonSelectedNode = node;

                        //while (firstNonSelectedNode.PrevNode != null && firstNonSelectedNode.PrevNode.StateImageIndex == 0)
                        //{
                        //    firstNonSelectedNode = firstNonSelectedNode.PrevNode;
                        //}
                        //if (firstNonSelectedNode != node)
                        //{
                        //    int index = treeList1.GetNodeIndex(firstNonSelectedNode);
                        //    treeList1.SetNodeIndex(node, treeList1.GetNodeIndex(firstNonSelectedNode));
                        //    treeList1.Selection.Clear();
                        //    treeList1.FocusedNode = treeList1.Nodes[0];
                        //    //treeList1.SetFocusedNode(treeList1.Nodes[0]);
                        //    //treeList1.Selection.Set(treeList1.Nodes[0]);
                        //}
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
            Owner.Filter.ClearOrderByColumns();
            List<ArchAngel.Providers.Database.Model.Filter.OrderByColumn> orderColumns = new List<ArchAngel.Providers.Database.Model.Filter.OrderByColumn>();

            foreach (TreeListNode node in treeList1.Nodes)
            {
                ArchAngel.Providers.Database.Model.Filter.OrderByColumn orderColumn;

                if (node.StateImageIndex == 1)
                {
                    Type type = node.Tag.GetType();

                    if (type == typeof(ArchAngel.Providers.Database.Model.Filter.OrderByColumn))
                    {
                        orderColumn = (ArchAngel.Providers.Database.Model.Filter.OrderByColumn)node.Tag;
                        orderColumn.SortOperator = (string)node[2];
                    }
                    else
                    {
                        ArchAngel.Providers.Database.Model.Column column = (ArchAngel.Providers.Database.Model.Column)node.Tag;
                        orderColumn = new ArchAngel.Providers.Database.Model.Filter.OrderByColumn(column, (string)node[2]);
                    }
                    Owner.Filter.AddOrderByColumn(orderColumn);
                }
            }
            return true;
        }

        private void treeList1_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            //EnableUpDownButtons();
        }

        //private void EnableUpDownButtons()
        //{
        //    if (treeList1.FocusedNode != null)
        //    {
        //        buttonUp.Enabled = (treeList1.FocusedNode.StateImageIndex == 1) && treeList1.FocusedNode.PrevNode != null && treeList1.FocusedNode.PrevNode.StateImageIndex == 1;
        //        buttonDown.Enabled = (treeList1.FocusedNode.StateImageIndex == 1) && treeList1.FocusedNode.NextNode != null && treeList1.FocusedNode.NextNode.StateImageIndex == 1;
        //    }
        //    else
        //    {
        //        buttonUp.Enabled = buttonDown.Enabled = false;
        //    }
        //}

        private void buttonUp_Click(object sender, EventArgs e)
        {
            int index = treeList1.GetNodeIndex(treeList1.FocusedNode);

            if (index > 0)
            {
                treeList1.SetNodeIndex(treeList1.FocusedNode, index - 1);
            }
            //EnableUpDownButtons();
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            int index = treeList1.GetNodeIndex(treeList1.FocusedNode);

            if (index < treeList1.Nodes.Count - 1)
            {
                treeList1.SetNodeIndex(treeList1.FocusedNode, index + 1);
            }
            //EnableUpDownButtons();
        }


    }
}
