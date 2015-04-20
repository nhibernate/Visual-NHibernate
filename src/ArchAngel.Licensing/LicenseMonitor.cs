using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Text;
using Microsoft.Win32;

namespace ArchAngel.Licensing
{
	internal class LicenseMonitor
	{
		[DotfuscatorDoNotRename]
		internal enum LicenseStates
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
		private enum HardwareIdAlgorithm
		{
			HarddriveOnly,
			MotherBoardOnly,
			Motherboard_And_Bios,
			OS_Only,
			CPU_Only,
			Registry
		}

		internal static LicenseMonitor CurrentLicense = new LicenseMonitor();
		private static char[] PublicKeyExponent = new char[] { 'A', 'Q', 'A', 'B' };
		internal LicenseStates _LicenseStatus = LicenseStates.NotChecked;
		private static List<string> RevokedSerials;
		private static string _HardwareId;
		public static string LicenseFilename = "";
		private static bool IsInitialised = false;
		private int _ExpirationDays;
		private int _ExpirationDays_Current;
		private bool _ExpirationDays_Enabled;
		private bool _ExpirationDate_Enabled;
		private DateTime _ExpirationDate;
		internal Dictionary<string, string> LicenseData = new Dictionary<string, string>();
		private static System.Management.ManagementObjectSearcher searcher2;
		private static System.Management.ManagementObjectCollection moc;
		internal string Serial;
		internal License Lic;

		private LicenseMonitor()
		{
		}

		internal static void Reset()
		{
			IsInitialised = false;
			//LicenseFilename = "";
			CurrentLicense.Lic = null;
			CurrentLicense.Init();
		}

		private void Init()
		{
			if (!IsInitialised)
			{
				IsInitialised = true;
				//LicenseFilename = "";
				Lic = null;
				RevokedSerials = new List<string>();
				RevokedSerials.AddRange(new string[] { });
				RevokedSerials.Sort();
				ReadLicenseInfo();
			}
		}

		internal int ExpirationDays
		{
			get
			{
				Init();
				return _ExpirationDays;
			}
			private set { _ExpirationDays = value; }
		}

		internal int ExpirationDays_Current
		{
			get
			{
				Init();
				return _ExpirationDays_Current;
			}
			private set { ExpirationDays_Current = value; }
		}

		internal bool ExpirationDays_Enabled
		{
			get
			{
				Init();
				return _ExpirationDays_Enabled;
			}
			private set { ExpirationDays_Enabled = value; }
		}

		internal bool ExpirationDate_Enabled
		{
			get
			{
				Init();
				return _ExpirationDate_Enabled;
			}
			private set { _ExpirationDate_Enabled = value; }
		}

		internal DateTime ExpirationDate
		{
			get
			{
				Init();
				return _ExpirationDate;
			}
			private set { _ExpirationDate = value; }
		}

		internal LicenseStates LicenseStatus
		{
			get
			{
				Init();
				return _LicenseStatus;
			}
			set { _LicenseStatus = value; }
		}

		private static string PublicKeyModulus
		{
			get { return "<Modulus>lRczp0fqVQB9heKLqzgcyQR6vu11GlUPeShB8yDGj8AqzN1pXE9hSTJ32VUVlBwlCH08VYdyeqYM32wfE518g5T0k2yZ5Q/ZgsDtBibdp9s1sRsqafItETieChG16WT5pvaaYUyxZOila6ajUr3y5wAC4maF7cE00tBqZTjtp7Py2W96pDXKACGSmP2hR00uC8vmIuGIPhBkdZ5tPol70apUg4GpkJNFZ9KVrSUMNZynV7Ca7D4rsacvFKaiyORbZARWxPlHAqtakyN1iXfeks4Bx91odkTSQugulc9YqMrzNuaB5Fp++uyoc3uZyeFkdjqMESNPFXvbO+8ld+ZcYw==</Modulus>"; }
		}

		internal static string LicenseFilePath
		{
			get { return Path.Combine(SlyceAuthorizer.LicenseFolder, LicenseFilename); }
		}

