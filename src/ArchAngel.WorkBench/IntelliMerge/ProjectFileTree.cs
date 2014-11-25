using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;
using Slyce.Common;
using Slyce.IntelliMerge;
using Slyce.IntelliMerge.Controller;
using Slyce.IntelliMerge.Controller.ManifestWorkers;

namespace ArchAngel.Workbench.IntelliMerge
{
    /// <summary>
    /// Represents the root of a ProjectFileTree. Contains a list of all the nodes in the tree. Should not
    /// be visualised as an actual node.
    /// </summary>
    public class ProjectFileTree : ProjectFileTreeNode
    {
        /// <summary>
        /// Cache of tree nodes, keyed to their path.
        /// </summary>
        private readonly Dictionary<string, ProjectFileTreeNode> treeNodeCache = new Dictionary<string, ProjectFileTreeNode>();
        /// <summary>
        /// True if the tree is undergoing massive change. Used to disable event spam, although setting it to false
        /// will trigger a full tree update.
        /// </summary>
        private bool treeRestructuring;
        /// <summary>
        /// A List of all the nodes in the tree.
        /// </summary>
        protected readonly List<ProjectFileTreeNode> allNodes = new List<ProjectFileTreeNode>();
        /// <summary>
        /// This event is fired when the tree changes in any way. For instance, if a node gets diff'd,
        /// this will fire an event with that node as part of the event args. If a bunch of nodes are 
        /// added, it will be called, but with the parent of those new nodes and the ChildrenChanged property
        /// set to true. If ChildrenChanged is true, the event handler should traverse the tree downwards and
        /// redisplay everything.
        /// </summary>
        public event EventHandler<ProjectFileTreeChangedEventArgs> TreeNodeChanged;
        /// <summary>
        /// Fired whent he tree has updated itself and needs to be reanalysed.
        /// </summary>
        public event EventHandler TreeNeedsAnalysis;

        /// <summary>
        /// Constructs an empty ProjectFileTree.
        /// </summary>
        public ProjectFileTree()
        {
            status = ProjectFileStatusEnum.Folder;
            isFolder = true;
        }

        /// <summary>
        /// Always returns true.
        /// </summary>
        public override bool IsTreeRoot
        {
            get { return true; }
        }

        /// <summary>Sets the Intellimerge type on each node in the tree.</summary>
        /// <param name="intellimergeEnabledStatus">
        /// Set to true if the FileInformation object should be consulted for the intellimerge type. Set to false
        /// if Intellimerge should be disabled altogether.
        /// </param>
        public void SetIntelliMergeOnEntireTree(bool intellimergeEnabledStatus)
        {
        	TreeRestructuring = true;
            foreach (ProjectFileTreeNode node in allNodes)
            {
                if(node.AssociatedFile != null)
                {
                    node.AssociatedFile.IntelliMerge = intellimergeEnabledStatus ? IntelliMergeType.AutoDetect : IntelliMergeType.Overwrite;
                }

                if (node.Status != ProjectFileStatusEnum.Folder)
                {
                    node.Status = ProjectFileStatusEnum.UnAnalysedFile;
                }
            }
			TreeRestructuring = false;
            RaiseTreeNeedsAnalysisEvent();
        }

        /// <summary>
        /// Returns null, as the root has no parent.
        /// </summary>
        public virtual new ProjectFileTreeNode ParentNode
        {
            get { return null; }
            protected set { throw new InvalidOperationException("Cannot set the parent node on the root of the tree."); }
        }

        /// <summary>
        /// Returns null, as this is the root of the tree and we don't want tree walkers getting into an infinite loop.
        /// </summary>
        public virtual new ProjectFileTree ParentTree
        {
            get { return null; }
            protected set { throw new InvalidOperationException("Cannot set the parent tree on the root of the tree."); }
        }

        /// <summary>
        /// A read only collection of all the nodes in the tree.
        /// </summary>
        public ReadOnlyCollection<ProjectFileTreeNode> AllNodes
        {
            get { return new ReadOnlyCollection<ProjectFileTreeNode>(allNodes); }
        }

        /// <summary>
        /// Gets the node with the specified path. If a node does not exist with that path,
        /// it returns null.
        /// </summary>
        /// <param name="path">The Path of the node to find.</param>
        /// <returns>the node with the specified path. If a node does not exist with that path,
        /// it returns null</returns>
        public ProjectFileTreeNode GetNodeAtPath(string path)
        {
			if (path == null) return null;
			ProjectFileTreeNode node;
            return treeNodeCache.TryGetValue(path, out node) ? node : null;
        }

