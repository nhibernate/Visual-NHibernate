using System;
using System.IO;
using System.Reflection;
using Slyce.ITemplate;

namespace Slyce.TemplateInfo
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
	}
}
