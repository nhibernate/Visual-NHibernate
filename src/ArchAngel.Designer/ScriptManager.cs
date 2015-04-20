using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;

namespace ArchAngel.Designer
{
	/// <summary>
	/// Summary description for ScriptManager.
	/// </summary>
	public class ScriptManager : MarshalByRefObject, IScriptManager
	{
		private static void AddReferencesFromFile(CompilerParameters compilerParams, string nrfFile)
		{
			using (StreamReader reader = new StreamReader(nrfFile))
			{
				string line;
				bool systemXmlFound = false;
				bool systemFound = false;

				while ((line = reader.ReadLine()) != null)
				{
					if (Slyce.Common.Utility.StringsAreEqual(line, "system.xml.dll", false)) { systemXmlFound = true; }
					if (Slyce.Common.Utility.StringsAreEqual(line, "system.dll", false)) { systemFound = true; }
					compilerParams.ReferencedAssemblies.Add(line);
				}
				// Ensure that System.Xml is always included
				if (!systemXmlFound) { compilerParams.ReferencedAssemblies.Add("System.Xml.dll"); }
				if (!systemFound) { compilerParams.ReferencedAssemblies.Add("System.dll"); }
			}
		}

		#region Implementation of IScriptManager

		public void CompileAndExecuteFile(string[] files, string[] args, IScriptManagerCallback callback, string[] filesToEmbed)
		{
			const int numITemplateFiles = 5;
			string[] extraFiles = new string[files.Length + numITemplateFiles];

			for (int i = 0; i < files.Length; i++)
			{
				extraFiles[i] = files[i];
			}
			StreamReader sr = null;
			StreamWriter sw = null;

			try
			{
				string tempFile = Path.Combine(Path.GetTempPath(), "Folder.cs");
				sr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(@"SlyceScripter.Config.Folder.cs"));
				sw = File.CreateText(tempFile);
				sw.Write(sr.ReadToEnd());
				sr.Close();
				sw.Flush();
				sw.Close();
				extraFiles[files.Length] = tempFile;

				tempFile = Path.Combine(Path.GetTempPath(), "Option.cs");
				sr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(@"SlyceScripter.Config.Option.cs"));
				sw = File.CreateText(tempFile);
				sw.Write(sr.ReadToEnd());
				sr.Close();
				sw.Flush();
				sw.Close();
				extraFiles[files.Length + 1] = tempFile;

				tempFile = Path.Combine(Path.GetTempPath(), "Output.cs");
				sr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(@"SlyceScripter.Config.Output.cs"));
				sw = File.CreateText(tempFile);
				sw.Write(sr.ReadToEnd());
				sr.Close();
				sw.Flush();
				sw.Close();
				extraFiles[files.Length + 2] = tempFile;

