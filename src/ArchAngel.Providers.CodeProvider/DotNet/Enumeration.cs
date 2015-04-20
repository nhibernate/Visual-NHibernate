using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast;
using ArchAngel.Providers.CodeProvider.CSharp;

namespace ArchAngel.Providers.CodeProvider.DotNet
{
	public class Enumeration : BaseConstruct, IVisibilityModifiers
	{
		private List<string> _Modifiers = new List<string>();
		public string EnumBase = "";
		public List<EnumMember> Members = new List<EnumMember>();

		public Enumeration(Controller controller, string name, List<string> modifiers, string enumBase, BaseConstruct parentObject, CodeLanguage language, int nodeIndex)
			: base(controller)
		{
			Name = name;
			EnumBase = enumBase;
			Modifiers = modifiers;
			ParentObject = parentObject;
			Language = language;
			Index = nodeIndex;
		}

		public Enumeration(Controller controller)
			: base(controller)
		{
		}

		public Enumeration(Controller controller, string name)
			: base(controller)
		{
			Name = name;
		}

		private Enumeration(Enumeration enumToCopyFrom)
			: base(null)
		{
			enumToCopyFrom.CloneInto(this);
			foreach (string mod in enumToCopyFrom.Modifiers)
			{
				Modifiers.Add(mod);
			}
			EnumBase = enumToCopyFrom.EnumBase;
		}

		/// <summary>
		/// Returns a shallow copy of the construct. Does not copy children.
		/// </summary>
		/// <returns>A shallow copy of the construct. Does not copy children.</returns>
		public override IBaseConstruct Clone()
		{
			return new Enumeration(this);
		}

		/// <summary>
		/// Adds a new child to this IBaseConstruct.
		/// </summary>
		/// <param name="childBC"></param>
		protected override void AddChildInternal(BaseConstruct childBC)
		{
			if (childBC == null)
				throw new InvalidOperationException("Cannot add null child");

			if (childBC is EnumMember)
			{
				Members.Add(childBC as EnumMember);
			}
			else
				throw new InvalidOperationException("Cannot add child of type " + childBC.GetType());
		}

		/// <summary>
		/// Returns all of the child IBaseConstructs in this node in no particular order.
		/// </summary>
		/// <returns>All of the child IBaseConstructs in this node in no particular order.</returns>
		protected override ReadOnlyCollection<IBaseConstruct> WalkChildrenInternal()
		{
			List<IBaseConstruct> children = new List<IBaseConstruct>();
			foreach (EnumMember bc in Members)
			{
				children.Add(bc);
			}
			return children.AsReadOnly();
		}

		public List<string> Modifiers
		{
			get { return _Modifiers; }
			set { _Modifiers = value; }
		}

		public override bool IsTheSame(BaseConstruct comparisonEnum, ComparisonDepth depth)
		{
			return IsTheSame(comparisonEnum as Enumeration, depth);
		}

		protected override bool CustomMergeStepInternal(BaseConstruct user, BaseConstruct newgen, BaseConstruct prevgen)
		{
			Enumeration userBC = (Enumeration)user, newgenBC = (Enumeration)newgen, prevgenBC = (Enumeration)prevgen;

			// Modifiers
			if (!Utility.MergeModifiers(this, userBC, newgenBC, prevgenBC))
				return false;
			// Base
			if (!Utility.MergeSingleItem(ref EnumBase, userBC.EnumBase, newgenBC.EnumBase, prevgenBC.EnumBase))
				return false;

			return true;
		}

		public override bool IsTheSame(BaseConstruct comparisonEnum)
		{
			return IsTheSame(comparisonEnum as Enumeration);
		}

		private bool IsTheSame(Enumeration comparisonEnum)
		{
			return IsTheSame(comparisonEnum, ComparisonDepth.Signature);
		}

