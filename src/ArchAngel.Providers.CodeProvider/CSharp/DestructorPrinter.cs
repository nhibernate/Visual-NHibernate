using System;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
	public class DestructorPrinter : BasePrinter<Destructor>
	{
		private Destructor obj;

		public DestructorPrinter(Destructor obj)
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
			StringBuilder sb = new StringBuilder();
			sb.Append(obj.Controller.Indent);

			if (obj.IsExtern) sb.Append("extern ");

			sb.Append("~").Append(obj.Name).Append("()");

			if (obj.IsExtern == false) sb.Append(Environment.NewLine).Append(obj.Controller.Indent);

			sb.Append(obj.BodyText);

			return sb.ToString();
		}
	}
}
