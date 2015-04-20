using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.Serialization;
using ArchAngel.Providers.CodeProvider;

namespace Slyce.IntelliMerge.Controller
{
    /// <summary>
    /// Enum for representing different versions of a file.
    /// </summary>
    /// <remarks>Don't change these numbers without a good examining the 
    /// rest of the code in this part of the system. A lot of things rely on the numbers
    /// being sequential, starting from 0, and in the order they are defined.</remarks>
    public enum Version
    {
        /// <summary>
        /// User Version of the object.
        /// </summary>
        /// <remarks>Do not change this from 0. The behaviour of a lot of the classes in this file relies on
        /// User being the first object.</remarks>
        User = 0,
        /// <summary>
        /// Newly Generated Version of the object.
        /// </summary>
        NewGen = 1,
        /// <summary>
        /// Previous Version of the object.
        /// </summary>
        PrevGen = 2
    }

    internal class VersionedList<T> where T : class
    {
        private readonly T[] codeRoots = new T[3];

        public T User
        {
			[DebuggerStepThrough]
            get { return codeRoots[(int)Version.User]; }
            set { codeRoots[(int)Version.User] = value; }
        }

        public T NewGen
        {
			[DebuggerStepThrough]
            get { return codeRoots[(int)Version.NewGen]; }
            set { codeRoots[(int)Version.NewGen] = value; }
        }

        public T PrevGen
        {
			[DebuggerStepThrough]
            get { return codeRoots[(int)Version.PrevGen]; }
            set { codeRoots[(int)Version.PrevGen] = value; }
        }

        /// <summary>
        /// The first non-null object it its list, in the order User, PrevGen, NewGen. If all objects are null, returns null.
        /// </summary>
        /// <returns>The first non-null object it its list. If all objects are null, returns null.</returns>
        public T GetFirstNonNullObject()
        {
            foreach(T t in codeRoots)
            {
                if (t != null)
                    return t;
            }
            return null;
        }

        /// <summary>
        /// Returns a MissingObject enum with flags set for each constuct that is missing.
        /// </summary>
        /// <returns>A MissingObject enum with flags set for each constuct that is missing.</returns>
        public MissingObject DetermineMissingConstructs()
        {
            MissingObject missing = MissingObject.None;

            if (User == null)
            {
                missing |= MissingObject.User;
            }
            if (PrevGen == null)
            {
                missing |= MissingObject.PrevGen;
            }
            if (NewGen == null)
            {
                missing |= MissingObject.NewGen;
            }

            return missing;
        }

        public T GetObject(Version t)
        {
            return codeRoots[(int)t];
        }

        public void SetObject(T obj, Version t)
        {
            codeRoots[(int)t] = obj;
        }
    }

    public class CodeRootMap : CodeRootMapNode
    {
        private readonly List<CodeRootMapNode> allNodes = new List<CodeRootMapNode>();

        private readonly VersionedList<ICodeRoot> coderoots = new VersionedList<ICodeRoot>();

        private bool twoWayDiffWhenPrevGenMissing = false;

    	/// <summary>
        /// If set to true, the diff will perform a two way diff if the prev gen is missing.
        /// </summary>
        public bool TwoWayDiffWhenPrevGenMissing
        {
            get { return twoWayDiffWhenPrevGenMissing; }
            set { twoWayDiffWhenPrevGenMissing = value; }
        }

        /// <summary>
        /// Returns null, as the root has no parent.
        /// </summary>
        public override CodeRootMapNode ParentNode
        {
            get { return null; }
            protected set { throw new InvalidOperationException("Cannot set the parent node on the root of the tree."); }
        }

        /// <summary>
        /// Returns null, as this is the root of the tree and we don't want tree walkers getting into an infinite loop.
        /// </summary>
        public override CodeRootMap ParentTree
        {
            get { return this; }
            protected set { throw new InvalidOperationException("Cannot set the parent tree on the root of the tree."); }
        }

    	public bool ContainsCodeRoots
    	{
    		get
    		{
				return coderoots.GetFirstNonNullObject() != null;
    		}
    	}

        /// <summary>
        /// A read only collection of all the nodes in the tree.
        /// </summary>
        public ReadOnlyCollection<CodeRootMapNode> AllNodes
        {
            get { return new ReadOnlyCollection<CodeRootMapNode>(allNodes); }
        }

        public override bool IsCustomMatch
        {
            get { return false; }
        }

        /// <summary>
        /// Removes all nodes from the tree.
        /// </summary>
        internal void Clear()
        {
            allNodes.Clear();
            children.Clear();
        }

