using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArchAngel.Providers.CodeProvider.CSharp;
using ArchAngel.Providers.CodeProvider.DotNet;
using Attribute = ArchAngel.Providers.CodeProvider.DotNet.Attribute;

namespace ArchAngel.Providers.CodeProvider
{
	[Serializable]
	public class AttributeSection : BaseConstruct
	{
		public string Target = "";
		protected readonly List<Attribute> singleAttributes = new List<Attribute>();

		public AttributeSection(Controller controller)
			: base(controller)
		{
		}

		public void AddAttribute(Attribute attr)
		{
			singleAttributes.Add(attr);
			attr.ParentObject = this;
		}

		public ReadOnlyCollection<Attribute> SingleAttributes
		{
			get { return singleAttributes.AsReadOnly(); }
		}

		/// <summary>
		/// Returns a shallow copy of the construct. Does not copy children.
		/// </summary>
		/// <returns>A shallow copy of the construct. Does not copy children.</returns>
		public override IBaseConstruct Clone()
		{
			AttributeSection att = new AttributeSection(null);
			CloneInto(att);
			att.Target = Target;
			return att;
		}

		/// <summary>
		/// Returns all of the child IBaseConstructs in this node in no particular order.
		/// </summary>
		/// <returns>All of the child IBaseConstructs in this node in no particular order.</returns>
		protected override ReadOnlyCollection<IBaseConstruct> WalkChildrenInternal()
		{
			List<IBaseConstruct> children = new List<IBaseConstruct>();
			foreach (Attribute att in singleAttributes)
			{
				children.Add(att);
			}
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

			if (childBC is Attribute)
			{
				singleAttributes.Add((Attribute)childBC);
			}
			else
				throw new InvalidOperationException("Cannot add child of type " + childBC.GetType());
		}

		protected override bool CustomMergeStepInternal(BaseConstruct user, BaseConstruct newgen, BaseConstruct prevgen)
		{
			return Utility.MergeSingleItem(ref Target, ((AttributeSection)user).Target,
						((AttributeSection)newgen).Target, ((AttributeSection)prevgen).Target);
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct)
		{
			return IsTheSame(comparisonStruct as AttributeSection);
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct, ComparisonDepth depth)
		{
			return IsTheSame(comparisonStruct as AttributeSection, depth);
		}

		private bool IsTheSame(AttributeSection comparisonAttribute)
		{
			return IsTheSame(comparisonAttribute, ComparisonDepth.Signature);
		}

		private bool IsTheSame(AttributeSection comparisonAttribute, ComparisonDepth depth)
		{
			if (comparisonAttribute == null)
				return false;

			if (Name == comparisonAttribute.Name && singleAttributes.Count == comparisonAttribute.singleAttributes.Count)
			{
				Comparers.BaseConstructComparer comparer = new Comparers.BaseConstructComparer();
				if (!ArchAngel.Providers.CodeProvider.CSharp.Utility.BaseContructCollectionsAreTheSame(singleAttributes.ToArray(), comparisonAttribute.singleAttributes.ToArray(), comparer, depth))
				{
					ComparisonDifference += GetType().Name + ".SingleAttributes";
					return false;
				}

				if (depth == ComparisonDepth.Signature)
				{
					return true;
				}

				if (!base.IsTheSame(comparisonAttribute, depth))
				{
					return false;
				}

				if (!ArchAngel.Providers.CodeProvider.CSharp.Utility.BaseContructCollectionsAreTheSame(Attributes.ToArray(), comparisonAttribute.Attributes.ToArray()))
				{
					ComparisonDifference += GetType().Name + ".Attributes";
					return false;
				}

				return true;
			}
			return false;
		}
	}
}
