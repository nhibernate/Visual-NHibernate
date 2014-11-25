using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;

namespace ArchAngel.Designer.Wizards.TemplateSync
{
    public partial class ScreenApiExtensions : ArchAngel.Interfaces.Controls.ContentItems.ContentItem
    {
        #region Enums
        enum TreeNodeImages
        {
            Unchecked = 0,
            Checked = 1
        }
        enum ActionTypes
        {
            Import,
            Remove
        }
        #endregion

        private int ProjectsHash = 0;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
        private bool MustSkip = false;

        public ScreenApiExtensions()
        {
            InitializeComponent();
            this.PageHeader = "API Extensions";
            this.PageDescription = "Select which API Extensions to synchronise.";
            this.HasNext = true;
            this.HasPrev = true;
            repositoryItemButtonEdit1.Click += new EventHandler(repositoryItemButtonEdit1_Click);
            repositoryItemTextEdit.ReadOnly = true;
            repositoryItemTextEdit.AllowFocused = false;
        }

        void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            for (int i = treeListBoth.FocusedNode.Nodes.Count - 1; i >= 0; i--)
            {
                ApplyChange(treeListBoth.FocusedNode.Nodes[i]);
            }
            if (treeListBoth.FocusedNode.Nodes.Count == 0)
            {
                treeListBoth.Nodes.Remove(treeListBoth.FocusedNode);
            }
        }

        private void ApplyChange(TreeListNode node)
        {
            string tagName = (string)node.GetValue(0);
            object theirValue = node.GetValue(1);

            switch (tagName)
            {
                case "Category":
                    ((Project.UserOption)node.ParentNode.Tag).Category = (string)theirValue;
                    break;
                case "DefaultValue":
                    ((Project.UserOption)node.ParentNode.Tag).DefaultValue = (string)theirValue;
                    break;
                case "Description":
                    ((Project.UserOption)node.ParentNode.Tag).Description = (string)theirValue;
                    break;
                case "IteratorType":
                    ((Project.UserOption)node.ParentNode.Tag).IteratorType = (Type)theirValue;
                    break;
                case "ResetPerSession":
                    ((Project.UserOption)node.ParentNode.Tag).ResetPerSession = (bool)theirValue;
                    break;
                case "Text":
                    ((Project.UserOption)node.ParentNode.Tag).Text = (string)theirValue;
                    break;
                case "VariableName":
                    ((Project.UserOption)node.ParentNode.Tag).VariableName = (string)theirValue;
                    break;
                case "VarType":
                    ((Project.UserOption)node.ParentNode.Tag).VarType = (Type)theirValue;
                    break;
                default:
                    throw new NotImplementedException("Node not hendled yet: " + tagName);
            }
            node.TreeList.Nodes.Remove(node);
        }

        private void Populate()
        {
            int newHash = frmTemplateSyncWizard.MyProject.GetHashCode() + frmTemplateSyncWizard.TheirProject.GetHashCode();

            if (newHash != ProjectsHash)
            {
                ProjectsHash = newHash;
                treeListBoth.BeginUnboundLoad();
                treeListBoth.Nodes.Clear();
                int numChanges = AddDefaultValueFunctionNodes();
                treeListBoth.EndUnboundLoad();

                if (numChanges == 0)
                {
                    MustSkip = true;
                    frmTemplateSyncWizard.Instance.RemoveCurrentScreen();
                    return;
                }
                else
                {
                    MustSkip = false;
                }
            }
            else if (MustSkip)
            {
                frmTemplateSyncWizard.Instance.RemoveCurrentScreen();
                return;
            }
            if (treeListBoth.Nodes.Count == 0)
            {
                splitContainer1.Panel1Collapsed = true;
            }
            if (treeListTheirs.Nodes.Count == 0 &&
                treeListMine.Nodes.Count == 0)
            {
                splitContainer1.Panel2Collapsed = true;
            }
            else if (treeListTheirs.Nodes.Count == 0)
            {
                splitContainer2.Panel1Collapsed = true;
            }
            else if (treeListMine.Nodes.Count == 0)
            {
                splitContainer2.Panel2Collapsed = true;
            }

        }

