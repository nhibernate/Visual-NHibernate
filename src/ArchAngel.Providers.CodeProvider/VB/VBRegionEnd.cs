using System;
using System.Collections.Generic;
using System.Text;
using ArchAngel.Providers.CodeProvider.CSharp;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
    class VBRegionEnd  :BaseRegionEnd
    {
        public VBRegionEnd(BaseController controller, int index) : base(controller, index)
        {
        }

        public VBRegionEnd(BaseRegionEnd reToCopyFrom) : base(reToCopyFrom)
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
