using ArchAngel.Providers.Database.Model;

namespace ArchAngel.Providers.Database.IDAL
{
    public interface ITable
    {
        Table[] GetTables();

        System.Data.DataTable RunQueryDataTable(string sql);
    }
}
