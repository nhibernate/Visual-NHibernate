using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ArchAngel.Licensing
{
	public class SlyceAuthorizer
	{
		public enum LockTypes
		{
			None,
			Days,
			Date
		}

		[DotfuscatorDoNotRename]
		public enum LicenseStates
		{
			EvaluationExpired,
			EvaluationMode,
			HardwareNotMatched,
			InvalidSignature,
			Licensed,
			LicenseFileNotFound,
			NotChecked,
			ServerValidationFailed,
			Revoked
		}

		internal static string ActivationUrl = @"http://www.slyce.com/Support/";
		private static string _LicenseFolder;

		public static void SetLicenseFilename(string filename)
		{
			LicenseMonitor.LicenseFilename = filename;
		}

		public static void Reset()
		{
			LicenseMonitor.Reset();
		}

		/// <summary>
		/// Gets the MachineID of the computer.
		/// </summary>
		/// <param name="machineId">Machine ID.</param>
		/// <param name="message">Error message.</param>
		/// <returns>True if no error occurred, false if error occurred.</returns>
		public static List<string> GetMachineIds(out string message)
		{
			return LicenseMonitor.GetMachineIds(out message);
		}

		public static bool IsLicensed(string licenseFilename, out string message, out int daysRemaining, out bool errorOccurred, out bool demo, out LockTypes lockType, out LicenseStates status)
		{
			status = LicenseStates.NotChecked;
			lockType = LockTypes.None;
			message = "";
			errorOccurred = false;
			daysRemaining = 0;
			demo = true;

			// TODO: add licensing back in for the real launch (not beta)
			demo = false;
			//return true;

			try
			{
				LicenseMonitor.LicenseFilename = licenseFilename;
				errorOccurred = false;
				daysRemaining = LicenseMonitor.CurrentLicense.ExpirationDays - LicenseMonitor.CurrentLicense.ExpirationDays_Current;

				switch (LicenseMonitor.CurrentLicense.LicenseStatus.ToString())
				{
					case "EvaluationExpired":
						status = LicenseStates.EvaluationExpired;
						break;
					case "EvaluationMode":
						status = LicenseStates.EvaluationMode;
						break;
					case "HardwareNotMatched":
						status = LicenseStates.HardwareNotMatched;
						break;
					case "InvalidSignature":
						status = LicenseStates.InvalidSignature;
						break;
					case "Licensed":
						status = LicenseStates.Licensed;
						break;
					case "LicenseFileNotFound":
						status = LicenseStates.LicenseFileNotFound;
						break;
					case "NotChecked":
						status = LicenseStates.NotChecked;
						break;
					case "ServerValidationFailed":
						status = LicenseStates.ServerValidationFailed;
						break;
					case "Revoked":
						status = LicenseStates.Revoked;
						break;
				}
				demo = false;

				// Are we in demo mode?
				if (LicenseMonitor.CurrentLicense.ExpirationDays_Enabled)
				{
					demo = true;
					daysRemaining = LicenseMonitor.CurrentLicense.ExpirationDays - LicenseMonitor.CurrentLicense.ExpirationDays_Current;
					lockType = LockTypes.Days;
				}
				else if (LicenseMonitor.CurrentLicense.ExpirationDate_Enabled)
				{
					demo = true;
					daysRemaining = LicenseMonitor.CurrentLicense.ExpirationDate.Subtract(DateTime.Now).Days;
					lockType = LockTypes.Date;
				}
				return LicenseMonitor.CurrentLicense.LicenseStatus == LicenseMonitor.LicenseStates.Licensed;
			}
			catch (Exception ex)
			{
				errorOccurred = true;
				message = ex.Message;
				return false;
			}
		}

		public static Dictionary<string, string> AdditionalLicenseInfo
		{
			get
			{
				return LicenseMonitor.CurrentLicense.LicenseData;
			}
		}

		public static License Lic
		{
			get
			{
				return LicenseMonitor.CurrentLicense.Lic;
			}
		}

		public static string Serial
		{
			get
			{
				return LicenseMonitor.CurrentLicense.Serial;
			}
		}

		public static bool ActivateViaInternet(string licenseNumber, string hardwareId, string email, bool fullLicense, out string message)
		{
			System.Collections.Specialized.StringDictionary dict = new System.Collections.Specialized.StringDictionary();

			if (fullLicense)
			{
				dict.Add("action", "getLicense");
				dict.Add("caller", "auto");
				dict.Add("serial", licenseNumber);
			}
			else
			{
				dict.Add("action", "getTrial");
				dict.Add("caller", "auto");
				dict.Add("email", email);
				dict.Add("product", "Visual NHibernate");
				dict.Add("trialNumber", hardwareId);
			}
			System.Net.WebResponse response;

			if (Slyce.Common.Utility.SendHttpPost(SlyceAuthorizer.ActivationUrl, dict, out response))
			{
				string errorString = RetrieveFromURL(response);

				if (string.IsNullOrWhiteSpace(errorString))
				{
					message = "";
					return true;
				}
				message = errorString;
				return false;
			}
			else
			{
				message = "Error occurred. Try again";
				return false;
			}
		}

		private static string RetrieveFromURL(System.Net.WebResponse response)
		{
			if (response.ContentType != "application/octet-stream")
			{
				// Error occurred - we did not receive a file, but text instead
				StringBuilder sb = new StringBuilder(1000);
				Stream ReceiveStream = response.GetResponseStream();

				Encoding encode = Encoding.GetEncoding("utf-8");

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
			// We received a file, now save it
			string filenameHeader = response.Headers["Content-Disposition"];
			string filename = System.Web.HttpUtility.UrlDecode(filenameHeader.Substring(filenameHeader.IndexOf("filename=") + "filename=".Length));
			// 2. Get the Stream Object from the response
			using (System.IO.Stream responseStream = response.GetResponseStream())
			{
				try
				{
					WriteStreamToFile(responseStream, Path.Combine(SlyceAuthorizer.LicenseFolder, filename));
				}
				catch (Exception ex)
				{
					return ex.Message;
				}
			}
			return "";
		}

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

		internal static bool IsValidUnlockCode(string text)
		{
			text = text.Replace("-", "").Replace(" ", "").Trim();
			long val;
			return text.Length == 16 && long.TryParse(text, out val);
		}

		internal static bool IsValidLicenseNumber(string text)
		{
			return text.Length > 0;
			/*
						text = text.Replace("-", "").Replace(" ", "").Trim();
						long val;
						return text.Length == 15 && long.TryParse(text, out val);
			*/
		}

		/// <summary>
		/// Activates a license on the machine. The unlocking key was previously retrieved via email or from webpage (Nalpeiron activation service).
		/// </summary>
		/// <param name="licenseFile">The license file to install.</param>
		/// <param name="message">Error message if the call was unsuccessful.</param>
		/// <returns>True if license installed successfully, false otherwise.</returns>
		public static bool ActivateManually(string licenseFile, out string message)
		{
			message = "";

			// Copy the file to the installation folder
			string destinationFile = Path.Combine(SlyceAuthorizer.LicenseFolder, "Visual NHibernate License.SlyceLicense");

			if (File.Exists(destinationFile))
			{
				if (Slyce.Common.Utility.GetCheckSumOfFile(licenseFile) == Slyce.Common.Utility.GetCheckSumOfFile(destinationFile))
				{
					// The files are the same, just return success
					return true;
				}
				if (System.Windows.Forms.MessageBox.Show("Overwrite existing license file?", "Overwrite", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
				{
					try
					{
						Slyce.Common.Utility.DeleteFileBrute(destinationFile);
						File.Copy(licenseFile, destinationFile);

						return true;
					}
					catch (Exception ex)
					{
						System.Windows.Forms.MessageBox.Show(ex.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
						return false;
					}
				}
				message = "Can't overwrite existing license file.";
				return false;
			}
			File.Copy(licenseFile, destinationFile);
			return true;
		}

		///// <summary>
		///// Removes an installed license from the machine.
		///// </summary>
		///// <param name="proofOfRemovalCode">Proof of removal code.</param>
		///// <param name="message">Error message if the call was unsuccessful.</param>
		///// <returns></returns>
		//public static bool RemoveLicense(out string proofOfRemovalCode, out string message)
		//{
		//    proofOfRemovalCode = "";
		//    message = "";

		//    try
		//    {
		//        //proofOfRemovalCode = License.Status.InvalidateLicense();
		//        proofOfRemovalCode = License_DeActivator.DeactivateLicense();
		//        return true;
		//    }
		//    catch (Exception ex)
		//    {
		//        message = ex.Message;
		//        return false;
		//    }
		//}

		private static bool IsSerialNumber(string text)
		{
			long val;
			return text.Length == 16 && long.TryParse(text, out val);
		}

		public static string LicenseFolder
		{
			get { return _LicenseFolder; }
			set
			{
				if (!Directory.Exists(value))
					Directory.CreateDirectory(value);

				_LicenseFolder = value;
			}
		}

	}
}
