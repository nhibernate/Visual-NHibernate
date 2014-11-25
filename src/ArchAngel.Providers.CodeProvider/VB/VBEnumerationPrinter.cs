using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
	class VBEnumerationPrinter : IPrinter
	{
		private Enumeration obj;

		public VBEnumerationPrinter(Enumeration obj)
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

		public class VBEnumMember : IPrinter
		{
			private Enumeration.EnumMember obj;

			public VBEnumMember(Enumeration.EnumMember obj)
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


}
