//using System;
//using System.Text;
//using System.Runtime.InteropServices;

//namespace ArchAngel.Licensing
//{
//    public class NalpeironAuthorizer
//    {
//        #region CPROT constants

//        internal enum CprotValues
//        {
//            CPROT_OK = 0,
//            CPROT_PRODUCT_AUTHORIZED = 10000,
//            CPROT_MISSING_PARM = -1,
//            CPROT_BAD_PARM = -2,
//            CPROT_BAD_UNLOCK_CODE = -3,
//            CPROT_PRODUCT_NOT_AUTHORIZED = -4,
//            CPROT_DEMO_EXPIRED = -5,
//            CPROT_EVAL_USES_EXPIRED = -6,
//            CPROT_LEASE_EXPIRED = -7,
//            CPROT_USES_EXHAUSED = -8,

//            CPROT_OPEN_DRIVE_FAILURE = -9,		//  DLLNT's compare updated buffer on drive to write buffer failed
//            CPROT_CANNOT_FIND_HARD_DRIVE = -10,	//	Windows open process failed.
//            CPROT_SERVICE_INSTALL_FAILED = -11,	//	Install Service failed
//            CPROT_SERVICE_FAILURE = -12,			//	Cannot add blessed path
//            CPROT_HARDDRIVE_READ_FAILURE = -13,	//	Cannot Read block
//            CPROT_HARDDRIVE_WRITE_FAILURE = -14,	//	Cannot write block
//            CPROT_NO_ROOM_FOR_ENTRY = -15,		//	Table out of room	
//            CPROT_FIND_ENTRY_FAILURE = -16,		//  FindEntry returned a value out of range
//            CPROT_INTERNET_DLL_NOT_PRESENT = -17,
//            CPROT_CANNOT_LOAD_INTERNET_DLL = -18,
//            CPROT_ERROR_GETTING_UNLOCKING_CODE = -19,
//            CPROT_INVALID_LICENSE_NUMBER = -20,
//            CPROT_INVALID_INSTALLATION_ID = -21,
//            CPROT_WRONG_LICENSE_NUMBER = -22,
//            CPROT_LICENSE_NUMBER_ALREADY_USED = -23,
//            CPROT_CANNOT_ACCESS_WEB_SITE = -24,		//  Maybe behind firewall
//            CPROT_CONCURRENT_USERS_EXCEEDED = -25,
//            CPROT_UNABLE_TO_READ_FROM_MEDIA = -26,
//            CPROT_UNABLE_TO_WRITE_TO_MEDIA = -27,
//            CPROT_INVALID_MEDIA_DATA = -28,
//            CPROT_LICENSE_ALREADY_INSTALLED = -29,
//            CPROT_UNKNOWN_ERROR = -30,
//            CPROT_INAPPROPRIATE_FUNCTION_CALL = -31,
//            CPROT_LICENSE_ALREADY_RETURNED = -32,
//            CPROT_DLL_NOT_CUSTOMIZED = -33,
//            CPROT_UNDEFINED_LICENSE_CONDITION = -34,
//            CPROT_SUBTRACTION_AMOUNT_GREATER_THAN_USES = -35,
//            CPROT_USER_IS_CHANGING_DATE = -36,
//            CPROT_NO_DRIVE_PRESENT = -37,
//            CPROT_NOT_INITIALIZED = -38,
//            CPROT_DLLNT_MISSING = -39,
//            CPROT_DLL9X_MISSING = -40,

