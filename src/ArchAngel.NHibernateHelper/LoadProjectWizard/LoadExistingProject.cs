using System;
using System.Drawing;
using System.Windows.Forms;
using ArchAngel.Interfaces.Wizards.NewProject;

namespace ArchAngel.NHibernateHelper.LoadProjectWizard
{
	public partial class LoadExistingProject : UserControl, INewProjectScreen
	{
		private static readonly string ScreenDataKey = typeof(LoadExistingProject).FullName;

		private class ScreenData
		{
			public SelectedOption SelectedOpt;
			public string ExistingFilename;
		}

		private enum SelectedOption { Blank, UseExistingProject, UseExistingDatabase }

		public IFormNewProject NewProjectForm { get; set; }

		public LoadExistingProject()
		{
			InitializeComponent();

			SetStyle(
		  ControlStyles.UserPaint |
		  ControlStyles.AllPaintingInWmPaint |
		  ControlStyles.OptimizedDoubleBuffer, true);

			rbStartNewProject.Checked = true;

			HidePanel(panelBlank, labelBlank);
			HidePanel(panelDatabase, labelDatabase);
			HidePanel(panelExistingProject, labelExistingProject);
		}

		private void buttonCreateProject_Click(object sender, EventArgs e)
		{
			NewProjectForm.UserChosenAction = NewProjectFormActions.NewProject;

			if (rbUseExistingProject.Checked)
			{
				NewProjectForm.SetScreenData(ScreenDataKey,
						new ScreenData { SelectedOpt = SelectedOption.UseExistingProject });
				NewProjectForm.LoadScreen(typeof(SetNhConfig));
			}
			else if (rbLoadFromDatabase.Checked)
			{
				NewProjectForm.SetScreenData(ScreenDataKey,
						new ScreenData { SelectedOpt = SelectedOption.UseExistingDatabase });
				NewProjectForm.LoadScreen(typeof(LoadExistingDatabase));

			}
			else if (rbStartNewProject.Checked)
			{
				NewProjectForm.SetScreenData(ScreenDataKey,
						new ScreenData { SelectedOpt = SelectedOption.Blank });

				NewProjectForm.Finish();
			}
		}

		private void btnBack_Click(object sender, EventArgs e)
		{
			NewProjectForm.LoadScreen("Screen1");
		}

		public void Setup()
		{
			if (this.NewProjectForm.SetupAction == NewProjectFormActions.NewProject)
				btnBack.Visible = false;

			var screenData = NewProjectForm.GetScreenData(ScreenDataKey) as ScreenData;

			if (screenData == null) return;

			switch (screenData.SelectedOpt)
			{
				case SelectedOption.Blank:
					rbStartNewProject.Checked = true;
					break;
				case SelectedOption.UseExistingProject:
					rbUseExistingProject.Checked = true;
					break;
				case SelectedOption.UseExistingDatabase:
					rbLoadFromDatabase.Checked = true;
					break;
			}
		}

		private void rbLoadFromDatabase_Click(object sender, EventArgs e)
		{
			rbUseExistingProject.Checked = false;

			buttonCreateProject.Text = "Next >";
		}

		private void rbStartNewProject_CheckedChanged(object sender, EventArgs e)
		{
			if (rbStartNewProject.Checked || rbLoadFromDatabase.Checked)
			{
				rbUseExistingProject.Checked = false;
			}

			if (rbStartNewProject.Checked)
			{
				buttonCreateProject.Text = "Finish and Create Project";
			}
		}

		private void rbUseExistingProject_CheckedChanged(object sender, EventArgs e)
		{
			if (rbUseExistingProject.Checked)
			{
				rbStartNewProject.Checked = false;
				rbLoadFromDatabase.Checked = false;
				buttonCreateProject.Text = "Finish and Create Project";
			}
		}

		private void buttonExistingDatabase_Click(object sender, EventArgs e)
		{
			NewProjectForm.SetScreenData(ScreenDataKey, new ScreenData { SelectedOpt = SelectedOption.UseExistingDatabase });
			NewProjectForm.UserChosenAction = NewProjectFormActions.NewProject;
			NewProjectForm.LoadScreen(typeof(LoadExistingDatabase));
		}

		private void btnNewBlankProject_Click(object sender, EventArgs e)
		{
			NewProjectForm.SetScreenData(ScreenDataKey, new ScreenData { SelectedOpt = SelectedOption.Blank });
			// Skip the Database screen.
			NewProjectForm.SkipScreens(3);
			NewProjectForm.UserChosenAction = NewProjectFormActions.NewProject;
			NewProjectForm.Finish();
		}

		private void buttonVisualStudioProject_Click(object sender, EventArgs e)
		{
			NewProjectForm.SetScreenData(ScreenDataKey, new ScreenData { SelectedOpt = SelectedOption.UseExistingProject });
			NewProjectForm.SkipScreens(3);
			NewProjectForm.UserChosenAction = NewProjectFormActions.ExistingProject;
			NewProjectForm.LoadScreen(typeof(SetNhConfig));
		}

		private void panelBlank_MouseEnter(object sender, EventArgs e)
		{
			ShowPanel(panelBlank, labelBlank);
		}

		private void panelBlank_MouseLeave(object sender, EventArgs e)
		{
			HidePanel(panelBlank, labelBlank);
		}

		private void panelDatabase_MouseEnter(object sender, EventArgs e)
		{
			ShowPanel(panelDatabase, labelDatabase);
		}

		private void panelDatabase_MouseLeave(object sender, EventArgs e)
		{
			HidePanel(panelDatabase, labelDatabase);
		}

		private void panelExistingProject_MouseEnter(object sender, EventArgs e)
		{
			ShowPanel(panelExistingProject, labelExistingProject);
		}

		private void panelExistingProject_MouseLeave(object sender, EventArgs e)
		{
			HidePanel(panelExistingProject, labelExistingProject);
		}

		private void ShowPanel(DevComponents.DotNetBar.PanelEx panel, Label label)
		{
			panel.Style.BackColor1.Color = Color.FromArgb(150, 150, 150);
			panel.Style.BackColor2.Color = Color.FromArgb(90, 90, 90);
			label.Visible = true;
		}

		private void HidePanel(DevComponents.DotNetBar.PanelEx panel, Label label)
		{
			panel.Style.BackColor1.Color = this.BackColor;
			panel.Style.BackColor2.Color = this.BackColor;
			label.Visible = false;
		}

		private void btnNewBlankProject_MouseEnter(object sender, EventArgs e)
		{
			ShowPanel(panelBlank, labelBlank);
		}

		private void buttonExistingDatabase_MouseEnter(object sender, EventArgs e)
		{
			ShowPanel(panelDatabase, labelDatabase);
		}

		private void buttonVisualStudioProject_MouseEnter(object sender, EventArgs e)
		{
			ShowPanel(panelExistingProject, labelExistingProject);
		}
	}
}