				tempFile = Path.Combine(Path.GetTempPath(), "Project.cs");
				sr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(@"SlyceScripter.Config.Project.cs"));
				sw = File.CreateText(tempFile);
				sw.Write(sr.ReadToEnd());
				sr.Close();
				sw.Flush();
				sw.Close();
				extraFiles[files.Length + 3] = tempFile;

				tempFile = Path.Combine(Path.GetTempPath(), "Script.cs");
				sr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(@"SlyceScripter.Config.Script.cs"));
				sw = File.CreateText(tempFile);
				sw.Write(sr.ReadToEnd());
				sr.Close();
				sw.Flush();
				sw.Close();
				extraFiles[files.Length + 4] = tempFile;
			}
			catch
			{
				if (sr != null) { sr.Close(); }
				if (sw != null) { sw.Close(); }
			}
			files = extraFiles;
			string outputFileName = "";
			string nrfFileName = "";

			foreach (string arg in args)
			{
				if (arg.IndexOf("outputFileName=") >= 0)
				{
					outputFileName = arg.Substring("outputFileName=".Length);
				}
				else if (arg.IndexOf("nrfFileName=") >= 0)
				{
					nrfFileName = arg.Substring("nrfFileName=".Length);
				}
			}
			if (outputFileName.Length == 0)
			{
				throw new ApplicationException("Output file type is missing.");
			}
			bool isExe = (outputFileName.IndexOf(".exe") > 0);
			//Currently only csharp scripting is supported
			CodeDomProvider provider;

			string extension = Path.GetExtension(files[0]);

			switch (extension)
			{
				case ".cs":
				case ".ncs":
					provider = new Microsoft.CSharp.CSharpCodeProvider();
					break;
				case ".vb":
				case ".nvb":
					provider = new Microsoft.VisualBasic.VBCodeProvider();
					break;
				case ".njs":
				case ".js":
					provider = (CodeDomProvider)Activator.CreateInstance("Microsoft.JScript", "Microsoft.JScript.JScriptCodeProvider").Unwrap();
					break;
				default:
					throw new UnsupportedLanguageExecption(extension);
			}
			var compiler = provider.CreateCompiler();
			var compilerparams = new CompilerParameters
																			{
																				GenerateInMemory = false,
																				GenerateExecutable = isExe,
																				OutputAssembly = outputFileName
																			};
			// Embed resource file
			string optionPath = Path.Combine(Path.GetTempPath(), "options.xml");
			string allCompilerOptions = @"/res:" + optionPath + ",";
			//compilerparams.CompilerOptions = @"/res:"+ optionPath;

			// Embed extra files, but not the first one which is the .cs file to compile, and
			// not the files which implement the ITemplate functions
			for (int i = 1; i < filesToEmbed.Length; i++)
			{
				string embeddedFilePath = Path.Combine(Controller.TempPath, filesToEmbed[i]);

				if (!File.Exists(embeddedFilePath) && embeddedFilePath.Length > 0)
				{
					System.Windows.Forms.MessageBox.Show(Controller.Instance.MainForm, "File not found: " + embeddedFilePath + ". Compile aborted.", "Missing file", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
					return;
				}
				string resFilePath = Path.Combine(Path.GetDirectoryName(embeddedFilePath), Path.GetFileNameWithoutExtension(embeddedFilePath) + ".xml");
				string resFileNameOnly = Path.GetFileNameWithoutExtension(embeddedFilePath) + ".xml";
				CreateResourceFile(embeddedFilePath, resFilePath);
				File.Copy(resFilePath, resFileNameOnly, true);
				allCompilerOptions += resFilePath + ",";
				//allCompilerOptions += @"/res:"+ resFileNameOnly +",";
				//allCompilerOptions += @"/res:"+ optionPath +",";
			}
			// Remove the trailing comma
			compilerparams.CompilerOptions = allCompilerOptions.Substring(0, allCompilerOptions.Length - 1);

			//Add assembly references from nscript.nrf or <file>.nrf
			if (File.Exists(nrfFileName))
			{
				AddReferencesFromFile(compilerparams, nrfFileName);
			}
			else
			{
				//Use nscript.nrf
				string nrfFile = Path.Combine(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), "nscript.nrf");

				if (File.Exists(nrfFile))
				{
					AddReferencesFromFile(compilerparams, nrfFile);
				}
			}
			CompilerResults results = compiler.CompileAssemblyFromFileBatch(compilerparams, files);

			if (results.Errors.HasErrors)
			{
				System.Collections.ArrayList templist = new System.Collections.ArrayList();

				foreach (System.CodeDom.Compiler.CompilerError error in results.Errors)
				{
					templist.Add(new CompilerError(error));
				}
				callback.OnCompilerError((CompilerError[])templist.ToArray(typeof(CompilerError)));
			}
		}

		private static void CreateResourceFile(string inputFile, string outputFile)
		{
			string fileName = Path.GetFileName(inputFile);
			System.Resources.IResourceWriter writer = null;
			FileStream fs = null;
			BinaryReader br = null;

			try
			{
				//writer	= new System.Resources.ResourceWriter(outputFile);
				writer = new System.Resources.ResXResourceWriter(outputFile);
				fs = new FileStream(inputFile, FileMode.Open);
				br = new BinaryReader(fs);
				byte[] myBuffer = new byte[br.BaseStream.Length];

				for (long i = 0; i < br.BaseStream.Length; i++)
				{
					myBuffer[i] = br.ReadByte();
				}
				writer.AddResource(fileName, myBuffer);
				writer.Generate();
				//writer.Close();
			}
			finally
			{
				if (writer != null) { writer.Close(); }
				if (fs != null) { fs.Close(); }
				if (br != null) { br.Close(); }
			}
		}
		#endregion
	}
}