//            CPROT_ASTDLL_MISSING = -53,
//            CPROT_ASTDLL_VERSION_NOT_CURRENT = -54,
//            CPROT_WRITE_DOES_NOT_VERIFY = -55,			// the value written does not compare with the write buffer
//            CPROT_UNABLE_VERIFY_WRITE = -56,
//            CPROT_FUNCTION_NOT_ALLOWED = -59,
//            CPROT_SERVICE_MISSING = -63,
//            CPROT_ERROR_GETTING_DISK_SPECIFICATRIONS = -64,	// Can't get max blocks and can't install service
//            CPROT_SERVICE_VERSION_NOT_CURRENT = -65,
//            CPROT_CANNOT_CONNECT_TO_INTERNET = -68,
//            CPROT_LICENSE_NOT_AUTHORIZED = -69, // GFH added from docs
//            CPROT_ERROR_GETTING_REMOVAL_CODE = -70, // GFH added from docs
//            CPROT_VMWARE_ENVIRONMENT = -71,
//            CPROT_VPC_ENVIRONMENT = -72,
//            CPROT_CUSTOMER_REGISTATION_REQUIRED = -73, // GFH added from docs
//            CPROT_UNABLE_TO_CONNECT_TO_WEBSITE = -74, // GFH added from docs
//            CPROT_SOCKET1_ERROR = -76, // GFH added from docs
//            CPROT_SOCKET2_ERROR = -77, // GFH added from docs
//            CPROT_UNABLE_TO_SEND_DATA = -78, // GFH added from docs
//            CPROT_UNABLE_TO_RECEIVE_DATA = -79, // GFH added from docs
//            CPROT_LICENSE_READ_FAILURE = -80,
//            CPROT_LICENSE_WRITE_FAILURE = -81,

//            CPROT_NEVER_AUTHORIZED = 0,
//            CPROT_NOT_AUTHORIZED = 1,
//            CPROT_UNLIMITED_USAGE = 2,
//            CPROT_LEASE_PERIOD_LIMITED = 3,
//            CPROT_AUTHORIZED_USES_LIMITED = 4,
//            CPROT_DEMO_DAYS_LIMITED = 5,
//            CPROT_DEMO_USES_LIMITED = 6,
//            CPROT_DEMO_USES_DAYS_LIMITED = 7,
//            CPROT_LIMITED_NETWORK_LICENSES = 8,
//            CPROT_DEMO_DAYS_EXPIRED = 9,
//            CPROT_DEMO_USES_EXPIRED = 10,
//            CPROT_LEASE_PERIOD_EXPIRED = 11,
//            CPROT_AUTHORIZED_USES_EXPIRED = 12,
//            CPROT_LEASE_USES_LIMITED = 13
//        }
//        private const string CPROT_RETURN_TRUE = "SUCCESS";
//        private const string CPROT_RETURN_FALSE = "FAIL";

//        #endregion

//        #region DLL Imports
//        // Import unmanaged functions. Ensure your application permissions allow this.
//        // The runtime must be able to locate the dll at runtime. The easiest
//        // solution is to copy Filechck.dll (and DLLNT.dll) to you application bin\($Configuzration)\ directory.
//        // This can be automated using build events in Visual Studio .NET
//        //[DllImport("Filechck.dll", CallingConvention = CallingConvention.Winapi)]
//        //private static extern int CheckLicense(ref long parmValue);
//        [DllImport("Filechck.dll", CallingConvention = CallingConvention.Winapi)]
//        private static extern int ValidateDLL(ref long customerNumber, ref long productNumber, ref long parmValue);

//        [DllImport("Filechck.dll", CallingConvention = CallingConvention.Winapi)]
//        private static extern int FindVirtualMachines();

//        [DllImport("Filechck.dll", CallingConvention = CallingConvention.Winapi)]
//        private static extern int CheckFlags(ref long ModuleNumber);

//        [DllImport("Filechck.dll", CallingConvention = CallingConvention.Winapi)]
//        private static extern int ViewExpirationDate(ref long Month, ref long Day, ref long Year);

//        [DllImport("Filechck.dll", CallingConvention = CallingConvention.Winapi)]
//        private static extern int CheckLicense(ref long RandomNumber);

//        [DllImport("Filechck.dll", CallingConvention = CallingConvention.Winapi)]
//        private static extern int AuditLicense(ref long RandomNumber);

//        [DllImport("Filechck.dll", CallingConvention = CallingConvention.Winapi)]
//        private static extern int DisplayInstallationID(StringBuilder CodeInfo); //private static extern int DisplayInstallationID(ref char CodeInfo);

//        [DllImport("Filechck.dll", CallingConvention = CallingConvention.Winapi)]
//        private static extern void DisplayVersion();

//        [DllImport("Filechck.dll", CallingConvention = CallingConvention.Winapi)]
//        private static extern int InstallLicense(StringBuilder UnlockCode); // private static extern int InstallLicense(ref char UnlockCode);

//        [DllImport("Filechck.dll", CallingConvention = CallingConvention.Winapi)]
//        private static extern int InitializeMedia(StringBuilder DriveLetter); // private static extern int InitializeMedia(ref char DriveLetter);

