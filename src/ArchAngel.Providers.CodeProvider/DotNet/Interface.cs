using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArchAngel.Providers.CodeProvider.CSharp;

namespace ArchAngel.Providers.CodeProvider.DotNet
{
	public class Interface : BaseConstruct, IVisibilityModifiers
	{
		public List<InterfaceMethod> Methods = new List<InterfaceMethod>();
		public List<InterfaceProperty> Properties = new List<InterfaceProperty>();
		public List<InterfaceIndexer> Indexers = new List<InterfaceIndexer>();
		public List<InterfaceEvent> Events = new List<InterfaceEvent>();
		private List<string> _Modifiers = new List<string>();
		public string InterfaceBase = "";

		public Interface(Controller controller, string name, List<string> modifiers, string interfaceBase, CodeLanguage language, BaseConstruct parentObject, int nodeIndex)
			: base(controller)
		{
			Name = name;
			InterfaceBase = interfaceBase;
			Language = language;
			Modifiers = modifiers;
			ParentObject = parentObject;
			Index = nodeIndex;
		}

		public Interface(Controller controller, string name)
			: base(controller)
		{
			Name = name;
		}

		public Interface(Controller controller, string name, string modifier)
			: base(controller)
		{
			Name = name;
			_Modifiers.Add(modifier);
		}

		private Interface(Interface interfaceToCopyFrom) : base(null)
		{
			interfaceToCopyFrom.CloneInto(this);
			InterfaceBase = interfaceToCopyFrom.InterfaceBase;
			foreach(string mod in interfaceToCopyFrom.Modifiers)
			{
				_Modifiers.Add(mod);
			}
		}

		/// <summary>
		/// Returns a shallow copy of the construct. Does not copy children.
		/// </summary>
		/// <returns>A shallow copy of the construct. Does not copy children.</returns>
		public override IBaseConstruct Clone()
		{
			return new Interface(this);
		}

		public List<string> Modifiers
		{
			get { return _Modifiers; }
			set { _Modifiers = value; }
		}

		/// <summary>
		/// Returns all of the child IBaseConstructs in this node in no particular order.
		/// </summary>
		/// <returns>All of the child IBaseConstructs in this node in no particular order.</returns>
		protected override ReadOnlyCollection<IBaseConstruct> WalkChildrenInternal()
		{
			List<IBaseConstruct> children = new List<IBaseConstruct>();
			foreach(InterfaceMethod im in Methods)
			{
				children.Add(im);
			}
			foreach (InterfaceProperty im in Properties)
			{
				children.Add(im);
			}
			foreach (InterfaceIndexer im in Indexers)
			{
				children.Add(im);
			}
			foreach (InterfaceEvent im in Events)
			{
				children.Add(im);
			}
			return children.AsReadOnly();
		}

		protected override bool CustomMergeStepInternal(BaseConstruct user, BaseConstruct newgen, BaseConstruct prevgen)
		{
			Interface userBC = (Interface)user, newgenBC = (Interface)newgen, prevgenBC = (Interface)prevgen;

			// Modifiers
			if (!Utility.MergeModifiers(this, userBC, newgenBC, prevgenBC))
				return false;
			// InterfaceBase
			if (!Utility.MergeSingleItem(ref InterfaceBase, userBC.InterfaceBase, newgenBC.InterfaceBase, prevgenBC.InterfaceBase))
				return false;

			return true;
		}