        /// <summary>
        /// True if the tree is undergoing massive change. Used to disable event spam, although setting it to false
        /// will trigger a full tree update.
        /// </summary>
        public bool TreeRestructuring
        {
            get { return treeRestructuring; }
            set
            {
            	bool prevValue = treeRestructuring;
                treeRestructuring = value;
                if(value == false && prevValue)
                {
                    RaiseNodeChangedEvent(this, true);
                }
            }
        }

        /// <summary>
        /// Gets a list of files that have changed in the tree.
        /// </summary>
        /// <returns>A list of files that have changed in the tree.</returns>
        public IEnumerable<ProjectFileTreeNode> GetChangedFiles()
        {
            List<ProjectFileTreeNode> nodes = new List<ProjectFileTreeNode>();
            foreach(ProjectFileTreeNode node in allNodes)
            {
                if (node.AssociatedFile == null) continue;
                if (node.AssociatedFile.MergedFileExists) nodes.Add(node);
            }

            return nodes.AsReadOnly();
        }

        /// <summary>
        /// Adds the node to the list of nodes contained in this tree.
        /// </summary>
        /// <param name="node">The node that has been added to the tree.</param>
        protected override void AddNode(ProjectFileTreeNode node)
        {
            allNodes.Add(node);
            treeNodeCache[node.Path] = node;
        }

        /// <summary>
        /// Creates a new node and adds it as a child.
        /// </summary>
        /// <param name="associatedFileInformation">The IFileInformation to associated with the node. Can be null if it needs to be set later.</param>
        /// <param name="nodeText">The text of the node. Can be null if it needs to be set later.</param>
        /// <returns>The created node.</returns>
        public override ProjectFileTreeNode AddChildNode(IFileInformation associatedFileInformation, String nodeText)
        {
            if (nodeText == null)
                nodeText = "";

            ProjectFileTreeNode node = new ProjectFileTreeNode(this, this);
            node.Text = nodeText;
            node.AssociatedFile = associatedFileInformation;
            childNodes.Add(node);
            AddNode(node);
            status = ProjectFileStatusEnum.Folder;
            RaiseNodeChangedEvent(this, true);
            return node;
        }

        /// <summary>
        /// Raise the TreeNodeChanged event.
        /// </summary>
        /// <param name="childrenChanged">Whether the children of this node also changed.</param>
        /// <param name="node">The node that changed.</param>
        internal override void RaiseNodeChangedEvent(ProjectFileTreeNode node, bool childrenChanged)
        {
            if(TreeNodeChanged != null && treeRestructuring == false)
            {
                TreeNodeChanged(this, new ProjectFileTreeChangedEventArgs(node, childrenChanged));
            }
        }

        /// <summary>
        /// Raise the TreeNeedsAnalysis event.
        /// </summary>
        internal void RaiseTreeNeedsAnalysisEvent()
        {
            if (TreeNodeChanged != null)
            {
                TreeNeedsAnalysis(this, new EventArgs());
            }
        }

        ///<summary>
        /// Load the prevgen files, using the given controller to get the path to load them from.
        ///</summary>
        ///<param name="controller"></param>
        public void LoadPrevGenFiles(IController controller)
        {
            PrevGenUtility utility = new PrevGenUtility();
            string stagingFolder = controller.GetTempFilePathForComponent(ComponentKey.SlyceMergePrevGen);
            if (Directory.Exists(stagingFolder))
            {
                Utility.DeleteDirectoryBrute(stagingFolder);
            }
            if(Directory.GetDirectories(controller.ProjectSettings.ProjectPath, ".ArchAngel", SearchOption.AllDirectories).Length != 0)
            {
                // New Way
                utility.CopyUserPrevGenFiles(controller.ProjectSettings.ProjectPath,
                                             stagingFolder,
                                             controller.ProjectSettings.ProjectGuid);
                LoadPreviousVersionMD5s(controller);
            }
            else if(Directory.GetFiles(controller.ProjectSettings.ProjectPath, "*.aaz", SearchOption.AllDirectories).Length != 0)
            {
                // Old way
                utility.CopyPrevGenFiles_AAZ(controller.ProjectSettings.ProjectPath, stagingFolder);
                controller.AAZFound = true;
            }

            foreach (ProjectFileTreeNode node in allNodes)
            {
                if(node.IsFolder == false)
                {
                    node.AssociatedFile.LoadPrevGenFile(controller.GetTempFilePathForComponent(ComponentKey.SlyceMergePrevGen));
                }
            }
        }

