using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArchAngel.Providers.CodeProvider.CSharp;

namespace ArchAngel.Providers.CodeProvider.DotNet
{
	[Serializable]
	public class Function : ParameterBase, IVisibilityModifiers, IBody
	{
		protected string Body = "";
		protected internal string FormattedBody = "";
		public DataType ReturnType;
		private List<string> _Modifiers = new List<string>();
		public readonly List<string> GenericParameters = new List<string>();
		public string GenericConstraintClause;

		public Function(Controller controller)
			: base(controller)
		{
		}

		public Function(Controller controller, String functionName, DataType returnType, Parameter param)
			: base(controller)
		{
			Name = functionName;
			ReturnType = returnType;
			Parameters.Add(param);
		}

		public Function(Controller controller, string name)
			: base(controller)
		{
			Name = name;
		}

		public Function(Controller controller, string name, DataType type)
			: base(controller)
		{
			Name = name;
			ReturnType = type;
		}

		public Function(Controller controller, BaseConstruct parentObject, int nodeIndex)
			: base(controller)
		{
			ParentObject = parentObject;
			Language = CodeLanguage.CSharp;
			Index = nodeIndex;
		}

		public Function(Controller controller, BaseConstruct parentObject)
			: base(controller)
		{
			ParentObject = parentObject;
			Language = CodeLanguage.CSharp;
		}

		private Function(Function funcToCopyFrom)
			: base(null)
		{
			funcToCopyFrom.CloneInto(this);
			ReturnType = funcToCopyFrom.ReturnType.Clone();
			Modifiers = new List<string>(funcToCopyFrom.Modifiers);

			foreach (Parameter p in funcToCopyFrom.Parameters)
				Parameters.Add((Parameter)p.Clone());
		}


		/// <summary>
		/// Returns a shallow copy of the construct. Does not copy children.
		/// </summary>
		/// <returns>A shallow copy of the construct. Does not copy children.</returns>
		public override IBaseConstruct Clone()
		{
			return new Function(this);
		}

		/// <summary>
		/// True if the construct is a leaf node, and can contain no children.
		/// </summary>
		public override bool IsLeaf
		{
			get { return true; }
		}

		public List<string> Modifiers
		{
			get { return _Modifiers; }
			set { _Modifiers = value; }
		}

		public string BodyText
		{
			get { return Body; }
			set { Body = FormattedBody = value; }
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

		protected override bool CustomMergeStepParameterInternal(BaseConstruct user, BaseConstruct newgen, BaseConstruct prevgen)
		{
			Function userBC = (Function)user, newgenBC = (Function)newgen, prevgenBC = (Function)prevgen;

			// ReturnType
			if (!Utility.MergeDataType(ref ReturnType, userBC.ReturnType, newgenBC.ReturnType, prevgenBC.ReturnType))
				return false;

			// Modifiers
			if (!Utility.MergeModifiers(this, userBC, newgenBC, prevgenBC))
				return false;

			return true;
		}

		/// <summary>
		/// Adds a new child to this IBaseConstruct.
		/// </summary>
		/// <param name="childBC">The child object to add</param>
		protected override void AddChildInternal(BaseConstruct childBC)
		{
			throw new InvalidOperationException("Cannot add child nodes to a Function");
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct)
		{
			return IsTheSame(comparisonStruct as Function);
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct, ComparisonDepth depth)
		{
			return IsTheSame(comparisonStruct as Function, depth);
		}

		private bool IsTheSame(Function comparisonFunction)
		{
			return IsTheSame(comparisonFunction, ComparisonDepth.Signature);
		}

		private bool IsTheSame(Function comparisonFunction, ComparisonDepth depth)
		{
			if (comparisonFunction == null)
				return false;

			if (Name == comparisonFunction.Name)
			{
				if (!ParametersAreTheSame(comparisonFunction, depth)) return false;
				// Function names are the same, so now compare the class names
				// TODO: Parent comparison
				//                if (ParentObject.IsTheSame(comparisonFunction.ParentObject))
				{
					if (depth == ComparisonDepth.Signature)
						return true;

					if (ReturnType != comparisonFunction.ReturnType)
						return false;

					if (!base.IsTheSame(comparisonFunction, depth))
						return false;

					if (!Utility.StringCollectionsAreTheSame(Modifiers, comparisonFunction.Modifiers))
					{
						ComparisonDifference += GetType().Name + ".Modifiers";
						return false;
					}
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Returns the body of the BaseConstruct. If it doesn't have a body (for instance, it is a field), then the default behaviour is to return string.Empty;
		/// </summary>
		/// <returns></returns>
		public override string GetInnerText()
		{
			return FormattedBody ?? Body;
		}
	}
}