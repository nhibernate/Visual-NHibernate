using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArchAngel.Providers.EntityModel.Model.EntityLayer
{
    public class Cache
    {
        public enum UsageTypes
        {
            None,
            Read_Only,
            Read_Write,
            NonStrict_Read_Write,
            Transactional
        }
        public enum IncludeTypes
        {
            All,
            Non_Lazy
        }
        private IncludeTypes _Include = IncludeTypes.All;

        public UsageTypes Usage { get; set; }

        public string Region { get; set; }

        public IncludeTypes Include { get; set; }
    }
}
