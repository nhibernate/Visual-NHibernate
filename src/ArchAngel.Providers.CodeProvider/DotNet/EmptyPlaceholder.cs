using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider
{
	public class EmptyPlaceholder : BaseConstruct
	{
		public EmptyPlaceholder(Controller controller)
			: base(controller)
		{
		}

		public EmptyPlaceholder(Controller controller, string name, int index)
			: base(controller)
		{
			Name = name;
			Index = index;
		}

		private EmptyPlaceholder(EmptyPlaceholder phToCopyFrom)
			: base(null)
		{
			phToCopyFrom.CloneInto(this);
		}

		/// <summary>
		/// Returns a shallow copy of the construct. Does not copy children.
		/// </summary>
		/// <returns>A shallow copy of the construct. Does not copy children.</returns>
		public override IBaseConstruct Clone()
		{
			return new EmptyPlaceholder(this);
		}

		protected override bool CustomMergeStepInternal(BaseConstruct user, BaseConstruct newgen, BaseConstruct prevgen)
		{
			return true;
		}

		/// <summary>
		/// Returns all of the child IBaseConstructs in this node in no particular order.
		/// </summary>
		/// <returns>All of the child IBaseConstructs in this node in no particular order.</returns>
		protected override ReadOnlyCollection<IBaseConstruct> WalkChildrenInternal()
		{
			return new List<IBaseConstruct>().AsReadOnly();
		}

		/// <summary>
		/// Adds a new child to this IBaseConstruct.
		/// </summary>
		/// <param name="childBC">The child object to add</param>
		protected override void AddChildInternal(BaseConstruct childBC)
		{
			throw new InvalidOperationException("Cannot add child nodes to an InterfaceMethod");
		}

		public override bool IsTheSame(BaseConstruct comparisonObject)
		{
			return comparisonObject is EmptyPlaceholder;
		}

		public override bool IsTheSame(BaseConstruct comparisonObject, ComparisonDepth depth)
		{
			return comparisonObject is EmptyPlaceholder;
		}
	}
}