		private void ReadLicenseInfo()
		{
			LicenseData.Clear();

			if (SecurityChecks.IsAllSecurityOk)
			{
				if (string.IsNullOrEmpty(LicenseFilename))
				{
					//throw new InvalidOperationException("LicenseFilename has not been set.");
					LicenseStatus = LicenseStates.LicenseFileNotFound;
					Lic = null;
					Serial = "";
					ExpirationDate_Enabled = true;
					ExpirationDate = new DateTime(1970, 1, 1);
					return;
				}
				// Inspect license file
				string licenseFile = LicenseFilePath;

				if (File.Exists(licenseFile))
				{
					string publicKey = string.Format("<RSAKeyValue>{0}<Exponent>{1}</Exponent></RSAKeyValue>", PublicKeyModulus, new string(PublicKeyExponent));
					string licenseText = Slyce.Common.Utility.ReadTextFile(licenseFile);

					if (!string.IsNullOrWhiteSpace(licenseText) && IsLicenceValid(publicKey, licenseText))
					{
						License lic = new License(licenseText);
						Lic = lic;
						Serial = lic.Serial;

						// Check that the serial hasn't been revoked
						if (RevokedSerials.BinarySearch(Serial) >= 0)
						{
							LicenseStatus = LicenseStates.Revoked;
							return;
						}
						//if (lic.Type == License.LicenseTypes.Full)
						//{
						//    LicenseStatus = LicenseStates.Licensed;
						//    ExpirationDate_Enabled = false;
						//}
						//else // This is a trial license
						//{
						ExpirationDate_Enabled = true;
						ExpirationDate = lic.ExpiryDate.AddMinutes(1);

						if (lic.Type == License.LicenseTypes.Trial)
						{
							if (DateTime.Now.Subtract(ExpirationDate).Days > 0)
								LicenseStatus = LicenseStates.EvaluationExpired;
							else
								LicenseStatus = LicenseStates.EvaluationMode;

							string message;
							List<string> machineIds = GetMachineIds(out message);

							if (!string.IsNullOrEmpty(lic.HardwareId) && !Slyce.Common.Utility.StringsAreEqual(lic.HardwareId, machineIds[0], false))
							{
								LicenseStatus = LicenseStates.HardwareNotMatched;
								return;
							}
						}
						else // Full
						{
							if (DateTime.Now.Subtract(ExpirationDate).Days > 0)
							{
								LicenseStatus = LicenseStates.EvaluationExpired;
								ExpirationDate_Enabled = true;
							}
							else
							{
								LicenseStatus = LicenseStates.Licensed;
								ExpirationDate_Enabled = false;
							}
						}
						LicenseData.Add("Serial", lic.Serial);
						LicenseData.Add("Creation Date", lic.OrderDate.ToString("d MMM yyyy"));
						LicenseData.Add("Company", lic.Company);
						LicenseData.Add("Address", string.Format("{1}{0}{2}{0}{3}{0}{4}", Environment.NewLine, lic.AddressLine1, lic.AddressLine2, lic.AddressCity, lic.AddressCountry));
						LicenseData.Add("Telephone", lic.Phone);
						LicenseData.Add("Contact person", lic.Name);
						LicenseData.Add("Contact email", lic.Email);
					}
					else
					{
						LicenseStatus = LicenseStates.InvalidSignature;
					}
				}
				else
				{
					LicenseStatus = LicenseStates.LicenseFileNotFound;
				}
			}
			else
			{
				// Check the 'dumb' options to fool any attached debuggers, regmon etc
			}
		}

		private static DateTime InstallDate
		{
			get
			{

				return DateTime.Now;
			}
		}

		internal static bool IsLicenceValid(string publicKey, string licenseText)
		{
			licenseText = Slyce.Common.Utility.StandardizeLineBreaks(licenseText, Slyce.Common.Utility.LineBreaks.Unix).Replace("\n", "");
			int start = licenseText.ToLower().IndexOf("<signature>");
			int end = licenseText.ToLower().IndexOf("</signature>") + "</signature>".Length;
			string digitalSignature = licenseText.Substring(start + "<signature>".Length, end - start - "<signature>".Length - "</signature>".Length);
			licenseText = licenseText.Substring(0, start) + "<signature />" + licenseText.Substring(end);
			//XmlDocument doc = new XmlDocument();
			//doc.LoadXml(licenseText);
			string rawXml = LicenseHelper.FormatXml(licenseText);
			//string rawXml = doc.InnerXml;//.Substring(doc.InnerXml.IndexOf(">") + 1);
			//rawXml = rawXml.Substring(rawXml.IndexOf(@"?>") + 1).Replace("  ", " ").Replace("\r", "").Replace("\n", "").Replace("\t", "");
			//rawXml = rawXml.Replace("  ", " ").Replace("\r", "").Replace("\n", "").Replace("\t", "");
			return Licensing.LicenseHelper.VerifySignature(rawXml, digitalSignature, publicKey);
		}

