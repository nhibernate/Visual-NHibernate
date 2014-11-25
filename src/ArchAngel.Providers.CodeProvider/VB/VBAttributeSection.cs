using System;
using System.Collections.Generic;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
    [Serializable]
    class VBAttributeSection : BaseAttributeSection
    {
        public VBAttributeSection(BaseController controller) : base(controller)
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