        public void LoadUserFiles(IController controller)
        {
            foreach (ProjectFileTreeNode node in allNodes)
            {
            	if (node.IsFolder) continue;

            	node.AssociatedFile.LoadUserFile(controller.ProjectSettings.ProjectPath);
            }
        }

        public void LoadPreviousVersionMD5s(IController controller)
        {
            string rootDir = controller.GetTempFilePathForComponent(ComponentKey.SlyceMergePrevGen);
            LoadMD5sForNode(this, rootDir);
        }

        private static void LoadMD5sForNode(ProjectFileTreeNode node, string dir)
        {
            if (node.IsFolder == false)
                return;
            string manifestFile = System.IO.Path.Combine(dir, ManifestConstants.MANIFEST_FILENAME);
            
            if(File.Exists(manifestFile) == false)
            {
                return;
            }

            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(manifestFile);
            } catch (Exception)
            {
                return;
            }

            foreach(ProjectFileTreeNode childNode in node.ChildNodes)
            {
                if(childNode.AssociatedFile != null)
                {
                    string userMD5, templateMD5, prevgenMD5;
                    PrevGenUtility.GetMD5sForFile(doc, System.IO.Path.GetFileName(childNode.Path), out prevgenMD5,
                                                  out templateMD5, out userMD5);
                    childNode.AssociatedFile.SetPreviousVersionMD5s(prevgenMD5, templateMD5, userMD5);   
                }
                else if (childNode.IsFolder)
                {
                    LoadMD5sForNode(childNode, System.IO.Path.Combine(dir, childNode.Text));
                }
            }
        }

        /// <summary>
        /// Reset the previous version MD5s to String.Empty
        /// </summary>
        public void ResetMD5s()
        {
            foreach(ProjectFileTreeNode node in AllNodes)
            {
                if(node.AssociatedFile != null)
                {
                    node.AssociatedFile.SetPreviousVersionMD5s("", "", "");
                }
            }
        }

		/// <summary>
		/// Creates an XmlDocument describing the NodeSelected property of each of the child nodes of the tree
		/// </summary>
		/// <returns>The XmlDocument created.</returns>
		public XmlDocument WriteIntelliMergeAndEnableStatusToXml()
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlNode xmlNodeOption = xmlDocument.AppendChild(xmlDocument.CreateElement("Options"));
			
			XmlNode xmlNodeCheckedFiles = xmlNodeOption.AppendChild(xmlDocument.CreateElement("CheckedFiles"));
			XmlAttribute xmlAttrVersion = xmlNodeOption.Attributes.Append(xmlDocument.CreateAttribute("version"));
			xmlAttrVersion.Value = "1";

