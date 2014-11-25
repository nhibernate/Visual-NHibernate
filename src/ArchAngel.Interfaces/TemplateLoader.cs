using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ArchAngel.Interfaces.ITemplate;
using ArchAngel.Interfaces.Wizards.NewProject;

namespace ArchAngel.Interfaces
{
	public class TemplateLoader : ITemplateLoader
	{
		private readonly string _ProjectNamespace;
		private readonly ScriptFunctionWrapper _ScriptRunnerInstance;
		private readonly object _TemplateGenInstance;
		private readonly Type _UserOptionsType;
		private readonly Type _VirtualPropertiesType;
		private readonly Type _LangAttributeType;
		private static string TemplateAssemblyDir = "";

		public Assembly CurrentAssembly { get; private set; }

		public TemplateLoader(Assembly assembly)
		{
			try
			{
				TemplateAssemblyDir = Path.GetDirectoryName(assembly.Location);
				AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
				AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += new ResolveEventHandler(CurrentDomain_ReflectionOnlyAssemblyResolve);

				foreach (Type type in assembly.GetTypes())
				{
					if (type.Name != "TemplateGen") continue;

					_ProjectNamespace = type.Namespace;
					_TemplateGenInstance = assembly.CreateInstance(type.FullName);
					break;
				}
			}
			catch (ReflectionTypeLoadException ex)
			{
				string message = "";

				foreach (var tle in ex.LoaderExceptions)
				{
					message += tle.Message + Environment.NewLine;
				}
				throw new Exception(message);
			}
			finally
			{
				AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;
				AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= CurrentDomain_ReflectionOnlyAssemblyResolve;
			}
			if (_TemplateGenInstance == null)
				throw new ArgumentException("Could not find a class called TemplateGen in the given template assembly");

			CurrentAssembly = assembly;
			_ScriptRunnerInstance =
				(ScriptFunctionWrapper)CurrentAssembly.CreateInstance(_ProjectNamespace + ".ScriptFunctionWrapper");

			_TemplateGenInstance = CurrentAssembly.CreateInstance(_ProjectNamespace + ".TemplateGen");
			_UserOptionsType = CurrentAssembly.GetType(_ProjectNamespace + ".TemplateGen+UserOptions");
			_VirtualPropertiesType = CurrentAssembly.GetType(_ProjectNamespace + ".VirtualProperties");

			_LangAttributeType = CurrentAssembly.GetType(_ProjectNamespace + ".LanguageAttribute");
		}

		private static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			if (args.Name.Contains("XmlSerializers"))
				return null;