//        [DllImport("Filechck.dll", CallingConvention = CallingConvention.Winapi)]
//        private static extern int RemoveToMedia(StringBuilder DriveLetter); // private static extern int RemoveToMedia(ref char DriveLetter);

//        [DllImport("Filechck.dll", CallingConvention = CallingConvention.Winapi)]
//        private static extern int InstallFromMedia(StringBuilder DriveLetter); // private static extern int InstallFromMedia(ref char DriveLetter);

//        [DllImport("Filechck.dll", CallingConvention = CallingConvention.Winapi)]
//        private static extern int InternetActivate(StringBuilder LicenseNumber, StringBuilder ProxyServerAddress, int ProxyPort); // private static extern int InternetActivate(ref char LicenseNumber, ref char ProxyServerAddress, int ProxyPort);

//        [DllImport("Filechck.dll", CallingConvention = CallingConvention.Winapi)]
//        private static extern int ReturnLicense(StringBuilder LicenseNumber, StringBuilder ProxyServerAddress, int ProxyPort); // private static extern int ReturnLicense(ref char LicenseNumber, ref char ProxyServerAddress, int ProxyPort);

//        [DllImport("Filechck.dll", CallingConvention = CallingConvention.Winapi)]
//        private static extern int RemoveLicense(StringBuilder RemovalCode); // private static extern int RemoveLicense(ref char RemovalCode);

//        [DllImport("Filechck.dll", CallingConvention = CallingConvention.Winapi)]
//        private static extern int ViewUses();

//        [DllImport("Filechck.dll", CallingConvention = CallingConvention.Winapi)]
//        private static extern int SubtractUses(ref long Uses);
//        #endregion

//        // ensure no instances
//        private NalpeironAuthorizer()
//        { }

//        // main auth method
//        public static bool IsLicensed(out string message, out int daysRemaining, out bool errorOccurred, out bool demo)
//        {
//            demo = false;

//            if (Slyce.Common.Utility.IsRunningUnderVisualStudio)
//            {
//                message = "";
//                errorOccurred = false;
//                daysRemaining = 12;
//                return false;
//            }
//            errorOccurred = false;
//            daysRemaining = -1;
//            message = "";
//            long parmValue = 0;
//            int offset = 0;
//            int validDLL;
//            int copyProtectionResult;
//            //string[] rtnValues;
//            //rtnValues = new string[2];

//            // #########################################################
//            //
//            // set your customer/product numbers here for DLL validation
//            //
//            // #########################################################
//            long customerNumber = 0;
//            long productNumber = 0;

//            // ######################################################################
//            //
//            // set your XYZ values here if you are using Dynamic Random Authorization
//            //
//            // ######################################################################
//            long constantX = 0;
//            long constantY = 0;
//            long constantZ = 0;
            
//            if (constantX != 0)
//            {
//                // randomize prior to external call
//                parmValue = new Random((int)DateTime.Now.Ticks).Next(1, 500);
//                offset = (int)(constantX + ((parmValue * constantY) % constantZ));
//            }
//            else
//            {
//                parmValue = 50;
//            }

//            // See if DLL matches customer number and last 5 digits of product ID
//            // If Both the Customer Number and Product Number are 0, then skip it.
//            if (customerNumber != 0 && productNumber != 0)
//            {
//                if (constantX != 0)
//                {
//                    // randomize prior to external call
//                    parmValue = new Random((int)DateTime.Now.Ticks).Next(1, 500);
//                    offset = (int)(constantX + ((parmValue * constantY) % constantZ));
//                }
//                else
//                {
//                    parmValue = 50;
//                    offset = 0;
//                }

//                // ###########################################################
//                // call external ValidateDLL method
//                // ###########################################################
//                try
//                {
//                    validDLL = ValidateDLL(ref customerNumber, ref productNumber, ref parmValue);
//                }
//                catch (System.DllNotFoundException)
//                {
//                    errorOccurred = true;
//                    message = "Authorization component not found";
//                    return false;
//                }
//                catch (System.Exception ex)
//                {
//                    errorOccurred = true;
//                    message = "Unknown error [" + ex.Message + "]";
//                    return false;
//                }
//                validDLL -= offset;

