using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using ArchAngel.Interfaces;
using Slyce.Common;
using UserOption = ArchAngel.Common.DesignerProject.UserOption;

namespace ArchAngel.Designer.DesignerProject
{
	public static class ProjectLoadAndSaveHelper
	{
		private const int ProjectFileFormatVersion = 2;

		/// <summary>
		/// Saves the project to an xml definition file
		/// </summary>
		public static void SaveToXml(string filePath, Project project)
		{
			XmlDocument doc = CreateProjectXmlDocument(project);

			string tempXmlFile = Path.Combine(Path.GetTempPath(), "definition.xml");
			project.ProjectXmlConfig = doc.InnerXml;
			// Save the document to a file and auto-indent the output.
			XmlTextWriter writer = null;
			try
			{
				writer = new XmlTextWriter(tempXmlFile, Encoding.UTF8);
				writer.Formatting = Formatting.Indented;
				doc.Save(writer);
				writer.Flush();
			}
			finally
			{
				if (writer != null) { writer.Close(); }
			}
			// Add this file to the zip file
			List<string> files = new List<string>();

			foreach (string filename in project.IncludedFiles.Select(f => f.DisplayName))
			{
				string tempFile = Path.Combine(Controller.TempPath, Path.GetFileName(filename));

				if (File.Exists(tempFile))
				{
					files.Add(tempFile);
				}
				else if (!Utility.StringsAreEqual(Path.GetFileName(tempFile), "thumbs.db", false))
				{
					throw new FileNotFoundException("File is missing: " + tempFile);
				}
			}
			files.Add(tempXmlFile);
			Utility.ZipFile(files, filePath, false);

			// Delete all the files we added to the temp path
			File.Delete(tempXmlFile);
		}

		public static XmlDocument CreateProjectXmlDocument(Project project)
		{
			XmlNode node;

			XmlDocument doc = new XmlDocument();
			node = doc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
			doc.AppendChild(node);
			XmlElement rootElement = doc.CreateElement("", "ROOT", "");
			XmlAttribute att = doc.CreateAttribute("about");
			att.Value = "This file is the settings file for an ArchAngel template project.";
			rootElement.Attributes.Append(att);
			att = doc.CreateAttribute("fileversion");
			att.Value = ProjectFileFormatVersion.ToString();
			rootElement.Attributes.Append(att);
			doc.AppendChild(rootElement);

			XmlElement projectElement = CreateProjectDetailsElement(rootElement, project);

			CreateUserOptionsCategoriesElement(project, projectElement);

			CreateUserOptionsElement(project, projectElement);

			//CreateDefaultValueFunctionsElement(project, projectElement);

			CreateOutputsElement(project, projectElement);

			CreateFunctionsElement(rootElement, project);

			CreateActionsElement(project, projectElement);
			return doc;
		}

		private static void CreateActionsElement(Project project, XmlElement projectElement)
		{
			XmlDocument doc = projectElement.OwnerDocument;

			XmlAttribute att;
			XmlElement actionsElement = AppendNewXmlElement(projectElement, "actions", "");

			foreach (BaseAction action in project.Actions)
			{
				XmlElement actionElement = AppendNewXmlElement(actionsElement, "action", "");
				att = doc.CreateAttribute("typename");
				// Get the attribute name
				object[] customAttributes = action.GetType().GetCustomAttributes(typeof(ActionAttribute), false);
				ActionAttribute actionAttribute = (ActionAttribute)customAttributes[0];
				att.Value = actionAttribute.OwnerType.FullName;
				actionElement.Attributes.Append(att);
				actionElement.InnerXml = action.SaveToXml();
			}
		}

		private static void CreateFunctionsElement(XmlNode xmlelem, Project project)
		{
			XmlDocument doc = xmlelem.OwnerDocument;

			foreach (FunctionInfo func in project.Functions)
			{
				var functionElement = CreateFunctionXML(func, doc);
				xmlelem.AppendChild(functionElement);
			}
		}

		private static void CreateOutputsElement(Project project, XmlElement projectElement)
		{
			XmlElement rootOutputElement = AppendNewXmlElement(projectElement, "rootoutput", "");
			XmlElement rootFolderElement = AppendNewXmlElement(rootOutputElement, "rootfolder", "");

			foreach (OutputFolder folder in project.RootOutput.Folders)
			{
				if (folder.Name != "ROOT")
				{
					SaveToXmlFolder(folder, rootFolderElement);
				}
			}
		}

