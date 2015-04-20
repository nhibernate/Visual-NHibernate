using System;
using System.Threading;
using System.Windows.Forms;
using ArchAngel.Interfaces.Wizards.NewProject;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using ArchAngel.Providers.EntityModel.UI.Presenters;

namespace ArchAngel.NHibernateHelper.LoadProjectWizard
{
	public partial class LoadExistingDatabase : UserControl, INewProjectScreen
	{
		private static readonly object databaseLock = new object();
		public IFormNewProject NewProjectForm { get; set; }

		private static readonly string ScreenDataKey = typeof(LoadExistingDatabase).FullName;

		private class ScreenData
		{
			public IDatabaseLoader Loader;

			public ScreenData(IDatabaseLoader loader)
			{
				Loader = loader;
			}
		}

		public LoadExistingDatabase()
		{
			InitializeComponent();

			ucDatabaseInformation1.SelectedDatabaseType = ArchAngel.Providers.EntityModel.SettingsEngine.LastDatabaseTypeUsed;
			ucDatabaseInformation1.DatabaseHelper = new ServerAndDatabaseHelper();
		}

		public void Setup()
		{
			ucDatabaseInformation1.Clear();

			var screenData = NewProjectForm.GetScreenData(ScreenDataKey) as ScreenData;
			if (screenData != null)
			{
				new DatabaseFormFillerFactory()
					.GetFormFillerFor(screenData.Loader.DatabaseConnector)
					.FillForm(ucDatabaseInformation1);
			}
		}

		private void buttonBack_Click(object sender, EventArgs e)
		{
			NewProjectForm.SetScreenData(ScreenDataKey, new ScreenData(DatabasePresenter.CreateDatabaseLoader(ucDatabaseInformation1)));
			NewProjectForm.LoadScreen(typeof(LoadExistingProject));
		}

		private void buttonFinish_Click(object sender, EventArgs e)
		{
			if (ucDatabaseInformation1.ConnectionStringHelper.CurrentDbType == DatabaseTypes.Unknown)
			{
				MessageBox.Show(this, "Please select a database.", "Missing data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			Cursor = Cursors.WaitCursor;

			try
			{
				bool informationFilled = TestConnection(false, ucDatabaseInformation1);

				if (informationFilled == false)
					return;

				NewProjectForm.SetScreenData(ScreenDataKey, new ScreenData(DatabasePresenter.CreateDatabaseLoader(ucDatabaseInformation1)));

				NewProjectForm.UserChosenAction = NewProjectFormActions.NewProject;
				NewProjectForm.NewProjectInformation = new LoadExistingDatabaseInfo
														{
															DatabaseLoader = DatabasePresenter.CreateDatabaseLoader(ucDatabaseInformation1),
															ConnStringHelper = ucDatabaseInformation1.GetHelper()
														};

				NewProjectForm.LoadScreen(typeof(SelectDatabaseObjects));
			}
			finally
			{
				Cursor = Cursors.Default;
			}
		}

		internal static bool TestConnection(bool showSuccess, ArchAngel.Providers.EntityModel.UI.PropertyGrids.ucDatabaseInformation dbInfo)
		{
			Monitor.Enter(databaseLock);

			try
			{
				if (!dbInfo.ReadyToProceed())
					return false;

				dbInfo.SetDatabaseOperationResults(new DatabaseOperationResults("Testing connection...", true));

				IDatabaseLoader loader = DatabasePresenter.CreateDatabaseLoader(dbInfo);

				try
				{
					loader.TestConnection();
					if (showSuccess)
						dbInfo.SetDatabaseOperationResults(new DatabaseOperationResults("Connection Test", true));

					return true;
				}
				catch (DatabaseLoaderException e)
				{
					DatabaseOperationResults results = new DatabaseOperationResults("Connection Test", false, e.ActualMessage);

					dbInfo.SetDatabaseOperationResults(results);

					return false;
				}
			}
			finally
			{
				Monitor.Exit(databaseLock);
			}
		}
	}
}
