using System;
using System.Collections.Generic;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
    class VBParameter : BaseParameter
    {
        public VBParameter(BaseController controller) : base(controller)
        {
        }

        public VBParameter(BaseController controller, string dataType, string name) : base(controller, dataType, name)
        {
        }

        public VBParameter(BaseBaseConstruct parentObject, CodeLanguage language) : base(parentObject, language)
        {
        }

        public VBParameter(BaseController controller, BaseBaseConstruct parentObject, string name, string dataType, CodeLanguage language) : base(controller, parentObject, name, dataType, language)
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
