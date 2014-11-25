using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using ArchAngel.Interfaces;
using ArchAngel.Providers.CodeProvider;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;
using ArchAngel.Providers.EntityModel.UI;
using FluentNHibernate.Automapping;
using log4net;
using Slyce.Common;

namespace ArchAngel.NHibernateHelper
{
	public class ProjectLoader
	{
		private IFileController fileController;
		private static NHibernateFileVerifier _verifier;
		private readonly IProgressUpdater _progress = new NullProgressUpdater();
		private readonly IUserInteractor _userInteractor = new NullUserInteractor();

		private static readonly ILog log = LogManager.GetLogger(typeof(ProjectLoader));

		private static NHibernateFileVerifier Verifier
		{
			get
			{
				if (_verifier == null)
				{
					_verifier = new NHibernateFileVerifier();
				}
				return _verifier;
			}
		}

		public ProjectLoader(IFileController fileController)
		{
			this.fileController = fileController;
		}

		public ProjectLoader(IFileController fileController, IProgressUpdater progress, IUserInteractor userInteractor)
			: this(fileController)
		{
			_progress = progress;
			_userInteractor = userInteractor;
		}

		public class LoadResult
		{
			public MappingSet MappingSet;
			public IDatabaseLoader DatabaseLoader;
			public NHConfigFile NhConfigFile;
			public CSProjFile CsProjFile;
		}

		public LoadResult LoadEntityModelFromCSProj(string csprojFilePath, NHConfigFile nhConfigFile)
		{
			_progress.SetCurrentState("Loading Entities From Visual Studio Project", ProgressState.Normal);

			EntityLoader entityLoader = new EntityLoader(new FileController());

			XmlDocument doc = new XmlDocument();
			doc.LoadXml(fileController.ReadAllText(csprojFilePath));
			CSProjFile csProjFile = new CSProjFile(doc, csprojFilePath);
			var hbmFiles = GetHBMFilesFromCSProj(csProjFile);

			if (IsFluentProject(csProjFile))
			{
				ArchAngel.Interfaces.SharedData.CurrentProject.SetUserOption("UseFluentNHibernate", true);
				string tempFluentPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Visual NHibernate" + Path.DirectorySeparatorChar + "Temp" + Path.DirectorySeparatorChar + "FluentTemp");
				var fluentHbmFiles = GetHBMFilesForFluentFromCSProj(csProjFile, tempFluentPath);
				// Combine the actual HBM files with the ones derived from FluentNH
				hbmFiles = hbmFiles.Union(fluentHbmFiles);
			}
			else
				ArchAngel.Interfaces.SharedData.CurrentProject.SetUserOption("UseFluentNHibernate", false);

			var csFiles = GetCSharpFilesFromCSProj(doc, csprojFilePath);
			var nhvFiles = GetNHVFilesFromCSProj(doc, csprojFilePath);

			//NHConfigFile nhConfigFile = GetNhConfigFile(csProjFile, fileController);

			var databaseConnector = nhConfigFile == null ? null : nhConfigFile.DatabaseConnector;

			//////// GFH
			// We need to fetch ALL tables, because HBM mappings don't include association tables, or at least it's difficult to find them.
			List<SchemaData> tablesToFetch = null;// entityLoader.GetTablesFromHbmFiles(hbmFiles);

			IDatabaseLoader loader = null;
			IDatabase database = null;

			if (databaseConnector != null)
				database = GetDatabase(databaseConnector, out loader, tablesToFetch);

			_progress.SetCurrentState("Parsing your existing Model Project", ProgressState.Normal);
			var parseResults = ParseResults.ParseCSharpFiles(csFiles);

			_progress.SetCurrentState("Loading Mapping Information From NHibernate Mapping Files", ProgressState.Normal);
			var mappingSet = entityLoader.GetEntities(hbmFiles, parseResults, database);
			entityLoader.ApplyConstraints(mappingSet, nhvFiles, parseResults);

			#region Create References

			// Get a set of all Guids for tables that we will want to create references from
			HashSet<Guid> existingTables = new HashSet<Guid>(database.Tables.Select(t => t.InternalIdentifier));

			foreach (var mappedTable in mappingSet.Mappings.Select(m => m.FromTable))
				existingTables.Add(mappedTable.InternalIdentifier);

			HashSet<Guid> processedRelationships = new HashSet<Guid>();
			foreach (var table in database.Tables)
			{
				foreach (var directedRel in table.DirectedRelationships)
				{
					var relationship = directedRel.Relationship;

					if (processedRelationships.Contains(relationship.InternalIdentifier))
						continue; // Skip relationships that have already been handled.
					if (relationship.MappedReferences().Any())
						continue; // Skip relationships that have been mapped by the user.
					if (existingTables.Contains(directedRel.ToTable.InternalIdentifier) == false)
						continue; // Skip relationships that have tables that have no mapped Entity

					if (relationship.PrimaryTable.MappedEntities().FirstOrDefault() == null ||
						relationship.ForeignTable.MappedEntities().FirstOrDefault() == null)
					{
						continue;
					}
					ArchAngel.Providers.EntityModel.Controller.MappingLayer.MappingProcessor.ProcessRelationshipInternal(mappingSet, relationship, new ArchAngel.Providers.EntityModel.Controller.MappingLayer.OneToOneEntityProcessor());
					processedRelationships.Add(relationship.InternalIdentifier);
				}
			}
			#endregion

			foreach (var entity in mappingSet.EntitySet.Entities)
				foreach (var reference in entity.References)
					if (!mappingSet.EntitySet.References.Contains(reference))
						mappingSet.EntitySet.AddReference(reference);

			LoadResult result = new LoadResult();
			result.MappingSet = mappingSet;
			result.DatabaseLoader = loader;
			result.NhConfigFile = nhConfigFile;
			result.CsProjFile = csProjFile;
			return result;
		}