			// Try looking in the folder of the assembly
			string dir = TemplateAssemblyDir;
			string filePath = Path.Combine(dir, args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll");

			if (File.Exists(filePath))
				return System.Reflection.Assembly.LoadFrom(filePath);

			// Try looking in the exe directory
			dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			filePath = Path.Combine(dir, args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll");

			if (File.Exists(filePath))
				return System.Reflection.Assembly.LoadFrom(filePath);

			throw new NotImplementedException();
		}

		static System.Reflection.Assembly CurrentDomain_ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
		{
			if (args.Name.Contains("XmlSerializers"))
				return null;

			// Try looking in the folder of the assembly
			string dir = TemplateAssemblyDir;
			string filePath = Path.Combine(dir, args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll");

			if (File.Exists(filePath))
				return System.Reflection.Assembly.ReflectionOnlyLoadFrom(filePath);

			// Try looking in the exe directory
			dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			filePath = Path.Combine(dir, args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll");

			if (File.Exists(filePath))
				return System.Reflection.Assembly.ReflectionOnlyLoadFrom(filePath);

			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets the value of the LanguageAttribute set on the given method, if it exists.
		/// </summary>
		/// <param name="functionName">The name of the function to get the Language for.</param>
		/// <returns>The value of the LanguageAttribute on the given method, or "" if the attribute doesn't exist.</returns>
		public string GetTemplateFunctionLanguage(string functionName)
		{
			MemberInfo[] potentialMethods = _TemplateGenInstance.GetType().GetMember(functionName);

			if (potentialMethods == null)
				return "";

			if (_LangAttributeType == null)
				return ""; // Unsupported file version.

			foreach (MethodInfo mi in potentialMethods)
			{
				object[] attributes = mi.GetCustomAttributes(_LangAttributeType, false);
				if (attributes != null && attributes.Length > 0)
					return attributes[0].ToString(); // The ToString method on the custom attribute has been overridden to return the language text.
			}

			return "";
		}


		/// <summary>
		/// Calls an API Extension method. Caches values indicating whether a method has an
		/// Extension method or not, so that future calls can be short-circuited.
		/// </summary>
		/// <returns>True is the API method has been extended by the template author.</returns>
		/// <param name="name">Name of the function to call.</param>
		/// <param name="result">The return value of the function call.</param>
		/// <param name="parameters">The parameters to call the function with.</param>
		public bool CallApiExtensionFunction(string name, out object result, ref object[] parameters)
		{
			return _ScriptRunnerInstance.RunApiExtensionFunction(name, out result, ref parameters);
		}

		/// <summary>Calls a function in the ArchAngel template file (*.aal).</summary>
		/// <returns>
		/// The object returned by the function. Returns null if the return-type of the
		/// function is 'void'.
		/// </returns>
		/// <param name="name">Name of function to call.</param>
		/// <param name="parameters">Array of parameters for the function.</param>
		public object CallTemplateFunction(string name, ref object[] parameters)
		{
			try
			{
				return _ScriptRunnerInstance.RunScriptFunction(name, ref parameters);
			}
			catch (Exception ex)
			{
				if (ex.Message.IndexOf("Function not handled in ScriptRunnerInstance") >= 0)
				{
					throw new Exceptions.FunctionMissingException(name, "Function not found: " + name);
				}
				throw;
			}
		}

		public object CallTemplateFunction(string name)
		{
			var parameters = new object[0];
			return CallTemplateFunction(name, ref parameters);
		}

		/// <summary>
		/// Returns an Xml document containing information on all of the functions in the TemplateGen
		/// type from the current assembly
		/// </summary>
		/// <returns></returns>
		public string GetFunctionsXml()
		{
			object obj = CurrentAssembly.CreateInstance(_ProjectNamespace + ".TemplateGen");
			Type objType = obj.GetType();

			MethodInfo[] methods = objType.GetMethods(BindingFlags.Public | BindingFlags.Static);

			System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
			System.Xml.XmlNode rootNode = doc.CreateNode(System.Xml.XmlNodeType.Element, "functions", "");

			foreach (MethodInfo method in methods)
			{
				System.Xml.XmlNode functionNode = doc.CreateNode(System.Xml.XmlNodeType.Element, "function", "");
				System.Xml.XmlAttribute attName = doc.CreateAttribute("name");
				attName.Value = method.Name;
				System.Xml.XmlAttribute attParamTypeName = doc.CreateAttribute("parametertypename");
				ParameterInfo[] parameters = method.GetParameters();

				switch (parameters.Length)
				{
					case 0:
						attParamTypeName.Value = "";
						break;
					case 1:
						attParamTypeName.Value = parameters[0].ParameterType.ToString();
						break;
					default:
						attParamTypeName.Value = parameters[0].ParameterType.ToString();
						// TODO: Determine how to handle Template vs. normal functions WRT number of parameters
						//throw new Exception("Template functions can't have more than one parameter: "+ method.Name);
						break;
				}
				functionNode.Attributes.Append(attName);
				functionNode.Attributes.Append(attParamTypeName);
				rootNode.AppendChild(functionNode);
			}
			doc.AppendChild(rootNode);
			return doc.OuterXml;
		}

		/// <summary>Sets the value of a UserOption.</summary>
		/// <param name="name">Name of the UserOption.</param>
		/// <param name="val">The value to set.</param>
		public void SetUserOption(string name, object val)
		{
			try
			{
				_UserOptionsType.InvokeMember(name, BindingFlags.SetProperty, null, null, new[] { val });
			}
			catch
			{
				// Check to see whether we are passing the expected type
				PropertyInfo property = _UserOptionsType.GetProperty(name);

				if (property != null && property.PropertyType != val.GetType())
				{
					throw new InvalidOperationException(string.Format("UserOptions.{0} has been called with a parameter of the wrong type ({2}). Expected: {1}", name, property.PropertyType.FullName, val.GetType().FullName));
				}
			}

		}

		/// <summary>Gets the value of a UserOption.</summary>
		/// <returns>Value of the UserOption.</returns>
		/// <param name="name">Name of the UserOption.</param>
		public object GetUserOption(string name)
		{
			return _UserOptionsType.InvokeMember(name, BindingFlags.GetProperty, null, null, null);
		}

		public void SetProperty(string name, object val)
		{
			Type objType = _TemplateGenInstance.GetType();
			try
			{
				objType.InvokeMember(name, BindingFlags.SetProperty, null, null, new[] { val });
			}
			catch
			{
				// Check to see whether we are passing the expected type
				PropertyInfo property = objType.GetProperty(name);

				if (property != null && property.PropertyType != val.GetType())
				{
					throw new InvalidOperationException(string.Format("SetProperty ({0}) has been called with a parameter of the wrong type. \nExpected: {1}\nIs: {2}", name, property.PropertyType.FullName, val.GetType().FullName));
				}
			}
		}

		public object GetProperty(string name)
		{
			Type objType = _TemplateGenInstance.GetType();
			return objType.InvokeMember(name, BindingFlags.GetProperty, null, null, null);
		}

		public void SetAssemblySearchPaths(List<string> searchPaths)
		{
			SetProperty("AssemblySearchPaths", searchPaths);
		}

		public List<string> GetAssemblySearchPaths()
		{
			return (List<string>)GetProperty("AssemblySearchPaths");
		}

		public string GetAssemblyVersionNumber()
		{
			object[] attributes = CurrentAssembly.GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false);
			if (attributes != null && attributes.Length > 0 && attributes[0] is AssemblyInformationalVersionAttribute)
			{
				return ((AssemblyInformationalVersionAttribute)attributes[0]).InformationalVersion;
			}

			return "0.0.0.0";
		}

		public string GetProjectInfoXml()
		{
			Type objType = _TemplateGenInstance.GetType();
			Stream s = (Stream)objType.InvokeMember("GetProjectInfoXml", BindingFlags.InvokeMethod, null, null, null);
			string docText = new StreamReader(s).ReadToEnd();
			System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
			doc.LoadXml(docText);
			return doc.OuterXml;
		}

		public object CallDefaultValueFunction(IOption option, object[] parameters)
		{
			if (option.DefaultValueIsFunction)
			{
				string function = TemplateHelper.GetDefaultValueFunction(option);
				MethodInfo method = GetMethod(option, function);
				return method.Invoke(null, parameters);
			}
			else
			{
				if (option.VarType == typeof(bool))
					return bool.Parse(option.DefaultValue);
				else
					throw new NotImplementedException("This type not handled yet: " + option.VarType.Name);
			}
		}

		private MethodInfo GetMethod(IOption option, string function)
		{
			MethodInfo method;
			if (option.IsVirtualProperty)
			{
				method = _VirtualPropertiesType.GetMethod(function);
			}
			else
			{
				method = _UserOptionsType.GetMethod(function);
			}
			return method;
		}

		public bool CallDisplayToUserFunction(IOption option, object[] parameters)
		{
			string function = TemplateHelper.GetDisplayToUserFunction(option);
			MethodInfo method = GetMethod(option, function);
			return (bool)method.Invoke(null, parameters);
		}

		public List<INewProjectScreen> CallCustomNewProjectScreensFunction()
		{
			MethodInfo method = _TemplateGenInstance.GetType().GetMethod(TemplateHelper.CustomNewProjectScreensFunctionName);

			if (method == null)
				return new List<INewProjectScreen>();

			var parameters = new object[1];
			method.Invoke(_TemplateGenInstance, parameters);

			return parameters[0] as List<INewProjectScreen> ?? new List<INewProjectScreen>();
		}

		public void SetGeneratedFileNameOnTemplate(string filename)
		{
			var parameters = new object[] { filename };
			CallTemplateFunction(TemplateHelper.SetGeneratedFileNameFunctionName, ref parameters);
		}

		public void CallPreGenerationInitialisationFunction(ProviderInfo provider, PreGenerationData data)
		{
			MethodInfo method = _TemplateGenInstance.GetType().GetMethod(TemplateHelper.PreGenerationModelProcessingFunctionName);

			if (method == null) return;

			var parameters = new object[] { provider, data };
			method.Invoke(_TemplateGenInstance, parameters);
		}
	}

	public interface ITemplateLoader
	{

		/// <summary>
		/// Gets the Assembly loaded from the current Template.
		/// </summary>
		Assembly CurrentAssembly { get; }

		/// <summary>
		/// Gets the value of the LanguageAttribute set on the given method, if it exists.
		/// </summary>
		/// <param name="functionName">The name of the function to get the Language for.</param>
		/// <returns>The value of the LanguageAttribute on the given method, or "" if the attribute doesn't exist.</returns>
		string GetTemplateFunctionLanguage(string functionName);

		/// <summary>
		/// Calls an API Extension method. Caches values indicating whether a method has an
		/// Extension method or not, so that future calls can be short-circuited.
		/// </summary>
		/// <returns>True is the API method has been extended by the template author.</returns>
		/// <param name="name">Name of the function to call.</param>
		/// <param name="result">The return value of the function call.</param>
		/// <param name="parameters">The parameters to call the function with.</param>
		bool CallApiExtensionFunction(string name, out object result, ref object[] parameters);

		/// <summary>Calls a function in the ArchANgel template file (*.aal).</summary>
		/// <returns>
		/// The object returned by the function. Returns null if the return-type of the
		/// function is 'void'.
		/// </returns>
		/// <param name="name">Name of function to call.</param>
		/// <param name="parameters">Array of parameters for the function.</param>
		object CallTemplateFunction(string name, ref object[] parameters);

		/// <summary>Calls a function in the ArchANgel template file. Uses an empty array of parameters.</summary>
		/// <returns>
		/// The object returned by the function. Returns null if the return-type of the
		/// function is 'void'.
		/// </returns>
		/// <param name="name">Name of function to call.</param>
		object CallTemplateFunction(string name);

		/// <summary>
		/// Returns an Xml document containing information on all of the functions in the TemplateGen
		/// type from the current assembly
		/// </summary>
		/// <returns></returns>
		string GetFunctionsXml();

		/// <summary>Sets the value of a UserOption.</summary>
		/// <param name="name">Name of the UserOption.</param>
		/// <param name="val">The value to set.</param>
		void SetUserOption(string name, object val);

		/// <summary>Gets the value of a UserOption.</summary>
		/// <returns>Value of the UserOption.</returns>
		/// <param name="name">Name of the UserOption.</param>
		object GetUserOption(string name);

		/// <summary>
		/// Sets the named property in the TemplateGen class.
		/// </summary>
		/// <param name="name">The name of the property to set.</param>
		/// <param name="val">The value to give the property.</param>
		void SetProperty(string name, object val);

		/// <summary>
		/// Gets the value of the named property in the TemplateGen class.
		/// </summary>
		/// <param name="name">The name of the property to get.</param>
		object GetProperty(string name);

		/// <summary>
		/// Sets the additional paths that the TemplateGen assembly will use to attempt to resolve
		/// missing assemblies from.
		/// </summary>
		/// <param name="searchPaths">The paths to look in.</param>
		void SetAssemblySearchPaths(List<string> searchPaths);

		/// <summary>
		/// Gets the additional paths that the TemplateGen assembly will use to attempt to resolve
		/// missing assemblies from.
		/// </summary>
		List<string> GetAssemblySearchPaths();

		string GetAssemblyVersionNumber();
		string GetProjectInfoXml();
		/// <summary>
		/// Calls the IOption's default value function in the compiled template.
		/// </summary>
		/// <param name="option"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		object CallDefaultValueFunction(IOption option, object[] parameters);
		/// <summary>
		/// Calls the IOption's "display to user" function in the compiled template.
		/// </summary>
		/// <param name="option"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		bool CallDisplayToUserFunction(IOption option, object[] parameters);
		/// <summary>
		/// If the template defines a function that gets the custom screens it wants displayed at the
		/// end of the new project wizard, this calls it and returns the result. Otherwise it returns an
		/// empty list. It will never return null.
		/// </summary>
		/// <returns>A list containing the custom screens to show at the end of the New Project wizard.</returns>
		List<INewProjectScreen> CallCustomNewProjectScreensFunction();

		/// <summary>
		/// If the template defines a pre-generation initialisation function, this calls it. The
		/// idea is to initialise the given provider model with whatever extra data the template
		/// needs later on. This extra data is usually computationally expensive, so should only 
		/// be calculated at the start of generation, or it requires a view of the whole model.
		/// </summary>
		/// <param name="provider">The Provider that should be initialised by the template.</param>
		/// <param name="data">Data about the current project that might be useful.</param>
		void CallPreGenerationInitialisationFunction(ProviderInfo provider, PreGenerationData data);

		/// <summary>
		/// Sets the GeneratedFileName property on the template instance. Should be set before running the any template function
		/// if that function is generating a file.
		/// </summary>
		/// <param name="filename">The absolute filename of the file that the output of the next template function will be written to.</param>
		void SetGeneratedFileNameOnTemplate(string filename);
	}
}
