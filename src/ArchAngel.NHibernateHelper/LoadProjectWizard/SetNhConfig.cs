using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ArchAngel.Interfaces.Wizards.NewProject;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using Slyce.Common;

namespace ArchAngel.NHibernateHelper.LoadProjectWizard
{
	public partial class SetNhConfig : UserControl, INewProjectScreen
	{
		private NHConfigFile nhConfigFile = null;

		private class ScreenData
		{
			public string ExistingFilename;
		}

		private static readonly string ScreenDataKey = typeof(SetNhConfig).FullName;

		public SetNhConfig()
		{
			InitializeComponent();

			superTabControl1.SelectedTab = superTabItemGeneral;
			PopulateComboBoxes();
		}

		private void PopulateComboBoxes()
		{
			comboBoxByteCodeGenerator.Items.AddRange(Enum.GetNames(typeof(NHibernateHelper.BytecodeGenerator)));
			comboBoxByteCodeGenerator.SelectedIndex = 0;

			comboBoxCacheUseMinimalPuts.Items.AddRange(new string[] { "True", "False" });
			comboBoxCacheUseMinimalPuts.Text = "True";

			comboBoxCacheUseQueryCache.Items.AddRange(new string[] { "True", "False" });
			comboBoxCacheUseQueryCache.Text = "True";

			comboBoxGenerateStatistics.Items.AddRange(new string[] { "True", "False" });
			comboBoxGenerateStatistics.Text = "False";

			comboBoxShowSql.Items.AddRange(new string[] { "True", "False" });
			comboBoxShowSql.Text = "False";

			comboBoxUseOuterJoin.Items.AddRange(new string[] { "True", "False" });
			comboBoxUseOuterJoin.Text = "False";

			comboBoxUseProxyValidator.Items.AddRange(new string[] { "True", "False" });
			comboBoxUseProxyValidator.Text = "True";
		}

		public IFormNewProject NewProjectForm { get; set; }

		public void Setup()
		{
			var screenData = NewProjectForm.GetScreenData(ScreenDataKey) as ScreenData;
		}

		private void buttonBrowseProjectFile_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog fileDialog = new OpenFileDialog();
			fileDialog.Filter = "C# Visual Studio Project (*.csproj)|*.csproj";
			fileDialog.CheckFileExists = true;
			fileDialog.Multiselect = false;

			if (fileDialog.ShowDialog(this) != DialogResult.OK)
				return;

			tbProjectLocation.Text = fileDialog.FileName;
		}

