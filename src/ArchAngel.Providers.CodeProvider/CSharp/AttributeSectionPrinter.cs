using System;
using System.Text;
using Attribute = ArchAngel.Providers.CodeProvider.DotNet.Attribute;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
	[Serializable]
	public class AttributeSectionPrinter : BasePrinter<AttributeSection>
	{
		private readonly AttributeSection obj;

		public AttributeSectionPrinter(AttributeSection obj)
			: base(obj)
		{
			this.obj = obj;
		}

		public override string Print()
		{
			StringBuilder sb = new StringBuilder(100);

			sb.Append(obj.Controller.Indent + "[");

			if (!string.IsNullOrEmpty(obj.Target))
			{
				sb.Append(obj.Target + ": ");
			}
			for (int i = 0; i < obj.SingleAttributes.Count; i++)
			{
				Attribute att = obj.SingleAttributes[i];

				if (i > 0) { sb.Append(", "); }

				if (obj.SingleAttributes.Count > 1)
				{
					sb.AppendLine(obj.Controller.Indent);
				}
				sb.Append(att.ToString());
			}
			if (obj.SingleAttributes.Count > 1)
			{
				sb.AppendLine(obj.Controller.Indent);
			}
			sb.Append("]");
			return sb.ToString();
		}
	}
}
