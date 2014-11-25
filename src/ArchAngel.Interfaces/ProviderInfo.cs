using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using ArchAngel.Interfaces.Controls.ContentItems;

namespace ArchAngel.Interfaces
{
	[DotfuscatorDoNotRename]
	public abstract class ProviderInfo
	{
		[DotfuscatorDoNotRename]
		private string _Name = "";
		[DotfuscatorDoNotRename]
		private string _Description = "";
		[DotfuscatorDoNotRename]
		private Controls.ContentItems.ContentItem[] _Screens = new Controls.ContentItems.ContentItem[0];
		[DotfuscatorDoNotRename]
		public Assembly Assembly;
		[DotfuscatorDoNotRename]
		private static readonly Dictionary<string, Setting> _Settings = new Dictionary<string, Setting>();
		[DotfuscatorDoNotRename]
		private readonly List<IOptionForm> _OptionForms = new List<IOptionForm>();
		[DotfuscatorDoNotRename]
		private static readonly Dictionary<Type, DisplayNames> _PreviewDisplayProperties = new Dictionary<Type, DisplayNames>();
		private static string TemplateAssemblyDir = "";

		private class DisplayNames
		{
			public readonly PropertyInfo Property;
			public readonly string DefaultString;

			public DisplayNames(PropertyInfo property, string defaultString)
			{
				Property = property;
				DefaultString = defaultString;
			}
		}

		[DotfuscatorDoNotRename]
		protected ProviderInfo()
		{
		}

		public abstract void InitialisePreGeneration(ArchAngel.Interfaces.PreGenerationData data);

		public abstract void InitialiseScriptObjects(ArchAngel.Interfaces.PreGenerationData data);

		public List<ProviderInfo> OtherProviders { get; set; }

		[DotfuscatorDoNotRename]
		public string Name
		{
			get { return _Name; }
			protected set { _Name = value; }
		}

		[DotfuscatorDoNotRename]
		public static Dictionary<string, Setting> Settings
		{
			get { return _Settings; }
		}

		[DotfuscatorDoNotRename]
		public List<IOptionForm> OptionForms
		{
			get { return _OptionForms; }
		}

		/// <summary>
		/// Gets a collection of objects that will be presented to the ArchAngel Designer user to select from
		/// when previewing a function. The object stack will be walked.
		/// </summary>
		public abstract IEnumerable<object> RootPreviewObjects { get; }

		[DotfuscatorDoNotRename]
		public string Description
		{
			get { return _Description; }
			set { _Description = value; }
		}

		public static bool IsProvider(Assembly assembly)
		{
			Type dummy;
			return IsProvider(assembly, out dummy);
		}

		public static ProviderInfo GetProviderInfo(Assembly assembly)
		{
			Type providerInfoType;

			if (IsProvider(assembly, out providerInfoType))
			{
				object obj = Activator.CreateInstance(providerInfoType);
				ProviderInfo provider = (ProviderInfo)obj;
				provider.Assembly = assembly;
				return provider;
			}
			return null;
		}

		public static string GetDisplayName(object obj)
		{
			Type type = obj.GetType();

			if (!_PreviewDisplayProperties.ContainsKey(type))
			{
				object[] allAttributes = type.GetCustomAttributes(false);
				bool hasEditorAtt = false;

				foreach (object att in allAttributes)
				{
					Type attType = att.GetType();

					if (!Slyce.Common.Utility.StringsAreEqual(attType.Name, "ArchAngelEditorAttribute", true)) continue;

					hasEditorAtt = true;
					string displayName = (string)attType.InvokeMember("PreviewDisplayName", BindingFlags.GetProperty, null, att, null);
					string displayProperty = (string)attType.InvokeMember("PreviewDisplayProperty", BindingFlags.GetProperty, null, att, null);

					if (!string.IsNullOrEmpty(displayName))
					{
						_PreviewDisplayProperties.Add(type, new DisplayNames(null, displayName));
					}
					else if (!string.IsNullOrEmpty(displayProperty))
					{
						PropertyInfo propInfo = type.GetProperty(displayProperty);
						if (propInfo == null)
						{
							throw new Exception(string.Format("The object does not contain the property [{1}] defined in the DisplayProperty of the APIExtension attribute of the class [{0}].", type.FullName, displayProperty));
						}
						_PreviewDisplayProperties.Add(type, new DisplayNames(propInfo, null));
					}

					break;
				}
				if (!hasEditorAtt)
				{
					string previewPropertyName = obj.ToString();
					_PreviewDisplayProperties.Add(type, new DisplayNames(null, previewPropertyName));
				}
			}

			DisplayNames displayNames;

			if (_PreviewDisplayProperties.ContainsKey(type))
				displayNames = _PreviewDisplayProperties[type];
			else
				return obj.ToString();

			if (displayNames.Property == null)
			{
				return displayNames.DefaultString;
			}

			return displayNames.Property.GetValue(obj, null).ToString();
		}