		/// <summary>
		/// Gets the MachineID of the computer.
		/// </summary>
		/// <param name="machineId">Machine ID.</param>
		/// <param name="message">Error message.</param>
		/// <returns>True if no error occurred, false if error occurred.</returns>
		public static List<string> GetMachineIds(out string message)
		{
			List<string> machineIds = new List<string>();

			if (LicenseMonitor.CurrentLicense.Lic != null &&
				LicenseMonitor.CurrentLicense.Lic.Type == License.LicenseTypes.Trial &&
				!LicenseMonitor.CurrentLicense.Lic.HardwareId.StartsWith("A-", true, System.Globalization.CultureInfo.InvariantCulture) &&
				!LicenseMonitor.CurrentLicense.Lic.HardwareId.StartsWith("A2-", true, System.Globalization.CultureInfo.InvariantCulture))
			{
				ASCIIEncoding AE = new ASCIIEncoding();
				System.Security.Cryptography.SHA1 shaM = new System.Security.Cryptography.SHA1Managed();

				HardwareIdAlgorithm algo = HardwareIdAlgorithm.MotherBoardOnly;
				string machineId = ID_Motherboard;
				if (!string.IsNullOrEmpty(machineId)) machineIds.Add("M-" + Convert.ToBase64String(shaM.ComputeHash(AE.GetBytes(machineId))));

				algo = HardwareIdAlgorithm.HarddriveOnly;
				machineId = ID_HardDrive;
				if (!string.IsNullOrEmpty(machineId)) machineIds.Add("H-" + Convert.ToBase64String(shaM.ComputeHash(AE.GetBytes(machineId))));

				algo = HardwareIdAlgorithm.Motherboard_And_Bios;
				machineId = ID_Bios + ID_Motherboard;
				if (!string.IsNullOrEmpty(machineId)) machineIds.Add("MB-" + Convert.ToBase64String(shaM.ComputeHash(AE.GetBytes(machineId))));

				algo = HardwareIdAlgorithm.CPU_Only;
				machineId = ID_Cpu;
				if (!string.IsNullOrEmpty(machineId)) machineIds.Add("C-" + Convert.ToBase64String(shaM.ComputeHash(AE.GetBytes(machineId))));

				algo = HardwareIdAlgorithm.OS_Only;
				machineId = ID_OperatingSystem;
				if (!string.IsNullOrEmpty(machineId)) machineIds.Add("O-" + Convert.ToBase64String(shaM.ComputeHash(AE.GetBytes(machineId))));

				algo = HardwareIdAlgorithm.Registry;
				machineId = ID_Registry;
				if (!string.IsNullOrEmpty(machineId)) machineIds.Add("R-" + Convert.ToBase64String(shaM.ComputeHash(AE.GetBytes(machineId))));

				switch (algo)
				{
					case HardwareIdAlgorithm.CPU_Only:
						machineId = "C-" + machineId;
						break;
					case HardwareIdAlgorithm.HarddriveOnly:
						machineId = "H-" + machineId;
						break;
					case HardwareIdAlgorithm.Motherboard_And_Bios:
						machineId = "MB-" + machineId;
						break;
					case HardwareIdAlgorithm.MotherBoardOnly:
						machineId = "M-" + machineId;
						break;
					case HardwareIdAlgorithm.OS_Only:
						machineId = "O-" + machineId;
						break;
					case HardwareIdAlgorithm.Registry:
						machineId = "R-" + machineId;
						break;
					default:
						throw new NotImplementedException("HardwareID algo not handled yet.");
				}
				message = "";
			}
			else if (LicenseMonitor.CurrentLicense.Lic != null &&
				LicenseMonitor.CurrentLicense.Lic.Type == License.LicenseTypes.Trial &&
				LicenseMonitor.CurrentLicense.Lic.HardwareId.StartsWith("A-", true, System.Globalization.CultureInfo.InvariantCulture))
			{
				message = "";
				machineIds.Add(GetMachineIdAll());
			}
			else
			{
				message = "";
				machineIds.Add(GetMachineIdAll2());
			}
			return machineIds;
		}

