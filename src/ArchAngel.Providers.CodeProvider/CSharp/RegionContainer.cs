using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
	public abstract class RegionContainer : BaseConstruct
	{
		protected List<Region> regions = new List<Region>();

		protected RegionContainer(Controller controller) : base(controller)
		{
		}

		public List<Region> Regions
		{
			get { return regions; }
			set { regions = value; }
		}

		protected override void AddChildInternal(BaseConstruct childBC)
		{
			if(childBC is Region)
			{
				regions.Add(childBC as Region);
			}
			else
				throw new ArgumentException("Cannot add child of type " + childBC.GetType() + "to a " + GetType());
		}

		protected ReadOnlyCollection<T> RecursiveGetAllOfType<T>(IEnumerable<T> directObjects) where T : class
		{
			List<T> funcs = new List<T>();
			funcs.AddRange(directObjects);
			SearchAndAddFromRegions(funcs);
			return funcs.AsReadOnly();
		}

		private void SearchAndAddFromRegions<T>(List<T> list) where T : class
		{
			foreach(Region region in regions)
			{
				list.AddRange(region.GetConstructsOfType<T>());
			}
		}
	}
}