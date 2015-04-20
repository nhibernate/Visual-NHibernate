using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using SmartAssembly.SmartExceptionsCore;

namespace SmartAssembly.SmartExceptionsWithAdvancedUI
{
	internal class ExceptionReportingForm : System.Windows.Forms.Form
	{
		private UnhandledExceptionHandler unhandledExceptionHandler;
		private ReportExceptionEventArgs reportExceptionEventArgs;
		private Thread workingThread;

		private System.Windows.Forms.CheckBox continueCheckBox;
		private System.Windows.Forms.Label pleaseTellTitle;
		private System.Windows.Forms.Button dontSendReport;
		private System.Windows.Forms.Button sendReport;
		private System.Windows.Forms.Panel panelInformation;
		private System.Windows.Forms.Panel panelSending;
		private System.Windows.Forms.Button cancelSending;
		private SmartAssembly.SmartExceptionsCore.UI.WaitSendingReportControl waitSendingReport;
		private SmartAssembly.SmartExceptionsCore.UI.FeedbackControl preparingFeedback;
		private SmartAssembly.SmartExceptionsCore.UI.FeedbackControl connectingFeedback;
		private SmartAssembly.SmartExceptionsCore.UI.FeedbackControl transferingFeedback;
		private SmartAssembly.SmartExceptionsCore.UI.FeedbackControl completedFeedback;
		private System.Windows.Forms.Button ok;
		private System.Windows.Forms.Button retrySending;
		private System.Windows.Forms.Button debug;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Panel panelEmail;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button continueSendReport;
		private System.Windows.Forms.TextBox email;
		private System.Windows.Forms.Label labelEmail;
		private SmartAssembly.SmartExceptionsCore.UI.AutoHeightLabel errorMessage;
		private TextBox textBoxExtraInfo;
		private Label label2;
		private Button buttonSaveAsFile;
		private Controls.Header header1;
		private Controls.Header header2;
		private Controls.Header header3;
		private System.Windows.Forms.Button saveAsFile;
		internal static bool NoUi = false;

