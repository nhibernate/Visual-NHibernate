using ArchAngel.Providers.CodeProvider.CSharp;

namespace ArchAngel.Providers.CodeProvider.VB
{
    class VBRegionStart : BaseRegionStart
    {
        public VBRegionStart(VBController controller, string name, int index) : base(controller, name, index)
        {
        }

        public VBRegionStart(VBRegionStart reToCopyFrom) : base(reToCopyFrom)
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
