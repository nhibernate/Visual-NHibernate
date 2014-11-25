using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ArchAngel.Interfaces.Wizards.NewProject;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;

namespace ArchAngel.NHibernateHelper.LoadProjectWizard
{
	public partial class Prefixes : UserControl, INewProjectScreen
	{
		public Prefixes()
		{
			InitializeComponent();
		}

		public IFormNewProject NewProjectForm { get; set; }

		public void Setup()
		{
			LoadExistingDatabaseInfo info = (LoadExistingDatabaseInfo)NewProjectForm.NewProjectInformation;
			//List<SchemaData> schemas = info.DatabaseLoader.GetSchemaObjects();

			formPrefixes1.Fill(null);
			formPrefixes1.TablePrefixes = info.TablePrefixes;
			formPrefixes1.ColumnPrefixes = info.ColumnPrefixes;
			formPrefixes1.TableSuffixes = info.TableSuffixes;
			formPrefixes1.ColumnSuffixes = info.ColumnSuffixes;
			label1.Focus();
		}

		private void buttonBack_Click(object sender, EventArgs e)
		{
			//object xxx = null;
			//NewProjectForm.SetScreenData(ScreenDataKey, xxx);
			formPrefixes1.FinaliseEdits();
			LoadExistingDatabaseInfo info = (LoadExistingDatabaseInfo)NewProjectForm.NewProjectInformation;
			info.TablePrefixes = formPrefixes1.TablePrefixes;
			info.ColumnPrefixes = formPrefixes1.ColumnPrefixes;
			info.TableSuffixes = formPrefixes1.TableSuffixes;
			info.ColumnSuffixes = formPrefixes1.ColumnSuffixes;
			NewProjectForm.LoadScreen(typeof(SelectDatabaseObjects));
		}

		private void buttonFinish_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;

			try
			{
				formPrefixes1.FinaliseEdits();
				LoadExistingDatabaseInfo info = (LoadExistingDatabaseInfo)NewProjectForm.NewProjectInformation;
				info.TablePrefixes = formPrefixes1.TablePrefixes;
				info.ColumnPrefixes = formPrefixes1.ColumnPrefixes;
				info.TableSuffixes = formPrefixes1.TableSuffixes;
				info.ColumnSuffixes = formPrefixes1.ColumnSuffixes;
				List<SchemaData> schemas = new List<SchemaData>();
				NewProjectForm.Finish();
			}
			finally
			{
				Cursor = Cursors.Default;
			}
		}

	}
}
