using System;

namespace Slyce.ITemplateConfig
{
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
	}

	public interface IOption
	{
		string VariableName
		{
			get;set;
		}
		string VarType
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

}
