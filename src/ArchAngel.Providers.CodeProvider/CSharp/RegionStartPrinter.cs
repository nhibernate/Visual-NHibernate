using System;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
	[Serializable]
	public class RegionStartPrinter : BasePrinter<RegionStart>
	{
		private RegionStart obj;

		public RegionStartPrinter(RegionStart obj)
			: base(obj)
		{
			this.obj = obj;
		}

		public override string Print()
		{
			return obj.Controller.Indent + "#region " + obj.Name;
		}
	}
}
