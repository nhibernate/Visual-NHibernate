using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArchAngel.Providers.CodeProvider.CSharp;

namespace ArchAngel.Providers.CodeProvider.DotNet
{
	public class InterfaceIndexer : DotNet.ParameterBase
	{
		protected DotNet.DataType dataType;
		protected bool hasNewKeyword = false;
		public InterfaceAccessor GetAccessor;
		public InterfaceAccessor SetAccessor;

		public InterfaceIndexer(Controller controller)
			: base(controller)
		{
		}

		public InterfaceIndexer(Controller controller, DotNet.DataType type, bool isNew)
			: this(controller)
		{
			dataType = type;
			hasNewKeyword = isNew;
		}

		public InterfaceIndexer(Controller controller, BaseConstruct parentObject, DotNet.DataType dataType, bool hasNewKeyword, IEnumerable<Parameter> parameters, InterfaceAccessor getAccessor, InterfaceAccessor setAccessor, CodeLanguage language, int nodeIndex)
			: this(controller)
		{
			ParentObject = parentObject;
			this.dataType = dataType;
			this.hasNewKeyword = hasNewKeyword;
			if (parameters != null)
				Parameters.AddRange(parameters);
			GetAccessor = getAccessor;
			SetAccessor = setAccessor;
			Language = language;
			Index = nodeIndex;
		}

		private InterfaceIndexer(InterfaceIndexer iiToCopyFrom)
			: base(null)
		{
			iiToCopyFrom.CloneInto(this);
			if (iiToCopyFrom.dataType != null)
			{
				dataType = iiToCopyFrom.dataType.Clone();
			}
			hasNewKeyword = iiToCopyFrom.hasNewKeyword;
			foreach (Parameter p in iiToCopyFrom.Parameters)
			{
				Parameters.Add((Parameter)p.Clone());
			}
		}

		public override string DisplayName
		{
			get
			{
				return string.Format("Indexer [{0}]", string.Join(", ", GetParameterTypeStrings()));
			}
		}

		public override string FullyQualifiedDisplayName
		{
			get
			{
				return (ParentObject != null) ? ParentObject.FullyQualifiedDisplayName + "." + DisplayName : DisplayName;
			}
		}

		public override string FullyQualifiedIdentifer
		{
			get
			{
				if (ParentObject != null && ParentObject is CSharpParser.CodeRootBaseConstructAdapter == false)
					return ParentObject.FullyQualifiedIdentifer + BaseConstructConstants.FullyQualifiedIdentifierSeparator + DisplayName;

				return DisplayName;
			}
		}

		public bool HasNewKeyword
		{
			get { return hasNewKeyword; }
			set { hasNewKeyword = value; }
		}

		public DotNet.DataType DataType
		{
			get { return dataType; }
			set { dataType = value; }
		}

		/// <summary>
		/// Returns a shallow copy of the construct. Does not copy children.
		/// </summary>
		/// <returns>A shallow copy of the construct. Does not copy children.</returns>
		public override IBaseConstruct Clone()
		{
			return new InterfaceIndexer(this);
		}

		/// <summary>
		/// Returns all of the child IBaseConstructs in this node in no particular order.
		/// </summary>
		/// <returns>All of the child IBaseConstructs in this node in no particular order.</returns>
		protected override ReadOnlyCollection<IBaseConstruct> WalkChildrenInternal()
		{
			List<IBaseConstruct> children = new List<IBaseConstruct>();
			if (GetAccessor != null)
				children.Add(GetAccessor);
			if (SetAccessor != null)
				children.Add(SetAccessor);
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

			if (childBC is InterfaceAccessor)
			{
				InterfaceAccessor newAccessor = (InterfaceAccessor)childBC;
				if (newAccessor.AccessorType == InterfaceAccessor.AccessorTypes.Get)
					GetAccessor = newAccessor;
				else
					SetAccessor = newAccessor;
			}
			else
				throw new InvalidOperationException("Cannot add child of type " + childBC.GetType());
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct)
		{
			return IsTheSame(comparisonStruct as InterfaceIndexer);
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct, ComparisonDepth depth)
		{
			return IsTheSame(comparisonStruct as InterfaceIndexer, depth);
		}

		private bool IsTheSame(InterfaceIndexer comparisonFunction)
		{
			return IsTheSame(comparisonFunction, ComparisonDepth.Signature);
		}

		private bool IsTheSame(InterfaceIndexer comparisonIndexer, ComparisonDepth depth)
		{
			if (comparisonIndexer == null)
				return false;

			if (Name == comparisonIndexer.Name)
			{
				if (ParametersAreTheSame(comparisonIndexer, depth) == false) return false;
				// Function names are the same, so now compare the class names

				if (ParentObject.IsTheSame(comparisonIndexer.ParentObject))
				{
					if (depth == ComparisonDepth.Signature)
					{
						return true;
					}
					if (dataType != comparisonIndexer.dataType) return false;

					if (hasNewKeyword != comparisonIndexer.hasNewKeyword)
					{
						ComparisonDifference += GetType().Name + ".HasNewKeyword";
						return false;
					}

					if (depth == ComparisonDepth.Outer)
					{
						return true;
					}

					if (!base.IsTheSame(comparisonIndexer, depth))
					{
						return false;
					}
					if (!GetAccessor.IsTheSame(comparisonIndexer.GetAccessor))
					{
						ComparisonDifference += GetType().Name + ".GetAccessor";
						return false;
					}
					if (!SetAccessor.IsTheSame(comparisonIndexer.SetAccessor))
					{
						ComparisonDifference += GetType().Name + ".SetAccessor";
						return false;
					}
					return true;
				}
			}
			return false;
		}

		protected override bool CustomMergeStepParameterInternal(BaseConstruct user, BaseConstruct newgen, BaseConstruct prevgen)
		{
			InterfaceIndexer userBC = (InterfaceIndexer)user, newgenBC = (InterfaceIndexer)newgen, prevgenBC = (InterfaceIndexer)prevgen;

			// DataType
			if (!Utility.MergeDataType(ref dataType, userBC.dataType, newgenBC.dataType, prevgenBC.dataType))
				return false;
			// HasNewKeyword
			if (!Utility.MergeSingleItem(ref hasNewKeyword, userBC.hasNewKeyword, newgenBC.hasNewKeyword, prevgenBC.hasNewKeyword))
				return false;

			return true;
		}
	}
}