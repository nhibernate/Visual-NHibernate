using System;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
	[Serializable]
	public class BaseConstructPrinter : BasePrinter<BaseConstruct>
	{
		protected readonly BaseConstruct obj;

		public BaseConstructPrinter(BaseConstruct obj)
			: base(obj)
		{
			this.obj = obj;
		}

		public string FullyQualifiedIdentifer
		{
			get { throw new System.NotImplementedException(); }
		}

		public override string Print()
		{
			throw new System.NotImplementedException();
		}
	}
}
