using System;
using System.Collections.Generic;
using System.Text;
using ArchAngel.Providers.CodeProvider.CSharp;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
    class VBInterfaceMethod :BaseInterfaceMethod
    {
        public VBInterfaceMethod(VBController controller, VBBaseConstruct parentObject, string name, VBDataType returnType, bool hasNewKeyword, IEnumerable<BaseParameter> parameters, CodeLanguage language, int nodeIndex)
            : base(controller, parentObject, name, returnType, hasNewKeyword, parameters, language, nodeIndex)
        {
        }

        public VBInterfaceMethod(VBController controller, string name)
            : base(controller, name)
        {
        }

        public VBInterfaceMethod(VBController controller, string name, VBDataType type)
            : base(controller, name, type)
        {
        }

        public VBInterfaceMethod(VBInterfaceMethod imToCopyFrom)
            : base(imToCopyFrom)
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
