using System;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
	[Serializable]
	public class DataTypePrinter : IPrinter
	{
		private DataType obj;

		public DataTypePrinter(DataType obj)
		{
			this.obj = obj;
		}

		public string FullyQualifiedName
		{
			get { throw new System.NotImplementedException(); }
		}

		public string Print()
		{
			string aliasString = !string.IsNullOrEmpty(obj.Alias) ? obj.Alias + "::" : "";

			if (!obj.IsGeneric)
				return aliasString + obj.Name;

			if (obj.IsGeneric && obj.Name == "System.Nullable" && obj.GenericParameters.Count == 1)
				return obj.GenericParameters[0] + "?";

			string parameterString = "";

			for (int i = 0; i < obj.GenericParameters.Count; i++)
			{
				if (i > 0)
				{
					parameterString += ", ";
				}
				parameterString += obj.GenericParameters[i].ToString();
			}
			return aliasString + string.Format("{0}<{1}>", obj.Name, parameterString);
		}

		public string GetOuterText()
		{
			throw new InvalidOperationException("Unable to call GetOuterText on a DataType");
		}

		public string GetInnerText()
		{
			throw new InvalidOperationException("Unable to call GetOuterText on a DataType");
		}
	}
}
