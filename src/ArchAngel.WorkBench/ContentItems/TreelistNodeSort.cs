using System.Collections;

namespace ArchAngel.Workbench.ContentItems
{
	public class TreelistNodeComparer : IComparer
	{
		public TreelistNodeComparer()
			: base()
		{
		}

		public int Compare(object node1, object node2)
		{
			return Compare((DevComponents.AdvTree.Node)node1, (DevComponents.AdvTree.Node)node2);
		}

		public int Compare(DevComponents.AdvTree.Node node1, DevComponents.AdvTree.Node node2)
		{
			if (node1 == null && node2 == null) return 0;
			else if (node1 == null && node2 != null) return -1;
			else if (node1 != null && node2 == null) return 1;
			else
			{
				if (node1.Tag is ArchAngel.Interfaces.Template.File &&
					node2.Tag is ArchAngel.Interfaces.Template.File)
					return ((ArchAngel.Interfaces.Template.File)node1.Tag).Name.CompareTo(((ArchAngel.Interfaces.Template.File)node2.Tag).Name);
				else if (node1.Tag is ArchAngel.Interfaces.Template.Folder &&
					node2.Tag is ArchAngel.Interfaces.Template.Folder)
					return ((ArchAngel.Interfaces.Template.Folder)node1.Tag).Name.CompareTo(((ArchAngel.Interfaces.Template.Folder)node2.Tag).Name);
				else if (node1.Tag is ArchAngel.Interfaces.Template.StaticFile &&
					node2.Tag is ArchAngel.Interfaces.Template.StaticFile)
					//return ((ArchAngel.Interfaces.Template.StaticFile)node1.Tag).Name.CompareTo(((ArchAngel.Interfaces.Template.StaticFile)node2.Tag).Name);
					return node1.Text.CompareTo(node2.Text);
				else
				{
					if (node1.Tag is ArchAngel.Interfaces.Template.Folder)
						return -1;
					else if (node1.Tag is ArchAngel.Interfaces.Template.StaticFile)
						return -1;
					else
						return 1;
				}
				//return node1.Tag is ArchAngel.Interfaces.Template.Folder ? -1 : 1;
			}
		}
	}

	//    public static void SortNodes(DevComponents.AdvTree.AdvTree treeList, TreelistNodeComparer comparer)
	//    {
	//        treeList.Nodes.Sort((IComparer)comparer);

	//        foreach (DevComponents.AdvTree.Node subNode in treeList.Nodes)
	//        {
	//            SortSubNodes(subNode, comparer);
	//        }
	//    }

	//    private static void SortSubNodes(DevComponents.AdvTree.Node node, TreelistNodeComparer comparer)
	//    {
	//        node.Nodes.Sort((IComparer)comparer);

	//        foreach (DevComponents.AdvTree.Node subNode in node.Nodes)
	//        {
	//            SortSubNodes(subNode, comparer);
	//        }
	//    }
	//}
}

