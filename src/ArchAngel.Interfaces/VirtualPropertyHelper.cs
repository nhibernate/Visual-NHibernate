using ArchAngel.Interfaces.ITemplate;

namespace ArchAngel.Interfaces
{
	public static class VirtualPropertyHelper
	{
		public static bool IsEnumType(IUserOption option)
		{
			return option.DataType.IsEnum;
		}

		public static bool IsEntityPropertyType(IUserOption option)
		{
			return option.DataType.Equals(typeof(ArchAngel.Interfaces.NHibernateEnums.PropertiesForThisEntity));
		}

		public static bool IsStringType(IUserOption opt)
		{
			return opt.DataType.Equals(typeof(string));
		}

		public static bool IsCharType(IUserOption opt)
		{
			return opt.DataType.Equals(typeof(char));
		}

		public static bool IsBoolType(IUserOption opt)
		{
			return opt.DataType.Equals(typeof(bool));
		}

		public static bool IsDecimalNumericType(IUserOption opt)
		{
			return opt.DataType.Equals(typeof(float))
				   || opt.DataType.Equals(typeof(double))
				   || opt.DataType.Equals(typeof(decimal));
		}

		public static bool IsIntegerNumericType(IUserOption opt)
		{
			return opt.DataType.Equals(typeof(int))
				   || opt.DataType.Equals(typeof(uint))
				   || opt.DataType.Equals(typeof(long))
				   || opt.DataType.Equals(typeof(ulong))
				   || opt.DataType.Equals(typeof(short))
				   || opt.DataType.Equals(typeof(ushort))
				   || opt.DataType.Equals(typeof(sbyte))
				   || opt.DataType.Equals(typeof(byte));
		}
	}
}
