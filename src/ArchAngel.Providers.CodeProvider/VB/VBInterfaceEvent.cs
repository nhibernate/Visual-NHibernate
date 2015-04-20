using System;
using System.Collections.Generic;
using System.Text;
using ArchAngel.Providers.CodeProvider.CSharp;

namespace ArchAngel.Providers.CodeProvider.VB
{
    class VBInterfaceEvent : BaseInterfaceEvent
    {
        public VBInterfaceEvent(VBController controller) : base(controller)
        {
        }

        public VBInterfaceEvent(VBController controller, string name, VBDataType type, bool isNew) : base(controller, name, type, isNew)
        {
        }

        public VBInterfaceEvent(VBController controller, VBBaseConstruct parentObject, string name, VBDataType dataType, bool hasNewKeyword, CodeLanguage language, int nodeIndex) : base(controller, parentObject, name, dataType, hasNewKeyword, language, nodeIndex)
        {
        }

        public VBInterfaceEvent(VBInterfaceEvent eventToCopyFrom) : base(eventToCopyFrom)
        {
        }

        protected override bool CustomMergeStepInternal(BaseBaseConstruct user, BaseBaseConstruct newgen, BaseBaseConstruct prevgen)
        {
            throw new System.NotImplementedException();
        }

        public override IBaseConstruct Clone()
        {
            throw new System.NotImplementedException();
        }

        protected override void AddChildInternal(BaseBaseConstruct childBC)
        {
            throw new System.NotImplementedException();
        }

        protected override string ToStringInternal()
        {
            throw new System.NotImplementedException();
        }
    }
}
