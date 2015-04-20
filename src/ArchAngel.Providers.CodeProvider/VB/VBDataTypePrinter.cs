using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
	class VBDataTypePrinter : IPrinter
	{
		private DataType obj;

		public VBDataTypePrinter(DataType obj)
		{
			this.obj = obj;
		}

		public string FullyQualifiedName
		{
			get { throw new System.NotImplementedException(); }
		}

		public string Print()
		{
			//string aliasString = !string.IsNullOrEmpty(obj.Alias) ? obj.Alias + "::" : "";
			string aliasString = ""; // VB.Net doesn't support aliases. See: http://msdn.microsoft.com/en-us/library/c3ay4x3d(v=VS.100).aspx

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
			throw new System.NotImplementedException();
		}

		public string GetInnerText()
		{
			throw new System.NotImplementedException();
		}
	}
}
