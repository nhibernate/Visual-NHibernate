using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Windows.Forms;
using ArchAngel.Designer.Properties;
using ArchAngel.Providers.CodeProvider;
using Slyce.Common;
using Slyce.Common.Controls;
using Slyce.Loader;

namespace ArchAngel.Designer
{
	public partial class frmTestFunction : Form
	{
		Project.FunctionInfo TheFunction;
		private bool[] ValuesThatHaveBeenSet = null;
		private object[] ParametersToPass;
		private Color InvalidColor = Color.Orange;
		private bool TemplateHasBeenLoaded = false;
		private bool ProjectHasBeenCompiled = false;
		internal static Dictionary<Project.FunctionInfo, List<object>> CachedParameteres = new Dictionary<Project.FunctionInfo, List<object>>();
		private static string OriginalCompileFolderName;
		private static string TempCompileFolderName;

		public frmTestFunction()
		{
            throw new Exception("This form shouldn't be ued anymore - tell Jamie about this.");
            
            InitializeComponent();
			ucHeading1.Text = "Parameters";
			ucHeading2.Text = "Generated Output";
			ucHeading3.Text = "";
			lblPageHeader.Text = "Preview Function Output";
			lblPageDescription.Text = "Set the parameter values, then click Run to preview the function's text output.";
			Controller.ShadeMainForm();
			string outputSyntaxFilePath = Slyce.Common.SyntaxEditorHelper.GetLanguageFileName(Project.Instance.TextLanguage);

			if (Project.Instance.TextLanguage != TemplateContentLanguage.CSharp)
			{
				tabStrip1.Pages.Remove(tabFormatted);
			}
			else
			{
				syntaxEditorFormatted.Document.Language = ActiproSoftware.SyntaxEditor.Addons.Dynamic.DynamicSyntaxLanguage.LoadFromXml(outputSyntaxFilePath, 0);
			}
			btnRun.Enabled = false;
			OriginalCompileFolderName = Project.Instance.CompileFolderName;
			Project.Instance.CompileFolderName = TempCompileFolderName;
			syntaxEditor1.Document.Language = ActiproSoftware.SyntaxEditor.Addons.Dynamic.DynamicSyntaxLanguage.LoadFromXml(outputSyntaxFilePath, 0);
			ucHeading3.Text = "Recompiling...";
			backgroundWorker1.RunWorkerAsync();
		}

		public void ShowFunction(IWin32Window owner, Project.FunctionInfo function)
		{
			TheFunction = function;
			Populate();
			Cursor = Cursors.Default;
			this.ShowDialog(owner);
		}

