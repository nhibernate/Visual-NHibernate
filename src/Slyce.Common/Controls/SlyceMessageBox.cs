using System;
using System.Threading;
using System.Windows.Forms;

namespace Slyce.Common.Controls
{
	/// <summary>
	/// Custom message box for showing the user messages in a non-blocking fashion.
	/// </summary>
	public partial class SlyceMessageBox : Form
	{
		private readonly ManualResetEvent optionChosen = new ManualResetEvent(false);

		/// <summary>
		/// Triggered after the User has made a choice.
		/// </summary>
		public event EventHandler<ResultAvailableArgs> ResultAvailable;

		/// <summary>
		/// Create a new message box, but don't show it to the user yet.
		/// </summary>
		public SlyceMessageBox()
		{
			InitializeComponent();
		}

		/// <summary>
		/// The caption shown in the title bar of the message box.
		/// </summary>
		public string Caption
		{
			get
			{
				return Text;
			}
			set
			{
				Text = value;
			}
		}

		/// <summary>
		/// The message shown in the main part of the mesage box.
		/// </summary>
		public string Message
		{
			get
			{
				return MessageTextBox.Text;
			}
			set
			{
				MessageTextBox.Text = value;
			}
		}

		/// <summary>
		/// This WaitHandle is triggered when the user has chosen yes or no.
		/// </summary>
		public WaitHandle OptionChosen
		{
			get { return optionChosen; }
		}

		private void YesButton_Click(object sender, EventArgs e)
		{
			DialogResult = System.Windows.Forms.DialogResult.Yes;
			TriggerResultAvailable();
		}

		private void NoButton_Click(object sender, EventArgs e)
		{
			DialogResult = System.Windows.Forms.DialogResult.No;
			TriggerResultAvailable();
		}

		/// <summary>
		/// Set the WaitHandle, trigger the ResultAvailable event if needed, then close the form.
		/// </summary>
		private void TriggerResultAvailable()
		{
			optionChosen.Set();
			if(ResultAvailable != null)
			{
				ResultAvailable(this, new ResultAvailableArgs(DialogResult));
			}
			Close();
		}
	}
	/// <summary>
	/// Holds the User's message box choice.
	/// </summary>
	public class ResultAvailableArgs : EventArgs
	{
		private readonly DialogResult result;

		public ResultAvailableArgs(DialogResult result)
		{
			this.result = result;
		}

		public DialogResult Result
		{
			get { return result; }
		}
	}
}
