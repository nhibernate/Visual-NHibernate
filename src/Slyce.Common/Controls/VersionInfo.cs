using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Slyce.Common.Controls
{
	public partial class VersionInfo : UserControl
	{
		public StringBuilder Html = new StringBuilder(2000);
		private bool MoreDetail = false;
		public string ManifestFile;
		private string _currentVersion;
		private List<string> FilesOk = new List<string>();
		private bool IsPopulated = false;

		public VersionInfo()
		{
			InitializeComponent();
			this.BackColor = Color.Transparent;
			webBrowser1.Width = this.ClientSize.Width;
			webBrowser1.Height = this.ClientSize.Height - webBrowser1.Top;
		}

		public static string GetCurrentVersion(string manifestFile)
		{
			string currentVersion = "";

			if (File.Exists(manifestFile))
			{
				XmlDocument doc = new XmlDocument();
				doc.Load(manifestFile);
				currentVersion = doc.SelectSingleNode("root/version").InnerText;
			}
			return currentVersion;
		}

		public string CurrentVersion
		{
			get
			{
				if (!IsPopulated)
				{
					Populate();
				}
				return _currentVersion;
			}
			set
			{
				_currentVersion = value;
			}
		}

		private bool ManifestFileIsMissing
		{
			get { return (!File.Exists(ManifestFile)); }
		}

		private string HtmlPageStart
		{
			get
			{
				return @"
                <html>
                <style>
                    * {
                        font-size:xx-small;
                        font-family:Verdana;
                        color:DarkBlue;
                        margin:0;
                    }
                    h1 {
                        font-size:x-small;
                        font-family:Verdana;
                        color:DarkBlue;
                        margin:0;
                    }
                    h2.corrupt {
                        font-size:xx-small;
                        font-family:Verdana;
                        color:Red;
                        margin:0;
                    }
                    ul {
						list-style-type:disc;
						list-style-position:inside;
					}
                </style>    
                <body>";
			}
		}

		private string HtmlPageEnd
		{
			get { return @"</body></html>"; }
		}

		private void Populate()
		{
			FilesOk.Clear();

			if (ManifestFileIsMissing)
			{
				//DisplayFiles();
				return;
			}
			XmlDocument doc = new XmlDocument();
			doc.Load(ManifestFile);
			CurrentVersion = doc.SelectSingleNode("root/version").InnerText;

			string rootFolder = Path.GetDirectoryName(ManifestFile);
			ProcessFiles(doc, rootFolder);
			ProcessMissingFiles(doc, rootFolder);
			//DisplayFiles();
			IsPopulated = true;
		}

		private void ProcessFiles(XmlDocument doc, string directory)
		{
			string[] files = Directory.GetFiles(directory);

			foreach (string file in files)
			{
				FileInfo fi = new FileInfo(file);
				System.Diagnostics.FileVersionInfo versionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(file);
				string fileVersionString = versionInfo.FileVersion == null ? "" : string.Format(" <i>({0})</i>", versionInfo.FileVersion.Replace(",", "."));
				string text = string.Format("{0}{1}", Path.GetFileName(file), fileVersionString);
				XmlNode fileNode = null;

				try
				{
					fileNode = doc.SelectSingleNode("root/file[translate(@name, 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz' )='" + Path.GetFileName(file).ToLower() + "']");
				}
				catch
				{
					// Do nothing. Probably a filename with a space in it.
				}
				if (fileNode != null)
				{
					if (fileNode.SelectSingleNode("@md5").InnerText != Slyce.Common.Utility.GetCheckSumOfFile(file))
					{
						text += " <span style='color:red;font-weight:bold;'>* corrupt</span>";
						FilesOk.Add(text);
					}
					else
					{
						FilesOk.Add(text);
					}
				}
			}
		}

		private void ProcessMissingFiles(XmlDocument doc, string directory)
		{
			// Look for missing files
			FilesOk.Sort();
			XmlNodeList missingNodes = doc.SelectNodes("root/file/@name");

			foreach (XmlNode missingNode in missingNodes)
			{
				bool found = false;
				string filename = missingNode.InnerText.ToLower();

				foreach (string fileText in FilesOk)
				{
					if (fileText.ToLower().IndexOf(filename) >= 0)
					{
						found = true;
						break;
					}
				}
				if (!found)
				{
					FilesOk.Add(missingNode.InnerText + "<span style='color=red;font-weight:bold;'> * missing</span>");
				}
			}
		}

		public string HtmlBody
		{
			get
			{
				if (!IsPopulated)
				{
					Populate();
				}
				StringBuilder Html = new StringBuilder(1000);

				if (ManifestFileIsMissing)
				{
					Html.Append("Manifest file is missing. Please repair this application using the Control Panel -> Add/Remove Programs");
					Html.Append(HtmlPageEnd);
					return Html.ToString();
				}
				Html.Append("<table width='100%' border='0'>");
				Html.AppendFormat("<tr bgcolor='LightSteelBlue'><td align='center'><h1>ArchAngel {0}</h1></td></tr>", CurrentVersion);
				Html.Append("<tr><td><ul>");

				foreach (string file in FilesOk)
				{
					if (MoreDetail || file.IndexOf("* corrupt") > 0 || file.IndexOf("* missing") > 0)
					{
						Html.AppendFormat("<li>{0}</li>", file);
					}
				}
				Html.Append("</ul></td></tr>");
				Html.AppendFormat("</table>");
				return Html.ToString();
			}
		}

		public bool IsValidInstall
		{
			get
			{
				foreach (string file in FilesOk)
				{
					if (file.IndexOf("* corrupt") > 0 || file.IndexOf("* missing") > 0)
					{
						return false;
					}
				}
				return true;
			}
		}

		public void Display()
		{
			if (!IsPopulated)
			{
				Populate();
			}
			Html = new StringBuilder(1000);
			Html.Append(HtmlPageStart);
			Html.Append(HtmlBody);
			Html.Append(HtmlPageEnd);
			webBrowser1.DocumentText = Html.ToString();
		}

		private void btnDetail_Click(object sender, EventArgs e)
		{
			MoreDetail = !MoreDetail;
			btnDetail.Text = MoreDetail ? "<< Less Detail" : "More Detail >>";
			Display();
		}

		private void VersionInfo_Resize(object sender, EventArgs e)
		{
			webBrowser1.Width = this.ClientSize.Width;
			webBrowser1.Height = this.ClientSize.Height - webBrowser1.Top;
			btnDetail.Left = this.ClientSize.Width - btnDetail.Width - 5;
		}
	}
}
