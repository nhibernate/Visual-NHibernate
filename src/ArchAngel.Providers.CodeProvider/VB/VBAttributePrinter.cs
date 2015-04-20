using System;
using ArchAngel.Providers.CodeProvider.DotNet;
using Attribute = ArchAngel.Providers.CodeProvider.DotNet.Attribute;

namespace ArchAngel.Providers.CodeProvider.VB
{
	class VBAttributePrinter : IPrinter
	{
		private Attribute obj;

		public VBAttributePrinter(Attribute obj)
		{
			this.obj = obj;
		}

		public string FullyQualifiedName
		{
			get
			{
				throw new NotImplementedException("FullyQualifiedName");
			}
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


		public string FullyQualifiedIdentifer
		{
			get
			{
				throw new NotImplementedException("FullyQualifiedIdentifier");
			}
		}
	}
}
