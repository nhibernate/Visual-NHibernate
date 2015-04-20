using System;
using System.Windows.Forms;

namespace Slyce.Common.ErrorReporting
{
    public partial class frmDebugWindow : Form
    {
        public frmDebugWindow(Exception exceptionToDebug)
        {
            InitializeComponent();

            string message = exceptionToDebug.Message;

            Exception parentException = exceptionToDebug.InnerException;

            if (parentException != null)
            {
                message += Environment.NewLine + Environment.NewLine + "Inner Exceptions:";
            }
            while (parentException != null)
            {
                message += Environment.NewLine + Environment.NewLine + parentException.Message;
                parentException = parentException.InnerException;
            }
            message += Environment.NewLine + Environment.NewLine + "Stack Trace:" + Environment.NewLine + exceptionToDebug.StackTrace;
            textBox1.Text = message;
        }
    }
}