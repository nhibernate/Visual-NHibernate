using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;

namespace ArchAngel.Providers.EntityModel.UI.Editors.UserControls
{
	public partial class DatabaseSynchroEditor : Form
	{
		public DatabaseSynchroEditor()
		{
			InitializeComponent();
		}

		public void Fill(DatabaseMergeResult results, IDatabase db1, IDatabase db2)
		{
			databaseChanges1.Fill(results, db1, db2);
		}
	}
}
