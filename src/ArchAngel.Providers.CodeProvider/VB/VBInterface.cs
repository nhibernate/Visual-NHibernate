using System;
using System.Collections.Generic;
using System.Text;
using ArchAngel.Providers.CodeProvider.CSharp;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
    class VBInterface : BaseInterface
    {
        public VBInterface( VBController controller, string name, List<string> modifiers, string interfaceBase, CodeLanguage language, BaseBaseConstruct parentObject, int nodeIndex) : base(controller, name, modifiers, interfaceBase, language, parentObject, nodeIndex)
        {
        }

        public VBInterface(VBController controller, string name)
            : base(controller, name)
        {
        }

        public VBInterface(VBController controller, string name, string modifier)
            : base(controller, name, modifier)
        {
        }

        public VBInterface(VBInterface interfaceToCopyFrom) : base(interfaceToCopyFrom)
        {
        }

        public override string GetOuterText()
        {
            throw new System.NotImplementedException();
        }

        public override string GetInnerText()
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
