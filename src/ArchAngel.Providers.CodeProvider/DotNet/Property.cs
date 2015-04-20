using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArchAngel.Providers.CodeProvider.CSharp;

namespace ArchAngel.Providers.CodeProvider.DotNet
{
	public class Property : BaseConstruct, IVisibilityModifiers
	{
		public DotNet.DataType DataType;
		private List<string> _Modifiers = new List<string>();
		public PropertyAccessor GetAccessor;
		public PropertyAccessor SetAccessor;

		public Property(Controller controller)
			: base(controller)
		{
		}

		public Property(Controller controller, string name, DataType dataType)
			: base(controller)
		{
			Name = name;
			DataType = dataType;
		}

		public Property(Controller controller, string name, DotNet.DataType dataType, string modifier)
			: base(controller)
		{
			Name = name;
			DataType = dataType;
			Modifiers.Add(modifier);
		}

		public Property(Controller controller, BaseConstruct parentObject, CodeLanguage language, int nodeIndex)
			: base(controller)
		{
			ParentObject = parentObject;
			Language = language;
			Index = nodeIndex;
		}

		private Property(Property propToCopyFrom)
			: base(null)
		{
			propToCopyFrom.CloneInto(this);
			DataType = propToCopyFrom.DataType.Clone();
			Modifiers = new List<string>(propToCopyFrom.Modifiers);
		}

		/// <summary>
		/// Returns a shallow copy of the construct. Does not copy children.
		/// </summary>
		/// <returns>A shallow copy of the construct. Does not copy children.</returns>
		public override IBaseConstruct Clone()
		{
			return new Property(this);
		}


		protected Property(Controller controller, BaseConstruct parentObject, string name, DotNet.DataType dataType, List<string> modifiers, PropertyAccessor getAccessor, PropertyAccessor setAccessor, CodeLanguage language, int nodeIndex)
			: base(controller)
		{
			ParentObject = parentObject;
			Name = name;
			DataType = dataType;
			Modifiers = modifiers;
			GetAccessor = getAccessor;
			SetAccessor = setAccessor;
			Language = language;
			Index = nodeIndex;
		}

		public List<string> Modifiers
		{
			get { return _Modifiers; }
			set { _Modifiers = value; }
		}

		protected override bool CustomMergeStepInternal(BaseConstruct user, BaseConstruct newgen, BaseConstruct prevgen)
		{
			Property userBC = (Property)user, newgenBC = (Property)newgen, prevgenBC = (Property)prevgen;

			// DataType
			if (!Utility.MergeDataType(ref DataType, userBC.DataType, newgenBC.DataType, prevgenBC.DataType))
				return false;
			// Modifiers
			if (!Utility.MergeModifiers(this, userBC, newgenBC, prevgenBC))
				return false;
			return true;
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct)
		{
			return IsTheSame(comparisonStruct as Property);
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct, ComparisonDepth depth)
		{
			return IsTheSame(comparisonStruct as Property, depth);
		}

		private bool IsTheSame(Property comparisonProperty)
		{
			return IsTheSame(comparisonProperty, ComparisonDepth.Signature);
		}

		private bool IsTheSame(Property comparisonProperty, ComparisonDepth depth)
		{
			if (comparisonProperty == null)
				return false;

			if (Name == comparisonProperty.Name)
			{
				if (depth == ComparisonDepth.Signature)
				{
					return true;
				}

				if (DataType != comparisonProperty.DataType) return false;
				if (!base.IsTheSame(comparisonProperty, depth))
				{
					return false;
				}
				if (!Utility.StringCollectionsAreTheSame(Modifiers, comparisonProperty.Modifiers))
				{
					ComparisonDifference += GetType().Name + ".Modifiers";
					return false;
				}

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
			return false;
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

			if (childBC is PropertyAccessor)
			{
				PropertyAccessor newAccessor = (PropertyAccessor)childBC;
				if (newAccessor.AccessorType == PropertyAccessor.AccessorTypes.Get)
					GetAccessor = newAccessor;
				else
					SetAccessor = newAccessor;
			}
			else
				throw new InvalidOperationException("Cannot add child of type " + childBC.GetType());
		}

		protected override void RemoveChildObjectInternal(BaseConstruct childBC)
		{
			if (childBC == null)
				throw new InvalidOperationException("Cannot remove null child");

			if (childBC is PropertyAccessor)
			{
				PropertyAccessor accessor = (PropertyAccessor)childBC;
				if (accessor == GetAccessor)
					GetAccessor = null;
				else if (accessor == SetAccessor)
					SetAccessor = null;
				else
					throw new InvalidOperationException("The accessor does not belong to this Property");
			}
			else
				throw new InvalidOperationException("Cannot remove child of type " + childBC.GetType());
		}
	}
}