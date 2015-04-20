using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using IntelliLock.Licensing;

namespace ArchAngel.Licensing
{
	public class ReactorAuthorizer
    {
        public enum LockTypes
        {
            None,
            Days,
            Date
        }

        public static bool IsLicensed(out string message, out int daysRemaining, out bool errorOccurred, out bool demo, out LockTypes lockType)
        {
            lockType = LockTypes.None;
            message = "";
            errorOccurred = false;
            daysRemaining = 0;
            demo = true;

            try
            {
#if DEBUG
                    daysRemaining = 12;
                    demo = true;
                    return false;
#endif
                errorOccurred = false;
                //daysRemaining = License.Status.Evaluation_Time - License.Status.Evaluation_Time_Current;
				daysRemaining = EvaluationMonitor.CurrentLicense.ExpirationDays - EvaluationMonitor.CurrentLicense.ExpirationDays_Current;
                demo = false;

                // Are we in demo mode?
                //if (License.Status.Evaluation_Lock_Enabled)
				if (EvaluationMonitor.CurrentLicense.ExpirationDays_Enabled)
                {
                    demo = true;
                    //daysRemaining = License.Status.Evaluation_Time - License.Status.Evaluation_Time_Current;
					daysRemaining = EvaluationMonitor.CurrentLicense.ExpirationDays - EvaluationMonitor.CurrentLicense.ExpirationDays_Current;
                    lockType = LockTypes.Days;
                }
                //else if (License.Status.Expiration_Date_Lock_Enable)
				else if (EvaluationMonitor.CurrentLicense.ExpirationDate_Enabled)
                {
                    demo = true;
                    //daysRemaining = License.Status.Expiration_Date.Subtract(DateTime.Now).Days;
					daysRemaining = EvaluationMonitor.CurrentLicense.ExpirationDate.Subtract(DateTime.Now).Days;
                    lockType = LockTypes.Date;
                }
                //return License.Status.Licensed;
				return EvaluationMonitor.CurrentLicense.LicenseStatus == LicenseStatus.Licensed;
                
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

        public static Dictionary<string, string> AdditionalLicenseInfo
        {
            get
            {
                Dictionary<string, string> results = new Dictionary<string, string>();

				//// Check first if a valid license file is found
				//if (License.Status.Licensed)
				//{
				//    // Read additional license information
				//    for (int i = 0; i < License.Status.KeyValueList.Count; i++)
				//    {
				//        string key = License.Status.KeyValueList.GetKey(i).ToString();
				//        string value = License.Status.KeyValueList.GetByIndex(i).ToString();
				//        results.Add(key, value);
				//    }
				//}
				/* Check first if a valid license file is found */
				if (EvaluationMonitor.CurrentLicense.LicenseStatus == IntelliLock.Licensing.LicenseStatus.Licensed)
				{
					/* Read additional license information */
					for (int i = 0; i < EvaluationMonitor.CurrentLicense.LicenseInformation.Count; i++)
					{
						string key = EvaluationMonitor.CurrentLicense.LicenseInformation.GetKey(i).ToString();
						string value = EvaluationMonitor.CurrentLicense.LicenseInformation.GetByIndex(i).ToString();
						results.Add(key, value);
					}
				}
                return results;
            }
        }

        public static bool ActivateViaInternet(string licenseNumber, out string message)
        {
            string proxyAddress;
            int port;

        	string hardwareId;
            GetMachineId(out hardwareId, out message);
            System.Net.WebRequest req = System.Net.WebRequest.Create(@"http://activate.slyce.com/Default.aspx?fromAA_LicenseNumber=" + licenseNumber + "&fromAA_HardwareID=" + hardwareId + "&fromAA_User=" + Environment.MachineName);
            req.UseDefaultCredentials = true;
            req.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            System.Net.WebResponse response = req.GetResponse();
            string errorString = RetrieveFromURL(response);

            if (string.IsNullOrEmpty(errorString))
            {
                message = "";
                return true;
            }
        	message = errorString;
        	return false;
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
        	string filename = filenameHeader.Substring(filenameHeader.IndexOf("filename=") + "filename=".Length);
        	// 2. Get the Stream Object from the response
        	System.IO.Stream responseStream = response.GetResponseStream();
        	WriteStreamToFile(responseStream, Path.Combine(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), filename));
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

        /// <summary>
        /// Gets the MachineID of the computer.
        /// </summary>
        /// <param name="machineId">Machine ID.</param>
        /// <param name="message">Error message.</param>
        /// <returns>True if no error occurred, false if error occurred.</returns>
        public static bool GetMachineId(out string machineId, out string message)
        {
            //machineId = License.Status.HardwareID;
			machineId = EvaluationMonitor.CurrentLicense.HardwareID;
            message = "";
            return true;
        }

        internal static bool IsValidUnlockCode(string text)
        {
            text = text.Replace("-", "").Replace(" ", "").Trim();
            long val;
            return text.Length == 16 && long.TryParse(text, out val);
        }

        internal static bool IsValidLicenseNumber(string text)
        {
            text = text.Replace("-", "").Replace(" ", "").Trim();
            long val;
            return text.Length == 15 && long.TryParse(text, out val);
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
            string destinationFile = Path.Combine(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), Path.GetFileName(licenseFile));

            if (File.Exists(destinationFile))
            {
                if (Slyce.Common.Utility.GetCheckSumOfFile(licenseFile) == Slyce.Common.Utility.GetCheckSumOfFile(destinationFile))
                {
                    // The files are the same, just return success
                    return true;
                }
            	if (System.Windows.Forms.MessageBox.Show("Overwrite existing license file?", "Overwrite", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            	{
            		Slyce.Common.Utility.DeleteFileBrute(destinationFile);
            		File.Copy(licenseFile, destinationFile);
            		return true;
            	}
            	message = "Can't overwrite existing license file.";
            	return false;
            }
        	File.Copy(licenseFile, destinationFile);
        	return true;
        }

        /// <summary>
        /// Removes an installed license from the machine.
        /// </summary>
        /// <param name="proofOfRemovalCode">Proof of removal code.</param>
        /// <param name="message">Error message if the call was unsuccessful.</param>
        /// <returns></returns>
        public static bool RemoveLicense(out string proofOfRemovalCode, out string message)
        {
            proofOfRemovalCode = "";
            message = "";

            try
            {
                //proofOfRemovalCode = License.Status.InvalidateLicense();
				proofOfRemovalCode = License_DeActivator.DeactivateLicense();
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

        private static bool IsSerialNumber(string text)
        {
            long val;
            return text.Length == 16 && long.TryParse(text, out val);
        }



    }
}