        /// <summary>
        /// Returns the combined diff type of this node and all of its children.
        /// </summary>
        /// <returns>The combined diff type of this node and all of its children.</returns>
        public override TypeOfDiff Diff()
        {
            if (coderoots.GetFirstNonNullObject() == null)
                throw new InvalidOperationException("Cannot perform a diff if no CodeRoots have been added.");
            
            TypeOfDiff diff = TypeOfDiff.ExactCopy;
            DiffTypeExcludingChildren = TypeOfDiff.ExactCopy;

            // Diff the coderoots, looking at more than a basic signature comparison but not looking at children (we have already done that above)
            if (coderoots.PrevGen == null)
            {
                if (coderoots.NewGen == null)
                {
                    return TypeOfDiff.ExactCopy;
                }
                if (coderoots.User == null)
                {
                    return TypeOfDiff.ExactCopy;
                }

                TwoWayDiffWhenPrevGenMissing = true;
            }
            else
            {
                if (coderoots.PrevGen.IsTheSame(coderoots.NewGen, ComparisonDepth.Outer) == false)
                { // If NewGen is null, this will return false.
                    DiffTypeExcludingChildren = CombineChildDiffTypes(DiffTypeExcludingChildren, TypeOfDiff.TemplateChangeOnly);
                }
                if (coderoots.PrevGen.IsTheSame(coderoots.User, ComparisonDepth.Outer) == false)
                { // If User is null, this will return false.
                    DiffTypeExcludingChildren = CombineChildDiffTypes(DiffTypeExcludingChildren, TypeOfDiff.UserChangeOnly);
                }
                if (DiffTypeExcludingChildren == TypeOfDiff.Conflict && coderoots.NewGen.IsTheSame(coderoots.User, ComparisonDepth.Outer))
                {
                    DiffTypeExcludingChildren = TypeOfDiff.UserAndTemplateChange;
                }
            }

            // Diff the children
            foreach (CodeRootMapNode node in children)
            {
                diff = CombineChildDiffTypes(diff, node.Diff());
            }

            diff = CombineChildDiffTypes(DiffTypeExcludingChildren, diff);
            TwoWayDiffWhenPrevGenMissing = false;
            return diff;
        }

        /// <summary>
        /// Returns every base construct in the tree of the type given.
        /// </summary>
        /// <param name="type">The type of base construct to look for.</param>
        /// <param name="versionToGet">The Version that we are matching against. Only objects from that version will be added.</param>
        /// <returns>A list of the base constructs in the tree with the given type.</returns>
        public ReadOnlyCollection<IBaseConstruct> GetChildrenOfType(Type type, Version versionToGet)
        {
            List<IBaseConstruct> childrenRetVal = new List<IBaseConstruct>();

            foreach(CodeRootMapNode child in AllNodes)
            {
                IBaseConstruct bc = child.GetFirstValidBaseConstruct();
                
                if (bc == null || !type.IsInstanceOfType(bc)) continue;

                if      (versionToGet == Version.NewGen   && child.NewGenObj != null) childrenRetVal.Add(child.NewGenObj);
                else if (versionToGet == Version.PrevGen  && child.PrevGenObj != null) childrenRetVal.Add(child.PrevGenObj);
                else if (versionToGet == Version.User     && child.UserObj != null) childrenRetVal.Add(child.UserObj);
            }

            return childrenRetVal.AsReadOnly();
        }

        /// <summary>
        /// Returns an IBaseConstruct which is the result of merging the BaseConstructs in this subtree.
        /// </summary>
        /// <returns>A IBaseConstruct which is the result of merging the BaseConstructs in this subtree.</returns>
        /// <exception cref="InvalidOperationException">Thrown if there are no BaseConstructs in this node 
        /// or any of its children.</exception>
        public override IBaseConstruct GetMergedBaseConstruct()
        {
            throw new InvalidOperationException("Cannot call GetMergedBaseconstruct() on the Root node. Use GetMergedCodeRoot()");
        }

        /// <summary>
        /// Always returns true.
        /// </summary>
        public override bool IsTreeRoot
        {
            get { return true; }
        }

        /// <summary>
        /// The User CodeRoot object
        /// </summary>
        public ICodeRoot UserCodeRoot
        {
            get { return coderoots.User; }
            set { coderoots.User = value; }
        }
        /// <summary>
        /// The newly generated CodeRoot object
        /// </summary>
        public ICodeRoot NewGenCodeRoot
        {
            get { return coderoots.NewGen; }
            set { coderoots.NewGen = value; }
        }

        /// <summary>
        /// The previously generated the CodeRoot object
        /// </summary>
        public ICodeRoot PrevGenCodeRoot
        {
            get { return coderoots.PrevGen; }
            set { coderoots.PrevGen = value; }
        }

        /// <summary>
        /// Generates the Merged CodeRoot object.
        /// </summary>
        /// <exception cref="InvalidOperationException">If no code roots have been added, or there is a conflict.</exception>
        /// <returns></returns>
        public ICodeRoot GetMergedCodeRoot()
        {
            ICodeRoot originalCodeRoot = coderoots.GetFirstNonNullObject();
            if (originalCodeRoot == null)
                throw new InvalidOperationException("Cannot merge code roots when none have been added");
            ICodeRoot newCodeRoot = originalCodeRoot.NewInstance();
            originalCodeRoot.ShallowCloneInto(newCodeRoot);

            foreach (CodeRootMapNode child in children)
            {
                MissingObject missingObjects = child.DetermineMissingConstructs();
                if ((missingObjects & MissingObject.User) != 0
                    && (missingObjects & MissingObject.NewGen) != 0)
                {
                    // Only PrevGen here. Ignore it.
                    continue;
                }

                newCodeRoot.AddChild(child.GetMergedBaseConstruct());
            }

            return newCodeRoot;
        }

