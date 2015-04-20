using System;
using System.Globalization;
using System.IO;

namespace Slyce.IntelliMerge.Controller.ManifestWorkers
{
    /// <summary>
    /// This class manages the creation of backups of User and Prevgen files.
    /// </summary>
    public class BackupUtility
    {
        public void CreateBackup(string projectDirectory, Guid projectGuid, string templateName, string outputDirectory, string timeString)
        {
            if (Directory.Exists(projectDirectory) == false)
                return;

            // UserFiles
            string[] allfiles = Directory.GetFiles(projectDirectory);

            if (allfiles.Length == 0) return;

        	string archAngelFolder = Path.Combine(outputDirectory, ManifestConstants.ArchAngelFolder);

			if(!Directory.Exists(archAngelFolder))
			{
				Directory.CreateDirectory(archAngelFolder);
			}

        	string backupFolder = Path.Combine(archAngelFolder,
                                               "ArchAngelBackup");

            backupFolder = Path.Combine(backupFolder, timeString);

            string userbackupDir = Path.Combine(backupFolder, "UserFiles");

            foreach (string file in allfiles)
            {
                string filename = Path.Combine(userbackupDir, Path.GetFileName(file));
                Directory.CreateDirectory(Path.GetDirectoryName(filename));
                File.Copy(file, filename, true);
            }

            string prevgenbackupDir = Path.Combine(backupFolder, "PrevGenFiles");
			string prevgendir = Path.Combine(Path.Combine(projectDirectory, ManifestConstants.ArchAngelFolder), projectGuid.ToString("B") + "_" + templateName);
        	
			if (!Directory.Exists(prevgendir)) return;

        	allfiles =
        		Directory.GetFiles(prevgendir);

        	foreach (string file in allfiles)
        	{
        		string filename = Path.Combine(prevgenbackupDir, Path.GetFileName(file));
        		Directory.CreateDirectory(Path.GetDirectoryName(filename));
        		File.Copy(file, filename, true);
        	}
        }

        public static string GetTimeString()
        {
            return DateTime.Now.ToShortDateString().Replace("/", ".") + "-" + DateTime.Now.ToString("HH'h'mm'm'ss's'", CultureInfo.InvariantCulture);
        }
    }
}