		private IDatabase GetDatabase(IDatabaseConnector databaseConnector, out IDatabaseLoader databaseLoader, List<SchemaData> tablesToFetch)
		{
			databaseLoader = null;
			_progress.SetCurrentState("Loading Database From Connection String in NHibernate Config File", ProgressState.Normal);

			databaseLoader = DatabaseLoaderFacade.GetDatabaseLoader(databaseConnector);
			databaseLoader.DatabaseObjectsToFetch = tablesToFetch;

			IDatabase database = null;
			bool success = false;

			while (success == false)
			{
				try
				{
					database = databaseLoader.LoadDatabase(databaseLoader.DatabaseObjectsToFetch, null);
					new DatabaseProcessor().CreateRelationships(database);
					success = true;
				}
				catch (DatabaseLoaderException e)
				{
					var loader = _userInteractor.GetDatabaseLoader(databaseConnector);
					if (loader == null)
						throw new NHibernateConfigException("Could not load the database specified in your config file.", e);
					databaseLoader = loader;
					_progress.SetCurrentState("Loading Database From Connection Information Given", ProgressState.Normal);
				}
			}
			return database;
		}

		/// <summary>
		/// Gets whether the app.config or web.config (or other xml file) file has an nhibernate config section.
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		private static bool FileHasNHibernateConfig(XDocument xml)
		{
			bool hasNhConfig = (from p in xml.Elements("configuration").Elements("hibernate-configuration")
								select p).ToList().Count > 0;

			if (!hasNhConfig)
			{
				XNamespace ns = XNamespace.Get("urn:nhibernate-configuration-2.2");

				hasNhConfig = (from p in xml.Elements("configuration").Elements(ns + "hibernate-configuration")
							   select p).ToList().Count > 0;
			}
			return hasNhConfig;
		}

		private static bool FileHasNHibernateConfig(string filepath)
		{
			string xml = File.ReadAllText(filepath);
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(File.ReadAllText(filepath));
			bool hasNhConfig = false;

			hasNhConfig = doc.SelectSingleNode("//hibernate-configuration") != null;

			if (!hasNhConfig)
			{
				var nsmgr = new XmlNamespaceManager(doc.NameTable);
				nsmgr.AddNamespace("nh", "urn:nhibernate-configuration-2.2");
				hasNhConfig = doc.SelectSingleNode("//nh:hibernate-configuration", nsmgr) != null;
			}
			return hasNhConfig;
		}