		private static void CreateUserOptionsElement(Project project, XmlElement projectElement)
		{
			XmlElement optionsElement = AppendNewXmlElement(projectElement, "options", "");
			XmlDocument doc = projectElement.OwnerDocument;

			foreach (UserOption option in project.UserOptions)
			{
				XmlElement optionElement = CreateUserOptionXML(option, doc);
				optionsElement.AppendChild(optionElement);
			}
		}

		private static void CreateUserOptionsCategoriesElement(Project project, XmlElement projectElement)
		{
			XmlElement categoriesElement = AppendNewXmlElement(projectElement, "optionCategories", "");
			string[] categories = project.UserOptionCategories;

			foreach (string category in categories)
			{
				XmlElement categoryElement = AppendNewXmlElement(categoriesElement, "category", category.Replace(" ", "_"));
			}
		}

		private static XmlElement CreateProjectDetailsElement(XmlElement xmlelem, Project project)
		{
			AppendNewXmlElement(xmlelem, "namespaces", GetCsvString(project.Namespaces));
			StringBuilder refFiles = new StringBuilder(2000);

			int refFileCounter = 0;
			foreach (var refFile in project.References)
			{
				if (refFileCounter > 0)
				{
					refFiles.Append(",");
				}
				refFiles.Append(project.GetPathRelativeToProjectFile(refFile.FileName) + "|" + refFile.MergeWithAssembly + "|isProvider=" + refFile.IsProvider + "|useInWorkbench=" + refFile.UseInWorkbench);
				refFileCounter++;
			}
			AppendNewXmlElement(xmlelem, "referencedfiles", refFiles.ToString());
			XmlElement configElement = AppendNewXmlElement(xmlelem, "config", "");
			XmlElement projectElement = AppendNewXmlElement(configElement, "project", "");
			AppendNewXmlElement(projectElement, "name", project.ProjectName);
			AppendNewXmlElement(projectElement, "description", project.ProjectDescription);
			string compileFilePath = project.GetPathRelativeToProjectFile(Path.Combine(project.CompileFolderName, project.ProjectName + ".AAT.DLL"));
			AppendNewXmlElement(projectElement, "compilefile", compileFilePath);
			AppendNewXmlElement(projectElement, "version", project.Version);
			AppendNewXmlElement(projectElement, "projecttype", project.ProjType.ToString());
			AppendNewXmlElement(projectElement, "debugfile", project.DebugProjectFile);
			AppendNewXmlElement(projectElement, "testgeneratedirectory", project.TestGenerateDirectory);
			return projectElement;
		}

		private static XmlElement AppendNewXmlElement(XmlElement parentElement, string name, string text)
		{
			XmlElement xmlelem2 = parentElement.OwnerDocument.CreateElement("", name, "");

			if (text.Length > 0)
			{
				xmlelem2.InnerText = text;
			}
			parentElement.AppendChild(xmlelem2);
			return xmlelem2;
		}

		private static XmlAttribute AppendNewAttribute(XmlElement parentElement, string name, string text)
		{
			XmlAttribute att = parentElement.OwnerDocument.CreateAttribute(name);
			att.Value = text;
			parentElement.Attributes.Append(att);
			return att;
		}

