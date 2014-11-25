using ArchAngel.Providers.CodeProvider.CSharp;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
    class VBPropertyAccessor : BasePropertyAccessor
    {
        public VBPropertyAccessor(VBController controller) : base(controller)
        {
        }

        public VBPropertyAccessor(VBController controller, AccessorTypes accessorType)
            : base(controller, accessorType)
        {
        }

        public VBPropertyAccessor(VBController controller, string modifier)
            : base(controller, modifier)
        {
        }

        public VBPropertyAccessor(VBController controller, VBBaseConstruct parentObject, AccessorTypes accessorType, string text, CodeLanguage language)
            : base(controller, parentObject, accessorType, text, language)
        {
        }

        public VBPropertyAccessor(BasePropertyAccessor propToCopyFrom) : base(propToCopyFrom)
        {
        }

        public override string FullyQualifiedIdentifer
        {
            get { throw new System.NotImplementedException(); }
        }

        public override string DisplayName
        {
            get { throw new System.NotImplementedException(); }
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
