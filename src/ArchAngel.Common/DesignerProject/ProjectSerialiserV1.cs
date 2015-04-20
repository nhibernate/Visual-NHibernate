using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using ArchAngel.Common.DesignerProject;
using Slyce.Common;

namespace ArchAngel.Designer.DesignerProject
{
	public interface IProjectSerialiser
	{
		void CreateApiExtensionFiles(IEnumerable<ApiExtensionMethod> extensions, string folder);
		void CreateFunctionFiles(IEnumerable<FunctionInfo> functions, string folder);
		void CreateOutputFile(OutputFolder rootFolder, string folder);
		void CreateProjectDetailsFile(IDesignerProject project, string folder);
		void CreateUserOptionsFiles(IEnumerable<UserOption> userOptions, string folder);
		void WriteProjectToDisk(IDesignerProject project, string projectFolder);
		void CreateUserOptionDetailsFile(IEnumerable<UserOption> options, string folder);
		void CreateStaticFiles(IEnumerable<IncludedFile> filePaths, string folder);
		void CreateStaticFilesDetails(IEnumerable<IncludedFile> filePaths, string folder);
		string WriteProjectToSingleXmlFile(IDesignerProject project);
	}

	public class ProjectSerialiserV1 : IProjectSerialiser
	{
		private readonly IFileController fileController;

		public const string ProjectFilesFolder = "_AADProjectFiles";
		public const string UserOptionFolder = "UserOptions";
		public const string FunctionFilesFolder = "Functions";
		public const string ApiExtensionFolder = "ApiExtensions";
		public const string StaticFilesFolder = "StaticFiles";
		public const string OutputsFileName = "Outputs.xml";
		public const string UserOptionsDetailsFileName = "UserOptions.xml";
		public const string StaticFilesDetailsFileName = "StaticFiles.xml";

		public ProjectSerialiserV1(IFileController fileController)
		{
			if (fileController == null) throw new ArgumentNullException("fileController");

			this.fileController = fileController;
		}

		protected virtual string GetFilenameFor(UserOption userOption)
		{
			return userOption.VariableName + ".useroption.xml";
		}

		protected virtual string GetFilenameFor(FunctionInfo func)
		{
			return func.Name + ".function.xml";
		}

		private string GetFilenameFor(IDesignerProject project)
		{
			return project.ProjectName + ".aad";
		}

		private string GetFilenameForApiExtention(Type extendedType)
		{
			return extendedType.FullName + ".apiext.xml";
		}

		public void WriteProjectToDisk(IDesignerProject project, string projectFolder)
		{
			string projectFilesFolder = projectFolder.PathSlash(ProjectFilesFolder);

			CheckFolder(projectFolder);
			CheckFolder(projectFilesFolder);
			CheckFolder(projectFilesFolder.PathSlash(ApiExtensionFolder));
			CheckFolder(projectFilesFolder.PathSlash(FunctionFilesFolder), true);
			CheckFolder(projectFilesFolder.PathSlash(UserOptionFolder));

			CreateProjectDetailsFile(project, projectFolder);
			CreateOutputFile(project.RootOutput, projectFilesFolder);
			CreateApiExtensionFiles(project.ApiExtensions, projectFilesFolder.PathSlash(ApiExtensionFolder));
			CreateFunctionFiles(project.Functions, projectFilesFolder.PathSlash(FunctionFilesFolder));
			CreateUserOptionsFiles(project.UserOptions, projectFilesFolder.PathSlash(UserOptionFolder));
			CreateUserOptionDetailsFile(project.UserOptions, projectFilesFolder.PathSlash(UserOptionFolder));
			CreateStaticFiles(project.IncludedFiles, projectFilesFolder.PathSlash(StaticFilesFolder));
		}

		public void CreateUserOptionDetailsFile(IEnumerable<UserOption> options, string folder)
		{
			string text = GetText(options, WriteUserOptionDetailsXML);
			string filePath = folder.PathSlash(UserOptionsDetailsFileName);

			fileController.WriteAllText(filePath, text);
		}

		public void CreateStaticFiles(IEnumerable<IncludedFile> filePaths, string folder)
		{
			CreateStaticFilesDetails(filePaths, folder);

			foreach (string filePath in filePaths.Select(f => f.FullFilePath))
			{
				string filename = Path.GetFileName(filePath);

				var fileContents = fileController.ReadAllBytes(filePath);
				string targetPath = folder.PathSlash(filename);

				// Don't copy a file to itself.
				if (targetPath == filePath)
					continue;

				fileController.WriteAllBytes(targetPath, fileContents);
			}
		}