		private static void SaveToXmlFolder(OutputFolder folder, XmlElement parentNode)
		{
			XmlElement folderNode = null;

			if (folder.Name != "root")
			{
				folderNode = AppendNewXmlElement(parentNode, "folder", "");
				AppendNewAttribute(folderNode, "id", folder.Id);
				AppendNewAttribute(folderNode, "name", folder.Name);
				AppendNewAttribute(folderNode, "iteratortype", folder.IteratorType == null ? "" : folder.IteratorType.FullName);
			}
			else
			{
				folderNode = parentNode;
			}

			#region Process files
			foreach (OutputFile file in folder.Files)
			{
				XmlElement fileNode;

				if (file.FileType == OutputFileTypes.File)
				{
					fileNode = AppendNewXmlElement(folderNode, "file", "");
					AppendNewAttribute(fileNode, "name", file.Name);
					AppendNewAttribute(fileNode, "staticfilename", file.StaticFileName);

					string staticFileIteratorTypeName = file.StaticFileIterator == null ? "" : Utility.GetDemangledGenericTypeName(file.StaticFileIterator);
					AppendNewAttribute(fileNode, "iteratorname", staticFileIteratorTypeName);
					AppendNewAttribute(fileNode, "id", file.Id);
				}
				else
				{
					fileNode = AppendNewXmlElement(folderNode, "script", "");
					AppendNewAttribute(fileNode, "filename", file.Name);
					AppendNewAttribute(fileNode, "scriptname", file.ScriptName);
					AppendNewAttribute(fileNode, "iteratorname", file.IteratorTypes);
					AppendNewAttribute(fileNode, "id", file.Id);
				}
			}
			#endregion

			#region Process sub-folders
			foreach (OutputFolder subFolder in folder.Folders)
			{
				SaveToXmlFolder(subFolder, folderNode);
			}
			#endregion
		}

		private static string GetCsvString(List<string> values)
		{
			string val = "";

			for (int i = 0; i < values.Count; i++)
			{
				if (i > 0)
				{
					val += ",";
				}
				val += values[i].Replace(" ", "_"); // Workbench does an XSL lookup using this name, for looking for stored settings. Spaces not allowed in XPath queries.
			}
			return val;
		}

		private static XmlElement CreateFunctionXML(FunctionInfo func, XmlDocument doc)
		{
			XmlElement functionElement = doc.CreateElement("function");

			XmlAttribute xmlAttribute = doc.CreateAttribute("name");
			xmlAttribute.Value = func.Name;
			functionElement.Attributes.Append(xmlAttribute);

			xmlAttribute = doc.CreateAttribute("returntype");
			xmlAttribute.Value = func.IsTemplateFunction ? func.TemplateReturnLanguage : (func.ReturnType == null ? "void" : func.ReturnType.Name);
			functionElement.Attributes.Append(xmlAttribute);

			xmlAttribute = doc.CreateAttribute("istemplatefunction");
			xmlAttribute.Value = func.IsTemplateFunction.ToString();
			functionElement.Attributes.Append(xmlAttribute);

			xmlAttribute = doc.CreateAttribute("isextensionmethod");
			xmlAttribute.Value = func.IsExtensionMethod.ToString();
			functionElement.Attributes.Append(xmlAttribute);

			xmlAttribute = doc.CreateAttribute("extendedtype");
			xmlAttribute.Value = func.ExtendedType;
			functionElement.Attributes.Append(xmlAttribute);

			xmlAttribute = doc.CreateAttribute("scriptlanguage");
			xmlAttribute.Value = func.ScriptLanguage.ToString();
			functionElement.Attributes.Append(xmlAttribute);

			xmlAttribute = doc.CreateAttribute("description");
			xmlAttribute.Value = func.Description;
			functionElement.Attributes.Append(xmlAttribute);

			xmlAttribute = doc.CreateAttribute("category");
			xmlAttribute.Value = func.Category;
			functionElement.Attributes.Append(xmlAttribute);

			foreach (ParamInfo parm in func.Parameters)
			{
				XmlElement xmlelem3 = doc.CreateElement("parameter");

				xmlAttribute = doc.CreateAttribute("name");
				xmlAttribute.Value = parm.Name;
				xmlelem3.Attributes.Append(xmlAttribute);

				xmlAttribute = doc.CreateAttribute("type");
				xmlAttribute.Value = parm.DataType.FullName;
				xmlelem3.Attributes.Append(xmlAttribute);

				xmlAttribute = doc.CreateAttribute("modifiers");
				xmlAttribute.Value = parm.Modifiers;
				xmlelem3.Attributes.Append(xmlAttribute);
				functionElement.AppendChild(xmlelem3);
			}
			XmlText xmltext = doc.CreateTextNode(func.Body);
			functionElement.AppendChild(xmltext);
			return functionElement;
		}

