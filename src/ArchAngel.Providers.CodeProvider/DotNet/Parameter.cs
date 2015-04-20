using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArchAngel.Providers.CodeProvider.CSharp;

namespace ArchAngel.Providers.CodeProvider.DotNet
{
	public class Parameter : BaseConstruct, IVisibilityModifiers
	{
		public string DataType;
		public bool IsParams = false;
		private List<string> _Modifiers = new List<string>();

		public Parameter(Controller controller)
			: base(controller)
		{
			PreceedingBlankLines = -1;
		}

		public Parameter(Controller controller, string dataType, string name)
			: this(controller)
		{
			DataType = dataType;
			Name = name;
		}

		public Parameter(BaseConstruct parentObject, CodeLanguage language)
			: this(parentObject.Controller)
		{
			ParentObject = parentObject;
			Language = language;
		}

		public Parameter(Controller controller, BaseConstruct parentObject, string name, string dataType, CodeLanguage language)
			: this(controller)
		{
			ParentObject = parentObject;
			Name = name;
			DataType = dataType;
			Language = language;
		}

		private Parameter(Parameter paramToCopyFrom)
			: base(null)
		{
			paramToCopyFrom.CloneInto(this);
			PreceedingBlankLines = -1;
			DataType = paramToCopyFrom.DataType;
			IsParams = paramToCopyFrom.IsParams;
			Modifiers = new List<string>(paramToCopyFrom.Modifiers);
		}


		/// <summary>
		/// Returns a shallow copy of the construct. Does not copy children.
		/// </summary>
		/// <returns>A shallow copy of the construct. Does not copy children.</returns>
		public override IBaseConstruct Clone()
		{
			return new Parameter(this);
		}

		public List<string> Modifiers
		{
			get { return _Modifiers; }
			set { _Modifiers = value; }
		}
		protected override bool CustomMergeStepInternal(BaseConstruct user, BaseConstruct newgen, BaseConstruct prevgen)
		{
			Parameter userBC = (Parameter)user, newgenBC = (Parameter)newgen, prevgenBC = (Parameter)prevgen;

			// DataType
			if (!Utility.MergeSingleItem(ref DataType, userBC.DataType, newgenBC.DataType, prevgenBC.DataType))
				return false;
			// IsParams
			if (!Utility.MergeSingleItem(ref IsParams, userBC.IsParams, newgenBC.IsParams, prevgenBC.IsParams))
				return false;
			// Modifiers
			if (!Utility.MergeModifiers(this, userBC, newgenBC, prevgenBC))
				return false;

			return true;
		}


		public override bool IsTheSame(BaseConstruct comparisonStruct)
		{
			return IsTheSame(comparisonStruct as Parameter, ComparisonDepth.Complete);
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct, ComparisonDepth depth)
		{
			return IsTheSame(comparisonStruct as Parameter, depth);
		}

		private bool IsTheSame(Parameter comparisonParameter, ComparisonDepth depth)
		{
			if (comparisonParameter == null)
				return false;

			if (Name != comparisonParameter.Name)
			{
				ComparisonDifference += GetType().Name + ".Name";
				return false;
			}

			if (depth == ComparisonDepth.Signature) return true;

			if (DataType != comparisonParameter.DataType)
			{
				ComparisonDifference += GetType().Name + ".DataType";
				return false;
			}

			if (!ArchAngel.Providers.CodeProvider.CSharp.Utility.BaseContructCollectionsAreTheSame(Attributes.ToArray(), comparisonParameter.Attributes.ToArray()))
			{
				ComparisonDifference += GetType().Name + ".Attributes";
				return false;
			}

			return true;
		}



		/// <summary>
		/// Returns all of the child IBaseConstructs in this node in no particular order.
		/// </summary>
		/// <returns>All of the child IBaseConstructs in this node in no particular order.</returns>
		protected override ReadOnlyCollection<IBaseConstruct> WalkChildrenInternal()
		{
			return new ReadOnlyCollection<IBaseConstruct>(new List<IBaseConstruct>());
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
	}
}
