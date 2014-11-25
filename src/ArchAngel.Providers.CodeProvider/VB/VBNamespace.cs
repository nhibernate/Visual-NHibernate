using System;
using System.Collections.Generic;
using System.Text;
using ArchAngel.Providers.CodeProvider.CSharp;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
    class VBNamespace : BaseNamespace
    {
        public VBNamespace(VBController controller) : base(controller)
        {
        }

        public VBNamespace(VBController controller, VBBaseConstruct parentObject, string name, VBCodeRoot parentCodeRoot, CodeLanguage language, int nodeIndex) : base(controller, parentObject, name, parentCodeRoot, language, nodeIndex)
        {
        }

        public VBNamespace(BaseNamespace nsToCopyFrom) : base(nsToCopyFrom)
        {
        }

        public VBNamespace(VBController controller, string name) : base(controller, name)
        {
        }

        public override BaseConstruct[] SortedConstructs
        {
            get { throw new System.NotImplementedException(); }
        }

        public override string GetInnerText()
        {
            throw new System.NotImplementedException();
        }

        public override string GetOuterText()
        {
            throw new System.NotImplementedException();
        }

        public override void GetBodyText(StringBuilder sb)
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
