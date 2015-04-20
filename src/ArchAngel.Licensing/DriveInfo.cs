using System;
using System.Runtime.InteropServices;

namespace ArchAngel.Licensing
{
	public class DriveInfo
	{
		private int _bufferSize;
		private DriveTypes _driveType;
		private string _firmware;
		private string _model;
		private int _numberCylinders;
		private int _numberHeads;
		private int _sectorsPerTrack;
		private string _serialNumber;
		private const int CREATE_NEW = 1;
		private const int DFP_RECEIVE_DRIVE_DATA = 0x7c088;
		private const int FILE_SHARE_READ = 1;
		private const int FILE_SHARE_WRITE = 2;
		private const int GENERIC_READ = -2147483648;
		private const int GENERIC_WRITE = 0x40000000;
		private const int INVALID_HANDLE_VALUE = -1;
		private const int OPEN_EXISTING = 3;
		private const int VER_PLATFORM_WIN32_NT = 2;

		public DriveInfo(int driveNumber)
		{
			int handle;
			this._driveType = DriveTypes.Unknown;
			SENDCMDINPARAMS sci = new SENDCMDINPARAMS();
			SENDCMDOUTPARAMS sco = new SENDCMDOUTPARAMS();
			if (Environment.OSVersion.Platform == PlatformID.Win32NT)
			{
				string file = @"\\.\PhysicalDrive" + driveNumber.ToString();
				handle = CreateFile(file, -1073741824, 3, 0, 3, 0, 0);
			}
			else
			{
				string _Vb_t_string_0 = @"\\.\Smartvsd";
				handle = CreateFile(_Vb_t_string_0, 0, 0, 0, 1, 0, 0);
			}
			if (handle != -1)
			{
				int returnSize;
				sci.DriveNumber = (byte)driveNumber;
				sci.BufferSize = Marshal.SizeOf(sco);
				sci.DriveRegs.DriveHead = (byte)(160 | (driveNumber << 4));
				sci.DriveRegs.Command = 0xec;
				sci.DriveRegs.SectorCount = 1;
				sci.DriveRegs.SectorNumber = 1;
				returnSize = 0;
				if (DeviceIoControl(handle, 0x7c088, sci, Marshal.SizeOf(sci), sco, Marshal.SizeOf(sco), ref returnSize, 0) != 0)
				{
					this._serialNumber = SwapChars(sco.IDS.SerialNumber);
					this._model = SwapChars(sco.IDS.ModelNumber);
					this._firmware = SwapChars(sco.IDS.FirmwareRevision);
					this._numberCylinders = sco.IDS.NumberCylinders;
					this._numberHeads = sco.IDS.NumberHeads;
					this._sectorsPerTrack = sco.IDS.SectorsPerTrack;
					this._bufferSize = sco.IDS.BufferSize * 0x200;
					if ((sco.IDS.GenConfig & 0x80) == 0x80)
					{
						this._driveType = DriveTypes.Removable;
					}
					else if ((sco.IDS.GenConfig & 0x40) == 0x40)
					{
						this._driveType = DriveTypes.Fixed;
					}
					else
					{
						this._driveType = DriveTypes.Unknown;
					}
				}
				CloseHandle(handle);
			}
		}

		[DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		private static extern int CloseHandle(int hObject);
		[DllImport("kernel32", EntryPoint = "CreateFileA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		private static extern int CreateFile(string lpFileName, int dwDesiredAccess, int dwShareMode, int lpSecurityAttributes, int dwCreationDisposition, int dwFlagsAndAttributes, int hTemplateFile);
		[DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		private static extern int DeviceIoControl(int hDevice, int dwIoControlCode, [In, Out] SENDCMDINPARAMS lpInBuffer, int nInBufferSize, [In, Out] SENDCMDOUTPARAMS lpOutBuffer, int nOutBufferSize, ref int lpBytesReturned, int lpOverlapped);
		private static string SwapChars(char[] chars)
		{
			int _Vb_t_i4_0 = chars.Length - 2;
			for (int i = 0; i <= _Vb_t_i4_0; i += 2)
			{
				Array.Reverse(chars, i, 2);
			}
			return new string(chars).Trim();
		}

		public int BufferSize
		{
			get
			{
				return this._bufferSize;
			}
		}

		public DriveTypes DriveType
		{
			get
			{
				return this._driveType;
			}
		}

		public string Firmware
		{
			get
			{
				return this._firmware;
			}
		}

		public string Model
		{
			get
			{
				return this._model;
			}
		}

		public int NumberCylinders
		{
			get
			{
				return this._numberCylinders;
			}
		}

		public int NumberHeads
		{
			get
			{
				return this._numberHeads;
			}
		}

		public int SectorsPerTrack
		{
			get
			{
				return this._sectorsPerTrack;
			}
		}

		public string SerialNumber
		{
			get
			{
				return this._serialNumber;
			}
		}

		[StructLayout(LayoutKind.Sequential, Size = 12)]
		private class DRIVERSTATUS
		{
			public byte DriveError;
			public byte IDEStatus;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
			public byte[] Reserved = new byte[2];
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
			public int[] Reserved2 = new int[2];
		}

		public enum DriveTypes
		{
			Fixed,
			Removable,
			Unknown
		}

		[StructLayout(LayoutKind.Sequential, Size = 8)]
		private class IDEREGS
		{
			public byte Features;
			public byte SectorCount;
			public byte SectorNumber;
			public byte CylinderLow;
			public byte CylinderHigh;
			public byte DriveHead;
			public byte Command;
			public byte Reserved;
		}

		[StructLayout(LayoutKind.Sequential)]
		private class IDSECTOR
		{
			public short GenConfig;
			public short NumberCylinders;
			public short Reserved;
			public short NumberHeads;
			public short BytesPerTrack;
			public short BytesPerSector;
			public short SectorsPerTrack;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
			public short[] VendorUnique = new short[3];
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
			public char[] SerialNumber = new char[20];
			public short BufferClass;
			public short BufferSize;
			public short ECCSize;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
			public char[] FirmwareRevision = new char[8];
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
			public char[] ModelNumber = new char[40];
			public short MoreVendorUnique;
			public short DoubleWordIO;
			public short Capabilities;
			public short Reserved1;
			public short PIOTiming;
			public short DMATiming;
			public short BS;
			public short NumberCurrentCyls;
			public short NumberCurrentHeads;
			public short NumberCurrentSectorsPerTrack;
			public int CurrentSectorCapacity;
			public short MultipleSectorCapacity;
			public short MultipleSectorStuff;
			public int TotalAddressableSectors;
			public short SingleWordDMA;
			public short MultiWordDMA;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x17e)]
			public byte[] Reserved2 = new byte[0x17e];
		}

		[StructLayout(LayoutKind.Sequential, Size = 0x20)]
		private class SENDCMDINPARAMS
		{
			public int BufferSize;
			public DriveInfo.IDEREGS DriveRegs = new DriveInfo.IDEREGS();
			public byte DriveNumber;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
			public byte[] Reserved = new byte[3];
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
			public int[] Reserved2 = new int[4];
		}

		[StructLayout(LayoutKind.Sequential)]
		private class SENDCMDOUTPARAMS
		{
			public int BufferSize;
			public DriveInfo.DRIVERSTATUS Status = new DriveInfo.DRIVERSTATUS();
			public DriveInfo.IDSECTOR IDS = new DriveInfo.IDSECTOR();
		}
	}
}