using System;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
	/// <summary>
	/// Summary description for InterfaceMethod.
	/// </summary>
	[Serializable]
	public class InterfaceMethodPrinter : ParameterBasePrinter
	{
		private InterfaceMethod obj;

		public InterfaceMethodPrinter(InterfaceMethod obj)
			: base(obj)
		{
			this.obj = obj;
		}

		public override string Print()
		{
			StringBuilder sb = new StringBuilder(500);

			sb.Append(obj.Controller.Indent);
			if (obj.HasNewKeyword)
			{
				sb.Append("new ");
			}

			sb.AppendFormat("{0} {1} (", obj.ReturnType, obj.Name);

			#region Write Parameters

			for (int i = 0; i < obj.Parameters.Count; i++)
			{
				if (i > 0) { sb.Append(", "); }
				sb.Append(obj.Parameters[i].ToString());
			}
			#endregion

			sb.Append(");");

			if (!string.IsNullOrEmpty(obj.Comments.TrailingComment))
			{
				sb.Append(" " + obj.Comments.TrailingComment);
			}
			return sb.ToString();
		}
	}
}