        /// <summary>
        /// Adds a CodeRoot to the tree.
        /// </summary>
        /// <param name="codeRoot">The CodeRoot to add.</param>
        /// <param name="type">The Version of the CodeRoot to add.</param>
        public void AddCodeRoot(ICodeRoot codeRoot, Version type)
        {
            coderoots.SetObject(codeRoot, type);

            foreach (IBaseConstruct bc in codeRoot.WalkChildren())
            {
                AddBaseConstructAsChild(bc, type);
            }
        }


        /// <summary>
        /// Adds node to a collection of all nodes.
        /// </summary>
        /// <param name="node">The node to add.</param>
        protected override void AddNode(CodeRootMapNode node)
        {
            allNodes.Add(node);
        }

        /// <summary>
        /// Searches through all of the nodes in the tree and returns the first node that has
        /// a base construct that matches the one given.
        /// </summary>
        /// <param name="baseConstruct">The base construct to search for in the tree.</param>
        /// <returns>The matched node.</returns>
        public CodeRootMapNode GetNode(IBaseConstruct baseConstruct)
        {
            foreach (CodeRootMapNode node in allNodes)
            {
                if (node.IsTheSameAsAny(baseConstruct))
                    return node;
            }
            return null;
        }

        /// <summary>
        /// Searches through all of the nodes in the tree and returns the node that contains the
        /// given base construct. It uses reference equality to determine this, so if the base construct
        /// is not in the tree it will return null.
        /// </summary>
        /// <param name="baseConstruct">The base construct to search for in the tree.</param>
        /// <returns>The matched node.</returns>
        public CodeRootMapNode GetExactNode(IBaseConstruct baseConstruct)
        {
            foreach (CodeRootMapNode node in allNodes)
            {
                if (node.IsTheSameReference(baseConstruct))
                    return node;
            }
            return null;
        }

