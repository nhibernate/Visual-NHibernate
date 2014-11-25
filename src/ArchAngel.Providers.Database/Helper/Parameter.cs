using System;

namespace ArchAngel.Providers.Database.Helper
{
    public sealed class Parameter
    {
    	public static string GetConnectionStringValue(string connectionString, string itemName)
        {
            string[] items = connectionString.Split(';');
            itemName = itemName.ToLower();

            foreach (string item in items)
            {
					if (item.Length > 0)
					{
						string[] nameValue = item.Split('=');
						string name = nameValue[0].ToLower();
						string val = nameValue[1];

						if (name == itemName ||
                            (itemName == "login" && name == "username") ||
                            (itemName == "username" && name == "login"))
						{
							return val;
						}
					}
            }
            throw new Exception("Cannot find " + itemName + " from connection string\n" + connectionString);
        }

        public static string GetNameAfterSplit(string str, char separator)
        {
            string[] ary = str.Split(separator);
            return ary[1];
        }
    }
}
