using System;
using System.Collections;
using System.IO;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;

namespace Slyce.IntelliMerge
{
	/// <summary>
	/// Summary description for Utility.
	/// </summary>
	public class Utility
	{
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

		/// <summary>
		/// Returns the checksum of a file.
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		public static long GetCheckSum(string filePath)
		{
			long crc = 0;
			Fesersoft.Hashing.crc32 cr = new Fesersoft.Hashing.crc32();
			//System.IO.StreamReader sr = new StreamReader(filePath);
			using (System.IO.Stream st = new System.IO.FileStream(filePath, FileMode.Open))
			{
				crc = cr.CRC(st);
				st.Close();
			}
			return crc;
		}

		public static void ZipFile(ArrayList aryFileNames, string strZipFileName)
		{
			if (aryFileNames.Count == 0) { return; }

			Crc32 crc = new Crc32();
			using (ZipOutputStream s = new ZipOutputStream(File.Create(strZipFileName)))
			{
				s.SetLevel(6); // 0 - store only to 9 - means best compression

				foreach (string file in aryFileNames)
				{
					using (FileStream fs = File.OpenRead(file))
					{
						byte[] buffer = new byte[fs.Length];
						fs.Read(buffer, 0, buffer.Length);
						ZipEntry entry = new ZipEntry(Path.GetFileName(file));
						entry.DateTime = DateTime.Now;
						// set Size and the crc, because the information
						// about the size and crc should be stored in the header
						// if it is not set it is automatically written in the footer.
						// (in this case size == crc == -1 in the header)
						// Some ZIP programs have problems with zip files that don't store
						// the size and crc in the header.
						entry.Size = fs.Length;
						fs.Close();
						crc.Reset();
						crc.Update(buffer);
						entry.Crc = crc.Value;
						s.PutNextEntry(entry);
						s.Write(buffer, 0, buffer.Length);
					}
				}
				s.Finish();
				s.Close();
			}
		}

		public static void UnzipFile(string filePath, string outputFolder)
		{
			ZipInputStream s = new ZipInputStream(File.OpenRead(filePath));
			ZipEntry theEntry;

			while ((theEntry = s.GetNextEntry()) != null)
			{
				string fileName = Path.GetFileName(theEntry.Name);

				// create directory
				if (outputFolder.Length > 0 &&
					!Directory.Exists(outputFolder))
				{
					Directory.CreateDirectory(outputFolder);
				}

				if (fileName != String.Empty)
				{
					string unzippedFilePath = Path.Combine(outputFolder, theEntry.Name);

					if (File.Exists(unzippedFilePath))
					{
						File.Delete(unzippedFilePath);
					}
					FileStream streamWriter = File.Create(unzippedFilePath);
					int size = 2048;
					byte[] data = new byte[2048];

					while (true)
					{
						size = s.Read(data, 0, data.Length);

						if (size > 0)
						{
							streamWriter.Write(data, 0, size);
						}
						else { break; }
					}
					streamWriter.Close();
				}
			}
			s.Close();
		}

		public static void UnzipIndividualFileEntry(string filePath, string outputFolder, string entryName)
		{
            if (entryName.ToLower() == "model.csproj") { System.Diagnostics.Debugger.Break(); }
			ZipInputStream s = new ZipInputStream(File.OpenRead(filePath));
			ZipEntry theEntry;

			while ((theEntry = s.GetNextEntry()) != null)
			{
				string fileName = Path.GetFileName(theEntry.Name);

                if (Slyce.Common.Utility.StringsAreEqual(fileName, entryName, false))
				{

					// create directory
					if (outputFolder.Length > 0 &&
						!Directory.Exists(outputFolder))
					{
						Directory.CreateDirectory(outputFolder);
					}

					if (fileName != String.Empty)
					{
						string unzippedFilePath = Path.Combine(outputFolder, theEntry.Name);

						if (File.Exists(unzippedFilePath))
						{
							File.Delete(unzippedFilePath);
						}
						FileStream streamWriter = File.Create(unzippedFilePath);
						int size = 2048;
						byte[] data = new byte[2048];

						while (true)
						{
							size = s.Read(data, 0, data.Length);

							if (size > 0)
							{
								streamWriter.Write(data, 0, size);
							}
							else { break; }
						}
						streamWriter.Close();
					}
				}
			}
			s.Close();
		}

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


	}
}
