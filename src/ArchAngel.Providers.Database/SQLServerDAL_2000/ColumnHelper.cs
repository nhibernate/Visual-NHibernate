using System;
using System.Collections.Generic;
using System.Text;

// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;
using ArchAngel.Providers.Database.IDAL;
using ArchAngel.Providers.Database.Helper;

namespace ArchAngel.Providers.Database.SQLServerDAL_2000
{
    internal class ColumnHelper
    {
        private ColumnHelper()
        {
        }

        #region datatypes

        public static bool IsDataTypeBit(string dataType)
        {
            switch (dataType)
            {
                case "bit":
                    return true;

                default:
                    return false;
            }
        }

        public static bool IsDataTypeBinary(string dataType)
        {
            switch (dataType)
            {
                case "binary":
                    return true;

                case "image":
                    return true;

                case "timestamp":
                    return true;

                case "varbinary":
                    return true;

                default:
                    return false;
            }
        }

        public static bool IsDataTypeText(string dataType)
        {
            switch (dataType)
            {
                case "char":
                    return true;

                case "nchar":
                    return true;

                case "ntext":
                    return true;

                case "nvarchar":
                    return true;

                case "text":
                    return true;

                case "varchar":
                    return true;

                default:
                    return false;
            }
        }

        public static bool IsDataTypeFixedChar(string dataType)
        {
            switch (dataType)
            {
                case "char":
                    return true;

                case "nchar":
                    return true;

                case "ntext":
                    return true;

                case "nvarchar":
                    return true;

                default:
                    return false;
            }
        }

        public static bool IsDataTypeNumeric(string dataType)
        {
            switch (dataType)
            {
                case "bigint":
                    return true;

                case "decimal":
                    return true;

                case "float":
                    return true;

                case "int":
                    return true;

                case "money":
                    return true;

                case "numeric":
                    return true;

                case "real":
                    return true;

                case "smallint":
                    return true;

                case "smallmoney":
                    return true;

                case "tinyint":
                    return true;

                default:
                    return false;
            }
        }

        public static bool IsDataTypeWholeNumber(string dataType)
        {
            switch (dataType)
            {
                case "bigint":
                    return true;

                case "decimal":
                    return true;

                case "int":
                    return true;

                case "numeric":
                    return true;

                case "smallint":
                    return true;

                case "tinyint":
                    return true;

                default:
                    return false;
            }
        }

        public static bool IsDataTypeDate(string dataType)
        {
            switch (dataType)
            {
                case "datetime":
                    return true;

                case "smalldatetime":
                    return true;

                default:
                    return false;
            }
        }

        public static bool IsDataTypeChar(string dataType)
        {
            switch (dataType)
            {
                case "char":
                    return true;

                case "nchar":
                    return true;

                case "nvarchar":
                    return true;

                case "varchar":
                    return true;

                default:
                    return false;
            }
        }

        #endregion

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