//                if (validDLL != 1)
//                {
//                    errorOccurred = true;
//                    message = "One of the support DLLs in this folder is for a different product. Run setup again.";
//                    return false;
//                }
//            }

//            // ###########################################################
//            // perform authorisation
//            // ###########################################################
//            if (constantX != 0)
//            {
//                // randomize prior to external call
//                parmValue = new Random((int)DateTime.Now.Ticks).Next(50, 500);
//                offset = (int)(constantX + ((parmValue * constantY) % constantZ));
//            }
//            else
//            {
//                parmValue = 50;
//                offset = 0;
//            }
//            try
//            {
//                copyProtectionResult = CheckLicense(ref parmValue);

//                System.Windows.Forms.MessageBox.Show(string.Format("NalpeironAuthorizer.IsLicensed.copyProtectionResult = {0}\nparmValue={1}\noffset={2}\nconstantX={3}\nconstantY={4}\nconstantZ={5}", copyProtectionResult, parmValue, offset, constantX, constantY, constantZ));
//            }
//            catch (System.DllNotFoundException)
//            {
//                errorOccurred = true;
//                message = "Authorization component not found";
//                return false;
//            }
//            catch (System.Exception ex)
//            {
//                errorOccurred = true;
//                message = "Unknown error [" + ex.Message + "]";
//                return false;
//            }
//            if (constantX != 0)
//            {
//                copyProtectionResult -= offset;
//            }
//            if (copyProtectionResult == (int)CprotValues.CPROT_DLLNT_MISSING)
//            {
//                errorOccurred = true;
//                message = "DLLNT.dll is missing.";
//                return false;
//            }
//            else
//            {
//                if (copyProtectionResult == (int)CprotValues.CPROT_DLL9X_MISSING)
//                {
//                    errorOccurred = true;
//                    message = "DLL9X.dll is missing.";
//                    return false;
//                }
//            }

//            // ###########################################################
//            // First we handle the three possible normal return situations
//            // ###########################################################

//            // check for unlimited use
//            if (copyProtectionResult == (int)CprotValues.CPROT_PRODUCT_AUTHORIZED && (parmValue == (int)CprotValues.CPROT_UNLIMITED_USAGE || parmValue == (int)CprotValues.CPROT_LEASE_USES_LIMITED))
//            {
//                return true;
//            }
//            // check for limited licensed use
//            if (copyProtectionResult == (int)CprotValues.CPROT_PRODUCT_AUTHORIZED && (parmValue == (int)CprotValues.CPROT_LEASE_PERIOD_LIMITED || parmValue == (int)CprotValues.CPROT_AUTHORIZED_USES_LIMITED))
//            {
//                return true;
//            }
//            // check for unlimited server use
//            if (copyProtectionResult == (int)CprotValues.CPROT_PRODUCT_AUTHORIZED && parmValue == (int)CprotValues.CPROT_LIMITED_NETWORK_LICENSES)
//            {
//                return true;
//            }

//            // #################################
//            // Now handle evaluation limitations
//            // #################################

//            // demo period
//            if (copyProtectionResult >= 0 && copyProtectionResult <= 100 && parmValue == (int)CprotValues.CPROT_DEMO_DAYS_LIMITED)
//            {
//                daysRemaining = copyProtectionResult;

//                if (copyProtectionResult == 0)
//                {
//                    message = "This program is running in Evaluation mode. This is your last day of evaluation.";
//                }
//                else
//                {
//                    message = "This program is running in Evaluation mode. You have " + copyProtectionResult.ToString() + " days left.";
//                }
//                demo = true;
//                return true;
//            }

//            // demo uses
//            if (copyProtectionResult >= 0 && copyProtectionResult <= 100 && parmValue == (int)CprotValues.CPROT_DEMO_USES_LIMITED)
//            {
//                daysRemaining = copyProtectionResult;

//                if (copyProtectionResult == 0)
//                {
//                    message = "This program is running in Evaluation mode. This is your last evaluation trial.";
//                }
//                else
//                {
//                    message = "This program is running in Evaluation mode. You have " + copyProtectionResult.ToString() + " evaluation trials left.";
//                }
//                demo = true;
//                return true;
//            }

//            // demo period AND demo uses (still in evaluation mode)
//            if (copyProtectionResult >= 0 && copyProtectionResult <= 100 && parmValue == (int)CprotValues.CPROT_DEMO_USES_DAYS_LIMITED)
//            {
//                daysRemaining = copyProtectionResult;

