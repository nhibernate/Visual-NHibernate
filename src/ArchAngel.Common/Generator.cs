using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using ArchAngel.Interfaces;
using ArchAngel.Interfaces.Template;
using Slyce.Common;
using Slyce.IntelliMerge.Controller;

namespace ArchAngel.Common
{
	public class Generator
	{
		#region Inner Classes
		public class DebugPos
		{
			public DebugPos(int line, int column, string functionName)
			{
				Line = line;
				Column = column;
				FunctionName = functionName;
			}
			public string FunctionName { get; set; }
			public int Line { get; set; }
			public int Column { get; set; }
		}

		private class Map
		{
			public Map(string functionName, string debugLine)
			{
				FunctionName = functionName;
				DebugLine = debugLine;
			}

			public string FunctionName { get; set; }
			public string DebugLine { get; set; }
		}

		private class FilePayload
		{
			public FilePayload(
				string functionNameInCode,
				string name,
				string code,
				string paramString,
				ArchAngel.Interfaces.Template.File file,
				Dictionary<int, Map> lineMaps)
			{
				FunctionNameInCode = functionNameInCode;
				Name = name;
				Code = code;
				ParamString = paramString;
				File = file;
				LineMaps = lineMaps;
			}

			public string Name { get; set; }
			public string FunctionNameInCode { get; set; }
			public string Code { get; set; }
			public string ParamString { get; set; }
			public MethodInfo GetNameMethod { get; set; }
			public MethodInfo GetBodyMethod { get; set; }
			public ArchAngel.Interfaces.Template.File File { get; set; }
			public Dictionary<int, Map> LineMaps { get; set; }
		}

		private class StaticFilePayload
		{
			public StaticFilePayload(
				string functionNameInCode,
				string name,
				string code,
				string paramString,
				ArchAngel.Interfaces.Template.StaticFile file,
				Dictionary<int, Map> lineMaps)
			{
				FunctionNameInCode = functionNameInCode;
				Name = name;
				Code = code;
				ParamString = paramString;
				File = file;
				LineMaps = lineMaps;
			}

			public string Name { get; set; }
			public string FunctionNameInCode { get; set; }
			public string Code { get; set; }
			public string ParamString { get; set; }
			public MethodInfo GetNameMethod { get; set; }
			public MethodInfo PreWriteMethod { get; set; }
			public ArchAngel.Interfaces.Template.StaticFile File { get; set; }
			public Dictionary<int, Map> LineMaps { get; set; }
		}

		private class FolderPayload
		{
			public FolderPayload(string functionNameInCode, string name, string code, string paramString, ArchAngel.Interfaces.Template.Folder folder)
			{
				FunctionNameInCode = functionNameInCode;
				Name = name;
				Code = code;
				ParamString = paramString;
				Folder = folder;
			}

			public string Name { get; set; }
			public string FunctionNameInCode { get; set; }
			public string Code { get; set; }
			public string ParamString { get; set; }
			public MethodInfo GetNameMethod { get; set; }
			public ArchAngel.Interfaces.Template.Folder Folder { get; set; }
		}
		#endregion

		private Dictionary<int, FilePayload> FileFunctionLookups = new Dictionary<int, FilePayload>();
		private Dictionary<int, FolderPayload> FolderFunctionLookups = new Dictionary<int, FolderPayload>();
		private Dictionary<int, StaticFilePayload> StaticFileFunctionLookups = new Dictionary<int, StaticFilePayload>();
		private int FunctionCounter = 0;
		public Assembly CurrentAssembly = null;
		public ArchAngel.Interfaces.Scripting.NHibernate.Model.IProject ScriptProject;
		private readonly ITemplateLoader _Loader;
		private readonly ITaskProgressHelper<GenerateFilesProgress> _ProgressHelper;
		private int NumFiles = 0;
		private PropertyInfo _SkipFileProperty;
		private PropertyInfo _CurrentFilePathProperty;
		public MethodInfo _GetSetProjectMethod;
		public MethodInfo _ConvertMethod;
		private List<FilenameInfo> AllTextFilenames = new List<FilenameInfo>();
		public static int OffsetLinesForFile = -1;
		public string TargetFolder = "";

		public Generator()
		{
		}

		public Generator(
			ITaskProgressHelper<GenerateFilesProgress> progressHelper,
			ITemplateLoader loader)
		{
			_Loader = loader;
			_ProgressHelper = progressHelper;
		}

		public void ClearAllDebugLines()
		{
			AllDebugLines.Clear();
		}

		public Assembly CompileBaseAssembly(List<string> referencedAssemblyPaths, List<string> embeddedResourcesPaths, string outputFilename)
		{
			FunctionCounter = 0;
			FileFunctionLookups = new Dictionary<int, FilePayload>();
			FolderFunctionLookups = new Dictionary<int, FolderPayload>();
			StaticFileFunctionLookups = new Dictionary<int, StaticFilePayload>();

			List<Slyce.Common.Scripter.FileToCompile> codeBodies = new List<Slyce.Common.Scripter.FileToCompile>();
			codeBodies.Add(new Slyce.Common.Scripter.FileToCompile("Main", GetFunctionLookupClass(), "Main"));

			List<System.CodeDom.Compiler.CompilerError> errors;
			Assembly assembly = Slyce.Common.Scripter.CompileCode(codeBodies, referencedAssemblyPaths, embeddedResourcesPaths, out errors, outputFilename);
			return assembly;
		}

		public Assembly CompileCombinedAssembly(
					ArchAngel.Interfaces.Template.TemplateProject templateProject,
					List<string> referencedAssemblyPaths,
					List<string> embeddedResourcesPaths,
					out List<System.CodeDom.Compiler.CompilerError> errors,
					bool forExecution,
					string assemblyPath)
		{
			FunctionCounter = 0;
			FileFunctionLookups = new Dictionary<int, FilePayload>();
			FolderFunctionLookups = new Dictionary<int, FolderPayload>();

			if (templateProject != null)
			{
				foreach (Folder subFolder in templateProject.OutputFolder.Folders)
					AddFolderToAssembly(subFolder, "");

				foreach (ArchAngel.Interfaces.Template.File file in templateProject.OutputFolder.Files)
					AddFileToAssembly(file, "");

				foreach (ArchAngel.Interfaces.Template.StaticFile staticFile in templateProject.OutputFolder.StaticFiles)
					AddStaticFileToAssembly(staticFile, "");
			}
			List<Slyce.Common.Scripter.FileToCompile> codeBodies = new List<Slyce.Common.Scripter.FileToCompile>();
			codeBodies.Add(new Slyce.Common.Scripter.FileToCompile("Main", GetFunctionLookupClass(), "Main"));

			foreach (var key in FileFunctionLookups.Keys)
				codeBodies.Add(new Slyce.Common.Scripter.FileToCompile("File_" + key.ToString(), FileFunctionLookups[key].Code, FileFunctionLookups[key].File));

			foreach (var key in FolderFunctionLookups.Keys)
				codeBodies.Add(new Slyce.Common.Scripter.FileToCompile("Folder_" + key.ToString(), FolderFunctionLookups[key].Code, FolderFunctionLookups[key].Folder));

			foreach (var key in StaticFileFunctionLookups.Keys)
				codeBodies.Add(new Slyce.Common.Scripter.FileToCompile("StaticFile_" + key.ToString(), StaticFileFunctionLookups[key].Code, StaticFileFunctionLookups[key].File));

			CurrentAssembly = Slyce.Common.Scripter.CompileCode(codeBodies, referencedAssemblyPaths, embeddedResourcesPaths, out errors, forExecution, assemblyPath);
			return CurrentAssembly;
		}

		public int WriteFiles(
			ProjectFileTreeNode parentNode,
			ArchAngel.Interfaces.Scripting.NHibernate.Model.IProject project,
			string targetFolder,
			ArchAngel.Interfaces.Template.TemplateProject templateProject,
			out List<FilenameInfo> duplicateFiles)
		{


			if (!Directory.Exists(targetFolder))
				throw new FileNotFoundException("Output folder doesn't exist: " + targetFolder, targetFolder);

			////////////////////////////////////////
			TargetFolder = targetFolder;

			SharedData.IsBusyGenerating = true;
			SharedData.CurrentProject.StartNewFileGenerationRun();

			// Reset the Template before the File name validation run.
			_Loader.CallTemplateFunction(TemplateHelper.ClearTemplateCacheFunctionName);

			// Run the pre generation template function.

			var data = new PreGenerationData
			{
				OutputFolder = SharedData.CurrentProject.ProjectSettings.OutputPath,
				OverwriteFiles = SharedData.CurrentProject.ProjectSettings.OverwriteFiles
			};

			foreach (var uo in SharedData.CurrentProject.Options.Where(o => o.IsVirtualProperty == false))
			{
				var optionValue = SharedData.CurrentProject.GetUserOption(uo.VariableName);
				data.UserOptions.Add(uo.VariableName, optionValue);
			}
			foreach (var provider in SharedData.CurrentProject.Providers)
			{
				ArchAngel.Interfaces.ProviderInfo[] otherProviders = new ProviderInfo[SharedData.CurrentProject.Providers.Count];
				SharedData.CurrentProject.Providers.CopyTo(otherProviders);
				data.OtherProviderInfos = otherProviders.ToList();
				data.OtherProviderInfos.Remove(provider);
				_Loader.CallPreGenerationInitialisationFunction(provider, data);
			}
			((ProjectFileTree)parentNode).TreeRestructuring = true;
			((ProjectFileTree)parentNode).Clear();
			ArchAngel.Interfaces.SharedData.CurrentProject.ScriptProject.OutputFolder = project.OutputFolder;
			ArchAngel.Interfaces.SharedData.CurrentProject.ScriptProject.TempFolder = project.TempFolder;
			ArchAngel.Interfaces.SharedData.CurrentProject.InitialiseScriptObjects();
			SetProjectInCode(ArchAngel.Interfaces.SharedData.CurrentProject.ScriptProject);
			project = ArchAngel.Interfaces.SharedData.CurrentProject.ScriptProject;
			//SetProjectInCode(project);

			////////////////////////////////////////

			// TODO: Check for duplicate files and folders
			ScriptProject = project;
			NumFiles = 0;
			//SkipFileProperty = CurrentAssembly.GetType("Slyce.FunctionRunner.FunctionProcessor").GetProperty("SkipCurrentFile");

			foreach (Folder subFolder in templateProject.OutputFolder.Folders)
				ProcessFolder(subFolder, targetFolder, parentNode);

			foreach (ArchAngel.Interfaces.Template.File file in templateProject.OutputFolder.Files)
				ProcessFile(file, targetFolder, parentNode, null);

			foreach (ArchAngel.Interfaces.Template.StaticFile staticFile in templateProject.OutputFolder.StaticFiles)
				ProcessStaticFile(staticFile, targetFolder, parentNode);

			targetFolder += Path.DirectorySeparatorChar.ToString();
			duplicateFiles = new List<FilenameInfo>();

			foreach (var f in AllTextFilenames)
				f.RelativePath = f.RelativePath.Replace(targetFolder, "");

			foreach (var group in AllTextFilenames.GroupBy(n => n.RelativePath).Where(g => g.Count() > 1))
				duplicateFiles.AddRange(group.Select(g => g));

			return NumFiles;
		}

		private PropertyInfo SkipFileProperty
		{
			get
			{
				if (_SkipFileProperty == null)
					_SkipFileProperty = CurrentAssembly.GetType("Slyce.FunctionRunner.FunctionProcessor").GetProperty("SkipCurrentFile");

				return _SkipFileProperty;
			}
		}

		private PropertyInfo CurrentFilePathProperty
		{
			get
			{
				if (_CurrentFilePathProperty == null)
					_CurrentFilePathProperty = CurrentAssembly.GetType("Slyce.FunctionRunner.FunctionProcessor").GetProperty("CurrentFilePath");

				return _CurrentFilePathProperty;
			}
		}

		private static Dictionary<string, Dictionary<int, Map>> AllDebugLines = new Dictionary<string, Dictionary<int, Map>>();

		public static DebugPos GetDebugPos(string functionId, int compileLine, int compileColumn, string errorText)
		{
			Map map;

			if (!AllDebugLines.ContainsKey(functionId))
				throw new Exception(string.Format("Function '{0}' not found in AllDebugLines. Error Line: {1}, Error Column: {2}. Error message: {3}.", functionId, compileLine, compileColumn, errorText));
			else
			{
				if (AllDebugLines[functionId].ContainsKey(compileLine))
					map = AllDebugLines[functionId][compileLine];
				else
					map = AllDebugLines[functionId][AllDebugLines[functionId].Count - 1];
			}
			return new DebugPos(int.Parse(map.DebugLine), compileColumn, map.FunctionName);
		}