        /// <summary>
        /// Searches through all of the nodes in the tree and returns the node that contains the
        /// base construct with this unique identifier. 
        /// </summary>
        /// <param name="uniqueIdentifier">The unique identifier of the base construct to search for in the tree.</param>
        /// <param name="version">The version of the base construct that we are looking for.</param>
        /// <returns>The matched node.</returns>
        private CodeRootMapNode GetExactNode(string uniqueIdentifier, Version version)
        {
            throw new NotImplementedException();
/*
            foreach (CodeRootMapNode node in allNodes)
            {
                
            }
            return null;
*/
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userUID"></param>
        /// <param name="newgenUID"></param>
        /// <param name="prevgenUID"></param>
        /// <returns></returns>
        public virtual bool MatchConstructs(string userUID, string newgenUID, string prevgenUID)
        {
        	if (string.IsNullOrEmpty(userUID) && string.IsNullOrEmpty(newgenUID) && string.IsNullOrEmpty(prevgenUID))
				throw new ArgumentException("Cannot match 3 null objects!");

        	
			// Get Base Constructs. Make sure we can find the ones we are given.
			IBaseConstruct userBC = GetBaseConstructfromUID(userUID, UserCodeRoot);
			IBaseConstruct newgenBC = GetBaseConstructfromUID(newgenUID, NewGenCodeRoot);
        	IBaseConstruct prevgenBC = GetBaseConstructfromUID(prevgenUID, PrevGenCodeRoot);

			if (userBC == null && newgenBC == null && prevgenBC == null)
				throw new ArgumentException("Could not find any of the elements specified");

        	CodeRootMapNode parent = GetExactNode(userBC ?? newgenBC ?? prevgenBC).ParentNode;
			if((newgenBC != null && parent != GetExactNode(newgenBC).ParentNode) || 
				(prevgenBC != null && parent != GetExactNode(prevgenBC).ParentNode))
				throw new ArgumentException("The 3 nodes specified do not belong to the same parent and thus cannot be matched.");

        	MatchConstructs(parent, userBC, newgenBC, prevgenBC);

			return true;
        }

    	private static IBaseConstruct GetBaseConstructfromUID(string uniqueIdentifier, ICodeRoot codeRoot)
    	{
			if (string.IsNullOrEmpty(uniqueIdentifier))
			{
				return null;
			}

    		string[] identifiers = BaseConstructConstants.SplitFullyQualifiedIdentifier(uniqueIdentifier);
    		IBaseConstruct baseConstruct = codeRoot.FindChild(identifiers[0]);
    		if (baseConstruct == null) return null;
    		for(int i = 1; i < identifiers.Length; i++)
    		{
    			baseConstruct = baseConstruct.FindChild(identifiers[i]);
    			if (baseConstruct == null) return null;
    		}
			if (baseConstruct == null)
				throw new ArgumentException("Could not find the object by unique id.");
    		return baseConstruct;
    	}

    	/// <summary>
        /// Matches the 3 given nodes for diff and merge purposes. Does not modify the CodeRoots, just rewires the 
        /// CodeRootMap. Only works if the base constructs being matched have the same parent. They may have the same 
        /// parent if their parent has been matched with somthing else. 
        /// </summary>
        /// <param name="parentNode">The parent CodeRootMapNode of the 3 base constructs. If the BCs were functions, this would 
        /// be the CodeRootMapNode that holds their parent class.</param>
        /// <param name="userObj">The user object to map.</param>
        /// <param name="newGenObj">The template object to map.</param>
        /// <param name="prevGenObj">The prevgen object to map.</param>
        public void MatchConstructs(CodeRootMapNode parentNode, IBaseConstruct userObj, IBaseConstruct newGenObj, IBaseConstruct prevGenObj)
        {
            if (userObj != null)
            {
                CodeRootMapNode childNode = parentNode.GetChildNode(userObj);
                if (childNode == null) throw new Exception("UserObject not part of this tree");
                childNode.RemoveBaseConstruct(userObj, false);
            }
            if (newGenObj != null)
            {
                CodeRootMapNode childNode = parentNode.GetChildNode(newGenObj);
                if (childNode == null) throw new Exception("NewGen not part of this tree");
                childNode.RemoveBaseConstruct(newGenObj, false);
            }
            if (prevGenObj != null)
            {
                CodeRootMapNode childNode = parentNode.GetChildNode(prevGenObj);
                if (childNode == null) throw new Exception("PrevGen not part of this tree");
                childNode.RemoveBaseConstruct(prevGenObj, false);
            }

            CleanUpTree();

            CodeRootMapNode newNode = new CodeRootMapNode(parentNode.ParentTree, parentNode);
			newNode.IsCustomMatch = true;
            if (userObj != null)
            {
                newNode.AddBaseConstruct(userObj, Version.User);
            }
            if (newGenObj != null)
            {
                newNode.AddBaseConstruct(newGenObj, Version.NewGen);
            }
            if (prevGenObj != null)
            {
                newNode.AddBaseConstruct(prevGenObj, Version.PrevGen);
            }
        }

        internal void RemoveChildFromTree(CodeRootMapNode node)
        {
            allNodes.Remove(node);
            
            List<CodeRootMapNode> nodesToRemove = new List<CodeRootMapNode>();
            foreach(CodeRootMapNode child in node.ChildNodes)
            {
                nodesToRemove.Add(child);
            }

            node.ParentNode.RemoveChild(node);
            foreach(CodeRootMapNode nodeToRemove in nodesToRemove)
            {
                RemoveChildFromTree(nodeToRemove);
            }
        }
   
        internal void CleanUpTree()
        {
            List<CodeRootMapNode> nodesToRemove = new List<CodeRootMapNode>();
            foreach (CodeRootMapNode node in allNodes)
            {
                if(node.GetFirstValidBaseConstruct() == null)
                {
                    nodesToRemove.Add(node);
                }
            }

            foreach(CodeRootMapNode node in nodesToRemove)
            {
                node.ParentNode.RemoveChild(node);
                allNodes.Remove(node);
            }
        }

		/// <summary>
		/// Gets a list of nodes that have custom matches. It is guaranteed
		/// that any custom matched child nodes will come after their ancestors,
		/// if any of their ancestors are custom matched.
		/// </summary>
		/// <returns></returns>
    	public IList<CodeRootMapNode> GetCustomMatchedNodes()
    	{
    		List<CodeRootMapNode> nodes = new List<CodeRootMapNode>();
			Queue<CodeRootMapNode> nodesToSearch = new Queue<CodeRootMapNode>(ChildNodes);
			// Do a breadth first search of all of the nodes in the tree.
			while(nodesToSearch.Count > 0)
			{
				CodeRootMapNode node = nodesToSearch.Dequeue();
				
				if (node.IsCustomMatch) nodes.Add(node);

				foreach(CodeRootMapNode child in node.ChildNodes)
					nodesToSearch.Enqueue(child);
			}
    		return nodes;
    	}
    }

    public class CodeRootMapNode
    {
        private readonly VersionedList<IBaseConstruct> baseConstructs = new VersionedList<IBaseConstruct>();
        private IBaseConstruct mergedObj;
        private bool omit = false;

        protected readonly List<CodeRootMapNode> children = new List<CodeRootMapNode>();
        protected CodeRootMapNode parentCRMNode = null;
        protected CodeRootMap parentCRMTree = null;
        protected bool customMatch = false;

        protected TypeOfDiff diffTypeExcChildren;

        protected CodeRootMapNode()
        {
        }

        public CodeRootMapNode(CodeRootMap parentTree, CodeRootMapNode parentNode)
        {
            parentCRMTree = parentTree;
            parentCRMNode = parentNode;
            parentCRMNode.children.Add(this);

            parentCRMTree.AddNode(this);
        }

