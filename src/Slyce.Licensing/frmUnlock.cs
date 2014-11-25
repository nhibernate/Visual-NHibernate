using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Slyce.Licensing
{
	public partial class frmUnlock : Form
	{
		private string ToolTipTextSerial = "You received your serial number by email when you purchased ArchAngel. \nVisit www.slyce.com/clientcenter to retrieve it again.";
		private int Gap = 10;
		private string HardwareId;

		public frmUnlock()
		{
			InitializeComponent();
            //if (Controller.InDesignMode) { return; }

            EnableDoubleBuffering();
		}

        private void EnableDoubleBuffering()
        {
            // Set the value of the double-buffering style bits to true.
            this.SetStyle(ControlStyles.DoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint,
                true);
            this.UpdateStyles();
        }

		private void Form1_Load(object sender, EventArgs e)
		{
			ReadLicense();
			toolTip1.SetToolTip(picSerialHelp, ToolTipTextSerial);
			toolTip1.SetToolTip(txtSerialNumber, ToolTipTextSerial);
			this.Height = (this.Height - this.ClientSize.Height) + button2.Top + button2.Height + Gap;

			lblWaitMessage.Top = 0;
			lblWaitMessage.Left = 0;
			lblWaitMessage.Width = this.ClientSize.Width;
			lblWaitMessage.Height = this.ClientSize.Height;
			lblWaitMessage.BringToFront();
		}

		private void ReadLicense()
		{
			//lblLicenseStatus.Text = License.Status.Licensed ? "Licensed" : "Not Activated";
			//label2.Text = "License.Status.Evaluation_Lock_Enabled = " + License.Status.Evaluation_Lock_Enabled.ToString();
			//label3.Text = "License.Status.Evaluation_Type = " + License.Status.Evaluation_Type.ToString();
			//label4.Text = "License.Status.Evaluation_Time = " + License.Status.Evaluation_Time.ToString();
			//label5.Text = "License.Status.Evaluation_Time_Current = " + License.Status.Evaluation_Time_Current.ToString();

			//label6.Text = "License.Status.Hardware_Lock_Enabled = " + License.Status.Hardware_Lock_Enabled.ToString();
			//label7.Text = "License.Status.License_Lock_Enabled = " + License.Status.License_Lock_Enabled.ToString();
			//label8.Text = "License.Status.License_Expiration_Date = " + License.Status.License_Expiration_Date.ToString();
			//label10.Text = "License.Status.HardwareID = " + License.Status.HardwareID;

            //this.listView1.Items.Clear();

            //if (License.Status.Licensed)
            //{
            //    for (int i = 0; i < License.Status.KeyValueList.Count; i++)
            //    {
            //        this.listView1.Items.Add(new ListViewItem(new string[] { License.Status.KeyValueList.GetKey(i).ToString(), License.Status.KeyValueList.GetByIndex(i).ToString() }));
            //    }
            //}
		}

		private void btnGetLicense_Click(object sender, EventArgs e)
		{
			if (txtSerialNumber.Text.Length == 0)
			{
				MessageBox.Show("Please enter your serial number in the textbox before retrieving your license.", "Serial Number Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			Cursor = Cursors.WaitCursor;
			lblRequestStatus.Text = "Generating key...";
			lblRequestStatus.Visible = true;
			lblRequestStatus.Refresh();
			lblRequestStatus.Text = "Connecting...";
			lblRequestStatus.Refresh();

            //System.Net.WebResponse response;
            //System.Collections.Specialized.StringDictionary dict = new System.Collections.Specialized.StringDictionary();
            //dict.Add("fromAA_HardwareID", HardwareId);
            //dict.Add("fromAA_Serial", txtSerialNumber.Text);
            //dict.Add("fromAA_User", System.Environment.MachineName);
            //Slyce.Common.Utility.SendHttpPost("http://localhost/SlyceWebsite/Activate/", dict, out response);
            //bool success = retrieveFromURL(response);
            //Cursor = Cursors.Default;

            ////System.Net.WebRequest req = System.Net.WebRequest.Create(@"http://www.slyce.com/LicenseService/autoserve.aspx?fromAA_Serial=" + txtSerialNumber.Text + "&fromAA_HardwareID=" + HardwareId +"&fromAA_User="+ System.Environment.MachineName);
            System.Net.WebRequest req = System.Net.WebRequest.Create(@"http://localhost/SlyceWebsite/Activate?fromAA_Serial=" + txtSerialNumber.Text + "&fromAA_HardwareID=" + HardwareId + "&fromAA_User=" + System.Environment.MachineName);
            req.UseDefaultCredentials = true;
            req.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            //req.Credentials = System.Net.CredentialCache.DefaultCredentials;
            System.Net.WebResponse response = req.GetResponse();
            string errorString = retrieveFromURL(response);

            Cursor = Cursors.Default;
            //bool success = retrieveFromURL(response);

            if (string.IsNullOrEmpty(errorString))
            {
                MessageBox.Show("Successfully activated. \n\nArchAngel Designer will now restart.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RestartApp();
            }
            else
            {
                if (button2.Text == ">>")
                {
                    button2_Click(null, null);
                }
                MessageBox.Show(string.Format("{0}\n\nPlease use an alternative activation method below.", errorString), "Online Activation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
		}

        private void RestartApp()
        {
            try
            {
                using (System.Diagnostics.Process p = new System.Diagnostics.Process())
                {
                    System.Diagnostics.Process.Start(Application.ExecutablePath);
                }
            }
            finally
            {
                Application.Exit();
            }
        }

        private string retrieveFromURL(System.Net.WebResponse response)
		{
            if (response.ContentType != "application/octet-stream")
			{
                StringBuilder sb = new StringBuilder(1000);
                Stream ReceiveStream = response.GetResponseStream();

                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");

                // Pipe the stream to a higher level stream reader with the required encoding format. 
                StreamReader readStream = new StreamReader(ReceiveStream, encode);
                Char[] read = new Char[256];
                int count = readStream.Read(read, 0, 256);

                while (count > 0)
                {
                    // Dump the 256 characters on a string and display the string onto the console.
                    String str = new String(read, 0, count);
                    sb.Append(str);
                    count = readStream.Read(read, 0, 256);
                }
                readStream.Close();

                // Release the resources of response object.
                response.Close(); 
                return sb.ToString();
			}
			lblRequestStatus.Text = "Saving...";
			lblRequestStatus.Refresh();
			string filenameHeader = response.Headers["Content-Disposition"];
			string filename = filenameHeader.Substring(filenameHeader.IndexOf("filename=") + "filename=".Length);
			// 2. Get the Stream Object from the response
			System.IO.Stream responseStream = response.GetResponseStream();
			WriteStreamToFile(responseStream, Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), filename));

			// 3. Create a stream reader and associate it with the stream object
			System.IO.StreamReader reader = new System.IO.StreamReader(responseStream);

			// 4. read the entire stream 
			lblRequestStatus.Text = "Finished";
			return "";
			//return reader.ReadToEnd();
		}

		private string GetHardwareId()
		{
			string hardwareID = "";
			//string hidPath = WriteHIDtoDisk();
#if DEBUG
            string hidPath = @"C:\Projects\SVN\ArchAngel\trunk\Slyce.Licensing\Resources\HID.exe";
#else
            string hidPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "HID.exe");

            if (!File.Exists(hidPath))
            {
                MessageBox.Show("HID.exe cannot be found in the ArchAngel installation folder. Please repair ArchAngel via Control Panel -> Add/Remove Programs.", "File Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return "";
            }
#endif

			using (System.Diagnostics.Process p = new System.Diagnostics.Process())
			{
				p.StartInfo.FileName = hidPath;
				p.StartInfo.UseShellExecute = false;
				p.StartInfo.RedirectStandardOutput = true;
				p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
				p.StartInfo.CreateNoWindow = true;
				p.StartInfo.Arguments = "-q";
				p.Start();
				hardwareID = p.StandardOutput.ReadToEnd();
				p.Close();
			}
            //if (File.Exists(hidPath))
            //{
            //    File.Delete(hidPath);
            //}
			return hardwareID;
		}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns>Path to HID.exe</returns>
        //private string WriteHIDtoDisk()
        //{
        //    string path = Path.Combine(Path.GetTempPath(), "HID.exe");

        //    //if (File.Exists(path))
        //    //{
        //    //    File.Delete(path);
        //    //}
        //    using (Stream input = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Slyce.Licensing.Resources.HID.exe"))
        //    {
        //        using (Stream output = new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.Write))
        //        {
        //            byte[] buffer = new byte[32768];
        //            int read;

        //            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
        //            {
        //                output.Write(buffer, 0, read);
        //            }
        //        }
        //    }
        //    if (!File.Exists(path))
        //    {
        //        throw new FileNotFoundException("HID.exe is missing.");
        //    }
        //    return path;
        //}

        private static void WriteStreamToFile(Stream input, string filePath)
        {
            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                throw new Exception("Directory doesn't exist");
            }
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            using (Stream output = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write, FileShare.Write))
            {
                byte[] buffer = new byte[32768];
                int read;

                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    output.Write(buffer, 0, read);
                }
            }
        }

		private void button1_Click(object sender, EventArgs e)
		{
			if (txtSerialNumber.Text.Length == 0)
			{
				MessageBox.Show("Please enter your serial number.", "Serial Number Missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
            Cursor = Cursors.WaitCursor;
			string toEmail = "sales@slyce.com";
			string subject = "License File Request: " + txtSerialNumber.Text;
			string body = "";// "I would like to purchase ArchAngel. \r\nPlease can you phone me at your earliest as soon as possible. \n\nThe best time to phone me is: I live in the United States. Yours sincerely, XXX";
			string message = string.Format("mailto:{0}?subject={1}&body={2}", toEmail, subject, txtEmailBody.Text);
			System.Diagnostics.Process.Start(message);
            Cursor = Cursors.Default;
		}

		private void lnkManualMethod_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (txtSerialNumber.Text.Length == 0)
			{
				MessageBox.Show("Please enter your serial number.", "Serial Number Missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			System.Diagnostics.Process.Start("http://www.slyce.com/license.aspx?serial=" + txtSerialNumber.Text);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (button2.Text == ">>")
			{
				button2.Text = "<<";
				this.Height = (this.Height - this.ClientSize.Height) + pnlEmail.Top + pnlEmail.Height + Gap;
			}
			else
			{
				button2.Text = ">>";
				this.Height = (this.Height - this.ClientSize.Height) + button2.Top + button2.Height + Gap;
			}
		}

		private void txtSerialNumber_Validated(object sender, EventArgs e)
		{
			txtEmailBody.Text = string.Format("Serial Number: {0}, Hardware ID: {1}", txtSerialNumber.Text, txtHardwareId.Text);
		}

		private void txtSerialNumber_TextChanged(object sender, EventArgs e)
		{
			txtEmailBody.Text = string.Format("Serial Number: {0}, Hardware ID: {1}", txtSerialNumber.Text, HardwareId);
		}

		private void frmUnlock_Shown(object sender, EventArgs e)
		{
			this.Refresh();
			HardwareId = GetHardwareId();
			txtHardwareId.Text = HardwareId;
			txtEmailBody.Text = string.Format("Serial Number: {0}, Hardware ID: {1}", "none", HardwareId);
			lblRequestStatus.Text = "* requires internet connection";
			lblWaitMessage.Visible = false;
		}

        //private void btnGetLicence_Click(object sender, EventArgs e)
        //{
        //    Cursor = Cursors.WaitCursor;
        //    System.Net.WebResponse response;
        //    System.Collections.Specialized.StringDictionary dict = new System.Collections.Specialized.StringDictionary();
        //    dict.Add("HardwareID", HardwareId);
        //    dict.Add("Serial", txtSerialNumber.Text);
        //    dict.Add("FromAA", "true");
        //    Slyce.Common.Utility.SendHttpPost("http://www.slyce.com/Activate/", dict, out response);
        //    Cursor = Cursors.Default;
        //}




	}
}