		public static NHConfigFile GetNhConfigFile(string configFilePath)
		{
			string cfgXmlFile = null;
			bool invalidSchemaFound = false;
			NHConfigFile nhConfigFile = GetConfigFromXmlFile(ref cfgXmlFile, ref invalidSchemaFound, configFilePath);

			if (invalidSchemaFound)
				return null;

			return nhConfigFile;
		}

		public NHConfigFile GetNhConfigFile(CSProjFile csproj)
		{
			return GetNhConfigFile(csproj, fileController);
		}

		public static NHConfigFile GetNhConfigFile(CSProjFile csproj, IFileController fileController)
		{
			//if (string.IsNullOrEmpty(csprojPath) || !File.Exists(csprojPath))
			//    return null;

			NHConfigFile nhConfigFile = null;
			var configFiles = GetPossibleNHibernateConfigFilesFromCSProj(csproj, fileController);

			string cfgXmlFile = null;
			bool invalidSchemaFound = false;

			foreach (var configFilePath in configFiles)
			{
				if (File.Exists(configFilePath))
				{
					// Test if it is an NHibernate file.
					nhConfigFile = GetConfigFromXmlFile(ref cfgXmlFile, ref invalidSchemaFound, configFilePath);

					if (nhConfigFile != null)
						break;
				}
			}
			if (cfgXmlFile == null &&
				!(invalidSchemaFound || configFiles.Contains("hibernate.cfg.xml")))
			{
				// We can't find a valid config file in the project - it might exist in 
				// another project, so ask the user to locate it.
				string filepath = "";
				return null;
			}
			if (cfgXmlFile == null)
			{
				if (invalidSchemaFound)
				{
					throw new NHibernateConfigException("Your NHibernate Configuration file uses an unsupported version of the schema. We only support NHibernate Version 2.2");
				}
				if (configFiles.Contains("hibernate.cfg.xml"))
				{
					throw new NHConfigFileMissingException("Your NHibernate configuration file does not validate against the NHibernate Configuration Schema version 2.2");
				}
				System.Text.StringBuilder sb = new System.Text.StringBuilder();

				foreach (var location in configFiles)
				{
					sb.AppendLine(location);
				}
				string message = string.Format("Could not find the NHibernate configuration file in your project. Locations searched:{0}{1}", Environment.NewLine, sb);
				return null;
				//throw new NHConfigFileMissingException(message);
			}
			return nhConfigFile;
		}

		private static NHConfigFile GetConfigFromXmlFile(ref string cfgXmlFile, ref bool invalidSchemaFound, string configFilePath)
		{
			NHConfigFile nhConfigFile = null;

			using (var streamReader = new StreamReader(configFilePath))
			{
				XDocument document = XDocument.Load(streamReader);

				if (configFilePath.EndsWith(".cfg.xml", StringComparison.OrdinalIgnoreCase))
				{
					if (Verifier.IsValidConfigFile(document))
					{
						cfgXmlFile = configFilePath;
						nhConfigFile = new NHConfigFile();
						nhConfigFile.FilePath = cfgXmlFile;
						nhConfigFile.ConfigLocation = NHConfigFile.ConfigLocations.NHConfigFile;
					}
					else
					{
						var schemas = NHibernateFileVerifier.GetSchemasInFile(document).Select(x => x.Value);
						var file = schemas.FirstOrDefault(s => s.Contains("nhibernate-configuration"));
						if (file != null)
						{
							invalidSchemaFound = true;
							log.WarnFormat("Found an NHibernate Configuration file that uses an unsupported schema");
						}
						else
						{
							log.WarnFormat("Possible NHibernate configuration file \"{0}\" failed schema validation");
						}
					}
				}
				else if (FileHasNHibernateConfig(configFilePath)) //(FileHasNHibernateConfig(document))
				{
					cfgXmlFile = configFilePath;
					nhConfigFile = new NHConfigFile();
					nhConfigFile.FilePath = cfgXmlFile;

					if (configFilePath.EndsWith("web.config", StringComparison.OrdinalIgnoreCase))
						nhConfigFile.ConfigLocation = NHConfigFile.ConfigLocations.WebConfigFile;
					else if (configFilePath.EndsWith(".config", StringComparison.OrdinalIgnoreCase))
						nhConfigFile.ConfigLocation = NHConfigFile.ConfigLocations.AppConfigFile;
					else
						nhConfigFile.ConfigLocation = NHConfigFile.ConfigLocations.Other;
				}
				else
					return null;
			}
			return nhConfigFile;
		}

