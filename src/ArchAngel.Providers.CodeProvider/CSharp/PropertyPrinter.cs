using System;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
	/// <summary>
	/// Summary description for Property.
	/// </summary>
	[Serializable]
	public class PropertyPrinter : BasePrinter<Property>
	{
		private Property obj;

		public PropertyPrinter(Property obj)
			: base(obj)
		{
			this.obj = obj;
		}

		public override string Print()
		{
			StringBuilder sb = new StringBuilder(500);
			string accessorBody;

			sb.Append(obj.Controller.Indent);

			#region Modifiers

			for (int i = 0; i < obj.Modifiers.Count; i++)
			{
				sb.Append(obj.Modifiers[i] + " ");
			}

			#endregion

			sb.AppendFormat("{0} {1}", obj.DataType, obj.Name);

			if (!string.IsNullOrEmpty(obj.Comments.TrailingComment))
			{
				sb.Append(" " + obj.Comments.TrailingComment);
			}
			bool bothAccessorsAreNull = (obj.GetAccessor == null || obj.GetAccessor.BodyText == ";") && (obj.SetAccessor == null || obj.SetAccessor.BodyText == ";");

			if (bothAccessorsAreNull)
			{
				sb.Append(" {");
			}
			else
			{
				sb.AppendLine("");
				sb.AppendLine(obj.Controller.Indent + "{");
				obj.Controller.IndentLevel++;
			}

			#region Write Accessors
			if (!bothAccessorsAreNull && obj.GetAccessor != null)
			{
				accessorBody = obj.GetAccessor.ToString();
				sb.Append(Utility.ResetIndents(Helper.StandardizeLineBreaks(accessorBody, Helper.LineBreaks.Unix), obj.Controller.Indent));
			}
			else
			{
				if (bothAccessorsAreNull) // Print it on a single line
					sb.Append(" get;");
				else
					sb.Append(obj.Controller.Indent).Append("get;");
			}
			if (!bothAccessorsAreNull && obj.SetAccessor != null)
			{
				//sb.Append(SetAccessor.ToString());
				accessorBody = obj.SetAccessor.ToString();
				sb.Append(Utility.ResetIndents(Helper.StandardizeLineBreaks(accessorBody, Helper.LineBreaks.Unix), obj.Controller.Indent));
			}
			else
			{
				sb.Append(" set;");
			}
			#endregion

			obj.Controller.IndentLevel--;
			if (bothAccessorsAreNull)
				sb.AppendLine(" }");
			else
				sb.AppendLine(obj.Controller.Indent + "}");

			return sb.ToString();
		}



	}
}
