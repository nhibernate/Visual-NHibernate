using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
	class VBNamespacePrinter : IPrinter
	{
		private Namespace obj;

		public VBNamespacePrinter(Namespace obj)
		{
			this.obj = obj;
		}

		public string GetInnerText()
		{
			throw new System.NotImplementedException();
		}

		public string GetOuterText()
		{
			throw new System.NotImplementedException();
		}

		public void GetBodyText(StringBuilder sb)
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
