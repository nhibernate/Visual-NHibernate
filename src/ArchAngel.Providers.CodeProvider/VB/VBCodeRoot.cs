using System;
using System.Collections.Generic;
using System.Text;
using ArchAngel.Providers.CodeProvider.CSharp;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
    class VBCodeRoot : BaseCodeRoot
    {
        public VBCodeRoot(BaseController controller) : base(controller)
        {
        }

        public override BaseController Controller
        {
            get { throw new System.NotImplementedException(); }
        }

        public override string UsingStatementsTextBlock
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public override ICodeRoot NewInstance()
        {
            throw new System.NotImplementedException();
        }

        public override IBaseConstruct CreateBaseConstruct(string code, IBaseConstruct originalConstruct)
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            throw new System.NotImplementedException();
        }

        public override string GetUsingTextBlock(BaseController tempController)
        {
            throw new System.NotImplementedException();
        }
    }
}
