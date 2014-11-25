using System;
//using CodeSpew.Model.ITemplateConfig;
//using Slyce.ITemplateConfig;

namespace Slyce.ITemplate
{
	/// <summary>
	/// Summary description for Script.
	/// </summary>
	[Serializable]
	public class Script : Slyce.ITemplate.IScript
	{
		private string m_fileName;
		private string m_scriptName;
		private string m_iteratorName;

		public string FileName
		{
			get {return m_fileName;}
			set {m_fileName = value;}
		}
		public string ScriptName
		{
			get {return m_scriptName;}
			set {m_scriptName = value;}
		}
		public string IteratorName
		{
			get {return m_iteratorName;}
			set {m_iteratorName = value;}
		}

		public Script()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}
}