//                if (copyProtectionResult == 0)
//                {
//                    message = "This program is running in Evaluation mode. This is your last day of evaluation.";
//                }
//                else
//                {
//                    message = "This program is running in Evaluation mode. You have " + copyProtectionResult.ToString() + " evaluation days left.";
//                }
//                demo = true;
//                return true;
//            }

//            // demo period and uses (evaluation expired)
//            if (copyProtectionResult < 0 && parmValue == (int)CprotValues.CPROT_DEMO_USES_DAYS_LIMITED)
//            {
//                if (copyProtectionResult == (int)CprotValues.CPROT_LEASE_EXPIRED)
//                {
//                    message = "Your evaluations are over.";
//                }
//                else
//                {
//                    message = "The evaluation period has expired.";
//                }
//                return false;
//            }

//            // license period about to expire
//            if (copyProtectionResult >= 0 && copyProtectionResult <= 48 && (parmValue == (int)CprotValues.CPROT_LEASE_PERIOD_LIMITED || parmValue == (int)CprotValues.CPROT_LEASE_USES_LIMITED))
//            {
//                daysRemaining = copyProtectionResult;

//                if (copyProtectionResult == 0)
//                {
//                    message = "This is the last day of your license period.";
//                }
//                else
//                {
//                    message = "This program's licence period is about to expire. You have " + copyProtectionResult.ToString() + " days left in this license period.";
//                }
//                return true;
//            }

//            // #####################################
//            // Now handle remaining error conditions
//            // #####################################

//            if (copyProtectionResult == (int)CprotValues.CPROT_PRODUCT_NOT_AUTHORIZED && parmValue == (int)CprotValues.CPROT_NOT_AUTHORIZED)
//            {
//                message = "This program must be unlocked to use.";
//                return false;
//            }
//            if (copyProtectionResult == (int)CprotValues.CPROT_DEMO_EXPIRED && parmValue == (int)CprotValues.CPROT_DEMO_DAYS_EXPIRED)
//            {
//                message = "The evaluation period has expired.";
//                return false;
//            }
//            if (copyProtectionResult == (int)CprotValues.CPROT_EVAL_USES_EXPIRED && parmValue == (int)CprotValues.CPROT_DEMO_USES_EXPIRED)
//            {
//                message = "There are no more evaluation uses.";
//                return false;
//            }
//            if (copyProtectionResult == (int)CprotValues.CPROT_LEASE_EXPIRED && parmValue == (int)CprotValues.CPROT_LEASE_PERIOD_EXPIRED)
//            {
//                message = "The lease period has expired.";
//                return false;
//            }
//            if (copyProtectionResult == (int)CprotValues.CPROT_USES_EXHAUSED && parmValue == (int)CprotValues.CPROT_AUTHORIZED_USES_EXPIRED)
//            {
//                message = "All uses have been exhausted.";
//                return false;
//            }
//            errorOccurred = (int)copyProtectionResult < 0;
//            message = GetErrorMessage((CprotValues)copyProtectionResult);
//            return !errorOccurred;
//        }

//        public static bool ActivateViaInternet(string licenseNumber, out string message)
//        {
//            string proxyAddress = "";
//            int port = 0;
//            bool hasProxy = Slyce.Common.Utility.GetDefaultProxy(out proxyAddress, out port);
//            int result = 0;
//            StringBuilder sbLicenseNumber = new StringBuilder(licenseNumber);
//            StringBuilder sbProxyAddress = new StringBuilder(proxyAddress);

//            try
//            {
//                if (hasProxy)
//                {
//                    result = InternetActivate(sbLicenseNumber, sbProxyAddress, port);
//                }
//                else
//                {
//                    result = InternetActivate(sbLicenseNumber, null, 0);
//                }
//                message = GetErrorMessage((CprotValues)result);
//                return result == (int)CprotValues.CPROT_PRODUCT_AUTHORIZED;
//            }
//            catch (DllNotFoundException ex)
//            {
//                int startPos = ex.Message.IndexOf("'") + 1;
//                int endPos = ex.Message.IndexOf("'", startPos + 1);
//                string filename = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), ex.Message.Substring(startPos, endPos - startPos));
//                message = "File not found:\n" + filename;
//                return false;
//            }
//        }

