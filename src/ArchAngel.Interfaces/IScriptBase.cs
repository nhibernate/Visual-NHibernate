using Slyce.Common;
using ArchAngel.Interfaces.ITemplate;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace ArchAngel.Interfaces
{
	public interface IScriptBase : ISerializable
	{
		bool Enabled {get;set;}
        string AliasPlural { get;set;}
        bool IsUserDefined { get;}
        string Name { get;set;}
        string Alias { get;set;}
		System.Collections.Generic.List<IUserOption> UserOptions{get;set;}

        string AliasPluralDefault(IScriptBase scriptBase);
        bool AliasPluralValidate(IScriptBase scriptBase, out string failReason);
		void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext);
		void ResetDefaults();
        string AliasDefault(IScriptBase scriptBase);
        bool AliasValidate(IScriptBase scriptBase, out string failReason);
        bool NameValidate(IScriptBase scriptBase, out string failReason);
		void AddUserOption(IUserOption userOption);
        object GetUserOptionValue(string name);
		void SetExposedUserOptions(object obj);
        bool IsValid(bool deepCheck, out string failReason);
	}

}

