using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArchAngel.Providers.CodeProvider.CSharp;

namespace ArchAngel.Providers.CodeProvider.DotNet
{
	public class InterfaceAccessor : BaseConstruct, IVisibilityModifiers
	{
		private List<string> _Modifiers = new List<string>();
		public AccessorTypes AccessorType = AccessorTypes.Get;

		public InterfaceAccessor(Controller controller)
			: base(controller)
		{
		}

		public InterfaceAccessor(Controller controller, AccessorTypes accessorType, string modifier)
			: base(controller)
		{
			AccessorType = accessorType;
			_Modifiers.Add(modifier);
		}

		public InterfaceAccessor(Controller controller, BaseConstruct parentObject, AccessorTypes accessorType, CodeLanguage language)
			: base(controller)
		{
			ParentObject = parentObject;
			AccessorType = accessorType;
			Language = language;
		}

		private InterfaceAccessor(InterfaceAccessor propToCopyFrom)
			: base(null)
		{
			propToCopyFrom.CloneInto(this);
			AccessorType = propToCopyFrom.AccessorType;
			Modifiers = new List<string>(propToCopyFrom.Modifiers);
		}

		/// <summary>
		/// Returns a shallow copy of the construct. Does not copy children.
		/// </summary>
		/// <returns>A shallow copy of the construct. Does not copy children.</returns>
		public override IBaseConstruct Clone()
		{
			return new InterfaceAccessor(this);
		}

		public override string DisplayName
		{
			get { return AccessorType == AccessorTypes.Get ? "Get Accessor" : "Set Accessor"; }
		}

		public override string FullyQualifiedDisplayName
		{
			get
			{
				return (ParentObject != null) ? ParentObject.FullyQualifiedDisplayName + "." + DisplayName : DisplayName;
			}
		}

		public enum AccessorTypes
		{
			Get,
			Set
		}

		/// <summary>
		/// True if the construct is a leaf node, and can contain no children.
		/// </summary>
		public override bool IsLeaf
		{
			get { return true; }
		}

		public List<string> Modifiers
		{
			get
			{
				if (_Modifiers == null) _Modifiers = new List<string>();

				return _Modifiers;
			}
			set { _Modifiers = value; }
		}

		/// <summary>
		/// Returns all of the child IBaseConstructs in this node in no particular order.
		/// </summary>
		/// <returns>All of the child IBaseConstructs in this node in no particular order.</returns>
		protected override ReadOnlyCollection<IBaseConstruct> WalkChildrenInternal()
		{
			List<IBaseConstruct> children = new List<IBaseConstruct>();
			return children.AsReadOnly();
		}

		protected override bool CustomMergeStepInternal(BaseConstruct user, BaseConstruct newgen, BaseConstruct prevgen)
		{
			InterfaceAccessor userBC = (InterfaceAccessor)user, newgenBC = (InterfaceAccessor)newgen, prevgenBC = (InterfaceAccessor)prevgen;
			// Modifiers
			if (!Utility.MergeModifiers(this, userBC, newgenBC, prevgenBC))
				return false;
			return true;
		}

		/// <summary>
		/// Adds a new child to this IBaseConstruct.
		/// </summary>
		/// <param name="childBC">The child object to add</param>
		protected override void AddChildInternal(BaseConstruct childBC)
		{
			throw new InvalidOperationException("Cannot add child nodes to an InterfaceMethod");
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct)
		{
			return IsTheSame(comparisonStruct as InterfaceAccessor);
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct, ComparisonDepth depth)
		{
			return IsTheSame(comparisonStruct as InterfaceAccessor, depth);
		}

		private bool IsTheSame(InterfaceAccessor comparisonFunction)
		{
			return IsTheSame(comparisonFunction, ComparisonDepth.Signature);
		}

		private bool IsTheSame(InterfaceAccessor comparisonInterfaceAccessor, ComparisonDepth depth)
		{
			if (comparisonInterfaceAccessor == null)
				return false;

			if (AccessorType == comparisonInterfaceAccessor.AccessorType)
			{
				// Function names are the same, so now compare the class names
				// TODO: Parent Comparison
				//                if (ParentObject.IsTheSame(comparisonInterfaceAccessor.ParentObject))
				{
					if (depth == ComparisonDepth.Signature)
					{
						return true;
					}

					if (!base.IsTheSame(comparisonInterfaceAccessor, depth))
					{
						return false;
					}
					if (!ArchAngel.Providers.CodeProvider.CSharp.Utility.StringCollectionsAreTheSame(Modifiers, comparisonInterfaceAccessor.Modifiers))
					{
						ComparisonDifference += GetType().Name + ".Modifiers";
						return false;
					}
					return true;
				}
			}
			return false;
		}

		public override string FullyQualifiedIdentifer
		{
			get
			{
				if (ParentObject != null && ParentObject is CSharpParser.CodeRootBaseConstructAdapter == false)
					return ParentObject.FullyQualifiedIdentifer + BaseConstructConstants.FullyQualifiedIdentifierSeparator + (AccessorType == AccessorTypes.Get ? "get" : "set");

				return AccessorType == AccessorTypes.Get ? "get" : "set";
			}
		}
	}
}