using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace ArchAngel.Interfaces.ProjectOptions.DatabaseScripts
{
	public class Scripts
	{
		public class ScriptRunnerContainer
		{
			private string DatabaseType;
			private MethodInfo _HeaderMethod;
			private MethodInfo _CreateMethod;
			private MethodInfo _UpdateMethod;
			private MethodInfo _DeleteMethod;
			private DateTime TimeOfLastReset = DateTime.Now;

			public ScriptRunnerContainer(string databaseType)
			{
				DatabaseType = databaseType;
			}

			public void ResetAllMethodInfos()
			{
				_HeaderMethod = null;
				_CreateMethod = null;
				_UpdateMethod = null;
				_DeleteMethod = null;
				TimeOfLastReset = DateTime.Now;
			}

			#region Methods

			private MethodInfo HeaderMethod
			{
				get
				{
					if (_HeaderMethod == null ||
						TimeOfLastCompile.CompareTo(Utility.TimeOfLastScriptChange) < 0 ||
						TimeOfLastReset.CompareTo(TimeOfLastCompile) < 0)
					{
						ResetAllMethodInfos();
						_HeaderMethod = TypeMapperAssembly.GetType(string.Format("Slyce.DBScriptRunner.{0}", DatabaseType)).GetMethod("GetHeaderScript");
					}
					return _HeaderMethod;
				}
			}

			private MethodInfo CreateMethod
			{
				get
				{
					if (_CreateMethod == null ||
						TimeOfLastCompile.CompareTo(Utility.TimeOfLastScriptChange) < 0 ||
						TimeOfLastReset.CompareTo(TimeOfLastCompile) < 0)
					{
						ResetAllMethodInfos();
						_CreateMethod = TypeMapperAssembly.GetType(string.Format("Slyce.DBScriptRunner.{0}", DatabaseType)).GetMethod("GetCreateScript");
					}
					return _CreateMethod;
				}
			}

			private MethodInfo UpdateMethod
			{
				get
				{
					if (_UpdateMethod == null ||
						TimeOfLastCompile.CompareTo(Utility.TimeOfLastScriptChange) < 0 ||
						TimeOfLastReset.CompareTo(TimeOfLastCompile) < 0)
					{
						ResetAllMethodInfos();
						_UpdateMethod = TypeMapperAssembly.GetType(string.Format("Slyce.DBScriptRunner.{0}", DatabaseType)).GetMethod("GetUpdateScript");
					}
					return _UpdateMethod;
				}
			}

			private MethodInfo DeleteMethod
			{
				get
				{
					if (_DeleteMethod == null ||
						TimeOfLastCompile.CompareTo(Utility.TimeOfLastScriptChange) < 0 ||
						TimeOfLastReset.CompareTo(TimeOfLastCompile) < 0)
					{
						ResetAllMethodInfos();
						_DeleteMethod = TypeMapperAssembly.GetType(string.Format("Slyce.DBScriptRunner.{0}", DatabaseType)).GetMethod("GetDeleteScript");
					}
					return _DeleteMethod;
				}
			}
			#endregion

			#region Calls
			public string GetHeader(ArchAngel.Interfaces.Scripting.DatabaseChanges.IDatabase database)
			{
				object[] parms = new object[] { database };
				string body = (string)HeaderMethod.Invoke(null, parms);

				if (body.StartsWith("          "))
					body = body.Substring(10);

				body = Slyce.Common.Scripter.RemoveDebugSymbols(body);
				return body;
			}

			public string GetCreate(ArchAngel.Interfaces.Scripting.NHibernate.Model.ITable table)
			{
				object[] parms = new object[] { table };
				string body = (string)CreateMethod.Invoke(null, parms);

				if (body.StartsWith("          "))
					body = body.Substring(10);

				return body;
			}

			public string GetUpdate(ArchAngel.Interfaces.Scripting.DatabaseChanges.IChangedTable table)
			{
				object[] parms = new object[] { table };
				string body = (string)UpdateMethod.Invoke(null, parms);

				if (body.StartsWith("          "))
					body = body.Substring(10);

				return body;
			}

			public string GetDelete(ArchAngel.Interfaces.Scripting.NHibernate.Model.ITable table)
			{
				object[] parms = new object[] { table };
				string body = (string)DeleteMethod.Invoke(null, parms);

				if (body.StartsWith("          "))
					body = body.Substring(10);

				return body;
			}

			#endregion

		}
		private static DateTime TimeOfLastCompile = DateTime.Now;
		private static Assembly _TypeMapperAssembly;
		public static ScriptRunnerContainer ContainerForSqlServer = new ScriptRunnerContainer("SqlServer");
		public static ScriptRunnerContainer ContainerForOracle = new ScriptRunnerContainer("Oracle");
		public static ScriptRunnerContainer ContainerForMySql = new ScriptRunnerContainer("MySQL");
		public static ScriptRunnerContainer ContainerForPostgreSql = new ScriptRunnerContainer("PostgreSQL");
		public static ScriptRunnerContainer ContainerForFirebird = new ScriptRunnerContainer("Firebird");

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
						string s = Environment.NewLine;

						foreach (var err in errors)
						{
							s += err.ErrorText + Environment.NewLine;
						}
						throw new Exception("Errors in the Database Scripts:" + s);
					}
				}
				return _TypeMapperAssembly;
			}
		}

		public static Assembly CompileAssembly(List<string> referencedAssemblyPaths, List<string> embeddedResourcesPaths, string outputFilename, out List<System.CodeDom.Compiler.CompilerError> errors)
		{
			List<Slyce.Common.Scripter.FileToCompile> codeBodies = new List<Slyce.Common.Scripter.FileToCompile>();
			codeBodies.Add(new Slyce.Common.Scripter.FileToCompile("DBScriptRunnerBase", GetBaseClass(), "DBScriptRunnerBase"));
			codeBodies.Add(new Slyce.Common.Scripter.FileToCompile("DBScriptRunnerSqlServer", GetClass("SqlServer", Utility.SqlServerScript), "DBScriptRunnerSqlServer"));
			codeBodies.Add(new Slyce.Common.Scripter.FileToCompile("DBScriptRunnerOracle", GetClass("Oracle", Utility.OracleScript), "DBScriptRunnerOracle"));
			codeBodies.Add(new Slyce.Common.Scripter.FileToCompile("DBScriptRunnerMySQL", GetClass("MySQL", Utility.MySqlScript), "DBScriptRunnerMySQL"));
			codeBodies.Add(new Slyce.Common.Scripter.FileToCompile("DBScriptRunnerPostgreSQL", GetClass("PostgreSQL", Utility.PostgreSqlScript), "DBScriptRunnerPostgreSQL"));
			codeBodies.Add(new Slyce.Common.Scripter.FileToCompile("DBScriptRunnerFirebird", GetClass("Firebird", Utility.FirebirdScript), "DBScriptRunnerFirebird"));
			codeBodies.Add(new Slyce.Common.Scripter.FileToCompile("DBScriptRunnerSQLite", GetClass("SQLite", Utility.SQLiteScript), "DBScriptRunnerSQLite"));
			Assembly assembly = Slyce.Common.Scripter.CompileCode(codeBodies, referencedAssemblyPaths, embeddedResourcesPaths, out errors, outputFilename);
			TimeOfLastCompile = DateTime.Now;
			return assembly;
		}

		private static string GetClass(string databaseType, MaintenanceScript script)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat(@"
					using System;
					using System.Xml;
					using System.Linq;
					using System.Text;
					using System.Collections.Generic;
					using ArchAngel.Interfaces;
					using ArchAngel.Interfaces.ProjectOptions.TypeMappings;
					using ArchAngel.Interfaces.Scripting.NHibernate.Model;

					namespace Slyce.DBScriptRunner
					{{
						public class {0} : RunnerBase
						{{
							public static string GetHeaderScript(ArchAngel.Interfaces.Scripting.DatabaseChanges.IDatabase database)
							{{
								{1}
							}}

							public static string GetCreateScript(ArchAngel.Interfaces.Scripting.NHibernate.Model.ITable table)
							{{
								{2}
							}}

							public static string GetUpdateScript(ArchAngel.Interfaces.Scripting.DatabaseChanges.IChangedTable table)
							{{
								{3}
							}}

							public static string GetDeleteScript(ArchAngel.Interfaces.Scripting.NHibernate.Model.ITable table)
							{{
								{4}
							}}
						}}
					}}",
					databaseType,
					Slyce.Common.Scripter.FormatFunctionBodyAsTemplate("xx", script.Header, "<%", "%>"),
					Slyce.Common.Scripter.FormatFunctionBodyAsTemplate("yy", script.Create, "<%", "%>"),
					Slyce.Common.Scripter.FormatFunctionBodyAsTemplate("zz", script.Update, "<%", "%>"),
					Slyce.Common.Scripter.FormatFunctionBodyAsTemplate("aa", script.Delete, "<%", "%>"));

			string g = Slyce.Common.Scripter.RemoveDebugSymbols(sb.ToString());
			g = g.Replace("\n          ", "\n");
			return g;
		}

		public static string GetCodeForDebuggingHeader(string bodyText)
		{
			StringBuilder sb = new StringBuilder(1000);
			sb.Append(@"
						public class xxx : RunnerBase
						{
							public static string GetHeaderScript(ArchAngel.Interfaces.Scripting.DatabaseChanges.IDatabase database)
							{
						");
			sb.Append(bodyText);
			return GetBaseClass(sb.ToString());
		}

		public static string GetCodeForDebuggingTable(string bodyText)
		{
			StringBuilder sb = new StringBuilder(1000);
			sb.Append(@"
						public class xxx : RunnerBase
						{
							public static string GetCreateScript(ArchAngel.Interfaces.Scripting.NHibernate.Model.ITable table)
							{
						");
			sb.Append(bodyText);
			return GetBaseClass(sb.ToString());
		}

		public static string GetCodeForDebuggingChangedTable(string bodyText)
		{
			StringBuilder sb = new StringBuilder(1000);
			sb.Append(@"
						public class xxx : RunnerBase
						{
							public static string GetUpdateScript(ArchAngel.Interfaces.Scripting.DatabaseChanges.IChangedTable table)
							{
						");
			sb.Append(bodyText);
			return GetBaseClass(sb.ToString());
		}

		private static string GetBaseClass()
		{
			return GetBaseClass("");
		}

		private static string GetBaseClass(string extraClass)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(@"
					using System;
					using System.Xml;
					using System.Linq;
					using System.Text;
					using System.Collections.Generic;
					using ArchAngel.Interfaces;
					using ArchAngel.Interfaces.ProjectOptions.DatabaseScripts;
					using ArchAngel.Interfaces.ProjectOptions.TypeMappings;

					namespace Slyce.DBScriptRunner
					{
						public class RunnerBase
						{
							protected static Stack<StringBuilder> _SBStack = new Stack<StringBuilder>();

							protected static void Write(object s)
							{
								if (s != null)
								{
									StringBuilder b = _SBStack.Peek();
									b.Insert(b.Length, s.ToString());
								}
							}
							
							protected static void WriteLine(object s)
							{
								if (s != null)
								{
									StringBuilder b = _SBStack.Peek();
									b.Insert(b.Length, s.ToString() + Environment.NewLine);
								}
							}

							protected static void WriteFormat(string format, params object[] args)
							{
								if (!string.IsNullOrEmpty(format))
								{
									StringBuilder b = _SBStack.Peek();
									b.Insert(b.Length, string.Format(format, args));
								}
							}

							protected static void WriteIf(bool val, object trueText)
							{
								if (val && trueText != null)
								{
									StringBuilder b = _SBStack.Peek();
									b.Insert(b.Length, trueText.ToString());
								}
							}

							protected static void WriteIf(bool val, object trueText, object falseText)
							{
								if (val && trueText != null)
								{
									StringBuilder b = _SBStack.Peek();
									b.Insert(b.Length, trueText.ToString());
								}
								else if (!val && falseText != null)
								{
									StringBuilder b = _SBStack.Peek();
									b.Insert(b.Length, falseText.ToString());
								}
							}
						}
					");

			if (!string.IsNullOrEmpty(extraClass))
				sb.Append(extraClass);
			else
				sb.Append(@"
					}");

			string g = Slyce.Common.Scripter.RemoveDebugSymbols(sb.ToString());
			return g;
		}
	}
}