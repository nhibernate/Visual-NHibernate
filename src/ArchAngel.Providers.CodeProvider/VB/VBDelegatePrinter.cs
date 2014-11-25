using ArchAngel.Providers.CodeProvider.DotNet;
using Delegate = ArchAngel.Providers.CodeProvider.DotNet.Delegate;

namespace ArchAngel.Providers.CodeProvider.VB
{
	class VBDelegatePrinter : IPrinter
	{
		private Delegate obj;

		public VBDelegatePrinter(Delegate obj)
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
