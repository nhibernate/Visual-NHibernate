using System;
using System.Collections.Generic;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
    class VBConstructor : BaseConstructor
    {
        public VBConstructor(BaseController controller) : base(controller)
        {
        }

        public VBConstructor(BaseController controller, string name) : base(controller, name)
        {
        }

        public VBConstructor(BaseController controller, BaseBaseConstruct parentObject, CodeLanguage language, int nodeIndex) : base(controller, parentObject, language, nodeIndex)
        {
        }

        public VBConstructor(BaseController controller, BaseBaseConstruct parentObject, CodeLanguage language) : base(controller, parentObject, language)
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

        public override string BodyText
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }
    }
}
