using System.Runtime.Serialization;

namespace ArchAngel.Providers.Database.Model
{
    public interface IScriptBase : Interfaces.IScriptBaseObject
    {
        bool Enabled { get;set;}
        string AliasPlural { get;set;}
        bool IsUserDefined { get;}
        string Name { get;set;}
        string Alias { get;set;}
        string AliasPluralDefault(IScriptBase scriptBase);
        bool AliasPluralValidate(IScriptBase scriptBase, out string failReason);
        void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext);
        void ResetDefaults();
        string AliasDefault(IScriptBase scriptBase);
        bool AliasValidate(IScriptBase scriptBase, out string failReason);
        bool NameValidate(IScriptBase scriptBase, out string failReason);
        bool IsValid(bool deepCheck, out string failReason);
		string UniqueId { get; }
        string Description { get; set; }
    }
}
