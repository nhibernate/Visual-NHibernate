using System;
using System.IO;
using System.Reflection;
using Slyce.ITemplate;

namespace Slyce.ITemplate
{
	/// <summary>
	/// Summary description for File.
	/// </summary>
	[Serializable]
	public class File : Slyce.ITemplate.IFile
	{
		private string m_name;

		public string Name
		{
			get {return m_name;}
			set {m_name = value;}
		}

		public void Write(string path)
		{
			if (!Directory.Exists(Path.GetDirectoryName(path)))
			{
				throw new Exception("Directory does not exist: "+ Path.GetDirectoryName(path));
			}
			if (System.IO.File.Exists(path))
			{
				System.IO.File.Delete(path);
			}
			using (Stream input = Assembly.GetExecutingAssembly().GetManifestResourceStream(this.Name))
			{
				using (Stream output = new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.Write))
				{
					byte[] buffer = new byte[32768];
					int read;

					while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
					{
						output.Write(buffer, 0, read);
					}
				}
			}
		}
	}
}
