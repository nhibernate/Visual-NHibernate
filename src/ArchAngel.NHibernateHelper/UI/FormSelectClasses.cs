using System;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.ExtensionMethods;
using ArchAngel.Providers.CodeProvider;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using Slyce.Common;


namespace ArchAngel.NHibernateHelper.UI
{
	public partial class FormSelectClasses : Form
	{
		private readonly IFileController fileController;

		public FormSelectClasses()
		{
			InitializeComponent();
		}

		private void buttonFetch_Click(object sender, EventArgs e)
		{
			string csprojFilename = textBoxCsProjFile.Text;
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(fileController.ReadAllText(csprojFilename));
			ArchAngel.Providers.EntityModel.Model.MappingLayer.MappingSet mappingSet = null;

			GetProject(mappingSet, doc, csprojFilename);
		}

		private void GetProject(ArchAngel.Providers.EntityModel.Model.MappingLayer.MappingSet mappingSet, XmlDocument csprojDocument, string filename)
		{
			var hbmFiles = ProjectLoader.GetHBMFilesFromCSProj(new CSProjFile(csprojDocument, filename), fileController);

			// Load HBMs
			var mappings = hbmFiles.Select(f => MappingFiles.Version_2_2.Utility.Open(f)).ToList();

			// Parse the CSharp files
			var csharpFiles = ProjectLoader.GetCSharpFilesFromCSProj(new CSProjFile(csprojDocument, filename), fileController);
			ParseResults parseResults = ParseResults.ParseCSharpFiles(csharpFiles);

			//foreach (ArchAngel.Providers.CodeProvider.DotNet.Class c in parseResults.parsedClasses)
			//{
			//}
			//mappingSet.CodeParseResults = parseResults;

			// Clear the current mapped class.
			//mappingSet.EntitySet.Entities.ForEach(e => e.MappedClass = null);

			// Map the Entity objects to the parsed Class
			var entities = mappingSet.EntitySet.Entities.ToDictionary(e => e.Name);

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
						//Log.InfoFormat("Could not find entity for class named {0} in the NHibernate project on disk.", shortClassName);
					}
				}
			}

		}
	}
}
