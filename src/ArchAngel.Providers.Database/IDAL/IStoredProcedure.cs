using ArchAngel.Providers.Database.Model;

namespace ArchAngel.Providers.Database.IDAL
{
    public interface IStoredProcedure
    {
        StoredProcedure[] GetStoredProcedures();
        void FillStoredProcedureColumns(StoredProcedure storedProcedure);
        string GetStoredProcedureBody(string storedProcedureName);
    }
}
