using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using ActiproSoftware.SyntaxEditor;
using DevComponents.AdvTree;
using Slyce.Common;

namespace ArchAngel.Workbench.ContentItems
{
	public partial class Templates : Interfaces.Controls.ContentItems.ContentItem
	{
		private enum CrumbBarImages
		{
			Entity,
			Project,
			Component,
			Table,
			Column
		}
		private const int IMG_CLOSED_FOLDER = 0;
		private const int IMG_OPEN_FOLDER = 1;
		private const int IMG_TEMPLATE_SCRIPT = 4;
		private const int IMG_NORMAL_SCRIPT = 2;
		private const int IMG_FILE = 5;
		private const int IMG_ROOT = 3;
		private const int IMG_FOLDER_GREEN = 6;
		private const int IMG_FOLDER_ORANGE = 7;
		private const int IMG_FILE_GREEN = 8;
		private const int IMG_FILE_ORANGE = 9;
		private const int IMG_STATIC_FILE = 10;

		private Dictionary<SyntaxEditor, TemplateContentLanguage> CurrentLanguages = new Dictionary<SyntaxEditor, TemplateContentLanguage>();
		private bool BusyPopulatingErrors = false;
		private ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom.DotNetProjectResolver dotNetProjectResolver = new ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom.DotNetProjectResolver();
		private Assembly BaseAssembly;
		private string BaseAssemblyPath;
		private string TempAssembliesDir;
		private bool BusyPopulatingCrumbBarTest = false;
		private bool IsPopulated = false;
		private bool BusyPopulating = false;
		internal static Templates Instance;

		private Dictionary<ArchAngel.Interfaces.Template.IteratorTypes, object> LatestTestObjects = new Dictionary<Interfaces.Template.IteratorTypes, object>();

		public Templates()
		{
			InitializeComponent();

			SetStyle(
					ControlStyles.UserPaint |
					ControlStyles.AllPaintingInWmPaint |
					ControlStyles.OptimizedDoubleBuffer, true);

			superTabControl1.SelectedTab = superTabItemScript;
			Instance = this;

			foreach (Slyce.Common.TemplateContentLanguage value in Enum.GetValues(typeof(Slyce.Common.TemplateContentLanguage)))
				comboBoxSyntax.Items.Add(Slyce.Common.SyntaxEditorHelper.LanguageNameFromEnum(value));

			comboBoxEncoding.Items.Add("Unicode");
			comboBoxEncoding.Items.Add("ASCII");
			comboBoxEncoding.Items.Add("UTF8");

			comboBoxSyntax.Text = "C#";

			comboBoxIterator.Visible = false;
			syntaxEditorFilename.Visible = false;
			comboBoxSyntax.Visible = false;
			labelFilename.Visible = false;
			syntaxEditor1.Visible = false;
			CurrentLanguages.Add(syntaxEditor1, TemplateContentLanguage.CSharp);
			CurrentLanguages.Add(syntaxEditorFilename, TemplateContentLanguage.CSharp);
			CurrentLanguages.Add(syntaxEditorTest, TemplateContentLanguage.CSharp);

			//Populate();
			PopulateComboBoxTemplates();

			string delimiterStart;
			string delimiterEnd;

			if (ArchAngel.Interfaces.SharedData.CurrentProject == null)
			{
				delimiterStart = @"<%";
				delimiterEnd = @"%>";
			}
			else
			{
				delimiterStart = ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.DelimiterStart;
				delimiterEnd = ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.DelimiterEnd;
			}
			Slyce.Common.TextBoxFocusHelper tbh = new Slyce.Common.TextBoxFocusHelper(new Control[] { syntaxEditorFilename });

			SetSyntax(TemplateContentLanguage.Assembly, syntaxEditor1, syntaxEditorTest);
			SyntaxEditorHelper.SetupEditorTemplateAndScriptLanguages(syntaxEditorFilename, TemplateContentLanguage.PlainText, SyntaxEditorHelper.ScriptLanguageTypes.CSharp, delimiterStart, delimiterEnd);
			SwitchFormatting(syntaxEditorFilename);

			Controller.Instance.OnCompileErrors += new Controller.CompileErrorsDelegate(Instance_OnCompileErrors);
			bar1.Items[0].Text = "Error List";
			SyntaxEditorHelper.SetupEditorTemplateAndScriptLanguages(syntaxEditorSkipStaticFile, TemplateContentLanguage.CSharp, SyntaxEditorHelper.ScriptLanguageTypes.CSharp, delimiterStart, delimiterEnd);
		}

		public void ClearAllSelections()
		{
			if (SyntaxEditor != null)
				SyntaxEditor.SelectedView.Selection.EndOffset = SyntaxEditor.SelectedView.Selection.StartOffset;
		}

		public void ShowFindResults()
		{
			//lblStatus.Visible = false;
			bar1.SelectedDockContainerItem = dockContainerItemFindResults;
			((ucFindResults)dockContainerItemFindResults.Control.Controls.Find("ucFindResults1", false)[0]).ShowResults();
		}

		public SyntaxEditor SyntaxEditor
		{
			get
			{
				if (syntaxEditor1.Visible)
					return syntaxEditor1;

				return null;
			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				// Stop the parser service... note that you should only do this if you are closing your application down
				//   and are sure no other languages are still using the service... in the case of this sample project,
				//   each QuickStart Form is modal so we know another window doesn't have a language that is still accessing the service
				SemanticParserService.Stop();

				// Dispose the project resolver... this releases all its resources
				try
				{
					dotNetProjectResolver.Dispose();
				}
				catch
				{
					// Do nothing
				}

				// Prune the cache to remove files that no longer apply... note that you should only do this if you are closing your application down
				//   and are sure no other project resolvers are still using the cache... in the case of this sample project, 
				//   each QuickStart Form is modal so we know another window is still not accessing the cache
				dotNetProjectResolver.PruneCache();

				if (components != null)
					components.Dispose();
			}
			base.Dispose(disposing);
		}

		List<ArchAngel.Interfaces.Template.File> FilesWithErrors = new List<ArchAngel.Interfaces.Template.File>();
		List<ArchAngel.Interfaces.Template.Folder> FoldersWithErrors = new List<ArchAngel.Interfaces.Template.Folder>();

		void Instance_OnCompileErrors(List<System.CodeDom.Compiler.CompilerError> errors)
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(() => Instance_OnCompileErrors(errors)));
				return;
			}
			BusyPopulatingErrors = true;

			if (!IsPopulated)
				Populate();

			bool originalBusyPopulating = BusyPopulating;
			BusyPopulating = true;
			Controller.Instance.MainForm.ShowContentItem(this);
			BusyPopulating = originalBusyPopulating;

			if (!BusyPopulating)
				MessageBox.Show(this, "Errors occurred in the template. See bottom of screen.", "Template Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);

			FilesWithErrors.Clear();
			FoldersWithErrors.Clear();
			treeFiles.BeginUpdate();
			ResetNodeImages();
			dataGridViewErrors.Rows.Clear();

			foreach (var err in errors)
			{
				//throw new Exception("TODO: Work out how to handle filename: file vs. static file");
				ArchAngel.Common.Generator.DebugPos debugPos = ArchAngel.Common.Generator.GetDebugPos(err.FileName, err.Line, err.Column, string.Format("Error: {0}\n{1}", err.ErrorText, err.FileName));
				Node node = treeFiles.FindNodeByDataKey(err.FileName);

				if (node != null)
				{
					node.ImageIndex = IMG_FILE_ORANGE;
					string filename = node.Text;
					string functionName = debugPos.FunctionName == "GetFileName" ? "Filename" : "Body";
					dataGridViewErrors.Rows.Add(err.FileName, "", err.ErrorText, filename, functionName, debugPos.Line, err.Column - 10);

					if (node.Tag is ArchAngel.Interfaces.Template.File)
						FilesWithErrors.Add((ArchAngel.Interfaces.Template.File)node.Tag);
					else if (node.Tag is ArchAngel.Interfaces.Template.Folder)
						FoldersWithErrors.Add((ArchAngel.Interfaces.Template.Folder)node.Tag);
				}
				else
				{
					MessageBox.Show(string.Format("Can't find node in tree: Filename [{0}] Col[{1}] Line [{2}] Text [{3}]", err.FileName, err.Column, err.Line, err.ErrorText));
				}
			}
			treeFiles.EndUpdate();
			treeFiles.Refresh();
			BusyPopulatingErrors = false;
		}

		private void ResetNodeImages()
		{
			foreach (Node node in treeFiles.Nodes)
				ResetNodeImages(node);
		}

		private void ResetNodeImages(Node node)
		{
			if (node.Tag is ArchAngel.Interfaces.Template.Folder)
			{
				node.ImageIndex = IMG_CLOSED_FOLDER;

				foreach (Node subNode in node.Nodes)
					ResetNodeImages(subNode);
			}
			else if (node.Tag is ArchAngel.Interfaces.Template.File)
				node.ImageIndex = IMG_FILE_GREEN;
		}

		private void PopulateIterators(bool isFile)
		{
			comboBoxIterator.Items.Clear();
			string val = isFile ? "file" : "folder";

			foreach (ArchAngel.Interfaces.Template.IteratorTypes value in Enum.GetValues(typeof(ArchAngel.Interfaces.Template.IteratorTypes)))
			{
				if (value == ArchAngel.Interfaces.Template.IteratorTypes.None)
					comboBoxIterator.Items.Add("one " + val);
				else
					comboBoxIterator.Items.Add(string.Format("one {0} per {1}", val, value));
			}
			comboBoxIterator.Text = "one " + val;
		}

		private ArchAngel.Interfaces.Template.TemplateProject TemplateProject { get; set; }

		internal void Populate()
		{
			if (BusyPopulating)
				return;

			try
			{
				if (ArchAngel.Interfaces.SharedData.CurrentProject == null)
					return;

				BusyPopulating = true;

				if (ArchAngel.Interfaces.SharedData.CurrentProject != null)
				{
					if (ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject == null)
						ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject = ArchAngel.Common.UserTemplateHelper.GetDefaultTemplate();

					TemplateProject = ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject;
					ArchAngel.Interfaces.SharedData.CurrentProject.InitialiseScriptObjects();
				}
				if (TemplateProject == null)
				{
					comboBoxTemplates.SelectedIndex = 0;
					comboBoxDelimiter.SelectedIndex = 0;
				}
				else
				{
					for (int i = 0; i < comboBoxTemplates.Items.Count; i++)
					{
						if (comboBoxTemplates.Items[i] is string)
							continue;

						ArchAngel.Interfaces.Template.TemplateProject p = (ArchAngel.Interfaces.Template.TemplateProject)comboBoxTemplates.Items[i];

						if (p.Name == TemplateProject.Name && p.IsOfficial == TemplateProject.IsOfficial)
						{
							comboBoxTemplates.SelectedIndex = i;
							break;
						}
					}
					if (TemplateProject.Delimiter == Interfaces.Template.TemplateProject.DelimiterTypes.ASP)
						comboBoxDelimiter.SelectedIndex = 0;
					else
						comboBoxDelimiter.SelectedIndex = 1;
				}
				PopulateTree();
				PopulateStaticFiles();

				#region Intellisense setup
				// Start the parser service (only call this once at the start of your application)
				SemanticParserService.Start();
				string temp = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Branding.ProductName + Path.DirectorySeparatorChar + "Temp");

				if (!Directory.Exists(temp))
					Directory.CreateDirectory(temp);

				string intellisenseCacheDir = Path.Combine(temp, @"IntellisenseCache");
				TempAssembliesDir = Path.Combine(temp, @"TempAssemblies");

				if (!Directory.Exists(intellisenseCacheDir))
					Directory.CreateDirectory(intellisenseCacheDir);

				dotNetProjectResolver.CachePath = intellisenseCacheDir;
				//AddMainFunctionAssemblyToProjectResolver();
				ActiproSoftware.SyntaxEditor.Addons.CSharp.CSharpSyntaxLanguage cSharpLanguage = new ActiproSoftware.SyntaxEditor.Addons.CSharp.CSharpSyntaxLanguage();
				syntaxEditorOffscreen.Document.Language = cSharpLanguage;
				syntaxEditorOffscreen.Document.LanguageData = dotNetProjectResolver;
				syntaxEditorOffscreen.Document.Filename = System.Guid.NewGuid().ToString() + ".cs";

				ArchAngel.Common.Generator gen = new ArchAngel.Common.Generator(null, Controller.Instance.CurrentProject.TemplateLoader);
				gen.ClearAllDebugLines();

				if (!backgroundWorkerAddReferences.IsBusy)
					backgroundWorkerAddReferences.RunWorkerAsync();

				#endregion

				IsPopulated = true;

				#region Compile to find errors
				string exeDir = Path.GetDirectoryName(Application.ExecutablePath);
				List<string> referencedAssemblies = new List<string>();
				//referencedAssemblies.Add(Path.Combine(exeDir, "ArchAngel.Scripting.dll"));
				referencedAssemblies.Add(Path.Combine(exeDir, "ArchAngel.Interfaces.dll"));
				referencedAssemblies.Add(Path.Combine(exeDir, "ArchAngel.Providers.EntityModel.dll"));
				referencedAssemblies.Add(Path.Combine(exeDir, "ArchAngel.NHibernateHelper.dll"));
#if DEBUG
				string path = Slyce.Common.RelativePaths.GetFullPath(System.Reflection.Assembly.GetExecutingAssembly().Location, @"..\..\..\..\3rd_Party_Libs\Inflector.Net.dll");
				referencedAssemblies.Add(path);
#else
				referencedAssemblies.Add(Path.Combine(exeDir, "Inflector.Net.dll"));
#endif
				List<System.CodeDom.Compiler.CompilerError> compileErrors;

				gen.CompileCombinedAssembly(ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject,
					referencedAssemblies,
					null,
					out compileErrors,
					false,
					null);

				if (compileErrors.Count > 0)
					Instance_OnCompileErrors(compileErrors);

				#endregion
			}
			finally
			{
				BusyPopulating = false;
			}
		}

		private bool BusyPopulatingTree = false;

		private void PopulateTree()
		{
			if (ArchAngel.Interfaces.SharedData.CurrentProject == null)
				return;

			BusyPopulatingTree = true;
			treeFiles.BeginUpdate();
			treeFiles.Nodes.Clear();

			if (ArchAngel.Interfaces.SharedData.CurrentProject != null)
				TemplateProject = ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject;

			AddFolderNode(null, TemplateProject.OutputFolder);

			if (treeFiles.Nodes.Count > 0)
				SelectFirstFileNode(treeFiles.Nodes[0]);

			treeFiles.EndUpdate();
			ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.IsDirty = false;
			BusyPopulatingTree = false;
		}

		private void PopulateStaticFiles()
		{
			if (ArchAngel.Interfaces.SharedData.CurrentProject == null)
				return;

			listViewResources.Items.Clear();

			foreach (var file in ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.ResourceFiles.OrderBy(f => f))
				listViewResources.Items.Add(file);

			SetWidthOfStaticFileList();
		}

		private void SetWidthOfStaticFileList()
		{
			Graphics g = Graphics.FromHwnd(listViewResources.Handle);

			float maxWidth = 0;

			foreach (ListViewItem item in listViewResources.Items)
				maxWidth = Math.Max(maxWidth, g.MeasureString(item.Text, listViewResources.Font).Width);

			listViewResources.Columns[0].Width = (int)maxWidth;
		}

		private bool SelectFirstFileNode(Node node)
		{
			if (node.Tag is ArchAngel.Interfaces.Template.File)
			{
				treeFiles.SelectedNode = node;
				return true;
			}
			else if (node.Tag is ArchAngel.Interfaces.Template.Folder)
				for (int i = 0; i < node.Nodes.Count; i++)
				{
					if (SelectFirstFileNode(node.Nodes[i]))
						return true;
				}
			return false;
		}

		private void PopulateComboBoxTemplates()
		{
			comboBoxTemplates.Items.Clear();
			comboBoxTemplates.DisplayMember = "Name";

			string officialTemplateFolder;

#if DEBUG
			officialTemplateFolder = Slyce.Common.RelativePaths.GetFullPath(System.Reflection.Assembly.GetExecutingAssembly().Location, @"..\..\..\..\ArchAngel.Templates");
#else
			officialTemplateFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(typeof(Templates)).Location);
			officialTemplateFolder = Path.Combine(officialTemplateFolder, "Templates");