        private int AddDefaultValueFunctionNodes()
        {
            #region Build list of owner types for virtual properties
            List<string> typeNames = new List<string>();
            Dictionary<string, TreeListNode> typeNodesBoth = new Dictionary<string, TreeListNode>();
            Dictionary<string, TreeListNode> typeNodesTheirs = new Dictionary<string, TreeListNode>();
            Dictionary<string, TreeListNode> typeNodesMine = new Dictionary<string, TreeListNode>();

            foreach (Project.UserOption theirUserOption in frmTemplateSyncWizard.TheirProject.UserOptions)
            {
                if (theirUserOption.IteratorType == null)
                {
                    continue;
                }
                if (typeNames.BinarySearch(theirUserOption.IteratorType.FullName) < 0)
                {
                    typeNames.Add(theirUserOption.IteratorType.FullName);
                    typeNames.Sort();
                }
            }
            foreach (Project.UserOption myUserOption in frmTemplateSyncWizard.MyProject.UserOptions)
            {
                if (myUserOption.IteratorType == null)
                {
                    continue;
                }
                if (typeNames.BinarySearch(myUserOption.IteratorType.FullName) < 0)
                {
                    typeNames.Add(myUserOption.IteratorType.FullName);
                    typeNames.Sort();
                }
            }
            foreach (string typeName in typeNames)
            {
                TreeListNode newNode = null;
                newNode = AddNodeBoth(typeName, null, null, null, typeName);
                newNode.StateImageIndex = -1;
                typeNodesBoth.Add(typeName, newNode);

                newNode = AddNodeTheirs(typeName, null, null, null, typeName);
                newNode.StateImageIndex = -1;
                typeNodesTheirs.Add(typeName, newNode);

                newNode = AddNodeMine(typeName, null, null, null, typeName);
                newNode.StateImageIndex = -1;
                typeNodesMine.Add(typeName, newNode);
            }
            #endregion

            int numAdded = 0;

            foreach (Project.UserOption theirUserOption in frmTemplateSyncWizard.TheirProject.UserOptions)
            {
                if (theirUserOption.IteratorType == null)
                {
                    continue;
                }
                bool found = false;
                TreeListNode userOptionNode = null;

                foreach (Project.UserOption myUserOption in frmTemplateSyncWizard.MyProject.UserOptions)
                {
                    if (Slyce.Common.Utility.StringsAreEqual(theirUserOption.VariableName, myUserOption.VariableName, false))
                    {
                        found = true;
                        userOptionNode = AddNodeBoth(myUserOption.VariableName + "[Virtual Property]", "", "", typeNodesBoth[theirUserOption.IteratorType.FullName], myUserOption);

                        if (AddUserOptionNode(userOptionNode, myUserOption, theirUserOption))
                        {
                            numAdded++;
                        }
                        else
                        {
                            treeListBoth.Nodes.Remove(userOptionNode);
                        }
                        break;
                    }
                }
                if (!found)
                {
                    userOptionNode = AddNodeTheirs(theirUserOption.VariableName + "[Virtual Property]", "", "", typeNodesTheirs[theirUserOption.IteratorType.FullName], null);
                    numAdded++;
                }
            }
            foreach (Project.UserOption myUserOption in frmTemplateSyncWizard.MyProject.UserOptions)
            {
                if (myUserOption.IteratorType == null)
                {
                    continue;
                }
                bool found = false;
                TreeListNode userOptionNode = null;

                foreach (Project.UserOption theirUserOption in frmTemplateSyncWizard.TheirProject.UserOptions)
                {
                    if (Slyce.Common.Utility.StringsAreEqual(theirUserOption.VariableName, myUserOption.VariableName, false))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    userOptionNode = AddNodeMine(myUserOption.VariableName + "[Virtual Property]", "", "", typeNodesMine[myUserOption.IteratorType.FullName], myUserOption);
                    numAdded++;
                }
            }
            RemoveEmptyNodes(treeListBoth);
            RemoveEmptyNodes(treeListTheirs);
            RemoveEmptyNodes(treeListMine);
            return numAdded;
        }

