using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
	class VBInterfaceAccessorPrinter : IPrinter
	{
		private InterfaceAccessor obj;

		public VBInterfaceAccessorPrinter(InterfaceAccessor obj)
		{
			this.obj = obj;
		}

		public string DisplayName
		{
			get { throw new System.NotImplementedException(); }
		}

		public string FullyQualifiedIdentifer
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
