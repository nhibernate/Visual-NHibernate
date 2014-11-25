using System.Collections.Generic;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
    class VBClass :BaseClass
    {
        public VBClass(VBController controller, string name, List<string> modifiers, List<string> baseNames, CodeLanguage language, VBBaseConstruct parentObject, int nodeIndex) : base(controller, name, modifiers, baseNames, language, parentObject, nodeIndex)
        {
        }

        public VBClass(VBController controller, string name, List<string> modifiers, List<string> baseNames, CodeLanguage language, VBBaseConstruct parentObject) : base(controller, name, modifiers, baseNames, language, parentObject)
        {
        }

        public VBClass(VBController controller, string name) : base(controller, name)
        {
        }

        public VBClass(VBClass classToCopy) : base(classToCopy)
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
