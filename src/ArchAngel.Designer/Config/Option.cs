using System;
//using CodeSpew.Model.ITemplateConfig;
//using Slyce.ITemplateConfig;

namespace Slyce.ITemplate
{
	/// <summary>
	/// Summary description for Option.
	/// </summary>
	[Serializable]
	public class Option : Slyce.ITemplate.IOption
	{
		public string m_variableName	= "";
		public string m_category		= "";
		public string m_varType			= "";
		public string m_text			= "";
		public string m_description		= "";
		public string m_defaultValue	= "";
		public string[] m_values		= new string[0];
		private string m_iteratorName	= "";

		public string VariableName
		{
			get {return m_variableName;}
			set {m_variableName = value;}
		}

		public string VarType
		{
			get {return m_varType;}
			set {m_varType = value;}
		}

		public string Text
		{
			get {return m_text;}
			set {m_text = value;}
		}

		public string Description
		{
			get {return m_description;}
			set {m_description = value;}
		}

		public string Category
		{
			get {return m_category;}
			set {m_category = value;}
		}

		public string[] Values
		{
			get {return m_values;}
			set {m_values = value;}
		}

		public string DefaultValue
		{
			get {return m_defaultValue;}
			set {m_defaultValue = value;}
		}

		public string IteratorName
		{
			get {return m_iteratorName;}
			set {m_iteratorName = value;}
		}


		public Option()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}
}