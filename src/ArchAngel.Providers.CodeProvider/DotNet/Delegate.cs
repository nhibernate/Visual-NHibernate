using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArchAngel.Providers.CodeProvider.CSharp;

namespace ArchAngel.Providers.CodeProvider.DotNet
{
	public class Delegate : ParameterBase, IVisibilityModifiers
	{
		public DataType ReturnType;
		private List<string> _Modifiers = new List<string>();
		public string GenericType = "";

		public Delegate(Controller controller) : base(controller) { }

		public Delegate(Controller controller, string name, DataType type, string modifier)
			: base(controller)
		{
			Name = name;
			ReturnType = type;
			_Modifiers.Add(modifier);
		}

		public Delegate(Controller controller, string name, List<string> modifiers, string genericType, IEnumerable<Parameter> parameters, DataType returnType, BaseConstruct.CodeLanguage language, BaseConstruct parentObject, int nodeIndex)
			: base(controller)
		{
			Name = name;
			ReturnType = returnType;
			GenericType = genericType ?? "";
			if (parameters != null)
				Parameters.AddRange(parameters);
			Language = language;
			Modifiers = modifiers;
			ParentObject = parentObject;
			Index = nodeIndex;
		}

		public Delegate(Controller controller, string name, DataType returnType)
			: base(controller)
		{
			Name = name;
			ReturnType = returnType;
		}

		private Delegate(Delegate propToCopyFrom)
			: base(null)
		{
			propToCopyFrom.CloneInto(this);
			ReturnType = propToCopyFrom.ReturnType.Clone();
			Modifiers = new List<string>(propToCopyFrom.Modifiers);
			GenericType = propToCopyFrom.GenericType;
			foreach (Parameter p in propToCopyFrom.Parameters)
			{
				Parameters.Add((Parameter)p.Clone());
			}
		}

		/// <summary>
		/// Returns a shallow copy of the construct. Does not copy children.
		/// </summary>
		/// <returns>A shallow copy of the construct. Does not copy children.</returns>
		public override IBaseConstruct Clone()
		{
			return new Delegate(this);
		}

		public List<string> Modifiers
		{
			get { return _Modifiers; }
			set { _Modifiers = value; }
		}

		protected override bool CustomMergeStepParameterInternal(BaseConstruct user, BaseConstruct newgen, BaseConstruct prevgen)
		{
			Delegate userBC = (Delegate)user, newgenBC = (Delegate)newgen, prevgenBC = (Delegate)prevgen;

			// Return Type
			if (!ArchAngel.Providers.CodeProvider.CSharp.Utility.MergeDataType(ref ReturnType, userBC.ReturnType, newgenBC.ReturnType, prevgenBC.ReturnType))
				return false;
			// Modifiers
			if (!ArchAngel.Providers.CodeProvider.CSharp.Utility.MergeModifiers(this, userBC, newgenBC, prevgenBC))
				return false;
			// Generic Type
			if (!ArchAngel.Providers.CodeProvider.CSharp.Utility.MergeSingleItem(ref GenericType, userBC.GenericType, newgenBC.GenericType, prevgenBC.GenericType))
				return false;

			return true;
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct)
		{
			return IsTheSame(comparisonStruct as Delegate);
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct, ComparisonDepth depth)
		{
			return IsTheSame(comparisonStruct as Delegate, depth);
		}

		private bool IsTheSame(Delegate comparisonDelegate)
		{
			return IsTheSame(comparisonDelegate, ComparisonDepth.Signature);
		}

		/// <summary>
		/// This is private so that the IsTheSameMethod in BaseConstruct is always called first.
		/// </summary>
		/// <param name="comparisonDelegate"></param>
		/// <param name="depth"></param>
		/// <returns></returns>
		private bool IsTheSame(Delegate comparisonDelegate, ComparisonDepth depth)
		{
			if (comparisonDelegate == null)
				return false;

			if (Name == comparisonDelegate.Name)
			{
				if (ParametersAreTheSame(comparisonDelegate, depth) == false) return false;

				if (depth == ComparisonDepth.Signature)
				{
					return true;
				}

				if (ReturnType != comparisonDelegate.ReturnType)
				{
					return false;
				}

				if (!base.IsTheSame(comparisonDelegate, depth))
				{
					return false;
				}

				if (!Utility.StringCollectionsAreTheSame(Modifiers, comparisonDelegate.Modifiers))
				{
					ComparisonDifference += GetType().Name + ".Modifiers";
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
			throw new InvalidOperationException("Cannot add child nodes to a Delegate");
		}

	}
}
