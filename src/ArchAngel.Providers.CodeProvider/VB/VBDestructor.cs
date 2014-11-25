using System;
using System.Collections.Generic;
using System.Text;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
    class VBDestructor :BaseDestructor
    {
        public VBDestructor(BaseController controller) : base(controller)
        {
        }

        public VBDestructor(BaseController controller, string name) : base(controller, name)
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

        public override bool IsExtern
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }
    }
}