		/// <summary>
		/// Removes the given child object from this object.
		/// </summary>
		/// <param name="child"></param>
		/// <exception cref="InvalidOperationException">If the child type is unsupported (the parent doesn't have any of those objects) then an
		/// InvalidOperationExeception is thrown.</exception>
		/// <exception cref="ArgumentException">If the child's parent is not this object, then an ArgumentException is thrown.</exception>
		protected override void RemoveChildObjectInternal(BaseConstruct child)
		{
			if (child is InterfaceIndexer)
			{
				if (Indexers.Remove(child as InterfaceIndexer) == false)
					throw new ArgumentException("object is not a member of this class");
			}
			else if (child is InterfaceMethod)
			{
				if (Methods.Remove(child as InterfaceMethod) == false)
					throw new ArgumentException("object is not a member of this class");
			}
			else if (child is InterfaceProperty)
			{
				if (Properties.Remove(child as InterfaceProperty) == false)
					throw new ArgumentException("object is not a member of this class");
			}
			else if (child is InterfaceEvent)
			{
				if (Events.Remove(child as InterfaceEvent) == false)
					throw new ArgumentException("object is not a member of this class");
			}
 
			else
				base.RemoveChildObjectInternal(child);
		}

		/// <summary>
		/// Adds a new child to this IBaseConstruct.
		/// </summary>
		/// <param name="child">The child object to add.</param>
		protected override void AddChildInternal(BaseConstruct child)
		{
			if(child == null)
				throw new InvalidOperationException("Cannot add null child");

			if (child is InterfaceIndexer)
			{
				Indexers.Add(child as InterfaceIndexer);
			}
			else if (child is InterfaceMethod)
			{
				Methods.Add(child as InterfaceMethod);
			}
			else if (child is InterfaceProperty)
			{
				Properties.Add(child as InterfaceProperty);        
			}
			else if (child is InterfaceEvent)
			{
				Events.Add(child as InterfaceEvent);
			}

			else
				throw new InvalidOperationException("Cannot add child of type " + child.GetType());
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct)
		{
			return IsTheSame(comparisonStruct as Interface);
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct, ComparisonDepth depth)
		{
			return IsTheSame(comparisonStruct as Interface, depth);
		}

		private bool IsTheSame(Interface comparisonFunction)
		{
			return IsTheSame(comparisonFunction, ComparisonDepth.Signature);
		}

		private bool IsTheSame(Interface comparisonInterface, ComparisonDepth depth)
		{
			if (comparisonInterface == null)
				return false;
			
			if (Name == comparisonInterface.Name)
			{
				if (depth == ComparisonDepth.Signature)
				{
					return true;
				}
				
				if (!base.IsTheSame(comparisonInterface, depth))
				{
					return false;
				}

				if (!Utility.StringCollectionsAreTheSame(Modifiers, comparisonInterface.Modifiers))
				{
					ComparisonDifference += GetType().Name + ".Modifiers";
					return false;
				}

				if (depth == ComparisonDepth.Outer)
				{
					return true;
				}

				// Now check all child objects
				Comparers.BaseConstructComparer comparer = new Comparers.BaseConstructComparer();

				if (!ArchAngel.Providers.CodeProvider.CSharp.Utility.BaseContructCollectionsAreTheSame(Events.ToArray(), comparisonInterface.Events.ToArray(), comparer))
				{
					ComparisonDifference += GetType().Name + ".Events";
					return false;
				}
				if (!ArchAngel.Providers.CodeProvider.CSharp.Utility.BaseContructCollectionsAreTheSame(Indexers.ToArray(), comparisonInterface.Indexers.ToArray(), comparer))
				{
					ComparisonDifference += GetType().Name + ".Indexers";
					return false;
				}
				if (!ArchAngel.Providers.CodeProvider.CSharp.Utility.BaseContructCollectionsAreTheSame(Methods.ToArray(), comparisonInterface.Methods.ToArray(), comparer))
				{
					ComparisonDifference += GetType().Name + ".Methods";
					return false;
				}
				if (!ArchAngel.Providers.CodeProvider.CSharp.Utility.BaseContructCollectionsAreTheSame(Properties.ToArray(), comparisonInterface.Properties.ToArray(), comparer))
				{
					ComparisonDifference += GetType().Name + ".Properties";
					return false;
				}
				return true;
				
			}
			return false;
		}
	  }
}