#endif
			ArchAngel.Interfaces.Template.TemplateProject defaultTemplate = null;

			// Add the official templates
			foreach (var proj in ArchAngel.Common.UserTemplateHelper.GetTemplates(officialTemplateFolder).OrderBy(t => t.Name))
			{
				//proj.Name = "[" + proj.Name + "]";
				proj.IsOfficial = true;
				comboBoxTemplates.Items.Add(proj);

				if (defaultTemplate == null)
					defaultTemplate = proj;
			}
			// Add the user templates
			foreach (var proj in ArchAngel.Common.UserTemplateHelper.GetTemplates().OrderBy(t => t.Name))
				comboBoxTemplates.Items.Add(proj);

			if (ArchAngel.Interfaces.SharedData.CurrentProject == null ||
				ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject == null)
			{
				comboBoxTemplates.SelectedItem = defaultTemplate;
			}
			else
			{
				SetComboTemplate(ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject);
			}
			comboBoxTemplates.Items.Add("<New template...>");
			//comboBoxTemplates.Items.Add("<Import...>");
			PopulateTree();
		}

		private void treeList1_NodeClick(object sender, DevComponents.AdvTree.TreeNodeMouseEventArgs e)
		{
			//Point pt = this.PointToScreen(e.Location);
			//contextMenuStrip1.Show();// (pt);
		}

		private void mnuNewFolder_Click(object sender, EventArgs e)
		{
			AddNewFolder();
		}


		public void AddNewFolder()
		{
			try
			{
				if (ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.IsOfficial)
				{
					if (MessageBox.Show(this, "Changes can't be made to built-in templates. Do you want to save-as a custom template?", "Built-in template", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
					{
						SaveAs();
					}
					return;
				}
				if (treeFiles.SelectedNodes.Count == 0)
				{
					MessageBox.Show(this, "Select a folder to add this file to first.", "No Folder Selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				else if (treeFiles.SelectedNodes.Count > 1)
				{
					throw new Exception("Only one node should be selected.");
				}
				Cursor = Cursors.WaitCursor;
				Refresh();
				Node selectedNode = treeFiles.SelectedNodes[0];
				ArchAngel.Interfaces.Template.Folder parentFolder = (ArchAngel.Interfaces.Template.Folder)selectedNode.Tag;
				ArchAngel.Interfaces.Template.Folder newFolder = new ArchAngel.Interfaces.Template.Folder()
				{
					ID = ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.GetNextAvailableFolderId(),
					Name = "NewFolder",
					Iterator = ArchAngel.Interfaces.Template.IteratorTypes.None,
					ParentFolder = parentFolder
				};
				parentFolder.Folders.Add(newFolder);
				CreateNewFolderAndAddToTree(selectedNode, newFolder);
				syntaxEditorFilename.Focus();
			}
			finally
			{
				Controller.Instance.MainForm.Activate();
				Cursor = Cursors.Default;
			}
		}

		private void CreateNewFolderAndAddToTree(Node selectedNode, ArchAngel.Interfaces.Template.Folder newFolder)
		{
			// TODO: mark project as IsDirty
			//ArchAngel.Interfaces.SharedData.CurrentProject..con.i Project.Instance.IsDirty = true;
			Node newFolderNode = AddFolderNode(selectedNode, newFolder);
			selectedNode.Expanded = true;
			treeFiles.SelectedNode = newFolderNode;
		}

		internal static string GetNodeDisplayText(string text, string placeholder)
		{
			if (placeholder == "[None]")
				placeholder = "[Project]";

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

		private Node AddFolderNode(Node parentNode, ArchAngel.Interfaces.Template.Folder subFolder)
		{
			string iteratorName = subFolder.Iterator.ToString();
			Node newNode = new Node();
			newNode.Text = GetNodeDisplayText(subFolder.Name, string.Format("[{0}]", subFolder.Iterator.ToString()));
			newNode.DragDropEnabled = true;
			//newNode.Cells.Add(new Cell(""));
			//newNode.Cells.Add(new Cell(iteratorName));
			newNode.ImageIndex = FoldersWithErrors.Contains(subFolder) ? IMG_FOLDER_ORANGE : IMG_CLOSED_FOLDER;
			newNode.ImageExpandedIndex = IMG_OPEN_FOLDER;
			newNode.Tag = subFolder;
			newNode.DataKey = "Folder_" + subFolder.ID;

			if (parentNode == null)
				treeFiles.Nodes.Add(newNode);
			else
				parentNode.Nodes.Add(newNode);

			foreach (var childFolder in subFolder.Folders)
				AddFolderNode(newNode, childFolder);

			foreach (var file in subFolder.Files)
				AddFileNode(newNode, file);

			foreach (var file in subFolder.StaticFiles)
				AddStaticFileNode(newNode, file);

			SortChildNodes(parentNode);
			SortChildNodes(newNode);

			// Don't expand if folder only contains static files
			newNode.Expanded = subFolder.Folders.Count + subFolder.Files.Count > 0;

			return newNode;
		}

		public void AddNewFile()
		{
			if (ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.IsOfficial)
			{
				if (MessageBox.Show(this, "Changes can't be made to built-in templates. Do you want to save-as a custom template?", "Built-in template", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
				{
					SaveAs();
				}
				return;
			}
			try
			{
				if (treeFiles.SelectedNodes.Count == 0)
				{
					MessageBox.Show(this, "Select a folder to add this file to first.", "No Folder Selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				else if (treeFiles.SelectedNodes.Count > 1)
				{
					throw new Exception("Only one node should be selected.");
				}
				Cursor = Cursors.WaitCursor;
				Refresh();
				Node selectedNode = treeFiles.SelectedNodes[0];
				ArchAngel.Interfaces.Template.Folder parentFolder = (ArchAngel.Interfaces.Template.Folder)selectedNode.Tag;
				ArchAngel.Interfaces.Template.File newFile = new ArchAngel.Interfaces.Template.File()
				{
					Id = ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.GetNextAvailableFileId(),
					Name = "NewFile",
					Iterator = ArchAngel.Interfaces.Template.IteratorTypes.None,
					ParentFolder = parentFolder
				};
				parentFolder.Files.Add(newFile);
				CreateNewFileAndAddToTree(selectedNode, newFile);
				syntaxEditorFilename.Focus();
			}
			finally
			{
				Controller.Instance.MainForm.Activate();
				Cursor = Cursors.Default;
			}
		}

		public void AddNewStaticFile()
		{
			if (ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.IsOfficial)
			{
				if (MessageBox.Show(this, "Changes can't be made to built-in templates. Do you want to save-as a custom template?", "Built-in template", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
				{
					SaveAs();
				}
				return;
			}
			try
			{
				if (treeFiles.SelectedNodes.Count == 0)
				{
					MessageBox.Show(this, "Select a folder to add this file to first.", "No Folder Selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				else if (treeFiles.SelectedNodes.Count > 1)
				{
					throw new Exception("Only one node should be selected.");
				}
				Cursor = Cursors.WaitCursor;
				Refresh();
				Node selectedNode = treeFiles.SelectedNodes[0];
				ArchAngel.Interfaces.Template.Folder parentFolder = (ArchAngel.Interfaces.Template.Folder)selectedNode.Tag;
				ArchAngel.Interfaces.Template.StaticFile newFile = new ArchAngel.Interfaces.Template.StaticFile()
				{
					Id = ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.GetNextAvailableStaticFileId(),
					Name = "NewFile",
					Iterator = ArchAngel.Interfaces.Template.IteratorTypes.None,
					ParentFolder = parentFolder
				};
				parentFolder.StaticFiles.Add(newFile);
				CreateNewStaticFileAndAddToTree(selectedNode, newFile);
				syntaxEditorFilename.Focus();
			}
			finally
			{
				Controller.Instance.MainForm.Activate();
				Cursor = Cursors.Default;
			}
		}

		private void CreateNewFileAndAddToTree(Node selectedNode, ArchAngel.Interfaces.Template.File newFile)
		{
			// TODO: mark project as IsDirty
			//ArchAngel.Interfaces.SharedData.CurrentProject..con.i Project.Instance.IsDirty = true;
			Node newFileNode = AddFileNode(selectedNode, newFile);
			selectedNode.Expanded = true;
			treeFiles.SelectedNode = newFileNode;
		}

		private void CreateNewStaticFileAndAddToTree(Node selectedNode, ArchAngel.Interfaces.Template.StaticFile newFile)
		{
			// TODO: mark project as IsDirty
			//ArchAngel.Interfaces.SharedData.CurrentProject..con.i Project.Instance.IsDirty = true;
			Node newFileNode = AddStaticFileNode(selectedNode, newFile);
			selectedNode.Expanded = true;
			treeFiles.SelectedNode = newFileNode;
		}

		private Node AddFileNode(Node parentNode, ArchAngel.Interfaces.Template.File file)
		{
			string iteratorName = file.Iterator.ToString();
			Node newNode = new Node();
			newNode.Text = GetNodeDisplayText(file.Name, string.Format("[{0}]", file.Iterator.ToString()));
			newNode.DragDropEnabled = true;
			//newNode.Cells.Add(new Cell(""));
			//newNode.Cells.Add(new Cell(iteratorName));
			newNode.ImageIndex = FilesWithErrors.Contains(file) ? IMG_FILE_ORANGE : IMG_FILE_GREEN;
			newNode.Tag = file;
			newNode.DataKey = "File_" + file.Id;

			if (parentNode == null)
				treeFiles.Nodes.Add(newNode);
			else
				parentNode.Nodes.Add(newNode);

			SortChildNodes(parentNode);
			return newNode;
		}

		private Node AddStaticFileNode(Node parentNode, ArchAngel.Interfaces.Template.StaticFile file)
		{
			string iteratorName = file.Iterator.ToString();
			Node newNode = new Node();

			string text = GetStaticFileNodeText(file);
			newNode.Text = GetNodeDisplayText(text, string.Format("[{0}]", file.Iterator.ToString()));
			newNode.DragDropEnabled = true;
			//newNode.Cells.Add(new Cell(""));
			//newNode.Cells.Add(new Cell(iteratorName));
			newNode.ImageIndex = IMG_STATIC_FILE;
			newNode.Tag = file;
			newNode.DataKey = "StaticFile_" + file.Id;

			if (parentNode == null)
				treeFiles.Nodes.Add(newNode);
			else
				parentNode.Nodes.Add(newNode);

			SortChildNodes(parentNode);
			return newNode;
		}

		private string GetStaticFileNodeText(ArchAngel.Interfaces.Template.StaticFile file)
		{
			string text = file.Name;
			string actualFilename = Path.GetFileName(file.ResourceName);

			if (!string.IsNullOrEmpty(actualFilename) && !file.Name.Equals(actualFilename, StringComparison.InvariantCultureIgnoreCase))
			{
				if (actualFilename.Length > file.Name.Length)
					text = string.Format("{0}  <i>({1})</i>", file.Name, actualFilename.ToLowerInvariant().Replace(file.Name.ToLowerInvariant(), ""));
			}
			return text;
		}

		private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
		{
			if (treeFiles.SelectedNode == null)
			{
				e.Cancel = true;
				return;
			}
			foreach (var item in contextMenuStrip1.Items)
				((ToolStripItem)item).Visible = false;

			if (treeFiles.SelectedNode.Tag is ArchAngel.Interfaces.Template.Folder)
			{
				mnuNewFile.Visible = true;
				mnuNewFolder.Visible = true;
				mnuNewStaticFile.Visible = true;
				mnuAddManyStaticFiles.Visible = true;

				if (treeFiles.SelectedNode != treeFiles.Nodes[0])
					mnuDelete.Visible = true;
			}
			else if (treeFiles.SelectedNode.Tag is ArchAngel.Interfaces.Template.File)
			{
				//e.Cancel = true;
				mnuDelete.Visible = true;
			}
			else if (treeFiles.SelectedNode.Tag is ArchAngel.Interfaces.Template.StaticFile)
			{
				//e.Cancel = true;
				mnuDelete.Visible = true;
			}
		}

		public frmFind FormFind { get; set; }

		public void EnableFindNext()
		{

		}

		private void mnuNewFile_Click(object sender, EventArgs e)
		{
			AddNewFile();
		}

		internal void SelectFile(ArchAngel.Interfaces.Template.File file)
		{
			if (treeFiles.SelectedNode.Tag == file)
				return;

			var node = treeFiles.FindNodeByDataKey(String.Format("File_{0}", file.Id));
			treeFiles.SelectedNode = node;
		}

		private void treeFiles_AfterNodeSelect(object sender, AdvTreeNodeEventArgs e)
		{
			_CurrentFile = null;
			syntaxEditorTest.Visible = false;
			labelResults.Visible = false;
			superTabControl1.SelectedTab = superTabItemScript;

			if (treeFiles.SelectedIndex <= 0)
			{
				comboBoxIterator.Visible = false;
				syntaxEditorFilename.Visible = false;
				comboBoxSyntax.Visible = false;
				labelFilename.Visible = false;
				syntaxEditor1.Visible = false;
				labelHeader.Text = "";
				labelHeader.Image = imageList1.Images[IMG_CLOSED_FOLDER];
				labelSkipStaticFile.Visible = false;
				syntaxEditorSkipStaticFile.Visible = false;
			}
			else if (e.Node.Tag is ArchAngel.Interfaces.Template.Folder)
			{
				PopulateIterators(false);
				ArchAngel.Interfaces.Template.Folder folder = (ArchAngel.Interfaces.Template.Folder)e.Node.Tag;

				if (folder.Iterator == ArchAngel.Interfaces.Template.IteratorTypes.None)
				{
					comboBoxIterator.Text = "one folder";
					crumbBarTestObjects.Items.Clear();
					crumbBarTestObjects.Enabled = false;
				}
				else
				{
					comboBoxIterator.Text = "one folder per " + folder.Iterator.ToString();
					PopulateTestCrumbBar(folder.Iterator);
				}

				syntaxEditorFilename.Text = folder.Name;
				labelFilename.Visible = true;
				labelFilename.Text = "Name:";
				comboBoxIterator.Visible = true;
				syntaxEditorFilename.Visible = true;
				comboBoxSyntax.Visible = false;
				syntaxEditor1.Visible = false;
				comboBoxStaticFiles.Visible = false;
				labelHeader.Text = "  Create";
				superTabItemScript.Text = " Folder script ";
				labelHeader.Image = imageList1.Images[IMG_CLOSED_FOLDER];
				labelSkipStaticFile.Visible = false;
				syntaxEditorSkipStaticFile.Visible = false;
				panel3.Height = syntaxEditorFilename.Bottom + 10;
			}
			else if (e.Node.Tag is ArchAngel.Interfaces.Template.File)
			{
				DisplayScriptFile((ArchAngel.Interfaces.Template.File)e.Node.Tag);
			}
			else if (e.Node.Tag is ArchAngel.Interfaces.Template.StaticFile)
			{
				PopulateIterators(true);
				ArchAngel.Interfaces.Template.StaticFile staticFile = (ArchAngel.Interfaces.Template.StaticFile)e.Node.Tag;

				if (staticFile.Iterator == ArchAngel.Interfaces.Template.IteratorTypes.None)
				{
					comboBoxIterator.Text = "one file";
					crumbBarTestObjects.Items.Clear();
					crumbBarTestObjects.Enabled = false;
				}
				else
				{
					comboBoxIterator.Text = "one file per " + staticFile.Iterator.ToString();
					crumbBarTestObjects.Items.Clear();
					crumbBarTestObjects.Enabled = true;
				}
				labelFilename.Visible = true;
				labelFilename.Text = "Name:";
				comboBoxIterator.Visible = true;
				syntaxEditorFilename.Visible = true;
				comboBoxSyntax.Visible = false;
				syntaxEditor1.Visible = false;
				labelHeader.Text = "  Create";
				superTabItemScript.Text = " Static file ";
				labelHeader.Image = imageList1.Images[IMG_CLOSED_FOLDER];
				string filename = staticFile.Name;
				syntaxEditorFilename.Text = staticFile.Name;
				comboBoxStaticFiles.Items.Clear();

				foreach (string sf in ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.ResourceFiles)
					comboBoxStaticFiles.Items.Add(sf);

				if (!string.IsNullOrEmpty(staticFile.ResourceName))
				{
					comboBoxStaticFiles.SelectedItem = staticFile.ResourceName;
					syntaxEditorFilename.Text = filename;
				}
				//else if (comboBoxStaticFiles.Items.Count > 0)
				//{
				//    comboBoxStaticFiles.SelectedIndex = 0;
				//    syntaxEditorFilename.Text = comboBoxStaticFiles.Text;
				//}
				comboBoxStaticFiles.Visible = true;
				labelSkipStaticFile.Visible = true;
				syntaxEditorSkipStaticFile.Visible = true;

				syntaxEditorSkipStaticFile.Text = staticFile.SkipThisFileScript;
				labelSkipStaticFile.Left = labelFilename.Left;
				labelSkipStaticFile.Top = labelFilename.Bottom + 20;
				syntaxEditorSkipStaticFile.Left = syntaxEditorFilename.Left;
				syntaxEditorSkipStaticFile.Top = labelSkipStaticFile.Bottom + 5;
				syntaxEditorSkipStaticFile.Width = syntaxEditorFilename.Width;
				syntaxEditorSkipStaticFile.Height = 200;
				panel3.Height = syntaxEditorSkipStaticFile.Bottom + 10;
				labelSkipStaticFile.BringToFront();
			}
		}

		internal void DisplayScriptFile(ArchAngel.Interfaces.Template.File file)
		{
			PopulateIterators(true);
			_CurrentFile = file;

			if (file.Iterator == ArchAngel.Interfaces.Template.IteratorTypes.None)
			{
				comboBoxIterator.Text = "one file";
				crumbBarTestObjects.Items.Clear();
				crumbBarTestObjects.Enabled = false;
			}
			else
			{
				comboBoxIterator.Text = "one file per " + file.Iterator.ToString();
				PopulateTestCrumbBar(file.Iterator);
			}
			syntaxEditorFilename.Text = file.Name;
			comboBoxSyntax.Text = Slyce.Common.SyntaxEditorHelper.LanguageNameFromEnum(file.Script.Syntax);

			switch (file.Encoding.EncodingName)
			{
				case "US-ASCII":
					comboBoxEncoding.Text = "ASCII";
					break;
				case "Unicode":
					comboBoxEncoding.Text = "Unicode";
					break;
				case "Unicode (UTF-8)":
					comboBoxEncoding.Text = "UTF8";
					break;
				default:
					throw new NotImplementedException("Encoding name not handled yet in AfterNodeSelect(): " + file.Encoding.EncodingName);
			}
			syntaxEditor1.Text = file.Script.Body;
			SetSyntax(file.Script.Syntax, syntaxEditor1, syntaxEditorTest);
			labelFilename.Visible = true;
			labelFilename.Text = "Name:";
			comboBoxIterator.Visible = true;
			syntaxEditorFilename.Visible = true;
			comboBoxSyntax.Visible = true;
			syntaxEditor1.Visible = true;
			comboBoxStaticFiles.Visible = false;
			labelHeader.Text = "  Create ";
			superTabItemScript.Text = " Script file ";
			labelHeader.Image = imageList1.Images[IMG_FILE];
			labelSkipStaticFile.Visible = false;
			syntaxEditorSkipStaticFile.Visible = false;
			panel3.Height = syntaxEditorFilename.Bottom + 10;
			timer1.Stop();
			timer1.Interval = 10;
			timer1.Start();
		}

		private static ArchAngel.Interfaces.Template.File _CurrentFile;

		public static ArchAngel.Interfaces.Template.File CurrentFile
		{
			get { return _CurrentFile; }
		}

		private void PopulateTestCrumbBar(ArchAngel.Interfaces.Template.IteratorTypes iteratorType)
		{
			try
			{
				BusyPopulatingCrumbBarTest = true;
				crumbBarTestObjects.Items.Clear();
				crumbBarTestObjects.Enabled = true;
				DevComponents.DotNetBar.CrumbBarItem rootItem = new DevComponents.DotNetBar.CrumbBarItem();
				rootItem.Text = "Project";
				rootItem.ImageIndex = (int)CrumbBarImages.Project;
				crumbBarTestObjects.Items.Add(rootItem);

				switch (iteratorType)
				{
					case ArchAngel.Interfaces.Template.IteratorTypes.Entity:

						foreach (var entity in ArchAngel.Interfaces.SharedData.CurrentProject.ScriptProject.Entities)
						{
							DevComponents.DotNetBar.CrumbBarItem item = new DevComponents.DotNetBar.CrumbBarItem();
							item.Text = entity.Name;
							item.Tag = entity;
							item.ImageIndex = (int)CrumbBarImages.Entity;
							rootItem.SubItems.Add(item);
						}
						if (rootItem.SubItems.Count > 0)
							crumbBarTestObjects.SelectedItem = (DevComponents.DotNetBar.CrumbBarItem)rootItem.SubItems[0];
						else
							crumbBarTestObjects.SelectedItem = rootItem;
						break;
					case ArchAngel.Interfaces.Template.IteratorTypes.Component:

						foreach (var component in ArchAngel.Interfaces.SharedData.CurrentProject.ScriptProject.Components)
						{
							DevComponents.DotNetBar.CrumbBarItem item = new DevComponents.DotNetBar.CrumbBarItem();
							item.Text = component.Name;
							item.Tag = component;
							item.ImageIndex = (int)CrumbBarImages.Component;
							rootItem.SubItems.Add(item);
						}
						if (rootItem.SubItems.Count > 0)
							crumbBarTestObjects.SelectedItem = (DevComponents.DotNetBar.CrumbBarItem)rootItem.SubItems[0];
						else
							crumbBarTestObjects.SelectedItem = rootItem;
						break;
					case ArchAngel.Interfaces.Template.IteratorTypes.Table:

						foreach (var table in ArchAngel.Interfaces.SharedData.CurrentProject.ScriptProject.Tables)
						{
							DevComponents.DotNetBar.CrumbBarItem item = new DevComponents.DotNetBar.CrumbBarItem();
							item.Text = table.Name;
							item.Tag = table;
							item.ImageIndex = (int)CrumbBarImages.Table;
							rootItem.SubItems.Add(item);
						}
						if (rootItem.SubItems.Count > 0)
							crumbBarTestObjects.SelectedItem = (DevComponents.DotNetBar.CrumbBarItem)rootItem.SubItems[0];
						else
							crumbBarTestObjects.SelectedItem = rootItem;
						break;
					case ArchAngel.Interfaces.Template.IteratorTypes.Column:

						foreach (var table in ArchAngel.Interfaces.SharedData.CurrentProject.ScriptProject.Tables)
						{
							DevComponents.DotNetBar.CrumbBarItem tableItem = new DevComponents.DotNetBar.CrumbBarItem();
							tableItem.Text = table.Name;
							tableItem.Tag = table;
							tableItem.ImageIndex = (int)CrumbBarImages.Table;
							rootItem.SubItems.Add(tableItem);

							foreach (var column in table.Columns)
							{
								DevComponents.DotNetBar.CrumbBarItem columnItem = new DevComponents.DotNetBar.CrumbBarItem();
								columnItem.Text = table.Name;
								columnItem.Tag = table;
								columnItem.ImageIndex = (int)CrumbBarImages.Table;
								tableItem.SubItems.Add(columnItem);
							}
							if (tableItem.SubItems.Count > 0)
								crumbBarTestObjects.SelectedItem = (DevComponents.DotNetBar.CrumbBarItem)tableItem.SubItems[0];
							else
								crumbBarTestObjects.SelectedItem = rootItem;
						}
						if (rootItem.SubItems.Count > 0)
							crumbBarTestObjects.SelectedItem = (DevComponents.DotNetBar.CrumbBarItem)rootItem.SubItems[0];
						else
							crumbBarTestObjects.SelectedItem = rootItem;
						break;
					default:
						throw new NotImplementedException("Iterator not handled yet: " + iteratorType.ToString());
				}
				if (LatestTestObjects.ContainsKey(iteratorType) &&
					LatestTestObjects[iteratorType] != null)
				{
					object currentObj = LatestTestObjects[iteratorType];
					bool itemFoundInCollection = false;

					foreach (DevComponents.DotNetBar.CrumbBarItem item in crumbBarTestObjects.Items)
					{
						if (item.Tag == currentObj)
						{
							crumbBarTestObjects.SelectedItem = item;
							itemFoundInCollection = true;
							break;
						}
						else
						{
							bool found = false;

							foreach (DevComponents.DotNetBar.CrumbBarItem subItem in item.SubItems)
							{
								if (subItem.Tag == currentObj)
								{
									crumbBarTestObjects.SelectedItem = item;
									crumbBarTestObjects.SelectedItem = subItem;
									found = true;
									itemFoundInCollection = true;
									break;
								}
							}
							if (found)
								break;
						}
					}
					if (!itemFoundInCollection)
						LatestTestObjects[iteratorType] = false;
				}
			}
			finally
			{
				BusyPopulatingCrumbBarTest = false;
			}
		}

		private void SetSyntax(TemplateContentLanguage textLanguage, params SyntaxEditor[] editors)
		{
			SetSyntax(textLanguage, false, editors);
		}

		private void SetSyntax(TemplateContentLanguage textLanguage, bool force, params SyntaxEditor[] editors)
		{
			foreach (var editor in editors)
			{
				if (CurrentLanguages[editor] != textLanguage || force)
				{

					if (ArchAngel.Interfaces.SharedData.CurrentProject == null)
						SyntaxEditorHelper.SetupEditorTemplateAndScriptLanguages(editor, textLanguage, SyntaxEditorHelper.ScriptLanguageTypes.CSharp, "<%", "%>");
					else
						SyntaxEditorHelper.SetupEditorTemplateAndScriptLanguages(editor, textLanguage, SyntaxEditorHelper.ScriptLanguageTypes.CSharp, ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.DelimiterStart, ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.DelimiterEnd);

					UseSplitLanguage = true;// !UseSplitLanguage;
					SwitchFormatting();
					CurrentLanguages[editor] = textLanguage;
				}
			}
		}

		private bool UseSplitLanguage = false;

		/// <summary>
		/// Swap faded and syntax-highlighted text.
		/// </summary>
		public void SwitchFormatting()
		{
			//UseSplitLanguage = !UseSplitLanguage;

			if (syntaxEditor1.Document.Language.LexicalStates.Count > 1)
			{
				syntaxEditor1.Document.Language.LexicalStates["ASPDirectiveState"].LexicalStateTransitionLexicalState.
					Language.BackColor = UseSplitLanguage
											 ? SyntaxEditorHelper.EDITOR_BACK_COLOR_FADED
											 : SyntaxEditorHelper.EDITOR_BACK_COLOR_NORMAL;
				syntaxEditor1.Document.Language.BackColor = UseSplitLanguage
																? SyntaxEditorHelper.EDITOR_BACK_COLOR_NORMAL
																: SyntaxEditorHelper.EDITOR_BACK_COLOR_FADED;
				syntaxEditor1.Refresh();
			}
		}

		private void SetSyntaxEditorBackColor()
		{
			Color backColor = Color.White;
			ActiproSoftware.SyntaxEditor.VisualStudio2005SyntaxEditorRenderer vs = (ActiproSoftware.SyntaxEditor.VisualStudio2005SyntaxEditorRenderer)syntaxEditor1.RendererResolved;
			ActiproSoftware.Drawing.SolidColorBackgroundFill fill = new ActiproSoftware.Drawing.SolidColorBackgroundFill(backColor);
			vs.TextAreaBackgroundFill = fill;
			syntaxEditor1.Document.Language.BackColor = backColor;
		}

		//private void ConfigureSyntaxEditor(ActiproSoftware.SyntaxEditor.SyntaxEditor syntaxEditor, bool multiLineMode)
		//{
		//    #region Syntax Editor settings
		//    syntaxEditor.Document.Multiline = multiLineMode;
		//    SyntaxEditorHelper.SetupEditorTemplateAndScriptLanguages(syntaxEditor, TemplateContentLanguage.CSharp, SyntaxEditorHelper.ScriptLanguageTypes.CSharp);
		//    ActiproSoftware.SyntaxEditor.KeyPressTrigger t = new ActiproSoftware.SyntaxEditor.KeyPressTrigger("MemberListTrigger2", true, '#');
		//    t.ValidLexicalStates.Add(syntaxEditor.Document.Language.DefaultLexicalState);
		//    syntaxEditor.Document.Language.Triggers.Add(t);
		//    SwitchFormatting(syntaxEditor);
		//    #endregion
		//}

		/// <summary>
		/// Swap faded and syntax-highlighted text.
		/// </summary>
		public void SwitchFormatting(ActiproSoftware.SyntaxEditor.SyntaxEditor syntaxEditor)
		{
			//UseSplitLanguage = !UseSplitLanguage;
			if (syntaxEditor.Document.Language.LexicalStates.Count > 1)
			{
				syntaxEditor.Document.Language.LexicalStates["ASPDirectiveState"].LexicalStateTransitionLexicalState.
					Language.BackColor = SyntaxEditorHelper.EDITOR_BACK_COLOR_FADED;
				syntaxEditor.Document.Language.BackColor = SyntaxEditorHelper.EDITOR_BACK_COLOR_NORMAL;
				syntaxEditor.Refresh();
			}
		}

		private void treeFiles_BeforeNodeSelect(object sender, AdvTreeNodeCancelEventArgs e)
		{
			SaveCurrent();
		}

		internal void SaveCurrent()
		{
			if (treeFiles.SelectedIndex > 0 &&
				treeFiles.SelectedNode != null)
			{
				if (treeFiles.SelectedNode.Tag is ArchAngel.Interfaces.Template.Folder)
				{
					ArchAngel.Interfaces.Template.Folder folder = (ArchAngel.Interfaces.Template.Folder)treeFiles.SelectedNode.Tag;
					folder.Name = syntaxEditorFilename.Document.Text;

					if (comboBoxIterator.Text == "one folder")
						folder.Iterator = ArchAngel.Interfaces.Template.IteratorTypes.None;
					else
						folder.Iterator = (ArchAngel.Interfaces.Template.IteratorTypes)Enum.Parse(typeof(ArchAngel.Interfaces.Template.IteratorTypes), comboBoxIterator.Text.Replace("one folder per ", ""), true);
				}
				else if (treeFiles.SelectedNode.Tag is ArchAngel.Interfaces.Template.File)
				{
					ArchAngel.Interfaces.Template.File file = (ArchAngel.Interfaces.Template.File)treeFiles.SelectedNode.Tag;
					file.Name = syntaxEditorFilename.Document.Text;
					file.Script.Syntax = Slyce.Common.SyntaxEditorHelper.LanguageEnumFromName(comboBoxSyntax.Text);
					file.Script.Body = syntaxEditor1.Document.Text;

					switch (comboBoxEncoding.Text)
					{
						case "ASCII":
							file.Encoding = Encoding.ASCII;
							break;
						case "Unicode":
							file.Encoding = Encoding.Unicode;
							break;
						case "UTF8":
							file.Encoding = Encoding.UTF8;
							break;
						default:
							throw new NotImplementedException("Encoding not handled yet: " + comboBoxEncoding.Text);
					}

					if (comboBoxIterator.Text == "one file")
						file.Iterator = ArchAngel.Interfaces.Template.IteratorTypes.None;
					else
						file.Iterator = (ArchAngel.Interfaces.Template.IteratorTypes)Enum.Parse(typeof(ArchAngel.Interfaces.Template.IteratorTypes), comboBoxIterator.Text.Replace("one file per ", ""), true);
				}
				else if (treeFiles.SelectedNode.Tag is ArchAngel.Interfaces.Template.StaticFile)
				{
					ArchAngel.Interfaces.Template.StaticFile file = (ArchAngel.Interfaces.Template.StaticFile)treeFiles.SelectedNode.Tag;
					file.Name = syntaxEditorFilename.Document.Text;
					file.ResourceName = comboBoxStaticFiles.Text;
					file.SkipThisFileScript = syntaxEditorSkipStaticFile.Document.Text;

					if (comboBoxIterator.Text == "one file")
						file.Iterator = ArchAngel.Interfaces.Template.IteratorTypes.None;
					else
						file.Iterator = (ArchAngel.Interfaces.Template.IteratorTypes)Enum.Parse(typeof(ArchAngel.Interfaces.Template.IteratorTypes), comboBoxIterator.Text.Replace("one file per ", ""), true);
				}
			}
			if (treeFiles.SelectedNode != null)
				SortChildNodes(treeFiles.SelectedNode.Parent);
		}

		private void SortChildNodes(Node node)
		{
			if (node != null)
				node.Nodes.Sort(new TreelistNodeComparer());
		}

		private void comboBoxTemplates_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ArchAngel.Interfaces.SharedData.CurrentProject != null &&
				ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject != null &&
				ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.IsDirty)
			{
				if (MessageBox.Show(this, string.Format("Save changes to '{0}'?", ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.Name), "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					Save();
			}
			if (comboBoxTemplates.SelectedItem is string && comboBoxTemplates.SelectedItem.ToString() == "<New template...>")
			{
				ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject = new ArchAngel.Interfaces.Template.TemplateProject();
				ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.Name = "NewTemplate";
			}
			else if (ArchAngel.Interfaces.SharedData.CurrentProject != null)
			{
				ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject = (ArchAngel.Interfaces.Template.TemplateProject)comboBoxTemplates.SelectedItem;

				if (ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.IsOfficial)
					ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.Name = ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.Name.Trim('[', ']');
			}
			if (ArchAngel.Interfaces.SharedData.CurrentProject != null)
				Populate();
		}

		private void syntaxEditor1_TextChanged(object sender, EventArgs e)
		{
			CheckSyntax();
		}

		private void CheckSyntax()
		{
			if (BusyPopulating)
				return;

			if (ArchAngel.Interfaces.SharedData.CurrentProject != null &&
				ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject != null)
			{
				ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.IsDirty = true;
			}

			if (!timer1.Enabled)
			{
				timer1.Enabled = true;
				timer1.Start();
			}
			else
			{
				timer1.Stop();
				timer1.Start();
			}
		}

		private void comboBoxIterator_SelectedIndexChanged(object sender, EventArgs e)
		{
			CheckSyntax();
		}

		private void comboBoxSyntax_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ArchAngel.Interfaces.SharedData.CurrentProject != null &&
				ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject != null)
			{
				ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.IsDirty = true;
				SetSyntax(Slyce.Common.SyntaxEditorHelper.LanguageEnumFromName(comboBoxSyntax.Text), syntaxEditor1, syntaxEditorTest);
			}
		}

		private void textBoxX1_TextChanged(object sender, EventArgs e)
		{
			if (BusyPopulatingTree) return;

			ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.IsDirty = true;
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			Save();
		}

		private void Save()
		{
			if (ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.IsOfficial)
			{
				if (MessageBox.Show(this, "Changes can't be made to built-in templates. Do you want to save-as a custom template?", "Built-in template", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
					SaveAs();

				return;
			}
			List<ArchAngel.Interfaces.Template.TemplateProject> matchingTemplates = ArchAngel.Common.UserTemplateHelper.GetTemplates().Where(t => t.Name == ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.Name && t.File != ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.File).ToList();

			if (matchingTemplates.Count > 0)
			{
				string list = "";

				foreach (var template in matchingTemplates)
					list += template.File + Environment.NewLine;

				MessageBox.Show(this, string.Format("Templates with the same name already exists:{0}{1}", Environment.NewLine, list), "Duplicate name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (string.IsNullOrEmpty(ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.File) ||
				!Directory.Exists(Path.GetDirectoryName(ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.File)))
			{
				SaveAs();
				return;
			}
			SaveCurrent();
			ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.Save(false);
		}

		private void buttonSaveAs_Click(object sender, EventArgs e)
		{
			SaveAs();
		}

		private void SaveAs()
		{
			//List<ArchAngel.Interfaces.Template.TemplateProject> matchingTemplates = ArchAngel.Common.UserTemplateHelper.GetTemplates().Where(t => t.Name == ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.Name && t.File != ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.File).ToList();

			//if (matchingTemplates.Count > 0)
			//{
			//    string list = "";

			//    foreach (var template in matchingTemplates)
			//        list += template.File + Environment.NewLine;

			//    MessageBox.Show(this, string.Format("Templates with the same name already exists:{0}{1}", Environment.NewLine, list), "Duplicate name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			//    return;
			//}
			SaveFileDialog dialog = new SaveFileDialog();
			dialog.DefaultExt = ".vnh_template";
			dialog.InitialDirectory = ArchAngel.Common.UserTemplateHelper.GetTemplatesFolder();
			dialog.Filter = "Visual NHibernate templates (*.vnh_template)|*.vnh_template";
			dialog.FileName = ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.Name.Trim('[', ']');

			if (dialog.ShowDialog(this) == DialogResult.OK)
			{
				if (File.Exists(dialog.FileName) && MessageBox.Show(this, "Overwrite existing template?", "Existing template", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
				{
					return;
				}
				SaveCurrent();
				ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.File = dialog.FileName;
				ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.Save(true);
			}
			ArchAngel.Interfaces.Template.TemplateProject temp = ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject;
			PopulateComboBoxTemplates();

			ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject = temp;
			SetComboTemplate(ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject);
		}

		private void SetComboTemplate(ArchAngel.Interfaces.Template.TemplateProject project)
		{
			for (int i = 0; i < comboBoxTemplates.Items.Count; i++)
			{
				if (comboBoxTemplates.Items[i] is string)
					continue;

				ArchAngel.Interfaces.Template.TemplateProject p = (ArchAngel.Interfaces.Template.TemplateProject)comboBoxTemplates.Items[i];

				if ((!p.IsOfficial && !project.IsOfficial) &&
					p.Name == project.Name)
				{
					comboBoxTemplates.SelectedIndex = i;
					break;
				}
				else if ((p.IsOfficial && project.IsOfficial) &&
					p.Name.Trim('[', ']') == project.Name.Trim('[', ']'))
				{
					comboBoxTemplates.SelectedIndex = i;
					break;
				}
			}
		}

		/// <summary>
		/// Adds a span indicator.
		/// </summary>
		/// <param name="layerKey">The key of the layer that will add the span indicator.</param>
		/// <param name="layerDisplayPriority">The display priority of the layer.</param>
		/// <param name="indicator">The <see cref="SpanIndicator"/> to add.</param>
		/// <param name="textRange">The text range over which to add the indicator.</param>
		private void AddSpanIndicator(
			ActiproSoftware.SyntaxEditor.SyntaxEditor editor,
			string layerKey,
			int layerDisplayPriority,
			ActiproSoftware.SyntaxEditor.SpanIndicator indicator,
			ActiproSoftware.SyntaxEditor.TextRange textRange)
		{
			// Ensure there is a selection
			if (textRange.AbsoluteLength == 0)
			{
				ActiproSoftware.SyntaxEditor.DefaultWordBreakFinder wbf = new ActiproSoftware.SyntaxEditor.DefaultWordBreakFinder();
				int startOffset = wbf.FindNextWordStart(editor.Document, textRange.StartOffset);
				int endOffset = wbf.FindCurrentWordEnd(editor.Document, startOffset);

				if (endOffset <= startOffset)
					return;

				textRange = new TextRange(startOffset, endOffset);
				//MessageBox.Show("Please make a selection first.");
				//return;
			}
			// Ensure that a syntax error layer is created...
			ActiproSoftware.SyntaxEditor.SpanIndicatorLayer layer = editor.Document.SpanIndicatorLayers[layerKey];
			if (layer == null)
			{
				layer = new ActiproSoftware.SyntaxEditor.SpanIndicatorLayer(layerKey, layerDisplayPriority);
				editor.Document.SpanIndicatorLayers.Add(layer);
			}
			// Don't allow the indicator to overlap another one
			if (layer.OverlapsWith(textRange))
			{
				//MessageBox.Show("Span indicators within the same layer may not overlap.");
				return;
			}
			// Add the indicator
			layer.Add(indicator, textRange);
		}

		private void dataGridViewErrors_RowEnter(object sender, DataGridViewCellEventArgs e)
		{
			if (BusyPopulatingErrors)
				return;

			string functionId = (string)dataGridViewErrors[ColFunctionId.Index, e.RowIndex].Value;
			string funcName = (string)dataGridViewErrors[ColFile.Index, e.RowIndex].Value;
			int line = (int)dataGridViewErrors[ColLine.Index, e.RowIndex].Value - 1;
			int column = (int)dataGridViewErrors[ColColumn.Index, e.RowIndex].Value - 1;
			string codeFunction = (string)dataGridViewErrors[ColCodeFunction.Index, e.RowIndex].Value;
			string errDescription = (string)dataGridViewErrors[ColDescription.Index, e.RowIndex].Value;

			if (line < 0)
			{
				MessageBox.Show("Error before beginning");
				return;
			}
			treeFiles.SelectedNode = treeFiles.FindNodeByDataKey(functionId);

			syntaxEditorFilename.Document.SpanIndicatorLayers.Clear();
			syntaxEditor1.Document.SpanIndicatorLayers.Clear();

			ActiproSoftware.SyntaxEditor.SyntaxEditor editor;
			if (codeFunction == "Filename")
			{
				editor = syntaxEditorFilename;
				highlighter1.SetHighlightColor(syntaxEditorFilename, DevComponents.DotNetBar.Validator.eHighlightColor.Red);
				//editor.Document.Lines[0].BackColor = Color.Salmon;
			}
			else if (codeFunction == "Body")
			{
				editor = syntaxEditor1;
				highlighter1.SetHighlightColor(syntaxEditorFilename, DevComponents.DotNetBar.Validator.eHighlightColor.None);
				//editor.Document.Lines[0].BackColor = Color.White;
			}
			else
				throw new NotImplementedException("Function type not handled yet: " + codeFunction);

			int startOffset = editor.Document.Lines[line].TextRange.StartOffset + column;
			// Account for \r\n instead of \n
			//startOffset -= line;
			//int endOffset = syntaxEditor1.Document.findc startOffset + 3;
			ActiproSoftware.SyntaxEditor.DefaultWordBreakFinder wbf = new ActiproSoftware.SyntaxEditor.DefaultWordBreakFinder();
			int endOffset = wbf.FindCurrentWordEnd(editor.Document, startOffset);

			if (endOffset < startOffset)
			{
				MessageBox.Show("endOffset < startOffset");
				return;
			}
			//editor.Document.SpanIndicatorLayers.Clear();

			//this.AddSpanIndicator(
			//    syntaxEditor1,
			//    ActiproSoftware.SyntaxEditor.SpanIndicatorLayer.SyntaxErrorKey, 
			//    ActiproSoftware.SyntaxEditor.SpanIndicatorLayer.SyntaxErrorDisplayPriority,
			//    new ActiproSoftware.SyntaxEditor.WaveLineSpanIndicator(Color.Red), new ActiproSoftware.SyntaxEditor.TextRange(startOffset, endOffset));

			ActiproSoftware.SyntaxEditor.HighlightingStyle highlightingStyle = new ActiproSoftware.SyntaxEditor.HighlightingStyle("Test", null, Color.Empty, Color.Red);
			ActiproSoftware.SyntaxEditor.HighlightingStyleSpanIndicator highlightSpan = new ActiproSoftware.SyntaxEditor.HighlightingStyleSpanIndicator("Test", highlightingStyle);
			highlightSpan.Tag = errDescription;

			this.AddSpanIndicator(
				editor,
				ActiproSoftware.SyntaxEditor.SpanIndicatorLayer.SyntaxErrorKey,
				ActiproSoftware.SyntaxEditor.SpanIndicatorLayer.SyntaxErrorDisplayPriority,
				highlightSpan, new ActiproSoftware.SyntaxEditor.TextRange(startOffset, endOffset));

			editor.SelectedView.GoToLine(line);
			editor.Caret.Offset = startOffset;
			editor.SelectedView.ScrollToCaret();
		}

		private void syntaxEditorFilename_TextChanged(object sender, EventArgs e)
		{
			string iteratorName;

			if (comboBoxIterator.SelectedItem.ToString().IndexOf(" per ") < 0)
			{
				iteratorName = "None";

				if (treeFiles.SelectedNode.Tag is ArchAngel.Interfaces.Template.StaticFile)
				{
					ArchAngel.Interfaces.Template.StaticFile staticFile = (ArchAngel.Interfaces.Template.StaticFile)treeFiles.SelectedNode.Tag;
					staticFile.Name = syntaxEditorFilename.Text;
					treeFiles.SelectedNode.Text = GetNodeDisplayText(GetStaticFileNodeText(staticFile), string.Format("[{0}]", iteratorName));
				}
				else
					treeFiles.SelectedNode.Text = GetNodeDisplayText(syntaxEditorFilename.Text, string.Format("[{0}]", iteratorName));
			}
			else
			{
				iteratorName = comboBoxIterator.SelectedItem.ToString();
				iteratorName = iteratorName.Substring(iteratorName.IndexOf(" per ") + 5);

				if (treeFiles.SelectedNode.Tag is ArchAngel.Interfaces.Template.StaticFile)
				{
					ArchAngel.Interfaces.Template.StaticFile staticFile = (ArchAngel.Interfaces.Template.StaticFile)treeFiles.SelectedNode.Tag;
					staticFile.Name = syntaxEditorFilename.Text;
					treeFiles.SelectedNode.Text = GetNodeDisplayText(GetStaticFileNodeText(staticFile), string.Format("[{0}]", iteratorName));
				}
				else
					treeFiles.SelectedNode.Text = GetNodeDisplayText(syntaxEditorFilename.Text, string.Format("[{0}]", iteratorName));
			}
			ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.IsDirty = true;

			if (!timer1.Enabled)
			{
				timer1.Enabled = true;
				timer1.Start();
			}
			else
			{
				timer1.Stop();
				timer1.Start();
			}
		}

		private void CompileBaseAssemblyOnly()
		{
			string exeDir = Path.GetDirectoryName(Application.ExecutablePath);
			List<string> referencedAssemblies = new List<string>();
			referencedAssemblies.Add(Path.Combine(exeDir, "ArchAngel.Interfaces.dll"));
			referencedAssemblies.Add(Path.Combine(exeDir, "ArchAngel.Providers.EntityModel.dll"));
			referencedAssemblies.Add(Path.Combine(exeDir, "ArchAngel.NHibernateHelper.dll"));

			ArchAngel.Common.Generator gen = new ArchAngel.Common.Generator();

			if (!Directory.Exists(TempAssembliesDir))
				Directory.CreateDirectory(TempAssembliesDir);

			BaseAssemblyPath = Path.Combine(TempAssembliesDir, Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".dll");
			BaseAssembly = gen.CompileBaseAssembly(referencedAssemblies, null, BaseAssemblyPath);
		}

		private void QuickCompile()
		{
			if (treeFiles.SelectedNode == null)
				return;

			string dummyFilename;
			string dummyBody;
			bool dummySkipFile;
			QuickCompile(false, -1, null, out dummyFilename, out dummyBody, out dummySkipFile);
		}

		private void QuickCompile(
			bool execute,
			int fileId,
			object param,
			out string genFilename,
			out string genBody,
			out bool genSkipFile)
		{
			genFilename = "";
			genBody = "";
			genSkipFile = false;

			System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();
			ArchAngel.Interfaces.Template.TemplateProject proj = new ArchAngel.Interfaces.Template.TemplateProject();

			if (treeFiles.SelectedNode.Tag is ArchAngel.Interfaces.Template.File)
			{
				ArchAngel.Interfaces.Template.File file = (ArchAngel.Interfaces.Template.File)treeFiles.SelectedNode.Tag;
				file.Name = syntaxEditorFilename.Text;
				file.Script.Syntax = Slyce.Common.SyntaxEditorHelper.LanguageEnumFromName(comboBoxSyntax.Text);
				file.Script.Body = syntaxEditor1.Text;

				if (comboBoxIterator.Text.ToLowerInvariant().Trim() == "one file")
					file.Iterator = ArchAngel.Interfaces.Template.IteratorTypes.None;
				else
				{
					string iterator = comboBoxIterator.Text.ToLowerInvariant().Trim();
					iterator = iterator.Substring(iterator.LastIndexOf(' ') + 1);
					file.Iterator = (ArchAngel.Interfaces.Template.IteratorTypes)Enum.Parse(typeof(ArchAngel.Interfaces.Template.IteratorTypes), iterator, true);
				}
				proj.OutputFolder.Files.Add(file);
			}
			else if (treeFiles.SelectedNode.Tag is ArchAngel.Interfaces.Template.Folder)
			{
				ArchAngel.Interfaces.Template.Folder folder = (ArchAngel.Interfaces.Template.Folder)treeFiles.SelectedNode.Tag;
				folder.Name = syntaxEditorFilename.Text;

				if (comboBoxIterator.Text.ToLowerInvariant().Trim() == "one folder")
					folder.Iterator = ArchAngel.Interfaces.Template.IteratorTypes.None;
				else
				{
					string iterator = comboBoxIterator.Text.ToLowerInvariant().Trim();
					iterator = iterator.Substring(iterator.LastIndexOf(' ') + 1);
					folder.Iterator = (ArchAngel.Interfaces.Template.IteratorTypes)Enum.Parse(typeof(ArchAngel.Interfaces.Template.IteratorTypes), iterator, true);
				}
				proj.OutputFolder.Folders.Add(folder);
			}
			string exeDir = Path.GetDirectoryName(Application.ExecutablePath);
			List<string> referencedAssemblies = new List<string>();
			//referencedAssemblies.Add(Path.Combine(exeDir, "ArchAngel.Scripting.dll"));
			referencedAssemblies.Add(Path.Combine(exeDir, "ArchAngel.Interfaces.dll"));
			referencedAssemblies.Add(Path.Combine(exeDir, "ArchAngel.Providers.EntityModel.dll"));
			referencedAssemblies.Add(Path.Combine(exeDir, "ArchAngel.NHibernateHelper.dll"));
#if DEBUG
			referencedAssemblies.Add(Slyce.Common.RelativePaths.GetFullPath(System.Reflection.Assembly.GetExecutingAssembly().Location, @"..\..\..\..\3rd_Party_Libs\Inflector.Net.dll"));
#else
			referencedAssemblies.Add(Path.Combine(exeDir, "Inflector.Net.dll"));
#endif
			//referencedAssemblies.Add(BaseAssemblyPath);

			List<System.CodeDom.Compiler.CompilerError> compileErrors;

			ArchAngel.Common.Generator gen = new ArchAngel.Common.Generator(
				null,
				Controller.Instance.CurrentProject.TemplateLoader);

			//gen.CompileFunctionsAssembly(proj, referencedAssemblies, out compileErrors);
			string temp = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Branding.ProductName + Path.DirectorySeparatorChar + "Temp"), "Compile");

			if (!Directory.Exists(temp))
				Directory.CreateDirectory(temp);

			temp = Path.Combine(temp, Path.GetFileNameWithoutExtension(Path.GetTempFileName()) + ".dll");
			gen.CompileCombinedAssembly(proj, referencedAssemblies, null, out compileErrors, execute, temp);

			if (treeFiles.SelectedNode.Tag is ArchAngel.Interfaces.Template.File)
				treeFiles.SelectedNode.ImageIndex = compileErrors.Count == 0 ? IMG_FILE_GREEN : IMG_FILE_ORANGE;
			else if (treeFiles.SelectedNode.Tag is ArchAngel.Interfaces.Template.Folder)
				treeFiles.SelectedNode.ImageIndex = compileErrors.Count == 0 ? IMG_CLOSED_FOLDER : IMG_FOLDER_ORANGE;

			if (execute)
			{
				if (compileErrors.Count == 0)
				{
					if (treeFiles.SelectedNode.Tag is ArchAngel.Interfaces.Template.File)
					{
						ArchAngel.Interfaces.Template.File file = (ArchAngel.Interfaces.Template.File)treeFiles.SelectedNode.Tag;
						gen.SetProjectInCode(ArchAngel.Interfaces.SharedData.CurrentProject.ScriptProject);
						genFilename = gen.GetFileName(fileId, param);

						try
						{
							genBody = Utility.StandardizeLineBreaks(gen.GetFileBody(file.Id, param, out genSkipFile), Utility.LineBreaks.Windows);
						}
						catch (Exception e)
						{
							int errLine;
							string stackTrace = ArchAngel.Common.Generator.GetCleanStackTrace(e.InnerException.StackTrace, file.Id, out errLine);
							stackTrace = stackTrace.Substring(stackTrace.IndexOf(":line ") + ":line ".Length);
							//throw new Exception("TODO: Work out how to handle filename: file vs. static file");
							ArchAngel.Common.Generator.DebugPos debugPos = ArchAngel.Common.Generator.GetDebugPos(string.Format("File_{0}", file.Id), errLine - 1, 1, string.Format("Error: {0}\nFile: {1}", e.Message, file.Id));
							debugPos.Line = errLine; // GFH
							HighlightRuntimeException(file.Id, debugPos, e.InnerException.Message);
							return;
						}
					}
					else if (treeFiles.SelectedNode.Tag is ArchAngel.Interfaces.Template.Folder)
					{
						ArchAngel.Interfaces.Template.Folder folder = (ArchAngel.Interfaces.Template.Folder)treeFiles.SelectedNode.Tag;

						try
						{
							genFilename = gen.GetFolderName(folder.ID, param);
						}
						catch (Exception e)
						{
							MessageBox.Show(this, e.InnerException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
						genBody = "";
					}
					else
						throw new NotImplementedException("Template type not handled yet: " + treeFiles.SelectedNode.Tag.GetType().ToString());

					if (superTabControl1.SelectedTab != superTabItemTest)
						superTabControl1.SelectedTab = superTabItemTest;
				}
				else
				{
					if (superTabControl1.SelectedTab != superTabItemScript)
						superTabControl1.SelectedTab = superTabItemScript;

					MessageBox.Show(this, "See bottom of screen for errors.", "Errors occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			syntaxEditorFilename.Document.SpanIndicatorLayers.Clear();
			syntaxEditor1.Document.SpanIndicatorLayers.Clear();
			BusyPopulatingErrors = true;
			dataGridViewErrors.Rows.Clear();
			highlighter1.SetHighlightColor(syntaxEditorFilename, DevComponents.DotNetBar.Validator.eHighlightColor.None);

			if (compileErrors.Count == 0)
			{
				labelHeader.BackgroundStyle.BackColor = Color.GreenYellow;
				labelHeader.BackgroundStyle.BackColor2 = Color.Green;
				labelHeader.Refresh();
			}
			else
			{
				labelHeader.BackgroundStyle.BackColor = Color.FromArgb(249, 224, 123);
				labelHeader.BackgroundStyle.BackColor2 = Color.FromArgb(224, 113, 21);
				labelHeader.Refresh();

				//Controller.Instance.RaiseCompileErrors(compileErrors);
				//progressHelper.ReportProgress(100, new GenerateFilesProgress(0, new Controller.CompileErrorsDelegate(compileErrors)));

				foreach (var err in compileErrors)
				{
					//throw new Exception("TODO: Work out how to handle filename: file vs. static file");
					ArchAngel.Common.Generator.DebugPos debugPos = ArchAngel.Common.Generator.GetDebugPos(err.FileName, err.Line, err.Column, string.Format("Error: {0}\nFile: {1}", err.ErrorText, err.FileName));
					string functionName = debugPos.FunctionName == "GetFileName" ? "Filename" : "Body";
					int line = debugPos.Line - 1;
					int column = err.Column - 10 - 1;
					string filename = treeFiles.FindNodeByDataKey(err.FileName).Text;
					string description = err.ErrorText
						.Replace("Slyce.FunctionRunner.", "");

					ActiproSoftware.SyntaxEditor.SyntaxEditor editor;

					if (functionName == "Filename")
						editor = syntaxEditorFilename;
					else if (functionName == "Body")
						editor = syntaxEditor1;
					else
						throw new NotImplementedException("Function type not handled yet: " + functionName);

					int startOffset = editor.Document.Lines[line].TextRange.StartOffset + column;

					//if (startOffset < 0 && functionName == "Body")
					//{
					//    editor = syntaxEditorFilename;
					//    startOffset = editor.Document.Lines[line].TextRange.StartOffset + column;
					//}

					dataGridViewErrors.Rows.Add(err.FileName, "", description, filename, functionName, debugPos.Line, err.Column - 10);

					// Account for \r\n instead of \n
					//startOffset -= line;
					//int endOffset = syntaxEditor1.Document.findc startOffset + 3;
					ActiproSoftware.SyntaxEditor.DefaultWordBreakFinder wbf = new ActiproSoftware.SyntaxEditor.DefaultWordBreakFinder();

					if (startOffset < 0)
						return;

					int endOffset = wbf.FindCurrentWordEnd(editor.Document, startOffset);

					if (endOffset < startOffset)
					{
						//MessageBox.Show("endOffset < startOffset");
						return;
					}
					ActiproSoftware.SyntaxEditor.WaveLineSpanIndicator errorIndicator = new ActiproSoftware.SyntaxEditor.WaveLineSpanIndicator(Color.Red);
					errorIndicator.Tag = err.ErrorText;

					this.AddSpanIndicator(
						editor,
						ActiproSoftware.SyntaxEditor.SpanIndicatorLayer.SyntaxErrorKey,
						ActiproSoftware.SyntaxEditor.SpanIndicatorLayer.SyntaxErrorDisplayPriority,
						errorIndicator, new ActiproSoftware.SyntaxEditor.TextRange(startOffset, endOffset));
				}
				bar1.SelectedDockContainerItem = dockContainerItemErrors;
				BusyPopulatingErrors = false;
			}
			stopwatch.Stop();
			long duration = stopwatch.ElapsedMilliseconds;
#if DEBUG
			timer1.Interval = 600;
#else
			timer1.Interval = Math.Max((int)duration * 3, 600);
#endif
		}

		private void HighlightRuntimeException(int functionId, ArchAngel.Common.Generator.DebugPos debugPos, string errDescription)
		{
			//string funcName = (string)dataGridViewErrors[ColFile.Index, e.RowIndex].Value;
			int line = debugPos.Line;

			treeFiles.SelectedNode = treeFiles.FindNodeByDataKey("File_" + functionId);

			syntaxEditorFilename.Document.SpanIndicatorLayers.Clear();
			syntaxEditor1.Document.SpanIndicatorLayers.Clear();

			ActiproSoftware.SyntaxEditor.SyntaxEditor editor;
			//if (codeFunction == "Filename")
			//{
			//editor = syntaxEditorFilename;
			//highlighter1.SetHighlightColor(syntaxEditorFilename, DevComponents.DotNetBar.Validator.eHighlightColor.Red);
			////editor.Document.Lines[0].BackColor = Color.Salmon;
			//}
			//else if (codeFunction == "Body")
			//{
			editor = syntaxEditor1;
			highlighter1.SetHighlightColor(syntaxEditorFilename, DevComponents.DotNetBar.Validator.eHighlightColor.None);
			//    //editor.Document.Lines[0].BackColor = Color.White;
			//}
			//else
			//    throw new NotImplementedException("Function type not handled yet: " + codeFunction);

			int startOffset = editor.Document.Lines[line].TextRange.StartOffset;// +column;
			ActiproSoftware.SyntaxEditor.DefaultWordBreakFinder wbf = new ActiproSoftware.SyntaxEditor.DefaultWordBreakFinder();
			int endOffset = editor.Document.Lines[line].TextRange.EndOffset;// wbf.FindCurrentWordEnd(editor.Document, startOffset);

			if (endOffset < startOffset)
			{
				//MessageBox.Show("endOffset < startOffset");
				return;
			}
			ActiproSoftware.SyntaxEditor.HighlightingStyle highlightingStyle = new ActiproSoftware.SyntaxEditor.HighlightingStyle("Test", null, Color.Empty, Color.Red);
			ActiproSoftware.SyntaxEditor.HighlightingStyleSpanIndicator highlightSpan = new ActiproSoftware.SyntaxEditor.HighlightingStyleSpanIndicator("Test", highlightingStyle);
			highlightSpan.Tag = errDescription;

			this.AddSpanIndicator(
				editor,
				ActiproSoftware.SyntaxEditor.SpanIndicatorLayer.SyntaxErrorKey,
				ActiproSoftware.SyntaxEditor.SpanIndicatorLayer.SyntaxErrorDisplayPriority,
				highlightSpan, new ActiproSoftware.SyntaxEditor.TextRange(startOffset, endOffset));

			editor.SelectedView.GoToLine(line);
			editor.Caret.Offset = startOffset;
			editor.SelectedView.ScrollToCaret();

			if (superTabControl1.SelectedTab != superTabItemScript)
				superTabControl1.SelectedTab = superTabItemScript;

			string description = string.Format("{0}\nLine: {1}", errDescription, line);
			MessageBox.Show(this, description, "Runtime error", MessageBoxButtons.OK, MessageBoxIcon.Error);

			//if (superTabControl1.SelectedTab != superTabItemScript)
			//    superTabControl1.SelectedTab = superTabItemScript;
		}

		private void syntaxEditor1_ViewMouseHover(object sender, ActiproSoftware.SyntaxEditor.EditorViewMouseEventArgs e)
		{
			switch (e.HitTestResult.Target)
			{
				case ActiproSoftware.SyntaxEditor.SyntaxEditorHitTestTarget.IndicatorMargin:
					// Set the tooltip text for an indicator in the indicator margin if there is one
					if (e.HitTestResult.DisplayLine.IsFirstForDocumentLine && e.HitTestResult.DocumentLine != null)
					{
						ActiproSoftware.SyntaxEditor.Indicator[] indicators = e.HitTestResult.DocumentLine.GetAllVisibleIndicators();
						for (int index = indicators.Length - 1; index >= 0; index--)
						{
							ActiproSoftware.SyntaxEditor.Indicator indicator = indicators[index];
							if (indicator.HasGlyph)
							{
								e.ToolTipText = String.Format("A <b>{0}</b> indicator is under the mouse.", indicator.Name);
								break;
							}
						}
					}
					break;
				case ActiproSoftware.SyntaxEditor.SyntaxEditorHitTestTarget.TextArea:
					// Set the tooltip text for a span indicator in the text area if there is one
					if (e.HitTestResult.Offset != -1)
					{
						ActiproSoftware.SyntaxEditor.SpanIndicator[] indicators = syntaxEditor1.Document.SpanIndicatorLayers.GetIndicatorsForTextRange(new ActiproSoftware.SyntaxEditor.TextRange(e.HitTestResult.Offset), true);
						if ((indicators != null) && (indicators.Length > 0))
							e.ToolTipText = indicators[indicators.Length - 1].Tag.ToString()
								.Replace("Slyce.FunctionRunner.", "");
					}
					break;
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			timer1.Stop();
			timer1.Interval = 600;
			QuickCompile();
		}

		private string GetCSharpCode(string rawCode, int caretOffset, string extraClasses)
		{
			//caretOffset += 1;
			StringBuilder sb = new StringBuilder();
			string argString = "";

			if (comboBoxIterator.Text.StartsWith("one file per"))
			{
				ArchAngel.Interfaces.Template.IteratorTypes iterator = (ArchAngel.Interfaces.Template.IteratorTypes)Enum.Parse(typeof(ArchAngel.Interfaces.Template.IteratorTypes), comboBoxIterator.Text.Replace("one file per ", ""), true);
				argString = iterator.ToString() + " " + iterator.ToString().ToLower();
			}
			else if (comboBoxIterator.Text.StartsWith("one folder per"))
			{
				ArchAngel.Interfaces.Template.IteratorTypes iterator = (ArchAngel.Interfaces.Template.IteratorTypes)Enum.Parse(typeof(ArchAngel.Interfaces.Template.IteratorTypes), comboBoxIterator.Text.Replace("one folder per ", ""), true);
				argString = iterator.ToString() + " " + iterator.ToString().ToLower();
			}
			sb.AppendFormat(@"
					using System;
					using System.Xml;
					using System.Linq;
					using System.Text;
					using System.Collections.Generic;
					using Slyce.FunctionRunner;
					using ArchAngel.Interfaces.Scripting;
					using ArchAngel.Interfaces.Scripting.NHibernate.Model;
					using ArchAngel.NHibernateHelper;

					namespace Slyce.FunctionRunner
					{{
						{1}

						public class TempTest : Slyce.FunctionRunner.FunctionBase
						{{
							public static string GetFileName({0})
							{{
							", argString, extraClasses);

			rawCode = rawCode.Substring(0, caretOffset);

			int codeStart = rawCode.IndexOf(ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.DelimiterStart);
			int codeEnd = 0;

			if (codeStart >= 0)
				codeEnd = rawCode.IndexOf(ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.DelimiterEnd, codeStart);

			while (codeStart >= 0)
			{
				bool addWrite = false;

				if (codeStart + 2 < rawCode.Length &&
					rawCode[codeStart + 2] == '=')
				{
					codeStart += 3;
					sb.Append("Write(");
					addWrite = true;
				}
				else
					codeStart += 2;

				if (codeEnd > codeStart)
				{
					codeEnd += 2;
					sb.Append(rawCode.Substring(codeStart, codeEnd - codeStart - 2));

					if (addWrite)
						sb.Append(")");

					sb.Append(";");
					codeStart = rawCode.IndexOf(ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.DelimiterStart, codeEnd);

					if (codeStart >= 0)
						codeEnd = rawCode.IndexOf(ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.DelimiterEnd, codeStart);
				}
				else
				{
					sb.Append(rawCode.Substring(codeStart));
					break;
				}
			}
			return sb.ToString();
		}

		private void syntaxEditor1_TriggerActivated(object sender, ActiproSoftware.SyntaxEditor.TriggerEventArgs e)
		{
			ProcessEditorTriggerActivated(sender, e);
		}

		private void ProcessEditorTriggerActivated(object sender, ActiproSoftware.SyntaxEditor.TriggerEventArgs e)
		{
			SyntaxEditor editor = (SyntaxEditor)sender;
			int closePos = editor.Document.GetText(LineTerminator.Newline).LastIndexOf(ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.DelimiterEnd, editor.Caret.Offset);
			int openPos = editor.Document.GetText(LineTerminator.Newline).LastIndexOf(ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.DelimiterStart, editor.Caret.Offset);

			if (openPos < 0 || closePos > openPos)
				return;

			ArchAngel.Common.Generator gen = new ArchAngel.Common.Generator();
			string extraCode = Slyce.Common.Utility.StandardizeLineBreaks(gen.GetFunctionLookupClass(true), Slyce.Common.Utility.LineBreaks.Unix);

			syntaxEditorOffscreen.Document.Text = GetCSharpCode(editor.Document.GetText(LineTerminator.Newline), editor.Caret.Offset, extraCode);
			syntaxEditorOffscreen.Caret.Offset = syntaxEditorOffscreen.Document.Text.Length - 1;
			((ActiproSoftware.SyntaxEditor.Addons.CSharp.CSharpSyntaxLanguage)syntaxEditorOffscreen.SelectedView.GetCurrentLanguageForContext()).ShowIntelliPromptMemberList(syntaxEditorOffscreen, editor);
			return;
		}

		private Assembly[] _ReferencedAssemblies;

		private Assembly[] ReferencedAssemblies
		{
			get
			{
				if (_ReferencedAssemblies == null)
				{
					List<Assembly> assemblies = new List<Assembly>();
					// ArchAngel.Interfaces.dll
					assemblies.Add(System.Reflection.Assembly.GetAssembly(typeof(ArchAngel.Interfaces.Template.TemplateProject)));
					// System.dll
					assemblies.Add(System.Reflection.Assembly.GetAssembly(typeof(string)));

					_ReferencedAssemblies = assemblies.ToArray();
				}
				return _ReferencedAssemblies;
			}
		}

		private void syntaxEditor1_KeyDown(object sender, KeyEventArgs e)
		{
			ProcessEditorKeyDown(sender, e);
		}

		private void ProcessEditorKeyDown(object sender, KeyEventArgs e)
		{
			int openParen = 57;

			if ((e.Control && e.KeyCode == Keys.Space) ||
				(e.Shift && e.KeyValue == openParen))
			{
				SyntaxEditor editor = (SyntaxEditor)sender;
				int closePos = editor.Document.GetText(LineTerminator.Newline).LastIndexOf(ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.DelimiterEnd, editor.Caret.Offset);
				int openPos = editor.Document.GetText(LineTerminator.Newline).LastIndexOf(ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.DelimiterStart, editor.Caret.Offset);

				if (openPos < 0 || closePos > openPos)
					return;

				ArchAngel.Common.Generator gen = new ArchAngel.Common.Generator();
				string extraCode = Slyce.Common.Utility.StandardizeLineBreaks(gen.GetFunctionLookupClass(true), Slyce.Common.Utility.LineBreaks.Unix);


				string iteratorName;

				if (comboBoxIterator.Text == "one file" || comboBoxIterator.Text == "one folder")
					iteratorName = ArchAngel.Interfaces.Template.IteratorTypes.None.ToString().ToLower();
				else
					iteratorName = comboBoxIterator.Text.Substring(comboBoxIterator.Text.IndexOf(" per ") + 5).ToLower();

				if (e.Control && e.KeyCode == Keys.Space)
				{
					syntaxEditorOffscreen.Document.Text = GetCSharpCode(editor.Document.GetText(LineTerminator.Newline), editor.Caret.Offset, extraCode) + "}";
					syntaxEditorOffscreen.Caret.Offset = syntaxEditorOffscreen.Document.GetText(ActiproSoftware.SyntaxEditor.LineTerminator.Newline).Length - 1;

					((ActiproSoftware.SyntaxEditor.Addons.CSharp.CSharpSyntaxLanguage)syntaxEditorOffscreen.SelectedView.GetCurrentLanguageForContext()).IntelliPromptCompleteWord(syntaxEditorOffscreen, editor);
				}
				else if (e.Shift && e.KeyValue == openParen)
				{
					syntaxEditorOffscreen.Document.Text = GetCSharpCode(editor.Document.GetText(LineTerminator.Newline), editor.Caret.Offset, extraCode) + "(";
					syntaxEditorOffscreen.Caret.Offset = syntaxEditorOffscreen.Document.GetText(ActiproSoftware.SyntaxEditor.LineTerminator.Newline).Length;// -1;
					//syntaxEditorOffscreen.Focus();
					((ActiproSoftware.SyntaxEditor.Addons.CSharp.CSharpSyntaxLanguage)syntaxEditorOffscreen.SelectedView.GetCurrentLanguageForContext()).ShowIntelliPromptParameterInfo(syntaxEditorOffscreen, editor);
				}
			}
		}

		private void backgroundWorkerAddReferences_DoWork(object sender, DoWorkEventArgs e)
		{
			//dotNetProjectResolver.AddExternalReferenceForSystemAssembly("System");
			//dotNetProjectResolver.AddExternalReferenceForMSCorLib();

			AddMainFunctionAssemblyToProjectResolver();
		}

		private void AddMainFunctionAssemblyToProjectResolver()
		{
			// Don't re-add if external references have already been added
			if (dotNetProjectResolver.ExternalReferences.Count > 0)
				return;

			//foreach (string assemblyName in dotNetProjectResolver.ExternalReferences)
			//{
			//    // Don't re-add if external references have already been added
			//    if (assemblyName == "Slyce.FunctionRunner")
			//        return;
			//}

			CompileBaseAssemblyOnly();

			//string exeDir = Path.GetDirectoryName(Application.ExecutablePath);
			//List<string> referencedAssemblies = new List<string>();
			//referencedAssemblies.Add(Path.Combine(exeDir, "ArchAngel.Interfaces.dll"));
			//referencedAssemblies.Add(Path.Combine(exeDir, "ArchAngel.Providers.EntityModel.dll"));
			//referencedAssemblies.Add(Path.Combine(exeDir, "ArchAngel.NHibernateHelper.dll"));
			//List<System.CodeDom.Compiler.CompilerError> compileErrors;

			//ArchAngel.Common.Generator gen = new ArchAngel.Common.Generator(
			//                                    null,
			//                                    Controller.Instance.CurrentProject.TemplateLoader,
			//                                    null,
			//                                    referencedAssemblies,
			//                                    out compileErrors);


			//Assembly mainAssembly = gen.CurrentAssembly;
			//dotNetProjectResolver.AddExternalReference(mainAssembly, "Slyce.FunctionRunner");
			dotNetProjectResolver.AddExternalReference(BaseAssembly, "Slyce.FunctionRunner");
			dotNetProjectResolver.AddExternalReference(Assembly.GetAssembly(typeof(ArchAngel.Interfaces.Scripting.NHibernate.Model.IColumn)), "ArchAngel.Interfaces");
			dotNetProjectResolver.AddExternalReference(Assembly.GetAssembly(typeof(ArchAngel.NHibernateHelper.BytecodeGenerator)), "ArchAngel.NHibernateHelper");
#if DEBUG
			dotNetProjectResolver.AddExternalReference(Slyce.Common.RelativePaths.GetFullPath(System.Reflection.Assembly.GetExecutingAssembly().Location, @"..\..\..\..\3rd_Party_Libs\Inflector.Net.dll"));
#else
			dotNetProjectResolver.AddExternalReference(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Inflector.Net.dll"));
#endif

			dotNetProjectResolver.AddExternalReferenceForSystemAssembly("System");
			dotNetProjectResolver.AddExternalReferenceForSystemAssembly("System.Xml");
			dotNetProjectResolver.AddExternalReferenceForMSCorLib();

			//ArchAngel.Common.Generator gen = new ArchAngel.Common.Generator();
			//ActiproSoftware.SyntaxEditor.Addons.CSharp.CSharpSyntaxLanguage cSharpLanguage = new ActiproSoftware.SyntaxEditor.Addons.CSharp.CSharpSyntaxLanguage();

			//dotNetProjectResolver.SourceProjectContent.LoadForCode(cSharpLanguage, "Slyce.FunctionRunner", gen.GetFunctionLookupClass());
		}

		private void crumbBarTestObjects_SelectedItemChanging(object sender, DevComponents.DotNetBar.CrumbBarSelectionEventArgs e)
		{
			if (e.NewSelectedItem != null &&
				e.NewSelectedItem.Tag != null &&
				e.NewSelectedItem.Tag is ArchAngel.Interfaces.Scripting.NHibernate.Model.IEntity)
			{
				ArchAngel.Interfaces.Template.IteratorTypes iterator = ArchAngel.Interfaces.Template.IteratorTypes.None;

				if (treeFiles.SelectedNode.Tag is ArchAngel.Interfaces.Template.File)
					iterator = ((ArchAngel.Interfaces.Template.File)treeFiles.SelectedNode.Tag).Iterator;
				else if (treeFiles.SelectedNode.Tag is ArchAngel.Interfaces.Template.Folder)
					iterator = ((ArchAngel.Interfaces.Template.File)treeFiles.SelectedNode.Tag).Iterator;

				ArchAngel.Interfaces.Scripting.NHibernate.Model.IEntity entity = (ArchAngel.Interfaces.Scripting.NHibernate.Model.IEntity)e.NewSelectedItem.Tag;

				switch (iterator)
				{
					case ArchAngel.Interfaces.Template.IteratorTypes.Entity:
						//         DevComponents.DotNetBar.CrumbBarItem item = new DevComponents.DotNetBar.CrumbBarItem();
						//e.NewSelectedItem.SubItems.Add
						break;
					case ArchAngel.Interfaces.Template.IteratorTypes.Table:
						break;
					case ArchAngel.Interfaces.Template.IteratorTypes.Column:
						break;
					case ArchAngel.Interfaces.Template.IteratorTypes.Component:
						break;
					case ArchAngel.Interfaces.Template.IteratorTypes.None:
						break;
					default:
						throw new NotImplementedException("Iterator-type not handled yet: " + iterator.ToString());
				}

			}
		}

		private void buttonTest_Click(object sender, EventArgs e)
		{
			if (treeFiles.SelectedNode == null || treeFiles.SelectedNode.Tag == null)
				return;

			Cursor = Cursors.WaitCursor;

			try
			{
				string filename;
				string body;
				bool skipFile;

				if (treeFiles.SelectedNode.Tag is ArchAngel.Interfaces.Template.File)
				{
					ArchAngel.Interfaces.Template.File file = (ArchAngel.Interfaces.Template.File)treeFiles.SelectedNode.Tag;
					object iterator = null;

					if (file.Iterator != ArchAngel.Interfaces.Template.IteratorTypes.None)
					{
						if (!crumbBarTestObjects.SelectedItem.Tag.GetType().Name.EndsWith(file.Iterator.ToString()))
						{
							MessageBox.Show(this, "Please select a valid " + file.Iterator.ToString(), "Invalid iterator", MessageBoxButtons.OK, MessageBoxIcon.Warning);
							return;
						}
						else
							iterator = crumbBarTestObjects.SelectedItem.Tag;
					}
					QuickCompile(true, file.Id, iterator, out filename, out body, out skipFile);
				}
				else if (treeFiles.SelectedNode.Tag is ArchAngel.Interfaces.Template.Folder)
				{
					ArchAngel.Interfaces.Template.Folder folder = (ArchAngel.Interfaces.Template.Folder)treeFiles.SelectedNode.Tag;
					object iterator = null;

					if (folder.Iterator != ArchAngel.Interfaces.Template.IteratorTypes.None)
					{
						if (crumbBarTestObjects.SelectedItem.Tag.GetType().Name != folder.Iterator.ToString() + "Base")
						{
							MessageBox.Show(this, "Please select a valid " + folder.Iterator.ToString(), "Invalid iterator", MessageBoxButtons.OK, MessageBoxIcon.Warning);
							return;
						}
						iterator = crumbBarTestObjects.SelectedItem.Tag;
					}
					QuickCompile(true, folder.ID, iterator, out filename, out body, out skipFile);
				}
				else
					throw new NotImplementedException("Unexpected node-type: " + treeFiles.SelectedNode.Tag.GetType().Name);


				labelResults.Visible = true;
				syntaxEditorTest.Visible = true;

				if (skipFile)
				{
					labelResults.Text = "  FILE SKIPPED";
					syntaxEditorTest.Document.Text = string.Format("This file won't get written to disk because 'SkipThisFile' was set to true.\n\nReason:\n{0}", body);
					SetSyntax(TemplateContentLanguage.PlainText, syntaxEditorTest);
				}
				else
				{
					if (treeFiles.SelectedNode == null)
						labelResults.Text = string.Format("  Filename or foldername:  <b>{0}</b>", filename.Trim());
					else if (treeFiles.SelectedNode.Tag is ArchAngel.Interfaces.Template.File)
					{
						labelResults.Text = string.Format("  Filename:  <b>{0}</b>", filename.Trim());
						//labelResults.Image = imageList1.Images[IMG_FILE];
					}
					else if (treeFiles.SelectedNode.Tag is ArchAngel.Interfaces.Template.Folder)
					{
						labelResults.Text = string.Format("  Folder:  <b>{0}</b>", filename.Trim());
						//labelResults.Image = imageList1.Images[IMG_CLOSED_FOLDER];
					}
					syntaxEditorTest.Document.Text = body;
					SetSyntax(Slyce.Common.SyntaxEditorHelper.LanguageEnumFromName(comboBoxSyntax.Text), syntaxEditorTest);
				}
			}
			finally
			{
				Cursor = Cursors.Default;
			}
		}

		private void crumbBarTestObjects_SelectedItemChanged(object sender, DevComponents.DotNetBar.CrumbBarSelectionEventArgs e)
		{
			labelResults.Visible = false;
			syntaxEditorTest.Visible = false;

			if (BusyPopulatingCrumbBarTest || e.NewSelectedItem == null || e.NewSelectedItem.Tag == null)
				return;

			ArchAngel.Interfaces.Template.IteratorTypes iteratorType = Interfaces.Template.IteratorTypes.None;

			string name = e.NewSelectedItem.Tag.GetType().Name;

			switch (name)
			{
				case "IEntity":
					iteratorType = Interfaces.Template.IteratorTypes.Entity;
					break;
				case "IColumn":
					iteratorType = Interfaces.Template.IteratorTypes.Column;
					break;
				case "IComponent":
					iteratorType = Interfaces.Template.IteratorTypes.Component;
					break;
				case "ITable":
					iteratorType = Interfaces.Template.IteratorTypes.Table;
					break;
				case "String":
					// Do nothing
					break;
				default:
					throw new NotImplementedException("IteratorType not handled yet: " + name);
			}
			if (!LatestTestObjects.ContainsKey(iteratorType))
				LatestTestObjects.Add(iteratorType, e.NewSelectedItem.Tag);
			else if (LatestTestObjects[iteratorType] != e.NewSelectedItem)
				LatestTestObjects[iteratorType] = e.NewSelectedItem.Tag;
		}

		private void mnuDelete_Click(object sender, EventArgs e)
		{
			if (ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.IsOfficial)
			{
				if (MessageBox.Show(this, "Changes can't be made to built-in templates. Do you want to save-as a custom template?", "Built-in template", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
				{
					SaveAs();
				}
				return;
			}
			try
			{
				if (treeFiles.SelectedNodes.Count > 1)
				{
					throw new Exception("Only one node can be deleted at a time.");
				}
				Cursor = Cursors.WaitCursor;
				Refresh();
				Node selectedNode = treeFiles.SelectedNodes[0];

				if (selectedNode.Tag is ArchAngel.Interfaces.Template.Folder)
				{
					ArchAngel.Interfaces.Template.Folder folderToDelete = (ArchAngel.Interfaces.Template.Folder)selectedNode.Tag;
					folderToDelete.ParentFolder.Folders.Remove(folderToDelete);
				}
				else if (selectedNode.Tag is ArchAngel.Interfaces.Template.File)
				{
					ArchAngel.Interfaces.Template.File fileToDelete = (ArchAngel.Interfaces.Template.File)selectedNode.Tag;
					fileToDelete.ParentFolder.Files.Remove(fileToDelete);
				}
				else if (selectedNode.Tag is ArchAngel.Interfaces.Template.StaticFile)
				{
					ArchAngel.Interfaces.Template.StaticFile fileToDelete = (ArchAngel.Interfaces.Template.StaticFile)selectedNode.Tag;
					fileToDelete.ParentFolder.StaticFiles.Remove(fileToDelete);
				}
				else
					throw new NotImplementedException("Node-type not handled yet: " + selectedNode.Tag.GetType().Name);

				selectedNode.Remove();
			}
			finally
			{
				Controller.Instance.MainForm.Activate();
				Cursor = Cursors.Default;
			}
		}

		private void superTabControl1_SelectedTabChanged(object sender, DevComponents.DotNetBar.SuperTabStripSelectedTabChangedEventArgs e)
		{
			if (e.NewValue == superTabItemScript && !string.IsNullOrEmpty(comboBoxSyntax.Text))
				SetSyntax(Slyce.Common.SyntaxEditorHelper.LanguageEnumFromName(comboBoxSyntax.Text), syntaxEditor1, syntaxEditorTest);
			else if (e.NewValue == superTabItemTest)
			{
				if (treeFiles.SelectedNode == null ||
					treeFiles.SelectedNode.Tag == null)
					return;

				if (treeFiles.SelectedNode.Tag is ArchAngel.Interfaces.Template.File)
				{
					ArchAngel.Interfaces.Template.File file = (ArchAngel.Interfaces.Template.File)treeFiles.SelectedNode.Tag;

					if (file.Iterator != ArchAngel.Interfaces.Template.IteratorTypes.None)
						PopulateTestCrumbBar(file.Iterator);
				}
				else if (treeFiles.SelectedNode.Tag is ArchAngel.Interfaces.Template.Folder)
				{
					ArchAngel.Interfaces.Template.Folder folder = (ArchAngel.Interfaces.Template.Folder)treeFiles.SelectedNode.Tag;

					if (folder.Iterator != ArchAngel.Interfaces.Template.IteratorTypes.None)
						PopulateTestCrumbBar(folder.Iterator);
				}
			}
		}

		private void buttonAddBinaryFile_Click(object sender, EventArgs e)
		{
			if (ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.IsOfficial)
			{
				if (MessageBox.Show(this, "Changes can't be made to built-in templates. Do you want to save-as a custom template?", "Built-in template", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
				{
					SaveAs();
				}
				return;
			}
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Multiselect = true;

			if (dialog.ShowDialog(this) == DialogResult.OK)
			{
				string templatesFolder = ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.ResourceFilesFolder;

				foreach (var file in dialog.FileNames)
				{
					string filename = Path.GetFileName(file);
					string newFilepath = Path.Combine(templatesFolder, filename);

					if (!file.Equals(newFilepath, StringComparison.InvariantCultureIgnoreCase))
					{
						if (File.Exists(newFilepath))
							File.Delete(newFilepath);

						File.Copy(file, newFilepath);
					}
					if (!ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.ResourceFiles.Any(f => f.Equals(newFilepath, StringComparison.InvariantCultureIgnoreCase)))
						ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.ResourceFiles.Add(filename);
				}
				PopulateStaticFiles();
			}
		}

		private void listViewEx1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete && listViewResources.SelectedItems.Count > 0)
			{
				DeleteStaticFiles();
			}
		}

		private void mnuDeleteStaticFiles_Click(object sender, EventArgs e)
		{
			DeleteStaticFiles();
		}

		private void DeleteStaticFiles()
		{
			if (ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.IsOfficial)
			{
				if (MessageBox.Show(this, "Changes can't be made to built-in templates. Do you want to save-as a custom template?", "Built-in template", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
				{
					SaveAs();
				}
				return;
			}
			if (MessageBox.Show(this, string.Format("Delete {0} files?", listViewResources.SelectedItems.Count), "Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
			{
				for (int i = listViewResources.SelectedItems.Count - 1; i >= 0; i--)
				{
					ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.DeleteResourceFile(listViewResources.SelectedItems[i].Text);
					listViewResources.Items.Remove(listViewResources.SelectedItems[i]);
				}
				SetWidthOfStaticFileList();
			}
		}

		private void mnuNewStaticFile_Click(object sender, EventArgs e)
		{
			AddNewStaticFile();
		}

		private void comboBoxStaticFiles_SelectedIndexChanged(object sender, EventArgs e)
		{
			syntaxEditorFilename.Document.Text = comboBoxStaticFiles.Text;
		}

		private void mnuAddManyStaticFiles_Click(object sender, EventArgs e)
		{
			if (ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.IsOfficial)
			{
				if (MessageBox.Show(this, "Changes can't be made to built-in templates. Do you want to save-as a custom template?", "Built-in template", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
				{
					SaveAs();
				}
				return;
			}
			FormStaticFiles form = new FormStaticFiles();

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				Node selectedNode = treeFiles.SelectedNodes[0];
				ArchAngel.Interfaces.Template.Folder parentFolder = (ArchAngel.Interfaces.Template.Folder)selectedNode.Tag;

				treeFiles.BeginUpdate();

				foreach (var name in form.ResourceNames)
				{
					ArchAngel.Interfaces.Template.StaticFile newFile = new ArchAngel.Interfaces.Template.StaticFile()
					{
						Id = ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.GetNextAvailableStaticFileId(),
						Name = name,
						Iterator = ArchAngel.Interfaces.Template.IteratorTypes.None,
						ParentFolder = parentFolder,
						ResourceName = name
					};
					parentFolder.StaticFiles.Add(newFile);
					AddStaticFileNode(selectedNode, newFile);
				}
				treeFiles.EndUpdate();
				syntaxEditorFilename.Focus();
			}
		}

		private void syntaxEditorSkipStaticFile_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.Space)
			{
				SyntaxEditor editor = (SyntaxEditor)sender;

				ArchAngel.Common.Generator gen = new ArchAngel.Common.Generator();
				string extraCode = Slyce.Common.Utility.StandardizeLineBreaks(gen.GetFunctionLookupClass(true), Slyce.Common.Utility.LineBreaks.Unix);

				syntaxEditorOffscreen.Document.Text = GetCSharpCode(ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.DelimiterStart + editor.Document.GetText(LineTerminator.Newline) + ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.DelimiterEnd, editor.Caret.Offset + 2, extraCode) + "}";
				syntaxEditorOffscreen.Caret.Offset = syntaxEditorOffscreen.Document.GetText(ActiproSoftware.SyntaxEditor.LineTerminator.Newline).Length - 1;

				string iteratorName;

				if (comboBoxIterator.Text == "one file" || comboBoxIterator.Text == "one folder")
					iteratorName = ArchAngel.Interfaces.Template.IteratorTypes.None.ToString().ToLower();
				else
					iteratorName = comboBoxIterator.Text.Substring(comboBoxIterator.Text.IndexOf(" per ") + 5).ToLower();

				((ActiproSoftware.SyntaxEditor.Addons.CSharp.CSharpSyntaxLanguage)syntaxEditorOffscreen.SelectedView.GetCurrentLanguageForContext()).IntelliPromptCompleteWord(syntaxEditorOffscreen, syntaxEditorSkipStaticFile);//, syntaxEditor1);
			}
		}

		private void syntaxEditorSkipStaticFile_TriggerActivated(object sender, TriggerEventArgs e)
		{
			SyntaxEditor editor = (SyntaxEditor)sender;
			ArchAngel.Common.Generator gen = new ArchAngel.Common.Generator();
			string extraCode = Slyce.Common.Utility.StandardizeLineBreaks(gen.GetFunctionLookupClass(true), Slyce.Common.Utility.LineBreaks.Unix);

			syntaxEditorOffscreen.Document.Text = GetCSharpCode(ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.DelimiterStart + editor.Document.GetText(LineTerminator.Newline) + ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.DelimiterEnd, editor.Caret.Offset + 2, extraCode);
			syntaxEditorOffscreen.Caret.Offset = syntaxEditorOffscreen.Document.Text.Length - 1;
			((ActiproSoftware.SyntaxEditor.Addons.CSharp.CSharpSyntaxLanguage)syntaxEditorOffscreen.SelectedView.GetCurrentLanguageForContext()).ShowIntelliPromptMemberList(syntaxEditorOffscreen, syntaxEditorSkipStaticFile);
		}

		private void syntaxEditorFilename_KeyDown(object sender, KeyEventArgs e)
		{
			ProcessEditorKeyDown(sender, e);
		}

		private void syntaxEditorFilename_TriggerActivated(object sender, TriggerEventArgs e)
		{
			ProcessEditorTriggerActivated(sender, e);
		}

		private void comboBoxDelimiter_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxDelimiter.SelectedIndex == 0)
				ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.Delimiter = Interfaces.Template.TemplateProject.DelimiterTypes.ASP;
			else
				ArchAngel.Interfaces.SharedData.CurrentProject.TemplateProject.Delimiter = Interfaces.Template.TemplateProject.DelimiterTypes.T4;

			SetSyntax(Slyce.Common.SyntaxEditorHelper.LanguageEnumFromName(comboBoxSyntax.Text), true, syntaxEditor1, syntaxEditorTest);
		}

		public void ShowFindForm(bool showReplace, bool searchAllFiles)
		{
			bool formExists = true;

			if (FormFind == null)
			{
				formExists = false;
				FormFind = new frmFind(showReplace, searchAllFiles);
			}
			if (SyntaxEditor != null)
			{
				//string selectedText = ((ActiproSoftware.SyntaxEditor.SyntaxEditor)((ucFunction)MainForm.UcFunctions.tabStrip1.SelectedPage.Controls[0]).Controls[1]).SelectedView.SelectedText;
				string selectedText = SyntaxEditor.SelectedView.SelectedText;

				if (selectedText.Length > 0 &&
					 selectedText.IndexOf("\n") < 0 &&
					 selectedText.IndexOf("\r") < 0)
				{
					SearchHelper.Options.FindText = selectedText;
				}
				FormFind.SetSearchText();
			}
			FormFind.TopMost = false;

			if (formExists)
				FormFind.Show();
			else
				FormFind.Show(this);
		}
	}
}
