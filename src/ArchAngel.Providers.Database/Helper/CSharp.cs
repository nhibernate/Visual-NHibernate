using System;
using System.Collections.Generic;
using System.Text;

// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;

namespace ArchAngel.Providers.Database.Helper
{
    [Interfaces.Attributes.ArchAngelEditor(false, false, "Alias")]
    public static class CSharp
    {
        private static readonly Type MapColumnType = typeof(MapColumn);

		[Interfaces.Attributes.ApiExtension]
    	public static string GetKeyValueString(Column[] columns)
        {
            return CSharp.GetKeyValues(columns, false);
        }

		[Interfaces.Attributes.ApiExtension]
        public static string GetKeyValueStringWithUnderscore(Column[] columns)
        {
            return CSharp.GetKeyValues2(columns, false, "_");
        }

		[Interfaces.Attributes.ApiExtension]
        public static string GetKeyValueStringWithClass(Column[] columns, string className)
        {
            return CSharp.GetKeyValues(columns, Script.GetCamelCase(className) + ".");
        }

		[Interfaces.Attributes.ApiExtension]
        public static string GetKeyValueStringWithDataType(Column[] columns)
        {
            return CSharp.GetKeyValues(columns, true);
        }

		[Interfaces.Attributes.ApiExtension]
        public static string GetKeyStringForConstant(Column[] columns)
        {
            return CSharp.GetKeyString(columns, true);
        }

		[Interfaces.Attributes.ApiExtension]
        public static string GetKeyString(Column[] columns, bool format)
        {
            string returnString = "";

            for (int i = 0; i < columns.Length; i++)
            {
                Column column = columns[i];
                returnString += column.Alias;

                if (i < columns.Length - 1)
                {
                    if (format)
                        returnString += "_And_";
                    else
                        returnString += "And";
                }
            }

            return (returnString);
        }

		[Interfaces.Attributes.ApiExtension]
        public static string GetKeyValues(Column[] columns, string prefix)
        {
            string returnString = "";

            for (int i = 0; i < columns.Length; i++)
            {
                Column column = columns[i];
                returnString += prefix + column.Alias;

                if (CSharp.MapColumnType.IsInstanceOfType(column) || (column.IsNullable && CSharp.ColumnIsBasicDataType(column)))
                {
                    returnString += ".Value";
                }

                if (i < columns.Length - 1)
                {
                    returnString += ", ";
                }
            }

            return (returnString);
        }

		[Interfaces.Attributes.ApiExtension]
        public static string GetKeyValues(Column[] columns, bool includeDataType)
        {
            string returnString = "";

            for (int i = 0; i < columns.Length; i++)
            {
                Column column = columns[i];

                if (includeDataType)
                    returnString += GetDataType(column) + " ";

                returnString += Script.GetCamelCase(column.Alias);

                if (i < columns.Length - 1)
                {
                    returnString += ", ";
                }
            }

            return (returnString);
        }

		[Interfaces.Attributes.ApiExtension]
        public static string GetKeyValues(Column[] columns, bool includeDataType, string prefix)
        {
            string returnString = "";

            for (int i = 0; i < columns.Length; i++)
            {
                Column column = columns[i];

                if (includeDataType)
                    returnString += CSharp.GetDataType(column) + " ";

                returnString += prefix + column.Alias;

                if (i < columns.Length - 1)
                {
                    returnString += ", ";
                }
            }

            return (returnString);
        }

		[Interfaces.Attributes.ApiExtension]
        public static string GetKeyValues2(Column[] columns, bool includeDataType, string prefix)
        {
            string returnString = "";

            for (int i = 0; i < columns.Length; i++)
            {
                Column column = columns[i];

                if (includeDataType)
                    returnString += CSharp.GetDataType(column) + " ";

                if (prefix == "_")
                {
                    returnString += prefix + Script.GetCamelCase(column.Alias);
                }
                else
                {
                    returnString += prefix + column.Alias;
                }

                if (i < columns.Length - 1)
                {
                    returnString += ", ";
                }
            }

            return (returnString);
        }

