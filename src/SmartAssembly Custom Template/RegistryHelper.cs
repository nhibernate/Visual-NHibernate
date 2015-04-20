using System;
using Microsoft.Win32;

namespace SmartAssembly.SmartExceptionsWithAdvancedUI
{
	internal class RegistryHelper
	{
		private const string REGISTRY_ROOT = @"SOFTWARE\MyCompany\MyProduct";

		public static string ReadHKLMRegistryString(string name)
		{
			try
			{
				RegistryKey key = Registry.LocalMachine.OpenSubKey(REGISTRY_ROOT);
				if (key == null) return string.Empty;

				string value = (string)key.GetValue(name, string.Empty);
				key.Close();
				return value;
			}
			catch
			{
				return string.Empty;
			}
		}

		public static void SaveHKLMRegistryString(string name, string value)
		{
			try
			{
				RegistryKey key = Registry.LocalMachine.OpenSubKey(REGISTRY_ROOT, true);
				if (key == null)
				{
					key = Registry.LocalMachine.CreateSubKey(REGISTRY_ROOT);
				}

				key.SetValue(name, value);
				key.Close();
			}
			catch
			{
			}
		}
	}
}
