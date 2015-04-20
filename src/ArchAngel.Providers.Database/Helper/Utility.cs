namespace ArchAngel.Providers.Database.Helper
{
    public class Utility
    {
        internal static void ResetAllConnections()
        {
            SQLServerDAL_2005.SQLServerBase.ResetConnection();
            SQLServerDAL_Express.SQLServerBase.ResetConnection();
        }
    }
}