		[Interfaces.Attributes.ApiExtension]
        public static bool ColumnIsBasicDataType(Column column)
        {
            switch (column.DataType)
            {
                case "bigint":
                    return true;

                case "binary":
                    return false;

                case "bit":
                    return true;

                case "char":
                    return false;

                case "datetime":
                    return true;

                case "decimal":
                    return true;

                case "float":
                    return true;

                case "image":
                    return false;

                case "int":
                    return true;

                case "money":
                    return true;

                case "nchar":
                    return false;

                case "ntext":
                    return false;

                case "numeric":
                    return true;

                case "nvarchar":
                    return false;

                case "real":
                    return true;

                case "smalldatetime":
                    return true;

                case "smallint":
                    return true;

                case "smallmoney":
                    return true;
                
                case "sql_variant":
                    return false;

                case "text":
                    return false;

                case "timestamp":
                    return false;

                case "tinyint":
                    return true;

                case "uniqueidentifier":
                    return true;

                case "varbinary":
                    return false;

                case "varchar":
                    return false;

                case "xml":
                    return true;

                default:
                    throw new Exception(column.DataType + " data type not supported by the ArchAngel.Providers.Database.Model API yet. Please contact Slyce support: support@slyce.com");
            }
        }

		[Interfaces.Attributes.ApiExtension]
        public static string GetDataType(Column column)
        {
            bool isNullable = column.IsNullable || CSharp.MapColumnType.IsInstanceOfType(column);
            return CSharp.GetDataType(column.DataType, isNullable);
        }

		[Interfaces.Attributes.ApiExtension]
        public static string GetDataType(StoredProcedure.Parameter parameter)
        {
            bool isNullable = false; // TODO: need to add a property to Parameters that specify whether they are nullable (have default values).
            return CSharp.GetDataType(parameter.DataType, isNullable);
        }

        /// <summary>
        /// Gets the C# data-type that matches the specified SQL data-type.
        /// </summary>
        /// <param name="sqlDataType">SQL data type.</param>
        /// <param name="isNullable">Whether the data-type is nullable or not.</param>
        /// <returns></returns>
		[Interfaces.Attributes.ApiExtension]
        public static string GetDataType(string sqlDataType, bool isNullable)
        {
            string nullableType = isNullable ? "?" : "";

            switch (sqlDataType.Trim().ToLower())
            {
                case "bigint":
                    return "long" + nullableType;

                case "binary":
                    return "byte[]";

                case "bit":
                    return "bool" + nullableType;

                case "char":
                    return "string";

                case "datetime":
                    return "DateTime" + nullableType;

                case "decimal":
                    return "decimal" + nullableType;

                case "float":
                    return "double" + nullableType;

                case "image":
                    return "byte[]";

                case "int":
                    return "int" + nullableType;

                case "money":
                    return "decimal" + nullableType;

                case "nchar":
                    return "string";

                case "ntext":
                    return "string";

                case "numeric":
                    return "decimal" + nullableType;

                case "nvarchar":
                    return "string";

                case "real":
                    return "float" + nullableType;

                case "smalldatetime":
                    return "DateTime" + nullableType;

                case "smallint":
                    return "short" + nullableType;

                case "smallmoney":
                    return "decimal" + nullableType;

                case "sql_variant":
                    return "object";

                case "text":
                    return "string";

                case "timestamp":
                    return "byte[]";

                case "tinyint":
                    return "byte" + nullableType;

                case "uniqueidentifier":
                    return "Guid" + nullableType;

                case "varbinary":
                    return "byte[]";

                case "varchar":
                    return "string";

                case "xml":
                    return "string";

                default:
                    throw new Exception(sqlDataType + " data type not supported by the ArchAngel.Providers.Database.Model API yet. Please contact Slyce support: support@slyce.com");
            }
        }

		[Interfaces.Attributes.ApiExtension]
        public static string GetDatabaseType(Column column)
        {
            return CSharp.GetDatabaseType(column.DataType, column.Size);
        }

