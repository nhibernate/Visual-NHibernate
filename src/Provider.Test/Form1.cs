using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;

namespace Provider.Test
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

			Database db = new SQLCEDatabaseLoader(new SQLCEDatabaseConnector("2Tables1Relationship.sdf")).LoadDatabase();
			new DatabaseProcessor().CreateRelationships(db);

			editModel.SetDatabase(db);
		}
	}
}
