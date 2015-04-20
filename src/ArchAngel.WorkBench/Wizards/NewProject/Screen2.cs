using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using ArchAngel.Common.DesignerProject;
using ArchAngel.Interfaces.Wizards.NewProject;

namespace ArchAngel.Workbench.Wizards.NewProject
{
	[SmartAssembly.Attributes.DoNotObfuscate]
	public partial class Screen2 : UserControl, INewProjectScreen
	{
		private readonly List<TemplateDefinition> UserTemplates = new List<TemplateDefinition>();
		private readonly List<TemplateDefinition> SampleTemplates = new List<TemplateDefinition>();
		private readonly string myProjectsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Branding.ProductName + Path.DirectorySeparatorChar + "Projects");
		private bool ShowBackButton = true;

		private class Screen2Data
		{
			public string SelectedTemplateName;

			public Screen2Data(string templateName)
			{
				SelectedTemplateName = templateName;
			}
		}

		public Screen2()
		{
			InitializeComponent();
		}

		public IFormNewProject NewProjectForm { get; set; }

		public void Setup()
		{
			NewProjectForm.Text = "New Project";

			switch (NewProjectForm.SetupAction)
			{
				case NewProjectFormActions.ExistingProject:
					break;
				case NewProjectFormActions.NewProject:
					btnBack.Text = "Cancel";
					ShowBackButton = false;
					//this.ParentForm.CancelButton = btnBack;
					break;
				case NewProjectFormActions.None:
					break;
				default:
					throw new NotImplementedException("Not handled yet: " + NewProjectForm.SetupAction);
			}
			NewProjectForm.UserChosenAction = NewProjectFormActions.None;
			if (!Directory.Exists(myProjectsFolder))
			{
				Directory.CreateDirectory(myProjectsFolder);
			}

			listTemplates.Groups.Clear();
			ListViewGroup group = new ListViewGroup("Sample templates", HorizontalAlignment.Left);
			listTemplates.Groups.Add(group);
			group = new ListViewGroup("My templates", HorizontalAlignment.Left);
			listTemplates.Groups.Add(group);
			FillTemplateCollection();

			var screenDataObj = NewProjectForm.GetScreenData(typeof(Screen2).FullName) as Screen2Data;
			if (screenDataObj != null)
			{
				listTemplates.FocusedItem = listTemplates.FindItemWithText(screenDataObj.SelectedTemplateName);
			}
		}

		private void btnBack_Click(object sender, EventArgs e)
		{
			if (listTemplates.FocusedItem != null)
			{
				NewProjectForm.SetScreenData(typeof(Screen2).FullName, new Screen2Data(((TemplateDefinition)listTemplates.FocusedItem.Tag).Name));
			}

			if (!ShowBackButton)
			{
				NewProjectForm.UserChosenAction = NewProjectFormActions.None;
				NewProjectForm.Close();
			}
			NewProjectForm.LoadScreen(typeof(Screen1));
		}

		private void PopulateProviders(List<string> allProviders)
		{
			tvProviders.Nodes.Clear();
			tvProviders.Nodes.Add("All");

			foreach (string providerName in allProviders)
			{
				tvProviders.Nodes.Add(providerName);
			}
			tvProviders.SelectedNode = tvProviders.Nodes[0];
		}

