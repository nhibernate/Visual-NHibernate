using System;
using System.IO;
using System.Net;
using System.Text;

namespace Slyce.Common
{
	public class FtpUtility
	{
		public string Username;
		public string Password;

		public FtpUtility(string username, string password)
		{
			Username = username;
			Password = password;
		}

		private FtpWebRequest GetFtpRequestObject(string encodedUrl)
		{
			return GetFtpRequestObject(encodedUrl, null);
		}

		private FtpWebRequest GetFtpRequestObject(string url, FileInfo fileInf)
		{
			string encodedUrl = url.Replace(" ", "%20");

			FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(encodedUrl));
			// Required if a proxy exists, otherwise get error: "The requested FTP command is not supported when using HTTP proxy"
			reqFTP.Proxy = Slyce.Common.Utility.WebProxy;
			// Provide the WebPermission Credintials
			reqFTP.Credentials = new NetworkCredential(Username, Password);
			// By default KeepAlive is true, where the control connection is 
			// not closed after a command is executed.
			reqFTP.KeepAlive = false;
			// Specify the command to be executed.
			reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
			// Specify the data transfer type.
			reqFTP.UseBinary = true;

			if (fileInf != null)
			{
				// Notify the server about the size of the uploaded file
				reqFTP.ContentLength = fileInf.Length;
			}
			reqFTP.UsePassive = false;
			reqFTP.Timeout = 50000;
			return reqFTP;
		}

		public bool Upload(string filename, string ftpUrl, bool throwExceptionOnFailure)
		{
			long uploadedSize = 0;
			FileInfo fileInf = new FileInfo(filename);

			// Create FtpWebRequest object from the Uri provided
			string url = "ftp://" + ftpUrl + "/" + fileInf.Name;

			FtpWebRequest reqFTP = GetFtpRequestObject(url, fileInf);

			// The buffer size is set to 2kb
			const int buffLength = 2048;
			byte[] buff = new byte[buffLength];

			// Opens a file stream (System.IO.FileStream) to read the file to be uploaded
			using (FileStream fs = fileInf.OpenRead())
			{
				try
				{
					// Stream to which the file to be upload is written
					Stream strm = null;

					try
					{
						strm = reqFTP.GetRequestStream();
					}
					catch (WebException ex)
					{
						String status = ((FtpWebResponse)ex.Response).StatusDescription;

						if (status.IndexOf("550") == 0)
						{
							if (!MakeDirectory(ftpUrl, true))
							{
								throw;
							}
							reqFTP = GetFtpRequestObject(url, fileInf);
							strm = reqFTP.GetRequestStream();
						}
					}
					// Read from the file stream 2kb at a time
					int contentLen = fs.Read(buff, 0, buffLength);

					// Till Stream content ends
					while (contentLen != 0)
					{
						// Write Content from the file stream to the 
						// FTP Upload Stream
						strm.Write(buff, 0, contentLen);
						contentLen = fs.Read(buff, 0, buffLength);

						uploadedSize += contentLen;
						//lblStatus.Text = originalText + string.Format("{0} of {1} kb", uploadedSize / 1024, fileSize / 1024);
						//Application.DoEvents();
					}
					// Close the file stream and the Request Stream
					strm.Flush();

					try
					{
						// We are probably going to get an exception, so let's reduce the timeout to make it fail quicker
						//reqFTP.Timeout = 1000;
						strm.Dispose();
					}
					catch (WebException ex)
					{
						if (ex.Message.IndexOf("The underlying connection was closed") >= 0)
						{
							// Do nothing
						}
						else
						{
							if (throwExceptionOnFailure)
							{
								throw;
							}
							return false;
						}
					}
					//strm.Close();
					fs.Close();
					return true;
				}
				catch (WebException ex)
				{
					string status = ((FtpWebResponse)ex.Response).StatusDescription;

					if (throwExceptionOnFailure)
					{
						throw new Exception("FTP upload error: " + status);
					}
					return false;

				}
				catch (Exception)
				{
					if (throwExceptionOnFailure)
					{
						throw;
					}
					return false;
				}
			}
		}

		public bool DeleteFile(string fileName, string ftpUrl, bool throwOnException)
		{
			try
			{
				FileInfo fi = new FileInfo(fileName);
				FileInfo fileInf = new FileInfo(fileName);

				// Create FtpWebRequest object from the Uri provided
				string url = "ftp://" + ftpUrl + "/" + fileInf.Name;
				FtpWebRequest reqFTP = GetFtpRequestObject(url, fileInf);
				reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;

				FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
				Stream datastream = response.GetResponseStream();
				StreamReader sr = new StreamReader(datastream);
				sr.Close();
				datastream.Close();
				response.Close();
				return true;
			}
			catch (Exception)
			{
				if (throwOnException)
				{
					throw;
				}
				return false;
			}
		}

		public string[] GetFileList(string ftpUrl, bool throwOnException)
		{
			StringBuilder result = new StringBuilder();

			try
			{
				//long uploadedSize = 0;

				// Create FtpWebRequest object from the Uri provided
				string url = "ftp://" + ftpUrl + "/";
				FtpWebRequest reqFTP = GetFtpRequestObject(url);

				reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
				WebResponse response = reqFTP.GetResponse();
				StreamReader reader = new StreamReader(response.GetResponseStream());
				//MessageBox.Show(reader.ReadToEnd());
				string line = reader.ReadLine();

				while (line != null)
				{
					result.Append(line);
					result.Append("\n");
					line = reader.ReadLine();
				}
				result.Remove(result.ToString().LastIndexOf('\n'), 1);
				reader.Close();
				response.Close();
				//MessageBox.Show(response.StatusDescription);
				return result.ToString().Split('\n');
			}
			catch (Exception)
			{
				if (throwOnException)
				{
					throw;
				}
				return null;
			}
		}

		public bool MakeDirectory(string ftpUrl, bool throwOnException)
		{
			FtpWebResponse ftpResp = null;

			try
			{
				FtpWebRequest reqFTP = GetFtpRequestObject(ftpUrl);
				reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
				ftpResp = (FtpWebResponse)reqFTP.GetResponse();
				return true;
			}
			catch (Exception)
			{
				if (throwOnException)
				{
					throw;
				}
				return false;
			}
			finally
			{
				if (ftpResp != null)
				{
					ftpResp.Close();
				}
			}
		}
	}
}
