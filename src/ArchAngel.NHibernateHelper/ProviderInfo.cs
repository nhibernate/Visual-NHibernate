using System;
using System.Collections.Generic;
using System.Linq;
using ArchAngel.Interfaces;

namespace ArchAngel.NHibernateHelper
{
	[DotfuscatorDoNotRename]
	public class ProviderInfo : ArchAngel.Interfaces.ProviderInfo, ArchAngel.Providers.EntityModel.Controller.Validation.IReturnValdationRules
	{
		public enum ProjectType
		{
			Blank,
			FromExistingDatabase,
			FromExistingVisualStudioProject
		}
		private ProjectType _ProjectOrigin = ProjectType.Blank;
		private NHConfigFile _NhConfigFile;
		private Slyce.Common.CSProjFile _CsProjFile;
		private static readonly Dictionary<string, Type> cachedTypes = new Dictionary<string, Type>();

		[DotfuscatorDoNotRename]
		public ProviderInfo()
		{
			Name = "NHibernate Provider";
			Description = "A Provider that allows access to certain NHibernate-specific settings.";

			NhConfigFile = new NHConfigFile();
		}

		public override void InitialisePreGeneration(ArchAngel.Interfaces.PreGenerationData data)
		{
			ArchAngel.NHibernateHelper.NHibernateProjectPreprocessor preprocessor = new ArchAngel.NHibernateHelper.NHibernateProjectPreprocessor(new Slyce.Common.FileController());
			preprocessor.InitialiseNHibernateProject(this, data);

			foreach (var providerInfo in data.OtherProviderInfos)
			{
				if (providerInfo is ArchAngel.Providers.EntityModel.ProviderInfo)
				{
					ArchAngel.NHibernateHelper.EntityModelPreprocessor entityModelPreprocessor = new ArchAngel.NHibernateHelper.EntityModelPreprocessor(new Slyce.Common.FileController());
					entityModelPreprocessor.InitialiseEntityModel((ArchAngel.Providers.EntityModel.ProviderInfo)providerInfo, data);
				}
			}
		}

		public override void InitialiseScriptObjects(PreGenerationData data)
		{
			string outputFolder = null;
			string tempFolder = null;

			if (ArchAngel.Interfaces.SharedData.CurrentProject.ScriptProject != null)
			{
				outputFolder = ArchAngel.Interfaces.SharedData.CurrentProject.ScriptProject.OutputFolder;
				tempFolder = ArchAngel.Interfaces.SharedData.CurrentProject.ScriptProject.TempFolder;
			}
			//this.NhConfigFile.ProviderInfo = this;
			InitialisePreGeneration(data);
			ArchAngel.Interfaces.SharedData.CurrentProject.ScriptProject = ArchAngel.NHibernateHelper.NHibernateProjectPreprocessor.FillScriptModel(this.EntityProviderInfo.MappingSet, null, this.NhConfigFile);
			ArchAngel.Interfaces.SharedData.CurrentProject.ScriptProject.OutputFolder = outputFolder;
			ArchAngel.Interfaces.SharedData.CurrentProject.ScriptProject.TempFolder = tempFolder;
		}

		/// <summary>
		/// Gets a collection of objects that will be presented to the ArchAngel Designer user to select from
		/// when previewing a function. The object stack will be walked.
		/// </summary>
		public override IEnumerable<object> RootPreviewObjects
		{
			get { return null; }
		}

		[DotfuscatorDoNotRename]
		protected void SaveVirtualPropertyData(IEnumerable<IScriptBaseObject> scriptObjects)
		{

		}

		public ArchAngel.Providers.EntityModel.ProviderInfo EntityProviderInfo
		{
			get
			{
				foreach (ArchAngel.Interfaces.ProviderInfo providerInfo in OtherProviders)
				{
					if (providerInfo is ArchAngel.Providers.EntityModel.ProviderInfo)
						return (ArchAngel.Providers.EntityModel.ProviderInfo)providerInfo;
				}
				return null;
			}
		}