        /// <summary>
        /// The parent node of this node. Null if this node is the root of the tree.
        /// </summary>
        public virtual CodeRootMapNode ParentNode
        {
            get { return parentCRMNode; }
            protected set { parentCRMNode = value; }
        }

        /// <summary>
        /// The ProjectFileTree that this node belongs to. Null if this node is the root of the tree.
        /// </summary>
        public virtual CodeRootMap ParentTree
        {
            get { return parentCRMTree; }
            protected set { parentCRMTree = value; }
        }

        /// <summary>
        /// Returns true if this node is the root of its tree.
        /// </summary>
        public virtual bool IsTreeRoot
        {
            get { return false; }
        }

        public virtual bool IsCustomMatch
        {
            get { return customMatch; }
			internal set { customMatch = value; }
        }

        /// <summary>
        /// Override this in the Tree class to provide a method of adding nodes to a collection of all nodes.
        /// </summary>
        /// <param name="node">The node to add.</param>
        /// <exception cref="InvalidOperationException">Thrown if the base version of this method is called. It must be overridden.</exception>
        protected virtual void AddNode(CodeRootMapNode node)
        {
            throw new InvalidOperationException("Cannot call this on a node, must be called on the root Tree.");
        }

        /// <summary>
        /// Returns the child nodes of this node. Returns an empty list if this node has no children.
        /// </summary>
        public ReadOnlyCollection<CodeRootMapNode> ChildNodes
        {
            get
            {
                return new ReadOnlyCollection<CodeRootMapNode>(children);
            }
        }

        internal void RemoveChild(CodeRootMapNode node)
        {
            children.Remove(node);
            node.ParentNode = null;
        }

