using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArchAngel.Providers.CodeProvider.CSharp;

namespace ArchAngel.Providers.CodeProvider.DotNet
{
	public class InterfaceProperty : BaseConstruct
	{
		public DotNet.DataType DataType;
		public InterfaceAccessor GetAccessor;
		public InterfaceAccessor SetAccessor;
		public bool HasNewKeyword = false;

		public InterfaceProperty(Controller controller, String name)
			: base(controller)
		{
			Name = name;
		}

		public InterfaceProperty(Controller controller, String name, DotNet.DataType type)
			: base(controller)
		{
			Name = name;
			DataType = type;
		}

		public InterfaceProperty(Controller controller, BaseConstruct parentObject, string name, DotNet.DataType dataType, InterfaceAccessor getAccessor, InterfaceAccessor setAccessor, bool hasNewKeyword, CodeLanguage language, int nodeIndex)
			: base(controller)
		{
			ParentObject = parentObject;
			Name = name;
			DataType = dataType;
			GetAccessor = getAccessor;
			SetAccessor = setAccessor;
			HasNewKeyword = hasNewKeyword;
			Language = language;
			Index = nodeIndex;
		}

		private InterfaceProperty(InterfaceProperty ipToCopyFrom)
			: base(null)
		{

			ipToCopyFrom.CloneInto(this);
			if (ipToCopyFrom.DataType != null)
			{
				DataType = ipToCopyFrom.DataType.Clone();
			}
			HasNewKeyword = ipToCopyFrom.HasNewKeyword;
		}

		/// <summary>
		/// Returns a shallow copy of the construct. Does not copy children.
		/// </summary>
		/// <returns>A shallow copy of the construct. Does not copy children.</returns>
		public override IBaseConstruct Clone()
		{
			return new InterfaceProperty(this);
		}

		/// <summary>
		/// Returns all of the child IBaseConstructs in this node in no particular order.
		/// </summary>
		/// <returns>All of the child IBaseConstructs in this node in no particular order.</returns>
		protected override ReadOnlyCollection<IBaseConstruct> WalkChildrenInternal()
		{
			List<IBaseConstruct> children = new List<IBaseConstruct>();
			if (GetAccessor != null)
				children.Add(GetAccessor);
			if (SetAccessor != null)
				children.Add(SetAccessor);
			return children.AsReadOnly();
		}

		/// <summary>
		/// Adds a new child to this IBaseConstruct.
		/// </summary>
		/// <param name="childBC"></param>
		protected override void AddChildInternal(BaseConstruct childBC)
		{
			if (childBC == null)
				throw new InvalidOperationException("Cannot add null child");

			if (childBC is InterfaceAccessor)
			{
				InterfaceAccessor newAccessor = (InterfaceAccessor)childBC;
				if (newAccessor.AccessorType == InterfaceAccessor.AccessorTypes.Get)
					GetAccessor = newAccessor;
				else
					SetAccessor = newAccessor;
			}
			else
				throw new InvalidOperationException("Cannot add child of type " + childBC.GetType());
		}

		protected override bool CustomMergeStepInternal(BaseConstruct user, BaseConstruct newgen, BaseConstruct prevgen)
		{
			InterfaceProperty userBC = (InterfaceProperty)user, newgenBC = (InterfaceProperty)newgen, prevgenBC = (InterfaceProperty)prevgen;

			// Return Type
			if (!Utility.MergeDataType(ref DataType, userBC.DataType, newgenBC.DataType, prevgenBC.DataType))
				return false;
			// HasNewKeyword
			if (!Utility.MergeSingleItem(ref HasNewKeyword, userBC.HasNewKeyword, newgenBC.HasNewKeyword, prevgenBC.HasNewKeyword))
				return false;

			return true;
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct)
		{
			return IsTheSame(comparisonStruct as InterfaceProperty);
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct, ComparisonDepth depth)
		{
			return IsTheSame(comparisonStruct as InterfaceProperty, depth);
		}

		private bool IsTheSame(InterfaceProperty comparisonProperty)
		{
			return IsTheSame(comparisonProperty, ComparisonDepth.Signature);
		}

		private bool IsTheSame(InterfaceProperty comparisonProperty, ComparisonDepth depth)
		{
			if (comparisonProperty == null)
				return false;

			if (Name == comparisonProperty.Name)
			{
				// TODO: Parent Object Comparison
				//                if (ParentObject.IsTheSame(comparisonProperty.ParentObject))
				{
					if (depth == ComparisonDepth.Signature)
					{
						return true;
					}

					if (!base.IsTheSame(comparisonProperty, depth))
					{
						return false;
					}

					if (DataType != comparisonProperty.DataType) return false;

					if (depth == ComparisonDepth.Outer)
					{
						return true;
					}

					if (!GetAccessor.IsTheSame(comparisonProperty.GetAccessor))
					{
						ComparisonDifference += GetType().Name + ".GetAccessor";
						return false;
					}
					if (!SetAccessor.IsTheSame(comparisonProperty.SetAccessor))
					{
						ComparisonDifference += GetType().Name + ".SetAccessor";
						return false;
					}
					return true;
				}
			}
			return false;
		}
	}
}