//        /// <summary>
//        /// Gets the MachineID of the computer.
//        /// </summary>
//        /// <param name="machineId">Machine ID.</param>
//        /// <param name="message">Error message.</param>
//        /// <returns>True if no error occurred, false if error occurred.</returns>
//        public static bool GetMachineId(out string machineId, out string message)
//        {
//            machineId = "";
//            System.Text.StringBuilder sb = new System.Text.StringBuilder(100);
//            try
//            {
//                int cprotResult = DisplayInstallationID(sb);
//                machineId = sb.ToString();
//                message = GetErrorMessage((CprotValues)cprotResult);
//                return cprotResult >= 0;
//            }
//            catch (DllNotFoundException ex)
//            {
//                int startPos = ex.Message.IndexOf("'") + 1;
//                int endPos = ex.Message.IndexOf("'", startPos + 1);
//                string filename = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), ex.Message.Substring(startPos, endPos - startPos));
//                message = "File not found:\n" + filename;
//                return false;
//            }
//        }

//        internal static bool IsValidUnlockCode(string text)
//        {
//            text = text.Replace("-", "").Replace(" ", "").Trim();
//            long val;
//            return text.Length == 16 && long.TryParse(text, out val);
//        }

//        internal static bool IsValidLicenseNumber(string text)
//        {
//            text = text.Replace("-", "").Replace(" ", "").Trim();
//            long val;
//            return text.Length == 15 && long.TryParse(text, out val);
//        }

//        /// <summary>
//        /// Activates a license on the machine. The unlocking key was previously retrieved via email or from webpage (Nalpeiron activation service).
//        /// </summary>
//        /// <param name="unlockingKey">The unlocking key to install.</param>
//        /// <param name="message">Error message if the call was unsuccessful.</param>
//        /// <returns>True if license installed successfully, false otherwise.</returns>
//        public static bool ActivateManually(string unlockingKey, out string message)
//        {
//            message = "";
//            unlockingKey = unlockingKey.Replace(" ", "").Replace("-", "").Trim();
//            StringBuilder sbUnlockingKey = new StringBuilder(unlockingKey);

//            if (!IsSerialNumber(unlockingKey))
//            {
//                message = "Invalid unlocking key. Must be 16 digits. No alphabetic characters.";
//                return false;
//            }
//            try
//            {
//                int result = InstallLicense(sbUnlockingKey);
//                message = GetErrorMessage((CprotValues)result);
//                return result >= 0;
//            }
//            catch (DllNotFoundException ex)
//            {
//                int startPos = ex.Message.IndexOf("'") + 1;
//                int endPos = ex.Message.IndexOf("'", startPos + 1);
//                string filename = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), ex.Message.Substring(startPos, endPos - startPos));
//                message = "File not found:\n" + filename;
//                return false;
//            }
//        }

//        /// <summary>
//        /// Removes an installed license from the machine.
//        /// </summary>
//        /// <param name="proofOfRemovalCode">Proof of removal code.</param>
//        /// <param name="message">Error message if the call was unsuccessful.</param>
//        /// <returns></returns>
//        public static bool RemoveLicense(out string proofOfRemovalCode, out string message)
//        {
//            proofOfRemovalCode = "";
//            StringBuilder sbProofOfRemovalCode = new StringBuilder(proofOfRemovalCode);
//            int result = RemoveLicense(sbProofOfRemovalCode);
//            proofOfRemovalCode = sbProofOfRemovalCode.ToString();
//            message = GetErrorMessage((CprotValues)result);
//            return result >= 0;
//        }

//        private static bool IsSerialNumber(string text)
//        {
//            long val;
//            return text.Length == 16 && long.TryParse(text, out val);
//        }