		private static XmlElement CreateUserOptionXML(UserOption option, XmlDocument doc)
		{
			string category = option.Category.Length == 0 ? "General" : option.Category;

			XmlElement optionElement = doc.CreateElement("option");
			AppendNewXmlElement(optionElement, "variablename", option.VariableName);
			AppendNewXmlElement(optionElement, "type", option.VarType.Name);
			AppendNewXmlElement(optionElement, "text", option.Text);
			AppendNewXmlElement(optionElement, "description", option.Description);
			AppendNewXmlElement(optionElement, "category", category.Replace(" ", "_"));
			AppendNewXmlElement(optionElement, "defaultvalue", option.DefaultValueFunctionBody);
			AppendNewXmlElement(optionElement, "defaultvalueisfunction", "true"); // This is always true
			AppendNewXmlElement(optionElement, "iteratorname", option.IteratorType == null ? "" : option.IteratorType.FullName);
			AppendNewXmlElement(optionElement, "validatorfunction", option.ValidatorFunctionBody);
			AppendNewXmlElement(optionElement, "displaytouserfunction", option.DisplayToUserFunctionBody);
			AppendNewXmlElement(optionElement, "resetpersession", option.ResetPerSession.ToString());

			// Values
			XmlElement valuesElement = AppendNewXmlElement(optionElement, "values", "");

			foreach (string optionValue in option.Values)
			{
				AppendNewXmlElement(valuesElement, "value", optionValue);
			}

			return optionElement;
		}