		private void tbProjectLocation_TextChanged(object sender, System.EventArgs e)
		{
			nhConfigFile = null;
			panelConfigSettings.Visible = false;
			radioFile.Checked = false;
			radioManual.Checked = false;
			labelConfigFile.Visible = false;
			textBoxConfigFileLocation.Visible = false;
			buttonBrowse.Visible = false;
			superTabControl1.Visible = false;

			if (ValidateProjectLocation())
			{
				highlighter1.SetHighlightColor(tbProjectLocation, DevComponents.DotNetBar.Validator.eHighlightColor.None);

				if (NHibernateHelper.ProjectLoader.IsFluentProject(tbProjectLocation.Text) && !FluentCompiledAssemblyFound())
				{
					MessageBox.Show(this, "Compiled assembly not found for this Fluent NHibernate project. Please recompile the project, then try again.", "Compiled assembly missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}

				// Check whether we can find a config file
				nhConfigFile = GetNhConfigFileFromCsprojFile(tbProjectLocation.Text);

				if (nhConfigFile == null)
				{
					MessageBox.Show(this, "No NHibernate config file could be found for this project. If the file exists in another project then please locate it, otherwise manually enter the settings.", "NH config file missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					radioFile.Visible = true;
					radioManual.Visible = true;
				}
				else
				{
					radioFile.Visible = false;
					radioManual.Visible = false;
				}
				panelConfigSettings.Visible = nhConfigFile == null;
			}
			else
				highlighter1.SetHighlightColor(tbProjectLocation, DevComponents.DotNetBar.Validator.eHighlightColor.Orange);
		}

		private bool ValidateProjectLocation()
		{
			if (!File.Exists(tbProjectLocation.Text))
				return false;

			return true;
		}

		private bool FluentCompiledAssemblyFound()
		{
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(File.ReadAllText(tbProjectLocation.Text));
			CSProjFile csProjFile = new CSProjFile(doc, tbProjectLocation.Text);

			string tempFluentPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Visual NHibernate" + Path.DirectorySeparatorChar + "Temp" + Path.DirectorySeparatorChar + "FluentTemp");

			try
			{
				var fluentHbmFiles = NHibernateHelper.ProjectLoader.GetHBMFilesForFluentFromCSProj(csProjFile, tempFluentPath);
			}
			catch (FluentNHibernateCompiledAssemblyMissingException e)
			{
				return false;
			}
			return true;
		}

		private void buttonNext_Click(object sender, System.EventArgs e)
		{
			if (ValidateProjectLocation())
			{
				if (NHibernateHelper.ProjectLoader.IsFluentProject(tbProjectLocation.Text) &&
					!FluentCompiledAssemblyFound())
				{
					MessageBox.Show(this, "Compiled assembly not found for this Fluent NHibernate project. Please recompile the project, then try again.", "Compiled assembly missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				if (radioManual.Checked)
				{
					if (ucDatabaseInformation1.ConnectionStringHelper.CurrentDbType == DatabaseTypes.Unknown)
					{
						MessageBox.Show(this, "Please select a database.", "Missing data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					bool informationFilled = LoadExistingDatabase.TestConnection(false, ucDatabaseInformation1);

					if (informationFilled == false)
						return;

					nhConfigFile = CreateConfigFileManually(ucDatabaseInformation1.SelectedDatabaseType, ucDatabaseInformation1.ConnectionStringHelper.GetNHConnectionStringSqlClient());
				}
				if (nhConfigFile == null)
				{
					//MessageBox.Show(this, "No config file specified.", "Config file missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					// Check whether we can find a config file
					nhConfigFile = GetNhConfigFileFromCsprojFile(tbProjectLocation.Text);

					if (nhConfigFile == null)
					{
						MessageBox.Show(this, "No NHibernate config file could be found for this project. If the file exists in another project then please locate it, otherwise manually enter the settings.", "NH config file missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						radioFile.Visible = true;
						radioManual.Visible = true;
					}
					else
					{
						radioFile.Visible = false;
						radioManual.Visible = false;
					}
					panelConfigSettings.Visible = nhConfigFile == null;
					return;
				}
				var newProjectInfo = new LoadExistingNHibernateProjectInfo();
				newProjectInfo.Filename = tbProjectLocation.Text;
				newProjectInfo.NhConfigFile = nhConfigFile;
				NewProjectForm.NewProjectInformation = newProjectInfo;
				NewProjectForm.NewProjectOutputPath = Path.GetDirectoryName(tbProjectLocation.Text);

				//if (radioManual.Checked)
				//{
				//    NewProjectForm.NewProjectInformation = new LoadExistingDatabaseInfo
				//                                            {
				//                                                DatabaseLoader = DatabasePresenter.CreateDatabaseLoader(ucDatabaseInformation1),
				//                                                ConnStringHelper = ucDatabaseInformation1.GetHelper()
				//                                            };
				//}
				NewProjectForm.SetScreenData(
												ScreenDataKey,
												new ScreenData
												{
													ExistingFilename = tbProjectLocation.Text
												});

				// Skip the Database, SelectSchemaObjects and Prefixes screens.
				NewProjectForm.SkipScreens(3);
				NewProjectForm.UserChosenAction = NewProjectFormActions.NewProject;
				NewProjectForm.Finish();
			}
			else
			{
				highlighter1.SetHighlightColor(tbProjectLocation, DevComponents.DotNetBar.Validator.eHighlightColor.Orange);
				MessageBox.Show(this, "Please select a *.csproj file", "Invalid Visual Studio project file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void buttonBack_Click(object sender, System.EventArgs e)
		{
			NewProjectForm.LoadScreen(typeof(LoadExistingProject));
		}

		private void buttonBrowse_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog fileDialog = new OpenFileDialog();
			fileDialog.Filter = "NHibernate config files (*.cfg.xml)|*.cfg.xml";
			fileDialog.Filter += "|.Net config files (*.config)|*.config";
			fileDialog.Filter += "|All files (*.*)|*.*";
			fileDialog.CheckFileExists = true;
			fileDialog.Multiselect = false;

			if (fileDialog.ShowDialog(this) != DialogResult.OK)
				return;

			textBoxConfigFileLocation.Text = fileDialog.FileName;
		}

		private void textBoxConfigFileLocation_TextChanged(object sender, System.EventArgs e)
		{
			if (string.IsNullOrEmpty(textBoxConfigFileLocation.Text))
			{
				highlighter1.SetHighlightColor(textBoxConfigFileLocation, DevComponents.DotNetBar.Validator.eHighlightColor.None);
				nhConfigFile = null;
				return;
			}
			if (!File.Exists(textBoxConfigFileLocation.Text))
			{
				highlighter1.SetHighlightColor(textBoxConfigFileLocation, DevComponents.DotNetBar.Validator.eHighlightColor.Orange);
				MessageBox.Show(this, "NHibernate config file doesn't exist. Please select a valid file.", "File missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			nhConfigFile = GetNhConfigFile(textBoxConfigFileLocation.Text);

			if (nhConfigFile == null)
			{
				highlighter1.SetHighlightColor(textBoxConfigFileLocation, DevComponents.DotNetBar.Validator.eHighlightColor.Orange);
				MessageBox.Show(this, "This file is not a valid NHibernate config file. It needs to be an XML file with a <hibernate-configuration> element.", "Invalid file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			else
				highlighter1.SetHighlightColor(textBoxConfigFileLocation, DevComponents.DotNetBar.Validator.eHighlightColor.None);
		}

		private NHConfigFile GetNhConfigFile(string filepath)
		{
			nhConfigFile = null;

			if (!File.Exists(filepath))
				return null;

			try
			{
				nhConfigFile = ProjectLoader.GetNhConfigFile(filepath);
			}
			catch
			{
				// Do nothing. We are probably trying to process a non-XML file.
			}
			return nhConfigFile;
		}

		private NHConfigFile GetNhConfigFileFromCsprojFile(string filepath)
		{
			nhConfigFile = null;

			if (!File.Exists(filepath))
				return null;

			try
			{
				// Check whether we can find a config file
				XmlDocument doc = new XmlDocument();
				doc.LoadXml(File.ReadAllText(filepath));
				CSProjFile csProjFile = new CSProjFile(doc, tbProjectLocation.Text);
				Slyce.Common.FileController fileController = new FileController();
				nhConfigFile = ProjectLoader.GetNhConfigFile(csProjFile, fileController);
			}
			catch
			{
				// Do nothing. We are probably trying to process a non-XML file.
			}
			return nhConfigFile;
		}

		private void radioFile_CheckedChanged(object sender, System.EventArgs e)
		{
			ShowConfigControls(radioFile.Checked);
		}

		private void ShowConfigControls(bool showFileSelector)
		{
			if (showFileSelector)
			{
				superTabControl1.Visible = false;
				labelConfigFile.Visible = true;
				textBoxConfigFileLocation.Visible = true;
				buttonBrowse.Visible = true;
				panelConfigSettings.Visible = true;
			}
			else
			{
				superTabControl1.Top = textBoxConfigFileLocation.Top;
				superTabControl1.Visible = true;
				labelConfigFile.Visible = false;
				textBoxConfigFileLocation.Visible = false;
				buttonBrowse.Visible = false;
				panelConfigSettings.Visible = true;
			}
		}

		private void radioManual_CheckedChanged(object sender, System.EventArgs e)
		{
			ShowConfigControls(radioFile.Checked);
		}

		private NHConfigFile CreateConfigFileManually(ArchAngel.Providers.EntityModel.Controller.DatabaseLayer.DatabaseTypes databaseType, string nhConnectionString)
		{
			NHConfigFile nhFile = new NHConfigFile();

			StringBuilder sb = new StringBuilder(1000);
			sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
			sb.AppendLine("<hibernate-configuration xmlns=\"urn:nhibernate-configuration-2.2\">");
			sb.AppendLine("<session-factory>");

			sb.AppendFormat(@"
										<property name=""connection.provider"">NHibernate.Connection.DriverConnectionProvider</property>
										<property name=""dialect"">{0}</property>
										<property name=""connection.driver_class"">{1}</property>
										<property name=""connection.connection_string"">{2}</property>",
						NHConfigFile.GetDialect(false, databaseType),
						NHConfigFile.GetDriver(databaseType),
						nhConnectionString
						);

			if (!string.IsNullOrWhiteSpace(textBoxCacheProviderClass.Text))
				sb.AppendFormat("<property name=\"cache_provider_class\">{0}</property>{1}", textBoxCacheProviderClass.Text, Environment.NewLine);

			if (!string.IsNullOrWhiteSpace(textBoxCacheQueryCacheFactory.Text))
				sb.AppendFormat("<property name=\"cache_query_cache_factory\">{0}</property>{1}", textBoxCacheQueryCacheFactory.Text, Environment.NewLine);

			if (!string.IsNullOrWhiteSpace(textBoxCacheRegionPrefix.Text))
				sb.AppendFormat("<property name=\"cache_region_prefix\">{0}</property>{1}", textBoxCacheRegionPrefix.Text, Environment.NewLine);

			if (!string.IsNullOrWhiteSpace(textBoxMaxFetchDepth.Text))
				sb.AppendFormat("<property name=\"max_fetch_depth\">{0}</property>{1}", textBoxMaxFetchDepth.Text, Environment.NewLine);

			if (!string.IsNullOrWhiteSpace(textBoxQuerySubstitutions.Text))
				sb.AppendFormat("<property name=\"query_substitutions\">{0}</property>{1}", textBoxQuerySubstitutions.Text, Environment.NewLine);

			if (!string.IsNullOrWhiteSpace(textBoxTransactionFactoryClass.Text))
				sb.AppendFormat("<property name=\"transaction_factory_class\">{0}</property>{1}", textBoxTransactionFactoryClass.Text, Environment.NewLine);

			sb.AppendFormat("<property name=\"cache_use_minimal_puts\">{0}</property>{1}", comboBoxCacheUseMinimalPuts.Text, Environment.NewLine);
			sb.AppendFormat("<property name=\"cache_use_query_cache\">{0}</property>{1}", comboBoxCacheUseQueryCache.Text, Environment.NewLine);
			sb.AppendFormat("<property name=\"generate_statistics\">{0}</property>{1}", comboBoxGenerateStatistics.Text, Environment.NewLine);
			sb.AppendFormat("<property name=\"show_sql\">{0}</property>{1}", comboBoxShowSql.Text, Environment.NewLine);
			sb.AppendFormat("<property name=\"use_outer_join\">{0}</property>{1}", comboBoxUseOuterJoin.Text, Environment.NewLine);
			sb.AppendFormat("<property name=\"use_proxy_validator\">{0}</property>{1}", comboBoxUseProxyValidator.Text, Environment.NewLine);

			sb.AppendLine("	</session-factory></hibernate-configuration>");

			nhFile.ConfigXmlFragment = sb.ToString();
			return nhFile;
		}
	}
}
