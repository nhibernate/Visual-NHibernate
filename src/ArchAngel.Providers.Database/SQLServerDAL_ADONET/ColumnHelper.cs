using System;
using System.Collections.Generic;
using System.Text;

namespace ArchAngel.Providers.Database.SQLServerDAL_ADONET
{
    internal class ColumnHelper
    {
        private ColumnHelper()
        {

        }

        public static bool IsText(string dataType)
        {
            if (dataType == "varchar")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ToBoolean(string str)
        {
            if (Slyce.Common.Utility.StringsAreEqual(str, "yes", false))
            {
                return true;
            }

            if (Slyce.Common.Utility.StringsAreEqual(str, "no", false))
            {
                return false;
            }

            return Convert.ToBoolean(str);
        }
    }
}
