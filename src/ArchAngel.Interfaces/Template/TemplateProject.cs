using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace ArchAngel.Interfaces.Template
{
	public class TemplateProject
	{
		public enum DelimiterTypes
		{
			ASP,
			T4
		}
		private string _File;

		public TemplateProject()
		{
			Delimiter = DelimiterTypes.ASP;

			OutputFolder = new Folder()
			{
				ID = 1,
				Name = "Output folder",
				Iterator = IteratorTypes.None,
				Files = new List<File>(),
				Folders = new List<Folder>()
			};
		}

		public DelimiterTypes Delimiter { get; set; }
		public bool IsOfficial { get; set; }
		public string Name { get; set; }
		public Folder OutputFolder { get; set; }
		public bool IsDirty { get; set; }
		public List<string> ResourceFiles = new List<string>();

		public ReadOnlyCollection<File> AllScriptFiles
		{
			get { return GetAllScriptFiles(OutputFolder).AsReadOnly(); }
		}

		private List<File> GetAllScriptFiles(Folder folder)
		{
			List<File> files = folder.Files;

			foreach (var subFolder in folder.Folders)
				files.AddRange(GetAllScriptFiles(subFolder));

			return files;
		}

		public string DelimiterStart
		{
			get
			{
				if (Delimiter == Interfaces.Template.TemplateProject.DelimiterTypes.ASP)
					return @"<%";
				else if (Delimiter == Interfaces.Template.TemplateProject.DelimiterTypes.T4)
					return @"<#";
				else
					throw new NotImplementedException("DelimiterType not handled yet:" + Delimiter.ToString());
			}
		}

		public string DelimiterEnd
		{
			get
			{
				if (Delimiter == Interfaces.Template.TemplateProject.DelimiterTypes.ASP)
					return @"%>";
				else if (Delimiter == Interfaces.Template.TemplateProject.DelimiterTypes.T4)
					return @"#>";
				else
					throw new NotImplementedException("DelimiterType not handled yet:" + Delimiter.ToString());
			}
		}

		public int GetNextAvailableFileId()
		{
			return GetMaxFileId(OutputFolder) + 1;
		}

		private int GetMaxFileId(Folder folder)
		{
			int max = 0;

			foreach (var subFolder in folder.Folders)
				max = Math.Max(max, GetMaxFileId(subFolder));

			foreach (var file in folder.Files)
				max = Math.Max(max, file.Id);

			return max;
		}

		public int GetNextAvailableStaticFileId()
		{
			return GetMaxStaticFileId(OutputFolder) + 1;
		}

		private int GetMaxStaticFileId(Folder folder)
		{
			int max = 0;

			foreach (var subFolder in folder.Folders)
				max = Math.Max(max, GetMaxStaticFileId(subFolder));

			foreach (var file in folder.StaticFiles)
				max = Math.Max(max, file.Id);

			return max;
		}

		public int GetNextAvailableFolderId()
		{
			return GetMaxFolderId(OutputFolder) + 1;
		}

		private int GetMaxFolderId(Folder folder)
		{
			int max = folder.ID;

			foreach (var subFolder in folder.Folders)
				max = Math.Max(max, GetMaxFolderId(subFolder));

			return max;
		}

		public bool Save(bool isSaveAs)
		{
			string newName = Path.GetFileNameWithoutExtension(File);

			if (isSaveAs)
				CopyResourceFiles(this.Name, newName);

			this.IsOfficial = false;
			this.Name = newName;
			string contents = TemplateSerializer.Serialise(this);
			System.IO.File.WriteAllText(File, contents);

			if (Directory.Exists(ScriptFilesFolder))
				Slyce.Common.Utility.DeleteDirectoryContentsBrute(ScriptFilesFolder);

			if (!Directory.Exists(ScriptFilesFolder))
				Directory.CreateDirectory(ScriptFilesFolder);

			SaveScriptFiles(this.OutputFolder);
			IsDirty = false;
			return true;
		}

		private void SaveScriptFiles(Folder folder)
		{
			foreach (var subFolder in folder.Folders)
				SaveScriptFiles(subFolder);

			foreach (var file in folder.Files)
			{
				string filepath = Path.Combine(ScriptFilesFolder, string.Format("File_{0}.vnh_script", file.Id));
				System.IO.File.WriteAllText(filepath, file.Script.Body);
			}

			foreach (var file in folder.StaticFiles)
			{
				string filepath = Path.Combine(ScriptFilesFolder, string.Format("StaticFile_{0}_Skip.vnh_script", file.Id));
				System.IO.File.WriteAllText(filepath, file.SkipThisFileScript);
			}
		}

		private void CopyResourceFiles(string oldName, string newName)
		{
			if (IsOfficial)
				oldName = oldName.Trim('[', ']');

			string userTemplatesFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Visual NHibernate" + Path.DirectorySeparatorChar + "Templates");

			if (!Directory.Exists(userTemplatesFolder))
				Directory.CreateDirectory(userTemplatesFolder);

			string officialTemplateFolder;
#if DEBUG
            officialTemplateFolder = Slyce.Common.RelativePaths.GetFullPath(System.Reflection.Assembly.GetExecutingAssembly().Location, @"..\..\..\..\ArchAngel.Templates");
#else
			officialTemplateFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
			officialTemplateFolder = Path.Combine(officialTemplateFolder, "Templates");
#endif
			string fromFolder = IsOfficial ? officialTemplateFolder : userTemplatesFolder;
			fromFolder = Path.Combine(Path.Combine(fromFolder, string.Format("{0}_files", oldName)), "Resources");
			string toFolder = Path.Combine(Path.Combine(userTemplatesFolder, string.Format("{0}_files", newName)), "Resources");

			if (fromFolder.ToLowerInvariant() == toFolder.ToLowerInvariant())
				return;

			if (Directory.Exists(toFolder))
				Slyce.Common.Utility.DeleteDirectoryContentsBrute(toFolder);

			if (!Directory.Exists(toFolder))
				Directory.CreateDirectory(toFolder);

			foreach (var file in this.ResourceFiles)
				System.IO.File.Copy(Path.Combine(fromFolder, file), Path.Combine(toFolder, file));
		}

		private string ScriptFilesFolder
		{
			get
			{
				string folder = Path.Combine(FilesFolder, string.Format("{0}_files", Name));

				if (!Directory.Exists(folder))
					Directory.CreateDirectory(folder);

				return folder;
			}
		}

		public string ResourceFilesFolder
		{
			get
			{
				string folder = Path.Combine(ScriptFilesFolder, "Resources");

				if (!Directory.Exists(folder))
					Directory.CreateDirectory(folder);

				return folder;
			}
		}

		public string File
		{
			get { return _File; }
			set
			{
				_File = value;
				FilesFolder = Path.GetDirectoryName(File);
			}
		}

		private string FilesFolder { get; set; }

		public void DeleteResourceFile(string file)
		{
			this.ResourceFiles.Remove(file);
			Slyce.Common.Utility.DeleteFileBrute(Path.Combine(ResourceFilesFolder, file));
		}
	}
}
