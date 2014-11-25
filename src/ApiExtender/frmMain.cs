using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Slyce.Common;

namespace ApiExtender
{
	public partial class frmMain : Form
	{
		internal static frmMain Instance;

		public frmMain()
		{
			InitializeComponent();
			Instance = this;
			Injector.RunningCommandLine = false;

			txtKeyName.Enabled = chkSignAssembly.Checked;

			string currentPath = Assembly.GetEntryAssembly().Location;
			// Gets back to the AA root directory
			currentPath = Directory.GetParent(currentPath).Parent.Parent.Parent.FullName;
			currentPath = currentPath.PathSlash("ArchAngel.Providers.EntityModel");

			txtCsprojFile.Text = currentPath.PathSlash("ArchAngel.Providers.EntityModel.csproj");
			txtDll.Text = currentPath.PathSlash("bin").PathSlash("Debug").PathSlash("ArchAngel.Providers.EntityModel.dll");
			txtKeyName.Text = currentPath.PathSlash("slyce_strong_name_key.snk");
		}

		internal RichTextBox StatusBox { get { return rtfOutput;}  }

		private void button1_Click(object sender, EventArgs e)
		{
			DateTime start = DateTime.Now;

			try
			{
				Cursor = Cursors.WaitCursor;

				ClearLog();
				LogLine("Start Injecting");

				Injector.WriteFunctionBodiesAsXml(txtCsprojFile.Text, txtDll.Text);

				TimeSpan ts = DateTime.Now - start;
				LogLine(string.Format("Finished Processing [{0}.{1} sec]", ts.Seconds, ts.Milliseconds));

				//if (Injector.Run(txtDll.Text, txtCsprojFile.Text))
				//{
				//    TimeSpan ts = DateTime.Now - start;
				//    LogLine(string.Format("Finished Injecting [{0}.{1} sec]", ts.Seconds, ts.Milliseconds));

				//    start = DateTime.Now;

				//    if (chkSignAssembly.Checked)
				//    {
				//        LogLine("Started Signing");

				//        string outputFilename = Injector.GetNewFilename(txtDll.Text);
				//        string standardOutput;
				//        if(Injector.SignAssembly(outputFilename, txtKeyName.Text, out standardOutput))
				//        {
				//            ts = DateTime.Now - start;
				//            LogLine(string.Format("Finished Signing [{0}.{1} sec]", ts.Seconds, ts.Milliseconds));
				//        }
				//        else
				//        {
				//            LogLine("Error occurred While Signing:{0}");
				//            LogLine(standardOutput);
				//            Environment.ExitCode = 1;
				//        }
				//    }
				//}
				//else
				//{
				//    Environment.ExitCode = 1;
				//    TimeSpan ts = DateTime.Now - start;
				//    LogLine(string.Format("Error occurred [{0}.{1} sec]", ts.Seconds, ts.Milliseconds));
				//}
			}
			finally
			{
				Cursor = Cursors.Default;
			}
		}

	    private void ClearLog()
	    {
	        rtfOutput.Clear();
	    }

	    private void LogLine(string format)
	    {
	        rtfOutput.AppendText(format);
            rtfOutput.AppendText(Environment.NewLine);
	        
            rtfOutput.Focus();
	        rtfOutput.SelectionStart = rtfOutput.Text.Length;
	        rtfOutput.SelectionLength = 0;
            rtfOutput.ScrollToCaret(); 
	    }

	    

		private void chkSignAssembly_CheckedChanged(object sender, EventArgs e)
		{
			txtKeyName.Enabled = chkSignAssembly.Checked;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog
            {
         		CheckFileExists = true,
				Filter = "Key File (*.snk)|*.snk"
         	};

			if(ofd.ShowDialog(this) == DialogResult.OK)
			{
				txtKeyName.Text = ofd.FileName;
			}
		}

		private void btnBrowseProjectFile_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog()
			{
				CheckFileExists = true,
				Filter = "Project File (*.csproj)|*.csproj"
			};

			if (ofd.ShowDialog(this) == DialogResult.OK)
			{
				txtCsprojFile.Text = ofd.FileName;
			}
		}

		private void btnBrowseDll_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog()
			{
				CheckFileExists = true,
				Filter = "DLL File (*.dll)|*.dll"
			};

			if (ofd.ShowDialog(this) == DialogResult.OK)
			{
				txtDll.Text = ofd.FileName;
			}
		}


	}
}
