using System;
using System.Collections.Generic;
using System.Text;

namespace ArchAngel.Providers.CodeProvider.VB
{
    class VBUsingStatement : BaseUsingStatement
    {

        private VBUsingStatement(VBUsingStatement nsToCopyFrom) : base(null)
        {
            nsToCopyFrom.CloneInto(this);
            Alias = nsToCopyFrom.Alias;
            Value = nsToCopyFrom.Value;
        }
        /// <summary>
        /// Returns a shallow copy of the construct. Does not copy children.
        /// </summary>
        /// <returns>A shallow copy of the construct. Does not copy children.</returns>
        public override IBaseConstruct Clone()
        {
            return new VBUsingStatement(this);
        }

      
		protected override string ToStringInternal()
        {
		    throw new NotImplementedException("ToStringInternal");
        }

        public override string DisplayNameExtended
        {
            get { throw new NotImplementedException("DisplyNameExtended"); }
        }

        public override string DisplayName
        {
            get { throw new NotImplementedException("DisplyName"); }
        }
    }
}
