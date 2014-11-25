using ArchAngel.Providers.CodeProvider.CSharp;

namespace ArchAngel.Providers.CodeProvider.VB
{
    class VBInterfaceProperty : BaseInterfaceProperty
    {
        public VBInterfaceProperty(VBController controller, string name) : base(controller, name)
        {
        }

        public VBInterfaceProperty(VBController controller, string name, VBDataType type) : base(controller, name, type)
        {
        }

        public VBInterfaceProperty(VBController controller, VBBaseConstruct parentObject, string name, DataType dataType, InterfaceAccessor getAccessor, InterfaceAccessor setAccessor, bool hasNewKeyword, CodeLanguage language, int nodeIndex) : base(controller, parentObject, name, dataType, getAccessor, setAccessor, hasNewKeyword, language, nodeIndex)
        {
        }

        public VBInterfaceProperty(VBInterfaceProperty ipToCopyFrom) : base(ipToCopyFrom)
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