		private void Populate()
		{
			this.Text = "Preview: " + TheFunction.Name;
			panel1.Controls.Clear();
			int gap = 5;
			int currentTop = 0;
			int maxLabelWidth = 0;
			System.Drawing.Font font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			Label measureLabel = new Label();
			measureLabel.BackColor = Color.Transparent;
			measureLabel.Font = font;
			Graphics graphics = Graphics.FromHwnd(measureLabel.Handle);
			string displayText;
			ParametersToPass = new object[TheFunction.Parameters.Length];
			bool parametersMatch = false;
			List<object> parameterObjects = null;
			ValuesThatHaveBeenSet = new bool[TheFunction.Parameters.Length];

			if (CachedParameteres.ContainsKey(TheFunction))
			{
				parameterObjects = CachedParameteres[TheFunction];
				parametersMatch = TheFunction.Parameters.Length == parameterObjects.Count;

				if (parametersMatch)
				{
					for (int i = 0; i < TheFunction.Parameters.Length; i++)
					{
						if (parameterObjects[i] == null || !TheFunction.Parameters[i].DataType.IsInstanceOfType(parameterObjects[i]))
						{
							parametersMatch = false;
						}
					}
				}
			}
			for (int i = 0; i < TheFunction.Parameters.Length; i++)
			{
				Project.ParamInfo param = TheFunction.Parameters[i];
				displayText = string.Format("{2}. {0} ({1}):", param.Name, param.DataType, i + 1);
				maxLabelWidth = (float)maxLabelWidth > graphics.MeasureString(displayText, font).Width ? maxLabelWidth : (int)graphics.MeasureString(displayText, new Font("Microsoft Sans Serif", 8.25f)).Width;

				if (parametersMatch)
				{
					ParametersToPass[i] = parameterObjects[i];
					ValuesThatHaveBeenSet[i] = true;
				}
			}
			for (int i = 0; i < TheFunction.Parameters.Length; i++)
			{
				Project.ParamInfo param = TheFunction.Parameters[i];
				// New implementation
				Label lbl = new Label();
				lbl.Text = string.Format("{2}. {0} ({1}):", param.Name, param.DataType, i + 1);
				lbl.TextAlign = ContentAlignment.BottomLeft;
				lbl.Width = maxLabelWidth + gap;
				panel1.Controls.Add(lbl);
				lbl.Left = gap;

				if (i == 0)
				{
					currentTop = 0;
				}
				else
				{
					currentTop += lbl.Height + gap;
				}
				lbl.Top = currentTop;
				currentTop = lbl.Bottom;
				//                typeof(ArchAngel.Interfaces.IScriptBaseObject).IsInstanceOfType(param.DataType)
				if (param.DataType.GetInterface("ArchAngel.Interfaces.IScriptBaseObject") != null)
				{
					Label txt = new Label();
					txt.BackColor = InvalidColor;
					txt.Top = currentTop;
					txt.Left = gap * 4;
					txt.Tag = param;
					txt.BorderStyle = BorderStyle.FixedSingle;

					if (parametersMatch)
					{
						txt.Text = ArchAngel.Interfaces.ProjectHelper.GetDisplayName(ParametersToPass[i]);
					}
					panel1.Controls.Add(txt);

					Button btnBrowseArchAngelObject = new Button();
					btnBrowseArchAngelObject.Text = "...";
					btnBrowseArchAngelObject.BackColor = Color.FromKnownColor(KnownColor.Control);
					btnBrowseArchAngelObject.Width = btnBrowseArchAngelObject.Height + gap;
					btnBrowseArchAngelObject.Top = currentTop;
					btnBrowseArchAngelObject.Tag = txt;
					btnBrowseArchAngelObject.Name = string.Format("btnBrowseArchAngelObject_{0}", i);
					btnBrowseArchAngelObject.Click += new EventHandler(btnBrowseArchAngelObject_Click);
					btnBrowseArchAngelObject.Left = txt.Right + gap;
					panel1.Controls.Add(btnBrowseArchAngelObject);
				}
				else
				{
					switch (param.DataType.Name.ToLower())
					{
						case "bool":
						case "boolean":
							CheckBox chk = new CheckBox();
							chk.Top = currentTop;
							chk.Left = gap * 4;
							chk.Name = string.Format("chk_{0}", i);
							chk.CheckedChanged += new EventHandler(chk_CheckedChanged);
							panel1.Controls.Add(chk);
							break;
						case "int":
						case "int32":
							NumEdit numericInt = new NumEdit();
							numericInt.BackColor = InvalidColor;
							numericInt.InputType = NumEdit.NumEditType.Integer;
							numericInt.Top = currentTop;
							numericInt.Left = gap * 4;
							numericInt.Name = string.Format("numericInt_{0}", i);
							numericInt.TextChanged += new EventHandler(numericInt_TextChanged);
							panel1.Controls.Add(numericInt);
							break;
						case "double":
							NumEdit numericDouble = new NumEdit();
							numericDouble.BackColor = InvalidColor;
							numericDouble.InputType = NumEdit.NumEditType.Double;
							numericDouble.Top = currentTop;
							numericDouble.Left = gap * 4;
							numericDouble.Name = string.Format("numericDouble_{0}", i);
							numericDouble.TextChanged += new EventHandler(numericDouble_TextChanged);
							panel1.Controls.Add(numericDouble);
							break;
						case "string":
							TextBox txt2 = new TextBox();
							txt2.BackColor = InvalidColor;
							txt2.Text = "Not set";
							txt2.Top = currentTop;
							txt2.Left = gap * 4;
							txt2.Name = string.Format("txt2_{0}", i);
							txt2.TextChanged += new EventHandler(txt2_TextChanged);
							panel1.Controls.Add(txt2);
							break;
						default:
							MessageBox.Show(this, "Data type not handled yet: " + param.DataType.Name, "Unexpected Data-Type", MessageBoxButtons.OK, MessageBoxIcon.Warning);
							break;
					}
				}
			}
			//if (parametersMatch)
			//{
			//    Run();
			//}
		}

		void txt2_TextChanged(object sender, EventArgs e)
		{
			TextBox txt = (TextBox)sender;
			txt.BackColor = Color.White;
			int index = int.Parse(txt.Name.Substring(txt.Name.LastIndexOf("_") + 1));
			ParametersToPass[index] = txt.Text;
			ValuesThatHaveBeenSet[index] = true;
		}

