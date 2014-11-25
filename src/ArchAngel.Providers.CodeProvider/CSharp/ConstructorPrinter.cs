using System;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
	/// <summary>
	/// Summary description for Function.
	/// </summary>
	[Serializable]
	public class ConstructorPrinter : ParameterBasePrinter
	{
		private Constructor obj;

		public ConstructorPrinter(Constructor obj)
			: base(obj)
		{
			this.obj = obj;
		}

		public override string Print()
		{
			return ToString(true);
		}

		public virtual string ToString(bool includeBody)
		{
			StringBuilder sb = new StringBuilder(10000);

			sb.Append(obj.Controller.Indent);

			#region Modifiers
			for (int i = 0; i < obj.Modifiers.Count; i++)
			{
				sb.Append(obj.Modifiers[i] + " ");
			}
			#endregion

			sb.Append(obj.Name + "(");

			for (int i = 0; i < obj.Parameters.Count; i++)
			{
				if (i > 0) { sb.Append(", "); }

				sb.Append(obj.Parameters[i].ToString());
			}
			sb.Append(")");

			if (!string.IsNullOrEmpty(obj.InitializerType))
			{
				sb.Append(" : " + obj.InitializerType);
				sb.Append("(");

				for (int i = 0; i < obj.InitializerArguments.Count; i++)
				{
					if (i > 0) { sb.Append(", "); }

					sb.Append(obj.InitializerArguments[i]);
				}
				sb.Append(")");
			}
			if (!string.IsNullOrEmpty(obj.Comments.TrailingComment))
			{
				sb.Append(" " + obj.Comments.TrailingComment);
			}
			sb.AppendLine("");
			//sb.Append(Controller.Indent);
			//sb.AppendLine(this.Body);
			string endLineBreak = "";

			//if (this.Body.Length > 0 && this.Body[this.Body.Length - 1] != '\r' && this.Body[this.Body.Length - 1] != '\n')
			//{
			//    endLineBreak = "\r";
			//}

			if (includeBody)
			{
				sb.Append(Utility.ResetIndents(Helper.StandardizeLineBreaks(obj.BodyText + endLineBreak, Helper.LineBreaks.Unix), obj.Controller.Indent));
			}
			//return sb.ToString();

			if (CodeRoot.IncludeExtraLines)
			{
				string text = sb.ToString();
				int numLines = Helper.GetNumberOfLines(text);

				if (obj.NumberOfLines > numLines)
				{
					return text + new string('\n', obj.NumberOfLines - numLines);
				}
				return text;
			}
			return sb.ToString();
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

		/// <summary>
		/// Returns the body of the BaseConstruct. If it doesn't have a body (for instance, it is a field), then the default behaviour is to return string.Empty;
		/// </summary>
		/// <returns></returns>
		public override string GetInnerText()
		{
			return obj.BodyText;
		}
	}
}
