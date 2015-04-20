using System.Collections;

namespace Slyce.Common
{
	public class TreelistUtility
	{
		public class TreelistNodeComparer : IComparer
		{
			public enum SortDirection
			{
				Ascending,
				Descending
			}
			private SortDirection _direction = SortDirection.Ascending;
			private int ColumnIndex = 0;

			public TreelistNodeComparer()
				: base()
			{
			}

			public TreelistNodeComparer(SortDirection sortDirection, int columnIndex)
			{
				_direction = sortDirection;
				ColumnIndex = columnIndex;
			}

			public int Compare(object node1, object node2)
			{
				return Compare((DevComponents.AdvTree.Node)node1, (DevComponents.AdvTree.Node)node2);
			}

			public int Compare(DevComponents.AdvTree.Node node1, DevComponents.AdvTree.Node node2)
			{
				if (node1 == null && node2 == null)
				{
					return 0;
				}
				else if (node1 == null && node2 != null)
				{
					return (this._direction == SortDirection.Ascending) ? -1 : 1;
				}
				else if (node1 != null && node2 == null)
				{
					return (this._direction == SortDirection.Ascending) ? 1 : -1;
				}
				else
				{
					string node1Text = node1.Cells[ColumnIndex].Text;
					string node2Text = node2.Cells[ColumnIndex].Text;

					if (node1Text != node2Text)
					{
						// The names are not the same, so no need to check parameters
						return (this._direction == SortDirection.Ascending) ? node1Text.CompareTo(node2Text) : node2Text.CompareTo(node1Text);
					}
					else // The names are equal, so need to check data-type as well
					{
						return (this._direction == SortDirection.Ascending) ? node1Text.CompareTo(node2Text) : node2Text.CompareTo(node1Text);
					}
				}
			}
		}

		public static void SortNodes(DevComponents.AdvTree.AdvTree treeList, TreelistNodeComparer comparer)
		{
			treeList.Nodes.Sort((IComparer)comparer);

			foreach (DevComponents.AdvTree.Node subNode in treeList.Nodes)
			{
				SortSubNodes(subNode, comparer);
			}
		}

		private static void SortSubNodes(DevComponents.AdvTree.Node node, TreelistNodeComparer comparer)
		{
			node.Nodes.Sort((IComparer)comparer);

			foreach (DevComponents.AdvTree.Node subNode in node.Nodes)
			{
				SortSubNodes(subNode, comparer);
			}
		}
	}
}
