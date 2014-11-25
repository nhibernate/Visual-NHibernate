using System;
using System.Collections.Generic;
using System.Text;
using ArchAngel.Providers.CodeProvider.CSharp;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
    class VBFunction : BaseFunction
    {
        public VBFunction(BaseController controller) : base(controller)
        {
        }

        public VBFunction(BaseController controller, string functionName, DataType returnType, BaseParameter param) : base(controller, functionName, returnType, param)
        {
        }

        public VBFunction(BaseController controller, string name) : base(controller, name)
        {
        }

        public VBFunction(BaseController controller, string name, BaseDataType type) : base(controller, name, type)
        {
        }

        public VBFunction(BaseController controller, BaseBaseConstruct parentObject, int nodeIndex) : base(controller, parentObject, nodeIndex)
        {
        }

        public VBFunction(BaseController controller, BaseBaseConstruct parentObject) : base(controller, parentObject)
        {
        }

        public VBFunction(BaseFunction funcToCopyFrom) : base(funcToCopyFrom)
        {
        }

        public override string DisplayNameExtended
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

        protected override string ToStringInternal()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString(bool includeBody)
        {
            throw new System.NotImplementedException();
        }

        public override IBaseConstruct Clone()
        {
            throw new System.NotImplementedException();
        }
    }
}
