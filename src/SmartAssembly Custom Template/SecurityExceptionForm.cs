using System;
using System.Drawing;
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

using SmartAssembly.SmartExceptionsCore;
using SmartAssembly.SmartExceptionsCore.UI;

namespace SmartAssembly.SmartExceptionsWithAdvancedUI
{
	/// <summary>
	/// Summary description for SecurityExceptionForm.
	/// </summary>
	public class SecurityExceptionForm : System.Windows.Forms.Form
	{
		private SecurityExceptionEventArgs securityExceptionEventArgs = null;
		private System.Windows.Forms.Button continueButton;
		private System.Windows.Forms.Button quitButton;
		private SmartAssembly.SmartExceptionsCore.UI.HeaderControl headerControl1;
		private SmartAssembly.SmartExceptionsCore.UI.AutoHeightLabel errorMessage;
		private SmartAssembly.SmartExceptionsCore.UI.PoweredBy poweredBy;
		private System.ComponentModel.IContainer components;
		
		private string GetConvertedString(string s)
		{
			s = s.Replace("%AppName%", UnhandledExceptionHandler.ApplicationName);
			s = s.Replace("%CompanyName%", UnhandledExceptionHandler.CompanyName);
			return s;
		}

		public SecurityExceptionForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

 			this.Icon = Win32.GetApplicationIcon();
			this.Text = GetConvertedString(this.Text);
			if (this.Text.Length == 0) this.Text = "Security Exception";

			foreach(Control control in this.Controls)
			{
				control.Text = GetConvertedString(control.Text);
				foreach(Control subControl in control.Controls)
				{
					subControl.Text = GetConvertedString(subControl.Text);
				}
			}
		}

		public SecurityExceptionForm(SecurityExceptionEventArgs securityExceptionEventArgs) : this()
		{
			if (securityExceptionEventArgs == null) return;

			if (!securityExceptionEventArgs.CanContinue) this.continueButton.Visible = false;

			this.securityExceptionEventArgs = securityExceptionEventArgs;

			if (securityExceptionEventArgs.SecurityMessage.Length > 0)
			{
				this.errorMessage.Text = securityExceptionEventArgs.SecurityMessage;
			}
			else
			{
				StringBuilder sb = new StringBuilder();
				sb.AppendFormat("{0} attempted to perform an operation not allowed by the security policy. To grant this application the required permission, contact your system administrator, or use the Microsoft .NET Framework Configuration tool.\n\n", UnhandledExceptionHandler.ApplicationName);

				if (securityExceptionEventArgs.CanContinue)
				{
					sb.Append("If you click Continue, the application will ignore this error and attempt to continue. If you click Quit, the application will close immediately.\n\n");
				}

				sb.Append(securityExceptionEventArgs.SecurityException.Message);
				this.errorMessage.Text = GetConvertedString(sb.ToString());
			}

			int newClientHeigth = errorMessage.Bottom + 60;
			if (newClientHeigth > ClientSize.Height) ClientSize = new Size(ClientSize.Width, newClientHeigth);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.quitButton = new System.Windows.Forms.Button();
			this.continueButton = new System.Windows.Forms.Button();
			this.headerControl1 = new SmartAssembly.SmartExceptionsCore.UI.HeaderControl();
			this.errorMessage = new SmartAssembly.SmartExceptionsCore.UI.AutoHeightLabel();
			this.poweredBy = new SmartAssembly.SmartExceptionsCore.UI.PoweredBy();
			this.SuspendLayout();
			// 
			// quitButton
			// 
			this.quitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.quitButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.quitButton.Location = new System.Drawing.Point(308, 188);
			this.quitButton.Name = "quitButton";
			this.quitButton.Size = new System.Drawing.Size(100, 24);
			this.quitButton.TabIndex = 0;
			this.quitButton.Text = "&Quit";
			this.quitButton.Click += new System.EventHandler(this.quitButton_Click);
			// 
			// continueButton
			// 
			this.continueButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.continueButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.continueButton.Location = new System.Drawing.Point(202, 188);
			this.continueButton.Name = "continueButton";
			this.continueButton.Size = new System.Drawing.Size(100, 24);
			this.continueButton.TabIndex = 1;
			this.continueButton.Text = "&Continue";
			this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
			// 
			// headerControl1
			// 
			this.headerControl1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(240)), ((System.Byte)(238)), ((System.Byte)(225)));
			this.headerControl1.Dock = System.Windows.Forms.DockStyle.Top;
			this.headerControl1.IconState = SmartAssembly.SmartExceptionsCore.UI.IconState.Warning;
			this.headerControl1.Image = null;
			this.headerControl1.Location = new System.Drawing.Point(0, 0);
			this.headerControl1.Name = "headerControl1";
			this.headerControl1.Size = new System.Drawing.Size(418, 58);
			this.headerControl1.TabIndex = 7;
			this.headerControl1.TabStop = false;
			this.headerControl1.Text = string.Format("{0} attempted to perform an operation not allowed by the security policy.", UnhandledExceptionHandler.ApplicationName);
			// 
			// errorMessage
			// 
			this.errorMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.errorMessage.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.errorMessage.Location = new System.Drawing.Point(20, 72);
			this.errorMessage.Name = "errorMessage";
			this.errorMessage.Size = new System.Drawing.Size(382, 13);
			this.errorMessage.TabIndex = 14;
			this.errorMessage.Text = "errorMessage";
			this.errorMessage.UseMnemonic = false;
			// 
			// poweredBy
			// 
			this.poweredBy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.poweredBy.Cursor = System.Windows.Forms.Cursors.Hand;
			this.poweredBy.Location = new System.Drawing.Point(6, 186);
			this.poweredBy.Name = "poweredBy";
			this.poweredBy.Size = new System.Drawing.Size(120, 32);
			this.poweredBy.TabIndex = 15;
			this.poweredBy.TabStop = false;
			this.poweredBy.Text = "poweredBy1";
			// 
			// SecurityExceptionForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(418, 224);
			this.ControlBox = false;
			this.Controls.Add(this.continueButton);
			this.Controls.Add(this.quitButton);
			this.Controls.Add(this.headerControl1);
			this.Controls.Add(this.errorMessage);
			this.Controls.Add(this.poweredBy);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SecurityExceptionForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = UnhandledExceptionHandler.ApplicationName;
			this.ResumeLayout(false);

		}
		#endregion

		private void continueButton_Click(object sender, System.EventArgs e)
		{
			if (securityExceptionEventArgs != null) securityExceptionEventArgs.TryToContinue = true;
			Close();
		}

		private void quitButton_Click(object sender, System.EventArgs e)
		{
			if (securityExceptionEventArgs != null) securityExceptionEventArgs.TryToContinue = false;
			Close();
		}
	}
}
