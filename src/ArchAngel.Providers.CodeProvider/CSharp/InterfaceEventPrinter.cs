using System;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
	/// <summary>
	/// Summary description for InterfaceEvent.
	/// </summary>
	[Serializable]
	public class InterfaceEventPrinter : BasePrinter<InterfaceEvent>
	{
		private InterfaceEvent obj;

		public InterfaceEventPrinter(InterfaceEvent obj)
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
			sb.AppendFormat("event {0} {1};", obj.DataType, obj.Name);

			if (!string.IsNullOrEmpty(obj.Comments.TrailingComment))
			{
				sb.Append(" " + obj.Comments.TrailingComment);
			}
			sb.AppendLine();
			return sb.ToString();
		}


	}
}
