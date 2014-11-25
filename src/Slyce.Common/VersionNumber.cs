using System;
using System.Collections.Generic;
using System.IO;

namespace Slyce.Common
{
	/// <summary>
	/// Represents an assembly version number. Useful for stamping files with the version number of the assembly that created them.
	/// Provides comparason methods so that version objects can be easily compared, and certain conditions checked.
	/// </summary>
	public class VersionNumber : IEquatable<VersionNumber>
	{
		private int major;
		private int minor;
		private int build;
		private int revision;

		/// <summary>
		/// Construct an empty VersionNumber, with all fields set to 0
		/// </summary>
		public VersionNumber()
		{
		}

		/// <summary>
		/// Constructs a new VersionNumber
		/// </summary>
		/// <param name="file">The file path.</param>
		public VersionNumber(string file)
		{
			System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(file);
			this.major = fvi.FileMajorPart;
			this.minor = fvi.FileMinorPart;
			this.build = fvi.FileBuildPart;
			this.revision = fvi.FilePrivatePart;
		}

		/// <summary>
		/// Constructs a new VersionNumber
		/// </summary>
		/// <param name="major">The Major version number.</param>
		/// <param name="minor">The Minor version number.</param>
		/// <param name="build">The build number.</param>
		/// <param name="revision">The revision number.</param>
		public VersionNumber(int major, int minor, int build, int revision)
		{
			this.major = major;
			this.minor = minor;
			this.build = build;
			this.revision = revision;
		}

		public static bool TryParse(string versionNumber, out VersionNumber output)
		{
			try
			{
				output = Parse(versionNumber);
				return true;
			}
			catch (Exception)
			{
				output = null;
				return false;
			}
		}

		public static VersionNumber Parse(string versionNumber)
		{
			if (versionNumber == null) throw new ArgumentNullException("versionNumber");

			string[] bits = versionNumber.Split('.');
			if (bits.Length != 4)
			{
				throw new ArgumentException(versionNumber + " is not a valid version number - too many or too little . characters");
			}

			VersionNumber vn = new VersionNumber();
			vn.major = int.Parse(bits[0]);
			vn.minor = int.Parse(bits[1]);
			vn.build = int.Parse(bits[2]);
			vn.revision = int.Parse(bits[3]);
			return vn;
		}

		/// <summary>
		/// The Major version number.
		/// </summary>
		public int Major
		{
			get { return major; }
			set { major = value; }
		}

		/// <summary>
		/// The Minor version number.
		/// </summary>
		public int Minor
		{
			get { return minor; }
			set { minor = value; }
		}
		/// <summary>
		/// The build number.
		/// </summary>
		public int Build
		{
			get { return build; }
			set { build = value; }
		}
		/// <summary>
		/// The revision number.
		/// </summary>
		public int Revision
		{
			get { return revision; }
			set { revision = value; }
		}

		///// <summary>
		///// Represents various options that can be used when comparing VersionNumber objects.
		///// </summary>
		//[Flags]
		//public enum ComparasonOptions
		//{
		//    /// <summary>
		//    /// Use the Major version number in the comparason.
		//    /// </summary>
		//    UseMajorVersion,
		//    /// <summary>
		//    /// Use the Minor version number in the comparason.
		//    /// </summary>
		//    UseMinorVersion,
		//    /// <summary>
		//    /// Use the Build number in the comparason.
		//    /// </summary>
		//    UseBuildNumber,
		//    /// <summary>
		//    /// Use the Revision in the comparason.
		//    /// </summary>
		//    UseRevision,
		//    /// <summary>
		//    /// Use all fields in the comparason.
		//    /// </summary>
		//    UseAll = (UseMajorVersion & UseMinorVersion & UseBuildNumber & UseRevision)
		//}

		///// <summary>
		///// Compares the given VersionNumber against this one to determine which one is greater.
		///// If this object is greater than the one passed, a positive result is returned. If they
		///// are equal, 0 is returned. If this is less than the one passed, a negative result is returned.
		///// </summary>
		///// <param name="number">The VersionNumber to compare against this one.</param>
		///// <returns>
		///// If this > number, a positive integer.
		///// If this == number, 0.
		///// If this < number, a negative integer
		///// </returns>
		//public int Compare(VersionNumber number)
		//{
		//    return Compare(number, ComparasonOptions.UseAll);
		//}

