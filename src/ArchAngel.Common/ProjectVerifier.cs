using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using ArchAngel.Interfaces;
using log4net;

namespace ArchAngel.Common
{
	public class NullVerificationIssueSolver : IVerificationIssueSolver
	{
		public string GetValidTemplateFilePath(string projectFile, string oldTemplateFile)
		{
			return null;
		}

		public string GetValidProjectDirectory(string projectFile, string oldOutputFolder)
		{
			return null;
		}

		public void InformUserThatAppConfigIsInvalid(string message)
		{
		}
	}

	public class CustomVerificationIssueSolver : IVerificationIssueSolver
	{
		private readonly string projectDirectory;
		private readonly string templateFilePath;

		public CustomVerificationIssueSolver(string templateFilePath, string projectDirectory)
		{
			this.templateFilePath = templateFilePath;
			this.projectDirectory = projectDirectory;
		}

		public string GetValidTemplateFilePath(string projectFile, string oldTemplateFile)
		{
			return templateFilePath;
		}

		public string GetValidProjectDirectory(string projectFile, string oldOutputFolder)
		{
			return projectDirectory;
		}

		public void InformUserThatAppConfigIsInvalid(string message)
		{
		}
	}

	public class ProjectVerifier
	{
		private readonly List<Failure> failures = new List<Failure>();
		private readonly string aaprojFilePath;

		private static readonly ILog log = LogManager.GetLogger(typeof(ProjectVerifier));

		/// <summary>
		/// </summary>
		/// <param name="aaprojFilePath"></param>
		public ProjectVerifier(string aaprojFilePath)
		{
			this.aaprojFilePath = aaprojFilePath;
		}

		public enum Failure
		{
			AppConfig_CouldNotLoad,
			AppConfig_Invalid,
			ProjectDirectory_DoesNotExist,
			TemplateAssembly_DoesNotExist,
		}

		public bool Verify(string scratchFolder)
		{
			try
			{
				XmlDocument appconfig = new XmlDocument();
				string appConfigPath = Path.Combine(scratchFolder, "appconfig.xml");

				if (!File.Exists(appConfigPath))
				{
					failures.Add(Failure.AppConfig_CouldNotLoad);
					return false;
				}
				try
				{
					appconfig.Load(appConfigPath);
				}
				catch (Exception)
				{
					failures.Add(Failure.AppConfig_CouldNotLoad);
					return false;
				}

				bool verified = true;

				verified &= VerifyProjectDirectory(appconfig);

				verified &= VerifyTemplateAssemblyFilename(appconfig);

				return verified;
			}
			finally
			{
				foreach (var failure in failures)
					log.InfoFormat("Project Verification Failure: {0}", failure);
			}
		}

		private bool VerifyTemplateAssemblyFilename(XmlNode doc)
		{
			XmlNode node = doc.SelectSingleNode("appconfig/templatefilename");
			if (node == null)
			{
				failures.Add(Failure.AppConfig_Invalid);
				return false;
			}

			string filename = GetAbsoluteTemplateFilename(doc);
			if (File.Exists(filename) == false)
			{
				failures.Add(Failure.TemplateAssembly_DoesNotExist);
				return false;
			}

			return true;
		}

		private string GetAbsoluteTemplateFilename(XmlNode doc)
		{
			string templatePath = Slyce.Common.RelativePaths.RelativeToAbsolutePath(
				aaprojFilePath, doc.SelectSingleNode("appconfig/templatefilename").InnerXml);

			if (File.Exists(templatePath))
				return templatePath;

#if DEBUG
			templatePath = Slyce.Common.RelativePaths.RelativeToAbsolutePath(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase.Replace(@"file:///", "")), @"..\..\..\ArchAngel.Templates\NHibernate\Template\NHibernate.AAT.DLL");
#else
			templatePath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase.Replace(@"file:///", "")), Path.GetFileName(templatePath));
#endif

			return templatePath;
		}

		private string GetAbsoluteProjectPath(string projectPath)
		{
			return Slyce.Common.RelativePaths.RelativeToAbsolutePath(aaprojFilePath, projectPath);
		}

		public bool VerifyProjectDirectory(XmlDocument doc)
		{
			XmlNodeList nodes = doc.SelectNodes("appconfig/projectpath");
			if (nodes == null || nodes.Count != 1)
			{
				failures.Add(Failure.AppConfig_Invalid);
				return false;
			}

			XmlNode node = nodes[0];

			string userDirectory = GetAbsoluteProjectPath(node.InnerXml);
			if (Directory.Exists(userDirectory) == false)
			{
				failures.Add(Failure.ProjectDirectory_DoesNotExist);
				return false;
			}

			return true;
		}

		public bool DealWithVerificationFailures(IVerificationIssueSolver form, string scratchFolder, string projectFile)
		{
			XmlDocument appconfig = new XmlDocument();
			string appConfigPath = Path.Combine(scratchFolder, "appconfig.xml");

			if (File.Exists(appConfigPath))
			{
				appconfig = new XmlDocument();
				appconfig.Load(appConfigPath);
			}
			else if (!failures.Contains(Failure.AppConfig_CouldNotLoad) && !failures.Contains(Failure.AppConfig_Invalid))
				form.InformUserThatAppConfigIsInvalid("Your project is corrupted. The AppConfig XML file is missing.");

			foreach (Failure failure in failures)
			{
				switch (failure)
				{
					case Failure.AppConfig_CouldNotLoad:
					case Failure.AppConfig_Invalid:
						form.InformUserThatAppConfigIsInvalid(
							"Your project is corrupted. The AppConfig XML file is missing, unreadable, or invalid. Contact Slyce for further assistance.");
						break;
					case Failure.ProjectDirectory_DoesNotExist:
						if (appconfig == null)
							break;

						string newDir = form.GetValidProjectDirectory(projectFile, GetAbsoluteProjectPath(appconfig.SelectSingleNode("appconfig/projectpath").InnerXml));
						if (newDir == null)
							return false;
						SetProjectPath(appconfig, newDir);
						appconfig.Save(Path.Combine(scratchFolder, "appconfig.xml"));
						break;
					case Failure.TemplateAssembly_DoesNotExist:
						if (appconfig == null)
							break;

						string newFile = form.GetValidTemplateFilePath(projectFile, GetAbsoluteTemplateFilename(appconfig));
						if (newFile == null)
							return false;
						SetTemplateFilename(appconfig, newFile);
						appconfig.Save(Path.Combine(scratchFolder, "appconfig.xml"));
						break;
				}
			}
			return true;
		}

		private static void SetTemplateFilename(XmlNode doc, string newPath)
		{
			XmlNode node = doc.SelectSingleNode("appconfig/templatefilename");
			node.InnerText = newPath;
		}

		private static void SetProjectPath(XmlNode doc, string newPath)
		{
			XmlNode node = doc.SelectSingleNode("appconfig/projectpath");
			node.InnerText = newPath;
		}
	}
}