		public IEnumerable<string> GetCSharpFilesFromCSProj(XmlDocument doc, string csprojPath)
		{
			return GetCSharpFilesFromCSProj(new CSProjFile(doc, csprojPath), fileController);
		}

		public static IEnumerable<string> GetCSharpFilesFromCSProj(CSProjFile csproj, IFileController controller)
		{
			return csproj
				.GetFilesMarkedCompile(f => f.EndsWith(".cs", StringComparison.OrdinalIgnoreCase))
				.Select(f => controller.ToAbsolutePath(f, csproj.FilePath))
				.ToList();
		}

		public IEnumerable<string> GetHBMFilesFromCSProj(CSProjFile csproj)
		{
			return GetHBMFilesFromCSProj(csproj, fileController);
		}

		public static IEnumerable<string> GetHBMFilesFromCSProj(CSProjFile csproj, IFileController controller)
		{
			return csproj
				.GetEmbeddedResources(f => f.EndsWith(".hbm.xml", StringComparison.OrdinalIgnoreCase))
				.Select(f => controller.ToAbsolutePath(f, csproj.FilePath))
				.ToList();
		}

		public static bool IsFluentProject(string csProjFilePath)
		{
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(File.ReadAllText(csProjFilePath));
			CSProjFile csProjFile = new CSProjFile(doc, csProjFilePath);
			return IsFluentProject(csProjFile);
		}

		public static bool IsFluentProject(CSProjFile csProjFile)
		{
			var referencedFiles = csProjFile.GetReferencedAssemblies();

			foreach (string file in referencedFiles)
			{
				if (file.ToLower().EndsWith("fluentnhibernate.dll"))
					return true;
			}
			return false;
		}

		public static IEnumerable<string> GetHBMFilesForFluentFromCSProj(CSProjFile csproj, string outputPath)
		{
			string assemblyName = csproj.GetAssemblyName();
			IEnumerable<string> outputPaths = csproj.GetOutputPaths();
			DateTime latestAssemblyDate = new DateTime(1900, 1, 1);
			string latestAssembly = "";

			foreach (string folder in outputPaths)
			{
				string assemblyPath = Path.Combine(folder, assemblyName);

				if (File.Exists(assemblyPath))
				{
					DateTime lastWriteTime = File.GetLastWriteTimeUtc(assemblyPath);

					if (lastWriteTime > latestAssemblyDate)
					{
						latestAssemblyDate = lastWriteTime;
						latestAssembly = assemblyPath;
					}
				}
			}
			if (string.IsNullOrEmpty(latestAssembly))
				throw new FluentNHibernateCompiledAssemblyMissingException("No compiled assembly found. A compiled assembly is required for Fluent NHibernate. Please recompile your project.");

			System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(latestAssembly);
			FluentCompileLatestAssemblyDir = Path.GetDirectoryName(latestAssembly);

			try
			{
				assembly.ModuleResolve += new System.Reflection.ModuleResolveEventHandler(assembly_ModuleResolve);
				AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
				AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += new ResolveEventHandler(CurrentDomain_ReflectionOnlyAssemblyResolve);

				if (!Directory.Exists(outputPath))
					Directory.CreateDirectory(outputPath);

				//var sessionFactory = FluentNHibernate.Cfg.Fluently.Configure()
				//                  .Database(FluentNHibernate.Cfg.Db.MsSqlConfiguration.MsSql2008
				//                    .ConnectionString("Data Source=STN_DEV;Initial Catalog=FNHProviderDB;Integrated Security=SSPI;")
				//                  )
				//                  .Mappings(m => m.FluentMappings.AddFromAssembly(assembly).ExportTo(outputPath))
				//                  .BuildConfiguration();

				AutoPersistenceModel mappings = new AutoPersistenceModel();
				mappings.AddMappingsFromAssembly(assembly);
				mappings.BuildMappings();
				mappings.WriteMappingsTo(outputPath);

				return Directory.GetFiles(outputPath).ToList();
			}
			finally
			{
				assembly.ModuleResolve -= assembly_ModuleResolve;
				AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;
				AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= CurrentDomain_ReflectionOnlyAssemblyResolve;
			}
		}

