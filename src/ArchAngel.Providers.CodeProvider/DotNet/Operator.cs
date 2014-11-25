using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArchAngel.Providers.CodeProvider.CSharp;

namespace ArchAngel.Providers.CodeProvider.DotNet
{
	public class Operator : ParameterBase, IVisibilityModifiers, IBody
	{
		public DotNet.DataType DataType;
		private List<string> _Modifiers = new List<string>();
		protected string Body = "";

		public Operator(Controller controller)
			: base(controller)
		{
		}

		public Operator(Controller controller, string name, DotNet.DataType type, string modifier)
			: base(controller)
		{
			Name = name;
			DataType = type;
			Modifiers.Add(modifier);
		}

		public Operator(Controller controller, BaseConstruct parentObject, CodeLanguage language, int nodeIndex)
			: base(controller)
		{
			ParentObject = parentObject;
			Language = language;
			Index = nodeIndex;
		}

		private Operator(Operator opToCopyFrom)
			: base(null)
		{
			opToCopyFrom.CloneInto(this);
			DataType = opToCopyFrom.DataType.Clone();
			Modifiers = new List<string>(opToCopyFrom.Modifiers);
			foreach (Parameter p in opToCopyFrom.Parameters)
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
			return new Operator(this);
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

		/// <summary>
		/// The body text of the construct. Does not include child nodes, and should only be
		/// used on leaf nodes.
		/// </summary>
		public string BodyText
		{
			get { return Body; }
			set { Body = value; }
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

		/// <summary>
		/// Gets the outer text of the node. 
		/// </summary>
		/// <remarks>This is the method signature for a C# Function, the element text (<![CDATA[<element></element>]]>) for an XML Element, etc.</remarks>
		/// <returns>The outer text of the node.</returns>
		public override string GetOuterText()
		{
			return Controller.GetPrinterFor(this).GetOuterText();
		}

		/// <summary>
		/// Returns the body of the BaseConstruct. If it doesn't have a body (for instance, it is a field), then the default behaviour is to return string.Empty;
		/// </summary>
		/// <returns></returns>
		public override string GetInnerText()
		{
			return Body;
		}

		public override bool IsTheSame(BaseConstruct comparisonOperator)
		{
			return IsTheSame(comparisonOperator as Operator);
		}

		public override bool IsTheSame(BaseConstruct comparisonOperator, ComparisonDepth depth)
		{
			return IsTheSame(comparisonOperator as Operator, depth);
		}

		private bool IsTheSame(Operator comparisonOperator)
		{
			return IsTheSame(comparisonOperator, ComparisonDepth.Signature);
		}

		private bool IsTheSame(Operator comparisonOperator, ComparisonDepth depth)
		{
			if (comparisonOperator == null)
				return false;

			if (Name == comparisonOperator.Name)
			{
				// Function names are the same, so now compare the class names

				if (ParametersAreTheSame(comparisonOperator, depth) == false) return false;

				// TODO: Parent object comparison
				//                if (ParentObject.IsTheSame(comparisonOperator.ParentObject))
				{
					if (depth == ComparisonDepth.Signature)
					{
						return true;
					}

					if (DataType != comparisonOperator.DataType) return false;

					if (!base.IsTheSame(comparisonOperator, depth))
					{
						return false;
					}
					if (!Utility.StringCollectionsAreTheSame(Modifiers, comparisonOperator.Modifiers))
					{
						ComparisonDifference += GetType().Name + ".Modifiers";
						return false;
					}

					return true;
				}
			}
			return false;
		}

		protected override bool CustomMergeStepParameterInternal(BaseConstruct user, BaseConstruct newgen, BaseConstruct prevgen)
		{
			Operator userBC = (Operator)user, newgenBC = (Operator)newgen, prevgenBC = (Operator)prevgen;

			// DataType
			if (!Utility.MergeDataType(ref DataType, userBC.DataType, newgenBC.DataType, prevgenBC.DataType))
				return false;
			// Modifiers
			if (!Utility.MergeModifiers(this, userBC, newgenBC, prevgenBC))
				return false;
			return true;
		}
	}
}