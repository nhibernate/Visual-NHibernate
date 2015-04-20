using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ArchAngel.Providers.CodeProvider.CSharp;

namespace ArchAngel.Providers.CodeProvider.DotNet
{
	public abstract class BaseConstruct : IBaseConstruct, IPrintable
	{
		public enum CodeLanguage
		{
			CSharp,
			VBDotNet,
			Unknown
		}
		public TextRange TextRange { get; set; }

		private CommentObject comments = new CommentObject();
		private int preceedingBlankLines = 0;
		public List<string> XmlComments = new List<string>();
		private readonly List<Attribute> _Attributes = new List<Attribute>();
		private readonly List<AttributeSection> _AttributeSections = new List<AttributeSection>();
		public int InputStartPos = -1;
		public int InputLength = -1;
		public int OutputOffset = -1;
		public CodeLanguage Language = CodeLanguage.Unknown;
		public BaseConstruct ParentObject = null;
		private string _name = "";
		private bool _omit = false;
		public static string ComparisonDifference = "";
		public Options DisplayOptions = new Options();
		public int Index = -1;
		protected List<EmptyPlaceholder> emptyPlaceholders = new List<EmptyPlaceholder>();
		public int NumberOfLines;
		private Controller controller = null;

		/// <summary>
		/// Construct a new BaseConstruct.
		/// </summary>
		/// <param name="controller">The controller used to control the formatting of this object.</param>
		protected BaseConstruct(Controller controller)
		{
			Controller = controller;
			TextRange = new TextRange(-1, -2);
		}

		//protected abstract CommentObject createCommentObject();

		///<summary>
		/// The controller used to control the formatting of this object.
		///</summary>
		public Controller Controller
		{
			get
			{
				if (controller == null)
				{
					if (ParentObject == null)
						return null;
					// Go up the tree to try find a parent who has a controller.
					controller = ParentObject.Controller;
				}
				return controller;
			}
			set { controller = value; }
		}

		/// <summary>
		/// The short name of this element.
		/// </summary>
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		public int PreceedingBlankLines
		{
			get { return preceedingBlankLines; }
			set { preceedingBlankLines = value; }
		}

		public CommentObject Comments
		{
			get { return comments; }
			set { comments = value; }
		}

		public List<EmptyPlaceholder> EmptyPlaceholders
		{
			get { return emptyPlaceholders; }
			set { emptyPlaceholders = value; }
		}

		/// <summary>
		/// True if the node should be omitted from the diff.
		/// </summary>
		public bool Omit
		{
			get { return _omit; }
			set
			{
				_omit = value;
			}
		}

		/// <summary>
		/// The full name of the node, in the form ParentName.Name, where parent name is the fully qaulified
		/// name of the parent.
		/// </summary>
		public virtual string FullyQualifiedName
		{
			get
			{
				IPrinter printer = controller.TryGetPrinterFor(this);
				return (printer == null) ? CalculateFullyQualifiedName(".") : printer.FullyQualifiedName;
			}
		}

		public virtual string FullyQualifiedDisplayName
		{
			get
			{
				return CalculateFullyQualifiedDisplayName(".");
			}
		}

		/// <summary>
		/// The full name, which could be used to identify the IBaseConstruct in the tree.
		/// </summary>
		public virtual string FullyQualifiedIdentifer
		{
			get { return CalculateFullyQualifiedDisplayName(BaseConstructConstants.FullyQualifiedIdentifierSeparator.ToString()); }
		}

		private string CalculateFullyQualifiedDisplayName(string separator)
		{
			StringBuilder sb = new StringBuilder(50);
			sb.Append(DisplayName);
			BaseConstruct parentObject = ParentObject;

			while (parentObject != null && parentObject is CSharpParser.CodeRootBaseConstructAdapter == false)
			{
				sb.Insert(0, parentObject.Name + separator);
				parentObject = parentObject.ParentObject;
			}
			return sb.ToString();
		}