		private void FillTemplateCollection()
		{
			UserTemplates.Clear();
			SampleTemplates.Clear();
			List<string> allProviders = new List<string>();

#if DEBUG
			string templateFolder = Slyce.Common.RelativePaths.RelativeToAbsolutePath(Path.GetDirectoryName(Application.ExecutablePath), @"..\..\..\ArchAngel.Templates");
#else
			//string templateFolder = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Templates");
			string templateFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Branding.ProductName + Path.DirectorySeparatorChar + "Templates");
#endif
			if (Directory.Exists(templateFolder))
			{
				foreach (string file in Slyce.Common.Utility.GetFiles(templateFolder, "*.AAT.DLL", SearchOption.AllDirectories))
				{
					List<string> providerNames = new List<string>();
					TemplateDefinition template = GetTemplateDefinition(file, providerNames);

					foreach (string providerName in providerNames)
					{
						if (allProviders.BinarySearch(providerName) < 0)
						{
							allProviders.Add(providerName);
							allProviders.Sort();
						}
					}
					UserTemplates.Add(template);
				}
			}
			// Also fetch any sample templates
			List<string> sampleTemplateFolders = new List<string>();
			sampleTemplateFolders.Add(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase.Replace(@"file:///", "")), "Samples" + Path.DirectorySeparatorChar + "Templates"));
			// Also check the install folder
			sampleTemplateFolders.Add(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase.Replace(@"file:///", "")));

			foreach (string subTemplateFolder in sampleTemplateFolders)
			{
				if (Directory.Exists(subTemplateFolder))
				{
					foreach (string file in Slyce.Common.Utility.GetFiles(subTemplateFolder, "*.AAT.DLL", SearchOption.AllDirectories))
					{
						List<string> providerNames = new List<string>();
						TemplateDefinition template = GetTemplateDefinition(file, providerNames);

						foreach (string providerName in providerNames)
						{
							if (allProviders.BinarySearch(providerName) < 0)
							{
								allProviders.Add(providerName);
								allProviders.Sort();
							}
						}
						SampleTemplates.Add(template);
					}
				}
			}
			PopulateProviders(allProviders);
		}

		private TemplateDefinition GetTemplateDefinition(string file, List<string> providerNames)
		{
			Assembly ass = Assembly.ReflectionOnlyLoadFrom(file);
			return ProjectDeserialiserV1.GetTemplateDefinitionFromXML(providerNames, file, GetEmbeddedResourceText(ass, "options.xml"));
		}

		private string GetEmbeddedResourceText(Assembly assembly, string resourceName)
		{
			string filePath = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());
			Stream outs = assembly.GetManifestResourceStream(resourceName);
			outs.Seek(0, SeekOrigin.Begin);
			StreamReader srException = new StreamReader(outs);
			StreamWriter oWrite = new StreamWriter(filePath, false, System.Text.Encoding.Unicode);
			oWrite.Write(srException.ReadToEnd());
			oWrite.Close();
			return Slyce.Common.Utility.ReadTextFile(filePath);
		}

		private void PopulateTemplates()
		{
			listTemplates.Clear();
			ListViewItem item;

			#region Sample Templates
			if (tvProviders.SelectedNode.Text == "All")
			{
				foreach (TemplateDefinition template in SampleTemplates)
				{
					item = listTemplates.Items.Add(template.Name);
					item.Tag = template;
					item.ImageIndex = item.StateImageIndex = 0;
					//item.ToolTipText = string.Format("{0}\n{1}", template.Path, template.Description);
					item.Group = listTemplates.Groups[0];
				}
			}
			else
			{
				foreach (TemplateDefinition template in SampleTemplates)
				{
					if (template.Providers.BinarySearch(tvProviders.SelectedNode.Text) >= 0)
					{
						item = listTemplates.Items.Add(template.Name);
						item.Tag = template;
						item.ImageIndex = item.StateImageIndex = 0;
						//item.ToolTipText = string.Format("{0}\n{1}", template.Path, template.Description);
						item.Group = listTemplates.Groups[0];
					}
				}
			}
			#endregion

			if (listTemplates.Items.Count > 0)
			{
				listTemplates.Items[0].Selected = true;
				listTemplates.FocusedItem = listTemplates.Items[0];
			}
			item = listTemplates.Items.Add("Search for a template...");
			item.Tag = "LocalSearch";
			item.ImageIndex = item.StateImageIndex = 2;
			//item.ToolTipText = "Search for a template on your computer or network.";
			item.Group = listTemplates.Groups[1];

			#region User Templates
			if (tvProviders.SelectedNode.Text == "All")
			{
				foreach (TemplateDefinition template in UserTemplates)
				{
					item = listTemplates.Items.Add(template.Name);
					item.Tag = template;
					item.ImageIndex = item.StateImageIndex = 0;
					//item.ToolTipText = string.Format("{0}\n{1}", template.Path, template.Description);
					item.Group = listTemplates.Groups[1];
				}
			}
			else
			{
				foreach (TemplateDefinition template in UserTemplates)
				{
					if (template.Providers.BinarySearch(tvProviders.SelectedNode.Text) >= 0)
					{
						item = listTemplates.Items.Add(template.Name);
						item.Tag = template;
						item.ImageIndex = item.StateImageIndex = 0;
						//superTooltip1.SetSuperTooltip(item, new DevComponents.DotNetBar.SuperTooltipInfo() { HeaderText = template.Name, FooterText = template.Path, BodyText = template.Description });
						//item.ToolTipText = string.Format("{0}\n{1}", template.Path, template.Description);
						item.Group = listTemplates.Groups[1];
					}
				}
			}
			#endregion

			SetTemplateDescription();
		}

		private void tvProviders_AfterSelect(object sender, TreeViewEventArgs e)
		{
			PopulateTemplates();
		}

		private void listTemplates_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void SetTemplateDescription()
		{
			if (listTemplates.FocusedItem != null)
			{
				lblTemplateDescription.Text = ((TemplateDefinition)listTemplates.FocusedItem.Tag).Description;
			}
			else
			{
				lblTemplateDescription.Text = "No template selected";
			}
		}

		private void btnNext_Click(object sender, EventArgs e)
		{
			if (listTemplates.FocusedItem == null || listTemplates.FocusedItem.Tag == null || !typeof(TemplateDefinition).IsInstanceOfType(listTemplates.FocusedItem.Tag))
			{
				MessageBox.Show("No template selected.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			// Set screen data
			NewProjectForm.SetScreenData(typeof(Screen2).FullName, new Screen2Data(((TemplateDefinition)listTemplates.FocusedItem.Tag).Name));

			NewProjectForm.NewProjectTemplate = ((TemplateDefinition)listTemplates.FocusedItem.Tag).Path;
			NewProjectForm.UserChosenAction = NewProjectFormActions.NewProject;
			NewProjectForm.Finish();
		}

		private void listTemplates_MouseClick(object sender, MouseEventArgs e)
		{
			ListViewItem item = listTemplates.GetItemAt(e.X, e.Y);

			if (item == null)
			{
				return;
			}
			if (item.Tag != null && typeof(TemplateDefinition).IsInstanceOfType(item.Tag))
			{
				SetTemplateDescription();
			}
			else if (typeof(string).IsInstanceOfType(item.Tag))
			{
				switch ((string)item.Tag)
				{
					case "LocalSearch":
						OpenFileDialog dialog = new OpenFileDialog();
						string path = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Providers");

						if (Directory.Exists(path))
						{
							dialog.InitialDirectory = path;
						}
						else
						{
							dialog.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
						}
						dialog.Filter = Branding.ProductName + " Template (*.AAT.DLL)|*.AAT.DLL";

						if (dialog.ShowDialog(ParentForm) == DialogResult.OK)
						{
							List<string> providerNames = new List<string>();
							TemplateDefinition template = GetTemplateDefinition(dialog.FileName, providerNames);
							ListViewItem newItem = listTemplates.Items.Add(template.Name);
							newItem.Tag = template;
							newItem.ImageIndex = newItem.StateImageIndex = 0;
							newItem.ToolTipText = template.Description;
							newItem.Group = listTemplates.Groups[1];
							newItem.Selected = true;
							newItem.Focused = true;
						}
						break;
					default:
						throw new NotImplementedException("Not handled yet: " + (string)item.Tag);
				}
			}
		}

		private void listTemplates_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
		{
			if (e.Item.Tag != null)
			{
				if (typeof(string).IsInstanceOfType(e.Item.Tag) && (string)e.Item.Tag == "LocalSearch")
				{
					superTooltip1.SetSuperTooltip(listTemplates, new DevComponents.DotNetBar.SuperTooltipInfo { HeaderText = "Search", FooterText = "", BodyText = "Search for a template on your computer or network." });
					superTooltip1.CheckTooltipPosition = false;
					Point pos = e.Item.Position;
					pos.Offset(new Point(5, 20));
					superTooltip1.ShowTooltip(listTemplates, listTemplates.PointToScreen(pos));
				}
				else if (typeof(TemplateDefinition).IsInstanceOfType(e.Item.Tag))
				{
					TemplateDefinition template = (TemplateDefinition)e.Item.Tag;
					superTooltip1.SetSuperTooltip(listTemplates, new DevComponents.DotNetBar.SuperTooltipInfo { HeaderText = template.Name, FooterText = template.Path, BodyText = template.Description });
					superTooltip1.CheckTooltipPosition = false;
					Point pos = e.Item.Position;
					pos.Offset(new Point(5, 20));
					superTooltip1.ShowTooltip(listTemplates, listTemplates.PointToScreen(pos));
				}
			}
		}
	}
}
