using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ArchAngel.Providers.CodeProvider.DotNet
{
	public class RegionStart : BaseConstruct
	{
		public RegionStart(Controller controller, string name, int index)
			: base(controller)
		{
			Name = name;
			Index = index;
		}

		private RegionStart(RegionStart reToCopyFrom)
			: base(null)
		{
			reToCopyFrom.CloneInto(this);
		}

		/// <summary>
		/// True if the construct is a leaf node, and can contain no children.
		/// </summary>
		public override bool IsLeaf
		{
			get { return true; }
		}

		/// <summary>
		/// Returns a shallow copy of the construct. Does not copy children.
		/// </summary>
		/// <returns>A shallow copy of the construct. Does not copy children.</returns>
		public override IBaseConstruct Clone()
		{
			return new RegionStart(this);
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
		/// Adds a new child to this IBaseConstruct.
		/// </summary>
		/// <param name="childBC">The child object to add</param>
		protected override void AddChildInternal(BaseConstruct childBC)
		{
			throw new InvalidOperationException("Cannot add child nodes to an InterfaceMethod");
		}

		protected override bool CustomMergeStepInternal(BaseConstruct user, BaseConstruct newgen, BaseConstruct prevgen)
		{
			return true;
		}

		public override bool IsTheSame(BaseConstruct comparisonObject)
		{
			return IsTheSame(comparisonObject as RegionStart, ComparisonDepth.Signature);
		}

		public override bool IsTheSame(BaseConstruct comparisonObject, ComparisonDepth depth)
		{
			return IsTheSame(comparisonObject as RegionStart, depth);
		}

		private bool IsTheSame(RegionStart comparisonObject, ComparisonDepth depth)
		{
			if (comparisonObject == null)
				return false;
			return base.IsTheSame(comparisonObject, depth);
		}
	}
}