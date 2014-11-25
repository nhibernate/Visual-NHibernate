using System;
using System.Collections.Generic;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
    class VBAttribute : BaseAttribute
    {
        public VBAttribute(VBController controller) : base(controller)
        {
        }

        public override IBaseConstruct Clone()
        {
            throw new NotImplementedException();
        }

        protected override string ToStringInternal()
        {
            throw new NotImplementedException();
        }

        public override string FullyQualifiedName
        {
            get
            {
                throw new NotImplementedException("FullyQualifiedName");
            }
        }


        public override string FullyQualifiedIdentifer
        {
            get
            {
                throw new NotImplementedException("FullyQualifiedIdentifier");
            }
        }
    }
}
