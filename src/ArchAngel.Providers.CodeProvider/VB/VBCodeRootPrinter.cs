using ArchAngel.Providers.CodeProvider.CSharp;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
	class VBCodeRootPrinter : IPrinter
	{
		private CodeRoot obj;

		public VBCodeRootPrinter(CodeRoot obj)
		{
			this.obj = obj;
		}

		public override string ToString()
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