			foreach (ProjectFileTreeNode node in allNodes)
			{
				XmlNode xmlNodeVariable = xmlNodeCheckedFiles.AppendChild(xmlDocument.CreateElement("File"));
				xmlNodeVariable.InnerText = node.Path;
				XmlAttribute attr = xmlDocument.CreateAttribute("checked");
				attr.Value = node.NodeSelected.ToString();
				xmlNodeVariable.Attributes.Append(attr);

				if (node.AssociatedFile != null)
				{
					attr = xmlDocument.CreateAttribute("intellimerge");
					attr.Value = node.AssociatedFile.IntelliMerge.ToString();
					xmlNodeVariable.Attributes.Append(attr);
				}
				
			}
			return xmlDocument;
		}

		public void LoadCheckedStatusFromXml(XmlDocument xmlDocument)
		{
			XmlNodeList nodes = xmlDocument.GetElementsByTagName("Options");
			if(nodes.Count != 1)
				throw new ArgumentException("The given XmlDocument is not a valid CheckedStatus file.");
			
			XmlElement rootNode = nodes[0] as XmlElement;
			if(rootNode == null)
				throw new ArgumentException("The given XmlDocument is not a valid CheckedStatus file.");

			nodes = rootNode.GetElementsByTagName("CheckedFiles");
			if(nodes.Count != 1)
				throw new ArgumentException("The given XmlDocument is not a valid CheckedStatus file.");

			XmlElement checkedFilesNode = nodes[0] as XmlElement;
			if(checkedFilesNode == null)
				throw new ArgumentException("The given XmlDocument is not a valid CheckedStatus file.");

			nodes = checkedFilesNode.GetElementsByTagName("File");

			TreeRestructuring = true;

			foreach(XmlNode node in nodes)
			{
				string relativePath = node.InnerText;

				ProjectFileTreeNode treeNode = GetNodeAtPath(relativePath);
				if (treeNode == null) continue;

				bool checkedStatus;
				try
				{
					checkedStatus = bool.Parse(node.Attributes["checked"].Value);
				}
				catch
				{
					checkedStatus = true;
				}
				treeNode.SetSingleNodeSelected(checkedStatus);

				if (treeNode.AssociatedFile == null)
					continue;
				
				if (node.Attributes["intellimerge"] != null)
				{
					if (rootNode.Attributes["version"] == null)
					{
						bool overwriteStatus;
						try
						{
							overwriteStatus = bool.Parse(node.Attributes["intellimerge"].Value);
						}
						catch
						{
							overwriteStatus = true;
						}

						treeNode.AssociatedFile.IntelliMerge = overwriteStatus
						                                       	? IntelliMergeType.Overwrite
						                                       	: IntelliMergeType.AutoDetect;
					}
					else
					{
						int version = int.Parse(rootNode.Attributes["version"].Value);

						if (version == 1 && node.Attributes["intellimerge"] != null)
						{
							IntelliMergeType mergeType =
								(IntelliMergeType) Enum.Parse(typeof (IntelliMergeType), node.Attributes["intellimerge"].Value);
							treeNode.AssociatedFile.IntelliMerge = mergeType;
						}
					}
				}
			}

			TreeRestructuring = false;
		}

		internal bool AllNodesAnalysed()
		{
			foreach(ProjectFileTreeNode node in allNodes)
			{
				if(node.IsFolder)
					continue;
				if(node.Status == ProjectFileStatusEnum.UnAnalysedFile)
					return false;
				if(node.Status == ProjectFileStatusEnum.Busy)
					return false;
			}
			return true;
		}

		internal bool AllChangesResolved()
		{
			foreach (ProjectFileTreeNode node in allNodes)
			{
				if (node.IsFolder)
					continue;
                if (node.NodeSelected == false)
                    continue;
				if (node.Status == ProjectFileStatusEnum.UnAnalysedFile)
					return false;
				if (node.Status == ProjectFileStatusEnum.Busy)
					return false;
				if (node.AssociatedFile == null) continue;

				if (node.AssociatedFile.IntelliMerge == IntelliMergeType.NotSet)
					return false;
				if (node.Status == ProjectFileStatusEnum.AnalysisError || node.Status == ProjectFileStatusEnum.GenerationError || node.Status == ProjectFileStatusEnum.MergeError)
				{
					if (node.NodeSelected)
						return false;
					continue;
				}
				// If overwrite mode is turned on, we ignore the diff type.
				if (node.AssociatedFile.CurrentDiffResult.DiffType == TypeOfDiff.Conflict && node.AssociatedFile.IntelliMerge != IntelliMergeType.Overwrite)
				{
					return false;
				}
			}
			return true;
		}

        internal void Clear()
        {
            allNodes.Clear();
            childNodes.Clear();
        }

        public ProjectFileTreeNode DepthFirstSearch(ProjectFileSearchCondition condition)
        {
            return DepthFirstSearch(this, condition);
        }

        /// <summary>
        /// Does a depth first search and returns the first node that satisfies the SearchCondition.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static ProjectFileTreeNode DepthFirstSearch(ProjectFileTreeNode node, ProjectFileSearchCondition condition)
        {
            if (condition.Satisfied(node))
                return node;
            foreach(ProjectFileTreeNode childNode in node.ChildNodes)
            {
                ProjectFileTreeNode result = DepthFirstSearch(childNode, condition);
                if (result != null)
                    return result;
            }
            return null;
        }
    }

    public interface ProjectFileSearchCondition
    {
        bool Satisfied(ProjectFileTreeNode node);
    }

    public class IsLeafNodeSearchCondition : ProjectFileSearchCondition
    {
        public bool Satisfied(ProjectFileTreeNode node)
        {
            return node.IsFolder == false && node.ChildNodes.Count == 0;
        }
    }

    /// <summary>
    /// Contains information about the change in the source tree.
    /// </summary>
    public class ProjectFileTreeChangedEventArgs : EventArgs
    {
        /// <summary>
        /// The node that changed.
        /// </summary>
        private ProjectFileTreeNode changedNode;
        /// <summary>
        /// Whether the children of this node also changed.
        /// </summary>
        private bool childrenChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="childrenChanged">Whether the children of this node also changed.</param>
        /// <param name="changedNode">The node that changed.</param>
        public ProjectFileTreeChangedEventArgs(ProjectFileTreeNode changedNode, bool childrenChanged)
        {
            this.childrenChanged = childrenChanged;
            this.changedNode = changedNode;
        }

        /// <summary>
        /// The node that changed.
        /// </summary>
        public ProjectFileTreeNode ChangedNode
        {
            get { return changedNode; }
            set { changedNode = value; }
        }

        /// <summary>
        /// Whether the children of this node also changed.
        /// </summary>
        public bool ChildrenChanged
        {
            get { return childrenChanged; }
            set { childrenChanged = value; }
        }
    }

    /// <summary>
    /// Represents a node in the Project's file heirarchy. Holds a reference to the IFileInformation
    /// object that it represents, if it represents a file and not a folder.
    /// </summary>
    public class ProjectFileTreeNode
    {
        /// <summary>
        /// The parent node of this node.
        /// </summary>
        protected ProjectFileTreeNode parentNode;
        /// <summary>
        /// The tree that this node belongs to.
        /// </summary>
        protected ProjectFileTree parentTree;
        /// <summary>
        /// The child nodes of this node.
        /// </summary>
        protected readonly List<ProjectFileTreeNode> childNodes = new List<ProjectFileTreeNode>();
        /// <summary>
        /// The status of the file. By default it is an UnAnalysedFile. If you want to change this default,
        /// you need to set it in your constructor.
        /// </summary>
        protected ProjectFileStatusEnum status = ProjectFileStatusEnum.UnAnalysedFile;
        /// <summary>
        /// The IFileInformation object this node represents the project heirarchy. Is null if
        /// this is a folder.
        /// </summary>
        protected IFileInformation associatedFile;
        /// <summary>The display text of the node.</summary>
        private string text;
        /// <summary>
        /// True if the node is currently selected.
        /// </summary>
        protected bool nodeSelected = true;
        /// <summary>
        /// True if the node is a folder.
        /// </summary>
        protected bool isFolder;
        /// <summary>
        /// The GenerationError that occurred when creating this file, if there is one.
        /// </summary>
        protected GenerationError generationError;

    	/// <summary>
    	/// The mergeError that occurred when creating this file, if there is one.
    	/// </summary>
    	protected MergeError mergeError;

        /// <summary>
        /// Provided for inheriting classes, so that they can 
        /// </summary>
        protected ProjectFileTreeNode() { }

        internal ProjectFileTreeNode(ProjectFileTreeNode parentNode, ProjectFileTree parentTree)
        {
            this.parentNode = parentNode;
            this.parentTree = parentTree;
        }

        /// <summary>
        /// Returns the child nodes of this node. Returns an empty list if this node has no children.
        /// </summary>
        public ReadOnlyCollection<ProjectFileTreeNode> ChildNodes
        {
            get
            {
                return childNodes.AsReadOnly();
            }
        }

        /// <summary>
        /// The parent node of this node. Null if this node is the root of the tree.
        /// </summary>
        public virtual ProjectFileTreeNode ParentNode
        {
            get { return parentNode; }
            protected set { parentNode = value; }
        }

        /// <summary>
        /// The ProjectFileTree that this node belongs to. Null if this node is the root of the tree.
        /// </summary>
        public virtual ProjectFileTree ParentTree
        {
            get { return parentTree; }
            protected set { parentTree = value; }
        }

        /// <summary>
        /// The IFileInformation object this node represents the project heirarchy. Is null if
        /// this is a folder.
        /// </summary>
        public IFileInformation AssociatedFile
        {
            get { return associatedFile; }
            set
            {
                associatedFile = value;
                RaiseNodeChangedEvent(this, false);
            }
        }

        /// <summary>
        /// Returns true if this node is the root of a tree.
        /// </summary>
        public virtual bool IsTreeRoot
        {
            get { return parentTree == null && parentNode == null; }
        }

        ///<summary>
        /// Returns true if the node is a folder.
        ///</summary>
        public virtual bool IsFolder
        {
            get { return isFolder; }
            set{ isFolder = value; }
        }

        /// <summary>
        /// The GenerationError that occurred when creating this file, if there is one.
        /// </summary>
        public GenerationError GenerationError
        {
            get { return generationError; }
            set 
            { 
                generationError = value;
                Status = ProjectFileStatusEnum.GenerationError;
            }
        }

		/// <summary>
		/// The MergeError that occurred when creating this file, if there is one.
		/// </summary>
		public MergeError MergeError
		{
			get { return mergeError; }
			set
			{
				mergeError = value;
				Status = ProjectFileStatusEnum.MergeError;
			}
		}

        /// <summary>
        /// Returns the status of the node. Defaults to UnanalysedFile.
        /// </summary>
        public ProjectFileStatusEnum Status
        {
            get { return status; }
            set
            {
				if (status != value)
				{
					status = value;
					RaiseNodeChangedEvent(this, false);
				}
            }
        }

        /// <summary>
        /// The display text of the node.
        /// </summary>
        public string Text
        {
            get { return text; }
            set
            {
				if (text != value)
				{
					text = value;
					RaiseNodeChangedEvent(this, false);
				}
            }
        }

        /// <summary>
        /// The path of the node in the tree.
        /// </summary>
        public string Path
        {
            get
            {
                if(parentNode != null && parentNode.IsTreeRoot == false)
                {
                    return parentNode.Path + System.IO.Path.DirectorySeparatorChar + Text;
                }
                return Text;
            }
        }



        /// <summary>
        /// Creates a new node and adds it as a child.
        /// </summary>
        /// <param name="associatedFileInformation">The IFileInformation to associated with the node. Can be null if it needs to be set later.</param>
        /// <returns>The created node.</returns>
        public virtual ProjectFileTreeNode AddChildNode(IFileInformation associatedFileInformation)
        {
            return AddChildNode(associatedFileInformation, null);
        }

        /// <summary>
        /// Creates a new node and adds it as a child.
        /// </summary>
        /// <param name="nodeText">The text of the node. Can be null if it needs to be set later.</param>
        /// <returns>The created node.</returns>
        public virtual ProjectFileTreeNode AddChildNode(String nodeText)
        {
            return AddChildNode(null, nodeText);
        }

        /// <summary>
        /// Creates a new node with both its text and associated file set to null, and adds it as a child.
        /// </summary>
        /// <returns>The created node.</returns>
        public virtual ProjectFileTreeNode AddChildNode()
        {
            return AddChildNode(null, null);
        }

        /// <summary>
        /// Creates a new node and adds it as a child.
        /// </summary>
        /// <param name="associatedFileInformation">The IFileInformation to associated with the node. Can be null if it needs to be set later.</param>
        /// <param name="nodeText">The text of the node. Can be null if it needs to be set later.</param>
        /// <returns>The created node.</returns>
        public virtual ProjectFileTreeNode AddChildNode(IFileInformation associatedFileInformation, String nodeText)
        {
            if (nodeText == null)
                nodeText = "";

            ProjectFileTreeNode node = new ProjectFileTreeNode(this, parentTree);
            node.Text = nodeText;
            node.associatedFile = associatedFileInformation;
            childNodes.Add(node);
            parentTree.AddNode(node);
            status = ProjectFileStatusEnum.Folder;
            RaiseNodeChangedEvent(this, true);
            return node;
        }

        /// <summary>
        /// Override this in the Tree class to provide a method of adding nodes to a collection of all nodes.
        /// </summary>
        /// <param name="node">The node to add.</param>
        /// <exception cref="InvalidOperationException">Thrown if the base version of this method is called. It must be overridden.</exception>
		protected virtual void AddNode(ProjectFileTreeNode node)
        {
        	throw new InvalidOperationException("Cannot call this on a node, must be called on the root Tree.");
        }

		/// <summary>
		/// Sorts the child nodes so that folders come first, then children, both in Alphabetical order.
		/// </summary>
		public void SortChildren()
		{
			SortedList<string, ProjectFileTreeNode> directoryChildren = new SortedList<string, ProjectFileTreeNode>();
			SortedList<string, ProjectFileTreeNode> fileChildren = new SortedList<string, ProjectFileTreeNode>();

			foreach(var child in childNodes)
			{
				if(child.IsFolder)
					directoryChildren.Add(child.Text, child);
				else
					fileChildren.Add(child.Text, child);
			}

			childNodes.Clear();

			foreach (var child in directoryChildren.Values)
				childNodes.Add(child);

			foreach (var child in fileChildren.Values)
				childNodes.Add(child);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="childText"></param>
		/// <returns>Null if a child with taht text was not found.</returns>
		public ProjectFileTreeNode GetChildWithText(string childText)
		{
			foreach(var child in childNodes)
			{
				if (child.Text == childText) return child;
			}

			return null;
		}

    	/// <summary>
        /// Returns the same as Text.
        /// </summary>
        /// <returns>The Text property.</returns>
        public override string ToString()
        {
            return Text;
        }

        /// <summary>
        /// Changes the selection status of this node and all of its children
        /// </summary>
        public virtual bool NodeSelected
        {
            get { return nodeSelected; }
            set
            {
                nodeSelected = value;
                foreach (ProjectFileTreeNode node in childNodes)
                {
                    node.NodeSelected = value;
                }
                RaiseNodeChangedEvent(this, true);
            }
        }

        /// <summary>
        /// Sets the NodeSelected property without propogating the selection status to
        /// this nodes children.
        /// </summary>
        /// <param name="selectionStatus">True if the node is selected, false if it is not.</param>
        public virtual void SetSingleNodeSelected(bool selectionStatus)
        {
            nodeSelected = selectionStatus;
            RaiseNodeChangedEvent(this, false);
        }

        /// <summary>
        /// Raise the TreeNodeChanged event.
        /// </summary>
        /// <param name="childrenChanged">Whether the children of this node also changed.</param>
        /// <param name="node">The node that changed.</param>
        internal virtual void RaiseNodeChangedEvent(ProjectFileTreeNode node, bool childrenChanged)
        {
            if(parentTree != null)
            {
                parentTree.RaiseNodeChangedEvent(node, childrenChanged);
            }
        }

		/// <summary>
		/// Raise the TreeNodeChanged event.
		/// </summary>
		public void RaiseNodeChangedEvent()
		{
			RaiseNodeChangedEvent(this, false);
		}

        /// <summary>
        /// Recursively looks through all of its decendents and determines if any of them have an error.
        /// </summary>
        /// <returns>True if any decendent node has an error.</returns>
        public bool HasDecendentWithError()
        {
            bool result = false;
            foreach(ProjectFileTreeNode child in childNodes)
            {
                result |= child.Status == ProjectFileStatusEnum.GenerationError ||
                          child.Status == ProjectFileStatusEnum.AnalysisError;
                
                // shortcut so we don't process more nodes than we need to.
                if (result) return true;

                result |= child.HasDecendentWithError();
            }

            return result;
        }

    	public void SetIntelliMergeOnSelfAndAllChildren(IntelliMergeType detect)
    	{
    		SetIntelliMergeOnSelfAndAllChildrenInternal(detect);

    		ParentTree.RaiseTreeNeedsAnalysisEvent();
    	}

    	protected void SetIntelliMergeOnSelfAndAllChildrenInternal(IntelliMergeType detect)
    	{
			if (AssociatedFile != null)
			{
				AssociatedFile.IntelliMerge = detect;

				if (Status == ProjectFileStatusEnum.AnalysedFile)
				{
					Status = ProjectFileStatusEnum.UnAnalysedFile;
				}
			}

			foreach (ProjectFileTreeNode child in ChildNodes)
			{
				child.SetIntelliMergeOnSelfAndAllChildrenInternal(detect);
			}
    	}
    }

	/// <summary>
    /// Represents the status of a node. 
    /// </summary>
    public enum ProjectFileStatusEnum
    {
        /// <summary>
        /// The file hasn't been analysed.
        /// </summary>
        UnAnalysedFile = 0,
        /// <summary>
        /// The file has been analysed.
        /// </summary>
        AnalysedFile,
        /// <summary>
        /// A folder. Folders don't get analysed.
        /// </summary>
        Folder,
        /// <summary>
        /// An error occurred during analysis of the node.
        /// </summary>
        AnalysisError,
        /// <summary>
        /// An error occurred during generation of the node.
        /// </summary>
        GenerationError,
		/// <summary>
		/// An error occurred during merging of base constructs during diff.
		/// </summary>
		MergeError,
        /// <summary>
        /// The file is busy.
        /// </summary>
        Busy
    }
}
