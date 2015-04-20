using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Win32;

namespace ArchAngel.Interfaces
{
	public class SharedData
	{
		public static event EventHandler AboutToSave;
		public static IWorkbenchProject CurrentProject;
		private static string _TemplateFileName;
		private static string _ProjectPath;
		/// <summary>
		/// The folder where setting files can be found.
		/// </summary>
		private static string _ProjectSettingsFolder;
		private static bool _IsBusyGenerating = false;
		public static Hashtable Settings = new Hashtable(50);
		private const string REGISTRY_KEY = @"Software\ArchAngel";
		public static Dictionary<string, ITemplate.IDefaultValueFunction> CustomFunctions = new Dictionary<string, ITemplate.IDefaultValueFunction>();
		private static readonly List<string> _AssemblySearchPaths = new List<string>();

		public static bool IsBusyGenerating
		{
			get { return _IsBusyGenerating; }
			set { _IsBusyGenerating = value; }
		}

		public static void RaiseAboutToSave()
		{
			if (AboutToSave != null)
				AboutToSave(null, null);
		}

		public static ReadOnlyCollection<string> AssemblySearchPaths
		{
			get { return _AssemblySearchPaths.AsReadOnly(); }
		}

		public static void AddAssemblySearchPath(string path)
		{
			if (_AssemblySearchPaths.Contains(path) == false)
				_AssemblySearchPaths.Add(path);
		}

		public static void AddAssemblySearchPaths(IEnumerable<string> paths)
		{
			foreach (var sp in paths)
				AddAssemblySearchPath(sp);
		}

		public static void ClearAssemblySearchPaths()
		{
			_AssemblySearchPaths.Clear();
		}

		public static string TemplateFileName
		{
			get { return _TemplateFileName; }
			set { _TemplateFileName = value; }
		}

		public static string ProjectSettingsFolder
		{
			get { return _ProjectSettingsFolder; }
			set { _ProjectSettingsFolder = value; }
		}

		public static string ProjectPath
		{
			get { return _ProjectPath; }
			set { _ProjectPath = value; }
		}

		public static string ActiveProjectPath
		{
			get { return RegistryGetValue("_activeProjectPath"); }
			set { RegistryUpdateValue("_activeProjectPath", value); }
		}

		public static string RegistryGetValue(string name)
		{
			if (Settings[name] != null)
			{
				return (string)Settings[name];
			}
			RegistryKey key = Registry.CurrentUser.OpenSubKey(REGISTRY_KEY);
			if (key == null)
			{
				key = Registry.CurrentUser.CreateSubKey(REGISTRY_KEY);
				Settings[name] = null;
				return null;
			}
			else
			{
				Settings[name] = key.GetValue(name);
				return (string)key.GetValue(name);
			}
		}

		public static void RegistryUpdateValue(string name, string value)
		{
			// Save last config file
			if (value != null)
			{
				if (Settings[name] != null && Settings[name].ToString() == value)
				{
					// Value hasn't changed, so don't bother updating the file
					return;
				}
				Settings[name] = value;
				RegistryKey key = Registry.CurrentUser.OpenSubKey(REGISTRY_KEY, true);

				if (key != null)
				{
					key.SetValue(name, value);
				}
			}
		}

		/// <summary>
		/// Gets the default value from the function that has been specified as the DefaultValueFunction.
		/// </summary>
		/// <param name="functionName"></param>
		/// <param name="iteratorObject"></param>
		/// <returns></returns>
		public static object GetDefaultOptionValueFromFunction(string functionName, object iteratorObject)
		{
			try
			{
				object[] parameters = new[] { iteratorObject };
				return CurrentProject.CallTemplateFunction(functionName, ref parameters);
			}
			catch (MissingMethodException)
			{
				object[] parameters = new object[0];
				return CurrentProject.CallTemplateFunction(functionName, ref parameters);
			}
		}

		/// <summary>
		/// Save the current app.config file to the specified location.
		/// </summary>
		/// <param name="filename"></param>
		public static void SaveCurrentConfig(string filename)
		{
			Slyce.Common.ConfigurationUtility.SaveCurrentConfig(filename);
		}
	}
}
