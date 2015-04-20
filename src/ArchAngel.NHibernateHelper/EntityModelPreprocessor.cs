using System.IO;
using System.Linq;
using System.Xml;
using ArchAngel.Interfaces;
using ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.ExtensionMethods;
using ArchAngel.Providers.CodeProvider;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using log4net;
using Slyce.Common;
using Slyce.Common.ExtensionMethods;
using Slyce.Common.IEnumerableExtensions;

namespace ArchAngel.NHibernateHelper
{
	public class EntityModelPreprocessor
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(EntityModelPreprocessor));

		private readonly IFileController fileController;

		public EntityModelPreprocessor(IFileController fileController)
		{
			this.fileController = fileController;
		}

		public void InitialiseEntityModel(ArchAngel.Providers.EntityModel.ProviderInfo providerInfo, PreGenerationData data)
		{
			providerInfo.MappingSet.CodeParseResults = null;
			// Clear the current mapped class.
			providerInfo.MappingSet.EntitySet.Entities.ForEach(e => e.MappedClass = null);

			// Find the csproj file we are going to use
			string filename;
			var csprojDocument = GetCSProjDocument(data, out filename);

			if (csprojDocument == null)
				return;

			CSProjFile csproj = new CSProjFile(csprojDocument, filename);
			var hbmFiles = ProjectLoader.GetHBMFilesFromCSProj(csproj, fileController);

			// Load HBMs
			foreach (string hbmFilePath in hbmFiles)
			{
				if (!File.Exists(hbmFilePath))
					throw new FileNotFoundException(string.Format("A file is defined is your csproj file [{0}], but it cannot be found: [{1}]", filename, hbmFilePath), hbmFilePath);
			}
			var mappings = hbmFiles.Select(f => MappingFiles.Version_2_2.Utility.Open(f)).ToList();

			// Parse the CSharp files
			var csharpFiles = ProjectLoader.GetCSharpFilesFromCSProj(csproj, fileController);
			var parseResults = ParseResults.ParseCSharpFiles(csharpFiles);

			providerInfo.MappingSet.CodeParseResults = parseResults;

			// Clear the current mapped class.
			providerInfo.MappingSet.EntitySet.Entities.ForEach(e => e.MappedClass = null);

			// Map the Entity objects to the parsed Class
			var entities = providerInfo.MappingSet.EntitySet.Entities.ToDictionary(e => e.Name);

			foreach (var hm in mappings)
			{
				foreach (var hClass in hm.Classes())
				{
					var fullClassName = HibernateMappingHelper.ResolveFullClassName(hClass, hm);
					var shortClassName = HibernateMappingHelper.ResolveShortClassName(hClass, hm);

					// try find the entity
					Entity entity;
					if (entities.TryGetValue(shortClassName, out entity))
					{
						// try find class in parse results
						var parsedClass = parseResults.FindClass(fullClassName, entity.Properties.Select(p => p.Name).ToList());
						entity.MappedClass = parsedClass;
					}
					else
					{
						Log.InfoFormat("Could not find entity for class named {0} in the NHibernate project on disk.", shortClassName);
					}
				}
			}
			// Now, try to map entities that haven't been found yet
			foreach (var entity in entities.Select(v => v.Value).Where(e => e.MappedClass == null))
			{
				string entityName = entity.Name;
				// try find class in parse results
				var parsedClass = parseResults.FindClass(entityName, entity.Properties.Select(p => p.Name).ToList());
				entity.MappedClass = parsedClass;
			}
		}

		private XmlDocument GetCSProjDocument(PreGenerationData data, out string filename)
		{
			var doc = new XmlDocument();

			var projectName = data.UserOptions.GetValueOrDefault("ProjectName") as string;
			if (string.IsNullOrEmpty(projectName) == false)
			{
				// Search for the project as named by the ProjectName User Option.
				filename = Path.Combine(data.OutputFolder, projectName + ".csproj");

				if (fileController.FileExists(filename))
				{
					doc.LoadXml(fileController.ReadAllText(filename));
					return doc;
				}
			}

			// If we get to this point, we couldn't find the project in the default location,
			// so we search for the first project that has any *.hbm.xml files in it.
			var csProjFilenames = fileController.FindAllFilesLike(data.OutputFolder, "*.csproj", SearchOption.AllDirectories);

			foreach (var csprojFilename in csProjFilenames)
			{
				doc = new XmlDocument();
				doc.LoadXml(fileController.ReadAllText(csprojFilename));
				var hbmFiles = ProjectLoader.GetHBMFilesFromCSProj(new CSProjFile(doc, csprojFilename), fileController);

				if (hbmFiles.Any())
				{
					filename = csprojFilename;
					return doc;
				}
			}

			filename = "";
			return null;
		}
	}
}