using System;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
	[Serializable]
	public class RegionEndPrinter : BasePrinter<RegionEnd>
	{
		private RegionEnd obj;

		public RegionEndPrinter(RegionEnd obj)
			: base(obj)
		{
			this.obj = obj;
		}

		public override string Print()
		{
			return Environment.NewLine + obj.Controller.Indent + "#endregion";
		}
	}
}
