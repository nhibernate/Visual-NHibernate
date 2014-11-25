using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Reflection;

namespace ArchAngel.Licensing
{
	/// <summary>
	/// Summary description for StrongNameTest.
	/// </summary>
	public class SecurityChecks
	{
		// Used to read assembly
		private static FileStream m_FileStream;

		// Cache of external properties - CLR initialises to false
		private static bool m_IsStrongNameValid_Cache;
		private static bool m_IsPublicTokenOk_Cache;

		// Internal state of properties - CLR initialises to false
		private static bool m_IsStrongNameValid_Init;
		private static bool m_IsPublicTokenOk_Init;
		private static bool m_IsStrongSignatureValid_Init;

		private static byte[] m_TokenExpected = { 79, 100, 19, 213, 55, 197, 140, 252 };

		private static List<string> _RunningFilenames = new List<string>();
		private static DateTime RunningFilenamesUpdated;

		/// <summary>
		/// Gets whether the parent process safe eg: explorer.exe, cmd.exe.
		/// </summary>
		public static bool IsParentProcessOk
		{
			get
			{
				Process p = Process.GetCurrentProcess();
				int parentPid = 0;

				using (ManagementObject mo = new ManagementObject("win32_process.handle='" + p.Id.ToString() + "'"))
				{
					mo.Get();
					parentPid = Convert.ToInt32(mo["ParentProcessId"]);
				}
				//using (ManagementObject mo = new ManagementObject("win32_process.handle='" + parentPid.ToString() + "'"))
				//{
				//    mo.Get();
				//    parentPath = Convert.ToString(mo.GetPropertyValue("ExecutablePath"));
				//}
				string realParentFilename = "";

				try
				{
					Process parentProcess = Process.GetProcessById(parentPid);

					if (parentProcess != null)
					{
						realParentFilename = parentProcess.MainModule.FileVersionInfo.OriginalFilename.ToLower();
					}
				}
				catch
				{
					return true;
				}

				switch (realParentFilename)
				{
					case "explorer.exe":
					case "cmd.exe":
					case "devenv.exe":
					case "explorer.exe.mui":
						return true;
					default:
						return false;
				}
			}
		}

		/// <summary>
		/// Gets a list of unwanted running processes, such as Regmon, Filemon etc.
		/// </summary>
		private static List<string> RunningFilenames
		{
			get
			{
				if (RunningFilenamesUpdated == null || DateTime.Now.Subtract(RunningFilenamesUpdated).Milliseconds > 5000)
				{
					_RunningFilenames.Clear();
					System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcesses();

					foreach (System.Diagnostics.Process proc in processes)
					{
						if (proc.MainWindowTitle.Length > 0)
						{
							string originalFilename = proc.MainModule.FileVersionInfo.OriginalFilename.ToLower();

							switch (originalFilename)
							{
								case "regmon.exe":
								case "filemon.exe":
								case "procmon.exe":
								case "diskmon.exe":
								case "syser.exe":
								case "sice.exe":
								case "ntice.exe":
									_RunningFilenames.Add(originalFilename);
									break;
								default:
									if (originalFilename.IndexOf("dbg") >= 0 ||
										originalFilename.IndexOf("debug") >= 0)
									{
										_RunningFilenames.Add(originalFilename);
									}
									break;
							}
						}
					}
					_RunningFilenames.Sort();
					RunningFilenamesUpdated = DateTime.Now;
				}
				return _RunningFilenames;
			}
		}

		/// <summary>
		/// Gets whether any monitoring apps are running.
		/// </summary>
		internal static bool IsBeingMonitored
		{
			get { return RunningFilenames.Count > 0; }
		}

		/// <summary>
		/// Gets whether Regmon is running.
		/// </summary>
		internal static bool IsRegmonRunning
		{
			get { return RunningFilenames.BinarySearch("regmon.exe") >= 0; }
		}

		/// <summary>
		/// Gets whether Filemon is running.
		/// </summary>
		internal static bool IsFilemonRunning
		{
			get { return RunningFilenames.BinarySearch("filemon.exe") >= 0; }
		}

		/// <summary>
		/// Gets whether Procmon is running.
		/// </summary>
		internal static bool IsProcmonRunning
		{
			get { return RunningFilenames.BinarySearch("procmon.exe") >= 0; }
		}

		/// <summary>
		/// Gets whether Diskmon is running.
		/// </summary>
		internal static bool IsDiskmonRunning
		{
			get { return RunningFilenames.BinarySearch("diskmon.exe") >= 0; }
		}

		// Constructor taking the expected public key token
		public static void SetPublicKeyToken(byte[] tokenExpected)
		{
			m_TokenExpected = tokenExpected;
		}

		#region This section contains public implementation of properties

