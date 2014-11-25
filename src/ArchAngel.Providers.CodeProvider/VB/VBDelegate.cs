using System;
using System.Collections.Generic;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
    class VBDelegate :BaseDelegate
    {
        public VBDelegate(BaseController controller) : base(controller)
        {
        }

        public VBDelegate(BaseController controller, string name, BaseDataType type, string modifier) : base(controller, name, type, modifier)
        {
        }

        public VBDelegate(BaseController controller, string name, List<string> modifiers, string genericType, IEnumerable<BaseParameter> parameters, BaseDataType returnType, CodeLanguage language, BaseBaseConstruct parentObject, int nodeIndex) : base(controller, name, modifiers, genericType, parameters, returnType, language, parentObject, nodeIndex)
        {
        }

        public VBDelegate(BaseController controller, string name, BaseDataType returnType) : base(controller, name, returnType)
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
