using System.Collections.Generic;

namespace ArchAngel.Providers.CodeProvider.DotNet
{
	interface IVisibilityModifiers : IBaseConstruct
    {
        List<string> Modifiers { get; set; }
    }
}