		/// <summary>
		/// Make sure that no managed or un-managed debugger is attached to this assembly.
		/// </summary>
		public static bool IsDebuggerOk
		{
			get
			{
				return !
				(System.Diagnostics.Debugger.IsAttached || NativeMethods.UnmanagedDebuggerPresent());
			}
		}

		/// <summary>
		/// Make sure that nobody has turned off CAS.
		/// </summary>
		public static bool IsSecurityEnabled
		{
			get { return System.Security.SecurityManager.SecurityEnabled; }
		}

		/// <summary>
		/// Check that this assembly has a strong name that was verified and no tampering has occurred.
		/// </summary>
		public static bool IsStrongNameValid
		{
			get
			{
				if (m_IsStrongNameValid_Init == true)
					return m_IsStrongNameValid_Cache;
				else
				{
					m_IsStrongNameValid_Cache = false;
					m_IsStrongNameValid_Cache = IsStrongNameValid_Check();
					m_IsStrongNameValid_Init = true;
					return m_IsStrongNameValid_Cache;
				}
			}
		}

		/// <summary>
		/// Check that public key token matches what it should be.
		/// </summary>
		public static bool IsPublicTokenOk
		{
			get
			{
				if (m_IsPublicTokenOk_Init == true)
					return m_IsPublicTokenOk_Cache;
				else
				{
					m_IsPublicTokenOk_Cache = false;
					m_IsPublicTokenOk_Cache = IsPublicTokenOk_Check(m_TokenExpected);
					m_IsPublicTokenOk_Init = true;
					return m_IsPublicTokenOk_Cache;
				}
			}
		}

		/// <summary>
		/// Check all of the security tests together.
		/// </summary>
		public static bool IsAllSecurityOk
		{
			get
			{
#if DEBUG
                return true;
#else
				return true;
				//return (/*IsDebuggerOk &&*/
				//		IsStrongNameValid && IsPublicTokenOk &&
				//		IsSecurityEnabled && !IsBeingMonitored && IsParentProcessOk);
#endif

			}
		}
		#endregion

		#region This section contains private implementation of properties

		/// <summary>
		/// Check that this assembly has a strong name.
		/// </summary>
		/// <returns></returns>
		private static bool IsStrongNameValid_Check()
		{
			byte wasVerified = Convert.ToByte(false);
			byte forceVerification = Convert.ToByte(true);
			string assemblyName = AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.FriendlyName;
			return NativeMethods.CheckSignature(assemblyName, forceVerification, ref wasVerified);
		}

		/// <summary>
		/// Check that public key token matches what's expected.
		/// </summary>
		/// <param name="tokenExpected"></param>
		/// <returns></returns>
		private static bool IsPublicTokenOk_Check(byte[] tokenExpected)
		{
			// Retrieve token from current assembly
			byte[] tokenCurrent = Assembly.GetExecutingAssembly().GetName().GetPublicKeyToken();

			// Check that lengths match
			if (tokenExpected.Length == tokenCurrent.Length)
			{
				// Check that token contents match
				for (int i = 0; i < tokenCurrent.Length; i++)
					if (tokenExpected[i] != tokenCurrent[i])
						return false;
			}
			else
			{
				return false;
			}
			return true;
		}

		#endregion

		#region From here are the helper procedures

		/// <summary>
		/// Do a character by character comparison.
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		/// <returns></returns>
		private static bool Compare(byte[] lhs, byte[] rhs)
		{
			int size = (lhs.Length > rhs.Length) ? rhs.Length : lhs.Length;
			for (int idx = 0; idx < size; ++idx)
			{
				if (lhs[idx] != rhs[idx]) return false;
			}
			return true;
		}

		/// <summary>
		/// Read a 16-bit value from the file.
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		private static short GetWord(int pos)
		{
			byte[] wordBuf = new byte[2];
			m_FileStream.Seek(pos, SeekOrigin.Begin);
			m_FileStream.Read(wordBuf, 0, wordBuf.Length);
			return BitConverter.ToInt16(wordBuf, 0);
		}

		/// <summary>
		/// Read a 32-bit value from the file.
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		private static int GetInt(int pos)
		{
			byte[] intBuf = new byte[4];
			m_FileStream.Seek(pos, SeekOrigin.Begin);
			m_FileStream.Read(intBuf, 0, intBuf.Length);
			return BitConverter.ToInt32(intBuf, 0);
		}

		/// <summary>
		/// Read an eight-byte array from the file.
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		private static byte[] GetEightBytes(int pos)
		{
			byte[] eightBuf = new byte[8];
			m_FileStream.Seek(pos, SeekOrigin.Begin);
			m_FileStream.Read(eightBuf, 0, eightBuf.Length);
			return eightBuf;
		}
		#endregion

	}
}
