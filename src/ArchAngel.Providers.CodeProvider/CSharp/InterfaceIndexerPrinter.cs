using System;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
	/// <summary>
	/// Summary description for InterfaceIndexer.
	/// </summary>
	[Serializable]
	public class InterfaceIndexerPrinter : ParameterBasePrinter
	{
		private InterfaceIndexer obj;

		public InterfaceIndexerPrinter(InterfaceIndexer obj)
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
			sb.AppendFormat("{0} this [", obj.DataType);

			for (int i = 0; i < obj.Parameters.Count; i++)
			{
				if (i > 0) { sb.Append(", "); }
				sb.Append(obj.Parameters[i].ToString());
			}
			sb.Append("] {");

			if (obj.GetAccessor != null) { sb.Append(" get;"); }
			if (obj.SetAccessor != null) { sb.Append(" set;"); }

			sb.AppendLine(" }");

			if (!string.IsNullOrEmpty(obj.Comments.TrailingComment))
			{
				sb.Append(" " + obj.Comments.TrailingComment);
			}
			return sb.ToString();
		}

		public override string FullyQualifiedName
		{
			get
			{
				if (obj.ParentObject != null)
					return obj.ParentObject.FullyQualifiedName + "." + DisplayName;

				return DisplayName;
			}
		}
	}
}