		/// <summary>
		/// Compares the given VersionNumber against this one to determine which one is greater,
		/// using the given ComparasonOptions to determine which fields to use.
		/// If this object is greater than the one passed, a positive result is returned. If they
		/// are equal, 0 is returned. If this is less than the one passed, a negative result is returned.
		/// </summary>
		/// <param name="number">The VersionNumber to compare against this one.</param>
		/// <returns>
		/// If this &gt; number, a positive integer.
		/// If this == number, 0.
		/// If this &lt; number, a negative integer
		/// </returns>
		public int Compare(VersionNumber number)//, ComparasonOptions options)
		{
			if (Equals(number))//, options))
				return 0;

			//if ((options & ComparasonOptions.UseMajorVersion) != 0)
			//{
			if (major != number.major)
			{
				return major - number.major;
			}
			//}
			//if ((options & ComparasonOptions.UseMinorVersion) != 0)
			//{
			if (minor != number.minor)
			{
				return minor - number.minor;
			}
			//}
			//if ((options & ComparasonOptions.UseBuildNumber) != 0)
			//{
			if (build != number.build)
			{
				return build - number.build;
			}
			//}
			//if ((options & ComparasonOptions.UseRevision) != 0)
			//{
			if (revision != number.revision)
			{
				return revision - number.revision;
			}
			//}

			return 0;
		}

		///// <summary>
		///// Compares the given VersionNumber for equality using the given ComparasonOptions.
		///// </summary>
		///// <param name="number">The VersionNumber object to compare against this one.</param>
		///// <param name="options">The ComparasonOptions to use when doing this comparason.</param>
		///// <returns>True if the objects represent the same version number.</returns>
		//public bool Equals(VersionNumber number, ComparasonOptions options)
		//{
		//    bool retVal = false;
		//    if (options == ComparasonOptions.UseAll)
		//        return Equals(number);
		//    if((options & ComparasonOptions.UseMajorVersion) != 0)
		//    {
		//        retVal |= major == number.major;
		//    }
		//    if ((options & ComparasonOptions.UseMinorVersion) != 0)
		//    {
		//        retVal |= minor == number.minor;
		//    }
		//    if ((options & ComparasonOptions.UseBuildNumber) != 0)
		//    {
		//        retVal |= build == number.build;
		//    }
		//    if ((options & ComparasonOptions.UseRevision) != 0)
		//    {
		//        retVal |= revision == number.revision;
		//    }
		//    return retVal;
		//}

		/// <summary>
		/// Compares the given VersionNumber for equality on every field.
		/// </summary>
		/// <param name="versionNumber">The VersionNumber object to compare against this one.</param>
		/// <returns>True if the objects represent the same version number.</returns>
		public bool Equals(VersionNumber versionNumber)
		{
			if (versionNumber == null) return false;
			if (major != versionNumber.major) return false;
			if (minor != versionNumber.minor) return false;
			if (build != versionNumber.build) return false;
			if (revision != versionNumber.revision) return false;
			return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as VersionNumber);
		}

		public override int GetHashCode()
		{
			int result = major;
			result = 29 * result + minor;
			result = 29 * result + build;
			result = 29 * result + revision;
			return result;
		}

		/// <summary>
		/// Returns a the list of files set to the path with the latest version (from the paths supplied).
		/// </summary>
		/// <param name="files">Files to set with latest version.</param>
		/// <returns></returns>
		public static List<string> GetLocationsWithLatestVersions(List<string> files)
		{
			// Ensure we are referencing the latest version of each file
			List<string> folders = new List<string>();
			List<string> filenames = new List<string>();
			List<string> assemblyLocations = new List<string>();

			foreach (string path in files)
			{
				folders.Add(Path.GetDirectoryName(path));
				filenames.Add(Path.GetFileName(path));
			}
			foreach (string filename in filenames)
			{
				string folderWithLatest = "";
				Slyce.Common.VersionNumber latestVersion = new Slyce.Common.VersionNumber(0, 0, 0, 0);

				foreach (string folder in folders)
				{
					string testPath = Path.Combine(folder, filename);

					if (File.Exists(testPath))
					{
						Slyce.Common.VersionNumber ver = new Slyce.Common.VersionNumber(testPath);

						//if (ver.Compare(latestVersion, Slyce.Common.VersionNumber.ComparasonOptions.UseAll) > 0)
						if (ver.Compare(latestVersion) > 0)
						{
							latestVersion = ver;
							folderWithLatest = folder;
						}
					}
				}
				assemblyLocations.Add(Path.Combine(folderWithLatest, filename));
			}
			return assemblyLocations;
		}
	}
}