		[Interfaces.Attributes.ApiExtension]
        public static string GetDatabaseType(string sqlDataType, int size)
        {
            switch (sqlDataType.Trim().ToLower())
            {
                case "bigint":
                    return "SqlDbType.BigInt, 0";

                case "binary":
                    return "SqlDbType.Binary, 0";

                case "bit":
                    return "SqlDbType.Bit, 0";

                case "char":
                    return "SqlDbType.Char, " + size;

                case "datetime":
                    return "SqlDbType.DateTime, 0";

                case "decimal":
                    return "SqlDbType.Decimal, 0";

                case "float":
                    return "SqlDbType.Float, 0";

                case "image":
                    return "SqlDbType.Image, 0";

                case "int":
                    return "SqlDbType.Int, 0";

                case "money":
                    return "SqlDbType.Money, 0";

                case "nchar":
                    return "SqlDbType.NChar, " + size;

                case "ntext":
                    return "SqlDbType.NText, 0";

                case "numeric":
                    return "SqlDbType.Decimal, 0";

                case "nvarchar":
                    if (size == -1)
                    {
                        // TODO: MAX Varchar length
                        return "SqlDbType.NVarChar";
                    }
            		return "SqlDbType.NVarChar, " + size;

            	case "real":
                    return "SqlDbType.Real, 0";

                case "smalldatetime":
                    return "SqlDbType.SmallDateTime, 0";

                case "smallint":
                    return "SqlDbType.SmallInt, 0";

                case "smallmoney":
                    return "SqlDbType.SmallMoney, 0";

                case "sql_variant":
                    return "SqlDbType.Variant, 0";

                case "text":
                    return "SqlDbType.Text, 0";

                case "timestamp":
                    return "SqlDbType.Timestamp, 0";

                case "tinyint":
                    return "SqlDbType.TinyInt, 0";

                case "uniqueidentifier":
                    return "SqlDbType.UniqueIdentifier, 0";

                case "varbinary":
                    return "SqlDbType.VarBinary, 0";

                case "varchar":
                    if (size == -1)
                    {
                        // TODO: MAX Varchar length
                        return "SqlDbType.VarChar";
                    }
            		return "SqlDbType.VarChar, " + size;

            	case "xml":
                    return "SqlDbType.Xml, 0";

                default:
                    throw new Exception(sqlDataType + " data type not supported by the ArchAngel.Providers.Database.Model API yet. Please contact Slyce support: support@slyce.com");
            }
        }

		[Interfaces.Attributes.ApiExtension]
        public static string GetDataReaderType(Column column)
        {
            switch (column.DataType)
            {
                case "bigint":
                    return "GetInt64";

                case "binary":
                    return "GetBytes";

                case "bit":
                    return "GetBoolean";

                case "char":
                    return "GetString";

                case "datetime":
                    return "GetDateTime";

                case "decimal":
                    return "GetDecimal";

                case "float":
                    return "GetDouble";

                case "image":
                    return "GetBytes";

                case "int":
                    return "GetInt32";

                case "money":
                    return "GetDecimal";

                case "nchar":
                    return "GetString";

                case "ntext":
                    return "GetString";

                case "numeric":
                    return "GetDecimal";

                case "nvarchar":
                    return "GetString";

                case "real":
                    return "GetFloat";

                case "smalldatetime":
                    return "GetDateTime";

                case "smallint":
                    return "GetInt16";

                case "smallmoney":
                    return "GetDecimal";

                case "sql_variant":
                    return "GetValue";

                case "text":
                    return "GetString";

                case "timestamp":
                    return "GetBytes";

                case "tinyint":
                    return "GetByte";

                case "uniqueidentifier":
                    return "GetGuid";

                case "varbinary":
                    return "GetBytes";

                case "varchar":
                    return "GetString";

				case "xml":
					return "GetString";

                default:
                    throw new Exception(column.DataType + " data type not supported by the ArchAngel.Providers.Database.Model API yet. Please contact Slyce support: support@slyce.com");
            }
        }

