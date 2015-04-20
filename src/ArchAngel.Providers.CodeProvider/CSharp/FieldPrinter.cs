using System;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
	/// <summary>
	/// Summary description for Field.
	/// </summary>
	[Serializable]
	public class FieldPrinter : BasePrinter<Field>
	{
		private Field obj;

		public FieldPrinter(Field obj)
			: base(obj)
		{
			this.obj = obj;
		}

		public string FullyQualifiedDisplayNameExtended
		{
			get { return string.Format("{0}: {1}", obj.FullyQualifiedName, obj.DataType); }
		}

		public string DisplayNameExtended
		{
			get
			{
				return string.Format("{0}: {1}", obj.Name, obj.DataType);
			}
		}

		public override string Print()
		{
			StringBuilder sb = new StringBuilder(10000);

			sb.Append(obj.Controller.Indent);

			#region Modifiers
			for (int i = 0; i < obj.Modifiers.Count; i++)
			{
				if (obj.DisplayOptions.ListVertically)
				{
					sb.AppendLine();
					sb.Append(obj.Controller.Indent);
				}
				sb.Append(obj.Modifiers[i] + " ");
			}
			#endregion

			sb.Append(obj.DataType);

			if (obj.DisplayOptions.ListVertically)
			{
				sb.AppendLine();
				sb.Append(obj.Controller.Indent);
			}
			else
			{
				sb.Append(" ");
			}
			sb.Append(obj.Name);
			//sb.AppendFormat("{0} {1}", this.DataType, this.Name);

			if (obj.InitialValue != null && obj.InitialValue.Length > 0)
			{
				if (obj.DisplayOptions.ListVertically)
				{
					sb.AppendLine();
					sb.Append(obj.Controller.Indent);
				}
				sb.AppendFormat(" = {0}", obj.InitialValue.Trim());
			}
			sb.Append(";");

			if (!string.IsNullOrEmpty(obj.Comments.TrailingComment))
			{
				sb.Append(" " + obj.Comments.TrailingComment);
			}
			sb.AppendLine();
			return sb.ToString();
		}

		public override string GetOuterText()
		{
			throw new System.NotImplementedException();
		}

		public override string GetInnerText()
		{
			throw new System.NotImplementedException();
		}
	}
}
