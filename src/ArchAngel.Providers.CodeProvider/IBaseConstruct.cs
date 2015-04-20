using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ArchAngel.Providers.CodeProvider
{
    ///<summary>
    /// Represents an element in an Object Model.
    ///</summary>
    public interface IBaseConstruct
    {
        /// <summary>
        /// The parent of this Base Construct.
        /// </summary>
        IBaseConstruct Parent { get; set; }

        /// <summary>
        /// True if the construct is a leaf node, and can contain no children.
        /// </summary>
        bool IsLeaf { get; }

        /// <summary>
        /// Returns all of the child IBaseConstructs in this node in no particular order.
        /// </summary>
        /// <returns>All of the child IBaseConstructs in this node in no particular order.</returns>
        ReadOnlyCollection<IBaseConstruct> WalkChildren();

        /// <summary>
        /// Returns all of the child IBaseConstructs in this node and below it in no particular order.
        /// </summary>
        /// <returns>All of the child IBaseConstructs in this node and below it in no particular order.</returns>
        IEnumerable<IBaseConstruct> WalkTree();
        
        /// <summary>
        /// The short display name of the element. The version intended to be shown to the user.
        /// </summary>
        string ShortName { get; }

        /// <summary>
        /// The full name, which could be used to identify the IBaseConstruct in the tree.
        /// </summary>
        string FullyQualifiedIdentifer { get; }

    	string FullyQualifiedDisplayName { get; }

    	/// <summary>
        /// Gets the inner text of the node, if it has any. If it doesn't, it returns string.Empty.
        /// </summary>
        /// <returns>The inner text of the node, if it has any. If it doesn't, it returns string.Empty.</returns>
        string GetInnerText();

        /// <summary>
        /// Gets the outer text of the node. 
        /// </summary>
        /// <remarks>This is the method signature for a C# Function, the element text (<![CDATA[<element></element>]]>) for an XML Element, etc.</remarks>
        /// <returns>The outer text of the node.</returns>
        string GetOuterText();

        /// <summary>
        /// Gets the full text of the node.
        /// </summary>
        /// <returns>The full text of the node.</returns>
        string GetFullText();

        /// <summary>
        /// Compares this IBaseConstruct against the one provided, and returns true if they are the same.
        /// Uses ComparisonDepth.Signature.
        /// </summary>
        /// <param name="comparisonObject">The object to compare against.</param>
        /// <returns>True if the objects are the same, false if they are different.</returns>
        bool IsTheSame(IBaseConstruct comparisonObject);
        /// <summary>
        /// Compares this IBaseConstruct against the one provided, and returns true if they are the same.
        /// </summary>
        /// <param name="depth">The depth of the comparison we do.</param>
        /// <param name="comparisonObject">The object to compare against.</param>
        /// <returns>True if the objects are the same, false if they are different.</returns>
        bool IsTheSame(IBaseConstruct comparisonObject, ComparisonDepth depth);
        /// <summary>
        /// Returns a shallow copy of the construct. Does not copy children.
        /// </summary>
        /// <returns>A shallow copy of the construct. Does not copy children.</returns>
        IBaseConstruct Clone();
        /// <summary>
        /// Adds a new child to this IBaseConstruct.
        /// </summary>
        /// <param name="childBC">The child object to add</param>
        void AddChild(IBaseConstruct childBC);
        /// <summary>
        /// Returns all children who are of the given type.
        /// </summary>
        /// <param name="type">The Type of children to return.</param>
        /// <returns>All child base constructs of the given type.</returns>
        ReadOnlyCollection<IBaseConstruct> GetChildrenOfType(Type type);

        /// <summary>
        /// Removes the given child object from this object.
        /// </summary>
        /// <param name="child"></param>
        /// <exception cref="InvalidOperationException">If the child type is unsupported (the parent doesn't have any of those objects) then an
        /// InvalidOperationExeception is thrown.</exception>
        /// <exception cref="ArgumentException">If the child's parent is not this object, then an ArgumentException is thrown.</exception>
        void RemoveChildObject(IBaseConstruct child);

        /// <summary>
        /// This method should be called on a merged object. It merged the minor features of a base construct, like modifiers in a 
        /// C# BC, or parameter names.
        /// </summary>
        /// <param name="user">The User's version of the object.</param>
        /// <param name="newgen">The Template's version of the object.</param>
        /// <param name="prevgen">The previously generated version of the object.</param>
        /// <returns>True if the merge was successful. If it returns false, the user will have to manually merge it. </returns>
        bool CustomMergeStep(IBaseConstruct user, IBaseConstruct newgen, IBaseConstruct prevgen);

    	/// <summary>
    	/// Searches for a child with the given unique id.
    	/// </summary>
    	/// <param name="uid"></param>
    	/// <returns>The child with that id, or null if the uid doesn't exist as a child of this construct.</returns>
    	IBaseConstruct FindChild(string uid);

        /// <summary>
        /// The range of text in the original document that holds the Base Construct's text.
        /// </summary>
        TextRange TextRange { get; set; }
    }

    public enum ComparisonDepth
    {
        /// <summary>
        /// Compare the BaseConstructs based on their signatures. This is the default level,
        /// and can be used to determine if two objects are superficially the same. Use for
        /// automatically matching objects.
        /// </summary>
        Signature = 0,
        /// <summary>
        /// Compare the BaseConstructs based on their outer text. This takes into account things
        /// like comments, accessability, attributes, and other non-identifying but important elements.
        /// </summary>
        Outer,
        /// <summary>
        /// Does a complete comparison based on all available information. Will compare children as well.
        /// Determines if two BaseConstructs are exactly equal.
        /// </summary>
        Complete
    }

	public static class BaseConstructConstants
	{
		public const char FullyQualifiedIdentifierSeparator = '|';

		public static string[] SplitFullyQualifiedIdentifier(string fqId)
		{
			return fqId.Split(FullyQualifiedIdentifierSeparator);
		}
	}
}