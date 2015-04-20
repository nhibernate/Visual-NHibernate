using System;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
	/// <summary>
	/// Summary description for Enumeration.
	/// </summary>
	[Serializable]
	public class EnumerationPrinter : BasePrinter<Enumeration>
	{
		private Enumeration obj;

		public EnumerationPrinter(Enumeration obj)
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
			StringBuilder sb = new StringBuilder(100);

			sb.Append(obj.Controller.Indent);

			#region Modifiers
			for (int i = 0; i < obj.Modifiers.Count; i++)
			{
				sb.Append(obj.Modifiers[i] + " ");
			}
			#endregion

			sb.AppendFormat("enum {0}", obj.Name);

			if (string.IsNullOrEmpty(obj.EnumBase) == false)
			{
				sb.Append(" : " + obj.EnumBase);
			}
			if (!string.IsNullOrEmpty(obj.Comments.TrailingComment))
			{
				sb.Append(" " + obj.Comments.TrailingComment);
			}
			sb.AppendLine("");
			sb.AppendLine(obj.Controller.Indent + "{");
			obj.Controller.IndentLevel++;

			#region Write Members
			for (int i = 0; i < obj.Members.Count; i++)
			{
				//if (!this.Members[i].Omit)
				//{

				sb.AppendLine(obj.Members[i] + (i < obj.Members.Count - 1 ? "," : ""));

				//}
			}
			#endregion

			obj.Controller.IndentLevel--;
			sb.AppendLine(obj.Controller.Indent + "}");
			return sb.ToString();
		}
		public class EnumMemberPrinter : IPrinter
		{
			private Enumeration.EnumMember obj;

			public EnumMemberPrinter(Enumeration.EnumMember obj)
			{
				this.obj = obj;
			}

			public string FullyQualifiedName
			{
				get { throw new System.NotImplementedException(); }
			}

			public string Print()
			{
				StringBuilder sb = new StringBuilder(10000);

				sb.Append(obj.Controller.Indent);
				sb.AppendFormat("{0}", obj.Name);

				if (obj.Value.Length > 0)
				{
					sb.AppendFormat(" = {0}", obj.Value);
				}
				sb.Append("");

				if (!string.IsNullOrEmpty(obj.Comments.TrailingComment))
				{
					sb.Append(" " + obj.Comments.TrailingComment);
				}
				return sb.ToString();
			}

			public string GetOuterText()
			{
				return String.Empty;
			}

			public string GetInnerText()
			{
				return obj.ToString();
			}
		}
	}

}
