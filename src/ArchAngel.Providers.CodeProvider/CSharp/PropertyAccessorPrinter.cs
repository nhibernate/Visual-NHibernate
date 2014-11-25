using System;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
	/// <summary>
	/// Summary description for PropertyAccessor.
	/// </summary>
	[Serializable]
	public class PropertyAccessorPrinter : BasePrinter<PropertyAccessor>
	{
		private PropertyAccessor obj;

		public PropertyAccessorPrinter(PropertyAccessor obj)
			: base(obj)
		{
			this.obj = obj;
		}

		public override string Print()
		{
			return obj.ToString(true);
		}


	}
}