		void numericInt_TextChanged(object sender, EventArgs e)
		{
			NumEdit numEdit = (NumEdit)sender;
			int index = int.Parse(numEdit.Name.Substring(numEdit.Name.LastIndexOf("_") + 1));

			if (!string.IsNullOrEmpty(numEdit.Text))
			{
				numEdit.BackColor = Color.White;
				ParametersToPass[index] = int.Parse(numEdit.Text);
				ValuesThatHaveBeenSet[index] = true;
			}
			else
			{
				numEdit.BackColor = InvalidColor;
				ParametersToPass[index] = null;
				ValuesThatHaveBeenSet[index] = false;
			}
		}

		void numericDouble_TextChanged(object sender, EventArgs e)
		{
			NumEdit numEdit = (NumEdit)sender;
			int index = int.Parse(numEdit.Name.Substring(numEdit.Name.LastIndexOf("_") + 1));

			if (!string.IsNullOrEmpty(numEdit.Text))
			{
				numEdit.BackColor = Color.White;
				ParametersToPass[index] = double.Parse(numEdit.Text);
				ValuesThatHaveBeenSet[index] = true;
			}
			else
			{
				numEdit.BackColor = InvalidColor;
				ParametersToPass[index] = null;
				ValuesThatHaveBeenSet[index] = false;
			}
		}

		void chk_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox chk = (CheckBox)sender;
			int index = int.Parse(chk.Name.Substring(chk.Name.LastIndexOf("_") + 1));
			ParametersToPass[index] = chk.Checked;
			ValuesThatHaveBeenSet[index] = true;
		}