		/// <summary>
		/// Populates the project from the xml definition file
		/// </summary>
		/// <param name="project"></param>
		/// <param name="FilePath">The file to read the project definition from.</param>
		public static void ReadFromXml(Project project, string FilePath)
		{
			project.ClearUserOptions();
			project.Functions.Clear();
			XPathDocument doc = null;
			string ext = Path.GetExtension(FilePath);

			if (ext == ".st")
			{
				doc = new XPathDocument(FilePath);
			}
			else if (ext == ".stz")
			{
				// Delete all files in the temp folder
				if (Directory.Exists(Controller.TempPath))
				{
					Directory.Delete(Controller.TempPath, true);
				}
				Directory.CreateDirectory(Controller.TempPath);
				Utility.UnzipFile(FilePath, Controller.TempPath);

				string[] includedFiles = Directory.GetFiles(Controller.TempPath, "*");
				project.ClearIncludedFiles();

				foreach (string file in includedFiles)
				{
					if (!Utility.StringsAreEqual(Path.GetFileName(file), "definition.xml", false))
					{
						project.AddIncludedFile(new IncludedFile(Path.GetFileName(file)));
					}
				}
				string xmlPath = Path.Combine(Controller.TempPath, "definition.xml");

				// Backward compatability mpr
				StreamReader reader = new StreamReader(xmlPath);
				string definitionXml = reader.ReadToEnd();
				reader.Close();

				StreamWriter writer = File.CreateText(xmlPath);
				//writer.Write(Convert.Script.Replace(definitionXml));
				writer.Write(definitionXml);
				writer.Close();
				// mpr

				doc = new XPathDocument(xmlPath);
			}
			XPathNavigator rootNavigator = doc.CreateNavigator();
			XPathNavigator navigator = rootNavigator.SelectSingleNode(@"ROOT/config/project");
			XPathNavigator fileVersionNavigator = rootNavigator.SelectSingleNode(@"ROOT/@fileversion");
			int fileVersion = fileVersionNavigator != null ? fileVersionNavigator.ValueAsInt : 0;
			List<string> missingTypesMessages = new List<string>();

			#region Project details

			project.ProjectName = navigator.SelectSingleNode("name").Value;
			project.ProjectDescription = navigator.SelectSingleNode("description").Value;
			ReadXmlReferencedFiles(project, FilePath, rootNavigator);
			string compileFolderRelPath = navigator.SelectSingleNode("compilefile") == null ? "" : navigator.SelectSingleNode("compilefile").Value;
			compileFolderRelPath = project.GetFullPath(compileFolderRelPath);
			project.DebugProjectFile = navigator.SelectSingleNode("debugfile") == null ? "" : navigator.SelectSingleNode("debugfile").Value;
			project.TestGenerateDirectory = navigator.SelectSingleNode("testgeneratedirectory") == null ? "" : navigator.SelectSingleNode("testgeneratedirectory").Value;

			if (Directory.Exists(compileFolderRelPath))
			{
				compileFolderRelPath = Path.GetDirectoryName(compileFolderRelPath);
			}
			else
			{
				compileFolderRelPath = Path.GetDirectoryName(project.ProjectFileName);
			}
			project.CompileFolderName = compileFolderRelPath;
			//CompileFolderName = GetFullPath(CompileFolderName);
			project.Version = navigator.SelectSingleNode("version") == null ? "0.0.0" : navigator.SelectSingleNode("version").Value;

			project.Namespaces.Clear();
			project.Namespaces.AddRange(rootNavigator.SelectSingleNode(@"ROOT/namespaces").Value.Split(','))
				;
			#endregion

			//#region Default Value Functions
			//m_defaultValueFunctions = new List<DefaultValueFunction>();

			//foreach (XPathNavigator subNode in navigator.Select("defaultvaluefunctions/defaultvaluefunction"))
			//{
			//    ReadXmlDefaultValueFunctionNode(missingTypesMessages, subNode);
			//}
			//#endregion

			#region Functions
			foreach (XPathNavigator funcNode in rootNavigator.Select("/ROOT/function"))
			{
				ReadXmlFunctionNode(project, missingTypesMessages, funcNode);
			}
			project.SortFunctions();
			#endregion

			#region Outputs
			XPathNavigator outputNode = navigator.SelectSingleNode("rootoutput/rootfolder");
			project.RootOutput = new OutputFolder("ROOT", Guid.NewGuid().ToString());
			OutputFolder rootFolder = new OutputFolder("root", Guid.NewGuid().ToString());
			project.RootOutput.Folders.Add(rootFolder);
			project.ReadXmlFolderNode(ref rootFolder, outputNode);
			#endregion

			#region User options

			foreach (XPathNavigator subNode in navigator.Select("options/option"))
			{
				ReadXmlUserOptionNode(project, missingTypesMessages, subNode);
			}

			project.SortUserOptions();

			#endregion

			project.SetupDynamicfilesAndFolders();

			#region Clean up process
			// Remove DefaultValueFunctions which have no matching Function. This was required
			// when the implementation for DefaultValueFunctions was changed.
			for (int i = project.DefaultValueFunctions.Count - 1; i >= 0; i--)
			{
				if (project.FindFunction(project.DefaultValueFunctions[i].FunctionName, project.DefaultValueFunctions[i].ParameterTypes.ToList()) == null)
				{
					project.DefaultValueFunctions.RemoveAt(i);
				}
			}
			if (fileVersion == 1)
			{
				CleanupVersion1Projects(project);
			}
			if (missingTypesMessages.Count > 0)
			{
				StringBuilder sb = new StringBuilder();

				foreach (string message in missingTypesMessages)
				{
					sb.AppendLine("* " + message);
				}
				MessageBox.Show(Controller.Instance.MainForm, "A number of types could not be found. This is probably because one of the \nreferenced files used by this template has changed. Make a note of the changes \nand either remove them from the project or fix them:\n\n" + sb, "Unknown Types", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			#endregion
		}

		/// <summary>
		/// We moved DefaultValue, Validator and DisplayToUser function bodies into the UserOptions in version 2, 
		/// so we need to do a bit of clean-up of old projects.
		/// </summary>
		private static void CleanupVersion1Projects(Project project)
		{
			foreach (var userOption in project.UserOptions)
			{
				// DefaultValue function
				FunctionInfo func = project.FindFunctionSingle(userOption.DefaultValueFunctionBody);

				if (func != null)
				{
					userOption.DefaultValueFunctionBody = func.Body;
					project.DeleteFunction(func);
				}
				// DisplayToUser function
				func = project.FindFunctionSingle(userOption.DisplayToUserFunctionBody);

				if (func != null)
				{
					userOption.DisplayToUserFunctionBody = func.Body;
					project.DeleteFunction(func);
				}
				// Validator function
				func = project.FindFunctionSingle(userOption.ValidatorFunctionBody);

				if (func != null)
				{
					userOption.ValidatorFunctionBody = func.Body;
					project.DeleteFunction(func);
				}
			}
		}

		private static void ReadXmlReferencedFiles(Project project, string FilePath, XPathNavigator rootNavigator)
		{
			string[] refFiles = rootNavigator.SelectSingleNode(@"ROOT/referencedfiles").Value.Split(',');

			project.ClearReferences();

			for (int refFileCounter = 0; refFileCounter < refFiles.Length; refFileCounter++)
			{
				string[] refFileParts = refFiles[refFileCounter].Split('|');
				bool useInWorkbench = refFileParts.Length > 3 ? refFileParts[3] == "useInWorkbench=True" : true;
				string refFilePath = GetFullPathOfReferencedFile(project, refFileParts[0]);

				if (!File.Exists(refFilePath))
				{
					// Look in the template path
					string fileRefTestPath = Path.Combine(Path.GetDirectoryName(FilePath), refFilePath);

					if (File.Exists(fileRefTestPath))
					{
						refFilePath = fileRefTestPath;
					}
					else
					{
						// Look in the application path
						fileRefTestPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), refFilePath);

						if (File.Exists(fileRefTestPath))
						{
							refFilePath = fileRefTestPath;
						}
					}
				}
				if (refFileParts.Length == 2)
				{
					project.AddReferencedFile(new ReferencedFile(refFilePath, false, useInWorkbench));
				}
				else
				{
					project.AddReferencedFile(new ReferencedFile(refFilePath, false, useInWorkbench));
				}
			}
		}

