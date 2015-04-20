using System;

namespace ArchAngel.Interfaces.ITemplate
{
	public interface IFile
	{
		string Name
		{
			get;
			set;
		}
		string StaticFileName
		{
			get;
			set;
		}
		string StaticFileSkipFunction
		{
			get;
			set;
		}

		string IteratorName
		{
			get;
			set;
		}
		string Id
		{
			get;
			set;
		}
	}

	public interface IFolder
	{
		string Name
		{
			get;
			set;
		}
		string IteratorName
		{
			get;
			set;
		}
		string Id
		{
			get;
			set;
		}
		IFolder[] SubFolders
		{
			get;
			set;
		}
		IScript[] Scripts
		{
			get;
			set;
		}
		IFile[] Files
		{
			get;
			set;
		}
	}

	public interface IOption
	{
		string VariableName
		{
			get;
			set;
		}
		Type VarType
		{
			get;
			set;
		}
		string Text
		{
			get;
			set;
		}
		string Description
		{
			get;
			set;
		}
		string Category
		{
			get;
			set;
		}
		string[] EnumValues
		{
			get;
			set;
		}
		string DefaultValue
		{
			get;
			set;
		}
		string IteratorName
		{
			get;
			set;
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
		string DisplayToUserFunction
		{
			get;
			set;
		}
		bool ResetPerSession
		{
			get;
			set;
		}
		bool? IsValidValue
		{
			get;
			set;
		}
		bool? DisplayToUserValue
		{
			get;
			set;
		}

		bool IsVirtualProperty { get; }
	}

	public interface IOutput
	{
		string Name
		{
			get;
			set;
		}
		IFolder RootFolder
		{
			get;
			set;
		}
	}

	public interface IProject
	{
		IOption[] Options
		{
			get;
			set;
		}
		IOutput[] Outputs
		{
			get;
			set;
		}
		string Name
		{
			get;
			set;
		}
		string Description
		{
			get;
			set;
		}
	}

	public interface IScript
	{
		string FileName
		{
			get;
			set;
		}
		string ScriptName
		{
			get;
			set;
		}
		string IteratorName
		{
			get;
			set;
		}
		string Id
		{
			get;
			set;
		}
	}

	public interface IUserOption
#if FAST_SERIALIZATION 
			: System.Runtime.Serialization.ISerializable
#endif
	{
		string Name
		{
			get;
			set;
		}
		Type DataType
		{
			get;
			set;
		}
		object Value
		{
			get;
			set;
		}
		IScriptBaseObject Owner
		{
			get;
			set;
		}
		bool IsValid(bool deepCheck, out string failReason);
		bool DisplayToUser { get; }
		object DefaultValue { get; }
		string Description { get; }
		string Text { get; }
#if FAST_SERIALIZATION 
		void GetObjectData(SerializationInfo info, StreamingContext context);
#endif
	}

	public enum FunctionTypes
	{
		DefaultValue,
		HelperOverride,
		Validate,
		DisplayToUser
	}

	public interface IDefaultValueFunction
	{
		Type ObjectType { get; set; }
		string PropertyName { get; set; }
		string FunctionName { get; }
		bool UseCustomCode { get; set; }
		FunctionTypes FunctionType { get; }
	}

}