		public void CreateStaticFilesDetails(IEnumerable<IncludedFile> filePaths, string folder)
		{
			string text = GetText(filePaths, WriteStaticFilesDetails);
			string filePath = folder.PathSlash(StaticFilesDetailsFileName);

			fileController.WriteAllText(filePath, text);
		}

		public void CreateOutputFile(OutputFolder rootFolder, string folder)
		{
			string text = GetText(rootFolder, WriteOutputsXML);
			string filePath = folder.PathSlash(OutputsFileName);

			fileController.WriteAllText(filePath, text);
		}

		public void CreateProjectDetailsFile(IDesignerProject project, string folder)
		{
			string text = GetText(project, WriteProjectDetailsXML);
			string filePath = folder.PathSlash(GetFilenameFor(project));

			fileController.WriteAllText(filePath, text);
		}

		public void CreateApiExtensionFiles(IEnumerable<ApiExtensionMethod> extensions, string folder)
		{
			var apiGroupsByType = extensions.GroupBy(a => a.ExtendedMethod.DeclaringType);

			foreach (IGrouping<Type, ApiExtensionMethod> extendedType in apiGroupsByType)
			{
				// Need to make a copy of this because we are using it in a lambda, 
				// and it could change before the lambda is executed.
				IGrouping<Type, ApiExtensionMethod> grouping = extendedType;

				string text = GetText(extendedType.ToList(), (arg1, arg2) => WriteApiExtensionMethodXML(arg1, grouping.Key, arg2));
				string filePath = folder.PathSlash(GetFilenameForApiExtention(extendedType.Key));
				fileController.WriteAllText(filePath, text);
			}
		}

		public void CreateFunctionFiles(IEnumerable<FunctionInfo> functions, string folder)
		{
			foreach (var func in functions)
			{
				string text = GetText(func, WriteFunctionXML);
				fileController.WriteAllText(folder.PathSlash(GetFilenameFor(func)), text);
			}
		}

		public void CreateUserOptionsFiles(IEnumerable<UserOption> userOptions, string folder)
		{
			foreach (var uo in userOptions)
			{
				string text = GetText(uo, WriteUserOptionXML);

				string filename = folder.PathSlash(GetFilenameFor(uo));
				fileController.WriteAllText(filename, text);
			}
		}

		public string WriteProjectToSingleXmlFile(IDesignerProject project)
		{
			return GetText(project, WriteProjectToSingleXmlFile);
		}

		private void WriteProjectToSingleXmlFile(IDesignerProject project, XmlWriter writer)
		{
			Document doc = new Document(writer);

			using (doc.StartElement("MegaProject"))
			{
				WriteProjectDetailsXML(project, writer);

				using (doc.StartElement("AllApiExtensions"))
				{
					var apiGroupsByType = project.ApiExtensions.GroupBy(a => a.ExtendedMethod.DeclaringType);

					foreach (var extendedType in apiGroupsByType)
					{
						WriteApiExtensionMethodXML(extendedType, extendedType.Key, writer);
					}
				}

				using (doc.StartElement("Functions"))
				{
					writer.WriteAttributeString("version", "1");
					foreach (var function in project.Functions)
						WriteFunctionXML(function, writer);
				}

				using (doc.StartElement("UserOptions"))
				{
					writer.WriteAttributeString("version", "1");
					foreach (var uo in project.UserOptions)
						WriteUserOptionXML(uo, writer);

					WriteUserOptionDetailsXML(project.UserOptions, writer);
				}

				WriteStaticFilesDetails(project.IncludedFiles, writer);
				WriteOutputsXML(project.RootOutput, writer);
			}
		}

		public void WriteProjectDetailsXML(IDesignerProject project, XmlWriter writer)
		{
			Document doc = new Document(writer);

			using (doc.StartElement("Project"))
			{
				writer.WriteAttributeString("version", "1");
				writer.WriteElementString("Name", project.ProjectName);
				writer.WriteElementString("Description", project.ProjectDescription);

				using (doc.StartElement("Config"))
				{
					writer.WriteElementString("RelativeCompilePath", project.GetCompiledDLLDirectory());
					writer.WriteElementString("Version", project.Version);
					writer.WriteElementString("ProjectType", project.ProjType.ToString());
					writer.WriteElementString("DebugProjectFile", project.GetPathRelativeToProjectFile(project.DebugProjectFile));
					writer.WriteElementString("TestGenerateDirectory", project.GetPathRelativeToProjectFile(project.TestGenerateDirectory));

					using (doc.StartElement("IncludedNamespaces"))
					{
						foreach (var ns in project.Namespaces.OrderBy(n => n))
							writer.WriteElementString("Namespace", ns);
					}

					using (doc.StartElement("References"))
					{
						foreach (var reference in project.References.OrderBy(r => r.FileName))
						{
							WriteProjectReference(reference, writer, project);
						}
					}
				}
			}
		}

