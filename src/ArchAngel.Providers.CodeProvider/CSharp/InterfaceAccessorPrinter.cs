using System;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
	/// <summary>
	/// Summary description for InterfaceProperty.
	/// </summary>
	[Serializable]
	public class InterfaceAccessorPrinter : BasePrinter<InterfaceAccessor>
	{
		private readonly InterfaceAccessor obj;

		public InterfaceAccessorPrinter(InterfaceAccessor obj)
			: base(obj)
		{
			this.obj = obj;
		}

		public override string Print()
		{
			return string.Join(" ", obj.Modifiers.ToArray()) + ((obj.AccessorType == InterfaceAccessor.AccessorTypes.Get) ? " get;" : " set;");
		}

	}
}
