using System;

namespace Slyce.ITemplate
{
	public interface IFile
	{
		string Name
		{
			get;set;
		}
	}

	public interface IFolder
	{
		string Name
		{
			get;set;
		}
		IFolder[] SubFolders
		{
			get;set;
		}
		IScript[] Scripts
		{
			get;set;
		}
		IFile[] Files
		{
			get;set;
		}
	}

	public interface IOption
	{
		string VariableName
		{
			get;set;
		}
		Type VarType
		{
			get;set;
		}
		string Text
		{
			get;set;
		}
		string Description
		{
			get;set;
		}
		string Category
		{
			get;set;
		}
		string[] Values
		{
			get;set;
		}
		string DefaultValue
		{
			get;set;
		}
		string IteratorName
		{
			get;set;
		}
        string ValidatorFunction
        {
            get;
            set;
        }
        bool DefaultValueIsFunction
        {
            get;
            set;
        }
        bool Enabled
        {
            get;
            set;
        }
        string DisplayToUser
        {
            get;
            set;
        }
        bool DisplayToUserIsFunction
        {
            get;
            set;
        }
	}

	public interface IOutput
	{
		string Name
		{
			get;set;
		}
		IFolder RootFolder
		{
			get;set;
		}
	}

	public interface IProject
	{
		IOption[] Options
		{
			get;set;
		}
		IOutput[] Outputs
		{
			get;set;
		}
		string Name
		{
			get;set;
		}
		string Description
		{
			get;set;
		}
	}

	public interface IScript
	{
		string FileName
		{
			get;set;
		}
		string ScriptName
		{
			get;set;
		}
		string IteratorName
		{
			get;set;
		}
	}

    public interface IUserOption
    {
        string Name
        {
            get;set;
        }
        Type DataType
        {
            get;set;
        }
        object Value
        {
            get;set;
        }
    }

    public enum FunctionTypes
    {
        DefaultValue,
        HelperOverride,
        Validate
    }

    public interface IDefaultValueFunction
    {    
        Type ObjectType {get;set;}
        string PropertyName{get;set;}
        string FunctionName{get;}
        bool UseCustomCode { get;set;}
        FunctionTypes FunctionType { get;}
    }

}