		private string CalculateFullyQualifiedName(string separator)
		{
			StringBuilder sb = new StringBuilder(50);
			sb.Append(Name);
			BaseConstruct parentObject = ParentObject;

			while (parentObject != null && parentObject is CSharpParser.CodeRootBaseConstructAdapter == false)
			{
				sb.Insert(0, parentObject.DisplayName + separator);
				parentObject = parentObject.ParentObject;
			}
			return sb.ToString();
		}

		/// <summary>
		/// Gets Display name with individual paramter names and types (for functions and constructors).
		/// </summary>
		public virtual string DisplayNameExtended
		{
			get { return Name; }
		}

		public virtual string FullyQualifiedDisplayNameExtended
		{
			get { return FullyQualifiedName; }
		}

		public virtual string DisplayName
		{
			get { return Name; }
		}

		public virtual void RemoveChildObject(IBaseConstruct child)
		{
			if (child is BaseConstruct == false)
				throw new ArgumentException("Cannot remove child as it is not a C# construct.");
			RemoveChildObjectInternal(child as BaseConstruct);
		}

		public virtual bool HasAttributeNamed(string name)
		{
			return Attributes.Any(a => a.Name == name) ||
				   AttributeSections.SelectMany(section => section.Attributes).Any(a => a.Name == name);
		}

		public virtual bool HasUsingStatementNamed(string name)
		{
			return Controller.Root.UsingStatements.Any(u => u.Name == name);
		}

		/// <summary>
		/// This method should be called on a merged object. It merged the minor features of a base construct, like modifiers in a 
		/// C# BC, or parameter names.
		/// </summary>
		/// <param name="user">The User's version of the object.</param>
		/// <param name="newgen">The Template's version of the object.</param>
		/// <param name="prevgen">The previously generated version of the object.</param>
		/// <returns>True if the merge was successful. If it returns false, the user will have to manually merge it. </returns>
		public bool CustomMergeStep(IBaseConstruct user, IBaseConstruct newgen, IBaseConstruct prevgen)
		{
			Type thisType = GetType();

			if (thisType.IsInstanceOfType(user) == false
				|| thisType.IsInstanceOfType(newgen) == false
				|| thisType.IsInstanceOfType(prevgen) == false)
			{
				throw new ArgumentException(
					string.Format("Cannot merge IBaseConstructs of a different type"
					+ " into this C# BaseConstruct (User = {0}, NewGen = {1}, PrevGen = {2}, This = {3}",
					user.GetType(), newgen.GetType(),
					prevgen.GetType(), thisType));
			}

			BaseConstruct userBC = (BaseConstruct)user,
						  newgenBC = (BaseConstruct)newgen,
						  prevgenBC = (BaseConstruct)prevgen;

			// Comments
			if (!MergeComments(userBC, newgenBC, prevgenBC)) return false;
			// Xml Comments
			if (!Utility.MergeStringCollections(XmlComments, userBC.XmlComments, newgenBC.XmlComments, prevgenBC.XmlComments)) return false;
			// Name
			if (!Utility.MergeSingleItem(ref _name, userBC.Name, newgenBC.Name, prevgenBC.Name)) return false;
			// Attributes
			if (!MergeAttributes(userBC, newgenBC, prevgenBC)) return false;

			return CustomMergeStepInternal(userBC, newgenBC, prevgenBC);
		}

		/// <summary>
		/// This method should merge the minor features of a base construct, like modifiers or parameter names.
		/// </summary>
		/// <param name="user">The User's version of the object.</param>
		/// <param name="newgen">The Template's version of the object.</param>
		/// <param name="prevgen">The previously generated version of the object.</param>
		/// <returns>True if the merge was successful. If it returns false, the user will have to manually merge it. </returns>
		protected abstract bool CustomMergeStepInternal(BaseConstruct user, BaseConstruct newgen,
														BaseConstruct prevgen);

