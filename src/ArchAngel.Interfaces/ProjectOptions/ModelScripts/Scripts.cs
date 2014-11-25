using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace ArchAngel.Interfaces.ProjectOptions.ModelScripts
{
	public class Scripts
	{
		private static DateTime TimeOfLastCompile = DateTime.Now;
		private static Assembly _TypeMapperAssembly;
		private static MethodInfo _EntityNameMethod;
		private static MethodInfo _PropertyNameMethod;

		private static FieldInfo _FieldExistingEntityNames;
		private static FieldInfo _FieldExistingPropertyNames;
		private static FieldInfo _FieldTablePrefixes;
		private static FieldInfo _FieldColumnPrefixes;
		private static FieldInfo _FieldTableSuffixes;
		private static FieldInfo _FieldColumnSuffixes;

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
						throw new Exception("Errors in the Model Scripts: " + s);
					}
				}
				return _TypeMapperAssembly;
			}
		}

		public static Assembly CompileAssembly(List<string> referencedAssemblyPaths, List<string> embeddedResourcesPaths, string outputFilename, out List<System.CodeDom.Compiler.CompilerError> errors)
		{
			List<Slyce.Common.Scripter.FileToCompile> codeBodies = new List<Slyce.Common.Scripter.FileToCompile>();
			codeBodies.Add(new Slyce.Common.Scripter.FileToCompile("ModelScriptRunnerBase", GetBaseClass(), "ModelScriptRunnerBase"));
			codeBodies.Add(new Slyce.Common.Scripter.FileToCompile("ModelScriptRunner_EntityNaming", GetEntityNameClass(Utility.EntityNamingScript), "ModelScriptRunner_EntityNaming"));
			codeBodies.Add(new Slyce.Common.Scripter.FileToCompile("ModelScriptRunner_PropertyNaming", GetPropertyNameClass(Utility.PropertyNamingScript), "ModelScriptRunner_PropertyNaming"));
			Assembly assembly = Slyce.Common.Scripter.CompileCode(codeBodies, referencedAssemblyPaths, embeddedResourcesPaths, out errors, outputFilename);
			TimeOfLastCompile = DateTime.Now;
			_EntityNameMethod = null;
			_PropertyNameMethod = null;
			_FieldExistingEntityNames = null;
			_FieldExistingPropertyNames = null;
			_FieldTablePrefixes = null;
			_FieldColumnPrefixes = null;
			_FieldTableSuffixes = null;
			_FieldColumnSuffixes = null;
			return assembly;
		}

		private static MethodInfo EntityNameMethod
		{
			get
			{
				if (_EntityNameMethod == null ||
					TimeOfLastCompile.CompareTo(Utility.TimeOfLastScriptChange) < 0)// ||
				//TimeOfLastReset.CompareTo(TimeOfLastCompile) < 0)
				{
					_EntityNameMethod = TypeMapperAssembly.GetType("Slyce.ModelScriptRunner.EntityNaming").GetMethod("GetEntityName");
				}
				return _EntityNameMethod;
			}
		}

		private static MethodInfo PropertyNameMethod
		{
			get
			{
				if (_PropertyNameMethod == null ||
					TimeOfLastCompile.CompareTo(Utility.TimeOfLastScriptChange) < 0)// ||
				//TimeOfLastReset.CompareTo(TimeOfLastCompile) < 0)
				{
					_PropertyNameMethod = TypeMapperAssembly.GetType("Slyce.ModelScriptRunner.PropertyNaming").GetMethod("GetPropertyName");
				}
				return _PropertyNameMethod;
			}
		}

		private static FieldInfo FieldExistingEntityNames
		{
			get
			{
				if (_FieldExistingEntityNames == null ||
					TimeOfLastCompile.CompareTo(Utility.TimeOfLastScriptChange) < 0)// ||
				//TimeOfLastReset.CompareTo(TimeOfLastCompile) < 0)
				{
					_FieldExistingEntityNames = TypeMapperAssembly.GetType("Slyce.ModelScriptRunner.RunnerBase").GetField("ExistingEntityNames");
				}
				return _FieldExistingEntityNames;
			}
		}

		private static FieldInfo FieldExistingPropertyNames
		{
			get
			{
				if (_FieldExistingPropertyNames == null ||
					TimeOfLastCompile.CompareTo(Utility.TimeOfLastScriptChange) < 0)// ||
				//TimeOfLastReset.CompareTo(TimeOfLastCompile) < 0)
				{
					_FieldExistingPropertyNames = TypeMapperAssembly.GetType("Slyce.ModelScriptRunner.RunnerBase").GetField("ExistingPropertyNames");
				}
				return _FieldExistingPropertyNames;
			}
		}

		private static FieldInfo FieldTablePrefixes
		{
			get
			{
				if (_FieldTablePrefixes == null ||
					TimeOfLastCompile.CompareTo(Utility.TimeOfLastScriptChange) < 0)// ||
				//TimeOfLastReset.CompareTo(TimeOfLastCompile) < 0)
				{
					_FieldTablePrefixes = TypeMapperAssembly.GetType("Slyce.ModelScriptRunner.RunnerBase").GetField("TablePrefixes");
				}
				return _FieldTablePrefixes;
			}
		}

		private static FieldInfo FieldColumnPrefixes
		{
			get
			{
				if (_FieldColumnPrefixes == null ||
					TimeOfLastCompile.CompareTo(Utility.TimeOfLastScriptChange) < 0)// ||
				//TimeOfLastReset.CompareTo(TimeOfLastCompile) < 0)
				{
					_FieldColumnPrefixes = TypeMapperAssembly.GetType("Slyce.ModelScriptRunner.RunnerBase").GetField("ColumnPrefixes");
				}
				return _FieldColumnPrefixes;
			}
		}

		private static FieldInfo FieldTableSuffixes
		{
			get
			{
				if (_FieldTableSuffixes == null ||
					TimeOfLastCompile.CompareTo(Utility.TimeOfLastScriptChange) < 0)
				{
					_FieldTableSuffixes = TypeMapperAssembly.GetType("Slyce.ModelScriptRunner.RunnerBase").GetField("TableSuffixes");
				}
				return _FieldTableSuffixes;
			}
		}

		private static FieldInfo FieldColumnSuffixes
		{
			get
			{
				if (_FieldColumnSuffixes == null ||
					TimeOfLastCompile.CompareTo(Utility.TimeOfLastScriptChange) < 0)
				{
					_FieldColumnSuffixes = TypeMapperAssembly.GetType("Slyce.ModelScriptRunner.RunnerBase").GetField("ColumnSuffixes");
				}
				return _FieldColumnSuffixes;
			}
		}

		public static string GetEntityName(ArchAngel.Interfaces.Scripting.NHibernate.Model.ITable table)
		{
			object[] parms = new object[] { table };
			string body = (string)EntityNameMethod.Invoke(null, parms);

			if (body.StartsWith("          "))
				body = body.Substring(10);

			return body;
		}

		public static string GetPropertyName(ArchAngel.Interfaces.Scripting.NHibernate.Model.IColumn column)
		{
			object[] parms = new object[] { column };
			string body = (string)PropertyNameMethod.Invoke(null, parms);

			if (body.StartsWith("          "))
				body = body.Substring(10);

			return body;
		}

		public static List<string> ExistingEntityNames
		{
			get { return (List<string>)FieldExistingEntityNames.GetValue(null); }
			set { FieldExistingEntityNames.SetValue(null, value); }
		}

		public static List<string> ExistingPropertyNames
		{
			get { return (List<string>)FieldExistingPropertyNames.GetValue(null); }
			set { FieldExistingPropertyNames.SetValue(null, value); }
		}

		public static List<string> TablePrefixes
		{
			get { return (List<string>)FieldTablePrefixes.GetValue(null); }
			set { FieldTablePrefixes.SetValue(null, value); }
		}

		public static List<string> ColumnPrefixes
		{
			get { return (List<string>)FieldColumnPrefixes.GetValue(null); }
			set { FieldColumnPrefixes.SetValue(null, value); }
		}

		public static List<string> TableSuffixes
		{
			get { return (List<string>)FieldTableSuffixes.GetValue(null); }
			set { FieldTableSuffixes.SetValue(null, value); }
		}

		public static List<string> ColumnSuffixes
		{
			get { return (List<string>)FieldColumnSuffixes.GetValue(null); }
			set { FieldColumnSuffixes.SetValue(null, value); }
		}

		private static string GetEntityNameClass(string body)
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
					using Slyce.Common;
					using Slyce.Common.StringExtensions;

					namespace Slyce.ModelScriptRunner
					{{
						public class EntityNaming : RunnerBase
						{{
							public static string GetEntityName(ArchAngel.Interfaces.Scripting.NHibernate.Model.ITable table)
							{{
								{0}
							}}
						}}
					}}",
					body);

			string g = Slyce.Common.Scripter.RemoveDebugSymbols(sb.ToString());
			g = g.Replace("\n          ", "\n");
			return g;
		}

		private static string GetPropertyNameClass(string body)
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
					using Slyce.Common;
					using Slyce.Common.StringExtensions;

					namespace Slyce.ModelScriptRunner
					{{
						public class PropertyNaming : RunnerBase
						{{
							public static string GetPropertyName(ArchAngel.Interfaces.Scripting.NHibernate.Model.IColumn column)
							{{
								{0}
							}}
						}}
					}}",
					body);

			string g = Slyce.Common.Scripter.RemoveDebugSymbols(sb.ToString());
			g = g.Replace("\n          ", "\n");
			return g;
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

		public static string GetCodeForDebuggingColumn(string bodyText)
		{
			StringBuilder sb = new StringBuilder(1000);
			sb.Append(@"
						public class xxx : RunnerBase
						{
							public static string GetCreateScript(ArchAngel.Interfaces.Scripting.NHibernate.Model.IColumn column)
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
					using Slyce.Common;

					namespace Slyce.ModelScriptRunner
					{
						public class RunnerBase
						{
							public static List<string> ExistingEntityNames = new List<string>();
							public static List<string> ExistingPropertyNames = new List<string>();
							public static List<string> TablePrefixes = new List<string>();
							public static List<string> ColumnPrefixes = new List<string>();
							public static List<string> TableSuffixes = new List<string>();
							public static List<string> ColumnSuffixes = new List<string>();
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