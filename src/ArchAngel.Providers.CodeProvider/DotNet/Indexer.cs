using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArchAngel.Providers.CodeProvider.CSharp;

namespace ArchAngel.Providers.CodeProvider.DotNet
{
	public class Indexer : ParameterBase
	{
		private DataType dataType;
		private PropertyAccessor getAccessor;
		private PropertyAccessor setAccessor;

		public Indexer(Controller controller)
			: base(controller)
		{
		}

		public Indexer(Controller controller, DataType type)
			: base(controller)
		{
			dataType = type;
		}

		public Indexer(Controller controller, BaseConstruct parentObject, DataType dataType, IEnumerable<Parameter> parameters, PropertyAccessor getAccessor, PropertyAccessor setAccessor, CodeLanguage language, int nodeIndex)
			: base(controller)
		{
			ParentObject = parentObject;
			DataType = dataType;
			Parameters.Clear();
			if (parameters != null)
				Parameters.AddRange(parameters);
			GetAccessor = getAccessor;
			SetAccessor = setAccessor;
			Language = language;
			Index = nodeIndex;
			Name = "this";
		}

		private Indexer(Indexer iiToCopyFrom)
			: base(null)
		{
			iiToCopyFrom.CloneInto(this);
			DataType = iiToCopyFrom.DataType.Clone();
			foreach (Parameter p in iiToCopyFrom.Parameters)
			{
				var clone = (Parameter)p.Clone();
				Parameters.Add(clone);
				clone.Parent = this;
			}
		}

		/// <summary>
		/// Returns a shallow copy of the construct. Does not copy children.
		/// </summary>
		/// <returns>A shallow copy of the construct. Does not copy children.</returns>
		public override IBaseConstruct Clone()
		{
			return new Indexer(this);
		}
		public DataType DataType
		{
			get { return dataType; }
			set { dataType = value; }
		}

		public PropertyAccessor GetAccessor
		{
			get { return getAccessor; }
			set { getAccessor = value; }
		}

		public PropertyAccessor SetAccessor
		{
			get { return setAccessor; }
			set { setAccessor = value; }
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

			if (childBC is PropertyAccessor)
			{
				PropertyAccessor newAccessor = (PropertyAccessor)childBC;
				if (newAccessor.AccessorType == PropertyAccessor.AccessorTypes.Get)
					GetAccessor = newAccessor;
				else
					SetAccessor = newAccessor;
			}
			else
				throw new InvalidOperationException("Cannot add child of type " + childBC.GetType());
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct)
		{
			return IsTheSame(comparisonStruct as Indexer);
		}

		public override bool IsTheSame(BaseConstruct comparisonStruct, ComparisonDepth depth)
		{
			return IsTheSame(comparisonStruct as Indexer, depth);
		}

		private bool IsTheSame(Indexer comparisonFunction)
		{
			return IsTheSame(comparisonFunction, ComparisonDepth.Signature);
		}

		private bool IsTheSame(Indexer comparisonIndexer, ComparisonDepth depth)
		{
			if (comparisonIndexer == null)
				return false;

			if (Name == comparisonIndexer.Name &&
				DataType == comparisonIndexer.DataType &&
				Parameters.Count == comparisonIndexer.Parameters.Count)
			{
				for (int i = 0; i < Parameters.Count; i++)
				{
					if (Parameters[i].Name != comparisonIndexer.Parameters[i].Name ||
						Parameters[i].DataType != comparisonIndexer.Parameters[i].DataType)
					{
						return false;
					}
				}
				// Function names are the same, so now compare the class names
				// TODO: Parent object comparison
				//if (ParentObject.IsTheSame(comparisonIndexer.ParentObject))
				{
					if (depth == ComparisonDepth.Signature)
					{
						return true;
					}

					if (!base.IsTheSame(comparisonIndexer, depth))
					{
						return false;
					}

					if (depth == ComparisonDepth.Outer)
					{
						return true;
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
			// Data Type
			return Utility.MergeDataType(ref dataType, ((Indexer)user).DataType, ((Indexer)newgen).DataType, ((Indexer)prevgen).DataType);
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
				if (ParentObject != null)
					return ParentObject.FullyQualifiedIdentifer + BaseConstructConstants.FullyQualifiedIdentifierSeparator + DisplayName;

				return DisplayName;
			}
		}
	}
}