		private bool MergeComments(BaseConstruct userBC, BaseConstruct newgenBC, BaseConstruct prevgenBC)
		{
			bool userChange = false, templateChange = false;
			// Comments
			if (userBC.Comments.IsTheSame(prevgenBC.Comments) == false)
				userChange = true;
			if (newgenBC.Comments.IsTheSame(prevgenBC.Comments) == false)
				templateChange = true;

			if (userBC.Equals(newgenBC))
			{
				userChange = true;
				templateChange = false;
			}

			if (templateChange && userChange)
			{
				return false;
			}
			if (userChange)
			{
				Comments = userBC.Comments.Clone();
			}
			else if (templateChange)
			{
				Comments = newgenBC.Comments.Clone();
			}
			return true;
		}

		/// <summary>
		/// Removes the given child object from this object.
		/// </summary>
		/// <param name="child"></param>
		/// <exception cref="InvalidOperationException">If the child type is unsupported (the parent doesn't have any of those objects) then an
		/// InvalidOperationExeception is thrown.</exception>
		/// <exception cref="ArgumentException">If the child's parent is not this object, then an ArgumentException is thrown.</exception>
		protected virtual void RemoveChildObjectInternal(BaseConstruct child)
		{
			throw new InvalidOperationException("Cannot call RemoveChildObject on " + GetType() +
												" objects, as they don't handle " + child.GetType() + " child types");
		}

		public virtual bool IsTheSame(BaseConstruct comparisonObject)
		{
			throw new InvalidOperationException("BaseConstruct.IsTheSame is not allowed to be called directly. Call through an inherited object.");
		}

		public virtual bool IsTheSame(BaseConstruct comparisonObject, ComparisonDepth depth)
		{
			if (Name != comparisonObject.Name)
			{
				ComparisonDifference += comparisonObject.GetType().Name + ".Name";
				return false;
			}

			if (depth == ComparisonDepth.Signature)
			{
				return true;
			}

			Comparers.BaseConstructComparer comparer = new Comparers.BaseConstructComparer();

			if (!ArchAngel.Providers.CodeProvider.CSharp.Utility.BaseContructCollectionsAreTheSame(_Attributes.ToArray(), comparisonObject._Attributes.ToArray(), comparer))
			{
				ComparisonDifference += comparisonObject.GetType().Name + ".Attributes";
				return false;
			}
			if (!ArchAngel.Providers.CodeProvider.CSharp.Utility.StringCollectionsAreTheSame(XmlComments, comparisonObject.XmlComments))
			{
				ComparisonDifference += comparisonObject.GetType().Name + ".XmlComments";
				return false;
			}
			if (Comments.IsTheSame(comparisonObject.Comments) == false)
			{
				ComparisonDifference += comparisonObject.GetType().Name + ".Comments";
				return false;
			}
			return true;
		}


		public string GetXmlCommentsText()
		{
			if (XmlComments.Count == 0) { return ""; }

			StringBuilder sb = new StringBuilder(100);

			for (int i = 0; i < XmlComments.Count; i++)
			{
				sb.AppendLine(Controller.Indent + "/// " + XmlComments[i]);
			}
			return sb.ToString();
		}

		public string GetAttributesText()
		{
			if (_Attributes.Count == 0) { return ""; }

			StringBuilder sb = new StringBuilder(100);

			foreach (AttributeSection sec in _AttributeSections)
			{
				sb.AppendLine(sec.ToString());
			}
			return sb.ToString();
		}

		/// <summary>
		/// Formatting options for BaseConstructs.
		/// </summary>
		[Serializable]
		public class Options
		{
			/// <summary>
			/// Causes the object to be split into its constituent parts across multiple lines when calling ToString().
			/// Useful when diffing a language construct that is normally a single line.
			/// </summary>
			public bool ListVertically = false;
		}

		///// <summary>
		///// Implement this method so that it returns the child nodes of the object in the order they were in the original code
		///// </summary>
		//public abstract BaseConstruct[] SortedConstructs { get; }


