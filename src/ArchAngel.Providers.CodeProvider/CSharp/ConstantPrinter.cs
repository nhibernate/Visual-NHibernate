using System;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
	[Serializable]
	public class ConstantPrinter : BasePrinter<Constant>
	{
		private Constant obj;

		public ConstantPrinter(Constant obj)
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
			StringBuilder sb = new StringBuilder(10000);

			sb.Append(obj.Controller.Indent);

			#region Modifiers
			for (int i = 0; i < obj.Modifiers.Count; i++)
			{
				sb.Append(obj.Modifiers[i] + " ");
			}
			#endregion

			sb.AppendFormat("const {0} {1}", obj.DataType, obj.Name);

			if (obj.Expression.Length > 0)
			{
				sb.AppendFormat(" = {0};", obj.Expression);
			}
			if (!string.IsNullOrEmpty(obj.Comments.TrailingComment))
			{
				sb.Append(" " + obj.Comments.TrailingComment);
			}
			return sb.ToString();
		}

	}
}
