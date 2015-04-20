using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// This is for Eziriz .net Reactor, to tell it not to obfuscate the names of certain fields etc.
/// </summary>
public class DoNotObfuscate : Attribute { }

namespace ArchAngel.Providers.Database.Model
{
    [Serializable]
    public class ArchAngelEditorAttribute : Attribute
    {
        public bool CanHaveUserOption = false;

        public ArchAngelEditorAttribute(bool canHaveUserOption)
        {
            CanHaveUserOption = canHaveUserOption;
        }
    }
}
