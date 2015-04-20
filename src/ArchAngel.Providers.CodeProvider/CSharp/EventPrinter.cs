using System;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
	/// <summary>
	/// Summary description for Event.
	/// </summary>
	[Serializable]
	public class EventPrinter : BasePrinter<Event>
	{
		private Event obj;

		public EventPrinter(Event obj)
			: base(obj)
		{
			this.obj = obj;
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

			sb.AppendFormat("event {0} {1}", obj.DataType, obj.Name);

			if (string.IsNullOrEmpty(obj.AddAccessorText) && string.IsNullOrEmpty(obj.RemoveAccessorText))
			{
				sb.AppendLine(string.Format("{0};", obj.InitialValue));

				if (!string.IsNullOrEmpty(obj.Comments.TrailingComment))
				{
					sb.Append(" " + obj.Comments.TrailingComment);
				}
			}
			else if (!string.IsNullOrEmpty(obj.AddAccessorText) || !string.IsNullOrEmpty(obj.RemoveAccessorText))
			{
				if (!string.IsNullOrEmpty(obj.Comments.TrailingComment))
				{
					sb.Append(" " + obj.Comments.TrailingComment);
				}
				sb.Append(Environment.NewLine + obj.Controller.Indent + "{");
				obj.Controller.IndentLevel++;
				sb.Append(Environment.NewLine);

				if (!string.IsNullOrEmpty(obj.AddAccessorText))
				{
					sb.Append(obj.Controller.Indent + "add");
					obj.Controller.IndentLevel--;
					sb.Append(Utility.ResetIndents(obj.AddAccessorText, obj.Controller.Indent));
					obj.Controller.IndentLevel++;
				}
				if (!string.IsNullOrEmpty(obj.RemoveAccessorText))
				{
					sb.Append(obj.Controller.Indent + "remove");
					obj.Controller.IndentLevel--;
					sb.Append(Utility.ResetIndents(obj.RemoveAccessorText, obj.Controller.Indent));
					obj.Controller.IndentLevel++;
				}
				obj.Controller.IndentLevel--;
				sb.AppendLine(obj.Controller.Indent + "}");
				//throw new NotImplementedException("Accessor methods have not been coded for yet.");
			}
			//sb.AppendLine(Controller.Indent + "}");
			return sb.ToString();
		}
	}
}
