using System.Collections.Generic;
using ArchAngel.Providers.CodeProvider.CSharp;

namespace ArchAngel.Providers.CodeProvider.VB
{
    class VBProperty : BaseProperty
    {
        public VBProperty(VBController controller) : base(controller)
        {
        }

        public VBProperty(VBController controller, string name, VBDataType dataType, string modifier) : base(controller, name, dataType, modifier)
        {
        }

        public VBProperty(VBController controller, VBBaseConstruct parentObject, CodeLanguage language, int nodeIndex) : base(controller, parentObject, language, nodeIndex)
        {
        }

        public VBProperty(BaseProperty propToCopyFrom) : base(propToCopyFrom)
        {
        }

        public VBProperty(VBController controller, VBBaseConstruct parentObject, string name, VBDataType dataType, List<string> modifiers, VBPropertyAccessor getAccessor, VBPropertyAccessor setAccessor, CodeLanguage language, int nodeIndex) : base(controller, parentObject, name, dataType, modifiers, getAccessor, setAccessor, language, nodeIndex)
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
