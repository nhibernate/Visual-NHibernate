using System;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
	/// <summary>
	/// Summary description for Operator.
	/// </summary>
	[Serializable]
	public class OperatorPrinter : ParameterBasePrinter
	{
		private readonly Operator obj;

		public OperatorPrinter(Operator obj)
			: base(obj)
		{
			this.obj = obj;
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

		new public string FullyQualifiedName
		{
			get { throw new System.NotImplementedException(); }
		}

		public override string Print()
		{
			return ToString(true);
		}

		public string ToString(bool includeBody)
		{
			StringBuilder sb = new StringBuilder(100);

			sb.Append(obj.Controller.Indent);

			#region Modifiers
			for (int i = 0; i < obj.Modifiers.Count; i++)
			{
				sb.Append(obj.Modifiers[i] + " ");
			}
			#endregion

			sb.AppendFormat("{0} operator {1}(", obj.DataType, obj.Name);

			#region Parameters
			for (int i = 0; i < obj.Parameters.Count; i++)
			{
				sb.Append(obj.Parameters[i].ToString());

				if (i < obj.Parameters.Count - 1) { sb.Append(", "); }
			}
			#endregion

			sb.Append(")");

			if (!string.IsNullOrEmpty(obj.Comments.TrailingComment))
			{
				sb.Append(" " + obj.Comments.TrailingComment);
			}

			if (includeBody)
			{
				#region Write Body

				string endLineBreak = "";

				if (obj.BodyText.Length > 0 && obj.BodyText[obj.BodyText.Length - 1] != '\r' &&
					obj.BodyText[obj.BodyText.Length - 1] != '\n')
				{
					endLineBreak = "\r";
				}
				sb.Append(
					Utility.ResetIndents(
						Helper.StandardizeLineBreaks(obj.BodyText + endLineBreak,
																   Helper.LineBreaks.Unix),
						obj.Controller.Indent));

				#endregion
			}

			return sb.ToString();
		}
	}
}