		private Dictionary<int, Map> GetLineMaps(string code)
		{
			Dictionary<int, Map> maps = new Dictionary<int, Map>();
			string[] lines = code.Split('\n');
			int start;
			int end;
			int debugLength = @"/*DEBUG:".Length;
			string currentDebugLine = "0";
			string currentFunctionName = "";
			List<int> queuedLines = new List<int>();

			for (int i = 0; i < lines.Length; i++)
			{
				start = lines[i].IndexOf(@"/*DEBUG:");

				if (start >= 0)
				{
					start += debugLength;
					end = lines[i].IndexOf(":", start);
					currentFunctionName = lines[i].Substring(start, end - start).Split('.')[1];
					start = end + 1;
					end = lines[i].IndexOf("*/", start);
					currentDebugLine = lines[i].Substring(start, end - start);

					foreach (int queuedLine in queuedLines)
						maps.Add(queuedLine, new Map(currentFunctionName, currentDebugLine));

					queuedLines.Clear();
					maps.Add(i + 1, new Map(currentFunctionName, currentDebugLine));
				}
				else
					queuedLines.Add(i + 1);
			}
			return maps;
		}

		private void ProcessFolder(Folder folder, string path, ProjectFileTreeNode parentNode)
		{
			string folderPath = "";
			ProjectFileTreeNode folderNode = null;

			switch (folder.Iterator)
			{
				case ArchAngel.Interfaces.Template.IteratorTypes.None:
					CreateFolder(folder, path, parentNode, ref folderPath, ref folderNode, null);
					break;
				case ArchAngel.Interfaces.Template.IteratorTypes.Entity:
					foreach (var entity in ScriptProject.Entities)
						CreateFolder(folder, path, parentNode, ref folderPath, ref folderNode, entity);

					break;
				case ArchAngel.Interfaces.Template.IteratorTypes.Table:
					foreach (var table in ScriptProject.Tables)
						CreateFolder(folder, path, parentNode, ref folderPath, ref folderNode, table);

					break;
				case ArchAngel.Interfaces.Template.IteratorTypes.Column:
					foreach (var column in ScriptProject.Tables.Select(t => t.Columns))
						CreateFolder(folder, path, parentNode, ref folderPath, ref folderNode, column);

					break;
				case ArchAngel.Interfaces.Template.IteratorTypes.Component:
					foreach (var component in ScriptProject.Components)
						CreateFolder(folder, path, parentNode, ref folderPath, ref folderNode, component);

					break;
				default:
					throw new NotImplementedException("This iterator not handled yet: " + folder.Iterator.ToString());
			}
		}

		private void CreateFolder(Folder folder, string path, ProjectFileTreeNode parentNode, ref string folderPath, ref ProjectFileTreeNode folderNode, object iteratorObject)
		{
			string folderName = GetFolderName(folder.ID, iteratorObject);
			folderPath = Utility.PathCombine(path, folderName);

			if (!Directory.Exists(folderPath))
				Directory.CreateDirectory(folderPath);

			folderNode = parentNode.AddChildNode(folderName);
			folderNode.IsFolder = true;

			foreach (Folder subFolder in folder.Folders)
				ProcessFolder(subFolder, folderPath, folderNode);

			foreach (ArchAngel.Interfaces.Template.File file in folder.Files)
				ProcessFile(file, folderPath, folderNode, iteratorObject);

			foreach (ArchAngel.Interfaces.Template.StaticFile staticFile in folder.StaticFiles)
				ProcessStaticFile(staticFile, folderPath, folderNode);
		}

		private void ProcessFile(ArchAngel.Interfaces.Template.File file, string path, ProjectFileTreeNode parentNode, object parentFolderIteratorObject)
		{
			string filePath;
			string relativeFilePath = null;
			string body;
			string fileName;
			TextFileInformation fileInfo;
			bool skipFile = false;

			switch (file.Iterator)
			{
				case ArchAngel.Interfaces.Template.IteratorTypes.None:
					try
					{
						fileName = GetFileName(file.Id, null);
					}
					catch (Exception ex)
					{
						fileName = "ERROR";
						filePath = Utility.PathCombine(path, fileName);


						relativeFilePath = filePath.Replace(TargetFolder, "").TrimStart(Path.DirectorySeparatorChar);

						fileInfo = new TextFileInformation()
						{
							RelativeFilePath = relativeFilePath,
							NewGenFile = new TextFile(filePath, false),
							Encoding = file.Encoding
						};
						ProcessFileGenerationException(parentNode, file.Id, fileName, fileInfo, ex);
						NumFiles++;
						return;
					}
					filePath = Utility.PathCombine(path, fileName);
					relativeFilePath = filePath.Replace(TargetFolder, "").TrimStart(Path.DirectorySeparatorChar);

					fileInfo = new TextFileInformation()
					{
						RelativeFilePath = relativeFilePath,
						NewGenFile = new TextFile(filePath, false),
						Encoding = file.Encoding
					};
					try
					{
						CopyUserFileToGenerationLocation(filePath, fileInfo);
						CurrentFilePathProperty.SetValue(null, filePath, null);
						body = Utility.StandardizeLineBreaks(GetFileBody(file.Id, null, out skipFile), Utility.LineBreaks.Windows);
						string newFilePath = (string)CurrentFilePathProperty.GetValue(null, null);

						if (newFilePath != filePath)
						{
							//filePath = newFilePath;
							//relativeFilePath = filePath.Replace(TargetFolder, "").TrimStart(Path.DirectorySeparatorChar);
							fileInfo = new TextFileInformation()
							{
								RelativeFilePath = relativeFilePath,
								NewGenFile = new TextFile(filePath, false),
								Encoding = file.Encoding
							};
						}
					}
					catch (Exception ex)
					{
						ProcessFileGenerationException(parentNode, file.Id, fileName, fileInfo, ex);
						return;
					}
					if (!skipFile)
					{
						Slyce.Common.Utility.DeleteFileBrute(filePath);
						System.IO.File.WriteAllText(filePath, body);
						parentNode.AddChildNode(fileInfo, fileName);
						AllTextFilenames.Add(new FilenameInfo(fileName, file.Name, file.Iterator, FilenameInfo.FilenameTypes.GeneratedFile) { RelativePath = relativeFilePath });
						NumFiles++;
					}
					break;
				case ArchAngel.Interfaces.Template.IteratorTypes.Entity:
					List<ArchAngel.Interfaces.Scripting.NHibernate.Model.IEntity> entities = new List<Interfaces.Scripting.NHibernate.Model.IEntity>();

					if (parentFolderIteratorObject == null)
						entities = ScriptProject.Entities;
					else if (parentFolderIteratorObject is Interfaces.Scripting.NHibernate.Model.IEntity)
					{
						entities = new List<Interfaces.Scripting.NHibernate.Model.IEntity>();
						entities.Add((ArchAngel.Interfaces.Scripting.NHibernate.Model.IEntity)parentFolderIteratorObject);
					}
					else
						throw new InvalidDataException(string.Format("File iterator is 'Entity' but parent folder is '{0}'. Iterator can therefore only be: 'Entity', 'None'.", parentFolderIteratorObject.GetType().Name));

					foreach (var entity in entities)
					{
						try
						{
							fileName = GetFileName(file.Id, entity);
						}
						catch (Exception ex)
						{
							fileName = GetNodeDisplayText(file.Name, string.Format("[{0}]", entity.Name));
							filePath = Utility.PathCombine(path, fileName);


							relativeFilePath = filePath.Replace(TargetFolder, "").TrimStart(Path.DirectorySeparatorChar);

							fileInfo = new TextFileInformation()
							{
								RelativeFilePath = relativeFilePath,
								NewGenFile = new TextFile(filePath, false),
								Encoding = file.Encoding
							};
							ProcessFileGenerationException(parentNode, file.Id, fileName, fileInfo, ex);
							NumFiles++;
							continue;
						}
						filePath = Utility.PathCombine(path, fileName);

						relativeFilePath = filePath.Replace(TargetFolder, "").TrimStart(Path.DirectorySeparatorChar);

						fileInfo = new TextFileInformation()
						{
							RelativeFilePath = relativeFilePath,
							NewGenFile = new TextFile(filePath, false),
							Encoding = file.Encoding
						};
						try
						{
							CopyUserFileToGenerationLocation(filePath, fileInfo);
							CurrentFilePathProperty.SetValue(null, filePath, null);
							body = Utility.StandardizeLineBreaks(GetFileBody(file.Id, entity, out skipFile), Utility.LineBreaks.Windows);
							string newFilePath = (string)CurrentFilePathProperty.GetValue(null, null);

							if (newFilePath != filePath)
							{
								//filePath = newFilePath;
								//relativeFilePath = filePath.Replace(TargetFolder, "").TrimStart(Path.DirectorySeparatorChar);

								fileInfo = new TextFileInformation()
								{
									RelativeFilePath = relativeFilePath,
									NewGenFile = new TextFile(filePath, false),
									Encoding = file.Encoding
								};
							}
						}
						catch (Exception ex)
						{
							ProcessFileGenerationException(parentNode, file.Id, fileName, fileInfo, ex);
							continue;
						}
						if (!skipFile)
						{
							Slyce.Common.Utility.DeleteFileBrute(filePath);
							System.IO.File.WriteAllText(filePath, body);
							parentNode.AddChildNode(fileInfo, fileName);
							AllTextFilenames.Add(new FilenameInfo(fileName, file.Name, file.Iterator, FilenameInfo.FilenameTypes.GeneratedFile) { RelativePath = relativeFilePath });
							NumFiles++;
						}
					}
					break;
				case ArchAngel.Interfaces.Template.IteratorTypes.Component:
					foreach (var component in ScriptProject.Components)
					{
						try
						{
							fileName = GetFileName(file.Id, component);
						}
						catch (Exception ex)
						{
							fileName = "ERROR";
							filePath = Utility.PathCombine(path, fileName);


							relativeFilePath = filePath.Replace(TargetFolder, "").TrimStart(Path.DirectorySeparatorChar);

							fileInfo = new TextFileInformation()
							{
								RelativeFilePath = relativeFilePath,
								NewGenFile = new TextFile(filePath, false),
								Encoding = file.Encoding
							};
							ProcessFileGenerationException(parentNode, file.Id, fileName, fileInfo, ex);
							NumFiles++;
							continue;
						}
						filePath = Utility.PathCombine(path, fileName);


						relativeFilePath = filePath.Replace(TargetFolder, "").TrimStart(Path.DirectorySeparatorChar);

						fileInfo = new TextFileInformation()
						{
							RelativeFilePath = relativeFilePath,
							NewGenFile = new TextFile(filePath, false),
							Encoding = file.Encoding
						};
						try
						{
							CopyUserFileToGenerationLocation(filePath, fileInfo);
							CurrentFilePathProperty.SetValue(null, filePath, null);
							body = Utility.StandardizeLineBreaks(GetFileBody(file.Id, component, out skipFile), Utility.LineBreaks.Windows);
							string newFilePath = (string)CurrentFilePathProperty.GetValue(null, null);

							if (newFilePath != filePath)
							{
								//filePath = newFilePath;


								//relativeFilePath = filePath.Replace(TargetFolder, "").TrimStart(Path.DirectorySeparatorChar);

								fileInfo = new TextFileInformation()
								{
									RelativeFilePath = relativeFilePath,
									NewGenFile = new TextFile(filePath, false),
									Encoding = file.Encoding
								};
							}
						}
						catch (Exception ex)
						{
							ProcessFileGenerationException(parentNode, file.Id, fileName, fileInfo, ex);
							continue;
						}
						if (!skipFile)
						{
							Slyce.Common.Utility.DeleteFileBrute(filePath);
							System.IO.File.WriteAllText(filePath, body);
							parentNode.AddChildNode(fileInfo, fileName);
							AllTextFilenames.Add(new FilenameInfo(fileName, file.Name, file.Iterator, FilenameInfo.FilenameTypes.GeneratedFile) { RelativePath = relativeFilePath });
							NumFiles++;
						}
					}
					break;
				case ArchAngel.Interfaces.Template.IteratorTypes.Table:
					List<ArchAngel.Interfaces.Scripting.NHibernate.Model.ITable> tables = new List<Interfaces.Scripting.NHibernate.Model.ITable>();

					if (parentFolderIteratorObject == null)
						tables = ScriptProject.Tables;
					else if (parentFolderIteratorObject is Interfaces.Scripting.NHibernate.Model.ITable)
					{
						tables = new List<Interfaces.Scripting.NHibernate.Model.ITable>();
						tables.Add((ArchAngel.Interfaces.Scripting.NHibernate.Model.ITable)parentFolderIteratorObject);
					}
					else
						throw new InvalidDataException(string.Format("File iterator is 'Table' but parent folder is '{0}'. Iterator can therefore only be: 'Table', 'None'.", parentFolderIteratorObject.GetType().Name));

					foreach (var table in tables)
					{
						try
						{
							fileName = GetFileName(file.Id, table);
						}
						catch (Exception ex)
						{
							fileName = "ERROR";
							filePath = Utility.PathCombine(path, fileName);


							relativeFilePath = filePath.Replace(TargetFolder, "").TrimStart(Path.DirectorySeparatorChar);

							fileInfo = new TextFileInformation()
							{
								RelativeFilePath = relativeFilePath,
								NewGenFile = new TextFile(filePath, false),
								Encoding = file.Encoding
							};
							ProcessFileGenerationException(parentNode, file.Id, fileName, fileInfo, ex);
							NumFiles++;
							continue;
						}
						filePath = Utility.PathCombine(path, fileName);


						relativeFilePath = filePath.Replace(TargetFolder, "").TrimStart(Path.DirectorySeparatorChar);

						fileInfo = new TextFileInformation()
						{
							RelativeFilePath = relativeFilePath,
							NewGenFile = new TextFile(filePath, false),
							Encoding = file.Encoding
						};
						try
						{
							CopyUserFileToGenerationLocation(filePath, fileInfo);
							CurrentFilePathProperty.SetValue(null, filePath, null);
							body = Utility.StandardizeLineBreaks(GetFileBody(file.Id, table, out skipFile), Utility.LineBreaks.Windows);
							string newFilePath = (string)CurrentFilePathProperty.GetValue(null, null);

							if (newFilePath != filePath)
							{
								//filePath = newFilePath;


								//relativeFilePath = filePath.Replace(TargetFolder, "").TrimStart(Path.DirectorySeparatorChar);

								fileInfo = new TextFileInformation()
								{
									RelativeFilePath = relativeFilePath,
									NewGenFile = new TextFile(filePath, false),
									Encoding = file.Encoding
								};
							}
						}
						catch (Exception ex)
						{
							ProcessFileGenerationException(parentNode, file.Id, fileName, fileInfo, ex);
							continue;
						}
						if (!skipFile)
						{
							Slyce.Common.Utility.DeleteFileBrute(filePath);
							System.IO.File.WriteAllText(filePath, body);
							parentNode.AddChildNode(fileInfo, fileName);
							AllTextFilenames.Add(new FilenameInfo(fileName, file.Name, file.Iterator, FilenameInfo.FilenameTypes.GeneratedFile) { RelativePath = relativeFilePath });
							NumFiles++;
						}
					}
					break;
				default:
					throw new NotImplementedException("This iterator not handled yet: " + file.Iterator.ToString());
			}
		}

