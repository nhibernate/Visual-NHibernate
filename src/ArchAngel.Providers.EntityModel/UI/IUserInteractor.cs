using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;

namespace ArchAngel.Providers.EntityModel.UI
{
	public interface IUserInteractor
	{
		void ShowError(string shortDescription, string longDescription);
		void ShowWaitScreen(string caption);
		void RemoveWaitScreen();
		void ShowDialog(Form form);
		void UpdateWaitScreen(string message);
		IDatabaseLoader GetDatabaseLoader(IDatabaseConnector existingInformation);
	}

	public class NullUserInteractor : IUserInteractor
	{
		public void ShowError(string shortDescription, string longDescription)
		{
		}

		public void ShowWaitScreen(string caption)
		{
		}

		public void RemoveWaitScreen()
		{
		}

		public void ShowDialog(Form form)
		{
		}

		public void UpdateWaitScreen(string message)
		{
		}

		public IDatabaseLoader GetDatabaseLoader(IDatabaseConnector existingInformation)
		{
			return null;
		}
	}
}