		[Interfaces.Attributes.ApiExtension]
        public static string GetASCXConversion(Column column)
        {
            string nullableType = "";
            if (column.IsNullable && CSharp.ColumnIsBasicDataType(column))
            {
                nullableType += ".Value";
            }

            switch (column.DataType)
            {
                case "bigint":
                    return nullableType + ".ToString()";

                case "binary":
                    return nullableType + ".ToString()";

                case "bit":
                    return nullableType + "";

                case "char":
                    return nullableType + "";

                case "datetime":
                    return nullableType + ".ToString(\"dd MMM yyyy\")";

                case "decimal":
                    return nullableType + ".ToString()";

                case "float":
                    return nullableType + ".ToString()";

                case "image":
                    return nullableType + ".ToString()";

                case "int":
                    return nullableType + ".ToString()";

                case "money":
                    return nullableType + ".ToString()";

                case "nchar":
                    return nullableType + "";

                case "ntext":
                    return nullableType + "";

                case "numeric":
                    return nullableType + ".ToString()";

                case "nvarchar":
                    return nullableType + "";

                case "real":
                    return nullableType + ".ToString()";

                case "smalldatetime":
                    return nullableType + ".ToString(\"dd MMM yyyy\")";

                case "smallint":
                    return nullableType + ".ToString()";

                case "smallmoney":
                    return nullableType + ".ToString()";

                case "sql_variant":
                    return nullableType + ".ToString()";

                case "text":
                    return nullableType + "";

                case "timestamp":
                    return nullableType + ".ToString()";

                case "tinyint":
                    return nullableType + ".ToString()";

                case "uniqueidentifier":
                    return nullableType + ".ToString()";

                case "varbinary":
                    return nullableType + ".ToString()";

                case "varchar":
                    return nullableType + "";

				case "xml":
					return nullableType + "";

                default:
                    throw new Exception(column.DataType + " data type not supported by the ArchAngel.Providers.Database.Model API yet. Please contact Slyce support: support@slyce.com");
            }
        }

		[Interfaces.Attributes.ApiExtension]
        public static string GetASCXConvertStart(Column column)
        {
            switch (column.DataType)
            {
                case "bigint":
                    return "Convert.ToInt64(";

                case "binary":
                    return "Convert.ToByte[](";

                case "bit":
                    return "Convert.ToBoolean(";

                case "char":
                    return "";

                case "datetime":
                    return "DateTime.Parse(";

                case "decimal":
                    return "Convert.ToDecimal(";

                case "float":
                    return "Convert.ToDouble(";

                case "image":
                    return "Convert.ToByte[](";

                case "int":
                    return "Convert.ToInt32(";

                case "money":
                    return "Convert.ToDecimal(";

                case "nchar":
                    return "";

                case "ntext":
                    return "";

                case "numeric":
                    return "Convert.ToDecimal(";

                case "nvarchar":
                    return "";

                case "real":
                    return "Convert.ToSingle(";

                case "smalldatetime":
                    return "DateTime.Parse(";

                case "smallint":
                    return "Convert.ToInt16(";

                case "smallmoney":
                    return "Convert.ToDecimal(";

                case "sql_variant":
                    return "";

                case "text":
                    return "";

                case "timestamp":
                    return "Convert.ToByte[](";

                case "tinyint":
                    return "Convert.ToByte(";

                case "uniqueidentifier":
                    //if (_appConfig.ArchitectureName == "PetShop")
                    return "new Guid(";
                //else
                //return("";

                case "varbinary":
                    return "Convert.ToByte[](";

                case "varchar":
                    return "";

				case "xml":
					return "";

                default:
                    throw new Exception(column.DataType + " data type not supported by the ArchAngel.Providers.Database.Model API yet. Please contact Slyce support: support@slyce.com");
            }
        }

		[Interfaces.Attributes.ApiExtension]
        public static string GetASCXConvertEnd(Column column)
        {
            switch (column.DataType)
            {
                case "bigint":
                    return ")";

                case "binary":
                    return ")";

                case "bit":
                    return ")";

                case "char":
                    return "";

                case "datetime":
                    return ")";

                case "decimal":
                    return ")";

                case "float":
                    return ")";

                case "image":
                    return ")";

                case "int":
                    return ")";

                case "money":
                    return ")";

                case "nchar":
                    return "";

                case "ntext":
                    return "";

                case "numeric":
                    return ")";

                case "nvarchar":
                    return "";

                case "real":
                    return ")";

                case "smalldatetime":
                    return ")";

                case "smallint":
                    return ")";

                case "smallmoney":
                    return ")";

                case "sql_variant":
                    return "";

                case "text":
                    return "";

                case "timestamp":
                    return ")";

                case "tinyint":
                    return ")";

                case "uniqueidentifier":
                    return ")";

                case "varbinary":
                    return ")";

                case "varchar":
                    return "";

				case "xml":
					return "";

                default:
                    throw new Exception(column.DataType + " data type not supported by the ArchAngel.Providers.Database.Model API yet. Please contact Slyce support: support@slyce.com");
            }
        }

