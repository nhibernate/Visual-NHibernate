using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
	class VBFunctionPrinter : IPrinter
	{
		private Function obj;

		public VBFunctionPrinter(Function obj)
		{
			this.obj = obj;
		}

		public string DisplayNameExtended
		{
			get { throw new System.NotImplementedException(); }
		}

		public string GetInnerText()
		{
			throw new System.NotImplementedException();
		}

		public string GetOuterText()
		{
			throw new System.NotImplementedException();
		}

		public string ToString(bool includeBody)
		{
			throw new System.NotImplementedException();
		}

		public string FullyQualifiedName
		{
			get { throw new System.NotImplementedException(); }
		}

		public string Print()
		{
			throw new System.NotImplementedException();
		}
	}
}
