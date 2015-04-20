using System;
using System.Collections.Generic;
using System.Text;
using ArchAngel.Providers.CodeProvider.CSharp;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
    class VBInterfaceAccessor : BaseInterfaceAccessor
    {
        public VBInterfaceAccessor(VBController controller) : base(controller)
        {
        }

        public VBInterfaceAccessor(VBController controller, AccessorTypes accessorType, string modifier) : base(controller, accessorType, modifier)
        {
        }

        public VBInterfaceAccessor(VBController controller, VBBaseConstruct parentObject, AccessorTypes accessorType, CodeLanguage language) : base(controller, parentObject, accessorType, language)
        {
        }

        public VBInterfaceAccessor(BaseInterfaceAccessor propToCopyFrom) : base(propToCopyFrom)
        {
        }

        public override string DisplayName
        {
            get { throw new System.NotImplementedException(); }
        }

        public override string FullyQualifiedIdentifer
        {
            get { throw new System.NotImplementedException(); }
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
