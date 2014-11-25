using System;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
	[Serializable]
	public class UsingStatementPrinter : BasePrinter<UsingStatement>
	{
		private UsingStatement obj;

		public UsingStatementPrinter(UsingStatement obj)
			: base(obj)
		{
			this.obj = obj;
		}

		public override string Print()
		{
			StringBuilder sb = new StringBuilder(100);

			if (!string.IsNullOrEmpty(obj.Alias))
			{
				sb.AppendLine(string.Format("{1}using {2} = {0};", obj.Value, obj.Controller.Indent, obj.Alias));
			}
			else
			{
				sb.Append(string.Format("{1}using {0};", obj.Value, obj.Controller.Indent));
			}
			if (!string.IsNullOrEmpty(obj.Comments.TrailingComment))
			{
				sb.Append(" " + obj.Comments.TrailingComment);
			}
			return sb.ToString();
		}

		public string DisplayNameExtended
		{
			get { return "using " + obj.Value; }
		}

		public string DisplayName
		{
			get { return "Reference"; }
		}
	}
}
