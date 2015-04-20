using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using SmartAssembly.SmartExceptionsCore;

namespace Slyce.Common.ErrorReporting
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
		private System.Windows.Forms.Label pleaseTellMessage;
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
		private SmartAssembly.SmartExceptionsCore.UI.HeaderControl headerControl1;
		private SmartAssembly.SmartExceptionsCore.UI.HeaderControl headerControl2;
		private System.Windows.Forms.Button debug;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Panel panelEmail;
		private System.Windows.Forms.Label label3;
		private SmartAssembly.SmartExceptionsCore.UI.HeaderControl headerControl3;
		private System.Windows.Forms.Button continueSendReport;
		private System.Windows.Forms.TextBox email;
		private System.Windows.Forms.Label labelEmail;
		private System.Windows.Forms.CheckBox sendAnonymously;
		private SmartAssembly.SmartExceptionsCore.UI.AutoHeightLabel errorMessage;
		private SmartAssembly.SmartExceptionsCore.UI.PoweredBy poweredBy;
		private Label label1;
		private TextBox textBoxExtraInfo;
		private Label label2;
		private Label labelRequiredInfo;
		private Button buttonSaveAsFile;
		private System.Windows.Forms.Button saveAsFile;

		public ExceptionReportingForm(UnhandledExceptionHandler unhandledExceptionHandler, ReportExceptionEventArgs reportExceptionEventArgs)
			: this()
		{
			int newHeight = Height;

			this.reportExceptionEventArgs = reportExceptionEventArgs;
			this.unhandledExceptionHandler = unhandledExceptionHandler;
			this.errorMessage.Text = reportExceptionEventArgs.Exception.Message;

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
				poweredBy.Visible = false;
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

			if (reportedExceptions.Contains(newHashCode))
			{
				labelRequiredInfo.Visible = false;
				dontSendReport.Enabled = true;
				cancelSending.Enabled = true;
				saveAsFile.Enabled = true;
			}
			else
			{
				labelRequiredInfo.Visible = true;
				dontSendReport.Enabled = false;
				cancelSending.Enabled = false;
				saveAsFile.Enabled = false;
				RegistryHelper.SaveHKLMRegistryString("ReportedExceptions", reportedExceptions + "," + newHashCode);
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
			this.Text = GetConvertedString(this.Text);

			this.panelEmail.Location = Point.Empty;
			this.panelEmail.Dock = DockStyle.Fill;

			foreach (Control control in this.Controls)
			{
				control.Text = GetConvertedString(control.Text);
				foreach (Control subControl in control.Controls)
				{
					subControl.Text = GetConvertedString(subControl.Text);
				}
			}
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
			this.labelRequiredInfo = new System.Windows.Forms.Label();
			this.debug = new System.Windows.Forms.Button();
			this.continueCheckBox = new System.Windows.Forms.CheckBox();
			this.pleaseTellTitle = new System.Windows.Forms.Label();
			this.dontSendReport = new System.Windows.Forms.Button();
			this.sendReport = new System.Windows.Forms.Button();
			this.pleaseTellMessage = new System.Windows.Forms.Label();
			this.headerControl1 = new SmartAssembly.SmartExceptionsCore.UI.HeaderControl();
			this.errorMessage = new SmartAssembly.SmartExceptionsCore.UI.AutoHeightLabel();
			this.saveAsFile = new System.Windows.Forms.Button();
			this.panelSending = new System.Windows.Forms.Panel();
			this.buttonSaveAsFile = new System.Windows.Forms.Button();
			this.cancelSending = new System.Windows.Forms.Button();
			this.ok = new System.Windows.Forms.Button();
			this.retrySending = new System.Windows.Forms.Button();
			this.waitSendingReport = new SmartAssembly.SmartExceptionsCore.UI.WaitSendingReportControl();
			this.headerControl2 = new SmartAssembly.SmartExceptionsCore.UI.HeaderControl();
			this.preparingFeedback = new SmartAssembly.SmartExceptionsCore.UI.FeedbackControl();
			this.connectingFeedback = new SmartAssembly.SmartExceptionsCore.UI.FeedbackControl();
			this.transferingFeedback = new SmartAssembly.SmartExceptionsCore.UI.FeedbackControl();
			this.completedFeedback = new SmartAssembly.SmartExceptionsCore.UI.FeedbackControl();
			this.panelEmail = new System.Windows.Forms.Panel();
			this.email = new System.Windows.Forms.TextBox();
			this.textBoxExtraInfo = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.labelEmail = new System.Windows.Forms.Label();
			this.sendAnonymously = new System.Windows.Forms.CheckBox();
			this.headerControl3 = new SmartAssembly.SmartExceptionsCore.UI.HeaderControl();
			this.label3 = new System.Windows.Forms.Label();
			this.continueSendReport = new System.Windows.Forms.Button();
			this.poweredBy = new SmartAssembly.SmartExceptionsCore.UI.PoweredBy();
			this.panelInformation.SuspendLayout();
			this.panelSending.SuspendLayout();
			this.panelEmail.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelInformation
			// 
			this.panelInformation.Controls.Add(this.labelRequiredInfo);
			this.panelInformation.Controls.Add(this.debug);
			this.panelInformation.Controls.Add(this.continueCheckBox);
			this.panelInformation.Controls.Add(this.pleaseTellTitle);
			this.panelInformation.Controls.Add(this.dontSendReport);
			this.panelInformation.Controls.Add(this.sendReport);
			this.panelInformation.Controls.Add(this.pleaseTellMessage);
			this.panelInformation.Controls.Add(this.headerControl1);
			this.panelInformation.Controls.Add(this.errorMessage);
			this.panelInformation.Controls.Add(this.saveAsFile);
			this.panelInformation.Location = new System.Drawing.Point(8, 8);
			this.panelInformation.Name = "panelInformation";
			this.panelInformation.Size = new System.Drawing.Size(413, 240);
			this.panelInformation.TabIndex = 0;
			// 
			// labelRequiredInfo
			// 
			this.labelRequiredInfo.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.labelRequiredInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelRequiredInfo.Location = new System.Drawing.Point(124, 180);
			this.labelRequiredInfo.Name = "labelRequiredInfo";
			this.labelRequiredInfo.Size = new System.Drawing.Size(275, 16);
			this.labelRequiredInfo.TabIndex = 15;
			this.labelRequiredInfo.Text = "You are only required to send the first instance of an error.";
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
			this.continueCheckBox.Location = new System.Drawing.Point(22, 99);
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
			this.pleaseTellTitle.Location = new System.Drawing.Point(20, 124);
			this.pleaseTellTitle.Name = "pleaseTellTitle";
			this.pleaseTellTitle.Size = new System.Drawing.Size(381, 16);
			this.pleaseTellTitle.TabIndex = 11;
			this.pleaseTellTitle.Text = "Please tell %CompanyName% about this problem.";
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
			// pleaseTellMessage
			// 
			this.pleaseTellMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pleaseTellMessage.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.pleaseTellMessage.Location = new System.Drawing.Point(20, 140);
			this.pleaseTellMessage.Name = "pleaseTellMessage";
			this.pleaseTellMessage.Size = new System.Drawing.Size(381, 55);
			this.pleaseTellMessage.TabIndex = 12;
			this.pleaseTellMessage.Text = "To help improve the software you use, %CompanyName% is interested in learning mor" +
				"e about this error. We have created a report about the error for you to send to " +
				"us.";
			// 
			// headerControl1
			// 
			this.headerControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(238)))), ((int)(((byte)(225)))));
			this.headerControl1.Dock = System.Windows.Forms.DockStyle.Top;
			this.headerControl1.IconState = SmartAssembly.SmartExceptionsCore.UI.IconState.Error;
			this.headerControl1.Image = null;
			this.headerControl1.Location = new System.Drawing.Point(0, 0);
			this.headerControl1.Name = "headerControl1";
			this.headerControl1.Size = new System.Drawing.Size(413, 58);
			this.headerControl1.TabIndex = 3;
			this.headerControl1.TabStop = false;
			this.headerControl1.Text = "%AppName% has encountered a problem.\nWe are sorry for the inconvenience.";
			// 
			// errorMessage
			// 
			this.errorMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.errorMessage.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.errorMessage.Location = new System.Drawing.Point(20, 69);
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
			this.panelSending.Controls.Add(this.buttonSaveAsFile);
			this.panelSending.Controls.Add(this.cancelSending);
			this.panelSending.Controls.Add(this.ok);
			this.panelSending.Controls.Add(this.retrySending);
			this.panelSending.Controls.Add(this.waitSendingReport);
			this.panelSending.Controls.Add(this.headerControl2);
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
			// headerControl2
			// 
			this.headerControl2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(238)))), ((int)(((byte)(225)))));
			this.headerControl2.Dock = System.Windows.Forms.DockStyle.Top;
			this.headerControl2.IconState = SmartAssembly.SmartExceptionsCore.UI.IconState.Error;
			this.headerControl2.Image = null;
			this.headerControl2.Location = new System.Drawing.Point(0, 0);
			this.headerControl2.Name = "headerControl2";
			this.headerControl2.Size = new System.Drawing.Size(413, 58);
			this.headerControl2.TabIndex = 24;
			this.headerControl2.TabStop = false;
			this.headerControl2.Text = "Please wait while %AppName% sends the report to %CompanyName% through the Internet.";
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
			this.panelEmail.Controls.Add(this.email);
			this.panelEmail.Controls.Add(this.textBoxExtraInfo);
			this.panelEmail.Controls.Add(this.label2);
			this.panelEmail.Controls.Add(this.label1);
			this.panelEmail.Controls.Add(this.labelEmail);
			this.panelEmail.Controls.Add(this.sendAnonymously);
			this.panelEmail.Controls.Add(this.headerControl3);
			this.panelEmail.Controls.Add(this.label3);
			this.panelEmail.Controls.Add(this.continueSendReport);
			this.panelEmail.Location = new System.Drawing.Point(11, 512);
			this.panelEmail.Name = "panelEmail";
			this.panelEmail.Size = new System.Drawing.Size(413, 232);
			this.panelEmail.TabIndex = 4;
			this.panelEmail.Visible = false;
			// 
			// email
			// 
			this.email.Location = new System.Drawing.Point(93, 128);
			this.email.Name = "email";
			this.email.Size = new System.Drawing.Size(248, 20);
			this.email.TabIndex = 10;
			this.email.TextChanged += new System.EventHandler(this.email_TextChanged);
			// 
			// textBoxExtraInfo
			// 
			this.textBoxExtraInfo.Location = new System.Drawing.Point(93, 166);
			this.textBoxExtraInfo.Multiline = true;
			this.textBoxExtraInfo.Name = "textBoxExtraInfo";
			this.textBoxExtraInfo.Size = new System.Drawing.Size(189, 55);
			this.textBoxExtraInfo.TabIndex = 14;
			// 
			// label2
			// 
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label2.Location = new System.Drawing.Point(20, 166);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(67, 16);
			this.label2.TabIndex = 15;
			this.label2.Text = "More details:";
			// 
			// label1
			// 
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label1.ForeColor = System.Drawing.Color.Navy;
			this.label1.Location = new System.Drawing.Point(347, 131);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(54, 16);
			this.label1.TabIndex = 13;
			this.label1.Text = "* required";
			// 
			// labelEmail
			// 
			this.labelEmail.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.labelEmail.Location = new System.Drawing.Point(20, 131);
			this.labelEmail.Name = "labelEmail";
			this.labelEmail.Size = new System.Drawing.Size(100, 16);
			this.labelEmail.TabIndex = 9;
			this.labelEmail.Text = "&Email address:";
			// 
			// sendAnonymously
			// 
			this.sendAnonymously.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.sendAnonymously.Location = new System.Drawing.Point(106, 148);
			this.sendAnonymously.Name = "sendAnonymously";
			this.sendAnonymously.Size = new System.Drawing.Size(115, 16);
			this.sendAnonymously.TabIndex = 11;
			this.sendAnonymously.Text = "Send &anonymously.";
			this.sendAnonymously.CheckedChanged += new System.EventHandler(this.sendAnonymously_CheckedChanged);
			// 
			// headerControl3
			// 
			this.headerControl3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(238)))), ((int)(((byte)(225)))));
			this.headerControl3.Dock = System.Windows.Forms.DockStyle.Top;
			this.headerControl3.IconState = SmartAssembly.SmartExceptionsCore.UI.IconState.Error;
			this.headerControl3.Image = null;
			this.headerControl3.Location = new System.Drawing.Point(0, 0);
			this.headerControl3.Name = "headerControl3";
			this.headerControl3.Size = new System.Drawing.Size(413, 58);
			this.headerControl3.TabIndex = 3;
			this.headerControl3.TabStop = false;
			this.headerControl3.Text = "Do you want to be contacted by %CompanyName% regarding this problem?";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label3.Location = new System.Drawing.Point(20, 69);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(381, 43);
			this.label3.TabIndex = 10;
			this.label3.Text = "If you want to be contacted by %CompanyName% regarding this error, please provide" +
				" your e-mail address. This information will not be used for any other purpose.";
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
			// poweredBy
			// 
			this.poweredBy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.poweredBy.Cursor = System.Windows.Forms.Cursors.Hand;
			this.poweredBy.Location = new System.Drawing.Point(6, 730);
			this.poweredBy.Name = "poweredBy";
			this.poweredBy.Size = new System.Drawing.Size(112, 32);
			this.poweredBy.TabIndex = 5;
			this.poweredBy.TabStop = false;
			this.poweredBy.Text = "poweredBy1";
			this.poweredBy.Visible = false;
			// 
			// ExceptionReportingForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(434, 768);
			this.ControlBox = false;
			this.Controls.Add(this.poweredBy);
			this.Controls.Add(this.panelEmail);
			this.Controls.Add(this.panelInformation);
			this.Controls.Add(this.panelSending);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "ExceptionReportingForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "%AppName%";
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
			if (string.IsNullOrWhiteSpace(email.Text))// || !IsValidEmail(email.Text))
			{
				if (sendAnonymously.Checked)
					MessageBox.Show(this, "Valid email address still required even though it won't be sent to us. It will be auto-filled next time you experience an error.", "Email required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				else
					MessageBox.Show(this, "Valid email address required.", "Email required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			RegistryHelper.SaveHKLMRegistryString("Email", email.Text);

			if (reportExceptionEventArgs != null)
			{
				if (sendAnonymously.Checked)
					reportExceptionEventArgs.AddCustomProperty("Email", "anon");
				else
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
			continueSendReport.Enabled = (email.Text.Length > 0 || sendAnonymously.Checked);
		}

		private void sendAnonymously_CheckedChanged(object sender, System.EventArgs e)
		{
			if (sendAnonymously.Checked &&
				MessageBox.Show(this, string.Format("Please re-consider. Sometimes we really need your help to track down bugs.{0}{0}Note: you still need to enter a valid email address, but it won't attached to the error-report.{0}{0}Do you still want to send anonymously?", Environment.NewLine), "Please help us", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
			{
				sendAnonymously.Checked = false;
				return;
			}
			//email.Enabled = !sendAnonymously.Checked;
			continueSendReport.Enabled = (email.Text.Length > 0 || sendAnonymously.Checked);
		}

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
					if (MessageBox.Show(string.Format("Please email this Exception Report file to support@slyce.com/nCreate email now?", UnhandledExceptionHandler.CompanyName), UnhandledExceptionHandler.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
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
	}
}
