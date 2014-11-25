using System.Windows.Forms;
using ArchAngel.Providers.EntityModel.Controller.DatabaseLayer;
using ArchAngel.Providers.EntityModel.UI.Presenters;

namespace ArchAngel.Providers.EntityModel.UI
{
    public partial class LoadDatabaseForm : Form
    {
        public LoadDatabaseForm()
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
            ucDatabaseInformation1.DatabaseHelper = new ServerAndDatabaseHelper();
        }

        public string MessageText
        {
            get { return labelX1.Text; }
            set { labelX1.Text = value; }
        }

        public IDatabaseLoader DatabaseLoader
        {
            get { return DatabasePresenter.CreateDatabaseLoader(ucDatabaseInformation1); }
            set
            {
                if (value == null) return;

                new DatabaseFormFillerFactory()
                    .GetFormFillerFor(value.DatabaseConnector)
                    .FillForm(ucDatabaseInformation1);
            }
        }

        public void FillFrom(IDatabaseConnector information)
        {
            new DatabaseFormFillerFactory()
                .GetFormFillerFor(information)
                .FillForm(ucDatabaseInformation1);
        }

        private void buttonOk_Click(object sender, System.EventArgs e)
        {
            try
            {
                DatabaseLoader.TestConnection();

            }
            catch (DatabaseLoaderException ex)
            {
                MessageBox.Show(this, ex.ActualMessage, "Connection failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void panelEx2_Click(object sender, System.EventArgs e)
        {

        }

    }
}