		private static void CopyUserFileToGenerationLocation(string genFilePath, BinaryFileInformation fileInfo)
		{
			CopyUserFileToGenerationLocation(genFilePath, fileInfo.RelativeFilePath);
		}

		private static void CopyUserFileToGenerationLocation(string genFilePath, TextFileInformation fileInfo)
		{
			CopyUserFileToGenerationLocation(genFilePath, fileInfo.RelativeFilePath);
		}

		private static void CopyUserFileToGenerationLocation(string genFilePath, string relativeFilePath)
		{
			// Copy the original file into the target location before running the script
			string originalFile = Path.Combine(ArchAngel.Interfaces.SharedData.CurrentProject.ScriptProject.OutputFolder, relativeFilePath);

			if (System.IO.File.Exists(originalFile))
			{
				Slyce.Common.Utility.DeleteFileBrute(genFilePath);
				System.IO.File.Copy(originalFile, genFilePath);
			}
		}

		private void ProcessStaticFile(ArchAngel.Interfaces.Template.StaticFile staticFile, string path, ProjectFileTreeNode parentNode)
		{
			string filePath;
			string relativeFilePath;
			string fileName;
			BinaryFileInformation fileInfo;
			bool skipFile = false;

			switch (staticFile.Iterator)
			{
				case ArchAngel.Interfaces.Template.IteratorTypes.None:
					try
					{
						fileName = GetStaticFileName(staticFile.Id, null);
					}
					catch (Exception ex)
					{
						fileName = "ERROR";
						filePath = Utility.PathCombine(path, fileName);
						relativeFilePath = filePath.Replace(TargetFolder, "").TrimStart(Path.DirectorySeparatorChar);
						fileInfo = new BinaryFileInformation()
						{
							RelativeFilePath = relativeFilePath,
							NewGenFile = new BinaryFile(filePath, false)
						};
						ProcessFileGenerationException(parentNode, staticFile.Id, fileName, fileInfo, ex);
						NumFiles++;
						return;
					}
					filePath = Utility.PathCombine(path, fileName);
					relativeFilePath = filePath.Replace(TargetFolder, "").TrimStart(Path.DirectorySeparatorChar);
					fileInfo = new BinaryFileInformation()
					{
						RelativeFilePath = relativeFilePath,
						NewGenFile = new BinaryFile(filePath, false)
					};
					try
					{
						CopyUserFileToGenerationLocation(filePath, fileInfo);
						CurrentFilePathProperty.SetValue(null, filePath, null);
						CallStaticFilePreWriteFunction(staticFile.Id, null, out skipFile);
						string newFilePath = (string)CurrentFilePathProperty.GetValue(null, null);

						if (newFilePath != filePath)
						{
							//filePath = newFilePath;
							relativeFilePath = filePath.Replace(TargetFolder, "").TrimStart(Path.DirectorySeparatorChar);
							fileInfo = new BinaryFileInformation()
							{
								RelativeFilePath = relativeFilePath,
								NewGenFile = new BinaryFile(filePath, false)
							};
						}
					}
					catch (Exception ex)
					{
						ProcessFileGenerationException(parentNode, staticFile.Id, fileName, fileInfo, ex);
						return;
					}
					if (!skipFile)
					{
						string fromFile = Utility.PathCombine(ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.ResourceFilesFolder, staticFile.ResourceName);

						if (System.IO.File.Exists(filePath))
							Slyce.Common.Utility.DeleteFileBrute(filePath);

						System.IO.File.Copy(fromFile, filePath);
						parentNode.AddChildNode(fileInfo, fileName);
						AllTextFilenames.Add(new FilenameInfo(fileName, staticFile.Name, staticFile.Iterator, FilenameInfo.FilenameTypes.StaticFile) { RelativePath = relativeFilePath });
						NumFiles++;
					}
					break;
				case ArchAngel.Interfaces.Template.IteratorTypes.Entity:
					foreach (var entity in ScriptProject.Entities)
					{
						try
						{
							fileName = GetStaticFileName(staticFile.Id, entity);
						}
						catch (Exception ex)
						{
							fileName = GetNodeDisplayText(staticFile.Name, string.Format("[{0}]", entity.Name));
							filePath = Utility.PathCombine(path, fileName);
							relativeFilePath = filePath.Replace(TargetFolder, "").TrimStart(Path.DirectorySeparatorChar);
							fileInfo = new BinaryFileInformation()
							{
								RelativeFilePath = relativeFilePath,
								NewGenFile = new BinaryFile(filePath, false)
							};
							ProcessFileGenerationException(parentNode, staticFile.Id, fileName, fileInfo, ex);
							NumFiles++;
							continue;
						}
						filePath = Utility.PathCombine(path, fileName);
						relativeFilePath = filePath.Replace(TargetFolder, "").TrimStart(Path.DirectorySeparatorChar);
						fileInfo = new BinaryFileInformation()
						{
							RelativeFilePath = relativeFilePath,
							NewGenFile = new BinaryFile(filePath, false)
						};
						try
						{
							CopyUserFileToGenerationLocation(filePath, fileInfo);
							CurrentFilePathProperty.SetValue(null, filePath, null);
							CallStaticFilePreWriteFunction(staticFile.Id, entity, out skipFile);
							string newFilePath = (string)CurrentFilePathProperty.GetValue(null, null);

							if (newFilePath != filePath)
							{
								//filePath = newFilePath;
								relativeFilePath = filePath.Replace(TargetFolder, "").TrimStart(Path.DirectorySeparatorChar);
								fileInfo = new BinaryFileInformation()
								{
									RelativeFilePath = relativeFilePath,
									NewGenFile = new BinaryFile(filePath, false)
								};
							}
						}
						catch (Exception ex)
						{
							ProcessFileGenerationException(parentNode, staticFile.Id, fileName, fileInfo, ex);
							continue;
						}
						if (!skipFile)
						{
							System.IO.File.Copy(staticFile.ResourceName, filePath);
							parentNode.AddChildNode(fileInfo, fileName);
							AllTextFilenames.Add(new FilenameInfo(fileName, staticFile.Name, staticFile.Iterator, FilenameInfo.FilenameTypes.GeneratedFile) { RelativePath = relativeFilePath });
							NumFiles++;
						}
					}
					break;
				case ArchAngel.Interfaces.Template.IteratorTypes.Component:
					foreach (var component in ScriptProject.Components)
					{
						try
						{
							fileName = GetStaticFileName(staticFile.Id, component);
						}
						catch (Exception ex)
						{
							fileName = "ERROR";
							filePath = Utility.PathCombine(path, fileName);
							relativeFilePath = filePath.Replace(TargetFolder, "").TrimStart(Path.DirectorySeparatorChar);
							fileInfo = new BinaryFileInformation()
							{
								RelativeFilePath = relativeFilePath,
								NewGenFile = new BinaryFile(filePath, false)
							};
							ProcessFileGenerationException(parentNode, staticFile.Id, fileName, fileInfo, ex);
							NumFiles++;
							continue;
						}
						filePath = Utility.PathCombine(path, fileName);
						relativeFilePath = filePath.Replace(TargetFolder, "").TrimStart(Path.DirectorySeparatorChar);
						fileInfo = new BinaryFileInformation()
						{
							RelativeFilePath = relativeFilePath,
							NewGenFile = new BinaryFile(filePath, false)
						};
						try
						{
							CopyUserFileToGenerationLocation(filePath, fileInfo);
							CurrentFilePathProperty.SetValue(null, filePath, null);
							CallStaticFilePreWriteFunction(staticFile.Id, component, out skipFile);
							string newFilePath = (string)CurrentFilePathProperty.GetValue(null, null);

							if (newFilePath != filePath)
							{
								//filePath = newFilePath;
								relativeFilePath = filePath.Replace(TargetFolder, "").TrimStart(Path.DirectorySeparatorChar);
								fileInfo = new BinaryFileInformation()
								{
									RelativeFilePath = relativeFilePath,
									NewGenFile = new BinaryFile(filePath, false)
								};
							}
						}
						catch (Exception ex)
						{
							ProcessFileGenerationException(parentNode, staticFile.Id, fileName, fileInfo, ex);
							continue;
						}
						if (!skipFile)
						{
							System.IO.File.Copy(staticFile.ResourceName, filePath);
							parentNode.AddChildNode(fileInfo, fileName);
							AllTextFilenames.Add(new FilenameInfo(fileName, staticFile.Name, staticFile.Iterator, FilenameInfo.FilenameTypes.GeneratedFile) { RelativePath = relativeFilePath });
							NumFiles++;
						}
					}
					break;
				case ArchAngel.Interfaces.Template.IteratorTypes.Table:
					foreach (var table in ScriptProject.Tables)
					{
						try
						{
							fileName = GetStaticFileName(staticFile.Id, table);
						}
						catch (Exception ex)
						{
							fileName = "ERROR";
							filePath = Utility.PathCombine(path, fileName);
							relativeFilePath = filePath.Replace(TargetFolder, "").TrimStart(Path.DirectorySeparatorChar);
							fileInfo = new BinaryFileInformation()
							{
								RelativeFilePath = relativeFilePath,
								NewGenFile = new BinaryFile(filePath, false)
							};
							ProcessFileGenerationException(parentNode, staticFile.Id, fileName, fileInfo, ex);
							NumFiles++;
							continue;
						}
						filePath = Utility.PathCombine(path, fileName);
						relativeFilePath = filePath.Replace(TargetFolder, "").TrimStart(Path.DirectorySeparatorChar);
						fileInfo = new BinaryFileInformation()
						{
							RelativeFilePath = relativeFilePath,
							NewGenFile = new BinaryFile(filePath, false)
						};
						try
						{
							CopyUserFileToGenerationLocation(filePath, fileInfo);
							CurrentFilePathProperty.SetValue(null, filePath, null);
							CallStaticFilePreWriteFunction(staticFile.Id, table, out skipFile);
							string newFilePath = (string)CurrentFilePathProperty.GetValue(null, null);

							if (newFilePath != filePath)
							{
								//filePath = newFilePath;
								relativeFilePath = filePath.Replace(TargetFolder, "").TrimStart(Path.DirectorySeparatorChar);
								fileInfo = new BinaryFileInformation()
								{
									RelativeFilePath = relativeFilePath,
									NewGenFile = new BinaryFile(filePath, false)
								};
							}
						}
						catch (Exception ex)
						{
							ProcessFileGenerationException(parentNode, staticFile.Id, fileName, fileInfo, ex);
							continue;
						}
						if (!skipFile)
						{
							System.IO.File.Copy(staticFile.ResourceName, filePath);
							parentNode.AddChildNode(fileInfo, fileName);
							AllTextFilenames.Add(new FilenameInfo(fileName, staticFile.Name, staticFile.Iterator, FilenameInfo.FilenameTypes.GeneratedFile) { RelativePath = relativeFilePath });
							NumFiles++;
						}
					}
					break;
				default:
					throw new NotImplementedException("This iterator not handled yet: " + staticFile.Iterator.ToString());
			}

		}

