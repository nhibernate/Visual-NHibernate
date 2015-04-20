using System;
using System.Collections.Generic;
using System.Text;
using ArchAngel.Providers.CodeProvider.CSharp;
using ArchAngel.Providers.CodeProvider.DotNet;

namespace ArchAngel.Providers.CodeProvider.VB
{
    class VBInterfaceIndexer : BaseInterfaceIndexer
    {
        public VBInterfaceIndexer(VBController controller) : base(controller)
        {
        }

        public VBInterfaceIndexer(VBController controller, DataType type, bool isNew)
            : base(controller, type, isNew)
        {
        }

        public VBInterfaceIndexer(VBController controller, VBBaseConstruct parentObject, VBDataType dataType, bool hasNewKeyword, IEnumerable<BaseParameter> parameters, BaseInterfaceAccessor getAccessor, BaseInterfaceAccessor setAccessor, CodeLanguage language, int nodeIndex)
            : base(controller, parentObject, dataType, hasNewKeyword, parameters, getAccessor, setAccessor, language, nodeIndex)
        {
        }

        public VBInterfaceIndexer(VBInterfaceIndexer iiToCopyFrom) : base(iiToCopyFrom)
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

        public override string DisplayName
        {
            get { throw new System.NotImplementedException(); }
        }

        public override string FullyQualifiedName
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}
