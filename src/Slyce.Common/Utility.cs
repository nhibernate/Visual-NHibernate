using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Ionic.Zip;
using Ionic.Zlib;
using Microsoft.Win32;
using System.Security;

namespace Slyce.Common
{
	/// <summary>
	/// Summary description for Utility.
	/// </summary>
	public class Utility
	{
		#region Enums
		public enum LineBreaks
		{
			/// <summary>
			/// "\r\n"
			/// </summary>
			Windows,
			/// <summary>
			/// "\n"
			/// </summary>
			Unix,
			/// <summary>
			/// "\r"
			/// </summary>
			Mac
		}
		public enum ZipCompressionLevels
		{
			None,
			BestCompression,
			BestSpeed,
			Default,
			Level0,
			Level1,
			Level2,
			Level3,
			Level4,
			Level5,
			Level6,
			Level7,
			Level8,
			Level9,
		}
		#endregion

		#region DLL Imports
		[SecuritySafeCritical]
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
		public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, Int32 wParam, Int32 lParam);

		[DllImport("user32.dll")]
		private static extern bool SetForegroundWindow(IntPtr hWnd);

		[DllImport("gdi32.dll")]
		private static extern bool BitBlt(
		IntPtr hdcDest, // handle to destination DC
		int nXDest, // x-coord of destination upper-left corner
		int nYDest, // y-coord of destination upper-left corner
		int nWidth, // width of destination rectangle
		int nHeight, // height of destination rectangle
		IntPtr hdcSrc, // handle to source DC
		int nXSrc, // x-coordinate of source upper-left corner
		int nYSrc, // y-coordinate of source upper-left corner
		Int32 dwRop // raster operation code
		);

		public const uint WM_SETREDRAW = 0x000B;
		#endregion

		public static bool InDesignMode = System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower().IndexOf("devenv") >= 0;
		//public static bool IsRunningUnderVisualStudio = System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower().IndexOf("vshost") >= 0;
		private static MD5 md5p;
		private static readonly object md5Lock = new object();
		//private static System.Net.WebProxy _DefaultProxy = null;
		private static Dictionary<Encoding, byte[]> _UnicodePreambles;
		private static readonly Dictionary<Control, Control> ShadedControls = new Dictionary<Control, Control>();
		private static readonly Dictionary<Control, Control> MessagePanelControls = new Dictionary<Control, Control>();

