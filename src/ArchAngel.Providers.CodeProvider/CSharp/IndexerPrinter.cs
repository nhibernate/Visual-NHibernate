using System;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CSharp
{

	/// <summary>
	/// Summary description for Indexer.
	/// </summary>
	[Serializable]
	public class IndexerPrinter : ParameterBasePrinter
	{
		private Indexer obj;

		public IndexerPrinter(Indexer obj)
			: base(obj)
		{
			this.obj = obj;
		}

		public override string Print()
		{
			StringBuilder sb = new StringBuilder(500);

			sb.Append(obj.Controller.Indent);

			sb.AppendFormat(" {0} this [", obj.DataType);

			for (int i = 0; i < obj.Parameters.Count; i++)
			{
				if (i > 0) { sb.Append(", "); }
				sb.Append(obj.Parameters[i].ToString());
			}
			sb.Append("]");

			#region Write Accessors

			sb.AppendLine("{");

			string accessorBody;
			if (obj.GetAccessor != null)
			{
				//sb.Append(GetAccessor.ToString());
				accessorBody = obj.GetAccessor.ToString();
				sb.Append(Utility.ResetIndents(Helper.StandardizeLineBreaks(accessorBody, Helper.LineBreaks.Unix), obj.Controller.Indent));
			}
			if (obj.SetAccessor != null)
			{
				//sb.Append(SetAccessor.ToString());
				accessorBody = obj.SetAccessor.ToString();
				sb.Append(Utility.ResetIndents(Helper.StandardizeLineBreaks(accessorBody, Helper.LineBreaks.Unix), obj.Controller.Indent));
			}
			sb.AppendLine("}");
			#endregion

			if (!string.IsNullOrEmpty(obj.Comments.TrailingComment))
			{
				sb.Append(" " + obj.Comments.TrailingComment);
			}
			return sb.ToString();
		}

		public override string FullyQualifiedIdentifer
		{
			get
			{
				if (obj.ParentObject != null)
					return obj.ParentObject.FullyQualifiedIdentifer + BaseConstructConstants.FullyQualifiedIdentifierSeparator + DisplayName;

				return DisplayName;
			}
		}
	}
}
