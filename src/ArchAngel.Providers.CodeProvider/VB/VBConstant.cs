using System;
using System.Collections.Generic;
using System.Text;
using ArchAngel.Providers.CodeProvider.CSharp;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
    class VBConstant : BaseConstant
    {
        public VBConstant(BaseController controller, BaseBaseConstruct parentObject, CodeLanguage language, int nodeIndex) : base(controller, parentObject, language, nodeIndex)
        {
        }

        public VBConstant(BaseController controller) : base(controller)
        {
        }

        public VBConstant(BaseController controller, string name, DataType type, string expr) : base(controller, name, type, expr)
        {
        }

        public VBConstant(BaseController controller, string name, DataType dataType) : base(controller, name, dataType)
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
