using System;
using System.Collections.Generic;
using System.Text;
using ArchAngel.Providers.CodeProvider.CSharp;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
    class VBEnumeration : BaseEnumeration
    {
        public VBEnumeration(BaseController controller, string name, List<string> modifiers, string enumBase, BaseConstruct parentObject, CodeLanguage language, int nodeIndex) : base(controller, name, modifiers, enumBase, parentObject, language, nodeIndex)
        {
        }

        public VBEnumeration(BaseController controller) : base(controller)
        {
        }

        public VBEnumeration(BaseController controller, string name) : base(controller, name)
        {
        }

        public override IBaseConstruct Clone()
        {
            throw new System.NotImplementedException();
        }

        protected override string ToStringInternal()
        {
            throw new System.NotImplementedException();
        }
    }
}
