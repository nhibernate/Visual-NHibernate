using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArchAngel.Providers.CodeProvider.CSharp;

namespace ArchAngel.Providers.CodeProvider.DotNet
{
	public class UsingStatement : BaseConstruct, IEquatable<UsingStatement>
	{
		public string Alias = "";
		public string Value = "";

		public UsingStatement(Controller controller)
			: base(controller)
		{
			PreceedingBlankLines = -1;
		}

		public override string ToString()
		{
			if (string.IsNullOrEmpty(Alias))
				return string.Format("{0}", Value);
			else
				return string.Format("{0} = {1}", Alias, Value);
		}

		public UsingStatement(Controller controller, string alias, string value)
			: this(controller)
		{
			Alias = alias ?? "";
			Value = value ?? "";
		}

		private UsingStatement(UsingStatement nsToCopyFrom)
			: base(null)
		{
			nsToCopyFrom.CloneInto(this);
			Alias = nsToCopyFrom.Alias;
			Value = nsToCopyFrom.Value;
		}
		/// <summary>
		/// Returns a shallow copy of the construct. Does not copy children.
		/// </summary>
		/// <returns>A shallow copy of the construct. Does not copy children.</returns>
		public override IBaseConstruct Clone()
		{
			return new UsingStatement(this);
		}


		/// <summary>
		/// Adds a new child to this IBaseConstruct.
		/// </summary>
		/// <param name="childBC">The child object to add</param>
		protected override void AddChildInternal(BaseConstruct childBC)
		{
			throw new InvalidOperationException("Cannot add child nodes to an InterfaceMethod");
		}

		public bool Equals(UsingStatement usingStatement)
		{
			if (usingStatement == null) return false;
			if (!Equals(Alias, usingStatement.Alias)) return false;
			if (!Equals(Value, usingStatement.Value)) return false;
			if (!Comments.IsTheSame(usingStatement.Comments)) return false;
			return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as UsingStatement);
		}

		public override int GetHashCode()
		{
			int result = Alias != null ? Alias.GetHashCode() : 0;
			result = 29 * result + (Value != null ? Value.GetHashCode() : 0);
			result = 29 * result + (Comments != null ? Comments.GetHashCode() : 0);
			return result;
		}


		protected override bool CustomMergeStepInternal(BaseConstruct user, BaseConstruct newgen, BaseConstruct prevgen)
		{
			UsingStatement userBC = (UsingStatement)user, newgenBC = (UsingStatement)newgen, prevgenBC = (UsingStatement)prevgen;

			// Alias
			if (!Utility.MergeSingleItem(ref Alias, userBC.Alias, newgenBC.Alias, prevgenBC.Alias))
				return false;
			// Value
			if (!Utility.MergeSingleItem(ref Value, userBC.Value, newgenBC.Value, prevgenBC.Value))
				return false;

			return true;
		}

		public override bool IsTheSame(BaseConstruct comparisonObject)
		{
			return IsTheSame(comparisonObject as UsingStatement, ComparisonDepth.Signature);
		}

		public override bool IsTheSame(BaseConstruct comparisonObject, ComparisonDepth depth)
		{
			return IsTheSame(comparisonObject as UsingStatement, depth);
		}

		private bool IsTheSame(UsingStatement comparisonObject, ComparisonDepth depth)
		{
			if (comparisonObject == null) return false;

			if (!base.IsTheSame(comparisonObject, depth))
			{
				return false;
			}
			if (Alias != comparisonObject.Alias)
			{
				ComparisonDifference += ".Alias";
				return false;
			}
			if (Value != comparisonObject.Value)
			{
				ComparisonDifference += ".Value";
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
			return new List<IBaseConstruct>().AsReadOnly();
		}

		/// <summary>
		/// True if the construct is a leaf node, and can contain no children.
		/// </summary>
		public override bool IsLeaf
		{
			get { return true; }
		}


	}
}
