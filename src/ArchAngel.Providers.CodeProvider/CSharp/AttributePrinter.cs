using System;
using System.Text;
using Attribute = ArchAngel.Providers.CodeProvider.DotNet.Attribute;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
	[Serializable]
	public class AttributePrinter : BasePrinter<Attribute>
	{
		private readonly Attribute attr;

		public AttributePrinter(Attribute attr)
			: base(attr)
		{
			this.attr = attr;
		}

		public override string FullyQualifiedName
		{
			get
			{
				if (attr.ParentObject is AttributeSection)
				{
					if (attr.ParentObject.ParentObject != null)
						return string.Format("{0}{2}[{1}]", attr.ParentObject.ParentObject.FullyQualifiedName, attr.Name, ".");
					return string.Format("[{0}]", attr.Name);
				}

				if (attr.ParentObject != null && attr.ParentObject is CSharpParser.CodeRootBaseConstructAdapter == false)
					return string.Format("{0}{2}[{1}]", attr.ParentObject.FullyQualifiedName, attr.Name, ".");

				return string.Format("[{0}]", attr.Name);
			}
		}

		public override string Print()
		{
			StringBuilder sb = new StringBuilder(100);

			sb.Append(string.Format("{0}", attr.Name));

			if (attr.PositionalArguments.Count + attr.NamedArguments.Count > 0)
			{
				sb.Append("(");

				for (int i = 0; i < attr.PositionalArguments.Count; i++)
				{
					if (i > 0) { sb.Append(", "); }

					sb.Append(attr.PositionalArguments[i]);
				}
				for (int i = 0; i < attr.NamedArguments.Count; i++)
				{
					if (attr.PositionalArguments.Count > 0 || i > 0) { sb.Append(", "); }

					sb.Append(string.Format("{0} = {1}", attr.NamedArguments[i].Name, attr.NamedArguments[i].Value));
				}
				sb.Append(")");
			}
			if (CodeRoot.IncludeExtraLines)
			{
				string text = sb.ToString();
				int numLines = Helper.GetNumberOfLines(text);

				if (attr.NumberOfLines > numLines)
				{
					return text + new string('\n', attr.NumberOfLines - numLines);
				}

				return text;
			}

			return sb.ToString();
		}
	}
}