		#region IBaseConstruct Members

		/// <summary>
		/// Returns the parent of this BaseConstruct.
		/// </summary>
		public IBaseConstruct Parent
		{
			get { return ParentObject; }
			set
			{
				if (value == null || value is BaseConstruct)
					ParentObject = value as BaseConstruct;
				else
					throw new ArgumentException("Cannot set a non-C# BaseConstruct as the parent of a C# construct.");
			}
		}

		/// <summary>
		/// Returns all of the child IBaseConstructs in this node in no particular order.
		/// </summary>
		/// <returns>All of the child IBaseConstructs in this node in no particular order.</returns>
		public ReadOnlyCollection<IBaseConstruct> WalkChildren()
		{
			List<IBaseConstruct> children = new List<IBaseConstruct>();
			children.AddRange(WalkChildrenInternal());

			// No longer treating attributes as children.
			//foreach(Attribute attr in Attributes)
			//{
			//    children.Add(attr);
			//}

			return children.AsReadOnly();
		}

		/// <summary>
		/// Returns all of the child IBaseConstructs in this node in no particular order.
		/// </summary>
		/// <returns>All of the child IBaseConstructs in this node in no particular order.</returns>
		protected abstract ReadOnlyCollection<IBaseConstruct> WalkChildrenInternal();


		/// <summary>
		/// Returns all of the IBaseConstructs in this sub tree in no particular order.
		/// </summary>
		/// <returns>All of the IBaseConstructs in this sub tree in no particular order.</returns>
		public IEnumerable<IBaseConstruct> WalkTree()
		{
			foreach (IBaseConstruct child in WalkChildren())
			{
				yield return child;
				foreach (var bc in child.WalkTree())
					yield return bc;
			}
		}


		/// <summary>
		/// Returns all children who are of the given type.
		/// </summary>
		/// <remarks>This could be completely reworked to not iterate through all available children, but
		/// until this is a performance problem I will leave this naive solution here as it requires a LOT
		/// less work. For types that have a lot of children it may be beneficial to override this method.</remarks>
		/// <param name="type">The Type of children to return.</param>
		/// <returns>All child base constructs of the given type.</returns>
		public virtual ReadOnlyCollection<IBaseConstruct> GetChildrenOfType(Type type)
		{
			List<IBaseConstruct> children = new List<IBaseConstruct>();

			foreach (IBaseConstruct child in WalkChildren())
			{
				if (type.IsInstanceOfType(child))
				{
					children.Add(child);
				}
			}

			return children.AsReadOnly();
		}

		/// <summary>
		/// The short display name of the element. The version intended to be shown to the user.
		/// </summary>
		public virtual string ShortName
		{
			get { return DisplayName; }
		}

		/// <summary>
		/// Gets the inner text of the node, if it has any. If it doesn't, it returns string.Empty.
		/// </summary>
		/// <returns>The inner text of the node, if it has any. If it doesn't, it returns string.Empty.</returns>
		public virtual string GetInnerText()
		{
			return string.Empty;
		}


		/// <summary>
		/// True if the construct is a leaf node, and can contain no children.
		/// </summary>
		public virtual bool IsLeaf
		{
			get { return false; }
		}

		public List<Attribute> Attributes
		{
			get { return _Attributes; }
		}

		public List<AttributeSection> AttributeSections
		{
			get { return _AttributeSections; }
		}

		/// <summary>
		/// Gets the outer text of the node. 
		/// </summary>
		/// <remarks>This is the method signature for a C# Function, the element text (<![CDATA[<element></element>]]>) for an XML Element, etc.</remarks>
		/// <returns>The outer text of the node.</returns>
		public virtual string GetOuterText()
		{
			return ToString();
		}

		/// <summary>
		/// Gets the full text of the node.
		/// </summary>
		/// <returns>The full text of the node.</returns>
		public string GetFullText()
		{
			return ToString();
		}

