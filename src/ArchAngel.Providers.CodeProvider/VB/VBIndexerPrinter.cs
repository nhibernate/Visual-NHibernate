using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
	class VBIndexerPrinter : IPrinter
	{
		private Indexer obj;

		public VBIndexerPrinter(Indexer obj)
		{
			this.obj = obj;
		}

		public string FullyQualifiedDisplayName
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

		public string DisplayName
		{
			get { throw new System.NotImplementedException(); }
		}
	}
}
