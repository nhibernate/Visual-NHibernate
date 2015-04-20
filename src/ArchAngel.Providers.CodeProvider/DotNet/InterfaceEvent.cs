using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArchAngel.Providers.CodeProvider.CSharp;

namespace ArchAngel.Providers.CodeProvider.DotNet
{
	public class InterfaceEvent : BaseConstruct
	{
		public DotNet.DataType DataType;
		public bool HasNewKeyword = false;

		public InterfaceEvent(Controller controller)
			: base(controller)
		{
		}

		public InterfaceEvent(Controller controller, string name, DotNet.DataType type, bool isNew)
			: base(controller)
		{
			Name = name;
			DataType = type;
			HasNewKeyword = isNew;
		}

		public InterfaceEvent(Controller controller, BaseConstruct parentObject, string name, DotNet.DataType dataType, bool hasNewKeyword, CodeLanguage language, int nodeIndex)
			: base(controller)
		{
			ParentObject = parentObject;
			Name = name;
			DataType = dataType;
			HasNewKeyword = hasNewKeyword;
			Language = language;
			Index = nodeIndex;
		}

		private InterfaceEvent(InterfaceEvent eventToCopyFrom)
			: base(null)
		{
			eventToCopyFrom.CloneInto(this);
			if (eventToCopyFrom.DataType != null)
			{
				DataType = eventToCopyFrom.DataType.Clone();
			}
			HasNewKeyword = eventToCopyFrom.HasNewKeyword;
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
			return new InterfaceEvent(this);
		}

		/// <summary>
		/// Adds a new child to this IBaseConstruct.
		/// </summary>
		/// <param name="childBC">The child object to add</param>
		protected override void AddChildInternal(BaseConstruct childBC)
		{
			throw new InvalidOperationException("Cannot add child nodes to an InterfaceMethod");
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
			InterfaceEvent userBC = (InterfaceEvent)user, newgenBC = (InterfaceEvent)newgen, prevgenBC = (InterfaceEvent)prevgen;
			// DataType
			if (!Utility.MergeDataType(ref DataType, userBC.DataType, newgenBC.DataType, prevgenBC.DataType))
				return false;
			// HasNewKeyword
			if (!Utility.MergeSingleItem(ref HasNewKeyword, userBC.HasNewKeyword, newgenBC.HasNewKeyword, prevgenBC.HasNewKeyword))
				return false;

			return true;
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct)
		{
			return IsTheSame(comparisonStruct as InterfaceEvent);
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct, ComparisonDepth depth)
		{
			return IsTheSame(comparisonStruct as InterfaceEvent, depth);
		}

		private bool IsTheSame(InterfaceEvent comparisonEvent)
		{
			return IsTheSame(comparisonEvent, ComparisonDepth.Signature);
		}

		private bool IsTheSame(InterfaceEvent comparisonEvent, ComparisonDepth depth)
		{
			if (comparisonEvent == null)
				return false;

			if (Name == comparisonEvent.Name)
			{
				// Function names are the same, so now compare the class names
				// TODO: Parent Comparison
				//if (ParentObject.IsTheSame(comparisonEvent.ParentObject))
				{
					if (depth == ComparisonDepth.Signature)
					{
						return true;
					}

					if (DataType != comparisonEvent.DataType) return false;

					if (HasNewKeyword != comparisonEvent.HasNewKeyword)
					{
						ComparisonDifference += GetType().Name + ".HasNewKeyword";
						return false;
					}
					if (!base.IsTheSame(comparisonEvent, depth))
					{
						return false;
					}
					return true;
				}
			}
			return false;
		}
	}
}