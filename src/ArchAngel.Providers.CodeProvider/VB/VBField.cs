using System;
using System.Collections.Generic;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
    class VBField : BaseField
    {
        public VBField(BaseController controller) : base(controller)
        {
        }

        public VBField(BaseController controller, BaseDataType type, string name, string modifier) : base(controller, type, name, modifier)
        {
        }

        public VBField(BaseController controller, BaseBaseConstruct parentObject, CodeLanguage language, int nodeIndex) : base(controller, parentObject, language, nodeIndex)
        {
        }

        public VBField(BaseController controller, BaseBaseConstruct parentObject, CodeLanguage language) : base(controller, parentObject, language)
        {
        }

        public override string FullyQualifiedDisplayNameExtended
        {
            get { throw new System.NotImplementedException(); }
        }

        public override string DisplayNameExtended
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
