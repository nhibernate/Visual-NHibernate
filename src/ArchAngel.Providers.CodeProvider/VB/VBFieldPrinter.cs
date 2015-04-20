using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
	class VBFieldPrinter : IPrinter
	{
		private Field obj;

		public VBFieldPrinter(Field obj)
		{
			this.obj = obj;
		}

		public string FullyQualifiedDisplayNameExtended
		{
			get { throw new System.NotImplementedException(); }
		}

		public string DisplayNameExtended
		{
			get { throw new System.NotImplementedException(); }
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