		/// <summary>
		/// Compares this IBaseConstruct against the one provided, and returns true if they are the same.
		/// </summary>
		/// <param name="comparisonObject">The object to compare against.</param>
		/// <returns>True if the objects are the same, false if they are different.</returns>
		public bool IsTheSame(IBaseConstruct comparisonObject)
		{
			if (comparisonObject is BaseConstruct)
				return IsTheSame(comparisonObject as BaseConstruct);

			return false;
		}


		/// <summary>
		/// Compares this IBaseConstruct against the one provided, and returns true if they are the same.
		/// </summary>
		/// <param name="depth">The depth of the comparison we do.</param>
		/// <param name="comparisonObject">The object to compare against.</param>
		/// <returns>True if the objects are the same, false if they are different.</returns>
		public bool IsTheSame(IBaseConstruct comparisonObject, ComparisonDepth depth)
		{
			if (comparisonObject is BaseConstruct)
			{
				if (depth == ComparisonDepth.Complete && !Utility.BaseContructCollectionsAreTheSame(_Attributes.ToArray(), ((BaseConstruct)comparisonObject)._Attributes.ToArray()))
				{
					ComparisonDifference += GetType().Name + ".Attributes";
					return false;
				}

				return IsTheSame(comparisonObject as BaseConstruct, depth);
			}
			return false;
		}

		/// <summary>
		/// Returns a shallow copy of the construct. Does not copy children.
		/// </summary>
		/// <returns>A shallow copy of the construct. Does not copy children.</returns>
		public abstract IBaseConstruct Clone();

		/// <summary>
		/// Adds a new child to this IBaseConstruct.
		/// </summary>
		/// <param name="childBC">The child object to add</param>
		public void AddChild(IBaseConstruct childBC)
		{
			if (childBC == this)
				throw new ArgumentException("Cannot add an object as a child of itself");

			// Cannot add non C# BaseConstructs.
			if (!(childBC is BaseConstruct))
				throw new ArgumentException("Cannot add an non C# BaseConstruct as a child of a C# BaseConstruct");

			BaseConstruct bc = (BaseConstruct)childBC;
			bc.ParentObject = this;
			bc.Controller = Controller;

			// No longer treating attributes as children.
			//if(childBC is Attribute)
			//{
			//    Attributes.Add(childBC as Attribute);
			//}
			if (childBC is EmptyPlaceholder)
			{
				emptyPlaceholders.Add(childBC as EmptyPlaceholder);
			}
			else
			{
				AddChildInternal(bc);
			}
		}

		/// <summary>
		/// Searches for a child with the given unique id.
		/// </summary>
		/// <param name="uid"></param>
		/// <returns>The child with that id, or null if the uid doesn't exist as a child of this construct.</returns>
		public IBaseConstruct FindChild(string uid)
		{
			foreach (BaseConstruct bc in WalkChildren())
			{
				if (bc.DisplayName == uid)
					return bc;
			}

			return null;
		}

		public void AddAttributeSections(List<AttributeSection> attrSections)
		{
			foreach (AttributeSection sec in attrSections)
			{
				AddAttributeSection(sec);
			}
		}

		public void AddAttributeSection(AttributeSection sec)
		{
			sec.ParentObject = this;
			_AttributeSections.Add(sec);
			foreach (Attribute attr in sec.SingleAttributes)
			{
				_Attributes.Add(attr);
			}
		}

		/// <summary>
		/// Adds a new child to this BaseConstruct.
		/// </summary>
		/// <param name="childBC">The child object to add</param>
		protected abstract void AddChildInternal(BaseConstruct childBC);

		#endregion

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			for (int i = 0; i <= PreceedingBlankLines; i++)
			{
				sb.AppendLine("");
			}
			foreach (string comment in Comments.PreceedingComments)
			{
				sb.AppendLine(Controller.Indent + comment);
			}
			sb.Append(GetXmlCommentsText());
			sb.Append(GetAttributesText());