//        private static string GetErrorMessage(CprotValues val)
//        {
//            switch (val)
//            {
//                case CprotValues.CPROT_SERVICE_INSTALL_FAILED: return "Unable to install Windows Service.";
//                case CprotValues.CPROT_SERVICE_FAILURE: return "Windows Service failure.";
//                case CprotValues.CPROT_HARDDRIVE_READ_FAILURE: return "Unable to read from hard drive.";
//                case CprotValues.CPROT_HARDDRIVE_WRITE_FAILURE: return "Unable to write to hard drive.";
//                case CprotValues.CPROT_NO_ROOM_FOR_ENTRY: return "License access failure 1.";
//                case CprotValues.CPROT_FIND_ENTRY_FAILURE: return "License access failure 2.";
//                case CprotValues.CPROT_CONCURRENT_USERS_EXCEEDED: return "Maximum number of cuncurrent users exceeded.";
//                case CprotValues.CPROT_UNABLE_TO_READ_FROM_MEDIA: return "Unable to read USB Key.";
//                case CprotValues.CPROT_UNABLE_TO_WRITE_TO_MEDIA: return "Unable to write to USB Key.";
//                case CprotValues.CPROT_DLL_NOT_CUSTOMIZED: return "Publisher supplied non-customized DLL.";
//                case CprotValues.CPROT_UNDEFINED_LICENSE_CONDITION: return "License state undefined - reinstall authorization.";
//                case CprotValues.CPROT_USER_IS_CHANGING_DATE: return "Securtiy bypass attempted - product must be reauthorized.";
//                case CprotValues.CPROT_DLLNT_MISSING: return "DLLNT.dll is missing.";
//                case CprotValues.CPROT_ASTDLL_MISSING: return "File astdll.dll is missing.";
//                case CprotValues.CPROT_ASTDLL_VERSION_NOT_CURRENT: return "File astdll.dll is not current.";
//                case CprotValues.CPROT_WRITE_DOES_NOT_VERIFY: return "Media is bad or computer is not writing correctly.";
//                case CprotValues.CPROT_UNABLE_VERIFY_WRITE: return "Media defective or computer not reading correctly.";
//                case CprotValues.CPROT_SERVICE_MISSING: return "File astsrv.exe is missing.";
//                case CprotValues.CPROT_ERROR_GETTING_DISK_SPECIFICATRIONS: return "File dllnt.dll is not current version..";
//                case CprotValues.CPROT_SERVICE_VERSION_NOT_CURRENT: return "File astsrv.exe is not current version.";
//                case CprotValues.CPROT_VMWARE_ENVIRONMENT: return "This product will not work in a VMWare environment.";
//                case CprotValues.CPROT_VPC_ENVIRONMENT: return "This product will not work in a Virtual PC environment.";
//                case CprotValues.CPROT_LICENSE_READ_FAILURE: return "Error reading license data.";
//                case CprotValues.CPROT_LICENSE_WRITE_FAILURE: return "Error writing license data.";
//                case CprotValues.CPROT_PRODUCT_AUTHORIZED: return "Unknown error.";


