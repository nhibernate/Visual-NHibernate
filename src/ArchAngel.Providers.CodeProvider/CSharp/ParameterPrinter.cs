using System;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
	/// <summary>
	/// Summary description for Parameter.
	/// </summary>
	[Serializable]
	public class ParameterPrinter : BasePrinter<Parameter>
	{
		private Parameter obj;

		public ParameterPrinter(Parameter obj)
			: base(obj)
		{
			this.obj = obj;
		}

		public override string FullyQualifiedName
		{
			get { throw new System.NotImplementedException(); }
		}

		public override string Print()
		{
			string modifierString = "";

			foreach (string modifier in obj.Modifiers)
			{
				modifierString += modifier + " ";
			}
			string isParamText = obj.IsParams ? "params " : "";
			return string.Format("{3}{0}{1} {2}", isParamText, obj.DataType, obj.Name, modifierString);
		}
	}
}
