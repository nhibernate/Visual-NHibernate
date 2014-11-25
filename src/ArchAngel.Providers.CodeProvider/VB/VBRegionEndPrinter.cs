using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
	class VBRegionEndPrinter : IPrinter
	{
		private RegionEnd obj;

		public VBRegionEndPrinter(RegionEnd obj)
		{
			this.obj = obj;
		}

		public string FullyQualifiedName
		{
			get { throw new System.NotImplementedException(); }
		}

		public string Print()
		{
			throw new System.NotImplementedException();
		}

		public string GetOuterText()
		{
			throw new System.NotImplementedException();
		}

		public string GetInnerText()
		{
			throw new System.NotImplementedException();
		}
	}
}