			sb.Append(Controller.GetPrinterFor(this).Print());

			return sb.ToString();
		}

		protected void CloneInto(BaseConstruct newBaseConstruct)
		{
			newBaseConstruct.Name = Name;
			newBaseConstruct.Omit = Omit;
			newBaseConstruct.Comments = Comments.Clone();

			//newBaseConstruct.Attributes.Clear();
			//foreach(AttributeSection att in Attributes)
			//{
			//    AttributeSection att1 = (AttributeSection)att.Clone();
			//    att1.ParentObject = newBaseConstruct;
			//    newBaseConstruct.Attributes.Add(att1);
			//}

			newBaseConstruct.emptyPlaceholders.Clear();
			foreach (EmptyPlaceholder eph in emptyPlaceholders)
			{
				EmptyPlaceholder clone = (EmptyPlaceholder)eph.Clone();
				clone.ParentObject = newBaseConstruct;
				newBaseConstruct.emptyPlaceholders.Add(clone);
			}
			newBaseConstruct._Attributes.Clear();
			foreach (Attribute attr in _Attributes)
			{
				Attribute clone = (Attribute)attr.Clone();

				AttributeSection parentSec = new AttributeSection(null);
				parentSec.Target = ((AttributeSection)attr.ParentObject).Target;
				parentSec.AddAttribute(clone);
				newBaseConstruct.AddAttributeSection(parentSec);
			}

			newBaseConstruct.Index = Index;
			newBaseConstruct.InputLength = InputLength;
			newBaseConstruct.InputStartPos = InputStartPos;
			newBaseConstruct.Language = Language;
			newBaseConstruct.NumberOfLines = NumberOfLines;
			newBaseConstruct.OutputOffset = OutputOffset;
			newBaseConstruct.PreceedingBlankLines = PreceedingBlankLines;
			newBaseConstruct.XmlComments.Clear();
			foreach (string xml in XmlComments)
			{
				newBaseConstruct.XmlComments.Add(xml);
			}
		}
		private bool MergeAttributes(BaseConstruct userBC, BaseConstruct newgenBC, BaseConstruct prevgenBC)
		{
			bool userChange = false, templateChange = false;

			if (!ArchAngel.Providers.CodeProvider.CSharp.Utility.BaseContructCollectionsAreTheSame(userBC._Attributes.ToArray(), prevgenBC._Attributes.ToArray()))
				userChange = true;
			if (!ArchAngel.Providers.CodeProvider.CSharp.Utility.BaseContructCollectionsAreTheSame(newgenBC._Attributes.ToArray(), prevgenBC._Attributes.ToArray()))
				templateChange = true;

			if (ArchAngel.Providers.CodeProvider.CSharp.Utility.BaseContructCollectionsAreTheSame(newgenBC._Attributes.ToArray(), userBC._Attributes.ToArray()))
			{
				userChange = true;
				templateChange = false;
			}

			if (templateChange && userChange)
			{
				return false;
			}

			_Attributes.Clear();
			_AttributeSections.Clear();
			if (userChange)
			{
				foreach (Attribute attr in userBC._Attributes)
				{
					Attribute clone = (Attribute)attr.Clone();

					AttributeSection parentSec = new AttributeSection(controller);
					parentSec.Target = ((AttributeSection)attr.ParentObject).Target;
					parentSec.AddAttribute(clone);
					AddAttributeSection(parentSec);
				}
			}
			else if (templateChange)
			{
				foreach (Attribute attr in newgenBC._Attributes)
				{
					Attribute clone = (Attribute)attr.Clone();

					AttributeSection parentSec = new AttributeSection(controller);
					parentSec.Target = ((AttributeSection)attr.ParentObject).Target;
					parentSec.AddAttribute(clone);
					AddAttributeSection(parentSec);
				}
			}
			return true;
		}
	}
}
