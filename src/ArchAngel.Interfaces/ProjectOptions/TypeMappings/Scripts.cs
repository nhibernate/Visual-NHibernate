using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace ArchAngel.Interfaces.ProjectOptions.TypeMappings
{
	public class Scripts
	{
		private static DateTime TimeOfLastCompile = DateTime.Now;
		private static Assembly _TypeMapperAssembly;
		private static MethodInfo _GetPreProcessSqlServerMethod;
		private static MethodInfo _GetPreProcessOracleMethod;
		private static MethodInfo _GetPreProcessMySqlMethod;
		private static MethodInfo _GetPreProcessPostgreSqlMethod;
		private static MethodInfo _GetPreProcessFirebirdMethod;
		private static MethodInfo _GetPreProcessSQLiteMethod;

		#region MethodInfos

		private static MethodInfo GetPreProcessSqlServerMethod
		{
			get
			{
				if (_GetPreProcessSqlServerMethod == null || TimeOfLastCompile.CompareTo(Utility.TimeOfLastScriptChange) < 0)
					_GetPreProcessSqlServerMethod = TypeMapperAssembly.GetType("Slyce.TypeMapperScriptRunner.Runner").GetMethod("PostProcessSqlServerType");

				return _GetPreProcessSqlServerMethod;
			}
		}

		private static MethodInfo GetPreProcessOracleMethod
		{
			get
			{
				if (_GetPreProcessOracleMethod == null || TimeOfLastCompile.CompareTo(Utility.TimeOfLastScriptChange) < 0)
					_GetPreProcessOracleMethod = TypeMapperAssembly.GetType("Slyce.TypeMapperScriptRunner.Runner").GetMethod("PostProcessOracleType");

				return _GetPreProcessOracleMethod;
			}
		}

		private static MethodInfo GetPreProcessMySqlMethod
		{
			get
			{
				if (_GetPreProcessMySqlMethod == null || TimeOfLastCompile.CompareTo(Utility.TimeOfLastScriptChange) < 0)
					_GetPreProcessMySqlMethod = TypeMapperAssembly.GetType("Slyce.TypeMapperScriptRunner.Runner").GetMethod("PostProcessMySqlType");

				return _GetPreProcessMySqlMethod;
			}
		}

		private static MethodInfo GetPreProcessPostgreSqlMethod
		{
			get
			{
				if (_GetPreProcessPostgreSqlMethod == null || TimeOfLastCompile.CompareTo(Utility.TimeOfLastScriptChange) < 0)
					_GetPreProcessPostgreSqlMethod = TypeMapperAssembly.GetType("Slyce.TypeMapperScriptRunner.Runner").GetMethod("PostProcessPostgreSqlType");

				return _GetPreProcessPostgreSqlMethod;
			}
		}

		private static MethodInfo GetPreProcessFirebirdMethod
		{
			get
			{
				if (_GetPreProcessFirebirdMethod == null || TimeOfLastCompile.CompareTo(Utility.TimeOfLastScriptChange) < 0)
					_GetPreProcessFirebirdMethod = TypeMapperAssembly.GetType("Slyce.TypeMapperScriptRunner.Runner").GetMethod("PostProcessFirebirdType");

				return _GetPreProcessFirebirdMethod;
			}
		}

		private static MethodInfo GetPreProcessSQLiteMethod
		{
			get
			{
				if (_GetPreProcessSQLiteMethod == null || TimeOfLastCompile.CompareTo(Utility.TimeOfLastScriptChange) < 0)
					_GetPreProcessSQLiteMethod = TypeMapperAssembly.GetType("Slyce.TypeMapperScriptRunner.Runner").GetMethod("PostProcessSQLiteType");

				return _GetPreProcessSQLiteMethod;
			}
		}
		#endregion

		#region Calls

		public static string PostProcessSqlServerType(Utility.ColumnInfo column, string dotnetName)
		{
			object[] parms = new object[] { column, dotnetName };
			string body = (string)GetPreProcessSqlServerMethod.Invoke(null, parms);
			return body;
		}

		public static string PostProcessOracleType(Utility.ColumnInfo column, string dotnetName)
		{
			object[] parms = new object[] { column, dotnetName };
			string body = (string)GetPreProcessOracleMethod.Invoke(null, parms);
			return body;
		}

		public static string PostProcessMySqlType(Utility.ColumnInfo column, string dotnetName)
		{
			object[] parms = new object[] { column, dotnetName };
			string body = (string)GetPreProcessMySqlMethod.Invoke(null, parms);
			return body;
		}

		public static string PostProcessPostgreSqlType(Utility.ColumnInfo column, string dotnetName)
		{
			object[] parms = new object[] { column, dotnetName };
			string body = (string)GetPreProcessPostgreSqlMethod.Invoke(null, parms);
			return body;
		}

		public static string PostProcessFirebirdType(Utility.ColumnInfo column, string dotnetName)
		{
			object[] parms = new object[] { column, dotnetName };
			string body = (string)GetPreProcessFirebirdMethod.Invoke(null, parms);
			return body;
		}

		public static string PostProcessSQLiteType(Utility.ColumnInfo column, string dotnetName)
		{
			object[] parms = new object[] { column, dotnetName };
			string body = (string)GetPreProcessSQLiteMethod.Invoke(null, parms);
			return body;
		}
		#endregion

		private static Assembly TypeMapperAssembly
		{
			get
			{
				if (_TypeMapperAssembly == null || TimeOfLastCompile.CompareTo(Utility.TimeOfLastScriptChange) < 0)
				{
					string outputFilename = "";
					string exeDir = Path.GetDirectoryName(Application.ExecutablePath);
					List<string> referencedAssemblyPaths = new List<string>();
					//referencedAssemblies.Add(Path.Combine(exeDir, "ArchAngel.Scripting.dll"));
					referencedAssemblyPaths.Add(Path.Combine(exeDir, "ArchAngel.Interfaces.dll"));
					//referencedAssemblies.Add(Path.Combine(exeDir, "ArchAngel.Providers.EntityModel.dll"));
					//referencedAssemblies.Add(Path.Combine(exeDir, "ArchAngel.NHibernateHelper.dll"));
					List<System.CodeDom.Compiler.CompilerError> errors;
					_TypeMapperAssembly = CompileAssembly(referencedAssemblyPaths, null, outputFilename, out errors);

					if (_TypeMapperAssembly == null && errors.Count > 0)
					{
						throw new Exception("Errors in the Type Mapping Scripts");
					}
				}
				return _TypeMapperAssembly;
			}
		}

		private static DotNetType PostProcessDotnetTypeForSqlServer(Utility.ColumnInfo column, string dotnetTypeName)
		{
			object[] parms = new object[] { column, dotnetTypeName };
			dotnetTypeName = (string)GetPreProcessSqlServerMethod.Invoke(null, parms);
			return Utility.DotNetTypes.Single(t => t.Name == dotnetTypeName);
		}

		public static string GetTypeMappingClass()
		{
			List<string> kk = new List<string>(new string[] { "aa", "bb" });
			StringBuilder sb = new StringBuilder(2000);

			sb.Append(@"
					using System;
					using System.Xml;
					using System.Linq;
					using System.Text;
					using System.Collections.Generic;
					using ArchAngel.Interfaces;
					using ArchAngel.Interfaces.ProjectOptions.TypeMappings;

					namespace Slyce.TypeMapperScriptRunner
					{
						public class Runner
						{
							private static List<string> NullableTypes = new List<string>(new string[] 
								{
									""System.Boolean"",
									""System.Char"",
									""System.DateTime"",
									""System.Decimal"",
									""System.Double"",
									""System.Guid"",
									""System.Int16"",
									""System.Int32"",
									""System.Int64"",
									""System.SByte"",
									""System.Single"",
									""System.Timespan"",
									""System.UInt16"",
									""System.UInt32"",
									""System.UInt64""
								});

							public static bool TypeIsNullable(string type)
							{
								return NullableTypes.Contains(type);
							}
					");

			sb.AppendFormat("public static string PostProcessSqlServerType(Utility.ColumnInfo column, string dotnetTypeName){{{0}}}\n\n", Utility.PostProcessSciptSqlServer);
			sb.AppendFormat("public static string PostProcessOracleType(Utility.ColumnInfo column, string dotnetTypeName){{{0}}}\n\n", Utility.PostProcessSciptOracle);
			sb.AppendFormat("public static string PostProcessMySqlType(Utility.ColumnInfo column, string dotnetTypeName){{{0}}}\n\n", Utility.PostProcessSciptMySql);
			sb.AppendFormat("public static string PostProcessPostgreSqlType(Utility.ColumnInfo column, string dotnetTypeName){{{0}}}\n\n", Utility.PostProcessSciptPostgreSql);
			sb.AppendFormat("public static string PostProcessFirebirdType(Utility.ColumnInfo column, string dotnetTypeName){{{0}}}\n\n", Utility.PostProcessSciptFirebird);
			sb.AppendFormat("public static string PostProcessSQLiteType(Utility.ColumnInfo column, string dotnetTypeName){{{0}}}\n\n", Utility.PostProcessSciptSQLite);

			sb.Append(@"
						}
					}
			");
			return sb.ToString();
		}

		public static Assembly CompileAssembly(List<string> referencedAssemblyPaths, List<string> embeddedResourcesPaths, string outputFilename, out List<System.CodeDom.Compiler.CompilerError> errors)
		{
			List<Slyce.Common.Scripter.FileToCompile> codeBodies = new List<Slyce.Common.Scripter.FileToCompile>();
			codeBodies.Add(new Slyce.Common.Scripter.FileToCompile("TypeMapperScriptRunner", GetTypeMappingClass(), "TypeMapperScriptRunner"));
			Assembly assembly = Slyce.Common.Scripter.CompileCode(codeBodies, referencedAssemblyPaths, embeddedResourcesPaths, out errors, outputFilename);
			TimeOfLastCompile = DateTime.Now;
			return assembly;
		}
	}
}