		public ExceptionReportingForm(UnhandledExceptionHandler unhandledExceptionHandler, ReportExceptionEventArgs reportExceptionEventArgs)
			: this()
		{
			int newHeight = Height;

			this.reportExceptionEventArgs = reportExceptionEventArgs;
			this.unhandledExceptionHandler = unhandledExceptionHandler;
			this.errorMessage.Text = reportExceptionEventArgs.Exception.Message;
			this.header1.Text = string.Format("{1} has encountered a problem.{0}We are sorry for the inconvenience.", Environment.NewLine, UnhandledExceptionHandler.ApplicationName);
			this.header2.Text = string.Format("Please wait while {0} sends the report to {1} through the Internet.", UnhandledExceptionHandler.ApplicationName, UnhandledExceptionHandler.CompanyName);
			this.header3.Text = string.Format("Do you want to be contacted by {0} regarding this problem?", UnhandledExceptionHandler.CompanyName);
			this.label3.Text = string.Format("If you want to be contacted by {0} regarding this error, please provide your e-mail address. This information will not be used for any other purpose.", UnhandledExceptionHandler.CompanyName);
			//this.pleaseTellMessage.Text = string.Format("To help improve the software you use, {0} is interested in learning more about this error. We have created a report about the error for you to send to us.", UnhandledExceptionHandler.CompanyName);

			foreach (var key in reportExceptionEventArgs.Exception.Data.Keys)
				reportExceptionEventArgs.AddCustomProperty(string.Format("Data [{0}]", key.ToString()), reportExceptionEventArgs.Exception.Data[key].ToString());

			newHeight += (this.errorMessage.Height - FontHeight);

			if (!reportExceptionEventArgs.CanContinue)
			{
				this.continueCheckBox.Visible = false;
				newHeight -= (this.continueCheckBox.Height);
			}

			if (newHeight > Height) Height = newHeight;

			if (reportExceptionEventArgs.CanDebug)
			{
				unhandledExceptionHandler.DebuggerLaunched += new EventHandler(OnDebuggerLaunched);
				debug.Visible = true;
				//poweredBy.Visible = false;
			}

			if (!reportExceptionEventArgs.CanSendReport)
			{
				sendReport.Enabled = false;
				if (dontSendReport.CanFocus) dontSendReport.Focus();
			}

			this.email.Text = RegistryHelper.ReadHKLMRegistryString("Email");

			unhandledExceptionHandler.SendingReportFeedback += new SendingReportFeedbackEventHandler(OnFeedback);

			// GFH
			//if (WebProxy.Credentials != null)
			unhandledExceptionHandler.SetProxy(WebProxy);

			string reportedExceptions = RegistryHelper.ReadHKLMRegistryString("ReportedExceptions");
			string currentVersion;

			try
			{
				currentVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

				if (!reportedExceptions.StartsWith(currentVersion))
					reportedExceptions = currentVersion + ":";
			}
			catch
			{
				currentVersion = "";
			}
			string newHashCode = GetExceptionHashCode(reportExceptionEventArgs.Exception).ToString();

			// Force reporting of first-time exceptions
			if (reportedExceptions.Contains(newHashCode))
			{
				//labelRequiredInfo.Visible = false;
				dontSendReport.Enabled = true;
				cancelSending.Enabled = true;
				saveAsFile.Enabled = true;
			}
			else
			{
				//labelRequiredInfo.Visible = true;
				//dontSendReport.Enabled = false;
				//cancelSending.Enabled = false;
				//saveAsFile.Enabled = false;

				// Temp: Don't force user to send new message. These 4 lines below are repeated above, and should be removed once we decide what to do going forward.
				//labelRequiredInfo.Visible = false;
				dontSendReport.Enabled = true;
				cancelSending.Enabled = true;
				saveAsFile.Enabled = true;

				RegistryHelper.SaveHKLMRegistryString("ReportedExceptions", reportedExceptions + "," + newHashCode);
			}
			if (NoUi)
			{
				panelInformation.Visible = false;
				continueSendReport_Click(null, null);
			}
		}

		public static IWebProxy WebProxy
		{
			get
			{
				System.Net.IWebProxy proxy = WebRequest.GetSystemWebProxy();
				proxy.Credentials = CredentialCache.DefaultCredentials;
				// TODO: Add checks for Firefox and Chrome proxies if IE proxy isn't valid: http://www.codeguru.com/csharp/csharp/cs_network/http/article.php/c16479
				//if (proxy.Credentials == null)
				//{

				//}
				return proxy;
			}
		}

		private int GetExceptionHashCode(Exception e)
		{
			try
			{
				string s = "";

				try
				{
					s = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
				}
				catch
				{
					// Do nothing
				}
				s += e.Message + e.StackTrace;
				Exception innerException = e.InnerException;

				while (innerException != null)
				{
					s += innerException.StackTrace;
					innerException = innerException.InnerException;
				}
				return s.GetHashCode();
			}
			catch
			{
				return new Random().Next();
			}
		}

		public ExceptionReportingForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.Size = new Size(419, 264);
			this.MinimizeBox = false;
			this.MaximizeBox = false;
			this.panelInformation.Location = Point.Empty;
			this.panelInformation.Dock = DockStyle.Fill;

			this.retrySending.Location = ok.Location;
			this.retrySending.Size = ok.Size;
			this.retrySending.BringToFront();

			this.panelSending.Location = Point.Empty;
			this.panelSending.Dock = DockStyle.Fill;
			this.Text = this.GetConvertedString(this.Text);

			this.panelEmail.Location = Point.Empty;
			this.panelEmail.Dock = DockStyle.Fill;

