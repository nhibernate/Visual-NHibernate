using System.Collections.Generic;
using ArchAngel.Providers.CodeProvider.CSharp;

namespace ArchAngel.Providers.CodeProvider.VB
{
    class VBStruct : BaseStruct
    {
        public VBStruct(VBController controller) : base(controller)
        {
        }

        public VBStruct(VBController controller, string name, string baseName, string genericType, string modifier) : base(controller, name, baseName, genericType, modifier)
        {
        }

        public VBStruct(VBController controller, VBBaseConstruct parentObject, string name, List<string> modifiers, CodeLanguage language, int nodeIndex) : base(controller, parentObject, name, modifiers, language, nodeIndex)
        {
        }

        public VBStruct(BaseStruct structToCopyFrom) : base(structToCopyFrom)
        {
        }

        public VBStruct(VBController controller, string name) : base(controller, name)
        {
        }

        public override string GetInnerText()
        {
            throw new System.NotImplementedException();
        }

        public override string GetOuterText()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString(bool includeBody)
        {
            throw new System.NotImplementedException();
        }

        protected override string ToStringInternal()
        {
            throw new System.NotImplementedException();
        }

        public override IBaseConstruct Clone()
        {
            throw new System.NotImplementedException();
        }
    }
}
