using ArchAngel.Providers.Database.Model;

namespace ArchAngel.Providers.Database.IDAL
{
    public interface IHelper
    {        
        Key GetPrimaryKey(Table table);

        bool IsDataTypeText(Column column);

        string[] GetDataTypes();
    }
}
