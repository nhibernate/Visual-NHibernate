using System;

namespace ArchAngel.Providers.Database.Controls.FilterWizard
{
    public partial class ucFilterReturnOrder : Interfaces.Controls.ContentItems.ContentItem
    {
    	readonly FormFilter2 Owner;
    	//readonly DevExpress.XtraEditors.Repository.RepositoryItemComboBox ReposComboBoxSortDirection = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();

        public ucFilterReturnOrder(FormFilter2 owner)
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
			//Owner.TheFilter.ClearOrderByColumns();

			//foreach (TreeListNode node in treeList1.Nodes)
			//{
			//    Model.Filter.OrderByColumn orderColumn;

			//    if (node.StateImageIndex == 1)
			//    {
			//        Type type = node.Tag.GetType();

			//        if (type == typeof(Model.Filter.OrderByColumn))
			//        {
			//            orderColumn = (Model.Filter.OrderByColumn)node.Tag;
			//            orderColumn.SortOperator = (string)node[2];
			//        }
			//        else
			//        {
			//            Model.Column column = (Model.Column)node.Tag;
			//            orderColumn = new Model.Filter.OrderByColumn(column, (string)node[2]);
			//        }
			//        Owner.TheFilter.AddOrderByColumn(orderColumn);
			//    }
			//}
			return true;
        }

        public override bool Back()
        {
            return true;
        }

        private void Populate()
        {
			//treeList1.BeginUnboundLoad();
			//foreach (Model.Column column in Owner.TheFilter.Parent.Columns)
			//{
			//    AddTreeListNode(column, true);
			//}
			//foreach (Model.Column column in Owner.TheFilter.Parent.Columns)
			//{
			//    AddTreeListNode(column, false);
			//}
			//ReposComboBoxSortDirection.Items.AddRange(new[] { "ASC", "DESC" });
			//treeList1.RepositoryItems.Add(ReposComboBoxSortDirection);
			//treeList1.Columns[2].ColumnEdit = ReposComboBoxSortDirection;
			//treeList1.EndUnboundLoad();
        }

		//public TreeListNode AddTreeListNode(Model.Column column, bool selectedOnly)
		//{
		//    TreeListNode newNode = null;
		//    Model.Filter.OrderByColumn orderColumn = null;

		//    foreach (Model.Filter.OrderByColumn orderCol in Owner.TheFilter.OrderByColumns)
		//    {
		//        if (orderCol.Column.Name == column.Name && orderCol.Column.Parent.Name == column.Parent.Name)
		//        {
		//            orderColumn = orderCol;
		//            break;
		//        }
		//    }
		//    if (orderColumn != null && selectedOnly)
		//    {
		//        newNode = treeList1.AppendNode(new object[] { orderColumn.Column.Parent.Name, orderColumn.Column.Name, orderColumn.SortOperator }, null);
		//        newNode.StateImageIndex = 1;
		//        newNode.Tag = orderColumn;
		//    }
		//    else if (orderColumn == null && !selectedOnly)
		//    {
		//        newNode = treeList1.AppendNode(new object[] { column.Parent.Name, column.Name, "" }, null);
		//        newNode.StateImageIndex = 0;
		//        newNode.Tag = column;
		//    }
		//    return newNode;
		//}

		//private void treeList1_MouseDown(object sender, MouseEventArgs e)
		//{
		//    TreeListHitInfo hInfo = treeList1.CalcHitInfo(new Point(e.X, e.Y));
		//    TreeListNode node = hInfo.Node;

		//    if (node == null)
		//    {
		//        return;
		//    }
		//    // Toggle Checkbox
		//    if (e.Button == MouseButtons.Left)
		//    {
		//        if (hInfo.HitInfoType == HitInfoType.StateImage)
		//        {
		//            bool isChecked = node.StateImageIndex == 1;

		//            if (isChecked)
		//            {
		//                node.StateImageIndex = 0;
		//                node[2] = "";
		//            }
		//            else
		//            {
		//                node.StateImageIndex = 1;
		//                if (string.IsNullOrEmpty((string)node[2]))
		//                {
		//                    node[2] = "ASC";
		//                }
		//                if (string.IsNullOrEmpty((string)node[3]))
		//                {
		//                    node[3] = "=";
		//                }
		//                if (string.IsNullOrEmpty((string)node[4]))
		//                {
		//                    node[4] = "And";
		//                }
		//            }
		//        }
		//    }
		//    else
		//    {
		//        treeList1.FocusedNode = node;
		//    }
		//}

		//private void treeList1_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
		//{
		//    if (e.Node.StateImageIndex == 0)
		//    {
		//        e.Appearance.BackColor = Color.White;
		//    }
		//    else
		//    {
		//        e.Appearance.BackColor = Color.GreenYellow;
		//        e.Appearance.ForeColor = Slyce.Common.Colors.IdealTextColor(e.Appearance.BackColor);
		//    }
		//    if (e.Node.Focused)
		//    {
		//        SetFocusedNodeProperties(e);
		//    }
		//}

		//private static void SetFocusedNodeProperties(GetCustomNodeCellStyleEventArgs e)
		//{
		//    e.Appearance.ForeColor = Slyce.Common.Colors.IdealTextColor(e.Appearance.BackColor);
		//}

		private void buttonUp_Click(object sender, EventArgs e)
		{
			//int index = treeList1.GetNodeIndex(treeList1.FocusedNode);

			//if (index > 0)
			//{
			//    treeList1.SetNodeIndex(treeList1.FocusedNode, index - 1);
			//}
		}

		private void buttonDown_Click(object sender, EventArgs e)
		{
			//int index = treeList1.GetNodeIndex(treeList1.FocusedNode);

			//if (index < treeList1.Nodes.Count - 1)
			//{
			//    treeList1.SetNodeIndex(treeList1.FocusedNode, index + 1);
			//}
		}
    }
}