        internal void RemoveBaseConstruct(IBaseConstruct bc, bool cleanUpTree)
        {
            if (bc == null) return;

            if (UserObj == bc)
                UserObj = null;
            else if (NewGenObj == bc)
                NewGenObj = null;
            else if (PrevGenObj == bc)
                PrevGenObj = null;
            else return;

            foreach(IBaseConstruct child in bc.WalkChildren())
            {
                GetChildNode(child).RemoveBaseConstruct(child, false);
            }

            if(cleanUpTree)
            {
                ParentTree.CleanUpTree();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="baseConstruct">The IBaseConstruct to find.</param>
        /// <returns>
        /// Finds the given IBaseConstruct in this node's children, and returns the node it belongs to. If 
        /// it does not exist in any of this nodes children, returns null.
        /// </returns>
        public virtual CodeRootMapNode GetChildNode(IBaseConstruct baseConstruct)
        {
            foreach(CodeRootMapNode child in children)
            {
                if(child.IsTheSameReference(baseConstruct))
                {
                    return child;
                }
            }

            return null;
        }

        public bool ContainsBaseConstruct
        {
            get { return baseConstructs.GetFirstNonNullObject() != null; }
        }

        public IBaseConstruct GetFirstValidBaseConstruct()
        {
            return baseConstructs.GetFirstNonNullObject();
        }

        public IBaseConstruct UserObj
        {
            get { return baseConstructs.User; }
            set { baseConstructs.User = value; }
        }

        public IBaseConstruct NewGenObj
        {
            get { return baseConstructs.NewGen; }
            set { baseConstructs.NewGen = value; }
        }

        public IBaseConstruct PrevGenObj
        {
            get { return baseConstructs.PrevGen; }
            set { baseConstructs.PrevGen = value; }
        }

        public void AddBaseConstructAsChild(IBaseConstruct bc, Version t)
        {
            foreach(CodeRootMapNode child in children)
            {
                if(child.IsTheSame(bc))
                {
                    child.AddBaseConstruct(bc, t);
                    return;
                }
            }

            AddBaseConstructAsNewChild(bc, t);
        }

        public ReadOnlyCollection<IBaseConstruct> GetSiblingsOfSameType(Version versionToGet)
        {
            List<IBaseConstruct> siblings = new List<IBaseConstruct>();
			
			foreach(CodeRootMapNode child in ParentNode.ChildNodes)
			{
				if(child == this)
					continue;
				IBaseConstruct potentialMatch = child.baseConstructs.GetObject(versionToGet);
				if (potentialMatch != null && potentialMatch.GetType().Equals(GetFirstValidBaseConstruct().GetType()))
					siblings.Add(potentialMatch);
			}

            return siblings.AsReadOnly();
        }

        /// <summary>
        /// Adds the Base Construct to this node, and processes its children to add them too.
        /// </summary>
        /// <param name="bc"></param>
        /// <param name="t"></param>
        public CodeRootMapNode AddBaseConstructAsNewChild(IBaseConstruct bc, Version t)
        {
            if (bc == null) return null;
            CodeRootMapNode node = new CodeRootMapNode(ParentTree, this);
            node.SetBaseConstruct(bc, t);
            
            foreach (IBaseConstruct child in bc.WalkChildren())
            {
                node.AddBaseConstructAsNewChild(child, t);
            }

            return node;
        }

        internal void AddBaseConstruct(IBaseConstruct bc, Version t)
        {
            baseConstructs.SetObject(bc, t);

            foreach(IBaseConstruct bcChild in bc.WalkChildren())
            {
                bool found = false;

                foreach(CodeRootMapNode crChild in children)
                {
                	if (!crChild.IsTheSame(bcChild)) continue;

                	crChild.AddBaseConstruct(bcChild, t);
                	found = true;
                	break;
                }
                if (!found)
                {
                    AddBaseConstructAsNewChild(bcChild, t);    
                }
            }
        }

        /// <summary>
        /// Sets the BaseConstruct specified by the Version parameter.
        /// </summary>
        /// <param name="bc"></param>
        /// <param name="t"></param>
        public void SetBaseConstruct(IBaseConstruct bc, Version t)
        {
            baseConstructs.SetObject(bc, t);
        }

        private bool IsTheSame(IBaseConstruct otherbc)
        {
            IBaseConstruct thisbc = baseConstructs.GetFirstNonNullObject();
            if(thisbc == null || otherbc == null)
            {
                return false;
            }

            return thisbc.IsTheSame(otherbc);
        }

        public bool IsTheSameReference(IBaseConstruct otherbc)
        {
            if (otherbc == null)
            {
                return false;
            }

            if (ReferenceEquals(baseConstructs.NewGen, otherbc))
            {
                return true;
            }

            if (ReferenceEquals(baseConstructs.PrevGen, otherbc))
            {
                return true;
            }
            if (ReferenceEquals(baseConstructs.User, otherbc))
            {
                return true;
            }

            return false;
        }

        public bool IsTheSameAsAny(IBaseConstruct otherbc)
        {
            if(otherbc == null)
            {
                return false;
            }

            IBaseConstruct thisbc = baseConstructs.NewGen;
            if (thisbc != null && thisbc.IsTheSame(otherbc))
            {
                return true;
            }

            thisbc = baseConstructs.PrevGen;
            if (thisbc != null && thisbc.IsTheSame(otherbc))
            {
                return true;
            }

            thisbc = baseConstructs.User;
            if (thisbc != null && thisbc.IsTheSame(otherbc))
            {
                return true;
            }

            return false;
        }

        public MissingObject DetermineMissingConstructs()
        {
            return baseConstructs.DetermineMissingConstructs();
        }

        public IBaseConstruct MergedObj
        {
            get { return mergedObj; }
            set { mergedObj = value; }
        }

        public bool Omit
        {
            get { return omit; }
            set { omit = value; }
        }

        public virtual TypeOfDiff Diff()
        {
            TypeOfDiff diff = TypeOfDiff.ExactCopy;
            DiffTypeExcludingChildren = TypeOfDiff.ExactCopy;

            if (MergedObj != null)
                DiffTypeExcludingChildren = TypeOfDiff.ExactCopy;
			else if (baseConstructs.PrevGen == null)
            {
                if (baseConstructs.NewGen == null && baseConstructs.User == null)
                {
                    DiffTypeExcludingChildren =
                        CombineChildDiffTypes(diffTypeExcChildren, TypeOfDiff.Warning);
                }
				else if(baseConstructs.NewGen == null)
				{
					DiffTypeExcludingChildren =
						CombineChildDiffTypes(diffTypeExcChildren, TypeOfDiff.UserChangeOnly);
				}
				else if (baseConstructs.User == null)
				{
					DiffTypeExcludingChildren =
						CombineChildDiffTypes(diffTypeExcChildren, TypeOfDiff.TemplateChangeOnly);
				}
                else if(baseConstructs.User.IsTheSame(baseConstructs.NewGen, ComparisonDepth.Outer))
                {
                    DiffTypeExcludingChildren = CombineChildDiffTypes(diffTypeExcChildren, TypeOfDiff.ExactCopy);
                }
                else
                {
                    DiffTypeExcludingChildren = CombineChildDiffTypes(diffTypeExcChildren, TypeOfDiff.Conflict);
                }

				if (baseConstructs.NewGen != null && baseConstructs.User != null && baseConstructs.GetFirstNonNullObject().IsLeaf)
                {
                    // Do a diff on the body text of our BaseConstructs
                    string userText = GetFullText(UserObj).TrimStart(Environment.NewLine.ToCharArray());
					string newgenText = GetFullText(NewGenObj).TrimStart(Environment.NewLine.ToCharArray());

                    TypeOfDiff result = SlyceMerge.PerformTwoWayDiff(userText, newgenText);
                    DiffTypeExcludingChildren = CombineChildDiffTypes(diffTypeExcChildren, result);
                }
            }
            else
            {
                if (BaseConstructsAreSame == false)
                {
                    // Do a diff on the outer text of our BaseConstructs
                    string userText = GetOuterText(UserObj);
                    string newgenText = GetOuterText(NewGenObj);
                    string prevgenText = GetOuterText(PrevGenObj);
                    string mergedText;

                    DiffTypeExcludingChildren =
                        SlyceMerge.Perform3wayDiff(userText, prevgenText, newgenText, out mergedText, false).DiffType;
                }

                // Also diff ourselves, looking at more than a basic signature comparison but not looking at children (we have already done that above)
                if (baseConstructs.PrevGen == null)
                {
                    if (baseConstructs.NewGen == null)
                    {
                        DiffTypeExcludingChildren =
                            CombineChildDiffTypes(diffTypeExcChildren, TypeOfDiff.UserChangeOnly);
                    }
                    else if (baseConstructs.User == null && ParentTree.UserCodeRoot != null)
                        // if the user file is null it may not have been generated yet - this is the first run of this project.
                    {
                        DiffTypeExcludingChildren =
                            CombineChildDiffTypes(diffTypeExcChildren, TypeOfDiff.TemplateChangeOnly);
                    }
                    else
                    {
                        DiffTypeExcludingChildren =
                            CombineChildDiffTypes(diffTypeExcChildren, TypeOfDiff.UserAndTemplateChange);
                    }
                }
                else
                {
                    if (baseConstructs.PrevGen.IsTheSame(baseConstructs.NewGen, ComparisonDepth.Outer) == false)
                    {
                        // If NewGen is null, this will return false.
                        DiffTypeExcludingChildren =
                            CombineChildDiffTypes(diffTypeExcChildren, TypeOfDiff.TemplateChangeOnly);
                    }
                    if (baseConstructs.PrevGen.IsTheSame(baseConstructs.User, ComparisonDepth.Outer) == false)
                    {
                        // If User is null, this will return false.
                        DiffTypeExcludingChildren =
                            CombineChildDiffTypes(diffTypeExcChildren, TypeOfDiff.UserChangeOnly);
                    }
                    if (baseConstructs.NewGen != null && baseConstructs.User != null &&
                        diffTypeExcChildren == TypeOfDiff.Conflict &&
                        baseConstructs.NewGen.IsTheSame(baseConstructs.User, ComparisonDepth.Outer))
                    {
                        DiffTypeExcludingChildren = TypeOfDiff.UserAndTemplateChange;
                    }
                    if (baseConstructs.NewGen == null && baseConstructs.User == null)
                    {
                        DiffTypeExcludingChildren = TypeOfDiff.UserAndTemplateChange;
                    }
                }

                if(baseConstructs.GetFirstNonNullObject().IsLeaf)
                {
                    // Do a diff on the body text of our BaseConstructs
                    string userText = GetBodyText(UserObj).TrimStart(Environment.NewLine.ToCharArray());
					string newgenText = GetBodyText(NewGenObj).TrimStart(Environment.NewLine.ToCharArray());
					string prevgenText = GetBodyText(PrevGenObj).TrimStart(Environment.NewLine.ToCharArray());
                    string mergedText;

                    TypeOfDiff result = SlyceMerge.Perform3wayDiff(userText, prevgenText, newgenText, out mergedText).DiffType;
                    DiffTypeExcludingChildren = CombineChildDiffTypes(diffTypeExcChildren, result);
                }

                if (baseConstructs.NewGen != null && ParentTree.PrevGenCodeRoot == null && ParentTree.UserCodeRoot == null)
                {
                    // Template base construct exists, but Prevgen and User CodeRoots haven't been added.. Make it an exact copy.
                    DiffTypeExcludingChildren = TypeOfDiff.ExactCopy;
                }
            }

            // Diff out child nodes.
            foreach(CodeRootMapNode node in children)
            {
                diff = CombineChildDiffTypes(diff, node.Diff());
            }

            diff = CombineChildDiffTypes(diff, diffTypeExcChildren);

            return diff;
        }

        /// <summary>
        /// Returns a IBaseConstruct which is the result of merging the BaseConstructs in this subtree.
        /// </summary>
        /// <returns>A IBaseConstruct which is the result of merging the BaseConstructs in this subtree.</returns>
        /// <exception cref="InvalidOperationException">Thrown if there are no BaseConstructs in this node 
        /// or any of its children.</exception>
        public virtual IBaseConstruct GetMergedBaseConstruct()
        {
            if (mergedObj != null)
                return mergedObj;

            IBaseConstruct newBc = baseConstructs.GetFirstNonNullObject();

            if (newBc == null)
                throw new InvalidOperationException(
                    "Cannot create a merged IBaseConstruct because this node contains no data");

            newBc = newBc.Clone();

            if(baseConstructs.DetermineMissingConstructs() == MissingObject.None)
            {
                bool result = newBc.CustomMergeStep(baseConstructs.User, baseConstructs.NewGen, baseConstructs.PrevGen);
				if(result == false)
				{
					// Need to notify the user of this issue.
					throw new MergeException("Could not perform automatic merge, CustomMergeStep failed. ", GetFirstValidBaseConstruct().FullyQualifiedIdentifer, GetFirstValidBaseConstruct().GetType().ToString());
				}
            }

            if(newBc is IBody)
            {
                IBody bodyNode = (IBody)newBc;
                string userText = GetBodyText(UserObj);
                string newgenText = GetBodyText(NewGenObj);
                string prevgenText = GetBodyText(PrevGenObj);

                if (UserObj == null && NewGenObj != null)
                {
                    bodyNode.BodyText = newgenText;
                }
                else if (NewGenObj == null && UserObj != null)
                {
                    bodyNode.BodyText = userText;
                }
                else
                {
                    string mergedText;
                    TypeOfDiff result = SlyceMerge.Perform3wayDiff(userText, prevgenText, newgenText, out mergedText).DiffType;
                    if (result == TypeOfDiff.Conflict)
                        throw new InvalidOperationException("Cannot merge BaseConstructs if there is a conflict in their body text.");
                    bodyNode.BodyText = mergedText;
                }
            }

            foreach(CodeRootMapNode child in children)
            {
                MissingObject missingObjects = child.DetermineMissingConstructs();
                if ((missingObjects & MissingObject.NewGen) != 0 &&
                    (missingObjects & MissingObject.User) != 0)
                {
                    continue;
                }

                IBaseConstruct childBC = child.GetMergedBaseConstruct();
                newBc.AddChild(childBC);
                
            }

            return newBc;
        }

        public void SetMergedBaseConstruct(string mergedCode)
        {
            MergedObj = ParentTree.UserCodeRoot.CreateBaseConstruct(mergedCode, baseConstructs.GetFirstNonNullObject());
        }

        private bool BaseConstructsAreSame
        {
            get
            {
                IBaseConstruct firstBc = baseConstructs.GetFirstNonNullObject();
                if (firstBc == null)
                    return true;
                bool result = false;
                if(baseConstructs.User!= null)
                    result |= firstBc.IsTheSame(baseConstructs.User, ComparisonDepth.Signature);
                if (baseConstructs.NewGen != null)
                    result |= firstBc.IsTheSame(baseConstructs.NewGen, ComparisonDepth.Signature);
                if (baseConstructs.PrevGen != null)
                    result |= firstBc.IsTheSame(baseConstructs.PrevGen, ComparisonDepth.Signature);
                return result;
            }
        }

        public TypeOfDiff DiffTypeExcludingChildren
        {
            get { return diffTypeExcChildren; }
            set { diffTypeExcChildren = value; }
        }

        public static TypeOfDiff CombineChildDiffTypes(TypeOfDiff diff1, TypeOfDiff diff2)
        {
            if(diff1 == diff2)
                return diff1;
            if (diff1 == TypeOfDiff.Conflict || diff2 == TypeOfDiff.Conflict)
                return TypeOfDiff.Conflict;
            if (diff1 == TypeOfDiff.Warning || diff2 == TypeOfDiff.Warning)
                return TypeOfDiff.Warning;
            if (diff1 == TypeOfDiff.Unknown || diff2 == TypeOfDiff.Unknown)
                return TypeOfDiff.Unknown;

            if (diff1 == TypeOfDiff.ExactCopy)
                return diff2;
            if (diff2 == TypeOfDiff.ExactCopy)
                return diff1;

            switch(diff1)
            {
                case TypeOfDiff.TemplateChangeOnly:
                    if (diff2 == TypeOfDiff.UserChangeOnly)
                        return TypeOfDiff.UserAndTemplateChange;
                    if (diff2 == TypeOfDiff.UserAndTemplateChange)
                        return TypeOfDiff.UserAndTemplateChange;
                    break;
                case TypeOfDiff.UserChangeOnly:
                    if (diff2 == TypeOfDiff.TemplateChangeOnly)
                        return TypeOfDiff.UserAndTemplateChange;
                    if (diff2 == TypeOfDiff.UserAndTemplateChange)
                        return TypeOfDiff.UserAndTemplateChange;
                    break;
                case TypeOfDiff.UserAndTemplateChange:
                    return TypeOfDiff.UserAndTemplateChange;
            }

            return TypeOfDiff.Unknown;
        }

        private static string GetBodyText(IBaseConstruct bc)
        {
            return bc != null ? bc.GetInnerText() : string.Empty;
        }

        private static string GetOuterText(IBaseConstruct bc)
        {
            return bc != null ? bc.GetOuterText() : string.Empty;
        }

		private static string GetFullText(IBaseConstruct bc)
		{
			return bc != null ? bc.GetFullText() : string.Empty;
		}
    }

	/// <summary>
	/// Indicates that there was a problem merging a set of BaseConstructs.
	/// </summary>
	public class MergeException : Exception
	{
		private readonly string baseConstructName;
		public readonly string bcType;

		public string BaseConstructName
		{
			get { return baseConstructName; }
		}

		public string BaseConstructType
		{
			get { return bcType; }
		}

		public MergeException()
		{
		}

		public MergeException(string message, string baseConstructName, string baseConstructType)
			: base(message)
		{
			this.baseConstructName = baseConstructName;
			this.bcType = baseConstructType;
		}

		public MergeException(string message) : base(message)
		{
		}

		public MergeException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected MergeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}