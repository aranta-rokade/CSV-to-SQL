using System.Configuration;

namespace SQLOperations
{
    class DatabaseConfig
    {
        private static string _ConnectionString;
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            }
        }
    }
}