		public static bool IsProvider(Assembly assembly, out Type providerInfoType)
		{
			providerInfoType = null;
			Type baseType = typeof(ProviderInfo);
			string baseTypeName = baseType.FullName;

			try
			{
				if (!string.IsNullOrEmpty(assembly.Location))
					TemplateAssemblyDir = Path.GetDirectoryName(assembly.Location);
				else
					TemplateAssemblyDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase.Replace(@"file:///", ""));

				AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
				AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += new ResolveEventHandler(CurrentDomain_ReflectionOnlyAssemblyResolve);

				foreach (Type typeX in assembly.GetTypes())
				{
					//if (typeX.IsSubclassOf(baseType))
					if (typeX.BaseType != null && typeX.BaseType.FullName == baseTypeName)
					{
						providerInfoType = typeX;
						return true;
					}
				}
			}
			catch (ReflectionTypeLoadException ex)
			{
				AssemblyName[] referencedAssemblies = assembly.GetReferencedAssemblies();
				string aaInterfacesFilename = Path.Combine(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), "ArchAngel.Interfaces.dll");
				string aaDatabaseProviderFilename = Path.Combine(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), "ArchAngel.Providers.Database.dll");
				StringBuilder sbWrongFileVersionMessage = new StringBuilder();

				foreach (AssemblyName referencedAssembly in referencedAssemblies)
				{
					if (Slyce.Common.Utility.StringsAreEqual(referencedAssembly.Name, "ArchAngel.Interfaces", false) &&
						File.Exists(aaInterfacesFilename))
					{
						System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(aaInterfacesFilename);

						if (fvi.FileVersion != referencedAssembly.Version.ToString())
						{
							sbWrongFileVersionMessage.AppendLine(string.Format("ArchAngel.Interfaces.dll {0} instead of {1}.", referencedAssembly.Version, fvi.FileVersion));
						}
					}
					if (Slyce.Common.Utility.StringsAreEqual(referencedAssembly.Name, "ArchAngel.Providers.Database", false) &&
						File.Exists(aaInterfacesFilename))
					{
						System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(aaDatabaseProviderFilename);

						if (fvi.FileVersion != referencedAssembly.Version.ToString())
						{
							sbWrongFileVersionMessage.AppendLine(string.Format("ArchAngel.Providers.Database.dll {0} instead of {1}.", referencedAssembly.Version, fvi.FileVersion));
						}
					}
				}
				if (sbWrongFileVersionMessage.Length > 0)
				{
					System.Windows.Forms.MessageBox.Show(string.Format("Wrong version of ArchAngel file is being used by {0}. {1}An old version probably exists with the other referenced files: {1}{1}{2}", assembly.GetName().Name, Environment.NewLine, sbWrongFileVersionMessage), "Incorrect ArchAngel File Version", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
				}
				List<string> messages = new List<string>();

				foreach (Exception ex2 in ex.LoaderExceptions)
				{
					if (messages.BinarySearch(ex2.Message) < 0)
					{
						messages.Add(ex2.Message);
						messages.Sort();
					}
				}
				StringBuilder sb = new StringBuilder(100);

				foreach (string message in messages)
				{
					sb.AppendLine("** " + message);
				}
				throw new Exception(string.Format("Errors while loading assemblies:{0}{1}", Environment.NewLine, sb));
			}
			finally
			{
				AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;
				AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= CurrentDomain_ReflectionOnlyAssemblyResolve;
			}
			return false;
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

		[DotfuscatorDoNotRename]
		protected void SaveVirtualPropertyData(IEnumerable<IScriptBaseObject> scriptObjects)
		{

		}

		[DotfuscatorDoNotRename]
		public Controls.ContentItems.ContentItem[] Screens
		{
			get { return _Screens; }
			protected set { _Screens = value; }
		}

		[DotfuscatorDoNotRename]
		public string FileName = "";

