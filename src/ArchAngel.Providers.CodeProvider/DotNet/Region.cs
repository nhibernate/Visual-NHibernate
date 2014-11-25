using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ArchAngel.Providers.CodeProvider.DotNet
{
	public class Region : BaseConstruct
	{
		public readonly List<BaseConstruct> children = new List<BaseConstruct>();
		private int endOffset;

		public Region(Controller controller) : base(controller)
		{
		}

		public Region(Controller controller, string name)
			: this(controller)
		{
			Name = name;
		}

		public Region(Controller controller, string name, int index)
			: this(controller, name)
		{
			Index = index;
		}

		public override IBaseConstruct Clone()
		{
			Region clone = new Region(null);
			CloneInto(clone);
			return clone;
		}

		public int EndOffset
		{
			get { return endOffset; }
			set { endOffset = value; }
		}

		protected override bool CustomMergeStepInternal(BaseConstruct user, BaseConstruct newgen, BaseConstruct prevgen)
		{
			return true;
		}

		protected override ReadOnlyCollection<IBaseConstruct> WalkChildrenInternal()
		{
			List<IBaseConstruct> list = new List<IBaseConstruct>();
			foreach(BaseConstruct bc in children)
			{
				list.Add(bc);	
			}
			return list.AsReadOnly();
		}

		public BaseConstruct[] SortedConstructs
		{
			get
			{
				return children.ToArray();
			}
		}

		protected override void AddChildInternal(BaseConstruct childBC)
		{
			children.Add(childBC);
		}

		public override bool IsTheSame(BaseConstruct comparisonObject, ComparisonDepth depth)
		{
			return IsTheSame(comparisonObject as Region, depth);
		}

		public override bool IsTheSame(BaseConstruct comparisonObject)
		{
			return IsTheSame(comparisonObject as Region);
		}


		public bool IsTheSame(Region comparisonObject)
		{
			return IsTheSame(comparisonObject, ComparisonDepth.Signature);
		}

		public bool IsTheSame(Region comparisonRegion, ComparisonDepth depth)
		{
			if (comparisonRegion == null)
				return false;

			if (Name == comparisonRegion.Name)
			{
				if (depth == ComparisonDepth.Signature)
				{
					return true;
				}

				return base.IsTheSame(comparisonRegion, depth);
			}

			return false;
		}

		public IEnumerable<T> GetConstructsOfType<T>() where T : class
		{
			List<T> list = new List<T>();

			foreach(BaseConstruct bc in children)
			{
				if(bc is T)
					list.Add(bc as T);
				else if(bc is Region)
				{
					Region innerRegion = (Region)bc;
					list.AddRange(innerRegion.GetConstructsOfType<T>());
				}
			}
			return list;
		}
	}
}