		[Interfaces.Attributes.ApiExtension]
        public static string GetXsElementsType(Column column)
        {
            switch (column.DataType)
            {
                case "bigint":
                    return "type=\"xs:long\" ";

                case "bit":
                    return "type=\"xs:boolean\" ";

                case "char":
                    return "type=\"xs:string\" ";

                case "datetime":
                    return "type=\"xs:dateTime\" ";

                case "decimal":
                    return "type=\"xs:decimal\" ";

                case "float":
                    return "type=\"xs:double\" ";

                case "image":
                    return "type=\"xs:image\" ";

                case "int":
                    return "type=\"xs:int\" ";

                case "money":
                    return "type=\"xs:decimal\" ";

                case "nchar":
                    return "type=\"xs:string\" ";

                case "ntext":
                    return "type=\"xs:string\" ";

                case "numeric":
                    return "type=\"xs:decimal\" ";

                case "nvarchar":
                    return "type=\"xs:string\" ";

                case "real":
                    return "type=\"xs:float\" ";

                case "smalldatetime":
                    return "type=\"xs:dateTime\" ";

                case "smallint":
                    return "type=\"xs:short\" ";

                case "smallmoney":
                    return "type=\"xs:decimal\" ";

                case "text":
                    return "type=\"xs:string\" ";

                case "tinyint":
                    return "type=\"xs:unsignedByte\" ";

                case "uniqueidentifier":
                    return "type=\"xs:string\" ";

                case "varchar":
                    return "type=\"xs:string\" ";

				case "xml": // TDOD: not sure this is correct
					return "type=\"xs:string\" ";

                default:
                    throw new Exception(column.DataType + " data type not supported by the ArchAngel.Providers.Database.Model API yet. Please contact Slyce support: support@slyce.com");
            }
        }

		[Interfaces.Attributes.ApiExtension]
        public static string GetXsElement(Column column)
        {
            string returnValue = "";

            if (column.IsIdentity)
            {
                returnValue += "<xs:element name=\"" + column.Alias + "\" ";
                returnValue += CSharp.GetXsElementsType(column);
                returnValue += "msdata:AutoIncrementStep=\"-1\" msdata:AutoIncrement=\"true\"";
                returnValue += "/>";
            }
            else
            {
                returnValue = "<xs:element name=\"" + column.Alias + "\" ";

                returnValue += CSharp.GetXsElementsType(column);

                if (column.IsNullable)
                {
                    returnValue += "minOccurs=\"0\" ";
                }

                if (ModelTypes.MapColumn.IsInstanceOfType(column))
                {
                    returnValue += "nillable=\"true\" ";
                }

                returnValue += "/>";
            }
            return returnValue;
        }

        /// <summary>
        /// Gets a comma-separated list of the names and data-types of the FilterColumns in the filter (eg: 'int colName1, string colName2, bool colName3')
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
		[Interfaces.Attributes.ApiExtension]
        public static string GetFilterValueAndDataTypeString(Filter filter)
        {
            string filterValueAndDataTypeString = "";

            for (int i = 0; i < filter.FilterColumns.Length; i++)
            {
                Filter.FilterColumn filterColumn = filter.FilterColumns[i];
                string alias = filterColumn.Alias;

                if (string.IsNullOrEmpty(alias))
                {
                    alias = Script.GetCamelCase(filterColumn.Column.Alias);
                }
                else
                {
                    alias = Script.GetCamelCase(alias);
                }
                filterValueAndDataTypeString += CSharp.GetDataType(filterColumn.Column) + " " + alias;

                if (i < filter.FilterColumns.Length - 1)
                {
                    filterValueAndDataTypeString += ", ";
                }
            }
            return filterValueAndDataTypeString;
        }