		private static void ReadXmlUserOptionNode(Project project, ICollection<string> missingTypesMessages, XPathNavigator subNode)
		{
			string variableName = subNode.SelectSingleNode("variablename").Value;
			string category = subNode.SelectSingleNode("category").Value.Replace("_", " ");
			string varType = subNode.SelectSingleNode("type").Value;
			string text = subNode.SelectSingleNode("text").Value;
			string description = subNode.SelectSingleNode("description").Value;
			string defaultValue = subNode.SelectSingleNode("defaultvalue").Value;
			string IteratorType = subNode.SelectSingleNode("iteratorname").Value;
			string validatorFunction = subNode.SelectSingleNode("validatorfunction") != null ? subNode.SelectSingleNode("validatorfunction").Value : "";
			bool defaultValueIsFunction = subNode.SelectSingleNode("defaultvalueisfunction") != null ? subNode.SelectSingleNode("defaultvalueisfunction").Value == "true" : false;
			string displayToUserFunction = subNode.SelectSingleNode("displaytouserfunction") != null ? subNode.SelectSingleNode("displaytouserfunction").Value : String.Format("DisplayToUser_UserOption_{0}", variableName);
			bool refreshPerSession = subNode.SelectSingleNode("resetpersession") != null ? Boolean.Parse(subNode.SelectSingleNode("resetpersession").Value) : false;

			if (category.Length == 0) { category = "General"; }

			Type realVarType = Project.Instance.GetTypeFromReferencedAssemblies(varType, true);

			if (realVarType == null)
			{
				missingTypesMessages.Add(String.Format("UserOption ({0}): The type of this UserOption ({1}) cannot be found in the referenced files. It will be changed to type 'string'.", variableName, varType));
				realVarType = typeof(string);
			}
			Type iteratorType = null;

			if (IteratorType.Length > 0 && IteratorType != "No iteration")
			{
				iteratorType = Project.Instance.GetTypeFromReferencedAssemblies(IteratorType, false);

				if (iteratorType == null)
				{
					missingTypesMessages.Add(String.Format("UserOption ({0}): The iterator-type ({1}) of this UserOption cannot be found in the referenced files. It will be changed to have no iterator.", variableName, IteratorType));
					iteratorType = null;
				}
			}
			UserOption newUserOption = new UserOption(variableName, category, realVarType, text, description, new string[0], defaultValue, iteratorType, validatorFunction, displayToUserFunction, refreshPerSession);
			project.AddUserOption(newUserOption);

			XPathNodeIterator valueNodes = subNode.Select("values/value");
			newUserOption.Values.Clear();

			int userOptionValueCounter = -1;

			foreach (XPathNavigator valueNode in valueNodes)
			{
				userOptionValueCounter++;
				newUserOption.Values.Add(valueNode.Value);
			}
		}

