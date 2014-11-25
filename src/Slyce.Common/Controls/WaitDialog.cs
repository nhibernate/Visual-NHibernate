using System.Windows.Forms;

namespace Slyce.Common.Controls
{
    public partial class WaitDialog : Form
    {
        public WaitDialog()
        {
            InitializeComponent();
        }

        public WaitDialog(string waitText)
            : this()
        {
            InitializeComponent();
            Message = waitText;
            Show();
        }

        public string Message
        {
            get { return label1.Text; }
            set
            {
                label1.Text = value;
                label1.Refresh();
            }
        }

        private void WaitDialog_Load(object sender, System.EventArgs e)
        {
            label1.Text = "Hello";
        }

        private void label1_Paint(object sender, PaintEventArgs e)
        {
            label1.Text = "kkkk";
        }

    }
}
