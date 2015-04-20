using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArchAngel.Providers.EntityModel.Model.MappingLayer
{
    public class Prefix
    {
        public Prefix(string value)
        {
            Value = value;
        }

        public string Value { get; set; }

        public bool IsPostfix { get; set; }
    }
}
