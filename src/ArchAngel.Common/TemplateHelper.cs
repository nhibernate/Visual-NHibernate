using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ArchAngel.Common
{
	public class UserTemplateHelper
	{
		public static ArchAngel.Interfaces.Template.TemplateProject GetDefaultTemplate()
		{
			string officialTemplateFolder;

#if DEBUG
			string basePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            officialTemplateFolder = Slyce.Common.RelativePaths.GetFullPath(basePath, @"..\..\..\..\ArchAngel.Templates");
#else
			officialTemplateFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
			officialTemplateFolder = Path.Combine(officialTemplateFolder, "Templates");
#endif
			// Add the official templates
			ArchAngel.Interfaces.Template.TemplateProject proj = ArchAngel.Common.UserTemplateHelper.GetTemplates(officialTemplateFolder).OrderBy(t => t.Name).FirstOrDefault();

			if (proj == null)
				throw new Exception(string.Format("No templates found in the templates folder: {0}{1}Re-install Visual NHibernate to repair this.", officialTemplateFolder, Environment.NewLine));

			proj.IsOfficial = true;
			return proj;
		}

		public static List<ArchAngel.Interfaces.Template.TemplateProject> GetTemplates()
		{
			return GetTemplates(GetTemplatesFolder());
		}

		public static List<ArchAngel.Interfaces.Template.TemplateProject> GetTemplates(string folder)
		{
			if (!Directory.Exists(folder))
				Directory.CreateDirectory(folder);

			List<ArchAngel.Interfaces.Template.TemplateProject> results = new List<ArchAngel.Interfaces.Template.TemplateProject>();

			foreach (string file in System.IO.Directory.GetFiles(folder, "*.vnh_template"))
			{
				ArchAngel.Interfaces.Template.TemplateProject proj = ArchAngel.Interfaces.Template.TemplateDeserializer.DeserialiseTemplateProject(File.ReadAllText(file));
				proj.Name = Path.GetFileNameWithoutExtension(file);
				proj.File = file;

				string filesDir = Path.Combine(folder, string.Format("{0}_files", proj.Name));
				string resourcesDir = Path.Combine(filesDir, "Resources");

				AddScriptBodies(proj.OutputFolder, filesDir);

				if (Directory.Exists(resourcesDir))
					foreach (string resourceFile in Directory.GetFiles(resourcesDir))
						proj.ResourceFiles.Add(Path.GetFileName(resourceFile));

				results.Add(proj);
			}
			return results;
		}

		private static void AddScriptBodies(ArchAngel.Interfaces.Template.Folder folder, string filesDir)
		{
			foreach (var subFolder in folder.Folders)
				AddScriptBodies(subFolder, filesDir);

			foreach (var file in folder.Files)
			{
				string filePath = Path.Combine(filesDir, string.Format("File_{0}.vnh_script", file.Id));
				file.Script.Body = File.ReadAllText(filePath);
			}

			foreach (var file in folder.StaticFiles)
			{
				string filePath = Path.Combine(filesDir, string.Format("StaticFile_{0}_Skip.vnh_script", file.Id));
				file.SkipThisFileScript = File.ReadAllText(filePath);
			}
		}

		public static string GetTemplatesFolder()
		{
			string vnhFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Visual NHibernate");

			if (!Directory.Exists(vnhFolder))
				Directory.CreateDirectory(vnhFolder);

			string templatesFolder = Path.Combine(vnhFolder, "Templates");

			if (!Directory.Exists(templatesFolder))
				Directory.CreateDirectory(templatesFolder);

			return templatesFolder;
		}
	}
}
