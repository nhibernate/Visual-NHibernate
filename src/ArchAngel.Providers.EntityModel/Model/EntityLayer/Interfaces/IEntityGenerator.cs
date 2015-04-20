using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArchAngel.Providers.EntityModel.Model.EntityLayer
{
    public interface IEntityGenerator
    {
        string ClassName { get; set; }
        //IList<IEntityGeneratorParameter> Parameters { get; set; }
    }

    //public interface IEntityGeneratorParameter
    //{
    //    string Name { get; set; }
    //    string Value { get; set; }
    //}
}
