using IOTDatabaseTraveller.DataClasses;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTDatabaseTraveller.Datamanager
{
    public partial class DataManager
    {
        string dbName = "jim_ictprg431";
        string dbUser = "JIM-ictprg431_james";
        string dbPassword = "password1";
        int dbPort = 3306;
        string dbServer = "localhost";

        string DbConnectionString;
        MySqlConnection conn;

        public DataManager()
        {
            DbConnectionString = $"server={dbServer}; user={dbUser}; database={dbName}; port={dbPort}; password={dbPassword};";
            conn = new MySqlConnection(DbConnectionString);
        }
    }
}