		/// <summary>
		/// Returns all objects of the specified type that currently exist in this provider. This typically gets called
		/// when analysis and generation begins and the objects get passed to the template to create the output files.
		/// </summary>
		/// <param name="typeName">Fully qualified name of the type of objects to return.</param>
		/// <returns>An array of objects of the specified type.</returns>
		public override IEnumerable<IScriptBaseObject> GetAllObjectsOfType(string typeName)
		{
			Type type = GetTypeFromTypeName(typeName);
			return GetAllObjectsOfType(type).ToArray();
		}

		public IEnumerable<IScriptBaseObject> GetAllObjectsOfType(Type type)
		{
			if (type == typeof(NHConfigFile))
				return new[] { NhConfigFile };

			throw new NotImplementedException("We do not currently handle " + type.FullName + " objects in GetAllObjectsOfType(Type type)");
		}

		/// <summary>
		/// Returns all objects of the specified type that are valid 'beneath' the supplied rootObject. This typically gets called
		/// when analysis and generation begins and the objects get passed to the template to create the output files.
		/// </summary>
		/// <param name="typeName">Fully qualified name of the type of objects to return.</param>
		/// <param name="rootObject">Object by which the results must get filtered.</param>
		/// <returns>An arra of objects of the specified type.</returns>
		public override IEnumerable<IScriptBaseObject> GetAllObjectsOfType(string typeName, IScriptBaseObject rootObject)
		{
			throw new NotImplementedException("We do not currently handle " + rootObject.GetType().FullName + " types as root objects or " + typeName + " as objects to get types of.");
		}

		public NHConfigFile NhConfigFile
		{
			get
			{
				return _NhConfigFile;
			}
			set
			{
				_NhConfigFile = value;

				if (_NhConfigFile == null)
				{
					_NhConfigFile = new NHConfigFile();
				}
				//_NhConfigFile.EntityProviderInfo = EntityProviderInfo;
				_NhConfigFile.ProviderInfo = this;
			}
		}

		public Slyce.Common.CSProjFile CsProjFile
		{
			get
			{
				return _CsProjFile;
			}
			set
			{
				_CsProjFile = value;
			}
		}

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
		public override void CreateScreens()
		{
			EntityProviderInfo.CreateScreens();
		}

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
		public override void Save(string folder)
		{
		}

		/// <summary>
		/// Reads the page/object(s) state from file eg: xml, binary serialization etc
		/// </summary>
		/// <param name="folder">Folder to open files from.</param>
		[DotfuscatorDoNotRename]
		public override void Open(string folder)
		{
		}

		/// <summary>
		/// Clears all data from the provider. Gets called when opening projects or creating new projects.
		/// </summary>
		[DotfuscatorDoNotRename]
		public override void Clear()
		{
			_NhConfigFile = new NHConfigFile();
		}

		/// <summary>
		/// Returns whether this provider is in a valid state. This typically gets called just before generation.
		/// If false is returned, analysis and generation will not proceed.
		/// </summary>
		/// <param name="failReason">The reason for the invalid state.</param>
		/// <returns>True if the provider's state is valid, false otherwise.</returns>
		[DotfuscatorDoNotRename]
		public override bool IsValid(out string failReason)
		{
			failReason = "";
			return true;
		}

		/// <summary>
		/// Gets called by ArchAngel just before Analysis begins, allowing you to perform any last second actions. This is a synchronous operation.
		/// </summary>
		[DotfuscatorDoNotRename]
		public void PerformPreAnalysisActions()
		{
		}

		public ProjectType ProjectOrigin
		{
			get { return _ProjectOrigin; }
			private set { _ProjectOrigin = value; }
		}

