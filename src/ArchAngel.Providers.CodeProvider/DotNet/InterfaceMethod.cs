using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArchAngel.Providers.CodeProvider.CSharp;

namespace ArchAngel.Providers.CodeProvider.DotNet
{
	/// <summary>
	/// Summary description for InterfaceMethod.
	/// </summary>
	[Serializable]
	public class InterfaceMethod : ParameterBase
	{
		protected DotNet.DataType returnType;
		protected bool hasNewKeyword = false;
		public readonly List<string> GenericParameters = new List<string>();
		public string GenericConstraintClause;

		public InterfaceMethod(Controller controller, BaseConstruct parentObject, string name, DotNet.DataType returnType, bool hasNewKeyword, IEnumerable<Parameter> parameters, CodeLanguage language, int nodeIndex)
			: base(controller)
		{
			ParentObject = parentObject;
			Name = name;
			this.returnType = returnType;
			this.hasNewKeyword = hasNewKeyword;
			Parameters.Clear();
			if (parameters != null)
				Parameters.AddRange(parameters);
			Language = language;
			Index = nodeIndex;
		}

		public InterfaceMethod(Controller controller, string name)
			: base(controller)
		{
			Name = name;
		}

		public InterfaceMethod(Controller controller, string name, DotNet.DataType type)
			: base(controller)
		{
			Name = name;
			returnType = type;
		}

		private InterfaceMethod(InterfaceMethod imToCopyFrom)
			: base(null)
		{
			imToCopyFrom.CloneInto(this);
			returnType = imToCopyFrom.returnType.Clone();
			hasNewKeyword = imToCopyFrom.hasNewKeyword;
			foreach (Parameter p in imToCopyFrom.Parameters)
			{
				Parameters.Add((Parameter)p.Clone());
			}
		}

		public DotNet.DataType ReturnType
		{
			get { return returnType; }
			set { returnType = value; }
		}

		public bool HasNewKeyword
		{
			get { return hasNewKeyword; }
			set { hasNewKeyword = value; }
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
			return new InterfaceMethod(this);
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

		public override bool IsTheSame(BaseConstruct comparisonStruct)
		{
			return IsTheSame(comparisonStruct as InterfaceMethod);
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct, ComparisonDepth depth)
		{
			return IsTheSame(comparisonStruct as InterfaceMethod, depth);
		}

		private bool IsTheSame(InterfaceMethod comparisonFunction)
		{
			return IsTheSame(comparisonFunction, ComparisonDepth.Signature);
		}

		private bool IsTheSame(InterfaceMethod comparisonFunction, ComparisonDepth depth)
		{
			if (comparisonFunction == null)
				return false;

			if (Name == comparisonFunction.Name)
			{
				if (ParametersAreTheSame(comparisonFunction, depth) == false) return false;

				// Function names are the same, so now compare the class names
				Interface thisParentClass = (Interface)ParentObject;
				Interface comparisonParentClass = (Interface)comparisonFunction.ParentObject;

				if (thisParentClass.IsTheSame(comparisonParentClass))
				{
					if (depth == ComparisonDepth.Signature)
					{
						return true;
					}

					if (returnType != comparisonFunction.returnType) return false;

					if (hasNewKeyword != comparisonFunction.hasNewKeyword)
					{
						ComparisonDifference += GetType().Name + ".HasNewKeyword";
						return false;
					}

					if (!base.IsTheSame(comparisonFunction, depth))
					{
						return false;
					}
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Adds a new child to this IBaseConstruct.
		/// </summary>
		/// <param name="childBC">The child object to add</param>
		protected override void AddChildInternal(BaseConstruct childBC)
		{
			throw new InvalidOperationException("Cannot add child nodes to an InterfaceMethod");
		}

		protected override bool CustomMergeStepParameterInternal(BaseConstruct user, BaseConstruct newgen, BaseConstruct prevgen)
		{
			InterfaceMethod userBC = (InterfaceMethod)user, newgenBC = (InterfaceMethod)newgen, prevgenBC = (InterfaceMethod)prevgen;

			// Return Type
			if (!Utility.MergeDataType(ref returnType, userBC.returnType, newgenBC.returnType, prevgenBC.returnType))
				return false;
			// HasNewKeyword
			if (!Utility.MergeSingleItem(ref hasNewKeyword, userBC.HasNewKeyword, newgenBC.HasNewKeyword, prevgenBC.HasNewKeyword))
				return false;

			return true;
		}
	}
}