		private string GetNodeDisplayText(string text, string placeholder)
		{
			int start = text.IndexOf(ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.DelimiterStart);

			if (start < 0)
				return text;

			int end = text.IndexOf(ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.DelimiterEnd, start) + 2;

			while (start >= 0 && end > start)
			{
				text = text.Substring(0, start) + placeholder + text.Substring(end);
				start = text.IndexOf(ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.DelimiterStart);

				if (start < 0)
					return text;

				end = text.IndexOf(ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.DelimiterEnd, start) + 2;
			}
			return text;
		}

		private void ProcessFileGenerationException(ProjectFileTreeNode parentNode, int fileId, string fileName, TextFileInformation fileInfo, Exception ex)
		{
			int line;
			string cleanStackTrace = GetCleanStackTrace(ex.InnerException.StackTrace, fileId, out line);
			string message = string.Format("<span class='filename'><b>Error Message:</b></span>{0}<span class='error'>{1}{0}{0}</span><span class='filename'><b>Stacktrace:</b></span>{0}<span class='error'>{2}</span>", "<br/>&nbsp;", ex.InnerException.Message, cleanStackTrace);
			parentNode.AddChildNode(fileInfo, fileName).GenerationError = new GenerationError(fileName, message);
			NumFiles++;
		}

		public static string GetCleanStackTrace(string stacktrace, int fileId, out int firstErrLine)
		{
			string[] lines = stacktrace.Split('\n');
			firstErrLine = -1;

			for (int i = 0; i < lines.Length; i++)
			{
				if (lines[i].Contains("Slyce.FunctionRunner") &&
					(lines[i].Contains("GetFileBody(") ||
					lines[i].Contains("GetFileName(") ||
					lines[i].Contains("PreGeneration(")))
				{
					int start;
					int scriptLine;
					ArchAngel.Common.Generator.DebugPos debugPos = null;
					start = lines[i].LastIndexOf(" ") + 1;
					string lineNumberString = lines[i].Substring(start).Trim().TrimEnd('.');

					if (!int.TryParse(lineNumberString, out scriptLine))
						throw new Exception(string.Format("Unexpected line style.\nLine:{0}\n\nStackTrace:\n{1}", lines[i], stacktrace));

					debugPos = ArchAngel.Common.Generator.GetDebugPos(string.Format("File_{0}", fileId), scriptLine - 1, 1, "In GetCleanStackTrace().");

					string functionType = "";

					if (lines[i].IndexOf("GetFileBody(") > 0)
						functionType = "Body";
					else if (lines[i].IndexOf("GetFileName(") > 0)
						functionType = "Filename";
					else if (lines[i].IndexOf("PreGeneration(") > 0)
						functionType = "PreGeneration";

					lines[i] = string.Format("Script ({0}): line {1}", functionType, debugPos.Line);

					if (debugPos != null && firstErrLine < 0)
						firstErrLine = debugPos.Line;
				}
			}
			StringBuilder sb = new StringBuilder(200);

			foreach (string line in lines)
				sb.AppendLine(line);

			return sb.ToString();
		}

		private void ProcessFileGenerationException(ProjectFileTreeNode parentNode, int fileId, string fileName, BinaryFileInformation fileInfo, Exception ex)
		{
			var baseException = ex.GetBaseException();
			string stackTrace = baseException.StackTrace;
			stackTrace = stackTrace.Substring(stackTrace.IndexOf(":line ") + ":line ".Length);
			int errLine = int.Parse(stackTrace);
			ArchAngel.Common.Generator.DebugPos debugPos = ArchAngel.Common.Generator.GetDebugPos(string.Format("StaticFile_{0}", fileId), errLine - 1, 1, string.Format("Error: {0}\nFileId: {1}.", baseException.Message, fileId));

			string message = string.Format("<span class='error'>Line {0}: {1}</span>", errLine, baseException.Message);

			//if (ex.InnerException != null)
			//{
			//    message += ":<br/>" + Environment.NewLine + "<b>" + ex.InnerException.Message + "</b>" +
			//               Environment.NewLine + GetCleanTemplateFunctionErrorStackTrace(ex) +
			//               Environment.NewLine + "Target Site: " + ex.InnerException.TargetSite;
			//}

			//if (addToProjectFileTree)
			parentNode.AddChildNode(fileInfo, fileName).GenerationError = new GenerationError(fileName, message);

			NumFiles++;
		}

		private void RaiseTemplateFunctionCallErrorEvent(Exception ex)
		{
			if (_ProgressHelper == null) return;

			int generated = 0;

			if (_ProgressHelper.LastProgressObject != null)
				generated = _ProgressHelper.LastProgressObject.NumberOfFilesGenerated;

			_ProgressHelper.ReportProgress(40, new GenerateFilesProgress(generated, ex));
		}

		private static string GetCleanTemplateFunctionErrorStackTrace(Exception ex)
		{
			return ex.StackTrace;
			//string stackTrace = "";

			//if (ex.InnerException != null)
			//{
			//    stackTrace = Utility.StandardizeLineBreaks(ex.InnerException.StackTrace, Utility.LineBreaks.Unix);

			//    int ourCodeStart = stackTrace.IndexOf("ScriptFunctionWrapper.RunScriptFunction");
			//    ourCodeStart = stackTrace.LastIndexOf("\n", ourCodeStart);

			//    stackTrace = stackTrace.Substring(0, ourCodeStart);// - 1);
			//    stackTrace = stackTrace.Replace("TemplateGen.", "");
			//    stackTrace = stackTrace.Replace(" at ", "<br/> at ");
			//    stackTrace = "<i>" + stackTrace + "</i>";
			//}
			//return stackTrace;
		}

		private MethodInfo GetSetProjectMethod()
		{
			//return baseAssembly.GetType("Slyce.FunctionRunner.FunctionProcessor").GetMethod("SetProject");
			//return CurrentAssembly.GetType("Slyce.FunctionRunner.Controller").GetMethod("SetProject");
			return CurrentAssembly.GetType("Slyce.FunctionRunner.FunctionProcessor").GetMethod("SetProject");
		}

		private MethodInfo ConvertMethod
		{
			get
			{
				if (_ConvertMethod == null)
					_ConvertMethod = CurrentAssembly.GetType("Slyce.FunctionRunner.FunctionProcessor").GetMethod("Convert");

				return _ConvertMethod;
			}
		}

		public void SetProjectInCode(ArchAngel.Interfaces.Scripting.NHibernate.Model.IProject project)
		{
			object[] parms = new object[] { project };
			GetSetProjectMethod().Invoke(null, parms);
		}

		public string GetFileBody(int id, object iterator, out bool skip)
		{
			SkipFileProperty.SetValue(null, false, null);
			if (FileFunctionLookups[id].GetBodyMethod == null)
				FileFunctionLookups[id].GetBodyMethod = CurrentAssembly.GetType("Slyce.FunctionRunner." + FileFunctionLookups[id].FunctionNameInCode).GetMethod("GetFileBody");

			object[] parms = iterator == null ? new object[0] : new object[] { ConvertMethod.Invoke(null, new object[] { iterator }) };
			string body = (string)FileFunctionLookups[id].GetBodyMethod.Invoke(null, parms);
			skip = (bool)SkipFileProperty.GetValue(null, null);
			return body.StartsWith("          ") ? body.Substring(10).Replace("\n          ", "\n") : body;
		}

		public bool CallStaticFilePreWriteFunction(int id, object iterator, out bool skip)
		{
			SkipFileProperty.SetValue(null, false, null);

			if (StaticFileFunctionLookups[id].PreWriteMethod == null)
				StaticFileFunctionLookups[id].PreWriteMethod = CurrentAssembly.GetType("Slyce.FunctionRunner." + StaticFileFunctionLookups[id].FunctionNameInCode).GetMethod("PreWrite");

			object[] parms = iterator == null ? new object[0] : new object[] { ConvertMethod.Invoke(null, new object[] { iterator }) };
			StaticFileFunctionLookups[id].PreWriteMethod.Invoke(null, parms);
			skip = (bool)SkipFileProperty.GetValue(null, null);
			return skip;
		}

		public string GetFileName(int id, object iterator)
		{
			if (FileFunctionLookups[id].GetNameMethod == null)
				FileFunctionLookups[id].GetNameMethod = CurrentAssembly.GetType("Slyce.FunctionRunner." + FileFunctionLookups[id].FunctionNameInCode).GetMethod("GetFileName");

			object[] parms = iterator == null ? new object[0] : new object[] { ConvertMethod.Invoke(null, new object[] { iterator }) };
			//object[] parms = iterator == null ? new object[0] : new object[] { iterator };
			string filename = (string)FileFunctionLookups[id].GetNameMethod.Invoke(null, parms);
			return filename.StartsWith("          ") ? filename.Substring(10).Replace("\n          ", "\n") : filename;
		}

		public string GetStaticFileName(int id, object iterator)
		{
			if (StaticFileFunctionLookups[id].GetNameMethod == null)
				StaticFileFunctionLookups[id].GetNameMethod = CurrentAssembly.GetType("Slyce.FunctionRunner." + StaticFileFunctionLookups[id].FunctionNameInCode).GetMethod("GetFileName");

			object[] parms = iterator == null ? new object[0] : new object[] { ConvertMethod.Invoke(null, new object[] { iterator }) };
			//object[] parms = iterator == null ? new object[0] : new object[] { iterator };
			string filename = (string)StaticFileFunctionLookups[id].GetNameMethod.Invoke(null, parms);
			return filename.StartsWith("          ") ? filename.Substring(10).Replace("\n          ", "\n") : filename;
		}

		public string GetFolderName(int id, object iterator)
		{
			if (FolderFunctionLookups[id].GetNameMethod == null)
				FolderFunctionLookups[id].GetNameMethod = CurrentAssembly.GetType("Slyce.FunctionRunner." + FolderFunctionLookups[id].FunctionNameInCode).GetMethod("GetFolderName");

			object[] parms = iterator == null ? new object[0] : new object[] { ConvertMethod.Invoke(null, new object[] { iterator }) };
			string folderName = (string)FolderFunctionLookups[id].GetNameMethod.Invoke(null, parms);
			return folderName.StartsWith("          ") ? folderName.Substring(10).Replace("\n          ", "\n") : folderName;
		}

		private void AddFolderToAssembly(Folder folder, string path)
		{
			string functionNameInCode = string.Format("Func_{0}", FunctionCounter++);
			string code = GetFolderFunctionCode(functionNameInCode, folder.Name, folder.Iterator);
			FolderFunctionLookups.Add(folder.ID, new FolderPayload(functionNameInCode, folder.Name, Scripter.RemoveDebugSymbols(code), GetIteratorParamString(folder.Iterator), folder));

			foreach (Folder subFolder in folder.Folders)
				AddFolderToAssembly(subFolder, path + folder.Name);

			foreach (ArchAngel.Interfaces.Template.File file in folder.Files)
				AddFileToAssembly(file, path + folder.Name);

			foreach (ArchAngel.Interfaces.Template.StaticFile staticFile in folder.StaticFiles)
				AddStaticFileToAssembly(staticFile, path + folder.Name);
		}

