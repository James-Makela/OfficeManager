using IOTDatabaseTraveller.DataClasses;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        public List<ComboBoxStringIdItem> GetNameAndIDForCombo(List<ComboBoxStringIdItem> comboList, string sqlQuery)
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sqlQuery, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int.TryParse(reader[1].ToString(), out int id);
                    string name = reader[0].ToString();
                    ComboBoxStringIdItem nameId = new ComboBoxStringIdItem(name, id);
                    comboList.Add(nameId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            conn.Close();
            return comboList;
        }

        public void SqlNonQuery(string sqlQuery)
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sqlQuery, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }
    }
}
