using System;

/// <summary>
/// This is for Eziriz .net Reactor, to tell it not to obfuscate the names of certain fields etc.
/// </summary>
public sealed class DotfuscatorDoNotRename : Attribute { }

/// <summary>
/// This is to disable DotFuscator Control Flow obfuscation
/// </summary>
public sealed class DotfuscatorDoNotAlterControlFlow : Attribute { }

/// <summary>
/// This is to disable DotFuscator removal of unused elements (ie: properties that are only accessed via reflection)
/// </summary>
public sealed class DotfuscatorDoNotRemove : Attribute { }

//namespace Slyce.Common
//{
//    [Serializable]
//    [AttributeUsage(AttributeTargets.All,
//                   AllowMultiple = true,
//                   Inherited = true)]
//    public sealed class ArchAngelEditorAttribute : Attribute
//    {
//        public bool CanHaveUserOption = false;
//        public string PropertiesWithDefaultValueFunctionality = "";

//        public ArchAngelEditorAttribute(bool canHaveUserOption)
//        {
//            CanHaveUserOption = canHaveUserOption;
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="canHaveUserOption"></param>
//        /// <param name="propertiesWithDefaultValueFunctionality">Comma-separated property names.</param>
//        public ArchAngelEditorAttribute(bool canHaveUserOption, string propertiesWithDefaultValueFunctionality)
//        {
//            CanHaveUserOption = canHaveUserOption;
//            PropertiesWithDefaultValueFunctionality = propertiesWithDefaultValueFunctionality;
//        }
//    }

//    [Serializable]
//    [AttributeUsage(AttributeTargets.All,
//                   AllowMultiple = true,
//                   Inherited = true)]
//    public sealed class DefaultCodeAttribute : Attribute
//    {
//        public string DefaultCode = "";
//        public string Description = "";

//        public DefaultCodeAttribute(string description, string defaultCode)
//        {
//            Description = description;
//            DefaultCode = defaultCode;
//        }

//    }

//}
