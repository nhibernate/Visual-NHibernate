using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;

namespace ArchAngel.NHibernateHelper
{
	public class NHibernateLoaderException : Exception
	{
		public struct HbmXmlFile
		{
			public HbmXmlFile(string filename, string errorText, string xml)
			{
				Filename = filename;
				ErrorText = errorText;
				Xml = xml;
			}

			public string Filename;
			public string ErrorText;
			public string Xml;
		}
		public readonly List<ValidationEventArgs> Errors;
		public List<HbmXmlFile> ErrorFiles;

		public NHibernateLoaderException(string message, List<HbmXmlFile> errorFiles, IEnumerable<ValidationEventArgs> errors)
			: base(message)
		{
			if (errors != null)
				Errors = errors.ToList();

			ErrorFiles = errorFiles;

			if (errorFiles != null)
				for (int i = 0; i < errorFiles.Count; i++)
					this.Data.Add("XML " + i.ToString(), string.Format("Errors: {0}\n\n{1}", errorFiles[i].ErrorText, errorFiles[i].Xml));
		}

		public string Filenames
		{
			get
			{
				if (ErrorFiles == null)
					return "";

				string result = "";

				foreach (var xmlFile in ErrorFiles)
					result += string.Format("{0} : {1}, ", xmlFile.Filename, xmlFile.ErrorText);

				return result.TrimEnd(' ', ',');
			}
		}
	}
}