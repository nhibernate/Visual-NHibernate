using System;

namespace ArchAngel.Providers.Database.DALFactory
{
    public static class DataAccess
    {
    	public static IDAL.ITable CreateTable(DatabaseTypes dalAssemblyName, BLL.ConnectionStringHelper connectionString)
        {
            switch (dalAssemblyName)
            {
                case DatabaseTypes.SQLServer2000:
                    return new SQLServerDAL_2005.Table(connectionString);
                case DatabaseTypes.SQLServer2005:
                    return new SQLServerDAL_2005.Table(connectionString);
                case DatabaseTypes.SQLServerExpress:
                    return new SQLServerDAL_Express.Table(connectionString);
                default:
                    throw new NotImplementedException("Not handled yet: "+ dalAssemblyName);
            }
        }

        public static IDAL.IView CreateView(DatabaseTypes dalAssemblyName, BLL.ConnectionStringHelper connectionString)
        {
            switch (dalAssemblyName)
            {
                case DatabaseTypes.SQLServer2000:
                    return new SQLServerDAL_2005.View(connectionString);
                case DatabaseTypes.SQLServer2005:
                    return new SQLServerDAL_2005.View(connectionString);
                case DatabaseTypes.SQLServerExpress:
                    return new SQLServerDAL_Express.View(connectionString);
                default:
                    throw new NotImplementedException("Not handled yet: " + dalAssemblyName);
            }
        }

        public static IDAL.IStoredProcedure CreateStoredProcedure(DatabaseTypes dalAssemblyName, BLL.ConnectionStringHelper connectionString)
        {
            switch (dalAssemblyName)
            {
                case DatabaseTypes.SQLServer2000:
                    return new SQLServerDAL_2005.StoredProcedure(connectionString);
                case DatabaseTypes.SQLServer2005:
                    return new SQLServerDAL_2005.StoredProcedure(connectionString);
                case DatabaseTypes.SQLServerExpress:
                    return new SQLServerDAL_Express.StoredProcedure(connectionString);
                default:
                    throw new NotImplementedException("Not handled yet: " + dalAssemblyName);
            }
        }

        public static IDAL.IHelper CreateHelper(DatabaseTypes dalAssemblyName)
        {
            switch (dalAssemblyName)
            {
                case DatabaseTypes.SQLServer2000:
                    return new SQLServerDAL_2005.Helper();
                case DatabaseTypes.SQLServer2005:
                    return new SQLServerDAL_2005.Helper();
                case DatabaseTypes.SQLServerExpress:
                    return new SQLServerDAL_Express.Helper();
                default:
                    throw new NotImplementedException("Not handled yet: " + dalAssemblyName);
            }
        }
    }
}
