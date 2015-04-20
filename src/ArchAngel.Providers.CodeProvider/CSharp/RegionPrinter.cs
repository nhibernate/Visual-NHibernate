using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
	public class RegionPrinter : BasePrinter<Region>
	{
		private Region obj;

		public RegionPrinter(Region obj)
			: base(obj)
		{
			this.obj = obj;
		}

		public override string Print()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(obj.Controller.Indent).Append("#region ").Append(obj.Name);

			foreach (IBaseConstruct child in obj.children)
			{
				sb.Append(child.ToString());
			}

			string text = sb.ToString();
			// Determine if the indent has already been appended
			if (text.Substring(text.Length - obj.Controller.Indent.Length) != obj.Controller.Indent)
				sb.Append(obj.Controller.Indent);
			sb.AppendLine("#endregion");

			return Utility.ResetIndents(Helper.StandardizeLineBreaks(sb.ToString(), Helper.LineBreaks.Unix),
									 obj.Controller.Indent);
		}

		/// <summary>
		/// Gets the inner text of the node, if it has any. If it doesn't, it returns string.Empty.
		/// </summary>
		/// <returns>The inner text of the node, if it has any. If it doesn't, it returns string.Empty.</returns>
		public override string GetInnerText()
		{
			return string.Empty;
		}

		/// <summary>
		/// Gets the outer text of the node. 
		/// </summary>
		/// <remarks>This is the method signature for a C# Function, the element text (<![CDATA[<element></element>]]>) for an XML Element, etc.</remarks>
		/// <returns>The outer text of the node.</returns>
		public override string GetOuterText()
		{
			return ToString();
		}
	}
}
