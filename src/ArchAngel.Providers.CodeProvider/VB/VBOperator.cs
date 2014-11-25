using ArchAngel.Providers.CodeProvider.CSharp;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
    class VBOperator : BaseOperator
    {
        public VBOperator(VBController controller) : base(controller)
        {
        }

        public VBOperator(VBController controller, string name, BaseDataType type, string modifier) : base(controller, name, type, modifier)
        {
        }

        public VBOperator(VBController controller, BaseConstruct parentObject, CodeLanguage language, int nodeIndex) : base(controller, parentObject, language, nodeIndex)
        {
        }

        public VBOperator(BaseOperator opToCopyFrom) : base(opToCopyFrom)
        {
        }

        public override IBaseConstruct Clone()
        {
            throw new System.NotImplementedException();
        }

        public override string GetOuterText()
        {
            throw new System.NotImplementedException();
        }

        public override string GetInnerText()
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
    }
}
