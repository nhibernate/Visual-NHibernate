using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArchAngel.Providers.CodeProvider.CSharp;

namespace ArchAngel.Providers.CodeProvider.DotNet
{
	public class Constant : BaseConstruct, IVisibilityModifiers
	{
		public DataType DataType;
		private List<string> _Modifiers = new List<string>();
		public string Expression = "";

		public Constant(Controller controller, BaseConstruct parentObject, CodeLanguage language, int nodeIndex)
			: base(controller)
		{
			ParentObject = parentObject;
			Language = language;
			Index = nodeIndex;
		}

		public Constant(Controller controller) : base(controller) { }

		public Constant(Controller controller, string name, DataType type, string expr)
			: base(controller)
		{
			Name = name;
			DataType = type;
			Expression = expr;
		}

		public Constant(Controller controller, string name, DataType dataType)
			: base(controller)
		{
			Name = name;
			DataType = dataType;
		}

		private Constant(Constant iiToCopyFrom)
			: base(null)
		{
			iiToCopyFrom.CloneInto(this);
			DataType = iiToCopyFrom.DataType.Clone();
			Expression = iiToCopyFrom.Expression;
			Modifiers = new List<string>(iiToCopyFrom.Modifiers);
		}

		/// <summary>
		/// Returns a shallow copy of the construct. Does not copy children.
		/// </summary>
		/// <returns>A shallow copy of the construct. Does not copy children.</returns>
		public override IBaseConstruct Clone()
		{
			return new Constant(this);
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

		/// <summary>
		/// True if the construct is a leaf node, and can contain no children.
		/// </summary>
		public override bool IsLeaf
		{
			get { return true; }
		}

		/// <summary>
		/// Adds a new child to this IBaseConstruct.
		/// </summary>
		/// <param name="childBC">The child object to add</param>
		protected override void AddChildInternal(BaseConstruct childBC)
		{
			throw new InvalidOperationException("Cannot add child nodes to an InterfaceMethod");
		}

		public List<string> Modifiers
		{
			get { return _Modifiers; }
			set { _Modifiers = value; }
		}

		protected override bool CustomMergeStepInternal(BaseConstruct user, BaseConstruct newgen, BaseConstruct prevgen)
		{
			Constant userCon = (Constant)user, newgenCon = (Constant)newgen, prevgenCon = (Constant)prevgen;

			// Modifiers
			if (Utility.MergeModifiers(this, userCon, newgenCon, prevgenCon) == false)
				return false;

			// Expression
			if (Utility.MergeSingleItem(ref Expression, userCon.Expression, newgenCon.Expression, prevgenCon.Expression) == false)
				return false;

			// DataType
			if (Utility.MergeDataType(ref DataType, userCon.DataType, newgenCon.DataType, prevgenCon.DataType) == false)
				return false;

			return true;
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct)
		{
			return IsTheSame(comparisonStruct as Constant);
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct, ComparisonDepth depth)
		{
			return IsTheSame(comparisonStruct as Constant, depth);
		}

		private bool IsTheSame(Constant comparisonConstant)
		{
			return IsTheSame(comparisonConstant, ComparisonDepth.Signature);
		}

		private bool IsTheSame(Constant comparisonConstant, ComparisonDepth depth)
		{
			if (comparisonConstant == null)
				return false;

			if (Name == comparisonConstant.Name)
			{
				// Function names are the same, so now compare the class names
				//bool parentClassesTheSame = false;

				//if (ParentObject == null)
				//{
				//    parentClassesTheSame = comparisonConstant.ParentObject == null;
				//}
				//else if (comparisonConstant.ParentObject == null)
				//{
				//    // thisParentClass is obviously not null, because we checked above, so the classes are different
				//}
				//else
				//{
				//    parentClassesTheSame = ParentObject.IsTheSame(comparisonConstant.ParentObject);
				//}
				//if (parentClassesTheSame)
				//{
				if (depth == ComparisonDepth.Signature)
				{
					return true;
				}

				if (DataType != comparisonConstant.DataType)
					return false;

				if (!base.IsTheSame(comparisonConstant, depth))
				{
					return false;
				}

				if (!ArchAngel.Providers.CodeProvider.CSharp.Utility.StringCollectionsAreTheSame(Modifiers, comparisonConstant.Modifiers))
				{
					ComparisonDifference += GetType().Name + ".Modifiers";
					return false;
				}

				if (!ArchAngel.Providers.CodeProvider.CSharp.Utility.BaseContructCollectionsAreTheSame(Attributes.ToArray(), comparisonConstant.Attributes.ToArray()))
				{
					ComparisonDifference += GetType().Name + ".Attributes";
					return false;
				}

				//}
				return true;
			}
			return false;
		}
	}
}
