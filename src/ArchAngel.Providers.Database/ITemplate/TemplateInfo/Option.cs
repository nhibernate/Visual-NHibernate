using System;
//using CodeSpew.Model.ITemplateConfig;
//using Slyce.ITemplateConfig;

namespace Slyce.TemplateInfo
{
	/// <summary>
	/// Summary description for Option.
	/// </summary>
	[Serializable]
	public class Option : Slyce.ITemplate.IOption
	{
		public string m_variableName	= "";
		public string m_category		= "";
		public Type m_varType			= null;
		public string m_text			= "";
		public string m_description		= "";
		public string m_defaultValue	= "";
		public string[] m_values		= new string[0];
		private string m_iteratorName	= "";
        private string m_validatorFunction = "";
        private bool m_defaultValueIsFunction = false;
        private bool m_enabled = true;
        private string m_displayToUser = "true";
        private bool m_displayToUserIsFunction = false;

		public string VariableName
		{
			get {return m_variableName;}
			set {m_variableName = value;}
		}

		public Type VarType
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

        public string ValidatorFunction
        {
            get { return m_validatorFunction; }
            set { m_validatorFunction = value; }
        }

        public bool DefaultValueIsFunction
        {
            get { return m_defaultValueIsFunction; }
            set { m_defaultValueIsFunction = value; }
        }

        public bool Enabled
        {
            get { return m_enabled; }
            set { m_enabled = value; }
        }

        public string DisplayToUser
        {
            get { return m_displayToUser; }
            set { m_displayToUser = value; }
        }

        public bool DisplayToUserIsFunction
        {
            get { return m_displayToUserIsFunction; }
            set { m_displayToUserIsFunction = value; }
        }

		public Option()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}
}