using System;
using System.Windows.Forms;

namespace Slyce.Common.ErrorReporting
{
    public partial class frmSendReport : Form
    {
        private string SubmitUrl = "";
        private Exception Ex;
        private string ApplicationName = "";
        private string Version = "";
        public string SubmissionError = "";
        public bool SubmitError = false;
        private string ClipboardText = "";

        public frmSendReport()
        {
            InitializeComponent();
            EnableDoubleBuffering();
#if DEBUG
            btnDebug.Visible = true;
#else
            btnDebug.Visible = false;
#endif
        }

        private void EnableDoubleBuffering()
        {
            // Set the value of the double-buffering style bits to true.
            SetStyle(ControlStyles.DoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint,
                true);
            UpdateStyles();
        }

        internal void Show(string applicationName, string version, string shortDescription, Exception e, string submitUrl)
        {
            SubmitUrl = submitUrl;
            Ex = e;
            ApplicationName = applicationName;
            Version = version;
            lblCompanyMessage.Text = applicationName + " has encountered a problem.";
            txtDescription.Text = shortDescription;
            ShowDialog(ParentForm);
        }

        /// <summary>
        /// See: http://blogs.msdn.com/craigskibo/archive/2004/02/14/73064.aspx
        /// </summary>
        private void CopyToClipboardThreadSafe()
        {
            Clipboard.Clear();
            Clipboard.SetText(ClipboardText);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            btnSend.Enabled = false;
            System.Collections.Specialized.StringDictionary dictionary = new System.Collections.Specialized.StringDictionary();
            dictionary.Add("application", ApplicationName);
            dictionary.Add("version", Version);
            dictionary.Add("error", Ex.Message.Trim());
            dictionary.Add("inner_exception", Ex.InnerException == null ? "" : Ex.InnerException.Message.Trim());
            dictionary.Add("stacktrace", FormatForXml(Ex.StackTrace.Trim()));

            // Temporary error reporting mechanism

            ClipboardText = FormatErrorAsXml(dictionary);
            System.Threading.Thread t = new System.Threading.Thread(CopyToClipboardThreadSafe);
            t.SetApartmentState(System.Threading.ApartmentState.STA);
            t.Start();

            // end temp

            //if (Utility.SendHttpPost(SubmitUrl, dictionary))
            //{
            //    Cursor = Cursors.Default;
            //    lblBoldPleed.Text = "Successfully reported.";
            //    txtDescription.Visible = false;
            //    lblGeneralDescription.Visible = false;
            //    chkIgnore.Visible = false;
            //    btnDontSend.Text = "Close";
            //}
            //else
            //{
            //    Cursor = Cursors.Default;
            //    btnSend.Enabled = true;

            //    if (MessageBox.Show("Message could not be sent. Do you want to copy the message to your clipboard so that you can paste it into an email to support@slyce.com?", "Unsuccessful", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            //    {
            //        ClipboardText = FormatErrorAsXml(dictionary);
            //        System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(CopyToClipboardThreadSafe));
            //        t.SetApartmentState(System.Threading.ApartmentState.STA);
            //        t.Start();
            //    }
            //}
        }

        private static string FormatForXml(string text)
        {
            return text.Replace("\t", "&nbsp;&nbsp;&nbsp;").Replace(Environment.NewLine, "<br/>");
        }

        private static string FormatErrorAsXml(System.Collections.Specialized.StringDictionary dictionary)
        {
            string xml = string.Format(@"<error_report>
                <application>{0}</application>
                <version>{1}</version>
                <exception>{2}</exception>
                <inner_exception>{3}</inner_exception>
                <stacktrace>{4}</stacktrace>
                </error_report>
            ", dictionary["application"], dictionary["version"], dictionary["error"], dictionary["inner_exception"], dictionary["stacktrace"], dictionary["user_message"]);

            return xml;
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
            frmDebugWindow form = new frmDebugWindow(Ex);
            form.ShowDialog(ParentForm);
        }

        private void frmSendReport_Paint(object sender, PaintEventArgs e)
        {
            const int gap = 5;
            btnDontSend.Left = ClientSize.Width - btnDontSend.Width - gap;
            btnSend.Left = btnDontSend.Left - btnSend.Width - gap;
            btnDebug.Left = btnSend.Left - btnDebug.Width - gap;
        }

        private void frmSendReport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.D)
            {
                btnDebug.Visible = true;
            }
        }

    }
}