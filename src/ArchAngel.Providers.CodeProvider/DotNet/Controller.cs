using System;
using System.Collections.Generic;
using ArchAngel.Providers.CodeProvider.CSharp;

namespace ArchAngel.Providers.CodeProvider.DotNet
{
	public abstract class Controller
	{
		public CodeRoot Root;
		public int IndentLevel = 0;
		public bool Reorder = false;
		public bool MaintainWhitespace = false;
		public bool AddRegions = false;
		protected Dictionary<Type, Func<IPrintable, IPrinter>> dict;

		public abstract void Reset();

		public string Indent
		{
			get
			{
				if (IndentLevel < 0)
				{
					IndentLevel = 0;
				}
				string indent = new string('\t', IndentLevel);
				return indent;
			}
		}

		public IPrinter GetPrinterFor<T>(T bc) where T : IPrintable
		{
			return dict[bc.GetType()](bc);
		}

		public IPrinter TryGetPrinterFor<T>(T bc) where T : IPrintable
		{
			try
			{
				return dict[bc.GetType()](bc);
			}
			catch
			{
				return null;
			}

		}
	}

	public interface IPrintable
	{
	}
}
