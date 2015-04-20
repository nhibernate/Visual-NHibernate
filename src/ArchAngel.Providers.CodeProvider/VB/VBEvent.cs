using System;
using System.Collections.Generic;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
    class VBEvent : BaseEvent
    {
        public VBEvent(BaseController controller) : base(controller)
        {
        }

        public VBEvent(BaseController controller, BaseDataType type, string name, string modifier) : base(controller, type, name, modifier)
        {
        }

        public VBEvent(BaseController controller, BaseBaseConstruct parentObject, CodeLanguage language, int nodeIndex) : base(controller, parentObject, language, nodeIndex)
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
