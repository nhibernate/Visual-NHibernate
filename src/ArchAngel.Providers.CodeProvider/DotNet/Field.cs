using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArchAngel.Providers.CodeProvider.CSharp;

namespace ArchAngel.Providers.CodeProvider.DotNet
{
	public class Field : BaseConstruct, IVisibilityModifiers
	{
		public DataType DataType;
		private List<string> _Modifiers = new List<string>();
		public string InitialValue = "";

		public Field(Controller controller)
			: base(controller)
		{
		}

		public Field(Controller controller, DataType type, string name, string modifier)
			: base(controller)
		{
			DataType = type;
			Name = name;
			_Modifiers.Add(modifier);
		}

		public Field(Controller controller, BaseConstruct parentObject, CodeLanguage language, int nodeIndex)
			: base(controller)
		{
			ParentObject = parentObject;
			Language = language;
			Index = nodeIndex;
		}

		public Field(Controller controller, BaseConstruct parentObject, CodeLanguage language)
			: base(controller)
		{
			ParentObject = parentObject;
			Language = language;
		}

		private Field(Field fieldToCopyFrom)
			: base(null)
		{
			fieldToCopyFrom.CloneInto(this);
			DataType = fieldToCopyFrom.DataType.Clone();
			Modifiers = new List<string>(fieldToCopyFrom.Modifiers);
			InitialValue = fieldToCopyFrom.InitialValue;
		}

		/// <summary>
		/// Returns a shallow copy of the construct. Does not copy children.
		/// </summary>
		/// <returns>A shallow copy of the construct. Does not copy children.</returns>
		public override IBaseConstruct Clone()
		{
			return new Field(this);
		}

		/// <summary>
		/// True if the construct is a leaf node, and can contain no children.
		/// </summary>
		public override bool IsLeaf
		{
			get { return true; }
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
			throw new InvalidOperationException("Cannot add child nodes to a Field");
		}

		public List<string> Modifiers
		{
			get { return _Modifiers; }
			set { _Modifiers = value; }
		}

		protected override bool CustomMergeStepInternal(BaseConstruct user, BaseConstruct newgen, BaseConstruct prevgen)
		{
			Field userBC = (Field)user, newgenBC = (Field)newgen, prevgenBC = (Field)prevgen;

			// DataType
			if (!Utility.MergeDataType(ref DataType, userBC.DataType, newgenBC.DataType, prevgenBC.DataType))
				return false;
			// Modifiers
			if (!Utility.MergeModifiers(this, userBC, newgenBC, prevgenBC))
				return false;
			// InitialValue
			if (!Utility.MergeSingleItem(ref InitialValue, userBC.InitialValue, newgenBC.InitialValue, prevgenBC.InitialValue))
				return false;

			return true;
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct)
		{
			return IsTheSame(comparisonStruct as Field);
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct, ComparisonDepth depth)
		{
			return IsTheSame(comparisonStruct as Field, depth);
		}

		private bool IsTheSame(Field comparisonField)
		{
			return IsTheSame(comparisonField, ComparisonDepth.Signature);
		}

		private bool IsTheSame(Field comparisonField, ComparisonDepth depth)
		{
			if (comparisonField == null)
				return false;

			if (Name == comparisonField.Name)
			{
				// TODO: Why are these checks on parents here?
				//                if (ParentObject.IsTheSame(comparisonField.ParentObject))
				{
					if (depth == ComparisonDepth.Signature)
					{
						return true;
					}

					if (!base.IsTheSame(comparisonField, depth))
					{
						return false;
					}
					if (!Utility.StringCollectionsAreTheSame(Modifiers, comparisonField.Modifiers))
					{
						ComparisonDifference += GetType().Name + ".Modifiers";
						return false;
					}

					if (DataType != comparisonField.DataType)
						return false;

					if (depth == ComparisonDepth.Outer)
					{
						return true;
					}

					if (InitialValue != comparisonField.InitialValue)
					{
						ComparisonDifference += GetType().Name + ".InitialValue";
						return false;
					}
					return true;
				}
			}
			return false;
		}
	}
}
