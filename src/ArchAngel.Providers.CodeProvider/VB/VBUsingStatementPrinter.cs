using System;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
	class VBUsingStatementPrinter : IPrinter
	{
		private UsingStatement obj;

		public VBUsingStatementPrinter(UsingStatement obj)
		{
			this.obj = obj;
		}

		public string DisplayNameExtended
		{
			get { throw new NotImplementedException("DisplyNameExtended"); }
		}

		public string DisplayName
		{
			get { throw new NotImplementedException("DisplyName"); }
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