		public static string GetMachineIdAll2()
		{
			ASCIIEncoding AE = new ASCIIEncoding();
			System.Security.Cryptography.SHA1 shaM = new System.Security.Cryptography.SHA1Managed();

			string machineId = "A2-" + Convert.ToBase64String(shaM.ComputeHash(AE.GetBytes(ID_Motherboard + ID_Bios + ID_Cpu + ID_OperatingSystem)));
			return machineId;
		}

		public static string GetMachineIdAll()
		{
			ASCIIEncoding AE = new ASCIIEncoding();
			System.Security.Cryptography.SHA1 shaM = new System.Security.Cryptography.SHA1Managed();

			string machineId = "A-" + Convert.ToBase64String(shaM.ComputeHash(AE.GetBytes(ID_Motherboard + ID_HardDrive + ID_Bios + ID_Cpu + ID_OperatingSystem + ID_Registry)));
			return machineId;
		}

		private static string ID_Registry
		{
			get
			{
				StringBuilder systemInfo = new StringBuilder();

				RegistryKey processorKey = Registry.LocalMachine.OpenSubKey(@"HARDWARE\DESCRIPTION\System\CentralProcessor\0");
				systemInfo.Append(processorKey.GetValue("Identifier"));
				systemInfo.Append(processorKey.GetValue("ProcessorNameString"));

				RegistryKey windowsKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
				systemInfo.Append(windowsKey.GetValue("ProductId"));

				systemInfo.Append(Environment.MachineName);
				systemInfo.Append(Environment.OSVersion);
				systemInfo.Append(Environment.SystemDirectory);

				try
				{
					foreach (ManagementObject physicalMedia in new ManagementClass("Win32_PhysicalMedia").GetInstances())
					{
						systemInfo.Append(physicalMedia["SerialNumber"]);
						break;
					}
				}
				catch (Exception) { }

				try
				{
					foreach (ManagementObject bios in new ManagementClass("Win32_BIOS").GetInstances())
					{
						systemInfo.Append(bios["Name"]);
						systemInfo.Append(bios["SerialNumber"]);
						break;
					}
				}
				catch (Exception) { }

				try
				{
					foreach (ManagementObject disk in new ManagementClass("Win32_DiskDrive").GetInstances())
					{
						systemInfo.Append(disk["Model"]);
						systemInfo.Append(disk["Signature"]);
						break;
					}
				}
				catch (Exception) { }
				return systemInfo.ToString();
			}
		}

		private static string GetID(string wmiClass, string[] wmiProperties)
		{
			string result = "";


			if (searcher2 == null)
			{
				System.Management.EnumerationOptions k = new System.Management.EnumerationOptions();
				k.DirectRead = true;
				k.EnumerateDeep = false;
				k.EnsureLocatable = false;
				k.Rewindable = false;
				k.ReturnImmediately = false;
				searcher2 = new System.Management.ManagementObjectSearcher();
				searcher2.Options = k;
			}
			searcher2.Query.QueryString = "select * from " + wmiClass;
			try
			{
				moc = searcher2.Get();
			}
			catch
			{
				return "";
			}
			foreach (System.Management.ManagementObject mo in moc)
			{
				bool found = false;

				foreach (string wmiProperty in wmiProperties)
				{
					if (mo[wmiProperty] != null)
					{
						found = true;
						result += mo[wmiProperty].ToString();
					}
				}
				if (found)
				{
					// All properties exist in the same ManagementObject
					break;
				}
			}
			return result;
		}

		private static string ID_Bios
		{
			get { return GetID("Win32_BIOS", new string[] { "Manufacturer", "SMBIOSBIOSVersion", "IdentificationCode", "SerialNumber", "ReleaseDate", "Version" }); }
		}

		private static string ID_Motherboard
		{
			get { return GetID("Win32_BaseBoard", new string[] { "Model", "Manufacturer", "Name", "SerialNumber" }); }
		}

		private static string ID_Cpu
		{
			get { return GetID("Win32_Processor", new string[] { "UniqueId", "ProcessorId", "Name", "Manufacturer" }); }
		}

		private static string ID_OperatingSystem
		{
			get { return GetID("Win32_OperatingSystem", new string[] { "SerialNumber" }); }
		}

		private static string ID_HardDrive
		{
			get
			{
				DriveInfo drive = new DriveInfo(0);
				return drive.SerialNumber;
			}
		}

	}
}