		public static string RemoveXmlEncodingHeader(string file)
		{
			string xml = File.ReadAllText(file);

			if (xml.IndexOf("<?xml", 0, 50, StringComparison.OrdinalIgnoreCase) >= 0)
			{
				int encodingEnd = xml.IndexOf('>');
				xml = xml.Remove(0, encodingEnd + 1).TrimStart('\r', '\n', ' ');
				string tempFile = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());
				File.WriteAllText(tempFile, xml, Encoding.Unicode);
				return tempFile;
			}
			return file;
		}

		/// <summary>
		/// Inplements code from System.Path.CheckInvalidPathChars()
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static bool PathIsValid(string path)
		{
			for (int i = 0; i < path.Length; i++)
			{
				int num2 = path[i];

				if (((num2 == 0x22) || (num2 == 60)) || (((num2 == 0x3e) || (num2 == 0x7c)) || (num2 < 0x20)))
					return false;
			}
			return true;
		}

		/// <summary>
		/// Reimplementation of Path.Combine to ensure we throw an exception witht he invalid path.
		/// </summary>
		/// <param name="path1"></param>
		/// <param name="path2"></param>
		/// <returns></returns>
		public static string PathCombine(string path1, string path2)
		{
			if ((path1 == null) || (path2 == null))
				throw new ArgumentNullException((path1 == null) ? "path1" : "path2");

			if (path2.Length == 0)
			{
				if (!PathIsValid(path1))
					throw new Exceptions.InvalidPathException(string.Format("Path has invalid characters: '{0}'", path1), path1);

				return path1;
			}
			if (path1.Length == 0)
			{
				if (!PathIsValid(path2))
					throw new Exceptions.InvalidPathException(string.Format("Path has invalid characters: '{0}'", path2), path2);

				return path2;
			}
			string combinedPath;

			char ch = path1[path1.Length - 1];

			if (((ch != Path.DirectorySeparatorChar) && (ch != Path.AltDirectorySeparatorChar)) && (ch != Path.VolumeSeparatorChar))
				combinedPath = (path1 + Path.DirectorySeparatorChar + path2);
			else
				combinedPath = (path1 + path2);

			if (!PathIsValid(combinedPath))
				throw new Exceptions.InvalidPathException(string.Format("Path has invalid characters: '{0}'", combinedPath), combinedPath);

			if (Path.IsPathRooted(path2))
				return path2;

			if (!PathIsValid(path1))
				throw new Exceptions.InvalidPathException(string.Format("Path has invalid characters: '{0}'", path1), path2);

			return combinedPath;
		}

		/// <summary>
		/// Gets the number of lines appearing in the text.
		/// </summary>
		/// <param name="text">Text to check.</param>
		/// <returns>Number of lines.</returns>
		public static int GetNumberOfLines(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return 0;
			}
			int numLines = 0;
			int index = 0;

			// Check for Windows and Unix style linebreaks first
			while ((index = text.IndexOf('\n', index)) >= 0)
			{
				numLines++;
				index += 1;
			}
			if (numLines == 0)
			{
				index = 0;
				// Maybe we are dealing with Mac style linebreaks, so check for those
				while ((index = text.IndexOf('\r', index)) >= 0)
				{
					numLines++;
					index += 1;
				}
			}
			return numLines;
		}

		public static string GetDescription(Enum en)
		{
			Type type = en.GetType();
			MemberInfo[] memInfo = type.GetMember(en.ToString());

			if (memInfo != null && memInfo.Length > 0)
			{
				object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

				if (attrs != null && attrs.Length > 0)
					return ((DescriptionAttribute)attrs[0]).Description;
			}

			return en.ToString();
		}

		public static void DisplayMessagePanel(Control control, string headerText)
		{
			DisplayMessagePanel(control, headerText, Controls.MessagePanel.ImageType.Hourglass);
		}

		/// <summary>
		/// Gets a string representation of the default value for a type.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static string GetDefaultValueAsString(Type type)
		{
			if (type == typeof(string))
				return "\"\"";
			if (type == typeof(bool))
				return "false";
			if (type == typeof(char))
				return "'\0'";
			if (type == typeof(byte) ||
				type == typeof(decimal) ||
				type == typeof(double) ||
				type == typeof(float) ||
				type == typeof(int) ||
				type == typeof(long) ||
				type == typeof(sbyte) ||
				type == typeof(short) ||
				type == typeof(uint) ||
				type == typeof(ulong) ||
				type == typeof(ushort))
				return "0";
			return "null";
		}

		public static void DisplayMessagePanel(Control control, string headerText, Controls.MessagePanel.ImageType imageType)
		{
			DisplayMessagePanel(control, headerText, "", imageType);
		}

		public static void DisplayMessagePanel(Control control, string headerText, string statusText, Controls.MessagePanel.ImageType imageType)
		{
			if (MessagePanelControls.ContainsKey(control))
			{
				// The control is already overlaid.
				((Controls.MessagePanel)MessagePanelControls[control]).StatusText = headerText;
				return;
			}

			// Makes sure we are working on the correct thread.
			if (control.InvokeRequired)
			{
				MethodInvoker mi = () => DisplayMessagePanel(control, headerText);
				control.Invoke(mi);
				return;
			}
			SuspendPainting(control);
			// We are overlaying a control
			Controls.MessagePanel panel = new Controls.MessagePanel(headerText);
			panel.StatusText = statusText;
			panel.Image = imageType;
			panel.Dock = DockStyle.Fill;
			control.Controls.Add(panel);
			panel.BringToFront();
			MessagePanelControls.Add(control, panel);
			ResumePainting(control);
		}

		public static void UpdateMessagePanelStatus(Control control, string statusText)
		{
			// Makes sure we are working on the correct thread.
			if (control.InvokeRequired)
			{
				MethodInvoker mi = () => DisplayMessagePanel(control, statusText);
				control.Invoke(mi);
				return;
			}

			if (MessagePanelControls.ContainsKey(control))
			{
				SuspendPainting(control);
				Controls.MessagePanel panel = (Controls.MessagePanel)MessagePanelControls[control];
				//SuspendPainting(panel);
				panel.StatusText = statusText;
				//ResumePainting(panel);
				ResumePainting(control);
				//((Controls.MessagePanel)MessagePanelControls[control]).StatusText = statusText;
			}
		}

		/// <summary>
		/// Removes the Message Panel from the given control. If no message panel is
		/// present, it does nothing.
		/// </summary>
		/// <param name="control"></param>
		public static void HideMessagePanel(Control control)
		{
			if (control.InvokeRequired)
			{
				MethodInvoker invoker = () => HideMessagePanel(control);
				control.Invoke(invoker);
				return;
			}

			if (!MessagePanelControls.ContainsKey(control))
			{
				return;
			}
			if (control.Controls.Contains(MessagePanelControls[control]))
			{
				control.Controls.Remove(MessagePanelControls[control]);
			}
			MessagePanelControls.Remove(control);
		}

		/// <summary>
		/// Creates an image overlay of the form, making the form look shaded.
		/// </summary>
		/// <param name="form"></param>
		/// 
		/// <param name="transparency">The transparency of the shading: 0 to 255</param>
		/// <param name="color">The colour of the shading.</param>
		public static void ShadeForm(Form form, int transparency, Color color)
		{
			return;

			if (form == null)
			{
				throw new InvalidOperationException("Form is null");
			}
			if (ShadedControls.ContainsKey(form))
			{
				// Don't shade forms that are already shaded.
				return;
			}
			// Note: form transparency only works in Windows 2000+. Earlier operating systems should use the commented-out code below.
			// See version matrix here: http://www.vb-helper.com/howto_net_os_version.html
			if (Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major >= 5)
			{
				Form shade = new Form();
				shade.ShowInTaskbar = false;
				shade.FormBorderStyle = FormBorderStyle.None;
				shade.BackColor = Color.Black;
				shade.Opacity = (double)transparency / 255;
				shade.Width = form.Width;
				shade.Height = form.Height;
				shade.Top = form.Location.Y;
				shade.Left = form.Location.X;
				shade.StartPosition = FormStartPosition.Manual;
				shade.Show(form);
				//SuspendPainting(form.Handle);
				//SuspendPainting(shade.Handle);
				ShadedControls.Add(form, shade);
				//ResumePainting();
			}
			else
			{
				Graphics g = Graphics.FromHwnd(form.Handle);

				if (g == null ||
					form.ClientSize.Width <= 0 ||
					form.ClientSize.Height <= 0)
				{
					return;
				}
				Image img = GetBitMapFromGraphicsObject(g, 0, 0, form.ClientSize.Width, form.ClientSize.Height);
				g.Dispose();

				using (Graphics g2 = Graphics.FromImage(img))
				{
					g2.CompositingQuality = CompositingQuality.HighSpeed;

					using (Brush brush = new SolidBrush(Color.FromArgb(transparency, color)))
					{
						g2.FillRectangle(brush, 0, 0, img.Width, img.Height);
					}
					g2.Dispose();
				}
				var pic = new PictureBox();
				pic.Name = "ShadedOverlay";
				pic.Image = img;
				pic.Left = 0;
				pic.Top = 0;
				pic.Width = form.ClientSize.Width;
				pic.Dock = DockStyle.Fill;
				form.Controls.Add(pic);
				pic.BringToFront();
				ShadedControls.Add(form, pic);
			}
		}

		/// <summary>
		/// Removes the shading of a form.
		/// </summary>
		/// <param name="control"></param>
		public static void UnShadeForm(Control control)
		{
			return;

			if (!ShadedControls.ContainsKey(control))
			{
				return;
			}
			// Note: form transparency only works in Windows 2000+. Earlier operating systems should use the commented-out code below.
			if (Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major >= 5 && typeof(Form).IsInstanceOfType(control))
			{
				if (ShadedControls.ContainsKey(control))
				{
					if (typeof(Form).IsInstanceOfType(ShadedControls[control]))
					{
						((Form)ShadedControls[control]).Close();
					}
				}
			}
			else
			{
				if (control.Controls.Contains(ShadedControls[control]))
				{
					control.Controls.Remove(ShadedControls[control]);
				}
			}
			ShadedControls.Remove(control);
		}

		/// <summary>
		/// Checks if the specified control is shaded.
		/// </summary>
		/// <param name="control">The control to check</param>
		/// <returns>True if the form has been shaded by the ShadeForm() method.</returns>
		public static bool ControlShaded(Control control)
		{
			return control.Controls.ContainsKey("ShadedOverlay");
		}

		/// <summary>
		/// Converts hex string to byte array.
		/// </summary>
		/// <param name="hexString"></param>
		/// <returns></returns>
		public static byte[] HexToData(string hexString)
		{
			if (hexString == null)
				return null;

			if (hexString.Length % 2 == 1)
				hexString = '0' + hexString; // Up to you whether to pad the first or last byte

			byte[] data = new byte[hexString.Length / 2];

			for (int i = 0; i < data.Length; i++)
				data[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);

			return data;
		}

		/// <summary>
		/// Gets the mangled type name for a generic type eg: List&lt;string&gt; becomes List`1[string]
		/// </summary>
		/// <param name="unmangeldTypeName"></param>
		/// <returns></returns>
		public static string GetMangledGenericTypeName(string unmangeldTypeName)
		{
			if (unmangeldTypeName.IndexOf('<') < 0)
			{
				return unmangeldTypeName;
			}
			string[] parts = unmangeldTypeName.Split('<');
			parts[1] = parts[1].Replace(">", "");
			string[] args = parts[1].Split(',');

			return string.Format("{0}`{1}[{2}]", parts[0], args.Length, parts[1]);
		}

		public static string GetDemangledGenericTypeName(Type type)
		{
			return GetDemangledGenericTypeName(type, new List<string>());
		}

		public static string GetDemangledGenericTypeName(Type type, List<string> namespaces)
		{
			if (type == null) return "";
			if (!type.IsGenericType)
			{
				string fullname = type.FullName;

				foreach (string ns in namespaces)
				{
					if (fullname.IndexOf(ns + ".") == 0)
					{
						return fullname.Replace(ns + ".", "").Replace("+", ".");
					}
				}
				return fullname.Replace("+", ".");
			}
			string genericPartFullname = string.Format("{0}<", type.FullName.Substring(0, type.FullName.IndexOf('`')));

			foreach (string ns in namespaces)
			{
				if (genericPartFullname.IndexOf(ns + ".") == 0)
				{
					genericPartFullname = genericPartFullname.Replace(ns + ".", "");
					break;
				}
			}
			StringBuilder mangledName = new StringBuilder(50);
			mangledName.Append(genericPartFullname);

			Type[] typeArgs = type.GetGenericArguments();

			for (int i = 0; i < typeArgs.Length; i++)
			{
				if (i > 0)
				{
					mangledName.Append(",");
				}
				mangledName.Append(GetDemangledGenericTypeName(typeArgs[i], namespaces));
			}
			mangledName.Append(">");
			return mangledName.ToString().Replace("+", ".");
		}

		public static string GetDemangledGenericTypeName(string mangledTypeName)
		{
			return GetDemangledGenericTypeName(mangledTypeName, new List<string>());
		}

		public static string GetDemangledGenericTypeName(string mangledTypeName, List<string> namespaces)
		{
			if (mangledTypeName.IndexOf("`") < 0)
			{
				foreach (string ns in namespaces)
				{
					if (mangledTypeName.IndexOf(ns + ".") == 0)
					{
						return mangledTypeName.Replace(ns + ".", "").Replace("+", ".");
					}
				}
				return mangledTypeName.Replace("+", ".");
			}
			int specialCharPos = mangledTypeName.IndexOf("`");
			int firstPos = mangledTypeName.IndexOf("[");
			int lastPos = mangledTypeName.LastIndexOf("]");
			string genericPartFullname = mangledTypeName.Substring(0, specialCharPos - 1);

			foreach (string ns in namespaces)
			{
				if (genericPartFullname.IndexOf(ns + ".") == 0)
				{
					genericPartFullname = genericPartFullname.Replace(ns + ".", "");
					break;
				}
			}
			string argPartFullname = mangledTypeName.Substring(firstPos, lastPos - firstPos - 1);

			foreach (string ns in namespaces)
			{
				if (argPartFullname.IndexOf(ns + ".") == 0)
				{
					argPartFullname = argPartFullname.Replace(ns + ".", "");
					break;
				}
			}
			string demangledName = string.Format("{0}<{1}>", genericPartFullname, argPartFullname);
			return demangledName.Replace("+", ".");
		}

		/// <summary>
		/// The filename will be used as the namespace of the compiled class, 
		/// and namespaces can only consist of letters and numbers, and the 
		/// first character must be a letter
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static bool IsValidFunctionName(string name)
		{
			for (int i = 0; i < name.Length; i++)
			{
				char character = name[i];

				if (i == 0 && !char.IsLetter(character))
				{
					return false;
				}
				if (!char.IsLetterOrDigit(character) && character != '_')
				{
					return false;
				}
			}
			return true;
		}

		public static string GetCamelCase(string text)
		{
			if (text.Length > 0)
			{
				return text[0].ToString().ToLower() + text.Substring(1);
			}
			return text;
		}

		public static void BringWindowToForeground(IntPtr handle)
		{
			SetForegroundWindow(handle);
		}

		public static Bitmap GetBitMapFromGraphicsObject(Graphics g, int left, int top, int width, int height)
		{
			const int SRCCOPY = 0xCC0020;

			Bitmap memImage = new Bitmap(width, height, g);
			Graphics memGraphic = Graphics.FromImage(memImage);
			IntPtr dc1 = g.GetHdc();
			IntPtr dc2 = memGraphic.GetHdc();
			BitBlt(dc2, 0, 0, width, height, dc1, 0, 0, SRCCOPY);
			g.ReleaseHdc(dc1);
			memGraphic.ReleaseHdc(dc2);
			memGraphic.Dispose();
			return memImage;
		}

		public static string RemoveTrailingLineBreaks(string text)
		{
			if (text.Length == 0)
			{
				return text;
			}
			string lastChar = text.Substring(text.Length - 1, 1);

			while (lastChar == "\r" || lastChar == "\n" || lastChar == "\t")
			{
				text = text.Remove(text.Length - 1, 1);
				lastChar = text.Substring(text.Length - 1, 1);
			}
			return text;
		}

		/// <summary>
		/// Safe version of System.IO.Directory.GetFiles() that skips folders with access restrictions.
		/// </summary>
		/// <param name="directory"></param>
		/// <param name="searchPattern"></param>
		/// <param name="searchOption"></param>
		/// <returns></returns>
		public static List<string> GetFiles(string directory, string searchPattern, SearchOption searchOption)
		{
			DirectoryInfo dir = new DirectoryInfo(directory);
			List<string> files = dir.GetFiles(searchPattern, SearchOption.TopDirectoryOnly).Select(f => f.FullName).ToList();

			if (searchOption == SearchOption.AllDirectories)
				foreach (DirectoryInfo d in dir.GetDirectories())
					if ((d.Attributes & FileAttributes.System) == 0) //Is not a system directory
						files.AddRange(GetFiles(d.FullName, searchPattern, searchOption));

			return files;
		}

		/// <summary>
		/// Deletes all files in the folder, except hidden files, if the flag is set.
		/// </summary>
		/// <param name="directory"></param>
		/// <param name="deleteHiddenFiles"></param>
		public static void DeleteDirectoryContentsBrute(string directory, bool leaveHiddenFiles)
		{
			if (!Directory.Exists(directory))
				return;

			foreach (string file in Directory.GetFiles(directory))
			{
				if (leaveHiddenFiles && IOHelper.FileIsHidden(file))
					continue;

				DeleteFileBrute(file);
			}
		}

		public static void DeleteDirectoryContentsBrute(string directory)
		{
			if (!Directory.Exists(directory))
				return;

			Array.ForEach(Directory.GetFiles(directory),
			  delegate(string path) { DeleteFileBrute(path); });
		}

		public static bool DeleteDirectoryBrute(string directory)
		{
			bool success = false;

			if (Directory.Exists(directory))
			{
				int tries = 0;

				while (!success && tries < 20)
				{
					try
					{
						if (Directory.Exists(directory))
						{
							ClearAttributes(directory);
							Directory.Delete(directory, true);
						}
						success = true;
					}
					catch (Exception e)
					{
						tries++;

						if (e is UnauthorizedAccessException && Directory.Exists(directory))
						{
							if (!Directory.Exists(directory))
								return true;

							DirectoryInfo dirInfo = new DirectoryInfo(directory);
							dirInfo.Attributes = FileAttributes.Normal;
						}
						else
							Thread.Sleep(80);
					}
				}
			}
			else
			{
				success = true;
			}
			return success;
		}

		public static void ClearAttributes(string directory)
		{
			if (Directory.Exists(directory))
			{
				foreach (string subDirectory in Directory.GetDirectories(directory))
				{
					DirectoryInfo dirInfo = new DirectoryInfo(directory);
					dirInfo.Attributes = FileAttributes.Normal;
					ClearAttributes(subDirectory);
				}
				foreach (string file in Directory.GetFiles(directory))
					File.SetAttributes(file, FileAttributes.Normal);
			}
		}

		public static bool DeleteFileBrute(string file)
		{
			bool success = false;

			if (File.Exists(file))
			{
				if (IOHelper.FileIsReadOnly(file))
				{
					FileInfo fileInfo = new FileInfo(file);
					fileInfo.IsReadOnly = false;
				}
				int tries = 0;

				while (!success && tries < 20)
				{
					try
					{
						File.Delete(file);
						success = true;
					}
					catch (UnauthorizedAccessException)
					{
						// Only check for ReadOnly state once per file
						if (tries == 0 && (File.GetAttributes(file) & FileAttributes.ReadOnly) != 0)
						{
							throw;
						}
						tries++;
						Thread.Sleep(80);
					}
					catch (Exception)
					{
						tries++;
						Thread.Sleep(80);
					}
				}
			}
			else
			{
				success = true;
			}
			return success;
		}

		public static int GetComboBoxMaxWidth(ComboBox box)
		{
			Graphics g = Graphics.FromHwnd(box.Handle);
			int maxWidth = 0;

			foreach (string text in box.Items)
			{
				maxWidth = Math.Max(maxWidth, (int)g.MeasureString(text, box.Font).Width);
			}
			return maxWidth;
		}

		//public static void SubmitError(string submitUrl, string applicationName, string version, string shortDescription, Exception e)
		//{
		//    using (ErrorReporting.frmSendReport form = new ErrorReporting.frmSendReport())
		//    {
		//        form.Show(applicationName, version, shortDescription, e, submitUrl);
		//    }
		//}

		public static string StandardizeLineBreaks(string text, LineBreaks lineBreak)
		{
			switch (lineBreak)
			{
				case LineBreaks.Mac:
					return StandardizeLineBreaks(text, "\r");
				case LineBreaks.Unix:
					return StandardizeLineBreaks(text, "\n");
				case LineBreaks.Windows:
					return StandardizeLineBreaks(text, "\r\n");
				default:
					throw new NotImplementedException("This LineBreak hasn't been coded yet.");
			}
		}

		public static string StandardizeLineBreaks(string text, string lineBreak)
		{
			if (text == null) { return text; }

			StringBuilder sb = new StringBuilder(text, text.Length + 1000);
			sb.Replace("\r\n", "\n").Replace("\r", "\n");

			if (lineBreak != "\n")
			{
				sb.Replace("\n", lineBreak);
			}
			return sb.ToString();
		}

		/// <summary>
		/// Sends form data to a web url.
		/// </summary>
		/// <param name="url"></param>
		/// <param name="dictionary"></param>
		/// <returns>True if successful, false otherwise.</returns>
		public static bool SendHttpPost(string url, StringDictionary dictionary)
		{
			System.Net.WebResponse response;
			return SendHttpPost(url, dictionary, out response);
		}

		public static string GetTextFromWebResponse(System.Net.WebResponse response)
		{
			StringBuilder sb = new StringBuilder(1000);

			using (Stream ReceiveStream = response.GetResponseStream())
			{
				Encoding encode = Encoding.GetEncoding("utf-8");
				// Pipe the stream to a higher level stream reader with the required encoding format. 

				using (StreamReader readStream = new StreamReader(ReceiveStream, encode))
				{
					Char[] read = new Char[256];
					int count = readStream.Read(read, 0, 256);

					while (count > 0)
					{
						// Dump the 256 characters on a string and display the string onto the console.
						sb.Append(new String(read, 0, count));
						count = readStream.Read(read, 0, 256);
					}
					readStream.Close();
				}
			}
			return sb.ToString();
		}

		/// <summary>
		/// Sends form data to a web url.
		/// </summary>
		/// <param name="url"></param>
		/// <param name="dictionary"></param>
		/// <returns>True if successful, false otherwise.</returns>
		/// <param name="response"></param>
		public static bool SendHttpPost(string url, StringDictionary dictionary, out System.Net.WebResponse response)
		{
			response = null;

			try
			{
				ASCIIEncoding encoding = new ASCIIEncoding();
				string postData = "";
				bool isFirst = true;

				foreach (string key in dictionary.Keys)
				{
					string amp = isFirst ? "" : "&";
					postData += string.Format("{0}{1}={2}", amp, key, System.Web.HttpUtility.UrlEncode(dictionary[key]));
					isFirst = false;
				}
				byte[] data = encoding.GetBytes(postData);

				// Prepare web request...
				System.Net.HttpWebRequest myRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
				myRequest.Timeout = 30000;
				myRequest.UseDefaultCredentials = true;
				myRequest.Proxy = Slyce.Common.Utility.WebProxy;
				//myRequest.KeepAlive = false;
				//myRequest.ProtocolVersion = System.Net.HttpVersion.Version10;
				//myRequest.Accept = "*/*";

				myRequest.Method = "POST";
				myRequest.ContentType = "application/x-www-form-urlencoded";
				myRequest.ContentLength = data.Length;

				using (Stream newStream = myRequest.GetRequestStream())
				{
					if (newStream != null)
					{
						// Send the data.
						newStream.Write(data, 0, data.Length);
						newStream.Close();

						// Wait for response
						response = myRequest.GetResponse();
						return true;
					}
					return false;
				}
			}
			catch (System.Net.WebException)
			{
				return false;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public static void CheckForNulls(object[] args, string[] names)
		{
			for (int i = 0; i < args.Length; i++)
			{
				if (args[i] == null)
				{
					throw new ArgumentNullException(names[i]);
				}
			}
		}

		/// <summary>
		/// Creates the required directory if it doesn't exist
		/// </summary>
		/// <param name="from"></param>
		/// <param name="to"></param>
		public static void FileCopy(string from, string to)
		{
			if (!Directory.Exists(Path.GetDirectoryName(to)))
			{
				Directory.CreateDirectory(Path.GetDirectoryName(to));
			}
			File.Copy(from, to, true);
		}

		public static string GetSingleDirectoryName(string dirPath)
		{
			if (dirPath.LastIndexOf(Path.DirectorySeparatorChar) < dirPath.Length - 1)
			{
				dirPath = dirPath + Path.DirectorySeparatorChar;
			}
			dirPath = Path.GetDirectoryName(dirPath);
			dirPath = dirPath.Substring(dirPath.LastIndexOf(Path.DirectorySeparatorChar) + 1);
			return dirPath;
		}

		/// <summary>
		/// Returns the MD5 checksum of a file.
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		public static string GetCheckSumOfFile(string filePath)
		{
			byte[] md5;
			lock (md5Lock)
			{
				md5 = MD5Provider.ComputeHash(ReadFileAsByteArray(filePath));
			}
			return ToHexa(md5);
		}

		/// <summary>
		/// Returns the MD5 checksum of a string.
		/// </summary>
		/// <param name="text">The text to run an MD5 hash over</param>
		/// <returns>The MD5 hash represented as a hexadecimal string.</returns>
		public static string GetCheckSumOfString(string text)
		{
			byte[] md5;
			lock (md5Lock)
			{
				md5 = MD5Provider.ComputeHash(Encoding.ASCII.GetBytes(text));
			}
			return ToHexa(md5);
		}

		/// <summary>
		/// Returns the MD5 checksum of a string.
		/// </summary>
		/// <param name="bytes">The bytes to run an MD5 hash over</param>
		/// <returns>The MD5 hash represented as a hexadecimal string.</returns>
		public static string GetCheckSumOfBytes(byte[] bytes)
		{
			lock (md5Lock)
			{
				byte[] md5 = MD5Provider.ComputeHash(bytes);
				return ToHexa(md5);
			}
		}


		/// <summary>
		/// Gets the checksum of all constituent files appended together. Can be used to determine whether a project
		/// has changed in any way. You can't do a checksum on the compiled binary, because it is different every time.
		/// </summary>
		/// <param name="solutionFile">Path of the *.sln file.</param>
		/// <returns></returns>
		public static string GetCheckSumOfDotNetSolution(string solutionFile)
		{
			List<string> projectFiles = GetProjectsInVisualStudioSolution(solutionFile);
			StringBuilder sb = new StringBuilder(10000);
			string tempFile = Path.GetTempFileName();

			foreach (string projectFile in projectFiles)
			{
				List<string> files = new List<string>(100) { projectFile };
				XmlDocument doc = new XmlDocument();
				doc.Load(projectFile);
				XmlNamespaceManager nsManager = new XmlNamespaceManager(doc.NameTable);
				nsManager.AddNamespace("ms", "http://schemas.microsoft.com/developer/msbuild/2003");

				foreach (XmlNode node in doc.SelectNodes("ms:Project/ms:ItemGroup/ms:Compile/@Include", nsManager))
				{
					files.Add(node.InnerText);
				}
				foreach (XmlNode node in doc.SelectNodes("ms:Project/ms:ItemGroup/ms:EmbeddedResource/@Include", nsManager))
				{
					files.Add(node.InnerText);
				}
				foreach (XmlNode node in doc.SelectNodes("ms:Project/ms:ItemGroup/ms:None/@Include", nsManager))
				{
					files.Add(node.InnerText);
				}
				string rootPath = Path.GetDirectoryName(projectFile);

				foreach (string file in files)
				{
					string filePath = Path.Combine(rootPath, file);
					sb.Append(ReadTextFile(filePath));
				}
				File.WriteAllText(tempFile, sb.ToString());
			}
			return GetCheckSumOfFile(tempFile);
		}

		/// <summary>
		/// Dump binary data in hexadecimal
		/// </summary>
		/// <param name="data">byte array</param>
		/// <returns>string in hexadecimal format</returns>
		private static string ToHexa(byte[] data)
		{
			StringBuilder sb = new StringBuilder(data.Length);

			for (int i = 0; i < data.Length; i++)
			{
				sb.AppendFormat("{0:X2}", data[i]);
			}
			return sb.ToString().ToLower();
		}

		/// <summary>
		/// Gets the project files that are in a Visual Studio 2005 solution.
		/// </summary>
		/// <param name="solutionFile">Path of the solution file (*.sln).</param>
		/// <returns></returns>
		public static List<string> GetProjectsInVisualStudioSolution(string solutionFile)
		{
			List<string> projectFiles = new List<string>();
			string solutionFolder = Path.GetDirectoryName(solutionFile);

			string[] slnFileLines = StandardizeLineBreaks(ReadTextFile(solutionFile), LineBreaks.Unix).Split('\n');

			foreach (string line in slnFileLines)
			{
				if (line.IndexOf("Project(\"{") >= 0)
				{
					string projectFile = line.Substring(line.IndexOf(",") + 1);
					projectFile = projectFile.Substring(0, projectFile.IndexOf(","));
					projectFile = projectFile.Replace("\"", "").Trim();
					projectFile = RelativePaths.RelativeToAbsolutePath(solutionFolder, projectFile);
					projectFiles.Add(projectFile);
				}
			}
			return projectFiles;
		}

		/// <summary>
		/// Returns 0 if the versions are the same, -1 if version1 &lt; version2, and +1 if version1 %gt; version2
		/// </summary>
		/// <param name="version1"></param>
		/// <param name="version2"></param>
		/// <returns></returns>
		public static int CompareFileVersions(System.Diagnostics.FileVersionInfo version1, System.Diagnostics.FileVersionInfo version2)
		{
			if (version1.FileMajorPart > version2.FileMajorPart) { return 1; }
			if (version1.FileMajorPart < version2.FileMajorPart) { return -1; }
			if (version1.FileMinorPart > version2.FileMinorPart) { return 1; }
			if (version1.FileMinorPart < version2.FileMinorPart) { return -1; }
			if (version1.FileBuildPart > version2.FileBuildPart) { return 1; }
			if (version1.FileBuildPart < version2.FileBuildPart) { return -1; }
			if (version1.FilePrivatePart > version2.FilePrivatePart) { return 1; }
			if (version1.FilePrivatePart < version2.FilePrivatePart) { return -1; }
			return 0;
		}

		public static void ZipFile(List<string> files, string strZipFileName)
		{
			ZipFile(files, strZipFileName, true);
		}

		public static void ZipFile(string folder, string strZipFileName)
		{
			string[] arrFiles = Directory.GetFiles(folder);
			List<string> files = new List<string>(arrFiles);
			ZipFile(files, strZipFileName, true);
		}

		public static void ZipFile(List<string> files, string strZipFileName, bool appendZipContents)
		{
			ZipFile(files, strZipFileName, ZipCompressionLevels.BestSpeed, appendZipContents);
		}

		public static void ZipFile(List<string> files, string strZipFileName, ZipCompressionLevels compression)
		{
			ZipFile(files, strZipFileName, compression, true);
		}

		public static void ZipFile(List<string> files, string strZipFileName, ZipCompressionLevels compression, bool appendZipContents)
		{
			if (files.Count == 0) { return; }

			string tempFolder = Path.Combine(Path.GetTempPath(), "SlyceTempZip");

			if (Directory.Exists(tempFolder))
			{
				DeleteDirectoryBrute(tempFolder);
				Directory.CreateDirectory(tempFolder);
				// Maybe the OS or anti-virus app denies access to the new 
				// folder for a while, so let's give it a chance to do its thing.
				Thread.Sleep(50);
			}

			if (appendZipContents && File.Exists(strZipFileName))
			{
				using (Ionic.Zip.ZipFile zip = Ionic.Zip.ZipFile.Read(strZipFileName))
				{
					zip.CompressionLevel = ConvertCompressionLevel(compression);

					DeleteDirectoryBrute(tempFolder);
					Directory.CreateDirectory(tempFolder);

					try
					{
						// Unzip to temp folder
						zip.ExtractAll(tempFolder);
					}
					catch (Exception)
					{
						// Sometimes, for unknown reasons we get access denied exceptions
						DeleteDirectoryBrute(tempFolder);
						Directory.CreateDirectory(tempFolder);
						// Maybe the OS or anti-virus app denies access to the new 
						// folder for a while, so let's give it a chance to do its thing.
						Thread.Sleep(1000);
						zip.ExtractAll(tempFolder);
					}
					// Delete the current Zip file
					try
					{
						DeleteFileBrute(strZipFileName);
					}
					catch (UnauthorizedAccessException)
					{
						if (File.Exists(strZipFileName) && (File.GetAttributes(strZipFileName) & FileAttributes.ReadOnly) != 0)
						{
							MessageBox.Show("Cannot save because file is readonly: " + strZipFileName, "Cannot Save - ReadOnly", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						}
						else
						{
							MessageBox.Show("Zip failed due to locked files. Try adding [" + tempFolder + "] to the exclusion list in your anti-virus application.", "Cannot Zip - Locked Files", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
						throw;
					}
				}
			}
			// Copy all new files to the temp folder, deleting existing file if it exists
			foreach (string file in files)
			{
				string newFile = Path.Combine(tempFolder, Path.GetFileName(file));
				DeleteFileBrute(newFile);

				try
				{
					File.Copy(file, newFile, true);
				}
				catch (Exception)
				{
					// Sometimes, for some reason the temp directory doesn't exist, 
					// and checking for its existence on the line above doesn't help either!
					if (!Directory.Exists(tempFolder))
					{
						Directory.CreateDirectory(tempFolder);
						// Maybe the OS or anti-virus app denies access to the new 
						// folder for a while, so let's give it a chance to do its thing.
						Thread.Sleep(1000);
						File.Copy(file, newFile, true);
					}
					else
					{
						throw;
					}
				}
			}

			// Zip to a new file, not over any existing file. We will copy over the top if we are successful
			string tempZipFilename = Path.Combine(Path.GetDirectoryName(strZipFileName),
													  "~" + Path.GetFileName(strZipFileName));

			using (ZipFile targetZip = new ZipFile(tempZipFilename))
			{
				targetZip.CompressionLevel = ConvertCompressionLevel(compression);

				// This adds all of the files in tempFolder to the root of the zip file.
				targetZip.AddDirectory(tempFolder);

				try
				{
					targetZip.Save(tempZipFilename);
					// Delete existing file
					if (File.Exists(strZipFileName))
					{
						DeleteFileBrute(strZipFileName);
					}
					File.Move(tempZipFilename, strZipFileName);
				}
				catch (Exception)
				{
					int numTries = 0;
					bool success = false;
					Exception innerEx = null;

					while (!success && numTries < 10)
					{
						numTries++;
						// Maybe the OS or anti-virus app denies access to the new 
						// folder for a while, so let's give it a chance to do its thing.
						Thread.Sleep(500);

						try
						{
							targetZip.Save(tempZipFilename);
							success = true;
						}
						catch (Exception ex2)
						{
							innerEx = ex2;
						}
					}
					if (!success)
					{
						if (File.Exists(strZipFileName) && (File.GetAttributes(strZipFileName) & FileAttributes.ReadOnly) != 0)
						{
							MessageBox.Show("Cannot save because file is readonly: " + strZipFileName, "Cannot Save - ReadOnly",
											MessageBoxButtons.OK, MessageBoxIcon.Warning);
						}
						else
						{
							MessageBox.Show(
								"Zip failed due to locked files. Try adding [" + tempFolder +
								"] to the exclusion list in your anti-virus application.", "Cannot Zip - Locked Files",
								MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
						if (innerEx != null)
						{
							throw innerEx;
						}
						else
						{
							throw;
						}
					}
					else
					{
						// Delete existing file
						if (File.Exists(strZipFileName))
						{
							DeleteFileBrute(strZipFileName);
						}
						File.Move(tempZipFilename, strZipFileName);
					}
				}
			}
		}

		private static CompressionLevel ConvertCompressionLevel(ZipCompressionLevels compression)
		{
			return (CompressionLevel)Enum.Parse(typeof(CompressionLevel), compression.ToString(), true);
		}

		public static void UnzipFile(string zipFile, string outputFolder)
		{
			if (!Directory.Exists(outputFolder))
				Directory.CreateDirectory(outputFolder);

			using (ZipFile zip = Ionic.Zip.ZipFile.Read(zipFile))
				foreach (ZipEntry e in zip)
					e.Extract(outputFolder, ExtractExistingFileAction.OverwriteSilently); // extract and overwrite
		}

		public static void UnzipIndividualFileEntry(string zipFile, string outputFolder, string fileToUnzip)
		{
			UnzipIndividualFileEntry(zipFile, outputFolder, new[] { fileToUnzip });
		}

		public static void UnzipIndividualFileEntry(string zipFile, string outputFolder, IEnumerable<string> filenamesToUnzip)
		{
			using (ZipFile zip = Ionic.Zip.ZipFile.Read(zipFile))
				foreach (ZipEntry e in zip)
					if (filenamesToUnzip.Contains(e.FileName))
						e.Extract(outputFolder, ExtractExistingFileAction.OverwriteSilently); // extract and overwrite
		}

		public static Assembly FindAssembly(string assemblyName, List<string> AssemblySearchPaths, string requestorName)
		{
			// Workaround for a problem in the VS Designer generated Settings class that will attempt to
			// load a non-existant assembly System.XmlSerializers when trying to serialize a StringCollection.
			if (assemblyName.StartsWith("System.XmlSerializers"))
				return null;

			string filenameWithoutExt;

			if (assemblyName.IndexOf(',') >= 0)
			{
				filenameWithoutExt = assemblyName.Substring(0, assemblyName.IndexOf(','));
			}
			else
			{
				filenameWithoutExt = assemblyName;
			}
			for (int i = 0; i < 2; i++)
			{
				if (i == 1)
				{
					if (filenameWithoutExt.IndexOf(".XmlSerializers") > 0)
					{
						filenameWithoutExt = filenameWithoutExt.Replace(".XmlSerializers", "");
					}
					else
					{
						break;
					}
				}
				foreach (string resolvePath in AssemblySearchPaths)
				{
					// Check for AAL files
					string filename = Path.Combine(resolvePath, filenameWithoutExt + ".AAT.DLL");

					if (File.Exists(filename))
					{
						return Assembly.LoadFrom(filename);
					}
					// Check for DLL files
					filename = Path.Combine(resolvePath, filenameWithoutExt + ".DLL");

					if (File.Exists(filename))
					{
						return Assembly.LoadFrom(filename);
					}
					// Check for EXE files
					filename = Path.Combine(resolvePath, filenameWithoutExt + ".EXE");

					if (File.Exists(filename))
					{
						return Assembly.LoadFrom(filename);
					}
				}
			}
			// Try to load from the GAC
			string gacPath = Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.System));
			gacPath = string.Format("{0}{1}assembly{1}gac{1}{2}", gacPath, Path.DirectorySeparatorChar, filenameWithoutExt);

			if (Directory.Exists(gacPath))
			{
				string[] subFolders = Directory.GetDirectories(gacPath);

				// Only load from GAC if one version of the file exists.
				// TODO: rethink what needs to be done if multiple versions exist
				if (subFolders.Length == 1)
				{
					gacPath = subFolders[0];
					string assemblyPath = Path.Combine(gacPath, filenameWithoutExt + ".dll");

					if (File.Exists(assemblyPath))
					{
						return Assembly.LoadFrom(assemblyPath);
					}
				}
				else if (subFolders.Length > 1)
				{
					throw new FileNotFoundException("Assembly failed to load because multiple versions of the following assembly were found in the GAC: " + filenameWithoutExt);
				}
			}
			string searchPaths = "";

			foreach (string searchPath in AssemblySearchPaths)
			{
				searchPaths += searchPath + Environment.NewLine;
			}
			if (MessageBox.Show(string.Format("The '{0}' assembly (.dll or .exe) can't be located. The following folders have been searched: \n\n{1}\nDo you want to locate this file?", assemblyName, searchPaths), requestorName + " - File not found", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
			{
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.FileName = filenameWithoutExt + "*";

				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					if (File.Exists(openFileDialog.FileName))
					{
						Assembly ass = Assembly.LoadFrom(openFileDialog.FileName);

						if (ass != null)
						{
							// Add this path to the search paths, because related assemblies might be in here as well.
							AssemblySearchPaths.Add(Path.GetDirectoryName(openFileDialog.FileName));
							return ass;
						}
					}
				}
			}
			throw new FileNotFoundException("Assembly could not be found: " + filenameWithoutExt);
		}


		/// <summary>
		/// Write a stream to a file.
		/// </summary>
		/// <param name="filePath"></param>
		/// <param name="input"></param>
		public static void WriteStreamToFile(Stream input, string filePath)
		{
			if (!Directory.Exists(Path.GetDirectoryName(filePath)))
			{
				throw new Exception("Directory doesn't exist");
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

		public static void WriteResourceToFile(Assembly assembly, string resourceName, string path)
		{
			string folder = Path.GetDirectoryName(path);

			if (!Directory.Exists(folder))
			{
				Directory.CreateDirectory(folder);
				//throw new Exception("Directory does not exist: " + Path.GetDirectoryName(path));
			}
			if (File.Exists(path))
			{
				DeleteFileBrute(path);
			}
			Stream stream = assembly.GetManifestResourceStream(resourceName);

			if (stream == null)
			{
				string[] resourceNames = assembly.GetManifestResourceNames();
				StringBuilder sb = new StringBuilder(resourceNames.Length * 100);
				sb.AppendLine("Embedded resource could not be located: " + resourceName);
				sb.AppendLine("");
				sb.AppendLine("Available Resources:");

				foreach (string resName in resourceNames)
				{
					sb.AppendLine(resName);
				}
				throw new Exception(sb.ToString());
			}
			WriteStreamToFile(stream, path);
		}

		public static void CopyDirectory(string source, string destination, bool overwrite)
		{
			// Create the destination folder if missing.
			if (!Directory.Exists(destination))
				Directory.CreateDirectory(destination);

			DirectoryInfo dirInfo = new DirectoryInfo(source);

			// Copy all files.
			foreach (FileInfo fileInfo in dirInfo.GetFiles())
				fileInfo.CopyTo(Path.Combine(destination, fileInfo.Name), overwrite);

			// Recursively copy all sub-directories.
			foreach (DirectoryInfo subDirectoryInfo in dirInfo.GetDirectories())
				CopyDirectory(subDirectoryInfo.FullName, Path.Combine(destination, subDirectoryInfo.Name), overwrite);
		}

		public static string ReadTextFile(string path)
		{
			if (!File.Exists(path))
			{
				throw new FileNotFoundException(path + " cannot be found.");
			}
			using (TextReader tr = new StreamReader(path))
			{
				try
				{
					return tr.ReadToEnd();
				}
				finally
				{
					tr.Close();
				}
			}
		}


		public static string ReadTextFileSafe(string path)
		{
			if (!File.Exists(path))
			{
				throw new FileNotFoundException(path + " cannot be found.");
			}
			Encoding enc = GetFileEncoding(path);

			using (TextReader tr = new StreamReader(path, enc, true))
			{
				string text = tr.ReadToEnd();
				return Encoding.UTF8.GetString(Encoding.Convert(enc, Encoding.UTF8, enc.GetBytes(text)));
			}
		}

		private static Dictionary<Encoding, byte[]> UnicodePreambles
		{
			get
			{
				if (_UnicodePreambles == null)
				{
					_UnicodePreambles = new Dictionary<Encoding, byte[]>();
					Encoding[] unicodeEncodings = { Encoding.BigEndianUnicode, Encoding.Unicode, Encoding.UTF8 };

					for (int i = 0; i < unicodeEncodings.Length; i++)
					{
						byte[] preamble = unicodeEncodings[i].GetPreamble();
						_UnicodePreambles.Add(unicodeEncodings[i], preamble);
					}
				}
				return _UnicodePreambles;
			}
		}

		/// <remarks>
		/// Do not make this public - we have a locking mechanism on this that should not be
		/// ignored. Hashing providers are not thread safe, so our single provider should not 
		/// be used concurrently.
		/// </remarks>
		private static MD5 MD5Provider
		{
			get
			{
				if (md5p == null)
				{
					md5p = MD5.Create();
				}
				return md5p;
			}
		}

		/// <summary>
		/// Return the Encoding of a text file.  Return Encoding.Default if no Unicode
		/// BOM (byte order mark) is found.
		/// </summary>
		/// <param name="fileName">Path of file to inspect.</param>
		/// <returns>The file's Encoding.</returns>
		public static Encoding GetFileEncoding(string fileName)
		{
			if (!File.Exists(fileName))
			{
				throw new FileNotFoundException(fileName + " cannot be found.");
			}
			using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
			{
				foreach (Encoding encoding in UnicodePreambles.Keys)
				{
					fileStream.Position = 0;
					byte[] preamble = UnicodePreambles[encoding];
					bool preamblesAreEqual = true;

					for (int j = 0; preamblesAreEqual && j < preamble.Length; j++)
					{
						preamblesAreEqual = preamble[j] == fileStream.ReadByte();
					}
					if (preamblesAreEqual)
					{
						return encoding;
					}
				}
			}
			return Encoding.Default;
		}

		public static byte[] ReadFileAsByteArray(string path)
		{
			byte[] data;

			using (FileStream fs = File.OpenRead(path))
			{
				data = new byte[fs.Length];
				fs.Position = 0;
				fs.Read(data, 0, data.Length);
			}
			return data;
		}

		public static bool StringsAreEqual(string string1, string string2, bool caseSensitive)
		{
			if (caseSensitive)
			{
				return (string.Compare(string1, string2, StringComparison.InvariantCulture) == 0);
			}
			return (string.Compare(string1, string2, StringComparison.InvariantCultureIgnoreCase) == 0);
		}

		/// <summary>
		/// Creates a relative path from one file or folder to another.
		/// </summary>
		/// <param name="fromDirectory">
		/// Contains the directory that defines the start of the relative path.
		/// </param>
		/// <param name="toPath">
		/// Contains the path that defines the endpoint of the relative path.
		/// </param>
		/// <returns>
		/// The relative path from the startdirectory to the end path
		/// </returns>
		/// <exception cref="ArgumentNullException"></exception>
		public static string RelativePathTo(string fromDirectory, string toPath)
		{
			if (fromDirectory == null)
				throw new ArgumentNullException("fromDirectory");
			if (toPath == null)
				throw new ArgumentNullException("toPath");

			bool isRooted = Path.IsPathRooted(fromDirectory) && Path.IsPathRooted(toPath);

			if (isRooted)
			{
				bool isDifferentRoot = string.Compare(Path.GetPathRoot(fromDirectory), Path.GetPathRoot(toPath), true) != 0;
				if (isDifferentRoot)
					return toPath;
			}

			StringCollection relativePath = new StringCollection();
			string[] fromDirectories = fromDirectory.Split(Path.DirectorySeparatorChar);
			string[] toDirectories = toPath.Split(Path.DirectorySeparatorChar);
			int length = Math.Min(fromDirectories.Length, toDirectories.Length);
			int lastCommonRoot = -1;

			// find common root

			for (int x = 0; x < length; x++)
			{
				if (string.Compare(fromDirectories[x], toDirectories[x], true) != 0)
					break;
				lastCommonRoot = x;
			}

			if (lastCommonRoot == -1)
				return toPath;

			// add relative folders in from path
			for (int x = lastCommonRoot + 1; x < fromDirectories.Length; x++)
			{
				if (fromDirectories[x].Length > 0)
					relativePath.Add("..");
			}

			// add to folders to path
			for (int x = lastCommonRoot + 1; x < toDirectories.Length; x++)
			{
				relativePath.Add(toDirectories[x]);
			}

			// create relative path
			string[] relativeParts = new string[relativePath.Count];
			relativePath.CopyTo(relativeParts, 0);

			string newPath = string.Join(Path.DirectorySeparatorChar.ToString(), relativeParts);
			return newPath;
		}

		/// <summary>
		/// Stop control redraw flickering by passing in window (gui-object) handle before performing
		/// screen updates. Call ResumePainting to resume normal painting operations.
		/// </summary>
		/// <param name="control"></param>
		[SecuritySafeCritical]
		public static void SuspendPainting(Control control)
		{
			if (!control.IsDisposed)
				SendMessage(control.Handle, WM_SETREDRAW, 0, 0);
		}

		/// <summary>
		/// Resumes normal painting operations.
		/// </summary>
		[SecuritySafeCritical]
		public static void ResumePainting(Control control)
		{
			if (!control.IsDisposed)
			{
				SendMessage(control.Handle, WM_SETREDRAW, 1, 0);
				control.Refresh();
			}
		}

		public static string RegistryRead(string applicationName, string regKey, bool userSpecific)
		{
			RegistryKey key = userSpecific ? Registry.CurrentUser : Registry.LocalMachine;
			key = key.OpenSubKey("SOFTWARE\\" + applicationName + "\\" + regKey, true);

			if (key != null)
			{
				try
				{
					if (key.GetValue(regKey) != null)
					{
						return key.GetValue(regKey).ToString();
					}
					return "";
				}
				catch
				{
					return "";
				}
			}
			return "";
		}

		public static void RegistryWrite(string applicationName, string regKey, string val, bool userSpecific)
		{
			RegistryKey key = userSpecific ? Registry.CurrentUser : Registry.LocalMachine;
			key = key.OpenSubKey(@"SOFTWARE\\" + applicationName + "\\" + regKey, true);

			if (key == null)
			{
				key = userSpecific ? Registry.CurrentUser : Registry.LocalMachine;
				key.CreateSubKey("SOFTWARE\\" + applicationName + "\\" + regKey);
				key = key.OpenSubKey("SOFTWARE\\" + applicationName + "\\" + regKey, true);
			}
			key.SetValue(regKey, val);
		}

		public static string ReadBufferAsString(byte[] buffer, int offset, int count, Encoding encoding)
		{
			byte[] data = new byte[count];
			Buffer.BlockCopy(buffer, offset, data, 0, count);
			return encoding.GetString(data);
		}

		public static object CloneObject(object objectToClone)
		{
			System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

			using (MemoryStream stream = new MemoryStream())
			{
				bFormatter.Serialize(stream, objectToClone);
				stream.Seek(0, SeekOrigin.Begin);
				Type type = objectToClone.GetType();
				object clone = bFormatter.Deserialize(stream);
				stream.Close();
				stream.Dispose();
				return clone;
			}
		}

		public static bool GetDefaultProxy(out string host, out int port)
		{
			host = "";
			port = 0;
			Uri uri = new Uri("http://www.internetactivation.com");
			System.Net.WebRequest myWebRequest = System.Net.WebRequest.Create(uri);
			myWebRequest.Proxy = WebProxy;

			if (!myWebRequest.Proxy.IsBypassed(uri))
			{
				Uri proxyUri = myWebRequest.Proxy.GetProxy(uri);
				host = proxyUri.Host;
				port = proxyUri.Port;
			}
			return !string.IsNullOrEmpty(host);
		}

		/// <summary>
		/// Tries 10 times to open a file and read the bytes from it,
		/// sleeping 20ms between each attempt. If all of those attempts fail,
		/// the last exception is rethrown.
		/// </summary>
		/// <param name="path">The file path to load from.</param>
		/// <returns></returns>
		public static byte[] GetTextFileContentsBrute(string path)
		{
			return GetTextFileContentsBrute(path, 20);
		}

		/// <summary>
		/// Tries 10 times to open a file and read the bytes from it,
		/// sleeping waitBetweenTries milliseconds between each attempt. If all of those attempts fail,
		/// the last exception is rethrown.
		/// </summary>
		/// <param name="path">The file path to load from.</param>
		/// <param name="waitBetweenTries"></param>
		/// <returns></returns>
		public static byte[] GetTextFileContentsBrute(string path, int waitBetweenTries)
		{
			for (int i = 0; i < 10; i++)
			{
				try
				{
					return File.ReadAllBytes(path);
				}
				catch (IOException)
				{
					Thread.Sleep(waitBetweenTries);
					if (i == 9)
						throw;
				}
			}
			throw new IOException("Could not get the file bytes, but no exception was thrown. This should never happen.");
		}

		/// <summary>
		/// Create a base64 encoded MD5 hash string of the supplied string.
		/// </summary>
		/// <param name="str">The string to hash.</param>
		/// <returns>The Base64 encoded MD5 hash of the input string.</returns>
		public static string GetMD5HashString(string str)
		{
			byte[] result = GetMD5FromString(str);

			// Convert into base64 encoding and return it.
			return Convert.ToBase64String(result);
		}


		/// <summary>
		/// Gets the 16 byte MD5 hash of the supplied string.
		/// </summary>
		/// <param name="str">The string to hash</param>
		/// <returns>The 16byte array representing the hash.</returns>
		public static byte[] GetMD5FromString(string str)
		{
			UnicodeEncoding enc = new UnicodeEncoding();

			// Convert the string to bytes using the UnicodeEncoding
			byte[] textBytes = enc.GetBytes(str);

			lock (md5Lock)
			{
				// Hash the string's bytes.
				return MD5Provider.ComputeHash(textBytes);
			}
		}

		/// <summary>
		/// Computes the MD5 hash of the given bytes using the stored MD5Provider. Converts it to a Base64 encoded string.
		/// </summary>
		/// <param name="bytes">The bytes to hash.</param>
		/// <returns>The Base64 encoded MD5 hash.</returns>
		public static string GetMD5HashString(byte[] bytes)
		{
			lock (md5Lock)
			{
				return Convert.ToBase64String(MD5Provider.ComputeHash(bytes));
			}
		}


		/// <summary>
		/// Reads the file at fullPath as a text file, creates an MD5 hash of it, and
		/// stores that as a Base64 encoded string in the file outputPath. If outputPath
		/// does not exist, it will be created.  This method is limited to files that are
		/// less that 2^32 bytes in size.
		/// </summary>
		/// <param name="fullPath">The path of the text file to create a hash for.</param>
		/// <param name="outputPath">The path of the file to write the base64 encodedMD5 hash to.</param>
		/// <returns>The base64 encoded MD5 hash.</returns>
		public static string CreateMD5HashFileForTextFile(string fullPath, string outputPath)
		{
			string fileText;
			using (StreamReader reader = new StreamReader(fullPath))
			{
				fileText = reader.ReadToEnd();
			}

			byte[] fileBytes = new UnicodeEncoding().GetBytes(fileText);

			byte[] resultBytes;

			lock (md5Lock)
			// Hash the bytes we just read using MD5
			{
				resultBytes = MD5Provider.ComputeHash(fileBytes);
			}

			// Convert bytes to Base64 encoded string.
			string resultString = Convert.ToBase64String(resultBytes);

			using (StreamWriter writer = new StreamWriter(outputPath, false))
			{
				writer.Write(resultString);
			}
			return resultString;
		}

		/// <summary>
		/// Checks the contents of the file at filename against the MD5 checksum
		/// stored in md5Filename. Returns true if either of the files does not exist,
		/// or if the base64 encoded MD5 string stored in md5Filename is different to
		/// the one generated from the contents of filename.
		/// </summary>
		/// <param name="filename">The file to check using the basse64 encoded MD5 hash in md5Filename</param>
		/// <param name="md5Filename">The file that contains the base64 encoded MD5 hash.</param>
		/// <returns>True if either file is non existant, or the MD5 hash of the first matches the hash stored in the second.</returns>
		public static bool HasFileChangedMD5(string filename, string md5Filename)
		{
			if (File.Exists(filename) == false || File.Exists(md5Filename) == false)
			{
				return true;
			}

			string fileText;
			string MD5Text;
			using (StreamReader reader = new StreamReader(filename),
				readerMD5 = new StreamReader(md5Filename))
			{
				fileText = reader.ReadToEnd();
				MD5Text = readerMD5.ReadToEnd();
			}

			string fileMD5 = GetMD5HashString(fileText);

			return fileMD5 != MD5Text;
		}

		public static IWebProxy WebProxy
		{
			get
			{
				System.Net.IWebProxy proxy = WebRequest.GetSystemWebProxy();
				proxy.Credentials = CredentialCache.DefaultCredentials;
				// TODO: Add checks for Firefox and Chrome proxies if IE proxy isn't valid: http://www.codeguru.com/csharp/csharp/cs_network/http/article.php/c16479
				//if (proxy.Credentials == null)
				//{

				//}
				return proxy;
			}
		}
	}
}