		/// <summary>
		/// Instructs the Provider to create the ContentItem Screens. This is so that the creation
		/// of UI elements can be deferred until it needs to happen, and our Unit Tests can run
		/// without spinning up forms in the background for no reason.
		/// 
		/// To Implementors:
		/// Your implementation should be able to be called multiple times. The first time, create the
		/// screens. Subsequently it should do nothing. 
		/// 
		/// Also, you should not rely on this being called before any of your other methods. It will 
		/// probably happen like that in the Workbench, but our debugger will not be running this method
		/// at all.
		/// 
		/// If you really need your UI objects to be created, then leave that code in the constructor 
		/// and leave this method blank. You will cause extra memory to be used in the Workbench Debugger,
		/// and potentially cause other problems. For instance, the Debugger runs in Multi Threaded Apartment
		/// mode, and WinForms objects cannot be created in this mode. The debugger cannot be changed to avoid
		/// this, we have already tried.
		/// </summary>
		[DotfuscatorDoNotRename]
		public abstract void CreateScreens();

		/// <summary>
		/// Saves this provider's state to a single file or multiple files in the specified folder.
		/// </summary>
		/// <remarks>
		/// For Provider Authors:
		/// You can save the provider's data in any format you like, such as xml, binary 
		/// serialization etc. What files you create and what you put inside them is totally up to you.
		/// </remarks>
		/// <param name="folder">Folder to save the files to.</param>
		[DotfuscatorDoNotRename]
		public abstract void Save(string folder);

		/// <summary>
		/// Reads the page/object(s) state from file eg: xml, binary serialization etc
		/// </summary>
		/// <param name="folder">Folder to open files from.</param>
		[DotfuscatorDoNotRename]
		public abstract void Open(string folder);

		/// <summary>
		/// Clears all data from the provider. Gets called when opening projects or creating new projects.
		/// </summary>
		[DotfuscatorDoNotRename]
		public abstract void Clear();

		/// <summary>
		/// Returns whether this provider is in a valid state. This typically gets called just before generation.
		/// If false is returned, analysis and generation will not proceed.
		/// </summary>
		/// <param name="failReason">The reason for the invalid state.</param>
		/// <returns>True if the provider's state is valid, false otherwise.</returns>
		[DotfuscatorDoNotRename]
		public abstract bool IsValid(out string failReason);

		/// <summary>
		/// Gets called by ArchAngel just before Analysis begins, allowing you to perform any last second actions. This is a synchronous operation.
		/// </summary>
		[DotfuscatorDoNotRename]
		public virtual void PerformPreAnalysisActions()
		{
		}

		/// <summary>
		/// Returns all objects of the specified type that currently exist in this provider. This typically gets called
		/// when analysis and generation begins and the objects get passed to the template to create the output files.
		/// </summary>
		/// <param name="typeName">Fully qualified name of the type of objects to return.</param>
		/// <returns>An array of objects of the specified type.</returns>
		[DotfuscatorDoNotRename]
		public abstract IEnumerable<IScriptBaseObject> GetAllObjectsOfType(string typeName);

		/// <summary>
		/// Returns all objects of the specified type that are valid 'beneath' the supplied rootObject. This typically gets called
		/// when analysis and generation begins and the objects get passed to the template to create the output files.
		/// </summary>
		/// <param name="typeName">Fully qualified name of the type of objects to return.</param>
		/// <param name="rootObject">Object by which the results must get filtered.</param>
		/// <returns>An arra of objects of the specified type.</returns>
		[DotfuscatorDoNotRename]
		public abstract IEnumerable<IScriptBaseObject> GetAllObjectsOfType(string typeName, IScriptBaseObject rootObject);

		/// <summary>
		/// If info is non null and applies to this Provider, clears the Provider data and loads from the information given.
		/// </summary>
		/// <param name="info">The information to use to load the provider. If null, nothing happens.</param>
		[DotfuscatorDoNotRename]
		public abstract void LoadFromNewProjectInformation(INewProjectInformation info);

		/// <summary>
		/// Validates that the model is in a valid state for generating from. 
		/// </summary>
		/// <returns>A ValidationResult that reports whether validation passed or not, and what provider screen should be shown on failure.</returns>
		[DotfuscatorDoNotRename]
		public abstract ValidationResult RunPreGenerationValidation();
	}

	public class ValidationResult
	{
		/// <summary>
		/// If true, the model did not validate and ScreenToShow must be displayed to show the user the error.
		/// </summary>
		public bool ValidationFailed { get; private set; }
		public ContentItem ScreenToShow { get; private set; }

		public ValidationResult()
		{
			ValidationFailed = false;
		}

		public ValidationResult(bool validationFailed, ContentItem screenToShow)
		{
			ValidationFailed = validationFailed;
			ScreenToShow = screenToShow;
		}
	}



	public interface INewProjectInformation
	{
		/// <summary>
		/// The Type of the ProviderInfo this information belongs to.
		/// </summary>
		Type ValidProviderType { get; }
		string OutputPath { get; set; }
	}
}
