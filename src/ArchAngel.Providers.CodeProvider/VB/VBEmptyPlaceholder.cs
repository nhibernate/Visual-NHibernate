using System;
using System.Collections.Generic;
using System.Text;
using ArchAngel.Providers.CodeProvider.CSharp;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
    class VBEmptyPlaceholder : BaseEmptyPlaceholder
    {
        public VBEmptyPlaceholder(BaseController controller) : base(controller)
        {
        }

        public VBEmptyPlaceholder(CSharpController controller, string name, int index) : base(controller, name, index)
        {
        }

        public override bool IsTheSame(BaseBaseConstruct comparisonObject)
        {
            throw new System.NotImplementedException();
        }

        public override bool IsTheSame(BaseBaseConstruct comparisonObject, ComparisonDepth depth)
        {
            throw new System.NotImplementedException();
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