		void btnBrowseArchAngelObject_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(Settings.Default.DebugCSPDatabasePath) ||
!File.Exists(Settings.Default.DebugCSPDatabasePath))
			{
				MessageBox.Show(this, "Please select the ArchAngel project file (.aaproj) you want to read settings from on the Tools -> Options menu. \n\nThis will now be opened for you.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				frmOptions formOpt = new frmOptions();
				formOpt.ShowDialog(this);
				//this.Close();
				if (string.IsNullOrEmpty(Settings.Default.DebugCSPDatabasePath) ||
!File.Exists(Settings.Default.DebugCSPDatabasePath))
				{
					return;
				}
			}

			Cursor = Cursors.WaitCursor;
			Button button = (Button)sender;

			Type dataType = null;
			Label textBox = (Label)button.Tag;

			if (textBox.Tag.GetType() == typeof(Project.ParamInfo))
			{
				Project.ParamInfo p = (Project.ParamInfo)textBox.Tag;
				dataType = p.DataType;
			}
			else
			{
				dataType = textBox.Tag.GetType();
			}
			frmSelectModelObject form = frmSelectModelObject.Instance;
			//frmSelectModelObject form = new frmSelectModelObject();
			//form.Show();
			form.ShowObject(this, dataType);

			if (form.SelectedObject != null)
			{
				int index = int.Parse(button.Name.Substring(button.Name.LastIndexOf("_") + 1));
				ParametersToPass[index] = form.SelectedObject;

				if (textBox.Tag != form.SelectedObject)
				{
					// One of the parameters have now changed, so clear any previously generated text
					syntaxEditor1.Text = "";
					syntaxEditorFormatted.Text = "";
				}
				textBox.Tag = form.SelectedObject;

				if (form.SelectedObject != null)
				{
					textBox.Text = ArchAngel.Interfaces.ProjectHelper.GetDisplayName(form.SelectedObject);
					ValuesThatHaveBeenSet[index] = true;
				}
			}
			Cursor = Cursors.Default;
		}

		private void listView1_Click(object sender, EventArgs e)
		{

		}

		private void listView1_MouseClick(object sender, MouseEventArgs e)
		{

		}

		private void btnRun_Click(object sender, EventArgs e)
		{
			if (!ProjectHasBeenCompiled)
			{
				MessageBox.Show(this, "Project compilation has not yet finished successfully. Please wait unitl the message at the bottom of the screen disappears, then try again.", "Busy Compiling", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			for (int i = 0; i < ValuesThatHaveBeenSet.Length; i++)
			{
				if (!ValuesThatHaveBeenSet[i])
				{
					MessageBox.Show(this, "Some values haven't been set.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
			}
			if (string.IsNullOrEmpty(Settings.Default.DebugCSPDatabasePath) || !File.Exists(Settings.Default.DebugCSPDatabasePath))
			{
				MessageBox.Show(this, "Please select the ArchAngel project file (.aaproj) you want to read settings from on the Tools -> Options menu. \n\nThis will now be opened for you.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				frmOptions formOpt = new frmOptions();
				formOpt.ShowDialog(this);

				if (string.IsNullOrEmpty(Settings.Default.DebugCSPDatabasePath) || !File.Exists(Settings.Default.DebugCSPDatabasePath))
				{
					return;
				}
			}
			Run();
		}

		private void Run()
		{
			Cursor = Cursors.WaitCursor;

			try
			{
				//string compileFolder = Path.Combine(System.IO.Path.GetTempPath(), "SlyceDebugFolder");
				//string filepath = Path.Combine(compileFolder, Project.Instance.ProjectName + ".aal");
				//string filepath = Path.Combine(Project.Instance.CompileFolderName, Project.Instance.ProjectName + ".aal");
				string filepath = Path.Combine(Project.Instance.CompileFolderName, Project.Instance.ProjectName + ".aal");

				if (!File.Exists(filepath))
				{
					throw new FileNotFoundException("File is missing: " + filepath);
				}
				LoadAssembly(filepath);
				Cursor = Cursors.WaitCursor;
				CallFunction();

			}
			finally
			{
				Cursor = Cursors.Default;
			}
		}

		private void CallFunction()
		{
			try
			{
				SetUserOptionValues();
				string fileName = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "temp.aal");
				string str = (string)Loader.Instance.CallTemplateFunction(TheFunction.Name, ref ParametersToPass);

				if (TheFunction.IsTemplateFunction)
				{
					//SetSyntaxLanguage(Slyce.Common.SyntaxEditorHelper.LanguageEnumFromName(TheFunction.ReturnType.Name));
				}
				else
				{
					//SetSyntaxLanguage(Project.OutputLanguageTypes.PlainText);
				}
				syntaxEditor1.Text = str;

				if (Project.Instance.TextLanguage == TemplateContentLanguage.CSharp)
				{
					CSharpParser formatter = new CSharpParser();
					formatter.ParseCode(str);
					
					if(formatter.ErrorOccurred)
					{
						StringBuilder errorMsg = new StringBuilder();
						foreach(ParserSyntaxError error in formatter.SyntaxErrors)
						{
							errorMsg.AppendFormat("An error occurred on line {1}:\n\n{0}", error.ErrorMessage, error.LineNumber);
							errorMsg.AppendLine();
						}
						MessageBox.Show(this, errorMsg.ToString(), "Formatting Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else
					{
						str = formatter.CreatedCodeRoot.ToString();
						syntaxEditorFormatted.Text = Utility.StandardizeLineBreaks(str, Utility.LineBreaks.Windows);	
					}
				}
			}
			catch (Exception ex)
			{
				Controller.ReportError(ex);
			}
		}

		private void SetUserOptionValues()
		{
			string tempFolder = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

			while (Directory.Exists(tempFolder))
			{
				tempFolder = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
			}
			Directory.CreateDirectory(tempFolder);

			try
			{
				Slyce.Common.Utility.UnzipFile(Settings.Default.DebugCSPDatabasePath, tempFolder);
				System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
				doc.Load(Path.Combine(tempFolder, "options.xml"));

				foreach (System.Xml.XmlNode userOptionNode in doc.SelectNodes("/*/*/*"))
				{
					foreach (Project.UserOption opt in Project.Instance.UserOptions)
					{
						if (opt.VariableName == userOptionNode.Name)
						{
							object obj = null;

							switch (opt.VarType.Name.ToLower())
							{
								case "string":
									obj = userOptionNode.InnerText;
									break;
								case "int32":
									obj = int.Parse(userOptionNode.InnerText);
									break;
								case "double":
									obj = double.Parse(userOptionNode.InnerText);
									break;
								case "boolean":
									obj = bool.Parse(userOptionNode.InnerText);
									break;
								case "enum":
									obj = userOptionNode.InnerText;
									break;
								default:
									throw new NotImplementedException("This object type has not been catered for yet: " + opt.VarType.Name);
							}
							try
							{
								Loader.Instance.SetUserOption(userOptionNode.Name, obj);
							}
							catch (Exception ex)
							{
								if (ex.Message.IndexOf("not found") >= 0)
								{
									// Do nothing. This useroption is saved in the .aaproj file but has now been removed from the actual template
								}
								else
								{
									throw;
								}
							}
							break;
						}
					}
				}
			}
			finally
			{
				Slyce.Common.Utility.DeleteDirectoryBrute(tempFolder);
			}
		}

		/// <summary>
		/// Loads the new assembly, unloading the previously loaded assembly ( if it is loaded.
		/// </summary>
		/// <param name="filepath"></param>
		public bool LoadAssembly(string filepath)
		{
			if (!File.Exists(filepath))
			{
				MessageBox.Show(this, string.Format("The template file could not be loaded: \n{0}", filepath), "Template File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			if (!TemplateHasBeenLoaded)
			{
				string templateFolder = Path.GetDirectoryName(filepath);
				List<string> searchPaths = new List<string>();
				searchPaths.Add(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath));
				searchPaths.Add(Path.GetDirectoryName(filepath));
				searchPaths.Sort();

				foreach (System.Reflection.Assembly assembly in Project.Instance.ReferencedAssemblies)
				{
					string directory = Path.GetDirectoryName(assembly.Location);

					if (searchPaths.BinarySearch(directory) < 0)
					{
						searchPaths.Add(directory);
						searchPaths.Sort();
					}
#if DEBUG
					// Copy the referenced assmblies to the temp folder
					string destinationFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), Path.GetFileName(assembly.Location));

					if (!File.Exists(destinationFile))
					{
						File.Copy(assembly.Location, destinationFile);
					}
					string natFile = Path.Combine(Path.GetDirectoryName(assembly.Location), Path.GetFileNameWithoutExtension(assembly.Location) + "_nat" + Path.GetExtension(assembly.Location));
					destinationFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), Path.GetFileName(natFile));

					if (File.Exists(natFile) && !File.Exists(destinationFile))
					{
						File.Copy(natFile, destinationFile);
					}
#endif
				}
				Loader.Instance.SetTemplateFolder(templateFolder, searchPaths);

				if (!File.Exists(filepath))
				{
					throw new FileNotFoundException("File is missing: " + filepath);
				}
				Loader.ForceNewAppDomain = true;
				bool result = Loader.Instance.LoadAssembly(filepath, searchPaths, CompileHelper.NamespaceUsed);
				TemplateHasBeenLoaded = result;
			}
			return TemplateHasBeenLoaded;
		}

		private void frmTestFunction_Paint(object sender, PaintEventArgs e)
		{
			this.BackColor = Slyce.Common.Colors.BackgroundColor;
		}

		private void frmTestFunction_FormClosed(object sender, FormClosedEventArgs e)
		{
			Controller.UnshadeMainForm();
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			CompileProject();
		}

		private void CompileProject()
		{
			if (Project.Instance.ProjectChangedSinceLastCompile)
			{
				ProjectHasBeenCompiled = false;
				//string origTarget = Project.Instance.CompileFolderName;

				try
				{
					Project.Instance.CompileFolderName = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "SlyceDebugFolder_" + Path.GetRandomFileName().Replace(".", ""));
					TempCompileFolderName = Project.Instance.CompileFolderName;

					if (!Directory.Exists(Project.Instance.CompileFolderName))
					{
						Directory.CreateDirectory(Project.Instance.CompileFolderName);
					}
					bool success = Controller.Instance.MainForm.CompileProject(false, false);

					if (success)
					{
						ProjectHasBeenCompiled = true;
						Project.Instance.ProjectChangedSinceLastCompile = false;
					}
				}
				catch (Exception ex)
				{
					string gg = ex.Message;
					throw;
				}
				finally
				{
					//Project.Instance.CompileFolderName = origTarget;
				}
			}
			else
			{
				ProjectHasBeenCompiled = true;
			}
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (ProjectHasBeenCompiled)
			{
				ucHeading3.Text = "";
				btnRun.Enabled = true;
			}
			else
			{
				ucHeading3.Text = "Failed to compile";
			}
			this.Refresh();
		}

		private void frmTestFunction_FormClosing(object sender, FormClosingEventArgs e)
		{
			Project.Instance.CompileFolderName = OriginalCompileFolderName;

			if (!CachedParameteres.ContainsKey(TheFunction))
			{
				CachedParameteres.Add(TheFunction, new List<object>());
			}
			CachedParameteres[TheFunction].Clear();

			foreach (object param in ParametersToPass)
			{
				CachedParameteres[TheFunction].Add(param);
			}
		}



	}
}