		private void AddFileToAssembly(ArchAngel.Interfaces.Template.File file, string path)
		{
			string functionNameInCode = string.Format("Func_{0}", FunctionCounter++);
			string code = GetFileFunctionCode(functionNameInCode, file.Name, file.Script.Body, file.Iterator);
			Dictionary<int, Map> lineMaps = GetLineMaps(code);
			string key = string.Format("File_{0}", file.Id);

			if (AllDebugLines.ContainsKey(key))
				AllDebugLines[key] = lineMaps;
			else
				AllDebugLines.Add(key, lineMaps);

			FileFunctionLookups.Add(file.Id, new FilePayload(functionNameInCode, file.Name, Scripter.RemoveDebugSymbols(code), GetIteratorParamString(file.Iterator), file, lineMaps));
		}

		private void AddStaticFileToAssembly(ArchAngel.Interfaces.Template.StaticFile file, string path)
		{
			string functionNameInCode = string.Format("StaticFileFunc_{0}", FunctionCounter++);
			string code = GetStaticFileFunctionCode(functionNameInCode, file.Name, file.SkipThisFileScript, file.Iterator);
			Dictionary<int, Map> lineMaps = GetLineMaps(code);
			string key = string.Format("StaticFile_{0}", file.Id);

			if (AllDebugLines.ContainsKey(key))
				AllDebugLines[key] = lineMaps;
			else
				AllDebugLines.Add(key, lineMaps);

			StaticFileFunctionLookups.Add(file.Id, new StaticFilePayload(functionNameInCode, file.Name, Scripter.RemoveDebugSymbols(code), GetIteratorParamString(file.Iterator), file, lineMaps));
		}

		private string GetIteratorParamString(object iterator)
		{
			switch (iterator.ToString())
			{
				case "None":
					return "";
				case "Entity":
					return "Entity entity";
				case "Component":
					return "Component component";
				case "Table":
					return "Table table";
				case "Column":
					return "Column column";
				default:
					throw new NotImplementedException("IteratorType not handled yet: " + iterator.ToString());
			}
		}

		private string GetIteratorTypeString(object iterator)
		{
			switch (iterator.ToString())
			{
				case "None":
					return "";
				case "Entity":
					return "Entity";
				case "Component":
					return "Component";
				case "Table":
					return "Table";
				case "Column":
					return "Column";
				default:
					throw new NotImplementedException("IteratorType not handled yet: " + iterator.ToString());
			}
		}

		private string GetFolderFunctionCode(string id, string rawFileName, object iterator)
		{
			string iteratorVar = GetIteratorParamString(iterator);

			StringBuilder sb = new StringBuilder(2000);
			sb.AppendFormat(@"
					using System;
					using System.Xml;
					using System.Linq;
					using System.Text;
					using System.Collections.Generic;
					using ArchAngel.Interfaces.Scripting;
					using ArchAngel.Interfaces.Scripting.NHibernate.Model;
					using ArchAngel.NHibernateHelper;

					namespace Slyce.FunctionRunner
					{{
						public class {0} : FunctionBase
						{{
							public static string GetFolderName({1})
							{{
								{2}
							}}
						}}
					}}
			",
			 id,
			 iteratorVar,
			 Slyce.Common.Scripter.FormatFunctionBodyAsTemplate(id, rawFileName, ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.DelimiterStart, ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.DelimiterEnd));
			return sb.ToString();
		}

		private string GetFileFunctionCode(string id, string rawFileName, string rawScript, object iterator)
		{
			string iteratorVar = GetIteratorParamString(iterator);

			StringBuilder sb = new StringBuilder(2000);
			sb.AppendFormat(@"
					using System;
					using System.Xml;
					using System.Linq;
					using System.Text;
					using System.Collections.Generic;
					using ArchAngel.Interfaces.Scripting;
					using ArchAngel.Interfaces.Scripting.NHibernate.Model;
					using ArchAngel.NHibernateHelper;

					namespace Slyce.FunctionRunner
					{{
						public class {0} : FunctionBase
						{{
							public static string GetFileName({1})
							{{
								{2}
							}}

							public static string GetFileBody({1})
							{{
					",
					id,
			 iteratorVar,
			 Slyce.Common.Scripter.FormatFunctionBodyAsTemplate(id + ".GetFileName", rawFileName, ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.DelimiterStart, ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.DelimiterEnd));

			if (OffsetLinesForFile <= 0)
				OffsetLinesForFile = sb.ToString().Split('\n').Count();

			sb.AppendFormat(@"{0}
							}}
						}}
					}}
			",
			 Slyce.Common.Scripter.FormatFunctionBodyAsTemplate(id + ".GetFileBody", rawScript, ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.DelimiterStart, ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.DelimiterEnd));


			return sb.ToString();
		}

		private string GetStaticFileFunctionCode(string id, string rawFileName, string skipFileRawScript, object iterator)
		{
			string iteratorVar = GetIteratorParamString(iterator);

			StringBuilder sb = new StringBuilder(2000);
			sb.AppendFormat(@"
					using System;
					using System.Xml;
					using System.Linq;
					using System.Text;
					using System.Collections.Generic;
					using ArchAngel.Interfaces.Scripting;
					using ArchAngel.Interfaces.Scripting.NHibernate.Model;
					using ArchAngel.NHibernateHelper;

					namespace Slyce.FunctionRunner
					{{
						public class {0} : FunctionBase
						{{
							public static string GetFileName({1})
							{{
								{2}
							}}

							public static void PreWrite({1})
							{{
					",
					id,
			 iteratorVar,
			 Slyce.Common.Scripter.FormatFunctionBodyAsTemplate(id + ".GetFileName", rawFileName, ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.DelimiterStart, ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.DelimiterEnd));

			if (OffsetLinesForFile <= 0)
				OffsetLinesForFile = sb.ToString().Split('\n').Count();

			sb.AppendFormat(@"{0}
							}}
						}}
					}}
			",
			 Slyce.Common.Scripter.FormatFunctionBodyAsCodeOnly(id + ".SkipFile", skipFileRawScript));

			return sb.ToString();
		}

		public string GetFunctionLookupClass()
		{
			return GetFunctionLookupClass(false);
		}

