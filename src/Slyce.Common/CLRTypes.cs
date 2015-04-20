using System.Collections.Generic;

namespace Slyce.Common
{
	public static class CLRTypes
	{
		private static HashSet<string> NumericTypes = new HashSet<string>
		                                              	{
                                                            "sbyte", "system.sbyte",
                                                            "byte", "system.byte",
                                                            "short", "int16", "system.int16",
															"ushort", "uint16", "system.uint16",
															"int", "int32", "system.int32",
                                                            "uint", "uint32", "system.uint32",
                                                            "long", "int64", "system.int64",
                                                            "ulong", "uint64", "system.uint64",
                                                            "float", "single", "system.single",
                                                            "double", "system.double"
		                                              	};



		public static bool IsNumericType(string typename)
		{
			if(typename == null) return false;

			return NumericTypes.Contains(typename.ToLower());
		}

		public static bool IsString(string typename)
		{
			if(typename == null) return false;
			
			var lower = typename.ToLower();
			return lower == "string" || lower == "system.string";
		}
	}
}