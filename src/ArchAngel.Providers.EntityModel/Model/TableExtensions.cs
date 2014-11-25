using System.Collections.Generic;
using ArchAngel.Providers.EntityModel.Model.DatabaseLayer;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using ArchAngel.Providers.EntityModel.Model.MappingLayer;

namespace ArchAngel.Providers.EntityModel.Model
{
    public static class TableExtensions
    {
        public static IEnumerable<ITable> PossibleAssociationTables(this ITable table)
        {
            MappingSet ms = table.m database.MappingSet;
            return ms == null ? new List<ITable>() : ms.GetAssociationTablesFor(database);
        }
    }
}
