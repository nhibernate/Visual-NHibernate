using System;
using System.Collections.Generic;
using System.Text;
using ArchAngel.Providers.CodeProvider.CSharp;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
    class VBIndexer : BaseIndexer
    {
        public VBIndexer(VBController controller) : base(controller)
        {
        }

        public VBIndexer(VBController controller, VBDataType type)
            : base(controller, type)
        {
        }

        public VBIndexer(VBController controller, VBBaseConstruct parentObject, VBDataType dataType, IEnumerable<BaseParameter> parameters, PropertyAccessor getAccessor, PropertyAccessor setAccessor, CodeLanguage language, int nodeIndex)
            : base(controller, parentObject, dataType, parameters, getAccessor, setAccessor, language, nodeIndex)
        {
        }

        public VBIndexer(VBIndexer iiToCopyFrom) : base(iiToCopyFrom)
        {
        }

        public override string FullyQualifiedDisplayName
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

        public override string FullyQualifiedIdentifer
        {
            get { throw new System.NotImplementedException(); }
        }

        public override string FullyQualifiedName
        {
            get { throw new System.NotImplementedException(); }
        }

        public override string DisplayName
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}