        /// <summary>
        /// Gets a comma-separated list of the names of the FilterColumns in the filter (eg: 'colName1, colName2, colName3')
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
		[Interfaces.Attributes.ApiExtension]
        public static string GetFilterValueString(Filter filter)
        {
            string filterValueString = "";

            for (int i = 0; i < filter.FilterColumns.Length; i++)
            {
                Filter.FilterColumn filterColumn = filter.FilterColumns[i];
                string alias = filterColumn.Alias;

                if (string.IsNullOrEmpty(alias))
                {
                    alias = Script.GetCamelCase(filterColumn.Column.Alias);
                }
                else
                {
                    alias = Script.GetCamelCase(alias);
                }

                filterValueString += alias;
                if (i < filter.FilterColumns.Length - 1)
                {
                    filterValueString += ", ";
                }
            }

            return filterValueString;
        }

        /// <summary>
        /// Gets a comma-separated list of the names of the parameters in the stored procedure (eg: 'colName1, colName2, colName3')
        /// </summary>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <returns>String of comma-sepearted parameter names.</returns>
		[Interfaces.Attributes.ApiExtension]
        public static string GetStoredProcedureParameterValueString(StoredProcedure storedProcedure)
        {
            StringBuilder result = new StringBuilder(100);

            for (int i = 0; i < storedProcedure.Parameters.Length; i++)
            {
                if (i > 0)
                {
                    result.Append(", ");
                }

                StoredProcedure.Parameter parameter = storedProcedure.Parameters[i];
                string modifier = "";

                if (parameter.Direction.ToLower().IndexOf("out") >= 0)
                {
                    modifier = "ref ";
                }
                result.Append(string.Format("{0}{1}", modifier, Script.GetCamelCase(parameter.Alias)));
            }
            return result.ToString();
        }

        /// <summary>
        /// Gets a comma-separated list of the names and data-types of the parameters in the stored procedure (eg: 'int colName1, string colName2, bool colName3')
        /// </summary>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <returns>String of comma-sepearted parameter names and data-types.</returns>
		[Interfaces.Attributes.ApiExtension]
        public static string GetStoredProcedureParameterValueAndDataTypeString(StoredProcedure storedProcedure)
        {
            StringBuilder result = new StringBuilder(100);

            for (int i = 0; i < storedProcedure.Parameters.Length; i++)
            {
                if (i > 0)
                {
                    result.Append(", ");
                }
                StoredProcedure.Parameter parameter = storedProcedure.Parameters[i];
                string alias = Script.GetCamelCase(parameter.Alias);
                string modifier = "";

                if (parameter.Direction.ToLower().IndexOf("out") >= 0)
                {
                    modifier = "ref ";
                }
                result.AppendFormat("{0}{1} {2}", modifier, CSharp.GetDataType(parameter.DataType, false), alias);
            }
            return result.ToString();
        }

        /// <summary>
        /// Gets all columns for the ScriptObject, including inherited columns.
        /// </summary>
        /// <param name="scriptObject"></param>
        /// <returns></returns>
		[Interfaces.Attributes.ApiExtension]
        public static Column[] GetSelectColumns(ScriptObject scriptObject)
        {
            OneToOneRelationship[] oneToOneRelationships = Script.GetInheritedOneToOneRelationships(scriptObject);

            Column[] columns = Script.GetColumnsAndInheritedColumns(scriptObject, oneToOneRelationships, true, false);

            // Remove duplicates for inherited columns
            List<Column> selectColumns = new List<Column>();
            foreach (Column column in columns)
            {
                Column tempColumn = Script.GetColumnByColumnAlias(selectColumns.ToArray(), column.Alias);
                if (tempColumn == null)
                {
                    selectColumns.Add(column);
                }
            }

            return selectColumns.ToArray();
        }

		[Interfaces.Attributes.ApiExtension]
        public static MapColumn[] GetSelectMapColumns(ScriptObject scriptObject)
        {
            OneToOneRelationship[] oneToOneRelationships = Script.GetInheritedOneToOneRelationships(scriptObject);

            MapColumn[] mapColumns = Script.GetMapColumnsAndInheritedMapColumns(scriptObject, oneToOneRelationships);

            return mapColumns;
        }

