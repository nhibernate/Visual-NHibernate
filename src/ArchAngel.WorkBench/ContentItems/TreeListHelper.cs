using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;

// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;
using ArchAngel.Providers.Database.BLL;

namespace ArchAngel.Workbench.ContentItems
{
    public class TreeListHelper
    {
        private TreeList _treeList;
        private TreeListNode _treeListNode;
        //public delegate void UpdateTreeNodeCallback(TreeListNode treeListNode, Color color);

        public TreeListHelper(TreeList treeList)
        {
            _treeList = treeList;
        }

        public void TreeListViewValidate()
        {
            TreeListViewValidate(_treeList);

            Slyce.Common.Utility.ResumePainting();
        }

        public void TreeListNodeValidate()
        {
            TreeListNodeValidate(_treeListNode);
        }

        public static void TreeListViewValidate(TreeList treeList)
        {
            if (treeList.Nodes.Count == 0)
            {
                return;
            }

            foreach (TreeListNode treeListNode in treeList.Nodes)
            {
                TreeListNodeValidate(treeListNode);
            }
        }

        public static void TreeListNodeValidate(TreeListNode treeListNode)
        {
            //TreeListNodeColor(treeListNode, Color.Black);
            //TreeListNodeError(treeListNode);
        }

        private static void TreeListNodeColor(TreeListNode treeListNode, Color color)
        {
            //treeListNode.ForeColor = color;

            foreach (TreeListNode childTreeListNode in treeListNode.Nodes)
            {
                //Thread.Sleep(1);

                if (ArchAngel.Providers.Database.BLL.Helper.GetBaseType(childTreeListNode.Tag) == typeof(ScriptBase))
                {
                    ScriptBase scriptBase = (ScriptBase)childTreeListNode.Tag;
                    if (!scriptBase.Enabled)
                    {
                        continue;
                    }

                    if (scriptBase.GetType() == typeof(MapColumn))
                    {
                        color = Color.Green;
                    }
                    else
                    {
                        if (scriptBase.IsUserDefined)
                        {
                            color = Color.Blue;
                        }
                    }
                }

                TreeListNodeColor(childTreeListNode, color);
            }
        }

        public static void TreeListNodeError(DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            return;
            TreeListNode treeListNode = e.Node;

            if (treeListNode.StateImageIndex == 0)
            {
                return;
            }

            if (ArchAngel.Providers.Database.BLL.Helper.GetBaseType(treeListNode.Tag) == typeof(ScriptBase))
            {
                ScriptBase scriptBase = (ScriptBase)treeListNode.Tag;
                if (scriptBase.Enabled)
                {
                    bool errorNode = false;
                    if (scriptBase.GetType() == typeof(ArchAngel.Providers.Database.Model.Table) ||
                        scriptBase.GetType() == typeof(ArchAngel.Providers.Database.Model.View) ||
                        scriptBase.GetType() == typeof(ArchAngel.Providers.Database.Model.StoredProcedure))
                    {
                        if (CountAlias(Controller.Instance.BllDatabase.EnabledScriptObjects, scriptBase.Alias) > 1)
                        {
                            errorNode = true;
                            //treeListNode.TreeList.SetColumnError(treeListNode.TreeList.Columns[0], "Duplicate alias: More than one item has the same alias. Right-click one of the duplicate nodes to change its name to be unique.");
                        }
                    }

                    if (scriptBase.GetType() == typeof(Column) ||
                        scriptBase.GetType() == typeof(MapColumn))
                    {
                        ScriptObject scriptObject = ((Column)scriptBase).Parent;
                        if (CountAlias(ScriptBLL.GetEnabledColumns(scriptObject.Columns), scriptBase.Alias) > 1)
                        {
                            errorNode = true;
                            //treeListNode.TreeList.SetColumnError(treeListNode.TreeList.Columns[0], "Duplicate alias: More than one column has the same alias. Right-click one of the duplicate columns to change its name to be unique.");
                        }
                    }

                    if (scriptBase.GetType() == typeof(Key))
                    {
                        ArchAngel.Providers.Database.Model.Table table = (ArchAngel.Providers.Database.Model.Table)((Key)scriptBase).Parent;
                        if (CountAlias(ScriptBLL.GetEnabledKeys(table.Keys), scriptBase.Alias) > 1)
                        {
                            errorNode = true;
                            //treeListNode.TreeList.SetColumnError(treeListNode.TreeList.Columns[0], "Duplicate alias: More than one key has the same alias. Right-click one of the duplicate keys to change its name to be unique.");
                        }
                    }

                    if (scriptBase.GetType() == typeof(Filter))
                    {
                        ScriptObject scriptObject = (ScriptObject)((Filter)scriptBase).Parent;
                        if (CountAlias(ScriptBLL.GetEnabledFilters(scriptObject.Filters), scriptBase.Alias) > 1)
                        {
                            errorNode = true;
                            //treeListNode.TreeList.SetColumnError(treeListNode.TreeList.Columns[0], "Duplicate alias: More than one filter has the same alias. Right-click one of the duplicate filters to change its name to be unique.");
                        }
                    }

                    if (scriptBase.GetType() == typeof(OneToOneRelationship) ||
                        scriptBase.GetType() == typeof(OneToManyRelationship) ||
                        scriptBase.GetType() == typeof(ManyToOneRelationship) ||
                        scriptBase.GetType() == typeof(ManyToManyRelationship))
                    {
                        ScriptObject scriptObject = (ScriptObject)((Relationship)scriptBase).Parent;
                        if (CountAlias(ScriptBLL.GetEnabledRelationships(scriptObject.Relationships), scriptBase.Alias) > 1)
                        {
                            errorNode = true;
                            //treeListNode.TreeList.SetColumnError(treeListNode.TreeList.Columns[0], "Duplicate alias: More than one relationship has the same alias. Right-click one of the duplicate relationships to change its name to be unique.");
                        }
                    }

                    if (errorNode)
                    {
                        e.Appearance.ForeColor = Color.Red;
                        // Parents are shown as red also
                        /*TreeListNode parentTreeListNode = treeListNode.ParentNode;
                        while (parentTreeListNode != null)
                        {
                            //parentTreeListNode.ForeColor = Color.Red;
                            parentTreeListNode = parentTreeListNode.ParentNode;
                        }*/
                    }
                    else
                    {
                        //treeListNode.TreeList.ToolTipController.SetToolTip(treeListNode, "");
                    }
                }
            }

            /*foreach (TreeListNode childTreeListNode in treeListNode.Nodes)
            {
                if (ArchAngel.Providers.Database.BLL.Helper.GetBaseType(childTreeListNode.Tag) == typeof(ScriptBase))
                {
                    ScriptBase scriptBase = (ScriptBase)childTreeListNode.Tag;
                    if (!scriptBase.Enabled)
                    {
                        continue;
                    }
                }

                TreeListNodeError(childTreeListNode);
            }*/
        }

        private static int CountAlias(ScriptBase[] scriptBases, string alias)
        {
            int count = 0;

            foreach (ScriptBase scriptBase in scriptBases)
            {
                if (scriptBase.Alias == alias)
                {
                    count++;
                }
            }

            return count;
        }
    }
}