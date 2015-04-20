using System;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
	[Serializable]
	public class FunctionPrinter : ParameterBasePrinter
	{
		private Function obj;

		public FunctionPrinter(Function obj)
			: base(obj)
		{
			this.obj = obj;
		}


		public override string DisplayNameExtended
		{
			get
			{
				string parameterNames = "";

				for (int i = 0; i < obj.Parameters.Count; i++)
				{
					if (i > 0)
					{
						parameterNames += ", ";
					}
					parameterNames += string.Format("{0} {1}", obj.Parameters[i].DataType, obj.Parameters[i].Name);
				}
				return string.Format("{0} ({1})", obj.Name, parameterNames);
			}
		}

		/// <summary>
		/// Returns the body of the BaseConstruct. If it doesn't have a body (for instance, it is a field), then the default behaviour is to return string.Empty;
		/// </summary>
		/// <returns></returns>
		public override string GetInnerText()
		{
			return obj.FormattedBody ?? obj.BodyText;
		}

		/// <summary>
		/// Gets the outer text of the node. 
		/// </summary>
		/// <remarks>This is the method signature for a C# Function, the element text (<![CDATA[<element></element>]]>) for an XML Element, etc.</remarks>
		/// <returns>The outer text of the node.</returns>
		public override string GetOuterText()
		{
			return ToString(false);
		}

		public override string Print()
		{
			return ToString(true);
		}

		public string ToString(bool includeBody)
		{
			StringBuilder sb = new StringBuilder(10000);

			sb.Append(obj.Controller.Indent);

			#region Modifiers
			for (int i = 0; i < obj.Modifiers.Count; i++)
			{
				sb.Append(obj.Modifiers[i] + " ");
			}
			#endregion

			sb.AppendFormat("{0} {1}(", obj.ReturnType, obj.Name);

			#region Parameters

			if (obj.DisplayOptions.ListVertically) { obj.Controller.IndentLevel++; }

			for (int i = 0; i < obj.Parameters.Count; i++)
			{
				if (obj.DisplayOptions.ListVertically)
				{
					sb.AppendLine();
					sb.Append(obj.Controller.Indent);
				}
				sb.Append(obj.Parameters[i].ToString());

				if (i < obj.Parameters.Count - 1) { sb.Append(", "); }
			}
			if (obj.DisplayOptions.ListVertically) { obj.Controller.IndentLevel--; }
			#endregion

			sb.Append(")");

			if (!string.IsNullOrEmpty(obj.Comments.TrailingComment))
			{
				sb.Append(" " + obj.Comments.TrailingComment);
			}
			//string endLineBreak = "";

			//if (FormattedBody.Length > 0 && FormattedBody[FormattedBody.Length - 1] != '\r' && FormattedBody[FormattedBody.Length - 1] != '\n')
			//{
			//    endLineBreak = "\r";
			//}

			if (includeBody)
			{
				sb.Append("\n");
				sb.Append(Utility.ResetIndents(Helper.StandardizeLineBreaks(obj.FormattedBody, Helper.LineBreaks.Unix), obj.Controller.Indent));
			}

			if (CodeRoot.IncludeExtraLines == false)
			{
				return sb.ToString();
			}

			string text = sb.ToString();
			int numLines = Helper.GetNumberOfLines(text);

			if (obj.NumberOfLines > numLines)
			{
				//return text + new string('\n', this.NumberOfLines - numLines);

				for (int i = 0; i < obj.NumberOfLines - numLines - 1; i++)
				{
					sb.Append("\n***");
				}
				return sb.ToString();
			}

			return text;
		}

	}
}
