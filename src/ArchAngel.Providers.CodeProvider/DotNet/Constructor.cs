using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArchAngel.Providers.CodeProvider.CSharp;

namespace ArchAngel.Providers.CodeProvider.DotNet
{
	public class Constructor : ParameterBase, IVisibilityModifiers, IBody
	{
		private List<string> _Modifiers = new List<string>();
		public string InitializerType = ""; // Allowed values: this, base
		public List<string> InitializerArguments = new List<string>();
		private string m_body = "{ }";

		public Constructor(Controller controller) : base(controller) { }

		public Constructor(Controller controller, string name)
			: base(controller)
		{
			Name = name;
		}

		public Constructor(Controller controller, BaseConstruct parentObject, CodeLanguage language, int nodeIndex)
			: base(controller)
		{
			ParentObject = parentObject;
			Language = language;
			Index = nodeIndex;
		}

		public Constructor(Controller controller, BaseConstruct parentObject, CodeLanguage language)
			: base(controller)
		{
			ParentObject = parentObject;
			Language = language;
		}

		private Constructor(Constructor iiToCopyFrom)
			: base(null)
		{
			iiToCopyFrom.CloneInto(this);

			InitializerType = iiToCopyFrom.InitializerType;
			InitializerArguments = new List<string>(iiToCopyFrom.InitializerArguments);
			Modifiers = new List<string>(iiToCopyFrom.Modifiers);
			CloneParameters(iiToCopyFrom);
		}

		/// <summary>
		/// Returns a shallow copy of the construct. Does not copy children.
		/// </summary>
		/// <returns>A shallow copy of the construct. Does not copy children.</returns>
		public override IBaseConstruct Clone()
		{
			return new Constructor(this);
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
			throw new InvalidOperationException("Cannot add child nodes to a Constructor");
		}

		public List<string> Modifiers
		{
			get { return _Modifiers; }
			set { _Modifiers = value; }
		}

		protected override bool CustomMergeStepParameterInternal(BaseConstruct user, BaseConstruct newgen, BaseConstruct prevgen)
		{
			Constructor userCon = (Constructor)user, newgenCon = (Constructor)newgen, prevgenCon = (Constructor)prevgen;

			// Modifiers
			if (Utility.MergeModifiers(this, userCon, newgenCon, prevgenCon) == false)
				return false;

			if (Utility.MergeSingleItem(ref InitializerType, userCon.InitializerType, newgenCon.InitializerType, prevgenCon.InitializerType) == false)
				return false;

			if (Utility.MergeStringCollections(InitializerArguments, userCon.InitializerArguments, newgenCon.InitializerArguments, prevgenCon.InitializerArguments) == false)
				return false;

			return true;
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct)
		{
			return IsTheSame(comparisonStruct as Constructor);
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct, ComparisonDepth depth)
		{
			return IsTheSame(comparisonStruct as Constructor, depth);
		}

		private bool IsTheSame(Constructor comparisonConstructor)
		{
			return IsTheSame(comparisonConstructor, ComparisonDepth.Signature);
		}

		private bool IsTheSame(Constructor comparisonConstructor, ComparisonDepth depth)
		{
			if (comparisonConstructor == null)
				return false;

			if (Name == comparisonConstructor.Name)
			{
				if (ParametersAreTheSame(comparisonConstructor, depth) == false) return false;

				// Function names are the same, so now compare the class names
				if (depth == ComparisonDepth.Signature)
				{
					return true;
				}

				if (!base.IsTheSame(comparisonConstructor, depth))
				{
					return false;
				}

				if (!Utility.StringCollectionsAreTheSame(Modifiers, comparisonConstructor.Modifiers))
				{
					ComparisonDifference += GetType().Name + ".Modifiers";
					return false;
				}

				if (depth == ComparisonDepth.Outer)
				{
					return true;
				}

				if (BodyText != comparisonConstructor.BodyText)
				{
					ComparisonDifference += GetType().Name + ".Body";
					return false;
				}

				if (!ParentObject.IsTheSame(comparisonConstructor.ParentObject))
				{
					ComparisonDifference += GetType().Name + ".Parent";
					return false;
				}

				if (!ArchAngel.Providers.CodeProvider.CSharp.Utility.BaseContructCollectionsAreTheSame(Attributes.ToArray(), comparisonConstructor.Attributes.ToArray()))
				{
					ComparisonDifference += GetType().Name + ".Attributes";
					return false;
				}

				return true;
			}
			return false;
		}

		public string BodyText
		{
			get { return m_body; }
			set { m_body = value; }
		}

		/// <summary>
		/// Returns the body of the BaseConstruct. If it doesn't have a body (for instance, it is a field), then the default behaviour is to return string.Empty;
		/// </summary>
		/// <returns></returns>
		public override string GetInnerText()
		{
			return BodyText;
		}

	}
}
