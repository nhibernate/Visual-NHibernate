using System;
using System.Text;
using Delegate = ArchAngel.Providers.CodeProvider.DotNet.Delegate;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
	/// <summary>
	/// Summary description for Delegate.
	/// </summary>
	[Serializable]
	public class DelegatePrinter : ParameterBasePrinter
	{
		private Delegate obj;

		public DelegatePrinter(Delegate obj)
			: base(obj)
		{
			this.obj = obj;
		}

		public override string Print()
		{
			StringBuilder sb = new StringBuilder(100);

			sb.Append(obj.Controller.Indent);

			#region Modifiers
			for (int i = 0; i < obj.Modifiers.Count; i++)
			{
				sb.Append(obj.Modifiers[i] + " ");
			}
			#endregion

			sb.AppendFormat("delegate {0} {1}", obj.ReturnType, obj.Name);

			if (!string.IsNullOrEmpty(obj.GenericType))
			{
				sb.Append("<" + obj.GenericType + ">");
			}
			sb.Append("(");

			#region Parameters
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
