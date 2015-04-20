using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;
using ArchAngel.Interfaces;
using Slyce.Common;

namespace ArchAngel.Common
{
	[Serializable]
	[DotfuscatorDoNotRename]
	public sealed class ProjectSettings : IWorkbenchProjectSettings
	{
		[DotfuscatorDoNotRename]
		private string _projectPath;
		[DotfuscatorDoNotRename]
		private string _templateFileName;
		[DotfuscatorDoNotRename]
		private Guid _projectGuid = Guid.NewGuid(); // Needed to set the Guid on a new project.

		/// <summary>
		/// Default constructor. Does nothing.
		/// </summary>
		[DotfuscatorDoNotRename]
		public ProjectSettings()
		{
		}

		private string _UserTemplateName = "Default NHibernate";

		public string UserTemplateName
		{
			get { return _UserTemplateName; }
			set { _UserTemplateName = value; }
		}

		public bool OverwriteFiles { get; set; }

		/// <summary>
		/// Sets up the Project Settings with defaults.
		/// </summary>
		/// <param name="defaultTemplateFileName"></param>
		[DotfuscatorDoNotRename]
		public ProjectSettings(string defaultTemplateFileName)
		{
			_templateFileName = defaultTemplateFileName;
		}

		[DotfuscatorDoNotRename]
		public ProjectSettings(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			Type type = GetType();
			while (type != typeof(object))
			{
				MemberInfo[] fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

				foreach (MemberInfo field in fields)
				{
					FieldInfo reflectedField = field.ReflectedType.GetField(field.Name, BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance);
					try
					{
						if (field.Name != "__identity") // MarshalByRef property, which we don't want serialized etc.
						{
							object reflectedVal = serializationInfo.GetValue(field.Name, reflectedField.FieldType);
							type.InvokeMember(field.Name, BindingFlags.SetField | BindingFlags.NonPublic | BindingFlags.Instance, null, this, new object[] { reflectedVal });
						}
					}
					catch
					{
						//throw; 
					}
				}
				type = type.BaseType;
			}
		}

		[DotfuscatorDoNotRename]
		public void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			Type type = GetType();
			while (type != typeof(object))
			{
				MemberInfo[] fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
				foreach (MemberInfo field in fields)
				{
					object reflectedObj = type.InvokeMember(field.Name, BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance, null, this, null);
					serializationInfo.AddValue(field.Name, reflectedObj);
				}
				type = type.BaseType;
			}
		}

		[DotfuscatorDoNotRename]
		public string OutputPath
		{
			get { return _projectPath; }
			set
			{
				ArchAngel.Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _projectPath, value);
				_projectPath = value;
				ArchAngel.Interfaces.SharedData.ProjectPath = value;
			}
		}

		[DotfuscatorDoNotRename]
		public string TemplateFileName
		{
			get { return _templateFileName; }
			set
			{
				//if (!string.IsNullOrEmpty(value))
				//{
				//    if (!File.Exists(value) && Controller.Instance.ProjectFile != null)
				//    {
				//        // Let's see if we can find the template file in the current project folder
				//        string testFile = Path.Combine(Path.GetDirectoryName(Controller.Instance.ProjectFile), Path.GetFileName(value));

				//        if (File.Exists(testFile))
				//        {
				//            value = testFile;
				//        }
				//    }
				//}
				Interfaces.Events.RaiseDataChangedEvent(GetType(), (MethodInfo)MethodBase.GetCurrentMethod(), _templateFileName, value);
				_templateFileName = value;
				Interfaces.SharedData.TemplateFileName = value;
			}
		}

		///<summary>
		/// The unique identifier for this project.
		///</summary>
		public Guid ProjectGuid
		{
			get { return _projectGuid; }
			set { _projectGuid = value; }
		}

		public void Save(string file, IWorkbenchProject project)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode rootNode = doc.CreateElement("appconfig");
			doc.AppendChild(rootNode);

			XmlNode node = doc.CreateElement("projectpath");
			node.InnerText = RelativePaths.GetRelativePath(Path.GetDirectoryName(project.ProjectFile), OutputPath);
			rootNode.AppendChild(node);

			node = doc.CreateElement("templatefilename");

			node.InnerText = project.GetPathRelativeToProjectFile(TemplateFileName);
			rootNode.AppendChild(node);

			node = doc.CreateElement("guid");
			node.InnerText = ProjectGuid.ToString("B");
			rootNode.AppendChild(node);

			node = doc.CreateElement("user-template-name");
			node.InnerText = UserTemplateName;
			rootNode.AppendChild(node);

			doc.Save(file);
		}

		public void Open(string file, IWorkbenchProject project)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(file);

			string relativePath = doc.SelectSingleNode("appconfig/projectpath").InnerText;
			OutputPath = RelativePaths.GetFullPath(Path.GetDirectoryName(project.ProjectFile), relativePath);

			string templateFile = doc.SelectSingleNode("appconfig/templatefilename").InnerText;
			templateFile = project.GetPathAbsoluteFromProjectFile(templateFile);

			if (!File.Exists(templateFile))
			{
#if DEBUG
				templateFile = Slyce.Common.RelativePaths.RelativeToAbsolutePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase.Replace(@"file:///", "")), @"..\..\..\ArchAngel.Templates\NHibernate\Template\NHibernate.AAT.DLL");
#else
				templateFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase.Replace(@"file:///", "")), Path.GetFileName(templateFile));
#endif

			}
			TemplateFileName = templateFile;

			XmlNode node = doc.SelectSingleNode("appconfig/guid");
			if (node != null)
				ProjectGuid = new Guid(node.InnerText);
			else
				ProjectGuid = Guid.NewGuid();

			XmlNode userTemplateNode = doc.SelectSingleNode("appconfig/user-template-name");
			if (userTemplateNode != null)
				UserTemplateName = userTemplateNode.InnerText;
			else
				UserTemplateName = "Default NHibernate";
		}
	}
}