//                case CprotValues.CPROT_OK: return "OK.";
//                case CprotValues.CPROT_MISSING_PARM: return "Missing parameter.";
//                case CprotValues.CPROT_BAD_PARM: return "Bad parameter.";
//                case CprotValues.CPROT_BAD_UNLOCK_CODE: return "Bad unlock code.";
//                case CprotValues.CPROT_PRODUCT_NOT_AUTHORIZED: return "Product not authorized.";
//                case CprotValues.CPROT_DEMO_EXPIRED: return "Demo has expired.";
//                case CprotValues.CPROT_EVAL_USES_EXPIRED: return "Number of allowed evaluation usages has been exceeded.";
//                case CprotValues.CPROT_LEASE_EXPIRED: return "Lease usage has expired..";
//                case CprotValues.CPROT_USES_EXHAUSED: return "Number of allowed usages has been exceeded.";
//                case CprotValues.CPROT_OPEN_DRIVE_FAILURE: return "Failed to open drive.";//  DLLNT's compare updated buffer on drive to write buffer failed
//                case CprotValues.CPROT_CANNOT_FIND_HARD_DRIVE: return "Cannot find hard-drive.";//	Windows open process failed.
//                case CprotValues.CPROT_INTERNET_DLL_NOT_PRESENT: return "Internet DLL is missing.";
//                case CprotValues.CPROT_CANNOT_LOAD_INTERNET_DLL: return "Cannot load internet DLL.";
//                case CprotValues.CPROT_ERROR_GETTING_UNLOCKING_CODE: return "Error getting unlock code.";
//                case CprotValues.CPROT_INVALID_LICENSE_NUMBER: return "Invalid license number.";
//                case CprotValues.CPROT_INVALID_INSTALLATION_ID: return "Invalid installation ID.";
//                case CprotValues.CPROT_WRONG_LICENSE_NUMBER: return "Wrong license number.";
//                case CprotValues.CPROT_LICENSE_NUMBER_ALREADY_USED: return "License number has already been used.";
//                case CprotValues.CPROT_CANNOT_ACCESS_WEB_SITE: return "Cannot access internet. Maybe firewall is blocking access.";//  Maybe behind firewall
//                case CprotValues.CPROT_INVALID_MEDIA_DATA: return "Invalid media data.";
//                case CprotValues.CPROT_LICENSE_ALREADY_INSTALLED: return "License is already installed.";
//                case CprotValues.CPROT_UNKNOWN_ERROR: return "Unknown error.";
//                case CprotValues.CPROT_INAPPROPRIATE_FUNCTION_CALL: return "Inappropriate function call.";
//                case CprotValues.CPROT_LICENSE_ALREADY_RETURNED: return "License already returned.";
//                case CprotValues.CPROT_SUBTRACTION_AMOUNT_GREATER_THAN_USES: return "Subtraction amount greater than the number of uses.";
//                case CprotValues.CPROT_NO_DRIVE_PRESENT: return "No drive present.";
//                case CprotValues.CPROT_NOT_INITIALIZED: return "Not initialized.";
//                case CprotValues.CPROT_DLL9X_MISSING: return "DLL9x.dll is missing.";
//                case CprotValues.CPROT_FUNCTION_NOT_ALLOWED: return "Function not allowed.";
//                case CprotValues.CPROT_CANNOT_CONNECT_TO_INTERNET: return "Cannot access internet. Maybe firewall is blocking access.";
//                case CprotValues.CPROT_LICENSE_NOT_AUTHORIZED: return "License not authorised.";// GFH added from docs
//                case CprotValues.CPROT_ERROR_GETTING_REMOVAL_CODE: return "Error getting removal code.";// GFH added from docs
//                case CprotValues.CPROT_CUSTOMER_REGISTATION_REQUIRED: return "Customer registration required.";// GFH added from docs
//                case CprotValues.CPROT_UNABLE_TO_CONNECT_TO_WEBSITE: return "Cannot access internet. Maybe firewall is blocking access.";// GFH added from docs
//                case CprotValues.CPROT_SOCKET1_ERROR: return "Socket 1 error.";
//                case CprotValues.CPROT_SOCKET2_ERROR: return "Socket 2 error.";
//                case CprotValues.CPROT_UNABLE_TO_SEND_DATA: return "Unable to send data.";
//                case CprotValues.CPROT_UNABLE_TO_RECEIVE_DATA: return "Unable to receive data.";

//                case CprotValues.CPROT_NOT_AUTHORIZED: return "Not authorised.";
//                case CprotValues.CPROT_UNLIMITED_USAGE: return "Unlimited usage.";
//                case CprotValues.CPROT_LEASE_PERIOD_LIMITED: return "Lease period limited.";
//                case CprotValues.CPROT_AUTHORIZED_USES_LIMITED: return "Authorixed uses limited.";
//                case CprotValues.CPROT_DEMO_DAYS_LIMITED: return "Demo days limited.";
//                case CprotValues.CPROT_DEMO_USES_LIMITED: return "Demo uses limited.";
//                case CprotValues.CPROT_DEMO_USES_DAYS_LIMITED: return "Demo use days limited.";
//                case CprotValues.CPROT_LIMITED_NETWORK_LICENSES: return "Limited network licenses.";
//                case CprotValues.CPROT_DEMO_DAYS_EXPIRED: return "Demo days expired.";
//                case CprotValues.CPROT_DEMO_USES_EXPIRED: return "Demo uses expired.";
//                case CprotValues.CPROT_LEASE_PERIOD_EXPIRED: return "Lease period expired.";
//                case CprotValues.CPROT_AUTHORIZED_USES_EXPIRED: return "Authorized uses expired.";
//                case CprotValues.CPROT_LEASE_USES_LIMITED: return "Lease uses expired.";

//                default:
//                    if ((int)val < 0)
//                    {
//                        return "ERROR: Please inform support@slyce.com that CPROT value [" + val + "] is not handled correctly.";
//                    }
//                    //throw new NotImplementedException("CPROT value not handled yet: " + val.ToString());
//                    return "";
//            }
//        }

//    }
//}
