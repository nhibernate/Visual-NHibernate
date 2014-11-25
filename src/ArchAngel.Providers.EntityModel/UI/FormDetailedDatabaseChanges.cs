using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using ArchAngel.Providers.EntityModel.UI.Presenters;

namespace ArchAngel.Providers.EntityModel.UI
{
	public partial class FormDetailedDatabaseChanges : Form
	{
		public FormDetailedDatabaseChanges()
		{
			InitializeComponent();
		}

		public void SetDatabaseChanges(DatabaseMergeResult results)
		{
			var processor = new DBMergeResultProcessor(results);
			rtbChanges.Text = processor.GetTextResults();
		}
	}
}
