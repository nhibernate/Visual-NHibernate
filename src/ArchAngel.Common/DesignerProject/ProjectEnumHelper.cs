using System;
using Slyce.Common;

namespace ArchAngel.Designer.DesignerProject
{
    public static class ProjectEnumHelper
    {
        public static string ScriptLanguageNameFromEnum(SyntaxEditorHelper.ScriptLanguageTypes scriptLanguage)
        {
            switch (scriptLanguage)
            {
                case SyntaxEditorHelper.ScriptLanguageTypes.CSharp: return "C#";
                case SyntaxEditorHelper.ScriptLanguageTypes.VbNet: return "VB.Net";
                default:
                    throw new NotImplementedException("Output language not handled yet: " + scriptLanguage);
            }
        }

        public static SyntaxEditorHelper.ScriptLanguageTypes ScriptLanguageEnumFromName(string scriptLanguageTypeName)
        {
            switch (scriptLanguageTypeName.ToLower())
            {
                case "c#":
                    return SyntaxEditorHelper.ScriptLanguageTypes.CSharp;
                case "vb.net":
                    return SyntaxEditorHelper.ScriptLanguageTypes.VbNet;
                default:
                    throw new NotImplementedException("Script language name not handled yet: " + scriptLanguageTypeName);
            }
        }
    }
}