		private static void ReadXmlFunctionNode(Project project, ICollection<string> missingTypesMessages, XPathNavigator funcNode)
		{
			string funcName = funcNode.SelectSingleNode("@name").Value;
			SyntaxEditorHelper.ScriptLanguageTypes scriptLanguage = funcNode.SelectSingleNode("@scriptlanguage") == null ? SyntaxEditorHelper.ScriptLanguageTypes.CSharp : (SyntaxEditorHelper.ScriptLanguageTypes)Enum.Parse(typeof(SyntaxEditorHelper.ScriptLanguageTypes), funcNode.SelectSingleNode("@scriptlanguage").Value, true);
			string description = funcNode.SelectSingleNode("@description") == null ? "" : funcNode.SelectSingleNode("@description").Value;
			string category = funcNode.SelectSingleNode("@category") == null ? "" : funcNode.SelectSingleNode("@category").Value;
			bool isTemplateFunction = false;

			if (funcNode.SelectSingleNode("@istemplatefunction") != null)
			{
				isTemplateFunction = Boolean.Parse(funcNode.SelectSingleNode("@istemplatefunction").Value);
			}
			string templateReturnLanguage = isTemplateFunction ? funcNode.SelectSingleNode("@returntype").Value : "";

			XPathNavigator tempnode = funcNode.SelectSingleNode("@isextensionmethod");
			bool isExtensionMethod = tempnode == null ? false : Boolean.Parse(tempnode.Value);
			string extendedtype = isExtensionMethod ? funcNode.SelectSingleNode("@extendedtype").Value : "";

			Type dataType;

			if (isTemplateFunction)
			{
				dataType = typeof(string);
			}
			else
			{
				if (funcNode.SelectSingleNode("@returntype").Value != "void")
				{
					dataType = Project.Instance.GetTypeFromReferencedAssemblies(funcNode.SelectSingleNode("@returntype").Value, false);

					if (dataType == null)
					{
						missingTypesMessages.Add(String.Format("Function ({0}): The return-type ({1}) cannot be found in the referenced files. The return-type will be changed to 'void'.", funcName, funcNode.SelectSingleNode("@returntype").Value));
					}
				}
				else
				{
					dataType = null;
				}
			}
			project.Functions.Add(new FunctionInfo(funcName, dataType, funcNode.Value, isTemplateFunction, scriptLanguage, description, templateReturnLanguage, category, isExtensionMethod, extendedtype));
			XPathNodeIterator paramNodes = funcNode.Select("parameter");
			project.Functions[project.Functions.Count - 1].Parameters.Clear();

			if (paramNodes.Count > 0)
			{
				foreach (XPathNavigator paramNode in paramNodes)
				{
					Type paramType = Project.Instance.GetTypeFromReferencedAssemblies(paramNode.SelectSingleNode("@type").Value, false);

					if (paramType == null)
					{
						missingTypesMessages.Add(String.Format("Function ({0}): The parameter named '{1}' of type ({2}) cannot be found in the referenced files. It will be changed to type 'object'.", funcName, paramNode.SelectSingleNode("@name").Value, paramNode.SelectSingleNode("@type").Value));
						paramType = typeof(object);
					}
					ParamInfo param = new ParamInfo(paramNode.SelectSingleNode("@name").Value, paramType);
					param.Modifiers = paramNode.SelectSingleNode("@modifiers") != null ? paramNode.SelectSingleNode("@modifiers").Value : "";
					project.Functions[project.Functions.Count - 1].Parameters.Add(param);
				}
			}
		}

		/// <summary>
		/// Gets the full path of a referenced file, by looking in all the obvious places. Returns 
		/// just the filename if the full path can't be determined.
		/// </summary>
		/// <param name="relativePath"></param>
		/// <returns></returns>
		private static string GetFullPathOfReferencedFile(Project project, string relativePath)
		{
			string refFilePath = project.GetFullPath(relativePath);

			if (!File.Exists(refFilePath))
			{
				string refFileName = Path.GetFileName(refFilePath);

				// Can we find the file in the same folder as the current project file (.stz)?
				string pathToCheck = Path.Combine(Path.GetDirectoryName(project.ProjectFileName), refFileName);

				if (File.Exists(pathToCheck))
				{
					refFilePath = pathToCheck;
				}
				else
				{
					// Can we find it in the application path?
					pathToCheck = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), refFileName);

					if (File.Exists(pathToCheck))
					{
						refFilePath = pathToCheck;
					}
					else
					{
						// We still can't find it anywhere, so let's rather make the path blank, rather 
						// than a weird non-existant one from the development environment! 
						refFilePath = refFileName;
					}
				}
			}
			return refFilePath;
		}
	}
}
