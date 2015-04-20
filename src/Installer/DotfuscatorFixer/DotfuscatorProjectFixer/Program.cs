using System.IO;

namespace DotfuscatorProjectFixer
{
	public class Program
	{
		static void Main(string[] args)
		{
			string newFilePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
			string[] config = File.ReadAllLines(args[0]);

			string[] dofuscatorFiles = Directory.GetFiles(".", config[0], SearchOption.TopDirectoryOnly);

			foreach(string filename in dofuscatorFiles)
			{
				string fileText = File.ReadAllText(filename);

				for (int i = 1; i < config.Length; i++)
				{
					string pathToReplace = config[i];
					fileText = fileText.Replace(pathToReplace, newFilePath);
				}

				File.WriteAllText(filename, fileText);
			}
		}
	}
}