		public string GetFunctionAssemblyOverrideClass()
		{
			StringBuilder sb = new StringBuilder(2000);
			sb.Append(@"
					using System;
					using System.Xml;
					using System.Linq;
					using System.Text;
					using System.Collections.Generic;
					using ArchAngel.Providers.EntityModel.Helper;
					using ArchAngel.Interfaces;
					using ArchAngel.Interfaces.Scripting;
					using ArchAngel.Interfaces.Scripting.NHibernate.Model;
					using ArchAngel.NHibernateHelper;

					namespace Slyce.FunctionRunner
					{
						public class Controller
						{
							public static Slyce.FunctionRunner.Project Project { get; set; }

							public static void SetProject(IProject project)
							{
								Project = new Project(project);
							}
						}
						");
			sb.Append(GetFunctionBaseClass());
			sb.Append(@"
					}
					");

			return sb.ToString();
		}

		public string GetFunctionLookupClass(bool justClasses)
		{
			StringBuilder sb = new StringBuilder(2000);

			if (!justClasses)
			{
				sb.Append(@"
					using System;
					using System.Xml;
					using System.Linq;
					using System.Text;
					using System.Collections.Generic;
					using ArchAngel.Providers.EntityModel.Helper;
					using ArchAngel.Interfaces;
					using ArchAngel.Interfaces.Scripting;
					using ArchAngel.Interfaces.Scripting.NHibernate.Model;
					using ArchAngel.NHibernateHelper;

					namespace Slyce.FunctionRunner
					{
					");
			}

			sb.Append(GetFunctionBaseClass());
			sb.Append(@"

						public class FunctionProcessor
						{
						");

			if (!justClasses)
				sb.Append(GetFillScriptModelFunctionCode());

			sb.Append(@"
							public static bool SkipFile = false;

							public static bool SkipCurrentFile
							{
								get { return SkipFile; }
								set { SkipFile = value; }
							}

							public static string CurrentFilePath { get; set; }

							public static Project Project { get; set; }

							public static void SetProject(IProject project)
							{
								Project = new Project(project);
							}
					");
			sb.Append(@"
							public string GetFolderName(string functionName, object iterator)
							{
								switch (functionName)
								{
					");

			foreach (var f in FolderFunctionLookups)
			{
				string param = GetIteratorTypeString(f.Value.Folder.Iterator);

				if (param.Length > 0)
					param = "(" + param + ")iterator";

				sb.AppendFormat(@"case ""{0}"": return {1}.GetFolderName({2});{3}", f.Key, f.Value.FunctionNameInCode, param, Environment.NewLine);
			}

			sb.Append(@"
									default: throw new Exception(""Function not found: "" + functionName);
								}
							}

							public string GetFileName(string functionName, object iterator)
							{
								switch (functionName)
								{
					");

			foreach (var f in FileFunctionLookups)
			{
				string param = GetIteratorTypeString(f.Value.File.Iterator);

				if (param.Length > 0)
					param = "(" + param + ")iterator";

				sb.AppendFormat(@"case ""{0}"": return {1}.GetFileName({2});{3}", f.Key, f.Value.FunctionNameInCode, param, Environment.NewLine);
			}

			sb.Append(@"
									default: throw new Exception(""Function not found: "" + functionName);
								}
							}

							public string GetFileBody(string functionName, object iterator)
							{
								switch (functionName)
								{
					");

			foreach (var f in FileFunctionLookups)
			{
				string param = GetIteratorTypeString(f.Value.File.Iterator);

				if (param.Length > 0)
					param = "(" + param + ")iterator";

				sb.AppendFormat(@"case ""{0}"": return {1}.GetFileBody({2});{3}", f.Key, f.Value.FunctionNameInCode, param, Environment.NewLine);
			}

			sb.Append(@"
									default: throw new Exception(""Function not found: "" + functionName);
								}
							}
						}

						");
			sb.Append(GetExtensionMethodClass());
			sb.Append(GetXmlExtensionsClass());

			if (!justClasses)
			{
				sb.Append(@"
					}
			");
			}
			return sb.ToString();
		}

		private static string GetUserSettingProperties(string iteratorName)
		{
			string prefix = "";

			if (iteratorName.EndsWith(".ComponentSpecification"))
				prefix = "ComponentSpec_";
			else if (iteratorName.EndsWith(".Component"))
				prefix = "Component_";
			else if (iteratorName.EndsWith(".ComponentProperty"))
				prefix = "ComponentProperty_";
			else if (iteratorName.EndsWith(".Entity"))
				prefix = "Entity_";
			else if (iteratorName.EndsWith(".Property"))
				prefix = "Property_";
			else if (iteratorName.EndsWith(".Table"))
				prefix = "Table_";
			else if (string.IsNullOrEmpty(iteratorName))
				prefix = "Project";
			else if (iteratorName.EndsWith(".Column"))
				prefix = "Column_";
			else if (iteratorName.EndsWith(".EntityKey"))
				prefix = "EntityKey_";
			else if (iteratorName.EndsWith(".Reference"))
				prefix = "Reference_";
			else if (iteratorName.EndsWith(".Index"))
				prefix = "Index_";
			else if (iteratorName.EndsWith(".Key"))
				prefix = "Key_";
			else
				throw new NotImplementedException("iteratorName not handled in GetUserSettingProperties() yet: " + iteratorName);

			StringBuilder sb = new StringBuilder(1000);

			foreach (var userOption in ArchAngel.Interfaces.SharedData.CurrentProject.Options.Where(o => o.IteratorName == iteratorName).OrderBy(o => o.Category))
			{
				string cleanName = userOption.VariableName;

				if (!string.IsNullOrEmpty(prefix) && userOption.VariableName.StartsWith(prefix))
					cleanName = cleanName.Substring(prefix.Length);

				if (string.IsNullOrEmpty(iteratorName)) // Project
					sb.AppendLine(string.Format(@"  public {0} {1} {{  get {{ return ({0})SharedData.CurrentProject.GetUserOption(""{2}""); }} }}", GetUserOptionTypeName(userOption), cleanName, userOption.VariableName));
				else
					sb.AppendLine(string.Format(@"  public {0} {1} {{ get {{ return ({0})((IScriptBaseObject)this.ScriptObject).GetUserOptionValue(""{2}""); }} }}", GetUserOptionTypeName(userOption), cleanName, userOption.VariableName));
			}
			return sb.ToString();
		}

		private static string GetXmlExtensionsClass()
		{
			return @"
				public static class XmlExtensions
				{
					internal static XmlNamespaceManager NamespaceManager;
					internal static string NamespacePrefix;
					internal static string Namespace;

					public static string Beautify(this XmlDocument doc)
					{
						StringBuilder sb = new StringBuilder();
						XmlWriterSettings settings = new XmlWriterSettings();
						settings.Indent = true;
						settings.IndentChars = ""\t"";
						settings.NewLineChars = ""\r\n"";
						settings.NewLineHandling = NewLineHandling.Replace;
						XmlWriter writer = XmlWriter.Create(sb, settings);
						doc.Save(writer);
						writer.Close();
						return sb.ToString();
					}

					public static XmlNode EnsureChildNodeExists(this XmlNode node, string childNodeName)
					{
						return EnsureChildNodeExists(node, childNodeName, """", new List<RequiredAttribute>());
					}

					public static XmlNode EnsureChildNodeExists(this XmlNode node, string childNodeName, RequiredAttribute withAttribute)
					{
						List<RequiredAttribute> withAttributes = new List<RequiredAttribute>();
						withAttributes.Add(withAttribute);
						return EnsureChildNodeExists(node, childNodeName, """", withAttributes);
					}

					public static XmlNode EnsureChildNodeExists(this XmlNode node, string childNodeName, IEnumerable<RequiredAttribute> withAttributes)
					{
						return EnsureChildNodeExists(node, childNodeName, """", withAttributes);
					}

					public static XmlNode EnsureChildNodeExists(this XmlNode node, string childNodeName, string defaultInnerText, RequiredAttribute withAttribute)
					{
						List<RequiredAttribute> withAttributes = new List<RequiredAttribute>();
						withAttributes.Add(withAttribute);
						return EnsureChildNodeExists(node, childNodeName, defaultInnerText, withAttributes);
					}

					public static XmlNode EnsureChildNodeExists(this XmlNode node, string childNodeName, string defaultInnerText, IEnumerable<RequiredAttribute> withAttributes)
					{
						StringBuilder sb = new StringBuilder(50);

						for (int i = 0; i < withAttributes.Count(); i++)
						{
							RequiredAttribute reqAtt = withAttributes.ElementAt(i);

							if (i > 0)
								sb.Append("" and "");

							if (reqAtt.ValueIsPartOfCheck)
								sb.AppendFormat(""@{0} = '{1}'"", reqAtt.Name, reqAtt.Value);
							else
								sb.AppendFormat(""@{0}"", reqAtt.Name);
						}
						string xPath;

						if (sb.Length > 0)
							xPath = string.Format(""{0}[{1}]"", childNodeName, sb);
						else
							xPath = childNodeName;

						if (NamespaceManager != null)
							xPath = NamespacePrefix + "":"" + xPath;

						XmlNode childNode;

						if (NamespaceManager == null)
							childNode = node.SelectSingleNode(xPath);
						else
							childNode = node.SelectSingleNode(xPath, NamespaceManager);

						if (childNode == null)
						{
							if (string.IsNullOrEmpty(Namespace))
								childNode = node.OwnerDocument.CreateElement(childNodeName);
							else
								childNode = node.OwnerDocument.CreateElement(childNodeName, Namespace);

							foreach (RequiredAttribute reqAtt in withAttributes)
							{
								XmlAttribute att = node.OwnerDocument.CreateAttribute(reqAtt.Name);
								att.Value = reqAtt.Value ?? """";
								childNode.Attributes.Append(att);
							}
							if (!string.IsNullOrEmpty(defaultInnerText))
								childNode.InnerXml = defaultInnerText;

							node.AppendChild(childNode);
						}
						return childNode;
					}
				}

				public class RequiredAttribute
				{
					public RequiredAttribute(string name, string value, bool valueIsPartOfCheck)
					{
						Name = name;
						Value = value;
						ValueIsPartOfCheck = valueIsPartOfCheck;
					}

					public string Name { get; set; }
					public string Value { get; set; }
					public bool ValueIsPartOfCheck { get; set; }
				}
				";
		}

		private static string GetExtensionMethodClass()
		{
			StringBuilder sb = new StringBuilder(2000);

			sb.Append(@"
					public class Helper
					{
						public static string GetHbmXml(IEntity entity, string assemblyName,
													string entityNamespace,
													bool autoImport,
													ArchAngel.Interfaces.NHibernateEnums.TopLevelAccessTypes defaultAccess,
													ArchAngel.Interfaces.NHibernateEnums.TopLevelCascadeTypes defaultCascade,
													bool defaultLazy)
						{
							ArchAngel.Providers.EntityModel.Model.EntityLayer.Entity realEntity = (ArchAngel.Providers.EntityModel.Model.EntityLayer.Entity)entity.ScriptObject;

							return ArchAngel.NHibernateHelper.MappingFiles.Version_2_2.Utility.CreateMappingXMLFrom(
													realEntity,
													assemblyName,
													entityNamespace,
													autoImport,
													defaultAccess,
													defaultCascade,
													defaultLazy);
						}

						public static string GetNhValidatorXml(IEntity entity, string assembly, string @namespace)
						{
							ArchAngel.Providers.EntityModel.Model.EntityLayer.Entity realEntity = (ArchAngel.Providers.EntityModel.Model.EntityLayer.Entity)entity.ScriptObject;
							return ArchAngel.NHibernateHelper.NHValidatorMapping.CreateXmlForEntity(realEntity, assembly, @namespace);
						}

						public static string GetExistingRelativeFilepathForEntityClass(Entity entity, string csprojFullFilePath)
						{
							return ArchAngel.NHibernateHelper.HibernateMappingHelper.GetRelativeFilenameForEntityCSharpFile((IEntity)entity.ScriptObject, csprojFullFilePath);
						}

						private static Dictionary<string, object> TemplateCache = new Dictionary<string, object>();

						public static string GetMergedSourceCodeForExistingFile(Entity entity, out string existingFilePath)
						{
							if (entity.MappedClass == null)
								throw new Exception(""entity.MappedClass is null. GetMergedSourceCodeForExistingFile() can't be called with a nnull MappedClass"");

							ArchAngel.NHibernateHelper.CodeInsertions.EntityCodeInsertions inserter = new ArchAngel.NHibernateHelper.CodeInsertions.EntityCodeInsertions();
							return inserter.Process(entity, TemplateCache, out existingFilePath);
						}
					}

					public class Lookups
					{
						public static Dictionary<IEntity, IEntity> EntityLookups = new Dictionary<IEntity, IEntity>();
						public static Dictionary<ITable, ITable> TableLookups = new Dictionary<ITable, ITable>();
						public static Dictionary<ITable, ITable> ViewLookups = new Dictionary<ITable, ITable>();
						public static Dictionary<IColumn, IColumn> ColumnLookups = new Dictionary<IColumn, IColumn>();
						public static Dictionary<IProperty, IProperty> PropertyLookups = new Dictionary<IProperty, IProperty>();
					}

					[Serializable]
					public class Column : IColumn
					{
						public Column()
						{
						}

						public Column(IColumn fromColumn)
						{
							Name = fromColumn.Name;
							ScriptObject = fromColumn.ScriptObject;
							IsIdentity = fromColumn.IsIdentity;
							IsNullable = fromColumn.IsNullable;
							Length = fromColumn.Length;
							Type = fromColumn.Type;
							SizeIsMax = fromColumn.SizeIsMax;

							Lookups.ColumnLookups.Add(fromColumn, this);
						}

						private UserSettings _Settings;
						public UserSettings Settings { get { if (_Settings == null) _Settings = new UserSettings(this.ScriptObject); return _Settings; } }

						public class UserSettings
						{
							IScriptBaseObject ScriptObject;

							internal UserSettings(IScriptBaseObject scriptObject)
							{
								ScriptObject = scriptObject;
							}
					");
			sb.Append(GetUserSettingProperties("ArchAngel.Providers.EntityModel.Model.EntityLayer.Column"));
			sb.Append(@"
						}
					}

					[Serializable]
					public class Index : IIndex
					{
						public Index()
						{
							Columns = new List<IColumn>();
						}

						public Index(IIndex fromIndex)
						{
							Name = fromIndex.Name;
							IndexType = fromIndex.IndexType;
							IsClustered = fromIndex.IsClustered;
							IsUnique = fromIndex.IsUnique;
							IsUserDefined = fromIndex.IsUserDefined;

							foreach (var column in fromIndex.Columns)
								Columns.Add(Lookups.ColumnLookups[column]);
						}
					}

					[Serializable]
					public class Key : IKey
					{
						public Key()
						{
							Columns = new List<IColumn>();
						}

						public Key(IKey fromKey)
						{
							Name = fromKey.Name;
							TableSchema = fromKey.TableSchema;
							TableName = fromKey.TableName;
							KeyType = fromKey.KeyType;

							//TODO: public IKey ReferencedPrimaryKey { get; set; }

							foreach (var column in fromKey.Columns)
								Columns.Add(Lookups.ColumnLookups[column]);
						}
					}

					[Serializable]
					public class Component : IComponent
					{
						public Component()
						{
							Properties = new List<IFieldDef>();
						}

						public Component(IComponent fromComponent)
						{
							Name = fromComponent.Name;
							ScriptObject = fromComponent.ScriptObject;
							Properties = new List<IFieldDef>();

							foreach (IFieldDef fromField in fromComponent.Properties)
								Properties.Add(new FieldDef(fromField));
						}

						private UserSettings _Settings;
						public UserSettings Settings { get { if (_Settings == null) _Settings = new UserSettings((IScriptBaseObject)this.ScriptObject); return _Settings; } }

						public class UserSettings
						{
							IScriptBaseObject ScriptObject;

							internal UserSettings(IScriptBaseObject scriptObject)
							{
								ScriptObject = scriptObject;
							}
					");
			sb.Append(GetUserSettingProperties("ArchAngel.Providers.EntityModel.Model.EntityLayer.ComponentSpecification"));
			sb.Append(@"
						}
					}

					[Serializable]
					public class EntityKey : IEntityKey
					{
						public EntityKey()
						{
						}

						public EntityKey(IEntityKey fromEntityKey) : this (fromEntityKey, Lookups.EntityLookups[fromEntityKey.Parent])
						{
						}

						public EntityKey(IEntityKey fromEntityKey, IEntity parent)
						{
							KeyType = (KeyTypes)Enum.Parse(typeof(KeyTypes), fromEntityKey.KeyType.ToString(), true);
							Properties = new List<IProperty>();
							Parent = parent;

							foreach (IProperty fromKeyProperty in fromEntityKey.Properties)
								Properties.Add(new Property(fromKeyProperty, parent));
						}

						private UserSettings _Settings;
						public UserSettings Settings { get { if (_Settings == null) _Settings = new UserSettings(); return _Settings; } }

						public class UserSettings
						{
					");
			sb.Append(GetUserSettingProperties("ArchAngel.Providers.EntityModel.Model.EntityLayer.EntityKey"));
			sb.Append(@"
						}
					}

					[Serializable]
					public class ComponentProperty : IComponentProperty
					{
						public ComponentProperty()
						{
						}

						public ComponentProperty(IComponentProperty fromComponentProperty)
						{
							Name = fromComponentProperty.Name;
							Type = fromComponentProperty.Type;
							IsSetterPrivate = fromComponentProperty.IsSetterPrivate;
							ScriptObject = fromComponentProperty.ScriptObject;
							Properties = new List<IField>();

							foreach (IField fromField in fromComponentProperty.Properties)
								Properties.Add(new Field(fromField));
						}

						private UserSettings _Settings;
						public UserSettings Settings { get { if (_Settings == null) _Settings = new UserSettings((IScriptBaseObject)this.ScriptObject); return _Settings; } }

						public class UserSettings
						{
							IScriptBaseObject ScriptObject;

							internal UserSettings(IScriptBaseObject scriptObject)
							{
								ScriptObject = scriptObject;
							}
					");
			sb.Append(GetUserSettingProperties("ArchAngel.Providers.EntityModel.Model.EntityLayer.Component"));
			sb.Append(@"
						}
					}

					[Serializable]
					public class Discriminator : IDiscriminator
					{
						public Discriminator()
						{
						}

						public Discriminator(IDiscriminator fromDiscriminator)
						{
							if (fromDiscriminator.Column != null)
								Column = Lookups.ColumnLookups[fromDiscriminator.Column];

							DiscriminatorType = fromDiscriminator.DiscriminatorType;
							Formula = fromDiscriminator.Formula;
							CSharpType = fromDiscriminator.CSharpType;
						}
					}

					[Serializable]
					public class Entity : IEntity
					{
						public Entity()
						{
							Properties = new List<IProperty>();
							Components = new List<IComponentProperty>();
							References = new List<IReference>();
							InheritanceTypeWithParent = InheritanceTypes.None;
							Children = new List<IEntity>();
						}

						public Entity(IEntity fromEntity)
						{
							Name = fromEntity.Name;
							NamePlural = fromEntity.NamePlural;
							NameCamelCase = fromEntity.NameCamelCase;
							NameCamelCasePlural = fromEntity.NameCamelCasePlural;
							ParentName = fromEntity.ParentName;
							IsInherited = fromEntity.IsInherited;
							IsMapped = fromEntity.IsMapped;
							IsAbstract = fromEntity.IsAbstract;
							IsReadOnly = fromEntity.IsReadOnly;
							PrimaryMappedTable = fromEntity.PrimaryMappedTable == null ? null : Lookups.TableLookups[fromEntity.PrimaryMappedTable];
							Key = new EntityKey(fromEntity.Key, fromEntity);
							Discriminator = fromEntity.Discriminator == null ? null : new Discriminator(fromEntity.Discriminator);
							DiscriminatorValue = fromEntity.DiscriminatorValue;
							ScriptObject = fromEntity.ScriptObject;
							Properties = new List<IProperty>();
							Components = new List<IComponentProperty>();
							References = new List<IReference>();
							InheritanceTypeWithParent = fromEntity.InheritanceTypeWithParent;
							Proxy = fromEntity.Proxy;
							Cache = fromEntity.Cache;
							SelectBeforeUpdate = fromEntity.SelectBeforeUpdate;
							BatchSize = fromEntity.BatchSize;
							IsDynamicUpdate = fromEntity.IsDynamicUpdate;
							IsDynamicInsert = fromEntity.IsDynamicInsert;
							OptimisticLock = fromEntity.OptimisticLock;
							DefaultCascade = fromEntity.DefaultCascade;
							PrimaryMappedTable = fromEntity.PrimaryMappedTable == null ? null : Lookups.TableLookups[fromEntity.PrimaryMappedTable];
							LazyLoad = fromEntity.LazyLoad;

							if (fromEntity.MappedClass != null)
								MappedClass = new SourceClass(fromEntity.MappedClass);

							Lookups.EntityLookups.Add(fromEntity, this);

							foreach (IProperty fromProperty in fromEntity.Properties)
								Properties.Add(new Property(fromProperty));

							foreach (IComponentProperty fromComponentProperty in fromEntity.Components)
								Components.Add(new ComponentProperty(fromComponentProperty));

							Generator = new EntityGenerator(fromEntity.Generator.ClassName);

							foreach (var p in fromEntity.Generator.Parameters)
								Generator.Parameters.Add(new EntityGenerator.Parameter(p.Name, p.Value));
						}

						private UserSettings _Settings;
						public UserSettings Settings { get { if (_Settings == null) _Settings = new UserSettings((IScriptBaseObject)this.ScriptObject); return _Settings; } }

						public class UserSettings
						{
							IScriptBaseObject ScriptObject;

							internal UserSettings(IScriptBaseObject scriptObject)
							{
								ScriptObject = scriptObject;
							}
					");
			sb.Append(GetUserSettingProperties("ArchAngel.Providers.EntityModel.Model.EntityLayer.Entity"));
			sb.Append(@"
						}
					}

					[Serializable]
					public class EntityGenerator : IEntityGenerator
					{
						public class Parameter : IEntityGenerator.IParameter
						{
							public string Name { get; set; }
							public string Value { get; set; }

							public Parameter(string name, string value) : base(name, value)
							{
							}
						}

						public EntityGenerator(string className) : base(className)
						{
						}

						public string ClassName { get; set; }
						public IList<Parameter> Parameters { get; set; }
					}

					[Serializable]
					public class SourceClass : ISourceClass
					{
						public SourceClass(ISourceClass fromSourceClass)
						{
							FilePath = fromSourceClass.FilePath;

							foreach (ISourceAttribute fromAttribute in fromSourceClass.SourceAttributesThatMustExist)
								SourceAttributesThatMustExist.Add(new SourceAttribute(fromAttribute));

							foreach (ISourceProperty fromProperty in fromSourceClass.SourcePropertiesThatMustExist)
								SourcePropertiesThatMustExist.Add(new SourceProperty(fromProperty));

							foreach (ISourceProperty fromProperty in fromSourceClass.SourcePropertiesThatMustNotExist)
								SourcePropertiesThatMustNotExist.Add(new SourceProperty(fromProperty));

							foreach (ISourceFunction fromFunction in fromSourceClass.SourceFunctionsThatMustExist)
								SourceFunctionsThatMustExist.Add(new SourceFunction(fromFunction));

							foreach (ISourceFunction fromFunction in fromSourceClass.SourceFunctionsThatMustNotExist)
								SourceFunctionsThatMustNotExist.Add(new SourceFunction(fromFunction));

							foreach (ISourceField fromField in fromSourceClass.SourceFieldsThatMustExist)
								SourceFieldsThatMustExist.Add(new SourceField(fromField));
						}
					}

					[Serializable]
					public class SourceProperty : ISourceProperty
					{
						public SourceProperty()
						{
							PreviousNames = new List<string>();
							Modifiers = new List<string>();
						}

						public SourceProperty(ISourceProperty fromSourceProperty)
						{
							Name = fromSourceProperty.Name;
							Type = fromSourceProperty.Type;
							PreviousNames = fromSourceProperty.PreviousNames;
							Modifiers = fromSourceProperty.Modifiers;
							GetAccessor = fromSourceProperty.GetAccessor;
							SetAccessor = fromSourceProperty.SetAccessor;
						}
					}

					[Serializable]
					public class SourceFunction : ISourceFunction
					{
						public SourceFunction()
						{
							PreviousNames = new List<string>();
							Modifiers = new List<string>();
						}

						public SourceFunction(ISourceFunction fromSourceFunction)
						{
							Name = fromSourceFunction.Name;
							Parameters = fromSourceFunction.Parameters;
							Body = fromSourceFunction.Body;
							ReturnType = fromSourceFunction.ReturnType;
							PreviousNames = fromSourceFunction.PreviousNames;
							Modifiers = fromSourceFunction.Modifiers;
						}

						public void AddParameter(string name, string dataType)
						{
							var p = new ISourceParameter(name, dataType);
							this.Parameters.Add(p);
						}
					}

					[Serializable]
					public class SourceParameter : ISourceParameter
					{
						public SourceParameter()
						{
						}

						public SourceParameter(string name, string dataType)
						{
							Name = name;
							DataType = dataType;
						}

						public SourceParameter(ISourceParameter fromSourceParameter)
						{
							Name = fromSourceParameter.Name;
							DataType = fromSourceParameter.DataType;
						}
					}

					[Serializable]
					public class SourceField : ISourceField
					{
						public SourceField()
						{
							PreviousNames = new List<string>();
							Modifiers = new List<string>();
						}

						public SourceField(ISourceField fromSourceField)
						{
							Name = fromSourceField.Name;
							Type = fromSourceField.Type;
							InitialValue = fromSourceField.InitialValue;
							PreviousNames = fromSourceField.PreviousNames;
							Modifiers = fromSourceField.Modifiers;
						}
					}

					[Serializable]
					public class SourceAttribute : ISourceAttribute
					{
						public SourceAttribute()
						{
						}

						public SourceAttribute(ISourceAttribute fromSourceAttribute)
						{
							Name = fromSourceAttribute.Name;
						}
					}

					[Serializable]
					public class FieldDef : IFieldDef
					{
						public FieldDef()
						{
						}

						public FieldDef(IFieldDef fromField)
						{
							Name = fromField.Name;
							Type = fromField.Type;
							IsSetterPrivate = fromField.IsSetterPrivate;
							PreviousNames = fromField.PreviousNames;
							ScriptObject = fromField.ScriptObject;
						}

						private UserSettings _Settings;
						public UserSettings Settings { get { if (_Settings == null) _Settings = new UserSettings((IScriptBaseObject)this.ScriptObject); return _Settings; } }

						public class UserSettings
						{
							IScriptBaseObject ScriptObject;

							internal UserSettings(IScriptBaseObject scriptObject)
							{
								ScriptObject = scriptObject;
							}
					");
			sb.Append(GetUserSettingProperties("ArchAngel.Providers.EntityModel.Model.EntityLayer.ComponentProperty"));
			sb.Append(@"
						}
					}

					[Serializable]
					public class Field : IField
					{
						public Field()
						{
						}

						public Field(IField fromField)
						{
							Name = fromField.Name;
							Type = fromField.Type;
							IsSetterPrivate = fromField.IsSetterPrivate;
							ScriptObject = fromField.ScriptObject;
							MappedColumn = Lookups.ColumnLookups[fromField.MappedColumn];
						}

						private UserSettings _Settings;
						public UserSettings Settings { get { if (_Settings == null) _Settings = new UserSettings((IScriptBaseObject)this.ScriptObject); return _Settings; } }

						public class UserSettings
						{
							IScriptBaseObject ScriptObject;

							internal UserSettings(IScriptBaseObject scriptObject)
							{
								ScriptObject = scriptObject;
							}
					");
			sb.Append(GetUserSettingProperties("ArchAngel.Providers.EntityModel.Model.EntityLayer.ComponentProperty"));
			sb.Append(@"
						}
					}

					[Serializable]
					public class Project : IProject
					{
						public static Project Instance { get; set; }

						public Project()
						{
							Lookups.EntityLookups.Clear();
							Lookups.TableLookups.Clear();
							Lookups.ViewLookups.Clear();
							Lookups.ColumnLookups.Clear();
							Lookups.PropertyLookups.Clear();

							Entities = new List<IEntity>();
							Components = new List<IComponent>();
							Tables = new List<ITable>();
							Views = new List<ITable>();
							NHibernateConfig = new INhConfig();
						}

						public Project(IProject fromProject)
						{
							Lookups.EntityLookups.Clear();
							Lookups.TableLookups.Clear();
							Lookups.ViewLookups.Clear();
							Lookups.ColumnLookups.Clear();
							Lookups.PropertyLookups.Clear();
							OutputFolder = fromProject.OutputFolder;
							TempFolder = fromProject.TempFolder;
							ExistingCsProjectFile = fromProject.ExistingCsProjectFile;
							OverwriteFiles = fromProject.OverwriteFiles;
							NHibernateConfig = new INhConfig();

							if (fromProject.NHibernateConfig != null)
							{
								NHibernateConfig.ConnectionString = fromProject.NHibernateConfig.ConnectionString;
								NHibernateConfig.Driver = fromProject.NHibernateConfig.Driver;
								NHibernateConfig.Dialect = fromProject.NHibernateConfig.Dialect;
								NHibernateConfig.DialectSpatial = fromProject.NHibernateConfig.DialectSpatial;
								NHibernateConfig.FileExists = fromProject.NHibernateConfig.FileExists;
								NHibernateConfig.ExistingFilePath = fromProject.NHibernateConfig.ExistingFilePath;
							}
							Entities = new List<IEntity>();
							Components = new List<IComponent>();
							Tables = new List<ITable>();
							Views = new List<ITable>();

							foreach (ITable fromTable in fromProject.Tables)
								this.Tables.Add(new Table(fromTable));

							foreach (ITable fromView in fromProject.Views)
								this.Views.Add(new Table(fromView));

							foreach (IEntity fromEntity in fromProject.Entities)
								this.Entities.Add(new Entity(fromEntity));

							foreach (IComponent fromComponent in fromProject.Components)
								this.Components.Add(new Component(fromComponent));

							foreach (IEntity fromEntity in fromProject.Entities)
							{
								foreach (IReference fromReference in fromEntity.References)
									Lookups.EntityLookups[fromEntity].References.Add(new Reference(fromReference));

								Lookups.EntityLookups[fromEntity].Parent = fromEntity.Parent;
							}

							foreach (IEntity fromEntity in fromProject.Entities)
							{
								IEntity newEntity = Lookups.EntityLookups[fromEntity];

								foreach (IEntity fromChild in fromEntity.Children)
								{
									IEntity newChild = Lookups.EntityLookups[fromChild];
									newChild.Parent = newEntity;
									newEntity.Children.Add(newChild);
								}
							}
						}

						private UserSettings _Settings;
						public UserSettings Settings { get { if (_Settings == null) _Settings = new UserSettings(); return _Settings; } }

						public class UserSettings
						{
					");
			sb.Append(GetUserSettingProperties(""));
			sb.Append(@"
						}
					}
					[Serializable]
					public class Property : IProperty
					{
						public Property(IProperty fromProperty) : this(fromProperty, Lookups.EntityLookups[fromProperty.Parent])
						{
						}

						public Property(IProperty fromProperty, IEntity parent)
						{
							Name = fromProperty.Name;
							Type = fromProperty.Type;
							IsInherited = fromProperty.IsInherited;
							IsSetterPrivate = fromProperty.IsSetterPrivate;
							IsKeyProperty = fromProperty.IsKeyProperty;
							PreviousNames = fromProperty.PreviousNames;
							MappedColumnName = fromProperty.MappedColumnName;
							MappedColumn = fromProperty.MappedColumn == null ? null : Lookups.ColumnLookups[fromProperty.MappedColumn];
							ScriptObject = fromProperty.ScriptObject;
							Access = fromProperty.Access;
							Formula = fromProperty.Formula;
							Insert = fromProperty.Insert;
							Update = fromProperty.Update;
							OptimisticLock = fromProperty.OptimisticLock;
							Generate = fromProperty.Generate;
							IsNullable = fromProperty.IsNullable;
							IsVersionProperty = fromProperty.IsVersionProperty;
							Parent = parent;//Lookups.EntityLookups[fromProperty.Parent];

							if (!Lookups.PropertyLookups.ContainsKey(fromProperty))
								Lookups.PropertyLookups.Add(fromProperty, this);
						}

						private UserSettings _Settings;
						public UserSettings Settings { get { if (_Settings == null) _Settings = new UserSettings((IScriptBaseObject)this.ScriptObject); return _Settings; } }

						public class UserSettings
						{
							IScriptBaseObject ScriptObject;
							internal UserSettings(IScriptBaseObject scriptObject)
							{
								ScriptObject = scriptObject;
							}
					");
			sb.Append(GetUserSettingProperties("ArchAngel.Providers.EntityModel.Model.EntityLayer.Property"));
			sb.Append(@"
						}
					}
					[Serializable]
					public class Reference : IReference
					{
						public Reference()
						{
							KeyColumns = new List<IColumn>();
						}

						public Reference(IReference fromReference)
						{
							Name = fromReference.Name;
							ToName = fromReference.ToName;
							Type = fromReference.Type;
							ToEntity = Lookups.EntityLookups[fromReference.ToEntity];

							if (fromReference.CollectionIndexColumn != null)
								CollectionIndexColumn = Lookups.ColumnLookups[fromReference.CollectionIndexColumn];

							if (fromReference.ManyToManyAssociationTable != null)
								ManyToManyAssociationTable = Lookups.TableLookups[fromReference.ManyToManyAssociationTable];

							CollectionType = (CollectionTypes)Enum.Parse(typeof(CollectionTypes), fromReference.CollectionType.ToString(), true);
							FetchType = (FetchTypes)Enum.Parse(typeof(FetchTypes), fromReference.FetchType.ToString(), true);
							LazyType = (LazyTypes)Enum.Parse(typeof(LazyTypes), fromReference.LazyType.ToString(), true);
							CascadeType = (CascadeTypes)Enum.Parse(typeof(CascadeTypes), fromReference.CascadeType.ToString(), true);
							CollectionCascadeType = (CollectionCascadeTypes)Enum.Parse(typeof(CollectionCascadeTypes), fromReference.CollectionCascadeType.ToString(), true);
							ReferenceType = (ReferenceTypes)Enum.Parse(typeof(ReferenceTypes), fromReference.ReferenceType.ToString(), true);
							KeyType = (ReferenceKeyTypes)Enum.Parse(typeof(ReferenceKeyTypes), fromReference.KeyType.ToString(), true);
							IsSetterPrivate = fromReference.IsSetterPrivate;
							Inverse = fromReference.Inverse;
							Insert = fromReference.Insert;
							Update = fromReference.Update;
							OrderByProperty = fromReference.OrderByProperty == null ? null : Lookups.PropertyLookups[fromReference.OrderByProperty];
							OrderByIsAsc = fromReference.OrderByIsAsc;

							foreach (IColumn fromColumn in fromReference.KeyColumns)
								this.KeyColumns.Add(Lookups.ColumnLookups[fromColumn]);

							foreach (IColumn toColumn in fromReference.ToKeyColumns)
								this.ToKeyColumns.Add(Lookups.ColumnLookups[toColumn]);
						}

						/*public IScriptBaseObject ScriptObject { get; set; }

						private UserSettings _Settings;
						public UserSettings Settings { get { if (_Settings == null) _Settings = new UserSettings((IScriptBaseObject)this.ScriptObject); return _Settings; } }

						public class UserSettings
						{
							IScriptBaseObject ScriptObject;

							internal UserSettings(IScriptBaseObject scriptObject)
							{
								ScriptObject = scriptObject;
							}
					");
			sb.Append(GetUserSettingProperties("ArchAngel.Providers.EntityModel.Model.EntityLayer.Reference"));
			sb.Append(@"
						}*/
					}
					[Serializable]
					public class Table : ITable
					{
						public Table(string databaseName) : base(databaseName)
						{
							Columns = new List<IColumn>();
							Indexes = new List<IIndex>();
							ForeignKeys = new List<IKey>();
						}

						public Table(ITable fromTable) : base(fromTable.DatabaseName)
						{
							Name = fromTable.Name;
							Schema = fromTable.Schema;
							DatabaseName = fromTable.DatabaseName;
							ScriptObject = fromTable.ScriptObject;
							Columns = new List<IColumn>();
							Indexes = new List<IIndex>();
							ForeignKeys = new List<IKey>();
							PrimaryKey = fromTable.PrimaryKey;
							IsView = fromTable.IsView;

							foreach (IColumn fromColumn in fromTable.Columns)
								Columns.Add(new Column(fromColumn));

							foreach (IIndex fromIndex in fromTable.Indexes)
								Indexes.Add(new Index(fromIndex));

							foreach (IKey fromKey in fromTable.ForeignKeys)
								ForeignKeys.Add(new Key(fromKey));

							Lookups.TableLookups.Add(fromTable, this);
						}

						private UserSettings _Settings;
						public UserSettings Settings { get { if (_Settings == null) _Settings = new UserSettings((IScriptBaseObject)this.ScriptObject); return _Settings; } }

						public class UserSettings
						{
							IScriptBaseObject ScriptObject;

							internal UserSettings(IScriptBaseObject scriptObject)
							{
								ScriptObject = scriptObject;
							}
					");
			sb.Append(GetUserSettingProperties("ArchAngel.Providers.EntityModel.Model.EntityLayer.Table"));
			sb.Append(@"
						}
					}
					");
			return sb.ToString();
		}

		private static string GetUserOptionObjectName(ArchAngel.Interfaces.ITemplate.IOption userOption)
		{
			switch (userOption.IteratorName)
			{
				case "": return "Project : ProjectBase";
				case "ArchAngel.Providers.EntityModel.Model.EntityLayer.Entity": return "Entity";
				case "ArchAngel.Providers.EntityModel.Model.EntityLayer.Reference": return "Reference";
				case "ArchAngel.Providers.EntityModel.Model.EntityLayer.Component": return "ComponentProperty";
				case "ArchAngel.Providers.EntityModel.Model.EntityLayer.ComponentSpecification": return "Component";
				case "ArchAngel.Providers.EntityModel.Model.EntityLayer.ComponentProperty": return "Field";
				case "ArchAngel.Providers.EntityModel.Model.EntityLayer.Table": return "Table";
				case "ArchAngel.Providers.EntityModel.Model.EntityLayer.Column": return "Column";
				case "ArchAngel.Providers.EntityModel.Model.EntityLayer.Property": return "Property";
				default:
					throw new NotImplementedException("IteratorName not handled yet: " + userOption.IteratorName);
			}
		}

		private static string GetUserOptionClassName(string iteratorName)
		{
			switch (iteratorName)
			{
				case "": return "Project";
				case "ArchAngel.Providers.EntityModel.Model.EntityLayer.Entity": return "Entity";
				case "ArchAngel.Providers.EntityModel.Model.EntityLayer.Reference": return "Reference";
				case "ArchAngel.Providers.EntityModel.Model.EntityLayer.Component": return "ComponentProperty";
				case "ArchAngel.Providers.EntityModel.Model.EntityLayer.ComponentSpecification": return "Component";
				case "ArchAngel.Providers.EntityModel.Model.EntityLayer.ComponentProperty": return "Field";
				case "ArchAngel.Providers.EntityModel.Model.EntityLayer.Table": return "Table";
				case "ArchAngel.Providers.EntityModel.Model.EntityLayer.Column": return "Column";
				case "ArchAngel.Providers.EntityModel.Model.EntityLayer.Property": return "Property";
				default:
					throw new NotImplementedException("IteratorName not handled yet: " + iteratorName);
			}
		}

		private static string GetUserOptionTypeName(ArchAngel.Interfaces.ITemplate.IOption userOption)
		{
			Type t = Nullable.GetUnderlyingType(userOption.VarType);

			if (t != null)
			{
				if (t.Name == "Boolean")
					return "bool?";
				else if (t.Name == "Int32")
					return "int?";
				else
					throw new NotImplementedException("Nullable type not handled yet: " + t.FullName);
			}
			else if (userOption.VarType.FullName == "ArchAngel.Interfaces.SourceCodeType")
				return "string";
			else if (userOption.VarType.FullName == "ArchAngel.Interfaces.SourceCodeMultiLineType")
				return "string";
			else if (userOption.VarType.FullName == "System.String")
				return "string";
			else if (userOption.VarType.FullName == "System.Int32")
				return "int";
			else if (userOption.VarType.FullName == "System.Boolean")
				return "bool";
			else if (userOption.VarType.FullName.StartsWith("ArchAngel.NHibernateHelper.") ||
				userOption.VarType.FullName.StartsWith("ArchAngel.Interfaces.NHibernateEnums."))
			{
				return userOption.VarType.FullName;
			}
			else
				throw new NotImplementedException("Type not handled yet: " + userOption.VarType.FullName);
		}

		private static string GetFunctionBaseClass()
		{
			return @"
						public class FunctionBase
						{
							protected static Stack<StringBuilder> _SBStack = new Stack<StringBuilder>();

							public static string CurrentFilePath
							{
								get { return FunctionProcessor.CurrentFilePath; }
								set { FunctionProcessor.CurrentFilePath = value; }
							}
							
							public static bool SkipThisFile
							{
								get { return FunctionProcessor.SkipFile; }
								set { FunctionProcessor.SkipFile = value; }
							}

							public static Project Project
							{
								get { return (FunctionRunner.Project)FunctionProcessor.Project; }
								set { FunctionProcessor.Project = (FunctionRunner.Project)value; }
							}

							private static StringBuilder GetCurrentStringBuilder()
							{
								return _SBStack.Peek();
							}

							protected static void Write(object s)
							{
								if (s != null)
								{
									StringBuilder b = _SBStack.Peek();
									b.Insert(b.Length, s.ToString());
								}
							}
							
							protected static void WriteLine(object s)
							{
								if (s != null)
								{
									StringBuilder b = _SBStack.Peek();
									b.Insert(b.Length, s.ToString() + Environment.NewLine);
								}
							}

							protected static void WriteFormat(string format, params object[] args)
							{
								if (!string.IsNullOrEmpty(format))
								{
									StringBuilder b = _SBStack.Peek();
									b.Insert(b.Length, string.Format(format, args));
								}
							}

							protected static void WriteIf(bool val, object trueText)
							{
								if (val && trueText != null)
								{
									StringBuilder b = _SBStack.Peek();
									b.Insert(b.Length, trueText.ToString());
								}
							}

							protected static void WriteIf(bool val, object trueText, object falseText)
							{
								if (val && trueText != null)
								{
									StringBuilder b = _SBStack.Peek();
									b.Insert(b.Length, trueText.ToString());
								}
								else if (!val && falseText != null)
								{
									StringBuilder b = _SBStack.Peek();
									b.Insert(b.Length, falseText.ToString());
								}
							}    
						}";
		}

		private string GetFillScriptModelFunctionCode()
		{
			return @"
					public static object Convert(object obj)
					{
						if (obj is IEntity)
						{
							if (Lookups.EntityLookups.ContainsKey((IEntity)obj))
								return Lookups.EntityLookups[(IEntity)obj];

							return new Entity((IEntity)obj);
						}
						if (obj is IProject) return new Project((IProject)obj);

						if (obj is ITable)
						{
							if (Lookups.TableLookups.ContainsKey((ITable)obj))
								return Lookups.TableLookups[(ITable)obj];

							return new Table((ITable)obj);
						}
						if (obj is IColumn)
						{
							if (Lookups.ColumnLookups.ContainsKey((IColumn)obj))
								return Lookups.ColumnLookups[(IColumn)obj];

							return new Column((IColumn)obj);
						}
						if (obj is IComponent) return new Component((IComponent)obj);
						if (obj is IReference) return new Reference((IReference)obj);
						throw new NotImplementedException(""Convert() doesn't handle this type of object yet: "" + obj.GetType().FullName);
					}
			";
		}
	}
}