        private bool AddUserOptionNode(TreeListNode parentNode, Project.UserOption myOption, Project.UserOption theirOption)
        {
            int originalNumChildNodes = parentNode.Nodes.Count;

            if (myOption != null && theirOption != null)
            {
                if (myOption.Category != theirOption.Category) { AddNodeBoth("Category", theirOption.Category, myOption.Category, parentNode, null); }
                if (myOption.DefaultValue != theirOption.DefaultValue) { AddNodeBoth("DefaultValue", theirOption.DefaultValue, myOption.DefaultValue, parentNode, null); }
                if (myOption.Description != theirOption.Description) { AddNodeBoth("Description", theirOption.Description, myOption.Description, parentNode, null); }
                if (myOption.IteratorType != theirOption.IteratorType) { AddNodeBoth("IteratorType", theirOption.IteratorType, myOption.IteratorType, parentNode, null); }
                if (myOption.ResetPerSession != theirOption.ResetPerSession) { AddNodeBoth("ResetPerSession", theirOption.ResetPerSession, myOption.ResetPerSession, parentNode, null); }
                if (myOption.Text != theirOption.Text) { AddNodeBoth("Text", theirOption.Text, myOption.Text, parentNode, null); }
                // TODO: Handle enum values
                //if (myOption.Values != theirOption.Category) { AddNodeBoth("Category", theirOption.Category, myOption.Category, parentNode, null); }
                if (myOption.VariableName != theirOption.VariableName) { AddNodeBoth("VariableName", theirOption.VariableName, myOption.VariableName, parentNode, null); }
                if (myOption.VarType != theirOption.VarType) { AddNodeBoth("VarType", theirOption.VarType, myOption.VarType, parentNode, null); }

                return originalNumChildNodes != parentNode.Nodes.Count;
            }
            else if (theirOption != null)
            {
                AddNodeTheirs("Category", theirOption.Category, null, parentNode, null);
                AddNodeTheirs("DefaultValue", theirOption.DefaultValue, null, parentNode, null);
                AddNodeTheirs("Description", theirOption.Description, null, parentNode, null);
                AddNodeTheirs("IteratorType", theirOption.IteratorType, null, parentNode, null);
                AddNodeTheirs("ResetPerSession", theirOption.ResetPerSession, null, parentNode, null);
                AddNodeTheirs("Text", theirOption.Text, null, parentNode, null);
                // TODO: Handle enum values
                //if (myOption.Values != theirOption.Category) { AddNodeTheirs("Category", myOption.Category, theirOption.Category, parentNode, null); }
                AddNodeTheirs("VariableName", theirOption.VariableName, null, parentNode, null);
                AddNodeTheirs("VarType", theirOption.VarType, null, parentNode, null);
            }
            else if (myOption != null)
            {
                AddNodeMine("Category", null, myOption.Category, parentNode, null);
                AddNodeMine("DefaultValue", null, myOption.DefaultValue, parentNode, null);
                AddNodeMine("Description", null, myOption.Description, parentNode, null);
                AddNodeMine("IteratorType", null, myOption.IteratorType, parentNode, null);
                AddNodeMine("ResetPerSession", null, myOption.ResetPerSession, parentNode, null);
                AddNodeMine("Text", null, myOption.Text, parentNode, null);
                // TODO: Handle enum values
                //if (myOption.Values != theirOption.Category) { AddNodeMine("Category", myOption.Category, theirOption.Category, parentNode, null); }
                AddNodeMine("VariableName", null, myOption.VariableName, parentNode, null);
                AddNodeMine("VarType", null, myOption.VarType, parentNode, null);
            }
            return true;
        }