		[Interfaces.Attributes.ApiExtension]
        public static Column[] GetBaseColumns(ScriptObject scriptObject)
        {
            OneToOneRelationship[] oneToOneRelationships = Script.GetInheritedOneToOneRelationships(scriptObject);

            Column[] columns = Script.GetAllInheritedColumns(oneToOneRelationships, true, true);

            // Remove duplicates for inherited columns
            List<Column> baseColumns = new List<Column>();
            foreach (Column column in columns)
            {
                Column tempColumn = Script.GetColumnByColumnAlias(baseColumns.ToArray(), column.Alias);
                if (tempColumn == null)
                {
                    baseColumns.Add(column);
                }
            }

            return baseColumns.ToArray();
        }

		[Interfaces.Attributes.ApiExtension]
        public static Column[] GetInheritedColumns(ScriptObject scriptObject)
        {
            OneToOneRelationship[] oneToOneRelationships = Script.GetInheritedOneToOneRelationships(scriptObject);

            Column[] columns = Script.GetAllInheritedColumns(oneToOneRelationships, false, false);

            // Remove duplicates for inherited columns
            List<Column> inheritedColumns = new List<Column>();
            foreach (Column column in columns)
            {
                Column tempColumn = Script.GetColumnByColumnAlias(inheritedColumns.ToArray(), column.Alias);
                if (tempColumn == null)
                {
                    inheritedColumns.Add(column);
                }
            }

            return inheritedColumns.ToArray();
        }

        /// <summary>
        /// Gets all columns that can be updated (not read-only), including inherited columns.
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
		[Interfaces.Attributes.ApiExtension]
        public static Column[] GetUpdateColumns(Table table)
        {
            OneToOneRelationship[] oneToOneRelationships = Script.GetInheritedOneToOneRelationships(table);

            Column[] columns = Script.GetUpdateableColumnsAndInheritedUpdateableColumns(table, oneToOneRelationships, true, false);

            // Remove duplicates for inherited columns
            List<Column> updateColumns = new List<Column>();
            foreach (Column column in columns)
            {
                Column tempColumn = Script.GetColumnByColumnAlias(updateColumns.ToArray(), column.Alias);
                if (tempColumn == null)
                {
                    updateColumns.Add(column);
                }
            }

            return updateColumns.ToArray();
        }

        /// <summary>
        /// Gets all inherited columns that can be updated (not read-only), including inherited columns.
        /// </summary>
        /// <param name="scriptObject"></param>
        /// <returns></returns>
		[Interfaces.Attributes.ApiExtension]
        public static Column[] GetBaseUpdateColumns(ScriptObject scriptObject)
        {
            OneToOneRelationship[] oneToOneRelationships = Script.GetInheritedOneToOneRelationships(scriptObject);

            Column[] columns = Script.GetInheritedUpdateableColumns(oneToOneRelationships, true, true);

            // Remove duplicates for inherited columns
            List<Column> baseUpdateColumns = new List<Column>();
            foreach (Column column in columns)
            {
                if (column.ReadOnly)
                {
                    continue;
                }
                Column tempColumn = Script.GetColumnByColumnAlias(baseUpdateColumns.ToArray(), column.Alias);
                if (tempColumn == null)
                {
                    baseUpdateColumns.Add(column);
                }
            }

            return baseUpdateColumns.ToArray();
        }

		[Interfaces.Attributes.ApiExtension]
        public static Column[] GetOverrideColumns(ScriptObject scriptObject)
        {
            List<Column> overrideColumns = new List<Column>();

            OneToOneRelationship[] oneToOneRelationships = Script.GetDerivedOneToOneRelationships(scriptObject);
            foreach (OneToOneRelationship oneToOneRelationship in oneToOneRelationships)
            {
                overrideColumns.AddRange(oneToOneRelationship.ForeignScriptObject.Columns);
                overrideColumns.AddRange(GetOverrideColumns(oneToOneRelationship.ForeignScriptObject));
            }

            return overrideColumns.ToArray();
        }
    }
}