		/// <summary>
		/// If info is non null and applies to this Provider, clears the Provider data and loads from the information given.
		/// </summary>
		/// <param name="info">The information to use to load the provider. If null, nothing happens.</param>
		[DotfuscatorDoNotRename]
		public override void LoadFromNewProjectInformation(INewProjectInformation info)
		{
			if (info == null ||
				(!(info is LoadProjectWizard.LoadExistingNHibernateProjectInfo) &&
				!(info is LoadProjectWizard.LoadExistingDatabaseInfo)))
			{
				return;
			}
			ArchAngel.Providers.EntityModel.UI.IUserInteractor userInteractor = EntityProviderInfo.UserInteractor;

			if (info is LoadProjectWizard.LoadExistingDatabaseInfo)
			{
				ProjectOrigin = ProjectType.FromExistingDatabase;
				var newProjectInfo = (LoadProjectWizard.LoadExistingDatabaseInfo)info;
				newProjectInfo.RunCustomNewProjectLogic(this, userInteractor);
			}
			else if (info is LoadProjectWizard.LoadExistingNHibernateProjectInfo)
			{
				ProjectOrigin = ProjectType.FromExistingVisualStudioProject;
				var newProjectInfo = (LoadProjectWizard.LoadExistingNHibernateProjectInfo)info;
				newProjectInfo.RunCustomNewProjectLogic(this, userInteractor);
			}
			else
			{
				throw new NotImplementedException("Not handled yet.");
			}
			//EntityProviderInfo.LoadFromNewProjectInformation(info);
		}

		/// <summary>
		/// Validates that the model is in a valid state for generating from. 
		/// </summary>
		/// <returns>A ValidationResult that reports whether validation passed or not, and what provider screen should be shown on failure.</returns>
		[DotfuscatorDoNotRename]
		public override ValidationResult RunPreGenerationValidation()
		{
			//IEnumerable<ArchAngel.Interfaces.IScriptBaseObject> scriptObjects = EntityProviderInfo.GetAllObjectsOfType(typeof(ArchAngel.Interfaces.IScriptBaseObject));
			//List<ArchAngel.Providers.EntityModel.Model.DatabaseLayer.Database> databases = new List<ArchAngel.Providers.EntityModel.Model.DatabaseLayer.Database>();

			//foreach (var scriptObject in scriptObjects)
			//{
			//     ArchAngel.Providers.EntityModel.Model.DatabaseLayer.Database database = (ArchAngel.Providers.EntityModel.Model.DatabaseLayer.Database)scriptObject;
			//    EntityProviderInfo.Engine.AddModule(new Validation.NHibernateProjectLoaderModule(database));
			//}

			return new ValidationResult();
		}

		public IList<ArchAngel.Providers.EntityModel.Controller.Validation.IValidationRule> ValidationRules
		{
			get
			{
				List<ArchAngel.Providers.EntityModel.Controller.Validation.IValidationRule> rules = new List<ArchAngel.Providers.EntityModel.Controller.Validation.IValidationRule>();
				IEnumerable<ArchAngel.Interfaces.IScriptBaseObject> scriptObjects = EntityProviderInfo.GetAllObjectsOfType(typeof(ArchAngel.Providers.EntityModel.Model.DatabaseLayer.Database));

				foreach (var scriptObject in scriptObjects)
				{
					ArchAngel.Providers.EntityModel.Model.DatabaseLayer.Database database = (ArchAngel.Providers.EntityModel.Model.DatabaseLayer.Database)scriptObject;
					rules.Add(new Validation.CheckSchemaAgainstRealDatabaseRule(database));
				}
				rules.Add(new Validation.CheckReferenceCollectionTypeRule());
				rules.Add(new Validation.CheckLazyPropertiesHaveLazyEntityRule());
				return rules;
			}
		}

		private Type GetTypeFromTypeName(string name)
		{
			lock (cachedTypes)
			{
				if (cachedTypes.ContainsKey(name))
					return cachedTypes[name];

				Type type = GetType().Assembly.GetType(name);
				cachedTypes[name] = type;
				return type;
			}
		}
	}
}