		private bool IsTheSame(Enumeration comparisonEnum, ComparisonDepth depth)
		{
			if (comparisonEnum == null)
				return false;

			if (Name == comparisonEnum.Name)
			{
				if ((ParentObject == null && comparisonEnum.ParentObject == null) ||
					(ParentObject.GetType() == comparisonEnum.ParentObject.GetType() &&
					ParentObject.Name == comparisonEnum.ParentObject.Name))
				{
					if (depth == ComparisonDepth.Signature)
					{
						return true;
					}

					if (!base.IsTheSame(comparisonEnum, depth))
					{
						return false;
					}

					if (!Utility.StringCollectionsAreTheSame(Modifiers, comparisonEnum.Modifiers))
					{
						ComparisonDifference += GetType().Name + ".Modifiers";
						return false;
					}

					if (depth == ComparisonDepth.Outer)
					{
						return true;
					}

					// Now check all child objects
					Comparers.BaseConstructComparer comparer = new Comparers.BaseConstructComparer();

					if (EnumBase != comparisonEnum.EnumBase)
					{
						return false;
					}
					if (!ArchAngel.Providers.CodeProvider.CSharp.Utility.BaseContructCollectionsAreTheSame(Members.ToArray(), comparisonEnum.Members.ToArray(), comparer))
					{
						ComparisonDifference += GetType().Name + ".Members";
						return false;
					}
					return true;
				}
			}
			return false;
		}

		public BaseConstruct[] SortedConstructs
		{
			get
			{
				ArrayList objects = new ArrayList();
				objects.AddRange(Members);

				BaseConstruct[] baseObjects = (BaseConstruct[])objects.ToArray(typeof(BaseConstruct));
				int[] indexes = new int[baseObjects.Length];

				for (int i = 0; i < baseObjects.Length; i++)
				{
					if (baseObjects[i].Index < 0)
					{
						throw new InvalidOperationException("Index has not been set in CSharpFormatter for type: " + baseObjects[i].GetType().Name);
					}
					indexes[i] = baseObjects[i].Index;
				}
				Array.Sort(indexes, baseObjects);
				return baseObjects;
			}
		}
		[Serializable]
		public class EnumMember : BaseConstruct
		{
			public string Value = "";

			public EnumMember(Controller controller, BaseConstruct parentObject, int nodeIndex)
				: base(controller)
			{
				ParentObject = parentObject;
				Index = nodeIndex;
			}

			public EnumMember(Controller controller, string name)
				: base(controller)
			{
				Name = name ?? "";
			}

			public EnumMember(Controller controller)
				: base(controller)
			{
			}

			public EnumMember(Controller controller, string name, string value)
				: this(controller, name)
			{
				Value = value ?? "";
			}

			private EnumMember(EnumMember emToCopyFrom)
				: base(null)
			{
				emToCopyFrom.CloneInto(this);
				Value = emToCopyFrom.Value;
			}

			/// <summary>
			/// Returns a shallow copy of the construct. Does not copy children.
			/// </summary>
			/// <returns>A shallow copy of the construct. Does not copy children.</returns>
			public override IBaseConstruct Clone()
			{
				return new EnumMember(this);
			}

			protected override bool CustomMergeStepInternal(BaseConstruct user, BaseConstruct newgen, BaseConstruct prevgen)
			{
				return Utility.MergeSingleItem(ref Value, ((EnumMember)user).Value, ((EnumMember)newgen).Value,
											   ((EnumMember)prevgen).Value);
			}

			/// <summary>
			/// Adds a new child to this IBaseConstruct.
			/// </summary>
			/// <param name="childBC">The child object to add</param>
			protected override void AddChildInternal(BaseConstruct childBC)
			{
				throw new InvalidOperationException("Cannot add child nodes to an InterfaceMethod");
			}

			//public override BaseConstruct[] SortedConstructs
			//{
			//    get { throw new InvalidOperationException(); }
			//}

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

			public override bool IsTheSame(BaseConstruct comparisonEnumMember, ComparisonDepth depth)
			{
				return IsTheSame((EnumMember)comparisonEnumMember, depth);
			}

			public override bool IsTheSame(BaseConstruct comparisonEnumMember)
			{
				return IsTheSame((EnumMember)comparisonEnumMember);
			}

			private bool IsTheSame(EnumMember comparisonEnumMember)
			{
				return IsTheSame(comparisonEnumMember, ComparisonDepth.Signature);
			}

			private bool IsTheSame(EnumMember comparisonEnumMember, ComparisonDepth depth)
			{
				if (Name == comparisonEnumMember.Name)
				{
					if ((ParentObject == null && comparisonEnumMember.ParentObject == null) ||
						(ParentObject.GetType() == comparisonEnumMember.ParentObject.GetType() &&
						ParentObject.Name == comparisonEnumMember.ParentObject.Name))
					{
						if (depth == ComparisonDepth.Signature)
						{
							return true;
						}

						if (!base.IsTheSame(comparisonEnumMember, depth))
						{
							return false;
						}
						return true;
					}
				}
				return false;
			}

		}

	}

}