			foreach (Control control in this.Controls)
			{
				control.Text = this.GetConvertedString(control.Text);
				foreach (Control subControl in control.Controls)
				{
					subControl.Text = this.GetConvertedString(subControl.Text);
				}
			}
			header1.Text = this.GetConvertedString(header1.Text);
			header2.Text = this.GetConvertedString(header2.Text);
			header3.Text = this.GetConvertedString(header3.Text);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panelInformation = new System.Windows.Forms.Panel();
			this.header1 = new SmartAssembly.SmartExceptionsWithAdvancedUI.Controls.Header();
			this.debug = new System.Windows.Forms.Button();
			this.continueCheckBox = new System.Windows.Forms.CheckBox();
			this.pleaseTellTitle = new System.Windows.Forms.Label();
			this.dontSendReport = new System.Windows.Forms.Button();
			this.sendReport = new System.Windows.Forms.Button();
			this.errorMessage = new SmartAssembly.SmartExceptionsCore.UI.AutoHeightLabel();
			this.saveAsFile = new System.Windows.Forms.Button();
			this.panelSending = new System.Windows.Forms.Panel();
			this.header2 = new SmartAssembly.SmartExceptionsWithAdvancedUI.Controls.Header();
			this.buttonSaveAsFile = new System.Windows.Forms.Button();
			this.cancelSending = new System.Windows.Forms.Button();
			this.ok = new System.Windows.Forms.Button();
			this.retrySending = new System.Windows.Forms.Button();
			this.waitSendingReport = new SmartAssembly.SmartExceptionsCore.UI.WaitSendingReportControl();
			this.preparingFeedback = new SmartAssembly.SmartExceptionsCore.UI.FeedbackControl();
			this.connectingFeedback = new SmartAssembly.SmartExceptionsCore.UI.FeedbackControl();
			this.transferingFeedback = new SmartAssembly.SmartExceptionsCore.UI.FeedbackControl();
			this.completedFeedback = new SmartAssembly.SmartExceptionsCore.UI.FeedbackControl();
			this.panelEmail = new System.Windows.Forms.Panel();
			this.header3 = new SmartAssembly.SmartExceptionsWithAdvancedUI.Controls.Header();
			this.email = new System.Windows.Forms.TextBox();
			this.textBoxExtraInfo = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.labelEmail = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.continueSendReport = new System.Windows.Forms.Button();
			this.panelInformation.SuspendLayout();
			this.panelSending.SuspendLayout();
			this.panelEmail.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelInformation
			// 
			this.panelInformation.Controls.Add(this.pleaseTellTitle);
			this.panelInformation.Controls.Add(this.header1);
			this.panelInformation.Controls.Add(this.debug);
			this.panelInformation.Controls.Add(this.continueCheckBox);
			this.panelInformation.Controls.Add(this.dontSendReport);
			this.panelInformation.Controls.Add(this.sendReport);
			this.panelInformation.Controls.Add(this.errorMessage);
			this.panelInformation.Controls.Add(this.saveAsFile);
			this.panelInformation.Location = new System.Drawing.Point(8, 8);
			this.panelInformation.Name = "panelInformation";
			this.panelInformation.Size = new System.Drawing.Size(413, 240);
			this.panelInformation.TabIndex = 0;
			// 
			// header1
			// 
			this.header1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(238)))), ((int)(((byte)(225)))));
			this.header1.Dock = System.Windows.Forms.DockStyle.Top;
			this.header1.Location = new System.Drawing.Point(0, 0);
			this.header1.Name = "header1";
			this.header1.Size = new System.Drawing.Size(413, 58);
			this.header1.TabIndex = 16;
			// 
			// debug
			// 
			this.debug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.debug.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.debug.Location = new System.Drawing.Point(59, 205);
			this.debug.Name = "debug";
			this.debug.Size = new System.Drawing.Size(64, 24);
			this.debug.TabIndex = 13;
			this.debug.Text = "Debug";
			this.debug.Visible = false;
			this.debug.Click += new System.EventHandler(this.debug_Click);
			// 
			// continueCheckBox
			// 
			this.continueCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.continueCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.continueCheckBox.Location = new System.Drawing.Point(19, 174);
			this.continueCheckBox.Name = "continueCheckBox";
			this.continueCheckBox.Size = new System.Drawing.Size(226, 16);
			this.continueCheckBox.TabIndex = 14;
			this.continueCheckBox.Text = "Ignore this error and attempt to &continue.";
			this.continueCheckBox.CheckedChanged += new System.EventHandler(this.continueCheckBox_CheckedChanged);
			// 
			// pleaseTellTitle
			// 
			this.pleaseTellTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pleaseTellTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.pleaseTellTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pleaseTellTitle.Location = new System.Drawing.Point(16, 70);
			this.pleaseTellTitle.Name = "pleaseTellTitle";
			this.pleaseTellTitle.Size = new System.Drawing.Size(388, 16);
			this.pleaseTellTitle.TabIndex = 11;
			this.pleaseTellTitle.Text = "Please report it so that we can fix it for you.";
			// 
			// dontSendReport
			// 
			this.dontSendReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.dontSendReport.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.dontSendReport.Location = new System.Drawing.Point(324, 205);
			this.dontSendReport.Name = "dontSendReport";
			this.dontSendReport.Size = new System.Drawing.Size(75, 24);
			this.dontSendReport.TabIndex = 6;
			this.dontSendReport.Text = "&Don\'t Send";
			this.dontSendReport.Click += new System.EventHandler(this.dontSendReport_Click);
			// 
			// sendReport
			// 
			this.sendReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.sendReport.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.sendReport.Location = new System.Drawing.Point(211, 205);
			this.sendReport.Name = "sendReport";
			this.sendReport.Size = new System.Drawing.Size(105, 24);
			this.sendReport.TabIndex = 9;
			this.sendReport.Text = "&Send Error Report";
			this.sendReport.Click += new System.EventHandler(this.sendReport_Click);
			// 
			// errorMessage
			// 
			this.errorMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.errorMessage.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.errorMessage.Location = new System.Drawing.Point(18, 97);
			this.errorMessage.Name = "errorMessage";
			this.errorMessage.Size = new System.Drawing.Size(381, 13);
			this.errorMessage.TabIndex = 10;
			this.errorMessage.Text = "errorMessage";
			this.errorMessage.UseMnemonic = false;
			// 
			// saveAsFile
			// 
			this.saveAsFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.saveAsFile.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.saveAsFile.Location = new System.Drawing.Point(131, 205);
			this.saveAsFile.Name = "saveAsFile";
			this.saveAsFile.Size = new System.Drawing.Size(72, 24);
			this.saveAsFile.TabIndex = 11;
			this.saveAsFile.Text = "Save as &File";
			this.saveAsFile.Click += new System.EventHandler(this.saveAsFile_Click);
			// 
			// panelSending
			// 
			this.panelSending.Controls.Add(this.header2);
			this.panelSending.Controls.Add(this.buttonSaveAsFile);
			this.panelSending.Controls.Add(this.cancelSending);
			this.panelSending.Controls.Add(this.ok);
			this.panelSending.Controls.Add(this.retrySending);
			this.panelSending.Controls.Add(this.waitSendingReport);
			this.panelSending.Controls.Add(this.preparingFeedback);
			this.panelSending.Controls.Add(this.connectingFeedback);
			this.panelSending.Controls.Add(this.transferingFeedback);
			this.panelSending.Controls.Add(this.completedFeedback);
			this.panelSending.Location = new System.Drawing.Point(8, 264);
			this.panelSending.Name = "panelSending";
			this.panelSending.Size = new System.Drawing.Size(413, 232);
			this.panelSending.TabIndex = 2;
			this.panelSending.Visible = false;
			// 
			// header2
			// 
			this.header2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(238)))), ((int)(((byte)(225)))));
			this.header2.Dock = System.Windows.Forms.DockStyle.Top;
			this.header2.Location = new System.Drawing.Point(0, 0);
			this.header2.Name = "header2";
			this.header2.Size = new System.Drawing.Size(413, 58);
			this.header2.TabIndex = 26;
			// 
			// buttonSaveAsFile
			// 
			this.buttonSaveAsFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSaveAsFile.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonSaveAsFile.Location = new System.Drawing.Point(66, 197);
			this.buttonSaveAsFile.Name = "buttonSaveAsFile";
			this.buttonSaveAsFile.Size = new System.Drawing.Size(72, 24);
			this.buttonSaveAsFile.TabIndex = 25;
			this.buttonSaveAsFile.Text = "Save as &File";
			this.buttonSaveAsFile.Visible = false;
			this.buttonSaveAsFile.Click += new System.EventHandler(this.buttonSaveAsFile_Click);
			// 
			// cancelSending
			// 
			this.cancelSending.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelSending.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cancelSending.Location = new System.Drawing.Point(320, 197);
			this.cancelSending.Name = "cancelSending";
			this.cancelSending.Size = new System.Drawing.Size(80, 24);
			this.cancelSending.TabIndex = 10;
			this.cancelSending.Text = "&Cancel";
			this.cancelSending.Click += new System.EventHandler(this.cancelSending_Click);
			// 
			// ok
			// 
			this.ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ok.Enabled = false;
			this.ok.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.ok.Location = new System.Drawing.Point(232, 197);
			this.ok.Name = "ok";
			this.ok.Size = new System.Drawing.Size(80, 24);
			this.ok.TabIndex = 22;
			this.ok.Text = "&OK";
			this.ok.Click += new System.EventHandler(this.ok_Click);
			// 
			// retrySending
			// 
			this.retrySending.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.retrySending.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.retrySending.Location = new System.Drawing.Point(144, 197);
			this.retrySending.Name = "retrySending";
			this.retrySending.Size = new System.Drawing.Size(80, 24);
			this.retrySending.TabIndex = 23;
			this.retrySending.Text = "&Retry";
			this.retrySending.Visible = false;
			this.retrySending.Click += new System.EventHandler(this.retrySending_Click);
			// 
			// waitSendingReport
			// 
			this.waitSendingReport.Location = new System.Drawing.Point(87, 145);
			this.waitSendingReport.Name = "waitSendingReport";
			this.waitSendingReport.Size = new System.Drawing.Size(250, 42);
			this.waitSendingReport.TabIndex = 11;
			this.waitSendingReport.TabStop = false;
			this.waitSendingReport.Visible = false;
			// 
			// preparingFeedback
			// 
			this.preparingFeedback.Location = new System.Drawing.Point(24, 72);
			this.preparingFeedback.Name = "preparingFeedback";
			this.preparingFeedback.Size = new System.Drawing.Size(368, 16);
			this.preparingFeedback.TabIndex = 18;
			this.preparingFeedback.TabStop = false;
			this.preparingFeedback.Text = "Preparing the error report.";
			// 
			// connectingFeedback
			// 
			this.connectingFeedback.Location = new System.Drawing.Point(24, 96);
			this.connectingFeedback.Name = "connectingFeedback";
			this.connectingFeedback.Size = new System.Drawing.Size(368, 16);
			this.connectingFeedback.TabIndex = 19;
			this.connectingFeedback.TabStop = false;
			this.connectingFeedback.Text = "Connecting to server.";
			// 
			// transferingFeedback
			// 
			this.transferingFeedback.Location = new System.Drawing.Point(24, 120);
			this.transferingFeedback.Name = "transferingFeedback";
			this.transferingFeedback.Size = new System.Drawing.Size(368, 16);
			this.transferingFeedback.TabIndex = 20;
			this.transferingFeedback.TabStop = false;
			this.transferingFeedback.Text = "Transferring report.";
			// 
			// completedFeedback
			// 
			this.completedFeedback.Location = new System.Drawing.Point(24, 144);
			this.completedFeedback.Name = "completedFeedback";
			this.completedFeedback.Size = new System.Drawing.Size(368, 16);
			this.completedFeedback.TabIndex = 21;
			this.completedFeedback.TabStop = false;
			this.completedFeedback.Text = "Error reporting completed. Thank you.";
			// 
			// panelEmail
			// 
			this.panelEmail.Controls.Add(this.header3);
			this.panelEmail.Controls.Add(this.email);
			this.panelEmail.Controls.Add(this.textBoxExtraInfo);
			this.panelEmail.Controls.Add(this.label2);
			this.panelEmail.Controls.Add(this.labelEmail);
			this.panelEmail.Controls.Add(this.label3);
			this.panelEmail.Controls.Add(this.continueSendReport);
			this.panelEmail.Location = new System.Drawing.Point(11, 512);
			this.panelEmail.Name = "panelEmail";
			this.panelEmail.Size = new System.Drawing.Size(413, 232);
			this.panelEmail.TabIndex = 4;
			this.panelEmail.Visible = false;
			// 
			// header3
			// 
			this.header3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(238)))), ((int)(((byte)(225)))));
			this.header3.Dock = System.Windows.Forms.DockStyle.Top;
			this.header3.Location = new System.Drawing.Point(0, 0);
			this.header3.Name = "header3";
			this.header3.Size = new System.Drawing.Size(413, 58);
			this.header3.TabIndex = 27;
			// 
			// email
			// 
			this.email.Location = new System.Drawing.Point(56, 183);
			this.email.Name = "email";
			this.email.Size = new System.Drawing.Size(189, 20);
			this.email.TabIndex = 10;
			this.email.TextChanged += new System.EventHandler(this.email_TextChanged);
			// 
			// textBoxExtraInfo
			// 
			this.textBoxExtraInfo.Location = new System.Drawing.Point(18, 90);
			this.textBoxExtraInfo.Multiline = true;
			this.textBoxExtraInfo.Name = "textBoxExtraInfo";
			this.textBoxExtraInfo.Size = new System.Drawing.Size(379, 38);
			this.textBoxExtraInfo.TabIndex = 14;
			this.textBoxExtraInfo.TextChanged += new System.EventHandler(this.textBoxExtraInfo_TextChanged);
			// 
			// label2
			// 
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label2.Location = new System.Drawing.Point(18, 71);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(262, 16);
			this.label2.TabIndex = 15;
			this.label2.Text = "What were you doing when this happened?";
			// 
			// labelEmail
			// 
			this.labelEmail.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.labelEmail.Location = new System.Drawing.Point(18, 186);
			this.labelEmail.Name = "labelEmail";
			this.labelEmail.Size = new System.Drawing.Size(52, 16);
			this.labelEmail.TabIndex = 9;
			this.labelEmail.Text = "&Email:";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label3.Location = new System.Drawing.Point(20, 143);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(381, 37);
			this.label3.TabIndex = 10;
			this.label3.Text = "If you\'re happy for us to contact you, please provide your e-mail address. This i" +
    "nformation will not be used for any other purpose.";
			// 
			// continueSendReport
			// 
			this.continueSendReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.continueSendReport.Enabled = false;
			this.continueSendReport.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.continueSendReport.Location = new System.Drawing.Point(295, 197);
			this.continueSendReport.Name = "continueSendReport";
			this.continueSendReport.Size = new System.Drawing.Size(105, 24);
			this.continueSendReport.TabIndex = 12;
			this.continueSendReport.Text = "&Send Error Report";
			this.continueSendReport.Click += new System.EventHandler(this.continueSendReport_Click);
			// 
			// ExceptionReportingForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(434, 768);
			this.ControlBox = false;
			this.Controls.Add(this.panelEmail);
			this.Controls.Add(this.panelInformation);
			this.Controls.Add(this.panelSending);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "ExceptionReportingForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "{1fe9e38e-05cc-46a3-ae48-6cda8fb62056}";
			this.TopMost = true;
			this.panelInformation.ResumeLayout(false);
			this.panelSending.ResumeLayout(false);
			this.panelEmail.ResumeLayout(false);
			this.panelEmail.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

		private string GetConvertedString(string s)
		{
			s = s.Replace("%AppName%", UnhandledExceptionHandler.ApplicationName);
			s = s.Replace("%CompanyName%", UnhandledExceptionHandler.CompanyName);
			return s;
		}

		public void SendReport()
		{
			try
			{
				this.panelEmail.Visible = false;
				this.panelSending.Visible = true;
				if (reportExceptionEventArgs != null) StartWorkingThread(new ThreadStart(StartSendReport));
			}
			catch
			{
			}
		}

		private void sendReport_Click(object sender, System.EventArgs e)
		{
			this.panelInformation.Visible = false;
			this.panelEmail.Visible = true;
		}

		private void StartWorkingThread(ThreadStart start)
		{
			workingThread = new Thread(start);
			workingThread.Start();
		}

		private void dontSendReport_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void cancelSending_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (workingThread != null) workingThread.Abort();
			}
			catch
			{
			}
			Close();
		}

		private void ok_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void continueCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			reportExceptionEventArgs.TryToContinue = this.continueCheckBox.Checked;
		}

		private void OnFeedback(object sender, SendingReportFeedbackEventArgs e)
		{
			try
			{
				Invoke(new SendingReportFeedbackEventHandler(Feedback), new object[] { sender, e });
			}
			catch (InvalidOperationException)
			{
			}
		}

		private void OnDebuggerLaunched(object sender, EventArgs e)
		{
			try
			{
				Invoke(new EventHandler(DebuggerLaunched), new object[] { sender, e });
			}
			catch (InvalidOperationException)
			{
			}
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			if (workingThread != null && workingThread.IsAlive)
			{
				workingThread.Abort();
			}
			base.OnClosing(e);
		}

		private void Feedback(object sender, SendingReportFeedbackEventArgs e)
		{
			switch (e.Step)
			{
				case SendingReportStep.PreparingReport:
					if (e.Failed)
					{
						preparingFeedback.Stop(e.ErrorMessage);
						retrySending.Visible = true;
						retrySending.Focus();
						buttonSaveAsFile.Visible = true;
					}
					else
					{
						preparingFeedback.Start();
					}
					break;

				case SendingReportStep.ConnectingToServer:
					if (e.Failed)
					{
						connectingFeedback.Stop(e.ErrorMessage);
						retrySending.Visible = true;
						retrySending.Focus();
						buttonSaveAsFile.Visible = true;
					}
					else
					{
						preparingFeedback.Stop();
						connectingFeedback.Start();
					}
					break;

				case SendingReportStep.Transfering:
					if (e.Failed)
					{
						waitSendingReport.Visible = false;
						transferingFeedback.Stop(e.ErrorMessage);
						retrySending.Visible = true;
						retrySending.Focus();
						buttonSaveAsFile.Visible = true;
					}
					else
					{
						connectingFeedback.Stop();
						transferingFeedback.Start();
						waitSendingReport.Visible = true;
					}
					break;

				case SendingReportStep.Finished:
					waitSendingReport.Visible = false;
					transferingFeedback.Stop();
					completedFeedback.Stop();
					ok.Enabled = true;
					ok.Focus();
					cancelSending.Enabled = false;
					break;
			}
		}

		private void DebuggerLaunched(object sender, EventArgs e)
		{
			Close();
		}

		private void retrySending_Click(object sender, System.EventArgs e)
		{
			retrySending.Visible = false;
			preparingFeedback.Init();
			connectingFeedback.Init();
			transferingFeedback.Init();
			if (reportExceptionEventArgs != null) StartWorkingThread(new ThreadStart(StartSendReport));
		}

		private void StartSendReport()
		{
			reportExceptionEventArgs.SendReport();
		}

		private void debug_Click(object sender, System.EventArgs e)
		{
			if (reportExceptionEventArgs != null) StartWorkingThread(new ThreadStart(reportExceptionEventArgs.LaunchDebugger));
		}

		private void continueSendReport_Click(object sender, System.EventArgs e)
		{
			//if (!NoUi && string.IsNullOrWhiteSpace(email.Text))// || !IsValidEmail(email.Text))
			//{
			//	if (sendAnonymously.Checked)
			//		MessageBox.Show(this, "Valid email address still required even though it won't be sent to us. It will be auto-filled next time you experience an error.", "Email required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			//	else
			//		MessageBox.Show(this, "Valid email address required.", "Email required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			//	return;
			//}
			RegistryHelper.SaveHKLMRegistryString("Email", email.Text);

			if (reportExceptionEventArgs != null)
			{
				//if (sendAnonymously.Checked)
				//	reportExceptionEventArgs.AddCustomProperty("Email", "anon");
				//else
					reportExceptionEventArgs.AddCustomProperty("Email", email.Text);

				if (!string.IsNullOrEmpty(textBoxExtraInfo.Text))
					reportExceptionEventArgs.AddCustomProperty("ExtraInfo", textBoxExtraInfo.Text);

				reportExceptionEventArgs.AddCustomProperty("Email from License", GetEmailFromLicenseFile());
			}
			SendReport();
		}

		private string GetEmailFromLicenseFile()
		{
			string email = "";
			string licenseFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Visual NHibernate");
			licenseFile = Path.Combine(licenseFile, "Visual NHibernate License.SlyceLicense");

			if (File.Exists(licenseFile))
			{
				string xml = "";

				try
				{
					xml = File.ReadAllText(licenseFile);
					XmlDocument doc = new XmlDocument();
					doc.LoadXml(xml);
					XmlNode root = doc.SelectSingleNode("slyce-license");
					XmlNode orderNode = root.SelectSingleNode("order");
					XmlNode purchaserNode = orderNode.SelectSingleNode("purchaser");

					email = purchaserNode.Attributes["email"].Value;
				}
				catch (Exception e)
				{
					email = "GetEmailFromLicenseFile: " + e.Message + Environment.NewLine + "LicenseXML: " + xml;
				}
			}
			return email;
		}

		private void email_TextChanged(object sender, System.EventArgs e)
		{
			//continueSendReport.Enabled = (email.Text.Length > 0 || sendAnonymously.Checked);
			continueSendReport.Enabled = true;
		}

		//private void sendAnonymously_CheckedChanged(object sender, System.EventArgs e)
		//{
		//	if (sendAnonymously.Checked &&
		//		MessageBox.Show(this, string.Format("Please re-consider. Sometimes we really need your help to track down bugs.{0}{0}Note: you still need to enter a valid email address, but it won't attached to the error-report.{0}{0}Do you still want to send anonymously?", Environment.NewLine), "Please help us", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
		//	{
		//		sendAnonymously.Checked = false;
		//		return;
		//	}
		//	//email.Enabled = !sendAnonymously.Checked;
		//	continueSendReport.Enabled = (email.Text.Length > 0 || sendAnonymously.Checked);
		//}

		private void saveAsFile_Click(object sender, System.EventArgs e)
		{
			SaveAsFile();
		}

		private void SaveAsFile()
		{
			SaveFileDialog saveReportDialog = new SaveFileDialog();
			saveReportDialog.DefaultExt = "saencryptedreport";

			saveReportDialog.Filter = "SmartAssembly Exception Report|*.saencryptedreport|All files|*.*";
			saveReportDialog.Title = "Save an Exception Report";

			if (saveReportDialog.ShowDialog(this) != DialogResult.Cancel)
			{
				if (reportExceptionEventArgs.SaveEncryptedReport(saveReportDialog.FileName))
				{
					if (MessageBox.Show(string.Format("Please email this Exception Report file to support@slyce.com\n\nCreate email now?", UnhandledExceptionHandler.CompanyName), UnhandledExceptionHandler.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
					{
						string args = @"mailto:support@slyce.com?subject=Visual NHibernate Error&body=I have attached the error file.";

						try { System.Diagnostics.Process.Start(args); }
						catch
						{
							//Do nothing
						}
					}
					Close();
				}
				else
				{
					MessageBox.Show("Failed to save the report.", UnhandledExceptionHandler.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private bool IsValidEmail(string email)
		{
			return new System.Text.RegularExpressions.Regex(@"[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}").IsMatch(email.Trim().ToUpper());
		}

		private void buttonSaveAsFile_Click(object sender, EventArgs e)
		{
			SaveAsFile();
		}

		private void textBoxExtraInfo_TextChanged(object sender, EventArgs e)
		{

		}
	}
}
