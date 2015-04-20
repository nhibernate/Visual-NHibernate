using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArchAngel.Providers.CodeProvider.CSharp;

namespace ArchAngel.Providers.CodeProvider.DotNet
{
	public class Destructor : BaseConstruct, IBody
	{
		protected string bodyText;
		protected bool isExtern;

		public Destructor(Controller controller)
			: base(controller)
		{
		}

		public Destructor(Controller controller, string name)
			: base(controller)
		{
			Name = name;
		}

		private Destructor(Destructor iiToCopyFrom)
			: base(null)
		{
			iiToCopyFrom.CloneInto(this);
		}

		public override IBaseConstruct Clone()
		{
			return new Destructor(this);
		}

		protected override bool CustomMergeStepInternal(BaseConstruct user, BaseConstruct newgen, BaseConstruct prevgen)
		{
			Destructor userCon = (Destructor)user, newgenCon = (Destructor)newgen, prevgenCon = (Destructor)prevgen;

			// IsExtern
			if (Utility.MergeSingleItem(ref isExtern, userCon.IsExtern, newgenCon.IsExtern, prevgenCon.IsExtern) == false)
				return false;
			IsExtern = isExtern; // Triggers the replacement of BodyText if needed.

			return true;
		}

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
			throw new InvalidOperationException("Cannot add child nodes to a Destructor");
		}

		public string BodyText
		{
			get { return isExtern ? ";" : bodyText; }
			set { bodyText = value; }
		}

		public bool IsExtern
		{
			get { return isExtern; }
			set
			{
				isExtern = value;
			}
		}
	}
}
