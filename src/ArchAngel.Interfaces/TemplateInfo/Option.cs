using System;

namespace ArchAngel.Interfaces.TemplateInfo
{
	[Serializable]
    public class Option : ITemplate.IOption
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
        private string m_displayToUserFunction = "";
        private bool m_resetPerSession = false;
        private bool? m_isValidValue = null;
        private bool? m_displayToUserValue = null;

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

		public string[] EnumValues
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

        public string DisplayToUserFunction
        {
            get { return m_displayToUserFunction; }
            set { m_displayToUserFunction = value; }
        }

        public bool ResetPerSession
        {
            get { return m_resetPerSession; }
            set { m_resetPerSession = value; }
        }

        public bool? IsValidValue
        {
            get { return m_isValidValue; }
            set { m_isValidValue = value; }
        }

        public bool? DisplayToUserValue
        {
            get { return m_displayToUserValue; }
            set { m_displayToUserValue = value; }
        }

		public bool IsVirtualProperty
		{
			get
			{
				return string.IsNullOrEmpty(IteratorName) == false;
			}
		}
	}
}