		public void WriteOutputsXML(OutputFolder folder, XmlWriter writer)
		{
			Document doc = new Document(writer);

			using (doc.StartElement("Outputs"))
			{
				writer.WriteAttributeString("version", "1");

				WriteOutputFolderXML(folder, writer);
			}
		}

		private void WriteOutputFolderXML(OutputFolder folder, XmlWriter writer)
		{
			writer.WriteStartElement("Folder");

			writer.WriteAttributeString("name", folder.Name);
			writer.WriteAttributeString("id", folder.Id);
			if (folder.IteratorType != null)
				writer.WriteAttributeString("iterator", folder.IteratorType.FullName);

			foreach (var subfolder in folder.Folders)
				WriteOutputFolderXML(subfolder, writer);

			foreach (var file in folder.Files)
				WriteOutputFileXML(file, writer);

			writer.WriteEndElement();
		}

		public void WriteOutputFileXML(OutputFile file, XmlWriter writer)
		{
			if (file.FileType == OutputFileTypes.File)
			{
				writer.WriteStartElement("StaticFile");

				writer.WriteAttributeString("name", file.Name);
				writer.WriteAttributeString("id", file.Id);
				if (file.StaticFileName != null)
				{
					writer.WriteAttributeString("staticfilename", file.StaticFileName);
				}

				if (file.StaticFileIterator != null)
					writer.WriteAttributeString("iteratorname", file.StaticFileIterator.FullName);

				if (string.IsNullOrEmpty(file.StaticFileSkipFunctionName) == false)
					writer.WriteAttributeString("skipfunction", file.StaticFileSkipFunctionName);

				writer.WriteEndElement();
			}
			else
			{
				if (string.IsNullOrEmpty(file.ScriptName))
					throw new SerializationException(string.Format("File {0} has no ScriptName", file.Name));

				writer.WriteStartElement("ScriptFile");

				writer.WriteAttributeString("name", file.Name);
				writer.WriteAttributeString("id", file.Id);
				writer.WriteAttributeString("scriptname", file.ScriptName);
				// I am not storing the IteratorType for scripts, as this information would be
				// duplicated from the function it is associated with.

				writer.WriteEndElement();
			}
		}

		private void WriteProjectReference(ReferencedFile reference, XmlWriter writer, IDesignerProject project)
		{
			writer.WriteStartElement("Reference");
			writer.WriteAttributeString("filename", project.GetPathRelativeToProjectFile(reference.FileName));
			writer.WriteAttributeString("mergewithassembly", reference.MergeWithAssembly.ToString(CultureInfo.InvariantCulture));
			writer.WriteAttributeString("useinworkbench", reference.UseInWorkbench.ToString(CultureInfo.InvariantCulture));
			writer.WriteAttributeString("isprovider", reference.IsProvider.ToString(CultureInfo.InvariantCulture));
			writer.WriteEndElement();
		}

		public void WriteApiExtensionMethodXML(IEnumerable<ApiExtensionMethod> extensions, Type extendedType, XmlWriter writer)
		{
			Document doc = new Document(writer);

			using (doc.StartElement("ApiExtensions"))
			{
				writer.WriteAttributeString("version", "1");
				writer.WriteAttributeString("type", extendedType.FullName);

				foreach (var apiExtensionMethod in extensions.OrderBy(e => e.ExtendedMethod.Name))
				{
					WriteSingleApiExtensionMethodXML(apiExtensionMethod, writer);
				}
			}
		}

		private void WriteSingleApiExtensionMethodXML(ApiExtensionMethod method, XmlWriter writer)
		{
			Document doc = new Document(writer);

			using (doc.StartElement("ApiExtension"))
			{
				writer.WriteElementString("MethodName", method.ExtendedMethod.Name);
				writer.WriteElementString("OverrideFunctionText", method.OverridingFunctionBody);
			}
		}

		public void WriteUserOptionDetailsXML(IEnumerable<UserOption> options, XmlWriter writer)
		{
			Document doc = new Document(writer);

			var groupedOptions = options.GroupBy(o => o.Category);

			using (doc.StartElement("UserOptionDetails"))
			{
				writer.WriteAttributeString("version", "1");

				foreach (var category in groupedOptions.OrderBy(g => g.Key))
				{
					using (doc.StartElement("Category"))
					{
						writer.WriteAttributeString("name", category.Key);

						foreach (var userOption in category.OrderBy(u => u.VariableName))
						{
							writer.WriteStartElement("UserOption");
							writer.WriteAttributeString("varname", userOption.VariableName);
							writer.WriteEndElement();
						}
					}
				}
			}
		}

