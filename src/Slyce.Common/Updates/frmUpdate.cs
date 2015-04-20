using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Slyce.Common.Updates
{
	public partial class frmUpdate : Form
	{
		private string PatchFile = "";
		private string FileSize = "";
		private static string CurrentVersion = "";
		private static string LatestVersion = "";
		private string MD5Checksum = "";
		private string TempPath = "";
		private static string UpdatePathsXml;
		public static bool SilentMode = false;
		private static frmUpdate Instance;
		private static string ProductName;
		private bool IsPatch = false;
		private WebClient webClient = new WebClient();

		/// <summary>
		/// To be called from Designer
		/// </summary>
		public frmUpdate()
		{
			InitializeComponent();

			Instance = this;
			panel1.Height = pictureBox1.Height;
			ucHeading1.Text = "";
		}

		/// <summary>
		/// To be called from Workbench.
		/// </summary>
		/// <param name="productName"></param>
		public frmUpdate(string productName)
		{
			InitializeComponent();

			Instance = this;
			panel1.Height = pictureBox1.Height;
			ucHeading1.Text = "";
			ProductName = productName;

			webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
			webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
		}

		/// <summary>
		/// Gets whether an update exists.
		/// </summary>
		/// <returns></returns>
		public static bool UpdateExists(string productName)
		{
			ProductName = productName;
			DeterminePatch();

			if (UpdatePathsXml.IndexOf("<updatepatches") == 0)
			{
				XmlDocument doc = new XmlDocument();
				doc.LoadXml(UpdatePathsXml);
				LatestVersion = doc.SelectSingleNode("updatepatches/@latest_version").InnerText;
				Slyce.Common.VersionNumberUtility.VersionNumberComparer comparer = new Slyce.Common.VersionNumberUtility.VersionNumberComparer();

				if (comparer.Compare(CurrentVersion, LatestVersion) < 0)
					return true;
			}
			return false;
		}

		/// <summary>
		/// Query the webserver for the latest version number, download size and release history.
		/// </summary>
		private static void DeterminePatch()
		{
			CurrentVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
			string serialNumber = "";// License.Status.KeyValueList["serialnumber"] == null ? "none" : License.Status.KeyValueList["serialnumber"].ToString();
			//string productName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name.Replace(" ", "").ToLower();
			UpdatePathsXml = DownloadTextFile(string.Format("http://www.slyce.com/updates/updatecheck.asp?serialnumber={0}&hardwareid={1}&currentversion={2}&productName={3}", serialNumber, "fake", CurrentVersion, ProductName));
		}

		private static string HtmlPageStart
		{
			get
			{
				return @"
				<html>
				<style>
					body {
						font-size:xx-small;
						font-family:Verdana;
						color:DarkBlue;
						margin:0;
					}
				</style>    
				<body>";
			}
		}

		private static string HtmlPageEnd
		{
			get
			{
				return @"</body></html>";
			}
		}

		private static string DownloadTextFile(string url)
		{
			StringBuilder sb = new StringBuilder();
			byte[] buf = new byte[8192];
			HttpWebRequest request;
			HttpWebResponse response = null;

			try
			{
				request = (HttpWebRequest)WebRequest.Create(url);
				// Set default authentication for retrieving the file
				request.Credentials = CredentialCache.DefaultCredentials;
				// Set proxy
				request.Proxy = Slyce.Common.Utility.WebProxy;
				response = (HttpWebResponse)request.GetResponse();

				using (Stream resStream = response.GetResponseStream())
				{
					string tempString = null;
					int count = 0;

					do
					{
						// fill the buffer with data
						count = resStream.Read(buf, 0, buf.Length);

						// make sure we read some data
						if (count != 0)
						{
							// translate from bytes to ASCII text
							tempString = Encoding.ASCII.GetString(buf, 0, count);

							// continue building the string
							sb.Append(tempString);
						}
					}
					while (count > 0); // any more data to read?
				}
				// print out page source
				return sb.ToString();
			}
			catch (WebException)
			{
				return "";
			}
			catch (Exception ex)
			{
				if (!SilentMode)
				{
					if (Instance != null)
						Instance.Cursor = Cursors.Default;

					MessageBox.Show("An unexpected error occurred. Please inform support@slyce.com about this error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				return "";
			}
			finally
			{
				if (response != null) { response.Close(); }
			}
		}

		private void frmUpdate_Paint(object sender, PaintEventArgs e)
		{
			this.BackColor = Slyce.Common.Colors.BackgroundColor;
		}

		private void lnkPatchFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(lnkPatchFile.Text);
		}

		private void lnkPatchFile_SizeChanged(object sender, EventArgs e)
		{
			lblFileSize.Left = lnkPatchFile.Right + 5;
		}

		private string SaveFilePath;

		private void btnViewDetails_Click(object sender, EventArgs e)
		{
			string url = lnkPatchFile.Text;

			SaveFileDialog dialog = new SaveFileDialog();
			dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			dialog.FileName = System.Web.HttpUtility.UrlDecode(PatchFile);

			if (dialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				SaveFilePath = dialog.FileName;

				try
				{
					progressBarDownload.Value = 0;
					progressBarDownload.Visible = true;
					webClient.DownloadFileAsync(new Uri(url), SaveFilePath);
				}
				catch
				{
					Environment.CurrentDirectory = Path.GetDirectoryName(SaveFilePath);
					System.Diagnostics.Process.Start(url);
				}
			}
		}

		private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			progressBarDownload.Value = e.ProgressPercentage;
			progressBarDownload.Text = string.Format("{0}%", e.ProgressPercentage);
		}

		private void Completed(object sender, AsyncCompletedEventArgs e)
		{
			progressBarDownload.Visible = false;

			if (e.Error == null)
			{
				if (MessageBox.Show(this, "Download complete. Exit Visual NHibernate when installing the new version.\n\nInstall now?", "Download complete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
					System.Diagnostics.Process.Start(SaveFilePath);
			}
			else if (!e.Cancelled)
			{
				MessageBox.Show(this, string.Format("An error occurred with the auto-download:\n\n\t{0}\n\nThe download page will now be opened for you.", e.Error.Message), "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

				string url = lnkPatchFile.Text;
				Environment.CurrentDirectory = Path.GetDirectoryName(SaveFilePath);
				System.Diagnostics.Process.Start(url);
			}
		}

		private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			Cursor = Cursors.Default;
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			if (webClient.IsBusy)
			{
				if (MessageBox.Show(this, "Cancel download?", "Cancel download", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
				{
					webClient.CancelAsync();
					progressBarDownload.Visible = false;
					System.Threading.Thread.Sleep(300);
				}
				else
					return;
			}
			this.Close();
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			DeterminePatch();
		}

		private void frmUpdate_Load(object sender, EventArgs e)
		{
			webBrowser1.DocumentText = HtmlPageStart + "Checking for updates..." + HtmlPageEnd;
			webBrowser1.Refresh();
			backgroundWorker1.RunWorkerAsync();
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Error != null)
			{
				// Ensure that we get this reported to us.
				throw e.Error;
			}
			if (UpdatePathsXml.IndexOf("<updatepatches") == 0)
			{
				XmlDocument doc = new XmlDocument();
				doc.LoadXml(UpdatePathsXml);
				LatestVersion = doc.SelectSingleNode("updatepatches/@latest_version").InnerText;
				Slyce.Common.VersionNumberUtility.VersionNumberComparer comparer = new Slyce.Common.VersionNumberUtility.VersionNumberComparer();

				if (comparer.Compare(CurrentVersion, LatestVersion) >= 0)
				{
					webBrowser1.DocumentText = HtmlPageStart + string.Format("<b>You have the latest version: {0}</b>", CurrentVersion) + HtmlPageEnd;

					if (SilentMode)
					{
						this.Close();
					}
				}
				else
				{
					//string productName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name.Replace(" ", "").ToLower();
					webBrowser1.Navigate(string.Format("http://www.slyce.com/updates/archangel_release_notes_view.asp?currentversion={0}&latestversion={1}&productName={2}", CurrentVersion, LatestVersion, ProductName));
					XmlNode node = doc.SelectSingleNode(string.Format(@"updatepatches/patch[@fromVersion='{0}']", CurrentVersion));
					double filesizeInMb = 0;

					if (node == null)
					{
						// No patch update available. Download latest full install instead.
						IsPatch = false;
						PatchFile = doc.SelectSingleNode("updatepatches/@file").InnerText;
						FileSize = doc.SelectSingleNode("updatepatches/@filesize").InnerText;
						filesizeInMb = double.Parse(FileSize) / 1024 / 1024;
						MD5Checksum = doc.SelectSingleNode("updatepatches/@md5").InnerText;
						btnViewDetails.Text = string.Format("Download - {0:0.00}Mb", filesizeInMb);
					}
					else
					{
						IsPatch = true;
						PatchFile = node.SelectSingleNode("@file").InnerText;
						FileSize = node.SelectSingleNode("@filesize").InnerText;
						filesizeInMb = double.Parse(FileSize) / 1024 / 1024;
						MD5Checksum = node.SelectSingleNode("@md5").InnerText;
						btnViewDetails.Text = string.Format("Download patch - {0:0.00}Mb", filesizeInMb);
					}
					TempPath = Path.Combine(Path.GetTempPath(), PatchFile);

					lnkPatchFile.Text = "http://www.slyce.com/downloads/" + PatchFile;
					lblFileSize.Text = "(" + FileSize + ")";
					btnViewDetails.Enabled = true;
				}
			}
			else
			{
				// Error downloading the patch definitions
				webBrowser1.DocumentText = HtmlPageStart + @"There was a problem connecting to the internet. <p/> Click here to check manually in your browser: <strong><a href=""http://www.slyce.com/downloads/"" target=""_blank"">www.slyce.com/downloads/</a><strong>" + HtmlPageEnd;
			}
		}
	}
}
