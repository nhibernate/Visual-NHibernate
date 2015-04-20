using System;
using System.Text;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
	[Serializable]
	public class EmptyPlaceholderPrinter : BasePrinter<EmptyPlaceholder>
	{
		private EmptyPlaceholder obj;

		public EmptyPlaceholderPrinter(EmptyPlaceholder obj)
			: base(obj)
		{
			this.obj = obj;
		}


		public override string FullyQualifiedName
		{
			get { throw new System.NotImplementedException(); }
		}

		public override string Print()
		{
			if (CodeRoot.IncludeExtraLines)
			{
				StringBuilder sb = new StringBuilder();

				for (int i = 0; i < obj.NumberOfLines; i++)
				{
					sb.AppendLine();
				}
				return sb.ToString();
			}

			return "";
		}


	}
}
