using System;
using System.Collections.Generic;
using System.Text;
using ArchAngel.Providers.CodeProvider.CSharp;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
    class VBRegion : BaseRegion
    {
        public VBRegion(BaseController controller) : base(controller)
        {
        }

        public VBRegion(BaseController controller, string name, int index) : base(controller, name, index)
        {
        }

        public override IBaseConstruct Clone()
        {
            VBRegion clone = new VBRegion(null);
            CloneInto(clone);
            return clone;
        }

        protected override string ToStringInternal()
        {
           throw new NotImplementedException("Tostring not yet implemented");
        }
    }
}
