using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
    public abstract class VBBaseConstruct : BaseBaseConstruct
    {
        protected VBBaseConstruct(BaseController controller) : base(controller)
        {
        }

        public override string ToString()
        {
            throw new System.NotImplementedException();
        }
    }
}
