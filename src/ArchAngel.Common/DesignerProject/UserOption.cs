using System;
using System.Collections.Generic;
using ArchAngel.Designer.DesignerProject;
using ArchAngel.Interfaces.ITemplate;
using ArchAngel.Interfaces.TemplateInfo;
using Slyce.Common;

namespace ArchAngel.Common.DesignerProject
{
    [Serializable]
    public class UserOption
    {
    	public string VariableName = "";
        public string Category = "";
        public Type VarType = null;
        public string Text = "";
        public string Description = "";
        public readonly List<string> Values = new List<string>();
        public Type IteratorType = null;
        public bool ResetPerSession = false;

        public const string Default_ValidatorFunctionBody = "failReason = \"\";\nreturn true;";
        public const string Default_DisplayToUserFunctionBody = "return true;";
        public const string Default_DefaultValueFunctionBody = @"return default({0});";

        public UserOption()
        {
        }

        public UserOption(
            string variableName,
            string category,
            Type varType,
            string text,
            string description,
            IEnumerable<string> values,
            string defaultValueFunctionBody,
            Type iteratorType,
            string validatorFunctionBody,
            string displayToUserFunctionBody,
            bool resetPerSession)
        {
            VariableName = variableName;
            Category = category;
            VarType = varType;
            Text = text;
            Description = description;
            DefaultValueFunctionBody = defaultValueFunctionBody;
            IteratorType = iteratorType;
            ValidatorFunctionBody = validatorFunctionBody;
            DisplayToUserFunctionBody = displayToUserFunctionBody;
            ResetPerSession = resetPerSession;

            if (values != null)
                Values.AddRange(values);
        }

        public UserOption(
            string variableName,
            string category,
            string text,
            string description,
            IEnumerable<string> values,
            Type iteratorType,
            bool resetPerSession) 
                : this(variableName, category, typeof(string), text, description, values, 
                        Default_DefaultValueFunctionBody, iteratorType, Default_ValidatorFunctionBody, 
                        Default_DisplayToUserFunctionBody, resetPerSession) { }

    	private string defaultValueFunctionBody;
    	public string DefaultValueFunctionBody
    	{
    		get
    		{
				if(IsDefaultValueFunctionOverridden())
                    return defaultValueFunctionBody;

    			return string.Format(defaultValueFunctionBody, VarType.FullName);
    		}
    		set
    		{
    			defaultValueFunctionBody = value;
    		}
    	}

    	public string ValidatorFunctionBody { get; set; }

		public string DisplayToUserFunctionBody { get; set; }

		public IOption ToOption()
		{
			Option option = new Option();

			option.Category = Category;
			option.Description = Description;
			option.EnumValues = Values.ToArray();
		    option.IteratorName = IteratorType == null ? "" : IteratorType.FullName;
		    option.ResetPerSession = ResetPerSession;
			option.Text = Text;
			option.VariableName = VariableName;
			option.VarType = VarType;
            option.DefaultValueIsFunction = true; // Always true for options defined in a template

			return option;
		}

        public FunctionInfo GetDefaultValueFunction()
        {
            FunctionInfo function = new FunctionInfo(VariableName + "_DefaultValue", VarType, DefaultValueFunctionBody, false,
                                                     SyntaxEditorHelper.ScriptLanguageTypes.CSharp, Description, "", Category);

			if(IteratorType != null)
			{
				function.Parameters.Add(new ParamInfo(IteratorType.Name.ToLower(), IteratorType));
			}

            return function;
        }

        public FunctionInfo GetDisplayToUserFunction()
        {
            FunctionInfo function = new FunctionInfo(VariableName + "_DisplayToUser", typeof(bool), DisplayToUserFunctionBody, false,
                                                     SyntaxEditorHelper.ScriptLanguageTypes.CSharp, Description, "", Category);

			if (IteratorType != null)
			{
				function.Parameters.Add(new ParamInfo(IteratorType.Name.ToLower(), IteratorType));
			}

        	return function;
        }

        public FunctionInfo GetValidatorFunction()
        {
            FunctionInfo function = new FunctionInfo(VariableName + "_Validator", typeof(bool), ValidatorFunctionBody, false,
                                                     SyntaxEditorHelper.ScriptLanguageTypes.CSharp, Description, "", Category);

			if (IteratorType != null)
			{
				function.Parameters.Add(new ParamInfo(IteratorType.Name.ToLower(), IteratorType));
			}
        	function.Parameters.Add(new ParamInfo("failReason", typeof(string), "out"));
            return function;

        }

        public bool IsDefaultValueFunctionOverridden()
        {
            return Default_DefaultValueFunctionBody.Equals(defaultValueFunctionBody) == false;
        }

        public bool IsDisplayToUserFunctionOverridden()
        {
            return Default_DisplayToUserFunctionBody.Equals(DisplayToUserFunctionBody) == false;
        }

        public bool IsValidationFunctionOverridden()
        {
            return Default_ValidatorFunctionBody.Equals(ValidatorFunctionBody) == false;
        }
    }
}