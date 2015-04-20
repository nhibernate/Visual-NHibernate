using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArchAngel.Providers.CodeProvider.CSharp;

namespace ArchAngel.Providers.CodeProvider.DotNet
{
	public class Attribute : BaseConstruct
	{
		public Attribute(Controller controller)
			: base(controller)
		{
			PreceedingBlankLines = -1;
		}

		#region Inner Classes
		[Serializable]
		public class NamedArgument : IEquatable<NamedArgument>
		{
			public string Name = "";
			public string Value = "";

			public NamedArgument(string name, string value)
			{
				Name = name ?? "";
				Value = value ?? "";
			}

			public NamedArgument Clone()
			{
				return new NamedArgument(Name, Value);
			}

			public bool Equals(NamedArgument obj)
			{
				return Equals(obj.Name, Name) && Equals(obj.Value, Value);
			}

			public override bool Equals(object obj)
			{
				if (ReferenceEquals(null, obj)) return false;
				if (ReferenceEquals(this, obj)) return true;
				if (obj.GetType() != typeof(NamedArgument)) return false;
				return Equals((NamedArgument)obj);
			}

			public override int GetHashCode()
			{
				return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (Value != null ? Value.GetHashCode() : 0);
			}
		}
		#endregion

		#region Fields
		public List<string> PositionalArguments = new List<string>();
		public List<NamedArgument> NamedArguments = new List<NamedArgument>();
		#endregion

		/// <summary>
		/// Returns a shallow copy of the construct. Does not copy children.
		/// </summary>
		/// <returns>A shallow copy of the construct. Does not copy children.</returns>
		public override IBaseConstruct Clone()
		{
			Attribute newAttr = new Attribute(null);
			CloneInto(newAttr);

			newAttr.PositionalArguments = new List<string>(PositionalArguments);
			foreach (Attribute.NamedArgument na in NamedArguments)
			{
				newAttr.NamedArguments.Add(na.Clone());
			}
			return newAttr;
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

		public override string FullyQualifiedIdentifer
		{
			get
			{
				if (ParentObject is AttributeSection)
				{
					if (ParentObject.ParentObject != null)
						return string.Format("{0}{2}[{1}]", ParentObject.ParentObject.FullyQualifiedIdentifer, Name, BaseConstructConstants.FullyQualifiedIdentifierSeparator);
					return string.Format("[{0}]", Name);
				}

				if (ParentObject != null && ParentObject is CSharpParser.CodeRootBaseConstructAdapter == false)
					return string.Format("{0}{2}[{1}]", ParentObject.FullyQualifiedIdentifer, Name, BaseConstructConstants.FullyQualifiedIdentifierSeparator);

				return string.Format("[{0}]", Name);
			}
		}

		protected override bool CustomMergeStepInternal(BaseConstruct user, BaseConstruct newgen, BaseConstruct prevgen)
		{
			Attribute userBC = (Attribute)user, newgenBC = (Attribute)newgen, prevgenBC = (Attribute)prevgen;
			// PositionalArguments
			if (!Utility.MergeStringCollections(PositionalArguments, userBC.PositionalArguments, newgenBC.PositionalArguments, prevgenBC.PositionalArguments))
				return false;

			// NamedArguments            
			bool userChange = !prevgenBC.NamedArgumentCollectionsAreTheSame(userBC);
			bool templateChange = !prevgenBC.NamedArgumentCollectionsAreTheSame(newgenBC);

			NamedArguments.Clear();

			if (user.Equals(newgen))
			{
				userChange = true;
				templateChange = false;
			}

			if (templateChange && userChange)
			{
				return false;
			}

			if (userChange)
			{
				NamedArguments.AddRange(userBC.NamedArguments);
			}
			else if (templateChange)
			{
				NamedArguments.AddRange(newgenBC.NamedArguments);
			}

			return true;
		}

		private bool NamedArgumentCollectionsAreTheSame(Attribute other)
		{
			if (NamedArguments == null && other.NamedArguments == null)
				return true;

			if (NamedArguments == null || other.NamedArguments == null)
				return false;

			// Modifiers
			if (NamedArguments.Count != other.NamedArguments.Count)
			{
				return false;
			}
			NamedArguments.Sort();
			other.NamedArguments.Sort();

			for (int i = 0; i < NamedArguments.Count; i++)
			{
				if (NamedArguments[i] != other.NamedArguments[i])
				{
					return false;
				}
			}
			return true;
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct)
		{
			return IsTheSame(comparisonStruct as Attribute);
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct, ComparisonDepth depth)
		{
			return IsTheSame(comparisonStruct as Attribute, depth);
		}

		private bool IsTheSame(Attribute comparisonAttribute)
		{
			return IsTheSame(comparisonAttribute, ComparisonDepth.Signature);
		}

		private bool IsTheSame(Attribute comparisonAttribute, ComparisonDepth depth)
		{
			if (comparisonAttribute == null)
				return false;

			if (Name == comparisonAttribute.Name)
			{
				if (depth == ComparisonDepth.Signature)
				{
					return true;
				}

				if (!Utility.StringCollectionsAreTheSame(PositionalArguments, comparisonAttribute.PositionalArguments))
				{
					ComparisonDifference += GetType().Name + ".PositionalArguments";
					return false;
				}

				if (NamedArguments.Count != comparisonAttribute.NamedArguments.Count)
					return false;
				for (int i = 0; i < NamedArguments.Count; i++)
				{
					if (NamedArguments[i].Name != comparisonAttribute.NamedArguments[i].Name ||
						NamedArguments[i].Value != comparisonAttribute.NamedArguments[i].Value)
					{
						return false;
					}
				}

				if (!base.IsTheSame(comparisonAttribute, depth))
				{
					return false;
				}
				return true;
			}
			return false;
		}
	}
}