		private static string FluentCompileLatestAssemblyDir = "";

		private static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			if (args.Name.Contains("XmlSerializers"))
				return null;

			string filePath = Path.Combine(FluentCompileLatestAssemblyDir, args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll");

			if (File.Exists(filePath))
				return System.Reflection.Assembly.LoadFrom(filePath);

			throw new NotImplementedException();
		}

		static System.Reflection.Assembly CurrentDomain_ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
		{
			if (args.Name.Contains("XmlSerializers"))
				return null;

			string filePath = Path.Combine(FluentCompileLatestAssemblyDir, args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll");

			if (File.Exists(filePath))
				return System.Reflection.Assembly.ReflectionOnlyLoadFrom(filePath);

			throw new NotImplementedException();
		}

		static System.Reflection.Module assembly_ModuleResolve(object sender, ResolveEventArgs e)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<string> GetNHVFilesFromCSProj(XmlDocument doc, string csprojPath)
		{
			return GetNHVFilesFromCSProj(new CSProjFile(doc, csprojPath), csprojPath, fileController);
		}

		public static IEnumerable<string> GetNHVFilesFromCSProj(CSProjFile csproj, string csprojPath, IFileController controller)
		{
			return csproj
				.GetEmbeddedResources(f => f.EndsWith(".nhv.xml", StringComparison.OrdinalIgnoreCase))
				.Select(f => controller.ToAbsolutePath(f, csprojPath))
				.ToList();
		}

		public IEnumerable<string> GetPossibleNHibernateConfigFilesFromCSProj(CSProjFile csproj)
		{
			return GetPossibleNHibernateConfigFilesFromCSProj(csproj, fileController);
		}

		public static IEnumerable<string> GetPossibleNHibernateConfigFilesFromCSProj(CSProjFile csproj, IFileController controller)
		{
			return csproj
				.GetEmbeddedResources(f => f.EndsWith(".cfg.xml", StringComparison.OrdinalIgnoreCase))
				.Concat(csproj.GetFilesMarkedNone(f => f.EndsWith(".cfg.xml", StringComparison.OrdinalIgnoreCase)))
				.Concat(csproj.GetContentFiles(f => f.EndsWith(".cfg.xml", StringComparison.OrdinalIgnoreCase)))
				.Concat(csproj.GetResourceFiles(f => f.EndsWith(".cfg.xml", StringComparison.OrdinalIgnoreCase)))
				.Concat(csproj.GetFilesMarkedNone(f => f.EndsWith(".config", StringComparison.OrdinalIgnoreCase)))
				.Select(f => controller.ToAbsolutePath(f, csproj.FilePath))
				.ToList();
		}
	}

	public class NHibernateConfigException : Exception
	{
		public NHibernateConfigException(string message)
			: base(message)
		{
		}

		public NHibernateConfigException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}

	public class NHConfigFileMissingException : Exception
	{
		public NHConfigFileMissingException(string message)
			: base(message)
		{
		}
	}

	public class FluentNHibernateCompiledAssemblyMissingException : Exception
	{
		public FluentNHibernateCompiledAssemblyMissingException(string message)
			: base(message)
		{
		}
	}
}