		public void WriteFunctionXML(FunctionInfo func, XmlWriter writer)
		{
			writer.WriteStartElement("Function");
			{
				writer.WriteAttributeString("version", "1");
				writer.WriteAttributeString("about", "This file describes an ArchAngel Template function");

				writer.WriteElementString("Name", func.Name);
				writer.WriteElementString("IsTemplateFunction", func.IsTemplateFunction.ToString());
				writer.WriteElementString("IsExtensionMethod", func.IsExtensionMethod.ToString());
				writer.WriteElementString("ReturnType", func.ReturnType == null ? "System.Void" : func.ReturnType.FullName);
				writer.WriteElementString("TemplateReturnLanguage", func.TemplateReturnLanguage);
				writer.WriteElementString("ExtendedType", func.ExtendedType);
				writer.WriteElementString("ScriptLanguage", func.ScriptLanguage.ToString());
				writer.WriteElementString("Description", func.Description);
				writer.WriteElementString("Category", func.Category);

				writer.WriteStartElement("Parameters");
				{
					foreach (ParamInfo parm in func.Parameters)
					{
						writer.WriteStartElement("Parameter");

						writer.WriteAttributeString("name", parm.Name);
						writer.WriteAttributeString("type", parm.DataType.FullName);
						writer.WriteAttributeString("modifiers", parm.Modifiers);

						writer.WriteEndElement();
					}
				}
				writer.WriteEndElement();

				writer.WriteElementString("Body", func.Body);
			}
			writer.WriteEndElement();
		}

		public void WriteStaticFilesDetails(IEnumerable<IncludedFile> files, XmlWriter writer)
		{
			Document doc = new Document(writer);
			using (doc.StartElement("StaticFiles"))
			{
				writer.WriteAttributeString("version", "1");
				foreach (var file in files)
				{
					string filename = Path.GetFileName(file.FullFilePath);
					using (doc.StartElement("StaticFile"))
					{
						writer.WriteAttributeString("filename", filename);
						writer.WriteAttributeString("displayname", file.DisplayName);
					}
				}
			}
		}

		public void WriteUserOptionXML(UserOption option, XmlWriter writer)
		{
			writer.WriteStartElement("Option");
			{
				writer.WriteAttributeString("version", "1");

				writer.WriteElementString("VariableName", option.VariableName);
				writer.WriteElementString("Type", option.VarType.FullName);
				writer.WriteElementString("DisplayText", option.Text);
				writer.WriteElementString("Description", option.Description);
				writer.WriteElementString("DefaultValue", option.DefaultValueFunctionBody);
				writer.WriteElementString("IteratorName", option.IteratorType == null ? "" : option.IteratorType.FullName);
				writer.WriteElementString("ValidatorFunction", option.ValidatorFunctionBody);
				writer.WriteElementString("DisplayToUserFunction", option.DisplayToUserFunctionBody);
				writer.WriteElementString("ResetPerSession", option.ResetPerSession.ToString());

				writer.WriteStartElement("Values");
				{
					foreach (string optionValue in option.Values.OrderBy(v => v))
					{
						writer.WriteStartElement("Value");
						writer.WriteAttributeString("value", optionValue);
						writer.WriteEndElement();
					}
				}
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}

		private static string GetText<T>(T func, Action<T, XmlWriter> writeXML)
		{
			var sb = new StringBuilder();

			var writer = XmlWriter.Create(sb, new XmlWriterSettings { Indent = true, IndentChars = "\t" });
			if (writer == null) throw new Exception("Cannot create an XmlWriter");

			writeXML(func, writer);
			writer.Close();

			return sb.ToString();
		}

		private void CheckFolder(string folder, bool deleteContents)
		{
			if (deleteContents)
				Slyce.Common.Utility.DeleteDirectoryContentsBrute(folder, true);

			CheckFolder(folder);
		}

		/// <summary>
		/// Does some basic checks on the given folder to make sure it exists and is writable.
		/// </summary>
		/// <param name="folder"></param>
		private void CheckFolder(string folder)
		{
			if (fileController.DirectoryExists(folder) == false)
				fileController.CreateDirectory(folder);
			if (fileController.CanCreateFilesIn(folder) == false)
				throw new IOException("Functions folder is not writeable");
		}
	}

	internal class Element : IDisposable
	{
		private readonly XmlWriter Writer;

		public Element(XmlWriter writer, string elementName)
		{
			Writer = writer;
			writer.WriteStartElement(elementName);
		}

		public void Dispose()
		{
			Writer.WriteEndElement();
		}
	}

	internal class Document
	{
		private readonly XmlWriter Writer;

		public Document(XmlWriter writer)
		{
			Writer = writer;
		}

		public Element StartElement(string elementName) { return new Element(Writer, elementName); }
	}
}