        private void RemoveEmptyNodes(DevExpress.XtraTreeList.TreeList treelist)
        {
            for (int i = treelist.Nodes.Count - 1; i >= 0; i--)
            {
                if (treelist.Nodes[i].Nodes.Count == 0)
                {
                    treelist.Nodes.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="myReferencedFile"></param>
        /// <param name="theirReferencedFile"></param>
        /// <returns>True if the referenced files were different and a node was added, false if they were the same and no node was added.</returns>
        private bool AddDefaultValueFunctionNode(TreeListNode parentNode, Project.DefaultValueFunction myDefaultValueFunction, Project.DefaultValueFunction theirDefaultValueFunction)
        {
            int originalNumChildNodes = parentNode.Nodes.Count;

            if (myDefaultValueFunction != null && theirDefaultValueFunction != null)
            {
                //if (myDefaultValueFunction.FunctionName != theirDefaultValueFunction.FunctionName) { AddNode("FunctionName", theirDefaultValueFunction.FunctionName, myDefaultValueFunction.FunctionName, parentNode, null); }
                if (myDefaultValueFunction.FunctionType != theirDefaultValueFunction.FunctionType) { AddNodeBoth("FunctionType", theirDefaultValueFunction.FunctionType, myDefaultValueFunction.FunctionType, parentNode, null); }
                if (myDefaultValueFunction.IsForUserOption != theirDefaultValueFunction.IsForUserOption) { AddNodeBoth("IsForUserOption", theirDefaultValueFunction.IsForUserOption, myDefaultValueFunction.IsForUserOption, parentNode, null); }
                if (myDefaultValueFunction.ObjectType != theirDefaultValueFunction.ObjectType) { AddNodeBoth("ObjectType", theirDefaultValueFunction.ObjectType, myDefaultValueFunction.ObjectType, parentNode, null); }
                if (myDefaultValueFunction.ParameterTypes != theirDefaultValueFunction.ParameterTypes) { AddNodeBoth("ParameterTypes", theirDefaultValueFunction.ParameterTypes, myDefaultValueFunction.ParameterTypes, parentNode, null); }
                if (myDefaultValueFunction.PropertyName != theirDefaultValueFunction.PropertyName) { AddNodeBoth("PropertyName", theirDefaultValueFunction.PropertyName, myDefaultValueFunction.PropertyName, parentNode, null); }
                if (myDefaultValueFunction.UseCustomCode != theirDefaultValueFunction.UseCustomCode) { AddNodeBoth("UseCustomCode", theirDefaultValueFunction.UseCustomCode, myDefaultValueFunction.UseCustomCode, parentNode, null); }

                return originalNumChildNodes != parentNode.Nodes.Count;
            }
            else if (theirDefaultValueFunction != null)
            {
                //AddNode("FunctionName", theirDefaultValueFunction.FunctionName, null, parentNode, null);
                AddNodeTheirs("FunctionType", theirDefaultValueFunction.FunctionType, null, parentNode, null);
                AddNodeTheirs("IsForUserOption", theirDefaultValueFunction.IsForUserOption, null, parentNode, null);
                AddNodeTheirs("ObjectType", theirDefaultValueFunction.ObjectType, null, parentNode, null);
                AddNodeTheirs("ParameterTypes", theirDefaultValueFunction.ParameterTypes, null, parentNode, null);
                AddNodeTheirs("PropertyName", theirDefaultValueFunction.PropertyName, null, parentNode, null);
                AddNodeTheirs("UseCustomCode", theirDefaultValueFunction.UseCustomCode, null, parentNode, null);
            }
            else if (myDefaultValueFunction != null)
            {
                //AddNode("FunctionName", myDefaultValueFunction.FunctionName, null, parentNode, null);
                AddNodeMine("FunctionType", null, myDefaultValueFunction.FunctionType, parentNode, null);
                AddNodeMine("IsForUserOption", null, myDefaultValueFunction.IsForUserOption, parentNode, null);
                AddNodeMine("ObjectType", null, myDefaultValueFunction.ObjectType, parentNode, null);
                AddNodeMine("ParameterTypes", null, myDefaultValueFunction.ParameterTypes, parentNode, null);
                AddNodeMine("PropertyName", null, myDefaultValueFunction.PropertyName, parentNode, null);
                AddNodeMine("UseCustomCode", null, myDefaultValueFunction.UseCustomCode, parentNode, null);
            }
            return true;
        }

        private TreeListNode AddNodeBoth(string name, object theirValue, object myValue, TreeListNode parentNode, object tag)
        {
            TreeListNode node = treeListBoth.AppendNode(new object[] { name, theirValue, "", myValue }, parentNode);
            node.Tag = tag;
            node.StateImageIndex = (int)TreeNodeImages.Unchecked;
            return node;
        }

        private TreeListNode AddNodeTheirs(string name, object theirValue, object myValue, TreeListNode parentNode, object tag)
        {
            TreeListNode node = treeListTheirs.AppendNode(new object[] { name }, parentNode);
            node.Tag = tag;
            node.StateImageIndex = (int)TreeNodeImages.Unchecked;
            return node;
        }
        private TreeListNode AddNodeMine(string name, object theirValue, object myValue, TreeListNode parentNode, object tag)
        {
            TreeListNode node = treeListMine.AppendNode(new object[] { name }, parentNode);
            node.Tag = tag;
            node.StateImageIndex = (int)TreeNodeImages.Unchecked;
            return node;
        }

        public override void OnDisplaying()
        {
            Populate();
        }

        private void treeList1_CustomNodeCellEdit(object sender, DevExpress.XtraTreeList.GetCustomNodeCellEditEventArgs e)
        {
            if (e.Column.AbsoluteIndex == 2)
            {
                object theirVal = e.Node.GetValue(1);
                object myVal = e.Node.GetValue(3);

                if (theirVal == null && myVal == null)
                {
                    e.RepositoryItem = repositoryItemTextEdit;
                    return;
                }
                Type type = theirVal != null ? theirVal.GetType() : myVal.GetType();

                if (theirVal != null && myVal == null)
                {
                    e.RepositoryItem = repositoryItemButtonEdit1;
                    return;
                }
                else if (theirVal == null)
                {
                    e.RepositoryItem = repositoryItemTextEdit;
                    return;
                }
                else if (type == typeof(string))
                {
                    string theirs = (string)theirVal;
                    string mine = (string)myVal;

                    if (Slyce.Common.Utility.StringsAreEqual(theirs, mine, true))
                    {
                        e.RepositoryItem = repositoryItemTextEdit;
                    }
                    else
                    {
                        e.RepositoryItem = repositoryItemButtonEdit1;
                    }
                }
                else if (type == typeof(bool))
                {
                    bool theirs = (bool)theirVal;
                    bool mine = (bool)myVal;

                    if (mine == theirs)
                    {
                        e.RepositoryItem = repositoryItemTextEdit;
                    }
                    else
                    {
                        e.RepositoryItem = repositoryItemButtonEdit1;
                    }
                }
            }
        }

        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (treeListBoth.FocusedColumn.AbsoluteIndex == 2)
            {
                object theirVal = treeListBoth.FocusedNode.GetValue(1);
                object myVal = treeListBoth.FocusedNode.GetValue(3);
                Type type = theirVal != null ? theirVal.GetType() : myVal.GetType();

                if (theirVal == null)
                {
                    e.Cancel = true;
                    return;
                }
                else if (type == typeof(string))
                {
                    string theirs = (string)theirVal;
                    string mine = (string)myVal;

                    if (Slyce.Common.Utility.StringsAreEqual(theirs, mine, true))
                    {
                        e.Cancel = true;
                    }
                }
                else if (type == typeof(bool))
                {
                    bool theirs = (bool)theirVal;
                    bool mine = (bool)myVal;

                    if (theirs == mine)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void ToggleNodeState(TreeListNode node)
        {
            if (node.StateImageIndex == (int)TreeNodeImages.Checked)
            {
                node.StateImageIndex = (int)TreeNodeImages.Unchecked;
            }
            else
            {
                node.StateImageIndex = (int)TreeNodeImages.Checked;
            }
        }

        private void treeListTheirs_StateImageClick(object sender, DevExpress.XtraTreeList.NodeClickEventArgs e)
        {
            ToggleNodeState(e.Node);
        }

        private void treeListMine_StateImageClick(object sender, DevExpress.XtraTreeList.NodeClickEventArgs e)
        {
            ToggleNodeState(e.Node);
        }

        private void buttonImportSelected_Click(object sender, EventArgs e)
        {
            ProcessNodes(true, ActionTypes.Import);
        }

        private void buttonImportAll_Click(object sender, EventArgs e)
        {
            ProcessNodes(false, ActionTypes.Import);
        }

        private void buttonRemoveSelected_Click(object sender, EventArgs e)
        {
            ProcessNodes(true, ActionTypes.Remove);
        }

        private void buttonRemoveAll_Click(object sender, EventArgs e)
        {
            ProcessNodes(false, ActionTypes.Remove);
        }

        private void ProcessNodes(bool selectedOnly, ActionTypes actionType)
        {
            DevExpress.XtraTreeList.TreeList treelist = null;

            switch (actionType)
            {
                case ActionTypes.Import:
                    treelist = treeListTheirs;
                    break;
                case ActionTypes.Remove:
                    treelist = treeListMine;
                    break;
                default:
                    throw new NotImplementedException("ActionType not handled yet: " + actionType.ToString());
            }
            int checkedImage = (int)TreeNodeImages.Checked;

            for (int i = treelist.Nodes.Count - 1; i >= 0; i--)
            {
                TreeListNode topNode = treelist.Nodes[i];

                for (int childIndex = topNode.Nodes.Count - 1; childIndex >= 0; childIndex--)
                {
                    TreeListNode node = topNode.Nodes[i];

                    if (!selectedOnly || node.StateImageIndex == checkedImage)
                    {
                        switch (actionType)
                        {
                            case ActionTypes.Import:
                                frmTemplateSyncWizard.MyProject.UserOptions.Add((Project.UserOption)node.Tag);
                                break;
                            case ActionTypes.Remove:
                                frmTemplateSyncWizard.MyProject.UserOptions.Remove((Project.UserOption)node.Tag);
                                break;
                            default:
                                throw new NotImplementedException("ActionType not handled yet: " + actionType.ToString());
                        }
                        treelist.Nodes.Remove(node);
                    }
                }
                if (topNode.Nodes.Count == 0)
                {
                    treelist.Nodes.Remove(topNode);
                }
            }
        }


    }
}
