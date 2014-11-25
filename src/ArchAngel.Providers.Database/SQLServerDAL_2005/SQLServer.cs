namespace ArchAngel.Providers.Database.SQLServerDAL_2005
{
    public static class SQLServer
    {
    	public static string[] GetSqlServers()
        {
            return Providers.Database.Helper.SQLServer.GetSqlServers();
        }
    }
}
