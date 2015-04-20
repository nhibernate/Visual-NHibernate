using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Xml;
using ActiproSoftware.ComponentModel;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Dom {

	/// <summary>
	/// Represents the cached documentation for an assembly.
	/// </summary>
	internal class AssemblyDocumentation : DisposableObject {

		#region Cache File Format
		/*
		
		 FILE FORMAT:
		 
		 Header (one instance)
		 - Int32: Version
		 - Int32: Hash code verification
		 - Int64: Date/time updated
		 - String: XML documentation file location
		 - Int64: XML documentation file size
		 - Int32: Index file position
		 - Int32: Entry count
		 
		 Index (one for each entry)
		 - Int32: Hash code
		 - Int32: File position
		 
		 Documentation Entry (one for each entry)
		 - String: Documentation key
		 - String: Documentation value
		  
		*/
		#endregion

		private string			path;
		private IndexEntry[]	indexEntries;
		private BinaryReader	reader;
		private ArrayList		recentlyUsedEntries	= new ArrayList();

		internal const int		MaxRecentlyUsedEntries	= 30;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INNER TYPES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
			
		#region CacheHeader Class

		/// <summary>
		/// Stores data for the cache header.
		/// </summary>
		internal class CacheHeader {

			internal int		Version;
			internal int		HashCodeVerification;
			internal long		LastWriteDateTime;
			internal string		XmlDocumentationLocation;
			internal long		XmlDocumentationSize;
			
			/// <summary>
			/// Returns whether the cache header is valid.
			/// </summary>
			/// <returns>
			/// <c>true</c> if the cache header is valid; otherwise, <c>false</c>.
			/// </returns>
			internal bool IsValid() {
				if (this.Version != AssemblyProjectContent.Version)
					return false;
				else if (this.HashCodeVerification != AssemblyProjectContent.HashCodeVerification)
					return false;
				else if (!File.Exists(this.XmlDocumentationLocation))
					return false;

				FileInfo fileInfo = new FileInfo(this.XmlDocumentationLocation);
				if (this.XmlDocumentationSize != fileInfo.Length)
					return false;
				else if (this.LastWriteDateTime != fileInfo.LastWriteTimeUtc.Ticks)
					return false;

				return true;
			}

		}

		#endregion

		#region DocumentationEntry Class

		/// <summary>
		/// Represents a documentation entry.
		/// </summary>
		internal class DocumentationEntry {

			internal string	Name;
			internal string	Value;
			
			/////////////////////////////////////////////////////////////////////////////////////////////////////
			// OBJECT
			/////////////////////////////////////////////////////////////////////////////////////////////////////
			
			/// <summary>
			/// Initializes a new instance of the <c>DocumentationEntry</c> class.
			/// </summary>
			/// <param name="name">The name of the entry.</param>
			/// <param name="value">The value of the entry.</param>
			public DocumentationEntry(string name, string value) {
				this.Name	= name;
				this.Value	= value;
			}

		}

		#endregion

		#region IndexEntry Class

		/// <summary>
		/// Represents an index entry.
		/// </summary>
		internal struct IndexEntry : IComparable {

			internal int	HashCode;
			internal int	FilePosition;
			
			/////////////////////////////////////////////////////////////////////////////////////////////////////
			// OBJECT
			/////////////////////////////////////////////////////////////////////////////////////////////////////
			
			/// <summary>
			/// Initializes a new instance of the <c>IndexEntry</c> class.
			/// </summary>
			/// <param name="hashCode">The hash code of the entry.</param>
			/// <param name="filePosition">The file position of the entry.</param>
			public IndexEntry(int hashCode, int filePosition) {
				this.HashCode		= hashCode;
				this.FilePosition	= filePosition;
			}
			
			/////////////////////////////////////////////////////////////////////////////////////////////////////
			// PUBLIC PROCEDURES
			/////////////////////////////////////////////////////////////////////////////////////////////////////
			
			/// <summary>
			/// Compares the current instance with another object of the same type.
			/// </summary>
			/// <param name="obj">An object to compare with this instance.</param>
			/// <returns>
			/// A 32-bit signed integer that indicates the relative order of the comparands.
			/// </returns>
			public int CompareTo(object obj) {
				return this.HashCode.CompareTo(((IndexEntry)obj).HashCode);
			}
			
		}
	
		#endregion

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>AssemblyDocumentation</c> class that uses raw XML documentation files.
		/// </summary>
		/// <param name="path">The path to the documentation.</param>
		public AssemblyDocumentation(string path) {
			// Initialize parameters
			this.path = path;
		}
		
		/// <summary>
		/// Initializes a new instance of the <c>AssemblyDocumentation</c> class that uses a cached documentation file.
		/// </summary>
		/// <param name="path">The path to the documentation.</param>
		/// <param name="indexEntries">The array of index entries.</param>
		/// <param name="reader">The <see cref="BinaryReader"/> to use for reading the XML data.</param>
		public AssemblyDocumentation(string path, IndexEntry[] indexEntries, BinaryReader reader) {
			// Initialize parameters
			this.indexEntries	= indexEntries;
			this.reader			= reader;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the documentation for the specified key.
		/// </summary>
		/// <param name="key">The documentation key to look up.</param>
		/// <returns>The documentation for the specified key.</returns>
		internal string GetDocumentation(string key) {
			// Scan the recently used list first
			for (int recentlyUsedIndex = recentlyUsedEntries.Count - 1; recentlyUsedIndex >= 0; recentlyUsedIndex--) {
				if (((DocumentationEntry)recentlyUsedEntries[recentlyUsedIndex]).Name == key)
					return ((DocumentationEntry)recentlyUsedEntries[recentlyUsedIndex]).Value;
			}

			// If using a documentation cache approach... 
			if ((indexEntries != null) && (reader != null)) {
				// Do a binary search for the key
				int hashCode = key.GetHashCode();
				int index = Array.BinarySearch(indexEntries, new IndexEntry(hashCode, 0));
				if (index >= 0) {
					// An entry was found although there might be others with the same key... move to the first
					while ((index > 0) && (indexEntries[index - 1].HashCode == hashCode)) {
						index--;
					}

					// Loop through the valid entries
					while ((index < indexEntries.Length) && (indexEntries[index].HashCode == hashCode)) {
						// Jump to the appropriate file position
						reader.BaseStream.Position = indexEntries[index].FilePosition;

						// If the keys match...
						if (key == reader.ReadString()) {
							// Return the documentation value
							DocumentationEntry documentationEntry = new DocumentationEntry(key, reader.ReadString());
							recentlyUsedEntries.Add(documentationEntry);
							if (recentlyUsedEntries.Count > AssemblyDocumentation.MaxRecentlyUsedEntries)
								recentlyUsedEntries.RemoveAt(0);
							return documentationEntry.Value;
						}
					}
				}
			}
			else if ((path != null) && (Path.GetExtension(path).ToLower() == ".xml") && (File.Exists(path))) {
				// Using a raw XML documentation file
				XmlDocument xmlDocumentation = new XmlDocument();
				xmlDocumentation.Load(path);
				XmlNode node = xmlDocumentation.SelectSingleNode(String.Format("doc/members/member[@name = '{0}']", key));
				if (node != null)
					return node.InnerXml;
			}

			return null;
		}

		/// <summary>
		/// Loads a <see cref="AssemblyDocumentation"/> from the specified path.
		/// </summary>
		/// <param name="dateTime">The last modification date/time of the documentation.</param>
		/// <param name="path">The path to the cached documentation file.</param>
		/// <returns>The <see cref="AssemblyDocumentation"/> that was loaded.</returns>
		internal static AssemblyDocumentation LoadFromCache(DateTime dateTime, string path) {
			// Quit if there is no file
			if (!File.Exists(path))
				return null;

			// Open the cache file
			FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
			BinaryReader reader = new BinaryReader(stream);

			// Check the version
			if (reader.ReadInt32() != AssemblyProjectContent.Version) {
				// Close the stream
				stream.Close();
				return null;
			}

			// Check the hash code verification
			if (reader.ReadInt32() != AssemblyProjectContent.HashCodeVerification) {
				// Close the stream
				stream.Close();
				return null;
			}

			// Check the date/time
			if (reader.ReadInt64() != dateTime.Ticks) {
				// Close the stream
				stream.Close();
				return null;
			}

			// Load XML documentation file location and size
			reader.ReadString();
			reader.ReadInt64();

			// Get the index file position and the entry count
			int indexFilePosition = reader.ReadInt32();
			int entryCount = reader.ReadInt32();

			// Load index entries
			stream.Position = indexFilePosition;
			IndexEntry[] indexEntries = new IndexEntry[entryCount];
			for (int index = 0; index < entryCount; index++)
				indexEntries[index] = new IndexEntry(reader.ReadInt32(), reader.ReadInt32());

			return new AssemblyDocumentation(path, indexEntries, reader);
		}
		
		/// <summary>
		/// Loads the <see cref="CacheHeader"/> from the specified cache file.
		/// </summary>
		/// <param name="path">The path to the cached reflection file.</param>
		/// <returns>The <see cref="CacheHeader"/> that was loaded.</returns>
		internal static CacheHeader LoadHeaderFromCache(string path) {
			// Quit if there is no file
			if (!File.Exists(path))
				return null;

			// Open the cache file
			using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read)) {
				using (BinaryReader reader = new BinaryReader(stream)) {
					CacheHeader header = new CacheHeader();
					header.Version = reader.ReadInt32();
					header.HashCodeVerification = reader.ReadInt32();
					header.LastWriteDateTime = reader.ReadInt64();
					header.XmlDocumentationLocation = reader.ReadString();
					header.XmlDocumentationSize = reader.ReadInt64();
					return header;
				}
			}
		}

		/// <summary>
		/// Saves XML documentation to a cache file.
		/// </summary>
		/// <param name="xmlDocumentationPath">The path to the file containing the XML documentation.</param>
		/// <param name="dateTime">The last modification date/time of the documentation.</param>
		/// <param name="xmlDocumentationSize">The size of the documentation.</param>
		/// <param name="cachedDocumentationPath">The path to the cached documentation file.</param>
		/// <returns>The <see cref="AssemblyDocumentation"/> that was created based on the documentation.</returns>
		internal static AssemblyDocumentation SaveToCache(string xmlDocumentationPath, DateTime dateTime, long xmlDocumentationSize, string cachedDocumentationPath) {
			ArrayList documentationEntries = new ArrayList();
			ArrayList indexEntries = new ArrayList();

			try {
				// Load the raw XML documenatation
				XmlDocument xmlDocumentation = new XmlDocument();
				xmlDocumentation.Load(xmlDocumentationPath);

				// Build the hashtable of entries
				XmlNodeList memberNodes = xmlDocumentation.SelectNodes("doc/members/member");
				foreach (XmlNode memberNode in memberNodes) {
					XmlNode node = memberNode.Attributes["name"];
					if (node != null) {
						string memberDocumentation = memberNode.InnerXml;
						if (memberDocumentation != null)
							documentationEntries.Add(new DocumentationEntry(node.InnerText, memberDocumentation));
					}
				}

				// Create the cache directory if necessary
				if (!Directory.Exists(Path.GetDirectoryName(cachedDocumentationPath)))
					Directory.CreateDirectory(Path.GetDirectoryName(cachedDocumentationPath));

				// Open a writer to the file
				using (FileStream stream = new FileStream(cachedDocumentationPath, FileMode.Create, FileAccess.Write, FileShare.None)) {
					using (BinaryWriter writer = new BinaryWriter(stream)) {
						// Write the header
						writer.Write(AssemblyProjectContent.Version);
						writer.Write(AssemblyProjectContent.HashCodeVerification);
						writer.Write(dateTime.Ticks);
						writer.Write(xmlDocumentationPath);
						writer.Write(xmlDocumentationSize);

						int indexPointerPosition = (int)stream.Position;
						writer.Write(0);  // Fill in later
						writer.Write(documentationEntries.Count);

						// Write the documentation entries
						foreach (DocumentationEntry entry in documentationEntries) {
							indexEntries.Add(new IndexEntry(entry.Name.GetHashCode(), (int)stream.Position));
							writer.Write(entry.Name);
							writer.Write(entry.Value);
						}

						// Sort the index entries on hashcode
						indexEntries.Sort();

						// Write the index entries
						int indexFilePosition = (int)stream.Position;
						foreach (IndexEntry entry in indexEntries) {
							writer.Write(entry.HashCode);
							writer.Write(entry.FilePosition);
						}

						// Fill in the index file position
						stream.Position = indexPointerPosition;
						writer.Write(indexFilePosition);
					}
				}
			}
			catch (Exception) {}

			// If the cache file is now available, load it
			if (File.Exists(cachedDocumentationPath))
				return AssemblyDocumentation.LoadFromCache(dateTime, cachedDocumentationPath);
			else
				return null;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Releases the unmanaged resources used by the object and optionally releases the managed resources.
		/// </summary>
		/// <param name="disposing">
		/// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources. 
		/// </param>
		/// <remarks>
		/// This method is called by the public <c>Dispose</c> method and the <c>Finalize</c> method. 
		/// <c>Dispose</c> invokes this method with the <paramref name="disposing"/> parameter set to <c>true</c>. 
		/// <c>Finalize</c> invokes this method with <paramref name="disposing"/> set to <c>false</c>.
		/// </remarks>
		protected override void Dispose(bool disposing) {
			if (disposing) {
				if (reader != null) {
					reader.Close();
					reader = null;
				}
				indexEntries = null;
				path = null;
				recentlyUsedEntries = null;
			}
		}
	
	}
}
