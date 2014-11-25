using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
	class VBOperatorPrinter : IPrinter
	{
		private Operator obj;

		public VBOperatorPrinter(Operator obj)
		{
			this.obj = obj;
		}

		public string GetOuterText()
		{
			throw new System.NotImplementedException();
		}

		public string GetInnerText()
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

		public string ToString(bool includeBody)
		{
			throw new System.NotImplementedException();
		}
	}
}
